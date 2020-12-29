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
Public Class WSValoresCuota
    Inherits System.Web.Services.WebService

    Private Const SP_CRUD As String = "PRC_ValoresCuotaCRUD"

    Private Const CONST_INSERT As String = "INSERT"
    Private Const CONST_UPDATE As String = "UPDATE"
    Private Const CONST_DELETE As String = "DELETE"
    Private Const CONST_SELECT_ALL As String = "SELECT_ALL"
    Private Const CONST_SELECT_FILTRO As String = "SELECT_FILTRO"
    Private Const CONST_SELECT_ONE As String = "SELECT_ONE"
    Private Const CONST_SELECT_RELACIONES As String = "SELECT_RELACIONES"

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function FNAIngresar(valoresCuota As VcSerieDTO) As Boolean
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)
        Try
            sp.AgregarParametro("Accion", CONST_INSERT, System.Data.SqlDbType.VarChar)
            valoresCuota.Estado = DTO.Estados.CONST_ACTIVO
            FillParameters(valoresCuota, sp)
            sp.ReturnDataSet()

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Sub FillParameters(valoresCuota As VcSerieDTO, sp As DBSqlServer.SqlServer.StoredProcedure)
        sp.AgregarParametro("FnRut", valoresCuota.FnRut, System.Data.SqlDbType.VarChar)
        sp.AgregarParametro("FsNemotecnico", valoresCuota.FsNemotecnico, System.Data.SqlDbType.VarChar)
        sp.AgregarParametro("VCS_Fecha", valoresCuota.Fecha, System.Data.SqlDbType.Date)
        sp.AgregarParametro("VCS_Valor", valoresCuota.Valor, System.Data.SqlDbType.Decimal)
        sp.AgregarParametro("VCS_Estado", valoresCuota.Estado, System.Data.SqlDbType.Int)
        'sp.AgregarParametro("fechaIngreso", valoresCuota.FechaIngreso, System.Data.SqlDbType.Date)
        sp.AgregarParametro("VCS_Usuario", valoresCuota.UsuarioIngreso, System.Data.SqlDbType.VarChar)
        sp.AgregarParametro("VCS_Fecha_Modificacion", valoresCuota.FechaModificacion, System.Data.SqlDbType.Date)
        sp.AgregarParametro("VCS_Usuario_Modificacion", valoresCuota.UsuarioModificacion, System.Data.SqlDbType.VarChar)
    End Sub

    Private Function fillObject(dataRow As DataRow) As VcSerieDTO
        Dim ValoresCuota As New VcSerieDTO

        With ValoresCuota
            .FnRut = dataRow("FN_RUT").ToString().Trim()
            .FsNemotecnico = dataRow("FS_Nemotecnico").ToString().Trim()
            .Fecha = dataRow("VCS_Fecha")
            .Valor = dataRow("VCS_Valor")
            .FechaIngreso = dataRow("VCS_Fecha_Ingreso")
            .UsuarioIngreso = dataRow("VCS_Usuario_Ingreso")
            .FechaModificacion = dataRow("VCS_Fecha_Modificacion")
            .UsuarioModificacion = dataRow("VCS_Usuario_Modificacion")
            .Estado = dataRow("VCS_Estado").ToString().Trim()

        End With
        Return ValoresCuota
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function FNConsultar(ValoresCuota As VcSerieDTO) As List(Of VcSerieDTO)
        Dim listaValoresCuota As List(Of VcSerieDTO) = New List(Of VcSerieDTO)
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)
        Dim ds As DataSet

        Try
            sp.AgregarParametro("Accion", CONST_SELECT_ALL, System.Data.SqlDbType.VarChar)

            FillParameters(ValoresCuota, sp)

            ds = sp.ReturnDataSet()

            For Each dataRow As DataRow In ds.Tables(0).Rows
                listaValoresCuota.Add(fillObject(dataRow))
            Next

        Catch ex As Exception
            Throw
        End Try

        Return listaValoresCuota
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function ValoresCuotaPorNemotecnico(valor As VcSerieDTO) As List(Of VcSerieDTO)
        Dim Lista As List(Of VcSerieDTO) = New List(Of VcSerieDTO)
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)
        Dim ds As DataSet

        Try

            sp.AgregarParametro("Accion", "SELECT_BY_NEMOTECNICOYFECHA", System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("FsNemotecnico", valor.FsNemotecnico, System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("VCS_Fecha", valor.Fecha, System.Data.SqlDbType.Date)

            ds = sp.ReturnDataSet()

            For Each dataRow As DataRow In ds.Tables(0).Rows
                Lista.Add(fillObject(dataRow))
            Next

        Catch ex As Exception
            Throw
        End Try

        Return Lista

    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function UltimoValorCuota(valor As VcSerieDTO) As List(Of VcSerieDTO)
        Dim Lista As List(Of VcSerieDTO) = New List(Of VcSerieDTO)
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)
        Dim ds As DataSet

        Try

            sp.AgregarParametro("Accion", "SELECT_BY_ULTIMO", System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("FsNemotecnico", valor.FsNemotecnico, System.Data.SqlDbType.VarChar)

            ds = sp.ReturnDataSet()

            For Each dataRow As DataRow In ds.Tables(0).Rows
                Lista.Add(fillObject(dataRow))
            Next

        Catch ex As Exception
            Throw
        End Try

        Return Lista

    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function CargarFiltroNemotecnico(valor As VcSerieDTO) As List(Of VcSerieDTO)
        Dim Lista As List(Of VcSerieDTO) = New List(Of VcSerieDTO)
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)
        Dim ds As DataSet
        Dim ValorCuotaNew As New VcSerieDTO

        Try
            sp.AgregarParametro("Accion", "SELECT_FILTRO_NEMOTECNICO", System.Data.SqlDbType.VarChar)

            ds = sp.ReturnDataSet()

            For Each dataRow As DataRow In ds.Tables(0).Rows
                ValorCuotaNew = New VcSerieDTO
                With ValorCuotaNew
                    .FsNemotecnico = dataRow("FS_Nemotecnico").ToString().Trim()
                    .FnRut = dataRow("FN_RUT").ToString().Trim()
                End With
                Lista.Add(ValorCuotaNew)
            Next


        Catch ex As Exception
            Throw
        End Try

        Return Lista
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function ValoresCuotaBuscarFiltro(ValoresCuota As VcSerieDTO, FechaHasta As Nullable(Of Date)) As List(Of VcSerieDTO)
        Dim listaValoresCuota As List(Of VcSerieDTO) = New List(Of VcSerieDTO)
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)
        Dim ds As DataSet

        Try
            sp.AgregarParametro("Accion", CONST_SELECT_FILTRO, System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("FsNemotecnico", ValoresCuota.FsNemotecnico, System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("fechaDesde", ValoresCuota.FechaIngreso, System.Data.SqlDbType.Date)
            sp.AgregarParametro("fechaHasta", FechaHasta, System.Data.SqlDbType.Date)

            ds = sp.ReturnDataSet()

            For Each dataRow As DataRow In ds.Tables(0).Rows
                listaValoresCuota.Add(fillObject(dataRow))
            Next

        Catch ex As Exception
            Throw
        End Try

        Return listaValoresCuota
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function GetValoresCuota(ValoresCuota As VcSerieDTO) As VcSerieDTO
        Dim ValoresCuotaRetorno As VcSerieDTO = New VcSerieDTO
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)
        Dim ds As DataSet

        Try
            sp.AgregarParametro("Accion", CONST_SELECT_ONE, System.Data.SqlDbType.VarChar)

            FillParameters(ValoresCuota, sp)

            ds = sp.ReturnDataSet()
            If ds.Tables.Count > 0 AndAlso ds.Tables(0).Rows.Count > 0 Then
                ValoresCuotaRetorno = fillObject(ds.Tables(0).Rows(0))
            Else
                ValoresCuotaRetorno = Nothing
            End If

        Catch ex As Exception
            Throw
        End Try

        Return ValoresCuotaRetorno
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function FNModificar(ValoresCuota As VcSerieDTO) As Boolean
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)

        Try
            ' ---TODO Data prueba---
            sp.AgregarParametro("Accion", CONST_UPDATE, System.Data.SqlDbType.VarChar)

            ValoresCuota.Estado = DTO.Estados.CONST_ACTIVO
            FillParameters(ValoresCuota, sp)

            sp.ReturnDataSet()

            Return (sp.FilasAfectas > 0)
        Catch ex As Exception
            Return False
        End Try
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function FNAEliminar(ValoresCuota As VcSerieDTO) As Boolean
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)
        Try
            ' ---TODO Data prueba---
            sp.AgregarParametro("Accion", CONST_DELETE, System.Data.SqlDbType.VarChar)
            ValoresCuota.Estado = DTO.Estados.CONST_ELIMINADO
            FillParameters(ValoresCuota, sp)

            sp.ReturnDataSet()

            Return (sp.FilasAfectas > 0)
        Catch ex As Exception
            Return False
        End Try
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function BuscarRelaciones(ValoresCuota As VcSerieDTO) As Integer
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)
        Dim ds As DataSet
        Try
            ' ---TODO Data prueba---
            sp.AgregarParametro("Accion", CONST_SELECT_RELACIONES, System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("FnRut", ValoresCuota.FnRut, System.Data.SqlDbType.VarChar)

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

End Class