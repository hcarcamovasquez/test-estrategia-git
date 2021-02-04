<%@ Page Title="Ventana Rescates" Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false" CodeFile="frmMantenedorVentanasRescate.aspx.vb" Inherits="Presentacion_Mantenedores_frmMantenedorVentanasRescate" %>

<script runat="server">

    Protected Sub RowSelector_CheckedChanged(sender As Object, e As EventArgs)

    End Sub
</script>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" />

    <asp:HiddenField ID="txtAccionHidden" runat="server" />

    <asp:HiddenField ID="txtEstadoCargueCrear" runat="server" />

    <asp:TextBox ID="txtModalVariableEstado" runat="server" Visible="false"></asp:TextBox>
    <asp:TextBox ID="txtModalVariableUsuarioIngreso" runat="server" Visible="false"></asp:TextBox>
    <asp:TextBox ID="txtModalVariableFechaIngreso" runat="server" Visible="false"></asp:TextBox>

    <h2 class="TdRedondeado titleMant">Maestro de <strong>Ventanas Rescates</strong></h2>

    <div class="card p-4 jumbotron">

        <div class="row">
            <!-- RUT del Fondo-->
            <div class="col-md-6">
                <asp:Label runat="server" ID="Label1">Fondo</asp:Label>
                <asp:DropDownList ID="ddlNombreFondoBuscar" CssClass="form-control js-select2-rut" runat="server" />
            </div>

            <!-- Nombre del Aportante-->
            <div class="col-md-6">
                <asp:Label runat="server" ID="Label2">Nemotécnico</asp:Label>
                <asp:DropDownList ID="ddlNemotecnicoBuscar" CssClass="form-control js-select2-rut" runat="server" />
            </div>
            
        </div>

        <div class="row mt-3">
            <div class="col-md-5">
                <asp:Label runat="server" ID="x" Visible="false"></asp:Label>
            </div>
            <div class="col-md-5">
                <asp:Label runat="server" ID="y" Visible="false"></asp:Label>
            </div>
        </div>
        
 <!-- BOTONES BUSCAR LIMPIAR Y CREAR --> 

        
        <div class="row text-center mt-5 p-3 border-bottom">
          <!-- BOTÓN BUSCAR -->
            <div class="col-md-12">
                <asp:Button ID="BtnBuscar" Text="Buscar" class="btn btn-moneda" runat="server"/>
                <asp:Button ID="btnLimpiarFrm" Text="Borrar" class="btn btn-secondary" runat="server" OnClick="btnLimpiarFrm_Click" />
            
          <!-- BOTÓN CREAR -->
                <asp:Button ID="btnCrear" Text="Crear" class="btn btn-info" runat="server" />
             </div>
        </div>

<!-- TABLA DE RESULTADOS -->
  <h5 class="mt-3 text-secondary"><i class="fas fa-file-invoice fa-sm"></i> Resultado de la búsqueda</h5>
        <div class="table-responsive card mt-4 p-3">
            <asp:GridView
                ID="GrvTabla"
                runat="server"
                CssClass="table table-bordered table-hover table-sm gvv"
                HeaderStyle-BackColor=""
                HeaderStyle-Font-Size=""
                RowStyle-Font-Size="Small"
                AutoGenerateColumns="False" EnableModelValidation="True">
                <Columns>
                    <asp:TemplateField HeaderText="Select">
                        <ItemTemplate>
                            <asp:RadioButton ID="RadioButton1" runat="server" onclick="checkRadioBtn(this);" GroupName="a"  />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="FN_Nombre_Corto" HeaderText="Fondo" />
                    <asp:BoundField DataField="FS_Nemotecnico" HeaderText="Nemotécnico" />
                    <asp:BoundField DataField="RES_Fecha_Solicitud" HeaderText="Fecha Solicitud" DataFormatString="{0:dd/MM/yyyy}"/>
                    <asp:BoundField DataField="VTRES_Fecha_NAV" HeaderText="Fecha NAV" DataFormatString="{0:dd/MM/yyyy}"/>
                    <asp:BoundField DataField="VTRES_Fecha_Pago" HeaderText="Fecha Pago" DataFormatString="{0:dd/MM/yyyy}"/>
                    <asp:BoundField DataField="VTRES_Usuario_Ingreso" HeaderText="Usuario Ingreso" />
                    <asp:BoundField DataField="VTRES_Fecha_Ingreso" HeaderText="Fecha Ingreso" DataFormatString="{0:dd/MM/yyyy}"/>
                    <asp:BoundField DataField="VTRES_Usuario_Modificacion" HeaderText="Usuario Modificador" />
                    <asp:BoundField DataField="VTRES_Fecha_Modificacion" HeaderText="Fecha Modificación" DataFormatString="{0:dd/MM/yyyy}"/>
                    <asp:BoundField DataField="FN_RUT" HeaderText="Rut Fondo"/>

                    <%--<asp:BoundField DataField="VTRES_Estado" HeaderText="Estado" />--%>
                </Columns>
            </asp:GridView>
        </div>

        <div class="row mt-4">
            <div class="col-md-12 text-center">
                <asp:Button ID="BtnModificar" runat="server" class="btn btn-info" Text="Modificar" Enabled="false" OnClick="BtnModificar_Click"></asp:Button>
                <asp:Button ID="BtnEliminar" runat="server" class="btn btn-danger" Text="Eliminar" 
                        OnClientClick="if (!confirm('¿Seguro que desea Eliminar los elementos seleccionados?')) return false;" 
                    Enabled="false"></asp:Button>
                <asp:Button ID="BtnExportar" class="btn btn-success" Text="Exportar" runat="server" Enabled="false" />
            </div>
        </div>
    </div>

    <!-- Bootstrap Modal Dialog Crear/Modificar -->
    <div class="modal fade" id="myModal" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" data-keyboard="false" data-backdrop="static">
        <div class="modal-dialog" style="max-width: 90%;">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title">
                        <asp:Label ID="Label5" runat="server" Text="Formulario - Ventanas Rescates" Font-Bold="true" Font-Size="X-Large">
                        </asp:Label>
                    </h4>
                    <button id="btnXCerrar" type="button" class="close" data-dismiss="modal" aria-label="Close" runat="server"><span aria-hidden="true">&times;</span></button>
                </div>
                <div class="modal-body">
                    <div class="jumbotron">
                        <div class="p-4">
                            <div class="row">
                                <!-- FORMULARIO-->
                                <div class="col-md-5 p-4">
                                    <hr />
                                    <br />
                                    <asp:HiddenField ID="txtHiddenOldValue" runat="server"></asp:HiddenField>

                                    <!-- FORMULARIO -->
                                    <div class="row mt-3">
                                        <div class="col-md-6">
                                            <label class="form-control-label">Nombre Fondo</label>
                                        </div>
                                        <div class="col-md-6">
                                            <asp:UpdatePanel runat="server" ID="UpdatePanel3" UpdateMode="Conditional">
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="ddlModalNemotecnico" EventName="SelectedIndexChanged" />
                                                    <asp:AsyncPostBackTrigger ControlID="btnModalAgregar" EventName="Click" />
                                                </Triggers>
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="ddlModalNombreFondo" CssClass="form-control js-select2-rut" runat="server" 
                                                        OnSelectedIndexChanged="CargarNemotecnicoPorNombreFondoModal" AutoPostBack="true" />
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                    <div class="row mt-3">
                                        <div class="col-md-6">
                                            <label class="form-control-label">Nemotécnico</label>
                                        </div>
                                        <div class="col-md-6">
                                            <asp:UpdatePanel runat="server" ID="UpdatePanel" UpdateMode="Conditional">
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="ddlModalNombreFondo" EventName="SelectedIndexChanged" />
                                                    <asp:AsyncPostBackTrigger ControlID="ddlModalNemotecnico" EventName="SelectedIndexChanged" />
                                                    <asp:AsyncPostBackTrigger ControlID="btnModalAgregar" EventName="Click" />
                                                </Triggers>
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="ddlModalNemotecnico" CssClass="form-control js-select2-rut" runat="server" OnSelectedIndexChanged="CargarNombreFondoPorNemotecnicoModal" AutoPostBack="true" />
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                    <div class="row mt-3">
                                        <div class="col-md-5">
                                            <label class="form-control-label">Fecha de Solicitud</label>
                                        </div>
                                        <div class="col-md-7">
                                            <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Conditional">
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="lnkBtnModalFechaSolicitud" EventName="Click" />
                                                </Triggers>
                                                <ContentTemplate>
                                                    <div class="input-group"> 
                                                    <asp:TextBox ID="txtModalFechaSolicitud" runat="server" CssClass="form-control datepicker" enabled="false"></asp:TextBox>
                                                    <asp:LinkButton ID="lnkBtnModalFechaSolicitud" Class="btn btn-moneda" runat="server" OnClientClick="return clickCalendar('txtModalFechaSolicitud')"><i class="far fa-calendar-alt"></i></asp:LinkButton>
                                                    <asp:LinkButton ID="BtnLimpiarFechaDesde" Text="" class="btn btn-secondary ml-1" OnClientClick="return limpiarCalendar('txtModalFechaSolicitud')" runat="server"><i class="far fa-trash-alt"></i></asp:LinkButton>
                                                   </div>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>

                                    <div class="row mt-3">
                                        <div class="col-md-5">
                                            <label class="form-control-label">Fecha NAV</label>
                                        </div>
                                        <div class="col-md-7">
                                            <asp:UpdatePanel runat="server" ID="UpdatePanel2" UpdateMode="Conditional">
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="lnkBtnModalFechaNAV" EventName="Click" />
                                                </Triggers>
                                                <ContentTemplate>
                                                    <div class="input-group"> 
                                                    <asp:TextBox ID="txtModalFechaNAV" runat="server" CssClass="form-control datepicker" enabled="false"></asp:TextBox>
                                                    <asp:LinkButton ID="lnkBtnModalFechaNAV" Class="btn btn-moneda" runat="server" OnClientClick="return clickCalendar('txtModalFechaNAV')"><i class="far fa-calendar-alt"></i></asp:LinkButton>
                                                    <asp:LinkButton ID="lnkBtnModalBorrarFechaNAV" Text="" OnClientClick="return limpiarCalendar('txtModalFechaNAV')" class="btn btn-secondary ml-1" runat="server"><i class="far fa-trash-alt"></i></asp:LinkButton>
                                                    </div>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>

                                    <div class="row mt-3">
                                        <div class="col-md-5">
                                            <label class="form-control-label">Fecha Pago</label>
                                        </div>
                                        <div class="col-md-7">
                                            <asp:UpdatePanel runat="server" ID="UpdatePanel4" UpdateMode="Conditional">
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="lnkBtnModalFechaPago" EventName="Click" />
                                                    <%--<asp:AsyncPostBackTrigger ControlID="RowSelector" EventName="OnCheckedChanged" />--%>
                                                </Triggers>
                                                <ContentTemplate>
                                                    <div class="input-group">
                                                    <asp:TextBox ID="txtModalFechaPago" runat="server" CssClass="form-control datepicker" enabled="false"></asp:TextBox>
                                                    <asp:LinkButton ID="lnkBtnModalFechaPago" Class="btn btn-moneda" runat="server" OnClientClick="return clickCalendar('txtModalFechaPago')"><i class="far fa-calendar-alt"></i></asp:LinkButton>
                                                    <asp:LinkButton ID="lnkBtnModalBorrarFechaPago" Text="" class="btn btn-secondary ml-1" OnClientClick="return limpiarCalendar('txtModalFechaPago')" runat="server"><i class="far fa-trash-alt"></i></asp:LinkButton>
                                                  
                                                </div>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                    <!-- FIN FORMULARIO -->
                                    <asp:HiddenField ID="txtEstadoCambio" runat="server"></asp:HiddenField>
                                    <asp:HiddenField ID="txtControlesHidden" runat="server" />
                                    <!-- GRUPO DE BOTONES -->
                                    <div class="form-group mt-5">
                                        <div class="col-md-offset-1">
                                            <asp:Button ID="btnModalAgregar" Text="Agregar" CssClass="btn btn-success btnModalAgregar" runat="server" Enabled="true"></asp:Button>
                                            <asp:Button ID="btnModalModificar" Text="Modificar" CssClass="btn btn-info" runat="server" OnClientClick="javascript:return confirma('¿Seguro que desea Guardar?');" Enabled="false"></asp:Button>
                                            <asp:Button ID="btnModalEliminarCertificado" Text="Eliminar R." runat="server" class="btn btn-danger" OnClientClick="if (!confirm('¿Seguro que desea Eliminar el Certificado seleccionado?')) return false;" Enabled="false"></asp:Button>

                                        </div>
                                    </div>
                                    <!-- FIN GRUPO DE BOTONES -->
                                </div>
                                <!-- TABLA -->
                                <div class="card col-md-7 p-4">

                                    <h4>Asignación</h4>
                                    <hr />
                                    <br />
                                    <asp:UpdatePanel runat="server" ID="UpdatePanelGrilla" UpdateMode="Conditional">
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="btnModalAgregar" EventName="Click" />
                                            <asp:AsyncPostBackTrigger ControlID="btnModalModificar" EventName="Click" />
                                            <asp:AsyncPostBackTrigger ControlID="btnModalEliminarCertificado" EventName="Click" />
                                            <asp:AsyncPostBackTrigger ControlID="ddlModalNemotecnico" EventName="SelectedIndexChanged" />
                                            <asp:AsyncPostBackTrigger ControlID="ddlModalNombreFondo" EventName="SelectedIndexChanged" />
                                            <asp:AsyncPostBackTrigger ControlID="lnkBtnModalFechaSolicitud" EventName="Click" />
                                            <asp:AsyncPostBackTrigger ControlID="lnkBtnModalFechaNAV" EventName="Click" />
                                            <asp:AsyncPostBackTrigger ControlID="lnkBtnModalFechaPago" EventName="Click" />
                                            <asp:AsyncPostBackTrigger ControlID="BtnLimpiarFechaDesde" EventName="Click" />
                                            <asp:AsyncPostBackTrigger ControlID="lnkBtnModalBorrarFechaNAV" EventName="Click" />
                                            <asp:AsyncPostBackTrigger ControlID="lnkBtnModalBorrarFechaPago" EventName="Click" />
                                        </Triggers>
                                        <ContentTemplate>

                                            <asp:GridView ID="grvAsignacion"
                                                runat="server"
                                                CssClass="table table-bordered table-hover table-sm gvv2"
                                                HeaderStyle-BackColor=""
                                                HeaderStyle-Font-Size=""
                                                RowStyle-Font-Size="Small"
                                                AutoGenerateColumns="False"
                                                AllowSorting="false">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Sel.">
                                                        <ItemTemplate>
                                                            <asp:RadioButton ID="RowSelector" runat="server" onclick="checkRadioBtn2(this);"/>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="VTRES_ID" HeaderText="ID" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol"/>
                                                    <asp:BoundField DataField="FN_Nombre_Corto" HeaderText="Fondo" />
                                                    <asp:BoundField DataField="FS_Nemotecnico" HeaderText="Nemotécnico" />
                                                    <asp:BoundField DataField="RES_Fecha_Solicitud" HeaderText="Fecha Solicitud" DataFormatString="{0:dd/MM/yyyy}"/>
                                                    <asp:BoundField DataField="VTRES_Fecha_NAV" HeaderText="Fecha NAV" DataFormatString="{0:dd/MM/yyyy}"/>
                                                    <asp:BoundField DataField="VTRES_Fecha_Pago" HeaderText="Fecha Pago" DataFormatString="{0:dd/MM/yyyy}"/>

                                                </Columns>
                                            </asp:GridView>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>

                            <!-- GRUPO DE BOTONES 2 -->
                            <div class="form-group mt-5 text-right">
                                <div class="col-md-offset-1">
                                    <asp:Button ID="btnModalGuardar" Text="Guardar" CssClass="btn btn-info" runat="server" Enabled="false"></asp:Button>
                                    <asp:Button ID="btnModalCancelar" Text="Cancelar" CssClass="btn btn-secondary" runat="server" OnClientClick="if (!confirm('¿Seguro que desea Cancelar?')) return false;"></asp:Button>
                                    <asp:Button ID="btnModalEliminarGrupo" Text="Eliminar" runat="server" class="btn btn-danger" OnClientClick="if (!confirm('¿Seguro que desea Eliminar los elementos seleccionados?')) return false;"></asp:Button>

                                </div>
                            </div>
                            <!-- FIN GRUPO DE BOTONES 2 -->
                        </div>
                    </div>
                </div>

            </div>
        </div>
    </div>
    <!-- End Bootstrap Modal Dialog Crear/Modificar -->



    <!-- Bootstrap Modal Dialog Mensajes-->
    <div class="modal fade" id="myModalmg" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content text-center">
                <div class="modal-header mx-auto">
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

<asp:Content ContentPlaceHolderID="FooterScript" runat="Server">
    <script src="<%=ResolveUrl("~/Scripts/jquery.dataTables.min.js")%>"></script>
    <script src="<%=ResolveUrl("~/Scripts/dataTables.bootstrap4.min.js")%>"></script>
    <script src="<%=ResolveUrl("~/Scripts/scripts.js")%>"></script>
    <script src="<%=ResolveUrl("~/Scripts/select2.min.js")%>"></script>
    <script src="<%=ResolveUrl("~/Scripts/date-dd-mmm-yyyy.js")%>"></script>
    <style type="text/css">
        .hiddencol {
            display: none;
        }
         .datepicker {
            z-index: 1600 !important;
        }
    </style>
    <script>

        function calendarInitial() {
            $("[id*=txtModalFechaSolicitud]").datepicker();
            $("[id*=txtModalFechaSolicitud]").datepicker({
                container: '#myModal modal-body'
                , showOn: "none"
            });

            $("[id*=txtModalFechaNAV]").datepicker();
            $("[id*=txtModalFechaNAV]").datepicker({
                container: '#myModal modal-body'
                , showOn: "none"
            });

            
            $("[id*=txtModalFechaPago]").datepicker();
            $("[id*=txtModalFechaPago]").datepicker({
                container: '#myModal modal-body'
                , showOn: "none"
            });
        };


        $(document).ready(function () {
            //checkGrilla();

            $(".table-bordered").css("width", "100%");

            var txtAccionHidden = $("#<%=txtAccionHidden.ClientID %>").val();
            if ((txtAccionHidden == "MODIFICAR") || (txtAccionHidden == "ELIMINAR") || (txtAccionHidden == "CREAR")) {
                $('#myModal').modal('show');
            }

           
            //$("body").on("click", "#btnXCerrar", function () {
            //    $("#btnXCerrar").val("");
            //});

            $("#<%=ddlModalNemotecnico.ClientID%>").change(function () {
                var txtAccionHidden = $("#<%=txtAccionHidden.ClientID %>").val();
                if (txtAccionHidden == "CREAR") {
                    return true;
                }
                if (txtAccionHidden == "MODIFICAR") {
                    return false;
                }
                return false;
            });

            seteaBotonGuardar();
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(bindDataTable);
            calendarInitial();

            //ocultarColumnas();
            
            
        });

        function ocultarColumnas() {
            var table = $('#<%=GrvTabla.ClientID%>').DataTable();
            table.columns([10]).visible(false, false);   // Oculta las columnas Indeseadas
            table.columns.adjust();
            table.draw(false);    // redibuja la grilla
        }

        function seteaBotonGuardar() {
            $("#<%=btnModalGuardar.ClientID %>").unbind("click");
            $("#<%=btnModalGuardar.ClientID %>").click(function () {
                if (!confirm('¿Confirma que desea Guardar?')) {
                    return false; Seguro
                }
                else {
                    return true;
                }
            });
        }



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
        function enableDisableButtons(newValue) {
            var btnModificar = document.getElementById('<%=BtnModificar.ClientID%>');
            btnModificar.disabled = newValue;
            var btnEliminar = document.getElementById('<%=BtnEliminar.ClientID%>');
            btnEliminar.disabled = newValue;
        }
        function checkRadioBtn(id) {
            var gv = document.getElementById('<%=GrvTabla.ClientID %>');
            if (gv != null) {
                for (var i = 1; i < gv.rows.length; i++) {
                    var radioBtn = gv.rows[i].cells[0].getElementsByTagName("input");

                    // Check if the id not same
                    if (radioBtn[0].id != id.id) {
                        radioBtn[0].checked = false;
                    }
                    else {
                        if (!isPerfilConsulta()) {
                            enableDisableButtons(false)
                        }
                    }
                }
            }
        }


        function isPerfilConsulta() {
            var HiddenPerfil = $("#<%=HiddenPerfil.ClientID %>").val();
            var HiddenConstante = $("#<%=HiddenConstante.ClientID %>").val();

            if (HiddenPerfil == HiddenConstante || HiddenPerfil == "") {
                return true;
            }
            return false
        }


          $('.btnModalAgregar').on('click', function () {
                var table = $('.dataTable').DataTable(); 
                table.destroy();
            });


        //On UpdatePanel Refresh
        function bindDataTable() {          

            $(".gvv2").prepend($("<thead></thead>").append($(".gvv2").find("tr:first"))).DataTable({
                "lengthMenu": [[10, 15, 25, 50, -1], [10, 15, 25, 50, "All"]],
                "language": {
                    "lengthMenu": "Mostrar _MENU_ registros por página",
                    "info": "Revisando la página _PAGE_ de _PAGES_",
                    "sSearch": "Filtrar :",
                    "oPaginate": {
                        "sNext": "Siguiente",
                        "sPrevious": "Anterior"
                    },
                }
            }).on('draw', function () {
                llamarFuncion();
            });

            $(".js-select2-rut").select2({
                templateResult: formatState,
                placeholder: 'Selecciona una opción'
            });

            llamarFuncion();
            calendarInitial();
        }

        function llamarFuncion() {
            var gv = document.getElementById('<%=grvAsignacion.ClientID %>');
            var txtAccionHidden = $("#<%=txtAccionHidden.ClientID %>").val();
            if (gv != null) {
                if ((txtAccionHidden == "MODIFICAR") || (txtAccionHidden == "ELIMINAR")) {
                    for (var i = 1; i < gv.rows.length; i++) {
                        var radioBtn = gv.rows[i].cells[0].getElementsByTagName("input");
                        //radioBtn[0].checked = false;
                        enableDisableButtons2(true);
                    }

                }
                else if (txtAccionHidden == "CREAR") {
                    enableDisableButtons2((gv.rows.length > 0));
                }
            }

        }

        function checkRadioBtn2(id) {
            var gv = document.getElementById('<%=grvAsignacion.ClientID %>');

            for (var i = 1; i < gv.rows.length; i++) {
                var radioBtn = gv.rows[i].cells[0].getElementsByTagName("input");

                // Check if the id not same
                if (radioBtn[0].id != id.id) {
                    radioBtn[0].checked = false;
                }
                else {
                    enableDisableButtons2(false);
                }
            }
        }

        function enableDisableButtons2(newValue) {

            var btnModalEliminarGrupo = document.getElementById('<%=btnModalEliminarGrupo.ClientID%>');
            var btnModalGuardar = document.getElementById('<%=btnModalGuardar.ClientID%>');
            var btnModalAgregar = document.getElementById('<%=btnModalAgregar.ClientID%>');
            var btnModificar = document.getElementById('<%=btnModalModificar.ClientID%>');

            var txtModalFechaSolicitud = document.getElementById('<%=txtModalFechaSolicitud.ClientID%>');
            var txtModalFechaNAV = document.getElementById('<%=txtModalFechaNAV.ClientID%>');
            var txtModalFechaPago = document.getElementById('<%=txtModalFechaPago.ClientID%>');
            var ddlModalNombreFondo = document.getElementById('<%=ddlModalNombreFondo.ClientID%>');
            var ddlModalNemotecnico = document.getElementById('<%=ddlModalNemotecnico.ClientID%>');

            var txtAccionHidden = $("#<%=txtAccionHidden.ClientID %>").val();
            var txtEstadoCargueCrear = $("#<%=txtEstadoCargueCrear.ClientID %>").val();

            

            if (btnModalEliminarGrupo.disabled) {
                var btnModificar = document.getElementById('<%=btnModalModificar.ClientID%>');
                btnModificar.disabled = newValue;
                var btnModalEliminarCertificado = document.getElementById('<%=btnModalEliminarCertificado.ClientID%>');
                btnModalEliminarCertificado.disabled = newValue;
            }
            else {
                var btnModalEliminarCertificado = document.getElementById('<%=btnModalEliminarCertificado.ClientID%>');
                btnModalEliminarCertificado.disabled = newValue;
            }

             if (txtAccionHidden == "MODIFICAR") {
                btnModificar.disabled = false;
            } 

        }

    </script>

</asp:Content>

