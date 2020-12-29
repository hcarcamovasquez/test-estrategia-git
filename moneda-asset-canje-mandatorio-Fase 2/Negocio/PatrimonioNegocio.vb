Imports DTO
Imports Datos

Public Class PatrimonioNegocio
    Public Function GetPatrimonio(Patrimonio As PatrimonioDTO) As PatrimonioDTO
        Dim PatrimonioRetorno As PatrimonioDTO
        Dim PatrimonioDatos As New Datos.PatrimonioDatos

        PatrimonioRetorno = PatrimonioDatos.GetPatrimonio(Patrimonio)
        Return PatrimonioRetorno
    End Function
End Class
