Imports DTO
Imports Negocio
Imports DBSUtils


Partial Class Presentacion_Reportes_frmCuotasDCVVsGeneva
    Inherits System.Web.UI.Page

    Private Const CONST_NO_SE_ENCUENTRA_CONFIGURACION As String = "Nos e encuentra la configuracion de Pentaho para el informe"
    Private Const CONST_ID_PENTAHO As Integer = 2

    Private Sub Presentacion_Reportes_frmCuotasDCVVsGeneva_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            DataInitial()
        End If
        ValidaPermisosPerfil()
    End Sub

    Private Sub ValidaPermisosPerfil()

        If Session("PERFIL") = Constantes.CONST_PERFIL_CONSULTA Then

        ElseIf Session("PERFIL") = Constantes.CONST_PERFIL_FULL Or Session("PERFIL") = Constantes.CONST_PERFIL_ADMIN Then

        End If
    End Sub

    Private Sub DataInitial()
        txtFechaEjecucion.Text = Date.Now.Date.ToString("yyyy-MM-dd")
    End Sub

    Protected Sub btnGenerarInforme_Click(sender As Object, e As EventArgs) Handles btnGenerarInforme.Click
        Dim pentaho As ConfigPentahoDTO = New ConfigPentahoDTO()
        Dim negocio As ConfigPentahoNegocio = New ConfigPentahoNegocio

        Dim parErrores As String = ""
        Dim strFecha As String
        Dim parametros As String

        txtFechaEjecucion.Text = Request.Form(txtFechaEjecucion.UniqueID)

        strFecha = IIf(txtFechaEjecucion.Text = "", Date.Now.Date, CDate(txtFechaEjecucion.Text)).ToString("yyyy-MM-dd")

        parametros = "VAR_FECHA=" & strFecha

        pentaho.ID = CONST_ID_PENTAHO

        pentaho = negocio.GetPentahoPorId(pentaho)

        If pentaho.Code = Nothing Then
            ShowAlert(CONST_NO_SE_ENCUENTRA_CONFIGURACION)
        Else

            If pentahoutil.EjecutarETLParametrosAPI(pentaho, parametros, parErrores) Then
                ShowAlert(Constantes.CONST_EJECUTADO_CORRECTAMENTE)
            Else
                ShowAlert(parErrores)
            End If
        End If

    End Sub

    Private Sub ShowAlert(mesagge As String)
        Dim myScript As String = "alert('" + mesagge + "');"
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", myScript, True)
    End Sub
End Class
