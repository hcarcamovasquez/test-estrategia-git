
Imports DTO
Imports Datos

Public Class RescateNegocio
    Dim Datos As RescateDatos = New RescateDatos
    Private Excel As ExcelWriter = New ExcelWriter
    Public Property rutaArchivosExcel As String

    Public Const CONST_INSERT_EXITO As Integer = 0
    Public Const CONST_ERROR_EXISTE_RUTYMULTIFONDO As Integer = 1
    Public Const CONST_ERROR_RUT_SIN_MULTIFONDO As Integer = 2
    Public Const CONST_ERROR_NO_INGRESO_BBDD As Integer = -99
    Public Const CONST_ACCION_ALL As String = "SELECT_ALL"
    Public Const CONST_MENSAJE_EXCEL_GUARDADO As String = "Excel Guardado de forma Exitosa"
    Public Const CONST_MENSAJE_EXCEL_ERROR As String = "Error al Guardar Excel"

    Public Function ConsultarTodos(Rescate As RescatesDTO) As List(Of DTO.RescatesDTO)
        Dim RescateDatos As New Datos.RescateDatos
        Return RescateDatos.ConsultarTodos(Rescate)
    End Function

    Public Function ConsultarTransito(Rescate As RescatesDTO) As List(Of DTO.RescatesDTO)
        Dim RescateDatos As New Datos.RescateDatos
        Return RescateDatos.RescateEnTransito(Rescate)
    End Function

    Public Function ConsultarPorFiltro(Rescate As RescatesDTO, FechaDesdeSolicitud As Nullable(Of Date), FechaHastaSolicitud As Nullable(Of Date), FechaDesdeNAV As Nullable(Of Date), FechaHastaNAV As Nullable(Of Date), FechaDesdePago As Nullable(Of Date), FechaHastaPago As Nullable(Of Date)) As List(Of RescatesDTO)
        Dim RescateDatos As New Datos.RescateDatos
        Return RescateDatos.ConsultarPorFiltro(Rescate, FechaDesdeSolicitud, FechaHastaSolicitud, FechaDesdeNAV, FechaHastaNAV, FechaDesdePago, FechaHastaPago)
    End Function

    Public Function SelectFechaPagoSIRescatable(FechaPagoFondoRescatableINT As Integer, FechaCalculo As DateTime, SoloDiasHabiles As Integer)
        Dim FechaPagoRetorno As DateTime
        Dim RescateDatos As New Datos.RescateDatos

        FechaPagoRetorno = Datos.SelectFechaPagoSIRescatable(FechaPagoFondoRescatableINT, FechaCalculo, SoloDiasHabiles)
        Return FechaPagoRetorno
    End Function

    Public Function SelectRescatesTransito(Rescate As RescatesDTO) As RescatesDTO
        Dim RescateRetorno As RescatesDTO
        Dim RescateDatos As New Datos.RescateDatos

        RescateRetorno = RescateDatos.SelectRescatesTransito(Rescate)
        Return RescateRetorno
    End Function
    Public Function SelectRescatesTransito2(Rescate As RescatesDTO) As RescatesDTO
        Dim RescateRetorno As RescatesDTO
        Dim RescateDatos As New Datos.RescateDatos

        RescateRetorno = RescateDatos.SelectRescatesTransito2(Rescate)
        Return RescateRetorno
    End Function

    Public Function InsertRescates(Rescate As RescatesDTO)
        Dim RescateDatos As New Datos.RescateDatos

        Return RescateDatos.InsertRescate(Rescate)

    End Function

    Public Function CargarFiltroNombreAportante(Rescate As RescatesDTO) As List(Of RescatesDTO)
        Dim datosRescate = New Datos.RescateDatos()
        Return datosRescate.CargarFiltroNombreAportante(Rescate)
    End Function

    Public Function CargarFiltroNombreFondo(Rescate As RescatesDTO) As List(Of RescatesDTO)
        Dim datosRescate = New Datos.RescateDatos()
        Return datosRescate.CargarFiltroNombreFondo(Rescate)
    End Function

    Public Function CargarFiltroNemotecnico(Rescate As RescatesDTO) As List(Of RescatesDTO)
        Dim datosRescate = New Datos.RescateDatos()
        Return datosRescate.CargarFiltroNemotecnico(Rescate)
    End Function

    Public Function GetFijacionId(Id As Int32) As RescatesDTO
        Dim RescateRetorno As RescatesDTO
        Dim datosRescate As New Datos.RescateDatos

        RescateRetorno = datosRescate.GetFijacionId(Id)
        Return RescateRetorno
    End Function

    Public Function GetRescateOne(Rescate As RescatesDTO) As RescatesDTO
        Dim RescateRetorno As RescatesDTO
        Dim datosRescate As New Datos.RescateDatos

        RescateRetorno = datosRescate.GetRescateOne(Rescate)
        Return RescateRetorno
    End Function

    Public Function SelectRescatesHoy(Rescate As RescatesDTO) As RescatesDTO
        Dim RescateRetorno As RescatesDTO
        Dim datosRescate As New Datos.RescateDatos

        RescateRetorno = datosRescate.SelectRescatesHoy(Rescate)
        Return RescateRetorno
    End Function

    Public Function RecalculoFijacionNAV(Rescate As RescatesDTO) As Integer
        Dim RescateDatos As New Datos.RescateDatos

        Return RescateDatos.RecalculoFijacionNAV(Rescate)
    End Function

    Public Function RecalculoFijacionTC(Rescate As RescatesDTO) As Integer
        Dim RescateDatos As New Datos.RescateDatos

        Return RescateDatos.RecalculoFijacionTC(Rescate)
    End Function

    Public Function UpdateRescate(Rescate As RescatesDTO) As Integer
        Dim RescateDatos As New Datos.RescateDatos

        Return RescateDatos.UpdateRescate(Rescate)
    End Function

    Public Function DeleteRescate(Rescate As RescatesDTO) As Integer
        Dim RescateDatos As New Datos.RescateDatos

        Return RescateDatos.DeleteRescate(Rescate)
    End Function


    Public Function ExportarAExcelTodos(Rescate As RescatesDTO) As String
        Dim RescateDatos As New Datos.RescateDatos
        Dim ListaRescates As List(Of RescatesDTO) = RescateDatos.ConsultarTodos(Rescate)

        If CrearExcel(ListaRescates) Then

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

    Public Function ExportarAExcel(Rescate As RescatesDTO, FechaDesdeSolicitud As Nullable(Of Date), FechaHastaSolicitud As Nullable(Of Date), FechaDesdeNAV As Nullable(Of Date), FechaHastaNAV As Nullable(Of Date), FechaDesdePago As Nullable(Of Date), FechaHastaPago As Nullable(Of Date)) As String
        Dim RescateDatos As New Datos.RescateDatos
        Dim ListaRescates As List(Of RescatesDTO) = RescateDatos.ConsultarPorFiltro(Rescate, FechaDesdeSolicitud, FechaHastaSolicitud, FechaDesdeNAV, FechaHastaNAV, FechaDesdePago, FechaHastaPago)

        If CrearExcel(ListaRescates) Then

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

    Public Function CrearExcel(ListaRescate As List(Of RescatesDTO)) As Boolean
        If Excel.CrearExcelRescates(ListaRescate) Then
            Return True
        End If
        Return False
    End Function

    Public Function GetRelaciones(Rescates As DTO.RescatesDTO)
        Dim RescatesDatos As New Datos.RescateDatos
        Return RescatesDatos.GetRelaciones(Rescates)
    End Function

    Public Function ValidacionDeFondo() As Boolean

        Return True
    End Function

    Public Function ControlMontoRescateVsPatrimonio(rescate As RescatesDTO, fondo As FondoDTO) As Boolean
        Dim NegocioFondo As Negocio.FondosNegocio = New FondosNegocio()
        If (fondo.ControlTipoControl = "Movil") Then
            Return ControlMovil(rescate, fondo)
        ElseIf (fondo.ControlTipoControl = "Ventana") Then
            'TODO: FALTA AGREGAR LOGICA DE VENTANA A SP
            Return ControlVentana(rescate, fondo)
        Else
            Return False
        End If

        Return False
    End Function

    Public Function ControlVentana(rescate As RescatesDTO, fondo As FondoDTO) As Boolean
        Dim datosRescate As RescateDatos = New RescateDatos()
        Return datosRescate.ControlVentana(rescate, fondo)
    End Function

    Private Function ControlMovil(rescate As RescatesDTO, fondo As FondoDTO) As Boolean
        Dim datosRescate As RescateDatos = New RescateDatos()
        Return datosRescate.ControlMovil(rescate, fondo)
    End Function

    Public Function Prorrata(stringID As String, ByRef stringError As String) As String
        Dim datosRescate As RescateDatos = New RescateDatos()
        Return datosRescate.Prorrata(stringID, stringError)
    End Function
End Class
