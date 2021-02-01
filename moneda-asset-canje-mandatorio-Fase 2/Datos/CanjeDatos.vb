Imports DTO
Imports WSCanjeMandatorio.WSCanje

Public Class CanjeDatos


    Public Function CompararDatosEntrantes(series As DTO.FondoSerieDTO) As List(Of FondoSerieDTO)
        Dim ws = New WSCanjeMandatorio.WSCanje()
        Return ws.CompararDatosEntrantes(series)
    End Function
    Public Function CompararDatosSalientes(series As DTO.FondoSerieDTO) As List(Of FondoSerieDTO)
        Dim ws = New WSCanjeMandatorio.WSCanje()
        Return ws.CompararDatosSalientes(series)
    End Function
    Public Function CompararDatosAportantes(aportante As DTO.AportanteDTO) As List(Of AportanteDTO)
        Dim ws = New WSCanjeMandatorio.WSCanje()
        Return ws.CompararDatosAportantes(aportante)
    End Function

    Public Function GetCanje(canje As CanjeDTO) As CanjeDTO
        Dim ws = New WSCanjeMandatorio.WSCanje()
        Return ws.GetCanje(canje)
    End Function

    Public Function GetFijacionId(Id As Int32) As CanjeDTO
        Dim ws = New WSCanjeMandatorio.WSCanje()
        Return ws.GetFijacionId(Id)
    End Function

    Public Function CanjeTodos(canje As DTO.CanjeDTO) As List(Of DTO.CanjeDTO)
        Dim ws = New WSCanjeMandatorio.WSCanje()
        Return ws.CanjeTodos(canje)
    End Function

    Public Function UltimoCanje(canje As DTO.CanjeDTO) As CanjeDTO
        Dim ws = New WSCanjeMandatorio.WSCanje()
        Return ws.ConsultarUltimoCanje(canje)
    End Function

    Public Function CanjeAportante(canje As DTO.CanjeDTO) As List(Of DTO.CanjeDTO)
        Dim ws = New WSCanjeMandatorio.WSCanje()
        Return ws.CanjeNombreAportante(canje)
    End Function

    Public Function ConsultarTransito(canje As DTO.CanjeDTO)
        Dim ws = New WSCanjeMandatorio.WSCanje()
        Return ws.ConsultarTransito(canje)
    End Function

    Public Function EncontrarNemotecnicoSaliente(canje As DTO.CanjeDTO) As List(Of DTO.CanjeDTO)
        Dim ws = New WSCanjeMandatorio.WSCanje()
        Return ws.EncontrarNemotecnicoSalientes(canje)
    End Function

    Public Function EncontrarNemotecnicoEntrante(canje As DTO.CanjeDTO) As List(Of DTO.CanjeDTO)
        Dim ws = New WSCanjeMandatorio.WSCanje()
        Return ws.EncontrarNemotecnicoEntrante(canje)
    End Function

    Public Function CanjeFondo(canje As DTO.CanjeDTO) As List(Of DTO.CanjeDTO)
        Dim ws = New WSCanjeMandatorio.WSCanje()
        Return ws.CanjeNombreFondo(canje)
    End Function

    Public Function CanjeNemotecnico(canje As DTO.CanjeDTO) As List(Of DTO.CanjeDTO)
        Dim ws = New WSCanjeMandatorio.WSCanje()
        Return ws.CanjeNemotecnico(canje)
    End Function

    Public Function CanjeEstado(canje As DTO.CanjeDTO) As List(Of DTO.CanjeDTO)
        Dim ws = New WSCanjeMandatorio.WSCanje()
        Return ws.CanjeEstado(canje)
    End Function

    Public Function CanjeNombreSerie(serie As DTO.FondoSerieDTO) As List(Of DTO.FondoSerieDTO)
        Dim ws = New WSCanjeMandatorio.WSCanje()
        Return ws.CanjeNombreSerie(serie)
    End Function
    Public Function CanjeMonedaSerie(serie As DTO.FondoSerieDTO) As List(Of DTO.FondoSerieDTO)
        Dim ws = New WSCanjeMandatorio.WSCanje()
        Return ws.CanjeMonedaSerie(serie)
    End Function
    Public Function CanjeMultifondo(aportante As DTO.AportanteDTO) As List(Of DTO.AportanteDTO)
        Dim ws = New WSCanjeMandatorio.WSCanje()
        Return ws.CanjeMultifondo(aportante)
    End Function

    Public Function CanjeFiltros(canje As DTO.CanjeDTO, fechaHastaSolicitud As Nullable(Of Date), fechaHastaNav As Nullable(Of Date), fechaHastaCanje As Nullable(Of Date)) As List(Of DTO.CanjeDTO)
        Dim ws = New WSCanjeMandatorio.WSCanje
        Return ws.CanjeFiltros(canje, fechaHastaSolicitud, fechaHastaNav, fechaHastaCanje)
    End Function
    Public Function InsertCanje(canje As CanjeDTO) As CanjeDTO
        Dim Ws = New WSCanjeMandatorio.WSCanje
        Return Ws.CanjeIngresar(canje)
    End Function

    Public Function UpdateCanje(canje As CanjeDTO) As Integer
        Dim Ws = New WSCanjeMandatorio.WSCanje
        If Ws.CanjeModificar(canje) Then
            Return Constantes.CONST_OPERACION_EXITOSA
        Else
            Return Constantes.CONST_ERROR_BBDD
        End If
    End Function

    Public Function DeleteCanje(canje As CanjeDTO) As Integer
        Dim Ws = New WSCanjeMandatorio.WSCanje

        If Ws.CanjeEliminar(canje) Then
            Return Constantes.CONST_OPERACION_EXITOSA
        Else
            Return Constantes.CONST_ERROR_BBDD
        End If
    End Function

    Public Function CanjesTransito(canje As CanjeDTO) As CanjeDTO
        Dim Ws = New WSCanjeMandatorio.WSCanje()
        Return Ws.CanjesTransito(canje)
    End Function

End Class
