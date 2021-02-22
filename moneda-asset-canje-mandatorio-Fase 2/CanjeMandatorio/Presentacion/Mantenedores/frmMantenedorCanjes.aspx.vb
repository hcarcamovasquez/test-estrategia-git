Imports DTO
Imports Negocio
Imports System.Data
Imports DBSUtils

Partial Class Presentacion_Mantenedores_frmMantenedorCanjes
    Inherits System.Web.UI.Page

    Private ReadOnly NegocioCanje As CanjeNegocio = New CanjeNegocio
    Private ReadOnly NegocioFondoSerie As FondoSeriesNegocio = New FondoSeriesNegocio
    Private ReadOnly NegocioAportante As AportanteNegocio = New AportanteNegocio

    Public Const CONST_TITULO_CANJE As String = "Canje"
    Public Const CONST_TITULO_MODAL_MODIFICAR As String = "Modificar Canje"
    Public Const CONST_MODIFICAR_ERROR As String = "Error al modificar los datos del Canje"
    Public Const CONST_MODIFICAR_EXITO As String = "Canje modificado con Éxito"
    Public Const CONST_ELIMINAR_ERROR As String = "Error al eliminar el canje seleccionado"
    Public Const CONST_ELIMINAR_EXITO As String = "Canje eliminado con Éxito"
    Public Const CONST_TITULO_MODAL_ELIMINAR As String = "Eliminar Canje"
    Public Const CONST_TITULO_MODAL_CREAR As String = "Nuevo Canje"
    Public Const CONST_ERROR_AL_GUARDAR As String = "Error al guardar el Canje"
    Public Const CONST_EXITO_AL_GUARDAR As String = "Canje guardado con Éxito"
    Public Const CONST_EXITO_AL_MODIFICAR As String = "Canje modificado con Éxito"
    Public Const CONST_ERROR_AL_MODIFICAR As String = "Error al modificar Canje"
    Public Const CONST_SIN_RESULTADOS As String = "No se obtuvieron resultados de la búsqueda"
    Public Const CONST_NAV_ENTRANTE_ESCRIBIR As String = "Las Cuotas Disponibles no pueden ser menores que las Cuotas Salientes"
    Public Const CONST_NAV_SALIENTE_ESCRIBIR As String = "Debe escribir un Nav Saliente"
    Public Const CONST_FECHANAV_MANUAL As String = "Debe seleccionar una Fecha Nav"
    Public Const CONST_FECHAOBSERVADO As String = "Debe seleccionar una Fecha TC Observado"

    Public Const CONST_COL_ID_CANJE As Integer = 1
    Public Const CONST_COL_TIPO_TRANSACCION As Integer = 2
    Public Const CONST_COL_RUT_APORTANTE As Integer = 3
    Public Const CONST_COL_NOMBRE_APORTANTE As Integer = 4
    Public Const CONST_COL_MULTIFONDO As Integer = 5
    Public Const CONST_COL_RUT_FONDO As Integer = 6
    Public Const CONST_COL_NOMBRE_FONDO As Integer = 7
    Public Const CONST_COL_FECHA_SOLICITUD As Integer = 8
    Public Const CONST_COL_FECHA_OBSERVADO As Integer = 9
    Public Const CONST_COL_FECHA_CANJE As Integer = 10
    Public Const CONST_COL_FECHA_NAV_SALIENTE As Integer = 11
    Public Const CONST_COL_NEMOTECNICO_SALIENTE As Integer = 12
    Public Const CONST_COL_SERIE_SALIENTE As Integer = 13
    Public Const CONST_COL_MONEDA_SALIENTE As Integer = 14
    Public Const CONST_COL_CUOTAS_SALIENTE As Integer = 15
    Public Const CONST_COL_NAV_SALIENTE As Integer = 16
    Public Const CONST_COL_MONTO_SALIENTE As Integer = 17
    Public Const CONST_COL_NAVCLP_SALIENTE As Integer = 18
    Public Const CONST_COL_MONTOCLP_SALIENTE As Integer = 19
    Public Const CONST_COL_FACTOR As Integer = 20
    Public Const CONST_COL_DIFERENCIA As Integer = 21
    Public Const CONST_COL_DIFERENCIA_CLP As Integer = 22
    Public Const CONST_COL_FECHA_NAV_ENTRANTE As Integer = 23
    Public Const CONST_COL_NEMOTECNICO_ENTRANTE As Integer = 24
    Public Const CONST_COL_SERIE_ENTRANTE As Integer = 25
    Public Const CONST_COL_MONEDA_ENTRANTE As Integer = 26
    Public Const CONST_COL_CUOTA_ENTRANTE As Integer = 27
    Public Const CONST_COL_NAV_ENTRANTE As Integer = 28
    Public Const CONST_COL_MONTO_ENTRANTE As Integer = 29
    Public Const CONST_COL_NAVCLP_ENTRANTE As Integer = 30
    Public Const CONST_COL_MONTOCLP_ENTRANTE As Integer = 31
    Public Const CONST_COL_CONTRATO As Integer = 32
    Public Const CONST_COL_PODERES As Integer = 33
    Public Const CONST_COL_ESTADO_CANJE As Integer = 34
    Public Const CONST_COL_OBSERVACIONES As Integer = 35
    Public Const CONST_COL_FECHA_DCV As Integer = 36
    Public Const CONST_COL_CUOTA_DCV As Integer = 37
    Public Const CONST_COL_RESCATE_TRANSITO As Integer = 38
    Public Const CONST_COL_SUSCRIPCION_TRANSITO As Integer = 39
    Public Const CONST_COL_CANJE_TRANSITO As Integer = 40
    Public Const CONST_COL_CUOTAS_DISPONIBLES As Integer = 41
    Public Const CONST_COL_FIJACION_NAV As Integer = 42
    Public Const CONST_COL_FIJACION_TC As Integer = 43
    Public Const CONST_COL_TIPO_CAMBIO As Integer = 44

    Public Const CONST_INHABIL_PARA_TC As String = "La fecha TC es inhábil en la moneda. Se moverá al hábil siguiente"

    Private Sub Presentacion_Mantenedores_frmMantenedorFondoSerie_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            DataInitial()
        End If
    End Sub

    Private Sub ValidaPermisosPerfil()
        HiddenPerfil.Value = Session("PERFIL")
        HiddenConstante.Value = Constantes.CONST_PERFIL_CONSULTA

        If Session("PERFIL") = (Constantes.CONST_PERFIL_CONSULTA Or Nothing) Then
            btnCrear.Enabled = False
            BtnModificar.Enabled = False
            BtnEliminar.Enabled = False
            BtnExportar.Enabled = False

        ElseIf Session("PERFIL") = (Constantes.CONST_PERFIL_FULL Or Constantes.CONST_PERFIL_ADMIN) Then
            btnCrear.Visible = True
            BtnModificar.Visible = True
            BtnEliminar.Visible = True
            BtnExportar.Visible = True

        End If
    End Sub

    Private Sub DataInitial()
        ddlEstado.Items.Clear()

        cargaFiltroAportanteBusqueda()
        cargaFiltroFondoBusqueda()
        cargaFiltroNemotecnicoBusqueda()

        cargarFiltroRutAportanteModal()
        cargarFiltroNombreAportanteModal()
        cargarFiltroMultifondoAportanteModal()
        cargaFiltroRutModal()
        cargaFiltroEstado()
        cargaFiltroNombreRutModal()
        cargaFiltroNemotecnicoSalienteEntranteModal()
        cargaFiltroNombreSerieSalienteEntranteModal()
        cargaFiltroMonedaSerieSalienteEntranteModal()

        GrvTabla.DataSource = New List(Of CanjeDTO)
        GrvTabla.DataBind()
        cargarIdCanje()
        BtnExportar.Enabled = (GrvTabla.Rows.Count <> 0)
        'txtAccionHidden.Value = ""
        ValidaPermisosPerfil()

        txtModalFSolicitud.Text = Date.Now().ToShortDateString()
        txtModalFechaCuotaDCV.Text = Date.Now().ToShortDateString()

        ddlModalEstado.SelectedValue = "Pendiente"
        txtFechaNavDesde.Text = ""
        txtFechaNavHasta.Text = ""
        txtFechaSolicitudDesde.Text = ""
        txtFechaSolicitudHasta.Text = ""
        ddlEstado.SelectedValue = ""
    End Sub

    Private Sub FormateoLimpiarDatosModal()
        cargarFiltroRutAportanteModal()
        cargarFiltroNombreAportanteModal()
        cargarFiltroMultifondoAportanteModal()
        cargaFiltroRutModal()
        cargaFiltroNombreRutModal()
        cargaFiltroNemotecnicoSalienteEntranteModal()
        cargaFiltroNombreSerieSalienteEntranteModal()
        cargaFiltroMonedaSerieSalienteEntranteModal()
        ddlModalNombreAportante.SelectedIndex = 0
        ddlModalMultifondo.SelectedIndex = 0
        ddlModalFondo.Text = ""
        ddlModalNombreFondo.SelectedIndex = 0
        txtModalFechaObservado.Text = ""
        ddlModalFijacionTC.SelectedIndex = 0
        ddlModalNemotecnicoSaliente.SelectedIndex = 0
        txtModalFechaNavSaliente.Text = ""
        ddlModalSerieSaliente.SelectedIndex = 0
        ddlModalMonedaSaliente.SelectedIndex = 0
        txtModalCuotaSaliente.Text = ""
        txtFactorSaliente.Text = ""
        txtModalNavSaliente.Text = ""
        txtModalNavCLPSaliente.Text = ""
        txtModalMontoSaliente.Text = ""
        txtModalMontoCLPSaliente.Text = ""
        txtModalDiferencia.Text = ""
        txtModalDiferenciaCLP.Text = ""
        ddlModalNemotecnicoEntrante.SelectedIndex = 0
        txtModalFechaNavEntrante.Text = ""
        ddlModalSerieEntrante.SelectedIndex = 0
        ddlModalMonedaEntrante.SelectedIndex = 0
        txtModalCuotaEntrante.Text = ""
        txtModalFactor.Text = ""
        txtModalNavEntrante.Text = ""
        txtModalNavCLPEntrante.Text = ""
        txtModalMontoEntrante.Text = ""
        txtModalMontoCLPEntrante.Text = ""
        ddlModalFijacionNav.SelectedIndex = 0
        txtModalCuotaDCV.Text = ""
        txtModalRescateTransito.Text = ""
        txtModalCanjeTransito.Text = ""
        txtModalSuscripcionTransito.Text = ""
        txtModalCuotasDisponibles.Text = ""
        txtModalTipoCambio.Text = ""
        ddlModalContrato.SelectedIndex = 0
        ddlModalPoderes.SelectedIndex = 0
        txtModalObservaciones.Text = ""
        txtModalFSolicitud.Text = Date.Now().ToShortDateString()
        txtModalFechaCanje.Text = ""

    End Sub

    Protected Sub btnLimpiarFrm_Click(sender As Object, e As EventArgs)
        DataInitial()
        txtAccionHidden.Value = ""
    End Sub

    Protected Sub BtnBuscar_Click(sender As Object, e As EventArgs) Handles BtnBuscar.Click
        FindCanje()

        BtnModificar.Enabled = False
        If GrvTabla.Rows.Count <> 0 Then
            BtnExportar.Enabled = True
        Else
            BtnExportar.Enabled = False
            ShowAlert(CONST_SIN_RESULTADOS)
        End If
        txtAccionHidden.Value = ""
    End Sub

    Private Sub ShowMessages(tittle As String, message As String, urlconTittle As String, urlconMessage As String, Optional borralink As Boolean = True)
        lblModalTitle.Text = tittle
        lblModalBody.Text = message
        img_modal.ImageUrl = urlconTittle
        img_body_modal.ImageUrl = urlconMessage
        Archivo.Visible = (Not borralink)
    End Sub

    Private Function cargarIdCanje() As CanjeDTO
        Dim canje As New CanjeDTO
        Dim negocio As CanjeNegocio = New CanjeNegocio

        Dim CanjeActualizado As CanjeDTO = negocio.UltimoCanje(canje)
        txtIdCanje.Text = CanjeActualizado.IdCanje + 1
        Return canje
    End Function

    Protected Sub RowSelector_CheckedChanged(sender As Object, e As EventArgs)
        For Each row As GridViewRow In GrvTabla.Rows
            Dim chk As RadioButton = row.Cells(0).Controls(1)
            If chk IsNot Nothing And chk.Checked Then
            End If
        Next
    End Sub

    Private Sub ShowAlert(mesagge As String)
        Dim myScript As String = "alert('" + mesagge + "');"
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", myScript, True)
    End Sub

    Private Sub btnModalModificar_Click(sender As Object, e As EventArgs) Handles btnModalModificar.Click
        If (verificarcampos()) Then
            Dim canje As CanjeDTO = GetCanje()
            Dim solicitud As Integer = NegocioCanje.UpdateCanje(canje)

            If solicitud = Constantes.CONST_OPERACION_EXITOSA Then
                ShowAlert(CONST_EXITO_AL_MODIFICAR)
                'TODO: DESCOMENTAR
                GenerarPopUp()
            Else
                ShowAlert(CONST_ERROR_AL_MODIFICAR)
            End If

            DataInitial()
        End If
    End Sub
    Public Sub tipoCambioChanged()
        validarTipoCambio()
        If (txtModalTipoCambio.Text <> "" And IsNumeric(txtModalTipoCambio.Text)) Then
            If (Double.Parse(txtModalTipoCambio.Text) < 99999999999999) Then
                ConversionMoneda()
            Else
                ShowAlert("El valor de tipo de cambio ingresado excede el límite permitido")
            End If
        End If
    End Sub
    Public Sub validarTipoCambio()
        Dim contador As Integer = 0
        If (txtModalTipoCambio.Text <> "") Then
            For s As Integer = 0 To txtModalTipoCambio.Text.Length - 1
                If (txtModalTipoCambio.Text.Chars(s) = ",") Then
                    contador = contador + 1
                End If
            Next
            If contador > 1 Or txtModalTipoCambio.Text.Chars(0) = "," Then
                ShowAlert("Tipo cambio no tiene un formato correcto, por favor verifique")
                txtModalTipoCambio.Text = ""
            End If
        End If
    End Sub
    Protected Sub BtnModificar_Click(sender As Object, e As EventArgs)
        cargarFiltroRutAportanteModal()
        cargarFiltroNombreAportanteModal()
        cargarFiltroMultifondoAportanteModal()
        cargaFiltroRutModal()
        cargaFiltroNombreRutModal()
        cargaFiltroNemotecnicoSalienteEntranteModal()
        cargaFiltroNombreSerieSalienteEntranteModal()
        cargaFiltroMonedaSerieSalienteEntranteModal()

        Dim negocio As CanjeNegocio = New CanjeNegocio
        Dim CanjeSelect As CanjeDTO = GetCanjeSelect()
        Dim aportante As AportanteDTO = New AportanteDTO()

        aportante.Rut = CanjeSelect.RutAportante
        If CanjeSelect.Multifondo = "&nbsp;" Then
            aportante.Multifondo = String.Empty
        Else
            aportante.Multifondo = CanjeSelect.Multifondo
        End If

        Dim listaAportante As List(Of AportanteDTO) = negocio.CompararDatosAportantes(aportante)

        Dim serieEntrante As FondoSerieDTO = New FondoSerieDTO()
        serieEntrante.Rut = CanjeSelect.RutFondo
        serieEntrante.Nemotecnico = CanjeSelect.NemotecnicoEntrante
        Dim listaEntrantes As List(Of FondoSerieDTO) = negocio.CompararDatosEntrantes(serieEntrante)

        Dim serieSaliente As FondoSerieDTO = New FondoSerieDTO()
        serieSaliente.Rut = CanjeSelect.RutFondo
        serieSaliente.Nemotecnico = CanjeSelect.NemotecnicoSaliente
        Dim listaSaliente As List(Of FondoSerieDTO) = negocio.CompararDatosSalientes(serieSaliente)

        If listaAportante.Count > 0 Then
            For Each aportantes As AportanteDTO In listaAportante
                Dim razonSocial = aportantes.RazonSocial
                Dim estado = aportantes.Estado
                If estado = 0 Then
                    ShowAlert("No se puede modificar el canje, información del Aportante se modifico")
                    DataInitial()
                    Exit Sub
                ElseIf listaEntrantes.Count > 0 Then
                    For Each entrante As FondoSerieDTO In listaEntrantes
                        Dim serie = entrante.Nombrecorto
                        Dim moneda = entrante.Moneda
                        Dim estadoEntrante = entrante.Estado
                        If estadoEntrante = 0 Then
                            ShowAlert("No se puede modificar el canje, Serie entrante modificada")
                            DataInitial()
                            Exit Sub
                        ElseIf listaSaliente.Count > 0 Then
                            For Each saliente As FondoSerieDTO In listaSaliente
                                Dim serieSaliente2 = saliente.Nombrecorto
                                Dim monedaSaliente = saliente.Moneda
                                Dim estadoSaliente = saliente.Estado
                                If estadoSaliente = 0 Then
                                    ShowAlert("No se puede modificar el canje, Serie saliente modificada ")
                                    DataInitial()
                                    Exit Sub
                                ElseIf CanjeSelect.NombreAportante = razonSocial And CanjeSelect.NombreSerieEntrante = serie And CanjeSelect.MonedaEntrante = moneda And CanjeSelect.NombreSerieSaliente = serieSaliente2 And CanjeSelect.MonedaSaliente = monedaSaliente And estado = 1 And estadoEntrante = 1 And estadoSaliente = 1 Then
                                    Dim CanjeActualizado As CanjeDTO = negocio.GetCanje(CanjeSelect)
                                    txtAccionHidden.Value = "MODIFICAR"
                                    cargaFiltroNemotecnicoSalienteEntranteModal()
                                    FormateoEstiloFormModificar()
                                    FormateoFormDatos(CanjeSelect)
                                    cargaFiltroNemotecnicoSalienteEntranteModalModificar()
                                End If
                            Next

                        End If
                    Next

                End If
            Next
        End If


    End Sub

    Protected Sub BtnEliminar_Click(sender As Object, e As EventArgs) Handles BtnEliminar.Click
        Dim canjeSelect As CanjeDTO = GetCanjeSelect()
        Dim negocio As CanjeNegocio = New CanjeNegocio

        cargarFiltroRutAportanteModal()
        cargarFiltroNombreAportanteModal()
        cargarFiltroMultifondoAportanteModal()
        cargaFiltroRutModal()
        cargaFiltroNombreRutModal()
        cargaFiltroNemotecnicoSalienteEntranteModal()
        cargaFiltroNombreSerieSalienteEntranteModal()
        cargaFiltroMonedaSerieSalienteEntranteModal()

        Dim aportante As AportanteDTO = New AportanteDTO()
        aportante.Rut = canjeSelect.RutAportante
        If canjeSelect.Multifondo = "&nbsp;" Then
            aportante.Multifondo = String.Empty
        Else
            aportante.Multifondo = canjeSelect.Multifondo
        End If
        Dim listaAportante As List(Of AportanteDTO) = negocio.CompararDatosAportantes(aportante)

        Dim serieEntrante As FondoSerieDTO = New FondoSerieDTO()
        serieEntrante.Rut = canjeSelect.RutFondo
        serieEntrante.Nemotecnico = canjeSelect.NemotecnicoEntrante
        Dim listaEntrantes As List(Of FondoSerieDTO) = negocio.CompararDatosEntrantes(serieEntrante)

        Dim serieSaliente As FondoSerieDTO = New FondoSerieDTO()
        serieSaliente.Rut = canjeSelect.RutFondo
        serieSaliente.Nemotecnico = canjeSelect.NemotecnicoSaliente
        Dim listaSaliente As List(Of FondoSerieDTO) = negocio.CompararDatosSalientes(serieSaliente)

        If listaAportante.Count > 0 Then
            For Each aportantes As AportanteDTO In listaAportante
                Dim razonSocial = aportantes.RazonSocial
                Dim estado = aportantes.Estado

                If canjeSelect.NombreAportante <> razonSocial Or estado = 0 Then
                    ShowAlert("No se puede eliminar el canje, El aportante esta deshabilitado")
                    DataInitial()
                    Exit Sub
                ElseIf listaEntrantes.Count > 0 Then
                    For Each entrante As FondoSerieDTO In listaEntrantes
                        Dim serie = entrante.Nombrecorto
                        Dim moneda = entrante.Moneda
                        Dim estadoEntrante = entrante.Estado
                        If canjeSelect.NombreSerieEntrante <> serie Or canjeSelect.MonedaEntrante <> moneda And estadoEntrante = 0 Then
                            ShowAlert("No se puede eliminar el canje, Serie entrante deshabilitada")
                            DataInitial()
                            Exit Sub
                        ElseIf listaSaliente.Count > 0 Then
                            For Each saliente As FondoSerieDTO In listaSaliente
                                Dim serieSaliente2 = saliente.Nombrecorto
                                Dim monedaSaliente = saliente.Moneda
                                Dim estadoSaliente = saliente.Estado
                                If canjeSelect.NombreSerieSaliente <> serieSaliente2 Or canjeSelect.MonedaSaliente <> monedaSaliente And estadoSaliente = 0 Then
                                    ShowAlert("No se puede eliminar el canje, Serie saliente deshabilitada")
                                    DataInitial()
                                    Exit Sub
                                ElseIf canjeSelect.NombreAportante = razonSocial And canjeSelect.NombreSerieEntrante = serie And canjeSelect.MonedaEntrante = moneda And canjeSelect.NombreSerieSaliente = serieSaliente2 And canjeSelect.MonedaSaliente = monedaSaliente And estado = 1 And estadoEntrante = 1 And estadoSaliente = 1 Then
                                    Dim CanjeActualizado As CanjeDTO = negocio.GetCanje(canjeSelect)
                                    FormateoEstiloFormEliminar()
                                    FormateoFormDatos(canjeSelect)
                                    txtAccionHidden.Value = "ELIMINAR"
                                End If
                            Next

                        End If
                    Next

                End If
            Next
        End If
    End Sub

    Private Sub btnModalEliminar_Click(sender As Object, e As EventArgs) Handles btnModalEliminar.Click
        Dim canje As CanjeDTO = GetCanje()
        Dim solicitud As Integer = NegocioCanje.DeleteCanje(canje)

        If solicitud = Constantes.CONST_OPERACION_EXITOSA Then
            cargaFiltroAportanteBusqueda()
            cargaFiltroFondoBusqueda()
            cargaFiltroNemotecnicoBusqueda()
            ShowAlert(CONST_ELIMINAR_EXITO)
            DataInitial()
            txtAccionHidden.Value = ""
        Else
            ShowAlert(CONST_ELIMINAR_ERROR)
            Exit Sub
        End If
    End Sub

    Private Sub btnModalCancelar_Click(sender As Object, e As EventArgs) Handles btnModalCancelar.Click
        txtAccionHidden.Value = ""
    End Sub

    Private Function GetCanjeSelect() As CanjeDTO
        Dim canjeParam As New CanjeDTO

        For Each row As GridViewRow In GrvTabla.Rows
            Dim chk As RadioButton = row.Cells(0).Controls(1)
            If chk IsNot Nothing And chk.Checked Then
                canjeParam.IdCanje = row.Cells(CONST_COL_ID_CANJE).Text.Trim()
                Exit For
            End If
        Next

        Dim negocioCanje As CanjeNegocio = New CanjeNegocio
        Dim canje As CanjeDTO

        canje = negocioCanje.GetCanje(canjeParam)

        Return canje
    End Function

    Protected Sub btnCrear_Click(sender As Object, e As EventArgs) Handles btnCrear.Click
        cargarIdCanje()
        FormateoLimpiarDatosModal()
        FormateoEstiloFormCrear()

        txtAccionHidden.Value = "CREAR"
    End Sub

    Private Function verificarcampos() As Boolean
        If (Double.Parse(txtModalNavSaliente.Text) > 99999999999999 Or Double.Parse(txtModalNavEntrante.Text) > 99999999999999 Or Double.Parse(txtModalMontoEntrante.Text) > 99999999999999 Or Double.Parse(txtModalMontoSaliente.Text) > 99999999999999 Or Double.Parse(txtModalMontoCLPEntrante.Text) > 99999999999999 Or Double.Parse(txtModalMontoCLPSaliente.Text) > 99999999999999 Or Double.Parse(txtModalCuotaEntrante.Text) > 99999999999999 Or Double.Parse(txtModalCuotaSaliente.Text) > 99999999999999 Or Double.Parse(txtModalNavCLPEntrante.Text) > 99999999999999 Or Double.Parse(txtModalNavCLPSaliente.Text) > 99999999999999 Or Double.Parse(txtModalTipoCambio.Text) > 99999999999999) Then
            ShowAlert("Los campos NAV, NAV (CLP), Monto, Monto(CLP), Cuotas y Tc observado no pueden ser mayores a 99999999999999, verifique por favor")
            Return False

        ElseIf (txtModalMontoSaliente.Text = "0" Or txtModalMontoEntrante.Text = "0" Or txtModalNavEntrante.Text = "0" Or txtModalNavSaliente.Text = "0" Or txtModalCuotaEntrante.Text = "0" Or txtModalCuotaSaliente.Text = "0" Or txtModalTipoCambio.Text = "0") Then
            ShowAlert("Los montos, valores NAV, Tipo de cambio y cuotas no pueden ser 0, verifique por favor")
            Return False

        Else
            Return True
        End If

    End Function

    Private Sub btnModalGuardar_Click(sender As Object, e As EventArgs) Handles btnModalGuardar.Click
        If (verificarcampos()) Then
            Dim canje As CanjeDTO = GetCanje()
            canje = NegocioCanje.InsertarCanje(canje)
            txtIdCanje.Text = canje.IdCanje

            If txtIdCanje.Text > 0 Then
                ShowAlert(CONST_EXITO_AL_GUARDAR)
                GenerarPopUp()
                DataInitial()
            Else
                ShowAlert(CONST_ERROR_AL_GUARDAR)
                Exit Sub
            End If
        End If
    End Sub

    Public Function GetCanje() As CanjeDTO
        Dim canje As CanjeDTO = New CanjeDTO
        canje.IdCanje = txtIdCanje.Text
        canje.TipoTransaccion = txtModalTipoTrnasaccion.Text
        canje.RutAportante = ddlModalRutAportante.SelectedValue
        canje.Multifondo = ddlModalMultifondo.Text
        canje.NombreAportante = ddlModalNombreAportante.SelectedValue
        canje.RutFondo = ddlModalFondo.SelectedValue
        canje.NombreFondo = ddlModalNombreFondo.SelectedValue
        canje.FechaNavSaliente = txtModalFechaNavSaliente.Text
        canje.FechaSolicitud = txtModalFSolicitud.Text
        canje.FechaObservado = txtModalFechaObservado.Text
        canje.NemotecnicoSaliente = ddlModalNemotecnicoSaliente.SelectedValue
        canje.NombreSerieSaliente = ddlModalSerieSaliente.SelectedValue
        canje.MonedaSaliente = ddlModalMonedaSaliente.SelectedValue
        canje.CuotaSaliente = txtModalCuotaSaliente.Text
        canje.NavSaliente = txtModalNavSaliente.Text
        canje.MontoSaliente = txtModalMontoSaliente.Text
        canje.NavCLPSaliente = txtModalNavCLPSaliente.Text
        canje.MontoCLPSaliente = txtModalMontoCLPSaliente.Text
        canje.Factor = txtFactorSaliente.Text
        canje.Diferencia = txtModalDiferencia.Text
        canje.DiferenciaCLP = txtModalDiferenciaCLP.Text
        canje.NemotecnicoEntrante = ddlModalNemotecnicoEntrante.SelectedValue
        canje.NombreSerieEntrante = ddlModalSerieEntrante.SelectedValue
        canje.MonedaEntrante = ddlModalMonedaEntrante.SelectedValue
        canje.CuotaEntrante = txtModalCuotaEntrante.Text
        canje.NavEntrante = txtModalNavEntrante.Text
        canje.MontoEntrante = txtModalMontoEntrante.Text
        canje.NavCLPEntrante = txtModalNavCLPEntrante.Text
        canje.MontoCLPEntrante = txtModalMontoCLPEntrante.Text
        canje.ContratoGeneral = IIf(ddlModalContrato.SelectedValue = "", Nothing, ddlModalContrato.SelectedValue)
        canje.RevisionPoderes = IIf(ddlModalPoderes.SelectedValue = "", Nothing, ddlModalPoderes.SelectedValue)
        canje.EstadoCanje = IIf(ddlModalEstado.SelectedValue = "", Nothing, ddlModalEstado.SelectedValue)
        canje.Observaciones = IIf(txtModalObservaciones.Text = "", Nothing, txtModalObservaciones.Text)
        canje.FechaActual = txtModalFechaCuotaDCV.Text
        canje.Cuotas = txtModalCuotaDCV.Text
        canje.RescateTransito = txtModalRescateTransito.Text
        canje.SuscripcionTransito = txtModalSuscripcionTransito.Text
        canje.CanjeTransito = txtModalCanjeTransito.Text
        canje.CuotasDisponibles = IIf(txtModalCuotasDisponibles.Text = "", Nothing, txtModalCuotasDisponibles.Text)
        canje.FijacionNav = ddlModalFijacionNav.SelectedValue
        canje.FijacionTC = ddlModalFijacionTC.SelectedValue
        canje.Estado = IIf(txtEstadoCambio.Value = "", 1, txtEstadoCambio.Value)
        canje.UsuarioIngreso = Session("NombreUsuario")
        canje.UsuarioModificacion = Session("NombreUsuario")
        canje.FechaNavEntrante = txtModalFechaNavEntrante.Text
        canje.TipoCambio = txtModalTipoCambio.Text

        If txtModalFechaCanje.Text <> "" Then
            canje.FechaCanjeDate = txtModalFechaCanje.Text
        End If

        Return canje
    End Function
    '--INICIO FILTROS DE BUSQUEDA--
    Private Sub cargaFiltroAportanteBusqueda()

        llenarComboAportanteBusqueda(New CanjeDTO)
    End Sub

    Private Sub cargaFiltroEstado()
        ddlEstado.Items.Insert(0, StrDup(2, " "))
        ddlEstado.Items.Insert(1, "Pendiente")
        ddlEstado.Items.Insert(2, "Cerrado")
        ddlEstado.Items.Insert(0, New ListItem("", ""))
    End Sub

    Private Sub cargaFiltroFondoBusqueda()
        llenarComboFondosBusqueda(New CanjeDTO)
    End Sub

    Private Sub cargaFiltroNemotecnicoBusqueda()
        llenarComboNemotecnicos(New CanjeDTO)
    End Sub

    '--FIN FILTROS DE BUSQUEDA--

    '--LISTAS DESPLEGABLES MODAL APORTANTE--
    Private Sub cargarFiltroRutAportanteModal()
        Dim negocioCertificados As CertificadoNegocio = New CertificadoNegocio
        Dim aportante As New AportanteDTO
        Dim listaRut As List(Of AportanteDTO) = negocioCertificados.SoloRutAportante(aportante)

        If listaRut.Count = 0 Then
            ddlModalRutAportante.Items.Insert(0, New ListItem("Seleccione", String.Empty))
        Else
            ddlModalRutAportante.DataSource = listaRut
            ddlModalRutAportante.DataMember = "Rut"
            ddlModalRutAportante.DataValueField = "Rut"
            ddlModalRutAportante.DataBind()
            ddlModalRutAportante.Items.Insert(0, New ListItem("Seleccione", String.Empty))
        End If
    End Sub

    Private Sub cargarFiltroNombreAportanteModal()
        Dim aportante As New AportanteDTO
        Dim listaRut As List(Of AportanteDTO) = NegocioAportante.GetListaAportantesPorRazonSocial(aportante)

        If listaRut.Count = 0 Then
            ddlModalNombreAportante.Items.Insert(0, New ListItem("Seleccione", String.Empty))
        Else
            ddlModalNombreAportante.DataSource = listaRut
            ddlModalNombreAportante.DataMember = "RazonSocial"
            ddlModalNombreAportante.DataValueField = "RazonSocial"
            ddlModalNombreAportante.DataBind()
            ddlModalNombreAportante.Items.Insert(0, New ListItem("Seleccione", String.Empty))

        End If
    End Sub

    Private Sub cargarFiltroMultifondoAportanteModal()
        Dim aportante As New AportanteDTO
        Dim listaRut As List(Of AportanteDTO) = NegocioCanje.ConsultarMultifondo(aportante)
        If listaRut.Count = 0 Then
            ddlModalMultifondo.Items.Insert(0, New ListItem("Seleccione", String.Empty))
        Else
            ddlModalMultifondo.DataSource = listaRut
            ddlModalMultifondo.DataMember = "Multifondo"
            ddlModalMultifondo.DataValueField = "Multifondo"
            ddlModalMultifondo.DataBind()
            ddlModalMultifondo.Items.Insert(0, New ListItem("Seleccione", String.Empty))
        End If
    End Sub


    Public Sub LlenaRutNombreAportante()
        txtAccionHidden.Value = "MANTENER_MODAL"

        Dim negocioAportante As AportanteNegocio = New AportanteNegocio
        Dim aportante As AportanteDTO = New AportanteDTO()
        aportante.Multifondo = ddlModalMultifondo.SelectedValue
        Dim listAportante As List(Of AportanteDTO) = negocioAportante.AportantePorMultifondo(aportante)

        ddlModalRutAportante.DataSource = listAportante
        ddlModalRutAportante.DataMember = "Rut"
        ddlModalRutAportante.DataValueField = "Rut"
        ddlModalRutAportante.DataBind()
        ddlModalRutAportante.SelectedIndex = 0

        ddlModalNombreAportante.DataSource = listAportante
        ddlModalNombreAportante.DataMember = "RazonSocial"
        ddlModalNombreAportante.DataValueField = "RazonSocial"
        ddlModalNombreAportante.DataBind()
        ddlModalNombreAportante.SelectedIndex = 0
    End Sub

    Public Sub LlenarMultifondoAportante()
        txtAccionHidden.Value = "MANTENER_MODAL"

        Dim negocioAportante As AportanteNegocio = New AportanteNegocio
        Dim aportante As AportanteDTO = New AportanteDTO()
        Dim listAportante As List(Of AportanteDTO)

        aportante.Rut = ddlModalRutAportante.SelectedValue
        listAportante = negocioAportante.MultifondoPorRut(aportante)

        If listAportante.Count > 0 Then

            ddlModalNombreAportante.DataSource = listAportante
            ddlModalNombreAportante.DataMember = "RazonSocial"
            ddlModalNombreAportante.DataValueField = "RazonSocial"
            ddlModalNombreAportante.DataBind()
            ddlModalNombreAportante.SelectedIndex = 0

            For Each mul As AportanteDTO In listAportante
                Dim vacio As String = mul.Multifondo

                ddlModalMultifondo.Enabled = True
                ddlModalMultifondo.DataSource = listAportante
                ddlModalMultifondo.DataMember = "Multifondo"
                ddlModalMultifondo.DataValueField = "Multifondo"
                ddlModalMultifondo.Items.Insert(0, New ListItem("", ""))
                ddlModalMultifondo.DataBind()
                ddlModalMultifondo.SelectedIndex = 0

                CalcularCuotaDCV()
                CompararCuotas()

                If vacio = "" Then
                    ddlModalMultifondo.Enabled = False
                    ddlModalMultifondo.Text = ""
                    CalcularCuotaDCV()
                    CompararCuotas()
                    Exit Sub
                End If
            Next
        End If

    End Sub

    Public Sub LlenaRutMultifondoAportante()
        txtAccionHidden.Value = "MANTENER_MODAL"

        Dim negocioAportante As AportanteNegocio = New AportanteNegocio
        Dim aportante As AportanteDTO = New AportanteDTO()
        aportante.RazonSocial = ddlModalNombreAportante.SelectedValue
        Dim listAportante As List(Of AportanteDTO) = negocioAportante.AportantePorNombre(aportante)

        ddlModalRutAportante.DataSource = listAportante
        ddlModalRutAportante.DataMember = "Rut"
        ddlModalRutAportante.DataValueField = "Rut"
        ddlModalRutAportante.DataBind()
        ddlModalRutAportante.SelectedIndex = 0

        For Each mul As AportanteDTO In listAportante
            Dim vacio As String
            vacio = mul.Multifondo

            ddlModalMultifondo.Enabled = True
            ddlModalMultifondo.DataSource = listAportante
            ddlModalMultifondo.DataMember = "Multifondo"
            ddlModalMultifondo.DataValueField = "Multifondo"
            ddlModalMultifondo.DataBind()
            ddlModalMultifondo.SelectedIndex = 0
            ddlModalMultifondo.Items.Insert(0, New ListItem("", ""))

            CalcularCuotaDCV()
            CompararCuotas()

            If vacio = "" Then
                ddlModalMultifondo.Text = ""
                ddlModalMultifondo.Enabled = False
                CalcularCuotaDCV()
                CompararCuotas()
                Exit Sub
            End If
        Next

    End Sub
    '--FIN LISTAS DESPLEGABLES MODAL APORTANTE--

    '--LISTAS DESPLEGABLES MODAL SERIE--
    Private Sub cargaFiltroRutModal()
        Dim fondo As New FondoSerieDTO
        Dim lista As List(Of FondoSerieDTO) = NegocioFondoSerie.GetListaFondosRut(fondo)
        ddlModalFondo.Items.Add("Seleccione")

        If lista.Count = 0 Then
            ddlModalFondo.Items.Insert(0, New ListItem("Seleccione", String.Empty))
        Else
            ddlModalFondo.DataSource = lista
            ddlModalFondo.DataMember = "Rut"
            ddlModalFondo.DataValueField = "Rut"
            ddlModalFondo.DataBind()
            ddlModalFondo.Items.Insert(0, New ListItem("Seleccione", String.Empty))
        End If
    End Sub

    Private Sub cargaFiltroNombreRutModal()
        Dim fondo As New FondoDTO
        Dim fondoSerie As New FondoSerieDTO
        Dim NegocioFondo As New FondosNegocio
        Dim lista As List(Of FondoDTO) = NegocioFondo.GetNombreFondo(fondo)

        If lista.Count = 0 Then
            ddlModalNombreFondo.Items.Insert(0, New ListItem("", ""))
        Else
            ddlModalNombreFondo.DataSource = lista
            ddlModalNombreFondo.DataMember = "RazonSocial"
            ddlModalNombreFondo.DataValueField = "RazonSocial"
            ddlModalNombreFondo.DataBind()
            ddlModalNombreFondo.Items.Insert(0, New ListItem("", ""))
        End If
    End Sub

    Private Sub cargaFiltroNemotecnicoSalienteEntranteModal()
        Dim nemotecnico As New FondoSerieDTO
        Dim lista As List(Of FondoSerieDTO) = NegocioFondoSerie.GetListaFondoSerieporNemotecnico(nemotecnico)

        If lista.Count = 0 Then
            ddlModalNemotecnicoSaliente.Items.Insert(0, New ListItem("", ""))
            ddlModalNemotecnicoEntrante.Items.Insert(0, New ListItem("", ""))
        Else
            ddlModalNemotecnicoSaliente.DataSource = lista
            ddlModalNemotecnicoSaliente.DataMember = "Nemotecnico"
            ddlModalNemotecnicoSaliente.DataValueField = "Nemotecnico"

            ddlModalNemotecnicoSaliente.Items.Insert(0, New ListItem("", ""))
            ddlModalNemotecnicoSaliente.DataBind()
            ddlModalNemotecnicoEntrante.DataSource = lista
            ddlModalNemotecnicoEntrante.DataMember = "Nemotecnico"
            ddlModalNemotecnicoEntrante.DataValueField = "Nemotecnico"
            ddlModalNemotecnicoEntrante.DataBind()
            ddlModalNemotecnicoEntrante.Items.Insert(0, New ListItem("", ""))
        End If
    End Sub

    Private Sub cargaFiltroNombreSerieSalienteEntranteModal()
        Dim serie As New FondoSerieDTO
        Dim lista As List(Of FondoSerieDTO) = NegocioCanje.ConsultarNombreSerie(serie)

        If lista.Count = 0 Then
            ddlModalSerieEntrante.Items.Insert(0, New ListItem("", ""))
            ddlModalSerieSaliente.Items.Insert(0, New ListItem("", ""))
        Else
            ddlModalSerieEntrante.DataSource = lista
            ddlModalSerieEntrante.DataMember = "Nombrecorto"
            ddlModalSerieEntrante.DataValueField = "Nombrecorto"
            ddlModalSerieEntrante.DataBind()
            ddlModalSerieEntrante.Items.Insert(0, New ListItem("", ""))

            ddlModalSerieSaliente.DataSource = lista
            ddlModalSerieSaliente.DataMember = "Nombrecorto"
            ddlModalSerieSaliente.DataValueField = "Nombrecorto"
            ddlModalSerieSaliente.DataBind()
            ddlModalSerieSaliente.Items.Insert(0, New ListItem("", ""))
        End If
    End Sub

    Private Sub cargaFiltroNemotecnicoSalienteEntranteModalModificar()
        Dim serieSaliente As FondoSerieDTO = New FondoSerieDTO
        serieSaliente.Rut = ddlModalFondo.SelectedValue
        serieSaliente.Moneda = ddlModalMonedaEntrante.SelectedValue
        Dim listaSaliente As List(Of FondoSerieDTO) = NegocioFondoSerie.GetByMoneda(serieSaliente)

        ddlModalNemotecnicoEntrante.DataSource = listaSaliente
        ddlModalNemotecnicoEntrante.DataMember = "Nemotecnico"
        ddlModalNemotecnicoEntrante.DataValueField = "Nemotecnico"
        ddlModalNemotecnicoEntrante.DataBind()
        ddlModalNemotecnicoEntrante.Items.Insert(0, New ListItem("", ""))

        ddlModalNemotecnicoSaliente.DataSource = listaSaliente
        ddlModalNemotecnicoSaliente.DataMember = "Nemotecnico"
        ddlModalNemotecnicoSaliente.DataValueField = "Nemotecnico"
        ddlModalNemotecnicoSaliente.DataBind()
        ddlModalNemotecnicoSaliente.Items.Insert(0, New ListItem("", ""))
        'ConsultarFechaNavEntrante()
    End Sub

    Private Sub cargaFiltroMonedaSerieSalienteEntranteModal()
        Dim serie As New FondoSerieDTO
        Dim lista As List(Of FondoSerieDTO) = NegocioCanje.ConsultarMonedaSerie(serie)

        If lista.Count = 0 Then
            ddlModalMonedaSaliente.Items.Insert(0, New ListItem("", ""))
            ddlModalMonedaEntrante.Items.Insert(0, New ListItem("", ""))
        Else
            ddlModalMonedaSaliente.DataSource = lista
            ddlModalMonedaSaliente.DataMember = "Moneda"
            ddlModalMonedaSaliente.DataValueField = "Moneda"
            ddlModalMonedaSaliente.DataBind()
            ddlModalMonedaSaliente.Items.Insert(0, New ListItem("", ""))

            ddlModalMonedaEntrante.DataSource = lista
            ddlModalMonedaEntrante.DataMember = "Moneda"
            ddlModalMonedaEntrante.DataValueField = "Moneda"
            ddlModalMonedaEntrante.DataBind()
            ddlModalMonedaEntrante.Items.Insert(0, New ListItem("", ""))
        End If
    End Sub

    Public Sub LlenarRutNombreSerie()
        txtAccionHidden.Value = "MANTENER_MODAL"
        Dim negocioSerie As FondoSeriesNegocio = New FondoSeriesNegocio
        Dim negocioFondo As FondosNegocio = New FondosNegocio
        Dim serie As FondoSerieDTO = New FondoSerieDTO()
        serie.Rut = ddlModalFondo.SelectedValue
        Dim listaSerie As List(Of FondoSerieDTO) = negocioSerie.GrupoSeriesPorRut(serie)
        Dim fondo As FondoDTO = New FondoDTO()
        fondo.Rut = ddlModalFondo.SelectedValue
        Dim listaFondo As List(Of FondoDTO) = negocioFondo.ConsultarUnFondo(fondo)

        ddlModalNombreFondo.DataSource = listaFondo
        ddlModalNombreFondo.DataMember = "RazonSocial"
        ddlModalNombreFondo.DataValueField = "RazonSocial"
        ddlModalNombreFondo.DataBind()
        ddlModalNombreFondo.SelectedIndex = 0

        If listaSerie.Count >= 1 Then
            ddlModalNemotecnicoSaliente.DataSource = listaSerie
            ddlModalNemotecnicoSaliente.DataMember = "Nemotecnico"
            ddlModalNemotecnicoSaliente.DataValueField = "Nemotecnico"
            ddlModalNemotecnicoSaliente.DataBind()
            ddlModalNemotecnicoSaliente.Items.Insert(0, New ListItem("", ""))

            ddlModalNemotecnicoEntrante.DataSource = listaSerie
            ddlModalNemotecnicoEntrante.DataMember = "Nemotecnico"
            ddlModalNemotecnicoEntrante.DataValueField = "Nemotecnico"
            ddlModalNemotecnicoEntrante.DataBind()
            ddlModalNemotecnicoEntrante.Items.Insert(0, New ListItem("", ""))
        End If

    End Sub


    Public Sub LlenarRutNemotecnicos()
        txtAccionHidden.Value = "MANTENER_MODAL"

        Dim negocioFondo As FondosNegocio = New FondosNegocio
        Dim fondo As FondoDTO = New FondoDTO()
        fondo.RazonSocial = ddlModalNombreFondo.SelectedValue
        Dim listaFondo As List(Of FondoDTO) = negocioFondo.RutByNombreFondo(fondo)

        ddlModalFondo.DataSource = listaFondo
        ddlModalFondo.DataMember = "Rut"
        ddlModalFondo.DataValueField = "Rut"
        ddlModalFondo.DataBind()
        ddlModalFondo.SelectedIndex = 0

        Dim negocioSerie As FondoSeriesNegocio = New FondoSeriesNegocio
        Dim serie As FondoSerieDTO = New FondoSerieDTO()
        serie.Rut = ddlModalFondo.SelectedValue
        Dim listaSerie As List(Of FondoSerieDTO) = negocioSerie.GrupoSeriesPorRut(serie)

        If listaSerie.Count >= 1 Then
            ddlModalNemotecnicoSaliente.DataSource = listaSerie
            ddlModalNemotecnicoSaliente.DataMember = "Nemotecnico"
            ddlModalNemotecnicoSaliente.DataValueField = "Nemotecnico"
            ddlModalNemotecnicoSaliente.DataBind()
            ddlModalNemotecnicoSaliente.Items.Insert(0, New ListItem("", ""))

            ddlModalNemotecnicoEntrante.DataSource = listaSerie
            ddlModalNemotecnicoEntrante.DataMember = "Nemotecnico"
            ddlModalNemotecnicoEntrante.DataValueField = "Nemotecnico"
            ddlModalNemotecnicoEntrante.DataBind()
            ddlModalNemotecnicoEntrante.Items.Insert(0, New ListItem("", ""))
        End If

    End Sub

    'Evento OnSelectedIndexChanged del ddlModalNemotecnicoSaliente

    Public Sub LlenarSerieMonedaSaliente()
        txtAccionHidden.Value = "MANTENER_MODAL"
        Dim negocioFondoSerie As FondoSeriesNegocio = New FondoSeriesNegocio
        Dim NegocioFondo As FondosNegocio = New FondosNegocio
        Dim serie As FondoSerieDTO = New FondoSerieDTO
        Dim lista As List(Of FondoSerieDTO)
        Dim fondo As FondoDTO = New FondoDTO
        Dim fondos As List(Of FondoDTO)

        serie.Nemotecnico = ddlModalNemotecnicoSaliente.SelectedValue
        lista = negocioFondoSerie.GrupoSeriesPorNemotecnico(serie)

        fondos = NegocioFondo.GetNombrePorNemotecnico(serie)

        ddlModalFondo.DataSource = lista
        ddlModalFondo.DataMember = "Rut"
        ddlModalFondo.DataValueField = "Rut"
        ddlModalFondo.DataBind()
        ddlModalFondo.SelectedIndex = 0

        ddlModalNombreFondo.DataSource = fondos
        ddlModalNombreFondo.DataMember = "RazonSocial"
        ddlModalNombreFondo.DataValueField = "RazonSocial"
        ddlModalNombreFondo.DataBind()
        ddlModalNombreFondo.SelectedIndex = 0

        ddlModalSerieSaliente.DataSource = lista
        ddlModalSerieSaliente.DataMember = "Nombrecorto"
        ddlModalSerieSaliente.DataValueField = "Nombrecorto"
        ddlModalSerieSaliente.DataBind()
        ddlModalSerieSaliente.SelectedIndex = 0

        ddlModalMonedaSaliente.DataSource = lista
        ddlModalMonedaSaliente.DataMember = "Moneda"
        ddlModalMonedaSaliente.DataValueField = "Moneda"
        ddlModalMonedaSaliente.DataBind()
        ddlModalMonedaSaliente.SelectedIndex = 0

        Dim saliente As String = ddlModalNemotecnicoSaliente.SelectedValue
        Dim entrante As String = ddlModalNemotecnicoEntrante.SelectedValue


        Dim serieEntrante As FondoSerieDTO = New FondoSerieDTO
        serieEntrante.Rut = ddlModalFondo.SelectedValue
        serieEntrante.Moneda = ddlModalMonedaSaliente.SelectedValue
        Dim listaEntrante As List(Of FondoSerieDTO) = negocioFondoSerie.GetByMoneda(serieEntrante)
        ddlModalNemotecnicoEntrante.DataSource = listaEntrante
        ddlModalNemotecnicoEntrante.DataMember = "Nemotecnico"
        ddlModalNemotecnicoEntrante.DataValueField = "Nemotecnico"
        ddlModalNemotecnicoEntrante.DataBind()
        ddlModalNemotecnicoEntrante.Items.Insert(0, New ListItem("", ""))

        Dim seriesaliente As FondoSerieDTO = New FondoSerieDTO
        seriesaliente.Rut = ddlModalFondo.SelectedValue
        Dim listasaliente As List(Of FondoSerieDTO) = negocioFondoSerie.GrupoSeriesPorRut(serieEntrante)
        ddlModalNemotecnicoSaliente.DataSource = listasaliente
        ddlModalNemotecnicoSaliente.DataMember = "Nemotecnico"
        ddlModalNemotecnicoSaliente.DataValueField = "Nemotecnico"
        ddlModalNemotecnicoSaliente.DataBind()
        ddlModalNemotecnicoSaliente.Items.Insert(0, New ListItem("", ""))

        ddlModalNemotecnicoSaliente.SelectedValue = saliente
        Try
            ddlModalNemotecnicoEntrante.SelectedValue = entrante
        Catch ex As Exception
            ddlModalNemotecnicoEntrante.SelectedValue = ""
        End Try

        ddlModalSerieSaliente.DataSource = lista
        ddlModalSerieSaliente.DataMember = "Nombrecorto"
        ddlModalSerieSaliente.DataValueField = "Nombrecorto"
        ddlModalSerieSaliente.DataBind()
        ddlModalSerieSaliente.SelectedIndex = 0

        ddlModalMonedaSaliente.DataSource = lista
        ddlModalMonedaSaliente.DataMember = "Moneda"
        ddlModalMonedaSaliente.DataValueField = "Moneda"
        ddlModalMonedaSaliente.DataBind()
        ddlModalMonedaSaliente.SelectedIndex = 0

        txtModalMontoSaliente.Text = ""
        txtModalNavSaliente.Text = ""
        txtModalCuotaSaliente.Text = ""

        ConsultarFechaNavSaliente()
        CalcularCuotaDCV()
        ConversionMonedaEntrante()
        calcularFactor()
        ConsultarFechaCanje()
    End Sub

    ''' <summary>
    ''' Evento OnTextChanged desde ddlModalNemotecnicoSaliente 
    ''' </summary>
    ''' 
    Public Sub ConsultarFechaNavSaliente()
        Dim serie As FondoSerieDTO = New FondoSerieDTO
        Dim negocioSerie As FondoSeriesNegocio = New FondoSeriesNegocio
        Dim FechaNavCanje As String
        Dim estructuraFechas As EstructuraFechasDto

        'Dim fechaNavC As String
        'Dim diasNavC As String

        'Dim canje As CanjeDTO = New CanjeDTO
        Dim fechaParaCalculo As Date
        Dim SoloDiasHabiles As Integer

        Dim series As FondoSerieDTO = New FondoSerieDTO

        If txtModalFSolicitud.Text = "" Then
            txtModalFechaNavSaliente.Text = ""
            Exit Sub
        End If

        serie.Nemotecnico = ddlModalNemotecnicoSaliente.SelectedValue
        series = negocioSerie.GetFondoSeriesNemotecnico(serie)

        txtModalTipoCambio.Text = IIf(series.Moneda = "CLP", "1", txtModalTipoCambio.Text)

        If series.FijacionCanje = "Automático" Then
            FechaNavCanje = series.FechaNavCanje
            estructuraFechas = New EstructuraFechasDto
            estructuraFechas = Utiles.splitCharByComma(series.FechaNavCanje)
            'fechaNavC = estructuraFechas.DesdeQueFecha

            Select Case estructuraFechas.DesdeQueFecha
                Case "FechaSolicitud"
                    If txtModalFSolicitud.Text <> "" Then
                        fechaParaCalculo = txtModalFSolicitud.Text
                    End If
                Case "FechaCanje"
                    If txtModalFechaCanje.Text <> "" Then
                        fechaParaCalculo = txtModalFechaCanje.Text
                    End If

                Case Else
                    txtModalFechaNavSaliente.Text = txtModalFSolicitud.Text
            End Select

            If fechaParaCalculo <> Nothing Then
                SoloDiasHabiles = IIf(series.SoloDiasHabilesFechaNavCanje, Constantes.CONST_SOLO_DIAS_HABILES, Constantes.CONST_SOLO_DIAS_CORRIDOS)
                txtModalFechaNavSaliente.Text = Utiles.SumaDiasAFechas(ddlModalMonedaSaliente.Text, fechaParaCalculo, estructuraFechas.DiasASumar, SoloDiasHabiles)

                CalcularValorSaliente()
                CalcularCuotaEntrante()
                CalcularMontoEntrante()
            End If

        ElseIf series.FijacionCanje = "Manual" Then
            estructuraFechas = New EstructuraFechasDto

            estructuraFechas = Utiles.splitCharByComma(series.FechaNavCanje)
            FechaNavCanje = series.FechaNavCanje

            ''diasNavC = estructuraFechas.DiasASumar

            Select Case estructuraFechas.DesdeQueFecha
                Case "FechaSolicitud"
                    ' canje.FechaSolicitud = txtModalFSolicitud.Text
                    fechaParaCalculo = txtModalFSolicitud.Text
                    txtModalFechaNavSaliente.Text = txtModalFSolicitud.Text ' fechaParaCalculo

                    'CalcularValorSaliente()
                   ' ConsultarFechaObservado()
                Case "FechaCanje"
                    fechaParaCalculo = txtModalFechaCanje.Text
                Case Else
                    txtModalFechaNavSaliente.Text = ""
            End Select

            If fechaParaCalculo <> Nothing Then
                SoloDiasHabiles = IIf(series.SoloDiasHabilesFechaNavCanje, Constantes.CONST_SOLO_DIAS_HABILES, Constantes.CONST_SOLO_DIAS_CORRIDOS)

                fechaParaCalculo = Utiles.SumaDiasAFechas(ddlModalMonedaSaliente.Text, fechaParaCalculo, estructuraFechas.DiasASumar, SoloDiasHabiles)

                txtModalFechaNavSaliente.Text = fechaParaCalculo
                txtModalFechaNavEntrante.Text = txtModalFechaNavSaliente.Text

                ConsultarFechaObservado()
                CalcularValorSaliente()
                CalcularCuotaEntrante()
                CalcularMontoEntrante()
            End If
        Else
            ConsultarFechaObservado()
            CalcularValorSaliente()
            CalcularCuotaEntrante()
            CalcularMontoEntrante()
        End If

    End Sub

    ''' <summary>
    ''' Evento OnSelectionChanged de CalendarModalFechaObservado
    ''' </summary>
    ''' 
    Public Sub CalcularTipo()
        Dim negocioFondoSerie As FondoSeriesNegocio = New FondoSeriesNegocio
        Dim serie As FondoSerieDTO = New FondoSerieDTO
        Dim lista As List(Of FondoSerieDTO)

        Dim TipoCambio As TipoCambioDTO = New TipoCambioDTO
        Dim negocioTipo As TipoCambioNegocio = New TipoCambioNegocio

        Dim listaTipo As List(Of TipoCambioDTO)
        Dim retornar As Double
        Dim listaUltimo As List(Of TipoCambioDTO)

        If txtModalFechaObservado.Text = "" Then Return

        serie.Nemotecnico = ddlModalNemotecnicoSaliente.SelectedValue
        lista = negocioFondoSerie.GrupoSeriesPorNemotecnico(serie)

        For Each series As FondoSerieDTO In lista
            If series.FijacionCanje = "Automático" Then
                TipoCambio.Codigo = ddlModalMonedaSaliente.SelectedValue
                TipoCambio.Fecha = txtModalFechaObservado.Text

                listaTipo = negocioTipo.ConsultarPorTipoCambioYFecha(TipoCambio)

                If listaTipo.Count = 0 Then
                    listaUltimo = negocioTipo.UltimoTipoCambioPorCodigo(TipoCambio)

                    If listaUltimo.Count = 0 Then
                        retornar = 1
                        txtModalTipoCambio.Text = retornar
                        ddlModalFijacionTC.SelectedValue = "Pendiente"
                    Else
                        For Each tis As TipoCambioDTO In listaUltimo
                            retornar = tis.Valor
                            txtModalTipoCambio.Text = Math.Round(tis.Valor, 12)
                            ddlModalFijacionTC.SelectedValue = "Pendiente"
                        Next
                    End If
                Else
                    For Each tips As TipoCambioDTO In listaTipo
                        retornar = tips.Valor
                        txtModalTipoCambio.Text = Math.Round(tips.Valor, 12)
                        ddlModalFijacionTC.SelectedValue = "Realizado"
                    Next
                End If
            Else 'If series.FijacionCanje = "Manual" Then
                If txtModalFechaObservado.Text = "" Or ddlModalMonedaSaliente.SelectedValue = "" Then
                    txtModalTipoCambio.Text = ""
                    ddlModalFijacionNav.SelectedValue = "Pendiente"
                Else
                    TipoCambio.Codigo = ddlModalMonedaSaliente.SelectedValue
                    TipoCambio.Fecha = txtModalFechaObservado.Text

                    listaTipo = negocioTipo.ConsultarPorTipoCambioYFecha(TipoCambio)

                    If listaTipo.Count = 0 Then
                        listaUltimo = negocioTipo.UltimoTipoCambioPorCodigo(TipoCambio)

                        If listaUltimo.Count = 0 Then
                            retornar = 1
                            txtModalTipoCambio.Text = retornar
                            ddlModalFijacionTC.SelectedValue = "Pendiente"
                        Else
                            For Each tis As TipoCambioDTO In listaUltimo
                                retornar = tis.Valor
                                txtModalTipoCambio.Text = Math.Round(tis.Valor, 12)
                                ddlModalFijacionTC.SelectedValue = "Pendiente"
                            Next
                        End If
                    Else
                        For Each tips As TipoCambioDTO In listaTipo
                            retornar = tips.Valor
                            txtModalTipoCambio.Text = Math.Round(tips.Valor, 12)
                            ddlModalFijacionTC.SelectedValue = "Realizado"
                        Next
                    End If

                End If
            End If

            If ddlModalMonedaSaliente.SelectedValue = "CLP" Then
                txtModalTipoCambio.Text = 1
                ddlModalFijacionTC.SelectedValue = "Realizado"
            End If

        Next

        CalcularValoresEntranteYSalienteConFactor()
        CalcularDiferencias()
    End Sub

    Public Sub ConversionMoneda()
        If txtModalTipoCambio.Text = "" Or ddlModalMonedaSaliente.SelectedValue = "" Or txtModalNavSaliente.Text = "" Then
            txtModalNavCLPSaliente.Text = ""
        Else
            ConversionMonedaEntrante()
            CalcularValoresEntranteYSalienteConFactor() ' txtModalNavCLPSaliente.Text = Utiles.calcularNAVCLP(cambio, saliente)  '  String.Format("{0:N4}", (saliente * cambio))

        End If
    End Sub


    Private Sub CalcularValoresEntranteYSalienteConFactor()
        Dim navEntrante As Decimal
        Dim navSaliente As Decimal
        Dim Factor As Decimal

        txtModalNavCLPSaliente.Text = Utiles.calcularNAVCLP(txtModalTipoCambio.Text, txtModalNavSaliente.Text)
        txtModalMontoSaliente.Text = Utiles.calcularMonto(txtModalCuotaSaliente.Text, txtModalNavSaliente.Text, ddlModalMonedaSaliente.Text)
        txtModalMontoCLPSaliente.Text = Utiles.calcularMontoCLP(txtModalCuotaSaliente.Text, txtModalNavCLPSaliente.Text)

        txtModalNavCLPEntrante.Text = Utiles.calcularNAVCLP(txtModalTipoCambio.Text, txtModalNavEntrante.Text) ' String.Format("{0:N4}", Double.Parse(entrante * cambio))
        txtModalMontoEntrante.Text = Utiles.calcularMonto(txtModalCuotaEntrante.Text, txtModalNavEntrante.Text, ddlModalMonedaEntrante.SelectedValue)      '
        txtModalMontoCLPEntrante.Text = Utiles.calcularMontoCLP(txtModalCuotaEntrante.Text, txtModalNavCLPEntrante.Text)

        If IsNumeric(txtModalNavEntrante.Text) Then
            navEntrante = txtModalNavEntrante.Text
        Else
            navEntrante = 0
        End If

        If IsNumeric(txtModalNavSaliente.Text) Then
            navSaliente = txtModalNavSaliente.Text
        Else
            navSaliente = 0
        End If

        If navEntrante = 0 Then
            Factor = 0
        Else
            Factor = navSaliente / navEntrante
        End If

        txtFactorSaliente.Text = Utiles.SetToCapitalizedNumber(Factor)
        txtModalFactor.Text = Utiles.SetToCapitalizedNumber(Factor)


    End Sub

    Public Sub ConversionMonedaEntrante()
        If txtModalTipoCambio.Text = "" Or ddlModalMonedaEntrante.SelectedValue = "" Or txtModalNavEntrante.Text = "" Then
            txtModalNavCLPEntrante.Text = ""
        Else
            CalcularValoresEntranteYSalienteConFactor()
        End If

        CalcularMontoEntrante()
        CalcularMontoSaliente()
    End Sub

    Public Sub LlenarSerieMonedaEntrante()
        txtAccionHidden.Value = "MANTENER_MODAL"
        Dim negocioFondoSerie As FondoSeriesNegocio = New FondoSeriesNegocio
        Dim NegocioFondo As FondosNegocio = New FondosNegocio
        Dim serie As FondoSerieDTO = New FondoSerieDTO

        serie.Nemotecnico = ddlModalNemotecnicoEntrante.SelectedValue
        Dim lista As List(Of FondoSerieDTO) = negocioFondoSerie.GrupoSeriesPorNemotecnico(serie)
        Dim fondo As FondoDTO = New FondoDTO
        Dim fondos As List(Of FondoDTO) = NegocioFondo.GetNombrePorNemotecnico(serie)

        ddlModalFondo.DataSource = lista
        ddlModalFondo.DataMember = "Rut"
        ddlModalFondo.DataValueField = "Rut"
        ddlModalFondo.DataBind()
        ddlModalFondo.SelectedIndex = 0

        ddlModalNombreFondo.DataSource = fondos
        ddlModalNombreFondo.DataMember = "RazonSocial"
        ddlModalNombreFondo.DataValueField = "RazonSocial"
        ddlModalNombreFondo.DataBind()
        ddlModalNombreFondo.SelectedIndex = 0

        ddlModalSerieEntrante.DataSource = lista
        ddlModalSerieEntrante.DataMember = "Nombrecorto"
        ddlModalSerieEntrante.DataValueField = "Nombrecorto"
        ddlModalSerieEntrante.DataBind()
        ddlModalSerieEntrante.SelectedIndex = 0

        ddlModalMonedaEntrante.DataSource = lista
        ddlModalMonedaEntrante.DataMember = "Moneda"
        ddlModalMonedaEntrante.DataValueField = "Moneda"
        ddlModalMonedaEntrante.DataBind()
        ddlModalMonedaEntrante.SelectedIndex = 0

        Dim entrante As String = ddlModalNemotecnicoEntrante.SelectedValue
        Dim saliente As String = ddlModalNemotecnicoSaliente.SelectedValue

        Dim serieSaliente As FondoSerieDTO = New FondoSerieDTO
        serieSaliente.Rut = ddlModalFondo.SelectedValue
        serieSaliente.Moneda = ddlModalMonedaEntrante.SelectedValue
        Dim listaSaliente As List(Of FondoSerieDTO) = negocioFondoSerie.GetByMoneda(serieSaliente)
        ddlModalNemotecnicoSaliente.DataSource = listaSaliente
        ddlModalNemotecnicoSaliente.DataMember = "Nemotecnico"
        ddlModalNemotecnicoSaliente.DataValueField = "Nemotecnico"
        ddlModalNemotecnicoSaliente.DataBind()
        ddlModalNemotecnicoSaliente.Items.Insert(0, New ListItem("", ""))

        'ConsultarFechaCanje()

        ConsultarFechaNavEntrante()

        Dim serieEntrante As FondoSerieDTO = New FondoSerieDTO
        serieEntrante.Rut = ddlModalFondo.SelectedValue
        serieEntrante.Moneda = ddlModalMonedaSaliente.SelectedValue
        Dim listaEntrante As List(Of FondoSerieDTO) = negocioFondoSerie.GrupoSeriesPorRut(serieEntrante)
        ddlModalNemotecnicoEntrante.DataSource = listaEntrante
        ddlModalNemotecnicoEntrante.DataMember = "Nemotecnico"
        ddlModalNemotecnicoEntrante.DataValueField = "Nemotecnico"
        ddlModalNemotecnicoEntrante.DataBind()
        ddlModalNemotecnicoEntrante.Items.Insert(0, New ListItem("", ""))

        ddlModalNemotecnicoEntrante.SelectedValue = entrante
        Try
            ddlModalNemotecnicoSaliente.SelectedValue = saliente
        Catch ex As Exception
            ddlModalNemotecnicoSaliente.SelectedValue = ""
        End Try

        ddlModalSerieEntrante.DataSource = lista
        ddlModalSerieEntrante.DataMember = "Nombrecorto"
        ddlModalSerieEntrante.DataValueField = "Nombrecorto"
        ddlModalSerieEntrante.DataBind()
        ddlModalSerieEntrante.SelectedIndex = 0

        ddlModalMonedaEntrante.DataSource = lista
        ddlModalMonedaEntrante.DataMember = "Moneda"
        ddlModalMonedaEntrante.DataValueField = "Moneda"
        ddlModalMonedaEntrante.DataBind()
        ddlModalMonedaEntrante.SelectedIndex = 0
    End Sub


    'TODO: REVISAR EL COMPORTAMIENTO DEL CALENDARIO. 

    Private Sub txtModalFSolicitud_TextChanged(sender As Object, e As EventArgs) Handles txtModalFSolicitud.TextChanged
        'txtModalFSolicitud.Text = CalendarModalFechaSolicitud.SelectedDate.ToShortDateString()
        ConsultarFechaNavSaliente()
        ConsultarFechaNavEntrante()
        ConsultarFechaObservado()
        ConsultarFechaCanje()
        CalcularCuotaDCV()

    End Sub

    Protected Sub ddlModalNemotecnicoEntrante_TextChanged(sender As Object, e As EventArgs) Handles ddlModalNemotecnicoEntrante.TextChanged
        ConsultarFechaNavSaliente()
        ConsultarFechaNavEntrante()
        ConsultarFechaObservado()
        'ConsultarFechaCanje()
        CalcularCuotaDCV()
    End Sub

    Public Sub ConsultarFechaCanje()
        Dim paramSerie As FondoSerieDTO = New FondoSerieDTO
        Dim serie As FondoSerieDTO = New FondoSerieDTO
        Dim negocioSerie As FondoSeriesNegocio = New FondoSeriesNegocio
        Dim estructuraFechas As EstructuraFechasDto = New EstructuraFechasDto
        Dim FechaParaCalculo As Date
        Dim SoloDiasHabiles As Integer

        paramSerie.Nemotecnico = ddlModalNemotecnicoSaliente.SelectedValue
        serie = negocioSerie.GetFondoSeriesNemotecnico(paramSerie)

        If serie Is Nothing Then Return

        estructuraFechas = Utiles.splitCharByComma(serie.FechaCanjeCanje)

        Select Case estructuraFechas.DesdeQueFecha
            Case "FechaSolicitud"
                FechaParaCalculo = txtModalFSolicitud.Text
            Case "FechaNav"
                If txtModalFechaNavSaliente.Text <> "" Then
                    FechaParaCalculo = txtModalFechaNavSaliente.Text
                End If
            Case Else
                FechaParaCalculo = txtModalFSolicitud.Text
        End Select

        If FechaParaCalculo <> Nothing Then

            SoloDiasHabiles = IIf(serie.SoloDiasHabilesFechaCanje, Constantes.CONST_SOLO_DIAS_HABILES, Constantes.CONST_SOLO_DIAS_CORRIDOS)
            txtModalFechaCanje.Text = Utiles.SumaDiasAFechas(ddlModalMonedaSaliente.Text, FechaParaCalculo, estructuraFechas.DiasASumar, SoloDiasHabiles)
        End If
    End Sub

    Private Sub txtModalFechaObservado_TextChanged(sender As Object, e As EventArgs) Handles txtModalFechaObservado.TextChanged
        Dim negocioRescate As VentanasRescateNegocio = New VentanasRescateNegocio
        Dim fechaValidar As String

        If txtModalFechaObservado.Text <> "" Then
            fechaValidar = negocioRescate.ValidaDiaHabil(txtModalFechaObservado.Text)

            If fechaValidar = "Festivo" Then
                txtModalFechaObservado.Text = ""
                ShowAlert("La fecha seleccionada es un día feriado")
                Return
            ElseIf fechaValidar = "No_Habil" Then
                txtModalFechaObservado.Text = ""
                ShowAlert("La fecha seleccionada no es hábil")
                Return
            End If

            ConversionMoneda()
        End If

    End Sub

    Private Sub txtModalFechaNavSaliente_TextChanged(sender As Object, e As EventArgs) Handles txtModalFechaNavSaliente.TextChanged
        txtModalFechaNavEntrante.Text = txtModalFechaNavSaliente.Text

        CalcularValorSaliente()
        CalcularValorEntrante()
    End Sub

    Private Sub txtModalFechaNavEntrante_TextChanged(sender As Object, e As EventArgs) Handles txtModalFechaNavEntrante.TextChanged
        txtModalFechaNavSaliente.Text = txtModalFechaNavEntrante.Text ' CalendarModalFechaNavEntrante.SelectedDate.ToShortDateString()

        CalcularValorEntrante()
        CalcularValorSaliente()

    End Sub

    '--FUN MOSTRAR CALENDARIOS--
    Public Sub ConsultarFechaNavEntrante()
        Dim serieParam As FondoSerieDTO = New FondoSerieDTO
        Dim negocioSerie As FondoSeriesNegocio = New FondoSeriesNegocio

        Dim FechaNavCanje As String
        Dim FechaParaCalculo As Date
        Dim serie As FondoSerieDTO
        Dim SoloDiasHabiles As Integer

        serieParam.Nemotecnico = ddlModalNemotecnicoEntrante.SelectedValue
        serie = negocioSerie.GetFondoSeriesNemotecnico(serieParam)

        If serie.FijacionCanje = "Automático" Then
            Dim estructuraFechas As EstructuraFechasDto = New EstructuraFechasDto
            Dim DiasCorridos As Integer

            estructuraFechas = Utiles.splitCharByComma(serie.FechaNavCanje)
            FechaNavCanje = serie.FechaNavCanje

            Select Case estructuraFechas.DesdeQueFecha
                Case "FechaSolicitud"
                    FechaParaCalculo = txtModalFSolicitud.Text
                Case "FechaCanje"
                    If txtModalFechaCanje.Text <> "" Then
                        FechaParaCalculo = txtModalFechaCanje.Text
                    End If
            End Select

            If FechaParaCalculo <> Nothing Then
                DiasCorridos = IIf(serie.SoloDiasHabilesFechaNavCanje, Constantes.CONST_SOLO_DIAS_HABILES, Constantes.CONST_SOLO_DIAS_CORRIDOS)
                txtModalFechaNavEntrante.Text = Utiles.SumaDiasAFechas(ddlModalMonedaEntrante.Text, FechaParaCalculo, estructuraFechas.DiasASumar, DiasCorridos)
                CalcularValorEntrante()
            End If

        ElseIf serie.FijacionCanje = "Manual" Then

            Dim estructuraFechas As EstructuraFechasDto = New EstructuraFechasDto

            estructuraFechas = Utiles.splitCharByComma(serie.FechaNavCanje)

            FechaNavCanje = serie.FechaNavCanje
            Select Case estructuraFechas.DesdeQueFecha
                Case "FechaSolicitud"
                    FechaParaCalculo = txtModalFSolicitud.Text

                Case "FechaCanje"
                    FechaParaCalculo = txtModalFechaCanje.Text

                Case Else
                    txtModalFechaNavEntrante.Text = ""

            End Select

            If FechaParaCalculo <> Nothing Then

                SoloDiasHabiles = IIf(serieParam.SoloDiasHabilesFechaNavCanje, Constantes.CONST_SOLO_DIAS_HABILES, Constantes.CONST_SOLO_DIAS_CORRIDOS)

                txtModalFechaNavEntrante.Text = Utiles.SumaDiasAFechas(ddlModalMonedaSaliente.Text, FechaParaCalculo, estructuraFechas.DiasASumar, SoloDiasHabiles)
                CalcularValorEntrante()
            End If

        End If
    End Sub

    Public Sub ConsultarFechaObservado()
        Dim NegocioRescate As RescateNegocio = New RescateNegocio
        Dim serieParam As FondoSerieDTO = New FondoSerieDTO
        Dim negocioSerie As FondoSeriesNegocio = New FondoSeriesNegocio
        Dim canje As CanjeDTO = New CanjeDTO
        Dim FechaObservado As String

        serieParam.Nemotecnico = ddlModalNemotecnicoSaliente.SelectedValue
        ' Dim listaSerie As List(Of FondoSerieDTO) = negocioSerie.GrupoSeriesPorNemotecnico(serie)
        Dim serieActual As FondoSerieDTO = negocioSerie.GetFondoSeriesNemotecnico(serieParam)

        canje.FechaSolicitud = txtModalFSolicitud.Text

        Dim fechaSolicitud As Date
        Dim testString As String = FormatDateTime(fechaSolicitud, DateFormat.LongDate)
        fechaSolicitud = canje.FechaSolicitud

        Dim fijacion As String = serieActual.FijacionCanje
        Dim estructuraFechas As EstructuraFechasDto = New EstructuraFechasDto
        Dim SoloDiasHabiles As Integer

        estructuraFechas = Utiles.splitCharByComma(serieActual.FechaTCCanje)

        FechaObservado = serieActual.FechaTCCanje

        Dim bDiaInhabil As Boolean = False

        Select Case estructuraFechas.DesdeQueFecha
            Case "FechaSolicitud"

                fechaSolicitud = txtModalFSolicitud.Text
                fechaSolicitud = Utiles.SumaDiasAFechas(ddlModalMonedaSaliente.Text, fechaSolicitud, estructuraFechas.DiasASumar, Constantes.CONST_SOLO_DIAS_HABILES)

                bDiaInhabil = (Not Utiles.esFechaHabil(ddlModalMonedaSaliente.Text, fechaSolicitud) And ddlModalMonedaSaliente.Text = "USD")
                fechaSolicitud = Utiles.getDiaHabilSiguiente(fechaSolicitud, ddlModalMonedaSaliente.Text)

                txtModalFechaObservado.Text = fechaSolicitud

            Case "FechaNav"
                canje.FechaNavSaliente = txtModalFechaNavSaliente.Text
                Dim fechaNavSaliente = canje.FechaNavSaliente

                SoloDiasHabiles = IIf(serieActual.SoloDiasHabilesFechaNavCanje, Constantes.CONST_SOLO_DIAS_HABILES, Constantes.CONST_SOLO_DIAS_HABILES)

                fechaNavSaliente = Utiles.SumaDiasAFechas(ddlModalMonedaSaliente.Text, fechaNavSaliente, estructuraFechas.DiasASumar, SoloDiasHabiles)
                bDiaInhabil = (Not Utiles.esFechaHabil(ddlModalMonedaSaliente.Text, fechaNavSaliente) And ddlModalMonedaSaliente.Text = "USD")

                fechaNavSaliente = Utiles.getDiaHabilSiguiente(fechaNavSaliente, ddlModalMonedaSaliente.Text)

                txtModalFechaObservado.Text = fechaNavSaliente

            Case "FechaCanje"
                If txtModalFechaCanje.Text = "" Then
                    ConsultarFechaCanje()
                End If

                canje.FechaNavSaliente = txtModalFechaCanje.Text
                Dim FechaTCObservado = canje.FechaNavSaliente

                SoloDiasHabiles = IIf(serieActual.SoloDiasHabilesFechaNavCanje, Constantes.CONST_SOLO_DIAS_HABILES, Constantes.CONST_SOLO_DIAS_CORRIDOS)

                FechaTCObservado = Utiles.SumaDiasAFechas(ddlModalMonedaSaliente.Text, FechaTCObservado, estructuraFechas.DiasASumar, Constantes.CONST_SOLO_DIAS_HABILES)
                ' Si fecha en cuestión cae en un día inhábil US, sistema mostrará mensaje de advertencia
                bDiaInhabil = (Not Utiles.esFechaHabil(ddlModalMonedaSaliente.Text, FechaTCObservado) And ddlModalMonedaSaliente.Text = "USD")

                FechaTCObservado = Utiles.getDiaHabilSiguiente(FechaTCObservado, ddlModalMonedaSaliente.Text)

                txtModalFechaObservado.Text = FechaTCObservado

            Case Else
                txtModalFechaObservado.Text = fechaSolicitud

        End Select

        If bDiaInhabil Then
            ShowAlertTC(CONST_INHABIL_PARA_TC)
        End If

        CalcularTipo()
    End Sub
    'Private Function fncSoloDiasHabiles(serie As FondoSerieDTO) As Integer
    '    Return (IIf(serie.SoloDiasHabilesFechaNavCanje, 1, 0))
    'End Function
    Private Sub ShowAlertTC(mesagge As String, Optional mostrarEnPage As Boolean = False)
        Dim myScript As String = "alert('" + mesagge + "');"

        If Not mostrarEnPage Then
            ScriptManager.RegisterStartupScript(UpdatePanel20, UpdatePanel20.GetType(), "alert", myScript, True)
        Else
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", myScript, True)
        End If
    End Sub

    Private Sub ShowAlertCuotasDisponibles(mesagge As String, Optional mostrarEnPage As Boolean = False)
        Dim myScript As String = "alert('" + mesagge + "');"

        If Not mostrarEnPage Then
            ScriptManager.RegisterStartupScript(UpdatePanel20, UpdatePanel20.GetType(), "alert", myScript, True)
        Else
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", myScript, True)
        End If
    End Sub

    'Evento de cambio de fechaNAV Entrante
    Public Sub CalcularValorEntrante()
        Dim valor As VcSerieDTO = New VcSerieDTO
        Dim negocioValor As ValoresCuotaNegocio = New ValoresCuotaNegocio
        Dim listaValores As List(Of VcSerieDTO)
        Dim listaUltimo As List(Of VcSerieDTO)
        Dim valorNav As Single

        If ddlModalNemotecnicoEntrante.SelectedValue = "" Or txtModalFechaNavEntrante.Text = "" Then
            txtModalNavEntrante.Text = ""
        Else
            valor.FsNemotecnico = ddlModalNemotecnicoEntrante.SelectedValue
            valor.Fecha = txtModalFechaNavEntrante.Text

            listaValores = negocioValor.ValoresCuotaPorNemotecnicoYFecha(valor)

            If listaValores.Count = 0 Then
                listaUltimo = negocioValor.UltimoValorCuota(valor)
                If listaUltimo.Count = 0 Then
                    txtModalNavEntrante.Text = ""
                    txtModalNavCLPEntrante.Text = ""
                Else
                    For Each vcs As VcSerieDTO In listaUltimo
                        valorNav = vcs.Valor
                        If ddlModalMonedaEntrante.SelectedValue <> "CLP" Then
                            If ddlModalMonedaEntrante.Text.Trim = "USD" Then
                                txtModalNavEntrante.Text = Utiles.formatearNAV(vcs.Valor) ' String.Format("{0:N4}", vcs.Valor)
                            Else
                                txtModalNavEntrante.Text = Utiles.formatearNAV(vcs.Valor) ' String.Format("{0:N4}", vcs.Valor)
                            End If
                            txtModalNavCLPEntrante.Text = Utiles.formatearNAVCLP(vcs.Valor) ' String.Format("{0:N4}", valorNav)
                        ElseIf ddlModalMonedaEntrante.SelectedValue = "CLP" Then
                            txtModalNavEntrante.Text = Utiles.formatearNAV(vcs.Valor) ' String.Format("{0:N4}", vcs.Valor)
                            txtModalNavCLPEntrante.Text = Utiles.formatearNAVCLP(vcs.Valor) ' String.Format("{0:N4}", valorNav)
                            replicarNavCLPEntrante()
                        End If
                    Next
                End If
            Else
                For Each valores As VcSerieDTO In listaValores
                    valorNav = valores.Valor
                    If ddlModalMonedaEntrante.SelectedValue <> "CLP" Then
                        If ddlModalMonedaEntrante.Text.Trim = "USD" Then
                            txtModalNavEntrante.Text = Utiles.formatearNAV(valores.Valor) '  String.Format("{0:N4}", valores.Valor)
                        Else
                            txtModalNavEntrante.Text = Utiles.formatearNAV(valores.Valor) '  String.Format("{0:N4}", valores.Valor)
                        End If
                        txtModalNavCLPEntrante.Text = Utiles.formatearNAVCLP(valores.Valor)  ' valorNav
                    ElseIf ddlModalMonedaEntrante.SelectedValue = "CLP" Then
                        txtModalNavEntrante.Text = Utiles.formatearNAV(valores.Valor) '  String.Format("{0:N4}", valores.Valor)
                        txtModalNavCLPEntrante.Text = Utiles.formatearNAVCLP(valores.Valor)  ' String.Format("{0:N4}", valorNav)
                        replicarNavCLPEntrante()
                    End If

                Next
            End If

            ConversionMonedaEntrante()
            calcularFactor()
            CalcularCuotaEntrante()

            txtModalFechaNavSaliente.Text = txtModalFechaNavEntrante.Text
        End If
    End Sub

    ''' <summary>
    ''' Evento CalendarModalFechaNavSaliente SelectedChange 
    ''' </summary>
    ''' 
    Public Sub CalcularValorSaliente()
        Dim valor As VcSerieDTO = New VcSerieDTO
        Dim negocioValor As ValoresCuotaNegocio = New ValoresCuotaNegocio
        Dim ValorCuotaNavFecha As List(Of VcSerieDTO)
        Dim valorNav As Single
        Dim serie As FondoSerieDTO = New FondoSerieDTO
        Dim negocioSerie As FondoSeriesNegocio = New FondoSeriesNegocio
        Dim listaSerie As List(Of FondoSerieDTO)

        If ddlModalNemotecnicoSaliente.SelectedValue = "" Or txtModalFechaNavSaliente.Text = "" Then
            txtModalNavSaliente.Text = ""
        Else
            valor.FsNemotecnico = ddlModalNemotecnicoSaliente.SelectedValue
            valor.Fecha = txtModalFechaNavSaliente.Text

            ValorCuotaNavFecha = negocioValor.ValoresCuotaPorNemotecnicoYFecha(valor)

            serie.Nemotecnico = ddlModalNemotecnicoSaliente.SelectedValue
            listaSerie = negocioSerie.GrupoSeriesPorNemotecnico(serie)

            If listaSerie.Count > 0 Then
                For Each ser As FondoSerieDTO In listaSerie
                    If ser.FijacionCanje = "Automático" Then
                        If ValorCuotaNavFecha.Count = 0 Then ' Buscar ultimo valor ya que no hay valores para la fecha 
                            Dim listaUltimoValorNAV As List(Of VcSerieDTO) = negocioValor.UltimoValorCuota(valor)

                            If listaUltimoValorNAV.Count = 0 Then
                                txtModalNavSaliente.Text = ""
                                txtModalNavCLPSaliente.Text = ""
                                ddlModalFijacionNav.SelectedValue = "Pendiente"
                            Else
                                For Each vcs As VcSerieDTO In listaUltimoValorNAV
                                    valorNav = vcs.Valor
                                    If ddlModalMonedaSaliente.SelectedValue <> "CLP" Then
                                        txtModalNavSaliente.Text = Utiles.formatearNAV(vcs.Valor) ' String.Format("{0:N4}", vcs.Valor)
                                        txtModalNavCLPSaliente.Text = Utiles.formatearNAVCLP(valorNav) ' String.Format("{0:N4}", valorNav)
                                        ddlModalFijacionNav.SelectedValue = "Pendiente"

                                    ElseIf ddlModalMonedaSaliente.SelectedValue = "CLP" Then
                                        txtModalNavSaliente.Text = Utiles.formatearNAV(vcs.Valor) ' String.Format("{0:N4}", vcs.Valor)
                                        txtModalNavCLPSaliente.Text = Utiles.formatearNAVCLP(valorNav) ' String.Format("{0:N4}", valorNav)
                                        ddlModalFijacionNav.SelectedValue = "Pendiente"
                                    Else
                                        ddlModalFijacionNav.SelectedValue = "Pendiente"
                                    End If
                                Next
                            End If

                        Else
                            For Each valores As VcSerieDTO In ValorCuotaNavFecha
                                valorNav = valores.Valor
                                txtModalNavCLPSaliente.Text = Utiles.formatearNAVCLP(valorNav) ' String.Format("{0:N4}", valorNav)
                                txtModalNavSaliente.Text = Utiles.formatearNAV(valores.Valor) 'String.Format("{0:N4}", valores.Valor)
                                ddlModalFijacionNav.SelectedValue = "Realizado"
                            Next
                        End If

                        ConversionMoneda()
                        calcularFactor()
                        CalcularCuotaEntrante()
                        CalcularMontoEntrante()
                        CalcularDiferencias()
                        ConsultarFechaObservado()


                    ElseIf ser.FijacionCanje = "Manual" Then
                        If ValorCuotaNavFecha.Count = 0 Then
                            Dim listaUltimo As List(Of VcSerieDTO) = negocioValor.UltimoValorCuota(valor)
                            If listaUltimo.Count = 0 Then
                                txtModalNavSaliente.Text = ""
                                txtModalNavCLPSaliente.Text = ""
                                ddlModalFijacionNav.SelectedValue = "Pendiente"
                            Else
                                For Each vcs As VcSerieDTO In listaUltimo
                                    If ddlModalMonedaSaliente.SelectedValue <> "CLP" Then
                                        valorNav = vcs.Valor
                                        If ddlModalMonedaSaliente.Text.Trim = "USD" Then
                                            txtModalNavSaliente.Text = Utiles.formatearNAV(vcs.Valor) ' String.Format("{0:N4}", vcs.Valor)
                                        Else
                                            txtModalNavSaliente.Text = Utiles.formatearNAV(vcs.Valor) 'String.Format("{0:N4}", vcs.Valor)
                                        End If
                                        txtModalNavCLPSaliente.Text = Utiles.formatearNAVCLP(valorNav) ' String.Format("{0:N4}", valorNav)
                                        ddlModalFijacionNav.SelectedValue = "Pendiente"
                                    ElseIf ddlModalMonedaSaliente.SelectedValue = "CLP" Then
                                        valorNav = vcs.Valor
                                        txtModalNavSaliente.Text = Utiles.formatearNAV(vcs.Valor) 'String.Format("{0:N4}", vcs.Valor)
                                        txtModalNavCLPSaliente.Text = Utiles.formatearNAVCLP(valorNav) ' String.Format("{0:N4}", valorNav)
                                        ddlModalFijacionNav.SelectedValue = "Pendiente"
                                    End If
                                Next
                            End If
                        Else
                            For Each valores As VcSerieDTO In ValorCuotaNavFecha
                                txtModalNavCLPSaliente.Text = Utiles.formatearNAVCLP(valores.Valor) ' String.Format("{0:N4}", valorNav)
                                txtModalNavSaliente.Text = Utiles.formatearNAV(valores.Valor) 'String.Format("{0:N4}", valores.Valor)
                                ddlModalFijacionNav.SelectedValue = "Pendiente"
                            Next
                        End If

                        calcularFactor()

                        CalcularCuotaEntrante()
                        CalcularMontoEntrante()

                        CalcularDiferencias()
                        ConsultarFechaObservado()
                    Else
                        ddlModalFijacionNav.SelectedValue = "Pendiente"
                    End If
                Next
            End If
        End If

        CalcularValoresEntranteYSalienteConFactor()
    End Sub

    Public Sub replicarNavCLP()
        Dim canje As CanjeDTO = New CanjeDTO
        canje.MonedaSaliente = ddlModalMonedaSaliente.SelectedValue
        If txtModalNavSaliente.Text = "" Then
            txtModalNavCLPSaliente.Text = ""
        Else
            If ddlModalMonedaSaliente.SelectedValue = "CLP" Then
                canje.NavSaliente = txtModalNavSaliente.Text
                txtModalNavCLPSaliente.Text = Utiles.formatearNAVCLP(canje.NavSaliente) 'String.Format("{0:N4}", canje.NavSaliente)
            End If
        End If
    End Sub

    Public Sub replicarNavCLPEntrante()
        Dim canje As CanjeDTO = New CanjeDTO
        canje.MonedaEntrante = ddlModalMonedaEntrante.SelectedValue
        If txtModalNavEntrante.Text = "" Then
            txtModalNavCLPEntrante.Text = ""
        Else
            If ddlModalMonedaEntrante.SelectedValue = "CLP" Then
                canje.NavEntrante = txtModalNavEntrante.Text
                txtModalNavEntrante.Text = Utiles.formatearNAV(canje.NavEntrante) ' String.Format("{0:N4}", canje.NavEntrante)
                txtModalNavCLPEntrante.Text = Utiles.formatearNAVCLP(canje.NavEntrante)  'String.Format("{0:N4}", canje.NavEntrante)
            Else
                ConversionMonedaEntrante()
            End If
        End If
    End Sub
    Protected Sub MontoSaliente()
        If (IsNumeric(txtModalNavSaliente.Text) And IsNumeric(txtModalMontoSaliente.Text)) Then

            If (Double.Parse(txtModalNavSaliente.Text) > 0) Then
                Dim cuota = (Double.Parse(txtModalMontoSaliente.Text) / Double.Parse(txtModalNavSaliente.Text))
                txtModalCuotaSaliente.Text = String.Format("{0:N0}", Math.Floor(cuota))


                CalcularValoresEntranteYSalienteConFactor()
            Else
                txtModalCuotaSaliente.Text = 0
                txtModalMontoSaliente.Text = 0
            End If
        End If


    End Sub
    Public Sub cuotaschange()
        CalcularMontoSaliente()
        CalcularCuotaEntrante()
    End Sub
    Public Sub calcularFactor()

        replicarNavCLP()
        replicarNavCLPEntrante()

        If txtModalNavCLPSaliente.Text = "" Or txtModalNavCLPEntrante.Text = "" Then
            txtModalFactor.Text = ""
            CalcularMontoSaliente()
        Else
            ConversionMoneda()
            ConversionMonedaEntrante()
            CalcularCuotaEntrante()
        End If

        If IsNumeric(txtModalNavCLPSaliente.Text) Then
            txtModalNavCLPSaliente.Text = Utiles.formatearNAVCLP(txtModalNavCLPSaliente.Text) ' String.Format("{0:N4}", Double.Parse(txtModalNavCLPSaliente.Text))
        End If
        If IsNumeric(txtModalNavCLPEntrante.Text) Then
            txtModalNavCLPEntrante.Text = Utiles.formatearNAVCLP(txtModalNavCLPEntrante.Text) ' String.Format("{0:N4}", Double.Parse(txtModalNavCLPEntrante.Text))
        End If


        If IsNumeric(txtModalMontoCLPEntrante.Text) And ddlModalMonedaEntrante.SelectedValue <> "" Then
            txtModalMontoCLPEntrante.Text = Utiles.formatearMontoCLP(txtModalMontoCLPEntrante.Text) ' String.Format("{0:N2}", Double.Parse(txtModalMontoCLPEntrante.Text))
        End If
        If IsNumeric(txtModalMontoCLPSaliente.Text) And ddlModalMonedaSaliente.SelectedValue <> "" Then
            txtModalMontoCLPSaliente.Text = Utiles.formatearMontoCLP(txtModalMontoCLPSaliente.Text) ' String.Format("{0:N2}", Double.Parse(txtModalMontoCLPSaliente.Text))
        End If
    End Sub


    Public Sub CalcularCuotaEntrante()
        If txtModalCuotaSaliente.Text = "" Or txtFactorSaliente.Text = "" Then
            txtModalCuotaEntrante.Text = ""
            CalcularMontoSaliente()
            CompararCuotas()
        Else

            txtModalCuotaEntrante.Text = Math.Floor(txtModalCuotaSaliente.Text * txtFactorSaliente.Text)

            ConversionMonedaEntrante()
            CalcularMontoEntrante()
            CalcularMontoSaliente()
            CalcularDiferencias()
            CompararCuotas()

        End If
    End Sub

    Public Sub CompararCuotas()
        Dim canje As CanjeDTO = New CanjeDTO
        If (txtModalCuotaSaliente.Text = "" And txtModalCuotasDisponibles.Text = "") Then
            txtModalCuotasDisponibles.Text = ""

        ElseIf txtModalCuotasDisponibles.Text = "" Or txtModalCuotaSaliente.Text = "" Then
            txtFechaSolicitudHasta.Text = ""
        Else
            canje.CuotasDisponibles = txtModalCuotasDisponibles.Text
            canje.CuotaSaliente = txtModalCuotaSaliente.Text

            If canje.CuotasDisponibles < canje.CuotaSaliente Then

                ShowAlertCuotasDisponibles("Las Cuotas Disponibles no pueden ser menor a las Cuotas Salientes")
                txtModalCuotaSaliente.Text = ""
                txtModalCuotaEntrante.Text = ""
                txtModalMontoEntrante.Text = ""
                txtModalMontoCLPEntrante.Text = ""
                txtModalMontoCLPSaliente.Text = ""
                txtModalMontoSaliente.Text = ""
                txtModalDiferencia.Text = ""
                txtModalDiferenciaCLP.Text = ""
            End If
        End If
    End Sub

    Public Sub CalcularCuotaPorMontoSaliente()
        MontoSaliente()
        CalcularCuotaEntrante()
        CalcularMontoSaliente()
    End Sub
    Public Sub navsalientechange()
        If (IsNumeric(txtModalNavSaliente.Text)) Then
            If (IsNumeric(txtModalCuotaSaliente.Text)) Then
                cuotaschange()

            ElseIf (IsNumeric(txtModalMontoSaliente.Text)) Then
                MontoSaliente()

            End If

        End If
        calcularFactor()
    End Sub
    Public Sub CalcularMontoEntrante()

        If txtModalCuotaEntrante.Text = "" Or txtModalNavEntrante.Text = "" Or txtModalNavCLPEntrante.Text = "" Then
            txtModalMontoEntrante.Text = ""
            txtModalMontoCLPEntrante.Text = ""

        Else
            CalcularValoresEntranteYSalienteConFactor()
            CalcularDiferencias()

        End If
    End Sub

    Public Sub CalcularDiferencias()
        Dim canje As CanjeDTO = New CanjeDTO

        If txtModalMontoEntrante.Text = "" Or _
            txtModalMontoSaliente.Text = "" Or _
            txtModalMontoCLPSaliente.Text = "" Or _
            txtModalMontoCLPEntrante.Text = "" Then

            txtModalDiferencia.Text = ""
            txtModalDiferenciaCLP.Text = ""
        Else
            If ddlModalMonedaEntrante.SelectedValue = "CLP" And ddlModalMonedaSaliente.SelectedValue = "CLP" Then

                canje.MontoEntrante = txtModalMontoEntrante.Text
                canje.MontoSaliente = txtModalMontoSaliente.Text
                canje.MontoCLPEntrante = txtModalMontoCLPEntrante.Text
                canje.MontoCLPSaliente = Utiles.formatearMontoCLP(txtModalMontoCLPSaliente.Text)  ' String.Format("{0:N0}", Double.Parse(txtModalMontoCLPSaliente.Text))

                Dim diferencia = canje.MontoSaliente - canje.MontoEntrante
                Dim diferenciaCLP = canje.MontoCLPSaliente - canje.MontoCLPEntrante

                txtModalDiferencia.Text = Utiles.formatearDiferencia(diferencia, ddlModalMonedaEntrante.SelectedValue)
                txtModalDiferenciaCLP.Text = Utiles.formatearDiferenciaCLP(diferenciaCLP)
            Else
                canje.MontoEntrante = txtModalMontoEntrante.Text
                canje.MontoSaliente = txtModalMontoSaliente.Text
                canje.MontoCLPEntrante = txtModalMontoCLPEntrante.Text
                canje.MontoCLPSaliente = txtModalMontoCLPSaliente.Text

                Dim diferencia = canje.MontoSaliente - canje.MontoEntrante
                Dim diferenciaCLP = canje.MontoCLPSaliente - canje.MontoCLPEntrante

                txtModalDiferencia.Text = Utiles.formatearDiferencia(diferencia, ddlModalMonedaEntrante.SelectedValue)
                txtModalDiferenciaCLP.Text = Utiles.formatearDiferenciaCLP(diferenciaCLP)
            End If

        End If
    End Sub

    Public Sub DiferenciarNavEntrantes()
        Dim canje As CanjeDTO = New CanjeDTO()
        Dim replicar As Double

        canje.NavEntrante = txtModalNavEntrante.Text

        replicar = canje.NavEntrante
        txtModalNavCLPEntrante.Text = replicar
    End Sub

    Public Sub DiferenciarNavSalientes()
        Dim canje As CanjeDTO = New CanjeDTO()
        Dim replicar As Double

        canje.NavSaliente = txtModalNavSaliente.Text
        replicar = canje.NavEntrante

        txtModalNavCLPSaliente.Text = Utiles.formatearNAVCLP(replicar)
    End Sub

    Public Sub CalcularMontoSaliente()
        If ddlModalMonedaSaliente.SelectedValue = "" Then
            If (IsNumeric(txtModalNavSaliente.Text) And IsNumeric(txtModalCuotaSaliente.Text)) Then
                ' txtModalMontoSaliente.Text = Utiles.calcularMonto(txtModalCuotaSaliente.Text, txtModalNavSaliente.Text, ddlModalMonedaSaliente.SelectedValue)
                CalcularValoresEntranteYSalienteConFactor()
            End If
        Else
            If txtModalNavSaliente.Text = "" Or txtModalCuotaSaliente.Text = "" Then
                txtModalMontoCLPSaliente.Text = ""
                txtModalMontoSaliente.Text = ""
            Else
                If txtModalNavCLPSaliente.Text = "" Then
                    txtModalMontoCLPSaliente.Text = ""
                Else
                    CalcularValoresEntranteYSalienteConFactor()
                End If

                CalcularMontoEntrante()
            End If
        End If

        txtModalMontoSaliente.Text = Utiles.formatearMonto(txtModalMontoSaliente.Text, ddlModalMonedaSaliente.SelectedValue)
        txtModalMontoCLPSaliente.Text = Utiles.formatearMontoCLP(txtModalMontoCLPSaliente.Text)

    End Sub

    Public Sub CalcularCuotaDCV()
        Dim dcv As ADCVDTO = New ADCVDTO
        Dim negocioDCV As ADCVNegocio = New ADCVNegocio
        If ddlModalRutAportante.SelectedValue = "" Or ddlModalNombreAportante.SelectedValue = "" Or ddlModalNemotecnicoSaliente.SelectedValue = "" Or txtModalFechaCuotaDCV.Text = "" Then
            txtModalCuotaDCV.Text = ""
        Else
            dcv.AP_RUT = IIf(ddlModalRutAportante.SelectedValue.Trim() = "", Nothing, ddlModalRutAportante.SelectedValue.Trim())
            dcv.ADCV_Razon_Social = IIf(ddlModalNombreAportante.SelectedValue.Trim() = "", Nothing, ddlModalNombreAportante.SelectedValue.Trim())
            dcv.FS_Nemotecnico = IIf(ddlModalNemotecnicoSaliente.SelectedValue.Trim() = "", Nothing, ddlModalNemotecnicoSaliente.SelectedValue.Trim())
            dcv.ADCV_Fecha = IIf(txtModalFSolicitud.Text = "", Nothing, txtModalFSolicitud.Text)
            Dim listaDCV As List(Of ADCVDTO) = negocioDCV.ConsultaDCV(dcv)
            Dim cuotaDCV As Double

            If listaDCV.Count = 0 Then
                Dim listaUltimo As List(Of ADCVDTO) = negocioDCV.UltimoDCV(dcv)
                If listaUltimo.Count = 0 Then
                    cuotaDCV = 0
                    txtModalCuotaDCV.Text = cuotaDCV
                    txtModalFechaCuotaDCV.Text = dcv.ADCV_Fecha.ToShortDateString()

                End If
                For Each dvcs As ADCVDTO In listaUltimo
                    Dim fecha As Date
                    fecha = dvcs.ADCV_Fecha
                    cuotaDCV = dvcs.ADCV_Cantidad
                    txtModalFechaCuotaDCV.Text = fecha.ToShortDateString()
                    txtModalCuotaDCV.Text = cuotaDCV
                Next
            Else
                For Each dvcs As ADCVDTO In listaDCV
                    cuotaDCV = dvcs.ADCV_Cantidad
                    txtModalCuotaDCV.Text = cuotaDCV
                Next
            End If
        End If

        RescateTransito()
        SuscripcionTransito()
        CanjeTransito()
    End Sub

    Public Sub RescateTransito()
        Dim Rescate As RescatesDTO = New RescatesDTO()
        Dim Negocio As RescateNegocio = New RescateNegocio
        Dim RescateActualizado As RescatesDTO = New RescatesDTO()

        If txtModalFSolicitud.Text <> "" Then
            Rescate.RES_Fecha_Pago = txtModalFSolicitud.Text
        Else
            Rescate.RES_Fecha_Pago = Nothing
        End If

        If (ddlModalRutAportante.SelectedValue <> "" And ddlModalNemotecnicoSaliente.SelectedValue <> "") Then
            Rescate.FS_Nemotecnico = ddlModalNemotecnicoSaliente.SelectedValue
            Rescate.AP_RUT = ddlModalRutAportante.SelectedValue
            Rescate.AP_Multifondo = ddlModalMultifondo.SelectedValue
            Rescate.RES_Fecha_Solicitud = txtModalFSolicitud.Text

            RescateActualizado = Negocio.SelectRescatesTransito2(Rescate)

            If RescateActualizado IsNot Nothing Then
                txtModalRescateTransito.Text = RescateActualizado.RES_Cuotas
            Else
                txtModalRescateTransito.Text = "0"
            End If
        Else
            txtModalRescateTransito.Text = "0"
        End If
    End Sub

    Public Sub SuscripcionTransito()
        Dim sus As SuscripcionDTO = New SuscripcionDTO
        Dim negocio As SuscripcionNegocio = New SuscripcionNegocio

        If ddlModalRutAportante.SelectedValue = "" Or ddlModalNemotecnicoSaliente.Text = "" Or txtModalFSolicitud.Text = "" Then
            txtModalSuscripcionTransito.Text = ""
        Else
            sus.RutAportante = ddlModalRutAportante.SelectedValue
            sus.Nemotecnico = ddlModalNemotecnicoSaliente.SelectedValue
            sus.FechaSuscripcion = txtModalFSolicitud.Text
            sus.Multifondo = ddlModalMultifondo.SelectedValue()

            Dim listaSus As SuscripcionDTO = negocio.GetSuscripcionTransito2(sus)

            If listaSus IsNot Nothing Then
                txtModalSuscripcionTransito.Text = listaSus.CuotasASuscribir
            End If
        End If


    End Sub

    Public Sub CanjeTransito()

        Dim canje As CanjeDTO = New CanjeDTO
        Dim negocioCanje As CanjeNegocio = New CanjeNegocio
        canje.RutAportante = ddlModalRutAportante.SelectedValue
        canje.NemotecnicoSaliente = ddlModalNemotecnicoSaliente.SelectedValue
        canje.FechaSolicitud = txtModalFSolicitud.Text
        canje.Multifondo = ddlModalMultifondo.SelectedValue
        Dim RetornoCanje As CanjeDTO = negocioCanje.CanjesTransito(canje)

        If RetornoCanje IsNot Nothing Then
            txtModalCanjeTransito.Text = RetornoCanje.CanjeTransito
        Else
            txtModalCanjeTransito.Text = "0"
        End If

        'CARGA TOTAL DISPONIBLES
        If (IsNumeric(txtModalCanjeTransito.Text) And IsNumeric(txtModalRescateTransito.Text) And IsNumeric(txtModalCuotaDCV.Text) And IsNumeric(txtModalSuscripcionTransito.Text)) Then
            txtModalCuotasDisponibles.Text = txtModalCuotaDCV.Text - txtModalRescateTransito.Text + txtModalSuscripcionTransito.Text + txtModalCanjeTransito.Text
        End If
    End Sub

    Private Sub CargarTodosCanjes()
        Dim canje As CanjeDTO = New CanjeDTO()
        Dim negocio As CanjeNegocio = New CanjeNegocio()

        GrvTabla.DataSource = negocio.ConsultarTodos(canje)
        GrvTabla.DataBind()
    End Sub

    Private Sub FindCanje()
        Dim canje As CanjeDTO = New CanjeDTO()
        Dim fechaSolicitudHasta As Nullable(Of Date)
        Dim fechaNavHasta As Nullable(Of Date)


        Dim fechaCanjeHasta As Nullable(Of Date)

        txtFechaSolicitudDesde.Text = Request.Form(txtFechaSolicitudDesde.UniqueID)
        txtFechaSolicitudHasta.Text = Request.Form(txtFechaSolicitudHasta.UniqueID)

        txtFechaNavDesde.Text = Request.Form(txtFechaNavDesde.UniqueID)
        txtFechaNavHasta.Text = Request.Form(txtFechaNavHasta.UniqueID)

        txtFechaCanjeDesde.Text = Request.Form(txtFechaCanjeDesde.UniqueID)
        txtFechaCanjeHasta.Text = Request.Form(txtFechaCanjeHasta.UniqueID)

        If ddlListaRutAportante.SelectedValue.Trim() = Nothing Then
            canje.NombreAportante = ""
            canje.RutAportante = ""
        Else
            Dim arrCadena As String() = ddlListaRutAportante.SelectedItem.Text().Split(New Char() {"/"c})

            canje.RutAportante = arrCadena(0).Trim()
            canje.NombreAportante = arrCadena(1).Trim()
        End If

        If ddlListaRutFondo.SelectedValue.Trim() = Nothing Then
            canje.NombreFondo = ""
            canje.RutFondo = ""
        Else
            Dim arrCadena As String() = ddlListaRutFondo.SelectedItem.Text().Split(New Char() {"/"c})

            canje.RutFondo = arrCadena(0).Trim()
            canje.NombreFondo = arrCadena(1).Trim()
        End If

        If ddlListaNemotecnico.SelectedValue.Trim() = Nothing Then
            canje.NemotecnicoSaliente = ""
        Else
            canje.NemotecnicoSaliente = ddlListaNemotecnico.SelectedValue.Trim()
        End If
        If ddlEstado.SelectedValue.Trim() = Nothing Then
            canje.EstadoCanje = ""
        Else
            canje.EstadoCanje = ddlEstado.SelectedValue.Trim()
        End If

        If Not txtFechaSolicitudDesde.Text.Equals("") Then
            canje.FechaSolicitud = Date.Parse(txtFechaSolicitudDesde.Text)
        Else
            canje.FechaSolicitud = Nothing
        End If

        If Not txtFechaSolicitudHasta.Text.Equals("") Then
            fechaSolicitudHasta = Date.Parse(txtFechaSolicitudHasta.Text)
        Else
            fechaSolicitudHasta = Nothing
        End If

        If Not txtFechaNavDesde.Text.Equals("") Then
            canje.FechaNavSaliente = Date.Parse(txtFechaNavDesde.Text)
        Else
            canje.FechaNavSaliente = Nothing
        End If

        If Not txtFechaNavHasta.Text.Equals("") Then
            fechaNavHasta = Date.Parse(txtFechaNavHasta.Text)
        Else
            fechaNavHasta = Nothing
        End If

        If Not txtFechaCanjeDesde.Text.Equals("") Then
            canje.FechaCanjeDate = Date.Parse(txtFechaCanjeDesde.Text)
        Else
            canje.FechaCanjeDate = Nothing
        End If

        If Not txtFechaCanjeHasta.Text.Equals("") Then
            fechaCanjeHasta = Date.Parse(txtFechaCanjeHasta.Text)
        Else
            fechaCanjeHasta = Nothing
        End If


        If ddlListaRutAportante.Text = "" And _
            ddlListaRutFondo.Text = "" And _
            ddlListaNemotecnico.Text = "" And _
            ddlEstado.Text = "" And _
            txtFechaSolicitudDesde.Text = Nothing And _
            txtFechaSolicitudHasta.Text = Nothing And _
            txtFechaNavDesde.Text = Nothing And _
            txtFechaNavHasta.Text = Nothing And _
            txtFechaCanjeDesde.Text = Nothing And _
            txtFechaCanjeHasta.Text = Nothing Then

            CargarTodosCanjes()
        Else
            GrvTabla.DataSource = NegocioCanje.ConsultarFiltros(canje, fechaSolicitudHasta, fechaNavHasta, fechaCanjeHasta)
        End If

        GrvTabla.DataBind()
    End Sub

    Private Sub FormateoEstiloFormCrear()
        btnModalModificar.Enabled = False
        btnModalModificar.Visible = False
        btnModalGuardar.Enabled = True
        btnModalGuardar.Visible = True
        btnModalEliminar.Enabled = False
        lbModalTittle.Text = CONST_TITULO_MODAL_CREAR
        ddlModalRutAportante.Enabled = True
        ddlModalMultifondo.Enabled = True
        ddlModalNombreAportante.Enabled = True
        ddlModalFondo.Enabled = True
        ddlModalNombreFondo.Enabled = True
        txtModalFechaNavSaliente.Enabled = False
        txtModalFSolicitud.Enabled = True
        txtModalFechaObservado.Enabled = True
        ddlModalNemotecnicoSaliente.Enabled = True
        ddlModalSerieSaliente.Enabled = False
        ddlModalMonedaSaliente.Enabled = False
        txtModalCuotaSaliente.Enabled = True
        txtModalNavSaliente.Enabled = True
        txtModalMontoSaliente.Enabled = True
        txtModalNavCLPSaliente.Enabled = False
        txtModalMontoCLPSaliente.Enabled = False
        txtFactorSaliente.Enabled = False
        txtModalDiferencia.Enabled = False
        txtModalDiferenciaCLP.Enabled = False
        ddlModalNemotecnicoEntrante.Enabled = True
        ddlModalSerieEntrante.Enabled = False
        ddlModalMonedaEntrante.Enabled = False
        txtModalCuotaEntrante.Enabled = False
        txtModalNavEntrante.Enabled = True
        txtModalMontoEntrante.Enabled = False
        txtModalNavCLPEntrante.Enabled = False
        txtModalMontoCLPEntrante.Enabled = False
        ddlModalContrato.Enabled = True
        ddlModalPoderes.Enabled = True
        ddlModalEstado.Enabled = True
        txtModalObservaciones.Enabled = True
        txtModalFechaCuotaDCV.Enabled = True
        txtModalCuotaDCV.Enabled = False
        txtModalRescateTransito.Enabled = False
        txtModalSuscripcionTransito.Enabled = False
        txtModalCanjeTransito.Enabled = False
        txtModalCuotasDisponibles.Enabled = False
        ddlModalFijacionNav.Enabled = False
        ddlModalFijacionTC.Enabled = False
        txtModalFechaNavEntrante.Enabled = False
        txtModalTipoCambio.Enabled = True
        lnkbtnModalFechaObservado.Visible = True
        lnkbtnModalFechaNavSaliente.Visible = True
        lnkbtnModalFechaNavEntrante.Visible = True

        'lnkBtnModalFechaSolicitud.Enabled = True
        'LinkButton4.Enabled = True
        'lnkBtnModalFechaSolicitud.Visible = True

    End Sub

    Private Sub FormateoEstiloFormModificar()
        btnModalModificar.Enabled = True
        btnModalModificar.Visible = True
        btnModalGuardar.Enabled = False
        btnModalGuardar.Visible = False
        btnModalEliminar.Enabled = False
        lbModalTittle.Text = CONST_TITULO_MODAL_MODIFICAR
        ddlModalRutAportante.Enabled = True
        ddlModalMultifondo.Enabled = True
        ddlModalNombreAportante.Enabled = True
        ddlModalFondo.Enabled = True
        ddlModalNombreFondo.Enabled = True
        txtModalFechaNavSaliente.Enabled = False
        txtModalFSolicitud.Enabled = True
        txtModalFechaObservado.Enabled = True
        ddlModalNemotecnicoSaliente.Enabled = True
        ddlModalSerieSaliente.Enabled = False
        ddlModalMonedaSaliente.Enabled = False
        txtModalCuotaSaliente.Enabled = True
        txtModalNavSaliente.Enabled = True
        txtModalMontoSaliente.Enabled = True
        txtModalNavCLPSaliente.Enabled = False
        txtModalMontoCLPSaliente.Enabled = False
        txtFactorSaliente.Enabled = False
        txtModalDiferencia.Enabled = False
        txtModalDiferenciaCLP.Enabled = False
        ddlModalNemotecnicoEntrante.Enabled = True
        ddlModalSerieEntrante.Enabled = False
        ddlModalMonedaEntrante.Enabled = False
        txtModalCuotaEntrante.Enabled = False
        txtModalNavEntrante.Enabled = True
        txtModalMontoEntrante.Enabled = False
        txtModalNavCLPEntrante.Enabled = False
        txtModalMontoCLPEntrante.Enabled = False
        ddlModalContrato.Enabled = True
        ddlModalPoderes.Enabled = True
        ddlModalEstado.Enabled = True
        txtModalObservaciones.Enabled = True
        txtModalFechaCuotaDCV.Enabled = True
        txtModalCuotaDCV.Enabled = False
        txtModalRescateTransito.Enabled = False
        txtModalSuscripcionTransito.Enabled = False
        txtModalCanjeTransito.Enabled = False
        txtModalCuotasDisponibles.Enabled = False
        ddlModalFijacionNav.Enabled = False
        ddlModalFijacionTC.Enabled = False
        txtModalFechaNavEntrante.Enabled = False
        txtModalTipoCambio.Enabled = True
        lnkbtnModalFechaObservado.Visible = True
        lnkbtnModalFechaNavSaliente.Visible = True
        lnkbtnModalFechaNavEntrante.Visible = True
        'lnkBtnModalFechaSolicitud.Visible = True
        'lnkBtnModalFechaSolicitud.Enabled = True
        'LinkButton4.Enabled = True
    End Sub

    Private Sub FormateoEstiloFormEliminar()
        btnModalModificar.Enabled = False
        btnModalModificar.Visible = False
        btnModalGuardar.Enabled = False
        btnModalGuardar.Visible = False
        btnModalEliminar.Enabled = True
        lbModalTittle.Text = CONST_TITULO_MODAL_ELIMINAR
        txtIdCanje.Enabled = False
        txtModalTipoTrnasaccion.Enabled = False
        ddlModalRutAportante.Enabled = False
        ddlModalMultifondo.Enabled = False
        ddlModalNombreAportante.Enabled = False
        ddlModalFondo.Enabled = False
        ddlModalNombreFondo.Enabled = False
        txtModalFechaNavSaliente.Enabled = False
        txtModalFSolicitud.Enabled = False
        txtModalFechaObservado.Enabled = False
        ddlModalNemotecnicoSaliente.Enabled = False
        ddlModalSerieSaliente.Enabled = False
        ddlModalMonedaSaliente.Enabled = False
        txtModalCuotaSaliente.Enabled = False
        txtModalNavSaliente.Enabled = False
        txtModalMontoSaliente.Enabled = False
        txtModalNavCLPSaliente.Enabled = False
        txtModalMontoCLPSaliente.Enabled = False
        txtFactorSaliente.Enabled = False
        txtModalDiferencia.Enabled = False
        txtModalDiferenciaCLP.Enabled = False
        ddlModalNemotecnicoEntrante.Enabled = False
        ddlModalSerieEntrante.Enabled = False
        ddlModalMonedaEntrante.Enabled = False
        txtModalCuotaEntrante.Enabled = False
        txtModalNavEntrante.Enabled = False
        txtModalMontoEntrante.Enabled = False
        txtModalNavCLPEntrante.Enabled = False
        txtModalMontoCLPEntrante.Enabled = False
        ddlModalContrato.Enabled = False
        ddlModalPoderes.Enabled = False
        ddlModalEstado.Enabled = False
        txtModalObservaciones.Enabled = False
        txtModalFechaCuotaDCV.Enabled = False
        txtModalCuotaDCV.Enabled = False
        txtModalRescateTransito.Enabled = False
        txtModalSuscripcionTransito.Enabled = False
        txtModalCanjeTransito.Enabled = False
        txtModalCuotasDisponibles.Enabled = False
        ddlModalFijacionNav.Enabled = False
        ddlModalFijacionTC.Enabled = False
        txtModalFechaNavEntrante.Enabled = False
        txtModalTipoCambio.Enabled = False
        'lnkBtnModalFechaSolicitud.Enabled = False
        'LinkButton4.Enabled = False
        lnkbtnModalFechaObservado.Visible = False
        lnkbtnModalFechaNavSaliente.Visible = False
        lnkbtnModalFechaNavEntrante.Visible = False
        'lnkBtnModalFechaSolicitud.Visible = False
    End Sub

    Private Sub FormateoFormDatos(canje As CanjeDTO)

        txtIdCanje.Text = canje.IdCanje
        txtModalTipoTrnasaccion.Text = canje.TipoTransaccion
        ddlModalRutAportante.SelectedValue = canje.RutAportante

        If canje.Multifondo = "&nbsp;" Then
            ddlModalMultifondo.Text = ""
            ddlModalMultifondo.Enabled = False
        Else
            ddlModalMultifondo.Text = canje.Multifondo
        End If

        ddlModalNombreAportante.SelectedValue = canje.NombreAportante
        ddlModalFondo.SelectedValue = canje.RutFondo
        ddlModalNombreFondo.SelectedValue = canje.NombreFondo
        txtModalFechaNavSaliente.Text = canje.FechaNavSaliente
        txtModalFSolicitud.Text = canje.FechaSolicitud
        txtModalFechaObservado.Text = canje.FechaObservado

        txtModalFechaCanje.Text = canje.FechaCanje   ' IIf(canje.FechaCanjeDate = Nothing, "", canje.FechaCanjeDate)

        ddlModalNemotecnicoSaliente.SelectedValue = canje.NemotecnicoSaliente
        ddlModalSerieSaliente.SelectedValue = canje.NombreSerieSaliente
        ddlModalMonedaSaliente.SelectedValue = canje.MonedaSaliente
        txtModalCuotaSaliente.Text = Utiles.SetToCapitalizedNumber(Math.Floor(canje.CuotaSaliente))
        txtModalNavSaliente.Text = canje.NavSalienteFormat
        txtModalMontoSaliente.Text = Utiles.SetToCapitalizedNumber(Math.Round(canje.MontoSaliente, 2))
        txtModalNavCLPSaliente.Text = canje.NavCLPSalienteFormat

        txtModalMontoCLPSaliente.Text = Utiles.SetToCapitalizedNumber(Math.Truncate(canje.MontoCLPSaliente))
        txtModalFactor.Text = Utiles.SetToCapitalizedNumber(canje.Factor)
        txtFactorSaliente.Text = Utiles.SetToCapitalizedNumber(canje.Factor)

        txtModalDiferencia.Text = Utiles.formatearDiferencia(canje.Diferencia, canje.MonedaSaliente)
        txtModalDiferenciaCLP.Text = Utiles.formatearDiferenciaCLP(canje.DiferenciaCLP)

        ddlModalNemotecnicoEntrante.SelectedValue = canje.NemotecnicoEntrante
        ddlModalSerieEntrante.SelectedValue = canje.NombreSerieEntrante
        ddlModalMonedaEntrante.SelectedValue = canje.MonedaEntrante
        txtModalCuotaEntrante.Text = Utiles.SetToCapitalizedNumber(Math.Floor(canje.CuotaEntrante))
        txtModalNavEntrante.Text = canje.NavEntranteFormat

        If canje.MonedaEntrante = "CLP" Then
            txtModalMontoEntrante.Text = Utiles.formatearMonto(canje.MontoEntrante, canje.MonedaEntrante)
            ' String.Format("{0:N0}", canje.MontoEntrante)
            txtModalNavCLPEntrante.Text = canje.NavCLPEntranteFormat
            txtModalMontoCLPEntrante.Text = Utiles.formatearMontoCLP(canje.MontoCLPEntrante)
            ' String.Format("{0:N0}", canje.MontoCLPEntrante)
        Else
            txtModalMontoEntrante.Text = Utiles.formatearMonto(canje.MontoEntrante, canje.MonedaEntrante)
            ' String.Format("{0:N2}", canje.MontoEntrante)
            txtModalNavCLPEntrante.Text = canje.NavCLPEntranteFormat
            txtModalMontoCLPEntrante.Text = Utiles.formatearMontoCLP(canje.MontoCLPEntrante)
            'String.Format("{0:N2}", canje.MontoCLPEntrante)
        End If

        If canje.MonedaSaliente = "CLP" Then
            txtModalMontoSaliente.Text = Utiles.formatearMonto(canje.MontoSaliente, canje.MonedaSaliente)
            ' String.Format("{0:N0}", canje.MontoSaliente)
            txtModalNavCLPSaliente.Text = canje.NavCLPSalienteFormat
            txtModalMontoCLPSaliente.Text = Utiles.formatearMontoCLP(canje.MontoCLPSaliente)
            ' String.Format("{0:N0}", canje.MontoCLPSaliente)
        Else
            txtModalMontoSaliente.Text = Utiles.formatearMonto(canje.MontoSaliente, canje.MonedaSaliente)
            ' String.Format("{0:N2}", canje.MontoSaliente)
            txtModalNavCLPSaliente.Text = canje.NavCLPSalienteFormat
            txtModalMontoCLPSaliente.Text = Utiles.formatearMontoCLP(canje.MontoCLPSaliente)
            ' String.Format("{0:N2}", canje.MontoCLPSaliente)
        End If


        If canje.ContratoGeneral = "&nbsp;" Then
            ddlModalContrato.SelectedValue = ""
        Else
            ddlModalContrato.SelectedValue = canje.ContratoGeneral
        End If
        If canje.RevisionPoderes = "&nbsp;" Then
            ddlModalPoderes.SelectedValue = ""
        Else
            ddlModalPoderes.SelectedValue = canje.RevisionPoderes
        End If
        ddlModalEstado.SelectedValue = canje.EstadoCanje
        If canje.Observaciones = "&amp;nbsp;" Or canje.Observaciones = "&nbsp;" Then
            txtModalObservaciones.Text = ""
        Else
            txtModalObservaciones.Text = canje.Observaciones
        End If
        txtModalFechaCuotaDCV.Text = canje.FechaActual
        txtModalCuotaDCV.Text = Utiles.SetToCapitalizedNumber(canje.Cuotas)
        txtModalRescateTransito.Text = Utiles.SetToCapitalizedNumber(canje.RescateTransito)
        txtModalSuscripcionTransito.Text = Utiles.SetToCapitalizedNumber(canje.SuscripcionTransito)
        txtModalCanjeTransito.Text = Utiles.SetToCapitalizedNumber(Integer.Parse(canje.CanjeTransito))
        txtModalCuotasDisponibles.Text = Utiles.SetToCapitalizedNumber(Integer.Parse(canje.CuotasDisponibles))
        ddlModalFijacionNav.SelectedValue = canje.FijacionNav
        ddlModalFijacionTC.SelectedValue = canje.FijacionTC
        txtModalFechaNavEntrante.Text = canje.FechaNavEntrante
        txtModalTipoCambio.Text = canje.TipoCambio
        txtEstadoCambio.Value = Utiles.SetToCapitalizedNumber(canje.Estado)
    End Sub

    Protected Sub BtnExportar_Click(sender As Object, e As EventArgs) Handles BtnExportar.Click
        Dim canje As CanjeDTO = New CanjeDTO()
        Dim fechaSolicitudHasta As Nullable(Of Date)
        Dim fechaNavHasta As Nullable(Of Date)
        Dim fechaCanjeHasta As Nullable(Of Date)

        If ddlListaRutAportante.SelectedValue.Trim() = Nothing Then
            canje.NombreAportante = ""
        Else
            Dim arrCadena As String() = ddlListaRutAportante.SelectedItem.Text().Split(New Char() {"/"c})
            canje.NombreAportante = arrCadena(1).Trim()
        End If

        If ddlListaRutFondo.SelectedValue.Trim() = Nothing Then
            canje.NombreFondo = ""
        Else
            Dim arrCadena As String() = ddlListaRutFondo.SelectedItem.Text().Split(New Char() {"/"c})
            canje.NombreFondo = arrCadena(1).Trim()
        End If
        If ddlListaNemotecnico.SelectedValue.Trim() = Nothing Then
            canje.NemotecnicoSaliente = ""
        Else
            canje.NemotecnicoSaliente = ddlListaNemotecnico.SelectedValue.Trim()
        End If
        If ddlEstado.SelectedValue.Trim() = Nothing Then
            canje.EstadoCanje = ""
        Else
            canje.EstadoCanje = ddlEstado.SelectedValue.Trim()
        End If

        If Not txtFechaSolicitudDesde.Text.Equals("") Then
            canje.FechaSolicitud = Date.Parse(txtFechaSolicitudDesde.Text)
        Else
            canje.FechaSolicitud = Nothing
        End If

        If Not txtFechaSolicitudHasta.Text.Equals("") Then
            fechaSolicitudHasta = Date.Parse(txtFechaSolicitudDesde.Text)
        Else
            fechaSolicitudHasta = Nothing
        End If

        If Not txtFechaNavDesde.Text.Equals("") Then
            canje.FechaNavSaliente = Date.Parse(txtFechaSolicitudDesde.Text)
        Else
            canje.FechaNavSaliente = Nothing
        End If

        If Not txtFechaNavDesde.Text.Equals("") Then
            fechaNavHasta = Date.Parse(txtFechaNavDesde.Text)
        Else
            fechaNavHasta = Nothing
        End If

        If Not txtFechaCanjeDesde.Text.Equals("") Then
            canje.FechaCanjeDate = Date.Parse(txtFechaCanjeDesde.Text)
        Else
            canje.FechaCanjeDate = Nothing
        End If

        If Not txtFechaCanjeHasta.Text.Equals("") Then
            fechaCanjeHasta = Date.Parse(txtFechaCanjeHasta.Text)
        Else
            fechaCanjeHasta = Nothing
        End If

        Dim mensaje As String = NegocioCanje.ExportarExcel(canje, fechaSolicitudHasta, fechaNavHasta, fechaCanjeHasta)

        If NegocioCanje.rutaArchivoExcel IsNot Nothing Then
            Archivo.NavigateUrl = NegocioCanje.rutaArchivoExcel
            Archivo.Text = "Bajar Archivo"
        Else
            Archivo.Visible = False
        End If

        txtAccionHidden.Value = "MOSTRAR_DIALOGO"

        ShowMessages(CONST_TITULO_CANJE, mensaje, Constantes.CONST_RUTA_IMG + Constantes.CONST_IMG_LOGO, Constantes.CONST_RUTA_IMG + Constantes.CONST_IMG_INFO, False)
    End Sub

    Private Sub btnPrueba_Click(sender As Object, e As EventArgs) Handles btnPrueba.Click
        GenerarPopUp()
    End Sub

    Private Sub GenerarPopUp()
        Dim Canje As CanjeDTO = New CanjeDTO()
        Canje.IdCanje = txtIdCanje.Text

        Dim negocioMod As CanjeNegocio = New CanjeNegocio()

        FillPopUp(negocioMod.GetCanje(Canje))
        txtAccionHidden.Value = "POPUPCANJES"

    End Sub

    Private Sub FillPopUp(Canje As CanjeDTO)
        lblPopUpFechaSolicitud.Text = Canje.FechaSolicitud.ToShortDateString
        lblPopUpHoraSolicitud.Text = Canje.FechaSolicitud.ToShortTimeString
        lblPopUpTipo.Text = Canje.TipoTransaccion
        lblPopUpNemoFondo.Text = Canje.NemotecnicoEntrante
        lblPopUpNombreFondo.Text = Canje.NombreFondo
        lblPopUpMonedaDelFondo.Text = Canje.MonedaEntrante
        lblPopUpAdministradora.Text = "En Validacion con Moneda" 'VALIDAR MONEDA
        lblPopUpRutAdministradora.Text = "En Validacion con Moneda" 'VALIDAR MONEDA
        lblPopUpNombreAportante.Text = Canje.NombreAportante
        lblPopUpRutAportante.Text = Canje.RutAportante
        lblPopUpFechaDeCanje.Text = Canje.FechaCanje
        lblPopUpFechaDeNav.Text = Canje.FechaNavSaliente
        lblPopUpSerieSaliente.Text = Canje.NombreSerieSaliente
        lblPopUpCuotasSalientes.Text = Canje.CuotaSaliente
        lblPopUpNavSaliente.Text = "Por Confirmar"
        lblPopUpMontoSaliente.Text = "Por Confirmar"

        lblPopUpSerieEntrante.Text = Canje.NombreSerieEntrante
        lblPopUpCuotasEntrantes.Text = "Por Confirmar"
        lblPopUpNavEntrante.Text = "Por Confirmar"
        lblPopUpMontoEntrante.Text = "Por Confirmar"
        lblPopUpFactor.Text = "Por Confirmar"
        lblPopUpRemanenteADevolver.Text = "Por Confirmar"
        lblPopUpContratoGralDeFondos.Text = Canje.ContratoGeneral
        lblPopUpPoderes.Text = Canje.RevisionPoderes

    End Sub
#Region "Listas de Busqueda"
    Protected Sub ddlListaRutAportante_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlListaRutAportante.SelectedIndexChanged
        Dim canje As New CanjeDTO

        canje = GetRutFondoParametro(canje)
        canje = GetNemotecnicoParametro(canje)
        canje = GetRutAportanteParametro(canje)

        If ddlListaRutFondo.SelectedValue.ToString().Trim() = "" Then
            llenarComboFondosBusqueda(canje)
        End If

        If ddlListaNemotecnico.SelectedValue.ToString().Trim() = "" Then
            llenarComboNemotecnicos(canje)
        End If

    End Sub

    Protected Sub ddlListaRutFondo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlListaRutFondo.SelectedIndexChanged
        Dim canje As New CanjeDTO

        canje = GetRutFondoParametro(canje)
        canje = GetNemotecnicoParametro(canje)
        canje = GetRutAportanteParametro(canje)

        If ddlListaNemotecnico.SelectedValue.ToString().Trim() = "" Then
            llenarComboNemotecnicos(canje)
        End If

        If (ddlListaRutAportante.SelectedValue.ToString().Trim() = "") Then
            llenarComboAportanteBusqueda(canje)
        End If


    End Sub

    Protected Sub ddlListaNemotecnico_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlListaNemotecnico.SelectedIndexChanged
        Dim canje As New CanjeDTO

        canje = GetRutFondoParametro(canje)
        canje = GetNemotecnicoParametro(canje)
        canje = GetRutAportanteParametro(canje)

        If ddlListaRutFondo.SelectedValue.ToString().Trim() = "" Then
            llenarComboFondosBusqueda(canje)
        End If

        If (ddlListaRutAportante.SelectedValue.ToString().Trim() = "") Then
            llenarComboAportanteBusqueda(canje)
        End If
    End Sub

    Private Sub llenarComboAportanteBusqueda(canje As CanjeDTO)
        Dim lista As List(Of CanjeDTO)
        Dim canjeVacio As New CanjeDTO

        lista = NegocioCanje.ConsultarAportante(canje)

        If lista.Count = 0 Then
            lista.Add(canjeVacio)
        Else
            lista.Insert(0, canjeVacio)
        End If

        ddlListaRutAportante.Items.Clear()

        ddlListaRutAportante.DataSource = lista
        ddlListaRutAportante.DataMember = "NombreAportanteBusqueda"
        ddlListaRutAportante.DataValueField = "NombreAportanteBusqueda"
        ddlListaRutAportante.DataBind()
        ddlListaRutAportante.Items.Insert(0, New ListItem(0, String.Empty))
    End Sub

    Private Sub llenarComboNemotecnicos(canje As CanjeDTO)
        Dim lista As List(Of CanjeDTO)
        Dim canjeVacio As CanjeDTO = New CanjeDTO()

        lista = NegocioCanje.ConsultarNemotecnico(canje)

        If lista.Count = 0 Then
            lista.Add(canjeVacio)
        Else
            lista.Insert(0, canjeVacio)
        End If

        ddlListaNemotecnico.Items.Clear()

        ddlListaNemotecnico.DataSource = lista
        ddlListaNemotecnico.DataMember = "NemotecnicoBusqueda"
        ddlListaNemotecnico.DataValueField = "NemotecnicoBusqueda"
        ddlListaNemotecnico.DataBind()
        ddlListaNemotecnico.Items.Insert(0, New ListItem(0, String.Empty))
    End Sub

    Private Sub llenarComboFondosBusqueda(canje As CanjeDTO)

        Dim lista As List(Of CanjeDTO)
        Dim canjeVacio As CanjeDTO = New CanjeDTO()

        lista = NegocioCanje.ConsultarFondo(canje)

        If lista.Count = 0 Then
            lista.Add(canjeVacio)
        Else
            lista.Insert(0, canjeVacio)
        End If

        ddlListaRutFondo.Items.Clear()

        ddlListaRutFondo.DataSource = lista
        ddlListaRutFondo.DataMember = "FondoBusqueda"
        ddlListaRutFondo.DataValueField = "FondoBusqueda"
        ddlListaRutFondo.DataBind()
        ddlListaRutFondo.Items.Insert(0, New ListItem(0, String.Empty))
    End Sub

    Private Function GetRutAportanteParametro(canje As CanjeDTO) As CanjeDTO
        Dim nombreAportante As String
        Dim rutAportante As String
        Dim arrCadena As String()

        If (ddlListaRutAportante.SelectedValue.ToString().Trim() <> "") Then

            arrCadena = ddlListaRutAportante.SelectedItem.Text().Split(New Char() {"/"c})

            If arrCadena.Length = 2 Then
                rutAportante = arrCadena(0).Trim()
                nombreAportante = arrCadena(1).Trim()

                canje.RutAportante = rutAportante
            End If
        End If
        Return canje
    End Function

    Private Function GetNemotecnicoParametro(ByRef canje As CanjeDTO) As CanjeDTO
        If ddlListaNemotecnico.SelectedValue.ToString().Trim() <> "" Then
            canje.NemotecnicoSaliente = ddlListaNemotecnico.SelectedValue.ToString().Trim()
        End If
        Return canje
    End Function

    Private Function GetRutFondoParametro(ByRef canje As CanjeDTO) As CanjeDTO
        Dim nombreFondo As String
        Dim rutFondo As String
        Dim arrCadena As String()

        If ddlListaRutFondo.SelectedValue.ToString().Trim() <> "" Then
            arrCadena = ddlListaRutFondo.SelectedItem.Text().Split(New Char() {"/"c})

            If arrCadena.Length = 2 Then
                rutFondo = arrCadena(0).Trim()
                nombreFondo = arrCadena(1).Trim()

                canje.RutFondo = rutFondo
            End If

        End If

        Return canje
    End Function
#End Region

#Region "Basuras"
    '--FIN LISTAS DESPLEGABLES MODAL SERIE--

    '--MOSTRAR CALENDARIOS FORMULARIO--

    'Protected Sub lnkbtnFechaSolicitudDesde_Click(sender As Object, e As EventArgs)
    '    txtAccionHidden.Value = ""
    '    CalendarFechaSolicitudDesde.Visible = (Not CalendarFechaSolicitudDesde.Visible)
    'End Sub

    'Protected Sub BtnLimpiarFechaHasta_Click(sender As Object, e As EventArgs)
    '    txtFechaSolicitudDesde.Text = ""
    'End Sub

    'Private Sub CalendarFechaSolicitudDesde_SelectionChanged(sender As Object, e As EventArgs) Handles CalendarFechaSolicitudDesde.SelectionChanged
    '    txtFechaSolicitudDesde.Text = CalendarFechaSolicitudDesde.SelectedDate.ToShortDateString()
    '    If txtFechaSolicitudDesde.Text <> "" And txtFechaSolicitudHasta.Text <> "" Then
    '        If Date.Parse(txtFechaSolicitudHasta.Text) < Date.Parse(txtFechaSolicitudDesde.Text) Then
    '            txtFechaSolicitudHasta.Text = CalendarFechaSolicitudDesde.SelectedDate.ToShortDateString()
    '        End If
    '    End If
    '    CalendarFechaSolicitudDesde.SelectedDate = Nothing
    '    CalendarFechaSolicitudDesde.Visible = False
    'End Sub
    'Protected Sub CalendarFechaSolicitudHasta_DayRender(sender As Object, e As DayRenderEventArgs) Handles CalendarFechaSolicitudHasta.DayRender
    '    If Not txtFechaSolicitudDesde.Text.Equals("") Then
    '        If e.Day.Date < DateTime.Parse(txtFechaSolicitudDesde.Text) Then
    '            e.Day.IsSelectable = False
    '            e.Cell.ForeColor = System.Drawing.Color.Gray
    '        End If
    '    End If
    'End Sub
    'Protected Sub lkbtnFechaSolicitudHasta_Click(sender As Object, e As EventArgs)
    '    txtAccionHidden.Value = ""
    '    CalendarFechaSolicitudHasta.Visible = (Not CalendarFechaSolicitudHasta.Visible)
    'End Sub

    'Protected Sub LinkButton1_Click(sender As Object, e As EventArgs)
    '    txtFechaSolicitudHasta.Text = ""
    'End Sub

    'Private Sub CalendarFechaSolicitudHasta_SelectionChanged(sender As Object, e As EventArgs) Handles CalendarFechaSolicitudHasta.SelectionChanged
    '    txtFechaSolicitudHasta.Text = CalendarFechaSolicitudHasta.SelectedDate.ToShortDateString()

    '    If txtFechaSolicitudHasta.Text <> "" And txtFechaSolicitudDesde.Text <> "" Then
    '        If Date.Parse(txtFechaSolicitudHasta.Text) < Date.Parse(txtFechaSolicitudDesde.Text) Then
    '            txtFechaSolicitudDesde.Text = CalendarFechaSolicitudHasta.SelectedDate.ToShortDateString()
    '        End If
    '    End If
    '    CalendarFechaSolicitudHasta.SelectedDate = Nothing
    '    CalendarFechaSolicitudHasta.Visible = False
    'End Sub

    'Protected Sub lkbtnFechaNavDesde_Click(sender As Object, e As EventArgs)
    '    txtAccionHidden.Value = ""
    '    CalendarFechaNavDesde.Visible = (Not CalendarFechaNavDesde.Visible)
    'End Sub

    'Protected Sub LinkButton2_Click(sender As Object, e As EventArgs)
    '    txtFechaNavDesde.Text = ""
    'End Sub

    'Private Sub CalendarFechaNavDesde_SelectionChanged(sender As Object, e As EventArgs) Handles CalendarFechaNavDesde.SelectionChanged
    '    txtFechaNavDesde.Text = CalendarFechaNavDesde.SelectedDate.ToShortDateString()
    '    If txtFechaNavDesde.Text <> "" And txtFechaNavHasta.Text <> "" Then
    '        If Date.Parse(txtFechaNavHasta.Text) < Date.Parse(txtFechaNavDesde.Text) Then
    '            txtFechaNavHasta.Text = CalendarFechaNavDesde.SelectedDate.ToShortDateString()
    '        End If
    '    End If
    '    CalendarFechaNavDesde.SelectedDate = Nothing
    '    CalendarFechaNavDesde.Visible = False
    'End Sub

    'Protected Sub lkbtnFechaNavHasta_Click(sender As Object, e As EventArgs)
    '    txtAccionHidden.Value = ""
    '    CalendarFechaNavHasta.Visible = (Not CalendarFechaNavHasta.Visible)
    'End Sub

    'Protected Sub LinkButton3_Click(sender As Object, e As EventArgs)
    '    txtFechaNavHasta.Text = ""
    'End Sub

    'Private Sub CalendarFechaNavHasta_SelectionChanged(sender As Object, e As EventArgs) Handles CalendarFechaNavHasta.SelectionChanged
    '    txtFechaNavHasta.Text = CalendarFechaNavHasta.SelectedDate.ToShortDateString()
    '    CalendarFechaNavHasta.SelectedDate = Nothing
    '    CalendarFechaNavHasta.Visible = False
    'End Sub

    'Protected Sub CalendarFechaNavHasta_DayRender(sender As Object, e As DayRenderEventArgs) Handles CalendarFechaNavHasta.DayRender
    '    If Not txtFechaNavDesde.Text.Equals("") Then
    '        If e.Day.Date < DateTime.Parse(txtFechaNavDesde.Text) Then
    '            e.Day.IsSelectable = False
    '            e.Cell.ForeColor = System.Drawing.Color.Gray
    '        End If
    '    End If
    'End Sub

    'Protected Sub lnkBtnModalFechaSolicitud_Click(sender As Object, e As EventArgs)
    '    'CalendarModalFechaSolicitud.Visible = (Not CalendarModalFechaSolicitud.Visible)
    'End Sub

    'Protected Sub LinkButton4_Click(sender As Object, e As EventArgs)
    '    txtModalFSolicitud.Text = ""
    'End Sub

    'Private Sub CalendarModalFechaNavEntrante_SelectionChanged(sender As Object, e As EventArgs) Handles CalendarModalFechaNavEntrante.SelectionChanged
    '    txtModalFechaNavEntrante.Text = CalendarModalFechaNavEntrante.SelectedDate.ToShortDateString()
    '    txtModalFechaNavSaliente.Text = CalendarModalFechaNavEntrante.SelectedDate.ToShortDateString()

    '    CalcularValorEntrante()
    '    CalcularValorSaliente()

    '    CalendarModalFechaNavEntrante.SelectedDate = Nothing
    '    CalendarModalFechaNavEntrante.Visible = False


    'End Sub
    'Private Sub CalendarModalFechaNavSaliente_SelectionChanged(sender As Object, e As EventArgs) Handles CalendarModalFechaNavSaliente.SelectionChanged
    '    txtModalFechaNavSaliente.Text = CalendarModalFechaNavSaliente.SelectedDate.ToShortDateString()
    '    txtModalFechaNavEntrante.Text = CalendarModalFechaNavSaliente.SelectedDate.ToShortDateString()

    '    CalcularValorSaliente()
    '    CalcularValorEntrante()
    '    CalendarModalFechaNavSaliente.SelectedDate = Nothing
    '    CalendarModalFechaNavSaliente.Visible = False
    'End Sub

    'Protected Sub lnkbtnModalFechaNavEntrante_Click(sender As Object, e As EventArgs)
    '    CalendarModalFechaNavEntrante.Visible = (Not CalendarModalFechaNavEntrante.Visible)
    'End Sub

    'Private Sub CalendarModalFechaSolicitud_SelectionChanged(sender As Object, e As EventArgs) Handles CalendarModalFechaSolicitud.SelectionChanged
    '    'txtModalFSolicitud.Text = CalendarModalFechaSolicitud.SelectedDate.ToShortDateString()

    '    ConsultarFechaNavSaliente()
    '    ConsultarFechaNavEntrante()
    '    ConsultarFechaObservado()
    '    CalcularCuotaDCV()

    '    'CalendarModalFechaSolicitud.SelectedDate = Nothing
    '    'CalendarModalFechaSolicitud.Visible = False
    'End Sub


    'Protected Sub lnkbtnModalFechaObservado_Click(sender As Object, e As EventArgs)
    '    CalendarModalFechaObservado.Visible = (Not CalendarModalFechaObservado.Visible)
    'End Sub


    'Protected Sub lnkbtnModalFechaCanje_Click(sender As Object, e As EventArgs)
    '    CalendarModalFechaCanje.Visible = (Not CalendarModalFechaCanje.Visible)
    'End Sub
    'Private Sub CalendarModalFechaObservado_SelectionChanged(sender As Object, e As EventArgs) Handles CalendarModalFechaObservado.SelectionChanged
    '    Dim negocioRescate As VentanasRescateNegocio = New VentanasRescateNegocio
    '    Dim fechaValidar As String

    '    txtModalFechaObservado.Text = CalendarModalFechaObservado.SelectedDate.ToShortDateString()
    '    fechaValidar = negocioRescate.ValidaDiaHabil(txtModalFechaObservado.Text)

    '    If fechaValidar = "Festivo" Then
    '        txtModalFechaObservado.Text = ""
    '        ShowAlert("La fecha seleccionada es un día feriado")
    '        Return
    '    ElseIf fechaValidar = "No_Habil" Then
    '        txtModalFechaObservado.Text = ""
    '        ShowAlert("La fecha seleccionada no es hábil")
    '        Return
    '    End If

    '    'CalcularTipo()
    '    ConversionMoneda()

    '    CalendarModalFechaObservado.SelectedDate = Nothing
    '    CalendarModalFechaObservado.Visible = False
    'End Sub
#End Region

End Class
