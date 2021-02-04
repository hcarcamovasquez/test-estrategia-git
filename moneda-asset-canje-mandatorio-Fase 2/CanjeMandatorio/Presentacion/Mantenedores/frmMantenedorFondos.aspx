<%@ Page Title="Fondos" Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false" CodeFile="frmMantenedorFondos.aspx.vb" Inherits="Presentacion_Mantenedores_frmMantenedorFondos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server"></asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <h2 class="TdRedondeado titleMant">Maestro de <strong>Fondos</strong></h2>

    <div class="card p-4 jumbotron">

        <div class="row">
            <!-- RUT del Fondo-->
            <div class="col-md-4">
                <asp:Label runat="server" ID="lblRut">Fondo</asp:Label>
                <asp:DropDownList ID="ddlRutFondo" CssClass="form-control js-select2-rut" runat="server" />
            </div>

            <!-- FECHA DESDE -->
            <div class="col-md-4">
                <asp:Label runat="server" ID="Label2">Fecha Creación desde</asp:Label>
                <div class="input-group">
                    <asp:TextBox ID="txtFeCreacionDesde" runat="server" CssClass="form-control datepicker" ReadOnly="True"></asp:TextBox>
                    <asp:LinkButton ID="LinkButton1" class="btn btn-moneda" runat="server" OnClientClick="return clickCalendar('txtFeCreacionDesde')"><i class="far fa-calendar-alt"></i></asp:LinkButton>
                    <asp:LinkButton ID="BtnLimpiarFechaDesde" Text="Limpiar" class="btn btn-secondary ml-1" runat="server" OnClientClick="return limpiarCalendar('txtFeCreacionDesde')"><i class="far fa-trash-alt"></i></asp:LinkButton>
                </div>
            </div>

            <!-- FECHA HASTA -->
            <div class="col-md-4">
                <asp:Label runat="server" ID="lblFechaHasta">Fecha Creación hasta</asp:Label>
                <div class="input-group">
                    <asp:TextBox ID="txtFeCreacionHasta" runat="server" CssClass="form-control  datepicker" ReadOnly="True"></asp:TextBox>
                    <asp:LinkButton ID="LinkButton2" class="btn btn-moneda" runat="server" OnClientClick="return clickCalendar('txtFeCreacionHasta')"><i class="far fa-calendar-alt"></i></asp:LinkButton>
                    <asp:LinkButton ID="BtnLimpiarFechaHasta" Text="Limpiar Fecha" class="btn btn-secondary ml-1" runat="server" OnClientClick="return limpiarCalendar('txtFeCreacionHasta')"><i class="far fa-trash-alt"></i></asp:LinkButton>
                </div>
            </div>

            <!-- NOMBRE-->
            <div class="col-md-5">
                <asp:Label runat="server" ID="lblNombreFondo" Visible="False">Nombre del fondo</asp:Label>
                <asp:DropDownList ID="ddlNombreFondo" CssClass="form-control js-select2-rut" runat="server" Visible="False" />
            </div>
        </div>

        <!-- BOTONES BUSCAR LIMPIAR Y CREAR -->
        <!-- BOTÓN BUSCAR -->
        <div class="row text-center mt-5 p-3 border-bottom">
            <div class="col-md-12">
                <asp:Button ID="BtnBuscar" Text="Buscar" class="btn btn-moneda" runat="server" OnClick="BtnBuscar_Click" />
                <asp:Button ID="btnLimpiarFrm" Text="Borrar" class="btn btn-secondary" runat="server" OnClick="btnLimpiarFrm_Click" />

                <!-- BOTÓN CREAR -->
                <asp:Button ID="btnCrear" Text="Crear" class="btn btn-info" runat="server" />
            </div>
        </div>

        <!-- TABLA DE RESULTADOS -->
        <h5 class="mt-3 text-secondary"><i class="fas fa-file-invoice fa-sm"></i>Resultado de la búsqueda</h5>
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
                            <asp:RadioButton ID="RowSelector" runat="server" onclick="checkRadioBtn(this);" GroupName="a" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Rut" HeaderText="Rut del Fondo" />
                    <asp:BoundField DataField="RazonSocial" HeaderText="Nombre del fondo" />
                    <asp:BoundField DataField="NombreCorto" HeaderText="Nombre corto" />
                    <asp:BoundField DataField="Estado" HeaderText="Estado" Visible="false" />

                    <asp:BoundField DataField="CuotasEmitidas" HeaderText="Cuotas Emitidas" DataFormatString="{0:N6}" ItemStyle-HorizontalAlign="Right"/>
                    <asp:BoundField DataField="FechaEmision" HeaderText="Fecha Emisión" DataFormatString="{0:dd-MM-yyyy}" />
                    <asp:BoundField DataField="FechaVencimiento" HeaderText="Fecha Vencimiento" DataFormatString="{0:dd-MM-yyyy}" />
                    <asp:BoundField DataField="Acumulado" HeaderText="Acumulado" DataFormatString="{0:N6}" ItemStyle-HorizontalAlign="Right"/>

                    <asp:BoundField DataField="ControlDeCuotas" HeaderText="Control de cuotas"  ItemStyle-HorizontalAlign="Center"/>

                    <asp:BoundField DataField="FechaIngreso" HeaderText="Fecha Ingreso" DataFormatString="{0:dd-MM-yyyy}" />
                    <asp:BoundField DataField="UsuarioIngreso" HeaderText="Usuario Ingreso" />
                    <asp:BoundField DataField="FechaModificacion" HeaderText="Fecha modificación" DataFormatString="{0:dd-MM-yyyy}" />
                    <asp:BoundField DataField="UsuarioModificacion" HeaderText="Usuario modificador" />
                </Columns>

                <RowStyle Font-Size="Small"></RowStyle>
            </asp:GridView>
        </div>

        <div class="row mt-4">
            <div class="col-md-12 text-center">
                <asp:Button ID="BtnModificar" runat="server" class="btn btn-info btn-primary" Text="Modificar" OnClick="BtnModificar_Click" Enabled="false"></asp:Button>
                <asp:Button ID="BtnEliminar" runat="server" class="btn btn-danger" Text="Eliminar" OnClientClick="if (!confirm('¿Seguro que desea Eliminar los elementos seleccionados?')) return false;" Enabled="false"></asp:Button>
                <asp:Button ID="BtnExportar" class="btn btn-success" Text="Exportar" runat="server" Enabled="false" />
            </div>
            <div class="col-md-2">
                <asp:Button ID="btnVolver" runat="server" Width="125px" class="btn btn-secondary" Text="Volver" OnClientClick="return volver();" Visible="False"></asp:Button>
            </div>
        </div>
        <div>
        </div>
    </div>

    <asp:HiddenField ID="txtHiddenAccion" runat="server" />
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <!-- Bootstrap Modal Dialog Crear/Modificar -->
    <div class="modal fade" id="myModal" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" data-keyboard="false" data-backdrop="static">
        <div class="modal-dialog" style="max-width: 75%;">
            <div class="modal-content text-center">
                <div class="modal-header">
                    <h4 class="modal-title">
                        <asp:Label ID="lblModalTitulo" runat="server" Text="Formulario - Maestro de fondos" Font-Bold="true" Font-Size="X-Large">
                        </asp:Label>
                    </h4>
                    <button id="btnXCerrar" type="button" class="close" data-dismiss="modal" aria-label="Close" onclick="cerrarAlert(); return true;">
                        <span aria-hidden="true" onclick="cerrarAlert(); return true;">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div class="jumbotron">
                        <div class="card p-4">
                            <br />
                            <!-- FORMULARIO -->
                            <div class="row">
                                <div class="col-md-6">
                                    <h5 class="text-secondary">Crear</h5>
                                    <div class="col-md-12 mt-3">
                                        <asp:Label runat="server" ID="lblModalRutFondo" Font-Size="Large">Rut del fondo: </asp:Label>
                                        <div class="input-group">
                                            <asp:TextBox ID="txtModalRutFondo" runat="server" MaxLength="15" CssClass="form-control" onkeypress="return soloRut(event)" onblur="validateRut(this)"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="col-md-12 mt-3">
                                        <asp:Label runat="server" ID="lblModalNombreFondo" Font-Size="Large">Nombre del fondo: </asp:Label>
                                        <div class="input-group">
                                            <asp:TextBox ID="txtModalNombreFondo" runat="server" MaxLength="100" CssClass="form-control" onkeypress="return soloLetras(event)" onblur="validarSoloLetras(this)"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-12 mt-3">
                                        <asp:Label runat="server" ID="lblNombreCorto" Font-Size="Large">Nombre corto: </asp:Label>
                                        <div class="input-group">
                                            <asp:TextBox ID="txtModalNombreCorto" runat="server" MaxLength="20" CssClass="form-control" onkeypress="return soloLetras(event)" onblur="validarSoloLetras(this)"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <!-- CONTROL DE CUOTAS SUSCRIPCIONES -->
                                    <h5 class="text-secondary">Control de cuotas emitidas</h5>
                                    <!-- CUOTAS EMITIDAS -->
                                    <div class="col-md-12 mt-3">
                                        <div class="row">
                                            <div class="col-md-8 mt-3">
                                                <asp:Label runat="server" ID="Label1">Cuotas Emitidas: </asp:Label>
                                                <div class="input-group">
                                                    <asp:TextBox ID="txtCuotasEmitidas" runat="server" CssClass="form-control dbs-entero-decimal" ></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-4 mt-3">
                                                <div class="form-check">
                                                        <asp:CheckBox ID="chkControlCuotas" runat="server" Text="" cssclass="form-check-input" />
                                                        <label class="form-check-label" for="chkControlCuotas">No Aplica</label>
                                                    </div>
                                            </div>
                                        </div>
                                    </div>
                                    <!-- FECHA DE EMISIÓN -->
                                    <div class="col-md-12 mt-3">
                                        <asp:Label runat="server" ID="Label3">Fecha de Emisión</asp:Label>
                                        <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Conditional">
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="LinkButton3" EventName="click" />
                                                <asp:AsyncPostBackTrigger ControlID="LinkButton5" EventName="click" />
                                            </Triggers>
                                            <ContentTemplate>
                                                <div class="input-group">
                                                    <asp:TextBox ID="Textbox2" runat="server" CssClass="form-control  datepicker" ReadOnly="True"></asp:TextBox>
                                                    <asp:LinkButton ID="LinkButton3" class="btn btn-moneda" Height="38px" runat="server" OnClientClick="return clickCalendar('Textbox2')"><i class="far fa-calendar-alt"></i></asp:LinkButton>
                                                    <asp:LinkButton ID="Linkbutton4" Text="Limpiar Fecha" class="btn btn-secondary ml-1" Height="38px" runat="server" OnClientClick="return limpiarCalendar('Textbox2')"><i class="far fa-trash-alt" ></i></asp:LinkButton>
                                                  
                                                </div>

                                                <!-- FECHA DE VENCIMIENTO -->
                                                <asp:Label runat="server" ID="Label4">Fecha de Vencimiento</asp:Label>
                                                <div class="input-group">
                                                    <asp:TextBox ID="Textbox3" runat="server" CssClass="form-control datepicker" ReadOnly="True"></asp:TextBox>
                                                    <asp:LinkButton ID="LinkButton5" class="btn btn-moneda" Height="38px" runat="server" OnClientClick="return clickCalendar('Textbox3')"><i class="far fa-calendar-alt"></i></asp:LinkButton>
                                                    <asp:LinkButton ID="Linkbutton6" Text="Limpiar Fecha" class="btn btn-secondary ml-1" Height="38px" runat="server" OnClientClick="return limpiarCalendar('Textbox3')"><i class="far fa-trash-alt"></i></asp:LinkButton>
                                                   
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>

                                    <!-- ACUMULADO -->
                                    <div class="col-md-12 mt-3">
                                        <asp:Label runat="server" ID="Label5">Acumulado: </asp:Label>
                                        <div class="input-group">
                                            <asp:TextBox ID="Textbox4" runat="server" CssClass="form-control dbs-entero-decimal" ></asp:TextBox>                                            
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="w-100"></div>
                            <div class="row form-group">
                                <div class="col-xs-2 col-md-2">
                                    <asp:Label runat="server" ID="Label6" Font-Size="Large">Control Rescate </asp:Label>
                                </div>

                                <div class="col-xs-2 col-md-2">
                                    <asp:DropDownList ID="ddlControlTipoControl" CssClass="form-control js-select2-rut" runat="server" AutoPostBack="False" onchange="CambiaEstadosTipoControl(this)">
                                        <asp:ListItem Value="">&nbsp;</asp:ListItem>
                                        <asp:ListItem Value="Movil">Días Móviles</asp:ListItem>
                                        <asp:ListItem Value="Ventana">Ventana</asp:ListItem>
                                    </asp:DropDownList>

                                </div>
                                <div class="col-xs-2 col-md-2">
                                    <div class="input-group">
                                        <asp:TextBox ID="txtControlDiasAVerificar" runat="server" MaxLength="2" CssClass="form-control dbs-entero"></asp:TextBox>
                                        <asp:RangeValidator ID="rangeDiasAVerificar" runat="server" ControlToValidate="txtControlDiasAVerificar" ErrorMessage="El valor debe estar entre 0 y 30 días" MaximumValue="30" MinimumValue="0" Type="Integer"></asp:RangeValidator>
                                    </div>
                                </div>

                                <div class="col-xs-2 col-md-2">
                                     <asp:DropDownList ID="ddlControlTipoDeConfiguracion" CssClass="form-control js-select2-rut" runat="server" AutoPostBack="False" onchange="CambiaEstadosConfiguracion(this)" >
                                          <asp:ListItem Value="">&nbsp;</asp:ListItem>
                                        <asp:ListItem Value="Pago">Pago</asp:ListItem>
                                        <asp:ListItem Value="Prorrata">Prorrata</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-xs-2 col-md-2">
                                        <asp:TextBox ID="txtControlCantidadDias" runat="server" MaxLength="2" CssClass="form-control dbs-entero"></asp:TextBox>
                                        <asp:RangeValidator ID="rangeMessageCantDias" runat="server" ControlToValidate="txtControlCantidadDias" ErrorMessage="El valor debe estar entre 0 y 30 días" MaximumValue="30" MinimumValue="0" Type="Integer"></asp:RangeValidator>
                                </div>
                                <div class="col-xs-2 col-md-2">
                                     <div class="form-check">
                                        <asp:CheckBox ID="chkControlDiasHabiles" runat="server" Text="" cssclass="form-check-input" />
                                        <label class="form-check-label" for="chkControlDiasHabiles">Dias Hábiles</label>
                                    </div>
                                </div>
                            </div>
                            <asp:HiddenField ID="txtHidenEstado" runat="server" />
                            <!-- GRUPO DE BOTONES 2 -->
                            <div class="form-group mt-5 text-right">
                                <div class="col-md-offset-1">
                                    <asp:Button ID="btnModalGuardarModificar" Text="Guardar" CssClass="btn btn-info" runat="server" OnClientClick="return validateBtn();"></asp:Button>
                                    <asp:Button ID="btnModalGuardarCrear" Text="Guardar" CssClass="btn btn-info" runat="server" OnClientClick="return validateBtn();"></asp:Button>
                                    <asp:Button ID="btnModalCancelar" Text="Cancelar" CssClass="btn btn-secondary" runat="server" OnClientClick="if (!confirm('¿Seguro que desea Cancelar?')) return false;"></asp:Button>
                                    <asp:Button ID="btnModalEliminarGrupo" Text="Eliminar" runat="server" class="btn btn-danger" Width="15%" OnClientClick="if (!confirm('¿Seguro que desea Eliminar los elementos seleccionados?')) return false;"></asp:Button>
                                </div>
                            </div>
                            <!-- FIN GRUPO DE BOTONES 2 -->
                        </div>
                    </div>
                </div>

                <!-- <div class="modal-footer"> </div>-->
            </div>
        </div>
    </div>
    <!-- End Bootstrap Modal Dialog Crear/Modificar -->

    <!-- Bootstrap Modal Dialog Mensajes-->
    <div class="modal fade" id="myModalmg" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" data-keyboard="false" data-backdrop="static">
        <div class="modal-dialog">
            <div class="modal-content text-center">
                <div class="modal-header mx-auto">
                    <h4 class="modal-title">
                        <asp:Image ID="img_modal" ImageUrl="~/Img/info1.png" runat="server" Width="130" Height="50" CssClass="mr-4" />
                        <asp:Label ID="lblModalTitle" runat="server" Text="" Font-Bold="true" Font-Size="X-Large">
                        </asp:Label>
                    </h4>
                    <button type="button" class="btn rounded-circle close ml-5" data-dismiss="modal">&times;</button>
                </div>
                <div class="modal-body">
                    <asp:Label ID="lblModalBody" runat="server" Font-Size="X-Large" Text=""></asp:Label>
                    <br>
                    <br />
                    <asp:HyperLink ID="linkArchivo" runat="server"></asp:HyperLink>
                    <div class="text-center">
                        <asp:Image ID="img_body_modal" runat="server" ImageUrl="~/Img/important.png" Width="100" Height="100" />
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-info" data-dismiss="modal">Cerrar</button>
                </div>
            </div>
        </div>
    </div>
    <!-- End Bootstrap Modal Dialog Mensajes-->

    <!-- INICIO Mensaje Alert -->
    <div class="modal fade" id="modalAlert" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" data-keyboard="false" data-backdrop="static">
        <div class="modal-dialog modal-dialog-centered" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="h5dialogTitle">Modal title</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Cerrar">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <p id="pMensajeAlert"></p>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal" onclick="cerrarAlert();">Cerrar</button>
                </div>
            </div>
        </div>
    </div>
    <!-- FIN Mensaje Alert -->
    <asp:HiddenField ID="HiddenPerfil" runat="server" />
    <asp:HiddenField ID="HiddenConstante" runat="server" />

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="FooterScript" runat="Server">
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
            var txtHiddenAccion = $('#<%=txtHiddenAccion.ClientID %>').val();
            confNumeros();

            if ((txtHiddenAccion == "MODIFICAR") || (txtHiddenAccion == "CREAR") || (txtHiddenAccion == "ELIMINAR")) {
                $('#myModal').modal('show');
            } else if (txtHiddenAccion == "MOSTRAR_DIALOGO") {
                $('#myModalmg').modal();

            } else {
                checkRadioBtn("");
            }

            $("[id*=txtFeCreacionDesde]").datepicker();
            $("[id*=txtFeCreacionHasta]").datepicker();

            $("[id*=Textbox2]").datepicker();
            $("[id*=Textbox3]").datepicker();


            $("[id*=txtFeCreacionDesde]").change(function () {
                changeFechas($("[id*=txtFeCreacionDesde]"), $("[id*=txtFeCreacionHasta]"), 1)
            });
            $("[id*=txtFeCreacionHasta]").change(function () {
                changeFechas($("[id*=txtFeCreacionDesde]"), $("[id*=txtFeCreacionHasta]"), 2)
            });

            $("[id*=Textbox2]").change(function () {
                changeFechas($("[id*=Textbox2]"), $("[id*=Textbox3]"), 1)
            });
            $("[id*=Textbox3]").change(function () {
                changeFechas($("[id*=Textbox2]"), $("[id*=Textbox3]"), 2)
            });

            $("[id*=Textbox2]").datepicker({
                container: '#myModal modal-body'
                , showOn: "none"
            });

            $("[id*=Textbox3]").datepicker({
                container: '#myModal modal-body'
                , showOn: "none"
            });

            

        });        


        function CambiaEstadosConfiguracion(ddl) {
            var txtCantidadDias = document.getElementById('<%=txtControlCantidadDias.ClientID%>'); 
            var ckkDiasHabiles = document.getElementById('<%=chkControlDiasHabiles.ClientID%>'); 

            if (ddl.value == "Prorrata") {
                txtCantidadDias.disabled= true;
                ckkDiasHabiles.disabled = true;
            } else {
                txtCantidadDias.disabled = false;
                ckkDiasHabiles.disabled = false;

            }
            return false;
        }

        function CambiaEstadosTipoControl(ddl) {
            var txtControlDiasAVerificar = document.getElementById('<%=txtControlDiasAVerificar.ClientID%>'); 

            if (ddl.value == "Ventana") {
                txtControlDiasAVerificar.disabled= true;
            } else {
                txtControlDiasAVerificar.disabled = false;
            }
            return false;
        }

        function confNumeros() {
            $('.dbs-entero-decimal').mask2(getMask(12, 6));            
            $('.dbs-entero').mask2(getMask(2, 0));            
        }

        function soloNumerosyComa(e) {
            key = e.keyCode || e.which;
            tecla = String.fromCharCode(key).toLowerCase();
            letras = "0123456789,";
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


        function msgAlert(mensaje) {
            $('#pMensajeAlert').text(mensaje);
            $('#h5dialogTitle').html("Error");
            $('#modalAlert').modal();
        }

        function validateBtn() {
            if (!confirm('¿Seguro que desea Guardar?')) {
                return false;
            } else {
                var rutTxt = document.getElementById('<%=txtModalRutFondo.ClientID%>');
                if (validateRut(rutTxt)) {
                    return CheckTxtEmpty();
                }
                else {
                    return false;
                }
            }
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

        function CheckTxtEmpty() {
            if ($('#<%=txtModalRutFondo.ClientID %>').val() == "-" || $('#<%=txtModalRutFondo.ClientID %>').val() == "") {
                msgAlert('El Rut es un campo obligatorio.')
                return false;
            } 
                if ($('#<%=txtModalNombreFondo.ClientID %>').val() == "") {
                    msgAlert('El nombre fondo es un campo obligatorio')
                    return false;
            } 

            if ($('#<%=txtModalNombreCorto.ClientID %>').val() == "") {
                    msgAlert('El nombre corto es un campo obligatorio')
                    return false;
            }

            var cantDias = $('#<%=txtControlCantidadDias.ClientID%>').val(); 
            if ((cantDias == "") || (cantDias > 30)) {
                msgAlert('La cantidad de dias No debe exceder a 30');
                return false;
            } 

            cantDias = $('#<%=txtControlDiasAVerificar.ClientID%>').val(); 
            if ((cantDias == "") || (cantDias > 30)) {
                msgAlert('La cantidad de dias No debe exceder a 30');
                return false;
            } 


                if ($('#<%=txtCuotasEmitidas.ClientID %>').val() != "") {
                    if (!validarValorEmitidas()) {
                        return false;
                    }
                }

                if ($('#<%=Textbox4.ClientID %>').val() != "") {
                    if (validarValorAcumulado()) {
                        return true;
                    }
                    else {
                        return false;
                    }
                }

        }

        function validarValorEmitidas() {
            return true;

            var valor1 = document.getElementById('<%=txtCuotasEmitidas.ClientID%>').value.replace(/\./g, '').replace(/\,/g, '.');         

            console.log("valor:" + valor1);
           
            var valor = parseFloat(valor1);

            console.log("float valor:" + valor);

            if (valor.match(/^\d{1,12}(\,\d{1,6})?$/)) {
                return true;
            } else {
                alert('El campo cuotas emitidas debe tener el siguiente formato (12 enteros , 6 decimales)');
                return false;
            }
        }

        function validarValorAcumulado() {
            return true;

            var valor = document.getElementById('<%=Textbox4.ClientID%>').value;
            if (valor.match(/^\d{1,12}(\,\d{1,6})?$/)) {
                return true;
            } else {
                alert('El campo acumulado debe tener el siguiente formato (12 enteros , 6 decimales)');
                return false;
            }
        }

        function validateRut(rut) {
            // Despejar Puntos
            var valor = rut.value.replace('.', '').trim();
            // Despejar Guión
            valor = valor.replace('-', '');
            //valor = valor.value.trim()

            // Aislar Cuerpo y Dígito Verificador
            cuerpo = valor.slice(0, -1);
            dv = valor.slice(-1).toUpperCase();

            // Formatear RUN
            rut.value = cuerpo + '-' + dv

            // Calcular Dígito Verificador
            suma = 0;
            multiplo = 2;

            // Para cada dígito del Cuerpo
            for (i = 1; i <= cuerpo.length; i++) {

                // Obtener su Producto con el Múltiplo Correspondiente
                index = multiplo * valor.charAt(cuerpo.length - i);

                // Sumar al Contador General
                suma = suma + index;

                // Consolidar Múltiplo dentro del rango [2,7]
                if (multiplo < 7) { multiplo = multiplo + 1; } else { multiplo = 2; }

            }

            // Calcular Dígito Verificador en base al Módulo 11
            dvEsperado = 11 - (suma % 11);

            // Casos Especiales (0 y K)
            dv = (dv == 'K') ? 10 : dv;
            dv = (dv == 0) ? 11 : dv;

            // Validar que el Cuerpo coincide con su Dígito Verificador
            if (dvEsperado != dv) {
                msgAlert("RUT Inválido");
                rut.value = ""
                return false;
            }
            return true;
        };

        function volver() {

            if (!confirm('¿Seguro que desea volver al menu principal?')) {
                return false;
            } else {
                return true;
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

        function soloNumeros(e) {
            key = e.keyCode || e.which;
            tecla = String.fromCharCode(key).toLowerCase();
            letras = "0123456789";
            especiales = "8-37-39-46";
            tecla_especial = false
            for (var i in especiales) {
                if (key == especiales[i]) {
                    tecla_especial = true;
                    reak;
                }
            }
            if (letras.indexOf(tecla) == -1 && !tecla_especial) {
                return false;
            }
            else {
                return true;
            }
        };

        function cerrarAlert() {
            $('#<%=txtHiddenAccion.ClientID %>').val("");
        };

        function validarSoloLetras(texto) {
            var valor = texto.value.trim();

            // TODO: SE AGREGAR FUNCION QUE QUITA ESPACIOS EN BLANCO DEL TEXTO          

            // Obtener su Producto con el Múltiplo Correspondiente
            if (!onBlurSoloLetras(valor)) {
                msgAlert("Texto Inválido, solo se aceptan letras");
                texto.value = ""
                return false;
            }
            texto.value = valor;
            return true;
        }

        function validarSoloNumeros(texto) {
            var valor = texto.value.trim();

            // TODO: SE AGREGAR FUNCION QUE QUITA ESPACIOS EN BLANCO DEL TEXTO          

            // Obtener su Producto con el Múltiplo Correspondiente
            if (!onBlurSoloNumeros(valor)) {
                msgAlert("Valor Inválido, solo se aceptan números");
                texto.value = ""
                return false;
            }
            texto.value = valor;
            return true;
        }

    </script>
</asp:Content>

