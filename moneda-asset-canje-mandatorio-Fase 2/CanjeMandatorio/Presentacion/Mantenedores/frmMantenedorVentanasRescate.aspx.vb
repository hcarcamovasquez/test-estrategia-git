Imports System.Data
Imports System.Reflection
Imports DTO
Imports Negocio
Imports DBSUtils

Partial Class Presentacion_Mantenedores_frmMantenedorVentanasRescate
    Inherits System.Web.UI.Page
    Private listaCarga As Object


    Public Const CONST_TITULO_VENTANASRESCATE As String = "Ventanas Rescate"
    Public Const CONST_TITULO_MODAL_MODIFICAR As String = "Modificar Ventanas Rescate"
    Public Const CONST_TITULO_MODAL_ElIMINAR As String = "Eliminar Ventanas Rescate"
    Public Const CONST_TITULO_MODAL_CREAR As String = "Nuevo Ventanas Rescate"

    Public Const CONST_MODIFICAR_ERROR As String = "Error al modificar los datos de Ventanas Rescate"
    Public Const CONST_MODIFICAR_EXITO As String = "Ventanas Rescate modificado con éxito"
    Public Const CONST_ELIMINAR_ERROR As String = "Error al eliminar Ventanas Rescate"
    Public Const CONST_ELIMINAR_EXITO As String = "Ventanas Rescate eliminado con éxito"
    Public Const CONST_ELIMINAR_EXISTE_EN_OTRA_TABLA As String = "Ventanas Rescate se encuentra relacionado en otra Tabla"
    Public Const CONST_INSERTAR_ERROR As String = "Error al ingresar Ventanas Rescate"
    Public Const CONST_INSERTAR_EXITO As String = "Ventanas Rescate ingresado con éxito"
    Public Const CONST_ELIMINAR_ESTADO_CERO As String = "No se puede eliminar un registro ya deshabilitado"

    Public Const CONST_VALIDA_RUT_SI_MULTIFONDO_BLANCO As String = "RUT existe en la base de aportantes, para grabar debe llenar el campo Multifondo"
    Public Const CONST_VALIDA_RUT_SI_MULTIFONDO_SI As String = "RUT y Multifondo ya existen en la base de aportantes"

    Public Const CONST_AGREGAR_EXISTE_CERTIFICADO As String = "Ventanas Rescate ya se encuentra registrado"
    Public Const CONST_AGREGAR_EXISTE_EN_LA_GRILLA As String = "Ventanas Rescate ya se encuentra en la lista"

    Public Const CONST_LISTAS_DROPDOWN As String = "Alguna de las listas está vacía"

    Public Const CONST_COL_NUMERO As Integer = 1
    Public Const CONST_COL_CORRELATIVO As Integer = 2
    Public Const CONST_COL_FECHA As Integer = 6
    Public Const CONST_COL_APORTANTE_RUT As Integer = 7
    Public Const CONST_COL_MULTIFONDO As Integer = 11
    Public Const CONST_COL_NEMOTECNICO As Integer = 12
    Public Const CONST_COL_ESTADO As Integer = 16

    Public Const CONST_SIN_RESULTADOS As String = "No se obtuvieron resultados de la búsqueda"
    Public Const CONST_GRILLA_ASIGNACION_VACIA As String = "No hay elementos para almacenar"
    Public Const CONST_ERROR_AL_GUARDAR As String = "Error al [accion] Ventanas Rescate"
    Public Const CONST_EXITO_AL_GUARDAR As String = "Ventanas Rescate [accion] con Exito"

    Dim ContadorRegistrosCargados As Integer = 0


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

        'LLENA LOS COMBOS DE BUSQUEDA NOMBRE APORTANTE Y NOMBRE FONDO
        CargaFiltroNombreFondo()
        CargaFiltroNemotecnico()

        GrvTabla.DataSource = Nothing
        GrvTabla.DataBind()
        Session("lista") = Nothing
    End Sub

    Private Sub CargaFiltroNombreFondo()
        Dim lista As New List(Of VentanasRescateDTO)
        Dim VentanasRescate As New VentanasRescateDTO()
        Dim NegocioVentanasRescate As VentanasRescateNegocio = New VentanasRescateNegocio
        Dim VentanasRescatevacia As New VentanasRescateDTO

        lista = NegocioVentanasRescate.ConsultarNombreFondo(VentanasRescate)

        If lista.Count = 0 Then
            lista.Add(VentanasRescatevacia)
        Else
            lista.Insert(0, VentanasRescatevacia)
        End If

        ddlNombreFondoBuscar.DataSource = lista

        ddlNombreFondoBuscar.DataMember = "RutRNombreCortoFondo"
        ddlNombreFondoBuscar.DataValueField = "RutRNombreCortoFondo"
        ddlNombreFondoBuscar.DataTextField = "RutRNombreCortoFondo"
        ddlNombreFondoBuscar.DataBind()
    End Sub

    Private Sub CargaFiltroNemotecnico()
        Dim lista As New List(Of VentanasRescateDTO)
        Dim VentanasRescate As New VentanasRescateDTO()
        Dim NegocioVentanasRescate As VentanasRescateNegocio = New VentanasRescateNegocio
        Dim VentanasRescatevacia As New VentanasRescateDTO

        VentanasRescate.FN_Nombre_Corto = ""
        lista = NegocioVentanasRescate.ConsultarNemotecnico(VentanasRescate)

        If lista.Count = 0 Then
            lista.Add(VentanasRescatevacia)
            ddlNombreFondoBuscar.Items.Insert(0, New ListItem(0, String.Empty))
        Else
            lista.Insert(0, VentanasRescatevacia)
            ddlNemotecnicoBuscar.DataSource = lista
            ddlNemotecnicoBuscar.DataMember = "NemotecnicoBusqueda"
            ddlNemotecnicoBuscar.DataValueField = "NemotecnicoBusqueda"
            ddlNemotecnicoBuscar.DataBind()
            ddlNemotecnicoBuscar.Items.Insert(0, New ListItem(0, String.Empty))
        End If
    End Sub
#End Region

#Region "LIMPIAR BUSQUEDA INICIAL"
    Protected Sub btnLimpiarFrm_Click(sender As Object, e As EventArgs)
        txtAccionHidden.Value = ""
        ddlNombreFondoBuscar.SelectedIndex = 0
        ddlNemotecnicoBuscar.SelectedIndex = 0
        BtnExportar.Enabled = False
        GrvTabla.DataSource = Nothing
        GrvTabla.DataBind()
        Session("lista") = Nothing

        lblMensaje.Text = ""

    End Sub
#End Region

#Region "BUSQUEDA INICIAL"
    Protected Sub BtnBuscar_Click(sender As Object, e As EventArgs) Handles BtnBuscar.Click
        txtAccionHidden.Value = ""
        If ddlNombreFondoBuscar.SelectedValue.Trim() = Nothing And ddlNemotecnicoBuscar.SelectedValue.Trim() = Nothing Then
            CargarTodosVentanasRescate()

            If GrvTabla.Rows.Count = 0 Then
                BtnExportar.Enabled = False
                ShowAlert(CONST_SIN_RESULTADOS)
            Else
                BtnExportar.Enabled = True
            End If

        Else
            FindVentanasRescate()
            If GrvTabla.Rows.Count = 0 Then
                BtnExportar.Enabled = False
                ShowAlert(CONST_SIN_RESULTADOS)
            Else
                BtnExportar.Enabled = True
            End If

            txtAccionHidden.Value = ""
        End If

    End Sub

    Private Sub CargarTodosVentanasRescate()
        Dim VentanasRescate As VentanasRescateDTO = New VentanasRescateDTO()
        Dim negocio As VentanasRescateNegocio = New VentanasRescateNegocio

        GrvTabla.DataSource = negocio.ConsultarTodos(VentanasRescate)
        GrvTabla.DataBind()
    End Sub

    Private Sub FindVentanasRescate()
        Dim VentanasRescate As VentanasRescateDTO = New VentanasRescateDTO()
        Dim negocio As VentanasRescateNegocio = New VentanasRescateNegocio

        If ddlNombreFondoBuscar.SelectedValue.Trim() = Nothing Then
            VentanasRescate.FN_Nombre_Corto = Nothing
        Else
            VentanasRescate.FN_Nombre_Corto = ddlNombreFondoBuscar.SelectedValue
        End If

        If (ddlNombreFondoBuscar.SelectedIndex > 0) Then
            Dim arrCadena As String() = ddlNombreFondoBuscar.SelectedItem.Text().Split(New Char() {"/"c})

            VentanasRescate.FN_RUT = arrCadena(0).Trim()
            VentanasRescate.FN_Nombre_Corto = arrCadena(1).Trim()
        Else
            VentanasRescate.FN_RUT = Nothing
            VentanasRescate.FN_Nombre_Corto = Nothing
        End If

        If ddlNemotecnicoBuscar.SelectedValue.Trim() = Nothing Then
            VentanasRescate.FS_Nemotecnico = Nothing
        Else
            VentanasRescate.FS_Nemotecnico = ddlNemotecnicoBuscar.SelectedValue
        End If


        GrvTabla.DataSource = negocio.GetListaVentanasRescateConFiltro(VentanasRescate)
        GrvTabla.DataBind()

    End Sub
#End Region

#Region "CARGA COMBOS MODAL TODOS SIN FILTRO"
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

    Public Sub CargaNemotecnicoModal()

        Dim NegocioFondoSerie As FondoSeriesNegocio = New FondoSeriesNegocio
        Dim fondoSeries As FondoSerieDTO = New FondoSerieDTO()
        Dim fondo As FondoDTO = New FondoDTO()

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
        FormateoLimpiarDatosModal()
        FormateoEstiloFormCrear()

        'CARGAS DDL MODAL
        CargaNombreFondoModal()
        CargaNemotecnicoModal()

        Session("ContadorRegistros") = 0

        txtAccionHidden.Value = "CREAR"

    End Sub

    Private Sub FormateoLimpiarDatosModal()

        txtModalFechaSolicitud.Text = ""
        txtModalFechaNAV.Text = ""
        txtModalFechaPago.Text = ""

        ddlModalNombreFondo.Enabled = True
        ddlModalNemotecnico.Enabled = True

        grvAsignacion.Columns(0).Visible = False
        grvAsignacion.DataSource = Nothing
        grvAsignacion.DataBind()
        Session("lista") = Nothing

    End Sub

    Private Sub FormateoEstiloFormCrear()
        btnModalModificar.Enabled = False
        btnModalModificar.Visible = True
        btnModalGuardar.Enabled = True
        btnModalGuardar.Visible = True
        btnModalAgregar.Enabled = True
        btnModalAgregar.Visible = True
        btnModalEliminarGrupo.Enabled = False

        ddlModalNombreFondo.Enabled = True
        ddlModalNemotecnico.Enabled = True
        txtModalFechaSolicitud.Enabled = False
        txtModalFechaNAV.Enabled = False
        txtModalFechaPago.Enabled = False

        lnkBtnModalFechaSolicitud.Visible = True
        BtnLimpiarFechaDesde.Visible = True
        lnkBtnModalFechaNAV.Visible = True
        lnkBtnModalBorrarFechaNAV.Visible = True
        lnkBtnModalFechaPago.Visible = True
        lnkBtnModalBorrarFechaPago.Visible = True

        lblModalTitle.Text = CONST_TITULO_MODAL_CREAR
    End Sub
#End Region

#Region "CARGA NEMOTECNICO CUANDO CAMBIA COMBO NOMBRE FONDO"
    Public Sub CargarNemotecnicoPorNombreFondoModal()
        Dim negocioFondo As FondosNegocio = New FondosNegocio
        Dim NegocioFondoSerie As FondoSeriesNegocio = New FondoSeriesNegocio
        Dim fondoSeries As FondoSerieDTO = New FondoSerieDTO()
        Dim fondo As FondoDTO = New FondoDTO()

        fondo.RazonSocial = ddlModalNombreFondo.SelectedValue
        Dim listafondo As List(Of FondoDTO) = negocioFondo.RutByNombreFondo(fondo)

        If listafondo.Count > 0 Then
            For Each fondos As FondoDTO In listafondo
                Dim rut = fondos.Rut

                fondoSeries.Rut = rut
                Dim listafondoSeries As List(Of FondoSerieDTO) = NegocioFondoSerie.GrupoSeriesPorRut(fondoSeries)
                If listafondoSeries.Count = 0 Then
                    ddlModalNemotecnico.Items.Insert(0, New ListItem("", ""))
                    ddlModalNemotecnico.SelectedIndex = 0
                Else



                    ddlModalNemotecnico.DataSource = listafondoSeries
                    ddlModalNemotecnico.DataMember = "Nemotecnico"
                    ddlModalNemotecnico.DataValueField = "Nemotecnico"
                    ddlModalNemotecnico.DataBind()
                    ddlModalNemotecnico.Items.Insert(0, New ListItem("     ", ""))
                    ddlModalNemotecnico.SelectedIndex = 0


                End If
            Next
        End If
    End Sub
#End Region

#Region "CARGA NOMBRE FONDO CUANDO CAMBIA COMBO NEMOTECNICO"
    Public Sub CargarNombreFondoPorNemotecnicoModal()
        Dim NegocioFondoSerie As FondosNegocio = New FondosNegocio
        Dim fondoSeries As FondoSerieDTO = New FondoSerieDTO()
        Dim fondo As FondoDTO = New FondoDTO()

        fondoSeries.Nemotecnico = ddlModalNemotecnico.SelectedValue
        Dim listafondoSeries As List(Of FondoDTO) = NegocioFondoSerie.GetNombrePorNemotecnico(fondoSeries)

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

#Region "AGREGA A LA GRILLA DE ASIGNACION DOCUMENTOS"
    Private Sub btnModalAgregar_Click(sender As Object, e As EventArgs) Handles btnModalAgregar.Click
        Dim negocio As VentanasRescateNegocio = New VentanasRescateNegocio
        'TRAE LA CANTIDAD DE VENTANAS 
        Dim CantidadVentanasRescate As Integer
        Dim VentanasRescateCount As VentanasRescateDTO = New VentanasRescateDTO()
        VentanasRescateCount.FN_Nombre_Corto = ddlModalNombreFondo.SelectedValue

        VentanasRescateCount.FS_Nemotecnico = ddlModalNemotecnico.Text

        CantidadVentanasRescate = negocio.GetCountVentanaRescate(VentanasRescateCount)

        Session("ContadorRegistrosTotales") = CantidadVentanasRescate + Session("ContadorRegistros")

        If Session("ContadorRegistrosTotales") > 14 Then
            ShowAlert("Se pueden agregar Máximo 15 registros")
            Return
        End If

        Dim lista As New List(Of VentanasRescateDTO)
        Dim VentanasRescate As New VentanasRescateDTO


        If Session("lista") IsNot Nothing Then
            lista = Session("lista")
        Else
            lista = New List(Of VentanasRescateDTO)
        End If

        If ddlModalNombreFondo.Text <> "" Then ' And ddlModalNemotecnico.Text <> "" Then
            If txtModalFechaSolicitud.Text.Trim <> "" And txtModalFechaNAV.Text.Trim <> "" And txtModalFechaPago.Text.Trim <> "" Then

                If txtAccionHidden.Value = "CREAR" Then
                    VentanasRescate.FN_Nombre_Corto = ddlModalNombreFondo.SelectedValue
                    'VentanasRescate.FS_Nemotecnico = ddlModalNemotecnico.SelectedValue
                    VentanasRescate.FS_Nemotecnico = ddlModalNemotecnico.Text
                    VentanasRescate.RES_Fecha_Solicitud = txtModalFechaSolicitud.Text
                    VentanasRescate.VTRES_Fecha_NAV = txtModalFechaNAV.Text
                    VentanasRescate.VTRES_Fecha_Pago = txtModalFechaPago.Text
                    VentanasRescate.VTRES_Usuario_Ingreso = Session("NombreUsuario")
                    VentanasRescate.VTRES_Fecha_Ingreso = DateTime.Now
                    'VentanasRescate.FN_RUT = 

                ElseIf txtAccionHidden.Value = "MODIFICAR" Then
                    'codigo modificar
                    VentanasRescate.FN_Nombre_Corto = ddlModalNombreFondo.SelectedValue
                    'VentanasRescate.FS_Nemotecnico = ddlModalNemotecnico.SelectedValue
                    VentanasRescate.FS_Nemotecnico = ddlModalNemotecnico.Text
                    VentanasRescate.RES_Fecha_Solicitud = txtModalFechaSolicitud.Text
                    VentanasRescate.VTRES_Fecha_NAV = txtModalFechaNAV.Text
                    VentanasRescate.VTRES_Fecha_Pago = txtModalFechaPago.Text
                    VentanasRescate.VTRES_Usuario_Ingreso = Session("NombreUsuario")
                    VentanasRescate.VTRES_Fecha_Ingreso = DateTime.Now
                    VentanasRescate.VTRES_Usuario_Modificacion = Session("NombreUsuario")
                    VentanasRescate.VTRES_Fecha_Modificacion = DateTime.Now

                End If


                Dim existeVentanasRescate As Boolean = negocio.ExisteVentanasRescate(VentanasRescate)

                If Not existeVentanasRescate Then
                    Dim existeEnGrilla As Boolean = ExisteEnGrillaAsignacion(lista, VentanasRescate)
                    If Not existeEnGrilla Then
                        If txtAccionHidden.Value = "CREAR" Then
                            ddlModalNombreFondo.Enabled = False
                            ddlModalNemotecnico.Enabled = False
                        End If

                        lista.Add(VentanasRescate)
                        Session("lista") = lista

                        'Solo Agregar Nuevos
                        Dim listaAgregar As New List(Of VentanasRescateDTO)

                        If Session("listaAgregado") Is Nothing Then
                            listaAgregar.Add(VentanasRescate)
                            Session("listaAgregado") = listaAgregar
                        Else
                            listaAgregar = Session("listaAgregado")
                            listaAgregar.Add(VentanasRescate)
                            Session("listaAgregado") = listaAgregar
                        End If

                        grvAsignacion.DataSource = lista
                        grvAsignacion.DataBind()

                        Session("ContadorRegistros") = Session("ContadorRegistros") + 1

                        ddlModalNombreFondo.Enabled = False
                        ddlModalNemotecnico.Enabled = False

                    Else
                        ShowAlert(CONST_AGREGAR_EXISTE_EN_LA_GRILLA)
                    End If


                Else
                    ShowAlert(CONST_AGREGAR_EXISTE_CERTIFICADO)
                End If
            Else
                ShowAlert("Los campos de fechas NO pueden estar vacíos")
            End If

        Else
            'TODO Buscar otro metodo que muestre emnsajes de alerta sin que cierre el modal
            ShowAlert(CONST_LISTAS_DROPDOWN)
        End If
        ClientScript.RegisterStartupScript(Me.GetType(), "myModal", "$('#myModal').modal('show');", True)
    End Sub

    Private Function ExisteEnGrillaAsignacion(lista As List(Of VentanasRescateDTO), VentanasRescate As VentanasRescateDTO) As Boolean
        Dim findVentanasRescate As VentanasRescateDTO = lista.Find(Function(l) l.FN_Nombre_Corto = VentanasRescate.FN_Nombre_Corto And l.FS_Nemotecnico = VentanasRescate.FS_Nemotecnico And l.RES_Fecha_Solicitud = VentanasRescate.RES_Fecha_Solicitud And l.VTRES_Fecha_NAV = VentanasRescate.VTRES_Fecha_NAV And l.VTRES_Fecha_Pago = VentanasRescate.VTRES_Fecha_Pago)
        Return (findVentanasRescate IsNot Nothing)

    End Function

#End Region

#Region "GUARDA-MODIFICA Y ELIMINA CERTIFICADOS MODAL"
    Protected Sub btnModalGuardar_Click(sender As Object, e As EventArgs) Handles btnModalGuardar.Click

        If grvAsignacion.Rows.Count = 0 Then 'And txtAccionHidden.Value = "CREAR" Then
            ShowAlert("No hay elementos para almacenar")
            Exit Sub
        End If


        Dim listaVentanasRescate As List(Of VentanasRescateDTO) = New List(Of VentanasRescateDTO)
        Dim listaVentanasRescateAgregados As List(Of VentanasRescateDTO) = New List(Of VentanasRescateDTO)
        Dim listaVentanasRescateModificados As List(Of VentanasRescateDTO) = New List(Of VentanasRescateDTO)
        Dim listaVentanasRescateEliminados As List(Of VentanasRescateDTO) = New List(Of VentanasRescateDTO)
        Dim VentanasRescate As VentanasRescateDTO = New VentanasRescateDTO()
        Dim negocio As VentanasRescateNegocio = New VentanasRescateNegocio


        If Session("lista") IsNot Nothing Then
            listaVentanasRescate = Session("lista")
        End If
        If Session("listaAgregado") IsNot Nothing Then
            listaVentanasRescateAgregados = Session("listaAgregado")
        End If
        If Session("listaModificado") IsNot Nothing Then
            listaVentanasRescateModificados = Session("listaModificado")
        End If
        If Session("listaEliminar") IsNot Nothing Then
            listaVentanasRescateEliminados = Session("listaEliminar")
        End If

        Dim accion As String
        Dim mensajeAccionExito As String
        Dim mensajeAccionError As String

        If txtAccionHidden.Value = "CREAR" Then
            accion = "AGREGAR_VENTANASRESCATE"


            mensajeAccionExito = CONST_INSERTAR_EXITO.Replace("[accion]", "insertado")
            mensajeAccionError = CONST_INSERTAR_ERROR.Replace("[accion]", "insertar")


        ElseIf txtAccionHidden.Value = "MODIFICAR" Then
            accion = "MODIFICAR_VENTANASRESCATE"
            mensajeAccionExito = CONST_MODIFICAR_EXITO.Replace("[accion]", "modificado")
            mensajeAccionError = CONST_MODIFICAR_ERROR.Replace("[accion]", "modificar")

        ElseIf txtAccionHidden.Value = "ELIMINAR" Then
            accion = "ELIMINAR_VENTANASRESCATE"
            mensajeAccionExito = CONST_ELIMINAR_EXITO.Replace("[accion]", "eliminado")
            mensajeAccionError = CONST_ELIMINAR_ERROR.Replace("[accion]", "eliminar")

        Else
            txtAccionHidden.Value = ""
            Exit Sub
        End If

        Dim ID As Integer
        Dim Fondo As String
        Dim Nemotecnico As String
        Dim FechaSolicitud As Date
        Dim FechaNAV As Date
        Dim FechaPago As Date

        If accion = "AGREGAR_VENTANASRESCATE" Then
            listaVentanasRescateAgregados = Nothing
            listaVentanasRescateModificados = Nothing
            listaVentanasRescateEliminados = Nothing
            If (negocio.GuardarVentanasRescate(accion, listaVentanasRescate, listaVentanasRescateAgregados, listaVentanasRescateModificados, listaVentanasRescateEliminados)) Then

                If txtAccionHidden.Value = "CREAR" Then
                    ShowAlert(CONST_INSERTAR_EXITO)
                ElseIf txtAccionHidden.Value = "MODIFICAR" Then
                    ShowAlert(CONST_MODIFICAR_EXITO)
                End If
            Else
                'ShowMesagges(CONST_TITULO_VENTANASRESCATE, mensajeAccionError, Constantes.CONST_RUTA_IMG + Constantes.CONST_IMG_LOGO, Constantes.CONST_RUTA_IMG + Constantes.CONST_IMG_ERROR)
                If txtAccionHidden.Value = "CREAR" Then
                    ShowAlert(CONST_INSERTAR_ERROR)
                ElseIf txtAccionHidden.Value = "MODIFICAR" Then
                    ShowAlert(CONST_MODIFICAR_ERROR)
                End If
            End If
        ElseIf accion = "MODIFICAR_VENTANASRESCATE" Then
            Dim VentanasRescateModif As VentanasRescateDTO = New VentanasRescateDTO()
            Dim negocioVentanasRe As VentanasRescateNegocio = New VentanasRescateNegocio
            Fondo = ""
            Nemotecnico = ""
            For Each row As GridViewRow In grvAsignacion.Rows
                ID = row.Cells(1).Text.Trim()
                Fondo = HttpUtility.HtmlDecode(row.Cells(2).Text.Trim())
                'Fondo = row.Cells(2).Text.Trim()
                Nemotecnico = Trim(HttpUtility.HtmlDecode(row.Cells(3).Text.Replace("&nbsp;", ""))) 'row.Cells(3).Text.Trim()
                FechaSolicitud = row.Cells(4).Text.Trim()
                FechaNAV = row.Cells(5).Text.Trim()
                FechaPago = row.Cells(6).Text.Trim()

                VentanasRescateModif = New VentanasRescateDTO()

                VentanasRescateModif.VTRES_ID = ID
                VentanasRescateModif.FN_Nombre_Corto = Fondo
                VentanasRescateModif.FS_Nemotecnico = Nemotecnico
                VentanasRescateModif.RES_Fecha_Solicitud = FechaSolicitud
                VentanasRescateModif.VTRES_Fecha_NAV = FechaNAV
                VentanasRescateModif.VTRES_Fecha_Pago = FechaPago
                VentanasRescateModif.VTRES_Usuario_Ingreso = Session("NombreUsuario")

                'INSERTA LA TEMPORAL PARA SU POSTERIOR COMPARACION
                negocioVentanasRe.IngresarTemporalVentanasRescate(VentanasRescateModif)
            Next

            Dim negocioModif As VentanasRescateNegocio = New VentanasRescateNegocio
            Dim solicitudModificarEliminar As Integer

            'COMPARA LA DATA CON LA TEMPORAL Y/O ACTUALIZA AGREGA Y/O ELIMINA
            VentanasRescateModif.FN_Nombre_Corto = Fondo
            VentanasRescateModif.FS_Nemotecnico = Nemotecnico
            VentanasRescateModif.VTRES_Usuario_Ingreso = Session("NombreUsuario")
            solicitudModificarEliminar = negocioModif.ModificarEliminarVentanasRescate(VentanasRescateModif)

            txtAccionHidden.Value = "MOSTRAR_DIALOGO"

            If solicitudModificarEliminar = Constantes.CONST_OPERACION_EXITOSA Then
                'Ingresado con Exito
                ShowAlert(CONST_MODIFICAR_EXITO)
            Else
                'Error en la BBDD)
                ShowAlert(CONST_MODIFICAR_ERROR)
            End If
        ElseIf accion = "ELIMINAR_VENTANASRESCATE" Then
            listaVentanasRescate = Nothing
            listaVentanasRescateModificados = Nothing
            listaVentanasRescateAgregados = Nothing
            If (negocio.GuardarVentanasRescate(accion, listaVentanasRescate, listaVentanasRescateAgregados, listaVentanasRescateModificados, listaVentanasRescateEliminados)) Then

                If txtAccionHidden.Value = "CREAR" Then
                    ShowAlert(CONST_INSERTAR_EXITO)
                ElseIf txtAccionHidden.Value = "MODIFICAR" Then
                    ShowAlert(CONST_MODIFICAR_EXITO)
                ElseIf txtAccionHidden.Value = "ELIMINAR" Then
                    ShowAlert(CONST_ELIMINAR_EXITO)
                End If
            Else
                'ShowMesagges(CONST_TITULO_VENTANASRESCATE, mensajeAccionError, Constantes.CONST_RUTA_IMG + Constantes.CONST_IMG_LOGO, Constantes.CONST_RUTA_IMG + Constantes.CONST_IMG_ERROR)
                If txtAccionHidden.Value = "CREAR" Then
                    ShowAlert(CONST_INSERTAR_ERROR)
                ElseIf txtAccionHidden.Value = "MODIFICAR" Then
                    ShowAlert(CONST_MODIFICAR_ERROR)
                ElseIf txtAccionHidden.Value = "ELIMINAR" Then
                    ShowAlert(CONST_ELIMINAR_ERROR)
                End If
            End If
        End If

        txtAccionHidden.Value = ""
        CargarTodosVentanasRescate()
        DataInitial()
        Me.GrvTabla.DataSource = Nothing
        GrvTabla.DataBind()
        BtnExportar.Enabled = False
    End Sub
#End Region

#Region "BOTON AGREGA A LA MODAL DOCUMENTOS A MODIFICAR"
    Protected Sub BtnModificar_Click(sender As Object, e As EventArgs)
        Session("listaModificado") = Nothing
        Session("listaAgregado") = Nothing
        grvAsignacion.Columns(0).Visible = True
        Dim negocio As VentanasRescateNegocio = New VentanasRescateNegocio
        Dim VentanasRescateSelect As VentanasRescateDTO = GetVentanasRescateSelect()


        Dim VentanasRescateActualizado As VentanasRescateDTO = negocio.GetVentanasRescate(VentanasRescateSelect)
        FormateoFormDatos(VentanasRescateActualizado)
        FormateoEstiloFormModificar()

        ddlModalNemotecnico.Enabled = False
        ddlModalNombreFondo.Enabled = False

        ''TRAE LA CANTIDAD DE VENTANAS 
        Session("ContadorRegistros") = 0
        Session("ContadorRegistrosTotales") = 0
        txtAccionHidden.Value = "MODIFICAR"
        Session("CRUD") = "MODIFICAR"

    End Sub

    Private Function GetVentanasRescateSelect() As VentanasRescateDTO
        Dim VentanasRescate As New VentanasRescateDTO

        For Each row As GridViewRow In GrvTabla.Rows
            Dim chk As RadioButton = row.Cells(0).Controls(1)
            If chk IsNot Nothing And chk.Checked Then

                VentanasRescate.FN_Nombre_Corto = HttpUtility.HtmlDecode(row.Cells(1).Text)

                'VentanasRescate.FN_Nombre_Corto = row.Cells(1).Text.Trim()

                VentanasRescate.FS_Nemotecnico = Trim(HttpUtility.HtmlDecode(row.Cells(2).Text.Replace("&nbsp;", "")))
                VentanasRescate.RES_Fecha_Solicitud = row.Cells(3).Text.Trim()
                VentanasRescate.VTRES_Fecha_NAV = row.Cells(4).Text.Trim()
                VentanasRescate.VTRES_Fecha_Pago = row.Cells(5).Text.Trim()
                'VentanasRescate.VTRES_Estado = row.Cells(10).Text.Trim()
            End If
        Next

        Return VentanasRescate
    End Function

    Private Sub FormateoFormDatos(VentanasRescate As VentanasRescateDTO)
        Dim negocio As VentanasRescateNegocio = New VentanasRescateNegocio

        If txtAccionHidden.Value = "MODIFICAR" Then
            ddlModalNemotecnico.Enabled = False
            ddlModalNombreFondo.Enabled = False
            txtModalFechaSolicitud.Enabled = False
            txtModalFechaNAV.Enabled = False
            txtModalFechaPago.Enabled = False
        ElseIf txtAccionHidden.Value = "ELIMINAR" Then
            ddlModalNemotecnico.Enabled = False
            ddlModalNombreFondo.Enabled = False
            txtModalFechaSolicitud.Enabled = False
            txtModalFechaNAV.Enabled = False
            txtModalFechaPago.Enabled = False
        End If


        grvAsignacion.DataSource = Nothing
        grvAsignacion.DataBind()
        Session("lista") = Nothing


        CargaNombreFondoModal()
        CargaNemotecnicoModal()

        ddlModalNombreFondo.SelectedValue = VentanasRescate.FN_Nombre_Corto
        If VentanasRescate.FS_Nemotecnico <> "" Then
            ddlModalNemotecnico.Text = VentanasRescate.FS_Nemotecnico
        End If

        txtModalFechaSolicitud.Text = VentanasRescate.RES_Fecha_Solicitud
        txtModalFechaNAV.Text = VentanasRescate.VTRES_Fecha_NAV
        txtModalFechaPago.Text = VentanasRescate.VTRES_Fecha_Pago

        txtModalVariableUsuarioIngreso.Text = VentanasRescate.VTRES_Usuario_Ingreso
        txtModalVariableFechaIngreso.Text = VentanasRescate.VTRES_Fecha_Ingreso
        txtModalVariableEstado.Text = VentanasRescate.VTRES_Estado

        Dim lista As List(Of VentanasRescateDTO) = negocio.ConsultarPorNombreFondo_Nemotecnico(VentanasRescate)
        grvAsignacion.DataSource = lista
        grvAsignacion.DataBind()

        Session("lista") = lista
        Session("listaEliminar") = Nothing

    End Sub

    Private Sub FormateoEstiloFormModificar()
        btnModalGuardar.Enabled = True
        btnModalGuardar.Visible = True
        btnModalEliminarGrupo.Enabled = False

        btnModalAgregar.Enabled = True
        btnModalAgregar.Visible = True
        btnModalModificar.Enabled = False
        btnModalModificar.Visible = True
        btnModalEliminarCertificado.Enabled = False
        btnModalEliminarCertificado.Visible = True

        lnkBtnModalFechaSolicitud.Visible = True
        BtnLimpiarFechaDesde.Visible = True
        lnkBtnModalFechaNAV.Visible = True
        lnkBtnModalBorrarFechaNAV.Visible = True
        lnkBtnModalFechaPago.Visible = True
        lnkBtnModalBorrarFechaPago.Visible = True
        lblMensaje.Text = ""

        lblModalTitle.Text = CONST_TITULO_MODAL_MODIFICAR
    End Sub
#End Region

#Region "AGREGA Y ELIMINA REGISTROS DE GRILLA PARA MODIFICAR- MODIFICA REGISTROS"
    Private Sub btnModalModificar_Click(sender As Object, e As EventArgs) Handles btnModalModificar.Click

        txtAccionHidden.Value = "MODIFICAR"



        Dim agregadoCorrectamente As Boolean

        Dim BandCheck As Integer

        BandCheck = 0

        Session("ID_Seleccionado") = Nothing

        For Each row As GridViewRow In grvAsignacion.Rows
            Dim chk As RadioButton = row.Cells(0).Controls(1)
            If chk IsNot Nothing And chk.Checked Then
                BandCheck = 1
                Session("ID_Seleccionado") = row.Cells(1).Text.Trim()
            End If
        Next

        If BandCheck = 1 Then
            Dim negocio As VentanasRescateNegocio = New VentanasRescateNegocio
            Dim VentanasRescate As New VentanasRescateDTO

            VentanasRescate.FN_Nombre_Corto = ddlModalNombreFondo.SelectedValue
            VentanasRescate.FS_Nemotecnico = ddlModalNemotecnico.SelectedValue
            VentanasRescate.RES_Fecha_Solicitud = txtModalFechaSolicitud.Text
            VentanasRescate.VTRES_Fecha_NAV = txtModalFechaNAV.Text
            VentanasRescate.VTRES_Fecha_Pago = txtModalFechaPago.Text

            Dim existeVentanasRescate As Boolean = negocio.ExisteVentanasRescate(VentanasRescate)

            If Not existeVentanasRescate Then
                Dim aVentanasRescateAnterior As VentanasRescateDTO = GetVentanasRescateSelectModificar(grvAsignacion)

                agregadoCorrectamente = AgregarElementoGridViewAsignacion()

                If (agregadoCorrectamente) Then
                    EliminarElementoGridViewAsignacion(aVentanasRescateAnterior)
                End If
            Else
                ShowAlert(CONST_AGREGAR_EXISTE_CERTIFICADO)
            End If
        Else
            ShowAlert("Debe seleccionar un elemento en la lista para modificar")
            Return
        End If
    End Sub

    Private Function GetVentanasRescateSelectModificar(tabla As GridView) As VentanasRescateDTO
        Dim VentanasRescate As New VentanasRescateDTO
        For Each row As GridViewRow In tabla.Rows
            Dim chk As RadioButton = row.Cells(0).Controls(1)
            If chk IsNot Nothing And chk.Checked Then
                VentanasRescate.VTRES_ID = row.Cells(1).Text.Trim()
                VentanasRescate.FN_Nombre_Corto = HttpUtility.HtmlDecode(row.Cells(2).Text.Trim())
                'VentanasRescate.FN_Nombre_Corto = row.Cells(2).Text.Trim()
                VentanasRescate.FS_Nemotecnico = Trim(HttpUtility.HtmlDecode(row.Cells(3).Text.Replace("&nbsp;", ""))) 'row.Cells(3).Text.Trim()
                VentanasRescate.RES_Fecha_Solicitud = row.Cells(4).Text.Trim()
                VentanasRescate.VTRES_Fecha_NAV = row.Cells(5).Text.Trim()
                VentanasRescate.VTRES_Fecha_Pago = row.Cells(6).Text.Trim()
            End If
        Next

        Return VentanasRescate
    End Function

    Private Function AgregarElementoGridViewAsignacion() As Boolean
        Dim lista As New List(Of VentanasRescateDTO)
        Dim VentanasRescate As New VentanasRescateDTO
        Dim resultadoAgregar As Boolean = False

        If Session("lista") IsNot Nothing Then
            lista = Session("lista")
        Else
            lista = New List(Of VentanasRescateDTO)
        End If

        If ddlModalNemotecnico.SelectedIndex > -1 And ddlModalNombreFondo.SelectedIndex > -1 Then
            VentanasRescate.VTRES_ID = Session("ID_Seleccionado")
            VentanasRescate.FN_Nombre_Corto = ddlModalNombreFondo.SelectedValue
            VentanasRescate.FS_Nemotecnico = ddlModalNemotecnico.SelectedValue
            VentanasRescate.RES_Fecha_Solicitud = txtModalFechaSolicitud.Text
            VentanasRescate.VTRES_Fecha_NAV = txtModalFechaNAV.Text
            VentanasRescate.VTRES_Fecha_Pago = txtModalFechaPago.Text

            lista.Add(VentanasRescate)
            Session("lista") = lista

            'Solo elementos Modificados
            Dim listaModificar As New List(Of VentanasRescateDTO)

            If Session("listaModificado") Is Nothing Then
                listaModificar.Add(VentanasRescate)
                Session("listaModificado") = listaModificar
            Else
                listaModificar = Session("listaModificado")
                listaModificar.Add(VentanasRescate)
                Session("listaModificado") = listaModificar
            End If

            grvAsignacion.DataSource = listaModificar
            grvAsignacion.DataBind()

            btnModalGuardar.Enabled = (grvAsignacion.Rows.Count > 0)

            resultadoAgregar = True
            btnModalGuardar.Enabled = True

        Else
            ShowAlert(CONST_LISTAS_DROPDOWN)
        End If

        Return resultadoAgregar
    End Function

    Private Sub EliminarElementoGridViewAsignacion(aVentanasRescateAnterior As VentanasRescateDTO)
        Dim listaEliminar As New List(Of VentanasRescateDTO)

        If Session("listaEliminar") IsNot Nothing Then
            listaEliminar = Session("listaEliminar")
        End If


        Dim lista As List(Of VentanasRescateDTO) = Session("lista")

        Dim objetoEliminar = lista.First(Function(t) t.FN_Nombre_Corto = aVentanasRescateAnterior.FN_Nombre_Corto And t.FS_Nemotecnico = aVentanasRescateAnterior.FS_Nemotecnico And t.RES_Fecha_Solicitud = aVentanasRescateAnterior.RES_Fecha_Solicitud And t.VTRES_Fecha_NAV = aVentanasRescateAnterior.VTRES_Fecha_NAV And t.VTRES_Fecha_Pago = aVentanasRescateAnterior.VTRES_Fecha_Pago)
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
        Dim VentanasRescateAnterior As VentanasRescateDTO = GetVentanasRescateSelectModificar(grvAsignacion)
        EliminarElementoGridViewAsignacion(VentanasRescateAnterior)
        Session("ContadorRegistros") = Session("ContadorRegistros") - 1
        'btnModalGuardar.Enabled = True
        Me.GrvTabla.DataSource = Nothing
        GrvTabla.DataBind()
        BtnExportar.Enabled = False
    End Sub
#End Region

#Region "BOTON ELIMINAR "
    Protected Sub BtnEliminar_Click(sender As Object, e As EventArgs) Handles BtnEliminar.Click

        lnkBtnModalFechaSolicitud.Visible = False
        BtnLimpiarFechaDesde.Visible = False
        lnkBtnModalFechaNAV.Visible = False
        lnkBtnModalBorrarFechaNAV.Visible = False
        lnkBtnModalFechaPago.Visible = False
        lnkBtnModalBorrarFechaPago.Visible = False

        Dim Negocio As VentanasRescateNegocio = New VentanasRescateNegocio
        Dim certificadoSelect As VentanasRescateDTO = GetVentanasRescateSelectEliminar(GrvTabla)

        Dim fondo As FondoDTO = New FondoDTO
        fondo.RazonSocial = certificadoSelect.FN_Nombre_Corto
        Dim lista As List(Of FondoDTO) = Negocio.CompararDatosFondos(fondo)


        'Dim serie As FondoSerieDTO = New FondoSerieDTO
        'serie.Nemotecnico = certificadoSelect.FS_Nemotecnico
        'Dim listaSerie As List(Of FondoSerieDTO) = Negocio.CompararDatosSeries(serie)

        'If lista.Count > 0 Then
        'For Each fondos As FondoDTO In lista
        'Dim razonSocial = fondos.RazonSocial
        'Dim estadoFondo = fondos.Estado
        'If certificadoSelect.FN_Nombre_Corto <> razonSocial And estadoFondo = 1 Or estadoFondo = 0 Then
        'ShowAlert("No se puede eliminar la ventana seleccionada, información del fondo modificada")
        'DataInitial()
        'Return

        'ElseIf listaSerie.Count > 0 Then
        'For Each recorrer As FondoSerieDTO In listaSerie
        'Dim estadoSerie = recorrer.Estado
        'If estadoSerie = 0 Then
        'ShowAlert("No se puede eliminar la ventana seleccionada, Serie deshabilitada")
        'DataInitial()
        'Exit Sub
        'Return

        'ElseIf razonSocial = certificadoSelect.FN_Nombre_Corto And estadoFondo = 1 And estadoSerie = 1 Then
        txtAccionHidden.Value = "ELIMINAR"
        grvAsignacion.Columns(0).Visible = True
        FormateoEstiloFormEliminar()
        FormateoFormDatos(certificadoSelect)
        ' End If
        'Next
        '    End If
        '   Next
        'Else
        'ShowAlert("No se puede ingresar a la ventana seleccionada, información del fondo modificada")
        'End If


    End Sub

    Private Function GetVentanasRescateSelectEliminar(tabla As GridView) As VentanasRescateDTO
        Dim VentanasRescate As New VentanasRescateDTO
        For Each row As GridViewRow In tabla.Rows
            Dim chk As RadioButton = row.Cells(0).Controls(1)
            If chk IsNot Nothing And chk.Checked Then
                VentanasRescate.FN_Nombre_Corto = HttpUtility.HtmlDecode(row.Cells(1).Text.Trim())
                VentanasRescate.FS_Nemotecnico = Trim(HttpUtility.HtmlDecode(row.Cells(2).Text.Replace("&nbsp;", ""))) ' row.Cells(2).Text.Trim()
                VentanasRescate.RES_Fecha_Solicitud = row.Cells(3).Text.Trim()
                VentanasRescate.VTRES_Fecha_NAV = row.Cells(4).Text.Trim()
                VentanasRescate.VTRES_Fecha_Pago = row.Cells(5).Text.Trim()
            End If
        Next

        Return VentanasRescate
    End Function

    Private Sub FormateoEstiloFormEliminar()
        btnModalGuardar.Enabled = True
        btnModalGuardar.Visible = True
        btnModalAgregar.Enabled = False
        btnModalAgregar.Visible = True
        btnModalModificar.Enabled = False
        btnModalModificar.Visible = True
        btnModalEliminarCertificado.Enabled = False
        btnModalEliminarCertificado.Visible = True
        btnModalEliminarGrupo.Enabled = True
        btnModalEliminarGrupo.Visible = True

        ddlModalNombreFondo.Enabled = False
        ddlModalNemotecnico.Enabled = False
        txtModalFechaSolicitud.Enabled = False
        txtModalFechaNAV.Enabled = False
        txtModalFechaPago.Enabled = False

        lblModalTitle.Text = CONST_TITULO_MODAL_ElIMINAR



    End Sub
#End Region

#Region "ELIMINAR GRUPO CERTIFICADOS POR DOCUMENTO"
    Protected Sub btnModalEliminarGrupo_Click(sender As Object, e As EventArgs) Handles btnModalEliminarGrupo.Click
        Dim negocio As VentanasRescateNegocio = New VentanasRescateNegocio
        Dim VentanasRescate As VentanasRescateDTO = New VentanasRescateDTO
        Dim mensaje As String

        VentanasRescate.FN_Nombre_Corto = ddlModalNombreFondo.SelectedValue
        VentanasRescate.FS_Nemotecnico = ddlModalNemotecnico.SelectedValue

        txtAccionHidden.Value = ""
        If negocio.EliminarTodosVentanasRescate(VentanasRescate) = Constantes.CONST_OPERACION_EXITOSA Then
            mensaje = CONST_EXITO_AL_GUARDAR.Replace("[accion]", "eliminado")
            ShowAlert(CONST_ELIMINAR_EXITO)
        Else
            mensaje = CONST_EXITO_AL_GUARDAR.Replace("[accion]", "eliminado")
            ShowAlert(CONST_ELIMINAR_ERROR)
        End If

        txtAccionHidden.Value = ""
        CargarTodosVentanasRescate()

        Me.GrvTabla.DataSource = Nothing
        GrvTabla.DataBind()
        BtnExportar.Enabled = False
    End Sub
#End Region

#Region "GESTION DE CALENDARIOS"

    Private Sub BtnLimpiarFechaDesde_Click(sender As Object, e As EventArgs) Handles BtnLimpiarFechaDesde.Click
        txtModalFechaSolicitud.Text = Nothing
    End Sub

    Private Sub lnkBtnModalBorrarFechaNAV_Click(sender As Object, e As EventArgs) Handles lnkBtnModalBorrarFechaNAV.Click
        txtModalFechaNAV.Text = Nothing
    End Sub

    Private Sub lnkBtnModalBorrarFechaPago_Click(sender As Object, e As EventArgs) Handles lnkBtnModalBorrarFechaPago.Click
        txtModalFechaPago.Text = Nothing
    End Sub
#End Region

#Region "BOTON EXPORTAR"
    Protected Sub BtnExportar_Click(sender As Object, e As EventArgs) Handles BtnExportar.Click
        Dim VentanasRescate As VentanasRescateDTO = New VentanasRescateDTO()
        Dim negocio As VentanasRescateNegocio = New VentanasRescateNegocio
        Dim mensaje As String

        If ddlNombreFondoBuscar.SelectedValue.Trim() <> Nothing Then
            Dim arrCadena As String() = ddlNombreFondoBuscar.SelectedItem.Text().Split(New Char() {"/"c})

            VentanasRescate.FN_RUT = arrCadena(0).Trim()
        End If

        If ddlNemotecnicoBuscar.SelectedValue.Trim() <> Nothing Then
            VentanasRescate.FS_Nemotecnico = ddlNemotecnicoBuscar.SelectedValue
        End If

        If ddlNombreFondoBuscar.SelectedValue.Trim() = Nothing And ddlNemotecnicoBuscar.SelectedValue.Trim() = Nothing Then
            mensaje = negocio.ExportarAExcelTodos(VentanasRescate)
        Else
            mensaje = negocio.ExportarAExcel(VentanasRescate)
        End If



        If negocio.rutaArchivosExcel IsNot Nothing Then
            Archivo.NavigateUrl = negocio.rutaArchivosExcel
            Archivo.Text = "Bajar Archivo"
        Else
            Archivo.Visible = False
        End If

        txtAccionHidden.Value = "MOSTRAR_DIALOGO"

        ShowMesagges(CONST_TITULO_VENTANASRESCATE, mensaje, Constantes.CONST_RUTA_IMG + Constantes.CONST_IMG_LOGO, Constantes.CONST_RUTA_IMG + Constantes.CONST_IMG_INFO, False)


    End Sub
#End Region

#Region "BOTON CANCELAR"
    Private Sub btnModalCancelar_Click(sender As Object, e As EventArgs) Handles btnModalCancelar.Click
        txtAccionHidden.Value = ""
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

        txtAccionHidden.Value = "SHOW_DIALOG"

        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModalmg", "$('#myModalmg').modal();", True)
    End Sub

    Private Sub btnCerraModal_Click(sender As Object, e As EventArgs) Handles btnCerraModal.Click
        txtAccionHidden.Value = ""
    End Sub



#End Region
    Protected Sub txtModalFechaPago_TextChanged(sender As Object, e As EventArgs) Handles txtModalFechaPago.TextChanged
        Dim Negocio As VentanasRescateNegocio = New VentanasRescateNegocio
        Dim ventanaRescate As VentanasRescateDTO = New VentanasRescateDTO()

        Dim monedaFondo As String
        Dim boolEsHabil As Boolean

        txtModalFechaPago.Text = Request.Form(txtModalFechaPago.UniqueID)

        ventanaRescate.FN_Nombre_Corto = ddlModalNombreFondo.SelectedValue
        ventanaRescate.FS_Nemotecnico = ddlModalNemotecnico.SelectedValue

        monedaFondo = Negocio.TraerMonedaDelFondo(ventanaRescate)
        boolEsHabil = Utiles.esFechaHabil(monedaFondo, txtModalFechaPago.Text)

        If Not boolEsHabil Then
            txtModalFechaPago.Text = ""
            lblMensaje.Text = "El día seleccionado es No Hábil"
            ShowAlert("El día seleccionado es No Hábil")
        Else
            lblMensaje.Text = ""
        End If

    End Sub

    Protected Sub txtModalFechaNAV_TextChanged(sender As Object, e As EventArgs) Handles txtModalFechaNAV.TextChanged
        Dim Negocio As VentanasRescateNegocio = New VentanasRescateNegocio
        txtModalFechaNAV.Text = Request.Form(txtModalFechaNAV.UniqueID)
    End Sub
    Protected Sub txtModalFechaSolicitud_TextChanged(sender As Object, e As EventArgs) Handles txtModalFechaSolicitud.TextChanged
        Dim Negocio As VentanasRescateNegocio = New VentanasRescateNegocio
        txtModalFechaSolicitud.Text = Request.Form(txtModalFechaSolicitud.UniqueID)
    End Sub

    Shared rdChecked As Integer = -1
    Protected Sub rd_CheckedChanged(sender As Object, e As EventArgs)
        'If rdChecked <> -1 Then
        '    Dim rdSelection As RadioButton = TryCast(grvAsignacion.Rows(rdChecked).FindControl("RowSelector"), RadioButton)
        '    rdSelection.Checked = False
        'End If

        'For i As Integer = 0 To grvAsignacion.Rows.Count - 1
        '    Dim rdSelection As RadioButton = TryCast(grvAsignacion.Rows(i).FindControl("RowSelector"), RadioButton)

        '    If rdSelection.Checked Then
        '        rdChecked = i
        '    End If
        'Next

        'txtModalFechaSolicitud.Text = grvAsignacion.Rows(rdChecked).Cells(4).Text
        'txtModalFechaNAV.Text = grvAsignacion.Rows(rdChecked).Cells(5).Text
        'txtModalFechaPago.Text = grvAsignacion.Rows(rdChecked).Cells(6).Text
    End Sub


End Class
