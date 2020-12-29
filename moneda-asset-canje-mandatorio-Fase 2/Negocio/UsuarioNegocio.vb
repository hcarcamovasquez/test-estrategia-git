Imports DTO
Imports Datos
Imports ActiveDirectoryWrapper.PC

Public Class UsuarioNegocio
    Private Excel As ExcelWriter = New ExcelWriter
    Public Property rutaArchivosExcel As String
    Private Datos As UsuarioDatos = New UsuarioDatos()

    Public Const CONST_MENSAJE_EXCEL_GUARDADO As String = "Excel Guardado de forma Exitosa"
    Public Const CONST_MENSAJE_EXCEL_ERROR As String = "Error al Guardar Excel"

    Public Function UsuariosConsultarFiltro(usuario As UsuarioDTO, FechaHasta As Nullable(Of Date)) As List(Of UsuarioDTO)
        Return Datos.UsuariosConsultarFiltro(usuario, FechaHasta)
    End Function

    Public Function GetListaUsuarios(usuario As UsuarioDTO) As List(Of UsuarioDTO)
        Return Datos.GetListaUsuarios(usuario)
    End Function

    Public Function GetUsuarioPorNombre(usuario As UsuarioDTO) As UsuarioDTO
        Return Datos.UsuarioTraerPorNombre(usuario)
    End Function

    Public Function GetUsuarioPorID(usuario As UsuarioDTO) As UsuarioDTO
        Return Datos.UsuarioTraerPorID(usuario)
    End Function

    ''' <summary>
    ''' Permite verificar si una cuenta en AD es totalmente valida
    ''' </summary>
    ''' <param name="username">Nombre de Usuario</param>
    ''' <param name="userPass">Contraseña</param>
    ''' <returns>true : LoginOk false: Login failed </returns>
    Public Function IsUserValidInActiveDirectory(username As String, userPass As String) As Boolean
        Dim loginOK As ADWrapper.LoginResult
        loginOK = ADWrapper.Login(username, userPass)
        Return (loginOK = ADWrapper.LoginResult.LOGIN_OK)
    End Function

    Public Function GetListaUsuariosActiveDirectory(user As String, pass As String) As List(Of String)
        Dim configurationAppSettings As New System.Configuration.AppSettingsReader()
        Dim Ac As New List(Of String)
        Dim domain As String = DirectCast(configurationAppSettings.GetValue("ADDomainIP", GetType(System.String)), String)
        Dim search As String = DirectCast(configurationAppSettings.GetValue("ADSearch", GetType(System.String)), String)

        Ac = ADWrapper.ListUSers(domain, search, user, pass)

        Return Ac
    End Function

    Private Function GuardarUsuario(usuario As UsuarioDTO, accion As String) As Integer
        If accion = "INSERTAR" Then
            Return Datos.InsertUsuario(usuario)
        ElseIf accion = "UPDATE" Then
            Return Datos.UpdateUsuario(usuario)
        Else
            Return Constantes.CONST_ERROR_BBDD
        End If
    End Function

    Public Function UpdateUsuario(usuario As UsuarioDTO) As Integer
        Return GuardarUsuario(usuario, "UPDATE")
    End Function

    Public Function InsertUsuario(usuario As UsuarioDTO) As Integer
        Return GuardarUsuario(usuario, "INSERTAR")
    End Function

    Public Function DeleteUsuario(usuario As UsuarioDTO) As Integer
        Return Datos.DeleteUsuario(usuario)
    End Function

    Public Function ExportarAExcel(usuario As UsuarioDTO, FechaHasta As Nullable(Of Date)) As String
        If CrearExcel(Datos.UsuariosConsultarFiltro(usuario, FechaHasta)) Then
            If Excel.rutaArchivosExcel Is Nothing Then
                Return CONST_MENSAJE_EXCEL_ERROR
            Else
                Me.rutaArchivosExcel = Excel.rutaArchivosExcel
                Return CONST_MENSAJE_EXCEL_GUARDADO
            End If
        Else
            Return CONST_MENSAJE_EXCEL_ERROR
        End If
    End Function

    Public Function CrearExcel(ListaUsuarios As List(Of UsuarioDTO)) As Boolean
        Return Excel.CrearExcelUsuarios(ListaUsuarios)
    End Function

End Class
