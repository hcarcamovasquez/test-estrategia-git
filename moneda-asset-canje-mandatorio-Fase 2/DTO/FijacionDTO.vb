Public Class FijacionDTO
    Public Property ID As Int32
    Public Property FechaNav As Date
    Public Property FechaTCObs As Date
    Public Property ApRut As String
    Public Property ApMultifondo As String
    Public Property Nemotecnico As String
    Public Property TipoTransaccion As String
    Public Property RazonSocial As String
    Public Property Cuotas As Int32
    Public Property Rut As String
    Public Property FnNombreCorto As String
    Public Property FsNombreCorto As String
    Public Property Contrato As String
    Public Property Poderes As String
    Public Property Transito As Decimal
    Public Property Monto As Decimal
    Public Property MontoCLP As Decimal
    Public Property Estado As String
    Public Property FsMoneda As String
    Public Property NavCLP As Decimal
    Public Property CnCuotasDisponibles As Decimal
    Public Property FijacionNAV As String
    Public Property FijacionTCObservado As String
    Public Property Observaciones As String
    Public Property Estados As String
    Public Property fechaPago As Date

    Public Property TC_OBSERVADO As Decimal
    Public Property NAV_FIJADO As Decimal

    Public Property ObjCanje As CanjeDTO
    Public Property ObjSuscripcion As SuscripcionDTO
    Public Property ObjRescate As RescatesDTO

    'Jovb R3 
    Public Property EstadoIntencion As String


    Public Sub New(id As Int32, fechaNav As Date, fechaTCObs As Date, apRut As String, apMultifondo As String, nemotecnico As String,
                   tipoTransaccion As String, razonSocial As String, cuotas As Int32, rut As String, fnNombreCorto As String, fsNombreCorto As String, contrato As String, poderes As String,
                   transito As Decimal, monto As Decimal, montoCLP As Decimal, estado As String, fsMoneda As String, navCLP As Decimal, cnCuotasDisponibles As Decimal, fijacionNAV As String,
                   fijacionTCObservado As String, observaciones As String, estados As String
        )

        Me.ID = id
        Me.FechaNav = fechaNav
        Me.FechaTCObs = fechaTCObs
        Me.ApRut = apRut
        Me.ApMultifondo = apMultifondo
        Me.Nemotecnico = nemotecnico
        Me.TipoTransaccion = tipoTransaccion
        Me.RazonSocial = razonSocial
        Me.Cuotas = cuotas
        Me.Rut = rut
        Me.FnNombreCorto = fnNombreCorto
        Me.FsNombreCorto = fsNombreCorto
        Me.Contrato = contrato
        Me.Poderes = poderes
        Me.Transito = transito
        Me.Monto = monto
        Me.MontoCLP = montoCLP
        Me.Estado = estado
        Me.FsMoneda = fsMoneda
        Me.NavCLP = navCLP
        Me.CnCuotasDisponibles = cnCuotasDisponibles
        Me.FijacionNAV = fijacionNAV
        Me.FijacionTCObservado = fijacionTCObservado
        Me.Observaciones = observaciones
        Me.Estados = estados

        ObjCanje = New CanjeDTO()
        ObjSuscripcion = New SuscripcionDTO
        ObjRescate = New RescatesDTO()
    End Sub

    Public ReadOnly Property TipoTransaccionBusqueda As String
        Get
            If TipoTransaccion = "" Then
                Return StrDup(50, " ")
            Else
                Return Me.TipoTransaccion
            End If

        End Get
    End Property

    Public ReadOnly Property FijacionNAVBusqueda As String
        Get
            If FijacionNAV = "" Then
                Return StrDup(50, " ")
            Else
                Return Me.FijacionNAV
            End If

        End Get
    End Property

    Public ReadOnly Property FijacionTCObservadoBusqueda As String
        Get
            If FijacionTCObservado = "" Then
                Return StrDup(50, " ")
            Else
                Return Me.FijacionTCObservado
            End If

        End Get
    End Property

    Public ReadOnly Property RutBusqueda As String
        Get
            If Rut = "" And FnNombreCorto = "" Then
                Return StrDup(50, " ")
            Else
                Return Me.Rut + "/" + Me.FnNombreCorto
            End If

        End Get
    End Property

    Public Property MonedaPago As String

    Public Sub New()
        objCanje = New CanjeDTO()
        ObjSuscripcion = New SuscripcionDTO
        ObjRescate = New RescatesDTO()
    End Sub
End Class
