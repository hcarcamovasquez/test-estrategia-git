Public Class AportantesXGrupoDTO

    Public Property IdGrupo As Integer
    Public Property RutAportante As String
    Public Property NombreGrupo As String
    Public Property NombreAportante As String
    Public Property Estado As Integer
    Public Property FechaIngreso As Nullable(Of Date)
    Public Property UsuarioIngreso As String
    Public Property FechaModificacion As Nullable(Of Date)
    Public Property UsuarioModificacion As String

    Public Sub New(idGrupo As Integer, rut As String, nombreGrupo As String, nombreAportante As String, estado As Integer, fechaIngreso As Date?, usuarioIngreso As String, fechaModificacion As Date, usuarioModificacion As String)
        Me.IdGrupo = idGrupo
        Me.RutAportante = rut
        Me.NombreGrupo = nombreGrupo
        Me.NombreAportante = nombreAportante
        Me.Estado = estado
        Me.FechaIngreso = fechaIngreso
        Me.UsuarioIngreso = usuarioIngreso
        Me.FechaModificacion = fechaModificacion
        Me.UsuarioModificacion = usuarioModificacion
    End Sub

    Public Sub New()
    End Sub

End Class
