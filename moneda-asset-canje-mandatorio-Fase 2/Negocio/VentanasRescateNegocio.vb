
Imports DTO
Imports Datos
Public Class VentanasRescateNegocio
    Dim Datos As VentanasRescateDatos = New VentanasRescateDatos
    Private Excel As ExcelWriter = New ExcelWriter
    Public Property rutaArchivosExcel As String

    Public Const CONST_INSERT_EXITO As Integer = 0
    Public Const CONST_ERROR_EXISTE_RUTYMULTIFONDO As Integer = 1
    Public Const CONST_ERROR_RUT_SIN_MULTIFONDO As Integer = 2
    Public Const CONST_ERROR_NO_INGRESO_BBDD As Integer = -99
    Public Const CONST_ACCION_ALL As String = "SELECT_ALL"
    Public Const CONST_MENSAJE_EXCEL_GUARDADO As String = "Excel Guardado de forma Exitosa"
    Public Const CONST_MENSAJE_EXCEL_ERROR As String = "Error al Guardar Excel"

    Public Function ConsultarNombreFondo(VentanasRescate As VentanasRescateDTO) As List(Of VentanasRescateDTO)
        Return Datos.ConsultarNombreFondo(VentanasRescate)
    End Function

    Public Function ConsultarNemotecnico(VentanasRescate As VentanasRescateDTO) As List(Of VentanasRescateDTO)
        Return Datos.ConsultarNemotecnico(VentanasRescate)
    End Function

    Public Function GuardarVentanasRescate(accion As String,
                                             listaVentanasRescate As List(Of VentanasRescateDTO),
                                             listaVentanasRescateAgregados As List(Of VentanasRescateDTO),
                                             listaVentanasRescateModificados As List(Of VentanasRescateDTO),
                                             listaVentanasRescateEliminados As List(Of VentanasRescateDTO)) As Boolean
        Try


            If accion = "AGREGAR_VENTANASRESCATE" Then
                InsertaVentanasRescate(listaVentanasRescate)
            ElseIf accion = "MODIFICAR_VENTANASRESCATE" Then
                EliminarVentanasRescate(listaVentanasRescateEliminados)
                InsertaVentanasRescate(listaVentanasRescateAgregados)
                InsertaVentanasRescate(listaVentanasRescateModificados)
            ElseIf accion = "ELIMINAR_VENTANASRESCATE" Then
                EliminarVentanasRescate(listaVentanasRescateEliminados)
            End If
            Return True

        Catch ex As Exception
            Return False
        End Try

        Return False
    End Function

    Private Sub InsertaVentanasRescate(listaVentanasRescate As List(Of VentanasRescateDTO))
        Try
            For Each VentanasRescate As VentanasRescateDTO In listaVentanasRescate
                VentanasRescateIngresar(VentanasRescate)
            Next
        Catch ex As Exception
            Throw
        End Try

    End Sub

    Public Function VentanasRescateIngresar(VentanasRescate As VentanasRescateDTO) As Integer
        Try
            Dim datosVentanasRescate = New Datos.VentanasRescateDatos
            Return datosVentanasRescate.VentanasRescateIngresar(VentanasRescate)
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function IngresarTemporalVentanasRescate(VentanasRescate As VentanasRescateDTO) As Integer
        Try
            Dim datosVentanasRescate = New Datos.VentanasRescateDatos
            Return datosVentanasRescate.IngresarTemporalVentanasRescate(VentanasRescate)
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function ModificarEliminarVentanasRescate(VentanasRescate As VentanasRescateDTO) As Integer
        Dim datosVentanasRescate = New Datos.VentanasRescateDatos
        Return datosVentanasRescate.ModificarEliminarVentanasRescate(VentanasRescate)

    End Function

    Public Function ExisteVentanasRescate(VentanasRescate As VentanasRescateDTO) As Boolean
        Dim datosVentanasRescate = New Datos.VentanasRescateDatos()
        Return datosVentanasRescate.ExisteVentanasRescate(VentanasRescate)
    End Function

    Public Function ValidaDiaHabil(fechaValidar As Date) As String
        Dim datosVentanasRescate = New Datos.VentanasRescateDatos()
        Return datosVentanasRescate.ValidaDiaHabil(fechaValidar)
    End Function


    Public Function GetCountVentanaRescate(VentanasRescate As VentanasRescateDTO) As Integer
        Dim datosVentanasRescate = New Datos.VentanasRescateDatos()
        Return datosVentanasRescate.GetCountVentanaRescate(VentanasRescate)
    End Function

    Public Function ConsultarTodos(VentanasRescate As DTO.VentanasRescateDTO) As List(Of DTO.VentanasRescateDTO)
        Dim VentanasRescateDatos As New Datos.VentanasRescateDatos
        Return VentanasRescateDatos.ConsultarTodos(VentanasRescate)
    End Function

    Public Function GetListaVentanasRescateConFiltro(VentanasRescate As VentanasRescateDTO) As List(Of VentanasRescateDTO)
        Dim VentanasRescateDatos As New Datos.VentanasRescateDatos
        Return VentanasRescateDatos.GetListaVentanasRescateConFiltro(VentanasRescate)
    End Function

    Public Function ConsultarPorNombreFondo_Nemotecnico(VentanasRescate As VentanasRescateDTO) As List(Of VentanasRescateDTO)
        Return Datos.ConsultarPorNombreFondo_Nemotecnico(VentanasRescate)
    End Function

    Public Function CompararDatosFondos(fondos As DTO.FondoDTO) As List(Of DTO.FondoDTO)
        Dim ventanaDatos As New Datos.VentanasRescateDatos
        Return ventanaDatos.CompararDatosFondos(fondos)
    End Function

    Public Function CompararDatosSeries(serie As DTO.FondoSerieDTO) As List(Of DTO.FondoSerieDTO)
        Dim ventanaDatos As New Datos.VentanasRescateDatos
        Return ventanaDatos.CompararDatosSeries(serie)
    End Function

    Public Function GetVentanasRescate(VentanasRescate As VentanasRescateDTO) As VentanasRescateDTO
        Dim VentanasRescateRetorno As VentanasRescateDTO
        Dim VentanasRescateDatos As New Datos.VentanasRescateDatos

        VentanasRescateRetorno = VentanasRescateDatos.GetVentanasRescate(VentanasRescate)
        Return VentanasRescateRetorno
    End Function

    Public Function SelectFechasNORescatable(VentanasRescate As VentanasRescateDTO) As VentanasRescateDTO
        Dim VentanasRescateRetorno As VentanasRescateDTO
        Dim VentanasRescateDatos As New Datos.VentanasRescateDatos

        VentanasRescateRetorno = VentanasRescateDatos.SelectFechasNORescatable(VentanasRescate)
        Return VentanasRescateRetorno
    End Function

    Private Sub EliminarVentanasRescate(listaVentanasRescateEliminados As List(Of VentanasRescateDTO))
        Dim datosVentanasRescate As Datos.VentanasRescateDatos = New Datos.VentanasRescateDatos()
        Try
            For Each VentanasRescate As VentanasRescateDTO In listaVentanasRescateEliminados
                datosVentanasRescate.DeleteVentanasRescate(VentanasRescate)
            Next
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Public Function EliminarTodosVentanasRescate(VentanasRescate As VentanasRescateDTO) As Integer
        Dim datosVentanasRescate As Datos.VentanasRescateDatos = New Datos.VentanasRescateDatos()

        If datosVentanasRescate.DeleteVentanasRescateAll(VentanasRescate) = Constantes.CONST_OPERACION_EXITOSA Then
            Return Constantes.CONST_OPERACION_EXITOSA

        Else
            Return Constantes.CONST_ERROR_BBDD
        End If

    End Function

    Public Function ExportarAExcelTodos(VentanasRescate As VentanasRescateDTO) As String
        Dim VentanasRescateDatos As New Datos.VentanasRescateDatos
        Dim ListaVentanasRescate As List(Of VentanasRescateDTO) = VentanasRescateDatos.ConsultarTodos(VentanasRescate)

        If CrearExcel(ListaVentanasRescate) Then

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

    Public Function ExportarAExcel(VentanasRescate As VentanasRescateDTO) As String
        Dim VentanasRescateDatos As New Datos.VentanasRescateDatos
        Dim ListaVentanasRescate As List(Of VentanasRescateDTO) = VentanasRescateDatos.GetListaVentanasRescateConFiltro(VentanasRescate)

        If CrearExcel(ListaVentanasRescate) Then

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


    Public Function CrearExcel(ListaVentanasRescate As List(Of VentanasRescateDTO)) As Boolean
        If Excel.CrearExcelVentanasRescate(ListaVentanasRescate) Then
            Return True
        End If
        Return False
    End Function
End Class
