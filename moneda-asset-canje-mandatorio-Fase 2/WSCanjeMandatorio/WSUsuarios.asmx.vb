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
Public Class WSUsuarios
    Inherits System.Web.Services.WebService

    Private Const SP_CRUD As String = "PRC_UsuarioCRUD"

    Private Const CONST_INSERT As String = "INSERT"
    Private Const CONST_UPDATE As String = "UPDATE"
    Private Const CONST_DELETE As String = "DELETE"
    Private Const CONST_SELECT_ALL As String = "SELECT_ALL"
    Private Const CONST_SELECT_ONE As String = "SELECT_ONE"
    Private Const CONST_SELECT_FILTRO As String = "SELECT_FILTRO"

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function UsuariosConsultarFiltro(usuario As UsuarioDTO, FechaHasta As Nullable(Of Date)) As List(Of UsuarioDTO)
        Dim listaUsuarios As List(Of UsuarioDTO) = New List(Of UsuarioDTO)
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)
        Dim ds As DataSet

        Try
            sp.AgregarParametro("Accion", CONST_SELECT_FILTRO, System.Data.SqlDbType.VarChar)

            FillParameters(usuario, sp)

            sp.AgregarParametro("fechaDesde", usuario.US_FechaIngreso, System.Data.SqlDbType.Date)
            sp.AgregarParametro("fechaHasta", FechaHasta, System.Data.SqlDbType.Date)

            ds = sp.ReturnDataSet()

            For Each dataRow As DataRow In ds.Tables(0).Rows
                listaUsuarios.Add(fillObject(dataRow))
            Next

        Catch ex As Exception
            Throw
        End Try

        Return listaUsuarios
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function GetListaUsuarios(usuario As UsuarioDTO) As List(Of UsuarioDTO)
        Dim listaUsuarios As List(Of UsuarioDTO) = New List(Of UsuarioDTO)
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)
        Dim ds As DataSet

        Try
            sp.AgregarParametro("Accion", CONST_SELECT_ALL, System.Data.SqlDbType.VarChar)

            FillParameters(usuario, sp)
            ds = sp.ReturnDataSet()

            For Each dataRow As DataRow In ds.Tables(0).Rows
                listaUsuarios.Add(fillObject(dataRow))
            Next

        Catch ex As Exception
            Throw
        End Try
        Return listaUsuarios
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function UsuarioTraerPorNombre(usuario As UsuarioDTO) As UsuarioDTO
        Try
            Return UsuarioTraer(usuario, "SELECT_BYNAME")
        Catch ex As Exception
            Throw
        End Try
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function UsuarioTraerPorID(usuario As UsuarioDTO) As UsuarioDTO
        Try
            Return UsuarioTraer(usuario, CONST_SELECT_ONE)
        Catch ex As Exception
            Throw
        End Try
    End Function


    Private Function UsuarioTraer(usuario As UsuarioDTO, accion As String) As UsuarioDTO
        Dim usuarioRet As UsuarioDTO = New UsuarioDTO
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)
        Dim ds As DataSet

        Try
            sp.AgregarParametro("Accion", accion, System.Data.SqlDbType.VarChar)

            FillParameters(usuario, sp)

            ds = sp.ReturnDataSet()

            If ds.Tables.Count > 0 AndAlso ds.Tables(0).Rows.Count > 0 Then
                usuarioRet = fillObject(ds.Tables(0).Rows(0))
            End If

        Catch ex As Exception
            Throw
        End Try

        Return usuarioRet
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function UsuarioModificar(usuario As UsuarioDTO) As Boolean
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)

        Try
            ' ---TODO Data prueba---
            sp.AgregarParametro("Accion", CONST_UPDATE, System.Data.SqlDbType.VarChar)

            FillParameters(usuario, sp)

            sp.ReturnDataSet()

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function UsuarioEliminar(usuario As UsuarioDTO) As Boolean
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)
        Try
            usuario.US_Estado = 0
            ' ---TODO Data prueba---
            sp.AgregarParametro("Accion", CONST_DELETE, System.Data.SqlDbType.VarChar)
            FillParameters(usuario, sp)
            sp.ReturnDataSet()

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function UsuarioIngresar(usuario As UsuarioDTO) As Integer
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)
        Try
            sp.AgregarParametro("Accion", CONST_INSERT, System.Data.SqlDbType.VarChar)

            usuario.US_Estado = DTO.Estados.CONST_ACTIVO

            FillParameters(usuario, sp)

            sp.ReturnDataSet()

            Return True

        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Shared Sub FillParameters(usuario As UsuarioDTO, sp As DBSqlServer.SqlServer.StoredProcedure)
        Try
            sp.AgregarParametro("UsId", usuario.US_Id, System.Data.SqlDbType.Int)
            sp.AgregarParametro("UsNombre", usuario.US_Nombre, System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("UsPerfil", usuario.US_Perfil, System.Data.SqlDbType.Int)
            sp.AgregarParametro("UsEstado", usuario.US_Estado, System.Data.SqlDbType.Int)
            sp.AgregarParametro("UsUsuario", usuario.US_UsuarioIngreso, System.Data.SqlDbType.VarChar)
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Private Function fillObject(dataRow As DataRow) As UsuarioDTO
        Dim usuario As New UsuarioDTO
        Try
            With usuario
                .US_Id = dataRow("US_ID").ToString().Trim()
                .US_Nombre = dataRow("US_Nombre").ToString().Trim()
                .US_Estado = dataRow("US_Estado").ToString().Trim()
                .US_Perfil = dataRow("US_Perfil").ToString().Trim()
                .US_FechaIngreso = dataRow("US_FechaIngreso").ToString().Trim()
                .US_UsuarioIngreso = dataRow("US_UsuarioIngreso").ToString().Trim()
                .US_FechaModificacion = dataRow("US_FechaModificacion").ToString().Trim()
                .US_UsuarioModificacion = dataRow("US_UsuarioModificacion").ToString().Trim()
            End With
            Return usuario
        Catch ex As Exception
            Throw
        End Try
    End Function
End Class