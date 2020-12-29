Imports DBSUtils
Imports DTO
Imports Negocio
Imports System.Data


Partial Class Presentacion_Mantenedores_frmMantenedorHitos
    Inherits System.Web.UI.Page

    Private ReadOnly NegocioHito As HitosNegocio = New HitosNegocio
    Private ReadOnly NegocioFondo As FondosNegocio = New FondosNegocio

    Public Const CONST_TITULO_FONDO As String = "Hito"
    Public Const CONST_TITULO_MODAL_MODIFICAR As String = "Modificar Hito"
    Public Const CONST_MODIFICAR_ERROR As String = "Error al modificar los datos del Hito"
    Public Const CONST_MODIFICAR_EXITO As String = "Hito modificado con Éxito"
    Public Const CONST_ELIMINAR_ERROR As String = "Error al eliminar el Hito seleccionado"
    Public Const CONST_ELIMINAR_EXITO As String = "Hito eliminado con Éxito"
    Public Const CONST_ELIMINAR_ESTADO_CERO As String = "No se puede eliminar un registro ya deshabilitado"
    Public Const CONST_ELIMINAR_EXISTE_EN_OTRA_TABLA As String = "Hito se encuentra relacionado en otra Tabla"
    Public Const CONST_TITULO_MODAL_ELIMINAR As String = "Eliminar Hito"
    Public Const CONST_TITULO_MODAL_CREAR As String = "Nuevo Hito"
    Public Const CONST_COMBINACION_EXISTE As String = "La combinación de Rut y Fecha Corte ya existe"
    Public Const CONST_ERROR_AL_GUARDAR As String = "Error al guardar el Hito"
    Public Const CONST_EXITO_AL_GUARDAR As String = "Hito guardado con Éxito"
    Public Const CONST_FECHA_OBLIGATORIA As String = "La Fecha de Canje debe ser mayor a la Fecha de Corte"
    Public Const CONST_SIN_RESULTADOS As String = "No se obtuvieron resultados de la búsqueda"


    Public Const CONST_COL_HITO As Integer = 1
    Public Const CONST_COL_RUT_FONDO As Integer = 2
    Public Const CONST_COL_NOMBRE_FONDO As Integer = 3
    Public Const CONST_COL_FECHA_CORTE As Integer = 4
    Public Const CONST_COL_FECHA_CANJE As Integer = 5
    Public Const CONST_COL_ESTADO As Integer = 6
    Public Const CONST_COL_FECHA_INGRESO As Integer = 7
    Public Const CONST_COL_USUARIO_INGRESO As Integer = 8
    Public Const CONST_COL_FECHA_MODIFICACION As Integer = 9
    Public Const CONST_COL_USUARIO_MODIFICACION As Integer = 10


    Private Sub Presentacion_Mantenedores_frmMantenedorHito_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            DataInitial()
        End If
    End Sub

    Private Sub DataInitial()
        txtFechaCorteBusqueda.Text = ""
        txtFechaCorteHasta.Text = ""

        cargaFiltroRutBusqueda()
        cargaFiltroRutModal()
        cargaFiltroNombreModal()


        cargaIdHito()

        txtHiddenAccion.Value = ""
        GrvTabla.DataSource = New List(Of HitoDTO)
        GrvTabla.DataBind()
        BtnExportar.Enabled = (GrvTabla.Rows.Count <> 0)
        ValidaPermisosPerfil()
    End Sub

    Private Sub ValidaPermisosPerfil()
        HiddenPerfil.Value = Session("PERFIL")
        HiddenConstante.Value = Constantes.CONST_PERFIL_CONSULTA

        If Session("PERFIL") = Constantes.CONST_PERFIL_CONSULTA Or Session("PERFIL") = Nothing Then
            btnCrear.Enabled = False
            BtnModificar.Enabled = False
            BtnEliminar.Enabled = False
            BtnExportar.Enabled = False
        ElseIf Session("PERFIL") = Constantes.CONST_PERFIL_FULL Or Session("PERFIL") = Constantes.CONST_PERFIL_ADMIN Then
            btnCrear.Visible = True
            BtnModificar.Visible = True
            BtnEliminar.Visible = True
            BtnExportar.Visible = True
        End If
    End Sub

    Private Sub LlenarHito()
        Dim NegocioFondo As FondosNegocio = New FondosNegocio
        Dim fondo As FondoDTO = New FondoDTO()
        Dim listafondo As List(Of FondoDTO) = NegocioFondo.ConsultarTodos(fondo)

        ddlRutdeFondo.DataSource = listafondo
        ddlRutdeFondo.DataMember = "Rut"
        ddlRutdeFondo.DataValueField = "Rut"
        ddlRutdeFondo.DataBind()

        ddlNombreRutModal.DataSource = listafondo
        ddlNombreRutModal.DataMember = "RazonSocial"
        ddlNombreRutModal.DataValueField = "RazonSocial"
        ddlNombreRutModal.DataBind()

    End Sub

    Private Sub cargaFiltroRutModal()
        Dim fondo As New FondoDTO
        Dim listaFondos As List(Of FondoDTO) = NegocioFondo.ConsultarPorRut(fondo)
        If listaFondos.Count = 0 Then
            ddlRutdeFondo.Items.Insert(0, New ListItem("Seleccione", String.Empty))
        Else
            ddlRutdeFondo.DataSource = listaFondos
            ddlRutdeFondo.DataMember = "Rut"
            ddlRutdeFondo.DataValueField = "Rut"
            ddlRutdeFondo.DataBind()
            ddlRutdeFondo.Items.Insert(0, New ListItem("Seleccione", String.Empty))
        End If
    End Sub

    Private Sub cargaFiltroNombreModal()
        Dim fondo As New FondoDTO
        Dim listaFondos As List(Of FondoDTO) = NegocioFondo.ConsultarTodos(fondo)
        If listaFondos.Count = 0 Then
            ddlNombreRutModal.Items.Insert(0, New ListItem("Seleccione", String.Empty))
        Else
            ddlNombreRutModal.DataSource = listaFondos
            ddlNombreRutModal.DataMember = "RazonSocial"
            ddlNombreRutModal.DataValueField = "RazonSocial"
            ddlNombreRutModal.DataBind()
            ddlNombreRutModal.Items.Insert(0, New ListItem("Seleccione", String.Empty))
        End If
    End Sub

    Private Sub cargaFiltroRutBusqueda()
        Dim fondo As FondoDTO = New FondoDTO()
        Dim listaConcatenado As List(Of FondoDTO) = NegocioHito.ConsultarNombreRut(fondo)

        Dim hitoVacio As FondoDTO = New FondoDTO()

        If listaConcatenado.Count = 0 Then
            listaConcatenado.Add(hitoVacio)
        Else
            listaConcatenado.Insert(0, hitoVacio)
        End If

        ddlRutFondoBusqueda.DataSource = listaConcatenado
        ddlRutFondoBusqueda.DataMember = "RutBusqueda"
        ddlRutFondoBusqueda.DataValueField = "RutBusqueda"
        ddlRutFondoBusqueda.DataBind()
        ddlRutFondoBusqueda.Items.Insert(0, New ListItem("Seleccione", String.Empty))
    End Sub

    Protected Sub btnCrear_Click(sender As Object, e As EventArgs) Handles btnCrear.Click

        FormateoLimpiarDatosModal()
        FormateoEstiloFormCrear()
        cargaIdHito()
        txtHiddenAccion.Value = "CREAR"
    End Sub

    Private Sub FormateoLimpiarDatosModal()
        cargaFiltroNombreModal()
        cargaFiltroRutModal()
        txtModalFechaCorteFC.Text = ""
        txtModalFechaCanjeFC.Text = ""
        txtUsuarioIngreso.Text = ""
        txtFechaIngreso.Text = ""
        txtUsuarioModificacion.Text = ""
        txtFechaModificacion.Text = ""
    End Sub

    Private Sub FormateoFormDatos(hito As HitoDTO)
        LlenarHito()
        txtIdHito.Text = hito.IdHito
        ddlRutdeFondo.Text = hito.Rut
        Dim fondo As New FondoDTO
        fondo.RazonSocial = hito.NombreFondo
        Dim listaFondos As List(Of FondoDTO) = NegocioFondo.RutByNombreFondo(fondo)

        ddlNombreRutModal.SelectedValue = hito.NombreFondo
        txtModalFechaCorteFC.Text = hito.FechaCorte
        txtModalFechaCanjeFC.Text = hito.FechaCanje
        txtFechaIngreso.Text = hito.FechaIngreso
        txtUsuarioIngreso.Text = hito.UsuarioIngreso
        txtFechaModificacion.Text = hito.FechaModificacion
        txtUsuarioModificacion.Text = hito.UsuarioModificacion
    End Sub

    Private Sub FormateoEstiloFormCrear()
        btnModalGuardarModificar.Enabled = False
        btnModalGuardarModificar.Visible = False
        btnModalGuardarCrear.Enabled = True
        btnModalGuardarCrear.Visible = True
        btnModalEliminarHito.Enabled = False
        ddlNombreRutModal.Enabled = True
        ddlRutdeFondo.Enabled = True
        lnkBtnModalFechaCorte.Enabled = True
        lnkBtnModalFechaCanje.Enabled = True
        LnkBtnModalLimpiarFecha.Enabled = True
        lnkBtnModalLimpiarCanje.Enabled = True
        lblModalFondoTitle.Text = CONST_TITULO_MODAL_CREAR

        lnkBtnModalFechaCorte.Visible = True
        LnkBtnModalLimpiarFecha.Visible = True
        lnkBtnModalFechaCanje.Visible = True
        lnkBtnModalLimpiarCanje.Visible = True
    End Sub

    Private Function cargaIdHito() As HitoDTO
        Dim hito As New HitoDTO
        Dim negocio As HitosNegocio = New HitosNegocio

        Dim HitoActualizado As HitoDTO = negocio.ConsultarUltimoHito(hito)
        txtIdHito.Text = HitoActualizado.IdHito + 1
        Return hito
    End Function

    Private Function GetHitoSelect() As HitoDTO
        Dim hito As New HitoDTO

        For Each row As GridViewRow In GrvTabla.Rows
            Dim chk As RadioButton = row.Cells(0).Controls(1)
            If chk IsNot Nothing And chk.Checked Then
                hito.IdHito = row.Cells(CONST_COL_HITO).Text.Trim()
                hito.Rut = row.Cells(CONST_COL_RUT_FONDO).Text.Trim()
                hito.NombreFondo = HttpUtility.HtmlDecode(row.Cells(CONST_COL_NOMBRE_FONDO).Text.Trim())
                hito.FechaCorte = row.Cells(CONST_COL_FECHA_CORTE).Text.Trim()
                hito.FechaCanje = row.Cells(CONST_COL_FECHA_CANJE).Text.Trim()
            End If
        Next
        Return hito
    End Function

    Protected Sub BtnEliminar_Click(sender As Object, e As EventArgs) Handles BtnEliminar.Click
        Dim hitoSelect As HitoDTO = GetHitoSelect()
        Dim hitoActualizado As HitoDTO = NegocioHito.GetHito(hitoSelect)
        FormateoFormDatos(hitoActualizado)
            FormateoEstiloFormEliminar()
        txtHiddenAccion.Value = "ELIMINAR"
    End Sub

    Private Sub btnModalEliminarHito_Click(sender As Object, e As EventArgs) Handles btnModalEliminarHito.Click
        Dim Hito As HitoDTO = GetHito()
        '''TODO: DESCOMENTAR LAS SIGUIENTES LINEAS PARA ASEguRAR QUE NO PUEDE BORRAR HITOS 
        'Dim nRelaciones As Integer
        'nRelaciones = NegocioHito.BuscarRelaciones(Hito)

        'If (nRelaciones <> 0) Then
        '    ShowAlert(CONST_ELIMINAR_EXISTE_EN_OTRA_TABLA)
        '    Exit Sub
        'End If

        If NegocioHito.DeleteHito(Hito) = Constantes.CONST_OPERACION_EXITOSA Then
            cargaFiltroRutBusqueda()
            ShowAlert(CONST_ELIMINAR_EXITO)
            DataInitial()
        Else
            ShowAlert(CONST_ELIMINAR_ERROR)
            Exit Sub
        End If

    End Sub

    Private Sub FormateoEstiloFormEliminar()
        btnModalGuardarModificar.Enabled = False
        btnModalGuardarCrear.Enabled = False
        btnModalGuardarCrear.Visible = False
        btnModalEliminarHito.Enabled = True
        ddlRutdeFondo.Enabled = False
        ddlNombreRutModal.Enabled = False
        lnkBtnModalFechaCorte.Enabled = False
        lnkBtnModalFechaCanje.Enabled = False
        LnkBtnModalLimpiarFecha.Enabled = False
        lnkBtnModalLimpiarCanje.Enabled = False
        lblModalFondoTitle.Text = CONST_TITULO_MODAL_ELIMINAR

        lnkBtnModalFechaCorte.Visible = False
        LnkBtnModalLimpiarFecha.Visible = False
        lnkBtnModalFechaCanje.Visible = False
        lnkBtnModalLimpiarCanje.Visible = False

    End Sub

    Protected Sub BtnModificar_Click(sender As Object, e As EventArgs)
        Dim negocio As HitosNegocio = New HitosNegocio
        Dim HitoSelect As HitoDTO = GetHitoSelect()
        Dim HitoActualizado As HitoDTO = negocio.GetHito(HitoSelect)
        FormateoFormDatos(HitoActualizado)
            FormateoEstiloFormModificar()
            txtHiddenAccion.Value = "MODIFICAR"

    End Sub

    Private Sub btnModalGuardarModificar_Click(sender As Object, e As EventArgs) Handles btnModalGuardarModificar.Click
        Dim hito As HitoDTO = GetHito()
        Dim solicitud As Integer = NegocioHito.UpdateHito(hito)

        If solicitud = Constantes.CONST_OPERACION_EXITOSA Then
            ShowAlert(CONST_MODIFICAR_EXITO)
        Else
            ShowAlert(CONST_MODIFICAR_ERROR)
        End If
        DataInitial()
    End Sub


    Protected Sub RowSelector_CheckedChanged(sender As Object, e As EventArgs)
        For Each row As GridViewRow In GrvTabla.Rows
            Dim chk As RadioButton = row.Cells(0).Controls(1)
            If chk IsNot Nothing And chk.Checked Then
            End If
        Next
    End Sub

    Private Sub FormateoEstiloFormModificar()
        btnModalGuardarCrear.Enabled = False
        btnModalGuardarCrear.Visible = False
        btnModalGuardarModificar.Enabled = True
        btnModalGuardarModificar.Visible = True
        btnModalEliminarHito.Enabled = False
        ddlRutdeFondo.Enabled = False
        ddlNombreRutModal.Enabled = False
        lnkBtnModalFechaCorte.Enabled = False
        lnkBtnModalFechaCanje.Enabled = True
        LnkBtnModalLimpiarFecha.Enabled = False
        lnkBtnModalLimpiarCanje.Enabled = True
        txtHiddenAccion.Value = "MODIFICAR"

        lnkBtnModalFechaCorte.Visible = False
        LnkBtnModalLimpiarFecha.Visible = False
        lnkBtnModalFechaCanje.Visible = True
        lnkBtnModalLimpiarCanje.Visible = True

        lblModalFondoTitle.Text = CONST_TITULO_MODAL_MODIFICAR
    End Sub



    Private Sub ShowMessages(tittle As String, message As String, urlconTittle As String, urlconMessage As String, Optional borralink As Boolean = True)
        lblModalTitle.Text = tittle
        lblModalBody.Text = message
        img_modal.ImageUrl = urlconTittle
        img_body_modal.ImageUrl = urlconMessage
        linkArchivo.Visible = (Not borralink)
    End Sub


    Public Sub ValidarFechas()
        If (txtModalFechaCanjeFC.Text = "" Or txtModalFechaCorteFC.Text = "") Then
            txtFechaCorteBusqueda.Text = ""
            VerificarCombinacion()
        Else
            Dim hito As HitoDTO = New HitoDTO()
            hito.FechaCorte = txtModalFechaCorteFC.Text
            hito.FechaCanje = txtModalFechaCanjeFC.Text

            If hito.FechaCanje < hito.FechaCorte Then
                ShowAlertFechaCanje("La Fecha Canje no puede ser menor a la Fecha Corte")
                txtModalFechaCanjeFC.Text = ""
            End If
        End If
    End Sub

    Public Sub VerificarCombinacion()
        Dim Hito As HitoDTO = New HitoDTO()
        If txtModalFechaCorteFC.Text = "" Or ddlRutdeFondo.SelectedValue = "" Then
            txtFechaCorteBusqueda.Text = ""
        Else
            Hito.Rut = ddlRutdeFondo.SelectedValue
            Hito.FechaCorte = txtModalFechaCorteFC.Text
            Dim solicitud As Integer = NegocioHito.verificarExistentes(Hito)
            If solicitud = 1 Then
                ShowAlert(CONST_COMBINACION_EXISTE)
                txtModalFechaCorteFC.Text = ""
            End If
        End If

    End Sub

    Public Sub VerificarPorNombreFondo()
        Dim hito As HitoDTO = New HitoDTO()
        If txtModalFechaCorteFC.Text = "" Or ddlNombreRutModal.SelectedValue = "" Then
            txtFechaCorteBusqueda.Text = ""
        Else
            hito.NombreFondo = ddlNombreRutModal.SelectedValue
            hito.FechaCorte = txtModalFechaCorteFC.Text
            Dim solicitud As Integer = NegocioHito.verificarExistentesPorNombreFondo(hito)
            If solicitud = 3 Then
                ShowAlert(CONST_COMBINACION_EXISTE)
                txtModalFechaCorteFC.Text = ""

            End If
        End If
    End Sub


    Private Sub ShowAlertFechaCorte(mesagge As String, Optional mostrarEnPage As Boolean = False)
        Dim myScript As String = "alert('" + mesagge + "');"
        If Not mostrarEnPage Then
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel2.GetType(), "alert", myScript, True)
        Else
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", myScript, True)
        End If
    End Sub

    Private Sub ShowAlertFechaCanje(mesagge As String, Optional mostrarEnPage As Boolean = False)
        Dim myScript As String = "alert('" + mesagge + "');"
        If Not mostrarEnPage Then
            ScriptManager.RegisterStartupScript(UpdatePanel2, UpdatePanel2.GetType(), "alert", myScript, True)
        Else
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", myScript, True)
        End If
    End Sub


    Private Sub btnModalCancelar_Click(sender As Object, e As EventArgs) Handles btnModalCancelar.Click
        txtHiddenAccion.Value = ""
    End Sub

    Protected Sub btnLimpiarFrm_Click(sender As Object, e As EventArgs)
        DataInitial()
    End Sub

    Protected Sub BtnBuscar_Click(sender As Object, e As EventArgs) Handles BtnBuscar.Click
        FindHitos()
        BtnModificar.Enabled = False
        If GrvTabla.Rows.Count <> 0 Then
            BtnExportar.Enabled = True
        Else
            BtnExportar.Enabled = False
            ShowAlert(CONST_SIN_RESULTADOS)
        End If
        txtHiddenAccion.Value = ""
    End Sub

    Private Sub ShowAlert(mesagge As String)
        Dim myScript As String = "alert('" + mesagge + "');"
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", myScript, True)
    End Sub

    Private Sub CargarTodosHitos()
        Dim Hito As HitoDTO = New HitoDTO()
        Dim negocio As HitosNegocio = New HitosNegocio

        GrvTabla.DataSource = negocio.ConsultarTodos(Hito)
        GrvTabla.DataBind()


    End Sub

    Private Sub FindHitos()
        Dim Hito As HitoDTO = New HitoDTO()
        Dim fondo As FondoDTO = New FondoDTO()
        Dim fechaHasta As Nullable(Of Date)

        txtFechaCorteBusqueda.Text = Request.Form(txtFechaCorteBusqueda.UniqueID)
        txtFechaCorteHasta.Text = Request.Form(txtFechaCorteHasta.UniqueID)

        If (ddlRutFondoBusqueda.SelectedIndex > 0) Then
            Dim arrCadena As String() = ddlRutFondoBusqueda.SelectedItem.Text().Split(New Char() {"/"c})

            If ddlRutFondoBusqueda.SelectedValue.Trim() = "" Then
                Hito.Rut = arrCadena(0).Trim()
                fondo.RazonSocial = arrCadena(0).Trim()
            Else
                Hito.Rut = arrCadena(0).Trim()
                fondo.RazonSocial = arrCadena(1).Trim()
            End If
        Else
            Hito.Rut = Nothing
            fondo.RazonSocial = Nothing

        End If

        If Not txtFechaCorteBusqueda.Text.Equals("") Then
            Hito.FechaCorte = Date.Parse(txtFechaCorteBusqueda.Text)
        Else
            Hito.FechaCorte = Nothing
        End If

        If Not txtFechaCorteHasta.Text.Equals("") Then
            fechaHasta = Date.Parse(txtFechaCorteHasta.Text)
        Else
            fechaHasta = Nothing
        End If

        If ddlRutFondoBusqueda.Text = "" And txtFechaCorteBusqueda.Text = Nothing And txtFechaCorteHasta.Text = Nothing Then
            CargarTodosHitos()
        Else
            GrvTabla.DataSource = NegocioHito.ConsultarHitosFiltro(Hito, fechaHasta, fondo)
        End If
        GrvTabla.DataBind()
    End Sub




    Private Sub btnModalGuardarCrear_Click(sender As Object, e As EventArgs) Handles btnModalGuardarCrear.Click
        Dim hito As HitoDTO = GetHito()
        Dim solicitud As Integer = NegocioHito.InsertHito(hito)

        If solicitud = Constantes.CONST_OPERACION_EXITOSA Then
            ShowAlert(CONST_EXITO_AL_GUARDAR)
            DataInitial()
        Else
            ShowAlert(CONST_ERROR_AL_GUARDAR)
            Exit Sub
        End If
    End Sub

    Public Function GetHito() As HitoDTO
        Dim Hito As HitoDTO = New HitoDTO()

        Hito.IdHito = txtIdHito.Text
        Hito.Rut = ddlRutdeFondo.SelectedValue
        Hito.FechaCorte = IIf(txtModalFechaCorteFC.Text = "", Nothing, txtModalFechaCorteFC.Text)
        Hito.FechaCanje = IIf(txtModalFechaCanjeFC.Text = "", Nothing, txtModalFechaCanjeFC.Text)
        Hito.UsuarioIngreso = Session("NombreUsuario")
        Hito.UsuarioModificacion = Session("NombreUsuario")
        Hito.NombreFondo = ddlNombreRutModal.SelectedValue
        'Hito.Estado = IIf(txtEstadoCambio.Value = "", 1, txtEstadoCambio.Value)
        Return Hito
    End Function

    Protected Sub BtnExportar_Click(sender As Object, e As EventArgs) Handles BtnExportar.Click
        Dim hito As HitoDTO = New HitoDTO()
        Dim fondo As FondoDTO = New FondoDTO()
        Dim fechaHasta As Nullable(Of Date)

        If (ddlRutFondoBusqueda.SelectedIndex > 0) Then
            Dim arrCadena As String() = ddlRutFondoBusqueda.SelectedItem.Text().Split(New Char() {"/"c})

            If ddlRutFondoBusqueda.SelectedValue = "                                                  " Then
                hito.Rut = arrCadena(0).Trim()
                fondo.NombreCorto = arrCadena(0).Trim()
            Else
                hito.Rut = arrCadena(0).Trim()
                fondo.NombreCorto = arrCadena(1).Trim()
            End If
        Else
            hito.Rut = Nothing
            fondo.NombreCorto = Nothing

        End If

        If Not txtFechaCorteBusqueda.Text.Equals("") Then
            hito.FechaCorte = Date.Parse(txtFechaCorteBusqueda.Text)
        Else
            hito.FechaCorte = Nothing
        End If

        If Not txtFechaCorteHasta.Text.Equals("") Then
            fechaHasta = Date.Parse(txtFechaCorteHasta.Text)
        Else
            fechaHasta = Nothing
        End If

        Dim mensaje As String = NegocioHito.ExportarExcel(hito, fechaHasta, fondo)

        If NegocioHito.rutaArchivoExcel IsNot Nothing Then
            linkArchivo.NavigateUrl = NegocioHito.rutaArchivoExcel
            linkArchivo.Text = "Bajar Archivo"
        Else
            linkArchivo.Visible = False
        End If

        txtHiddenAccion.Value = "MOSTRAR_DIALOGO"

        ShowMessages(CONST_TITULO_FONDO, mensaje, Constantes.CONST_RUTA_IMG + Constantes.CONST_IMG_LOGO, Constantes.CONST_RUTA_IMG + Constantes.CONST_IMG_INFO, False)


    End Sub
    Protected Sub txtModalFechaCorteFC_TextChanged(sender As Object, e As EventArgs) Handles txtModalFechaCorteFC.TextChanged
        Dim negocioRescates As VentanasRescateNegocio = New VentanasRescateNegocio
        Dim fechaValidar As String

        txtModalFechaCorteFC.Text = Request.Form(txtModalFechaCorteFC.UniqueID)
        If (txtModalFechaCorteFC.Text <> "") Then
            'txtModalFechaCorteFC.Text = CalendarModalFechaCorte.SelectedDate.ToShortDateString()
            fechaValidar = negocioRescates.ValidaDiaHabil(txtModalFechaCorteFC.Text)

            If fechaValidar = "Festivo" Then
                txtModalFechaCorteFC.Text = ""
                ShowAlert("La fecha seleccionada es feriada")

            ElseIf fechaValidar = "No_Habil" Then
                txtModalFechaCorteFC.Text = ""
                ShowAlert("La fecha seleccionada no es hábil")
            Else

            End If

            VerificarCombinacion()
        End If

    End Sub
    Protected Sub txtModalFechaCanjeFC_TextChanged(sender As Object, e As EventArgs) Handles txtModalFechaCanjeFC.TextChanged
        Dim negocioRescates As VentanasRescateNegocio = New VentanasRescateNegocio
        Dim fechaValidar As String

        txtModalFechaCanjeFC.Text = Request.Form(txtModalFechaCanjeFC.UniqueID)

        If (txtModalFechaCanjeFC.Text <> "") Then
            fechaValidar = negocioRescates.ValidaDiaHabil(txtModalFechaCanjeFC.Text)

            If fechaValidar = "Festivo" Then
                txtModalFechaCanjeFC.Text = ""
                ShowAlert("La fecha seleccionada es feriada")

            ElseIf fechaValidar = "No_Habil" Then
                txtModalFechaCanjeFC.Text = ""
                ShowAlert("La fecha seleccionada no es hábil")

            Else
            End If
        End If
    End Sub
End Class
