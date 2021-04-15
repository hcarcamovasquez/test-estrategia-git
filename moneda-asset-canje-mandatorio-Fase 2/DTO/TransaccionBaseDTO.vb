Public Class TransaccionBaseDTO
    Private _campoHoraTransaccion As String

    Public Property HoraTransaccion As String
        Get
            Return _campoHoraTransaccion
        End Get
        Set(value As String)
            If (String.IsNullOrEmpty(value) Or value = Nothing) Then
                _campoHoraTransaccion = ""
            Else
                Dim largo As Integer = value.Length

                largo = IIf(largo > 5, 5, largo)

                _campoHoraTransaccion = value.Substring(0, largo)
            End If
        End Set
    End Property
End Class
