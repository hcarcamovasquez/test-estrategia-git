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
Public Class WSGrupoAportantes
    Inherits System.Web.Services.WebService

    Private Const SP_CRUD As String = "PRC_GrupoAportantesCRUD"
    Private Const SP_CRUD_APGP As String = "PRC_ApGpCRUD"
    Private Const CONST_APORTANTE_EN_GRUPO = "APORTANTE_EN_GRUPO"

    Private Const CONST_INSERT As String = "INSERT"
    Private Const CONST_UPDATE As String = "UPDATE"
    Private Const CONST_DELETE As String = "DELETE"
    Private Const CONST_SELECT_ALL As String = "SELECT_ALL"
    Private Const CONST_SELECT_ONE As String = "SELECT_ONE"
    Private Const CONST_DELETE_ONE As String = "SELECT_ONE"
    Private Const CONST_CONSULTA_FILTRO As String = "PRC_GrupoAportantesFiltro"

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function GrupoAportanteTraerID(aportante As AportantesXGrupoDTO) As List(Of AportantesXGrupoDTO)
        Return gruposDistinct(aportante, "SELECT_DISTINCT_ID")
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function GrupoAportanteTraerNombre(aportante As AportantesXGrupoDTO) As List(Of AportantesXGrupoDTO)
        Return gruposDistinct(aportante, "SELECT_DISTINCT_NOMBRE")
    End Function

    Private Function gruposDistinct(aportante As AportantesXGrupoDTO, accion As String) As List(Of AportantesXGrupoDTO)
        Dim ListaAportantes As List(Of AportantesXGrupoDTO) = New List(Of AportantesXGrupoDTO)
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)
        Dim ds As DataSet
        Dim aportanteNew As New AportantesXGrupoDTO
        Dim campo As String

        Try

            If accion = "SELECT_DISTINCT_NOMBRE" Then
                campo = "GPA_Descripcion"
            ElseIf accion = "SELECT_DISTINCT_ID" Then
                campo = "GPA_ID"
            Else
                Return ListaAportantes
            End If

            ' ---TODO Data prueba---
            sp.AgregarParametro("Accion", accion, System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("@GpaId", aportante.IdGrupo, System.Data.SqlDbType.Int)
            sp.AgregarParametro("@GpaDescripcion", aportante.NombreGrupo, System.Data.SqlDbType.VarChar)

            ds = sp.ReturnDataSet()

            For Each dataRow As DataRow In ds.Tables(0).Rows
                aportanteNew = New AportantesXGrupoDTO
                With aportanteNew
                    .IdGrupo = dataRow(campo).ToString().Trim()
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
    Public Function poseeOtroGrupo(aportanteXGrupo As AportantesXGrupoDTO) As Integer
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD_APGP)
        Dim ds As DataSet
        Try
            sp.AgregarParametro("Accion", CONST_APORTANTE_EN_GRUPO, System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("APRut", aportanteXGrupo.RutAportante, System.Data.SqlDbType.VarChar)

            ds = sp.ReturnDataSet()

            If ds.Tables.Count > 0 AndAlso ds.Tables(0).Rows.Count > 0 Then
                Dim dataRow As DataRow = ds.Tables(0).Rows(0)
                Return dataRow("ExisteEnGrupo")
            Else
                Return Constantes.CONST_ERROR_BBDD
            End If

        Catch ex As Exception
            Return Constantes.CONST_ERROR_BBDD
        End Try
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function GrupoAportanteFiltro(GrupoAportante As AportantesXGrupoDTO, FechaHasta As Nullable(Of Date)) As List(Of AportantesXGrupoDTO)
        Dim ListaAportantes As List(Of AportantesXGrupoDTO) = New List(Of AportantesXGrupoDTO)
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(CONST_CONSULTA_FILTRO)
        Dim ds As DataSet

        Try

            sp.AgregarParametro("accion", "SELECT_FILTRO", System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("GpaId", GrupoAportante.IdGrupo, System.Data.SqlDbType.Int)
            sp.AgregarParametro("GpaDescripcion", GrupoAportante.NombreGrupo, System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("fechaDesde", GrupoAportante.FechaIngreso, System.Data.SqlDbType.Date)
            sp.AgregarParametro("fechaHasta", FechaHasta, System.Data.SqlDbType.Date)

            ds = sp.ReturnDataSet()

            For Each dataRow As DataRow In ds.Tables(0).Rows
                ListaAportantes.Add(fillGrupoAportante(dataRow))
            Next

        Catch ex As Exception
            Throw
        End Try

        Return ListaAportantes

    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function GrupoAportanteConsultar(GrupoAportante As GrupoAportanteDTO) As List(Of GrupoAportanteDTO)
        Dim ListaAportantes As List(Of GrupoAportanteDTO) = New List(Of GrupoAportanteDTO)
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)
        Dim ds As DataSet

        Try
            sp.AgregarParametro("Accion", CONST_SELECT_ALL, System.Data.SqlDbType.VarChar)

            FillParameters(GrupoAportante, sp)

            ds = sp.ReturnDataSet()

            For Each dataRow As DataRow In ds.Tables(0).Rows
                ListaAportantes.Add(fillObject(dataRow))
            Next

        Catch ex As Exception
            Throw
        End Try

        Return ListaAportantes
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function GrupoAportanteTraer(GrupoAportante As GrupoAportanteDTO) As GrupoAportanteDTO
        Dim grupo As GrupoAportanteDTO = New GrupoAportanteDTO
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)
        Dim ds As DataSet

        Try
            sp.AgregarParametro("Accion", CONST_SELECT_ALL, System.Data.SqlDbType.VarChar)

            FillParameters(GrupoAportante, sp)

            ds = sp.ReturnDataSet()

            If ds.Tables.Count > 0 AndAlso ds.Tables(0).Rows.Count > 0 Then
                grupo = fillObject(ds.Tables(0).Rows(0))
            End If

        Catch ex As Exception
            Throw
        End Try

        Return grupo
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function GrupoAportanteIngresar(aportante As GrupoAportanteDTO) As Integer
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)
        Dim ds As DataSet
        Try
            sp.AgregarParametro("Accion", CONST_INSERT, System.Data.SqlDbType.VarChar)

            aportante.GPA_Estado = DTO.Estados.CONST_ACTIVO

            FillParameters(aportante, sp)
            ds = sp.ReturnDataSet()

            If ds.Tables.Count > 0 AndAlso ds.Tables(0).Rows.Count > 0 Then
                Dim dataRow As DataRow = ds.Tables(0).Rows(0)
                Return dataRow("ultimoId")
            Else
                Return Constantes.CONST_ERROR_BBDD
            End If

        Catch ex As Exception
            Throw
        End Try
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function GrupoAportanteModificar(aportante As GrupoAportanteDTO) As Integer
        Dim ds As DataSet
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)
        Try
            ' ---TODO Data prueba---
            sp.AgregarParametro("Accion", CONST_UPDATE, System.Data.SqlDbType.VarChar)

            aportante.GPA_Estado = DTO.Estados.CONST_ACTIVO
            FillParameters(aportante, sp)

            ds = sp.ReturnDataSet()

            If ds.Tables.Count > 0 AndAlso ds.Tables(0).Rows.Count > 0 Then
                Dim dataRow As DataRow = ds.Tables(0).Rows(0)
                Return dataRow("filasAfectas")
            Else
                Return Constantes.CONST_ERROR_BBDD
            End If

        Catch ex As Exception
            Throw
        End Try
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function GrupoAportanteEliminar(aportante As GrupoAportanteDTO) As Integer
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)
        Try
            ' ---TODO Data prueba---
            sp.AgregarParametro("Accion", CONST_DELETE, System.Data.SqlDbType.VarChar)
            aportante.GPA_Estado = DTO.Estados.CONST_ELIMINADO
            FillParameters(aportante, sp)

            sp.ReturnDataSet()

            Return sp.FilasAfectas
        Catch ex As Exception
            Return sp.FilasAfectas
        End Try
    End Function

    Private Sub FillParameters(grupoAportante As GrupoAportanteDTO, sp As DBSqlServer.SqlServer.StoredProcedure)
        sp.AgregarParametro("GpaId", grupoAportante.GPA_Id, System.Data.SqlDbType.Decimal)
        sp.AgregarParametro("GpaDescripcion", grupoAportante.GPA_Descripcion, System.Data.SqlDbType.VarChar)
        sp.AgregarParametro("GpaEstado", grupoAportante.GPA_Estado, System.Data.SqlDbType.Int)
        sp.AgregarParametro("GpaUsuario", grupoAportante.GPA_UsuarioIngreso, System.Data.SqlDbType.VarChar)
    End Sub

    Private Function fillGrupoAportante(dataRow As DataRow) As AportantesXGrupoDTO
        Dim grupoAportante As New AportantesXGrupoDTO
        With grupoAportante
            .IdGrupo = dataRow("GPA_ID").ToString().Trim()
            .RutAportante = dataRow("AP_RUT").ToString().Trim()
            .NombreGrupo = dataRow("GPA_Descripcion").ToString().Trim()
            .NombreAportante = dataRow("AP_Razon_Social").ToString().Trim()
            .Estado = dataRow("GPA_Estado").ToString().Trim()
            .FechaIngreso = dataRow("GPA_Fecha_Ingreso").ToString().Trim()
            .UsuarioIngreso = dataRow("GPA_Usuario_Ingreso").ToString().Trim()
            .FechaModificacion = dataRow("GPA_Fecha_Modificacion").ToString().Trim()
            .UsuarioModificacion = dataRow("GPA_Usuario_Modificacion").ToString().Trim()

        End With
        Return grupoAportante
    End Function

    Private Function fillObject(dataRow As DataRow) As GrupoAportanteDTO
        Dim grupoAportante As New GrupoAportanteDTO
        With grupoAportante

            .GPA_Id = dataRow("GPA_ID").ToString().Trim()
            .GPA_Descripcion = dataRow("GPA_Descripcion").ToString().Trim()
            .GPA_Estado = dataRow("GPA_Estado").ToString().Trim()
            .GPA_FechaIngreso = dataRow("GPA_Fecha_Ingreso").ToString().Trim()
            .GPA_UsuarioIngreso = dataRow("GPA_Usuario_Ingreso").ToString().Trim()
            .GPA_FechaModificacion = dataRow("GPA_Fecha_Modificacion").ToString().Trim()
            .GPA_UsuarioModificacion = dataRow("GPA_Usuario_Modificacion").ToString().Trim()

        End With
        Return grupoAportante
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function AportanteXGrupoIngresar(aportanteXGrupo As AportantesXGrupoDTO) As Boolean
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure("PRC_APGPCRUD")
        Try
            sp.AgregarParametro("Accion", "INSERT", System.Data.SqlDbType.VarChar)

            sp.AgregarParametro("GpaId", aportanteXGrupo.IdGrupo, System.Data.SqlDbType.Decimal)
            sp.AgregarParametro("ApRut", aportanteXGrupo.RutAportante, System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("GpaEstado", aportanteXGrupo.Estado, System.Data.SqlDbType.Int)
            sp.AgregarParametro("GpaUsuario", aportanteXGrupo.UsuarioIngreso, System.Data.SqlDbType.VarChar)

            sp.ReturnDataSet()

            Return True
        Catch ex As Exception
            Throw
        End Try
    End Function

End Class