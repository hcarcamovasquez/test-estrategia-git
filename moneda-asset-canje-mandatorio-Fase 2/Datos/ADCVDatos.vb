
Imports DTO
Imports WSCanjeMandatorio.WSADCV
Public Class ADCVDatos
    Public Function GetADCV(fondoSerie As ADCVDTO) As ADCVDTO
        Dim Ws = New WSCanjeMandatorio.WSADCV()
        Return Ws.GetADCV(fondoSerie)
    End Function

    Public Function SelectCuotasDCV(ADCV As DTO.ADCVDTO) As ADCVDTO
        Dim Ws = New WSCanjeMandatorio.WSADCV()
        Return Ws.SelectCuotasDCV(ADCV)
    End Function

    Public Function ConsultaDCV(dcv As ADCVDTO) As List(Of ADCVDTO)
        Dim Ws = New WSCanjeMandatorio.WSADCV()
        Return Ws.CuotaDCV(dcv)
    End Function

    Public Function UltimoDCV(dcv As ADCVDTO) As List(Of ADCVDTO)
        Dim Ws = New WSCanjeMandatorio.WSADCV()
        Return Ws.UltimaCuotaDCV(dcv)
    End Function
End Class

