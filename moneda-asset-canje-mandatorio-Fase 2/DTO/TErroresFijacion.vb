Public Class TErroresFijacion
    Public Property Id As Integer
    Public Property MsgError As String
    Public Property Cantidad As Integer
    Public Property InformacionAdicional As String

    Public Sub New(id As Integer, msgError As String, cantidad As Integer)
        Me.Id = id
        Me.MsgError = msgError
        Me.Cantidad = cantidad
        Me.InformacionAdicional = ""
    End Sub
    Public Sub New()

    End Sub
End Class
