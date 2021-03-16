Imports DTO
Imports WSCanjeMandatorio.WSFondos

Public Class FondosDatos

    Public Function ConsultarPorRut(fondo As DTO.FondoDTO) As List(Of DTO.FondoDTO)
        Return Consultar(fondo, "POR_RUT")
    End Function

    Public Function ConsultarPorRazonSocial(fondo As DTO.FondoDTO) As List(Of DTO.FondoDTO)
        Return Consultar(fondo, "POR_NOMBRE")
    End Function

    Public Function ConsultarPorNombre(fondo As DTO.FondoDTO) As List(Of DTO.FondoDTO)
        Return Consultar(fondo, "POR_NOMBRE_RUT")
    End Function

    Public Function ConsultarPorVentana(fondo As DTO.FondoDTO) As List(Of DTO.FondoDTO)
        Return Consultar(fondo, "POR_VENTANA")
    End Function

    Private Function Consultar(fondo As DTO.FondoDTO, accion As String) As List(Of DTO.FondoDTO)
        Dim listaFondos As New List(Of DTO.FondoDTO)
        Dim Ws = New WSCanjeMandatorio.WSFondos()

        If accion = "POR_RUT" Then
            listaFondos = Ws.ConsultarPorRut(fondo)
        ElseIf accion = "POR_NOMBRE" Then
            listaFondos = Ws.ConsultarPorRazonSocial(fondo)
        ElseIf accion = "POR_NOMBRE_RUT" Then
            listaFondos = Ws.ConsultarPorNombre(fondo)
        ElseIf accion = "POR_VENTANA" Then
            listaFondos = Ws.ConsultarPorVentana(fondo)
        End If
        Return listaFondos
    End Function

    Public Function GetListaFondoConFiltro(fondo As DTO.FondoDTO, fechaHasta As Nullable(Of Date)) As List(Of DTO.FondoDTO)
        Dim Ws = New WSCanjeMandatorio.WSFondos()
        Return Ws.FondoBuscarFiltro(fondo, fechaHasta)
    End Function


    Public Function GetListaFondos(fondo As DTO.FondoDTO) As List(Of DTO.FondoDTO)
        Dim Ws = New WSCanjeMandatorio.WSFondos()
        Return Ws.FNConsultar(fondo)
    End Function
    Public Function RutByNombreFondo(fondo As DTO.FondoDTO) As List(Of DTO.FondoDTO)
        Dim Ws = New WSCanjeMandatorio.WSFondos()
        Return Ws.RutByNombreFondo(fondo)
    End Function

    Public Function GetNombrePorNemotecnico(fondoserie As FondoSerieDTO) As List(Of FondoDTO)
        Dim Ws = New WSCanjeMandatorio.WSFondos()
        Return Ws.GetNombresFondo(fondoserie)
    End Function
    Public Function FondoPorNombre(fondo As DTO.FondoDTO) As List(Of DTO.FondoDTO)
        Dim Ws = New WSCanjeMandatorio.WSFondos()

        Return Ws.FondosPorNombre(fondo)

    End Function

    Public Function GetFondo(fondo As FondoDTO) As FondoDTO
        Dim Ws = New WSCanjeMandatorio.WSFondos()
        Return Ws.GetFondo(fondo)
    End Function

    Public Function ConsultarUnoFondo(fondo As FondoDTO) As List(Of FondoDTO)
        Dim Ws = New WSCanjeMandatorio.WSFondos()
        Return Ws.ConsultarUnFondo(fondo)
    End Function

    Public Function DeleteFondo(fondo As FondoDTO) As Integer
        Dim Ws = New WSCanjeMandatorio.WSFondos()

        If Ws.FNAEliminar(fondo) Then
            Return Constantes.CONST_OPERACION_EXITOSA
        Else
            Return Constantes.CONST_ERROR_BBDD
        End If

    End Function

    Public Function InsertFondo(fondo As FondoDTO) As Integer
        Dim Ws = New WSCanjeMandatorio.WSFondos()

        If Ws.FNAIngresar(fondo) Then
            Return Constantes.CONST_OPERACION_EXITOSA
        Else
            Return Constantes.CONST_ERROR_BBDD
        End If
    End Function

    Public Function UpdateFondo(fondo As FondoDTO) As Integer
        Dim Ws = New WSCanjeMandatorio.WSFondos()

        If Ws.FNModificar(fondo) Then
            Return Constantes.CONST_OPERACION_EXITOSA
        Else
            Return Constantes.CONST_ERROR_BBDD
        End If
    End Function

    Public Function BuscarRelaciones(fondo As FondoDTO) As Integer
        Dim Ws = New WSCanjeMandatorio.WSFondos()

        Return Ws.BuscarRelaciones(fondo)
    End Function

    Public Function ConsultarTodos(fondo As DTO.FondoDTO) As List(Of DTO.FondoDTO)
        Dim Ws = New WSCanjeMandatorio.WSFondos()

        Return Ws.FNConsultar(fondo)
    End Function
    Public Function GetNombreFondo(fondo As DTO.FondoDTO) As List(Of DTO.FondoDTO)
        Dim Ws = New WSCanjeMandatorio.WSFondos()
        Return Ws.GetNombreFondo(fondo)
    End Function
    Public Function GetNombreFondoHitos(fondo As DTO.FondoDTO) As List(Of DTO.FondoDTO)
        Dim Ws = New WSCanjeMandatorio.WSFondos()
        Return Ws.GetNombreFondoHitos(fondo)
    End Function
End Class
