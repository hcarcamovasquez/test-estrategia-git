Imports DTO
Public Class VcSerieDTO
    Public Property FnRut As String
    Public Property FsNemotecnico As String
    Public Property Fecha As Date
    Public Property Valor As Decimal
    Public Property Estado As String
    Public Property FechaIngreso As Date
    Public Property UsuarioIngreso As String
    Public Property FechaModificacion As Date
    Public Property UsuarioModificacion As String

    Public Sub New(fnRut As String, fsNemotecnico As String, fecha As Date, valor As Decimal, estado As String, fechaIngreso As Date, usuarioIngreso As String, fechaModificacion As Date, usuarioModificacion As String)
        Me.FnRut = fnRut
        Me.FsNemotecnico = fsNemotecnico
        Me.Fecha = fecha
        Me.Valor = valor
        Me.Estado = estado
        Me.FechaIngreso = fechaIngreso
        Me.UsuarioIngreso = usuarioIngreso
        Me.FechaModificacion = fechaModificacion
        Me.UsuarioModificacion = usuarioModificacion
    End Sub

    Public ReadOnly Property NemotecnicoBusqueda As String
        Get
            If FsNemotecnico = "" Then
                Return StrDup(50, " ")
            Else
                Return Me.FsNemotecnico
            End If

        End Get
    End Property

    Public Sub New()
    End Sub
End Class