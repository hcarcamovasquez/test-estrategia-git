Imports DTO
Imports Datos.ValoresCuotaDatos

Public Class ValoresCuotaNegocio
    Private Excel As ExcelWriter = New ExcelWriter
    Public Property rutaArchivosExcel As String

    Private Const CONST_FONDO_YA_EXISTE As Integer = 1
    Public Const CONST_MENSAJE_EXCEL_GUARDADO As String = "Excel Guardado de forma Exitosa"
    Public Const CONST_MENSAJE_EXCEL_ERROR As String = "Error al Guardar Excel"

    Public Function InsertvaloresCuota(valoresCuota As VcSerieDTO) As Integer
        Dim valoresCuotaDatos As New Datos.ValoresCuotaDatos

        'Dim fondoConsultado As FondoDTO = fondoDatos.GetFondo(fondo)

        'If fondoConsultado Is Nothing Then
        Return valoresCuotaDatos.InsertvaloresCuota(valoresCuota)
        'Else
        '    Return CONST_FONDO_YA_EXISTE
        'End If
    End Function

    Public Function GetListaValoresCuotaConFiltro(ValoresCuota As VcSerieDTO, FechaHasta As Nullable(Of Date)) As List(Of VcSerieDTO)
        Dim ValoresCuotaDatos As New Datos.ValoresCuotaDatos
        Return ValoresCuotaDatos.GetListaValoresCuotaConFiltro(ValoresCuota, FechaHasta)
    End Function

    Public Function ConsultarTodos(ValoresCuota As DTO.VcSerieDTO) As List(Of DTO.VcSerieDTO)
        Dim ValoresCuotaDatos As New Datos.ValoresCuotaDatos
        Return ValoresCuotaDatos.ConsultarTodos(ValoresCuota)
    End Function

    Public Function ValoresCuotaPorNemotecnicoYFecha(ValoresCuota As DTO.VcSerieDTO) As List(Of DTO.VcSerieDTO)
        Dim ValoresCuotaDatos As New Datos.ValoresCuotaDatos
        Return ValoresCuotaDatos.ValoresCuotaPorNemotecnicoYFecha(ValoresCuota)
    End Function
    Public Function UltimoValorCuota(ValoresCuota As DTO.VcSerieDTO) As List(Of DTO.VcSerieDTO)
        Dim ValoresCuotaDatos As New Datos.ValoresCuotaDatos
        Return ValoresCuotaDatos.UltimoValorCuota(ValoresCuota)
    End Function
    Public Function GetValoresCuota(ValoresCuota As VcSerieDTO) As VcSerieDTO
        Dim ValoresCuotaRetorno As VcSerieDTO
        Dim ValoresCuotaDatos As New Datos.ValoresCuotaDatos

        ValoresCuotaRetorno = ValoresCuotaDatos.GetValoresCuota(ValoresCuota)
        Return ValoresCuotaRetorno
    End Function

    Public Function UpdateValoresCuota(ValoresCuota As VcSerieDTO) As Integer
        Dim ValoresCuotaDatos As New Datos.ValoresCuotaDatos

        Return ValoresCuotaDatos.UpdateValoresCuota(ValoresCuota)
    End Function

    Public Function DeleteValoresCuota(ValoresCuota As VcSerieDTO) As Integer
        Dim ValoresCuotaDatos As New Datos.ValoresCuotaDatos
        Dim relaciones As Integer = ValoresCuotaDatos.BuscarRelaciones(ValoresCuota)

        If relaciones = 0 Then
            Return ValoresCuotaDatos.DeleteValoresCuota(ValoresCuota)
        Else
            Return Constantes.CONST_ERROR_NO_SE_PUEDE_BORRAR
        End If

    End Function

    Public Function ExportarAExcel(ValoresCuota As VcSerieDTO, FechaHasta As Nullable(Of Date)) As String
        Dim ValoresCuotaDatos As New Datos.ValoresCuotaDatos
        Dim ListaValoresCuota As List(Of VcSerieDTO) = ValoresCuotaDatos.GetListaValoresCuotaConFiltro(ValoresCuota, FechaHasta)

        If CrearExcel(ListaValoresCuota) Then

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

    Public Function ExportarAExcelTodos(ValoresCuota As VcSerieDTO) As String
        Dim ValoresCuotaDatos As New Datos.ValoresCuotaDatos
        Dim ListaValoresCuota As List(Of VcSerieDTO) = ValoresCuotaDatos.ConsultarTodos(ValoresCuota)

        If CrearExcel(ListaValoresCuota) Then

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

    Public Function CrearExcel(ListaValoresCuota As List(Of VcSerieDTO)) As Boolean
        If Excel.CrearExcelValoresCuota(ListaValoresCuota) Then
            Return True
        End If
        Return False
    End Function

    Public Function CargarFiltroNemotecnico(ValoresCuota As VcSerieDTO) As List(Of VcSerieDTO)
        Dim datosvc = New Datos.ValoresCuotaDatos()
        Return datosvc.CargarFiltroNemotecnico(ValoresCuota)
    End Function
End Class
