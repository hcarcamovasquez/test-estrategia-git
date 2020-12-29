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
Public Class WSCertificados
    Inherits System.Web.Services.WebService

    Private Const SP_CRUD As String = "PRC_CertificadoCRUD"
    Private Const SP_CONSULTAR As String = "PRC_CertificadoConsultar"


    Private Const SP_CERTIFICADOBUSCAR As String = "PRC_CertificadoBuscar"
    Private Const CONST_INSERT As String = "INSERT"
    Private Const CONST_UPDATE As String = "UPDATE"
    Private Const CONST_DELETE As String = "DELETE"
    Private Const SP_CERTIFICADO_RELACION As String = "PRC_CertificadoRelaciones"
    Private Const CONST_ACCION_RELACION As String = "PUEDE_BORRAR"


    Private Const CONST_SELECT_ALL As String = "SELECT_ALL"
    Private CONST_SELECT_FILTRO As String = "SELECT_FILTRO"
    Private Const CONST_SELECT_ONE As String = "SELECT_ONE"
    Private Const CONST_SELECT_RELACIONES As String = "SELECT_RELACIONES"

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function FNConsultar(Certificado As CertificadoDTO) As List(Of CertificadoDTO)
        Dim listaCertificados As List(Of CertificadoDTO) = New List(Of CertificadoDTO)
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)
        Dim ds As DataSet

        Try
            sp.AgregarParametro("Accion", CONST_SELECT_ALL, System.Data.SqlDbType.VarChar)
            ds = sp.ReturnDataSet()
            For Each dataRow As DataRow In ds.Tables(0).Rows
                listaCertificados.Add(fillCertificado(dataRow))
            Next

        Catch ex As Exception
            Throw
        End Try

        Return listaCertificados
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function CertificadosBuscarFiltro(Certificado As CertificadoDTO, FechaDesde As Nullable(Of Date), FechaHasta As Nullable(Of Date)) As List(Of CertificadoDTO)
        Dim listaCertificados As List(Of CertificadoDTO) = New List(Of CertificadoDTO)
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)
        Dim ds As DataSet
        Dim HT_Corte As Nullable(Of Date)
        Dim HT_Canje As Nullable(Of Date)
        Dim CT_Fecha_Ingreso As Nullable(Of Date)

        Try

            If Certificado.HT_Corte = "0001-01-01" Then
                HT_Corte = Nothing
            Else
                HT_Corte = Certificado.HT_Corte
            End If

            If Certificado.HT_Canje = "0001-01-01" Then
                HT_Canje = Nothing
            Else
                HT_Canje = Certificado.HT_Canje
            End If

            If Certificado.CT_Fecha_Ingreso = "0001-01-01" Then
                CT_Fecha_Ingreso = Nothing
            Else
                CT_Fecha_Ingreso = Certificado.CT_Fecha_Ingreso
            End If

            sp.AgregarParametro("Accion", CONST_SELECT_FILTRO, System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("HT_Corte", HT_Corte, System.Data.SqlDbType.DateTime)
            sp.AgregarParametro("HT_Canje", HT_Canje, System.Data.SqlDbType.DateTime)
            sp.AgregarParametro("FN_RUT", Certificado.FN_Rut, System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("FN_Nombre_Corto", Certificado.FN_Nombre_Corto, System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("AP_RUT", Certificado.AP_Rut, System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("AP_Razon_Social", Certificado.AP_Razon_Social, System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("fechaDesde", CT_Fecha_Ingreso, System.Data.SqlDbType.Date)
            sp.AgregarParametro("fechaHasta", FechaHasta, System.Data.SqlDbType.Date)


            ds = sp.ReturnDataSet()

            For Each dataRow As DataRow In ds.Tables(0).Rows
                listaCertificados.Add(fillCertificado(dataRow))
            Next

        Catch ex As Exception
            Throw
        End Try

        Return listaCertificados
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function APIngresar(certificado As CertificadoDTO) As Boolean
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)
        Try
            ' ---TODO Data prueba---
            sp.AgregarParametro("Accion", CONST_INSERT, System.Data.SqlDbType.VarChar)

            certificado.CT_Estado = DTO.Estados.CONST_ACTIVO

            FillParameters(certificado, sp)

            sp.ReturnDataSet()

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function BuscarCertificado(accion As String, rut As String, multiFondo As String) As List(Of CertificadoDTO)
        Dim ListaCertificados As List(Of CertificadoDTO) = New List(Of CertificadoDTO)
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CERTIFICADOBUSCAR)
        Dim ds As DataSet

        Try
            ' ---TODO Data prueba---
            sp.AgregarParametro("Accion", accion, System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("ApRut", rut, System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("ApMultifondo", multiFondo, System.Data.SqlDbType.VarChar)

            ds = sp.ReturnDataSet()
            If ds.Tables.Count > 0 Then
                For Each dataRow As DataRow In ds.Tables(0).Rows
                    ListaCertificados.Add(fillCertificado(dataRow))
                Next
            End If
        Catch ex As Exception
            Throw
        End Try

        Return ListaCertificados
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function SoloRutAportante(aportante As AportanteDTO) As List(Of AportanteDTO)
        Dim Lista As List(Of AportanteDTO) = New List(Of AportanteDTO)
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)
        Dim ds As DataSet
        Dim AportanteNew As New AportanteDTO

        Try
            sp.AgregarParametro("Accion", "SOLO_RUT_APORTANTES", System.Data.SqlDbType.VarChar)
            ds = sp.ReturnDataSet()

            For Each dataRow As DataRow In ds.Tables(0).Rows
                AportanteNew = New AportanteDTO
                With AportanteNew
                    .Rut = dataRow("AP_RUT").ToString().Trim()

                End With
                Lista.Add(AportanteNew)
            Next
        Catch ex As Exception
            Throw
        End Try

        Return Lista
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function ExisteCertificado(certificado As CertificadoDTO) As Integer
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)
        Dim ds As DataSet
        Try
            sp.AgregarParametro("Accion", "CERTIFICADO_EXISTE", System.Data.SqlDbType.VarChar)
            FillParameters(certificado, sp)

            ds = sp.ReturnDataSet()

            If ds.Tables.Count > 0 AndAlso ds.Tables(0).Rows.Count > 0 Then
                Dim dataRow As DataRow = ds.Tables(0).Rows(0)
                Return dataRow("ExisteEnGrupo")
            Else
                Return Constantes.CONST_ERROR_BBDD
            End If

        Catch ex As Exception
            Return Constantes.CONST_ERROR_BBDD
        End Try
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function ConsultarUltimoCorrelativo(certificado As CertificadoDTO) As Integer
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)
        Dim ds As DataSet

        Try
            ' ---TODO Data prueba---
            sp.AgregarParametro("Accion", "ULT_COORELATIVO", System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("CT_Numero", certificado.CT_Numero, System.Data.SqlDbType.Decimal)

            ds = sp.ReturnDataSet()

            If ds.Tables.Count > 0 AndAlso ds.Tables(0).Rows.Count > 0 Then
                Dim dataRow As DataRow = ds.Tables(0).Rows(0)
                Return dataRow("CT_Correlativo")
            Else
                Return Constantes.CONST_ERROR_BBDD
            End If

        Catch ex As Exception
            Return Constantes.CONST_ERROR_BBDD
        End Try
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function ConsultarNombreAportante(certificado As CertificadoDTO) As List(Of CertificadoDTO)
        Dim ListaAportantes As List(Of CertificadoDTO) = New List(Of CertificadoDTO)
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)
        Dim ds As DataSet
        Dim aportanteNew As New CertificadoDTO

        Try
            ' ---TODO Data prueba---
            sp.AgregarParametro("Accion", "SELECT_FILTRO_NOMBREAPORT", System.Data.SqlDbType.VarChar)

            ds = sp.ReturnDataSet()

            For Each dataRow As DataRow In ds.Tables(0).Rows
                aportanteNew = New CertificadoDTO
                With aportanteNew
                    .AP_Razon_Social = dataRow("AP_Razon_Social").ToString().Trim()
                    .AP_Rut = dataRow("AP_RUT").ToString().Trim()
                End With

                ListaAportantes.Add(aportanteNew)
            Next

        Catch ex As Exception
            Throw
        End Try

        Return ListaAportantes
    End Function



    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function ConsultarNombreFondo(certificado As CertificadoDTO) As List(Of CertificadoDTO)
        Dim ListaAportantes As List(Of CertificadoDTO) = New List(Of CertificadoDTO)
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)
        Dim ds As DataSet
        Dim fondoNew As New CertificadoDTO

        Try
            ' ---TODO Data prueba---
            sp.AgregarParametro("Accion", "SELECT_FILTRO_NOMBREFONDO", System.Data.SqlDbType.VarChar)

            ds = sp.ReturnDataSet()

            For Each dataRow As DataRow In ds.Tables(0).Rows
                fondoNew = New CertificadoDTO
                With fondoNew
                    .FN_Nombre_Corto = dataRow("FN_Nombre_Corto").ToString().Trim()
                    .FN_Rut = dataRow("FN_RUT").ToString().Trim()
                End With

                ListaAportantes.Add(fondoNew)
            Next

        Catch ex As Exception
            Throw
        End Try

        Return ListaAportantes
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function ConsultarPorDocumento(certificado As CertificadoDTO) As List(Of CertificadoDTO)
        Dim listaCertificados As List(Of CertificadoDTO) = New List(Of CertificadoDTO)
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)
        Dim ds As DataSet
        Dim aportanteNew As New CertificadoDTO

        Try
            ' ---TODO Data prueba---
            sp.AgregarParametro("Accion", "SELECT_BY_DOCUMENT", System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("CT_Numero", certificado.CT_Numero, System.Data.SqlDbType.Decimal)

            ds = sp.ReturnDataSet()

            For Each dataRow As DataRow In ds.Tables(0).Rows
                listaCertificados.Add(fillCertificado(dataRow))
            Next

        Catch ex As Exception
            Throw
        End Try

        Return listaCertificados
    End Function



    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function GetDocumento(certificado As CertificadoDTO) As CertificadoDTO
        Dim certificadoRetorno As CertificadoDTO = New CertificadoDTO
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)
        Dim ds As DataSet

        Try
            sp.AgregarParametro("Accion", CONST_SELECT_ONE, System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("CT_Numero", certificado.CT_Numero, System.Data.SqlDbType.Decimal)
            sp.AgregarParametro("CT_Correlativo", certificado.CT_Correlativo, System.Data.SqlDbType.Decimal)

            'FillParameters(certificado, sp)

            ds = sp.ReturnDataSet()
            If ds.Tables.Count > 0 AndAlso ds.Tables(0).Rows.Count > 0 Then
                certificadoRetorno = fillCertificado(ds.Tables(0).Rows(0))
            Else
                certificadoRetorno = Nothing
            End If

        Catch ex As Exception
            Throw
        End Try

        Return certificadoRetorno
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function FNModificar(Certificado As CertificadoDTO) As Boolean
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)

        Try
            ' ---TODO Data prueba---
            sp.AgregarParametro("Accion", CONST_UPDATE, System.Data.SqlDbType.VarChar)

            FillParameters(Certificado, sp)

            sp.ReturnDataSet()

            Return (sp.FilasAfectas > 0)
        Catch ex As Exception
            Return False
        End Try
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function BuscarRelaciones(Certificado As CertificadoDTO) As Integer
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)
        Dim ds As DataSet
        Try
            ' ---TODO Data prueba---
            sp.AgregarParametro("Accion", CONST_SELECT_RELACIONES, System.Data.SqlDbType.VarChar)
            FillParameters(Certificado, sp)

            ds = sp.ReturnDataSet()

            If ds.Tables.Count > 0 AndAlso ds.Tables(0).Rows.Count > 0 Then
                For Each dataRow As DataRow In ds.Tables(0).Rows
                    Return dataRow(0)
                Next
            End If

            Return 99

        Catch ex As Exception
            Return 99
        End Try
    End Function


    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function FNAEliminar(Certificado As CertificadoDTO) As Boolean
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)
        Try
            ' ---TODO Data prueba---
            sp.AgregarParametro("Accion", CONST_DELETE, System.Data.SqlDbType.VarChar)
            Certificado.CT_Estado = DTO.Estados.CONST_ELIMINADO
            FillParameters(Certificado, sp)

            sp.ReturnDataSet()

            Return (sp.FilasAfectas > 0)
        Catch ex As Exception
            Return False
        End Try
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function DeleteCertificado(Certificado As CertificadoDTO) As Boolean
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)
        Dim ds As DataSet
        Try
            sp.AgregarParametro("Accion", "DELETE_ONE", System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("CT_Numero", Certificado.CT_Numero, System.Data.SqlDbType.Decimal)
            sp.AgregarParametro("CT_Correlativo", Certificado.CT_Correlativo, System.Data.SqlDbType.Decimal)
            ds = sp.ReturnDataSet()
            Return True
        Catch ex As Exception
            Throw
        End Try

    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function DeleteCertificadoAll(Certificado As CertificadoDTO) As Integer
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)
        Dim ds As DataSet
        Try
            sp.AgregarParametro("Accion", "DELETE_ALL", System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("CT_Numero", Certificado.CT_Numero, System.Data.SqlDbType.Decimal)
            ds = sp.ReturnDataSet()

            Return sp.FilasAfectas
        Catch ex As Exception
            Return 0
        End Try

    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function CertificadosIngresar(certificado As CertificadoDTO) As Boolean
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)

        Dim HT_Corte As Nullable(Of Date)
        Dim HT_Canje As Nullable(Of Date)
        Dim CT_Fecha As Nullable(Of Date)
        Try

            If certificado.HT_Corte = "0001-01-01" Then
                HT_Corte = Nothing
            Else
                HT_Corte = certificado.HT_Corte
            End If

            If certificado.HT_Canje = "0001-01-01" Then
                HT_Canje = Nothing
            Else
                HT_Canje = certificado.HT_Canje
            End If

            If certificado.CT_Fecha = "0001-01-01" Then
                CT_Fecha = Date.Now
            Else
                CT_Fecha = certificado.CT_Fecha
            End If

            sp.AgregarParametro("Accion", "INSERT", System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("CT_Numero", certificado.CT_Numero, System.Data.SqlDbType.Decimal)
            sp.AgregarParametro("CT_Correlativo", certificado.CT_Correlativo, System.Data.SqlDbType.Decimal)
            sp.AgregarParametro("HT_ID", certificado.HT_ID, System.Data.SqlDbType.Decimal)
            sp.AgregarParametro("CT_Fecha", CT_Fecha, System.Data.SqlDbType.DateTime)
            sp.AgregarParametro("AP_Rut", certificado.AP_Rut, System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("AP_Multifondo", certificado.AP_Multifondo, System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("FS_Nemotecnico", certificado.FS_Nemotecnico, System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("CT_Usuario_Ingreso", certificado.CT_Usuario_Ingreso, System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("CT_Cuotas", certificado.CT_Cuotas, System.Data.SqlDbType.Decimal)
            sp.AgregarParametro("HT_Corte", HT_Corte, System.Data.SqlDbType.DateTime)
            sp.AgregarParametro("HT_Canje", HT_Canje, System.Data.SqlDbType.DateTime)
            sp.AgregarParametro("FN_Rut", certificado.FN_Rut, System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("FN_Nombre_Corto", certificado.FN_Nombre_Corto, System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("AP_Razon_Social", certificado.AP_Razon_Social, System.Data.SqlDbType.VarChar)

            sp.ReturnDataSet()

            Return True
        Catch ex As Exception
            Throw
        End Try
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function IngresarTemporalModificados(certificado As CertificadoDTO) As Boolean
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)

        Dim HT_Corte As Nullable(Of Date)
        Dim HT_Canje As Nullable(Of Date)
        Dim CT_Fecha As Nullable(Of Date)
        Try
            If certificado.HT_Corte = "0001-01-01" Then
                HT_Corte = Nothing
            Else
                HT_Corte = certificado.HT_Corte
            End If

            If certificado.HT_Canje = "0001-01-01" Then
                HT_Canje = Nothing
            Else
                HT_Canje = certificado.HT_Canje
            End If

            If certificado.CT_Fecha = "0001-01-01" Then
                CT_Fecha = Date.Now
            Else
                CT_Fecha = certificado.CT_Fecha
            End If
            sp.AgregarParametro("Accion", "INSERTA_TEMPORAL", System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("CT_Numero", certificado.CT_Numero, System.Data.SqlDbType.Decimal)
            sp.AgregarParametro("CT_Correlativo", certificado.CT_Correlativo, System.Data.SqlDbType.Decimal)
            sp.AgregarParametro("HT_ID", certificado.HT_ID, System.Data.SqlDbType.Decimal)
            sp.AgregarParametro("CT_Fecha", CT_Fecha, System.Data.SqlDbType.DateTime)
            sp.AgregarParametro("AP_Rut", certificado.AP_Rut, System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("AP_Multifondo", certificado.AP_Multifondo, System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("FS_Nemotecnico", certificado.FS_Nemotecnico, System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("CT_Usuario_Ingreso", certificado.CT_Usuario_Ingreso, System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("CT_Cuotas", certificado.CT_Cuotas, System.Data.SqlDbType.Decimal)
            sp.AgregarParametro("HT_Corte", HT_Corte, System.Data.SqlDbType.DateTime)
            sp.AgregarParametro("HT_Canje", HT_Canje, System.Data.SqlDbType.DateTime)
            sp.AgregarParametro("FN_Rut", certificado.FN_Rut, System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("FN_Nombre_Corto", certificado.FN_Nombre_Corto, System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("AP_Razon_Social", certificado.AP_Razon_Social, System.Data.SqlDbType.VarChar)
            sp.ReturnDataSet()

            Return True
        Catch ex As Exception
            Throw
        End Try
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function ModificarEliminarCertificados(certificado As CertificadoDTO) As Boolean
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)
        Try
            sp.AgregarParametro("Accion", "MODIFICA_CERTIFICADOS", System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("CT_Numero", certificado.CT_Numero, System.Data.SqlDbType.Decimal)

            sp.ReturnDataSet()

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Function fillCertificado(dataRow As DataRow) As CertificadoDTO
        Dim certificado As New CertificadoDTO
        With certificado
            .CT_Numero = dataRow("CT_Numero")
            .CT_Correlativo = dataRow("CT_Correlativo")
            .HT_ID = dataRow("HT_ID")
            .CT_Fecha = dataRow("CT_Fecha")
            .AP_Rut = dataRow("AP_Rut").ToString().Trim()
            .FN_Rut = dataRow("FN_Rut").ToString().Trim()
            .AP_Multifondo = dataRow("AP_Multifondo").ToString().Trim()
            .FS_Nemotecnico = dataRow("FS_Nemotecnico").ToString().Trim()
            .CT_Cuotas = dataRow("CT_Cuotas")
            .CT_Estado = dataRow("CT_Estado").ToString().Trim()
            .CT_Fecha_Ingreso = dataRow("CT_Fecha_Ingreso")
            .CT_Usuario_Ingreso = dataRow("CT_Usuario_Ingreso").ToString().Trim()
            .CT_Fecha_Modificacion = dataRow("CT_Fecha_Modificacion").ToString().Trim()
            .CT_Usuario_Modificacion = dataRow("CT_Usuario_Modificacion").ToString().Trim()

            .HT_Corte = dataRow("HT_Corte").ToString().Trim()
            .HT_Canje = dataRow("HT_Canje").ToString().Trim()
            .FN_Rut = dataRow("FN_RUT").ToString().Trim()
            .FN_Nombre_Corto = dataRow("FN_Nombre_Corto").ToString().Trim()
            .AP_Razon_Social = dataRow("AP_Razon_Social").ToString().Trim()
        End With
        Return certificado
    End Function

    Private Sub FillParameters(certificado As CertificadoDTO, sp As DBSqlServer.SqlServer.StoredProcedure)

        Dim CT_Fecha As Nullable(Of Date)
        Dim CT_Fecha_Ingreso As Nullable(Of Date)
        Dim CT_Fecha_Modificacion As Nullable(Of Date)
        Dim HT_Corte As Nullable(Of Date)
        Dim HT_Canje As Nullable(Of Date)

        If certificado.HT_Corte = "0001-01-01" Then
            HT_Corte = Nothing
        Else
            HT_Corte = certificado.HT_Corte
        End If

        If certificado.HT_Canje = "0001-01-01" Then
            HT_Canje = Nothing
        Else
            HT_Canje = certificado.HT_Canje
        End If

        If certificado.CT_Fecha = "0001-01-01" Then
            CT_Fecha = Nothing
        Else
            CT_Fecha = certificado.CT_Fecha
        End If

        If certificado.CT_Fecha_Ingreso = "0001-01-01" Then
            CT_Fecha_Ingreso = Nothing
        Else
            CT_Fecha_Ingreso = certificado.CT_Fecha_Ingreso
        End If


        sp.AgregarParametro("CT_Numero", certificado.CT_Numero, System.Data.SqlDbType.Int)
        sp.AgregarParametro("CT_Correlativo", certificado.CT_Correlativo, System.Data.SqlDbType.Int)
        sp.AgregarParametro("HT_ID", certificado.HT_ID, System.Data.SqlDbType.Int)
        sp.AgregarParametro("CT_Fecha", CT_Fecha, System.Data.SqlDbType.DateTime)
        sp.AgregarParametro("AP_RUT", certificado.AP_Rut, System.Data.SqlDbType.VarChar)
        sp.AgregarParametro("FN_RUT", certificado.FN_Rut, System.Data.SqlDbType.VarChar)
        sp.AgregarParametro("AP_Multifondo", certificado.AP_Multifondo, System.Data.SqlDbType.Char)
        sp.AgregarParametro("FS_Nemotecnico", certificado.FS_Nemotecnico, System.Data.SqlDbType.VarChar)
        sp.AgregarParametro("CT_Cuotas", certificado.CT_Cuotas, System.Data.SqlDbType.Decimal)
        sp.AgregarParametro("CT_Estado", certificado.CT_Estado, System.Data.SqlDbType.Char)
        sp.AgregarParametro("CT_Fecha_Ingreso", CT_Fecha_Ingreso, System.Data.SqlDbType.Date)
        sp.AgregarParametro("CT_Fecha_Modificacion", CT_Fecha_Modificacion, System.Data.SqlDbType.Date)
        sp.AgregarParametro("CT_Usuario_Modificacion", certificado.CT_Usuario_Modificacion, System.Data.SqlDbType.VarChar)
        sp.AgregarParametro("HT_Corte", HT_Corte, System.Data.SqlDbType.DateTime)
        sp.AgregarParametro("HT_Canje", HT_Canje, System.Data.SqlDbType.DateTime)
        sp.AgregarParametro("FN_Nombre_Corto", certificado.FN_Nombre_Corto, System.Data.SqlDbType.VarChar)
        sp.AgregarParametro("AP_Razon_Social", certificado.AP_Razon_Social, System.Data.SqlDbType.VarChar)
    End Sub

End Class