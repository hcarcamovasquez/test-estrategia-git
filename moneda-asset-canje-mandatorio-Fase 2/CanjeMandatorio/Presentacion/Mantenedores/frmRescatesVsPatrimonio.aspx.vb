Imports DTO
Imports Negocio
Imports System.Net
Imports System.Net.Http

Partial Class Presentacion_Mantenedores_frmRescatesVsPatrimonio
    Inherits System.Web.UI.Page

    Private Sub Presentacion_Mantenedores_frmRescatesVsPatrimonio_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            DataInitial()
        End If
        ValidaPermisosPerfil()
    End Sub



    Private Sub ValidaPermisosPerfil()

        If Session("PERFIL") = Constantes.CONST_PERFIL_CONSULTA Then

        ElseIf Session("PERFIL") = Constantes.CONST_PERFIL_FULL Or Session("PERFIL") = Constantes.CONST_PERFIL_ADMIN Then

        End If
    End Sub

    Private Sub DataInitial()
        CargaFiltroNombre()
        ' txtFechaEjecucion.Text = Date.Now.ToString("dd/MM/yyyy")
    End Sub

    Private Sub CargaFiltroNombre()
        Dim fondo As New FondoDTO()
        Dim FondosNegocio As FondosNegocio = New FondosNegocio
        Dim lista As List(Of FondoDTO) = FondosNegocio.ConsultarPorRut(fondo)

        If lista.Count = 0 Then
            ddlListaRutFondo.Items.Insert(0, New ListItem("", ""))
        Else
            ddlListaRutFondo.DataSource = lista
            ddlListaRutFondo.DataMember = "RutRazonSocial"
            ddlListaRutFondo.DataValueField = "RutRazonSocial"
            ddlListaRutFondo.DataTextField = "RutRazonSocial"
            ddlListaRutFondo.DataBind()
            ddlListaRutFondo.Items.Insert(0, New ListItem("", ""))
        End If
    End Sub
    Protected Sub BtnBuscar_Click(sender As Object, e As EventArgs) Handles BtnBuscar.Click
        Dim negocio As EjecucionRescateVsPatrimonioNegocio = New EjecucionRescateVsPatrimonioNegocio()
        Dim ejecucionDto As EjecucionRescateVsPatrimonioDTO = New EjecucionRescateVsPatrimonioDTO()
        Dim lista As List(Of EjecucionRescateVsPatrimonioDTO)

        txtFechaEjecucion.Text = Request.Form(txtFechaEjecucion.UniqueID)

        ejecucionDto.FnRut = IIf(ddlListaRutFondo.SelectedValue = "", Nothing, ddlListaRutFondo.SelectedValue)
        ejecucionDto.FechaEjecucion = IIf(txtFechaEjecucion.Text = "", Nothing, txtFechaEjecucion.Text)

        lista = negocio.Select(ejecucionDto)
        If lista.Count() > 0 Then
            grvEjecuciones.DataSource = lista
            grvEjecuciones.DataBind()
        Else
            ShowAlert(Constantes.CONST_SIN_RESULTADOS)
        End If

    End Sub

    Private Sub ShowAlert(mesagge As String)
        Dim myScript As String = "alert('" + mesagge + "');"
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", myScript, True)
    End Sub

    Protected Sub btnGenerarInforme_Click(sender As Object, e As EventArgs) Handles btnGenerarInforme.Click
        Dim pentaho As ConfigPentahoDTO = New ConfigPentahoDTO()
        Dim negocio As ConfigPentahoNegocio = New ConfigPentahoNegocio
        Dim parErrores As String = ""

        pentaho.ID = 1

        pentaho = negocio.GetPentahoPorId(pentaho)

        If pentaho.Code = Nothing Then
            ShowAlert("Nos e encuentra la configuracion de Pentaho")
        Else
            If EjecutarETLParametrosAPI(pentaho, "p1=1234", parErrores) Then
                ShowAlert("Ejecutado correctamente")
            Else
                ShowAlert(parErrores)
            End If
        End If

    End Sub

    Public Function EjecutarETLParametrosAPI(pentaho As ConfigPentahoDTO, parametos As String, ByRef ParErrores As String) As Boolean
        Try
            Dim TARGETURL = pentaho.API_Url ' & parametos
            Dim handler As New HttpClientHandler()
            Dim client As HttpClient

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
        End Try
    End Function

End Class
