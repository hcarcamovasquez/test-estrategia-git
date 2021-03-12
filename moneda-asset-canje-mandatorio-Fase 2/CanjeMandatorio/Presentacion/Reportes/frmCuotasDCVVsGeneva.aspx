<%@ Page Title="" Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false" CodeFile="frmCuotasDCVVsGeneva.aspx.vb" Inherits="Presentacion_Reportes_frmCuotasDCVVsGeneva" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
<asp:ScriptManager ID="ScriptManager1" runat="server" />

    
    <h2 class="TdRedondeado titleMant">Generar reporte control de cuotas DCV vs Geneva</h2>

       <div class="card p-4 jumbotron">
        <div class="row">
            <!-- FECHA ejecución -->
            <div class="col-md-3">
                <asp:Label runat="server" ID="Label2">Fecha de ejecución</asp:Label>
                <div class="input-group">
                    <asp:TextBox ID="txtFechaEjecucion" runat="server" CssClass="form-control datepicker" ReadOnly="True"></asp:TextBox>
                    <asp:LinkButton ID="lnkbtnFechaEjecucion" class="btn btn-moneda" runat="server"
                        OnClientClick="return clickCalendar('txtFechaEjecucion')"><i class="far fa-calendar-alt"></i></asp:LinkButton>

                    <asp:LinkButton ID="BtnLimpiarFechaHasta" Text="" class="btn btn-secondary ml-1" runat="server"
                        OnClientClick="return limpiarCalendar('txtFechaEjecucion')"><i class="far fa-trash-alt"></i></asp:LinkButton>
                </div>
            </div>
        </div>

        <!-- BOTONES BUSCAR LIMPIAR Y CREAR -->
        <div class="row text-center mt-5 p-3 border-bottom">
            <div class="col-md-12">
                <!-- BOTÓN BUSCAR -->
                <asp:Button ID="btnGenerarInforme" Text="Generar Informe" class="btn btn-moneda" runat="server"/>
                <asp:Button ID="btnLimpiarFrm" Text="Limpiar" class="btn btn-secondary" runat="server" />
            </div>
        </div>

        <asp:HiddenField ID="txtAccionHidden" runat="server" />

        <!-- TABLA DE RESULTADOS -->
        <h5 class="mt-3 text-secondary"><i class="fas fa-file-invoice fa-sm"></i>Resultado de la búsqueda</h5>
        <div class="table-responsive card mt-4 p-3">
            <asp:GridView
                ID="grvEjecuciones"
                runat="server"
                CssClass="table table-bordered table-hover table-sm gvv"
                HeaderStyle-BackColor=""
                HeaderStyle-Font-Size=""
                RowStyle-Font-Size="Small"
                AutoGenerateColumns="false"
                AllowSorting="false">
                <Columns>
                    <asp:BoundField DataField="DCV_Nemo" HeaderText="Nemo DCV" ItemStyle-HorizontalAlign="Left"/>
                    <asp:BoundField DataField="DCV_Cuotas" HeaderText="Cuotas Colocadas DCV"  ItemStyle-HorizontalAlign="Right"/>

                    <asp:BoundField DataField="GNV_Nemo" HeaderText="Nemo Geneva" ItemStyle-HorizontalAlign="Left"/>
                    <asp:BoundField DataField="GNV_Clase" HeaderText="Clase Geneva" ItemStyle-HorizontalAlign="Left"/>

                    <asp:BoundField DataField="GNV_Cuotas" HeaderText="Cuotas Colocadas Geneva"  ItemStyle-HorizontalAlign="Right"  DataFormatString="{0:N0}"/>
                    <asp:BoundField DataField="TRS_Rescates" HeaderText="Rescates"  ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N2}"/>
                    <asp:BoundField DataField="TRS_Suscripciones" HeaderText="Suscripciones"  ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N2}"/>
                    <asp:BoundField DataField="TRS_Canje" HeaderText="Canjes"  ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N2}"/>
                    <asp:BoundField DataField="Recompra" HeaderText="Recompra"  ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N2}"/>
                    <asp:BoundField DataField="Total" HeaderText="Total"  ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N2}"/>
                    <asp:BoundField DataField="Diferencia" HeaderText="Diferencia"  ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N2}"/>
                    <asp:BoundField DataField="Observaciones" HeaderText="Observaciones"  ItemStyle-HorizontalAlign="Left"/>

                    <%--<asp:BoundField DataField="Fecha_DCV" HeaderText="Fecha DCV" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd/MM/yyyy}"/>
                    <asp:BoundField DataField="Fecha_VC" HeaderText="Fecha VC" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd/MM/yyyy}" />
                    <asp:BoundField DataField="GNV_Clase" HeaderText="Estado" ItemStyle-HorizontalAlign="Left"/>--%>

                </Columns>
            </asp:GridView>
        </div>
        <div class="row mt-4">
            <div class="col-md-12 text-center">
                <asp:Button ID="BtnExportar" class="btn btn-success" Text="Exportar" runat="server" Enabled="false" />                
            </div>
        </div>
     </div>
     <!-- Bootstrap Modal Dialog Mensajes-->
    <div class="modal fade" id="myModalmg" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content text-center">
                <div class="modal-header mx-auto">
                    <asp:image id="img_modal" imageurl="~/Img/info1.png" runat="server" width="130" height="50" />
                    <h4 class="modal-title">
                        <asp:label id="lblModalTitle" runat="server" text="" font-bold="true" font-size="X-Large">
                        </asp:label>
                    </h4>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Cerrar">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <asp:label id="lblModalBody" runat="server" font-size="X-Large" text=""></asp:label>
                    <br>
                    <br />
                    <asp:hyperlink id="Archivo" runat="server"></asp:hyperlink>
                    <div class="text-center">
                        <asp:image id="img_body_modal" runat="server" imageurl="~/Img/important.png" width="100" height="100" />
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal" onclick="cerrarAlert();">Cerrar</button>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

<asp:Content ContentPlaceHolderID="FooterScript" runat="Server">
    <script src="<%=ResolveUrl("~/Scripts/jquery.dataTables.min.js")%>"></script>
    <script src="<%=ResolveUrl("~/Scripts/dataTables.bootstrap4.min.js")%>"></script>
    <script src="<%=ResolveUrl("~/Scripts/scripts.js")%>"></script>
    <script src="<%=ResolveUrl("~/Scripts/select2.min.js")%>"></script>
    <script src="<%=ResolveUrl("~/Scripts/date-dd-mmm-yyyy.js")%>"></script>

    <style>
        .datepicker {
            z-index: 1600 !important;
        }
    </style>

    <script>
        $(document).ready(function () {
            var txtHiddenAccion = $('#<%=txtAccionHidden.ClientID %>').val();
            $("[id*=txtFechaEjecucion]").datepicker();

            if (txtHiddenAccion == "MOSTRAR_DIALOGO") {
                $('#myModalmg').modal();
                $('#<%=txtAccionHidden.ClientID %>').val("");
            } 
        })

        function formatState(state) {
            if (!state.id) {
                return state.text;
            }
            var $state = $(
                '<span>' + state.text + '</span>'
            );
            return $state;
        };

         $(".js-select2-rut").select2({
                templateResult: formatState,
                placeholder: 'Selecciona una opción'
            });
    </script>

</asp:Content>

