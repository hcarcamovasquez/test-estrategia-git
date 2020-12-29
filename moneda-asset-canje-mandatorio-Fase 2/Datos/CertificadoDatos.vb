Imports DTO
Imports WSCanjeMandatorio.WSCertificados

Public Class CertificadoDatos
    Public Function InsertarCertificado(Certificado As CertificadoDTO) As Integer
        Dim Ws = New WSCanjeMandatorio.WSCertificados()

        If Ws.APIngresar(Certificado) Then
            Return Constantes.CONST_OPERACION_EXITOSA
        Else
            Return Constantes.CONST_ERROR_BBDD
        End If
    End Function

    Public Function ExisteCertificado(Certificado As CertificadoDTO) As Boolean
        Dim Ws = New WSCanjeMandatorio.WSCertificados()
        Return (Ws.ExisteCertificado(Certificado) = 1)

    End Function

    Public Function ConsultarUltimoCorrelativo(certificado) As Integer
        Dim Ws = New WSCanjeMandatorio.WSCertificados()

        Return (Ws.ConsultarUltimoCorrelativo(certificado))
    End Function

    Public Function CertificadosIngresar(certificado As CertificadoDTO) As Integer
        Dim Ws = New WSCanjeMandatorio.WSCertificados()
        Try
            If Ws.CertificadosIngresar(certificado) Then
                Return Constantes.CONST_OPERACION_EXITOSA
            Else
                Return Constantes.CONST_ERROR_BBDD
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function IngresarTemporalModificados(certificado As CertificadoDTO) As Integer
        Dim Ws = New WSCanjeMandatorio.WSCertificados()
        Try
            If Ws.IngresarTemporalModificados(certificado) Then
                Return Constantes.CONST_OPERACION_EXITOSA
            Else
                Return Constantes.CONST_ERROR_BBDD
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function ModificarEliminarCertificados(certificado As CertificadoDTO) As Integer
        Dim Ws = New WSCanjeMandatorio.WSCertificados

        If Ws.ModificarEliminarCertificados(certificado) Then
            Return Constantes.CONST_OPERACION_EXITOSA
        Else
            Return Constantes.CONST_ERROR_BBDD
        End If
    End Function

    Public Function SoloRutAportante(Aportante As DTO.AportanteDTO) As List(Of DTO.AportanteDTO)
        Dim Ws = New WSCanjeMandatorio.WSCertificados()
        Return Ws.SoloRutAportante(Aportante)
    End Function

    Public Function ConsultarTodos(certificado As DTO.CertificadoDTO) As List(Of DTO.CertificadoDTO)
        Dim Ws = New WSCanjeMandatorio.WSCertificados()

        Return Ws.FNConsultar(certificado)

    End Function

    Public Function GetListaCertificadosConFiltro(certificado As DTO.CertificadoDTO, fechaDesde As Nullable(Of Date), fechaHasta As Nullable(Of Date)) As List(Of DTO.CertificadoDTO)
        Dim Ws = New WSCanjeMandatorio.WSCertificados()
        Return Ws.CertificadosBuscarFiltro(certificado, fechaDesde, fechaHasta)
    End Function

    Public Function GetDocumento(certificado As DTO.CertificadoDTO) As CertificadoDTO
        Dim Ws = New WSCanjeMandatorio.WSCertificados()
        Return Ws.GetDocumento(certificado)
    End Function

    Public Function ConsultarNombreAportante(certificado As CertificadoDTO) As List(Of CertificadoDTO)
        Dim listaAportantes As New List(Of CertificadoDTO)
        Dim Ws = New WSCanjeMandatorio.WSCertificados()

        listaAportantes = Ws.ConsultarNombreAportante(certificado)

        Return listaAportantes
    End Function

    Public Function ConsultarNombreFondo(certificado As CertificadoDTO) As List(Of CertificadoDTO)
        Dim listaFondos As New List(Of CertificadoDTO)
        Dim Ws = New WSCanjeMandatorio.WSCertificados()

        listaFondos = Ws.ConsultarNombreFondo(certificado)

        Return listaFondos
    End Function

    Public Function ConsultarPorDocumento(certificado As CertificadoDTO) As List(Of CertificadoDTO)
        Dim listaFondos As New List(Of CertificadoDTO)
        Dim Ws = New WSCanjeMandatorio.WSCertificados()

        listaFondos = Ws.ConsultarPorDocumento(certificado)

        Return listaFondos
    End Function



    Public Function UpdateCertificado(certificado As CertificadoDTO) As Integer
        Dim Ws = New WSCanjeMandatorio.WSCertificados()

        If Ws.FNModificar(certificado) Then
            Return Constantes.CONST_OPERACION_EXITOSA
        Else
            Return Constantes.CONST_ERROR_BBDD
        End If
    End Function

    Public Function BuscarRelaciones(Certificado As CertificadoDTO) As Integer
        Dim Ws = New WSCanjeMandatorio.WSCertificados()

        Return Ws.BuscarRelaciones(Certificado)
    End Function

    Public Function DeleteCertificado(Certificado As CertificadoDTO) As Integer
        Dim Ws = New WSCanjeMandatorio.WSCertificados()

        If Ws.FNAEliminar(Certificado) Then
            Return Constantes.CONST_OPERACION_EXITOSA
        Else
            Return Constantes.CONST_ERROR_BBDD
        End If

    End Function

    Public Function DeleteCertificados(Certificado As CertificadoDTO) As Integer
        Dim Ws = New WSCanjeMandatorio.WSCertificados()
        Try
            Ws.DeleteCertificado(Certificado)
            Return 0
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function DeleteCertificadoAll(Certificado As CertificadoDTO) As Integer
        Dim Ws = New WSCanjeMandatorio.WSCertificados()
        Dim filasAfectas As Integer = Ws.DeleteCertificadoAll(Certificado)
        If filasAfectas > 0 Then
            Return Constantes.CONST_OPERACION_EXITOSA
        Else
            Return Constantes.CONST_ERROR_BBDD
        End If

    End Function
End Class
