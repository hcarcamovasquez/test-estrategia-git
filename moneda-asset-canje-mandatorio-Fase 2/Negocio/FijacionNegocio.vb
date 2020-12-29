
Imports DTO
Imports Datos

Public Class FijacionNegocio
    Dim Datos As FijacionDatos = New FijacionDatos
    Private Excel As ExcelWriter = New ExcelWriter
    Public Property rutaArchivosExcel As String

    Public Const CONST_INSERT_EXITO As Integer = 0
    Public Const CONST_ERROR_EXISTE_RUTYMULTIFONDO As Integer = 1
    Public Const CONST_ERROR_RUT_SIN_MULTIFONDO As Integer = 2
    Public Const CONST_ERROR_NO_INGRESO_BBDD As Integer = -99
    Public Const CONST_ACCION_ALL As String = "SELECT_ALL"
    Public Const CONST_MENSAJE_EXCEL_GUARDADO As String = "Excel Guardado de forma Exitosa"
    Public Const CONST_MENSAJE_EXCEL_ERROR As String = "Error al Guardar Excel"

    Public Function ConsultarTodos(Fijacion As FijacionDTO) As List(Of DTO.FijacionDTO)
        Dim FijacionDatos As New Datos.FijacionDatos
        Return FijacionDatos.ConsultarTodos(Fijacion)
    End Function
    Public Function ConsultarFiltro(Fijacion As FijacionDTO, FechaNavDesde As Nullable(Of Date), FechaNavHasta As Nullable(Of Date),
                                    FechaTCDesde As Nullable(Of Date), FechaTCHasta As Nullable(Of Date),
                                    FechaPagoDesde As Nullable(Of Date), FechaPagoHasta As Nullable(Of Date)) As List(Of DTO.FijacionDTO)
        Dim FijacionDatos As New Datos.FijacionDatos
        Return FijacionDatos.ConsultarFiltro(Fijacion, FechaNavDesde, FechaNavHasta, FechaTCDesde, FechaTCHasta, FechaPagoDesde, FechaPagoHasta)
    End Function

    Public Function ConsultarTipoTransacion() As List(Of DTO.FijacionDTO)
        Dim FijacionDatos As New Datos.FijacionDatos
        Return FijacionDatos.ConsultarTipoTransacion()
    End Function
    Public Function Nemotecnico() As List(Of DTO.FondoSerieDTO)
        Dim Fondo As New Datos.FijacionDatos
        Return Fondo.nemotecnico()
    End Function
    Public Function ConsultarFijacionNav() As List(Of DTO.FijacionDTO)
        Dim FijacionDatos As New Datos.FijacionDatos
        Return FijacionDatos.ConsultarFijacionNav()
    End Function

    Public Function CargarFiltroRutFondo(Fijacion As FijacionDTO) As List(Of FijacionDTO)
        Dim datosFijacion = New Datos.FijacionDatos()
        Return datosFijacion.CargarFiltroRutFondo(Fijacion)
    End Function

    Public Function UpdateFijacion(TipoTransaccion As String, IdCanje As String, NavSaliente As Int32, NavEntrante As String)
        Dim FijacionDatos As New Datos.FijacionDatos
        Return FijacionDatos.UpdateFijacion(TipoTransaccion, IdCanje, NavSaliente, NavEntrante)
    End Function
    Public Function UpdateFijacionNav(ID As Integer, TipoTransaccion As String)
        Dim FijacionDatos As New Datos.FijacionDatos
        Return FijacionDatos.UpdateFijacionNav(ID, TipoTransaccion)
    End Function
    Public Function UpdateFijacionTC(ID As Integer, TipoTransaccion As String)
        Dim FijacionDatos As New Datos.FijacionDatos
        Return FijacionDatos.UpdateFijacionTC(ID, TipoTransaccion)
    End Function
    Public Function ConsultarFijacionTC() As List(Of DTO.FijacionDTO)
        Dim FijacionDatos As New Datos.FijacionDatos
        Return FijacionDatos.ConsultarFijacionTC()
    End Function

    Public Function GetFijacion(Fijacion As FijacionDTO) As FijacionDTO
        Dim FijacionRetorno As FijacionDTO
        Dim FijacionDatos As New Datos.FijacionDatos
        FijacionRetorno = FijacionDatos.GetListaFijacion(Fijacion)
        Return FijacionRetorno
    End Function


    Public Function ExportarAExcelTodos(Fijacion As FijacionDTO) As String
        Dim FijacionDatos As New Datos.FijacionDatos
        Dim ListaFijacion As List(Of FijacionDTO) = FijacionDatos.ConsultarTodos(Fijacion)

        If CrearExcel(ListaFijacion) Then

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

    Public Function ExportarAExcel(Fijacion As FijacionDTO, FechaNAVDesde As Nullable(Of Date), FechaNAVHasta As Nullable(Of Date),
                                   FechaTCDesde As Nullable(Of Date), FechaTCHasta As Nullable(Of Date),
                                   FechaPagoDesde As Nullable(Of Date), FechaPagoHasta As Nullable(Of Date)) As String

        Dim FijacionDatos As New Datos.FijacionDatos
        Dim ListaFijacion As List(Of FijacionDTO) = FijacionDatos.ConsultarFiltro(Fijacion, FechaNAVDesde, FechaNAVHasta, FechaTCDesde, FechaTCHasta, FechaPagoDesde, FechaPagoHasta)

        If CrearExcel(ListaFijacion) Then

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

    Public Function CrearExcel(ListaFijacion As List(Of FijacionDTO)) As Boolean
        If Excel.CrearExcelFijacion(ListaFijacion) Then
            Return True
        End If
        Return False
    End Function
End Class

