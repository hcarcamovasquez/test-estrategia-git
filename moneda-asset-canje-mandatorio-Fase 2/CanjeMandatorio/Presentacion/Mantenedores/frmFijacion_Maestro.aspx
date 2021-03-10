<%@ Page Title="Fijación de Operaciones" Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false" CodeFile="frmFijacion_Maestro.aspx.vb" Inherits="Presentacion_Mantenedores_frmFijacion_Maestro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:scriptmanager id="ScriptManager1" runat="server" />

    <h2 class="TdRedondeado titleMant">Fijación de<strong> Transacciones</strong></h2>

    <div class="card p-4 jumbotron">

        <div class="row mt-2">
            <!-- LISTA Tipo Transaccion-->
            <div class="col-md-4">
                <asp:label runat="server" id="lblTipoTransaccion">Tipo Transacción</asp:label>
                <asp:dropdownlist id="ddlListaTipoTransaccion" cssclass="form-control js-select2-rut" runat="server" />
            </div>

            <!-- LISTA RUT FONDO-->
            <div class="col-md-4">
                <asp:label runat="server" id="rutfondo">Fondo</asp:label>
                <asp:dropdownlist id="ddlListaRutFondo" cssclass="form-control js-select2-rut" runat="server" />
            </div>
            <!-- NEMOTÉCNICO -->
            <div class="col-md-4">
                <asp:label runat="server" id="nemotecnico">Nemotécnico</asp:label>
                <asp:dropdownlist id="ddlListaNemotecnico" cssclass="form-control js-select2-rut" runat="server" />
            </div>
        </div>

        <div class="row mt-2">
            <!-- FIJACIÓN NAV -->
            <div class="col-md-3">
                <asp:label runat="server" id="Label6">Fijación NAV</asp:label>
                <asp:dropdownlist id="ddlFijacionNav" cssclass="form-control js-select2-rut" runat="server" />
            </div>

            <!-- FECHA NAV DESDE -->
            <div class="col-md-3">
                <asp:label runat="server" id="Label4">Fecha NAV Desde</asp:label>
                <div class="input-group">
                    <asp:textbox id="txtNAVDesde" runat="server" cssclass="form-control datepicker" readonly="True"></asp:textbox>

                    <asp:linkbutton id="lnkBtnNAVDesde" class="btn btn-moneda" runat="server" onclientclick="return clickCalendar('txtNAVDesde')"><i class="far fa-calendar-alt"></i></asp:linkbutton>
                    <asp:linkbutton id="BtnLimpiarFechaDesde" text="" onclientclick="return limpiarCalendar('txtNAVDesde')" class="btn btn-secondary ml-1" runat="server">
                        <i class="far fa-trash-alt"></i></asp:linkbutton>

                </div>
            </div>

            <!-- FECHA NAV HASTA -->
            <div class="col-md-3">
                <asp:label runat="server" id="Label7">Fecha NAV Hasta</asp:label>
                <div class="input-group">
                    <asp:textbox id="txtNAVHasta" runat="server" cssclass="form-control datepicker" readonly="True"></asp:textbox>
                    <asp:linkbutton id="lnkBtnNAVHasta" class="btn btn-moneda" runat="server" onclientclick="return clickCalendar('txtNAVHasta')">
                        <i class="far fa-calendar-alt"></i>
                    </asp:linkbutton>
                    <asp:linkbutton id="LinkButton5" text="" onclientclick="return limpiarCalendar('txtNAVHasta')" class="btn btn-secondary ml-1" runat="server"><i class="far fa-trash-alt"></i></asp:linkbutton>

                </div>
            </div>

            <!-- BOTÓN FIJAR NAV -->
            <div class="col-md-3 text-center">
                <asp:button id="BtnFijarNav" text="Fijar NAV" class="btn btn-info mt-4 BtnFijarNav" runat="server" enabled="false" onclientclick="return validateBtn();" />
            </div>

        </div>

        <div class="row mt-2">
            <!-- FIJACIÓN TC OBSERVADO-->
            <div class="col-md-3">
                <asp:label runat="server" id="Label8">Fijación TC Observado</asp:label>
                <asp:dropdownlist id="ddlFijacionTCObservacion" cssclass="form-control js-select2-rut" runat="server" />
            </div>
            <!-- FECHA TC OBSERVACION DESDE -->
            <div class="col-md-3">
                <asp:label runat="server" id="Label2">Fecha TC Obser. Desde</asp:label>
                <div class="input-group">
                    <asp:textbox id="txtFechaTCDesde" runat="server" cssclass="form-control datepicker" readonly="True"></asp:textbox>
                    <asp:linkbutton id="LinkButton1" class="btn btn-moneda" runat="server" onclientclick="return clickCalendar('txtFechaTCDesde')"><i class="far fa-calendar-alt"></i></asp:linkbutton>
                    <asp:linkbutton id="LinkButton6" text="" class="btn btn-secondary ml-1" onclientclick="return limpiarCalendar('txtFechaTCDesde')" runat="server"><i class="far fa-trash-alt"></i></asp:linkbutton>

                </div>
            </div>

            <!-- FECHA TC OBSERVACION HASTA -->
            <div class="col-md-3">
                <asp:label runat="server" id="Label3">Fecha TC Obser. Hasta</asp:label>
                <div class="input-group">
                    <asp:textbox id="txtFechaTCHasta" runat="server" cssclass="form-control datepicker" readonly="True"></asp:textbox>
                    <asp:linkbutton id="LinkButton2" class="btn btn-moneda" runat="server"
                        onclientclick="return clickCalendar('txtFechaTCHasta')"><i class="far fa-calendar-alt"></i></asp:linkbutton>
                    <asp:linkbutton id="LinkButton11" text="" class="btn btn-secondary ml-1" onclientclick="return limpiarCalendar('txtFechaTCHasta')" runat="server"><i class="far fa-trash-alt"></i></asp:linkbutton>


                </div>
            </div>
            <!-- BOTÓN TC OBS -->
            <div class="col-md-3 text-center">
                <asp:button id="BtnTCObs" text="TC Obser." class="btn btn-info mt-4 BtnTCObs" runat="server" enabled="false" onclientclick="return validateBtn();" />
            </div>
        </div>

        <div class="row mt-2">
            <div class="col-md-3">
                <asp:label runat="server" id="lblIntencion">Estado de Confirmación</asp:label>
                <asp:dropdownlist id="ddlEstadoConfirmacion" cssclass="form-control js-select2-rut" runat="server" autopostback="false">
                    <asp:ListItem Value="&nbsp;">&nbsp;</asp:ListItem>
                    <asp:ListItem Value="Intencion">Intención</asp:ListItem>
                    <asp:ListItem Value="Confirmada">Confirmada</asp:ListItem>
                </asp:dropdownlist>
            </div>
            <!-- FECHA TC OBSERVACION DESDE -->
            <div class="col-md-3">
                <asp:label runat="server" id="Label37">Fecha pago desde</asp:label>
                <div class="input-group">
                    <asp:textbox id="txtFechaPagoDesde" runat="server" cssclass="form-control datepicker" readonly="True"></asp:textbox>
                    <asp:linkbutton id="LinkButton3" class="btn btn-moneda" runat="server" onclientclick="return clickCalendar('txtFechaPagoDesde')"><i class="far fa-calendar-alt"></i></asp:linkbutton>
                    <asp:linkbutton id="LinkButton4" text="" class="btn btn-secondary ml-1" onclientclick="return limpiarCalendar('txtFechaPagoDesde')" runat="server"><i class="far fa-trash-alt"></i></asp:linkbutton>

                </div>
            </div>

            <!-- FECHA TC OBSERVACION HASTA -->
            <div class="col-md-3">
                <asp:label runat="server" id="Label38">Fecha pago Hasta</asp:label>
                <div class="input-group">
                    <asp:textbox id="txtFechaPagoHasta" runat="server" cssclass="form-control datepicker" readonly="True"></asp:textbox>
                    <asp:linkbutton id="LinkButton12" class="btn btn-moneda" runat="server"
                        onclientclick="return clickCalendar('txtFechaPagoHasta')"><i class="far fa-calendar-alt"></i></asp:linkbutton>
                    <asp:linkbutton id="LinkButton13" text="" class="btn btn-secondary ml-1"
                        onclientclick="return limpiarCalendar('txtFechaPagoHasta')" runat="server"><i class="far fa-trash-alt"></i></asp:linkbutton>

                </div>
            </div>
            <!-- BOTÓN TC OBS -->
            <div class="col-md-3 text-center">
                <asp:button id="btnConfirmar" text="Confirmar" class="btn btn-info mt-4 BtnTCObs" runat="server" enabled="false" visible="true" OnClientClick="return validateBotonConfirmar();" />
            </div>
        </div>
        <div class="row mt-4">
            <div class="col-md-3"></div>
            <div class="col-md-3"></div>
            <div class="col-md-3"></div>
            <div class="col-md-3 text-center">
                <asp:button id="btnMoverIntencion" text="Mover Intención" class="btn btn-info mt-4 BtnTCObs" runat="server" enabled="false" visible="true" />
            </div>
        </div>

        <!-- BOTONES BUSCAR LIMPIAR-->
        <div class="row text-center mt-5 p-3 border-bottom">
            <div class="col-md-12">
                <!-- BOTÓN BUSCAR -->
                <asp:button id="BtnBuscar" text="Buscar" class="btn btn-moneda" runat="server" />
                <asp:button id="btnLimpiarFrm" text="Borrar" class="btn btn-secondary" runat="server" onclick="btnLimpiarFrm_Click" />

            </div>
        </div>
        
        <asp:hiddenfield id="txtAccionHidden" runat="server" />

        <!-- TABLA DE RESULTADOS -->
        <div class="row mt-2">
            <div class="col-md-8">
                <h5 class="mt-3 text-secondary"><i class="fas fa-file-invoice fa-sm"></i>Resultado de la búsqueda</h5>
            </div>
            <div class="col-md-4">
                <asp:button id="btnImprimir" text="Imprimir" class="btn btn-moneda btnImprimir" runat="server" enabled="false" />
            </div>
        </div>

        <div class="table-responsive card p-3">
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
                        <HeaderTemplate>
                            Sel.
                                <asp:CheckBox ID="checkFijacionAll" CssClass="checkFijacionAll" runat="server" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="RowSelector" CssClass="checkFijacion" runat="server" onclick="checkRadioBtn(this);" />
                            <%-- <asp:CheckBox ID="RowSelector" CssClass="checkFijacion" runat="server" />--%>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:BoundField DataField="ID" HeaderText="ID" />
                    <asp:BoundField DataField="TipoTransaccion" HeaderText="Tipo de Transacción" />
                    <asp:BoundField DataField="APRUT" HeaderText="RUT Aportante" />
                    <asp:BoundField DataField="RazonSocial" HeaderText="Nombre/Razón Social" />
                    <asp:BoundField DataField="RUT" HeaderText="RUT del Fondo" />
                    <asp:BoundField DataField="FNNombreCorto" HeaderText="Nombre del Fondo" />
                    <asp:BoundField DataField="FechaNav" HeaderText="Fecha NAV" DataFormatString="{0:dd-MM-yyyy}" ItemStyle-HorizontalAlign="center"/>
                    <asp:BoundField DataField="FechaTCObs" HeaderText="Fecha TC Observado" DataFormatString="{0:dd-MM-yyyy}" ItemStyle-HorizontalAlign="center"/>
                    <asp:BoundField DataField="fechaPago" HeaderText="Fecha de Pago"  DataFormatString="{0:dd-MM-yyyy}" ItemStyle-HorizontalAlign="center"/>
                    <asp:BoundField DataField="NAV_FIJADO" HeaderText="NAV Fijado"  DataFormatString="{0:N6}" ItemStyle-HorizontalAlign="Right"/>
                    <asp:BoundField DataField="TC_OBSERVADO" HeaderText="TC Observado" DataFormatString="{0:N6}" ItemStyle-HorizontalAlign="Right"/>

                    <asp:BoundField DataField="EstadoIntencion" HeaderText="Estado de Confirmación" ItemStyle-HorizontalAlign="Left"/>

                    <asp:BoundField DataField="FijacionNAV" HeaderText="Fijación NAV" />
                    <asp:BoundField DataField="FijacionTCObservado" HeaderText="Fijación TC Observado" />                    
                    <asp:BoundField DataField="Nemotecnico" HeaderText="Nemotécnico" />
                    <asp:BoundField DataField="MonedaPago" HeaderText="Moneda Pago" />
                    <asp:BoundField DataField="Cuotas" HeaderText="Cuotas" DataFormatString="{0:N0}" ItemStyle-HorizontalAlign="Right"/>
                    <asp:BoundField DataField="Monto" HeaderText="Monto" DataFormatString="{0:N6}" ItemStyle-HorizontalAlign="Right"/>
                </Columns>
            </asp:gridview>
        </div>
        <div class="row mt-3">
            <div class="col-md-12 text-center">
                <asp:button id="BtnModificar" runat="server" class="btn btn-info btnmodificar" text="Fijación Manual" enabled="false"></asp:button>
                <asp:button id="BtnExportar" class="btn btn-success" text="Exportar" runat="server" enabled="false" />
                <asp:button id="BtnEliminar" class="btn btn-success btnEliminar" text="Eliminar" runat="server" enabled="false" visible="true" />
            </div>
        </div>

    </div>

    <!-- Bootstrap Modal Dialog Suscripcione Crear/Modificar -->
    <div class="modal fade" id="myModalSuscripcion" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" data-keyboard="false" data-backdrop="static">
        <div class="modal-dialog" style="max-width: 90%;">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title">
                        <asp:label id="lbModalTittle" runat="server" text="Formulario - Suscripciones" font-bold="true" font-size="X-Large">
                        </asp:label>
                    </h4>
                    <button id="btnXCerrar" type="button" class="close" data-dismiss="modal" aria-label="Close" runat="server"><span aria-hidden="true">&times;</span></button>
                </div>
                <div class="modal-body">
                    <div class="jumbotron p-3">
                        <!-- FORMULARIO-->
                        <asp:hiddenfield id="txtEstadoCambio" runat="server"></asp:hiddenfield>
                        <div class="col-md-12 mx-auto mt-10 " style="margin-top: 30px" visible="false">
                            <div class="col-md-12 mx-auto d-flex p-0 mb-10 justify-content-between" style="margin-top: 30px">
                                <!-- TARJETA 1 -->

                                <div class="card mt-0 col-md-6">
                                    <div class="card-body">
                                        <div class="row mt-3">
                                            <div class="col-md-6">
                                                <label class="form-control-label">Id Suscripción</label>
                                                <asp:textbox id="txtIdSuscripcion" runat="server" cssclass="form-control form-control-sm" readonly="true"></asp:textbox>
                                            </div>
                                            <div class="col-md-6">
                                                <label class="form-control-label">RUT Aportante</label>
                                                <asp:updatepanel runat="server" id="UpdatePanel6" updatemode="Conditional">
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="ddlModalNombreAportante" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddlModalMultifondo" EventName="SelectedIndexChanged" />
                                                    </Triggers>
                                                    <ContentTemplate>
                                                        <asp:DropDownList ID="ddlModalRutAportante" CssClass="form-control js-select2-rut" runat="server" OnSelectedIndexChanged="CargarNombreAportanteNemotecnicoPorRutAportanteModal" AutoPostBack="true" />
                                                    </ContentTemplate>
                                                </asp:updatepanel>
                                            </div>
                                        </div>
                                        <div class="row mt-3">
                                            <div class="col-md-6">
                                                <label class="form-control-label">Nombre Aportante</label>
                                                <asp:updatepanel runat="server" id="UpdatePanel2" updatemode="Conditional">
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="ddlModalRutAportante" EventName="" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddlModalMultifondo" EventName="" />
                                                    </Triggers>
                                                    <ContentTemplate>
                                                        <asp:DropDownList ID="ddlModalNombreAportante" CssClass="form-control js-select2-rut" runat="server" OnSelectedIndexChanged="CargarRutAportanteNemotecnicoPorNombreAportanteModal" AutoPostBack="true" />
                                                    </ContentTemplate>
                                                </asp:updatepanel>
                                            </div>
                                            <div class="col-md-6">
                                                <label class="form-control-label">Multifondo</label>
                                                <asp:updatepanel runat="server" id="UpdatePanel7" updatemode="Conditional">
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="ddlModalRutAportante" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddlModalNombreAportante" EventName="SelectedIndexChanged" />
                                                    </Triggers>
                                                    <ContentTemplate>
                                                        <asp:DropDownList ID="ddlModalMultifondo" CssClass="form-control js-select2-rut" runat="server" OnSelectedIndexChanged="CargarRutYRazonSocialPorMultifondo" AutoPostBack="true" />
                                                    </ContentTemplate>
                                                </asp:updatepanel>
                                            </div>
                                        </div>

                                        <div class="row mt-3">

                                            <div class="col-md-6">
                                                <asp:label runat="server" id="Label10">Rut Fondo</asp:label>
                                                <asp:updatepanel runat="server" id="UpdatePanel9" updatemode="Conditional">
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="ddlNemotecnico" EventName="SelectedIndexChanged" />
                                                    </Triggers>
                                                    <ContentTemplate>
                                                        <asp:DropDownList ID="ddlFondo" CssClass="form-control js-select2-rut" runat="server" OnSelectedIndexChanged="SelectedIndexChangedFnRut" AutoPostBack="true" />
                                                    </ContentTemplate>
                                                </asp:updatepanel>
                                            </div>
                                            <div class="col-md-6">
                                                <asp:label runat="server" id="Label27">Nombre Fondo</asp:label>
                                                <asp:updatepanel runat="server" id="UpdatePanel25" updatemode="Conditional">
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="ddlNemotecnico" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddlFondo" EventName="SelectedIndexChanged" />
                                                    </Triggers>
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtNombreFondo" CssClass="form-control" runat="server" ReadOnly="true" />
                                                    </ContentTemplate>
                                                </asp:updatepanel>
                                            </div>
                                        </div>
                                        <div class="row mt-3">
                                            <div class="col-md-6">
                                                <asp:label runat="server" id="LbNemotecnico">Nemotécnico</asp:label>
                                                <asp:updatepanel runat="server" id="UpdatePanel8" updatemode="Conditional">
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="ddlFondo" EventName="SelectedIndexChanged" />
                                                    </Triggers>
                                                    <ContentTemplate>
                                                        <asp:DropDownList ID="ddlNemotecnico" CssClass="form-control js-select2-rut" runat="server" OnSelectedIndexChanged="LlenarPorNemotecnico" AutoPostBack="true" />
                                                    </ContentTemplate>
                                                </asp:updatepanel>
                                            </div>
                                            <div class="col-md-6">
                                                <asp:label runat="server" id="Label28">Serie</asp:label>
                                                <asp:updatepanel runat="server" id="UpdatePanel26" updatemode="Conditional">
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="ddlNemotecnico" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddlFondo" EventName="SelectedIndexChanged" />
                                                    </Triggers>
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtNombreSerie" CssClass="form-control" runat="server" ReadOnly="true" />
                                                    </ContentTemplate>
                                                </asp:updatepanel>
                                            </div>
                                        </div>
                                        <div class="row mt-3">
                                            <div class="col-md-6">
                                                <asp:label runat="server" id="Label29">Moneda Serie</asp:label>
                                                <asp:updatepanel runat="server" id="UpdatePanel27" updatemode="Conditional">
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="ddlNemotecnico" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddlFondo" EventName="SelectedIndexChanged" />
                                                    </Triggers>
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtMonedaSerie" CssClass="form-control" runat="server" ReadOnly="true" />
                                                    </ContentTemplate>
                                                </asp:updatepanel>
                                            </div>
                                            <div class="col-md-6">
                                                <asp:label runat="server" id="lblMonedaPago">Moneda pago</asp:label>
                                                <asp:dropdownlist id="ddlMonedaPago" cssclass="form-control js-select2-rut" runat="server" />
                                            </div>
                                        </div>

                                        <div class="row mt-3">
                                            <div class="col-md-6">
                                                <asp:label runat="server" id="lbNAV">NAV</asp:label>
                                                <asp:updatepanel runat="server" id="UpdatePanel19" updatemode="Conditional">
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="ddlNemotecnico" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddlFondo" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtMonto" EventName="TextChanged" />
                                                    </Triggers>
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtNAV" CssClass="form-control dbs-entero20-decimal6" runat="server" OnTextChanged="CuotasTextChanged" AutoPostBack="true" />
                                                    </ContentTemplate>
                                                </asp:updatepanel>
                                            </div>
                                            <div class="col-md-6">
                                                <asp:label runat="server" id="Label11">NAV (CLP)</asp:label>
                                                <asp:updatepanel runat="server" id="UpdatePanel23" updatemode="Conditional">
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="ddlNemotecnico" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddlFondo" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddlEstado1" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtMonto" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtCuotas" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtNAV" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtTcObservado" EventName="TextChanged" />
                                                    </Triggers>
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtNAVCLP" CssClass="form-control dbs-entero20-decimal4" runat="server" ReadOnly="true" />
                                                    </ContentTemplate>
                                                </asp:updatepanel>
                                            </div>
                                        </div>

                                    </div>
                                </div>
                                <!-- TARJETA 2 -->
                                <div class="card mt- d-flex col-md-6 justify-content-between">
                                    <div class="card-body ">
                                        <div class="row mt-3">
                                            <div class="col-md-6">
                                                <asp:label runat="server" id="Label15">Fecha intención</asp:label>
                                                <asp:updatepanel runat="server" id="UpdatePanel3" updatemode="Conditional">
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="LinkButton9" EventName="click" />
                                                    </Triggers>
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtFechaIntencion" runat="server" CssClass="form-control datepicker" ReadOnly="True" OnClick="LinkButton9_Click"></asp:TextBox>
                                                        <asp:Calendar ID="Calendar9" runat="server" Visible="False" BackColor="White" BorderColor="White" BorderWidth="1px" Font-Names="Verdana" Font-Size="9pt" ForeColor="Black" Height="190px" NextPrevFormat="FullMonth" Width="350px" class="calendarios">
                                                            <DayHeaderStyle Font-Bold="True" Font-Size="8pt" />
                                                            <NextPrevStyle Font-Bold="True" Font-Size="8pt" ForeColor="#333333" VerticalAlign="Bottom" />
                                                            <OtherMonthDayStyle ForeColor="#999999" />
                                                            <SelectedDayStyle BackColor="#333399" ForeColor="White" />
                                                            <TitleStyle BackColor="White" BorderColor="Black" BorderWidth="4px" Font-Bold="True" Font-Size="12pt" ForeColor="#333399" />
                                                            <TodayDayStyle BackColor="#CCCCCC" />
                                                        </asp:Calendar>
                                                        <asp:LinkButton ID="LinkButton9" Visible="false" Class="btn btn-secondary mt-1 btn-sm w-100" OnClick="LinkButton9_Click" runat="server">Elegir Fecha</asp:LinkButton>
                                                        <span id="reqtxtFecha3" class="reqError"></span>
                                                    </ContentTemplate>
                                                </asp:updatepanel>
                                            </div>
                                            <div class="col-md-6">
                                                <asp:label runat="server" id="Label5">Fecha NAV</asp:label>
                                                <asp:updatepanel runat="server" id="UpdatePanel1" updatemode="Conditional">
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="LinkButton7" EventName="click" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddlNemotecnico" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddlFondo" EventName="SelectedIndexChanged" />
                                                    </Triggers>
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtFechaNAV" runat="server" CssClass="form-control datepicker" ReadOnly="True"></asp:TextBox>
                                                        <asp:Calendar ID="Calendar7" runat="server" Visible="False" BackColor="White" BorderColor="White" BorderWidth="1px" Font-Names="Verdana" Font-Size="9pt" ForeColor="Black" Height="190px" NextPrevFormat="FullMonth" Width="350px" class="calendarios">
                                                            <DayHeaderStyle Font-Bold="True" Font-Size="8pt" />
                                                            <NextPrevStyle Font-Bold="True" Font-Size="8pt" ForeColor="#333333" VerticalAlign="Bottom" />
                                                            <OtherMonthDayStyle ForeColor="#999999" />
                                                            <SelectedDayStyle BackColor="#333399" ForeColor="White" />
                                                            <TitleStyle BackColor="White" BorderColor="Black" BorderWidth="4px" Font-Bold="True" Font-Size="12pt" ForeColor="#333399" />
                                                            <TodayDayStyle BackColor="#CCCCCC" />
                                                        </asp:Calendar>
                                                        <asp:LinkButton ID="LinkButton7" Visible="false" Class="btn btn-secondary mt-1 btn-sm w-100" OnClick="LinkButton7_Click" runat="server">Elegir Fecha</asp:LinkButton>
                                                        <span id="reqtxtFecha1" class="reqError"></span>
                                                    </ContentTemplate>
                                                </asp:updatepanel>
                                            </div>
                                        </div>
                                        <div class="row mt-3">
                                            <div class="col-md-6">
                                                <asp:label runat="server" id="Label1">Fecha suscripción</asp:label>
                                                <asp:updatepanel runat="server" id="UpdatePanel4" updatemode="Conditional">
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="LinkButton10" EventName="click" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddlNemotecnico" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddlFondo" EventName="SelectedIndexChanged" />
                                                    </Triggers>
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtFechaSuscripcion" runat="server" CssClass="form-control datepicker" ReadOnly="True"></asp:TextBox>
                                                        <asp:Calendar ID="Calendar10" runat="server" Visible="False" BackColor="White" BorderColor="White" BorderWidth="1px" Font-Names="Verdana" Font-Size="9pt" ForeColor="Black" Height="190px" NextPrevFormat="FullMonth" Width="350px" class="calendarios">
                                                            <DayHeaderStyle Font-Bold="True" Font-Size="8pt" />
                                                            <NextPrevStyle Font-Bold="True" Font-Size="8pt" ForeColor="#333333" VerticalAlign="Bottom" />
                                                            <OtherMonthDayStyle ForeColor="#999999" />
                                                            <SelectedDayStyle BackColor="#333399" ForeColor="White" />
                                                            <TitleStyle BackColor="White" BorderColor="Black" BorderWidth="4px" Font-Bold="True" Font-Size="12pt" ForeColor="#333399" />
                                                            <TodayDayStyle BackColor="#CCCCCC" />
                                                        </asp:Calendar>
                                                        <asp:LinkButton ID="LinkButton10" Visible="false" Class="btn btn-secondary mt-1 btn-sm w-100" OnClick="LinkButton10_Click" runat="server">Elegir Fecha</asp:LinkButton>
                                                        <span id="reqtxtFecha8" class="reqError"></span>
                                                    </ContentTemplate>
                                                </asp:updatepanel>
                                            </div>
                                            <div class="col-md-6">
                                                <asp:label runat="server" id="Label13">Fecha TC Obs</asp:label>
                                                <asp:updatepanel runat="server" id="UpdatePanel5" updatemode="Conditional">
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="LinkButton8" EventName="click" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddlNemotecnico" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddlFondo" EventName="SelectedIndexChanged" />
                                                    </Triggers>
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtFechaTC" runat="server" CssClass="form-control datepicker" ReadOnly="True"></asp:TextBox>
                                                        <asp:Calendar ID="Calendar8" runat="server" Visible="False" BackColor="White" BorderColor="White" BorderWidth="1px" Font-Names="Verdana" Font-Size="9pt" ForeColor="Black" Height="190px" NextPrevFormat="FullMonth" Width="350px" class="calendarios">
                                                            <DayHeaderStyle Font-Bold="True" Font-Size="8pt" />
                                                            <NextPrevStyle Font-Bold="True" Font-Size="8pt" ForeColor="#333333" VerticalAlign="Bottom" />
                                                            <OtherMonthDayStyle ForeColor="#999999" />
                                                            <SelectedDayStyle BackColor="#333399" ForeColor="White" />
                                                            <TitleStyle BackColor="White" BorderColor="Black" BorderWidth="4px" Font-Bold="True" Font-Size="12pt" ForeColor="#333399" />
                                                            <TodayDayStyle BackColor="#CCCCCC" />
                                                        </asp:Calendar>
                                                        <asp:LinkButton ID="LinkButton8" Visible="false" Class="btn btn-secondary mt-1 btn-sm w-100" OnClick="LinkButton8_Click" runat="server">Elegir Fecha</asp:LinkButton>
                                                        <span id="reqtxtFecha" class="reqError"></span>
                                                    </ContentTemplate>
                                                </asp:updatepanel>
                                            </div>
                                        </div>
                                        <div class="row mt-3">
                                            <div class="col-md-6">
                                                <asp:label runat="server" id="lbTC">TC Observado</asp:label>
                                                <asp:updatepanel runat="server" id="UpdatePanel20" updatemode="Conditional">
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="ddlNemotecnico" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddlFondo" EventName="SelectedIndexChanged" />
                                                    </Triggers>
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtTCObservado" CssClass="form-control dbs-entero20-decimal6" runat="server" OnTextChanged="TcObservadoChanged" AutoPostBack="true" />
                                                    </ContentTemplate>
                                                </asp:updatepanel>
                                            </div>
                                            <div class="col-md-6">
                                                <asp:label runat="server" id="lbCuotas">Cuotas</asp:label>
                                                <asp:updatepanel runat="server" id="UpdatePanel24" updatemode="Conditional">
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="ddlNemotecnico" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddlFondo" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtMonto" EventName="TextChanged" />
                                                    </Triggers>
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtCuotas" CssClass="form-control dbs-entero-decimal" runat="server" OnTextChanged="CuotasTextChanged" AutoPostBack="true" AutoComplete="off" />
                                                    </ContentTemplate>
                                                </asp:updatepanel>
                                            </div>
                                        </div>

                                        <div class="row mt-3">
                                            <div class="col-md-6">
                                                <asp:label runat="server" id="Label14">Monto</asp:label>
                                                <asp:updatepanel runat="server" id="UpdatePanel21" updatemode="Conditional">
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="ddlNemotecnico" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddlFondo" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtNAV" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtCuotas" EventName="TextChanged" />
                                                    </Triggers>
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtMonto" CssClass="form-control dbs-entero20-decimal2" runat="server" OnTextChanged="MontoTextChanged" AutoPostBack="true" AutoComplete="off" />
                                                    </ContentTemplate>
                                                </asp:updatepanel>
                                            </div>
                                            <div class="col-md-6">
                                                <asp:label runat="server" id="Label16">Monto (CLP)</asp:label>
                                                <asp:updatepanel runat="server" id="UpdatePanel16" updatemode="Conditional">
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="ddlNemotecnico" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddlFondo" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddlEstado1" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtMonto" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtNAV" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtCuotas" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtTcObservado" EventName="TextChanged" />
                                                    </Triggers>
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtMontoCLP" CssClass="form-control dbs-entero20-decimal0" runat="server" ReadOnly="True" />
                                                    </ContentTemplate>
                                                </asp:updatepanel>
                                            </div>
                                        </div>
                                        <div class="row mt-3">
                                            <div class="col-md-6">
                                                <asp:label runat="server" id="Label17">Contrato</asp:label>
                                                <asp:dropdownlist id="ddlContrato" cssclass="form-control js-select2-rut" runat="server" />
                                            </div>
                                            <div class="col-md-6">
                                                <asp:label runat="server" id="lbPoderes">Poderes</asp:label>
                                                <asp:dropdownlist id="ddlPoderes" cssclass="form-control js-select2-rut" runat="server" />
                                            </div>
                                        </div>
                                         <%-- Estado de Confirmacion  --%>
                                        <div class="row mt-3">
                                            <div class="col-md-6">
                                                <asp:Label runat="server" ID="Label36">Estado de Confirmación</asp:Label>
                                                <asp:DropDownList ID="ddlEstadoIntencion" CssClass="form-control js-select2-rut" runat="server" AutoPostBack="false">
                                                    <asp:ListItem Value="Intencion">Intención</asp:ListItem>
                                                    <asp:ListItem Value="Confirmada">Confirmada</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>


                            <div class="col-md-12 mx-auto d-flex p-0 mb-10 justify-content-between" style="margin-bottom: 30px">
                                <!-- TARJETA 3 -->
                                <div class="card mt-0 col-md-4">
                                    <div class="card-body">

                                        <div class="row mt-3">
                                            <div class="col-md-6">
                                                <asp:label runat="server" id="Label12">Fecha DCV</asp:label>
                                            </div>
                                            <div class="col-md-6">
                                                <asp:updatepanel runat="server" id="UpdatePanel15" updatemode="Conditional">
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="ddlNemotecnico" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddlFondo" EventName="SelectedIndexChanged" />
                                                    </Triggers>
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtFechaDCV" CssClass="form-control form-control-sm" runat="server" ReadOnly="True" />
                                                    </ContentTemplate>
                                                </asp:updatepanel>
                                            </div>
                                        </div>
                                        <div class="row mt-3">
                                            <div class="col-md-6">
                                                <asp:label runat="server" id="Label18">Estado</asp:label>
                                            </div>
                                            <div class="col-md-6">
                                                <asp:updatepanel runat="server" id="UpdatePanel22" updatemode="Conditional">
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="ddlNemotecnico" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddlFondo" EventName="SelectedIndexChanged" />
                                                    </Triggers>
                                                    <ContentTemplate>
                                                        <asp:DropDownList ID="ddlEstado1" CssClass="form-control js-select2-rut" runat="server" SelectedIndexChanged="llenarCLP" />
                                                    </ContentTemplate>
                                                </asp:updatepanel>

                                            </div>
                                        </div>
                                        <div class="row mt-3">
                                            <div class="col-md-6">
                                                <asp:label runat="server" id="Label19">Observaciones</asp:label>
                                            </div>
                                            <div class="col-md-6">
                                                <asp:textbox id="txtObservaciones" cssclass="form-control form-control-sm" runat="server" />
                                            </div>
                                        </div>
                                        <div class="row mt-3">
                                            <div class="col-md-6">
                                                <asp:label runat="server" id="Label20">Fijación NAV</asp:label>
                                            </div>
                                            <div class="col-md-6">
                                                <asp:updatepanel runat="server" id="UpdatePanel17" updatemode="Conditional">
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="ddlNemotecnico" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddlFondo" EventName="SelectedIndexChanged" />
                                                    </Triggers>
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtFijacionNAV" Text="" CssClass="form-control form-control-sm" runat="server" ReadOnly="True" />
                                                    </ContentTemplate>
                                                </asp:updatepanel>
                                            </div>
                                        </div>
                                        <div class="row mt-3">
                                            <div class="col-md-6">
                                                <asp:label runat="server" id="Label21">Fijación TC Obs</asp:label>
                                            </div>
                                            <div class="col-md-6">
                                                <asp:updatepanel runat="server" id="UpdatePanel18" updatemode="Conditional">
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="ddlNemotecnico" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddlFondo" EventName="SelectedIndexChanged" />
                                                    </Triggers>
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtFijacionTCObs" Text="" CssClass="form-control form-control-sm" runat="server" ReadOnly="True" />
                                                    </ContentTemplate>
                                                </asp:updatepanel>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <!-- TARJETA 4 -->

                                <div class="card mt-0 col-md-4">
                                    <div class="card-body">
                                        <h6 class="card-title">Cuotas emitidas</h6>
                                        <hr />

                                        <div class="row mt-3">
                                            <div class="col-md-6">
                                                <asp:label runat="server" id="Label34">Cuotas emitidas</asp:label>

                                                <asp:updatepanel runat="server" id="UpdatePanel89" updatemode="Conditional">
                                                    <Triggers>
                                                    </Triggers>
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtCuotasEmitidas" CssClass="form-control dbs-entero16-decimal0" runat="server" ReadOnly="true" EventName="SelectionChanged" />
                                                    </ContentTemplate>
                                                </asp:updatepanel>
                                            </div>
                                            <div class="col-md-6">
                                                <asp:label runat="server" id="Label31">Acumulada</asp:label>

                                                <asp:updatepanel runat="server" id="UpdatePanel90" updatemode="Conditional">
                                                    <Triggers>
                                                    </Triggers>
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtAcumulada" CssClass="form-control dbs-entero16-decimal0" runat="server" ReadOnly="true" EventName="SelectionChanged" />
                                                    </ContentTemplate>
                                                </asp:updatepanel>
                                            </div>
                                        </div>


                                        <div class="row mt-3">
                                            <div class="col-md-6">
                                                <asp:label runat="server" id="Label32">Actual</asp:label>

                                                <asp:updatepanel runat="server" id="UpdatePanel91" updatemode="Conditional">
                                                    <Triggers>
                                                    </Triggers>
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtActual" CssClass="form-control dbs-entero16-decimal0" runat="server" ReadOnly="true" EventName="SelectionChanged" />
                                                    </ContentTemplate>
                                                </asp:updatepanel>
                                            </div>
                                            <div class="col-md-6">
                                                <asp:label runat="server" id="Label33">Utilizado</asp:label>

                                                <asp:updatepanel runat="server" id="UpdatePanel92" updatemode="Conditional">
                                                    <Triggers>
                                                    </Triggers>
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtUtilizado" CssClass="form-control dbs-entero16-decimal0" runat="server" ReadOnly="true" EventName="SelectionChanged" />
                                                    </ContentTemplate>
                                                </asp:updatepanel>
                                            </div>
                                        </div>



                                        <div class="row mt-3">
                                            <div class="col-md-6">
                                                <asp:label runat="server" id="Label35">DISPONIBLES</asp:label>

                                                <asp:updatepanel runat="server" id="UpdatePanel93" updatemode="Conditional">
                                                    <Triggers>
                                                    </Triggers>
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtDisponiblesEmitidas" CssClass="form-control dbs-entero16-decimal0" runat="server" ReadOnly="true" EventName="SelectionChanged" />
                                                    </ContentTemplate>
                                                </asp:updatepanel>
                                            </div>
                                        </div>

                                    </div>
                                </div>

                                <!-- TARJETA 5 -->
                                <div class="card mt- d-flex justify-content-between col-md-4">
                                    <div class="card-body">
                                        <h6 class="card-title">Cuotas disponibles</h6>
                                        <hr />

                                        <div class="row mt-3">
                                            <div class="col-md-6">
                                                <asp:label runat="server" id="Label22">Cuotas DCV</asp:label>

                                                <asp:updatepanel runat="server" id="UpdatePanel12" updatemode="Conditional">
                                                    <Triggers>
                                                    </Triggers>
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtCuotasDCV" CssClass="form-control dbs-entero16-decimal0" runat="server" ReadOnly="True" />
                                                    </ContentTemplate>
                                                </asp:updatepanel>
                                            </div>
                                            <div class="col-md-6">
                                                <asp:label runat="server" id="Label23">Rescates</asp:label>

                                                <asp:updatepanel runat="server" id="UpdatePanel10" updatemode="Conditional">
                                                    <Triggers>
                                                    </Triggers>
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtRescates" CssClass="form-control dbs-entero16-decimal0" runat="server" ReadOnly="True" />
                                                    </ContentTemplate>
                                                </asp:updatepanel>
                                            </div>
                                        </div>
                                        <div class="row mt-3">
                                            <div class="col-md-6">
                                                <asp:label runat="server" id="Label24">Suscripciones</asp:label>

                                                <asp:updatepanel runat="server" id="UpdatePanel11" updatemode="Conditional">
                                                    <Triggers>
                                                    </Triggers>
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtSuscripciones" CssClass="form-control dbs-entero16-decimal0" runat="server" ReadOnly="True" />
                                                    </ContentTemplate>
                                                </asp:updatepanel>
                                            </div>
                                            <div class="col-md-6">
                                                <asp:label runat="server" id="Label25">Canje</asp:label>

                                                <asp:updatepanel runat="server" id="UpdatePanel13" updatemode="Conditional">
                                                    <Triggers>
                                                    </Triggers>
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtCanje" CssClass="form-control dbs-entero16-decimal0" runat="server" ReadOnly="True" />
                                                    </ContentTemplate>
                                                </asp:updatepanel>
                                            </div>
                                        </div>
                                        <div class="row mt-3">
                                            <div class="col-md-6">
                                                <asp:label runat="server" id="Label26">DISPONIBLES</asp:label>

                                                <asp:updatepanel runat="server" id="UpdatePanel14" updatemode="Conditional">
                                                    <Triggers>
                                                    </Triggers>
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtDisponibles" CssClass="form-control dbs-entero16-decimal0" runat="server" ReadOnly="True" />
                                                    </ContentTemplate>
                                                </asp:updatepanel>
                                            </div>
                                        </div>

                                    </div>
                                </div>
                            </div>
                        </div>


                        <!-- GRUPO DE BOTONES 2 -->
                        <div class="form-group mt-5 text-center">
                            <div class="col-md-offset-1">
                                <asp:button id="btnModalModificar" text="Modificar" cssclass="btn btn-info" runat="server" onclientclick="if (!confirm('¿Seguro que desea guardar los elementos cambios?')) return false;"></asp:button>
                                <asp:button id="btnModalCancelar" text="Cancelar" cssclass="btn btn-secondary" runat="server" onclientclick="if (!confirm('¿Seguro que desea Cancelar?')) return false;"></asp:button>
                            </div>
                        </div>
                        <!-- FIN GRUPO DE BOTONES 2 -->
                    </div>
                </div>
            </div>
        </div>
    </div>

    <!-- Bootstrap Modal Dialog Canje Crear/Modificar -->
    <div class="modal fade" id="myModalCanje" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" data-keyboard="false" data-backdrop="static">
        <div class="modal-dialog" style="max-width: 98%;">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title">
                        <asp:label id="Label9" runat="server" text="Formulario - Canjes" font-bold="true" font-size="X-Large">
                        </asp:label>
                    </h4>
                    <button id="Button1" type="button" class="close" data-dismiss="modal" aria-label="Close" runat="server"><span aria-hidden="true">&times;</span></button>
                </div>
                <div class="modal-body">
                    <div class="jumbotron p-3">
                        <asp:hiddenfield id="HiddenField1" runat="server"></asp:hiddenfield>
                        <div class="col-md-12 mt-10" visible="false">
                            <div class="col-md-12 d-flex p-0 mb-10">
                                <!-- TARJETA 1 -->
                                <div class="card mt-0 col-md-12">
                                    <div class="card-body">
                                        <div class="row mt-3">
                                            <div class="col-md-1">
                                                <label class="form-control-label">Rut Aportante</label>

                                            </div>
                                            <div class="col-md-3">
                                                <asp:updatepanel runat="server" id="UpdatePanel28" updatemode="Conditional">
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="ddlModalMultifondoCanje" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddlModalNombreAportanteCanje" EventName="SelectedIndexChanged" />
                                                    </Triggers>
                                                    <ContentTemplate>
                                                        <asp:DropDownList ID="ddlModalRutAportanteCanje" CssClass="form-control js-select2-rut" runat="server" AutoPostBack="true" />
                                                    </ContentTemplate>
                                                </asp:updatepanel>

                                            </div>
                                            <div class="col-md-1">
                                                <label class="form-control-label">Nombre Aportante</label>
                                            </div>
                                            <div class="col-md-3">
                                                <asp:updatepanel runat="server" id="UpdatePanel29" updatemode="Conditional">
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="ddlModalRutAportanteCanje" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddlModalMultifondoCanje" EventName="SelectedIndexChanged" />
                                                    </Triggers>
                                                    <ContentTemplate>
                                                        <asp:DropDownList ID="ddlModalNombreAportanteCanje" CssClass="form-control js-select2-rut" runat="server" AutoPostBack="true" />
                                                    </ContentTemplate>
                                                </asp:updatepanel>
                                            </div>
                                            <div class="col-md-1">
                                                <label class="form-control-label">Multifondo</label>
                                            </div>
                                            <div class="col-md-3">
                                                <asp:updatepanel runat="server" id="UpdatePanel30" updatemode="Conditional">
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="ddlModalRutAportanteCanje" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddlModalNombreAportanteCanje" EventName="SelectedIndexChanged" />
                                                    </Triggers>
                                                    <ContentTemplate>
                                                        <asp:DropDownList ID="ddlModalMultifondoCanje" CssClass="form-control js-select2-rut" runat="server" AutoPostBack="true" />
                                                    </ContentTemplate>
                                                </asp:updatepanel>

                                            </div>
                                        </div>
                                        <div class="row mt-3">
                                            <div class="col-md-1">
                                                <label class="form-control-label">Fondo</label>
                                            </div>
                                            <div class="col-md-3">
                                                <asp:updatepanel runat="server" id="UpdatePanel31" updatemode="Conditional">
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="ddlModalNemotecnicoEntranteCanje" EventName="SelectedIndexChanged" />
                                                    </Triggers>
                                                    <ContentTemplate>
                                                        <asp:DropDownList ID="ddlModalFondoCanje" CssClass="form-control js-select2-rut" runat="server" AutoPostBack="true" />
                                                    </ContentTemplate>
                                                </asp:updatepanel>

                                            </div>
                                            <div class="col-md-1">
                                                <label class="form-control-label">Tipo Transacción</label>
                                            </div>
                                            <div class="col-md-3">
                                                <asp:textbox id="txtModalTipoTrnasaccion" cssclass="form-control" runat="server" enabled="false">Canje</asp:textbox>
                                            </div>
                                            <div class="col-md-1">
                                                <label class="form-control-label">Fecha Solicitud</label>
                                            </div>
                                            <div class="col-md-3">
                                                <asp:updatepanel runat="server" id="UpdatePanel32" updatemode="Conditional">
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="lnkBtnModalFechaSolicitud" EventName="click" />
                                                    </Triggers>
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtModalFechaSolicitud" runat="server" CssClass="form-control datepicker" ReadOnly="True"></asp:TextBox>
                                                        <asp:Calendar ID="CalendarModalFechaSolicitud" runat="server" Visible="False" onblur="onblurCalendar(this)" BackColor="White" BorderColor="White" BorderWidth="1px" Font-Names="Verdana" Font-Size="9pt" ForeColor="Black" Height="190px" NextPrevFormat="FullMonth" Width="350px" class="calendarios">
                                                            <DayHeaderStyle Font-Bold="True" Font-Size="8pt" />
                                                            <NextPrevStyle Font-Bold="True" Font-Size="8pt" ForeColor="#333333" VerticalAlign="Bottom" />
                                                            <OtherMonthDayStyle ForeColor="#999999" />
                                                            <SelectedDayStyle BackColor="#333399" ForeColor="White" />
                                                            <TitleStyle BackColor="White" BorderColor="Black" BorderWidth="4px" Font-Bold="True" Font-Size="12pt" ForeColor="#333399" />
                                                            <TodayDayStyle BackColor="#CCCCCC" />
                                                        </asp:Calendar>
                                                        <asp:LinkButton ID="lnkBtnModalFechaSolicitud" Class="btn btn-secondary mt-1 btn-sm w-100" runat="server" Visible="false">Elegir Fecha</asp:LinkButton>
                                                        <span id="reqtxtModalFechaSolicitud" class="reqError"></span>
                                                    </ContentTemplate>
                                                </asp:updatepanel>
                                            </div>
                                        </div>
                                        <div class="row mt-2">
                                            <div class="col-md-1">
                                                <label class="form-control-label">Nombre Fondo</label>
                                            </div>
                                            <div class="col-md-3">

                                                <asp:updatepanel runat="server" id="UpdatePanel33" updatemode="Conditional">
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="ddlModalFondoCanje" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddlModalNemotecnicoEntranteCanje" EventName="SelectedIndexChanged" />
                                                    </Triggers>
                                                    <ContentTemplate>
                                                        <asp:DropDownList ID="ddlModalNombreFondoCanje" CssClass="form-control js-select2-rut" runat="server" />
                                                    </ContentTemplate>
                                                </asp:updatepanel>
                                            </div>
                                            <div class="col-md-1">
                                                <label class="form-control-label">Fijación TC</label>
                                            </div>
                                            <div class="col-md-3">
                                                <asp:updatepanel runat="server" id="UpdatePanel36" updatemode="Conditional">
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="txtModalNavSaliente" EventName="TextChanged" />
                                                    </Triggers>
                                                    <ContentTemplate>
                                                        <asp:DropDownList ID="ddlModalFijacionTC" CssClass="form-control js-select2-rut" runat="server">
                                                            <asp:ListItem Value="">Seleccione una opción</asp:ListItem>
                                                            <asp:ListItem Value="Pendiente">Pendiente</asp:ListItem>
                                                            <asp:ListItem Value="Realizado">Realizado</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </ContentTemplate>
                                                </asp:updatepanel>

                                            </div>

                                            <div class="col-md-1">
                                                <label class="form-control-label">Fecha de Canje</label>
                                            </div>
                                            <div class="col-md-3">
                                                <asp:updatepanel runat="server" id="UpdatePanelFC" updatemode="Conditional">
                                                    <Triggers>
                                                        <%--<asp:AsyncPostBackTrigger ControlID="lnkbtnModalFechaObservado" EventName="click" />
                                                        <asp:AsyncPostBackTrigger ControlID="CalendarModalFechaNavSaliente" EventName="SelectionChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtModalFechaNavSaliente" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddlModalNemotecnicoSaliente" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddlModalNemotecnicoEntrante" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddlModalNemotecnicoEntrante" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtModalFSolicitud" EventName="TextChanged" />--%>
                                                    </Triggers>
                                                    <ContentTemplate>
                                                        <div class="input-group">
                                                            <asp:TextBox ID="txtModalFechaCanje" runat="server" CssClass="form-control datepicker" AutoPostBack="true" Enabled="false"></asp:TextBox>
                                                            <asp:LinkButton ID="lnkbtnModalFechaCanje" Class="btn btn-moneda" runat="server" Visible="false"
                                                                OnClientClick="return clickCalendar('txtModalFechaCanje')"><i class="far fa-calendar-alt"></i></asp:LinkButton>

                                                            <span id="reqtxtModalFechaCanje" class="reqError"></span>
                                                        </div>
                                                    </ContentTemplate>
                                                </asp:updatepanel>
                                            </div>

                                        </div>
                                    </div>
                                </div>
                                <!-- TARJETA 2 -->

                            </div>


                            <div class="col-md-11 mx-auto d-flex p-0 mb-10 justify-content-between">

                                <div class="card mt-0 col-md-6">
                                    <div class="card-body">
                                        <h5 class="card-title">Saliente</h5>
                                        <hr />
                                        <div class="row mt-2">
                                            <div class="col-md-3">
                                                <label class="form-control-label">Nemotécnico</label>
                                            </div>
                                            <div class="col-md-9">
                                                <asp:updatepanel runat="server" id="UpdatePanel35" updatemode="Conditional">
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="ddlModalFondoCanje" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddlModalNemotecnicoEntranteCanje" EventName="SelectedIndexChanged" />
                                                    </Triggers>
                                                    <ContentTemplate>
                                                        <asp:DropDownList ID="ddlModalNemotecnicoSalienteCanje" CssClass="form-control js-select2-rut" runat="server" AutoPostBack="true" />
                                                    </ContentTemplate>
                                                </asp:updatepanel>

                                            </div>
                                        </div>
                                        <div class="row mt-2">
                                            <div class="col-md-3">
                                                <label class="form-control-label">Fecha Nav</label>
                                            </div>
                                            <div class="col-md-9">
                                                <asp:updatepanel runat="server" id="UpdatePanel37" updatemode="Conditional">
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="lnkbtnModalFechaNavSaliente" EventName="click" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddlModalNemotecnicoSalienteCanje" EventName="SelectedIndexChanged" />
                                                    </Triggers>
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtModalFechaNavSaliente" runat="server" CssClass="form-control datepicker" ReadOnly="True"></asp:TextBox>
                                                        <asp:Calendar ID="CalendarModalFechaNavSaliente" runat="server" Visible="False" onblur="onblurCalendar(this)" BackColor="White" BorderColor="White" BorderWidth="1px" Font-Names="Verdana" Font-Size="9pt" ForeColor="Black" Height="190px" NextPrevFormat="FullMonth" Width="350px" class="calendarios">
                                                            <DayHeaderStyle Font-Bold="True" Font-Size="8pt" />
                                                            <NextPrevStyle Font-Bold="True" Font-Size="8pt" ForeColor="#333333" VerticalAlign="Bottom" />
                                                            <OtherMonthDayStyle ForeColor="#999999" />
                                                            <SelectedDayStyle BackColor="#333399" ForeColor="White" />
                                                            <TitleStyle BackColor="White" BorderColor="Black" BorderWidth="4px" Font-Bold="True" Font-Size="12pt" ForeColor="#333399" />
                                                            <TodayDayStyle BackColor="#CCCCCC" />
                                                        </asp:Calendar>
                                                        <asp:LinkButton ID="lnkbtnModalFechaNavSaliente" Class="btn btn-secondary mt-1 btn-sm w-100" runat="server" Height="5%" Visible="false">Elegir Fecha</asp:LinkButton>
                                                        <span id="reqtxtModalFechaNavSaliente" class="reqError"></span>
                                                    </ContentTemplate>
                                                </asp:updatepanel>
                                            </div>
                                        </div>
                                        <div class="row mt-2">
                                            <div class="col-md-2">
                                                <label class="form-control-label">Serie</label>

                                            </div>
                                            <div class="col-md-4">
                                                <asp:updatepanel runat="server" id="UpdatePanel38" updatemode="Conditional">
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="ddlModalNemotecnicoSalienteCanje" EventName="SelectedIndexChanged" />
                                                    </Triggers>
                                                    <ContentTemplate>
                                                        <asp:DropDownList ID="ddlModalSerieSalienteCanje" CssClass="form-control js-select2-rut" runat="server" />
                                                    </ContentTemplate>
                                                </asp:updatepanel>

                                            </div>
                                            <div class="col-md-2">
                                                <label class="form-control-label">Moneda</label>
                                            </div>
                                            <div class="col-md-4">
                                                <asp:updatepanel runat="server" id="UpdatePanel39" updatemode="Conditional">
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="ddlModalNemotecnicoSalienteCanje" EventName="SelectedIndexChanged" />
                                                    </Triggers>
                                                    <ContentTemplate>
                                                        <asp:DropDownList ID="ddlModalMonedaSalienteCanje" CssClass="form-control js-select2-rut" runat="server" />
                                                    </ContentTemplate>
                                                </asp:updatepanel>

                                            </div>
                                        </div>

                                        <div class="row mt-2">
                                            <div class="col-md-2">
                                                <label class="form-control-label">Cuotas</label>

                                            </div>
                                            <div class="col-md-4">
                                                <asp:updatepanel runat="server" id="UpdatePanel94" updatemode="Conditional">
                                                            <Triggers>
                                                                <asp:AsyncPostBackTrigger ControlID="txtModalMontoSaliente" EventName="TextChanged" />
                                                                <asp:AsyncPostBackTrigger ControlID="txtModalNavSaliente" EventName="TextChanged" /> 
                                                        <asp:AsyncPostBackTrigger ControlID="txtModalTipoCambio" EventName="TextChanged" />
                                                            </Triggers>
                                                            <ContentTemplate>
                                                                 <asp:Textbox ID="txtModalCuotaSaliente" CssClass="form-control dbs-entero13-decimal0" onpaste="return false" oncut="return false" oncopy="return false" runat="server" OnTextChanged="CalcularCuotaEntrante" AutoPostBack="true"></asp:Textbox>
                                                            </ContentTemplate>
                                                        </asp:updatepanel>

                                            </div>
                                            <div class="col-md-2">
                                                <label class="form-control-label">Factor</label>

                                            </div>
                                            <div class="col-md-4">
                                                <asp:updatepanel runat="server" id="UpdatePanel40" updatemode="Conditional">
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="ddlModalNemotecnicoSalienteCanje" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtModalNavSaliente" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtModalNavEntrante" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtModalTipoCambio" EventName="TextChanged" />
                                                    </Triggers>
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtFactorSaliente" CssClass="form-control dbs-entero17-decimal9" runat="server"></asp:TextBox>
                                                    </ContentTemplate>
                                                </asp:updatepanel>

                                            </div>

                                        </div>
                                        <div class="row mt-2">
                                            <div class="col-md-2">
                                                <label class="form-control-label">Nav</label>

                                            </div>
                                            <div class="col-md-4">
                                                <asp:updatepanel runat="server" id="UpdatePanel41" updatemode="Conditional">
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="ddlModalNemotecnicoSalienteCanje" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtModalTipoCambio" EventName="TextChanged" />

                                                    </Triggers>
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtModalNavSaliente" OnTextChanged="calcularFactor" AutoPostBack="true" CssClass="form-control dbs-entero20-decimal6" runat="server"> </asp:TextBox>
                                                    </ContentTemplate>
                                                </asp:updatepanel>

                                            </div>
                                            <div class="col-md-2">
                                                <label class="form-control-label">Nav (CLP)</label>

                                            </div>
                                            <div class="col-md-4">
                                                <asp:updatepanel runat="server" id="UpdatePanel42" updatemode="Conditional">
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="ddlModalNemotecnicoSalienteCanje" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtModalNavSaliente" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtModalTipoCambio" EventName="TextChanged" />
                                                    </Triggers>
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtModalNavCLPSaliente" CssClass="form-control dbs-entero20-decimal4" runat="server"></asp:TextBox>
                                                    </ContentTemplate>
                                                </asp:updatepanel>

                                            </div>
                                        </div>
                                        <div class="row mt-2">
                                            <div class="col-md-2">
                                                <label class="form-control-label">Monto</label>

                                            </div>
                                            <div class="col-md-4">
                                                <asp:updatepanel runat="server" id="UpdatePanel43" updatemode="Conditional">
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="txtModalNavSaliente" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtModalCuotaSaliente" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtModalTipoCambio" EventName="TextChanged" />
                                                    </Triggers>
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtModalMontoSaliente" CssClass="form-control dbs-entero20-decimal2" runat="server"></asp:TextBox>
                                                    </ContentTemplate>
                                                </asp:updatepanel>

                                            </div>
                                            <div class="col-md-2">
                                                <label class="form-control-label">Monto (CLP)</label>

                                            </div>
                                            <div class="col-md-4">
                                                <asp:updatepanel runat="server" id="UpdatePanel44" updatemode="Conditional">
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="txtModalNavSaliente" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtModalTipoCambio" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtModalCuotaSaliente" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtModalMontoSaliente" EventName="TextChanged" />
                                                    </Triggers>
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtModalMontoCLPSaliente" CssClass="form-control dbs-entero20-decimal2" runat="server"></asp:TextBox>

                                                    </ContentTemplate>
                                                </asp:updatepanel>

                                            </div>
                                        </div>
                                        <div class="row mt-2">
                                            <div class="col-md-2">
                                                <label class="form-control-label">Diferencia</label>

                                            </div>
                                            <div class="col-md-4">
                                                <asp:updatepanel runat="server" id="UpdatePanel45" updatemode="Conditional">
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="txtModalCuotaSaliente" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtModalNavSaliente" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtModalNavEntrante" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtModalTipoCambio" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtModalMontoSaliente" EventName="TextChanged" />
                                                    </Triggers>
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtModalDiferencia" CssClass="form-control dbs-entero20-decimal6" runat="server"></asp:TextBox>
                                                    </ContentTemplate>
                                                </asp:updatepanel>
                                            </div>
                                            <div class="col-md-2">
                                                <label class="form-control-label">Diferencia CLP</label>

                                            </div>
                                            <div class="col-md-4">
                                                <asp:updatepanel runat="server" id="UpdatePanel46" updatemode="Conditional">
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="txtModalCuotaSaliente" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtModalNavSaliente" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtModalNavEntrante" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtModalTipoCambio" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtModalMontoSaliente" EventName="TextChanged" />
                                                    </Triggers>
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtModalDiferenciaCLP" CssClass="form-control dbs-entero18-decimal0" runat="server"></asp:TextBox>
                                                    </ContentTemplate>
                                                </asp:updatepanel>
                                            </div>

                                        </div>
                                        <div class="row mt-2">
                                            <div class="col-md-3">
                                                <label class="form-control-label">Fijación Nav</label>
                                            </div>
                                            <div class="col-md-9">
                                                <asp:updatepanel runat="server" id="UpdatePanel56" updatemode="Conditional">
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="ddlModalNemotecnicoEntranteCanje" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtModalNavSaliente" EventName="TextChanged" />
                                                    </Triggers>
                                                    <ContentTemplate>
                                                        <asp:DropDownList ID="ddlModalFijacionNav" CssClass="form-control js-select2-rut" runat="server">
                                                            <asp:ListItem Value="">Seleccione una opción</asp:ListItem>
                                                            <asp:ListItem Value="Pendiente">Pendiente</asp:ListItem>
                                                            <asp:ListItem Value="Realizado">Realizado</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </ContentTemplate>
                                                </asp:updatepanel>

                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="card mt- d-flex justify-content-between col-md-6">
                                    <div class="card-body">
                                        <h5 class="card-title">Entrante</h5>
                                        <hr />
                                        <div class="row mt-2">
                                            <div class="col-md-3">
                                                <label class="form-control-label">Nemotécnico</label>

                                            </div>
                                            <div class="col-md-9">
                                                <asp:updatepanel runat="server" id="UpdatePanel47" updatemode="Conditional">
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="ddlModalFondoCanje" EventName="SelectedIndexChanged" />

                                                    </Triggers>
                                                    <ContentTemplate>
                                                        <asp:DropDownList ID="ddlModalNemotecnicoEntranteCanje" CssClass="form-control js-select2-rut" AutoPostBack="true" runat="server" />
                                                    </ContentTemplate>
                                                </asp:updatepanel>
                                            </div>
                                        </div>
                                        <div class="row mt-2">
                                            <div class="col-md-3">
                                                <label class="form-control-label">Fecha Nav</label>
                                            </div>
                                            <div class="col-md-9">
                                                <asp:updatepanel runat="server" id="UpdatePanel48" updatemode="Conditional">
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="lnkbtnModalFechaNavEntrante" EventName="click" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddlModalNemotecnicoEntranteCanje" EventName="SelectedIndexChanged" />
                                                    </Triggers>
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtModalFechaNavEntrante" runat="server" CssClass="form-control datepicker" ReadOnly="True"></asp:TextBox>
                                                        <asp:Calendar ID="CalendarModalFechaNavEntrante" runat="server" Visible="False" onblur="onblurCalendar(this)" BackColor="White" BorderColor="White" BorderWidth="1px" Font-Names="Verdana" Font-Size="9pt" ForeColor="Black" Height="190px" NextPrevFormat="FullMonth" Width="350px" class="calendarios">
                                                            <DayHeaderStyle Font-Bold="True" Font-Size="8pt" />
                                                            <NextPrevStyle Font-Bold="True" Font-Size="8pt" ForeColor="#333333" VerticalAlign="Bottom" />
                                                            <OtherMonthDayStyle ForeColor="#999999" />
                                                            <SelectedDayStyle BackColor="#333399" ForeColor="White" />
                                                            <TitleStyle BackColor="White" BorderColor="Black" BorderWidth="4px" Font-Bold="True" Font-Size="12pt" ForeColor="#333399" />
                                                            <TodayDayStyle BackColor="#CCCCCC" />
                                                        </asp:Calendar>
                                                        <asp:LinkButton ID="lnkbtnModalFechaNavEntrante" Class="btn btn-secondary mt-1 btn-sm w-100" runat="server" Height="5%" Visible="false">Elegir Fecha</asp:LinkButton>
                                                        <span id="reqtxtModalFechaNavEntrante" class="reqError"></span>
                                                    </ContentTemplate>
                                                </asp:updatepanel>
                                            </div>
                                        </div>
                                        <div class="row mt-2">
                                            <div class="col-md-2">
                                                <label class="form-control-label">Serie</label>

                                            </div>
                                            <div class="col-md-4">
                                                <asp:updatepanel runat="server" id="UpdatePanel49" updatemode="Conditional">
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="ddlModalNemotecnicoEntranteCanje" EventName="SelectedIndexChanged" />
                                                    </Triggers>
                                                    <ContentTemplate>
                                                        <asp:DropDownList ID="ddlModalSerieEntranteCanje" CssClass="form-control js-select2-rut" runat="server" />
                                                    </ContentTemplate>
                                                </asp:updatepanel>

                                            </div>
                                            <div class="col-md-2">
                                                <label class="form-control-label">Moneda</label>
                                            </div>
                                            <div class="col-md-4">
                                                <asp:updatepanel runat="server" id="UpdatePanel50" updatemode="Conditional">
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="ddlModalNemotecnicoEntranteCanje" EventName="SelectedIndexChanged" />
                                                    </Triggers>
                                                    <ContentTemplate>
                                                        <asp:DropDownList ID="ddlModalMonedaEntranteCanje" CssClass="form-control js-select2-rut" runat="server" />
                                                    </ContentTemplate>
                                                </asp:updatepanel>

                                            </div>
                                        </div>
                                        <div class="row mt-2">
                                            <div class="col-md-2">
                                                <label class="form-control-label">Cuotas</label>

                                            </div>
                                            <div class="col-md-4">
                                                <asp:updatepanel runat="server" id="UpdatePanel51" updatemode="Conditional">
                                                    <Triggers>                                                        
                                                        <asp:AsyncPostBackTrigger ControlID="txtModalCuotaSaliente" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtModalNavSaliente" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtModalMontoSaliente" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtModalTipoCambio" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtModalNavEntrante" EventName="TextChanged" />
                                                    </Triggers>
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtModalCuotaEntrante" CssClass="form-control dbs-entero13-decimal0" runat="server" AutoPostBack="true"></asp:TextBox>
                                                    </ContentTemplate>
                                                </asp:updatepanel>

                                            </div>
                                            <div class="col-md-2">
                                                <label class="form-control-label">Factor</label>

                                            </div>
                                            <div class="col-md-4">
                                                <asp:updatepanel runat="server" id="UpdatePanel200" updatemode="Conditional">
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="ddlModalNemotecnicoSalienteCanje" EventName="SelectedIndexChanged" />
                                                        
                                                        <asp:AsyncPostBackTrigger ControlID="txtModalNavSaliente" EventName="TextChanged" />      
                                                        <asp:AsyncPostBackTrigger ControlID="txtModalNavEntrante" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtModalTipoCambio" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtModalNavSaliente" EventName="TextChanged" />
                                                    </Triggers>
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtModalFactor" CssClass="form-control dbs-entero17-decimal9" runat="server" ReadOnly="true"></asp:TextBox>
                                                    </ContentTemplate>
                                                </asp:updatepanel>

                                            </div>
                                        </div>
                                        <div class="row mt-2">
                                            <div class="col-md-2">
                                                <label class="form-control-label">Nav</label>

                                            </div>
                                            <div class="col-md-4">
                                                <asp:updatepanel runat="server" id="UpdatePanel52" updatemode="Conditional">
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="ddlModalNemotecnicoEntranteCanje" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtModalTipoCambio" EventName="TextChanged" />
                                                    </Triggers>
                                                    <ContentTemplate>
                                                        <asp:Textbox ID="txtModalNavEntrante" CssClass="form-control dbs-entero20-decimal6" runat="server" onpaste="return false" oncut="return false" oncopy="return false" onchange="validarValorEntrante()" OnTextChanged="calcularFactor" AutoPostBack="true"></asp:Textbox>
                                                    </ContentTemplate>
                                                </asp:updatepanel>
                                            </div>
                                            <div class="col-md-2">
                                                <label class="form-control-label">Nav (CLP)</label>

                                            </div>
                                            <div class="col-md-4">
                                                <asp:updatepanel runat="server" id="UpdatePanel53" updatemode="Conditional">
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="txtModalNavEntrante" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtModalCuotaSaliente" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddlModalNemotecnicoEntranteCanje" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtModalTipoCambio" EventName="TextChanged" />
                                                    </Triggers>
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtModalNavCLPEntrante" CssClass="form-control dbs-entero20-decimal4" runat="server"></asp:TextBox>
                                                    </ContentTemplate>
                                                </asp:updatepanel>
                                            </div>
                                        </div>
                                        <div class="row mt-2">
                                            <div class="col-md-2">
                                                <label class="form-control-label">Monto</label>

                                            </div>
                                            <div class="col-md-4">
                                                <asp:updatepanel runat="server" id="UpdatePanel54" updatemode="Conditional">
                                                    <Triggers>                                                        
                                                        <asp:AsyncPostBackTrigger ControlID="txtModalCuotaSaliente" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtModalMontoSaliente" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtModalNavEntrante" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtModalTipoCambio" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtModalNavSaliente" EventName="TextChanged" />
                                                    </Triggers>
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtModalMontoEntrante" CssClass="form-control dbs-entero20-decimal2" runat="server"></asp:TextBox>
                                                    </ContentTemplate>
                                                </asp:updatepanel>

                                            </div>
                                            <div class="col-md-2">
                                                <label class="form-control-label">Monto (CLP)</label>

                                            </div>
                                            <div class="col-md-4">
                                                <asp:updatepanel runat="server" id="UpdatePanel55" updatemode="Conditional">
                                                    <Triggers>                                                        
                                                        <asp:AsyncPostBackTrigger ControlID="txtModalTipoCambio" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtModalMontoSaliente" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtModalCuotaSaliente" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtModalNavEntrante" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtModalNavSaliente" EventName="TextChanged" />
                                                    </Triggers>
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtModalMontoCLPEntrante" CssClass="form-control dbs-entero20-decimal2" runat="server"></asp:TextBox>
                                                    </ContentTemplate>
                                                </asp:updatepanel>

                                            </div>
                                        </div>
                                        <div class="row mt-2">
                                            <div class="col-md-2">
                                                <label class="form-control-label">Fecha Observado</label>
                                            </div>
                                            <div class="col-md-4">
                                                <asp:updatepanel runat="server" id="UpdatePanel34" updatemode="Conditional">
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="lnkbtnModalFechaObservado" EventName="click" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddlModalNemotecnicoSalienteCanje" EventName="SelectedIndexChanged" />
                                                    </Triggers>
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtModalFechaObservado" runat="server" CssClass="form-control datepicker" ReadOnly="True"></asp:TextBox>
                                                        <asp:Calendar ID="CalendarModalFechaObservado" runat="server" Visible="False" onblur="onblurCalendar(this)" BackColor="White" BorderColor="White" BorderWidth="1px" Font-Names="Verdana" Font-Size="9pt" ForeColor="Black" Height="190px" NextPrevFormat="FullMonth" Width="350px" class="calendarios">
                                                            <DayHeaderStyle Font-Bold="True" Font-Size="8pt" />
                                                            <NextPrevStyle Font-Bold="True" Font-Size="8pt" ForeColor="#333333" VerticalAlign="Bottom" />
                                                            <OtherMonthDayStyle ForeColor="#999999" />
                                                            <SelectedDayStyle BackColor="#333399" ForeColor="White" />
                                                            <TitleStyle BackColor="White" BorderColor="Black" BorderWidth="4px" Font-Bold="True" Font-Size="12pt" ForeColor="#333399" />
                                                            <TodayDayStyle BackColor="#CCCCCC" />
                                                        </asp:Calendar>
                                                        <asp:LinkButton ID="lnkbtnModalFechaObservado" Class="btn btn-secondary mt-1 btn-sm w-100" runat="server" Visible="false">Elegir Fecha</asp:LinkButton>
                                                        <span id="reqtxtModalFechaObservado" class="reqError"></span>
                                                    </ContentTemplate>
                                                </asp:updatepanel>
                                            </div>
                                            <div class="col-md-2">
                                            </div>
                                            <div class="col-md-4">
                                            </div>
                                        </div>

                                    </div>
                                </div>


                            </div>
                            <div class="col-md-11 mx-auto d-flex p-0 mb-10 justify-content-between">

                                <div class="card mt-0 col-md-6">
                                    <div class="card-body">
                                        <h5 class="card-title">CUSTODIA DISPONIBLE</h5>
                                        <hr />
                                        <div class="row mt-2">
                                            <div class="col-md-2">
                                                <label class="form-control-label">Fecha Actual</label>
                                            </div>
                                            <div class="col-md-4">
                                                <asp:updatepanel runat="server" id="UpdatePanel57" updatemode="Conditional">
                                                    <Triggers>

                                                        <asp:AsyncPostBackTrigger ControlID="ddlModalNemotecnicoSalienteCanje" EventName="SelectedIndexChanged" />
                                                    </Triggers>
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtModalFechaCuotaDCV" runat="server" CssClass="form-control" ReadOnly="True"></asp:TextBox>
                                                    </ContentTemplate>
                                                </asp:updatepanel>
                                            </div>
                                            <div class="col-md-2">
                                                <label class="form-control-label">Cuotas DCV</label>
                                            </div>
                                            <div class="col-md-4">
                                                <asp:updatepanel runat="server" id="UpdatePanel58" updatemode="Conditional">
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="ddlModalNemotecnicoSalienteCanje" EventName="SelectedIndexChanged" />
                                                    </Triggers>
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtModalCuotaDCV" CssClass="form-control dbs-entero18-decimal0" runat="server" ReadOnly="True"></asp:TextBox>
                                                    </ContentTemplate>
                                                </asp:updatepanel>

                                            </div>
                                        </div>
                                        <div class="row mt-2">
                                            <div class="col-md-2">
                                                <label class="form-control-label">Rescates</label>

                                            </div>
                                            <div class="col-md-4">
                                                <asp:updatepanel runat="server" id="UpdatePanel59" updatemode="Conditional">
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="ddlModalNemotecnicoSalienteCanje" EventName="SelectedIndexChanged" />
                                                    </Triggers>
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtModalRescateTransito" CssClass="form-control dbs-entero10-decimal0" runat="server"></asp:TextBox>
                                                    </ContentTemplate>
                                                </asp:updatepanel>

                                            </div>
                                            <div class="col-md-2">
                                                <label class="form-control-label">Canjes</label>
                                            </div>
                                            <div class="col-md-4">
                                                <asp:updatepanel runat="server" id="UpdatePanel60" updatemode="Conditional">
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="ddlModalNemotecnicoSalienteCanje" EventName="SelectedIndexChanged" />
                                                    </Triggers>
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtModalCanjeTransito" CssClass="form-control dbs-entero10-decimal0" runat="server"></asp:TextBox>
                                                    </ContentTemplate>
                                                </asp:updatepanel>

                                            </div>
                                        </div>

                                        <div class="row mt-2">
                                            <div class="col-md-3">
                                                <label class="form-control-label">Suscripciones</label>

                                            </div>
                                            <div class="col-md-3">
                                                <asp:updatepanel runat="server" id="UpdatePanel61" updatemode="Conditional">
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="ddlModalNemotecnicoSalienteCanje" EventName="SelectedIndexChanged" />
                                                    </Triggers>
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtModalSuscripcionTransito" CssClass="form-control dbs-entero10-decimal0" runat="server"></asp:TextBox>
                                                    </ContentTemplate>
                                                </asp:updatepanel>
                                            </div>
                                            <div class="col-md-2">
                                                <label class="form-control-label">Cuotas Disponibles</label>
                                            </div>
                                            <div class="col-md-4">
                                                <asp:updatepanel runat="server" id="UpdatePanel62" updatemode="Conditional">
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="ddlModalNemotecnicoSalienteCanje" EventName="SelectedIndexChanged" />
                                                    </Triggers>
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtModalCuotasDisponibles" CssClass="form-control dbs-entero18-decimal0" runat="server"></asp:TextBox>
                                                    </ContentTemplate>
                                                </asp:updatepanel>

                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="card mt- d-flex justify-content-between col-md-6">
                                    <div class="card-body">
                                        <div class="row mt-2">
                                            <div class="col-md-2">
                                                <label class="form-control-label">Tipo Cambio</label>
                                            </div>
                                            <div class="col-md-4">
                                                <asp:updatepanel runat="server" id="UpdatePanel63" updatemode="Conditional">
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="ddlModalNemotecnicoSalienteCanje" EventName="SelectedIndexChanged" />
                                                    </Triggers>
                                                    <ContentTemplate>
                                                        <asp:Textbox ID="txtModalTipoCambio" CssClass="form-control dbs-entero20-decimal6" runat="server" onpaste="return false" oncut="return false" OnTextChanged="cambioTC" AutoPostBack="true"></asp:Textbox>
                                                           </ContentTemplate>
                                                </asp:updatepanel>
                                            </div>
                                            <div class="col-md-2">
                                                <label class="form-control-label">Contrato</label>
                                            </div>
                                            <div class="col-md-4">
                                                <asp:dropdownlist id="ddlModalContrato" cssclass="form-control js-select2-rut" runat="server">
                                                    <asp:ListItem Value="">Seleccione una opción</asp:ListItem>
                                                    <asp:ListItem Value="OK">OK</asp:ListItem>
                                                    <asp:ListItem Value="No OK">No OK</asp:ListItem>
                                                </asp:dropdownlist>
                                            </div>
                                        </div>
                                        <div class="row mt-2">
                                            <div class="col-md-2">
                                                <label class="form-control-label">Estado</label>
                                            </div>
                                            <div class="col-md-4">
                                                <asp:dropdownlist id="ddlModalEstado" cssclass="form-control js-select2-rut" runat="server">
                                                    <asp:ListItem Value="">Seleccione una opción</asp:ListItem>
                                                    <asp:ListItem Value="Pendiente">Pendiente</asp:ListItem>
                                                    <asp:ListItem Value="Cerrado">Cerrado</asp:ListItem>
                                                </asp:dropdownlist>
                                            </div>
                                            <div class="col-md-2">
                                                <label class="form-control-label">Poderes</label>
                                            </div>
                                            <div class="col-md-4">
                                                <asp:dropdownlist id="ddlModalPoderes" cssclass="form-control js-select2-rut" runat="server">
                                                    <asp:ListItem Value="">Seleccione una opción</asp:ListItem>
                                                    <asp:ListItem Value="OK">OK</asp:ListItem>
                                                    <asp:ListItem Value="No OK">No OK</asp:ListItem>
                                                </asp:dropdownlist>
                                            </div>
                                        </div>
                                        <div class="row mt-2">
                                            <div class="col-md-3">
                                                <label class="form-control-label">Observaciones</label>

                                            </div>
                                            <div class="col-md-9">
                                                <asp:textbox id="txtModalObservaciones" cssclass="form-control" runat="server" textmode="MultiLine"></asp:textbox>
                                            </div>
                                        </div>
                                        <div class="row mt-2">
                                            <div class="col-md-3">
                                                <label class="form-control-label">Id Canje</label>

                                            </div>
                                            <div class="col-md-9">
                                                <asp:textbox id="txtIdCanje" cssclass="form-control" runat="server" readonly="true"></asp:textbox>
                                            </div>
                                        </div>
                                    </div>
                                </div>


                            </div>




                            <!-- GRUPO DE BOTONES 2 -->
                            <div class="form-group mt-5 text-right">
                                <div class="col-md-offset-1">
                                    <asp:button id="btnModalModificarCanje" text="Modificar" cssclass="btn btn-info w-25" runat="server" onclientclick="if (!confirm('¿Seguro que desea guardar los elementos cambios?')) return false;"></asp:button>
                                    <asp:button id="btnModalCancelarCanje" text="Cancelar" cssclass="btn btn-secondary w-25" width="15%" runat="server" onclientclick="if (!confirm('¿Seguro que desea Cancelar?')) return false;"></asp:button>
                                </div>
                            </div>
                            <!-- FIN GRUPO DE BOTONES 2 -->
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <!-- Bootstrap Modal Dialog Rastreo Crear/Modificar -->
    <div class="modal fade" id="myModalRastreo" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" data-keyboard="false" data-backdrop="static">
        <div class="modal-dialog" style="max-width: 90%;">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title">
                        <asp:label id="Label30" runat="server" text="Formulario - Rescates" font-bold="true" font-size="X-Large">
                        </asp:label>
                    </h4>
                    <button id="Button6" type="button" class="close" data-dismiss="modal" aria-label="Close" runat="server"><span aria-hidden="true">&times;</span></button>
                </div>
                <div class="modal-body">
                    <div class="jumbotron">
                        <div class="card p-4">
                            <div class="row">
                                <!-- FORMULARIO-->
                                <asp:textbox id="txtIDRescate" runat="server" visible="false"></asp:textbox>

                                <div class="col-lg-6">
                                    <div class="card h-30 mt-0">
                                        <div class="card-body">
                                            <hr />
                                            <div class="row mt-3">
                                                <div class="col-md-5">
                                                    <label class="form-control-label">RUT Aportante</label>
                                                </div>
                                                <div class="col-md-6">
                                                    <asp:updatepanel runat="server" id="UpdatePanel64" updatemode="Conditional">
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="ddlModalNombreAportanteRescate" EventName="SelectedIndexChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="ddlModalMultifondoRescate" EventName="SelectedIndexChanged" />
                                                        </Triggers>
                                                        <ContentTemplate>
                                                            <asp:DropDownList ID="ddlModalRutAportanteRescate" CssClass="form-control js-select2-rut" runat="server" OnSelectedIndexChanged="CargarNombreAportanteNemotecnicoPorRutAportanteModal" AutoPostBack="true" />
                                                        </ContentTemplate>
                                                    </asp:updatepanel>
                                                </div>
                                            </div>
                                            <div class="row mt-3">
                                                <div class="col-md-5">
                                                    <label class="form-control-label">Nombre Aportante</label>
                                                </div>
                                                <div class="col-md-6">
                                                    <asp:updatepanel runat="server" id="UpdatePanel65" updatemode="Conditional">
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="ddlModalRutAportanteRescate" EventName="SelectedIndexChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="ddlModalMultifondoRescate" EventName="SelectedIndexChanged" />
                                                        </Triggers>
                                                        <ContentTemplate>
                                                            <asp:DropDownList ID="ddlModalNombreAportanteRescate" CssClass="form-control js-select2-rut" runat="server" OnSelectedIndexChanged="CargarRutAportanteNemotecnicoPorNombreAportanteModal" AutoPostBack="true" />
                                                        </ContentTemplate>
                                                    </asp:updatepanel>
                                                </div>
                                            </div>
                                            <div class="row mt-3">
                                                <div class="col-md-5">
                                                    <label class="form-control-label">Multifondo</label>
                                                </div>
                                                <div class="col-md-6">
                                                    <asp:updatepanel runat="server" id="UpdatePanel66" updatemode="Conditional">
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="ddlModalRutAportanteRescate" EventName="SelectedIndexChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="ddlModalNombreAportanteRescate" EventName="SelectedIndexChanged" />
                                                        </Triggers>
                                                        <ContentTemplate>
                                                            <asp:DropDownList ID="ddlModalMultifondoRescate" CssClass="form-control js-select2-rut" runat="server" AutoPostBack="true" />
                                                        </ContentTemplate>
                                                    </asp:updatepanel>
                                                </div>
                                            </div>
                                            <div class="row mt-3">
                                                <div class="col-md-5">
                                                    <label class="form-control-label">Nombre Serie</label>
                                                </div>
                                                <div class="col-md-6">
                                                    <asp:updatepanel runat="server" id="UpdatePanel67" updatemode="Conditional">
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="ddlModalRutFondoRescate" EventName="SelectedIndexChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="ddlModalNemotecnicoRescate" EventName="SelectedIndexChanged" />
                                                        </Triggers>
                                                        <ContentTemplate>
                                                            <asp:TextBox ID="txtModalNombreSerie" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>

                                                        </ContentTemplate>
                                                    </asp:updatepanel>
                                                </div>
                                            </div>

                                            <div class="row mt-3">
                                                <div class="col-md-5">
                                                    <label class="form-control-label">Cuotas</label>
                                                </div>
                                                <div class="col-md-6">
                                                    <asp:updatepanel runat="server" id="UpdatePanel68" updatemode="Conditional">
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="txtModalNAV" EventName="TextChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="txtModalMonto" EventName="TextChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="txtModalMontoCLP" EventName="TextChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="txtModalNAV_CLP" EventName="TextChanged" />
                                                        </Triggers>
                                                        <ContentTemplate>
                                                            <asp:TextBox ID="txtModalCuota" runat="server" CssClass="form-control dbs-entero-decimal" onkeypress="return soloNumeros(event)"></asp:TextBox>
                                                        </ContentTemplate>
                                                    </asp:updatepanel>
                                                </div>
                                            </div>
                                            <div class="row mt-3">
                                                <div class="col-md-5">
                                                    <label class="form-control-label">Moneda de Pago</label>
                                                </div>
                                                <div class="col-md-6">
                                                    <asp:updatepanel runat="server" id="UpdatePanel69" updatemode="Conditional">
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="ddlModalRutFondoRescate" EventName="SelectedIndexChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="ddlModalNombreFondoRescate" EventName="SelectedIndexChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="ddlModalNemotecnicoRescate" EventName="SelectedIndexChanged" />
                                                        </Triggers>
                                                        <ContentTemplate>
                                                            <asp:DropDownList ID="ddlModalMonedaPago" CssClass="form-control js-select2-rut" runat="server" AutoPostBack="true">
                                                                <asp:ListItem Value="USD">USD</asp:ListItem>
                                                                <asp:ListItem Value="CLP">CLP</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </ContentTemplate>
                                                    </asp:updatepanel>
                                                </div>
                                            </div>
                                            <div class="row mt-3">
                                                <div class="col-md-5">
                                                    <label class="form-control-label">TC Observado</label>
                                                </div>
                                                <div class="col-md-6">
                                                    <asp:updatepanel runat="server" id="UpdatePanel70" updatemode="Conditional">
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="ddlModalRutFondoRescate" EventName="SelectedIndexChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="ddlModalNombreFondoRescate" EventName="SelectedIndexChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="ddlModalNemotecnicoRescate" EventName="SelectedIndexChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="ddlModalMonedaPago" EventName="SelectedIndexChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="CalendarModalFechaTCObs" EventName="SelectionChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="txtModalNAV" EventName="TextChanged" />
                                                        </Triggers>
                                                        <ContentTemplate>
                                                            <asp:TextBox ID="txtModalTCObservado" runat="server" CssClass="form-control dbs-entero20-decimal6" OnTextChanged="tcrescateschanged" AutoPostBack="true"></asp:TextBox>
                                                        </ContentTemplate>
                                                    </asp:updatepanel>
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
                                                    <asp:updatepanel runat="server" id="UpdatePanel71" updatemode="Conditional">
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="ddlModalRutFondoRescate" EventName="SelectedIndexChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="ddlModalNombreFondoRescate" EventName="SelectedIndexChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="ddlModalNemotecnicoRescate" EventName="SelectedIndexChanged" />
                                                        </Triggers>
                                                        <ContentTemplate>
                                                            <asp:TextBox ID="txtModalFechaSolicitudRescate" runat="server" CssClass="form-control datepicker"></asp:TextBox>
                                                            <asp:Calendar ID="Calendar5" runat="server" Visible="False" BackColor="White" BorderColor="White" BorderWidth="1px" Font-Names="Verdana" Font-Size="9pt" ForeColor="Black" Height="190px" NextPrevFormat="FullMonth" Width="350px" class="calendarios">
                                                                <DayHeaderStyle Font-Bold="True" Font-Size="8pt" />
                                                                <NextPrevStyle Font-Bold="True" Font-Size="8pt" ForeColor="#333333" VerticalAlign="Bottom" />
                                                                <OtherMonthDayStyle ForeColor="#999999" />
                                                                <SelectedDayStyle BackColor="#333399" ForeColor="White" />
                                                                <TitleStyle BackColor="White" BorderColor="Black" BorderWidth="4px" Font-Bold="True" Font-Size="12pt" ForeColor="#333399" />
                                                                <TodayDayStyle BackColor="#CCCCCC" />
                                                            </asp:Calendar>
                                                        </ContentTemplate>
                                                    </asp:updatepanel>
                                                </div>
                                            </div>
                                            <div class="row mt-3">
                                                <div class="col-md-5">
                                                    <label class="form-control-label">Fecha de NAV</label>
                                                </div>
                                                <div class="col-md-6">
                                                    <asp:updatepanel runat="server" id="UpdatePanel72" updatemode="Conditional">
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="ddlModalRutFondoRescate" EventName="SelectedIndexChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="ddlModalNombreFondoRescate" EventName="SelectedIndexChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="ddlModalNemotecnicoRescate" EventName="SelectedIndexChanged" />
                                                        </Triggers>
                                                        <ContentTemplate>
                                                            <asp:TextBox ID="txtModalFechaNAV" runat="server" CssClass="form-control datepicker"></asp:TextBox>
                                                            <asp:Calendar ID="CalendarModalFechaNAV" runat="server" Visible="False" BackColor="White" BorderColor="White" BorderWidth="1px" Font-Names="Verdana" Font-Size="9pt" ForeColor="Black" Height="190px" NextPrevFormat="FullMonth" Width="350px" class="calendarios">
                                                                <DayHeaderStyle Font-Bold="True" Font-Size="8pt" />
                                                                <NextPrevStyle Font-Bold="True" Font-Size="8pt" ForeColor="#333333" VerticalAlign="Bottom" />
                                                                <OtherMonthDayStyle ForeColor="#999999" />
                                                                <SelectedDayStyle BackColor="#333399" ForeColor="White" />
                                                                <TitleStyle BackColor="White" BorderColor="Black" BorderWidth="4px" Font-Bold="True" Font-Size="12pt" ForeColor="#333399" />
                                                                <TodayDayStyle BackColor="#CCCCCC" />
                                                            </asp:Calendar>
                                                            <asp:LinkButton ID="lnkBtnModalFechaNAV" class="btn btn-secondary mt-1 btn-sm w-100" runat="server" Visible="false">Elegir Fecha NAV</asp:LinkButton>
                                                        </ContentTemplate>
                                                    </asp:updatepanel>
                                                </div>
                                            </div>
                                            <div class="row mt-3">
                                                <div class="col-md-5">
                                                    <label class="form-control-label">Fecha de Pago</label>
                                                </div>
                                                <div class="col-md-6">
                                                    <asp:updatepanel runat="server" id="UpdatePanel73" updatemode="Conditional">
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="ddlModalRutFondoRescate" EventName="SelectedIndexChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="ddlModalNombreFondoRescate" EventName="SelectedIndexChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="ddlModalNemotecnicoRescate" EventName="SelectedIndexChanged" />
                                                        </Triggers>
                                                        <ContentTemplate>
                                                            <asp:TextBox ID="txtModalFechaPago" runat="server" CssClass="form-control datepicker"></asp:TextBox>
                                                            <asp:Calendar ID="CalendarModalFechaPago" runat="server" Visible="False" BackColor="White" BorderColor="White" BorderWidth="1px" Font-Names="Verdana" Font-Size="9pt" ForeColor="Black" Height="190px" NextPrevFormat="FullMonth" Width="350px" class="calendarios">
                                                                <DayHeaderStyle Font-Bold="True" Font-Size="8pt" />
                                                                <NextPrevStyle Font-Bold="True" Font-Size="8pt" ForeColor="#333333" VerticalAlign="Bottom" />
                                                                <OtherMonthDayStyle ForeColor="#999999" />
                                                                <SelectedDayStyle BackColor="#333399" ForeColor="White" />
                                                                <TitleStyle BackColor="White" BorderColor="Black" BorderWidth="4px" Font-Bold="True" Font-Size="12pt" ForeColor="#333399" />
                                                                <TodayDayStyle BackColor="#CCCCCC" />
                                                            </asp:Calendar>
                                                            <asp:LinkButton ID="lnkBtnModalFechaPago" class="btn btn-secondary mt-1 btn-sm w-100" runat="server" Visible="false">Elegir Fecha Pago</asp:LinkButton>
                                                        </ContentTemplate>
                                                    </asp:updatepanel>
                                                </div>
                                            </div>
                                            <div class="row mt-3">
                                                <div class="col-md-5">
                                                    <label class="form-control-label">Fecha de TC Obs</label>
                                                </div>
                                                <div class="col-md-6">
                                                    <asp:updatepanel runat="server" id="UpdatePanel74" updatemode="Conditional">
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="ddlModalRutFondoRescate" EventName="SelectedIndexChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="ddlModalNombreFondoRescate" EventName="SelectedIndexChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="ddlModalNemotecnicoRescate" EventName="SelectedIndexChanged" />
                                                        </Triggers>
                                                        <ContentTemplate>
                                                            <asp:TextBox ID="txtModalFechaTCObs" runat="server" CssClass="form-control datepicker"></asp:TextBox>
                                                            <asp:Calendar ID="CalendarModalFechaTCObs" runat="server" Visible="False" AutoPostBack="true" BackColor="White" BorderColor="White" BorderWidth="1px" Font-Names="Verdana" Font-Size="9pt" ForeColor="Black" Height="190px" NextPrevFormat="FullMonth" Width="350px" class="calendarios">
                                                                <DayHeaderStyle Font-Bold="True" Font-Size="8pt" />
                                                                <NextPrevStyle Font-Bold="True" Font-Size="8pt" ForeColor="#333333" VerticalAlign="Bottom" />
                                                                <OtherMonthDayStyle ForeColor="#999999" />
                                                                <SelectedDayStyle BackColor="#333399" ForeColor="White" />
                                                                <TitleStyle BackColor="White" BorderColor="Black" BorderWidth="4px" Font-Bold="True" Font-Size="12pt" ForeColor="#333399" />
                                                                <TodayDayStyle BackColor="#CCCCCC" />
                                                            </asp:Calendar>
                                                            <asp:LinkButton ID="lnkBtnModalFechaTCObs" class="btn btn-secondary mt-1 btn-sm w-100" runat="server" Visible="false">Elegir Fecha TC</asp:LinkButton>
                                                        </ContentTemplate>
                                                    </asp:updatepanel>
                                                </div>
                                            </div>
                                            <div class="row mt-3">
                                                <div class="col-md-5">
                                                    <label class="form-control-label">Monto</label>
                                                </div>
                                                <div class="col-md-6">
                                                    <asp:updatepanel runat="server" id="UpdatePanel75" updatemode="Conditional">
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="txtModalCuota" EventName="TextChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="txtModalNAV" EventName="TextChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="txtModalFechaTCObs" EventName="TextChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="CalendarModalFechaTCObs" EventName="SelectionChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="CalendarModalFechaSolicitud" EventName="SelectionChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="txtModalTCObservado" EventName="TextChanged" />
                                                        </Triggers>
                                                        <ContentTemplate>
                                                            <asp:TextBox ID="txtModalMonto" runat="server" CssClass="form-control dbs-entero14-decimal2" OnTextChanged="CargarCuotasModalRescate" AutoPostBack="true"></asp:TextBox>
                                                        </ContentTemplate>
                                                    </asp:updatepanel>
                                                </div>
                                            </div>
                                            <div class="row mt-3">
                                                <div class="col-md-5">
                                                    <label class="form-control-label">Monto (CLP)</label>
                                                </div>
                                                <div class="col-md-6">
                                                    <asp:updatepanel runat="server" id="UpdatePanel76" updatemode="Conditional">
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="txtModalCuota" EventName="TextChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="txtModalNAV" EventName="TextChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="txtModalNAV_CLP" EventName="TextChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="CalendarModalFechaTCObs" EventName="SelectionChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="txtModalFechaTCObs" EventName="TextChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="txtModalMonto" EventName="TextChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="CalendarModalFechaSolicitud" EventName="SelectionChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="txtModalTCObservado" EventName="TextChanged" />
                                                        </Triggers>
                                                        <ContentTemplate>
                                                            <asp:TextBox ID="txtModalMontoCLP" runat="server" CssClass="form-control dbs-entero14-decimal2" AutoPostBack="true"></asp:TextBox>
                                                        </ContentTemplate>
                                                    </asp:updatepanel>
                                                </div>
                                            </div>
                                            <div class="row mt-3">
                                                <div class="col-md-5">
                                                    <label class="form-control-label">Contrato</label>
                                                </div>
                                                <div class="col-md-6">
                                                    <asp:dropdownlist id="Dropdownlist7" cssclass="form-control js-select2-rut" runat="server" enabled="false">
                                                        <asp:ListItem Value=""></asp:ListItem>
                                                        <asp:ListItem Value="OK">OK</asp:ListItem>
                                                        <asp:ListItem Value="NO OK">NO OK</asp:ListItem>
                                                    </asp:dropdownlist>
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
                                                    <asp:textbox id="txtModalPatrimonio" runat="server" cssclass="form-control dbs-entero15-decimal4" readonly="True"></asp:textbox>
                                                </div>
                                            </div>
                                            <div class="row mt-3">
                                                <div class="col-md-5">
                                                    <label class="form-control-label">Porcentaje %</label>
                                                </div>
                                                <div class="col-md-6">
                                                    <asp:updatepanel runat="server" id="UpdatePanel77" updatemode="Conditional">
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="ddlModalRutFondoRescate" EventName="SelectedIndexChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="ddlModalNombreFondoRescate" EventName="SelectedIndexChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="ddlModalNemotecnicoRescate" EventName="SelectedIndexChanged" />
                                                        </Triggers>
                                                        <ContentTemplate>
                                                            <asp:TextBox ID="txtModalPorcentaje" runat="server" CssClass="form-control form-control-sm" ReadOnly="True"></asp:TextBox>
                                                        </ContentTemplate>
                                                    </asp:updatepanel>
                                                </div>
                                            </div>
                                            <div class="row mt-3">
                                                <div class="col-md-5">
                                                    <label class="form-control-label">Rescate Max</label>
                                                </div>
                                                <div class="col-md-6">
                                                    <asp:textbox id="txtModalRescateMax" runat="server" cssclass="form-control dbs-entero14-decimal4" readonly="True"></asp:textbox>
                                                </div>
                                            </div>
                                            <div class="row mt-3">
                                                <div class="col-md-5">
                                                    <label class="form-control-label">Utilizado</label>
                                                </div>
                                                <div class="col-md-6">
                                                    <asp:textbox id="txtModalUtilizado" runat="server" cssclass="form-control dbs-entero14-decimal4" readonly="True"></asp:textbox>
                                                </div>
                                            </div>
                                            <div class="row mt-3">
                                                <div class="col-md-5">
                                                    <label class="form-control-label">Disponibles</label>
                                                </div>
                                                <div class="col-md-6">
                                                    <asp:textbox id="txtModalDisponibles" runat="server" cssclass="form-control dbs-entero14-decimal4" readonly="True"></asp:textbox>
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
                                                    <asp:updatepanel runat="server" id="UpdatePanel78" updatemode="Conditional">
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="ddlModalNombreFondoRescate" EventName="SelectedIndexChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="ddlModalNemotecnicoRescate" EventName="SelectedIndexChanged" />
                                                        </Triggers>
                                                        <ContentTemplate>
                                                            <asp:DropDownList ID="ddlModalRutFondoRescate" CssClass="form-control js-select2-rut" runat="server" AutoPostBack="true" />
                                                        </ContentTemplate>
                                                    </asp:updatepanel>
                                                </div>
                                            </div>
                                            <div class="row mt-3">
                                                <div class="col-md-5">
                                                    <label class="form-control-label">Nombre Fondo</label>
                                                </div>
                                                <div class="col-md-6">
                                                    <asp:updatepanel runat="server" id="UpdatePanel79" updatemode="Conditional">
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="ddlModalRutFondoRescate" EventName="SelectedIndexChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="ddlModalNemotecnicoRescate" EventName="SelectedIndexChanged" />
                                                        </Triggers>
                                                        <ContentTemplate>
                                                            <asp:DropDownList ID="ddlModalNombreFondoRescate" CssClass="form-control js-select2-rut" runat="server" AutoPostBack="true" />
                                                        </ContentTemplate>
                                                    </asp:updatepanel>
                                                </div>
                                            </div>

                                            <div class="row mt-3">
                                                <div class="col-md-5">
                                                    <label class="form-control-label">Nemotécnico</label>
                                                </div>
                                                <div class="col-md-6">
                                                    <asp:updatepanel runat="server" id="UpdatePanel" updatemode="Conditional">
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="ddlModalRutFondoRescate" EventName="SelectedIndexChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="ddlModalNombreFondoRescate" EventName="SelectedIndexChanged" />
                                                        </Triggers>
                                                        <ContentTemplate>
                                                            <asp:DropDownList ID="ddlModalNemotecnicoRescate" CssClass="form-control js-select2-rut" runat="server" AutoPostBack="true" />
                                                        </ContentTemplate>
                                                    </asp:updatepanel>
                                                </div>
                                            </div>
                                            <div class="row mt-3">
                                                <div class="col-md-5">
                                                    <label class="form-control-label">Moneda Serie</label>
                                                </div>
                                                <div class="col-md-6">
                                                    <asp:updatepanel runat="server" id="UpdatePanel80" updatemode="Conditional">
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="ddlModalRutFondoRescate" EventName="SelectedIndexChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="ddlModalNemotecnicoRescate" EventName="SelectedIndexChanged" />
                                                        </Triggers>
                                                        <ContentTemplate>
                                                            <%--<asp:DropDownList ID="ddlModalMonedaSerie" CssClass="form-control js-select2-rut" runat="server" />--%>
                                                            <asp:TextBox ID="txtModalMonedaSerie" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                                        </ContentTemplate>
                                                    </asp:updatepanel>
                                                </div>
                                            </div>
                                            <div class="row mt-3">
                                                <div class="col-md-5">
                                                    <label class="form-control-label">NAV</label>
                                                </div>
                                                <div class="col-md-6">
                                                    <asp:updatepanel runat="server" id="UpdatePanel81" updatemode="Conditional">
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="ddlModalRutFondoRescate" EventName="SelectedIndexChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="ddlModalNemotecnicoRescate" EventName="SelectedIndexChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="txtModalCuota" EventName="TextChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="CalendarModalFechaNAV" EventName="SelectionChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="CalendarModalFechaSolicitud" EventName="SelectionChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="txtModalTCObservado" EventName="TextChanged" />
                                                        </Triggers>
                                                        <ContentTemplate>
                                                            <asp:TextBox ID="txtModalNAV" runat="server" CssClass="form-control dbs-entero14-decimal6" OnTextChanged="CargarMontoModal" AutoPostBack="true"></asp:TextBox>
                                                        </ContentTemplate>
                                                    </asp:updatepanel>
                                                </div>
                                            </div>
                                            <div class="row mt-3">
                                                <div class="col-md-5">
                                                    <label class="form-control-label">NAV (CLP)</label>
                                                </div>
                                                <div class="col-md-6">
                                                    <asp:updatepanel runat="server" id="UpdatePanel88" updatemode="Conditional">
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="ddlModalRutFondoRescate" EventName="SelectedIndexChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="ddlModalNombreFondoRescate" EventName="SelectedIndexChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="ddlModalNemotecnicoRescate" EventName="SelectedIndexChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="txtModalCuota" EventName="TextChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="txtModalNAV" EventName="TextChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="CalendarModalFechaNAV" EventName="SelectionChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="CalendarModalFechaTCObs" EventName="SelectionChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="txtModalTCObservado" EventName="TextChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="CalendarModalFechaSolicitud" EventName="SelectionChanged" />
                                                        </Triggers>
                                                        <ContentTemplate>
                                                            <asp:TextBox ID="txtModalNAV_CLP" runat="server" CssClass="form-control dbs-entero14-decimal4" Enabled="false"></asp:TextBox>
                                                        </ContentTemplate>
                                                    </asp:updatepanel>
                                                </div>
                                            </div>
                                            <div class="row mt-3">
                                                <div class="col-md-5">
                                                    <label class="form-control-label">Poderes</label>
                                                </div>
                                                <div class="col-md-6">
                                                    <asp:dropdownlist id="Dropdownlist9" cssclass="form-control js-select2-rut" runat="server" enabled="false">
                                                        <asp:ListItem Value=""></asp:ListItem>
                                                        <asp:ListItem Value="OK">OK</asp:ListItem>
                                                        <asp:ListItem Value="NO OK">NO OK</asp:ListItem>
                                                    </asp:dropdownlist>
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
                                                    <asp:updatepanel runat="server" id="UpdatePanel82" updatemode="Conditional">
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="ddlModalRutFondoRescate" EventName="SelectedIndexChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="ddlModalNombreFondoRescate" EventName="SelectedIndexChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="ddlModalNemotecnicoRescate" EventName="SelectedIndexChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="ddlModalNombreAportanteRescate" EventName="SelectedIndexChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="ddlModalRutAportanteRescate" EventName="SelectedIndexChanged" />
                                                        </Triggers>
                                                        <ContentTemplate>
                                                            <asp:TextBox ID="txtModalFechaDCV" runat="server" CssClass="form-control datepicker" ReadOnly="True"></asp:TextBox>
                                                            <asp:Calendar ID="CalendarModalFechaDCV" runat="server" Visible="False" BackColor="White" BorderColor="White" BorderWidth="1px" Font-Names="Verdana" Font-Size="9pt" ForeColor="Black" Height="190px" NextPrevFormat="FullMonth" Width="350px" class="calendarios">
                                                                <DayHeaderStyle Font-Bold="True" Font-Size="8pt" />
                                                                <NextPrevStyle Font-Bold="True" Font-Size="8pt" ForeColor="#333333" VerticalAlign="Bottom" />
                                                                <OtherMonthDayStyle ForeColor="#999999" />
                                                                <SelectedDayStyle BackColor="#333399" ForeColor="White" />
                                                                <TitleStyle BackColor="White" BorderColor="Black" BorderWidth="4px" Font-Bold="True" Font-Size="12pt" ForeColor="#333399" />
                                                                <TodayDayStyle BackColor="#CCCCCC" />
                                                            </asp:Calendar>
                                                        </ContentTemplate>
                                                    </asp:updatepanel>
                                                </div>
                                            </div>
                                            <div class="row mt-3">
                                                <div class="col-md-5">
                                                    <label class="form-control-label">Estado</label>
                                                </div>
                                                <div class="col-md-6">
                                                    <asp:dropdownlist id="Dropdownlist10" cssclass="form-control js-select2-rut" runat="server" enabled="false">
                                                        <asp:ListItem Value="Pendiente">PENDIENTE</asp:ListItem>
                                                        <asp:ListItem Value="Cerrado">CERRADO</asp:ListItem>
                                                    </asp:dropdownlist>
                                                </div>
                                            </div>
                                            <div class="row mt-3">
                                                <div class="col-md-5">
                                                    <label class="form-control-label">Observaciones</label>
                                                </div>
                                                <div class="col-md-6">
                                                    <asp:textbox id="Textbox2" runat="server" cssclass="form-control form-control-sm" enabled="false"></asp:textbox>
                                                </div>
                                            </div>
                                            <div class="row mt-3">
                                                <div class="col-md-5">
                                                    <label class="form-control-label">Fijación NAV</label>
                                                </div>
                                                <div class="col-md-6">
                                                    <asp:updatepanel runat="server" id="UpdatePanel83" updatemode="Conditional">
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="ddlModalRutFondoRescate" EventName="SelectedIndexChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="ddlModalNemotecnicoRescate" EventName="SelectedIndexChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="txtModalCuota" EventName="TextChanged" />
                                                        </Triggers>
                                                        <ContentTemplate>
                                                            <asp:TextBox ID="txtModalFijacionNAV" runat="server" CssClass="form-control form-control-sm" ReadOnly="True"></asp:TextBox>
                                                        </ContentTemplate>
                                                    </asp:updatepanel>
                                                </div>
                                            </div>
                                            <div class="row mt-3">
                                                <div class="col-md-5">
                                                    <label class="form-control-label">Fijacion TC Obs</label>
                                                </div>
                                                <div class="col-md-6">
                                                    <asp:updatepanel runat="server" id="UpdatePanel84" updatemode="Conditional">
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="ddlModalRutFondoRescate" EventName="SelectedIndexChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="ddlModalNombreFondoRescate" EventName="SelectedIndexChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="ddlModalNemotecnicoRescate" EventName="SelectedIndexChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="ddlModalMonedaPago" EventName="SelectedIndexChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="CalendarModalFechaTCObs" EventName="SelectionChanged" />
                                                        </Triggers>
                                                        <ContentTemplate>
                                                            <asp:TextBox ID="txtModalFijacionTCObs" runat="server" CssClass="form-control form-control-sm" ReadOnly="True"></asp:TextBox>
                                                        </ContentTemplate>
                                                    </asp:updatepanel>
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
                                                    <asp:updatepanel runat="server" id="UpdatePanel85" updatemode="Conditional">
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="ddlModalRutFondoRescate" EventName="SelectedIndexChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="ddlModalNombreFondoRescate" EventName="SelectedIndexChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="ddlModalNemotecnicoRescate" EventName="SelectedIndexChanged" />
                                                        </Triggers>
                                                        <ContentTemplate>
                                                            <asp:TextBox ID="txtModalCuotasDVC" runat="server" CssClass="form-control dbs-entero26-decimal0" ReadOnly="True"></asp:TextBox>
                                                        </ContentTemplate>
                                                    </asp:updatepanel>
                                                </div>
                                            </div>
                                            <div class="row mt-3">
                                                <div class="col-md-5">
                                                    <label class="form-control-label">Rescates</label>
                                                </div>
                                                <div class="col-md-6">
                                                    <asp:updatepanel runat="server" id="UpdatePanel86" updatemode="Conditional">
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="ddlModalRutFondoRescate" EventName="SelectedIndexChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="ddlModalNombreFondoRescate" EventName="SelectedIndexChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="ddlModalNemotecnicoRescate" EventName="SelectedIndexChanged" />
                                                        </Triggers>
                                                        <ContentTemplate>
                                                            <asp:TextBox ID="txtModalRescates" runat="server" CssClass="form-control dbs-entero18-decimal0" ReadOnly="True"></asp:TextBox>
                                                        </ContentTemplate>
                                                    </asp:updatepanel>
                                                </div>
                                            </div>
                                            <div class="row mt-3">
                                                <div class="col-md-5">
                                                    <label class="form-control-label">Suscripciones</label>
                                                </div>
                                                <div class="col-md-6">
                                                    <asp:textbox id="txtModalSuscripciones" runat="server" cssclass="form-control dbs-entero18-decimal0" readonly="True"></asp:textbox>
                                                </div>
                                            </div>
                                            <div class="row mt-3">
                                                <div class="col-md-5">
                                                    <label class="form-control-label">Canje</label>
                                                </div>
                                                <div class="col-md-6">
                                                    <asp:textbox id="txtModalCanje" runat="server" cssclass="form-control dbs-entero18-decimal0" readonly="True"></asp:textbox>
                                                </div>
                                            </div>
                                            <div class="row mt-3">
                                                <div class="col-md-5">
                                                    <label class="form-control-label">Disponibles</label>
                                                </div>
                                                <div class="col-md-6">
                                                    <asp:updatepanel runat="server" id="UpdatePanel87" updatemode="Conditional">
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="ddlModalRutFondoRescate" EventName="SelectedIndexChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="ddlModalNombreFondoRescate" EventName="SelectedIndexChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="ddlModalNemotecnicoRescate" EventName="SelectedIndexChanged" />
                                                        </Triggers>
                                                        <ContentTemplate>
                                                            <asp:TextBox ID="txtModalDisponiblesCuotasDisponibles" runat="server" CssClass="form-control dbs-entero18-decimal0" ReadOnly="True"></asp:TextBox>
                                                        </ContentTemplate>
                                                    </asp:updatepanel>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                </div>

                            </div>

                            <!-- GRUPO DE BOTONES 2 -->
                            <div class="form-group mt-5 text-right">
                                <div class="col-md-offset-1">
                                    <asp:button id="btnModalModificarRastreo" text="Modificar" cssclass="btn btn-info w-25" runat="server"></asp:button>
                                    <asp:button id="btnCancelarModalRescates" text="Cancelar" cssclass="btn btn-secondary w-25" width="15%" runat="server" onclientclick="if (!confirm('¿Seguro que desea Cancelar?')) return false;"></asp:button>
                                </div>
                            </div>
                            <!-- FIN GRUPO DE BOTONES 2 -->
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <!-- Bootstrap Modal Dialog Mensajes-->
    <div class="modal fade" id="myModalmg" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content text-center">
                <div class="modal-header mx-auto">
                    <h4 class="modal-title">
                        <asp:image id="img_modal" imageurl="~/Img/info1.png" runat="server" width="130" height="50" />
                        <asp:label id="lblModalTitle" runat="server" text="" font-bold="true" font-size="X-Large">
                        </asp:label>
                    </h4>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Cerrar">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <asp:label id="lblModalBody" runat="server" font-size="X-Large" text=""></asp:label>
                    <br>
                    <br />
                    <asp:hyperlink id="Archivo" runat="server"></asp:hyperlink>
                    <div class="text-center">
                        <asp:image id="img_body_modal" runat="server" imageurl="~/Img/important.png" width="100" height="100" />
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal" onclick="cerrarAlert();">Cerrar</button>
                </div>
            </div>
        </div>
    </div>

    <!-- End Bootstrap Modal Dialog Mensajes-->
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

    <asp:hiddenfield id="HiddenPerfil" runat="server" />
    <asp:hiddenfield id="HiddenConstante" runat="server" />


</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="FooterScript" runat="Server">
    <script src="<%=ResolveUrl("~/Scripts/jquery.dataTables.min.js")%>"></script>
    <script src="<%=ResolveUrl("~/Scripts/dataTables.bootstrap4.min.js")%>"></script>
    <script src="<%=ResolveUrl("~/Scripts/scripts.js")%>"></script>
    <script src="<%=ResolveUrl("~/Scripts/select2.min.js")%>"></script>
    <script src="<%=ResolveUrl("~/Scripts/date-dd-mmm-yyyy.js")%>"></script>
    <script>
        $(document).ready(function () {

            confNumeros();

            $("[id*=txtNAVDesde]").datepicker();
            $("[id*=txtNAVHasta]").datepicker();
            $("[id*=txtFechaTCDesde]").datepicker();
            $("[id*=txtFechaTCHasta]").datepicker();
            $("[id*=txtFechaPagoHasta]").datepicker();
            $("[id*=txtFechaPagoDesde]").datepicker();

            $("[id*=txtNAVDesde]").change(function () {
                changeFechas($("[id*=txtNAVDesde]"), $("[id*=txtNAVHasta]"), 1)
            });
            $("[id*=txtNAVHasta]").change(function () {
                changeFechas($("[id*=txtNAVDesde]"), $("[id*=txtNAVHasta]"), 2)
            });

            $("[id*=txtFechaTCDesde]").change(function () {
                changeFechas($("[id*=txtFechaTCDesde]"), $("[id*=txtFechaTCHasta]"), 1)
            });
            $("[id*=txtFechaTCHasta]").change(function () {
                changeFechas($("[id*=txtFechaTCDesde]"), $("[id*=txtFechaTCHasta]"), 2)
            });

            $("[id*=txtFechaPagoDesde]").change(function () {
                changeFechas($("[id*=txtFechaPagoDesde]"), $("[id*=txtFechaPagoHasta]"), 1)
            });
            $("[id*=txtFechaPagoHasta]").change(function () {
                changeFechas($("[id*=txtFechaPagoDesde]"), $("[id*=txtFechaPagoHasta]"), 2)
            });

            var txtHiddenAccion = $('#<%=txtAccionHidden.ClientID %>').val();
            if (!isPerfilConsulta()) {

                $('.checkFijacion').change(function () {
                    var contador = 0;

                    $('input[type=checkbox]:checked').each(function (i) {
                        contador++;
                    });
                    if (contador > 1) {
                        $(".btnmodificar").prop('disabled', true);
                        $(".BtnFijarNav").prop('disabled', false);
                        $(".BtnTCObs").prop('disabled', false);
                        $(".btnImprimir").prop('disabled', false);
                        $(".btnEliminar").prop('disabled', false);
                    }
                    else {
                        if (contador == 1) {
                            $(".btnmodificar").prop('disabled', false);
                            $(".BtnFijarNav").prop('disabled', false);
                            $(".BtnTCObs").prop('disabled', false);
                            $(".btnImprimir").prop('disabled', false);
                            $(".btnEliminar").prop('disabled', false);
                        }
                        else {
                            $(".btnmodificar").prop('disabled', true);
                            $(".BtnFijarNav").prop('disabled', true);
                            $(".BtnTCObs").prop('disabled', true);
                            $(".btnImprimir").prop('disabled', true);
                            $(".btnEliminar").prop('disabled', true);

                        }
                    }
                });
                $('.checkFijacionAll').change(function () {
                    if ($('.checkFijacionAll input').prop('checked') == true) {
                        $('.checkFijacion input').prop('checked', true);
                        $(".BtnFijarNav").prop('disabled', false);
                        $(".BtnTCObs").prop('disabled', false);
                        $(".btnImprimir").prop('disabled', false);
                        $(".btnEliminar").prop('disabled', false);
                    }
                    else {
                        $('.checkFijacion input').prop('checked', false);
                        $(".BtnFijarNav").prop('disabled', true);
                        $(".BtnTCObs").prop('disabled', true);
                        $(".btnImprimir").prop('disabled', true);
                        $(".btnEliminar").prop('disabled', true);
                    }

                });
                if ((txtHiddenAccion == "Rescate")) {
                    $('#myModalRastreo').modal('show');
                }
                else if (txtHiddenAccion == "Canje") {
                    $('#myModalCanje').modal('show');
                }
                else if (txtHiddenAccion == "Suscripcion") {
                    $('#myModalSuscripcion').modal('show');
                }
                else if (txtHiddenAccion == "MOSTRAR_DIALOGO") {
                    $('#myModalmg').modal();
                } else {
                    checkRadioBtn("");
                }
                seteaBotonFijacionNAV();
                seteaBotonFijacionTC();
                seteaBotonModificar();
                seteaBotonConfirmar()

                //Sys.WebForms.PageRequestManager.getInstance().add_endRequest(bindDataTable);
            }
        });

        function bindDataTable() {
            $(".js-select2-rut").select2({
                templateResult: formatState,
                placeholder: 'Selecciona una opción'
            });

            confNumeros();
        };

        function confNumeros() {
            $('.dbs-entero20-decimal6').mask2(getMask(20, 6));
            $('.dbs-entero17-decimal9').mask2(getMask(17, 9));
            $('.dbs-entero18-decimal0').mask2(getMask(18, 0));
            $('.dbs-entero13-decimal0').mask2(getMask(13, 0));
            $('.dbs-entero10-decimal0').mask2(getMask(10, 0));
            $('.dbs-entero28-decimal12').mask2(getMask(16, 12));
            $('.dbs-entero14-decimal4').mask2(getMask(14, 4));
            $('.dbs-entero15-decimal4').mask2(getMask(15, 4));
            $('.dbs-entero26-decimal0').mask2(getMask(26, 0));
            $('.dbs-entero-decimal').mask2(getMask(16, 0));
            $('.dbs-entero20-decimal4').mask2(getMask(20, 4));
            $('.dbs-entero20-decimal2').mask2(getMask(20, 2));
            $('.dbs-entero20-decimal0').mask2(getMask(20, 0));
            $('.dbs-entero14-decimal6').mask2(getMask(14, 6));

        }

        function noPuntoComa(event) {
            var e = event || window.event;
            var key = e.keyCode || e.which;
            if (key === 110 || key === 190 || key === 188) {
                e.preventDefault();
            }
        }
        function seteaBotonFijacionNAV() {
            $("#<%=BtnFijarNav.ClientID %>").unbind("click");
            $("#<%=BtnFijarNav.ClientID %>").click(function () {
                if (!confirm('¿Confirma que desea Fijar NAV?')) {
                    return false; 
                }
                else {
                    return true;
                }
            });
        }
        function seteaBotonFijacionTC() {
            $("#<%=BtnTCObs.ClientID %>").unbind("click");
            $("#<%=BtnTCObs.ClientID %>").click(function () {
                if (!confirm('¿Confirma que desea Fijar Tipo Cambio?')) {
                    return false; 
                }
                else {
                    return true;
                }
            });
        }
        function seteaBotonModificar() {
            $("#<%=btnModalModificarRastreo.ClientID %>").unbind("click");
            $("#<%=btnModalModificarRastreo.ClientID %>").click(function () {
                if (!confirm('¿Confirma que desea Modificar y Fijar NAV?')) {
                    return false; 
                }
                else {
                    return true;
                }
            });
        }
        
        function validarValorEntrante() {
            var valor = document.getElementById('<%=txtModalNavEntrante.ClientID%>').value;
            if (valor.match(/^\d{1,20}(\,\d{1,6})?$/)) {
                return true;
            } else {
                document.getElementById('<%=txtModalNavEntrante.ClientID%>').value = ""
                alert('La cadena no tiene formato correcto (20 enteros , 6 decimales)');
                return false;
            }
        }

        function cerrarAlert() {
            $('#<%=txtAccionHidden.ClientID %>').val("");
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
                var rutTxt = document.getElementById('<%=ddlListaTipoTransaccion.ClientID%>');
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
            var BtnFijarNav = document.getElementById('<%=BtnFijarNav.ClientID%>');
            var BtnTCObs = document.getElementById('<%=BtnTCObs.ClientID%>');
            var btnImprimir = document.getElementById('<%=btnImprimir.ClientID%>');
            var btnEliminar = document.getElementById('<%=BtnEliminar.ClientID%>');

            btnModificar.disabled = newValue;
            BtnFijarNav.disabled = newValue;
            BtnTCObs.disabled = newValue;
            btnImprimir.disabled = newValue;
            btnEliminar.disabled = newValue;
        }

        function checkRadioBtn(id) {
            var gv = document.getElementById('<%=GrvTabla.ClientID %>');
            var flag = false
            if (gv != null) {
                for (var i = 1; i < gv.rows.length; i++) {
                    var radioBtn = gv.rows[i].cells[0].getElementsByTagName("input");
                    if (radioBtn[0].checked) {
                        flag = true;
                    }
                    // Check if the id not same
                    if (radioBtn[0].id != id.id) {
                        //radioBtn[0].checked = false;
                    }
                    else {
                        if (!isPerfilConsulta()) {
                            enableDisableButtons(false)
                        }
                    }
                }
            }
            var btnImprimir = document.getElementById('<%=btnImprimir.ClientID%>');
            btnImprimir.disabled = !(flag);

            var btnEliminar= document.getElementById('<%=BtnEliminar.ClientID%>');
            btnEliminar.disabled = !(flag);

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
            if ($('#<%=ddlModalNombreAportante.ClientID%>').val() == "") {
                alert("Debe seleccionar un nombre de aportante")
                return false;
            } else {
                if ($('#<%=ddlFondo.ClientID %>').val() == "") {
                    alert("Debe seleccionar el rut de un fondo")
                    return false;
                } else {
                    if ($('#<%=ddlModalRutAportante.ClientID%>').val() == "") {
                        alert("Debe seleccionar un rut de aportante")
                        return false;
                    } else {
                        if ($('#<%=ddlNemotecnico.ClientID%>').val() == "") {
                            alert("Debe seleccionar un Nemotécnico")
                            return false;
                        } else {
                            if ($('#<%=txtCuotas.ClientID%>').val() == "") {
                                alert("Debe digitar las cuotas a suscribir")
                                return false;
                            } else {
                                if ($('#<%=txtMonto.ClientID%>').val() == "") {
                                    alert("Debe ingresar un monto")
                                    return false;
                                } else {
                                    if ($('#<%=ddlMonedaPago.ClientID%>').val() == "") {
                                        alert("Debe seleccionar la moneda de pago")
                                        return false;
                                    } else {
                                        if ($('#<%=txtMontoCLP.ClientID%>').val() == "") {
                                            alert("Debe ingresar el monto CLP")
                                            return false;
                                        } else {
                                            if ($('<%=txtTCObservado.ClientID%>').val() == "") {
                                                alert("Debe ingresar el TC observado")
                                                return false
                                            } else {
                                                if ($('#<%=ddlPoderes.ClientID%>').val() == "") {
                                                    alert("Debe seleccionar una opción en el campo 'Poderes'")
                                                    return false;
                                                } else {
                                                    if ($('#<%=txtFijacionNAV.ClientID%>').val() == "") {
                                                        alert("El campo fijación NAV está vacío")
                                                        return false
                                                    } else {
                                                        if ($('#<%=txtNAV.ClientID%>').val() == "") {
                                                            alert("Debe ingresar un valor NAV")
                                                            return false
                                                        } else {
                                                            if ($('#<%=txtNAVCLP.ClientID%>').val() == "") {
                                                                alert("Debe ingresar un valor NAV CLP")
                                                                return false
                                                            } else {
                                                                if ($('#<%=ddlEstado1.ClientID%>').val() == "") {
                                                                    alert("Debe seleccionar el estado de la suscripción");
                                                                    return false;
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
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

        /*
         JOVB : R3 
        */

        function seteaBotonConfirmar() {
            $("#<%=btnConfirmar.ClientID %>").unbind("click");
            $("#<%=btnConfirmar.ClientID %>").click(function () {
                if (!confirm('¿ Confirma que desea Confirmar la(s) transacciones Seleccionadas ?')) {
                    return false; 
                }
                else {
                    return true;
                }
            });
        }

        function validateBotonConfirmar() {
            if (!confirm('¿Confirma que desea Confirmar la(s) transacciones Seleccionadas?')) {
                return false;
            } else {
                return true;
            }
        }


    </script>
</asp:Content>
