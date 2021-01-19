Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel
Imports DTO
Imports System.Web.Script.Services
' Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente.
' <System.Web.Script.Services.ScriptService()> _
<System.Web.Services.WebService(Namespace:="http://tempuri.org/")> _
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<ToolboxItem(False)> _
Public Class WSSeries
    Inherits System.Web.Services.WebService

    Private Const CONST_DELETE_ONE As String = "SELECT_ONE"
    Private Const CONST_CONSULTA_FILTRO As String = "PRC_FondoSerieCRUD"

    Private Const CONST_INSERT As String = "INSERT"
    Private Const CONST_UPDATE As String = "UPDATE"
    Private Const CONST_DELETE As String = "DELETE"
    Private Const CONST_SELECT_ALL As String = "SELECT_ALL"
    Private Const CONST_SELECT_ONE As String = "SELECT_ONE"
    Private Const CONST_SELECT_ONE_NEMOTECNICO As String = "SELECT_ONE_NEMOTECNICO"
    Private Const CONST_SELECT_BY_RUT_AGRUPACION As String = "SELECT_BY_RUT_AGRUPACION"
    Private Const CONST_SELECT_BY_MONEDA As String = "NEMOTECNICO_POR_MONEDA_Y_FONDO"


    Private Const SP_CONSULTAS As String = "PRC_FondoSerieConsultas"
    Private Const CONST_CONSULTA_POR_FILTRO As String = "CONSULTA_POR_FILTRO"
    Private Const CONST_SELECT_RELACIONES As String = "SELECT_RELACIONES"

    Private Const SP_CRUD As String = "PRC_FondoSerieCRUD"
    Private Const CONST_SELECT_FONDOS As String = "GET_FONDOS"
    Private Const CONST_FILTRAR_NEMOTECNICO_POR_FONDO As String = "FILTRAR_NEMOTECNICO_POR_FONDO"
    Private Const CONST_FILTRAR_GRUPO_COMPATIBLE_POR_FONDO As String = "FILTRAR_GRUPO_COMPATIBLE_POR_FONDO"

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function BuscarRelaciones(fondoSerie As FondoSerieDTO) As Integer
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CONSULTAS)
        Dim ds As DataSet
        Try
            ' ---TODO Data prueba---
            sp.AgregarParametro("Accion", CONST_SELECT_RELACIONES, System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("Nemotecnico", fondoSerie.Nemotecnico, System.Data.SqlDbType.VarChar)

            ds = sp.ReturnDataSet()

            If ds.Tables.Count > 0 AndAlso ds.Tables(0).Rows.Count > 0 Then
                For Each dataRow As DataRow In ds.Tables(0).Rows
                    Return dataRow(0)
                Next
            End If

            Return 99

        Catch ex As Exception
            Return 99
        End Try
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function GetFondo(fondoSerie As FondoSerieDTO) As FondoSerieDTO
        Dim fondoRetorno As FondoSerieDTO = New FondoSerieDTO
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)
        Dim ds As DataSet

        Try
            sp.AgregarParametro("Accion", CONST_SELECT_ONE, System.Data.SqlDbType.VarChar)

            FillParameters(fondoSerie, sp)

            ds = sp.ReturnDataSet()
            If ds.Tables.Count > 0 AndAlso ds.Tables(0).Rows.Count > 0 Then
                fondoRetorno = fillSerie(ds.Tables(0).Rows(0))
            Else
                fondoRetorno = Nothing
            End If

        Catch ex As Exception
            Throw
        End Try

        Return fondoRetorno
    End Function
    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function Nemotecnico() As List(Of FondoSerieDTO)
        Dim ListaSerie As List(Of FondoSerieDTO) = New List(Of FondoSerieDTO)
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)
        Dim ds As DataSet

        Try
            sp.AgregarParametro("Accion", "Nemotecnico", System.Data.SqlDbType.VarChar)
            ds = sp.ReturnDataSet()


            For Each dataRow As DataRow In ds.Tables(0).Rows
                Dim Sus = New FondoSerieDTO
                With Sus
                    .Nemotecnico = dataRow("Nemotecnico").ToString().Trim()
                End With
                ListaSerie.Add(Sus)
            Next

        Catch ex As Exception
            Throw
        End Try

        Return ListaSerie
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function GetFondoNemotecncio(fondoSerie As FondoSerieDTO) As FondoSerieDTO
        Dim fondoRetorno As FondoSerieDTO = New FondoSerieDTO
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)
        Dim ds As DataSet

        Try
            sp.AgregarParametro("Accion", CONST_SELECT_ONE_NEMOTECNICO, System.Data.SqlDbType.VarChar)

            FillParameters(fondoSerie, sp)

            ds = sp.ReturnDataSet()
            If ds.Tables.Count > 0 AndAlso ds.Tables(0).Rows.Count > 0 Then
                fondoRetorno = fillSerie(ds.Tables(0).Rows(0))
            Else
                fondoRetorno = Nothing
            End If

        Catch ex As Exception
            Throw
        End Try

        Return fondoRetorno
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function GrupoSeriesPorRut(series As FondoSerieDTO) As List(Of FondoSerieDTO)
        Dim Lista As List(Of FondoSerieDTO) = New List(Of FondoSerieDTO)
        'Dim sp As New DBSqlServer.SqlServer.StoredProcedure(CONST_CONSULTA_FILTRO)
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)
        Dim ds As DataSet

        Try

            sp.AgregarParametro("accion", "SELECT_BY_RUT", System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("FnRut", series.Rut, System.Data.SqlDbType.VarChar)

            ds = sp.ReturnDataSet()

            For Each dataRow As DataRow In ds.Tables(0).Rows
                Lista.Add(fillGrupoSeries(dataRow))
            Next

        Catch ex As Exception
            Throw
        End Try

        Return Lista

    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function CompararDatos(fondo As FondoDTO) As List(Of FondoDTO)
        Dim Lista As List(Of FondoDTO) = New List(Of FondoDTO)
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CONSULTAS)
        Dim ds As DataSet
        Dim fondoNew As New FondoDTO

        Try
            sp.AgregarParametro("Accion", "COMPARAR_DATOS", System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("RutFondo", fondo.Rut, System.Data.SqlDbType.VarChar)
            ds = sp.ReturnDataSet()

            For Each dataRow As DataRow In ds.Tables(0).Rows
                fondoNew = New FondoDTO
                With fondoNew
                    .Rut = dataRow("FN_RUT").ToString().Trim()
                    .RazonSocial = dataRow("FN_Razon_Social").ToString().Trim()
                    .NombreCorto = dataRow("FN_Nombre_Corto").ToString.Trim()
                    .Estado = dataRow("FN_Estado").ToString().Trim()
                    .FechaIngreso = dataRow("FN_Fecha_Ingreso").ToString().Trim()
                    .UsuarioIngreso = dataRow("FN_Usuario_Ingreso")
                    .FechaModificacion = dataRow("FN_Fecha_Modificacion")
                    .UsuarioModificacion = dataRow("FN_Usuario_Modificacion")
                    .CuotasEmitidas = dataRow("FN_Cuotas_Emitidas")
                    .FechaEmision = dataRow("FN_Fecha_Emision")
                    .FechaVencimiento = dataRow("FN_Fecha_Vencimiento")
                    .Acumulado = dataRow("FN_Acumulado")
                End With
                Lista.Add(fondoNew)
            Next
        Catch ex As Exception
            Throw
        End Try

        Return Lista
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function filtrarGrupoCompatiblePorFondo(fondoSerie As FondoSerieDTO) As List(Of FondoSerieDTO)
        Dim Lista As List(Of FondoSerieDTO) = New List(Of FondoSerieDTO)
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CONSULTAS)
        Dim ds As DataSet
        Dim Serie As FondoSerieDTO
        Try
            sp.AgregarParametro("Accion", CONST_FILTRAR_GRUPO_COMPATIBLE_POR_FONDO, System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("RutFondo", fondoSerie.Rut, System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("Nemotecnico", fondoSerie.Nemotecnico, System.Data.SqlDbType.VarChar)

            ds = sp.ReturnDataSet()
            If ds.Tables.Count > 0 AndAlso ds.Tables(0).Rows.Count > 0 Then
                For Each dataRow As DataRow In ds.Tables(0).Rows
                    Serie = New FondoSerieDTO With {
                        .Compatible = dataRow("FS_Nivel_Agrupacion").ToString().Trim()
                    }
                    Lista.Add(Serie)
                Next
            End If

        Catch ex As Exception
            Throw
        End Try
        Return Lista
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function filtrarNemotecnicoPorFondo(fondoSerie As FondoSerieDTO) As List(Of FondoSerieDTO)
        Dim Lista As List(Of FondoSerieDTO) = New List(Of FondoSerieDTO)
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CONSULTAS)
        Dim ds As DataSet
        Try
            sp.AgregarParametro("Accion", CONST_FILTRAR_NEMOTECNICO_POR_FONDO, System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("RutFondo", fondoSerie.Rut, System.Data.SqlDbType.VarChar)

            ds = sp.ReturnDataSet()

            For Each dataRow As DataRow In ds.Tables(0).Rows
                Lista.Add(fillGrupoSeries(dataRow))
            Next
        Catch ex As Exception
            Throw
        End Try
        Return Lista
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function GrupoSeriesPorNemotecnico(series As FondoSerieDTO) As List(Of FondoSerieDTO)
        Dim Lista As List(Of FondoSerieDTO) = New List(Of FondoSerieDTO)
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CONSULTAS)
        Dim ds As DataSet

        Try
            sp.AgregarParametro("Accion", "SELECT_BY_NEMOTECNICO", System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("Nemotecnico", series.Nemotecnico, System.Data.SqlDbType.VarChar)

            ds = sp.ReturnDataSet()

            For Each dataRow As DataRow In ds.Tables(0).Rows
                Lista.Add(fillGrupoSeries(dataRow))
            Next

        Catch ex As Exception
            Throw
        End Try

        Return Lista

    End Function

    Private Function fillGrupoSeries(dataRow As DataRow) As FondoSerieDTO
        Dim grupoFondoSeries As New FondoSerieDTO

        With grupoFondoSeries
            .Rut = dataRow("FN_RUT").ToString().Trim()
            .Nemotecnico = dataRow("FS_Nemotecnico").ToString().Trim()
            .Nombrecorto = dataRow("FS_Nombre_Corto").ToString().Trim()
            .Remuneracion = dataRow("FS_Remuneracion").ToString().Trim()
            .Nacionalidad = dataRow("FS_Nac_Ext").ToString().Trim()
            .Calificado = dataRow("FS_Calificado").ToString().Trim()
            .Moneda = dataRow("FS_Moneda").ToString().Trim()
            .LimiteMoneda = dataRow("FS_Limite_Moneda").ToString().Trim()
            .LimiteMinimo = dataRow("FS_Limite_Min").ToString().Trim()
            .LimiteMaximo = dataRow("FS_Limite_Max").ToString().Trim()
            .ExclusivoMAM = dataRow("FS_Exc_Mon")
            .Compatible = dataRow("FS_Default")
            .Canje = dataRow("FS_Canje_Mandatorio")
            .Nivel = dataRow("FS_Nivel_Agrupacion").ToString().Trim()
            .Estado = dataRow("FS_Estado").ToString().Trim()
            .FechaIngreso = dataRow("FS_Fecha_Ingreso").ToString().Trim()
            .UsuarioIngreso = dataRow("FS_Usuario_Ingreso").ToString().Trim()
            .FechaModificacion = dataRow("FS_Fecha_Modificacion").ToString().Trim()
            .UsuarioModificacion = dataRow("FS_Usuario_Modificacion").ToString().Trim()
            .HorarioRecaste = dataRow("FS_Horario_Recastes").ToString.Trim
            .FondoRescatable = dataRow("FS_Fondo_Rescatable").ToString().Trim()
            .FechaNav = dataRow("FS_Fecha_Nav").ToString().Trim()
            .FechaRescate = dataRow("FS_Fecha_Rescate").ToString().Trim()
            .FechaTCObservado = dataRow("FS_Fecha_TC_Observado").ToString().Trim()
            .Patrimonio = dataRow("FS_Patrimonio").ToString().Trim()
            .FijacionNav = dataRow("FS_Fijacion_Nav").ToString().Trim()
            .FechaNavSuscripcion = dataRow("FS_Fecha_Nav_Suscripcion").ToString().Trim()
            .FechaSuscripcion = dataRow("FS_Fecha_Suscripcion").ToString().Trim()
            .FechaTCSuscripcion = dataRow("FS_Fecha_TC_Suscripcion").ToString().Trim()
            .FijacionSuscripcion = dataRow("FS_Fijacion_Nav_Suscripcion").ToString().Trim()
            .FechaNavCanje = dataRow("FS_Fecha_Nav_Canje").ToString().Trim()
            .FechaTCCanje = dataRow("FS_Fecha_TC_Canje").ToString().Trim()
            .FijacionCanje = dataRow("FS_Fijacion_Nav_Canje").ToString().Trim()


            .DiasHabilesCanje = dataRow("FS_DiasHabilesCanje").ToString().Trim()
            .DiasHabilesRescate = dataRow("FS_DiasHabilesRescate").ToString().Trim()
            .DiasHabilesSuscripciones = dataRow("FS_DiasHabilesSuscripciones").ToString().Trim()

            .FechaCanjeCanje = dataRow("FS_Fecha_Canje_Canje").ToString().Trim()
            .DiasHabilesFechaCanje = dataRow("FS_DiasHabilesFechaCanje").ToString().Trim()

        End With
        Return grupoFondoSeries
    End Function

    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function ConsultarPorRut(fondoSerie As FondoSerieDTO) As List(Of FondoSerieDTO)
        Dim ListaSeries As List(Of FondoSerieDTO) = New List(Of FondoSerieDTO)
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CONSULTAS)
        Dim ds As DataSet
        Dim fondoSerieNew As New FondoSerieDTO

        Try
            sp.AgregarParametro("Accion", "CONSULTA_POR_RUT", System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("RutFondo", fondoSerie.Rut, System.Data.SqlDbType.VarChar)

            ds = sp.ReturnDataSet()

            For Each dataRow As DataRow In ds.Tables(0).Rows
                fondoSerieNew = New FondoSerieDTO
                With fondoSerieNew
                    .Rut = dataRow("FN_RUT").ToString().Trim()
                End With

                ListaSeries.Add(fondoSerieNew)
            Next
        Catch ex As Exception
            Throw
        End Try
        Return ListaSeries
    End Function

    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function GetFondos(fondoSerie As FondoSerieDTO) As List(Of FondoDTO)
        Dim ListaFondo As List(Of FondoDTO) = New List(Of FondoDTO)
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CONSULTAS)
        Dim ds As DataSet
        Dim fondoNew As New FondoDTO

        Try
            sp.AgregarParametro("Accion", CONST_SELECT_FONDOS, System.Data.SqlDbType.VarChar)

            ds = sp.ReturnDataSet()

            For Each dataRow As DataRow In ds.Tables(0).Rows
                fondoNew = New FondoDTO
                With fondoNew
                    .Rut = dataRow("FN_RUT").ToString().Trim()
                    .RazonSocial = dataRow("FN_Razon_Social").ToString().Trim()
                End With

                ListaFondo.Add(fondoNew)
            Next
        Catch ex As Exception
            Throw
        End Try
        Return ListaFondo
    End Function

    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function ConsultarPorRutHitos(fondoSerie As FondoSerieDTO) As List(Of FondoSerieDTO)
        Dim ListaSeries As List(Of FondoSerieDTO) = New List(Of FondoSerieDTO)
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CONSULTAS)
        Dim ds As DataSet
        Dim fondoSerieNew As New FondoSerieDTO

        Try
            sp.AgregarParametro("Accion", "CONSULTA_POR_RUT_HITOS", System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("RutFondo", fondoSerie.Rut, System.Data.SqlDbType.VarChar)

            ds = sp.ReturnDataSet()

            For Each dataRow As DataRow In ds.Tables(0).Rows
                fondoSerieNew = New FondoSerieDTO
                With fondoSerieNew
                    .Rut = dataRow("FN_RUT").ToString().Trim()
                End With

                ListaSeries.Add(fondoSerieNew)
            Next
        Catch ex As Exception
            Throw
        End Try
        Return ListaSeries
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function ConsultarPorNombreRut(fondo As FondoDTO, fondoSerie As FondoSerieDTO) As List(Of FondoDTO)
        Dim Lista As List(Of FondoDTO) = New List(Of FondoDTO)
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CONSULTAS)
        Dim ds As DataSet
        Dim fondoNew As New FondoDTO

        Try
            sp.AgregarParametro("Accion", "CONSULTA_POR_NOMBRE", System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("NombreFondo", fondo.NombreCorto, System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("Nemotecnico", fondoSerie.Nemotecnico, System.Data.SqlDbType.VarChar)

            ds = sp.ReturnDataSet()

            For Each dataRow As DataRow In ds.Tables(0).Rows
                fondoNew = New FondoDTO
                With fondoNew
                    .NombreCorto = dataRow("FN_Nombre_corto").ToString().Trim()
                End With
                Lista.Add(fondoNew)
            Next
        Catch ex As Exception
            Throw
        End Try
        Return Lista
    End Function

    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function llenarFiltroRutNombreSerie() As List(Of FondoSerieDTO)
        Dim ListaSeries As List(Of FondoSerieDTO) = New List(Of FondoSerieDTO)
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CONSULTAS)
        Dim ds As DataSet
        Dim fondoSerieNew As New FondoSerieDTO

        Try
            sp.AgregarParametro("Accion", "CONSULTA_POR_RUT_NOMBRE", System.Data.SqlDbType.VarChar)

            ds = sp.ReturnDataSet()

            For Each dataRow As DataRow In ds.Tables(0).Rows
                fondoSerieNew = New FondoSerieDTO
                With fondoSerieNew
                    .Rut = dataRow("FN_RUT").ToString().Trim()
                    .Nombrecorto = dataRow("FN_Nombre_Corto").ToString().Trim()
                End With

                ListaSeries.Add(fondoSerieNew)
            Next
        Catch ex As Exception
            Throw
        End Try
        Return ListaSeries
    End Function
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function llenarFiltroRutNombreSerieConsulta() As List(Of FondoSerieDTO)
        Dim ListaSeries As List(Of FondoSerieDTO) = New List(Of FondoSerieDTO)
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CONSULTAS)
        Dim ds As DataSet
        Dim fondoSerieNew As New FondoSerieDTO

        Try
            sp.AgregarParametro("Accion", "CONSULTA_POR_RUT_NOMBRE", System.Data.SqlDbType.VarChar)

            ds = sp.ReturnDataSet()

            For Each dataRow As DataRow In ds.Tables(0).Rows
                fondoSerieNew = New FondoSerieDTO
                With fondoSerieNew
                    .Rut = dataRow("FN_RUT").ToString().Trim()
                    .Nombrecorto = dataRow("FN_Nombre_Corto").ToString().Trim()
                End With

                ListaSeries.Add(fondoSerieNew)
            Next
        Catch ex As Exception
            Throw
        End Try
        Return ListaSeries
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function ConsultarPorNemotecnico(fondoSerie As FondoSerieDTO) As List(Of FondoSerieDTO)
        Dim ListaSerie As List(Of FondoSerieDTO) = New List(Of FondoSerieDTO)
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CONSULTAS)
        Dim ds As DataSet
        Dim fondoSerieNew As New FondoSerieDTO

        Try
            sp.AgregarParametro("Accion", "CONSULTA_POR_NEMOTECNICO", System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("Nemotecnico", fondoSerie.Nemotecnico, System.Data.SqlDbType.VarChar)

            ds = sp.ReturnDataSet()

            For Each dataRow As DataRow In ds.Tables(0).Rows
                fondoSerieNew = New FondoSerieDTO
                With fondoSerieNew
                    .Nemotecnico = dataRow("FS_Nemotecnico").ToString().Trim()
                    .Rut = dataRow("FN_RUT").ToString().Trim()
                End With

                ListaSerie.Add(fondoSerieNew)
            Next

        Catch ex As Exception
            Throw
        End Try
        Return ListaSerie
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function ConsultarPorNemotecnicoSeries(fondoSerie As FondoSerieDTO) As List(Of FondoSerieDTO)
        Dim ListaSerie As List(Of FondoSerieDTO) = New List(Of FondoSerieDTO)
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CONSULTAS)
        Dim ds As DataSet
        Dim fondoSerieNew As New FondoSerieDTO

        Try
            sp.AgregarParametro("Accion", "CONSULTA_POR_NEMOTECNICO_SERIES", System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("Nemotecnico", fondoSerie.Nemotecnico, System.Data.SqlDbType.VarChar)

            ds = sp.ReturnDataSet()

            For Each dataRow As DataRow In ds.Tables(0).Rows
                fondoSerieNew = New FondoSerieDTO
                With fondoSerieNew
                    .Nemotecnico = dataRow("FS_Nemotecnico").ToString().Trim()
                End With

                ListaSerie.Add(fondoSerieNew)
            Next

        Catch ex As Exception
            Throw
        End Try
        Return ListaSerie
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function ConsultarPorGrupoCompatible(fondoSerie As FondoSerieDTO) As List(Of FondoSerieDTO)
        Dim ListaSerie As List(Of FondoSerieDTO) = New List(Of FondoSerieDTO)
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CONSULTAS)
        Dim ds As DataSet
        Dim fondoSerieNew As New FondoSerieDTO

        Try
            sp.AgregarParametro("Accion", "CONSULTA_POR_GRUPO_COMPATIBLE", System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("Nivel", fondoSerie.Nivel, System.Data.SqlDbType.VarChar)

            ds = sp.ReturnDataSet()

            For Each dataRow As DataRow In ds.Tables(0).Rows
                fondoSerieNew = New FondoSerieDTO
                With fondoSerieNew
                    .Nivel = dataRow("FS_Nivel_Agrupacion").ToString().Trim()
                End With

                ListaSerie.Add(fondoSerieNew)
            Next

        Catch ex As Exception
            Throw
        End Try
        Return ListaSerie
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function FSConsultar(fondoSerie As FondoSerieDTO, fondo As FondoDTO) As List(Of FondoSerieDTO)
        Dim ListaSerie As List(Of FondoSerieDTO) = New List(Of FondoSerieDTO)
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CONSULTAS)
        Dim ds As DataSet

        Try
            sp.AgregarParametro("Accion", "CONSULTA_POR_FILTRO", System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("RutFondo", fondoSerie.Rut, System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("NombreFondo", fondo.NombreCorto, System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("Nemotecnico", fondoSerie.Nemotecnico, System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("Nivel", fondoSerie.Nivel, System.Data.SqlDbType.Int)

            ds = sp.ReturnDataSet()

            For Each dataRow As DataRow In ds.Tables(0).Rows
                ListaSerie.Add(fillSerie(dataRow))
            Next
        Catch ex As Exception
            Throw
        End Try

        Return ListaSerie
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function FSConsultarSerie(fondoSerie As FondoSerieDTO, fondo As FondoDTO) As List(Of FondoSerieDTO)
        Dim ListaSerie As List(Of FondoSerieDTO) = New List(Of FondoSerieDTO)
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CONSULTAS)
        Dim ds As DataSet

        Dim Nemotecnico As String

        If fondoSerie.Nemotecnico = "" Then
            Nemotecnico = Nothing
        Else
            Nemotecnico = fondoSerie.Nemotecnico
        End If

        If fondoSerie.Nombrecorto = Nothing Then
            fondoSerie.Nombrecorto = ""
        End If

        If fondoSerie.Rut = Nothing Then
            fondoSerie.Rut = ""
        End If

        Try
            sp.AgregarParametro("Accion", "CONSULTA_POR_FILTRO_SERIES", System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("RutFondo", fondoSerie.Rut, System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("NombreFondo", fondoSerie.Nombrecorto, System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("Nemotecnico", Nemotecnico, System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("Nivel", fondoSerie.Nivel, System.Data.SqlDbType.Int)

            ds = sp.ReturnDataSet()

            For Each dataRow As DataRow In ds.Tables(0).Rows
                ListaSerie.Add(fillSerie(dataRow))
            Next
        Catch ex As Exception
            Throw
        End Try

        Return ListaSerie
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function GetbyNombreFondo(fondoSerie As FondoSerieDTO, fondo As FondoDTO) As List(Of FondoSerieDTO)
        Dim ListaSerie As List(Of FondoSerieDTO) = New List(Of FondoSerieDTO)
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CONSULTAS)
        Dim ds As DataSet

        Try
            sp.AgregarParametro("Accion", "SELECT_BY_NOMBRE_FONDO", System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("RutFondo", fondoSerie.Rut, System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("NombreFondo", fondo.RazonSocial, System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("Nemotecnico", fondoSerie.Nemotecnico, System.Data.SqlDbType.VarChar)

            ds = sp.ReturnDataSet()

            For Each dataRow As DataRow In ds.Tables(0).Rows
                ListaSerie.Add(fillSerie(dataRow))
            Next
        Catch ex As Exception
            Throw
        End Try

        Return ListaSerie
    End Function



    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function FondoSerieConsultarHitos(fondoserie As FondoSerieDTO) As List(Of FondoSerieDTO)
        Dim ListaSerie As List(Of FondoSerieDTO) = New List(Of FondoSerieDTO)
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)
        Dim ds As DataSet

        Try
            sp.AgregarParametro("Accion", "SELECT_ALL_HITOS", System.Data.SqlDbType.VarChar)
            FillParameters(fondoserie, sp)
            ds = sp.ReturnDataSet()

            For Each dataRow As DataRow In ds.Tables(0).Rows
                ListaSerie.Add(fillSerie(dataRow))
            Next

        Catch ex As Exception
            Throw
        End Try

        Return ListaSerie
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function CargarDistinctNemotecnicoHitos(FondoSerie As FondoSerieDTO) As List(Of FondoSerieDTO)
        Dim Lista As List(Of FondoSerieDTO) = New List(Of FondoSerieDTO)
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)
        Dim ds As DataSet
        Dim FondoSerieNew As New FondoSerieDTO

        Try
            sp.AgregarParametro("Accion", "SELECT_DISTINCT_HITOS", System.Data.SqlDbType.VarChar)

            ds = sp.ReturnDataSet()

            For Each dataRow As DataRow In ds.Tables(0).Rows
                FondoSerieNew = New FondoSerieDTO
                With FondoSerieNew
                    .Nemotecnico = dataRow("FS_Nemotecnico").ToString().Trim()
                End With
                Lista.Add(FondoSerieNew)
            Next


        Catch ex As Exception
            Throw
        End Try

        Return Lista
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function FondoSerieConsultar(fondoserie As FondoSerieDTO) As List(Of FondoSerieDTO)
        Dim ListaSerie As List(Of FondoSerieDTO) = New List(Of FondoSerieDTO)
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)
        Dim ds As DataSet

        Try
            sp.AgregarParametro("Accion", CONST_SELECT_ALL, System.Data.SqlDbType.VarChar)
            FillParameters(fondoserie, sp)
            ds = sp.ReturnDataSet()

            For Each dataRow As DataRow In ds.Tables(0).Rows
                ListaSerie.Add(fillSerie(dataRow))
            Next

        Catch ex As Exception
            Throw
        End Try

        Return ListaSerie
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function FondoSerieTraer(fondoSerie As FondoSerieDTO) As FondoSerieDTO
        Dim serie As FondoSerieDTO = New FondoSerieDTO
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)
        Dim ds As DataSet

        Try
            sp.AgregarParametro("Accion", CONST_SELECT_ALL, System.Data.SqlDbType.VarChar)
            FillParameters(fondoSerie, sp)
            ds = sp.ReturnDataSet()

            If ds.Tables.Count > 0 AndAlso ds.Tables(0).Rows.Count > 0 Then
                serie = fillSerie(ds.Tables(0).Rows(0))
            End If
        Catch ex As Exception
            Throw
        End Try
        Return serie
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function FondoSerieIngresar(serie As FondoSerieDTO) As Integer
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)
        Try
            ' ---TODO Data prueba---
            sp.AgregarParametro("Accion", CONST_INSERT, System.Data.SqlDbType.VarChar)
            serie.Estado = DTO.Estados.CONST_ACTIVO
            FillParameters(serie, sp)

            sp.ReturnDataSet()

            Return True
        Catch ex As Exception
            Throw
        End Try
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function FondoSerieModificar(serie As FondoSerieDTO) As Boolean
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)
        Try
            sp.AgregarParametro("Accion", CONST_UPDATE, System.Data.SqlDbType.VarChar)
            serie.Estado = DTO.Estados.CONST_ACTIVO
            FillParameters(serie, sp)
            sp.ReturnDataSet()

            Return True
        Catch ex As Exception
            Throw
        End Try
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function FondoSerieEliminar(serie As FondoSerieDTO) As Boolean
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)
        Try
            sp.AgregarParametro("Accion", CONST_DELETE, System.Data.SqlDbType.VarChar)
            serie.Estado = DTO.Estados.CONST_ELIMINADO
            FillParameters(serie, sp)
            sp.ReturnDataSet()
            Return True

        Catch ex As Exception
            Return False
        End Try
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function GetByRutAgrupacion(fondoSerie As FondoSerieDTO) As List(Of FondoSerieDTO)
        Dim listaSerie As List(Of FondoSerieDTO) = New List(Of FondoSerieDTO)
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)
        Dim ds As DataSet

        Try
            sp.AgregarParametro("Accion", CONST_SELECT_BY_RUT_AGRUPACION, System.Data.SqlDbType.VarChar)

            FillParameters(fondoSerie, sp)

            ds = sp.ReturnDataSet()

            For Each dataRow As DataRow In ds.Tables(0).Rows
                listaSerie.Add(fillSerie(dataRow))
            Next

        Catch ex As Exception
            Throw
        End Try

        Return listaSerie
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function GetByMoneda(fondoSerie As FondoSerieDTO) As List(Of FondoSerieDTO)
        Dim listaSerie As List(Of FondoSerieDTO) = New List(Of FondoSerieDTO)
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CONSULTAS)
        Dim FondoSerieNew As New FondoSerieDTO
        Dim ds As DataSet

        Try
            sp.AgregarParametro("Accion", CONST_SELECT_BY_MONEDA, System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("Moneda", fondoSerie.Moneda, System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("RutFondo", fondoSerie.Rut, System.Data.SqlDbType.VarChar)

            ds = sp.ReturnDataSet()

            For Each dataRow As DataRow In ds.Tables(0).Rows
                FondoSerieNew = New FondoSerieDTO
                With FondoSerieNew
                    .Nemotecnico = dataRow("FS_Nemotecnico").ToString().Trim()
                End With
                listaSerie.Add(FondoSerieNew)
            Next


        Catch ex As Exception
            Throw
        End Try

        Return listaSerie
    End Function

    Private Sub FillParameters(fondoSerie As FondoSerieDTO, sp As DBSqlServer.SqlServer.StoredProcedure)
        sp.AgregarParametro("FnRut", fondoSerie.Rut, System.Data.SqlDbType.VarChar)
        sp.AgregarParametro("FsNemotecnico", fondoSerie.Nemotecnico, System.Data.SqlDbType.VarChar)
        sp.AgregarParametro("FsNombreCorto", fondoSerie.Nombrecorto, System.Data.SqlDbType.VarChar)
        sp.AgregarParametro("FsRemuneracion", fondoSerie.Remuneracion, System.Data.SqlDbType.Char)
        sp.AgregarParametro("FsNacExt", fondoSerie.Nacionalidad, System.Data.SqlDbType.Char)
        sp.AgregarParametro("FsCalificado", fondoSerie.Calificado, System.Data.SqlDbType.Char)
        sp.AgregarParametro("FsMoneda", fondoSerie.Moneda, System.Data.SqlDbType.VarChar)
        sp.AgregarParametro("FsLimiteMoneda", fondoSerie.LimiteMoneda, System.Data.SqlDbType.VarChar)
        sp.AgregarParametro("FsLimiteMin", fondoSerie.LimiteMinimo, System.Data.SqlDbType.Decimal)
        sp.AgregarParametro("FsLimiteMax", fondoSerie.LimiteMaximo, System.Data.SqlDbType.Decimal)
        sp.AgregarParametro("FsExcMon", fondoSerie.ExclusivoMAM, System.Data.SqlDbType.Bit)
        sp.AgregarParametro("FsDefault", fondoSerie.Compatible, System.Data.SqlDbType.Bit)
        sp.AgregarParametro("FsCanjeMandatorio", fondoSerie.Canje, System.Data.SqlDbType.Bit)
        sp.AgregarParametro("FsNivelAgrupacion", fondoSerie.Nivel, System.Data.SqlDbType.Decimal)
        sp.AgregarParametro("FsEstado", fondoSerie.Estado, System.Data.SqlDbType.Int)
        sp.AgregarParametro("FsUsuario", fondoSerie.UsuarioIngreso, System.Data.SqlDbType.VarChar)
        sp.AgregarParametro("FsHorarioRescates", fondoSerie.HorarioRecaste, System.Data.SqlDbType.VarChar)
        sp.AgregarParametro("FsFondoRescatable", fondoSerie.FondoRescatable, System.Data.SqlDbType.VarChar)
        sp.AgregarParametro("FsFechaNav", fondoSerie.FechaNav, System.Data.SqlDbType.VarChar)
        sp.AgregarParametro("FsFechaRescate ", fondoSerie.FechaRescate, System.Data.SqlDbType.VarChar)
        sp.AgregarParametro("FechaTCObservado ", fondoSerie.FechaTCObservado, System.Data.SqlDbType.VarChar)
        sp.AgregarParametro("FsFijacionNav", fondoSerie.FijacionNav, System.Data.SqlDbType.VarChar)
        sp.AgregarParametro("FsFechaNavSuscripcion", fondoSerie.FechaNavSuscripcion, System.Data.SqlDbType.VarChar)
        sp.AgregarParametro("FsFechaSuscripcion", fondoSerie.FechaSuscripcion, System.Data.SqlDbType.VarChar)
        sp.AgregarParametro("FsFechaTCSuscripcion", fondoSerie.FechaTCSuscripcion, System.Data.SqlDbType.VarChar)
        sp.AgregarParametro("FsFijacionNavSuscripcion", fondoSerie.FijacionSuscripcion, System.Data.SqlDbType.VarChar)
        sp.AgregarParametro("FsFechaNavCanje", fondoSerie.FechaNavCanje, System.Data.SqlDbType.VarChar)
        sp.AgregarParametro("FsFechaTCCanje", fondoSerie.FechaTCCanje, System.Data.SqlDbType.VarChar)
        sp.AgregarParametro("FsFijacionNavCanje", fondoSerie.FijacionCanje, System.Data.SqlDbType.VarChar)
        sp.AgregarParametro("FsPatrimonio", fondoSerie.Patrimonio, SqlDbType.VarChar)

        sp.AgregarParametro("FsDiasHabilesRescate", fondoSerie.DiasHabilesRescate, System.Data.SqlDbType.Int)
        sp.AgregarParametro("FsDiasHabilesSuscripciones", fondoSerie.DiasHabilesSuscripciones, System.Data.SqlDbType.Int)
        sp.AgregarParametro("FsDiasHabilesCanje", fondoSerie.DiasHabilesCanje, System.Data.SqlDbType.Int)
        sp.AgregarParametro("FsFechaCanjeCanje", fondoSerie.FechaCanjeCanje, System.Data.SqlDbType.VarChar)
        sp.AgregarParametro("FsDiasHabilesFechaCanje", fondoSerie.DiasHabilesFechaCanje, System.Data.SqlDbType.Int)
    End Sub


    Public Function fillSerie(dataRow As DataRow) As FondoSerieDTO
        Dim fondoSerie As New FondoSerieDTO

        With fondoSerie
            .Rut = dataRow("FN_RUT").ToString().Trim()
            .Nemotecnico = dataRow("FS_Nemotecnico").ToString().Trim()
            .Nombrecorto = dataRow("FS_Nombre_Corto").ToString().Trim()
            .Remuneracion = dataRow("FS_Remuneracion").ToString().Trim()
            .Nacionalidad = dataRow("FS_Nac_Ext").ToString().Trim()
            .Calificado = dataRow("FS_Calificado").ToString().Trim()
            .Moneda = dataRow("FS_Moneda").ToString().Trim()
            .LimiteMoneda = dataRow("FS_Limite_Moneda").ToString().Trim()
            .LimiteMinimo = dataRow("FS_Limite_Min").ToString().Trim()
            .LimiteMaximo = dataRow("FS_Limite_Max").ToString().Trim()
            .ExclusivoMAM = dataRow("FS_Exc_Mon")
            .Compatible = dataRow("FS_Default")
            .Canje = dataRow("FS_Canje_Mandatorio")
            .Nivel = dataRow("FS_Nivel_Agrupacion")
            .Estado = dataRow("FS_Estado").ToString().Trim()
            .FechaIngreso = dataRow("FS_Fecha_Ingreso")
            .UsuarioIngreso = dataRow("FS_Usuario_Ingreso")
            .FechaModificacion = dataRow("FS_Fecha_Modificacion")
            .UsuarioModificacion = dataRow("FS_Usuario_Modificacion")
            .HorarioRecaste = dataRow("FS_Horario_Recastes").ToString.Trim
            .FondoRescatable = dataRow("FS_Fondo_Rescatable").ToString().Trim()
            .FechaNav = dataRow("FS_Fecha_Nav").ToString().Trim()
            .FechaRescate = dataRow("FS_Fecha_Rescate").ToString().Trim()
            .FechaTCObservado = dataRow("FS_Fecha_TC_Observado").ToString().Trim()
            .Patrimonio = dataRow("FS_Patrimonio").ToString().Trim()
            .FijacionNav = dataRow("FS_Fijacion_Nav").ToString().Trim()
            .FechaNavSuscripcion = dataRow("FS_Fecha_Nav_Suscripcion").ToString().Trim()
            .FechaSuscripcion = dataRow("FS_Fecha_Suscripcion").ToString().Trim()
            .FechaTCSuscripcion = dataRow("FS_Fecha_TC_Suscripcion").ToString().Trim()
            .FijacionSuscripcion = dataRow("FS_Fijacion_Nav_Suscripcion").ToString().Trim()
            .FechaNavCanje = dataRow("FS_Fecha_Nav_Canje").ToString().Trim()
            .FechaTCCanje = dataRow("FS_Fecha_TC_Canje").ToString().Trim()
            .FijacionCanje = dataRow("FS_Fijacion_Nav_Canje").ToString().Trim()

            .DiasHabilesCanje = dataRow("FS_DiasHabilesCanje").ToString().Trim()
            .DiasHabilesRescate = dataRow("FS_DiasHabilesRescate").ToString().Trim()
            .DiasHabilesSuscripciones = dataRow("FS_DiasHabilesSuscripciones").ToString().Trim()

            .FechaCanjeCanje = dataRow("FS_Fecha_Canje_Canje").ToString().Trim()
            .DiasHabilesFechaCanje = dataRow("FS_DiasHabilesFechaCanje").ToString().Trim()

        End With
        Return fondoSerie

    End Function

End Class