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
Public Class WSConfigPentaho
    Inherits System.Web.Services.WebService

    Private Const SP_CRUD As String = "PRC_ConfigPentahoCRUD"
    Private Const CONST_SELECT_BY_ID As String = "SELECT_BY_ID"

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function GetPentahoPorId(pentaho As ConfigPentahoDTO) As ConfigPentahoDTO
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)
        Dim ds As DataSet
        Dim pentahoReturn As New ConfigPentahoDTO

        Try
            ' ---TODO Data prueba---
            sp.AgregarParametro("Accion", CONST_SELECT_BY_ID, System.Data.SqlDbType.VarChar)

            FillParameters(pentaho, sp)

            ds = sp.ReturnDataSet()

            If ds.Tables.Count > 0 AndAlso ds.Tables(0).Rows.Count > 0 Then
                pentahoReturn = FillObject(ds.Tables(0).Rows(0))
            End If

        Catch ex As Exception
            Throw
        End Try

        Return pentahoReturn
    End Function

    Private Sub FillParameters(pentahoDto As ConfigPentahoDTO, sp As DBSqlServer.SqlServer.StoredProcedure)
        sp.AgregarParametro("ID", pentahoDto.ID, System.Data.SqlDbType.Int)
        sp.AgregarParametro("Code", pentahoDto.Code, System.Data.SqlDbType.VarChar)
        sp.AgregarParametro("API_Repositorio", pentahoDto.API_Repositorio, System.Data.SqlDbType.VarChar)
        sp.AgregarParametro("API_Usuario", pentahoDto.API_Usuario, System.Data.SqlDbType.VarChar)
        sp.AgregarParametro("API_Password", pentahoDto.API_Password, System.Data.SqlDbType.VarChar)
        sp.AgregarParametro("API_WebProxy", pentahoDto.API_WebProxy, System.Data.SqlDbType.VarChar)
        sp.AgregarParametro("API_Url", pentahoDto.API_Url, System.Data.SqlDbType.VarChar)
        sp.AgregarParametro("API_Descripcion", pentahoDto.API_Descripcion, System.Data.SqlDbType.VarChar)
    End Sub

    Private Function FillObject(dataRow As DataRow) As ConfigPentahoDTO
        Dim pentahoDto As New ConfigPentahoDTO

        With pentahoDto
            .ID = dataRow("ID").ToString().Trim()
            .Code = dataRow("Code").ToString().Trim()
            .API_Repositorio = dataRow("API_Repositorio").ToString().Trim()
            .API_Usuario = dataRow("API_Usuario").ToString().Trim()
            .API_Password = dataRow("API_Password").ToString().Trim()
            .API_WebProxy = dataRow("API_WebProxy").ToString().Trim()
            .API_Url = dataRow("API_Url").ToString().Trim()
            .API_Descripcion = dataRow("API_Descripcion").ToString().Trim()

        End With
        Return pentahoDto
    End Function


End Class