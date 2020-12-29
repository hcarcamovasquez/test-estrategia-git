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
Public Class WSADCV
    Inherits System.Web.Services.WebService

    Private Const SP_ADCV_CONSULTAS As String = "PRC_ADCVConsultas"

    Private Const CONST_INSERT As String = "INSERT"
    Private Const CONST_UPDATE As String = "UPDATE"
    Private Const CONST_DELETE As String = "DELETE"
    Private Const CONST_SELECT_ALL As String = "SELECT_ALL"
    Private Const CONST_SELECT_FILTRO As String = "SELECT_FILTRO"
    Private Const CONST_SELECT_ONE As String = "SELECT_ONE"
    Private Const CONST_SELECT_RELACIONES As String = "SELECT_RELACIONES"

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function ADCVBuscarFiltro(ADCV As ADCVDTO) As List(Of ADCVDTO)
        Dim listaADCV As List(Of ADCVDTO) = New List(Of ADCVDTO)
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_ADCV_CONSULTAS)
        Dim ds As DataSet

        Try
            sp.AgregarParametro("Accion", CONST_SELECT_FILTRO, System.Data.SqlDbType.VarChar)

            FillParameters(ADCV, sp)


            ds = sp.ReturnDataSet()

            For Each dataRow As DataRow In ds.Tables(0).Rows
                listaADCV.Add(fillObject(dataRow))
            Next

        Catch ex As Exception
            Throw
        End Try

        Return listaADCV
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function GetADCV(ADCV As ADCVDTO) As ADCVDTO
        Dim ADCVRetorno As ADCVDTO = New ADCVDTO
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_ADCV_CONSULTAS)
        Dim ds As DataSet

        Try
            sp.AgregarParametro("Accion", CONST_SELECT_ONE, System.Data.SqlDbType.VarChar)

            FillParameters(ADCV, sp)

            ds = sp.ReturnDataSet()


            If ds.Tables.Count > 0 AndAlso ds.Tables(0).Rows.Count > 0 Then
                ADCVRetorno = fillObject(ds.Tables(0).Rows(0))
            Else
                ADCVRetorno = Nothing
            End If

        Catch ex As Exception
            Throw
        End Try

        Return ADCVRetorno
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function SelectCuotasDCV(ADCV As ADCVDTO) As ADCVDTO
        Dim ADCVRetorno As ADCVDTO = New ADCVDTO
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_ADCV_CONSULTAS)
        Dim ds As DataSet

        Try
            sp.AgregarParametro("Accion", "SELECT_CUOTAS_DCV", System.Data.SqlDbType.VarChar)
            FillParameters(ADCV, sp)


            ds = sp.ReturnDataSet()
            If ds.Tables.Count > 0 AndAlso ds.Tables(0).Rows.Count > 0 Then
                Dim dataRow As DataRow = ds.Tables(0).Rows(0)
                ADCVRetorno.ADCV_Cantidad = dataRow("ADCV_Cantidad")
            End If

        Catch ex As Exception
            Throw
        End Try

        Return ADCVRetorno
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function CuotaDCV(dcv As ADCVDTO) As List(Of ADCVDTO)
        Dim Lista As List(Of ADCVDTO) = New List(Of ADCVDTO)
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_ADCV_CONSULTAS)
        Dim ds As DataSet
        Try
            sp.AgregarParametro("Accion", "SELECT_BY", System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("AP_RUT", dcv.AP_RUT, System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("ADCV_Razon_Social", dcv.ADCV_Razon_Social, System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("FS_Nemotecnico", dcv.FS_Nemotecnico, System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("ADCV_Fecha", dcv.ADCV_Fecha, System.Data.SqlDbType.DateTime)

            ds = sp.ReturnDataSet()

            For Each dataRow As DataRow In ds.Tables(0).Rows
                Lista.Add(fillObject(dataRow))
            Next

        Catch ex As Exception
            Throw
        End Try

        Return Lista

    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function UltimaCuotaDCV(dcv As ADCVDTO) As List(Of ADCVDTO)
        Dim Lista As List(Of ADCVDTO) = New List(Of ADCVDTO)
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_ADCV_CONSULTAS)
        Dim ds As DataSet
        Try
            sp.AgregarParametro("Accion", "SELECT_BY_ULTIMO", System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("AP_RUT", dcv.AP_RUT, System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("ADCV_Razon_Social", dcv.ADCV_Razon_Social, System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("FS_Nemotecnico", dcv.FS_Nemotecnico, System.Data.SqlDbType.VarChar)

            ds = sp.ReturnDataSet()

            For Each dataRow As DataRow In ds.Tables(0).Rows
                Lista.Add(fillObject(dataRow))
            Next

        Catch ex As Exception
            Throw
        End Try

        Return Lista

    End Function


    Private Sub FillParameters(ADCV As ADCVDTO, sp As DBSqlServer.SqlServer.StoredProcedure)
        Dim ADCV_Fecha As Nullable(Of Date)
        If ADCV.ADCV_Fecha = "0001-01-01" Then
            ADCV_Fecha = Nothing
        Else
            ADCV_Fecha = ADCV.ADCV_Fecha
        End If

        sp.AgregarParametro("ADCV_ID", ADCV.ADCV_ID, System.Data.SqlDbType.Decimal)
        sp.AgregarParametro("ADCV_Fecha", ADCV_Fecha, System.Data.SqlDbType.DateTime)
        sp.AgregarParametro("AP_RUT", ADCV.AP_RUT, System.Data.SqlDbType.VarChar)
        sp.AgregarParametro("ADCV_Numero_Registro", ADCV.ADCV_Numero_Registro, System.Data.SqlDbType.Decimal)
        sp.AgregarParametro("ADCV_Razon_Social", ADCV.ADCV_Razon_Social, System.Data.SqlDbType.VarChar)
        sp.AgregarParametro("FS_Nemotecnico", ADCV.FS_Nemotecnico, System.Data.SqlDbType.VarChar)
        sp.AgregarParametro("ADCV_Cantidad", ADCV.ADCV_Cantidad, System.Data.SqlDbType.Decimal)
    End Sub

    Private Function fillObject(dataRow As DataRow) As ADCVDTO
        Dim ADCV As New ADCVDTO

        With ADCV
            .ADCV_ID = dataRow("ADCV_ID")
            .ADCV_Fecha = dataRow("ADCV_Fecha")
            .AP_RUT = dataRow("AP_RUT").ToString().Trim().ToString().Trim()
            .ADCV_Numero_Registro = dataRow("ADCV_Numero_Registro")
            .ADCV_Razon_Social = dataRow("ADCV_Razon_Social").ToString().Trim()
            .FS_Nemotecnico = dataRow("FS_Nemotecnico").ToString().Trim()
            .ADCV_Cantidad = dataRow("ADCV_Cantidad")

        End With
        Return ADCV
    End Function
End Class