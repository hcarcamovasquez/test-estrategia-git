Imports DTO
Imports Datos

Public Class ProcesoDetalleNegocio
    Private Excel As ExcelWriter = New ExcelWriter
    Public Property rutaArchivosExcel As String

    Public Const CONST_INSERT_EXITO As Integer = 0
    Public Const CONST_ERROR_EXISTE_RUTYMULTIFONDO As Integer = 1
    Public Const CONST_ERROR_RUT_SIN_MULTIFONDO As Integer = 2
    Public Const CONST_ERROR_NO_INGRESO_BBDD As Integer = -99
    Public Const CONST_ACCION_ALL As String = "SELECT_ALL"
    Public Const CONST_MENSAJE_EXCEL_GUARDADO As String = "Excel Guardado de forma Exitosa"
    Public Const CONST_MENSAJE_EXCEL_ERROR As String = "Error al Guardar Excel"

    'Dim Datos As AportanteDatos = New AportanteDatos
    Dim Datos As ProcesoDetalleDatos = New ProcesoDetalleDatos

    Public Function GuardarProcesoDetalle(listaProcesoDetalle As List(Of ProcesoDetalleDTO)) As Boolean
        If listaProcesoDetalle.Count > 0 Then

            If SelectProcesoDetalle(listaProcesoDetalle(0)).Count > 0 Then
                DeleteProcesoDetalleById(listaProcesoDetalle(0))
            End If

            For Each proceso As ProcesoDetalleDTO In listaProcesoDetalle
                InsertProcesoDetalle(proceso)
            Next
        Else
            Return False
        End If
        Return True
    End Function

    Public Function SelectProcesoDetalle(prcesoDetalle As ProcesoDetalleDTO) As List(Of ProcesoDetalleDTO)
        Return Datos.SelectProcesoDetalle(prcesoDetalle)
    End Function

    Public Function InsertProcesoDetalle(proceso As ProcesoDetalleDTO) As Boolean
        Return Datos.InsertProcesoDetalle(proceso)
    End Function

    Public Function DeleteProcesoDetalleById(proceso As ProcesoDetalleDTO) As Boolean
        Return Datos.DeleteProcesoDetalleById(proceso)
    End Function

    Public Function UpdateProcesoByID(proceso As ProcesoDetalleDTO) As Boolean
        Return Datos.UpdateProcesoDetalle(proceso)
    End Function

End Class