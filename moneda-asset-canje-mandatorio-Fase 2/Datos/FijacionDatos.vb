Imports DTO
Imports WSCanjeMandatorio.WSRescates

Public Class FijacionDatos

    Public Function ConsultarTodos(Fijacion As DTO.FijacionDTO) As List(Of DTO.FijacionDTO)
        Dim Ws = New WSCanjeMandatorio.WSFijacion()

        Return Ws.ConsultarTodos(Fijacion)

    End Function

    Public Function ConsultarFiltro(Fijacion As FijacionDTO, FechaNavDesde As Nullable(Of Date), FechaNavHasta As Nullable(Of Date),
                                    FechaTCDesde As Nullable(Of Date), FechaTCHasta As Nullable(Of Date),
                                    FechaPagoDesde As Nullable(Of Date), FechaPagoHasta As Nullable(Of Date)) As List(Of DTO.FijacionDTO)

        Dim Ws = New WSCanjeMandatorio.WSFijacion()

        Return Ws.ConsultarFiltro(Fijacion, FechaNavDesde, FechaNavHasta, FechaTCDesde, FechaTCHasta, FechaPagoDesde, FechaPagoHasta)

    End Function
    Public Function Nemotecnico()
        Dim Ws = New WSCanjeMandatorio.WSFijacion()
        Return Ws.Nemotecnico()
    End Function

    Public Function ConsultarTipoTransacion() As List(Of DTO.FijacionDTO)
        Dim Ws = New WSCanjeMandatorio.WSFijacion()

        Return Ws.ConsultarTipoTransacion()

    End Function

    Public Function ConsultarFijacionNav() As List(Of DTO.FijacionDTO)
        Dim Ws = New WSCanjeMandatorio.WSFijacion()

        Return Ws.ConsultarFijacionNav()

    End Function

    Public Function CargarFiltroRutFondo(Fijacion As DTO.FijacionDTO) As List(Of DTO.FijacionDTO)
        Dim ws = New WSCanjeMandatorio.WSFijacion()
        Return ws.CargarFiltroRutFondo(Fijacion)
    End Function

    Public Function UpdateFijacion(TipoTransaccion As String, IdCanje As String, NavSaliente As Int32, NavEntrante As String)
        Dim Ws = New WSCanjeMandatorio.WSFijacion()

        Return Ws.UpdateFijacion(TipoTransaccion, IdCanje, NavSaliente, NavEntrante)

    End Function
    Public Function UpdateFijacionNav(ID As Integer, TipoTransaccion As String)
        Dim Ws = New WSCanjeMandatorio.WSFijacion()

        Return Ws.UpdateFijacionNav(ID, TipoTransaccion)

    End Function

    Public Function UpdateFijacionTC(ID As Integer, TipoTransaccion As String)
        Dim Ws = New WSCanjeMandatorio.WSFijacion()

        Return Ws.UpdateFijacionTC(ID, TipoTransaccion)

    End Function

    'Public Function GetListaFijacion(Fijacion As DTO.FijacionDTO)
    '    Dim Ws = New WSCanjeMandatorio.WSFijacion()
    '    Return Ws.GetListaFijacion(Fijacion)
    'End Function

    Public Function ConsultarFijacionTC() As List(Of DTO.FijacionDTO)
        Dim Ws = New WSCanjeMandatorio.WSFijacion()

        Return Ws.ConsultarFijacionTC()

    End Function

    Public Function UpdateEstadoConfirmacion(fijacion As FijacionDTO) As Boolean

        Dim Ws = New WSCanjeMandatorio.WSFijacion()
        Return Ws.UpdateEstadoConfirmacion(fijacion)

    End Function
End Class
