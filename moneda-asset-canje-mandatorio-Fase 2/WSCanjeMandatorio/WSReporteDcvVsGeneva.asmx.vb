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
Public Class WSReporteDcvVsGeneva
    Inherits System.Web.Services.WebService

    Private Const SP_CRUD As String = "PRC_ConsultaReporteDCVvsGeneva"

    Private Const CONST_GENERA_INFORME As String = "SELECT_ALL"

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function GetInformeGeneva(ejecucionDto As ReporteDcvVsGenevaDTO) As List(Of ReporteDcvVsGenevaDTO)
        Dim Lista As List(Of ReporteDcvVsGenevaDTO) = New List(Of ReporteDcvVsGenevaDTO)

        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)
        Dim ds As DataSet
        Dim aportanteNew As New ReporteDcvVsGenevaDTO

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


    Private Sub FillParameters(ejecucionDto As ReporteDcvVsGenevaDTO, sp As DBSqlServer.SqlServer.StoredProcedure)
        sp.AgregarParametro("ID_Grupo", ejecucionDto.ID_Grupo, System.Data.SqlDbType.Int)
        sp.AgregarParametro("Fecha_DCV", ejecucionDto.Fecha_DCV, System.Data.SqlDbType.Date)
        sp.AgregarParametro("Fecha_VC", ejecucionDto.Fecha_VC, System.Data.SqlDbType.Date)
        sp.AgregarParametro("DCV_Nemo", ejecucionDto.DCV_Nemo, System.Data.SqlDbType.VarChar)
        sp.AgregarParametro("DCV_Cuotas", ejecucionDto.DCV_Cuotas, System.Data.SqlDbType.Decimal)
        sp.AgregarParametro("GNV_Nemo", ejecucionDto.GNV_Nemo, System.Data.SqlDbType.VarChar)
        sp.AgregarParametro("GNV_Clase", ejecucionDto.GNV_Clase, System.Data.SqlDbType.VarChar)
        sp.AgregarParametro("GNV_Cuotas", ejecucionDto.GNV_Cuotas, System.Data.SqlDbType.Decimal)
        sp.AgregarParametro("TRS_Rescates", ejecucionDto.TRS_Rescates, System.Data.SqlDbType.VarChar)
        sp.AgregarParametro("TRS_Suscripciones", ejecucionDto.TRS_Suscripciones, System.Data.SqlDbType.Decimal)
        sp.AgregarParametro("TRS_Canje", ejecucionDto.TRS_Canje, System.Data.SqlDbType.Decimal)
        sp.AgregarParametro("Recompra", ejecucionDto.Recompra, System.Data.SqlDbType.Decimal)
        sp.AgregarParametro("Total", ejecucionDto.Total, System.Data.SqlDbType.Decimal)
        sp.AgregarParametro("Diferencia", ejecucionDto.Diferencia, System.Data.SqlDbType.Decimal)
        sp.AgregarParametro("Observaciones", ejecucionDto.Observaciones, System.Data.SqlDbType.VarChar)

    End Sub

    Private Function fillObject(dataRow As DataRow) As ReporteDcvVsGenevaDTO
        Dim ejecucionDto As ReporteDcvVsGenevaDTO = New ReporteDcvVsGenevaDTO

        With ejecucionDto
            .ID_Grupo = dataRow("ID_Grupo").ToString().Trim()
            .Fecha_DCV = dataRow("Fecha_DCV").ToString().Trim()
            .Fecha_VC = dataRow("Fecha_VC").ToString().Trim()
            .DCV_Nemo = dataRow("DCV_Nemo").ToString().Trim()
            .DCV_Cuotas = dataRow("DCV_Cuotas").ToString().Trim()
            .GNV_Nemo = dataRow("GNV_Nemo").ToString().Trim()
            .GNV_Clase = dataRow("GNV_Clase").ToString().Trim()
            .GNV_Cuotas = dataRow("GNV_Cuotas").ToString().Trim()
            .TRS_Rescates = dataRow("TRS_Rescates").ToString().Trim()
            .TRS_Suscripciones = dataRow("TRS_Suscripciones").ToString().Trim()
            .TRS_Canje = dataRow("TRS_Canje").ToString().Trim()
            .Recompra = dataRow("Recompra").ToString().Trim()
            .Total = dataRow("Total").ToString().Trim()
            .Diferencia = dataRow("Diferencia").ToString().Trim()
            .Observaciones = dataRow("Observaciones").ToString().Trim()

        End With

        Return ejecucionDto
    End Function
End Class