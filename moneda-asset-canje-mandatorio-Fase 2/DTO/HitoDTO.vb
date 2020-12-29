Public Class HitoDTO
    Public Property IdHito As Integer
    Public Property Rut As String
    Public Property FechaCorte As Date
    Public Property FechaCanje As Date
    Public Property Estado As String 
    Public Property FechaIngreso As Date 
    Public Property UsuarioIngreso As String
    Public Property FechaModificacion AS Date
    Public Property UsuarioModificacion As String
    Public Property NombreFondo As String


    Public Sub New(idHito As Integer, rut As String, fechaCorte As Date, fechaCanje As Date, estado As String, fechaIngreso As Date, usuarioIngreso As String, fechaModificacion As Date, usuarioModificacion As String, nombreFondo As String)
        Me.IdHito = idHito
        Me.Rut = rut
        Me.FechaCorte = fechaCorte
        Me.FechaCanje = fechaCanje
        Me.Estado = estado
        Me.FechaIngreso = fechaIngreso
        Me.UsuarioIngreso = usuarioIngreso
        Me.FechaModificacion = fechaModificacion
        Me.UsuarioModificacion = usuarioModificacion
        Me.NombreFondo = nombreFondo
    End Sub

    Public ReadOnly Property RutBusqueda As String
        Get
            If Rut = "" Then
                Return StrDup(50, " ")
            Else
                Return Me.Rut
            End If

        End Get
    End Property

    Public Sub New()
    End Sub
End Class