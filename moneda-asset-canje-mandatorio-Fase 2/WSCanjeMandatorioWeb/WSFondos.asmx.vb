Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel
Imports DTO

' Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente.
' <System.Web.Script.Services.ScriptService()> _
<System.Web.Services.WebService(Namespace:="http://tempuri.org/")>
Public Class WSFondos
    Inherits System.Web.Services.WebService

    Private Const SP_CRUD As String = "PRC_FondosCRUD"
    Private Const SP_FONDO_FILTRO As String = "PRC_FondosBuscarFiltro"

    Private Const CONST_INSERT As String = "INSERT"
    Private Const CONST_UPDATE As String = "UPDATE"
    Private Const CONST_DELETE As String = "DELETE"
    Private Const CONST_SELECT_ALL As String = "SELECT_ALL"
    Private Const CONST_SELECT_FILTRO As String = "SELECT_FILTRO"
    Private Const CONST_SELECT_ONE As String = "SELECT_ONE"

    Public Sub New()
    End Sub
    <WebMethod(Description:="Devuelve datos de los fondos por filtro")>
    Public Function SSSS_TEST() As String
        Return "asdjsakld"
    End Function
    <WebMethod(Description:="Devuelve datos de los fondos por filtro")>
    Public Function FondoBuscarFiltro(fondo As FondoDTO, FechaHasta As Nullable(Of Date)) As List(Of FondoDTO)
        Dim listaFondos As List(Of FondoDTO) = New List(Of FondoDTO)
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)
        Dim ds As DataSet

        Try
            sp.AgregarParametro("Accion", CONST_SELECT_ALL, System.Data.SqlDbType.VarChar)

            FillParameters(fondo, sp)
            sp.AgregarParametro("fechaDesde", fondo.FechaIngreso, System.Data.SqlDbType.Date)
            sp.AgregarParametro("fechaHasta", FechaHasta, System.Data.SqlDbType.Date)

            ds = sp.ReturnDataSet()

            For Each dataRow As DataRow In ds.Tables(0).Rows
                listaFondos.Add(fillObject(dataRow))
            Next

        Catch ex As Exception
            Throw
        End Try

        Return listaFondos
    End Function


    <WebMethod()>
    Public Function FNConsultar(fondo As FondoDTO) As List(Of FondoDTO)
        Dim listaFondos As List(Of FondoDTO) = New List(Of FondoDTO)
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)
        Dim ds As DataSet

        Try
            sp.AgregarParametro("Accion", CONST_SELECT_ALL, System.Data.SqlDbType.VarChar)

            FillParameters(fondo, sp)

            ds = sp.ReturnDataSet()

            For Each dataRow As DataRow In ds.Tables(0).Rows
                listaFondos.Add(fillObject(dataRow))
            Next

        Catch ex As Exception
            Throw
        End Try

        Return listaFondos
    End Function

    <WebMethod()>
    Public Function GetFondo(fondo As FondoDTO) As FondoDTO
        Dim fondoRetorno As FondoDTO = New FondoDTO
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)
        Dim ds As DataSet

        Try
            sp.AgregarParametro("Accion", CONST_SELECT_ONE, System.Data.SqlDbType.VarChar)

            FillParameters(fondo, sp)

            ds = sp.ReturnDataSet()
            If ds.Tables.Count > 0 AndAlso ds.Tables(0).Rows.Count > 0 Then
                fondoRetorno = fillObject(ds.Tables(0).Rows(0))
            End If

        Catch ex As Exception
            Throw
        End Try

        Return fondoRetorno
    End Function

    <WebMethod()>
    Public Function FNModificar(fondo As FondoDTO) As Boolean
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)

        Try
            ' ---TODO Data prueba---
            sp.AgregarParametro("Accion", CONST_UPDATE, System.Data.SqlDbType.VarChar)

            fondo.Estado = DTO.Estados.CONST_ACTIVO
            FillParameters(fondo, sp)

            sp.ReturnDataSet()

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    <WebMethod()>
    Public Function FNAEliminar(fondo As FondoDTO) As Boolean
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)
        Try
            ' ---TODO Data prueba---
            sp.AgregarParametro("Accion", CONST_DELETE, System.Data.SqlDbType.VarChar)
            fondo.Estado = DTO.Estados.CONST_ELIMINADO
            FillParameters(fondo, sp)

            sp.ReturnDataSet()

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    <WebMethod()>
    Public Function FNAIngresar(fondo As FondoDTO) As Boolean
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)
        Try
            sp.AgregarParametro("Accion", CONST_INSERT, System.Data.SqlDbType.VarChar)

            fondo.Estado = DTO.Estados.CONST_ACTIVO

            FillParameters(fondo, sp)

            sp.ReturnDataSet()

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Shared Sub FillParameters(fondo As FondoDTO, sp As DBSqlServer.SqlServer.StoredProcedure)
        sp.AgregarParametro("FnRut", fondo.Rut, System.Data.SqlDbType.VarChar)
        sp.AgregarParametro("FnRazonSocial", fondo.RazonSocial, System.Data.SqlDbType.VarChar)
        sp.AgregarParametro("FnNombreCorto", fondo.NombreCorto, System.Data.SqlDbType.Int)
        sp.AgregarParametro("FnEstado", fondo.Estado, System.Data.SqlDbType.Int)
        sp.AgregarParametro("FnUsuario", fondo.UsuarioIngreso, System.Data.SqlDbType.VarChar)
    End Sub

    Private Function fillObject(dataRow As DataRow) As FondoDTO
        Dim fondo As New FondoDTO

        With fondo
            .Rut = dataRow("FN_RUT").ToString().Trim()
            .RazonSocial = dataRow("FN_Razon_Social").ToString().Trim()
            .NombreCorto = dataRow("FN_Nombre_Corto").ToString().Trim()
            .Estado = dataRow("FN_Estado").ToString().Trim()
            .FechaIngreso = dataRow("FN_Fecha_Ingreso")
            .UsuarioIngreso = dataRow("FN_Usuario_Ingreso")
            .FechaModificacion = dataRow("FN_Fecha_Modificacion")
            .UsuarioModificacion = dataRow("FN_Usuario_Modificacion")

        End With
        Return fondo
    End Function
End Class