Imports DTO
Imports WSCanjeMandatorio.WSSeries
Public Class FondoSeriesDatos
    Public Function GrupoSeriesPorRut(fondoSeries As DTO.FondoSerieDTO) As List(Of DTO.FondoSerieDTO)
        Dim Ws = New WSCanjeMandatorio.WSSeries()
        Return Ws.GrupoSeriesPorRut(fondoSeries)
    End Function
    Public Function CompararDatos(fondo As DTO.FondoDTO) As List(Of FondoDTO)
        Dim ws = New WSCanjeMandatorio.WSSeries()
        Return ws.CompararDatos(fondo)
    End Function
    Public Function GetByMoneda(Serie As DTO.FondoSerieDTO) As List(Of FondoSerieDTO)
        Dim ws = New WSCanjeMandatorio.WSSeries()
        Return ws.GetByMoneda(Serie)
    End Function

    Public Function GetFondoSeries(fondoSerie As FondoSerieDTO) As FondoSerieDTO
        Dim Ws = New WSCanjeMandatorio.WSSeries()
        Return Ws.GetFondo(fondoSerie)
    End Function
    Public Function GetFondoSeriesNemotecnico(fondoSerie As FondoSerieDTO) As FondoSerieDTO
        Dim Ws = New WSCanjeMandatorio.WSSeries()
        Return Ws.GetFondoNemotecncio(fondoSerie)
    End Function

    Public Function SeriesPorRut(fondoSeries As DTO.FondoSerieDTO) As List(Of DTO.FondoSerieDTO)
        Dim Ws = New WSCanjeMandatorio.WSSeries()
        Return Ws.ConsultarPorRut(fondoSeries)
    End Function

    Public Function GrupoSeriesPorNemotecnico(fondoSerie As DTO.FondoSerieDTO) As List(Of DTO.FondoSerieDTO)
        Dim ws = New WSCanjeMandatorio.WSSeries()
        Return ws.ConsultarPorNemotecnico(fondoSerie)
    End Function
    Public Function GrupoSeriesPorNemotecnicoSeries(fondoSerie As DTO.FondoSerieDTO) As List(Of DTO.FondoSerieDTO)
        Dim ws = New WSCanjeMandatorio.WSSeries()
        Return ws.ConsultarPorNemotecnicoSeries(fondoSerie)
    End Function
    Public Function GrupoSeriesPorNemotecnicos(fondoSerie As DTO.FondoSerieDTO) As List(Of DTO.FondoSerieDTO)
        Dim ws = New WSCanjeMandatorio.WSSeries()
        Return ws.GrupoSeriesPorNemotecnico(fondoSerie)
    End Function

    Public Function GetListFondoSeriePorNombreRut(fondo As DTO.FondoDTO, fondoSerie As FondoSerieDTO) As List(Of DTO.FondoDTO)
        Dim ws = New WSCanjeMandatorio.WSSeries()
        Return ws.ConsultarPorNombreRut(fondo, fondoSerie)
    End Function

    Public Function GrupoSeriesPorCompatible(fondoserie As DTO.FondoSerieDTO) As List(Of DTO.FondoSerieDTO)
        Dim ws = New WSCanjeMandatorio.WSSeries()
        Return ws.ConsultarPorGrupoCompatible(fondoserie)
    End Function

    Public Function GetListaFondoSerie(fondoserie As FondoSerieDTO, fondo As FondoDTO) As List(Of FondoSerieDTO)
        Dim Ws = New WSCanjeMandatorio.WSSeries()
        Return Ws.FondoSerieConsultar(fondoserie)
    End Function
    Public Function GetListaFiltroFondoSerie(fondoserie As FondoSerieDTO, fondo As FondoDTO) As List(Of FondoSerieDTO)
        Dim Ws = New WSCanjeMandatorio.WSSeries()
        Return Ws.FSConsultar(fondoserie, fondo)
    End Function
    Public Function GetListaFiltroFondoSerieConsultar(fondoserie As FondoSerieDTO, fondo As FondoDTO) As List(Of FondoSerieDTO)
        Dim Ws = New WSCanjeMandatorio.WSSeries()
        Return Ws.FSConsultarSerie(fondoserie, fondo)
    End Function
    Public Function GetbyNombreFondo(fondoserie As FondoSerieDTO, fondo As FondoDTO) As List(Of FondoSerieDTO)
        Dim Ws = New WSCanjeMandatorio.WSSeries()
        Return Ws.GetbyNombreFondo(fondoserie, fondo)
    End Function
    Public Function GetFondoTraer(fondoserie As FondoSerieDTO) As List(Of FondoSerieDTO)
        Dim Ws = New WSCanjeMandatorio.WSSeries()
        Return Ws.ConsultarPorRut(fondoserie)
    End Function

    Public Function GetFondoTraerHitos(fondoserie As FondoSerieDTO) As List(Of FondoSerieDTO)
        Dim Ws = New WSCanjeMandatorio.WSSeries()
        Return Ws.ConsultarPorRutHitos(fondoserie)
    End Function

    Public Function llenarFiltroRutNombreSerie() As List(Of FondoSerieDTO)
        Dim Ws = New WSCanjeMandatorio.WSSeries()
        Return Ws.llenarFiltroRutNombreSerie()
    End Function
    Public Function llenarFiltroRutNombreSerieConsulta() As List(Of FondoSerieDTO)
        Dim Ws = New WSCanjeMandatorio.WSSeries()
        Return Ws.llenarFiltroRutNombreSerieConsulta()
    End Function
    Public Function GetFondoSerie(fondoserie As FondoSerieDTO) As List(Of FondoSerieDTO)
        Dim Ws = New WSCanjeMandatorio.WSSeries()
        Return Ws.FondoSerieConsultar(fondoserie)
    End Function
    Public Function GetFondoSerieHitos(fondoserie As FondoSerieDTO) As List(Of FondoSerieDTO)
        Dim Ws = New WSCanjeMandatorio.WSSeries()
        Return Ws.FondoSerieConsultarHitos(fondoserie)
    End Function

    Public Function CargarDistinctNemotecnicoHitos(FondoSeries As DTO.FondoSerieDTO) As List(Of DTO.FondoSerieDTO)
        Dim ws = New WSCanjeMandatorio.WSSeries()
        Return ws.CargarDistinctNemotecnicoHitos(FondoSeries)
    End Function

    Public Function GetFondos(FondoSeries As FondoSerieDTO) As List(Of FondoDTO)
        Dim ws = New WSCanjeMandatorio.WSSeries()
        Return ws.GetFondos(FondoSeries)
    End Function

    Public Function DeleteFondoSerie(serie As FondoSerieDTO) As Integer
        Dim Ws = New WSCanjeMandatorio.WSSeries

        If Ws.FondoSerieEliminar(serie) Then
            Return Constantes.CONST_OPERACION_EXITOSA
        Else
            Return Constantes.CONST_ERROR_BBDD
        End If
    End Function

    Public Function InsertFondoSerie(fondoSerie As FondoSerieDTO) As Integer
        Dim Ws = New WSCanjeMandatorio.WSSeries()

        If Ws.FondoSerieIngresar(fondoSerie) Then
            Return Constantes.CONST_OPERACION_EXITOSA
        Else
            Return Constantes.CONST_ERROR_BBDD
        End If
    End Function

    Public Function UpdateFondoSerie(serie As FondoSerieDTO) As Integer
        Dim Ws = New WSCanjeMandatorio.WSSeries

        If Ws.FondoSerieModificar(serie) Then
            Return Constantes.CONST_OPERACION_EXITOSA
        Else
            Return Constantes.CONST_ERROR_BBDD
        End If
    End Function

    Public Function BuscarRelaciones(fondoSerie As FondoSerieDTO) As Integer
        Dim Ws = New WSCanjeMandatorio.WSSeries()

        Return Ws.BuscarRelaciones(fondoSerie)
    End Function

    Public Function GetByRutAgrupacion(fondoSerie As FondoSerieDTO) As List(Of FondoSerieDTO)
        Dim Ws = New WSCanjeMandatorio.WSSeries()
        Return Ws.GetByRutAgrupacion(fondoSerie)
    End Function

    Public Function filtrarNemotecnicoPorFondo(fondoSerie As FondoSerieDTO) As List(Of FondoSerieDTO)
        Dim Ws = New WSCanjeMandatorio.WSSeries()
        Return Ws.filtrarNemotecnicoPorFondo(fondoSerie)
    End Function

    Public Function filtrarGrupoCompatiblePorFondo(fondoSerie As FondoSerieDTO) As List(Of FondoSerieDTO)
        Dim Ws = New WSCanjeMandatorio.WSSeries()
        Return Ws.filtrarGrupoCompatiblePorFondo(fondoSerie)
    End Function
End Class




