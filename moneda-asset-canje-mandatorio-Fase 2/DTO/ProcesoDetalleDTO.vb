Public Class ProcesoDetalleDTO

    Public Property PRD_ID As Integer
    Public Property PR_ID As Integer
    Public Property FS_Nemotecnico As String
    Public Property PRD_CuotasEntrante As Decimal
    Public Property PRD_NAVEntrante As Decimal
    Public Property PRD_NAVEntranteCLP As Decimal
    Public Property PRD_MontoEntrante As Decimal
    Public Property PRD_MontoEntranteCLP As Decimal
    Public Property PRD_Factor As Decimal
    Public Property PRD_CuotasSalientes As String
    Public Property PRD_MontoSaliente As Decimal
    Public Property PRD_MontoSalienteCLP As Decimal
    Public Property PRD_Diferencia As Decimal
    Public Property PRD_DiferenciaCLP As Decimal
    Public Property FS_MonedaEntrante As String
    Public Property PRD_Observaciones As String
    Public Property PR_DescEstado As String
    Public Property C_AP_Nac_Ext As String
    Public Property C_AP_Calificado As String
    Public Property C_AP_Rel_MAM As String
    Public Property C_AP_Limite As String
    Public Property C_Certificado As String
    Public Property C_AP_Final_I As String
    Public Property C_Cuotas_C As Decimal
    Public Property C_Cuotas_Certificar As Decimal
    Public Property PRD_Accion As String

    Public Property filaRowPadre As Integer
    Public Property Clave As Integer
    Public Property NemoSeleccionado As String

    Public Property PRCuotasSalientes As Decimal

    Public Sub New()

    End Sub

    Public ReadOnly Property Factor As Decimal
        Get
            If PRD_NAVEntrante > 0 And PRD_CuotasEntrante > 0 Then
                Return PRD_NAVEntrante / PRD_CuotasEntrante
            Else
                Return 0
            End If
        End Get
    End Property

End Class