Imports DTO
Imports WSCanjeMandatorio.WSPatrimonio

Public Class PatrimonioDatos

    Public Function GetPatrimonio(Patrimonio As DTO.PatrimonioDTO) As PatrimonioDTO
        Dim Ws = New WSCanjeMandatorio.WSPatrimonio()
        Return Ws.GetPatrimonio(Patrimonio)
    End Function

End Class
