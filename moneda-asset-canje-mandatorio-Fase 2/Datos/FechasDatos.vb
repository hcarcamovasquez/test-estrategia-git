Imports DTO

Public Class FechasDatos

    Public Function ValidaDiaHabil(fecha As FechasDTO) As Integer
        Dim Ws = New WSCanjeMandatorio.WSFechas()
        Return Ws.ValidaDiaHabil(fecha)
    End Function

    Public Function getHabilSiguiente(fecha As FechasDTO) As Date
        Dim Ws = New WSCanjeMandatorio.WSFechas()
        Return Ws.getHabilSiguiente(fecha)

    End Function

    Public Function SumarDiasAFecha(fecha As FechasDTO) As Date
        Dim Ws = New WSCanjeMandatorio.WSFechas()
        Return Ws.SumarDiasAFecha(fecha)
    End Function
End Class
