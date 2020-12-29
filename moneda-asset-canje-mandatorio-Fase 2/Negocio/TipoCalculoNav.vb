Imports DTO
Imports Datos.TipoCalculoNav

Public Class TipoCalculoNav
    Private Excel As ExcelWriter = New ExcelWriter
    Public Property rutaArchivosExcel As String

    Private Const CONST_TIPO_CAMBIO_YA_EXISTE As Integer = 1
    Public Const CONST_MENSAJE_EXCEL_GUARDADO As String = "Excel Guardado de forma Exitosa"
    Public Const CONST_MENSAJE_EXCEL_ERROR As String = "Error al Guardar Excel"
    Public Function UpdateTipoCalculoNav(TipoCalculoNav As TipoCalculoNavDTO) As Integer

        Dim TipoCalculoNew As New Datos.TipoCalculoNav

        Return TipoCalculoNew.UpdateTipoCalculoNav(TipoCalculoNav)
    End Function

End Class
