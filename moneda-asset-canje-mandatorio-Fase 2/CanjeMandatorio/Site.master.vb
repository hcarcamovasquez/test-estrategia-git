Partial Class Site
    Inherits System.Web.UI.MasterPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If HttpContext.Current.Session("NombreUsuario") = Nothing Then
            Response.Redirect("~/Default.aspx", True)
        End If
    End Sub
    Protected Sub btnCerrarSesion_Click(sender As Object, e As EventArgs)
        HttpContext.Current.Session.Clear()
        Response.Redirect("~/Default.aspx", True)
    End Sub
End Class
