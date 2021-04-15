Public Class SuscripcionDTO
    Inherits TransaccionBaseDTO

    Public Property IdSuscripcion As Integer
    Public Property TipoTransaccion As String
    Public Property FechaIntencion As Date
    Public Property RutAportante As String
    'Public Property Multifondo As Char
    Public Property Multifondo As String
    Public Property RutFondo As String
    Public Property Nemotecnico As String
    Public Property CuotasASuscribir As Decimal
    Public Property Moneda_Pago As String
    Public Property FechaNAV As Date
    Public Property FechaSuscripcion As Date
    Public Property FechaTC As Date
    Public Property NAV As Double
    Public Property Monto As Double
    Public Property NAVCLP As Double
    Public Property MontoCLP As Double
    Public Property TipoCambio As String
    Public Property ContratoFondo As String
    Public Property RevisionPoderes As String
    Public Property Estado As Integer
    Public Property Observaciones As String
    Public Property FechaDCV As Date
    Public Property CuotasDCV As Integer
    Public Property RescatesTransitos As Integer
    Public Property SuscripcionesTransito As Integer
    Public Property CanjesTransito As Integer
    Public Property CuotasDisponibles As Integer
    Public Property FijacionNAV As String
    Public Property FijacionTC As String
    Public Property RazonSocial As String
    Public Property FondoNombreCorto As String
    Public Property SerieNombreCorto As String
    Public Property MonedaSerie As String
    Public Property TcObservado As String
    Public Property EstadoSuscripcion As String

    Public Property CuotasEmitidas As Decimal
    Public Property FnAcumulada As Decimal
    Public Property ScActual As Decimal
    Public Property ScUtilizado As Decimal
    Public Property ScDisponibles As Decimal

    Public Property ScUsuarioIngreso As String
    Public Property ScFechaIngreso As DateTime
    Public Property ScUsuarioModificacion As String
    Public Property ScFechaModificacion As DateTime

    Public Property CountAP As Integer
    Public Property CountFN As Integer
    Public Property CountFS As Integer

    'Jovb R3 
    Public Property EstadoIntencion As String

    Public Sub New(IdSuscripcion As Integer, TipoTransaccion As String, FechaIntencion As Date, RutAportante As String, Multifondo As Char, RutFondo As String,
                   Nemotecnico As String, CuotasASuscribir As Decimal, Moneda_Pago As String, FechaNAV As Date, FechaSuscripcion As Date, FechaTC As Date,
                   NAV As Double, Monto As Double, NAVCLP As Double, MontoCLP As Double, TipoCambio As String, ContratoFondo As String, RevisionPoderes As String,
                   Estado As Integer, Observaciones As String, FechaDCV As Date, CuotasDCV As Integer, RescatesTransitos As Integer,
                   SuscripcionesTransito As Integer, CanjesTransito As Integer, CuotasDisponibles As Integer, FijacionNAV As String, FijacionTC As String,
                   RazonSocial As String, FondoNombreCorto As String, SerieNombreCorto As String, MonedaSerie As String, TcObservado As String, EstadoSuscripcion As String,
                   CuotasEmitidas As Decimal, FnAcumulada As Decimal, ScActual As Decimal, ScUtilizado As Decimal, ScDisponibles As Decimal, ScUsuarioModificacion As String,
                   ScFechaModificacion As Date, CountAP As Integer, CountFN As Integer, CountFS As Integer, ScUsuarioIngreso As String, ScFechaIngreso As Date)

        Me.IdSuscripcion = IdSuscripcion
        Me.TipoTransaccion = TipoTransaccion
        Me.FechaIntencion = FechaIntencion
        Me.RutAportante = RutAportante
        Me.Multifondo = Multifondo
        Me.RutFondo = RutFondo
        Me.Nemotecnico = Nemotecnico
        Me.CuotasASuscribir = CuotasASuscribir
        Me.Moneda_Pago = Moneda_Pago
        Me.FechaNAV = FechaNAV
        Me.FechaSuscripcion = FechaSuscripcion
        Me.FechaTC = FechaTC
        Me.NAV = NAV
        Me.Monto = Monto
        Me.NAVCLP = NAVCLP
        Me.MontoCLP = MontoCLP
        Me.TipoCambio = TipoCambio
        Me.ContratoFondo = ContratoFondo
        Me.RevisionPoderes = RevisionPoderes
        Me.Estado = Estado
        Me.Observaciones = Observaciones
        Me.FechaDCV = FechaDCV
        Me.CuotasDCV = CuotasDCV
        Me.RescatesTransitos = RescatesTransitos
        Me.SuscripcionesTransito = SuscripcionesTransito
        Me.CanjesTransito = CanjesTransito
        Me.CuotasDisponibles = CuotasDisponibles
        Me.FijacionNAV = FijacionNAV
        Me.FijacionTC = FijacionTC
        Me.RazonSocial = RazonSocial
        Me.FondoNombreCorto = FondoNombreCorto
        Me.SerieNombreCorto = SerieNombreCorto
        Me.MonedaSerie = MonedaSerie
        Me.TcObservado = TcObservado
        Me.EstadoSuscripcion = EstadoSuscripcion

        ' JOVB
        Me.CuotasEmitidas = CuotasEmitidas
        Me.FnAcumulada = FnAcumulada
        Me.ScActual = ScActual

        Me.ScFechaIngreso = ScFechaIngreso
        Me.ScUsuarioIngreso = ScUsuarioIngreso
        Me.ScFechaModificacion = ScFechaIngreso
        Me.ScUsuarioModificacion = ScUsuarioModificacion
        Me.CountAP = CountAP
        Me.CountFN = CountFN
        Me.CountFS = CountFS
    End Sub

    Public Sub New()
    End Sub
    Public ReadOnly Property NemotecnicoRead As String
        Get
            If Nemotecnico = "" Then
                Return StrDup(50, " ")
            Else
                Return Me.Nemotecnico
            End If
        End Get
    End Property
    Public ReadOnly Property RutFondoRead As String
        Get
            If FondoNombreCorto = "" And RutFondo = "" Then
                Return StrDup(50, " ")
            Else
                Return Me.RutFondo + "/" + Me.FondoNombreCorto
            End If
        End Get
    End Property
    Public ReadOnly Property RutAportanteRead As String
        Get
            If RutAportante = "" Then
                Return StrDup(50, " ")
            Else
                Return Me.RutAportante
            End If
        End Get
    End Property
    Public ReadOnly Property MultifondoRead As String
        Get
            If Multifondo = "" Then
                Return StrDup(50, " ")
            Else
                Return Me.Multifondo
            End If
        End Get
    End Property
    Public ReadOnly Property RazonSocialRead As String
        Get
            If RazonSocial = "" And RutAportante = "" Then
                Return StrDup(50, " ")
            Else
                Return Me.RutAportante + "/" + Me.RazonSocial
            End If
        End Get
    End Property

    Public ReadOnly Property ScUtilizadoAUX As String
        Get
            Return FnAcumulada + ScActual
        End Get
    End Property

    Public ReadOnly Property ScDisponiblesAUX As String
        Get
            Return CuotasEmitidas + ScUtilizadoAUX
        End Get
    End Property

    Public ReadOnly Property NavFormat As String
        Get
            'If MonedaSerie = "USD" Then
            '    Return String.Format("{0:N2}", NAV)
            'Else
            Return String.Format("{0:N6}", NAV)
            'End If
        End Get
    End Property

    Public ReadOnly Property NAVCLPFormat As String
        Get
            Return String.Format("{0:N4}", NAVCLP)
        End Get
    End Property

    Public ReadOnly Property MontoFormat As String
        Get
            If MonedaSerie = "CLP" Then
                Return String.Format("{0:N0}", Monto)
            Else
                Return String.Format("{0:N2}", Monto)
            End If
        End Get
    End Property
    Public ReadOnly Property MontoCLPFormat As String
        Get
            'If MonedaSerie = "CLP" Then
            Return String.Format("{0:N0}", MontoCLP)
            'Else
            'Return String.Format("{0:N2}", MontoCLP)
            'End If
        End Get
    End Property

End Class