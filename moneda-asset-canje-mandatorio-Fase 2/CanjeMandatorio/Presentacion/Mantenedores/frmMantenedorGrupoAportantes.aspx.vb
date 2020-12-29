Imports DTO
Imports Negocio
Imports System.Data

Partial Class Presentacion_Mantenedores_frmMantenedorGrupoAportantes
    Inherits System.Web.UI.Page

    Private ReadOnly negocioGrupoAportante As GrupoAportanteNegocio = New GrupoAportanteNegocio
    Private ReadOnly negocioAportante As AportanteNegocio = New AportanteNegocio

    Public Const CONST_TITULO_FONDO As String = "Grupo de Aportantes"
    Public Const CONST_TITULO_MODAL_MODIFICAR As String = "Modificar Grupo de Aportantes"
    Public Const CONST_TITULO_MODAL_ElIMINAR As String = "Eliminar Grupo de Aportantes"
    Public Const CONST_TITULO_MODAL_CREAR As String = "Nuevo Grupo de Aportantes"

    Public Const CONST_AGREGAR_EXISTE_EN_OTRA_GRUPO As String = "Aportante se encuentra en otro Grupo"
    Public Const CONST_AGREGAR_EXISTE_EN_LA_GRILLA As String = "El Aportante ya se encuentra en la lista"
    Public Const CONST_ERROR_AL_GUARDAR As String = "Error al [accion] el Grupo de Aportantes"
    Public Const CONST_EXITO_AL_GUARDAR As String = "Grupo de Aportantes [accion] con éxito"
    Public Const CONST_LISTAS_DROPDOWN As String = "La lista de aportantes vacia"

    Public Const CONST_SIN_RESULTADOS As String = "No se obtuvieron resultados de la búsqueda"

    Public Const CONST_COL_IDGRUPO As Integer = 1
    Public Const CONST_COL_NOMBRE_GRUPO As Integer = 2
    Public Const CONST_COL_RUT As Integer = 3
    Public Const CONST_COL_RAZON_SOCIAL As Integer = 4
    Public Const CONST_COL_ESTADO As Integer = 5
    Public Const CONST_COL_FECHA_CREACION As Integer = 6
    Public Const CONST_COL_USUARIO_CREACION As Integer = 7
    Public Const CONST_COL_FECHA_MODIFICACION As Integer = 8
    Public Const CONST_COL_USUARIO_MODIFICACION As Integer = 9
    Public Const CONST_GRILLA_ASIGNACION_VACIA As String = "No hay elementos para almacenar"
    Public Const CONST_NOMBRE_DE_GRUPO_VACIO As String = "Para almacenar los cambios el Nombre de Grupo no puede estar vacio o rellenado colo con espacios."

    Private Sub Presentacion_Mantenedores_frmMantenedorGrupoAportantes_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            DataInitial()
        End If

        ValidaPermisosPerfil()
    End Sub

    Private Sub DataInitial()
        txtFeCreacionDesde.Text = ""
        txtFeCreacionHasta.Text = ""
        txtAccionHidden.Value = ""
        'TODO: SE VALIDA QUE CALENDARIOS DEL FILTRO INICIEN OCULTOS
        Calendar1.Visible = False
        Calendar2.Visible = False
        CargaFiltroIdGrupo()
        CargaFiltroNombre()
        GrvTabla.DataSource = New List(Of AportantesXGrupoDTO)
        GrvTabla.DataBind()

        BtnExportar.Enabled = (GrvTabla.Rows.Count <> 0)
        ValidaPermisosPerfil()
    End Sub

    Private Sub CargaFiltroNombre()
        Dim grupo As New GrupoAportanteDTO
        Dim lista As List(Of GrupoAportanteDTO) = negocioGrupoAportante.GetListaGrupoAportante(grupo)

        If lista.Count = 0 Then
            ddlNombreGrupo.Items.Insert(0, New ListItem("", ""))
        Else
            ddlNombreGrupo.DataSource = lista
            ddlNombreGrupo.DataMember = "GPA_Descripcion"
            ddlNombreGrupo.DataValueField = "GPA_Descripcion"
            ddlNombreGrupo.DataBind()
            ddlNombreGrupo.Items.Insert(0, New ListItem("", ""))
        End If
    End Sub

    Private Sub CargaFiltroIdGrupo()
        Dim grupo As New GrupoAportanteDTO
        Dim grupoVacio As New GrupoAportanteDTO
        Dim lista As List(Of GrupoAportanteDTO) = negocioGrupoAportante.GetListaGrupoAportante(grupo)


        If lista.Count = 0 Then
            lista.Add(grupoVacio)
        Else
            lista.Insert(0, grupoVacio)
        End If

        ddlIdGrupo.DataSource = lista
            ddlIdGrupo.DataMember = "IdDescripcion"
            ddlIdGrupo.DataValueField = "IdDescripcion"
            ddlIdGrupo.DataTextField = "IdDescripcion"
            ddlIdGrupo.DataBind()
            ddlIdGrupo.Items.Insert(0, New ListItem("", ""))

    End Sub

    Private Sub ValidaPermisosPerfil()
        HiddenPerfil.Value = Session("PERFIL")
        HiddenConstante.Value = Constantes.CONST_PERFIL_CONSULTA

        If Session("PERFIL") = Constantes.CONST_PERFIL_CONSULTA Or Session("PERFIL") = Nothing Then
            BtnCrear.Enabled = False
            BtnModificar.Enabled = False
            BtnEliminar.Enabled = False
            BtnExportar.Enabled = False

        ElseIf Session("PERFIL") = Constantes.CONST_PERFIL_FULL Or Session("PERFIL") = Constantes.CONST_PERFIL_ADMIN Then
            BtnCrear.Visible = True
            BtnModificar.Visible = True
            BtnEliminar.Visible = True
            BtnExportar.Visible = True
        End If
    End Sub

    Private Sub GrvTabla_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles GrvTabla.PageIndexChanging
    End Sub

    Protected Sub BtnBuscar_Click(sender As Object, e As EventArgs) Handles BtnBuscar.Click
        txtAccionHidden.Value = ""
        FindAportantesXGrupo()

        If GrvTabla.Rows.Count <> 0 Then
            BtnExportar.Enabled = True
        Else
            BtnExportar.Enabled = False
            ShowAlert(CONST_SIN_RESULTADOS, True)
        End If

    End Sub

    Protected Sub btnCrear_Click(sender As Object, e As EventArgs) Handles BtnCrear.Click
        FormateoEstiloFormCrear()
        LlenarListaModal()
        FormateoLimpiarDatosModal()

    End Sub

    Protected Sub BtnModificar_Click(sender As Object, e As EventArgs) Handles BtnModificar.Click
        LlenarListaModal()
        Dim aportanteXGrupoSelect As AportantesXGrupoDTO = GetAportanteXGrupoSelect(GrvTabla)
        FormateoEstiloFormModificar()
        FormateoFormDatosModificar(aportanteXGrupoSelect)
    End Sub

    Protected Sub btnLimpiarFrm_Click(sender As Object, e As EventArgs) Handles btnLimpiarFrm.Click
        DataInitial()
    End Sub

    Protected Sub BtnExportar_Click(sender As Object, e As EventArgs) Handles BtnExportar.Click

        Dim grupoAportante As AportantesXGrupoDTO = New AportantesXGrupoDTO()
        Dim fechaHasta As Nullable(Of Date)
        Dim negocio As GrupoAportanteNegocio = New GrupoAportanteNegocio()

        txtFeCreacionDesde.Text = Request.Form(txtFeCreacionDesde.UniqueID)
        txtFeCreacionHasta.Text = Request.Form(txtFeCreacionHasta.UniqueID)

        If ddlIdGrupo.SelectedValue.Trim() = Nothing Then
            grupoAportante.IdGrupo = 0
        Else
            If (ddlIdGrupo.SelectedIndex > 0) Then
                Dim arrCadena As String() = ddlIdGrupo.SelectedItem.Text().Split(New Char() {"/"c})
                grupoAportante.IdGrupo = arrCadena(0).Trim()
            End If

        End If

        If ddlNombreGrupo.SelectedValue.Trim() = Nothing Then
            grupoAportante.NombreGrupo = ""
        Else
            grupoAportante.NombreGrupo = ddlNombreGrupo.SelectedValue.Trim()
        End If

        If Not txtFeCreacionDesde.Text.Equals("") Then
            grupoAportante.FechaIngreso = Date.Parse(txtFeCreacionDesde.Text)
        Else
            grupoAportante.FechaIngreso = Nothing
        End If

        If Not txtFeCreacionHasta.Text.Equals("") Then
            fechaHasta = Date.Parse(txtFeCreacionHasta.Text)
        Else
            fechaHasta = Nothing
        End If

        Dim mensaje As String = negocio.ExportarAExcel(grupoAportante, fechaHasta)

        If negocio.rutaArchivosExcel IsNot Nothing Then
            linkArchivo.NavigateUrl = negocio.rutaArchivosExcel
            linkArchivo.Text = "Bajar Archivo"
        Else
            linkArchivo.Visible = False
        End If

        ShowMesagges(CONST_TITULO_FONDO, mensaje, Constantes.CONST_RUTA_IMG + Constantes.CONST_IMG_LOGO, Constantes.CONST_RUTA_IMG + Constantes.CONST_IMG_INFO, False)

    End Sub

    Protected Sub BtnEliminar_Click(sender As Object, e As EventArgs) Handles BtnEliminar.Click
        grvAsignacion.Columns(0).Visible = True
        Dim aportanteXGrupoSelect As AportantesXGrupoDTO = GetAportanteXGrupoSelect(GrvTabla)
        FormateoEstiloFormEliminar()
        FormateoFormDatosModificar(aportanteXGrupoSelect)
    End Sub

    Protected Sub btnModalGuardar_Click(sender As Object, e As EventArgs) Handles btnModalGuardar.Click

        If grvAsignacion.Rows.Count = 0 Then ' And txtAccionHidden.Value = "CREAR" Then
            ShowAlert(CONST_GRILLA_ASIGNACION_VACIA)
            Exit Sub
        End If

        If txtModalNombreGrupo.Text.Trim = "" Then
            ShowAlert(CONST_NOMBRE_DE_GRUPO_VACIO)
            Exit Sub
        End If

        Dim listaGrupoXAporntates As List(Of AportantesXGrupoDTO) = New List(Of AportantesXGrupoDTO)
        Dim listaGrupoXAporntatesEliminados As List(Of AportantesXGrupoDTO) = New List(Of AportantesXGrupoDTO)
        Dim grupoAportante As GrupoAportanteDTO = New GrupoAportanteDTO()

        If Session("lista") IsNot Nothing Then
            listaGrupoXAporntates = Session("lista")
        End If
        If Session("listaEliminar") IsNot Nothing Then
            listaGrupoXAporntatesEliminados = Session("listaEliminar")
        End If

        grupoAportante.GPA_UsuarioIngreso = Session("NombreUsuario")
        grupoAportante.GPA_UsuarioModificacion = Session("NombreUsuario")


        grupoAportante.GPA_Descripcion = txtModalNombreGrupo.Text
        grupoAportante.GPA_Estado = 1


        Dim accion As String
        Dim mensajeAccionExito As String
        Dim mensajeAccionError As String
        If txtAccionHidden.Value = "CREAR" Then
            grupoAportante.GPA_Id = 0
            accion = "AGREGAR_GRUPO"
            mensajeAccionExito = CONST_EXITO_AL_GUARDAR.Replace("[accion]", "insertado")
            mensajeAccionError = CONST_ERROR_AL_GUARDAR.Replace("[accion]", "insertar")

        ElseIf txtAccionHidden.Value = "MODIFICAR" Then
            grupoAportante.GPA_Id = Integer.Parse(txtModalIdGrupo.Text)
            accion = "MODIFICAR_GRUPO"
            mensajeAccionExito = CONST_EXITO_AL_GUARDAR.Replace("[accion]", "modificado")
            mensajeAccionError = CONST_ERROR_AL_GUARDAR.Replace("[accion]", "modificar")

        ElseIf txtAccionHidden.Value = "ELIMINAR" Then
            grupoAportante.GPA_Id = Integer.Parse(txtModalIdGrupo.Text)
            accion = "ELIMINAR_APORTANTE_DE_GRUPO"
            mensajeAccionExito = CONST_EXITO_AL_GUARDAR.Replace("[accion]", "eliminado")
            mensajeAccionError = CONST_ERROR_AL_GUARDAR.Replace("[accion]", "eliminar")

        Else
            txtAccionHidden.Value = ""
            Exit Sub
        End If

        If (negocioGrupoAportante.GuardarGrupoDeAporntates(accion, grupoAportante, listaGrupoXAporntates, listaGrupoXAporntatesEliminados)) Then
            CargaFiltroNombre()
            CargaFiltroIdGrupo()
            ShowAlert(mensajeAccionExito)
        Else
            ShowAlert(mensajeAccionError)
        End If
        txtAccionHidden.Value = ""
        DataInitial()
    End Sub

    Private Sub btnModalAgregar_Click(sender As Object, e As EventArgs) Handles btnModalAgregar.Click
        AgregarElementoGridViewAsignacion()
    End Sub

    Private Sub btnModalModificar_Click(sender As Object, e As EventArgs) Handles btnModalModificar.Click
        Dim agregadoCorrectamente As Boolean
        Dim aXgAnterior As AportantesXGrupoDTO = GetAportanteXGrupoSelect(grvAsignacion)

        agregadoCorrectamente = AgregarElementoGridViewAsignacion()

        If (agregadoCorrectamente) Then
            EliminarElementoGridViewAsignacion(aXgAnterior)
        End If
        DataInitial()
    End Sub

    Protected Sub btnModalEliminarGrupo_Click(sender As Object, e As EventArgs) Handles btnModalEliminarGrupo.Click
        Dim grupoAportante As GrupoAportanteDTO = New GrupoAportanteDTO
        Dim mensaje As String
        grupoAportante.GPA_UsuarioIngreso = Session("NombreUsuario")
        grupoAportante.GPA_UsuarioModificacion = Session("NombreUsuario")
        grupoAportante.GPA_Id = txtModalIdGrupo.Text

        txtAccionHidden.Value = ""
        If negocioGrupoAportante.EliminarGrupoDeAportantes(grupoAportante) = Constantes.CONST_OPERACION_EXITOSA Then
            CargaFiltroIdGrupo()
            CargaFiltroNombre()
            mensaje = CONST_EXITO_AL_GUARDAR.Replace("[accion]", "eliminado")
            ShowAlert(mensaje)
        Else
            mensaje = CONST_EXITO_AL_GUARDAR.Replace("[accion]", "eliminado")
            ShowAlert(mensaje)
        End If
        DataInitial()
    End Sub

    Private Sub btnModalEliminarAportante_Click(sender As Object, e As EventArgs) Handles btnModalEliminarAportante.Click
        Dim aXgAnterior As AportantesXGrupoDTO = GetAportanteXGrupoSelect(grvAsignacion)
        EliminarElementoGridViewAsignacion(aXgAnterior)
    End Sub

    Protected Sub LinkButton1_Click(sender As Object, e As EventArgs)
        Calendar1.Visible = (Not Calendar1.Visible)
    End Sub

    Protected Sub LinkButton2_Click(sender As Object, e As EventArgs)
        Calendar2.Visible = (Not Calendar2.Visible)
    End Sub

    Protected Sub Calendar1_SelectionChanged(sender As Object, e As EventArgs) Handles Calendar1.SelectionChanged
        txtFeCreacionDesde.Text = Calendar1.SelectedDate.ToShortDateString()
        'TODO: CORRECCION VALIDACION DE COMPARACION FECHAS
        If txtFeCreacionHasta.Text <> "" And txtFeCreacionDesde.Text <> "" Then
            If Date.Parse(txtFeCreacionHasta.Text) < Date.Parse(txtFeCreacionDesde.Text) Then
                txtFeCreacionHasta.Text = Calendar1.SelectedDate.ToShortDateString()
            End If
        End If
        Calendar1.SelectedDate = Nothing
        Calendar1.Visible = False
    End Sub

    Protected Sub Calendar2_SelectionChanged(sender As Object, e As EventArgs) Handles Calendar2.SelectionChanged
        txtFeCreacionHasta.Text = Calendar2.SelectedDate.ToShortDateString()
        'TODO: CORRECCION VALIDACION DE COMPARACION FECHAS
        If txtFeCreacionDesde.Text <> "" And txtFeCreacionHasta.Text <> "" Then
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

    Private Sub FindAportantesXGrupo()
        Dim grupoAportante As AportantesXGrupoDTO = New AportantesXGrupoDTO()
        Dim fechaHasta As Nullable(Of Date)

        txtFeCreacionDesde.Text = Request.Form(txtFeCreacionDesde.UniqueID)
        txtFeCreacionHasta.Text = Request.Form(txtFeCreacionHasta.UniqueID)

        If ddlIdGrupo.SelectedValue.Trim() = Nothing Then
            grupoAportante.IdGrupo = 0
        Else
            If (ddlIdGrupo.SelectedIndex > 0) Then
                Dim arrCadena As String() = ddlIdGrupo.SelectedItem.Text().Split(New Char() {"/"c})

                grupoAportante.IdGrupo = arrCadena(0).Trim()
                grupoAportante.NombreGrupo = arrCadena(1).Trim()
                'aportante.RazonSocial = ddlNombre.SelectedValue.Trim()
            Else
                grupoAportante.IdGrupo = ""
                grupoAportante.NombreGrupo = ""
            End If
        End If

        If Not txtFeCreacionDesde.Text.Equals("") Then
            grupoAportante.FechaIngreso = Date.Parse(txtFeCreacionDesde.Text)
        Else
            grupoAportante.FechaIngreso = Nothing
        End If

        If Not txtFeCreacionHasta.Text.Equals("") Then
            fechaHasta = Date.Parse(txtFeCreacionHasta.Text)
        Else
            fechaHasta = Nothing
        End If

        GrvTabla.DataSource = negocioGrupoAportante.GetListaGrupoAportanteFiltro(grupoAportante, fechaHasta)
        GrvTabla.DataBind()
    End Sub

    Private Sub FormateoEstiloFormCrear()
        grvAsignacion.Columns(0).Visible = False

        btnModalEliminarAportante.Enabled = False
        btnModalEliminarGrupo.Enabled = False
        btnModalModificar.Enabled = False
        btnModalAgregar.Enabled = False
        btnModalGuardar.Enabled = False
        txtModalNombreGrupo.Enabled = True
        'ddlModalNombreAportante.Enabled = True
        ddlModalRutAportantes.Enabled = True

        btnModalAgregar.Visible = True
        btnModalModificar.Visible = True
        btnModalEliminarAportante.Visible = True

        lblModalTitulo.Text = CONST_TITULO_MODAL_CREAR
        txtAccionHidden.Value = "CREAR"
        txtModalNombreGrupo.Text = ""
    End Sub

    Private Sub FormateoEstiloFormModificar()
        grvAsignacion.Columns(0).Visible = True

        btnModalEliminarAportante.Enabled = False
        btnModalEliminarGrupo.Enabled = False
        btnModalModificar.Enabled = False
        btnModalAgregar.Enabled = True
        btnModalGuardar.Enabled = False
        txtModalNombreGrupo.Enabled = True
        'ddlModalNombreAportante.Enabled = True
        ddlModalRutAportantes.Enabled = True

        btnModalAgregar.Visible = True
        btnModalModificar.Visible = True
        btnModalEliminarAportante.Visible = True

        lblModalTitulo.Text = CONST_TITULO_MODAL_MODIFICAR
        txtAccionHidden.Value = "MODIFICAR"
        txtModalNombreGrupo.Text = ""
    End Sub

    Private Sub FormateoEstiloFormEliminar()
        grvAsignacion.Columns(0).Visible = True

        btnModalEliminarAportante.Enabled = False
        btnModalEliminarGrupo.Enabled = True
        btnModalModificar.Enabled = False
        btnModalAgregar.Enabled = False
        btnModalGuardar.Enabled = False
        txtModalNombreGrupo.Enabled = False
        'ddlModalNombreAportante.Enabled = False
        ddlModalRutAportantes.Enabled = False

        btnModalAgregar.Visible = True
        btnModalModificar.Visible = True
        btnModalEliminarAportante.Visible = True

        lblModalTitulo.Text = CONST_TITULO_MODAL_ElIMINAR
        txtAccionHidden.Value = "ELIMINAR"
        txtModalNombreGrupo.Text = ""
    End Sub

    Private Function AgregarElementoGridViewAsignacion() As Boolean
        Dim lista As New List(Of AportantesXGrupoDTO)
        Dim listaEliminar As New List(Of AportantesXGrupoDTO)
        Dim aportanteXGrupo As New AportantesXGrupoDTO
        Dim resultadoAgregar As Boolean = False
        Dim poseeOtroGrupo As Boolean

        If Session("lista") IsNot Nothing Then
            lista = Session("lista")
        Else
            lista = New List(Of AportantesXGrupoDTO)
        End If

        If ddlModalRutAportantes.SelectedIndex > -1 Then ' And ddlModalNombreAportante.SelectedIndex > -1 Then
            If txtModalIdGrupo.Text IsNot "" Then
                aportanteXGrupo.IdGrupo = txtModalIdGrupo.Text
            End If

            aportanteXGrupo.NombreGrupo = txtModalNombreGrupo.Text
            aportanteXGrupo.NombreAportante = "" ' ddlModalNombreAportante.SelectedValue
            aportanteXGrupo.RutAportante = ddlModalRutAportantes.SelectedValue
            aportanteXGrupo.Estado = Constantes.CONST_HABILITADO
            aportanteXGrupo.UsuarioIngreso = Session("NombreUsuario")
            aportanteXGrupo.UsuarioModificacion = Session("NombreUsuario")

            Dim existeEnGrilla As Boolean = ExisteEnGrillaAsignacion(lista, aportanteXGrupo)

            If (Session("listaEliminar") IsNot Nothing) Then
                listaEliminar = Session("listaEliminar")
                Dim objetoEliminar = listaEliminar.FirstOrDefault(Function(t) t.IdGrupo = aportanteXGrupo.IdGrupo And t.RutAportante = aportanteXGrupo.RutAportante)

                If objetoEliminar Is Nothing Then
                    poseeOtroGrupo = negocioGrupoAportante.AportanteExisteEnOtroGrupo(aportanteXGrupo)
                Else
                    poseeOtroGrupo = False
                    listaEliminar.Remove(objetoEliminar)
                    Session("listaEliminar") = listaEliminar
                End If
            Else
                poseeOtroGrupo = negocioGrupoAportante.AportanteExisteEnOtroGrupo(aportanteXGrupo)
            End If

            If existeEnGrilla Then
                ShowAlert(CONST_AGREGAR_EXISTE_EN_LA_GRILLA)
            ElseIf poseeOtroGrupo Then
                ShowAlert(CONST_AGREGAR_EXISTE_EN_OTRA_GRUPO)
            Else
                lista.Add(aportanteXGrupo)

                'cambia el nombre por todos los elementos de la grilla

                For Each obj In lista
                    obj.NombreGrupo = aportanteXGrupo.NombreGrupo
                Next

                Session("lista") = lista

                grvAsignacion.DataSource = lista
                grvAsignacion.DataBind()

                btnModalGuardar.Enabled = (grvAsignacion.Rows.Count > 0)

                ComprobarCountListaAsignacion()
                resultadoAgregar = True
                btnModalGuardar.Enabled = True

            End If

        Else
                ShowAlert(CONST_LISTAS_DROPDOWN)
        End If

        Return resultadoAgregar
    End Function

    Private Sub EliminarElementoGridViewAsignacion(aXgAnterior As AportantesXGrupoDTO)
        Dim listaEliminar As New List(Of AportantesXGrupoDTO)

        If Session("listaEliminar") IsNot Nothing Then
            listaEliminar = Session("listaEliminar")
        End If


        Dim lista As List(Of AportantesXGrupoDTO) = Session("lista")


        Dim objetoEliminar = lista.FirstOrDefault(Function(t) t.IdGrupo = aXgAnterior.IdGrupo And t.RutAportante = aXgAnterior.RutAportante)

        lista.Remove(objetoEliminar)

        listaEliminar.Add(objetoEliminar)
        Session("listaEliminar") = listaEliminar

        Session("lista") = lista

        grvAsignacion.DataSource = lista
        grvAsignacion.DataBind()
    End Sub

    Private Sub ComprobarCountListaAsignacion()
        If Session("lista") IsNot Nothing And txtAccionHidden.Value = "CREAR" Then
            txtModalNombreGrupo.Enabled = False
            btnModalAgregar.Enabled = True
        Else
            txtModalNombreGrupo.Enabled = True
        End If
    End Sub

    Private Sub FormateoLimpiarDatosModal()
        txtModalIdGrupo.Text = ""
        txtModalNombreGrupo.Text = ""
        ddlModalRutAportantes.SelectedIndex = 0
        'ddlModalNombreAportante.SelectedIndex = 0
        txtModalNombreGrupo.Enabled = True
        grvAsignacion.DataSource = Nothing
        grvAsignacion.DataBind()
        Session("lista") = Nothing
    End Sub

    Private Function GetAportanteXGrupoSelect(tabla As GridView) As AportantesXGrupoDTO
        Dim grupo As New AportantesXGrupoDTO
        For Each row As GridViewRow In tabla.Rows
            Dim chk As RadioButton = row.Cells(0).Controls(1)
            If chk IsNot Nothing And chk.Checked Then
                grupo.IdGrupo = row.Cells(CONST_COL_IDGRUPO).Text.Trim()
                grupo.NombreGrupo = HttpUtility.HtmlDecode(row.Cells(CONST_COL_NOMBRE_GRUPO).Text.Trim())
                grupo.RutAportante = row.Cells(CONST_COL_RUT).Text.Trim()
                ' grupo.NombreAportante = row.Cells(CONST_COL_RAZON_SOCIAL).Text.Trim()
            End If
        Next

        Return grupo
    End Function

    Private Function ExisteEnGrillaAsignacion(lista As List(Of AportantesXGrupoDTO), aportantesXGrupo As AportantesXGrupoDTO) As Boolean
        Dim findAportanteXGrupo As AportantesXGrupoDTO = lista.Find(Function(l) l.RutAportante = aportantesXGrupo.RutAportante)
        Return (findAportanteXGrupo IsNot Nothing)

    End Function

    Private Sub LlenarListaModal()
        Dim negocioAportante As AportanteNegocio = New AportanteNegocio()

        Dim aportante As AportanteDTO = New AportanteDTO()
        Dim ListaAportantes As List(Of AportanteDTO) = negocioAportante.GetListaAportantesDistinct(aportante)
        Dim ListaAportantesDistinct As List(Of AportanteDTO)

        ListaAportantesDistinct = negocioAportante.GetListaAportantesPorRut(aportante)

        LLenaComboRutAportante(ListaAportantesDistinct)

        LlenaComboNombreAportante(ListaAportantes)
    End Sub

    Private Sub FormateoFormDatosModificar(aportantesXGrupo As AportantesXGrupoDTO)
        Dim fechaHasta As Nullable(Of Date)
        'txtFeCreacionDesde.Text = Request.Form(txtFeCreacionDesde.UniqueID)
        txtFeCreacionHasta.Text = Request.Form(txtFeCreacionHasta.UniqueID)

        txtModalIdGrupo.Text = aportantesXGrupo.IdGrupo

        txtModalNombreGrupo.Text = aportantesXGrupo.NombreGrupo
        txtHiddenOldValue.Value = txtModalNombreGrupo.Text

        If Not txtFeCreacionHasta.Text.Equals("") Then
            fechaHasta = Date.Parse(txtFeCreacionHasta.Text)
        Else
            fechaHasta = Nothing
        End If

        Dim lista As List(Of AportantesXGrupoDTO) = negocioGrupoAportante.GetListaGrupoAportanteFiltro(aportantesXGrupo, fechaHasta)
        grvAsignacion.DataSource = lista
        grvAsignacion.DataBind()

        Session("lista") = lista
        Session("listaEliminar") = Nothing
    End Sub

    Private Sub FormateoFormDatosEliminar(aportantesXGrupo As AportantesXGrupoDTO)
        Dim fechaHasta As Nullable(Of Date)
        'txtFeCreacionDesde.Text = Request.Form(txtFeCreacionDesde.UniqueID)
        txtFeCreacionHasta.Text = Request.Form(txtFeCreacionHasta.UniqueID)


        txtModalIdGrupo.Text = aportantesXGrupo.IdGrupo
        txtModalNombreGrupo.Text = aportantesXGrupo.NombreGrupo

        If Not txtFeCreacionHasta.Text.Equals("") Then
            fechaHasta = Date.Parse(txtFeCreacionHasta.Text)
        Else
            fechaHasta = Nothing
        End If

        Dim lista As List(Of AportantesXGrupoDTO) = negocioGrupoAportante.GetListaGrupoAportanteFiltro(aportantesXGrupo, fechaHasta)
        grvAsignacion.DataSource = lista
        grvAsignacion.DataBind()

        Session("lista") = lista
        Session("listaEliminar") = Nothing

        txtAccionHidden.Value = "ELIMINAR"
        lblModalTitulo.Text = CONST_TITULO_MODAL_ElIMINAR
    End Sub

    Private Sub ShowMesagges(title As String, mesagge As String, urlIconTitle As String, urlIconMesagge As String, Optional borraLink As Boolean = True)
        lblModalTitle.Text = title
        lblModalBody.Text = mesagge
        img_modal.ImageUrl = urlIconTitle
        img_body_modal.ImageUrl = urlIconMesagge

        linkArchivo.Visible = Not (borraLink)
        txtAccionHidden.Value = "SHOW_DIALOG"

        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModalmg", "$('#myModalmg').modal();", True)
    End Sub

    Private Sub ShowAlert(mesagge As String, Optional mostrarEnPage As Boolean = False)
        Dim myScript As String = "alert('" + mesagge + "');"
        If Not mostrarEnPage Then
            ScriptManager.RegisterStartupScript(UpdatePanelGrilla, UpdatePanelGrilla.GetType(), "alert", myScript, True)
        Else
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", myScript, True)
        End If
    End Sub

    Protected Sub btnVolver_Click(sender As Object, e As EventArgs) Handles btnVolver.Click
        Response.Redirect("~/blank.aspx")
    End Sub

    Private Sub btnModalCancelar_Click(sender As Object, e As EventArgs) Handles btnModalCancelar.Click
        txtAccionHidden.Value = ""
    End Sub

    Private Sub LLenaComboRutAportante(ListaAportantesDistinct As List(Of AportanteDTO))

        ddlModalRutAportantes.DataSource = ListaAportantesDistinct
        ddlModalRutAportantes.DataMember = "Rut"
        ddlModalRutAportantes.DataValueField = "Rut"
        ddlModalRutAportantes.DataTextField = "RutRazonSocial"
        ddlModalRutAportantes.DataBind()
    End Sub

    Private Sub LlenaComboNombreAportante(ListaAportantes As List(Of AportanteDTO))
    End Sub

    Protected Sub BtnLimpiarFechaDesde_Click(sender As Object, e As EventArgs) Handles BtnLimpiarFechaDesde.Click
        txtFeCreacionDesde.Text = Nothing
    End Sub

    Protected Sub BtnLimpiarFechaHasta_Click(sender As Object, e As EventArgs) Handles BtnLimpiarFechaHasta.Click
        txtFeCreacionHasta.Text = Nothing
    End Sub
End Class
