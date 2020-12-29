Imports DTO
Imports Datos.TipoCambioDatos

Public Class TipoCambioNegocio
    Private Excel As ExcelWriter = New ExcelWriter
    Public Property rutaArchivosExcel As String

    Private Const CONST_TIPO_CAMBIO_YA_EXISTE As Integer = 1
    Public Const CONST_MENSAJE_EXCEL_GUARDADO As String = "Excel Guardado de forma Exitosa"
    Public Const CONST_MENSAJE_EXCEL_ERROR As String = "Error al Guardar Excel"

    Public Function ConsultarPorCodigo(TipoCambio As DTO.TipoCambioDTO) As List(Of DTO.TipoCambioDTO)
        Dim TipoCambioDatos As New Datos.TipoCambioDatos
        Return TipoCambioDatos.ConsultarPorCodigo(TipoCambio)
    End Function

    Public Function ConsultarPorTipoCambioYFecha(TipoCambio As DTO.TipoCambioDTO) As List(Of DTO.TipoCambioDTO)
        Dim TipoCambioDatos As New Datos.TipoCambioDatos
        Return TipoCambioDatos.ConsultarTipoCambioPorCodigoYFecha(TipoCambio)
    End Function

    Public Function UltimoTipoCambioPorCodigo(TipoCambio As DTO.TipoCambioDTO) As List(Of DTO.TipoCambioDTO)
        Dim TipoCambioDatos As New Datos.TipoCambioDatos
        Return TipoCambioDatos.UltimoTipoCambio(TipoCambio)
    End Function
    Public Function GetRelaciones(TipoCambio As DTO.TipoCambioDTO)
        Dim TipoCambioDatos As New Datos.TipoCambioDatos
        Return TipoCambioDatos.GetRelaciones(TipoCambio)
    End Function

    Public Function GetListaTCConFiltro(TipoCambio As TipoCambioDTO, FechaHasta As Nullable(Of Date)) As List(Of TipoCambioDTO)
        Dim TipoCambioDatos As New Datos.TipoCambioDatos
        Return TipoCambioDatos.GetListaTCConFiltro(TipoCambio, FechaHasta)
    End Function
    'Buscar según las fechas y el código
    Public Function GetListaTCConFiltroCodigo(TipoCambio As TipoCambioDTO, FechaHasta As Nullable(Of Date)) As List(Of TipoCambioDTO)
        Dim TipoCambioDatos As New Datos.TipoCambioDatos
        Return TipoCambioDatos.GetListaTCConFiltroCodigo(TipoCambio, FechaHasta)
    End Function
    Public Function GetListaTipoCambioporCodigo(serie As TipoCambioDTO) As List(Of TipoCambioDTO)
        Dim TipoCambioDatos As New Datos.TipoCambioDatos
        Return TipoCambioDatos.GrupoTipoCambioPorCodigo(serie)
    End Function
    Public Function GetListaTipoCambio(TipoCambio As DTO.TipoCambioDTO) As List(Of DTO.TipoCambioDTO)
        Dim TipoCambioDatos As New Datos.TipoCambioDatos
        Return TipoCambioDatos.GetListaTipoCambio(TipoCambio)
    End Function

    Public Function GetTipoCambio(TipoCambio As TipoCambioDTO) As TipoCambioDTO
        Dim TipoCambioRetorno As TipoCambioDTO
        Dim TipoCambioDatos As New Datos.TipoCambioDatos
        If TipoCambio.Codigo <> "CLP" Then
            TipoCambioRetorno = TipoCambioDatos.GetTipoCambio(TipoCambio)
            Return TipoCambioRetorno
        Else

            TipoCambio.Valor = 1
            Return TipoCambio
        End If

    End Function
    Public Function GetTipoCambioPorFecha(TipoCambio As TipoCambioDTO) As TipoCambioDTO
        Dim TipoCambioRetorno As TipoCambioDTO
        Dim TipoCambioDatos As New Datos.TipoCambioDatos

        TipoCambioRetorno = TipoCambioDatos.GetTipoCambioPorFecha(TipoCambio)
        Return TipoCambioRetorno
    End Function

    Public Function DeleteTipoCambio(TipoCambio As TipoCambioDTO) As Integer
        Dim TipoCambiooDatos As New Datos.TipoCambioDatos
        Return (TipoCambiooDatos.DeleteTipoCambio(TipoCambio))
    End Function

    Public Function InsertTipoCambio(TipoCambio As TipoCambioDTO) As Integer
        Dim TipoCambioDatos As New Datos.TipoCambioDatos
        Dim TipoCambioConsultado As TipoCambioDTO = TipoCambioDatos.GetTipoCambio(TipoCambio)
        If TipoCambioConsultado Is Nothing Then
            Return TipoCambioDatos.InsertTipoCambio(TipoCambio)
        Else
            Return CONST_TIPO_CAMBIO_YA_EXISTE
        End If
    End Function

    Public Function UpdateTipoCambio(TipoCambio As TipoCambioDTO) As Integer
        Dim TipoCambioDatos As New Datos.TipoCambioDatos

        Return TipoCambioDatos.UpdateTipoCambio(TipoCambio)
    End Function

    Public Function ExportarAExcel(TipoCambio As TipoCambioDTO, Codigo As String, FechaHasta As Nullable(Of Date)) As String
        Dim TipoCambioDatos As New Datos.TipoCambioDatos
        Dim ListaTipoCambio As List(Of TipoCambioDTO)

        If (Not Codigo = "" And FechaHasta = "31/12/9999" And TipoCambio.FechaIngreso.ToString Is Nothing) Then
            ListaTipoCambio = ConsultarPorCodigo(TipoCambio)
        ElseIf (Not Codigo = "" And (Not FechaHasta Is Nothing Or Not TipoCambio.FechaIngreso.ToString Is Nothing)) Then
            ListaTipoCambio = TipoCambioDatos.GetListaTCConFiltroCodigo(TipoCambio, FechaHasta)
        Else
            ListaTipoCambio = TipoCambioDatos.GetListaTCConFiltro(TipoCambio, FechaHasta)
        End If


        If CrearExcel(ListaTipoCambio) Then

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

    Public Function CrearExcel(ListaTipoCambio As List(Of TipoCambioDTO)) As Boolean
        If Excel.CrearExcelTipoCambio(ListaTipoCambio) Then
            Return True
        End If
        Return False
    End Function

End Class
