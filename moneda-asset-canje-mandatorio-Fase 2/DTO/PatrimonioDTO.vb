Public Class PatrimonioDTO
    Public Property DFECHA As Date
    Public Property IDFONDO As String
    Public Property IDMONEDA As String
    Public Property NPATRIMONIO As Decimal
    Public Property DTIMESTAMP As Date

    Public Sub New(DFECHA As Date, IDFONDO As String, IDMONEDA As String, NPATRIMONIO As Decimal, DTIMESTAMP As Date)

        Me.DFECHA = DFECHA
        Me.IDFONDO = IDFONDO
        Me.IDMONEDA = IDMONEDA
        Me.NPATRIMONIO = NPATRIMONIO
        Me.DTIMESTAMP = DTIMESTAMP

    End Sub

    Public Sub New()
    End Sub
End Class
