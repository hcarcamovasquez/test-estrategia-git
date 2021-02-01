Imports DTO
Imports Datos.CanjeDatos

Public Class CanjeNegocio

    Private Excel As ExcelWriter = New ExcelWriter
    Public Property rutaArchivoExcel As String

    Public Const CONST_INSERT_EXITO As Integer = 0
    Public Const CONST_ERROR_NO_INGRESO_BBDD As Integer = -99
    Public Const CONS_ACCION_ALL As String = "SELECT_ALL"
    Public Const CONST_MENSAJE_EXCEL_GUARDADO As String = "Excel Guardado de forma Exitosa"
    Public Const CONST_MENSAJE_EXCEL_ERROR As String = "Error al Guardar Excel"

    Public Function GetCanje(canje As CanjeDTO) As CanjeDTO
        Dim canjeRetorno As CanjeDTO
        Dim canjes As New Datos.CanjeDatos

        canjeRetorno = canjes.GetCanje(canje)
        Return canjeRetorno
    End Function

    Public Function UltimoCanje(canje As CanjeDTO) As CanjeDTO
        Dim CanjeRetorno As CanjeDTO
        Dim CanjeDatos As New Datos.CanjeDatos
        CanjeRetorno = CanjeDatos.UltimoCanje(canje)
        Return CanjeRetorno
    End Function

    Public Function CompararDatosEntrantes(fondoSerie As DTO.FondoSerieDTO) As List(Of DTO.FondoSerieDTO)
        Dim canjesDatos As New Datos.CanjeDatos
        Return canjesDatos.CompararDatosEntrantes(fondoSerie)
    End Function

    Public Function CompararDatosSalientes(fondoSerie As DTO.FondoSerieDTO) As List(Of DTO.FondoSerieDTO)
        Dim canjesDatos As New Datos.CanjeDatos
        Return canjesDatos.CompararDatosSalientes(fondoSerie)
    End Function

    Public Function CompararDatosAportantes(aportante As DTO.AportanteDTO) As List(Of DTO.AportanteDTO)
        Dim canjesDatos As New Datos.CanjeDatos
        Return canjesDatos.CompararDatosAportantes(aportante)
    End Function

    Public Function ConsultarTodos(canje As DTO.CanjeDTO) As List(Of DTO.CanjeDTO)
        Dim canjeDatos As New Datos.CanjeDatos
        Return canjeDatos.CanjeTodos(canje)
    End Function

    Public Function ConsultarTransito(canje As DTO.CanjeDTO)
        Dim canjeDatos As New Datos.CanjeDatos
        Return canjeDatos.ConsultarTransito(canje)
    End Function

    Public Function EncontrarNemotecnicoSaliente(canje As DTO.CanjeDTO) As List(Of DTO.CanjeDTO)
        Dim canjeDatos As New Datos.CanjeDatos
        Return canjeDatos.EncontrarNemotecnicoSaliente(canje)
    End Function

    Public Function EncontrarNemotecnicoEntrante(canje As DTO.CanjeDTO) As List(Of DTO.CanjeDTO)
        Dim canjeDatos As New Datos.CanjeDatos
        Return canjeDatos.EncontrarNemotecnicoEntrante(canje)
    End Function

    Public Function ConsultarAportante(canje As DTO.CanjeDTO) As List(Of DTO.CanjeDTO)
        Dim canjeDatos As New Datos.CanjeDatos
        Return canjeDatos.CanjeAportante(canje)
    End Function
    Public Function ConsultarFondo(canje As DTO.CanjeDTO) As List(Of DTO.CanjeDTO)
        Dim canjeDatos As New Datos.CanjeDatos
        Return canjeDatos.CanjeFondo(canje)
    End Function
    Public Function ConsultarNemotecnico(canje As DTO.CanjeDTO) As List(Of DTO.CanjeDTO)
        Dim canjeDatos As New Datos.CanjeDatos
        Return canjeDatos.CanjeNemotecnico(canje)
    End Function
    Public Function ConsultarNombreSerie(serie As DTO.FondoSerieDTO) As List(Of DTO.FondoSerieDTO)
        Dim canjeDatos As New Datos.CanjeDatos
        Return canjeDatos.CanjeNombreSerie(serie)
    End Function
    Public Function ConsultarMonedaSerie(serie As DTO.FondoSerieDTO) As List(Of DTO.FondoSerieDTO)
        Dim canjeDatos As New Datos.CanjeDatos
        Return canjeDatos.CanjeMonedaSerie(serie)
    End Function
    Public Function ConsultarMultifondo(aportante As DTO.AportanteDTO) As List(Of DTO.AportanteDTO)
        Dim canjeDatos As New Datos.CanjeDatos
        Return canjeDatos.CanjeMultifondo(aportante)
    End Function
    Public Function ConsultarEstado(canje As DTO.CanjeDTO) As List(Of DTO.CanjeDTO)
        Dim canjeDatos As New Datos.CanjeDatos
        Return canjeDatos.CanjeEstado(canje)
    End Function
    Public Function ConsultarFiltros(canje As DTO.CanjeDTO, fechaSolicitudHasta As Nullable(Of Date), fechaHastaNav As Nullable(Of Date), fechaHastaCanje As Nullable(Of Date)) As List(Of DTO.CanjeDTO)
        Dim canjeDatos As New Datos.CanjeDatos
        Return canjeDatos.CanjeFiltros(canje, fechaSolicitudHasta, fechaHastaNav, fechaHastaCanje)
    End Function

    Public Function GetFijacionId(Id As Int32) As CanjeDTO
        Dim CanjeRetorno As CanjeDTO
        Dim datosCanje As New Datos.CanjeDatos

        CanjeRetorno = datosCanje.GetFijacionId(Id)
        Return CanjeRetorno
    End Function

    Public Function InsertarCanje(canje As CanjeDTO) As CanjeDTO
        Dim canjeDatos As New Datos.CanjeDatos
        Return canjeDatos.InsertCanje(canje)
    End Function

    Public Function UpdateCanje(canje As CanjeDTO) As Integer
        Dim canjeDatos As New Datos.CanjeDatos
        Return canjeDatos.UpdateCanje(canje)
    End Function
    Public Function DeleteCanje(canje As CanjeDTO) As Integer
        Dim canjeDatos As New Datos.CanjeDatos
        Return canjeDatos.DeleteCanje(canje)
    End Function

    Public Function ExportarExcel(Canje As CanjeDTO, fechaHastaSolicitud As Nullable(Of Date), fechaHastaNav As Nullable(Of Date), fechaHastaCanje As Nullable(Of Date)) As String
        Dim canjeDatos As New Datos.CanjeDatos
        Dim listaCanje As List(Of CanjeDTO) = canjeDatos.CanjeFiltros(Canje, fechaHastaSolicitud, fechaHastaNav, fechaHastaCanje)

        If CrearExcel(listaCanje) Then
            If Excel.rutaArchivosExcel Is Nothing Then
                Return CONST_MENSAJE_EXCEL_ERROR
            Else
                Me.rutaArchivoExcel = Excel.rutaArchivosExcel
                Return CONST_MENSAJE_EXCEL_GUARDADO
            End If
        Else
            Return CONST_MENSAJE_EXCEL_ERROR
        End If
    End Function

    Public Function CrearExcel(listaCanje As List(Of CanjeDTO)) As Boolean
        If Excel.CrearExcelCanje(listaCanje) Then
            Return True
        End If
        Return False
    End Function

    Public Function CanjesTransito(Canje As CanjeDTO) As CanjeDTO
        Dim CanjeRetorno As CanjeDTO
        Dim datosCanje As New Datos.CanjeDatos

        CanjeRetorno = datosCanje.CanjesTransito(Canje)
        Return CanjeRetorno
    End Function
End Class
