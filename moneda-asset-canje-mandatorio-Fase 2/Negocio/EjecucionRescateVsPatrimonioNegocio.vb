Imports Datos
Imports DTO

Public Class EjecucionRescateVsPatrimonioNegocio

    Public Function [Select](ejecucionDto As EjecucionRescateVsPatrimonioDTO) As List(Of EjecucionRescateVsPatrimonioDTO)
        Dim lista As List(Of EjecucionRescateVsPatrimonioDTO) = New List(Of EjecucionRescateVsPatrimonioDTO)
        Dim datos As EjecucionRescateVsPatrimonioDatos = New EjecucionRescateVsPatrimonioDatos()

        lista = datos.SelectFiltro(ejecucionDto)

        Return lista
    End Function


End Class
