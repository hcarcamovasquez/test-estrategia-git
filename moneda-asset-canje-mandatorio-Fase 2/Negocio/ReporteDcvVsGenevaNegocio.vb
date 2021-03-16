Imports DTO
Imports Datos

Public Class ReporteDcvVsGenevaNegocio
    Public Property rutaArchivosExcel As String

    Public Function GetInformeGeneva(informeGeneva As ReporteDcvVsGenevaDTO) As List(Of ReporteDcvVsGenevaDTO)
        Dim lista As List(Of ReporteDcvVsGenevaDTO) = New List(Of ReporteDcvVsGenevaDTO)
        Dim datos As ReporteDcvVsGenevaDatos = New ReporteDcvVsGenevaDatos()

        lista = datos.GetInformeGeneva(informeGeneva)

        Return lista
    End Function



    Public Function ExportarAExcel(reporteDCV As ReporteDcvVsGenevaDTO) As String
        Dim ReporteDatos As New Datos.ReporteDcvVsGenevaDatos
        Dim lista As List(Of ReporteDcvVsGenevaDTO)
        Dim Excel As ExcelWriter = New ExcelWriter

        lista = GetInformeGeneva(reporteDCV)


        If Excel.CrearExcelReporteGeneva(lista) Then

            If Excel.rutaArchivosExcel Is Nothing Then
                Return Constantes.CONST_EXCEL_MENSAJE_ERROR
            Else
                Me.rutaArchivosExcel = Excel.rutaArchivosExcel
                Return Constantes.CONST_EXCEL_GUARDADO_EXITO
            End If

        Else
            Return Constantes.CONST_EXCEL_MENSAJE_ERROR
        End If
    End Function
End Class
