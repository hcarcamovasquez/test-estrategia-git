Imports DTO
Imports Negocio

Partial Class Presentacion_Mantenedores_frmMantenedorValoresCuota
    Inherits System.Web.UI.Page
    Private listaCarga As Object

    Public Const CONST_TITULO_VALORESCUOTA As String = "Valores Cuotas"
    Public Const CONST_TITULO_MODAL_MODIFICAR As String = "Modificar Valores Cuotas"
    Public Const CONST_TITULO_MODAL_ElIMINAR As String = "Eliminar Valores Cuotas"
    Public Const CONST_TITULO_MODAL_CREAR As String = "Nuevo Valores Cuotas"

    Public Const CONST_MODIFICAR_ERROR As String = "Error al modificar los datos de Valores Cuotas"
    Public Const CONST_MODIFICAR_EXITO As String = "Valores Cuotas modificado con éxito"
    Public Const CONST_ELIMINAR_ERROR As String = "Error al eliminar Valores Cuota"
    Public Const CONST_ELIMINAR_EXITO As String = "Valores Cuotas eliminado con éxito"
    Public Const CONST_ELIMINAR_ESTADO_CERO As String = "No se puede eliminar un registro ya deshabilitado"
    Public Const CONST_ELIMINAR_EXISTE_EN_OTRA_TABLA As String = "Valores Cuotas se encuentra relacionado en otra Tabla"
    Public Const CONST_INSERTAR_ERROR As String = "Error al ingresar Valores Cuota"
    Public Const CONST_INSERTAR_EXITO As String = "Valores Cuotas ingresado con éxito"
    Public Const CONST_VALIDA_RUT_EXISTE As String = "El RUT ya se encuentra creado."
    Public Const CONST_YA_EXISTE As String = "La combinación ya existe"

    Public Const CONST_COL_RUT As Integer = 1
    Public Const CONST_COL_NEMOTECNICO As Integer = 2
    Public Const CONST_COL_FECHA As Integer = 3
    Public Const CONST_COL_FECHA_DESDE As Integer = 5
    Public Const CONST_COL_ESTADO As Integer = 9

    Public Const CONST_SIN_RESULTADOS As String = "No se obtuvieron resultados de la búsqueda"

    Private Sub Presentacion_Mantenedores_frmMantenedorValoresCuota_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            DataInitial()
        End If

        ValidaPermisosPerfil()
    End Sub

    Private Sub DataInitial()
        txtFechaDesde.Text = ""
        txtFechaHasta.Text = ""

        CargaNemotecnicoBuscar()
        txtHiddenAccion.Value = ""

        GrvTabla.DataSource = New List(Of VcSerieDTO)
        GrvTabla.DataBind()

        BtnExportar.Enabled = (GrvTabla.Rows.Count <> 0)
        ValidaPermisosPerfil()
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

    Private Sub CargaFechaUsuarioActual()
        txtModalFechaIngreso.Text = Date.Now.ToShortDateString()
        txtModalUsuarioIngreso.Text = Session("NombreUsuario")
    End Sub

#Region "CARGA COMBOS BUSQUEDA"


    Private Sub CargaNemotecnicoBuscar()
        Dim Negocio As ValoresCuotaNegocio = New ValoresCuotaNegocio
        Dim ValoresCuota As VcSerieDTO = New VcSerieDTO()

        Dim ValoresCuotavacia As New VcSerieDTO

        Dim lista As List(Of VcSerieDTO) = Negocio.CargarFiltroNemotecnico(ValoresCuota)

        If lista.Count = 0 Then
            lista.Add(ValoresCuotavacia)
            ddlNemotecnico.Items.Insert(0, New ListItem(0, String.Empty))
        Else
            lista.Insert(0, ValoresCuotavacia)

            ddlNemotecnico.DataSource = lista
            ddlNemotecnico.DataMember = "NemotecnicoBusqueda"
            ddlNemotecnico.DataValueField = "NemotecnicoBusqueda"
            ddlNemotecnico.DataBind()
            ddlNemotecnico.Items.Insert(0, New ListItem(0, String.Empty))
        End If
    End Sub
#End Region

#Region "CARGA COMBOS MODAL TODOS SIN FILTRO"

    Private Sub CargarRutFondoModal()

        Dim Negocio As FondoSeriesNegocio = New FondoSeriesNegocio
        Dim serie As FondoSerieDTO = New FondoSerieDTO()

        Dim listafondo As List(Of FondoDTO) = Negocio.GetFondos(serie)

        If listafondo.Count = 0 Then
            ddlModalFondo.Items.Insert(0, New ListItem("", ""))
        Else
            ddlModalFondo.DataSource = listafondo
            ddlModalFondo.DataMember = "RutRazonSocial"
            ddlModalFondo.DataValueField = "Rut"
            ddlModalFondo.DataTextField = "RutRazonSocial"
            ddlModalFondo.DataBind()
            ddlModalFondo.Items.Insert(0, New ListItem("", ""))
        End If
    End Sub

    Public Sub CargaNemotecnicoModal()

        Dim NegocioFondoSerie As FondoSeriesNegocio = New FondoSeriesNegocio
        Dim fondoSeries As FondoSerieDTO = New FondoSerieDTO()
        Dim fondo As FondoDTO = New FondoDTO()


        Dim listafondoSeries As List(Of FondoSerieDTO) = NegocioFondoSerie.GetListaFondoSerieConFiltro(fondoSeries, fondo)

        If listafondoSeries.Count = 0 Then
            ddlModalNemotecnico.Items.Insert(0, New ListItem("", ""))
        Else
            ddlModalNemotecnico.DataSource = listafondoSeries
            ddlModalNemotecnico.DataMember = "Nemotecnico"
            ddlModalNemotecnico.DataValueField = "Nemotecnico"
            ddlModalNemotecnico.DataBind()
            ddlModalNemotecnico.Items.Insert(0, New ListItem("", ""))
        End If

    End Sub

#End Region


#Region "CARGA  NEMOTECNICO CUANDO CAMBIA COMBO RUT FONDO"

    Public Sub CargarNemotecnicoPorRutFondoModal()
        Dim NegocioFondoSerie As FondoSeriesNegocio = New FondoSeriesNegocio
        Dim fondoSeries As FondoSerieDTO = New FondoSerieDTO()
        Dim fondo As FondoDTO = New FondoDTO()

        fondoSeries.Rut = ddlModalFondo.SelectedValue
        Dim listafondoSeries As List(Of FondoSerieDTO) = NegocioFondoSerie.GetListaFondoSerieConFiltro(fondoSeries, fondo)

        If listafondoSeries.Count = 0 Then
            ddlModalNemotecnico.Items.Insert(0, New ListItem("", ""))
        Else
            ddlModalNemotecnico.DataSource = listafondoSeries
            ddlModalNemotecnico.DataMember = "Nemotecnico"
            ddlModalNemotecnico.DataValueField = "Nemotecnico"
            ddlModalNemotecnico.DataBind()
        End If
    End Sub

#End Region

#Region "CARGA RUT FONDO Y NOMBRE FONDO CUANDO CAMBIA COMBO NEMOTECNICO"

    Public Sub CargarRutFondoPorNemotecnicoModal()
        Dim NegocioFondoSerie As FondoSeriesNegocio = New FondoSeriesNegocio
        Dim fondoSeries As FondoSerieDTO = New FondoSerieDTO()
        Dim fondo As FondoDTO = New FondoDTO()

        fondoSeries.Nemotecnico = ddlModalNemotecnico.SelectedValue
        'Dim listafondoSeries As List(Of FondoSerieDTO) = NegocioFondoSerie.GetListaFondoSerieConFiltro(fondoSeries, fondo)
        Dim listafondoSeries As List(Of FondoSerieDTO) = NegocioFondoSerie.GrupoSeriesPorNemotecnico(fondoSeries)

        If listafondoSeries.Count = 0 Then
            ddlModalFondo.Items.Insert(0, New ListItem("", ""))
        Else
            Dim fondoSerie As FondoSerieDTO = listafondoSeries(0)
            ddlModalFondo.SelectedValue = fondoSerie.Rut
            ddlModalFondo.Enabled = False
        End If
    End Sub

#End Region

#Region "CARGA COMBOS A MODIFICAR"

#End Region

    Protected Sub btnCrear_Click(sender As Object, e As EventArgs) Handles btnCrear.Click

        CargaNemotecnicoModal()
        CargarRutFondoModal()
        FormateoLimpiarDatosModal()
        FormateoEstiloFormCrear()

        txtHiddenAccion.Value = "CREAR"
    End Sub

    Private Sub FormateoLimpiarDatosModal()
        txtHiddenAccion.Value = ""
        txtModalFecha1.Text = ""
        txtModalValorCuota.Text = ""
        txtModalFechaIngreso.Text = ""
        txtModalUsuarioIngreso.Text = ""
        txtModalFechaModificacion.Text = ""
        txtModalUsuarioModificacion.Text = ""
        ddlModalFondo.SelectedIndex = 0
        ddlModalNemotecnico.SelectedIndex = 0

        txtHidenEstado.Value = "0"
    End Sub

    Private Sub FormateoEstiloFormCrear()
        btnModalGuardarModificar.Enabled = False
        btnModalGuardarModificar.Visible = False
        btnModalGuardarCrear.Enabled = True
        btnModalGuardarCrear.Visible = True
        btnModalEliminarGrupo.Enabled = False
        BtnLimpiarFechaHasta.Enabled = True
        BtnLimpiarFechaHasta.Visible = True

        'txtModalFecha.Enabled = True
        txtModalValorCuota.Enabled = True
        ddlModalFondo.Enabled = True
        ddlModalNemotecnico.Enabled = True
        lnkBtnModalFecha.Visible = True
        lnkBtnModalFecha.Enabled = True
        BtnLimpiarFechaHasta.Visible = True
        BtnLimpiarFechaHasta.Enabled = True


        lblModalFondoTitle.Text = CONST_TITULO_MODAL_CREAR
    End Sub



    Private Sub btnModalGuardarCrear_Click(sender As Object, e As EventArgs) Handles btnModalGuardarCrear.Click
        'CREAR
        Dim negocioIns As ValoresCuotaNegocio = New ValoresCuotaNegocio
        Dim valoresCuota As VcSerieDTO = GetValoresCuotaModal()
        Dim solicitudInsertar As Integer
        'VALIDA QUE NO EXISTA EL REGISTRO YA CREADO
        Dim valoresCuotaValidar As VcSerieDTO = New VcSerieDTO()
        valoresCuotaValidar = negocioIns.GetValoresCuota(valoresCuota)
        If valoresCuotaValidar Is Nothing Then
            solicitudInsertar = negocioIns.InsertvaloresCuota(valoresCuota)
            If solicitudInsertar = Constantes.CONST_OPERACION_EXITOSA Then
                'Ingresado con Exito
                DataInitial()
                ShowAlert(CONST_INSERTAR_EXITO)
            ElseIf solicitudInsertar = Constantes.CONST_ERROR_NO_SE_PUEDE_BORRAR Then
                'Rut ya existe
                DataInitial()
                ShowAlert(CONST_YA_EXISTE)
            Else
                'Error en la BBDD
                DataInitial()
                ShowAlert(CONST_INSERTAR_ERROR)
            End If
        Else
            ShowAlert("El Registro ya fué creado, no se pueden duplicar datos.")
            Return
        End If
    End Sub

    Private Function GetValoresCuotaModal() As VcSerieDTO
        Dim valoresCuota As VcSerieDTO = New VcSerieDTO()

        txtModalFecha1.Text = Request.Form(txtModalFecha1.UniqueID)

        valoresCuota.FnRut = ddlModalFondo.SelectedValue
        valoresCuota.FsNemotecnico = ddlModalNemotecnico.SelectedValue
        valoresCuota.Fecha = txtModalFecha1.Text
        valoresCuota.Valor = txtModalValorCuota.Text
        valoresCuota.UsuarioIngreso = Session("NombreUsuario")
        valoresCuota.FechaModificacion = Date.Now
        valoresCuota.UsuarioModificacion = Session("NombreUsuario")
        valoresCuota.Estado = Constantes.CONST_HABILITADO

        Return valoresCuota
    End Function

    Private Sub ShowMesagges(title As String, mesagge As String, urlIconTitle As String, urlIconMesagge As String, Optional borraLink As Boolean = True)
        lblModalTitle.Text = title
        lblModalBody.Text = mesagge
        img_modal.ImageUrl = urlIconTitle
        img_body_modal.ImageUrl = urlIconMesagge

        linkArchivo.Visible = (Not borraLink)

    End Sub

    Private Sub btnModalCancelar_Click(sender As Object, e As EventArgs) Handles btnModalCancelar.Click
        txtHiddenAccion.Value = ""
        FormateoLimpiarDatosModal()
    End Sub



    Private Sub CargarTodosValoresCuota()
        Dim ValoresCuota As VcSerieDTO = New VcSerieDTO()
        Dim negocio As ValoresCuotaNegocio = New ValoresCuotaNegocio

        GrvTabla.DataSource = negocio.ConsultarTodos(ValoresCuota)
        GrvTabla.DataBind()

    End Sub

    Private Sub FindValoresCuota()
        Dim ValoresCuota As VcSerieDTO = New VcSerieDTO()
        Dim negocio As ValoresCuotaNegocio = New ValoresCuotaNegocio
        Dim FechaHasta As Nullable(Of Date)

        txtFechaDesde.Text = Request.Form(txtFechaDesde.UniqueID)
        txtFechaHasta.Text = Request.Form(txtFechaHasta.UniqueID)

        ValoresCuota.FsNemotecnico = ddlNemotecnico.SelectedValue

        If Not txtFechaDesde.Text.Equals("") Then
            ValoresCuota.FechaIngreso = Date.Parse(txtFechaDesde.Text)
        Else
            ValoresCuota.FechaIngreso = Nothing
        End If

        If Not txtFechaHasta.Text.Equals("") Then
            FechaHasta = Date.Parse(txtFechaHasta.Text)
        Else
            FechaHasta = Nothing
        End If

        GrvTabla.DataSource = negocio.GetListaValoresCuotaConFiltro(ValoresCuota, FechaHasta)
        GrvTabla.DataBind()

    End Sub

    Protected Sub btnLimpiarFrm_Click(sender As Object, e As EventArgs)
        txtHiddenAccion.Value = ""
        DataInitial()
    End Sub

    Protected Sub BtnModificar_Click(sender As Object, e As EventArgs)
        Dim negocio As ValoresCuotaNegocio = New ValoresCuotaNegocio

        Dim ValoresCuotaSelect As VcSerieDTO = GetValoresCuotaSelect()
        Dim ValoresCuotaActualizado As VcSerieDTO = negocio.GetValoresCuota(ValoresCuotaSelect)

        FormateoFormDatos(ValoresCuotaActualizado)
        FormateoEstiloFormModificar()
        txtHiddenAccion.Value = "MODIFICAR"
        ddlModalFondo.Enabled = False
        ddlModalNemotecnico.Enabled = False
        txtModalFecha1.ReadOnly = True
        txtModalValorCuota.Enabled = True

    End Sub

    Private Function GetValoresCuotaSelect() As VcSerieDTO
        Dim ValoresCuota As New VcSerieDTO

        For Each row As GridViewRow In GrvTabla.Rows
            Dim chk As RadioButton = row.Cells(0).Controls(1)
            If chk IsNot Nothing And chk.Checked Then
                ValoresCuota.FnRut = row.Cells(CONST_COL_RUT).Text.Trim()
                ValoresCuota.FsNemotecnico = row.Cells(CONST_COL_NEMOTECNICO).Text.Trim()
                ValoresCuota.Fecha = row.Cells(CONST_COL_FECHA).Text.Trim()
                ValoresCuota.FechaIngreso = row.Cells(CONST_COL_FECHA_DESDE).Text.Trim()
                ValoresCuota.UsuarioModificacion = Session("NombreUsuario")
                'ValoresCuota.Estado = row.Cells(CONST_COL_ESTADO).Text.Trim()
            End If
        Next

        Return ValoresCuota
    End Function

    Private Sub FormateoFormDatos(ValoresCuota As VcSerieDTO)
        CargarRutFondoModal()
        CargaNemotecnicoModal()
        txtModalFecha1.Text = ValoresCuota.Fecha
        ddlModalFondo.SelectedValue = ValoresCuota.FnRut
        ddlModalNemotecnico.SelectedValue = ValoresCuota.FsNemotecnico
        txtModalValorCuota.Text = String.Format("{0:N6}", ValoresCuota.Valor)
        txtModalFechaIngreso.Text = ValoresCuota.FechaIngreso
        txtModalUsuarioIngreso.Text = ValoresCuota.UsuarioIngreso
        txtModalFechaModificacion.Text = ValoresCuota.FechaModificacion
        txtModalUsuarioModificacion.Text = Session("NombreUsuario")
    End Sub

    Private Sub FormateoEstiloFormModificar()
        btnModalGuardarModificar.Enabled = True
        btnModalGuardarModificar.Visible = True
        btnModalGuardarCrear.Enabled = False
        btnModalGuardarCrear.Visible = False
        btnModalEliminarGrupo.Enabled = False
        BtnLimpiarFechaHasta.Enabled = False
        BtnLimpiarFechaHasta.Visible = False

        lnkBtnModalFecha.Visible = False
        lnkBtnModalFecha.Enabled = False
        lblModalFondoTitle.Text = CONST_TITULO_MODAL_MODIFICAR
    End Sub

    Private Sub btnModalGuardarModificar_Click(sender As Object, e As EventArgs) Handles btnModalGuardarModificar.Click
        'MODIFICAR
        Dim negocioMod As ValoresCuotaNegocio = New ValoresCuotaNegocio
        Dim valoresCuota As VcSerieDTO = GetValoresCuotaModal()

        Dim solicitudMod As Integer = negocioMod.UpdateValoresCuota(valoresCuota)

        If solicitudMod = Constantes.CONST_OPERACION_EXITOSA Then
            ShowAlert(CONST_MODIFICAR_EXITO)
            DataInitial()
        Else
            ShowAlert(CONST_MODIFICAR_ERROR)
            DataInitial()
        End If
    End Sub

    Protected Sub BtnEliminar_Click(sender As Object, e As EventArgs) Handles BtnEliminar.Click
        Dim negocioElim As ValoresCuotaNegocio = New ValoresCuotaNegocio
        Dim ValoresCuotaSelect As VcSerieDTO = GetValoresCuotaSelect()
        Dim ValoresCuotaActualizado As VcSerieDTO = negocioElim.GetValoresCuota(ValoresCuotaSelect)

        FormateoFormDatos(ValoresCuotaActualizado)
        FormateoEstiloFormEliminar()

        txtHiddenAccion.Value = "ELIMINAR"
    End Sub

    Private Sub FormateoEstiloFormEliminar()
        btnModalGuardarModificar.Enabled = False
        btnModalGuardarCrear.Enabled = False
        btnModalGuardarCrear.Visible = False
        btnModalEliminarGrupo.Enabled = True
        BtnLimpiarFechaHasta.Enabled = False
        BtnLimpiarFechaHasta.Visible = False

        txtModalFecha1.ReadOnly = True
        ddlModalFondo.Enabled = False
        ddlModalNemotecnico.Enabled = False
        txtModalValorCuota.Enabled = False
        txtModalFechaIngreso.Enabled = False
        txtModalUsuarioIngreso.Enabled = False
        txtModalFechaModificacion.Enabled = False
        txtModalUsuarioModificacion.Enabled = False
        lnkBtnModalFecha.Visible = False
        lnkBtnModalFecha.Enabled = False

        lblModalFondoTitle.Text = CONST_TITULO_MODAL_ElIMINAR
    End Sub

    Private Sub btnModalEliminarGrupo_Click(sender As Object, e As EventArgs) Handles btnModalEliminarGrupo.Click
        Dim ValoresCuota As VcSerieDTO = GetValoresCuotaModal()
        Dim negocioElim As ValoresCuotaNegocio = New ValoresCuotaNegocio

        'txtHiddenAccion.Value = "MOSTRAR_DIALOGO"
        If Not ValoresCuota.Estado = 0 Then
            Dim solicitud As Integer = negocioElim.DeleteValoresCuota(ValoresCuota)
            If solicitud = Constantes.CONST_OPERACION_EXITOSA Then
                CargarRutFondoModal()
                ShowAlert(CONST_ELIMINAR_EXITO)
            ElseIf solicitud = Constantes.CONST_ERROR_NO_SE_PUEDE_BORRAR Then
                ShowAlert(CONST_ELIMINAR_EXISTE_EN_OTRA_TABLA)
            Else
                ShowAlert(CONST_ELIMINAR_ERROR)
            End If
        Else
            ShowAlert(CONST_ELIMINAR_ESTADO_CERO)
        End If
        DataInitial()
        txtHiddenAccion.Value = ""
        CargaNemotecnicoBuscar()
    End Sub

    Protected Sub BtnExportar_Click(sender As Object, e As EventArgs) Handles BtnExportar.Click
        Dim ValoresCuota As VcSerieDTO = New VcSerieDTO()
        Dim FechaHasta As Nullable(Of Date)
        Dim negocio As ValoresCuotaNegocio = New ValoresCuotaNegocio
        Dim mensaje As String

        txtFechaDesde.Text = Request.Form(txtFechaDesde.UniqueID)
        txtFechaHasta.Text = Request.Form(txtFechaHasta.UniqueID)

        ValoresCuota.FsNemotecnico = ddlNemotecnico.SelectedValue

        If Not txtFechaDesde.Text.Equals("") Then
            ValoresCuota.FechaIngreso = Date.Parse(txtFechaDesde.Text)
        Else
            ValoresCuota.FechaIngreso = Nothing
        End If

        If Not txtFechaHasta.Text.Equals("") Then
            FechaHasta = Date.Parse(txtFechaHasta.Text)
        Else
            FechaHasta = Nothing
        End If

        If ddlNemotecnico.Text.Trim() = "" And txtFechaHasta.Text = Nothing And txtFechaDesde.Text = Nothing Then
            mensaje = negocio.ExportarAExcelTodos(ValoresCuota)
        Else
            mensaje = negocio.ExportarAExcel(ValoresCuota, FechaHasta)
        End If

        If Negocio.rutaArchivosExcel IsNot Nothing Then
            linkArchivo.NavigateUrl = Negocio.rutaArchivosExcel
            linkArchivo.Text = "Bajar Archivo"
        Else
            linkArchivo.Visible = False
        End If

        txtHiddenAccion.Value = "MOSTRAR_DIALOGO"

        ShowMesagges(CONST_TITULO_VALORESCUOTA, mensaje, Constantes.CONST_RUTA_IMG + Constantes.CONST_IMG_LOGO, Constantes.CONST_RUTA_IMG + Constantes.CONST_IMG_INFO, False)
    End Sub

    Private Sub ShowAlert(mesagge As String)
        Dim myScript As String = "alert('" + mesagge + "');"
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", myScript, True)
    End Sub

    Private Sub btnXCerrar_Load(sender As Object, e As EventArgs) Handles btnXCerrar.Load

    End Sub

    Protected Sub RowSelector_CheckedChanged(sender As Object, e As EventArgs)
        For Each row As GridViewRow In GrvTabla.Rows
            Dim chk As RadioButton = row.Cells(0).Controls(1)
            If chk IsNot Nothing And chk.Checked Then
            End If
        Next
    End Sub


    Protected Sub BtnBuscar_Click(sender As Object, e As EventArgs) Handles BtnBuscar.Click

        txtHiddenAccion.Value = ""
        FindValoresCuota()
        If GrvTabla.Rows.Count = 0 Then
            BtnExportar.Enabled = False
            ShowAlert(CONST_SIN_RESULTADOS)
        Else
            BtnExportar.Enabled = True
        End If

        txtHiddenAccion.Value = ""

    End Sub

End Class
