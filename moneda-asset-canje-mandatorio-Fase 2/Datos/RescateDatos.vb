Imports DTO
Imports WSCanjeMandatorio.WSRescates

Public Class RescateDatos

    Public Function ConsultarTodos(Rescate As DTO.RescatesDTO) As List(Of DTO.RescatesDTO)
        Dim Ws = New WSCanjeMandatorio.WSRescates()

        Return Ws.ConsultarTodos(Rescate)

    End Function

    Public Function GetFijacionId(Id As Int32) As RescatesDTO
        Dim ws = New WSCanjeMandatorio.WSRescates()
        Return ws.GetFijacionId(Id)
    End Function

    Public Function RescateEnTransito(Rescate As DTO.RescatesDTO) As List(Of DTO.RescatesDTO)
        Dim Ws = New WSCanjeMandatorio.WSRescates()
        Return Ws.ConsultarTransito(Rescate)
    End Function

    Public Function ConsultarPorFiltro(Rescate As DTO.RescatesDTO, fechaDesdeSolicitud As Nullable(Of Date), fechaHastaSolicitud As Nullable(Of Date), fechaDesdeNAV As Nullable(Of Date), fechaHastaNAV As Nullable(Of Date), fechaDesdePago As Nullable(Of Date), fechaHastaPago As Nullable(Of Date)) As List(Of DTO.RescatesDTO)
        Dim Ws = New WSCanjeMandatorio.WSRescates()
        Return Ws.ConsultarPorFiltro(Rescate, fechaDesdeSolicitud, fechaHastaSolicitud, fechaDesdeNAV, fechaHastaNAV, fechaDesdePago, fechaHastaPago)
    End Function

    Public Function SelectFechaPagoSIRescatable(FechaPagoFondoRescatableINT As Integer, FechaCalculo As DateTime, SoloDiasHabiles As Integer)
        Dim Ws = New WSCanjeMandatorio.WSRescates()
        Return Ws.SelectFechaPagoSIRescatable(FechaPagoFondoRescatableINT, FechaCalculo, SoloDiasHabiles)
    End Function

    Public Function SelectRescatesTransito(Rescate As DTO.RescatesDTO) As RescatesDTO
        Dim Ws = New WSCanjeMandatorio.WSRescates()
        Return Ws.SelectRescatesTransito(Rescate)
    End Function
    Public Function SelectRescatesTransito2(Rescate As DTO.RescatesDTO) As RescatesDTO
        Dim Ws = New WSCanjeMandatorio.WSRescates()
        Return Ws.SelectRescatesTransito2(Rescate)
    End Function
    Public Function InsertRescate(Rescate As RescatesDTO) As Integer
        Dim Ws = New WSCanjeMandatorio.WSRescates

        If Ws.FNAIngresar(Rescate) Then
            Return Constantes.CONST_OPERACION_EXITOSA
        Else
            Return Constantes.CONST_ERROR_BBDD
        End If
    End Function

    Public Function CargarFiltroNombreAportante(Rescate As DTO.RescatesDTO) As List(Of DTO.RescatesDTO)
        Dim ws = New WSCanjeMandatorio.WSRescates()
        Return ws.CargarFiltroNombreAportante(Rescate)
    End Function

    Public Function CargarFiltroNombreFondo(Rescate As DTO.RescatesDTO) As List(Of DTO.RescatesDTO)
        Dim ws = New WSCanjeMandatorio.WSRescates()
        Return ws.CargarFiltroNombreFondo(Rescate)
    End Function

    Public Function CargarFiltroNemotecnico(Rescate As DTO.RescatesDTO) As List(Of DTO.RescatesDTO)
        Dim ws = New WSCanjeMandatorio.WSRescates()
        Return ws.CargarFiltroNemotecnico(Rescate)
    End Function

    Public Function SelectRescatesHoy(Rescate As DTO.RescatesDTO) As RescatesDTO
        Dim Ws = New WSCanjeMandatorio.WSRescates()
        Return Ws.SelectRescatesHoy(Rescate)
    End Function

    Public Function GetRescateOne(Rescate As DTO.RescatesDTO) As RescatesDTO
        Dim Ws = New WSCanjeMandatorio.WSRescates()
        Return Ws.GetRescateOne(Rescate)
    End Function

    Public Function UpdateRescate(Rescate As DTO.RescatesDTO) As Integer
        Dim Ws = New WSCanjeMandatorio.WSRescates()

        If Ws.FNModificar(Rescate) Then
            Return Constantes.CONST_OPERACION_EXITOSA
        Else
            Return Constantes.CONST_ERROR_BBDD
        End If
    End Function

    Public Function RecalculoFijacionNAV(Rescate As DTO.RescatesDTO) As Integer
        Dim Ws = New WSCanjeMandatorio.WSRescates()

        If Ws.RecalculoFijacionNAV(Rescate) Then
            Return Constantes.CONST_OPERACION_EXITOSA
        Else
            Return Constantes.CONST_ERROR_BBDD
        End If
    End Function

    Public Function RecalculoFijacionTC(Rescate As DTO.RescatesDTO) As Integer
        Dim Ws = New WSCanjeMandatorio.WSRescates()

        If Ws.RecalculoFijacionTC(Rescate) Then
            Return Constantes.CONST_OPERACION_EXITOSA
        Else
            Return Constantes.CONST_ERROR_BBDD
        End If
    End Function


    Public Function DeleteRescate(Rescate As DTO.RescatesDTO) As Integer
        Dim Ws = New WSCanjeMandatorio.WSRescates()

        If Ws.FNEliminar(Rescate) Then
            Return Constantes.CONST_OPERACION_EXITOSA
        Else
            Return Constantes.CONST_ERROR_BBDD
        End If
    End Function

    Public Function GetRelaciones(Rescate As DTO.RescatesDTO)
        Dim Ws = New WSCanjeMandatorio.WSRescates()
        Return Ws.GetRelaciones(Rescate)
    End Function
End Class
