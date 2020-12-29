Imports DTO
Imports Datos

Public Class ADCVNegocio
    Public Function GetADCV(ADCV As ADCVDTO) As ADCVDTO
        Dim ADCVRetorno As ADCVDTO
        Dim ADCVDatos As New Datos.ADCVDatos

        ADCVRetorno = ADCVDatos.GetADCV(ADCV)
        Return ADCVRetorno
    End Function

    Public Function SelectCuotasDCV(ADCV As ADCVDTO) As ADCVDTO
        Dim ADCVRetorno As ADCVDTO
        Dim ADCVDatos As New Datos.ADCVDatos



        ADCVRetorno = ADCVDatos.SelectCuotasDCV(ADCV)
        Return ADCVRetorno
    End Function

    Public Function ConsultaDCV(dcv As ADCVDTO) As List(Of ADCVDTO)
        Dim datos = New Datos.ADCVDatos
        Return datos.ConsultaDCV(dcv)
    End Function

    Public Function UltimoDCV(dcv As ADCVDTO) As List(Of ADCVDTO)
        Dim datos = New Datos.ADCVDatos
        Return datos.UltimoDCV(dcv)
    End Function
End Class
