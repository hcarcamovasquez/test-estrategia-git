Imports DTO
Imports Datos.FondosDatos

Public Class FondosNegocio
    Private Excel As ExcelWriter = New ExcelWriter
    Public Property rutaArchivosExcel As String

    Private Const CONST_FONDO_YA_EXISTE As Integer = 1
    Public Const CONST_MENSAJE_EXCEL_GUARDADO As String = "Excel Guardado de forma Exitosa"
    Public Const CONST_MENSAJE_EXCEL_ERROR As String = "Error al Guardar Excel"

    Public Function ConsultarPorRut(fondo As DTO.FondoDTO) As List(Of DTO.FondoDTO)
        Dim fondoDatos As New Datos.FondosDatos
        Return fondoDatos.ConsultarPorRut(fondo)
    End Function

    Public Function ConsultarPorVentana(fondo As DTO.FondoDTO) As List(Of DTO.FondoDTO)
        Dim fondoDatos As New Datos.FondosDatos
        Return fondoDatos.ConsultarPorVentana(fondo)
    End Function

    Public Function FondosPorNombre(fondo As DTO.FondoDTO) As List(Of DTO.FondoDTO)
        Dim fondoDatos As New Datos.FondosDatos
        Return fondoDatos.FondoPorNombre(fondo)
    End Function

    Public Function ConsultarUnFondo(fondo As DTO.FondoDTO) As List(Of DTO.FondoDTO)
        Dim fondoDatos As New Datos.FondosDatos
        Return fondoDatos.ConsultarUnoFondo(fondo)
    End Function

    Public Function ConsultarPorRazonSocial(fondo As DTO.FondoDTO) As List(Of DTO.FondoDTO)
        Dim fondoDatos As New Datos.FondosDatos
        Return fondoDatos.ConsultarPorRazonSocial(fondo)
    End Function

    Public Function ConsultarPorNombreFondo(fondo As DTO.FondoDTO) As List(Of DTO.FondoDTO)
        Dim fondoDatos As New Datos.FondosDatos
        Return fondoDatos.ConsultarPorNombre(fondo)
    End Function
    Public Function RutByNombreFondo(fondo As DTO.FondoDTO) As List(Of DTO.FondoDTO)
        Dim fondoDatos As New Datos.FondosDatos
        Return fondoDatos.RutByNombreFondo(fondo)
    End Function

    Public Function GetListaFondosConFiltro(fondo As FondoDTO, FechaHasta As Nullable(Of Date)) As List(Of FondoDTO)
        Dim fondoDatos As New Datos.FondosDatos
        Return fondoDatos.GetListaFondoConFiltro(fondo, FechaHasta)
    End Function

    Public Function GetFondo(fondo As FondoDTO) As FondoDTO
        Dim fondoRetorno As FondoDTO
        Dim fondoDatos As New Datos.FondosDatos

        fondoRetorno = fondoDatos.GetFondo(fondo)
        Return fondoRetorno
    End Function

    Public Function DeleteFondo(fondo As FondoDTO) As Integer
        Dim fondoDatos As New Datos.FondosDatos
        Dim relaciones As Integer = fondoDatos.BuscarRelaciones(fondo)

        If relaciones = 0 Then
            Return fondoDatos.DeleteFondo(fondo)
        Else
            Return Constantes.CONST_ERROR_NO_SE_PUEDE_BORRAR
        End If
    End Function

    Public Function UpdateFondo(fondo As FondoDTO) As Integer
        Dim fondoDatos As New Datos.FondosDatos
        Dim relaciones As Integer = fondoDatos.BuscarRelaciones(fondo)

        'If relaciones = 0 Then
        Return fondoDatos.UpdateFondo(fondo)
        'Else
        '    Return Constantes.CONST_ERROR_NO_SE_PUEDE_BORRAR
        'End If
    End Function

    Public Function InsertFondo(fondo As FondoDTO) As Integer
        Dim fondoDatos As New Datos.FondosDatos
        Dim fondoConsultado As FondoDTO = fondoDatos.GetFondo(fondo)

        If fondoConsultado Is Nothing Then
            Return fondoDatos.InsertFondo(fondo)
        Else
            Return CONST_FONDO_YA_EXISTE
        End If
    End Function

    Public Function BuscarRelaciones(fondo As FondoDTO) As Integer
        Dim fondoDatos As New Datos.FondosDatos
        Dim relaciones As Integer = fondoDatos.BuscarRelaciones(fondo)
        If relaciones > 0 Then
            Return 1
        Else
            Return relaciones
        End If

    End Function

    Public Function ExportarAExcel(fondo As FondoDTO, FechaHasta As Nullable(Of Date)) As String
        Dim fondoDatos As New Datos.FondosDatos
        Dim ListaFondo As List(Of FondoDTO) = fondoDatos.GetListaFondoConFiltro(fondo, FechaHasta)

        If CrearExcel(ListaFondo) Then

            If Excel.rutaArchivosExcel Is Nothing Then
                Return CONST_MENSAJE_EXCEL_ERROR
            Else
                Me.rutaArchivosExcel = Excel.rutaArchivosExcel
                Return CONST_MENSAJE_EXCEL_GUARDADO
            End If

        Else
            Return CONST_MENSAJE_EXCEL_ERROR
        End If

    End Function

    Public Function CrearExcel(ListaFondo As List(Of FondoDTO)) As Boolean
        If Excel.CrearExcelFondos(ListaFondo) Then
            Return True
        End If
        Return False
    End Function

    Public Function GetNombrePorNemotecnico(fondoSerie As FondoSerieDTO) As List(Of FondoDTO)
        Dim fondoDatos As New Datos.FondosDatos
        Return fondoDatos.GetNombrePorNemotecnico(fondoSerie)
    End Function
    Public Function ConsultarTodos(fondo As DTO.FondoDTO) As List(Of DTO.FondoDTO)
        Dim fondoDatos As New Datos.FondosDatos
        Return fondoDatos.ConsultarTodos(fondo)
    End Function
    Public Function GetNombreFondo(fondo As DTO.FondoDTO) As List(Of DTO.FondoDTO)
        Dim fondoDatos As New Datos.FondosDatos
        Return fondoDatos.GetNombreFondo(fondo)
    End Function
    Public Function GetNombreFondoHitos(fondo As DTO.FondoDTO) As List(Of DTO.FondoDTO)
        Dim fondoDatos As New Datos.FondosDatos
        Return fondoDatos.GetNombreFondoHitos(fondo)
    End Function
End Class
