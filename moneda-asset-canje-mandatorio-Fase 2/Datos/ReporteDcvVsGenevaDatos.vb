Imports DTO

Public Class ReporteDcvVsGenevaDatos
    Public Function GetInformeGeneva(ejecucionDto As Object) As List(Of ReporteDcvVsGenevaDTO)
        Dim Ws = New WSCanjeMandatorio.WSReporteDcvVsGeneva()
        Return Ws.GetInformeGeneva(ejecucionDto)
    End Function
End Class
