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
Public Class WSFechas
    Inherits System.Web.Services.WebService

    Private Const SP_CRUD As String = "PRC_DiasFestivosCRUD"

    Private Const CONST_DIA_HABIL_SIGUIENTE As String = "DIA_HABIL_SIGUIENTE"
    Private Const CONST_DIA_HABIL_ANTERIOR As String = "DIA_HABIL_ANTERIOR"
    Private Const CONST_ES_DIA_HABIL As String = "ES_DIA_HABIL"
    Private Const CONST_SUMAR_DIAS_FECHA As String = "SUMA_DIAS_A_FECHA"


    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function ValidaDiaHabil(fecha As FechasDTO) As Integer
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)
        Dim ds As DataSet

        Try
            ' ---TODO Data prueba---
            sp.AgregarParametro("Accion", CONST_ES_DIA_HABIL, System.Data.SqlDbType.VarChar)

            FillParameters(fecha, sp)
            ds = sp.ReturnDataSet()

            If ds.Tables.Count > 0 AndAlso ds.Tables(0).Rows.Count > 0 Then
                Dim dataRow As DataRow = ds.Tables(0).Rows(0)
                Return dataRow("EsDiaHabil").ToString()
            Else
                Return 99
            End If

        Catch ex As Exception
            Return 99
        End Try
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function getHabilSiguiente(fecha As FechasDTO) As Date
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)
        Dim ds As DataSet

        Try
            ' ---TODO Data prueba---
            sp.AgregarParametro("Accion", CONST_DIA_HABIL_SIGUIENTE, System.Data.SqlDbType.VarChar)
            FillParameters(fecha, sp)

            ds = sp.ReturnDataSet()

            If ds.Tables.Count > 0 AndAlso ds.Tables(0).Rows.Count > 0 Then
                Dim dataRow As DataRow = ds.Tables(0).Rows(0)
                Return dataRow("Fecha_Resultado")
            Else
                Return New Date
            End If

        Catch ex As Exception
            Return New Date
        End Try
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function SumarDiasAFecha(fecha As FechasDTO) As Date
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)
        Dim ds As DataSet

        Try
            ' ---TODO Data prueba---
            sp.AgregarParametro("Accion", CONST_SUMAR_DIAS_FECHA, System.Data.SqlDbType.VarChar)

            FillParameters(fecha, sp)

            ds = sp.ReturnDataSet()

            If ds.Tables.Count > 0 AndAlso ds.Tables(0).Rows.Count > 0 Then
                Dim dataRow As DataRow = ds.Tables(0).Rows(0)
                Return dataRow("Fecha_Resultado")
            Else
                Return New Date
            End If

        Catch ex As Exception
            Return New Date
        End Try
    End Function


    Private Sub FillParameters(dtoFecha As FechasDTO, sp As DBSqlServer.SqlServer.StoredProcedure)
        sp.AgregarParametro("dfAnio", dtoFecha.Anno, System.Data.SqlDbType.Int)
        sp.AgregarParametro("dfMes", dtoFecha.Mes, System.Data.SqlDbType.Int)
        sp.AgregarParametro("dfDia", dtoFecha.Dia, System.Data.SqlDbType.Int)
        sp.AgregarParametro("dfPais", dtoFecha.DF_PAIS, System.Data.SqlDbType.VarChar)
        sp.AgregarParametro("dfDiasASumar", dtoFecha.DiasASumar, System.Data.SqlDbType.Int)
        sp.AgregarParametro("dfDiasHabiles", dtoFecha.DiasCorridos, System.Data.SqlDbType.Int)
    End Sub


    Public Function fillObject(dataRow As DataRow) As FechasDTO
        Dim dtoFecha As New FechasDTO
        With dtoFecha
            .Anno = dataRow("FN_RUT").ToString().Trim()
            .DF_PAIS = dataRow("FS_Nemotecnico").ToString().Trim()
            .Dia = dataRow("FS_Nombre_Corto").ToString().Trim()
            .Mes = dataRow("FS_Remuneracion").ToString().Trim()
        End With
        Return dtoFecha

    End Function


End Class