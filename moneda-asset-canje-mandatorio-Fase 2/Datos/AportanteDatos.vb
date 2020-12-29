Imports DTO
Imports WSCanjeMandatorio.WSAportante

Public Class AportanteDatos

    Public Function GetListaAportantesPorRut(aportante As AportanteDTO) As List(Of AportanteDTO)
        Dim Ws = New WSCanjeMandatorio.WSAportante()
        Return Ws.ConsultarPorRut(aportante)
    End Function

    Public Function GetListaAportantesPorRazonSocial(aportante As AportanteDTO) As List(Of AportanteDTO)
        Dim listaAportantes As New List(Of AportanteDTO)
        Dim Ws = New WSCanjeMandatorio.WSAportante()

        listaAportantes = Ws.ConsultarPorRazonSocial(aportante)

        Return listaAportantes
    End Function

    Public Function AportantePorMultifondo(aportante As DTO.AportanteDTO) As List(Of DTO.AportanteDTO)
        Dim Ws = New WSCanjeMandatorio.WSAportante()
        Return Ws.AportantePorMultifondo(aportante)
    End Function

    Public Function AportantePorNombre(aportante As DTO.AportanteDTO) As List(Of DTO.AportanteDTO)
        Dim Ws = New WSCanjeMandatorio.WSAportante()
        Return Ws.AportantePorNombre(aportante)
    End Function

    Public Function MultifondoPorRut(aportante As DTO.AportanteDTO) As List(Of DTO.AportanteDTO)
        Dim Ws = New WSCanjeMandatorio.WSAportante()
        Return Ws.MultifondoPorRut(aportante)
    End Function


    Public Function GetListaAportantes(aportante As AportanteDTO, fechaHasta As Nullable(Of Date)) As List(Of AportanteDTO)
        Dim Ws = New WSCanjeMandatorio.WSAportante()
        Return Ws.APConsultar(aportante, fechaHasta)
    End Function

    Public Function GetListaAportantesDistinct(aportante As AportanteDTO) As List(Of AportanteDTO)
        Dim listaAportantes As New List(Of AportanteDTO)
        Dim Ws = New WSCanjeMandatorio.WSAportante()

        listaAportantes = Ws.GetListaAportantesDistinct(aportante)

        Return listaAportantes
    End Function

    Public Function DeleteAportante(Aportante As AportanteDTO) As Integer
        Dim Ws = New WSCanjeMandatorio.WSAportante()

        If Ws.APEliminar(Aportante) Then
            Return Constantes.CONST_OPERACION_EXITOSA
        Else
            Return Constantes.CONST_ERROR_BBDD
        End If
    End Function

    Public Function BuscarAportanteByKey(accion As String, aportante As AportanteDTO) As List(Of AportanteDTO)
        Dim listaAportante As New List(Of AportanteDTO)
        Dim Ws = New WSCanjeMandatorio.WSAportante()

        listaAportante = Ws.BuscarAportante(accion, aportante.Rut, aportante.Multifondo)

        Return listaAportante
    End Function

    Public Function BuscarTodoAportante(accion As String, aportante As AportanteDTO) As List(Of AportanteDTO)
        Dim listaAportantes As New List(Of AportanteDTO)
        Dim Ws = New WSCanjeMandatorio.WSAportante()

        listaAportantes = Ws.BuscarAportante(accion, aportante.Rut)

        Return listaAportantes
    End Function

    Public Function InsertarAportante(aportante As AportanteDTO) As Integer
        Dim Ws = New WSCanjeMandatorio.WSAportante()

        If Ws.APIngresar(aportante) Then
            Return Constantes.CONST_OPERACION_EXITOSA
        Else
            Return Constantes.CONST_ERROR_BBDD
        End If
    End Function

    Public Function UpdateAportante(Aportante As AportanteDTO) As Integer
        Dim Ws = New WSCanjeMandatorio.WSAportante()

        If Ws.APModificar(Aportante) Then
            Return Constantes.CONST_OPERACION_EXITOSA
        Else
            Return Constantes.CONST_ERROR_BBDD
        End If
    End Function

    Public Function BuscarRelacionAportante(aportante As AportanteDTO) As Integer
        Dim Ws = New WSCanjeMandatorio.WSAportante()
        Return Ws.BuscarRelacionAportante(aportante)
    End Function

    Public Function TraerAportantes(aportante As AportanteDTO) As List(Of AportanteDTO)
        Dim Ws = New WSCanjeMandatorio.WSAportante()
        Return Ws.TraerAportantes(aportante)
    End Function

    Public Function TraerMultifondos() As List(Of AportanteDTO)
        Dim Ws = New WSCanjeMandatorio.WSAportante()
        Return Ws.TraerMultifondos()
    End Function

End Class
