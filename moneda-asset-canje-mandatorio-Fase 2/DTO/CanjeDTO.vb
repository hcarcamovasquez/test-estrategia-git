Public Class CanjeDTO
    Inherits TransaccionBaseDTO

    Public Property IdCanje As Integer
    Public Property TipoTransaccion As String
    Public Property RutAportante As String
    Public Property Multifondo As String
    Public Property NombreAportante As String
    Public Property RutFondo As String
    Public Property NombreFondo As String
    Public Property FechaNavSaliente As Date
    Public Property FechaSolicitud As Date
    Public Property FechaObservado As Date
    Public Property NemotecnicoSaliente As String
    Public Property NombreSerieSaliente As String
    Public Property MonedaSaliente As String
    Public Property CuotaSaliente As Decimal
    Public Property NavSaliente As Decimal
    Public Property MontoSaliente As Decimal
    Public Property NavCLPSaliente As Decimal
    Public Property MontoCLPSaliente As Decimal
    Public Property Factor As Decimal
    Public Property Diferencia As Decimal
    Public Property DiferenciaCLP As Decimal
    Public Property NemotecnicoEntrante As String
    Public Property NombreSerieEntrante As String
    Public Property MonedaEntrante As String
    Public Property CuotaEntrante As Decimal
    Public Property NavEntrante As Decimal
    Public Property MontoEntrante As Decimal
    Public Property NavCLPEntrante As Decimal
    Public Property MontoCLPEntrante As Decimal
    Public Property ContratoGeneral As String
    Public Property RevisionPoderes As String
    Public Property EstadoCanje As String
    Public Property Observaciones As String
    Public Property FechaActual As Date
    Public Property Cuotas As Decimal
    Public Property RescateTransito As Decimal
    Public Property SuscripcionTransito As Integer
    Public Property CanjeTransito As Integer
    Public Property CuotasDisponibles As Decimal
    Public Property FijacionNav As String
    Public Property FijacionTC As String
    Public Property Estado As Integer
    Public Property FechaIngreso As Date
    Public Property UsuarioIngreso As String
    Public Property FechaModificacion As Date
    Public Property UsuarioModificacion As String
    Public Property FechaNavEntrante As Date
    Public Property TipoCambio As String

    Public Property FechaCanjeDate As Date

    ' toido 
    Public Sub New(idCanje As Integer, tipoTransaccion As String, rutAportante As String, multifondo As String, nombreAportante As String, rutFondo As String, nombreFondo As String, fechaNavSaliente As Date, fechaSolicitud As Date, fechaObservado As Date, nemotecnicoSaliente As String,
                   nombreSerieSaliente As String, monedaSaliente As String, cuotaSaliente As Decimal, navSaliente As Decimal, montoSaliente As Decimal, navCLPSaliente As Decimal, montoCLPSaliente As Decimal, factor As Decimal, diferencia As Decimal,
                   diferenciaCLP As Decimal, nemotecnicoEntrante As String, nombreSerieEntrante As String, monedaEntrante As String, cuotaEntrante As Decimal, navEntrante As Decimal, montoEntrante As Decimal, navCLPEntrante As Decimal, montoCLPEntrante As Decimal, contratoGeneral As String,
                   revisionPoderes As String, estadoCanje As String, observaciones As String, fechaActual As Date, cuotas As Decimal, rescateTransito As Decimal, suscripcionTransito As Integer, canjeTransito As Integer, cuotasDisponibles As Decimal, fijacionNav As String, fijacionTC As String,
                   estado As Integer, fechaIngreso As Date, usuarioIngreso As String, fechaModificacion As Date, usuarioModificacion As String, fechaNavEntrante As Date, tipoCambio As String)

        Me.IdCanje = idCanje
        Me.TipoTransaccion = tipoTransaccion
        Me.RutAportante = rutAportante
        Me.Multifondo = multifondo
        Me.NombreAportante = nombreAportante
        Me.RutFondo = rutFondo
        Me.NombreFondo = nombreFondo
        Me.FechaNavSaliente = fechaNavSaliente
        Me.FechaSolicitud = fechaSolicitud
        Me.FechaObservado = fechaObservado
        Me.NemotecnicoSaliente = nemotecnicoSaliente
        Me.NombreSerieSaliente = nombreSerieSaliente
        Me.MonedaSaliente = monedaSaliente
        Me.CuotaSaliente = cuotaSaliente
        Me.NavSaliente = navSaliente
        Me.MontoSaliente = montoSaliente
        Me.NavCLPSaliente = navCLPSaliente
        Me.MontoCLPSaliente = montoCLPSaliente
        Me.Factor = factor
        Me.Diferencia = diferencia
        Me.DiferenciaCLP = diferenciaCLP
        Me.NemotecnicoEntrante = nemotecnicoEntrante
        Me.NombreSerieEntrante = nombreSerieEntrante
        Me.MonedaEntrante = monedaEntrante
        Me.CuotaEntrante = cuotaEntrante
        Me.NavEntrante = navEntrante
        Me.MontoEntrante = montoEntrante
        Me.NavCLPEntrante = navCLPEntrante
        Me.MontoCLPEntrante = montoCLPEntrante
        Me.ContratoGeneral = contratoGeneral
        Me.RevisionPoderes = revisionPoderes
        Me.EstadoCanje = estadoCanje
        Me.Observaciones = observaciones
        Me.FechaActual = fechaActual
        Me.Cuotas = cuotas
        Me.RescateTransito = rescateTransito
        Me.SuscripcionTransito = suscripcionTransito
        Me.CanjeTransito = canjeTransito
        Me.CuotasDisponibles = cuotasDisponibles
        Me.FijacionNav = fijacionNav
        Me.FijacionTC = fijacionTC
        Me.Estado = estado
        Me.FechaIngreso = fechaIngreso
        Me.UsuarioIngreso = usuarioIngreso
        Me.FechaModificacion = fechaModificacion
        Me.UsuarioModificacion = usuarioModificacion
        Me.FechaNavEntrante = fechaNavEntrante
        Me.TipoCambio = tipoCambio
    End Sub


    Public ReadOnly Property cuotaSalientePaso As Int64
        Get
            Return Math.Floor(CuotaSaliente)
        End Get
    End Property

    Public ReadOnly Property cuotaEntrantePaso As Int64

        Get
            Return Math.Floor(CuotaEntrante)
        End Get
    End Property

    Public ReadOnly Property cuotasDisponiblesPaso As Int64
        Get
            Return Math.Floor(CuotasDisponibles)
        End Get
    End Property

    Public ReadOnly Property NavSalientePaso As Decimal
        Get
            Return Math.Round(NavSaliente, 6)
        End Get
    End Property

    Public ReadOnly Property NavCLPSalientePaso As Decimal
        Get
            Return Math.Round(NavCLPSaliente, 4)

        End Get
    End Property

    Public ReadOnly Property MontoSalientePaso As Decimal
        Get
            If MonedaSaliente <> "CLP" Then
                Return String.Format("{0:N2}", MontoSaliente)
            Else
                Return String.Format("{0:N0}", MontoSaliente)
            End If
        End Get
    End Property

    Public ReadOnly Property MontoSalienteCLPPaso As Decimal
        Get
            'If MonedaSaliente <> "CLP" Then
            'Return String.Format("{0:N2}", MontoCLPSaliente)
            'Else
            Return String.Format("{0:N0}", MontoCLPSaliente)
            'End If
        End Get
    End Property

    Public ReadOnly Property NavEntrantePaso As Decimal
        Get
            Return Math.Round(NavEntrante, 4)
        End Get
    End Property

    Public ReadOnly Property NavCLPEntrantePaso As Decimal
        Get
            Return Math.Truncate(NavCLPEntrante)
        End Get
    End Property

    Public ReadOnly Property MontoEntrantePaso As Decimal
        Get
            If MonedaEntrante <> "CLP" Then
                Return String.Format("{0:N2}", MontoEntrante)
            Else
                Return String.Format("{0:N0}", MontoEntrante)
            End If
        End Get
    End Property

    Public ReadOnly Property MontoCLPEntrantePaso As Decimal
        Get
            'If MonedaEntrante <> "CLP" Then
            ' Return String.Format("{0:N2}", MontoCLPEntrante)
            'Else
            Return String.Format("{0:N0}", MontoCLPEntrante)
            ' End If

        End Get
    End Property


    Public ReadOnly Property NombreAportanteBusqueda As String
        Get
            If NombreAportante = "" And RutAportante = "" Then
                Return StrDup(50, " ")
            Else
                Return Me.RutAportante + "/" + Me.NombreAportante
            End If

        End Get
    End Property

    Public ReadOnly Property FondoBusqueda As String
        Get
            If NombreFondo = "" And RutFondo = "" Then
                Return StrDup(50, " ")
            Else
                Return Me.RutFondo + "/" + Me.NombreFondo
            End If

        End Get
    End Property

    Public ReadOnly Property NemotecnicoBusqueda As String
        Get
            If NemotecnicoSaliente = "" Then
                Return StrDup(50, " ")
            Else
                Return Me.NemotecnicoSaliente
            End If

        End Get
    End Property

    Public Sub New()
    End Sub

    Public ReadOnly Property NavEntranteFormat As String
        Get
            If MonedaEntrante <> "USD" Then
                Return String.Format("{0:N6}", NavEntrante)
            Else
                Return String.Format("{0:N6}", NavEntrante)
            End If
        End Get
    End Property

    Public ReadOnly Property NavCLPEntranteFormat As String
        Get
            Return String.Format("{0:N4}", NavCLPEntrante)
        End Get
    End Property

    Public ReadOnly Property NavSalienteFormat As String
        Get
            If MonedaSaliente = "USD" Then
                Return String.Format("{0:N6}", NavSaliente)
            Else
                Return String.Format("{0:N6}", NavSaliente)
            End If
        End Get
    End Property

    Public ReadOnly Property NavCLPSalienteFormat As String
        Get
            Return String.Format("{0:N4}", NavCLPSaliente)
        End Get
    End Property

    Public ReadOnly Property FechaCanje As String
        Get
            If FechaCanjeDate = Nothing Then
                Return ""
            Else
                Return FechaCanjeDate
            End If
        End Get
    End Property

End Class
