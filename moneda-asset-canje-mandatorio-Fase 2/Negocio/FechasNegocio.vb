Imports DTO
Imports Datos

Public Class FechasNegocio
    Public Function Insert(fecha As FechasDTO) As FechasDTO
        Throw New NotImplementedException
    End Function

    Public Function Update(fecha As FechasDTO) As FechasDTO
        Throw New NotImplementedException
    End Function

    Public Function Delete(fecha As FechasDTO) As FechasDTO
        Throw New NotImplementedException
    End Function

    Public Function ValidaDiaHabil(fecha As FechasDTO) As String
        Dim datos As FechasDatos = New FechasDatos()
        Dim f As Integer

        f = datos.ValidaDiaHabil(fecha)
        If f = 1 Then
            Return "Festivo"
        ElseIf f = 2 Then
            Return "No_Habil"
        ElseIf f = 0 Then
            Return ""
        Else
            Return "error"
        End If
    End Function

    Public Function getHabilSiguiente(fecha As FechasDTO) As Date
        Dim datos As FechasDatos = New FechasDatos()
        Return datos.getHabilSiguiente(fecha)
    End Function

    Public Function getPaisDeLaMoneda(moneda As String) As RelacionMonedaPaisDto
        ' buscar la moneda, sacar el pais y devolverlo
        Return New RelacionMonedaPaisDto()
    End Function

    Public Function SumaDiasAFecha(fecha As FechasDTO) As Date
        Dim datos As FechasDatos = New FechasDatos()
        Return datos.SumarDiasAFecha(fecha)
    End Function
End Class
