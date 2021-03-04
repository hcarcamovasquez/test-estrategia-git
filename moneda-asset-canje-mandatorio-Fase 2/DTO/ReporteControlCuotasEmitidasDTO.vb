Public Class ReporteControlCuotasEmitidasDTO
    Public Property FsNemotecnico As String
    Public Property FsMoneda As String
    Public Property FnFechaVencimiento As Date
    Public Property FnFechaEmision As Date
    Public Property FnCuotasEmitidas As String
    Public Property CuotasDisponibles As Double
    Public Property Acumulado As Double
    Public Property Anno_En_Curso As Double
    Public Property PorcentajeUltimaEmision As Integer
    Public Property TotalSuscritasUltimaEmision As Double
    Public Property TotalCuotasSuscritaspagadas As Double
    Public Property PorcentajeTotalCuotasSuscritasPagadas As Double
    Public Property dummy As String = " "


    Public Sub New()
    End Sub


End Class
