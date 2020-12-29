Public Class FondoSerieDTO
    Public Property Rut As String
    Public Property Nemotecnico As String
    Public Property Nombrecorto As String
    Public Property Remuneracion As String
    Public Property Nacionalidad As String
    Public Property Calificado As String
    Public Property Moneda As String
    Public Property LimiteMoneda As String
    Public Property LimiteMinimo As Decimal
    Public Property LimiteMaximo As Decimal
    Public Property ExclusivoMAM As Integer
    Public Property Compatible As Integer
    Public Property Canje As Integer
    Public Property Nivel As Double
    Public Property HorarioRecaste As String
    Public Property FondoRescatable As String
    Public Property FechaNav As String
    Public Property FechaRescate As String
    Public Property FechaTCObservado As String
    Public Property Patrimonio As String
    Public Property FijacionNav As String
    Public Property FechaNavSuscripcion As String
    Public Property FechaSuscripcion As String
    Public Property FechaTCSuscripcion As String
    Public Property FijacionSuscripcion As String
    Public Property FechaNavCanje As String
    Public Property FechaTCCanje As String
    Public Property FijacionCanje As String
    Public Property Estado As Integer
    Public Property FechaIngreso As Date
    Public Property UsuarioIngreso As String
    Public Property FechaModificacion As Date
    Public Property UsuarioModificacion As String
    ' Columnas nuevas 
    Public Property DiasHabilesRescate As Integer
    Public Property DiasHabilesSuscripciones As Integer
    Public Property DiasHabilesCanje As Integer
    Public Property FsFechaCanjeCanje As String
    Public Property DiasHabilesFechaCanje As Integer



    Public Sub New(rut As String, nemotecnico As String, nombrecorto As String, remuneracion As String, nacionalidad As String, calificado As String, moneda As String, limiteMoneda As String,
                   limiteMinimo As Double, limiteMaximo As Double, exclusivoMAM As Integer, compatible As Integer, canje As Integer, nivel As Double, horarioRecaste As String,
                   fondoRescatable As String, fechaNav As String, fechaRescate As String, fechaTCObservado As String, patrimonio As String, fijacionNav As String, fechaNavSuscripcion As String,
                   fechaSuscripcion As String, fechaTCSuscripcion As String, fijacionSuscripcion As String, fechaNavCanje As String, fechaTCCanje As String, fijacionCanje As String,
                   estado As Integer, fechaIngreso As Date, usuarioIngreso As String, fechaModificacion As Date, usuarioModificacion As String)

        Me.Rut = rut
        Me.Nemotecnico = nemotecnico
        Me.Nombrecorto = nombrecorto
        Me.Remuneracion = remuneracion
        Me.Nacionalidad = nacionalidad
        Me.Calificado = calificado
        Me.Moneda = moneda
        Me.LimiteMoneda = limiteMoneda
        Me.LimiteMinimo = limiteMinimo
        Me.LimiteMaximo = limiteMaximo
        Me.ExclusivoMAM = exclusivoMAM
        Me.Compatible = compatible
        Me.Canje = canje
        Me.Nivel = nivel
        Me.HorarioRecaste = horarioRecaste
        Me.FondoRescatable = fondoRescatable
        Me.FechaNav = fechaNav
        Me.FechaRescate = fechaRescate
        Me.FechaTCObservado = fechaTCObservado
        Me.Patrimonio = patrimonio
        Me.FijacionNav = fijacionNav
        Me.FechaNavSuscripcion = fechaNavSuscripcion
        Me.FechaSuscripcion = fechaSuscripcion
        Me.FechaTCSuscripcion = fechaTCSuscripcion
        Me.FijacionSuscripcion = fijacionSuscripcion
        Me.FechaNavCanje = fechaNavCanje
        Me.FechaTCCanje = fechaTCCanje
        Me.FijacionCanje = fijacionCanje
        Me.Estado = estado
        Me.FechaIngreso = fechaIngreso
        Me.UsuarioIngreso = usuarioIngreso
        Me.FechaModificacion = fechaModificacion
        Me.UsuarioModificacion = usuarioModificacion
    End Sub

    Public ReadOnly Property exclusivoPaso As String
        Get
            If ExclusivoMAM = -1 Then
                Return "S"
            Else
                Return "N"
            End If

        End Get
    End Property

    Public ReadOnly Property fechaNavPaso As String
        Get
            If FechaNav = "," Then
                Return " "
            Else
                Return FechaNav
            End If
        End Get
    End Property

    Public ReadOnly Property fechaTcPaso As String
        Get
            If FechaTCObservado = "," Then
                Return " "
            Else
                Return FechaTCObservado
            End If
        End Get
    End Property

    Public ReadOnly Property fechaRescatePaso As String
        Get
            If FechaRescate = "," Then
                Return " "
            Else
                Return FechaRescate
            End If
        End Get
    End Property

    Public ReadOnly Property fechaNavSusPaso As String
        Get
            If FechaNavSuscripcion = "," Then
                Return " "
            Else
                Return FechaNavSuscripcion
            End If
        End Get
    End Property

    Public ReadOnly Property fechaSusPaso As String
        Get
            If FechaSuscripcion = "," Then
                Return " "
            Else
                Return FechaSuscripcion
            End If
        End Get
    End Property

    Public ReadOnly Property fechaTcSusPaso As String
        Get
            If FechaTCSuscripcion = "," Then
                Return " "
            Else
                Return FechaTCSuscripcion
            End If
        End Get
    End Property

    Public ReadOnly Property fechaNavcPaso As String
        Get
            If FechaNavCanje = "," Then
                Return " "
            Else
                Return FechaNavCanje
            End If
        End Get
    End Property

    Public ReadOnly Property fechaTcCanjePaso As String
        Get
            If FechaTCCanje = "," Then
                Return " "
            Else
                Return FechaTCCanje
            End If
        End Get
    End Property

    Public ReadOnly Property compatiblePaso As String
        Get
            If Compatible Then
                Return "S"
            Else
                Return "N"
            End If
        End Get
    End Property

    Public ReadOnly Property canjePaso As String
        Get
            If Canje Then
                Return "S"
            Else
                Return "N"
            End If

        End Get
    End Property

    Public ReadOnly Property RutNombreFondo As String
        Get
            If Rut.Trim() = "" And Nombrecorto.Trim() = "" Then
                Return StrDup(50, " ")
            Else
                Return Me.Rut + "/" + Me.Nombrecorto
            End If

        End Get
    End Property

    Public ReadOnly Property RutNemotecnico As String
        Get
            If Rut = "" And Nemotecnico = "" Then
                Return StrDup(50, " ")
            Else
                Return Me.Rut + "/" + Me.Nemotecnico
            End If

        End Get
    End Property

    Public ReadOnly Property NemotecnicoBusqueda As String
        Get
            If Nemotecnico = "" Then
                Return StrDup(50, " ")
            Else
                Return Me.Nemotecnico
            End If

        End Get
    End Property

    Public ReadOnly Property RutBusqueda As String
        Get
            If Rut = "" Then
                Return StrDup(50, " ")
            Else
                Return Me.Rut
            End If

        End Get
    End Property

    Public ReadOnly Property AgrupacionBusqueda As String
        Get
            If Nivel = -99 Then
                Return StrDup(50, " ")
            Else
                Return Me.Nivel
            End If

        End Get
    End Property


    Public ReadOnly Property esFechaNavRescateDiasHabiles As Boolean
        Get
            Return (DiasHabilesRescate = 1)
        End Get
    End Property

    Public ReadOnly Property esFechaNavSuscripcionesDiasHabiles As Boolean
        Get
            Return (DiasHabilesSuscripciones = 1)
        End Get
    End Property

    Public ReadOnly Property esFechaNavCanjeDiasHabiles As Boolean
        Get
            Return (DiasHabilesCanje = 1)
        End Get
    End Property

    Public ReadOnly Property esFechaCanjeDiasHabiles As Boolean
        Get
            Return (DiasHabilesFechaCanje = 1)
        End Get
    End Property

    Public Sub New()
    End Sub
End Class
