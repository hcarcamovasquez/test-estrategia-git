Imports DTO
Imports WSCanjeMandatorio.WSHitos

Public Class HitosDatos
    Public Function GetHito(hito As HitoDTO) As HitoDTO
        Dim Ws = New WSCanjeMandatorio.WSHitos()
        Return Ws.GetHito(hito)
    End Function

    Public Function GetHitoPorNombreFondo(hito As HitoDTO) As HitoDTO
        Dim Ws = New WSCanjeMandatorio.WSHitos()
        Return Ws.GetHitoPorNombre(hito)
    End Function

    Public Function ConsultarTodos(Hito As DTO.HitoDTO) As List(Of DTO.HitoDTO)
        Dim Ws = New WSCanjeMandatorio.WSHitos()
        Return Ws.HTConsultar(Hito)
    End Function

    Public Function GetListaporHitos(Hito As DTO.HitoDTO) As List(Of DTO.HitoDTO)
        Dim ws = New WSCanjeMandatorio.WSHitos()
        Return ws.ConsultarPorHitos(Hito)
    End Function
    Public Function GetListHitoPorNombreRut(fondo As DTO.FondoDTO) As List(Of DTO.FondoDTO)
        Dim ws = New WSCanjeMandatorio.WSHitos()
        Return ws.ConsultarPorNombreRut(fondo)
    End Function

    Public Function CompararDatos(fondo As DTO.FondoDTO) As List(Of FondoDTO)
        Dim ws = New WSCanjeMandatorio.WSHitos()
        Return ws.CompararDatos(fondo)
    End Function
    Public Function GetListHitosPorRut(hito As DTO.HitoDTO) As List(Of DTO.HitoDTO)
        Dim ws = New WSCanjeMandatorio.WSHitos()
        Return ws.HTConsultarRut(hito)
    End Function
    Public Function ConsultaUltimosParaCertificado(hito As DTO.HitoDTO) As List(Of DTO.HitoDTO)
        Dim ws = New WSCanjeMandatorio.WSHitos()
        Return ws.ConsultarUltimoshitos(hito)
    End Function
    Public Function GetListaHitosConFiltro(hito As DTO.HitoDTO, fechaHasta As Nullable(Of Date), fondo As FondoDTO) As List(Of DTO.HitoDTO)
        Dim Ws = New WSCanjeMandatorio.WSHitos
        Return Ws.HTConsultarFiltros(hito, fechaHasta, fondo)
    End Function
    Public Function GetFechasParaCertificados(Hito As HitoDTO) As HitoDTO
        Dim Ws = New WSCanjeMandatorio.WSHitos()
        Return Ws.ConsultarFechasParaCertificados(Hito)
    End Function
    Public Function ConsultarUltimoHito(Hito As HitoDTO) As HitoDTO
        Dim Ws = New WSCanjeMandatorio.WSHitos()
        Return Ws.ConsultarUltimoHito(Hito)
    End Function
    Public Function InsertHito(hito As HitoDTO) As Integer
        Dim Ws = New WSCanjeMandatorio.WSHitos

        If Ws.HitoIngresar(hito) Then
            Return Constantes.CONST_OPERACION_EXITOSA
        Else
            Return Constantes.CONST_ERROR_BBDD
        End If
    End Function
    Public Function UpdateHito(hito As HitoDTO) As Integer
        Dim Ws = New WSCanjeMandatorio.WSHitos

        If Ws.HitoModificar(hito) Then
            Return Constantes.CONST_OPERACION_EXITOSA
        Else
            Return Constantes.CONST_ERROR_BBDD
        End If
    End Function
    Public Function DeleteHito(hito As HitoDTO) As Integer
        Dim Ws = New WSCanjeMandatorio.WSHitos

        If Ws.HitoEliminar(hito) Then
            Return Constantes.CONST_OPERACION_EXITOSA
        Else
            Return Constantes.CONST_ERROR_BBDD
        End If
    End Function

    Public Function BuscarRelaciones(Hito As HitoDTO) As Integer
        Dim Ws = New WSCanjeMandatorio.WSHitos()

        Return Ws.BuscarRelaciones(Hito)
    End Function
End Class
