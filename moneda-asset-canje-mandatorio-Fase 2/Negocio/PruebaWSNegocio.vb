Public Class PruebaWSNegocio
    Public Function ConsultarPrueba(fondo As DTO.FondoDTO) As List(Of DTO.FondoDTO)
        Dim fondoDatos As New Datos.PruebaWSDatos
        Return fondoDatos.ConsultaPrueba(fondo)
    End Function
End Class
