<%@ Page Title="Aportantes" Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false" CodeFile="frmMantenedorAportantes.aspx.vb" Inherits="Mantenedores_Aportantes_Maestro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">


    <h2 class="TdRedondeado titleMant">Maestro de <strong>Aportantes</strong></h2>

    <div class="card p-4 jumbotron">

        <div class="row">
            <!-- Aportante-->
            <div class="col-md-4">
                <asp:Label ID="lblRut" runat="Server">Aportante</asp:Label>
                <asp:DropDownList ID="ddlRutAportante" CssClass="form-control js-select2-rut" runat="server"></asp:DropDownList>
            </div>

            <!-- FECHA DESDE -->
            <div class="col-md-4">
                <asp:Label runat="server" ID="Label2">Fecha Creación desde</asp:Label>
                <div class="input-group">
                    <asp:TextBox ID="txtFeCreacionDesde" runat="server" CssClass="form-control datepicker" ReadOnly="True"></asp:TextBox>

                    <asp:LinkButton ID="LinkButton1" class="btn btn-moneda" runat="server" OnClientClick="return clickCalendar('txtFeCreacionDesde')"><i class="far fa-calendar-alt"></i></asp:LinkButton>
                    <asp:LinkButton ID="BtnLimpiarFechaDesde" Text="Limpiar" class="btn btn-secondary ml-1" runat="server" OnClientClick="return limpiarCalendar('txtFeCreacionDesde')"><i class="far fa-trash-alt"></i></asp:LinkButton>
                    
                    <%--<asp:Calendar ID="Calendar1" runat="server" Visible="False" onblur="onblurCalendar(this)" BackColor="White" BorderColor="White" BorderWidth="1px" Font-Names="Verdana" Font-Size="9pt" ForeColor="Black" Height="190px" NextPrevFormat="FullMonth" Width="350px" class="calendarios">
                        <DayHeaderStyle Font-Bold="True" Font-Size="8pt" />
                        <NextPrevStyle Font-Bold="True" Font-Size="8pt" ForeColor="#333333" VerticalAlign="Bottom" />
                        <OtherMonthDayStyle ForeColor="#999999" />
                        <SelectedDayStyle BackColor="#333399" ForeColor="White" />
                        <TitleStyle BackColor="White" BorderColor="Black" BorderWidth="1px" Font-Bold="True" Font-Size="12pt" ForeColor="#333399" CssClass="cabecera texto-cacebera" />
                        <TodayDayStyle BackColor="#CCCCCC" />
                    </asp:Calendar>--%>

                </div>
            </div>

            <!-- FECHA HASTA -->
            <div class="col-md-4">
                <asp:Label runat="server" ID="lblFechaHasta">Fecha Creación hasta</asp:Label>
                <div class="input-group">
                    <asp:TextBox ID="txtFeCreacionHasta" runat="server" CssClass="form-control  datepicker" ReadOnly="True"></asp:TextBox>
                    <asp:LinkButton ID="LinkButton2" class="btn btn-moneda" runat="server" OnClientClick="return clickCalendar('txtFeCreacionHasta')"><i class="far fa-calendar-alt"></i></asp:LinkButton>
                    <asp:LinkButton ID="BtnLimpiarFechaHasta" Text="Limpiar Fecha" class="btn btn-secondary ml-1" runat="server" OnClientClick="return limpiarCalendar('txtFeCreacionHasta')"><i class="far fa-trash-alt"></i></asp:LinkButton>
                    <%--<asp:Calendar ID="Calendar2" runat="server" Visible="False" BackColor="White" BorderColor="White" BorderWidth="1px" Font-Names="Verdana" Font-Size="9pt" ForeColor="Black" Height="190px" NextPrevFormat="FullMonth" Width="350px" class="calendarios">
                        <DayHeaderStyle Font-Bold="True" Font-Size="8pt" />
                        <NextPrevStyle Font-Bold="True" Font-Size="8pt" ForeColor="#333333" VerticalAlign="Bottom" />
                        <OtherMonthDayStyle ForeColor="#999999" />
                        <SelectedDayStyle BackColor="#333399" ForeColor="White" />
                        <TitleStyle BackColor="White" BorderColor="Black" BorderWidth="1px" Font-Bold="True" Font-Size="12pt" ForeColor="#333399" CssClass="cabecera texto-cacebera" />
                        <TodayDayStyle BackColor="#CCCCCC" />
                    </asp:Calendar>--%>
                </div>
            </div>
            <!-- NOMBRE-->
            <div class="col-md-5">
                <asp:Label runat="server" ID="lblNombreFondo" Visible="False">Nombre del fondo</asp:Label>
                <asp:DropDownList ID="ddlNombreFondo" CssClass="form-control js-select2-rut" runat="server" Visible="False" />
            </div>
        </div>


        <!-- BOTONES BUSCAR Y CREAR -->
        <div class="row text-center mt-5 p-3 border-bottom">
            <div class="col-12">
                <!-- BOTÓN BUSCAR -->
                <asp:Button ID="BtnBuscar" Text="Buscar" class="btn btn-moneda" runat="server" />
                <asp:Button ID="btnLimpiarFrm" Text="Borrar" class="btn btn-secondary" runat="server" OnClick="btnLimpiarFrm_Click" />

                <!-- BOTÓN CREAR -->
                <asp:Button ID="btnCrear" Text="Crear" class="btn btn-info" runat="server" OnClick="btnCrear_Click" />
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
                            <asp:RadioButton ID="RowSelector" runat="server" onclick="checkRadioBtn(this);" GroupName="a" AutoPostBack="false" OnCheckedChanged="RowSelector_CheckedChanged" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="RUT" HeaderText="Rut" />
                    <asp:BoundField DataField="RazonSocial" HeaderText="Nombre" />
                    <asp:BoundField DataField="Multifondo" HeaderText="Multifondo" />
                    <asp:BoundField DataField="NacExt" HeaderText="Domiciliado en el Extranjero" />
                    <asp:BoundField DataField="Calificado" HeaderText="Inversionista Calificado" />
                    <asp:BoundField DataField="Intermediario" HeaderText="Tipo Aportante" />
                    <asp:BoundField DataField="RelacionMan" HeaderText="Relacionado MAM" />
                    <asp:BoundField DataField="Distribucion" HeaderText="Contrato Distribución" />
                    <asp:HyperLinkField ItemStyle-HorizontalAlign ="Center" Target="_blank" DataNavigateUrlFields="Documentacion" DataNavigateUrlFormatString="//{0}" Text="IR" HeaderText="Documentación">
                        <ControlStyle CssClass="AporteLink" />
                    </asp:HyperLinkField>
                    <asp:BoundField DataField="Estado" HeaderText="Estado" Visible="false" />
                    <asp:BoundField DataField="FechaIngreso" HeaderText="Fecha Ingreso" DataFormatString="{0:dd-MM-yyyy}" />
                    <asp:BoundField DataField="UsuarioIngreso" HeaderText="Usuario Ingreso" />
                    <asp:BoundField DataField="FechaModificacion" HeaderText="Fecha Modificación" DataFormatString="{0:dd-MM-yyyy}" />
                    <asp:BoundField DataField="UsuarioModificacion" HeaderText="Usuario Modificador" />
                </Columns>
                <RowStyle Font-Size="Small"></RowStyle>
            </asp:GridView>
        </div>

        <div class="row mt-4">
            <div class="col-md-12 text-center">
                <asp:Button ID="BtnModificar" runat="server" class="btn btn-info" Text="Modificar" OnClick="BtnModificar_Click" Enabled="false"></asp:Button>
                <asp:Button ID="BtnEliminar" runat="server" class="btn btn-danger" Text="Eliminar" OnClientClick="if (!confirm('¿Seguro que desea Eliminar los elementos seleccionados?')) return false;" Enabled="False"></asp:Button>
                <asp:Button ID="BtnExportar" class="btn btn-success" Text="Exportar" runat="server" Enabled="False" />
            </div>
            <div class="col-md-2">
                <asp:Button ID="btnVolver" runat="server" Width="125px" class="btn btn-secondary" Text="Volver"
                    OnClientClick="return volver();" Visible="False"></asp:Button>
            </div>
        </div>
        <div>
        </div>
    </div>

    <!-- Bootstrap Modal Dialog Crear/Modificar -->
    <div class="modal fade" id="myModal" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" data-keyboard="false" data-backdrop="static">
        <div class="modal-dialog" style="max-width: 70%;">
            <div class="modal-content text-center">
                <div class="modal-header">
                    <h4 class="modal-title">
                        <asp:Label ID="lblModalTitulo" runat="server" Text="Formulario - Nuevo Aportante" Font-Bold="true" Font-Size="X-Large">
                        </asp:Label>
                    </h4>
                    <button id="btnXCerrar" type="button" class="close" data-dismiss="modal" aria-label="Close" onclick="cerrarAlert(); return true;">
                        <span aria-hidden="true" onclick="cerrarAlert(); return true;">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div class="jumbotron">
                        <div>
                            <div class="card-body  pt-2 pb-2 text-left">
                                <div class="row mt-4">
                                    <!-- RUT-->
                                    <div class="col-md-6">
                                        <label class="form-control-label">Rut Aportante</label>
                                        <asp:TextBox runat="server" ID="txtRut" MaxLength="15" onblur="validateRut(this)" CssClass="form-control form-control-sm " onkeypress="return soloRut(event)" />
                                        <span id="reqtxtRut" class="reqError"></span>
                                        <br>
                                    </div>

                                    <!-- FECHA DE INGRESO -->
                                    <div class="col-md-6">
                                        <label class="form-control-label">Fecha de Ingreso</label>
                                        <asp:TextBox ID="txtFechaIngreso" runat="server" CssClass="form-control form-control-sm" ReadOnly="True"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="row mt-2">
                                    <!-- NOMBRE APORTANTE -->
                                    <div class="col-md-6">
                                        <label class="form-control-label">Nombre aportante</label>
                                        <!-- <asp:TextBox ID="txtNombre1" runat="server" MaxLength="200" CssClass="form-control form-control-sm" onkeypress="return soloLetras(event)" onblur="validarSoloLetras(this)"></asp:TextBox>-->
                                        <asp:TextBox ID="txtNombre" runat="server" MaxLength="200" CssClass="form-control form-control-sm"></asp:TextBox>
                                    </div>
                                    <!-- USUARIO INGRESO -->
                                    <div class="col-md-6">
                                        <label class="form-control-label">Usuario Ingreso</label>
                                        <asp:TextBox ID="txtUsuarioIngreso" runat="server" CssClass="form-control form-control-sm" ReadOnly="True"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="row mt-2">
                                    <!-- MULTIFONDO -->
                                    <div class="col-md-6">
                                        <label class="form-control-label">Multifondo</label>
                                        <asp:TextBox ID="txtMultifondo" runat="server" MaxLength="1" CssClass="form-control form-control-sm" onkeypress="return soloLetras(event)" onblur="validarSoloLetras(this)"></asp:TextBox>

                                    </div>
                                    <!-- FECHA DE MODIFICACION -->
                                    <div class="col-md-6">
                                        <label class="form-control-label">Fecha de Modificación</label>
                                        <asp:TextBox ID="txtFechaModificacion" runat="server" CssClass="form-control form-control-sm" ReadOnly="True"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="row mt-2">
                                    <!-- DOMICILIO EXTRANJERO -->
                                    <div class="col-md-6">
                                        <label class="form-control-label">Domicilio en el Extranjero</label>
                                        <asp:DropDownList ID="ddlNacExt" CssClass="form-control form-control-sm" runat="server">
                                            <asp:ListItem Value=""></asp:ListItem>
                                            <asp:ListItem Value="E"> Extranjero</asp:ListItem>
                                            <asp:ListItem Value="N"> Nacional</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <!-- USUARIO MODIFICACION -->
                                    <div class="col-md-6">
                                        <label class="form-control-label">Usuario de Modificación</label>
                                        <asp:TextBox ID="txtUsuarioModificacion" runat="server" CssClass="form-control form-control-sm" ReadOnly="True"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="row mt-2">
                                    <!-- INVERSIONISTA CALIFICADO -->
                                    <div class="col-md-6">
                                        <label class="form-control-label">Inversionista Calificado</label>
                                        <asp:DropDownList ID="ddlInversionista" CssClass="form-control form-control-sm" runat="server">
                                            <asp:ListItem Value=""></asp:ListItem>
                                            <asp:ListItem Value="C"> Calificado</asp:ListItem>
                                            <asp:ListItem Value="NC"> NO Calificado</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-6">
                                    </div>
                                </div>

                                <div class="row mt-3">
                                    <!-- TIPO APORTANTE -->
                                    <div class="col-md-6">
                                        <label class="form-control-label">Tipo Aportante</label>
                                        <asp:DropDownList ID="ddlTipoAportante" CssClass="form-control form-control-sm" runat="server">
                                            <asp:ListItem Value=""></asp:ListItem>
                                            <asp:ListItem Value="D"> Directo</asp:ListItem>
                                            <asp:ListItem Value="I"> Indirecto</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-6">
                                    </div>
                                </div>

                                <div class="row mt-3">
                                    <!-- RELACIONADO MAM -->
                                    <div class="col-md-6">
                                        <label class="form-control-label">Relacionado MAM</label>
                                        <asp:DropDownList ID="ddlRelacionado" CssClass="form-control form-control-sm" runat="server">
                                            <asp:ListItem Value=""></asp:ListItem>
                                            <asp:ListItem Value="S"> SI</asp:ListItem>
                                            <asp:ListItem Value="N"> NO</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-6">
                                    </div>
                                </div>

                                <div class="row mt-3">
                                    <!-- CONTRATO DISTRIBUCION -->
                                    <div class="col-md-6">
                                        <label class="form-control-label">Contrato Distribución</label>
                                        <asp:DropDownList ID="ddlContrato" CssClass="form-control form-control-sm" runat="server">
                                            <asp:ListItem Value=""></asp:ListItem>
                                            <asp:ListItem Value="S"> SI</asp:ListItem>
                                            <asp:ListItem Value="N"> NO</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-6">
                                    </div>
                                </div>

                                <div class="row mt-2">
                                    <!-- Documentacion -->
                                    <div class="col-md-6">
                                        <label class="form-control-label">Documentación</label>
                                        <asp:TextBox ID="txtDocumentacion" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                    </div>
                                    <div class="col-md-6">
                                    </div>
                                </div>
                                <asp:HiddenField ID="txtAccionHidden" runat="server" />
                                <hr />
                                <asp:HiddenField ID="txtHidenEstado" runat="server" />
                                <div class="form-group">
                                    <div class="col-12 text-center">
                                        <asp:Button ID="btnModalGuardar" Text="Crear" CssClass="btn btn-info" runat="server" OnClientClick="return validateBtn();"></asp:Button>
                                        <asp:Button ID="btnModalModificar" Text="Modificar" CssClass="btn btn-info" runat="server" OnClientClick="return validateBtn();"></asp:Button>
                                        <asp:Button ID="btnModalCancelar" runat="server" Text="Cancelar" CssClass="btn btn-secondary" OnClientClick="if (!confirm('¿Seguro que desea Cancelar?')) return false;"></asp:Button>
                                        <asp:Button ID="btnModalEliminar" runat="server" class="btn btn-danger" Text="Eliminar" OnClientClick="if (!confirm('¿Seguro que desea Eliminar los elementos seleccionados?')) return false;"></asp:Button>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <!-- <div class="modal-footer">-->
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

<asp:Content ContentPlaceHolderID="FooterScript" runat="Server">
   
    <script src="<%=ResolveUrl("~/Scripts/jquery.dataTables.min.js")%>"></script>
    <script src="<%=ResolveUrl("~/Scripts/dataTables.bootstrap4.min.js")%>"></script>
    <script src="<%=ResolveUrl("~/Scripts/scripts.js")%>"></script>
    <script src="<%=ResolveUrl("~/Scripts/select2.min.js")%>"></script>

    <script src="<%=ResolveUrl("~/Scripts/date-dd-mmm-yyyy.js")%>"></script>

    <script>
        $(document).ready(function () {
            //checkGrilla();
            var txtAccionHidden = $("#<%=txtAccionHidden.ClientID %>").val();
            if ((txtAccionHidden == "MODIFICAR") || (txtAccionHidden == "ELIMINAR") || (txtAccionHidden == "CREAR")) {
                $('#myModal').modal('show');
            }

            
            $("[id*=txtFeCreacionDesde]").datepicker();
            $("[id*=txtFeCreacionHasta]").datepicker();

            $("[id*=txtFeCreacionDesde]").change(function(){
                  changeFechas( $("[id*=txtFeCreacionDesde]"), $("[id*=txtFeCreacionHasta]"), 1)
            });
            $("[id*=txtFeCreacionHasta]").change(function(){
                  changeFechas( $("[id*=txtFeCreacionDesde]"), $("[id*=txtFeCreacionHasta]"), 2)
            });


        });

        

         function validateBtn() {
            if (!confirm('¿Seguro que desea Guardar?')) {
                return false;
            } else {
                var rutTxt = document.getElementById('<%=txtRut.ClientID%>');
                if (validateRut(rutTxt)) {
                    return CheckTxtEmpty();
                }
                else {
                    return false
                }
            }
        };

        function msgAlert(mensaje) {
            $('#pMensajeAlert').text(mensaje);
            $('#h5dialogTitle').html("Error");
            $('#modalAlert').modal();
        };

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

        function checkRadioBtn(id) {
            var gv = document.getElementById('<%=GrvTabla.ClientID %>');
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

        function isPerfilConsulta() {
            var HiddenPerfil = $("#<%=HiddenPerfil.ClientID %>").val();
            var HiddenConstante = $("#<%=HiddenConstante.ClientID %>").val();

            if (HiddenPerfil == HiddenConstante || HiddenPerfil == "") {
                return true;
            }
            return false
        }

        function enableDisableButtons(newValue) {
            var btnModificar = document.getElementById('<%=BtnModificar.ClientID%>');
            btnModificar.disabled = newValue;
            var btnEliminar = document.getElementById('<%=BtnEliminar.ClientID%>');
            btnEliminar.disabled = newValue;
        }

        function CheckTxtEmpty() {
            if ($('#<%=txtRut.ClientID %>').val() == "-" || $('#<%=txtRut.ClientID %>').val() == "") {
                msgAlert('El Rut Aportante es un campo obligatorio.')
                return false;
            } else {
                if ($('#<%=txtNombre.ClientID %>').val() == "") {
                    msgAlert('El Nombre Aportante es un campo obligatorio')
                    return false;
                } else {
                    if ($('#<%=ddlNacExt.ClientID%>').val() == "") {
                        msgAlert('Debe elegir una opción en el campo "Domicilio en el Extranjero"')
                        return false;
                    } else {
                        if ($('#<%=ddlInversionista.ClientID%>').val() == "") {
                            msgAlert('Debe elegir una opción en el campo "Inversionista Calificado"')
                            return false;
                        } else {
                            if ($('#<%=ddlTipoAportante.ClientID%>').val() == "") {
                                msgAlert('Debe elegir una opción en el campo "Tipo Aportante"')
                                return false;
                            } else {
                                if ($('#<%=ddlRelacionado.ClientID%>').val() == "") {
                                    msgAlert('Debe elegir una opción en el campo "Relacionado MAM"')
                                    return false;
                                } else {
                                    if ($('#<%=ddlContrato.ClientID%>').val() == "") {
                                        msgAlert('Debe elegir una opción en el campo "Contrato Distribución"')
                                        return false;
                                    } else {
                                        return true;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        };

        function validateRut(rut) {
            // Despejar Puntos
            var valor = rut.value.replace('.', '').trim();
            // Despejar Guión
            valor = valor.replace('-', '');

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
                rut.value = "";
                return false;
            }
            return true;
        };

        function validarSoloLetras(texto) {
            var valor = texto.value.trim();

            // TODO: SE AGREGAR FUNCION QUE QUITA ESPACIOS EN BLANCO DEL TEXTO          

            // Obtener su Producto con el Múltiplo Correspondiente
            if (!onBlurSoloLetras(valor)) {
                msgAlert("Texto Inválido, solo se aceptan letras");
                texto.value = ""; 
                return false;
            }
            texto.value = valor;
            return true;
        }

        function volver() {

            if (!confirm('¿Seguro que desea volver al menu principal?')) {
                return false;
            } else {
                return true;
            }
        };

        function cerrarAlert() {
            $('#<%=txtAccionHidden.ClientID %>').val("");
        };

    </script>
</asp:Content>
