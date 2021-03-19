Public Class TipoCambioDTO

    Private _estado As String

    Public Property Fecha As Date
    Public Property Codigo As String
    Public Property Valor As Decimal

    Public Property Estado() As String
        Get
            Return _estado
        End Get
        Set(value As String)
            _estado = IIf(value = "", "0", value)
        End Set
    End Property

    Public Property FechaIngreso As Date
    Public Property UsuarioIngreso As String
    Public Property FechaModificacion As Date
    Public Property UsuarioModificacion As String

    Public Sub New(fecha As Date, codigo As String, valor As Decimal, estado As String, fechaIngreso As Date, usuarioIngreso As String, fechaModificacion As Date, usuarioModificacion As String)
        Me.Fecha = fecha
        Me.Codigo = codigo
        Me.Valor = valor
        Me.Estado = estado
        Me.FechaIngreso = fechaIngreso
        Me.UsuarioIngreso = usuarioIngreso
        Me.FechaModificacion = fechaModificacion
        Me.UsuarioModificacion = usuarioModificacion
    End Sub
    Public ReadOnly Property CodigoRead As String
        Get
            If Codigo = "" Then
                Return StrDup(50, " ")
            Else
                Return Me.Codigo
            End If
        End Get
    End Property
    Public Sub New()
    End Sub
End Class