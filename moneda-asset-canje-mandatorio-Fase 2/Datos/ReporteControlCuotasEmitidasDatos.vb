Imports DTO

Public Class ReporteControlCuotasEmitidasDatos
    Public Function SelectFiltro(ejecucionDto As ReporteControlCuotasEmitidasDTO) As List(Of ReporteControlCuotasEmitidasDTO)
        Dim Ws = New WSCanjeMandatorio.WSReporteControlCuotasEmitidas()
        Return Ws.SelectFiltro(ejecucionDto)

    End Function
End Class
