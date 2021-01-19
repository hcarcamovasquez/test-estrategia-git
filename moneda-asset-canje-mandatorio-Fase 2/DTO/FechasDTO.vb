Public Class FechasDTO
    Public Property Anno As Integer
    Public Property Mes As Integer
    Public Property Dia As Integer
    Public Property DF_PAIS As String

    Public Property DiasASumar As Integer
    Public Property DiasCorridos As Integer


    Public ReadOnly Property FechaInicial As Date
        Get
            Return DateSerial(Anno, Mes, Dia)
        End Get
    End Property

    Public Property FechaHabilSiguiente As Date
    Public Property FechaHabilAnterior As Date
    Public Property FechaEsHabil As Integer

    Public ReadOnly Property EsHabil As Boolean
        Get
            Return (FechaEsHabil = 1)
        End Get
    End Property

End Class
