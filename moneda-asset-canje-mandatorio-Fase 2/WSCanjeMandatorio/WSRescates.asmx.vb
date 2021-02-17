Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel
Imports DTO
Imports System.Web.Script.Services

' Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente.
' <System.Web.Script.Services.ScriptService()> _
<System.Web.Services.WebService(Namespace:="http://tempuri.org/")> _
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<ToolboxItem(False)> _
Public Class WSRescates
    Inherits System.Web.Services.WebService

    Private Const SP_CRUD As String = "PRC_RescateCRUD"
    Private Const SP_CONSULTAS As String = "PRC_RescateConsultas"
    Private Const SP_FIJACION As String = "PRC_FijacionUpdate"

    Private Const CONST_INSERT As String = "INSERT"
    Private Const CONST_UPDATE As String = "UPDATE"
    Private Const CONST_DELETE As String = "DELETE"
    Private Const SP_CERTIFICADO_RELACION As String = "PRC_CertificadoRelaciones"
    Private Const CONST_ACCION_RELACION As String = "PUEDE_BORRAR"


    Private Const CONST_SELECT_ALL As String = "SELECT_ALL"
    Private CONST_SELECT_FILTRO As String = "SELECT_FILTRO"
    Private Const CONST_SELECT_ONE As String = "SELECT_ONE"
    Private Const CONST_SELECT_ONE_FIJACION As String = "SELECT_ONE_FIJACION"
    Private Const CONST_SELECT_RELACIONES As String = "SELECT_RELACIONES"
    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function ConsultarTodos(Rescate As RescatesDTO) As List(Of RescatesDTO)
        Dim listaRescates As List(Of RescatesDTO) = New List(Of RescatesDTO)
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)
        Dim ds As DataSet

        Try
            sp.AgregarParametro("Accion", CONST_SELECT_ALL, System.Data.SqlDbType.VarChar)

            ds = sp.ReturnDataSet()

            For Each dataRow As DataRow In ds.Tables(0).Rows
                listaRescates.Add(fillRescate(dataRow))
            Next

        Catch ex As Exception
            Throw
        End Try

        Return listaRescates
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function ConsultarPorFiltro(Rescate As RescatesDTO, FechaDesdeSolicitud As Nullable(Of Date), FechaHastaSolicitud As Nullable(Of Date), FechaDesdeNAV As Nullable(Of Date), FechaHastaNAV As Nullable(Of Date), FechaDesdePago As Nullable(Of Date), FechaHastaPago As Nullable(Of Date)) As List(Of RescatesDTO)
        Dim listaRescates As List(Of RescatesDTO) = New List(Of RescatesDTO)
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)
        Dim ds As DataSet

        Dim RES_Fecha_Solicitud As Nullable(Of Date)
        Dim RES_Fecha_Nav As Nullable(Of Date)
        Dim RES_Fecha_Pago As Nullable(Of Date)

        If Rescate.RES_Fecha_Solicitud = "0001-01-01" Then
            RES_Fecha_Solicitud = Nothing
        Else
            RES_Fecha_Solicitud = Rescate.RES_Fecha_Solicitud
        End If

        If Rescate.RES_Fecha_Nav = "0001-01-01" Then
            RES_Fecha_Nav = Nothing
        Else
            RES_Fecha_Nav = Rescate.RES_Fecha_Nav
        End If

        If Rescate.RES_Fecha_Pago = "0001-01-01" Then
            RES_Fecha_Pago = Nothing
        Else
            RES_Fecha_Pago = Rescate.RES_Fecha_Pago
        End If

        Try

            sp.AgregarParametro("Accion", CONST_SELECT_FILTRO, System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("AP_RUT", Rescate.AP_RUT, System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("AP_Razon_Social", Rescate.AP_Razon_Social, System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("FN_Nombre_Corto", Rescate.FN_Nombre_Corto, System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("FS_Nemotecnico", Rescate.FS_Nemotecnico, System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("RES_Estado", Rescate.RES_Estado, System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("FechaDesdeSolicitud", RES_Fecha_Solicitud, System.Data.SqlDbType.Date)
            sp.AgregarParametro("FechaHastaSolicitud", FechaHastaSolicitud, System.Data.SqlDbType.Date)
            sp.AgregarParametro("FechaDesdeNAV", RES_Fecha_Nav, System.Data.SqlDbType.Date)
            sp.AgregarParametro("FechaHastaNAV", FechaHastaNAV, System.Data.SqlDbType.Date)
            sp.AgregarParametro("FechaDesdePago", RES_Fecha_Pago, System.Data.SqlDbType.Date)
            sp.AgregarParametro("FechaHastaPago", FechaHastaPago, System.Data.SqlDbType.Date)


            ds = sp.ReturnDataSet()

            For Each dataRow As DataRow In ds.Tables(0).Rows
                listaRescates.Add(fillRescate(dataRow))
            Next

        Catch ex As Exception
            Throw
        End Try

        Return listaRescates
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function ControlMovil(rescate As RescatesDTO, fondo As FondoDTO) As Boolean
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure("PRC_RescateControl")
        Dim ds As DataSet
        Try
            FillParametrosControl(rescate, fondo, sp)
            sp.AgregarParametro("Accion", "Movil", System.Data.SqlDbType.VarChar)
            ds = sp.ReturnDataSet()

            If ds.Tables.Count > 0 AndAlso ds.Tables(0).Rows.Count > 0 Then
                Return ControlfillResultado(ds.Tables(0).Rows(0))
            Else
                Return False
            End If
        Catch ex As Exception
            Return False
        End Try
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function Prorrata(stringID As String, ByRef stringError As String) As String
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure("PRC_RescatesProrrata")
        Dim ds As DataSet

        Try
            sp.AgregarParametro("ListaIDFondo", stringID, System.Data.SqlDbType.VarChar)
            ds = sp.ReturnDataSet()

            If ds.Tables.Count > 0 AndAlso ds.Tables(0).Rows.Count > 0 Then
                Dim DataRow As DataRow = ds.Tables(0).Rows(0)
                stringError = ""
                Return DataRow("Resultado")
            Else
                stringError = "Error no se encontraron datos para el prorroteo"
                Return "NOK"
            End If
        Catch ex As Exception
            stringError = "ERROR: " & ex.Message
            Return "NOK"
        End Try
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function ControlVentana(rescate As RescatesDTO, fondo As FondoDTO) As Boolean
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure("PRC_RescateControl")
        Dim ds As DataSet
        Try
            FillParametrosControl(rescate, fondo, sp)
            sp.AgregarParametro("Accion", "Ventana", System.Data.SqlDbType.VarChar)
            ds = sp.ReturnDataSet()

            If ds.Tables.Count > 0 AndAlso ds.Tables(0).Rows.Count > 0 Then
                Return ControlfillResultado(ds.Tables(0).Rows(0))
            Else
                Return False
            End If
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Shared Sub FillParametrosControl(rescate As RescatesDTO, fondo As FondoDTO, sp As DBSqlServer.SqlServer.StoredProcedure)
        sp.AgregarParametro("FechaSolucitud", rescate.RES_Fecha_Solicitud, System.Data.SqlDbType.VarChar)
        sp.AgregarParametro("dias", fondo.ControlDiasAVerificar, System.Data.SqlDbType.Int)
        sp.AgregarParametro("soloDiasHabiles", IIf(fondo.ControlDiasHabiles = -1, 1, 0), System.Data.SqlDbType.Int)
        sp.AgregarParametro("pais", IIf(rescate.FS_Moneda = "USD", "USA", "CHILE"), System.Data.SqlDbType.VarChar)
        sp.AgregarParametro("rutFondo", fondo.Rut, System.Data.SqlDbType.VarChar)
        sp.AgregarParametro("monto", rescate.RES_Monto, System.Data.SqlDbType.Decimal)
        sp.AgregarParametro("nemotecnico", rescate.FS_Nemotecnico, System.Data.SqlDbType.VarChar)
    End Sub

    Private Function ControlfillResultado(dataRow As DataRow) As Boolean
        Return dataRow("Resultado")
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function GetRescateOne(Rescate As RescatesDTO) As RescatesDTO
        Dim RescateRetorno As RescatesDTO = New RescatesDTO
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)
        Dim ds As DataSet

        Try
            sp.AgregarParametro("Accion", CONST_SELECT_ONE, System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("RES_ID", Rescate.RES_ID, System.Data.SqlDbType.Decimal)


            ds = sp.ReturnDataSet()
            If ds.Tables.Count > 0 AndAlso ds.Tables(0).Rows.Count > 0 Then
                RescateRetorno = fillRescate(ds.Tables(0).Rows(0))
            Else
                RescateRetorno = Nothing
            End If

        Catch ex As Exception
            Throw
        End Try

        Return RescateRetorno
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function CargarFiltroNombreAportante(Rescate As RescatesDTO) As List(Of RescatesDTO)
        Dim Lista As List(Of RescatesDTO) = New List(Of RescatesDTO)
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)
        Dim ds As DataSet
        Dim RescateNew As New RescatesDTO

        Try
            sp.AgregarParametro("Accion", "SELECT_FILTRO_NOMBREAPORT", System.Data.SqlDbType.VarChar)

            FillParameters(Rescate, sp)

            ds = sp.ReturnDataSet()

            For Each dataRow As DataRow In ds.Tables(0).Rows
                RescateNew = New RescatesDTO
                With RescateNew
                    .AP_Razon_Social = dataRow("AP_Razon_Social").ToString().Trim()
                    .AP_RUT = dataRow("AP_RUT").ToString().Trim()
                End With
                Lista.Add(RescateNew)
            Next


        Catch ex As Exception
            Throw
        End Try

        Return Lista
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function GetFijacionId(Id As Int32) As RescatesDTO
        Dim rescateRetorno As RescatesDTO = New RescatesDTO
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_FIJACION)
        Dim ds As DataSet

        Try
            sp.AgregarParametro("Accion", CONST_SELECT_ONE_FIJACION, System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("Id", Id, System.Data.SqlDbType.Int)
            sp.AgregarParametro("TipoTransaccion", "Rescate", System.Data.SqlDbType.VarChar)

            ds = sp.ReturnDataSet()
            If ds.Tables.Count > 0 AndAlso ds.Tables(0).Rows.Count > 0 Then
                rescateRetorno = fillRescate(ds.Tables(0).Rows(0))
            Else
                rescateRetorno = Nothing
            End If

        Catch ex As Exception
            Throw
        End Try
        Return rescateRetorno
    End Function


    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function CargarFiltroNombreFondo(Rescate As RescatesDTO) As List(Of RescatesDTO)
        Dim Lista As List(Of RescatesDTO) = New List(Of RescatesDTO)
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)
        Dim ds As DataSet
        Dim RescateNew As New RescatesDTO

        Try
            sp.AgregarParametro("Accion", "SELECT_FILTRO_NOMBREFONDO", System.Data.SqlDbType.VarChar)
            FillParameters(Rescate, sp)

            ds = sp.ReturnDataSet()

            For Each dataRow As DataRow In ds.Tables(0).Rows
                RescateNew = New RescatesDTO
                With RescateNew
                    .FN_Nombre_Corto = dataRow("FN_Nombre_Corto").ToString().Trim()
                    .FN_RUT = dataRow("FN_RUT").ToString().Trim()
                End With
                Lista.Add(RescateNew)
            Next


        Catch ex As Exception
            Throw
        End Try

        Return Lista
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function CargarFiltroNemotecnico(Rescate As RescatesDTO) As List(Of RescatesDTO)
        Dim Lista As List(Of RescatesDTO) = New List(Of RescatesDTO)
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)
        Dim ds As DataSet
        Dim RescateNew As New RescatesDTO

        Try
            sp.AgregarParametro("Accion", "SELECT_FILTRO_NEMOTECNICO", System.Data.SqlDbType.VarChar)
            FillParameters(Rescate, sp)

            ds = sp.ReturnDataSet()

            For Each dataRow As DataRow In ds.Tables(0).Rows
                RescateNew = New RescatesDTO
                With RescateNew
                    .FS_Nemotecnico = dataRow("FS_Nemotecnico").ToString().Trim()
                End With
                Lista.Add(RescateNew)
            Next


        Catch ex As Exception
            Throw
        End Try

        Return Lista
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function SelectFechaPagoSIRescatable(FechaPagoFondoRescatableINT As Integer, FechaCalculo As DateTime, SoloDiasHabiles As Integer)
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)
        Dim ds As DataSet
        Dim FechaPago As DateTime

        Try
            Dim Fecha_Calculo As Nullable(Of Date)

            If FechaCalculo = "0001-01-01" Or FechaCalculo = "0001-01-02" Then
                Fecha_Calculo = Nothing
            Else
                Fecha_Calculo = FechaCalculo
            End If

            sp.AgregarParametro("Accion", "SELECT_FECHARESCATE", System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("FechaPagoFondoRescatableINT", FechaPagoFondoRescatableINT, System.Data.SqlDbType.Int)
            sp.AgregarParametro("FechaCalculo", Fecha_Calculo, System.Data.SqlDbType.DateTime)
            sp.AgregarParametro("SoloDiasHabiles", SoloDiasHabiles, System.Data.SqlDbType.Int)

            ds = sp.ReturnDataSet()

            If ds.Tables.Count > 0 AndAlso ds.Tables(0).Rows.Count > 0 Then
                Dim dataRow As DataRow = ds.Tables(0).Rows(0)
                FechaPago = IIf(IsDBNull(dataRow("FechaPago")), Nothing, dataRow("FechaPago").ToString().Trim())
            Else
                FechaPago = Nothing
            End If

        Catch ex As Exception
            Throw
        End Try

        Return FechaPago
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function ConsultarTransito(rescate As RescatesDTO) As List(Of RescatesDTO)
        Dim Lista As List(Of RescatesDTO) = New List(Of RescatesDTO)
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CONSULTAS)
        Dim ds As DataSet
        Try
            Dim Fecha_Pago As Nullable(Of Date)

            If rescate.RES_Fecha_Pago = "0001-01-01" Or rescate.RES_Fecha_Pago = "0001-01-02" Then
                Fecha_Pago = Nothing
            Else
                Fecha_Pago = rescate.RES_Fecha_Pago
            End If

            sp.AgregarParametro("Accion", "SELECT_BY", System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("AP_RUT", rescate.AP_RUT, System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("FS_Nemotecnico", rescate.FS_Nemotecnico, System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("RES_Fecha_Pago", Fecha_Pago, System.Data.SqlDbType.DateTime)

            ds = sp.ReturnDataSet()

            For Each dataRow As DataRow In ds.Tables(0).Rows
                Lista.Add(fillRescate(dataRow))
            Next

        Catch ex As Exception
            Throw
        End Try

        Return Lista

    End Function


    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function SelectRescatesTransito(Rescate As RescatesDTO) As RescatesDTO
        Dim RescateRetorno As RescatesDTO = New RescatesDTO
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)
        Dim ds As DataSet

        Try
            sp.AgregarParametro("Accion", "SELECT_RESCATE_TRANSITO2", System.Data.SqlDbType.VarChar)
            FillParameters(Rescate, sp)


            ds = sp.ReturnDataSet()
            If ds.Tables.Count > 0 AndAlso ds.Tables(0).Rows.Count > 0 Then
                Dim dataRow As DataRow = ds.Tables(0).Rows(0)
                RescateRetorno.RES_Cuotas = dataRow("RES_Cuotas")
            End If

        Catch ex As Exception
            Throw
        End Try

        Return RescateRetorno
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function SelectRescatesTransito2(Rescate As RescatesDTO) As RescatesDTO
        Dim RescateRetorno As RescatesDTO = New RescatesDTO
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)
        Dim ds As DataSet

        Try
            sp.AgregarParametro("Accion", "SELECT_RESCATE_TRANSITO", System.Data.SqlDbType.VarChar)
            FillParameters(Rescate, sp)


            ds = sp.ReturnDataSet()
            If ds.Tables.Count > 0 AndAlso ds.Tables(0).Rows.Count > 0 Then
                Dim dataRow As DataRow = ds.Tables(0).Rows(0)
                RescateRetorno.RES_Cuotas = dataRow("RES_Cuotas")
            End If

        Catch ex As Exception
            Throw
        End Try

        Return RescateRetorno
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function SelectRescatesHoy(Rescate As RescatesDTO) As RescatesDTO
        Dim RescateRetorno As RescatesDTO = New RescatesDTO
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)
        Dim ds As DataSet

        Try
            Dim RES_Fecha_Solicitud As Nullable(Of Date)

            If Rescate.RES_Fecha_Solicitud = "0001-01-01" Then
                RES_Fecha_Solicitud = Nothing
            Else
                RES_Fecha_Solicitud = Rescate.RES_Fecha_Solicitud
            End If

            sp.AgregarParametro("Accion", "SELECT_RESCATES_DIA", System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("RES_Fecha_Solicitud", RES_Fecha_Solicitud, System.Data.SqlDbType.DateTime)
            sp.AgregarParametro("FS_Nemotecnico", Rescate.FS_Nemotecnico, System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("FN_RUT", Rescate.FN_RUT, System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("AP_RUT", Rescate.AP_RUT, System.Data.SqlDbType.VarChar)


            ds = sp.ReturnDataSet()
            If ds.Tables.Count > 0 AndAlso ds.Tables(0).Rows.Count > 0 Then
                Dim dataRow As DataRow = ds.Tables(0).Rows(0)
                RescateRetorno.RES_Monto = dataRow("RES_Monto")
            End If

        Catch ex As Exception
            Throw
        End Try

        Return RescateRetorno
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function FNAIngresar(Rescate As RescatesDTO)
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)
        Dim ds As DataSet
        Dim RescateRetorno As RescatesDTO = New RescatesDTO
        Try
            sp.AgregarParametro("Accion", CONST_INSERT, System.Data.SqlDbType.VarChar)

            FillParameters(Rescate, sp)
            ds = sp.ReturnDataSet()
            If ds.Tables.Count > 0 AndAlso ds.Tables(0).Rows.Count > 0 Then
                Dim dataRow As DataRow = ds.Tables(0).Rows(0)
                Try
                    RescateRetorno.RES_ID = dataRow("RES_ID")
                Catch ex As Exception
                    RescateRetorno.RES_ID = 0
                End Try
            End If

            Return RescateRetorno
        Catch ex As Exception
            Return False
        End Try
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function FNModificar(Rescate As RescatesDTO) As Boolean
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)

        Try
            ' ---TODO Data prueba---
            sp.AgregarParametro("Accion", CONST_UPDATE, System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("RES_ID", Rescate.RES_ID, System.Data.SqlDbType.Decimal)
            FillParameters(Rescate, sp)

            sp.ReturnDataSet()

            Return (sp.FilasAfectas > 0)
        Catch ex As Exception
            Return False
        End Try
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function FNEliminar(Rescate As RescatesDTO) As Boolean
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)

        Try
            ' ---TODO Data prueba---
            sp.AgregarParametro("Accion", CONST_DELETE, System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("RES_ID", Rescate.RES_ID, System.Data.SqlDbType.Decimal)

            sp.ReturnDataSet()

            Return (sp.FilasAfectas > 0)
        Catch ex As Exception
            Return False
        End Try
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function RecalculoFijacionNAV(Rescate As RescatesDTO) As Boolean
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)

        Try
            ' ---TODO Data prueba---
            sp.AgregarParametro("Accion", "RECALCULO_FIJACION_NAV", System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("RES_ID", Rescate.RES_ID, System.Data.SqlDbType.Decimal)
            sp.AgregarParametro("RES_Nav", Rescate.RES_Nav, System.Data.SqlDbType.Decimal)
            sp.AgregarParametro("RES_Nav_CLP", Rescate.RES_Nav_CLP, System.Data.SqlDbType.Decimal)
            sp.AgregarParametro("RES_Monto", Rescate.RES_Monto, System.Data.SqlDbType.Decimal)
            sp.AgregarParametro("RES_Monto_CLP", Rescate.RES_Monto_CLP, System.Data.SqlDbType.Decimal)


            sp.ReturnDataSet()

            Return (sp.FilasAfectas > 0)
        Catch ex As Exception
            Return False
        End Try
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function RecalculoFijacionTC(Rescate As RescatesDTO) As Boolean
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)

        Try
            ' ---TODO Data prueba---
            sp.AgregarParametro("Accion", "RECALCULO_FIJACION_TC", System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("RES_ID", Rescate.RES_ID, System.Data.SqlDbType.Decimal)
            sp.AgregarParametro("RES_Nav", Rescate.RES_Nav, System.Data.SqlDbType.Decimal)
            sp.AgregarParametro("RES_Nav_CLP", Rescate.RES_Nav_CLP, System.Data.SqlDbType.Decimal)
            sp.AgregarParametro("RES_Monto", Rescate.RES_Monto, System.Data.SqlDbType.Decimal)
            sp.AgregarParametro("RES_Monto_CLP", Rescate.RES_Monto_CLP, System.Data.SqlDbType.Decimal)
            sp.AgregarParametro("TC_Valor", Rescate.TC_Valor, System.Data.SqlDbType.Decimal)


            sp.ReturnDataSet()

            Return (sp.FilasAfectas > 0)
        Catch ex As Exception
            Return False
        End Try
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function GetRelaciones(Rescate As RescatesDTO)
        Dim RescateRetorno As RescatesDTO = New RescatesDTO
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)
        Dim ds As DataSet

        Try
            sp.AgregarParametro("Accion", "RELACIONES", System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("RES_ID", Rescate.RES_ID, System.Data.SqlDbType.Int)

            ds = sp.ReturnDataSet()
            If ds.Tables.Count > 0 AndAlso ds.Tables(0).Rows.Count > 0 Then
                Dim dataRow As DataRow = ds.Tables(0).Rows(0)
                Try
                    RescateRetorno.CountAP = dataRow("CountAP")
                    RescateRetorno.CountFS = dataRow("CountFS")
                    RescateRetorno.CountFN = dataRow("CountFN")
                Catch ex As Exception
                    RescateRetorno.CountAP = 0
                    RescateRetorno.CountFS = 0
                    RescateRetorno.CountFN = 0
                End Try
            End If
        Catch ex As Exception
            Throw
        End Try

        Return RescateRetorno
    End Function

    Private Function fillRescate(dataRow As DataRow) As RescatesDTO
        Dim Rescate As New RescatesDTO
        With Rescate
            .RES_ID = dataRow("RES_ID")
            .RES_Fecha_Solicitud = dataRow("RES_Fecha_Solicitud").ToString().Trim()
            .RES_Fecha_Pago = dataRow("RES_Fecha_Pago").ToString().Trim()
            .AP_RUT = dataRow("AP_RUT")
            .AP_Multifondo = dataRow("AP_Multifondo").ToString().Trim()
            .FS_Nemotecnico = dataRow("FS_Nemotecnico").ToString().Trim()
            .RES_Cuotas = dataRow("RES_Cuotas").ToString().Trim()
            .Res_Fecha_Carga = dataRow("Res_Fecha_Carga").ToString().Trim()
            .FN_RUT = dataRow("FN_RUT").ToString().Trim()
            .FN_Nombre_Corto = dataRow("FN_Nombre_Corto").ToString().Trim()
            .RES_Tipo_Transaccion = dataRow("RES_Tipo_Transaccion").ToString().Trim()
            .AP_Razon_Social = dataRow("AP_Razon_Social").ToString().Trim()
            .FS_Nombre_Corto = dataRow("FS_Nombre_Corto").ToString().Trim()
            .RES_Moneda_Pago = dataRow("RES_Moneda_Pago").ToString().Trim()
            .ADCV_Cantidad = dataRow("ADCV_Cantidad").ToString().Trim()
            .RES_Fecha_Nav = dataRow("RES_Fecha_Nav").ToString().Trim()
            .RES_FechaTCObs = dataRow("RES_FechaTCObs").ToString().Trim()
            .RES_Nav = dataRow("RES_Nav").ToString().Trim()
            .RES_Monto = dataRow("RES_Monto").ToString().Trim()
            .RES_Nav_CLP = dataRow("RES_Nav_CLP").ToString().Trim()
            .RES_Monto_CLP = dataRow("RES_Monto_CLP").ToString().Trim()
            .TC_Valor = dataRow("TC_Valor").ToString().Trim()
            .RES_Contrato = dataRow("RES_Contrato").ToString().Trim()
            .RES_Poderes = dataRow("RES_Poderes").ToString().Trim()
            .RES_Estado = dataRow("RES_Estado").ToString().Trim()
            .RES_Observaciones = dataRow("RES_Observaciones").ToString().Trim()
            .RES_Patrimonio = dataRow("RES_Patrimonio").ToString().Trim()
            .FS_Patrimonio = dataRow("FS_Patrimonio").ToString().Trim()
            .RES_Disponible_Patrimonio = dataRow("RES_Disponible_Patrimonio").ToString().Trim()
            .ADCV_Fecha = dataRow("ADCV_Fecha").ToString().Trim()
            .SC_Cuotas_a_Suscribir = dataRow("SC_Cuotas_a_Suscribir").ToString().Trim()
            .CN_Cuotas_Disponibles = dataRow("CN_Cuotas_Disponibles").ToString().Trim()
            .RES_Cuotas_Disponibles = dataRow("RES_Cuotas_Disponibles").ToString().Trim()
            .RES_Transito = dataRow("RES_Transito").ToString().Trim()
            .RES_Fijacion_NAV = dataRow("RES_Fijacion_NAV").ToString().Trim()
            .RES_Fijacion_TCObservado = dataRow("RES_Fijacion_TCObservado").ToString().Trim()
            .RES_Fecha_Ingreso = dataRow("RES_Fecha_Ingreso").ToString().Trim()
            .RES_Usuario_Ingreso = dataRow("RES_Usuario_Ingreso").ToString().Trim()
            .RES_Fecha_Modificacion = dataRow("RES_Fecha_Modificacion").ToString().Trim()
            .RES_Usuario_Modificacion = dataRow("RES_Usuario_Modificacion").ToString().Trim()
            .RES_Estado_Rescate = dataRow("RES_Estado_Rescate").ToString().Trim()
            .FS_Moneda = dataRow("FS_Moneda").ToString().Trim()
            .RES_Maximo = dataRow("RES_Maximo").ToString().Trim()
            .RES_Utilizado = dataRow("RES_Utilizado").ToString().Trim()

        End With
        Return Rescate
    End Function

    Private Sub FillParameters(Rescate As RescatesDTO, sp As DBSqlServer.SqlServer.StoredProcedure)

        Dim RES_Fecha_Solicitud As Nullable(Of Date)
        Dim RES_Fecha_Pago As Nullable(Of Date)
        Dim Res_Fecha_Carga As Nullable(Of Date)
        Dim RES_Fecha_Nav As Nullable(Of Date)
        Dim RES_FechaTCObs As Nullable(Of Date)
        Dim ADCV_Fecha As Nullable(Of Date)
        Dim RES_Fecha_Ingreso As Nullable(Of Date)
        Dim RES_Fecha_Modificacion As Nullable(Of Date)

        If Rescate.RES_Fecha_Solicitud = "0001-01-01" Then
            RES_Fecha_Solicitud = Nothing
        Else
            RES_Fecha_Solicitud = Rescate.RES_Fecha_Solicitud
        End If

        If Rescate.RES_Fecha_Pago = "0001-01-01" Then
            RES_Fecha_Pago = Nothing
        Else
            RES_Fecha_Pago = Rescate.RES_Fecha_Pago
        End If

        If Rescate.Res_Fecha_Carga = "0001-01-01" Then
            Res_Fecha_Carga = Nothing
        Else
            Res_Fecha_Carga = Rescate.Res_Fecha_Carga
        End If

        If Rescate.RES_Fecha_Nav = "0001-01-01" Then
            RES_Fecha_Nav = Nothing
        Else
            RES_Fecha_Nav = Rescate.RES_Fecha_Nav
        End If

        If Rescate.RES_FechaTCObs = "0001-01-01" Then
            RES_FechaTCObs = Nothing
        Else
            RES_FechaTCObs = Rescate.RES_FechaTCObs
        End If

        If Rescate.ADCV_Fecha = "0001-01-01" Then
            ADCV_Fecha = Nothing
        Else
            ADCV_Fecha = Rescate.ADCV_Fecha
        End If

        If Rescate.RES_Fecha_Ingreso = "0001-01-01" Then
            RES_Fecha_Ingreso = Nothing
        Else
            RES_Fecha_Ingreso = Rescate.RES_Fecha_Ingreso
        End If

        If Rescate.RES_Fecha_Modificacion = "0001-01-01" Then
            RES_Fecha_Modificacion = Nothing
        Else
            RES_Fecha_Modificacion = Rescate.RES_Fecha_Modificacion
        End If

        sp.AgregarParametro("RES_Fecha_Solicitud", RES_Fecha_Solicitud, System.Data.SqlDbType.DateTime)
        sp.AgregarParametro("RES_Fecha_Pago", RES_Fecha_Pago, System.Data.SqlDbType.DateTime)
        sp.AgregarParametro("AP_RUT", Rescate.AP_RUT, System.Data.SqlDbType.VarChar)
        sp.AgregarParametro("AP_Multifondo", Rescate.AP_Multifondo, System.Data.SqlDbType.VarChar)
        sp.AgregarParametro("FS_Nemotecnico", Rescate.FS_Nemotecnico, System.Data.SqlDbType.VarChar)
        sp.AgregarParametro("RES_Cuotas", Rescate.RES_Cuotas, System.Data.SqlDbType.Decimal)
        sp.AgregarParametro("Res_Fecha_Carga", Res_Fecha_Carga, System.Data.SqlDbType.DateTime)
        sp.AgregarParametro("FN_RUT", Rescate.FN_RUT, System.Data.SqlDbType.VarChar)
        sp.AgregarParametro("FN_Nombre_Corto", Rescate.FN_Nombre_Corto, System.Data.SqlDbType.VarChar)
        sp.AgregarParametro("RES_Tipo_Transaccion", Rescate.RES_Tipo_Transaccion, System.Data.SqlDbType.VarChar)
        sp.AgregarParametro("AP_Razon_Social", Rescate.AP_Razon_Social, System.Data.SqlDbType.VarChar)
        sp.AgregarParametro("FS_Nombre_Corto", Rescate.FS_Nombre_Corto, System.Data.SqlDbType.VarChar)
        sp.AgregarParametro("RES_Moneda_Pago", Rescate.RES_Moneda_Pago, System.Data.SqlDbType.VarChar)
        sp.AgregarParametro("ADCV_Cantidad", Rescate.ADCV_Cantidad, System.Data.SqlDbType.Decimal)
        sp.AgregarParametro("RES_Fecha_Nav", RES_Fecha_Nav, System.Data.SqlDbType.DateTime)
        sp.AgregarParametro("RES_FechaTCObs", RES_FechaTCObs, System.Data.SqlDbType.DateTime)
        sp.AgregarParametro("RES_Nav", Rescate.RES_Nav, System.Data.SqlDbType.Decimal)
        sp.AgregarParametro("RES_Monto", Rescate.RES_Monto, System.Data.SqlDbType.Decimal)
        sp.AgregarParametro("RES_Nav_CLP", Rescate.RES_Nav_CLP, System.Data.SqlDbType.Decimal)
        sp.AgregarParametro("RES_Monto_CLP", Rescate.RES_Monto_CLP, System.Data.SqlDbType.Decimal)
        sp.AgregarParametro("TC_Valor", Rescate.TC_Valor, System.Data.SqlDbType.Decimal)
        sp.AgregarParametro("RES_Contrato", Rescate.RES_Contrato, System.Data.SqlDbType.VarChar)
        sp.AgregarParametro("RES_Poderes", Rescate.RES_Poderes, System.Data.SqlDbType.VarChar)
        sp.AgregarParametro("RES_Estado", Rescate.RES_Estado, System.Data.SqlDbType.VarChar)
        sp.AgregarParametro("RES_Observaciones", Rescate.RES_Observaciones, System.Data.SqlDbType.VarChar)
        sp.AgregarParametro("RES_Patrimonio", Rescate.RES_Patrimonio, System.Data.SqlDbType.Decimal)
        sp.AgregarParametro("FS_Patrimonio", Rescate.FS_Patrimonio, System.Data.SqlDbType.VarChar)
        sp.AgregarParametro("RES_Disponible_Patrimonio", Rescate.RES_Disponible_Patrimonio, System.Data.SqlDbType.Decimal)
        sp.AgregarParametro("ADCV_Fecha", ADCV_Fecha, System.Data.SqlDbType.DateTime)
        sp.AgregarParametro("SC_Cuotas_a_Suscribir", Rescate.SC_Cuotas_a_Suscribir, System.Data.SqlDbType.Decimal)
        sp.AgregarParametro("CN_Cuotas_Disponibles", Rescate.CN_Cuotas_Disponibles, System.Data.SqlDbType.Decimal)
        sp.AgregarParametro("RES_Cuotas_Disponibles", Rescate.RES_Cuotas_Disponibles, System.Data.SqlDbType.Decimal)
        sp.AgregarParametro("RES_Transito", Rescate.RES_Transito, System.Data.SqlDbType.Decimal)
        sp.AgregarParametro("RES_Fijacion_NAV", Rescate.RES_Fijacion_NAV, System.Data.SqlDbType.VarChar)
        sp.AgregarParametro("RES_Fijacion_TCObservado", Rescate.RES_Fijacion_TCObservado, System.Data.SqlDbType.VarChar)
        sp.AgregarParametro("RES_Fecha_Ingreso", RES_Fecha_Ingreso, System.Data.SqlDbType.DateTime)
        sp.AgregarParametro("RES_Usuario_Ingreso", Rescate.RES_Usuario_Ingreso, System.Data.SqlDbType.VarChar)
        sp.AgregarParametro("RES_Fecha_Modificacion", RES_Fecha_Modificacion, System.Data.SqlDbType.DateTime)
        sp.AgregarParametro("RES_Usuario_Modificacion", Rescate.RES_Usuario_Modificacion, System.Data.SqlDbType.VarChar)
        sp.AgregarParametro("RES_Estado_Rescate", Rescate.RES_Estado_Rescate, System.Data.SqlDbType.Int)
        sp.AgregarParametro("FS_Moneda", Rescate.FS_Moneda, System.Data.SqlDbType.VarChar)
        sp.AgregarParametro("RES_Maximo", Rescate.RES_Maximo, System.Data.SqlDbType.Decimal)
        sp.AgregarParametro("RES_Utilizado", Rescate.RES_Utilizado, System.Data.SqlDbType.Decimal)

    End Sub

End Class