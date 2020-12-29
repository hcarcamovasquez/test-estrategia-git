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
Public Class WSFondos
    Inherits System.Web.Services.WebService

    Private Const SP_CRUD As String = "PRC_FondosCRUD"
    Private Const SP_FONDO_FILTRO As String = "PRC_FondosBuscarFiltro"

    Private Const CONST_INSERT As String = "INSERT"
    Private Const CONST_UPDATE As String = "UPDATE"
    Private Const CONST_DELETE As String = "DELETE"
    Private Const CONST_SELECT_ALL As String = "SELECT_ALL"
    Private Const CONST_SELECT_FILTRO As String = "SELECT_FILTRO"
    Private Const CONST_SELECT_ONE As String = "SELECT_ONE"
    Private Const CONST_SELECT_RELACIONES As String = "SELECT_RELACIONES"
    Private Const SP_CONSULTAS As String = "PRC_FondoSerieConsultas"

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function ConsultarPorRut(fondo As FondoDTO) As List(Of FondoDTO)
        Dim ListaAportantes As List(Of FondoDTO) = New List(Of FondoDTO)
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)
        Dim ds As DataSet
        Dim aportanteNew As New FondoDTO

        Try
            ' ---TODO Data prueba---
            sp.AgregarParametro("Accion", "CONSULTA_POR_RUT", System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("FnRut", fondo.Rut, System.Data.SqlDbType.VarChar)

            ds = sp.ReturnDataSet()

            For Each dataRow As DataRow In ds.Tables(0).Rows
                aportanteNew = New FondoDTO
                With aportanteNew
                    .Rut = dataRow("FN_RUT").ToString().Trim()
                    .RazonSocial = dataRow("FN_Razon_Social").ToString().Trim()
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
    Public Function ConsultarPorRazonSocial(fondo As FondoDTO) As List(Of FondoDTO)
        Dim ListaAportantes As List(Of FondoDTO) = New List(Of FondoDTO)
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)
        Dim ds As DataSet
        Dim aportanteNew As New FondoDTO

        Try
            ' ---TODO Data prueba---
            sp.AgregarParametro("Accion", "CONSULTA_POR_NOMBRE", System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("FnRazonSocial", fondo.RazonSocial, System.Data.SqlDbType.VarChar)

            ds = sp.ReturnDataSet()

            For Each dataRow As DataRow In ds.Tables(0).Rows
                aportanteNew = New FondoDTO
                With aportanteNew
                    .RazonSocial = dataRow("FN_Razon_Social").ToString().Trim()
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
    Public Function ConsultarPorNombre(fondo As FondoDTO) As List(Of FondoDTO)
        Dim ListaAportantes As List(Of FondoDTO) = New List(Of FondoDTO)
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)
        Dim ds As DataSet
        Dim aportanteNew As New FondoDTO

        Try
            ' ---TODO Data prueba---
            sp.AgregarParametro("Accion", "CONSULTA_POR_NOMBRE_RUT", System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("FnNombreCorto", fondo.NombreCorto, System.Data.SqlDbType.VarChar)

            ds = sp.ReturnDataSet()

            For Each dataRow As DataRow In ds.Tables(0).Rows
                aportanteNew = New FondoDTO
                With aportanteNew
                    .NombreCorto = dataRow("Fn_Nombre_Corto").ToString().Trim()
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
    Public Function FondoBuscarFiltro(fondo As FondoDTO, FechaHasta As Nullable(Of Date)) As List(Of FondoDTO)
        Dim listaFondos As List(Of FondoDTO) = New List(Of FondoDTO)
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)
        Dim ds As DataSet

        Try
            sp.AgregarParametro("Accion", CONST_SELECT_FILTRO, System.Data.SqlDbType.VarChar)

            FillParameters(fondo, sp)

            sp.AgregarParametro("fechaDesde", fondo.FechaIngreso, System.Data.SqlDbType.Date)
            sp.AgregarParametro("fechaHasta", FechaHasta, System.Data.SqlDbType.Date)

            ds = sp.ReturnDataSet()

            For Each dataRow As DataRow In ds.Tables(0).Rows
                listaFondos.Add(fillObject(dataRow))
            Next

        Catch ex As Exception
            Throw
        End Try

        Return listaFondos
    End Function


    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function FNConsultar(fondo As FondoDTO) As List(Of FondoDTO)
        Dim listaFondos As List(Of FondoDTO) = New List(Of FondoDTO)
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)
        Dim ds As DataSet

        Try
            sp.AgregarParametro("Accion", CONST_SELECT_ALL, System.Data.SqlDbType.VarChar)

            FillParameters(fondo, sp)

            ds = sp.ReturnDataSet()

            For Each dataRow As DataRow In ds.Tables(0).Rows
                listaFondos.Add(fillObject(dataRow))
            Next

        Catch ex As Exception
            Throw
        End Try

        Return listaFondos
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function ConsultarUnFondo(fondo As FondoDTO) As List(Of FondoDTO)
        Dim listaFondos As List(Of FondoDTO) = New List(Of FondoDTO)
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)
        Dim ds As DataSet

        Try
            sp.AgregarParametro("Accion", CONST_SELECT_ONE, System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("FnRut", fondo.Rut, System.Data.SqlDbType.VarChar)


            ds = sp.ReturnDataSet()

            For Each dataRow As DataRow In ds.Tables(0).Rows
                listaFondos.Add(fillObject(dataRow))
            Next

        Catch ex As Exception
            Throw
        End Try

        Return listaFondos
    End Function
    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function FondosPorNombre(fondo As FondoDTO) As List(Of FondoDTO)
        Dim Lista As List(Of FondoDTO) = New List(Of FondoDTO)
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)
        Dim ds As DataSet
        Try
            sp.AgregarParametro("Accion", "SELECT_BY_NOMBRE", System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("FnNombreCorto", fondo.NombreCorto, System.Data.SqlDbType.VarChar)

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
    Public Function GetFondo(fondo As FondoDTO) As FondoDTO
        Dim fondoRetorno As FondoDTO = New FondoDTO
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)
        Dim ds As DataSet

        Try
            sp.AgregarParametro("Accion", CONST_SELECT_ONE, System.Data.SqlDbType.VarChar)

            FillParameters(fondo, sp)

            ds = sp.ReturnDataSet()
            If ds.Tables.Count > 0 AndAlso ds.Tables(0).Rows.Count > 0 Then
                fondoRetorno = fillObject(ds.Tables(0).Rows(0))
            Else
                fondoRetorno = Nothing
            End If

        Catch ex As Exception
            Throw
        End Try

        Return fondoRetorno
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function FNModificar(fondo As FondoDTO) As Boolean
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)

        Try
            ' ---TODO Data prueba---
            sp.AgregarParametro("Accion", CONST_UPDATE, System.Data.SqlDbType.VarChar)

            FillParameters(fondo, sp)

            sp.ReturnDataSet()

            Return (sp.FilasAfectas > 0)
        Catch ex As Exception
            Return False
        End Try
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function FNAEliminar(fondo As FondoDTO) As Boolean
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)
        Try
            ' ---TODO Data prueba---
            sp.AgregarParametro("Accion", CONST_DELETE, System.Data.SqlDbType.VarChar)
            fondo.Estado = DTO.Estados.CONST_ELIMINADO
            FillParameters(fondo, sp)

            sp.ReturnDataSet()

            Return (sp.FilasAfectas > 0)
        Catch ex As Exception
            Return False
        End Try
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function FNAIngresar(fondo As FondoDTO) As Boolean
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)
        Try
            sp.AgregarParametro("Accion", CONST_INSERT, System.Data.SqlDbType.VarChar)

            fondo.Estado = DTO.Estados.CONST_ACTIVO

            FillParameters(fondo, sp)

            sp.ReturnDataSet()

            Return True

        Catch ex As Exception
            Return False
        End Try
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function BuscarRelaciones(fondo As FondoDTO) As Integer
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)
        Dim ds As DataSet
        Try
            ' ---TODO Data prueba---
            sp.AgregarParametro("Accion", CONST_SELECT_RELACIONES, System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("FnRut", fondo.Rut, System.Data.SqlDbType.VarChar)

            ds = sp.ReturnDataSet()

            If ds.Tables.Count > 0 AndAlso ds.Tables(0).Rows.Count > 0 Then
                For Each dataRow As DataRow In ds.Tables(0).Rows
                    Return dataRow("CantidadRelaciones")
                Next
            End If

            Return 99

        Catch ex As Exception
            Return 99
        End Try
    End Function
    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function GetNombreFondo(Serie As FondoDTO) As List(Of FondoDTO)
        Dim ListaFondo As List(Of FondoDTO) = New List(Of FondoDTO)
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)
        Dim ds As DataSet

        Try
            sp.AgregarParametro("Accion", "SELECT_NOMBRE_FONDO", System.Data.SqlDbType.VarChar)
            ds = sp.ReturnDataSet()

            For Each dataRow As DataRow In ds.Tables(0).Rows
                Dim Sus = New FondoDTO
                With Sus
                    .RazonSocial = dataRow("Fn_Razon_Social").ToString().Trim()
                End With
                ListaFondo.Add(Sus)
            Next

        Catch ex As Exception
            Throw
        End Try

        Return ListaFondo
    End Function
    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function GetNombreFondoHitos(Serie As FondoDTO) As List(Of FondoDTO)
        Dim ListaFondo As List(Of FondoDTO) = New List(Of FondoDTO)
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)
        Dim ds As DataSet

        Try
            sp.AgregarParametro("Accion", "SELECT_NOMBRE_FONDO_HITOS", System.Data.SqlDbType.VarChar)
            ds = sp.ReturnDataSet()

            For Each dataRow As DataRow In ds.Tables(0).Rows
                Dim Sus = New FondoDTO
                With Sus
                    .RazonSocial = dataRow("Fn_Razon_Social").ToString().Trim()
                End With
                ListaFondo.Add(Sus)
            Next

        Catch ex As Exception
            Throw
        End Try

        Return ListaFondo
    End Function

    Public Function RutByNombreFondo(fondo As FondoDTO) As List(Of FondoDTO)
        Dim ListaFondo As List(Of FondoDTO) = New List(Of FondoDTO)
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)
        Dim ds As DataSet

        Try
            sp.AgregarParametro("Accion", "SELECT_RUT_BY_NOMBRE_FONDO", System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("FnRazonSocial", fondo.RazonSocial, System.Data.SqlDbType.VarChar)
            ds = sp.ReturnDataSet()

            For Each dataRow As DataRow In ds.Tables(0).Rows
                Dim Sus = New FondoDTO
                With Sus
                    .Rut = dataRow("FN_RUT").ToString().Trim()
                End With
                ListaFondo.Add(Sus)
            Next

        Catch ex As Exception
            Throw
        End Try

        Return ListaFondo
    End Function
    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function GetNombresFondo(fondoSerie As FondoSerieDTO) As List(Of FondoDTO)
        Dim Listafondo As List(Of FondoDTO) = New List(Of FondoDTO)
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure(SP_CRUD)
        Dim ds As DataSet
        Dim fondo As New FondoDTO

        Try
            sp.AgregarParametro("Accion", "SELECT_NOMBRE_BY_NEMOTECNICO", System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("FnRut", fondoSerie.Rut, System.Data.SqlDbType.VarChar)
            sp.AgregarParametro("Nemotecnico", fondoSerie.Nemotecnico, System.Data.SqlDbType.VarChar)

            ds = sp.ReturnDataSet()
            For Each dataRow As DataRow In ds.Tables(0).Rows
                fondo = New FondoDTO
                With fondo
                    .RazonSocial = dataRow("FN_Razon_Social").ToString().Trim()
                End With

                Listafondo.Add(fondo)
            Next


        Catch ex As Exception
            Throw
        End Try

        Return Listafondo
    End Function

    Private Sub FillParameters(fondo As FondoDTO, sp As DBSqlServer.SqlServer.StoredProcedure)
        sp.AgregarParametro("FnRut", fondo.Rut, System.Data.SqlDbType.VarChar)
        sp.AgregarParametro("FnRazonSocial", fondo.RazonSocial, System.Data.SqlDbType.VarChar)
        sp.AgregarParametro("FnNombreCorto", fondo.NombreCorto, System.Data.SqlDbType.VarChar)
        sp.AgregarParametro("FnEstado", fondo.Estado, System.Data.SqlDbType.Int)
        sp.AgregarParametro("FnUsuario", fondo.UsuarioIngreso, System.Data.SqlDbType.VarChar)

        sp.AgregarParametro("CuotasEmitidas", fondo.CuotasEmitidas, System.Data.SqlDbType.Decimal)
        sp.AgregarParametro("FechaEmision", fondo.FechaEmision, System.Data.SqlDbType.Date)
        sp.AgregarParametro("FechaVencimiento", fondo.FechaVencimiento, System.Data.SqlDbType.Date)
        sp.AgregarParametro("Acumulado", fondo.Acumulado, System.Data.SqlDbType.Decimal)

    End Sub

    Private Function fillObject(dataRow As DataRow) As FondoDTO
        Dim fondo As New FondoDTO

        With fondo
            .Rut = dataRow("FN_RUT").ToString().Trim()
            .RazonSocial = dataRow("FN_Razon_Social").ToString().Trim()
            .NombreCorto = dataRow("FN_Nombre_Corto").ToString().Trim()
            .Estado = dataRow("FN_Estado").ToString().Trim()
            .FechaIngreso = dataRow("FN_Fecha_Ingreso")
            .UsuarioIngreso = dataRow("FN_Usuario_Ingreso")
            .FechaModificacion = dataRow("FN_Fecha_Modificacion")
            .UsuarioModificacion = dataRow("FN_Usuario_Modificacion")

            .CuotasEmitidas = If(IsDBNull(dataRow("FN_Cuotas_Emitidas")), Nothing, dataRow("FN_Cuotas_Emitidas"))
            .FechaEmision = dataRow("FN_Fecha_Emision")
            .FechaVencimiento = If(IsDBNull(dataRow("FN_Fecha_Vencimiento")), Nothing, dataRow("FN_Fecha_Vencimiento"))
            .Acumulado = If(IsDBNull(dataRow("FN_Acumulado")), Nothing, dataRow("FN_Acumulado"))

        End With
        Return fondo
    End Function


End Class