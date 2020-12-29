Public Class UsuarioDTO
    Public Property US_Id As Integer
    Public Property US_Nombre As String
    Public Property US_Estado As Integer
    Public Property US_Perfil As Integer
    Public Property US_FechaIngreso As Nullable(Of Date)
    Public Property US_UsuarioIngreso As String
    Public Property US_FechaModificacion As Nullable(Of Date)
    Public Property US_UsuarioModificacion As String

    Public Sub New()
    End Sub

    Public Sub New(uS_Id As Integer, uS_Nombre As String, uS_Estado As Integer, uS_Perfil As Integer, uS_FechaIngreso As Date, uS_UsuarioIngreso As String, uS_FechaModificacion As Date, uS_UsuarioModificacion As String)
        Me.US_Id = uS_Id
        Me.US_Nombre = uS_Nombre
        Me.US_Estado = uS_Estado
        Me.US_Perfil = uS_Perfil
        Me.US_FechaIngreso = uS_FechaIngreso
        Me.US_UsuarioIngreso = uS_UsuarioIngreso
        Me.US_FechaModificacion = uS_FechaModificacion
        Me.US_UsuarioModificacion = uS_UsuarioModificacion
    End Sub

    Public ReadOnly Property Perfil_Chr As String
        Get
            If US_Perfil = 1 Then
                Return "Perfil Consulta"
            ElseIf US_Perfil = 2 Then
                Return "Perfil Full"
            ElseIf US_Perfil = 3 Then
                Return "Perfil Administrador"
            End If
            Return ""
        End Get
    End Property

    Public ReadOnly Property US_NombreValido As String
        Get
            If US_Nombre = "" Then
                Return StrDup(50, " ")
            Else
                Return Me.US_Nombre
            End If
        End Get
    End Property
End Class
