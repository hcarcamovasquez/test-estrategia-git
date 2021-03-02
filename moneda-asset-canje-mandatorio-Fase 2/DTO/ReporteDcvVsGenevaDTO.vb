
Public Class ReporteDcvVsGenevaDTO
    Public Property ID_Grupo As Integer
    Public Property Fecha_DCV As Date
    Public Property Fecha_VC As Date
    Public Property DCV_Nemo As String
    Public Property DCV_Cuotas As Integer
    Public Property GNV_Nemo As String
    Public Property GNV_Clase As String
    Public Property GNV_Cuotas As Integer
    Public Property TRS_Rescates As Integer
    Public Property TRS_Suscripciones As Integer
    Public Property TRS_Canje As Integer
    Public Property Recompra As Integer
    Public Property Total As Integer
    Public Property Diferencia As Integer
    Public Property Observaciones As String

    Public Sub New()
    End Sub
End Class
