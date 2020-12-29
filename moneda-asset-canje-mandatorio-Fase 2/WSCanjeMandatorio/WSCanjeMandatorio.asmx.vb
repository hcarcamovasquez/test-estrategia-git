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
Public Class WSCanjeMandatorio
    Inherits System.Web.Services.WebService

    Private Const CONST_INSERT As String = "INSERT"
    Private Const CONST_UPDATE As String = "UPDATE"
    Private Const CONST_DELETE As String = "DELETE"
    Private Const SP_APORTANTE_RELACION As String = "PRC_AportanteRelaciones"
    Private Const CONST_ACCION_RELACION As String = "PUEDE_BORRAR"
    Private Const CONST_SELECT_ALL As String = "SELECT_ALL"


    Private Function fillReporte(dataRow As DataRow) As ReporteFechaCorteDTO
        Dim reporte As New ReporteFechaCorteDTO
        With reporte
            .ADCV_Cantidad = If(dataRow("ADCV_Cantidad").ToString().Trim() = "", Nothing, dataRow("ADCV_Cantidad").ToString().Trim())
            .AP_Razon_Social = If(dataRow("AP_Razon_Social").ToString().Trim() = "", Nothing, dataRow("AP_Razon_Social").ToString().Trim())
            .AP_RUT = If(dataRow("AP_RUT").ToString().Trim() = "", Nothing, dataRow("AP_RUT").ToString().Trim())
            .CANJE = If(dataRow("CANJE").ToString().Trim() = "", Nothing, dataRow("CANJE").ToString().Trim())
            .FN_Razon_Social = If(dataRow("FN_Razon_Social").ToString().Trim() = "", Nothing, dataRow("FN_Razon_Social").ToString().Trim())
            .PR_Directo_Indirecto = If(dataRow("PR_Directo_Indirecto").ToString().Trim() = "", Nothing, dataRow("PR_Directo_Indirecto").ToString().Trim())
            .FS_Grupo = If(dataRow("FS_Grupo").ToString().Trim() = "", Nothing, dataRow("FS_Grupo").ToString().Trim())
            .GPA_Descripcion = If(dataRow("GPA_Descripcion").ToString().Trim() = "", Nothing, dataRow("GPA_Descripcion").ToString().Trim())
            .AP_RUT = If(dataRow("AP_RUT").ToString().Trim() = "", Nothing, dataRow("AP_RUT").ToString().Trim())
            .AP_Razon_Social = If(dataRow("AP_Razon_Social").ToString().Trim() = "", Nothing, dataRow("AP_Razon_Social").ToString().Trim())
            .FS_Nemotecnico = If(dataRow("FS_Nemotecnico").ToString().Trim() = "", Nothing, dataRow("FS_Nemotecnico").ToString().Trim())
            .FS_Moneda = If(dataRow("FS_Moneda").ToString().Trim() = "", Nothing, dataRow("FS_Moneda").ToString().Trim())
            .VCS_Valor = If(dataRow("VCS_Valor").ToString().Trim() = "", Nothing, dataRow("VCS_Valor").ToString().Trim())
            .ADCV_Cantidad = If(dataRow("ADCV_Cantidad").ToString().Trim() = "", Nothing, dataRow("ADCV_Cantidad").ToString().Trim())
            .RES_Cuotas = If(dataRow("RES_Cuotas").ToString().Trim() = "", Nothing, dataRow("RES_Cuotas").ToString().Trim())
            .Susc_Cuotas = If(dataRow("Susc_Cuotas").ToString().Trim() = "", Nothing, dataRow("Susc_Cuotas").ToString().Trim())
            .PR_Saldo_Cuotas = If(dataRow("PR_Saldo_Cuotas").ToString().Trim() = "", Nothing, dataRow("PR_Saldo_Cuotas").ToString().Trim())
            .PR_Monto = If(dataRow("PR_Monto").ToString().Trim() = "", Nothing, dataRow("PR_Monto").ToString().Trim())
            .PR_DescEstado = If(dataRow("PR_DescEstado").ToString().Trim() = "", Nothing, dataRow("PR_DescEstado").ToString().Trim())
            .SerieOptima = If(dataRow("SerieOptima").ToString().Trim() = "", Nothing, dataRow("SerieOptima").ToString().Trim())
            .C_AP_Nac_Ext = If(dataRow("C_AP_Nac_Ext").ToString().Trim() = "", Nothing, dataRow("C_AP_Nac_Ext").ToString().Trim())
            .C_AP_Calificado = If(dataRow("C_AP_Calificado").ToString().Trim() = "", Nothing, dataRow("C_AP_Calificado").ToString().Trim())
            .C_AP_Rel_MAM = If(dataRow("C_AP_Rel_MAM").ToString().Trim() = "", Nothing, dataRow("C_AP_Rel_MAM").ToString().Trim())
            .ContratoDistribucion = If(dataRow("ContratoDistribucion").ToString().Trim() = "", Nothing, dataRow("ContratoDistribucion").ToString().Trim())
            .C_AP_Limite = If(dataRow("C_AP_Limite").ToString().Trim() = "", Nothing, dataRow("C_AP_Limite").ToString().Trim())
            .C_Certificado = If(dataRow("C_Certificado").ToString().Trim() = "", Nothing, dataRow("C_Certificado").ToString().Trim())
            .C_AP_Final_I = If(dataRow("C_AP_Final_I").ToString().Trim() = "", Nothing, dataRow("C_AP_Final_I").ToString().Trim())
            .C_Cuotas_C = If(dataRow("C_Cuotas_C").ToString().Trim() = "", Nothing, dataRow("C_Cuotas_C").ToString().Trim())
            .C_Cuotas_Certificar = If(dataRow("C_Cuotas_Certificar").ToString().Trim() = "", Nothing, dataRow("C_Cuotas_Certificar").ToString().Trim())

        End With
        Return reporte
    End Function

    Private Sub FillParameters(listaFondo As List(Of FondoDTO), sp As DBSqlServer.SqlServer.StoredProcedure)
        Dim listaRut As String = ""
        Dim AuxlistaRut As String = ""

        For Each Fondo As FondoDTO In listaFondo
            AuxlistaRut = listaRut
            listaRut = AuxlistaRut + "," + Fondo.Rut.ToString()
        Next

        sp.AgregarParametro("ListaRutFondo", listaRut, System.Data.SqlDbType.VarChar)
    End Sub
End Class