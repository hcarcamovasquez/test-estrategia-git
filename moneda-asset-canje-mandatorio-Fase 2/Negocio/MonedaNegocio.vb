Imports DTO
Imports Datos

Public Class MonedaNegocio

    Public Function SelectAll() As List(Of MonedasDTO)
        Return getMonedasFromConfig()
    End Function


    Private Function getMonedasFromConfig() As List(Of MonedasDTO)
        Dim lista As List(Of String) = New List(Of String)
        Dim listaRetorno As List(Of MonedasDTO) = New List(Of MonedasDTO)
        Dim moneda As MonedasDTO

        Dim configurationAppSettings As New System.Configuration.AppSettingsReader()
        Dim listaMonedasString As String

        listaMonedasString = DirectCast(configurationAppSettings.GetValue("ListaMonedas", GetType(System.String)), String)

        lista = listaMonedasString.Split(",").ToList()

        For Each s As String In lista
            moneda = New MonedasDTO
            moneda.MNCodigo = s
            moneda.MNMoneda = s
            listaRetorno.Add(moneda)
        Next

        Return listaRetorno
    End Function



End Class
