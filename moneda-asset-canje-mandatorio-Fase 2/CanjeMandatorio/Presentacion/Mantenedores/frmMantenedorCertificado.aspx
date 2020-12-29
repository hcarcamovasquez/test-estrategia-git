<%@ Page Title="Certificados" Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false" CodeFile="frmMantenedorCertificado.aspx.vb" Inherits="Presentacion_Mantenedores_frmMantenedorCertificado" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    <asp:HiddenField ID="txtAccionHidden" runat="server" />
    <asp:TextBox ID="txtAccionModificar" Class="txtAccionModificar" runat="server" Visible="false" />
    <asp:HiddenField ID="filaseleccionada" runat="server" />

    <h2 class="TdRedondeado titleMant">Maestro de <strong>Certificados</strong></h2>

    <div class="card p-4 jumbotron">

        <div class="row ">
            <!-- RUT del Fondo-->
            <div class="col-md-4">
                <asp:Label runat="server" ID="lblRut">Fondo</asp:Label>
                <asp:DropDownList ID="ddlNombreFondoBuscar" CssClass="form-control js-select2-rut" runat="server" />
            </div>

            <!-- Nombre del Aportante-->
            <div class="col-md-4">
                <asp:Label runat="server" ID="Label4">Aportante</asp:Label>
                <asp:DropDownList ID="ddlNombreAportanteBuscar" CssClass="form-control js-select2-rut" runat="server" />
            </div>

            <!-- FECHA CORTE -->
            <div class="col-md-4">
                <asp:Label runat="server" ID="Label1">Fecha de Corte</asp:Label>
                <div class="input-group">
                    <asp:TextBox ID="txtFechaCorteBuscar" runat="server" CssClass="form-control datepicker" ReadOnly="True"></asp:TextBox>
                    <asp:LinkButton ID="lnkBtnFechaCorteBuscar" class="btn btn-moneda" runat="server" 
                        OnClientClick="return clickCalendar('txtFechaCorteBuscar')"><i class="far fa-calendar-alt"></i></asp:LinkButton>
                    <asp:LinkButton ID="BtnLimpiarFechaDesde" Text="" class="btn btn-secondary ml-1" runat="server"
                        OnClientClick="return limpiarCalendar('txtFechaCorteBuscar')"><i class="far fa-trash-alt"></i></asp:LinkButton>
                    
                    
                  <asp:Calendar ID="CalendarFechaCorteBuscar" runat="server" Visible="False" BackColor="White" BorderColor="White" BorderWidth="1px" Font-Names="Verdana" Font-Size="9pt" ForeColor="Black" Height="190px" NextPrevFormat="FullMonth" Width="350px" class="calendarios">
                        <DayHeaderStyle Font-Bold="True" Font-Size="8pt" />
                        <NextPrevStyle Font-Bold="True" Font-Size="8pt" ForeColor="#333333" VerticalAlign="Bottom" />
                        <OtherMonthDayStyle ForeColor="#999999" />
                        <SelectedDayStyle BackColor="#333399" ForeColor="White" />
                        <TitleStyle BackColor="White" BorderColor="Black" BorderWidth="4px" Font-Bold="True" Font-Size="12pt" ForeColor="#333399" />
                        <TodayDayStyle BackColor="#CCCCCC" />
                    </asp:Calendar>


                </div>
            </div>
        </div>

        <div class="row mt-4">
            <!-- FECHA INGRESO DESDE -->
            <div class="col-md-4">
                <asp:Label runat="server" ID="Label2">Fecha Ingreso desde</asp:Label>
                <div class="input-group">
                    <asp:TextBox ID="txtFechaIngresoDesdeBuscar" runat="server" CssClass="form-control datepicker" ReadOnly="True"></asp:TextBox>
                    <asp:LinkButton ID="lnkBtnFechaIngresoDesdeBuscar" class="btn btn-moneda" runat="server" 
                        OnClientClick="return clickCalendar('txtFechaIngresoDesdeBuscar')"><i class="far fa-calendar-alt"></i></asp:LinkButton>
                    <asp:LinkButton ID="LinkButton1" Text="" class="btn btn-secondary ml-1" runat="server"
                        OnClientClick="return limpiarCalendar('txtFechaIngresoDesdeBuscar')"><i class="far fa-trash-alt"></i></asp:LinkButton>

                   <asp:Calendar ID="CalendarFechaIngresoDesdeBuscar" runat="server" Visible="False" onblur="onblurCalendar(this)" BackColor="White" BorderColor="White" BorderWidth="1px" Font-Names="Verdana" Font-Size="9pt" ForeColor="Black" Height="190px" NextPrevFormat="FullMonth" Width="350px" class="calendarios">
                        <DayHeaderStyle Font-Bold="True" Font-Size="8pt" />
                        <NextPrevStyle Font-Bold="True" Font-Size="8pt" ForeColor="#333333" VerticalAlign="Bottom" />
                        <OtherMonthDayStyle ForeColor="#999999" />
                        <SelectedDayStyle BackColor="#333399" ForeColor="White" />
                        <TitleStyle BackColor="White" BorderColor="Black" BorderWidth="4px" Font-Bold="True" Font-Size="12pt" ForeColor="#333399" />
                        <TodayDayStyle BackColor="#CCCCCC" />
                    </asp:Calendar>
                </div>
            </div>

            <!-- FECHA INGRESO HASTA -->
            <div class="col-md-4">
                <asp:Label runat="server" ID="lblFechaHasta">Fecha Ingreso hasta</asp:Label>
                <div class="input-group">
                    <asp:TextBox ID="txtFechaIngresoHastaBuscar" runat="server" CssClass="form-control datepicker" ReadOnly="True"></asp:TextBox>

                    <asp:LinkButton ID="lnkBtnFechaIngresoHastaBuscar" class="btn btn-moneda" runat="server" 
                        OnClientClick="return clickCalendar('txtFechaIngresoHastaBuscar')"><i class="far fa-calendar-alt"></i></asp:LinkButton>

                    <asp:LinkButton ID="LinkButton2" Text="" class="btn btn-secondary ml-1" runat="server"
                        OnClientClick="return limpiarCalendar('txtFechaIngresoHastaBuscar')"><i class="far fa-trash-alt"></i></asp:LinkButton>

                    <asp:Calendar ID="CalendarFechaIngresoHastaBuscar" runat="server" Visible="False" BackColor="White" BorderColor="White" BorderWidth="1px" Font-Names="Verdana" Font-Size="9pt" ForeColor="Black" Height="190px" NextPrevFormat="FullMonth" Width="350px" class="calendarios">
                        <DayHeaderStyle Font-Bold="True" Font-Size="8pt" />
                        <NextPrevStyle Font-Bold="True" Font-Size="8pt" ForeColor="#333333" VerticalAlign="Bottom" />
                        <OtherMonthDayStyle ForeColor="#999999" />
                        <SelectedDayStyle BackColor="#333399" ForeColor="White" />
                        <TitleStyle BackColor="White" BorderColor="Black" BorderWidth="4px" Font-Bold="True" Font-Size="12pt" ForeColor="#333399" />
                        <TodayDayStyle BackColor="#CCCCCC" />
                    </asp:Calendar>
                </div>
            </div>

            <!-- FECHA CANJE -->
            <div class="col-md-4">
                <asp:Label runat="server" ID="Label3">Fecha Canje</asp:Label>
                <div class="input-group">
                    <asp:TextBox ID="txtFechaCanjeBuscar" runat="server" CssClass="form-control datepicker" ReadOnly="True"></asp:TextBox>
                    <asp:LinkButton ID="lnkBtnFechaCanjeBuscar" class="btn btn-moneda" runat="server" 
                        OnClientClick="return clickCalendar('txtFechaCanjeBuscar')"><i class="far fa-calendar-alt"></i></asp:LinkButton>

                    <asp:LinkButton ID="LinkButton3" Text="" class="btn btn-secondary ml-1" runat="server"
                        OnClientClick="return limpiarCalendar('txtFechaCanjeBuscar')"><i class="far fa-trash-alt"></i></asp:LinkButton>

                    <asp:Calendar ID="CalendarFechaCanjeBuscar" runat="server" Visible="False" BackColor="White" BorderColor="White" BorderWidth="1px" Font-Names="Verdana" Font-Size="9pt" ForeColor="Black" Height="190px" NextPrevFormat="FullMonth" Width="350px" class="calendarios">
                        <DayHeaderStyle Font-Bold="True" Font-Size="8pt" />
                        <NextPrevStyle Font-Bold="True" Font-Size="8pt" ForeColor="#333333" VerticalAlign="Bottom" />
                        <OtherMonthDayStyle ForeColor="#999999" />
                        <SelectedDayStyle BackColor="#333399" ForeColor="White" />
                        <TitleStyle BackColor="White" BorderColor="Black" BorderWidth="4px" Font-Bold="True" Font-Size="12pt" ForeColor="#333399" />
                        <TodayDayStyle BackColor="#CCCCCC" />
                    </asp:Calendar>
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
                    <asp:BoundField DataField="HT_ID" HeaderText="Id Hito" />
                    <asp:BoundField DataField="HT_Corte" HeaderText="Fecha Corte" DataFormatString="{0:dd-MM-yyyy}" />
                    <asp:BoundField DataField="HT_Canje" HeaderText="Fecha Canje" DataFormatString="{0:dd-MM-yyyy}" />
                    <asp:BoundField DataField="FN_RUT" HeaderText="Rut Fondo" Visible="true" />
                    <asp:BoundField DataField="FN_Nombre_Corto" HeaderText="Nombre Fondo" Visible="true" />
                    <asp:BoundField DataField="FS_Nemotecnico" HeaderText="Nemotécnico" Visible="true" />
                    <asp:BoundField DataField="AP_RUT" HeaderText="Rut Aportante" Visible="true" />
                    <asp:BoundField DataField="AP_Razon_Social" HeaderText="Nombre Aportante" Visible="true" />
                    <asp:BoundField DataField="AP_Multifondo" HeaderText="Multifondo" Visible="true" />
                    <asp:BoundField DataField="CT_Cuotas" HeaderText="Cantidad" Visible="true" DataFormatString="{0:N0}" ItemStyle-HorizontalAlign="Right"/>
                    <asp:BoundField DataField="CT_Numero" HeaderText="Número Documento" DataFormatString="{0:N0}" ItemStyle-HorizontalAlign="Right" />
                    <asp:BoundField DataField="CT_Correlativo" HeaderText="Correlativo" DataFormatString="{0:N0}" ItemStyle-HorizontalAlign="Right" />
                    <asp:BoundField DataField="CT_Fecha_Ingreso" HeaderText="Fecha Ingreso" Visible="true" DataFormatString="{0:dd-MM-yyyy}" />
                    <asp:BoundField DataField="CT_Usuario_Ingreso" HeaderText="Usuario Ingreso" Visible="true" />
                    <asp:BoundField DataField="CT_Fecha_Modificacion" HeaderText="Fecha Modificación" Visible="true" DataFormatString="{0:dd-MM-yyyy}" />
                    <asp:BoundField DataField="CT_Usuario_Modificacion" HeaderText="Usuario Modificador" Visible="true" />
                    <asp:BoundField DataField="CT_Estado" HeaderText="Estado" Visible="false" />

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
    </div>


    <!-- Bootstrap Modal Dialog Crear/Modificar -->
    <div class="modal fade" id="myModal" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" data-keyboard="false" data-backdrop="static">
        <div class="modal-dialog" style="max-width: 90%;">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title">
                        <asp:Label ID="Label5" runat="server" Text="Formulario - Certificados" Font-Bold="true" Font-Size="X-Large">
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
                                    <h4 class="modal-title">
                                        <asp:Label ID="lblTitleModalCrud" runat="server" Text="Modificar o eliminar" Font-Bold="true" Font-Size="X-Large" />
                                    </h4>
                                    <hr />
                                    <br />
                                    <asp:HiddenField ID="txtHiddenOldValue" runat="server"></asp:HiddenField>
                                    <asp:TextBox ID="txtModalNombreGrupo" onblur="IsAccTextValid(this)" onkeyup="IsAccTextValid(this)" runat="server" MaxLength="100" CssClass="form-control" onkeypress="return soloLetras(event)" Visible="false"></asp:TextBox>
                                    <!-- FORMULARIO -->
                                    <div class="row mt-3">
                                        <div class="col-md-6">
                                            <asp:Label runat="server" ID="lblIdHito">ID Hito</asp:Label>
                                        </div>
                                        <div class="col-md-6">
                                            <asp:UpdatePanel runat="server" ID="UpdateHito" UpdateMode="Conditional">
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="ddlModalRutFondo" EventName="SelectedIndexChanged" />
                                                    <asp:AsyncPostBackTrigger ControlID="ddlModalNemotecnico" EventName="SelectedIndexChanged" />
                                                    <asp:AsyncPostBackTrigger ControlID="ddlModalNombreFondo" EventName="SelectedIndexChanged" />
                                                </Triggers>
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="ddlModalIdHito" CssClass="form-control js-select2-rut" runat="server" OnSelectedIndexChanged="CargaFechasHitosPorIDHitoModal" AutoPostBack="true" />
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                    <div class="row mt-3">
                                        <div class="col-md-6">
                                            <label class="form-control-label">Fecha de Corte</label>
                                        </div>
                                        <div class="col-md-6">
                                            <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Conditional">
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="ddlModalIdHito" EventName="SelectedIndexChanged" />
                                                    <asp:AsyncPostBackTrigger ControlID="ddlModalRutFondo" EventName="SelectedIndexChanged" />
                                                    <asp:AsyncPostBackTrigger ControlID="ddlModalNemotecnico" EventName="SelectedIndexChanged" />
                                                    <asp:AsyncPostBackTrigger ControlID="ddlModalNombreFondo" EventName="SelectedIndexChanged" />
                                                </Triggers>
                                                <ContentTemplate>
                                                    <asp:TextBox ID="txtModalFechaCorte" runat="server" CssClass="form-control datepicker" ReadOnly="True"></asp:TextBox>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>

                                    <div class="row mt-3">
                                        <div class="col-md-6">
                                            <label class="form-control-label">Fecha de Canje</label>
                                        </div>
                                        <div class="col-md-6">
                                            <asp:UpdatePanel runat="server" ID="UpdatePanel2" UpdateMode="Conditional">
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="ddlModalIdHito" EventName="SelectedIndexChanged" />
                                                    <asp:AsyncPostBackTrigger ControlID="ddlModalRutFondo" EventName="SelectedIndexChanged" />
                                                    <asp:AsyncPostBackTrigger ControlID="ddlModalNemotecnico" EventName="SelectedIndexChanged" />
                                                    <asp:AsyncPostBackTrigger ControlID="ddlModalNombreFondo" EventName="SelectedIndexChanged" />
                                                </Triggers>
                                                <ContentTemplate>
                                                    <asp:TextBox ID="txtModalFechaCanje" runat="server" CssClass="form-control datepicker" ReadOnly="True"></asp:TextBox>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                    <div class="row mt-3">
                                        <div class="col-md-6">
                                            <label class="form-control-label">Número de Documento</label>
                                        </div>
                                        <div class="col-md-6">
                                            <asp:TextBox ID="txtModalNumeroDocumento" runat="server" onblur="IsAccTextValid(this)" onkeyup="IsAccTextValid(this)" CssClass="form-control datepicker" ReadOnly="True"></asp:TextBox>
                                        </div>
                                    </div>


                                    <div class="row mt-3">
                                        <div class="col-md-6">
                                            <label class="form-control-label">Rut del Fondo</label>
                                        </div>
                                        <div class="col-md-6">
                                            <asp:UpdatePanel runat="server" ID="UpdatePanel5" UpdateMode="Conditional">
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="ddlModalNombreFondo" EventName="SelectedIndexChanged" />
                                                    <asp:AsyncPostBackTrigger ControlID="ddlModalNemotecnico" EventName="SelectedIndexChanged" />
                                                    <asp:AsyncPostBackTrigger ControlID="btnModalAgregar" EventName="Click" />
                                                </Triggers>
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="ddlModalRutFondo" CssClass="form-control js-select2-rut" runat="server" OnSelectedIndexChanged="CargarNombreFondoPorRutFondoModal" AutoPostBack="true" />
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>

                                    <div class="row mt-3">
                                        <div class="col-md-6">
                                            <label class="form-control-label">Nombre Fondo</label>
                                        </div>
                                        <div class="col-md-6">
                                            <asp:UpdatePanel runat="server" ID="UpdatePanel3" UpdateMode="Conditional">
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="ddlModalRutFondo" EventName="SelectedIndexChanged" />
                                                    <asp:AsyncPostBackTrigger ControlID="ddlModalNemotecnico" EventName="SelectedIndexChanged" />
                                                    <asp:AsyncPostBackTrigger ControlID="btnModalAgregar" EventName="Click" />
                                                </Triggers>
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="ddlModalNombreFondo" CssClass="form-control js-select2-rut" runat="server" OnSelectedIndexChanged="CargarRutFondoPorNombreFondoModal" AutoPostBack="true" />
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
                                                    <asp:AsyncPostBackTrigger ControlID="ddlModalRutFondo" EventName="SelectedIndexChanged" />
                                                    <asp:AsyncPostBackTrigger ControlID="ddlModalNombreFondo" EventName="SelectedIndexChanged" />
                                                    <asp:AsyncPostBackTrigger ControlID="btnModalAgregar" EventName="Click" />
                                                </Triggers>
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="ddlModalNemotecnico" CssClass="form-control js-select2-rut" runat="server" OnSelectedIndexChanged="CargarNemotecnicoPorRutFondoModal" AutoPostBack="true" />
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                    <div class="row mt-3">
                                        <div class="col-md-6">
                                            <label class="form-control-label">Rut del Aportante</label>
                                        </div>
                                        <div class="col-md-6">
                                            <asp:UpdatePanel runat="server" ID="UpdatePanel6" UpdateMode="Conditional">
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="ddlModalNombreAportante" EventName="SelectedIndexChanged" />
                                                    <asp:AsyncPostBackTrigger ControlID="ddlModalMultifondo" EventName="SelectedIndexChanged" />
                                                </Triggers>
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="ddlModalRutAportante" CssClass="form-control js-select2-rut" runat="server" OnSelectedIndexChanged="CargarNombreAportantePorRutAportanteModal" AutoPostBack="true" />
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>

                                    <div class="row mt-3">
                                        <div class="col-md-6">
                                            <label class="form-control-label">Nombre del Aportante</label>
                                        </div>
                                        <div class="col-md-6">
                                            <asp:UpdatePanel runat="server" ID="UpdatePanel4" UpdateMode="Conditional">
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="ddlModalRutAportante" EventName="SelectedIndexChanged" />
                                                    <asp:AsyncPostBackTrigger ControlID="ddlModalMultifondo" EventName="SelectedIndexChanged" />
                                                </Triggers>
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="ddlModalNombreAportante" CssClass="form-control js-select2-rut" runat="server" OnSelectedIndexChanged="CargarMultifondoPorRutAportanteModal" AutoPostBack="true" />
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                    <div class="row mt-3">
                                        <div class="col-md-6">
                                            <label class="form-control-label">Multifondo</label>
                                        </div>
                                        <div class="col-md-6">
                                            <asp:UpdatePanel runat="server" ID="UpdatePanel7" UpdateMode="Conditional">
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="ddlModalRutAportante" EventName="SelectedIndexChanged" />
                                                    <asp:AsyncPostBackTrigger ControlID="ddlModalNombreAportante" EventName="SelectedIndexChanged" />
                                                </Triggers>
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="ddlModalMultifondo" CssClass="form-control js-select2-rut" OnSelectedIndexChanged="CargarRutAportantePorNombreAportanteModal" AutoPostBack="true" runat="server" />
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>

                                    <div class="row mt-3">
                                        <div class="col-md-6">
                                            <label class="form-control-label">Cantidad</label>
                                        </div>
                                        <div class="col-md-6">
                                            <asp:TextBox ID="txtModalCantidad" onkeypress="return soloNumeros(event)" onpaste="return false" oncut="return false" oncopy="return false" MaxLength="26" runat="server" CssClass="form-control form-control-sm dbs-entero-decimal" onblur="ValidarBotonAgregar(this)" onkeyup="ValidarBotonAgregar(this)"></asp:TextBox>

                                        </div>
                                        <asp:TextBox ID="txtModalVariableDocumento" runat="server" Visible="false"></asp:TextBox>
                                        <asp:TextBox ID="txtModalVariableCorrelativo" runat="server" Visible="false"></asp:TextBox>
                                        <asp:TextBox ID="txtModalVariableFecha" runat="server" Visible="false"></asp:TextBox>
                                        <asp:TextBox ID="txtModalVariableUsuarioIngreso" runat="server" Visible="false"></asp:TextBox>
                                        <asp:TextBox ID="txtModalVariableFechaIngreso" runat="server" Visible="false"></asp:TextBox>
                                        <asp:TextBox ID="txtModalVariableEstado" runat="server" Visible="false"></asp:TextBox>
                                    </div>
                                    <!-- FIN FORMULARIO -->
                                    <asp:HiddenField ID="txtEstadoCambio" runat="server"></asp:HiddenField>
                                    <asp:HiddenField ID="txtControlesHidden" runat="server" />
                                    <!-- GRUPO DE BOTONES M-->
                                    <div class="form-group mt-5">
                                        <div class="col-md-offset-1">

                                            <asp:Button ID="btnModalAgregar" Text="Agregar" CssClass="btn btn-info btnModalAgregar" runat="server" Enabled="false"></asp:Button>

                                            <asp:Button ID="btnModalModificar" Text="Modificar" CssClass="btn btn-info btnModalModificar" runat="server" OnClientClick="return validateBtn();"></asp:Button>
                                            <asp:Button ID="btnModalEliminarCertificado" Text="Eliminar R." runat="server" class="btn btn-danger btnModalEliminarCertificado"></asp:Button>
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

                                                            <asp:RadioButton ID="RowSelector" GroupName="a" runat="server" onclick="checkRadioBtn2(this);" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="CT_Numero" HeaderText="Número Documento" />
                                                    <asp:BoundField DataField="CT_Correlativo" HeaderText="Correlativo" DataFormatString="{0:N0}" ItemStyle-HorizontalAlign="Right"/>
                                                    <asp:BoundField DataField="HT_ID" HeaderText="Id Hito" DataFormatString="{0:N0}" ItemStyle-HorizontalAlign="Right"/>
                                                    <asp:BoundField DataField="HT_Corte" HeaderText="Fecha Corte" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol" DataFormatString="{0:dd/MM/yyyy}" />
                                                    <asp:BoundField DataField="HT_Canje" HeaderText="Fecha Canje" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol" DataFormatString="{0:dd/MM/yyyy}" />
                                                    <asp:BoundField DataField="CT_Fecha" HeaderText="Fecha" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol" DataFormatString="{0:dd/MM/yyyy}" />
                                                    <asp:BoundField DataField="AP_RUT" HeaderText="Rut Aportante" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol" />
                                                    <asp:BoundField DataField="AP_Razon_Social" HeaderText="Nombre Aportante" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol" />
                                                    <asp:BoundField DataField="FN_RUT" HeaderText="Rut Fondo" Visible="true" />
                                                    <asp:BoundField DataField="FN_Nombre_Corto" HeaderText="Nombre Fondo" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol" />
                                                    <asp:BoundField DataField="AP_Multifondo" HeaderText="Multifondo" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol" />
                                                    <asp:BoundField DataField="FS_Nemotecnico" HeaderText="Nemotécnico" />
                                                    <asp:BoundField DataField="CT_Cuotas" HeaderText="Cantidad" Visible="true" DataFormatString="{0:N0}" ItemStyle-HorizontalAlign="Right"/>
                                                    <asp:BoundField DataField="CT_Fecha_Ingreso" HeaderText="Fecha Ingreso" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol" DataFormatString="{0:dd/MM/yyyy}" />
                                                    <asp:BoundField DataField="CT_Usuario_Ingreso" HeaderText="Usuario Ingreso" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol"/>

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
                                    <asp:Button ID="btnModalEliminarGrupo" Text="Eliminar C." runat="server" class="btn btn-danger" OnClientClick="if (!confirm('¿Seguro que desea Eliminar los elementos seleccionados?')) return false;"></asp:Button>

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
  .hiddencol
  {
    display: none;
  }

</style>

    <script>
        $(document).ready(function () {
            //checkGrilla();

            $(".table-bordered").css("width", "100%");

            var txtAccionHidden = $("#<%=txtAccionHidden.ClientID %>").val();
            if ((txtAccionHidden == "MODIFICAR") || (txtAccionHidden == "ELIMINAR") || (txtAccionHidden == "CREAR")) {
                $('#myModal').modal('show');
            }


            $("body").on("click", "#btnXCerrar", function () {
                $("#btnXCerrar").val("");
            });

            $("[id*=txtFechaCorteBuscar]").datepicker();
            $("[id*=txtFechaIngresoDesdeBuscar]").datepicker();
            $("[id*=txtFechaCanjeBuscar]").datepicker();
            $("[id*=txtFechaIngresoHastaBuscar]").datepicker();

            $("[id*=txtFechaCanjeBuscar]").change(function () {
                var fd = $("[id*=txtFechaCanjeBuscar]").datepicker("getDate");
                var fh = $("[id*=txtFechaCorteBuscar]").datepicker("getDate");
                if (Date.parse(fd) < Date.parse(fh)) {
                    $("[id*=txtFechaCorteBuscar]").datepicker("setDate", fd);
                }
            });

            $("[id*=txtFechaCorteBuscar]").change(function () {
                var fd = $("[id*=txtFechaCanjeBuscar]").datepicker("getDate");
                var fh = $("[id*=txtFechaCorteBuscar]").datepicker("getDate");
                if (Date.parse(fd) < Date.parse(fh)) {
                    $("[id*=txtFechaCanjeBuscar]").datepicker("setDate", fh);
                }
                
            });

            $("[id*=txtFechaIngresoDesdeBuscar]").change(function () {
                var fd = $("[id*=txtFechaIngresoDesdeBuscar]").datepicker("getDate");
                var fh = $("[id*=txtFechaIngresoHastaBuscar]").datepicker("getDate");

                if (Date.parse(fh) < Date.parse(fd)) {
                    $("[id*=txtFechaIngresoHastaBuscar]").datepicker("setDate", fd);
                }

            });

            $("[id*=txtFechaIngresoHastaBuscar]").change(function () {
                var fd = $("[id*=txtFechaIngresoDesdeBuscar]").datepicker("getDate");
                var fh = $("[id*=txtFechaIngresoHastaBuscar]").datepicker("getDate");

                if (Date.parse(fh) < Date.parse(fd)) {
                    $("[id*=txtFechaIngresoDesdeBuscar]").datepicker("setDate", fh);
                }

            });

          <%--  $("#<%=ddlModalNemotecnico.ClientID%>").change(function () {
                var txtAccionHidden = $("#<%=txtAccionHidden.ClientID %>").val();
                if (txtAccionHidden == "CREAR") {
                    return true;
                }
                if (txtAccionHidden == "MODIFICAR") {
                    return false;
                }
                return false;
            });--%>

            seteaBotonGuardar();
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(bindDataTable);

            confNumeros();
        });


                function confNumeros() {

                        $('.dbs-entero-decimal').mask2(getMask(26,0));            
                    }

        function seteaBotonGuardar() {
            $("#<%=btnModalGuardar.ClientID %>").unbind("click");
            $("#<%=btnModalGuardar.ClientID %>").click(function () {
                if (!confirm('¿Seguro que desea Guardar?')) {
                    return false;
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

        function validateBtn() {
            if (!confirm('¿Seguro que desea Guardar?')) {
                return false;
            } else {
                return true;
            }
        }
        /*destroy jc*/
          
        $('.btnModalAgregar').on('click', function () {
                var table = $('.dataTable').DataTable(); 
                table.destroy();
            });

        //On UpdatePanel Refresh
        function bindDataTable() {
             var gv = $('.dataTable').DataTable();
            if (gv != null) {
                gv.destroy();
            }
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
            var ddlModalIdHito = document.getElementById('<%=ddlModalIdHito.ClientID%>');
            var ddlModalRutFondo = document.getElementById('<%=ddlModalRutFondo.ClientID%>');
            var ddlModalNombreFondo = document.getElementById('<%=ddlModalNombreFondo.ClientID%>');
            var ddlModalNemotecnico = document.getElementById('<%=ddlModalNemotecnico.ClientID%>');
            var ddlModalRutAportante = document.getElementById('<%=ddlModalRutAportante.ClientID%>');
            var ddlModalNombreAportante = document.getElementById('<%=ddlModalNombreAportante.ClientID%>');
            var ddlModalMultifondo = document.getElementById('<%=ddlModalMultifondo.ClientID%>');
            var txtModalCantidad = document.getElementById('<%=txtModalCantidad.ClientID%>');

            var btnModalEliminarGrupo = document.getElementById('<%=btnModalEliminarGrupo.ClientID%>');
            var btnModalGuardar = document.getElementById('<%=btnModalGuardar.ClientID%>');
            var btnModalAgregar = document.getElementById('<%=btnModalAgregar.ClientID%>');
            var btnModificar = document.getElementById('<%=btnModalModificar.ClientID%>');
            var btnModalEliminarCertificado = document.getElementById('<%=btnModalEliminarCertificado.ClientID%>');

            var txtAccionHidden = $("#<%=txtAccionHidden.ClientID %>").val();



            if (btnModalEliminarGrupo.disabled) {
                btnModificar.disabled = newValue;
                btnModalEliminarCertificado.disabled = newValue;
            }
            else {
                btnModalEliminarCertificado.disabled = newValue;
            }

            if (btnModificar.disabled == false) {
                btnModalGuardar.disabled = false;
            }

            if (btnModalAgregar.disabled == false) {
                btnModalGuardar.disabled = false;
            }



            if (btnModalGuardar.disabled == false) {
                ddlModalIdHito.disabled = true;
                ddlModalRutFondo.disabled = true;
                ddlModalNombreFondo.disabled = true;
                ddlModalRutAportante.disabled = true;
                ddlModalNombreAportante.disabled = true;
                ddlModalMultifondo.disabled = true;
                if (txtAccionHidden == "CREAR") {
                    txtModalCantidad.value = "";
                }
            }
        }

        $(".btnModalModificar").click(function () {
            if (!confirm('¿Seguro que desea Modificar los elementos seleccionados?')) {
                return false;
            } else {
                var txtAccionHidden = $("#<%=txtAccionHidden.ClientID %>").val();

                if (txtAccionHidden == "MODIFICAR") {

                    var btnModificar = document.getElementById('<%=btnModalModificar.ClientID%>');

                    btnModificar.disabled = false;
                }
                return true;
            }
        });


        $(".btnModalEliminarCertificado").click(function () {
            if (!confirm('¿Seguro que desea Eliminar los elementos seleccionados?')) {
                return false;
            } else {
                var txtAccionHidden = $("#<%=txtAccionHidden.ClientID %>").val();

                if (txtAccionHidden == "ELIMINAR") {

                    var btnModalAgregar = document.getElementById('<%=btnModalAgregar.ClientID%>');
                        var btnModificar = document.getElementById('<%=btnModalModificar.ClientID%>');
                        var btnModalEliminarCertificado = document.getElementById('<%=btnModalEliminarCertificado.ClientID%>');
                        var btnModalGuardar = document.getElementById('<%=btnModalGuardar.ClientID%>');
                        var btnModalEliminarGrupo = document.getElementById('<%=btnModalEliminarGrupo.ClientID%>');

                    btnModalAgregar.disabled = true;
                    btnModificar.disabled = true;
                    btnModalEliminarCertificado.disabled = false;
                    btnModalGuardar.disabled = false;
                    btnModalEliminarGrupo.disabled = false;
                }


                return true;
            }
        });


        function ValidarBotonAgregar(txtModalCantidad) {

            var btnModalAgregar = document.getElementById('<%=btnModalAgregar.ClientID%>');
            var ddlModalIdHito = document.getElementById('<%=ddlModalIdHito.ClientID%>');
            var SelValddlIdHito = ddlModalIdHito.options[ddlModalIdHito.selectedIndex].index;

            var txtModalFechaCorte = document.getElementById('<%=txtModalFechaCorte.ClientID%>');

            var txtModalFechaCanje = document.getElementById('<%=txtModalFechaCanje.ClientID%>');

            var txtModalNumeroDocumento = document.getElementById('<%=txtModalNumeroDocumento.ClientID%>');

            var ddlModalRutFondo = document.getElementById('<%=ddlModalRutFondo.ClientID%>');
            var SelValddlModalRutFondo = ddlModalRutFondo.options[ddlModalRutFondo.selectedIndex].index;

            var ddlModalNombreFondo = document.getElementById('<%=ddlModalNombreFondo.ClientID%>');
            var SelValddlModalNombreFondo = ddlModalNombreFondo.options[ddlModalNombreFondo.selectedIndex].index;

            var ddlModalNemotecnico = document.getElementById('<%=ddlModalNemotecnico.ClientID%>');
            var SelValddlModalNemotecnico = ddlModalNemotecnico.options[ddlModalNemotecnico.selectedIndex].index;

            var ddlModalRutAportante = document.getElementById('<%=ddlModalRutAportante.ClientID%>');
            var SelValddlModalRutAportante = ddlModalRutAportante.options[ddlModalRutAportante.selectedIndex].index;

            var ddlModalNombreAportante = document.getElementById('<%=ddlModalNombreAportante.ClientID%>');
            var SelValddlModalNombreAportante = ddlModalNombreAportante.options[ddlModalNombreAportante.selectedIndex].index;

            if (SelValddlIdHito == 0) {
                btnModalAgregar.disabled = true;
            }
            else {
                btnModalAgregar.disabled = false;
            }

            if (ddlModalIdHito.value.length == 0) {
                btnModalAgregar.disabled = true;
            }
            else {
                btnModalAgregar.disabled = false;
            }

            if (txtModalFechaCorte.value.length == 0) {
                btnModalAgregar.disabled = true;
            }
            else {
                btnModalAgregar.disabled = false;
            }

            if (txtModalFechaCanje.value.length == 0) {
                btnModalAgregar.disabled = true;
            }
            else {
                btnModalAgregar.disabled = false;
            }

            if (txtModalNumeroDocumento.value.length == 0) {
                btnModalAgregar.disabled = true;
            }
            else {
                btnModalAgregar.disabled = false;
            }

            if (SelValddlModalRutFondo == 0) {
                btnModalAgregar.disabled = true;
            }
            else {
                btnModalAgregar.disabled = false;
            }

            if (ddlModalRutFondo.value.length == 0) {
                btnModalAgregar.disabled = true;
            }
            else {
                btnModalAgregar.disabled = false;
            }

            if (SelValddlModalNombreFondo == 0) {
                btnModalAgregar.disabled = true;
            }
            else {
                btnModalAgregar.disabled = false;
            }

            if (ddlModalNombreFondo.value.length == 0) {
                btnModalAgregar.disabled = true;
            }
            else {
                btnModalAgregar.disabled = false;
            }

            if (SelValddlModalNemotecnico == 0) {
                btnModalAgregar.disabled = true;
            }
            else {
                btnModalAgregar.disabled = false;
            }

            if (ddlModalNemotecnico.value.length == 0) {
                btnModalAgregar.disabled = true;
            }
            else {
                btnModalAgregar.disabled = false;
            }

            if (SelValddlModalRutAportante == 0) {
                btnModalAgregar.disabled = true;
            }
            else {
                btnModalAgregar.disabled = false;
            }

            if (ddlModalRutAportante.value.length == 0) {
                btnModalAgregar.disabled = true;
            }
            else {
                btnModalAgregar.disabled = false;
            }

            if (SelValddlModalNombreAportante == 0) {
                btnModalAgregar.disabled = true;
            }
            else {
                btnModalAgregar.disabled = false;
            }

            if (ddlModalNombreAportante.value.length == 0) {
                btnModalAgregar.disabled = true;
            }
            else {
                btnModalAgregar.disabled = false;
            }

            if (txtModalCantidad.value.length == 0) {
                btnModalAgregar.disabled = true;
            }
            else {
                btnModalAgregar.disabled = false;
            }

            //btnModalAgregar.disabled = false;
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

