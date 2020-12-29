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
Public Class WSDocumento
    Inherits System.Web.Services.WebService

    Private Const SP_CRUD As String = "PRC_DocumentoCRUD"
    Private Const CONST_INSERT As String = "INSERT"
    Private Const CONST_UPDATE As String = "UPDATE"
    Private Const CONST_UPDATE_NUEVO As String = "UPDATE_NUEVO"
    Private Const CONST_DELETE As String = "DELETE"
    Private Const CONST_SELECT_ALL As String = "SELECT_ALL"
    Private Const CONST_SELECT_ONE As String = "SELECT_ONE"
    Private Const CONST_SELECT_FILTRO As String = "CONSULTAR_POR_FILTRO"

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function ConsultarDatosDocumento(Documento As DocumentoDTO) As DocumentoDTO
        Dim DocumentoRetorno As DocumentoDTO = New DocumentoDTO
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)
        Dim ds As DataSet

        Try
            sp.AgregarParametro("Accion", "CONSULTA_DOCUMENTO", System.Data.SqlDbType.VarChar)

            FillParameters(Documento, sp)

            ds = sp.ReturnDataSet()
            If ds.Tables.Count > 0 AndAlso ds.Tables(0).Rows.Count > 0 Then
                DocumentoRetorno = fillDocumentos(ds.Tables(0).Rows(0))
            Else
                DocumentoRetorno = Nothing
            End If

        Catch ex As Exception
            Throw
        End Try

        Return DocumentoRetorno
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function FNModificar(Documento As DocumentoDTO) As Boolean
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)

        Try
            ' ---TODO Data prueba---
            sp.AgregarParametro("Accion", CONST_UPDATE, System.Data.SqlDbType.VarChar)

            FillParameters(Documento, sp)

            sp.ReturnDataSet()

            Return (sp.FilasAfectas > 0)
        Catch ex As Exception
            Return False
        End Try
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function FNModificarNumeroNuevo(Documento As DocumentoDTO) As Boolean
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)

        Try
            ' ---TODO Data prueba---
            sp.AgregarParametro("Accion", CONST_UPDATE_NUEVO, System.Data.SqlDbType.VarChar)

            FillParameters(Documento, sp)

            sp.ReturnDataSet()

            Return (sp.FilasAfectas > 0)
        Catch ex As Exception
            Return False
        End Try
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function ValidaNumeroDocumento(Documento As DocumentoDTO) As Integer
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)
        Dim ds As DataSet
        Try
            sp.AgregarParametro("Accion", "DOCUMENTO_EXISTE", System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("DOC_NumeroSiguiente", Documento.NumeroSiguiente, System.Data.SqlDbType.Decimal)

            ds = sp.ReturnDataSet()

            If ds.Tables.Count > 0 AndAlso ds.Tables(0).Rows.Count > 0 Then
                Dim dataRow As DataRow = ds.Tables(0).Rows(0)
                Return dataRow("ExisteDocumento")
            Else
                Return Constantes.CONST_ERROR_BBDD
            End If

        Catch ex As Exception
            Return Constantes.CONST_ERROR_BBDD
        End Try
    End Function

    Private Sub FillParameters(Documento As DocumentoDTO, sp As DBSqlServer.SqlServer.StoredProcedure)
        sp.AgregarParametro("DOC_NumeroActual", Documento.NumeroActual, System.Data.SqlDbType.Decimal)
        sp.AgregarParametro("DOC_NumeroAnterior", Documento.NumeroAnterior, System.Data.SqlDbType.Decimal)
        sp.AgregarParametro("DOC_NumeroSiguiente", Documento.NumeroSiguiente, System.Data.SqlDbType.Decimal)
    End Sub


    Private Function fillDocumentos(dataRow As DataRow) As DocumentoDTO
        Dim Documento As New DocumentoDTO

        With Documento
            .NumeroActual = dataRow("DOC_NumeroActual").ToString().Trim()
            .NumeroAnterior = dataRow("DOC_NumeroAnterior").ToString().Trim()
            .NumeroSiguiente = dataRow("DOC_NumeroSiguiente").ToString().Trim()
        End With
        Return Documento

    End Function



End Class