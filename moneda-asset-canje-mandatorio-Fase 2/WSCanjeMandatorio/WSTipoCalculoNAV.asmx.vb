Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel
Imports DTO
Imports System.Web.Script.Services

' Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente.
' <System.Web.Script.Services.ScriptService()> _
<System.Web.Services.WebService(Namespace:="http://tempuri.org/")>
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)>
<ToolboxItem(False)>
Public Class WSTipoCalculoNav
    Inherits System.Web.Services.WebService

    Private Const SP_CRUD As String = "PRC_TipoCalculoNAVCRUD"

    Private Const CONST_INSERT As String = "INSERT"
    Private Const CONST_UPDATE As String = "UPDATE"
    Private Const CONST_DELETE As String = "DELETE"
    Private Const CONST_SELECT_ALL As String = "SELECT_ALL"
    Private Const CONST_SELECT_ONE As String = "SELECT_ONE"
    Private Const CONST_SELECT_FILTRO As String = "SELECT_FILTRO"

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function TipoCambioModificar(TipoCalculoNav As TipoCalculoNavDTO) As Boolean
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)

        Try

            sp.AgregarParametro("Accion", CONST_INSERT, System.Data.SqlDbType.VarChar)

            FillParameters(TipoCalculoNav, sp)

            sp.ReturnDataSet()

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Shared Sub FillParameters(TipoCalculoNav As TipoCalculoNavDTO, sp As DBSqlServer.SqlServer.StoredProcedure)
        sp.AgregarParametro("ID", TipoCalculoNav.ID, System.Data.SqlDbType.Decimal)
        sp.AgregarParametro("TipoTransaccion", TipoCalculoNav.TipoTransaccion, System.Data.SqlDbType.VarChar)
        sp.AgregarParametro("TipoCalculo", TipoCalculoNav.TipoCalculo, System.Data.SqlDbType.VarChar)
    End Sub
End Class