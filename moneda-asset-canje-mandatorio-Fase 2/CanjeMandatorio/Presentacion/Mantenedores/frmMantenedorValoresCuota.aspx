<%@ Page Title="Valores Cuotas" Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false" CodeFile="frmMantenedorValoresCuota.aspx.vb" Inherits="Presentacion_Mantenedores_frmMantenedorValoresCuota" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    <h2 class="TdRedondeado titleMant">Maestro de <strong>Valores Cuotas</strong></h2>

    <div class="card p-4 jumbotron">

        <div class="row">
            <!-- RUT del Fondo-->
            <div class="col-md-4">
                <asp:Label runat="server" ID="lblRut">Nemotécnico</asp:Label>
                <asp:DropDownList ID="ddlNemotecnico" CssClass="form-control js-select2-rut" runat="server" />
            </div>

            <!-- FECHA DESDE -->
            <div class="col-md-4">
                <asp:Label runat="server" ID="Label2">Fecha Creación Desde</asp:Label>
                <div class="input-group">
                    <asp:TextBox ID="txtFechaDesde" runat="server" CssClass="form-control datepicker" ReadOnly="True"></asp:TextBox>
                    <asp:LinkButton ID="lnkBtnFechaDesde" Class="btn btn-moneda" runat="server" 
                        OnClientClick="return clickCalendar('txtFechaDesde')"><i class="far fa-calendar-alt"></i></asp:LinkButton>
                    <asp:LinkButton ID="BtnLimpiarFechaDesde" Text="" class="btn btn-secondary ml-1" runat="server" 
                        OnClientClick="return limpiarCalendar('txtFechaDesde')"><i class="far fa-trash-alt"></i></asp:LinkButton>


                </div>
            </div>


            <!-- FECHA HASTA -->
            <div class="col-md-4">
                <asp:Label runat="server" ID="lblFechaHasta">Fecha Creación Hasta</asp:Label>
                <div class="input-group">
                    <asp:TextBox ID="txtFechaHasta" runat="server" CssClass="form-control datepicker" ReadOnly="True"></asp:TextBox>
                    <asp:LinkButton ID="lnkBtnFechaHasta" Class="btn btn-moneda" runat="server" 
                        OnClientClick="return clickCalendar('txtFechaHasta')"><i class="far fa-calendar-alt"></i></asp:LinkButton>
                    <asp:LinkButton ID="BtnLimpiarFechaDesde0" Text="" class="btn btn-secondary ml-1" runat="server" 
                        OnClientClick="return limpiarCalendar('txtFechaHasta')"><i class="far fa-trash-alt"></i></asp:LinkButton>
                </div>
            </div>
        </div>
        <!-- BOTONES BUSCAR LIMPIAR Y CREAR -->

        <div class="row text-center mt-5 p-3 border-bottom">
            <div class="col-md-12">
                <!-- BOTÓN BUSCAR -->
                <asp:Button ID="BtnBuscar" Text="Buscar" class="btn btn-moneda" runat="server" />
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
                            <asp:RadioButton ID="RadioButton1" runat="server" onclick="checkRadioBtn(this);" GroupName="a" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="FnRut" HeaderText="Rut" />
                    <asp:BoundField DataField="FsNemotecnico" HeaderText="Nemotécnico" />
                    <asp:BoundField DataField="Fecha" HeaderText="Fecha" DataFormatString="{0:dd-MM-yyyy}" />
                    <asp:BoundField DataField="Valor" HeaderText="Valor" DataFormatString="{0:N6}" ItemStyle-HorizontalAlign="Right"/>
                    <asp:BoundField DataField="FechaIngreso" HeaderText="Fecha Ingreso" DataFormatString="{0:dd-MM-yyyy}" />
                    <asp:BoundField DataField="UsuarioIngreso" HeaderText="Usuario Ingreso" />
                    <asp:BoundField DataField="FechaModificacion" HeaderText="Fecha Modificación" DataFormatString="{0:dd-MM-yyyy}" />
                    <asp:BoundField DataField="UsuarioModificacion" HeaderText="Usuario Modificador" />
                    <asp:BoundField DataField="Estado" HeaderText="Estado" Visible="false" />
                </Columns>
            </asp:GridView>
        </div>

        <div class="row mt-4">
            <div class="col-md-12 text-center">
                <asp:Button ID="BtnModificar" runat="server" class="btn btn-info" Text="Modificar" OnClick="BtnModificar_Click" Enabled="false"></asp:Button>
                <asp:Button ID="BtnEliminar" runat="server" class="btn btn-danger" Text="Eliminar" OnClientClick="if (!confirm('¿Seguro que desea Eliminar los elementos seleccionados?')) return false;" Enabled="false"></asp:Button>
                <asp:Button ID="BtnExportar" class="btn btn-success" Text="Exportar" runat="server" Enabled="false" />
            </div>

        </div>
    </div>

    <asp:HiddenField ID="txtHiddenAccion" runat="server" />

    <!-- Bootstrap Modal Dialog Crear/Modificar -->
    <div class="modal fade" id="myModal" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" data-keyboard="false" data-backdrop="static">
        <div class="modal-dialog" style="max-width: 50%;">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title">
                        <asp:Label ID="lblModalTitulo" runat="server" Text="Formulario - Maestro de Valores Cuotas" Font-Bold="true" Font-Size="X-Large"> </asp:Label>
                    </h4>
                    <button id="btnXCerrar" type="button" class="close" data-dismiss="modal" aria-label="Close" runat="server"><span aria-hidden="true">&times;</span></button>
                </div>
                <div class="modal-body">
                    <div class="jumbotron">
                        <div class="p-4">
                            <h4 class="modal-title">
                                <asp:Label ID="lblModalFondoTitle" runat="server" Text="Modificar o eliminar" Font-Bold="true" Font-Size="X-Large"> </asp:Label>
                            </h4>

                            <hr />
                            <br />
                            <!-- FORMULARIO -->
                            <div class="row">
                                <div class="col-md-5">
                                    <asp:Label runat="server" ID="lblModalFecha" Font-Size="Large">Fecha: </asp:Label>
                                </div>
                                <div class="col-md-7">


                                            <div class="input-group">
                                                <asp:TextBox ID="txtModalFecha1" runat="server" CssClass="form-control datepicker" ReadOnly="True"></asp:TextBox>
                                                <asp:LinkButton ID="lnkBtnModalFecha" Class="btn btn-moneda" runat="server" 
                                                    OnClientClick="return clickCalendar('txtModalFecha1')"><i class="far fa-calendar-alt"></i></asp:LinkButton>
                                                <asp:LinkButton ID="BtnLimpiarFechaHasta" Text="" class="btn btn-secondary ml-1" runat="server"  
                                                    OnClientClick="return limpiarCalendar('txtModalFecha1')"><i class="far fa-trash-alt"></i></asp:LinkButton>
                                                
                                                <span id="reqtxtModalFecha" class="reqError"></span>
                                            </div>


                                </div>
                            </div>
                            <div class="row mt-3">
                                <div class="col-md-5">
                                    <asp:Label runat="server" ID="lblModalFondo" Font-Size="Large">Fondo: </asp:Label>
                                </div>
                                <div class="col-md-7">
                                    <asp:UpdatePanel runat="server" ID="UpdatePanel2" UpdateMode="Conditional">
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="ddlModalNemotecnico" EventName="SelectedIndexChanged" />
                                        </Triggers>
                                        <ContentTemplate>
                                            <asp:DropDownList ID="ddlModalFondo" CssClass="form-control js-select2-rut" runat="server" OnSelectedIndexChanged="CargarNemotecnicoPorRutFondoModal" AutoPostBack="true" />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                            <div class="row mt-3">
                                <div class="col-md-5">
                                    <asp:Label runat="server" ID="lblModalNemotecnico" Font-Size="Large">Nemotécnico: </asp:Label>
                                </div>
                                <div class="col-md-7">
                                    <asp:UpdatePanel runat="server" ID="UpdatePanel" UpdateMode="Conditional">
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="ddlModalFondo" EventName="SelectedIndexChanged" />
                                        </Triggers>
                                        <ContentTemplate>
                                            <asp:DropDownList ID="ddlModalNemotecnico" CssClass="form-control js-select2-rut" runat="server" OnSelectedIndexChanged="CargarRutFondoPorNemotecnicoModal" AutoPostBack="true" />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                            <div class="row mt-3">
                                <div class="col-md-5">
                                    <asp:Label runat="server" ID="lblModalValorCuota" Font-Size="Large">Valor Cuota: </asp:Label>
                                </div>
                                <div class="col-md-7">

                                            <asp:TextBox ID="txtModalValorCuota" runat="server"  CssClass="form-control form-control-sm valor dbs-entero-decimal" onkeypress="return soloNumerosyComa(event)" onpaste="return false" oncut="return false" oncopy="return false" ></asp:TextBox>

                                    
                                </div>
                            </div>
                            <div class="row mt-3">
                                <div class="col-md-5">
                                    <asp:Label runat="server" ID="lblModalFechaIngreso" Font-Size="Large">Fecha Ingreso: </asp:Label>
                                </div>
                                <div class="col-md-7">
                                    <asp:TextBox ID="txtModalFechaIngreso" runat="server" CssClass="form-control form-control-sm" ReadOnly="true"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row mt-3">
                                <div class="col-md-5">
                                    <asp:Label runat="server" ID="lblModalUsuarioIngreso" Font-Size="Large">Usuario Ingreso: </asp:Label>
                                </div>
                                <div class="col-md-7">
                                    <asp:TextBox ID="txtModalUsuarioIngreso" runat="server" CssClass="form-control form-control-sm" ReadOnly="true"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row mt-3">
                                <div class="col-md-5">
                                    <asp:Label runat="server" ID="lblModalFechaModificacion" Font-Size="Large">Fecha Modificación: </asp:Label>
                                </div>
                                <div class="col-md-7">
                                    <asp:TextBox ID="txtModalFechaModificacion" runat="server" CssClass="form-control form-control-sm" ReadOnly="True"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row mt-3">
                                <div class="col-md-5">
                                    <asp:Label runat="server" ID="lblModalUsuarioModificacion" Font-Size="Large">Usuario Modificación: </asp:Label>
                                </div>
                                <div class="col-md-7">
                                    <asp:TextBox ID="txtModalUsuarioModificacion" runat="server" CssClass="form-control form-control-sm" ReadOnly="True"></asp:TextBox>
                                </div>
                            </div>

                            <hr />
                            <asp:HiddenField ID="txtHidenEstado" runat="server" />
                            <!-- GRUPO DE BOTONES 2 -->
                            <div class="form-group mt-5 text-right">
                                <div class="col-md-offset-1">
                                    <asp:Button ID="btnModalGuardarModificar" Text="Guardar" CssClass="btn btn-info" runat="server" OnClientClick="return validateBtn();"></asp:Button>
                                    <asp:Button ID="btnModalGuardarCrear" Text="Guardar" CssClass="btn btn-info" runat="server" OnClientClick="return validateBtnCrear();"></asp:Button>
                                    <asp:Button ID="btnModalCancelar" Text="Cancelar" CssClass="btn btn-secondary" runat="server" OnClientClick="if (!confirm('¿Seguro que desea Cancelar?')) return false;"></asp:Button>
                                    <asp:Button ID="btnModalEliminarGrupo" Text="Eliminar" runat="server" class="btn btn-danger" Width="15%" OnClientClick="if (!confirm('¿Seguro que desea Eliminar los elementos seleccionados?')) return false;"></asp:Button>
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
    <div class="modal fade" id="myModalmg" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" data-keyboard="false" data-backdrop="static">
        <div class="modal-dialog">
            <div class="modal-content text-center">
                <div class="modal-header mx-auto">
                    <h4 class="modal-title">
                        <asp:Image ID="img_modal" ImageUrl="~/Img/info1.png" runat="server" Width="130" Height="50" />
                        <asp:Label ID="lblModalTitle" runat="server" Text="" Font-Bold="true" Font-Size="X-Large"> </asp:Label>
                    </h4>
                    <button type="button" class="btn rounded-circle close ml-5" data-dismiss="modal">x</button>
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
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Cerrar</button>
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
            z-index: 1600 !important; /* has to be larger than 1050 */
        }
    </style>

    <script>
        function calendarInitial() {
            $("[id*=txtFechaDesde]").datepicker();
            $("[id*=txtFechaHasta]").datepicker();
            $("[id*=txtModalFecha1]").datepicker({
                container: '#myModal modal-body'
                ,showOn: "none"
              });

            $("[id*=txtFechaDesde]").change(function(){
                  changeFechas( $("[id*=txtFechaDesde]"), $("[id*=txtFechaHasta]"), 1)
            });
            $("[id*=txtFechaHasta]").change(function(){
                  changeFechas( $("[id*=txtFechaDesde]"), $("[id*=txtFechaHasta]"), 2)
            });
            $("[id*=txtModalFecha1]").change(function () {
                console.log('txtModalFecha1' )
                return false; 
            });
        }


        $(document).ready(function () {
            var txtHiddenAccion = $('#<%=txtHiddenAccion.ClientID %>').val();
            if ((txtHiddenAccion == "MODIFICAR") || (txtHiddenAccion == "CREAR") || (txtHiddenAccion == "ELIMINAR") || (txtHiddenAccion == "MANTENER_MODAL")) {
                $('#myModal').modal('show');
            } else if (txtHiddenAccion == "MOSTRAR_DIALOGO") {
                $('#myModalmg').modal();
            } else {
                checkRadioBtn("");
            }
            
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(bindDataTable);
            calendarInitial();

            confNumeros();
        });

                function confNumeros() {

                    $('.dbs-entero-decimal').mask2(getMask(20,6));            
                }

        function bindDataTable() {
            $(".js-select2-rut").select2({
                templateResult: formatState,
                placeholder: 'Selecciona una opción'
            });
            calendarInitial();
        };
        function cerrarAlert() {
            $('#<%=txtHiddenAccion.ClientID %>').val("");
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
                return CheckTxtEmpty();
            }
        }

        function validateBtnCrear() {
            if (!confirm('¿Seguro que desea Guardar?')) {
                return false;
            } else {
                return CheckTxtEmptyCrear();
            }
        }

        <%--function seteaBotonGuardarCrear() {
            $("#<%=btnModalGuardarCrear.ClientID %>").unbind("click");
            $("#<%=btnModalGuardarCrear.ClientID %>").click(function () {
                if (!confirm('¿Confirma que desea Guardar?')) {
                    return false; Seguro
                }
                else {
                    return true;
                }
            });
        }--%>

        <%--function seteaBotonGuardarModificar() {
            $("#<%=btnModalGuardarModificar.ClientID %>").unbind("click");
            $("#<%=btnModalGuardarModificar.ClientID %>").click(function () {
                if (!confirm('¿Confirma que desea Guardar?')) {
                    return false; Seguro
                }
                else {
                    return true;
                }
            });
        }--%>

        function CheckTxtEmpty() {
            if ($('#<%=txtModalValorCuota.ClientID%>').val() == "") {
                alert('El Valor de la Cuota es un campo obligatorio')
                return false;
            } else {
                return validarValor();
            }
        }



        function CheckTxtEmptyCrear() {
            if ($('#<%=txtModalFecha1.ClientID%>').val() == "") {
                alert('La fecha es un campo obligatorio')
                return false;
            } else {
                if ($('#<%=ddlModalFondo.ClientID%>').val() == "") {
                    alert('Debe seleccionar un opción válida en Fondo')
                    return false;
                } else {
                    if ($('#<%=txtModalValorCuota.ClientID%>').val() == "" || $('#<%=txtModalValorCuota.ClientID%>').val() == "0") {
                        alert('El campo Valor Cuota es obligatorio');
                        document.getElementById('<%=txtModalValorCuota.ClientID%>').value = "";
                        return false;
                    } else {
                        return validarValor()
                    }
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

        function isPerfilConsulta() {
            var HiddenPerfil = $("#<%=HiddenPerfil.ClientID %>").val();
            var HiddenConstante = $("#<%=HiddenConstante.ClientID %>").val();

            if (HiddenPerfil == HiddenConstante || HiddenPerfil == "") {
                return true;
            }
            return false
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

        function validarValor() {
            var valor1 = document.getElementById('<%=txtModalValorCuota.ClientID%>').value.replace("/\./g", "");

            var valor = parseFloat(valor1);
            //if (valor.match(/^[1-9]\d{0,2}(\.\d{3})*(,\d+)?$/)) {
            if (valor.match(/^\d{1,20}(\,\d{1,6})?$/)) {
                return true;
            } else {
                alert('La cadena no tiene formato correcto (20 enteros , 6 decimales)');
                return false;
            }
        }

      
        function volver() {
            if (!confirm('¿Seguro que desea volver al menu principal?')) {
                return false;
            } else {
                return true;
            }
        }
    </script>
</asp:Content>
