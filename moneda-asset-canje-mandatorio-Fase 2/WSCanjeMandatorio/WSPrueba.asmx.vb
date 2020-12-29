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
Public Class WSPrueba
    Inherits System.Web.Services.WebService

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function HelloWorld() As String
        Return "Hola a todos"
    End Function

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Function FNConsultar(fondo As FondoDTO) As List(Of FondoDTO)
        Dim listaFondos As List(Of FondoDTO) = New List(Of FondoDTO)
        Dim sp As New DBSqlServer.SqlServer.StoredProcedure("PRC_FondosCRUD")
        Dim ds As DataSet

        Try
            sp.AgregarParametro("Accion", "SELECT_ALL", System.Data.SqlDbType.VarChar)

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
    Private Shared Sub FillParameters(fondo As FondoDTO, sp As DBSqlServer.SqlServer.StoredProcedure)
        sp.AgregarParametro("FnRut", fondo.Rut, System.Data.SqlDbType.VarChar)
        sp.AgregarParametro("FnRazonSocial", fondo.RazonSocial, System.Data.SqlDbType.VarChar)
        sp.AgregarParametro("FnNombreCorto", fondo.NombreCorto, System.Data.SqlDbType.VarChar)
        sp.AgregarParametro("FnEstado", fondo.Estado, System.Data.SqlDbType.Int)
        sp.AgregarParametro("FnUsuario", fondo.UsuarioIngreso, System.Data.SqlDbType.VarChar)
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

        End With
        Return fondo
    End Function
End Class