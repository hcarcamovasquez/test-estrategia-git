Imports DTO
Imports WSCanjeMandatorio.WSUsuarios

Public Class UsuarioDatos
    Private Ws As WSCanjeMandatorio.WSUsuarios = New WSCanjeMandatorio.WSUsuarios()

    Public Function UsuariosConsultarFiltro(usuario As UsuarioDTO, FechaHasta As Nullable(Of Date)) As List(Of UsuarioDTO)
        Return Ws.UsuariosConsultarFiltro(usuario, FechaHasta)
    End Function

    Public Function GetListaUsuarios(fondo As UsuarioDTO) As List(Of UsuarioDTO)
        Return Ws.GetListaUsuarios(fondo)
    End Function

    Public Function UsuarioTraerPorID(usuario As UsuarioDTO) As UsuarioDTO
        Return GetUsuario(usuario, "POR_ID")
    End Function

    Public Function UsuarioTraerPorNombre(usuario As UsuarioDTO) As UsuarioDTO
        Return GetUsuario(usuario, "POR_NOMBRE")
    End Function

    Private Function GetUsuario(usuario As UsuarioDTO, accion As String) As UsuarioDTO
        If (accion = "POR_ID") Then
            Return Ws.UsuarioTraerPorID(usuario)
        ElseIf accion = "POR_NOMBRE" Then
            Return Ws.UsuarioTraerPorNombre(usuario)
        Else
            Return New UsuarioDTO()
        End If
    End Function

    Private Function EjecutaAccion(usuario As UsuarioDTO, accion As String) As Integer
        If accion = "DELETE" Then
            Return Ws.UsuarioEliminar(usuario)
        ElseIf accion = "UPDATE" Then
            Return Ws.UsuarioModificar(usuario)
        ElseIf accion = "INSERT" Then
            Return Ws.UsuarioIngresar(usuario)
        Else
            Return Constantes.CONST_ERROR_BBDD
        End If
    End Function

    Public Function DeleteUsuario(usuario As UsuarioDTO) As Integer
        Return EjecutaAccion(usuario, "DELETE")
    End Function

    Public Function InsertUsuario(usuario As UsuarioDTO) As Integer
        Return EjecutaAccion(usuario, "INSERT")
    End Function

    Public Function UpdateUsuario(usuario As UsuarioDTO) As Integer
        Return EjecutaAccion(usuario, "UPDATE")
    End Function
End Class
