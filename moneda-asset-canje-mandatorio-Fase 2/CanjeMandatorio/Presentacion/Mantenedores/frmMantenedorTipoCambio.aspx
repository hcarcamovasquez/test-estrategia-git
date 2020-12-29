<%@ Page Title="Tipo de cambio" Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false" CodeFile="frmMantenedorTipoCambio.aspx.vb" Inherits="TipoCambio_Maestro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">

     <h2 class="TdRedondeado titleMant">Maestro de <strong>Tipo de Cambio</strong></h2>
    
    <div class="card p-4 jumbotron">

        <div class="row">
            <!-- MONEDA-->
            <div class="col-md-4">
                <asp:Label runat="server" ID="lblMoneda">Moneda</asp:Label>
                <asp:DropDownList ID="ddlMoneda" CssClass="form-control js-select2-rut" runat="server" />   
            </div>

           <!-- FECHA DESDE -->
            <div class="col-md-4">
                <asp:Label runat="server" ID="Label2">Fecha Creación desde</asp:Label>
                <div class="input-group"> 
                    <asp:TextBox ID="txtFeCreacionDesde" runat="server" CssClass="form-control datepicker" ReadOnly="True"></asp:TextBox>
                    <asp:LinkButton ID="LinkButton1" Class="btn btn-moneda" runat="server" 
                        OnClientClick="return clickCalendar('txtFeCreacionDesde')"><i class="far fa-calendar-alt"></i></asp:LinkButton>
                    <asp:LinkButton ID="BtnLimpiarFechaDesde" Text="" class="btn btn-secondary ml-1" runat="server" 
                        OnClientClick="return limpiarCalendar('txtFeCreacionDesde')"><i class="far fa-trash-alt"></i></asp:LinkButton>
                    
                    <%--<asp:Calendar ID="Calendar1" runat="server" Visible="False" BackColor="White" BorderColor="White" BorderWidth="1px" Font-Names="Verdana" Font-Size="9pt" ForeColor="Black" Height="190px" NextPrevFormat="FullMonth" Width="350px" class="calendarios">
                        <DayHeaderStyle Font-Bold="True" Font-Size="8pt" />
                        <NextPrevStyle Font-Bold="True" Font-Size="8pt" ForeColor="#333333" VerticalAlign="Bottom" />
                        <OtherMonthDayStyle ForeColor="#999999" />
                        <SelectedDayStyle BackColor="#333399" ForeColor="White" />
                        <TitleStyle BackColor="White" BorderColor="Black" BorderWidth="1px" Font-Bold="True" Font-Size="12pt" ForeColor="#333399" CssClass="cabecera texto-cabecera" />
                        <TodayDayStyle BackColor="#CCCCCC" />
                    </asp:Calendar>--%>
                </div>
            </div>
        
            <!-- FECHA HASTA -->
            <div class="col-md-4">
                <asp:Label runat="server" ID="lblFechaHasta">Fecha Creación hasta</asp:Label>
                <div class="input-group"> 
                    <asp:TextBox ID="txtFeCreacionHasta" runat="server" CssClass="form-control" ReadOnly="True"></asp:TextBox>
                    <asp:LinkButton ID="LinkButton2" Class="btn btn-moneda" runat="server" 
                        OnClientClick="return clickCalendar('txtFeCreacionHasta')"><i class="far fa-calendar-alt"></i></asp:LinkButton>
                    <asp:LinkButton ID="LinkButton4" Text="" class="btn btn-secondary ml-1" runat="server" 
                        OnClientClick="return limpiarCalendar('txtFeCreacionHasta')"><i class="far fa-trash-alt"></i></asp:LinkButton>
                   
                    <%--<asp:Calendar ID="Calendar2" runat="server" Visible="False" BackColor="White" BorderColor="White" BorderWidth="1px" Font-Names="Verdana" Font-Size="9pt" ForeColor="Black" Height="190px" NextPrevFormat="FullMonth" Width="350px" class="calendarios">
                        <DayHeaderStyle Font-Bold="True" Font-Size="8pt" />
                        <NextPrevStyle Font-Bold="True" Font-Size="8pt" ForeColor="#333333" VerticalAlign="Bottom" />
                        <OtherMonthDayStyle ForeColor="#999999" />
                        <SelectedDayStyle BackColor="#333399" ForeColor="White" />
                        <TitleStyle BackColor="White" BorderColor="Black" BorderWidth="1px" Font-Bold="True" Font-Size="12pt" ForeColor="#333399" CssClass="cabecera texto-cabecera" />
                        <TodayDayStyle BackColor="#CCCCCC" />
                    </asp:Calendar>--%>
                </div>
            </div>
</div>

 <!-- BOTONES BUSCAR LIMPIAR Y CREAR --> 
         <div class="row text-center mt-5 p-3 border-bottom">
          <div class="col-md-12">    
            <!-- BOTÓN BUSCAR -->
                <asp:Button ID="BtnBuscar" Text="Buscar" class="btn btn-moneda" runat="server" OnClick="BtnBuscar_Click"/>
                <asp:Button ID="btnLimpiarFrm" Text="Borrar" class="btn btn-secondary" runat="server" OnClick="btnLimpiarFrm_Click"/>
          
            <!-- BOTÓN CREAR -->
                <asp:Button ID="btnCrear" Text="Crear" class="btn btn-info" runat="server"/>
            
           </div>
        </div>
            
 

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
                            <asp:RadioButton ID="RowSelector" runat="server" onclick="checkRadioBtn(this);" GroupName="a" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Fecha" HeaderText="Fecha"  DataFormatString="{0:dd-MM-yyyy}" />
                    <asp:BoundField DataField="Codigo" HeaderText="Moneda" />
                    <asp:BoundField DataField="Valor" HeaderText="Valor" DataFormatString="{0:N12}" ItemStyle-HorizontalAlign="Right"/>
                    <asp:BoundField DataField="FechaIngreso" HeaderText="Fecha Ingreso" DataFormatString="{0:dd/MM/yyyy}"/> 
                    <asp:BoundField DataField="UsuarioIngreso" HeaderText="Usuario Ingreso" />
                    <asp:BoundField DataField="FechaModificacion" HeaderText="Fecha Modificación" DataFormatString="{0:dd/MM/yyyy}"/> 
                    <asp:BoundField DataField="UsuarioModificacion" HeaderText="Usuario Modificador" />
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
        <div>
        </div>
    </div>

    <asp:HiddenField ID="txtHiddenAccion" runat="server" />

    <!-- Bootstrap Modal Dialog Crear/Modificar -->
    <div class="modal fade" id="myModal" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" data-keyboard="false" data-backdrop="static">
        <div class="modal-dialog" style="max-width: 60%;">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title">
                        <asp:Label ID="lblModalTitulo" runat="server" Text="Formulario - Tipo de cambio" Font-Bold="true" Font-Size="X-Large">
                        </asp:Label>
                    </h4>
                    <Button ID="btnXCerrar" type="button" class="close" data-dismiss="modal" aria-label="Close" runat="server"><span aria-hidden="true">&times;</span></Button>
                </div>
                <div class="modal-body">
                    <div class="jumbotron">
                        <div class="p-4">
                            <h4 class="modal-title">                            
                            <asp:Label ID="lblModalFondoTitle" runat="server" Text="Modificar o eliminar" Font-Bold="true" Font-Size="X-Large">
                            </asp:Label>
                        </h4>
                                    <hr />
                                    <br />
                            <!-- FORMULARIO -->
                             <div class="row mt-4">
                                 <div class="col-md-6">
                                    <label class="form-control-label">Fecha</label>
                                    <asp:scriptmanager id="ScriptManager1" runat="server"></asp:scriptmanager>

                                   <%-- <asp:updatepanel runat="server" id="UpdatePanel1" updatemode="Conditional">
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="LinkButton3" EventName="click" />
                                       <</Triggers>
                                        <ContentTemplate>--%>
                                            <div class="input-group"> 
                                            <asp:TextBox ID="txtFechaTC" runat="server" CssClass="form-control datepicker" ReadOnly="True"></asp:TextBox>
                                            <asp:LinkButton ID="LinkButton3" Class="btn btn-moneda" OnClientClick="return clickCalendar('txtFechaTC')" runat="server"><i class="far fa-calendar-alt"></i></asp:LinkButton>
                                             <asp:LinkButton ID="LinkButton5" Text="" class="btn btn-secondary ml-1" runat="server"  
                                                 OnClientClick="return limpiarCalendar('txtFechaTC')"><i class="far fa-trash-alt"></i></asp:LinkButton>
                                            
                                                <%--<asp:Calendar ID="Calendar3" runat="server" Visible="False" BackColor="White" BorderColor="White" BorderWidth="1px" Font-Names="Verdana" Font-Size="9pt" ForeColor="Black" Height="190px" NextPrevFormat="FullMonth" Width="350px" class="calendarios">
                                                <DayHeaderStyle Font-Bold="True" Font-Size="8pt" />
                                                <NextPrevStyle Font-Bold="True" Font-Size="8pt" ForeColor="#333333" VerticalAlign="Bottom" />
                                                <OtherMonthDayStyle ForeColor="#999999" />
                                                <SelectedDayStyle BackColor="#333399" ForeColor="White" />
                                                <TitleStyle BackColor="White" BorderColor="Black" BorderWidth="1px" Font-Bold="True" Font-Size="12pt" ForeColor="#333399" CssClass="cabecera texto-cabecera" />
                                                <TodayDayStyle BackColor="#CCCCCC" />
                                            </asp:Calendar>--%>
                                            <span id="reqtxtFecha" class="reqError"></span>
                                                </div>
                                        <%--</ContentTemplate>
                                    </asp:updatepanel>--%>
                                  </div>
                                 <div class="col-md-6">
                                    <label class="form-control-label">Fecha de Ingreso</label>
                                    <asp:TextBox ID="txtFechaIngreso" runat="server" CssClass="form-control form-control-sm" ReadOnly="True"></asp:TextBox>
                                 </div>
                               </div>
                                 <!-- FECHA DE INGRESO -->
                            <div class="row mt-2">                              
                                 <div class="col-md-6">
                                    <label class="form-control-label">Moneda</label>
                                    <asp:DropDownList ID="txtMoneda" CssClass="form-control js-select2-rut" runat="server" /> 
                                </div>
                                 <div class="col-md-6">
                                    <label class="form-control-label">Usuario Ingreso</label>
                                    <asp:TextBox ID="txtUsuarioIngreso" runat="server" CssClass="form-control form-control-sm" ReadOnly="True"></asp:TextBox>
                                </div>
                            </div>
                                    
                                  <!-- USUARIO INGRESO -->
                            <div class="row mt-2">
                                
                                 <div class="col-md-6">
                                    <label class="form-control-label">Valor</label>
                                    <asp:TextBox ID="txtValor" runat="server" onpaste="return false" oncut="return false" oncopy="return false" CssClass="form-control form-control-sm dbs-entero-decimal"></asp:TextBox>

                                </div>   
                                 <div class="col-md-6">
                                    <label class="form-control-label">Fecha de Modificación</label>
                                    <asp:TextBox ID="txtFechaModificacion" runat="server" CssClass="form-control form-control-sm" ReadOnly="True"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row mt-2">
                                <div class="col-md-6">
                                </div> 
                                <div class="col-md-6">
                                    <label class="form-control-label">Usuario de Modificación</label>
                                    <asp:TextBox ID="txtUsuarioModificacion" runat="server" CssClass="form-control form-control-sm" ReadOnly="True"></asp:TextBox>
                                </div>
                            </div>
                               
                                 
                                                        <hr />
                            <asp:HiddenField ID="txtHidenEstado" runat="server" />
                            <!-- GRUPO DE BOTONES 2 -->
                            <div class="form-group mt-5 text-right">
                                <div class="col-md-offset-1">
                                    <asp:Button ID="btnModalGuardar" Text="Guardar" CssClass="btn btn-info" runat="server" OnClientClick="return validateBtn();" ></asp:Button>
                                    <asp:Button ID="btnModalModificar" Text="Guardar" CssClass="btn btn-info" runat="server" OnClientClick="return validateBtnModificar();"></asp:Button>
                                    <asp:Button ID="btnModalCancelar" runat="server" Text="Cancelar" CssClass="btn btn-secondary" OnClientClick="if (!confirm('¿Seguro que desea Cancelar?')) return false;"></asp:Button>
                                    <asp:Button ID="btnModalEliminar" runat="server" class="btn btn-danger" Text="Eliminar" OnClientClick="if (!confirm('¿Seguro que desea Eliminar los elementos seleccionados?')) return false;"></asp:Button>
                                </div>
                            </div>
                            <!-- FIN GRUPO DE BOTONES 2 -->
                        </div>
                    </div>
                </div>

                <!--<div class="modal-footer"></div>-->
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
                            <asp:Label ID="lblModalTitle" runat="server" Text="" Font-Bold="true" Font-Size="X-Large">
                            </asp:Label>
                        </h4>
                        <asp:Button ID="btnCerraModal" runat="server" Text="&times;" CssClass="btn rounded-circle close ml-5"/>
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
<asp:Content ID="Content3" ContentPlaceHolderID="FooterScript" Runat="Server">
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

        $(document).ready(function () {
            var txtHiddenAccion = $('#<%=txtHiddenAccion.ClientID %>').val();
           
            if ((txtHiddenAccion == "MODIFICAR") || (txtHiddenAccion == "CREAR") || (txtHiddenAccion == "ELIMINAR")) {
                $('#myModal').modal('show');
            } else if (txtHiddenAccion == "MOSTRAR_DIALOGO") {
                $('#myModalmg').modal();
                cerrarAlert();

            } else {
                checkRadioBtn("");
            }

             $("[id*=txtFeCreacionDesde]").datepicker();
            $("[id*=txtFeCreacionHasta]").datepicker();
            $("[id*=txtFechaTC]").datepicker({
                container: '#myModal modal-body'
                ,showOn: "none"
              });

            $("[id*=txtFeCreacionDesde]").change(function(){
                  changeFechas( $("[id*=txtFeCreacionDesde]"), $("[id*=txtFeCreacionHasta]"), 1)
            });
            $("[id*=txtFeCreacionHasta]").change(function(){
                  changeFechas( $("[id*=txtFeCreacionDesde]"), $("[id*=txtFeCreacionHasta]"), 2)
            });

            confNumeros()


            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(bindDataTable);
        });
        
         function bindDataTable() {
            $(".js-select2-rut").select2({
                templateResult: formatState,
                placeholder: 'Selecciona una opción'
            });

             confNumeros();
        };

        function confNumeros() {
            $('.dbs-entero-decimal').mask2(getMask(16,12));            
        }

        function cerrarAlert() {
            $('#<%=txtHiddenAccion.ClientID %>').val("");
        }

        function msgAlert(mensaje) {
            $('#pMensajeAlert').text(mensaje);
            $('#h5dialogTitle').html("Error");
            $('#modalAlert').modal();
        }
        function validateBtn() {
            
            if (!confirm('¿Seguro que desea Guardar?'))
            {
                return false;
            }
            else
            {
                return CheckTxtEmpty();
            }
        }

        function validateBtnModificar() {
            if (!confirm('¿Seguro que desea Guardar?')) {
                return false;
            } else {
                return CheckTxtEmptyModificar();
            }
        }

        function CheckTxtEmpty() {
            if ($('#<%=txtFechaTC.ClientID %>').val() == "")
            {
                alert('La fecha es un campo obligatorio.')
                return false;
            }
            else
            {
                if ($('#<%=txtMoneda.ClientID %>').val() == "") {
                    alert('Debe seleccionar una opción válida en Moneda')
                    return false;
                } 

                else
                {
                    if ($('#<%=txtValor.ClientID %>').val() == "" || $('#<%=txtValor.ClientID %>').val() == "0") {
                        alert('El valor ingresado no es permitido')
                        document.getElementById('<%=txtValor.ClientID%>').value = ""
                        return false;
                    }
                    
                    else {
                        return validarValor();
                    }
                }
            }
            
        }

        function validarValor() {
            var valor1 = document.getElementById('<%=txtValor.ClientID%>').value.replace(/\./g, '');
            var valor = parseFloat(valor1);

            if (valor.match(/^\d{1,16}(\,\d{1,12})?$/)) {
                return true;
            } else {
                alert('La cadena no tiene formato correcto (16 enteros , 12 decimales)');
                return false;
            }
        }

        function CheckTxtEmptyModificar() {
            if ($('#<%=txtValor.ClientID%>').val == "") {
                alert('El valor es un campo obligatorio')
                return false;
            } else {
                return validarValor();
            }
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
    </script>

   
</asp:Content>