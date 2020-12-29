Imports DTO
Imports Negocio
Imports DBSUtils

Partial Class TipoCambio_Maestro
    Inherits System.Web.UI.Page

    Private ReadOnly Negocio As TipoCambioNegocio = New TipoCambioNegocio()

    Public Const CONST_TITULO_TIPO_CAMBIO As String = "Tipo de cambio"
    Public Const CONST_TITULO_MODAL_MODIFICAR As String = "Modificar tipo de cambio"
    Public Const CONST_TITULO_MODAL_ElIMINAR As String = "Eliminar tipo de cambio"
    Public Const CONST_TITULO_MODAL_CREAR As String = "Nuevo tipo de cambio"

    Public Const CONST_MODIFICAR_ERROR As String = "Error al modificar los datos del tipo de cambio"
    Public Const CONST_MODIFICAR_EXITO As String = "tipo de cambio modificado con éxito"
    Public Const CONST_ELIMINAR_ERROR As String = "Error al eliminar el tipo de cambio"
    Public Const CONST_ELIMINAR_EXITO As String = "tipo de cambio eliminado con éxito"
    Public Const CONST_ELIMINAR_EXISTE_EN_OTRA_TABLA As String = "Tipo de cambio se encuentra relacionado en otra Tabla"
    Public Const CONST_INSERTAR_ERROR As String = "Error al ingresar el Tipo de cambio"
    Public Const CONST_INSERTAR_EXITO As String = "Tipo de cambio ingresado con éxito"
    Public Const CONST_EXISTE_MONEDA As String = "Moneda ya existe"
    Public Const CONST_ELIMINAR_ESTADO_CERO As String = "No se puede eliminar un registro ya deshabilitado"

    Public Const CONST_COL_CODIGO As Integer = 2
    Public Const CONST_COL_FECHA As Integer = 1
    Public Const CONST_COL_VALOR As Integer = 3
    Public Const CONST_COL_ESTADO As Integer = 4
    Public Const CONST_COL_FECHAINGRESO As Integer = 5


    Private Sub Presentacion_Mantenedores_frmMantenedorTipoCambio_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            DataInitial()
        End If

        ValidaPermisosPerfil()
    End Sub

    Protected Sub BtnLimpiarFechaDesde_Click(sender As Object, e As EventArgs)
        txtFeCreacionDesde.Text = ""
    End Sub


    Protected Sub BtnBuscar_Click(sender As Object, e As EventArgs)

        FindTipoCambio()

        If GrvTabla.Rows.Count = 0 Then
            BtnExportar.Enabled = False
            ShowAlert("No se obtuvieron resultados de la búsqueda")
        Else
            BtnExportar.Enabled = True
        End If
        'BtnExportar.Enabled = (GrvTabla.Rows.Count <> 0)
        txtHiddenAccion.Value = ""

    End Sub
    Protected Sub btnLimpiarFrm_Click(sender As Object, e As EventArgs)
        FormateoLimpiarForm()
    End Sub

    Protected Sub BtnModificar_Click(sender As Object, e As EventArgs) Handles BtnModificar.Click
        Dim TipoCambioSelect As TipoCambioDTO = GetTipoCambioSelect()
        Dim TipoCambioActualizado As TipoCambioDTO = Negocio.GetTipoCambio(TipoCambioSelect)

        FormateoFormDatos(TipoCambioActualizado)
            FormateoEstiloFormModificar()
            txtHiddenAccion.Value = "MODIFICAR"
      

    End Sub


    Protected Sub btnCrear_Click(sender As Object, e As EventArgs) Handles btnCrear.Click
        FormateoLimpiarDatosModal()
        FormateoEstiloFormCrear()

        txtHiddenAccion.Value = "CREAR"
    End Sub

    Protected Sub BtnEliminar_Click(sender As Object, e As EventArgs) Handles BtnEliminar.Click
        Dim TipoCambioSelect As TipoCambioDTO = GetTipoCambioSelect()
        Dim TipoCambioActualizado As TipoCambioDTO = Negocio.GetTipoCambio(TipoCambioSelect)

        FormateoFormDatos(TipoCambioActualizado)
            FormateoEstiloFormEliminar()
        txtHiddenAccion.Value = "ELIMINAR"
    End Sub

    Private Sub FindTipoCambio()
        Dim TipoCambio As DTO.TipoCambioDTO = New DTO.TipoCambioDTO()
        Dim negocio As TipoCambioNegocio = New TipoCambioNegocio

        txtFeCreacionDesde.Text = Request.Form(txtFeCreacionDesde.UniqueID)
        txtFeCreacionHasta.Text = Request.Form(txtFeCreacionHasta.UniqueID)

        If (txtFeCreacionDesde.Text.Equals("") And txtFeCreacionHasta.Text.Equals("") And (ddlMoneda.SelectedIndex = 0 Or ddlMoneda.SelectedIndex = 1)) Then
            CargarTodosTipoCambio()
        ElseIf (txtFeCreacionDesde.Text.Equals("") And txtFeCreacionHasta.Text.Equals("") And Not (ddlMoneda.SelectedIndex = 0 Or ddlMoneda.SelectedIndex = 1)) Then
            TipoCambio.Codigo = ddlMoneda.SelectedValue
            GrvTabla.DataSource = negocio.ConsultarPorCodigo(TipoCambio)
            GrvTabla.DataBind()
        Else
            Dim FechaHasta As Nullable(Of Date)
            TipoCambio.Codigo = ddlMoneda.SelectedValue

            If Not txtFeCreacionDesde.Text.Equals("") Then
                TipoCambio.FechaIngreso = Date.Parse(txtFeCreacionDesde.Text)
            Else
                TipoCambio.FechaIngreso = Nothing
            End If

            If Not txtFeCreacionHasta.Text.Equals("") Then
                FechaHasta = Date.Parse(txtFeCreacionHasta.Text)
            Else
                FechaHasta = Date.Parse("31/12/9999")
            End If

            If ((Not txtFeCreacionDesde.Text.Equals("") Or Not txtFeCreacionHasta.Text.Equals("")) And Not ddlMoneda.Text.Equals("")) Then
                GrvTabla.DataSource = negocio.GetListaTCConFiltroCodigo(TipoCambio, FechaHasta)
            Else
                GrvTabla.DataSource = negocio.GetListaTCConFiltro(TipoCambio, FechaHasta)
            End If
            GrvTabla.DataBind()
        End If

    End Sub

    Protected Sub BtnExportar_Click(sender As Object, e As EventArgs) Handles BtnExportar.Click
        Dim TipoCambio As TipoCambioDTO = New TipoCambioDTO()
        Dim FechaHasta As Nullable(Of Date)
        TipoCambio.Codigo = ddlMoneda.SelectedValue

        txtFeCreacionDesde.Text = Request.Form(txtFeCreacionDesde.UniqueID)
        txtFeCreacionHasta.Text = Request.Form(txtFeCreacionHasta.UniqueID)

        If Not txtFeCreacionDesde.Text.Equals("") Then
            TipoCambio.FechaIngreso = Date.Parse(txtFeCreacionDesde.Text)
        Else
            TipoCambio.FechaIngreso = Nothing
        End If

        If Not txtFeCreacionHasta.Text.Equals("") Then
            FechaHasta = Date.Parse(txtFeCreacionHasta.Text)
        Else
            FechaHasta = Date.Parse("31/12/9999")
        End If

        Dim mensaje As String = Negocio.ExportarAExcel(TipoCambio, TipoCambio.Codigo.ToString, FechaHasta)

        If Negocio.rutaArchivosExcel IsNot Nothing Then
            linkArchivo.NavigateUrl = Negocio.rutaArchivosExcel
            linkArchivo.Text = "Bajar Archivo"
        Else
            linkArchivo.Visible = False
        End If

        txtHiddenAccion.Value = "MOSTRAR_DIALOGO"

        ShowMesagges(CONST_TITULO_TIPO_CAMBIO, mensaje, Constantes.CONST_RUTA_IMG + Constantes.CONST_IMG_LOGO, Constantes.CONST_RUTA_IMG + Constantes.CONST_IMG_INFO, False)


    End Sub

    Private Sub DataInitial()
        txtFeCreacionDesde.Text = ""
        txtFeCreacionHasta.Text = ""

        CargaFiltroCodigo()
        GrvTabla.DataSource = New List(Of TipoCambioDTO)
        GrvTabla.DataBind()
        BtnExportar.Enabled = False

        ' ValidaPermisosPerfil()

        txtHiddenAccion.Value = ""
        txtMoneda.Items.Clear()
        ddlMoneda.Items.Clear()

        txtMoneda.Items.Insert(0, "")
        txtMoneda.Items.Insert(1, "CLP")
        txtMoneda.Items.Insert(2, "CLF")
        txtMoneda.Items.Insert(3, "EUR")
        txtMoneda.Items.Insert(4, "USD")
        txtMoneda.Items.Insert(0, New ListItem("", ""))

        ddlMoneda.Items.Insert(0, StrDup(2, " "))
        ddlMoneda.Items.Insert(1, "CLP")
        ddlMoneda.Items.Insert(2, "CLF")
        ddlMoneda.Items.Insert(3, "EUR")
        ddlMoneda.Items.Insert(4, "USD")
        ddlMoneda.Items.Insert(0, New ListItem("", ""))

    End Sub



    Private Sub CargarTodosTipoCambio()
        Dim TipoCambio As TipoCambioDTO = New TipoCambioDTO()
        Dim negocio As TipoCambioNegocio = New TipoCambioNegocio

        GrvTabla.DataSource = negocio.GetListaTipoCambio(TipoCambio)
        GrvTabla.DataBind()
    End Sub


    Private Sub CargaFiltroCodigo()
        Dim tipo As TipoCambioDTO = New TipoCambioDTO
        Dim lista As List(Of TipoCambioDTO) = Negocio.ConsultarPorCodigo(tipo)
        Dim vacia As New TipoCambioDTO
        If lista.Count = 0 Then
            lista.Add(vacia)
        Else

            ddlMoneda.DataSource = lista
            ddlMoneda.DataMember = "Codigo"
            ddlMoneda.DataValueField = "Codigo"
            ddlMoneda.DataBind()
            ddlMoneda.Items.Insert(0, New ListItem("", ""))
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

    Private Function GetTipoCambioPorCodigo() As List(Of TipoCambioDTO)
        Dim lista As New List(Of TipoCambioDTO)
        Dim TipoCambio As New TipoCambioDTO()
        TipoCambio.Codigo = ""
        lista = Negocio.ConsultarPorCodigo(TipoCambio)

        Return lista
    End Function

    Private Function GetTipoCambioSelect() As TipoCambioDTO
        Dim TipoCambio As New TipoCambioDTO
        For Each row As GridViewRow In GrvTabla.Rows
            Dim chk As RadioButton = row.Cells(0).Controls(1)
            If chk IsNot Nothing And chk.Checked Then
                TipoCambio.Fecha = row.Cells(CONST_COL_FECHA).Text().Trim()
                TipoCambio.Codigo = row.Cells(CONST_COL_CODIGO).Text.Trim()
                TipoCambio.Valor = row.Cells(CONST_COL_VALOR).Text.Trim()
            End If
        Next

        Return TipoCambio
    End Function

    Private Function GetTipoCambioModal() As TipoCambioDTO
        Dim TipoCambio As TipoCambioDTO = New TipoCambioDTO()
        txtFechaTC.Text = Request.Form(txtFechaTC.UniqueID)

        TipoCambio.Codigo = txtMoneda.Text
        TipoCambio.Fecha = IIf(txtFechaTC.Text = "", Nothing, txtFechaTC.Text())
        'Reemplaza el punto por comas en caso de que lo tenga para que los datos se ingresen correctamente
        TipoCambio.Valor = IIf(txtValor.Text = "", Nothing, IIf(txtValor.Text.Contains("."), Replace(txtValor.Text, ".", ""), txtValor.Text))
        TipoCambio.UsuarioIngreso = Session("NombreUsuario")
        TipoCambio.Estado = txtHidenEstado.Value

        Return TipoCambio
    End Function

    Private Sub FormateoFormDatos(TipoCambio As TipoCambioDTO)
        Try
            txtMoneda.Text = TipoCambio.Codigo
        Catch ex As Exception
            txtMoneda.Text = ""
        End Try

        txtFechaTC.Text = TipoCambio.Fecha
        txtValor.Text = String.Format("{0:N12}", TipoCambio.Valor)
        txtHidenEstado.Value = TipoCambio.Estado
        txtFechaIngreso.Text = TipoCambio.FechaIngreso.ToString("dd/MM/yyyy")
        txtFechaModificacion.Text = TipoCambio.FechaModificacion.ToString("dd/MM/yyyy")
        txtUsuarioIngreso.Text = TipoCambio.UsuarioIngreso
        txtUsuarioModificacion.Text = TipoCambio.UsuarioModificacion
        'Calendar3.SelectedDates.Clear()
    End Sub
    Private Sub FormateoLimpiarForm()
        ddlMoneda.SelectedIndex = 0
        txtFeCreacionDesde.Text = ""
        txtFeCreacionHasta.Text = ""
        txtHiddenAccion.Value = ""
        'Calendar1.Visible = False
        'Calendar2.Visible = False
        BtnExportar.Enabled = False
        GrvTabla.DataSource = New List(Of TipoCambioDTO)
        GrvTabla.DataBind()
    End Sub
    Private Sub FormateoEstiloFormModificar()
        btnModalEliminar.Visible = False
        btnModalGuardar.Visible = False
        btnModalGuardar.Enabled = False
        btnModalEliminar.Enabled = False
        LinkButton3.Enabled = False
        LinkButton3.Visible = False
        txtMoneda.Enabled = False
        LinkButton5.Enabled = False
        'Calendar3.Visible = False
        LinkButton5.Visible = False

        btnModalModificar.Visible = True
        btnModalModificar.Enabled = True
        txtValor.Enabled = True

        txtHiddenAccion.Value = "MODIFICAR"
        lblModalFondoTitle.Text = CONST_TITULO_MODAL_MODIFICAR
    End Sub

    Private Sub FormateoEstiloFormCrear()
        btnModalEliminar.Visible = False
        btnModalModificar.Visible = False
        btnModalGuardar.Visible = True
        'Calendar3.Visible = False
        btnModalGuardar.Enabled = True
        btnModalModificar.Enabled = False
        LinkButton3.Enabled = True
        LinkButton3.Visible = True
        txtMoneda.Enabled = True
        txtValor.Enabled = True
        txtFechaTC.ReadOnly = True
        LinkButton5.Enabled = True
        LinkButton5.Visible = True

        txtHiddenAccion.Value = "CREAR"
        lblModalFondoTitle.Text = CONST_TITULO_MODAL_CREAR
    End Sub

    Private Sub FormateoEstiloFormEliminar()
        btnModalModificar.Enabled = False
        btnModalGuardar.Enabled = False
        btnModalGuardar.Visible = False
        btnModalEliminar.Enabled = True
        'Calendar3.Visible = False
        btnModalModificar.Visible = False
        btnModalEliminar.Visible = True
        LinkButton3.Visible = False
        LinkButton3.Enabled = False

        LinkButton5.Enabled = False
        LinkButton5.Visible = False

        txtFechaTC.ReadOnly = True
        txtMoneda.Enabled = False
        txtValor.Enabled = False

        lblModalFondoTitle.Text = CONST_TITULO_MODAL_ElIMINAR
    End Sub

    Private Sub FormateoLimpiarDatosModal()

        txtValor.Text = ""
        txtFechaTC.Text = ""
        txtMoneda.SelectedIndex = 0
        txtHidenEstado.Value = "1"
        'Calendar3.SelectedDates.Clear()
        txtUsuarioIngreso.Text = ""
        txtUsuarioModificacion.Text = ""
        txtFechaIngreso.Text = ""
        txtFechaModificacion.Text = ""
    End Sub

    Protected Sub valorfocus() Handles txtValor.DataBinding
        If (txtHiddenAccion.Value = "MODIFICAR") Then
            txtValor.Text = "1231"
        End If

    End Sub

    Private Sub ShowMesagges(title As String, mesagge As String, urlIconTitle As String, urlIconMesagge As String, Optional borraLink As Boolean = True)
        lblModalTitle.Text = title
        lblModalBody.Text = mesagge
        img_modal.ImageUrl = urlIconTitle
        img_body_modal.ImageUrl = urlIconMesagge

        linkArchivo.Visible = (Not borraLink)
    End Sub

    Private Sub btnModalGuardar_Click(sender As Object, e As EventArgs) Handles btnModalGuardar.Click
        Dim TipoCambio As TipoCambioDTO = GetTipoCambioModal()
        Dim solicitud As Integer = Negocio.InsertTipoCambio(TipoCambio)
        If solicitud = Constantes.CONST_OPERACION_EXITOSA Then
            'Ingresado con éxito
            ShowAlert(CONST_INSERTAR_EXITO)
            DataInitial()

        ElseIf solicitud = Constantes.CONST_ERROR_NO_SE_PUEDE_BORRAR Then
            'Código ya existe
            ShowAlert(CONST_EXISTE_MONEDA)
            'DataInitial()
            Exit Sub
        Else
            'Error en la BBDD
            ShowAlert(CONST_INSERTAR_ERROR)
            DataInitial()
            Exit Sub
        End If
    End Sub
    Private Sub btnModalCancelar_Click(sender As Object, e As EventArgs) Handles btnModalCancelar.Click
        FormateoLimpiarDatosModal()
        txtHiddenAccion.Value = ""
    End Sub

    Private Sub btnModalModificar_Click(sender As Object, e As EventArgs) Handles btnModalModificar.Click
        'MODIFICAR

        Dim negocioMod As TipoCambioNegocio = New TipoCambioNegocio
        Dim TipoCambio As TipoCambioDTO = GetTipoCambioModal()
        Dim solicitudMod As Integer = negocioMod.UpdateTipoCambio(TipoCambio)

        If solicitudMod = Constantes.CONST_OPERACION_EXITOSA Then
            ShowAlert(CONST_MODIFICAR_EXITO)
            DataInitial()
        Else
            ShowAlert(CONST_MODIFICAR_ERROR)
            DataInitial()
        End If


    End Sub

    Private Sub btnModalEliminar_Click(sender As Object, e As EventArgs) Handles btnModalEliminar.Click
        Dim TipoCambio As TipoCambioDTO = GetTipoCambioModal()
        Dim Negocio As TipoCambioNegocio = New TipoCambioNegocio
        If Not TipoCambio.Estado = 0 Then
            Dim solicitud As Integer = Negocio.DeleteTipoCambio(TipoCambio)
            If solicitud = Constantes.CONST_OPERACION_EXITOSA Then
                ShowAlert(CONST_ELIMINAR_EXITO)
                CargaFiltroCodigo()
                FindTipoCambio()
                DataInitial()
            ElseIf solicitud = Constantes.CONST_ERROR_NO_SE_PUEDE_BORRAR Then
                ShowAlert(CONST_ELIMINAR_ERROR)
                Exit Sub
            Else
                ShowAlert(CONST_ELIMINAR_ERROR)
                Exit Sub
            End If
        Else
            ShowAlert(CONST_ELIMINAR_ESTADO_CERO)
            Exit Sub

        End If


    End Sub
    Private Sub btnXCerrar_Load(sender As Object, e As EventArgs) Handles btnCerraModal.Click

    End Sub


    Private Sub ShowAlert(mesagge As String)
        Dim myScript As String = "alert('" + mesagge + "');"
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", myScript, True)
    End Sub

End Class
