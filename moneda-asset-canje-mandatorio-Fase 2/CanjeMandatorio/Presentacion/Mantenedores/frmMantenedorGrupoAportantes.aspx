<%@ Page Title="Grupo Aportantes" Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false" CodeFile="frmMantenedorGrupoAportantes.aspx.vb" Inherits="Presentacion_Mantenedores_frmMantenedorGrupoAportantes" %>

<script runat="server">

    Protected Sub btnModalAgregar_Click1(sender As Object, e As EventArgs)
    End Sub

</script>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:scriptmanager id="ScriptManager1" runat="server"></asp:scriptmanager>
    <h2 class="TdRedondeado titleMant">Maestro de <strong>Grupos de Aportantes</strong></h2>

    <div class="card p-4 jumbotron">

        <div class="row">
            <!-- ID Grupo-->
            <div class="col-md-4">
                <asp:label runat="server" id="lblIdGrupo">Grupo</asp:label>
                 <asp:dropdownlist id="ddlIdGrupo" cssclass="form-control js-select2-rut" runat="server" />
            </div>

            <!-- FECHA DESDE -->
            <div class="col-md-4">
                <asp:label runat="server" id="Label2">Fecha Creación desde</asp:label>
                <div class="input-group">
                    <asp:textbox id="txtFeCreacionDesde" runat="server" cssclass="form-control datepicker" readonly="True"></asp:textbox>
                    <asp:linkbutton id="LinkButton1" class="btn btn-moneda" runat="server" OnClientClick="return clickCalendar('txtFeCreacionDesde')"><i class="far fa-calendar-alt"></i></asp:linkbutton>
                    <asp:linkbutton id="BtnLimpiarFechaDesde" text="" class="btn btn-secondary ml-1" runat="server" OnClientClick="return limpiarCalendar('txtFeCreacionDesde')"><i class="far fa-trash-alt"></i></asp:linkbutton>
                    <asp:calendar id="Calendar1" runat="server" visible="False" backcolor="White" bordercolor="White" borderwidth="1px" font-names="Verdana" font-size="9pt" forecolor="Black" height="190px" nextprevformat="FullMonth" width="350px" class="calendarios">
                    <DayHeaderStyle Font-Bold="True" Font-Size="8pt" />
                    <NextPrevStyle Font-Bold="True" Font-Size="8pt" ForeColor="#333333" VerticalAlign="Bottom" />
                    <OtherMonthDayStyle ForeColor="#999999" />
                    <SelectedDayStyle BackColor="#333399" ForeColor="White" />
                    <TitleStyle BackColor="White" BorderColor="Black" BorderWidth="4px" Font-Bold="True" Font-Size="12pt" ForeColor="#333399" />
                    <TodayDayStyle BackColor="#CCCCCC" />
                    </asp:calendar>
                </div>
            </div>

            <!-- FECHA HASTA -->
            <div class="col-md-4">
                <asp:label runat="server" id="lblFechaHasta">Fecha Creación hasta</asp:label>
                <div class="input-group">
                    <asp:textbox id="txtFeCreacionHasta" runat="server" cssclass="form-control datepicker" readonly="True"></asp:textbox>
                    <asp:linkbutton id="LinkButton2" class="btn btn-moneda" runat="server" OnClientClick="return clickCalendar('txtFeCreacionHasta')"><i class="far fa-calendar-alt"></i></asp:linkbutton>
                    <asp:linkbutton id="BtnLimpiarFechaHasta" text="" class="btn btn-secondary ml-1" runat="server" OnClientClick="return limpiarCalendar('txtFeCreacionHasta')"><i class="far fa-trash-alt"></i></asp:linkbutton>
                    <asp:calendar id="Calendar2" runat="server" visible="False" backcolor="White" bordercolor="White" borderwidth="1px" font-names="Verdana" font-size="9pt" forecolor="Black" height="190px" nextprevformat="FullMonth" width="350px" class="calendarios">
                    <DayHeaderStyle Font-Bold="True" Font-Size="8pt" />
                    <NextPrevStyle Font-Bold="True" Font-Size="8pt" ForeColor="#333333" VerticalAlign="Bottom" />
                    <OtherMonthDayStyle ForeColor="#999999" />
                    <SelectedDayStyle BackColor="#333399" ForeColor="White" />
                    <TitleStyle BackColor="White" BorderColor="Black" BorderWidth="4px" Font-Bold="True" Font-Size="12pt" ForeColor="#333399" />
                    <TodayDayStyle BackColor="#CCCCCC" />
                    </asp:calendar>
                </div>
            </div>
            <!-- NOMBRE DEL GRUPO-->
            <div class="col-md-5">
                <asp:label runat="server" id="lblNombreGrupo" visible="False">Nombre del grupo</asp:label>
                <asp:dropdownlist id="ddlNombreGrupo" cssclass="form-control js-select2-rut" runat="server" visible="False" />
            </div>
        </div>

        <!-- BOTONES BUSCAR LIMPIAR Y CREAR -->
        <div class="row text-center mt-5 p-3 border-bottom">
            <div class="col-md-12">
                <!-- BOTÓN BUSCAR -->
                <asp:button id="BtnBuscar" text="Buscar" class="btn btn-moneda" runat="server" />
                <asp:button id="btnLimpiarFrm" text="Borrar" class="btn btn-secondary" runat="server"/> 

                <!-- BOTÓN CREAR -->
                <asp:button id="BtnCrear" text="Crear" class="btn btn-info" runat="server" />
            </div>
        </div>

        <!-- TABLA DE RESULTADOS -->
        <h5 class="mt-3 text-secondary"><i class="fas fa-file-invoice fa-sm"></i> Resultado de la búsqueda</h5>
        <div class="table-responsive card mt-4 p-3">
            <asp:gridview
                id="GrvTabla"
                runat="server"
                cssclass="table table-bordered table-hover table-sm gvv"
                headerstyle-backcolor=""
                headerstyle-font-size=""
                rowstyle-font-size="Small"
                autogeneratecolumns="false"
                allowsorting="false">
                <Columns>
                    <asp:TemplateField HeaderText="Sel.">
                        <ItemTemplate>
                            <asp:RadioButton ID="RowSelector" runat="server" onclick="checkRadioBtn(this);" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="IdGrupo" HeaderText="Id Grupo" />
                    <asp:BoundField DataField="NombreGrupo" HeaderText="Nombre del grupo" />
                    <asp:BoundField DataField="RutAportante" HeaderText="Rut Aportante" />
                    <asp:BoundField DataField="NombreAportante" HeaderText="Nombre Aportante" Visible="false" />
                    <asp:BoundField DataField="Estado" HeaderText="Estado" visible="false"/>
                    <asp:BoundField DataField="FechaIngreso" HeaderText="Fecha Ingreso" DataFormatString="{0:dd-MM-yyyy}"/>
                    <asp:BoundField DataField="UsuarioIngreso" HeaderText="Usuario Ingreso"/>
                    <asp:BoundField DataField="FechaModificacion" HeaderText="Fecha Modificación" DataFormatString="{0:dd-MM-yyyy}"/>
                    <asp:BoundField DataField="UsuarioModificacion" HeaderText="Usuario Modificador"/>
                </Columns>
            </asp:gridview>
        </div>


        <div class="row mt-4">
            <div class="col-md-12 text-center">
                <asp:button id="BtnModificar" runat="server" class="btn btn-info" text="Modificar" enabled="false"></asp:button>
                <asp:button id="BtnEliminar" runat="server" class="btn btn-danger" text="Eliminar" onclientclick="if (!confirm('¿Seguro que desea Eliminar los elementos seleccionados?')) return false;" enabled="false"></asp:button>
                <asp:button id="BtnExportar" class="btn btn-success" text="Exportar" runat="server" enabled="false" />
            </div>
            <div class="col-md-2">
                <asp:button id="btnVolver" runat="server" width="125px" class="btn btn-secondary" text="Volver" onclientclick="return volver();" visible="False"></asp:button>
            </div>
        </div>
        <div>
        </div>
    </div>

    <!-- Bootstrap Modal Dialog Crear/Modificar -->
    <div class="modal fade" id="myModal" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" data-keyboard="false" data-backdrop="static">
        <div class="modal-dialog" style="max-width: 90%;">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title">
                        <asp:label id="lblModalTitulo" runat="server" text="Formulario - Grupo aportantes" font-bold="true" font-size="X-Large">
                        </asp:label>
                    </h4>
                    <button id="btnXCerrar" type="button" class="close" data-dismiss="modal" aria-label="Close" onclick="cerrarAlert(); return true;">
                        <span aria-hidden="true" onclick="cerrarAlert(); return true;">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div class="jumbotron">
                        <div class="card p-4">
                            <div class="row">
                                <!-- FORMULARIO-->
                                <div class="card col-md-5 p-4">

                                    <!-- FORMULARIO -->
                                    <div class="row">
                                        <div class="col-md-6">
                                            <asp:label runat="server" id="lblModalIdGrupo">ID de grupo: </asp:label>
                                        </div>
                                        <div class="col-md-6">
                                            <asp:textbox id="txtModalIdGrupo" runat="server" cssclass="form-control" readonly="True"></asp:textbox>
                                        </div>
                                    </div>
                                    <div class="row mt-3">
                                        <div class="col-md-6">
                                            <asp:label runat="server" id="lblModalNombreGrupo">Nombre del grupo: </asp:label>
                                        </div>
                                        <div class="col-md-6">
                                            <asp:hiddenfield id="txtHiddenOldValue" runat="server"></asp:hiddenfield>
                                            <asp:textbox id="txtModalNombreGrupo" 
                                                    onblur="return IsAccTextValid(this);" 
                                                runat="server" 
                                                maxlength="100" 
                                                cssclass="form-control" 
                                                onkeypress="return soloLetras(event)"></asp:textbox>
                                        </div>
                                    </div>

                                    <br />
                                    <br />
                                    <h6>Asignación de aportantes</h6>
                                    <hr />
                                    <br />

                                    <div class="row">
                                        <div class="col-md-6">
                                            <asp:label runat="server" id="lblModalRutAportante">Rut aportante: </asp:label>
                                        </div>
                                        <div class="col-md-6">
                                            <asp:dropdownlist id="ddlModalRutAportantes" cssclass="form-control js-select2-rut" runat="server" />
                                        </div>
                                    </div>
                                    <div class="row mt-3">
                                        <div class="col-md-6">
                                            <%-- <asp:Label runat="server" ID="lblModalNombreAportante">Nombre del aportante: </asp:Label>--%>
                                        </div>
                                        <div class="col-md-6">
                                            <%--                                            <asp:UpdatePanel runat="server" ID="UpdatePanel" UpdateMode="Conditional">
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="ddlModalRutAportantes" EventName="SelectedIndexChanged" />
                                                </Triggers>
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="ddlModalNombreAportante" CssClass="form-control js-select2-rut" runat="server" />
                                                </ContentTemplate>
                                            </asp:UpdatePanel>--%>
                                        </div>
                                    </div>
                                    <!-- FIN FORMULARIO -->
                                    <asp:hiddenfield id="txtEstadoCambio" runat="server"></asp:hiddenfield>
                                    <asp:hiddenfield id="txtAccionHidden" runat="server" />

                                    <!-- GRUPO DE BOTONES -->
                                    <div class="form-group mt-5">
                                        <div class="col-md-offset-1">
                                            <asp:button id="btnModalAgregar" text="Agregar" cssclass="btn btn-success" runat="server" enabled="false"></asp:button>
                                            <asp:button id="btnModalModificar" text="Modificar" cssclass="btn btn-info" runat="server" onclientclick="javascript:return confirma('¿Seguro que desea Guardar?');" enabled="false"></asp:button>
                                            <asp:button id="btnModalEliminarAportante" text="Eliminar Aportante" runat="server" class="btn btn-danger" onclientclick="if (!confirm('¿Seguro que desea Eliminar el Grupo seleccionado?')) return false;" enabled="false"></asp:button>
                                        </div>
                                    </div>
                                    <!-- FIN GRUPO DE BOTONES -->
                                </div>

                                <!-- TABLA -->
                                <div class="card col-md-7 p-4">

                                    <h4>Asignación</h4>
                                    <hr />
                                    <br />

                                    <asp:updatepanel runat="server" id="UpdatePanelGrilla" updatemode="Conditional">
                                        <Triggers>
                                           <asp:AsyncPostBackTrigger ControlID="btnModalAgregar" EventName="Click" />
                                           <asp:AsyncPostBackTrigger ControlID="btnModalModificar" EventName="Click" />
                                            <asp:AsyncPostBackTrigger ControlID="btnModalEliminarAportante" EventName="Click" />
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
                                                            <asp:RadioButton ID="RowSelector" runat="server" onclick="checkRadioBtn2(this);" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="IdGrupo" HeaderText="Id Grupo" />
                                                    <asp:BoundField DataField="NombreGrupo" HeaderText="Nombre del grupo" />
                                                    <asp:BoundField DataField="RutAportante" HeaderText="Rut Aportante" />
                                                    <asp:BoundField DataField="NombreAportante" HeaderText="Nombre Aportante" Visible="false"  />
                                                    <asp:BoundField DataField="Estado" HeaderText="Estado" Visible="false" />
                                                    <asp:BoundField DataField="FechaIngreso" HeaderText="Fecha Creación" Visible="false" />
                                                    <asp:BoundField DataField="UsuarioIngreso" HeaderText="Usuario Creador" Visible="false" />
                                                    <asp:BoundField DataField="FechaModificacion" HeaderText="Fecha Modificación" Visible="false" />
                                                    <asp:BoundField DataField="UsuarioModificacion" HeaderText="Usuario Modificador" Visible="false" />
                                                </Columns>
                                            </asp:GridView>

                                        </ContentTemplate>
                                    </asp:updatepanel>

                                </div>
                            </div>

                            <!-- GRUPO DE BOTONES 2 -->
                            <div class="form-group mt-5 text-right">
                                <div class="col-md-offset-1">
                                    <asp:button id="btnModalGuardar" text="Guardar" cssclass="btn btn-info" runat="server" onclientclick="if (!confirm('¿Seguro que desea Guardar?')) return false;" enabled="false"></asp:button>
                                    <asp:button id="btnModalCancelar" text="Cancelar" cssclass="btn btn-secondary" runat="server" onclientclick="if (!confirm('¿Seguro que desea Cancelar?')) return false;"></asp:button>
                                    <asp:button id="btnModalEliminarGrupo" text="Eliminar grupo" runat="server" class="btn btn-danger" onclientclick="if (!confirm('¿Seguro que desea Eliminar los elementos seleccionados?')) return false;" enabled="false"></asp:button>
                                </div>
                            </div>
                            <!-- FIN GRUPO DE BOTONES 2 -->
                        </div>
                    </div>
                </div>

                <!--<div class="modal-footer">                    
                </div>-->
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
                        <asp:image id="img_modal" imageurl="~/Img/info1.png" runat="server" width="130" height="50" />
                        <asp:label id="lblModalTitle" runat="server" text="" font-bold="true" font-size="X-Large">
                            </asp:label>
                    </h4>
                    <asp:button id="btnCerraModal" runat="server" text="&times;" cssclass="btn rounded-circle close ml-5" />
                </div>
                <div class="modal-body">
                    <asp:label id="lblModalBody" runat="server" font-size="X-Large" text=""></asp:label>
                    <br>
                    <br />
                    <asp:hyperlink id="linkArchivo" runat="server"></asp:hyperlink>
                    <div class="text-center">
                        <asp:image id="img_body_modal" runat="server" imageurl="~/Img/important.png" width="100" height="100" />
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:button id="Button1" runat="server" cssclass="btn btn-info" text="Cerrar" />
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
            $("body").on("click", "#<%=btnModalGuardar.ClientID%>", function () {
                return Confirma('¿Seguro que desea Guardar?');
            });

            $("body").on("click", "#btnXCerrar", function () {
                $("#btnXCerrar").val("");
                cerrarAlert();
            });

            $("[id*=txtFeCreacionDesde]").datepicker();
            $("[id*=txtFeCreacionHasta]").datepicker();

            $("[id*=txtFeCreacionDesde]").change(function(){
                  changeFechas( $("[id*=txtFeCreacionDesde]"), $("[id*=txtFeCreacionHasta]"), 1)
            });
            $("[id*=txtFeCreacionHasta]").change(function(){
                  changeFechas( $("[id*=txtFeCreacionDesde]"), $("[id*=txtFeCreacionHasta]"), 2)
            });

            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(bindDataTable);
        });

        function formatState(state) {
            if (!state.id) {
                return state.text;
            }
            var $state = $(
                '<span>' + state.text + '</span>'
            );
            return $state;
        };

        function msgAlert(mensaje) {
            $('#pMensajeAlert').text(mensaje);
            $('#h5dialogTitle').html("Error");
            $('#modalAlert').modal();
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
        };


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
        };

        function llamarFuncion() {
            var gv = document.getElementById('<%=grvAsignacion.ClientID %>');
            var txtAccionHidden = $("#<%=txtAccionHidden.ClientID %>").val();
            if (gv != null) {
                if ((txtAccionHidden == "MODIFICAR") || (txtAccionHidden == "ELIMINAR")) {
                    for (var i = 1; i < gv.rows.length; i++) {
                        var radioBtn = gv.rows[i].cells[0].getElementsByTagName("input");
                        radioBtn[0].checked = false;
                        enableDisableButtons2(true);
                    }

                } else if (txtAccionHidden == "CREAR") {
                    enableDisableButtons2((gv.rows.length > 0));
                }
            }
        };

        function enableDisableButtons2(newValue) {
            var btnModalEliminarGrupo = document.getElementById('<%=btnModalEliminarGrupo.ClientID%>');

            if (btnModalEliminarGrupo.disabled) {
                var btnModificar = document.getElementById('<%=btnModalModificar.ClientID%>');
                btnModificar.disabled = newValue;
                var btnEliminarAportante = document.getElementById('<%=btnModalEliminarAportante.ClientID%>');
                btnEliminarAportante.disabled = newValue;
            }
            else {
                var btnEliminarAportante = document.getElementById('<%=btnModalEliminarAportante.ClientID%>');
                btnEliminarAportante.disabled = newValue;
            }

        };

        function IsAccTextValid(txtModalNombreGrupo) {
            const hiddenAction = $(txtModalNombreGrupo).closest('.card').find('input[type="hidden"][name*="txtAccionHidden"]').val();

            var txtHiddenOldValue = $("#<%=txtHiddenOldValue.ClientID %>").val();
            var btnModalAgregar = document.getElementById('<%=btnModalAgregar.ClientID%>');
            var btnModalGuardar = document.getElementById('<%=btnModalGuardar.ClientID%>');
            var btnModalEliminarAportante = document.getElementById('<%=btnModalEliminarAportante.ClientID%>');
            var btnModalModificar = document.getElementById('<%=btnModalModificar.ClientID%>');

            var gv = document.getElementById('<%=grvAsignacion.ClientID %>');

            if (txtModalNombreGrupo.value.length == 0 && hiddenAction == "CREAR") {
                btnModalAgregar.disabled = true;
            } else if (txtModalNombreGrupo.value.length != 0 && hiddenAction == "CREAR") {
                btnModalAgregar.disabled = false;
            }

            // ¿ cuando se habilita el boton btnModalGuardar ? 
            if ((hiddenAction == "MODIFICAR" || hiddenAction == "CREAR") && txtModalNombreGrupo.value.length == 0) {
                btnModalGuardar.disabled = true;

            } else if (hiddenAction == "MODIFICAR" || hiddenAction == "CREAR") {
                if (gv == null) {
                    btnModalGuardar.disabled = true;
                    btnModalEliminarAportante.disabled = true
                    btnModalModificar.disabled = true
                } else {
                    btnModalGuardar.disabled = ((gv.rows.length <= 0) && (txtHiddenOldValue != txtModalNombreGrupo.value));
                    ////TODO: MODIFICAR VERIFICAR QUE ESTE SELECCIONADO ALGO
                    //if (hiddenAction == "MODIFICAR") {
                    //    btnModalEliminarAportante.disabled = (gv.rows.length <= 0)
                    //    btnModalModificar.disabled = (gv.rows.length <= 0)
                    //}                    
                }
            } else if (hiddenAction == "ELIMINAR") {
                if (gv == null) {
                    btnModalGuardar.disabled = true;
                    btnModalEliminarAportante.disabled = false;
                } else {
                    btnModalEliminarAportante.disabled = (gv.rows.length <= 0);
                    btnModalGuardar.disabled = (gv.rows.length <= 0);
                }
            }

            return validarSoloLetras(txtModalNombreGrupo);  

        };
        function validarSoloLetras(texto) {
            var valor = texto.value.trim();

            // TODO: SE AGREGAR FUNCION QUE QUITA ESPACIOS EN BLANCO DEL TEXTO          
            console.log("[" + valor + "]");
            console.log("[" + texto.value + "]");
            // Obtener su Producto con el Múltiplo Correspondiente
            if (!onBlurSoloLetras(valor)) {
                msgAlert("Texto Inválido, solo se aceptan letras");
                return false;
            }
            texto.value = valor;

            return true;
        }
        function Confirma(msg) {
            if (!confirm(msg)) {
                return false;
            } else {
                return true;
            }
        };

        function volver() {
            return (!confirma('¿Seguro que desea volver al menu principal?'))
        };

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

            IsAccTextValid(document.getElementById('<%=txtModalNombreGrupo.ClientID%>'));
            llamarFuncion();
        };

        function cerrarAlert() {
            $('#<%=txtAccionHidden.ClientID %>').val("");
        };

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