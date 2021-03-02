Imports Datos
Imports DTO

Public Class ReporteControlCuotasEmitidasNegocio
    Public Property rutaArchivosExcel As String

    Public Function [Select](ejecucionDto As ReporteControlCuotasEmitidasDTO) As List(Of ReporteControlCuotasEmitidasDTO)
        Dim lista As List(Of ReporteControlCuotasEmitidasDTO) = New List(Of ReporteControlCuotasEmitidasDTO)
        Dim datos As ReporteControlCuotasEmitidasDatos = New ReporteControlCuotasEmitidasDatos()

        lista = datos.SelectFiltro(ejecucionDto)

        Return lista
    End Function

    Public Function ExportarAExcel(controlCuota As ReporteControlCuotasEmitidasDTO) As String
        Dim FijacionDatos As New Datos.ReporteControlCuotasEmitidasDatos
        Dim ListaFijacion As List(Of ReporteControlCuotasEmitidasDTO) = FijacionDatos.SelectFiltro(controlCuota)
        Dim Excel As ExcelWriter = New ExcelWriter

        Dim resultadoExcel As Boolean = Excel.CrearExcelReporteCuotasEmitidas(ListaFijacion)

        If resultadoExcel Then

            If Excel.rutaArchivosExcel Is Nothing Then
                Return Constantes.CONST_MENSAJE_EXCEL_ERROR
            Else
                Me.rutaArchivosExcel = Excel.rutaArchivosExcel
                Return Constantes.CONST_MENSAJE_EXCEL_GUARDADO
            End If

        Else
            Return Constantes.CONST_MENSAJE_EXCEL_ERROR
        End If

    End Function


End Class



