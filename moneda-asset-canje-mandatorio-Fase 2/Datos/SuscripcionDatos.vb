Imports DTO
Imports WSCanjeMandatorio.WSTipoCambio

Public Class SuscripcionDatos
    Public Function GetNemotecnicoPorRut(Serie As DTO.SuscripcionDTO) As List(Of DTO.SuscripcionDTO)
        Dim Ws = New WSCanjeMandatorio.WSSuscripcion()
        Return Ws.GetNemotecnicoPorRut(Serie)
    End Function

    Public Function GetFijacionId(Id As Int32) As SuscripcionDTO
        Dim ws = New WSCanjeMandatorio.WSSuscripcion()
        Return ws.GetFijacionId(Id)
    End Function


    Public Function ConsultarTransito(sus As DTO.SuscripcionDTO) As List(Of DTO.SuscripcionDTO)
        Dim Ws = New WSCanjeMandatorio.WSSuscripcion()
        Return Ws.ConsultarTransito(sus)
    End Function
    Public Function GetSuscripcionTransito(Suscripcion As DTO.SuscripcionDTO)
        Dim Ws = New WSCanjeMandatorio.WSSuscripcion()
        Return Ws.GetSuscripcionTransito(Suscripcion)
    End Function
    Public Function GetRelaciones(Suscripcion As DTO.SuscripcionDTO)
        Dim Ws = New WSCanjeMandatorio.WSSuscripcion()
        Return Ws.GetRelaciones(Suscripcion)
    End Function
    Public Function GetSuscripcionTransito2(Suscripcion As DTO.SuscripcionDTO)
        Dim Ws = New WSCanjeMandatorio.WSSuscripcion()
        Return Ws.GetSuscripcionTransito2(Suscripcion)
    End Function
    'Public Function ConsultarActuales(Suscripcion As DTO.SuscripcionDTO)
    '    Dim Ws = New WSCanjeMandatorio.WSSuscripcion()
    '    Return Ws.ConsultarActual(Suscripcion)
    'End Function
    Public Function SelectRescatesTransito(Rescate As DTO.RescatesDTO) As RescatesDTO
        Dim Ws = New WSCanjeMandatorio.WSRescates()
        Return Ws.SelectRescatesTransito2(Rescate)
    End Function
    Public Function GetUltimaSuscripcion(Suscripcion As DTO.SuscripcionDTO)
        Dim Ws = New WSCanjeMandatorio.WSSuscripcion()
        Return Ws.GetUltimaSuscripcion(Suscripcion)
    End Function
    Public Function GetListaSuscripcionConFiltro(Suscripcion As DTO.SuscripcionDTO, FechaIntencionHasta As Nullable(Of Date), FechaNAVHasta As Nullable(Of Date),
    FechaSuscripcionHasta As Nullable(Of Date)) As List(Of DTO.SuscripcionDTO)
        Dim Ws = New WSCanjeMandatorio.WSSuscripcion()
        Return Ws.BuscarFiltro(Suscripcion, FechaIntencionHasta, FechaNAVHasta, FechaSuscripcionHasta)
    End Function
    Public Function GetListaSuscripcion(Suscripcion As DTO.SuscripcionDTO) As List(Of DTO.SuscripcionDTO)
        Dim Ws = New WSCanjeMandatorio.WSSuscripcion()
        Return Ws.GetListaSuscripcion(Suscripcion)
    End Function
    Public Function ConsultarPorCodigo(Suscripcion As DTO.SuscripcionDTO) As List(Of DTO.SuscripcionDTO)
        Dim ListaSuscripcion As New List(Of DTO.SuscripcionDTO)
        Dim Ws = New WSCanjeMandatorio.WSSuscripcion()
        ListaSuscripcion = Ws.ConsultarPorCodigo(Suscripcion)
        Return ListaSuscripcion
    End Function
    Public Function ConsultarPorRazonSocial(Suscripcion As DTO.SuscripcionDTO) As List(Of DTO.SuscripcionDTO)
        Dim ListaSuscripcion As New List(Of DTO.SuscripcionDTO)
        Dim Ws = New WSCanjeMandatorio.WSSuscripcion()
        ListaSuscripcion = Ws.ConsultarPorRazonSocial(Suscripcion)
        Return ListaSuscripcion
    End Function
    Public Function ConsultarPorMultifondo(Suscripcion As DTO.SuscripcionDTO) As List(Of DTO.SuscripcionDTO)
        Dim ListaSuscripcion As New List(Of DTO.SuscripcionDTO)
        Dim Ws = New WSCanjeMandatorio.WSSuscripcion()
        ListaSuscripcion = Ws.ConsultarPorMultifondo(Suscripcion)
        Return ListaSuscripcion
    End Function
    Public Function GetSuscripcion(Suscripcion As DTO.SuscripcionDTO)
        Dim Ws = New WSCanjeMandatorio.WSSuscripcion()
        Return Ws.GetSuscripcion(Suscripcion)
    End Function

    Public Function DeleteSuscripcion(Suscripcion As SuscripcionDTO) As Integer
        Dim Ws = New WSCanjeMandatorio.WSSuscripcion()

        If Ws.SuscripcionEliminar(Suscripcion) Then
            Return Constantes.CONST_OPERACION_EXITOSA
        Else
            Return Constantes.CONST_ERROR_BBDD
        End If
    End Function

    Public Function InsertSuscripcion(Suscripcion As SuscripcionDTO) As Integer
        Dim Ws = New WSCanjeMandatorio.WSSuscripcion()

        If Ws.SuscripcionIngresar(Suscripcion) Then
            Return Constantes.CONST_OPERACION_EXITOSA
        Else
            Return Constantes.CONST_ERROR_BBDD
        End If
    End Function

    Public Function UpdateSuscripcion(Suscripcion As SuscripcionDTO) As Integer
        Dim Ws = New WSCanjeMandatorio.WSSuscripcion()

        If Ws.SuscripcionModificar(Suscripcion) Then
            Return Constantes.CONST_OPERACION_EXITOSA
        Else
            Return Constantes.CONST_ERROR_BBDD
        End If
    End Function

    Public Function ObtenerActual(suscripcion As SuscripcionDTO) As SuscripcionDTO
        Dim Ws = New WSCanjeMandatorio.WSSuscripcion()
        Return Ws.ObtenerActual(suscripcion)
    End Function

    Public Function ObtenerCuotasFondo(suscripcion As SuscripcionDTO) As SuscripcionDTO
        Dim Ws = New WSCanjeMandatorio.WSSuscripcion()
        Return Ws.ObtenerCuotasFondo(suscripcion)
    End Function
    Public Function GetAportanteSuscripcion(Suscripcion As DTO.SuscripcionDTO) As List(Of DTO.SuscripcionDTO)
        Dim Ws = New WSCanjeMandatorio.WSSuscripcion()
        Return Ws.GetAportanteSuscripcion(Suscripcion)
    End Function
    Public Function GetSerieSuscripcion(Suscripcion As DTO.SuscripcionDTO) As List(Of DTO.SuscripcionDTO)
        Dim Ws = New WSCanjeMandatorio.WSSuscripcion()
        Return Ws.GetSerieSuscripcion(Suscripcion)
    End Function
    Public Function GetFondoSuscripcion(Suscripcion As DTO.SuscripcionDTO) As List(Of DTO.SuscripcionDTO)
        Dim Ws = New WSCanjeMandatorio.WSSuscripcion()
        Return Ws.GetFondoSuscripcion(Suscripcion)
    End Function
    Public Function GetAportanteDistinct(Suscripcion As DTO.SuscripcionDTO) As List(Of DTO.SuscripcionDTO)
        Dim Ws = New WSCanjeMandatorio.WSSuscripcion()
        Return Ws.GetAportanteDistinct(Suscripcion)
    End Function
    Public Function RecalculoFijacionNAV(Suscripcion As DTO.SuscripcionDTO) As Integer
        Dim Ws = New WSCanjeMandatorio.WSSuscripcion()

        If Ws.RecalculoFijacionNAV(Suscripcion) Then
            Return Constantes.CONST_OPERACION_EXITOSA
        Else
            Return Constantes.CONST_ERROR_BBDD
        End If
    End Function
    Public Function RecalculoFijacionTC(Suscripcion As DTO.SuscripcionDTO) As Integer
        Dim Ws = New WSCanjeMandatorio.WSSuscripcion()

        If Ws.RecalculoFijacionTC(Suscripcion) Then
            Return Constantes.CONST_OPERACION_EXITOSA
        Else
            Return Constantes.CONST_ERROR_BBDD
        End If
    End Function
End Class
