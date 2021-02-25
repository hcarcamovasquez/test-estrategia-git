<%@ Page Title="" Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false" CodeFile="frmMantenedorRescates.aspx.vb" Inherits="Presentacion_Mantenedores_frmMantenedorRescates" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    <h2 class="TdRedondeado titleMant">Maestro de <strong>Rescates</strong></h2>


    <div class="card p-4 jumbotron">

        <div class="row">
            <!-- APORTANTE -->
            <div class="col-md-3">
                <asp:Label runat="server" ID="rutfondo">Aportante</asp:Label>
                <asp:DropDownList ID="ddlAportanteBuscar" CssClass="form-control js-select2-rut" runat="server" AutoPostBack="True" />
            </div>

            <!-- LISTA NOMBRE FONDOS -->
            <div class="col-md-3">
                <asp:Label runat="server" ID="lbnombreFondo">Fondo</asp:Label>
                <asp:DropDownList ID="ddlNombreFondoBuscar" CssClass="form-control js-select2-rut" runat="server" AutoPostBack="True" />
            </div>

            <!-- LISTA NEMOTECNICOS -->
            <div class="col-md-3">
                <asp:Label runat="server" ID="Label1">Nemotécnico</asp:Label>
                <asp:DropDownList ID="ddlNemotecnicoBuscar" CssClass="form-control js-select2-rut" runat="server" AutoPostBack="True" />
            </div>

            <!-- LISTA ESTADOS -->
            <div class="col-md-3">
                <asp:Label runat="server" ID="Label4">Estado</asp:Label>
                <asp:DropDownList ID="ddlEstadoBuscar" CssClass="form-control js-select2-rut" runat="server">
                    <asp:ListItem Value="                                                  ">                                                  </asp:ListItem>
                    <asp:ListItem Value="Pendiente">PENDIENTE</asp:ListItem>
                    <asp:ListItem Value="Cerrado">CERRADO</asp:ListItem>
                </asp:DropDownList>
            </div>
        </div>


        <div class="row mt-5">
            <!-- FECHA DESDE -->
            <div class="col-md-3">
                <asp:Label runat="server" ID="Label2">Fecha Solicitud Desde</asp:Label>
                <div class="input-group">
                    <asp:TextBox ID="txtFechaSolicitudDesde" runat="server" CssClass="form-control datepicker" ReadOnly="True"></asp:TextBox>
                    <asp:LinkButton ID="lnkBtnFechaSolicitudDesde" class="btn btn-moneda" runat="server"
                        OnClientClick="return clickCalendar('txtFechaSolicitudDesde')"><i class="far fa-calendar-alt"></i></asp:LinkButton>

                    <asp:LinkButton ID="lnkBtnFechaBorrarSolicitudDesde" Text="" class="btn btn-secondary ml-1" OnClientClick="return limpiarCalendar('txtFechaSolicitudDesde')" runat="server"><i class="far fa-trash-alt"></i></asp:LinkButton>

                </div>
            </div>

            <!-- FECHA HASTA -->
            <div class="col-md-3">
                <asp:Label runat="server" ID="lblFechaHasta">Fecha Solicitud Hasta</asp:Label>
                <div class="input-group">
                    <asp:TextBox ID="txtFechaSolicitudHasta" runat="server" CssClass="form-control" ReadOnly="True"></asp:TextBox>
                    <asp:LinkButton ID="lnkBtnFechaSolicitudHasta" class="btn btn-moneda" runat="server"
                        OnClientClick="return clickCalendar('txtFechaSolicitudHasta')"><i class="far fa-calendar-alt"></i></asp:LinkButton>
                    <asp:LinkButton ID="BtnLimpiarFechaDesde" Text="" class="btn btn-secondary ml-1"
                        OnClientClick="return limpiarCalendar('txtFechaSolicitudHasta')" runat="server"><i class="far fa-trash-alt"></i></asp:LinkButton>

                </div>
            </div>

            <!-- FECHA NAV DESDE -->
            <div class="col-md-3">
                <asp:Label runat="server" ID="Label3">Fecha NAV Desde</asp:Label>
                <div class="input-group">
                    <asp:TextBox ID="txtFechaNAVDesde" runat="server" CssClass="form-control" ReadOnly="True"></asp:TextBox>
                    <asp:LinkButton ID="lnkBtnFechaNAVDesde" class="btn btn-moneda" runat="server"
                        OnClientClick="return clickCalendar('txtFechaNAVDesde')"><i class="far fa-calendar-alt"></i></asp:LinkButton>
                    <asp:LinkButton ID="lnkBtnFechaBorrarNAVDesde" Text="" class="btn btn-secondary ml-1" OnClientClick="return limpiarCalendar('txtFechaNAVDesde')" runat="server"><i class="far fa-trash-alt"></i></asp:LinkButton>

                </div>
            </div>

            <!-- FECHA NAV HASTA -->
            <div class="col-md-3">
                <asp:Label runat="server" ID="Label5">Fecha NAV Hasta</asp:Label>
                <div class="input-group">
                    <asp:TextBox ID="txtFechaNAVHasta" runat="server" CssClass="form-control" ReadOnly="True"></asp:TextBox>
                    <asp:LinkButton ID="lnkBtnFechaNAVHasta" class="btn btn-moneda" runat="server"
                        OnClientClick="return clickCalendar('txtFechaNAVHasta')"><i class="far fa-calendar-alt"></i></asp:LinkButton>
                    <asp:LinkButton ID="lnkBtnFechaBorrarNAVHasta" Text="" class="btn btn-secondary ml-1" OnClientClick="return limpiarCalendar('txtFechaNAVHasta')" runat="server"><i class="far fa-trash-alt"></i></asp:LinkButton>

                </div>
            </div>
        </div>

        <div class="row mt-5">
            <!-- FECHA PAGO DESDE -->
            <div class="col-md-3">
                <asp:Label runat="server" ID="Label6">Fecha Pago Desde</asp:Label>
                <div class="input-group">
                    <asp:TextBox ID="txtFechaPagoDesde" runat="server" CssClass="form-control" ReadOnly="True"></asp:TextBox>
                    <asp:LinkButton ID="lnkBtnFechaPagoDesde" class="btn btn-moneda" runat="server"
                        OnClientClick="return clickCalendar('txtFechaPagoDesde')"><i class="far fa-calendar-alt"></i></asp:LinkButton>
                    <asp:LinkButton ID="lnkBtnFechaBorrarPagoDesde" Text="" class="btn btn-secondary ml-1" OnClientClick="return limpiarCalendar('txtFechaPagoDesde')" runat="server"><i class="far fa-trash-alt"></i></asp:LinkButton>

                </div>
            </div>

            <!-- FECHA PAGO HASTA -->
            <div class="col-md-3">
                <asp:Label runat="server" ID="Label7">Fecha Pago Hasta</asp:Label>
                <div class="input-group">
                    <asp:TextBox ID="txtFechaPagoHasta" runat="server" CssClass="form-control" ReadOnly="True"></asp:TextBox>
                    <asp:LinkButton ID="lnlBtnFechaPagoHasta" class="btn btn-moneda" runat="server"
                        OnClientClick="return clickCalendar('txtFechaPagoHasta')"><i class="far fa-calendar-alt"></i></asp:LinkButton>
                    <asp:LinkButton ID="lnlBtnFechaBorrarPagoHasta" Text="" class="btn btn-secondary ml-1" OnClientClick="return limpiarCalendar('txtFechaPagoHasta')" runat="server"><i class="far fa-trash-alt"></i></asp:LinkButton>

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
                <asp:Button ID="btnCrear" Text="Crear" class="btn btn-info" runat="server" />
                <asp:Button ID="btnProrrotear" Text="Prorratear" class="btn btn-info" runat="server" />
            </div>
        </div>


        <asp:HiddenField ID="txtAccionHidden" runat="server" />

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
                AutoGenerateColumns="false"
                AllowSorting="false">
                <Columns>
                    <asp:TemplateField HeaderText="Sel.">
                        <HeaderTemplate>
                            Sel.
                           <asp:CheckBox ID="checkFijacionAll" CssClass="checkFijacionAll" runat="server" />
                        </HeaderTemplate>

                        <ItemTemplate>
                            <%--<asp:RadioButton ID="RowSelector" runat="server" onclick="checkRadioBtn(this);" GroupName="a" AutoPostBack="false" />--%>
                            <asp:CheckBox ID="RowSelector" CssClass="checkFijacion" runat="server" onclick="checkRadioBtn(this);" />

                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="RES_ID" HeaderText="ID" />
                    <asp:BoundField DataField="RES_Tipo_Transaccion" HeaderText="Tipo Transacción" />
                    <asp:BoundField DataField="RES_Fecha_Solicitud" HeaderText="Fecha Solicitud" DataFormatString="{0:dd-MM-yyyy}" />
                    <asp:BoundField DataField="AP_RUT" HeaderText="RUT Aportante" />
                    <asp:BoundField DataField="AP_Razon_Social" HeaderText="Razón Social" />
                    <asp:BoundField DataField="AP_Multifondo" HeaderText="Multifondo" />
                    <asp:BoundField DataField="FN_RUT" HeaderText="RUT Fondo" />
                    <asp:BoundField DataField="FN_Nombre_Corto" HeaderText="Nombre Fondo" />
                    <asp:BoundField DataField="FS_Nemotecnico" HeaderText="Nemotécnico" />
                    <asp:BoundField DataField="FS_Nombre_Corto" HeaderText="Serie" />
                    <asp:BoundField DataField="FS_Moneda" HeaderText="Moneda Serie" />
                    <asp:BoundField DataField="RES_Cuotas" HeaderText="Cuotas a Rescatar" DataFormatString="{0:N0}" ItemStyle-HorizontalAlign="Right" />
                    <asp:BoundField DataField="RES_Moneda_Pago" HeaderText="Moneda Pago" />
                    <asp:BoundField DataField="ADCV_Cantidad" HeaderText="Cuotas DCV" DataFormatString="{0:N0}" ItemStyle-HorizontalAlign="Right" />
                    <asp:BoundField DataField="RES_Fecha_Nav" HeaderText="Fecha Nav" DataFormatString="{0:dd-MM-yyyy}" />
                    <asp:BoundField DataField="RES_Fecha_Pago" HeaderText="Fecha Rescate" DataFormatString="{0:dd-MM-yyyy}" />
                    <asp:BoundField DataField="RES_FechaTCObs" HeaderText="Fecha TC" DataFormatString="{0:dd-MM-yyyy}" />
                    <asp:BoundField DataField="RES_NavFormat" HeaderText="NAV" ItemStyle-HorizontalAlign="Right" />
                    <asp:BoundField DataField="RES_MontoFormat" HeaderText="Monto" DataFormatString="{0:N2}" ItemStyle-HorizontalAlign="Right" />
                    <asp:BoundField DataField="RES_Nav_CLPFormat" HeaderText="NAV CLP" ItemStyle-HorizontalAlign="Right" />
                    <asp:BoundField DataField="RES_Monto_CLPFormat" HeaderText="Monto CLP" DataFormatString="{0:N0}" ItemStyle-HorizontalAlign="Right" />
                    <asp:BoundField DataField="TC_Valor" HeaderText="Tipo Cambio" DataFormatString="{0:N12}" ItemStyle-HorizontalAlign="Right" />
                    <asp:BoundField DataField="RES_Contrato" HeaderText="Contrato" />
                    <asp:BoundField DataField="RES_Poderes" HeaderText="Poderes" />
                    <asp:BoundField DataField="RES_Estado" HeaderText="Estado" />
                    <asp:BoundField DataField="RES_Observaciones" HeaderText="Observaciones" />
                    <asp:BoundField DataField="RES_Patrimonio" HeaderText="Patrimonio" DataFormatString="{0:N4}" ItemStyle-HorizontalAlign="Right" />
                    <asp:BoundField DataField="FS_Patrimonio" HeaderText="% Patrimonio" DataFormatString="{0:N0}" ItemStyle-HorizontalAlign="Right" />
                    <asp:BoundField DataField="RES_Maximo" HeaderText="Rescates Máximo" DataFormatString="{0:N4}" ItemStyle-HorizontalAlign="Right" />
                    <asp:BoundField DataField="RES_Utilizado" HeaderText="Patrimonio Utilizado" DataFormatString="{0:N4}" ItemStyle-HorizontalAlign="Right" />
                    <asp:BoundField DataField="RES_Disponible_Patrimonio" HeaderText="Disponible Patrimonio" DataFormatString="{0:N4}" ItemStyle-HorizontalAlign="Right" />
                    <asp:BoundField DataField="ADCV_Fecha" HeaderText="Fecha DCV" DataFormatString="{0:dd-MM-yyyy}" />
                    <asp:BoundField DataField="RES_Transito" HeaderText="Rescates en Tránsito" DataFormatString="{0:N0}" ItemStyle-HorizontalAlign="Right" />
                    <asp:BoundField DataField="SC_Cuotas_a_Suscribir" HeaderText="Suscripciones en Tránsito" DataFormatString="{0:N0}" ItemStyle-HorizontalAlign="Right" />
                    <asp:BoundField DataField="CN_Cuotas_Disponibles" HeaderText="Canjes en Tránsito" DataFormatString="{0:N0}" ItemStyle-HorizontalAlign="Right" />
                    <asp:BoundField DataField="RES_Cuotas_Disponibles" HeaderText="Cuotas Disponibles" DataFormatString="{0:N0}" ItemStyle-HorizontalAlign="Right" />
                    <asp:BoundField DataField="RES_Fijacion_NAV" HeaderText="Fijación NAV" />
                    <asp:BoundField DataField="RES_Fijacion_TCObservado" HeaderText="Fijación TC Observado" />
                    <asp:BoundField DataField="RES_Fecha_Ingreso" HeaderText="Fecha Ingreso" DataFormatString="{0:dd-MM-yyyy}" />
                    <asp:BoundField DataField="RES_Usuario_Ingreso" HeaderText="Usuario Ingreso" />
                    <asp:BoundField DataField="RES_Fecha_Modificacion" HeaderText="Fecha Modificación" DataFormatString="{0:dd-MM-yyyy}" />
                    <asp:BoundField DataField="RES_Usuario_Modificacion" HeaderText="Usuario Modificador" />
                </Columns>
            </asp:GridView>
        </div>
        <div class="row mt-3">
            <div class="col-md-12 text-center">
                <asp:Button ID="BtnModificar" runat="server" class="btn btn-info" Text="Modificar" Enabled="false" OnClick="BtnModificar_Click"></asp:Button>
                <asp:Button ID="BtnEliminar" runat="server" class="btn btn-danger" Text="Eliminar" Enabled="false" OnClick="BtnEliminar_Click"></asp:Button>
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
                        <asp:Label ID="lbModalTittle" runat="server" Text="Formulario - Rescates" Font-Bold="true" Font-Size="X-Large"> </asp:Label>
                    </h4>
                    <button id="btnXCerrar" type="button" class="close" data-dismiss="modal" aria-label="Close" runat="server"><span aria-hidden="true">&times;</span></button>
                </div>
                <div class="modal-body">
                    <div class="jumbotron">
                        <div class="card p-4">
                            <div class="row">
                                <!-- FORMULARIO-->
                                <asp:TextBox ID="txtIDRescate" runat="server" Visible="false"></asp:TextBox>
                                <asp:TextBox ID="txtultimoTC_CLP" runat="server" Visible="false"></asp:TextBox>

                                <div class="col-lg-6">
                                    <div class="card h-30 mt-0">
                                        <div class="card-body">
                                            <hr />
                                            <div class="row mt-3">
                                                <div class="col-md-5">
                                                    <label class="form-control-label">RUT Aportante</label>
                                                </div>
                                                <div class="col-md-6">
                                                    <asp:UpdatePanel runat="server" ID="UpdatePanel6" UpdateMode="Conditional">
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="ddlModalNombreAportante" EventName="SelectedIndexChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="ddlModalMultifondo" EventName="SelectedIndexChanged" />
                                                        </Triggers>
                                                        <ContentTemplate>
                                                            <asp:DropDownList ID="ddlModalRutAportante" CssClass="form-control js-select2-rut" runat="server" OnSelectedIndexChanged="CargarNombreAportanteNemotecnicoPorRutAportanteModal" AutoPostBack="true" />
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                            </div>
                                            <div class="row mt-3">
                                                <div class="col-md-5">
                                                    <label class="form-control-label">Nombre Aportante</label>
                                                </div>
                                                <div class="col-md-6">
                                                    <asp:UpdatePanel runat="server" ID="UpdatePanel4" UpdateMode="Conditional">
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="ddlModalRutAportante" EventName="SelectedIndexChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="ddlModalMultifondo" EventName="SelectedIndexChanged" />
                                                        </Triggers>
                                                        <ContentTemplate>
                                                            <asp:DropDownList ID="ddlModalNombreAportante" CssClass="form-control js-select2-rut" runat="server" OnSelectedIndexChanged="CargarRutAportanteNemotecnicoPorNombreAportanteModal" AutoPostBack="true" />
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                            </div>
                                            <div class="row mt-3">
                                                <div class="col-md-5">
                                                    <label class="form-control-label">Multifondo</label>
                                                </div>
                                                <div class="col-md-6">
                                                    <asp:UpdatePanel runat="server" ID="UpdatePanel7" UpdateMode="Conditional">
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="ddlModalRutAportante" EventName="SelectedIndexChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="ddlModalNombreAportante" EventName="SelectedIndexChanged" />
                                                        </Triggers>
                                                        <ContentTemplate>
                                                            <asp:DropDownList ID="ddlModalMultifondo" CssClass="form-control js-select2-rut" runat="server" OnSelectedIndexChanged="CargarRutAportanteNombreAportantePorModalMultifondo" AutoPostBack="true" />
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                            </div>
                                            <div class="row mt-3">
                                                <div class="col-md-5">
                                                    <label class="form-control-label">Nombre Serie</label>
                                                </div>
                                                <div class="col-md-6">
                                                    <asp:UpdatePanel runat="server" ID="UpdatePanel2" UpdateMode="Conditional">
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="ddlModalRutFondo" EventName="SelectedIndexChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="ddlModalNombreFondo" EventName="SelectedIndexChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="ddlModalNemotecnico" EventName="SelectedIndexChanged" />
                                                        </Triggers>
                                                        <ContentTemplate>
                                                            <asp:TextBox ID="txtModalNombreSerie" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>

                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                            </div>

                                            <div class="row mt-3">
                                                <div class="col-md-5">
                                                    <label class="form-control-label">Cuotas</label>
                                                </div>
                                                <div class="col-md-6">
                                                    <asp:UpdatePanel runat="server" ID="UpdatePanel16" UpdateMode="Conditional">
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="txtModalNAV" EventName="TextChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="txtModalMonto" EventName="TextChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="txtModalMontoCLP" EventName="TextChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="txtModalNAV_CLP" EventName="TextChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="ddlModalRutAportante" EventName="SelectedIndexChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="ddlModalNombreAportante" EventName="SelectedIndexChanged" />
                                                            
                                                        </Triggers>
                                                        <ContentTemplate>
                                                            <asp:TextBox ID="txtModalCuota" runat="server" CssClass="form-control dbs-entero-decimal" OnTextChanged="CargarMontoModal" AutoPostBack="true"></asp:TextBox>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                            </div>
                                            <div class="row mt-3">
                                                <div class="col-md-5">
                                                    <label class="form-control-label">Moneda de Pago</label>
                                                </div>
                                                <div class="col-md-6">
                                                    <asp:UpdatePanel runat="server" ID="UpdatePanel9" UpdateMode="Conditional">
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="ddlModalRutFondo" EventName="SelectedIndexChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="ddlModalNombreFondo" EventName="SelectedIndexChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="ddlModalNemotecnico" EventName="SelectedIndexChanged" />
                                                        </Triggers>
                                                        <ContentTemplate>
                                                            <asp:DropDownList ID="ddlModalMonedaPago" CssClass="form-control js-select2-rut" runat="server" AutoPostBack="true">
                                                                <asp:ListItem Value="USD">USD</asp:ListItem>
                                                                <asp:ListItem Value="CLP">CLP</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                            </div>
                                            <div class="row mt-3">
                                                <div class="col-md-5">
                                                    <label class="form-control-label">TC Observado</label>
                                                </div>
                                                <div class="col-md-6">
                                                    <asp:UpdatePanel runat="server" ID="UpdatePanel18" UpdateMode="Conditional">
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="ddlModalRutFondo" EventName="SelectedIndexChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="ddlModalNombreFondo" EventName="SelectedIndexChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="ddlModalNemotecnico" EventName="SelectedIndexChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="txtModalFechaSolicitud" EventName="TextChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="txtModalFechaTCObs" EventName="TextChanged" />
                                                        </Triggers>
                                                        <ContentTemplate>
                                                            <asp:TextBox ID="txtModalTCObservado" runat="server" CssClass="form-control dbs-entero28-decimal12" OnTextChanged="CargarNAV_CLP" AutoPostBack="true"></asp:TextBox>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="card h-40 mt-0">
                                        <div class="card-body">
                                            <h4 class="card-title"></h4>
                                            <hr />
                                            <div class="row mt-3">
                                                <div class="col-md-5">
                                                    <label class="form-control-label">Fecha de Solicitud</label>
                                                </div>
                                                <div class="col-md-6">
                                                    <asp:UpdatePanel runat="server" ID="UpdatePanel3" UpdateMode="Conditional">
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="ddlModalRutFondo" EventName="SelectedIndexChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="ddlModalNombreFondo" EventName="SelectedIndexChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="ddlModalNemotecnico" EventName="SelectedIndexChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="txtModalFechaSolicitud" EventName="TextChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="ddlModalMonedaPago" EventName="SelectedIndexChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="txtModalCuota" EventName="TextChanged" />

                                                        </Triggers>
                                                        <ContentTemplate>
                                                            <div class="input-group">
                                                                <asp:TextBox ID="txtModalFechaSolicitud" runat="server" CssClass="form-control datepicker" AutoPostBack="true" Enabled="false"></asp:TextBox>
                                                                <asp:LinkButton ID="lnkModalFechaSolicitud" class="btn btn-moneda" runat="server" Visible="false" OnClientClick="return clickCalendar('txtModalFechaSolicitud')"><i class="far fa-calendar-alt"></i></asp:LinkButton>
                                                                <asp:LinkButton ID="lnkModalBorrarFechaSolicitud" Text="" OnClientClick="return limpiarCalendar('txtModalFechaSolicitud')" class="btn btn-secondary ml-1" runat="server" Visible="false"><i class="far fa-trash-alt"></i></asp:LinkButton>

                                                            </div>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                            </div>
                                            <div class="row mt-3">
                                                <div class="col-md-5">
                                                    <label class="form-control-label">Fecha de NAV</label>
                                                </div>
                                                <div class="col-md-6">
                                                    <asp:UpdatePanel runat="server" ID="UpdatePanel11" UpdateMode="Conditional">
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="ddlModalRutFondo" EventName="SelectedIndexChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="ddlModalNombreFondo" EventName="SelectedIndexChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="ddlModalNemotecnico" EventName="SelectedIndexChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="txtModalFechaSolicitud" EventName="TextChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="ddlModalMonedaPago" EventName="SelectedIndexChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="txtModalCuota" EventName="TextChanged" />
                                                        </Triggers>
                                                        <ContentTemplate>
                                                            <div class="input-group">
                                                                <asp:TextBox ID="txtModalFechaNAV" runat="server" CssClass="form-control datepicker" AutoPostBack="true" Enabled="false"></asp:TextBox>
                                                                <asp:LinkButton ID="lnkBtnModalFechaNAV" class="btn btn-moneda" runat="server" Visible="false" OnClientClick="return clickCalendar('txtModalFechaNAV')"><i class="far fa-calendar-alt"></i></asp:LinkButton>
                                                                <asp:LinkButton ID="lnkBtnModalBorrarFechaNAV" Text="" class="btn btn-secondary ml-1" OnClientClick="return limpiarCalendar('txtModalFechaNAV')" runat="server" Visible="false"><i class="far fa-trash-alt"></i></asp:LinkButton>

                                                            </div>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                            </div>
                                            <div class="row mt-3">
                                                <div class="col-md-5">
                                                    <label class="form-control-label">Fecha de Pago</label>
                                                </div>
                                                <div class="col-md-6">
                                                    <asp:UpdatePanel runat="server" ID="UpdatePanel12" UpdateMode="Conditional">
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="ddlModalRutFondo" EventName="SelectedIndexChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="ddlModalNombreFondo" EventName="SelectedIndexChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="ddlModalNemotecnico" EventName="SelectedIndexChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="txtModalFechaSolicitud" EventName="TextChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="ddlModalMonedaPago" EventName="SelectedIndexChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="txtModalCuota" EventName="TextChanged" />
                                                        </Triggers>
                                                        <ContentTemplate>
                                                            <div class="input-group">
                                                                <asp:TextBox ID="txtModalFechaPago" runat="server" CssClass="form-control datepicker" Enabled="false" AutoPostBack="true"></asp:TextBox>
                                                                <asp:LinkButton ID="lnkBtnModalFechaPago" class="btn btn-moneda" runat="server" Visible="false" OnClientClick="return clickCalendar('txtModalFechaPago')"><i class="far fa-calendar-alt"></i></asp:LinkButton>
                                                                <asp:LinkButton ID="lnkBtnModalBorrarFechaPago" Text="" class="btn btn-secondary ml-1" runat="server" OnClientClick="return limpiarCalendar('txtModalFechaPago')" Visible="false"><i class="far fa-trash-alt"></i></asp:LinkButton>

                                                            </div>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                            </div>
                                            <div class="row mt-3">
                                                <div class="col-md-5">
                                                    <label class="form-control-label">Fecha de TC Obs</label>
                                                </div>
                                                <div class="col-md-6">
                                                    <asp:UpdatePanel runat="server" ID="UpdatePanel13" UpdateMode="Conditional">
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="ddlModalRutFondo" EventName="SelectedIndexChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="ddlModalNombreFondo" EventName="SelectedIndexChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="ddlModalNemotecnico" EventName="SelectedIndexChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="txtModalFechaSolicitud" EventName="TextChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="ddlModalMonedaPago" EventName="SelectedIndexChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="txtModalCuota" EventName="TextChanged" />
                                                        </Triggers>
                                                        <ContentTemplate>
                                                            <div class="input-group">
                                                                <asp:TextBox ID="txtModalFechaTCObs" runat="server" CssClass="form-control datepicker" Enabled="false" AutoPostBack="true"></asp:TextBox>
                                                                <asp:LinkButton ID="lnkBtnModalFechaTCObs" class="btn btn-moneda" runat="server" Visible="true" OnClientClick="return clickCalendar('txtModalFechaTCObs')"><i class="far fa-calendar-alt"></i></asp:LinkButton>
                                                                <asp:LinkButton ID="lnkBtnModalBorrarFechaTCObs" Text="" class="btn btn-secondary ml-1" OnClientClick="return limpiarCalendar('txtModalFechaTCObs')" runat="server" Visible="false"><i class="far fa-trash-alt"></i></asp:LinkButton>

                                                            </div>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                            </div>
                                            <div class="row mt-3">
                                                <div class="col-md-5">
                                                    <label class="form-control-label">Monto</label>
                                                </div>
                                                <div class="col-md-6">
                                                    <asp:UpdatePanel runat="server" ID="UpdatePanel14" UpdateMode="Conditional">
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="txtModalCuota" EventName="TextChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="txtModalNAV" EventName="TextChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="txtModalFechaTCObs" EventName="TextChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="txtModalFechaSolicitud" EventName="TextChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="txtModalFechaNAV" EventName="TextChanged" />
                                                        </Triggers>
                                                        <ContentTemplate>
                                                            <asp:TextBox ID="txtModalMonto" runat="server" CssClass="form-control dbs-entero14-decimal4" OnTextChanged="CargarCuotasModal" AutoPostBack="true"></asp:TextBox>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                            </div>
                                            <div class="row mt-3">
                                                <div class="col-md-5">
                                                    <label class="form-control-label">Monto (CLP)</label>
                                                </div>
                                                <div class="col-md-6">
                                                    <asp:UpdatePanel runat="server" ID="UpdatePanel20" UpdateMode="Conditional">
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="txtModalCuota" EventName="TextChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="txtModalNAV" EventName="TextChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="txtModalNAV_CLP" EventName="TextChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="txtModalFechaTCObs" EventName="TextChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="txtModalMonto" EventName="TextChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="txtModalFechaSolicitud" EventName="TextChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="txtModalTCObservado" EventName="TextChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="txtModalFechaNAV" EventName="TextChanged" />

                                                        </Triggers>
                                                        <ContentTemplate>
                                                            <asp:TextBox ID="txtModalMontoCLP" runat="server" CssClass="form-control dbs-entero14-decimal4" OnTextChanged="CargarCuotasCLPModal" AutoPostBack="true" Enabled="false"></asp:TextBox>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                            </div>
                                            <div class="row mt-3">
                                                <div class="col-md-5">
                                                    <label class="form-control-label">Contrato</label>
                                                </div>
                                                <div class="col-md-6">
                                                    <asp:DropDownList ID="ddlModalContrato" CssClass="form-control js-select2-rut" runat="server"></asp:DropDownList>

                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="card h-30 mt-0">
                                        <div class="card-body">
                                            <h4 class="card-title">Patrimonio Disponible</h4>
                                            <hr />
                                            <div class="row mt-3">
                                                <div class="col-md-5">
                                                    <label class="form-control-label">Patrimonio</label>
                                                </div>
                                                <div class="col-md-6">
                                                    <asp:UpdatePanel runat="server" ID="UpdatePanel28" UpdateMode="Conditional">
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="ddlModalRutFondo" EventName="SelectedIndexChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="ddlModalNombreFondo" EventName="SelectedIndexChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="ddlModalNemotecnico" EventName="SelectedIndexChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="txtModalFechaSolicitud" EventName="TextChanged" />
                                                        </Triggers>
                                                        <ContentTemplate>
                                                            <asp:TextBox ID="txtModalPatrimonio" runat="server" CssClass="form-control dbs-entero15-decimal4" ReadOnly="True"></asp:TextBox>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                            </div>
                                            <div class="row mt-3">
                                                <div class="col-md-5">
                                                    <label class="form-control-label">Porcentaje %</label>
                                                </div>
                                                <div class="col-md-6">
                                                    <asp:UpdatePanel runat="server" ID="UpdatePanel24" UpdateMode="Conditional">
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="ddlModalRutFondo" EventName="SelectedIndexChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="ddlModalNombreFondo" EventName="SelectedIndexChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="ddlModalNemotecnico" EventName="SelectedIndexChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="txtModalFechaSolicitud" EventName="TextChanged" />
                                                        </Triggers>
                                                        <ContentTemplate>
                                                            <asp:TextBox ID="txtModalPorcentaje" runat="server" CssClass="form-control form-control-sm" ReadOnly="True"></asp:TextBox>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                            </div>
                                            <div class="row mt-3">
                                                <div class="col-md-5">
                                                    <label class="form-control-label">Rescate Max</label>
                                                </div>
                                                <div class="col-md-6">
                                                    <asp:UpdatePanel runat="server" ID="UpdatePanel29" UpdateMode="Conditional">
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="ddlModalRutFondo" EventName="SelectedIndexChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="ddlModalNombreFondo" EventName="SelectedIndexChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="ddlModalNemotecnico" EventName="SelectedIndexChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="txtModalFechaSolicitud" EventName="TextChanged" />
                                                        </Triggers>
                                                        <ContentTemplate>
                                                            <asp:TextBox ID="txtModalRescateMax" runat="server" CssClass="form-control dbs-entero14-decimal4" ReadOnly="True"></asp:TextBox>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                            </div>
                                            <div class="row mt-3">
                                                <div class="col-md-5">

                                                    <label class="form-control-label">Utilizado</label>
                                                </div>
                                                <div class="col-md-6">
                                                    <asp:UpdatePanel runat="server" ID="UpdatePanel30" UpdateMode="Conditional">
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="ddlModalRutFondo" EventName="SelectedIndexChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="ddlModalNombreFondo" EventName="SelectedIndexChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="ddlModalNemotecnico" EventName="SelectedIndexChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="txtModalFechaSolicitud" EventName="TextChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="ddlModalRutAportante" EventName="SelectedIndexChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="ddlModalNombreAportante" EventName="SelectedIndexChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="ddlModalMultifondo" EventName="SelectedIndexChanged" />
                                                        </Triggers>
                                                        <ContentTemplate>
                                                            <asp:TextBox ID="txtModalUtilizado" runat="server" CssClass="form-control dbs-entero14-decimal4" ReadOnly="True"></asp:TextBox>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                            </div>
                                            <div class="row mt-3">
                                                <div class="col-md-5">
                                                    <label class="form-control-label">Disponibles</label>
                                                </div>
                                                <div class="col-md-6">
                                                    <asp:UpdatePanel runat="server" ID="UpdatePanel31" UpdateMode="Conditional">
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="ddlModalRutFondo" EventName="SelectedIndexChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="ddlModalNombreFondo" EventName="SelectedIndexChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="ddlModalNemotecnico" EventName="SelectedIndexChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="txtModalFechaSolicitud" EventName="TextChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="ddlModalRutAportante" EventName="SelectedIndexChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="ddlModalNombreAportante" EventName="SelectedIndexChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="ddlModalMultifondo" EventName="SelectedIndexChanged" />
                                                        </Triggers>
                                                        <ContentTemplate>
                                                            <asp:TextBox ID="txtModalDisponibles" runat="server" CssClass="form-control dbs-entero14-decimal4" ReadOnly="True"></asp:TextBox>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                            </div>
                                        </div>
                                    </div>


                                </div>
                                <div class="col-lg-6">
                                    <div class="card h-30 mt-0">
                                        <div class="card-body">
                                            <h4 class="card-title"></h4>
                                            <hr />
                                            <div class="row mt-3">
                                                <div class="col-md-5">
                                                    <label class="form-control-label">RUT Fondo</label>
                                                </div>
                                                <div class="col-md-6">
                                                    <asp:UpdatePanel runat="server" ID="UpdatePanel5" UpdateMode="Conditional">
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="ddlModalNombreFondo" EventName="SelectedIndexChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="ddlModalNemotecnico" EventName="SelectedIndexChanged" />
                                                        </Triggers>
                                                        <ContentTemplate>
                                                            <asp:DropDownList ID="ddlModalRutFondo" CssClass="form-control js-select2-rut" runat="server"
                                                                OnSelectedIndexChanged="CargarNombreFondoNemotecnicoPorRutFondoModal" AutoPostBack="true" />
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                            </div>
                                            <div class="row mt-3">
                                                <div class="col-md-5">
                                                    <label class="form-control-label">Nombre Fondo</label>
                                                </div>
                                                <div class="col-md-6">
                                                    <asp:UpdatePanel runat="server" ID="UpdatePanel10" UpdateMode="Conditional">
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="ddlModalRutFondo" EventName="SelectedIndexChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="ddlModalNemotecnico" EventName="SelectedIndexChanged" />
                                                        </Triggers>
                                                        <ContentTemplate>
                                                            <asp:DropDownList ID="ddlModalNombreFondo" CssClass="form-control js-select2-rut" runat="server" OnSelectedIndexChanged="CargarRutFondoNemotecnicoPorNombreFondoModal" AutoPostBack="true" />
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                            </div>

                                            <div class="row mt-3">
                                                <div class="col-md-5">
                                                    <label class="form-control-label">Nemotécnico</label>
                                                </div>
                                                <div class="col-md-6">
                                                    <asp:UpdatePanel runat="server" ID="UpdatePanel" UpdateMode="Conditional">
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="ddlModalRutFondo" EventName="SelectedIndexChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="ddlModalNombreFondo" EventName="SelectedIndexChanged" />
                                                        </Triggers>
                                                        <ContentTemplate>
                                                            <asp:DropDownList ID="ddlModalNemotecnico" CssClass="form-control js-select2-rut" runat="server" 
                                                                OnSelectedIndexChanged="CargarNombreFondoRutFondoPorModalNemotecnico" AutoPostBack="true" />
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                            </div>
                                            <div class="row mt-3">
                                                <div class="col-md-5">
                                                    <label class="form-control-label">Moneda Serie</label>
                                                </div>
                                                <div class="col-md-6">
                                                    <asp:UpdatePanel runat="server" ID="UpdatePanel15" UpdateMode="Conditional">
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="ddlModalRutFondo" EventName="SelectedIndexChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="ddlModalNombreFondo" EventName="SelectedIndexChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="ddlModalNemotecnico" EventName="SelectedIndexChanged" />
                                                        </Triggers>
                                                        <ContentTemplate>
                                                            <%--<asp:DropDownList ID="ddlModalMonedaSerie" CssClass="form-control js-select2-rut" runat="server" />--%>
                                                            <asp:TextBox ID="txtModalMonedaSerie"
                                                                runat="server" CssClass="form-control form-control-sm" Enabled="false"></asp:TextBox>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                            </div>
                                            <div class="row mt-3">
                                                <div class="col-md-5">
                                                    <label class="form-control-label">NAV</label>
                                                </div>
                                                <div class="col-md-6">
                                                    <asp:UpdatePanel runat="server" ID="UpdatePanel17" UpdateMode="Conditional">
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="ddlModalRutFondo" EventName="SelectedIndexChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="ddlModalNombreFondo" EventName="SelectedIndexChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="ddlModalNemotecnico" EventName="SelectedIndexChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="txtModalCuota" EventName="TextChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="txtModalFechaSolicitud" EventName="TextChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="txtModalFechaNAV" EventName="TextChanged" />
                                                        </Triggers>
                                                        <ContentTemplate>
                                                            <asp:TextBox ID="txtModalNAV" runat="server" CssClass="form-control dbs-entero14-decimal6" OnTextChanged="CargarMontoModal" AutoPostBack="true"></asp:TextBox>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                            </div>
                                            <div class="row mt-3">
                                                <div class="col-md-5">
                                                    <label class="form-control-label">NAV (CLP)</label>
                                                </div>
                                                <div class="col-md-6">
                                                    <asp:UpdatePanel runat="server" ID="UpdatePanel25" UpdateMode="Conditional">
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="ddlModalRutFondo" EventName="SelectedIndexChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="ddlModalNombreFondo" EventName="SelectedIndexChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="ddlModalNemotecnico" EventName="SelectedIndexChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="txtModalCuota" EventName="TextChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="txtModalNAV" EventName="TextChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="txtModalTCObservado" EventName="TextChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="txtModalFechaSolicitud" EventName="TextChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="txtModalFechaNAV" EventName="TextChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="txtModalFechaTCObs" EventName="TextChanged" />
                                                        </Triggers>
                                                        <ContentTemplate>
                                                            <asp:TextBox ID="txtModalNAV_CLP" runat="server" CssClass="form-control dbs-entero14-decimal4" OnTextChanged="CargarMontoModal" AutoPostBack="true" Enabled="false"></asp:TextBox>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                            </div>
                                            <div class="row mt-3">
                                                <div class="col-md-5">
                                                    <label class="form-control-label">Poderes</label>
                                                </div>
                                                <div class="col-md-6">
                                                    <asp:DropDownList ID="ddlModalPoderes" CssClass="form-control js-select2-rut" runat="server"></asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="card h-40 mt-0">
                                        <div class="card-body">
                                            <h4 class="card-title"></h4>
                                            <hr />
                                            <div class="row mt-3">
                                                <div class="col-md-5">
                                                    <label class="form-control-label">Fecha DCV</label>
                                                </div>
                                                <div class="col-md-6">
                                                    <asp:UpdatePanel runat="server" ID="UpdatePanel19" UpdateMode="Conditional">
                                                        <Triggers>
                                                            <%--<asp:AsyncPostBackTrigger ControlID="ddlModalRutFondo" EventName="SelectedIndexChanged" />--%>
                                                            <asp:AsyncPostBackTrigger ControlID="ddlModalNombreFondo" EventName="SelectedIndexChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="ddlModalNemotecnico" EventName="SelectedIndexChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="ddlModalNombreAportante" EventName="SelectedIndexChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="ddlModalRutAportante" EventName="SelectedIndexChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="txtModalFechaSolicitud" EventName="TextChanged" />
                                                        </Triggers>
                                                        <ContentTemplate>
                                                            <asp:TextBox ID="txtModalFechaDCV" runat="server" CssClass="form-control datepicker" ReadOnly="True"></asp:TextBox>
                                                            <asp:Calendar ID="CalendarModalFechaDCV" runat="server" Visible="False" BackColor="White" BorderColor="White" BorderWidth="1px" Font-Names="Verdana" Font-Size="9pt" ForeColor="Black" Height="190px" NextPrevFormat="FullMonth" Width="350px" class="calendarios">
                                                                <DayHeaderStyle Font-Bold="True" Font-Size="8pt" />
                                                                <NextPrevStyle Font-Bold="True" Font-Size="8pt" ForeColor="#333333" VerticalAlign="Bottom" />
                                                                <OtherMonthDayStyle ForeColor="#999999" />
                                                                <SelectedDayStyle BackColor="#333399" ForeColor="White" />
                                                                <TitleStyle BackColor="White" BorderColor="Black" BorderWidth="1px" Font-Bold="True" Font-Size="12pt" ForeColor="#333399" CssClass="cabecera texto-cabecera" />
                                                                <TodayDayStyle BackColor="#CCCCCC" />
                                                            </asp:Calendar>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                            </div>
                                            <div class="row mt-3">
                                                <div class="col-md-5">
                                                    <label class="form-control-label">Estado</label>
                                                </div>
                                                <div class="col-md-6">
                                                    <asp:DropDownList ID="ddlModalEstado" CssClass="form-control js-select2-rut" runat="server">
                                                        <asp:ListItem Value="Pendiente">PENDIENTE</asp:ListItem>
                                                        <asp:ListItem Value="Cerrado">CERRADO</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="row mt-3">
                                                <div class="col-md-5">
                                                    <label class="form-control-label">Observaciones</label>
                                                </div>
                                                <div class="col-md-6">
                                                    <asp:TextBox ID="txtModalObservaciones" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="row mt-3">
                                                <div class="col-md-5">
                                                    <label class="form-control-label">Fijación NAV</label>
                                                </div>
                                                <div class="col-md-6">
                                                    <asp:UpdatePanel runat="server" ID="UpdatePanel21" UpdateMode="Conditional">
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="ddlModalRutFondo" EventName="SelectedIndexChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="ddlModalNombreFondo" EventName="SelectedIndexChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="ddlModalNemotecnico" EventName="SelectedIndexChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="txtModalCuota" EventName="TextChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="txtModalFechaSolicitud" EventName="TextChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="txtModalFechaNAV" EventName="TextChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="txtModalFechaPago" EventName="TextChanged" />

                                                        </Triggers>
                                                        <ContentTemplate>
                                                            <asp:TextBox ID="txtModalFijacionNAV" runat="server" CssClass="form-control form-control-sm" ReadOnly="True"></asp:TextBox>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                            </div>
                                            <div class="row mt-3">
                                                <div class="col-md-5">
                                                    <label class="form-control-label">Fijación TC Obs</label>
                                                </div>
                                                <div class="col-md-6">
                                                    <asp:UpdatePanel runat="server" ID="UpdatePanel22" UpdateMode="Conditional">
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="ddlModalRutFondo" EventName="SelectedIndexChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="ddlModalNombreFondo" EventName="SelectedIndexChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="ddlModalNemotecnico" EventName="SelectedIndexChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="txtModalFechaSolicitud" EventName="TextChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="txtModalFechaTCObs" EventName="TextChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="txtModalFechaNAV" EventName="TextChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="txtModalFechaPago" EventName="TextChanged" />
                                                        </Triggers>
                                                        <ContentTemplate>
                                                            <asp:TextBox ID="txtModalFijacionTCObs" runat="server" CssClass="form-control form-control-sm" ReadOnly="True"></asp:TextBox>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="card h-30 mt-0">
                                        <div class="card-body">
                                            <h4 class="card-title">Cuotas Disponibles</h4>
                                            <hr />
                                            <div class="row mt-3">
                                                <div class="col-md-5">
                                                    <label class="form-control-label">Cuotas DCV</label>
                                                </div>
                                                <div class="col-md-6">
                                                    <asp:UpdatePanel runat="server" ID="UpdatePanel8" UpdateMode="Conditional">
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="ddlModalRutFondo" EventName="SelectedIndexChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="ddlModalNombreFondo" EventName="SelectedIndexChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="ddlModalNemotecnico" EventName="SelectedIndexChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="ddlModalRutAportante" EventName="SelectedIndexChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="ddlModalNombreAportante" EventName="SelectedIndexChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="ddlModalMultifondo" EventName="SelectedIndexChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="txtModalFechaSolicitud" EventName="TextChanged" />
                                                        </Triggers>
                                                        <ContentTemplate>
                                                            <asp:TextBox ID="txtModalCuotasDVC" runat="server" CssClass="form-control dbs-entero26-decimal0" ReadOnly="True"></asp:TextBox>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                            </div>
                                            <div class="row mt-3">
                                                <div class="col-md-5">
                                                    <label class="form-control-label">Rescates</label>
                                                </div>
                                                <div class="col-md-6">
                                                    <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Conditional">
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="ddlModalRutFondo" EventName="SelectedIndexChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="ddlModalNombreFondo" EventName="SelectedIndexChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="ddlModalNemotecnico" EventName="SelectedIndexChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="ddlModalRutAportante" EventName="SelectedIndexChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="ddlModalNombreAportante" EventName="SelectedIndexChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="ddlModalMultifondo" EventName="SelectedIndexChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="txtModalFechaSolicitud" EventName="TextChanged" />
                                                        </Triggers>
                                                        <ContentTemplate>
                                                            <asp:TextBox ID="txtModalRescates" runat="server" CssClass="form-control dbs-entero18-decimal0" ReadOnly="True"></asp:TextBox>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                            </div>
                                            <div class="row mt-3">
                                                <div class="col-md-5">
                                                    <label class="form-control-label">Suscripciones</label>
                                                </div>
                                                <div class="col-md-6">
                                                    <asp:UpdatePanel runat="server" ID="UpdatePanel26" UpdateMode="Conditional">
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="ddlModalRutFondo" EventName="SelectedIndexChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="ddlModalNombreFondo" EventName="SelectedIndexChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="ddlModalNemotecnico" EventName="SelectedIndexChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="ddlModalRutAportante" EventName="SelectedIndexChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="ddlModalNombreAportante" EventName="SelectedIndexChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="ddlModalMultifondo" EventName="SelectedIndexChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="txtModalFechaSolicitud" EventName="TextChanged" />
                                                        </Triggers>
                                                        <ContentTemplate>
                                                            <asp:TextBox ID="txtModalSuscripciones" runat="server" CssClass="form-control dbs-entero18-decimal0" ReadOnly="True"></asp:TextBox>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                            </div>
                                            <div class="row mt-3">
                                                <div class="col-md-5">
                                                    <label class="form-control-label">Canje</label>
                                                </div>
                                                <div class="col-md-6">
                                                    <asp:UpdatePanel runat="server" ID="UpdatePanel27" UpdateMode="Conditional">
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="ddlModalRutFondo" EventName="SelectedIndexChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="ddlModalNombreFondo" EventName="SelectedIndexChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="ddlModalNemotecnico" EventName="SelectedIndexChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="ddlModalRutAportante" EventName="SelectedIndexChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="ddlModalNombreAportante" EventName="SelectedIndexChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="ddlModalMultifondo" EventName="SelectedIndexChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="txtModalFechaSolicitud" EventName="TextChanged" />
                                                        </Triggers>
                                                        <ContentTemplate>
                                                            <asp:TextBox ID="txtModalCanje" runat="server" CssClass="form-control dbs-entero18-decimal0" ReadOnly="True"></asp:TextBox>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                            </div>
                                            <div class="row mt-3">
                                                <div class="col-md-5">
                                                    <label class="form-control-label">Disponibles</label>
                                                </div>
                                                <div class="col-md-6">
                                                    <asp:UpdatePanel runat="server" ID="UpdatePanel23" UpdateMode="Conditional">
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="ddlModalRutFondo" EventName="SelectedIndexChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="ddlModalNombreFondo" EventName="SelectedIndexChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="ddlModalNemotecnico" EventName="SelectedIndexChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="ddlModalRutAportante" EventName="SelectedIndexChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="ddlModalNombreAportante" EventName="SelectedIndexChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="ddlModalMultifondo" EventName="SelectedIndexChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="txtModalFechaSolicitud" EventName="TextChanged" />
                                                        </Triggers>
                                                        <ContentTemplate>
                                                            <asp:TextBox ID="txtModalDisponiblesCuotasDisponibles" runat="server" CssClass="form-control dbs-entero18-decimal0" ReadOnly="True"></asp:TextBox>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                </div>

                            </div>

                            <!-- GRUPO DE BOTONES 2 -->
                            <div class="form-group mt-5 text-center">
                                <div class="col-md-offset-1">
                                    <asp:Button ID="btnModalGuardar" Text="Guardar" CssClass="btn btn-info" runat="server"></asp:Button>
                                    <asp:Button ID="btnModalModificar" Text="Modificar" CssClass="btn btn-info" runat="server"></asp:Button>
                                    <asp:Button ID="btnModalCancelar" Text="Cancelar" CssClass="btn btn-secondary" runat="server" OnClientClick="if (!confirm('¿Seguro que desea Cancelar?')) return false;"></asp:Button>
                                    <asp:Button ID="btnModalEliminar" Text="Eliminar Rescate" runat="server" class="btn btn-danger" OnClientClick="if (!confirm('¿Seguro que desea Eliminar los elementos seleccionados?')) return false;"></asp:Button>
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
                        <asp:Label ID="lblModalTitle" runat="server" Text="" Font-Bold="true" Font-Size="X-Large"> </asp:Label>
                    </h4>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Cerrar">
                        <span aria-hidden="true">&times;</span>
                    </button>
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
                    <button type="button" class="btn btn-secondary" data-dismiss="modal" onclick="cerrarAlert();">Cerrar</button>
                </div>
            </div>
        </div>
    </div>

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
    <!-- End Bootstrap Modal Dialog Mensajes-->

    <!--PopUp Rescates-->
    <div class="modal fade" id="PopUpRescate" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" data-keyboard="false" data-backdrop="static">
        <div class="modal-dialog" style="max-width: 90%;">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title">
                        <asp:Label ID="Label8" runat="server" Text="CONFIRMACION SOLICITUD DE RESCATE"
                            Font-Bold="true" Font-Size="X-Large"> </asp:Label>
                    </h4>
                    <button id="Button1" type="button" class="close" data-dismiss="modal" aria-label="Close" runat="server"><span aria-hidden="true">&times;</span></button>
                </div>
                <div class="modal-body">
                    <div class="jumbotron">
                        <div class="card p-4">
                            <div class="row">
                                <!-- FECHA SOLICITUD-->
                                <div class="col-md-4">
                                    <label class="form-control-label">FECHA SOLICITUD</label>
                                </div>
                                <div class="col-md-8">
                                    <asp:Label ID="lblPopUpFechaSolicitud" runat="server" Text="" Font-Bold="true" Font-Size="X-Large"> </asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <!-- Hora Solicitud-->
                                <div class="col-md-4">
                                    <label class="form-control-label">Hora Solicitud</label>
                                </div>
                                <div class="col-md-8">
                                    <asp:Label ID="lblPopUpHoraSolicitud" runat="server" Text="" Font-Bold="true" Font-Size="X-Large"> </asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <!-- Tipo-->
                                <div class="col-md-4">
                                    <label class="form-control-label">Tipo</label>
                                </div>
                                <div class="col-md-8">
                                    <asp:Label ID="lblPopUpTipo" runat="server" Text="" Font-Bold="true" Font-Size="X-Large"> </asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <!-- Nemo Fondo-->
                                <div class="col-md-4">
                                    <label class="form-control-label">Nemo Fondo</label>
                                </div>
                                <div class="col-md-8">
                                    <asp:Label ID="lblPopUpNemoFondo" runat="server" Text="" Font-Bold="true" Font-Size="X-Large"> </asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <!-- Nombre Fondo-->
                                <div class="col-md-4">
                                    <label class="form-control-label">Nombre Fondo</label>
                                </div>
                                <div class="col-md-8">
                                    <asp:Label ID="lblPopUpNombreFondo" runat="server" Text="" Font-Bold="true" Font-Size="X-Large"> </asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <!-- Serie-->
                                <div class="col-md-4">
                                    <label class="form-control-label">Serie</label>
                                </div>
                                <div class="col-md-8">
                                    <asp:Label ID="lblPopUpSerie" runat="server" Text="" Font-Bold="true" Font-Size="X-Large"> </asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <!-- Administradora-->
                                <div class="col-md-4">
                                    <label class="form-control-label">Administradora</label>
                                </div>
                                <div class="col-md-8">
                                    <asp:Label ID="lblPopUpAdministradora" runat="server" Text="" Font-Bold="true" Font-Size="X-Large"> </asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <!-- RUT Administradora-->
                                <div class="col-md-4">
                                    <label class="form-control-label">RUT Administradora</label>
                                </div>
                                <div class="col-md-8">
                                    <asp:Label ID="lblPopUpRutAdministradora" runat="server" Text="" Font-Bold="true" Font-Size="X-Large"> </asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <!-- Nombre Aportante-->
                                <div class="col-md-4">
                                    <label class="form-control-label">Nombre Aportante</label>
                                </div>
                                <div class="col-md-8">
                                    <asp:Label ID="lblPopUpNombreAportante" runat="server" Text="" Font-Bold="true" Font-Size="X-Large"> </asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <!-- Rut Aportante-->
                                <div class="col-md-4">
                                    <label class="form-control-label">Rut Aportante</label>
                                </div>
                                <div class="col-md-8">
                                    <asp:Label ID="lblPopUpRutAportante" runat="server" Text="" Font-Bold="true" Font-Size="X-Large"> </asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <!-- CUOTAS -->
                                <div class="col-md-4">
                                    <label class="form-control-label">CUOTAS</label>
                                </div>
                                <div class="col-md-8">
                                    <asp:Label ID="lblPopUpCuotas" runat="server" Text="" Font-Bold="true" Font-Size="X-Large"> </asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <!-- CUOTAS EN DCV-->
                                <div class="col-md-4">
                                    <label class="form-control-label">CUOTAS EN DCV</label>
                                </div>
                                <div class="col-md-8">
                                    <asp:Label ID="lblPopUpCuotasEnDCV" runat="server" Text="" Font-Bold="true" Font-Size="X-Large"> </asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <!-- CTTO GRAL DE FONDOS-->
                                <div class="col-md-4">
                                    <label class="form-control-label">CTTO GRAL DE FONDOS</label>
                                </div>
                                <div class="col-md-8">
                                    <asp:Label ID="lblPopUpCttoGralDeFondos" runat="server" Text="" Font-Bold="true" Font-Size="X-Large"> </asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <!-- PODERES/REG FIR-->
                                <div class="col-md-4">
                                    <label class="form-control-label">PODERES/REG FIR</label>
                                </div>
                                <div class="col-md-8">
                                    <asp:Label ID="lblPopUpPoderRegFir" runat="server" Text="" Font-Bold="true" Font-Size="X-Large"> </asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <!-- MONEDA DE PAGO-->
                                <div class="col-md-4">
                                    <label class="form-control-label">MONEDA DE PAGO</label>
                                </div>
                                <div class="col-md-8">
                                    <asp:Label ID="lblPopUpMonedaDePago" runat="server" Text="" Font-Bold="true" Font-Size="X-Large"> </asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <!-- VALOR NAV -->
                                <div class="col-md-4">
                                    <label class="form-control-label">VALOR NAV</label>
                                </div>
                                <div class="col-md-8">
                                    <asp:Label ID="lblPopUpValorNav" runat="server" Text="" Font-Bold="true" Font-Size="X-Large"> </asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <!-- USD OBS-->
                                <div class="col-md-4">
                                    <label class="form-control-label">USD OBS</label>
                                </div>
                                <div class="col-md-8">
                                    <asp:Label ID="lblPopUpUsdObs" runat="server" Text="" Font-Bold="true" Font-Size="X-Large"> </asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <!-- VALOR RESCATE-->
                                <div class="col-md-4">
                                    <label class="form-control-label">VALOR RESCATE</label>
                                </div>
                                <div class="col-md-8">
                                    <asp:Label ID="lblPopUpValorRescate" runat="server" Text="" Font-Bold="true" Font-Size="X-Large"> </asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <!-- FECHA NAV -->
                                <div class="col-md-4">
                                    <label class="form-control-label">FECHA NAV</label>
                                </div>
                                <div class="col-md-8">
                                    <asp:Label ID="lblPopUpFechaNav" runat="server" Text="" Font-Bold="true" Font-Size="X-Large"> </asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <!-- FECHA PAGO RESCATE-->
                                <div class="col-md-4">
                                    <label class="form-control-label">FECHA PAGO RESCATE</label>
                                </div>
                                <div class="col-md-8">
                                    <asp:Label ID="lblPopUpFechaPagoRescate" runat="server" Text="" Font-Bold="true" Font-Size="X-Large"> </asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <!-- EJECUTADO-->
                                <div class="col-md-4">
                                    <label class="form-control-label">EJECUTADO</label>
                                </div>
                                <div class="col-md-8">
                                    <asp:Label ID="lblPopUpEjecutado" runat="server" Text="" Font-Bold="true" Font-Size="X-Large"> </asp:Label>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <asp:HiddenField ID="HiddenPerfil" runat="server" />
    <asp:HiddenField ID="HiddenConstante" runat="server" />

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

        function calendarInitial() {
            $("[id*=txtFechaSolicitudDesde]").datepicker();
            $("[id*=txtFechaSolicitudHasta]").datepicker();
            $("[id*=txtFechaNAVDesde]").datepicker();
            $("[id*=txtFechaNAVHasta]").datepicker();
            $("[id*=txtFechaPagoDesde]").datepicker();
            $("[id*=txtFechaPagoHasta]").datepicker();

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

            $("[id*=txtModalFechaTCObs]").datepicker();
            $("[id*=txtModalFechaTCObs]").datepicker({
                container: '#myModal modal-body'
                , showOn: "none"
            });


            $("[id*=txtFechaSolicitudDesde]").change(function () {
                changeFechas($("[id*=txtFechaSolicitudDesde]"), $("[id*=txtFechaSolicitudHasta]"), 1)
            });
            $("[id*=txtFechaSolicitudHasta]").change(function () {
                changeFechas($("[id*=txtFechaSolicitudDesde]"), $("[id*=txtFechaSolicitudHasta]"), 2)
            });

            $("[id*=txtFechaNAVDesde]").change(function () {
                changeFechas($("[id*=txtFechaNAVDesde]"), $("[id*=txtFechaNAVHasta]"), 1)
            });
            $("[id*=txtFechaNAVHasta]").change(function () {
                changeFechas($("[id*=txtFechaNAVDesde]"), $("[id*=txtFechaNAVHasta]"), 2)
            });

            $("[id*=txtFechaPagoDesde]").change(function () {
                changeFechas($("[id*=txtFechaPagoDesde]"), $("[id*=txtFechaPagoHasta]"), 1)
            });
            $("[id*=txtFechaPagoHasta]").change(function () {
                changeFechas($("[id*=txtFechaPagoDesde]"), $("[id*=txtFechaPagoHasta]"), 2)
            });

            $("[id*=txtFechaPagoDesde]").change(function () {
                changeFechas($("[id*=txtFechaPagoDesde]"), $("[id*=txtFechaPagoHasta]"), 1)
            });
            $("[id*=txtFechaPagoHasta]").change(function () {
                changeFechas($("[id*=txtFechaPagoDesde]"), $("[id*=txtFechaPagoHasta]"), 2)
            });
        }

        $(document).ready(function () {
            //checkGrilla();
            confNumeros();

            var txtAccionHidden = $("#<%=txtAccionHidden.ClientID %>").val();
            if ((txtAccionHidden == "MODIFICAR") || (txtAccionHidden == "ELIMINAR") || (txtAccionHidden == "CREAR")) {
                $('#myModal').modal('show');
            }
            else {
                if (txtAccionHidden == "POPUPRESCATE") {
                    $('#PopUpRescate').modal('show');
                }
            }

            $("body").on("click", "#btnXCerrar", function () {
                $("#btnXCerrar").val("");
            });

            seteaBotonGuardar();
            seteaBotonModificar();
            //seteaBotonProrrotear();
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(bindDataTable);

            calendarInitial();

            $(".ConDecimales").val(function (index, value) {

                var arrayDeCadenas = value.split(",")
                console.log("lkasdlkja");
                valEnetero = arrayDeCadenas[0].replace(/\D/g, "")
                    .replace(/([0-9])([0-9]{3})$/g, '$1.$2')
                    .replace(/\B(?=(\d{3})+(?!\d)\.?)/g, ".");
                valDecimal = arrayDeCadenas[1]

                if (valDecimal == null || valDecimal == "") {
                    return valEnetero;
                }
                //valDecimal = valDecimal + '00000'; 
                return valEnetero + "," + valDecimal;
            });




        });



        function bindDataTable() {
            $(".js-select2-rut").select2({
                templateResult: formatState,
                placeholder: 'Selecciona una opción'
            });
            calendarInitial();
            confNumeros();
        };

        function confNumeros() {
            $('.dbs-entero18-decimal0').mask2(getMask(18, 0));
            $('.dbs-entero26-decimal0').mask2(getMask(26, 0));
            $('.dbs-entero14-decimal4').mask2(getMask(14, 4));
            $('.dbs-entero15-decimal4').mask2(getMask(15, 4));
            $('.dbs-entero28-decimal12').mask2(getMask(16, 12));
            $('.dbs-entero-decimal').mask2(getMask(16, 0));
            $('.dbs-entero14-decimal6').mask2(getMask(14, 6));

        }

        $('.checkFijacionAll').change(function () {
                    if ($('.checkFijacionAll input').prop('checked') == true) {
                        $('.checkFijacion input').prop('checked', true);
                        enableDisableButtons(true);
                    }
                    else {
                        $('.checkFijacion input').prop('checked', false);
                        enableDisableButtons(false);
                    }

                });

        function seteaBotonGuardar() {
            $("#<%=btnModalGuardar.ClientID %>").unbind("click");
            $("#<%=btnModalGuardar.ClientID %>").click(function () {
                var cuotasDCV = $('#<%=txtModalCuotasDVC.ClientID%>').val();

                if (cuotasDCV == "" || cuotasDCV == "0") {
                    alert("El rescate debe poseer cuotas DCV.")
                    return false;
                }

                var mensaje = '¿Confirma que desea Guardar?'
                var fdcv = $("#<%=txtModalFechaDCV.ClientID%>").val();
                var fSol = $("#<%=txtModalFechaSolicitud.ClientID%>").val();
                <%--var diasDesplazar = $("#<%=txtModalDiasHabiles.ClientID%>").val();--%>
                var diasDesplazar = 2;

                if ((fSol != "") && (fdcv != "")) {
                    var fechaSolicitud = new Date(fSol.split('-')[2], fSol.split('-')[1], fSol.split('-')[0]);
                    var fechaDcv = new Date(fdcv.split('-')[2], fdcv.split('-')[1], fdcv.split('-')[0] + diasDesplazar);


                    if (Date.parse(fechaSolicitud) > Date.parse(fechaDcv)) {
                        mensaje = 'ADVERTENCIA: La información DCV es menor a la Fecha de Solicitud.'
                    }
                }


                if (!confirm(mensaje)) {
                    return false;
                }
                else {
                    return true;
                }
            });
        }

        function seteaBotonModificar() {
            $("#<%=btnModalModificar.ClientID %>").unbind("click");
            $("#<%=btnModalModificar.ClientID %>").click(function () {
                var cuotasDCV = $('#<%=txtModalCuotasDVC.ClientID%>').val();

                if (cuotasDCV == "" || cuotasDCV == "0") {
                    alert("El rescate debe poseer cuotas DCV.")
                    return false;
                }

                if (!confirm('¿Confirma que desea Modificar?')) {
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

        function enableDisableButtonsProrrotear(newValue) {
            var btnProrrotear = document.getElementById('<%=btnProrrotear.ClientID%>');
            btnProrrotear.disabled = newValue;
        }

        function checkRadioBtn(id) {
            var gv = document.getElementById('<%=GrvTabla.ClientID %>');
            if (gv != null) {
                for (var i = 1; i < gv.rows.length; i++) {
                    var radioBtn = gv.rows[i].cells[0].getElementsByTagName("input");

                    // Check if the id not same
                    if (radioBtn[0].id != id.id) {
                       // radioBtn[0].checked = false;
                    }
                    else {
                        if (!isPerfilConsulta()) {
                            enableDisableButtons(false);
                            enableDisableButtonsProrrotear(false);
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


        function soloNumeros12Decimales(e, field) {
            key = e.keyCode ? e.keyCode : e.which;
            if (key === 8)
                return true;
            if (field.value !== "") {
                if ((field.value.indexOf(",")) > 0) {
                    if (key > 47 && key < 58) {
                        if (field.value === "")
                            return true;
                        //regexp = /[0-9]{1,10}[,][0-9]{1,3}$/;
                        regexp = /[0-9]{12}$/;
                        return !(regexp.test(field.value))
                    }
                }
            }
            if (key > 47 && key < 58) {
                if (field.value === "")
                    return true;
                regexp = /[0-9]{20}/;
                return !(regexp.test(field.value));
            }
            if (key === 44) {
                if (field.value === "")
                    return false;
                regexp = /^[0-9]+$/;
                return regexp.test(field.value);

            }

            return false;
        }

        function validateBtn() {
            var cuotasDCV = $('#<%=txtModalCuotasDVC.ClientID%>').val();

            if (CheckTxtCuotasDCVEmpty(rutTxt)) {
                return true;
            }
            else {
                return false;
            }
        }

        function CheckTxtCuotasDCVEmpty(cuotasDCV) {
            if (cuotasDCV == "" || cuotasDCV == "0") {
                alert("El rescate debe poseer coutas DCV.")
                return false;
            }
            return true;
        }

    </script>

</asp:Content>

