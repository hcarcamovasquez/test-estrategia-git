Imports DTO
Imports Negocio

Partial Class Presentacion_Mantenedores_frmMantenedorFondos
    Inherits System.Web.UI.Page

    Private ReadOnly negocio As FondosNegocio = New FondosNegocio
    Private listaCarga As Object

    Public Const CONST_TITULO_FONDO As String = "Fondo"
    Public Const CONST_TITULO_MODAL_MODIFICAR As String = "Modificar Fondo"
    Public Const CONST_TITULO_MODAL_ElIMINAR As String = "Eliminar Fondo"
    Public Const CONST_TITULO_MODAL_CREAR As String = "Nuevo Fondo"

    Public Const CONST_MODIFICAR_ERROR As String = "Error al modificar los datos del Fondo"
    Public Const CONST_MODIFICAR_EXITO As String = "Fondo modificado con éxito"
    Public Const CONST_ELIMINAR_ERROR As String = "Error al eliminar el Fondo"
    Public Const CONST_ELIMINAR_EXITO As String = "Fondo eliminado con éxito"
    Public Const CONST_ELIMINAR_ESTADO_CERO As String = "No se puede eliminar un registro ya deshabilitado"
    Public Const CONST_ELIMINAR_EXISTE_EN_OTRA_TABLA As String = "Fondo se encuentra relacionado en otra Tabla"
    Public Const CONST_INSERTAR_ERROR As String = "Error al ingresar el Fondo"
    Public Const CONST_INSERTAR_EXITO As String = "Fondo ingresado con éxito"
    Public Const CONST_VALIDA_RUT_EXISTE As String = "El RUT ya se encuentra creado."
    Public Const CONST_SIN_RESULTADOS As String = "No se obtuvieron resultados de la búsqueda"

    Public Const CONST_COL_RUT As Integer = 1
    Public Const CONST_COL_RAZONSOCIAL As Integer = 2
    Public Const CONST_COL_NOMBRECORTO As Integer = 3
    Public Const CONST_COL_ESTADO As Integer = 4
    Public Const CONST_COL_CUOTASEMITIDAS As Integer = 5
    Public Const CONST_COL_FECHAEMISION As Integer = 6
    Public Const CONST_COL_FECHAVENCIMIENTO As Integer = 7
    Public Const CONST_COL_ACUMULADO As Integer = 8


    Public Const CONST_INGRESAR_RUT_FONDO As String = "DEBE INGRESAR RUT DE FONDO"
    Private Const CONST_INGRESAR_NOMBRE_CORTO As String = "Debe ingresar Nombre Corto Valido"
    Private Const CONST_INGRESAR_NOMBRE_FONDO As String = "Debe ingresar Nombre De fondo Valido"

    Private Const CONST_INGRESAR_CUOTAS_EMITIDAS As String = "Debe ingresar un valor mayor a 0 en Cuotas Emitidas"
    Private Const CONST_INGRESAR_FECHA_EMISION As String = "Debe ingresar Fecha de Emisión"
    Private Const CONST_INGRESAR_FECHA_VENCIMIENTO As String = "Debe ingresar fecha vencimiento"
    Private Const CONST_INGRESAR_ACUMULADO As String = "Debe ingresar un valor en el campo valor acumulado"

    Private Sub Presentacion_Mantenedores_frmMantenedorFondos_Load(sender As Object, e As EventArgs) Handles Me.Load
        txtModalRutFondo.Attributes("onBlur") = "validateRut(" & txtModalRutFondo.ClientID & ")"

        If Not IsPostBack Then
            DataInitial()
        End If

        ValidaPermisosPerfil()
    End Sub

    Private Sub DataInitial()
        txtFeCreacionDesde.Text = ""
        txtFeCreacionHasta.Text = ""

        CargaFiltroRut()
        CargaFiltroNombre()
        GrvTabla.DataSource = New List(Of FondoDTO)
        GrvTabla.DataBind()

        BtnExportar.Enabled = (GrvTabla.Rows.Count <> 0)
        ValidaPermisosPerfil()

        ddlControlTipoControl.Items.Insert(0, New ListItem("", ""))
        ddlControlTipoDeConfiguracion.Items.Insert(0, New ListItem("", ""))

        txtControlCantidadDias.Text = ""
        txtControlDiasAVerificar.Text = ""
        chkControlDiasHabiles.Checked = False


    End Sub

    Private Sub CargaFiltroRut()
        Dim fondo As New FondoDTO()
        Dim lista As List(Of FondoDTO) = negocio.ConsultarPorRut(fondo)
        Dim fondoVacio As New FondoDTO()
        If lista.Count = 0 Then
            ddlRutFondo.Items.Insert(0, New ListItem("", ""))
        Else
            lista.Insert(0, fondoVacio)
        End If

        ddlRutFondo.DataSource = lista
        ddlRutFondo.DataMember = "RutRazonSocial"
        ddlRutFondo.DataValueField = "RutRazonSocial"
        ddlNombreFondo.DataTextField = "RutRazonSocial"
        ddlRutFondo.DataBind()
    End Sub

    Private Sub CargaFiltroNombre()
        Dim fondo As New FondoDTO()
        Dim lista As List(Of FondoDTO) = negocio.ConsultarPorRazonSocial(fondo)

        If lista.Count = 0 Then
            ddlNombreFondo.Items.Insert(0, New ListItem("", ""))
        Else
            ddlNombreFondo.DataSource = lista
            ddlNombreFondo.DataMember = "RutRazonSocial"
            ddlNombreFondo.DataValueField = "RutRazonSocial"
            ddlNombreFondo.DataTextField = "RutRazonSocial"
            ddlNombreFondo.DataBind()
            ddlNombreFondo.Items.Insert(0, New ListItem("", ""))
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

    Private Sub GrvTabla_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles GrvTabla.PageIndexChanging

    End Sub

    Private Sub FindFondos()
        Dim fondo As FondoDTO = New FondoDTO()
        Dim FechaHasta As Nullable(Of Date)

        txtFeCreacionDesde.Text = Request.Form(txtFeCreacionDesde.UniqueID)
        txtFeCreacionHasta.Text = Request.Form(txtFeCreacionHasta.UniqueID)

        If (ddlRutFondo.SelectedIndex > 0) Then
            Dim arrCadena As String() = ddlRutFondo.SelectedItem.Text().Split(New Char() {"/"c})

            fondo.Rut = arrCadena(0).Trim()
            fondo.RazonSocial = arrCadena(1).Trim()
        Else
            fondo.Rut = ""
            fondo.RazonSocial = ""
        End If

        If Not txtFeCreacionDesde.Text.Equals("") Then
            fondo.FechaIngreso = Date.Parse(txtFeCreacionDesde.Text)
        Else
            fondo.FechaIngreso = Nothing
        End If

        If Not txtFeCreacionHasta.Text.Equals("") Then
            FechaHasta = Date.Parse(txtFeCreacionHasta.Text)
        Else
            FechaHasta = Nothing
        End If

        GrvTabla.DataSource = negocio.GetListaFondosConFiltro(fondo, FechaHasta)
        GrvTabla.DataBind()
    End Sub

    Protected Sub LinkButton1_Click(sender As Object, e As EventArgs)
        'Calendar1.Visible = (Not Calendar1.Visible)
    End Sub

    Protected Sub BtnBuscar_Click(sender As Object, e As EventArgs)
        FindFondos()

        BtnModificar.Enabled = False
        If GrvTabla.Rows.Count <> 0 Then
            BtnExportar.Enabled = True
        Else
            BtnExportar.Enabled = False
            ShowAlert(CONST_SIN_RESULTADOS)
        End If

        txtHiddenAccion.Value = ""
    End Sub

    Protected Sub btnLimpiarFrm_Click(sender As Object, e As EventArgs)
        DataInitial()
        txtHiddenAccion.Value = ""
    End Sub



    Protected Sub BtnModificar_Click(sender As Object, e As EventArgs)
        Dim fondoSelect As FondoDTO = GetFondoSelect()
        Dim fondoActualizado As FondoDTO = negocio.GetFondo(fondoSelect)

        FormateoFormDatos(fondoActualizado)
        FormateoEstiloFormModificar()
        txtHiddenAccion.Value = "MODIFICAR"
    End Sub

    Protected Sub BtnEliminar_Click(sender As Object, e As EventArgs) Handles BtnEliminar.Click
        Dim fondoSelect As FondoDTO = GetFondoSelect()
        Dim fondoActualizado As FondoDTO = negocio.GetFondo(fondoSelect)

        FormateoFormDatos(fondoActualizado)
        FormateoEstiloFormEliminar()
        txtHiddenAccion.Value = "ELIMINAR"
    End Sub

    Protected Sub btnCrear_Click(sender As Object, e As EventArgs) Handles btnCrear.Click
        FormateoLimpiarDatosModal()
        FormateoEstiloFormCrear()
        txtHiddenAccion.Value = "CREAR"
    End Sub

    Protected Sub BtnExportar_Click(sender As Object, e As EventArgs) Handles BtnExportar.Click
        Dim fondo As FondoDTO = New FondoDTO()
        Dim FechaHasta As Nullable(Of Date)
        Dim arrCadena As String() = ddlRutFondo.SelectedItem.Text().Split(New Char() {"/"c})

        If ddlRutFondo.SelectedIndex > 0 Then
            fondo.Rut = arrCadena(0).Trim()
            fondo.RazonSocial = arrCadena(1).Trim()
        Else
            fondo = New FondoDTO()
        End If

        If Not txtFeCreacionDesde.Text.Equals("") Then
            fondo.FechaIngreso = Date.Parse(txtFeCreacionDesde.Text)
        Else
            fondo.FechaIngreso = Nothing
        End If

        If Not txtFeCreacionHasta.Text.Equals("") Then
            FechaHasta = Date.Parse(txtFeCreacionHasta.Text)
        Else
            FechaHasta = Nothing
        End If

        Dim mensaje As String = negocio.ExportarAExcel(fondo, FechaHasta)

        If negocio.rutaArchivosExcel IsNot Nothing Then
            linkArchivo.NavigateUrl = negocio.rutaArchivosExcel
            linkArchivo.Text = "Bajar Archivo"
        Else
            linkArchivo.Visible = False
        End If

        txtHiddenAccion.Value = "MOSTRAR_DIALOGO"

        ShowMesagges(CONST_TITULO_FONDO, mensaje, Constantes.CONST_RUTA_IMG + Constantes.CONST_IMG_LOGO, Constantes.CONST_RUTA_IMG + Constantes.CONST_IMG_INFO, False)
    End Sub
    Private Function ValidaDatos() As Boolean
        Dim retorno As Boolean = False

        Textbox2.Text = Request.Form(Textbox2.UniqueID)

        If txtModalNombreFondo.Text.Trim() = "" Then
            ShowAlert(CONST_INGRESAR_NOMBRE_FONDO)
        ElseIf txtModalNombreCorto.Text.Trim() = "" Then
            ShowAlert(CONST_INGRESAR_NOMBRE_CORTO)

        ElseIf Textbox2.Text.Trim() = "" Then
            ShowAlert(CONST_INGRESAR_FECHA_EMISION)
        Else
            retorno = True
        End If
        Return retorno

    End Function

    Private Sub btnModalGuardarCrear_Click(sender As Object, e As EventArgs) Handles btnModalGuardarCrear.Click
        Dim solicitud As Integer
        Dim fondo As FondoDTO = New FondoDTO()
        Dim arrCadena As String()

        If ValidaDatos() Then
            fondo = GetFondoModal()
            arrCadena = fondo.Rut.Split(New Char() {"-"c})
            If arrCadena(1) = "-" Or arrCadena(1) = "" Then
                ShowAlert(CONST_INGRESAR_RUT_FONDO)
                Exit Sub
            Else
                solicitud = negocio.InsertFondo(fondo)

                If solicitud = Constantes.CONST_OPERACION_EXITOSA Then
                    'Ingresado con Exito
                    txtHiddenAccion.Value = ""
                    ShowAlert(CONST_INSERTAR_EXITO)
                    DataInitial()
                ElseIf solicitud = Constantes.CONST_ERROR_NO_SE_PUEDE_BORRAR Then
                    'Rut ya existe
                    ShowAlert(CONST_VALIDA_RUT_EXISTE)
                    Exit Sub
                Else
                    'Error en la BBDD
                    ShowAlert(CONST_INSERTAR_ERROR)
                    Exit Sub
                End If
            End If
        End If
    End Sub

    Private Sub btnModalGuardarModificar_Click(sender As Object, e As EventArgs) Handles btnModalGuardarModificar.Click
        If ValidaDatos() Then
            Dim fondo As FondoDTO = GetFondoModal()
            txtHiddenAccion.Value = ""
            Dim solicitud As Integer = negocio.UpdateFondo(fondo)

            If solicitud = Constantes.CONST_OPERACION_EXITOSA Then
                ShowAlert(CONST_MODIFICAR_EXITO)
            ElseIf solicitud = Constantes.CONST_ERROR_NO_SE_PUEDE_BORRAR Then
                ShowAlert(CONST_ELIMINAR_EXISTE_EN_OTRA_TABLA)
            Else
                ShowAlert(CONST_MODIFICAR_ERROR)
            End If
            DataInitial()
        End If
    End Sub

    Private Sub btnModalEliminarGrupo_Click(sender As Object, e As EventArgs) Handles btnModalEliminarGrupo.Click
        Dim fondo As FondoDTO = GetFondoModal()

        txtHiddenAccion.Value = ""
        If Not fondo.Estado = 0 Then
            Dim solicitud As Integer = negocio.DeleteFondo(fondo)
            If solicitud = Constantes.CONST_OPERACION_EXITOSA Then
                ShowAlert(CONST_ELIMINAR_EXITO)
                DataInitial()
            ElseIf solicitud = Constantes.CONST_ERROR_NO_SE_PUEDE_BORRAR Then
                ShowAlert(CONST_ELIMINAR_EXISTE_EN_OTRA_TABLA)
            Else
                ShowAlert(CONST_ELIMINAR_ERROR)
            End If
        Else
            ShowAlert(CONST_ELIMINAR_ESTADO_CERO)
        End If

    End Sub


    Private Sub ShowMesagges(title As String, mesagge As String, urlIconTitle As String, urlIconMesagge As String, Optional borraLink As Boolean = True)
        lblModalTitle.Text = title
        lblModalBody.Text = mesagge
        img_modal.ImageUrl = urlIconTitle
        img_body_modal.ImageUrl = urlIconMesagge

        linkArchivo.Visible = (Not borraLink)

    End Sub

    Private Function GetFondoSelect() As FondoDTO
        Dim fondo As New FondoDTO

        For Each row As GridViewRow In GrvTabla.Rows
            Dim chk As RadioButton = row.Cells(0).Controls(1)
            If chk IsNot Nothing And chk.Checked Then
                fondo.Rut = row.Cells(CONST_COL_RUT).Text.Trim()
                fondo.RazonSocial = HttpUtility.HtmlDecode(row.Cells(CONST_COL_RAZONSOCIAL).Text.Trim())
                fondo.NombreCorto = HttpUtility.HtmlDecode(row.Cells(CONST_COL_NOMBRECORTO).Text.Trim())

                If row.Cells(CONST_COL_CUOTASEMITIDAS).Text.Trim() = "&nbsp;" Then
                    fondo.CuotasEmitidas = Nothing
                Else
                    fondo.CuotasEmitidas = row.Cells(CONST_COL_CUOTASEMITIDAS).Text.Trim()
                End If

                fondo.FechaEmision = row.Cells(CONST_COL_FECHAEMISION).Text.Trim()

                If row.Cells(CONST_COL_FECHAVENCIMIENTO).Text = "&nbsp;" Then
                    fondo.FechaVencimiento = Nothing
                Else
                    fondo.FechaVencimiento = row.Cells(CONST_COL_FECHAVENCIMIENTO).Text.Trim()
                End If

                If row.Cells(CONST_COL_ACUMULADO).Text.Trim() = "&nbsp;" Then
                    fondo.Acumulado = Nothing
                Else
                    fondo.Acumulado = row.Cells(CONST_COL_ACUMULADO).Text.Trim()
                End If

                ' fondo.Estado = row.Cells(CONST_COL_ESTADO).Text.Trim()
            End If
        Next

        Return fondo
    End Function

    Private Sub FormateoFormDatos(fondo As FondoDTO)
        txtModalRutFondo.Text = fondo.Rut
        txtModalNombreFondo.Text = fondo.RazonSocial
        txtModalNombreCorto.Text = fondo.NombreCorto
        txtHidenEstado.Value = fondo.Estado

        If fondo.CuotasEmitidas Is Nothing Then
            txtCuotasEmitidas.Text = ""
        Else
            'txtCuotasEmitidas.Text = fondo.CuotasEmitidas
            txtCuotasEmitidas.Text = String.Format("{0:N6}", fondo.CuotasEmitidas)
        End If

        Textbox2.Text = fondo.FechaEmision
        If fondo.FechaVencimiento Is Nothing Then
            Textbox3.Text = ""
        Else
            Textbox3.Text = fondo.FechaVencimiento
        End If

        If fondo.Acumulado Is Nothing Then
            Textbox4.Text = ""
        Else
            Textbox4.Text = String.Format("{0:N6}", fondo.Acumulado)
        End If

        chkControlCuotas.Checked = fondo.ControlCuotas

        If fondo.ControlTipoControl <> "" Then
            ddlControlTipoControl.SelectedValue = fondo.ControlTipoControl
        End If

        txtControlDiasAVerificar.Text = fondo.ControlDiasAVerificar


        If fondo.ControlTipoDeConfiguracion <> "" Then
            ddlControlTipoDeConfiguracion.SelectedValue = fondo.ControlTipoDeConfiguracion
        End If

        txtControlCantidadDias.Text = fondo.ControlCantidadDias
        chkControlDiasHabiles.Checked = fondo.ControlDiasHabiles

        txtControlCantidadDias.Enabled = Not (fondo.ControlTipoDeConfiguracion.Equals("Pago"))
        chkControlDiasHabiles.Enabled = Not (fondo.ControlTipoDeConfiguracion.Equals("Pago"))

    End Sub

    Private Sub FormateoEstiloFormModificar()
        btnModalGuardarModificar.Enabled = True
        btnModalGuardarModificar.Visible = True
        btnModalGuardarCrear.Enabled = False
        btnModalGuardarCrear.Visible = False
        btnModalEliminarGrupo.Enabled = False

        txtModalRutFondo.Enabled = False
        txtModalNombreFondo.Enabled = True
        txtModalNombreCorto.Enabled = True

        txtCuotasEmitidas.Enabled = True
        Textbox2.Enabled = True
        Textbox3.Enabled = True
        Textbox4.Enabled = True

        LinkButton3.Enabled = True
        Linkbutton4.Enabled = True
        LinkButton5.Enabled = True
        Linkbutton6.Enabled = True

        lblModalTitulo.Text = CONST_TITULO_MODAL_MODIFICAR
    End Sub

    Private Sub FormateoEstiloFormEliminar()
        btnModalGuardarModificar.Enabled = False
        btnModalGuardarCrear.Enabled = False
        btnModalGuardarCrear.Visible = False
        btnModalEliminarGrupo.Enabled = True

        txtModalRutFondo.Enabled = False
        txtModalNombreFondo.Enabled = False
        txtModalNombreCorto.Enabled = False

        txtCuotasEmitidas.Enabled = False
        Textbox2.Enabled = False
        Textbox3.Enabled = False
        Textbox4.Enabled = False

        LinkButton3.Enabled = False
        Linkbutton4.Enabled = False
        LinkButton5.Enabled = False
        Linkbutton6.Enabled = False

        lblModalTitulo.Text = CONST_TITULO_MODAL_ElIMINAR
    End Sub

    Private Sub FormateoEstiloFormCrear()
        btnModalGuardarModificar.Enabled = False
        btnModalGuardarModificar.Visible = False
        btnModalGuardarCrear.Enabled = True
        btnModalGuardarCrear.Visible = True
        btnModalEliminarGrupo.Enabled = False

        txtModalRutFondo.Enabled = True
        txtModalNombreFondo.Enabled = True
        txtModalNombreCorto.Enabled = True

        txtCuotasEmitidas.Enabled = True
        Textbox2.Enabled = True
        Textbox3.Enabled = True
        Textbox4.Enabled = True

        LinkButton3.Enabled = True
        Linkbutton4.Enabled = True
        LinkButton5.Enabled = True
        Linkbutton6.Enabled = True

        lblModalTitulo.Text = CONST_TITULO_MODAL_CREAR
    End Sub

    Private Sub FormateoLimpiarDatosModal()
        txtModalRutFondo.Text = ""
        txtModalNombreFondo.Text = ""
        txtModalNombreCorto.Text = ""
        txtHidenEstado.Value = "0"
        txtCuotasEmitidas.Text = ""
        Textbox2.Text = ""
        Textbox3.Text = ""
        Textbox4.Text = ""

        chkControlCuotas.Checked = False

        ddlControlTipoControl.SelectedValue = ""
        txtControlDiasAVerificar.Text = ""
        ddlControlTipoDeConfiguracion.SelectedValue = ""
        txtControlCantidadDias.Text = ""
        chkControlDiasHabiles.Checked = False

    End Sub

    Private Function GetFondoModal() As FondoDTO
        Dim fondo As FondoDTO = New FondoDTO()

        'Textbox2.Text = Request.Form(Textbox2.UniqueID)
        Textbox3.Text = Request.Form(Textbox3.UniqueID)

        fondo.Rut = txtModalRutFondo.Text
        fondo.RazonSocial = IIf(txtModalNombreFondo.Text = "", Nothing, txtModalNombreFondo.Text.Trim())
        fondo.NombreCorto = IIf(txtModalNombreCorto.Text = "", Nothing, txtModalNombreCorto.Text.Trim())
        fondo.UsuarioIngreso = Session("NombreUsuario")
        fondo.Estado = txtHidenEstado.Value
        If txtCuotasEmitidas.Text = "" Then
            fondo.CuotasEmitidas = Nothing
        Else
            fondo.CuotasEmitidas = Decimal.Parse(Replace(txtCuotasEmitidas.Text, ".", ""))
        End If

        fondo.FechaEmision = Textbox2.Text
        If Textbox3.Text = "" Then
            fondo.FechaVencimiento = Nothing
        Else
            fondo.FechaVencimiento = Textbox3.Text.Trim()
        End If
        'fondo.FechaVencimiento = IIf(Textbox3.Text = "", Nothing, Textbox3.Text.Trim())

        If Textbox4.Text = "" Then
            fondo.Acumulado = Nothing
        Else
            fondo.Acumulado = Decimal.Parse(Replace(Textbox4.Text, ".", ""))
        End If

        fondo.ControlCuotas = chkControlCuotas.Checked


        fondo.ControlTipoControl = ddlControlTipoControl.SelectedValue
        fondo.ControlDiasAVerificar = IIf(txtControlDiasAVerificar.Text = "", 0, txtControlDiasAVerificar.Text)

        fondo.ControlTipoDeConfiguracion = ddlControlTipoDeConfiguracion.SelectedValue

        fondo.ControlCantidadDias = IIf(txtControlCantidadDias.Text = "", 0, txtControlCantidadDias.Text)
        fondo.ControlDiasHabiles = chkControlDiasHabiles.Checked


        Return fondo
    End Function

    Protected Sub btnVolver_Click(sender As Object, e As EventArgs) Handles btnVolver.Click
        Response.Redirect("~/blank.aspx")
    End Sub

    Private Sub btnModalCancelar_Click(sender As Object, e As EventArgs) Handles btnModalCancelar.Click
        txtHiddenAccion.Value = ""
    End Sub

    Private Sub ShowAlert(mesagge As String)
        Dim myScript As String = "alert('" + mesagge + "');"
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", myScript, True)
    End Sub

    Protected Sub BtnLimpiarFechaDesde_Click(sender As Object, e As EventArgs) Handles BtnLimpiarFechaDesde.Click
        txtFeCreacionDesde.Text = Nothing
    End Sub
    Protected Sub BtnLimpiarFechaHasta_Click(sender As Object, e As EventArgs) Handles BtnLimpiarFechaHasta.Click
        txtFeCreacionHasta.Text = Nothing
    End Sub

    Protected Sub Linkbutton4_Click(sender As Object, e As EventArgs) Handles Linkbutton4.Click
        Textbox2.Text = Nothing
    End Sub

    Protected Sub Linkbutton6_Click(sender As Object, e As EventArgs) Handles Linkbutton6.Click
        Textbox3.Text = Nothing
    End Sub
End Class
