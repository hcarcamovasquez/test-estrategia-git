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
Public Class WSHitos
    Inherits System.Web.Services.WebService

    Private Const SP_CRUD As String = "PRC_HitosCRUD"
    Private Const CONST_INSERT As String = "INSERT"
    Private Const CONST_UPDATE As String = "UPDATE"
    Private Const CONST_DELETE As String = "DELETE"
    Private Const CONST_SELECT_ALL As String = "SELECT_ALL"
    Private Const CONST_SELECT_ONE As String = "SELECT_ONE"
    Private Const CONST_SELECT_FILTRO As String = "CONSULTA_POR_FILTRO"
    Private Const CONST_SELECT_RELACIONES As String = "SELECT_RELACIONES"

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function BuscarRelaciones(Hito As HitoDTO) As Integer
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)
        Dim ds As DataSet
        Try
            ' ---TODO Data prueba---
            sp.AgregarParametro("Accion", CONST_SELECT_RELACIONES, System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("HtId", Hito.IdHito, System.Data.SqlDbType.Int)

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
    Public Function HTConsultarFiltros(hito As HitoDTO, fechaHasta As Nullable(Of Date), fondo As FondoDTO) As List(Of HitoDTO)
        Dim listaHitos As List(Of HitoDTO) = New List(Of HitoDTO)
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)
        Dim ds As DataSet

        Try
            sp.AgregarParametro("Accion", CONST_SELECT_FILTRO, System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("FnRut", hito.Rut, System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("FnNombreFondo", hito.NombreFondo, System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("fechaDesde", hito.FechaCorte, System.Data.SqlDbType.Date)
            sp.AgregarParametro("fechaHasta", fechaHasta, System.Data.SqlDbType.Date)

            ds = sp.ReturnDataSet()

            For Each dataRow As DataRow In ds.Tables(0).Rows
                listaHitos.Add(fillHitos(dataRow))
            Next

        Catch ex As Exception
            Throw
        End Try

        Return listaHitos
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function ConsultarUltimoshitos(hito As HitoDTO) As List(Of HitoDTO)
        Dim listaHitos As List(Of HitoDTO) = New List(Of HitoDTO)
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)
        Dim ds As DataSet

        Try
            sp.AgregarParametro("Accion", "CONSULTAULTIMOPARACERTIFICADO", System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("FnRut", hito.Rut, System.Data.SqlDbType.VarChar)

            ds = sp.ReturnDataSet()

            For Each dataRow As DataRow In ds.Tables(0).Rows
                listaHitos.Add(fillHitos(dataRow))
            Next

        Catch ex As Exception
            Throw
        End Try

        Return listaHitos
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function HTConsultarRut(hito As HitoDTO) As List(Of HitoDTO)
        Dim listaHitos As List(Of HitoDTO) = New List(Of HitoDTO)
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)
        Dim ds As DataSet

        Try
            sp.AgregarParametro("Accion", "CONSULTA_POR_RUT", System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("FnRut", hito.Rut, System.Data.SqlDbType.VarChar)

            ds = sp.ReturnDataSet()

            For Each dataRow As DataRow In ds.Tables(0).Rows
                hito = New HitoDTO
                With hito
                    .Rut = dataRow("FN_RUT").ToString().Trim()
                End With

                listaHitos.Add(hito)
            Next

        Catch ex As Exception
            Throw
        End Try

        Return listaHitos
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function ConsultarPorNombreRut(fondo As FondoDTO) As List(Of FondoDTO)
        Dim Lista As List(Of FondoDTO) = New List(Of FondoDTO)
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)
        Dim ds As DataSet
        Dim fondoNew As New FondoDTO

        Try
            sp.AgregarParametro("Accion", "CONSULTA_POR_NOMBRE", System.Data.SqlDbType.VarChar)

            ds = sp.ReturnDataSet()

            For Each dataRow As DataRow In ds.Tables(0).Rows
                fondoNew = New FondoDTO
                With fondoNew
                    .Rut = dataRow("FN_RUT").ToString().Trim() + "/" + dataRow("FN_Razon_Social").ToString().Trim()
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
    Public Function CompararDatos(fondo As FondoDTO) As List(Of FondoDTO)
        Dim Lista As List(Of FondoDTO) = New List(Of FondoDTO)
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)
        Dim ds As DataSet
        Dim fondoNew As New FondoDTO

        Try
            sp.AgregarParametro("Accion", "COMPARAR_DATOS", System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("FnRut", fondo.Rut, System.Data.SqlDbType.VarChar)
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
    Public Function ConsultarPorHitos(Hito As HitoDTO) As List(Of HitoDTO)
        Dim ListaHitos As List(Of HitoDTO) = New List(Of HitoDTO)
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)
        Dim ds As DataSet
        Dim HitoNew As New HitoDTO

        Try
            sp.AgregarParametro("Accion", "CONSULTA_POR_HITO", System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("HtId", Hito.IdHito, System.Data.SqlDbType.VarChar)

            ds = sp.ReturnDataSet()

            For Each dataRow As DataRow In ds.Tables(0).Rows
                HitoNew = New HitoDTO
                With HitoNew
                    .IdHito = dataRow("HT_ID").ToString().Trim()
                End With

                ListaHitos.Add(HitoNew)
            Next

        Catch ex As Exception
            Throw
        End Try
        Return ListaHitos
    End Function





    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function ConsultarFechasParaCertificados(Hitos As HitoDTO) As HitoDTO
        Dim HitoRetorno As HitoDTO = New HitoDTO
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)
        Dim ds As DataSet

        Try
            sp.AgregarParametro("Accion", "CONSULTA_FECHASCERTIFICADOS", System.Data.SqlDbType.VarChar)
            ' sp.AgregarParametro("HtId", Hitos.IdHito, System.Data.SqlDbType.VarChar)

            FillParameters(Hitos, sp)

            ds = sp.ReturnDataSet()
            If ds.Tables.Count > 0 AndAlso ds.Tables(0).Rows.Count > 0 Then
                HitoRetorno = fillHitos(ds.Tables(0).Rows(0))
            Else
                HitoRetorno = Nothing
            End If

        Catch ex As Exception
            Throw
        End Try

        Return HitoRetorno
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function HTConsultar(hito As HitoDTO) As List(Of HitoDTO)
        Dim listaHitos As List(Of HitoDTO) = New List(Of HitoDTO)
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)
        Dim ds As DataSet

        Try
            sp.AgregarParametro("Accion", CONST_SELECT_ALL, System.Data.SqlDbType.VarChar)

            FillParameters(hito, sp)

            ds = sp.ReturnDataSet()

            For Each dataRow As DataRow In ds.Tables(0).Rows
                listaHitos.Add(fillHitos(dataRow))
            Next

        Catch ex As Exception
            Throw
        End Try

        Return listaHitos
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function GetHito(hito As HitoDTO) As HitoDTO
        Dim hitoRetorno As HitoDTO = New HitoDTO
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)
        Dim ds As DataSet
        Try
            sp.AgregarParametro("Accion", CONST_SELECT_ONE, System.Data.SqlDbType.VarChar)
            hito.Estado = DTO.Estados.CONST_ACTIVO

            FillParameters(hito, sp)
            ds = sp.ReturnDataSet()
            If ds.Tables.Count > 0 AndAlso ds.Tables(0).Rows.Count > 0 Then
                hitoRetorno = fillHitos(ds.Tables(0).Rows(0))
            Else
                hitoRetorno = Nothing
            End If


        Catch ex As Exception
            Throw
        End Try

        Return hitoRetorno
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function GetHitoPorNombre(hito As HitoDTO) As HitoDTO
        Dim hitoRetorno As HitoDTO = New HitoDTO
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)
        Dim ds As DataSet
        Try
            sp.AgregarParametro("Accion", "SELECT_ONE_POR_NOMBRE", System.Data.SqlDbType.VarChar)
            hito.Estado = DTO.Estados.CONST_ACTIVO

            FillParameters(hito, sp)
            ds = sp.ReturnDataSet()
            If ds.Tables.Count > 0 AndAlso ds.Tables(0).Rows.Count > 0 Then
                hitoRetorno = fillHitos(ds.Tables(0).Rows(0))
            Else
                hitoRetorno = Nothing
            End If


        Catch ex As Exception
            Throw
        End Try

        Return hitoRetorno
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function ConsultarUltimoHito(hito As HitoDTO) As HitoDTO
        Dim listaHitos As HitoDTO = New HitoDTO
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)
        Dim ds As DataSet

        Try
            sp.AgregarParametro("Accion", "CONSULTA_ULTIMO", System.Data.SqlDbType.VarChar)

            FillParameters(hito, sp)

            ds = sp.ReturnDataSet()

            For Each dataRow As DataRow In ds.Tables(0).Rows
                listaHitos = fillHitos(dataRow)
            Next

        Catch ex As Exception
        End Try

        Return listaHitos
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function HitoIngresar(hito As HitoDTO) As Boolean
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)
        Try
            sp.AgregarParametro("Accion", CONST_INSERT, System.Data.SqlDbType.VarChar)
            hito.Estado = DTO.Estados.CONST_ACTIVO
            FillParameters(hito, sp)
            sp.ReturnDataSet()

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function HitoEliminar(hito As HitoDTO) As Boolean
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)
        Try
            sp.AgregarParametro("Accion", CONST_DELETE, System.Data.SqlDbType.VarChar)
            hito.Estado = DTO.Estados.CONST_ELIMINADO

            FillParameters(hito, sp)
            sp.ReturnDataSet()

            Return (sp.FilasAfectas > 0)
        Catch ex As Exception
            Return False
        End Try
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function HitoModificar(hito As HitoDTO) As Boolean
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)
        Try
            sp.AgregarParametro("Accion", CONST_UPDATE, System.Data.SqlDbType.VarChar)
            hito.Estado = DTO.Estados.CONST_ACTIVO

            FillParameters(hito, sp)
            sp.ReturnDataSet()

            Return (sp.FilasAfectas > 0)
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Sub FillParameters(hito As HitoDTO, sp As DBSqlServer.SqlServer.StoredProcedure)
        sp.AgregarParametro("HtId", hito.IdHito, System.Data.SqlDbType.Int)
        sp.AgregarParametro("FnRut", hito.Rut, System.Data.SqlDbType.VarChar)
        sp.AgregarParametro("FnNombreFondo", hito.NombreFondo, System.Data.SqlDbType.VarChar)
        sp.AgregarParametro("HtCorte", hito.FechaCorte, System.Data.SqlDbType.Date)
        sp.AgregarParametro("HtCanje", hito.FechaCanje, System.Data.SqlDbType.Date)
        sp.AgregarParametro("HtEstado", hito.Estado, System.Data.SqlDbType.Int)
        sp.AgregarParametro("HtUsuario", hito.UsuarioIngreso, System.Data.SqlDbType.VarChar)
    End Sub


    Private Function fillHitos(dataRow As DataRow) As HitoDTO
        Dim hito As New HitoDTO

        With hito
            .IdHito = dataRow("HT_ID").ToString().Trim()
            .Rut = dataRow("FN_RUT").ToString().Trim()
            .FechaCorte = dataRow("HT_Corte")
            .FechaCanje = dataRow("HT_Canje")
            .Estado = dataRow("HT_Estado").ToString().Trim()
            .FechaIngreso = dataRow("HT_Fecha_Ingreso").ToString().Trim()
            .UsuarioIngreso = dataRow("HT_Usuario_Ingreso").ToString().Trim()
            .FechaModificacion = dataRow("HT_Fecha_Modificacion").ToString().Trim()
            .UsuarioModificacion = dataRow("HT_Usuario_Modificacion").ToString().Trim()
            .NombreFondo = dataRow("FN_Nombre_Fondo").ToString().Trim()
        End With
        Return hito

    End Function

End Class