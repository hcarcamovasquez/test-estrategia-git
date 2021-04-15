Public Class RescatesDTO
    Inherits TransaccionBaseDTO

    Public Property RES_ID As Integer
    Public Property RES_Fecha_Solicitud As DateTime
    Public Property RES_Fecha_Pago As DateTime
    Public Property AP_RUT As String
    Public Property AP_Multifondo As String
    Public Property FS_Nemotecnico As String
    Public Property RES_Cuotas As Decimal
    Public Property Res_Fecha_Carga As DateTime
    Public Property FN_RUT As String
    Public Property FN_Nombre_Corto As String
    Public Property RES_Tipo_Transaccion As String
    Public Property AP_Razon_Social As String
    Public Property FS_Nombre_Corto As String
    Public Property RES_Moneda_Pago As String
    Public Property ADCV_Cantidad As Decimal
    Public Property RES_Fecha_Nav As DateTime
    Public Property RES_FechaTCObs As DateTime
    Public Property RES_Nav As Decimal
    Public Property RES_Monto As Decimal
    Public Property RES_Nav_CLP As Decimal
    Public Property RES_Monto_CLP As Decimal
    Public Property TC_Valor As Decimal
    Public Property RES_Contrato As String
    Public Property RES_Poderes As String
    Public Property RES_Estado As String
    Public Property RES_Observaciones As String
    Public Property RES_Patrimonio As Decimal
    Public Property FS_Patrimonio As String
    Public Property RES_Disponible_Patrimonio As Decimal
    Public Property ADCV_Fecha As DateTime
    Public Property SC_Cuotas_a_Suscribir As Decimal
    Public Property CN_Cuotas_Disponibles As Decimal
    Public Property RES_Cuotas_Disponibles As Decimal
    Public Property RES_Transito As Decimal
    Public Property RES_Fijacion_NAV As String
    Public Property RES_Fijacion_TCObservado As String
    Public Property RES_Fecha_Ingreso As DateTime
    Public Property RES_Usuario_Ingreso As String
    Public Property RES_Fecha_Modificacion As DateTime
    Public Property RES_Usuario_Modificacion As String
    Public Property RES_Estado_Rescate As Integer
    Public Property FS_Moneda As String
    Public Property RES_Maximo As Decimal
    Public Property RES_Utilizado As Decimal
    Public Property CountAP As Integer
    Public Property CountFN As Integer
    Public Property CountFS As Integer

    'Variable Utilizada para saber desde donde es la invocacion ( Desde Rescates o Desde Fijacion ) 
    Public Property DesdeProceso As String = "Rescate"


    Public Sub New(RES_ID As Integer, RES_Fecha_Solicitud As DateTime, RES_Fecha_Pago As DateTime, AP_RUT As String, AP_Multifondo As String, FS_Nemotecnico As String, RES_Cuotas As Decimal, Res_Fecha_Carga As DateTime, FN_RUT As String,
                   FN_Nombre_Corto As String, RES_Tipo_Transaccion As String, AP_Razon_Social As String, FS_Nombre_Corto As String, RES_Moneda_Pago As String, ADCV_Cantidad As Decimal, RES_Fecha_Nav As DateTime, RES_FechaTCObs As DateTime,
                   RES_Nav As Decimal, RES_Monto As Decimal, RES_Nav_CLP As Decimal, RES_Monto_CLP As Decimal, TC_Valor As Decimal, RES_Contrato As String, RES_Poderes As String, RES_Estado As String, RES_Observaciones As String,
                   RES_Patrimonio As Decimal, FS_Patrimonio As String, RES_Disponible_Patrimonio As Decimal, ADCV_Fecha As DateTime, SC_Cuotas_a_Suscribir As Decimal, CN_Cuotas_Disponibles As Decimal, RES_Cuotas_Disponibles As Decimal, RES_Transito As Decimal, RES_Fijacion_NAV As String,
                   RES_Fijacion_TCObservado As String, RES_Fecha_Ingreso As DateTime, RES_Usuario_Ingreso As String, RES_Fecha_Modificacion As DateTime, RES_Usuario_Modificacion As String, RES_Estado_Rescate As Integer, FS_Moneda As String, RES_Maximo As Decimal, RES_Utilizado As Decimal,
                   CountAP As Integer, CountFN As Integer, CountFS As Integer)
        Me.RES_ID = RES_ID
        Me.RES_Fecha_Solicitud = RES_Fecha_Solicitud
        Me.RES_Fecha_Pago = RES_Fecha_Pago
        Me.AP_RUT = AP_RUT
        Me.AP_Multifondo = AP_Multifondo
        Me.FS_Nemotecnico = FS_Nemotecnico
        Me.RES_Cuotas = RES_Cuotas
        Me.Res_Fecha_Carga = Res_Fecha_Carga
        Me.FN_RUT = FN_RUT
        Me.FN_Nombre_Corto = FN_Nombre_Corto
        Me.RES_Tipo_Transaccion = RES_Tipo_Transaccion
        Me.AP_Razon_Social = AP_Razon_Social
        Me.FS_Nombre_Corto = FS_Nombre_Corto
        Me.RES_Moneda_Pago = RES_Moneda_Pago
        Me.ADCV_Cantidad = ADCV_Cantidad
        Me.RES_Fecha_Nav = RES_Fecha_Nav
        Me.RES_FechaTCObs = RES_FechaTCObs
        Me.RES_Nav = RES_Nav
        Me.RES_Monto = RES_Monto
        Me.RES_Nav_CLP = RES_Nav_CLP
        Me.RES_Monto_CLP = RES_Monto_CLP
        Me.TC_Valor = TC_Valor
        Me.RES_Contrato = RES_Contrato
        Me.RES_Poderes = RES_Poderes
        Me.RES_Estado = RES_Estado
        Me.RES_Observaciones = RES_Observaciones
        Me.RES_Patrimonio = RES_Patrimonio
        Me.FS_Patrimonio = FS_Patrimonio
        Me.RES_Disponible_Patrimonio = RES_Disponible_Patrimonio
        Me.ADCV_Fecha = ADCV_Fecha
        Me.SC_Cuotas_a_Suscribir = SC_Cuotas_a_Suscribir
        Me.CN_Cuotas_Disponibles = CN_Cuotas_Disponibles
        Me.RES_Cuotas_Disponibles = RES_Cuotas_Disponibles
        Me.RES_Transito = RES_Transito
        Me.RES_Fijacion_NAV = RES_Fijacion_NAV
        Me.RES_Fijacion_TCObservado = RES_Fijacion_TCObservado
        Me.RES_Fecha_Ingreso = RES_Fecha_Ingreso
        Me.RES_Usuario_Ingreso = RES_Usuario_Ingreso
        Me.RES_Fecha_Modificacion = RES_Fecha_Modificacion
        Me.RES_Usuario_Modificacion = RES_Usuario_Modificacion
        Me.RES_Estado_Rescate = RES_Estado_Rescate
        Me.FS_Moneda = FS_Moneda
        Me.RES_Maximo = RES_Maximo
        Me.RES_Utilizado = RES_Utilizado
        Me.CountAP = CountAP
        Me.CountFN = CountFN
        Me.CountFS = CountFS
    End Sub

    Public ReadOnly Property RutRazonSocialAportante As String
        Get
            If AP_RUT = "" And AP_Razon_Social = "" Then
                Return StrDup(50, " ")
            Else
                Return Me.AP_RUT + "/" + Me.AP_Razon_Social
            End If

        End Get
    End Property

    Public ReadOnly Property NombreAportanteBusqueda As String
        Get
            If AP_Razon_Social = "" Then
                Return StrDup(50, " ")
            Else
                Return Me.AP_Razon_Social
            End If

        End Get
    End Property

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

    Public ReadOnly Property RES_NavFormat As String
        Get
            Return String.Format("{0:N6}", RES_Nav)
        End Get
    End Property

    Public ReadOnly Property RES_Nav_CLPFormat As String
        Get
            Return String.Format("{0:N4}", RES_Nav_CLP)
        End Get
    End Property


    Public ReadOnly Property RES_MontoFormat As String
        Get
            If FS_Moneda <> "CLP" Then
                Return String.Format("{0:N2}", RES_Monto)
            Else
                Return String.Format("{0:N0}", RES_Monto)
            End If
        End Get
    End Property

    Public ReadOnly Property RES_Monto_CLPFormat As String
        Get
            If FS_Moneda <> "CLP" Then
                Return String.Format("{0:N0}", RES_Monto_CLP)
            Else
                Return String.Format("{0:N0}", RES_Monto_CLP)
            End If
        End Get
    End Property
    Public Sub New()
    End Sub

End Class
