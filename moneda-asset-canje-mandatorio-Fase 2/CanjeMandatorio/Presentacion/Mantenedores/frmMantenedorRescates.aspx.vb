Imports DTO
Imports Negocio
Imports DBSUtils

Partial Class Presentacion_Mantenedores_frmMantenedorRescates
    Inherits System.Web.UI.Page

    Private listaCarga As Object

    Public Const CONST_TITULO_VALORESCUOTA As String = "Rescate"
    Public Const CONST_TITULO_MODAL_MODIFICAR As String = "Modificar Rescate"
    Public Const CONST_TITULO_MODAL_ElIMINAR As String = "Eliminar Rescate"
    Public Const CONST_TITULO_MODAL_CREAR As String = "Nuevo Rescate"

    Public Const CONST_MODIFICAR_ERROR As String = "Error al modificar los datos de Rescate"
    Public Const CONST_MODIFICAR_EXITO As String = "Rescate modificado con éxito"
    Public Const CONST_ELIMINAR_ERROR As String = "Error al eliminar Rescate"
    Public Const CONST_ELIMINAR_EXITO As String = "Rescate eliminado con éxito"
    Public Const CONST_ELIMINAR_ESTADO_CERO As String = "No se puede eliminar un registro ya deshabilitado"
    Public Const CONST_ELIMINAR_EXISTE_EN_OTRA_TABLA As String = "Rescate se encuentra relacionado en otra Tabla"
    Public Const CONST_INSERTAR_ERROR As String = "Error al ingresar Rescate"
    Public Const CONST_INSERTAR_EXITO As String = "Rescate ingresado con éxito"
    Public Const CONST_VALIDA_RUT_EXISTE As String = "El RUT ya se encuentra creado."

    Public Const CONST_COL_RUT As Integer = 1
    Public Const CONST_COL_NEMOTECNICO As Integer = 2
    Public Const CONST_COL_FECHA As Integer = 3
    Public Const CONST_COL_FECHA_DESDE As Integer = 5
    Public Const CONST_COL_ESTADO As Integer = 9

    Public Const CONST_SIN_RESULTADOS As String = "No se obtuvieron resultados de la búsqueda"
    Public Const CONST_INHABIL_PARA_TC As String = "La fecha TC es inhábil en la moneda. Se moverá al hábil siguiente"

#Region "VALIDA PERMISOS"
    Private Sub ValidaPermisosPerfil()
        HiddenPerfil.Value = Session("PERFIL")
        HiddenConstante.Value = Constantes.CONST_PERFIL_CONSULTA

        If Session("PERFIL") = (Constantes.CONST_PERFIL_CONSULTA Or Nothing) Then
            btnCrear.Enabled = False
            BtnModificar.Enabled = False
            BtnEliminar.Enabled = False
            BtnExportar.Enabled = False
            btnProrrotear.Enabled = False

        ElseIf Session("PERFIL") = (Constantes.CONST_PERFIL_FULL Or Constantes.CONST_PERFIL_ADMIN) Then
            btnCrear.Visible = True
            BtnModificar.Visible = True
            BtnEliminar.Visible = True
            BtnExportar.Visible = True
            btnProrrotear.Visible = True

        End If
    End Sub
#End Region

#Region "INICIALIZA Y CARGA CONTROLES DE BUSQUEDA"
    Private Sub Presentacion_Mantenedores_frmMantenedorRescates_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            DataInitial()
        End If

        ValidaPermisosPerfil()
    End Sub

    Private Sub DataInitial()
        txtFechaSolicitudDesde.Text = ""
        txtFechaSolicitudHasta.Text = ""
        txtFechaNAVDesde.Text = ""
        txtFechaNAVHasta.Text = ""
        txtFechaPagoDesde.Text = ""
        txtFechaPagoHasta.Text = ""

        ddlModalContrato.Items.Clear()
        ddlModalContrato.Items.Insert(0, "")
        ddlModalContrato.Items.Insert(1, "OK")
        ddlModalContrato.Items.Insert(2, "NO OK")
        ddlModalContrato.Items.Insert(0, New ListItem("", ""))

        ddlModalPoderes.Items.Clear()
        ddlModalPoderes.Items.Insert(0, "")
        ddlModalPoderes.Items.Insert(1, "OK")
        ddlModalPoderes.Items.Insert(2, "NO OK")
        ddlModalPoderes.Items.Insert(0, New ListItem("", ""))

        ddlEstadoBuscar.Items.Insert(0, New ListItem("", ""))
        ddlEstadoBuscar.SelectedIndex = 0

        CargaNombreAportanteBuscar()
        CargaNombreFondoBuscar()
        ddlNemotecnicoBuscar.Items.Clear()
        CargaNemotecnicoBuscar()

        CargarMonedas()

        GrvTabla.DataSource = New List(Of RescatesDTO)
        GrvTabla.DataBind()
        BtnExportar.Enabled = (GrvTabla.Rows.Count <> 0)
        ValidaPermisosPerfil()
    End Sub

    Private Sub CargarMonedas()
        Dim lista As List(Of MonedasDTO)
        lista = Utiles.TraerMonedas()

        Dim listacorta = From m In lista Where m.MNCodigo = "CLP" Or m.MNCodigo = "USD" Select m

        ddlModalMonedaPago.DataSource = listacorta
        ddlModalMonedaPago.DataMember = "MNCodigo"
        ddlModalMonedaPago.DataValueField = "MNCodigo"
        ddlModalMonedaPago.DataBind()
        ddlModalMonedaPago.Items.Insert(0, New ListItem("", ""))
        'Utiles.CargarMonedas(ddlModalMonedaPago, "")
    End Sub

    Private Sub CargaNombreAportanteBuscar()

        llenarComboAportantesFiltrado(New RescatesDTO)

    End Sub

    Private Sub CargaNombreFondoBuscar()
        llenarComboFondosBuscar(New RescatesDTO)
    End Sub



    Private Sub CargaNemotecnicoBuscar()
        llenarComboNemotecnicoFiltrado(New RescatesDTO())
    End Sub
#End Region

#Region "BUSQUEDA INICIAL"
    Protected Sub BtnBuscar_Click(sender As Object, e As EventArgs)
        txtAccionHidden.Value = ""
        txtFechaSolicitudDesde.Text = Request.Form(txtFechaSolicitudDesde.UniqueID)
        txtFechaSolicitudHasta.Text = Request.Form(txtFechaSolicitudHasta.UniqueID)

        txtFechaNAVDesde.Text = Request.Form(txtFechaNAVDesde.UniqueID)
        txtFechaNAVHasta.Text = Request.Form(txtFechaNAVHasta.UniqueID)

        txtFechaPagoDesde.Text = Request.Form(txtFechaPagoDesde.UniqueID)
        txtFechaPagoHasta.Text = Request.Form(txtFechaPagoHasta.UniqueID)

        'If ddlAportanteBuscar.SelectedValue.Trim() = Nothing And txtFechaSolicitudDesde.Text.Equals("") And ddlNombreFondoBuscar.SelectedValue.Trim() = Nothing And txtFechaSolicitudHasta.Text.Equals("") And ddlNemotecnicoBuscar.SelectedValue.Trim() = Nothing And txtFechaNAVDesde.Text.Equals("") And ddlEstadoBuscar.SelectedValue.Trim() = Nothing And txtFechaNAVHasta.Text.Equals("") And txtFechaPagoDesde.Text.Equals("") And txtFechaPagoHasta.Text.Equals("") Then
        If ddlAportanteBuscar.SelectedValue.Trim() = Nothing And Request.Form(txtFechaSolicitudDesde.UniqueID).Equals("") And ddlNombreFondoBuscar.SelectedValue.Trim() = Nothing And Request.Form(txtFechaSolicitudHasta.UniqueID).Equals("") And ddlNemotecnicoBuscar.SelectedValue.Trim() = Nothing And Request.Form(txtFechaNAVDesde.UniqueID).Equals("") And ddlEstadoBuscar.SelectedValue.Trim() = Nothing And Request.Form(txtFechaNAVHasta.UniqueID).Equals("") And Request.Form(txtFechaPagoDesde.UniqueID).Equals("") And Request.Form(txtFechaPagoHasta.UniqueID).Equals("") Then
            CargarTodosRescate()

            If GrvTabla.Rows.Count = 0 Then
                BtnExportar.Enabled = False
                ShowAlert(CONST_SIN_RESULTADOS)
            Else
                BtnExportar.Enabled = True
            End If

        Else
            FindRescates()
            If GrvTabla.Rows.Count = 0 Then
                BtnExportar.Enabled = False
                ShowAlert(CONST_SIN_RESULTADOS)
            Else
                BtnExportar.Enabled = True
            End If

            txtAccionHidden.Value = ""
        End If

    End Sub

    Private Sub CargarTodosRescate()
        Dim Rescate As RescatesDTO = New RescatesDTO()
        Dim negocio As RescateNegocio = New RescateNegocio

        GrvTabla.DataSource = negocio.ConsultarTodos(Rescate)
        GrvTabla.DataBind()
    End Sub

    Private Sub FindRescates()
        Dim Rescate As RescatesDTO = New RescatesDTO()
        Dim negocio As RescateNegocio = New RescateNegocio

        Dim FechaDesdeSolicitud As Nullable(Of DateTime)
        Dim FechaHastaSolicitud As Nullable(Of DateTime)

        Dim FechaDesdeNAV As Nullable(Of DateTime)
        Dim FechaHastaNAV As Nullable(Of DateTime)

        Dim FechaDesdePago As Nullable(Of DateTime)
        Dim FechaHastaPago As Nullable(Of DateTime)


        If ddlAportanteBuscar.SelectedValue.Trim() = Nothing Then
            Rescate.AP_Razon_Social = Nothing
        Else
            Rescate.AP_Razon_Social = ddlAportanteBuscar.SelectedValue
        End If

        If (ddlAportanteBuscar.SelectedIndex > 0) Then
            Dim arrCadena As String() = ddlAportanteBuscar.SelectedItem.Text().Split(New Char() {"/"c})

            Rescate.AP_RUT = arrCadena(0).Trim()
            Rescate.AP_Razon_Social = arrCadena(1).Trim()
        Else
            Rescate.AP_RUT = Nothing
            Rescate.AP_Razon_Social = Nothing
        End If

        If ddlNombreFondoBuscar.SelectedValue.Trim() = Nothing Then
            Rescate.FN_Nombre_Corto = Nothing
        Else
            Rescate.FN_Nombre_Corto = ddlNombreFondoBuscar.SelectedValue
        End If

        If (ddlNombreFondoBuscar.SelectedIndex > 0) Then
            Dim arrCadena As String() = ddlNombreFondoBuscar.SelectedItem.Text().Split(New Char() {"/"c})

            Rescate.FN_RUT = arrCadena(0).Trim()
            Rescate.FN_Nombre_Corto = arrCadena(1).Trim()
        Else
            Rescate.FN_RUT = Nothing
            Rescate.FN_Nombre_Corto = Nothing
        End If

        If ddlNemotecnicoBuscar.SelectedValue.Trim() = Nothing Then
            Rescate.FS_Nemotecnico = Nothing
        Else
            Rescate.FS_Nemotecnico = ddlNemotecnicoBuscar.SelectedValue
        End If

        If ddlEstadoBuscar.SelectedValue.Trim() = Nothing Then
            Rescate.RES_Estado = Nothing
        Else
            Rescate.RES_Estado = ddlEstadoBuscar.SelectedValue
        End If

        If Not txtFechaSolicitudDesde.Text.Equals("") Then
            Rescate.RES_Fecha_Solicitud = Date.Parse(txtFechaSolicitudDesde.Text)
            'FechaDesdeSolicitud = Date.Parse(Request.Form(txtFechaSolicitudDesde.UniqueID))
        Else
            Rescate.RES_Fecha_Solicitud = Nothing
        End If

        If Not txtFechaSolicitudHasta.Text.Equals("") Then
            FechaHastaSolicitud = Date.Parse(txtFechaSolicitudHasta.Text)
        Else
            FechaHastaSolicitud = Nothing
        End If

        If Not txtFechaNAVDesde.Text.Equals("") Then
            Rescate.RES_Fecha_Nav = Date.Parse(txtFechaNAVDesde.Text)
            'FechaDesdeNAV = Date.Parse(Request.Form(txtFechaNAVDesde.UniqueID))
        Else
            Rescate.RES_Fecha_Nav = Nothing
        End If

        If Not txtFechaNAVHasta.Text.Equals("") Then
            FechaHastaNAV = Date.Parse(txtFechaNAVHasta.Text)
        Else
            FechaHastaNAV = Nothing
        End If

        If Not txtFechaPagoDesde.Text.Equals("") Then
            Rescate.RES_Fecha_Pago = Date.Parse(txtFechaPagoDesde.Text)
            'FechaDesdePago = Date.Parse(Request.Form(txtFechaPagoDesde.UniqueID))
        Else
            Rescate.RES_Fecha_Pago = Nothing
        End If

        If Not txtFechaPagoHasta.Text.Equals("") Then
            FechaHastaPago = Date.Parse(txtFechaPagoHasta.Text)
        Else
            FechaHastaPago = Nothing
        End If


        GrvTabla.DataSource = negocio.ConsultarPorFiltro(Rescate, FechaDesdeSolicitud, FechaHastaSolicitud, FechaDesdeNAV, FechaHastaNAV, FechaDesdePago, FechaHastaPago)
        GrvTabla.DataBind()

    End Sub

#End Region

#Region "CARGA COMBOS MODAL TODOS SIN FILTRO"
    Private Sub CargarRutFondoModal()


        Dim Negocio As FondoSeriesNegocio = New FondoSeriesNegocio
        Dim serie As FondoSerieDTO = New FondoSerieDTO()

        Dim listafondo As List(Of FondoSerieDTO) = Negocio.GetListaFondoRut(serie)

        If listafondo.Count = 0 Then
            ddlModalRutFondo.Items.Insert(0, New ListItem("", ""))
            ddlModalRutFondo.SelectedIndex = 0
        Else

            ddlModalRutFondo.DataSource = listafondo
            ddlModalRutFondo.DataMember = "Rut"
            ddlModalRutFondo.DataValueField = "Rut"
            ddlModalRutFondo.DataBind()
            ddlModalRutFondo.Items.Insert(0, New ListItem("", ""))
            ddlModalRutFondo.SelectedIndex = 0
        End If
    End Sub

    Public Sub CargaNombreFondoModal()
        Dim Negocio As FondosNegocio = New FondosNegocio
        Dim serie As FondoSerieDTO = New FondoSerieDTO()
        Dim fondo As FondoDTO = New FondoDTO()

        Dim listafondo As List(Of FondoDTO) = Negocio.GetNombreFondo(fondo)

        If listafondo.Count = 0 Then
            ddlModalNombreFondo.Items.Insert(0, New ListItem("", ""))
        Else
            ddlModalNombreFondo.DataSource = listafondo
            ddlModalNombreFondo.DataMember = "RazonSocial"
            ddlModalNombreFondo.DataValueField = "RazonSocial"
            ddlModalNombreFondo.DataBind()
            ddlModalNombreFondo.Items.Insert(0, New ListItem("", ""))
            ddlModalNombreFondo.SelectedIndex = 0
        End If
    End Sub



    Private Sub CargarRutAportanteModal()
        Dim negociosus As New SuscripcionNegocio
        Dim Suscripcion As New SuscripcionDTO
        Dim aportantes As List(Of SuscripcionDTO) = negociosus.GetAportanteDistinct(Suscripcion)

        If aportantes.Count = 0 Then
            ddlModalRutAportante.Items.Insert(0, New ListItem("", ""))
            ddlModalRutAportante.SelectedIndex = 0
        Else
            ddlModalRutAportante.DataSource = aportantes
            ddlModalRutAportante.DataMember = "RutAportante"
            ddlModalRutAportante.DataValueField = "RutAportante"
            ddlModalRutAportante.DataBind()
            ddlModalRutAportante.Items.Insert(0, New ListItem("", ""))
            ddlModalRutAportante.SelectedIndex = 0
        End If
    End Sub

    Private Sub CargarNombreAportanteModal()
        Dim NegocioAportante As AportanteNegocio = New AportanteNegocio
        Dim aportante As AportanteDTO = New AportanteDTO()
        Dim aportantes As List(Of AportanteDTO) = NegocioAportante.GetListaAportantesDistinct(aportante)

        If aportantes.Count = 0 Then
            ddlModalNombreAportante.Items.Insert(0, New ListItem("", ""))
            ddlModalNombreAportante.SelectedIndex = 0
        Else
            ddlModalNombreAportante.DataSource = aportantes
            ddlModalNombreAportante.DataMember = "razonSocial"
            ddlModalNombreAportante.DataValueField = "razonSocial"
            ddlModalNombreAportante.DataBind()
            ddlModalNombreAportante.Items.Insert(0, New ListItem("", ""))
            ddlModalNombreAportante.SelectedIndex = 0
        End If
    End Sub

    Private Sub CargarMultifondoModal()
        Dim NegocioAportante As AportanteNegocio = New AportanteNegocio
        Dim aportantes As List(Of AportanteDTO) = NegocioAportante.TraerMultifondos()

        If aportantes.Count = 0 Then
            ddlModalMultifondo.Items.Insert(0, New ListItem("", ""))
            ddlModalMultifondo.SelectedIndex = 0
        Else
            ddlModalMultifondo.DataSource = aportantes
            ddlModalMultifondo.DataMember = "Multifondo"
            ddlModalMultifondo.DataValueField = "Multifondo"
            ddlModalMultifondo.DataBind()
            ddlModalMultifondo.Items.Insert(0, New ListItem("", ""))
            ddlModalMultifondo.SelectedIndex = 0
        End If
    End Sub

    Public Sub CargaNemotecnicoModal()

        Dim NegocioFondoSerie As FondoSeriesNegocio = New FondoSeriesNegocio
        Dim fondoSeries As FondoSerieDTO = New FondoSerieDTO()
        Dim fondo As FondoDTO = New FondoDTO()

        fondo.NombreCorto = ddlModalNombreFondo.SelectedValue
        Dim listafondoSeries As List(Of FondoSerieDTO) = NegocioFondoSerie.GetListaFondoSerieConFiltro(fondoSeries, fondo)

        If listafondoSeries.Count = 0 Then
            ddlModalNemotecnico.Items.Insert(0, New ListItem("", ""))
            ddlModalNemotecnico.SelectedIndex = 0
        Else
            ddlModalNemotecnico.DataSource = listafondoSeries
            ddlModalNemotecnico.DataMember = "Nemotecnico"
            ddlModalNemotecnico.DataValueField = "Nemotecnico"
            ddlModalNemotecnico.DataBind()
            ddlModalNemotecnico.Items.Insert(0, New ListItem("", ""))
            ddlModalNemotecnico.SelectedIndex = 0

        End If

    End Sub

#End Region


#Region "CARGA MODAL PARA CREAR"
    Protected Sub btnCrear_Click(sender As Object, e As EventArgs) Handles btnCrear.Click
        Session("TipodeCalculoTC") = Nothing
        FormateoLimpiarDatosModal()
        FormateoEstiloFormCrear()

        'CARGAS DDL MODAL
        CargarRutFondoModal()
        CargaNombreFondoModal()
        CargarRutAportanteModal()
        CargarNombreAportanteModal()
        CargaNemotecnicoModal()
        CargarMultifondoModal()

        'CARGA FECHA SOLICITUD CON FECHA ACTUAL
        txtModalFechaSolicitud.Text = CDate(Date.Today.ToString("dd/MM/yyyy"))

        ddlModalContrato.SelectedIndex = 0
        ddlModalPoderes.SelectedIndex = 0

        txtAccionHidden.Value = "CREAR"
    End Sub

    Private Sub FormateoLimpiarDatosModal()
        txtModalCuota.Text = "0"
        txtModalTCObservado.Text = "0"
        txtModalFechaDCV.Text = ""
        txtModalObservaciones.Text = ""
        txtModalFijacionNAV.Text = ""
        txtModalFijacionTCObs.Text = ""
        txtModalNAV.Text = "0"
        txtModalNAV_CLP.Text = "0"
        txtModalPatrimonio.Text = "0"
        txtModalPorcentaje.Text = "0"
        txtModalRescateMax.Text = "0"
        txtModalUtilizado.Text = ""
        txtModalDisponibles.Text = ""
        txtModalFechaSolicitud.Text = ""
        txtModalFechaNAV.Text = ""
        txtModalFechaPago.Text = ""
        txtModalFechaTCObs.Text = ""
        txtModalMonto.Text = "0"
        txtModalMontoCLP.Text = "0"
        txtModalCuotasDVC.Text = "0"
        txtModalRescates.Text = "0"
        txtModalSuscripciones.Text = "0"
        txtModalCanje.Text = "0"
        txtModalDisponiblesCuotasDisponibles.Text = "0"
        txtModalMonedaSerie.Text = ""
        txtModalNombreSerie.Text = ""
        txtModalUtilizado.Text = "0"
        txtModalDisponibles.Text = "0"

        'INICIALIZA CAMPOS CON UPDATEPANEL
        ddlModalMultifondo.DataSource = Nothing
        ddlModalMultifondo.DataBind()

        ddlModalNemotecnico.DataSource = Nothing
        ddlModalNemotecnico.DataBind()

        ddlModalMonedaPago.SelectedIndex = 0
    End Sub

    Private Sub FormateoEstiloFormCrear()
        btnModalModificar.Enabled = False
        btnModalModificar.Visible = False
        btnModalGuardar.Enabled = True
        btnModalGuardar.Visible = True
        btnModalEliminar.Enabled = False

        ddlModalRutAportante.Enabled = True
        ddlModalNombreAportante.Enabled = True
        ddlModalMultifondo.Enabled = True
        txtModalNombreSerie.Enabled = False
        txtModalCuota.Enabled = True
        ddlModalMonedaPago.Enabled = True
        txtModalTCObservado.Enabled = True
        txtModalFechaSolicitud.Enabled = False
        txtModalFechaNAV.Enabled = False
        lnkBtnModalFechaNAV.Enabled = True
        lnkBtnModalFechaNAV.Visible = True
        lnkModalFechaSolicitud.Enabled = True
        lnkModalFechaSolicitud.Visible = True

        txtModalFechaPago.Enabled = False
        lnkBtnModalFechaPago.Visible = True
        txtModalFechaTCObs.Enabled = False
        lnkBtnModalFechaTCObs.Visible = True
        txtModalMonto.Enabled = True
        txtModalMontoCLP.Enabled = False
        ddlModalContrato.Enabled = True
        ddlModalRutFondo.Enabled = True
        ddlModalNombreFondo.Enabled = True
        ddlModalNemotecnico.Enabled = True
        txtModalMonedaSerie.Enabled = False
        txtModalNAV.Enabled = True
        txtModalNAV_CLP.Enabled = False
        ddlModalPoderes.Enabled = True
        ddlModalEstado.Enabled = True
        txtModalObservaciones.Enabled = True

        lnkModalFechaSolicitud.Visible = True

        lnkModalBorrarFechaSolicitud.Visible = False
        lnkBtnModalBorrarFechaNAV.Visible = False
        lnkBtnModalBorrarFechaPago.Visible = False
        lnkBtnModalBorrarFechaTCObs.Visible = False

        lblModalTitle.Text = CONST_TITULO_MODAL_CREAR
    End Sub
#End Region

#Region "CARGA NOMBRE FONDO Y NEMOTECNICO CUANDO CAMBIA COMBO RUT FONDO"
    Public Sub CargarNombreFondoNemotecnicoPorRutFondoModal()
        'CARGA NOMBRE FONDO Y NEMOTECNICO POR RUT FONDO
        CargarNombreFondoPorRutFondoModal()
        CargarNemotecnicoPorRutFondoModal()

        'CARGA NOMBRE Y MONEDA DE LA SERIE
        CargarNombreMonedaSerieModal()
    End Sub

    Public Sub CargarNombreFondoPorRutFondoModal()
        Dim NegocioFondo As FondosNegocio = New FondosNegocio
        Dim fondo As FondoSerieDTO = New FondoSerieDTO()
        fondo.Rut = ddlModalRutFondo.SelectedValue
        Dim listafondoSeries As List(Of FondoDTO) = NegocioFondo.GetNombrePorNemotecnico(fondo)

        If listafondoSeries.Count = 0 Then
            ddlModalNombreFondo.Items.Insert(0, New ListItem("", ""))
            ddlModalNombreFondo.SelectedIndex = 0
        Else

            ddlModalNombreFondo.DataSource = listafondoSeries
            ddlModalNombreFondo.DataMember = "RazonSocial"
            ddlModalNombreFondo.DataValueField = "RazonSocial"
            ddlModalNombreFondo.DataBind()
        End If

        'txtControlesHidden.Value = "EVENTO_ACTIVADO"

    End Sub

    Public Sub CargarNemotecnicoPorRutFondoModal()
        Dim NegocioFondoSerie As FondoSeriesNegocio = New FondoSeriesNegocio
        Dim fondoSeries As FondoSerieDTO = New FondoSerieDTO()
        Dim fondo As FondoDTO = New FondoDTO()
        fondoSeries.Rut = ddlModalRutFondo.SelectedValue
        Dim listafondoSeries As List(Of FondoSerieDTO) = NegocioFondoSerie.GetListaFondoSerieConFiltro(fondoSeries, fondo)

        If listafondoSeries.Count = 0 Then
            ddlModalNemotecnico.Items.Insert(0, New ListItem("", ""))
            ddlModalNemotecnico.SelectedIndex = 0
        Else
            ddlModalNemotecnico.DataSource = listafondoSeries
            ddlModalNemotecnico.DataMember = "Nemotecnico"
            ddlModalNemotecnico.DataValueField = "Nemotecnico"
            ddlModalNemotecnico.DataBind()
        End If
    End Sub

    Function cboExiste(ddl As DropDownList, s As String) As Boolean

        For Each l As ListItem In ddl.Items
            If l.Value = txtModalMonedaSerie.Text Then
                Return True
            End If
        Next
        Return False
    End Function

    Public Sub CargarNombreMonedaSerieModal()
        Session("TipodeCalculoTC") = "ActivaRutNombreNemotecnico"

        Dim NegocioFondoSerie As FondoSeriesNegocio = New FondoSeriesNegocio
        Dim NegocioRescate As RescateNegocio = New RescateNegocio

        Dim fondoSerie As FondoSerieDTO = New FondoSerieDTO()
        Dim primerRegistroNemotecnico As String = ""
        Dim FondoRescatable As String = ""
        Dim FechaNAVFondoRescatable As String = ""
        Dim FechaNAVFondoRescatableINT As Integer = 0
        Dim FechaPagoFondoRescatable As String = ""
        Dim FechaPagoFondoRescatableINT As Integer = 0
        Dim FechaTCFondoRescatable As String = ""

        Dim FechaSolicitud As Date
        Dim FechaNAV As Date
        Dim FechaPago As Date
        Dim FechaTC As Date
        Dim FijacionNAV As String = ""
        Dim PorcentajePatrimonio As String = ""

        'CARGA Y VALIDA RESTRICCION DE LA FECHA DE SOLICITUD
        Dim HorarioRestriccion As String
        Dim hora As Integer

        Dim EsSoloDiasHabiles As Integer = 0

        HorarioRestriccion = "00:00"
        PorcentajePatrimonio = 0

        Dim Fondo As FondoDTO = New FondoDTO()

        If ddlModalRutFondo.SelectedValue IsNot Nothing Then
            fondoSerie.Nemotecnico = ddlModalNemotecnico.SelectedValue
            fondoSerie.Rut = ddlModalRutFondo.SelectedValue
        Else
            fondoSerie.Nemotecnico = ddlModalNemotecnico.SelectedValue
            fondoSerie.Rut = Nothing
        End If

        'Dim ListaFondoSerieActualizado As List(Of FondoSerieDTO) = NegocioFondoSerie.GetListaFondoSerieConFiltro(fondoSerie, Fondo)
        Dim ListaFondoSerieActualizado As List(Of FondoSerieDTO) = NegocioFondoSerie.GrupoSeriesPorNemotecnico(fondoSerie)

        If ListaFondoSerieActualizado IsNot Nothing Then
            For Each FondoSerieAct As FondoSerieDTO In ListaFondoSerieActualizado
                If FondoSerieAct.HorarioRecaste = "" Then
                    HorarioRestriccion = "00:00"
                Else
                    HorarioRestriccion = FondoSerieAct.HorarioRecaste
                End If
                If FondoSerieAct.FondoRescatable = "" Then
                    FondoRescatable = "N/A"
                Else
                    FondoRescatable = FondoSerieAct.FondoRescatable
                End If

                If FondoSerieAct.Patrimonio = "" Then
                    PorcentajePatrimonio = "0"
                Else
                    PorcentajePatrimonio = FondoSerieAct.Patrimonio
                End If

                FechaNAVFondoRescatable = FondoSerieAct.FechaNav
                FechaPagoFondoRescatable = FondoSerieAct.FechaRescate
                FechaTCFondoRescatable = FondoSerieAct.FechaTCObservado
                FijacionNAV = FondoSerieAct.FijacionNav
                'CARGA NOMBRE DE LA SERIE
                txtModalNombreSerie.Text = FondoSerieAct.Nombrecorto
                'CARGA MONEDA DE LA SERIE
                txtModalMonedaSerie.Text = FondoSerieAct.Moneda

                If (FondoSerieAct.SoloDiasHabilesFechaNavRescate) Then
                    EsSoloDiasHabiles = 1
                Else
                    EsSoloDiasHabiles = 0
                End If
            Next
        End If

        Dim HoraSistema As String = Now.ToString("HH:mm:ss")
        Dim horaSistemaInt As Integer = CInt(HoraSistema.Substring(0, 2))

        If HorarioRestriccion = "" Or HorarioRestriccion = "00:00" Then
            hora = 99
        Else
            hora = CInt(HorarioRestriccion.Substring(0, 2))
        End If

        If horaSistemaInt >= hora Then
            txtModalFechaSolicitud.Text = CDate(Date.Now.AddDays(1).ToString("dd/MM/yyyy"))
        Else
            txtModalFechaSolicitud.Text = CDate(Date.Now.ToString("dd/MM/yyyy"))
        End If



        '//////////////SECCION PATRIMONIO/////////////////////
        'CARGA CAMPO PATRIMONIO DESDE LA INTERFAZ
        Dim Patrimonio As PatrimonioDTO = New PatrimonioDTO()
        Dim NegocioPatrimonio As PatrimonioNegocio = New PatrimonioNegocio

        Patrimonio.IDFONDO = ddlModalRutFondo.SelectedValue

        Dim PatrimonionActualizado As PatrimonioDTO = NegocioPatrimonio.GetPatrimonio(Patrimonio)

        If PatrimonionActualizado IsNot Nothing Then
            txtModalPatrimonio.Text = Utiles.SetToCapitalizedNumber(PatrimonionActualizado.NPATRIMONIO)
        Else
            txtModalPatrimonio.Text = "0"
        End If

        'CARGA PORCENTAJE % PATRIMONIO
        txtModalPorcentaje.Text = PorcentajePatrimonio

        'CALCULA RESCATE MAX
        txtModalRescateMax.Text = Utiles.SetToCapitalizedNumber(((txtModalPorcentaje.Text / 100) * txtModalPatrimonio.Text))

        'PATRIMONIO UTILIZADO (RESCATES DEL DIA)
        Dim RescateHoy As RescatesDTO = New RescatesDTO()
        Dim NegocioRescateHoy As RescateNegocio = New RescateNegocio
        Dim RescateActualizadoHoy As RescatesDTO = New RescatesDTO()

        If txtModalFechaSolicitud.Text <> "" Then
            RescateHoy.RES_Fecha_Solicitud = txtModalFechaSolicitud.Text
        Else
            RescateHoy.RES_Fecha_Solicitud = Nothing
        End If

        RescateHoy.FS_Nemotecnico = ddlModalNemotecnico.SelectedValue
        RescateHoy.FN_RUT = ddlModalRutFondo.SelectedValue
        RescateHoy.AP_RUT = ddlModalRutAportante.SelectedValue

        RescateActualizadoHoy = NegocioRescateHoy.SelectRescatesHoy(RescateHoy)

        If RescateActualizadoHoy IsNot Nothing Then
            txtModalUtilizado.Text = Utiles.SetToCapitalizedNumber(RescateActualizadoHoy.RES_Monto)
        Else
            txtModalRescates.Text = "0"
        End If

        'CALCULO DISPONIBLES PATRIMONIO
        txtModalDisponibles.Text = Utiles.SetToCapitalizedNumber(Double.Parse(txtModalRescateMax.Text) - Double.Parse(txtModalUtilizado.Text))

        '////////////FIN SECCION PATRIMONIO//////////////////


        'REFACTORIZACION DCV
        EstablecerDatosDCV()

        'VALIDA MONEDA PAGO
        If txtModalMonedaSerie.Text <> "USD" Then
            If txtModalMonedaSerie.Text = "" Then
                ddlModalMonedaPago.SelectedIndex = 0

            Else
                If cboExiste(ddlModalMonedaPago, txtModalMonedaSerie.Text) Then
                    ddlModalMonedaPago.Text = txtModalMonedaSerie.Text
                    ddlModalMonedaPago.Enabled = False
                Else
                    ddlModalMonedaPago.SelectedIndex = 0
                End If
            End If
        Else
            ddlModalMonedaPago.Enabled = True
        End If

        'VALIDA FECHAS NAV Y PAGO SEGUN TIPO DE FONDO RESCATABLE
        FechaSolicitud = txtModalFechaSolicitud.Text

        If txtModalFechaNAV.Text = "" Then
            FechaNAV = Nothing
        Else
            FechaNAV = txtModalFechaNAV.Text
        End If

        If txtModalFechaPago.Text = "" Then
            FechaPago = Nothing
        Else
            FechaPago = txtModalFechaPago.Text
        End If

        If txtModalFechaTCObs.Text = "" Then
            FechaTC = Nothing
        Else
            FechaTC = txtModalFechaTCObs.Text
        End If


        'If FondoRescatable = "Si" Then
        Dim TipoFechaAñadirNAV As String
        Dim TipoFechaAñadirPago As String
        Dim TipoFechaAñadirTC As String = ""
        Dim FechaCalculo As DateTime

        txtModalFechaNAV.Enabled = False
        txtModalFechaPago.Enabled = False
        txtModalFechaTCObs.Enabled = False
        lnkBtnModalFechaNAV.Visible = True
        lnkModalFechaSolicitud.Visible = True
        lnkBtnModalFechaPago.Visible = True
        lnkBtnModalFechaTCObs.Visible = True

        txtModalFechaNAV.Text = ""
        txtModalFechaPago.Text = ""
        txtModalFechaTCObs.Text = ""

        'FECHA NAV

        If FechaNAVFondoRescatable.Length > 1 Then
            TipoFechaAñadirNAV = FechaNAVFondoRescatable.Substring(5, 2)

            If TipoFechaAñadirNAV = "So" Then
                FechaCalculo = FechaSolicitud
                FechaNAVFondoRescatableINT = getDiasParaDesplazar(FechaNAVFondoRescatable)

            ElseIf TipoFechaAñadirNAV = "Re" Then
                'VALIDA QUE CARGUE 1 FECHA PAGO(RESCATE) PARA EVITAR FECHAS NO EXISTENTES
                'FECHA PAGO
                If FechaPagoFondoRescatable.Length > 1 Then
                    TipoFechaAñadirPago = FechaPagoFondoRescatable.Substring(5, 2)

                    FechaCalculo = getFechaParaCalculo(TipoFechaAñadirPago, FechaSolicitud, FechaPago, FechaNAV, FechaTC)

                    FechaPagoFondoRescatableINT = getDiasParaDesplazar(FechaPagoFondoRescatable)

                    'FECHA PAGO DIAS HABILES
                    If FechaCalculo = Nothing Then
                        txtModalFechaPago.Text = ""
                        FechaPago = Nothing
                    Else
                        'txtModalFechaPago.Text = NegocioRescate.SelectFechaPagoSIRescatable(FechaPagoFondoRescatableINT, FechaCalculo, 0)
                        txtModalFechaPago.Text = Utiles.SumaDiasAFechas(ddlModalMonedaPago.Text, FechaCalculo, FechaPagoFondoRescatableINT, Constantes.CONST_SOLO_DIAS_HABILES)
                        txtModalFechaPago.Text = CDate(txtModalFechaPago.Text).ToString("dd/MM/yyyy")
                        FechaPago = txtModalFechaPago.Text
                    End If

                End If
                ' FechaCalculo = txtModalFechaPago.Text
                FechaCalculo = FechaPago
                FechaNAVFondoRescatableINT = getDiasParaDesplazar(FechaNAVFondoRescatable)
            Else
                FechaCalculo = FechaTC
                FechaNAVFondoRescatableINT = getDiasParaDesplazar(FechaNAVFondoRescatable)

            End If

            'FECHA NAV DIAS CORRIDOS
            If FechaCalculo = Nothing Then
                txtModalFechaNAV.Text = ""
                FechaNAV = Nothing
            Else
                ' txtModalFechaNAV.Text = CDate(FechaCalculo.AddDays(FechaNAVFondoRescatableINT).ToString("dd/MM/yyyy"))
                ' txtModalFechaNAV.Text = NegocioRescate.SelectFechaPagoSIRescatable(FechaPagoFondoRescatableINT, FechaCalculo, EsSoloDiasHabiles)
                txtModalFechaNAV.Text = Utiles.SumaDiasAFechas(ddlModalMonedaPago.Text, FechaCalculo, FechaNAVFondoRescatableINT, EsSoloDiasHabiles)
                FechaNAV = txtModalFechaNAV.Text
            End If

        End If

        'FECHA PAGO
        If FechaPagoFondoRescatable.Length > 1 Then
            TipoFechaAñadirPago = FechaPagoFondoRescatable.Substring(5, 2)

            FechaCalculo = getFechaParaCalculo(TipoFechaAñadirPago, FechaSolicitud, FechaPago, FechaNAV, FechaTC)

            FechaPagoFondoRescatableINT = getDiasParaDesplazar(FechaPagoFondoRescatable)

            'FECHA PAGO DIAS HABILES

            If FechaCalculo = Nothing Then
                txtModalFechaPago.Text = ""
                FechaPago = Nothing
            Else
                'txtModalFechaPago.Text = NegocioRescate.SelectFechaPagoSIRescatable(FechaPagoFondoRescatableINT, FechaCalculo, 0)
                txtModalFechaPago.Text = Utiles.SumaDiasAFechas(ddlModalMonedaPago.Text, FechaCalculo, FechaPagoFondoRescatableINT, Constantes.CONST_SOLO_DIAS_HABILES)

                'txtModalFechaPago.Text = Utiles.getDiaHabilSiguiente(txtModalFechaPago.Text, ddlModalMonedaPago.Text)

                txtModalFechaPago.Text = CDate(txtModalFechaPago.Text).ToString("dd/MM/yyyy")
                FechaPago = txtModalFechaPago.Text
            End If

            'If FechaPagoFondoRescatableINT = 0 Then
            '    txtModalFechaPago.Text = FechaCalculo.ToString("dd/MM/yyyy")
            '    FechaPago = txtModalFechaPago.Text
            'Else
            '    txtModalFechaPago.Text = NegocioRescate.SelectFechaPagoSIRescatable(FechaPagoFondoRescatableINT, FechaCalculo)
            '    txtModalFechaPago.Text = CDate(txtModalFechaPago.Text).ToString("dd/MM/yyyy")
            '    FechaPago = txtModalFechaPago.Text
            'End If
        End If

        'FECHA TIPO CAMBIO

        If (FechaTCFondoRescatable.Length > 1) Then
            TipoFechaAñadirTC = FechaTCFondoRescatable.Substring(5, 2)

            FechaCalculo = getFechaParaCalculo(TipoFechaAñadirTC, FechaSolicitud, FechaPago, FechaNAV, FechaTC)

            FechaPagoFondoRescatableINT = getDiasParaDesplazar(FechaTCFondoRescatable)

            If FechaCalculo = Nothing Then
                txtModalFechaTCObs.Text = ""
                FechaTC = Nothing
            Else
                'txtModalFechaTCObs.Text = CDate(NegocioRescate.SelectFechaPagoSIRescatable(FechaPagoFondoRescatableINT, FechaCalculo, 0)).ToString("dd/MM/yyyy")
                txtModalFechaTCObs.Text = Utiles.SumaDiasAFechas(ddlModalMonedaPago.Text, FechaCalculo, FechaPagoFondoRescatableINT, Constantes.CONST_SOLO_DIAS_CORRIDOS)
                Dim bDiaInhabil As Boolean = (Not Utiles.esFechaHabil(ddlModalMonedaPago.Text, txtModalFechaTCObs.Text) And ddlModalMonedaPago.Text = "USD")
                txtModalFechaTCObs.Text = Utiles.getDiaHabilSiguiente(txtModalFechaTCObs.Text, ddlModalMonedaPago.Text)

                If bDiaInhabil Then
                    ShowAlert(CONST_INHABIL_PARA_TC)
                End If

                FechaTC = txtModalFechaTCObs.Text
            End If
            'If FechaPagoFondoRescatableINT = 0 Then
            '    txtModalFechaTCObs.Text = FechaCalculo
            '    txtModalFechaTCObs.Text = CDate(txtModalFechaTCObs.Text).ToString("dd/MM/yyyy")
            '    FechaTC = txtModalFechaTCObs.Text
            'Else
            '    txtModalFechaTCObs.Text = CDate(NegocioRescate.SelectFechaPagoSIRescatable(FechaPagoFondoRescatableINT, FechaCalculo)).ToString("dd/MM/yyyy")
            '    FechaTC = txtModalFechaTCObs.Text
            'End If

        Else
            FechaCalculo = FechaSolicitud
            FechaPagoFondoRescatableINT = 0
        End If

        If FondoRescatable = "No" Then
            txtModalFechaNAV.Enabled = False
            txtModalFechaPago.Enabled = False
            txtModalFechaTCObs.Enabled = False
            lnkBtnModalFechaNAV.Visible = True
            lnkModalFechaSolicitud.Visible = True
            lnkBtnModalFechaPago.Visible = True
            lnkBtnModalFechaTCObs.Visible = True

            Dim VentanasRescate As VentanasRescateDTO = New VentanasRescateDTO()
            Dim NegocioVentanasRescate As VentanasRescateNegocio = New VentanasRescateNegocio
            Dim VentanasRescateActualizado As VentanasRescateDTO = New VentanasRescateDTO()
            VentanasRescateActualizado = Nothing

            If ddlModalNombreFondo.Text <> "" And ddlModalNemotecnico.Text <> "" And txtModalFechaSolicitud.Text <> "" Then
                VentanasRescate.FN_Nombre_Corto = ddlModalNombreFondo.SelectedValue
                VentanasRescate.FS_Nemotecnico = ddlModalNemotecnico.SelectedValue
                VentanasRescate.RES_Fecha_Solicitud = txtModalFechaSolicitud.Text
                VentanasRescateActualizado = NegocioVentanasRescate.SelectFechasNORescatable(VentanasRescate)
            End If

            If VentanasRescateActualizado IsNot Nothing Then
                txtModalFechaNAV.Text = ""
                txtModalFechaPago.Text = ""
                txtModalFechaNAV.Text = CDate(VentanasRescateActualizado.VTRES_Fecha_NAV.ToString("dd/MM/yyyy"))
                txtModalFechaPago.Text = CDate(VentanasRescateActualizado.VTRES_Fecha_Pago.ToString("dd/MM/yyyy"))

                FechaCalculo = getFechaParaCalculo(TipoFechaAñadirTC, FechaSolicitud, txtModalFechaPago.Text, txtModalFechaNAV.Text, FechaTC)

                FechaPagoFondoRescatableINT = getDiasParaDesplazar(FechaTCFondoRescatable)

                If FechaCalculo = Nothing Then
                    txtModalFechaTCObs.Text = ""
                Else
                    'txtModalFechaTCObs.Text = CDate(NegocioRescate.SelectFechaPagoSIRescatable(FechaPagoFondoRescatableINT, FechaCalculo, 0)).ToString("dd/MM/yyyy")
                    txtModalFechaTCObs.Text = Utiles.SumaDiasAFechas(ddlModalMonedaPago.Text, FechaCalculo, FechaPagoFondoRescatableINT, Constantes.CONST_SOLO_DIAS_CORRIDOS)
                    Dim bDiaInhabil As Boolean = (Not Utiles.esFechaHabil(ddlModalMonedaPago.Text, txtModalFechaTCObs.Text) And ddlModalMonedaPago.Text = "USD")
                    txtModalFechaTCObs.Text = Utiles.getDiaHabilSiguiente(txtModalFechaTCObs.Text, ddlModalMonedaPago.Text)

                    If bDiaInhabil Then
                        ShowAlert(CONST_INHABIL_PARA_TC)
                    End If
                End If

            Else
                txtModalFechaNAV.Text = ""
                txtModalFechaPago.Text = ""
                txtModalFechaTCObs.Text = ""
            End If

        ElseIf FondoRescatable = "N/A" Then
            txtModalFechaNAV.Text = ""
            txtModalFechaPago.Text = ""
            txtModalFechaTCObs.Text = ""

            txtModalFechaNAV.Enabled = False
            txtModalFechaTCObs.Enabled = False
            txtModalFechaPago.Enabled = False
            lnkBtnModalFechaNAV.Visible = True
            lnkModalFechaSolicitud.Visible = True
            lnkBtnModalFechaPago.Visible = True
            lnkBtnModalFechaTCObs.Enabled = True
        Else
            txtModalFechaNAV.Enabled = False
            txtModalFechaTCObs.Enabled = False
            txtModalFechaPago.Enabled = False
            lnkBtnModalFechaNAV.Visible = True
            lnkModalFechaSolicitud.Visible = True
            lnkBtnModalFechaPago.Visible = True
            lnkBtnModalFechaTCObs.Enabled = True

        End If

        'TRAER NAV DEPENDE DE LA FIJACION
        Dim ValoresCuota As VcSerieDTO = New VcSerieDTO()
        Dim NegocioValoresCuota As ValoresCuotaNegocio = New ValoresCuotaNegocio
        Dim ValoresCuotaActualizado As VcSerieDTO = New VcSerieDTO()

        If FijacionNAV = "Automático" Or FijacionNAV = "Manual" Then
            ValoresCuota.FsNemotecnico = ddlModalNemotecnico.SelectedValue
            If txtModalFechaNAV.Text = "" Then
                ValoresCuota.Fecha = Nothing
            Else
                ValoresCuota.Fecha = txtModalFechaNAV.Text
            End If
            ValoresCuota.FnRut = ddlModalRutFondo.SelectedValue
            ValoresCuotaActualizado = NegocioValoresCuota.GetValoresCuota(ValoresCuota)

            If ValoresCuotaActualizado IsNot Nothing Then
                txtModalNAV.Text = Utiles.formatearNAV(ValoresCuotaActualizado.Valor)  ' String.Format("{0:N6}", ValoresCuotaActualizado.Valor)

                'TRAER ULTIMA TC DE CLP
                Dim TipoCambio1 As TipoCambioDTO = New TipoCambioDTO()
                Dim NegocioTipoCambio1 As TipoCambioNegocio = New TipoCambioNegocio
                Dim TipoCambioActualizado1 As TipoCambioDTO = New TipoCambioDTO()
                Dim txtModalNAVText As Decimal
                'TipoCambio1.Codigo = "CLP"
                TipoCambio1.Codigo = txtModalMonedaSerie.Text
                Dim valorTC As Decimal
                Dim ListaTC As List(Of TipoCambioDTO) = NegocioTipoCambio1.UltimoTipoCambioPorCodigo(TipoCambio1)

                If txtModalMonedaSerie.Text = "CLP" Then
                    txtModalNAVText = txtModalNAV.Text
                    txtModalNAV_CLP.Text = Utiles.formatearNAVCLP(txtModalNAVText)  ' String.Format("{0:N4}", Convert.ToDecimal(txtModalNAVText))
                Else
                    For Each tipos As TipoCambioDTO In ListaTC
                        valorTC = Double.Parse(tipos.Valor)
                        txtModalNAV_CLP.Text = Utiles.calcularNAVCLP(valorTC, txtModalNAV.Text)   '  String.Format("{0:N4}", Convert.ToDecimal(txtModalNAVText * valor1))
                    Next
                End If

                txtModalFijacionNAV.Text = "Realizado"
            Else
                'TRAE EL ULTIMO VALORES CUOTA POR NEMOTECNICO
                ValoresCuota.FsNemotecnico = ddlModalNemotecnico.SelectedValue

                Dim ListaVc As List(Of VcSerieDTO) = NegocioValoresCuota.UltimoValorCuota(ValoresCuota)
                Dim txtModalNAVText As Decimal

                For Each ValCuota As VcSerieDTO In ListaVc
                    txtModalNAV.Text = Utiles.formatearNAV(ValCuota.Valor)  ' String.Format("{0:N4}", ValCuota.Valor)
                    txtModalNAVText = txtModalNAV.Text

                    If txtModalMonedaSerie.Text = "CLP" Then
                        txtModalNAV_CLP.Text = Utiles.formatearNAVCLP(txtModalNAVText)  ' String.Format("{0:N4}", Convert.ToDecimal(txtModalNAVText))
                    Else

                        'TRAER ULTIMA TC DE CLP
                        Dim TipoCambio1 As TipoCambioDTO = New TipoCambioDTO()
                        Dim NegocioTipoCambio1 As TipoCambioNegocio = New TipoCambioNegocio
                        Dim TipoCambioActualizado1 As TipoCambioDTO = New TipoCambioDTO()
                        'TipoCambio1.Codigo = "CLP"
                        TipoCambio1.Codigo = txtModalMonedaSerie.Text
                        'Dim valor1 As Decimal
                        Dim ListaTC As List(Of TipoCambioDTO) = NegocioTipoCambio1.UltimoTipoCambioPorCodigo(TipoCambio1)

                        If txtModalMonedaSerie.Text = "CLP" Then
                            txtModalNAV_CLP.Text = Utiles.formatearNAVCLP(txtModalNAVText)  '   String.Format("{0:N4}", Convert.ToDecimal(txtModalNAVText))
                        Else
                            For Each oTipoCambio As TipoCambioDTO In ListaTC
                                txtModalNAV_CLP.Text = Utiles.calcularNAVCLP(oTipoCambio.Valor, txtModalNAVText) ' String.Format("{0:N4}", Convert.ToDecimal(txtModalNAVText * valor1))
                            Next
                        End If
                    End If

                Next
                txtModalFijacionNAV.Text = "Pendiente"
            End If

        Else
            'ElseIf FijacionNAV = "Manual" Then
            txtModalNAV.Text = "0"
            txtModalFijacionNAV.Text = "Pendiente"
        End If

        'TRAER VALOR TIPO CAMBIO OBSERVADO
        TraerYFijarTCObservado()

        cargaRescatesEnTransito()
        cargaSuscripcionesEnTransito()
        cargaCanjesEnTransito()

        'CARGA TOTAL DISPONIBLES
        txtModalDisponiblesCuotasDisponibles.Text = Utiles.SetToCapitalizedNumber(txtModalCuotasDVC.Text - txtModalRescates.Text + txtModalSuscripciones.Text + txtModalCanje.Text)

        'Reset de valores si FondoRescatable es 'N/A'
        resetValores(FondoRescatable)

    End Sub

    Private Function getFechaParaCalculo(ByVal TipoFechaAñadirPago As String, ByVal FechaSolicitud As Date, ByVal FechaPago As Date, ByVal FechaNAV As Date, ByVal FechaTC As Date) As Date
        Dim FechaCalculo As Date
        Select Case TipoFechaAñadirPago.ToLower()
            Case "so"
                FechaCalculo = FechaSolicitud
            Case "re"
                FechaCalculo = FechaPago
            Case "na"
                FechaCalculo = FechaNAV
            Case Else
                FechaCalculo = FechaTC
        End Select
        Return FechaCalculo
    End Function

    Private Function getFechaParaCalculo(ByVal estructuraFechas As EstructuraFechasDto, ByVal FechaSolicitud As Date, ByVal FechaPago As Date, ByVal FechaNAV As Date, ByVal FechaTC As Date, ByVal fechaCanje As Date) As Date
        Dim FechaCalculo As Date

        Select Case estructuraFechas.DesdeQueFecha.ToLower()
            Case "fechasolicitud"
                FechaCalculo = FechaSolicitud
            Case "fecharescate"
                FechaCalculo = FechaPago
            Case "fechanav"
                FechaCalculo = FechaNAV
            Case "fechacanje"
                FechaCalculo = fechaCanje
            Case Else
                FechaCalculo = FechaTC
        End Select

        Return FechaCalculo
    End Function


    Private Sub TraerYFijarTCObservado()
        Dim TipoCambio As TipoCambioDTO = New TipoCambioDTO()
        Dim NegocioTipoCambio As TipoCambioNegocio = New TipoCambioNegocio
        Dim TipoCambioActualizado As TipoCambioDTO = New TipoCambioDTO()


        If txtModalFechaTCObs.Text <> "" And txtModalMonedaSerie.Text <> "" Then
            TipoCambio.Fecha = txtModalFechaTCObs.Text
            TipoCambio.Codigo = txtModalMonedaSerie.Text

            TipoCambioActualizado = NegocioTipoCambio.GetTipoCambio(TipoCambio)

            If TipoCambioActualizado IsNot Nothing Then
                txtModalTCObservado.Text = Utiles.SetToCapitalizedNumber(TipoCambioActualizado.Valor)
                txtModalFijacionTCObs.Text = "Realizado"
            Else
                getUltimoValorTC(txtModalMonedaSerie.Text)
                txtModalFijacionTCObs.Text = "Pendiente"
            End If

        Else
            getUltimoValorTC(txtModalMonedaSerie.Text)
            txtModalFijacionTCObs.Text = "Pendiente"
        End If


        'SI LA MONEDA ES CLP CARGA NAV_CLP
        If txtModalMonedaSerie.Text = "CLP" Then
            Dim txtModalNAVText As Decimal
            txtModalNAVText = txtModalNAV.Text
            txtModalNAV_CLP.Text = Utiles.formatearNAVCLP(txtModalNAVText)  'String.Format("{0:N4}", Convert.ToDecimal(txtModalNAVText))
            txtModalFijacionTCObs.Text = "Realizado"
        End If
    End Sub

    Private Sub resetValores(FondoRescatable As String)
        If FondoRescatable = "N/A" Then
            txtModalFechaNAV.Text = ""
            txtModalFechaPago.Text = ""
            txtModalFechaTCObs.Text = ""
            txtModalNAV.Text = "0"
            txtModalNAV_CLP.Text = "0"
            txtModalTCObservado.Text = "0"
        End If
    End Sub

    Private Sub cargaRescatesEnTransito()
        'CARGA RESCATES EN TRANSITO
        Dim Rescate As RescatesDTO = New RescatesDTO()
        Dim Negocio As RescateNegocio = New RescateNegocio
        Dim RescateActualizado As RescatesDTO = New RescatesDTO()

        ' If txtModalFechaPago.Text <> "" Then
        If txtModalFechaSolicitud.Text <> "" Then
            Rescate.RES_Fecha_Solicitud = txtModalFechaSolicitud.Text
            'Rescate.RES_Fecha_Pago = txtModalFechaPago.Text
        Else
            Rescate.RES_Fecha_Solicitud = Nothing
            'Rescate.RES_Fecha_Pago = Nothing
        End If

        Rescate.FS_Nemotecnico = ddlModalNemotecnico.SelectedValue
        Rescate.FN_RUT = ddlModalRutFondo.SelectedValue
        Rescate.AP_RUT = ddlModalRutAportante.SelectedValue
        Rescate.AP_Multifondo = ddlModalMultifondo.SelectedValue

        RescateActualizado = Negocio.SelectRescatesTransito(Rescate)

        If RescateActualizado IsNot Nothing Then
            txtModalRescates.Text = Utiles.SetToCapitalizedNumber(RescateActualizado.RES_Cuotas)
        Else
            txtModalRescates.Text = "0"
        End If
    End Sub
    Private Sub cargaSuscripcionesEnTransito()
        'CARGA SUSCRIPCIONES EN TRANSITO
        Dim suscripcion As SuscripcionDTO = New SuscripcionDTO()
        Dim NegocioSuscripcion As SuscripcionNegocio = New SuscripcionNegocio

        suscripcion.FechaSuscripcion = txtModalFechaSolicitud.Text
        suscripcion.Nemotecnico = ddlModalNemotecnico.SelectedValue
        suscripcion.RutAportante = ddlModalRutAportante.SelectedValue
        suscripcion.Multifondo = ddlModalMultifondo.SelectedValue

        Dim suscripcionActualizado As SuscripcionDTO = NegocioSuscripcion.GetSuscripcionTransito(suscripcion)

        If suscripcionActualizado IsNot Nothing Then
            txtModalSuscripciones.Text = Utiles.SetToCapitalizedNumber(suscripcionActualizado.CuotasASuscribir)
        Else
            txtModalSuscripciones.Text = "0"
        End If
    End Sub
    Private Sub cargaCanjesEnTransito()
        'CARGA DE CANJES EN TRANSITO
        Dim canje As CanjeDTO = New CanjeDTO
        Dim negocioCanje As CanjeNegocio = New CanjeNegocio

        canje.RutAportante = ddlModalRutAportante.SelectedValue
        canje.NemotecnicoSaliente = ddlModalNemotecnico.SelectedValue
        canje.FechaSolicitud = txtModalFechaSolicitud.Text
        canje.Multifondo = ddlModalMultifondo.SelectedValue

        Dim RetornoCanje As CanjeDTO = negocioCanje.CanjesTransito(canje)

        If RetornoCanje IsNot Nothing Then
            txtModalCanje.Text = Utiles.SetToCapitalizedNumber(RetornoCanje.CanjeTransito)
        Else
            txtModalCanje.Text = "0"
        End If

    End Sub


    ''' <summary>
    ''' SETEA VALORES DE FECHA DCV Y CUOTAS DCV
    ''' </summary>
    Private Sub EstablecerDatosDCV()
        'CARGA LA FECHA Y CUOTA  DCV
        Dim ADCV As ADCVDTO = New ADCVDTO()
        Dim NegocioADCV As ADCVNegocio = New ADCVNegocio
        Dim ADCVActualizado As ADCVDTO

        ADCV.AP_RUT = IIf(ddlModalRutAportante.SelectedValue.Trim() = "", Nothing, ddlModalRutAportante.SelectedValue.Trim())
        ADCV.ADCV_Razon_Social = IIf(ddlModalNombreAportante.SelectedValue.Trim() = "", Nothing, ddlModalNombreAportante.SelectedValue.Trim())
        ADCV.FS_Nemotecnico = IIf(ddlModalNemotecnico.SelectedValue.Trim() = "", Nothing, ddlModalNemotecnico.SelectedValue.Trim())
        ADCV.ADCV_Fecha = IIf(txtModalFechaSolicitud.Text = "", Nothing, txtModalFechaSolicitud.Text)

        If ADCV.AP_RUT <> "" AndAlso ADCV.ADCV_Razon_Social <> "" AndAlso ADCV.FS_Nemotecnico <> "" AndAlso ADCV.ADCV_Fecha <> Nothing Then

            ADCVActualizado = NegocioADCV.GetADCV(ADCV)

            If ADCVActualizado Is Nothing Then
                Dim ADCVUltimo As List(Of ADCVDTO) = NegocioADCV.UltimoDCV(ADCV)

                If ADCVUltimo.Count = 0 Then
                    txtModalCuotasDVC.Text = "0"
                    'txtModalFechaDCV.Text = CDate(Date.Now.AddDays(-1).ToString("dd/MM/yyyy"))
                    txtModalFechaDCV.Text = ""
                Else
                    For Each ADCVUltimo1 As ADCVDTO In ADCVUltimo
                        txtModalCuotasDVC.Text = Utiles.SetToCapitalizedNumber(ADCVUltimo1.ADCV_Cantidad)
                        txtModalFechaDCV.Text = CDate(ADCVUltimo1.ADCV_Fecha.ToString("dd/MM/yyyy"))
                    Next
                End If
            Else
                txtModalFechaDCV.Text = CDate(ADCVActualizado.ADCV_Fecha.ToString("dd/MM/yyyy"))
                txtModalCuotasDVC.Text = Utiles.SetToCapitalizedNumber(ADCVActualizado.ADCV_Cantidad)
            End If
        Else
            txtModalCuotasDVC.Text = "0"
            txtModalFechaDCV.Text = ""
        End If
    End Sub

#End Region

#Region "CARGA RUT FONDO Y NEMOTECNICO CUANDO CAMBIA COMBO NOMBRE FONDO"
    Public Sub CargarRutFondoNemotecnicoPorNombreFondoModal()
        'CARGA NOMBRE FONDO Y NEMOTECNICO POR RUT FONDO
        CargarRutFondoPorNombreFondoModal()
        CargarNemotecnicoPorNombreFondoModal()

        'CARGA NOMBRE Y MONEDA DE LA SERIE
        CargarNombreMonedaSerieModal()
    End Sub

    Public Sub CargarRutFondoPorNombreFondoModal()
        Dim NegocioFondo As FondosNegocio = New FondosNegocio
        Dim fondo As FondoDTO = New FondoDTO()
        fondo.RazonSocial = ddlModalNombreFondo.SelectedValue
        Dim listafondoSeries As List(Of FondoDTO) = NegocioFondo.RutByNombreFondo(fondo)

        If listafondoSeries.Count = 0 Then
            ddlModalRutFondo.Items.Insert(0, New ListItem("", ""))
            ddlModalRutFondo.SelectedIndex = 0
        Else

            ddlModalRutFondo.DataSource = listafondoSeries
            ddlModalRutFondo.DataMember = "rut"
            ddlModalRutFondo.DataValueField = "rut"
            ddlModalRutFondo.DataBind()
        End If

        'txtControlesHidden.Value = "EVENTO_ACTIVADO"

    End Sub

    Public Sub CargarNemotecnicoPorNombreFondoModal()

        Dim NegocioFondoSerie As FondoSeriesNegocio = New FondoSeriesNegocio
        Dim fondoSeries As FondoSerieDTO = New FondoSerieDTO()
        Dim fondo As FondoDTO = New FondoDTO()
        fondo.RazonSocial = ddlModalNombreFondo.SelectedValue
        Dim listafondoSeries As List(Of FondoSerieDTO) = NegocioFondoSerie.GetbyNombreFondo(fondoSeries, fondo)

        If listafondoSeries.Count = 0 Then
            ddlModalNemotecnico.Items.Insert(0, New ListItem("", ""))
            ddlModalNemotecnico.SelectedIndex = 0
        Else
            ddlModalNemotecnico.DataSource = listafondoSeries
            ddlModalNemotecnico.DataMember = "Nemotecnico"
            ddlModalNemotecnico.DataValueField = "Nemotecnico"
            ddlModalNemotecnico.DataBind()
        End If
    End Sub
#End Region

#Region "CARGA RUT FONDO Y NOMBRE FONDO CUANDO CAMBIA COMBO NEMOTECNICO"
    Public Sub CargarNombreFondoRutFondoPorModalNemotecnico()
        txtModalFechaNAV.Text = ""
        txtModalFechaPago.Text = ""

        'CARGA RUT FONDO Y NOMBRE FONDO  POR NEMOTECNICO
        CargarRutFondoPorNemotecnicoModal()
        CargarNombreFondoPorNemotecnicoModal()

        'CARGA NOMBRE Y MONEDA DE LA SERIE
        CargarNombreMonedaSerieModal()
    End Sub

    Public Sub CargarRutFondoPorNemotecnicoModal()
        Dim NegocioFondoSerie As FondoSeriesNegocio = New FondoSeriesNegocio
        Dim fondoSeries As FondoSerieDTO = New FondoSerieDTO()
        Dim fondo As FondoDTO = New FondoDTO()

        If ddlModalRutFondo.SelectedValue IsNot Nothing Then
            fondoSeries.Nemotecnico = ddlModalNemotecnico.SelectedValue
            'fondoSeries.Rut = ddlModalRutFondo.SelectedValue
        Else
            fondoSeries.Nemotecnico = ddlModalNemotecnico.SelectedValue
            fondoSeries.Rut = Nothing
        End If

        Dim listafondoSeries As List(Of FondoSerieDTO) = NegocioFondoSerie.GetListaFondoSerieConFiltro(fondoSeries, fondo)

        If listafondoSeries.Count = 0 Then
            ddlModalRutFondo.Items.Insert(0, New ListItem("", ""))
        Else
            ddlModalRutFondo.DataSource = listafondoSeries
            ddlModalRutFondo.DataMember = "rut"
            ddlModalRutFondo.DataValueField = "rut"
            ddlModalRutFondo.DataBind()
        End If
    End Sub

    Public Sub CargarNombreFondoPorNemotecnicoModal()
        Dim NegocioFondo As FondosNegocio = New FondosNegocio
        Dim fondoSeries As FondoSerieDTO = New FondoSerieDTO()
        Dim fondo As FondoDTO = New FondoDTO()

        fondoSeries.Nemotecnico = ddlModalNemotecnico.SelectedValue
        Dim listafondoSeries As List(Of FondoDTO) = NegocioFondo.GetNombrePorNemotecnico(fondoSeries)

        If listafondoSeries.Count = 0 Then
            ddlModalNombreFondo.Items.Insert(0, New ListItem("", ""))
        Else
            ddlModalNombreFondo.DataSource = listafondoSeries
            ddlModalNombreFondo.DataMember = "RazonSocial"
            ddlModalNombreFondo.DataValueField = "RazonSocial"
            ddlModalNombreFondo.DataBind()
        End If
    End Sub

#End Region

#Region "CARGA VALOR TIPO CAMBIO CUANDO CAMBIA TIPO MONEDA PAGO"
    Public Sub CargarValorTipoCambioPorMonedaPago()
        'TRAER VALOR TIPO CAMBIO OBSERVADO
        TraerYFijarTCObservado()
        CalcularMontos()

    End Sub
#End Region

#Region "CARGA NOMBRE APORTANTE Y MULTIFONDO  CUANDO CAMBIA COMBO RUT APORTANTE"
    Public Sub CargarNombreAportanteNemotecnicoPorRutAportanteModal()
        'CARGA NOMBRE APORTANTE Y MULTIFONDO POR RUT APORTANTE
        CargarNombreAportantePorRutAportanteModal()
        CargarMultifondoPorRutAportanteModal()
        CargarInfoCuotasDisponibles()

        'VALIDA CUOTAS CONTRA CUOTAS DIPONIBLES
        If (Double.Parse(txtModalCuota.Text)) > (Double.Parse(txtModalDisponiblesCuotasDisponibles.Text)) Then
            ShowAlert("El número de cuotas debe ser menor o igual a las Cuotas Disponibles")
            txtModalCuota.Text = "0"
        End If


        'CARGA LA FECHA Y CUOTA  DCV
        EstablecerDatosDCV()

        'PATRIMONIO UTILIZADO (RESCATES DEL DIA)
        Dim RescateHoy As RescatesDTO = New RescatesDTO()
        Dim NegocioRescateHoy As RescateNegocio = New RescateNegocio
        Dim RescateActualizadoHoy As RescatesDTO = New RescatesDTO()

        If txtModalFechaSolicitud.Text <> "" Then
            RescateHoy.RES_Fecha_Solicitud = txtModalFechaSolicitud.Text
        Else
            RescateHoy.RES_Fecha_Solicitud = Nothing
        End If

        RescateHoy.FS_Nemotecnico = ddlModalNemotecnico.SelectedValue
        RescateHoy.FN_RUT = ddlModalRutFondo.SelectedValue
        RescateHoy.AP_RUT = ddlModalRutAportante.SelectedValue

        RescateActualizadoHoy = NegocioRescateHoy.SelectRescatesHoy(RescateHoy)

        If RescateActualizadoHoy IsNot Nothing Then
            txtModalUtilizado.Text = Utiles.SetToCapitalizedNumber(RescateActualizadoHoy.RES_Monto)
        Else
            txtModalRescates.Text = "0"
        End If
        txtModalDisponibles.Text = Utiles.SetToCapitalizedNumber(Double.Parse(txtModalRescateMax.Text) - Double.Parse(txtModalUtilizado.Text))
    End Sub

    Private Sub CargarNombreAportantePorRutAportanteModal()
        Dim fechahasta As Nullable(Of Date)
        Dim NegocioAportante As AportanteNegocio = New AportanteNegocio
        Dim aportante As AportanteDTO = New AportanteDTO()

        aportante.Rut = ddlModalRutAportante.SelectedValue

        Dim aportantes As List(Of AportanteDTO) = NegocioAportante.GetListaAportantes(aportante, fechahasta)

        If aportantes.Count = 0 Then
            ddlModalNombreAportante.Items.Insert(0, New ListItem("", ""))
        Else
            ddlModalNombreAportante.DataSource = aportantes
            ddlModalNombreAportante.DataMember = "razonSocial"
            ddlModalNombreAportante.DataValueField = "razonSocial"
            ddlModalNombreAportante.DataBind()
        End If
    End Sub

    Private Sub CargarMultifondoPorRutAportanteModal()
        Dim fechahasta As Nullable(Of Date)
        Dim NegocioAportante As AportanteNegocio = New AportanteNegocio
        Dim aportante As AportanteDTO = New AportanteDTO()

        aportante.Rut = ddlModalRutAportante.SelectedValue

        Dim aportantes As List(Of AportanteDTO) = NegocioAportante.GetListaAportantes(aportante, fechahasta)

        If aportantes.Count = 0 Then
            ddlModalMultifondo.Items.Insert(0, New ListItem("", ""))
            ddlModalMultifondo.SelectedIndex = 0
            ddlModalMultifondo.Enabled = False
        Else
            ddlModalMultifondo.Enabled = True
            ddlModalMultifondo.DataSource = aportantes
            ddlModalMultifondo.DataMember = "multifondo"
            ddlModalMultifondo.DataValueField = "multifondo"
            ddlModalMultifondo.DataBind()

            If ddlModalMultifondo.Text = "" Then
                ddlModalMultifondo.Enabled = False
            End If
        End If
    End Sub

    Private Sub CargarInfoCuotasDisponibles()

        'CARGA CUOTAS DCV
        Dim ADCV1 As ADCVDTO = New ADCVDTO()
        Dim NegocioADCV1 As ADCVNegocio = New ADCVNegocio
        Dim ADCVActualizado1 As ADCVDTO

        ADCV1.AP_RUT = ddlModalRutAportante.SelectedValue
        ADCV1.FS_Nemotecnico = ddlModalNemotecnico.SelectedValue

        If ADCV1.AP_RUT <> "" AndAlso ADCV1.FS_Nemotecnico.Trim() <> "" Then
            ADCVActualizado1 = NegocioADCV1.SelectCuotasDCV(ADCV1)
            txtModalCuotasDVC.Text = Utiles.SetToCapitalizedNumber(ADCVActualizado1.ADCV_Cantidad)
        Else
            txtModalCuotasDVC.Text = "0"
        End If

        'CARGA RESCATES EN TRANSITO
        Dim Rescate As RescatesDTO = New RescatesDTO()
        Dim Negocio As RescateNegocio = New RescateNegocio
        Dim RescateActualizado As RescatesDTO = New RescatesDTO()

        If txtModalFechaPago.Text <> "" Then
            Rescate.RES_Fecha_Solicitud = txtModalFechaSolicitud.Text
            Rescate.RES_Fecha_Pago = txtModalFechaPago.Text
        Else
            Rescate.RES_Fecha_Solicitud = Nothing
            Rescate.RES_Fecha_Pago = Nothing
        End If
        Rescate.RES_Fecha_Solicitud = txtModalFechaSolicitud.Text
        Rescate.FS_Nemotecnico = ddlModalNemotecnico.SelectedValue
        Rescate.FN_RUT = ddlModalRutFondo.SelectedValue
        Rescate.AP_RUT = ddlModalRutAportante.SelectedValue
        Rescate.AP_Multifondo = ddlModalMultifondo.SelectedValue

        RescateActualizado = Negocio.SelectRescatesTransito(Rescate)

        If RescateActualizado IsNot Nothing Then
            txtModalRescates.Text = Utiles.SetToCapitalizedNumber(RescateActualizado.RES_Cuotas)
        Else
            txtModalRescates.Text = "0"
        End If

        'CARGA SUSCRIPCIONES EN TRANSITO
        Dim suscripcion As SuscripcionDTO = New SuscripcionDTO()
        Dim NegocioSuscripcion As SuscripcionNegocio = New SuscripcionNegocio

        If txtModalFechaSolicitud.Text = "" Then
            suscripcion.FechaSuscripcion = Nothing
        Else
            suscripcion.FechaSuscripcion = txtModalFechaSolicitud.Text
        End If

        suscripcion.Nemotecnico = ddlModalNemotecnico.SelectedValue
        suscripcion.RutAportante = ddlModalRutAportante.SelectedValue
        suscripcion.Multifondo = ddlModalMultifondo.SelectedValue

        Dim suscripcionActualizado As SuscripcionDTO = NegocioSuscripcion.GetSuscripcionTransito(suscripcion)

        If suscripcionActualizado IsNot Nothing Then
            txtModalSuscripciones.Text = Utiles.SetToCapitalizedNumber(suscripcionActualizado.CuotasASuscribir)
        Else
            txtModalSuscripciones.Text = "0"
        End If

        'CARGA DE CANJES EN TRANSITO
        Dim canje As CanjeDTO = New CanjeDTO
        Dim negocioCanje As CanjeNegocio = New CanjeNegocio
        canje.RutAportante = ddlModalRutAportante.SelectedValue
        canje.NemotecnicoSaliente = ddlModalNemotecnico.SelectedValue
        canje.FechaSolicitud = txtModalFechaSolicitud.Text
        canje.Multifondo = ddlModalMultifondo.SelectedValue

        Dim RetornoCanje As CanjeDTO = negocioCanje.CanjesTransito(canje)

        If RetornoCanje IsNot Nothing Then
            txtModalCanje.Text = Utiles.SetToCapitalizedNumber(RetornoCanje.CanjeTransito)
        Else
            txtModalCanje.Text = "0"
        End If
        'CARGA TOTAL DISPONIBLES
        txtModalDisponiblesCuotasDisponibles.Text = Utiles.SetToCapitalizedNumber(txtModalCuotasDVC.Text - txtModalRescates.Text + txtModalSuscripciones.Text + txtModalCanje.Text)
    End Sub

#End Region

#Region "CARGA RUT APORTANTE Y MULTIFONDO  CUANDO CAMBIA COMBO  NOMBRE APORTANTE"
    Public Sub CargarRutAportanteNemotecnicoPorNombreAportanteModal()
        'CARGA NOMBRE APORTANTE Y MULTIFONDO POR RUT APORTANTE
        CargarRutAportantePorNombreAportanteModal()
        CargarMultifondoPorNombreAportanteModal()
        CargarInfoCuotasDisponibles()

        'VALIDA CUOTAS CONTRA CUOTAS DIPONIBLES
        If (Double.Parse(txtModalCuota.Text)) > (Double.Parse(txtModalDisponiblesCuotasDisponibles.Text)) Then
            ShowAlert("El número de cuotas debe ser menor o igual a las Cuotas Disponibles")
            txtModalCuota.Text = "0"
        End If

        'CARGA LA FECHA Y CUOTA  DCV
        EstablecerDatosDCV()

        'PATRIMONIO UTILIZADO (RESCATES DEL DIA)
        Dim RescateHoy As RescatesDTO = New RescatesDTO()
        Dim NegocioRescateHoy As RescateNegocio = New RescateNegocio
        Dim RescateActualizadoHoy As RescatesDTO = New RescatesDTO()

        If txtModalFechaSolicitud.Text <> "" Then
            RescateHoy.RES_Fecha_Solicitud = txtModalFechaSolicitud.Text
        Else
            RescateHoy.RES_Fecha_Solicitud = Nothing
        End If

        RescateHoy.FS_Nemotecnico = ddlModalNemotecnico.SelectedValue
        RescateHoy.FN_RUT = ddlModalRutFondo.SelectedValue
        RescateHoy.AP_RUT = ddlModalRutAportante.SelectedValue

        RescateActualizadoHoy = NegocioRescateHoy.SelectRescatesHoy(RescateHoy)

        If RescateActualizadoHoy IsNot Nothing Then
            txtModalUtilizado.Text = Utiles.SetToCapitalizedNumber(RescateActualizadoHoy.RES_Monto)
        Else
            txtModalRescates.Text = "0"
        End If
        txtModalDisponibles.Text = Utiles.SetToCapitalizedNumber(Double.Parse(txtModalRescateMax.Text) - Double.Parse(txtModalUtilizado.Text))
    End Sub

    Private Sub CargarRutAportantePorNombreAportanteModal()
        Dim fechahasta As Nullable(Of Date)
        Dim NegocioAportante As AportanteNegocio = New AportanteNegocio
        Dim aportante As AportanteDTO = New AportanteDTO()

        aportante.RazonSocial = ddlModalNombreAportante.SelectedValue

        Dim aportantes As List(Of AportanteDTO) = NegocioAportante.GetListaAportantes(aportante, fechahasta)

        If aportantes.Count = 0 Then
            ddlModalRutAportante.Items.Insert(0, New ListItem("", ""))
        Else
            ddlModalRutAportante.DataSource = aportantes
            ddlModalRutAportante.DataMember = "rut"
            ddlModalRutAportante.DataValueField = "rut"
            ddlModalRutAportante.DataBind()
        End If
    End Sub

    Private Sub CargarMultifondoPorNombreAportanteModal()
        'Dim fechahasta As Nullable(Of Date)
        Dim NegocioAportante As AportanteNegocio = New AportanteNegocio
        Dim aportante As AportanteDTO = New AportanteDTO()

        aportante.RazonSocial = ddlModalNombreAportante.SelectedValue
        Dim aportantes As List(Of AportanteDTO) = NegocioAportante.AportantePorNombre(aportante)


        If aportantes.Count = 0 Then
            ddlModalMultifondo.Items.Insert(0, New ListItem("", ""))
            ddlModalMultifondo.Enabled = False
        Else
            ddlModalMultifondo.Enabled = True
            ddlModalMultifondo.DataSource = aportantes
            ddlModalMultifondo.DataMember = "multifondo"
            ddlModalMultifondo.DataValueField = "multifondo"
            ddlModalMultifondo.DataBind()

            If ddlModalMultifondo.Text = "" Then
                ddlModalMultifondo.Enabled = False
            End If
        End If
    End Sub

#End Region

#Region "CARGA RUT APORTANTE Y NOMBRE APORTANTE CUANDO CAMBIA COMBO MULTIFONDO"
    Public Sub CargarRutAportanteNombreAportantePorModalMultifondo()
        'CARGA NOMBRE APORTANTE Y MULTIFONDO POR RUT APORTANTE
        CargarRutAportantePorMultifondoModal()
        CargarNombreAportantePorMultifondoModal()
        CargarInfoCuotasDisponibles()

        'CARGA LA FECHA Y CUOTA  DCV
        Dim ADCV As ADCVDTO = New ADCVDTO()
        Dim NegocioADCV As ADCVNegocio = New ADCVNegocio
        Dim ADCVActualizado As ADCVDTO

        ADCV.AP_RUT = ddlModalRutAportante.SelectedValue
        ADCV.ADCV_Razon_Social = ddlModalNombreAportante.SelectedValue
        ADCV.FS_Nemotecnico = ddlModalNemotecnico.SelectedValue
        ADCV.ADCV_Fecha = txtModalFechaSolicitud.Text

        If ADCV.AP_RUT <> "" AndAlso ADCV.ADCV_Razon_Social <> "" AndAlso ADCV.FS_Nemotecnico.Trim() <> "" Then

            ADCVActualizado = NegocioADCV.GetADCV(ADCV)

            If ADCVActualizado Is Nothing Then
                Dim ADCVUltimo As List(Of ADCVDTO) = NegocioADCV.UltimoDCV(ADCV)

                If ADCVUltimo.Count = 0 Then
                    txtModalCuotasDVC.Text = "0"
                    txtModalFechaDCV.Text = CDate(Date.Now.AddDays(-1).ToString("dd/MM/yyyy"))
                Else
                    For Each ADCVUltimo1 As ADCVDTO In ADCVUltimo
                        txtModalCuotasDVC.Text = Utiles.SetToCapitalizedNumber(ADCVUltimo1.ADCV_Cantidad)
                        txtModalFechaDCV.Text = CDate(ADCVUltimo1.ADCV_Fecha.ToString("dd/MM/yyyy"))
                    Next
                End If
            Else
                txtModalFechaDCV.Text = CDate(ADCVActualizado.ADCV_Fecha.ToString("dd/MM/yyyy"))
                txtModalCuotasDVC.Text = Utiles.SetToCapitalizedNumber(ADCVActualizado.ADCV_Cantidad)
            End If
        Else
            txtModalCuotasDVC.Text = "0"
            txtModalFechaDCV.Text = ""
        End If

        'PATRIMONIO UTILIZADO (RESCATES DEL DIA)
        Dim RescateHoy As RescatesDTO = New RescatesDTO()
        Dim NegocioRescateHoy As RescateNegocio = New RescateNegocio
        Dim RescateActualizadoHoy As RescatesDTO = New RescatesDTO()

        If txtModalFechaSolicitud.Text <> "" Then
            RescateHoy.RES_Fecha_Solicitud = txtModalFechaSolicitud.Text
        Else
            RescateHoy.RES_Fecha_Solicitud = Nothing
        End If

        RescateHoy.FS_Nemotecnico = ddlModalNemotecnico.SelectedValue
        RescateHoy.FN_RUT = ddlModalRutFondo.SelectedValue
        RescateHoy.AP_RUT = ddlModalRutAportante.SelectedValue

        RescateActualizadoHoy = NegocioRescateHoy.SelectRescatesHoy(RescateHoy)

        If RescateActualizadoHoy IsNot Nothing Then
            txtModalUtilizado.Text = Utiles.SetToCapitalizedNumber(RescateActualizadoHoy.RES_Monto)
        Else
            txtModalRescates.Text = "0"
        End If
        txtModalDisponibles.Text = Utiles.SetToCapitalizedNumber(Double.Parse(txtModalRescateMax.Text) - Double.Parse(txtModalUtilizado.Text))
    End Sub

    Private Sub CargarRutAportantePorMultifondoModal()
        Dim NegocioAportante As AportanteNegocio = New AportanteNegocio
        Dim aportante As AportanteDTO = New AportanteDTO()

        aportante.Multifondo = ddlModalMultifondo.SelectedValue

        Dim aportantes As List(Of AportanteDTO) = NegocioAportante.AportantePorMultifondo(aportante)

        If aportantes.Count = 0 Then
            ddlModalRutAportante.Items.Insert(0, New ListItem("", ""))
        Else
            ddlModalRutAportante.DataSource = aportantes
            ddlModalRutAportante.DataMember = "rut"
            ddlModalRutAportante.DataValueField = "rut"
            ddlModalRutAportante.DataBind()
        End If
    End Sub

    Private Sub CargarNombreAportantePorMultifondoModal()
        Dim NegocioAportante As AportanteNegocio = New AportanteNegocio
        Dim aportante As AportanteDTO = New AportanteDTO()

        aportante.Multifondo = ddlModalMultifondo.SelectedValue

        Dim aportantes As List(Of AportanteDTO) = NegocioAportante.AportantePorMultifondo(aportante)

        If aportantes.Count = 0 Then
            ddlModalNombreAportante.Items.Insert(0, New ListItem("", ""))
        Else
            ddlModalNombreAportante.DataSource = aportantes
            ddlModalNombreAportante.DataMember = "RazonSocial"
            ddlModalNombreAportante.DataValueField = "RazonSocial"
            ddlModalNombreAportante.DataBind()
        End If
    End Sub

#End Region

#Region "CARGA MONTO  TEXTCHANGED CAMPO CUOTAS"
    Public Sub CargarMontoModal()

        If txtModalCuota.ToolTip = "" Then
            txtModalCuota.ToolTip = "0"
        End If
        If txtModalCuota.Text = "" Then
            txtModalCuota.Text = "0"
        End If

        If ((Double.Parse(txtModalCuota.Text)) > (Double.Parse(txtModalDisponiblesCuotasDisponibles.Text) + Double.Parse(txtModalCuota.ToolTip)) And ((ddlModalNombreAportante.ToolTip = ddlModalNombreAportante.SelectedValue))) Then
            ShowAlert("El número de cuotas debe ser menor o igual a las Cuotas Disponibles")
            txtModalCuota.Text = "0"
            txtModalMonto.Text = "0"
            txtModalMontoCLP.Text = "0"
            Return
        End If

        If ((Double.Parse(txtModalCuota.Text)) > (Double.Parse(txtModalDisponiblesCuotasDisponibles.Text)) And ((ddlModalNombreAportante.ToolTip <> ddlModalNombreAportante.SelectedValue))) Then
            ShowAlert("El número de cuotas debe ser menor o igual a las Cuotas Disponibles")
            txtModalCuota.Text = "0"
            txtModalMonto.Text = "0"
            txtModalMontoCLP.Text = "0"
            Return
        End If

        Dim TipoCalculoNav As TipoCalculoNavDTO = New TipoCalculoNavDTO()
        Dim NegocioTipoCalculoNav As TipoCalculoNav = New TipoCalculoNav

        CalcularMontos()

        If (txtModalFechaSolicitud.Text >= CDate(Date.Today.ToString("dd/MM/yyyy"))) Then

            If (Double.Parse(txtModalMonto.Text)) > (Double.Parse(txtModalDisponibles.Text)) Then
                ShowAlert("El Valor del Monto debe ser menor o igual al Patrimonio Disponible")
                txtModalMonto.Text = "0"
                txtModalMontoCLP.Text = "0"
                txtModalCuota.Text = "0"
                Return
            End If

        End If

        If txtIDRescate.Text() <> Nothing Then
            TipoCalculoNav.ID = txtIDRescate.Text()
            TipoCalculoNav.TipoTransaccion = "Rescate"
            TipoCalculoNav.TipoCalculo = "C"
        End If


        Dim updateCNJ_Tipo_CalculoNAV = NegocioTipoCalculoNav.UpdateTipoCalculoNav(TipoCalculoNav)

    End Sub
#End Region

#Region "CARGA CUOTAS TEXTCHANGED CAMPO MONTO"
    Public Sub CargarCuotasModal()
        Dim NegocioTipoCalculoNav As TipoCalculoNav = New TipoCalculoNav
        Dim ModalCuota As Decimal
        Dim TipoCalculoNav As TipoCalculoNavDTO = New TipoCalculoNavDTO()

        If txtModalMonto.Text = "" Then
            txtModalMonto.Text = "0"
        Else
            If txtModalNAV.Text <> "" And txtModalNAV.Text <> "0" And txtModalMonto.Text <> "0" Then

                ModalCuota = txtModalMonto.Text / txtModalNAV.Text
                txtModalCuota.Text = Utiles.SetToCapitalizedNumber(Math.Floor(ModalCuota))

                If (Double.Parse(txtModalCuota.Text)) > (Double.Parse(txtModalDisponiblesCuotasDisponibles.Text)) Then
                    ShowAlert("El número de cuotas debe ser menor o igual a las Cuotas Disponibles")
                    txtModalCuota.Text = "0"
                    Return
                End If

                CalcularMontos()

                If (Double.Parse(txtModalMonto.Text)) > (Double.Parse(txtModalDisponibles.Text)) Then
                    ShowAlert("El Valor del Monto debe ser menor o igual al Patrimonio Disponible")
                    txtModalMonto.Text = "0"
                    txtModalMontoCLP.Text = "0"
                    Return
                End If


                If txtIDRescate.Text() <> Nothing Then
                    TipoCalculoNav.ID = txtIDRescate.Text()
                    TipoCalculoNav.TipoTransaccion = "Rescate"
                    TipoCalculoNav.TipoCalculo = "M"

                    Dim updateCNJ_Tipo_CalculoNAV = NegocioTipoCalculoNav.UpdateTipoCalculoNav(TipoCalculoNav)
                Else
                    TipoCalculoNav.TipoTransaccion = "Rescate"
                    TipoCalculoNav.TipoCalculo = "M"

                    Dim updateCNJ_Tipo_CalculoNAV = NegocioTipoCalculoNav.UpdateTipoCalculoNav(TipoCalculoNav)
                End If
            End If
        End If
    End Sub
#End Region

#Region "CARGA CUOTAS TEXTCHANGED CAMPO MONTO CLP"
    Public Sub CargarCuotasCLPModal()
        If txtModalNAV_CLP.Text <> "" And txtModalNAV_CLP.Text <> "0" And txtModalMontoCLP.Text <> "0" Then
            txtModalCuota.Text = Utiles.SetToCapitalizedNumber(txtModalMontoCLP.Text / txtModalNAV_CLP.Text)
        End If

    End Sub
#End Region

#Region "RECALCULA CUANDO CAMBIA FECHA SOLICITUD"
    Public Sub RecalculoCambiaFechaSolicitud()
        Session("CambioFechaSolicitud") = "Hecho"
        If txtModalFechaSolicitud.Text = "" Then
            txtModalFechaSolicitud.Text = "1900-01-01"
            CargarNombreMonedaSerieModal()
        Else
            CargarNombreMonedaSerieModal()
        End If

    End Sub
#End Region


#Region "BOTON GUARDAR"
    Private Sub btnModalGuardar_Click(sender As Object, e As EventArgs) Handles btnModalGuardar.Click
        'CREAR

        Dim FechaDCV As Date
        Dim FechaSolicitud As Date
        Dim CuotasAportante As Decimal
        Dim CuotasDisponibles As Decimal

        If txtModalFechaDCV.Text = "" Then
            FechaDCV = Nothing
        Else
            FechaDCV = txtModalFechaDCV.Text
        End If

        If txtModalFechaSolicitud.Text = "" Then
            FechaSolicitud = Nothing
        Else
            FechaSolicitud = txtModalFechaSolicitud.Text
        End If


        CuotasAportante = txtModalCuota.Text
        CuotasDisponibles = txtModalDisponiblesCuotasDisponibles.Text

        'VALIDACIONES DE CAMPOS REQUERIDOS
        If ddlModalRutAportante.Text = "" Then
            ShowAlert("Debe seleccionar datos de Aportantes")
            Return
        End If
        If ddlModalNombreAportante.Text = "" Then
            ShowAlert("Debe seleccionar datos de Aportantes")
            Return
        End If
        If txtModalNombreSerie.Text = "" Then
            ShowAlert("El campo Nombre Serie No es Válido")
            Return
        End If
        If txtModalCuota.Text < 1 Then
            ShowAlert("El campo Cuota debe ser mayor a cero(0)")
            Return
        End If
        If ddlModalRutFondo.Text = "" Then
            ShowAlert("Debe seleccionar datos del Fondo")
            Return
        End If
        If ddlModalNombreFondo.Text = "" Then
            ShowAlert("Debe seleccionar datos del Fondo")
            Return
        End If

        If ddlModalMonedaPago.Text = "" Then
            ShowAlert("El campo Moneda Pago No puede estar vacío")
            Return
        End If
        If ddlModalNemotecnico.Text = "" Then
            ShowAlert("Debe seleccionar datos de Nemotécnico")
            Return
        End If
        If txtModalMonedaSerie.Text = "" Then
            ShowAlert("El campo Moneda Serie No puede estar vacío")
            Return
        End If
        If txtModalNAV.Text < 1 Then
            ShowAlert("El campo NAV debe ser mayor a cero(0)")
            Return
        End If
        If txtModalNAV_CLP.Text < 1 Then
            ShowAlert("El campo NAV(CLP) debe ser mayor a cero(0)")
            Return
        End If

        If txtModalFechaSolicitud.Text = "" Then
            ShowAlert("El campo Fecha Solicitud No puede estar vacío")
            Return
        End If

        If txtModalFechaNAV.Text = "" Then
            ShowAlert("El campo Fecha NAV No puede estar vacío")
            Return
        End If

        If txtModalFechaPago.Text = "" Then
            ShowAlert("El campo Fecha Pago No puede estar vacío")
            Return
        End If

        If txtModalFechaTCObs.Text = "" Then
            ShowAlert("El campo Fecha Tipo Cambio No puede estar vacío")
            Return
        End If

        'VALIDACIONES ANTES DE GUARDAR
        If txtModalMonto.Text = "0" Then
            ShowAlert("El Monto debe ser mayor a cero(0)")
            Return
        End If

        If txtModalMontoCLP.Text = "0" Then
            ShowAlert("El Monto CLP debe ser mayor a cero(0)")
            Return
        End If


        If CuotasDisponibles < CuotasAportante Then
            ShowAlert("NO es posible rescatar cuotas mayores al saldo disponible")
            Return
        End If

        Dim negocioIns As RescateNegocio = New RescateNegocio
        Dim Rescate As RescatesDTO = GetRescateModal()

        Rescate = negocioIns.InsertRescates(Rescate)
        txtIDRescate.Text = Rescate.RES_ID

        txtAccionHidden.Value = "MOSTRAR_DIALOGO"

        If txtIDRescate.Text > 0 Then
            'Ingresado con Exito
            CargaNombreAportanteBuscar()
            CargaNombreFondoBuscar()
            CargaNemotecnicoBuscar()
            ' ShowMesagges(CONST_TITULO_VALORESCUOTA, CONST_INSERTAR_EXITO, Constantes.CONST_RUTA_IMG + Constantes.CONST_IMG_LOGO, Constantes.CONST_RUTA_IMG + Constantes.CONST_IMG_CORRECTO)

            'If FechaDCV < FechaSolicitud Then
            '    ShowAlert("ADVERTENCIA: La información DCV es menor a la Fecha de Solicitud.")
            'End If
            ShowAlert(CONST_INSERTAR_EXITO)
            GenerarPopUp()
        Else
            'Error en la BBDD
            'ShowMesagges(CONST_TITULO_VALORESCUOTA, CONST_INSERTAR_ERROR, Constantes.CONST_RUTA_IMG + Constantes.CONST_IMG_LOGO, Constantes.CONST_RUTA_IMG + Constantes.CONST_IMG_ERROR)
            ShowAlert(CONST_INSERTAR_ERROR)
        End If

        'txtAccionHidden.Value = ""
        DataInitial()
        Me.GrvTabla.DataSource = Nothing
        GrvTabla.DataBind()

    End Sub

    Private Function GetRescateModal() As RescatesDTO
        Dim Rescate As RescatesDTO = New RescatesDTO()

        Rescate.RES_Fecha_Solicitud = txtModalFechaSolicitud.Text
        Rescate.RES_Fecha_Pago = txtModalFechaPago.Text
        Rescate.AP_RUT = ddlModalRutAportante.SelectedValue
        Rescate.AP_Multifondo = ddlModalMultifondo.SelectedValue
        Rescate.FS_Nemotecnico = ddlModalNemotecnico.SelectedValue
        Rescate.RES_Cuotas = txtModalCuota.Text
        Rescate.Res_Fecha_Carga = Date.Now
        Rescate.FN_RUT = ddlModalRutFondo.SelectedValue
        Rescate.FN_Nombre_Corto = ddlModalNombreFondo.SelectedValue
        Rescate.RES_Tipo_Transaccion = "Rescate"
        Rescate.AP_Razon_Social = ddlModalNombreAportante.SelectedValue
        Rescate.FS_Nombre_Corto = txtModalNombreSerie.Text
        If ddlModalMonedaPago.Enabled = False Then
            Rescate.RES_Moneda_Pago = "CLP"
        Else
            Rescate.RES_Moneda_Pago = ddlModalMonedaPago.SelectedValue
        End If
        Rescate.ADCV_Cantidad = txtModalCuotasDVC.Text
        Rescate.RES_Fecha_Nav = txtModalFechaNAV.Text
        Rescate.RES_FechaTCObs = IIf(txtModalFechaTCObs.Text = "", Nothing, txtModalFechaTCObs.Text)
        Rescate.RES_Nav = txtModalNAV.Text
        Rescate.RES_Monto = txtModalMonto.Text
        Rescate.RES_Nav_CLP = txtModalNAV_CLP.Text
        Rescate.RES_Monto_CLP = txtModalMontoCLP.Text
        Rescate.TC_Valor = txtModalTCObservado.Text
        Rescate.RES_Contrato = ddlModalContrato.SelectedValue
        Rescate.RES_Poderes = ddlModalPoderes.SelectedValue
        Rescate.RES_Estado = ddlModalEstado.SelectedValue
        Rescate.RES_Observaciones = txtModalObservaciones.Text
        Rescate.RES_Patrimonio = txtModalPatrimonio.Text
        Rescate.FS_Patrimonio = txtModalPorcentaje.Text
        Rescate.RES_Disponible_Patrimonio = txtModalDisponibles.Text
        Rescate.ADCV_Fecha = txtModalFechaDCV.Text
        Rescate.SC_Cuotas_a_Suscribir = txtModalSuscripciones.Text
        Rescate.CN_Cuotas_Disponibles = txtModalCanje.Text
        Rescate.RES_Cuotas_Disponibles = txtModalDisponiblesCuotasDisponibles.Text
        Rescate.RES_Transito = txtModalRescates.Text
        Rescate.RES_Fijacion_NAV = txtModalFijacionNAV.Text
        Rescate.RES_Fijacion_TCObservado = txtModalFijacionTCObs.Text
        Rescate.RES_Fecha_Ingreso = Date.Now
        Rescate.RES_Usuario_Ingreso = Session("NombreUsuario")
        Rescate.RES_Fecha_Modificacion = Nothing
        Rescate.RES_Usuario_Modificacion = Nothing
        Rescate.FS_Moneda = txtModalMonedaSerie.Text
        Rescate.RES_Estado_Rescate = 1
        Rescate.RES_Maximo = txtModalRescateMax.Text
        Rescate.RES_Utilizado = txtModalUtilizado.Text

        Return Rescate
    End Function
#End Region

#Region "AGREGA A LA MODAL RESCATE A MODIFICAR"
    Protected Sub BtnModificar_Click(sender As Object, e As EventArgs)
        ddlModalPoderes.SelectedIndex = 0
        ddlModalContrato.SelectedIndex = 0

        Session("TipodeCalculoTC") = Nothing
        Dim negocio As RescateNegocio = New RescateNegocio
        Dim RescateSelect As RescatesDTO = GetRescateSelect()
        Dim RescateActualizado As RescatesDTO = negocio.GetRescateOne(RescateSelect)
        Dim Relacion As RescatesDTO = negocio.GetRelaciones(RescateSelect)

        If (Relacion.CountAP > 0) Then
            ShowAlert("No se puede modificar el rescate, información del aportante se modificó")
            txtAccionHidden.Value = ""
        ElseIf (Relacion.CountFN > 0) Then
            ShowAlert("No se puede modificar este rescate, información del fondo se modificó")
            txtAccionHidden.Value = ""
        ElseIf (Relacion.CountFS > 0) Then
            ShowAlert("No se puede modificar este rescate, información de la serie se modificó")
            txtAccionHidden.Value = ""
        Else
            FormateoFormDatosModificar(RescateActualizado)
            FormateoEstiloFormModificar()

            txtAccionHidden.Value = "MODIFICAR"
            Session("CRUD") = "MODIFICAR"
        End If
    End Sub

    Private Function GetRescateSelect() As RescatesDTO
        Dim Rescate As New RescatesDTO

        For Each row As GridViewRow In GrvTabla.Rows
            Dim chk As RadioButton = row.Cells(0).Controls(1)
            If chk IsNot Nothing And chk.Checked Then
                Rescate.RES_ID = row.Cells(1).Text.Trim()
            End If
        Next

        Return Rescate
    End Function

    Private Sub FormateoFormDatosModificar(Rescate As RescatesDTO)
        Dim negocio As CertificadoNegocio = New CertificadoNegocio

        CargarRutFondoModal()
        CargaNombreFondoModal()
        CargarRutAportanteModal()
        CargarNombreAportanteModal()
        CargarMultifondoModal()
        CargaNemotecnicoModal()
        ddlModalMonedaPago.Items.Insert(0, New ListItem("", ""))

        txtIDRescate.Text = Rescate.RES_ID
        txtModalFechaSolicitud.Text = CDate(Rescate.RES_Fecha_Solicitud.ToString("dd/MM/yyyy"))
        txtModalFechaPago.Text = CDate(Rescate.RES_Fecha_Pago.ToString("dd/MM/yyyy"))
        ddlModalRutAportante.SelectedValue = Rescate.AP_RUT
        ddlModalMultifondo.SelectedValue = Rescate.AP_Multifondo
        ddlModalNemotecnico.SelectedValue = Rescate.FS_Nemotecnico
        txtModalCuota.Text = Utiles.SetToCapitalizedNumber(Rescate.RES_Cuotas)
        txtModalCuota.ToolTip = Rescate.RES_Cuotas
        ddlModalRutFondo.SelectedValue = Rescate.FN_RUT
        'ddlModalNombreFondo.SelectedValue = Rescate.FN_Nombre_Corto

        Dim fondo As FondoDTO = Utiles.GetFondo(Rescate.FN_RUT)
        ddlModalNombreFondo.SelectedValue = fondo.RazonSocial
        ddlModalNombreAportante.SelectedValue = Rescate.AP_Razon_Social
        ddlModalNombreAportante.ToolTip = Rescate.AP_Razon_Social
        txtModalNombreSerie.Text = Rescate.FS_Nombre_Corto
        ddlModalMonedaPago.SelectedValue = Rescate.RES_Moneda_Pago

        If Rescate.FS_Moneda <> "USD" Then
            ddlModalMonedaPago.Enabled = False
        Else
            ddlModalMonedaPago.Enabled = True
        End If

        'AJUSTE MODIFICAR PANEL CUOTAS DISPONIBLES 2020-01-07 NDP
        'txtModalCuotasDVC.Text = Rescate.ADCV_Cantidad
        'txtModalRescates.Text = Rescate.RES_Transito
        'txtModalSuscripciones.Text = Rescate.SC_Cuotas_a_Suscribir
        'txtModalCanje.Text = Rescate.CN_Cuotas_Disponibles
        'txtModalDisponiblesCuotasDisponibles.Text = Rescate.RES_Cuotas_Disponibles
        'CARGA LA FECHA Y CUOTA  DCV
        Dim ADCV As ADCVDTO = New ADCVDTO()
        Dim NegocioADCV As ADCVNegocio = New ADCVNegocio
        Dim ADCVActualizado As ADCVDTO

        ADCV.AP_RUT = ddlModalRutAportante.SelectedValue
        ADCV.ADCV_Razon_Social = ddlModalNombreAportante.SelectedValue
        ADCV.FS_Nemotecnico = ddlModalNemotecnico.SelectedValue
        ADCV.ADCV_Fecha = txtModalFechaSolicitud.Text

        If ADCV.AP_RUT <> "" AndAlso ADCV.ADCV_Razon_Social <> "" AndAlso ADCV.FS_Nemotecnico.Trim() <> "" Then
            ADCVActualizado = NegocioADCV.GetADCV(ADCV)

            If ADCVActualizado Is Nothing Then
                Dim ADCVUltimo As List(Of ADCVDTO) = NegocioADCV.UltimoDCV(ADCV)

                If ADCVUltimo.Count = 0 Then
                    txtModalCuotasDVC.Text = "0"
                Else
                    For Each ADCVUltimo1 As ADCVDTO In ADCVUltimo
                        txtModalCuotasDVC.Text = Utiles.SetToCapitalizedNumber(ADCVUltimo1.ADCV_Cantidad)
                    Next
                End If
            Else
                txtModalCuotasDVC.Text = Utiles.SetToCapitalizedNumber(ADCVActualizado.ADCV_Cantidad)
            End If
        Else
            txtModalCuotasDVC.Text = "0"
        End If

        'CARGA RESCATES EN TRANSITO
        'Dim Rescate As RescatesDTO = New RescatesDTO()
        Dim NegocioRescate As RescateNegocio = New RescateNegocio
        Dim RescateActualizado As RescatesDTO = New RescatesDTO()

        If txtModalFechaPago.Text <> "" Then
            Rescate.RES_Fecha_Solicitud = txtModalFechaSolicitud.Text
            Rescate.RES_Fecha_Pago = txtModalFechaPago.Text
        Else
            Rescate.RES_Fecha_Solicitud = Nothing
            Rescate.RES_Fecha_Pago = Nothing
        End If

        Rescate.FS_Nemotecnico = ddlModalNemotecnico.SelectedValue
        Rescate.FN_RUT = ddlModalRutFondo.SelectedValue
        Rescate.AP_RUT = ddlModalRutAportante.SelectedValue
        Rescate.AP_Multifondo = ddlModalMultifondo.SelectedValue

        RescateActualizado = NegocioRescate.SelectRescatesTransito(Rescate)

        If RescateActualizado IsNot Nothing Then
            txtModalRescates.Text = Utiles.SetToCapitalizedNumber(RescateActualizado.RES_Cuotas)
        Else
            txtModalRescates.Text = "0"
        End If

        'CARGA SUSCRIPCIONES EN TRANSITO
        Dim suscripcion As SuscripcionDTO = New SuscripcionDTO()
        Dim NegocioSuscripcion As SuscripcionNegocio = New SuscripcionNegocio

        suscripcion.FechaSuscripcion = txtModalFechaSolicitud.Text
        suscripcion.Nemotecnico = ddlModalNemotecnico.SelectedValue
        suscripcion.RutAportante = ddlModalRutAportante.SelectedValue
        suscripcion.Multifondo = ddlModalMultifondo.SelectedValue

        Dim suscripcionActualizado As SuscripcionDTO = NegocioSuscripcion.GetSuscripcionTransito(suscripcion)

        If suscripcionActualizado IsNot Nothing Then
            txtModalSuscripciones.Text = Utiles.SetToCapitalizedNumber(suscripcionActualizado.CuotasASuscribir)
        Else
            txtModalSuscripciones.Text = "0"
        End If

        'CARGA DE CANJES EN TRANSITO
        Dim canje As CanjeDTO = New CanjeDTO
        Dim negocioCanje As CanjeNegocio = New CanjeNegocio
        canje.RutAportante = ddlModalRutAportante.SelectedValue
        canje.NemotecnicoSaliente = ddlModalNemotecnico.SelectedValue
        canje.FechaSolicitud = txtModalFechaSolicitud.Text
        canje.Multifondo = Rescate.AP_Multifondo

        Dim RetornoCanje As CanjeDTO = negocioCanje.CanjesTransito(canje)

        If RetornoCanje IsNot Nothing Then
            txtModalCanje.Text = Utiles.SetToCapitalizedNumber(RetornoCanje.CanjeTransito)
        Else
            txtModalCanje.Text = "0"
        End If
        'CARGA TOTAL DISPONIBLES
        txtModalDisponiblesCuotasDisponibles.Text = Utiles.SetToCapitalizedNumber(txtModalCuotasDVC.Text - txtModalRescates.Text + txtModalSuscripciones.Text + txtModalCanje.Text)

        txtModalFechaNAV.Text = CDate(Rescate.RES_Fecha_Nav.ToString("dd/MM/yyyy"))
        txtModalFechaTCObs.Text = CDate(Rescate.RES_FechaTCObs.ToString("dd/MM/yyyy"))
        txtModalNAV.Text = Rescate.RES_NavFormat
        txtModalMonto.Text = Rescate.RES_MontoFormat   ' Utiles.SetToCapitalizedNumber(Rescate.RES_Monto)
        txtModalTCObservado.Text = Utiles.SetToCapitalizedNumber(Rescate.TC_Valor)
        ddlModalContrato.SelectedValue = Rescate.RES_Contrato
        ddlModalPoderes.SelectedValue = Rescate.RES_Poderes
        ddlModalEstado.SelectedValue = Rescate.RES_Estado
        txtModalObservaciones.Text = Rescate.RES_Observaciones
        txtModalPatrimonio.Text = Utiles.SetToCapitalizedNumber(Rescate.RES_Patrimonio)
        txtModalPorcentaje.Text = Rescate.FS_Patrimonio
        txtModalDisponibles.Text = Utiles.SetToCapitalizedNumber(Rescate.RES_Disponible_Patrimonio)
        txtModalFechaDCV.Text = CDate(Rescate.ADCV_Fecha.ToString("dd/MM/yyyy"))

        txtModalFijacionNAV.Text = Rescate.RES_Fijacion_NAV
        txtModalFijacionTCObs.Text = Rescate.RES_Fijacion_TCObservado
        txtModalMonedaSerie.Text = Rescate.FS_Moneda

        If Rescate.FS_Moneda = "CLP" Then
            txtModalTCObservado.Text = "1"

            txtModalNAV_CLP.Text = Utiles.formatearNAVCLP(Rescate.RES_Nav_CLP)  '  String.Format("{0:N4}", Math.Round(Rescate.RES_Nav_CLP, MidpointRounding.ToEven))
            CalcularMontos()

            'txtModalMontoCLP.Text = Utiles.calcularMontoCLP(txtModalCuota.Text, txtModalNAV_CLP.Text, 1) '   String.Format("{0:N0}", txtModalCuota.Text * txtModalNAV_CLP.Text)
        Else
            Dim navCLP As Double
            navCLP = Rescate.RES_Nav * txtModalTCObservado.Text

            txtModalNAV_CLP.Text = Utiles.formatearNAVCLP(navCLP) ' String.Format("{0:N4}", Math.Round(navCLP, MidpointRounding.ToEven))
            CalcularMontos()
        End If
        txtModalRescateMax.Text = Utiles.SetToCapitalizedNumber(Rescate.RES_Maximo)
        txtModalUtilizado.Text = Utiles.SetToCapitalizedNumber(Rescate.RES_Utilizado)

        Dim fechahasta As Nullable(Of Date)
        Dim NegocioAportante As AportanteNegocio = New AportanteNegocio
        Dim aportante As AportanteDTO = New AportanteDTO()

        aportante.Rut = ddlModalRutAportante.SelectedValue

        Dim aportantes As List(Of AportanteDTO) = NegocioAportante.GetListaAportantes(aportante, fechahasta)

        If ddlModalMultifondo.Text = "" Then
            ddlModalMultifondo.Enabled = False
        Else
            ddlModalMultifondo.Enabled = True
        End If


    End Sub

    Private Sub FormateoEstiloFormModificar()
        btnModalGuardar.Enabled = False
        btnModalGuardar.Visible = False


        btnModalModificar.Enabled = True
        btnModalModificar.Visible = True
        btnModalEliminar.Enabled = False
        btnModalEliminar.Visible = True

        ddlModalRutAportante.Enabled = True
        ddlModalNombreAportante.Enabled = True
        'ddlModalMultifondo.Enabled = True
        txtModalNombreSerie.Enabled = False
        txtModalCuota.Enabled = True
        'ddlModalMonedaPago.Enabled = True
        'If ddlModalMonedaPago.SelectedValue = "CLP" Then
        '    ddlModalMonedaPago.Enabled = False
        'Else
        '    ddlModalMonedaPago.Enabled = True
        'End If
        txtModalTCObservado.Enabled = True
        txtModalFechaSolicitud.Enabled = False
        txtModalFechaNAV.Enabled = False
        lnkBtnModalFechaNAV.Enabled = True
        lnkBtnModalFechaNAV.Visible = True
        lnkModalFechaSolicitud.Enabled = True
        lnkModalFechaSolicitud.Visible = True
        txtModalFechaPago.Enabled = False
        lnkBtnModalFechaPago.Visible = True
        txtModalFechaTCObs.Enabled = False
        lnkBtnModalFechaTCObs.Visible = True
        txtModalMonto.Enabled = True
        txtModalMontoCLP.Enabled = False
        ddlModalContrato.Enabled = True
        ddlModalRutFondo.Enabled = True
        ddlModalNombreFondo.Enabled = True
        ddlModalNemotecnico.Enabled = True
        txtModalMonedaSerie.Enabled = False
        txtModalNAV.Enabled = True
        txtModalNAV_CLP.Enabled = False
        ddlModalPoderes.Enabled = True
        ddlModalEstado.Enabled = True
        txtModalObservaciones.Enabled = True
        lnkModalFechaSolicitud.Visible = True

        lnkModalBorrarFechaSolicitud.Visible = False
        lnkBtnModalBorrarFechaNAV.Visible = False
        lnkBtnModalBorrarFechaPago.Visible = False
        lnkBtnModalBorrarFechaTCObs.Visible = False


        lblModalTitle.Text = CONST_TITULO_MODAL_MODIFICAR
    End Sub
#End Region

#Region "AGREGA A LA MODAL RESCATE A ELIMINAR"
    Protected Sub BtnEliminar_Click(sender As Object, e As EventArgs)
        Session("TipodeCalculoTC") = Nothing
        Dim negocio As RescateNegocio = New RescateNegocio
        Dim RescateSelect As RescatesDTO = GetRescateSelectEliminar()
        Dim RescateActualizado As RescatesDTO = negocio.GetRescateOne(RescateSelect)

        Dim Relacion As RescatesDTO = negocio.GetRelaciones(RescateSelect)

        If (Relacion.CountAP > 0) Then
            ShowAlert("No se puede eliminar el rescate, información del aportante se modificó")
            txtAccionHidden.Value = ""
        ElseIf (Relacion.CountFN > 0) Then
            ShowAlert("No se puede eliminar este rescate, información del fondo se modificó")
            txtAccionHidden.Value = ""
        ElseIf (Relacion.CountFS > 0) Then
            ShowAlert("No se puede eliminar este rescate, información de la serie se modificó")
            txtAccionHidden.Value = ""
        Else
            FormateoFormDatosEliminar(RescateActualizado)
            FormateoEstiloFormEliminar()

            txtAccionHidden.Value = "ELIMINAR"
        End If




    End Sub

    Private Function GetRescateSelectEliminar() As RescatesDTO
        Dim Rescate As New RescatesDTO

        For Each row As GridViewRow In GrvTabla.Rows
            Dim chk As RadioButton = row.Cells(0).Controls(1)
            If chk IsNot Nothing And chk.Checked Then
                Rescate.RES_ID = row.Cells(1).Text.Trim()
            End If
        Next

        Return Rescate
    End Function

    Private Sub FormateoFormDatosEliminar(Rescate As RescatesDTO)
        Dim negocio As CertificadoNegocio = New CertificadoNegocio

        CargarRutFondoModal()
        CargaNombreFondoModal()
        CargarRutAportanteModal()
        CargarNombreAportanteModal()
        CargarMultifondoModal()
        CargaNemotecnicoModal()
        ddlModalMonedaPago.Items.Insert(0, New ListItem("", ""))

        txtIDRescate.Text = Rescate.RES_ID
        txtModalFechaSolicitud.Text = CDate(Rescate.RES_Fecha_Solicitud.ToString("dd/MM/yyyy"))
        txtModalFechaPago.Text = CDate(Rescate.RES_Fecha_Pago.ToString("dd/MM/yyyy"))
        ddlModalRutAportante.SelectedValue = Rescate.AP_RUT
        ddlModalMultifondo.SelectedValue = Rescate.AP_Multifondo
        ddlModalNemotecnico.SelectedValue = Rescate.FS_Nemotecnico
        txtModalCuota.Text = Utiles.SetToCapitalizedNumber(Rescate.RES_Cuotas)
        ddlModalRutFondo.SelectedValue = Rescate.FN_RUT
        'ddlModalNombreFondo.SelectedValue = Rescate.FN_Nombre_Corto

        Dim fondo As FondoDTO = Utiles.GetFondo(Rescate.FN_RUT)
        ddlModalNombreFondo.SelectedValue = fondo.RazonSocial
        ddlModalNombreAportante.SelectedValue = Rescate.AP_Razon_Social
        txtModalNombreSerie.Text = Rescate.FS_Nombre_Corto
        ddlModalMonedaPago.SelectedValue = Rescate.RES_Moneda_Pago
        txtModalCuotasDVC.Text = Utiles.SetToCapitalizedNumber(Rescate.ADCV_Cantidad)
        txtModalFechaNAV.Text = CDate(Rescate.RES_Fecha_Nav.ToString("dd/MM/yyyy"))
        txtModalFechaTCObs.Text = CDate(Rescate.RES_FechaTCObs.ToString("dd/MM/yyyy"))
        txtModalNAV.Text = Rescate.RES_NavFormat
        txtModalMonto.Text = Rescate.RES_MontoFormat            ' Utiles.SetToCapitalizedNumber(Rescate.RES_Monto)
        txtModalNAV_CLP.Text = Rescate.RES_Nav_CLPFormat
        txtModalMontoCLP.Text = Rescate.RES_Monto_CLPFormat     ' String.Format("{0:N0}", Rescate.RES_Monto_CLP)
        txtModalTCObservado.Text = Utiles.SetToCapitalizedNumber(Rescate.TC_Valor)
        ddlModalContrato.SelectedValue = Rescate.RES_Contrato
        ddlModalPoderes.SelectedValue = Rescate.RES_Poderes
        ddlModalEstado.Text = Rescate.RES_Estado
        txtModalObservaciones.Text = Rescate.RES_Observaciones
        txtModalPatrimonio.Text = Utiles.SetToCapitalizedNumber(Rescate.RES_Patrimonio)
        txtModalPorcentaje.Text = Rescate.FS_Patrimonio
        txtModalDisponibles.Text = Utiles.SetToCapitalizedNumber(Rescate.RES_Disponible_Patrimonio)
        txtModalFechaDCV.Text = CDate(Rescate.ADCV_Fecha.ToString("dd/MM/yyyy"))
        txtModalSuscripciones.Text = Utiles.SetToCapitalizedNumber(Rescate.SC_Cuotas_a_Suscribir)
        txtModalCanje.Text = Utiles.SetToCapitalizedNumber(Rescate.CN_Cuotas_Disponibles)
        txtModalDisponiblesCuotasDisponibles.Text = Utiles.SetToCapitalizedNumber(Rescate.RES_Cuotas_Disponibles)
        txtModalRescates.Text = Utiles.SetToCapitalizedNumber(Rescate.RES_Transito)
        txtModalFijacionNAV.Text = Rescate.RES_Fijacion_NAV
        txtModalFijacionTCObs.Text = Rescate.RES_Fijacion_TCObservado
        txtModalMonedaSerie.Text = Rescate.FS_Moneda
        txtModalRescateMax.Text = Utiles.SetToCapitalizedNumber(Rescate.RES_Maximo)
        txtModalUtilizado.Text = Utiles.SetToCapitalizedNumber(Rescate.RES_Utilizado)


    End Sub

    Private Sub FormateoEstiloFormEliminar()
        btnModalGuardar.Enabled = False
        btnModalGuardar.Visible = False


        btnModalModificar.Enabled = False
        btnModalModificar.Visible = True
        btnModalEliminar.Enabled = True
        btnModalEliminar.Visible = True

        ddlModalRutAportante.Enabled = False
        ddlModalNombreAportante.Enabled = False
        ddlModalMultifondo.Enabled = False
        txtModalNombreSerie.Enabled = False
        txtModalCuota.Enabled = False
        ddlModalMonedaPago.Enabled = False
        txtModalTCObservado.Enabled = False
        txtModalFechaSolicitud.Enabled = False
        txtModalFechaNAV.Enabled = False
        lnkBtnModalFechaNAV.Enabled = False
        lnkBtnModalFechaNAV.Visible = False
        txtModalFechaPago.Enabled = False
        lnkBtnModalFechaPago.Visible = False
        txtModalFechaTCObs.Enabled = False
        lnkBtnModalFechaTCObs.Visible = False
        txtModalMonto.Enabled = False
        txtModalMontoCLP.Enabled = False
        ddlModalContrato.Enabled = False
        ddlModalRutFondo.Enabled = False
        ddlModalNombreFondo.Enabled = False
        ddlModalNemotecnico.Enabled = False
        txtModalMonedaSerie.Enabled = False
        txtModalNAV.Enabled = False
        txtModalNAV_CLP.Enabled = False
        ddlModalPoderes.Enabled = False
        ddlModalEstado.Enabled = False
        txtModalObservaciones.Enabled = False

        lnkModalFechaSolicitud.Visible = False

        lnkModalBorrarFechaSolicitud.Visible = False
        lnkBtnModalBorrarFechaNAV.Visible = False
        lnkBtnModalBorrarFechaPago.Visible = False
        lnkBtnModalBorrarFechaTCObs.Visible = False


        lblModalTitle.Text = CONST_TITULO_MODAL_ElIMINAR
    End Sub
#End Region


#Region "BOTON MODIFICAR"
    Private Sub btnModalModificar_Click(sender As Object, e As EventArgs) Handles btnModalModificar.Click

        'Validaciones    JC
        If txtModalFechaNAV.Text.Equals("") Then
            ShowAlert("El campo Fecha NAV No puede estar vacío")
            Return
        End If

        If txtModalFechaPago.Text.Equals("") Then
            ShowAlert("El campo Fecha Pago No puede estar vacío")
            Return
        End If

        If txtModalCuota.Text.Equals("0") Then
            ShowAlert("El campo cuotas debe ser mayor a 0")
            Return
        End If

        If txtModalMonto.Text.Equals("0") Then
            ShowAlert("El campo monto debe ser mayor a 0")
            Return
        End If

        'MODIFICAR
        Dim negocioMod As RescateNegocio = New RescateNegocio
        Dim Rescate As RescatesDTO = GetRescateModalModificar()

        Dim solicitudMod As Integer = negocioMod.UpdateRescate(Rescate)

        txtAccionHidden.Value = "MOSTRAR_DIALOGO"

        If solicitudMod = Constantes.CONST_OPERACION_EXITOSA Then
            'ShowMesagges(CONST_TITULO_VALORESCUOTA, CONST_MODIFICAR_EXITO, Constantes.CONST_RUTA_IMG + Constantes.CONST_IMG_LOGO, Constantes.CONST_RUTA_IMG + Constantes.CONST_IMG_CORRECTO)
            ShowAlert(CONST_MODIFICAR_EXITO)
            GenerarPopUp()
        Else
            ' ShowMesagges(CONST_TITULO_VALORESCUOTA, CONST_MODIFICAR_ERROR, Constantes.CONST_RUTA_IMG + Constantes.CONST_IMG_LOGO, Constantes.CONST_RUTA_IMG + Constantes.CONST_IMG_ERROR)
            ShowAlert(CONST_MODIFICAR_ERROR)
        End If

        'txtAccionHidden.Value = ""
        DataInitial()
        Me.GrvTabla.DataSource = Nothing
        GrvTabla.DataBind()
    End Sub

    Private Function GetRescateModalModificar() As RescatesDTO
        Dim Rescate As RescatesDTO = New RescatesDTO()

        Rescate.RES_ID = txtIDRescate.Text
        Rescate.RES_Fecha_Solicitud = txtModalFechaSolicitud.Text
        Rescate.RES_Fecha_Pago = txtModalFechaPago.Text
        Rescate.AP_RUT = ddlModalRutAportante.SelectedValue
        Rescate.AP_Multifondo = ddlModalMultifondo.SelectedValue
        Rescate.FS_Nemotecnico = ddlModalNemotecnico.SelectedValue
        Rescate.RES_Cuotas = txtModalCuota.Text
        Rescate.FN_RUT = ddlModalRutFondo.SelectedValue
        Rescate.FN_Nombre_Corto = ddlModalNombreFondo.SelectedValue
        Rescate.RES_Tipo_Transaccion = "Rescate"
        Rescate.AP_Razon_Social = ddlModalNombreAportante.SelectedValue
        Rescate.FS_Nombre_Corto = txtModalNombreSerie.Text
        Rescate.RES_Moneda_Pago = ddlModalMonedaPago.SelectedValue
        Rescate.ADCV_Cantidad = txtModalCuotasDVC.Text
        Rescate.RES_Fecha_Nav = txtModalFechaNAV.Text
        Rescate.RES_FechaTCObs = txtModalFechaTCObs.Text
        Rescate.RES_Nav = txtModalNAV.Text
        Rescate.RES_Monto = txtModalMonto.Text
        Rescate.RES_Nav_CLP = txtModalNAV_CLP.Text
        Rescate.RES_Monto_CLP = txtModalMontoCLP.Text
        Rescate.TC_Valor = txtModalTCObservado.Text
        Rescate.RES_Contrato = ddlModalContrato.SelectedValue
        Rescate.RES_Poderes = ddlModalPoderes.SelectedValue
        Rescate.RES_Estado = ddlModalEstado.Text
        Rescate.RES_Observaciones = txtModalObservaciones.Text
        Rescate.RES_Patrimonio = txtModalPatrimonio.Text
        Rescate.FS_Patrimonio = txtModalPorcentaje.Text
        Rescate.RES_Disponible_Patrimonio = txtModalDisponibles.Text
        Rescate.ADCV_Fecha = txtModalFechaDCV.Text
        Rescate.SC_Cuotas_a_Suscribir = txtModalSuscripciones.Text
        Rescate.CN_Cuotas_Disponibles = txtModalCanje.Text
        Rescate.RES_Cuotas_Disponibles = txtModalDisponiblesCuotasDisponibles.Text
        Rescate.RES_Transito = txtModalRescates.Text
        Rescate.RES_Fijacion_NAV = txtModalFijacionNAV.Text
        Rescate.RES_Fijacion_TCObservado = txtModalFijacionTCObs.Text
        Rescate.RES_Fecha_Modificacion = Date.Now
        Rescate.RES_Usuario_Modificacion = Session("NombreUsuario")
        Rescate.FS_Moneda = txtModalMonedaSerie.Text
        Rescate.RES_Estado_Rescate = 1
        Rescate.RES_Maximo = txtModalRescateMax.Text
        Rescate.RES_Utilizado = txtModalUtilizado.Text

        Return Rescate
    End Function
#End Region

#Region "BOTON ELIMINAR"
    Private Sub btnModalEliminar_Click(sender As Object, e As EventArgs) Handles btnModalEliminar.Click

        'MODIFICAR
        Dim negocioMod As RescateNegocio = New RescateNegocio
        Dim Rescate As RescatesDTO = GetRescateModalEliminar()

        Dim solicitudMod As Integer = negocioMod.DeleteRescate(Rescate)

        txtAccionHidden.Value = "MOSTRAR_DIALOGO"

        If solicitudMod = Constantes.CONST_OPERACION_EXITOSA Then
            'ShowMesagges(CONST_TITULO_VALORESCUOTA, CONST_ELIMINAR_EXITO, Constantes.CONST_RUTA_IMG + Constantes.CONST_IMG_LOGO, Constantes.CONST_RUTA_IMG + Constantes.CONST_IMG_CORRECTO)
            ShowAlert(CONST_ELIMINAR_EXITO)
        Else
            'ShowMesagges(CONST_TITULO_VALORESCUOTA, CONST_ELIMINAR_ERROR, Constantes.CONST_RUTA_IMG + Constantes.CONST_IMG_LOGO, Constantes.CONST_RUTA_IMG + Constantes.CONST_IMG_ERROR)
            ShowAlert(CONST_ELIMINAR_ERROR)
        End If

        txtAccionHidden.Value = ""
        DataInitial()
        Me.GrvTabla.DataSource = Nothing
        GrvTabla.DataBind()
    End Sub

    Private Function GetRescateModalEliminar() As RescatesDTO
        Dim Rescate As RescatesDTO = New RescatesDTO()

        Rescate.RES_ID = txtIDRescate.Text

        Return Rescate
    End Function
#End Region

#Region "ALERTAS"
    Private Sub ShowAlert(mesagge As String)
        Dim myScript As String = "alert('" + mesagge + "');"
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", myScript, True)
    End Sub

    Private Sub ShowMesagges(title As String, mesagge As String, urlIconTitle As String, urlIconMesagge As String, Optional borraLink As Boolean = True)
        lblModalTitle.Text = title
        lblModalBody.Text = mesagge
        img_modal.ImageUrl = urlIconTitle
        img_body_modal.ImageUrl = urlIconMesagge

        'LinkButton.Visible = Not (borraLink)
        txtAccionHidden.Value = "SHOW_DIALOG"

        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModalmg", "$('#myModalmg').modal();", True)
    End Sub
#End Region


#Region "EVENTOS FILTROS DE FECHA"

    Private Sub lnkModalBorrarFechaSolicitud_Click(sender As Object, e As EventArgs) Handles lnkModalBorrarFechaSolicitud.Click
        Session("CambioFechaSolicitud") = "Hecho"
        Request.Form(txtModalFechaSolicitud.UniqueID) = ""
        If Request.Form(txtModalFechaSolicitud.UniqueID) = "" Then
            txtModalFechaSolicitud.Text = "1900-01-01"
            CargarNombreMonedaSerieModal()
        Else
            CargarNombreMonedaSerieModal()
        End If
    End Sub

    Private Sub lnkBtnModalBorrarFechaNAV_Click(sender As Object, e As EventArgs) Handles lnkBtnModalBorrarFechaNAV.Click
        txtModalFechaNAV.Text = ""
    End Sub

    Private Sub lnkBtnModalBorrarFechaPago_Click(sender As Object, e As EventArgs) Handles lnkBtnModalBorrarFechaPago.Click
        txtModalFechaPago.Text = ""
    End Sub

    Private Sub lnkBtnModalBorrarFechaTCObs_Click(sender As Object, e As EventArgs) Handles lnkBtnModalBorrarFechaTCObs.Click
        txtModalFechaTCObs.Text = ""
    End Sub

    Private Sub lnkBtnFechaBorrarSolicitudDesde_Click(sender As Object, e As EventArgs) Handles lnkBtnFechaBorrarSolicitudDesde.Click
        txtAccionHidden.Value = ""
        txtFechaSolicitudDesde.Text = ""
    End Sub

    Private Sub BtnLimpiarFechaDesde_Click(sender As Object, e As EventArgs) Handles BtnLimpiarFechaDesde.Click
        txtAccionHidden.Value = ""
        txtFechaSolicitudHasta.Text = ""
    End Sub

    Private Sub lnkBtnFechaBorrarNAVDesde_Click(sender As Object, e As EventArgs) Handles lnkBtnFechaBorrarNAVDesde.Click
        txtAccionHidden.Value = ""
        txtFechaNAVDesde.Text = ""
    End Sub

    Private Sub lnkBtnFechaBorrarNAVHasta_Click(sender As Object, e As EventArgs) Handles lnkBtnFechaBorrarNAVHasta.Click
        txtAccionHidden.Value = ""
        txtFechaNAVHasta.Text = ""
    End Sub

    Private Sub lnkBtnFechaBorrarPagoDesde_Click(sender As Object, e As EventArgs) Handles lnkBtnFechaBorrarPagoDesde.Click
        txtAccionHidden.Value = ""
        txtFechaPagoDesde.Text = ""
    End Sub

    Private Sub lnlBtnFechaBorrarPagoHasta_Click(sender As Object, e As EventArgs) Handles lnlBtnFechaBorrarPagoHasta.Click
        txtAccionHidden.Value = ""
        txtFechaPagoHasta.Text = ""
    End Sub

#End Region

#Region "TRAER VALOR NAV Y VALOR TIPO CAMBIO MANUAL POR FECHAS RESPECTIVAS(NAV Y TC)"
    Public Sub CargarNAV()
        Dim negocioFondoSerie As FondoSeriesNegocio = New FondoSeriesNegocio
        Dim serie As FondoSerieDTO = New FondoSerieDTO
        Dim lista As List(Of FondoSerieDTO)

        serie.Nemotecnico = ddlModalNemotecnico.SelectedValue
        lista = negocioFondoSerie.GrupoSeriesPorNemotecnico(serie)

        For Each series As FondoSerieDTO In lista
            Dim llega As String = series.FijacionCanje

            If llega = "Automático" Then
                Dim ValoresCuota As VcSerieDTO = New VcSerieDTO()
                Dim NegocioValoresCuota As ValoresCuotaNegocio = New ValoresCuotaNegocio
                Dim ValoresCuotaActualizado As VcSerieDTO = New VcSerieDTO()
                Dim txtModalNAVText As Decimal

                ValoresCuota.FsNemotecnico = ddlModalNemotecnico.SelectedValue
                If txtModalFechaNAV.Text = "" Then
                    ValoresCuota.Fecha = Nothing
                Else
                    ValoresCuota.Fecha = txtModalFechaNAV.Text
                End If

                ValoresCuota.FnRut = ddlModalRutFondo.SelectedValue
                ValoresCuotaActualizado = NegocioValoresCuota.GetValoresCuota(ValoresCuota)

                If ValoresCuotaActualizado IsNot Nothing Then
                    txtModalNAV.Text = Utiles.formatearNAV(ValoresCuotaActualizado.Valor) ' String.Format("{0:N6}", ValoresCuotaActualizado.Valor)

                    'TRAER ULTIMA TC DE CLP
                    Dim TipoCambio1 As TipoCambioDTO = New TipoCambioDTO()
                    Dim NegocioTipoCambio1 As TipoCambioNegocio = New TipoCambioNegocio
                    Dim TipoCambioActualizado1 As TipoCambioDTO = New TipoCambioDTO()

                    TipoCambio1.Codigo = txtModalMonedaSerie.Text

                    Dim ListaTC As List(Of TipoCambioDTO) = NegocioTipoCambio1.UltimoTipoCambioPorCodigo(TipoCambio1)
                    If txtModalMonedaSerie.Text = "CLP" Then
                        txtModalNAVText = txtModalNAV.Text
                        txtModalNAV_CLP.Text = Utiles.formatearNAVCLP(txtModalNAVText)  '  String.Format("{0:N4}", Convert.ToDecimal(txtModalNAVText))
                    Else
                        For Each tipos As TipoCambioDTO In ListaTC
                            txtModalNAVText = txtModalNAV.Text
                            txtModalNAV_CLP.Text = Utiles.calcularNAVCLP(tipos.Valor, txtModalNAVText)    ' String.Format("{0:N4}", Math.Round(txtModalNAVText * valor1, MidpointRounding.ToEven))
                        Next
                    End If

                    txtModalFijacionNAV.Text = "Realizado"
                Else
                    'TRAE EL ULTIMO VALORES CUOTA POR NEMOTECNICO
                    ValoresCuota.FsNemotecnico = ddlModalNemotecnico.SelectedValue

                    Dim ListaVc As List(Of VcSerieDTO) = NegocioValoresCuota.UltimoValorCuota(ValoresCuota)

                    For Each ValCuota As VcSerieDTO In ListaVc
                        txtModalNAV.Text = Utiles.formatearNAV(ValCuota.Valor)  ' String.Format("{0:N6}", ValCuota.Valor)

                        If txtModalMonedaSerie.Text = "CLP" Then
                            txtModalNAVText = txtModalNAV.Text
                            txtModalNAV_CLP.Text = Utiles.formatearNAVCLP(txtModalNAVText) ' String.Format("{0:N4}", Convert.ToDecimal(txtModalNAVText))
                        Else
                            'TRAER ULTIMA TC DE CLP
                            Dim TipoCambio1 As TipoCambioDTO = New TipoCambioDTO()
                            Dim NegocioTipoCambio1 As TipoCambioNegocio = New TipoCambioNegocio
                            Dim TipoCambioActualizado1 As TipoCambioDTO = New TipoCambioDTO()

                            TipoCambio1.Codigo = txtModalMonedaSerie.Text

                            Dim ListaTC As List(Of TipoCambioDTO) = NegocioTipoCambio1.UltimoTipoCambioPorCodigo(TipoCambio1)

                            If txtModalMonedaSerie.Text = "CLP" Then
                                txtModalNAVText = txtModalNAV.Text
                                txtModalNAV_CLP.Text = Utiles.formatearNAVCLP(txtModalNAVText) ' String.Format("{0:N4}", Convert.ToDecimal(txtModalNAVText))
                            Else
                                For Each oTC As TipoCambioDTO In ListaTC
                                    txtModalNAV_CLP.Text = Utiles.calcularNAVCLP(oTC.Valor, txtModalNAV.Text) 'String.Format("{0:N4}", Math.Round(txtModalNAVText * valor1, MidpointRounding.ToEven))
                                Next
                            End If
                        End If

                    Next
                    txtModalFijacionNAV.Text = "Pendiente"
                End If

            Else ' If llega = "Manual" Then
                Dim ValoresCuota As VcSerieDTO = New VcSerieDTO()
                Dim NegocioValoresCuota As ValoresCuotaNegocio = New ValoresCuotaNegocio
                Dim ValoresCuotaActualizado As VcSerieDTO = New VcSerieDTO()

                Dim txtModalNAVText As Decimal

                ValoresCuota.FsNemotecnico = ddlModalNemotecnico.SelectedValue
                If txtModalFechaNAV.Text = "" Then
                    ValoresCuota.Fecha = Nothing
                Else
                    ValoresCuota.Fecha = txtModalFechaNAV.Text
                End If
                ValoresCuota.FnRut = ddlModalRutFondo.SelectedValue
                ValoresCuotaActualizado = NegocioValoresCuota.GetValoresCuota(ValoresCuota)

                If ValoresCuotaActualizado IsNot Nothing Then

                    txtModalNAV.Text = Utiles.formatearNAV(ValoresCuotaActualizado.Valor) ' String.Format("{0:N6}", ValoresCuotaActualizado.Valor)


                    'TRAER ULTIMA TC DE CLP
                    Dim TipoCambio1 As TipoCambioDTO = New TipoCambioDTO()
                    Dim NegocioTipoCambio1 As TipoCambioNegocio = New TipoCambioNegocio
                    Dim TipoCambioActualizado1 As TipoCambioDTO = New TipoCambioDTO()
                    'TipoCambio1.Codigo = "CLP"
                    TipoCambio1.Codigo = txtModalMonedaSerie.Text
                    Dim valor1 As Decimal
                    Dim ListaTC As List(Of TipoCambioDTO) = NegocioTipoCambio1.UltimoTipoCambioPorCodigo(TipoCambio1)
                    If txtModalMonedaSerie.Text = "CLP" Then
                        txtModalNAV_CLP.Text = Utiles.formatearNAVCLP(txtModalNAV.Text) 'String.Format("{0:N4}", Convert.ToDecimal(txtModalNAVText))
                    Else
                        For Each tipos As TipoCambioDTO In ListaTC
                            valor1 = Double.Parse(tipos.Valor)
                            txtModalNAVText = txtModalNAV.Text
                            txtModalNAV_CLP.Text = Utiles.calcularNAVCLP(tipos.Valor, txtModalNAV.Text)  ' String.Format("{0:N4}", Math.Round(txtModalNAVText * valor1, MidpointRounding.ToEven))
                        Next
                    End If

                    txtModalFijacionNAV.Text = "Pendiente"
                Else
                    'TRAE EL ULTIMO VALORES CUOTA POR NEMOTECNICO
                    ValoresCuota.FsNemotecnico = ddlModalNemotecnico.SelectedValue

                    Dim ListaVc As List(Of VcSerieDTO) = NegocioValoresCuota.UltimoValorCuota(ValoresCuota)

                    For Each ValCuota As VcSerieDTO In ListaVc
                        txtModalNAV.Text = Utiles.formatearNAV(ValCuota.Valor) ' String.Format("{0:N6}", ValCuota.Valor)

                        If txtModalMonedaSerie.Text = "CLP" Then
                            txtModalNAVText = txtModalNAV.Text
                            txtModalNAV_CLP.Text = Utiles.formatearNAVCLP(txtModalNAVText) ' String.Format("{0:N4}", Convert.ToDecimal(txtModalNAVText))
                        Else
                            'TRAER ULTIMA TC DE CLP
                            Dim TipoCambio1 As TipoCambioDTO = New TipoCambioDTO()
                            Dim NegocioTipoCambio1 As TipoCambioNegocio = New TipoCambioNegocio
                            Dim TipoCambioActualizado1 As TipoCambioDTO = New TipoCambioDTO()
                            TipoCambio1.Codigo = txtModalMonedaSerie.Text
                            Dim ListaTC As List(Of TipoCambioDTO) = NegocioTipoCambio1.UltimoTipoCambioPorCodigo(TipoCambio1)
                            If txtModalMonedaSerie.Text = "CLP" Then
                                txtModalNAV_CLP.Text = Utiles.formatearNAVCLP(txtModalNAV.Text) ' String.Format("{0:N4}", Convert.ToDecimal(txtModalNAVText))
                            Else
                                For Each tipos As TipoCambioDTO In ListaTC
                                    txtModalNAV_CLP.Text = Utiles.calcularNAVCLP(tipos.Valor, txtModalNAV.Text) ' String.Format("{0:N4}", Math.Round(txtModalNAV.Text * valor1, MidpointRounding.ToEven))
                                Next
                            End If
                        End If

                    Next
                    txtModalFijacionNAV.Text = "Pendiente"
                End If
            End If
        Next

    End Sub

    Public Sub CargarTC()
        'TRAER VALOR TIPO CAMBIO OBSERVADO
        Session("TipodeCalculoTC") = "ActivaFecha"

        Dim TipoCambio As TipoCambioDTO = New TipoCambioDTO()
        Dim NegocioTipoCambio As TipoCambioNegocio = New TipoCambioNegocio
        Dim TipoCambioActualizado As TipoCambioDTO = New TipoCambioDTO()
        'Dim txtModalTCObservadoText As Decimal

        'If TipoCambio IsNot Nothing Then
        If txtModalFechaTCObs.Text <> "" And txtModalMonedaSerie.Text <> "" Then
            TipoCambio.Fecha = txtModalFechaTCObs.Text
            TipoCambio.Codigo = txtModalMonedaSerie.Text

            TipoCambioActualizado = NegocioTipoCambio.GetTipoCambio(TipoCambio)

            If TipoCambioActualizado IsNot Nothing Then
                txtModalTCObservado.Text = Utiles.SetToCapitalizedNumber(TipoCambioActualizado.Valor)
            Else
                'TRAE EL ULTIMO TC DE LA MONEDA DE SERIE
                setUltimoTC()
            End If
        Else
            'TRAE EL ULTIMO TC DE LA MONEDA DE DE SERIE
            setUltimoTC()
        End If

        CalcularMontos()

    End Sub

    Private Sub setUltimoTC()
        Dim TipoCambio As TipoCambioDTO = New TipoCambioDTO()
        Dim NegocioTipoCambio As TipoCambioNegocio = New TipoCambioNegocio

        Dim valor As Decimal
        Dim fecha As Date
        Dim ListaTC As List(Of TipoCambioDTO)

        TipoCambio.Codigo = txtModalMonedaSerie.Text
        ListaTC = NegocioTipoCambio.UltimoTipoCambioPorCodigo(TipoCambio)

        For Each tipos As TipoCambioDTO In ListaTC
            valor = Double.Parse(tipos.Valor)
            fecha = tipos.Fecha
            txtModalTCObservado.Text = Utiles.SetToCapitalizedNumber(valor)
        Next
    End Sub
#End Region

    Public Sub CargarNAV_CLP()
        CalcularMontos()
    End Sub

    Private Sub CalcularMontos()
        txtModalNAV_CLP.Text = Utiles.calcularNAVCLP(txtModalTCObservado.Text, txtModalNAV.Text)

        txtModalMontoCLP.Text = Utiles.calcularMontoCLP(txtModalCuota.Text, txtModalNAV.Text, txtModalTCObservado.Text)
        txtModalMonto.Text = Utiles.calcularMonto(txtModalCuota.Text, txtModalNAV.Text, txtModalMonedaSerie.Text)  ' String.Format("{0:N2}", txtModalCuota.Text * txtModalNAV.Text)

    End Sub

    Public Sub CargarTodoCuandoCambiaFechaSolicitud()
        Session("TipodeCalculoTC") = "ActivaRutNombreNemotecnico"
        Dim NegocioFondoSerie As FondoSeriesNegocio = New FondoSeriesNegocio
        Dim NegocioRescate As RescateNegocio = New RescateNegocio

        Dim fondoSerie As FondoSerieDTO = New FondoSerieDTO()
        Dim primerRegistroNemotecnico As String = ""
        Dim FondoRescatable As String = ""
        Dim FechaNAVFondoRescatable As String = ""
        Dim FechaNAVFondoRescatableINT As Integer = 0
        Dim FechaPagoFondoRescatable As String = ""
        Dim FechaPagoFondoRescatableINT As Integer = 0
        Dim FechaTCFondoRescatable As String = ""
        'Dim FechaTCFondoRescatableINT As Integer
        Dim FechaSolicitud As Date
        Dim FechaNAV As Date
        Dim FechaPago As Date
        Dim FechaTC As Date
        Dim FijacionNAV As String = ""
        Dim PorcentajePatrimonio As String = ""

        'CARGA Y VALIDA RESTRICCION DE LA FECHA DE SOLICITUD
        Dim HorarioRestriccion As String
        Dim hora As Integer

        HorarioRestriccion = "00:00"
        PorcentajePatrimonio = 0

        Dim Fondo As FondoDTO = New FondoDTO()

        If ddlModalRutFondo.SelectedValue IsNot Nothing Then
            fondoSerie.Nemotecnico = ddlModalNemotecnico.SelectedValue
            fondoSerie.Rut = ddlModalRutFondo.SelectedValue
        Else
            fondoSerie.Nemotecnico = ddlModalNemotecnico.SelectedValue
            fondoSerie.Rut = Nothing
        End If

        Dim FondoSerieAct As FondoSerieDTO = New FondoSerieDTO()
        Dim ListaFondoSerieActualizado As List(Of FondoSerieDTO) = NegocioFondoSerie.GrupoSeriesPorNemotecnico(fondoSerie)
        If ListaFondoSerieActualizado IsNot Nothing Then
            If ListaFondoSerieActualizado.Count > 0 AndAlso ListaFondoSerieActualizado(0).Nemotecnico <> Nothing Then
                FondoSerieAct = ListaFondoSerieActualizado(0)
            End If
            'For Each FondoSerieAct As FondoSerieDTO In ListaFondoSerieActualizado
            If FondoSerieAct.HorarioRecaste = "" Then
                HorarioRestriccion = "00:00"
            Else
                HorarioRestriccion = FondoSerieAct.HorarioRecaste
            End If

            FondoRescatable = FondoSerieAct.FondoRescatable
            If FondoSerieAct.Patrimonio = "" Then
                PorcentajePatrimonio = "0"
            Else
                PorcentajePatrimonio = FondoSerieAct.Patrimonio
            End If

            FechaNAVFondoRescatable = FondoSerieAct.FechaNav
            FechaPagoFondoRescatable = FondoSerieAct.FechaRescate
            FechaTCFondoRescatable = FondoSerieAct.FechaTCObservado
            FijacionNAV = FondoSerieAct.FijacionNav
            'CARGA NOMBRE DE LA SERIE
            txtModalNombreSerie.Text = FondoSerieAct.Nombrecorto
            'CARGA MONEDA DE LA SERIE
            txtModalMonedaSerie.Text = FondoSerieAct.Moneda
            '  Next
        End If


        hora = CInt(HorarioRestriccion.Substring(0, 2))



        '//////////////SECCION PATRIMONIO/////////////////////
        'CARGA CAMPO PATRIMONIO DESDE LA INTERFAZ
        Dim Patrimonio As PatrimonioDTO = New PatrimonioDTO()
        Dim NegocioPatrimonio As PatrimonioNegocio = New PatrimonioNegocio

        Patrimonio.IDFONDO = ddlModalRutFondo.SelectedValue

        Dim PatrimonionActualizado As PatrimonioDTO = NegocioPatrimonio.GetPatrimonio(Patrimonio)

        If PatrimonionActualizado IsNot Nothing Then
            txtModalPatrimonio.Text = Utiles.SetToCapitalizedNumber(PatrimonionActualizado.NPATRIMONIO)
        Else
            txtModalPatrimonio.Text = "0"
        End If

        'CARGA PORCENTAJE % PATRIMONIO
        txtModalPorcentaje.Text = PorcentajePatrimonio

        'CALCULA RESCATE MAX
        txtModalRescateMax.Text = Utiles.SetToCapitalizedNumber(((txtModalPorcentaje.Text / 100) * txtModalPatrimonio.Text))

        'PATRIMONIO UTILIZADO (RESCATES DEL DIA)
        Dim RescateHoy As RescatesDTO = New RescatesDTO()
        Dim NegocioRescateHoy As RescateNegocio = New RescateNegocio
        Dim RescateActualizadoHoy As RescatesDTO = New RescatesDTO()

        If txtModalFechaSolicitud.Text <> "" Then
            RescateHoy.RES_Fecha_Solicitud = txtModalFechaSolicitud.Text
        Else
            RescateHoy.RES_Fecha_Solicitud = Nothing
        End If

        RescateHoy.FS_Nemotecnico = ddlModalNemotecnico.SelectedValue
        RescateHoy.FN_RUT = ddlModalRutFondo.SelectedValue
        RescateHoy.AP_RUT = ddlModalRutAportante.SelectedValue

        RescateActualizadoHoy = NegocioRescateHoy.SelectRescatesHoy(RescateHoy)

        If RescateActualizadoHoy IsNot Nothing Then
            txtModalUtilizado.Text = Utiles.SetToCapitalizedNumber(RescateActualizadoHoy.RES_Monto)
        Else
            txtModalRescates.Text = "0"
        End If

        'CALCULO DISPONIBLES PATRIMONIO
        txtModalDisponibles.Text = Utiles.SetToCapitalizedNumber(Double.Parse(txtModalRescateMax.Text) - Double.Parse(txtModalUtilizado.Text))

        '////////////FIN SECCION PATRIMONIO//////////////////

        EstablecerDatosDCV()

        'VALIDA MONEDA PAGO
        If txtModalMonedaSerie.Text <> "USD" Then
            If txtModalMonedaSerie.Text = "" Then
                ddlModalMonedaPago.SelectedIndex = 0

            Else
                If cboExiste(ddlModalMonedaPago, txtModalMonedaSerie.Text) Then
                    ddlModalMonedaPago.Text = txtModalMonedaSerie.Text
                    ddlModalMonedaPago.Enabled = False
                Else
                    ddlModalMonedaPago.SelectedIndex = 0
                End If
            End If
        Else
            ddlModalMonedaPago.Enabled = True
        End If

        'VALIDA FECHAS NAV Y PAGO SEGUN TIPO DE FONDO RESCATABLE
        FechaSolicitud = txtModalFechaSolicitud.Text

        If txtModalFechaNAV.Text = "" Then
            FechaNAV = Nothing
        Else
            FechaNAV = txtModalFechaNAV.Text
        End If

        If txtModalFechaPago.Text = "" Then
            FechaPago = Nothing
        Else
            FechaPago = txtModalFechaPago.Text
        End If

        If txtModalFechaTCObs.Text = "" Then
            FechaTC = Nothing
        Else
            FechaTC = txtModalFechaTCObs.Text
        End If


        If FondoRescatable = "Si" Then
            Dim TipoFechaAñadirNAV As String
            Dim TipoFechaAñadirPago As String
            ' Dim TipoFechaAñadirTC As String
            Dim FechaCalculo As DateTime

            txtModalFechaNAV.Enabled = False
            txtModalFechaPago.Enabled = False
            txtModalFechaTCObs.Enabled = False
            lnkBtnModalFechaNAV.Visible = True
            lnkModalFechaSolicitud.Visible = True
            lnkBtnModalFechaPago.Visible = True
            lnkBtnModalFechaTCObs.Visible = True

            txtModalFechaNAV.Text = ""
            txtModalFechaPago.Text = ""
            txtModalFechaTCObs.Text = ""

            'FECHA NAV
            If FechaNAVFondoRescatable.Length > 1 Then
                Dim estructuraFechas As EstructuraFechasDto
                estructuraFechas = New EstructuraFechasDto
                estructuraFechas = Utiles.splitCharByComma(FechaNAVFondoRescatable)

                If estructuraFechas.DesdeQueFecha.Equals("FechaSolicitud") Then
                    FechaCalculo = FechaSolicitud
                    FechaNAVFondoRescatableINT = estructuraFechas.DiasASumar

                ElseIf estructuraFechas.DesdeQueFecha.Equals("FechaRescate") Then
                    'VALIDA QUE CARGUE 1 FECHA PAGO(RESCATE) PARA EVITAR FECHAS NO EXISTENTES
                    'FECHA PAGO
                    If FechaPagoFondoRescatable.Length > 1 Then
                        TipoFechaAñadirPago = FechaPagoFondoRescatable.Substring(5, 2)
                        FechaCalculo = getFechaParaCalculo(TipoFechaAñadirPago, FechaSolicitud, FechaPago, FechaNAV, FechaTC)

                        FechaPagoFondoRescatableINT = getDiasParaDesplazar(FechaPagoFondoRescatable)
                        'FECHA PAGO DIAS HABILES
                        If FechaCalculo = Nothing Then
                            If TipoFechaAñadirPago = "Na" AndAlso FechaCalculo <> Nothing Then
                                txtModalFechaPago.Text = Utiles.SumaDiasAFechas(ddlModalMonedaPago.Text, FechaNAV, FechaPagoFondoRescatableINT, Constantes.CONST_SOLO_DIAS_HABILES)
                            Else
                                txtModalFechaPago.Text = Utiles.SumaDiasAFechas(ddlModalMonedaPago.Text, FechaSolicitud, FechaPagoFondoRescatableINT, Constantes.CONST_SOLO_DIAS_HABILES)
                            End If

                            'txtModalFechaPago.Text = ""
                            FechaPago = Nothing
                        Else
                            txtModalFechaPago.Text = Utiles.SumaDiasAFechas(ddlModalMonedaPago.Text, FechaCalculo, FechaPagoFondoRescatableINT, Constantes.CONST_SOLO_DIAS_HABILES)
                            'txtModalFechaPago.Text = NegocioRescate.SelectFechaPagoSIRescatable(FechaPagoFondoRescatableINT, FechaCalculo, 0)
                            txtModalFechaPago.Text = CDate(txtModalFechaPago.Text).ToString("dd/MM/yyyy")
                            FechaPago = txtModalFechaPago.Text
                        End If

                    End If
                    FechaCalculo = txtModalFechaPago.Text
                    FechaNAVFondoRescatableINT = estructuraFechas.DiasASumar

                Else
                    FechaCalculo = FechaTC
                    FechaNAVFondoRescatableINT = estructuraFechas.DiasASumar

                End If
                'FECHA NAV DIAS CORRIDOS
                Dim SoloDiasHabiles As Integer
                SoloDiasHabiles = IIf(FondoSerieAct.SoloDiasHabilesFechaNavRescate, Constantes.CONST_SOLO_DIAS_HABILES, Constantes.CONST_SOLO_DIAS_CORRIDOS)
                txtModalFechaNAV.Text = Utiles.SumaDiasAFechas(ddlModalMonedaPago.Text, FechaCalculo, FechaNAVFondoRescatableINT, SoloDiasHabiles)
                'CDate(FechaCalculo.AddDays(FechaNAVFondoRescatableINT).ToString("dd/MM/yyyy"))
                FechaNAV = txtModalFechaNAV.Text
            End If

            'FECHA PAGO
            If FechaPagoFondoRescatable.Length > 1 Then
                TipoFechaAñadirPago = FechaPagoFondoRescatable.Substring(5, 2)

                FechaCalculo = getFechaParaCalculo(TipoFechaAñadirPago, FechaSolicitud, FechaPago, FechaNAV, FechaTC)
                FechaPagoFondoRescatableINT = getDiasParaDesplazar(FechaPagoFondoRescatable)

                'FECHA PAGO DIAS HABILES

                If FechaCalculo = Nothing Then
                    txtModalFechaPago.Text = ""
                    FechaPago = Nothing
                Else
                    txtModalFechaPago.Text = Utiles.SumaDiasAFechas(ddlModalMonedaPago.Text, FechaCalculo, FechaPagoFondoRescatableINT, Constantes.CONST_SOLO_DIAS_HABILES)
                    ' txtModalFechaPago.Text = NegocioRescate.SelectFechaPagoSIRescatable(FechaPagoFondoRescatableINT, FechaCalculo, 0)
                    txtModalFechaPago.Text = CDate(txtModalFechaPago.Text).ToString("dd/MM/yyyy")
                    FechaPago = txtModalFechaPago.Text
                End If

            End If

            'FECHA TIPO CAMBIO

            txtModalFechaTCObs.Text = CalcularFechaTCObservado(FechaTCFondoRescatable, txtModalFechaSolicitud.Text, txtModalFechaNAV.Text, txtModalFechaPago.Text, txtModalFechaTCObs.Text)
            Dim bDiaInhabil As Boolean = (Not Utiles.esFechaHabil(ddlModalMonedaPago.Text, txtModalFechaTCObs.Text) And ddlModalMonedaPago.Text = "USD")
            txtModalFechaTCObs.Text = Utiles.getDiaHabilSiguiente(txtModalFechaTCObs.Text, ddlModalMonedaPago.Text)

            If bDiaInhabil Then
                ShowAlert(CONST_INHABIL_PARA_TC)
            End If
            'txtModalFechaTCObs.Text = CDate(txtModalFechaTCObs.Text).ToString("dd/MM/yyyy")
            'FechaTC = txtModalFechaTCObs.Text

        ElseIf FondoRescatable = "No" Then
            txtModalFechaNAV.Enabled = False
            txtModalFechaPago.Enabled = False
            txtModalFechaTCObs.Enabled = False
            lnkBtnModalFechaNAV.Visible = True
            lnkModalFechaSolicitud.Visible = True
            lnkBtnModalFechaPago.Visible = True
            lnkBtnModalFechaTCObs.Visible = True

            Dim VentanasRescate As VentanasRescateDTO = New VentanasRescateDTO()
            Dim NegocioVentanasRescate As VentanasRescateNegocio = New VentanasRescateNegocio
            Dim VentanasRescateActualizado As VentanasRescateDTO = New VentanasRescateDTO()
            VentanasRescateActualizado = Nothing

            If ddlModalNombreFondo.Text <> "" And ddlModalNemotecnico.Text <> "" And txtModalFechaSolicitud.Text <> "" Then
                VentanasRescate.FN_Nombre_Corto = ddlModalNombreFondo.SelectedValue
                VentanasRescate.FS_Nemotecnico = ddlModalNemotecnico.SelectedValue
                VentanasRescate.RES_Fecha_Solicitud = txtModalFechaSolicitud.Text
                VentanasRescateActualizado = NegocioVentanasRescate.SelectFechasNORescatable(VentanasRescate)
            End If

            If VentanasRescateActualizado IsNot Nothing Then
                txtModalFechaNAV.Text = ""
                txtModalFechaPago.Text = ""
                txtModalFechaNAV.Text = CDate(VentanasRescateActualizado.VTRES_Fecha_NAV.ToString("dd/MM/yyyy"))
                txtModalFechaPago.Text = CDate(VentanasRescateActualizado.VTRES_Fecha_Pago.ToString("dd/MM/yyyy"))
            Else
                txtModalFechaNAV.Text = ""
                txtModalFechaPago.Text = ""
                txtModalFechaTCObs.Text = ""
            End If

            txtModalFechaTCObs.Text = CalcularFechaTCObservado(FechaTCFondoRescatable, txtModalFechaSolicitud.Text, txtModalFechaNAV.Text, txtModalFechaPago.Text, txtModalFechaTCObs.Text)
            Dim bDiaInhabil As Boolean = (Not Utiles.esFechaHabil(ddlModalMonedaPago.Text, txtModalFechaTCObs.Text) And ddlModalMonedaPago.Text = "USD")
            txtModalFechaTCObs.Text = Utiles.getDiaHabilSiguiente(txtModalFechaTCObs.Text, ddlModalMonedaPago.Text)

            If bDiaInhabil Then
                ShowAlert(CONST_INHABIL_PARA_TC)
            End If
        ElseIf FondoRescatable = "N/A" Then
            txtModalFechaNAV.Text = ""
            txtModalFechaPago.Text = ""
            txtModalFechaTCObs.Text = ""

            txtModalFechaNAV.Enabled = False
            txtModalFechaTCObs.Enabled = False
            txtModalFechaPago.Enabled = False
            lnkBtnModalFechaNAV.Visible = True
            lnkModalFechaSolicitud.Visible = True
            lnkBtnModalFechaPago.Visible = True
            lnkBtnModalFechaTCObs.Enabled = True
        Else
            txtModalFechaNAV.Text = ""
            txtModalFechaPago.Text = ""

            txtModalFechaNAV.Enabled = False
            txtModalFechaTCObs.Enabled = False
            txtModalFechaPago.Enabled = False
            lnkBtnModalFechaNAV.Visible = True
            lnkModalFechaSolicitud.Visible = True
            lnkBtnModalFechaPago.Visible = True
            lnkBtnModalFechaTCObs.Enabled = True
        End If

        'TRAER NAV DEPENDE DE LA FIJACION
        Dim ValoresCuota As VcSerieDTO = New VcSerieDTO()
        Dim NegocioValoresCuota As ValoresCuotaNegocio = New ValoresCuotaNegocio
        Dim ValoresCuotaActualizado As VcSerieDTO = New VcSerieDTO()

        Dim txtModalNAVText As Decimal

        If FijacionNAV = "Automático" Then
            ValoresCuota.FsNemotecnico = ddlModalNemotecnico.SelectedValue
            If txtModalFechaNAV.Text = "" Then
                ValoresCuota.Fecha = Nothing
            Else
                ValoresCuota.Fecha = txtModalFechaNAV.Text
            End If
            ValoresCuota.FnRut = ddlModalRutFondo.SelectedValue
            ValoresCuotaActualizado = NegocioValoresCuota.GetValoresCuota(ValoresCuota)
            If ValoresCuotaActualizado IsNot Nothing Then
                If txtModalMonedaSerie.Text.Trim = "USD" Then
                    txtModalNAV.Text = Utiles.formatearNAV(ValoresCuotaActualizado.Valor)  ' String.Format("{0:N4}", ValoresCuotaActualizado.Valor)
                Else
                    txtModalNAV.Text = Utiles.formatearNAV(ValoresCuotaActualizado.Valor) ' String.Format("{0:N4}", ValoresCuotaActualizado.Valor)
                End If

                'TRAER ULTIMA TC DE CLP
                Dim TipoCambio1 As TipoCambioDTO = New TipoCambioDTO()
                Dim NegocioTipoCambio1 As TipoCambioNegocio = New TipoCambioNegocio
                Dim TipoCambioActualizado1 As TipoCambioDTO = New TipoCambioDTO()
                'TipoCambio1.Codigo = "CLP"
                TipoCambio1.Codigo = txtModalMonedaSerie.Text
                Dim valor1 As Decimal
                Dim ListaTC As List(Of TipoCambioDTO) = NegocioTipoCambio1.UltimoTipoCambioPorCodigo(TipoCambio1)
                If txtModalMonedaSerie.Text = "CLP" Then
                    txtModalNAVText = txtModalNAV.Text
                    txtModalNAV_CLP.Text = Utiles.formatearNAVCLP(txtModalNAVText)  'String.Format("{0:N4}", Convert.ToDecimal(txtModalNAVText))
                Else
                    For Each tipos As TipoCambioDTO In ListaTC
                        valor1 = Double.Parse(tipos.Valor)
                        'If valor1.ToString.Contains(",") Then
                        '    valor1 = valor1.ToString.Substring("0", valor1.ToString.IndexOf(","))
                        'End If
                        txtModalNAVText = txtModalNAV.Text
                        txtModalNAV_CLP.Text = Utiles.calcularNAVCLP(tipos.Valor, txtModalNAVText) '   String.Format("{0:N4}", Math.Round(txtModalNAVText * valor1, MidpointRounding.ToEven))
                    Next
                End If

                txtModalFijacionNAV.Text = "Realizado"
            Else
                'TRAE EL ULTIMO VALORES CUOTA POR NEMOTECNICO
                ValoresCuota.FsNemotecnico = ddlModalNemotecnico.SelectedValue

                Dim ListaVc As List(Of VcSerieDTO) = NegocioValoresCuota.UltimoValorCuota(ValoresCuota)

                For Each ValCuota As VcSerieDTO In ListaVc
                    If txtModalMonedaSerie.Text.Trim = "USD" Then
                        txtModalNAV.Text = Utiles.formatearNAV(ValCuota.Valor) ' String.Format("{0:N4}", ValCuota.Valor)
                    Else
                        txtModalNAV.Text = Utiles.formatearNAV(ValCuota.Valor) ' String.Format("{0:N4}", ValCuota.Valor)
                    End If

                    If txtModalMonedaSerie.Text = "CLP" Then
                        txtModalNAVText = txtModalNAV.Text
                        txtModalNAV_CLP.Text = Utiles.formatearNAVCLP(txtModalNAVText) ' String.Format("{0:N4}", Convert.ToDecimal(txtModalNAVText))
                    Else
                        'TRAER ULTIMA TC DE CLP
                        Dim TipoCambio1 As TipoCambioDTO = New TipoCambioDTO()
                        Dim NegocioTipoCambio1 As TipoCambioNegocio = New TipoCambioNegocio
                        Dim TipoCambioActualizado1 As TipoCambioDTO = New TipoCambioDTO()
                        'TipoCambio1.Codigo = "CLP"
                        TipoCambio1.Codigo = txtModalMonedaSerie.Text
                        Dim valor1 As Decimal
                        Dim ListaTC As List(Of TipoCambioDTO) = NegocioTipoCambio1.UltimoTipoCambioPorCodigo(TipoCambio1)
                        If txtModalMonedaSerie.Text = "CLP" Then
                            txtModalNAVText = txtModalNAV.Text
                            txtModalNAV_CLP.Text = Utiles.formatearNAVCLP(txtModalNAVText) ' String.Format("{0:N4}", Convert.ToDecimal(txtModalNAVText))
                        Else
                            For Each tipos As TipoCambioDTO In ListaTC
                                valor1 = Double.Parse(tipos.Valor)

                                txtModalNAVText = txtModalNAV.Text
                                txtModalNAV_CLP.Text = Utiles.calcularNAVCLP(tipos.Valor, txtModalNAVText)  'String.Format("{0:N4}", Math.Round(txtModalNAVText * valor1, MidpointRounding.ToEven))
                            Next
                        End If
                    End If

                Next
                txtModalFijacionNAV.Text = "Pendiente"
            End If

        ElseIf FijacionNAV = "Manual" Then
            ValoresCuota.FsNemotecnico = ddlModalNemotecnico.SelectedValue
            If txtModalFechaNAV.Text = "" Then
                ValoresCuota.Fecha = Nothing
            Else
                ValoresCuota.Fecha = txtModalFechaNAV.Text
            End If
            ValoresCuota.FnRut = ddlModalRutFondo.SelectedValue
            ValoresCuotaActualizado = NegocioValoresCuota.GetValoresCuota(ValoresCuota)

            If ValoresCuotaActualizado IsNot Nothing Then
                If txtModalMonedaSerie.Text.Trim = "USD" Then
                    txtModalNAV.Text = Utiles.formatearNAV(ValoresCuotaActualizado.Valor) '  String.Format("{0:N4}", ValoresCuotaActualizado.Valor)
                Else
                    txtModalNAV.Text = Utiles.formatearNAV(ValoresCuotaActualizado.Valor)  ' String.Format("{0:N4}", ValoresCuotaActualizado.Valor)
                End If

                'TRAER ULTIMA TC DE CLP
                Dim TipoCambio1 As TipoCambioDTO = New TipoCambioDTO()
                Dim NegocioTipoCambio1 As TipoCambioNegocio = New TipoCambioNegocio
                Dim TipoCambioActualizado1 As TipoCambioDTO = New TipoCambioDTO()
                'TipoCambio1.Codigo = "CLP"
                TipoCambio1.Codigo = txtModalMonedaSerie.Text
                Dim ListaTC As List(Of TipoCambioDTO) = NegocioTipoCambio1.UltimoTipoCambioPorCodigo(TipoCambio1)
                If txtModalMonedaSerie.Text = "CLP" Then
                    txtModalNAVText = txtModalNAV.Text
                    txtModalNAV_CLP.Text = Utiles.formatearNAVCLP(txtModalNAVText) '    String.Format("{0:N4}", Convert.ToDecimal(txtModalNAVText))
                Else
                    For Each tipos As TipoCambioDTO In ListaTC
                        txtModalNAVText = txtModalNAV.Text
                        txtModalNAV_CLP.Text = Utiles.calcularNAVCLP(tipos.Valor, txtModalNAVText) ' String.Format("{0:N4}", Math.Round(txtModalNAVText * Double.Parse(tipos.Valor), MidpointRounding.ToEven))
                    Next
                End If

                txtModalFijacionNAV.Text = "Pendiente"
            Else
                'TRAE EL ULTIMO VALORES CUOTA POR NEMOTECNICO
                ValoresCuota.FsNemotecnico = ddlModalNemotecnico.SelectedValue

                Dim ListaVc As List(Of VcSerieDTO) = NegocioValoresCuota.UltimoValorCuota(ValoresCuota)

                For Each ValCuota As VcSerieDTO In ListaVc
                    If txtModalMonedaSerie.Text.Trim = "USD" Then
                        txtModalNAV.Text = Utiles.formatearNAV(ValCuota.Valor)  ' String.Format("{0:N2}", ValCuota.Valor)
                    Else
                        txtModalNAV.Text = Utiles.formatearNAV(ValCuota.Valor)    'String.Format("{0:N4}", ValCuota.Valor)
                    End If

                    If txtModalMonedaSerie.Text = "CLP" Then
                        txtModalNAVText = txtModalNAV.Text
                        txtModalNAV_CLP.Text = Utiles.formatearNAVCLP(txtModalNAVText)     'String.Format("{0:N4}", Convert.ToDecimal(txtModalNAVText))
                    Else
                        'TRAER ULTIMA TC DE CLP
                        Dim TipoCambio1 As TipoCambioDTO = New TipoCambioDTO()
                        Dim NegocioTipoCambio1 As TipoCambioNegocio = New TipoCambioNegocio
                        Dim TipoCambioActualizado1 As TipoCambioDTO = New TipoCambioDTO()
                        'TipoCambio1.Codigo = "CLP"
                        TipoCambio1.Codigo = txtModalMonedaSerie.Text
                        Dim valor1 As Decimal
                        Dim ListaTC As List(Of TipoCambioDTO) = NegocioTipoCambio1.UltimoTipoCambioPorCodigo(TipoCambio1)
                        If txtModalMonedaSerie.Text = "CLP" Then
                            txtModalNAVText = txtModalNAV.Text
                            txtModalNAV_CLP.Text = Utiles.formatearNAVCLP(txtModalNAVText)  '   String.Format("{0:N4}", Convert.ToDecimal(txtModalNAVText))
                        Else
                            For Each tipos As TipoCambioDTO In ListaTC
                                valor1 = Double.Parse(tipos.Valor)
                                txtModalNAVText = txtModalNAV.Text
                                txtModalNAV_CLP.Text = Utiles.formatearNAVCLP(txtModalNAVText)  'String.Format("{0:N4}", Math.Round(txtModalNAVText * valor1, MidpointRounding.ToEven))
                            Next
                        End If
                    End If

                Next
                txtModalFijacionNAV.Text = "Pendiente"
            End If
        End If

        TraerYFijarTCObservado()


        cargaRescatesEnTransito()
        cargaSuscripcionesEnTransito()
        cargaCanjesEnTransito()

        'CARGA TOTAL DISPONIBLES
        txtModalDisponiblesCuotasDisponibles.Text = Utiles.SetToCapitalizedNumber(txtModalCuotasDVC.Text - txtModalRescates.Text + txtModalSuscripciones.Text + txtModalCanje.Text)

        'Reset de valores si FondoRescatable es 'N/A'
        resetValores(FondoRescatable)


    End Sub

    Private Function CalcularFechaTCObservado(FechaTCFondoRescatable As String, FechaSolicitud As String, FechaNAV As String, FechaPago As String, FechaTC As String) As String
        Dim NegocioRescate As RescateNegocio = New RescateNegocio
        Dim FechaCalculo As Date
        Dim TipoFechaAñadirTC As String
        Dim FechaPagoFondoRescatableINT As Integer

        If FechaTCFondoRescatable.Length > 1 Then
            TipoFechaAñadirTC = FechaTCFondoRescatable.Substring(5, 2)

            If TipoFechaAñadirTC = "So" AndAlso FechaSolicitud <> "" Then
                FechaCalculo = FechaSolicitud
                FechaPagoFondoRescatableINT = getDiasParaDesplazar(FechaTCFondoRescatable)

            ElseIf TipoFechaAñadirTC = "Re" AndAlso FechaPago <> "" Then
                FechaCalculo = FechaPago
                FechaPagoFondoRescatableINT = getDiasParaDesplazar(FechaTCFondoRescatable)

            ElseIf TipoFechaAñadirTC = "Na" AndAlso FechaNAV <> "" Then
                FechaCalculo = FechaNAV
                FechaPagoFondoRescatableINT = getDiasParaDesplazar(FechaTCFondoRescatable)

            Else
                FechaCalculo = IIf(FechaTC = "", Nothing, FechaTC)
                FechaPagoFondoRescatableINT = getDiasParaDesplazar(FechaTCFondoRescatable)

            End If
        Else
            FechaCalculo = IIf(FechaSolicitud = "", Nothing, FechaSolicitud)
            FechaPagoFondoRescatableINT = 0
        End If

        Dim strFecha As DateTime
        strFecha = Utiles.SumaDiasAFechas(ddlModalMonedaPago.Text, FechaCalculo, FechaPagoFondoRescatableINT, Constantes.CONST_SOLO_DIAS_CORRIDOS)
        'strFecha = NegocioRescate.SelectFechaPagoSIRescatable(FechaPagoFondoRescatableINT, FechaCalculo, 0)

        If strFecha = Nothing Then
            Return ""
        Else
            Return strFecha.ToString("dd/MM/yyyy")
        End If

    End Function

#Region "BOTON LIMPIAR FORMULARIO"
    Protected Sub btnLimpiarFrm_Click(sender As Object, e As EventArgs)
        txtAccionHidden.Value = ""
        DataInitial()
        Me.GrvTabla.DataSource = Nothing
        GrvTabla.DataBind()
    End Sub
#End Region

#Region "BOTON EXPORTAR"
    Protected Sub BtnExportar_Click(sender As Object, e As EventArgs) Handles BtnExportar.Click
        Dim Rescate As RescatesDTO = New RescatesDTO()
        Dim negocio As RescateNegocio = New RescateNegocio

        Dim FechaDesdeSolicitud As Nullable(Of DateTime)
        Dim FechaHastaSolicitud As Nullable(Of DateTime)

        Dim FechaDesdeNAV As Nullable(Of DateTime)
        Dim FechaHastaNAV As Nullable(Of DateTime)

        Dim FechaDesdePago As Nullable(Of DateTime)
        Dim FechaHastaPago As Nullable(Of DateTime)
        Dim mensaje As String

        If ddlAportanteBuscar.SelectedValue.Trim() = Nothing Then
            Rescate.AP_Razon_Social = Nothing
        Else
            Dim arrCadena As String() = ddlAportanteBuscar.SelectedItem.Text().Split(New Char() {"/"c})
            Rescate.AP_Razon_Social = arrCadena(1).Trim()
        End If

        If ddlNombreFondoBuscar.SelectedValue.Trim() = Nothing Then
            Rescate.FN_Nombre_Corto = Nothing
        Else
            Dim arrCadena As String() = ddlNombreFondoBuscar.SelectedItem.Text().Split(New Char() {"/"c})
            Rescate.FN_Nombre_Corto = arrCadena(1).Trim()
        End If

        If ddlNemotecnicoBuscar.SelectedValue.Trim() = Nothing Then
            Rescate.FS_Nemotecnico = Nothing
        Else
            Rescate.FS_Nemotecnico = ddlNemotecnicoBuscar.SelectedValue
        End If

        If ddlEstadoBuscar.SelectedValue.Trim() = Nothing Then
            Rescate.RES_Estado = Nothing
        Else
            Rescate.RES_Estado = ddlEstadoBuscar.SelectedValue
        End If


        If Not txtFechaSolicitudDesde.Text.Equals("") Then
            Rescate.RES_Fecha_Solicitud = Date.Parse(txtFechaSolicitudDesde.Text)
            FechaDesdeSolicitud = Date.Parse(txtFechaSolicitudDesde.Text)
        Else
            Rescate.RES_Fecha_Solicitud = Nothing
        End If

        If Not txtFechaSolicitudHasta.Text.Equals("") Then
            FechaHastaSolicitud = Date.Parse(txtFechaSolicitudHasta.Text)
        Else
            FechaHastaSolicitud = Nothing
        End If

        If Not txtFechaNAVDesde.Text.Equals("") Then
            Rescate.RES_Fecha_Nav = Date.Parse(txtFechaNAVDesde.Text)
            FechaDesdeNAV = Date.Parse(txtFechaNAVDesde.Text)
        Else
            Rescate.RES_Fecha_Nav = Nothing
        End If

        If Not txtFechaSolicitudHasta.Text.Equals("") Then
            FechaHastaNAV = Date.Parse(txtFechaNAVHasta.Text)
        Else
            FechaHastaNAV = Nothing
        End If

        If Not txtFechaPagoDesde.Text.Equals("") Then
            Rescate.RES_Fecha_Pago = Date.Parse(txtFechaPagoDesde.Text)
            FechaDesdePago = Date.Parse(txtFechaPagoDesde.Text)
        Else
            Rescate.RES_Fecha_Pago = Nothing
        End If

        If Not txtFechaPagoHasta.Text.Equals("") Then
            FechaHastaPago = Date.Parse(txtFechaPagoHasta.Text)
        Else
            FechaHastaPago = Nothing
        End If



        If ddlAportanteBuscar.SelectedValue.Trim() = Nothing And txtFechaSolicitudDesde.Text.Equals("") And ddlNombreFondoBuscar.SelectedValue.Trim() = Nothing And txtFechaSolicitudHasta.Text.Equals("") And ddlNemotecnicoBuscar.SelectedValue.Trim() = Nothing And txtFechaNAVDesde.Text.Equals("") And ddlEstadoBuscar.SelectedValue.Trim() = Nothing And txtFechaNAVHasta.Text.Equals("") And txtFechaPagoDesde.Text.Equals("") And txtFechaPagoHasta.Text.Equals("") Then
            mensaje = negocio.ExportarAExcelTodos(Rescate)
        Else
            mensaje = negocio.ExportarAExcel(Rescate, FechaDesdeSolicitud, FechaHastaSolicitud, FechaDesdeNAV, FechaHastaNAV, FechaDesdePago, FechaHastaPago)
        End If



        If negocio.rutaArchivosExcel IsNot Nothing Then
            Archivo.NavigateUrl = negocio.rutaArchivosExcel
            Archivo.Text = "Bajar Archivo"
        Else
            Archivo.Visible = False
        End If

        txtAccionHidden.Value = "MOSTRAR_DIALOGO"

        ShowMesagges(CONST_TITULO_VALORESCUOTA, mensaje, Constantes.CONST_RUTA_IMG + Constantes.CONST_IMG_LOGO, Constantes.CONST_RUTA_IMG + Constantes.CONST_IMG_INFO, False)
        'ShowAlert(mensaje)

    End Sub
#End Region

#Region "BOTON CANCELAR"
    Private Sub btnModalCancelar_Click(sender As Object, e As EventArgs) Handles btnModalCancelar.Click
        txtAccionHidden.Value = ""
    End Sub


#End Region

    Protected Sub ddlModalMonedaPago_SelectedChange(sender As Object, e As EventArgs) Handles ddlModalMonedaPago.SelectedIndexChanged
        txtModalFechaSolicitud.Text = Request.Form(txtModalFechaSolicitud.UniqueID)
        CargarTodoCuandoCambiaFechaSolicitud()
        CalcularMontos()
    End Sub

    Protected Sub txtModalFechaSolicitud_TextChanged(sender As Object, e As EventArgs) Handles txtModalFechaSolicitud.TextChanged
        txtModalFechaSolicitud.Text = Request.Form(txtModalFechaSolicitud.UniqueID)
        CargarTodoCuandoCambiaFechaSolicitud()
        CalcularMontos()
    End Sub

    Private Sub txtModalFechaNAV_TextChanged(sender As Object, e As EventArgs) Handles txtModalFechaNAV.TextChanged
        txtModalFechaNAV.Text = Request.Form(txtModalFechaNAV.UniqueID)
        CargarNAV()
        CalcularMontos()
    End Sub

    Private Sub txtModalFechaPago_TextChanged(sender As Object, e As EventArgs) Handles txtModalFechaPago.TextChanged
        Dim Negocio As VentanasRescateNegocio = New VentanasRescateNegocio
        Dim FechaValidar As String
        Dim FechaActual = Request.Form(txtModalFechaPago.UniqueID)

        txtModalFechaPago.Text = Request.Form(txtModalFechaPago.UniqueID)
        FechaValidar = Negocio.ValidaDiaHabil(txtModalFechaPago.Text)

        If FechaValidar = "Festivo" Then
            'JC
            txtModalFechaPago.Text = FechaActual
            txtModalFechaPago.Text = ""
            ShowAlert("El día seleccionado es No Hábil")
            Return
        End If

        If FechaValidar = "No_Habil" Then
            'JC
            txtModalFechaPago.Text = FechaActual
            txtModalFechaPago.Text = ""
            ShowAlert("El día seleccionado es No Hábil")
            Return
        End If
    End Sub

    Private Sub txtModalFechaTCObs_TextChanged(sender As Object, e As EventArgs) Handles txtModalFechaTCObs.TextChanged
        Dim Negocio As VentanasRescateNegocio = New VentanasRescateNegocio
        Dim FechaValidar As String
        Dim FechaActual = txtModalFechaTCObs.Text

        txtModalFechaTCObs.Text = Request.Form(txtModalFechaTCObs.UniqueID)
        FechaValidar = Negocio.ValidaDiaHabil(txtModalFechaTCObs.Text)

        If FechaValidar = "Festivo" Then
            'JC
            txtModalFechaTCObs.Text = FechaActual
            txtModalFechaTCObs.Text = ""
            ShowAlert("El día seleccionado es No Hábil")
            Return
        End If

        If FechaValidar = "No_Habil" Then
            'JC
            txtModalFechaTCObs.Text = FechaActual
            txtModalFechaTCObs.Text = ""
            ShowAlert("El día seleccionado es No Hábil")
            Return
        End If

        If FechaValidar <> "No_Habil" And FechaValidar <> "Festivo" Then
            CargarTC()
        End If

        TraerYFijarTCObservado()

    End Sub

    Private Sub getUltimoValorTC(monedaSerie As String)
        'TRAE EL ULTIMO TC DE LA MONEDA DE SERIE
        Dim TipoCambio As TipoCambioDTO = New TipoCambioDTO()
        Dim NegocioTipoCambio As TipoCambioNegocio = New TipoCambioNegocio
        Dim valor As Decimal
        Dim ListaTC As List(Of TipoCambioDTO)

        TipoCambio.Codigo = monedaSerie
        ListaTC = NegocioTipoCambio.UltimoTipoCambioPorCodigo(TipoCambio)

        For Each tipos As TipoCambioDTO In ListaTC
            valor = Double.Parse(tipos.Valor)
            txtModalTCObservado.Text = Utiles.SetToCapitalizedNumber(valor)
        Next

        If TipoCambio.Codigo = "CLP" Then
            txtModalTCObservado.Text = "1"
        End If

    End Sub

    Private Function getDiasParaDesplazar(fechaTipoDeAumento As String) As Integer
        Dim diasEnString As String
        Dim numeroDeDias As Integer

        If fechaTipoDeAumento.LastIndexOf(",") > -1 Then
            diasEnString = Mid(fechaTipoDeAumento, fechaTipoDeAumento.LastIndexOf(",") + 2)
        Else
            diasEnString = ""
        End If

        If diasEnString = "" Then
            numeroDeDias = 0
        Else
            numeroDeDias = CInt(diasEnString)
        End If

        Return numeroDeDias
    End Function

#Region "ACCIONES COMBOS DE BUSQUEDA"
    Protected Sub ddlAportanteBuscar_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlAportanteBuscar.SelectedIndexChanged
        Dim rescate As RescatesDTO = New RescatesDTO()

        rescate = GetRutAportanteParametro(rescate)
        rescate = GetNemotecnicoParametro(rescate)
        rescate = GetRutFondoParametro(rescate)

        If ddlNombreFondoBuscar.SelectedValue.ToString().Trim() = "" Then
            llenarComboFondosBuscar(rescate)
        End If

        If ddlNemotecnicoBuscar.SelectedValue.ToString().Trim() = "" Then
            llenarComboNemotecnicoFiltrado(rescate)
        End If

    End Sub

    Protected Sub ddlNombreFondoBuscar_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlNombreFondoBuscar.SelectedIndexChanged
        Dim rescate As RescatesDTO = New RescatesDTO()

        rescate = GetRutAportanteParametro(rescate)
        rescate = GetNemotecnicoParametro(rescate)
        rescate = GetRutFondoParametro(rescate)

        If ddlNemotecnicoBuscar.SelectedValue.ToString().Trim() = "" Then
            llenarComboNemotecnicoFiltrado(rescate)
        End If

        If (ddlAportanteBuscar.SelectedValue.ToString().Trim() = "") Then
            llenarComboAportantesFiltrado(rescate)
        End If

    End Sub

    Protected Sub ddlNemotecnicoBuscar_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlNemotecnicoBuscar.SelectedIndexChanged
        Dim rescate As RescatesDTO = New RescatesDTO()

        rescate = GetRutAportanteParametro(rescate)
        rescate = GetNemotecnicoParametro(rescate)
        rescate = GetRutFondoParametro(rescate)

        If ddlNombreFondoBuscar.SelectedValue.ToString().Trim() = "" Then
            llenarComboFondosBuscar(rescate)
        End If

        If (ddlAportanteBuscar.SelectedValue.ToString().Trim() = "") Then
            llenarComboAportantesFiltrado(rescate)
        End If

    End Sub

    Private Sub llenarComboNemotecnicoFiltrado(rescate As RescatesDTO)
        Dim listaRescates As List(Of RescatesDTO)
        Dim Rescatesvacia As New RescatesDTO

        Dim negocioRescate As RescateNegocio = New RescateNegocio

        listaRescates = negocioRescate.CargarFiltroNemotecnico(rescate)

        ddlNemotecnicoBuscar.Items.Clear()

        If listaRescates.Count = 0 Then
            listaRescates.Add(Rescatesvacia)
            ddlNemotecnicoBuscar.Items.Insert(0, New ListItem(0, String.Empty))
        Else
            listaRescates.Insert(0, Rescatesvacia)
            ddlNemotecnicoBuscar.DataSource = listaRescates
            ddlNemotecnicoBuscar.DataMember = "NemotecnicoBusqueda"
            ddlNemotecnicoBuscar.DataValueField = "NemotecnicoBusqueda"
            ddlNemotecnicoBuscar.DataBind()
            ddlNemotecnicoBuscar.Items.Insert(0, New ListItem(0, String.Empty))
        End If
    End Sub

    Private Sub llenarComboAportantesFiltrado(rescate As RescatesDTO)
        Dim Negocio As RescateNegocio = New RescateNegocio

        Dim Rescatesvacia As New RescatesDTO
        Dim listaRescates As List(Of RescatesDTO)

        listaRescates = Negocio.CargarFiltroNombreAportante(rescate)

        ddlAportanteBuscar.Items.Clear()

        If listaRescates.Count = 0 Then
            listaRescates.Add(Rescatesvacia)
        Else
            listaRescates.Insert(0, Rescatesvacia)
        End If

        ddlAportanteBuscar.DataSource = listaRescates

        ddlAportanteBuscar.DataMember = "RutRazonSocialAportante"
        ddlAportanteBuscar.DataValueField = "RutRazonSocialAportante"
        ddlAportanteBuscar.DataTextField = "RutRazonSocialAportante"
        ddlAportanteBuscar.DataBind()

    End Sub

    Private Sub llenarComboFondosBuscar(rescate As RescatesDTO)
        Dim Negocio As RescateNegocio = New RescateNegocio
        Dim Rescatesvacia As New RescatesDTO
        Dim listaRescates As List(Of RescatesDTO)

        listaRescates = Negocio.CargarFiltroNombreFondo(rescate)

        ddlNombreFondoBuscar.Items.Clear()

        If listaRescates.Count = 0 Then
            listaRescates.Add(Rescatesvacia)
        Else
            listaRescates.Insert(0, Rescatesvacia)
        End If

        ddlNombreFondoBuscar.DataSource = listaRescates

        ddlNombreFondoBuscar.DataMember = "RutRNombreCortoFondo"
        ddlNombreFondoBuscar.DataValueField = "RutRNombreCortoFondo"
        ddlNombreFondoBuscar.DataTextField = "RutRNombreCortoFondo"
        ddlNombreFondoBuscar.DataBind()
    End Sub

    Private Function GetRutAportanteParametro(rescate As RescatesDTO) As RescatesDTO
        Dim nombreAportante As String
        Dim rutAportante As String
        Dim arrCadena As String()

        If (ddlAportanteBuscar.SelectedValue.ToString().Trim() <> "") Then

            arrCadena = ddlAportanteBuscar.SelectedItem.Text().Split(New Char() {"/"c})

            If arrCadena.Length = 2 Then
                rutAportante = arrCadena(0).Trim()
                nombreAportante = arrCadena(1).Trim()

                rescate.AP_RUT = rutAportante
            End If
        End If
        Return rescate
    End Function

    Private Function GetNemotecnicoParametro(ByRef rescate As RescatesDTO) As RescatesDTO
        If ddlNemotecnicoBuscar.SelectedValue.ToString().Trim() <> "" Then
            rescate.FS_Nemotecnico = ddlNemotecnicoBuscar.SelectedValue.ToString().Trim()
        End If
        Return rescate
    End Function

    Private Function GetRutFondoParametro(ByRef rescate As RescatesDTO) As RescatesDTO
        Dim nombreFondo As String
        Dim rutFondo As String
        Dim arrCadena As String()

        If ddlNombreFondoBuscar.SelectedValue.ToString().Trim() <> "" Then
            arrCadena = ddlNombreFondoBuscar.SelectedItem.Text().Split(New Char() {"/"c})

            If arrCadena.Length = 2 Then
                rutFondo = arrCadena(0).Trim()
                nombreFondo = arrCadena(1).Trim()

                rescate.FN_RUT = rutFondo
            End If

        End If

        Return rescate
    End Function


#End Region

    Private Sub GenerarPopUp()
        Dim Rescate As RescatesDTO = New RescatesDTO()
        Rescate.RES_ID = txtIDRescate.Text

        Dim negocioMod As RescateNegocio = New RescateNegocio
        FillPopUp(negocioMod.GetRescateOne(Rescate))

        txtAccionHidden.Value = "POPUPRESCATE"
    End Sub

    Private Sub FillPopUp(Rescate As RescatesDTO)
        lblPopUpFechaSolicitud.Text = Rescate.RES_Fecha_Solicitud.ToShortDateString
        lblPopUpHoraSolicitud.Text = Rescate.RES_Fecha_Solicitud.ToShortTimeString
        lblPopUpTipo.Text = Rescate.RES_Tipo_Transaccion
        lblPopUpNemoFondo.Text = Rescate.FS_Nemotecnico
        lblPopUpNombreFondo.Text = Rescate.FN_Nombre_Corto
        lblPopUpSerie.Text = Rescate.FS_Nombre_Corto
        lblPopUpAdministradora.Text = "En Validacion con Moneda" 'FN_Razon_Social" 'VALIDAR MONEDA
        lblPopUpRutAdministradora.Text = "En Validacion con Moneda" 'FN_RUT" 'VALIDAR MONEDA
        lblPopUpNombreAportante.Text = Rescate.AP_Razon_Social
        lblPopUpRutAportante.Text = Rescate.AP_RUT
        lblPopUpCuotas.Text = Rescate.RES_Cuotas
        lblPopUpCuotasEnDCV.Text = Rescate.ADCV_Cantidad
        lblPopUpCttoGralDeFondos.Text = Rescate.RES_Contrato
        lblPopUpPoderRegFir.Text = Rescate.RES_Poderes
        lblPopUpMonedaDePago.Text = Rescate.RES_Moneda_Pago
        lblPopUpValorNav.Text = "Por Confirmar"
        If Rescate.RES_Moneda_Pago <> "CLP" Then
            lblPopUpUsdObs.Text = "Por Confirmar"
        Else
            lblPopUpUsdObs.Text = "No Aplica"
        End If
        lblPopUpValorRescate.Text = "Por Confirmar"
        lblPopUpFechaNav.Text = Rescate.RES_Fecha_Nav
        lblPopUpFechaPagoRescate.Text = Rescate.RES_Fecha_Pago
        lblPopUpEjecutado.Text = "PENDIENTE"
    End Sub

    Protected Sub btnProrrotear_Click(sender As Object, e As EventArgs) Handles btnProrrotear.Click

    End Sub
End Class
