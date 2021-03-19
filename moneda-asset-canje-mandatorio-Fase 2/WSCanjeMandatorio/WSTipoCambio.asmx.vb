Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel
Imports DTO
Imports System.Web.Script.Services

' Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente.
' <System.Web.Script.Services.ScriptService()> _
<System.Web.Services.WebService(Namespace:="http://tempuri.org/")> _
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)>
<ToolboxItem(False)>
Public Class WSTipoCambio
    Inherits System.Web.Services.WebService

    Private Const SP_CRUD As String = "PRC_TipoCambioCRUD"

    Private Const CONST_INSERT As String = "INSERT"
    Private Const CONST_UPDATE As String = "UPDATE"
    Private Const CONST_DELETE As String = "DELETE"
    Private Const CONST_SELECT_ALL As String = "SELECT_ALL"
    Private Const CONST_SELECT_ONE As String = "SELECT_ONE"
    Private Const CONST_SELECT_FILTRO As String = "SELECT_FILTRO"

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function GetListaTipoCambio(TipoCambio As TipoCambioDTO) As List(Of TipoCambioDTO)
        Dim listaTipoCambio As List(Of TipoCambioDTO) = New List(Of TipoCambioDTO)
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)
        Dim ds As DataSet

        Try
            sp.AgregarParametro("Accion", CONST_SELECT_ALL, System.Data.SqlDbType.VarChar)

            FillParameters(TipoCambio, sp)
            ds = sp.ReturnDataSet()

            For Each dataRow As DataRow In ds.Tables(0).Rows
                listaTipoCambio.Add(fillObject(dataRow))
            Next

        Catch ex As Exception
            Throw
        End Try

        Return listaTipoCambio
    End Function


    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function TCBuscarFiltroCodigo(TipoCambio As TipoCambioDTO, FechaHasta As Nullable(Of Date)) As List(Of TipoCambioDTO)
        Dim listaTipoCambio As List(Of TipoCambioDTO) = New List(Of TipoCambioDTO)
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)
        Dim ds As DataSet

        Try
            sp.AgregarParametro("Accion", "SELECT_FILTRO_CODIGO", System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("fechaDesde", TipoCambio.FechaIngreso, System.Data.SqlDbType.Date)
            sp.AgregarParametro("fechaHasta", FechaHasta, System.Data.SqlDbType.Date)

            FillParameters(TipoCambio, sp)
            ds = sp.ReturnDataSet()
            For Each dataRow As DataRow In ds.Tables(0).Rows
                listaTipoCambio.Add(fillObject(dataRow))
            Next

        Catch ex As Exception
            Throw
        End Try

        Return listaTipoCambio
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function TCBuscarFiltro(TipoCambio As TipoCambioDTO, FechaHasta As Nullable(Of Date)) As List(Of TipoCambioDTO)
        Dim listaTipoCambio As List(Of TipoCambioDTO) = New List(Of TipoCambioDTO)
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)
        Dim ds As DataSet

        Try
            sp.AgregarParametro("Accion", CONST_SELECT_FILTRO, System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("fechaDesde", TipoCambio.FechaIngreso, System.Data.SqlDbType.Date)
            sp.AgregarParametro("fechaHasta", FechaHasta, System.Data.SqlDbType.Date)

            FillParameters(TipoCambio, sp)
            ds = sp.ReturnDataSet()
            For Each dataRow As DataRow In ds.Tables(0).Rows
                listaTipoCambio.Add(fillObject(dataRow))
            Next

        Catch ex As Exception
            Throw
        End Try

        Return listaTipoCambio
    End Function
    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function GetRelaciones(TipoCambio As DTO.TipoCambioDTO)
        Dim TCRetorno As TipoCambioDTO = New TipoCambioDTO
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)
        Dim ds As DataSet

        Try
            sp.AgregarParametro("Accion", "SELECT_RELACIONES", System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("TcCodigo", TipoCambio.Codigo, System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("TcFecha", TipoCambio.Fecha, System.Data.SqlDbType.Date)

            ds = sp.ReturnDataSet()
            If ds.Tables.Count > 0 AndAlso ds.Tables(0).Rows.Count > 0 Then
                Dim dataRow As DataRow = ds.Tables(0).Rows(0)
                Try
                    TCRetorno.Valor = dataRow("CantidadRelaciones")
                Catch ex As Exception
                    TCRetorno.Valor = 0
                End Try
            End If
        Catch ex As Exception
            Throw
        End Try

        Return TCRetorno
    End Function
    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function ConsultarPorCodigo(TipoCambio As TipoCambioDTO) As List(Of TipoCambioDTO)
        Dim ListaTC As List(Of TipoCambioDTO) = New List(Of TipoCambioDTO)
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)
        Dim ds As DataSet
        Dim TipoCambioNew As New TipoCambioDTO

        Try
            ' ---TODO Data prueba---
            sp.AgregarParametro("Accion", "SELECT_MONEDA", System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("TcCodigo", TipoCambio.Codigo, System.Data.SqlDbType.VarChar)

            ds = sp.ReturnDataSet()

            For Each dataRow As DataRow In ds.Tables(0).Rows
                TipoCambioNew = New TipoCambioDTO
                With TipoCambioNew
                    .Codigo = dataRow("TC_CODIGO").ToString().Trim()
                End With

                ListaTC.Add(TipoCambioNew)
            Next

        Catch ex As Exception
            Throw
        End Try

        Return ListaTC
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function ConsultarTipoCambioPorCodigoYFecha(tipoCambio As TipoCambioDTO) As List(Of TipoCambioDTO)
        Dim Lista As List(Of TipoCambioDTO) = New List(Of TipoCambioDTO)
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)
        Dim ds As DataSet
        Try
            sp.AgregarParametro("Accion", "SELECT_BY_FECHA", System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("TcCodigo", tipoCambio.Codigo, System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("TcFecha", tipoCambio.Fecha, System.Data.SqlDbType.Date)

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
    Public Function UltimoTipoCambioPorCodigo(tipoCambio As TipoCambioDTO) As List(Of TipoCambioDTO)
        Dim Lista As List(Of TipoCambioDTO) = New List(Of TipoCambioDTO)
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)
        Dim ds As DataSet
        Try
            sp.AgregarParametro("Accion", "SELECT_BY_ULTIMO", System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("TcCodigo", tipoCambio.Codigo, System.Data.SqlDbType.VarChar)

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
    Public Function ConsultarPorMoneda(TipoCambio As TipoCambioDTO) As List(Of TipoCambioDTO)
        Dim ListaTC As List(Of TipoCambioDTO) = New List(Of TipoCambioDTO)
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)
        Dim ds As DataSet
        Dim TCNew As New TipoCambioDTO

        Try
            ' ---TODO Data prueba---
            sp.AgregarParametro("Accion", "SELECT_BY_CODE", System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("TcCodigo", TipoCambio.Codigo, System.Data.SqlDbType.VarChar)

            ds = sp.ReturnDataSet()

            For Each dataRow As DataRow In ds.Tables(0).Rows
                TCNew = New TipoCambioDTO
                With TCNew
                    .Fecha = dataRow("TC_Fecha").ToString.Trim()
                    .Codigo = dataRow("TC_Codigo").ToString().Trim()
                    .Valor = dataRow("TC_Valor").ToString().Trim()
                    .FechaIngreso = dataRow("TC_Fecha_Ingreso").ToString().Trim()
                    .UsuarioIngreso = dataRow("TC_Usuario_Ingreso").ToString().Trim()
                    .FechaModificacion = dataRow("TC_Fecha_Modificacion").ToString().Trim()
                    .UsuarioModificacion = dataRow("TC_Usuario_Modificacion").ToString().Trim()
                End With
                ListaTC.Add(TCNew)
            Next

        Catch ex As Exception
            Throw
        End Try

        Return ListaTC
    End Function


    Private Function TipoCambioTraer(TipoCambio As TipoCambioDTO, accion As String) As TipoCambioDTO
        Dim tcRet As TipoCambioDTO = New TipoCambioDTO
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)
        Dim ds As DataSet

        Try
            sp.AgregarParametro("Accion", accion, System.Data.SqlDbType.VarChar)

            FillParameters(TipoCambio, sp)

            ds = sp.ReturnDataSet()

            If ds.Tables.Count > 0 AndAlso ds.Tables(0).Rows.Count > 0 Then
                tcRet = fillObject(ds.Tables(0).Rows(0))
            End If

        Catch ex As Exception
            Throw
        End Try

        Return tcRet
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function TipoCambioModificar(TipoCambio As TipoCambioDTO) As Boolean
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)

        Try

            sp.AgregarParametro("Accion", CONST_UPDATE, System.Data.SqlDbType.VarChar)

            FillParameters(TipoCambio, sp)

            sp.ReturnDataSet()

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function TipoCambioEliminar(TipoCambio As TipoCambioDTO) As Boolean
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)
        Try

            sp.AgregarParametro("Accion", CONST_DELETE, System.Data.SqlDbType.VarChar)
            FillParameters(TipoCambio, sp)
            sp.ReturnDataSet()

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function TipoCambioIngresar(TipoCambio As TipoCambioDTO) As Boolean
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)
        Try
            sp.AgregarParametro("Accion", CONST_INSERT, System.Data.SqlDbType.VarChar)
            TipoCambio.Estado = DTO.Estados.CONST_ACTIVO
            FillParameters(TipoCambio, sp)
            sp.ReturnDataSet()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function GetTipoCambio(TipoCambio As TipoCambioDTO) As TipoCambioDTO
        Dim TCRetorno As TipoCambioDTO = New TipoCambioDTO
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)
        Dim ds As DataSet

        Try
            sp.AgregarParametro("Accion", CONST_SELECT_ONE, System.Data.SqlDbType.VarChar)

            FillParameters(TipoCambio, sp)

            ds = sp.ReturnDataSet()
            If ds.Tables.Count > 0 AndAlso ds.Tables(0).Rows.Count > 0 Then
                TCRetorno = fillObject(ds.Tables(0).Rows(0))
            Else
                TCRetorno = Nothing
            End If

        Catch ex As Exception
            Throw
        End Try

        Return TCRetorno
    End Function
    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function GetTipoCambioPorFecha(TipoCambio As TipoCambioDTO) As TipoCambioDTO
        Dim TCRetorno As TipoCambioDTO = New TipoCambioDTO
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)
        Dim ds As DataSet

        Try
            sp.AgregarParametro("Accion", "TC_POR_FECHA", System.Data.SqlDbType.VarChar)

            FillParameters(TipoCambio, sp)

            ds = sp.ReturnDataSet()
            If ds.Tables.Count > 0 AndAlso ds.Tables(0).Rows.Count > 0 Then
                TCRetorno = fillObject(ds.Tables(0).Rows(0))
            Else
                TCRetorno = Nothing
            End If

        Catch ex As Exception
            Throw
        End Try

        Return TCRetorno
    End Function

    Private Shared Sub FillParameters(TipoCambio As TipoCambioDTO, sp As DBSqlServer.SqlServer.StoredProcedure)
        sp.AgregarParametro("TcCodigo", TipoCambio.Codigo, System.Data.SqlDbType.VarChar)
        sp.AgregarParametro("TcFecha", TipoCambio.Fecha, System.Data.SqlDbType.Date)
        sp.AgregarParametro("TcValor", TipoCambio.Valor, System.Data.SqlDbType.Decimal)
        sp.AgregarParametro("TcEstado", TipoCambio.Estado, System.Data.SqlDbType.SmallInt)
        sp.AgregarParametro("TcUsuario", TipoCambio.UsuarioIngreso, System.Data.SqlDbType.VarChar)
    End Sub

    Private Function fillObject(dataRow As DataRow) As TipoCambioDTO
        Dim TipoCambio As New TipoCambioDTO

        With TipoCambio
            .Codigo = dataRow("TC_Codigo").ToString().Trim()
            .Fecha = dataRow("TC_Fecha").ToString().Trim()
            .Valor = dataRow("TC_Valor").ToString().Trim()
            .Estado = dataRow("TC_Estado").ToString().Trim()
            .FechaIngreso = dataRow("TC_Fecha_Ingreso").ToString().Trim()
            .UsuarioIngreso = dataRow("TC_Usuario_Ingreso").ToString().Trim()
            .FechaModificacion = dataRow("TC_Fecha_Modificacion").ToString().Trim()
            .UsuarioModificacion = dataRow("TC_Usuario_Modificacion").ToString().Trim()



        End With
        Return TipoCambio
    End Function


End Class