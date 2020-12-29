<%@ Page Title="Usuarios" Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false" CodeFile="frmMantenedorUsuarios.aspx.vb" Inherits="Presentacion_Administracion_frmMantenedorUsuarios" %>
<%@ MasterType virtualPath="~/Site.master"%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">

    <h2 class="TdRedondeado titleMant">Mantenedor de <strong> Usuarios</strong></h2>

    <div class="card p-4 jumbotron">

        <div class="row">
            <!-- Nombre-->
            <div class="col-md-3">
                <asp:Label runat="server" ID="lblRut">Nombre Usuario</asp:Label>
                <asp:DropDownList ID="ddlNombreUsuario" CssClass="form-control js-select2-rut" runat="server" />
            </div>
            <!-- PERFIL DE USUARIO-->
            <div class="col-md-3">
                <asp:Label runat="server" ID="lblPerfilUsuario">Perfil usuario</asp:Label>
                <asp:DropDownList ID="ddlPerfilUsuario" CssClass="form-control js-select2-rut" runat="server">
                    <asp:ListItem Value="&nbsp;"></asp:ListItem>
                    <asp:ListItem Value="1">Perfil Consulta</asp:ListItem>
                    <asp:ListItem Value="2">Perfil Full</asp:ListItem>
                    <asp:ListItem Value="3">Perfil Administrador</asp:ListItem>
                </asp:DropDownList>
            </div>

            <!-- FECHA DESDE -->
            <div class="col-md-3">
                <asp:Label runat="server" ID="Label2">Fecha Creación desde</asp:Label>
                <div class="input-group">
                <asp:TextBox ID="txtFeCreacionDesde" runat="server" CssClass="form-control datepicker" ReadOnly="True"></asp:TextBox>
                <asp:LinkButton ID="LinkButton1" Class="btn btn-moneda" runat="server" OnClientClick="return clickCalendar('txtFeCreacionDesde')"><i class="far fa-calendar-alt"></i></asp:LinkButton>
                <asp:LinkButton ID="BtnLimpiarFechaDesde" Text="Borrar" class="btn btn-secondary" runat="server" OnClientClick="return limpiarCalendar('txtFeCreacionDesde')"><i class="far fa-trash-alt"></i></asp:LinkButton>
                <asp:Calendar ID="Calendar1" runat="server" Visible="False" BackColor="White" BorderColor="White" BorderWidth="1px" Font-Names="Verdana" Font-Size="9pt" ForeColor="Black" Height="190px" NextPrevFormat="FullMonth" Width="350px" CssClass="calendarios">
                    <DayHeaderStyle Font-Bold="True" Font-Size="8pt" />
                    <NextPrevStyle Font-Bold="True" Font-Size="8pt" ForeColor="#333333" VerticalAlign="Bottom" />
                    <OtherMonthDayStyle ForeColor="#999999" />
                    <SelectedDayStyle BackColor="#333399" ForeColor="White" />
                    <TitleStyle BackColor="White" BorderColor="Black" BorderWidth="1px" Font-Bold="True" Font-Size="12pt" ForeColor="#333399" CssClass="cabecera texto-cabecera" />
                    <TodayDayStyle BackColor="#CCCCCC" />
                    </asp:Calendar>
                </div>
            </div> 
            <!-- FECHA HASTA -->
            <div class="col-md-3">
                <asp:Label runat="server" ID="lblFechaHasta">Fecha Creación hasta</asp:Label>
                <div class="input-group">
                <asp:TextBox ID="txtFeCreacionHasta" runat="server" CssClass="form-control" ReadOnly="True"></asp:TextBox>
                <asp:LinkButton ID="LinkButton2" Class="btn btn-moneda" runat="server" OnClientClick="return clickCalendar('txtFeCreacionHasta')"><i class="far fa-calendar-alt"></i></asp:LinkButton>
                <asp:LinkButton ID="BtnLimpiarFechaHasta" Text="" class="btn btn-secondary" runat="server" OnClientClick="return limpiarCalendar('txtFeCreacionHasta')"><i class="far fa-trash-alt"></i></asp:LinkButton>
                <asp:Calendar ID="Calendar2" runat="server" Visible="False" BackColor="White" BorderColor="White" BorderWidth="1px" Font-Names="Verdana" Font-Size="9pt" ForeColor="Black" Height="190px" NextPrevFormat="FullMonth" Width="350px" CssClass="calendarios">
                    <DayHeaderStyle Font-Bold="True" Font-Size="8pt" />
                    <NextPrevStyle Font-Bold="True" Font-Size="8pt" ForeColor="#333333" VerticalAlign="Bottom" />
                    <OtherMonthDayStyle ForeColor="#999999" />
                    <SelectedDayStyle BackColor="#333399" ForeColor="White" />
                    <TitleStyle BackColor="White" BorderColor="Black" BorderWidth="1px" Font-Bold="True" Font-Size="12pt" ForeColor="#333399" CssClass="cabecera texto-cabecera" />
                    <TodayDayStyle BackColor="#CCCCCC" />
                    </asp:Calendar>
                </div>
            </div>

         </div>  

<!-- BOTONES BUSCAR LIMPIAR Y CREAR -->           
    <div class="row text-center mt-5 p-3 border-bottom">
          <div class="col-md-12">
         
            <!-- BOTÓN BUSCAR -->
             <asp:Button ID="BtnBuscar" Text="Buscar" class="btn btn-moneda" runat="server"/>
                <asp:Button ID="btnLimpiarFrm" Text="Borrar" class="btn btn-secondary" runat="server"/>
            
           <!-- BOTÓN CREAR -->
                <asp:Button ID="btnCrear" Text="Crear" class="btn btn-info" runat="server"/>
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
                AutoGenerateColumns="False"
                EnableModelValidation="True">
                <Columns>
                    <asp:TemplateField HeaderText="Select">
                        <ItemTemplate>
                            <asp:RadioButton ID="RowSelector" runat="server" onclick="checkRadioBtn(this);" GroupName="a" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="US_ID" HeaderText="ID" />
                    <asp:BoundField DataField="US_Nombre" HeaderText="Nombre de usuario" />
                    <asp:BoundField DataField="Perfil_Chr" HeaderText="Perfil" />
                    <asp:BoundField DataField="US_Estado" HeaderText="Estado" Visible="false"/>
                    <asp:BoundField DataField="US_FechaIngreso" HeaderText="Fecha de ingreso" DataFormatString="{0:dd-MM-yyyy}"/>
                    <asp:BoundField DataField="US_UsuarioIngreso" HeaderText="Usuario creador" />
                    <asp:BoundField DataField="US_FechaModificacion" HeaderText="Fecha modificación" DataFormatString="{0:dd-MM-yyyy}"/>
                    <asp:BoundField DataField="US_UsuarioModificacion" HeaderText="Usuario modificador" />
                </Columns>
            </asp:GridView>
        </div>

        <div class="row mt-3">
            <div class="col-md-12 text-center">
                <asp:Button ID="BtnModificar" runat="server" class="btn btn-info" Text="Modificar" Enabled="false"></asp:Button>
                <asp:Button ID="BtnEliminar" runat="server" class="btn btn-danger" Text="Eliminar" OnClientClick="if (!confirm('¿Seguro que desea Eliminar los elementos seleccionados?')) return false;" Enabled="false"></asp:Button>
                <asp:Button ID="BtnExportar" class="btn btn-success" Text="Exportar" runat="server" Enabled="false"/>
            </div>
            <div class="col-md-2">
                <asp:Button ID="btnVolver" runat="server" Width="125px" class="btn-secondary btnNormal" Text="Volver" OnClientClick="return volver();" Visible="False"></asp:Button>
            </div>
        </div>
        <div>
        </div>
    </div>

        <!-- Bootstrap Modal Dialog Crear/Modificar -->
    <div class="modal fade" id="myModal" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" data-keyboard="false" data-backdrop="static">
        <div class="modal-dialog" style="max-width: 50%;">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title">
                        <asp:Label ID="lblModalTitulo" runat="server" Text="Formulario - Maestro de usuarios" Font-Bold="true" Font-Size="X-Large">
                        </asp:Label>
                    </h4>
                        <Button ID="btnXCerrar" type="button" class="close" data-dismiss="modal" aria-label="Close" onclick="cerrarAlert(); return true;">
                            <span aria-hidden="true" onclick="cerrarAlert(); return true;">&times;</span>
                        </Button>
                </div>
                <div class="modal-body">
                    <div class="jumbotron">
                        <div class="p-4">
                            <!-- FORMULARIO -->
                                    <div class="row">
                                        <div class="col-md-5">
                                            <asp:Label runat="server" ID="lblModalNombreUsuario" Font-Size="Large">Nombre de usuario: </asp:Label>
                                        </div>
                                        <div class="col-md-7">                                            
                                            <asp:DropDownList ID="ddlModalNombreUsuario" CssClass="form-control form-control-sm" runat="server">
                                           </asp:DropDownList> 
                                        </div>
                                    </div>
                                    <div class="row mt-3">
                                        <div class="col-md-5">
                                            <asp:Label runat="server" ID="lblModalPerfil" Font-Size="Large">Perfil: </asp:Label>
                                        </div>
                                        <div class="col-md-7">
                                           <asp:DropDownList ID="ddlModalPerfil" CssClass="form-control form-control-sm " runat="server">
                                               <asp:ListItem Value="&nbsp;"></asp:ListItem>
                                                <asp:ListItem Value="1"> Perfil Consulta</asp:ListItem>
                                                <asp:ListItem Value="2"> Perfil Full</asp:ListItem>
                                                <asp:ListItem Value="3"> Perfil Administrador</asp:ListItem>
                                           </asp:DropDownList> 
                                        </div>
                                    </div>
                                    <div class="row mt-3">
                                        <div class="col-md-5">
                                            <asp:Label runat="server" ID="lblEstado" Font-Size="Large" Visible="False">Estado: </asp:Label>
                                        </div>
                                        <div class="col-md-7">
                                                    <asp:DropDownList ID="ddlModalEstado" CssClass="form-control form-control-sm" runat="server" Visible="False">
                                                    <asp:ListItem Value=0> Deshabilitado</asp:ListItem>
                                                    <asp:ListItem Value=1> Habilitado</asp:ListItem>
                                           </asp:DropDownList> 
                                        </div>
                                    </div>

                            <asp:HiddenField ID="txtAccionHidden" runat="server" />

                            <!-- GRUPO DE BOTONES 2 -->
                            <div class="form-group mt-5 text-right">
                                <div class="col-md-offset-1">
                                    <asp:Button ID="btnModalGuardarModificar" Text="Guardar" CssClass="btn btn-info" Width="15%" runat="server" OnClientClick="return validarCampos('¿Seguro que desea Guardar?');"></asp:Button>
                                    <asp:Button ID="btnModalGuardarCrear" Text="Guardar" CssClass="btn btn-info" Width="15%" runat="server" OnClientClick="return validarCampos('¿Seguro que desea Guardar?');"></asp:Button>
                                    <asp:Button ID="btnModalCancelar" Text="Cancelar" CssClass="btn btn-secondary" Width="15%" runat="server" OnClientClick="if (!confirm('¿Seguro que desea Cancelar?')) return false;"></asp:Button>
                                    <asp:Button ID="btnModalEliminar" Text="Eliminar" runat="server" class="btn btn-danger" Width="15%" OnClientClick="if (!confirm('¿Seguro que desea Eliminar los elementos seleccionados?')) return false;"></asp:Button>
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
                        <asp:Button ID="btnModal" runat="server" CssClass="btn-info btnNormal" Text="Cerrar" />
                    </div>
                </div>
            </div>
        </div>
        <!-- End Bootstrap Modal Dialog Mensajes-->
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="FooterScript" Runat="Server">
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
            for (var i = 1; i < gv.rows.length; i++) {
                var radioBtn = gv.rows[i].cells[0].getElementsByTagName("input");

                // Check if the id not same
                if (radioBtn[0].id != id.id) {
                    radioBtn[0].checked = false;
                }
                else {
                    enableDisableButtons(false)
                }
            }
        }

        function volver() {

            if (!confirm('¿Seguro que desea volver al menu principal?')) {
                return false;
            } else {
                return true;
            }
        }

        function cerrarAlert() {
            $('#<%=txtAccionHidden.ClientID %>').val("");
        }

        function validarCampos(texto) {
            var perfil = $('#<%=ddlModalPerfil.ClientID%>').val();
            var usuario = $('#<%=ddlModalNombreUsuario.ClientID%>').val();

            if ( perfil.trim() == "") {
                alert('Debe seleccionar un perfil')
                return false;
            }
            if (usuario.trim() == "") {
                alert('Debe seleccionar un nombre de usuario')
                return false;
            }

            if (!confirm(texto)) {
                return false;
            } else {
                return true;
            }
        }
    </script>
</asp:Content>

