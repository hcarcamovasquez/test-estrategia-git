Imports DTO

Public Class RelacionMonedaPaisNegocio

    Public Function SelectAll() As List(Of RelacionMonedaPaisDto)
        Return getMonedasFromConfig()
    End Function

    Public Function SelectOne(moneda As String) As String
        Dim listaRetorno As List(Of RelacionMonedaPaisDto) = New List(Of RelacionMonedaPaisDto)
        Dim objRetorno As RelacionMonedaPaisDto = New RelacionMonedaPaisDto

        listaRetorno = getMonedasFromConfig()

        objRetorno = listaRetorno.Find(Function(p) p.Moneda = moneda)

        If objRetorno Is Nothing Then
            Return ""
        Else
            Return objRetorno.Pais
        End If

    End Function


    Private Function getMonedasFromConfig() As List(Of RelacionMonedaPaisDto)
        Dim lista As List(Of String) = New List(Of String)
        Dim listaRetorno As List(Of RelacionMonedaPaisDto) = New List(Of RelacionMonedaPaisDto)
        Dim moneda As RelacionMonedaPaisDto

        Dim configurationAppSettings As New System.Configuration.AppSettingsReader()
        Dim listaMonedasString As String

        listaMonedasString = DirectCast(configurationAppSettings.GetValue("ListaMonedasPaises", GetType(System.String)), String)

        lista = listaMonedasString.Split(",").ToList()

        For Each s As String In lista
            moneda = New RelacionMonedaPaisDto
            Dim desNavC As String() = splitCharByChar(s, "=")
            moneda.Moneda = desNavC(0)
            moneda.Pais = desNavC(1)

            listaRetorno.Add(moneda)
        Next

        Return listaRetorno
    End Function

    Private Function splitCharByChar(texto As String, Optional charSplit As String = ",") As String()
        If InStr(texto, charSplit) = 0 Then
            texto += charSplit
        End If

        Return texto.Split(New Char() {charSplit})
    End Function
End Class
