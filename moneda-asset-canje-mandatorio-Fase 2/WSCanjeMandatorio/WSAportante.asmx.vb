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
Public Class WSAportante
    Inherits System.Web.Services.WebService

    Private Const SP_CRUD As String = "PRC_AportanteCRUD"
    Private Const SP_CONSULTAR As String = "PRC_AportanteConsultar"
    Private Const SP_APORTANTEBUSCAR As String = "PRC_AportanteBuscar"
    Private Const CONST_INSERT As String = "INSERT"
    Private Const CONST_UPDATE As String = "UPDATE"
    Private Const CONST_DELETE As String = "DELETE"
    Private Const SP_APORTANTE_RELACION As String = "PRC_AportanteRelaciones"
    Private Const CONST_ACCION_RELACION As String = "PUEDE_BORRAR"
    Private Const CONST_SELECT_ALL As String = "SELECT_ALL"

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function ConsultarPorRut(aportante As AportanteDTO) As List(Of AportanteDTO)
        Dim ListaAportantes As List(Of AportanteDTO) = New List(Of AportanteDTO)
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CONSULTAR)
        Dim ds As DataSet
        Dim aportanteNew As New AportanteDTO

        Try
            ' ---TODO Data prueba---
            sp.AgregarParametro("Accion", "SELECT_DISTINCT", System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("RutAportante", aportante.Rut, System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("RazonSocial", Nothing, System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("fechaDesde", Nothing, System.Data.SqlDbType.Date)
            sp.AgregarParametro("fechaHasta", Nothing, System.Data.SqlDbType.Date)

            ds = sp.ReturnDataSet()

            For Each dataRow As DataRow In ds.Tables(0).Rows
                aportanteNew = New AportanteDTO
                With aportanteNew
                    .Rut = dataRow("AP_RUT").ToString().Trim()
                    .RazonSocial = dataRow("AP_Razon_Social").ToString().Trim()
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
    Public Function ConsultarPorRazonSocial(aportante As AportanteDTO) As List(Of AportanteDTO)
        Dim ListaAportantes As List(Of AportanteDTO) = New List(Of AportanteDTO)
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CONSULTAR)
        Dim ds As DataSet
        Dim aportanteNew As New AportanteDTO

        Try
            ' ---TODO Data prueba---
            sp.AgregarParametro("Accion", "CONSULTA_POR_NOMBRE", System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("RutAportante", Nothing, System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("RazonSocial", aportante.RazonSocial, System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("fechaDesde", Nothing, System.Data.SqlDbType.Date)
            sp.AgregarParametro("fechaHasta", Nothing, System.Data.SqlDbType.Date)

            ds = sp.ReturnDataSet()

            For Each dataRow As DataRow In ds.Tables(0).Rows
                aportanteNew = New AportanteDTO
                With aportanteNew
                    .RazonSocial = dataRow("AP_Razon_Social").ToString().Trim()
                    .Multifondo = dataRow("AP_Multifondo").ToString().Trim()
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
    Public Function APConsultar(aportante As AportanteDTO, FechaHasta As Nullable(Of Date)) As List(Of AportanteDTO)
        Dim ListaAportantes As List(Of AportanteDTO) = New List(Of AportanteDTO)
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CONSULTAR)
        Dim ds As DataSet

        Try
            sp.AgregarParametro("Accion", "CONSULTA_POR_FILTRO", System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("RutAportante", aportante.Rut, System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("RazonSocial", aportante.RazonSocial, System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("fechaDesde", aportante.FechaIngreso, System.Data.SqlDbType.Date)
            sp.AgregarParametro("fechaHasta", FechaHasta, System.Data.SqlDbType.Date)

            ds = sp.ReturnDataSet()

            For Each dataRow As DataRow In ds.Tables(0).Rows
                ListaAportantes.Add(fillAportante(dataRow))
            Next

        Catch ex As Exception
            Throw
        End Try

        Return ListaAportantes
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function GetListaAportantesDistinct(aportante As AportanteDTO) As List(Of AportanteDTO)
        Dim ListaAportantes As List(Of AportanteDTO) = New List(Of AportanteDTO)
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CONSULTAR)
        Dim ds As DataSet

        Try
            sp.AgregarParametro("Accion", "CONSULTA_POR_FILTRO", System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("RutAportante", aportante.Rut, System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("RazonSocial", aportante.RazonSocial, System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("fechaDesde", aportante.FechaIngreso, System.Data.SqlDbType.Date)
            sp.AgregarParametro("fechaHasta", aportante.FechaIngreso, System.Data.SqlDbType.Date)

            ds = sp.ReturnDataSet()

            For Each dataRow As DataRow In ds.Tables(0).Rows
                ListaAportantes.Add(fillAportante(dataRow))
            Next



        Catch ex As Exception
            Throw
        End Try

        Return ListaAportantes
    End Function


    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function BuscarAportante(accion As String, rut As String) As List(Of AportanteDTO)
        Return BuscarAportante(accion, rut, Nothing)
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function BuscarAportante(accion As String, rut As String, multiFondo As String) As List(Of AportanteDTO)
        Dim ListaAportantes As List(Of AportanteDTO) = New List(Of AportanteDTO)
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_APORTANTEBUSCAR)
        Dim ds As DataSet

        Try
            ' ---TODO Data prueba---
            sp.AgregarParametro("Accion", accion, System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("ApRut", rut, System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("ApMultifondo", multiFondo, System.Data.SqlDbType.VarChar)

            ds = sp.ReturnDataSet()
            If ds.Tables.Count > 0 Then
                For Each dataRow As DataRow In ds.Tables(0).Rows
                    ListaAportantes.Add(fillAportante(dataRow))
                Next
            End If
        Catch ex As Exception
            Throw
        End Try

        Return ListaAportantes
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function AportantePorMultifondo(aportante As AportanteDTO) As List(Of AportanteDTO)
        Dim Lista As List(Of AportanteDTO) = New List(Of AportanteDTO)
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_APORTANTEBUSCAR)
        Dim ds As DataSet

        Try

            sp.AgregarParametro("Accion", "SELECT_BY_MULTIFONDO", System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("ApMultifondo", aportante.Multifondo, System.Data.SqlDbType.VarChar)

            ds = sp.ReturnDataSet()
            If ds.Tables.Count > 0 Then
                For Each dataRow As DataRow In ds.Tables(0).Rows
                    Lista.Add(fillAportante(dataRow))
                Next
            End If

        Catch ex As Exception
            Throw
        End Try

        Return Lista
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function MultifondoPorRut(aportante As AportanteDTO) As List(Of AportanteDTO)
        Dim Lista As List(Of AportanteDTO) = New List(Of AportanteDTO)
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CONSULTAR)
        Dim ds As DataSet

        Try

            sp.AgregarParametro("Accion", "SELECT_BY_RUT", System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("RutAportante", aportante.Rut, System.Data.SqlDbType.VarChar)

            ds = sp.ReturnDataSet()

            For Each dataRow As DataRow In ds.Tables(0).Rows
                Lista.Add(fillAportante(dataRow))
            Next

        Catch ex As Exception
            Throw
        End Try

        Return Lista

    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function AportantePorNombre(aportante As AportanteDTO) As List(Of AportanteDTO)
        Dim Lista As List(Of AportanteDTO) = New List(Of AportanteDTO)
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CONSULTAR)
        Dim ds As DataSet

        Try

            sp.AgregarParametro("Accion", "SELECT_BY_NOMBRE", System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("RazonSocial", aportante.RazonSocial, System.Data.SqlDbType.VarChar)

            ds = sp.ReturnDataSet()

            For Each dataRow As DataRow In ds.Tables(0).Rows
                Lista.Add(fillAportante(dataRow))
            Next

        Catch ex As Exception
            Throw
        End Try

        Return Lista

    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function APIngresar(aportante As AportanteDTO) As Boolean
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)
        Try
            ' ---TODO Data prueba---
            sp.AgregarParametro("Accion", CONST_INSERT, System.Data.SqlDbType.VarChar)

            aportante.Estado = DTO.Estados.CONST_ACTIVO

            FillParameters(aportante, sp)

            sp.ReturnDataSet()

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function APModificar(aportante As AportanteDTO) As Boolean

        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)
        Try
            ' ---TODO Data prueba---
            sp.AgregarParametro("Accion", CONST_UPDATE, System.Data.SqlDbType.VarChar)

            FillParameters(aportante, sp)

            sp.ReturnDataSet()

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function APEliminar(aportante As AportanteDTO) As Boolean
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)
        Try
            ' ---TODO Data prueba---
            sp.AgregarParametro("Accion", CONST_DELETE, System.Data.SqlDbType.VarChar)
            aportante.Estado = DTO.Estados.CONST_ELIMINADO
            FillParameters(aportante, sp)

            sp.ReturnDataSet()

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function BuscarRelacionAportante(aportante As AportanteDTO) As Integer
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_APORTANTE_RELACION)
        Dim ds As DataSet
        Try
            ' ---TODO Data prueba---
            sp.AgregarParametro("Accion", CONST_ACCION_RELACION, System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("ApRut", aportante.Rut, System.Data.SqlDbType.VarChar)

            ds = sp.ReturnDataSet()

            If ds.Tables.Count > 0 AndAlso ds.Tables(0).Rows.Count > 0 Then
                Dim dataRow As DataRow = ds.Tables(0).Rows(0)
                Return dataRow("CantidadRelaciones")
            Else
                Return Constantes.CONST_ERROR_BBDD
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function TraerAportantes(aportante As AportanteDTO) As List(Of AportanteDTO)
        Dim ListaAportantes As List(Of AportanteDTO) = New List(Of AportanteDTO)
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_APORTANTEBUSCAR)
        Dim ds As DataSet

        Try
            ' ---TODO Data prueba---
            sp.AgregarParametro("Accion", CONST_SELECT_ALL, System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("ApRut", aportante.Rut, System.Data.SqlDbType.VarChar)


            ds = sp.ReturnDataSet()
            If ds.Tables.Count > 0 Then
                For Each dataRow As DataRow In ds.Tables(0).Rows
                    ListaAportantes.Add(fillAportante(dataRow))
                Next
            End If
        Catch ex As Exception
            Throw
        End Try

        Return ListaAportantes
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function TraerMultifondos() As List(Of AportanteDTO)
        Dim ListaAportantes As List(Of AportanteDTO) = New List(Of AportanteDTO)
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CONSULTAR)
        Dim ds As DataSet
        Dim aportanteNew As New AportanteDTO

        Try
            ' ---TODO Data prueba---
            sp.AgregarParametro("Accion", "SELECT_MULTIFONDO", System.Data.SqlDbType.VarChar)

            ds = sp.ReturnDataSet()

            For Each dataRow As DataRow In ds.Tables(0).Rows
                aportanteNew = New AportanteDTO
                With aportanteNew
                    .Multifondo = dataRow("AP_Multifondo").ToString().Trim()
                End With

                ListaAportantes.Add(aportanteNew)
            Next

        Catch ex As Exception
            Throw
        End Try

        Return ListaAportantes
    End Function

    Private Function fillAportante(dataRow As DataRow) As AportanteDTO
        Dim aportante As New AportanteDTO
        With aportante
            .Rut = dataRow("AP_RUT").ToString().Trim()
            .RazonSocial = dataRow("AP_Razon_Social").ToString().Trim()
            .Multifondo = dataRow("AP_Multifondo").ToString().Trim()
            .NacExt = dataRow("AP_Nac_Ext")
            .Calificado = dataRow("AP_Calificado")
            .Intermediario = dataRow("AP_Intermediario")
            .RelacionMan = dataRow("AP_Rel_MAM")
            .Distribucion = dataRow("AP_Contrato_Distribucion")
            .Estado = dataRow("AP_Estado")
            .FechaIngreso = dataRow("AP_Fecha_Ingreso")
            .UsuarioIngreso = dataRow("AP_Usuario_Ingreso")
            .FechaModificacion = dataRow("AP_Fecha_Modificacion")
            .UsuarioModificacion = dataRow("AP_Usuario_Modificacion")
            .Documentacion = dataRow("AP_Documentacion") 'Agrega parametro de documentacion de aportantes jc'
        End With
        Return aportante
    End Function

    Private Sub FillParameters(aportante As AportanteDTO, sp As DBSqlServer.SqlServer.StoredProcedure)
        sp.AgregarParametro("ApRut", aportante.Rut, System.Data.SqlDbType.VarChar)

        If aportante.Multifondo = Nothing Then
            aportante.Multifondo = ""
        End If
        sp.AgregarParametro("ApMultifondo", aportante.Multifondo, System.Data.SqlDbType.VarChar)
        sp.AgregarParametro("ApRazonSocial", aportante.RazonSocial, System.Data.SqlDbType.VarChar)
        sp.AgregarParametro("ApNacExt", aportante.NacExt, System.Data.SqlDbType.VarChar)
        sp.AgregarParametro("ApCalificado", aportante.Calificado, System.Data.SqlDbType.VarChar)
        sp.AgregarParametro("ApIntermediario", aportante.Intermediario, System.Data.SqlDbType.VarChar)
        sp.AgregarParametro("ApRelMam", aportante.RelacionMan, System.Data.SqlDbType.VarChar)
        sp.AgregarParametro("ApContratoDistribucion", aportante.Distribucion, System.Data.SqlDbType.VarChar)
        sp.AgregarParametro("ApEstado", aportante.Estado, System.Data.SqlDbType.Int)
        sp.AgregarParametro("ApUsuario", aportante.UsuarioIngreso, System.Data.SqlDbType.VarChar)
        sp.AgregarParametro("ApDocumentacion", aportante.Documentacion, System.Data.SqlDbType.VarChar)
    End Sub

End Class