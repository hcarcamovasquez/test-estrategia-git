Imports DTO
Imports Negocio
Imports DBSUtils

Partial Class Presentacion_Reportes_frmReporteControlCuotasEmitidas
    Inherits System.Web.UI.Page

    Private Const CONST_ID_PENTAHO As Integer = 3
    Private Const CONST_TITULO_REPORTE As String = "Control de Cuotas Emitidas"

    Private Sub Presentacion_Reportes_frmReporteControlCuotasEmitidas_Load(sender As Object, e As EventArgs) Handles Me.Load
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
        llenarComboNemotecnico()
        BtnExportar.Enabled = False
        txtAccionHidden.Value = ""
    End Sub

    Protected Sub btnGenerarInforme_Click(sender As Object, e As EventArgs) Handles btnGenerarInforme.Click
        Dim negocioReporte As ReporteControlCuotasEmitidasNegocio = New ReporteControlCuotasEmitidasNegocio()
        Dim controlCuota As ReporteControlCuotasEmitidasDTO = New ReporteControlCuotasEmitidasDTO()
        Dim listaCuotas As List(Of ReporteControlCuotasEmitidasDTO) = New List(Of ReporteControlCuotasEmitidasDTO)

        Try
            controlCuota.FsNemotecnico = IIf(ddlNemotecnico.SelectedValue.Trim() = "", Nothing, ddlNemotecnico.SelectedValue)

            listaCuotas = negocioReporte.Select(controlCuota)

            BtnExportar.Enabled = (listaCuotas.Count() > 0)

            If listaCuotas.Count() > 0 Then
                grvReporte.DataSource = listaCuotas
                grvReporte.DataBind()

            Else
                ShowAlert(Constantes.CONST_SIN_RESULTADOS)
            End If

        Catch ex As Exception
            Throw ex
        Finally
            negocioReporte = Nothing
            controlCuota = Nothing
        End Try
    End Sub

    Private Sub ShowAlert(mesagge As String)
        Dim myScript As String = "alert('" + mesagge + "');"
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", myScript, True)
    End Sub

    Private Sub llenarComboNemotecnico()
        Dim listaNemo As List(Of FondoSerieDTO)
        Dim fondoSeriesVacia As FondoSerieDTO = New FondoSerieDTO
        Dim fondoParam As FondoSerieDTO = New FondoSerieDTO
        Dim negocio As FondoSeriesNegocio = New FondoSeriesNegocio

        Try
            listaNemo = negocio.GetFondoSerie(fondoParam)

            ddlNemotecnico.Items.Clear()

            If listaNemo.Count = 0 Then
                listaNemo.Add(fondoSeriesVacia)
            Else
                listaNemo.Insert(0, fondoSeriesVacia)
            End If

            ddlNemotecnico.Items.Clear()

            ddlNemotecnico.DataSource = listaNemo
            ddlNemotecnico.DataMember = "NemotecnicoBusqueda"
            ddlNemotecnico.DataValueField = "NemotecnicoBusqueda"
            ddlNemotecnico.DataBind()
        Catch ex As Exception
            Throw ex
        Finally
            fondoSeriesVacia = Nothing
            fondoParam = Nothing
            negocio = Nothing
        End Try
    End Sub
    Protected Sub btnLimpiarFrm_Click(sender As Object, e As EventArgs) Handles btnLimpiarFrm.Click
        grvReporte.DataSource = Nothing
        grvReporte.DataBind()

        ddlNemotecnico.SelectedIndex = -1

        BtnExportar.Enabled = False
        txtAccionHidden.Value = ""
    End Sub


    Protected Sub BtnExportar_Click(sender As Object, e As EventArgs) Handles BtnExportar.Click
        Dim negocio As ReporteControlCuotasEmitidasNegocio = New ReporteControlCuotasEmitidasNegocio()
        Dim mensaje As String = ""
        Dim controlCuota As ReporteControlCuotasEmitidasDTO = New ReporteControlCuotasEmitidasDTO()

        controlCuota.FsNemotecnico = IIf(ddlNemotecnico.SelectedValue.Trim() = "", Nothing, ddlNemotecnico.SelectedValue)

        mensaje = negocio.ExportarAExcel(controlCuota)

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
End Class
