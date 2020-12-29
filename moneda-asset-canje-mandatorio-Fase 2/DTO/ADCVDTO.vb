Imports DTO
Public Class ADCVDTO
    Public Property ADCV_ID As Decimal
    Public Property ADCV_Fecha As DateTime
    Public Property AP_RUT As String
    Public Property ADCV_Numero_Registro As Decimal
    Public Property ADCV_Razon_Social As String
    Public Property FS_Nemotecnico As String
    Public Property ADCV_Cantidad As Decimal


    Public Sub New(ADCV_ID As Decimal, ADCV_Fecha As DateTime, AP_RUT As String, ADCV_Numero_Registro As Decimal, ADCV_Razon_Social As String, FS_Nemotecnico As String, ADCV_Cantidad As Decimal)
        Me.ADCV_ID = ADCV_ID
        Me.ADCV_Fecha = ADCV_Fecha
        Me.AP_RUT = AP_RUT
        Me.ADCV_Numero_Registro = ADCV_Numero_Registro
        Me.ADCV_Razon_Social = ADCV_Razon_Social
        Me.FS_Nemotecnico = FS_Nemotecnico
        Me.ADCV_Cantidad = ADCV_Cantidad
    End Sub

    Public Sub New()
    End Sub
End Class
