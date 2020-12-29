Imports DTO
Imports WSCanjeMandatorio.WSProcesoDetalle

Public Class ProcesoDetalleDatos

    Public Function InsertProcesoDetalle(procesoDetalle As ProcesoDetalleDTO) As Boolean
        Dim Ws = New WSCanjeMandatorio.WSProcesoDetalle()
        Return Ws.InsertProcesoDetalle(procesoDetalle)
    End Function

    Public Function UpdateProcesoDetalle(procesoDetalle As ProcesoDetalleDTO) As Boolean
        Dim Ws = New WSCanjeMandatorio.WSProcesoDetalle()
        Return Ws.UpdateProcesoDetalle(procesoDetalle)
    End Function

    Public Function DeleteProcesoDetalleById(procesoDetalle As ProcesoDetalleDTO) As Boolean
        Dim Ws = New WSCanjeMandatorio.WSProcesoDetalle()
        Return Ws.DeleteProcesoDetalleById(procesoDetalle)
    End Function

    Public Function SelectProcesoDetalle(procesoDetalle As ProcesoDetalleDTO) As List(Of ProcesoDetalleDTO)
        Dim Ws = New WSCanjeMandatorio.WSProcesoDetalle()
        Return Ws.SelectProcesoDetalle(procesoDetalle)
    End Function
End Class

