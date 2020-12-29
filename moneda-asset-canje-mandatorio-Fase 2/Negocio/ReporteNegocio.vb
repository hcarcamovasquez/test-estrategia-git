Imports DTO
Imports Datos

Public Class Reportenegocio
    Private Excel As ExcelWriter = New ExcelWriter
    Public Property rutaArchivosExcel As String

    Public Const CONST_INSERT_EXITO As Integer = 0
    Public Const CONST_ERROR_EXISTE_RUTYMULTIFONDO As Integer = 1
    Public Const CONST_ERROR_RUT_SIN_MULTIFONDO As Integer = 2
    Public Const CONST_ERROR_NO_INGRESO_BBDD As Integer = -99
    Public Const CONST_ACCION_ALL As String = "SELECT_ALL"
    Public Const CONST_MENSAJE_EXCEL_GUARDADO As String = "Excel Guardado de forma Exitosa"
    Public Const CONST_MENSAJE_EXCEL_ERROR As String = "Error al Guardar Excel"

    'Dim Datos As AportanteDatos = New AportanteDatos
    Dim Datos As ReporteDatos = New ReporteDatos

    Public Function GetListaReporte(listaFondo As List(Of FondoDTO), FechaProceso As Nullable(Of Date), FechaCanje As Nullable(Of Date), txtCambio As Decimal) As List(Of ReporteFechaCorteDTO)
        Return Datos.GetListaReporte(listaFondo, FechaProceso, FechaCanje, txtCambio)
    End Function

    Public Function GetNuevaEvaluacion(proceso As ProcesoDTO) As ReporteFechaCorteDTO
        Return Datos.GetNuevaEvaluacion(proceso)
    End Function

    Public Function GetNuevaEvaluacionHija(proceso As ProcesoDTO) As ReporteFechaCorteDTO
        Return Datos.GetNuevaEvaluacionHija(proceso)
    End Function

    Public Function UpdateProcesoByID(proceso As ReporteFechaCorteDTO) As Boolean
        Return Datos.UpdateProcesoByID(proceso)
    End Function

    Public Function InsertarAportante(aportante As AportanteDTO) As Integer
        'Dim existe As Integer = BuscarTodoAportante(aportante)

        'If existe = Constantes.CONST_OPERACION_EXITOSA Then
        '    Return Datos.InsertarAportante(aportante)
        'Else
        '    Return existe
        'End If
        Return 0
    End Function

    Public Function UpdateAportante(Aportante As AportanteDTO) As Boolean
        ' Dim Result As Integer = Datos.UpdateAportante(Aportante)

        'Return (Result >= 0)
        Return 0
    End Function

    Public Function ExportarAExcel(aportante As AportanteDTO, FechaHasta As Nullable(Of Date)) As String
        'Dim ListaAportante As List(Of AportanteDTO) = Datos.GetListaAportantes(aportante, FechaHasta)
        'If CrearExcel(ListaAportante) Then

        '    If Excel.rutaArchivosExcel Is Nothing Then
        '        Return CONST_MENSAJE_EXCEL_ERROR
        '    Else
        '        Me.rutaArchivosExcel = Excel.rutaArchivosExcel
        '        Return CONST_MENSAJE_EXCEL_GUARDADO
        '    End If

        'Else
        '    Return CONST_MENSAJE_EXCEL_ERROR
        'End If
        Return ""
    End Function

    Public Function CrearExcel(listaAportante As List(Of AportanteDTO)) As Boolean
        If Excel.CrearExcelAportantes(listaAportante) Then
            Return True
        End If
        Return False
    End Function

    Public Function GetListaProcesoPorId(PR_ID As Integer) As ReporteFechaCorteDTO
        Return Datos.GetListaProcesoPorId(PR_ID)
    End Function

End Class
