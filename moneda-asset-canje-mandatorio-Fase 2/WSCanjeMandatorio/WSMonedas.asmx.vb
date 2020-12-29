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
Public Class WSMonedas
    Inherits System.Web.Services.WebService

    Private Const SP_CRUD As String = "PRC_MonedasCRUD"

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function SelectAll() As List(Of MonedasDTO)
        Dim ListaMonedas As List(Of MonedasDTO) = New List(Of MonedasDTO)
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)
        Dim ds As DataSet
        Dim aportanteNew As New MonedasDTO

        Try
            ' ---TODO Data prueba---
            sp.AgregarParametro("Accion", "SELECT_ALL", System.Data.SqlDbType.VarChar)

            ds = sp.ReturnDataSet()

            For Each dataRow As DataRow In ds.Tables(0).Rows
                ListaMonedas.Add(FillObject(dataRow))
            Next

        Catch ex As Exception
            Throw
        End Try

        Return ListaMonedas
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function Guardar(moneda As MonedasDTO) As Boolean
        Dim ListaMonedas As List(Of MonedasDTO) = New List(Of MonedasDTO)
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)
        Dim ds As DataSet
        Dim aportanteNew As New MonedasDTO

        Try
            ' ---TODO Data prueba---
            sp.AgregarParametro("Accion", "GUARDAR", System.Data.SqlDbType.VarChar)
            FillParameters(moneda, sp)

            ds = sp.ReturnDataSet()

            Return (sp.FilasAfectas > 1)
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function GetMonedasWS(tokken As String) As Boolean




        Return True
    End Function




    Private Sub FillParameters(moneda As MonedasDTO, sp As DBSqlServer.SqlServer.StoredProcedure)
        sp.AgregarParametro("MNCodigo", moneda.MNCodigo, System.Data.SqlDbType.VarChar)
        sp.AgregarParametro("MNDescripcion", moneda.MNDescripcion, System.Data.SqlDbType.VarChar)
        sp.AgregarParametro("MNTipo", moneda.MNTipo, System.Data.SqlDbType.VarChar)
        sp.AgregarParametro("MNMoneda", moneda.MNMoneda, System.Data.SqlDbType.Int)
        sp.AgregarParametro("PAICodigo", moneda.PAICodigo, System.Data.SqlDbType.VarChar)
        sp.AgregarParametro("MNCodCmf", moneda.MNCodCmf, System.Data.SqlDbType.VarChar)
        sp.AgregarParametro("MNFrecuente", moneda.MNFrecuente, System.Data.SqlDbType.Int)
    End Sub


    Private Function FillObject(dataRow As DataRow) As MonedasDTO
        Dim fondo As New MonedasDTO

        With fondo
            .MNCodigo = dataRow("MN_Codigo").ToString().Trim()
            .MNDescripcion = dataRow("MN_Descripcion").ToString().Trim()
            .MNTipo = dataRow("MN_Tipo").ToString().Trim()
            .MNMoneda = dataRow("MN_Moneda").ToString().Trim()
            .PAICodigo = dataRow("PAI_Codigo").ToString().Trim()
            .MNCodCmf = dataRow("MN_CodCmf").ToString().Trim()
            .MNFrecuente = dataRow("MN_Frecuente").ToString().Trim()
        End With
        Return fondo
    End Function
End Class