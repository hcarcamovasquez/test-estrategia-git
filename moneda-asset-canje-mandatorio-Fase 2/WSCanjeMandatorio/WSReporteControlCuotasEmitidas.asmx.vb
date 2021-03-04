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
Public Class WSReporteControlCuotasEmitidas
    Inherits System.Web.Services.WebService

    Private Const SP_CRUD As String = "PRC_REPORTECONTROLCUOTASEMITIDAS"

    Private Const CONST_GENERA_INFORME As String = "GENERA_INFORME"


    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function SelectFiltro(ejecucionDto As ReporteControlCuotasEmitidasDTO) As List(Of ReporteControlCuotasEmitidasDTO)
        Dim Lista As List(Of ReporteControlCuotasEmitidasDTO) = New List(Of ReporteControlCuotasEmitidasDTO)

        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)
        Dim ds As DataSet
        Dim aportanteNew As New ReporteControlCuotasEmitidasDTO

        Try
            ' ---TODO Data prueba---
            sp.AgregarParametro("Accion", CONST_GENERA_INFORME, System.Data.SqlDbType.VarChar)

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


    Private Sub FillParameters(ejecucionDto As ReporteControlCuotasEmitidasDTO, sp As DBSqlServer.SqlServer.StoredProcedure)
        sp.AgregarParametro("fsNemotecnico", ejecucionDto.FsNemotecnico, System.Data.SqlDbType.VarChar)
    End Sub

    Private Function fillObject(dataRow As DataRow) As ReporteControlCuotasEmitidasDTO
        Dim ejecucionDto As ReporteControlCuotasEmitidasDTO = New ReporteControlCuotasEmitidasDTO

        With ejecucionDto
            .FsNemotecnico = dataRow("FS_Nemotecnico").ToString().Trim()
            .FsMoneda = dataRow("FS_Moneda").ToString().Trim()
            .FnFechaEmision = IIf(dataRow("FN_Fecha_Emision").ToString().Trim() = "", Nothing, dataRow("FN_Fecha_Emision").ToString().Trim())
            .FnFechaVencimiento = IIf(dataRow("FN_Fecha_Vencimiento").ToString().Trim() = "", Nothing, dataRow("FN_Fecha_Vencimiento").ToString().Trim())
            .FnCuotasEmitidas = dataRow("FN_Cuotas_Emitidas").ToString().Trim()
            .CuotasDisponibles = dataRow("Cuotas_Disponibles").ToString().Trim()
            .Acumulado = dataRow("Acumulado").ToString().Trim()
            .Anno_En_Curso = dataRow("Anno_En_Curso").ToString().Trim()
            .PorcentajeUltimaEmision = dataRow("Porcentaje_Ultima_Emision").ToString().Trim()
            .TotalSuscritasUltimaEmision = dataRow("Total_Suscritas_Ultima_Emision").ToString().Trim()
            .TotalCuotasSuscritaspagadas = dataRow("Total_Cuotas_suscritas_pagadas").ToString().Trim()
            .PorcentajeTotalCuotasSuscritasPagadas = dataRow("Porcentaje_Total_Cuotas_Suscritas_pagadas").ToString().Trim()
        End With

        Return ejecucionDto
    End Function


End Class