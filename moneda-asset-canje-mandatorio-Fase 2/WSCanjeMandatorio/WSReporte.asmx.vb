Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel
Imports DTO
Imports System.Web.Script.Services

' Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente.
' <System.Web.Script.Services.ScriptService()> _
<System.Web.Services.WebService(Namespace:="http://tempuri.org/")> _
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)>
<ToolboxItem(False)>
Public Class WSReporte
    Inherits System.Web.Services.WebService

    Private Const CONST_PREFIJO As String = ""

    Private Const SP_CONSULTAR As String = CONST_PREFIJO + "PRC_ObtieneEvaluacionesCompleto"
    Private Const SP_NUEVA_EVALUACION As String = CONST_PREFIJO + "PRC_Evaluar_Canje_Serie"
    Private Const SP_NUEVA_EVALUACION_SERIE As String = CONST_PREFIJO + "PRC_Evaluar_Canje_Serie_Detalle"
    Private Const SP_UPDATE_PROCESO As String = "PRC_UpdateProcesoById"
    Private Const SP_TRAER_PROCESO As String = "PRC_TraeProcesoPorId"

    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function GetListaReporte(listaFondo As List(Of FondoDTO), FechaProceso As Nullable(Of Date), FechaCanje As Nullable(Of Date), txtCambio As Decimal) As List(Of ReporteFechaCorteDTO)
        Dim ListaReporte As List(Of ReporteFechaCorteDTO) = New List(Of ReporteFechaCorteDTO)
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CONSULTAR)
        Dim ds As DataSet


        Try
            sp.AgregarParametro("FechaProceso", FechaProceso, System.Data.SqlDbType.DateTime)
            sp.AgregarParametro("FechaCanje", FechaCanje, System.Data.SqlDbType.DateTime)
            FillParameters(listaFondo, sp)
            ds = sp.ReturnDataSet()

            For Each dataRow As DataRow In ds.Tables(0).Rows
                ListaReporte.Add(fillReporte(dataRow, txtCambio))
            Next

        Catch ex As Exception
            Throw
        End Try
        Return ListaReporte
    End Function

    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function GetNuevaEvaluacion(proceso As ProcesoDTO) As ReporteFechaCorteDTO
        Dim reporte As ReporteFechaCorteDTO = New ReporteFechaCorteDTO
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_NUEVA_EVALUACION)
        Dim ds As DataSet

        Try
            sp.AgregarParametro("prID", proceso.IdProceso, System.Data.SqlDbType.Int)
            sp.AgregarParametro("prCuotaSaliente", proceso.ResCuotas, System.Data.SqlDbType.BigInt)
            sp.AgregarParametro("paramNemotecnico", proceso.FsNemotecnico, System.Data.SqlDbType.VarChar)

            ds = sp.ReturnDataSet()

            If ds.Tables.Count > 0 AndAlso ds.Tables(0).Rows.Count > 0 Then
                reporte = fillReporteNuevaEvaluacion(ds.Tables(0).Rows(0))
            End If

            Return reporte

        Catch ex As Exception
            Throw
        End Try
    End Function

    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function GetNuevaEvaluacionHija(proceso As ProcesoDTO) As ReporteFechaCorteDTO
        Dim reporte As ReporteFechaCorteDTO = New ReporteFechaCorteDTO
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_NUEVA_EVALUACION_SERIE)
        Dim ds As DataSet

        Try
            sp.AgregarParametro("prID", proceso.IdProceso, System.Data.SqlDbType.Int)
            sp.AgregarParametro("prCuotaSaliente", proceso.ResCuotas, System.Data.SqlDbType.BigInt)
            sp.AgregarParametro("paramNemotecnico", proceso.FsNemotecnico, System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("TC_reporte", proceso.TcValor, System.Data.SqlDbType.Decimal)

            ds = sp.ReturnDataSet()

            If ds.Tables.Count > 0 AndAlso ds.Tables(0).Rows.Count > 0 Then
                reporte = fillReporteNuevaEvaluacionHija(ds.Tables(0).Rows(0))
            End If

            Return reporte

        Catch ex As Exception
            Throw
        End Try
    End Function

    '   KEMP_PRC_Evaluar_Canje_Serie] 
    '@prID		integer,
    '@prCuotaSaliente	numeric(26,0),
    '@paramNemotecnico	varchar(20), 
    '@debug varchar(1) = null
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function UpdateProcesoByID(proceso As ReporteFechaCorteDTO) As Boolean
        Dim reporte As ReporteFechaCorteDTO = New ReporteFechaCorteDTO
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_UPDATE_PROCESO)
        Dim ds As DataSet

        Try
            FillParametersUpdateProceso(proceso, sp)

            ds = sp.ReturnDataSet()

            Return (sp.FilasAfectas > 0)

        Catch ex As Exception
            Throw
        End Try
    End Function


    Private Sub FillParametersUpdateProceso(proceso As ReporteFechaCorteDTO, sp As DBSqlServer.SqlServer.StoredProcedure)
        sp.AgregarParametro("PR_ID", proceso.pca_PR_ID, System.Data.SqlDbType.BigInt)
        sp.AgregarParametro("FS_Nemotecnico", proceso.FS_Nemotecnico, System.Data.SqlDbType.VarChar)
        sp.AgregarParametro("FS_Moneda", proceso.FS_Moneda, System.Data.SqlDbType.VarChar)
        sp.AgregarParametro("VCS_Valor", proceso.VCS_Valor, System.Data.SqlDbType.BigInt)
        sp.AgregarParametro("PR_Saldo_Cuotas", proceso.PR_Saldo_Cuotas, System.Data.SqlDbType.BigInt)
        sp.AgregarParametro("PR_Monto", proceso.PR_Monto, System.Data.SqlDbType.BigInt)
        sp.AgregarParametro("C_AP_Final_I", proceso.C_AP_Final_I, System.Data.SqlDbType.VarChar)
        sp.AgregarParametro("PR_DescEstado", proceso.PR_DescEstado, System.Data.SqlDbType.VarChar)
        sp.AgregarParametro("C_AP_Nac_Ext", proceso.C_AP_Nac_Ext, System.Data.SqlDbType.VarChar)
        sp.AgregarParametro("C_AP_Calificado", proceso.C_AP_Calificado, System.Data.SqlDbType.VarChar)
        sp.AgregarParametro("C_AP_Rel_MAM", proceso.C_AP_Rel_MAM, System.Data.SqlDbType.VarChar)
        sp.AgregarParametro("C_AP_Limite", proceso.C_AP_Limite, System.Data.SqlDbType.VarChar)
        sp.AgregarParametro("C_Certificado", proceso.C_Certificado, System.Data.SqlDbType.VarChar)
        sp.AgregarParametro("C_Cuotas_C", proceso.C_Cuotas_C, System.Data.SqlDbType.BigInt)
        sp.AgregarParametro("C_Cuotas_Certificar", proceso.C_Cuotas_Certificar, System.Data.SqlDbType.BigInt)

    End Sub

    Private Function fillReporteNuevaEvaluacion(dataRow As DataRow) As ReporteFechaCorteDTO
        Dim reporte As New ReporteFechaCorteDTO
        With reporte
            .pca_C_AP_Nac_Ext = If(dataRow("C_AP_NAC_EXT").ToString().Trim() = "", Nothing, dataRow("C_AP_NAC_EXT").ToString().Trim())
            .pca_C_AP_Calificado = If(dataRow("C_AP_CALIFICADO").ToString().Trim() = "", Nothing, dataRow("C_AP_CALIFICADO").ToString().Trim())
            .pca_C_AP_Rel_MAM = If(dataRow("C_AP_RELNAM").ToString().Trim() = "", Nothing, dataRow("C_AP_RELNAM").ToString().Trim())
            .pca_C_AP_Limite = If(dataRow("C_AP_LIMITE").ToString().Trim() = "", Nothing, dataRow("C_AP_LIMITE").ToString().Trim())
            .pca_C_AP_Final_I = If(dataRow("C_AP_FINAL_I").ToString().Trim() = "", Nothing, dataRow("C_AP_FINAL_I").ToString().Trim())
            .pca_ContratoDistribucion = If(dataRow("C_CONTRATO_DISTRIBUCION").ToString().Trim() = "", Nothing, dataRow("C_CONTRATO_DISTRIBUCION").ToString().Trim())
            .pca_PR_DescEstado = If(dataRow("PR_DESC_ESTADO").ToString().Trim() = "", Nothing, dataRow("PR_DESC_ESTADO").ToString().Trim())
            .PR_Saldo_Cuotas = If(dataRow("PR_Saldo_Cuotas").ToString().Trim() = "", Nothing, dataRow("PR_Saldo_Cuotas").ToString().Trim())
            .NavCuotaEntrante = If(dataRow("NavCuotaEntrante").ToString().Trim() = "", Nothing, dataRow("NavCuotaEntrante").ToString().Trim())
            .ValorCambio = If(dataRow("ValorCambio").ToString().Trim() = "", Nothing, dataRow("ValorCambio").ToString().Trim())
            .VCS_Valor = If(dataRow("VCS_Valor").ToString().Trim() = "", Nothing, dataRow("VCS_Valor").ToString().Trim())
            .PR_Monto = If(dataRow("PR_Monto").ToString().Trim() = "", Nothing, dataRow("PR_Monto").ToString().Trim())

        End With
        Return reporte
    End Function

    Private Function fillReporteNuevaEvaluacionHija(dataRow As DataRow) As ReporteFechaCorteDTO
        Dim reporte As New ReporteFechaCorteDTO
        With reporte
            .pca_C_AP_Nac_Ext = If(dataRow("C_AP_NAC_EXT").ToString().Trim() = "", Nothing, dataRow("C_AP_NAC_EXT").ToString().Trim())
            .pca_C_AP_Calificado = If(dataRow("C_AP_CALIFICADO").ToString().Trim() = "", Nothing, dataRow("C_AP_CALIFICADO").ToString().Trim())
            .pca_C_AP_Rel_MAM = If(dataRow("C_AP_RELNAM").ToString().Trim() = "", Nothing, dataRow("C_AP_RELNAM").ToString().Trim())
            .pca_C_AP_Limite = If(dataRow("C_AP_LIMITE").ToString().Trim() = "", Nothing, dataRow("C_AP_LIMITE").ToString().Trim())
            .pca_C_AP_Final_I = If(dataRow("C_AP_FINAL_I").ToString().Trim() = "", Nothing, dataRow("C_AP_FINAL_I").ToString().Trim())
            .pca_ContratoDistribucion = If(dataRow("C_CONTRATO_DISTRIBUCION").ToString().Trim() = "", Nothing, dataRow("C_CONTRATO_DISTRIBUCION").ToString().Trim())
            .pca_PR_DescEstado = If(dataRow("PR_DESC_ESTADO").ToString().Trim() = "", Nothing, dataRow("PR_DESC_ESTADO").ToString().Trim())
            .PR_Saldo_Cuotas = If(dataRow("PR_Saldo_Cuotas").ToString().Trim() = "", Nothing, dataRow("PR_Saldo_Cuotas").ToString().Trim())
            .NavCuotaEntrante = If(dataRow("NavCuotaEntrante").ToString().Trim() = "", Nothing, dataRow("NavCuotaEntrante").ToString().Trim())
            .ValorCambio = If(dataRow("ValorCambio").ToString().Trim() = "", Nothing, dataRow("ValorCambio").ToString().Trim())
            .VCS_Valor = If(dataRow("VCS_Valor").ToString().Trim() = "", Nothing, dataRow("VCS_Valor").ToString().Trim())
            .PR_Monto = If(dataRow("PR_Monto").ToString().Trim() = "", Nothing, dataRow("PR_Monto").ToString().Trim())

            .monto_saliente = If(dataRow("monto_saliente").ToString().Trim() = "", Nothing, dataRow("monto_saliente").ToString().Trim())
            .NAV_saliente_CLP = If(dataRow("NAV_saliente_CLP").ToString().Trim() = "", Nothing, dataRow("NAV_saliente_CLP").ToString().Trim())
            .monto_saliente_CLP = If(dataRow("monto_saliente_CLP").ToString().Trim() = "", Nothing, dataRow("monto_saliente_CLP").ToString().Trim())
            '.Factor = If(dataRow("factor").ToString().Trim() = "", Nothing, dataRow("factor").ToString().Trim())
            .cuotas_entrantes = If(dataRow("cuotas_entrantes").ToString().Trim() = "", Nothing, dataRow("cuotas_entrantes").ToString().Trim())
            .monto_entrante = If(dataRow("monto_entrante").ToString().Trim() = "", Nothing, dataRow("monto_entrante").ToString().Trim())
            .NavCuotaEntranteCLP = If(dataRow("NavCuotaEntranteCLP").ToString().Trim() = "", Nothing, dataRow("NavCuotaEntranteCLP").ToString().Trim())
            .monto_entrante_CLP = If(dataRow("monto_entrante_CLP").ToString().Trim() = "", Nothing, dataRow("monto_entrante_CLP").ToString().Trim())
            '.Diferencia = If(dataRow("diferencia").ToString().Trim() = "", Nothing, dataRow("diferencia").ToString().Trim())
            .diferencia_CLP = If(dataRow("diferencia_CLP").ToString().Trim() = "", Nothing, dataRow("diferencia_CLP").ToString().Trim())
            .PRD_factor = If(dataRow("factor").ToString().Trim() = "", Nothing, dataRow("factor").ToString().Trim())
            .diferenciaMoneda = If(dataRow("diferencia").ToString().Trim() = "", Nothing, dataRow("diferencia").ToString().Trim())
            .PRCuotasSalientes = If(dataRow("PRCuotaSaliente").ToString().Trim() = "", Nothing, dataRow("PRCuotaSaliente").ToString().Trim())
        End With
        Return reporte
    End Function



    Private Function fillReporte(dataRow As DataRow, txtCambio As Decimal) As ReporteFechaCorteDTO
        Dim reporte As New ReporteFechaCorteDTO
        With reporte
            .pr_id = If(dataRow("PR_ID").ToString().Trim() = "", Nothing, dataRow("PR_ID").ToString().Trim())
            .ADCV_Cantidad = If(dataRow("ADCV_Cantidad").ToString().Trim() = "", Nothing, dataRow("ADCV_Cantidad").ToString().Trim())
            .AP_Razon_Social = If(dataRow("AP_Razon_Social").ToString().Trim() = "", Nothing, dataRow("AP_Razon_Social").ToString().Trim())
            .FN_RUT = If(dataRow("FN_RUT").ToString().Trim() = "", Nothing, dataRow("FN_RUT").ToString().Trim())
            .CANJE = If(dataRow("CANJE").ToString().Trim() = "", Nothing, dataRow("CANJE").ToString().Trim())
            .FN_Razon_Social = If(dataRow("FN_Razon_Social").ToString().Trim() = "", Nothing, dataRow("FN_Razon_Social").ToString().Trim())
            .PR_Directo_Indirecto = If(dataRow("PR_Directo_Indirecto").ToString().Trim() = "", Nothing, dataRow("PR_Directo_Indirecto").ToString().Trim())
            .FS_Grupo = If(dataRow("FS_Grupo").ToString().Trim() = "", Nothing, dataRow("FS_Grupo").ToString().Trim())
            .GPA_Descripcion = If(dataRow("GPA_Descripcion").ToString().Trim() = "", Nothing, dataRow("GPA_Descripcion").ToString().Trim())
            .AP_RUT = If(dataRow("AP_RUT").ToString().Trim() = "", Nothing, dataRow("AP_RUT").ToString().Trim())
            .FS_Nemotecnico = If(dataRow("FS_Nemotecnico").ToString().Trim() = "", Nothing, dataRow("FS_Nemotecnico").ToString().Trim())
            .FS_Moneda = If(dataRow("FS_Moneda").ToString().Trim() = "", Nothing, dataRow("FS_Moneda").ToString().Trim())
            .VCS_Valor = If(dataRow("VCS_Valor").ToString().Trim() = "", Nothing, dataRow("VCS_Valor").ToString().Trim())
            .RES_Cuotas = If(dataRow("RES_Cuotas").ToString().Trim() = "", Nothing, dataRow("RES_Cuotas").ToString().Trim())
            .Susc_Cuotas = If(dataRow("Susc_Cuotas").ToString().Trim() = "", Nothing, dataRow("Susc_Cuotas").ToString().Trim())
            .PR_Saldo_Cuotas = If(dataRow("PR_Saldo_Cuotas").ToString().Trim() = "", Nothing, dataRow("PR_Saldo_Cuotas").ToString().Trim())
            .PR_Monto = If(dataRow("PR_Monto").ToString().Trim() = "", Nothing, dataRow("PR_Monto").ToString().Trim())
            .PR_DescEstado = If(dataRow("PR_DescEstado").ToString().Trim() = "", Nothing, dataRow("PR_DescEstado").ToString().Trim())
            .SerieOptima = If(dataRow("SerieOptima").ToString().Trim() = "", Nothing, dataRow("SerieOptima").ToString().Trim())
            .C_AP_Nac_Ext = If(dataRow("C_AP_Nac_Ext").ToString().Trim() = "", Nothing, dataRow("C_AP_Nac_Ext").ToString().Trim())
            .C_AP_Calificado = If(dataRow("C_AP_Calificado").ToString().Trim() = "", Nothing, dataRow("C_AP_Calificado").ToString().Trim())
            .C_AP_Rel_MAM = If(dataRow("C_AP_Rel_MAM").ToString().Trim() = "", Nothing, dataRow("C_AP_Rel_MAM").ToString().Trim())
            .ContratoDistribucion = If(dataRow("ContratoDistribucion").ToString().Trim() = "", Nothing, dataRow("ContratoDistribucion").ToString().Trim())
            .C_AP_Limite = If(dataRow("C_AP_Limite").ToString().Trim() = "", Nothing, dataRow("C_AP_Limite").ToString().Trim())
            .C_Certificado = If(dataRow("C_Certificado").ToString().Trim() = "", Nothing, dataRow("C_Certificado").ToString().Trim())
            .C_AP_Final_I = If(dataRow("C_AP_Final_I").ToString().Trim() = "", Nothing, dataRow("C_AP_Final_I").ToString().Trim())
            .C_Cuotas_C = If(dataRow("C_Cuotas_C").ToString().Trim() = "", Nothing, dataRow("C_Cuotas_C").ToString().Trim())
            .C_Cuotas_Certificar = If(dataRow("C_Cuotas_Certificar").ToString().Trim() = "", Nothing, dataRow("C_Cuotas_Certificar").ToString().Trim())

            .pca_ADCV_Cantidad = If(dataRow("pca_ADCV_Cantidad").ToString().Trim() = "", Nothing, dataRow("pca_ADCV_Cantidad").ToString().Trim())
            .pca_AP_Razon_Social = If(dataRow("pca_AP_Razon_Social").ToString().Trim() = "", Nothing, dataRow("pca_AP_Razon_Social").ToString().Trim())
            .pca_FN_RUT = If(dataRow("pca_FN_RUT").ToString().Trim() = "", Nothing, dataRow("pca_FN_RUT").ToString().Trim())
            .pca_CANJE = If(dataRow("pca_CANJE").ToString().Trim() = "", Nothing, dataRow("pca_CANJE").ToString().Trim())
            .pca_FN_Razon_Social = If(dataRow("pca_FN_Razon_Social").ToString().Trim() = "", Nothing, dataRow("pca_FN_Razon_Social").ToString().Trim())
            .pca_PR_Directo_Indirecto = If(dataRow("pca_PR_Directo_Indirecto").ToString().Trim() = "", Nothing, dataRow("pca_PR_Directo_Indirecto").ToString().Trim())
            .pca_FS_Grupo = If(dataRow("pca_FS_Grupo").ToString().Trim() = "", Nothing, dataRow("pca_FS_Grupo").ToString().Trim())
            .pca_GPA_Descripcion = If(dataRow("pca_GPA_Descripcion").ToString().Trim() = "", Nothing, dataRow("pca_GPA_Descripcion").ToString().Trim())
            .pca_AP_RUT = If(dataRow("pca_AP_RUT").ToString().Trim() = "", Nothing, dataRow("pca_AP_RUT").ToString().Trim())
            .pca_FS_Nemotecnico = If(dataRow("pca_FS_Nemotecnico").ToString().Trim() = "", Nothing, dataRow("pca_FS_Nemotecnico").ToString().Trim())
            .pca_FS_Moneda = If(dataRow("pca_FS_Moneda").ToString().Trim() = "", Nothing, dataRow("pca_FS_Moneda").ToString().Trim())
            .pca_VCS_Valor = If(dataRow("pca_VCS_Valor").ToString().Trim() = "", Nothing, dataRow("pca_VCS_Valor").ToString().Trim())

            .pca_TC_Valor = If(dataRow("pca_TC_Valor").ToString().Trim() = "", Nothing, dataRow("pca_TC_Valor").ToString().Trim())

            .pca_RES_Cuotas = If(dataRow("pca_RES_Cuotas").ToString().Trim() = "", Nothing, dataRow("pca_RES_Cuotas").ToString().Trim())
            .pca_Susc_Cuotas = If(dataRow("pca_Susc_Cuotas").ToString().Trim() = "", Nothing, dataRow("pca_Susc_Cuotas").ToString().Trim())
            .pca_PR_Saldo_Cuotas = If(dataRow("pca_PR_Saldo_Cuotas").ToString().Trim() = "", Nothing, dataRow("pca_PR_Saldo_Cuotas").ToString().Trim())
            .pca_PR_Monto = If(dataRow("pca_PR_Monto").ToString().Trim() = "", Nothing, dataRow("pca_PR_Monto").ToString().Trim())
            .pca_PR_DescEstado = If(dataRow("pca_PR_DescEstado").ToString().Trim() = "", Nothing, dataRow("pca_PR_DescEstado").ToString().Trim())
            .pca_SerieOptima = If(dataRow("pca_SerieOptima").ToString().Trim() = "", Nothing, dataRow("pca_SerieOptima").ToString().Trim())
            .pca_C_AP_Nac_Ext = If(dataRow("pca_C_AP_Nac_Ext").ToString().Trim() = "", Nothing, dataRow("pca_C_AP_Nac_Ext").ToString().Trim())
            .pca_C_AP_Calificado = If(dataRow("pca_C_AP_Calificado").ToString().Trim() = "", Nothing, dataRow("pca_C_AP_Calificado").ToString().Trim())
            .pca_C_AP_Rel_MAM = If(dataRow("pca_C_AP_Rel_MAM").ToString().Trim() = "", Nothing, dataRow("pca_C_AP_Rel_MAM").ToString().Trim())
            .pca_ContratoDistribucion = If(dataRow("pca_ContratoDistribucion").ToString().Trim() = "", Nothing, dataRow("pca_ContratoDistribucion").ToString().Trim())
            .pca_C_AP_Limite = If(dataRow("pca_C_AP_Limite").ToString().Trim() = "", Nothing, dataRow("pca_C_AP_Limite").ToString().Trim())
            .pca_C_Certificado = If(dataRow("pca_C_Certificado").ToString().Trim() = "", Nothing, dataRow("pca_C_Certificado").ToString().Trim())
            .pca_C_AP_Final_I = If(dataRow("pca_C_AP_Final_I").ToString().Trim() = "", Nothing, dataRow("pca_C_AP_Final_I").ToString().Trim())
            .pca_C_Cuotas_C = If(dataRow("pca_C_Cuotas_C").ToString().Trim() = "", Nothing, dataRow("pca_C_Cuotas_C").ToString().Trim())
            .pca_C_Cuotas_Certificar = If(dataRow("pca_C_Cuotas_Certificar").ToString().Trim() = "", Nothing, dataRow("pca_C_Cuotas_Certificar").ToString().Trim())
            .pca_PR_ID = If(dataRow("pca_PR_ID").ToString().Trim() = "", Nothing, dataRow("pca_PR_ID").ToString().Trim())
            .PRCuotasSalientes = If(dataRow("pca_RES_Cuotas").ToString().Trim() = "", Nothing, dataRow("pca_RES_Cuotas").ToString().Trim())
            .ValorCambio = txtCambio

            .x_MontoSaliente = If(dataRow("x_MontoSaliente").ToString().Trim() = "", Nothing, dataRow("x_MontoSaliente").ToString().Trim())
            .prd_cuotasentrante = If(dataRow("prd_cuotasentrante").ToString().Trim() = "", Nothing, dataRow("prd_cuotasentrante").ToString().Trim())
            .x_MontoEntrante = If(dataRow("MontoEntrante").ToString().Trim() = "", Nothing, dataRow("MontoEntrante").ToString().Trim())
            .x_MontoEntranteCLP = If(dataRow("MontoEntranteCLP").ToString().Trim() = "", Nothing, dataRow("MontoEntranteCLP").ToString().Trim())
            .x_Factor = If(dataRow("x_Factor").ToString().Trim() = "", Nothing, dataRow("x_Factor").ToString().Trim())
            .prd_diferencia = If(dataRow("prd_diferencia").ToString().Trim() = "", Nothing, dataRow("prd_diferencia").ToString().Trim())
            .prd_diferenciaclp = If(dataRow("prd_diferenciaclp").ToString().Trim() = "", Nothing, dataRow("prd_diferenciaclp").ToString().Trim())
            .prd_naventrante = If(dataRow("prd_naventrante").ToString().Trim() = "", Nothing, dataRow("prd_naventrante").ToString().Trim())
            .fs_monedaentrante = If(dataRow("fs_monedaentrante").ToString().Trim() = "", Nothing, dataRow("fs_monedaentrante").ToString().Trim())
            .prd_observaciones = If(dataRow("prd_observaciones").ToString().Trim() = "", Nothing, dataRow("prd_observaciones").ToString().Trim())
            .x_pr_descestado = If(dataRow("x_pr_descestado").ToString().Trim() = "", Nothing, dataRow("x_pr_descestado").ToString().Trim())
            .x_c_ap_nac_ext = If(dataRow("x_c_ap_nac_ext").ToString().Trim() = "", Nothing, dataRow("x_c_ap_nac_ext").ToString().Trim())
            .x_c_ap_calificado = If(dataRow("x_c_ap_calificado").ToString().Trim() = "", Nothing, dataRow("x_c_ap_calificado").ToString().Trim())
            .x_c_ap_rel_mam = If(dataRow("x_c_ap_rel_mam").ToString().Trim() = "", Nothing, dataRow("x_c_ap_rel_mam").ToString().Trim())
            .x_c_cuotas_certificar = If(dataRow("x_c_cuotas_certificar").ToString().Trim() = "", Nothing, dataRow("x_c_cuotas_certificar").ToString().Trim())
            .x_fs_nemotecnico = If(dataRow("x_fs_nemotecnico").ToString().Trim() = "", Nothing, dataRow("x_fs_nemotecnico").ToString().Trim())
            .x_c_ap_limite = If(dataRow("x_c_ap_limite").ToString().Trim() = "", Nothing, dataRow("x_c_ap_limite").ToString().Trim())
            .x_c_certificado = If(dataRow("x_c_certificado").ToString().Trim() = "", Nothing, dataRow("x_c_certificado").ToString().Trim())
            .x_c_ap_final_i = If(dataRow("x_c_ap_final_i").ToString().Trim() = "", Nothing, dataRow("x_c_ap_final_i").ToString().Trim())
            .prd_cuotassalientes = If(dataRow("prd_cuotassalientes").ToString().Trim() = "", Nothing, dataRow("prd_cuotassalientes").ToString().Trim())
            .prd_accion = If(dataRow("prd_accion").ToString().Trim() = "", Nothing, dataRow("prd_accion").ToString().Trim())

        End With
        Return reporte
    End Function

    Private Function fillProcesoReporte(dataRow As DataRow) As ReporteFechaCorteDTO
        Dim reporte As New ReporteFechaCorteDTO
        With reporte
            .pca_PR_ID = If(dataRow("PR_ID").ToString().Trim() = "", Nothing, dataRow("PR_ID").ToString().Trim())
            .FN_RUT = If(dataRow("FN_RUT").ToString().Trim() = "", Nothing, dataRow("FN_RUT").ToString().Trim())
            .FN_Razon_Social = If(dataRow("FN_Razon_Social").ToString().Trim() = "", Nothing, dataRow("FN_Razon_Social").ToString().Trim())
            .AP_RUT = If(dataRow("AP_RUT").ToString().Trim() = "", Nothing, dataRow("AP_RUT").ToString().Trim())
            .AP_Razon_Social = If(dataRow("AP_Razon_Social").ToString().Trim() = "", Nothing, dataRow("AP_Razon_Social").ToString().Trim())
            .FS_Nemotecnico = If(dataRow("FS_Nemotecnico").ToString().Trim() = "", Nothing, dataRow("FS_Nemotecnico").ToString().Trim())
            .FS_Moneda = If(dataRow("FS_Moneda").ToString().Trim() = "", Nothing, dataRow("FS_Moneda").ToString().Trim())
            .VCS_Valor = If(dataRow("VCS_Valor").ToString().Trim() = "", Nothing, dataRow("VCS_Valor").ToString().Trim())
            .pca_TC_Valor = If(dataRow("TC_Valor").ToString().Trim() = "", Nothing, dataRow("TC_Valor").ToString().Trim())
            .ADCV_Cantidad = If(dataRow("ADCV_Cantidad").ToString().Trim() = "", Nothing, dataRow("ADCV_Cantidad").ToString().Trim())
            .RES_Cuotas = If(dataRow("RES_Cuotas").ToString().Trim() = "", Nothing, dataRow("RES_Cuotas").ToString().Trim())
            .PR_Saldo_Cuotas = If(dataRow("PR_Saldo_Cuotas").ToString().Trim() = "", Nothing, dataRow("PR_Saldo_Cuotas").ToString().Trim())
            .PR_Monto = If(dataRow("PR_Monto").ToString().Trim() = "", Nothing, dataRow("PR_Monto").ToString().Trim())
            .C_AP_Final_D = If(dataRow("C_AP_Final_D").ToString().Trim() = "", Nothing, dataRow("C_AP_Final_D").ToString().Trim())
            .C_AP_Final_I = If(dataRow("C_AP_Final_I").ToString().Trim() = "", Nothing, dataRow("C_AP_Final_I").ToString().Trim())
            .PR_DescEstado = If(dataRow("PR_DescEstado").ToString().Trim() = "", Nothing, dataRow("PR_DescEstado").ToString().Trim())
            .C_AP_Nac_Ext = If(dataRow("C_AP_Nac_Ext").ToString().Trim() = "", Nothing, dataRow("C_AP_Nac_Ext").ToString().Trim())
            .C_AP_Calificado = If(dataRow("C_AP_Calificado").ToString().Trim() = "", Nothing, dataRow("C_AP_Calificado").ToString().Trim())
            .C_AP_Rel_MAM = If(dataRow("C_AP_Rel_MAM").ToString().Trim() = "", Nothing, dataRow("C_AP_Rel_MAM").ToString().Trim())
            .C_AP_Limite = If(dataRow("C_AP_Limite").ToString().Trim() = "", Nothing, dataRow("C_AP_Limite").ToString().Trim())
            .C_Certificado = If(dataRow("C_Certificado").ToString().Trim() = "", Nothing, dataRow("C_Certificado").ToString().Trim())
            .C_Cuotas_C = If(dataRow("C_Cuotas_C").ToString().Trim() = "", Nothing, dataRow("C_Cuotas_C").ToString().Trim())
            .C_Cuotas_Certificar = If(dataRow("C_Cuotas_Certificar").ToString().Trim() = "", Nothing, dataRow("C_Cuotas_Certificar").ToString().Trim())
            'FC_PR_DescEstado
            'FC_SerieOptima
            .CANJE = If(dataRow("CANJE").ToString().Trim() = "", Nothing, dataRow("CANJE").ToString().Trim())
            ',pc.FC_CANJE
            .SerieOptima = If(dataRow("SerieOptima").ToString().Trim() = "", Nothing, dataRow("SerieOptima").ToString().Trim())
            .ContratoDistribucion = If(dataRow("ContratoDistribucion").ToString().Trim() = "", Nothing, dataRow("ContratoDistribucion").ToString().Trim())
            'SerieOptimaGrupo
            'AP_Contrato_Distribucion
            'montoTotalGrupo            

        End With
        Return reporte
    End Function

    Private Sub FillParameters(listaFondo As List(Of FondoDTO), sp As DBSqlServer.SqlServer.StoredProcedure)
        Dim listaRut As String = ""
        Dim AuxlistaRut As String = ""

        For Each Fondo As FondoDTO In listaFondo
            AuxlistaRut = listaRut
            listaRut = AuxlistaRut + "," + Fondo.Rut.ToString()
        Next

        sp.AgregarParametro("ListaRutFondo", listaRut, System.Data.SqlDbType.VarChar)
    End Sub

    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function GetListaProcesoPorId(PR_ID As Integer) As ReporteFechaCorteDTO
        Dim ListaReporte As ReporteFechaCorteDTO = New ReporteFechaCorteDTO
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_TRAER_PROCESO)
        Dim ds As DataSet

        Try
            sp.AgregarParametro("PR_ID", PR_ID, System.Data.SqlDbType.BigInt)
            ds = sp.ReturnDataSet()

            For Each dataRow As DataRow In ds.Tables(0).Rows
                ListaReporte = fillProcesoReporte(dataRow)
            Next

        Catch ex As Exception
            Throw
        End Try
        Return ListaReporte
    End Function


End Class