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
Public Class WSPatrimonio
    Inherits System.Web.Services.WebService

    Private Const SP_CRUD As String = "PRC_PatrimonioCRUD"
    Private Const SP_CONSULTAR As String = "PRC_CertificadoConsultar"


    Private Const SP_CERTIFICADOBUSCAR As String = "PRC_CertificadoBuscar"
    Private Const CONST_INSERT As String = "INSERT"
    Private Const CONST_UPDATE As String = "UPDATE"
    Private Const CONST_DELETE As String = "DELETE"
    Private Const SP_CERTIFICADO_RELACION As String = "PRC_CertificadoRelaciones"
    Private Const CONST_ACCION_RELACION As String = "PUEDE_BORRAR"


    Private Const CONST_SELECT_ALL As String = "SELECT_ALL"
    Private CONST_SELECT_FILTRO As String = "SELECT_FILTRO"
    Private Const CONST_SELECT_ONE As String = "SELECT_ONE"
    Private Const CONST_SELECT_RELACIONES As String = "SELECT_RELACIONES"

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function GetPatrimonio(Patrimonio As PatrimonioDTO) As PatrimonioDTO
        Dim PatrimonioRetorno As PatrimonioDTO = New PatrimonioDTO
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)
        Dim ds As DataSet

        Try
            sp.AgregarParametro("Accion", CONST_SELECT_ONE, System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("IDFONDO", Patrimonio.IDFONDO, System.Data.SqlDbType.VarChar)


            ds = sp.ReturnDataSet()
            If ds.Tables.Count > 0 AndAlso ds.Tables(0).Rows.Count > 0 Then
                PatrimonioRetorno = fillPatrimonio(ds.Tables(0).Rows(0))
            Else
                PatrimonioRetorno = Nothing
            End If

        Catch ex As Exception
            Throw
        End Try

        Return PatrimonioRetorno
    End Function

    Private Function fillPatrimonio(dataRow As DataRow) As PatrimonioDTO
        Dim Patrimonio As New PatrimonioDTO
        With Patrimonio
            .DFECHA = dataRow("DFECHA").ToString().Trim()
            .IDFONDO = dataRow("IDFONDO").ToString().Trim()
            .IDMONEDA = dataRow("IDMONEDA").ToString().Trim()
            .NPATRIMONIO = dataRow("NPATRIMONIO").ToString().Trim()
            .DTIMESTAMP = dataRow("DTIMESTAMP").ToString().Trim()
        End With
        Return Patrimonio
    End Function

End Class