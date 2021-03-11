Imports System.Text.RegularExpressions

Public Class ConfigPentahoDTO
    Public Property ID As Integer
    Public Property Code As String
    Public Property API_Repositorio As String
    Public Property API_Usuario As String
    Public Property API_Password As String
    Public Property API_WebProxy As String
    Public Property API_Url As String
    Public Property API_Descripcion As String

    Public Property API_Parametros As String

    Public ReadOnly Property API_UrlStatus As String
        Get
            Dim s As String = "executeJob"
            Dim url As String()
            Dim retorno As String = ""

            If Not API_Url.Trim().Equals("") Then
                url = Split(API_Url.Trim(), s)
                If url.Count() >= 0 Then
                    retorno = url(0) + "status?xml=y"
                End If
            End If
            Return retorno
        End Get
    End Property

    Public ReadOnly Property API_JobName As String
        Get
            Dim s As String = "/"
            Dim url As String()
            Dim retorno As String = ""

            If Not API_Url.Trim().Equals("") Then
                url = Split(API_Url.Trim(), s)
                If url.Count() >= 0 Then
                    retorno = url(url.Count() - 1).Trim()
                    retorno = retorno.Replace("&", "")
                End If
            End If
            Return retorno
        End Get
    End Property

End Class