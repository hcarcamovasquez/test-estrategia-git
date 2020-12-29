Imports DTO
Imports WSCanjeMandatorio.WSTipoCalculoNAV

Public Class TipoCalculoNav

    Public Function UpdateTipoCalculoNav(TipoCalculoNav As TipoCalculoNavDTO) As Integer
        Dim Ws = New WSCanjeMandatorio.WSTipoCalculoNav()

        If Ws.TipoCambioModificar(TipoCalculoNav) Then
            Return Constantes.CONST_OPERACION_EXITOSA
        Else
            Return Constantes.CONST_ERROR_BBDD
        End If
    End Function

End Class
