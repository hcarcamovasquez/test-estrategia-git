Imports DTO
Imports Negocio
Imports DBSUtils


Partial Class Presentacion_Reportes_frmCuotasDCVVsGeneva
    Inherits System.Web.UI.Page


    Private Const CONST_TITULO_REPORTE As String = "Control DCV vs Geneva"
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
        txtFechaEjecucion.Text = Date.Now.Date.ToString("dd-MM-yyyy")
    End Sub

    Protected Sub btnGenerarInforme_Click(sender As Object, e As EventArgs) Handles btnGenerarInforme.Click
        Dim lista As List(Of ReporteDcvVsGenevaDTO) = New List(Of ReporteDcvVsGenevaDTO)

        txtFechaEjecucion.Text = Request.Form(txtFechaEjecucion.UniqueID)

        lista = TraerUltimoInformeGeneva()

        If lista.Count() > 0 Then
            grvEjecuciones.DataSource = lista
            grvEjecuciones.DataBind()
            BtnExportar.Enabled = True
        Else
            CorrerPentaho()
            BtnExportar.Enabled = False
        End If

    End Sub

    Private Function TraerUltimoInformeGeneva() As List(Of ReporteDcvVsGenevaDTO)
        Dim lista As List(Of ReporteDcvVsGenevaDTO) = New List(Of ReporteDcvVsGenevaDTO)
        Dim informeGeneva As ReporteDcvVsGenevaDTO = New ReporteDcvVsGenevaDTO()

        Dim negocio As ReporteDcvVsGenevaNegocio = New ReporteDcvVsGenevaNegocio()

        Dim strFecha As String

        If txtFechaEjecucion.Text = "" Then
            strFecha = Date.Now.Date.ToString("yyyy-MM-dd")
        Else
            strFecha = CDate(txtFechaEjecucion.Text).ToString("yyyy-MM-dd")
        End If
        informeGeneva.Fecha_DCV = strFecha

        lista = negocio.GetInformeGeneva(informeGeneva)

        Return lista
    End Function

    Private Function CorrerPentaho() As Boolean
        Dim pentaho As ConfigPentahoDTO = New ConfigPentahoDTO()
        Dim negocio As ConfigPentahoNegocio = New ConfigPentahoNegocio

        Dim parErrores As String = ""
        Dim strFecha As String
        Dim parametros As String

        Dim strMensajeAlert As String = ""
        Dim retorno As Boolean = False

        If txtFechaEjecucion.Text = "" Then
            strFecha = Date.Now.Date.ToString("yyyy-MM-dd")
        Else
            strFecha = CDate(txtFechaEjecucion.Text).ToString("yyyy-MM-dd")
        End If

        parametros = "VAR_FECHA=" & strFecha

        pentaho.ID = CONST_ID_PENTAHO

        pentaho = negocio.GetPentahoPorId(pentaho)

        If pentaho.Code = Nothing Then
            strMensajeAlert = Constantes.CONST_PENTAHO_NO_SE_ENCUENTRA_CONFIGURACION
        Else
            pentaho.API_Parametros = parametros
            If PentahoUtil.IsJobRunning(pentaho, parErrores) Then
                strMensajeAlert = String.Format(Constantes.CONST_PENTAHO_EJECUTANDOSE, pentaho.API_JobName)
            Else
                If parErrores.Equals("") AndAlso PentahoUtil.EjecutarETLParametrosAPI(pentaho, parErrores) Then
                    strMensajeAlert = Constantes.CONST_PENTAHO_EJECUTADO_CORRECTAMENTE
                    retorno = True
                Else
                    strMensajeAlert = parErrores
                End If
            End If
        End If
        If Not retorno Then
            ShowAlert(strMensajeAlert)
        End If

        Return retorno
    End Function

    Private Sub ShowAlert(mesagge As String)
        Dim myScript As String = "alert('" + mesagge + "');"
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", myScript, True)
    End Sub
    Protected Sub BtnExportar_Click(sender As Object, e As EventArgs) Handles BtnExportar.Click
        Dim negocio As ReporteDcvVsGenevaNegocio = New ReporteDcvVsGenevaNegocio()
        Dim mensaje As String = ""
        Dim reporteDCV As ReporteDcvVsGenevaDTO = New ReporteDcvVsGenevaDTO()
        Dim strFecha As String

        If txtFechaEjecucion.Text = "" Then
            strFecha = Date.Now.Date.ToString("yyyy-MM-dd")
        Else
            strFecha = CDate(txtFechaEjecucion.Text).ToString("yyyy-MM-dd")
        End If

        reporteDCV.Fecha_DCV = strFecha
        mensaje = negocio.ExportarAExcel(reporteDCV)

        If negocio.rutaArchivosExcel IsNot Nothing Then
            Archivo.NavigateUrl = negocio.rutaArchivosExcel
            Archivo.Text = "Bajar Archivo"
        Else
            Archivo.Visible = False
        End If

        txtAccionHidden.Value = "MOSTRAR_DIALOGO"

        ShowMessages(CONST_TITULO_REPORTE, mensaje, Constantes.CONST_RUTA_IMG + Constantes.CONST_IMG_LOGO, Constantes.CONST_RUTA_IMG + Constantes.CONST_IMG_INFO, False)
    End Sub

    Private Sub ShowMessages(tittle As String, message As String, urlconTittle As String, urlconMessage As String, Optional borralink As Boolean = True)
        lblModalTitle.Text = tittle
        lblModalBody.Text = message
        img_modal.ImageUrl = urlconTittle
        img_body_modal.ImageUrl = urlconMessage
        Archivo.Visible = (Not borralink)
    End Sub

    Protected Sub btnLimpiarFrm_Click(sender As Object, e As EventArgs) Handles btnLimpiarFrm.Click
        grvEjecuciones.DataSource = Nothing
        grvEjecuciones.DataBind()

        txtFechaEjecucion.Text = ""

        BtnExportar.Enabled = False
        txtAccionHidden.Value = ""
    End Sub

End Class
