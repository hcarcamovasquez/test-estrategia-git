Imports DTO
Imports Datos

Public Class GrupoAportanteNegocio
    Private Excel As ExcelWriter = New ExcelWriter
    Public Property rutaArchivosExcel As String
    Public Const CONST_MENSAJE_EXCEL_GUARDADO As String = "Excel Guardado de forma Exitosa"
    Public Const CONST_MENSAJE_EXCEL_ERROR As String = "Error al Guardar Excel"

    Dim Datos As AportanteDatos = New AportanteDatos

    Public Function GrupoAportanteTraerID(grupoAportante As AportantesXGrupoDTO) As List(Of AportantesXGrupoDTO)
        Dim datosGrupoAportante = New Datos.GrupoAportantesDatos()
        Return datosGrupoAportante.GrupoAportanteTraerID(grupoAportante)
    End Function

    Public Function GrupoAportanteTraerNombre(grupoAportante As AportantesXGrupoDTO) As List(Of AportantesXGrupoDTO)
        Dim datosGrupoAportante = New Datos.GrupoAportantesDatos()
        Return datosGrupoAportante.GrupoAportanteTraerNombre(grupoAportante)
    End Function

    Public Function GetListaGrupoAportanteFiltro(grupoAportante As AportantesXGrupoDTO, fechaHasta As Nullable(Of Date)) As List(Of AportantesXGrupoDTO)
        Dim datosGrupoAportante = New Datos.GrupoAportantesDatos()
        Return datosGrupoAportante.GetListaGrupoAportanteFiltro(grupoAportante, fechaHasta)
    End Function

    Public Function GetListaGrupoAportante(grupoAportante As GrupoAportanteDTO) As List(Of GrupoAportanteDTO)
        Dim datosGrupoAportante = New Datos.GrupoAportantesDatos()
        Return datosGrupoAportante.GetListaGrupoAportante(grupoAportante)
    End Function

    Public Function GetGrupoAportante(usuario As GrupoAportanteDTO) As GrupoAportanteDTO
        Dim datosGrupoAportante = New Datos.GrupoAportantesDatos()
        Return datosGrupoAportante.GetGrupoAportante(usuario)
    End Function

    Public Function DeleteGrupoAportante(grupo As GrupoAportanteDTO) As Integer
        Dim datosGrupoAportante = New Datos.GrupoAportantesDatos()
        Return datosGrupoAportante.DeleteGrupoAportante(grupo)

    End Function

    Public Function InsertGrupoAportante(grupo As GrupoAportanteDTO) As Integer
        Try
            Dim datosGrupoAportante = New Datos.GrupoAportantesDatos()
            Return datosGrupoAportante.InsertGrupoAportante(grupo)
        Catch ex As Exception
            Throw
        End Try

    End Function

    Public Function GuardarGrupoDeAporntates(accion As String,
                                             grupoAportante As GrupoAportanteDTO,
                                             listaAportanteXGrupo As List(Of AportantesXGrupoDTO),
                                             listaGrupoXAporntatesEliminados As List(Of AportantesXGrupoDTO)) As Boolean
        Try
            Dim idGrupo As Integer
            Dim datosGrupoAportante = New Datos.GrupoAportantesDatos()

            If accion = "AGREGAR_GRUPO" Then
                idGrupo = InsertGrupoAportante(grupoAportante)
                InsertaAportantesAGrupo(listaAportanteXGrupo, idGrupo)
            ElseIf accion = "MODIFICAR_GRUPO" Then
                UpdateGrupoAportante(grupoAportante)
                idGrupo = grupoAportante.GPA_Id
                EliminarAportantesDeGrupo(listaGrupoXAporntatesEliminados)
                EliminarAportantesDeGrupo(listaAportanteXGrupo)

                InsertaAportantesAGrupo(listaAportanteXGrupo, idGrupo)
            ElseIf accion = "ELIMINAR_APORTANTE_DE_GRUPO" Then
                idGrupo = grupoAportante.GPA_Id
                EliminarAportantesDeGrupo(listaGrupoXAporntatesEliminados)
            End If
            Return True

        Catch ex As Exception
            Return False
        End Try

        Return False
    End Function

    Private Sub InsertaAportantesAGrupo(listaAportanteXGrupo As List(Of AportantesXGrupoDTO), idGrupo As Integer)
        Try
            For Each AportanteXgrupo As AportantesXGrupoDTO In listaAportanteXGrupo
                AportanteXgrupo.IdGrupo = idGrupo
                AportanteXGrupoIngresar(AportanteXgrupo)
            Next
        Catch ex As Exception
            Throw
        End Try

    End Sub

    Private Sub EliminarAportantesDeGrupo(listaGrupoXAporntatesEliminados As List(Of AportantesXGrupoDTO))
        Dim datosApGp As Datos.ApGpDatos = New Datos.ApGpDatos()
        Try
            For Each aportanteXgrupo As AportantesXGrupoDTO In listaGrupoXAporntatesEliminados
                datosApGp.DeleteAportanteEnGrupo(aportanteXgrupo)
            Next
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Public Function EliminarGrupoDeAportantes(grupoAportante As GrupoAportanteDTO) As Integer
        Dim datosApGp As Datos.ApGpDatos = New Datos.ApGpDatos()

        If datosApGp.DeleteGrupoAll(grupoAportante) = Constantes.CONST_OPERACION_EXITOSA Then
            If DeleteGrupoAportante(grupoAportante) = Constantes.CONST_OPERACION_EXITOSA Then
                Return Constantes.CONST_OPERACION_EXITOSA
            Else
                Return Constantes.CONST_ERROR_BBDD
            End If
        Else
            Return Constantes.CONST_ERROR_BBDD
        End If

    End Function

    Public Function UpdateGrupoAportante(grupo As GrupoAportanteDTO) As Integer
        Dim datosGrupoAportante = New Datos.GrupoAportantesDatos()
        Try
            Return datosGrupoAportante.UpdateGrupoAportante(grupo)
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function ExportarAExcel(grupoAportante As AportantesXGrupoDTO, FechaHasta As Nullable(Of Date)) As String
        Dim datosGrupoAportante = New Datos.GrupoAportantesDatos()
        Dim ListaGrupoAportante As List(Of AportantesXGrupoDTO) = datosGrupoAportante.GetListaGrupoAportanteFiltro(grupoAportante, FechaHasta)

        If CrearExcel(ListaGrupoAportante) Then

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

    Public Function CrearExcel(listaGrupoAportante As List(Of AportantesXGrupoDTO)) As Boolean
        If Excel.CrearExcelGrupoAportantes(listaGrupoAportante) Then
            Return True
        End If
        Return False
    End Function

    Public Function AportanteXGrupoIngresar(aportanteXGrupo As AportantesXGrupoDTO) As Integer
        Try
            Dim datosGrupoAportante = New Datos.GrupoAportantesDatos()
            Return datosGrupoAportante.AportanteXGrupoIngresar(aportanteXGrupo)
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function AportanteExisteEnOtroGrupo(aportanteXGrupo As AportantesXGrupoDTO) As Boolean
        Dim datosGrupoAportante = New Datos.GrupoAportantesDatos()
        Return datosGrupoAportante.AportanteExisteEnOtroGrupo(aportanteXGrupo)
    End Function

End Class
