Imports DTO

Public class FondoDTO
    Public Property Rut As String
    Public Property RazonSocial As String
    Public Property NombreCorto As String
    Public Property Estado As Integer
    Public Property FechaIngreso As Nullable(Of Date)
    Public Property UsuarioIngreso As String
    Public Property FechaModificacion As Nullable(Of Date)
    Public Property UsuarioModificacion As String

    Public Property CuotasEmitidas As Nullable(Of Decimal)
    Public Property FechaEmision As Nullable(Of Date)
    Public Property FechaVencimiento As Nullable(Of Date)
    Public Property Acumulado As Nullable(Of Decimal)

    Public Property ControlCuotas As Integer

    Public Sub New(rut As String, razonsocial As String, nombreCorto As String, estado As String, fechaIngreso As Date, usuarioIngreso As String, fechaModificacion As Date, usuarioModificacion As String, CuotasEmitidas As Decimal, FechaEmision As Date, FechaVencimiento As Date, Acumulado As Decimal)
        Me.Rut = rut
        Me.RazonSocial = razonsocial
        Me.NombreCorto = nombreCorto
        Me.Estado = estado
        Me.FechaIngreso = fechaIngreso
        Me.UsuarioIngreso = usuarioIngreso
        Me.FechaModificacion = fechaModificacion
        Me.UsuarioModificacion = usuarioModificacion
        Me.CuotasEmitidas = CuotasEmitidas
        Me.FechaEmision = FechaEmision
        Me.FechaVencimiento = FechaVencimiento
        Me.Acumulado = Acumulado
    End Sub

    Public ReadOnly Property RutRazonSocial As String
        Get
            If Rut = "" And RazonSocial = "" Then
                Return StrDup(50, " ")
            Else
                Return Me.Rut + "/" + Me.RazonSocial
            End If
        End Get
    End Property

    Public ReadOnly Property RutBusqueda As String
        Get
            If Rut = "" Then
                Return StrDup(50, " ")
            Else
                Return Me.Rut
            End If

        End Get
    End Property

    Public ReadOnly Property ControlDeCuotas As String

        Get
            If ControlCuotas = 0 Then
                Return "NO"
            Else
                Return "SI"
            End If
        End Get

    End Property

    Public Sub New()
    End Sub

End Class