Imports DTO
Imports Negocio
Imports DBSUtils

Partial Class Presentacion_Mantenedores_frmFijacion_Maestro
    Inherits System.Web.UI.Page

#Region "Constantes"
    Public Const CONST_TITULO_FIJACION As String = "Fijación"
    Public Const CONST_TITULO_MODAL_MODIFICAR As String = "Modificar Fijación"
    Public Const CONST_MODIFICAR_ERROR As String = "Error al modificar los datos de la Fijación"
    Public Const CONST_MODIFICAR_EXITO As String = "Fijación modificada con éxito"
    Public Const CONST_ELIMINAR_ERROR As String = "Error al eliminar la fijacion seleccionada"
    Public Const CONST_ELIMINAR_EXITO As String = "Fijación eliminada con éxito"
    Public Const CONST_ELIMINAR_ESTADO_CERO As String = "No se puede eliminar un registro ya deshabilitado"
    Public Const CONST_ELIMINAR_EXISTE_EN_OTRA_TABLA As String = "Fijación se encuentra relacionada en otra Tabla"
    Public Const CONST_TITULO_MODAL_ELIMINAR As String = "Eliminar Fijación"
    Public Const CONST_TITULO_MODAL_CREAR As String = "Nueva Fijación"
    Public Const CONST_NEMOTECNICO_EXISTE As String = "El Nemotécnico ya existe"
    Public Const CONST_ERROR_AL_GUARDAR As String = "Error al guardar la Fijación"
    Public Const CONST_EXITO_AL_GUARDAR As String = "Fijación guardada con éxito"
    Public Const CONST_SIN_RESULTADOS As String = "No se obtuvieron resultados de la búsqueda"
    Public Const CONST_MAXIMO As String = "EL Monto Máximo debe ser mayor o igual que el Monto Mínimo"
    Public Const CONST_MODIFICAR_TC_EXITO As String = "Éxito al fijar TC observado. "
    Public Const CONST_MODIFICAR_TC_ERROR As String = "Error al fijar TC observado. "
    Public Const CONST_MODIFICAR_NAV_EXITO As String = "Éxito al fijar NAV. "
    Public Const CONST_MODIFICAR_NAV_ERROR As String = "Error al fijar NAV. "
    Public Const CONST_SIN_MODIFICACION As String = "No se realizó ninguna modificación"

    Public Const CONST_COL_ID As Integer = 1
    Public Const CONST_COL_TIPOTRANSACCION As Integer = 2
    Public Const CONST_COL_APRUT As Integer = 3
    Public Const CONST_COL_RAZONSOCIAL As Integer = 4
    Public Const CONST_COL_RUT As Integer = 5
    Public Const CONST_COL_FNNOMBRECORTO As Integer = 6
    Public Const CONST_COL_FECHANAV As Integer = 7
    Public Const CONST_COL_FECHATCOBS As Integer = 8

    Public Const CONST_COL_FECHAPAGO As Integer = 9
    Public Const CONST_COL_NAV_FIJADO As Integer = 10
    Public Const CONST_COL_TC_OBSERVADO As Integer = 11
    Public Const CONST_COL_ESTADOINTENCION As Integer = 12

    Public Const CONST_COL_FIJACIONNAV As Integer = 13
    Public Const CONST_COL_FIJACIONTCOBSERVADO As Integer = 14
    Public Const CONST_COL_NEMOTECNICO As Integer = 15

    Public Const CONST_COL_MONEDA_PAGO As Integer = 16
    Public Const CONST_COL_CUOTAS As Integer = 17
    Public Const CONST_COL_MONTO As Integer = 18

    Private Const CONST_LLENAR_CAMPOS As String = "Debe llenar todos los campos"
    Private Const CONST_NO_HAY_TRANSACCIONES_SELECCIONADAS As String = "No hay transacciones seleccionadas"


#End Region
    Dim NegocioSuscripcion As SuscripcionNegocio = New SuscripcionNegocio
    Dim NegocioRescate As RescateNegocio = New RescateNegocio
    Dim NegocioCanje As CanjeNegocio = New CanjeNegocio
    Dim Negocio As FijacionNegocio = New FijacionNegocio



#Region "DATA INICIAL"
    Private Sub Presentacion_Mantenedores_frmFijacion_Maestro_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            DataInitial()
        End If
        ValidaPermisosPerfil()
    End Sub

    Private Sub ValidaPermisosPerfil()
        HiddenPerfil.Value = Session("PERFIL")
        HiddenConstante.Value = Constantes.CONST_PERFIL_CONSULTA

        If Session("PERFIL") = Constantes.CONST_PERFIL_CONSULTA Then
            BtnModificar.Enabled = False
            BtnExportar.Enabled = False
            btnImprimir.Enabled = False
            BtnEliminar.Enabled = False

        ElseIf Session("PERFIL") = Constantes.CONST_PERFIL_FULL Or Session("PERFIL") = Constantes.CONST_PERFIL_ADMIN Then
            BtnModificar.Visible = True
            BtnModificar.Enabled = False
            BtnExportar.Visible = True
            btnImprimir.Enabled = False
            BtnEliminar.Enabled = False
        End If
    End Sub

    Private Sub CargaDatosModalInicial()
        CargaFiltroRutAportante()
        CargarTipoTransaccion()
        CargaFiltroRutFondo()
        CargaNemotecnico()
        CargaFijacionNav()
        CargaFijacionTC()
        CargaFiltroNombreAportante()
    End Sub

    Private Sub DataInitial()
        CargaDatosModalInicial()
        txtFechaIntencion.Text = Date.Now.ToString("dd/MM/yyyy")
        Utiles.CargarMonedas(ddlMonedaPago, "")

        ddlContrato.Items.Insert(0, "")
        ddlContrato.Items.Insert(1, "OK")
        ddlContrato.Items.Insert(2, "NO OK")
        ddlPoderes.Items.Insert(0, "")
        ddlPoderes.Items.Insert(1, "OK")
        ddlPoderes.Items.Insert(2, "NO OK")
        ddlEstado1.Items.Insert(0, "Pendiente")
        ddlEstado1.Items.Insert(1, "Cerrado")
        ddlEstado1.Items.Insert(0, New ListItem("", ""))
        GrvTabla.DataSource = New List(Of AportantesXGrupoDTO)
        GrvTabla.DataBind()
        BtnExportar.Enabled = (GrvTabla.Rows.Count <> 0)

    End Sub

#End Region

#Region "Click Botones"

#Region "Botones modal"
#End Region
    Private Sub btnModalCancelar_Click(sender As Object, e As EventArgs) Handles btnModalCancelar.Click
        txtAccionHidden.Value = ""
    End Sub

    Protected Sub btnLimpiarFrm_Click(sender As Object, e As EventArgs)
        txtAccionHidden.Value = ""
        FormateoLimpiarForm()
        Me.GrvTabla.DataSource = Nothing
        GrvTabla.DataBind()
    End Sub

    Protected Sub BtnFijarNav_Click(sender As Object, e As EventArgs) Handles BtnFijarNav.Click
        Dim TransaccionesTotales As Integer = 0
        Dim TransaccionesExitosas As Integer = 0
        Dim TransaccionesNoExitosas As Integer = 0

        Dim negocioMod As FijacionNegocio = New FijacionNegocio
        Dim Fijacion As New FijacionDTO
        Dim negocioSeries As FondoSeriesNegocio = New FondoSeriesNegocio
        Dim Series As New FondoSerieDTO
        Dim Fondo As New FondoDTO
        Dim SerieActualizado As FondoSerieDTO
        Dim TipoFijacionNAV As String
        Dim CantidadFijados As Integer = 0
        Dim CantidadNOFijados As Integer = 0
        Dim Mensaje As String = ""

        Dim TransaccionSeleccionada As Integer
        Dim FechaNAVSeleccionada As Date
        Dim NemotecnicoSeleccionado As String
        Dim FondoRUTSeleccionado As String
        Dim TipoTransaccionSeleccionado As String
        Dim ValorNAVSeleccionado As Decimal
        Dim Fijado As String

        Dim ListaErrores As List(Of TErroresFijacion)

        ListaErrores = CargarListaErroresSoportados()

        Dim listaMensajes As List(Of String) = New List(Of String)

        For Each row As GridViewRow In GrvTabla.Rows
            Dim chk As CheckBox = row.Cells(0).Controls(1)

            If chk IsNot Nothing And chk.Checked Then
                Fijacion.ID = row.Cells(CONST_COL_ID).Text().Trim()
                Fijacion.TipoTransaccion = row.Cells(CONST_COL_TIPOTRANSACCION).Text().Trim()

                TransaccionSeleccionada = row.Cells(CONST_COL_ID).Text().Trim()
                TipoTransaccionSeleccionado = row.Cells(CONST_COL_TIPOTRANSACCION).Text().Trim()
                FechaNAVSeleccionada = row.Cells(CONST_COL_FECHANAV).Text().Trim()
                FechaNAVSeleccionada = FechaNAVSeleccionada.ToShortDateString()
                NemotecnicoSeleccionado = row.Cells(CONST_COL_NEMOTECNICO).Text().Trim()
                FondoRUTSeleccionado = row.Cells(CONST_COL_RUT).Text().Trim()
                Fijado = row.Cells(CONST_COL_FIJACIONNAV).Text().Trim()

                If (Fijado = "Realizado") Then
                    ListaErrores = AgregarError(ListaErrores, TipoErroresFijacion.EF_ERROR_TRANSACCION_YA_FIJADA, NemotecnicoSeleccionado)
                    CantidadNOFijados = CantidadNOFijados + 1
                    Continue For
                End If

                Series.Nemotecnico = NemotecnicoSeleccionado
                Series.Rut = FondoRUTSeleccionado
                SerieActualizado = negocioSeries.GetFondosSeries(Series)

                If (SerieActualizado IsNot Nothing) Then
                    Select Case Fijacion.TipoTransaccion.ToLower()
                        Case "canje"
                            TipoFijacionNAV = SerieActualizado.FijacionCanje
                        Case "suscripcion"
                            TipoFijacionNAV = SerieActualizado.FijacionSuscripcion
                        Case "rescate"
                            TipoFijacionNAV = SerieActualizado.FijacionNav
                        Case Else
                            TipoFijacionNAV = Nothing
                    End Select
                Else
                    TipoFijacionNAV = Nothing
                End If

                If (TipoFijacionNAV IsNot Nothing) Then
                    If TipoFijacionNAV = "Automático" Then
                        Dim NegocioVC As ValoresCuotaNegocio = New ValoresCuotaNegocio
                        Dim ValoresCuota As VcSerieDTO = New VcSerieDTO()
                        Dim ValoresCuotaSeleccionado As VcSerieDTO = New VcSerieDTO()

                        Dim NegocioRescates As RescateNegocio = New RescateNegocio
                        Dim Rescates As RescatesDTO = New RescatesDTO()
                        Dim RescateSeleccionado As RescatesDTO = New RescatesDTO()
                        Dim RescateActualizar As RescatesDTO = New RescatesDTO()

                        Dim NegocioSuscripcion As SuscripcionNegocio = New SuscripcionNegocio
                        Dim Suscripcion As SuscripcionDTO = New SuscripcionDTO()
                        Dim SuscripcionSeleccionada As SuscripcionDTO = New SuscripcionDTO()
                        Dim SuscripcionActualizar As SuscripcionDTO = New SuscripcionDTO()

                        Dim CuotasSeleccionado As Decimal
                        Dim MonedaSerieSeleccionado As String
                        Dim ValorNAV_CLPSeleccionado As Decimal
                        Dim ValorTcObsSeleccionado As Decimal

                        Dim ValorNAVActualizado As Decimal
                        Dim ValorNAV_CLPActualizado As Decimal
                        Dim ValorMontoActualizado As Decimal
                        Dim ValorMontoCLPActualizado As Decimal

                        ValoresCuota.FnRut = FondoRUTSeleccionado
                        ValoresCuota.FsNemotecnico = NemotecnicoSeleccionado
                        ValoresCuota.Fecha = FechaNAVSeleccionada
                        ValoresCuotaSeleccionado = NegocioVC.GetValoresCuota(ValoresCuota)

                        If ValoresCuotaSeleccionado IsNot Nothing Then
                            'Trae el valor NAV, Fija y Recalcula 
                            If TipoTransaccionSeleccionado = "Rescate" Then
                                ValorNAVSeleccionado = ValoresCuotaSeleccionado.Valor
                                'Trae los campos necesarios para recalcular
                                Rescates.RES_ID = TransaccionSeleccionada
                                RescateSeleccionado = NegocioRescates.GetRescateOne(Rescates)

                                If RescateSeleccionado IsNot Nothing Then
                                    CuotasSeleccionado = RescateSeleccionado.RES_Cuotas
                                    MonedaSerieSeleccionado = RescateSeleccionado.FS_Moneda
                                    ValorNAV_CLPSeleccionado = RescateSeleccionado.RES_Nav_CLP
                                    ValorTcObsSeleccionado = RescateSeleccionado.TC_Valor


                                    Dim Relacion As RescatesDTO = NegocioRescate.GetRelaciones(RescateSeleccionado)

                                    If (Relacion.CountAP > 0) Then
                                        txtAccionHidden.Value = ""
                                        Mensaje = Mensaje & " No se pudo modificar el rescate " & TransaccionSeleccionada & " El aportante fue modificado."
                                        CantidadNOFijados = CantidadNOFijados + 1
                                    ElseIf (Relacion.CountFN > 0) Then
                                        txtAccionHidden.Value = ""
                                        Mensaje = Mensaje & " No se pudo modificar el rescate " & TransaccionSeleccionada & " El fondo fue modificado."
                                        CantidadNOFijados = CantidadNOFijados + 1
                                    ElseIf (Relacion.CountFS > 0) Then
                                        txtAccionHidden.Value = ""
                                        Mensaje = Mensaje & " No se pudo modificar el rescate " & TransaccionSeleccionada & " La serie fue modificada"
                                        CantidadNOFijados = CantidadNOFijados + 1
                                    Else

                                        'SI EXCEDE; GENERAR ADVERTENCIA
                                        If ControlValidacionPatrimonioAutomatico(RescateSeleccionado) Then
                                            listaMensajes.Add("Los rescates no cumplen con el maximo del patrimonio")
                                        End If

                                        If MonedaSerieSeleccionado = "CLP" Then
                                            'Recalcula y Manda a Actualizar cuando Moneda es CLP
                                            ValorNAVActualizado = ValorNAVSeleccionado
                                            ValorNAV_CLPActualizado = ValorNAVSeleccionado
                                            ValorMontoActualizado = (CuotasSeleccionado) * (ValorNAVActualizado)
                                            ValorMontoCLPActualizado = (CuotasSeleccionado) * (ValorNAV_CLPActualizado)

                                            RescateActualizar.RES_ID = TransaccionSeleccionada
                                            RescateActualizar.RES_Nav = ValorNAVActualizado
                                            RescateActualizar.RES_Nav_CLP = ValorNAV_CLPActualizado
                                            RescateActualizar.RES_Monto = ValorMontoActualizado
                                            RescateActualizar.RES_Monto_CLP = ValorMontoCLPActualizado

                                            CantidadFijados = CantidadFijados + 1

                                            ListaErrores = AgregarError(ListaErrores, TipoErroresFijacion.EF_TRANSACCIONES_FIJADAS_EXITOSAMENTE)

                                            NegocioRescates.RecalculoFijacionNAV(RescateActualizar)
                                            negocioMod.UpdateFijacionNav(Fijacion.ID, Fijacion.TipoTransaccion)
                                        Else
                                            'Recalcula y Manda a Actualizar cuando Moneda es Diferente a CLP
                                            ValorNAVActualizado = ValorNAVSeleccionado
                                            ValorNAV_CLPActualizado = (ValorNAVSeleccionado) * (ValorTcObsSeleccionado)
                                            ValorMontoActualizado = (CuotasSeleccionado) * (ValorNAVActualizado)
                                            ValorMontoCLPActualizado = (CuotasSeleccionado) * (ValorNAV_CLPActualizado)

                                            RescateActualizar.RES_ID = TransaccionSeleccionada
                                            RescateActualizar.RES_Nav = ValorNAVActualizado
                                            RescateActualizar.RES_Nav_CLP = ValorNAV_CLPActualizado
                                            RescateActualizar.RES_Monto = ValorMontoActualizado
                                            RescateActualizar.RES_Monto_CLP = ValorMontoCLPActualizado

                                            CantidadFijados = CantidadFijados + 1

                                            ListaErrores = AgregarError(ListaErrores, TipoErroresFijacion.EF_TRANSACCIONES_FIJADAS_EXITOSAMENTE)

                                            NegocioRescates.RecalculoFijacionNAV(RescateActualizar)
                                            negocioMod.UpdateFijacionNav(Fijacion.ID, Fijacion.TipoTransaccion)
                                        End If

                                    End If
                                End If

                            ElseIf TipoTransaccionSeleccionado = "Canje" Then

                                Dim valorSaliente As VcSerieDTO = New VcSerieDTO
                                valorSaliente.Fecha = FechaNAVSeleccionada
                                valorSaliente.FnRut = FondoRUTSeleccionado
                                valorSaliente.FsNemotecnico = NemotecnicoSeleccionado
                                Dim valorNavSaliente = NegocioVC.GetValoresCuota(valorSaliente)

                                Dim canje As CanjeDTO = New CanjeDTO()
                                canje.IdCanje = TransaccionSeleccionada

                                Dim canjeSeleccionado As CanjeDTO = NegocioCanje.GetCanje(canje)
                                Dim valorEntrante As VcSerieDTO = New VcSerieDTO
                                valorEntrante.Fecha = canjeSeleccionado.FechaNavEntrante
                                valorEntrante.FnRut = canjeSeleccionado.RutFondo
                                valorEntrante.FsNemotecnico = canjeSeleccionado.NemotecnicoEntrante.Trim()

                                Dim valorNavEntrante = NegocioVC.GetValoresCuota(valorEntrante)

                                If valorNavSaliente Is Nothing Or valorNavEntrante Is Nothing Then
                                    canjeSeleccionado.FijacionNav = "Pendiente"
                                    ListaErrores = AgregarError(ListaErrores, TipoErroresFijacion.EF_ERROR_TRANSACCION_YA_FIJADA)
                                    CantidadNOFijados = CantidadNOFijados + 1
                                Else

                                    Dim aportante As AportanteDTO = New AportanteDTO()
                                    aportante.Rut = canjeSeleccionado.RutAportante
                                    If canjeSeleccionado.Multifondo = "&nbsp;" Then
                                        aportante.Multifondo = String.Empty
                                    Else
                                        aportante.Multifondo = canjeSeleccionado.Multifondo
                                    End If
                                    Dim listaAportante As List(Of AportanteDTO) = NegocioCanje.CompararDatosAportantes(aportante)

                                    Dim serieEntrante As FondoSerieDTO = New FondoSerieDTO()
                                    serieEntrante.Rut = canjeSeleccionado.RutFondo
                                    serieEntrante.Nemotecnico = canjeSeleccionado.NemotecnicoEntrante
                                    Dim listaEntrantes As List(Of FondoSerieDTO) = NegocioCanje.CompararDatosEntrantes(serieEntrante)

                                    Dim Ex As String = ""
                                    Dim serieSaliente As FondoSerieDTO = New FondoSerieDTO()
                                    serieSaliente.Rut = canjeSeleccionado.RutFondo
                                    serieSaliente.Nemotecnico = canjeSeleccionado.NemotecnicoSaliente
                                    Dim listaSaliente As List(Of FondoSerieDTO) = NegocioCanje.CompararDatosSalientes(serieSaliente)

                                    If listaAportante.Count > 0 Then
                                        For Each aportantes As AportanteDTO In listaAportante
                                            Dim razonSocial = aportantes.RazonSocial
                                            Dim estado = aportantes.Estado

                                            If canjeSeleccionado.NombreAportante <> razonSocial And estado = 1 Or estado = 0 Then
                                                Mensaje = Mensaje & " No se puede modificar el canje, información del Aportante se modifico"
                                                Ex = "x"
                                            ElseIf listaEntrantes.Count > 0 Then
                                                For Each entrante As FondoSerieDTO In listaEntrantes
                                                    Dim serie = entrante.Nombrecorto
                                                    Dim moneda = entrante.Moneda
                                                    Dim estadoEntrante = entrante.Estado
                                                    If canjeSeleccionado.NombreSerieEntrante <> serie Or canjeSeleccionado.MonedaEntrante <> moneda And estadoEntrante = 1 Or estadoEntrante = 0 Then
                                                        Mensaje = Mensaje & (" No se puede modificar el canje, Serie entrante modificada")
                                                        Ex = "x"
                                                    ElseIf listaSaliente.Count > 0 Then
                                                        For Each saliente As FondoSerieDTO In listaSaliente
                                                            Dim serieSaliente2 = saliente.Nombrecorto
                                                            Dim monedaSaliente = saliente.Moneda
                                                            Dim estadoSaliente = saliente.Estado
                                                            If canjeSeleccionado.NombreSerieSaliente <> serieSaliente2 Or canjeSeleccionado.MonedaSaliente <> monedaSaliente And estadoSaliente = 1 Or estadoSaliente = 0 Then
                                                                Mensaje = Mensaje & (" No se puede modificar el canje, Serie saliente modificada ")
                                                                Ex = "x"
                                                            End If
                                                        Next
                                                    End If
                                                Next
                                            End If
                                        Next
                                    End If

                                    If (canjeSeleccionado IsNot Nothing And Ex <> "x") Then
                                        Dim cuotaSaliente As Double
                                        Dim navEntranteUSD As Double
                                        Dim navEntranteCLP As Double
                                        Dim montoEntranteUSD As Double
                                        Dim montoEntranteCLP As Double
                                        Dim diferencias As Double
                                        Dim diferenciasCLP As Double
                                        Dim factor As Double
                                        Dim cuotaEntrante As Double

                                        Dim navSalienteUSD As Double
                                        Dim navSalienteCLP As Double

                                        Dim montoSalienteUSD As Double
                                        Dim montoSalienteCLP As Double

                                        Dim navEntranteCLPConDecimales As Double
                                        Dim navSalienteCLPConDecimales As Double

                                        cuotaSaliente = canjeSeleccionado.CuotaSaliente

                                        navEntranteUSD = valorNavEntrante.Valor
                                        navEntranteCLP = Utiles.calcularNAVCLP(canjeSeleccionado.TipoCambio, navEntranteUSD)

                                        navSalienteUSD = valorNavSaliente.Valor '     valorNavSaliente.Valor
                                        navSalienteCLP = Utiles.calcularNAVCLP(canjeSeleccionado.TipoCambio, navSalienteUSD)  '     canjeSeleccionado.NavCLPSaliente  '     valorNavSaliente.Valor

                                        navSalienteCLPConDecimales = canjeSeleccionado.TipoCambio * navSalienteUSD
                                        navEntranteCLPConDecimales = canjeSeleccionado.TipoCambio * navEntranteUSD

                                        If navEntranteCLPConDecimales = 0 Then
                                            factor = 0
                                        Else
                                            factor = navSalienteCLPConDecimales / navEntranteCLPConDecimales
                                        End If


                                        cuotaEntrante = Math.Truncate(factor * cuotaSaliente)

                                        montoEntranteUSD = cuotaEntrante * navEntranteUSD
                                        montoEntranteCLP = Utiles.calcularMontoCLP(cuotaEntrante, navEntranteUSD, canjeSeleccionado.TipoCambio)

                                        montoSalienteUSD = cuotaSaliente * navSalienteUSD
                                        montoSalienteCLP = Utiles.calcularMontoCLP(cuotaSaliente, navSalienteUSD, canjeSeleccionado.TipoCambio)

                                        diferencias = montoSalienteUSD - montoEntranteUSD
                                        diferenciasCLP = montoSalienteCLP - montoEntranteCLP

                                        canjeSeleccionado.IdCanje = TransaccionSeleccionada
                                        canjeSeleccionado.CuotaSaliente = cuotaSaliente
                                        canjeSeleccionado.CuotaEntrante = cuotaEntrante
                                        canjeSeleccionado.NavEntrante = navEntranteUSD
                                        canjeSeleccionado.Factor = factor
                                        canjeSeleccionado.NavSaliente = navSalienteUSD
                                        canjeSeleccionado.NavCLPEntrante = navEntranteCLP
                                        canjeSeleccionado.NavCLPSaliente = navSalienteCLP
                                        canjeSeleccionado.Diferencia = diferencias
                                        canjeSeleccionado.DiferenciaCLP = diferenciasCLP
                                        canjeSeleccionado.MontoEntrante = montoEntranteUSD
                                        canjeSeleccionado.MontoCLPEntrante = montoEntranteCLP
                                        canjeSeleccionado.MontoSaliente = montoSalienteUSD
                                        canjeSeleccionado.MontoCLPSaliente = montoSalienteCLP
                                        canjeSeleccionado.FijacionNav = "Realizado"
                                        NegocioCanje.UpdateCanje(canjeSeleccionado)
                                        negocioMod.UpdateFijacionNav(Fijacion.ID, Fijacion.TipoTransaccion)

                                        ListaErrores = AgregarError(ListaErrores, TipoErroresFijacion.EF_TRANSACCIONES_FIJADAS_EXITOSAMENTE)

                                        CantidadFijados = CantidadFijados + 1
                                    Else
                                        ListaErrores = AgregarError(ListaErrores, TipoErroresFijacion.EF_ERROR_INTERNO)
                                        CantidadNOFijados = CantidadNOFijados + 1

                                    End If
                                End If
                            ElseIf TipoTransaccionSeleccionado = "Suscripcion" Then
                                ValorNAVSeleccionado = ValoresCuotaSeleccionado.Valor
                                'Trae los campos necesarios para recalcular
                                Suscripcion.IdSuscripcion = TransaccionSeleccionada
                                SuscripcionSeleccionada = NegocioSuscripcion.GetSuscripcion(Suscripcion)

                                If SuscripcionSeleccionada IsNot Nothing Then

                                    Dim Relacion As SuscripcionDTO = NegocioSuscripcion.GetRelaciones(SuscripcionSeleccionada)
                                    If (Relacion.CountAP > 0) Then
                                        txtAccionHidden.Value = ""
                                        Mensaje = Mensaje & " No se pudo modificar la suscripción " & TransaccionSeleccionada & " El aportante fue modificado."
                                        CantidadNOFijados = CantidadNOFijados + 1
                                    ElseIf (Relacion.CountFN > 0) Then
                                        txtAccionHidden.Value = ""
                                        Mensaje = Mensaje & " No se pudo modificar la suscripción " & TransaccionSeleccionada & " El fondo fue modificado."
                                        CantidadNOFijados = CantidadNOFijados + 1
                                    ElseIf (Relacion.CountFS > 0) Then
                                        txtAccionHidden.Value = ""
                                        Mensaje = Mensaje & " No se pudo modificar la suscripción " & TransaccionSeleccionada & " La serie fue modificada"
                                        CantidadNOFijados = CantidadNOFijados + 1
                                        ' JOVB: R3 
                                    ElseIf Not EsConfirmada(SuscripcionSeleccionada) Then
                                        txtAccionHidden.Value = ""
                                        Mensaje = Mensaje & " No se pudo fijar la suscripción " & TransaccionSeleccionada & ". Transacción en Intención"
                                        CantidadNOFijados = CantidadNOFijados + 1

                                    Else

                                        CuotasSeleccionado = SuscripcionSeleccionada.CuotasASuscribir
                                        MonedaSerieSeleccionado = SuscripcionSeleccionada.MonedaSerie
                                        ValorNAV_CLPSeleccionado = SuscripcionSeleccionada.NAVCLP
                                        If (SuscripcionSeleccionada.TcObservado.Contains(".")) Then
                                            ValorTcObsSeleccionado = (SuscripcionSeleccionada.TcObservado.Substring("0", SuscripcionSeleccionada.TcObservado.IndexOf(".")))
                                        Else
                                            ValorTcObsSeleccionado = (SuscripcionSeleccionada.TcObservado)
                                        End If

                                        If MonedaSerieSeleccionado = "CLP" Then
                                            'Recalcula y Manda a Actualizar cuando Moneda es CLP
                                            ValorNAVActualizado = ValorNAVSeleccionado
                                            ValorNAV_CLPActualizado = ValorNAVSeleccionado
                                            ValorMontoActualizado = (CuotasSeleccionado) * (ValorNAVActualizado)
                                            ValorMontoCLPActualizado = (CuotasSeleccionado) * (ValorNAV_CLPActualizado)

                                        Else
                                            'Recalcula y Manda a Actualizar cuando Moneda es Diferente a CLP
                                            ValorNAVActualizado = ValorNAVSeleccionado
                                            ValorNAV_CLPActualizado = (ValorNAVSeleccionado) * (ValorTcObsSeleccionado)
                                            ValorMontoActualizado = (CuotasSeleccionado) * (ValorNAVActualizado)
                                            ValorMontoCLPActualizado = (CuotasSeleccionado) * (ValorNAV_CLPActualizado)

                                        End If

                                        SuscripcionActualizar.IdSuscripcion = TransaccionSeleccionada
                                        SuscripcionActualizar.NAV = ValorNAVActualizado
                                        SuscripcionActualizar.NAVCLP = ValorNAV_CLPActualizado
                                        SuscripcionActualizar.Monto = ValorMontoActualizado
                                        SuscripcionActualizar.MontoCLP = ValorMontoCLPActualizado

                                        CantidadFijados = CantidadFijados + 1
                                        NegocioSuscripcion.RecalculoFijacionNAV(SuscripcionActualizar)
                                        negocioMod.UpdateFijacionNav(Fijacion.ID, Fijacion.TipoTransaccion)

                                        ListaErrores = AgregarError(ListaErrores, TipoErroresFijacion.EF_TRANSACCIONES_FIJADAS_EXITOSAMENTE)
                                    End If
                                Else
                                    ListaErrores = AgregarError(ListaErrores, TipoErroresFijacion.EF_ERROR_INTERNO)
                                    CantidadNOFijados = CantidadNOFijados + 1
                                End If
                            End If
                        Else
                            'NO Realiza la fijacion
                            ListaErrores = AgregarError(ListaErrores, TipoErroresFijacion.EF_ERROR_NO_EXISTE_NAV_O_TC, NemotecnicoSeleccionado)
                            CantidadNOFijados = CantidadNOFijados + 1
                        End If

                    ElseIf TipoFijacionNAV = "Manual" Then
                        'NO Realiza la fijacion
                        ListaErrores = AgregarError(ListaErrores, TipoErroresFijacion.EF_CONFIGURACION_MANUAL, NemotecnicoSeleccionado)
                        CantidadNOFijados = CantidadNOFijados + 1
                    Else
                        'NO Realiza la fijacion la transaccion no ES MANUAL ni AUTOMATICA
                        ListaErrores = AgregarError(ListaErrores, TipoErroresFijacion.EF_ERROR_SERIES_CONFIGURACION, NemotecnicoSeleccionado)
                        CantidadNOFijados = CantidadNOFijados + 1
                    End If
                Else
                    ListaErrores = AgregarError(ListaErrores, TipoErroresFijacion.EF_ERROR_INTERNO)
                    CantidadNOFijados = CantidadNOFijados + 1
                End If
            End If

        Next

        MostrarMensaje(ListaErrores, TransaccionesTotales, TransaccionesExitosas, TransaccionesNoExitosas, CantidadFijados, CantidadNOFijados, Mensaje, listaMensajes)

        txtAccionHidden.Value = ""
        'FormateoLimpiarForm()
        Me.GrvTabla.DataSource = Nothing
        GrvTabla.DataBind()
        FindFijacion()

    End Sub

    Private Function EsConfirmada(suscripcionSeleccionada As SuscripcionDTO) As Boolean
        'If (suscripcionSeleccionada.EstadoIntencion.Equals("Intencion")) Then
        '    Return False
        'Else
        '    Return True
        'End If
        Return True
    End Function

    Protected Sub BtnTCObs_Click(sender As Object, e As EventArgs) Handles BtnTCObs.Click
        Dim TransaccionesTotales As Integer = 0
        Dim TransaccionesExitosas As Integer = 0
        Dim TransaccionesNoExitosas As Integer = 0

        Dim negocioMod As FijacionNegocio = New FijacionNegocio
        Dim Fijacion As New FijacionDTO
        Dim negocioSeries As FondoSeriesNegocio = New FondoSeriesNegocio
        Dim Series As New FondoSerieDTO
        Dim Fondo As New FondoDTO

        Dim CantidadFijados As Integer = 0
        Dim CantidadNOFijados As Integer = 0
        Dim Mensaje As String = ""

        Dim TransaccionSeleccionada As Integer
        Dim FechaTCObsSeleccionada As Date
        Dim TipoTransaccionSeleccionado As String
        Dim Fijado As String
        Dim nemotecnicoSel As String

        Dim listaMensajes As List(Of String) = New List(Of String)
        'Dim ValorTCObsSeleccionado As Decimal

        Dim ListaErrores As List(Of TErroresFijacion)
        ListaErrores = CargarListaErroresSoportados()

        For Each row As GridViewRow In GrvTabla.Rows
            Dim chk As CheckBox = row.Cells(0).Controls(1)
            If chk IsNot Nothing And chk.Checked Then
                Fijacion.ID = row.Cells(CONST_COL_ID).Text().Trim()
                Fijacion.TipoTransaccion = row.Cells(CONST_COL_TIPOTRANSACCION).Text().Trim()

                TransaccionSeleccionada = row.Cells(CONST_COL_ID).Text().Trim()
                TipoTransaccionSeleccionado = row.Cells(CONST_COL_TIPOTRANSACCION).Text().Trim()
                FechaTCObsSeleccionada = row.Cells(CONST_COL_FECHATCOBS).Text().Trim()
                FechaTCObsSeleccionada = FechaTCObsSeleccionada.ToShortDateString()
                Fijado = row.Cells(CONST_COL_FIJACIONTCOBSERVADO).Text().Trim()

                nemotecnicoSel = row.Cells(CONST_COL_NEMOTECNICO).Text().Trim()

                'Trae valor TC de Tipo Cambio
                Dim NegocioTC As TipoCambioNegocio = New TipoCambioNegocio
                Dim TipoCambio As TipoCambioDTO = New TipoCambioDTO()
                Dim TipoCambioSeleccionado As TipoCambioDTO = New TipoCambioDTO()

                Dim NegocioRescates As RescateNegocio = New RescateNegocio
                Dim Rescates As RescatesDTO = New RescatesDTO()
                Dim RescateSeleccionado As RescatesDTO = New RescatesDTO()
                Dim RescateActualizar As RescatesDTO = New RescatesDTO()

                Dim NegocioSuscripcion As SuscripcionNegocio = New SuscripcionNegocio
                Dim Suscripcion As SuscripcionDTO = New SuscripcionDTO()
                Dim SuscripcionSeleccionada As SuscripcionDTO = New SuscripcionDTO()
                Dim SuscripcionActualizar As SuscripcionDTO = New SuscripcionDTO()

                Dim CuotasSeleccionado As Decimal
                Dim MonedaSerieSeleccionado As String
                Dim ValorNAVSeleccionado As Decimal
                Dim ValorNAV_CLPSeleccionado As Decimal
                Dim ValorTcObsSeleccionado As Decimal

                Dim ValorNAVActualizado As Decimal
                Dim ValorNAV_CLPActualizado As Decimal
                Dim ValorMontoActualizado As Decimal
                Dim ValorMontoCLPActualizado As Decimal

                'Trae los campos necesarios para recalcular
                If (Fijado = "Realizado") Then
                    ListaErrores = AgregarError(ListaErrores, TipoErroresFijacion.EF_ERROR_TRANSACCION_YA_FIJADA)
                    CantidadNOFijados = CantidadNOFijados + 1
                    Continue For
                End If

                If TipoTransaccionSeleccionado = "Rescate" Then
                    Rescates.RES_ID = TransaccionSeleccionada
                    RescateSeleccionado = NegocioRescates.GetRescateOne(Rescates)

                    If RescateSeleccionado IsNot Nothing Then
                        TipoCambio.Codigo = RescateSeleccionado.FS_Moneda

                        Dim Relacion As RescatesDTO = NegocioRescate.GetRelaciones(RescateSeleccionado)

                        If (Relacion.CountAP > 0) Then
                            txtAccionHidden.Value = ""
                            Mensaje = Mensaje & " No se pudo modificar el rescate " & TransaccionSeleccionada & " El aportante fue modificado."
                            CantidadNOFijados = CantidadNOFijados + 1
                        ElseIf (Relacion.CountFN > 0) Then
                            txtAccionHidden.Value = ""
                            Mensaje = Mensaje & " No se pudo modificar el rescate " & TransaccionSeleccionada & " El fondo fue modificado."
                            CantidadNOFijados = CantidadNOFijados + 1
                        ElseIf (Relacion.CountFS > 0) Then
                            txtAccionHidden.Value = ""
                            Mensaje = Mensaje & " No se pudo modificar el rescate " & TransaccionSeleccionada & " La serie fue modificada"
                            CantidadNOFijados = CantidadNOFijados + 1
                        Else
                            TipoCambio.Fecha = FechaTCObsSeleccionada

                            TipoCambioSeleccionado = NegocioTC.GetTipoCambio(TipoCambio)

                            'Trae el valor TC, Fija y Recalcula 
                            If TipoCambioSeleccionado IsNot Nothing Then
                                ValorTcObsSeleccionado = TipoCambioSeleccionado.Valor


                                CuotasSeleccionado = RescateSeleccionado.RES_Cuotas
                                MonedaSerieSeleccionado = RescateSeleccionado.FS_Moneda
                                ValorNAVSeleccionado = RescateSeleccionado.RES_Nav
                                ValorNAV_CLPSeleccionado = RescateSeleccionado.RES_Nav_CLP
                                RescateSeleccionado.TC_Valor = ValorTcObsSeleccionado

                                'SI EXCEDE PATRIMONIO GENERAR ALERTA
                                If ControlValidacionPatrimonioAutomatico(RescateSeleccionado) Then
                                    listaMensajes.Add("Los rescates no cumplen con el maximo del patrimonio")
                                End If

                                If MonedaSerieSeleccionado = "CLP" Then
                                    CantidadFijados = CantidadFijados + 1
                                    NegocioRescate.RecalculoFijacionTC(RescateSeleccionado)
                                    negocioMod.UpdateFijacionTC(Fijacion.ID, Fijacion.TipoTransaccion)

                                    ListaErrores = AgregarError(ListaErrores, TipoErroresFijacion.EF_TRANSACCIONES_FIJADAS_EXITOSAMENTE)

                                Else
                                    'Recalcula y Manda a Actualizar cuando Moneda es Diferente a CLP
                                    ValorNAVActualizado = ValorNAVSeleccionado
                                    ValorNAV_CLPActualizado = (ValorNAVSeleccionado) * (ValorTcObsSeleccionado)
                                    ValorMontoActualizado = (CuotasSeleccionado) * (ValorNAVActualizado)
                                    ValorMontoCLPActualizado = (CuotasSeleccionado) * (ValorNAV_CLPActualizado)

                                    RescateActualizar.RES_ID = TransaccionSeleccionada
                                    RescateActualizar.RES_Nav = ValorNAVActualizado
                                    RescateActualizar.RES_Nav_CLP = ValorNAV_CLPActualizado
                                    RescateActualizar.RES_Monto = ValorMontoActualizado
                                    RescateActualizar.RES_Monto_CLP = ValorMontoCLPActualizado
                                    RescateActualizar.TC_Valor = ValorTcObsSeleccionado

                                    CantidadFijados = CantidadFijados + 1
                                    NegocioRescates.RecalculoFijacionTC(RescateActualizar)
                                    negocioMod.UpdateFijacionTC(Fijacion.ID, Fijacion.TipoTransaccion)

                                    ListaErrores = AgregarError(ListaErrores, TipoErroresFijacion.EF_TRANSACCIONES_FIJADAS_EXITOSAMENTE)
                                End If

                            Else
                                'NO Realiza la fijacion
                                ListaErrores = AgregarError(ListaErrores, TipoErroresFijacion.EF_ERROR_NO_EXISTE_NAV_O_TC, nemotecnicoSel)
                                CantidadNOFijados = CantidadNOFijados + 1
                            End If
                        End If
                    Else
                        ListaErrores = AgregarError(ListaErrores, TipoErroresFijacion.EF_ERROR_INTERNO, nemotecnicoSel)
                        CantidadNOFijados = CantidadNOFijados + 1
                    End If

                ElseIf TipoTransaccionSeleccionado = "Canje" Then

                    Dim tipoCambioCanje As TipoCambioDTO = New TipoCambioDTO()

                    tipoCambioCanje.Fecha = FechaTCObsSeleccionada
                    Dim canje As CanjeDTO = New CanjeDTO()
                    canje.IdCanje = TransaccionSeleccionada
                    Dim canjeSeleccionado As CanjeDTO = NegocioCanje.GetCanje(canje)
                    canje.MonedaSaliente = canjeSeleccionado.MonedaSaliente
                    tipoCambioCanje.Codigo = canje.MonedaSaliente
                    Dim devolverTipoCambio = NegocioTC.GetTipoCambio(tipoCambioCanje)

                    If devolverTipoCambio Is Nothing Then
                        ListaErrores = AgregarError(ListaErrores, TipoErroresFijacion.EF_ERROR_NO_EXISTE_NAV_O_TC, canjeSeleccionado.NemotecnicoSaliente)
                        canjeSeleccionado.FijacionTC = "Pendiente"
                        CantidadNOFijados = CantidadNOFijados + 1
                        Continue For
                    End If

                    Dim aportante As AportanteDTO = New AportanteDTO()
                    aportante.Rut = canjeSeleccionado.RutAportante
                    If canjeSeleccionado.Multifondo = "&nbsp;" Then
                        aportante.Multifondo = String.Empty
                    Else
                        aportante.Multifondo = canjeSeleccionado.Multifondo
                    End If

                    Dim listaAportante As List(Of AportanteDTO) = NegocioCanje.CompararDatosAportantes(aportante)

                    Dim serieEntrante As FondoSerieDTO = New FondoSerieDTO()
                    Dim listaEntrantes As List(Of FondoSerieDTO)

                    serieEntrante.Rut = canjeSeleccionado.RutFondo
                    serieEntrante.Nemotecnico = canjeSeleccionado.NemotecnicoEntrante

                    listaEntrantes = NegocioCanje.CompararDatosEntrantes(serieEntrante)

                    Dim Ex As String = ""
                    Dim serieSaliente As FondoSerieDTO = New FondoSerieDTO()
                    Dim listaSaliente As List(Of FondoSerieDTO)

                    serieSaliente.Rut = canjeSeleccionado.RutFondo
                    serieSaliente.Nemotecnico = canjeSeleccionado.NemotecnicoSaliente

                    listaSaliente = NegocioCanje.CompararDatosSalientes(serieSaliente)

                    If listaAportante.Count > 0 Then
                        For Each aportantes As AportanteDTO In listaAportante
                            Dim razonSocial = aportantes.RazonSocial
                            Dim estado = aportantes.Estado
                            If canjeSeleccionado.NombreAportante <> razonSocial And estado = 1 Or estado = 0 Then
                                Mensaje = Mensaje & " No se puede modificar el canje, información del Aportante se modifico"
                                Ex = "x"
                            ElseIf listaEntrantes.Count > 0 Then
                                For Each entrante As FondoSerieDTO In listaEntrantes
                                    Dim serie = entrante.Nombrecorto
                                    Dim moneda = entrante.Moneda
                                    Dim estadoEntrante = entrante.Estado
                                    If canjeSeleccionado.NombreSerieEntrante <> serie Or canjeSeleccionado.MonedaEntrante <> moneda And estadoEntrante = 1 Or estadoEntrante = 0 Then
                                        Mensaje = Mensaje & (" No se puede modificar el canje, Serie entrante modificada")
                                        Ex = "x"
                                    ElseIf listaSaliente.Count > 0 Then
                                        For Each saliente As FondoSerieDTO In listaSaliente
                                            Dim serieSaliente2 = saliente.Nombrecorto
                                            Dim monedaSaliente = saliente.Moneda
                                            Dim estadoSaliente = saliente.Estado
                                            If canjeSeleccionado.NombreSerieSaliente <> serieSaliente2 Or canjeSeleccionado.MonedaSaliente <> monedaSaliente And estadoSaliente = 1 Or estadoSaliente = 0 Then
                                                Mensaje = Mensaje & (" No se puede modificar el canje, Serie saliente modificada ")
                                                Ex = "x"
                                            End If
                                        Next
                                    End If
                                Next
                            End If
                        Next
                    End If

                    If (Ex = "") Then
                        Dim navCLPSaliente As Decimal
                        Dim navCLPEntrante As Decimal
                        Dim montoCLPSaliente As Decimal
                        Dim montoCLPEntrante As Decimal

                        Dim diferencias As Decimal
                        Dim tipoCambi As Decimal

                        tipoCambi = devolverTipoCambio.Valor

                        If canjeSeleccionado.MonedaSaliente <> "CLP" And canjeSeleccionado.MonedaEntrante <> "CLP" Then

                            navCLPEntrante = Utiles.calcularNAVCLP(tipoCambi, canjeSeleccionado.NavEntrante)
                            navCLPSaliente = Utiles.calcularNAVCLP(tipoCambi, canjeSeleccionado.NavSaliente)
                            montoCLPEntrante = Utiles.calcularMontoCLP(canjeSeleccionado.CuotaEntrante, navCLPEntrante)
                            montoCLPSaliente = Utiles.calcularMontoCLP(canjeSeleccionado.CuotaSaliente, navCLPSaliente)

                            diferencias = montoCLPSaliente - montoCLPEntrante

                            canjeSeleccionado.NavCLPEntrante = navCLPEntrante
                            canjeSeleccionado.NavCLPSaliente = navCLPSaliente
                            canjeSeleccionado.MontoCLPEntrante = montoCLPEntrante
                            canjeSeleccionado.MontoCLPSaliente = montoCLPSaliente
                            canjeSeleccionado.DiferenciaCLP = diferencias
                            canjeSeleccionado.TipoCambio = tipoCambi
                            canjeSeleccionado.FijacionTC = "Realizado"

                            NegocioCanje.UpdateCanje(canjeSeleccionado)
                            negocioMod.UpdateFijacionTC(Fijacion.ID, Fijacion.TipoTransaccion)
                            CantidadFijados = CantidadFijados + 1
                            ListaErrores = AgregarError(ListaErrores, TipoErroresFijacion.EF_TRANSACCIONES_FIJADAS_EXITOSAMENTE)

                        ElseIf canjeSeleccionado.MonedaSaliente = "CLP" And canjeSeleccionado.MonedaEntrante = "CLP" Then
                            canjeSeleccionado.TipoCambio = tipoCambi
                            canjeSeleccionado.FijacionTC = "Realizado"

                            NegocioCanje.UpdateCanje(canjeSeleccionado)
                            negocioMod.UpdateFijacionTC(Fijacion.ID, Fijacion.TipoTransaccion)
                            CantidadFijados = CantidadFijados + 1
                            ListaErrores = AgregarError(ListaErrores, TipoErroresFijacion.EF_TRANSACCIONES_FIJADAS_EXITOSAMENTE)
                        End If
                    Else
                        CantidadNOFijados = CantidadNOFijados + 1
                    End If

                ElseIf TipoTransaccionSeleccionado = "Suscripcion" Then

                    Suscripcion.IdSuscripcion = TransaccionSeleccionada
                    SuscripcionSeleccionada = NegocioSuscripcion.GetSuscripcion(Suscripcion)

                    If SuscripcionSeleccionada IsNot Nothing Then
                        TipoCambio.Codigo = SuscripcionSeleccionada.MonedaSerie
                        Dim Relacion As SuscripcionDTO = NegocioSuscripcion.GetRelaciones(SuscripcionSeleccionada)
                        If (Relacion.CountAP > 0) Then
                            txtAccionHidden.Value = ""
                            Mensaje = Mensaje & " No se pudo modificar la suscripción " & TransaccionSeleccionada & " El aportante fue modificado."
                            CantidadNOFijados = CantidadNOFijados + 1
                        ElseIf (Relacion.CountFN > 0) Then
                            txtAccionHidden.Value = ""
                            Mensaje = Mensaje & " No se pudo modificar la suscripción " & TransaccionSeleccionada & " El fondo fue modificado."
                            CantidadNOFijados = CantidadNOFijados + 1
                        ElseIf (Relacion.CountFS > 0) Then
                            txtAccionHidden.Value = ""
                            Mensaje = Mensaje & " No se pudo modificar la suscripción " & TransaccionSeleccionada & " La serie fue modificada"
                            CantidadNOFijados = CantidadNOFijados + 1

                            ' JOVB: R3 
                        ElseIf Not EsConfirmada(SuscripcionSeleccionada) Then
                            txtAccionHidden.Value = ""
                            Mensaje = Mensaje & " No se pudo fijar la suscripción " & TransaccionSeleccionada & ". Transacción en Intención"
                            CantidadNOFijados = CantidadNOFijados + 1
                            ListaErrores = AgregarError(ListaErrores, TipoErroresFijacion.EF_TRANSACCIONES_FIJADAS_EXITOSAMENTE, SuscripcionSeleccionada.Nemotecnico)

                        Else

                            TipoCambio.Fecha = FechaTCObsSeleccionada

                            TipoCambioSeleccionado = NegocioTC.GetTipoCambio(TipoCambio)

                            If TipoCambioSeleccionado IsNot Nothing Then
                                ValorTcObsSeleccionado = TipoCambioSeleccionado.Valor

                                CuotasSeleccionado = SuscripcionSeleccionada.CuotasASuscribir
                                MonedaSerieSeleccionado = SuscripcionSeleccionada.MonedaSerie
                                ValorNAVSeleccionado = SuscripcionSeleccionada.NAV
                                ValorNAV_CLPSeleccionado = SuscripcionSeleccionada.NAVCLP
                                SuscripcionSeleccionada.TcObservado = ValorTcObsSeleccionado

                                If MonedaSerieSeleccionado = "CLP" Then
                                    CantidadFijados = CantidadFijados + 1
                                    NegocioSuscripcion.RecalculoFijacionTC(SuscripcionSeleccionada)
                                    negocioMod.UpdateFijacionTC(Fijacion.ID, Fijacion.TipoTransaccion)
                                    ListaErrores = AgregarError(ListaErrores, TipoErroresFijacion.EF_TRANSACCIONES_FIJADAS_EXITOSAMENTE)

                                Else
                                    'Recalcula y Manda a Actualizar cuando Moneda es Diferente a CLP
                                    ValorNAVActualizado = ValorNAVSeleccionado
                                    ValorNAV_CLPActualizado = (ValorNAVSeleccionado) * (ValorTcObsSeleccionado)
                                    ValorMontoActualizado = (CuotasSeleccionado) * (ValorNAVActualizado)
                                    ValorMontoCLPActualizado = (CuotasSeleccionado) * (ValorNAV_CLPActualizado)

                                    SuscripcionActualizar.IdSuscripcion = TransaccionSeleccionada
                                    SuscripcionActualizar.NAV = ValorNAVActualizado
                                    SuscripcionActualizar.NAVCLP = ValorNAV_CLPActualizado
                                    SuscripcionActualizar.Monto = ValorMontoActualizado
                                    SuscripcionActualizar.MontoCLP = ValorMontoCLPActualizado
                                    SuscripcionActualizar.TcObservado = ValorTcObsSeleccionado

                                    CantidadFijados = CantidadFijados + 1
                                    NegocioSuscripcion.RecalculoFijacionTC(SuscripcionActualizar)
                                    negocioMod.UpdateFijacionTC(Fijacion.ID, Fijacion.TipoTransaccion)
                                    ListaErrores = AgregarError(ListaErrores, TipoErroresFijacion.EF_TRANSACCIONES_FIJADAS_EXITOSAMENTE)

                                End If
                            Else
                                ListaErrores = AgregarError(ListaErrores, TipoErroresFijacion.EF_ERROR_NO_EXISTE_NAV_O_TC, SuscripcionSeleccionada.Nemotecnico)
                                CantidadNOFijados = CantidadNOFijados + 1
                            End If
                        End If
                    Else
                        ListaErrores = AgregarError(ListaErrores, TipoErroresFijacion.EF_ERROR_INTERNO)
                        CantidadNOFijados = CantidadNOFijados + 1

                    End If
                End If
            End If
        Next

        MostrarMensaje(ListaErrores, TransaccionesTotales, TransaccionesExitosas, TransaccionesNoExitosas, CantidadFijados, CantidadNOFijados, Mensaje, listaMensajes)

        txtAccionHidden.Value = ""
        'FormateoLimpiarForm()
        Me.GrvTabla.DataSource = Nothing
        GrvTabla.DataBind()
        FindFijacion()
        'ShowMessages(CONST_TITULO_FIJACION, CONST_MODIFICAR_EXITO, Constantes.CONST_RUTA_IMG + Constantes.CONST_IMG_LOGO, Constantes.CONST_RUTA_IMG + Constantes.CONST_IMG_CORRECTO)
    End Sub

    Protected Sub btnModificar_Click(sender As Object, e As EventArgs) Handles BtnModificar.Click
        Dim FijacionSelected As FijacionDTO = GetFijacionesSelect()
        Dim CanjeActualizada As CanjeDTO
        Dim SuscripcionActualizada As SuscripcionDTO
        Session("NAV") = Nothing
        Session("Tc") = Nothing

        If (FijacionSelected.TipoTransaccion = "Rescate") Then

            Dim negocio As RescateNegocio = New RescateNegocio
            Dim RescateSelect As RescatesDTO = GetRescateSelect()
            Dim RescateActualizado As RescatesDTO = negocio.GetRescateOne(RescateSelect)
            MantenedorRescate(RescateActualizado)

        ElseIf (FijacionSelected.TipoTransaccion = "Canje") Then
            CanjeActualizada = NegocioCanje.GetFijacionId(FijacionSelected.ID)
            MantenedorCanje(CanjeActualizada)
        ElseIf (FijacionSelected.TipoTransaccion = "Suscripcion") Then
            SuscripcionActualizada = NegocioSuscripcion.GetFijacionId(FijacionSelected.ID)

            MantenedorSuscripcion(SuscripcionActualizada)
        End If

        If (txtAccionHidden.Value = "x") Then
            txtAccionHidden.Value = ""
        Else
            FormateoEstiloFormModificar()
            txtAccionHidden.Value = FijacionSelected.TipoTransaccion
        End If

    End Sub

    Private Function GetRescateSelect() As RescatesDTO
        Dim Rescate As New RescatesDTO

        For Each row As GridViewRow In GrvTabla.Rows
            Dim chk As CheckBox = row.Cells(0).Controls(1)
            If chk IsNot Nothing And chk.Checked Then
                Rescate.RES_ID = row.Cells(CONST_COL_ID).Text.Trim()
            End If
        Next

        Return Rescate
    End Function

    Private Sub btnModalModificar_Click(sender As Object, e As EventArgs) Handles btnModalModificar.Click
        'MODIFICAR
        If (IsNumeric(txtNAV.Text) And IsNumeric(txtTCObservado.Text)) Then
            If (txtNAV.Text = "0" Or txtTCObservado.Text = "0") Then
                ShowAlert("Tc Observado y NAV deben ser mayores a 0, por favor verifique")
            Else
                Dim negocioMod As FijacionNegocio = New FijacionNegocio
                Dim NavSaliente = Convert.ToDecimal(txtNAV.Text)
                Dim NavEntrante = txtTCObservado.Text
                Dim IdSuscripcion = txtIdSuscripcion.Text
                Dim TipoTransaccion = "Suscripcion"
                Dim solicitudNAV As Integer
                Dim solicitudTC As Integer
                Dim ResultadoNAV As String
                Dim ResultadoTC As String

                Dim SuscripcionNegocio As SuscripcionNegocio = New SuscripcionNegocio
                Dim suscripcion As SuscripcionDTO = New SuscripcionDTO()

                suscripcion.IdSuscripcion = IdSuscripcion
                suscripcion.NAV = txtNAV.Text
                suscripcion.NAVCLP = txtNAVCLP.Text
                suscripcion.Monto = txtMonto.Text
                suscripcion.MontoCLP = txtMontoCLP.Text
                suscripcion.TcObservado = txtTCObservado.Text

                'ACTUALIZA RESCATES

                'If (txtNAV.Text <> Session("NAV")) Then
                solicitudNAV = NegocioSuscripcion.RecalculoFijacionNAV(suscripcion)
                If solicitudNAV = Constantes.CONST_OPERACION_EXITOSA Then
                    negocioMod.UpdateFijacionNav(IdSuscripcion, TipoTransaccion)
                    ResultadoNAV = CONST_MODIFICAR_NAV_EXITO
                Else
                    ResultadoNAV = CONST_MODIFICAR_NAV_ERROR
                End If

                solicitudTC = NegocioSuscripcion.RecalculoFijacionTC(suscripcion)
                If solicitudTC = Constantes.CONST_OPERACION_EXITOSA Then
                    negocioMod.UpdateFijacionTC(IdSuscripcion, TipoTransaccion)
                    ResultadoTC = CONST_MODIFICAR_TC_EXITO
                Else
                    ResultadoTC = CONST_MODIFICAR_TC_ERROR
                End If

                Dim Mensaje As String = ResultadoNAV + ResultadoTC
                If Mensaje = "" Then
                    ShowAlert(CONST_SIN_MODIFICACION)
                Else
                    ShowAlert(Mensaje)
                End If
                txtAccionHidden.Value = ""
                'DataInitial()
                Session("TC") = Nothing
                Session("NAV") = Nothing
                FindFijacion()
            End If
        Else
            ShowAlert("No deben haber campos vacíos, por favor verifique")
        End If

    End Sub

    Private Sub btnModalModificarCanje_Click(sender As Object, e As EventArgs) Handles btnModalModificarCanje.Click

        If (IsNumeric(txtModalNavEntrante.Text) And IsNumeric(txtModalTipoCambio.Text) And IsNumeric(txtModalNavSaliente.Text)) Then
            If (txtModalNavEntrante.Text = "0" Or txtModalTipoCambio.Text = "0" Or txtModalNavSaliente.Text = "0") Then
                ShowAlert("Tipo de cambio y ambos NAV deben ser mayores a 0, por favor verifique")
            Else
                Dim canje As CanjeDTO = GetCanjeModificar()
                Dim ResultadoNAV As String
                Dim ResultadoTC As String

                canje.FijacionTC = "Realizado"
                canje.FijacionNav = "Realizado"
                Dim solicitudCanje As Integer = NegocioCanje.UpdateCanje(canje)

                If solicitudCanje = Constantes.CONST_OPERACION_EXITOSA Then
                    ResultadoTC = CONST_MODIFICAR_TC_EXITO
                    ResultadoNAV = CONST_MODIFICAR_NAV_EXITO
                Else
                    ResultadoTC = CONST_MODIFICAR_TC_ERROR
                    ResultadoNAV = CONST_MODIFICAR_NAV_ERROR
                End If

                Dim Mensaje As String = ResultadoNAV + ResultadoTC
                If Mensaje = "" Then
                    ShowAlert(CONST_SIN_MODIFICACION)
                Else
                    ShowAlert(Mensaje)
                End If
                txtAccionHidden.Value = ""
                DataInitial()
                FindFijacion()
            End If
        Else
            ShowAlert("No deben haber campos vacíos, por favor verifique")
        End If

    End Sub

    Private Function GetCanjeModificar() As CanjeDTO
        Dim canje As CanjeDTO = New CanjeDTO()
        canje.IdCanje = txtIdCanje.Text
        canje.TipoTransaccion = txtModalTipoTrnasaccion.Text
        canje.RutAportante = ddlModalRutAportanteCanje.SelectedValue
        canje.Multifondo = ddlModalMultifondoCanje.Text
        canje.NombreAportante = ddlModalNombreAportanteCanje.SelectedValue
        canje.RutFondo = ddlModalFondoCanje.SelectedValue
        canje.NombreFondo = ddlModalNombreFondoCanje.SelectedValue
        canje.FechaNavSaliente = txtModalFechaNavSaliente.Text
        canje.FechaSolicitud = txtModalFechaSolicitud.Text
        canje.FechaCanjeDate = txtModalFechaCanje.Text
        canje.FechaObservado = txtModalFechaObservado.Text
        canje.NemotecnicoSaliente = ddlModalNemotecnicoSalienteCanje.SelectedValue
        canje.NombreSerieSaliente = ddlModalSerieSalienteCanje.SelectedValue
        canje.MonedaSaliente = ddlModalMonedaSalienteCanje.SelectedValue
        canje.CuotaSaliente = txtModalCuotaSaliente.Text
        canje.NavSaliente = txtModalNavSaliente.Text
        canje.MontoSaliente = txtModalMontoSaliente.Text
        canje.MontoCLPSaliente = txtModalMontoCLPSaliente.Text
        canje.FijacionNav = ddlModalFijacionNav.SelectedValue
        canje.NavCLPSaliente = txtModalNavCLPSaliente.Text
        canje.MontoEntrante = txtModalMontoEntrante.Text
        canje.MontoCLPEntrante = txtModalMontoEntrante.Text
        canje.Diferencia = txtModalDiferencia.Text
        canje.DiferenciaCLP = txtModalDiferenciaCLP.Text
        canje.Factor = txtModalFactor.Text
        canje.NemotecnicoEntrante = ddlModalNemotecnicoEntranteCanje.SelectedValue
        canje.NombreSerieEntrante = ddlModalSerieEntranteCanje.SelectedValue
        canje.MonedaEntrante = ddlModalMonedaEntranteCanje.SelectedValue
        canje.CuotaEntrante = txtModalCuotaEntrante.Text
        canje.NavEntrante = txtModalNavEntrante.Text
        canje.MontoEntrante = txtModalMontoEntrante.Text
        canje.NavCLPEntrante = txtModalNavCLPEntrante.Text
        canje.MontoCLPEntrante = txtModalMontoCLPEntrante.Text
        canje.ContratoGeneral = IIf(ddlModalContrato.SelectedValue = "", Nothing, ddlModalContrato.SelectedValue)
        canje.RevisionPoderes = IIf(ddlModalPoderes.SelectedValue = "", Nothing, ddlModalPoderes.SelectedValue)
        canje.EstadoCanje = IIf(ddlModalEstado.SelectedValue = "", Nothing, ddlModalEstado.SelectedValue)
        canje.Observaciones = IIf(txtModalObservaciones.Text = "", Nothing, txtModalObservaciones.Text)
        canje.FechaActual = txtModalFechaCuotaDCV.Text
        canje.Cuotas = txtModalCuotaDCV.Text
        canje.RescateTransito = txtModalRescateTransito.Text
        canje.SuscripcionTransito = txtModalSuscripcionTransito.Text
        canje.CanjeTransito = txtModalCanjeTransito.Text
        canje.CuotasDisponibles = IIf(txtModalCuotasDisponibles.Text = "", Nothing, txtModalCuotasDisponibles.Text)
        canje.FijacionTC = ddlModalFijacionTC.SelectedValue
        canje.Estado = IIf(txtEstadoCambio.Value = "", 1, txtEstadoCambio.Value)
        canje.UsuarioIngreso = Session("NombreUsuario")
        canje.UsuarioModificacion = Session("NombreUsuario")
        canje.FechaNavEntrante = txtModalFechaNavEntrante.Text
        canje.TipoCambio = txtModalTipoCambio.Text
        Return canje
    End Function

    'TODO: JOVB -> revisar el proceso de Prorrata
    'TODO: JOVB -> Revisar el Control de Patrimonio
    'TODO: JOVB

    Private Sub btnModalModificarRastreo_Click(sender As Object, e As EventArgs) Handles btnModalModificarRastreo.Click
        'MODIFICAR
        If (IsNumeric(txtModalTCObservado.Text) And IsNumeric(txtModalNAV.Text)) Then
            If (txtModalTCObservado.Text = "0" Or txtModalNAV.Text = "0") Then
                ShowAlert(Constantes.CONST_ERROR_TC_NAV_MAYOR_A_CERO)
            Else
                If ControlValidacionPatrimonioManual() Then
                    ShowAlert(Constantes.CONST_MENSAJE_NO_CUMPLE_REGLA)
                    Exit Sub
                End If

                Dim negocioMod As FijacionNegocio = New FijacionNegocio
                Dim IdRescate = txtIDRescate.Text
                Dim TipoTransaccion = "Rescate"

                Dim negocioRescate As RescateNegocio = New RescateNegocio
                Dim rescate As RescatesDTO = New RescatesDTO()

                rescate.RES_ID = IdRescate
                rescate.RES_Nav = txtModalNAV.Text
                rescate.RES_Nav_CLP = txtModalNAV_CLP.Text
                rescate.RES_Monto = txtModalMonto.Text
                rescate.RES_Monto_CLP = txtModalMontoCLP.Text
                rescate.TC_Valor = txtModalTCObservado.Text

                'ACTUALIZA RESCATES
                Dim solicitudNAV As Integer
                Dim solicitudTC As Integer
                Dim ResultadoNAV As String
                Dim ResultadoTC As String

                solicitudNAV = negocioRescate.RecalculoFijacionNAV(rescate)
                If solicitudNAV = Constantes.CONST_OPERACION_EXITOSA Then
                    negocioMod.UpdateFijacionNav(IdRescate, TipoTransaccion)
                    ResultadoNAV = CONST_MODIFICAR_NAV_EXITO
                Else
                    ResultadoNAV = CONST_MODIFICAR_NAV_ERROR
                End If

                solicitudTC = negocioRescate.RecalculoFijacionTC(rescate)
                If solicitudTC = Constantes.CONST_OPERACION_EXITOSA Then
                    negocioMod.UpdateFijacionTC(IdRescate, TipoTransaccion)
                    ResultadoTC = CONST_MODIFICAR_TC_EXITO
                Else
                    ResultadoTC = CONST_MODIFICAR_TC_ERROR
                End If

                Dim Mensaje As String = ResultadoNAV + ResultadoTC
                If Mensaje = "" Then
                    ShowAlert(CONST_SIN_MODIFICACION)
                Else
                    ShowAlert(Mensaje)
                End If

                txtAccionHidden.Value = ""
                'FormateoLimpiarForm()
                Me.GrvTabla.DataSource = Nothing
                GrvTabla.DataBind()
                DataInitial()
                FindFijacion()
            End If
        Else
            ShowAlert("No deben haber campos vacíos, por favor verifique")
        End If


    End Sub

    Private Function ControlValidacionPatrimonioManual() As Boolean
        Dim rescate As RescatesDTO = New RescatesDTO()

        rescate.RES_Fecha_Solicitud = txtModalFechaSolicitudRescate.Text
        rescate.AP_RUT = ddlModalRutAportanteRescate.SelectedValue
        rescate.AP_Multifondo = ddlModalMultifondoRescate.SelectedValue
        rescate.FN_RUT = ddlModalRutFondoRescate.SelectedValue
        rescate.FS_Moneda = txtModalMonedaSerie.Text
        rescate.RES_Monto = 0
        rescate.FS_Nemotecnico = ddlModalNemotecnicoRescate.SelectedValue

        rescate.DesdeProceso = "Fijacion"

        Return ControlValidacionPatrimonio(rescate)

    End Function

    Private Function ControlValidacionPatrimonioAutomatico(rescate As RescatesDTO) As Boolean
        rescate.RES_Monto = 0

        If ControlValidacionPatrimonio(rescate) Then
            Return True
        Else
            Return False
        End If
    End Function

    Private Function ControlValidacionPatrimonio(rescate As RescatesDTO) As Boolean
        Dim negocioRescate As RescateNegocio = New RescateNegocio
        Dim fondo As FondoDTO = New FondoDTO()
        Dim negocioFondo As FondosNegocio = New FondosNegocio

        fondo.Rut = rescate.FN_RUT
        fondo = negocioFondo.GetFondo(fondo)

        'rescate.FN_Nombre_Corto = fondo.RazonSocial
        'rescate.FS_Nemotecnico = "" 'VERIFICAR SI DEBE APLICAR NEMOTECNICO DEL RESCATE A VALIDAR

        'If negocioRescate.ExisteVentana(rescate) Then
        'fondo.ControlTipoControl = "Ventana"

        Dim resultadoCalculo As String

        Dim resultado() As String = negocioRescate.ControlMontoRescateVsPatrimonio(rescate, fondo).Split(",")
        resultadoCalculo = resultado(0)

        Return resultadoCalculo
        'Else
        'Return False
        'End If
    End Function

    Protected Sub BtnBuscar_Click(sender As Object, e As EventArgs) Handles BtnBuscar.Click
        FindFijacion()
        ValidaPermisosPerfil()
        If GrvTabla.Rows.Count <> 0 Then

            BtnExportar.Enabled = True
        Else
            BtnExportar.Enabled = False
            ShowAlert(CONST_SIN_RESULTADOS)
        End If

        txtAccionHidden.Value = ""
        Session("TC") = Nothing
        Session("NAV") = Nothing
    End Sub

    Protected Sub BtnExportar_Click(sender As Object, e As EventArgs) Handles BtnExportar.Click
        Dim Fijacion As FijacionDTO = New FijacionDTO()
        Dim negocio As FijacionNegocio = New FijacionNegocio
        Dim FechaTCDesde As Nullable(Of Date)
        Dim FechaTCHasta As Nullable(Of Date)
        Dim FechaNAVDesde As Nullable(Of Date)
        Dim FechaNAVHasta As Nullable(Of Date)

        Dim FechaPagoDesde As Nullable(Of Date)
        Dim FechaPagoHasta As Nullable(Of Date)


        txtNAVDesde.Text = Request.Form(txtNAVDesde.UniqueID)
        txtNAVHasta.Text = Request.Form(txtNAVHasta.UniqueID)
        txtFechaTCDesde.Text = Request.Form(txtFechaTCDesde.UniqueID)
        txtFechaTCHasta.Text = Request.Form(txtFechaTCHasta.UniqueID)

        txtFechaPagoDesde.Text = Request.Form(txtFechaPagoDesde.UniqueID)
        txtFechaPagoHasta.Text = Request.Form(txtFechaPagoHasta.UniqueID)

        If ddlListaTipoTransaccion.SelectedValue.Trim() = Nothing Then
            Fijacion.TipoTransaccion = Nothing
        Else
            Fijacion.TipoTransaccion = ddlListaTipoTransaccion.SelectedValue.Trim()
        End If

        If ddlListaRutFondo.SelectedValue.Trim() = Nothing Then
            Fijacion.Rut = Nothing
        Else
            Dim arrCadena As String() = ddlListaRutFondo.SelectedItem.Text().Split(New Char() {"/"c})

            Fijacion.Rut = arrCadena(0).Trim()
            Fijacion.FnNombreCorto = arrCadena(1).Trim()
        End If

        If ddlListaNemotecnico.SelectedValue.Trim() = Nothing Then
            Fijacion.Nemotecnico = Nothing
        Else
            Fijacion.Nemotecnico = ddlListaNemotecnico.SelectedValue.Trim()
        End If

        If ddlFijacionNav.SelectedValue.Trim() = Nothing Then
            Fijacion.FijacionNAV = Nothing
        Else
            Fijacion.FijacionNAV = ddlFijacionNav.SelectedValue.Trim()
        End If

        If ddlFijacionTCObservacion.SelectedValue.Trim() = Nothing Then
            Fijacion.FijacionTCObservado = Nothing
        Else
            Fijacion.FijacionTCObservado = ddlFijacionTCObservacion.SelectedValue.Trim()
        End If

        If Request.Form(txtNAVDesde.UniqueID).Equals("") Then
            FechaNAVDesde = Nothing
        Else
            FechaNAVDesde = Date.Parse(Request.Form(txtNAVDesde.UniqueID))
        End If

        If Request.Form(txtNAVHasta.UniqueID).Equals("") Then
            FechaNAVHasta = Nothing
        Else
            FechaNAVHasta = Date.Parse(txtNAVHasta.Text)
        End If

        If Request.Form(txtFechaTCDesde.UniqueID).Equals("") Then
            FechaTCDesde = Nothing
        Else
            FechaTCDesde = Date.Parse(txtFechaTCDesde.Text)
        End If

        If Request.Form(txtFechaTCHasta.UniqueID).Equals("") Then
            FechaTCHasta = Nothing
        Else
            FechaTCHasta = Date.Parse(txtFechaTCHasta.Text)
        End If

        If Request.Form(txtFechaPagoDesde.UniqueID).Equals("") Then
            FechaPagoDesde = Nothing
        Else
            FechaPagoDesde = Date.Parse(txtFechaPagoDesde.Text)
        End If

        If Request.Form(txtFechaPagoHasta.UniqueID).Equals("") Then
            FechaPagoHasta = Nothing
        Else
            FechaPagoHasta = Date.Parse(txtFechaPagoHasta.Text)
        End If

        If ddlEstadoConfirmacion.Text.Trim() = "" Then
            Fijacion.EstadoIntencion = Nothing
        Else
            Fijacion.EstadoIntencion = ddlEstadoConfirmacion.SelectedValue
        End If

        Dim mensaje As String = ""

        If ddlFijacionTCObservacion.SelectedValue.Trim() = Nothing And _
            ddlListaTipoTransaccion.SelectedValue.Trim() = Nothing And _
            ddlListaRutFondo.SelectedValue.Trim() = Nothing And _
            ddlListaNemotecnico.SelectedValue.Trim() = Nothing And _
            ddlFijacionNav.SelectedValue.Trim() = Nothing And _
            txtNAVDesde.Text.Equals("") And _
            txtNAVHasta.Text.Equals("") And _
            txtFechaTCDesde.Text.Equals("") And _
            txtFechaTCHasta.Text.Equals("") And _
            txtFechaPagoDesde.Text.Equals("") And _
            txtFechaPagoHasta.Text.Equals("") And _
            ddlEstadoConfirmacion.Text = "" Then

            mensaje = negocio.ExportarAExcelTodos(Fijacion)
        Else
            mensaje = negocio.ExportarAExcel(Fijacion, FechaNAVDesde, FechaNAVHasta, FechaTCDesde, FechaTCHasta, FechaPagoDesde, FechaPagoHasta)
        End If

        If negocio.rutaArchivosExcel IsNot Nothing Then
            Archivo.NavigateUrl = negocio.rutaArchivosExcel
            Archivo.Text = "Bajar Archivo"
        Else
            Archivo.Visible = False
        End If

        txtAccionHidden.Value = "MOSTRAR_DIALOGO"

        ShowMessages(CONST_TITULO_FIJACION, mensaje, Constantes.CONST_RUTA_IMG + Constantes.CONST_IMG_LOGO, Constantes.CONST_RUTA_IMG + Constantes.CONST_IMG_INFO, False)

    End Sub
#End Region

#Region "Link Button"
    Protected Sub LinkButton7_Click(sender As Object, e As EventArgs)
        Calendar7.Visible = (Not Calendar7.Visible)
        txtAccionHidden.Value = ""
    End Sub

    Protected Sub LinkButton8_Click(sender As Object, e As EventArgs)
        Calendar8.Visible = (Not Calendar8.Visible)
        txtAccionHidden.Value = ""
    End Sub

    Protected Sub LinkButton9_Click(sender As Object, e As EventArgs)
        Calendar9.Visible = (Not Calendar9.Visible)
        txtAccionHidden.Value = ""
    End Sub

    Protected Sub LinkButton10_Click(sender As Object, e As EventArgs)
        Calendar10.Visible = (Not Calendar10.Visible)
        txtAccionHidden.Value = ""
    End Sub
#End Region
#Region "Carga Nemotecnico por rut"

#End Region

#Region "CARGA NOMBRE APORTANTE Y MULTIFONDO  CUANDO CAMBIA COMBO RUT APORTANTE"
    Public Sub CargarNombreAportanteNemotecnicoPorRutAportanteModal()
        'CARGA NOMBRE APORTANTE Y MULTIFONDO POR RUT APORTANTE
        CargarNombreAportantePorRutAportanteModal()
        CargarMultifondoPorRutAportanteModal()
    End Sub

    Private Sub CargarNombreAportantePorRutAportanteModal()
        Dim fechahasta As Nullable(Of Date)
        Dim NegocioAportante As AportanteNegocio = New AportanteNegocio
        Dim aportante As AportanteDTO = New AportanteDTO()

        aportante.Rut = ddlModalRutAportante.SelectedValue

        Dim aportantes As List(Of AportanteDTO) = NegocioAportante.GetListaAportantes(aportante, fechahasta)

        If aportantes.Count = 0 Then
            ddlModalNombreAportante.Items.Insert(0, New ListItem("", ""))
        Else
            ddlModalNombreAportante.DataSource = aportantes
            ddlModalNombreAportante.DataMember = "razonSocial"
            ddlModalNombreAportante.DataValueField = "razonSocial"
            ddlModalNombreAportante.DataBind()
        End If
    End Sub

    Private Sub CargarMultifondoPorRutAportanteModal()
        Dim fechahasta As Nullable(Of Date)
        Dim NegocioAportante As AportanteNegocio = New AportanteNegocio
        Dim aportante As AportanteDTO = New AportanteDTO()

        aportante.Rut = ddlModalRutAportante.SelectedValue

        Dim aportantes As List(Of AportanteDTO) = NegocioAportante.GetListaAportantes(aportante, fechahasta)

        If aportantes.Count = 0 Then
            ddlModalMultifondo.Items.Insert(0, New ListItem("", ""))
            ddlModalMultifondo.SelectedIndex = 0
            ddlModalMultifondo.Enabled = False
        Else
            Try
                ddlModalMultifondo.Enabled = True
                ddlModalMultifondo.DataSource = aportantes
                ddlModalMultifondo.DataMember = "multifondo"
                ddlModalMultifondo.DataValueField = "multifondo"
                ddlModalMultifondo.DataBind()
            Catch ex As Exception
                ddlModalMultifondo.SelectedIndex = 0
                ddlModalMultifondo.Enabled = False
            End Try

        End If
        If (ddlModalMultifondo.Text = "") Then
            ddlModalMultifondo.Enabled = False
        Else
            ddlModalMultifondo.Enabled = True
        End If
    End Sub

#End Region

    Protected Sub LlenarPorNemotecnico()
        CargarRutPorNemotecnico()
        ConsultarFechaSuscripcipn()
        ConsultarFechaNav()
        CargarNombreMonedaSerieModal()
        ConsultarFechaObservado()
        CargarCuotasDCV()
        CalcularValorEntrante()
        CargarTCObs()
        CargarNombreFondo()
        CargarNombreMonedaSerie()
    End Sub

#Region "CARGA DATOS DE SERIE Y FONDO"
    Public Sub CargarRutPorNemotecnico()
        Dim fondoSerie As FondoSerieDTO = New FondoSerieDTO()
        Dim fondo As FondoDTO = New FondoDTO()
        Dim NegocioSerie As FondoSeriesNegocio = New FondoSeriesNegocio

        fondoSerie.Nemotecnico = ddlNemotecnico.SelectedValue
        Dim Series As List(Of FondoSerieDTO) = NegocioSerie.GetListaFondoSerieConFiltro(fondoSerie, fondo)

        If Series.Count = 0 Then
            ddlFondo.Items.Insert(0, New ListItem("", ""))
            ddlFondo.Enabled = False
        Else
            ddlFondo.DataSource = Series
            ddlFondo.DataMember = "Rut"
            ddlFondo.DataValueField = "Rut"
            ddlFondo.DataBind()
        End If
    End Sub

    Public Sub CargarNombreFondo()
        Dim Fondo As FondoDTO = New FondoDTO()
        Dim FondoNegocio As FondosNegocio = New FondosNegocio()
        Fondo.Rut = ddlFondo.SelectedValue()
        Dim fondoActualizado As FondoDTO = FondoNegocio.GetFondo(Fondo)
        txtNombreFondo.Text = fondoActualizado.RazonSocial
    End Sub

    Public Sub CargarNombreMonedaSerie()
        Dim negocio As FondoSeriesNegocio = New FondoSeriesNegocio
        Dim FondoSerie As FondoSerieDTO = New FondoSerieDTO()
        FondoSerie.Rut = ddlFondo.SelectedValue
        FondoSerie.Nemotecnico = ddlNemotecnico.SelectedValue
        Dim FondoSerieActualizado As FondoSerieDTO = negocio.GetFondosSeries(FondoSerie)
        txtNombreSerie.Text = FondoSerieActualizado.Nombrecorto
        txtMonedaSerie.Text = FondoSerieActualizado.Moneda
    End Sub

    Public Sub SelectedIndexChangedFnRut()
        CargarNemotecnicoPorRut()
        ConsultarFechaSuscripcipn()
        ConsultarFechaNav()
        CargarNombreMonedaSerieModal()
        ConsultarFechaObservado()
        CargarCuotasDCV()
        CalcularValorEntrante()
        CargarTCObs()
        CargarNombreFondo()
        CargarNombreMonedaSerie()
    End Sub

    Public Sub CargarNemotecnicoPorRut()
        Dim fondoSerie As FondoSerieDTO = New FondoSerieDTO()
        Dim fondo As FondoDTO = New FondoDTO()
        Dim NegocioSerie As FondoSeriesNegocio = New FondoSeriesNegocio

        fondoSerie.Rut = ddlFondo.SelectedValue
        Dim Series As List(Of FondoSerieDTO) = NegocioSerie.GetListaFondoSerieConFiltro(fondoSerie, fondo)

        If Series.Count = 0 Then
            ddlNemotecnico.Items.Insert(0, New ListItem("", ""))
        Else
            ddlNemotecnico.DataSource = Series
            ddlNemotecnico.DataMember = "Nemotecnico"
            ddlNemotecnico.DataValueField = "Nemotecnico"
            ddlNemotecnico.DataBind()
        End If

    End Sub

    Public Sub CargarRutAportanteNemotecnicoPorNombreAportanteModal()
        'CARGA NOMBRE APORTANTE Y MULTIFONDO POR RUT APORTANTE

        CargarRutAportantePorNombreAportanteModal()
        CargarMultifondoPorNombreAportanteModal()
    End Sub

    Public Sub CargarNombreMultifondoPorRut()
        CargarNombreAportantePorRutAportanteModal()
        CargarMultifondoPorRutAportanteModal()
    End Sub

    Private Sub CargarRutAportantePorNombreAportanteModal()
        Dim fechahasta As Nullable(Of Date)
        Dim NegocioAportante As AportanteNegocio = New AportanteNegocio
        Dim aportante As AportanteDTO = New AportanteDTO()

        aportante.RazonSocial = ddlModalNombreAportante.SelectedValue

        Dim aportantes As List(Of AportanteDTO) = NegocioAportante.GetListaAportantes(aportante, fechahasta)

        If aportantes.Count = 0 Then
            ddlModalRutAportante.Items.Insert(0, New ListItem("", ""))
        Else
            ddlModalRutAportante.DataSource = aportantes
            ddlModalRutAportante.DataMember = "rut"
            ddlModalRutAportante.DataValueField = "rut"
            ddlModalRutAportante.DataBind()
        End If
    End Sub

    Private Sub CargarMultifondoPorNombreAportanteModal()
        Dim fechahasta As Nullable(Of Date)
        Dim NegocioAportante As AportanteNegocio = New AportanteNegocio
        Dim aportante As AportanteDTO = New AportanteDTO()

        aportante.RazonSocial = ddlModalNombreAportante.SelectedValue

        Dim aportantes As List(Of AportanteDTO) = NegocioAportante.GetListaAportantes(aportante, fechahasta)

        If aportantes.Count = 0 Then
            ddlModalMultifondo.Items.Insert(0, New ListItem("", ""))
        Else
            ddlModalMultifondo.DataSource = aportantes
            ddlModalMultifondo.DataMember = "multifondo"
            ddlModalMultifondo.DataValueField = "multifondo"
            ddlModalMultifondo.DataBind()
        End If
        If (ddlModalMultifondo.Text = "") Then
            ddlModalMultifondo.Enabled = False
        Else
            ddlModalMultifondo.Enabled = True
        End If
    End Sub

    Public Sub CargarRutYRazonSocialPorMultifondo()
        'CARGA NOMBRE APORTANTE Y RUT POR MULTIFONDO
        CargarRutAportantePorMultifondo()
        CargarRazonSocialPorMultifondo()
    End Sub

    Private Sub CargarRutAportantePorMultifondo()
        Dim aportante As AportanteDTO = New AportanteDTO()
        Dim Negocio As AportanteNegocio = New AportanteNegocio
        aportante.Multifondo = ddlModalMultifondo.SelectedValue
        Dim aportantes As List(Of AportanteDTO) = Negocio.AportantePorMultifondo(aportante)

        If aportantes.Count = 0 Then
            ddlModalRutAportante.Items.Insert(0, New ListItem("", ""))
        Else
            ddlModalRutAportante.DataSource = aportantes
            ddlModalRutAportante.DataMember = "rut"
            ddlModalRutAportante.DataValueField = "rut"
            ddlModalRutAportante.DataBind()
        End If
    End Sub

    Private Sub CargarRazonSocialPorMultifondo()
        Dim aportante As AportanteDTO = New AportanteDTO()
        Dim Negocio As AportanteNegocio = New AportanteNegocio
        aportante.Multifondo = ddlModalMultifondo.SelectedValue
        Dim aportantes As List(Of AportanteDTO) = Negocio.AportantePorMultifondo(aportante)

        If aportantes.Count = 0 Then
            ddlModalNombreAportante.Items.Insert(0, New ListItem("", ""))
        Else
            ddlModalNombreAportante.DataSource = aportantes
            ddlModalNombreAportante.DataMember = "RazonSocial"
            ddlModalNombreAportante.DataValueField = "RazonSocial"
            ddlModalNombreAportante.DataBind()
        End If
    End Sub

#End Region

#Region "Calendar Selection Change"

    Protected Sub Calendar7_SelectionChanged(sender As Object, e As EventArgs) Handles Calendar7.SelectionChanged
        txtFechaNAV.Text = Calendar7.SelectedDate.ToShortDateString()
        Calendar7.SelectedDate = Nothing
        Calendar7.Visible = False
    End Sub

    Protected Sub Calendar8_SelectionChanged(sender As Object, e As EventArgs) Handles Calendar8.SelectionChanged
        txtFechaTC.Text = Calendar8.SelectedDate.ToShortDateString()
        Calendar8.SelectedDate = Nothing
        Calendar8.Visible = False
    End Sub

    Protected Sub Calendar9_SelectionChanged(sender As Object, e As EventArgs) Handles Calendar9.SelectionChanged
        txtFechaIntencion.Text = Calendar9.SelectedDate.ToShortDateString()
        Calendar9.SelectedDate = Nothing
        Calendar9.Visible = False
    End Sub

    Protected Sub Calendar10_SelectionChanged(sender As Object, e As EventArgs) Handles Calendar10.SelectionChanged
        txtFechaSuscripcion.Text = Calendar10.SelectedDate.ToShortDateString()
        Calendar10.SelectedDate = Nothing
        Calendar10.Visible = False
    End Sub

    Private Sub BtnLimpiarFechaDesde_Click(sender As Object, e As EventArgs) Handles BtnLimpiarFechaDesde.Click
        txtNAVDesde.Text = Nothing
    End Sub

    Private Sub LinkButton5_Click(sender As Object, e As EventArgs) Handles LinkButton5.Click
        txtNAVHasta.Text = Nothing
    End Sub

    Private Sub LinkButton6_Click(sender As Object, e As EventArgs) Handles LinkButton6.Click
        txtFechaTCDesde.Text = Nothing
    End Sub

    Private Sub LinkButton11_Click(sender As Object, e As EventArgs) Handles LinkButton11.Click
        txtFechaTCHasta.Text = Nothing
    End Sub

#End Region

#Region "Formateo Datos"
    Private Sub MantenedorSuscripcion(Suscripcion As SuscripcionDTO)
        CargaRutAportanteSusc()
        CargarNombreAportanteModalSuscripciones()
        CargarMultifondoModalSuscripciones()

        Dim Relacion As SuscripcionDTO = NegocioSuscripcion.GetRelaciones(Suscripcion)
        If (Relacion.CountAP > 0) Then
            ShowAlert("No se puede modificar la suscripción, información del aportante se modificó")
            txtAccionHidden.Value = "x"

        ElseIf (Relacion.CountFN > 0) Then
            ShowAlert("No se puede modificar esta suscripción, información del fondo se modificó")
            txtAccionHidden.Value = "x"

        ElseIf (Relacion.CountFS > 0) Then
            ShowAlert("No se puede modificar esta suscripción, información de la serie se modificó")
            txtAccionHidden.Value = "x"

            ' JOVB: R3 
        ElseIf Not EsConfirmada(Suscripcion) Then
            ShowAlert("No se pudo fijar la suscripción " & Suscripcion.TipoTransaccion & ". Transacción en Intención")
            txtAccionHidden.Value = "x"

        Else
            txtIdSuscripcion.Text = Suscripcion.IdSuscripcion
            ddlModalNombreAportante.SelectedValue = Suscripcion.RazonSocial
            ddlModalRutAportante.Text = Suscripcion.RutAportante
            Try
                ddlModalMultifondo.SelectedValue = Suscripcion.Multifondo
            Catch ex As Exception
                ddlModalMultifondo.SelectedIndex = 0
            End Try
            ddlFondo.Text = Suscripcion.RutFondo
            ddlNemotecnico.Text = Suscripcion.Nemotecnico
            txtCuotas.Text = String.Format("{0:N0}", Suscripcion.CuotasASuscribir)
            txtNAV.Text = Suscripcion.NavFormat
            ddlMonedaPago.Text = Suscripcion.Moneda_Pago
            txtNAVCLP.Text = Suscripcion.NAVCLPFormat   '     String.Format("{0:N4}", Suscripcion.NAVCLP)
            Dim tcPaso As String = Replace(Suscripcion.TcObservado, ".", ",")
            txtTCObservado.Text = Utiles.formatearTC(tcPaso) ' String.Format("{0:N6}", Suscripcion.TcObservado)
            ddlPoderes.Text = Suscripcion.RevisionPoderes
            txtFechaIntencion.Text = Suscripcion.FechaIntencion
            txtFechaNAV.Text = Suscripcion.FechaNAV
            txtFechaSuscripcion.Text = Suscripcion.FechaSuscripcion
            txtFechaTC.Text = Suscripcion.FechaTC
            txtMonto.Text = Suscripcion.MontoFormat      ' String.Format("{0:N6}", Suscripcion.Monto)
            ddlContrato.Text = Suscripcion.ContratoFondo
            txtFechaDCV.Text = Suscripcion.FechaDCV
            ddlEstado1.Text = Suscripcion.EstadoSuscripcion
            txtObservaciones.Text = Suscripcion.Observaciones
            txtFijacionNAV.Text = Suscripcion.FijacionNAV
            txtFijacionTCObs.Text = Suscripcion.FijacionTC
            txtDisponibles.Text = String.Format("{0:N0}", Suscripcion.CuotasDisponibles)
            txtMontoCLP.Text = Suscripcion.MontoCLPFormat      ' String.Format("{0:N6}", Suscripcion.MontoCLP)
            'txtTCObservado.Text = String.Format("{0:N6}", Suscripcion.TcObservado)
            txtCuotasDCV.Text = String.Format("{0:N0}", Suscripcion.CuotasDCV)
            txtRescates.Text = String.Format("{0:N0}", Suscripcion.RescatesTransitos)
            txtSuscripciones.Text = String.Format("{0:N0}", Suscripcion.SuscripcionesTransito)
            txtCanje.Text = String.Format("{0:N0}", Suscripcion.CanjesTransito)
            txtNombreFondo.Text = Suscripcion.FondoNombreCorto
            txtNombreSerie.Text = Suscripcion.SerieNombreCorto
            txtMonedaSerie.Text = Suscripcion.MonedaSerie
            txtCuotasEmitidas.Text = String.Format("{0:N0}", Suscripcion.CuotasEmitidas)
            txtActual.Text = String.Format("{0:N0}", Suscripcion.ScActual)
            txtUtilizado.Text = String.Format("{0:N0}", Suscripcion.ScUtilizado)
            txtAcumulada.Text = String.Format("{0:N0}", Suscripcion.FnAcumulada)
            txtDisponiblesEmitidas.Text = String.Format("{0:N0}", Suscripcion.ScDisponibles)
            Suscripcion.Estado = "1"
            Session("NAV") = Suscripcion.NAV
            Session("Tc") = Suscripcion.TcObservado

            'JOVB: R3
            ddlEstadoIntencion.Text = IIf(Suscripcion.EstadoIntencion = "", "Intencion", Suscripcion.EstadoIntencion)
        End If
    End Sub

    Private Sub MantenedorRescate(Rescate As RescatesDTO)

        Dim Relacion As RescatesDTO = NegocioRescate.GetRelaciones(Rescate)
        Dim fondo As FondoDTO = New FondoDTO()
        Dim negocioFondo As FondosNegocio = New FondosNegocio

        fondo.Rut = Rescate.FN_RUT
        fondo = negocioFondo.GetFondo(fondo)

        If (Relacion.CountAP > 0) Then
            txtAccionHidden.Value = ""
            ShowAlert(" No se pudo modificar el rescate, el aportante fue modificado.")
            txtAccionHidden.Value = "x"

        ElseIf (Relacion.CountFN > 0) Then
            txtAccionHidden.Value = ""
            ShowAlert(" No se pudo modificar el rescate, el fondo fue modificado.")
            txtAccionHidden.Value = "x"

        ElseIf (Relacion.CountFS > 0) Then
            txtAccionHidden.Value = ""
            ShowAlert(" No se pudo modificar el rescate, la serie fue modificada")
            txtAccionHidden.Value = "x"

            'ElseIf (fondo.ControlTipoDeConfiguracion = "Prorrata") Then
            '    ShowAlert("DEBE REALIZAR PRORRATA")
            '    txtAccionHidden.Value = "x"
        Else


            CargarRutFondoModal()
            CargaNombreFondoModal()
            CargarRutAportanteModal()
            CargarMultifondoModal()
            CargaNemotecnicoModal()
            CargarNombreAportanteModal()

            txtIDRescate.Text = Rescate.RES_ID
            txtModalFechaSolicitudRescate.Text = Rescate.RES_Fecha_Solicitud
            txtModalFechaPago.Text = Rescate.RES_Fecha_Pago
            ddlModalRutAportanteRescate.SelectedValue = Rescate.AP_RUT
            ddlModalMultifondoRescate.SelectedValue = Rescate.AP_Multifondo
            ddlModalNemotecnicoRescate.SelectedValue = Rescate.FS_Nemotecnico
            txtModalCuota.Text = Utiles.SetToCapitalizedNumber(Rescate.RES_Cuotas)
            ddlModalRutFondoRescate.SelectedValue = Rescate.FN_RUT
            ddlModalNombreFondoRescate.SelectedValue = Rescate.FN_Nombre_Corto

            If (ddlModalNombreAportanteRescate.Items.IndexOf(ddlModalNombreAportanteRescate.Items.FindByValue(Rescate.AP_Razon_Social)) <> -1) Then
                ddlModalNombreAportanteRescate.SelectedValue = Rescate.AP_Razon_Social
            End If
            txtModalNombreSerie.Text = Rescate.FS_Nombre_Corto
            ddlModalMonedaPago.SelectedValue = Rescate.RES_Moneda_Pago
            txtModalCuotasDVC.Text = Utiles.SetToCapitalizedNumber(Rescate.ADCV_Cantidad)
            txtModalFechaNAV.Text = Rescate.RES_Fecha_Nav
            txtModalFechaTCObs.Text = Rescate.RES_FechaTCObs
            txtModalNAV.Text = Rescate.RES_NavFormat
            txtModalMonto.Text = Rescate.RES_MontoFormat   '   Utiles.SetToCapitalizedNumber(Rescate.RES_Monto)
            txtModalNAV_CLP.Text = Rescate.RES_Nav_CLPFormat
            txtModalMontoCLP.Text = Rescate.RES_Monto_CLPFormat     ' String.Format("{0:N0}", Double.Parse(Rescate.RES_Monto_CLP))
            txtModalTCObservado.Text = Utiles.SetToCapitalizedNumber(Rescate.TC_Valor)
            Dropdownlist7.SelectedValue = Rescate.RES_Contrato
            Dropdownlist9.SelectedValue = Rescate.RES_Poderes
            ddlModalEstado.SelectedValue = Rescate.RES_Estado
            txtModalObservaciones.Text = Rescate.RES_Observaciones
            txtModalPatrimonio.Text = Utiles.SetToCapitalizedNumber(Rescate.RES_Patrimonio)
            txtModalPorcentaje.Text = Utiles.SetToCapitalizedNumber(Rescate.FS_Patrimonio)
            txtModalDisponibles.Text = Utiles.SetToCapitalizedNumber(Rescate.RES_Disponible_Patrimonio)
            txtModalFechaDCV.Text = Rescate.ADCV_Fecha
            txtModalSuscripciones.Text = Utiles.SetToCapitalizedNumber(Rescate.SC_Cuotas_a_Suscribir)
            txtModalCanje.Text = Utiles.SetToCapitalizedNumber(Rescate.CN_Cuotas_Disponibles)
            txtModalDisponiblesCuotasDisponibles.Text = Utiles.SetToCapitalizedNumber(Rescate.RES_Cuotas_Disponibles)
            txtModalRescates.Text = Utiles.SetToCapitalizedNumber(Rescate.RES_Transito)
            txtModalFijacionNAV.Text = Rescate.RES_Fijacion_NAV
            txtModalFijacionTCObs.Text = Rescate.RES_Fijacion_TCObservado
            txtModalMonedaSerie.Text = Rescate.FS_Moneda
            txtModalRescateMax.Text = Utiles.SetToCapitalizedNumber(Rescate.RES_Maximo)
            txtModalUtilizado.Text = Utiles.SetToCapitalizedNumber(Rescate.RES_Utilizado)
            Session("NAV") = Rescate.RES_Nav
            Session("Tc") = Rescate.TC_Valor
        End If
    End Sub

    Private Sub MantenedorCanje(Canje As CanjeDTO)
        Dim aportante As AportanteDTO = New AportanteDTO()
        aportante.Rut = Canje.RutAportante
        If Canje.Multifondo = "&nbsp;" Then
            aportante.Multifondo = String.Empty
        Else
            aportante.Multifondo = Canje.Multifondo
        End If
        Dim listaAportante As List(Of AportanteDTO) = NegocioCanje.CompararDatosAportantes(aportante)

        Dim serieEntrante As FondoSerieDTO = New FondoSerieDTO()
        serieEntrante.Rut = Canje.RutFondo
        serieEntrante.Nemotecnico = Canje.NemotecnicoEntrante
        Dim listaEntrantes As List(Of FondoSerieDTO) = NegocioCanje.CompararDatosEntrantes(serieEntrante)

        Dim Ex As String = ""
        Dim serieSaliente As FondoSerieDTO = New FondoSerieDTO()
        serieSaliente.Rut = Canje.RutFondo
        serieSaliente.Nemotecnico = Canje.NemotecnicoSaliente
        Dim listaSaliente As List(Of FondoSerieDTO) = NegocioCanje.CompararDatosSalientes(serieSaliente)

        If listaAportante.Count > 0 Then
            For Each aportantes As AportanteDTO In listaAportante
                Dim razonSocial = aportantes.RazonSocial
                Dim estado = aportantes.Estado
                If estado = 0 Then
                    ShowAlert(" No se puede modificar el canje, información del Aportante se modifico")
                    txtAccionHidden.Value = "x"
                    Ex = "x"
                ElseIf listaEntrantes.Count > 0 Then
                    For Each entrante As FondoSerieDTO In listaEntrantes
                        Dim serie = entrante.Nombrecorto
                        Dim moneda = entrante.Moneda
                        Dim estadoEntrante = entrante.Estado
                        If estadoEntrante = 0 Then
                            ShowAlert(" No se puede modificar el canje, Serie entrante modificada")
                            Ex = "x"
                            txtAccionHidden.Value = "x"
                        ElseIf listaSaliente.Count > 0 Then
                            For Each saliente As FondoSerieDTO In listaSaliente
                                Dim serieSaliente2 = saliente.Nombrecorto
                                Dim monedaSaliente = saliente.Moneda
                                Dim estadoSaliente = saliente.Estado
                                If estadoSaliente = 0 Then
                                    ShowAlert(" No se puede modificar el canje, Serie saliente modificada ")
                                    Ex = "x"
                                    txtAccionHidden.Value = "x"
                                End If
                            Next
                        End If
                    Next
                End If
            Next
        End If
        If (Ex = "") Then
            cargarRutAportanteModalCanje()
            cargarNombreAportanteCanje()
            cargarMultifondoAportanteModalCanje()
            cargaRutModalCanje()
            cargaNombreRutModalCanje()
            cargaNemotecnicoSalienteEntranteModalCanje()
            cargaNombreSerieSalienteEntranteModalCanje()
            cargaMonedaSerieSalienteEntranteModalCanje()

            txtIdCanje.Text = Canje.IdCanje
            txtModalTipoTrnasaccion.Text = Canje.TipoTransaccion
            ddlModalRutAportanteCanje.SelectedValue = Canje.RutAportante
            ddlModalMultifondoCanje.SelectedValue = Canje.Multifondo
            ddlModalNombreAportanteCanje.SelectedValue = Canje.NombreAportante
            ddlModalFondoCanje.SelectedValue = Canje.RutFondo
            ddlModalNombreFondoCanje.SelectedValue = Canje.NombreFondo
            txtModalFechaNavSaliente.Text = Canje.FechaNavSaliente
            txtModalFechaSolicitud.Text = Canje.FechaSolicitud
            txtModalFechaCanje.Text = Canje.FechaCanje
            txtModalFechaObservado.Text = Canje.FechaObservado
            ddlModalNemotecnicoSalienteCanje.SelectedValue = Canje.NemotecnicoSaliente
            ddlModalSerieSalienteCanje.SelectedValue = Canje.NombreSerieSaliente
            ddlModalMonedaSalienteCanje.SelectedValue = Canje.MonedaSaliente
            txtModalCuotaSaliente.Text = Utiles.SetToCapitalizedNumber(Math.Truncate(Canje.CuotaSaliente))
            txtModalNavSaliente.Text = Canje.NavSalienteFormat
            txtModalMontoSaliente.Text = Utiles.formatearMonto(Canje.MontoSaliente, Canje.MonedaSaliente)     '       Utiles.SetToCapitalizedNumber(Canje.MontoSaliente)
            txtModalNavCLPSaliente.Text = Canje.NavCLPSalienteFormat
            txtModalMontoCLPSaliente.Text = Utiles.formatearMontoCLP(Canje.MontoCLPSaliente)       '    Utiles.SetToCapitalizedNumber(Math.Truncate(Canje.MontoCLPSaliente))
            txtModalFactor.Text = Utiles.SetToCapitalizedNumber(Canje.Factor)
            txtFactorSaliente.Text = Utiles.SetToCapitalizedNumber(Canje.Factor)
            txtModalDiferencia.Text = Utiles.SetToCapitalizedNumber(Canje.Diferencia)
            txtModalDiferenciaCLP.Text = Utiles.SetToCapitalizedNumber(Canje.DiferenciaCLP)
            ddlModalNemotecnicoEntranteCanje.SelectedValue = Canje.NemotecnicoEntrante
            ddlModalSerieEntranteCanje.SelectedValue = Canje.NombreSerieEntrante
            ddlModalMonedaEntranteCanje.SelectedValue = Canje.MonedaEntrante
            txtModalCuotaEntrante.Text = Utiles.SetToCapitalizedNumber(Math.Truncate(Canje.CuotaEntrante))
            txtModalNavEntrante.Text = Canje.NavEntranteFormat
            txtModalMontoEntrante.Text = Utiles.formatearMonto(Canje.MontoEntrante, Canje.MonedaEntrante)      '  Utiles.SetToCapitalizedNumber(Canje.MontoEntrante)
            txtModalNavCLPEntrante.Text = Canje.NavCLPEntranteFormat
            txtModalMontoCLPEntrante.Text = Utiles.formatearMontoCLP(Canje.MontoCLPEntrante)       ' Utiles.SetToCapitalizedNumber(Math.Truncate(Canje.MontoCLPEntrante))
            ddlModalContrato.SelectedValue = Canje.ContratoGeneral
            ddlModalPoderes.SelectedValue = Canje.RevisionPoderes
            ddlModalEstado.SelectedValue = Canje.EstadoCanje
            txtModalObservaciones.Text = Canje.Observaciones
            txtModalFechaCuotaDCV.Text = Canje.FechaActual
            txtModalCuotaDCV.Text = Utiles.SetToCapitalizedNumber(Canje.Cuotas)
            txtModalRescateTransito.Text = Utiles.SetToCapitalizedNumber(Canje.RescateTransito)
            txtModalSuscripcionTransito.Text = Utiles.SetToCapitalizedNumber(Canje.SuscripcionTransito)
            txtModalCanjeTransito.Text = Utiles.SetToCapitalizedNumber(Canje.CanjeTransito)
            txtModalCuotasDisponibles.Text = Utiles.SetToCapitalizedNumber(Canje.CuotasDisponibles)
            ddlModalFijacionNav.SelectedValue = Canje.FijacionNav
            ddlModalFijacionTC.SelectedValue = Canje.FijacionTC
            txtModalFechaNavEntrante.Text = Canje.FechaNavEntrante
            txtModalTipoCambio.Text = Utiles.SetToCapitalizedNumber(Canje.TipoCambio)
            txtEstadoCambio.Value = Utiles.SetToCapitalizedNumber(Canje.Estado)
            Session("NAV") = Canje.NavSaliente
            Session("Tc") = Canje.TipoCambio
        End If
    End Sub

    Private Sub FormateoEstiloFormCrear()
        btnModalModificar.Enabled = False
        btnModalModificar.Visible = False
        FormateoFormCrearModificar()
        CargaDatosModalInicial()
        lbModalTittle.Text = CONST_TITULO_MODAL_CREAR
    End Sub

    Private Sub FormateoEstiloFormModificar()
        btnModalModificar.Visible = True
        btnModalModificar.Enabled = True
        FormateoFormCrearModificar()
        txtAccionHidden.Value = "MODIFICAR"

        'Canje
        ddlModalRutAportanteCanje.Enabled = False
        ddlModalNombreAportanteCanje.Enabled = False
        txtIdCanje.Enabled = False
        txtModalTipoTrnasaccion.Enabled = False
        'DropDownList1.Enabled = False
        ddlModalMultifondoCanje.Enabled = False
        ddlModalNombreAportante.Enabled = False
        ddlModalFondoCanje.Enabled = False
        ddlModalNombreFondoCanje.Enabled = False
        txtModalFechaNavSaliente.Enabled = False
        txtModalFechaSolicitud.Enabled = False
        txtModalFechaObservado.Enabled = False
        ddlModalNemotecnicoSalienteCanje.Enabled = False
        ddlModalSerieSalienteCanje.Enabled = False
        ddlModalMonedaSalienteCanje.Enabled = False
        txtModalCuotaSaliente.Enabled = False
        txtModalMontoSaliente.Enabled = False
        txtModalNavCLPSaliente.Enabled = False
        txtModalMontoCLPSaliente.Enabled = False
        txtModalFactor.Enabled = False
        txtFactorSaliente.Enabled = False
        txtModalDiferencia.Enabled = False
        txtModalDiferenciaCLP.Enabled = False
        ddlModalNemotecnicoEntranteCanje.Enabled = False
        ddlModalSerieEntranteCanje.Enabled = False
        ddlModalMonedaEntranteCanje.Enabled = False
        txtModalCuotaEntrante.Enabled = False
        txtModalMontoEntrante.Enabled = False
        txtModalNavCLPEntrante.Enabled = False
        txtModalMontoCLPEntrante.Enabled = False
        ddlModalContrato.Enabled = False
        ddlModalPoderes.Enabled = False
        ddlModalEstado.Enabled = False
        txtModalObservaciones.Enabled = False
        txtModalFechaCuotaDCV.Enabled = False
        txtModalCuotaDCV.Enabled = False
        txtModalRescateTransito.Enabled = False
        txtModalSuscripcionTransito.Enabled = False
        txtModalCanjeTransito.Enabled = False
        txtModalCuotasDisponibles.Enabled = False
        ddlModalFijacionNav.Enabled = False
        ddlModalFijacionTC.Enabled = False
        txtModalFechaNavEntrante.Enabled = False
        txtModalTipoCambio.Enabled = True
        txtModalNavEntrante.Enabled = True

        'Rescate
        txtIDRescate.Enabled = False
        txtModalFechaSolicitudRescate.Enabled = False
        txtModalFechaPago.Enabled = False
        ddlModalRutAportanteRescate.Enabled = False
        ddlModalMultifondoRescate.Enabled = False
        ddlModalNemotecnicoRescate.Enabled = False
        txtModalCuota.Enabled = False
        ddlModalRutFondoRescate.Enabled = False
        ddlModalNombreFondoRescate.Enabled = False
        ddlModalNombreAportanteRescate.Enabled = False
        txtModalNombreSerie.Enabled = False
        ddlModalMonedaPago.Enabled = False
        txtModalCuotasDVC.Enabled = False
        txtModalFechaNAV.Enabled = False
        txtModalFechaTCObs.Enabled = False
        txtModalMonto.Enabled = False
        txtModalNAV_CLP.Enabled = False
        txtModalMontoCLP.Enabled = False
        ddlModalContrato.Enabled = False
        ddlModalPoderes.Enabled = False
        ddlModalEstado.Enabled = False
        txtModalObservaciones.Enabled = False
        txtModalPatrimonio.Enabled = False
        txtModalPorcentaje.Enabled = False
        txtModalDisponibles.Enabled = False
        txtModalFechaDCV.Enabled = False
        txtModalSuscripciones.Enabled = False
        txtModalCanje.Enabled = False
        txtModalDisponiblesCuotasDisponibles.Enabled = False
        txtModalRescates.Enabled = False
        txtModalFijacionNAV.Enabled = False
        txtModalFijacionTCObs.Enabled = False
        txtModalMonedaSerie.Enabled = False
        txtModalRescateMax.Enabled = False
        txtModalUtilizado.Enabled = False

        'suscripcion 

        txtIdSuscripcion.Enabled = False
        ddlModalNombreAportante.Enabled = False
        ddlModalRutAportante.Enabled = False
        ddlModalMultifondo.Enabled = False
        ddlFondo.Enabled = False
        ddlNemotecnico.Enabled = False
        txtCuotas.Enabled = False
        ddlMonedaPago.Enabled = False
        txtNAVCLP.Enabled = False
        ddlPoderes.Enabled = False
        txtFechaIntencion.Enabled = False
        txtFechaNAV.Enabled = False
        txtFechaSuscripcion.Enabled = False
        txtFechaTC.Enabled = False
        txtMonto.Enabled = False
        ddlContrato.Enabled = False
        txtFechaDCV.Enabled = False
        ddlEstado1.Enabled = False
        txtObservaciones.Enabled = False
        txtFijacionNAV.Enabled = False
        txtFijacionTCObs.Enabled = False
        txtDisponibles.Enabled = False
        txtMontoCLP.Enabled = False
        txtCuotasDCV.Enabled = False
        txtRescates.Enabled = False
        txtSuscripciones.Enabled = False
        txtCanje.Enabled = False
        txtNombreFondo.Enabled = False
        txtNombreSerie.Enabled = False
        txtMonedaSerie.Enabled = False
        ddlEstadoIntencion.Enabled = False



        lblModalTitle.Text = CONST_TITULO_MODAL_MODIFICAR
    End Sub

    Protected Sub FormateoFormCrearModificar()
        ddlModalNombreAportante.Enabled = True
        ddlModalRutAportante.Enabled = True
        ddlModalMultifondo.Enabled = True
        ddlFondo.Enabled = True
        ddlNemotecnico.Enabled = True
        txtCuotas.Enabled = True
        txtNAV.Enabled = True
        ddlMonedaPago.Enabled = True
        txtNAVCLP.Enabled = True
        txtTCObservado.Enabled = True
        ddlPoderes.Enabled = True
        txtFechaIntencion.Enabled = True
        txtFechaNAV.Enabled = True
        txtFechaSuscripcion.Enabled = True
        txtFechaTC.Enabled = True
        txtMonto.Enabled = True
        ddlContrato.Enabled = True
        ddlEstado1.Enabled = True
        txtObservaciones.Enabled = True
        txtDisponibles.Enabled = True
        txtMontoCLP.Enabled = True
        txtTCObservado.Enabled = True


    End Sub

    Private Sub FormateoLimpiarForm()
        ddlListaTipoTransaccion.SelectedIndex = 0
        ddlListaRutFondo.SelectedIndex = 0
        ddlListaNemotecnico.SelectedValue = ""
        ddlFijacionNav.SelectedValue = ""
        ddlFijacionTCObservacion.SelectedValue = ""

        txtFechaTCDesde.Text = ""
        txtFechaTCHasta.Text = ""

        txtNAVDesde.Text = ""
        txtNAVHasta.Text = ""

        txtFechaPagoHasta.Text = ""
        txtFechaPagoDesde.Text = ""


        GrvTabla.DataSource = New List(Of SuscripcionDTO)
        GrvTabla.DataBind()
        BtnExportar.Enabled = False
    End Sub

#End Region

#Region "Cargar Datos Modal Rescate"
    Private Sub CargarRutFondoModal()
        Dim Negocio As FondoSeriesNegocio = New FondoSeriesNegocio
        Dim serie As FondoSerieDTO = New FondoSerieDTO()

        Dim listafondo As List(Of FondoSerieDTO) = Negocio.GetListaFondoRut(serie)

        If listafondo.Count = 0 Then
            ddlModalRutFondoRescate.Items.Insert(0, New ListItem("", ""))
            ddlModalRutFondoRescate.SelectedIndex = 0
        Else

            ddlModalRutFondoRescate.DataSource = listafondo
            ddlModalRutFondoRescate.DataMember = "Rut"
            ddlModalRutFondoRescate.DataValueField = "Rut"
            ddlModalRutFondoRescate.DataBind()
            ddlModalRutFondoRescate.Items.Insert(0, New ListItem("", ""))
            ddlModalRutFondoRescate.SelectedIndex = 0
        End If
    End Sub

    Public Sub CargaNombreFondoModal()
        Dim Negocio As FondosNegocio = New FondosNegocio
        Dim fondo As FondoDTO = New FondoDTO()
        Dim listafondo As List(Of FondoDTO) = Negocio.GetNombreFondo(fondo)

        If listafondo.Count = 0 Then
            ddlModalNombreFondoRescate.Items.Insert(0, New ListItem("", ""))
        Else
            ddlModalNombreFondoRescate.DataSource = listafondo
            ddlModalNombreFondoRescate.DataMember = "RazonSocial"
            ddlModalNombreFondoRescate.DataValueField = "RazonSocial"
            ddlModalNombreFondoRescate.DataBind()
            ddlModalNombreFondoRescate.Items.Insert(0, New ListItem("", ""))
            ddlModalNombreFondoRescate.SelectedIndex = 0
        End If
    End Sub

    Private Sub CargarRutAportanteModal()
        Dim NegocioAportante As AportanteNegocio = New AportanteNegocio
        Dim aportante As AportanteDTO = New AportanteDTO()
        Dim aportantes As List(Of AportanteDTO) = NegocioAportante.GetListaAportantesDistinct(aportante)

        If aportantes.Count = 0 Then
            ddlModalRutAportanteRescate.Items.Insert(0, New ListItem("", ""))
            ddlModalRutAportanteRescate.SelectedIndex = 0
        Else
            ddlModalRutAportanteRescate.DataSource = aportantes
            ddlModalRutAportanteRescate.DataMember = "Rut"
            ddlModalRutAportanteRescate.DataValueField = "Rut"
            ddlModalRutAportanteRescate.DataBind()
            ddlModalRutAportanteRescate.Items.Insert(0, New ListItem("", ""))
            ddlModalRutAportanteRescate.SelectedIndex = 0
        End If
    End Sub

    Private Sub CargarNombreAportanteModal()
        Dim NegocioAportante As AportanteNegocio = New AportanteNegocio
        Dim aportante As AportanteDTO = New AportanteDTO()
        Dim aportantes As List(Of AportanteDTO) = NegocioAportante.GetListaAportantesDistinct(aportante)

        If aportantes.Count = 0 Then
            ddlModalNombreAportanteRescate.Items.Insert(0, New ListItem("", ""))
            ddlModalNombreAportanteRescate.SelectedIndex = 0
        Else
            ddlModalNombreAportanteRescate.DataSource = aportantes
            ddlModalNombreAportanteRescate.DataMember = "razonSocial"
            ddlModalNombreAportanteRescate.DataValueField = "razonSocial"
            ddlModalNombreAportanteRescate.DataBind()
            ddlModalNombreAportanteRescate.Items.Insert(0, New ListItem("", ""))
            ddlModalNombreAportanteRescate.SelectedIndex = 0
        End If
    End Sub

    Private Sub CargarMultifondoModal()
        Dim NegocioAportante As AportanteNegocio = New AportanteNegocio
        Dim aportante As AportanteDTO = New AportanteDTO()
        Dim aportantes As List(Of AportanteDTO) = NegocioAportante.GetListaAportantesDistinct(aportante)

        If aportantes.Count = 0 Then
            ddlModalMultifondoRescate.Items.Insert(0, New ListItem("", ""))
            ddlModalMultifondoRescate.SelectedIndex = 0
        Else
            ddlModalMultifondoRescate.DataSource = aportantes
            ddlModalMultifondoRescate.DataMember = "Multifondo"
            ddlModalMultifondoRescate.DataValueField = "Multifondo"
            ddlModalMultifondoRescate.DataBind()
            ddlModalMultifondoRescate.Items.Insert(0, New ListItem("", ""))
            ddlModalMultifondoRescate.SelectedIndex = 0
        End If
    End Sub

    Public Sub CargaNemotecnicoModal()
        Dim NegocioFondoSerie As FondoSeriesNegocio = New FondoSeriesNegocio
        Dim fondoSeries As FondoSerieDTO = New FondoSerieDTO()
        Dim fondo As FondoDTO = New FondoDTO()
        fondo.RazonSocial = ddlModalNombreFondoRescate.SelectedValue
        Dim listafondoSeries As List(Of FondoSerieDTO) = NegocioFondoSerie.GetbyNombreFondo(fondoSeries, fondo)

        If listafondoSeries.Count = 0 Then
            ddlModalNemotecnicoRescate.Items.Insert(0, New ListItem("", ""))
            ddlModalNemotecnicoRescate.SelectedIndex = 0
        Else
            ddlModalNemotecnicoRescate.DataSource = listafondoSeries
            ddlModalNemotecnicoRescate.DataMember = "Nemotecnico"
            ddlModalNemotecnicoRescate.DataValueField = "Nemotecnico"
            ddlModalNemotecnicoRescate.DataBind()
            ddlModalNemotecnicoRescate.Items.Insert(0, New ListItem("", ""))
            ddlModalNemotecnicoRescate.SelectedIndex = 0
        End If

    End Sub
#End Region

#Region "Cargar Datos Modal Canje"
    Private Sub cargarRutAportanteModalCanje()
        Dim NegocioAportante As AportanteNegocio = New AportanteNegocio
        Dim aportante As New AportanteDTO
        Dim listaRut As List(Of AportanteDTO) = NegocioAportante.GetListaAportantesPorRut(aportante)
        If listaRut.Count = 0 Then
            ddlModalRutAportanteCanje.Items.Insert(0, New ListItem("Seleccione", String.Empty))
        Else
            ddlModalRutAportanteCanje.DataSource = listaRut
            ddlModalRutAportanteCanje.DataMember = "Rut"
            ddlModalRutAportanteCanje.DataValueField = "Rut"
            ddlModalRutAportanteCanje.DataBind()
            ddlModalRutAportanteCanje.Items.Insert(0, New ListItem("Seleccione", String.Empty))

        End If
    End Sub

    Private Sub cargarNombreAportanteCanje()
        Dim NegocioAportante As AportanteNegocio = New AportanteNegocio
        Dim aportante As New AportanteDTO
        Dim listaRut As List(Of AportanteDTO) = NegocioAportante.GetListaAportantesPorRazonSocial(aportante)
        If listaRut.Count = 0 Then
            ddlModalNombreAportanteCanje.Items.Insert(0, New ListItem("Seleccione", String.Empty))
        Else
            ddlModalNombreAportanteCanje.DataSource = listaRut
            ddlModalNombreAportanteCanje.DataMember = "RazonSocial"
            ddlModalNombreAportanteCanje.DataValueField = "RazonSocial"
            ddlModalNombreAportanteCanje.DataBind()
            ddlModalNombreAportanteCanje.Items.Insert(0, New ListItem("Seleccione", String.Empty))

        End If
    End Sub

    Private Sub cargarMultifondoAportanteModalCanje()
        Dim aportante As New AportanteDTO
        Dim listaRut As List(Of AportanteDTO) = NegocioCanje.ConsultarMultifondo(aportante)
        If listaRut.Count = 0 Then
            ddlModalMultifondoCanje.Items.Insert(0, New ListItem("Seleccione", String.Empty))
        Else
            ddlModalMultifondoCanje.DataSource = listaRut
            ddlModalMultifondoCanje.DataMember = "Multifondo"
            ddlModalMultifondoCanje.DataValueField = "Multifondo"
            ddlModalMultifondoCanje.DataBind()
            ddlModalMultifondoCanje.Items.Insert(0, New ListItem("Seleccione", String.Empty))
        End If
    End Sub

    Private Sub cargaRutModalCanje()
        Dim NegocioFondoSerie As FondoSeriesNegocio = New FondoSeriesNegocio
        Dim fondo As New FondoSerieDTO
        Dim lista As List(Of FondoSerieDTO) = NegocioFondoSerie.GetListaFondosRut(fondo)
        ddlModalFondoCanje.Items.Add("Seleccione")

        If lista.Count = 0 Then
            ddlModalFondoCanje.Items.Insert(0, New ListItem("Seleccione", String.Empty))
        Else
            ddlModalFondoCanje.DataSource = lista
            ddlModalFondoCanje.DataMember = "Rut"
            ddlModalFondoCanje.DataValueField = "Rut"
            ddlModalFondoCanje.DataBind()
            ddlModalFondoCanje.Items.Insert(0, New ListItem("Seleccione", String.Empty))

        End If
    End Sub

    Private Sub cargaNombreRutModalCanje()
        Dim NegocioFondoSerie As FondosNegocio = New FondosNegocio
        Dim fondo As New FondoDTO
        Dim fondoSerie As New FondoSerieDTO
        Dim lista As List(Of FondoDTO) = NegocioFondoSerie.GetNombreFondo(fondo)
        If lista.Count = 0 Then
            ddlModalNombreFondoCanje.Items.Insert(0, New ListItem("", ""))
        Else
            ddlModalNombreFondoCanje.DataSource = lista
            ddlModalNombreFondoCanje.DataMember = "RazonSocial"
            ddlModalNombreFondoCanje.DataValueField = "RazonSocial"
            ddlModalNombreFondoCanje.DataBind()
            ddlModalNombreFondoCanje.Items.Insert(0, New ListItem("", ""))
        End If
    End Sub

    Private Sub cargaNemotecnicoSalienteEntranteModalCanje()
        Dim NegocioFondoSerie As FondoSeriesNegocio = New FondoSeriesNegocio
        Dim nemotecnico As New FondoSerieDTO
        Dim lista As List(Of FondoSerieDTO) = NegocioFondoSerie.GetListaFondoSerieporNemotecnico(nemotecnico)

        If lista.Count = 0 Then
            ddlModalNemotecnicoSalienteCanje.Items.Insert(0, New ListItem("", ""))
            ddlModalNemotecnicoEntranteCanje.Items.Insert(0, New ListItem("", ""))
        Else
            ddlModalNemotecnicoSalienteCanje.DataSource = lista
            ddlModalNemotecnicoSalienteCanje.DataMember = "Nemotecnico"
            ddlModalNemotecnicoSalienteCanje.DataValueField = "Nemotecnico"
            ddlModalNemotecnicoSalienteCanje.DataBind()
            ddlModalNemotecnicoSalienteCanje.Items.Insert(0, New ListItem("", ""))

            ddlModalNemotecnicoEntranteCanje.DataSource = lista
            ddlModalNemotecnicoEntranteCanje.DataMember = "Nemotecnico"
            ddlModalNemotecnicoEntranteCanje.DataValueField = "Nemotecnico"
            ddlModalNemotecnicoEntranteCanje.DataBind()
            ddlModalNemotecnicoEntranteCanje.Items.Insert(0, New ListItem("", ""))
        End If
    End Sub

    Private Sub cargaNombreSerieSalienteEntranteModalCanje()
        Dim serie As New FondoSerieDTO
        Dim lista As List(Of FondoSerieDTO) = NegocioCanje.ConsultarNombreSerie(serie)

        If lista.Count = 0 Then
            ddlModalSerieEntranteCanje.Items.Insert(0, New ListItem("", ""))
            ddlModalSerieSalienteCanje.Items.Insert(0, New ListItem("", ""))
        Else
            ddlModalSerieEntranteCanje.DataSource = lista
            ddlModalSerieEntranteCanje.DataMember = "Nombrecorto"
            ddlModalSerieEntranteCanje.DataValueField = "Nombrecorto"
            ddlModalSerieEntranteCanje.DataBind()
            ddlModalSerieEntranteCanje.Items.Insert(0, New ListItem("", ""))

            ddlModalSerieSalienteCanje.DataSource = lista
            ddlModalSerieSalienteCanje.DataMember = "Nombrecorto"
            ddlModalSerieSalienteCanje.DataValueField = "Nombrecorto"
            ddlModalSerieSalienteCanje.DataBind()
            ddlModalSerieSalienteCanje.Items.Insert(0, New ListItem("", ""))
        End If
    End Sub

    Private Sub cargaMonedaSerieSalienteEntranteModalCanje()
        Dim serie As New FondoSerieDTO
        Dim lista As List(Of FondoSerieDTO) = NegocioCanje.ConsultarMonedaSerie(serie)

        If lista.Count = 0 Then
            ddlModalMonedaSalienteCanje.Items.Insert(0, New ListItem("", ""))
            ddlModalMonedaEntranteCanje.Items.Insert(0, New ListItem("", ""))
        Else
            ddlModalMonedaSalienteCanje.DataSource = lista
            ddlModalMonedaSalienteCanje.DataMember = "Moneda"
            ddlModalMonedaSalienteCanje.DataValueField = "Moneda"
            ddlModalMonedaSalienteCanje.DataBind()
            ddlModalMonedaSalienteCanje.Items.Insert(0, New ListItem("", ""))

            ddlModalMonedaEntranteCanje.DataSource = lista
            ddlModalMonedaEntranteCanje.DataMember = "Moneda"
            ddlModalMonedaEntranteCanje.DataValueField = "Moneda"
            ddlModalMonedaEntranteCanje.DataBind()
            ddlModalMonedaEntranteCanje.Items.Insert(0, New ListItem("", ""))
        End If
    End Sub
#End Region

#Region "Cargar Datos Modal Suscripciones"
    Private Sub CargaRutAportanteSusc()
        Dim lista As List(Of AportanteDTO) = GetAportantesPorRut()

        If lista.Count = 0 Then
            ddlModalRutAportante.Items.Insert(0, New ListItem("", ""))

        Else
            ddlModalRutAportante.DataSource = lista
            ddlModalRutAportante.DataMember = "Rut"
            ddlModalRutAportante.DataValueField = "Rut"
            ddlModalRutAportante.DataBind()
            ddlModalRutAportante.Items.Insert(0, New ListItem("", ""))

            ddlModalNombreAportante.DataSource = lista
            ddlModalNombreAportante.DataMember = "RazonSocial"
            ddlModalNombreAportante.DataValueField = "RazonSocial"
            ddlModalNombreAportante.DataBind()

            ddlModalMultifondo.DataSource = lista
            ddlModalMultifondo.DataMember = "Multifondo"
            ddlModalMultifondo.DataValueField = "Multifondo"
            ddlModalMultifondo.DataBind()
        End If
    End Sub

    Private Sub CargarNombreAportanteModalSuscripciones()
        Dim Suscripcion As New SuscripcionDTO()
        Dim Negocio As New SuscripcionNegocio()

        If (ddlModalMultifondo.Text = "") Then
            Suscripcion.Multifondo = Nothing
        Else
            Suscripcion.Multifondo = ddlModalMultifondo.Text
        End If
        Dim lista As List(Of SuscripcionDTO) = Negocio.ConsultarPorRazonSocial(Suscripcion)

        Dim vacia As New SuscripcionDTO
        If lista.Count = 0 Then
            lista.Add(vacia)
        Else
            ddlModalNombreAportante.DataSource = lista
            ddlModalNombreAportante.DataMember = "RazonSocial"
            ddlModalNombreAportante.DataValueField = "RazonSocial"
            ddlModalNombreAportante.DataBind()
            ddlModalNombreAportante.Items.Insert(0, New ListItem("", ""))
        End If
    End Sub

    Private Sub CargarMultifondoModalSuscripciones()
        Dim NegocioAportante As AportanteNegocio = New AportanteNegocio
        Dim aportante As AportanteDTO = New AportanteDTO()
        Dim aportantes As List(Of AportanteDTO) = GetAportantesPorRut()

        If aportantes.Count = 0 Then
            ddlModalMultifondo.Items.Insert(0, New ListItem("", ""))
            ddlModalMultifondo.SelectedIndex = 0
        Else
            ddlModalMultifondo.DataSource = aportantes
            ddlModalMultifondo.DataMember = "Multifondo"
            ddlModalMultifondo.DataValueField = "Multifondo"
            ddlModalMultifondo.DataBind()
        End If
    End Sub

    Private Sub CargarNombreAportantePorRutAportanteModalSusc()
        Dim fechahasta As Nullable(Of Date)
        Dim NegocioAportante As AportanteNegocio = New AportanteNegocio
        Dim aportante As AportanteDTO = New AportanteDTO()

        aportante.Rut = ddlModalRutAportante.SelectedValue

        Dim aportantes As List(Of AportanteDTO) = NegocioAportante.GetListaAportantes(aportante, fechahasta)

        If aportantes.Count = 0 Then
            ddlModalNombreAportante.Items.Insert(0, New ListItem("", ""))
        Else
            ddlModalNombreAportante.DataSource = aportantes
            ddlModalNombreAportante.DataMember = "razonSocial"
            ddlModalNombreAportante.DataValueField = "razonSocial"
            ddlModalNombreAportante.DataBind()
        End If
    End Sub

    Private Sub CargarMultifondoPorNombreAportanteModalSusc()
        Dim fechahasta As Nullable(Of Date)
        Dim NegocioAportante As AportanteNegocio = New AportanteNegocio
        Dim aportante As AportanteDTO = New AportanteDTO()

        aportante.RazonSocial = ddlModalNombreAportante.SelectedValue

        Dim aportantes As List(Of AportanteDTO) = NegocioAportante.GetListaAportantes(aportante, fechahasta)

        If aportantes.Count = 0 Then
            ddlModalMultifondo.Items.Insert(0, New ListItem("", ""))
        Else
            ddlModalMultifondo.DataSource = aportantes
            ddlModalMultifondo.DataMember = "multifondo"
            ddlModalMultifondo.DataValueField = "multifondo"
            ddlModalMultifondo.DataBind()
        End If

    End Sub

#End Region

#Region "Cargar Datos "

    Private Sub CargaFiltroRutAportante()
        Dim lista As List(Of AportanteDTO) = GetAportantesPorRut()

        ddlModalRutAportanteCanje.DataSource = Nothing
        ddlModalRutAportanteCanje.DataBind()

        If lista.Count = 0 Then
            ddlModalRutAportante.Items.Insert(0, New ListItem("", ""))

        Else
            ddlModalRutAportanteCanje.DataSource = lista
            ddlModalRutAportanteCanje.DataMember = "Rut"
            ddlModalRutAportanteCanje.DataValueField = "Rut"
            ddlModalRutAportanteCanje.DataBind()
            ddlModalRutAportanteCanje.Items.Insert(0, New ListItem("", ""))
        End If
    End Sub

    Private Sub CargarTipoTransaccion()
        Dim listTipoTransaccion As List(Of FijacionDTO) = GetFijacionTipoTransacion()
        Dim Fijacionvacia As New FijacionDTO

        ddlListaTipoTransaccion.DataSource = Nothing
        ddlListaTipoTransaccion.DataBind()

        If listTipoTransaccion.Count = 0 Then
            listTipoTransaccion.Add(Fijacionvacia)
            ddlListaTipoTransaccion.Items.Insert(0, New ListItem(0, String.Empty))

        Else
            listTipoTransaccion.Insert(0, Fijacionvacia)
            ddlListaTipoTransaccion.DataSource = listTipoTransaccion
            ddlListaTipoTransaccion.DataMember = "TipoTransaccionBusqueda"
            ddlListaTipoTransaccion.DataValueField = "TipoTransaccionBusqueda"
            ddlListaTipoTransaccion.DataBind()
            ddlListaTipoTransaccion.Items.Insert(0, New ListItem(0, String.Empty))
        End If
    End Sub

    Private Sub CargaFijacionNav()
        Dim listFijacionNav As List(Of FijacionDTO) = GetFijacionNav()
        Dim Fijacionvacia As New FijacionDTO

        ddlFijacionNav.DataSource = Nothing
        ddlFijacionNav.DataBind()


        If listFijacionNav.Count = 0 Then
            listFijacionNav.Add(Fijacionvacia)
            ddlFijacionNav.Items.Insert(0, New ListItem(0, String.Empty))

        Else

            listFijacionNav.Insert(0, Fijacionvacia)
            ddlFijacionNav.DataSource = listFijacionNav
            ddlFijacionNav.DataMember = "FijacionNAVBusqueda"
            ddlFijacionNav.DataValueField = "FijacionNAVBusqueda"
            ddlFijacionNav.DataBind()
            ddlFijacionNav.Items.Insert(0, New ListItem(0, String.Empty))
        End If
    End Sub

    Private Sub CargaFijacionTC()
        Dim listFijacionTC As List(Of FijacionDTO) = GetFijacionTC()
        Dim Fijacionvacia As New FijacionDTO

        ddlFijacionTCObservacion.DataSource = Nothing
        ddlFijacionTCObservacion.DataBind()

        If listFijacionTC.Count = 0 Then
            listFijacionTC.Add(Fijacionvacia)
            ddlFijacionTCObservacion.Items.Insert(0, New ListItem(0, String.Empty))
        Else
            listFijacionTC.Insert(0, Fijacionvacia)
            ddlFijacionTCObservacion.DataSource = listFijacionTC
            ddlFijacionTCObservacion.DataMember = "FijacionTCObservadoBusqueda"
            ddlFijacionTCObservacion.DataValueField = "FijacionTCObservadoBusqueda"
            ddlFijacionTCObservacion.DataBind()
            ddlFijacionTCObservacion.Items.Insert(0, New ListItem(0, String.Empty))
        End If
    End Sub

    Private Function GetAportantesPorRut() As List(Of AportanteDTO)
        Dim aportante As New AportanteDTO()
        Dim NegocioA As New AportanteNegocio()
        aportante.Rut = ""
        Return NegocioA.GetListaAportantesPorRut(aportante)
    End Function

    Private Function GetFijacionTipoTransacion() As List(Of FijacionDTO)
        Dim NegocioF As New FijacionNegocio()
        Return NegocioF.ConsultarTipoTransacion()
    End Function

    Private Function GetFijacionNav() As List(Of FijacionDTO)
        Dim NegocioF As New FijacionNegocio()
        Return NegocioF.ConsultarFijacionNav()
    End Function

    Private Function GetFijacionTC() As List(Of FijacionDTO)
        Dim NegocioF As New FijacionNegocio()
        Return NegocioF.ConsultarFijacionTC()
    End Function

    Private Sub CargaFiltroRutFondo()
        Dim fondo As New FondoDTO()
        Dim Negocio As New FondosNegocio()
        Dim fondovacia As New FondoDTO


        Dim fijacion As New FijacionDTO()
        Dim NegocioFijacion As New FijacionNegocio()
        Dim fijacionvacia As New FijacionDTO

        Dim lista As List(Of FondoDTO) = Negocio.ConsultarPorRut(fondo)
        Dim listaFijacion As List(Of FijacionDTO) = NegocioFijacion.CargarFiltroRutFondo(fijacion)

        If lista.Count = 0 Then
            listaFijacion.Add(fijacionvacia)
            ddlListaRutFondo.Items.Insert(0, New ListItem(0, String.Empty))

            lista.Add(fondovacia)
            ddlFondo.Items.Insert(0, New ListItem(0, String.Empty))

        Else
            listaFijacion.Insert(0, fijacionvacia)
            ddlListaRutFondo.DataSource = listaFijacion
            ddlListaRutFondo.DataMember = "RutBusqueda"
            ddlListaRutFondo.DataValueField = "RutBusqueda"
            ddlListaRutFondo.DataBind()
            ddlListaRutFondo.Items.Insert(0, New ListItem(0, String.Empty))
            lista.Insert(0, fondovacia)
            ddlFondo.DataSource = lista
            ddlFondo.DataMember = "Rut"
            ddlFondo.DataValueField = "Rut"
            ddlFondo.DataBind()
            ddlFondo.Items.Insert(0, New ListItem(0, String.Empty))
        End If
    End Sub

    Protected Sub CargaFiltroNombreAportante()
        Dim Suscripcion As New SuscripcionDTO()
        Dim Negocio As New SuscripcionNegocio()
        If (ddlModalMultifondoCanje.Text = "") Then
            Suscripcion.Multifondo = Nothing
        Else
            Suscripcion.Multifondo = ddlModalMultifondoCanje.Text
        End If
        Dim lista As List(Of SuscripcionDTO) = Negocio.ConsultarPorRazonSocial(Suscripcion)

        If lista.Count = 0 Then
            ddlModalNombreAportanteCanje.Items.Insert(0, New ListItem("", ""))
        Else
            ddlModalNombreAportanteCanje.DataSource = lista
            ddlModalNombreAportanteCanje.DataMember = "RazonSocial"
            ddlModalNombreAportanteCanje.DataValueField = "RazonSocial"
            ddlModalNombreAportanteCanje.DataBind()
            ddlModalNombreAportanteCanje.Items.Insert(0, New ListItem("", ""))
        End If
    End Sub

    Protected Sub CargaFiltroMultifondoAportante1()
        Dim Suscripcion As New SuscripcionDTO()
        Dim Negocio As New SuscripcionNegocio()
        Suscripcion.RazonSocial = ddlModalNombreAportanteCanje.SelectedValue
        Dim lista As List(Of SuscripcionDTO) = Negocio.ConsultarPorMultifondo(Suscripcion)
        If lista.Count = 0 Then
            ddlModalMultifondoCanje.Items.Insert(0, New ListItem("", ""))
        Else
            ddlModalMultifondoCanje.DataSource = lista
            ddlModalMultifondoCanje.DataMember = "Multifondo"
            ddlModalMultifondoCanje.DataValueField = "Multifondo"
            ddlModalMultifondoCanje.DataBind()
        End If
    End Sub
#End Region

#Region "Funciones"
    Public Sub CargarMontoModal()
        Dim MonedaSerie As String
        Dim Nav As Decimal
        Dim NavCLP As Decimal
        Dim TCObservado As Decimal
        'Dim Monto As Decimal
        'Dim MontoCLP As Decimal
        Dim Cuotas As Decimal

        MonedaSerie = txtModalMonedaSerie.Text
        Nav = txtModalNAV.Text
        NavCLP = txtModalNAV_CLP.Text
        TCObservado = txtModalTCObservado.Text
        Cuotas = txtModalCuota.Text


        txtModalNAV_CLP.Text = Utiles.calcularNAVCLP(TCObservado, Nav) ' String.Format("{0:N4}", Double.Parse(txtModalNAV.Text))

        txtModalMonto.Text = Utiles.calcularMonto(Cuotas, Nav, MonedaSerie) ' String.Format("{0:N0}", Monto)
        txtModalMontoCLP.Text = Utiles.calcularMontoCLP(Cuotas, Nav, TCObservado)  ' String.Format("{0:N0}", Double.Parse(MontoCLP))


        If txtModalCuota.Text = "" Then
            txtModalCuota.Text = "0"
        End If

        If (Double.Parse(txtModalCuota.Text)) > (Double.Parse(txtModalDisponiblesCuotasDisponibles.Text)) Then
            ShowAlert("El número de cuotas debe ser menor o igual a las Cuotas Disponibles")
            txtModalCuota.Text = "0"
            Return
        End If

        If (Double.Parse(txtModalMonto.Text)) > (Double.Parse(txtModalDisponibles.Text)) Then
            ShowAlert("El Valor del Monto debe ser menor o igual a el Patrimonio Disponible")
            txtModalMonto.Text = "0"
            txtModalMontoCLP.Text = "0"
            Return
        End If

        Dim NegocioTipoCambio As TipoCambioNegocio = New TipoCambioNegocio
        Dim NegocioTipoCalculoNav As TipoCalculoNav = New TipoCalculoNav
        Dim TipoCambio As TipoCambioDTO = New TipoCambioDTO()
        Dim TipoCambioActualizado As TipoCambioDTO = New TipoCambioDTO()
        Dim TipoCalculoNav As TipoCalculoNavDTO = New TipoCalculoNavDTO()

        If txtIDRescate.Text() <> Nothing Then
            TipoCalculoNav.ID = txtIDRescate.Text()
            TipoCalculoNav.TipoTransaccion = "Rescate"
            TipoCalculoNav.TipoCalculo = "C"
        End If

        Session("TipodeFijacion") = "NAV"

        Dim updateCNJ_Tipo_CalculoNAV = NegocioTipoCalculoNav.UpdateTipoCalculoNav(TipoCalculoNav)

    End Sub

    Private Sub FindFijacion()
        Dim Fijacion As FijacionDTO = New FijacionDTO()
        Dim negocio As FijacionNegocio = New FijacionNegocio
        Dim FechaTCDesde As Nullable(Of Date)
        Dim FechaTCHasta As Nullable(Of Date)
        Dim FechaNAVDesde As Nullable(Of Date)
        Dim FechaNAVHasta As Nullable(Of Date)

        Dim FechaPagoDesde As Nullable(Of Date)
        Dim FechaPagoHasta As Nullable(Of Date)

        txtNAVDesde.Text = Request.Form(txtNAVDesde.UniqueID)
        txtNAVHasta.Text = Request.Form(txtNAVHasta.UniqueID)
        txtFechaTCDesde.Text = Request.Form(txtFechaTCDesde.UniqueID)
        txtFechaTCHasta.Text = Request.Form(txtFechaTCHasta.UniqueID)

        txtFechaPagoDesde.Text = Request.Form(txtFechaPagoDesde.UniqueID)
        txtFechaPagoHasta.Text = Request.Form(txtFechaPagoHasta.UniqueID)

        If ddlListaTipoTransaccion.SelectedValue.Trim() = Nothing Then
            Fijacion.TipoTransaccion = Nothing
        Else
            Fijacion.TipoTransaccion = ddlListaTipoTransaccion.SelectedValue.Trim()
        End If

        If ddlListaRutFondo.SelectedValue.Trim() = Nothing Then
            Fijacion.Rut = Nothing
            Fijacion.FnNombreCorto = Nothing
        Else
            Dim arrCadena As String() = ddlListaRutFondo.SelectedItem.Text().Split(New Char() {"/"c})

            Fijacion.Rut = arrCadena(0).Trim()
            Fijacion.FnNombreCorto = arrCadena(1).Trim()
        End If

        If ddlListaNemotecnico.SelectedValue.Trim() = Nothing Then
            Fijacion.Nemotecnico = Nothing
        Else
            Fijacion.Nemotecnico = ddlListaNemotecnico.SelectedValue.Trim()
        End If

        If ddlFijacionNav.SelectedValue.Trim() = Nothing Then
            Fijacion.FijacionNAV = Nothing
        Else
            Fijacion.FijacionNAV = ddlFijacionNav.SelectedValue.Trim()
        End If

        If ddlFijacionTCObservacion.SelectedValue.Trim() = Nothing Then
            Fijacion.FijacionTCObservado = Nothing
        Else
            Fijacion.FijacionTCObservado = ddlFijacionTCObservacion.SelectedValue.Trim()
        End If

        If Request.Form(txtNAVDesde.UniqueID).Equals("") Then
            FechaNAVDesde = Nothing
        Else
            FechaNAVDesde = Date.Parse(Request.Form(txtNAVDesde.UniqueID))
        End If

        If Request.Form(txtNAVHasta.UniqueID).Equals("") Then
            FechaNAVHasta = Nothing
        Else
            FechaNAVHasta = Date.Parse(Request.Form(txtNAVHasta.UniqueID))
        End If

        If Request.Form(txtFechaTCDesde.UniqueID).Equals("") Then
            FechaTCDesde = Nothing
        Else
            FechaTCDesde = Date.Parse(Request.Form(txtFechaTCDesde.UniqueID))
        End If

        If Request.Form(txtFechaTCHasta.UniqueID).Equals("") Then
            FechaTCHasta = Nothing
        Else
            FechaTCHasta = Date.Parse(Request.Form(txtFechaTCHasta.UniqueID))
        End If

        If Request.Form(txtFechaPagoDesde.UniqueID).Equals("") Then
            FechaPagoDesde = Nothing
        Else
            FechaPagoDesde = Date.Parse(Request.Form(txtFechaPagoDesde.UniqueID))
        End If

        If Request.Form(txtFechaPagoHasta.UniqueID).Equals("") Then
            FechaPagoHasta = Nothing
        Else
            FechaPagoHasta = Date.Parse(Request.Form(txtFechaPagoHasta.UniqueID))
        End If

        If ddlEstadoConfirmacion.Text.Trim() = "" Then
            Fijacion.EstadoIntencion = Nothing
        Else
            Fijacion.EstadoIntencion = ddlEstadoConfirmacion.SelectedValue
        End If

        GrvTabla.DataSource = Nothing

        If ddlFijacionTCObservacion.SelectedValue.Trim() = Nothing And _
            ddlListaTipoTransaccion.SelectedValue.Trim() = Nothing And _
            ddlListaRutFondo.SelectedValue.Trim() = Nothing And _
            ddlListaNemotecnico.SelectedValue.Trim() = Nothing And _
            ddlFijacionNav.SelectedValue.Trim() = Nothing And _
            Request.Form(txtNAVDesde.UniqueID).Equals("") And _
            Request.Form(txtNAVHasta.UniqueID).Equals("") And _
            Request.Form(txtFechaTCDesde.UniqueID).Equals("") And _
            Request.Form(txtFechaTCHasta.UniqueID).Equals("") And _
            Request.Form(txtFechaPagoDesde.UniqueID).Equals("") And _
            Request.Form(txtFechaPagoHasta.UniqueID).Equals("") And _
            ddlEstadoConfirmacion.Text = "" Then

            GrvTabla.DataSource = negocio.ConsultarTodos(Fijacion)

        Else
            GrvTabla.DataSource = negocio.ConsultarFiltro(Fijacion, FechaNAVDesde, FechaNAVHasta, FechaTCDesde, FechaTCHasta, FechaPagoDesde, FechaPagoHasta)
        End If
        GrvTabla.DataBind()
    End Sub

    Private Sub CargaNemotecnico()
        'ddlListaNemotecnico.Items.Clear()
        ddlListaNemotecnico.DataSource = Nothing
        ddlListaNemotecnico.DataBind()

        Dim nemotecnico As New FondoSerieDTO
        Dim Negocio As New FondoSeriesNegocio()
        Dim neg As New FijacionNegocio
        Dim FondoSerievacia As New FondoSerieDTO
        Dim lista As List(Of FondoSerieDTO) = Negocio.GetListaFondoSerieporNemotecnico(nemotecnico)
        Dim nemo As List(Of FondoSerieDTO) = neg.Nemotecnico

        If lista.Count = 0 Then
            lista.Add(FondoSerievacia)
            ddlListaNemotecnico.Items.Insert(0, New ListItem(0, String.Empty))
            ddlNemotecnico.Items.Insert(0, New ListItem("", ""))
        Else
            lista.Insert(0, FondoSerievacia)
            nemo.Insert(0, FondoSerievacia)
            ddlListaNemotecnico.DataSource = nemo
            ddlListaNemotecnico.DataMember = "NemotecnicoBusqueda"
            ddlListaNemotecnico.DataValueField = "NemotecnicoBusqueda"
            ddlListaNemotecnico.DataBind()
            ddlListaNemotecnico.Items.Insert(0, New ListItem("", ""))
            ddlNemotecnico.DataSource = lista
            ddlNemotecnico.DataMember = "Nemotecnico"
            ddlNemotecnico.DataValueField = "Nemotecnico"
            ddlNemotecnico.DataBind()
            ddlNemotecnico.Items.Insert(0, New ListItem("", ""))
        End If
    End Sub

    Private Sub ShowAlert(mesagge As String)
        Dim myScript As String = "alert('" + mesagge + "');"
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", myScript, True)
    End Sub

    Protected Sub RowSelector_CheckedChanged(sender As Object, e As EventArgs)
        For Each row As GridViewRow In GrvTabla.Rows
            Dim chk As RadioButton = row.Cells(0).Controls(1)
            If chk IsNot Nothing And chk.Checked Then
            End If
        Next
    End Sub

    Private Function GetFijacionesSelect() As FijacionDTO
        Dim Fijacion As New FijacionDTO
        For Each row As GridViewRow In GrvTabla.Rows
            Dim chk As CheckBox = row.Cells(0).Controls(1)
            If chk IsNot Nothing And chk.Checked Then
                Fijacion.ID = row.Cells(CONST_COL_ID).Text().Trim()
                Fijacion.TipoTransaccion = row.Cells(CONST_COL_TIPOTRANSACCION).Text().Trim()
            End If
        Next
        Return Fijacion
    End Function

    Private Function GetSuscripcionModal() As SuscripcionDTO
        Dim Suscripcion As SuscripcionDTO = New SuscripcionDTO()

        Suscripcion.IdSuscripcion = txtIdSuscripcion.Text
        Suscripcion.FechaIntencion = txtFechaIntencion.Text
        Suscripcion.RutAportante = ddlModalRutAportante.Text
        Suscripcion.RazonSocial = ddlModalNombreAportante.Text
        Suscripcion.Multifondo = ddlModalMultifondo.Text
        Suscripcion.RutFondo = ddlFondo.Text
        Suscripcion.Nemotecnico = ddlNemotecnico.Text
        Suscripcion.CuotasASuscribir = txtCuotas.Text
        Suscripcion.Moneda_Pago = ddlMonedaPago.Text
        Suscripcion.FechaNAV = txtFechaNAV.Text
        Suscripcion.FechaSuscripcion = txtFechaSuscripcion.Text
        Suscripcion.FechaTC = txtFechaTC.Text
        Suscripcion.NAV = txtNAV.Text
        Suscripcion.Monto = txtMonto.Text
        Suscripcion.NAVCLP = txtNAVCLP.Text
        Suscripcion.MontoCLP = txtMontoCLP.Text
        Suscripcion.ContratoFondo = ddlContrato.Text
        Suscripcion.RevisionPoderes = ddlPoderes.Text
        Suscripcion.Observaciones = txtObservaciones.Text
        Suscripcion.FechaDCV = txtFechaDCV.Text
        Suscripcion.CuotasDCV = txtCuotasDCV.Text
        Suscripcion.RescatesTransitos = txtRescates.Text
        Suscripcion.SuscripcionesTransito = txtSuscripciones.Text
        Suscripcion.CanjesTransito = txtCanje.Text
        Suscripcion.CuotasDisponibles = txtDisponibles.Text
        Suscripcion.FijacionNAV = txtFijacionNAV.Text
        Suscripcion.TcObservado = txtTCObservado.Text
        Suscripcion.FijacionTC = txtFijacionTCObs.Text
        Suscripcion.EstadoSuscripcion = ddlEstado1.Text
        Suscripcion.Estado = "1"

        Return Suscripcion
    End Function

    Public Sub CargarNAV_CLP_Rescates()
        If txtModalMonedaSerie.Text = "CLP" Then
            If txtModalTCObservado.Text <> "" And txtModalNAV.Text <> "" Then
                txtModalNAV_CLP.Text = Utiles.calcularNAVCLP(txtModalTCObservado.Text, txtModalNAV.Text)  ' String.Format("{0:N4}", Double.Parse(txtModalNAV.Text))
                txtModalMontoCLP.Text = Utiles.calcularMontoCLP(txtModalCuota.Text, txtModalNAV.Text, txtModalTCObservado.Text)  ' String.Format("{0:N0}", Double.Parse(txtModalCuota.Text) * Double.Parse(txtModalNAV_CLP.Text))
            End If
        Else
            If txtModalTCObservado.Text <> "" And txtModalNAV.Text <> "" Then
                txtModalNAV_CLP.Text = Utiles.calcularNAVCLP(txtModalTCObservado.Text, txtModalNAV.Text)  ' String.Format("{0:N4}", Double.Parse(txtModalTCObservado.Text) * Double.Parse(txtModalNAV.Text))
                txtModalMontoCLP.Text = Utiles.calcularMontoCLP(txtModalCuota.Text, txtModalNAV.Text, txtModalTCObservado.Text)  ' String.Format("{0:N2}", txtModalCuota.Text * txtModalNAV_CLP.Text)
            End If
        End If
        Session("TipodeFijacion") = "TC"
    End Sub

    Private Sub ShowMessages(tittle As String, message As String, urlconTittle As String, urlconMessage As String, Optional borralink As Boolean = True)
        lblModalTitle.Text = tittle
        lblModalBody.Text = message
        img_modal.ImageUrl = urlconTittle
        img_body_modal.ImageUrl = urlconMessage
        Archivo.Visible = (Not borralink)
    End Sub
#End Region

#Region "Consultar según nemotécnico"
    Private Sub ConsultarFechaNav()
        Dim serie As FondoSerieDTO = New FondoSerieDTO
        Dim negocioSerie As FondoSeriesNegocio = New FondoSeriesNegocio
        serie.Nemotecnico = ddlNemotecnico.SelectedValue
        Dim listaSerie As List(Of FondoSerieDTO) = negocioSerie.GrupoSeriesPorNemotecnico(serie)
        Dim FechaNavSuscripcion As String
        For Each series As FondoSerieDTO In listaSerie
            Dim estructuraFechas As EstructuraFechasDto = New EstructuraFechasDto
            estructuraFechas = Utiles.splitCharByComma(series.FechaNavSuscripcion)

            FechaNavSuscripcion = series.FechaNavSuscripcion
            Dim fechaNavC As String
            Dim diasNavC As String
            fechaNavC = estructuraFechas.DesdeQueFecha
            diasNavC = estructuraFechas.DiasASumar

            Dim Suscripcion As SuscripcionDTO = New SuscripcionDTO
            If diasNavC = "" Then
                Suscripcion.FechaSuscripcion = txtFechaSuscripcion.Text
                Dim FechaSolicitud As Date
                Dim testString As String = FormatDateTime(FechaSolicitud, DateFormat.LongDate)
                FechaSolicitud = Suscripcion.FechaSuscripcion
                txtFechaNAV.Text = FechaSolicitud
                Calendar7.SelectedDate = FechaSolicitud
            Else
                Dim dias As Integer
                dias = Integer.Parse(diasNavC)
                Suscripcion.FechaSuscripcion = txtFechaSuscripcion.Text
                Dim fechaSolicitud As Date
                Dim testString As String = FormatDateTime(fechaSolicitud, DateFormat.LongDate)
                fechaSolicitud = Suscripcion.FechaSuscripcion
                Dim dia As Integer
                Dim mes As Integer
                Dim año As Integer
                dia = Day(fechaSolicitud)
                mes = Month(fechaSolicitud)
                año = Year(fechaSolicitud)
                Dim suma = dia + dias
                fechaSolicitud = fechaSolicitud.AddDays(dias)
                txtFechaNAV.Text = fechaSolicitud
                Calendar7.SelectedDate = fechaSolicitud
            End If
        Next
    End Sub

    Public Sub ConsultarFechaObservado()
        Dim serie As FondoSerieDTO = New FondoSerieDTO
        Dim negocioSerie As FondoSeriesNegocio = New FondoSeriesNegocio
        serie.Nemotecnico = ddlNemotecnico.SelectedValue
        Dim listaSerie As List(Of FondoSerieDTO) = negocioSerie.GrupoSeriesPorNemotecnico(serie)
        Dim FechaNavSuscripcion As String
        For Each series As FondoSerieDTO In listaSerie
            Dim estructuraFechas As EstructuraFechasDto = New EstructuraFechasDto
            estructuraFechas = Utiles.splitCharByComma(series.FechaTCSuscripcion)
            FechaNavSuscripcion = series.FechaTCSuscripcion
            Dim fechaNavC As String
            Dim diasNavC As String
            fechaNavC = estructuraFechas.DesdeQueFecha
            diasNavC = estructuraFechas.DiasASumar

            Dim Suscripcion As SuscripcionDTO = New SuscripcionDTO
            If (FechaNavSuscripcion = ",") Then
                txtFechaTC.Text = txtFechaSuscripcion.Text
                Calendar8.SelectedDate = Calendar10.SelectedDate
            Else
                If diasNavC = "" Then
                    Suscripcion.FechaSuscripcion = txtFechaSuscripcion.Text
                    Dim FechaSolicitud As Date
                    Dim testString As String = FormatDateTime(FechaSolicitud, DateFormat.LongDate)
                    FechaSolicitud = Suscripcion.FechaIntencion
                    txtFechaTC.Text = FechaSolicitud
                    Calendar8.SelectedDate = FechaSolicitud
                Else
                    Dim dias As Integer
                    dias = Integer.Parse(diasNavC)
                    Suscripcion.FechaSuscripcion = txtFechaSuscripcion.Text
                    Dim fechaSolicitud As Date
                    Dim testString As String = FormatDateTime(fechaSolicitud, DateFormat.LongDate)
                    fechaSolicitud = Suscripcion.FechaSuscripcion
                    Dim dia As Integer
                    Dim mes As Integer
                    Dim año As Integer
                    dia = Day(fechaSolicitud)
                    mes = Month(fechaSolicitud)
                    año = Year(fechaSolicitud)
                    Dim suma = dia + dias
                    fechaSolicitud = fechaSolicitud.AddDays(dias)
                    txtFechaTC.Text = fechaSolicitud
                    Calendar8.SelectedDate = fechaSolicitud
                End If
            End If

        Next
    End Sub

    Public Sub ConsultarFechaSuscripcipn()
        Dim serie As FondoSerieDTO = New FondoSerieDTO
        Dim NegocioRescate As RescateNegocio = New RescateNegocio
        Dim negocioSerie As FondoSeriesNegocio = New FondoSeriesNegocio
        serie.Nemotecnico = ddlNemotecnico.SelectedValue
        Dim listaSerie As List(Of FondoSerieDTO) = negocioSerie.GrupoSeriesPorNemotecnico(serie)
        Dim FechaNavSuscripcion As String
        For Each series As FondoSerieDTO In listaSerie
            Dim estructuraFechas As EstructuraFechasDto = New EstructuraFechasDto
            estructuraFechas = Utiles.splitCharByComma(series.FechaSuscripcion)
            FechaNavSuscripcion = series.FechaSuscripcion
            Dim fechaNavC As String
            Dim diasNavC As String
            fechaNavC = estructuraFechas.DesdeQueFecha
            diasNavC = estructuraFechas.DiasASumar

            Dim Suscripcion As SuscripcionDTO = New SuscripcionDTO
            If diasNavC = "" Then
                Suscripcion.FechaIntencion = txtFechaIntencion.Text
                Dim FechaSolicitud As Date
                Dim testString As String = FormatDateTime(FechaSolicitud, DateFormat.LongDate)
                FechaSolicitud = Suscripcion.FechaIntencion
                txtFechaSuscripcion.Text = FechaSolicitud
                Calendar10.SelectedDate = FechaSolicitud
            Else
                Dim dias As Integer = Integer.Parse(diasNavC)

                Dim fechaSolicitud As Date
                Dim testString As String = FormatDateTime(fechaSolicitud, DateFormat.LongDate)

                Suscripcion.FechaIntencion = txtFechaIntencion.Text
                fechaSolicitud = Suscripcion.FechaIntencion

                'FechaPagoFondoRescatableINT es días que hay que sumar o restar, FechaCalculo es a la fecha a la que hay que sumar o restar
                'FECHA PAGO DIAS HABILES
                'fechaSolicitud = NegocioRescate.SelectFechaPagoSIRescatable(dias, fechaSolicitud, 0)
                fechaSolicitud = Utiles.SumaDiasAFechas(ddlMonedaPago.Text, fechaSolicitud, dias, 0)

                txtFechaSuscripcion.Text = fechaSolicitud
                Calendar10.SelectedDate = fechaSolicitud
            End If
        Next
    End Sub

    Public Sub CalcularValor()
        Dim valor As VcSerieDTO = New VcSerieDTO
        Dim negocioValor As ValoresCuotaNegocio = New ValoresCuotaNegocio
        valor.FsNemotecnico = ddlNemotecnico.SelectedValue
        Dim listaValores As List(Of VcSerieDTO) = negocioValor.ValoresCuotaPorNemotecnicoYFecha(valor)
    End Sub

#End Region

#Region "VALORES EN TRÁNSITO"
    Public Sub RescateTransito()
        Dim rescate As RescatesDTO = New RescatesDTO
        Dim negocio As RescateNegocio = New RescateNegocio
        rescate.AP_RUT = ddlModalRutAportante.SelectedValue
        rescate.FS_Nemotecnico = ddlNemotecnico.SelectedValue
        rescate.RES_Fecha_Solicitud = txtFechaIntencion.Text
        Dim listaRescates As List(Of RescatesDTO) = negocio.ConsultarTransito(rescate)
        Dim retornar As Integer
        If listaRescates.Count = 0 Then
            retornar = 0
            txtRescates.Text = retornar
        Else
            For Each res As RescatesDTO In listaRescates
                retornar = res.RES_Cuotas
                txtRescates.Text = retornar
            Next
        End If

    End Sub

    Public Sub SuscripcionTransito()
        Dim sus As SuscripcionDTO = New SuscripcionDTO
        Dim negocio As SuscripcionNegocio = New SuscripcionNegocio
        sus.RutAportante = ddlModalRutAportante.SelectedValue
        sus.Nemotecnico = ddlNemotecnico.SelectedValue
        sus.FechaIntencion = txtFechaIntencion.Text
        Dim listaSus As List(Of SuscripcionDTO) = negocio.ConsultarTransito(sus)
        Dim retornar As Integer
        If listaSus.Count = 0 Then
            retornar = 0
            txtSuscripciones.Text = retornar
        Else
            For Each su As SuscripcionDTO In listaSus
                retornar = su.CuotasASuscribir
                txtSuscripciones.Text = retornar
            Next
        End If
    End Sub

    Public Sub CanjeTransito()
        Dim can As CanjeDTO = New CanjeDTO
        Dim negocio As CanjeNegocio = New CanjeNegocio
        can.RutAportante = ddlModalRutAportante.SelectedValue
        can.NemotecnicoSaliente = ddlNemotecnico.SelectedValue
        If txtFechaSuscripcion.Text.Equals("") Then
            can.FechaSolicitud = Nothing
        Else
            can.FechaSolicitud = txtFechaSuscripcion.Text
        End If


        Dim lisEncontrados As List(Of CanjeDTO) = negocio.EncontrarNemotecnicoSaliente(can)
        Dim retornar As Integer
        If lisEncontrados.Count = 0 Then
            retornar = 0
            txtCanje.Text = retornar
        Else
            For Each reco As CanjeDTO In lisEncontrados
                If can.NemotecnicoSaliente.Equals(reco.NemotecnicoSaliente) Then
                    Dim devolver As Decimal
                    Dim cuotas As Decimal
                    devolver = can.CuotaSaliente
                    cuotas = reco.CuotaSaliente
                    Dim restar = cuotas - devolver
                    txtCanje.Text = restar
                    can.Cuotas = txtCuotasDCV.Text
                    can.RescateTransito = txtRescates.Text
                    can.SuscripcionTransito = txtSuscripciones.Text
                    Dim dcv As Decimal
                    dcv = can.Cuotas
                    Dim rescate As Decimal
                    rescate = can.RescateTransito
                    Dim sus As Integer
                    sus = can.SuscripcionTransito
                    Dim disponible = dcv - rescate + sus - restar
                    txtDisponibles.Text = disponible
                Else
                    Dim cuotasTraer As Decimal
                    Dim cuotasIngresadas As Decimal
                    cuotasTraer = can.CuotaEntrante
                    cuotasIngresadas = reco.CuotaEntrante
                    Dim sumar = cuotasIngresadas + cuotasTraer
                    txtCanje.Text = sumar
                    Dim dcv As Decimal
                    dcv = can.Cuotas
                    Dim rescate As Decimal
                    rescate = can.RescateTransito
                    Dim sus As Integer
                    sus = can.SuscripcionTransito
                    Dim disponible = dcv - rescate + sus + sumar
                    txtDisponibles.Text = disponible
                End If
            Next
        End If
    End Sub
#End Region

    Public Sub CargarCuotasDCV()

        Dim dcv As ADCVDTO = New ADCVDTO
        Dim negocioDCV As ADCVNegocio = New ADCVNegocio
        dcv.AP_RUT = ddlModalRutAportante.SelectedValue
        dcv.ADCV_Razon_Social = ddlModalNombreAportante.SelectedValue
        dcv.FS_Nemotecnico = ddlNemotecnico.SelectedValue
        dcv.ADCV_Fecha = txtFechaDCV.Text
        Dim listaDCV As List(Of ADCVDTO) = negocioDCV.ConsultaDCV(dcv)
        Dim cuotaDCV As Double

        If listaDCV.Count = 0 Then
            Dim listaUltimo As List(Of ADCVDTO) = negocioDCV.UltimoDCV(dcv)
            If listaUltimo.Count = 0 Then
                cuotaDCV = 0
                txtCuotasDCV.Text = cuotaDCV
                txtFechaDCV.Text = dcv.ADCV_Fecha
            End If
            For Each dvcs As ADCVDTO In listaUltimo
                Dim fecha As Date
                fecha = dvcs.ADCV_Fecha
                cuotaDCV = dvcs.ADCV_Cantidad
                txtFechaDCV.Text = fecha
                txtCuotasDCV.Text = cuotaDCV
            Next
        Else
            For Each dvcs As ADCVDTO In listaDCV
                cuotaDCV = dvcs.ADCV_Cantidad
                txtCuotasDCV.Text = cuotaDCV
            Next
        End If

        '----------------CARGA RESCATES ----------------
        RescateTransito()
        '----------------CARGA SUSCRIPCIONES ----------------
        SuscripcionTransito()
        '----------------CARGA CANJES ----------------
        CanjeTransito()

    End Sub

    Private Sub LLenarIdSuscripcion()
        Dim Suscripcion As SuscripcionDTO = New SuscripcionDTO()
        Dim NegocioS As SuscripcionNegocio = New SuscripcionNegocio
        Dim SuscripcionActualizada As SuscripcionDTO = New SuscripcionDTO()

        SuscripcionActualizada = NegocioS.GetUltimaSuscripcion(Suscripcion)

        If SuscripcionActualizada IsNot Nothing Then
            txtIdSuscripcion.Text = SuscripcionActualizada.IdSuscripcion + 1
        End If
    End Sub

    Public Sub CalcularValorEntrante()

        Dim valor As VcSerieDTO = New VcSerieDTO()
        Dim negocioValor As ValoresCuotaNegocio = New ValoresCuotaNegocio
        Dim ValorNuevo As New VcSerieDTO()

        valor.FsNemotecnico = ddlNemotecnico.SelectedValue
        valor.Fecha = txtFechaNAV.Text
        Dim listaValores As List(Of VcSerieDTO) = negocioValor.ValoresCuotaPorNemotecnicoYFecha(valor)
        Dim valorNav As Single
        If listaValores.Count = 0 Then
            Dim listaUltimo As List(Of VcSerieDTO) = negocioValor.UltimoValorCuota(valor)
            If listaUltimo.Count = 0 Then
                txtNAV.Text = 0
                txtNAVCLP.Text = Utiles.formatearNAVCLP(valorNav) ' String.Format("{0:N4}", valorNav)
                txtFijacionNAV.Text = "Pendiente"
            Else
                For Each vcs As VcSerieDTO In listaUltimo
                    valorNav = vcs.Valor
                    If txtModalMonedaSerie.Text.Trim = "USD" Then
                        txtNAV.Text = Utiles.formatearNAV(valorNav) ' String.Format("{0:N4}", valorNav)
                    Else
                        txtNAV.Text = Utiles.formatearNAV(valorNav) ' String.Format("{0:N4}", valorNav)
                    End If
                    txtNAVCLP.Text = Utiles.formatearNAVCLP(valorNav) ' String.Format("{0:N4}", valorNav)
                    txtFijacionNAV.Text = "Pendiente"
                Next

            End If
        Else
            For Each valores As VcSerieDTO In listaValores
                valorNav = valores.Valor
                If txtModalMonedaSerie.Text.Trim = "USD" Then
                    txtNAV.Text = Utiles.formatearNAV(valorNav) ' String.Format("{0:N4}", valorNav)
                Else
                    txtNAV.Text = Utiles.formatearNAV(valorNav) ' String.Format("{0:N4}", valorNav)
                End If
                txtNAVCLP.Text = Utiles.formatearNAVCLP(valorNav) '  String.Format("{0:N4}", valorNav)
                txtFijacionNAV.Text = "Realizado"
            Next
        End If

    End Sub

    Public Sub CargarNombreMonedaSerieModal()
        Dim NegocioFondoSerie As FondoSeriesNegocio = New FondoSeriesNegocio
        Dim NegocioRescate As RescateNegocio = New RescateNegocio
        Dim fondoSerie As FondoSerieDTO = New FondoSerieDTO()
        Dim primerRegistroNemotecnico As String
        primerRegistroNemotecnico = ddlNemotecnico.Text
        fondoSerie.Nemotecnico = primerRegistroNemotecnico


        'CARGA LA FECHA DCV
        Dim ADCV As ADCVDTO = New ADCVDTO()
        Dim NegocioADCV As ADCVNegocio = New ADCVNegocio

        ADCV.AP_RUT = ddlModalRutAportante.SelectedValue
        ADCV.ADCV_Razon_Social = ddlModalNombreAportante.SelectedValue
        ADCV.FS_Nemotecnico = ddlNemotecnico.SelectedValue

        Dim ADCVActualizado As ADCVDTO = NegocioADCV.GetADCV(ADCV)

        If ADCVActualizado Is Nothing Then
            txtFechaDCV.Text = Date.Now.AddDays(-1).ToString("dd/MM/yyyy")
        Else
            txtFechaDCV.Text = ADCVActualizado.ADCV_Fecha.ToString()
        End If

    End Sub

    Protected Sub CargarTCObs()
        'TRAER VALOR TIPO CAMBIO OBSERVADO
        Dim TipoCambio As TipoCambioDTO = New TipoCambioDTO()
        Dim NegocioTipoCambio As TipoCambioNegocio = New TipoCambioNegocio
        Dim TipoCambioActualizado As TipoCambioDTO = New TipoCambioDTO()

        If TipoCambio IsNot Nothing Then
            If txtFechaTC.Text IsNot "" And ddlMonedaPago.Text IsNot "" Then
                TipoCambio.Fecha = txtFechaTC.Text
                TipoCambio.Codigo = ddlMonedaPago.SelectedValue
                TipoCambioActualizado = NegocioTipoCambio.GetTipoCambio(TipoCambio)
                If TipoCambioActualizado.Codigo = "" Then
                    txtFijacionTCObs.Text = "Pendiente"
                    txtTCObservado.Text = "0"
                Else
                    txtTCObservado.Text = String.Format("{0:N6}", TipoCambioActualizado.Valor)
                    txtFijacionTCObs.Text = "Realizado"
                End If
            Else
                txtFijacionTCObs.Text = "Pendiente"
                txtTCObservado.Text = "0"
            End If
        Else
            txtFijacionTCObs.Text = "Pendiente"
            txtTCObservado.Text = "0"
        End If

    End Sub

    Protected Sub Cuotaschanged()
        If (IsNumeric(txtNAV.Text) And IsNumeric(txtCuotas.Text)) Then
            'If (txtMonedaSerie.Text <> "CLP") Then
            txtMonto.Text = Utiles.calcularMonto(txtCuotas.Text, txtNAV.Text, txtMonedaSerie.Text) ' String.Format("{0:N2}", Double.Parse(txtCuotas.Text) * Double.Parse(txtNAV.Text))
            'Else
            'txtMonto.Text = String.Format("{0:N0}", Double.Parse(txtCuotas.Text) * Double.Parse(txtNAV.Text))
            'End If
        End If
        llenarCLP()
    End Sub

    Protected Sub tcrescateschanged()
        validartcrescates()
        If (txtModalTCObservado.Text <> "" And IsNumeric(txtModalTCObservado.Text)) Then
            If (Double.Parse(txtModalTCObservado.Text) < 99999999999999) Then
                CargarNAV_CLP_Rescates()
            Else
                ShowAlert("El valor de tc observado ingresado excede el límite permitido")
                txtModalTCObservado.Text = ""
            End If
        End If
    End Sub

    Public Sub validartcrescates()
        Dim contador As Integer = 0
        If (txtModalTCObservado.Text <> "") Then
            For s = 0 To txtModalTCObservado.Text.Length - 1
                If (txtModalTCObservado.Text.Chars(s) = ",") Then
                    contador = contador + 1
                End If
            Next
            If contador > 1 Or txtModalTCObservado.Text.Chars(0) = "," Then
                ShowAlert("Tc observado no tiene un formato correcto, por favor verifique")
                txtModalTCObservado.Text = ""
            End If
        Else
            txtModalTCObservado.Text = "0"
        End If
    End Sub

    Protected Sub TcObservadoChanged()
        ValidarTCObservado()
        If (txtTCObservado.Text <> "" And IsNumeric(txtTCObservado.Text)) Then
            If (Double.Parse(txtTCObservado.Text) < 99999999999999) Then
                llenarCLP()
            Else
                ShowAlert("El valor de tc observado ingresado excede el límite permitido")
                txtTCObservado.Text = ""
            End If
        End If
    End Sub

    Public Sub ValidarTCObservado()
        Dim contador As Integer = 0
        If (txtTCObservado.Text <> "") Then
            For s = 0 To txtTCObservado.Text.Length - 1
                If (txtTCObservado.Text.Chars(s) = ",") Then
                    contador = contador + 1
                End If
            Next
            If contador > 1 Or txtTCObservado.Text.Chars(0) = "," Then
                ShowAlert("Tc observado no tiene un formato correcto, por favor verifique")
                txtTCObservado.Text = ""
            End If
        Else
            txtTCObservado.Text = "0"
        End If
    End Sub

    Protected Sub MontoTextChanged()
        Dim NegocioTipoCalculoNav As TipoCalculoNav = New TipoCalculoNav
        Dim TipoCalculoNav As TipoCalculoNavDTO = New TipoCalculoNavDTO()
        If (txtMonto.Text = "") Then
            txtMonto.Text = 0
        End If
        If (IsNumeric(txtMonto.Text)) Then
            If (IsNumeric(txtNAV.Text)) Then
                If (Double.Parse(txtNAV.Text) > 0) Then
                    Dim cuota = (Double.Parse(txtMonto.Text) / Double.Parse(txtNAV.Text))
                    txtCuotas.Text = String.Format("{0:N0}", Math.Floor(cuota))
                    Cuotaschanged()

                    TipoCalculoNav.ID = txtIdSuscripcion.Text()
                    TipoCalculoNav.TipoTransaccion = "Suscripcion"
                    TipoCalculoNav.TipoCalculo = "M"
                    Dim updateCNJ_Tipo_CalculoNAV = NegocioTipoCalculoNav.UpdateTipoCalculoNav(TipoCalculoNav)
                Else
                    txtCuotas.Text = 0
                    txtMonto.Text = 0
                End If
                llenarCLP()
            End If
        End If

        If (txtMonto.Text = 0) Then
            txtCuotas.Text = 0
        End If
    End Sub

    Protected Sub CuotasTextChanged()
        Dim NegocioTipoCalculoNav As TipoCalculoNav = New TipoCalculoNav
        Dim TipoCalculoNav As TipoCalculoNavDTO = New TipoCalculoNavDTO()
        Dim suscripcion As SuscripcionDTO = New SuscripcionDTO()
        If (IsNumeric(txtCuotas.Text) And IsNumeric(txtDisponiblesEmitidas.Text)) Then
            If (Double.Parse(txtCuotas.Text) > Double.Parse(txtDisponiblesEmitidas.Text)) Then
                ShowAlert("El número de cuotas no puede ser mayor que las cuotas disponibles")
                txtCuotas.Text = "0"
            Else
                If (txtCuotas.Text = "") Then
                    txtCuotas.Text = "0"
                End If

                TipoCalculoNav.ID = txtIdSuscripcion.Text()
                TipoCalculoNav.TipoTransaccion = "Suscripcion"
                TipoCalculoNav.TipoCalculo = "C"
                Dim updateCNJ_Tipo_CalculoNAV = NegocioTipoCalculoNav.UpdateTipoCalculoNav(TipoCalculoNav)
                'TODO: CAMBIAR suscripcion.CuotasASuscribir = Integer.Parse(Replace(txtCuotas.Text, ".", ""))

                suscripcion.CuotasASuscribir = Decimal.Parse(Replace(txtCuotas.Text, ".", ""))
            End If
        End If

        Cuotaschanged()
        llenarCLP()
    End Sub

    Protected Sub llenarCLP()
        If (IsNumeric(txtTCObservado.Text) And IsNumeric(txtNAV.Text)) Then
            'txtNAVCLP.Text = Double.Parse(txtNAV.Text.Replace(".", ",")) * Double.Parse(txtTCObservado.Text.Replace(".", ","))
            txtNAVCLP.Text = Utiles.calcularNAVCLP(txtTCObservado.Text, txtNAV.Text) '  String.Format("{0:N4}", txtNAV.Text * txtTCObservado.Text)
        Else
            txtNAVCLP.Text = ""
        End If

        If (IsNumeric(txtCuotas.Text) And IsNumeric(txtNAV.Text) And IsNumeric(txtTCObservado.Text)) Then
            'Dim NavClpAux As Decimal = (Utiles.SetToCapitalizedNumber((Double.Parse(txtNAV.Text.Replace(".", "")) * Double.Parse(txtCuotas.Text.Replace(".", "") * Double.Parse(txtTCObservado.Text.Replace(".", ""))))))
            txtMontoCLP.Text = Utiles.calcularMontoCLP(txtCuotas.Text, txtNAV.Text, txtTCObservado.Text) 'String.Format("{0:N2}", NavClpAux)
        End If
    End Sub

#Region "CARGA CUOTAS TEXTCHANGED CAMPO MONTO RESCATES"
    Public Sub CargarCuotasModalRescate()
        Dim ModalCuota As Decimal
        If txtModalNAV.Text <> "" And txtModalNAV.Text <> "0" And txtModalMonto.Text <> "0" Then

            ModalCuota = txtModalMonto.Text / txtModalNAV.Text
            txtModalCuota.Text = Utiles.SetToCapitalizedNumber(Math.Floor(ModalCuota))

            txtModalMonto.Text = Utiles.calcularMonto(txtModalCuota.Text, txtModalNAV.Text, txtModalMonedaSerie.Text)

        End If

    End Sub
#End Region

    Private Sub btnCancelarModalRescates_Click(sender As Object, e As EventArgs) Handles btnCancelarModalRescates.Click
        txtAccionHidden.Value = ""
    End Sub

    Private Sub btnModalCancelarCanje_Click(sender As Object, e As EventArgs) Handles btnModalCancelarCanje.Click
        txtAccionHidden.Value = ""
    End Sub

    'RECALCULAR VALORES EN CANJES 
    Public Sub CalcularFactor()
        replicarNavCLP()
        replicarNavCLPEntrante()

        If txtModalNavEntrante.Text = "" Or txtModalNavSaliente.Text = "" Then
            txtModalFactor.Text = ""
            CalcularMontoSaliente()
        Else
            Dim navEntrante As Decimal = txtModalNavEntrante.Text
            Dim navSaliente As Decimal = txtModalNavSaliente.Text
            Dim Factor As Decimal

            If navEntrante = 0 Then
                Factor = 0
            Else
                Factor = navSaliente / navEntrante
            End If


            txtFactorSaliente.Text = Utiles.SetToCapitalizedNumber(Factor)
            txtModalFactor.Text = Utiles.SetToCapitalizedNumber(Factor)
            ConversionMoneda()
            ConversionMonedaEntrante()
            CalcularCuotaEntrante()

        End If
        CalcularMontoSaliente()

        If txtModalNavCLPEntrante.Text = "" Then
            txtModalNavCLPEntrante.Text = ""
        Else
            txtModalNavCLPEntrante.Text = Utiles.formatearNAVCLP(txtModalNavCLPEntrante.Text) ' String.Format("{0:N4}", Double.Parse(txtModalNavCLPEntrante.Text))
        End If

        If txtModalNavCLPSaliente.Text = "" Then
            txtModalNavCLPSaliente.Text = ""
        Else
            txtModalNavCLPSaliente.Text = Utiles.formatearNAVCLP(txtModalNavCLPSaliente.Text) ' String.Format("{0:N4}", Double.Parse(txtModalNavCLPSaliente.Text))
        End If

        If txtModalMontoCLPEntrante.Text = "" Then
            txtModalMontoCLPEntrante.Text = ""
        Else
            If ddlModalMonedaEntranteCanje.SelectedValue = "CLP" Then
                txtModalMontoCLPEntrante.Text = Utiles.formatearMontoCLP(txtModalMontoCLPEntrante.Text) ' String.Format("{0:N0}", Double.Parse(txtModalMontoCLPEntrante.Text))
            Else
                txtModalMontoCLPEntrante.Text = Utiles.formatearMontoCLP(txtModalMontoCLPEntrante.Text) '    String.Format("{0:N2}", Double.Parse(txtModalMontoCLPEntrante.Text))
            End If
        End If


        If txtModalMontoCLPSaliente.Text = "" Then
            txtModalMontoCLPSaliente.Text = ""
        Else
            If ddlModalMonedaSalienteCanje.SelectedValue = "CLP" Then
                txtModalMontoCLPSaliente.Text = Utiles.formatearMontoCLP(txtModalMontoCLPSaliente.Text) ' String.Format("{0:N0}", Double.Parse(txtModalMontoCLPSaliente.Text))
            Else
                txtModalMontoCLPSaliente.Text = Utiles.formatearMontoCLP(txtModalMontoCLPSaliente.Text) ' String.Format("{0:N2}", Double.Parse(txtModalMontoCLPSaliente.Text))
            End If
        End If

    End Sub

    Public Sub ConversionMoneda()
        If txtModalTipoCambio.Text = "" Or ddlModalMonedaSalienteCanje.SelectedValue = "" Or txtModalNavSaliente.Text = "" Then
            txtModalNavCLPSaliente.Text = ""
        Else
            Dim canje As CanjeDTO = New CanjeDTO
            canje.MonedaSaliente = ddlModalMonedaSalienteCanje.SelectedValue
            canje.TipoCambio = txtModalTipoCambio.Text
            canje.NavSaliente = txtModalNavSaliente.Text

            Dim saliente As Double = canje.NavSaliente
            Dim cambio As Double = canje.TipoCambio


            txtModalNavCLPSaliente.Text = Utiles.calcularNAVCLP(cambio, saliente) '  String.Format("{0:N4}", saliente * cambio)
            ConversionMonedaEntrante()

        End If
    End Sub

    Public Sub cambioTC()
        validarTipoCambio()
        If (txtModalTipoCambio.Text <> "" And IsNumeric(txtModalTipoCambio.Text)) Then
            If (Double.Parse(txtModalTipoCambio.Text) < 99999999999999) Then
                ConversionMoneda()
            Else
                ShowAlert("El valor de tipo de cambio ingresado excede el límite permitido")
                txtModalTipoCambio.Text = ""
            End If
        End If
    End Sub

    Public Sub validarTipoCambio()
        Dim contador As Integer = 0
        If (txtModalTipoCambio.Text <> "") Then
            For s = 0 To txtModalTipoCambio.Text.Length - 1
                If (txtModalTipoCambio.Text.Chars(s) = ",") Then
                    contador = contador + 1
                End If
            Next
            If contador > 1 Or txtModalTipoCambio.Text.Chars(0) = "," Then
                ShowAlert("Tipo cambio no tiene un formato correcto, por favor verifique")
                txtModalTipoCambio.Text = ""
            End If
        End If
    End Sub

    Public Sub replicarNavCLP()
        Dim canje As CanjeDTO = New CanjeDTO
        canje.MonedaSaliente = ddlModalMonedaSalienteCanje.SelectedValue
        If txtModalNavSaliente.Text = "" Then
            txtModalNavCLPSaliente.Text = ""
        Else
            If ddlModalMonedaSalienteCanje.SelectedValue = "CLP" Then
                canje.NavSaliente = txtModalNavSaliente.Text
                txtModalNavCLPSaliente.Text = Utiles.formatearNAVCLP(txtModalNavSaliente.Text) ' canje.NavSalienteFormat
            End If
        End If
    End Sub

    Public Sub replicarNavCLPEntrante()
        Dim canje As CanjeDTO = New CanjeDTO
        canje.MonedaEntrante = ddlModalMonedaEntranteCanje.SelectedValue
        If txtModalNavEntrante.Text = "" Then
            txtModalNavCLPEntrante.Text = ""
        Else
            If ddlModalMonedaEntranteCanje.SelectedValue = "CLP" Then
                canje.NavEntrante = txtModalNavEntrante.Text
                txtModalNavEntrante.Text = Utiles.formatearNAV(txtModalNavEntrante.Text) ' canje.NavEntranteFormat
                txtModalNavCLPEntrante.Text = Utiles.calcularNAVCLP(txtModalTipoCambio.Text, txtModalNavEntrante.Text) 'canje.NavEntranteFormat
            Else
                ConversionMonedaEntrante()
            End If
        End If
    End Sub

    Public Sub ConversionMonedaEntrante()
        If txtModalTipoCambio.Text = "" Or ddlModalMonedaEntranteCanje.SelectedValue = "" Or txtModalNavEntrante.Text = "" Then
            txtModalNavCLPEntrante.Text = ""
        Else

            txtModalNavCLPEntrante.Text = Utiles.calcularNAVCLP(txtModalTipoCambio.Text, txtModalNavEntrante.Text) ' String.Format("{0:N4}", Double.Parse(entrante * cambio))
        End If
        CalcularMontoEntrante()
        CalcularMontoSaliente()
    End Sub

    Public Sub consultarNavSaliente()
        Dim negocioValor As ValoresCuotaNegocio = New ValoresCuotaNegocio
        Dim valor As VcSerieDTO = New VcSerieDTO()
        valor.FnRut = ddlModalFondoCanje.SelectedValue
        valor.FsNemotecnico = ddlModalNemotecnicoSalienteCanje.SelectedValue
        valor.Fecha = txtModalFechaNavSaliente.Text
        valor.Valor = txtModalNavSaliente.Text
        Dim devolver As VcSerieDTO = negocioValor.GetValoresCuota(valor)

        If devolver Is Nothing Then
            ddlModalFijacionNav.SelectedValue = "Pendiente"
            CalcularFactor()
        Else
            If ddlModalMonedaSalienteCanje.SelectedValue = "CLP" Then
                txtModalNavCLPSaliente.Text = Utiles.formatearNAVCLP(devolver.Valor)  ' String.Format("{0:N4}", devolver.Valor)
                ddlModalFijacionNav.SelectedValue = "Realizado"
                CalcularFactor()
            Else
                Dim nav = txtModalNavSaliente.Text
                Dim tipoCambio = txtModalTipoCambio.Text
                Dim retornar = nav * tipoCambio
                txtModalNavCLPSaliente.Text = Utiles.formatearNAVCLP(retornar)  ' String.Format("{0:N4}", retornar)
                ddlModalFijacionNav.SelectedValue = "Realizado"
                CalcularFactor()
            End If
        End If
    End Sub

    Public Sub CalcularCuotaEntrante()
        Dim canje As CanjeDTO = New CanjeDTO
        If txtModalCuotaSaliente.Text = "" Or txtFactorSaliente.Text = "" Then
            txtModalCuotaEntrante.Text = ""
            CalcularMontoSaliente()
        Else
            canje.CuotaSaliente = txtModalCuotaSaliente.Text
            canje.Factor = txtFactorSaliente.Text
            Dim saliente As Single
            Dim factor As Decimal
            saliente = canje.CuotaSaliente
            factor = canje.Factor
            Dim entrante = saliente * factor
            txtModalCuotaEntrante.Text = Utiles.SetToCapitalizedNumber(Math.Floor(entrante))
            ConversionMonedaEntrante()
            CalcularMontoEntrante()
            CalcularMontoSaliente()
            CalcularDiferencias()
        End If
    End Sub

    Public Sub CalcularMontoEntrante()

        If txtModalCuotaEntrante.Text = "" Or txtModalNavEntrante.Text = "" Or txtModalNavCLPEntrante.Text = "" Then
            txtModalMontoEntrante.Text = ""
            txtModalMontoCLPEntrante.Text = ""

        Else
            If ddlModalMonedaEntranteCanje.SelectedValue <> "CLP" Then
                txtModalMontoEntrante.Text = Utiles.calcularMonto(txtModalCuotaEntrante.Text, txtModalNavEntrante.Text, ddlModalMonedaEntranteCanje.SelectedValue) '  String.Format("{0:N2}", Double.Parse(txtModalNavEntrante.Text) * Double.Parse(txtModalCuotaEntrante.Text))
                txtModalMontoCLPEntrante.Text = Utiles.calcularMontoCLP(txtModalCuotaEntrante.Text, txtModalNavEntrante.Text, txtModalTipoCambio.Text) 'String.Format("{0:N2}", Double.Parse(canje.TipoCambio) * Double.Parse(canje.CuotaEntrante) * Double.Parse(canje.NavEntrante))
                CalcularDiferencias()

            ElseIf ddlModalMonedaEntranteCanje.SelectedValue = "CLP" Then

                txtModalMontoEntrante.Text = Utiles.calcularMonto(txtModalCuotaEntrante.Text, txtModalNavEntrante.Text, ddlModalMonedaEntranteCanje.SelectedValue) ' String.Format("{0:N0}", Double.Parse(txtModalNavEntrante.Text) * Double.Parse(txtModalCuotaEntrante.Text))
                txtModalMontoCLPEntrante.Text = Utiles.calcularMontoCLP(txtModalCuotaEntrante.Text, txtModalNavEntrante.Text, txtModalTipoCambio.Text) 'txtModalMontoEntrante.Text
                CalcularDiferencias()
            End If

        End If
    End Sub

    Public Sub CalcularMontoSaliente()
        If ddlModalMonedaSalienteCanje.SelectedValue = "" Then
            If (IsNumeric(txtModalNavSaliente.Text) And IsNumeric(txtModalCuotaSaliente.Text)) Then
                txtModalMontoSaliente.Text = Utiles.SetToCapitalizedNumber(Double.Parse(txtModalNavSaliente.Text) * Double.Parse(txtModalCuotaSaliente.Text))
            End If
        Else
            Dim canje As CanjeDTO = New CanjeDTO
            If txtModalNavSaliente.Text = "" Or txtModalCuotaSaliente.Text = "" Then
                txtModalMontoCLPSaliente.Text = ""
                txtModalMontoSaliente.Text = ""
            Else

                canje.NavSaliente = txtModalNavSaliente.Text
                canje.CuotaSaliente = txtModalCuotaSaliente.Text
                canje.NavCLPSaliente = IIf(txtModalNavCLPSaliente.Text = "", 0, txtModalNavCLPSaliente.Text)
                canje.TipoCambio = txtModalTipoCambio.Text



                If txtModalNavCLPSaliente.Text = "" Then
                    txtModalMontoCLPSaliente.Text = ""
                Else
                    If (ddlModalMonedaSalienteCanje.SelectedValue = "CLP") Then
                        txtModalMontoSaliente.Text = Utiles.calcularMonto(txtModalCuotaSaliente.Text, txtModalNavSaliente.Text, ddlModalMonedaSalienteCanje.SelectedValue)

                        txtModalMontoCLPSaliente.Text = Utiles.calcularMontoCLP(txtModalCuotaSaliente.Text, txtModalNavSaliente.Text, txtModalTipoCambio.Text)

                    Else
                        txtModalMontoSaliente.Text = Utiles.calcularMonto(txtModalCuotaSaliente.Text, txtModalNavSaliente.Text, ddlModalMonedaSalienteCanje.SelectedValue)

                        txtModalMontoCLPSaliente.Text = Utiles.calcularMontoCLP(txtModalCuotaSaliente.Text, txtModalNavSaliente.Text, txtModalTipoCambio.Text)

                    End If
                End If

                CalcularMontoEntrante()


            End If
        End If
        If (ddlModalMonedaSalienteCanje.SelectedValue = "CLP" And IsNumeric(txtModalMontoSaliente.Text)) Then
            txtModalMontoSaliente.Text = Utiles.calcularMonto(txtModalCuotaSaliente.Text, txtModalNavSaliente.Text, ddlModalMonedaSalienteCanje.SelectedValue) 'String.Format("{0:N0}", Double.Parse(txtModalMontoSaliente.Text))
            txtModalMontoCLPSaliente.Text = Utiles.calcularMontoCLP(txtModalCuotaSaliente.Text, txtModalNavSaliente.Text, txtModalTipoCambio.Text) ' txtModalMontoSaliente.Text
        End If
    End Sub

    Public Sub CalcularDiferencias()
        Dim canje As CanjeDTO = New CanjeDTO
        If txtModalMontoEntrante.Text = "" Or txtModalMontoSaliente.Text = "" Or txtModalMontoCLPSaliente.Text = "" Or txtModalMontoCLPEntrante.Text = "" Then
            txtModalDiferencia.Text = ""
            txtModalDiferenciaCLP.Text = ""
        Else
            If ddlModalMonedaEntranteCanje.SelectedValue = "CLP" And ddlModalMonedaSalienteCanje.SelectedValue = "CLP" Then

                canje.MontoEntrante = txtModalMontoEntrante.Text
                canje.MontoSaliente = txtModalMontoSaliente.Text
                canje.MontoCLPEntrante = txtModalMontoCLPEntrante.Text
                canje.MontoCLPSaliente = String.Format("{0:N0}", Decimal.Parse(txtModalMontoCLPSaliente.Text))

                Dim diferencia = canje.MontoSaliente - canje.MontoEntrante
                Dim diferenciaCLP = canje.MontoCLPSaliente - canje.MontoCLPEntrante
                txtModalDiferencia.Text = Utiles.SetToCapitalizedNumber(diferencia)
                txtModalDiferenciaCLP.Text = Utiles.SetToCapitalizedNumber(diferenciaCLP)

            Else
                canje.MontoEntrante = txtModalMontoEntrante.Text
                canje.MontoSaliente = txtModalMontoSaliente.Text
                canje.MontoCLPEntrante = txtModalMontoCLPEntrante.Text
                canje.MontoCLPSaliente = txtModalMontoCLPSaliente.Text

                Dim diferencia = canje.MontoSaliente - canje.MontoEntrante
                Dim diferenciaCLP = canje.MontoCLPSaliente - canje.MontoCLPEntrante
                txtModalDiferencia.Text = String.Format("{0:N4}", diferencia)
                txtModalDiferenciaCLP.Text = String.Format("{0:N0}", diferenciaCLP)
            End If

        End If
    End Sub

    Private Sub Presentacion_Mantenedores_frmFijacion_Maestro_AbortTransaction(sender As Object, e As EventArgs) Handles Me.AbortTransaction

    End Sub

    Private Sub btnImprimir_click(sender As Object, e As EventArgs) Handles btnImprimir.Click
        Dim fijaciones As List(Of FijacionDTO) = New List(Of FijacionDTO)
        Dim fijacion As FijacionDTO = New FijacionDTO

        Dim listaQueNoSeImprimen As List(Of FijacionDTO) = New List(Of FijacionDTO)


        For Each row As GridViewRow In GrvTabla.Rows
            Dim chk As CheckBox = row.Cells(0).Controls(1)
            If chk IsNot Nothing And chk.Checked Then
                fijacion = New FijacionDTO

                fijacion.ID = row.Cells(CONST_COL_ID).Text().Trim()
                fijacion.TipoTransaccion = row.Cells(CONST_COL_TIPOTRANSACCION).Text().Trim()
                fijacion.ApRut = row.Cells(CONST_COL_APRUT).Text().Trim()
                fijacion.RazonSocial = row.Cells(CONST_COL_RAZONSOCIAL).Text().Trim()
                fijacion.Rut = row.Cells(CONST_COL_RUT).Text().Trim()
                fijacion.FnNombreCorto = row.Cells(CONST_COL_FNNOMBRECORTO).Text().Trim()
                fijacion.FechaNav = row.Cells(CONST_COL_FECHANAV).Text().Trim()
                fijacion.Nemotecnico = row.Cells(CONST_COL_NEMOTECNICO).Text().Trim()
                fijacion.FijacionNAV = row.Cells(CONST_COL_FIJACIONNAV).Text().Trim()
                fijacion.FijacionTCObservado = row.Cells(CONST_COL_FIJACIONNAV).Text().Trim()
                fijacion.FechaNav = row.Cells(CONST_COL_FECHANAV).Text().Trim()
                fijacion.FechaTCObs = row.Cells(CONST_COL_FECHATCOBS).Text().Trim()
                fijacion.NAV_FIJADO = row.Cells(CONST_COL_NAV_FIJADO).Text().Trim()
                fijacion.TC_OBSERVADO = row.Cells(CONST_COL_TC_OBSERVADO).Text().Trim()
                fijacion.FijacionNAV = row.Cells(CONST_COL_FIJACIONNAV).Text().Trim()
                fijacion.FijacionTCObservado = row.Cells(CONST_COL_FIJACIONTCOBSERVADO).Text().Trim()
                fijacion.Nemotecnico = row.Cells(CONST_COL_NEMOTECNICO).Text().Trim()

                fijacion.MonedaPago = row.Cells(CONST_COL_MONEDA_PAGO).Text().Trim()
                fijacion.Cuotas = row.Cells(CONST_COL_CUOTAS).Text().Trim()
                fijacion.Monto = row.Cells(CONST_COL_MONTO).Text().Trim()

                fijacion.EstadoIntencion = row.Cells(CONST_COL_ESTADOINTENCION).Text().Trim()

                If fijacion.FijacionNAV.Equals("Pendiente") Or fijacion.FijacionTCObservado.Equals("Pendiente") Then
                    listaQueNoSeImprimen.Add(fijacion)

                Else
                    Select Case fijacion.TipoTransaccion.ToLower()
                        Case "canje"
                            SetCamposAdicionalesCanje(fijacion)
                        Case "suscripcion"
                            SetCamposAdicionalesSuscripcion(fijacion)
                        Case "rescate"
                            SetCamposAdicionalesRescate(fijacion)
                    End Select

                    fijaciones.Add(fijacion)
                End If
            End If
        Next

        Dim strMensaje As String = CONST_NO_HAY_TRANSACCIONES_SELECCIONADAS
        Dim strMensajeAux As String = ""

        If listaQueNoSeImprimen.Count() > 0 Then
            strMensajeAux = "No se pueden imprimir transacciones que no estan fijadas"
            ShowAlert(strMensajeAux)
            Exit Sub
        End If

        If (fijaciones.Count() < 1) Then
            ShowAlert(strMensaje)
            Exit Sub
        End If

        strMensaje = String.Format("{0}<BR>{1}", "Archivo Generado correctamente ", strMensajeAux)

        Dim nombrearchivo As String = exportarWord.GenerarCartas(fijaciones)

        If nombrearchivo IsNot Nothing Then
            Archivo.NavigateUrl = nombrearchivo
            Archivo.Text = "Bajar Archivo"
        Else
            Archivo.Visible = False
        End If

        txtAccionHidden.Value = "MOSTRAR_DIALOGO"

        ShowMessages(CONST_TITULO_FIJACION, strMensaje, Constantes.CONST_RUTA_IMG + Constantes.CONST_IMG_LOGO, Constantes.CONST_RUTA_IMG + Constantes.CONST_IMG_INFO, False)

    End Sub

    Private Shared Sub SetCamposAdicionalesRescate(fijacion As FijacionDTO)
        Dim negocio As RescateNegocio = New RescateNegocio()
        fijacion.ObjRescate = negocio.GetFijacionId(fijacion.ID)

    End Sub

    Private Shared Sub SetCamposAdicionalesSuscripcion(fijacion As FijacionDTO)
        Dim negocio As SuscripcionNegocio = New SuscripcionNegocio()
        fijacion.ObjSuscripcion = negocio.GetFijacionId(fijacion.ID)
    End Sub

    Private Sub SetCamposAdicionalesCanje(ByRef fijacion As FijacionDTO)
        Dim negocio As CanjeNegocio = New CanjeNegocio()
        fijacion.ObjCanje = negocio.GetFijacionId(fijacion.ID)
    End Sub

    Protected Sub BtnEliminar_Click(sender As Object, e As EventArgs) Handles BtnEliminar.Click
        Dim fijaciones As List(Of FijacionDTO) = New List(Of FijacionDTO)
        Dim strMensaje As String = CONST_NO_HAY_TRANSACCIONES_SELECCIONADAS

        Dim listaQueNoSeUpdatearon As List(Of FijacionDTO) = New List(Of FijacionDTO)

        fijaciones = getTransaccionesSeleccionadas()

        ' Ha seleccionado alguna transaccion ? - P1 
        If (fijaciones.Count() < 1) Then
            ShowAlert(strMensaje)
            Exit Sub
        End If

        Dim transaccionesSuscripciones As List(Of FijacionDTO) = fijaciones.Where(Function(x) x.TipoTransaccion.Equals("Suscripcion")).ToList()

        If (transaccionesSuscripciones.Count() < 1) Then
            ShowAlert("No ha selecionado ninguna Suscripción")
            Exit Sub
        End If


        Dim icantidad As Integer = transaccionesSuscripciones.Where(Function(x) x.ObjSuscripcion.EstadoIntencion.Equals("Intencion")).ToList().Count()

        If icantidad <> transaccionesSuscripciones.Count() Then
            ShowAlert("Existen registros seleccionados que no pueden ser eliminados por su Estado de Confirmación")
            Exit Sub
        End If

        For Each fija As FijacionDTO In transaccionesSuscripciones
            Dim negocio As SuscripcionNegocio = New SuscripcionNegocio()
            If Not negocio.DeleteSuscripcion(fija.ObjSuscripcion) Then
                listaQueNoSeUpdatearon.Add(fija)
            End If
        Next

        If listaQueNoSeUpdatearon.Count() > 0 Then
            ShowAlert("No se pudieron Eliminar todas las transacciones Seleccionadas")
        End If

        FindFijacion()

    End Sub

    Protected Sub btnConfirmar_Click(sender As Object, e As EventArgs) Handles btnConfirmar.Click
        Dim fijaciones As List(Of FijacionDTO) = New List(Of FijacionDTO)
        Dim fijacion As FijacionDTO = New FijacionDTO
        Dim strMensaje As String = CONST_NO_HAY_TRANSACCIONES_SELECCIONADAS
        Dim strMensajeAux As String = ""


        Dim listaQueNoSeUpdatearon As List(Of FijacionDTO) = New List(Of FijacionDTO)
        '•	Para las transacciones que son seleccionadas y se presiona el botón 'Confirmar’, pasan a tener Estado de Confirmación ‘Confirmadas’
        '•	Todo este flujo de confirmación aplica solo para las suscripciones. Para rescates y canjes por defecto en su Estado de Confirmación es 'Confirmadas’.

        fijaciones = getTransaccionesSeleccionadas()

        ' Ha seleccionado alguna transaccion ? - P1 
        If (fijaciones.Count() < 1) Then
            ShowAlert(strMensaje)
            Exit Sub
        End If

        For Each fija As FijacionDTO In fijaciones
            Dim negocio As FijacionNegocio = New FijacionNegocio()
            If Not negocio.UpdateEstadoConfirmacion(fija) Then
                listaQueNoSeUpdatearon.Add(fija)
            End If
        Next

        ' No se actualizaron estas Transacciones - P2
        If listaQueNoSeUpdatearon.Count() > 0 Then
            ShowAlert("No se pudieron actualizar estas transacciones")
            Exit Sub
        End If

        ShowAlert("Proceso concluido con Exito")

        FindFijacion()
    End Sub

    Private Function getTransaccionesSeleccionadas() As List(Of FijacionDTO)
        Dim fijaciones As List(Of FijacionDTO) = New List(Of FijacionDTO)
        Dim fijacion As FijacionDTO = New FijacionDTO

        For Each row As GridViewRow In GrvTabla.Rows
            Dim chk As CheckBox = row.Cells(0).Controls(1)
            If chk IsNot Nothing And chk.Checked Then
                fijacion = New FijacionDTO
                fijacion.ID = row.Cells(CONST_COL_ID).Text().Trim()
                fijacion.TipoTransaccion = row.Cells(CONST_COL_TIPOTRANSACCION).Text().Trim()

                Select Case fijacion.TipoTransaccion.ToLower()
                    Case "canje"
                        SetCamposAdicionalesCanje(fijacion)
                    Case "suscripcion"
                        SetCamposAdicionalesSuscripcion(fijacion)
                    Case "rescate"
                        SetCamposAdicionalesRescate(fijacion)
                End Select

                fijaciones.Add(fijacion)
            End If
        Next
        Return fijaciones
    End Function

#Region "******************************* MOVER INTENCION *****************************************************************"

    Protected Sub btnMoverIntencion_Click(sender As Object, e As EventArgs) Handles btnMoverIntencion.Click
        Dim fijaciones As List(Of FijacionDTO) = New List(Of FijacionDTO)
        Dim strMensaje As String = CONST_NO_HAY_TRANSACCIONES_SELECCIONADAS

        Try
            fijaciones = getTransaccionesSeleccionadas()

            ' Ha seleccionado alguna transaccion ? - P1 
            If (fijaciones.Count() < 1) Then
                ShowAlert(strMensaje)
                Exit Sub
            End If

            Dim transaccionesSuscripciones As List(Of FijacionDTO) = fijaciones.Where(Function(x) x.TipoTransaccion.Equals("Suscripcion")).ToList()

            If (transaccionesSuscripciones.Count() < 1) Then
                ShowAlert("No ha selecionado ninguna Suscripción")
                Exit Sub
            End If

            Dim icantidad As Integer = transaccionesSuscripciones.Where(Function(x) x.ObjSuscripcion.EstadoIntencion.Equals("Intencion")).ToList().Count()

            If icantidad <> transaccionesSuscripciones.Count() Then
                ShowAlert("Existen registros seleccionados que no pueden ser eliminados por su Estado de Confirmación")
                Exit Sub
            End If

            ' REcalculo de las suscripciones 

            For Each fija As FijacionDTO In transaccionesSuscripciones
                Dim negocio As SuscripcionNegocio = New SuscripcionNegocio()

                With fija.ObjSuscripcion
                    .FechaIntencion = Utiles.SumaDiasAFechas("CLP", .FechaIntencion, 1, Constantes.CONST_SOLO_DIAS_HABILES)
                    .FechaSuscripcion = ObtenerFechasSolicitud.ObtenerFechaSuscripcion(.Nemotecnico, .FechaIntencion, .FechaNAV)
                    .FechaNAV = ObtenerFechasSolicitud.ObtenerFechaNav(.Nemotecnico, .FechaSuscripcion, .FechaIntencion)
                    .FechaTC = ObtenerFechasSolicitud.ObtenerFechaTCObservado(.Nemotecnico, .FechaSuscripcion, .FechaNAV, .FechaIntencion)

                    'calculo de valores cuotas y tc 
                    .TcObservado = MoverIntencion_GetValorTC(fija.ObjSuscripcion)
                    .NAV = MoverIntencion_GetValorNAV(fija.ObjSuscripcion)

                    .NAVCLP = Utiles.calcularNAVCLP(.TcObservado, .NAV)
                    .Monto = Utiles.calcularMonto(.CuotasASuscribir, .NAV, .MonedaSerie)
                    .MontoCLP = Utiles.calcularMontoCLP(.CuotasASuscribir, .NAV, .TcObservado)

                End With

                If Not negocio.UpdateSuscripcion(fija.ObjSuscripcion) Then

                End If
            Next

            ShowAlert("Proceso terminado con Exito")
        Catch ex As Exception

        Finally
            FindFijacion()
        End Try
    End Sub

    Private Function MoverIntencion_GetValorTC(Suscripcion As SuscripcionDTO) As Double
        'TRAER VALOR TIPO CAMBIO OBSERVADO
        Dim TipoCambio As TipoCambioDTO = New TipoCambioDTO()
        Dim NegocioTipoCambio As TipoCambioNegocio = New TipoCambioNegocio
        Dim TipoCambioActualizado As TipoCambioDTO = New TipoCambioDTO()
        Dim ListaTC As List(Of TipoCambioDTO)

        Dim retornoTC As Double

        Try
            TipoCambio.Fecha = Suscripcion.FechaTC
            TipoCambio.Codigo = Suscripcion.MonedaSerie

            TipoCambioActualizado = NegocioTipoCambio.GetTipoCambio(TipoCambio)

            If TipoCambioActualizado IsNot Nothing Then
                retornoTC = TipoCambioActualizado.Valor
            Else
                ListaTC = NegocioTipoCambio.UltimoTipoCambioPorCodigo(TipoCambio)
                If ListaTC IsNot Nothing AndAlso ListaTC.Count() = 0 Then
                    retornoTC = 0
                Else
                    retornoTC = ListaTC(0).Valor
                End If
            End If
        Catch ex As Exception
            retornoTC = 0

        Finally
            TipoCambio = Nothing
            NegocioTipoCambio = Nothing
            TipoCambioActualizado = Nothing
            ListaTC = Nothing
        End Try

        Return retornoTC
    End Function

    Private Function MoverIntencion_GetValorNAV(Suscripcion As SuscripcionDTO) As Double
        Dim ValorNavDTO As VcSerieDTO = New VcSerieDTO()
        Dim negocioValor As ValoresCuotaNegocio = New ValoresCuotaNegocio

        Dim ValorCuotaAlaFecha As VcSerieDTO
        Dim listaUltimo As List(Of VcSerieDTO)

        Dim valorNav As Double

        Try
            ValorNavDTO.FsNemotecnico = Suscripcion.Nemotecnico
            ValorNavDTO.Fecha = Suscripcion.FechaNAV
            ValorNavDTO.FnRut = Suscripcion.RutFondo

            ValorCuotaAlaFecha = negocioValor.GetValoresCuota(ValorNavDTO)

            If ValorCuotaAlaFecha IsNot Nothing Then
                valorNav = ValorCuotaAlaFecha.Valor
            Else
                listaUltimo = negocioValor.UltimoValorCuota(ValorNavDTO)
                If listaUltimo IsNot Nothing AndAlso listaUltimo.Count = 0 Then
                    valorNav = 0
                Else
                    valorNav = listaUltimo(0).Valor
                End If
            End If
        Catch ex As Exception
            valorNav = 0

        Finally
            ValorNavDTO = Nothing
            negocioValor = Nothing
            ValorCuotaAlaFecha = Nothing
            listaUltimo = Nothing
        End Try

        Return valorNav
    End Function


#End Region


#Region "SECCION ERRORES DE FIJACION"

    Public Enum TipoErroresFijacion As Integer
        EF_ERROR_INTERNO
        EF_ERROR_NO_EXISTE_NAV_O_TC
        EF_ERROR_TRANSACCION_YA_FIJADA
        EF_ERROR_TRANSACCION_EN_INTENCION
        EF_TRANSACCIONES_FIJADAS_EXITOSAMENTE
        EF_ERROR_SERIES_CONFIGURACION
        EF_CONFIGURACION_MANUAL
    End Enum

    ''' <summary>
    ''' Creaacion de la lista de Errores Que Cargara el Sistema en la Fijación
    ''' </summary>
    ''' <returns></returns>
    Private Function CargarListaErroresSoportados() As List(Of TErroresFijacion)
        Dim ListaErrores As List(Of TErroresFijacion) = New List(Of TErroresFijacion)


        ListaErrores.Add(New TErroresFijacion(TipoErroresFijacion.EF_TRANSACCIONES_FIJADAS_EXITOSAMENTE, "Transacciones fijadas", 0))
        ListaErrores.Add(New TErroresFijacion(TipoErroresFijacion.EF_ERROR_INTERNO, "Error de la aplicación (interno)", 0))
        ListaErrores.Add(New TErroresFijacion(TipoErroresFijacion.EF_ERROR_NO_EXISTE_NAV_O_TC, "No existe NAV o TC", 0))
        ListaErrores.Add(New TErroresFijacion(TipoErroresFijacion.EF_ERROR_TRANSACCION_YA_FIJADA, "Transacción ya fijada", 0))
        ListaErrores.Add(New TErroresFijacion(TipoErroresFijacion.EF_ERROR_TRANSACCION_EN_INTENCION, "Transacción en intención", 0))
        ListaErrores.Add(New TErroresFijacion(TipoErroresFijacion.EF_CONFIGURACION_MANUAL, "Transaccines Manuales", 0))
        ListaErrores.Add(New TErroresFijacion(TipoErroresFijacion.EF_ERROR_SERIES_CONFIGURACION, "Errores en configuracion de Transacciones", 0))


        Return ListaErrores
    End Function

    Private Function AgregarError(listaErrores As List(Of TErroresFijacion), TipoError As Integer, Optional ByVal InformacionAdicional As String = "") As List(Of TErroresFijacion)
        Dim tErrorFijacion As TErroresFijacion = New TErroresFijacion()

        tErrorFijacion = listaErrores.Find(Function(l) l.Id = TipoError)

        If tErrorFijacion IsNot Nothing Then
            Dim posicion As Integer = listaErrores.FindIndex(Function(l) l.Id = TipoError)

            tErrorFijacion.Cantidad = tErrorFijacion.Cantidad + 1

            If Not InformacionAdicional.Equals("") AndAlso tErrorFijacion.InformacionAdicional.IndexOf(InformacionAdicional) < 0 Then
                tErrorFijacion.InformacionAdicional = tErrorFijacion.InformacionAdicional + InformacionAdicional + ", "
            End If

            listaErrores.RemoveAt(posicion)
            listaErrores.Insert(posicion, tErrorFijacion)
        End If

        Return listaErrores
    End Function

    Private Sub MostrarMensaje(listaErrores As List(Of TErroresFijacion), ByRef TransaccionesTotales As Integer, ByRef TransaccionesExitosas As Integer, ByRef TransaccionesNoExitosas As Integer, CantidadFijados As Integer, CantidadNOFijados As Integer, Mensaje As String, listaMensajes As List(Of String))
        Dim stringMensajes As String = ""
        Dim stringAux As String = ""
        Dim formato As String = "{0}: {1}\n"
        Dim maxChars As Integer = 40

        TransaccionesTotales = CantidadFijados + CantidadNOFijados
        TransaccionesExitosas = CantidadFijados
        TransaccionesNoExitosas = CantidadNOFijados

        If listaMensajes.Count() > 0 Then
            stringAux = "\n\n ADVERTENCIA: Algunas transacciones exceden el patrimonio del fondo."
        End If

        stringMensajes = "RESUMEN\n\n"
        stringMensajes = stringMensajes & String.Format(formato, "Transacciones Seleccionadas", CStr(TransaccionesTotales))

        For Each errorTxt As TErroresFijacion In listaErrores
            stringMensajes = stringMensajes + String.Format(formato, errorTxt.MsgError, CStr(errorTxt.Cantidad))

            If Not errorTxt.InformacionAdicional.Trim().Equals("") Then
                stringMensajes = stringMensajes + "(" + errorTxt.InformacionAdicional.Substring(0, errorTxt.InformacionAdicional.Trim().Length() - 1) + ") \n "
            End If
        Next

        stringMensajes = stringMensajes + stringAux

        stringMensajes = String.Format("Transacciones Seleccionadas: {0} \n Transacciones Fijadas: {1} \n Transacciones con error: {2} . {3} {4}", CStr(TransaccionesTotales), CStr(TransaccionesExitosas), CStr(TransaccionesNoExitosas), Mensaje, stringAux)

        ShowAlert(stringMensajes)
    End Sub


#End Region
End Class