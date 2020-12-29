Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel
Imports DTO

' Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente.
' <System.Web.Script.Services.ScriptService()> _
<System.Web.Services.WebService(Namespace:="http://tempuri.org/")> _
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<ToolboxItem(False)> _
Public Class WSGrupoAportante
    Inherits System.Web.Services.WebService

    Private Const SP_CRUD As String = "PRC_GrupoAportantesCRUD"

    Private Const CONST_INSERT As String = "INSERT"
    Private Const CONST_UPDATE As String = "UPDATE"
    Private Const CONST_DELETE As String = "DELETE"
    Private Const CONST_SELECT_ALL As String = "SELECT_ALL"
    Private Const CONST_SELECT_ONE As String = "SELECT_ONE"

    <WebMethod()>
    Public Function GrupoAportanteConsultar(GrupoAportante As GrupoAportanteDTO) As List(Of GrupoAportanteDTO)
        Dim ListaAportantes As List(Of GrupoAportanteDTO) = New List(Of GrupoAportanteDTO)
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)
        Dim ds As DataSet

        Try
            sp.AgregarParametro("Accion", CONST_SELECT_ALL, System.Data.SqlDbType.VarChar)

            FillParameters(GrupoAportante, sp)

            ds = sp.ReturnDataSet()

            For Each dataRow As DataRow In ds.Tables(0).Rows
                ListaAportantes.Add(fillObject(dataRow))
            Next

        Catch ex As Exception
            Throw
        End Try

        Return ListaAportantes
    End Function

    <WebMethod()>
    Public Function GrupoAportanteTraer(GrupoAportante As GrupoAportanteDTO) As GrupoAportanteDTO
        Dim grupo As GrupoAportanteDTO = New GrupoAportanteDTO
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)
        Dim ds As DataSet

        Try
            sp.AgregarParametro("Accion", CONST_SELECT_ALL, System.Data.SqlDbType.VarChar)

            FillParameters(GrupoAportante, sp)

            ds = sp.ReturnDataSet()

            If ds.Tables.Count > 0 AndAlso ds.Tables(0).Rows.Count > 0 Then
                grupo = fillObject(ds.Tables(0).Rows(0))
            End If

        Catch ex As Exception
            Throw
        End Try

        Return grupo
    End Function

    <WebMethod()>
    Public Function GrupoAportanteIngresar(aportante As GrupoAportanteDTO) As Boolean
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)
        Try
            sp.AgregarParametro("Accion", CONST_INSERT, System.Data.SqlDbType.VarChar)

            aportante.GPA_Estado = DTO.Estados.CONST_ACTIVO

            FillParameters(aportante, sp)
            sp.ReturnDataSet()

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    <WebMethod()>
    Public Function GrupoAportanteModificar(aportante As GrupoAportanteDTO) As Boolean
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)
        Try
            ' ---TODO Data prueba---
            sp.AgregarParametro("Accion", CONST_UPDATE, System.Data.SqlDbType.VarChar)

            aportante.GPA_Estado = DTO.Estados.CONST_ACTIVO
            FillParameters(aportante, sp)

            sp.ReturnDataSet()

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    <WebMethod()>
    Public Function GrupoAportanteEliminar(aportante As GrupoAportanteDTO) As Boolean
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)
        Try
            ' ---TODO Data prueba---
            sp.AgregarParametro("Accion", CONST_DELETE, System.Data.SqlDbType.VarChar)
            aportante.GPA_Estado = DTO.Estados.CONST_ELIMINADO
            FillParameters(aportante, sp)

            sp.ReturnDataSet()

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Function fillObject(dataRow As DataRow) As GrupoAportanteDTO
        Dim grupoAportante As New GrupoAportanteDTO
        With grupoAportante

            .GPA_Id = dataRow("GPA_ID").ToString().Trim()
            .GPA_Descripcion = dataRow("GPA_Descripcion").ToString().Trim()
            .GPA_Estado = dataRow("GPA_Estado").ToString().Trim()
            .GPA_FechaIngreso = dataRow("GPA_Fecha_Ingreso").ToString().Trim()
            .GPA_UsuarioIngreso = dataRow("GPA_Usuario_Ingreso").ToString().Trim()
            .GPA_FechaModificacion = dataRow("GPA_Fecha_Modificacion").ToString().Trim()
            .GPA_UsuarioModificacion = dataRow("GPA_Usuario_Modificacion").ToString().Trim()

        End With
        Return grupoAportante
    End Function

    Private Sub FillParameters(grupoAportante As GrupoAportanteDTO, sp As DBSqlServer.SqlServer.StoredProcedure)
        sp.AgregarParametro("GpaId", grupoAportante.GPA_Id, System.Data.SqlDbType.Decimal)
        sp.AgregarParametro("GpaDescripcion", grupoAportante.GPA_Descripcion, System.Data.SqlDbType.VarChar)
        sp.AgregarParametro("GpaEstado", grupoAportante.GPA_Estado, System.Data.SqlDbType.Int)
        sp.AgregarParametro("GpaUsuario", grupoAportante.GPA_UsuarioIngreso, System.Data.SqlDbType.VarChar)

    End Sub

End Class