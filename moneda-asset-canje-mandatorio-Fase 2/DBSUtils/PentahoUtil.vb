Imports System.Net
Imports System.Net.Http
Imports System.Text
Imports DTO

Public Class PentahoUtil


    Public Shared Function EjecutarETLParametrosAPI(pentaho As ConfigPentahoDTO, ByRef ParErrores As String) As Boolean
        Dim handler As New HttpClientHandler()
        Dim client As HttpClient

        Try
            Dim TARGETURL As String

            TARGETURL = pentaho.API_Url

            If pentaho.API_Parametros <> "" Then
                TARGETURL = TARGETURL & pentaho.API_Parametros
            End If

            If pentaho.API_WebProxy <> "" Then
                handler.Proxy = New WebProxy(pentaho.API_WebProxy)
                handler.UseProxy = True
                client = New HttpClient(handler)
            Else
                client = New HttpClient()
            End If

            Dim byteArray = Encoding.ASCII.GetBytes(pentaho.API_Usuario & ":" & pentaho.API_Password)

            client.DefaultRequestHeaders.Authorization = New Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray))
            Dim response As HttpResponseMessage = client.GetAsync(TARGETURL).Result

            Dim content As HttpContent = response.Content
            ' ... Read the string.
            Dim result As String = content.ReadAsStringAsync().Result

            ' ... Display the result.
            If result IsNot Nothing AndAlso Not result.Contains("ERROR") AndAlso CInt(response.StatusCode) = 200 Then
                ParErrores = ""
                Return True
            Else
                ParErrores = result & TARGETURL
                Return False
            End If

        Catch ex As Exception
            Throw ex
            Return False
        Finally
            handler = Nothing
            client = Nothing
        End Try

    End Function
End Class
