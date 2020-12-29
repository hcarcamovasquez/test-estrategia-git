Imports DTO
Imports Negocio
Imports DBSUtils

Partial Class Presentacion_Mantenedores_ReporteCanjeMaestro
    Inherits System.Web.UI.Page

#Region "CONSTANTES"

    Private ReadOnly Negocio As AportanteNegocio = New AportanteNegocio
    Private ReadOnly NegocioFondo As FondosNegocio = New FondosNegocio
    Private ReadOnly NegocioReporte As Reportenegocio = New Reportenegocio


    Public Const CONST_TITULO_APORTANTE As String = "Aportantes"
    Public Const CONST_TITULO_MODAL_MODIFICAR As String = "Modificar Aportante"
    Public Const CONST_TITULO_MODAL_ElIMINAR As String = "Eliminar Aportante"
    Public Const CONST_TITULO_MODAL_CREAR As String = "Nuevo Aportante"

    Public Const CONST_MODIFICAR_ERROR As String = "Error al modificar los datos del Aportante"
    Public Const CONST_MODIFICAR_EXITO As String = "Aportante modificado con Exito"
    Public Const CONST_ELIMINAR_ERROR As String = "Error al eliminar el Aportante"
    Public Const CONST_ELIMINAR_EXITO As String = "Aportante eliminado con Exito"
    Public Const CONST_ELIMINAR_EXISTE_EN_OTRA_TABLA As String = "Aportante se encuentra relacionado en otra Tabla"
    Public Const CONST_INSERTAR_ERROR As String = "Error al ingresar el Aportante"
    Public Const CONST_INSERTAR_EXITO As String = "Aportante ingresado con Exito"
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
    Public Const CONST_COL_ESTADO As Integer = 9
    Public Const CONST_COL_FECHAINGRESO As Integer = 10

    Public Const CONST_SIN_RESULTADOS As String = "No se obtuvieron resultados de la búsqueda"
    Public Const CONST_ELEGIR_FECHA_CORTE As String = "Debe Elegir una fecha de Proceso"
    Public Const CONST_ELEGIR_FECHA_CANJE As String = "Debe Elegir una fecha de canje"
    Public Const CONST_ELEGIR_FONDO As String = "Debe Elegir un fondo"
    Public Const CONST_INGRESAR_CAMBIO As String = "Debe Ingresar un valor para el tipo de cambio"

    Public Const GRVCANJE_COL_BTNDISTRIBUIR As Integer = 0
    Public Const GRVCANJE_COL_FN_RUT As Integer = 1
    Public Const GRVCANJE_COL_FN_RAZON_SOCIAL As Integer = 3
    Public Const GRVCANJE_COL_PR_DIRECTO_INDIRECTO As Integer = 4
    Public Const GRVCANJE_COL_FS_GRUPO As Integer = 5
    Public Const GRVCANJE_COL_GPA_DESCRIPCION As Integer = 6
    Public Const GRVCANJE_COL_AP_RUT As Integer = 7
    Public Const GRVCANJE_COL_AP_RAZON_SOCIAL As Integer = 8
    Public Const GRVCANJE_COL_FS_NEMOTECNICO As Integer = 9
    Public Const GRVCANJE_COL_FS_MONEDA As Integer = 10
    Public Const GRVCANJE_COL_VCS_VALOR As Integer = 11
    Public Const GRVCANJE_COL_PR_SALDO_CUOTAS As Integer = 12
    Public Const GRVCANJE_COL_PR_MONTO As Integer = 13
    Public Const GRVCANJE_COL_PR_DESCESTADO As Integer = 14
    Public Const GRVCANJE_COL_PCA_VCS_VALOR As Integer = 15



    Public Const GRVCANJE_COL_TC_VALOR As Integer = 16

    Public Const GRVCANJE_COL_PCA_PR_SALDO_CUOTAS As Integer = 17
    Public Const GRVCANJE_COL_PCA_PR_MONTO As Integer = 18
    Public Const GRVCANJE_COL_PCA_PR_DESCESTADO As Integer = 19
    Public Const GRVCANJE_COL_ACCION As Integer = 20
    Public Const GRVCANJE_COL_PCA_PR_SALDO_CUOTAS_UNO As Integer = 21
    Public Const GRVCANJE_COL_PCA_PR_MONTO_UNO As Integer = 22
    Public Const GRVCANJE_COL_CAN_SERIEOPTIMA As Integer = 23
    Public Const GRVCANJE_COL_NAVCUOTAENTRANTE As Integer = 24
    Public Const GRVCANJE_COL_FACTOR As Integer = 25
    Public Const GRVCANJE_COL_CUOTASENTRANTE As Integer = 26
    Public Const GRVCANJE_COL_MONTOENTRANTE As Integer = 27
    Public Const GRVCANJE_COL_MONTOENTRANTECLP As Integer = 28
    Public Const GRVCANJE_COL_DIFERENCIA As Integer = 29
    Public Const GRVCANJE_COL_DIFERENCIACLP As Integer = 30
    Public Const GRVCANJE_COL_FS_MONEDA_UNO As Integer = 31
    Public Const GRVCANJE_COL_OBSERVACION As Integer = 32
    Public Const GRVCANJE_COL_PCA_PR_DESCESTADO_UNO As Integer = 33
    Public Const GRVCANJE_COL_PCA_C_AP_NAC_EXT As Integer = 34
    Public Const GRVCANJE_COL_PCA_C_AP_CALIFICADO As Integer = 35
    Public Const GRVCANJE_COL_PCA_C_AP_REL_MAM As Integer = 36
    Public Const GRVCANJE_COL_PCA_C_AP_LIMITE As Integer = 37
    Public Const GRVCANJE_COL_PCA_C_CERTIFICADO As Integer = 38
    Public Const GRVCANJE_COL_PCA_C_AP_FINAL_I As Integer = 39
    Public Const GRVCANJE_COL_PCA_C_CUOTAS_C As Integer = 40
    Public Const GRVCANJE_COL_PCA_C_CUOTAS_CERTIFICAR As Integer = 41
    Public Const GRVCANJE_COL_PCA_PR_ID As Integer = 42

    ''**************************************************************************************
    '' Grilla de Asignacion 
    ''***************************************************************************************
    Public Const COL_PRD_ID As Integer = 1
    Public Const COL_PR_ID As Integer = 2
    Public Const COL_FS_NEMOTECNICO As Integer = 3
    Public Const COL_ACCION As Integer = 4
    Public Const COL_PRD_CUOTASENTRANTE As Integer = 5
    Public Const COL_PRD_MONTOSALIENTE As Integer = 6
    Public Const COL_PRD_MONTOSALIENTECLP As Integer = 7
    Public Const COL_SERIE_ENTRANTE As Integer = 8
    Public Const COL_PRD_NAVENTRANTE As Integer = 9
    Public Const COL_PRD_FACTOR As Integer = 10
    Public Const COL_PRD_CUOTASSALIENTES As Integer = 11
    Public Const COL_PRD_MONTOENTRANTE As Integer = 12
    Public Const COL_PRD_NAVENTRANTECLP As Integer = 13
    Public Const COL_PRD_MONTOENTRANTECLP As Integer = 14
    Public Const COL_PRD_DIFERENCIA As Integer = 15
    Public Const COL_PRD_DIFERENCIACLP As Integer = 16
    Public Const COL_FS_MONEDAENTRANTE As Integer = 17
    Public Const COL_PRD_OBSERVACIONES As Integer = 18
    Public Const COL_PR_DESCESTADO As Integer = 19
    Public Const COL_C_AP_NAC_EXT As Integer = 20
    Public Const COL_C_AP_CALIFICADO As Integer = 21
    Public Const COL_C_AP_REL_MAM As Integer = 22
    Public Const COL_C_AP_LIMITE As Integer = 23
    Public Const COL_C_CERTIFICADO As Integer = 24
    Public Const COL_C_AP_FINAL_I As Integer = 25
    Public Const COL_C_CUOTAS_C As Integer = 26
    Public Const COL_C_CUOTAS_CERTIFICAR As Integer = 27
    Public Const COL_FILA_ROW_PADRE As Integer = 28
    Public Const COL_CLAVE As Integer = 29
    Public Const COL_NEMO_SELECCIONADO As Integer = 30

    Public Const CONST_AGREGAR_EXISTE_EN_OTRA_GRUPO As String = "Aportante se encuentra en otro Grupo"
    Public Const CONST_AGREGAR_EXISTE_EN_LA_GRILLA As String = "El Aportante ya se encuentra en la lista"

    Public Const CONST_DISTRIBUCION_ERROR_NO_EXISTEN_REGISTROS As String = "No hay registros para realizar la distribucion"
    Public Const CONST_DISTRIBUCION_ERROR_SERIE_REPETIDA As String = "No puede distribuir a una misma serie más de una vez"
    Public Const CONST_DISTRIBUCION_ERROR_SELECCIONAR_ACCION As String = "Debe seleccionar una acción para cada una de las Asignaciones"
    Public Const CONST_DISTRIBUCION_ERROR_EXCEDE_CUOTAS_DISPONIBLES As String = "Excede las cuotas disponibles"
    Private Const CONST_DISTRIBUIR_ERROR_DEBE_DISTRIBUIR_TODAS_DISPONIBLES As String = "Debe distribuir todas las cuotas disponibles"

    Public Const CONST_MANTENER_INGRESADA_EXITO As String = "Acción realizada con exito"
    Public Const CONST_MANTENER_INGRESADA_ERROR As String = "Error al mantener cuotas"
    Public Const CONST_DISTRIBUIR_INGRESADA_EXITO As String = "Distribución ingresada con exito"
    Public Const CONST_DISTRIBUIR_INGRESADA_ERROR As String = "Error al ingresar la distribución"

    Public Const CONST_MANTENER As String = "Mantener"

#End Region

    Private Sub Mantenedores_Aportantes_Maestro_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            DataInitial()
        Else
            TabName.Value = Request.Form(TabName.UniqueID)
        End If

        ValidaPermisosPerfil()
    End Sub

    Private Sub DataInitial()
        txtFechaProceso.Text = ""
        txtFechaCanje.Text = ""

        txtCambio.Text = ""
        CargaFiltroFondo()
        GrvTabla.DataSource = New List(Of ReporteFechaCorteDTO)
        GrvTabla.DataBind()

        GrvFechaCanje.DataSource = New List(Of ReporteFechaCorteDTO)
        GrvFechaCanje.DataBind()

        GrvCanje.DataBind()

        ValidaPermisosPerfil()
    End Sub

    Private Sub CargaFiltroFondo()
        Dim lista As List(Of FondoDTO) = GetFondos()

        If lista.Count = 0 Then
            ddlFondo.Items.Insert(0, New ListItem("", ""))
        Else
            ddlFondo.DataSource = lista
            ddlFondo.DataMember = "RazonSocial"
            ddlFondo.DataValueField = "Rut"
            ddlFondo.DataTextField = "RazonSocial"
            ddlFondo.DataBind()
            ddlFondo.Items.Insert(0, New ListItem("", ""))
        End If
    End Sub

    Private Sub ValidaPermisosPerfil()
        If Session("PERFIL") = Constantes.CONST_PERFIL_CONSULTA Or Session("PERFIL") = Nothing Then

        ElseIf Session("PERFIL") = Constantes.CONST_PERFIL_FULL Or Session("PERFIL") = Constantes.CONST_PERFIL_ADMIN Then

        End If
    End Sub

    Private Sub GrvTabla_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles GrvTabla.PageIndexChanging
    End Sub

    Protected Sub BtnBuscar_Click(sender As Object, e As EventArgs) Handles BtnBuscar.Click
        Dim isAnyItemsSelected As Boolean = False

        For Each li As ListItem In ddlFondo.Items
            If li.Selected Then
                isAnyItemsSelected = True
                Exit For
            End If
        Next

        txtFechaProceso.Text = Request.Form(txtFechaProceso.UniqueID)
        txtFechaCanje.Text = Request.Form(txtFechaCanje.UniqueID)

        If Not isAnyItemsSelected Then
            ShowAlert(CONST_ELEGIR_FONDO)
        Else
            If txtFechaProceso.Text = "" Then
                ShowAlert(CONST_ELEGIR_FECHA_CORTE)
            Else
                If txtFechaCanje.Text = "" Then
                    ShowAlert(CONST_ELEGIR_FECHA_CANJE)
                Else
                    If txtCambio.Text = "" Then
                        ShowAlert(CONST_INGRESAR_CAMBIO)
                        txtCambio.Focus()
                    Else
                        FindReporte()
                        If GrvTabla.Rows.Count = 0 Then
                            ShowAlert(CONST_SIN_RESULTADOS)
                        End If
                    End If
                End If
            End If
        End If

    End Sub

    Private Sub cargaGrillaGrvData(listaReporte As List(Of ReporteFechaCorteDTO))
        GrvTabla.DataSource = listaReporte
        GrvTabla.DataBind()
    End Sub

    Private Sub cargaGrvFechaCanje(listaReporte As List(Of ReporteFechaCorteDTO))
        GrvFechaCanje.DataSource = listaReporte
        GrvFechaCanje.DataBind()
    End Sub

    Private Sub cargaGrvCanje(ListaReporteCanje As List(Of ReporteFechaCorteDTO))
        GrvCanje.DataSource = ListaReporteCanje
        GrvCanje.DataBind()
    End Sub

    Private Sub FindReporte()

        Dim ListaReporte As List(Of ReporteFechaCorteDTO)
        ListaReporte = New List(Of ReporteFechaCorteDTO)()
        Dim listFondo As List(Of FondoDTO) = New List(Of FondoDTO)()

        listFondo = fillFondosSeleccionados()

        ListaReporte = NegocioReporte.GetListaReporte(listFondo, Date.Parse(txtFechaProceso.Text), Date.Parse(txtFechaCanje.Text), txtCambio.Text)

        cargaGrillaGrvData(ListaReporte)
        cargaGrvFechaCanje(ListaReporte)

        cargaGrvCanje(ListaReporte)

    End Sub

    Private Function getSelectedFondos() As String
        Dim selectedValues As String = String.Empty

        For Each li As ListItem In ddlFondo.Items
            If li.Selected Then
                If selectedValues = String.Empty Then
                    selectedValues = li.Value
                Else
                    selectedValues = selectedValues + "," + li.Value
                End If

            End If
        Next
        Return selectedValues
    End Function

    Private Function fillFondosSeleccionados() As List(Of FondoDTO)
        Dim fondo As FondoDTO
        Dim listFondo As List(Of FondoDTO) = New List(Of FondoDTO)()
        Dim strFondos() As String = Split(getSelectedFondos(), ",")

        For Each s As String In strFondos
            fondo = New FondoDTO()
            fondo.Rut = s
            listFondo.Add(fondo)
        Next

        Return listFondo
    End Function

    Protected Sub GrvCanje_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GrvCanje.RowDataBound
        Try


        Catch ex As Exception
            Throw ex

        End Try
    End Sub

    Private Function getFondos(fondoSerie As FondoSerieDTO) As List(Of FondoSerieDTO)
        Dim fondoNegocio As New Negocio.FondoSeriesNegocio
        Dim listaFondoSerie As List(Of FondoSerieDTO) = New List(Of FondoSerieDTO)()
        Return fondoNegocio.GetByRutAgrupacion(fondoSerie)

    End Function

    Public Sub prcCargarComboGridView(ByVal cboCombo As DropDownList, listaFondoSerie As List(Of FondoSerieDTO))
        cboCombo.DataSource = listaFondoSerie
        cboCombo.DataTextField = "nemotecnico"
        cboCombo.DataValueField = "nemotecnico"
        cboCombo.DataBind()
    End Sub

    Protected Sub GrvCanje_RowCommand(sender As Object, e As GridViewCommandEventArgs)

    End Sub

    Protected Sub btnLimpiarFrm_Click(sender As Object, e As EventArgs)
        DataInitial()
    End Sub

    Private Function GetFondos() As List(Of FondoDTO)
        Dim Fondo As New FondoDTO()
        Fondo.Rut = ""
        Return NegocioFondo.GetListaFondosConFiltro(Fondo, Nothing)
    End Function

    Protected Sub btnVolver_Click(sender As Object, e As EventArgs) Handles btnVolver.Click
        Response.Redirect("~/blank.aspx")
    End Sub

    Private Sub ShowAlert(mesagge As String)
        Dim myScript As String = "alert('" + mesagge + "');"
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", myScript, True)
    End Sub

    Private Sub ShowAlertInModal(mesagge As String)
        Dim myScript As String = "alert('" + mesagge + "');"
        ScriptManager.RegisterStartupScript(Page, UpdatePanelGrilla1.GetType(), "alert", myScript, True)
    End Sub

    Protected Sub BtnGuardar_Click(sender As Object, e As EventArgs) Handles BtnGuardar.Click
        'UpdateProcesos()
    End Sub

    Protected Sub btnDistribuir_Click(sender As Object, e As EventArgs)
        Dim procesosDetalle As List(Of ProcesoDetalleDTO) = New List(Of ProcesoDetalleDTO)
        Dim reporteNegocio As Reportenegocio = New Reportenegocio
        Dim proceso As ReporteFechaCorteDTO = New ReporteFechaCorteDTO
        Dim procesoDetalle As ProcesoDetalleDTO

        Dim ddl As Button = CType(sender, Button)
        Dim row As GridViewRow = CType(ddl.Parent.Parent, GridViewRow)
        Dim idx As Integer = row.RowIndex
        Dim prID As Integer = row.Cells(GRVCANJE_COL_PCA_PR_ID).Text

        proceso = reporteNegocio.GetListaProcesoPorId(prID)

        proceso.pca_TC_Valor = txtCambio.Text

        llenarHeaderPopup(row, proceso)

        procesosDetalle = llenarGrillaDetalle(prID)

        If procesosDetalle.Count = 0 Then    ' indica que no tiene distribuciones, saca de la grilla padre los valores a mostrar
            procesoDetalle = fillObjetoGrillaPadre(row, proceso)
            procesoDetalle.PRD_CuotasSalientes = lblCuotasSalientes.Text
            procesosDetalle.Add(procesoDetalle)
        End If

        procesosDetalle = putKey(procesosDetalle)

        grvAsignacion.DataSource = procesosDetalle
        grvAsignacion.DataBind()

        txtAccionHidden.Value = "DISTRIBUIR"

        Session("listaEliminar") = Nothing
        SetSession(procesosDetalle)
    End Sub

    Protected Sub btnMantener_Click(sender As Object, e As EventArgs)
        Dim procesosDetalle As List(Of ProcesoDetalleDTO) = New List(Of ProcesoDetalleDTO)
        Dim dataAGuardar As ProcesoDetalleDTO = New ProcesoDetalleDTO
        Dim reporteNegocio As Reportenegocio = New Reportenegocio
        Dim proceso As ReporteFechaCorteDTO = New ReporteFechaCorteDTO
        Dim DTOproceso As ProcesoDTO = New ProcesoDTO()
        Dim reporteNuevaEvaluacion As ReporteFechaCorteDTO = New ReporteFechaCorteDTO()
        Dim negocioDetalle As ProcesoDetalleNegocio = New ProcesoDetalleNegocio()


        ' revisar 
        Dim btnMantener As Button = CType(sender, Button)
        Dim row As GridViewRow = CType(btnMantener.Parent.Parent, GridViewRow)
        Dim idx As Integer = row.RowIndex

        Dim txtCuotasSalientes As String
        Dim estado As Boolean

        Dim prID As Integer

        txtCuotasSalientes = GrvCanje.Rows(idx).Cells(GRVCANJE_COL_PR_SALDO_CUOTAS).Text

        prID = row.Cells(GRVCANJE_COL_PCA_PR_ID).Text

        proceso = reporteNegocio.GetListaProcesoPorId(prID)    ' busca el proceso de la grilla padre
        dataAGuardar = fillObjetoGrillaPadre(row, proceso)


        DTOproceso.IdProceso = prID
        DTOproceso.ResCuotas = IIf(txtCuotasSalientes.Trim() = "", 0, txtCuotasSalientes)
        DTOproceso.FsNemotecnico = proceso.SerieOptima
        DTOproceso.TcValor = txtCambio.Text

        reporteNuevaEvaluacion = NegocioReporte.GetNuevaEvaluacionHija(DTOproceso)

        If reporteNuevaEvaluacion.pca_PR_DescEstado IsNot Nothing Then
            reporteNuevaEvaluacion.ValorCambio = txtCambio.Text
            reporteNuevaEvaluacion.RES_Cuotas = DTOproceso.ResCuotas
            'reporteNuevaEvaluacion.PRCuotasSalientes = DTOproceso.ResCuotas

            dataAGuardar.C_AP_Nac_Ext = "" ' reporteNuevaEvaluacion.pca_C_AP_Nac_Ext
            dataAGuardar.C_AP_Calificado = "" ' reporteNuevaEvaluacion.pca_C_AP_Calificado
            dataAGuardar.C_AP_Rel_MAM = "" ' reporteNuevaEvaluacion.pca_C_AP_Rel_MAM
            dataAGuardar.C_AP_Limite = "" ' reporteNuevaEvaluacion.pca_C_AP_Limite
            dataAGuardar.C_AP_Final_I = "" ' reporteNuevaEvaluacion.pca_C_AP_Final_I
            dataAGuardar.PR_DescEstado = "" ' reporteNuevaEvaluacion.pca_PR_DescEstado
            dataAGuardar.PRD_NAVEntrante = 0 ' reporteNuevaEvaluacion.NavCuotaEntrante   ' NavCuotaEntrante
            dataAGuardar.PRD_Factor = 0 ' reporteNuevaEvaluacion.PRD_factor          ' Factor
            dataAGuardar.PRD_CuotasEntrante = 0 'reporteNuevaEvaluacion.cuotas_entrantes ' CuotasEntrante
            dataAGuardar.PRD_MontoEntrante = 0 ' reporteNuevaEvaluacion.monto_saliente ' MontoEntrante
            dataAGuardar.PRD_MontoEntranteCLP = 0 'Utiles.formateConSeparadorDeMiles(reporteNuevaEvaluacion.monto_saliente_CLP, 0) ' MontoEntrante
            dataAGuardar.PRD_Diferencia = 0 'reporteNuevaEvaluacion.diferenciaMoneda
            dataAGuardar.PRD_DiferenciaCLP = 0 'reporteNuevaEvaluacion.diferencia_CLP
            dataAGuardar.NemoSeleccionado = "" ' proceso.SerieOptima
            dataAGuardar.PRD_CuotasSalientes = 0 ' reporteNuevaEvaluacion.PRCuotasSalientes

            dataAGuardar.PRD_MontoSaliente = 0 'proceso.PR_Monto
            dataAGuardar.FS_MonedaEntrante = "" ' proceso.FS_Moneda
            dataAGuardar.C_Certificado = ""
            dataAGuardar.C_AP_Final_I = ""
            dataAGuardar.PRD_Accion = CONST_MANTENER

            procesosDetalle.Add(dataAGuardar)

            estado = negocioDetalle.GuardarProcesoDetalle(procesosDetalle)

            FindReporte()

            If estado Then
                ShowAlert(CONST_MANTENER_INGRESADA_EXITO)
            Else
                ShowAlert(CONST_MANTENER_INGRESADA_ERROR)
            End If

        End If

    End Sub

    Private Function putKey(procesosDetalle As List(Of ProcesoDetalleDTO)) As List(Of ProcesoDetalleDTO)
        Dim procesosDetalleSal As List(Of ProcesoDetalleDTO) = New List(Of ProcesoDetalleDTO)
        Dim detalle As ProcesoDetalleDTO

        For i As Integer = 0 To procesosDetalle.Count - 1
            detalle = New ProcesoDetalleDTO
            detalle = procesosDetalle(i)
            detalle.Clave = i
            procesosDetalleSal.Add(detalle)
            ' i += 1
        Next

        Return procesosDetalleSal
    End Function

    Private Sub llenarHeaderPopup(row As GridViewRow, proceso As ReporteFechaCorteDTO)

        txtHiddenGrupo.Value = row.Cells(GRVCANJE_COL_FS_GRUPO).Text()
        lblRutAportante.Text = String.Format("{0}/{1}", proceso.AP_RUT, proceso.AP_Razon_Social)
        lblFondo.Text = String.Format("{0}/{1}", proceso.FN_RUT, proceso.FN_Razon_Social)

        lblNemotecnicoSaliente.Text = proceso.FS_Nemotecnico
        lblCuotasSalientes.Text = String.Format("{0:N0}", Decimal.Parse(proceso.PR_Saldo_Cuotas))
        lblNavSaliente.Text = Utiles.formatearNAV(proceso.VCS_Valor)  '  String.Format("{0:N2}", Decimal.Parse(proceso.VCS_Valor))

        If row.Cells(GRVCANJE_COL_FS_MONEDA).Text = "CLP" Then
            lblNavSalienteCLP.Text = Utiles.formatearNAVCLP(proceso.VCS_Valor) ' String.Format("{0:N2}", Decimal.Parse(proceso.VCS_Valor))
            lblMontoSalienteCLP.Text = Utiles.formatearMontoCLP(proceso.PR_Monto)  ' String.Format("{0:N2}", Decimal.Parse(proceso.PR_Monto))
        Else
            lblNavSalienteCLP.Text = Utiles.calcularNAVCLP(proceso.pca_TC_Valor, proceso.VCS_Valor) ' String.Format("{0:N2}", Decimal.Parse(proceso.VCS_Valor) * Decimal.Parse(proceso.pca_TC_Valor))
            lblMontoSalienteCLP.Text = Utiles.calcularMontoCLP(proceso.PR_Saldo_Cuotas, proceso.VCS_Valor, proceso.pca_TC_Valor)

        End If
        lblFechaCanje.Text = txtFechaCanje.Text

        lblMontoSaliente.Text = Utiles.formatearMonto(proceso.PR_Monto, row.Cells(GRVCANJE_COL_FS_MONEDA).Text)  '  String.Format("{0:N4}", Decimal.Parse(proceso.PR_Monto))

        hidSerieOptima.Value = proceso.SerieOptima
        txtTipoCambio.Text = txtCambio.Text

    End Sub

    Private Function llenarGrillaDetalle(prId As Integer) As List(Of ProcesoDetalleDTO)
        Dim prDetalle As ProcesoDetalleDTO = New ProcesoDetalleDTO
        Dim procesosDetalle As List(Of ProcesoDetalleDTO) = New List(Of ProcesoDetalleDTO)
        Dim negocioDetalle As ProcesoDetalleNegocio = New ProcesoDetalleNegocio()

        prDetalle.PR_ID = prId

        procesosDetalle = negocioDetalle.SelectProcesoDetalle(prDetalle)

        If procesosDetalle Is Nothing Then
            procesosDetalle = New List(Of ProcesoDetalleDTO)
        End If

        Return procesosDetalle
    End Function

    Protected Sub Can_SerieOptima_SelectedIndexChanged(sender As Object, e As EventArgs)

    End Sub

    Protected Sub cmbOpcion_SelectedIndexChanged(sender As Object, e As EventArgs)
        Dim ddl As DropDownList = CType(sender, DropDownList)
        Dim row As GridViewRow = CType(ddl.Parent.Parent, GridViewRow)
        Dim listaProcesosDetalle As New List(Of ProcesoDetalleDTO)
        Dim clave As Integer
        Dim findDetalle As ProcesoDetalleDTO

        clave = row.Cells(COL_CLAVE).Text()
        listaProcesosDetalle = TraerListaDeObjetosEnGrilla()

        findDetalle = listaProcesosDetalle.Find(Function(l) l.Clave = clave)
        If findDetalle IsNot Nothing Then
            Dim posicion As Integer = listaProcesosDetalle.FindIndex(Function(l) l.Clave = clave)
            findDetalle.PRD_Accion = ddl.SelectedValue
            listaProcesosDetalle.RemoveAt(posicion)
            listaProcesosDetalle.Insert(posicion, findDetalle)
        End If


    End Sub

    Protected Sub RowSelectorCambio_SelectedIndexChanged(sender As Object, e As EventArgs)
        Dim ddl As DropDownList = CType(sender, DropDownList)
        Dim row As GridViewRow = CType(ddl.Parent.Parent, GridViewRow)

        If ddl.SelectedValue = "1" Then    ' Mantener
            bloquearControles(row, False)
        Else
            bloquearControles(row, True)
        End If
    End Sub

    'TODO: Get Columnas 

    Private Sub bloquearControles(row As GridViewRow, boolEnable As Boolean)

    End Sub

    Private Sub UpdateProcesos()
        Dim ddl As DropDownList

        For Each row As GridViewRow In GrvCanje.Rows
            ddl = DirectCast(GrvCanje.Rows(row.RowIndex).FindControl("RowSelectorCambio"), DropDownList)
        Next


    End Sub

    Function replaceHtml(texto As String) As String
        texto = Utiles.HtmlDecode(texto)
        texto = Replace(texto, "&nbsp;", "")
        texto = Replace(texto, "nbsp;", "")
        texto = Replace(texto, "&amp;", "")
        texto = Replace(texto, "amp;", "")
        texto = Replace(texto, "&a", "")
        texto = Replace(texto, "&", "")

        Return texto
    End Function

    Protected Sub btnModalGuardar_Click(sender As Object, e As EventArgs) Handles btnModalGuardar.Click
        Dim procesosDetalle As List(Of ProcesoDetalleDTO) = New List(Of ProcesoDetalleDTO)
        Dim negocioDetalle As ProcesoDetalleNegocio = New ProcesoDetalleNegocio()
        Dim estado As Boolean
        Dim cuotasDistribuidas As Decimal = 0
        Dim txtCuotasSalientes As Decimal
        Dim grHija As GridView = New GridView()

        If grvAsignacion.Rows.Count = 0 Then
            ShowAlert(CONST_DISTRIBUCION_ERROR_NO_EXISTEN_REGISTROS)
            Exit Sub
        End If

        txtCuotasSalientes = Utiles.SetNumberPointToDouble(lblCuotasSalientes.Text)

        procesosDetalle = traerDataDeGrilla()

        If verificarDuplicidadNemotecnicos() Then
            ShowAlert(CONST_DISTRIBUCION_ERROR_SERIE_REPETIDA)
        ElseIf verificarAccionSeleccionada() Then
            ShowAlert(CONST_DISTRIBUCION_ERROR_SELECCIONAR_ACCION)
        Else
            cuotasDistribuidas = sumaTotales(procesosDetalle)

            If (cuotasDistribuidas > txtCuotasSalientes) Then
                ShowAlertInModal(CONST_DISTRIBUCION_ERROR_EXCEDE_CUOTAS_DISPONIBLES)
            ElseIf (cuotasDistribuidas < txtCuotasSalientes) Then
                ShowAlertInModal(CONST_DISTRIBUIR_ERROR_DEBE_DISTRIBUIR_TODAS_DISPONIBLES)
            Else
                estado = negocioDetalle.GuardarProcesoDetalle(procesosDetalle)
                If estado Then
                    ShowAlert(CONST_DISTRIBUIR_INGRESADA_EXITO)
                    txtAccionHidden.Value = ""
                Else
                    ShowAlert(CONST_DISTRIBUIR_INGRESADA_ERROR)
                End If

                FindReporte()

            End If
        End If

    End Sub

    Private Function verificarDuplicidadNemotecnicos() As Boolean
        Dim lista As New List(Of ProcesoDetalleDTO)
        For Each row As GridViewRow In grvAsignacion.Rows
            Dim combo As DropDownList = DirectCast(row.FindControl("cmbseriehija"), DropDownList)
            If combo IsNot Nothing AndAlso combo.SelectedIndex > -1 Then
                Dim grupo As New ProcesoDetalleDTO
                grupo.NemoSeleccionado = combo.SelectedValue()
                lista.Add(grupo)
            End If
        Next

        Dim itemsGroupedByID = lista.GroupBy(Function(x) x.NemoSeleccionado)
        Dim duplicateItems = itemsGroupedByID.Where(Function(x) x.Count > 1) _
                                     .SelectMany(Function(x) x) _
                                     .ToList()

        If duplicateItems.Count > 0 Then
            Return True
        End If

        Return False
    End Function

    Private Function verificarAccionSeleccionada() As Boolean
        Dim retorno As Boolean = False
        Dim lista As New List(Of ProcesoDetalleDTO)
        Dim combo As DropDownList
        For Each row As GridViewRow In grvAsignacion.Rows
            combo = DirectCast(row.FindControl("cmbOpcion"), DropDownList)
            If combo IsNot Nothing AndAlso combo.SelectedIndex = 0 Then

                retorno = True
                Exit For
            End If
        Next

        Return retorno
    End Function

    Private Function sumaTotales(lista As List(Of ProcesoDetalleDTO)) As Decimal
        Dim suma As Decimal = lista.Sum(Function(gp) gp.PRD_CuotasSalientes)
        Return suma
    End Function

    Private Function traerDataDeGrilla() As List(Of ProcesoDetalleDTO)
        Dim procesosDetalle As List(Of ProcesoDetalleDTO) = New List(Of ProcesoDetalleDTO)
        Dim proceso As ProcesoDetalleDTO = New ProcesoDetalleDTO

        For Each row As GridViewRow In grvAsignacion.Rows
            proceso = New ProcesoDetalleDTO()

            proceso = fillObjetoGrilla(row)
            procesosDetalle.Add(proceso)

        Next

        Return procesosDetalle
    End Function

    Private Function fillObjetoGrilla(row As GridViewRow) As ProcesoDetalleDTO
        Dim proceso As ProcesoDetalleDTO = New ProcesoDetalleDTO()
        Dim txtCuotasSalientes As TextBox
        Dim txtObservacion As TextBox
        Dim ddlMoneda As DropDownList
        Dim ddlAccion As DropDownList
        Dim ddlNemotecnico As DropDownList

        With grvAsignacion
            txtCuotasSalientes = DirectCast(.Rows(row.RowIndex).FindControl("txtPRDCuotasSaliente"), TextBox)
            txtObservacion = DirectCast(.Rows(row.RowIndex).FindControl("txtObservacion"), TextBox)
            ddlMoneda = DirectCast(.Rows(row.RowIndex).FindControl("cmbMonedaDetalle"), DropDownList)
            ddlAccion = DirectCast(.Rows(row.RowIndex).FindControl("cmbOpcion"), DropDownList)
            ddlNemotecnico = DirectCast(.Rows(row.RowIndex).FindControl("cmbSerieHija"), DropDownList)

            proceso.PRD_ID = .Rows(row.RowIndex).Cells(COL_PRD_ID).Text
            proceso.PRD_Accion = ddlAccion.SelectedItem.Text()
            proceso.PR_ID = .Rows(row.RowIndex).Cells(COL_PR_ID).Text
            proceso.FS_Nemotecnico = ddlNemotecnico.Text   ' .Rows(row.RowIndex).Cells(COL_FS_NEMOTECNICO).Text
            proceso.PRD_CuotasEntrante = .Rows(row.RowIndex).Cells(COL_PRD_CUOTASSALIENTES).Text

            proceso.PRD_NAVEntrante = .Rows(row.RowIndex).Cells(COL_PRD_NAVENTRANTE).Text
            proceso.PRD_NAVEntranteCLP = .Rows(row.RowIndex).Cells(COL_PRD_NAVENTRANTECLP).Text
            proceso.PRD_MontoEntrante = .Rows(row.RowIndex).Cells(COL_PRD_MONTOENTRANTE).Text
            proceso.PRD_MontoEntranteCLP = .Rows(row.RowIndex).Cells(COL_PRD_MONTOENTRANTECLP).Text
            proceso.PRD_Factor = .Rows(row.RowIndex).Cells(COL_PRD_FACTOR).Text
            proceso.PRD_CuotasSalientes = txtCuotasSalientes.Text
            proceso.PRD_MontoSaliente = .Rows(row.RowIndex).Cells(COL_PRD_MONTOSALIENTE).Text
            proceso.PRD_MontoSalienteCLP = .Rows(row.RowIndex).Cells(COL_PRD_MONTOSALIENTECLP).Text
            proceso.PRD_Diferencia = .Rows(row.RowIndex).Cells(COL_PRD_DIFERENCIA).Text
            proceso.PRD_DiferenciaCLP = .Rows(row.RowIndex).Cells(COL_PRD_DIFERENCIACLP).Text
            proceso.FS_MonedaEntrante = ddlMoneda.SelectedValue
            proceso.PRD_Observaciones = Utiles.HtmlDecode(txtObservacion.Text)
            proceso.PR_DescEstado = Utiles.HtmlDecode(.Rows(row.RowIndex).Cells(COL_PR_DESCESTADO).Text)
            proceso.C_AP_Nac_Ext = Utiles.HtmlDecode(.Rows(row.RowIndex).Cells(COL_C_AP_NAC_EXT).Text)
            proceso.C_AP_Calificado = Utiles.HtmlDecode(.Rows(row.RowIndex).Cells(COL_C_AP_CALIFICADO).Text)
            proceso.C_AP_Rel_MAM = Utiles.HtmlDecode(.Rows(row.RowIndex).Cells(COL_C_AP_REL_MAM).Text)
            proceso.C_AP_Limite = Utiles.HtmlDecode(.Rows(row.RowIndex).Cells(COL_C_AP_LIMITE).Text)
            proceso.C_Certificado = Utiles.HtmlDecode(.Rows(row.RowIndex).Cells(COL_C_CERTIFICADO).Text)
            proceso.C_AP_Final_I = Utiles.HtmlDecode(.Rows(row.RowIndex).Cells(COL_C_AP_FINAL_I).Text)
            proceso.C_Cuotas_C = Utiles.HtmlDecode(.Rows(row.RowIndex).Cells(COL_C_CUOTAS_C).Text)
            proceso.C_Cuotas_Certificar = Utiles.HtmlDecode(.Rows(row.RowIndex).Cells(COL_C_CUOTAS_CERTIFICAR).Text)
            proceso.NemoSeleccionado = Utiles.HtmlDecode(.Rows(row.RowIndex).Cells(COL_NEMO_SELECCIONADO).Text)

        End With

        Return proceso
    End Function

    Private Function fillObjetoGrillaPadre(row As GridViewRow, procesoPadre As ReporteFechaCorteDTO) As ProcesoDetalleDTO
        Dim proceso As ProcesoDetalleDTO = New ProcesoDetalleDTO
        Dim rowCanje As GridViewRow = GrvFechaCanje.Rows(row.RowIndex)
        Dim lblSerieOPtima As Label = DirectCast(row.FindControl("lblSerieOptima"), Label)

        proceso.PRD_ID = 0
        proceso.PR_ID = row.Cells(GRVCANJE_COL_PCA_PR_ID).Text
        proceso.PRD_CuotasEntrante = row.Cells(GRVCANJE_COL_PCA_PR_SALDO_CUOTAS).Text
        proceso.PRD_MontoEntrante = procesoPadre.PR_Monto
        proceso.PRD_NAVEntrante = row.Cells(GRVCANJE_COL_VCS_VALOR).Text
        proceso.FS_Nemotecnico = row.Cells(GRVCANJE_COL_FS_NEMOTECNICO).Text
        proceso.PRD_Observaciones = ""
        proceso.NemoSeleccionado = lblSerieOPtima.Text

        proceso.filaRowPadre = row.RowIndex

        ' Factor = NavCuotaEntrante / VCS_Valor 
        Return proceso
    End Function

    Protected Sub grvasignacion_rowdatabound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grvAsignacion.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                'dim fondonegocio as new negocio.fondoseriesnegocio
                Dim fondoserie As FondoSerieDTO = New FondoSerieDTO()
                Dim listafondoserie As List(Of FondoSerieDTO) = New List(Of FondoSerieDTO)()

                Dim combo As DropDownList = DirectCast(e.Row.FindControl("cmbseriehija"), DropDownList)
                Dim cmbAccion As DropDownList = DirectCast(e.Row.FindControl("cmbseriehija"), DropDownList)
                Dim cmbMonedaDetalle As DropDownList = DirectCast(e.Row.FindControl("cmbMonedaDetalle"), DropDownList)

                Dim rowcorte As GridViewRow = GrvCanje.Rows(e.Row.RowIndex)
                Dim rowcanje As GridViewRow = GrvCanje.Rows(e.Row.RowIndex)
                Dim arrCadena As String() = lblFondo.Text.Split(New Char() {"/"c})


                Dim strrut As String = arrCadena(0).Trim()                                ' rowcanje.Cells(GRVCANJE_COL_FN_RUT).Text()
                Dim strnivel As String = txtHiddenGrupo.Value
                Dim serieoptima As String
                Dim serieSeleccionado As String = Utiles.HtmlDecode(e.Row.Cells(COL_NEMO_SELECCIONADO).Text())
                Dim SerieDeLaGrilla As String = Utiles.HtmlDecode(e.Row.Cells(COL_NEMO_SELECCIONADO).Text())

                If (serieSeleccionado.Trim() <> "") Then
                    serieoptima = serieSeleccionado
                Else
                    serieoptima = hidSerieOptima.Value
                End If

                combo.ClearSelection()

                If combo IsNot DBNull.Value Then
                    fondoserie.Rut = strrut.Trim()
                    If (strnivel.ToLower().Trim() = "suscripciones") Then
                        fondoserie.Nivel = -1
                    Else
                        fondoserie.Nivel = IIf(strnivel = "sin grupo", Nothing, strnivel.Substring(strnivel.IndexOf(" ")).Trim())
                    End If

                    fondoserie.Nemotecnico = rowcanje.Cells(GRVCANJE_COL_FS_NEMOTECNICO).Text()

                    listafondoserie = getFondos(fondoserie)

                    Me.prcCargarComboGridView(combo, listafondoserie)

                    Dim item As ListItem = combo.Items.FindByText(serieoptima)

                    If item IsNot Nothing Then
                        combo.SelectedValue = serieoptima
                    End If

                End If

            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Protected Sub cmbSerieHija_SelectedIndexChanged(sender As Object, e As EventArgs)
        Dim proceso As ProcesoDTO = New ProcesoDTO()
        Dim reporteNuevaEvaluacion As ReporteFechaCorteDTO = New ReporteFechaCorteDTO()
        Dim ddl As DropDownList = CType(sender, DropDownList)
        Dim txtCuotasSalientes As TextBox
        Dim row As GridViewRow = CType(ddl.Parent.Parent, GridViewRow)
        Dim idx As Integer = row.RowIndex
        Dim Serie As String = ddl.Text()
        Dim FechaCanje As String = txtFechaCanje.Text
        Dim idProceso As Integer = row.Cells(COL_PR_ID).Text()      ' Id proceso
        Dim txtObservacion As TextBox = DirectCast(grvAsignacion.Rows(idx).FindControl("txtObservacion"), TextBox)
        Dim ddlMoneda As DropDownList = DirectCast(grvAsignacion.Rows(idx).FindControl("cmbMonedaDetalle"), DropDownList)
        Dim monedaSelected = ddlMoneda.Text
        Dim serie2 As String = ddl.SelectedValue

        txtCuotasSalientes = DirectCast(grvAsignacion.Rows(idx).FindControl("txtPRDCuotasSaliente"), TextBox)

        proceso.IdProceso = idProceso
        proceso.ResCuotas = IIf(txtCuotasSalientes.Text.Trim() = "", 0, txtCuotasSalientes.Text)
        proceso.FsNemotecnico = Serie
        proceso.TcValor = txtCambio.Text

        reporteNuevaEvaluacion = NegocioReporte.GetNuevaEvaluacionHija(proceso)

        If reporteNuevaEvaluacion.pca_PR_DescEstado IsNot Nothing Then
            reporteNuevaEvaluacion.ValorCambio = txtCambio.Text
            reporteNuevaEvaluacion.RES_Cuotas = proceso.ResCuotas
            reporteNuevaEvaluacion.Observacion = txtObservacion.Text
            'reporteNuevaEvaluacion.PRCuotasSalientes = proceso.ResCuotas
            putObjectInGrid(row, reporteNuevaEvaluacion, Serie, idx, monedaSelected)
        End If

    End Sub

    Protected Sub cmbMonedaDetalle_SelectedIndexChanged(sender As Object, e As EventArgs)
        Dim cmbMonedaDetalle As DropDownList = CType(sender, DropDownList)
        Dim row As GridViewRow = CType(cmbMonedaDetalle.Parent.Parent, GridViewRow)
        Dim listaProcesosDetalle As New List(Of ProcesoDetalleDTO)

        Dim clave As Integer
        Dim findDetalle As ProcesoDetalleDTO

        clave = row.Cells(COL_CLAVE).Text()
        listaProcesosDetalle = TraerListaDeObjetosEnGrilla()

        findDetalle = listaProcesosDetalle.Find(Function(l) l.Clave = clave)
        If findDetalle IsNot Nothing Then
            Dim posicion As Integer = listaProcesosDetalle.FindIndex(Function(l) l.Clave = clave)
            findDetalle.FS_MonedaEntrante = cmbMonedaDetalle.Text
            listaProcesosDetalle.RemoveAt(posicion)
            listaProcesosDetalle.Insert(posicion, findDetalle)
        End If
    End Sub

    Protected Sub txtObservacion_SelectedIndexChanged(sender As Object, e As EventArgs)
        Dim txtObservacion As TextBox = CType(sender, TextBox)
        Dim row As GridViewRow = CType(txtObservacion.Parent.Parent, GridViewRow)
        Dim idx As Integer = row.RowIndex

        Dim listaProcesosDetalle As New List(Of ProcesoDetalleDTO)

        Dim clave As Integer
        Dim findDetalle As ProcesoDetalleDTO

        clave = row.Cells(COL_CLAVE).Text()
        listaProcesosDetalle = TraerListaDeObjetosEnGrilla()

        findDetalle = listaProcesosDetalle.Find(Function(l) l.Clave = clave)
        If findDetalle IsNot Nothing Then
            Dim posicion As Integer = listaProcesosDetalle.FindIndex(Function(l) l.Clave = clave)
            findDetalle.PRD_Observaciones = txtObservacion.Text
            listaProcesosDetalle.RemoveAt(posicion)
            listaProcesosDetalle.Insert(posicion, findDetalle)
        End If
    End Sub

    Private Sub putObjectInGrid(row As GridViewRow, reporteNuevaEvaluacion As ReporteFechaCorteDTO, serie As String, idx As Integer, monedaSelected As String)
        Dim detalle As ProcesoDetalleDTO = New ProcesoDetalleDTO
        Dim listaProcesosDetalle As New List(Of ProcesoDetalleDTO)
        Dim clave As Integer = row.Cells(COL_CLAVE).Text()

        listaProcesosDetalle = TraerListaDeObjetosEnGrilla()

        detalle.Clave = clave
        Dim findDetalle As ProcesoDetalleDTO = listaProcesosDetalle.Find(Function(l) l.Clave = detalle.Clave)

        If findDetalle IsNot Nothing Then
            Dim posicion As Integer = listaProcesosDetalle.FindIndex(Function(l) l.Clave = detalle.Clave)

            findDetalle.C_AP_Nac_Ext = reporteNuevaEvaluacion.pca_C_AP_Nac_Ext
            findDetalle.C_AP_Calificado = reporteNuevaEvaluacion.pca_C_AP_Calificado
            findDetalle.C_AP_Rel_MAM = reporteNuevaEvaluacion.pca_C_AP_Rel_MAM
            findDetalle.C_AP_Limite = reporteNuevaEvaluacion.pca_C_AP_Limite
            findDetalle.C_AP_Final_I = reporteNuevaEvaluacion.pca_C_AP_Final_I
            findDetalle.PR_DescEstado = reporteNuevaEvaluacion.pca_PR_DescEstado

            findDetalle.PRD_MontoSaliente = Utiles.formatearMonto(reporteNuevaEvaluacion.monto_saliente, monedaSelected)
            findDetalle.PRD_CuotasSalientes = Utiles.SetToCapitalizedNumber(reporteNuevaEvaluacion.PRCuotasSalientes)
            findDetalle.PRD_MontoSalienteCLP = Utiles.formatearMontoCLP(reporteNuevaEvaluacion.monto_saliente_CLP)

            findDetalle.PRD_NAVEntrante = Utiles.formatearNAV(reporteNuevaEvaluacion.NavCuotaEntrante)
            findDetalle.PRD_NAVEntranteCLP = Utiles.formatearNAVCLP(reporteNuevaEvaluacion.NavCuotaEntranteCLP)
            findDetalle.PRD_MontoEntrante = Utiles.formatearMonto(reporteNuevaEvaluacion.monto_entrante, monedaSelected)
            findDetalle.PRD_CuotasEntrante = reporteNuevaEvaluacion.cuotas_entrantes
            findDetalle.PRD_MontoEntranteCLP = Utiles.formatearMontoCLP(reporteNuevaEvaluacion.monto_entrante_CLP)

            findDetalle.PRD_Factor = reporteNuevaEvaluacion.PRD_factor
            findDetalle.PRD_Diferencia = Utiles.formatearDiferencia(reporteNuevaEvaluacion.diferenciaMoneda, monedaSelected)
            findDetalle.PRD_DiferenciaCLP = Utiles.formatearDiferenciaCLP(reporteNuevaEvaluacion.diferencia_CLP)
            findDetalle.NemoSeleccionado = serie

            findDetalle.FS_MonedaEntrante = monedaSelected
            findDetalle.PRD_Observaciones = reporteNuevaEvaluacion.Observacion

            listaProcesosDetalle.RemoveAt(posicion)
            listaProcesosDetalle.Insert(posicion, findDetalle)

            grvAsignacion.DataSource = listaProcesosDetalle
            grvAsignacion.DataBind()

            SetSession(listaProcesosDetalle)
        End If
    End Sub

    Private Sub btnAddAsignacion_Click(sender As Object, e As EventArgs) Handles btnAddAsignacion.Click
        AgregarElementoGridViewAsignacion()
    End Sub

    Private Sub btnEliminarAsignacion_Click(sender As Object, e As EventArgs) Handles btnEliminarAsignacion.Click
        Dim aXgAnterior As ProcesoDetalleDTO = GetAportanteXGrupoSelect(grvAsignacion)
        EliminarElementoGridViewAsignacion(aXgAnterior)
    End Sub

    Private Function GetAportanteXGrupoSelect(tabla As GridView) As ProcesoDetalleDTO
        Dim grupo As New ProcesoDetalleDTO
        For Each row As GridViewRow In tabla.Rows
            Dim chk As RadioButton = row.Cells(0).Controls(1)
            If chk IsNot Nothing And chk.Checked Then
                grupo.PR_ID = row.Cells(COL_PR_ID).Text.Trim()
                grupo.PRD_ID = Utiles.HtmlDecode(row.Cells(COL_PRD_ID).Text.Trim())
                grupo.Clave = row.Cells(COL_CLAVE).Text
            End If
        Next

        Return grupo
    End Function

    Private Function TraerListaDeObjetosEnGrilla() As List(Of ProcesoDetalleDTO)
        Dim listaProcesosDetalle As New List(Of ProcesoDetalleDTO)

        If Session("listaDistribucion") IsNot Nothing Then
            listaProcesosDetalle = Session("listaDistribucion")
        Else
            listaProcesosDetalle = New List(Of ProcesoDetalleDTO)
        End If

        Return listaProcesosDetalle
    End Function

    Private Sub SetSession(listaProcesosDetalle As List(Of ProcesoDetalleDTO))
        Session("listaDistribucion") = listaProcesosDetalle
    End Sub

    Private Function AgregarElementoGridViewAsignacion() As Boolean
        Dim resultadoAgregar As Boolean = False
        Dim listaProcesosDetalle As New List(Of ProcesoDetalleDTO)
        Dim detalle As New ProcesoDetalleDTO

        listaProcesosDetalle = TraerListaDeObjetosEnGrilla()

        detalle = AgregarfillObjetoGrillaPadre(listaProcesosDetalle)

        Dim cuotasDistribuidas As Decimal = 0
        Dim txtCuotasSalientes As Decimal
        Dim procesosDetalle As List(Of ProcesoDetalleDTO) = New List(Of ProcesoDetalleDTO)

        cuotasDistribuidas = sumaTotales(traerDataDeGrilla())

        txtCuotasSalientes = Utiles.SetNumberPointToDouble(lblCuotasSalientes.Text)

        If (cuotasDistribuidas > txtCuotasSalientes) Then
            detalle.PRD_CuotasSalientes = 0
        Else
            detalle.PRD_CuotasSalientes = txtCuotasSalientes - cuotasDistribuidas
        End If


        Dim existeEnGrilla As Boolean = ExisteEnGrillaAsignacion(listaProcesosDetalle, detalle)

        listaProcesosDetalle.Add(detalle)

        listaProcesosDetalle = putKey(listaProcesosDetalle)

        'cambia el nombre por todos los elementos de la grilla
        SetSession(listaProcesosDetalle)

        grvAsignacion.DataSource = listaProcesosDetalle
        grvAsignacion.DataBind()

        resultadoAgregar = True
        btnModalGuardar.Enabled = True

        Return resultadoAgregar
    End Function

    Private Function AgregarfillObjetoGrillaPadre(listaProcesosDetalle As List(Of ProcesoDetalleDTO)) As ProcesoDetalleDTO
        Dim detalle As ProcesoDetalleDTO = New ProcesoDetalleDTO
        Dim cantidad As Integer
        cantidad = listaProcesosDetalle.Count - 1

        For Each detalleDTO As ProcesoDetalleDTO In listaProcesosDetalle
            detalle.PRD_ID = detalleDTO.PRD_ID
            detalle.C_AP_Calificado = detalleDTO.C_AP_Calificado
            detalle.C_AP_Final_I = detalleDTO.C_AP_Final_I
            detalle.C_AP_Limite = detalleDTO.C_AP_Limite
            detalle.C_AP_Nac_Ext = detalleDTO.C_AP_Nac_Ext
            detalle.C_AP_Rel_MAM = detalleDTO.C_AP_Rel_MAM
            detalle.C_Certificado = detalleDTO.C_Certificado
            detalle.C_Cuotas_C = detalleDTO.C_Cuotas_C
            detalle.C_Cuotas_Certificar = detalleDTO.C_Cuotas_Certificar
            detalle.filaRowPadre = detalleDTO.filaRowPadre
            detalle.FS_MonedaEntrante = detalleDTO.FS_MonedaEntrante
            detalle.FS_Nemotecnico = detalleDTO.FS_Nemotecnico
            detalle.PRD_Accion = detalleDTO.PRD_Accion
            detalle.PRD_CuotasEntrante = detalleDTO.PRD_CuotasEntrante
            detalle.PRD_CuotasSalientes = detalleDTO.PRD_CuotasSalientes
            detalle.PRD_Diferencia = detalleDTO.PRD_Diferencia
            detalle.PRD_DiferenciaCLP = detalleDTO.PRD_DiferenciaCLP
            detalle.PRD_Factor = detalleDTO.PRD_Factor
            detalle.PRD_MontoEntrante = detalleDTO.PRD_MontoEntrante
            detalle.PRD_MontoEntranteCLP = detalleDTO.PRD_MontoEntranteCLP
            detalle.PRD_MontoSaliente = detalleDTO.PRD_MontoSaliente
            detalle.PRD_NAVEntrante = detalleDTO.PRD_NAVEntrante
            detalle.PRD_Observaciones = detalleDTO.PRD_Observaciones
            detalle.PR_DescEstado = detalleDTO.PR_DescEstado
            detalle.PR_ID = detalleDTO.PR_ID
        Next

        Return detalle
    End Function

    Private Function ExisteEnGrillaAsignacion(lista As List(Of ProcesoDetalleDTO), detalle As ProcesoDetalleDTO) As Boolean
        Dim findAportanteXGrupo As ProcesoDetalleDTO = lista.Find(Function(l) l.Clave = detalle.Clave)
        Return (findAportanteXGrupo IsNot Nothing)
    End Function


    Private Sub EliminarElementoGridViewAsignacion(aXgAnterior As ProcesoDetalleDTO)
        Dim listaEliminar As New List(Of ProcesoDetalleDTO)
        Dim listaProcesosDetalle As New List(Of ProcesoDetalleDTO)

        If Session("listaEliminar") IsNot Nothing Then
            listaEliminar = Session("listaEliminar")
        End If

        listaProcesosDetalle = TraerListaDeObjetosEnGrilla()

        Dim lista As List(Of ProcesoDetalleDTO) = listaProcesosDetalle

        Dim objetoEliminar = lista.FirstOrDefault(Function(t) t.Clave = aXgAnterior.Clave)

        If objetoEliminar IsNot Nothing Then
            lista.Remove(objetoEliminar)
            listaEliminar.Add(objetoEliminar)
        End If

        putKey(lista)

        Session("listaEliminar") = listaEliminar
        SetSession(lista)

        grvAsignacion.DataSource = lista
        grvAsignacion.DataBind()
    End Sub

    Protected Sub txtPRDCuotasSaliente_textChanged(sender As Object, e As EventArgs)
        Dim proceso As ProcesoDTO = New ProcesoDTO()
        Dim reporteNuevaEvaluacion As ReporteFechaCorteDTO = New ReporteFechaCorteDTO()

        Dim txtCuotasSalientes As TextBox = CType(sender, TextBox)
        Dim row As GridViewRow = CType(txtCuotasSalientes.Parent.Parent, GridViewRow)
        Dim idx As Integer = row.RowIndex
        Dim ddlSerie As DropDownList = DirectCast(grvAsignacion.Rows(idx).FindControl("cmbSerieHija"), DropDownList)
        Dim txtObservacion As TextBox = DirectCast(grvAsignacion.Rows(idx).FindControl("txtObservacion"), TextBox)
        Dim ddlMoneda As DropDownList = DirectCast(grvAsignacion.Rows(idx).FindControl("cmbMonedaDetalle"), DropDownList)
        Dim monedaSelected = ddlMoneda.Text

        Dim Serie As String = ddlSerie.Text()
        Dim FechaCanje As String = txtFechaCanje.Text
        Dim idProceso As Integer = row.Cells(COL_PR_ID).Text()      ' Id proceso

        proceso.IdProceso = idProceso
        proceso.ResCuotas = IIf(txtCuotasSalientes.Text.Trim() = "", 0, txtCuotasSalientes.Text)
        proceso.FsNemotecnico = Serie
        proceso.TcValor = txtCambio.Text


        reporteNuevaEvaluacion = NegocioReporte.GetNuevaEvaluacionHija(proceso)

        If reporteNuevaEvaluacion.pca_PR_DescEstado IsNot Nothing Then
            reporteNuevaEvaluacion.ValorCambio = txtCambio.Text
            reporteNuevaEvaluacion.RES_Cuotas = proceso.ResCuotas
            reporteNuevaEvaluacion.Observacion = txtObservacion.Text

            putObjectInGrid(row, reporteNuevaEvaluacion, Serie, idx, monedaSelected)
        End If

    End Sub

End Class

