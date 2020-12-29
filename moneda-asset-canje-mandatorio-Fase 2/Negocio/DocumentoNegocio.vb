Imports DTO
Imports Datos

Public Class DocumentoNegocio
    Public Function GetDatosDocumento(Documento As DocumentoDTO) As DocumentoDTO
        Dim DocumentosRetorno As DocumentoDTO
        Dim DocumentoDatos As New Datos.DocumentoDatos

        DocumentosRetorno = DocumentoDatos.GetDatosDocumento(Documento)
        Return DocumentosRetorno
    End Function

    Public Function UpdateDocumento(Documento As DocumentoDTO) As Integer
        Dim DocumentoDatos As New Datos.DocumentoDatos

        Return DocumentoDatos.UpdateDocumento(Documento)
    End Function

    Public Function UpdateDocumentoNuevo(Documento As DocumentoDTO) As Integer
        Dim DocumentoDatos As New Datos.DocumentoDatos

        Return DocumentoDatos.UpdateDocumentoNuevo(Documento)
    End Function

    Public Function ExisteDocumento(Documento As DocumentoDTO) As Boolean
        Dim datosDocumento = New Datos.DocumentoDatos()
        Return datosDocumento.ExisteDocumento(Documento)
    End Function
End Class
