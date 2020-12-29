Imports System.Data
Imports System.Reflection
Imports DTO
Imports Negocio

Partial Class Presentacion_Mantenedores_frmMantendedorDeDocumentos
    Inherits System.Web.UI.Page


    Public Const CONST_TITULO_DOCUMENTO As String = "Documentos"
    Public Const CONST_AGREGAR_EXISTE_DOCUMENTO As String = "El Número de Documento ya se encuentra Registrado"
    Public Const CONST_MODIFICAR_EXITO As String = "Documento modificado con éxito"

    Private Sub Presentacion_MantenedoresfrmMantenedorCertificados_Load(sender As Object, e As EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then
            DataInitial()
            txtAccionHidden.Value = ""
        End If

        ValidaPermisosPerfil()

    End Sub

    Private Sub DataInitial()
        CargaDatosDocumento()
    End Sub

    Private Sub ValidaPermisosPerfil()


        HiddenPerfil.Value = Session("PERFIL")
        HiddenConstante.Value = Constantes.CONST_PERFIL_CONSULTA

        If Session("PERFIL") = Constantes.CONST_PERFIL_CONSULTA Or Session("PERFIL") = Nothing Then
            btnGuardarModalDocumento.Enabled = False
        ElseIf Session("PERFIL") = Constantes.CONST_PERFIL_FULL Or Session("PERFIL") = Constantes.CONST_PERFIL_ADMIN Then
            btnGuardarModalDocumento.Visible = True
        End If
    End Sub


    Private Sub CargaDatosDocumento()
        Dim negocio As DocumentoNegocio = New DocumentoNegocio
        Dim DocumentoSelect As DocumentoDTO = GetDocumentoSelect()
        Dim ValoresDocumentos As DocumentoDTO = negocio.GetDatosDocumento(DocumentoSelect)

        TraerDatosDocumento(ValoresDocumentos)

        txtAccionHidden.Value = "MANTENER_MODAL"
    End Sub

    Private Sub TraerDatosDocumento(Documentos As DocumentoDTO)
        txtModalDocumentoNumeroActual.Text = Documentos.NumeroActual
        txtModalDocumentoNumeroAnterior.Text = Documentos.NumeroAnterior
        txtModalDocumentoNumeroSiguiente.Text = Documentos.NumeroSiguiente
    End Sub

    Private Function GetDocumentoSelect() As DocumentoDTO
        Dim Documento As New DocumentoDTO

        Documento.NumeroActual = 1

        Return Documento
    End Function

    Public Sub ActualizaDocumento()
        Dim negocio As DocumentoNegocio = New DocumentoNegocio
        Dim Documento As New DocumentoDTO
        Dim mensajeAccionExito As String
        mensajeAccionExito = "Documento modificado con Exito"


        Documento.NumeroSiguiente = txtModalDocumentoNumeroSiguiente.Text
        Dim existeDocumento As Boolean = negocio.ExisteDocumento(Documento)

        If existeDocumento Then

            ShowAlert(CONST_AGREGAR_EXISTE_DOCUMENTO)
        Else
            Documento.NumeroSiguiente = txtModalDocumentoNumeroSiguiente.Text
            Dim solicitudModDocumento As Integer = negocio.UpdateDocumentoNuevo(Documento)
            'ShowMesagges(CONST_TITULO_DOCUMENTO, CONST_MODIFICAR_EXITO, Constantes.CONST_RUTA_IMG + Constantes.CONST_IMG_LOGO, Constantes.CONST_RUTA_IMG + Constantes.CONST_IMG_CORRECTO)
            ShowAlert(CONST_MODIFICAR_EXITO)
            CargaDatosDocumento()
        End If
    End Sub

    Private Sub ShowAlert(mesagge As String, Optional mostrarEnPage As Boolean = False)
        Dim myScript As String = "alert('" + mesagge + "');"
        If Not mostrarEnPage Then
            ScriptManager.RegisterStartupScript(UpdatePanelDocumento, UpdatePanelDocumento.GetType(), "alert", myScript, True)
        Else
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", myScript, True)
        End If
    End Sub

    Private Sub ShowMesagges(title As String, mesagge As String, urlIconTitle As String, urlIconMesagge As String, Optional borraLink As Boolean = True)
        lblModalTitle.Text = title
        lblModalBody.Text = mesagge
        img_modal.ImageUrl = urlIconTitle
        img_body_modal.ImageUrl = urlIconMesagge

        txtAccionHidden.Value = "SHOW_DIALOG"

        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModalmg", "$('#myModalmg').modal();", True)
    End Sub
End Class
