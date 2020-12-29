Imports DTO
Imports WSCanjeMandatorio.WSReporte

Public Class ReporteDatos

    Public Function GetListaReporte(listaFondo As List(Of FondoDTO), FechaProceso As Nullable(Of Date), FechaCanje As Nullable(Of Date), txtCambio As Decimal) As List(Of ReporteFechaCorteDTO)
        Dim Ws = New WSCanjeMandatorio.WSReporte()
        Return Ws.GetListaReporte(listaFondo, FechaProceso, FechaCanje, txtCambio)
    End Function

    Public Function GetNuevaEvaluacion(proceso As ProcesoDTO) As ReporteFechaCorteDTO
        Dim Ws = New WSCanjeMandatorio.WSReporte()
        Return Ws.GetNuevaEvaluacion(proceso)
    End Function

    Public Function GetNuevaEvaluacionHija(proceso As ProcesoDTO) As ReporteFechaCorteDTO
        Dim Ws = New WSCanjeMandatorio.WSReporte()
        Return Ws.GetNuevaEvaluacionHija(proceso)
    End Function

    Public Function UpdateProcesoByID(proceso As ReporteFechaCorteDTO) As Boolean
        Dim Ws = New WSCanjeMandatorio.WSReporte()
        Return Ws.UpdateProcesoByID(proceso)
    End Function

    Public Function GetListaProcesoPorId(PR_ID As Integer) As ReporteFechaCorteDTO
        Dim Ws = New WSCanjeMandatorio.WSReporte()
        Return Ws.GetListaProcesoPorId(PR_ID)
    End Function
End Class
