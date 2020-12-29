Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel
Imports DTO
Imports System.Web.Script.Services

' Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente.
' <System.Web.Script.Services.ScriptService()> _
<System.Web.Services.WebService(Namespace:="http://tempuri.org/")>
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)>
<ToolboxItem(False)>
Public Class WSFijacion
    Inherits System.Web.Services.WebService

    Private Const SP_CRUD As String = "PRC_FijacionCRUD"
    Private Const SP_CONSULTAS As String = "PRC_FijacionBusqueda"
    Private Const SP_UPDATE As String = "PRC_FijacionUpdate"

    Private Const CONST_INSERT As String = "INSERT"
    Private Const CONST_UPDATE As String = "UPDATE"
    Private Const CONST_UPDATE_NAV As String = "UPDATE_NAV"
    Private Const CONST_UPDATE_TC As String = "UPDATE_TC"
    Private Const CONST_DELETE As String = "DELETE"
    Private Const SP_CERTIFICADO_RELACION As String = "PRC_CertificadoRelaciones"
    Private Const CONST_ACCION_RELACION As String = "PUEDE_BORRAR"


    Private Const CONST_SELECT_ALL As String = "SELECT_ALL"
    Private CONST_SELECT_FILTRO As String = "SELECT_FILTRO"
    Private CONST_SELECT_TIPO_TRANSACCION As String = "SELECT_TIPO_TRANSACCION"
    Private CONST_SELECT_FIJACION_NAV As String = "SELECT_FIJACION_NAV"
    Private CONST_SELECT_FIJACION_TC As String = "SELECT_FIJACIONTC"
    Private Const CONST_SELECT_ONE As String = "SELECT_ONE"
    Private Const CONST_SELECT_RELACIONES As String = "SELECT_RELACIONES"
    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function ConsultarTodos(Fijacion As FijacionDTO) As List(Of FijacionDTO)
        Dim listaFijacion As List(Of FijacionDTO) = New List(Of FijacionDTO)
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)
        Dim ds As DataSet

        Try
            sp.AgregarParametro("Accion", CONST_SELECT_ALL, System.Data.SqlDbType.VarChar)

            ds = sp.ReturnDataSet()

            For Each dataRow As DataRow In ds.Tables(0).Rows
                listaFijacion.Add(fillFijacion(dataRow))
            Next

        Catch ex As Exception
            Throw
        End Try

        Return listaFijacion
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function Nemotecnico As List(Of FondoSerieDTO)
        Dim ListaSerie As List(Of FondoSerieDTO) = New List(Of FondoSerieDTO)
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CONSULTAS)
        Dim ds As DataSet

        Try
            sp.AgregarParametro("Accion", "Nemotecnico", System.Data.SqlDbType.VarChar)
            ds = sp.ReturnDataSet()


            For Each dataRow As DataRow In ds.Tables(0).Rows
                Dim Sus = New FondoSerieDTO
                With Sus
                    .Nemotecnico = dataRow("Nemotecnico").ToString().Trim()
                End With
                ListaSerie.Add(Sus)
            Next

        Catch ex As Exception
            Throw
        End Try

        Return ListaSerie
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function ConsultarFiltro(Fijacion As FijacionDTO, FechaNavDesde As Nullable(Of Date), FechaNavHasta As Nullable(Of Date),
                                    FechaTCDesde As Nullable(Of Date), FechaTCHasta As Nullable(Of Date),
                                    FechaPagoDesde As Nullable(Of Date), FechaPagoHasta As Nullable(Of Date)) As List(Of FijacionDTO)

        Dim listaFijacion As List(Of FijacionDTO) = New List(Of FijacionDTO)
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CONSULTAS)
        Dim ds As DataSet

        Try
            sp.AgregarParametro("Accion", CONST_SELECT_FILTRO, System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("TipoTransaccion", Fijacion.TipoTransaccion, System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("RutFondo", Fijacion.Rut, System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("Nemotecnico", Fijacion.Nemotecnico, System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("RES_Fijacion_NAV", Fijacion.FijacionNAV, System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("RES_Fijacion_TCObservado", Fijacion.FijacionTCObservado, System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("FechaNavDesde", FechaNavDesde, System.Data.SqlDbType.Date)
            sp.AgregarParametro("FechaNavHasta", FechaNavHasta, System.Data.SqlDbType.Date)
            sp.AgregarParametro("FechaTCDesde", FechaTCDesde, System.Data.SqlDbType.Date)
            sp.AgregarParametro("FechaTCHasta", FechaTCHasta, System.Data.SqlDbType.Date)

            'sp.AgregarParametro("FechaPagoDesde", FechaPagoDesde, System.Data.SqlDbType.Date)
            'sp.AgregarParametro("FechaPagoHasta", FechaPagoHasta, System.Data.SqlDbType.Date)

            ds = sp.ReturnDataSet()

            For Each dataRow As DataRow In ds.Tables(0).Rows
                listaFijacion.Add(fillFijacion(dataRow))
            Next

        Catch ex As Exception
            Throw
        End Try

        Return listaFijacion
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function ConsultarTipoTransacion() As List(Of FijacionDTO)
        Dim listaTipoTransaccion As List(Of FijacionDTO) = New List(Of FijacionDTO)
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)
        Dim ds As DataSet

        Try

            sp.AgregarParametro("Accion", CONST_SELECT_TIPO_TRANSACCION, System.Data.SqlDbType.VarChar)

            ds = sp.ReturnDataSet()

            For Each dataRow As DataRow In ds.Tables(0).Rows
                listaTipoTransaccion.Add(fillTipoTransaccion(dataRow))
            Next

        Catch ex As Exception
            Throw
        End Try

        Return listaTipoTransaccion
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function ConsultarFijacionNav() As List(Of FijacionDTO)
        Dim listaTipoTransaccion As List(Of FijacionDTO) = New List(Of FijacionDTO)
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)
        Dim ds As DataSet

        Try

            sp.AgregarParametro("Accion", CONST_SELECT_FIJACION_NAV, System.Data.SqlDbType.VarChar)

            ds = sp.ReturnDataSet()

            For Each dataRow As DataRow In ds.Tables(0).Rows
                listaTipoTransaccion.Add(fillFijacionNav(dataRow))
            Next

        Catch ex As Exception
            Throw
        End Try

        Return listaTipoTransaccion
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function CargarFiltroRutFondo(Rescate As FijacionDTO) As List(Of FijacionDTO)
        Dim Lista As List(Of FijacionDTO) = New List(Of FijacionDTO)
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)
        Dim ds As DataSet
        Dim FijacionNew As New FijacionDTO

        Try
            sp.AgregarParametro("Accion", "SELECT_FONDORUT", System.Data.SqlDbType.VarChar)

            ds = sp.ReturnDataSet()

            For Each dataRow As DataRow In ds.Tables(0).Rows
                FijacionNew = New FijacionDTO
                With FijacionNew
                    .Rut = dataRow("RUT").ToString().Trim()
                    .FnNombreCorto = dataRow("Nombre").ToString().Trim()
                End With
                Lista.Add(FijacionNew)
            Next


        Catch ex As Exception
            Throw
        End Try

        Return Lista
    End Function


    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function UpdateFijacion(TipoTransaccion As String, IdCanje As String, NavSaliente As Int32, NavEntrante As String)

        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_UPDATE)
        Dim ds As DataSet

        Try

            sp.AgregarParametro("Accion", CONST_UPDATE, System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("TipoTransaccion", TipoTransaccion, System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("Id", IdCanje, System.Data.SqlDbType.Int)
            sp.AgregarParametro("Fijacion_NAV", NavSaliente, System.Data.SqlDbType.Int)
            sp.AgregarParametro("Fijacion_TCObservado", NavEntrante, System.Data.SqlDbType.VarChar)

            ds = sp.ReturnDataSet()

        Catch ex As Exception
            Throw
        End Try

    End Function
    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function UpdateFijacionNav(ID As Integer, TipoTransaccion As String)

        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_UPDATE)
        Dim ds As DataSet

        Try
            sp.AgregarParametro("Accion", CONST_UPDATE_NAV, System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("TipoTransaccion", TipoTransaccion, System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("Id", ID, System.Data.SqlDbType.Int)

            ds = sp.ReturnDataSet()

        Catch ex As Exception
            Throw
        End Try

    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function UpdateFijacionTC(ID As Integer, TipoTransaccion As String)

        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_UPDATE)
        Dim ds As DataSet

        Try
            sp.AgregarParametro("Accion", CONST_UPDATE_TC, System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("TipoTransaccion", TipoTransaccion, System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("Id", ID, System.Data.SqlDbType.Int)

            ds = sp.ReturnDataSet()

        Catch ex As Exception
            Throw
        End Try

    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function GetListaFijacion(Fijacion As DTO.FijacionDTO) As List(Of DTO.FijacionDTO)
        Dim TCRetorno As FijacionDTO = New FijacionDTO
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)
        Dim ds As DataSet

        Try
            sp.AgregarParametro("Accion", CONST_SELECT_ONE, System.Data.SqlDbType.VarChar)

            FillParameters(Fijacion, sp)

            ds = sp.ReturnDataSet()
            If ds.Tables.Count > 0 AndAlso ds.Tables(0).Rows.Count > 0 Then
                TCRetorno = fillFijacion(ds.Tables(0).Rows(0))
            Else
                TCRetorno = Nothing
            End If

        Catch ex As Exception
            Throw
        End Try

    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function ConsultarFijacionTC() As List(Of FijacionDTO)
        Dim listaTipoTransaccion As List(Of FijacionDTO) = New List(Of FijacionDTO)
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)
        Dim ds As DataSet

        Try

            sp.AgregarParametro("Accion", CONST_SELECT_FIJACION_TC, System.Data.SqlDbType.VarChar)

            ds = sp.ReturnDataSet()

            For Each dataRow As DataRow In ds.Tables(0).Rows
                listaTipoTransaccion.Add(fillFijacionTC(dataRow))
            Next

        Catch ex As Exception
            Throw
        End Try

        Return listaTipoTransaccion
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function ConsultarPorFiltro(Fijacion As FijacionDTO, FechaDesdeSolicitud As Nullable(Of Date), FechaHastaSolicitud As Nullable(Of Date), FechaDesdeNAV As Nullable(Of Date), FechaHastaNAV As Nullable(Of Date), FechaDesdePago As Nullable(Of Date), FechaHastaPago As Nullable(Of Date)) As List(Of FijacionDTO)
        Dim listaFijacion As List(Of FijacionDTO) = New List(Of FijacionDTO)
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)
        Dim ds As DataSet

        Dim RES_Fecha_Nav As Nullable(Of Date)


        If Fijacion.FechaNav = "0001-01-01" Then
            RES_Fecha_Nav = Nothing
        Else
            RES_Fecha_Nav = Fijacion.FechaNav
        End If

        Try

            sp.AgregarParametro("Accion", CONST_SELECT_FILTRO, System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("FechaHastaSolicitud", FechaHastaSolicitud, System.Data.SqlDbType.Date)
            sp.AgregarParametro("FechaDesdeNAV", RES_Fecha_Nav, System.Data.SqlDbType.Date)
            sp.AgregarParametro("FechaHastaNAV", FechaHastaNAV, System.Data.SqlDbType.Date)
            sp.AgregarParametro("FechaHastaPago", FechaHastaPago, System.Data.SqlDbType.Date)


            ds = sp.ReturnDataSet()

            For Each dataRow As DataRow In ds.Tables(0).Rows
                listaFijacion.Add(fillFijacion(dataRow))
            Next

        Catch ex As Exception
            Throw
        End Try

        Return listaFijacion
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function GetRescateOne(Fijacion As FijacionDTO) As FijacionDTO
        Dim FijacionRetorno As FijacionDTO = New FijacionDTO
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)
        Dim ds As DataSet

        Try
            sp.AgregarParametro("Accion", CONST_SELECT_ONE, System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("RES_ID", Fijacion.ID, System.Data.SqlDbType.Decimal)


            ds = sp.ReturnDataSet()
            If ds.Tables.Count > 0 AndAlso ds.Tables(0).Rows.Count > 0 Then
                FijacionRetorno = fillFijacion(ds.Tables(0).Rows(0))
            Else
                FijacionRetorno = Nothing
            End If

        Catch ex As Exception
            Throw
        End Try

        Return FijacionRetorno
    End Function

    Private Shared Sub FillParameters(Fijacion As FijacionDTO, sp As DBSqlServer.SqlServer.StoredProcedure)
        sp.AgregarParametro(“Id”, Fijacion.ID, System.Data.SqlDbType.Int)
        sp.AgregarParametro("TipoTransaccion", Fijacion.TipoTransaccion, System.Data.SqlDbType.VarChar)
        sp.AgregarParametro(“Fijacion_NAV”, Fijacion.FijacionNAV, System.Data.SqlDbType.VarChar)
        sp.AgregarParametro(“Fijacion_TCObservado”, Fijacion.FijacionTCObservado, System.Data.SqlDbType.VarChar)

    End Sub

    Private Function fillFijacion(dataRow As DataRow) As FijacionDTO
        Dim Fijacion As New FijacionDTO
        With Fijacion
            .ID = dataRow("ID")
            .FechaNav = dataRow("Fecha_Nav").ToString().Trim()
            .FechaTCObs = dataRow("FechaTC").ToString().Trim()
            .fechaPago = dataRow("Fecha_Pago").ToString().Trim()
            .ApRut = dataRow("AP_RUT")
            .ApMultifondo = dataRow("AP_Multifondo").ToString().Trim()
            .Nemotecnico = dataRow("Nemotecnico").ToString().Trim()
            .TipoTransaccion = dataRow("Tipo_Transaccion").ToString().Trim()
            .RazonSocial = dataRow("Razon_Social").ToString().Trim()
            .Cuotas = dataRow("Cuotas").ToString().Trim()
            .Rut = dataRow("RUT").ToString().Trim()
            .FnNombreCorto = dataRow("FN_Nombre_Corto").ToString().Trim()
            .FsNombreCorto = dataRow("FS_Nombre_Corto").ToString().Trim()
            .Contrato = dataRow("Contrato").ToString().Trim()
            .Poderes = dataRow("Poderes").ToString().Trim()
            .Transito = dataRow("Transito").ToString().Trim()
            .Monto = dataRow("Monto").ToString().Trim()
            .MontoCLP = IIf(dataRow("Monto_CLP").ToString().Trim() = Nothing, Nothing, dataRow("Monto_CLP").ToString().Trim())
            .Estado = dataRow("Estado").ToString().Trim()
            .FsMoneda = dataRow("Moneda").ToString().Trim()
            .NavCLP = IIf(dataRow("Nav_CLP").ToString().Trim() = Nothing, Nothing, dataRow("Nav_CLP").ToString().Trim())
            .CnCuotasDisponibles = dataRow("Cuotas_Disponibles").ToString().Trim()
            .FijacionNAV = dataRow("Fijacion_NAV").ToString().Trim()
            .FijacionTCObservado = dataRow("Fijacion_TCObservado").ToString().Trim()
            .Observaciones = dataRow("Observaciones").ToString().Trim()
            .Estados = dataRow("Estados").ToString().Trim()
            .fechaPago = dataRow("fecha_pago").ToString().Trim()
        End With
        Return Fijacion
    End Function
    Private Function fillTipoTransaccion(dataRow As DataRow) As FijacionDTO
        Dim Fijacion As New FijacionDTO
        With Fijacion
            .TipoTransaccion = dataRow("Tipo_Transaccion").ToString().Trim()
        End With
        Return Fijacion
    End Function

    Private Function fillFijacionNav(dataRow As DataRow) As FijacionDTO
        Dim Fijacion As New FijacionDTO
        With Fijacion
            .FijacionNAV = dataRow("Fijacion_NAV").ToString().Trim()
        End With
        Return Fijacion
    End Function

    Private Function fillFijacionTC(dataRow As DataRow) As FijacionDTO
        Dim Fijacion As New FijacionDTO
        With Fijacion
            .FijacionTCObservado = dataRow("Fijacion_TCObservado").ToString().Trim()
        End With
        Return Fijacion
    End Function
End Class