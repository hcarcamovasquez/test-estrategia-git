Imports DTO
Imports WSCanjeMandatorio.WSTipoCambio

Public Class TipoCambioDatos

    Public Function ConsultarPorCodigo(TipoCambio As DTO.TipoCambioDTO) As List(Of DTO.TipoCambioDTO)
        Return Consultar(TipoCambio, "POR_CODIGO")
    End Function

    Private Function Consultar(TipoCambio As DTO.TipoCambioDTO, accion As String) As List(Of DTO.TipoCambioDTO)
        Dim listaTipoCambio As New List(Of DTO.TipoCambioDTO)
        Dim Ws = New WSCanjeMandatorio.WSTipoCambio()

        If accion = "POR_CODIGO" Then
            listaTipoCambio = Ws.ConsultarPorMoneda(TipoCambio)
        End If
        Return listaTipoCambio
    End Function

    Public Function GrupoTipoCambioPorCodigo(TipoCambio As DTO.TipoCambioDTO) As List(Of DTO.TipoCambioDTO)
        Dim ws = New WSCanjeMandatorio.WSTipoCambio()
        Return ws.ConsultarPorCodigo(TipoCambio)
    End Function
    Public Function GetRelaciones(TipoCambio As DTO.TipoCambioDTO)
        Dim Ws = New WSCanjeMandatorio.WSTipoCambio()
        Return Ws.GetRelaciones(TipoCambio)
    End Function
    Public Function GetListaTCConFiltro(TipoCambio As DTO.TipoCambioDTO, fechaHasta As Nullable(Of Date)) As List(Of DTO.TipoCambioDTO)
        Dim Ws = New WSCanjeMandatorio.WSTipoCambio()
        Return Ws.TCBuscarFiltro(TipoCambio, fechaHasta)
    End Function
    'para buscar según las fechas y el código
    Public Function GetListaTCConFiltroCodigo(TipoCambio As DTO.TipoCambioDTO, fechaHasta As Nullable(Of Date)) As List(Of DTO.TipoCambioDTO)
        Dim Ws = New WSCanjeMandatorio.WSTipoCambio()
        Return Ws.TCBuscarFiltroCodigo(TipoCambio, fechaHasta)
    End Function
    Public Function GetListaTipoCambio(TipoCambio As DTO.TipoCambioDTO) As List(Of DTO.TipoCambioDTO)
        Dim Ws = New WSCanjeMandatorio.WSTipoCambio()

        Return Ws.GetListaTipoCambio(TipoCambio)

    End Function

    Public Function GetTipoCambio(TipoCambio As TipoCambioDTO) As TipoCambioDTO
        Dim Ws = New WSCanjeMandatorio.WSTipoCambio()
        Return Ws.GetTipoCambio(TipoCambio)
    End Function
    Public Function GetTipoCambioPorFecha(TipoCambio As TipoCambioDTO) As TipoCambioDTO
        Dim Ws = New WSCanjeMandatorio.WSTipoCambio()
        Return Ws.GetTipoCambioPorFecha(TipoCambio)
    End Function

    Public Function DeleteTipoCambio(TipoCambio As TipoCambioDTO) As Integer
        Dim Ws = New WSCanjeMandatorio.WSTipoCambio()

        If Ws.TipoCambioEliminar(TipoCambio) Then
            Return Constantes.CONST_OPERACION_EXITOSA
        Else
            Return Constantes.CONST_ERROR_BBDD
        End If

    End Function

    Public Function InsertTipoCambio(TipoCambio As TipoCambioDTO) As Integer
        Dim Ws = New WSCanjeMandatorio.WSTipoCambio()

        If Ws.TipoCambioIngresar(TipoCambio) Then
            Return Constantes.CONST_OPERACION_EXITOSA
        Else
            Return Constantes.CONST_ERROR_BBDD
        End If
    End Function

    Public Function UpdateTipoCambio(TipoCambio As TipoCambioDTO) As Integer
        Dim Ws = New WSCanjeMandatorio.WSTipoCambio()

        If Ws.TipoCambioModificar(TipoCambio) Then
            Return Constantes.CONST_OPERACION_EXITOSA
        Else
            Return Constantes.CONST_ERROR_BBDD
        End If
    End Function

    Public Function ConsultarTipoCambioPorCodigoYFecha(tipo As TipoCambioDTO) As List(Of TipoCambioDTO)
        Dim Ws = New WSCanjeMandatorio.WSTipoCambio()
        Return Ws.ConsultarTipoCambioPorCodigoYFecha(tipo)
    End Function

    Public Function UltimoTipoCambio(tipo As TipoCambioDTO) As List(Of TipoCambioDTO)
        Dim Ws = New WSCanjeMandatorio.WSTipoCambio()
        Return Ws.UltimoTipoCambioPorCodigo(tipo)
    End Function

End Class
