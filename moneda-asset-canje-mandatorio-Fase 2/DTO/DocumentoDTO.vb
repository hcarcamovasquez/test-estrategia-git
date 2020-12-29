Imports DTO

Public Class DocumentoDTO
    Public Property NumeroActual As Decimal
    Public Property NumeroAnterior As Decimal
    Public Property NumeroSiguiente As Decimal


    Public Sub New(NumeroActual As Decimal, NumeroAnterior As Decimal, NumeroSiguiente As Decimal)
        Me.NumeroActual = NumeroActual
        Me.NumeroAnterior = NumeroAnterior
        Me.NumeroSiguiente = NumeroSiguiente
    End Sub

    Public Sub New()
    End Sub
End Class
