Public Class GrupoAportanteDTO
    Public Property GPA_Id As Integer
    Public Property GPA_Descripcion As String
    Public Property GPA_Estado As Integer
    Public Property GPA_FechaIngreso As Nullable(Of Date)
    Public Property GPA_UsuarioIngreso As String
    Public Property GPA_FechaModificacion As Nullable(Of Date)
    Public Property GPA_UsuarioModificacion As String

    Public Sub New(Id As Integer, Descripcion As String, Estado As Integer, FechaIngreso As Date,
                   UsuarioIngreso As String, FechaModificacion As Date, UsuarioModificacion As String)
        Me.GPA_Id = Id
        Me.GPA_Descripcion = Descripcion
        Me.GPA_Estado = Estado
        Me.GPA_FechaIngreso = FechaIngreso
        Me.GPA_UsuarioIngreso = UsuarioIngreso
        Me.GPA_FechaModificacion = FechaModificacion
        Me.GPA_UsuarioModificacion = UsuarioModificacion
    End Sub

    Public Sub New()
    End Sub

    Public ReadOnly Property IdDescripcion As String
        Get
            If GPA_Id = 0 And GPA_Descripcion = "" Then
                Return StrDup(50, " ")
            Else
                Return Me.GPA_Id.ToString() + "/" + Me.GPA_Descripcion
            End If

        End Get
    End Property

End Class
