Imports DTO
Imports WSCanjeMandatorio.WSGrupoAportantes

Public Class GrupoAportantesDatos

    Public Function GrupoAportanteTraerID(aportante As AportantesXGrupoDTO) As List(Of AportantesXGrupoDTO)
        Dim Ws = New WSCanjeMandatorio.WSGrupoAportantes()
        Return Ws.GrupoAportanteTraerID(aportante)
    End Function

    Public Function GrupoAportanteTraerNombre(aportante As AportantesXGrupoDTO) As List(Of AportantesXGrupoDTO)
        Dim Ws = New WSCanjeMandatorio.WSGrupoAportantes()
        Return Ws.GrupoAportanteTraerNombre(aportante)
    End Function

    Public Function GetListaGrupoAportanteFiltro(grupoAportante As AportantesXGrupoDTO, fechaHasta As Nullable(Of Date)) As List(Of AportantesXGrupoDTO)
        Dim Ws = New WSCanjeMandatorio.WSGrupoAportantes()
        Return Ws.GrupoAportanteFiltro(grupoAportante, fechaHasta)
    End Function

    Public Function GetListaGrupoAportante(grupoAportante As GrupoAportanteDTO) As List(Of GrupoAportanteDTO)
        Dim Ws = New WSCanjeMandatorio.WSGrupoAportantes()
        Return Ws.GrupoAportanteConsultar(grupoAportante)
    End Function

    Public Function GetGrupoAportante(usuario As GrupoAportanteDTO) As GrupoAportanteDTO
        Dim Ws = New WSCanjeMandatorio.WSGrupoAportantes()
        Return Ws.GrupoAportanteTraer(usuario)
    End Function

    Public Function DeleteGrupoAportante(grupo As GrupoAportanteDTO) As Integer
        Dim Ws = New WSCanjeMandatorio.WSGrupoAportantes()

        If Ws.GrupoAportanteEliminar(grupo) > 0 Then
            Return Constantes.CONST_OPERACION_EXITOSA
        Else
            Return Constantes.CONST_ERROR_BBDD
        End If
    End Function

    Public Function InsertGrupoAportante(grupo As GrupoAportanteDTO) As Integer
        Dim Ws = New WSCanjeMandatorio.WSGrupoAportantes()
        Try
            Return Ws.GrupoAportanteIngresar(grupo)
        Catch ex As Exception
            Throw
        End Try

    End Function

    Public Function UpdateGrupoAportante(grupo As GrupoAportanteDTO) As Integer
        Dim Ws = New WSCanjeMandatorio.WSGrupoAportantes()
        Try
            Return Ws.GrupoAportanteModificar(grupo)
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function AportanteXGrupoIngresar(aportanteXGrupo As AportantesXGrupoDTO) As Integer
        Dim Ws = New WSCanjeMandatorio.WSGrupoAportantes()
        Try
            If Ws.AportanteXGrupoIngresar(aportanteXGrupo) Then
                Return Constantes.CONST_OPERACION_EXITOSA
            Else
                Return Constantes.CONST_ERROR_BBDD
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function AportanteExisteEnOtroGrupo(aportanteXGrupo As AportantesXGrupoDTO) As Boolean
        Dim Ws = New WSCanjeMandatorio.WSGrupoAportantes()
        Return (Ws.poseeOtroGrupo(aportanteXGrupo) = 1)

    End Function
End Class
