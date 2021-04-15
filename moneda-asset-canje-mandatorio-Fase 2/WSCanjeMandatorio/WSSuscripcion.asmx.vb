Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel
Imports DTO
Imports System.Web.Script.Services

' Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente.
' <System.Web.Script.Services.ScriptService()> _
<System.Web.Services.WebService(Namespace:="http://tempuri.org/")> _
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)>
<ToolboxItem(False)>
Public Class WSSuscripcion
    Inherits System.Web.Services.WebService

    Private Const SP_CRUD As String = "PRC_SuscripcionCRUD"
    Private Const SP_FIJACION As String = "PRC_FijacionUpdate"
    Private Const CONST_INSERT As String = "INSERT"
    Private Const CONST_UPDATE As String = "UPDATE"
    Private Const CONST_DELETE As String = "DELETE"
    Private Const CONST_SELECT_ALL As String = "SELECT_ALL"
    Private Const CONST_SELECT_ONE As String = "SELECT_ONE"
    Private Const CONST_SELECT_FILTRO As String = "SELECT_FILTRO"
    Private Const CONST_SELECT_ONE_FIJACION As String = "SELECT_ONE_FIJACION"
    Private Const CONST_SELECT_OBTENER_ACTUAL As String = "SELECT_OBTENER_ACTUAL"
    Private Const CONST_SELECT_CUOTAS_FONDO As String = "SELECT_CUOTAS_FONDO"
    Private Const CONST_SELECT_SUSCRIPCION_APORTANTE As String = "SELECT_APORTANTE_SUSCRIPCION"
    Private Const CONST_SELECT_SUSCRIPCION_FONDO As String = "SELECT_FONDO_SUSCRIPCION"
    Private Const CONST_SELECT_SUSCRIPCION_SERIE As String = "SELECT_SERIE_SUSCRIPCION"

    Private Const SP_CONSULTAS As String = "PRC_SuscripcionConsultas"

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function GetNemotecnicoPorRut(Serie As DTO.SuscripcionDTO) As List(Of DTO.SuscripcionDTO)
        Dim Series As List(Of SuscripcionDTO) = New List(Of SuscripcionDTO)
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)
        Dim ds As DataSet

        Try
            sp.AgregarParametro("Accion", "SELECT_NEMOTECNICO", System.Data.SqlDbType.VarChar)
            FillParameters(Serie, sp)
            ds = sp.ReturnDataSet()
            For Each dataRow As DataRow In ds.Tables(0).Rows
                Series.Add(fillObject(dataRow))
            Next

        Catch ex As Exception
            Throw
        End Try

        Return Series
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function GetSuscripcionTransito(Suscripcion As DTO.SuscripcionDTO)
        Dim SuscripcionRetorno As SuscripcionDTO = New SuscripcionDTO
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)
        Dim ds As DataSet

        Try
            sp.AgregarParametro("Accion", "SELECT_SUSCRIPCIONES_TRANSITO2", System.Data.SqlDbType.VarChar)
            FillParameters(Suscripcion, sp)


            ds = sp.ReturnDataSet()
            If ds.Tables.Count > 0 AndAlso ds.Tables(0).Rows.Count > 0 Then
                Dim dataRow As DataRow = ds.Tables(0).Rows(0)
                Try
                    SuscripcionRetorno.CuotasASuscribir = dataRow("SC_Cuotas_a_Suscribir")
                Catch ex As Exception
                    SuscripcionRetorno.CuotasASuscribir = 0
                End Try
            End If
        Catch ex As Exception
            Throw
        End Try

        Return SuscripcionRetorno
    End Function
    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function GetSuscripcionTransito2(Suscripcion As DTO.SuscripcionDTO)
        Dim SuscripcionRetorno As SuscripcionDTO = New SuscripcionDTO
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)
        Dim ds As DataSet

        Try
            sp.AgregarParametro("Accion", "SELECT_SUSCRIPCIONES_TRANSITO2", System.Data.SqlDbType.VarChar)
            FillParameters(Suscripcion, sp)


            ds = sp.ReturnDataSet()
            If ds.Tables.Count > 0 AndAlso ds.Tables(0).Rows.Count > 0 Then
                Dim dataRow As DataRow = ds.Tables(0).Rows(0)
                Try
                    SuscripcionRetorno.CuotasASuscribir = dataRow("SC_Cuotas_a_Suscribir")
                Catch ex As Exception
                    SuscripcionRetorno.CuotasASuscribir = 0
                End Try
            End If
        Catch ex As Exception
            Throw
        End Try

        Return SuscripcionRetorno
    End Function
    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function GetUltimaSuscripcion(Suscripcion As DTO.SuscripcionDTO)
        Dim SuscripcionRetorno As SuscripcionDTO = New SuscripcionDTO
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)
        Dim ds As DataSet
        Try
            sp.AgregarParametro("Accion", "SELECT_ULTIMO", System.Data.SqlDbType.VarChar)
            FillParameters(Suscripcion, sp)
            ds = sp.ReturnDataSet()
            If ds.Tables.Count > 0 AndAlso ds.Tables(0).Rows.Count > 0 Then
                Dim dataRow As DataRow = ds.Tables(0).Rows(0)
                SuscripcionRetorno.IdSuscripcion = dataRow("SC_ID")
            End If
        Catch ex As Exception
            Throw
        End Try
        Return SuscripcionRetorno
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function ObtenerActual(suscripcion As SuscripcionDTO) As SuscripcionDTO
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)
        Dim ds As DataSet
        Try
            sp.AgregarParametro("Accion", CONST_SELECT_OBTENER_ACTUAL, System.Data.SqlDbType.VarChar)

            FillParameters(suscripcion, sp)
            ds = sp.ReturnDataSet()

            For Each dataRow As DataRow In ds.Tables(0).Rows
                suscripcion.ScActual = IIf(dataRow("Sc_Actual").ToString().Trim() = "" Or dataRow("Sc_Actual").ToString().Trim() = Nothing, 0, dataRow("Sc_Actual").ToString().Trim())
            Next

        Catch ex As Exception
            Throw
        End Try

        Return suscripcion
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function ObtenerCuotasFondo(suscripcion As SuscripcionDTO) As SuscripcionDTO
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)
        Dim ds As DataSet
        Try
            sp.AgregarParametro("Accion", CONST_SELECT_CUOTAS_FONDO, System.Data.SqlDbType.VarChar)

            FillParameters(suscripcion, sp)
            ds = sp.ReturnDataSet()

            For Each dataRow As DataRow In ds.Tables(0).Rows
                suscripcion.CuotasEmitidas = Decimal.Parse(dataRow("FN_Cuotas_Emitidas"))
                suscripcion.FnAcumulada = Decimal.Parse(dataRow("FN_Acumulado"))
            Next

        Catch ex As Exception
            Throw
        End Try

        Return suscripcion
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function GetListaSuscripcion(Suscripcion As SuscripcionDTO) As List(Of SuscripcionDTO)
        Dim listaSuscripcion As List(Of SuscripcionDTO) = New List(Of SuscripcionDTO)
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)
        Dim ds As DataSet

        Try
            sp.AgregarParametro("Accion", CONST_SELECT_ALL, System.Data.SqlDbType.VarChar)

            FillParameters(Suscripcion, sp)
            ds = sp.ReturnDataSet()

            For Each dataRow As DataRow In ds.Tables(0).Rows
                listaSuscripcion.Add(fillObject(dataRow))
            Next

        Catch ex As Exception
            Throw
        End Try

        Return listaSuscripcion
    End Function
    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function GetRelaciones(Suscripcion As DTO.SuscripcionDTO)
        Dim SuscripcionRetorno As SuscripcionDTO = New SuscripcionDTO
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)
        Dim ds As DataSet

        Try
            sp.AgregarParametro("Accion", "RELACIONES", System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("ScId", Suscripcion.IdSuscripcion, System.Data.SqlDbType.Int)

            ds = sp.ReturnDataSet()
            If ds.Tables.Count > 0 AndAlso ds.Tables(0).Rows.Count > 0 Then
                Dim dataRow As DataRow = ds.Tables(0).Rows(0)
                Try
                    SuscripcionRetorno.CountAP = dataRow("CountAP")
                    SuscripcionRetorno.CountFS = dataRow("CountFS")
                    SuscripcionRetorno.CountFN = dataRow("CountFN")
                Catch ex As Exception
                    SuscripcionRetorno.CountAP = 0
                    SuscripcionRetorno.CountFS = 0
                    SuscripcionRetorno.CountFN = 0
                End Try
            End If
        Catch ex As Exception
            Throw
        End Try

        Return SuscripcionRetorno
    End Function
    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function GetFijacionId(Id As Int32) As SuscripcionDTO
        Dim suscripcionRetorno As SuscripcionDTO = New SuscripcionDTO
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_FIJACION)
        Dim ds As DataSet

        Try
            sp.AgregarParametro("Accion", CONST_SELECT_ONE_FIJACION, System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("Id", Id, System.Data.SqlDbType.Int)
            sp.AgregarParametro("TipoTransaccion", "Suscripcion", System.Data.SqlDbType.VarChar)

            ds = sp.ReturnDataSet()
            If ds.Tables.Count > 0 AndAlso ds.Tables(0).Rows.Count > 0 Then
                suscripcionRetorno = fillObject(ds.Tables(0).Rows(0))
            Else
                suscripcionRetorno = Nothing
            End If

        Catch ex As Exception
            Throw
        End Try
        Return suscripcionRetorno
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function BuscarFiltro(Suscripcion As SuscripcionDTO, FechaIntencionHasta As Nullable(Of Date), FechaNAVHasta As Nullable(Of Date),
    FechaSuscripcionHasta As Nullable(Of Date)) As List(Of SuscripcionDTO) 'FALTAN LOS PARAMETROS DE LAS OTRAS FECHAS
        Dim listaSuscripcion As List(Of SuscripcionDTO) = New List(Of SuscripcionDTO)
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)
        Dim ds As DataSet

        Try
            sp.AgregarParametro("Accion", CONST_SELECT_FILTRO, System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("FechaIntencionDesde", Suscripcion.FechaIntencion, System.Data.SqlDbType.Date)
            sp.AgregarParametro("fechaIntencionHasta", FechaIntencionHasta, System.Data.SqlDbType.Date)
            sp.AgregarParametro("FechaNAVDesde", Suscripcion.FechaNAV, System.Data.SqlDbType.Date)
            sp.AgregarParametro("FechaNAVHasta", FechaNAVHasta, System.Data.SqlDbType.Date)
            sp.AgregarParametro("FechaSuscripcionDesde", Suscripcion.FechaSuscripcion, System.Data.SqlDbType.Date)
            sp.AgregarParametro("FechaSuscripcionHasta", FechaSuscripcionHasta, System.Data.SqlDbType.Date)

            FillParameters(Suscripcion, sp)

            ds = sp.ReturnDataSet()
            For Each dataRow As DataRow In ds.Tables(0).Rows
                listaSuscripcion.Add(fillObject(dataRow))
            Next

        Catch ex As Exception
            Throw
        End Try

        Return listaSuscripcion
    End Function


    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function ConsultarPorCodigo(Suscripcion As SuscripcionDTO) As List(Of SuscripcionDTO)
        Dim ListaSc As List(Of SuscripcionDTO) = New List(Of SuscripcionDTO)
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)
        Dim ds As DataSet
        Dim SuscripcionNew As New SuscripcionDTO

        Try
            ' ---TODO Data prueba---
            sp.AgregarParametro("Accion", "SELECT_ONE", System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("ScId", Suscripcion.IdSuscripcion, System.Data.SqlDbType.Int)

            ds = sp.ReturnDataSet()

            For Each dataRow As DataRow In ds.Tables(0).Rows
                SuscripcionNew = New SuscripcionDTO
                With SuscripcionNew
                    .IdSuscripcion = dataRow("SC_ID").ToString().Trim()
                End With
                ListaSc.Add(SuscripcionNew)
            Next
        Catch ex As Exception
            Throw
        End Try
        Return ListaSc
    End Function
    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function ConsultarPorRazonSocial(Suscripcion As SuscripcionDTO) As List(Of SuscripcionDTO)
        Dim ListaAportantes As List(Of SuscripcionDTO) = New List(Of SuscripcionDTO)
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)
        Dim ds As DataSet
        Dim aportanteNew As New SuscripcionDTO
        Try
            sp.AgregarParametro("Accion", "SELECT_NOMBRE_APORTANTE", System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("ApMultifondo", Suscripcion.Multifondo, System.Data.SqlDbType.VarChar)
            ds = sp.ReturnDataSet()
            For Each dataRow As DataRow In ds.Tables(0).Rows
                aportanteNew = New SuscripcionDTO
                With aportanteNew
                    .RazonSocial = dataRow("AP_Razon_Social").ToString().Trim()
                End With
                ListaAportantes.Add(aportanteNew)
            Next
        Catch ex As Exception
            Throw
        End Try
        Return ListaAportantes
    End Function
    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function ConsultarPorMultifondo(Suscripcion As SuscripcionDTO) As List(Of SuscripcionDTO)
        Dim ListaAportantes As List(Of SuscripcionDTO) = New List(Of SuscripcionDTO)
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)
        Dim ds As DataSet
        Dim aportanteNew As New SuscripcionDTO
        Try
            sp.AgregarParametro("Accion", "SELECT_MULTIFONDO_APORTANTE", System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("ApRazonSocial", Suscripcion.RazonSocial, System.Data.SqlDbType.VarChar)
            ds = sp.ReturnDataSet()
            For Each dataRow As DataRow In ds.Tables(0).Rows
                aportanteNew = New SuscripcionDTO
                With aportanteNew
                    .Multifondo = dataRow("AP_Multifondo").ToString().Trim()
                End With
                ListaAportantes.Add(aportanteNew)
            Next
        Catch ex As Exception
            Throw
        End Try
        Return ListaAportantes
    End Function
    Private Function SuscripcionTraer(Suscripcion As SuscripcionDTO, accion As String) As SuscripcionDTO
        Dim tcRet As SuscripcionDTO = New SuscripcionDTO
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)
        Dim ds As DataSet

        Try
            sp.AgregarParametro("Accion", accion, System.Data.SqlDbType.VarChar)

            FillParameters(Suscripcion, sp)

            ds = sp.ReturnDataSet()

            If ds.Tables.Count > 0 AndAlso ds.Tables(0).Rows.Count > 0 Then
                tcRet = fillObject(ds.Tables(0).Rows(0))
            End If

        Catch ex As Exception
            Throw
        End Try

        Return tcRet
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function SuscripcionModificar(Suscripcion As SuscripcionDTO) As Boolean
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)

        Try

            sp.AgregarParametro("Accion", CONST_UPDATE, System.Data.SqlDbType.VarChar)

            FillParameters(Suscripcion, sp)

            sp.ReturnDataSet()

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function SuscripcionEliminar(Suscripcion As SuscripcionDTO) As Boolean
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)
        Try

            sp.AgregarParametro("Accion", CONST_DELETE, System.Data.SqlDbType.VarChar)
            FillParameters(Suscripcion, sp)
            sp.ReturnDataSet()

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function SuscripcionIngresar(Suscripcion As SuscripcionDTO)
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)
        Dim ds As DataSet
        Dim SuscripcionRetorno As SuscripcionDTO = New SuscripcionDTO
        Try
            sp.AgregarParametro("Accion", CONST_INSERT, System.Data.SqlDbType.VarChar)

            Suscripcion.Estado = DTO.Estados.CONST_ACTIVO

            FillParameters(Suscripcion, sp)

            ds = sp.ReturnDataSet()
            If ds.Tables.Count > 0 AndAlso ds.Tables(0).Rows.Count > 0 Then
                Dim dataRow As DataRow = ds.Tables(0).Rows(0)
                Try
                    SuscripcionRetorno.IdSuscripcion = dataRow("SC_ID")
                Catch ex As Exception
                    SuscripcionRetorno.IdSuscripcion = 0
                End Try
            End If

            Return SuscripcionRetorno

        Catch ex As Exception
            Return False
        End Try
    End Function


    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function GetSuscripcion(Suscripcion As SuscripcionDTO) As SuscripcionDTO
        Dim TCRetorno As SuscripcionDTO = New SuscripcionDTO
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)
        Dim ds As DataSet

        Try
            sp.AgregarParametro("Accion", CONST_SELECT_ONE, System.Data.SqlDbType.VarChar)

            FillParameters(Suscripcion, sp)

            ds = sp.ReturnDataSet()
            If ds.Tables.Count > 0 AndAlso ds.Tables(0).Rows.Count > 0 Then
                TCRetorno = fillObject(ds.Tables(0).Rows(0))
            Else
                TCRetorno = Nothing
            End If

        Catch ex As Exception
            Throw
        End Try

        Return TCRetorno
    End Function

    Private Shared Sub FillParameters(Suscripcion As SuscripcionDTO, sp As DBSqlServer.SqlServer.StoredProcedure)
        sp.AgregarParametro("ScId", Suscripcion.IdSuscripcion, System.Data.SqlDbType.Int)
        sp.AgregarParametro("ScFechaIntencion", Suscripcion.FechaIntencion, System.Data.SqlDbType.Date)
        sp.AgregarParametro("ApRut", Suscripcion.RutAportante, System.Data.SqlDbType.VarChar)
        sp.AgregarParametro("ApRazonSocial", Suscripcion.RazonSocial, System.Data.SqlDbType.VarChar)
        sp.AgregarParametro("ApMultifondo", IIf(Suscripcion.Multifondo = vbNullChar, "", Suscripcion.Multifondo), System.Data.SqlDbType.Char)
        sp.AgregarParametro("FnRut", Suscripcion.RutFondo, System.Data.SqlDbType.VarChar)
        sp.AgregarParametro("FnNombreCorto", Suscripcion.FondoNombreCorto, System.Data.SqlDbType.VarChar)
        sp.AgregarParametro("TcObservado", Suscripcion.TcObservado, System.Data.SqlDbType.VarChar)
        sp.AgregarParametro("FsNemotecnico", Suscripcion.Nemotecnico, System.Data.SqlDbType.VarChar)
        sp.AgregarParametro("FsNombreCorto", Suscripcion.SerieNombreCorto, System.Data.SqlDbType.VarChar)
        sp.AgregarParametro("FsMoneda", Suscripcion.MonedaSerie, System.Data.SqlDbType.VarChar)
        sp.AgregarParametro("ScCuotasaSuScribir", Suscripcion.CuotasASuscribir, System.Data.SqlDbType.Decimal)
        sp.AgregarParametro("ScMonedaPago", Suscripcion.Moneda_Pago, System.Data.SqlDbType.VarChar)
        sp.AgregarParametro("ScFechaNAV", Suscripcion.FechaNAV, System.Data.SqlDbType.Date)
        sp.AgregarParametro("ScFechaSuscripcion", Suscripcion.FechaSuscripcion, System.Data.SqlDbType.Date)
        sp.AgregarParametro("ScFechaTC", Suscripcion.FechaTC, System.Data.SqlDbType.Date)
        sp.AgregarParametro("ScNAV", Suscripcion.NavFormat, System.Data.SqlDbType.Decimal)
        sp.AgregarParametro("ScMonto", Suscripcion.Monto, System.Data.SqlDbType.Decimal)
        sp.AgregarParametro("ScNAVCLP", Suscripcion.NAVCLP, System.Data.SqlDbType.Decimal)
        sp.AgregarParametro("ScMontoCLP", Suscripcion.MontoCLP, System.Data.SqlDbType.Decimal)
        sp.AgregarParametro("ScContratoFondo", Suscripcion.ContratoFondo, System.Data.SqlDbType.VarChar)
        sp.AgregarParametro("ScRevisionPoderes", Suscripcion.RevisionPoderes, System.Data.SqlDbType.VarChar)
        sp.AgregarParametro("ScObservaciones", Suscripcion.Observaciones, System.Data.SqlDbType.VarChar)
        sp.AgregarParametro("ScFechaDCV", Suscripcion.FechaDCV, System.Data.SqlDbType.Date)
        sp.AgregarParametro("ScCuotasDCV", Suscripcion.CuotasDCV, System.Data.SqlDbType.Int)
        sp.AgregarParametro("RSRescatesTransito", Suscripcion.RescatesTransitos, System.Data.SqlDbType.Int)
        sp.AgregarParametro("ScSuscripcionesTransito", Suscripcion.SuscripcionesTransito, System.Data.SqlDbType.Int)
        sp.AgregarParametro("CJCanjesTransito", Suscripcion.CanjesTransito, System.Data.SqlDbType.Int)
        sp.AgregarParametro("ScCuotasDisponibles", Suscripcion.CuotasDisponibles, System.Data.SqlDbType.Int)
        sp.AgregarParametro("ScFijacionNav", Suscripcion.FijacionNAV, System.Data.SqlDbType.VarChar)
        sp.AgregarParametro("ScFijacionTC", Suscripcion.FijacionTC, System.Data.SqlDbType.VarChar)
        sp.AgregarParametro("EstadoSuscripcion", Suscripcion.EstadoSuscripcion, System.Data.SqlDbType.VarChar)

        sp.AgregarParametro("FnCuotasEmitidas", Suscripcion.CuotasEmitidas, System.Data.SqlDbType.Decimal)
        sp.AgregarParametro("FnAcumulada", Suscripcion.FnAcumulada, System.Data.SqlDbType.Decimal)
        sp.AgregarParametro("ScActual", Suscripcion.ScActual, System.Data.SqlDbType.Decimal)
        sp.AgregarParametro("ScUtilizado", Suscripcion.ScUtilizado, System.Data.SqlDbType.Decimal)
        sp.AgregarParametro("ScDisponibles", Suscripcion.ScDisponibles, System.Data.SqlDbType.Decimal)

        sp.AgregarParametro("ScUsuarioIngreso", Suscripcion.ScUsuarioIngreso, System.Data.SqlDbType.VarChar)
        sp.AgregarParametro("ScUsuarioModificacion", Suscripcion.ScUsuarioModificacion, System.Data.SqlDbType.VarChar)
        'sp.AgregarParametro("ScFechaModificacion", Suscripcion.ScFechaModificacion, System.Data.SqlDbType.DateTime)

        sp.AgregarParametro("ScEstadoIntencion", Suscripcion.EstadoIntencion, System.Data.SqlDbType.VarChar)
    End Sub

    Private Function fillObject(dataRow As DataRow) As SuscripcionDTO
        Dim Suscripcion As New SuscripcionDTO

        With Suscripcion
            .IdSuscripcion = dataRow("SC_ID").ToString().Trim()
            .TipoTransaccion = dataRow("Sc_Tipo_Transaccion").ToString().Trim()
            .FechaIntencion = dataRow("Sc_Fecha_intencion").ToString().Trim()
            .RutAportante = dataRow("Ap_Rut").ToString().Trim()
            .RazonSocial = dataRow("AP_Razon_Social").ToString().Trim()
            .Multifondo = dataRow("Ap_Multifondo").ToString().Trim()
            .RutFondo = dataRow("Fn_Rut").ToString().Trim()
            .FondoNombreCorto = dataRow("FN_Nombre_Corto").ToString().Trim()
            .Nemotecnico = dataRow("Fs_Nemotecnico").ToString().Trim()
            .SerieNombreCorto = dataRow("FS_Nombre_Corto").ToString().Trim()
            .MonedaSerie = dataRow("FS_Moneda").ToString().Trim()
            .CuotasASuscribir = dataRow("Sc_Cuotas_a_Suscribir").ToString().Trim()
            .Moneda_Pago = dataRow("Sc_Moneda_Pago").ToString().Trim()
            .CuotasDCV = dataRow("Sc_Cuotas_DCV").ToString().Trim()
            .FechaNAV = dataRow("Sc_Fecha_NAV").ToString().Trim()
            .FechaSuscripcion = dataRow("Sc_Fecha_Suscripcion").ToString().Trim()
            .FechaTC = dataRow("Sc_Fecha_TC").ToString().Trim()
            .NAV = dataRow("Sc_NAV").ToString().Trim()
            .Monto = dataRow("Sc_Monto").ToString().Trim()
            .NAVCLP = IIf(dataRow("Sc_NAV_CLP").ToString().Trim() = Nothing, Nothing, dataRow("Sc_NAV_CLP").ToString().Trim())
            .MontoCLP = IIf(dataRow("Sc_Monto_CLP").ToString().Trim() = Nothing, Nothing, dataRow("Sc_Monto_CLP").ToString().Trim())
            .ContratoFondo = dataRow("Sc_Contrato_Fondo").ToString().Trim()
            .RevisionPoderes = dataRow("Sc_Revision_Poderes").ToString().Trim()
            .Estado = dataRow("Sc_Estado").ToString().Trim()
            .Observaciones = dataRow("Sc_Observaciones").ToString().Trim()
            .FechaDCV = dataRow("Sc_Fecha_DCV").ToString().Trim()
            .RescatesTransitos = dataRow("RS_Rescates_Transito").ToString().Trim()
            .SuscripcionesTransito = dataRow("Sc_Suscripciones_Transito").ToString().Trim()
            .CanjesTransito = dataRow("CJ_Canjes_Transito").ToString().Trim()
            .CuotasDisponibles = dataRow("Sc_Cuotas_Disponibles").ToString().Trim()
            .FijacionNAV = dataRow("Sc_Fijacion_Nav").ToString().Trim()

            If (IsDBNull(dataRow("Sc_TC_Observado"))) Then
                .TcObservado = "0"
            Else
                .TcObservado = dataRow("Sc_TC_Observado").ToString().Trim()
            End If

            .FijacionTC = dataRow("Sc_Fijacion_TC").ToString().Trim()
            .EstadoSuscripcion = dataRow("EstadoSuscripcion").ToString().Trim()

            .CuotasEmitidas = Integer.TryParse(dataRow("Fn_Cuotas_Emitidas").ToString().Trim(), 0)
            .FnAcumulada = Decimal.TryParse(dataRow("Fn_Acumulada").ToString().Trim(), 0)
            .ScActual = Decimal.TryParse(dataRow("Sc_Actual").ToString().Trim(), 0)
            .ScUtilizado = Decimal.TryParse(dataRow("Sc_Utilizado").ToString().Trim(), 0)
            .ScDisponibles = Decimal.TryParse(dataRow("Sc_Disponibles").ToString().Trim(), 0)
            .ScUsuarioIngreso = dataRow("Sc_Usuario_Ingreso").ToString()
            .ScFechaIngreso = dataRow("Sc_Fecha_Ingreso").ToString().Trim()
            .ScUsuarioModificacion = dataRow("Sc_Usuario_Modificacion").ToString()
            .ScFechaModificacion = dataRow("Sc_Fecha_Modificacion").ToString().Trim()

            .EstadoIntencion = dataRow("Sc_EstadoIntencion").ToString().Trim()

            .HoraTransaccion = dataRow("HoraTransaccion").ToString().Trim()
        End With
        Return Suscripcion
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function ConsultarTransito(suscripcion As SuscripcionDTO) As List(Of SuscripcionDTO)
        Dim Lista As List(Of SuscripcionDTO) = New List(Of SuscripcionDTO)
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CONSULTAS)
        Dim ds As DataSet
        Try
            sp.AgregarParametro("Accion", "SELECT_BY", System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("ApRut", suscripcion.RutAportante, System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("Nemotecnico", suscripcion.Nemotecnico, System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("FechaSuscripcion", suscripcion.FechaSuscripcion, System.Data.SqlDbType.Date)

            ds = sp.ReturnDataSet()
            For Each dataRow As DataRow In ds.Tables(0).Rows
                Lista.Add(fillObject(dataRow))
            Next
        Catch ex As Exception
            Throw
        End Try

        Return Lista
    End Function
    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function ConsultarActual(Suscripcion As SuscripcionDTO) As SuscripcionDTO
        Dim SuscripcionRetorno As SuscripcionDTO = New SuscripcionDTO
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)
        Dim ds As DataSet

        Try
            sp.AgregarParametro("Accion", "SELECT_ACTUALES", System.Data.SqlDbType.VarChar)
            FillParameters(Suscripcion, sp)


            ds = sp.ReturnDataSet()
            If ds.Tables.Count > 0 AndAlso ds.Tables(0).Rows.Count > 0 Then
                Dim dataRow As DataRow = ds.Tables(0).Rows(0)
                SuscripcionRetorno.ScActual = dataRow("Cuotas")
            End If

        Catch ex As Exception
            Throw
        End Try

        Return SuscripcionRetorno
    End Function
    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function GetAportanteSuscripcion(Suscripcion As SuscripcionDTO) As List(Of SuscripcionDTO)
        Dim listaSuscripcion As List(Of SuscripcionDTO) = New List(Of SuscripcionDTO)
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)
        Dim ds As DataSet

        Try
            sp.AgregarParametro("Accion", CONST_SELECT_SUSCRIPCION_APORTANTE, System.Data.SqlDbType.VarChar)

            FillParameters(Suscripcion, sp)
            ds = sp.ReturnDataSet()

            For Each dataRow As DataRow In ds.Tables(0).Rows
                Dim Sus = New SuscripcionDTO
                With Sus
                    .RazonSocial = dataRow("AP_Razon_Social").ToString().Trim()
                    .RutAportante = dataRow("AP_Rut").ToString().Trim()
                End With
                listaSuscripcion.Add(Sus)
            Next

        Catch ex As Exception
            Throw
        End Try

        Return listaSuscripcion
    End Function


    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function GetFondoSuscripcion(Suscripcion As SuscripcionDTO) As List(Of SuscripcionDTO)
        Dim listaSuscripcion As List(Of SuscripcionDTO) = New List(Of SuscripcionDTO)
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)
        Dim ds As DataSet

        Try
            sp.AgregarParametro("Accion", CONST_SELECT_SUSCRIPCION_FONDO, System.Data.SqlDbType.VarChar)

            FillParameters(Suscripcion, sp)
            ds = sp.ReturnDataSet()

            For Each dataRow As DataRow In ds.Tables(0).Rows
                Dim Sus = New SuscripcionDTO
                With Sus
                    .RutFondo = dataRow("FN_Rut").ToString().Trim()
                    .FondoNombreCorto = dataRow("FN_Nombre_Corto").ToString().Trim()
                End With
                listaSuscripcion.Add(Sus)
            Next

        Catch ex As Exception
            Throw
        End Try

        Return listaSuscripcion
    End Function
    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function GetSerieSuscripcion(Suscripcion As SuscripcionDTO) As List(Of SuscripcionDTO)
        Dim listaSuscripcion As List(Of SuscripcionDTO) = New List(Of SuscripcionDTO)
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)
        Dim ds As DataSet

        Try
            sp.AgregarParametro("Accion", CONST_SELECT_SUSCRIPCION_SERIE, System.Data.SqlDbType.VarChar)

            FillParameters(Suscripcion, sp)
            ds = sp.ReturnDataSet()

            For Each dataRow As DataRow In ds.Tables(0).Rows
                Dim Sus = New SuscripcionDTO
                With Sus
                    .Nemotecnico = dataRow("FS_Nemotecnico").ToString().Trim()
                End With
                listaSuscripcion.Add(Sus)
            Next

        Catch ex As Exception
            Throw
        End Try

        Return listaSuscripcion
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function GetAportanteDistinct(Suscripcion As SuscripcionDTO) As List(Of SuscripcionDTO)
        Dim listaSuscripcion As List(Of SuscripcionDTO) = New List(Of SuscripcionDTO)
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)
        Dim ds As DataSet

        Try
            sp.AgregarParametro("Accion", "SELECT_APORTANTE_DISTINCT", System.Data.SqlDbType.VarChar)
            ds = sp.ReturnDataSet()

            For Each dataRow As DataRow In ds.Tables(0).Rows
                Dim Sus = New SuscripcionDTO
                With Sus
                    .RutAportante = dataRow("AP_Rut").ToString().Trim()
                End With
                listaSuscripcion.Add(Sus)
            Next

        Catch ex As Exception
            Throw
        End Try

        Return listaSuscripcion
    End Function
    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function RecalculoFijacionNAV(Suscripcion As SuscripcionDTO) As Boolean
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)

        Try
            ' ---TODO Data prueba---
            sp.AgregarParametro("Accion", "RECALCULO_FIJACION_NAV", System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("ScId", Suscripcion.IdSuscripcion, System.Data.SqlDbType.Decimal)
            sp.AgregarParametro("ScNAV", Suscripcion.NAV, System.Data.SqlDbType.Decimal)
            sp.AgregarParametro("ScNAVCLP", Suscripcion.NAVCLP, System.Data.SqlDbType.Decimal)
            sp.AgregarParametro("ScMonto", Suscripcion.Monto, System.Data.SqlDbType.Decimal)
            sp.AgregarParametro("ScMontoCLP", Suscripcion.MontoCLP, System.Data.SqlDbType.Decimal)

            sp.ReturnDataSet()

            Return (sp.FilasAfectas > 0)
        Catch ex As Exception
            Return False
        End Try
    End Function
    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function RecalculoFijacionTC(Suscripcion As SuscripcionDTO) As Boolean
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)

        Try
            ' ---TODO Data prueba---
            sp.AgregarParametro("Accion", "RECALCULO_FIJACION_TC", System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("ScId", Suscripcion.IdSuscripcion, System.Data.SqlDbType.Decimal)
            sp.AgregarParametro("ScNAV", Suscripcion.NAV, System.Data.SqlDbType.Decimal)
            sp.AgregarParametro("ScNAVCLP", Suscripcion.NAVCLP, System.Data.SqlDbType.Decimal)
            sp.AgregarParametro("ScMonto", Suscripcion.Monto, System.Data.SqlDbType.Decimal)
            sp.AgregarParametro("ScMontoCLP", Suscripcion.MontoCLP, System.Data.SqlDbType.Decimal)
            sp.AgregarParametro("TcObservado", Suscripcion.TcObservado, System.Data.SqlDbType.Decimal)

            sp.ReturnDataSet()

            Return (sp.FilasAfectas > 0)
        Catch ex As Exception
            Return False
        End Try
    End Function
End Class