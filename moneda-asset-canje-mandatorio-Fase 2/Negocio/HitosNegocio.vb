Imports DTO
Imports Datos.HitosDatos

Public Class HitosNegocio

    Private Excel As ExcelWriter = New ExcelWriter
    Public Property rutaArchivoExcel As String

    Public Const CONST_INSERT_EXITO As Integer = 0
    Public Const CONST_ERROR_EXISTE_FECHACORTE As Integer = 1
    Public Const CONST_NO_HAY As Integer = 2
    Public Const CONST_ERROR_EXISTE_FECHACORTE_POR_FONDO As Integer = 3
    Public Const CONST_ERROR_NO_INGRESO_BBDD As Integer = -99
    Public Const CONS_ACCION_ALL As String = "SELECT_ALL"
    Public Const CONST_MENSAJE_EXCEL_GUARDADO As String = "Excel Guardado de forma Exitosa"
    Public Const CONST_MENSAJE_EXCEL_ERROR As String = "Error al Guardar Excel"



    Public Function ConsultarTodos(Hito As DTO.HitoDTO) As List(Of DTO.HitoDTO)
        Dim HitosDatos As New Datos.HitosDatos
        Return HitosDatos.ConsultarTodos(Hito)
    End Function

    Public Function ConsultarRut(Hito As DTO.HitoDTO) As List(Of DTO.HitoDTO)
        Dim HitosDatos As New Datos.HitosDatos
        Return HitosDatos.GetListHitosPorRut(Hito)
    End Function
    Public Function ConsultarHitosFiltro(Hito As DTO.HitoDTO, fechaHasta As Nullable(Of Date), fondo As FondoDTO) As List(Of DTO.HitoDTO)
        Dim HitosDatos As New Datos.HitosDatos
        Return HitosDatos.GetListaHitosConFiltro(Hito, fechaHasta, fondo)
    End Function
    Public Function ConsultarNombreRut(fondo As DTO.FondoDTO) As List(Of DTO.FondoDTO)
        Dim HitosDatos As New Datos.HitosDatos
        Return HitosDatos.GetListHitoPorNombreRut(fondo)
    End Function

    Public Function CompararDatos(fondo As DTO.FondoDTO) As List(Of DTO.FondoDTO)
        Dim hitosDatos As New Datos.HitosDatos
        Return hitosDatos.CompararDatos(fondo)
    End Function

    Public Function GetListaporHitos(Hitos As HitoDTO) As List(Of HitoDTO)
        Dim HitosDatos As New Datos.HitosDatos
        Return HitosDatos.GetListaporHitos(Hitos)
    End Function

    Public Function ConsultaUltimosParaCertificados(Hitos As HitoDTO) As List(Of HitoDTO)
        Dim HitosDatos As New Datos.HitosDatos
        Return HitosDatos.ConsultaUltimosParaCertificado(Hitos)
    End Function
    Public Function GetFechasParaCertificados(Hitos As HitoDTO) As HitoDTO
        Dim HitosRetorno As HitoDTO
        Dim HitosDatos As New Datos.HitosDatos

        HitosRetorno = HitosDatos.GetFechasParaCertificados(Hitos)
        Return HitosRetorno
    End Function

    Public Function ConsultarUltimoHito(Hitos As HitoDTO) As HitoDTO
        Dim HitosRetorno As HitoDTO
        Dim HitosDatos As New Datos.HitosDatos
        HitosRetorno = HitosDatos.ConsultarUltimoHito(Hitos)
        Return HitosRetorno
    End Function

    Public Function verificarExistentes(hito As HitoDTO) As Integer
        Dim hitosDatos As New Datos.HitosDatos
        Dim hitoConsultado As HitoDTO = hitosDatos.GetHito(hito)
        If hitoConsultado Is Nothing Then
            Return CONST_NO_HAY
        Else
            Return CONST_ERROR_EXISTE_FECHACORTE
        End If

    End Function

    Public Function verificarExistentesPorNombreFondo(hito As HitoDTO) As Integer
        Dim hitosDatos As New Datos.HitosDatos
        Dim hitoPorFondo As HitoDTO = hitosDatos.GetHitoPorNombreFondo(hito)
        If hitoPorFondo Is Nothing Then
            Return CONST_NO_HAY
        Else
            Return CONST_ERROR_EXISTE_FECHACORTE_POR_FONDO
        End If

    End Function

    Public Function InsertHito(hito As HitoDTO) As Integer
        Dim hitosDatos As New Datos.HitosDatos
        Dim hitoConsultado As HitoDTO = hitosDatos.GetHito(hito)

        Return hitosDatos.InsertHito(hito)

    End Function

    Public Function UpdateHito(hito As HitoDTO) As Integer
        Dim hitoDatos As New Datos.HitosDatos
        Return hitoDatos.UpdateHito(hito)
    End Function

    Public Function GetHito(Hito As HitoDTO) As HitoDTO
        Dim hitoRetorno As HitoDTO
        Dim hitoDatos As New Datos.HitosDatos

        hitoRetorno = hitoDatos.GetHito(Hito)
        Return hitoRetorno
    End Function

    Public Function DeleteHito(hito As HitoDTO) As Integer
        Dim datosHito = New Datos.HitosDatos
        Return datosHito.DeleteHito(hito)
    End Function

    Public Function ExportarExcel(Hito As HitoDTO, fechaHasta As Nullable(Of Date), fondo As FondoDTO) As String
        Dim hitoDatos As New Datos.HitosDatos
        Dim listaHito As List(Of HitoDTO) = hitoDatos.GetListaHitosConFiltro(Hito, fechaHasta, fondo)

        If CrearExcel(listaHito) Then
            If Excel.rutaArchivosExcel Is Nothing Then
                Return CONST_MENSAJE_EXCEL_ERROR
            Else
                Me.rutaArchivoExcel = Excel.rutaArchivosExcel
                Return CONST_MENSAJE_EXCEL_GUARDADO
            End If
        Else
            Return CONST_MENSAJE_EXCEL_ERROR
        End If
    End Function

    Public Function CrearExcel(listaHitos As List(Of HitoDTO)) As Boolean
        If Excel.CrearExcelHitos(listaHitos) Then
            Return True
        End If
        Return False
    End Function

    Public Function BuscarRelaciones(hito As HitoDTO) As Integer
        Dim datosHito As New Datos.HitosDatos

        Return datosHito.BuscarRelaciones(hito)

    End Function


End Class
