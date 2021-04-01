Imports DTO
Imports Negocio
Imports System.Data
Imports DBSUtils

Partial Class Presentacion_Mantenedores_frmMantenedorFondoSerie
    Inherits System.Web.UI.Page

    Private ReadOnly negocioFondoSerie As FondoSeriesNegocio = New FondoSeriesNegocio
    Private ReadOnly NegocioFondo As FondosNegocio = New FondosNegocio

    Public Const CONST_TITULO_FONDO As String = "Serie"
    Public Const CONST_TITULO_MODAL_MODIFICAR As String = "Modificar Serie"
    Public Const CONST_MODIFICAR_ERROR As String = "Error al modificar los datos de la Serie"
    Public Const CONST_MODIFICAR_EXITO As String = "Serie modificado con Éxito"
    Public Const CONST_ELIMINAR_ERROR As String = "Error al eliminar la Serie seleccionada"
    Public Const CONST_ELIMINAR_EXITO As String = "Serie eliminado con Éxito"
    Public Const CONST_ELIMINAR_ESTADO_CERO As String = "No se puede eliminar un registro ya deshabilitado"
    Public Const CONST_ELIMINAR_EXISTE_EN_OTRA_TABLA As String = "Serie se encuentra relacionado en otra Tabla"
    Public Const CONST_TITULO_MODAL_ELIMINAR As String = "Eliminar Serie"
    Public Const CONST_TITULO_MODAL_CREAR As String = "Nueva Serie"
    Public Const CONST_COMBINACION_EXISTE As String = "La combinación de Rut y Nemotécnico ya existe"
    Public Const CONST_NEMOTECNICO_EXISTE As String = "El Nemotécnico ya existe"
    Public Const CONST_ERROR_AL_GUARDAR As String = "Error al guardar la Serie"
    Public Const CONST_EXITO_AL_GUARDAR As String = "Serie guardada con Éxito"
    Public Const CONST_SIN_RESULTADOS As String = "No se obtuvieron resultados de la búsqueda"
    Public Const CONST_MAXIMO As String = "EL Monto Máximo debe ser mayor o igual que el Monto Mínimo"

    Public Const CONST_COL_FONDO As Integer = 1
    Public Const CONST_COL_NEMOTECNICO As Integer = 2
    Public Const CONST_COL_NOMBRE_SERIE As Integer = 3
    Public Const CONST_COL_REMUNERACION As Integer = 4
    Public Const CONST_COL_NACIONALIDAD As Integer = 5
    Public Const CONST_COL_CALIFICADO As Integer = 6
    Public Const CONST_COL_MONEDA As Integer = 7
    Public Const CONST_COL_LIMITE_MONEDA As Integer = 8
    Public Const CONST_COL_MINIMO As Integer = 9
    Public Const CONST_COL_MAXIMO As Integer = 10
    Public Const CONST_COL_EXCLUSIVOMAM As Integer = 11
    Public Const CONST_COL_COMPATIBLE As Integer = 12
    Public Const CONST_COL_CANJE As Integer = 13
    Public Const CONST_COL_NIVEL As Integer = 14
    Public Const CONST_COL_FECHA_INGRESO As Integer = 15
    Public Const CONST_COL_USUARIO_INGRESO As Integer = 16
    Public Const CONST_COL_FECHA_MODIFICACION As Integer = 17
    Public Const CONST_COL_USUARIO_MODIFICACION As Integer = 18
    Public Const CONST_COL_HORARIO As Integer = 19
    Public Const CONST_COL_FONDO_RESCATABLE As Integer = 20
    Public Const CONST_COL_FECHA_NAV As Integer = 21
    Public Const CONST_COL_FECHA_RESCATE As Integer = 22
    Public Const CONST_COL_FECHA_TC_OBSERVADO As Integer = 23
    Public Const CONST_COL_PATRIMONIO As Integer = 24
    Public Const CONST_COL_FIJACION_NAV As Integer = 25
    Public Const CONST_COL_FECHA_NAV_SUSCRIPCION As Integer = 26
    Public Const CONST_COL_FECHA_SUSCRIPCION As Integer = 27
    Public Const CONST_COL_FECHATC_SUSCRIPCION As Integer = 28
    Public Const CONST_COL_FIJACION_SUSCRIPCION As Integer = 29
    Public Const CONST_COL_FECHA_NAV_CANJE As Integer = 30
    Public Const CONST_COL_FECHATC_CANJE As Integer = 31
    Public Const CONST_COL_FIJACION_CANJE As Integer = 32

    Private Sub Presentacion_Mantenedores_frmMantenedorFondoSerie_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            DataInitial()
        End If
    End Sub

    Private Sub DataInitial()
        cargaFiltroRut()
        cargaFiltroNemotecnico()
        cargaFiltroAgrupacion()
        cargaFiltroNombreRut()
        cargaFiltroRutModal()
        cargaFiltroNombreModal()
        GrvTabla.DataSource = New List(Of FondoSerieDTO)
        GrvTabla.DataBind()
        BtnExportar.Enabled = (GrvTabla.Rows.Count <> 0)
        txtAccionHidden.Value = ""
        ValidaPermisosPerfil()

        Utiles.cargarMonedas(ddlMonedaSerie)
        Utiles.CargarMonedas(ddlMonedaDeLimite)

    End Sub

    Protected Sub btnLimpiarFrm_Click(sender As Object, e As EventArgs)
        DataInitial()
    End Sub

    Private Sub LlenarFondoSerie()
        Dim NegocioFondo As FondosNegocio = New FondosNegocio
        Dim fondo As FondoDTO = New FondoDTO()
        Dim listafondo As List(Of FondoDTO) = NegocioFondo.ConsultarTodos(fondo)

        ddlRutdeFondo.DataSource = listafondo
        ddlRutdeFondo.DataMember = "Rut"
        ddlRutdeFondo.DataValueField = "Rut"
        ddlRutdeFondo.DataBind()

        ddlNombreRutModal.DataSource = listafondo
        ddlNombreRutModal.DataMember = "NombreCorto"
        ddlNombreRutModal.DataValueField = "NombreCorto"
        ddlNombreRutModal.DataBind()
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

    Private Sub cargaFiltroRut()
        Dim fondo As New FondoSerieDTO
        Dim lista As List(Of FondoSerieDTO) = negocioFondoSerie.llenarFiltroRutNombreSerieConsulta()
        Dim fondoVacio As FondoSerieDTO = New FondoSerieDTO()

        fondoVacio.Rut = " "
        fondoVacio.Nombrecorto = " "
        If lista.Count = 0 Then
            lista.Add(fondoVacio)
        Else
            lista.Insert(0, fondoVacio)
        End If

        ddlListaRutFondo.DataSource = lista
        ddlListaRutFondo.DataTextField = "RutNombreFondo"
        ddlListaRutFondo.DataValueField = "Rut"
        ddlListaRutFondo.DataBind()

    End Sub

    Private Sub cargaFiltroRutModal()
        Dim fondo As New FondoDTO
        Dim listaFondos As List(Of FondoDTO) = NegocioFondo.ConsultarPorRut(fondo)
        If listaFondos.Count = 0 Then
            ddlRutdeFondo.Items.Insert(0, New ListItem("Seleccione", String.Empty))
        Else
            ddlRutdeFondo.DataSource = listaFondos
            ddlRutdeFondo.DataMember = "RutRazonSocial"
            ddlRutdeFondo.DataValueField = "RutRazonSocial"
            ddlRutdeFondo.DataTextField = "RutRazonSocial"
            ddlRutdeFondo.DataBind()
            ddlRutdeFondo.Items.Insert(0, New ListItem("Seleccione", String.Empty))
            ddlNombreRutModal.Enabled = False
        End If
    End Sub

    Private Sub cargaFiltroNombreModal()
        Dim fondo As New FondoDTO
        Dim listaFondos As List(Of FondoDTO) = NegocioFondo.ConsultarTodos(fondo)
        If listaFondos.Count = 0 Then
            ddlNombreRutModal.Items.Insert(0, New ListItem("Seleccione", String.Empty))
        Else
            ddlNombreRutModal.DataSource = listaFondos
            ddlNombreRutModal.DataMember = "NombreCorto"
            ddlNombreRutModal.DataValueField = "NombreCorto"
            ddlNombreRutModal.DataBind()
            ddlNombreRutModal.Items.Insert(0, New ListItem("Seleccione", String.Empty))
        End If
    End Sub

    Private Sub cargaFiltroNombreRut()
        Dim fondo As New FondoDTO
        Dim fondoSerie As New FondoSerieDTO
        Dim lista As List(Of FondoDTO) = negocioFondoSerie.GrupoSeriesTraerNombre(fondo, fondoSerie)
        If lista.Count = 0 Then
            ddlListaNombreFondo.Items.Insert(0, New ListItem("", ""))
        Else
            ddlListaNombreFondo.DataSource = lista
            ddlListaNombreFondo.DataMember = "NombreCorto"
            ddlListaNombreFondo.DataValueField = "NombreCorto"
            ddlListaNombreFondo.DataBind()
            ddlListaNombreFondo.Items.Insert(0, New ListItem("", ""))
        End If
    End Sub

    Private Sub cargaFiltroNemotecnico()
        Dim fondo As New FondoSerieDTO
        Dim lista As List(Of FondoSerieDTO) = negocioFondoSerie.GetListaFondoSerieporNemotecnico(fondo)
        Dim fondoVacio As FondoSerieDTO = New FondoSerieDTO()
        fondoVacio.Rut = StrDup(50, " ")
        fondoVacio.Nemotecnico = StrDup(50, " ")
        If lista.Count = 0 Then
            lista.Add(fondoVacio)
        Else
            lista.Insert(0, fondoVacio)
        End If

        ddlListaNemotecnico.DataSource = lista
        'ddlListaNemotecnico.DataMember = "Rut"
        ddlListaNemotecnico.DataTextField = "Nemotecnico"
        ddlListaNemotecnico.DataValueField = "RutNemotecnico"
        ddlListaNemotecnico.DataBind()

    End Sub

    Private Sub filtrarPorFondo(fondoSerie As FondoSerieDTO)
        FiltrarNemotecnico(fondoSerie)
        FiltrarGrupoCompatible(fondoSerie)
    End Sub

    Private Sub FiltrarGrupoCompatible(fondoSerie As FondoSerieDTO)
        Dim listaFondoSerie As List(Of FondoSerieDTO) = negocioFondoSerie.filtrarGrupoCompatiblePorFondo(fondoSerie)

        ddlListaGrupoCompatible.DataSource = Nothing
        ddlListaGrupoCompatible.DataBind()
        'ddlListaGrupoCompatible.Items.Clear()
        ddlListaGrupoCompatible.DataSource = listaFondoSerie
        ddlListaGrupoCompatible.DataMember = "Compatible"
        ddlListaGrupoCompatible.DataTextField = "Compatible"
        ddlListaGrupoCompatible.DataValueField = "Compatible"
        ddlListaGrupoCompatible.DataBind()
        ddlListaGrupoCompatible.Items.Insert(0, New ListItem("", ""))

    End Sub

    Private Sub FiltrarNemotecnico(fondoSerie As FondoSerieDTO)
        Dim listaFondoSerie As List(Of FondoSerieDTO) = negocioFondoSerie.filtrarNemotecnicoPorFondo(fondoSerie)
        Dim fondoVacio As FondoSerieDTO = New FondoSerieDTO()
        fondoVacio.Nemotecnico = StrDup(50, " ")

        If listaFondoSerie.Count = 0 Then
            listaFondoSerie.Add(fondoVacio)
        Else
            listaFondoSerie.Insert(0, fondoVacio)
        End If

        ddlListaNemotecnico.DataSource = listaFondoSerie
        ddlListaNemotecnico.DataTextField = "Nemotecnico"
        ddlListaNemotecnico.DataValueField = "RutNemotecnico"
        ddlListaNemotecnico.DataBind()

    End Sub

    Private Sub cargaFiltroAgrupacion()
        Dim fondoSerie As New FondoSerieDTO
        fondoSerie.Compatible = Nothing
        FiltrarGrupoCompatible(fondoSerie)
    End Sub

    Protected Sub btnCrear_Click(sender As Object, e As EventArgs)
        FormateoLimpiarDatosModal()
        FormateoEstiloFormCrear()
        txtAccionHidden.Value = "CREAR"

    End Sub

    Private Sub FormateoLimpiarDatosModal()
        cargaFiltroRutModal()
        cargaFiltroNombreModal()

        txtNemotecnico.Text = ""
        txtNombreSerie.Text = ""
        ddlMonedaSerie.SelectedIndex = 0
        ddlTipoRemuneracion.SelectedIndex = 0
        ddlInversionistaCalificado.SelectedIndex = 0
        ddlExclusivoMAM.SelectedIndex = 0
        ddlMonedaDeLimite.SelectedIndex = 0
        txtMontoMinimo.Text = ""
        txtMontoMaximo.Text = ""
        txtGrupoCompatible.Text = ""
        ddlContratoDistribuicion.SelectedIndex = 0
        ddlDomiciliadoExtranjero.SelectedIndex = 0
        ddlCanjeMandatorio.SelectedIndex = 0
        txtHorarioCorte.Text = ""
        ddlFondoRescatable.SelectedIndex = 0
        ddlFechaNav.SelectedIndex = 0
        ddDiasHabilesFechaNav.Text = ""
        ddlFechaRescate.SelectedIndex = 0
        ddNumeroFechaRescate.Text = ""
        ddlFechaTC.SelectedIndex = 0
        ddlNumeroFechaTC.Text = ""
        txtPorcentajePatrimonio.Text = ""
        ddlFijacionNav.SelectedIndex = 0
        ddlFechaSuscripcion.SelectedIndex = 0
        ddNumeroFechaSuscripcion.Text = ""
        ddlFechaObservadoSuscripciones.SelectedIndex = 0
        ddNumeroTCSuscripciones.Text = ""
        ddlFechaNavSuscripciones.SelectedIndex = 0
        ddNumeroFechaNavSuscripciones.Text = ""
        ddlfijacionSuscripcion.SelectedIndex = 0
        ddlfechaObservadoCanje.SelectedIndex = 0
        ddlNumeroTCCanje.Text = ""
        ddFechaNavCanje.SelectedIndex = 0
        ddNumeroFechaNavCanje.Text = ""
        ddlFijacionNavCanje.SelectedIndex = 0

        chkDiasHabilesCanje.Checked = False
        chkDiasHabilesRescate.Checked = False
        chkDiasHabilesSuscipciones.Checked = False

        ddlFechaPatrimonio.SelectedIndex = 0
        ddlNumeroFechaPatrimonio.Text = ""
        chkDiasHabilesFechaPatrimonio.Checked = False

    End Sub

    Protected Sub BtnBuscar_Click(sender As Object, e As EventArgs) Handles BtnBuscar.Click
        FindFondoSerie()
        BtnModificar.Enabled = False
        If GrvTabla.Rows.Count <> 0 Then

            BtnExportar.Enabled = True
        Else
            BtnExportar.Enabled = False
            ShowAlert(CONST_SIN_RESULTADOS)
        End If
        txtAccionHidden.Value = ""
    End Sub

    Private Sub FindFondoSerie()
        Dim fondoSerie As FondoSerieDTO = New FondoSerieDTO()
        Dim fondo As FondoDTO = New FondoDTO()

        If (ddlListaRutFondo.SelectedIndex > 0) Then
            Dim arrCadena As String() = ddlListaRutFondo.SelectedItem.Text().Split(New Char() {"/"c})

            If ddlListaRutFondo.SelectedValue.Trim() = "" Then
                fondoSerie.Rut = arrCadena(0).Trim()
                fondo.NombreCorto = arrCadena(0).Trim
            Else
                fondoSerie.Rut = arrCadena(0).Trim()
                fondo.NombreCorto = arrCadena(1).Trim
            End If

        Else
            fondoSerie.Rut = Nothing
            fondo.NombreCorto = Nothing
        End If

        If ddlListaNemotecnico.SelectedItem.Text.Trim() = Nothing Then
            fondoSerie.Nemotecnico = Nothing
        Else
            fondoSerie.Nemotecnico = ddlListaNemotecnico.SelectedItem.Text.Trim()
        End If

        If ddlListaGrupoCompatible.SelectedValue.Trim() = Nothing Then
            fondoSerie.Nivel = Nothing
        Else
            fondoSerie.Nivel = ddlListaGrupoCompatible.SelectedValue.Trim()
        End If


        If fondoSerie.Rut = Nothing And fondo.NombreCorto = Nothing And fondoSerie.Nemotecnico = Nothing And fondoSerie.Nivel = 0 Then
            GrvTabla.DataSource = negocioFondoSerie.GetListaFondoSerieTodos(fondoSerie)
        Else
            GrvTabla.DataSource = negocioFondoSerie.GetListaFondoSerieConFiltroConsultar(fondoSerie, fondo)

        End If
        GrvTabla.DataBind()
    End Sub

    Private Sub ShowMessages(tittle As String, message As String, urlconTittle As String, urlconMessage As String, Optional borralink As Boolean = True)
        lblModalTitle.Text = tittle
        lblModalBody.Text = message
        img_modal.ImageUrl = urlconTittle
        img_body_modal.ImageUrl = urlconMessage
        Archivo.Visible = (Not borralink)
    End Sub

    Public Sub verificarNemotecnico()
        Dim fondo As FondoSerieDTO = GetFondoSerie()
        Dim solicitud As Integer = negocioFondoSerie.verificarNemotecnico(fondo)

        If solicitud = 1 Then
            ShowAlertNemotecnico(CONST_COMBINACION_EXISTE)
            txtNemotecnico.Text = ""
        ElseIf solicitud = 2 Then
            ShowAlertNemotecnico(CONST_NEMOTECNICO_EXISTE)
            txtNemotecnico.Text = ""
        End If
    End Sub

    Private Sub btnModalGuardar_Click(sender As Object, e As EventArgs) Handles btnModalGuardar.Click
        Dim serie As FondoSerieDTO = GetFondoSerie()
        Dim solicitud As Integer = negocioFondoSerie.InsertFondoSerie(serie)

        If solicitud = Constantes.CONST_OPERACION_EXITOSA Then
            ShowAlert(CONST_EXITO_AL_GUARDAR)
            DataInitial()
        Else solicitud = Constantes.CONST_ERROR_BBDD
            ShowAlert(CONST_ERROR_AL_GUARDAR)
            Exit Sub
        End If
    End Sub

    Protected Sub RowSelector_CheckedChanged(sender As Object, e As EventArgs)
        For Each row As GridViewRow In GrvTabla.Rows
            Dim chk As RadioButton = row.Cells(0).Controls(1)
            If chk IsNot Nothing And chk.Checked Then
            End If
        Next
    End Sub

    Private Sub ShowAlert(mesagge As String)
        Dim myScript As String = "alert('" + mesagge + "');"
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", myScript, True)
    End Sub

    Private Sub ShowAlertNemotecnico(mesagge As String, Optional mostrarEnPage As Boolean = False)
        Dim myScript As String = "alert('" + mesagge + "');"
        If Not mostrarEnPage Then
            ScriptManager.RegisterStartupScript(UpdatePanelNemotecnico, UpdatePanelNemotecnico.GetType(), "alert", myScript, True)
        Else
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", myScript, True)
        End If
    End Sub

    Protected Sub BtnModificar_Click(sender As Object, e As EventArgs)
        Dim negocio As FondoSeriesNegocio = New FondoSeriesNegocio
        Dim FondoSerieSelect As FondoSerieDTO = GetFondoSerieSelect()
        Dim FondoSerieActualizado As FondoSerieDTO = negocio.GetFondosSeries(FondoSerieSelect)
        FormateoFormDatos(FondoSerieActualizado)
        FormateoEstiloFormModificar()
        txtAccionHidden.Value = "MODIFICAR"
    End Sub

    Private Sub btnModalModificar_Click(sender As Object, e As EventArgs) Handles btnModalModificar.Click
        Dim fondoSerie As FondoSerieDTO = GetFondoSerie()
        Dim solicitud As Integer = negocioFondoSerie.UpdateFondoSerie(fondoSerie)

        If solicitud = Constantes.CONST_OPERACION_EXITOSA Then
            ShowAlert(CONST_MODIFICAR_EXITO)
        Else
            ShowAlert(CONST_MODIFICAR_ERROR)
        End If
        DataInitial()
    End Sub

    Protected Sub BtnEliminar_Click(sender As Object, e As EventArgs) Handles BtnEliminar.Click
        Dim fondoSelect As FondoSerieDTO = GetFondoSerieSelect()
        Dim fondoActualizado As FondoSerieDTO = negocioFondoSerie.GetFondosSeries(fondoSelect)
        FormateoFormDatos(fondoActualizado)
        FormateoEstiloFormEliminar()
        txtAccionHidden.Value = "ELIMINAR"
    End Sub

    Private Sub btnModalEliminar_Click(sender As Object, e As EventArgs) Handles btnModalEliminar.Click
        Dim fondoSerie As FondoSerieDTO = GetFondoSerie()
        Dim solicitud As Integer = negocioFondoSerie.DeleteFondoSerie(fondoSerie)

        If solicitud = Constantes.CONST_OPERACION_EXITOSA Then
            ShowAlert(CONST_ELIMINAR_EXITO)
            DataInitial()
        Else
            ShowAlert(CONST_ELIMINAR_ERROR)
            Exit Sub
        End If

    End Sub

    Private Sub btnModalCancelar_Click(sender As Object, e As EventArgs) Handles btnModalCancelar.Click
        txtAccionHidden.Value = ""
        FormateoLimpiarDatosModal()
    End Sub

    Private Function GetFondoSerieSelect() As FondoSerieDTO
        Dim fondoserie As New FondoSerieDTO

        For Each row As GridViewRow In GrvTabla.Rows
            Dim chk As RadioButton = row.Cells(0).Controls(1)
            If chk IsNot Nothing And chk.Checked Then

                fondoserie.Rut = row.Cells(CONST_COL_FONDO).Text.Trim()
                fondoserie.Nemotecnico = row.Cells(CONST_COL_NEMOTECNICO).Text.Trim()
                Exit For
            End If
        Next

        Return fondoserie
    End Function

    Private Sub FormateoEstiloFormModificar()
        btnModalModificar.Enabled = True
        btnModalModificar.Visible = True
        btnModalGuardar.Enabled = False
        btnModalGuardar.Visible = False
        btnModalEliminar.Enabled = False
        ddlRutdeFondo.Enabled = False
        ddlNombreRutModal.Enabled = False
        txtNemotecnico.Enabled = False
        txtNombreSerie.Enabled = True
        ddlMonedaSerie.Enabled = True
        ddlTipoRemuneracion.Enabled = True
        ddlInversionistaCalificado.Enabled = True
        ddlExclusivoMAM.Enabled = True
        ddlMonedaDeLimite.Enabled = True
        txtMontoMinimo.Enabled = True
        txtMontoMaximo.Enabled = True
        txtGrupoCompatible.Enabled = True
        ddlContratoDistribuicion.Enabled = True
        ddlDomiciliadoExtranjero.Enabled = True
        ddlCanjeMandatorio.Enabled = True
        txtHorarioCorte.Enabled = True
        ddlFondoRescatable.Enabled = True
        ddlFechaNav.Enabled = True
        ddDiasHabilesFechaNav.Enabled = True
        ddlFechaRescate.Enabled = True
        ddNumeroFechaRescate.Enabled = True
        ddlFechaTC.Enabled = True
        ddlNumeroFechaTC.Enabled = True
        txtPorcentajePatrimonio.Enabled = True
        ddlFijacionNav.Enabled = True
        ddlFechaSuscripcion.Enabled = True
        ddNumeroFechaSuscripcion.Enabled = True
        ddlFechaObservadoSuscripciones.Enabled = True
        ddNumeroTCSuscripciones.Enabled = True
        ddlFechaNavSuscripciones.Enabled = True
        ddNumeroFechaNavSuscripciones.Enabled = True
        ddlfijacionSuscripcion.Enabled = True
        ddlfechaObservadoCanje.Enabled = True
        ddlNumeroTCCanje.Enabled = True
        ddFechaNavCanje.Enabled = True
        ddNumeroFechaNavCanje.Enabled = True
        ddlFijacionNavCanje.Enabled = True
        lblTitleModalCrud.Text = CONST_TITULO_MODAL_MODIFICAR

        ddlFechaPatrimonio.Enabled = True

    End Sub

    ' TODO: Averiguar por que se cae cuando la fecha viene nula 
    Private Sub FormateoFormDatos(fondoSerie As FondoSerieDTO)
        Dim estructuraFechas As EstructuraFechasDto

        Dim fechaNavDes As String
        Dim diasDes As String

        estructuraFechas = New EstructuraFechasDto
        estructuraFechas = Utiles.splitCharByComma(fondoSerie.FechaNav)    '.Split(New Char() {","c})

        fechaNavDes = estructuraFechas.DesdeQueFecha
        diasDes = estructuraFechas.DiasASumar

        Dim fechaRes As String
        Dim diasRes As String
        estructuraFechas = New EstructuraFechasDto
        estructuraFechas = Utiles.splitCharByComma(fondoSerie.FechaRescate) ' .Split(New Char() {","c})
        fechaRes = estructuraFechas.DesdeQueFecha
        diasRes = estructuraFechas.DiasASumar

        Dim fechaTC As String
        Dim diasTC As String
        estructuraFechas = New EstructuraFechasDto
        estructuraFechas = Utiles.splitCharByComma(fondoSerie.FechaTCObservado) ' .Split(New Char() {","c})
        fechaTC = estructuraFechas.DesdeQueFecha
        diasTC = estructuraFechas.DiasASumar

        Dim fechaNavSus As String
        Dim diasNavSus As String
        estructuraFechas = New EstructuraFechasDto
        estructuraFechas = Utiles.splitCharByComma(fondoSerie.FechaNavSuscripcion) '.Split(New Char() {","c})
        fechaNavSus = estructuraFechas.DesdeQueFecha
        diasNavSus = estructuraFechas.DiasASumar

        Dim fechaSus As String
        Dim diaSus As String
        estructuraFechas = New EstructuraFechasDto
        estructuraFechas = Utiles.splitCharByComma(fondoSerie.FechaSuscripcion) '.Split(New Char() {","c})
        fechaSus = estructuraFechas.DesdeQueFecha
        diaSus = estructuraFechas.DiasASumar

        Dim fechaTcS As String
        Dim diasTcS As String
        estructuraFechas = New EstructuraFechasDto
        estructuraFechas = Utiles.splitCharByComma(fondoSerie.FechaTCSuscripcion) '.Split(New Char() {","c})
        fechaTcS = estructuraFechas.DesdeQueFecha
        diasTcS = estructuraFechas.DiasASumar

        Dim fechaNavC As String
        Dim diasNavC As String
        estructuraFechas = New EstructuraFechasDto
        estructuraFechas = Utiles.splitCharByComma(fondoSerie.FechaNavCanje) '.Split(New Char() {","c})
        fechaNavC = estructuraFechas.DesdeQueFecha
        diasNavC = estructuraFechas.DiasASumar

        Dim fechaTCanje As String
        Dim diasTCanje As String
        estructuraFechas = New EstructuraFechasDto
        estructuraFechas = Utiles.splitCharByComma(fondoSerie.FechaTCCanje) '.Split(New Char() {","c})
        fechaTCanje = estructuraFechas.DesdeQueFecha
        diasTCanje = estructuraFechas.DiasASumar

        Dim estructuraFechaCanje As EstructuraFechasDto = New EstructuraFechasDto
        estructuraFechaCanje = Utiles.splitCharByComma(fondoSerie.FechaCanjeCanje) '.Split(New Char() {","c})

        Dim estructuraFechaPatrimonio As EstructuraFechasDto = New EstructuraFechasDto
        estructuraFechaPatrimonio = Utiles.splitCharByComma(fondoSerie.FechaPatrimonio) '.Split(New Char() {","c})


        Dim fondo As FondoDTO = New FondoDTO
        Dim NegocioFondo As FondosNegocio = New FondosNegocio

        fondo.Rut = fondoSerie.Rut
        fondo = NegocioFondo.GetFondo(fondo)

        ddlRutdeFondo.SelectedValue = fondoSerie.Rut + "/" + fondo.RazonSocial
        txtNemotecnico.Text = fondoSerie.Nemotecnico
        txtNombreSerie.Text = fondoSerie.Nombrecorto
        ddlMonedaSerie.Text = fondoSerie.Moneda
        ddlTipoRemuneracion.Text = fondoSerie.Remuneracion
        ddlInversionistaCalificado.Text = fondoSerie.Calificado
        ddlExclusivoMAM.SelectedValue = fondoSerie.ExclusivoMAM
        ddlMonedaDeLimite.Text = fondoSerie.LimiteMoneda
        txtMontoMinimo.Text = String.Format("{0:N0}", fondoSerie.LimiteMinimo)
        txtMontoMaximo.Text = String.Format("{0:N0}", fondoSerie.LimiteMaximo)
        txtGrupoCompatible.Text = fondoSerie.Nivel
        ddlContratoDistribuicion.SelectedValue = fondoSerie.Compatible
        ddlDomiciliadoExtranjero.Text = fondoSerie.Nacionalidad
        ddlCanjeMandatorio.SelectedValue = fondoSerie.Canje
        txtHorarioCorte.Text = fondoSerie.HorarioRecaste
        ddlFondoRescatable.SelectedValue = fondoSerie.FondoRescatable
        ddlFechaNav.SelectedValue = fechaNavDes
        ddDiasHabilesFechaNav.Text = diasDes
        ddlFechaRescate.SelectedValue = fechaRes
        ddNumeroFechaRescate.Text = diasRes
        ddlFechaTC.SelectedValue = fechaTC
        ddlNumeroFechaTC.Text = diasTC
        txtPorcentajePatrimonio.Text = fondoSerie.Patrimonio
        ddlFijacionNav.SelectedValue = fondoSerie.FijacionNav
        ddlFechaSuscripcion.SelectedValue = fechaSus
        ddNumeroFechaSuscripcion.Text = diaSus
        ddlFechaObservadoSuscripciones.SelectedValue = fechaTcS
        ddNumeroTCSuscripciones.Text = diasTcS
        ddlFechaNavSuscripciones.SelectedValue = fechaNavSus
        ddNumeroFechaNavSuscripciones.Text = diasNavSus
        ddlfijacionSuscripcion.SelectedValue = fondoSerie.FijacionSuscripcion
        ddlfechaObservadoCanje.SelectedValue = fechaTCanje
        ddlNumeroTCCanje.Text = diasTCanje
        ddFechaNavCanje.SelectedValue = fechaNavC
        ddNumeroFechaNavCanje.Text = diasNavC
        ddlFijacionNavCanje.SelectedValue = fondoSerie.FijacionCanje
        txtEstadoCambio.Value = fondoSerie.Estado

        ddlFechaCanje.SelectedValue = estructuraFechaCanje.DesdeQueFecha
        txtNumeroFechaCanje.Text = estructuraFechaCanje.DiasASumar

        chkDiasHabilesCanje.Checked = fondoSerie.SoloDiasHabilesFechaNavCanje
        chkDiasHabilesFechaCanje.Checked = fondoSerie.SoloDiasHabilesFechaCanje
        chkDiasHabilesRescate.Checked = fondoSerie.SoloDiasHabilesFechaNavRescate
        chkDiasHabilesSuscipciones.Checked = fondoSerie.SoloDiasHabilesFechaNavSuscripciones

        ddlFechaPatrimonio.SelectedValue = estructuraFechaPatrimonio.DesdeQueFecha
        ddlNumeroFechaPatrimonio.Text = estructuraFechaPatrimonio.DiasASumar
        chkDiasHabilesFechaPatrimonio.Checked = fondoSerie.SoloDiasHabilesFechaPatrimonio

    End Sub

    Private Sub FormateoEstiloFormCrear()
        btnModalModificar.Enabled = False
        btnModalModificar.Visible = False
        btnModalGuardar.Enabled = True
        btnModalGuardar.Visible = True
        btnModalEliminar.Enabled = False
        ddlRutdeFondo.Enabled = True
        ddlNombreRutModal.Enabled = True
        txtNemotecnico.Enabled = True
        txtNombreSerie.Enabled = True
        ddlMonedaSerie.Enabled = True
        ddlTipoRemuneracion.Enabled = True
        ddlInversionistaCalificado.Enabled = True
        ddlExclusivoMAM.Enabled = True
        ddlMonedaDeLimite.Enabled = True
        txtMontoMinimo.Enabled = True
        txtMontoMaximo.Enabled = True
        txtGrupoCompatible.Enabled = True
        ddlContratoDistribuicion.Enabled = True
        ddlDomiciliadoExtranjero.Enabled = True
        ddlCanjeMandatorio.Enabled = True
        txtHorarioCorte.Enabled = True
        ddlFondoRescatable.Enabled = True
        ddlFechaNav.Enabled = True
        ddDiasHabilesFechaNav.Enabled = True
        ddlFechaRescate.Enabled = True
        ddNumeroFechaRescate.Enabled = True
        ddlFechaTC.Enabled = True
        ddlNumeroFechaTC.Enabled = True
        txtPorcentajePatrimonio.Enabled = True
        ddlFijacionNav.Enabled = True
        ddlFechaSuscripcion.Enabled = True
        ddNumeroFechaSuscripcion.Enabled = True
        ddlFechaObservadoSuscripciones.Enabled = True
        ddNumeroTCSuscripciones.Enabled = True
        ddlFechaNavSuscripciones.Enabled = True
        ddNumeroFechaNavSuscripciones.Enabled = True
        ddlfijacionSuscripcion.Enabled = True
        ddlfechaObservadoCanje.Enabled = True
        ddlNumeroTCCanje.Enabled = True
        ddFechaNavCanje.Enabled = True
        ddNumeroFechaNavCanje.Enabled = True
        ddlFijacionNavCanje.Enabled = True
        lblTitleModalCrud.Text = CONST_TITULO_MODAL_CREAR

        chkDiasHabilesCanje.Checked = False
        chkDiasHabilesFechaCanje.Checked = False
        chkDiasHabilesRescate.Checked = False
        chkDiasHabilesSuscipciones.Checked = False

        ddlFechaPatrimonio.SelectedValue = True

        chkDiasHabilesFechaPatrimonio.Checked = False


    End Sub

    Private Sub FormateoEstiloFormEliminar()
        btnModalModificar.Enabled = False
        btnModalGuardar.Enabled = False
        btnModalGuardar.Visible = False
        btnModalEliminar.Enabled = True

        ddlRutdeFondo.Enabled = False
        txtNemotecnico.Enabled = False
        txtNombreSerie.Enabled = False
        ddlMonedaSerie.Enabled = False
        ddlTipoRemuneracion.Enabled = False
        ddlInversionistaCalificado.Enabled = False
        ddlExclusivoMAM.Enabled = False
        ddlMonedaDeLimite.Enabled = False
        txtMontoMinimo.Enabled = False
        txtMontoMaximo.Enabled = False
        txtGrupoCompatible.Enabled = False
        ddlContratoDistribuicion.Enabled = False
        ddlDomiciliadoExtranjero.Enabled = False
        ddlCanjeMandatorio.Enabled = False
        txtHorarioCorte.Enabled = False
        ddlFondoRescatable.Enabled = False
        ddlFechaNav.Enabled = False
        ddDiasHabilesFechaNav.Enabled = False
        ddlFechaRescate.Enabled = False
        ddNumeroFechaRescate.Enabled = False
        ddlFechaTC.Enabled = False
        ddlNumeroFechaTC.Enabled = False
        txtPorcentajePatrimonio.Enabled = False
        ddlFijacionNav.Enabled = False
        ddlFechaSuscripcion.Enabled = False
        ddNumeroFechaSuscripcion.Enabled = False
        ddlFechaObservadoSuscripciones.Enabled = False
        ddNumeroTCSuscripciones.Enabled = False
        ddlFechaNavSuscripciones.Enabled = False
        ddNumeroFechaNavSuscripciones.Enabled = False
        ddlfijacionSuscripcion.Enabled = False
        ddlfechaObservadoCanje.Enabled = False
        ddlNumeroTCCanje.Enabled = False
        ddFechaNavCanje.Enabled = False
        ddNumeroFechaNavCanje.Enabled = False
        ddlFijacionNavCanje.Enabled = False

        chkDiasHabilesCanje.Enabled = False
        chkDiasHabilesFechaCanje.Enabled = False
        chkDiasHabilesRescate.Enabled = False
        chkDiasHabilesSuscipciones.Enabled = False

        ddlFechaPatrimonio.SelectedValue = True

        lblTitleModalCrud.Text = CONST_TITULO_MODAL_ELIMINAR
    End Sub

    Private Function GetFondoSerie() As FondoSerieDTO
        Dim fondoSerie As New FondoSerieDTO

        If (ddlRutdeFondo.SelectedIndex > 0) Then
            Dim arrCadena As String() = ddlRutdeFondo.SelectedItem.Text().Split(New Char() {"/"c})
            Dim rutSer As String
            Dim nomFon As String
            rutSer = arrCadena(0).Trim()
            nomFon = arrCadena(1).Trim()
            fondoSerie.Rut = rutSer
        End If

        fondoSerie.Nemotecnico = txtNemotecnico.Text.Trim()
        fondoSerie.Nombrecorto = txtNombreSerie.Text
        fondoSerie.Moneda = ddlMonedaSerie.SelectedValue
        fondoSerie.Remuneracion = ddlTipoRemuneracion.SelectedValue
        fondoSerie.Calificado = ddlInversionistaCalificado.SelectedValue
        fondoSerie.ExclusivoMAM = IIf(ddlExclusivoMAM.SelectedValue = "", Nothing, ddlExclusivoMAM.SelectedValue)
        fondoSerie.LimiteMoneda = ddlMonedaDeLimite.SelectedValue
        fondoSerie.LimiteMinimo = Replace(IIf(txtMontoMinimo.Text = "", Nothing, txtMontoMinimo.Text), ".", "")
        fondoSerie.LimiteMaximo = Replace(IIf(txtMontoMaximo.Text = "", Nothing, txtMontoMaximo.Text), ".", "")
        fondoSerie.Compatible = IIf(ddlContratoDistribuicion.SelectedValue = "", Nothing, ddlContratoDistribuicion.SelectedValue)
        fondoSerie.Nivel = IIf(txtGrupoCompatible.Text = "", Nothing, txtGrupoCompatible.Text)
        fondoSerie.Nacionalidad = ddlDomiciliadoExtranjero.SelectedValue
        fondoSerie.Canje = IIf(ddlCanjeMandatorio.SelectedValue = "", Nothing, ddlCanjeMandatorio.SelectedValue)
        fondoSerie.UsuarioIngreso = Session("NombreUsuario")
        fondoSerie.UsuarioModificacion = Session("NombreUsuario")
        fondoSerie.Estado = IIf(txtEstadoCambio.Value = "", 1, txtEstadoCambio.Value)

        fondoSerie.HorarioRecaste = IIf(txtHorarioCorte.Text = "", "", txtHorarioCorte.Text)

        fondoSerie.FondoRescatable = ddlFondoRescatable.SelectedValue
        fondoSerie.FechaNav = IIf(ddlFechaNav.SelectedIndex > 0 And ddDiasHabilesFechaNav.Text >= "", ddlFechaNav.SelectedValue.ToString() + "," + ddDiasHabilesFechaNav.Text.ToString(), " , ")
        fondoSerie.FechaRescate = IIf(ddlFechaRescate.SelectedIndex > 0 And ddNumeroFechaRescate.Text >= "", ddlFechaRescate.SelectedValue.ToString() + "," + ddNumeroFechaRescate.Text.ToString(), " , ")
        fondoSerie.FechaTCObservado = IIf(ddlFechaTC.SelectedIndex > 0 And ddlNumeroFechaTC.Text >= "", ddlFechaTC.SelectedValue.ToString() + "," + ddlNumeroFechaTC.Text.ToString(), " , ")

        fondoSerie.Patrimonio = ""

        If txtPorcentajePatrimonio.Text <> "" Then
            fondoSerie.Patrimonio = txtPorcentajePatrimonio.Text
        End If

        fondoSerie.FijacionNav = IIf(ddlFijacionNav.SelectedValue.ToString() = "", Nothing, ddlFijacionNav.SelectedValue.ToString())
        fondoSerie.FechaSuscripcion = IIf(ddlFechaSuscripcion.SelectedIndex > 0 And ddNumeroFechaSuscripcion.Text >= "", ddlFechaSuscripcion.SelectedValue.ToString() + "," + ddNumeroFechaSuscripcion.Text.ToString(), " , ")
        fondoSerie.FechaTCSuscripcion = IIf(ddlFechaObservadoSuscripciones.SelectedIndex > 0 And ddNumeroTCSuscripciones.Text >= "", ddlFechaObservadoSuscripciones.SelectedValue.ToString() + "," + ddNumeroTCSuscripciones.Text.ToString(), " , ")
        fondoSerie.FechaNavSuscripcion = IIf(ddlFechaNavSuscripciones.SelectedIndex > 0 And ddNumeroFechaNavSuscripciones.Text >= "", ddlFechaNavSuscripciones.SelectedValue.ToString() + "," + ddNumeroFechaNavSuscripciones.Text.ToString(), " , ")
        fondoSerie.FijacionSuscripcion = IIf(ddlfijacionSuscripcion.SelectedValue.ToString() = "", Nothing, ddlfijacionSuscripcion.SelectedValue.ToString())
        fondoSerie.FechaTCCanje = IIf(ddlfechaObservadoCanje.SelectedIndex > 0 And ddlNumeroTCCanje.Text >= "", ddlfechaObservadoCanje.SelectedValue.ToString() + "," + ddlNumeroTCCanje.Text.ToString(), " , ")
        fondoSerie.FijacionCanje = IIf(ddlFijacionNavCanje.SelectedValue.ToString() = "", Nothing, ddlFijacionNavCanje.SelectedValue.ToString())
        fondoSerie.FechaNavCanje = IIf(ddFechaNavCanje.SelectedIndex > 0 And ddNumeroFechaNavCanje.Text >= "", ddFechaNavCanje.SelectedValue.ToString() + "," + ddNumeroFechaNavCanje.Text.ToString(), " , ")

        fondoSerie.DiasHabilesCanje = chkDiasHabilesCanje.Checked
        fondoSerie.DiasHabilesRescate = chkDiasHabilesRescate.Checked
        fondoSerie.DiasHabilesSuscripciones = chkDiasHabilesSuscipciones.Checked

        fondoSerie.FechaCanjeCanje = IIf(ddlFechaCanje.SelectedIndex > 0 And txtNumeroFechaCanje.Text >= "", ddlFechaCanje.SelectedValue.ToString() + "," + txtNumeroFechaCanje.Text.ToString(), " , ")
        fondoSerie.DiasHabilesFechaCanje = chkDiasHabilesFechaCanje.Checked

        fondoSerie.FechaPatrimonio = IIf(ddlFechaPatrimonio.SelectedIndex > 0 And ddlNumeroFechaTC.Text >= "", ddlFechaPatrimonio.SelectedValue.ToString() + "," + ddlNumeroFechaPatrimonio.Text.ToString(), " , ")
        fondoSerie.DiasHabilesFechaPatrimonio = chkDiasHabilesFechaPatrimonio.Checked

        Return fondoSerie
    End Function

    Protected Sub BtnExportar_Click(sender As Object, e As EventArgs) Handles BtnExportar.Click
        Dim fondo As FondoDTO = New FondoDTO()
        Dim fondoSerie As FondoSerieDTO = New FondoSerieDTO()

        If (ddlListaRutFondo.SelectedIndex > 0) Then
            Dim arrCadena As String() = ddlListaRutFondo.SelectedItem.Text().Split(New Char() {"/"c})

            If ddlListaRutFondo.SelectedValue.Trim() = "" Then
                fondoSerie.Rut = arrCadena(0).Trim()
                fondo.NombreCorto = arrCadena(0).Trim
            Else
                fondoSerie.Rut = arrCadena(0).Trim()
                fondo.NombreCorto = arrCadena(1).Trim
            End If

        Else
            fondoSerie.Rut = Nothing
        fondo.NombreCorto = Nothing
        End If

        If ddlListaNemotecnico.SelectedItem.Text.Trim() = Nothing Then
            fondoSerie.Nemotecnico = ""
        Else
            fondoSerie.Nemotecnico = ddlListaNemotecnico.SelectedItem.Text.Trim()
        End If

        If ddlListaGrupoCompatible.SelectedValue.Trim() = Nothing Then
            fondoSerie.Nivel = 0
        Else
            fondoSerie.Nivel = ddlListaGrupoCompatible.SelectedValue.Trim()
        End If

        Dim mensaje As String = negocioFondoSerie.ExportarAExcel(fondoSerie, fondo)

        If negocioFondoSerie.rutaArchivoExcel IsNot Nothing Then
            Archivo.NavigateUrl = negocioFondoSerie.rutaArchivoExcel
            Archivo.Text = "Bajar Archivo"
        Else
            Archivo.Visible = False
        End If

        txtAccionHidden.Value = "MOSTRAR_DIALOGO"

        ShowMessages(CONST_TITULO_FONDO, mensaje, Constantes.CONST_RUTA_IMG + Constantes.CONST_IMG_LOGO, Constantes.CONST_RUTA_IMG + Constantes.CONST_IMG_INFO, False)

    End Sub

    Private Function ObtieneFondo(fondoSerie As FondoSerieDTO) As FondoSerieDTO
        If (ddlListaRutFondo.SelectedItem.Text.Trim() <> "") Then
            Dim arrCadena As String() = ddlListaRutFondo.SelectedItem.Text().Split(New Char() {"/"c})
            fondoSerie.Rut = arrCadena(0).Trim()
        Else
            fondoSerie.Rut = Nothing
        End If

        Return fondoSerie
    End Function

    Private Function ObtieneNemotecnico(fondoSerie As FondoSerieDTO) As FondoSerieDTO
        If (ddlListaNemotecnico.SelectedItem.Text <> "") Then
            fondoSerie.Nemotecnico = ddlListaNemotecnico.SelectedItem.Text()
        Else
            fondoSerie.Nemotecnico = Nothing
        End If

        Return fondoSerie
    End Function

    Private Sub filtrarGrupoCompatiblePorNemo(fondoSerie As FondoSerieDTO)
        FiltrarGrupoCompatible(fondoSerie)
    End Sub

    Protected Sub ddlListaRutFondo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlListaRutFondo.SelectedIndexChanged
        Dim fondoSerie As FondoSerieDTO = New FondoSerieDTO()

        'OBTIENE FONDO
        fondoSerie = ObtieneFondo(fondoSerie)

        filtrarPorFondo(fondoSerie)
    End Sub

    Protected Sub ddlListaNemotecnico_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlListaNemotecnico.SelectedIndexChanged
        Dim fondoSerie As FondoSerieDTO = New FondoSerieDTO()

        If (ddlListaNemotecnico.SelectedItem.Value.Trim() <> "" And ddlListaNemotecnico.SelectedItem.Value().Trim() <> "/") Then
            Dim arrCadena As String() = ddlListaNemotecnico.SelectedItem.Value().Split(New Char() {"/"c})
            If arrCadena(0).Trim() <> "" And arrCadena(1).Trim() <> "" Then
                fondoSerie.Rut = arrCadena(0).Trim()
                fondoSerie.Nemotecnico = arrCadena(1).Trim()
            End If
            ddlListaRutFondo.SelectedValue = fondoSerie.Rut
        Else
            fondoSerie.Rut = ddlListaRutFondo.SelectedItem.Value.Trim()
            fondoSerie.Nemotecnico = Nothing
        End If

        filtrarGrupoCompatiblePorNemo(fondoSerie)
        If ddlListaGrupoCompatible.Items.Count > 0 Then
            ddlListaGrupoCompatible.SelectedIndex = 1
        End If

    End Sub
End Class
