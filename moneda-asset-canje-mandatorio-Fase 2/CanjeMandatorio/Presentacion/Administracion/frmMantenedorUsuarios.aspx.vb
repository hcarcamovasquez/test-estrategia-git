Imports Negocio
Imports DTO

Partial Class Presentacion_Administracion_frmMantenedorUsuarios

    Inherits System.Web.UI.Page
    Private ReadOnly Negocio As UsuarioNegocio = New UsuarioNegocio

    Public Const CONST_TITULO_USUARIO As String = "Usuario"
    Public Const CONST_CREADO_EXITO As String = "Usuario Creado con éxito"
    Public Const CONST_CREAR_ERROR As String = "Error al Crear Usuario"

    Public Const CONST_TITULO_MODAL_MODIFICAR As String = "Modificar Usuario"
    Public Const CONST_TITULO_MODAL_ElIMINAR As String = "Eliminar usuario"
    Public Const CONST_TITULO_MODAL_CREAR As String = "Nuevo Usuario"

    Public Const CONST_MODIFICAR_ERROR As String = "Error al modificar los datos del Usuario"
    Public Const CONST_MODIFICAR_EXITO As String = "Usuario modificado con éxito"
    Public Const CONST_ELIMINAR_ERROR As String = "Error al eliminar el Usuario"
    Public Const CONST_ELIMINAR_EXITO As String = "Usuario eliminado con éxito"
    Public Const CONST_ELIMINAR_EXISTE_EN_OTRA_TABLA As String = "Usuario se encuentra relacionado en otra Tabla"
    Public Const CONST_INSERTAR_ERROR As String = "Error al ingresar el Usuario"
    Public Const CONST_INSERTAR_EXITO As String = "Usuario ingresado con éxito"
    Public Const CONST_ELIMINAR_ESTADO_CERO As String = "No se puede eliminar un registro ya deshabilitado"
    Private Const CONST_ERROR_ELIMINAR_ASI_MISMO As String = "Usuario no puede eliminar usuario"

    Public Const CONST_INSERTAR_VACIO As String = "No se puede agregar un nombre de usuario vacio"
    Public Const CONST_INSERTAR_PERFIL_VACIO As String = "Seleccione un perfil"
    Public Const CONST_INSERTAR_EXISTENTE As String = "No se puede agregar un usuario ya creado"

    Public Const CONST_PERFIL_INCORRECTO As String = "Pagina no disponible"
    Public Const CONST_SIN_RESULTADOS As String = "No se obtuvieron resultados de la búsqueda"

    Public Const CONST_COL_ID As Integer = 1
    Public Const CONST_COL_NOMBRE As Integer = 2
    Public Const CONST_COL_PERFIL As Integer = 3
    Public Const CONST_COL_ESTADO As Integer = 4
    Public Const CONST_COL_FECHAINGRESO As Integer = 5
    Public Const CONST_COL_USUARIOINGRESO As Integer = 6
    Public Const CONST_COL_FECHAMODIFICACION As Integer = 7
    Public Const CONST_COL_USAURIOMODIFICAION As Integer = 8


    Private Sub Presentacion_Administracion_frmMantenedorUsuarios_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            DataInitial()
        End If

        ValidaPermisosPerfil()
    End Sub

    Private Sub DataInitial()
        txtFeCreacionDesde.Text = ""
        txtFeCreacionHasta.Text = ""

        Calendar1.Visible = False
        Calendar2.Visible = False

        ddlPerfilUsuario.SelectedIndex = -1
        CargaFiltroUsuario()
        GrvTabla.DataSource = New List(Of AportantesXGrupoDTO)
        GrvTabla.DataBind()

        BtnExportar.Enabled = (GrvTabla.Rows.Count <> 0)
        ValidaPermisosPerfil()
    End Sub

    Private Sub ValidaPermisosPerfil()
        If Session("PERFIL") <> Constantes.CONST_PERFIL_ADMIN Then
            ShowAlertPerfilError(CONST_PERFIL_INCORRECTO)
        End If
    End Sub

    Private Sub ShowMesagges(title As String, mesagge As String, urlIconTitle As String, urlIconMesagge As String, Optional borraLink As Boolean = True)
        lblModalTitle.Text = title
        lblModalBody.Text = mesagge
        img_modal.ImageUrl = urlIconTitle
        img_body_modal.ImageUrl = urlIconMesagge
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModalmg", "$('#myModalmg').modal();", True)

    End Sub

    Private Sub ShowAlertPerfilError(mesagge As String)
        'Dim myScript As String = "alert('" + mesagge + "'); window.location='" + Request.ApplicationPath + "/blank.aspx';"
        Dim myScript As String = "alert('" + mesagge + "'); window.location='" + ResolveUrl("~/blank.aspx") + "';"
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alert", myScript, True)
    End Sub

    Protected Sub BtnBuscar_Click(sender As Object, e As EventArgs) Handles BtnBuscar.Click
        BuscarUsuario()

        If GrvTabla.Rows.Count <> 0 Then
            BtnExportar.Enabled = True
        Else
            BtnExportar.Enabled = False
            ShowAlert(CONST_SIN_RESULTADOS)
        End If

    End Sub

    Protected Sub btnCrear_Click(sender As Object, e As EventArgs) Handles btnCrear.Click
        FormateoEstiloFormCrear()

        txtFeCreacionHasta.Text = ""
        CargaUsuariosActiveDirectory()
    End Sub

    Protected Sub BtnModificar_Click(sender As Object, e As EventArgs) Handles BtnModificar.Click
        FormateoEstiloFormModificar()
        FormateoFormDatos(Negocio.GetUsuarioPorID(GetUsuarioSelect))
    End Sub

    Protected Sub BtnEliminar_Click(sender As Object, e As EventArgs) Handles BtnEliminar.Click
        Dim usuario As UsuarioDTO = GetUsuarioSelect()
        If Session("NombreUsuario") = usuario.US_Nombre Then
            ShowAlert(CONST_ERROR_ELIMINAR_ASI_MISMO)
        Else
            FormateoEstiloFormEliminar()
            FormateoFormDatos(Negocio.GetUsuarioPorID(usuario))
        End If

    End Sub

    Protected Sub btnVolver_Click(sender As Object, e As EventArgs) Handles btnVolver.Click
        Response.Redirect("~/blank.aspx")
    End Sub

    Private Sub btnLimpiarFrm_Click(sender As Object, e As EventArgs) Handles btnLimpiarFrm.Click
        DataInitial()
    End Sub

    Protected Sub BtnExportar_Click(sender As Object, e As EventArgs) Handles BtnExportar.Click
        Dim usuario As UsuarioDTO = New UsuarioDTO()
        Dim fechaHasta As Nullable(Of Date)
        usuario.US_Nombre = ddlNombreUsuario.SelectedValue.Trim()

        If ddlPerfilUsuario.SelectedValue.Trim() = "" Then
            usuario.US_Perfil = Nothing
        Else
            usuario.US_Perfil = Integer.Parse(ddlPerfilUsuario.SelectedValue.Trim())
        End If

        If Not txtFeCreacionDesde.Text.Equals("") Then
            usuario.US_FechaIngreso = Date.Parse(txtFeCreacionDesde.Text)
        Else
            usuario.US_FechaIngreso = Nothing
        End If

        If Not txtFeCreacionHasta.Text.Equals("") Then
            fechaHasta = Date.Parse(txtFeCreacionHasta.Text)
        Else
            fechaHasta = Nothing
        End If

        Dim mensaje As String = Negocio.ExportarAExcel(usuario, fechaHasta)

        If Negocio.rutaArchivosExcel IsNot Nothing Then
            linkArchivo.NavigateUrl = Negocio.rutaArchivosExcel
            linkArchivo.Text = "Bajar Archivo"
        Else
            linkArchivo.Visible = False
        End If

        ShowMesagges(CONST_TITULO_USUARIO, mensaje, Constantes.CONST_RUTA_IMG + Constantes.CONST_IMG_LOGO, Constantes.CONST_RUTA_IMG + Constantes.CONST_IMG_INFO, False)
    End Sub

    Private Sub FormateoEstiloFormCrear()
        'DESHABILITADOS
        btnModalGuardarModificar.Visible = False
        btnModalGuardarModificar.Enabled = False
        btnModalEliminar.Visible = False
        btnModalEliminar.Enabled = False

        'HABILITADOS
        btnModalGuardarCrear.Visible = True
        btnModalGuardarCrear.Enabled = True
        ddlModalNombreUsuario.Enabled = True
        ddlModalPerfil.Enabled = True
        ddlModalEstado.Enabled = True

        txtAccionHidden.Value = "CREAR"
        lblModalTitulo.Text = CONST_TITULO_MODAL_CREAR
    End Sub

    Private Sub FormateoEstiloFormModificar()
        btnModalGuardarModificar.Visible = True
        btnModalGuardarModificar.Enabled = True

        btnModalGuardarCrear.Visible = False
        btnModalEliminar.Visible = False

        ddlModalNombreUsuario.Enabled = False
        ddlModalPerfil.Enabled = True
        ddlModalEstado.Enabled = True

        txtAccionHidden.Value = "MODIFICAR"
        lblModalTitulo.Text = CONST_TITULO_MODAL_MODIFICAR
    End Sub

    Private Sub FormateoEstiloFormEliminar()
        btnModalGuardarModificar.Enabled = False
        btnModalGuardarCrear.Enabled = False
        btnModalGuardarModificar.Visible = False
        btnModalGuardarCrear.Visible = False

        btnModalEliminar.Visible = True
        btnModalEliminar.Enabled = True

        ddlModalNombreUsuario.Enabled = False
        ddlModalPerfil.Enabled = False
        ddlModalEstado.Enabled = False

        txtAccionHidden.Value = "ELIMINAR"
        lblModalTitulo.Text = CONST_TITULO_MODAL_ElIMINAR
    End Sub

    Private Sub BuscarUsuario()
        Dim usuario As UsuarioDTO = New UsuarioDTO()
        Dim fechaHasta As Nullable(Of Date)

        txtFeCreacionDesde.Text = Request.Form(txtFeCreacionDesde.UniqueID)
        txtFeCreacionHasta.Text = Request.Form(txtFeCreacionHasta.UniqueID)

        usuario.US_Perfil = Integer.Parse(ddlPerfilUsuario.SelectedIndex)

        If (ddlNombreUsuario.SelectedIndex > 0) Then
            usuario.US_Nombre = ddlNombreUsuario.Text
        Else
            usuario.US_Nombre = ""
        End If

        If Not txtFeCreacionDesde.Text.Equals("") Then
            usuario.US_FechaIngreso = Date.Parse(txtFeCreacionDesde.Text)
        Else
            usuario.US_FechaIngreso = Nothing
        End If

        If Not txtFeCreacionHasta.Text.Equals("") Then
            fechaHasta = Date.Parse(txtFeCreacionHasta.Text)
        Else
            fechaHasta = Nothing
        End If

        GrvTabla.DataSource = Negocio.UsuariosConsultarFiltro(usuario, fechaHasta)
        GrvTabla.DataBind()
    End Sub

    Protected Sub btnModalEliminar_Click(sender As Object, e As EventArgs) Handles btnModalEliminar.Click
        Dim usuario As UsuarioDTO = New UsuarioDTO()
        usuario = GetUsuarioModal()

        If Negocio.DeleteUsuario(usuario) Then
            ShowAlert(CONST_ELIMINAR_EXITO)
        Else
            ShowAlert(CONST_ELIMINAR_ERROR)
        End If
        txtAccionHidden.Value = ""

        DataInitial()
    End Sub

    Protected Sub btnModalGuardarModificar_Click(sender As Object, e As EventArgs) Handles btnModalGuardarModificar.Click

        If Negocio.UpdateUsuario(GetUsuarioModal) Then
            ShowAlert(CONST_MODIFICAR_EXITO)
        Else
            ShowAlert(CONST_MODIFICAR_ERROR)
        End If

        txtAccionHidden.Value = ""
        DataInitial()
    End Sub

    Private Sub FormateoFormDatos(usuario As UsuarioDTO)
        Dim listaUsuario As List(Of UsuarioDTO) = New List(Of UsuarioDTO)

        listaUsuario.Add(usuario)

        ddlModalNombreUsuario.DataSource = listaUsuario
        ddlModalNombreUsuario.DataMember = "US_Nombre"
        ddlModalNombreUsuario.DataValueField = "US_Id"
        ddlModalNombreUsuario.DataTextField = "US_Nombre"
        ddlModalNombreUsuario.DataBind()

        ddlModalPerfil.SelectedValue = usuario.US_Perfil
        ddlModalEstado.SelectedValue = usuario.US_Estado
    End Sub

    Private Function GetUsuarioSelect() As UsuarioDTO
        Dim usuario As New UsuarioDTO
        For Each row As GridViewRow In GrvTabla.Rows
            Dim chk As RadioButton = row.Cells(0).Controls(1)
            If chk IsNot Nothing And chk.Checked Then
                usuario.US_Id = row.Cells(CONST_COL_ID).Text.Trim()
                usuario.US_Nombre = row.Cells(CONST_COL_NOMBRE).Text.Trim()

                If row.Cells(CONST_COL_PERFIL).Text.Trim() = "Usuario Consulta" Then
                    usuario.US_Perfil = 1
                ElseIf row.Cells(CONST_COL_PERFIL).Text.Trim() = "Usuario Full" Then
                    usuario.US_Perfil = 2
                ElseIf row.Cells(CONST_COL_PERFIL).Text.Trim() = "Administrador" Then
                    usuario.US_Perfil = 3
                End If

                'usuario.US_Estado = row.Cells(CONST_COL_ESTADO).Text.Trim()
                usuario.US_FechaIngreso = row.Cells(CONST_COL_FECHAINGRESO).Text.Trim()
                usuario.US_UsuarioIngreso = row.Cells(CONST_COL_USUARIOINGRESO).Text.Trim()
                usuario.US_FechaModificacion = row.Cells(CONST_COL_FECHAMODIFICACION).Text.Trim()
                usuario.US_UsuarioModificacion = row.Cells(CONST_COL_USAURIOMODIFICAION).Text.Trim()
            End If
        Next

        Return usuario
    End Function

    'FUNCIONES DEL MODAL CREAR/MODIFICAR/ELIMINAR
    Protected Sub btnModalGuardarCrear_Click(sender As Object, e As EventArgs) Handles btnModalGuardarCrear.Click
        Dim usuario As UsuarioDTO = New UsuarioDTO()
        Dim solicitud As Integer


        If ddlModalNombreUsuario.SelectedItem.Text().Trim() = "" Then
            ShowAlert(CONST_INSERTAR_VACIO)
        ElseIf ddlModalPerfil.SelectedItem.Text().Trim() = "" Then
            ShowAlert(CONST_INSERTAR_PERFIL_VACIO)
        Else

            usuario = Negocio.GetUsuarioPorNombre(GetUsuarioModal())

            If usuario.US_Nombre Is Nothing Then
                solicitud = Negocio.InsertUsuario(GetUsuarioModal())

                If solicitud <> Constantes.CONST_ERROR_BBDD Then
                    If solicitud <> 0 Then
                        ShowAlert(CONST_CREADO_EXITO)
                        DataInitial()
                    Else
                        ShowAlert(CONST_CREAR_ERROR)
                    End If
                Else
                    ShowAlert(CONST_CREAR_ERROR)
                End If
            Else
                ShowAlert(CONST_INSERTAR_EXISTENTE)
            End If
        End If
        txtAccionHidden.Value = ""
        DataInitial()
    End Sub

    Private Function GetUsuarioModal() As UsuarioDTO
        Dim usuario As New UsuarioDTO()

        If ddlModalNombreUsuario.SelectedItem.Text() <> "" Then
            usuario.US_Id = ddlModalNombreUsuario.SelectedValue()
            usuario.US_Nombre = ddlModalNombreUsuario.SelectedItem.Text()
            usuario.US_Perfil = ddlModalPerfil.SelectedValue()
            usuario.US_Estado = ddlModalEstado.SelectedValue()
            usuario.US_UsuarioIngreso = Session("NombreUsuario")
            usuario.US_UsuarioModificacion = Session("NombreUsuario")
        End If
        Return usuario
    End Function

    Protected Sub btnModalCancelar_Click(sender As Object, e As EventArgs) Handles btnModalCancelar.Click
        txtAccionHidden.Value = ""
    End Sub

    Protected Sub LinkButton1_Click(sender As Object, e As EventArgs)
        Calendar1.Visible = (Not Calendar1.Visible)
    End Sub
    Protected Sub LinkButton2_Click(sender As Object, e As EventArgs)
        Calendar2.Visible = (Not Calendar2.Visible)
    End Sub
    Protected Sub Calendar1_SelectionChanged(sender As Object, e As EventArgs) Handles Calendar1.SelectionChanged
        txtFeCreacionDesde.Text = Calendar1.SelectedDate.ToShortDateString()

        If Not txtFeCreacionHasta.Text = "" Then
            If Date.Parse(txtFeCreacionHasta.Text) < Date.Parse(txtFeCreacionDesde.Text) Then
                txtFeCreacionHasta.Text = Calendar1.SelectedDate.ToShortDateString()
            End If
        End If
        Calendar1.SelectedDate = Nothing
        Calendar1.Visible = False
    End Sub
    Protected Sub Calendar2_SelectionChanged(sender As Object, e As EventArgs) Handles Calendar2.SelectionChanged
        txtFeCreacionHasta.Text = Calendar2.SelectedDate.ToShortDateString()

        If txtFeCreacionHasta.Text <> "" And txtFeCreacionDesde.Text <> "" Then
            If Date.Parse(txtFeCreacionHasta.Text) < Date.Parse(txtFeCreacionDesde.Text) Then
                txtFeCreacionDesde.Text = Calendar2.SelectedDate.ToShortDateString()
            End If
        End If
        Calendar2.SelectedDate = Nothing
        Calendar2.Visible = False
    End Sub

    Protected Sub Calendar2_DayRender(sender As Object, e As DayRenderEventArgs) Handles Calendar2.DayRender
        If Not txtFeCreacionDesde.Text.Equals("") Then
            If e.Day.Date < DateTime.Parse(txtFeCreacionDesde.Text) Then
                e.Day.IsSelectable = False
                e.Cell.ForeColor = System.Drawing.Color.Gray
            End If
        End If
    End Sub

    Private Sub CargaFiltroUsuario()
        Dim lista As List(Of UsuarioDTO) = GetUsuarioPorNombre()
        Dim usuarioVacio As UsuarioDTO = New UsuarioDTO()

        If lista.Count = 0 Then
            lista.Add(usuarioVacio)
        Else
            lista.Insert(0, usuarioVacio)
        End If

        ddlNombreUsuario.DataSource = lista
        ddlNombreUsuario.DataMember = "US_NombreValido"
        ddlNombreUsuario.DataValueField = "US_NombreValido"
        ddlNombreUsuario.DataTextField = "US_NombreValido"
        ddlNombreUsuario.DataBind()
    End Sub

    Private Sub CargaUsuariosActiveDirectory()
        Dim lista As List(Of String) = GetUsuarioActiveDirectory()
        Dim listaUsuarios As List(Of UsuarioDTO) = New List(Of UsuarioDTO)
        Dim usuario As UsuarioDTO

        If lista.Count = 0 Then
            lista.Add("")
        End If

        Dim i As Integer = 0

        For Each nombreUsuario As String In lista
            usuario = New UsuarioDTO()
            usuario.US_Id = i
            usuario.US_Nombre = nombreUsuario
            listaUsuarios.Add(usuario)
            i += 1
        Next
        ddlModalNombreUsuario.DataSource = listaUsuarios
        ddlModalNombreUsuario.DataMember = "US_Nombre"
        ddlModalNombreUsuario.DataValueField = "US_Id"
        ddlModalNombreUsuario.DataTextField = "US_Nombre"
        ddlModalNombreUsuario.DataBind()
    End Sub

    Private Function GetUsuarioPorNombre() As List(Of UsuarioDTO)
        Dim usuario As New UsuarioDTO()
        usuario.US_Nombre = ""
        Return Negocio.GetListaUsuarios(usuario)
    End Function

    Private Function GetUsuarioActiveDirectory() As List(Of String)
        Dim user As String = Session("NombreUsuario")
        Dim pass As String = Session("pass")
        Return Negocio.GetListaUsuariosActiveDirectory(user, pass)
    End Function

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
End Class
