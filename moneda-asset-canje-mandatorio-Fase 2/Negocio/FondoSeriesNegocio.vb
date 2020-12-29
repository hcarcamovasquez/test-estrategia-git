Imports DTO
Imports Datos


Public Class FondoSeriesNegocio

    Private Excel As ExcelWriter = New ExcelWriter
    Public Property rutaArchivoExcel As String

    Public Const CONST_INSERT_EXITO As Integer = 0
    Public Const CONST_ERROR_EXISTE_NEMOTECNICO As Integer = 1
    Public Const CONST_ERROR_EXISTE_NEMO As Integer = 2
    Public Const CONST_NO_HAY As Integer = 3
    Public Const CONST_ERROR_NO_INGRESO_BBDD As Integer = -99
    Public Const CONS_ACCION_ALL As String = "SELECT_ALL"
    Public Const CONST_MENSAJE_EXCEL_GUARDADO As String = "Excel Guardado de forma Exitosa"
    Public Const CONST_MENSAJE_EXCEL_ERROR As String = "Error al Guardar Excel"

    Dim Datos As FondoSeriesDatos = New FondoSeriesDatos

    Public Function GrupoSeriesPorRut(fondoSeries As DTO.FondoSerieDTO) As List(Of DTO.FondoSerieDTO)
        Dim fondoSerieDatos As New Datos.FondoSeriesDatos
        Return fondoSerieDatos.GrupoSeriesPorRut(fondoSeries)
    End Function

    Public Function CompararDatos(fondo As DTO.FondoDTO) As List(Of DTO.FondoDTO)
        Dim seriesDatos As New Datos.FondoSeriesDatos
        Return seriesDatos.CompararDatos(fondo)
    End Function

    Public Function GrupoSeriesPorNemotecnicos(fondoSeries As DTO.FondoSerieDTO) As List(Of DTO.FondoSerieDTO)
        Dim fondoSerieDatos As New Datos.FondoSeriesDatos
        Return fondoSerieDatos.GrupoSeriesPorNemotecnico(fondoSeries)
    End Function

    Public Function GrupoSeriesPorNemotecnico(fondoSeries As DTO.FondoSerieDTO) As List(Of DTO.FondoSerieDTO)
        Dim fondoSerieDatos As New Datos.FondoSeriesDatos
        Return fondoSerieDatos.GrupoSeriesPorNemotecnicos(fondoSeries)
    End Function

    Public Function GrupoSeriesTraerNombre(fondo As FondoDTO, fondoSerie As FondoSerieDTO) As List(Of FondoDTO)
        Dim datosFondoSerie = New Datos.FondoSeriesDatos()
        Return datosFondoSerie.GetListFondoSeriePorNombreRut(fondo, fondoSerie)
    End Function

    Public Function GetListaFondoSerieTodos(fondoSerie As DTO.FondoSerieDTO) As List(Of FondoSerieDTO)
        Dim datosFondoSerie = New Datos.FondoSeriesDatos()
        Return datosFondoSerie.GetFondoSerie(fondoSerie)
    End Function

    Public Function GetListaFondoSerieTodosHitos(fondoSerie As DTO.FondoSerieDTO) As List(Of FondoSerieDTO)
        Dim datosFondoSerie = New Datos.FondoSeriesDatos()
        Return datosFondoSerie.GetFondoSerieHitos(fondoSerie)
    End Function

    Public Function GetListaFondosRut(serie As DTO.FondoSerieDTO) As List(Of DTO.FondoSerieDTO)
        Return Datos.GetFondoTraer(serie)
    End Function

    Public Function GetListaFondosRutHitos(serie As DTO.FondoSerieDTO) As List(Of DTO.FondoSerieDTO)
        Return Datos.GetFondoTraerHitos(serie)
    End Function

    Public Function llenarFiltroRutNombreSerie() As List(Of FondoSerieDTO)
        Return Datos.llenarFiltroRutNombreSerie()
    End Function

    Public Function llenarFiltroRutNombreSerieConsulta() As List(Of FondoSerieDTO)
        Return Datos.llenarFiltroRutNombreSerieConsulta()
    End Function

    Public Function GetListaFondoRut(serie As DTO.FondoSerieDTO) As List(Of DTO.FondoSerieDTO)
        Return Datos.SeriesPorRut(serie)
    End Function

    Public Function GetListaFondoSerieporNemotecnico(serie As FondoSerieDTO) As List(Of FondoSerieDTO)
        Return Datos.GrupoSeriesPorNemotecnico(serie)
    End Function
    Public Function GetListaFondoSerieporNemotecnicoSeries(serie As FondoSerieDTO) As List(Of FondoSerieDTO)
        Return Datos.GrupoSeriesPorNemotecnicoSeries(serie)
    End Function
    Public Function GetListaFondoSerieGrupoCompatible(serie As FondoSerieDTO) As List(Of FondoSerieDTO)
        Return Datos.GrupoSeriesPorCompatible(serie)
    End Function
    Public Function GetListaFondoSerieConFiltro(fondoSerie As FondoSerieDTO, fondo As FondoDTO) As List(Of FondoSerieDTO)
        Dim fondoDatos As New Datos.FondoSeriesDatos
        Return fondoDatos.GetListaFiltroFondoSerie(fondoSerie, fondo)
    End Function
    Public Function GetListaFondoSerieConFiltroConsultar(fondoSerie As FondoSerieDTO, fondo As FondoDTO) As List(Of FondoSerieDTO)
        Dim fondoDatos As New Datos.FondoSeriesDatos
        Return fondoDatos.GetListaFiltroFondoSerieConsultar(fondoSerie, fondo)
    End Function
    Public Function GetbyNombreFondo(fondoSerie As FondoSerieDTO, fondo As FondoDTO) As List(Of FondoSerieDTO)
        Dim fondoDatos As New Datos.FondoSeriesDatos
        Return fondoDatos.GetbyNombreFondo(fondoSerie, fondo)
    End Function

    Public Function GetFondos(fondoSerie As FondoSerieDTO) As List(Of FondoDTO)
        Dim fondoDatos As New Datos.FondoSeriesDatos
        Return fondoDatos.GetFondos(fondoSerie)
    End Function

    Public Function GetFondoSerie(serie As DTO.FondoSerieDTO) As List(Of DTO.FondoSerieDTO)

        Dim fondoSerieDatos As New Datos.FondoSeriesDatos
        Return fondoSerieDatos.GetFondoSerie(serie)
    End Function
    Public Function GetByMoneda(serie As DTO.FondoSerieDTO) As List(Of DTO.FondoSerieDTO)
        Dim fondoDatos As New Datos.FondoSeriesDatos
        Return fondoDatos.GetByMoneda(serie)
    End Function

    Public Function CargarDistinctNemotecnicoHitos(FondoSeries As FondoSerieDTO) As List(Of FondoSerieDTO)
        Dim datosFondoSeries = New Datos.FondoSeriesDatos()
        Return datosFondoSeries.CargarDistinctNemotecnicoHitos(FondoSeries)
    End Function

    Public Function DeleteFondoSerie(serie As FondoSerieDTO) As Integer
        Dim datosFondoSerie = New Datos.FondoSeriesDatos
        Return datosFondoSerie.DeleteFondoSerie(serie)
    End Function

    Public Function verificarNemotecnico(fondo As FondoSerieDTO) As Integer
        Dim fondoDatos As New Datos.FondoSeriesDatos
        Dim fondoConsultado As FondoSerieDTO = fondoDatos.GetFondoSeries(fondo)
        Dim NemotecnicoConsultado As FondoSerieDTO = fondoDatos.GetFondoSeriesNemotecnico(fondo)

        If fondoConsultado Is Nothing Then
            If NemotecnicoConsultado Is Nothing Then
                Return CONST_NO_HAY
            Else
                Return CONST_ERROR_EXISTE_NEMO
            End If
        Else
            Return CONST_ERROR_EXISTE_NEMOTECNICO
        End If

    End Function

    Public Function InsertFondoSerie(fondo As FondoSerieDTO) As Integer
        Dim fondoDatos As New Datos.FondoSeriesDatos
        Dim fondoConsultado As FondoSerieDTO = fondoDatos.GetFondoSeries(fondo)
        Dim NemotecnicoConsultado As FondoSerieDTO = fondoDatos.GetFondoSeriesNemotecnico(fondo)

        If fondoConsultado Is Nothing Then
            If NemotecnicoConsultado Is Nothing Then
                Return fondoDatos.InsertFondoSerie(fondo)
            Else
                Return CONST_ERROR_EXISTE_NEMO
            End If
        Else
            Return CONST_ERROR_EXISTE_NEMOTECNICO
        End If

    End Function
    Public Function UpdateFondoSerie(fondoSerie As FondoSerieDTO) As Integer
        Dim fondoDatos As New Datos.FondoSeriesDatos
        Return fondoDatos.UpdateFondoSerie(fondoSerie)
    End Function

    Public Function GetFondosSeries(serie As FondoSerieDTO) As FondoSerieDTO
        Dim serieRetorno As FondoSerieDTO
        Dim fondoserie As New Datos.FondoSeriesDatos

        serieRetorno = fondoserie.GetFondoSeries(serie)
        Return serieRetorno
    End Function

    Public Function ExportarAExcel(fondoSerie As FondoSerieDTO, fondo As FondoDTO) As String
        Dim fondoSerieDatos As New Datos.FondoSeriesDatos
        Dim ListaFondoSerie As List(Of FondoSerieDTO) = fondoSerieDatos.GetListaFiltroFondoSerieConsultar(fondoSerie, fondo)


        If CrearExcelFondoSerie(ListaFondoSerie) Then

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

    Public Function CrearExcelFondoSerie(ListaFondoSerie As List(Of FondoSerieDTO)) As Boolean
        If Excel.CrearExcelFondoSerie(ListaFondoSerie) Then
            Return True
        End If
        Return False
    End Function

    Public Function GetByRutAgrupacion(fondoSerie As FondoSerieDTO) As List(Of FondoSerieDTO)
        Dim fondoDatos As New Datos.FondoSeriesDatos
        Return fondoDatos.GetByRutAgrupacion(fondoSerie)
    End Function

    Public Function filtrarNemotecnicoPorFondo(fondoSerie As FondoSerieDTO) As List(Of FondoSerieDTO)
        Dim fondoDatos As New Datos.FondoSeriesDatos
        Return fondoDatos.filtrarNemotecnicoPorFondo(fondoSerie)
    End Function

    Public Function filtrarGrupoCompatiblePorFondo(fondoSerie As FondoSerieDTO) As List(Of FondoSerieDTO)
        Dim fondoDatos As New Datos.FondoSeriesDatos
        Return fondoDatos.filtrarGrupoCompatiblePorFondo(fondoSerie)
    End Function
End Class
