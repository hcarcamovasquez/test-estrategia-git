<%@ Page Title="" Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false" CodeFile="frmMantendedorDeDocumentos.aspx.vb" Inherits="Presentacion_Mantenedores_frmMantendedorDeDocumentos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    <h2 class="TdRedondeado titleMant">Mantenedor de <strong>Número de Documento</strong></h2>

    <div class="card p-4 jumbotron">
        <asp:UpdatePanel runat="server" ID="UpdatePanelDocumento" UpdateMode="Conditional">
            <Triggers>
                <%--<asp:AsyncPostBackTrigger ControlID="btnGuardarModalDocumento" EventName="Click" />
                                           <asp:AsyncPostBackTrigger ControlID="btnCancelarModalDocumento" EventName="Click" />--%>
            </Triggers>
            <ContentTemplate>

                <!-- FORMULARIO -->
                <div class="row mt-3">
                    <div class="col-md-4">
                        <asp:Label runat="server" ID="Label8">Número Siguiente</asp:Label>
                        <asp:TextBox ID="txtModalDocumentoNumeroSiguiente" runat="server" CssClass="form-control form-control-sm" onkeypress="return soloNumeros(event)" MaxLength="10"></asp:TextBox>
                    </div>

                    <div class="col-md-4">
                        <asp:Label runat="server" ID="lblFechaHasta">Número Actual</asp:Label>
                        <asp:TextBox ID="txtModalDocumentoNumeroActual" runat="server" CssClass="form-control form-control-sm" ReadOnly="True"></asp:TextBox>
                    </div>

                    <div class="col-md-4">
                        <asp:Label runat="server" ID="Label1">Número Anterior</asp:Label>
                        <asp:TextBox ID="txtModalDocumentoNumeroAnterior" runat="server" CssClass="form-control form-control-sm" ReadOnly="True"></asp:TextBox>
                    </div>
                </div>

                <!-- FIN FORMULARIO -->
                <asp:HiddenField ID="txtAccionHidden" runat="server"></asp:HiddenField>
                <asp:HiddenField ID="HiddenField2" runat="server" />

                <!-- GRUPO DE BOTONES MODAL DOCUMENTO -->
                <div class="row form-group text-center mt-5 p-3">
                    <div class="col-md-12">
                        <asp:Button ID="btnGuardarModalDocumento" Text="Guardar" CssClass="btn btn-moneda" runat="server" OnClientClick="if (!confirm('¿Seguro que desea Guardar?')) return false;" OnClick="ActualizaDocumento"></asp:Button>
                        <%--                                                                <asp:Button ID="btnCancelarModalDocumento" Text="Cancelar" CssClass="btn btn-secondary" Width="15%" runat="server" OnClientClick="if (!confirm('¿Seguro que desea Cancelar?')) return false;"></asp:Button>--%>
                    </div>
                </div>
                <!-- FIN GRUPO DE BOTONES MODAL DOCUMENTO -->

            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    
    <!-- Bootstrap Modal Dialog Mensajes-->
    <div class="modal fade" id="myModalmg" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content text-center">
                <div class="modal-header">
                    <h4 class="modal-title">
                        <asp:Image ID="img_modal" ImageUrl="~/Img/info1.png" runat="server" Width="130" Height="50" />
                        <asp:Label ID="lblModalTitle" runat="server" Text="" Font-Bold="true" Font-Size="X-Large">
                        </asp:Label>
                    </h4>
                    <asp:Button ID="btnCerraModal" runat="server" Text="&times;" CssClass="btn rounded-circle close ml-5" />
                </div>
                <div class="modal-body">
                    <asp:Label ID="lblModalBody" runat="server" Font-Size="X-Large" Text=""></asp:Label>
                    <br>
                    <br />
                    <asp:HyperLink ID="Archivo" runat="server"></asp:HyperLink>
                    <div class="text-center">
                        <asp:Image ID="img_body_modal" runat="server" ImageUrl="~/Img/important.png" Width="100" Height="100" />
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="Button1" runat="server" CssClass="btn btn-info" Text="Cerrar" />
                </div>
            </div>
        </div>
    </div>
    <!-- End Bootstrap Modal Dialog Mensajes-->
    <asp:HiddenField ID="HiddenPerfil" runat="server" />
    <asp:HiddenField ID="HiddenConstante" runat="server" />
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="FooterScript" runat="Server">
    <script>
        function soloNumeros(e) {
            key = e.keyCode || e.which;
            tecla = String.fromCharCode(key).toLowerCase();
            letras = "0123456789.";
            especiales = "8-37-39-46";
            tecla_especial = false
            for (var i in especiales) {
                if (key == especiales[i]) {
                    tecla_especial = true;
                    break;
                }
            }
            if (letras.indexOf(tecla) == -1 && !tecla_especial) {
                return false;
            }
            else {
                return true;
            }
        }

    </script>
</asp:Content>

