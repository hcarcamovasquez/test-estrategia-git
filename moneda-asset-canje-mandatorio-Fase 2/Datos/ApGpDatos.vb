Imports DTO
Imports WSCanjeMandatorio.WSApGp

Public Class ApGpDatos

    Public Function DeleteAportanteEnGrupo(aportanteXGrupo As AportantesXGrupoDTO) As Integer
        Dim Ws = New WSCanjeMandatorio.WSApGp()
        Try
            Ws.DeleteAportanteEnGrupo(aportanteXGrupo)
            Return 0
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function DeleteGrupoAll(grupo As GrupoAportanteDTO) As Integer
        Dim Ws = New WSCanjeMandatorio.WSApGp()

        If Ws.DeleteGrupoAll(grupo) > 0 Then
            Return Constantes.CONST_OPERACION_EXITOSA
        Else
            Return Constantes.CONST_ERROR_BBDD
        End If

    End Function

End Class
