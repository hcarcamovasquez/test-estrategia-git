Imports DTO
Imports WSCanjeMandatorio.WSPrueba


Public Class PruebaWSDatos

    Public Function ConsultaPrueba(fondo As DTO.FondoDTO) As List(Of DTO.FondoDTO)
        Dim ws As New WSCanjeMandatorio.WSPrueba
        Dim listaFondos As New List(Of DTO.FondoDTO)
        listaFondos = ws.FNConsultar(fondo)
        Return listaFondos
    End Function


End Class
