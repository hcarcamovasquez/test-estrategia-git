Imports DTO
Imports WSCanjeMandatorio.WSDocumento

Public Class DocumentoDatos
    Public Function GetDatosDocumento(Documento As DocumentoDTO) As DocumentoDTO
        Dim Ws = New WSCanjeMandatorio.WSDocumento()
        Return Ws.ConsultarDatosDocumento(Documento)
    End Function

    Public Function UpdateDocumento(Documento As DocumentoDTO) As Integer
        Dim Ws = New WSCanjeMandatorio.WSDocumento()

        If Ws.FNModificar(Documento) Then
            Return Constantes.CONST_OPERACION_EXITOSA
        Else
            Return Constantes.CONST_ERROR_BBDD
        End If
    End Function

    Public Function UpdateDocumentoNuevo(Documento As DocumentoDTO) As Integer
        Dim Ws = New WSCanjeMandatorio.WSDocumento()

        If Ws.FNModificarNumeroNuevo(Documento) Then
            Return Constantes.CONST_OPERACION_EXITOSA
        Else
            Return Constantes.CONST_ERROR_BBDD
        End If
    End Function

    Public Function ExisteDocumento(Documento As DocumentoDTO) As Boolean
        Dim Ws = New WSCanjeMandatorio.WSDocumento()
        Return (Ws.ValidaNumeroDocumento(Documento) = 1)

    End Function
End Class
