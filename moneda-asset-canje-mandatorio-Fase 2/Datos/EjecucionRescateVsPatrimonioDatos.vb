Imports DTO

Public Class EjecucionRescateVsPatrimonioDatos
    Public Function SelectFiltro(ejecucionDto As EjecucionRescateVsPatrimonioDTO) As List(Of EjecucionRescateVsPatrimonioDTO)
        Dim Ws = New WSCanjeMandatorio.WSEjecucionRescateVsPatrimonio()
        Return Ws.SelectFiltro(ejecucionDto)

    End Function
End Class
