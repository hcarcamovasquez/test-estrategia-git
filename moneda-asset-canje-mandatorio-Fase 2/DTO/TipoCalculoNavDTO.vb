Public Class TipoCalculoNavDTO
    Public Property ID As Decimal
    Public Property TipoTransaccion As String
    Public Property TipoCalculo As String

    Public Sub New(ID As Decimal, TipoTransaccion As String, TipoCalculo As String)
        Me.ID = ID
        Me.TipoTransaccion = TipoTransaccion
        Me.TipoCalculo = TipoCalculo
    End Sub

    Public Sub New()
    End Sub
End Class
