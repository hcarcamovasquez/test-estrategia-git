Imports System.Data
Imports System.Reflection
Imports DTO
Imports Negocio
Imports DBSUtils

Partial Class Presentacion_Mantenedores_frmMantenedorCertificado
    Inherits System.Web.UI.Page
    Private listaCarga As Object


    Public Const CONST_TITULO_CERTIFICAD0 As String = "Certificados"
    Public Const CONST_TITULO_MODAL_MODIFICAR As String = "Modificar Certificado"
    Public Const CONST_TITULO_MODAL_ElIMINAR As String = "Eliminar Certificado"
    Public Const CONST_TITULO_MODAL_CREAR As String = "Nuevo Certificado"

    Public Const CONST_MODIFICAR_ERROR As String = "Error al modificar los datos del Certificado"
    Public Const CONST_MODIFICAR_EXITO As String = "Certificado modificado con éxito"
    Public Const CONST_ELIMINAR_ERROR As String = "Error al eliminar el Certificado"
    Public Const CONST_ELIMINAR_EXITO As String = "Certificado eliminado con éxito"
    Public Const CONST_ELIMINAR_EXISTE_EN_OTRA_TABLA As String = "Certificado se encuentra relacionado en otra Tabla"
    Public Const CONST_INSERTAR_ERROR As String = "Error al ingresar el Certificado"
    Public Const CONST_INSERTAR_EXITO As String = "Certificado ingresado con éxito"
    Public Const CONST_ELIMINAR_ESTADO_CERO As String = "No se puede eliminar un registro ya deshabilitado"

    Public Const CONST_VALIDA_RUT_SI_MULTIFONDO_BLANCO As String = "RUT existe en la base de aportantes, para grabar debe llenar el campo Multifondo"
    Public Const CONST_VALIDA_RUT_SI_MULTIFONDO_SI As String = "RUT y Multifondo ya existen en la base de aportantes"

    Public Const CONST_AGREGAR_EXISTE_CERTIFICADO As String = "Certificado ya se encuentra registrado"
    Public Const CONST_AGREGAR_EXISTE_EN_LA_GRILLA As String = "El Certificado ya se encuentra en la lista"

    Public Const CONST_LISTAS_DROPDOWN As String = "Alguna de las listas está vacía"

    Public Const CONST_COL_NUMERO As Integer = 11
    Public Const CONST_COL_CORRELATIVO As Integer = 12
    Public Const CONST_COL_FECHA As Integer = 15
    Public Const CONST_COL_APORTANTE_RUT As Integer = 7
    Public Const CONST_COL_MULTIFONDO As Integer = 9
    Public Const CONST_COL_NEMOTECNICO As Integer = 6
    Public Const CONST_COL_ESTADO As Integer = 16

    Public Const CONST_SIN_RESULTADOS As String = "No se obtuvieron resultados de la búsqueda"
    Public Const CONST_GRILLA_ASIGNACION_VACIA As String = "No hay elementos para almacenar"
    Public Const CONST_ERROR_AL_GUARDAR As String = "Error al [accion] el Certificado"
    Public Const CONST_EXITO_AL_GUARDAR As String = "Certificado [accion] con Exito"

#Region "VALIDA PERMISOS"
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
#End Region

#Region "INICIALIZA Y CARGA CONTROLES DE BUSQUEDA"
    Private Sub Presentacion_MantenedoresfrmMantenedorCertificado_Load(sender As Object, e As EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then
            DataInitial()
            txtAccionHidden.Value = ""
        End If

        ValidaPermisosPerfil()

    End Sub

    Private Sub DataInitial()
        ddlModalIdHito.Enabled = False
        txtFechaCorteBuscar.Text = ""
        txtFechaIngresoDesdeBuscar.Text = ""
        txtFechaCanjeBuscar.Text = ""
        txtFechaIngresoHastaBuscar.Text = ""

        'LLENA LOS COMBOS DE BUSQUEDA NOMBRE APORTANTE Y NOMBRE FONDO
        CargaFiltroNombreFondo()
        CargaFiltroNombreAportante()

        GrvTabla.DataSource = Nothing
        GrvTabla.DataBind()
        Session("lista") = Nothing
    End Sub

    Private Sub CargaFiltroNombreFondo()
        Dim lista As New List(Of CertificadoDTO)
        Dim Certificado As New CertificadoDTO()
        Dim NegocioCertificado As CertificadoNegocio = New CertificadoNegocio
        Dim CertificadoVacio As CertificadoDTO = New CertificadoDTO()

        lista = NegocioCertificado.ConsultarNombreFondo(Certificado)

        If lista.Count = 0 Then
            lista.Add(CertificadoVacio)
        Else
            lista.Insert(0, CertificadoVacio)
        End If

        ddlNombreFondoBuscar.DataSource = lista

        ddlNombreFondoBuscar.DataMember = "RutRNombreCortoFondo"
        ddlNombreFondoBuscar.DataValueField = "RutRNombreCortoFondo"
        ddlNombreFondoBuscar.DataTextField = "RutRNombreCortoFondo"
        ddlNombreFondoBuscar.DataBind()
    End Sub

    Private Sub CargaFiltroNombreAportante()
        Dim lista As New List(Of CertificadoDTO)
        Dim Certificado As New CertificadoDTO()
        Dim NegocioCertificado As CertificadoNegocio = New CertificadoNegocio
        Dim CertificadoVacio As CertificadoDTO = New CertificadoDTO()

        lista = NegocioCertificado.ConsultarNombreAportante(Certificado)

        If lista.Count = 0 Then
            lista.Add(CertificadoVacio)
        Else
            lista.Insert(0, CertificadoVacio)
        End If

        ddlNombreAportanteBuscar.DataSource = lista

        ddlNombreAportanteBuscar.DataMember = "RutRazonSocialAportante"
        ddlNombreAportanteBuscar.DataValueField = "RutRazonSocialAportante"
        ddlNombreAportanteBuscar.DataTextField = "RutRazonSocialAportante"
        ddlNombreAportanteBuscar.DataBind()

    End Sub
#End Region

#Region "BUSQUEDA INICIAL"
    Protected Sub BtnBuscar_Click(sender As Object, e As EventArgs)
        txtAccionHidden.Value = ""

        txtFechaIngresoHastaBuscar.Text = Request.Form(txtFechaIngresoHastaBuscar.UniqueID)
        txtFechaCorteBuscar.Text = Request.Form(txtFechaCorteBuscar.UniqueID)
        txtFechaIngresoDesdeBuscar.Text = Request.Form(txtFechaIngresoDesdeBuscar.UniqueID)
        txtFechaCanjeBuscar.Text = Request.Form(txtFechaCanjeBuscar.UniqueID)


        If txtFechaIngresoHastaBuscar.Text = Nothing And txtFechaCorteBuscar.Text = Nothing And txtFechaIngresoDesdeBuscar.Text = Nothing And txtFechaCanjeBuscar.Text = Nothing And ddlNombreFondoBuscar.SelectedValue.Trim() = Nothing And ddlNombreAportanteBuscar.SelectedValue.Trim() = Nothing Then
            CargarTodosCertificados()

            If GrvTabla.Rows.Count = 0 Then
                BtnExportar.Enabled = False
                ShowAlert(CONST_SIN_RESULTADOS)
            Else
                BtnExportar.Enabled = True
            End If

        Else
            FindCertificados()
            If GrvTabla.Rows.Count = 0 Then
                BtnExportar.Enabled = False
                ShowAlert(CONST_SIN_RESULTADOS)
            Else
                BtnExportar.Enabled = True
            End If

            txtAccionHidden.Value = ""
        End If

    End Sub

    Private Sub CargarTodosCertificados()
        Dim Certificado As CertificadoDTO = New CertificadoDTO()
        Dim negocio As CertificadoNegocio = New CertificadoNegocio

        GrvTabla.DataSource = negocio.ConsultarTodos(Certificado)
        GrvTabla.DataBind()
    End Sub

    Private Sub FindCertificados()
        Dim Certificado As CertificadoDTO = New CertificadoDTO()
        Dim negocio As CertificadoNegocio = New CertificadoNegocio

        Dim FechaDesde As Nullable(Of DateTime)
        Dim FechaHasta As Nullable(Of DateTime)

        If (ddlNombreAportanteBuscar.SelectedIndex > 0) Then
            Dim arrCadena As String() = ddlNombreAportanteBuscar.SelectedItem.Text().Split(New Char() {"/"c})

            Certificado.AP_Rut = arrCadena(0).Trim()
            Certificado.AP_Razon_Social = arrCadena(1).Trim()
        Else
            Certificado.AP_Rut = ""
            Certificado.AP_Razon_Social = ""
        End If

        If (ddlNombreFondoBuscar.SelectedIndex > 0) Then
            Dim arrCadena As String() = ddlNombreFondoBuscar.SelectedItem.Text().Split(New Char() {"/"c})

            Certificado.FN_Rut = arrCadena(0).Trim()
            Certificado.FN_Nombre_Corto = arrCadena(1).Trim()
        Else
            Certificado.FN_Rut = ""
            Certificado.FN_Nombre_Corto = ""
        End If


        If Not txtFechaCorteBuscar.Text.Equals("") Then
            Certificado.HT_Corte = Date.Parse(txtFechaCorteBuscar.Text)
        Else
            Certificado.HT_Corte = Nothing
        End If

        If Not txtFechaCanjeBuscar.Text.Equals("") Then
            Certificado.HT_Canje = Date.Parse(txtFechaCanjeBuscar.Text)
        Else
            Certificado.HT_Canje = Nothing
        End If

        If Not txtFechaIngresoDesdeBuscar.Text.Equals("") Then
            Certificado.CT_Fecha_Ingreso = Date.Parse(txtFechaIngresoDesdeBuscar.Text)
            FechaDesde = Date.Parse(txtFechaIngresoDesdeBuscar.Text)
        Else
            Certificado.CT_Fecha_Ingreso = Nothing
        End If

        If Not txtFechaIngresoHastaBuscar.Text.Equals("") Then
            FechaHasta = Date.Parse(txtFechaIngresoHastaBuscar.Text)
        Else
            FechaHasta = Nothing
        End If

        GrvTabla.DataSource = negocio.GetListaCertificadosConFiltro(Certificado, FechaDesde, FechaHasta)
        GrvTabla.DataBind()

    End Sub
#End Region

#Region "LIMPIAR BUSQUEDA INICIAL"
    Protected Sub btnLimpiarFrm_Click(sender As Object, e As EventArgs)
        txtAccionHidden.Value = ""
        txtFechaCorteBuscar.Text = ""
        txtFechaIngresoDesdeBuscar.Text = ""
        txtFechaCanjeBuscar.Text = ""
        txtFechaIngresoHastaBuscar.Text = ""
        ddlNombreFondoBuscar.SelectedIndex = 0
        ddlNombreAportanteBuscar.SelectedIndex = 0
        GrvTabla.DataSource = Nothing
        GrvTabla.DataBind()
        Session("lista") = Nothing
        BtnExportar.Enabled = False
    End Sub
#End Region


#Region "CARGA MODAL PARA CREAR"
    Protected Sub btnCrear_Click(sender As Object, e As EventArgs)
        txtAccionHidden.Value = "CREAR"

        'LIMPIA LOS CONTROLES Y HABILITA O DESHABILITA BOTONES
        FormateoLimpiarDatosModal()
        FormateoEstiloFormCrear()

        'TRAE EL  NUMERO DEL SIGUIENTE DOCUMENTO A CREAR
        CargaDatosDocumento()

        'CARGA LOS COMBOS DEL MODAL
        CargaHitosModal()
        CargarNombreFondoModal()
        CargarRutFondoModal()
        CargarNemotecnicoModal()
        CargarRutAportanteModal()
        CargarNombreAportanteModal()
        CargarMultifondoModal()

        ClientScript.RegisterStartupScript(Me.GetType(), "myModal", "$('#myModal').modal('show');", True)
    End Sub

    Private Sub FormateoLimpiarDatosModal()
        grvAsignacion.Columns(0).Visible = False
        txtModalFechaCorte.Text = ""
        txtModalFechaCanje.Text = ""
        txtModalCantidad.Text = ""
        grvAsignacion.DataSource = Nothing
        grvAsignacion.DataBind()
        Session("lista") = Nothing
        Session("Coorrelativo") = 1

    End Sub

    Private Sub FormateoEstiloFormCrear()

        btnModalGuardar.Enabled = False
        btnModalGuardar.Visible = True
        btnModalEliminarGrupo.Enabled = False

        btnModalAgregar.Enabled = False
        btnModalAgregar.Visible = True


        grvAsignacion.Columns(0).Visible = False
        btnModalEliminarCertificado.Enabled = False
        btnModalEliminarCertificado.Visible = True
        btnModalModificar.Enabled = False
        btnModalModificar.Visible = True
        btnModalEliminarGrupo.Enabled = False

        lblTitleModalCrud.Text = CONST_TITULO_MODAL_CREAR
        txtModalFechaCorte.Enabled = True
        txtModalFechaCanje.Enabled = True
        txtModalNumeroDocumento.Enabled = True
        ddlModalIdHito.Enabled = False
        ddlModalNombreFondo.Enabled = True
        ddlModalRutFondo.Enabled = True
        ddlModalNemotecnico.Enabled = True
        ddlModalRutAportante.Enabled = True
        ddlModalNombreAportante.Enabled = True
        ddlModalMultifondo.Enabled = True
        txtModalCantidad.Enabled = True


        lblModalTitle.Text = CONST_TITULO_MODAL_CREAR
    End Sub

    Private Sub CargaDatosDocumento()
        Dim negocio As DocumentoNegocio = New DocumentoNegocio
        Dim Documento As New DocumentoDTO
        Dim ValoresDocumentos As DocumentoDTO = negocio.GetDatosDocumento(Documento)

        txtModalNumeroDocumento.Text = ValoresDocumentos.NumeroSiguiente
    End Sub
#End Region


#Region "CARGA COMBOS MODAL TODOS SIN FILTRO"
    Private Sub CargaHitosModal()
        Dim negocioHitos As HitosNegocio = New HitosNegocio
        Dim Hitos As New HitoDTO
        Dim lista As List(Of HitoDTO) = negocioHitos.GetListaporHitos(Hitos)

        If lista.Count = 0 Then
            ddlModalIdHito.Items.Insert(0, New ListItem("", ""))

        Else
            ddlModalIdHito.DataSource = lista
            ddlModalIdHito.DataMember = "IdHito"
            ddlModalIdHito.DataValueField = "IdHito"
            ddlModalIdHito.DataBind()
            ddlModalIdHito.Items.Insert(0, New ListItem("", ""))
        End If
    End Sub



    Private Sub CargarNombreFondoModal()
        Dim NegocioFondo As FondosNegocio = New FondosNegocio
        Dim fondo As FondoDTO = New FondoDTO()

        Dim listafondo As List(Of FondoDTO) = NegocioFondo.GetNombreFondoHitos(fondo)

        If listafondo.Count = 0 Then
            ddlModalNombreFondo.Items.Insert(0, New ListItem("", ""))
            ddlModalNombreFondo.SelectedIndex = 0
        Else

            ddlModalNombreFondo.DataSource = listafondo
            ddlModalNombreFondo.DataMember = "RazonSocial"
            ddlModalNombreFondo.DataValueField = "RazonSocial"
            ddlModalNombreFondo.DataBind()
            ddlModalNombreFondo.Items.Insert(0, New ListItem("", ""))
            ddlModalNombreFondo.SelectedIndex = 0
        End If
    End Sub

    Private Sub CargarRutFondoModal()
        Dim NegocioFondo As FondoSeriesNegocio = New FondoSeriesNegocio
        Dim fondo As FondoSerieDTO = New FondoSerieDTO()

        Dim listafondo As List(Of FondoSerieDTO) = NegocioFondo.GetListaFondosRutHitos(fondo)

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

    Public Sub CargarNemotecnicoModal()
        Dim NegocioFondoSerie As FondoSeriesNegocio = New FondoSeriesNegocio
        Dim fondoSeries As FondoSerieDTO = New FondoSerieDTO()
        Dim listafondoSeries As List(Of FondoSerieDTO) = NegocioFondoSerie.CargarDistinctNemotecnicoHitos(fondoSeries)

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

    Private Sub CargarRutAportanteModal()
        Dim NegocioCertificados As CertificadoNegocio = New CertificadoNegocio
        Dim aportante As AportanteDTO = New AportanteDTO()
        Dim aportantes As List(Of AportanteDTO) = NegocioCertificados.SoloRutAportante(aportante)

        If aportantes.Count = 0 Then
            ddlModalRutAportante.Items.Insert(0, New ListItem("", ""))
            ddlModalRutAportante.SelectedIndex = 0
        Else
            ddlModalRutAportante.DataSource = aportantes
            ddlModalRutAportante.DataMember = "Rut"
            ddlModalRutAportante.DataValueField = "Rut"
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
        Dim NegocioAportante As CanjeNegocio = New CanjeNegocio
        Dim aportante As AportanteDTO = New AportanteDTO
        Dim aportantes As List(Of AportanteDTO) = NegocioAportante.ConsultarMultifondo(aportante)

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
#End Region

#Region "CARGA FECHAS CUANDO CAMBIA COMBO HITO"
    Public Sub CargaFechasHitosPorIDHitoModal()
        Dim negocio As HitosNegocio = New HitosNegocio
        Dim HitoSelect As New HitoDTO
        HitoSelect.IdHito = ddlModalIdHito.SelectedValue
        Dim ValoresFechasHitos As HitoDTO = negocio.GetFechasParaCertificados(HitoSelect)

        'LLENA LOS TEXTOS DE FECHA CORTE Y FECHA CANJE DE HITOS POR ID HITO
        txtModalFechaCorte.Text = ValoresFechasHitos.FechaCorte
        txtModalFechaCanje.Text = ValoresFechasHitos.FechaCanje


    End Sub
#End Region


#Region "CARGA NOMBRE FONDO Y NEMOTECNICO CUANDO CAMBIA COMBO RUT FONDO"
    Public Sub CargarNombreFondoNemotecnicoPorRutFondoModal()
        'CARGA NOMBRE FONDO Y NEMOTECNICO POR RUT FONDO
        CargarNombreFondoPorRutFondoModal()
        CargarNemotecnicoPorRutFondoModal()
    End Sub

    Public Sub CargarNombreFondoPorRutFondoModal()
        Dim fechahasta As Nullable(Of Date)
        Dim NegocioFondo As FondosNegocio = New FondosNegocio
        Dim fondo As FondoDTO = New FondoDTO()

        fondo.Rut = ddlModalRutFondo.SelectedValue

        Dim listafondo As List(Of FondoDTO) = NegocioFondo.GetListaFondosConFiltro(fondo, fechahasta)

        If listafondo.Count = 0 Then
            ddlModalNombreFondo.Items.Insert(0, New ListItem("", ""))
            ddlModalNombreFondo.SelectedIndex = 0
        Else

            ddlModalNombreFondo.DataSource = listafondo
            ddlModalNombreFondo.DataMember = "RazonSocial"
            ddlModalNombreFondo.DataValueField = "RazonSocial"
            ddlModalNombreFondo.DataBind()
        End If


        Dim NegocioFondoSerie As FondoSeriesNegocio = New FondoSeriesNegocio
        Dim fondoSeries As FondoSerieDTO = New FondoSerieDTO()
        fondoSeries.Rut = ddlModalRutFondo.SelectedValue
        Dim listafondoSeries As List(Of FondoSerieDTO) = NegocioFondoSerie.GrupoSeriesPorRut(fondoSeries)

        If listafondoSeries.Count = 0 Then
            ddlModalNemotecnico.DataSource = Nothing
            ddlModalNemotecnico.Items.Insert(0, New ListItem("", ""))
            ddlModalNemotecnico.SelectedIndex = 0
        Else
            ddlModalNemotecnico.DataSource = listafondoSeries
            ddlModalNemotecnico.DataMember = "Nemotecnico"
            ddlModalNemotecnico.DataValueField = "Nemotecnico"
            ddlModalNemotecnico.DataBind()
        End If

        Dim negocioHito As HitosNegocio = New HitosNegocio
        Dim hito As HitoDTO = New HitoDTO
        hito.Rut = ddlModalRutFondo.SelectedValue
        Dim lista As List(Of HitoDTO) = negocioHito.ConsultaUltimosParaCertificados(hito)

        If lista.Count = 0 Then
            ddlModalIdHito.DataSource = Nothing
            ddlModalIdHito.Items.Insert(0, New ListItem("", ""))
            ddlModalIdHito.SelectedIndex = 0
            ddlModalIdHito.Enabled = False
        ElseIf lista.Count > 1 Then
            ddlModalIdHito.Enabled = True
            ddlModalIdHito.DataSource = lista
            ddlModalIdHito.DataMember = "IdHito"
            ddlModalIdHito.DataValueField = "IdHito"
            ddlModalIdHito.DataBind()



            Dim negocio As HitosNegocio = New HitosNegocio
            Dim HitoSelect As New HitoDTO
            HitoSelect.IdHito = ddlModalIdHito.SelectedValue
            Dim ValoresFechasHitos As HitoDTO = negocio.GetFechasParaCertificados(HitoSelect)
            'LLENA LOS TEXTOS DE FECHA CORTE Y FECHA CANJE DE HITOS POR ID HITO
            txtModalFechaCorte.Text = ValoresFechasHitos.FechaCorte
            txtModalFechaCanje.Text = ValoresFechasHitos.FechaCanje


        Else
            ddlModalIdHito.Enabled = True
            ddlModalIdHito.DataSource = lista
            ddlModalIdHito.DataMember = "IdHito"
            ddlModalIdHito.DataValueField = "IdHito"
            ddlModalIdHito.DataBind()

            Dim negocio As HitosNegocio = New HitosNegocio
            Dim HitoSelect As New HitoDTO
            HitoSelect.IdHito = ddlModalIdHito.SelectedValue
            Dim ValoresFechasHitos As HitoDTO = negocio.GetFechasParaCertificados(HitoSelect)

            'LLENA LOS TEXTOS DE FECHA CORTE Y FECHA CANJE DE HITOS POR ID HITO
            txtModalFechaCorte.Text = ValoresFechasHitos.FechaCorte
            txtModalFechaCanje.Text = ValoresFechasHitos.FechaCanje
        End If

    End Sub

    Public Sub CargarNemotecnicoPorRutFondoModal()

        Dim NegocioFondoSerie As FondoSeriesNegocio = New FondoSeriesNegocio
        Dim fondoSeries As FondoSerieDTO = New FondoSerieDTO()
        fondoSeries.Nemotecnico = ddlModalNemotecnico.SelectedValue
        Dim listafondoSeries As List(Of FondoSerieDTO) = NegocioFondoSerie.GrupoSeriesPorNemotecnico(fondoSeries)

        If listafondoSeries.Count = 0 Then
            ddlModalRutFondo.Items.Insert(0, New ListItem("", ""))
            ddlModalRutFondo.SelectedIndex = 0
        Else
            ddlModalRutFondo.DataSource = listafondoSeries
            ddlModalRutFondo.DataMember = "Rut"
            ddlModalRutFondo.DataValueField = "Rut"
            ddlModalRutFondo.DataBind()
        End If

        Dim fechahasta As Nullable(Of Date)
        Dim NegocioFondo As FondosNegocio = New FondosNegocio
        Dim fondo As FondoDTO = New FondoDTO()

        fondo.Rut = ddlModalRutFondo.SelectedValue

        Dim listafondo As List(Of FondoDTO) = NegocioFondo.GetListaFondosConFiltro(fondo, fechahasta)

        If listafondo.Count = 0 Then
            ddlModalNombreFondo.Items.Insert(0, New ListItem("", ""))
            ddlModalNombreFondo.SelectedIndex = 0
        Else

            ddlModalNombreFondo.DataSource = listafondo
            ddlModalNombreFondo.DataMember = "RazonSocial"
            ddlModalNombreFondo.DataValueField = "RazonSocial"
            ddlModalNombreFondo.SelectedIndex = 0
            ddlModalNombreFondo.DataBind()
        End If


        Dim negocioHito As HitosNegocio = New HitosNegocio
        Dim hito As HitoDTO = New HitoDTO
        hito.Rut = ddlModalRutFondo.SelectedValue
        Dim lista As List(Of HitoDTO) = negocioHito.ConsultaUltimosParaCertificados(hito)

        If ddlModalIdHito.Enabled = True Or ddlModalIdHito.Text = "" Then
            If lista.Count = 0 Then
                ddlModalIdHito.DataSource = Nothing
                ddlModalIdHito.Items.Insert(0, New ListItem("", ""))
                ddlModalIdHito.SelectedIndex = 0
                ddlModalIdHito.Enabled = False
            Else
                ddlModalIdHito.Enabled = True
                ddlModalIdHito.DataSource = lista
                ddlModalIdHito.DataMember = "IdHito"
                ddlModalIdHito.DataValueField = "IdHito"
                ddlModalIdHito.DataBind()

                Dim negocio As HitosNegocio = New HitosNegocio
                Dim HitoSelect As New HitoDTO
                HitoSelect.IdHito = ddlModalIdHito.SelectedValue
                Dim ValoresFechasHitos As HitoDTO = negocio.GetFechasParaCertificados(HitoSelect)

                'LLENA LOS TEXTOS DE FECHA CORTE Y FECHA CANJE DE HITOS POR ID HITO
                txtModalFechaCorte.Text = ValoresFechasHitos.FechaCorte
                txtModalFechaCanje.Text = ValoresFechasHitos.FechaCanje
            End If
        End If


    End Sub
#End Region


#Region "CARGA RUT FONDO  CUANDO CAMBIA COMBO NOMBRE FONDO"
    Public Sub CargarRutFondoPorNombreFondoModal()
        Dim fechahasta As Nullable(Of Date)
        Dim NegocioFondo As FondosNegocio = New FondosNegocio
        Dim fondo As FondoDTO = New FondoDTO()

        fondo.RazonSocial = ddlModalNombreFondo.SelectedValue

        Dim listafondo As List(Of FondoDTO) = NegocioFondo.GetListaFondosConFiltro(fondo, fechahasta)

        If listafondo.Count = 0 Then
            ddlModalRutFondo.Items.Insert(0, New ListItem("", ""))
            ddlModalRutFondo.SelectedIndex = 0
        Else

            ddlModalRutFondo.DataSource = listafondo
            ddlModalRutFondo.DataMember = "rut"
            ddlModalRutFondo.DataValueField = "rut"
            ddlModalRutFondo.DataBind()
        End If


        Dim NegocioFondoSerie As FondoSeriesNegocio = New FondoSeriesNegocio
        Dim fondoSeries As FondoSerieDTO = New FondoSerieDTO()
        fondoSeries.Rut = ddlModalRutFondo.SelectedValue
        Dim listafondoSeries As List(Of FondoSerieDTO) = NegocioFondoSerie.GrupoSeriesPorRut(fondoSeries)

        If listafondoSeries.Count = 0 Then
            ddlModalNemotecnico.DataSource = Nothing
            ddlModalNemotecnico.Items.Insert(0, New ListItem("", ""))
            ddlModalNemotecnico.SelectedIndex = 0
        Else
            ddlModalNemotecnico.DataSource = listafondoSeries
            ddlModalNemotecnico.DataMember = "Nemotecnico"
            ddlModalNemotecnico.DataValueField = "Nemotecnico"
            ddlModalNemotecnico.DataBind()
        End If

        'If ddlModalIdHito.SelectedValue = "" Then
        Dim negocioHito As HitosNegocio = New HitosNegocio
            Dim hito As HitoDTO = New HitoDTO
            hito.Rut = ddlModalRutFondo.SelectedValue
            Dim lista As List(Of HitoDTO) = negocioHito.ConsultaUltimosParaCertificados(hito)

            If lista.Count = 0 Then
                ddlModalIdHito.DataSource = Nothing
                ddlModalIdHito.Items.Insert(0, New ListItem("", ""))
                ddlModalIdHito.SelectedIndex = 0
                ddlModalIdHito.Enabled = False
            Else
                ddlModalIdHito.Enabled = True
                ddlModalIdHito.DataSource = lista
                ddlModalIdHito.DataMember = "IdHito"
                ddlModalIdHito.DataValueField = "IdHito"
                ddlModalIdHito.DataBind()

                Dim negocio As HitosNegocio = New HitosNegocio
                Dim HitoSelect As New HitoDTO
                HitoSelect.IdHito = ddlModalIdHito.SelectedValue
                Dim ValoresFechasHitos As HitoDTO = negocio.GetFechasParaCertificados(HitoSelect)

                'LLENA LOS TEXTOS DE FECHA CORTE Y FECHA CANJE DE HITOS POR ID HITO
                txtModalFechaCorte.Text = ValoresFechasHitos.FechaCorte
                txtModalFechaCanje.Text = ValoresFechasHitos.FechaCanje
            End If
        'End If
    End Sub
#End Region

#Region "CARGA RUT FONDO Y NOMBRE FONDO CUANDO CAMBIA COMBO NEMOTECNICO"
    Public Sub CargarNombreFondoRutFondoPorModalNemotecnico()
        If txtAccionHidden.Value = "MODIFICAR" Then
            txtAccionModificar.Text = "ESTAMODIFICANDO"
        End If

        'CAPTURA FILA SELECCIONADA
        Dim filaseleccionada As String
        filaseleccionada = ""

        For Each mRow As GridViewRow In grvAsignacion.Rows
            Dim mCheck As RadioButton = CType(mRow.FindControl("RowSelector"), RadioButton)
            If mCheck.Checked = True Then
                filaseleccionada = mRow.ClientID
            End If

        Next
        'CARGA RUT FONDO Y NOMBRE FONDO  POR NEMOTECNICO
        CargarNemotecnicoPorRutFondoModal()
        CargarNombreFondoPorRutFondoModal()

        'RECARGA LA FILA SELECCIONADA
        For Each mRow As GridViewRow In grvAsignacion.Rows
            If mRow.ClientID = filaseleccionada Then
                Dim mCheck As RadioButton = CType(mRow.FindControl("RowSelector"), RadioButton)
                mCheck.Checked = True
            End If

        Next
    End Sub

    Public Sub CargarRutFondoPorNemotecnicoModal()
        Dim NegocioFondoSerie As FondoSeriesNegocio = New FondoSeriesNegocio
        Dim fondoSeries As FondoSerieDTO = New FondoSerieDTO()
        Dim fondo As FondoDTO = New FondoDTO()

        fondoSeries.Nemotecnico = ddlModalNemotecnico.SelectedValue
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
        Dim NegocioFondoSerie As FondoSeriesNegocio = New FondoSeriesNegocio
        Dim fondoSeries As FondoSerieDTO = New FondoSerieDTO()
        Dim fondo As FondoDTO = New FondoDTO()

        fondoSeries.Nemotecnico = ddlModalNemotecnico.SelectedValue
        Dim listafondoSeries As List(Of FondoDTO) = NegocioFondoSerie.GrupoSeriesTraerNombre(fondo, fondoSeries)

        If listafondoSeries.Count = 0 Then
            ddlModalNombreFondo.Items.Insert(0, New ListItem("", ""))
        Else
            ddlModalNombreFondo.DataSource = listafondoSeries
            ddlModalNombreFondo.DataMember = "nombreCorto"
            ddlModalNombreFondo.DataValueField = "nombreCorto"
            ddlModalNombreFondo.DataBind()
        End If
    End Sub
#End Region

#Region "CARGA NOMBRE APORTANTE Y MULTIFONDO  CUANDO CAMBIA COMBO RUT APORTANTE"
    Public Sub CargarNombreAportanteNemotecnicoPorRutAportanteModal()
        'CARGA NOMBRE APORTANTE Y MULTIFONDO POR RUT APORTANTE
        CargarNombreAportantePorRutAportanteModal()
        CargarMultifondoPorRutAportanteModal()
    End Sub

    Public Sub CargarNombreAportantePorRutAportanteModal()
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
            ddlModalNombreAportante.SelectedIndex = 0

            For Each multifondos As AportanteDTO In aportantes
                Dim vacio As String

                vacio = multifondos.Multifondo
                If vacio = "" Then
                    aportantes.Clear()
                    ddlModalMultifondo.DataSource = aportantes
                    ddlModalMultifondo.DataMember = "multifondo"
                    ddlModalMultifondo.DataValueField = "multifondo"
                    ddlModalMultifondo.DataBind()
                    ddlModalMultifondo.Enabled = False
                    Exit For
                Else
                    ddlModalMultifondo.Enabled = True
                    ddlModalMultifondo.DataSource = aportantes
                    ddlModalMultifondo.DataMember = "multifondo"
                    ddlModalMultifondo.DataValueField = "multifondo"
                    ddlModalMultifondo.DataBind()
                    ddlModalMultifondo.SelectedIndex = 0
                End If
            Next


        End If
    End Sub

    Public Sub CargarMultifondoPorRutAportanteModal()
        Dim fechahasta As Nullable(Of Date)
        Dim NegocioAportante As AportanteNegocio = New AportanteNegocio
        Dim aportante As AportanteDTO = New AportanteDTO()

        'aportante.Rut = ddlModalRutAportante.SelectedValue
        aportante.RazonSocial = ddlModalNombreAportante.SelectedValue

        Dim aportantes As List(Of AportanteDTO) = NegocioAportante.GetListaAportantes(aportante, fechahasta)

        If aportantes.Count = 0 Then
            ddlModalMultifondo.Items.Insert(0, New ListItem("", ""))
            ddlModalMultifondo.Enabled = False
        Else

            ddlModalRutAportante.DataSource = aportantes
            ddlModalRutAportante.DataMember = "Rut"
            ddlModalRutAportante.DataValueField = "Rut"
            ddlModalRutAportante.DataBind()
            ddlModalRutAportante.SelectedIndex = 0
            For Each multifondos As AportanteDTO In aportantes
                Dim vacio As String

                vacio = multifondos.Multifondo
                If vacio = "" Then
                    aportantes.Clear()
                    ddlModalMultifondo.DataSource = aportantes
                    ddlModalMultifondo.DataMember = "multifondo"
                    ddlModalMultifondo.DataValueField = "multifondo"
                    ddlModalMultifondo.DataBind()
                    ddlModalMultifondo.Enabled = False
                    Exit For
                Else
                    ddlModalMultifondo.Enabled = True
                    ddlModalMultifondo.DataSource = aportantes
                    ddlModalMultifondo.DataMember = "multifondo"
                    ddlModalMultifondo.DataValueField = "multifondo"
                    ddlModalMultifondo.DataBind()
                End If
            Next
        End If
    End Sub

#End Region

#Region "CARGA RUT APORTANTE Y MULTIFONDO  CUANDO CAMBIA COMBO  NOMBRE APORTANTE"
    Public Sub CargarRutAportanteNemotecnicoPorNombreAportanteModal()
        'CARGA NOMBRE APORTANTE Y MULTIFONDO POR RUT APORTANTE
        CargarRutAportantePorNombreAportanteModal()
        CargarMultifondoPorNombreAportanteModal()
    End Sub

    Public Sub CargarRutAportantePorNombreAportanteModal()
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

            ddlModalNombreAportante.DataSource = aportantes
            ddlModalNombreAportante.DataMember = "razonSocial"
            ddlModalNombreAportante.DataValueField = "razonSocial"
            ddlModalNombreAportante.DataBind()
            ddlModalNombreAportante.SelectedIndex = 0
        End If
    End Sub

    Private Sub CargarMultifondoPorNombreAportanteModal()
        Dim fechahasta As Nullable(Of Date)
        Dim NegocioAportante As AportanteNegocio = New AportanteNegocio
        Dim aportante As AportanteDTO = New AportanteDTO()

        aportante.RazonSocial = ddlModalNombreAportante.SelectedValue

        Dim aportantes As List(Of AportanteDTO) = NegocioAportante.GetListaAportantes(aportante, fechahasta)

        If aportantes.Count = 0 Then
            ddlModalMultifondo.Items.Insert(0, New ListItem("", ""))
            ddlModalMultifondo.Enabled = False
        Else
            ddlModalMultifondo.DataSource = aportantes
            ddlModalMultifondo.DataMember = "multifondo"
            ddlModalMultifondo.DataValueField = "multifondo"
            ddlModalMultifondo.DataBind()

            If ddlModalMultifondo.Text = "" Then
                ddlModalMultifondo.Enabled = False
            Else
                ddlModalMultifondo.Enabled = True
            End If
        End If
    End Sub

#End Region

#Region "AGREGA A LA GRILLA DE ASIGNACION DOCUMENTOS"
    Private Sub btnModalAgregar_Click(sender As Object, e As EventArgs) Handles btnModalAgregar.Click

        Dim lista As New List(Of CertificadoDTO)
        Dim Certificado As New CertificadoDTO
        Dim negocio As CertificadoNegocio = New CertificadoNegocio
        Dim UltimoCorrelativo As Integer

        If Session("lista") IsNot Nothing Then
            lista = Session("lista")
        Else
            lista = New List(Of CertificadoDTO)
        End If

        If ddlModalIdHito.SelectedValue <> "" And ddlModalNombreFondo.SelectedValue <> "" And ddlModalRutFondo.SelectedValue <> "" And ddlModalNemotecnico.SelectedValue <> "" And ddlModalNombreAportante.SelectedValue <> "" Then
            If txtModalCantidad.Text.Trim <> "" Then

                If txtAccionHidden.Value = "CREAR" Then
                    Certificado.HT_ID = ddlModalIdHito.SelectedValue
                    Certificado.HT_Corte = txtModalFechaCanje.Text
                    Certificado.HT_Canje = txtModalFechaCorte.Text
                    Certificado.CT_Numero = txtModalNumeroDocumento.Text
                    Certificado.CT_Correlativo = Session("Coorrelativo")
                    Session("Coorrelativo") = Session("Coorrelativo") + 1
                    Certificado.CT_Fecha = Date.Now

                ElseIf txtAccionHidden.Value = "MODIFICAR" Then
                    Dim correlativo As Integer
                    correlativo = txtModalVariableCorrelativo.Text

                    Dim Fecha As Date
                    Fecha = txtModalVariableFecha.Text

                    Certificado.HT_ID = ddlModalIdHito.SelectedValue
                    Certificado.HT_Corte = txtModalFechaCanje.Text
                    Certificado.HT_Canje = txtModalFechaCorte.Text
                    Certificado.CT_Numero = txtModalNumeroDocumento.Text
                    UltimoCorrelativo = negocio.ConsultarUltimoCorrelativo(Certificado)
                    Session("ContadorCorrelativos") = Session("ContadorCorrelativos") + 1

                    Certificado.CT_Correlativo = UltimoCorrelativo + Session("ContadorCorrelativos")
                End If

                Certificado.AP_Rut = ddlModalRutAportante.SelectedValue
                Certificado.AP_Razon_Social = ddlModalNombreAportante.SelectedValue
                Certificado.AP_Multifondo = ddlModalMultifondo.SelectedValue
                Certificado.FS_Nemotecnico = ddlModalNemotecnico.SelectedValue
                Certificado.CT_Cuotas = txtModalCantidad.Text
                Certificado.CT_Estado = Constantes.CONST_HABILITADO
                Certificado.CT_Fecha_Ingreso = Date.Now
                Certificado.CT_Usuario_Ingreso = Session("NombreUsuario")
                Certificado.HT_Corte = txtModalFechaCorte.Text
                Certificado.HT_Canje = txtModalFechaCanje.Text
                Certificado.FN_Rut = ddlModalRutFondo.SelectedValue
                Certificado.FN_Nombre_Corto = ddlModalNombreFondo.SelectedValue

                If txtAccionHidden.Value = "CREAR" Then
                    CargarNombreFondoNemotecnicoPorRutFondoModal()
                End If

                Dim existeCertificado As Boolean = negocio.ExisteCertificado(Certificado)

                If Not existeCertificado Then
                    Dim existeEnGrilla As Boolean = ExisteEnGrillaAsignacion(lista, Certificado)


                    lista.Add(Certificado)
                    Session("lista") = lista

                    'Solo Agregar Nuevos
                    Dim listaAgregar As New List(Of CertificadoDTO)

                    If Session("listaAgregado") Is Nothing Then
                        listaAgregar.Add(Certificado)
                        Session("listaAgregado") = listaAgregar
                    Else
                        listaAgregar = Session("listaAgregado")
                        listaAgregar.Add(Certificado)
                        Session("listaAgregado") = listaAgregar
                    End If


                    grvAsignacion.DataSource = lista
                    grvAsignacion.DataBind()
                    ComprobarCountListaAsignacion()


                Else
                    ShowAlert(CONST_AGREGAR_EXISTE_CERTIFICADO)
                End If
            Else
                ShowAlert("El Campo cantidad de Cuotas NO puede estar vacío")
            End If


        ElseIf txtModalCantidad.Text.Trim = "" Then
            ShowAlert("El Campo cantidad de Cuotas NO puede estar vacío")

        Else
            'TODO Buscar otro metodo que muestre emnsajes de alerta sin que cierre el modal
            ShowAlert(CONST_LISTAS_DROPDOWN)
        End If

        ddlModalIdHito.Enabled = False
        ddlModalNombreFondo.Enabled = False
        ddlModalRutFondo.Enabled = False
        ddlModalRutAportante.Enabled = False
        ddlModalNombreAportante.Enabled = False
        ddlModalMultifondo.Enabled = False
        txtModalCantidad.Text = ""
        ddlModalNemotecnico.SelectedIndex = 0
        'ClientScript.RegisterStartupScript(Me.GetType(), "myModal", "$('#myModal').modal('show');", True)
    End Sub

    Private Function ExisteEnGrillaAsignacion(lista As List(Of CertificadoDTO), Certificado As CertificadoDTO) As Boolean
        Dim findCertificado As CertificadoDTO = lista.Find(Function(l) l.CT_Numero = Certificado.CT_Numero And l.CT_Correlativo = Certificado.CT_Correlativo And l.CT_Fecha = Certificado.CT_Fecha And l.AP_Rut = Certificado.AP_Rut And l.AP_Multifondo = Certificado.AP_Multifondo And l.FS_Nemotecnico = Certificado.FS_Nemotecnico)
        Return (findCertificado IsNot Nothing)

    End Function

    Private Sub ComprobarCountListaAsignacion()
        If Session("lista") IsNot Nothing And txtAccionHidden.Value = "CREAR" Then
            'btnModalAgregar.Enabled = True

            ddlModalIdHito.Enabled = False
            ddlModalNombreFondo.Enabled = False
            ddlModalRutFondo.Enabled = False
            ddlModalRutAportante.Enabled = False
            ddlModalNombreAportante.Enabled = False
            ddlModalMultifondo.Enabled = False
            txtModalCantidad.Text = ""
            ddlModalNemotecnico.SelectedIndex = 0
        End If
    End Sub
#End Region

#Region "GUARDA-MODIFICA Y ELIMINA CERTIFICADOS MODAL"
    Protected Sub btnModalGuardar_Click(sender As Object, e As EventArgs) Handles btnModalGuardar.Click
        If grvAsignacion.Rows.Count = 0 Then
            ShowAlert("No hay elementos para almacenar")
            Exit Sub
        End If

        Dim listaCertificados As List(Of CertificadoDTO) = New List(Of CertificadoDTO)
        Dim listaCertificadosAgregados As List(Of CertificadoDTO) = New List(Of CertificadoDTO)
        Dim listaCertificadosModificados As List(Of CertificadoDTO) = New List(Of CertificadoDTO)
        Dim listaCertificadosEliminados As List(Of CertificadoDTO) = New List(Of CertificadoDTO)
        Dim Certificado As CertificadoDTO = New CertificadoDTO()
        Dim negocio As CertificadoNegocio = New CertificadoNegocio

        If Session("lista") IsNot Nothing Then
            listaCertificados = Session("lista")
        End If
        If Session("listaAgregado") IsNot Nothing Then
            listaCertificadosAgregados = Session("listaAgregado")
        End If
        If Session("listaModificado") IsNot Nothing Then
            listaCertificadosModificados = Session("listaModificado")
        End If
        If Session("listaEliminar") IsNot Nothing Then
            listaCertificadosEliminados = Session("listaEliminar")
        End If

        Certificado.CT_Numero = txtModalNumeroDocumento.Text
        Certificado.CT_Estado = Constantes.CONST_HABILITADO
        Certificado.CT_Usuario_Ingreso = Session("NombreUsuario")
        Certificado.CT_Usuario_Modificacion = Session("NombreUsuario")

        Dim accion As String
        Dim mensajeAccionExito As String
        Dim mensajeAccionError As String

        If txtAccionHidden.Value = "CREAR" Then
            accion = "AGREGAR_CERTIFICADO"

            'ACTUALIZA DOCUMENTO
            Dim negocioMod As DocumentoNegocio = New DocumentoNegocio
            Dim documento As DocumentoDTO = GetValoresDocumentoModal()

            Dim solicitudModDocumento As Integer = negocioMod.UpdateDocumento(documento)
            mensajeAccionExito = CONST_INSERTAR_EXITO.Replace("[accion]", "insertado")
            mensajeAccionError = CONST_INSERTAR_ERROR.Replace("[accion]", "insertar")


        ElseIf txtAccionHidden.Value = "MODIFICAR" Then
            accion = "MODIFICAR_CERTIFICADOS"
            mensajeAccionExito = CONST_MODIFICAR_EXITO.Replace("[accion]", "modificado")
            mensajeAccionError = CONST_MODIFICAR_ERROR.Replace("[accion]", "modificar")

        ElseIf txtAccionHidden.Value = "ELIMINAR" Then
            accion = "ELIMINAR_CERTIFICADOS"
            mensajeAccionExito = CONST_ELIMINAR_EXITO.Replace("[accion]", "eliminado")
            mensajeAccionError = CONST_ELIMINAR_ERROR.Replace("[accion]", "eliminar")

        Else
            txtAccionHidden.Value = ""
            Exit Sub
        End If

        Dim NumeroCertificado As Integer
        Dim NumeroCoorrelativo As Integer
        Dim Nemotecnico As String
        Dim Cantidad As Decimal
        Dim ID_Hito As Decimal
        Dim FechaCorte As Date
        Dim FechaCanje As Date
        Dim Fecha As Date
        Dim RutAportante As String
        Dim NombreAportante As String
        Dim RutFondo As String
        Dim NombreFondo As String
        Dim Multifondo As String

        Dim negocioModif As CertificadoNegocio = New CertificadoNegocio
        Dim solicitudModificarEliminar As Integer

        If accion = "AGREGAR_CERTIFICADO" Then
            listaCertificadosAgregados = Nothing
            listaCertificadosModificados = Nothing
            listaCertificadosEliminados = Nothing
            If (negocio.GuardarCertificados(accion, listaCertificados, listaCertificadosAgregados, listaCertificadosModificados, listaCertificadosEliminados)) Then

                If txtAccionHidden.Value = "CREAR" Then
                    ShowAlert(CONST_INSERTAR_EXITO)
                End If
            Else
                'ShowMesagges(CONST_TITULO_CERTIFICAD0, mensajeAccionError, Constantes.CONST_RUTA_IMG + Constantes.CONST_IMG_LOGO, Constantes.CONST_RUTA_IMG + Constantes.CONST_IMG_ERROR)
                If txtAccionHidden.Value = "CREAR" Then
                    ShowAlert(CONST_INSERTAR_ERROR)
                End If

            End If
        ElseIf accion = "MODIFICAR_CERTIFICADOS" Then
            Dim CertificadoModif As CertificadoDTO = New CertificadoDTO()
            Dim negocioCertif As CertificadoNegocio = New CertificadoNegocio
            For Each row As GridViewRow In grvAsignacion.Rows
                NumeroCertificado = row.Cells(1).Text.Trim()
                NumeroCoorrelativo = row.Cells(2).Text.Trim()
                ID_Hito = row.Cells(3).Text.Trim()
                FechaCorte = row.Cells(4).Text.Trim()
                FechaCanje = row.Cells(5).Text.Trim()
                Fecha = row.Cells(6).Text.Trim()
                RutAportante = row.Cells(7).Text.Trim()
                NombreAportante = row.Cells(8).Text.Trim()
                RutFondo = row.Cells(9).Text.Trim()
                NombreFondo = row.Cells(10).Text.Trim()
                Multifondo = row.Cells(11).Text.Trim()
                Nemotecnico = row.Cells(12).Text.Trim()
                Cantidad = row.Cells(13).Text.Trim()


                CertificadoModif.CT_Numero = NumeroCertificado
                CertificadoModif.CT_Correlativo = NumeroCoorrelativo
                CertificadoModif.HT_ID = ID_Hito
                CertificadoModif.HT_Corte = FechaCorte
                CertificadoModif.HT_Canje = FechaCanje
                CertificadoModif.CT_Fecha = Fecha
                CertificadoModif.AP_Rut = RutAportante
                CertificadoModif.AP_Razon_Social = NombreAportante
                CertificadoModif.FN_Rut = RutFondo
                CertificadoModif.FN_Nombre_Corto = NombreFondo
                CertificadoModif.AP_Multifondo = Multifondo
                CertificadoModif.FS_Nemotecnico = Nemotecnico
                CertificadoModif.CT_Cuotas = Cantidad
                CertificadoModif.CT_Usuario_Ingreso = Certificado.CT_Usuario_Ingreso
                CertificadoModif.CT_Usuario_Modificacion = Certificado.CT_Usuario_Modificacion

                'INSERTA LA TEMPORAL PARA SU POSTERIOR COMPARACION

                negocioCertif.IngresarTemporalModificados(CertificadoModif)
            Next

            'COMPARA LA DATA CON LA TEMPORAL Y/O ACTUALIZA AGREGA Y/O ELIMINA
            CertificadoModif.CT_Numero = NumeroCertificado
            solicitudModificarEliminar = negocioModif.ModificarEliminarCertificados(CertificadoModif)

            txtAccionHidden.Value = "MOSTRAR_DIALOGO"

            If solicitudModificarEliminar = Constantes.CONST_OPERACION_EXITOSA Then
                'Ingresado con Exito
                ShowAlert(CONST_MODIFICAR_EXITO)
            Else
                'Error en la BBDD)
                ShowAlert(CONST_MODIFICAR_ERROR)
            End If

        ElseIf accion = "ELIMINAR_CERTIFICADOS" Then
            listaCertificados = Nothing
            listaCertificadosModificados = Nothing
            listaCertificadosAgregados = Nothing
            If (negocio.GuardarCertificados(accion, listaCertificados, listaCertificadosAgregados, listaCertificadosModificados, listaCertificadosEliminados)) Then

                If txtAccionHidden.Value = "ELIMINAR" Then
                    ShowAlert("Registro eliminado con éxito")
                End If

            Else
                'ShowMesagges(CONST_TITULO_CERTIFICAD0, mensajeAccionError, Constantes.CONST_RUTA_IMG + Constantes.CONST_IMG_LOGO, Constantes.CONST_RUTA_IMG + Constantes.CONST_IMG_ERROR)
                If txtAccionHidden.Value = "ELIMINAR" Then
                    ShowAlert(CONST_ELIMINAR_ERROR)
                End If
            End If
        End If


        CargaFiltroNombreFondo()
        CargaFiltroNombreAportante()
        txtAccionHidden.Value = ""
        txtFechaCorteBuscar.Text = ""
        txtFechaIngresoDesdeBuscar.Text = ""
        txtFechaCanjeBuscar.Text = ""
        txtFechaIngresoHastaBuscar.Text = ""
        ddlNombreFondoBuscar.SelectedIndex = 0
        ddlNombreAportanteBuscar.SelectedIndex = 0
        GrvTabla.DataSource = Nothing
        GrvTabla.DataBind()
        Session("lista") = Nothing
        BtnExportar.Enabled = False
    End Sub

    Private Function GetValoresDocumentoModal() As DocumentoDTO
        Dim Documento As DocumentoDTO = New DocumentoDTO()

        Documento.NumeroActual = 1
        Documento.NumeroAnterior = 1
        Documento.NumeroSiguiente = 1

        Return Documento
    End Function
#End Region

#Region "AGREGA A LA MODAL DOCUMENTOS A MODIFICAR"
    Protected Sub BtnModificar_Click(sender As Object, e As EventArgs)
        Session("listaModificado") = Nothing
        Session("listaAgregado") = Nothing
        Session("ContadorCorrelativos") = 0
        grvAsignacion.Columns(0).Visible = True
        Dim negocio As CertificadoNegocio = New CertificadoNegocio
        Dim CertificadoSelect As CertificadoDTO = GetCertificadoSelect()
        Dim CertificadoActualizado As CertificadoDTO = negocio.GetDocumento(CertificadoSelect)

        FormateoFormDatos(CertificadoActualizado)
        FormateoEstiloFormModificar()

        ddlModalIdHito.Enabled = False
        ddlModalNombreAportante.Enabled = False
        ddlModalRutAportante.Enabled = False
        ddlModalMultifondo.Enabled = False
        ddlModalRutFondo.Enabled = False
        ddlModalNombreFondo.Enabled = False
        ddlModalNemotecnico.Enabled = True
        txtModalCantidad.Enabled = True

        txtAccionHidden.Value = "MODIFICAR"
        Session("CRUD") = "MODIFICAR"


    End Sub

    Private Function GetCertificadoSelect() As CertificadoDTO
        Dim Certificado As New CertificadoDTO

        For Each row As GridViewRow In GrvTabla.Rows
            Dim chk As RadioButton = row.Cells(0).Controls(1)
            If chk IsNot Nothing And chk.Checked Then


                Session("correlativo_modificar") = row.Cells(CONST_COL_CORRELATIVO).Text.Trim()
                Session("fecha_modificar") = row.Cells(CONST_COL_FECHA).Text.Trim()


                Certificado.HT_ID = row.Cells(1).Text.Trim()
                Certificado.HT_Corte = row.Cells(2).Text.Trim()
                Certificado.HT_Canje = row.Cells(3).Text.Trim()
                Certificado.FN_Rut = row.Cells(4).Text.Trim()
                Certificado.FN_Nombre_Corto = HttpUtility.HtmlDecode(row.Cells(5).Text.Trim())
                Certificado.FS_Nemotecnico = row.Cells(6).Text.Trim()
                Certificado.AP_Rut = row.Cells(7).Text.Trim()
                Certificado.AP_Razon_Social = HttpUtility.HtmlDecode(row.Cells(8).Text.Trim())
                Certificado.AP_Multifondo = row.Cells(9).Text.Trim()

                If Certificado.AP_Multifondo = "&nbsp;" Then
                    Certificado.AP_Multifondo = ""
                End If

                Certificado.CT_Cuotas = row.Cells(10).Text.Trim()
                Certificado.CT_Numero = row.Cells(11).Text.Trim()
                Certificado.CT_Correlativo = row.Cells(12).Text.Trim()
                Certificado.CT_Fecha_Ingreso = row.Cells(13).Text.Trim()
            End If
        Next

        Return Certificado
    End Function

    Private Sub FormateoFormDatos(Certificado As CertificadoDTO)
        Dim negocio As CertificadoNegocio = New CertificadoNegocio
        Dim fondo As FondoDTO = New FondoDTO()

        If txtAccionHidden.Value = "MODIFICAR" Then
            ddlModalNemotecnico.Enabled = True
            txtModalCantidad.Enabled = True
        ElseIf txtAccionHidden.Value = "ELIMINAR" Then
            ddlModalNemotecnico.Enabled = False
            txtModalCantidad.Enabled = False
        End If

        txtModalFechaCorte.Text = ""
        txtModalFechaCanje.Text = ""
        txtModalCantidad.Text = 0

        grvAsignacion.DataSource = Nothing
        grvAsignacion.DataBind()
        Session("lista") = Nothing

        fondo = Utiles.GetFondo(Certificado.FN_Rut)

        CargaHitosModal()
        CargarNombreFondoModal()
        CargarRutFondoModal()
        CargarNemotecnicoModal()
        CargarRutAportanteModal()
        CargarNombreAportanteModal()
        CargarMultifondoModal()

        ddlModalIdHito.SelectedValue = Certificado.HT_ID
        txtModalFechaCorte.Text = Certificado.HT_Corte
        txtModalFechaCanje.Text = Certificado.HT_Canje
        txtModalNumeroDocumento.Text = Certificado.CT_Numero

        'ddlModalNombreFondo.Text = fondo.RazonSocial ' Certificado.FN_Nombre_Corto
        ddlModalNombreFondo.Text = Certificado.FN_Nombre_Corto

        ddlModalRutFondo.Text = Certificado.FN_Rut

        If Certificado.FS_Nemotecnico = "&nbsp;" Then
            ddlModalNemotecnico.Text = ""
        Else
            ddlModalNemotecnico.SelectedValue = Certificado.FS_Nemotecnico
        End If

        ddlModalRutAportante.Text = Certificado.AP_Rut
        ddlModalNombreAportante.Text = Certificado.AP_Razon_Social

        If Certificado.AP_Multifondo = "&nbsp;" Then
            ddlModalMultifondo.Text = ""
        Else
            ddlModalMultifondo.Text = Certificado.AP_Multifondo
        End If

        txtModalCantidad.Text = String.Format("{0:N0}", Certificado.CT_Cuotas)
        txtModalVariableDocumento.Text = Certificado.CT_Numero
        txtModalVariableCorrelativo.Text = Certificado.CT_Correlativo
        txtModalVariableFecha.Text = Date.Now
        'txtModalVariableUsuarioIngreso.Text = Certificado.CT_Usuario_Ingreso
        txtModalVariableFechaIngreso.Text = Certificado.CT_Fecha_Ingreso
        'txtModalVariableEstado.Text = Certificado.CT_Estado

        Dim lista As List(Of CertificadoDTO) = negocio.ConsultarPorDocumento(Certificado)
        grvAsignacion.DataSource = lista
        grvAsignacion.DataBind()

        Session("lista") = lista
        Session("listaEliminar") = Nothing

        Dim NegocioFondoSerie As FondoSeriesNegocio = New FondoSeriesNegocio
        Dim fondoSeries As FondoSerieDTO = New FondoSerieDTO()
        fondoSeries.Rut = Certificado.FN_Rut
        Dim listafondoSeries As List(Of FondoSerieDTO) = NegocioFondoSerie.GrupoSeriesPorRut(fondoSeries)

        If listafondoSeries.Count = 0 Then
            ddlModalNemotecnico.Items.Insert(0, New ListItem("", ""))
            ddlModalNemotecnico.SelectedIndex = 0
        Else
            ddlModalNemotecnico.DataSource = listafondoSeries
            ddlModalNemotecnico.DataMember = "Nemotecnico"
            ddlModalNemotecnico.DataValueField = "Nemotecnico"
            ddlModalNemotecnico.SelectedIndex = -1
            ddlModalNemotecnico.DataBind()
        End If

    End Sub

    Private Sub FormateoEstiloFormModificar()
        btnModalGuardar.Enabled = False
        btnModalGuardar.Visible = True
        btnModalEliminarGrupo.Enabled = False

        btnModalAgregar.Enabled = True
        btnModalAgregar.Visible = True
        btnModalModificar.Enabled = False
        btnModalModificar.Visible = True
        btnModalEliminarCertificado.Enabled = False
        btnModalEliminarCertificado.Visible = True



        lblModalTitle.Text = CONST_TITULO_MODAL_MODIFICAR
    End Sub
#End Region

#Region "AGREGA A LA MODAL DOCUMENTOS A MODIFICAR"
    Private Sub btnModalModificar_Click(sender As Object, e As EventArgs) Handles btnModalModificar.Click

        txtAccionHidden.Value = "MODIFICAR"
        Dim agregadoCorrectamente As Boolean
        Dim BandCheck As Integer

        BandCheck = 0

        For Each row As GridViewRow In grvAsignacion.Rows
            Dim chk As RadioButton = row.Cells(0).Controls(1)
            If chk IsNot Nothing And chk.Checked Then
                BandCheck = 1
            End If
        Next

        If BandCheck = 1 Then
            Dim aCertificadoAnterior As CertificadoDTO = GetCertificadosSelectModificar(grvAsignacion)

            txtModalVariableCorrelativo.Text = aCertificadoAnterior.CT_Correlativo

            agregadoCorrectamente = AgregarElementoGridViewAsignacion()

            If (agregadoCorrectamente) Then
                EliminarElementoGridViewAsignacion(aCertificadoAnterior)
            End If
        Else
            ShowAlert("Debe seleccionar un elemento en la lista para modificar")
            Return
        End If


    End Sub

    Private Function GetCertificadosSelectModificar(tabla As GridView) As CertificadoDTO
        Dim certificado As New CertificadoDTO
        For Each row As GridViewRow In tabla.Rows
            Dim chk As RadioButton = row.Cells(0).Controls(1)
            If chk IsNot Nothing And chk.Checked Then
                certificado.CT_Numero = row.Cells(1).Text.Trim()
                certificado.CT_Correlativo = row.Cells(2).Text.Trim()
                certificado.HT_ID = row.Cells(3).Text.Trim()
                certificado.FN_Rut = row.Cells(9).Text.Trim()
                certificado.FS_Nemotecnico = row.Cells(12).Text.Trim()

            End If
        Next

        Return certificado
    End Function

    Private Function AgregarElementoGridViewAsignacion() As Boolean
        Dim lista As New List(Of CertificadoDTO)
        Dim listaModificado As New List(Of CertificadoDTO)
        Dim certificado As New CertificadoDTO
        Dim resultadoAgregar As Boolean = False

        If Session("lista") IsNot Nothing Then
            lista = Session("lista")
        Else
            lista = New List(Of CertificadoDTO)
        End If

        If ddlModalNemotecnico.SelectedIndex > -1 Then
            If txtModalCantidad.Text IsNot "" Then
                certificado.CT_Cuotas = txtModalCantidad.Text
            Else
                ShowAlert("Campo Cantidad no puede ser vacío")
                Exit Function
            End If

            certificado.CT_Numero = txtModalNumeroDocumento.Text
            certificado.CT_Correlativo = txtModalVariableCorrelativo.Text
            certificado.HT_ID = ddlModalIdHito.Text
            certificado.HT_Corte = txtModalFechaCorte.Text
            certificado.HT_Canje = txtModalFechaCanje.Text
            certificado.CT_Fecha = Date.Now
            certificado.AP_Rut = ddlModalRutAportante.SelectedValue
            certificado.AP_Razon_Social = ddlModalNombreAportante.SelectedValue
            certificado.FN_Rut = ddlModalRutFondo.SelectedValue
            certificado.FN_Nombre_Corto = ddlModalNombreFondo.SelectedValue
            certificado.AP_Multifondo = ddlModalMultifondo.SelectedValue
            certificado.FS_Nemotecnico = ddlModalNemotecnico.SelectedValue
            certificado.CT_Usuario_Modificacion = Session("NombreUsuario")
            certificado.CT_Fecha_Modificacion = Date.Now
            certificado.CT_Fecha_Ingreso = txtModalVariableFechaIngreso.Text

            lista.Add(certificado)
            Session("lista") = lista


            'Solo elementos Modificados
            Dim listaModificar As New List(Of CertificadoDTO)

            If Session("listaModificado") Is Nothing Then
                listaModificar.Add(certificado)
                Session("listaModificado") = listaModificar
            Else
                listaModificar = Session("listaModificado")
                listaModificar.Add(certificado)
                Session("listaModificado") = listaModificar
            End If


            grvAsignacion.DataSource = lista
            grvAsignacion.DataBind()

            btnModalGuardar.Enabled = (grvAsignacion.Rows.Count > 0)

            ComprobarCountListaAsignacion()
            resultadoAgregar = True
            btnModalGuardar.Enabled = True

        Else
            ShowAlert(CONST_LISTAS_DROPDOWN)
        End If

        Return resultadoAgregar
    End Function

    Private Sub EliminarElementoGridViewAsignacion(aCertificadoAnterior As CertificadoDTO)
        Dim listaEliminar As New List(Of CertificadoDTO)

        If Session("listaEliminar") IsNot Nothing Then
            listaEliminar = Session("listaEliminar")
        End If


        Dim lista As List(Of CertificadoDTO) = Session("lista")


        Dim objetoEliminar = lista.First(Function(t) t.CT_Numero = aCertificadoAnterior.CT_Numero And t.CT_Correlativo = aCertificadoAnterior.CT_Correlativo And t.HT_ID = aCertificadoAnterior.HT_ID And t.FN_Rut = aCertificadoAnterior.FN_Rut And t.FS_Nemotecnico = aCertificadoAnterior.FS_Nemotecnico)
        lista.Remove(objetoEliminar)

        listaEliminar.Add(objetoEliminar)
        Session("listaEliminar") = listaEliminar

        Session("lista") = lista

        grvAsignacion.DataSource = lista
        grvAsignacion.DataBind()
    End Sub
#End Region

#Region "ELIMINA CERTIFICADO DE GRILLA MODAL"
    Private Sub btnModalEliminarCertificado_Click(sender As Object, e As EventArgs) Handles btnModalEliminarCertificado.Click
        Dim CertificadoAnterior As CertificadoDTO = GetCertificadosSelectModificar(grvAsignacion)
        EliminarElementoGridViewAsignacion(CertificadoAnterior)
    End Sub
#End Region

#Region "BOTON ELIMINAR "
    Protected Sub BtnEliminar_Click(sender As Object, e As EventArgs) Handles BtnEliminar.Click
        txtAccionHidden.Value = "ELIMINAR"
        grvAsignacion.Columns(0).Visible = True

        ddlModalIdHito.Enabled = False
        txtModalFechaCorte.Enabled = False
        txtModalFechaCanje.Enabled = False
        txtModalNumeroDocumento.Enabled = False
        ddlModalRutFondo.Enabled = False
        ddlModalNombreFondo.Enabled = False
        ddlModalNemotecnico.Enabled = False
        ddlModalRutAportante.Enabled = False
        ddlModalNombreAportante.Enabled = False
        ddlModalMultifondo.Enabled = False
        txtModalCantidad.Enabled = False

        grvAsignacion.Columns(0).Visible = True
        Dim negocio As CertificadoNegocio = New CertificadoNegocio
        Dim CertificadoSelect As CertificadoDTO = GetCertificadoSelect()
        Dim CertificadoActualizado As CertificadoDTO = negocio.GetDocumento(CertificadoSelect)

        FormateoFormDatos(CertificadoActualizado)
        FormateoEstiloFormEliminar()



    End Sub

    Private Function GetCertificadosSelect(tabla As GridView) As CertificadoDTO
        Dim certificado As New CertificadoDTO
        For Each row As GridViewRow In tabla.Rows
            Dim chk As RadioButton = row.Cells(0).Controls(1)
            If chk IsNot Nothing And chk.Checked Then
                certificado.CT_Numero = row.Cells(1).Text.Trim()
                certificado.CT_Correlativo = row.Cells(2).Text.Trim()
                certificado.HT_ID = row.Cells(3).Text.Trim()
                certificado.HT_Corte = row.Cells(4).Text.Trim()
                certificado.HT_Canje = row.Cells(5).Text.Trim()
                certificado.CT_Fecha = row.Cells(6).Text.Trim()
                certificado.AP_Rut = row.Cells(7).Text.Trim()
                certificado.AP_Razon_Social = row.Cells(8).Text.Trim()
                certificado.FN_Rut = row.Cells(9).Text.Trim()
                certificado.FN_Nombre_Corto = row.Cells(10).Text.Trim()
                certificado.AP_Multifondo = row.Cells(11).Text.Trim()
                certificado.FS_Nemotecnico = row.Cells(12).Text.Trim()
                certificado.CT_Cuotas = row.Cells(13).Text.Trim()
            End If
        Next

        Return certificado
    End Function

    Private Sub FormateoEstiloFormEliminar()
        btnModalGuardar.Enabled = False
        btnModalGuardar.Visible = True
        btnModalEliminarGrupo.Enabled = True

        btnModalAgregar.Enabled = False
        btnModalAgregar.Visible = True
        btnModalModificar.Enabled = False
        btnModalModificar.Visible = True
        btnModalEliminarCertificado.Enabled = False
        btnModalEliminarCertificado.Visible = True

        lblModalTitle.Text = CONST_TITULO_MODAL_ElIMINAR
    End Sub
#End Region

#Region "ELIMINAR GRUPO CERTIFICADOS POR DOCUMENTO"
    Protected Sub btnModalEliminarGrupo_Click(sender As Object, e As EventArgs) Handles btnModalEliminarGrupo.Click
        Dim negocio As CertificadoNegocio = New CertificadoNegocio
        Dim certificado As CertificadoDTO = New CertificadoDTO
        Dim mensaje As String



        certificado.CT_Numero = txtModalNumeroDocumento.Text

        txtAccionHidden.Value = ""
        If negocio.EliminarTodosCertificados(certificado) = Constantes.CONST_OPERACION_EXITOSA Then
            mensaje = CONST_EXITO_AL_GUARDAR.Replace("[accion]", "eliminado")
            'ShowMesagges(CONST_TITULO_CERTIFICAD0, mensaje, Constantes.CONST_RUTA_IMG + Constantes.CONST_IMG_LOGO, Constantes.CONST_RUTA_IMG + Constantes.CONST_IMG_CORRECTO)
            ShowAlert(CONST_ELIMINAR_EXITO)
        Else
            mensaje = CONST_EXITO_AL_GUARDAR.Replace("[accion]", "eliminado")
            'ShowMesagges(CONST_TITULO_CERTIFICAD0, mensaje, Constantes.CONST_RUTA_IMG + Constantes.CONST_IMG_LOGO, Constantes.CONST_RUTA_IMG + Constantes.CONST_IMG_ERROR)
            ShowAlert(CONST_ELIMINAR_ERROR)
        End If

        txtAccionHidden.Value = ""
        txtFechaCorteBuscar.Text = ""
        txtFechaIngresoDesdeBuscar.Text = ""
        txtFechaCanjeBuscar.Text = ""
        txtFechaIngresoHastaBuscar.Text = ""
        ddlNombreFondoBuscar.SelectedIndex = 0
        ddlNombreAportanteBuscar.SelectedIndex = 0
        GrvTabla.DataSource = Nothing
        GrvTabla.DataBind()
        Session("lista") = Nothing
        BtnExportar.Enabled = False
    End Sub
#End Region

#Region "ALERTAS"
    Private Sub ShowAlert(mesagge As String, Optional mostrarEnPage As Boolean = False)
        Dim myScript As String = "alert('" + mesagge + "');"
        If Not mostrarEnPage Then
            ScriptManager.RegisterStartupScript(UpdatePanelGrilla, UpdatePanelGrilla.GetType(), "alert", myScript, True)
        Else
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", myScript, True)
        End If
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

    Protected Sub CalendarFechaCorteBuscar_SelectionChanged(sender As Object, e As EventArgs) Handles CalendarFechaCorteBuscar.SelectionChanged
        txtFechaCorteBuscar.Text = CalendarFechaCorteBuscar.SelectedDate.ToShortDateString()

        If txtFechaCorteBuscar.Text <> "" And txtFechaCanjeBuscar.Text <> "" Then
            If Date.Parse(txtFechaCanjeBuscar.Text) < Date.Parse(txtFechaCorteBuscar.Text) Then
                txtFechaCanjeBuscar.Text = CalendarFechaCorteBuscar.SelectedDate.ToShortDateString()
            End If
        End If
        CalendarFechaCorteBuscar.SelectedDate = Nothing
        CalendarFechaCorteBuscar.Visible = False
    End Sub


    Protected Sub CalendarFechaIngresoDesdeBuscar_SelectionChanged(sender As Object, e As EventArgs) Handles CalendarFechaIngresoDesdeBuscar.SelectionChanged
        txtFechaIngresoDesdeBuscar.Text = CalendarFechaIngresoDesdeBuscar.SelectedDate.ToShortDateString()

        If txtFechaIngresoDesdeBuscar.Text <> "" And txtFechaIngresoHastaBuscar.Text <> "" Then
            If Date.Parse(txtFechaIngresoHastaBuscar.Text) < Date.Parse(txtFechaIngresoDesdeBuscar.Text) Then
                txtFechaIngresoHastaBuscar.Text = CalendarFechaIngresoDesdeBuscar.SelectedDate.ToShortDateString()
            End If
        End If
        CalendarFechaIngresoDesdeBuscar.SelectedDate = Nothing
        CalendarFechaIngresoDesdeBuscar.Visible = False
    End Sub


    Protected Sub CalendarFechaCanjeBuscar_SelectionChanged(sender As Object, e As EventArgs) Handles CalendarFechaCanjeBuscar.SelectionChanged
        txtFechaCanjeBuscar.Text = CalendarFechaCanjeBuscar.SelectedDate.ToShortDateString()
        If txtFechaCorteBuscar.Text <> "" And txtFechaCanjeBuscar.Text <> "" Then
            If Date.Parse(txtFechaCanjeBuscar.Text) < Date.Parse(txtFechaCorteBuscar.Text) Then
                txtFechaCorteBuscar.Text = CalendarFechaCanjeBuscar.SelectedDate.ToShortDateString()
            End If
        End If
        CalendarFechaCanjeBuscar.SelectedDate = Nothing
        CalendarFechaCanjeBuscar.Visible = False
    End Sub

    Protected Sub lnkBtnFechaIngresoHastaBuscar_Click(sender As Object, e As EventArgs)
        txtAccionHidden.Value = ""
        CalendarFechaIngresoHastaBuscar.Visible = (Not CalendarFechaIngresoHastaBuscar.Visible)
    End Sub

    Protected Sub CalendarFechaIngresoHasta_SelectionChanged(sender As Object, e As EventArgs) Handles CalendarFechaIngresoHastaBuscar.SelectionChanged
        txtFechaIngresoHastaBuscar.Text = CalendarFechaIngresoHastaBuscar.SelectedDate.ToShortDateString()

        If txtFechaIngresoDesdeBuscar.Text <> "" And txtFechaIngresoHastaBuscar.Text <> "" Then
            If Date.Parse(txtFechaIngresoHastaBuscar.Text) < Date.Parse(txtFechaIngresoDesdeBuscar.Text) Then
                txtFechaIngresoDesdeBuscar.Text = CalendarFechaIngresoHastaBuscar.SelectedDate.ToShortDateString()
            End If
        End If
        CalendarFechaIngresoHastaBuscar.SelectedDate = Nothing
        CalendarFechaIngresoHastaBuscar.Visible = False
    End Sub

    Protected Sub CalendarFechaIngresoHasta_DayRender(sender As Object, e As DayRenderEventArgs) Handles CalendarFechaIngresoHastaBuscar.DayRender
        If Not txtFechaIngresoDesdeBuscar.Text.Equals("") Then
            If e.Day.Date < DateTime.Parse(txtFechaIngresoDesdeBuscar.Text) Then
                e.Day.IsSelectable = False
                e.Cell.ForeColor = System.Drawing.Color.Gray
            End If
        End If
    End Sub

#End Region

#Region "BOTON EXPORTAR"

    Protected Sub BtnExportar_Click(sender As Object, e As EventArgs) Handles BtnExportar.Click
        Dim Certificado As CertificadoDTO = New CertificadoDTO()
        Dim FechaDesde As Nullable(Of Date)
        Dim FechaHasta As Nullable(Of Date)
        Dim negocio As CertificadoNegocio = New CertificadoNegocio
        Dim mensaje As String

        If (ddlNombreAportanteBuscar.SelectedIndex > 0) Then
            Dim arrCadena As String() = ddlNombreAportanteBuscar.SelectedItem.Text().Split(New Char() {"/"c})

            Certificado.AP_Rut = arrCadena(0).Trim()
            Certificado.AP_Razon_Social = arrCadena(1).Trim()
        Else
            Certificado.AP_Rut = ""
            Certificado.AP_Razon_Social = ""
        End If

        If (ddlNombreFondoBuscar.SelectedIndex > 0) Then
            Dim arrCadena As String() = ddlNombreFondoBuscar.SelectedItem.Text().Split(New Char() {"/"c})

            Certificado.FN_Rut = arrCadena(0).Trim()
            Certificado.FN_Nombre_Corto = arrCadena(1).Trim()
        Else
            Certificado.FN_Rut = ""
            Certificado.FN_Nombre_Corto = ""
        End If


        If Not txtFechaCorteBuscar.Text.Equals("") Then
            Certificado.HT_Corte = Date.Parse(txtFechaCorteBuscar.Text)
        Else
            Certificado.HT_Corte = Nothing
        End If

        If Not txtFechaCanjeBuscar.Text.Equals("") Then
            Certificado.HT_Canje = Date.Parse(txtFechaCanjeBuscar.Text)
        Else
            Certificado.HT_Canje = Nothing
        End If

        If Not txtFechaIngresoDesdeBuscar.Text.Equals("") Then
            Certificado.CT_Fecha_Ingreso = Date.Parse(txtFechaIngresoDesdeBuscar.Text)
            FechaDesde = Date.Parse(txtFechaIngresoDesdeBuscar.Text)
        Else
            Certificado.CT_Fecha_Ingreso = Nothing
        End If

        If Not txtFechaIngresoHastaBuscar.Text.Equals("") Then
            FechaHasta = Date.Parse(txtFechaIngresoHastaBuscar.Text)
        Else
            FechaHasta = Nothing
        End If

        If txtFechaIngresoHastaBuscar.Text = Nothing And txtFechaCorteBuscar.Text = Nothing And txtFechaIngresoDesdeBuscar.Text = Nothing And txtFechaCanjeBuscar.Text = Nothing And ddlNombreFondoBuscar.SelectedValue.Trim() = Nothing And ddlNombreAportanteBuscar.SelectedValue.Trim() = Nothing Then
            mensaje = negocio.ExportarAExcelTodos(Certificado)
        Else
            mensaje = negocio.ExportarAExcel(Certificado, FechaDesde, FechaHasta)
        End If


        If negocio.rutaArchivosExcel IsNot Nothing Then
            Archivo.NavigateUrl = negocio.rutaArchivosExcel
            Archivo.Text = "Bajar Archivo"
        Else
            Archivo.Visible = False
        End If

        txtAccionHidden.Value = "MOSTRAR_DIALOGO"

        ShowMesagges(CONST_TITULO_CERTIFICAD0, mensaje, Constantes.CONST_RUTA_IMG + Constantes.CONST_IMG_LOGO, Constantes.CONST_RUTA_IMG + Constantes.CONST_IMG_INFO, False)


    End Sub
#End Region

#Region "BOTON CANCELAR"
    Private Sub btnModalCancelar_Click(sender As Object, e As EventArgs) Handles btnModalCancelar.Click
        txtAccionHidden.Value = ""
    End Sub
#End Region
    Protected Sub LinkButton1_Click(sender As Object, e As EventArgs) Handles LinkButton1.Click
        txtFechaIngresoDesdeBuscar.Text = Nothing
    End Sub
    Protected Sub LinkButton2_Click(sender As Object, e As EventArgs) Handles LinkButton2.Click
        txtFechaIngresoHastaBuscar.Text = Nothing
    End Sub
    Protected Sub LinkButton3_Click(sender As Object, e As EventArgs) Handles LinkButton3.Click
        txtFechaCanjeBuscar.Text = Nothing
    End Sub
End Class
