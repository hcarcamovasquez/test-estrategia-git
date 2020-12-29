Public Class AportanteDTO
    Public Property Rut As String
    Public Property RazonSocial As String
    Public Property Multifondo As String
    Public Property TipoAportante As String
    Public Property NacExt As String
    Public Property Calificado As String
    Public Property Intermediario As String
    Public Property RelacionMan As String
    Public Property Distribucion As String
    Public Property Estado As Integer
    Public Property FechaIngreso As Nullable(Of Date)
    Public Property UsuarioIngreso As String
    Public Property FechaModificacion As Nullable(Of Date)
    Public Property UsuarioModificacion As String
    Public Property Documentacion As String 'Agrega nuevo parametro para Documentacion Aportes Joseph Caceres

    Public Sub New(rut As String, razonSocial As String, multifondo As String, TipoAportante As String, nacExt As String, calificado As String, intermediario As String, relacionMan As String, distribucion As String, estado As Integer, fechaIngreso As Date, usuarioIngreso As String, fechaModificacion As Date, usuarioModificacion As String, documentacion As String)
        Me.Rut = rut
        Me.RazonSocial = razonSocial
        Me.Multifondo = multifondo
        Me.TipoAportante = TipoAportante
        Me.NacExt = nacExt
        Me.Calificado = calificado
        Me.Intermediario = intermediario
        Me.RelacionMan = relacionMan
        Me.Distribucion = distribucion
        Me.Estado = estado
        Me.FechaIngreso = fechaIngreso
        Me.UsuarioIngreso = usuarioIngreso
        Me.FechaModificacion = fechaModificacion
        Me.UsuarioModificacion = usuarioModificacion
        Me.Documentacion = documentacion 'agregar documentacion de aportes JC
    End Sub


    Public ReadOnly Property RazonSocialMultifondo As String
        Get
            Return Me.RazonSocial + " (" + Me.Multifondo + ")"
        End Get
    End Property

    Public ReadOnly Property RutRazonSocial As String
        Get
            If Rut = "" And RazonSocial = "" Then
                Return StrDup(50, " ")
            Else
                Return Me.Rut + "/" + Me.RazonSocial
            End If

        End Get
    End Property

    Public Sub New()
    End Sub

End Class
