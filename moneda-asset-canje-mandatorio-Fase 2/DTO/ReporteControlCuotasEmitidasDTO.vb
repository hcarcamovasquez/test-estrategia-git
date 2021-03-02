Public Class ReporteControlCuotasEmitidasDTO
    Public Property FsNemotecnico As String
    Public Property FsMoneda As String
    Public Property FnFechaVencimiento As Date
    Public Property FnFechaEmision As Date
    Public Property FnCuotasEmitidas As String
    Public Property CuotasDisponibles As Integer
    Public Property Acumulado As Integer
    Public Property Annio As Integer
    Public Property PorcentajeUltimaEmision As Integer
    Public Property TotalSuscritasUltimaEmision As Integer
    Public Property TotalCuotasSuscritaspagadas As Integer
    Public Property PorcentajeTotalCuotasSuscritasPagadas As Integer
    Public Property dummy As String = " "


    Public Sub New()
    End Sub


End Class
