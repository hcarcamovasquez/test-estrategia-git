Imports DTO
Imports Negocio
Imports System.Math
Imports System.Data

Imports DBSUtils

Partial Class Presentacion_Mantenedores_frmMantenedorSuscripciones
    Inherits System.Web.UI.Page
    Public Const CONST_INHABIL_PARA_TC As String = "La fecha TC es inhábil en la moneda. Se moverá al hábil siguiente"

#Region "DATA INICIAL"
    Private Sub Mantenedores_Aportantes_Maestro_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            DataInitial()
        End If
    End Sub

    Private Sub CargaDatosModalInicial()
        CargaFiltroRutAportanteModal()
        CargaFiltroRutAportanteS()
        CargaFiltroRutFondo()
        CargaNemotecnico()
        CargaFiltroMultifondoAportante()
        CargaFiltroNombreAportante()
    End Sub
    Private Sub DataInitial()
        CargaDatosModalInicial()
        ValidaPermisosPerfil()
        txtFechaIntencion.Text = Date.Now.ToString("dd/MM/yyyy")

        ddlEstado1.Items.Insert(0, "Pendiente")
        ddlEstado1.Items.Insert(1, "Cerrado")
        ddlEstado1.Items.Insert(0, New ListItem("", ""))
        ddlEstado.Items.Insert(0, StrDup(50, " "))
        ddlEstado.Items.Insert(1, "Pendiente")
        ddlEstado.Items.Insert(2, "Cerrado")
        ddlEstado.Items.Insert(0, New ListItem("", ""))
        'Utiles.cargarMonedas(ddlMonedaPago, "")

        ddlMonedaPago.Items.Insert(0, "")
        ddlMonedaPago.Items.Insert(1, "USD")
        ddlMonedaPago.Items.Insert(2, "CLP")
        ddlMonedaPago.Items.Insert(3, "EUR")
        ddlMonedaPago.Items.Insert(0, New ListItem("", ""))

        ddlContrato.Items.Insert(0, "")
        ddlContrato.Items.Insert(1, "OK")
        ddlContrato.Items.Insert(2, "NO OK")
        ddlContrato.Items.Insert(0, New ListItem("", ""))
        ddlPoderes.Items.Insert(0, "")
        ddlPoderes.Items.Insert(1, "OK")
        ddlPoderes.Items.Insert(2, "NO OK")
        ddlPoderes.Items.Insert(0, New ListItem("", ""))
        GrvTabla.DataSource = New List(Of AportantesXGrupoDTO)
        GrvTabla.DataBind()
        BtnExportar.Enabled = (GrvTabla.Rows.Count <> 0)

    End Sub
#End Region
#Region "Constantes"
    Public Const CONST_TITULO_SUSCRIPCION As String = "Suscripción"
    Public Const CONST_TITULO_MODAL_MODIFICAR As String = "Modificar suscripción"
    Public Const CONST_MODIFICAR_ERROR As String = "Error al modificar los datos de la suscripción"
    Public Const CONST_MODIFICAR_EXITO As String = "Suscripción modificada con éxito"
    Public Const CONST_ELIMINAR_ERROR As String = "Error al eliminar la suscripción seleccionada"
    Public Const CONST_ELIMINAR_EXITO As String = "Suscripción eliminada con éxito"
    Public Const CONST_ELIMINAR_ESTADO_CERO As String = "No se puede eliminar un registro ya deshabilitado"
    Public Const CONST_ELIMINAR_EXISTE_EN_OTRA_TABLA As String = "Suscripción se encuentra relacionada en otra Tabla"
    Public Const CONST_TITULO_MODAL_ELIMINAR As String = "Eliminar suscripción"
    Public Const CONST_TITULO_MODAL_CREAR As String = "Nueva suscripción"
    Public Const CONST_NEMOTECNICO_EXISTE As String = "El Nemotécnico ya existe"
    Public Const CONST_ERROR_AL_GUARDAR As String = "Error al guardar la suscripción"
    Public Const CONST_EXITO_AL_GUARDAR As String = "Suscripción guardada con éxito"
    Public Const CONST_SIN_RESULTADOS As String = "No se obtuvieron resultados de la búsqueda"
    Public Const CONST_SIN_RESULTADOS_MODAL As String = "Ningún dato coincide con la opción seleccionada"
    Public Const CONST_MAXIMO As String = "EL Monto Máximo debe ser mayor o igual que el Monto Mínimo"
    Public Const CONST_SUSCRIPCION_EXISTE As String = "Suscripción ya existe"

    Public Const CONST_COL_IDSUSCRIPCION As Integer = 1
    Public Const CONST_COL_TIPOTRANSACCION As Integer = 2
    Public Const CONST_COL_FECHAINTENCION As Integer = 3
    Public Const CONST_COL_RUTAPORTANTE As Integer = 4
    Public Const CONST_COL_RAZONSOCIAL As Integer = 5
    Public Const CONST_COL_MULTIFONDO As Integer = 6
    Public Const CONST_COL_RUTFONDO As Integer = 7
    Public Const CONST_COL_FNNOMBRECORTO As Integer = 8
    Public Const CONST_COL_NEMOTECNICO As Integer = 9
    Public Const CONST_COL_FSNOMBRECORTO As Integer = 10
    Public Const CONST_COL_FSMONEDA As Integer = 11
    Public Const CONST_COL_CUOTASASUSCRIBIR As Integer = 12
    Public Const CONST_COL_MONEDA_PAGO As Integer = 13
    Public Const CONST_COL_FECHANAV As Integer = 14
    Public Const CONST_COL_FECHASUSCRIPCION As Integer = 15
    Public Const CONST_COL_FECHATC As Integer = 16
    Public Const CONST_COL_NAV As Integer = 17
    Public Const CONST_COL_MONTO As Integer = 18
    Public Const CONST_COL_NAVCLP As Integer = 19
    Public Const CONST_COL_MONTOCLP As Integer = 20
    Public Const CONST_COL_CONTRATOFONDO As Integer = 21
    Public Const CONST_COL_REVISIONPODERES As Integer = 22
    Public Const CONST_COL_OBSERVACIONES As Integer = 23
    Public Const CONST_COL_FECHADCV As Integer = 24
    Public Const CONST_COL_CUOTASDCV As Integer = 25
    Public Const CONST_COL_RESCATESTRANSITOS As Integer = 26
    Public Const CONST_COL_SUSCRIPCIONESTRANSITO As Integer = 27
    Public Const CONST_COL_CANJESTRANSITO As Integer = 28
    Public Const CONST_COL_CUOTASDISPONIBLES As Integer = 29
    Public Const CONST_COL_FIJACIONNAV As Integer = 30
    Public Const CONST_COL_TCOBSERVADO As Integer = 31
    Public Const CONST_COL_FIJACIONTC As Integer = 32
    Public Const CONST_COL_ESTADOSUSCRIPCION As Integer = 33
    Public Const CONST_COL_CUOTAEMITIDA As Integer = 34
    Public Const CONST_COL_ACUMULADA As Integer = 35
    Public Const CONST_COL_ACTUAL As Integer = 36
    Public Const CONST_COL_UTILIZADA As Integer = 37
    Public Const CONST_COL_DISPONIBLES As Integer = 38
    Public Const CONST_COL_FECHAINGRESO As Integer = 39
    Public Const CONST_COL_USUARIOINGRESO As Integer = 40
    Public Const CONST_COL_FECHAMODIFICACION As Integer = 41
    Public Const CONST_COL_USUARIOMODIFICACION As Integer = 42


#End Region
    Dim Negocio As SuscripcionNegocio = New SuscripcionNegocio
#Region "Click Botones"


#Region "Botones modal"
    Private Sub btnModalGuardar_Click(sender As Object, e As EventArgs) Handles btnModalGuardar.Click
        If (Double.Parse(txtNAV.Text) > 99999999999999 Or Double.Parse(txtMonto.Text) > 99999999999999 Or Double.Parse(txtMontoCLP.Text) > 99999999999999 Or Double.Parse(txtCuotas.Text) > 99999999999999 Or Double.Parse(txtNAV.Text) > 99999999999999 Or Double.Parse(txtTCObservado.Text) > 99999999999999) Then
            ShowAlert("Los campos NAV, NAV (CLP), Monto, Monto(CLP), Cuotas y Tc observado no pueden ser mayores a 99999999999999, verifique por favor")
        ElseIf (txtNAV.Text = 0 Or txtMonto.Text = 0 Or txtMontoCLP.Text = 0 Or txtCuotas.Text = 0 Or txtNAV.Text = 0 Or txtTCObservado.Text = 0) Then
            ShowAlert("Los campos NAV, NAV (CLP), Monto, Monto(CLP), Cuotas y Tc observado deben ser mayores a 0, verifique por favor")
        ElseIf (txtCuotas.Text = "0" And txtCuotasEmitidas.Text > 0) Then
            ShowAlert("Debe ingresar un valor mayor a 0 en cuotas")
        Else
            Dim Suscripcion As SuscripcionDTO = GetSuscripcionModal()
            Suscripcion = Negocio.InsertSuscripcion(Suscripcion)
            txtIdSuscripcion.Text = Suscripcion.IdSuscripcion

            If txtIdSuscripcion.Text > 0 Then
                'Ingresado con éxito
                ShowAlert(CONST_EXITO_AL_GUARDAR)
                GenerarPopUp()
            Else
                'Error en la BBDD
                ShowAlert(CONST_ERROR_AL_GUARDAR)
            End If
            FormateoLimpiarDatosModal()
            'txtAccionHidden.Value = ""
            FormateoLimpiarForm()
        End If


    End Sub
    Private Sub btnModalCancelar_Click(sender As Object, e As EventArgs) Handles btnModalCancelar.Click
        FormateoLimpiarDatosModal()
        txtAccionHidden.Value = ""
    End Sub
    Private Sub btnModalModificar_Click(sender As Object, e As EventArgs) Handles btnModalModificar.Click
        'MODIFICAR  
        If (Double.Parse(txtNAV.Text) > 99999999999999 Or Double.Parse(txtMonto.Text) > 99999999999999 Or Double.Parse(txtMontoCLP.Text) > 99999999999999 Or Double.Parse(txtCuotas.Text) > 99999999999999 Or Double.Parse(txtNAV.Text) > 99999999999999 Or Double.Parse(txtTCObservado.Text) > 99999999999999) Then
            ShowAlert("Los campos NAV, NAV (CLP), Monto, Monto(CLP), Cuotas y Tc observado no pueden ser mayores a 99999999999999, verifique por favor")
        ElseIf (txtNAV.Text = 0 Or txtMonto.Text = 0 Or txtMontoCLP.Text = 0 Or txtCuotas.Text = 0 Or txtNAV.Text = 0 Or txtTCObservado.Text = 0) Then
            ShowAlert("Los campos NAV, Monto, Cuotas y Tc observado deben ser mayores a 0, verifique por favor")
        ElseIf (txtCuotas.Text = "0" And txtCuotasEmitidas.Text > 0) Then
            ShowAlert("Debe ingresar un valor mayor a 0 en cuotas")
        Else

            Dim negocioMod As SuscripcionNegocio = New SuscripcionNegocio
            Dim Suscripcion As SuscripcionDTO = GetSuscripcionModal()
            Dim solicitudMod As Integer = negocioMod.updatesuscripcion(Suscripcion)

            If solicitudMod = Constantes.CONST_OPERACION_EXITOSA Then
                ShowAlert(CONST_MODIFICAR_EXITO)
                GenerarPopUp()
            Else
                ShowAlert(CONST_MODIFICAR_ERROR)
            End If
            findsuscripcion()
            FormateoLimpiarDatosModal()
            FormateoLimpiarForm()

        End If
    End Sub
    Private Sub btnModalEliminar_Click(sender As Object, e As EventArgs) Handles btnModalEliminar.Click
        Dim Suscripcion As SuscripcionDTO = GetSuscripcionModal()
        Dim Negocio As SuscripcionNegocio = New SuscripcionNegocio
        If Not Suscripcion.Estado = 0 Then
            Dim solicitud As Integer = Negocio.DeleteSuscripcion(Suscripcion)

            If solicitud = Constantes.CONST_OPERACION_EXITOSA Then
                ShowAlert(CONST_ELIMINAR_EXITO)
            ElseIf solicitud = Constantes.CONST_ERROR_NO_SE_PUEDE_BORRAR Then
                ShowAlert(CONST_ELIMINAR_EXISTE_EN_OTRA_TABLA)

            Else
                ShowAlert(CONST_ELIMINAR_ERROR)
            End If
        Else
            ShowAlert(CONST_ELIMINAR_ESTADO_CERO)
        End If
        txtAccionHidden.Value = ""
        FormateoLimpiarDatosModal()
        FormateoLimpiarForm()
    End Sub
#End Region

    Protected Sub btnLimpiarFrm_Click(sender As Object, e As EventArgs)
        FormateoLimpiarForm()
        txtAccionHidden.Value = ""
    End Sub
    Protected Sub btnCrear_Click(sender As Object, e As EventArgs) Handles btnCrear.Click
        FormateoLimpiarDatosModal()
        FormateoEstiloFormCrear()
        'CargarCuotasDCV()
        txtAccionHidden.Value = "CREAR"
        LLenarIdSuscripcion()
    End Sub
    Protected Sub btnModificar_Click(sender As Object, e As EventArgs) Handles BtnModificar.Click
        Dim SuscripcionActualizada As SuscripcionDTO

        Dim SuscripcionSelected As SuscripcionDTO = GetSuscripcionSelect()
        Dim Relacion As SuscripcionDTO = Negocio.GetRelaciones(SuscripcionSelected)

        If (Relacion.CountAP > 0) Then
            ShowAlert("No se puede modificar la suscripción, información del aportante se modificó")
            txtAccionHidden.Value = ""
        ElseIf (Relacion.CountFN > 0) Then
            ShowAlert("No se puede modificar esta suscripción, información del fondo se modificó")
            txtAccionHidden.Value = ""
        ElseIf (Relacion.CountFS > 0) Then
            ShowAlert("No se puede modificar esta suscripción, información de la serie se modificó")
            txtAccionHidden.Value = ""
        Else
            SuscripcionActualizada = Negocio.GetSuscripcion(SuscripcionSelected)
            CargaDatosModalInicial()
            FormateoFormDatos(SuscripcionActualizada)
            FormateoEstiloFormModificar()
            'CargarNombreMultifondoPorRut()
            If ddlModalMultifondo.SelectedValue = "" Then
                ddlModalMultifondo.Enabled = False
            End If
            txtAccionHidden.Value = "MODIFICAR"
        End If
    End Sub
    Protected Sub BtnEliminar_Click(sender As Object, e As EventArgs) Handles BtnEliminar.Click

        Dim SuscripcionSelected As SuscripcionDTO = GetSuscripcionSelect()
        Dim SuscripcionActualizada As SuscripcionDTO = Negocio.GetSuscripcion(SuscripcionSelected)
        Dim Relacion As SuscripcionDTO = Negocio.GetRelaciones(SuscripcionSelected)
        If (Relacion.CountAP > 0) Then
            ShowAlert("No se puede eliminar la suscripción, información del aportante se modificó")
            txtAccionHidden.Value = ""
        ElseIf (Relacion.CountFN > 0) Then
            ShowAlert("No se puede eliminar esta suscripción, información del fondo se modificó")
            txtAccionHidden.Value = ""
        ElseIf (Relacion.CountFS > 0) Then
            ShowAlert("No se puede eliminar esta suscripción, información de la serie se modificó")
            txtAccionHidden.Value = ""
        Else
            CargaDatosModalInicial()
            FormateoFormDatos(SuscripcionActualizada)
            FormateoEstiloFormEliminar()
            If ddlModalMultifondo.SelectedValue = "" Then
                ddlModalMultifondo.Enabled = False
            End If
            txtAccionHidden.Value = "ELIMINAR"
            lblModalTitle.Text = CONST_TITULO_MODAL_ELIMINAR
        End If
    End Sub
    Protected Sub BtnBuscar_Click(sender As Object, e As EventArgs) Handles BtnBuscar.Click
        txtIntencionDesde.Text = Request.Form(txtIntencionDesde.UniqueID)
        txtIntencionHasta.Text = Request.Form(txtIntencionHasta.UniqueID)
        txtNAVDesde.Text = Request.Form(txtNAVDesde.UniqueID)
        txtNAVHasta.Text = Request.Form(txtNAVHasta.UniqueID)

        txtSuscripcionDesde.Text = Request.Form(txtSuscripcionDesde.UniqueID)
        txtSuscripcionHasta.Text = Request.Form(txtSuscripcionHasta.UniqueID)

        findsuscripcion()
        BtnModificar.Enabled = False
        If GrvTabla.Rows.Count <> 0 Then

            BtnExportar.Enabled = True
        Else
            BtnExportar.Enabled = False
            ShowAlert(CONST_SIN_RESULTADOS)
        End If
        txtAccionHidden.Value = ""
    End Sub

    Protected Sub BtnExportar_Click(sender As Object, e As EventArgs) Handles BtnExportar.Click
        Dim Suscripcion As SuscripcionDTO = New SuscripcionDTO()
        Dim negocio As SuscripcionNegocio = New SuscripcionNegocio
        Dim FechaIntencionHasta As Nullable(Of Date)
        Dim FechaNAVHasta As Nullable(Of Date)
        Dim FechaSuscripcionHasta As Nullable(Of Date)

        If ddlListaRutAportante.SelectedValue.Trim() = Nothing Then
            Suscripcion.RutAportante = Nothing
        Else
            Dim arrCadena As String() = ddlListaRutAportante.SelectedItem.Text().Split(New Char() {"/"c})
            Suscripcion.RutAportante = arrCadena(0).Trim()
        End If

        If ddlListaRutFondo.SelectedValue.Trim() = Nothing Then
            Suscripcion.RutFondo = Nothing
        Else
            Dim arrCadena As String() = ddlListaRutFondo.SelectedItem.Text().Split(New Char() {"/"c})
            Suscripcion.RutFondo = arrCadena(0).Trim()
        End If

        If ddlListaNemotecnico.SelectedValue.Trim() = Nothing Then
            Suscripcion.Nemotecnico = Nothing
        Else
            Suscripcion.Nemotecnico = ddlListaNemotecnico.SelectedValue.Trim()
        End If

        If ddlEstado.SelectedValue.Trim() = Nothing Then
            Suscripcion.EstadoSuscripcion = Nothing
        Else
            Suscripcion.EstadoSuscripcion = ddlEstado.SelectedValue.Trim()
        End If

        If Request.Form(txtIntencionDesde.UniqueID).Equals("") Then
            Suscripcion.FechaIntencion = Nothing
        Else
            Suscripcion.FechaIntencion = Date.Parse(Request.Form(txtIntencionDesde.UniqueID))
        End If

        If Request.Form(txtIntencionHasta.UniqueID).Equals("") Then
            FechaIntencionHasta = ("31/12/9999")
        Else
            FechaIntencionHasta = Date.Parse(Request.Form(txtIntencionHasta.UniqueID))
        End If

        If Request.Form(txtNAVDesde.UniqueID).Equals("") Then
            Suscripcion.FechaNAV = Nothing
        Else
            Suscripcion.FechaNAV = Date.Parse(Request.Form(txtNAVDesde.UniqueID))
        End If

        If Request.Form(txtNAVHasta.UniqueID).Equals("") Then
            FechaNAVHasta = ("31/12/9999")
        Else
            FechaNAVHasta = Date.Parse(Request.Form(txtNAVHasta.UniqueID))
        End If

        If Request.Form(txtSuscripcionDesde.UniqueID).Equals("") Then
            Suscripcion.FechaSuscripcion = Nothing
        Else
            Suscripcion.FechaSuscripcion = Date.Parse(Request.Form(txtSuscripcionDesde.UniqueID))
        End If

        If Request.Form(txtSuscripcionHasta.UniqueID).Equals("") Then
            FechaSuscripcionHasta = ("31/12/9999")
        Else
            FechaSuscripcionHasta = Date.Parse(Request.Form(txtSuscripcionHasta.UniqueID))
        End If

        Dim mensaje As String = negocio.ExportarAExcel(Suscripcion, FechaIntencionHasta, FechaNAVHasta, FechaSuscripcionHasta)

        If negocio.rutaArchivosExcel IsNot Nothing Then
            Archivo.NavigateUrl = negocio.rutaArchivosExcel
            Archivo.Text = "Bajar Archivo"
        Else
            Archivo.Visible = False
        End If

        txtAccionHidden.Value = "MOSTRAR_DIALOGO"

        ShowMessages(CONST_TITULO_SUSCRIPCION, mensaje, Constantes.CONST_RUTA_IMG + Constantes.CONST_IMG_LOGO, Constantes.CONST_RUTA_IMG + Constantes.CONST_IMG_INFO, False)
    End Sub
#End Region

#Region "CARGA NOMBRE APORTANTE Y MULTIFONDO  CUANDO CAMBIA COMBO RUT APORTANTE"
    Public Sub CargarNombreAportanteNemotecnicoPorRutAportanteModal()
        'CARGA NOMBRE APORTANTE Y MULTIFONDO POR RUT APORTANTE
        CargarNombreAportantePorRutAportanteModal()
        CargarMultifondoPorRutAportanteModal()
        If (ddlNemotecnico.SelectedValue <> "") Then
            Transitos()
        End If

    End Sub
    Private Sub CargarNombreAportantePorRutAportanteModal()
        Dim fechahasta As Nullable(Of Date)
        Dim NegocioAportante As AportanteNegocio = New AportanteNegocio
        Dim aportante As AportanteDTO = New AportanteDTO()

        aportante.Rut = ddlModalRutAportante.SelectedValue

        Dim aportantes As List(Of AportanteDTO) = NegocioAportante.GetListaAportantes(aportante, fechahasta)

        Dim vacio As New AportanteDTO

        If aportantes.Count = 0 Then
            aportantes.Add(vacio)
        Else

            ddlModalNombreAportante.DataSource = aportantes
            ddlModalNombreAportante.DataMember = "RazonSocial"
            ddlModalNombreAportante.DataValueField = "RazonSocial"
            ddlModalNombreAportante.DataBind()
        End If
    End Sub

    Private Sub CargarMultifondoPorRutAportanteModal()
        txtAccionHidden.Value = "MANTENER_MODAL"

        Dim negocioAportante As AportanteNegocio = New AportanteNegocio
        Dim aportante As AportanteDTO = New AportanteDTO()
        aportante.RazonSocial = ddlModalNombreAportante.SelectedValue
        Dim listAportante As List(Of AportanteDTO) = negocioAportante.AportantePorNombre(aportante)


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
            If vacio = "" Then
                ddlModalMultifondo.Text = ""
                ddlModalMultifondo.Enabled = False
                Exit Sub
            End If
        Next
    End Sub

#End Region

#Region "CARGA DATOS DE SERIE Y FONDO"
    Public Sub CargarRutPorNemotecnico()
        Dim fondoSerie As FondoSerieDTO = New FondoSerieDTO()
        Dim fondo As FondoDTO = New FondoDTO()
        Dim NegocioSerie As FondoSeriesNegocio = New FondoSeriesNegocio

        fondoSerie.Nemotecnico = ddlNemotecnico.SelectedValue
        Dim lista As List(Of FondoSerieDTO) = NegocioSerie.GetListaFondoSerieConFiltro(fondoSerie, fondo)

        Dim vacia As New FondoSerieDTO
        If lista.Count = 0 Then
            lista.Add(vacia)
        Else
            ddlFondo.DataSource = lista
            ddlFondo.DataMember = "Rut"
            ddlFondo.DataValueField = "Rut"
            ddlFondo.DataBind()
        End If

    End Sub

    Public Sub CargarNombreFondo()
        Dim NegocioFondo As FondosNegocio = New FondosNegocio
        Dim fondo As FondoSerieDTO = New FondoSerieDTO()
        fondo.Rut = ddlFondo.SelectedValue
        Dim lista As List(Of FondoDTO) = NegocioFondo.GetNombrePorNemotecnico(fondo)

        Dim vacia As New FondoDTO
        If lista.Count = 0 Then
            lista.Add(vacia)
        Else
            ddlNombreFondo.DataSource = lista
            ddlNombreFondo.DataMember = "RazonSocial"
            ddlNombreFondo.DataValueField = "RazonSocial"
            ddlNombreFondo.DataBind()
        End If

    End Sub
    Public Sub CargarNombreMonedaSerie()
        Dim negocio As FondoSeriesNegocio = New FondoSeriesNegocio
        Dim FondoSerie As FondoSerieDTO = New FondoSerieDTO()
        Dim FondoSerieActualizado As FondoSerieDTO = New FondoSerieDTO
        FondoSerie.Rut = ddlFondo.SelectedValue
        FondoSerie.Nemotecnico = ddlNemotecnico.SelectedValue
        FondoSerieActualizado = negocio.GetFondosSeries(FondoSerie)
        If FondoSerieActualizado IsNot Nothing Then
            txtNombreSerie.Text = FondoSerieActualizado.Nombrecorto
            txtMonedaSerie.Text = FondoSerieActualizado.Moneda
        End If
    End Sub
    Public Sub SelectedIndexChangedFnRut()
        CargarNemotecnicoPorRut()
        ConsultarFechaSuscripcion()
        If (txtFechaSuscripcion.Text <> "") Then
            ConsultarFechaNav()
            CargarNombreMonedaSerieModal()
            ConsultarFechaObservado()
            Transitos()
            CalcularValorEntrante()
            CargarNombreFondo()
            CargarNombreMonedaSerie()
            CargarTCObs()
            llenarCLP()
            consultarCuotasEmitidas(ddlNombreFondo.SelectedValue().ToString().Trim())
            Cuotaschanged()
        Else
            Dim fondo = ddlFondo.Text
            FormateoLimpiarDatosModal()
            FormateoEstiloFormCrear()
            ddlFondo.SelectedValue = fondo
            ShowAlert(CONST_SIN_RESULTADOS_MODAL)
        End If
    End Sub

    Public Sub SelectedChangedNombreFondo()
        CambiarFnRutPorNombre()
        CargarNemotecnicoPorRut()
        ConsultarFechaSuscripcion()
        If (txtFechaSuscripcion.Text <> "") Then
            ConsultarFechaNav()
            CargarNombreMonedaSerieModal()
            ConsultarFechaObservado()
            Transitos()
            CalcularValorEntrante()
            CargarNombreMonedaSerie()
            CargarTCObs()
            llenarCLP()
            Cuotaschanged()
        Else
            Dim fondo = ddlNombreFondo.Text
            FormateoLimpiarDatosModal()
            FormateoEstiloFormCrear()
            ddlNombreFondo.SelectedValue = fondo
            ShowAlert(CONST_SIN_RESULTADOS_MODAL)
        End If

        consultarCuotasEmitidas(ddlNombreFondo.SelectedValue().ToString().Trim())

    End Sub

    Private Sub consultarCuotasEmitidas(NombreFondo As String)
        Dim suscripcion As SuscripcionDTO = New SuscripcionDTO()
        'TODO : Cambiar Dim valorActual As Double
        Dim valorActual As Decimal
        valorActual = ObtenerActual(NombreFondo)
        suscripcion = ObtenerCuotasFondo(NombreFondo)
        suscripcion.ScActual = valorActual

        txtActual.Text = String.Format("{0:N0}", suscripcion.ScActual)
        txtCuotasEmitidas.Text = String.Format("{0:N0}", suscripcion.CuotasEmitidas)
        txtAcumulada.Text = String.Format("{0:N0}", suscripcion.FnAcumulada)
        txtUtilizado.Text = String.Format("{0:N0}", suscripcion.ScUtilizadoAUX)

        If (IsNumeric(txtCuotasEmitidas.Text) And IsNumeric(txtUtilizado.Text)) Then
            txtDisponiblesEmitidas.Text = String.Format("{0:N0}", txtCuotasEmitidas.Text - txtUtilizado.Text)
        Else
            txtDisponiblesEmitidas.Text = "0"
        End If
        ValidarCuotasVsDisponible(suscripcion)

    End Sub

    Private Sub ValidarCuotasVsDisponible(suscripcion As SuscripcionDTO)
        If txtDisponibles.Text = "" Then
            suscripcion.ScDisponibles = 0
        Else
            'TODO: CAmbiar : suscripcion.ScDisponibles = Decimal.Parse(txtDisponiblesEmitidas.Text.ToString()) 
            suscripcion.ScDisponibles = Decimal.Parse(txtDisponiblesEmitidas.Text.ToString())
        End If

        If txtCuotas.Text = "" Then
            suscripcion.CuotasASuscribir = 0
        Else
            suscripcion.CuotasASuscribir = Decimal.Parse(txtCuotas.Text)
        End If

        If suscripcion.CuotasASuscribir > suscripcion.ScDisponibles Then
            btnModalGuardar.Enabled = False
            btnModalModificar.Enabled = False
            ShowAlert("Valor de Cuotas no puede ser superior a cuotas Disponibles")
            txtCuotas.Text = "0"
        Else
            btnModalModificar.Enabled = True
            btnModalModificar.Enabled = True
        End If

    End Sub

    'TODO: MODIFICAR Protected Function ObtenerActual(NombreFondo As String) As Double
    Protected Function ObtenerActual(NombreFondo As String) As Decimal
        Dim suscripcion As SuscripcionDTO = New SuscripcionDTO()
        Dim susNegocio As SuscripcionNegocio = New SuscripcionNegocio()
        suscripcion.RazonSocial = NombreFondo
        suscripcion.RutFondo = ddlFondo.SelectedValue
        suscripcion = susNegocio.ObtenerActual(suscripcion)
        Return suscripcion.ScActual
    End Function

    Private Function ObtenerCuotasFondo(NombreFondo As String) As SuscripcionDTO
        Dim suscripcion As SuscripcionDTO = New SuscripcionDTO()
        Dim susNegocio As SuscripcionNegocio = New SuscripcionNegocio()
        suscripcion.RazonSocial = NombreFondo
        suscripcion.RutFondo = ddlFondo.SelectedValue
        suscripcion = susNegocio.ObtenerCuotasFondo(suscripcion)
        Return suscripcion
    End Function

    Public Sub CambiarFnRutPorNombre()

        Dim negocioFondo As FondosNegocio = New FondosNegocio
        Dim fondo As FondoDTO = New FondoDTO()
        fondo.RazonSocial = ddlNombreFondo.SelectedValue
        Dim listaFondo As List(Of FondoDTO) = negocioFondo.RutByNombreFondo(fondo)

        ddlFondo.DataSource = listaFondo
        ddlFondo.DataMember = "Rut"
        ddlFondo.DataValueField = "Rut"
        ddlFondo.DataBind()
        ddlFondo.SelectedIndex = 0
    End Sub

    Public Sub CargarNemotecnicoPorRut()
        Dim fondoSerie As FondoSerieDTO = New FondoSerieDTO()
        Dim fondo As FondoDTO = New FondoDTO()
        Dim NegocioSerie As FondoSeriesNegocio = New FondoSeriesNegocio

        fondoSerie.Rut = ddlFondo.SelectedValue
        Dim lista As List(Of FondoSerieDTO) = NegocioSerie.GetListaFondoSerieConFiltro(fondoSerie, fondo)

        Dim vacia As New FondoSerieDTO
        If lista.Count = 0 Then
            lista.Add(vacia)
        Else
            ddlNemotecnico.DataSource = lista
            ddlNemotecnico.DataMember = "Nemotecnico"
            ddlNemotecnico.DataValueField = "Nemotecnico"
            ddlNemotecnico.DataBind()
        End If

    End Sub
    Public Sub CargarRutAportanteNemotecnicoPorNombreAportanteModal()
        'CARGA NOMBRE APORTANTE Y MULTIFONDO POR RUT APORTANTE
        CargarRutAportantePorNombreAportanteModal()
        CargarMultifondoPorNombreAportanteModal()
        If (ddlNemotecnico.SelectedValue <> "") Then
            Transitos()
        End If
    End Sub
    Public Sub CargarNombreMultifondoPorRut()
        CargarNombreAportantePorRutAportanteModal()
        CargarMultifondoPorRutAportanteModal()
    End Sub

    Private Sub CargarRutAportantePorNombreAportanteModal()
        Dim fechahasta As Nullable(Of Date)
        Dim NegocioAportante As AportanteNegocio = New AportanteNegocio
        Dim aportante As AportanteDTO = New AportanteDTO()

        aportante.RazonSocial = ddlModalNombreAportante.SelectedValue

        Dim aportantes As List(Of AportanteDTO) = NegocioAportante.GetListaAportantes(aportante, fechahasta)

        Dim vacio As New AportanteDTO

        If aportantes.Count = 0 Then
            aportantes.Add(vacio)
        Else
            ddlModalRutAportante.DataSource = aportantes
            ddlModalRutAportante.DataMember = "rut"
            ddlModalRutAportante.DataValueField = "rut"
            ddlModalRutAportante.DataBind()
        End If

    End Sub

    Private Sub CargarMultifondoPorNombreAportanteModal()
        txtAccionHidden.Value = "MANTENER_MODAL"

        Dim negocioAportante As AportanteNegocio = New AportanteNegocio
        Dim aportante As AportanteDTO = New AportanteDTO()
        aportante.RazonSocial = ddlModalNombreAportante.SelectedValue
        Dim listAportante As List(Of AportanteDTO) = negocioAportante.AportantePorNombre(aportante)

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
            If vacio = "" Then
                ddlModalMultifondo.Text = ""
                ddlModalMultifondo.Enabled = False
                Exit Sub
            End If
        Next

    End Sub

    Protected Sub CargaPorMultifondo()
        'CARGA NOMBRE APORTANTE Y RUT POR MULTIFONDO
        'JC
        If (txtAccionHidden.Value <> "N") Then
            CargarRutAportantePorMultifondo()
            CargarRazonSocialPorMultifondo()
        End If
        If (ddlNemotecnico.SelectedValue <> "") Then
            Transitos()
        End If
    End Sub


    Protected Sub CargarRutAportantePorMultifondo()
        Dim aportante As AportanteDTO = New AportanteDTO()
        Dim NegocioAportante As AportanteNegocio = New AportanteNegocio
        aportante.Multifondo = ddlModalMultifondo.SelectedValue
        Dim aportantes As List(Of AportanteDTO) = NegocioAportante.AportantePorMultifondo(aportante)
        Dim vacio As New AportanteDTO

        If aportantes.Count = 0 Then
            aportantes.Add(vacio)
        Else
            ddlModalRutAportante.DataSource = aportantes
            ddlModalRutAportante.DataMember = "rut"
            ddlModalRutAportante.DataValueField = "rut"
            ddlModalRutAportante.DataBind()
        End If
    End Sub

    Private Sub CargarRazonSocialPorMultifondo()
        Dim aportante As AportanteDTO = New AportanteDTO()
        Dim NegocioAportante As AportanteNegocio = New AportanteNegocio
        aportante.Multifondo = ddlModalMultifondo.SelectedValue
        Dim aportantes As List(Of AportanteDTO) = NegocioAportante.AportantePorMultifondo(aportante)
        Dim vacio As New AportanteDTO

        If aportantes.Count = 0 Then
            aportantes.Add(vacio)
        Else
            ddlModalNombreAportante.DataSource = aportantes
            ddlModalNombreAportante.DataMember = "RazonSocial"
            ddlModalNombreAportante.DataValueField = "RazonSocial"
            ddlModalNombreAportante.DataBind()
        End If
    End Sub

#End Region

    Protected Sub FechaTc()
        'Dim Negocio As VentanasRescateNegocio = New VentanasRescateNegocio
        Dim negocio As FechasNegocio = New FechasNegocio

        Dim FechaValidar As String
        Dim fecha As FechasDTO = New FechasDTO()

        txtFechaTC.Text = Request.Form(txtFechaTC.UniqueID)

        fecha.Dia = Day(txtFechaTC.Text)
        fecha.Mes = Month(txtFechaTC.Text)
        fecha.Anno = Year(txtFechaTC.Text)
        fecha.DF_PAIS = ddlMonedaPago.Text

        'FechaValidar = Negocio.ValidaDiaHabil(txtFechaTC.Text)
        FechaValidar = Negocio.ValidaDiaHabil(fecha)

        If FechaValidar = "Festivo" Then
            txtFechaTC.Text = ""
            ShowAlert("El día seleccionado es No Hábil")
            txtFechaTC.Text = ""
            Return
        End If

        If FechaValidar = "No_Habil" Then
            txtFechaTC.Text = ""
            ShowAlert("El día seleccionado es No Hábil")
            txtFechaTC.Text = ""
            Return
        End If
    End Sub

#Region "Formateo Datos"
    Private Sub FormateoFormDatos(Suscripcion As SuscripcionDTO)

        txtIdSuscripcion.Text = Suscripcion.IdSuscripcion
        ddlModalNombreAportante.Text = Suscripcion.RazonSocial
        ddlModalRutAportante.Text = Suscripcion.RutAportante

        Try
            ddlModalMultifondo.Text = Suscripcion.Multifondo
        Catch ex As Exception
            ddlModalMultifondo.SelectedIndex = 0
        End Try

        ddlFondo.Text = Suscripcion.RutFondo
        ddlNemotecnico.Text = Suscripcion.Nemotecnico
        txtCuotas.Text = String.Format("{0:N0}", Suscripcion.CuotasASuscribir)
        txtNAV.Text = Suscripcion.NavFormat
        ddlMonedaPago.Text = Suscripcion.Moneda_Pago
        txtNAVCLP.Text = Utiles.formatearNAVCLP(Suscripcion.NAVCLP)

        txtTCObservado.Text = Utiles.formatearTC(Suscripcion.TcObservado)
        ddlPoderes.Text = Suscripcion.RevisionPoderes
        txtFechaIntencion.Text = Suscripcion.FechaIntencion
        txtFechaNAV.Text = Suscripcion.FechaNAV
        txtFechaSuscripcion.Text = Suscripcion.FechaSuscripcion
        txtFechaTC.Text = Suscripcion.FechaTC

        txtMonto.Text = Utiles.formatearMonto(Suscripcion.Monto, Suscripcion.MonedaSerie.Trim())
        txtMontoCLP.Text = Utiles.formatearMontoCLP(Suscripcion.MontoCLP)

        ddlContrato.Text = Suscripcion.ContratoFondo
        txtFechaDCV.Text = IIf(Suscripcion.FechaDCV = Nothing, "", Suscripcion.FechaDCV)
        ddlEstado1.Text = Suscripcion.EstadoSuscripcion
        txtObservaciones.Text = Suscripcion.Observaciones
        txtFijacionNAV.Text = Suscripcion.FijacionNAV
        txtFijacionTCObs.Text = Suscripcion.FijacionTC
        txtDisponibles.Text = String.Format("{0:N0}", Suscripcion.CuotasDisponibles)
        txtCuotasDCV.Text = String.Format("{0:N0}", Suscripcion.CuotasDCV)
        txtRescates.Text = String.Format("{0:N0}", Suscripcion.RescatesTransitos)
        txtSuscripciones.Text = String.Format("{0:N0}", Suscripcion.SuscripcionesTransito)
        txtCanje.Text = String.Format("{0:N0}", Suscripcion.CanjesTransito)
        ddlNombreFondo.Text = Suscripcion.FondoNombreCorto
        txtNombreSerie.Text = Suscripcion.SerieNombreCorto
        txtMonedaSerie.Text = Suscripcion.MonedaSerie
        txtCuotasEmitidas.Text = String.Format("{0:N0}", Suscripcion.CuotasEmitidas)
        txtAcumulada.Text = String.Format("{0:N0}", Suscripcion.FnAcumulada)
        txtUtilizado.Text = String.Format("{0:N0}", Suscripcion.ScUtilizado)
        txtActual.Text = String.Format("{0:N0}", Suscripcion.ScActual)
        txtDisponiblesEmitidas.Text = String.Format("{0:N0}", Suscripcion.ScDisponibles)

        Suscripcion.Estado = "1"
    End Sub
    Protected Sub FormateoLimpiarDatosModal()
        ddlModalNombreAportante.Items.Clear()
        ddlModalRutAportante.Items.Clear()
        ddlModalMultifondo.Items.Clear()
        ddlFondo.Items.Clear()
        ddlNombreFondo.Items.Clear()
        ddlNemotecnico.Items.Clear()
        txtCuotas.Text = ""
        txtNAV.Text = ""
        ddlMonedaPago.SelectedIndex = 0
        txtNAVCLP.Text = ""
        txtTCObservado.Text = ""
        ddlPoderes.SelectedIndex = 0
        txtFechaIntencion.Text = Date.Now.ToString("dd/MM/yyyy")
        txtFechaNAV.Text = ""
        txtFechaSuscripcion.Text = ""
        txtFechaTC.Text = ""
        txtNombreSerie.Text = ""
        txtMonedaSerie.Text = ""
        txtMonto.Text = ""
        txtMontoCLP.Text = ""
        ddlContrato.SelectedIndex = 0
        txtFechaDCV.Text = ""
        ddlEstado1.SelectedIndex = 1
        txtObservaciones.Text = ""
        txtFijacionTCObs.Text = ""
        txtCuotasDCV.Text = ""
        txtSuscripciones.Text = ""
        txtRescates.Text = ""
        txtCanje.Text = ""
        txtDisponibles.Text = ""
        txtFijacionNAV.Text = ""
        txtFechaSuscripcion.Text = ""
        txtFechaTC.Text = ""
        txtFechaNAV.Text = ""
        txtCuotasEmitidas.Text = ""
        txtActual.Text = ""
        txtUtilizado.Text = ""
        txtDisponiblesEmitidas.Text = ""
        txtAcumulada.Text = ""
    End Sub
    Private Sub FormateoEstiloFormCrear()
        btnModalModificar.Enabled = False
        btnModalModificar.Visible = False
        btnModalGuardar.Enabled = True
        btnModalGuardar.Visible = True

        btnModalEliminar.Enabled = False
        FormateoFormCrearModificar()
        CargaDatosModalInicial()
        lbModalTittle.Text = CONST_TITULO_MODAL_CREAR
    End Sub
    Private Sub FormateoEstiloFormModificar()
        btnModalEliminar.Visible = False
        btnModalModificar.Visible = True
        btnModalModificar.Enabled = True
        btnModalGuardar.Visible = False
        btnModalGuardar.Enabled = False
        btnModalEliminar.Enabled = False

        FormateoFormCrearModificar()
        txtAccionHidden.Value = "MODIFICAR"
        lbModalTittle.Text = CONST_TITULO_MODAL_MODIFICAR
    End Sub
    Private Sub FormateoEstiloFormEliminar()
        CargaDatosModalInicial()
        btnModalModificar.Enabled = False
        btnModalGuardar.Enabled = False
        btnModalGuardar.Visible = False
        btnModalEliminar.Enabled = True
        btnModalModificar.Visible = False
        btnModalEliminar.Visible = True

        ddlModalNombreAportante.Enabled = False
        ddlModalRutAportante.Enabled = False
        ddlModalMultifondo.Enabled = False
        ddlFondo.Enabled = False
        ddlNombreFondo.Enabled = False
        ddlNemotecnico.Enabled = False
        txtCuotas.Enabled = False
        txtNAV.Enabled = False
        ddlMonedaPago.Enabled = False
        txtNAVCLP.Enabled = False
        txtTCObservado.Enabled = False
        ddlPoderes.Enabled = False
        txtFechaIntencion.Enabled = False
        txtFechaNAV.Enabled = False
        txtFechaSuscripcion.Enabled = False
        txtFechaTC.Enabled = False
        txtMonto.Enabled = False
        ddlContrato.Enabled = False
        ddlEstado1.Enabled = False
        txtObservaciones.Enabled = False
        txtDisponibles.Enabled = False
        txtMontoCLP.Enabled = False
        txtTCObservado.Enabled = False
        LinkButton7.Visible = False
        LinkButton8.Visible = False
        LinkButton9.Visible = False
        LinkButton10.Visible = False
        LinkButton7.Enabled = False
        LinkButton8.Enabled = False
        LinkButton9.Enabled = False
        LinkButton10.Enabled = False
        lbModalTittle.Text = CONST_TITULO_MODAL_ELIMINAR

    End Sub
    Protected Sub FormateoFormCrearModificar()
        ddlModalNombreAportante.Enabled = True
        ddlModalRutAportante.Enabled = True
        btnModalEliminar.Enabled = False
        btnModalEliminar.Visible = False
        ddlModalMultifondo.Enabled = True
        ddlFondo.Enabled = True
        ddlNombreFondo.Enabled = True
        ddlNemotecnico.Enabled = True
        txtCuotas.Enabled = True
        txtNAV.Enabled = True
        ddlMonedaPago.Enabled = True
        txtNAVCLP.Enabled = True
        txtTCObservado.Enabled = True
        ddlPoderes.Enabled = True
        txtMonto.Enabled = True
        ddlContrato.Enabled = True
        ddlEstado1.Enabled = True
        txtObservaciones.Enabled = True
        txtMontoCLP.Enabled = True
        txtTCObservado.Enabled = True
        LinkButton7.Visible = True
        LinkButton8.Visible = True
        LinkButton9.Visible = True
        LinkButton10.Visible = True
        LinkButton7.Enabled = True
        LinkButton8.Enabled = True
        LinkButton9.Enabled = True
        LinkButton10.Enabled = True


    End Sub
    Private Sub FormateoLimpiarForm()
        CargaFiltroRutAportanteS()
        ddlListaRutAportante.SelectedIndex = 0
        ddlListaRutFondo.SelectedIndex = 0
        ddlListaNemotecnico.SelectedIndex = 0
        ddlEstado.SelectedIndex = 0
        txtIntencionDesde.Text = ""
        txtIntencionHasta.Text = ""
        txtSuscripcionDesde.Text = ""
        txtSuscripcionHasta.Text = ""
        txtNAVDesde.Text = ""
        txtNAVHasta.Text = ""
        GrvTabla.DataSource = New List(Of SuscripcionDTO)
        GrvTabla.DataBind()
        BtnExportar.Enabled = False
    End Sub
#End Region

#Region "Cargar Datos"
    Private Sub CargaNemotecnico()
        Dim nemotecnico As New FondoSerieDTO
        Dim Negocio As New FondoSeriesNegocio()
        Dim lista As List(Of FondoSerieDTO) = Negocio.GetListaFondoSerieporNemotecnico(nemotecnico)
        Dim fondovacio As New FondoSerieDTO

        If lista.Count = 0 Then
            lista.Add(fondovacio)
        Else
            ddlNemotecnico.DataSource = lista
            ddlNemotecnico.DataMember = "Nemotecnico"
            ddlNemotecnico.DataValueField = "Nemotecnico"
            ddlNemotecnico.DataBind()
            ddlNemotecnico.Items.Insert(0, New ListItem("", ""))
        End If
    End Sub
    Private Sub CargaFiltroRutAportanteModal()
        Dim Suscripcion As New SuscripcionDTO
        Dim lista As List(Of SuscripcionDTO) = Negocio.GetAportanteDistinct(Suscripcion)
        Dim vacia As New SuscripcionDTO
        If lista.Count = 0 Then
            lista.Add(vacia)
        Else
            ddlModalRutAportante.DataSource = lista
            ddlModalRutAportante.DataMember = "RutAportante"
            ddlModalRutAportante.DataValueField = "RutAportante"
            ddlModalRutAportante.DataBind()
            ddlModalRutAportante.Items.Insert(0, New ListItem("", ""))
        End If

    End Sub



    Private Sub CargaFiltroRutAportanteS()
        Dim suscripcion As New SuscripcionDTO

        llenarComboRutAportantes(suscripcion)
        llenarComboRutFondo(suscripcion)
        llenarComboNemotecnicos(suscripcion)

    End Sub


    Private Sub CargaFiltroRutFondo()
        Dim nemotecnico As New FondoSerieDTO
        Dim Negocio As New FondoSeriesNegocio()
        Dim lista As List(Of FondoSerieDTO) = Negocio.GetListaFondosRut(nemotecnico)
        Dim fondovacio As New FondoSerieDTO

        If lista.Count = 0 Then
            lista.Add(fondovacio)
        Else
            ddlFondo.DataSource = lista
            ddlFondo.DataMember = "Rut"
            ddlFondo.DataValueField = "Rut"
            ddlFondo.DataBind()
            ddlFondo.Items.Insert(0, New ListItem("", ""))
        End If
        CargaFiltroNombreFondo()
    End Sub

    Private Sub CargaFiltroNombreFondo()
        Dim Negocio As FondosNegocio = New FondosNegocio
        Dim fondo As FondoDTO = New FondoDTO()
        Dim fondovacio As New FondoDTO
        Dim lista As List(Of FondoDTO) = Negocio.GetNombreFondo(fondo)

        If lista.Count = 0 Then
            lista.Add(fondovacio)
        Else
            ddlNombreFondo.DataSource = lista
            ddlNombreFondo.DataMember = "RazonSocial"
            ddlNombreFondo.DataValueField = "RazonSocial"
            ddlNombreFondo.DataBind()
            ddlNombreFondo.Items.Insert(0, New ListItem("", ""))
        End If
    End Sub
    Protected Sub CargaFiltroNombreAportante()
        Dim Suscripcion As New SuscripcionDTO()
        Dim Negocio As New SuscripcionNegocio()

        If (ddlModalMultifondo.Text = "") Then
            Suscripcion.Multifondo = Nothing
        Else
            Suscripcion.Multifondo = ddlModalMultifondo.Text
        End If
        Dim lista As List(Of SuscripcionDTO) = Negocio.ConsultarPorRazonSocial(Suscripcion)

        Dim vacia As New SuscripcionDTO
        If lista.Count = 0 Then
            lista.Add(vacia)
        Else
            ddlModalNombreAportante.DataSource = lista
            ddlModalNombreAportante.DataMember = "RazonSocial"
            ddlModalNombreAportante.DataValueField = "RazonSocial"
            ddlModalNombreAportante.DataBind()
            ddlModalNombreAportante.Items.Insert(0, New ListItem("", ""))
        End If

    End Sub
    Protected Sub CargaFiltroMultifondoAportante()
        'ddlModalMultifondo.Items.Clear()
        Dim aportante As New AportanteDTO
        Dim Negoc As New CanjeNegocio
        Dim listaRut As List(Of AportanteDTO) = Negoc.ConsultarMultifondo(aportante)
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
    Protected Sub CargaFiltroMultifondoAportante1()
        Dim Suscripcion As New SuscripcionDTO()
        Dim Negocio As New SuscripcionNegocio()
        Suscripcion.RazonSocial = ddlModalNombreAportante.SelectedValue
        Dim lista As List(Of SuscripcionDTO) = Negocio.ConsultarPorMultifondo(Suscripcion)
        Dim vacia As New SuscripcionDTO
        If lista.Count = 0 Then
            lista.Add(vacia)
        Else
            ddlModalMultifondo.DataSource = lista
            ddlModalMultifondo.DataMember = "Multifondo"
            ddlModalMultifondo.DataValueField = "Multifondo"
            ddlModalMultifondo.DataBind()
            ddlModalMultifondo.Items.Insert(0, New ListItem("", ""))
        End If
    End Sub
    Public Sub CargarCuotasDCV()

        Dim ADCV1 As ADCVDTO = New ADCVDTO()
        Dim NegocioADCV1 As ADCVNegocio = New ADCVNegocio

        ADCV1.AP_RUT = ddlModalRutAportante.SelectedValue
        ADCV1.FS_Nemotecnico = ddlNemotecnico.SelectedValue

        Dim ADCVActualizado1 As ADCVDTO = NegocioADCV1.SelectCuotasDCV(ADCV1)

        If ADCVActualizado1 Is Nothing Then
            txtCuotasDCV.Text = "0"
        Else
            txtCuotasDCV.Text = String.Format("{0:N0}", ADCVActualizado1.ADCV_Cantidad)
        End If

    End Sub
    Private Sub LLenarIdSuscripcion()
        Dim Suscripcion As SuscripcionDTO = New SuscripcionDTO()
        Dim NegocioS As SuscripcionNegocio = New SuscripcionNegocio
        Dim SuscripcionActualizada As SuscripcionDTO = New SuscripcionDTO()

        SuscripcionActualizada = NegocioS.GetUltimaSuscripcion(Suscripcion)

        If SuscripcionActualizada IsNot Nothing Then
            txtIdSuscripcion.Text = SuscripcionActualizada.IdSuscripcion + 1
        End If
    End Sub

    Public Sub CalcularValorEntrante()
        Dim valor As VcSerieDTO = New VcSerieDTO()
        Dim negocioValor As ValoresCuotaNegocio = New ValoresCuotaNegocio
        Dim ValorNuevo As New VcSerieDTO()
        Dim negocioSerie As FondoSeriesNegocio = New FondoSeriesNegocio
        Dim serie As FondoSerieDTO = New FondoSerieDTO
        Dim serieac As FondoSerieDTO = New FondoSerieDTO

        serie.Nemotecnico = ddlNemotecnico.SelectedValue
        serie.Rut = ddlFondo.SelectedValue
        valor.FsNemotecnico = ddlNemotecnico.SelectedValue
        valor.Fecha = txtFechaNAV.Text
        serieac = negocioSerie.GetFondosSeries(serie)

        Dim listaValores As List(Of VcSerieDTO) = negocioValor.ValoresCuotaPorNemotecnicoYFecha(valor)
        Dim valorNav As Double
        Dim listaUltimo As List(Of VcSerieDTO) = negocioValor.UltimoValorCuota(valor)

        If listaValores.Count > 0 Then
            For Each valores As VcSerieDTO In listaValores
                txtNAV.Text = Utiles.formatearNAV(valores.Valor)   '  String.Format("{0:N4}", valorNav)
                txtNAVCLP.Text = Utiles.calcularNAVCLP(txtTCObservado.Text, valores.Valor) '  String.Format("{0:N4}", valorNav)

                txtFijacionNAV.Text = IIf(serieac.FijacionCanje = "Automático", "Realizado", "Pendiente")
            Next
        Else
            If listaUltimo.Count = 0 Then
                txtNAV.Text = 0
                txtFijacionNAV.Text = "Pendiente"
            Else
                For Each vcs As VcSerieDTO In listaUltimo
                    valorNav = vcs.Valor
                    txtNAV.Text = Utiles.formatearNAV(valorNav) ' String.Format("{0:N4}", valorNav)
                    txtNAVCLP.Text = Utiles.calcularNAVCLP(txtTCObservado.Text, valorNav) ' String.Format("{0:N4}", valorNav)
                    txtFijacionNAV.Text = "Pendiente"
                Next
                'navclp()
            End If
        End If

    End Sub

    Public Sub CargarNombreMonedaSerieModal()
        Dim NegocioFondoSerie As FondoSeriesNegocio = New FondoSeriesNegocio
        Dim NegocioRescate As RescateNegocio = New RescateNegocio
        Dim fondoSerie As FondoSerieDTO = New FondoSerieDTO()
        Dim primerRegistroNemotecnico As String
        primerRegistroNemotecnico = ddlNemotecnico.Text
        fondoSerie.Nemotecnico = primerRegistroNemotecnico


        'CARGA LA FECHA DCV
        Dim ADCV As ADCVDTO = New ADCVDTO()
        Dim NegocioADCV As ADCVNegocio = New ADCVNegocio

        ADCV.AP_RUT = IIf(ddlModalRutAportante.SelectedValue.Trim() = "", Nothing, ddlModalRutAportante.SelectedValue.Trim())
        ADCV.ADCV_Razon_Social = IIf(ddlModalNombreAportante.SelectedValue.Trim() = "", Nothing, ddlModalNombreAportante.SelectedValue.Trim())
        ADCV.FS_Nemotecnico = IIf(ddlNemotecnico.SelectedValue.Trim() = "", Nothing, ddlNemotecnico.SelectedValue.Trim())

        Dim ADCVActualizado As ADCVDTO = NegocioADCV.GetADCV(ADCV)
        If ADCVActualizado Is Nothing Then
            Dim ADCVUltimo As List(Of ADCVDTO) = NegocioADCV.UltimoDCV(ADCV)
            If ADCVUltimo.Count = 0 Then
                txtFechaDCV.Text = ""
            Else
                For Each ADCVUltimo1 As ADCVDTO In ADCVUltimo
                    txtFechaDCV.Text = CDate(ADCVUltimo1.ADCV_Fecha.ToString("dd/MM/yyyy"))
                Next
            End If
        Else
            txtFechaDCV.Text = CDate(ADCVActualizado.ADCV_Fecha.ToString("dd/MM/yyyy"))
        End If
    End Sub

    Protected Sub CargarTCObs()
        'TRAER VALOR TIPO CAMBIO OBSERVADO
        Dim TipoCambio As TipoCambioDTO = New TipoCambioDTO()
        Dim NegocioTipoCambio As TipoCambioNegocio = New TipoCambioNegocio
        Dim TipoCambioActualizado As TipoCambioDTO = New TipoCambioDTO()

        If (txtFechaTC.Text <> "" And txtMonedaSerie.Text <> "") Then
            TipoCambio.Fecha = txtFechaTC.Text
            TipoCambio.Codigo = txtMonedaSerie.Text
            TipoCambioActualizado = NegocioTipoCambio.GetTipoCambio(TipoCambio)

            If TipoCambioActualizado IsNot Nothing Then
                txtTCObservado.Text = String.Format("{0:N6}", TipoCambioActualizado.Valor)
                txtFijacionTCObs.Text = "Realizado"
            Else
                TraerUltimoTc()
            End If
        Else
            TraerUltimoTc()
        End If

    End Sub

    Protected Sub TraerUltimoTc()
        Dim TipoCambio As TipoCambioDTO = New TipoCambioDTO()
        Dim NegocioTipoCambio As TipoCambioNegocio = New TipoCambioNegocio
        TipoCambio.Codigo = txtMonedaSerie.Text
        Dim valor As Decimal
        Dim ListaTC As List(Of TipoCambioDTO) = NegocioTipoCambio.UltimoTipoCambioPorCodigo(TipoCambio)
        For Each tipos As TipoCambioDTO In ListaTC
            valor = tipos.Valor
            txtTCObservado.Text = String.Format("{0:N6}", valor)
        Next
        txtFijacionTCObs.Text = "Pendiente"
    End Sub
#End Region

#Region "Funciones"
    Private Sub ValidaPermisosPerfil()
        If Session("PERFIL") = Constantes.CONST_PERFIL_CONSULTA Or Session("PERFIL") = Nothing Then
            btnCrear.Visible = False
            BtnModificar.Visible = False
            BtnEliminar.Visible = False

        ElseIf Session("PERFIL") = Constantes.CONST_PERFIL_FULL Or Session("PERFIL") = Constantes.CONST_PERFIL_ADMIN Then
            btnCrear.Visible = True
            BtnModificar.Visible = True
            BtnEliminar.Visible = True
        End If
    End Sub
    Protected Sub LlenarPorNemotecnico()
        CargarRutPorNemotecnico()
        ConsultarFechaSuscripcion()
        If (txtFechaSuscripcion.Text <> "") Then
            ConsultarFechaNav()
            CargarNombreMonedaSerieModal()
            ConsultarFechaObservado()
            Transitos()
            CalcularValorEntrante()
            Cuotaschanged()
            CargarNombreFondo()
            CargarNombreMonedaSerie()
            CargarTCObs()
            llenarCLP()
            consultarCuotasEmitidas(ddlNombreFondo.SelectedValue().ToString().Trim())
        Else
            Dim fondo = ddlNemotecnico.Text
            FormateoLimpiarDatosModal()
            FormateoEstiloFormCrear()
            ddlNemotecnico.SelectedValue = fondo
            ShowAlert(CONST_SIN_RESULTADOS_MODAL)
        End If
    End Sub
    Private Sub findsuscripcion()
        Dim Suscripcion As SuscripcionDTO = New SuscripcionDTO()
        Dim negocio As SuscripcionNegocio = New SuscripcionNegocio
        Dim FechaIntencionHasta As Nullable(Of Date)
        Dim FechaNAVHasta As Nullable(Of Date)
        Dim FechaSuscripcionHasta As Nullable(Of Date)

        If ddlListaRutAportante.SelectedValue.Trim() = Nothing Then
            Suscripcion.RutAportante = Nothing
            Suscripcion.RazonSocial = Nothing
        Else
            Dim arrCadena As String() = ddlListaRutAportante.SelectedItem.Text().Split(New Char() {"/"c})

            Suscripcion.RutAportante = arrCadena(0).Trim()
            Suscripcion.RazonSocial = arrCadena(1).Trim()
        End If

        If ddlListaRutFondo.SelectedValue.Trim() = Nothing Then
            Suscripcion.RutFondo = Nothing
            Suscripcion.FondoNombreCorto = Nothing
        Else
            Dim arrCadena As String() = ddlListaRutFondo.SelectedItem.Text().Split(New Char() {"/"c})

            Suscripcion.RutFondo = arrCadena(0).Trim()
            Suscripcion.FondoNombreCorto = arrCadena(1).Trim()
        End If

        If ddlListaNemotecnico.SelectedValue.Trim() = Nothing Then
            Suscripcion.Nemotecnico = Nothing
        Else
            Suscripcion.Nemotecnico = ddlListaNemotecnico.SelectedValue.Trim()
        End If

        If ddlEstado.SelectedValue.Trim() = Nothing Then
            Suscripcion.EstadoSuscripcion = Nothing
        Else
            Suscripcion.EstadoSuscripcion = ddlEstado.SelectedValue.Trim()
        End If


        If Request.Form(txtIntencionDesde.UniqueID).Equals("") Then
            Suscripcion.FechaIntencion = Nothing
        Else
            Suscripcion.FechaIntencion = Date.Parse(Request.Form(txtIntencionDesde.UniqueID))
        End If


        If Request.Form(txtIntencionHasta.UniqueID).Equals("") Then
            FechaIntencionHasta = ("31/12/9999")
        Else
            FechaIntencionHasta = Date.Parse(Request.Form(txtIntencionHasta.UniqueID))
        End If

        If Request.Form(txtNAVDesde.UniqueID).Equals("") Then
            Suscripcion.FechaNAV = Nothing
        Else
            Suscripcion.FechaNAV = Date.Parse(Request.Form(txtNAVDesde.UniqueID))
        End If

        If Request.Form(txtNAVHasta.UniqueID).Equals("") Then
            FechaNAVHasta = ("31/12/9999")
        Else
            FechaNAVHasta = Date.Parse(Request.Form(txtNAVHasta.UniqueID))
        End If

        If Request.Form(txtSuscripcionDesde.UniqueID).Equals("") Then
            Suscripcion.FechaSuscripcion = Nothing
        Else
            Suscripcion.FechaSuscripcion = Date.Parse(Request.Form(txtSuscripcionDesde.UniqueID))
        End If

        If Request.Form(txtSuscripcionHasta.UniqueID).Equals("") Then
            FechaSuscripcionHasta = ("31/12/9999")
        Else
            FechaSuscripcionHasta = Date.Parse(Request.Form(txtSuscripcionHasta.UniqueID))
        End If

        If ddlListaRutAportante.SelectedValue.Trim() = Nothing And ddlListaRutFondo.SelectedValue.Trim() = Nothing And ddlListaNemotecnico.SelectedValue.Trim() = Nothing And ddlEstado.SelectedValue.Trim() = Nothing And Request.Form(txtIntencionDesde.UniqueID).Equals("") And Request.Form(txtIntencionHasta.UniqueID).Equals("") And Request.Form(txtNAVDesde.UniqueID).Equals("") And Request.Form(txtNAVHasta.UniqueID).Equals("") And Request.Form(txtSuscripcionDesde.UniqueID).Equals("") And Request.Form(txtSuscripcionHasta.UniqueID).Equals("") Then
            GrvTabla.DataSource = negocio.GetListaSuscripcion(Suscripcion)
        Else
            GrvTabla.DataSource = negocio.GetListaTCConFiltro(Suscripcion, FechaIntencionHasta, FechaNAVHasta, FechaSuscripcionHasta)
        End If
        GrvTabla.DataBind()
    End Sub

    Private Sub ShowAlert(mesagge As String)
        Dim myScript As String = "alert('" + mesagge + "');"
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", myScript, True)
    End Sub
    Protected Sub RowSelector_CheckedChanged(sender As Object, e As EventArgs)
        For Each row As GridViewRow In GrvTabla.Rows
            Dim chk As RadioButton = row.Cells(0).Controls(1)
            If chk IsNot Nothing And chk.Checked Then
            End If
        Next
    End Sub
    Private Function GetSuscripcionSelect() As SuscripcionDTO
        Dim Suscripcion As New SuscripcionDTO

        For Each row As GridViewRow In GrvTabla.Rows
            Dim chk As RadioButton = row.Cells(0).Controls(1)
            If chk IsNot Nothing And chk.Checked Then
                Suscripcion.IdSuscripcion = row.Cells(CONST_COL_IDSUSCRIPCION).Text().Trim()
                Suscripcion.TipoTransaccion = row.Cells(CONST_COL_TIPOTRANSACCION).Text().Trim()
                Suscripcion.FechaIntencion = row.Cells(CONST_COL_FECHAINTENCION).Text().Trim()
                Suscripcion.RutAportante = row.Cells(CONST_COL_RUTAPORTANTE).Text().Trim()
                Suscripcion.RazonSocial = HttpUtility.HtmlDecode(row.Cells(CONST_COL_RAZONSOCIAL).Text().Trim())
                Suscripcion.Multifondo = row.Cells(CONST_COL_MULTIFONDO).Text().Trim()
                Suscripcion.RutFondo = row.Cells(CONST_COL_RUTFONDO).Text().Trim()
                Suscripcion.FondoNombreCorto = HttpUtility.HtmlDecode(row.Cells(CONST_COL_FNNOMBRECORTO).Text().Trim())
                Suscripcion.Nemotecnico = row.Cells(CONST_COL_NEMOTECNICO).Text().Trim()
                Suscripcion.SerieNombreCorto = row.Cells(CONST_COL_FSNOMBRECORTO).Text().Trim()
                Suscripcion.MonedaSerie = row.Cells(CONST_COL_FSMONEDA).Text().Trim()
                Suscripcion.CuotasASuscribir = row.Cells(CONST_COL_CUOTASASUSCRIBIR).Text().Trim()
                Suscripcion.Moneda_Pago = row.Cells(CONST_COL_MONEDA_PAGO).Text().Trim()
                Suscripcion.FechaNAV = row.Cells(CONST_COL_FECHANAV).Text().Trim()
                Suscripcion.FechaSuscripcion = row.Cells(CONST_COL_FECHASUSCRIPCION).Text().Trim()
                Suscripcion.FechaTC = row.Cells(CONST_COL_FECHATC).Text().Trim()
                Suscripcion.NAV = row.Cells(CONST_COL_NAV).Text().Trim()
                Suscripcion.Monto = row.Cells(CONST_COL_MONTO).Text().Trim()
                Suscripcion.NAVCLP = row.Cells(CONST_COL_NAVCLP).Text().Trim()
                Suscripcion.MontoCLP = row.Cells(CONST_COL_MONTOCLP).Text().Trim()
                Suscripcion.ContratoFondo = row.Cells(CONST_COL_CONTRATOFONDO).Text().Trim()
                Suscripcion.RevisionPoderes = row.Cells(CONST_COL_REVISIONPODERES).Text().Trim()
                Suscripcion.Observaciones = HttpUtility.HtmlDecode(row.Cells(CONST_COL_OBSERVACIONES).Text().Trim())
                Suscripcion.FechaDCV = row.Cells(CONST_COL_FECHADCV).Text().Trim()
                Suscripcion.CuotasDCV = row.Cells(CONST_COL_CUOTASDCV).Text().Trim()
                Suscripcion.RescatesTransitos = row.Cells(CONST_COL_RESCATESTRANSITOS).Text().Trim()
                Suscripcion.SuscripcionesTransito = row.Cells(CONST_COL_SUSCRIPCIONESTRANSITO).Text().Trim()
                Suscripcion.CanjesTransito = row.Cells(CONST_COL_CANJESTRANSITO).Text().Trim()
                Suscripcion.CuotasDisponibles = row.Cells(CONST_COL_CUOTASDISPONIBLES).Text().Trim()
                Suscripcion.FijacionNAV = row.Cells(CONST_COL_FIJACIONNAV).Text().Trim()
                Suscripcion.TcObservado = row.Cells(CONST_COL_TCOBSERVADO).Text().Trim()
                Suscripcion.FijacionTC = row.Cells(CONST_COL_FIJACIONTC).Text().Trim()
                Suscripcion.EstadoSuscripcion = row.Cells(CONST_COL_ESTADOSUSCRIPCION).Text().Trim()
                Suscripcion.CuotasEmitidas = row.Cells(CONST_COL_CUOTAEMITIDA).Text().Trim()
                Suscripcion.FnAcumulada = row.Cells(CONST_COL_ACUMULADA).Text().Trim()
                Suscripcion.ScActual = row.Cells(CONST_COL_ACTUAL).Text().Trim()
                Suscripcion.ScUtilizado = row.Cells(CONST_COL_UTILIZADA).Text().Trim()
                Suscripcion.ScDisponibles = row.Cells(CONST_COL_DISPONIBLES).Text().Trim()
                Suscripcion.ScUsuarioModificacion = row.Cells(CONST_COL_USUARIOMODIFICACION).Text().Trim()
                Suscripcion.ScFechaModificacion = row.Cells(CONST_COL_FECHAMODIFICACION).Text().Trim()
                Suscripcion.ScUsuarioIngreso = row.Cells(CONST_COL_USUARIOINGRESO).Text().Trim()
                Suscripcion.ScFechaIngreso = row.Cells(CONST_COL_FECHAINGRESO).Text().Trim()
            End If
        Next

        Return Suscripcion
    End Function
    Private Function GetSuscripcionModal() As SuscripcionDTO
        Dim Suscripcion As SuscripcionDTO = New SuscripcionDTO()

        Suscripcion.IdSuscripcion = txtIdSuscripcion.Text
        Suscripcion.FechaIntencion = txtFechaIntencion.Text
        Suscripcion.RutAportante = ddlModalRutAportante.Text
        Suscripcion.RazonSocial = ddlModalNombreAportante.Text
        Suscripcion.Multifondo = ddlModalMultifondo.Text
        Suscripcion.RutFondo = ddlFondo.Text
        Suscripcion.Nemotecnico = ddlNemotecnico.Text
        Suscripcion.FondoNombreCorto = ddlNombreFondo.Text
        Suscripcion.CuotasASuscribir = txtCuotas.Text
        Suscripcion.Moneda_Pago = ddlMonedaPago.Text
        Suscripcion.FechaNAV = txtFechaNAV.Text
        Suscripcion.FechaSuscripcion = txtFechaSuscripcion.Text
        Suscripcion.FechaTC = txtFechaTC.Text
        Suscripcion.NAV = txtNAV.Text
        Suscripcion.Monto = txtMonto.Text
        Suscripcion.NAVCLP = txtNAVCLP.Text
        Suscripcion.MontoCLP = txtMontoCLP.Text
        Suscripcion.ContratoFondo = ddlContrato.Text
        Suscripcion.RevisionPoderes = ddlPoderes.Text
        Suscripcion.Observaciones = txtObservaciones.Text
        Suscripcion.FechaDCV = IIf(txtFechaDCV.Text = "", Nothing, txtFechaDCV.Text)
        Suscripcion.CuotasDCV = txtCuotasDCV.Text
        Suscripcion.RescatesTransitos = txtRescates.Text
        Suscripcion.SuscripcionesTransito = txtSuscripciones.Text
        Suscripcion.CanjesTransito = txtCanje.Text
        Suscripcion.CuotasDisponibles = txtDisponibles.Text
        Suscripcion.FijacionNAV = txtFijacionNAV.Text
        Suscripcion.TcObservado = Replace(Replace(txtTCObservado.Text, ".", ""), ",", ".")
        Suscripcion.FijacionTC = txtFijacionTCObs.Text
        Suscripcion.ScUsuarioIngreso = Session("NombreUsuario")
        Suscripcion.ScFechaIngreso = Date.Now
        Suscripcion.ScFechaModificacion = Date.Now
        Suscripcion.ScUsuarioModificacion = Session("NombreUsuario")
        Suscripcion.EstadoSuscripcion = ddlEstado1.Text
        Suscripcion.Estado = "1"

        If txtActual.Text = "" Then
            Suscripcion.ScActual = 0
        Else
            Suscripcion.ScActual = Decimal.Parse(Replace(txtActual.Text(), ".", ""))
        End If

        If txtCuotasEmitidas.Text = "" Then
            Suscripcion.CuotasEmitidas = 0
        Else
            Suscripcion.CuotasEmitidas = Decimal.Parse(Replace(txtCuotasEmitidas.Text(), ".", ""))
        End If

        If txtAcumulada.Text = "" Then
            Suscripcion.FnAcumulada = 0
        Else
            Suscripcion.FnAcumulada = Decimal.Parse(Replace(txtAcumulada.Text(), ".", ""))
        End If

        If txtUtilizado.Text = "" Then
            Suscripcion.ScUtilizado = 0
        Else
            Suscripcion.ScUtilizado = Decimal.Parse(Replace(txtUtilizado.Text(), ".", ""))
        End If

        If txtDisponiblesEmitidas.Text = "" Then
            Suscripcion.ScDisponibles = 0
        Else
            'TODO: JOVB
            Suscripcion.ScDisponibles = Decimal.Parse(Replace(txtDisponiblesEmitidas.Text.ToString(), ".", ""))
        End If

        Return Suscripcion
    End Function
    Private Sub ShowMessages(tittle As String, message As String, urlconTittle As String, urlconMessage As String, Optional borralink As Boolean = True)
        lblModalTitle.Text = tittle
        lblModalBody.Text = message
        img_modal.ImageUrl = urlconTittle
        img_body_modal.ImageUrl = urlconMessage
        Archivo.Visible = (Not borralink)
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModalmg", "$('#myModalmg').modal();", True)
    End Sub

    Protected Sub llenarCLP()

        If (IsNumeric(txtTCObservado.Text) And IsNumeric(txtNAV.Text)) Then
            txtNAVCLP.Text = Utiles.calcularNAVCLP(txtTCObservado.Text, txtNAV.Text) '  String.Format("{0:N4}", txtNAV.Text * txtTCObservado.Text)
        Else
            txtNAVCLP.Text = ""
        End If

        If (IsNumeric(txtCuotas.Text) And IsNumeric(txtNAV.Text) And IsNumeric(txtTCObservado.Text)) Then
            txtMontoCLP.Text = Utiles.calcularMontoCLP(txtCuotas.Text, txtNAV.Text, txtTCObservado.Text) 'String.Format("{0:N2}", NavClpAux)
        End If
    End Sub

    Private Sub CalcularMontos()
        txtMontoCLP.Text = Utiles.calcularMontoCLP(txtCuotas.Text, txtNAV.Text, txtTCObservado.Text)
        txtMonto.Text = Utiles.calcularMonto(txtCuotas.Text, txtNAV.Text, txtMonedaSerie.Text)  ' String.Format("{0:N2}", txtModalCuota.Text * txtModalNAV.Text)

    End Sub
#End Region
    Protected Sub navclp()
        If (txtNAVCLP.Text.ToString.Contains(",")) Then
            txtNAVCLP.Text = txtNAVCLP.Text.Substring("0", txtNAVCLP.Text.IndexOf(","))
        End If
    End Sub

#Region "Consultar según nemotécnico"
    Private Sub ConsultarFechaNav()
        Dim serieParam As FondoSerieDTO = New FondoSerieDTO
        Dim negocioSerie As FondoSeriesNegocio = New FondoSeriesNegocio
        Dim NegocioRescate As RescateNegocio = New RescateNegocio()

        serieParam.Nemotecnico = ddlNemotecnico.SelectedValue

        Dim SoloDiasHabiles As Integer

        Dim series As FondoSerieDTO
        series = negocioSerie.GetFondoSeriesNemotecnico(serieParam)

        Dim estructuraFechas = New EstructuraFechasDto
        estructuraFechas = Utiles.splitCharByComma(series.FechaNavSuscripcion)

        'FechaNavSuscripcion = series.FechaNavSuscripcion
        'Dim fechaNavC As String = estructuraFechas.DesdeQueFecha
        ' Dim diasNavC As String = estructuraFechas.DiasASumar

        'Dim Suscripcion As SuscripcionDTO = New SuscripcionDTO

        'If diasNavC = "" Then
        '    If (estructuraFechas.DesdeQueFecha = "FechaSuscripcion") Then
        '        Suscripcion.FechaSuscripcion = txtFechaSuscripcion.Text
        '    Else
        '        Suscripcion.FechaSuscripcion = txtFechaIntencion.Text
        '    End If


        '    Dim testString As String = FormatDateTime(FechaSolicitud, DateFormat.LongDate)
        '    FechaSolicitud = Suscripcion.FechaSuscripcion
        '    txtFechaNAV.Text = FechaSolicitud
        'Else
        'Dim dias As Integer = Integer.Parse(diasNavC)

        Dim FechaSolicitud As Date

        Select Case estructuraFechas.DesdeQueFecha
            Case "FechaSuscripcion"
                FechaSolicitud = txtFechaSuscripcion.Text
            Case Else
                FechaSolicitud = txtFechaIntencion.Text
        End Select

        'fechaSolicitud = Suscripcion.FechaSuscripcion

        If FechaSolicitud <> Nothing Then
            SoloDiasHabiles = IIf(series.SoloDiasHabilesFechaNavSuscripciones, Constantes.CONST_SOLO_DIAS_HABILES, Constantes.CONST_SOLO_DIAS_CORRIDOS)
            FechaSolicitud = Utiles.SumaDiasAFechas(ddlMonedaPago.Text, FechaSolicitud, estructuraFechas.DiasASumar, SoloDiasHabiles)
        End If

        txtFechaNAV.Text = FechaSolicitud
        'End If
        'Next
    End Sub

    Public Sub ConsultarFechaObservado()
        Dim serie As FondoSerieDTO = New FondoSerieDTO
        Dim NegocioRescate As RescateNegocio = New RescateNegocio()
        Dim negocioSerie As FondoSeriesNegocio = New FondoSeriesNegocio

        Dim listaSerie As List(Of FondoSerieDTO)

        serie.Nemotecnico = ddlNemotecnico.SelectedValue
        listaSerie = negocioSerie.GrupoSeriesPorNemotecnico(serie)

        For Each series As FondoSerieDTO In listaSerie
            Dim estructuraFechas As EstructuraFechasDto = Utiles.splitCharByComma(series.FechaTCSuscripcion)
            Dim fechaSolicitud As Date

            Select Case estructuraFechas.DesdeQueFecha
                Case "FechaSuscripcion"
                    fechaSolicitud = txtFechaSuscripcion.Text
                Case "FechaNav"
                    fechaSolicitud = txtFechaNAV.Text
            End Select

            If fechaSolicitud <> Nothing Then
                fechaSolicitud = Utiles.SumaDiasAFechas(ddlMonedaPago.Text, fechaSolicitud, estructuraFechas.DiasASumar, Constantes.CONST_SOLO_DIAS_HABILES)
                Dim bDiaInhabil As Boolean = (Not Utiles.esFechaHabil(ddlMonedaPago.Text, fechaSolicitud) And ddlMonedaPago.Text = "USD")

                If bDiaInhabil Then
                    ShowAlert(CONST_INHABIL_PARA_TC)
                End If

                fechaSolicitud = Utiles.getDiaHabilSiguiente(fechaSolicitud, ddlMonedaPago.Text)
                txtFechaTC.Text = fechaSolicitud
            Else
                txtFechaTC.Text = txtFechaIntencion.Text
            End If

        Next
    End Sub

    Public Sub ConsultarFechaSuscripcion()
        Dim serie As FondoSerieDTO = New FondoSerieDTO
        Dim negocioSerie As FondoSeriesNegocio = New FondoSeriesNegocio

        Dim listaSerie As List(Of FondoSerieDTO)
        Dim FechaNavSuscripcion As String

        serie.Nemotecnico = ddlNemotecnico.SelectedValue
        listaSerie = negocioSerie.GrupoSeriesPorNemotecnico(serie)

        For Each series As FondoSerieDTO In listaSerie
            Dim fechaNavC As String
            Dim diasNavC As String
            Dim estructuraFechas = New EstructuraFechasDto
            estructuraFechas = Utiles.splitCharByComma(series.FechaSuscripcion)
            Dim Suscripcion As SuscripcionDTO = New SuscripcionDTO

            FechaNavSuscripcion = series.FechaSuscripcion

            fechaNavC = estructuraFechas.DesdeQueFecha
            diasNavC = estructuraFechas.DiasASumar

            Dim FechaSolicitud As Date
            Dim testString As String = FormatDateTime(FechaSolicitud, DateFormat.LongDate)

            If diasNavC = "" Then
                Suscripcion.FechaIntencion = txtFechaIntencion.Text

                FechaSolicitud = Suscripcion.FechaIntencion
                txtFechaSuscripcion.Text = FechaSolicitud
            Else
                Dim dias As Integer = Integer.Parse(diasNavC)

                Suscripcion.FechaIntencion = txtFechaIntencion.Text
                fechaSolicitud = Suscripcion.FechaIntencion

                'FechaPagoFondoRescatableINT es días que hay que sumar o restar, FechaCalculo es a la fecha a la que hay que sumar o restar
                'FECHA DIAS HABILES

                If ddlMonedaPago.Text <> "" Then
                    FechaSolicitud = Utiles.SumaDiasAFechas(ddlMonedaPago.Text, FechaSolicitud, estructuraFechas.DiasASumar, Constantes.CONST_SOLO_DIAS_CORRIDOS)
                End If

                'fechaSolicitud = NegocioRescate.SelectFechaPagoSIRescatable(dias, fechaSolicitud, 0)
                txtFechaSuscripcion.Text = fechaSolicitud

            End If
        Next
    End Sub

    Public Sub MonedaSelectedChanged()
        ConsultarFechaSuscripcion()
        ConsultarFechaNav()
        ConsultarFechaObservado()
    End Sub
#End Region

#Region "VALORES EN TRÁNSITO"

    Public Sub Transitos()
        CargarCuotasDCV()
        RescateTransito()
        SuscripcionTransito()
        CanjeTransito()
    End Sub

    Public Sub RescateTransito()

        Dim Rescate As RescatesDTO = New RescatesDTO()
        Dim Negocio As RescateNegocio = New RescateNegocio
        Dim RescateActualizado As RescatesDTO = New RescatesDTO()

        If txtFechaSuscripcion.Text <> "" Then
            'Rescate.RES_Fecha_Pago = txtFechaSuscripcion.Text
            Rescate.RES_Fecha_Solicitud = txtFechaSuscripcion.Text
        Else
            'Rescate.RES_Fecha_Pago = Nothing
            Rescate.RES_Fecha_Solicitud = Nothing
        End If
        If (ddlModalRutAportante.SelectedValue <> "" And ddlNemotecnico.SelectedValue <> "") Then
            Rescate.FS_Nemotecnico = ddlNemotecnico.SelectedValue
            Rescate.FN_RUT = ddlFondo.SelectedValue
            Rescate.AP_RUT = ddlModalRutAportante.SelectedValue

            Rescate.AP_Multifondo = ddlModalMultifondo.SelectedValue

            RescateActualizado = Negocio.SelectRescatesTransito2(Rescate)

            If RescateActualizado IsNot Nothing Then
                txtRescates.Text = String.Format("{0:N0}", RescateActualizado.RES_Cuotas)
            Else
                txtRescates.Text = "0"
            End If
        Else
            txtRescates.Text = "0"
        End If
    End Sub

    Public Sub SuscripcionTransito()
        Dim suscripcion As SuscripcionDTO = New SuscripcionDTO()
        Dim NegocioSuscripcion As SuscripcionNegocio = New SuscripcionNegocio

        suscripcion.FechaSuscripcion = txtFechaSuscripcion.Text
        suscripcion.Nemotecnico = ddlNemotecnico.SelectedValue
        suscripcion.RutAportante = ddlModalRutAportante.SelectedValue

        suscripcion.Multifondo = ddlModalMultifondo.SelectedValue

        Dim suscripcionActualizado As SuscripcionDTO = NegocioSuscripcion.GetSuscripcionTransito2(suscripcion)

        If suscripcionActualizado IsNot Nothing Then
            txtSuscripciones.Text = String.Format("{0:N0}", suscripcionActualizado.CuotasASuscribir)
        Else
            txtSuscripciones.Text = "0"
        End If
    End Sub

    Public Sub CanjeTransito()
        Dim canje As CanjeDTO = New CanjeDTO
        Dim negocioCanje As CanjeNegocio = New CanjeNegocio

        canje.RutAportante = ddlModalRutAportante.SelectedValue
        canje.NemotecnicoSaliente = ddlNemotecnico.SelectedValue
        canje.FechaSolicitud = txtFechaSuscripcion.Text

        canje.Multifondo = ddlModalMultifondo.SelectedValue

        Dim RetornoCanje As CanjeDTO = negocioCanje.CanjesTransito(canje)

        If RetornoCanje IsNot Nothing Then
            txtCanje.Text = String.Format("{0:N0}", RetornoCanje.CanjeTransito)
        Else
            txtCanje.Text = "0"
        End If

        'CARGA TOTAL DISPONIBLES
        txtDisponibles.Text = String.Format("{0:N0}", txtCuotasDCV.Text - txtRescates.Text + txtSuscripciones.Text + txtCanje.Text)
    End Sub

#End Region

#Region "Changed"
    Protected Sub FechaNAVChanged()
        CalcularValorEntrante()
        Cuotaschanged()
    End Sub

    Protected Sub MontoTextChanged()
        Dim NegocioTipoCalculoNav As TipoCalculoNav = New TipoCalculoNav
        Dim TipoCalculoNav As TipoCalculoNavDTO = New TipoCalculoNavDTO()

        If (txtMonto.Text = "") Then
            txtMonto.Text = 0
        End If

        If (IsNumeric(txtMonto.Text)) Then
            If (IsNumeric(txtNAV.Text)) Then
                If (Double.Parse(txtNAV.Text) > 0) Then
                    Dim cuota As Integer = (Double.Parse(txtMonto.Text) / Double.Parse(txtNAV.Text))

                    txtCuotas.Text = String.Format("{0:N0}", Math.Floor(cuota))
                    Cuotaschanged()

                    TipoCalculoNav.ID = txtIdSuscripcion.Text()
                    TipoCalculoNav.TipoTransaccion = "Suscripcion"
                    TipoCalculoNav.TipoCalculo = "M"
                    Dim updateCNJ_Tipo_CalculoNAV = NegocioTipoCalculoNav.UpdateTipoCalculoNav(TipoCalculoNav)

                Else
                    txtCuotas.Text = 0
                    txtMonto.Text = 0

                End If

                llenarCLP()
            End If
        End If

        If (txtMonto.Text = 0) Then
            txtCuotas.Text = 0
        End If

    End Sub

    Protected Sub CuotasTextChanged()

        Dim NegocioTipoCalculoNav As TipoCalculoNav = New TipoCalculoNav
        Dim TipoCalculoNav As TipoCalculoNavDTO = New TipoCalculoNavDTO()
        Dim suscripcion As SuscripcionDTO = New SuscripcionDTO()

        If (IsNumeric(txtCuotas.Text) And IsNumeric(txtDisponiblesEmitidas.Text)) Then
            If (Double.Parse(txtCuotas.Text) > Double.Parse(txtDisponiblesEmitidas.Text)) Then
                ShowAlert("El número de cuotas no puede ser mayor que las cuotas disponibles")
                txtCuotas.Text = "0"
            Else
                If (txtCuotas.Text = "") Then
                    txtCuotas.Text = "0"
                End If

                suscripcion.CuotasASuscribir = Decimal.Parse(Utiles.SetNumberPointToDouble(txtCuotas.Text))
            End If
        End If
        Cuotaschanged()
        llenarCLP()


    End Sub
    Protected Sub TcObservadoChanged()
        If (txtTCObservado.Text = "") Then
            txtTCObservado.Text = "0"
        End If

        llenarCLP()
    End Sub

    Protected Sub Cuotaschanged()
        If (IsNumeric(txtNAV.Text) And IsNumeric(txtCuotas.Text)) Then
            If (txtMonedaSerie.Text = "CLP") Then
                Dim monto As Double = Utiles.SetNumberPointToDouble(Format(txtCuotas.Text * txtNAV.Text, "0"))
                Dim montoAux As Decimal = Utiles.SetToCapitalizedNumber(monto)
                txtMonto.Text = Utiles.calcularMonto(txtCuotas.Text, txtNAV.Text, txtMonedaSerie.Text) ' String.Format("{0:N0}", montoAux)
            Else
                Dim montoAux As Decimal = Utiles.SetToCapitalizedNumber(Format(Double.Parse(txtCuotas.Text * txtNAV.Text), "0.00"))
                txtMonto.Text = Utiles.calcularMonto(txtCuotas.Text, txtNAV.Text, txtMonedaSerie.Text)  ' String.Format("{0:N2}", montoAux)
            End If
        End If
        llenarCLP()

    End Sub

    Protected Sub navchanged()
        If (IsNumeric(txtNAV.Text)) Then
            If (IsNumeric(txtCuotas.Text)) Then
                Cuotaschanged()
            ElseIf (IsNumeric(txtMonto.Text)) Then
                MontoTextChanged()
            End If
        End If
    End Sub

    Protected Sub SuscripcionChanged() Handles txtFechaSuscripcion.TextChanged
        If (ddlNemotecnico.SelectedValue <> "") Then
            If (txtFechaSuscripcion.Text <> "") Then
                ConsultarFechaNav()
                CargarNombreMonedaSerieModal()
                ConsultarFechaObservado()
                Transitos()
                CalcularValorEntrante()
                Cuotaschanged()
                CargarNombreFondo()
                CargarNombreMonedaSerie()
                CargarTCObs()
                llenarCLP()
                'Calendar8.SelectedDate = Nothing
            Else
                Dim fondo = ddlNemotecnico.Text
                FormateoLimpiarDatosModal()
                FormateoEstiloFormCrear()
                ddlNemotecnico.SelectedValue = fondo
                ShowAlert(CONST_SIN_RESULTADOS_MODAL)
            End If
        End If
    End Sub

    Private Sub txtFechaIntencion_TextChanged(sender As Object, e As EventArgs) Handles txtFechaIntencion.TextChanged
        txtFechaIntencion.Text = Request.Form(txtFechaIntencion.UniqueID)
        If (ddlNemotecnico.SelectedValue IsNot "") Then
            ConsultarFechaSuscripcion()
            ConsultarFechaNav()
            CargarNombreMonedaSerieModal()
            ConsultarFechaObservado()
            Transitos()
            CalcularValorEntrante()
            CargarTCObs()
            CargarNombreFondo()
            CargarNombreMonedaSerie()
            llenarCLP()
            CalcularMontos()
        End If
    End Sub

    Private Sub txtFechaNAV_TextChanged(sender As Object, e As EventArgs) Handles txtFechaNAV.TextChanged
        CalcularValorEntrante()
        llenarCLP()
        CalcularMontos()
    End Sub

    Private Sub txtFechaTC_TextChanged(sender As Object, e As EventArgs) Handles txtFechaTC.TextChanged
        FechaTc()
        CargarTCObs()
        llenarCLP()
        CalcularMontos()
    End Sub
#End Region
#Region "Controles de Busqueda"

    Protected Sub ddlListaRutAportante_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlListaRutAportante.SelectedIndexChanged
        Dim suscripcion As New SuscripcionDTO

        suscripcion = GetRutFondoParametro(suscripcion)
        suscripcion = GetNemotecnicoParametro(suscripcion)
        suscripcion = GetRutAportanteParametro(suscripcion)

        If ddlListaRutFondo.SelectedValue.ToString().Trim() = "" Then
            llenarComboRutFondo(suscripcion)
        End If

        If ddlListaNemotecnico.SelectedValue.ToString().Trim() = "" Then
            llenarComboNemotecnicos(suscripcion)
        End If

    End Sub

    Protected Sub ddlListaRutFondo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlListaRutFondo.SelectedIndexChanged
        Dim suscripcion As New SuscripcionDTO

        suscripcion = GetRutFondoParametro(suscripcion)
        suscripcion = GetNemotecnicoParametro(suscripcion)
        suscripcion = GetRutAportanteParametro(suscripcion)

        If ddlListaNemotecnico.SelectedValue.ToString().Trim() = "" Then
            llenarComboNemotecnicos(suscripcion)
        End If

        If (ddlListaRutAportante.SelectedValue.ToString().Trim() = "") Then
            llenarComboRutAportantes(suscripcion)
        End If

    End Sub


    Protected Sub ddlListaNemotecnico_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlListaNemotecnico.SelectedIndexChanged
        Dim suscripcion As SuscripcionDTO = New SuscripcionDTO()

        suscripcion = GetRutFondoParametro(suscripcion)
        suscripcion = GetNemotecnicoParametro(suscripcion)
        suscripcion = GetRutAportanteParametro(suscripcion)

        If ddlListaRutFondo.SelectedValue.ToString().Trim() = "" Then
            llenarComboRutFondo(suscripcion)
        End If

        If (ddlListaRutAportante.SelectedValue.ToString().Trim() = "") Then
            llenarComboRutAportantes(suscripcion)
        End If
    End Sub

    Private Sub llenarComboNemotecnicos(suscripcion As SuscripcionDTO)
        Dim ListaSeries As List(Of SuscripcionDTO)
        Dim suscripcionvacia As New SuscripcionDTO

        ListaSeries = Negocio.GetSerieSuscripcion(suscripcion)

        If ListaSeries.Count = 0 Then
            ListaSeries.Add(suscripcionvacia)
        Else
            ListaSeries.Insert(0, suscripcionvacia)
        End If

        ddlListaNemotecnico.Items.Clear()

        ddlListaNemotecnico.DataSource = ListaSeries
        ddlListaNemotecnico.DataMember = "NemotecnicoRead"
        ddlListaNemotecnico.DataValueField = "NemotecnicoRead"
        ddlListaNemotecnico.DataBind()
        ddlListaNemotecnico.Items.Insert(0, New ListItem(0, String.Empty))
    End Sub

    Private Sub llenarComboRutFondo(suscripcion As SuscripcionDTO)
        Dim ListaFondos As List(Of SuscripcionDTO)
        Dim suscripcionvacia As New SuscripcionDTO

        ListaFondos = Negocio.GetFondoSuscripcion(suscripcion)

        If ListaFondos.Count = 0 Then
            ListaFondos.Add(suscripcionvacia)
        Else
            ListaFondos.Insert(0, suscripcionvacia)
        End If

        ddlListaRutFondo.Items.Clear()

        ddlListaRutFondo.DataSource = ListaFondos
        ddlListaRutFondo.DataMember = "RutFondoRead"
        ddlListaRutFondo.DataValueField = "RutFondoRead"
        ddlListaRutFondo.DataBind()
        ddlListaRutFondo.Items.Insert(0, New ListItem("", ""))

    End Sub

    Private Sub llenarComboRutAportantes(suscripcion As SuscripcionDTO)
        Dim ListaAportantes As List(Of SuscripcionDTO)
        Dim suscripcionvacia As New SuscripcionDTO

        ListaAportantes = Negocio.GetAportanteSuscripcion(suscripcion)

        If ListaAportantes.Count = 0 Then
            ListaAportantes.Add(suscripcionvacia)
        Else
            ListaAportantes.Insert(0, suscripcionvacia)
        End If

        ddlListaRutAportante.Items.Clear()


        ddlListaRutAportante.DataSource = ListaAportantes
        ddlListaRutAportante.DataMember = "RazonSocialRead"
        ddlListaRutAportante.DataValueField = "RazonSocialRead"
        ddlListaRutAportante.DataBind()
        ddlListaRutAportante.Items.Insert(0, New ListItem(0, String.Empty))

    End Sub

    Private Function GetRutAportanteParametro(suscripcion As SuscripcionDTO) As SuscripcionDTO
        Dim nombreAportante As String
        Dim rutAportante As String
        Dim arrCadena As String()

        If (ddlListaRutAportante.SelectedValue.ToString().Trim() <> "") Then

            arrCadena = ddlListaRutAportante.SelectedItem.Text().Split(New Char() {"/"c})

            If arrCadena.Length = 2 Then
                rutAportante = arrCadena(0).Trim()
                nombreAportante = arrCadena(1).Trim()

                suscripcion.RutAportante = rutAportante
            End If
        End If
        Return suscripcion
    End Function

    Private Function GetNemotecnicoParametro(ByRef suscripcion As SuscripcionDTO) As SuscripcionDTO
        If ddlListaNemotecnico.SelectedValue.ToString().Trim() <> "" Then
            suscripcion.Nemotecnico = ddlListaNemotecnico.SelectedValue.ToString().Trim()
        End If
        Return suscripcion
    End Function

    Private Function GetRutFondoParametro(ByRef suscripcion As SuscripcionDTO) As SuscripcionDTO
        Dim nombreFondo As String
        Dim rutFondo As String
        Dim arrCadena As String()

        If ddlListaRutFondo.SelectedValue.ToString().Trim() <> "" Then
            arrCadena = ddlListaRutFondo.SelectedItem.Text().Split(New Char() {"/"c})

            If arrCadena.Length = 2 Then
                rutFondo = arrCadena(0).Trim()
                nombreFondo = arrCadena(1).Trim()

                suscripcion.RutFondo = rutFondo
            End If

        End If

        Return suscripcion
    End Function
#End Region

    Private Sub btnPrueba_Click(sender As Object, e As EventArgs) Handles btnPrueba.Click
        GenerarPopUp()
    End Sub

    Private Sub GenerarPopUp()
        Dim Suscripcion As SuscripcionDTO = New SuscripcionDTO()
        Suscripcion.IdSuscripcion = txtIdSuscripcion.Text

        Dim negocioMod As SuscripcionNegocio = New SuscripcionNegocio()

        FillPopUp(negocioMod.GetSuscripcion(Suscripcion))
        txtAccionHidden.Value = "POPUPSUSCRIPCIONES"
    End Sub

    Private Sub FillPopUp(Suscripcion As SuscripcionDTO)
        lblPopUpFechaSolicitud.Text = Suscripcion.FechaSuscripcion.ToShortDateString
        lblPopUpHoraSolicitud.Text = Suscripcion.FechaSuscripcion.ToShortTimeString
        lblPopUpTipo.Text = Suscripcion.TipoTransaccion
        lblPopUpFondo.Text = Suscripcion.Nemotecnico
        lblPopUpNombreFondo.Text = Suscripcion.FondoNombreCorto
        lblPopUpSerie.Text = Suscripcion.SerieNombreCorto
        lblPopUpAdministradora.Text = "En Validacion con Moneda"
        lblPopUpRutAdministradora.Text = "En Validacion con Moneda"
        lblPopUpNombreAportante.Text = Suscripcion.RazonSocial
        lblPopUpRutAportante.Text = Suscripcion.RutAportanteRead
        lblPopUpMonedaDePago.Text = Suscripcion.Moneda_Pago
        lblPopUpCuotasMonto.Text = Suscripcion.CuotasASuscribir.ToString() + "/" + Suscripcion.Monto.ToString()
        lblPopUpNav.Text = "Por Confirmar"
        lblPopUpFechaNav.Text = Suscripcion.FechaNAV
        lblPopUpMontoDelAporte.Text = "Por Confirmar"
        lblPopUpCuotasDeAporte.Text = "Por Confirmar"
        lblPopUpContratoGralFondos.Text = Suscripcion.ContratoFondo
        lblPopUpPoderes.Text = Suscripcion.RevisionPoderes

    End Sub
End Class