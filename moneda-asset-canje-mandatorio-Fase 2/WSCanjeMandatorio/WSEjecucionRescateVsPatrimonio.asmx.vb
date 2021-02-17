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
Public Class WSEjecucionRescateVsPatrimonio
    Inherits System.Web.Services.WebService

    Private Const SP_CRUD As String = "PRC_EjecucionRescateVsPatrimonioCRUD"

    Private Const CONST_INSERT As String = "INSERT"
    Private Const CONST_UPDATE As String = "UPDATE"
    Private Const CONST_DELETE As String = "DELETE"

    Private Const CONST_SELECT_ALL As String = "SELECT_ALL"
    Private Const CONST_SELECT_FILTRO As String = "SELECT_FILTRO"
    Private Const CONST_SELECT_ONE As String = "SELECT_ONE"
    Private Const CONST_SELECT_RELACIONES As String = "SELECT_RELACIONES"

    Private Const SP_CONSULTAS As String = "PRC_FondoSerieConsultas"

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function SelectFiltro(ejecucionDto As EjecucionRescateVsPatrimonioDTO) As List(Of EjecucionRescateVsPatrimonioDTO)
        Dim Lista As List(Of EjecucionRescateVsPatrimonioDTO) = New List(Of EjecucionRescateVsPatrimonioDTO)

        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)
        Dim ds As DataSet
        Dim aportanteNew As New EjecucionRescateVsPatrimonioDTO

        Try
            ' ---TODO Data prueba---
            sp.AgregarParametro("Accion", CONST_SELECT_FILTRO, System.Data.SqlDbType.VarChar)

            FillParameters(ejecucionDto, sp)

            ds = sp.ReturnDataSet()

            For Each dataRow As DataRow In ds.Tables(0).Rows
                Lista.Add(fillObject(dataRow))
            Next

        Catch ex As Exception
            Throw
        End Try

        Return Lista
    End Function


    Private Sub FillParameters(ejecucionDto As EjecucionRescateVsPatrimonioDTO, sp As DBSqlServer.SqlServer.StoredProcedure)
        sp.AgregarParametro("FnRut", ejecucionDto.FnRut, System.Data.SqlDbType.VarChar)

        If ejecucionDto.FechaEjecucion = Nothing Then
            sp.AgregarParametro("FechaEjecucion", Nothing, System.Data.SqlDbType.Date)
        Else
            sp.AgregarParametro("FechaEjecucion", ejecucionDto.FechaEjecucion, System.Data.SqlDbType.Date)
        End If

        sp.AgregarParametro("Descripcion", ejecucionDto.Descripcion, System.Data.SqlDbType.VarChar)
        sp.AgregarParametro("Estado", ejecucionDto.Estado, System.Data.SqlDbType.VarChar)
    End Sub

    Private Function fillObject(dataRow As DataRow) As EjecucionRescateVsPatrimonioDTO
        Dim ejecucionDto As EjecucionRescateVsPatrimonioDTO = New EjecucionRescateVsPatrimonioDTO

        With ejecucionDto
            ' ID, FN_RUT, FECHA_EJECUCION, DESCRIPCION, ESTADO
            .Id = dataRow("ID").ToString().Trim()
            .FnRut = dataRow("FN_RUT").ToString().Trim()
            .FechaEjecucion = dataRow("FECHA_EJECUCION").ToString().Trim()
            .Descripcion = dataRow("DESCRIPCION").ToString().Trim()
            .Estado = dataRow("ESTADO").ToString().Trim()
            .NombreFondo = dataRow("FN_Razon_Social").ToString().Trim()
        End With

        Return ejecucionDto
    End Function


End Class