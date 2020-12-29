Imports DTO
Imports Datos
Public Class CertificadoNegocio
    Private Excel As ExcelWriter = New ExcelWriter
    Public Property rutaArchivosExcel As String

    Public Const CONST_INSERT_EXITO As Integer = 0
    Public Const CONST_ERROR_EXISTE_RUTYMULTIFONDO As Integer = 1
    Public Const CONST_ERROR_RUT_SIN_MULTIFONDO As Integer = 2
    Public Const CONST_ERROR_NO_INGRESO_BBDD As Integer = -99
    Public Const CONST_ACCION_ALL As String = "SELECT_ALL"
    Public Const CONST_MENSAJE_EXCEL_GUARDADO As String = "Excel Guardado de forma Exitosa"
    Public Const CONST_MENSAJE_EXCEL_ERROR As String = "Error al Guardar Excel"

    Dim Datos As CertificadoDatos = New CertificadoDatos
    Dim DatosAportante As AportanteDatos = New AportanteDatos


    Public Function InsertarCertificado(Certificado As CertificadoDTO, Aportante As AportanteDTO) As Integer
        Dim existe As Integer = BuscarTodoAportante(Aportante)

        If existe = Constantes.CONST_OPERACION_EXITOSA Then
            Return Datos.InsertarCertificado(Certificado)
        Else
            Return existe
        End If
    End Function

    Public Function BuscarTodoAportante(aportante As AportanteDTO) As Integer
        Dim listaAportantes As New List(Of AportanteDTO)

        listaAportantes = DatosAportante.BuscarTodoAportante(CONST_ACCION_ALL, aportante)

        If listaAportantes Is Nothing Then
            Return 0 'No se encontro registro en la base de datos 
        ElseIf aportante.Multifondo Is Nothing Then
            Return 1
        Else
            For Each ap As AportanteDTO In listaAportantes
                If ap.Multifondo = aportante.Multifondo Then
                    Return 2
                End If
            Next
        End If
        Return 0
    End Function

    Public Function ExisteCertificado(Certificado As CertificadoDTO) As Boolean
        Dim datosCertificado = New Datos.CertificadoDatos()
        Return datosCertificado.ExisteCertificado(Certificado)
    End Function

    Public Function ConsultarUltimoCorrelativo(certificado As CertificadoDTO) As Integer
        Return Datos.ConsultarUltimoCorrelativo(certificado)
    End Function

    Public Function GuardarCertificados(accion As String,
                                             listaCertificados As List(Of CertificadoDTO),
                                             listaCertificadosAgregados As List(Of CertificadoDTO),
                                             listaCertificadosModificados As List(Of CertificadoDTO),
                                             listaCertificadosEliminados As List(Of CertificadoDTO)) As Boolean
        Try


            If accion = "AGREGAR_CERTIFICADO" Then
                InsertaCertificados(listaCertificados)
            ElseIf accion = "MODIFICAR_CERTIFICADOS" Then
                EliminarCertificado(listaCertificadosEliminados)
                InsertaCertificados(listaCertificadosAgregados)
                InsertaCertificados(listaCertificadosModificados)
            ElseIf accion = "AGREGAR_CERTIFICADOS_MODIFICA" Then
                InsertaCertificados(listaCertificadosAgregados)
                'EliminarCertificado(listaCertificadosEliminados)
            ElseIf accion = "ELIMINAR_CERTIFICADOS" Then
                EliminarCertificado(listaCertificadosEliminados)
            End If
            Return True

        Catch ex As Exception
            Return False
        End Try

        Return False
    End Function

    Private Sub InsertaCertificados(listaCertificados As List(Of CertificadoDTO))
        Try
            For Each certificado As CertificadoDTO In listaCertificados
                CertificadosIngresar(certificado)
            Next
        Catch ex As Exception
            Throw
        End Try

    End Sub

    Public Function CertificadosIngresar(certificado As CertificadoDTO) As Integer
        Try
            Dim datosCertificado = New Datos.CertificadoDatos
            Return datosCertificado.CertificadosIngresar(certificado)
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function IngresarTemporalModificados(certificado As CertificadoDTO) As Integer
        Try
            Dim datosCertificado = New Datos.CertificadoDatos
            Return datosCertificado.IngresarTemporalModificados(certificado)
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function ModificarEliminarCertificados(certificado As CertificadoDTO) As Integer
        Dim datosCertificado = New Datos.CertificadoDatos

        Return datosCertificado.ModificarEliminarCertificados(certificado)

    End Function

    Public Function SoloRutAportante(Aportante As DTO.AportanteDTO) As List(Of DTO.AportanteDTO)
        Dim certificadoDatos As New Datos.CertificadoDatos
        Return certificadoDatos.SoloRutAportante(Aportante)
    End Function

    Public Function ConsultarTodos(certificado As DTO.CertificadoDTO) As List(Of DTO.CertificadoDTO)
        Dim certificadoDatos As New Datos.CertificadoDatos
        Return certificadoDatos.ConsultarTodos(certificado)
    End Function

    Public Function GetListaCertificadosConFiltro(certificados As CertificadoDTO, FechaDesde As Nullable(Of Date), FechaHasta As Nullable(Of Date)) As List(Of CertificadoDTO)
        Dim CertificadosDatos As New Datos.CertificadoDatos
        Return CertificadosDatos.GetListaCertificadosConFiltro(certificados, FechaDesde, FechaHasta)
    End Function

    Public Function GetDocumento(certificado As CertificadoDTO) As CertificadoDTO
        Dim CertificadoRetorno As CertificadoDTO
        Dim CertificadoDatos As New Datos.CertificadoDatos

        CertificadoRetorno = CertificadoDatos.GetDocumento(certificado)
        Return CertificadoRetorno
    End Function

    Public Function ConsultarNombreAportante(certificado As CertificadoDTO) As List(Of CertificadoDTO)
        Return Datos.ConsultarNombreAportante(certificado)
    End Function

    Public Function ConsultarNombreFondo(certificado As CertificadoDTO) As List(Of CertificadoDTO)
        Return Datos.ConsultarNombreFondo(certificado)
    End Function

    Public Function ConsultarPorDocumento(certificado As CertificadoDTO) As List(Of CertificadoDTO)
        Return Datos.ConsultarPorDocumento(certificado)
    End Function



    Public Function UpdateCertificado(certificado As CertificadoDTO) As Integer
        Dim certificadoDatos As New Datos.CertificadoDatos

        Return certificadoDatos.UpdateCertificado(certificado)
    End Function

    Public Function ExportarAExcelTodos(Certificado As CertificadoDTO) As String
        Dim certificadoDatos As New Datos.CertificadoDatos
        Dim ListaCertificados As List(Of CertificadoDTO) = certificadoDatos.ConsultarTodos(Certificado)

        If CrearExcel(ListaCertificados) Then

            If Excel.rutaArchivosExcel Is Nothing Then
                Return CONST_MENSAJE_EXCEL_ERROR
            Else
                Me.rutaArchivosExcel = Excel.rutaArchivosExcel
                Return CONST_MENSAJE_EXCEL_GUARDADO
            End If

        Else
            Return CONST_MENSAJE_EXCEL_ERROR
        End If

    End Function

    Public Function ExportarAExcel(Certificado As CertificadoDTO, FechaDesde As Nullable(Of Date), FechaHasta As Nullable(Of Date)) As String
        Dim CertificadoDatos As New Datos.CertificadoDatos
        Dim ListaCertificados As List(Of CertificadoDTO) = CertificadoDatos.GetListaCertificadosConFiltro(Certificado, FechaDesde, FechaHasta)

        If CrearExcel(ListaCertificados) Then

            If Excel.rutaArchivosExcel Is Nothing Then
                Return CONST_MENSAJE_EXCEL_ERROR
            Else
                Me.rutaArchivosExcel = Excel.rutaArchivosExcel
                Return CONST_MENSAJE_EXCEL_GUARDADO
            End If

        Else
            Return CONST_MENSAJE_EXCEL_ERROR
        End If

    End Function

    Public Function CrearExcel(ListaCertificados As List(Of CertificadoDTO)) As Boolean
        If Excel.CrearExcelCertificados(ListaCertificados) Then
            Return True
        End If
        Return False
    End Function

    Public Function DeleteCertificado(Certificado As CertificadoDTO) As Integer
        Dim CertificadoDatos As New Datos.CertificadoDatos
        Dim relaciones As Integer = CertificadoDatos.BuscarRelaciones(Certificado)

        If relaciones = 0 Then
            Return CertificadoDatos.DeleteCertificado(Certificado)
        Else
            Return Constantes.CONST_ERROR_NO_SE_PUEDE_BORRAR
        End If

    End Function

    Private Sub EliminarCertificado(listaCertificadosEliminados As List(Of CertificadoDTO))
        Dim datosCertificado As Datos.CertificadoDatos = New Datos.CertificadoDatos()
        Try
            For Each certificado As CertificadoDTO In listaCertificadosEliminados
                datosCertificado.DeleteCertificados(certificado)
            Next
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Public Function EliminarTodosCertificados(Certificado As CertificadoDTO) As Integer
        Dim datosCertificado As Datos.CertificadoDatos = New Datos.CertificadoDatos()

        If datosCertificado.DeleteCertificadoAll(Certificado) = Constantes.CONST_OPERACION_EXITOSA Then
            Return Constantes.CONST_OPERACION_EXITOSA

        Else
            Return Constantes.CONST_ERROR_BBDD
        End If

    End Function

End Class
