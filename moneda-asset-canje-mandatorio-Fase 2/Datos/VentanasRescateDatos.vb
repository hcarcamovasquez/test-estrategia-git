Imports DTO
Imports WSCanjeMandatorio.WSVentanasRescate

Public Class VentanasRescateDatos

    Public Function ConsultarNombreFondo(VentanasRescate As VentanasRescateDTO) As List(Of VentanasRescateDTO)
        Dim listaFondos As New List(Of VentanasRescateDTO)
        Dim Ws = New WSCanjeMandatorio.WSVentanasRescate()

        listaFondos = Ws.ConsultarNombreFondo(VentanasRescate)

        Return listaFondos
    End Function

    Public Function ConsultarNemotecnico(VentanasRescate As VentanasRescateDTO) As List(Of VentanasRescateDTO)
        Dim listaFondos As New List(Of VentanasRescateDTO)
        Dim Ws = New WSCanjeMandatorio.WSVentanasRescate()

        listaFondos = Ws.ConsultarNemotecnico(VentanasRescate)

        Return listaFondos
    End Function

    Public Function VentanasRescateIngresar(VentanasRescate As VentanasRescateDTO) As Integer
        Dim Ws = New WSCanjeMandatorio.WSVentanasRescate()
        Try
            If Ws.VentanasRescateIngresar(VentanasRescate) Then
                Return Constantes.CONST_OPERACION_EXITOSA
            Else
                Return Constantes.CONST_ERROR_BBDD
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function IngresarTemporalVentanasRescate(VentanasRescate As VentanasRescateDTO) As Integer
        Dim Ws = New WSCanjeMandatorio.WSVentanasRescate()
        Try
            If Ws.IngresarTemporalVentanasRescate(VentanasRescate) Then
                Return Constantes.CONST_OPERACION_EXITOSA
            Else
                Return Constantes.CONST_ERROR_BBDD
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function ModificarEliminarVentanasRescate(VentanasRescate As VentanasRescateDTO) As Integer
        Dim Ws = New WSCanjeMandatorio.WSVentanasRescate

        If Ws.ModificarEliminarVentanasRescate(VentanasRescate) Then
            Return Constantes.CONST_OPERACION_EXITOSA
        Else
            Return Constantes.CONST_ERROR_BBDD
        End If
    End Function


    Public Function ExisteVentanasRescate(VentanasRescate As VentanasRescateDTO) As Boolean
        Dim Ws = New WSCanjeMandatorio.WSVentanasRescate()
        Return (Ws.ExisteVentanasRescate(VentanasRescate) = 1)

    End Function

    Public Function ValidaDiaHabil(fechaValidar As Date) As String
        Dim Ws = New WSCanjeMandatorio.WSVentanasRescate()
        Return Ws.ValidaDiaHabil(fechaValidar)

    End Function

    Public Function GetCountVentanaRescate(VentanasRescate As VentanasRescateDTO) As Integer
        Dim Ws = New WSCanjeMandatorio.WSVentanasRescate()
        Return Ws.GetCountVentanaRescate(VentanasRescate)

    End Function


    Public Function ConsultarTodos(VentanasRescate As DTO.VentanasRescateDTO) As List(Of DTO.VentanasRescateDTO)
        Dim Ws = New WSCanjeMandatorio.WSVentanasRescate()

        Return Ws.FNConsultar(VentanasRescate)

    End Function

    Public Function GetListaVentanasRescateConFiltro(VentanasRescate As DTO.VentanasRescateDTO) As List(Of DTO.VentanasRescateDTO)
        Dim Ws = New WSCanjeMandatorio.WSVentanasRescate()
        Return Ws.VentanasRescateBuscarFiltro(VentanasRescate)
    End Function

    Public Function ConsultarPorNombreFondo_Nemotecnico(VentanasRescate As VentanasRescateDTO) As List(Of VentanasRescateDTO)
        Dim listaFondos As New List(Of VentanasRescateDTO)
        Dim Ws = New WSCanjeMandatorio.WSVentanasRescate()

        listaFondos = Ws.ConsultarPorNombreFondo_Nemotecnico(VentanasRescate)

        Return listaFondos
    End Function

    Public Function CompararDatosFondos(fondo As DTO.FondoDTO) As List(Of FondoDTO)
        Dim ws = New WSCanjeMandatorio.WSVentanasRescate()
        Return ws.CompararDatosFondos(fondo)
    End Function

    Public Function CompararDatosSeries(series As DTO.FondoSerieDTO) As List(Of FondoSerieDTO)
        Dim ws = New WSCanjeMandatorio.WSVentanasRescate()
        Return ws.CompararDatosSeries(series)
    End Function

    Public Function GetVentanasRescate(VentanasRescate As DTO.VentanasRescateDTO) As VentanasRescateDTO
        Dim Ws = New WSCanjeMandatorio.WSVentanasRescate()
        Return Ws.GetVentanasRescate(VentanasRescate)
    End Function

    Public Function SelectFechasNORescatable(VentanasRescate As DTO.VentanasRescateDTO) As VentanasRescateDTO
        Dim Ws = New WSCanjeMandatorio.WSVentanasRescate()
        Return Ws.SelectFechasNORescatable(VentanasRescate)
    End Function

    Public Function DeleteVentanasRescate(VentanasRescate As VentanasRescateDTO) As Integer
        Dim Ws = New WSCanjeMandatorio.WSVentanasRescate()
        Try
            Ws.DeleteVentanasRescate(VentanasRescate)
            Return 0
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function DeleteVentanasRescateAll(VentanasRescate As VentanasRescateDTO) As Integer
        Dim Ws = New WSCanjeMandatorio.WSVentanasRescate()
        Dim filasAfectas As Integer = Ws.DeleteVentanasRescateAll(VentanasRescate)
        If filasAfectas > 0 Then
            Return Constantes.CONST_OPERACION_EXITOSA
        Else
            Return Constantes.CONST_ERROR_BBDD
        End If

    End Function

    Public Function TraerMonedaDelFondo(ventanaRescate As VentanasRescateDTO) As String
        Dim Ws = New WSCanjeMandatorio.WSVentanasRescate()
        Return Ws.TraerMonedaDelFondo(ventanaRescate)
    End Function
End Class
