Imports DTO
Imports Datos

Public Class AportanteNegocio
    Private Excel As ExcelWriter = New ExcelWriter
    Public Property rutaArchivosExcel As String

    Public Const CONST_INSERT_EXITO As Integer = 0
    Public Const CONST_ERROR_EXISTE_RUTYMULTIFONDO As Integer = 1
    Public Const CONST_ERROR_RUT_SIN_MULTIFONDO As Integer = 2
    Public Const CONST_ERROR_NO_INGRESO_BBDD As Integer = -99
    Public Const CONST_ACCION_ALL As String = "SELECT_ALL"
    Public Const CONST_MENSAJE_EXCEL_GUARDADO As String = "Excel Guardado de forma Exitosa"
    Public Const CONST_MENSAJE_EXCEL_ERROR As String = "Error al Guardar Excel"

    Dim Datos As AportanteDatos = New AportanteDatos

    Public Function GetListaAportantesPorRut(Aportante As AportanteDTO) As List(Of AportanteDTO)
        Return Datos.GetListaAportantesPorRut(Aportante)
    End Function

    Public Function GetListaAportantesPorRazonSocial(Aportante As AportanteDTO) As List(Of AportanteDTO)
        Return Datos.GetListaAportantesPorRazonSocial(Aportante)
    End Function

    Public Function GetListaAportantes(Aportante As AportanteDTO, FechaHasta As Nullable(Of Date)) As List(Of AportanteDTO)
        Return Datos.GetListaAportantes(Aportante, FechaHasta)
    End Function

    Public Function GetListaAportantesDistinct(Aportante As AportanteDTO) As List(Of AportanteDTO)
        Return Datos.GetListaAportantesDistinct(Aportante)
    End Function

    Public Function InsertarAportante(aportante As AportanteDTO) As Integer
        Dim existe As Integer = BuscarTodoAportante(aportante)

        If existe = Constantes.CONST_OPERACION_EXITOSA Then
            If Datos.InsertarAportante(aportante) = Constantes.CONST_OPERACION_EXITOSA Then
                Return Constantes.CONST_OPERACION_EXITOSA
            Else
                Return Constantes.CONST_ERROR_BBDD
            End If
        Else
            Return existe
        End If
    End Function

    Public Function DeleteAportante(Aportante As AportanteDTO) As Integer
        Dim RelacionExite As Boolean = BuscarRelacionAportante(Aportante)
        Dim Result As Integer
        If RelacionExite Then
            Return 1
        Else
            Result = Datos.DeleteAportante(Aportante)
        End If

        If Result = Constantes.CONST_OPERACION_EXITOSA Then
            Return Constantes.CONST_OPERACION_EXITOSA
        Else
            Return Constantes.CONST_ERROR_BBDD
        End If
    End Function

    Public Function UpdateAportante(Aportante As AportanteDTO) As Integer
        Dim RelacionExite As Boolean = BuscarRelacionAportante(Aportante)
        Dim Result As Integer
        If RelacionExite Then
            Return 1
        Else
            Result = Datos.UpdateAportante(Aportante)
        End If

        If Result = Constantes.CONST_OPERACION_EXITOSA Then
            Return Constantes.CONST_OPERACION_EXITOSA
        Else
            Return Constantes.CONST_ERROR_BBDD
        End If
        Return (Result >= 0)
    End Function

    Public Function BuscarTodoAportante(aportante As AportanteDTO) As Integer
        Dim listaAportantes As New List(Of AportanteDTO)

        listaAportantes = Datos.BuscarTodoAportante(CONST_ACCION_ALL, aportante)

        If listaAportantes Is Nothing Or listaAportantes.Count = 0 Then
            Return 0 'No se encontro registro en la base de datos        
        ElseIf aportante.Multifondo Is Nothing Then
            Return 1
        Else
            For Each ap As AportanteDTO In listaAportantes
                If ap.Multifondo.Replace("&nbsp;", "").Trim() = aportante.Multifondo Then
                    Return 2
                End If
            Next
        End If
        Return 0
    End Function

    Public Function BuscarAportante(aportante As AportanteDTO) As AportanteDTO
        Dim listaAportantes As New List(Of AportanteDTO)
        Dim unAportante As New AportanteDTO
        Dim accion As String = "SELECT_ONE"

        listaAportantes = Datos.BuscarAportanteByKey(accion, aportante)

        For Each ap As AportanteDTO In listaAportantes
            unAportante = ap
        Next

        Return unAportante
    End Function

    Public Function BuscarRelacionAportante(aportante As AportanteDTO) As Boolean
        Return (Datos.BuscarRelacionAportante(aportante) > 0)
    End Function

    Public Function AportantePorMultifondo(aportante As DTO.AportanteDTO) As List(Of DTO.AportanteDTO)
        Dim aportanteDatos As New Datos.AportanteDatos
        Return aportanteDatos.AportantePorMultifondo(aportante)
    End Function

    Public Function AportantePorNombre(aportante As DTO.AportanteDTO) As List(Of DTO.AportanteDTO)
        Dim aportanteDatos As New Datos.AportanteDatos
        Return aportanteDatos.AportantePorNombre(aportante)
    End Function

    Public Function MultifondoPorRut(aportante As DTO.AportanteDTO) As List(Of DTO.AportanteDTO)
        Dim aportanteDatos As New Datos.AportanteDatos
        Return aportanteDatos.MultifondoPorRut(aportante)
    End Function

    Public Function TraerAportantes(aportante As AportanteDTO) As List(Of AportanteDTO)
        Return Datos.TraerAportantes(aportante)
    End Function

    Public Function TraerMultifondos() As List(Of AportanteDTO)
        Return Datos.TraerMultifondos()
    End Function

    Public Function ExportarAExcel(aportante As AportanteDTO, FechaHasta As Nullable(Of Date)) As String
        Dim ListaAportante As List(Of AportanteDTO) = Datos.GetListaAportantes(aportante, FechaHasta)
        If CrearExcel(ListaAportante) Then

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

    Public Function CrearExcel(listaAportante As List(Of AportanteDTO)) As Boolean
        If Excel.CrearExcelAportantes(listaAportante) Then
            Return True
        End If
        Return False
    End Function

End Class
