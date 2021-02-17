Imports DTO
Imports Datos.SuscripcionDatos

Public Class SuscripcionNegocio
    Private Excel As ExcelWriter = New ExcelWriter
    Public Property rutaArchivosExcel As String

    Private Const CONST_TIPO_CAMBIO_YA_EXISTE As Integer = 1
    Public Const CONST_MENSAJE_EXCEL_GUARDADO As String = "Excel Guardado de forma Exitosa"
    Public Const CONST_MENSAJE_EXCEL_ERROR As String = "Error al Guardar Excel"
    Public Function GetNemotecnicoPorRut(Serie As DTO.SuscripcionDTO) As List(Of DTO.SuscripcionDTO)
        Dim SuscripcionDatos As New Datos.SuscripcionDatos
        Return SuscripcionDatos.GetNemotecnicoPorRut(Serie)
    End Function

    Public Function ExportarAExcel(Suscripcion As SuscripcionDTO, FechaIntencionHasta As Nullable(Of Date), FechaNAVHasta As Nullable(Of Date),
    FechaSuscripcionHasta As Nullable(Of Date)) As String
        Dim SuscripcionDatos As New Datos.SuscripcionDatos
        Dim ListaSuscripcion As List(Of SuscripcionDTO) = SuscripcionDatos.GetListaSuscripcionConFiltro(Suscripcion, FechaIntencionHasta, FechaNAVHasta, FechaSuscripcionHasta)

        If CrearExcel(ListaSuscripcion) Then

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

    Public Function CrearExcel(ListaSuscripcion As List(Of SuscripcionDTO)) As Boolean
        If Excel.CrearExcelSuscripcion(ListaSuscripcion) Then
            Return True
        End If
        Return False
    End Function
    Public Function SelectRescatesTransito(Rescate As RescatesDTO) As RescatesDTO
        Dim RescateRetorno As RescatesDTO
        Dim SuscripcionDatos As New Datos.SuscripcionDatos

        RescateRetorno = SuscripcionDatos.SelectRescatesTransito(Rescate)
        Return RescateRetorno
    End Function
    Public Function ConsultarTransito(sus As DTO.SuscripcionDTO) As List(Of DTO.SuscripcionDTO)
        Dim SuscripcionDatos As New Datos.SuscripcionDatos
        Return SuscripcionDatos.ConsultarTransito(sus)
    End Function
    'Public Function ConsultarActuales(sus As DTO.SuscripcionDTO) As SuscripcionDTO
    '    Dim SuscripcionDatos As New Datos.SuscripcionDatos
    '    Return SuscripcionDatos.ConsultarActuales(sus)
    'End Function
    Public Function ConsultarPorCodigo(Suscripcion As DTO.SuscripcionDTO) As List(Of DTO.SuscripcionDTO)
        Dim SuscripcionDatos As New Datos.SuscripcionDatos
        Return SuscripcionDatos.ConsultarPorCodigo(Suscripcion)
    End Function
    Public Function ConsultarPorRazonSocial(Suscripcion As DTO.SuscripcionDTO) As List(Of DTO.SuscripcionDTO)
        Dim SuscripcionDatos As New Datos.SuscripcionDatos
        Return SuscripcionDatos.ConsultarPorRazonSocial(Suscripcion)
    End Function
    Public Function ConsultarPorMultifondo(Suscripcion As DTO.SuscripcionDTO) As List(Of DTO.SuscripcionDTO)
        Dim SuscripcionDatos As New Datos.SuscripcionDatos
        Return SuscripcionDatos.ConsultarPorMultifondo(Suscripcion)
    End Function
    Public Function GetSuscripcionTransito(Suscripcion As DTO.SuscripcionDTO)
        Dim SuscripcionDatos As New Datos.SuscripcionDatos
        Return SuscripcionDatos.GetSuscripcionTransito(Suscripcion)
    End Function
    Public Function GetSuscripcionTransito2(Suscripcion As DTO.SuscripcionDTO)
        Dim SuscripcionDatos As New Datos.SuscripcionDatos
        Return SuscripcionDatos.GetSuscripcionTransito2(Suscripcion)
    End Function
    Public Function GetRelaciones(Suscripcion As DTO.SuscripcionDTO)
        Dim SuscripcionDatos As New Datos.SuscripcionDatos
        Return SuscripcionDatos.GetRelaciones(Suscripcion)
    End Function
    Public Function GetUltimaSuscripcion(Suscripcion As DTO.SuscripcionDTO)
        Dim SuscripcionDatos As New Datos.SuscripcionDatos
        Return SuscripcionDatos.GetUltimaSuscripcion(Suscripcion)
    End Function
    Public Function GetListaTCConFiltro(Suscripcion As SuscripcionDTO, FechaIntencionHasta As Nullable(Of Date), FechaNAVHasta As Nullable(Of Date),
    FechaSuscripcionHasta As Nullable(Of Date)) As List(Of SuscripcionDTO)
        Dim SuscripcionDatos As New Datos.SuscripcionDatos
        Return SuscripcionDatos.GetListaSuscripcionConFiltro(Suscripcion, FechaIntencionHasta, FechaNAVHasta, FechaSuscripcionHasta)
    End Function

    Public Function GetListaSuscripcion(Suscripcion As DTO.SuscripcionDTO) As List(Of DTO.SuscripcionDTO)
        Dim SuscripcionDatos As New Datos.SuscripcionDatos
        Return SuscripcionDatos.GetListaSuscripcion(Suscripcion)
    End Function

    Public Function GetSuscripcion(Suscripcion As SuscripcionDTO) As SuscripcionDTO
        Dim SuscripcionRetorno As SuscripcionDTO
        Dim SuscripcionDatos As New Datos.SuscripcionDatos
        SuscripcionRetorno = SuscripcionDatos.GetSuscripcion(Suscripcion)
        Return SuscripcionRetorno
    End Function

    Public Function GetFijacionId(Id As Int32) As SuscripcionDTO
        Dim SuscripcionRetorno As SuscripcionDTO
        Dim datosSuscripcion As New Datos.SuscripcionDatos

        SuscripcionRetorno = datosSuscripcion.GetFijacionId(Id)
        Return SuscripcionRetorno
    End Function


    Public Function DeleteSuscripcion(Suscripcion As SuscripcionDTO) As Integer
        Dim SuscripcionoDatos As New Datos.SuscripcionDatos
        Return (SuscripcionoDatos.DeleteSuscripcion(Suscripcion))
    End Function

    Public Function InsertSuscripcion(Suscripcion As SuscripcionDTO) As SuscripcionDTO
        Dim SuscripcionDatos As New Datos.SuscripcionDatos
        Dim SuscripcionConsultado As SuscripcionDTO = SuscripcionDatos.GetSuscripcion(Suscripcion)

        SuscripcionConsultado = SuscripcionDatos.InsertSuscripcion(Suscripcion)

        Return SuscripcionConsultado
    End Function
    Public Function updatesuscripcion(suscripcion As SuscripcionDTO) As Integer
        Dim suscripciondatos As New Datos.SuscripcionDatos
        Return suscripciondatos.UpdateSuscripcion(suscripcion)
    End Function

    Public Function ObtenerActual(suscripcion As SuscripcionDTO) As SuscripcionDTO
        Dim suscripciondatos As New Datos.SuscripcionDatos
        Return suscripciondatos.ObtenerActual(suscripcion)
    End Function

    Public Function ObtenerCuotasFondo(suscripcion As SuscripcionDTO) As SuscripcionDTO
        Dim suscripciondatos As New Datos.SuscripcionDatos
        Return suscripciondatos.ObtenerCuotasFondo(suscripcion)
    End Function
    Public Function GetAportanteSuscripcion(Suscripcion As DTO.SuscripcionDTO) As List(Of DTO.SuscripcionDTO)
        Dim SuscripcionDatos As New Datos.SuscripcionDatos
        Return SuscripcionDatos.GetAportanteSuscripcion(Suscripcion)
    End Function
    Public Function GetFondoSuscripcion(Suscripcion As DTO.SuscripcionDTO) As List(Of DTO.SuscripcionDTO)
        Dim SuscripcionDatos As New Datos.SuscripcionDatos
        Return SuscripcionDatos.GetFondoSuscripcion(Suscripcion)
    End Function
    Public Function GetSerieSuscripcion(Suscripcion As DTO.SuscripcionDTO) As List(Of DTO.SuscripcionDTO)
        Dim SuscripcionDatos As New Datos.SuscripcionDatos
        Return SuscripcionDatos.GetSerieSuscripcion(Suscripcion)
    End Function
    Public Function GetAportanteDistinct(Suscripcion As DTO.SuscripcionDTO) As List(Of DTO.SuscripcionDTO)
        Dim SuscripcionDatos As New Datos.SuscripcionDatos
        Return SuscripcionDatos.GetAportanteDistinct(Suscripcion)
    End Function
    Public Function RecalculoFijacionNAV(Suscripcion As SuscripcionDTO) As Integer
        Dim SuscricpionDatos As New Datos.SuscripcionDatos

        Return SuscricpionDatos.RecalculoFijacionNAV(Suscripcion)
    End Function
    Public Function RecalculoFijacionTC(Suscripcion As SuscripcionDTO) As Integer
        Dim SuscricpionDatos As New Datos.SuscripcionDatos

        Return SuscricpionDatos.RecalculoFijacionTC(Suscripcion)
    End Function

End Class
