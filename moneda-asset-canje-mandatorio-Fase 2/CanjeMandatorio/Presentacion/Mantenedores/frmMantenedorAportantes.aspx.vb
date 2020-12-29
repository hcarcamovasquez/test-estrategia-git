Imports DTO
Imports Negocio

Partial Class Mantenedores_Aportantes_Maestro
    Inherits System.Web.UI.Page

    Private ReadOnly Negocio As AportanteNegocio = New AportanteNegocio

    Public Const CONST_TITULO_APORTANTE As String = "Aportantes"
    Public Const CONST_TITULO_MODAL_MODIFICAR As String = "Modificar Aportante"
    Public Const CONST_TITULO_MODAL_ElIMINAR As String = "Eliminar Aportante"
    Public Const CONST_TITULO_MODAL_CREAR As String = "Nuevo Aportante"

    Public Const CONST_MODIFICAR_ERROR As String = "Error al modificar los datos del Aportante"
    Public Const CONST_MODIFICAR_EXITO As String = "Aportante modificado con éxito"
    Public Const CONST_ELIMINAR_ERROR As String = "Error al eliminar el Aportante"
    Public Const CONST_ELIMINAR_EXITO As String = "Aportante eliminado con éxito"
    Public Const CONST_MENSAJE_EXISTE_RELACION As String = "Aportante se encuentra relacionado en otra Tabla"
    Public Const CONST_INSERTAR_ERROR As String = "Error al ingresar el Aportante"
    Public Const CONST_INSERTAR_EXITO As String = "Aportante ingresado con éxito"
    Public Const CONST_ELIMINAR_ESTADO_CERO As String = "No se puede eliminar un registro ya deshabilitado"

    Public Const CONST_VALIDA_RUT_SI_MULTIFONDO_BLANCO As String = "RUT existe en la base de aportantes, para grabar debe llenar el campo Multifondo"
    Public Const CONST_VALIDA_RUT_SI_MULTIFONDO_SI As String = "RUT y Multifondo ya existen en la base de aportantes"


    Public Const CONST_COL_RUT As Integer = 1
    Public Const CONST_COL_RAZONSOCIAL As Integer = 2
    Public Const CONST_COL_MULTIFONDO As Integer = 3
    Public Const CONST_COL_NACEXT As Integer = 4
    Public Const CONST_COL_CALIFICADO As Integer = 5
    Public Const CONST_COL_INTERMEDIARIO As Integer = 6
    Public Const CONST_COL_RELACIONMAN As Integer = 7
    Public Const CONST_COL_DISTRIBUCION As Integer = 8
    Public Const CONST_COL_DOCUMENTACION As Integer = 9
    Public Const CONST_COL_ESTADO As Integer = 10
    Public Const CONST_COL_FECHAINGRESO As Integer = 11
    Public Const CONST_SIN_RESULTADOS As String = "No se obtuvieron resultados de la búsqueda"
    Public Const CONST_LLENAR_TODOS_LOS_CAMPOS As String = "Debe llenar todos los campos"

    Private Sub Mantenedores_Aportantes_Maestro_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            DataInitial()
        End If
        ValidaPermisosPerfil()
    End Sub

    Private Sub DataInitial()
        txtFeCreacionDesde.Text = ""
        txtFeCreacionHasta.Text = ""
        'TODO: SE VALIDA QUE CALENDARIOS DEL FILTRO INICIEN OCULTOS
        'Calendar1.Visible = False
        'Calendar2.Visible = False
        CargaFiltroAportante()

        GrvTabla.DataSource = New List(Of AportantesXGrupoDTO)
        GrvTabla.DataBind()

        BtnExportar.Enabled = (GrvTabla.Rows.Count <> 0)
        ValidaPermisosPerfil()
    End Sub

    Private Sub CargaFiltroAportante()
        Dim lista As List(Of AportanteDTO) = GetAportantesPorRut()
        Dim aportanteVacio As AportanteDTO = New AportanteDTO()

        If lista.Count = 0 Then
            lista.Add(aportanteVacio)
        Else
            lista.Insert(0, aportanteVacio)
        End If

        ddlRutAportante.DataSource = lista
        ddlRutAportante.DataMember = "RutRazonSocial"
        ddlRutAportante.DataValueField = "RutRazonSocial"
        ddlRutAportante.DataTextField = "RutRazonSocial"
        ddlRutAportante.DataBind()

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

    Private Sub GrvTabla_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles GrvTabla.PageIndexChanging
    End Sub

    Private Sub FindAportantes()
        Dim aportante As AportanteDTO = New AportanteDTO()
        Dim fechaHasta As Nullable(Of Date)

        txtFeCreacionDesde.Text = Request.Form(txtFeCreacionDesde.UniqueID)
        txtFeCreacionHasta.Text = Request.Form(txtFeCreacionHasta.UniqueID)

        If (ddlRutAportante.SelectedIndex > 0) Then
            Dim arrCadena As String() = ddlRutAportante.SelectedItem.Text().Split(New Char() {"/"c})

            aportante.Rut = arrCadena(0).Trim()
            aportante.RazonSocial = arrCadena(1).Trim()
        Else
            aportante.Rut = ""
            aportante.RazonSocial = ""
        End If


        If Not txtFeCreacionDesde.Text.Equals("") Then
            aportante.FechaIngreso = Date.Parse(txtFeCreacionDesde.Text)
        Else
            aportante.FechaIngreso = Nothing
        End If

        If Not txtFeCreacionHasta.Text.Equals("") Then
            fechaHasta = Date.Parse(txtFeCreacionHasta.Text)
        Else
            fechaHasta = Nothing
        End If

        Dim ListaAportantes = Negocio.GetListaAportantes(aportante, fechaHasta)

        For Each item As AportanteDTO In ListaAportantes
            Dim documentacion = item.Documentacion
            Dim contiene = documentacion.Contains("https://")
            If (contiene) Then
                Dim delete = documentacion.TrimStart("h", "t", "t", "p", "s", ":", "/", "/")
                item.Documentacion = delete
            End If
            'item.Documentacion = item.Documentacion.Replace("\", "/")
        Next

        GrvTabla.DataSource = ListaAportantes
        GrvTabla.DataBind()
    End Sub

    Protected Sub BtnBuscar_Click(sender As Object, e As EventArgs) Handles BtnBuscar.Click
        FindAportantes()

        If GrvTabla.Rows.Count = 0 Then
            BtnExportar.Enabled = False
            ShowAlert(CONST_SIN_RESULTADOS)
        Else
            BtnExportar.Enabled = True
        End If

    End Sub

    Protected Sub BtnEliminar_Click(sender As Object, e As EventArgs) Handles BtnEliminar.Click
        Dim aportanteSelect As AportanteDTO = GetAportanteSelect()
        Dim aportanteActualizado As AportanteDTO = Negocio.BuscarAportante(aportanteSelect)

        FormateoFormDatos(aportanteActualizado)
        FormateoEstiloFormEliminar()
    End Sub

    Protected Sub BtnModificar_Click(sender As Object, e As EventArgs) Handles BtnModificar.Click
        Dim aportanteSelect As AportanteDTO = GetAportanteSelect()
        Dim aportanteActualizado As AportanteDTO = Negocio.BuscarAportante(aportanteSelect)

        FormateoFormDatos(aportanteActualizado)
        FormateoEstiloFormModificar()
    End Sub

    Protected Sub btnCrear_Click(sender As Object, e As EventArgs)
        FormateoLimpiarDatosModal()
        FormateoEstiloFormCrear()
    End Sub


    Private Sub btnModalModificar_Click(sender As Object, e As EventArgs) Handles btnModalModificar.Click
        Dim aportante As AportanteDTO = GetAportanteModal()
        Dim solicitud As Integer = Negocio.UpdateAportante(aportante)

        If solicitud = Constantes.CONST_OPERACION_EXITOSA Then
            ShowAlert(CONST_MODIFICAR_EXITO)
        ElseIf solicitud = Constantes.CONST_ERROR_NO_SE_PUEDE_BORRAR Then
            ShowAlert(CONST_MENSAJE_EXISTE_RELACION)
            Exit Sub
        Else
            ShowAlert(CONST_MODIFICAR_ERROR)
            Exit Sub
        End If

        txtAccionHidden.Value = ""
        DataInitial()
    End Sub

    Private Sub btnModalEliminar_Click(sender As Object, e As EventArgs) Handles btnModalEliminar.Click
        Dim aportante As AportanteDTO = GetAportanteModal()
        If Not aportante.Estado = 0 Then
            Dim solicitud As Integer = Negocio.DeleteAportante(aportante)

            If solicitud = Constantes.CONST_OPERACION_EXITOSA Then
                ShowAlert(CONST_ELIMINAR_EXITO)
            ElseIf solicitud = Constantes.CONST_ERROR_NO_SE_PUEDE_BORRAR Then
                ShowAlert(CONST_MENSAJE_EXISTE_RELACION)
                Exit Sub
            Else
                ShowAlert(CONST_ELIMINAR_ERROR)
                Exit Sub
            End If
        Else
            ShowAlert(CONST_ELIMINAR_ESTADO_CERO)
        End If

        txtAccionHidden.Value = ""
        DataInitial()
    End Sub

    Private Sub btnModalGuardar_Click(sender As Object, e As EventArgs) Handles btnModalGuardar.Click
        Dim aportante As AportanteDTO = GetAportanteModal()
        Dim solicitud As Integer = Negocio.InsertarAportante(aportante)

        If solicitud = Constantes.CONST_OPERACION_EXITOSA Then
            'Ingresado con Exito
            ShowAlert(CONST_INSERTAR_EXITO)
            DataInitial()
            txtAccionHidden.Value = ""
        ElseIf solicitud = 1 Then
            'Rut existe y Multifondo en blanco
            ShowAlert(CONST_VALIDA_RUT_SI_MULTIFONDO_BLANCO)
            FormateoEstiloFormCrear()
            Exit Sub
        ElseIf solicitud = 2 Then
            'Rut existe y Multifondo existe
            ShowAlert(CONST_VALIDA_RUT_SI_MULTIFONDO_SI)
            FormateoEstiloFormCrear()
            Exit Sub
        Else
            'Error en la BBDD
            ShowAlert(CONST_INSERTAR_ERROR)
            FormateoEstiloFormCrear()
            Exit Sub
        End If
    End Sub

    Protected Sub RowSelector_CheckedChanged(sender As Object, e As EventArgs)
        For Each row As GridViewRow In GrvTabla.Rows
            Dim chk As RadioButton = row.Cells(0).Controls(1)
            If chk IsNot Nothing And chk.Checked Then
                Return
            End If
        Next
    End Sub

    Private Function GetAportanteSelect() As AportanteDTO
        Dim aportante As New AportanteDTO
        For Each row As GridViewRow In GrvTabla.Rows
            Dim chk As RadioButton = row.Cells(0).Controls(1)
            If chk IsNot Nothing And chk.Checked Then
                aportante.Rut = row.Cells(CONST_COL_RUT).Text.Trim()
                aportante.RazonSocial = HttpUtility.HtmlDecode(row.Cells(CONST_COL_RAZONSOCIAL).Text.Trim())
                aportante.Multifondo = HttpUtility.HtmlDecode(row.Cells(CONST_COL_MULTIFONDO).Text.Replace("&nbsp;", "").Trim)
                aportante.NacExt = row.Cells(CONST_COL_NACEXT).Text.Trim()
                aportante.Calificado = row.Cells(CONST_COL_CALIFICADO).Text.Trim()
                aportante.Intermediario = row.Cells(CONST_COL_INTERMEDIARIO).Text.Trim()
                aportante.RelacionMan = row.Cells(CONST_COL_RELACIONMAN).Text.Trim()
                aportante.Distribucion = row.Cells(CONST_COL_DISTRIBUCION).Text.Trim()
                ' aportante.Estado = row.Cells(CONST_COL_ESTADO).Text.Trim()
            End If
        Next

        Return aportante
    End Function

    Private Sub FormateoEstiloFormModificar()
        'DESHABILTIADOS
        txtRut.Enabled = False
        txtMultifondo.Enabled = False
        btnModalGuardar.Visible = False
        btnModalEliminar.Enabled = False

        'HABILITADOS
        btnModalEliminar.Visible = True
        btnModalModificar.Visible = True
        btnModalModificar.Enabled = True
        txtNombre.Enabled = True
        ddlNacExt.Enabled = True
        ddlInversionista.Enabled = True
        ddlTipoAportante.Enabled = True
        ddlRelacionado.Enabled = True
        ddlContrato.Enabled = True
        txtDocumentacion.Enabled = True

        txtAccionHidden.Value = "MODIFICAR"
        lblModalTitulo.Text = CONST_TITULO_MODAL_MODIFICAR
    End Sub

    Private Sub FormateoEstiloFormEliminar()
        btnModalEliminar.Visible = True
        btnModalModificar.Visible = True
        btnModalGuardar.Visible = False

        btnModalModificar.Enabled = False
        btnModalEliminar.Enabled = True

        txtRut.Enabled = False
        txtNombre.Enabled = False
        txtMultifondo.Enabled = False
        ddlNacExt.Enabled = False
        ddlInversionista.Enabled = False
        ddlTipoAportante.Enabled = False
        ddlRelacionado.Enabled = False
        ddlContrato.Enabled = False
        txtDocumentacion.Enabled = False

        txtAccionHidden.Value = "ELIMINAR"
        lblModalTitulo.Text = CONST_TITULO_MODAL_ElIMINAR
    End Sub

    Private Sub FormateoEstiloFormCrear()
        btnModalEliminar.Visible = False
        btnModalModificar.Visible = False
        btnModalGuardar.Visible = True

        btnModalModificar.Enabled = True

        txtRut.Enabled = True
        txtNombre.Enabled = True
        txtMultifondo.Enabled = True
        ddlNacExt.Enabled = True
        ddlInversionista.Enabled = True
        ddlTipoAportante.Enabled = True
        ddlRelacionado.Enabled = True
        ddlContrato.Enabled = True
        txtDocumentacion.Enabled = True

        txtAccionHidden.Value = "CREAR"
        lblModalTitulo.Text = CONST_TITULO_MODAL_CREAR
    End Sub

    Private Sub FormateoFormDatos(aportante As AportanteDTO)
        txtRut.Text = aportante.Rut
        txtNombre.Text = aportante.RazonSocial
        txtMultifondo.Text = aportante.Multifondo
        ddlNacExt.SelectedValue = aportante.NacExt
        txtHidenEstado.Value = aportante.Estado

        ddlTipoAportante.SelectedValue = aportante.Intermediario
        ddlRelacionado.SelectedValue = aportante.RelacionMan
        ddlContrato.SelectedValue = aportante.Distribucion
        txtFechaIngreso.Text = Format$(aportante.FechaIngreso, "dd-MM-yyyy")
        txtUsuarioIngreso.Text = aportante.UsuarioIngreso
        txtFechaModificacion.Text = Format$(aportante.FechaModificacion, "dd-MM-yyyy")
        txtUsuarioModificacion.Text = aportante.UsuarioModificacion
        txtDocumentacion.Text = aportante.Documentacion 'traer documento.

        If aportante.Calificado IsNot Nothing Then
            ddlInversionista.SelectedValue = aportante.Calificado.Trim()
        End If

    End Sub

    Private Sub FormateoLimpiarDatosModal()
        txtRut.Text = ""
        txtNombre.Text = ""
        txtMultifondo.Text = ""
        ddlNacExt.SelectedIndex = 0
        ddlInversionista.SelectedIndex = 0
        ddlTipoAportante.SelectedIndex = 0
        ddlRelacionado.SelectedIndex = 0
        ddlContrato.SelectedIndex = 0
        txtFechaIngreso.Text = ""
        txtUsuarioIngreso.Text = ""
        txtFechaModificacion.Text = ""
        txtUsuarioModificacion.Text = ""
        txtDocumentacion.Text = ""
    End Sub

    Private Function GetAportanteModal() As AportanteDTO
        Dim aportante As New AportanteDTO

        aportante.Rut = txtRut.Text
        aportante.RazonSocial = IIf(txtNombre.Text = "", Nothing, txtNombre.Text)
        'TODO aqui lo setea a nothing si el cliente lo dejo sin rellenar.
        aportante.Multifondo = IIf(txtMultifondo.Text.Trim() = "", Nothing, txtMultifondo.Text.Trim().ToUpper())
        aportante.NacExt = ddlNacExt.SelectedValue
        aportante.Calificado = ddlInversionista.SelectedValue
        aportante.Intermediario = ddlTipoAportante.SelectedValue
        aportante.RelacionMan = ddlRelacionado.SelectedValue
        aportante.Distribucion = ddlContrato.SelectedValue
        aportante.UsuarioIngreso = Session("NombreUsuario")
        aportante.UsuarioModificacion = Session("NombreUsuario")
        aportante.Documentacion = txtDocumentacion.Text
        aportante.Estado = IIf(txtHidenEstado.Value = "", 1, txtHidenEstado.Value)

        Return aportante
    End Function

    Protected Sub btnLimpiarFrm_Click(sender As Object, e As EventArgs)
        DataInitial()
    End Sub

    Private Sub ShowMesagges(title As String, mesagge As String, urlIconTitle As String, urlIconMesagge As String, Optional borraLink As Boolean = True)
        lblModalTitle.Text = title
        lblModalBody.Text = mesagge
        img_modal.ImageUrl = urlIconTitle
        img_body_modal.ImageUrl = urlIconMesagge

        linkArchivo.Visible = (Not borraLink)

        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModalmg", "$('#myModalmg').modal();", True)
    End Sub

    Private Function GetAportantesPorRut() As List(Of AportanteDTO)
        Dim aportante As New AportanteDTO()
        Return Negocio.GetListaAportantesPorRut(aportante)
    End Function

    Private Function GetAportantesPorNombre() As List(Of AportanteDTO)
        Dim aportante As New AportanteDTO()
        aportante.RazonSocial = ""
        Return Negocio.GetListaAportantesPorRazonSocial(aportante)
    End Function

    Protected Sub BtnExportar_Click(sender As Object, e As EventArgs) Handles BtnExportar.Click
        Dim aportante As AportanteDTO = New AportanteDTO()
        Dim fechaHasta As Nullable(Of Date)
        Dim arrCadena As String() = ddlRutAportante.SelectedItem.Text().Split(New Char() {"/"c})

        txtFeCreacionDesde.Text = Request.Form(txtFeCreacionDesde.UniqueID)
        txtFeCreacionHasta.Text = Request.Form(txtFeCreacionHasta.UniqueID)

        If ddlRutAportante.SelectedIndex > 0 Then
            aportante.Rut = arrCadena(0).Trim()
            aportante.RazonSocial = arrCadena(1).Trim()
        Else
            aportante = New AportanteDTO()
        End If

        If Not txtFeCreacionDesde.Text.Equals("") Then
            aportante.FechaIngreso = Date.Parse(txtFeCreacionDesde.Text)
        Else
            aportante.FechaIngreso = Nothing
        End If

        If Not txtFeCreacionHasta.Text.Equals("") Then
            fechaHasta = Date.Parse(txtFeCreacionHasta.Text)
        Else
            fechaHasta = Nothing
        End If
        Dim mensaje As String = Negocio.ExportarAExcel(aportante, fechaHasta)

        If Negocio.rutaArchivosExcel IsNot Nothing Then
            linkArchivo.NavigateUrl = Negocio.rutaArchivosExcel
            linkArchivo.Text = "Bajar Archivo"
        Else
            linkArchivo.Visible = False
        End If

        ShowMesagges(CONST_TITULO_APORTANTE, mensaje, Constantes.CONST_RUTA_IMG + Constantes.CONST_IMG_LOGO, Constantes.CONST_RUTA_IMG + Constantes.CONST_IMG_INFO, False)

    End Sub

    Private Function ExtraerCaracterEntreDosPuntos(cadena As String, caracterInicio As String, caracterFinal As String) As String
        Dim p As Integer 'punto
        Dim p2 As Integer 'punto
        p = cadena.LastIndexOf(caracterInicio) + 1 'obtengo el punto
        p2 = cadena.LastIndexOf(caracterFinal) 'obtengo el punto
        Return cadena.Substring(p, p2 - p) 'Si quiero sacar el 1er caracter
    End Function

    Protected Sub btnVolver_Click(sender As Object, e As EventArgs) Handles btnVolver.Click
        Response.Redirect("~/blank.aspx")
    End Sub

    Private Sub btnModalCancelar_Click(sender As Object, e As EventArgs) Handles btnModalCancelar.Click
        txtAccionHidden.Value = ""
    End Sub

    Private Sub ShowAlert(mesagge As String)
        Dim myScript As String = "alert('" + mesagge + "');"
        'Dim myScript As String = "jAlert('" + mesagge + "');"
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", myScript, True)
    End Sub

    Protected Sub BtnLimpiarFechaDesde_Click(sender As Object, e As EventArgs) Handles BtnLimpiarFechaDesde.Click
        txtFeCreacionDesde.Text = Nothing
    End Sub
    Protected Sub BtnLimpiarFechaHasta_Click(sender As Object, e As EventArgs) Handles BtnLimpiarFechaHasta.Click
        txtFeCreacionHasta.Text = Nothing
    End Sub
End Class