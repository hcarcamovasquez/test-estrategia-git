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
        Dim lista As List(Of FondoDTO) = FondosNegocio.ConsultarPorRut(fondo)

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

        txtFechaEjecucion.Text = Request.Form(txtFechaEjecucion.UniqueID)

        ejecucionDto.FnRut = IIf(ddlListaRutFondo.SelectedValue = "", Nothing, ddlListaRutFondo.SelectedValue)
        ejecucionDto.FechaEjecucion = IIf(txtFechaEjecucion.Text = "", Nothing, txtFechaEjecucion.Text)

        lista = negocio.Select(ejecucionDto)
        If lista.Count() > 0 Then
            grvEjecuciones.DataSource = lista
            grvEjecuciones.DataBind()
        Else
            ShowAlert(Constantes.CONST_SIN_RESULTADOS)
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
        Dim parametros As String

        txtFechaEjecucion.Text = Request.Form(txtFechaEjecucion.UniqueID)

        If txtFechaEjecucion.Text = "" Then
            strFecha = Date.Now.Date.ToString("yyyy-MM-dd")
        Else
            strFecha = CDate(txtFechaEjecucion.Text).ToString("yyyy-MM-dd")
        End If

        'Set Parametros 
        parametros = "FECHA_PAGO=" & strFecha

        pentaho.ID = CONST_ID_PENTAHO
        pentaho = negocio.GetPentahoPorId(pentaho)

        If pentaho.Code = Nothing Then
            ShowAlert(Constantes.CONST_NO_SE_ENCUENTRA_CONFIGURACION)
        Else
            pentaho.API_Parametros = parametros

            If PentahoUtil.EjecutarETLParametrosAPI(pentaho, parErrores) Then
                ShowAlert(Constantes.CONST_EJECUTADO_CORRECTAMENTE)
            Else
                ShowAlert(parErrores)
            End If
        End If
    End Sub

    Protected Sub btnLimpiarFrm_Click(sender As Object, e As EventArgs) Handles btnLimpiarFrm.Click
        grvEjecuciones.DataSource = Nothing
        grvEjecuciones.DataBind()

        ddlListaRutFondo.SelectedIndex = -1
        txtFechaEjecucion.Text = ""
        txtAccionHidden.Value = ""
    End Sub
End Class
