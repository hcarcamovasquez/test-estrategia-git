Imports DTO
Imports Negocio.UsuarioNegocio
Imports Negocio.MonedaNegocio


Partial Class _Default
    Inherits System.Web.UI.Page


    Protected Sub btnInciarSesion_Click(sender As Object, e As EventArgs)
        Dim usuarioNegocio As Negocio.UsuarioNegocio = New Negocio.UsuarioNegocio
        Dim usuario As UsuarioDTO = New UsuarioDTO()
        Dim usuarioObj As UsuarioDTO = New UsuarioDTO()
        Dim user As String = ""
        Dim pass As String = ""
        Dim loginCorrecto As Boolean = False ' CAMBIAR A FALSE PARA PROBAR EL LOGIN CON AD
        user = txtUser.Text
        pass = txtPassword.Text

        If validaCamposUsuario() Then
            usuario.US_Nombre = user

            usuarioObj = usuarioNegocio.GetUsuarioPorNombre(usuario)

            If usuarioObj.US_Nombre IsNot Nothing Then
                If usuarioObj.US_Estado = Constantes.CONST_HABILITADO Then
                    'Usuario está activo y la contraseña es la misma de ActiveDirectory
                    If usuarioNegocio.IsUserValidInActiveDirectory(user, pass) Then
                        Session("PERFIL") = usuarioObj.US_Perfil
                        Session("NombreUsuario") = usuarioObj.US_Nombre
                        Session("pass") = pass

                        loginCorrecto = True
                    Else
                        loginCorrecto = False
                    End If
                Else
                    loginCorrecto = False
                End If
            ElseIf usuarioObj.US_Estado = Constantes.CONST_DESHBILITADO Then
                    loginCorrecto = False
            End If

            If loginCorrecto Then
                Response.Redirect("~/Presentacion/Mantenedores/frmMantenedorAportantes.aspx")
            Else
                lblMensaje.Text = "Usuario o contraseña incorrectos"
            End If
        Else
            lblMensaje.Text = "Debe Ingresar el nombre de usuario o la contraseña"
        End If
    End Sub

    Private Function validaCamposUsuario() As Boolean
        Return (txtUser.Text.Trim() <> "" And txtPassword.Text.Trim <> "")
    End Function

End Class
