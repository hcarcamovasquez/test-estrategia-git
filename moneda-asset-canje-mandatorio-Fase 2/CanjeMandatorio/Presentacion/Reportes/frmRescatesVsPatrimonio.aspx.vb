Imports DTO
Imports Negocio
Imports System.Net
Imports System.Net.Http
Imports DBSUtils

Partial Class Presentacion_Mantenedores_frmRescatesVsPatrimonio
    Inherits System.Web.UI.Page

    Private Const CONST_ID_PENTAHO As Integer = 1

    Private Sub Presentacion_Mantenedores_frmRescatesVsPatrimonio_Load(sender As Object, e As EventArgs) Handles Me.Load
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
        CargaFiltroNombre()
        ' txtFechaEjecucion.Text = Date.Now.ToString("dd/MM/yyyy")
    End Sub

    Private Sub CargaFiltroNombre()
        Dim fondo As New FondoDTO()
        Dim FondosNegocio As FondosNegocio = New FondosNegocio
        Dim lista As List(Of FondoDTO) = FondosNegocio.ConsultarPorVentana(fondo)

        If lista.Count = 0 Then
            ddlListaRutFondo.Items.Insert(0, New ListItem("", ""))
        Else

            ddlListaRutFondo.DataSource = lista
            ddlListaRutFondo.DataMember = "RutRazonSocial"
            ddlListaRutFondo.DataValueField = "RutRazonSocial"
            ddlListaRutFondo.DataTextField = "RutRazonSocial"
            ddlListaRutFondo.DataBind()
            ddlListaRutFondo.Items.Insert(0, New ListItem("", ""))
        End If
    End Sub

    Protected Sub BtnBuscar_Click(sender As Object, e As EventArgs) Handles BtnBuscar.Click
        Dim negocio As EjecucionRescateVsPatrimonioNegocio = New EjecucionRescateVsPatrimonioNegocio()
        Dim ejecucionDto As EjecucionRescateVsPatrimonioDTO = New EjecucionRescateVsPatrimonioDTO()
        Dim lista As List(Of EjecucionRescateVsPatrimonioDTO)
        Dim strMensajeAlert As String = ""

        txtFechaEjecucion.Text = Request.Form(txtFechaEjecucion.UniqueID)
        txtFechaNav.Text = Request.Form(txtFechaNav.UniqueID)
        txtFechaDesde.Text = Request.Form(txtFechaDesde.UniqueID)
        txtFechaHasta.Text = Request.Form(txtFechaHasta.UniqueID)

        ejecucionDto.FechaEjecucion = IIf(txtFechaEjecucion.Text = "", Nothing, txtFechaEjecucion.Text)

        If ddlListaRutFondo.SelectedValue = "" Then
            ejecucionDto.FnRut = Nothing
        Else
            ejecucionDto.FnRut = ddlListaRutFondo.SelectedValue.Split("/")(0)
        End If


        If txtFechaHasta.Text = "" Then
            strMensajeAlert = "Debe Seleccionar la Fecha Hasta"
        Else
            ejecucionDto.Fechahasta = CDate(txtFechaHasta.Text).ToString("yyyy-MM-dd")
        End If

        If txtFechaDesde.Text = "" Then
            strMensajeAlert = "Debe Seleccionar la Fecha Desde"
        Else
            ejecucionDto.FechaDesde = CDate(txtFechaDesde.Text).ToString("yyyy-MM-dd")

        End If

        If strMensajeAlert = "" Then
            lista = negocio.Select(ejecucionDto)

            If lista.Count() > 0 Then
                grvEjecuciones.DataSource = lista
                grvEjecuciones.DataBind()
                strMensajeAlert = ""
            Else
                grvEjecuciones.DataSource = Nothing
                grvEjecuciones.DataBind()
                strMensajeAlert = Constantes.CONST_SIN_RESULTADOS
            End If

        End If

        If strMensajeAlert <> "" Then
            ShowAlert(strMensajeAlert)
        End If

    End Sub

    Private Sub ShowAlert(mesagge As String)
        Dim myScript As String = "alert('" + mesagge + "');"
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", myScript, True)
    End Sub

    Protected Sub btnGenerarInforme_Click(sender As Object, e As EventArgs) Handles btnGenerarInforme.Click
        Dim pentaho As ConfigPentahoDTO = New ConfigPentahoDTO()
        Dim negocio As ConfigPentahoNegocio = New ConfigPentahoNegocio

        Dim parErrores As String = ""
        Dim strFecha As String
        Dim strFechaPatrimonio As String = ""
        Dim strFechaNav As String = ""
        Dim strRutFondo As String = ""
        Dim parametros As String = ""
        Dim FormatoParam As String

        Dim strMensajeAlert As String = ""

        txtFechaEjecucion.Text = Request.Form(txtFechaEjecucion.UniqueID)
        txtFechaPatrimonio.Text = Request.Form(txtFechaPatrimonio.UniqueID)
        txtFechaNav.Text = Request.Form(txtFechaNav.UniqueID)


        If txtFechaEjecucion.Text = "" Then
            strFecha = Date.Now.Date.ToString("yyyy-MM-dd")
        Else
            strFecha = CDate(txtFechaEjecucion.Text).ToString("yyyy-MM-dd")
        End If

        If txtFechaPatrimonio.Text <> "" Then
            strFechaPatrimonio = CDate(txtFechaPatrimonio.Text).ToString("yyyy-MM-dd")
        End If

        If txtFechaNav.Text <> "" Then
            strFechaNav = CDate(txtFechaNav.Text).ToString("yyyy-MM-dd")
        End If


        If ddlListaRutFondo.SelectedValue = "" Then
            strRutFondo = Nothing
        Else
            strRutFondo = ddlListaRutFondo.SelectedValue.Split("/")(0)
        End If

        If strMensajeAlert = "" Then
            'Seteo Parametros 
            FormatoParam = "FECHA_PAGO={0}&FONDO_RUT={1}&FECHA_PATRIMONIO={2}&FECHA_NAV={3}"
            parametros = String.Format(FormatoParam, strFecha, strRutFondo, strFechaPatrimonio, strFechaNav)

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
                    Else
                        strMensajeAlert = parErrores
                    End If
                End If
            End If
        End If

        ShowAlert(strMensajeAlert)

    End Sub

    Protected Sub btnLimpiarFrm_Click(sender As Object, e As EventArgs) Handles btnLimpiarFrm.Click
        grvEjecuciones.DataSource = Nothing
        grvEjecuciones.DataBind()

        ddlListaRutFondo.SelectedIndex = -1
        txtFechaEjecucion.Text = ""
        txtFechaPatrimonio.Text = ""
        txtFechaNav.Text = ""
        txtFechaDesde.Text = ""
        txtFechaHasta.Text = ""
        txtAccionHidden.Value = ""
    End Sub
End Class
