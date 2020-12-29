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
Public Class WSProcesoDetalle
    Inherits System.Web.Services.WebService

    Private Const CONST_SP_PROCESO_DETALLE As String = "SP_PROCESO_DETALLES"
    Private Const CONST_INSERT As String = "INSERT"
    Private Const CONST_UPDATE_BY_ID As String = "UPDATE_BY_ID"
    Private Const CONST_DELETE_BY_ID As String = "DELETE_BY_ID"
    Private Const CONST_SELECT_BY_ID_PADRE As String = "SELECT_BY_ID_PADRE"

    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function InsertProcesoDetalle(prcesoDetalle As ProcesoDetalleDTO) As Boolean
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(CONST_SP_PROCESO_DETALLE)

        Try
            sp.AgregarParametro("Accion", CONST_INSERT, System.Data.SqlDbType.VarChar)

            FillParameters(prcesoDetalle, sp)

            sp.ReturnDataSet()

            Return True
        Catch ex As Exception
            Throw
        End Try
        Return True
    End Function

    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function UpdateProcesoDetalle(procesoDetalle As ProcesoDetalleDTO) As Boolean
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(CONST_SP_PROCESO_DETALLE)

        Try
            sp.AgregarParametro("Accion", CONST_UPDATE_BY_ID, System.Data.SqlDbType.VarChar)

            FillParameters(procesoDetalle, sp)

            sp.ReturnDataSet()

            Return True
        Catch ex As Exception
            Throw
        End Try
        Return True
    End Function

    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function DeleteProcesoDetalleById(procesoDetalle As ProcesoDetalleDTO) As Boolean
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(CONST_SP_PROCESO_DETALLE)

        Try
            sp.AgregarParametro("Accion", CONST_DELETE_BY_ID, System.Data.SqlDbType.VarChar)

            FillParameters(procesoDetalle, sp)

            sp.ReturnDataSet()

            Return True
        Catch ex As Exception
            Throw
        End Try
        Return True
    End Function

    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function SelectProcesoDetalle(procesoDetalle As ProcesoDetalleDTO) As List(Of ProcesoDetalleDTO)
        Dim ListaprocesoDetalle As List(Of ProcesoDetalleDTO) = New List(Of ProcesoDetalleDTO)
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(CONST_SP_PROCESO_DETALLE)
        Dim ds As DataSet

        Try
            sp.AgregarParametro("Accion", CONST_SELECT_BY_ID_PADRE, System.Data.SqlDbType.VarChar)
            FillParameters(procesoDetalle, sp)

            ds = sp.ReturnDataSet()
            If ds.Tables.Count > 0 Then
                For Each dataRow As DataRow In ds.Tables(0).Rows
                    ListaprocesoDetalle.Add(FillObject(dataRow))
                Next
            End If

        Catch ex As Exception
            Throw
        End Try

        Return ListaprocesoDetalle
    End Function

    Private Function FillObject(dataRow As DataRow) As ProcesoDetalleDTO
        Dim procesoDetalle As New ProcesoDetalleDTO
        With procesoDetalle

            .PRD_ID = dataRow("PRD_ID").ToString().Trim()
            .PR_ID = dataRow("PR_ID").ToString().Trim()
            .FS_Nemotecnico = dataRow("FS_Nemotecnico").ToString().Trim()
            .PRD_CuotasEntrante = dataRow("PRD_CuotasEntrante").ToString().Trim()
            .PRD_MontoEntrante = dataRow("PRD_MontoEntrante").ToString().Trim()
            .PRD_MontoEntranteCLP = dataRow("PRD_MontoEntranteCLP").ToString().Trim()
            .PRD_Observaciones = dataRow("PRD_Observaciones").ToString().Trim()
            .PRD_NAVEntrante = dataRow("PRD_NAVEntrante").ToString().Trim()
            .PRD_NAVEntranteCLP = dataRow("PRD_NAVEntranteCLP").ToString().Trim()

            .PRD_Factor = dataRow("PRD_Factor").ToString().Trim()
            .PRD_CuotasSalientes = String.Format("{0:N0}", Decimal.Parse(dataRow("PRD_CuotasSalientes").ToString().Trim()))
            .PRD_MontoSaliente = dataRow("PRD_MontoSaliente").ToString().Trim()
            .PRD_Diferencia = dataRow("PRD_Diferencia").ToString().Trim()
            .PRD_DiferenciaCLP = dataRow("PRD_DiferenciaCLP").ToString().Trim()
            .FS_MonedaEntrante = dataRow("FS_MonedaEntrante").ToString().Trim()
            .PR_DescEstado = dataRow("PR_DescEstado").ToString().Trim()
            .C_AP_Nac_Ext = dataRow("C_AP_Nac_Ext").ToString().Trim()
            .C_AP_Calificado = dataRow("C_AP_Calificado").ToString().Trim()
            .C_AP_Rel_MAM = dataRow("C_AP_Rel_MAM").ToString().Trim()
            .C_AP_Limite = dataRow("C_AP_Limite").ToString().Trim()
            .C_Certificado = dataRow("C_Certificado").ToString().Trim()
            .C_AP_Final_I = dataRow("C_AP_Final_I").ToString().Trim()
            .C_Cuotas_C = dataRow("C_Cuotas_C").ToString().Trim()
            .C_Cuotas_Certificar = dataRow("C_Cuotas_Certificar").ToString().Trim()
            .PRD_Accion = dataRow("prd_Accion").ToString().Trim()
            .PRCuotasSalientes = dataRow("PRD_CuotasSalientes").ToString().Trim()

            .NemoSeleccionado = dataRow("FS_Nemotecnico").ToString().Trim()
            .PRD_MontoSalienteCLP = dataRow("PRD_MontoSalienteCLP").ToString().Trim()


        End With

        Return procesoDetalle
    End Function

    Private Sub FillParameters(procesoDetalle As ProcesoDetalleDTO, sp As DBSqlServer.SqlServer.StoredProcedure)
        sp.AgregarParametro("PRD_ID", procesoDetalle.PRD_ID, System.Data.SqlDbType.Int)
        sp.AgregarParametro("PR_ID", procesoDetalle.PR_ID, System.Data.SqlDbType.Int)
        sp.AgregarParametro("FS_Nemotecnico", procesoDetalle.FS_Nemotecnico, System.Data.SqlDbType.VarChar)
        sp.AgregarParametro("PRD_CuotasEntrante", procesoDetalle.PRD_CuotasEntrante, System.Data.SqlDbType.Int)
        sp.AgregarParametro("PRD_NAVEntrante", procesoDetalle.PRD_NAVEntrante, System.Data.SqlDbType.Decimal)
        sp.AgregarParametro("PRD_NAVEntranteCLP", procesoDetalle.PRD_NAVEntranteCLP, System.Data.SqlDbType.Decimal)
        sp.AgregarParametro("PRD_MontoEntrante", procesoDetalle.PRD_MontoEntrante, System.Data.SqlDbType.Decimal)
        sp.AgregarParametro("PRD_MontoEntranteCLP", procesoDetalle.PRD_MontoEntranteCLP, System.Data.SqlDbType.Decimal)
        sp.AgregarParametro("PRD_Observaciones", procesoDetalle.PRD_Observaciones, System.Data.SqlDbType.VarChar)
        sp.AgregarParametro("PRD_Factor", procesoDetalle.PRD_Factor, System.Data.SqlDbType.Decimal)
        sp.AgregarParametro("PRD_CuotasSalientes", procesoDetalle.PRD_CuotasSalientes, System.Data.SqlDbType.Decimal)
        sp.AgregarParametro("PRD_MontoSaliente", procesoDetalle.PRD_MontoSaliente, System.Data.SqlDbType.Decimal)
        sp.AgregarParametro("PRD_MontoSalienteCLP", procesoDetalle.PRD_MontoSalienteCLP, System.Data.SqlDbType.Decimal)

        sp.AgregarParametro("PRD_Diferencia", procesoDetalle.PRD_Diferencia, System.Data.SqlDbType.Decimal)
        sp.AgregarParametro("PRD_DiferenciaCLP", procesoDetalle.PRD_DiferenciaCLP, System.Data.SqlDbType.Decimal)
        sp.AgregarParametro("FS_MonedaEntrante", procesoDetalle.FS_MonedaEntrante, System.Data.SqlDbType.VarChar)
        sp.AgregarParametro("PR_DescEstado", procesoDetalle.PR_DescEstado, System.Data.SqlDbType.VarChar)
        sp.AgregarParametro("C_AP_Nac_Ext", procesoDetalle.C_AP_Nac_Ext, System.Data.SqlDbType.VarChar)
        sp.AgregarParametro("C_AP_Calificado", procesoDetalle.C_AP_Calificado, System.Data.SqlDbType.VarChar)
        sp.AgregarParametro("C_AP_Rel_MAM", procesoDetalle.C_AP_Rel_MAM, System.Data.SqlDbType.VarChar)
        sp.AgregarParametro("C_AP_Limite", procesoDetalle.C_AP_Limite, System.Data.SqlDbType.VarChar)
        sp.AgregarParametro("C_Certificado", procesoDetalle.C_Certificado, System.Data.SqlDbType.VarChar)
        sp.AgregarParametro("C_AP_Final_I", procesoDetalle.C_AP_Final_I, System.Data.SqlDbType.VarChar)
        sp.AgregarParametro("C_Cuotas_C", procesoDetalle.C_Cuotas_C, System.Data.SqlDbType.Int)
        sp.AgregarParametro("C_Cuotas_Certificar", procesoDetalle.C_Cuotas_Certificar, System.Data.SqlDbType.Int)
        sp.AgregarParametro("PRD_ACCION", procesoDetalle.PRD_Accion, System.Data.SqlDbType.VarChar)


    End Sub

End Class