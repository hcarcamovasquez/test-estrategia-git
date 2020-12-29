Public Class VentanasRescateDTO
    Public Property VTRES_ID As Decimal
    Public Property FN_RUT As String
    Public Property FN_Nombre_Corto As String
    Public Property FS_Nemotecnico As String
    Public Property RES_Fecha_Solicitud As DateTime
    Public Property VTRES_Fecha_NAV As DateTime
    Public Property VTRES_Fecha_Pago As DateTime
    Public Property VTRES_Usuario_Ingreso As String
    Public Property VTRES_Fecha_Ingreso As DateTime
    Public Property VTRES_Usuario_Modificacion As String
    Public Property VTRES_Fecha_Modificacion As DateTime
    Public Property VTRES_Estado As Integer



    Public Sub New(VTRES_ID As Decimal, FN_RUT As String, FN_Nombre_Corto As String, FS_Nemotecnico As String, RES_Fecha_Solicitud As DateTime,
                   VTRES_Fecha_NAV As DateTime, VTRES_Fecha_Pago As DateTime, VTRES_Usuario_Ingreso As String,
                   VTRES_Fecha_Ingreso As DateTime, VTRES_Usuario_Modificacion As String, VTRES_Fecha_Modificacion As DateTime, VTRES_Estado As Integer)

        Me.VTRES_ID = VTRES_ID
        Me.FN_RUT = FN_RUT
        Me.FN_Nombre_Corto = FN_Nombre_Corto
        Me.FS_Nemotecnico = FS_Nemotecnico
        Me.RES_Fecha_Solicitud = RES_Fecha_Solicitud
        Me.VTRES_Fecha_NAV = VTRES_Fecha_NAV
        Me.VTRES_Fecha_Pago = VTRES_Fecha_Pago
        Me.VTRES_Usuario_Ingreso = VTRES_Usuario_Ingreso
        Me.VTRES_Fecha_Ingreso = VTRES_Fecha_Ingreso
        Me.VTRES_Usuario_Modificacion = VTRES_Usuario_Modificacion
        Me.VTRES_Fecha_Modificacion = VTRES_Fecha_Modificacion
        Me.VTRES_Estado = VTRES_Estado

    End Sub



    Public ReadOnly Property NombreFondoBusqueda As String
        Get
            If FN_Nombre_Corto = "" Then
                Return StrDup(50, " ")
            Else
                Return Me.FN_Nombre_Corto
            End If

        End Get
    End Property

    Public ReadOnly Property NemotecnicoBusqueda As String
        Get
            If FS_Nemotecnico = "" Then
                Return StrDup(50, " ")
            Else
                Return Me.FS_Nemotecnico
            End If

        End Get
    End Property

    Public ReadOnly Property RutRNombreCortoFondo As String
        Get
            If FN_RUT = "" And FN_Nombre_Corto = "" Then
                Return StrDup(50, " ")
            Else
                Return Me.FN_RUT + "/" + Me.FN_Nombre_Corto
            End If

        End Get
    End Property

    Public Sub New()
    End Sub
End Class
