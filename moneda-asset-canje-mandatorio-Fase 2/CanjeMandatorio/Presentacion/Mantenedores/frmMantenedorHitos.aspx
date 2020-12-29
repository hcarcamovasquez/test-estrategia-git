<%@ Page Title="Hitos" Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false" CodeFile="frmMantenedorHitos.aspx.vb" Inherits="Presentacion_Mantenedores_frmMantenedorHitos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <h2 class="TdRedondeado titleMant">Maestro de <strong>Hitos</strong></h2>

    <div class="card p-4 jumbotron">

        <div class="row">
            <!-- RUT del Fondo-->
            <div class="col-md-4">
                <asp:Label runat="server" ID="lblRut">Fondo</asp:Label>
                <asp:DropDownList ID="ddlRutFondoBusqueda" CssClass="form-control js-select2-rut" runat="server" />
            </div>

            <!-- FECHA DESDE -->
            <div class="col-md-4">
                <asp:Label runat="server" ID="Label2">Fecha Corte desde</asp:Label>
                <div class="input-group">
                    <asp:TextBox ID="txtFechaCorteBusqueda" runat="server" CssClass="form-control datepicker" ReadOnly="True"></asp:TextBox>
                    <asp:LinkButton ID="lnbtnFechaCorteDesde" Class="btn btn-moneda" runat="server" 
                        OnClientClick="return clickCalendar('txtFechaCorteBusqueda')"><i class="far fa-calendar-alt"></i></asp:LinkButton>
                    <asp:LinkButton ID="BtnLimpiarFechaDesde" Text="" class="btn btn-secondary ml-1" runat="server" 
                        OnClientClick="return limpiarCalendar('txtFechaCorteBusqueda')"><i class="far fa-trash-alt"></i></asp:LinkButton>

                </div>
            </div>

            <!-- FECHA HASTA -->
            <div class="col-md-4">
                <asp:Label runat="server" ID="lblFechaHasta">Fecha Corte hasta</asp:Label>
                <div class="input-group">
                    <asp:TextBox ID="txtFechaCorteHasta" runat="server" CssClass="form-control" ReadOnly="True"></asp:TextBox>
                    <asp:LinkButton ID="lnbtnFechaCorteHasta" Class="btn btn-moneda" runat="server" 
                        OnClientClick="return clickCalendar('txtFechaCorteHasta')"><i class="far fa-calendar-alt"></i></asp:LinkButton>
                    <asp:LinkButton ID="lbntnLimpiarCajaTexto" Text="" class="btn btn-secondary ml-1" runat="server" 
                        OnClientClick="return limpiarCalendar('txtFechaCorteHasta')"><i class="far fa-trash-alt"></i></asp:LinkButton>
                    
                </div>
            </div>



        </div>

        <!-- BOTONES BUSCAR LIMPIAR Y CREAR -->
        <div class="row text-center mt-5 p-3 border-bottom">
            <div class="col-md-12">

                <!-- BOTÓN BUSCAR -->
                <asp:Button ID="BtnBuscar" Text="Buscar" class="btn btn-moneda" runat="server" OnClick="BtnBuscar_Click" />
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
                            <asp:RadioButton ID="RowSelector" runat="server" onclick="checkRadioBtn(this);" GroupName="a" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="IdHito" HeaderText="Id Hito" />
                    <asp:BoundField DataField="Rut" HeaderText="Rut del Fondo" />
                    <asp:BoundField DataField="NombreFondo" HeaderText="Nombre del Fondo" />
                    <asp:BoundField DataField="FechaCorte" HeaderText="Fecha de Corte" DataFormatString="{0:dd-MM-yyyy}" />
                    <asp:BoundField DataField="FechaCanje" HeaderText="Fecha de Canje" DataFormatString="{0:dd-MM-yyyy}" />
                    <asp:BoundField DataField="Estado" HeaderText="Estado" Visible="false" />
                    <asp:BoundField DataField="FechaIngreso" HeaderText="Fecha Ingreso" DataFormatString="{0:dd-MM-yyyy}" />
                    <asp:BoundField DataField="UsuarioIngreso" HeaderText="Usuario Ingreso" />
                    <asp:BoundField DataField="FechaModificacion" HeaderText="Fecha Modificación" DataFormatString="{0:dd-MM-yyyy}" />
                    <asp:BoundField DataField="UsuarioModificacion" HeaderText="Usuario Modificador" />
                </Columns>
            </asp:GridView>
        </div>

        <div class="row mt-4">
            <div class="col-md-12 text-center">
                <asp:Button ID="BtnModificar" runat="server" class="btn btn-info" Text="Modificar" Enabled="false" OnClick="BtnModificar_Click"></asp:Button>
                <asp:Button ID="BtnEliminar" runat="server" class="btn btn-danger" Text="Eliminar" OnClientClick="if (!confirm('¿Seguro que desea Eliminar los elementos seleccionados?')) return false;" Enabled="false"></asp:Button>
                <asp:Button ID="BtnExportar" class="btn btn-success" Text="Exportar" runat="server" Enabled="false" />
            </div>
        </div>
        <div>
        </div>
    </div>

    <asp:HiddenField ID="txtHiddenAccion" runat="server" />
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <!-- Bootstrap Modal Dialog Crear/Modificar -->
    <div class="modal fade" id="myModal" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" data-keyboard="false" data-backdrop="static">
        <div class="modal-dialog" style="max-width: 70%;">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title">
                        <asp:Label ID="lblModalFondoTitle" runat="server" Text="Formulario - Nuevo Hito" Font-Bold="true" Font-Size="X-Large">
                        </asp:Label>
                    </h4>
                    <button id="btnXCerrar" type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                </div>

                <div class="modal-body">
                    <div class="jumbotron">
                        <div class="">
                            <div class="card-body p-5 mb-1">
                                <!-- FORMULARIO -->
                                <div class="row mt-4">
                                    <div class="col-md-6">
                                        <asp:Label runat="server" ID="Label3">ID Hito: </asp:Label>
                                        <asp:TextBox ID="txtIdHito" runat="server" CssClass="form-control form-control-sm" ReadOnly="true"></asp:TextBox>
                                    </div>
                                    <div class="col-md-6">
                                        <asp:Label class="form-control-label" runat="server">Fecha de Ingreso</asp:Label>
                                        <asp:TextBox ID="txtFechaIngreso" runat="server" CssClass="form-control form-control-sm" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="row mt-2">

                                    <div class="col-md-6">
                                        <asp:Label runat="server" ID="lblModalRutFondo">Rut del fondo: </asp:Label>
                                        <asp:DropDownList ID="ddlRutdeFondo" CssClass="form-control js-select2-rut" runat="server" OnSelectedIndexChanged="VerificarCombinacion" AutoPostBack="true" />                                         
                                    </div>

                                    <div class="col-md-6">
                                        <asp:Label class="form-control-label" runat="server">Usuario Ingreso</asp:Label>
                                        <asp:TextBox ID="txtUsuarioIngreso" runat="server" CssClass="form-control form-control-sm" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="row mt-2">
                                    <div class="col-md-6">
                                        <asp:Label runat="server" ID="lbNombreRut">Nombre del fondo: </asp:Label>
                                        <asp:DropDownList ID="ddlNombreRutModal" CssClass="form-control js-select2-rut" OnSelectedIndexChanged="VerificarPorNombreFondo" AutoPostBack="true" runat="server" />
                                    </div>
                                    <div class="col-md-6">
                                        <label class="form-control-label">Fecha de Modificación</label>
                                        <asp:TextBox ID="txtFechaModificacion" runat="server" CssClass="form-control form-control-sm" ReadOnly="True"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="row mt-2">
                                    <div class="col-md-6">
                                        <asp:Label runat="server" ID="lblModalFecha">Fecha Corte</asp:Label>
                                        <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Conditional">
                                            <Triggers>
                                                <%-- <asp:AsyncPostBackTrigger ControlID="lnkBtnModalFechaCorte" EventName="click" />
                                               <asp:AsyncPostBackTrigger ControlID="CalendarModalFechaCorte" EventName="SelectionChanged" />--%>
                                                <asp:AsyncPostBackTrigger ControlID="ddlRutdeFondo" EventName="SelectedIndexChanged" />
                                                <asp:AsyncPostBackTrigger ControlID="ddlNombreRutModal" EventName="SelectedIndexChanged" />
                                            </Triggers>
                                            <ContentTemplate>
                                                <div class="input-group">
                                                    <asp:TextBox ID="txtModalFechaCorteFC" runat="server" CssClass="form-control datepicker" Autopostback="true" enabled="false"></asp:TextBox>
                                                    <asp:LinkButton ID="lnkBtnModalFechaCorte" Class="btn btn-moneda" runat="server" 
                                                        OnClientClick="return clickCalendar('txtModalFechaCorteFC')"><i class="far fa-calendar-alt"></i></asp:LinkButton>
                                                    <asp:LinkButton ID="LnkBtnModalLimpiarFecha" Text="" class="btn btn-secondary ml-1" runat="server" 
                                                        OnClientClick="return limpiarCalendar('txtModalFechaCorteFC')"><i class="far fa-trash-alt"></i></asp:LinkButton>

                                                    <span id="reqtxtModalFechaCorte" class="reqError"></span>
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                    <div class="col-md-6">
                                        <label class="form-control-label">Usuario de Modificación</label>
                                        <asp:TextBox ID="txtUsuarioModificacion" runat="server" CssClass="form-control form-control-sm" ReadOnly="True"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="row mt-2">
                                    <div class="col-md-6">
                                        <asp:Label runat="server" ID="Label1">Fecha Canje</asp:Label>
                                        <asp:UpdatePanel runat="server" ID="UpdatePanel2" UpdateMode="Conditional">
                                            <Triggers>
                                                <%-- <asp:AsyncPostBackTrigger ControlID="lnkBtnModalFechaCanje" EventName="click" />
                                               <asp:AsyncPostBackTrigger ControlID="CalendarModalFechaCorte" EventName="SelectionChanged" />--%>
                                            </Triggers>
                                            <ContentTemplate>
                                                <div class="input-group">
                                                    <asp:TextBox ID="txtModalFechaCanjeFC" runat="server" CssClass="form-control datepicker"  Autopostback="true"  enabled="false"></asp:TextBox>
                                                    <asp:LinkButton ID="lnkBtnModalFechaCanje" Class="btn btn-moneda" runat="server" 
                                                        OnClientClick="return clickCalendar('txtModalFechaCanjeFC')"><i class="far fa-calendar-alt"></i></asp:LinkButton>
                                                    <asp:LinkButton ID="lnkBtnModalLimpiarCanje" Text="" class="btn btn-secondary ml-1" runat="server" 
                                                        OnClientClick="return limpiarCalendar('txtModalFechaCanjeFC')"><i class="far fa-trash-alt"></i></asp:LinkButton>
                                                    
                                                    <span id="reqtxtModalFecha" class="reqError"></span>
                                                </div>
                                           </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                                <hr />
                                <asp:HiddenField ID="txtEstadoCambio" runat="server"></asp:HiddenField>
                                <!-- GRUPO DE BOTONES 2 -->
                                <div class="form-group mt-5 text-right">
                                    <div class="col-md-offset-1">
                                        <asp:Button ID="btnModalGuardarModificar" Text="Modificar" CssClass="btn btn-info" runat="server" OnClientClick="return validateBtn();"></asp:Button>
                                        <asp:Button ID="btnModalGuardarCrear" Text="Guardar" CssClass="btn btn-info" runat="server" OnClientClick="return validateBtn();"></asp:Button>
                                        <asp:Button ID="btnModalCancelar" Text="Cancelar" CssClass="btn btn-secondary" runat="server" OnClientClick="if (!confirm('¿Seguro que desea Cancelar?')) return false;"></asp:Button>
                                        <asp:Button ID="btnModalEliminarHito" Text="Eliminar" runat="server" class="btn btn-danger" OnClientClick="if (!confirm('¿Seguro que desea Eliminar los elementos seleccionados?')) return false;"></asp:Button>
                                    </div>
                                </div>
                                <!-- FIN GRUPO DE BOTONES 2 -->
                            </div>
                        </div>

                    </div>
                </div>

                <!--<div class="modal-footer"> </div>-->
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
                    <button type="button" class="close" data-dismiss="modal" aria-label="Cerrar" onclick="cerrarAlert(); return true">
                        <span aria-hidden="true">&times;</span>
                    </button>
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

        function ValidarFechas() {
            var fechaCorte = $("[id*=txtModalFechaCorteFC]").datepicker("getDate");
            var fechaCanje = $("[id*=txtModalFechaCanjeFC]").datepicker("getDate");

<%--            var fechaCorte = document.getElementById('<%=txtModalFechaCorteFC.ClientID%>').value;
            var fechaCanje = document.getElementById('<%=txtModalFechaCanjeFC.ClientID%>').value;--%>
            if (Date.parse(fechaCanje) >= Date.parse(fechaCorte)) {
                return true;
            } else {
                //if (Date.parse(fechaCanje) < Date.parse(fechaCorte)) {
                    alert("Fecha Canje debe ser mayor o igual a Fecha Corte");
                    return false;
               // }
            }
        }

        $(".close").click(function () {
            var txtHiddenAccion = $('#<%=txtHiddenAccion.ClientID %>').val();
            $('#<%=txtHiddenAccion.ClientID %>').val(""); 
         });

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

           
           Sys.WebForms.PageRequestManager.getInstance().add_endRequest(bindDataTable);
           bindDataTable();
       });


        //On UpdatePanel Refresh
        function bindDataTable() {
            $(function () {
                $("#<%=ddlRutdeFondo.ClientID%>").change(function () {
                    var checkStatus = $('#<%=txtEstadoCambio.ClientID%>').val()
                 console.log("checkStatus " + checkStatus)
                 if (checkStatus == "") {
                     var index = $(this).prop("selectedIndex");
                     $('#<%=ddlNombreRutModal.ClientID%>').prop("selectedIndex", index);
                     $('#<%=txtEstadoCambio.ClientID%>').val("1")
                     $('#<%=ddlNombreRutModal.ClientID%>').trigger('change');
                     $('#<%=txtEstadoCambio.ClientID%>').val("")
                 }
                });

                $("[id*=txtFechaCorteBusqueda]").datepicker();
                $("[id*=txtFechaCorteHasta]").datepicker();

                $("[id*=txtModalFechaCorteFC]").datepicker({
                    container: '#myModal modal-body'
                    , showOn: "none"
                });

                $("[id*=txtModalFechaCanjeFC]").datepicker({
                    container: '#myModal modal-body'
                    , showOn: "none"
                });

                $("[id*=txtFechaCorteBusqueda]").change(function () {
                    changeFechas($("[id*=txtFechaCorteBusqueda]"), $("[id*=txtFechaCorteHasta]"), 1)
                });

                $("[id*=txtFechaCorteHasta]").change(function () {
                    changeFechas($("[id*=txtFechaCorteBusqueda]"), $("[id*=txtFechaCorteHasta]"), 2)
                });

                 $("[id*=txtModalFechaCorteFC]").change(function () {
                //    var fd = $("[id*=txtModalFechaCorteFC]").datepicker("getDate");
                //return (fd != "");
                });

                $("[id*=txtModalFechaCanjeFC]").change(function () {
                    //var fd = $("[id*=txtModalFechaCorteFC]").datepicker("getDate");
                    //return (fd != "");
                });

            });

            $(function () {
                $('#<%=ddlNombreRutModal.ClientID%>').change(function () {
                console.log("ddlNombreRutModal")
                var checkStatus = $('#<%=txtEstadoCambio.ClientID%>').val()
                if (checkStatus == "") {
                    var index = $(this).prop("selectedIndex");
                    $("#<%=ddlRutdeFondo.ClientID%>").prop("selectedIndex", index);
                    $('#<%=txtEstadoCambio.ClientID%>').val("2")
                    $("#<%=ddlRutdeFondo.ClientID%>").trigger('change');
                    $('#<%=txtEstadoCambio.ClientID%>').val("")
                }
            });
        });
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
            if (!confirm('¿Seguro que desea Guardar?')) {
                return false;
            } else {
                if (CheckTxtEmpty()) {
                    if (ValidarFechas()) {
                        return true;
                    } else {
                        return false;
                    }
                } else {
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

        function isPerfilConsulta() {
            var HiddenPerfil = $("#<%=HiddenPerfil.ClientID %>").val();
            var HiddenConstante = $("#<%=HiddenConstante.ClientID %>").val();

            if (HiddenPerfil == HiddenConstante || HiddenPerfil == "") {
                return true;
            }
            return false
        }

        function CheckTxtEmpty() {
            if ($('#<%=ddlNombreRutModal.ClientID %>').val() == "") {
                alert('El Rut es un campo obligatorio.')
                return false;
            }
            if ($('#<%=ddlRutdeFondo.ClientID %>').val() == "") {
                alert('El nombre fondo es un campo obligatorio')
                return false;
            }
            if ($('#<%=txtModalFechaCorteFC.ClientID %>').val() == "") {
                alert('La fecha de corte es un campo obligatorio')
                return false;
            }
            if ($('#<%=txtModalFechaCanjeFC.ClientID %>').val() == "") {
                alert('La fecha de canje es un campo obligatorio')
                return false;
            }
            return true;
        }

        function validateRut(rut) {
            // Despejar Puntos
            var valor = rut.value.replace('.', '');
            // Despejar Guión
            valor = valor.replace('-', '');

            // Aislar Cuerpo y Dígito Verificador
            cuerpo = valor.slice(0, -1);
            dv = valor.slice(-1).toUpperCase();

            // Formatear RUN
            rut.value = cuerpo + '-' + dv

            // Si no cumple con el mínimo ej. (n.nnn.nnn)
            if (cuerpo.length < 7) {
                alert("RUT Incompleto");
                return false;
            }

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
                alert("RUT Inválido");
                return false;
            }
            return true;
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



