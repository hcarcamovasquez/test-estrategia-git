Imports DTO
Imports WSCanjeMandatorio.WSMonedas

Public Class MonedasDatos

    Public Function SelectAll() As List(Of MonedasDTO)
        Dim Ws = New WSCanjeMandatorio.WSMonedas
        Return Ws.SelectAll()
    End Function

    Public Function Guardar(moneda As MonedasDTO) As Boolean
        Dim Ws = New WSCanjeMandatorio.WSMonedas()
        Return Ws.Guardar(moneda)
    End Function


    Public Function getMonedasWS(tokken As String) As Boolean
        Dim Ws = New WSCanjeMandatorio.WSMonedas()
        Return Ws.GetMonedasWS(tokken)
    End Function

End Class
