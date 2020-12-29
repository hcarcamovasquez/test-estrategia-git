Imports DTO
Imports WSCanjeMandatorio.WSValoresCuota

Public Class ValoresCuotaDatos
    Public Function InsertvaloresCuota(valoresCuota As VcSerieDTO) As Integer
        Dim Ws = New WSCanjeMandatorio.WSValoresCuota

        If Ws.FNAIngresar(valoresCuota) Then
            Return Constantes.CONST_OPERACION_EXITOSA
        Else
            Return Constantes.CONST_ERROR_BBDD
        End If
    End Function

    Public Function GetListaValoresCuotaConFiltro(ValoresCuota As DTO.VcSerieDTO, fechaHasta As Nullable(Of Date)) As List(Of DTO.VcSerieDTO)
        Dim Ws = New WSCanjeMandatorio.WSValoresCuota()
        Return Ws.ValoresCuotaBuscarFiltro(ValoresCuota, fechaHasta)
    End Function

    Public Function ConsultarTodos(valoresCuota As DTO.VcSerieDTO) As List(Of DTO.VcSerieDTO)
        Dim Ws = New WSCanjeMandatorio.WSValoresCuota()

        Return Ws.FNConsultar(valoresCuota)

    End Function

    Public Function GetValoresCuota(valoresCuota As VcSerieDTO) As VcSerieDTO
        Dim Ws = New WSCanjeMandatorio.WSValoresCuota()
        Return Ws.GetValoresCuota(valoresCuota)
    End Function

    Public Function ValoresCuotaPorNemotecnicoYFecha(valoresCuota As VcSerieDTO) As List(Of VcSerieDTO)
        Dim Ws = New WSCanjeMandatorio.WSValoresCuota()
        Return Ws.ValoresCuotaPorNemotecnico(valoresCuota)
    End Function

    Public Function UltimoValorCuota(valoresCuota As VcSerieDTO) As List(Of VcSerieDTO)
        Dim Ws = New WSCanjeMandatorio.WSValoresCuota()
        Return Ws.UltimoValorCuota(valoresCuota)
    End Function

    Public Function UpdateValoresCuota(ValoresCuota As VcSerieDTO) As Integer
        Dim Ws = New WSCanjeMandatorio.WSValoresCuota()

        If Ws.FNModificar(ValoresCuota) Then
            Return Constantes.CONST_OPERACION_EXITOSA
        Else
            Return Constantes.CONST_ERROR_BBDD
        End If
    End Function

    Public Function DeleteValoresCuota(ValoresCuota As VcSerieDTO) As Integer
        Dim Ws = New WSCanjeMandatorio.WSValoresCuota()

        If Ws.FNAEliminar(ValoresCuota) Then
            Return Constantes.CONST_OPERACION_EXITOSA
        Else
            Return Constantes.CONST_ERROR_BBDD
        End If

    End Function

    Public Function BuscarRelaciones(ValoresCuota As VcSerieDTO) As Integer
        Dim Ws = New WSCanjeMandatorio.WSValoresCuota()

        Return Ws.BuscarRelaciones(ValoresCuota)
    End Function

    Public Function CargarFiltroNemotecnico(ValoresCuota As VcSerieDTO) As List(Of DTO.VcSerieDTO)
        Dim ws = New WSCanjeMandatorio.WSValoresCuota()
        Return ws.CargarFiltroNemotecnico(ValoresCuota)
    End Function
End Class
