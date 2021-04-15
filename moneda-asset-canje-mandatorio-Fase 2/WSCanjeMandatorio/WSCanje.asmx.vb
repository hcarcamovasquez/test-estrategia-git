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
Public Class WSCanje
    Inherits System.Web.Services.WebService

    Private Const SP_CRUD As String = "PRC_CanjeCRUD"
    Private Const SP_FIJACION As String = "PRC_FijacionUpdate"
    Private Const SP_CONSULTA_RESCATE As String = "PRC_RescateCRUD"
    Private Const SP_CONSULTA_SUSCRIPCION As String = "PRC_SuscripcionCRUD"
    Private Const CONST_INSERT As String = "INSERT"
    Private Const CONST_UPDATE As String = "UPDATE"
    Private Const CONST_DELETE As String = "DELETE"
    Private Const CONST_SELECT_ALL As String = "SELECT_ALL"
    Private Const CONST_SELECT_ONE As String = "SELECT_ONE"
    Private Const CONST_SELECT_ONE_FIJACION As String = "SELECT_ONE_FIJACION"
    Private Const CONST_SELECT_FILTRO As String = "CONSULTA_POR_FILTRO"

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function CanjeFiltros(canje As CanjeDTO, fechaHastaSolicitud As Nullable(Of Date), fechaHastaNav As Nullable(Of Date), fechaHastaCanje As Nullable(Of Date)) As List(Of CanjeDTO)
        Dim listaCanje As List(Of CanjeDTO) = New List(Of CanjeDTO)
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)
        Dim ds As DataSet

        Try
            sp.AgregarParametro("Accion", CONST_SELECT_FILTRO, System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("ApRazonSocial", canje.NombreAportante, System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("FnNombreCorto", canje.NombreFondo, System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("FsNemotecnicoSaliente", canje.NemotecnicoSaliente, System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("CnEstadoCanje", canje.EstadoCanje, System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("fechaDesdeSolicitud", canje.FechaSolicitud, System.Data.SqlDbType.Date)
            sp.AgregarParametro("fechaHastaSolicitud", fechaHastaSolicitud, System.Data.SqlDbType.Date)
            sp.AgregarParametro("fechaDesdeNav", canje.FechaNavSaliente, System.Data.SqlDbType.Date)
            sp.AgregarParametro("fechaHastaNav", fechaHastaNav, System.Data.SqlDbType.Date)

            If canje.FechaCanje = "" Then
                sp.AgregarParametro("fechaDesdeCanje", Nothing, System.Data.SqlDbType.Date)
            Else
                sp.AgregarParametro("fechaDesdeCanje", canje.FechaCanjeDate, System.Data.SqlDbType.Date)
            End If

            sp.AgregarParametro("fechaHastaCanje", fechaHastaCanje, System.Data.SqlDbType.Date)

            ds = sp.ReturnDataSet()

            If ds.Tables.Count > 0 Then
                For Each dataRow As DataRow In ds.Tables(0).Rows
                    listaCanje.Add(fillCanje(dataRow))
                Next
            End If
        Catch ex As Exception
            Throw
        End Try
        Return listaCanje
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function ConsultarTransito(canje As CanjeDTO) As List(Of CanjeDTO)
        Dim Lista As List(Of CanjeDTO) = New List(Of CanjeDTO)
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)
        Dim ds As DataSet
        Try
            sp.AgregarParametro("Accion", "SELECT_BY", System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("ApRut", canje.RutAportante, System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("FsNemotecnicoSaliente", canje.NemotecnicoSaliente, System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("CnFechaSolicitud", canje.FechaSolicitud, System.Data.SqlDbType.Date)

            ds = sp.ReturnDataSet()

            For Each dataRow As DataRow In ds.Tables(0).Rows
                Lista.Add(fillCanje(dataRow))
            Next

        Catch ex As Exception
            Throw
        End Try

        Return Lista

    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function EncontrarNemotecnicoSalientes(canje As CanjeDTO) As List(Of CanjeDTO)
        Dim Lista As List(Of CanjeDTO) = New List(Of CanjeDTO)
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)
        Dim ds As DataSet
        Try
            sp.AgregarParametro("Accion", "ENCONTRAR_SERIES_SALIENTE", System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("ApRut", canje.RutAportante, System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("FsNemotecnicoSaliente", canje.NemotecnicoSaliente, System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("CnFechaSolicitud", canje.FechaSolicitud, System.Data.SqlDbType.DateTime)

            ds = sp.ReturnDataSet()

            For Each dataRow As DataRow In ds.Tables(0).Rows
                Lista.Add(fillCanje(dataRow))
            Next

        Catch ex As Exception
            Throw
        End Try

        Return Lista

    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function CompararDatosEntrantes(serie As FondoSerieDTO) As List(Of FondoSerieDTO)
        Dim Lista As List(Of FondoSerieDTO) = New List(Of FondoSerieDTO)
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)
        Dim ds As DataSet
        Dim fondoSerieNew As New FondoSerieDTO

        Try
            sp.AgregarParametro("Accion", "COMPARAR_DATOS_SERIES_ENTRANTE", System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("FnRut", serie.Rut, System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("FsNemotecnicoEntrante", serie.Nemotecnico, System.Data.SqlDbType.VarChar)
            ds = sp.ReturnDataSet()

            For Each dataRow As DataRow In ds.Tables(0).Rows
                fondoSerieNew = New FondoSerieDTO
                With fondoSerieNew
                    .Rut = dataRow("FN_RUT").ToString().Trim()
                    .Nemotecnico = dataRow("FS_Nemotecnico").ToString.Trim()
                    .Nombrecorto = dataRow("FS_Nombre_Corto").ToString.Trim()
                    .Moneda = dataRow("FS_Moneda").ToString.Trim()
                    .Estado = dataRow("FS_Estado").ToString().Trim()
                End With
                Lista.Add(fondoSerieNew)
            Next
        Catch ex As Exception
            Throw
        End Try

        Return Lista
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function CompararDatosSalientes(serie As FondoSerieDTO) As List(Of FondoSerieDTO)
        Dim Lista As List(Of FondoSerieDTO) = New List(Of FondoSerieDTO)
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)
        Dim ds As DataSet
        Dim fondoSerieNew As New FondoSerieDTO

        Try
            sp.AgregarParametro("Accion", "COMPARAR_DATOS_SERIES_SALIENTE", System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("FnRut", serie.Rut, System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("FsNemotecnicoSaliente", serie.Nemotecnico, System.Data.SqlDbType.VarChar)
            ds = sp.ReturnDataSet()

            For Each dataRow As DataRow In ds.Tables(0).Rows
                fondoSerieNew = New FondoSerieDTO
                With fondoSerieNew
                    .Rut = dataRow("FN_RUT").ToString().Trim()
                    .Nemotecnico = dataRow("FS_Nemotecnico").ToString.Trim()
                    .Nombrecorto = dataRow("FS_Nombre_Corto").ToString.Trim()
                    .Moneda = dataRow("FS_Moneda").ToString.Trim()
                    .Estado = dataRow("FS_Estado").ToString().Trim()
                End With
                Lista.Add(fondoSerieNew)
            Next
        Catch ex As Exception
            Throw
        End Try

        Return Lista
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function CompararDatosAportantes(aportantes As AportanteDTO) As List(Of AportanteDTO)
        Dim Lista As List(Of AportanteDTO) = New List(Of AportanteDTO)
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)
        Dim ds As DataSet
        Dim aportanteNew As New AportanteDTO

        Try
            sp.AgregarParametro("Accion", "COMPARAR_APORTANTE", System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("ApRut", aportantes.Rut, System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("ApMultifondo", aportantes.Multifondo, System.Data.SqlDbType.VarChar)
            ds = sp.ReturnDataSet()

            For Each dataRow As DataRow In ds.Tables(0).Rows
                aportanteNew = New AportanteDTO
                With aportanteNew
                    .Rut = dataRow("AP_RUT").ToString().Trim()
                    .Multifondo = dataRow("AP_Multifondo").ToString.Trim()
                    .RazonSocial = dataRow("AP_Razon_Social").ToString.Trim()
                    .Estado = dataRow("AP_Estado").ToString().Trim()
                End With
                Lista.Add(aportanteNew)
            Next
        Catch ex As Exception
            Throw
        End Try
        Return Lista
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function EncontrarNemotecnicoEntrante(canje As CanjeDTO) As List(Of CanjeDTO)
        Dim Lista As List(Of CanjeDTO) = New List(Of CanjeDTO)
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)
        Dim ds As DataSet
        Try
            sp.AgregarParametro("Accion", "ENCONTRAR_SERIES_ENTRANTES", System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("ApRut", canje.RutAportante, System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("FsNemotecnicoSaliente", canje.NemotecnicoEntrante, System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("CnFechaSolicitud", canje.FechaSolicitud, System.Data.SqlDbType.DateTime)

            ds = sp.ReturnDataSet()

            For Each dataRow As DataRow In ds.Tables(0).Rows
                Lista.Add(fillCanje(dataRow))
            Next

        Catch ex As Exception
            Throw
        End Try

        Return Lista

    End Function
    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function CanjeTodos(canje As CanjeDTO) As List(Of CanjeDTO)
        Dim listacanje As List(Of CanjeDTO) = New List(Of CanjeDTO)
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)
        Dim ds As DataSet

        Try
            sp.AgregarParametro("Accion", CONST_SELECT_ALL, System.Data.SqlDbType.VarChar)
            FillParameters(canje, sp)
            ds = sp.ReturnDataSet()
            For Each dataRow As DataRow In ds.Tables(0).Rows
                listacanje.Add(fillCanje(dataRow))
            Next
        Catch ex As Exception
            Throw
        End Try
        Return listacanje
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function GetCanje(canje As CanjeDTO) As CanjeDTO
        Dim canjeRetorno As CanjeDTO = New CanjeDTO
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)
        Dim ds As DataSet

        Try
            sp.AgregarParametro("Accion", CONST_SELECT_ONE, System.Data.SqlDbType.VarChar)
            canje.Estado = DTO.Estados.CONST_ACTIVO

            FillParameters(canje, sp)
            ds = sp.ReturnDataSet()
            If ds.Tables.Count > 0 AndAlso ds.Tables(0).Rows.Count > 0 Then
                canjeRetorno = fillCanje(ds.Tables(0).Rows(0))
            Else
                canjeRetorno = Nothing
            End If

        Catch ex As Exception
            Throw
        End Try
        Return canjeRetorno
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function GetFijacionId(Id As Int32) As CanjeDTO
        Dim canjeRetorno As CanjeDTO = New CanjeDTO
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_FIJACION)
        Dim ds As DataSet

        Try
            sp.AgregarParametro("Accion", CONST_SELECT_ONE_FIJACION, System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("Id", Id, System.Data.SqlDbType.Int)
            sp.AgregarParametro("TipoTransaccion", "Canje", System.Data.SqlDbType.VarChar)

            ds = sp.ReturnDataSet()
            If ds.Tables.Count > 0 AndAlso ds.Tables(0).Rows.Count > 0 Then
                canjeRetorno = fillCanje(ds.Tables(0).Rows(0))
            Else
                canjeRetorno = Nothing
            End If

        Catch ex As Exception
            Throw
        End Try
        Return canjeRetorno
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function ConsultarUltimoCanje(Canje As CanjeDTO) As CanjeDTO
        Dim listaCanje As CanjeDTO = New CanjeDTO
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)
        Dim ds As DataSet

        Try
            sp.AgregarParametro("Accion", "CONSULTA_ULTIMO", System.Data.SqlDbType.VarChar)

            ds = sp.ReturnDataSet()

            For Each dataRow As DataRow In ds.Tables(0).Rows
                listaCanje = fillCanje(dataRow)
            Next

        Catch ex As Exception
        End Try

        Return listaCanje
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function CanjeNombreAportante(canje As CanjeDTO) As List(Of CanjeDTO)
        Dim listacanje As List(Of CanjeDTO) = New List(Of CanjeDTO)
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)
        Dim ds As DataSet

        Try
            sp.AgregarParametro("Accion", "CONSULTA_APORTANTE", System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("ApRazonSocial", canje.NombreAportante, System.Data.SqlDbType.VarChar)

            ds = sp.ReturnDataSet()

            For Each dataRow As DataRow In ds.Tables(0).Rows
                canje = New CanjeDTO
                With canje
                    .NombreAportante = dataRow("AP_Razon_Social").ToString().Trim()
                    .RutAportante = dataRow("AP_Rut").ToString().Trim()
                End With

                listacanje.Add(canje)
            Next

        Catch ex As Exception
            Throw
        End Try

        Return listacanje
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function CanjeNombreFondo(canje As CanjeDTO) As List(Of CanjeDTO)
        Dim listacanje As List(Of CanjeDTO) = New List(Of CanjeDTO)
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)
        Dim ds As DataSet

        Try
            sp.AgregarParametro("Accion", "CONSULTA_FONDO", System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("FnNombreCorto", canje.NombreFondo, System.Data.SqlDbType.VarChar)

            ds = sp.ReturnDataSet()

            For Each dataRow As DataRow In ds.Tables(0).Rows
                canje = New CanjeDTO
                With canje
                    .NombreFondo = dataRow("FN_Nombre_Corto").ToString().Trim()
                    .RutFondo = dataRow("FN_RUT").ToString.Trim()
                End With

                listacanje.Add(canje)
            Next

        Catch ex As Exception
            Throw
        End Try

        Return listacanje
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function CanjeNemotecnico(canje As CanjeDTO) As List(Of CanjeDTO)
        Dim listacanje As List(Of CanjeDTO) = New List(Of CanjeDTO)
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)
        Dim ds As DataSet

        Try
            sp.AgregarParametro("Accion", "CONSULTA_NEMOTECNICO", System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("FsNemotecnicoSaliente", canje.NemotecnicoSaliente, System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("FnRut", canje.RutFondo, System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("ApRut", canje.RutAportante, System.Data.SqlDbType.VarChar)

            ds = sp.ReturnDataSet()

            For Each dataRow As DataRow In ds.Tables(0).Rows
                canje = New CanjeDTO
                With canje
                    .NemotecnicoSaliente = dataRow("FS_Nemotecnico_Saliente").ToString().Trim()
                End With

                listacanje.Add(canje)
            Next

        Catch ex As Exception
            Throw
        End Try

        Return listacanje
    End Function



    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function CanjeNombreSerie(serie As FondoSerieDTO) As List(Of FondoSerieDTO)
        Dim listaserie As List(Of FondoSerieDTO) = New List(Of FondoSerieDTO)
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)
        Dim ds As DataSet

        Try
            sp.AgregarParametro("Accion", "CONSULTA_NOMBRE_SERIE", System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("FsNombreCortoSaliente", serie.Nombrecorto, System.Data.SqlDbType.VarChar)

            ds = sp.ReturnDataSet()

            For Each dataRow As DataRow In ds.Tables(0).Rows
                serie = New FondoSerieDTO
                With serie
                    .Nombrecorto = dataRow("FS_Nombre_Corto").ToString().Trim()
                End With

                listaserie.Add(serie)
            Next

        Catch ex As Exception
            Throw
        End Try

        Return listaserie
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function CanjeMultifondo(aportante As AportanteDTO) As List(Of AportanteDTO)
        Dim listamultifondo As List(Of AportanteDTO) = New List(Of AportanteDTO)
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)
        Dim ds As DataSet

        Try
            sp.AgregarParametro("Accion", "CONSULTA_MULTIFONDO", System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("ApMultifondo", aportante.Multifondo, System.Data.SqlDbType.VarChar)

            ds = sp.ReturnDataSet()

            For Each dataRow As DataRow In ds.Tables(0).Rows
                aportante = New AportanteDTO
                With aportante
                    .Multifondo = dataRow("AP_Multifondo").ToString().Trim()
                End With

                listamultifondo.Add(aportante)
            Next

        Catch ex As Exception
            Throw
        End Try

        Return listamultifondo
    End Function
    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function CanjeMonedaSerie(serie As FondoSerieDTO) As List(Of FondoSerieDTO)
        Dim listaserie As List(Of FondoSerieDTO) = New List(Of FondoSerieDTO)
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)
        Dim ds As DataSet

        Try
            sp.AgregarParametro("Accion", "CONSULTA_MONEDA_SERIE", System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("FsMonedaSaliente", serie.Moneda, System.Data.SqlDbType.VarChar)

            ds = sp.ReturnDataSet()

            For Each dataRow As DataRow In ds.Tables(0).Rows
                serie = New FondoSerieDTO
                With serie
                    .Moneda = dataRow("FS_Moneda").ToString().Trim()
                End With

                listaserie.Add(serie)
            Next

        Catch ex As Exception
            Throw
        End Try

        Return listaserie
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function CanjeEstado(canje As CanjeDTO) As List(Of CanjeDTO)
        Dim listacanje As List(Of CanjeDTO) = New List(Of CanjeDTO)
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)
        Dim ds As DataSet

        Try
            sp.AgregarParametro("Accion", "CONSULTA_ESTADO", System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("CnEstadoCanje", canje.EstadoCanje, System.Data.SqlDbType.VarChar)

            ds = sp.ReturnDataSet()

            For Each dataRow As DataRow In ds.Tables(0).Rows
                canje = New CanjeDTO
                With canje
                    .EstadoCanje = dataRow("CN_Estado_Canje").ToString().Trim()
                End With

                listacanje.Add(canje)
            Next

        Catch ex As Exception
            Throw
        End Try

        Return listacanje
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function CanjeIngresar(canje As CanjeDTO)
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)
        Dim ds As DataSet
        Dim CanjeRetorno As CanjeDTO = New CanjeDTO
        Try
            sp.AgregarParametro("Accion", CONST_INSERT, System.Data.SqlDbType.VarChar)
            canje.Estado = DTO.Estados.CONST_ACTIVO

            FillParameters(canje, sp)
            ds = sp.ReturnDataSet()
            If ds.Tables.Count > 0 AndAlso ds.Tables(0).Rows.Count > 0 Then
                Dim dataRow As DataRow = ds.Tables(0).Rows(0)
                Try
                    CanjeRetorno.IdCanje = dataRow("CN_ID")
                Catch ex As Exception
                    CanjeRetorno.IdCanje = 0
                End Try
            End If
            Return CanjeRetorno
        Catch ex As Exception
            Throw
        End Try
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function CanjeModificar(canje As CanjeDTO) As Boolean
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)
        Try
            sp.AgregarParametro("Accion", CONST_UPDATE, System.Data.SqlDbType.VarChar)
            canje.Estado = DTO.Estados.CONST_ACTIVO

            FillParameters(canje, sp)
            sp.ReturnDataSet()

            Return (sp.FilasAfectas > 0)
        Catch ex As Exception
            Throw
        End Try
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function CanjeEliminar(canje As CanjeDTO) As Boolean
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)
        Try
            sp.AgregarParametro("Accion", CONST_DELETE, System.Data.SqlDbType.VarChar)
            canje.Estado = DTO.Estados.CONST_ELIMINADO

            FillParameters(canje, sp)
            sp.ReturnDataSet()

            Return (sp.FilasAfectas > 0)
        Catch ex As Exception
            Return False
        End Try
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function CanjesTransito(canje As CanjeDTO) As CanjeDTO
        Dim CanjeRetorno As CanjeDTO = New CanjeDTO
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)
        Dim ds As DataSet
        Try
            sp.AgregarParametro("Accion", "CANJES_TRANSITO", System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("ApRut", canje.RutAportante, System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("FsNemotecnicoSaliente", canje.NemotecnicoSaliente, System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("CnFechaSolicitud", canje.FechaSolicitud, System.Data.SqlDbType.Date)
            sp.AgregarParametro("ApMultifondo", canje.Multifondo, System.Data.SqlDbType.Char)

            ds = sp.ReturnDataSet()

            If ds.Tables.Count > 0 AndAlso ds.Tables(0).Rows.Count > 0 Then
                Dim dataRow As DataRow = ds.Tables(0).Rows(0)
                Try
                    CanjeRetorno.CanjeTransito = dataRow("Canjes_Transito")
                Catch ex As Exception
                    CanjeRetorno.CanjeTransito = 0
                End Try
            End If

        Catch ex As Exception
            Throw
        End Try

        Return canjeRetorno
    End Function



    Private Sub FillParameters(canje As CanjeDTO, sp As DBSqlServer.SqlServer.StoredProcedure)
        sp.AgregarParametro("CnId", canje.IdCanje, System.Data.SqlDbType.Int)
        sp.AgregarParametro("CnTipoTransaccion", canje.TipoTransaccion, System.Data.SqlDbType.VarChar)
        sp.AgregarParametro("ApRut", canje.RutAportante, System.Data.SqlDbType.VarChar)
        sp.AgregarParametro("ApMultifondo", canje.Multifondo, System.Data.SqlDbType.Char)
        sp.AgregarParametro("ApRazonSocial", canje.NombreAportante, System.Data.SqlDbType.VarChar)
        sp.AgregarParametro("FnRut", canje.RutFondo, System.Data.SqlDbType.VarChar)
        sp.AgregarParametro("FnNombreCorto", canje.NombreFondo, System.Data.SqlDbType.VarChar)
        sp.AgregarParametro("CnFechaNav", canje.FechaNavSaliente, System.Data.SqlDbType.Date)
        sp.AgregarParametro("CnFechaSolicitud", canje.FechaSolicitud, System.Data.SqlDbType.Date)
        sp.AgregarParametro("CnFechaObservado", canje.FechaObservado, System.Data.SqlDbType.Date)
        sp.AgregarParametro("FsNemotecnicoSaliente", canje.NemotecnicoSaliente, System.Data.SqlDbType.VarChar)
        sp.AgregarParametro("FsNombreCortoSaliente", canje.NombreSerieSaliente, System.Data.SqlDbType.VarChar)
        sp.AgregarParametro("FsMonedaSaliente", canje.MonedaSaliente, System.Data.SqlDbType.VarChar)
        sp.AgregarParametro("CnCuotaSaliente", canje.CuotaSaliente, System.Data.SqlDbType.Decimal)
        sp.AgregarParametro("VcsValorSaliente", canje.NavSaliente, System.Data.SqlDbType.Decimal)
        sp.AgregarParametro("CnMontoSaliente", canje.MontoSaliente, System.Data.SqlDbType.Decimal)
        sp.AgregarParametro("CnNavCLPSaliente", canje.NavCLPSaliente, System.Data.SqlDbType.Decimal)
        sp.AgregarParametro("CnMontoCLPSaliente", canje.MontoCLPSaliente, System.Data.SqlDbType.Decimal)
        sp.AgregarParametro("CnFactor", canje.Factor, System.Data.SqlDbType.Decimal)
        sp.AgregarParametro("CnDiferencia", canje.Diferencia, System.Data.SqlDbType.Decimal)
        sp.AgregarParametro("CnDiferenciaCLP", canje.DiferenciaCLP, System.Data.SqlDbType.Decimal)
        sp.AgregarParametro("FsNemotecnicoEntrante", canje.NemotecnicoEntrante, System.Data.SqlDbType.VarChar)
        sp.AgregarParametro("FsNombreCortoEntrante ", canje.NombreSerieEntrante, System.Data.SqlDbType.VarChar)
        sp.AgregarParametro("FsMonedaEntrante", canje.MonedaEntrante, System.Data.SqlDbType.VarChar)
        sp.AgregarParametro("CnCuotaEntrante", canje.CuotaEntrante, System.Data.SqlDbType.Decimal)
        sp.AgregarParametro("VcsValorEntrante", canje.NavEntrante, System.Data.SqlDbType.Decimal)
        sp.AgregarParametro("CnMontoEntrante", canje.MontoEntrante, System.Data.SqlDbType.Decimal)
        sp.AgregarParametro("CnNavCLPEntrante", canje.NavCLPEntrante, System.Data.SqlDbType.Decimal)
        sp.AgregarParametro("CnMontoCLPEntrante", canje.MontoCLPEntrante, System.Data.SqlDbType.Decimal)
        sp.AgregarParametro("CnContratoGeneral", canje.ContratoGeneral, System.Data.SqlDbType.NChar)
        sp.AgregarParametro("CnRevisionPoderes", canje.RevisionPoderes, System.Data.SqlDbType.NChar)
        sp.AgregarParametro("CnEstadoCanje", canje.EstadoCanje, System.Data.SqlDbType.VarChar)
        sp.AgregarParametro("CnObservaciones", canje.Observaciones, System.Data.SqlDbType.VarChar)
        sp.AgregarParametro("DvcFechaAct", canje.FechaActual, System.Data.SqlDbType.Date)
        sp.AgregarParametro("DvcCuotas", canje.Cuotas, System.Data.SqlDbType.Decimal)
        sp.AgregarParametro("RescateTransito", canje.RescateTransito, System.Data.SqlDbType.Decimal)
        sp.AgregarParametro("SuscripcionTransito", canje.SuscripcionTransito, System.Data.SqlDbType.Int)
        sp.AgregarParametro("CanjeTransito", canje.CanjeTransito, System.Data.SqlDbType.Int)
        sp.AgregarParametro("CnCuotasDisponibles", canje.CuotasDisponibles, System.Data.SqlDbType.Decimal)
        sp.AgregarParametro("CnFijacionNav", canje.FijacionNav, System.Data.SqlDbType.VarChar)
        sp.AgregarParametro("CnFijacionTc", canje.FijacionTC, System.Data.SqlDbType.VarChar)
        sp.AgregarParametro("CnEstado", canje.Estado, System.Data.SqlDbType.Int)
        sp.AgregarParametro("CnUsuario", canje.UsuarioIngreso, System.Data.SqlDbType.VarChar)
        sp.AgregarParametro("CnFechaNavEntrante", canje.FechaNavEntrante, System.Data.SqlDbType.Date)
        sp.AgregarParametro("CnTipoCambio", canje.TipoCambio, System.Data.SqlDbType.VarChar)
        If Not (canje.FechaCanjeDate = Nothing OrElse canje.FechaCanjeDate.ToString("yyyyMMdd").Equals("00010101")) Then
            sp.AgregarParametro("cnFechaCanje", canje.FechaCanjeDate, System.Data.SqlDbType.Date)
        End If

    End Sub

    Private Function fillCanje(datarow As DataRow) As CanjeDTO
        Dim canje As New CanjeDTO

        With canje
            .IdCanje = datarow("CN_ID").ToString().Trim()
            .TipoTransaccion = datarow("CN_Tipo_Transaccion").ToString().Trim()
            .RutAportante = datarow("AP_RUT").ToString().Trim()
            .NombreAportante = datarow("AP_Razon_Social").ToString.Trim()
            .Multifondo = datarow("AP_Multifondo").ToString().Trim()
            .RutFondo = datarow("FN_RUT").ToString().Trim()
            .NombreFondo = datarow("FN_Razon_Social").ToString().Trim()
            .FechaNavSaliente = datarow("CN_Fecha_Nav").ToString().Trim()
            .FechaSolicitud = datarow("CN_Fecha_Solicitud").ToString().Trim()
            .FechaObservado = datarow("CN_Fecha_Observado").ToString().Trim()
            .NemotecnicoSaliente = datarow("FS_Nemotecnico_Saliente").ToString().Trim()
            .NombreSerieSaliente = datarow("FS_Nombre_Corto_Saliente").ToString().Trim()
            .MonedaSaliente = datarow("FS_Moneda_Saliente").ToString().Trim()
            .CuotaSaliente = datarow("CN_Cuotas_Saliente").ToString().Trim()
            .NavSaliente = datarow("VCS_Valor_Saliente").ToString().Trim()
            .MontoSaliente = datarow("CN_Monto_Saliente").ToString().Trim()
            .NavCLPSaliente = datarow("CN_Nav_CLP_Saliente").ToString().Trim()
            .MontoCLPSaliente = datarow("CN_Monto_CLP_Saliente").ToString().Trim()
            .Factor = datarow("CN_Factor").ToString().Trim()
            .Diferencia = datarow("CN_Diferencia").ToString().Trim()
            .DiferenciaCLP = datarow("CN_Diferencia_CLP").ToString().Trim()
            .NemotecnicoEntrante = datarow("FS_Nemotecnico_Entrate").ToString().Trim()
            .NombreSerieEntrante = datarow("FS_Nombre_Corto_Entrante").ToString().Trim()
            .MonedaEntrante = datarow("FS_Moneda_Entrante").ToString().Trim()
            .CuotaEntrante = datarow("CN_Cuotas_Entrate").ToString().Trim()
            .NavEntrante = datarow("VCS_Valor_Entrante").ToString().Trim()
            .MontoEntrante = datarow("CN_Monto_Entrante").ToString().Trim()
            .NavCLPEntrante = datarow("CN_Nav_CLP_Entrante").ToString().Trim()
            .MontoCLPEntrante = datarow("CN_Monto_CLP_Entrante").ToString().Trim()
            .ContratoGeneral = datarow("CN_Contrato_General").ToString().Trim()
            .RevisionPoderes = datarow("CN_Revision_Poderes").ToString().Trim()
            .EstadoCanje = datarow("CN_Estado_Canje").ToString().Trim()
            .Observaciones = datarow("CN_Observaciones").ToString().Trim()
            .FechaActual = datarow("DCV_Fecha_Act").ToString().Trim()
            .Cuotas = datarow("DVC_Cuotas").ToString().Trim()
            .RescateTransito = datarow("RES_Rescates_Transito").ToString().Trim()
            .SuscripcionTransito = datarow("SUP_Suspcion_Transito").ToString().Trim()
            .CanjeTransito = datarow("CN_Canje_Transito").ToString().Trim()
            .CuotasDisponibles = datarow("CN_Cuotas_Disponibles").ToString().Trim()
            .FijacionNav = datarow("CN_Fijacion_Nav").ToString().Trim()
            .FijacionTC = datarow("CN_Fijacion_TC").ToString().Trim()
            .Estado = datarow("CN_Estado").ToString().Trim()
            .FechaIngreso = datarow("CN_Fecha_Ingreso").ToString().Trim()
            .UsuarioIngreso = datarow("CN_Usuario_Ingreso").ToString().Trim()
            .FechaModificacion = datarow("CN_Fecha_Modificacion").ToString().Trim()
            .UsuarioModificacion = datarow("CN_Usuario_Modificacion").ToString().Trim()
            .FechaNavEntrante = datarow("CN_Fecha_Nav_Entrante").ToString().Trim()
            .TipoCambio = datarow("CN_Tipo_Cambio").ToString().Trim()

            If datarow("CN_Fecha_Canje").Equals("01-01-1900") Or IsDBNull(datarow("CN_Fecha_Canje")) Then
                .FechaCanjeDate = Nothing
            Else
                .FechaCanjeDate = datarow("CN_Fecha_Canje").ToString().Trim()
            End If

            .HoraTransaccion = datarow("HoraTransaccion").ToString().Trim()
        End With
        Return canje
    End Function

End Class