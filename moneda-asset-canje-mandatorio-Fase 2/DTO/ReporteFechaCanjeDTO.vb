Public Class ReporteFechaCorteDTO

    Public Property pr_id As Integer
    Public Property FN_RUT As String
    Public Property FN_Razon_Social As String
    Public Property PR_Directo_Indirecto As String
    Public Property FS_Grupo As String
    Public Property GPA_Descripcion As String
    Public Property AP_RUT As String
    Public Property AP_Razon_Social As String
    Public Property FS_Nemotecnico As String
    Public Property FS_Moneda As String
    Public Property VCS_Valor As Decimal
    Public Property ADCV_Cantidad As Decimal
    Public Property RES_Cuotas As Decimal
    Public Property Susc_Cuotas As Decimal
    Public Property CANJE As Decimal
    Public Property PR_Saldo_Cuotas As Decimal
    Public Property PR_Monto As Decimal
    Public Property PR_DescEstado As String
    Public Property SerieOptima As String
    Public Property C_AP_Nac_Ext As String
    Public Property C_AP_Calificado As String
    Public Property C_AP_Rel_MAM As String
    Public Property ContratoDistribucion As String
    Public Property C_AP_Limite As String
    Public Property C_Certificado As String
    Public Property C_AP_Final_I As String
    Public Property C_AP_Final_D As String
    Public Property C_Cuotas_C As Integer
    Public Property C_Cuotas_Certificar As Integer

    Public Property pca_FN_RUT As String
    Public Property pca_FN_Razon_Social As String
    Public Property pca_PR_Directo_Indirecto As String
    Public Property pca_FS_Grupo As String
    Public Property pca_GPA_Descripcion As String
    Public Property pca_AP_RUT As String
    Public Property pca_AP_Razon_Social As String
    Public Property pca_FS_Nemotecnico As String
    Public Property pca_FS_Moneda As String
    Public Property pca_VCS_Valor As Decimal
    Public Property pca_ADCV_Cantidad As Decimal
    Public Property pca_RES_Cuotas As Decimal
    Public Property pca_Susc_Cuotas As Decimal
    Public Property pca_CANJE As Decimal
    Public Property pca_PR_Saldo_Cuotas As Decimal
    Public Property pca_PR_Monto As Decimal
    Public Property pca_PR_DescEstado As String
    Public Property pca_SerieOptima As String
    Public Property pca_C_AP_Nac_Ext As String
    Public Property pca_C_AP_Calificado As String
    Public Property pca_C_AP_Rel_MAM As String
    Public Property pca_ContratoDistribucion As String
    Public Property pca_C_AP_Limite As String
    Public Property pca_C_Certificado As String
    Public Property pca_C_AP_Final_I As String
    Public Property pca_C_Cuotas_C As Integer
    Public Property pca_C_Cuotas_Certificar As Integer
    Public Property pca_PR_ID As Integer

    Public Property pca_TC_Valor As Decimal

    Public Property NavCuotaEntrante As Decimal 'Revisar de donde sale éste campo
    Public Property ValorCambio As Decimal
    Public Property Observacion As String 'Revisar de donde sale éste campo
    Public Property Cumple As String 'Revisar de donde sale éste campo


    Public Property monto_saliente As Decimal
    Public Property NAV_saliente_CLP As Decimal
    Public Property monto_saliente_CLP As Decimal
    ' Public Property Factor As Decimal
    Public Property cuotas_entrantes As Decimal
    Public Property monto_entrante As Decimal
    ' Public Property NavCuotaEntranteCLP As Decimal
    Public Property monto_entrante_CLP As Decimal
    ' Public Property Diferencia As Decimal
    Public Property diferencia_CLP As Decimal

    Public Property diferenciaMoneda As Decimal

    Public Property PRD_factor As Decimal

    Public Property Clave As Integer

    Public Property PRCuotasSalientes As Decimal

    Public Property x_MontoSaliente As String
    Public Property x_MontoEntrante As String
    Public Property x_MontoEntranteCLP As String
    Public Property prd_cuotasentrante As String
    Public Property x_Factor As String
    Public Property prd_diferencia As String
    Public Property prd_diferenciaclp As String
    Public Property prd_naventrante As String
    Public Property fs_monedaentrante As String
    Public Property prd_observaciones As String
    Public Property x_pr_descestado As String
    Public Property x_c_ap_nac_ext As String
    Public Property x_c_ap_calificado As String
    Public Property x_c_ap_rel_mam As String
    Public Property x_c_cuotas_certificar As String
    Public Property x_fs_nemotecnico As String
    Public Property x_c_ap_limite As String
    Public Property x_c_certificado As String
    Public Property x_c_ap_final_i As String
    Public Property prd_cuotassalientes As String
    Public Property prd_accion As String

    Public Property NavCuotaEntranteCLP As Decimal

    Public Sub New(FN_Razon_Social As String, PR_Directo_Indirecto As String, FS_Grupo As String, GPA_Descripcion As String, AP_RUT As String, AP_Razon_Social As String, FS_Nemotecnico As String, FS_Moneda As String, VCS_Valor As Decimal, ADCV_Cantidad As Decimal, RES_Cuotas As Decimal, Susc_Cuotas As Decimal, CANJE As Decimal, PR_Saldo_Cuotas As Decimal, PR_Monto As Decimal, PR_DescEstado As String, SerieOptima As String, C_AP_Nac_Ext As String, C_AP_Calificado As String, C_AP_Rel_MAM As String, ContratoDistribucion As String, C_AP_Limite As String, C_Certificado As String, C_AP_Final_I As String, C_Cuotas_C As Integer, C_Cuotas_Certificar As Integer, NavCuotaEntrante As Decimal, ValorCambio As Decimal, Observacion As String, Cumple As String)
        Me.FN_Razon_Social = FN_Razon_Social
        Me.PR_Directo_Indirecto = PR_Directo_Indirecto
        Me.FS_Grupo = FS_Grupo
        Me.GPA_Descripcion = GPA_Descripcion
        Me.AP_RUT = AP_RUT
        Me.AP_Razon_Social = AP_Razon_Social
        Me.FS_Nemotecnico = FS_Nemotecnico
        Me.FS_Moneda = FS_Moneda
        Me.VCS_Valor = VCS_Valor
        Me.ADCV_Cantidad = ADCV_Cantidad
        Me.RES_Cuotas = RES_Cuotas
        Me.Susc_Cuotas = Susc_Cuotas
        Me.CANJE = CANJE
        Me.PR_Saldo_Cuotas = PR_Saldo_Cuotas
        Me.PR_Monto = PR_Monto
        Me.PR_DescEstado = PR_DescEstado
        Me.SerieOptima = SerieOptima
        Me.C_AP_Nac_Ext = C_AP_Nac_Ext
        Me.C_AP_Calificado = C_AP_Calificado
        Me.C_AP_Rel_MAM = C_AP_Rel_MAM
        Me.ContratoDistribucion = ContratoDistribucion
        Me.C_AP_Limite = C_AP_Limite
        Me.C_Certificado = C_Certificado
        Me.C_AP_Final_I = C_AP_Final_I
        Me.C_Cuotas_C = C_Cuotas_C
        Me.C_Cuotas_Certificar = C_Cuotas_Certificar
        Me.NavCuotaEntrante = NavCuotaEntrante
        Me.ValorCambio = ValorCambio
        Me.Observacion = Observacion
        Me.Cumple = Cumple
    End Sub

    'CAMPOS PARA SECCION CANJE
    Public ReadOnly Property CN_Nav_CLP_Saliente As Decimal
        Get
            Return Me.VCS_Valor * Me.ValorCambio
        End Get
    End Property

    Public ReadOnly Property MontoSalienteCLP As Decimal
        Get
            Return Me.CN_Nav_CLP_Saliente * Me.PR_Saldo_Cuotas
        End Get
    End Property

    'Public ReadOnly Property NavCuotaEntranteCLP As Decimal
    '    Get
    '        Return NavCuotaEntrante * ValorCambio
    '    End Get
    'End Property

    Public ReadOnly Property Factor As Decimal
        Get
            If NavCuotaEntrante > 0 And VCS_Valor > 0 Then
                Return NavCuotaEntrante / VCS_Valor
            Else
                Return 0
            End If
        End Get
    End Property

    Public ReadOnly Property CuotasEntrante As Decimal
        Get
            If Factor > 0 And PR_Saldo_Cuotas > 0 Then
                Return Factor * PR_Saldo_Cuotas
            Else
                Return 0
            End If
        End Get
    End Property

    Public ReadOnly Property MontoEntrante As Decimal
        Get
            Return CuotasEntrante * NavCuotaEntrante
        End Get
    End Property

    Public ReadOnly Property MontoEntranteCLP As Decimal
        Get
            Return CuotasEntrante * NavCuotaEntranteCLP
        End Get
    End Property

    Public ReadOnly Property Diferencia As Decimal
        Get
            Return monto_saliente - monto_entrante
        End Get
    End Property

    Public ReadOnly Property DiferenciaCLP As Decimal
        Get
            Return MontoSalienteCLP - MontoEntranteCLP
        End Get
    End Property

    Public Sub New()
    End Sub
End Class