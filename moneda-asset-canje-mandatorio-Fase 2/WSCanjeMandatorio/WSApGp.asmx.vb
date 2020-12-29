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
Public Class WSApGp
    Inherits System.Web.Services.WebService

    Private Const SP_CRUD_APGP As String = "PRC_ApGpCRUD"
    Private Const CONST_DELETE_ONE = "DELETE_ONE"

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function DeleteAportanteEnGrupo(aportanteXGrupo As AportantesXGrupoDTO) As Boolean
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD_APGP)
        Dim ds As DataSet
        Try
            sp.AgregarParametro("Accion", CONST_DELETE_ONE, System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("APRut", aportanteXGrupo.RutAportante, System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("GpaId", aportanteXGrupo.IdGrupo, System.Data.SqlDbType.Int)
            ds = sp.ReturnDataSet()
            Return True
        Catch ex As Exception
            Throw
        End Try

    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function DeleteGrupoAll(grupo As GrupoAportanteDTO) As Integer
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD_APGP)
        Dim ds As DataSet
        Try
            sp.AgregarParametro("Accion", "DELETE_ALL", System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("GpaId", grupo.GPA_Id, System.Data.SqlDbType.Int)
            ds = sp.ReturnDataSet()

            Return sp.FilasAfectas
        Catch ex As Exception
            Return 0
        End Try

    End Function

End Class