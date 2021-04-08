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
Public Class WSVentanasRescate
    Inherits System.Web.Services.WebService
    Private Const SP_CRUD As String = "PRC_VentanasRescateCRUD"
    Private Const SP_CONSULTAR As String = "PRC_CertificadoConsultar"

    Private Const SP_CERTIFICADOBUSCAR As String = "PRC_CertificadoBuscar"
    Private Const CONST_INSERT As String = "INSERT"
    Private Const CONST_UPDATE As String = "UPDATE"
    Private Const CONST_DELETE As String = "DELETE"
    Private Const SP_CERTIFICADO_RELACION As String = "PRC_CertificadoRelaciones"
    Private Const CONST_ACCION_RELACION As String = "PUEDE_BORRAR"

    Private Const CONST_SELECT_ALL As String = "SELECT_ALL"
    Private CONST_SELECT_FILTRO As String = "SELECT_FILTRO"
    Private Const CONST_SELECT_ONE As String = "SELECT_ONE"
    Private Const CONST_SELECT_RELACIONES As String = "SELECT_RELACIONES"

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function ConsultarNombreFondo(VentanasRescate As VentanasRescateDTO) As List(Of VentanasRescateDTO)
        Dim ListaVentanasRescate As List(Of VentanasRescateDTO) = New List(Of VentanasRescateDTO)
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)
        Dim ds As DataSet
        Dim fondoNew As New VentanasRescateDTO

        Try
            ' ---TODO Data prueba---
            sp.AgregarParametro("Accion", "SELECT_FILTRO_NOMBREFONDO", System.Data.SqlDbType.VarChar)

            ds = sp.ReturnDataSet()

            For Each dataRow As DataRow In ds.Tables(0).Rows
                fondoNew = New VentanasRescateDTO
                With fondoNew
                    .FN_Nombre_Corto = dataRow("FN_Nombre_Corto").ToString().Trim()
                    .FN_RUT = dataRow("FN_RUT").ToString().Trim()
                End With

                ListaVentanasRescate.Add(fondoNew)
            Next

        Catch ex As Exception
            Throw
        End Try

        Return ListaVentanasRescate
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function CompararDatosFondos(Fondos As FondoDTO) As List(Of FondoDTO)
        Dim Lista As List(Of FondoDTO) = New List(Of FondoDTO)
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)
        Dim ds As DataSet
        Dim fondonew As New FondoDTO

        Try
            sp.AgregarParametro("Accion", "COMPARAR_FONDOS", System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("FN_Nombre_Corto", Fondos.RazonSocial, System.Data.SqlDbType.VarChar)
            ds = sp.ReturnDataSet()

            For Each dataRow As DataRow In ds.Tables(0).Rows
                fondonew = New FondoDTO
                With fondonew
                    .RazonSocial = dataRow("FN_Razon_Social").ToString.Trim()
                    .Estado = dataRow("FN_Estado").ToString.Trim()
                End With
                Lista.Add(fondonew)
            Next
        Catch ex As Exception
            Throw
        End Try
        Return Lista
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function CompararDatosSeries(Series As FondoSerieDTO) As List(Of FondoSerieDTO)
        Dim Lista As List(Of FondoSerieDTO) = New List(Of FondoSerieDTO)
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)
        Dim ds As DataSet
        Dim fondoserienew As New FondoSerieDTO

        Try
            sp.AgregarParametro("Accion", "COMPARAR_SERIES", System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("FS_Nemotecnico", Series.Nemotecnico, System.Data.SqlDbType.VarChar)
            ds = sp.ReturnDataSet()

            For Each dataRow As DataRow In ds.Tables(0).Rows
                fondoserienew = New FondoSerieDTO
                With fondoserienew
                    .Estado = dataRow("FS_Estado").ToString.Trim()
                End With
                Lista.Add(fondoserienew)
            Next
        Catch ex As Exception
            Throw
        End Try
        Return Lista
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function ConsultarNemotecnico(VentanasRescate As VentanasRescateDTO) As List(Of VentanasRescateDTO)
        Dim ListaVentanasRescate As List(Of VentanasRescateDTO) = New List(Of VentanasRescateDTO)
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)
        Dim ds As DataSet
        Dim NemotecnicoNew As New VentanasRescateDTO

        Try
            ' ---TODO Data prueba---
            sp.AgregarParametro("Accion", "SELECT_FILTRO_NEMOTECNICO", System.Data.SqlDbType.VarChar)

            ds = sp.ReturnDataSet()

            For Each dataRow As DataRow In ds.Tables(0).Rows
                NemotecnicoNew = New VentanasRescateDTO
                With NemotecnicoNew
                    .FS_Nemotecnico = dataRow("FS_Nemotecnico").ToString().Trim()
                End With

                ListaVentanasRescate.Add(NemotecnicoNew)
            Next

        Catch ex As Exception
            Throw
        End Try

        Return ListaVentanasRescate
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function TraerMonedaDelFondo(ventanaRescate As VentanasRescateDTO) As String
        Dim returnMoneda As String = ""

        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)
        Dim ds As DataSet

        Try
            ' ---TODO Data prueba---
            sp.AgregarParametro("Accion", "TRAER_MONEDA_FONDO", System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("FN_Nombre_Corto", ventanaRescate.FN_Nombre_Corto, System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("FS_Nemotecnico", ventanaRescate.FS_Nemotecnico, System.Data.SqlDbType.VarChar)

            ds = sp.ReturnDataSet()

            If ds.Tables.Count() > 0 AndAlso ds.Tables(0).Rows.Count() > 0 Then
                Dim dataRow As DataRow
                dataRow = ds.Tables(0).Rows(0)
                returnMoneda = dataRow("FS_MONEDA")
            End If

        Catch ex As Exception
            Return returnMoneda
        End Try

        Return returnMoneda
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function ConsultarPorNombreFondo_Nemotecnico(VentanasRescate As VentanasRescateDTO) As List(Of VentanasRescateDTO)
        Dim listaVentanasRescate As List(Of VentanasRescateDTO) = New List(Of VentanasRescateDTO)
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)
        Dim ds As DataSet

        Try
            ' ---TODO Data prueba---
            sp.AgregarParametro("Accion", "SELECT_BY_NOM_NEMO", System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("FN_Nombre_Corto", VentanasRescate.FN_Nombre_Corto, System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("FS_Nemotecnico", VentanasRescate.FS_Nemotecnico, System.Data.SqlDbType.VarChar)

            ds = sp.ReturnDataSet()

            For Each dataRow As DataRow In ds.Tables(0).Rows
                listaVentanasRescate.Add(fillVentanasRescate(dataRow))
            Next

        Catch ex As Exception
            Throw
        End Try

        Return listaVentanasRescate
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function ValidaDiaHabil(fechaValidar As Date) As String
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)
        Dim ds As DataSet
        Try
            sp.AgregarParametro("Accion", "VALIDA_DIAHABIL", System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("fechaValidar", fechaValidar, System.Data.SqlDbType.Date)

            ds = sp.ReturnDataSet()

            If ds.Tables.Count > 0 AndAlso ds.Tables(0).Rows.Count > 0 Then
                Dim dataRow As DataRow = ds.Tables(0).Rows(0)
                Return dataRow("DiaHabil")
            Else
                Return Constantes.CONST_ERROR_BBDD
            End If

        Catch ex As Exception
            Return Constantes.CONST_ERROR_BBDD
        End Try
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function ExisteVentanasRescate(VentanasRescate As VentanasRescateDTO) As Integer
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)
        Dim ds As DataSet
        Try
            sp.AgregarParametro("Accion", "VENTANARESCATE_EXISTE", System.Data.SqlDbType.VarChar)
            FillParameters(VentanasRescate, sp)

            ds = sp.ReturnDataSet()

            If ds.Tables.Count > 0 AndAlso ds.Tables(0).Rows.Count > 0 Then
                Dim dataRow As DataRow = ds.Tables(0).Rows(0)
                Return dataRow("ExisteVentanaRescate")
            Else
                Return Constantes.CONST_ERROR_BBDD
            End If

        Catch ex As Exception
            Return Constantes.CONST_ERROR_BBDD
        End Try
    End Function


    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function GetVentanasRescate(VentanasRescate As VentanasRescateDTO) As VentanasRescateDTO
        Dim VentanasRescateRetorno As VentanasRescateDTO = New VentanasRescateDTO
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)
        Dim ds As DataSet

        Try
            sp.AgregarParametro("Accion", CONST_SELECT_ONE, System.Data.SqlDbType.VarChar)

            FillParameters(VentanasRescate, sp)

            ds = sp.ReturnDataSet()
            If ds.Tables.Count > 0 AndAlso ds.Tables(0).Rows.Count > 0 Then
                VentanasRescateRetorno = fillVentanasRescate(ds.Tables(0).Rows(0))
            Else
                VentanasRescateRetorno = Nothing
            End If

        Catch ex As Exception
            Throw
        End Try

        Return VentanasRescateRetorno
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function SelectFechasNORescatable(VentanasRescate As VentanasRescateDTO) As VentanasRescateDTO
        Dim VentanasRescateRetorno As VentanasRescateDTO = New VentanasRescateDTO
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)
        Dim ds As DataSet

        Try
            sp.AgregarParametro("Accion", "SELECT_VENTANA", System.Data.SqlDbType.VarChar)

            FillParameters(VentanasRescate, sp)

            ds = sp.ReturnDataSet()
            If ds.Tables.Count > 0 AndAlso ds.Tables(0).Rows.Count > 0 Then
                VentanasRescateRetorno = fillFechasVentanasRescate(ds.Tables(0).Rows(0))

            Else
                VentanasRescateRetorno = Nothing
            End If

        Catch ex As Exception
            Throw
        End Try

        Return VentanasRescateRetorno
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function GetCountVentanaRescate(VentanasRescate As VentanasRescateDTO) As Integer
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)
        Dim ds As DataSet
        Try
            sp.AgregarParametro("Accion", "SELECT_COUNT_VENTANA", System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("FN_Nombre_Corto", VentanasRescate.FN_Nombre_Corto, System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("FS_Nemotecnico", VentanasRescate.FS_Nemotecnico, System.Data.SqlDbType.VarChar)

            ds = sp.ReturnDataSet()

            If ds.Tables.Count > 0 AndAlso ds.Tables(0).Rows.Count > 0 Then
                Dim dataRow As DataRow = ds.Tables(0).Rows(0)
                Return dataRow("CantidadVentanaRescate")
            Else
                Return Constantes.CONST_ERROR_BBDD
            End If

        Catch ex As Exception
            Return Constantes.CONST_ERROR_BBDD
        End Try
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function VentanasRescateIngresar(VentanasRescate As VentanasRescateDTO) As Boolean
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)

        Dim RES_Fecha_Solicitud As Nullable(Of Date)
        Dim VTRES_Fecha_NAV As Nullable(Of Date)
        Dim VTRES_Fecha_Pago As Nullable(Of Date)
        Dim VTRES_Fecha_Ingreso As Nullable(Of Date)
        Dim VTRES_Fecha_Modificacion As Nullable(Of Date)
        Try

            If VentanasRescate.RES_Fecha_Solicitud = "0001-01-01" Then
                RES_Fecha_Solicitud = Nothing
            Else
                RES_Fecha_Solicitud = VentanasRescate.RES_Fecha_Solicitud
            End If

            If VentanasRescate.VTRES_Fecha_NAV = "0001-01-01" Then
                VTRES_Fecha_NAV = Nothing
            Else
                VTRES_Fecha_NAV = VentanasRescate.VTRES_Fecha_NAV
            End If

            If VentanasRescate.VTRES_Fecha_Pago = "0001-01-01" Then
                VTRES_Fecha_Pago = Nothing
            Else
                VTRES_Fecha_Pago = VentanasRescate.VTRES_Fecha_Pago
            End If

            If VentanasRescate.VTRES_Fecha_Ingreso = "0001-01-01" Then
                VTRES_Fecha_Ingreso = Nothing
            Else
                VTRES_Fecha_Ingreso = VentanasRescate.VTRES_Fecha_Ingreso
            End If

            If VentanasRescate.VTRES_Fecha_Modificacion = "0001-01-01" Then
                VTRES_Fecha_Modificacion = Nothing
            Else
                VTRES_Fecha_Modificacion = VentanasRescate.VTRES_Fecha_Modificacion
            End If

            sp.AgregarParametro("Accion", "INSERT", System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("FN_Nombre_Corto", VentanasRescate.FN_Nombre_Corto, System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("FS_Nemotecnico", VentanasRescate.FS_Nemotecnico, System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("RES_Fecha_Solicitud", RES_Fecha_Solicitud, System.Data.SqlDbType.DateTime)
            sp.AgregarParametro("VTRES_Fecha_NAV", VTRES_Fecha_NAV, System.Data.SqlDbType.DateTime)
            sp.AgregarParametro("VTRES_Fecha_Pago", VTRES_Fecha_Pago, System.Data.SqlDbType.DateTime)
            sp.AgregarParametro("VTRES_Usuario_Ingreso", VentanasRescate.VTRES_Usuario_Ingreso, System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("VTRES_Fecha_Ingreso", VTRES_Fecha_Ingreso, System.Data.SqlDbType.DateTime)
            sp.AgregarParametro("VTRES_Usuario_Modificacion", VentanasRescate.VTRES_Usuario_Modificacion, System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("VTRES_Fecha_Modificacion", VTRES_Fecha_Modificacion, System.Data.SqlDbType.DateTime)
            sp.AgregarParametro("VTRES_Estado", VentanasRescate.VTRES_Estado, System.Data.SqlDbType.Int)
            sp.AgregarParametro("FN_RUT", VentanasRescate.FN_RUT, System.Data.SqlDbType.VarChar)

            sp.ReturnDataSet()

            Return True
        Catch ex As Exception
            Throw
        End Try
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function FNConsultar(VentanasRescate As VentanasRescateDTO) As List(Of VentanasRescateDTO)
        Dim listaVentanasRescate As List(Of VentanasRescateDTO) = New List(Of VentanasRescateDTO)
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)
        Dim ds As DataSet

        Try
            sp.AgregarParametro("Accion", CONST_SELECT_ALL, System.Data.SqlDbType.VarChar)

            ds = sp.ReturnDataSet()

            For Each dataRow As DataRow In ds.Tables(0).Rows
                listaVentanasRescate.Add(fillVentanasRescate(dataRow))
            Next

        Catch ex As Exception
            Throw
        End Try

        Return listaVentanasRescate
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function VentanasRescateBuscarFiltro(VentanasRescate As VentanasRescateDTO) As List(Of VentanasRescateDTO)
        Dim listaVentanasRescate As List(Of VentanasRescateDTO) = New List(Of VentanasRescateDTO)
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)
        Dim ds As DataSet

        Try
            sp.AgregarParametro("Accion", CONST_SELECT_FILTRO, System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("FN_Nombre_Corto", VentanasRescate.FN_Nombre_Corto, System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("FS_Nemotecnico", VentanasRescate.FS_Nemotecnico, System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("fn_rut", VentanasRescate.FN_RUT, System.Data.SqlDbType.VarChar)


            ds = sp.ReturnDataSet()

            For Each dataRow As DataRow In ds.Tables(0).Rows
                listaVentanasRescate.Add(fillVentanasRescate(dataRow))
            Next

        Catch ex As Exception
            Throw
        End Try

        Return listaVentanasRescate
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function DeleteVentanasRescate(VentanasRescate As VentanasRescateDTO) As Boolean
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)
        Dim ds As DataSet
        Try
            sp.AgregarParametro("Accion", "DELETE_ONE", System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("FN_Nombre_Corto", VentanasRescate.FN_Nombre_Corto, System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("FS_Nemotecnico", VentanasRescate.FS_Nemotecnico, System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("RES_Fecha_Solicitud", VentanasRescate.RES_Fecha_Solicitud, System.Data.SqlDbType.DateTime)
            sp.AgregarParametro("VTRES_Fecha_NAV", VentanasRescate.VTRES_Fecha_NAV, System.Data.SqlDbType.DateTime)
            sp.AgregarParametro("VTRES_Fecha_Pago", VentanasRescate.VTRES_Fecha_Pago, System.Data.SqlDbType.DateTime)

            ds = sp.ReturnDataSet()
            Return True
        Catch ex As Exception
            Throw
        End Try

    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function DeleteVentanasRescateAll(VentanasRescate As VentanasRescateDTO) As Integer
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)
        Dim ds As DataSet
        Try
            sp.AgregarParametro("Accion", "DELETE_ALL", System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("FN_Nombre_Corto", VentanasRescate.FN_Nombre_Corto, System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("FS_Nemotecnico", VentanasRescate.FS_Nemotecnico, System.Data.SqlDbType.VarChar)
            ds = sp.ReturnDataSet()

            Return sp.FilasAfectas
        Catch ex As Exception
            Return 0
        End Try

    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function IngresarTemporalVentanasRescate(VentanasRescate As VentanasRescateDTO) As Boolean
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)

        Dim RES_Fecha_Solicitud As Nullable(Of Date)
        Dim VTRES_Fecha_NAV As Nullable(Of Date)
        Dim VTRES_Fecha_Pago As Nullable(Of Date)
        Dim VTRES_Fecha_Ingreso As Nullable(Of Date)
        Dim VTRES_Fecha_Modificacion As Nullable(Of Date)
        Try

            If VentanasRescate.RES_Fecha_Solicitud = "0001-01-01" Then
                RES_Fecha_Solicitud = Nothing
            Else
                RES_Fecha_Solicitud = VentanasRescate.RES_Fecha_Solicitud
            End If

            If VentanasRescate.VTRES_Fecha_NAV = "0001-01-01" Then
                VTRES_Fecha_NAV = Nothing
            Else
                VTRES_Fecha_NAV = VentanasRescate.VTRES_Fecha_NAV
            End If

            If VentanasRescate.VTRES_Fecha_Pago = "0001-01-01" Then
                VTRES_Fecha_Pago = Nothing
            Else
                VTRES_Fecha_Pago = VentanasRescate.VTRES_Fecha_Pago
            End If

            If VentanasRescate.VTRES_Fecha_Ingreso = "0001-01-01" Then
                VTRES_Fecha_Ingreso = Nothing
            Else
                VTRES_Fecha_Ingreso = VentanasRescate.VTRES_Fecha_Ingreso
            End If

            If VentanasRescate.VTRES_Fecha_Modificacion = "0001-01-01" Then
                VTRES_Fecha_Modificacion = Nothing
            Else
                VTRES_Fecha_Modificacion = VentanasRescate.VTRES_Fecha_Modificacion
            End If

            sp.AgregarParametro("Accion", "INSERTA_TEMPORAL", System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("VTRES_ID", VentanasRescate.VTRES_ID, System.Data.SqlDbType.Int)
            sp.AgregarParametro("FN_Nombre_Corto", VentanasRescate.FN_Nombre_Corto, System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("FS_Nemotecnico", VentanasRescate.FS_Nemotecnico, System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("RES_Fecha_Solicitud", RES_Fecha_Solicitud, System.Data.SqlDbType.DateTime)
            sp.AgregarParametro("VTRES_Fecha_NAV", VTRES_Fecha_NAV, System.Data.SqlDbType.DateTime)
            sp.AgregarParametro("VTRES_Fecha_Pago", VTRES_Fecha_Pago, System.Data.SqlDbType.DateTime)
            sp.AgregarParametro("VTRES_Usuario_Ingreso", VentanasRescate.VTRES_Usuario_Ingreso, System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("VTRES_Fecha_Ingreso", VTRES_Fecha_Ingreso, System.Data.SqlDbType.DateTime)
            sp.AgregarParametro("VTRES_Usuario_Modificacion", VentanasRescate.VTRES_Usuario_Modificacion, System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("VTRES_Fecha_Modificacion", VTRES_Fecha_Modificacion, System.Data.SqlDbType.DateTime)
            sp.AgregarParametro("VTRES_Estado", VentanasRescate.VTRES_Estado, System.Data.SqlDbType.Int)
            sp.AgregarParametro("FN_RUT", VentanasRescate.FN_RUT, System.Data.SqlDbType.VarChar)

            sp.ReturnDataSet()

            Return True
        Catch ex As Exception
            Throw
        End Try
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function ModificarEliminarVentanasRescate(VentanasRescate As VentanasRescateDTO) As Boolean
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)
        Try
            sp.AgregarParametro("Accion", "MODIFICA_VENTANASRESCATE", System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("FN_Nombre_Corto", VentanasRescate.FN_Nombre_Corto, System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("FS_Nemotecnico", VentanasRescate.FS_Nemotecnico, System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("VTRES_Usuario_Ingreso", VentanasRescate.VTRES_Usuario_Ingreso, System.Data.SqlDbType.VarChar)

            sp.ReturnDataSet()

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Function fillVentanasRescate(dataRow As DataRow) As VentanasRescateDTO
        Dim VentanasRescate As New VentanasRescateDTO

        With VentanasRescate
            .VTRES_ID = dataRow("VTRES_ID")
            .FN_Nombre_Corto = dataRow("FN_Nombre_Corto").ToString().Trim()
            .FS_Nemotecnico = dataRow("FS_Nemotecnico").ToString().Trim()
            .RES_Fecha_Solicitud = dataRow("RES_Fecha_Solicitud")
            .VTRES_Fecha_NAV = dataRow("VTRES_Fecha_NAV")
            .VTRES_Fecha_Pago = dataRow("VTRES_Fecha_Pago")
            .VTRES_Usuario_Ingreso = dataRow("VTRES_Usuario_Ingreso").ToString().Trim()
            .VTRES_Fecha_Ingreso = dataRow("VTRES_Fecha_Ingreso")
            .VTRES_Usuario_Modificacion = dataRow("VTRES_Usuario_Modificacion").ToString().Trim()
            .VTRES_Fecha_Modificacion = dataRow("VTRES_Fecha_Modificacion")
            .VTRES_Estado = dataRow("VTRES_Estado")
            .FN_RUT = dataRow("FN_RUT")
        End With

        Return VentanasRescate
    End Function

    Private Function fillFechasVentanasRescate(dataRow As DataRow) As VentanasRescateDTO
        Dim VentanasRescate As New VentanasRescateDTO
        With VentanasRescate
            .RES_Fecha_Solicitud = dataRow("RES_Fecha_Solicitud")
            .VTRES_Fecha_NAV = dataRow("VTRES_Fecha_NAV")
            .VTRES_Fecha_Pago = dataRow("VTRES_Fecha_Pago")
        End With
        Return VentanasRescate
    End Function


    Private Sub FillParameters(VentanasRescate As VentanasRescateDTO, sp As DBSqlServer.SqlServer.StoredProcedure)
        Dim RES_Fecha_Solicitud As Nullable(Of Date)
        Dim VTRES_Fecha_NAV As Nullable(Of Date)
        Dim VTRES_Fecha_Pago As Nullable(Of Date)
        Dim VTRES_Fecha_Ingreso As Nullable(Of Date)
        Dim VTRES_Fecha_Modificacion As Nullable(Of Date)

        If VentanasRescate.RES_Fecha_Solicitud = "0001-01-01" Then
            RES_Fecha_Solicitud = Nothing
        Else
            RES_Fecha_Solicitud = VentanasRescate.RES_Fecha_Solicitud
        End If

        If VentanasRescate.VTRES_Fecha_NAV = "0001-01-01" Then
            VTRES_Fecha_NAV = Nothing
        Else
            VTRES_Fecha_NAV = VentanasRescate.VTRES_Fecha_NAV
        End If

        If VentanasRescate.VTRES_Fecha_Pago = "0001-01-01" Then
            VTRES_Fecha_Pago = Nothing
        Else
            VTRES_Fecha_Pago = VentanasRescate.VTRES_Fecha_Pago
        End If

        If VentanasRescate.VTRES_Fecha_Ingreso = "0001-01-01" Then
            VTRES_Fecha_Ingreso = Nothing
        Else
            VTRES_Fecha_Ingreso = VentanasRescate.VTRES_Fecha_Ingreso
        End If

        If VentanasRescate.VTRES_Fecha_Modificacion = "0001-01-01" Then
            VTRES_Fecha_Modificacion = Nothing
        Else
            VTRES_Fecha_Modificacion = VentanasRescate.VTRES_Fecha_Modificacion
        End If


        sp.AgregarParametro("FN_Nombre_Corto", VentanasRescate.FN_Nombre_Corto, System.Data.SqlDbType.VarChar)
        sp.AgregarParametro("FS_Nemotecnico", VentanasRescate.FS_Nemotecnico, System.Data.SqlDbType.VarChar)
        sp.AgregarParametro("RES_Fecha_Solicitud", RES_Fecha_Solicitud, System.Data.SqlDbType.DateTime)
        sp.AgregarParametro("VTRES_Fecha_NAV", VTRES_Fecha_NAV, System.Data.SqlDbType.DateTime)
        sp.AgregarParametro("VTRES_Fecha_Pago", VTRES_Fecha_Pago, System.Data.SqlDbType.DateTime)
        sp.AgregarParametro("VTRES_Usuario_Ingreso", VentanasRescate.VTRES_Usuario_Ingreso, System.Data.SqlDbType.VarChar)
        sp.AgregarParametro("VTRES_Fecha_Ingreso", VTRES_Fecha_Ingreso, System.Data.SqlDbType.DateTime)
        sp.AgregarParametro("VTRES_Usuario_Modificacion", VentanasRescate.VTRES_Usuario_Modificacion, System.Data.SqlDbType.VarChar)
        sp.AgregarParametro("VTRES_Fecha_Modificacion", VTRES_Fecha_Modificacion, System.Data.SqlDbType.DateTime)
        sp.AgregarParametro("VTRES_Estado", VentanasRescate.VTRES_Estado, System.Data.SqlDbType.Int)
        sp.AgregarParametro("FN_RUT", VentanasRescate.FN_RUT, System.Data.SqlDbType.VarChar)

    End Sub

End Class