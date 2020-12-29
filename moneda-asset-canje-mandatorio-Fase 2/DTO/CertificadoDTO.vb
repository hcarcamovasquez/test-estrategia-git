Public Class CertificadoDTO
    Public Property CT_Numero As Integer
    Public Property CT_Correlativo As Integer
    Public Property HT_ID As Integer
    Public Property HT_Corte As Date
    Public Property HT_Canje As Date
    Public Property CT_Fecha As Date
    Public Property AP_Rut As String
    Public Property AP_Razon_Social As String
    Public Property AP_Multifondo As String
    Public Property FS_Nemotecnico As String
    Public Property FN_Rut As String
    Public Property FN_Nombre_Corto As String
    Public Property CT_Cuotas As Decimal
    Public Property CT_Estado As String
    Public Property CT_Fecha_Ingreso As Date
    Public Property CT_Usuario_Ingreso As String
    Public Property CT_Fecha_Modificacion As Date
    Public Property CT_Usuario_Modificacion As String

    Public Sub New(Numero As Integer, Correlativo As Integer, Id_Hito As Integer, FechaCorte As Date, FechaCanje As Date, Fecha As Date, RutAportante As String, NombreAportante As String, RutFondo As String, NombreFondo As String,
                   Multifondo As String, Nemotecnico As String, Cuotas As Decimal, Estado As String,
                   FechaIngreso As Date, UsuarioIngreso As String, FechaModificacion As Date, UsuarioModificacion As String)

        Me.CT_Numero = Numero
        Me.CT_Correlativo = Correlativo
        Me.HT_ID = Id_Hito
        Me.HT_Corte = FechaCorte
        Me.HT_Canje = FechaCanje
        Me.CT_Fecha = Fecha
        Me.AP_Rut = RutAportante
        Me.AP_Razon_Social = NombreAportante
        Me.FN_Rut = RutFondo
        Me.FN_Nombre_Corto = NombreFondo
        Me.AP_Multifondo = Multifondo
        Me.FS_Nemotecnico = Nemotecnico
        Me.CT_Cuotas = Cuotas
        Me.CT_Estado = Estado
        Me.CT_Fecha_Ingreso = FechaIngreso
        Me.CT_Usuario_Ingreso = UsuarioIngreso
        Me.CT_Fecha_Modificacion = FechaModificacion
        Me.CT_Usuario_Modificacion = UsuarioModificacion

    End Sub

    Public ReadOnly Property RutRazonSocialAportante As String
        Get
            If AP_Rut = "" And AP_Razon_Social = "" Then
                Return StrDup(50, " ")
            Else
                Return Me.AP_Rut + "/" + Me.AP_Razon_Social
            End If

        End Get
    End Property

    Public ReadOnly Property RutRNombreCortoFondo As String
        Get
            If FN_Rut = "" And FN_Nombre_Corto = "" Then
                Return StrDup(50, " ")
            Else
                Return Me.FN_Rut + "/" + Me.FN_Nombre_Corto
            End If

        End Get
    End Property

    Public Sub New()
    End Sub
End Class

