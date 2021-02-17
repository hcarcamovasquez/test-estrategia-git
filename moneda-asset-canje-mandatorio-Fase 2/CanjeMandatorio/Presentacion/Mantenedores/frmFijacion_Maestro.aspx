<%@ Page Title="Fijación de Operaciones" Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false" CodeFile="frmFijacion_Maestro.aspx.vb" Inherits="Presentacion_Mantenedores_frmFijacion_Maestro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" />

    <h2 class="TdRedondeado titleMant">Fijación de<strong> Transacciones</strong></h2>

    <div class="card p-4 jumbotron">

        <div class="row">
            <!-- LISTA Tipo Transaccion-->
            <div class="col-md-4">
                <asp:Label runat="server" ID="lblTipoTransaccion">Tipo Transacción</asp:Label>
                <asp:DropDownList ID="ddlListaTipoTransaccion" CssClass="form-control js-select2-rut" runat="server" />
            </div>

            <!-- LISTA RUT FONDO-->
            <div class="col-md-4">
                <asp:Label runat="server" ID="rutfondo">Fondo</asp:Label>
                <asp:DropDownList ID="ddlListaRutFondo" CssClass="form-control js-select2-rut" runat="server" />
            </div>
            <!-- NEMOTÉCNICO -->
            <div class="col-md-4">
                <asp:Label runat="server" ID="nemotecnico">Nemotécnico</asp:Label>
                <asp:DropDownList ID="ddlListaNemotecnico" CssClass="form-control js-select2-rut" runat="server" />
            </div>
        </div>

        <div class="row mt-4">
            <!-- FIJACIÓN NAV -->
            <div class="col-md-3">
                <asp:Label runat="server" ID="Label6">Fijación NAV</asp:Label>
                <asp:DropDownList ID="ddlFijacionNav" CssClass="form-control js-select2-rut" runat="server" />
            </div>

            <!-- FECHA NAV DESDE -->
            <div class="col-md-3">
                <asp:Label runat="server" ID="Label4">Fecha NAV Desde</asp:Label>
                <div class="input-group">
                    <asp:TextBox ID="txtNAVDesde" runat="server" CssClass="form-control datepicker" ReadOnly="True"></asp:TextBox>

                    <asp:LinkButton ID="lnkBtnNAVDesde" Class="btn btn-moneda" runat="server" OnClientClick="return clickCalendar('txtNAVDesde')" 
                        ><i class="far fa-calendar-alt"></i></asp:LinkButton>
                    <asp:LinkButton ID="BtnLimpiarFechaDesde" Text=""  OnClientClick="return limpiarCalendar('txtNAVDesde')" class="btn btn-secondary ml-1" runat="server">
                        <i class="far fa-trash-alt"></i></asp:LinkButton>

                </div>
            </div>

            <!-- FECHA NAV HASTA -->
            <div class="col-md-3">
                <asp:Label runat="server" ID="Label7">Fecha NAV Hasta</asp:Label>
                <div class="input-group">
                    <asp:TextBox ID="txtNAVHasta" runat="server" CssClass="form-control datepicker" ReadOnly="True"></asp:TextBox>
                    <asp:LinkButton ID="lnkBtnNAVHasta" Class="btn btn-moneda" runat="server" OnClientClick="return clickCalendar('txtNAVHasta')">
                        <i class="far fa-calendar-alt"></i>
                    </asp:LinkButton>
                    <asp:LinkButton ID="LinkButton5" Text="" OnClientClick="return limpiarCalendar('txtNAVHasta')" class="btn btn-secondary ml-1" runat="server"><i class="far fa-trash-alt"></i></asp:LinkButton>
                   
                </div>
            </div>

            <!-- BOTÓN FIJAR NAV -->
            <div class="col-md-3 text-center">
                <asp:Button ID="BtnFijarNav" Text="Fijar NAV" class="btn btn-info mt-4 BtnFijarNav" runat="server" Enabled="false" OnClientClick="return validateBtn();" />
            </div>

        </div>

        <div class="row mt-4">
            <!-- FIJACIÓN TC OBSERVADO-->
            <div class="col-md-3">
                <asp:Label runat="server" ID="Label8">Fijación TC Observado</asp:Label>
                <asp:DropDownList ID="ddlFijacionTCObservacion" CssClass="form-control js-select2-rut" runat="server" />
            </div>
            <!-- FECHA TC OBSERVACION DESDE -->
            <div class="col-md-3">
                <asp:Label runat="server" ID="Label2">Fecha TC Obser. Desde</asp:Label>
                <div class="input-group">
                    <asp:TextBox ID="txtFechaTCDesde" runat="server" CssClass="form-control datepicker" ReadOnly="True"></asp:TextBox>
                    <asp:LinkButton ID="LinkButton1" Class="btn btn-moneda" runat="server" OnClientClick="return clickCalendar('txtFechaTCDesde')" 
                        ><i class="far fa-calendar-alt"></i></asp:LinkButton>
                    <asp:LinkButton ID="LinkButton6" Text="" class="btn btn-secondary ml-1" OnClientClick="return limpiarCalendar('txtFechaTCDesde')" runat="server"><i class="far fa-trash-alt"></i></asp:LinkButton>
                    
                </div>
            </div>

            <!-- FECHA TC OBSERVACION HASTA -->
            <div class="col-md-3">
                <asp:Label runat="server" ID="Label3">Fecha TC Obser. Hasta</asp:Label>
                <div class="input-group">
                    <asp:TextBox ID="txtFechaTCHasta" runat="server" CssClass="form-control datepicker" ReadOnly="True"></asp:TextBox>
                    <asp:LinkButton ID="LinkButton2" Class="btn btn-moneda" runat="server" 
                        OnClientClick="return clickCalendar('txtFechaTCHasta')" ><i class="far fa-calendar-alt"></i></asp:LinkButton>
                    <asp:LinkButton ID="LinkButton11" Text="" class="btn btn-secondary ml-1" OnClientClick="return limpiarCalendar('txtFechaTCHasta')" runat="server"><i class="far fa-trash-alt"></i></asp:LinkButton>

                   
                </div>
            </div>
            <!-- BOTÓN TC OBS -->
            <div class="col-md-3 text-center">
                <asp:Button ID="BtnTCObs" Text="TC Obser." class="btn btn-info mt-4 BtnTCObs" runat="server" Enabled="false" OnClientClick="return validateBtn();" />
            </div>
        </div>

        <div class="row mt-4">
            <div class="col-md-3">
            </div>
            <!-- FECHA TC OBSERVACION DESDE -->
            <div class="col-md-3">
                <asp:Label runat="server" ID="Label37">Fecha pago desde</asp:Label>
                <div class="input-group">
                    <asp:TextBox ID="txtFechaPagoDesde" runat="server" CssClass="form-control datepicker" ReadOnly="True"></asp:TextBox>
                    <asp:LinkButton ID="LinkButton3" Class="btn btn-moneda" runat="server" OnClientClick="return clickCalendar('txtFechaPagoDesde')" 
                        ><i class="far fa-calendar-alt"></i></asp:LinkButton>
                    <asp:LinkButton ID="LinkButton4" Text="" class="btn btn-secondary ml-1" OnClientClick="return limpiarCalendar('txtFechaPagoDesde')" runat="server"><i class="far fa-trash-alt"></i></asp:LinkButton>
                    
                </div>
            </div>

            <!-- FECHA TC OBSERVACION HASTA -->
            <div class="col-md-3">
                <asp:Label runat="server" ID="Label38">Fecha pago Hasta</asp:Label>
                <div class="input-group">
                    <asp:TextBox ID="txtFechaPagoHasta" runat="server" CssClass="form-control datepicker" ReadOnly="True"></asp:TextBox>
                    <asp:LinkButton ID="LinkButton12" Class="btn btn-moneda" runat="server" 
                        OnClientClick="return clickCalendar('txtFechaPagoHasta')" ><i class="far fa-calendar-alt"></i></asp:LinkButton>
                    <asp:LinkButton ID="LinkButton13" Text="" class="btn btn-secondary ml-1" 
                        OnClientClick="return limpiarCalendar('txtFechaPagoHasta')" runat="server"><i class="far fa-trash-alt"></i></asp:LinkButton>

                   
                </div>
            </div>
            <!-- BOTÓN TC OBS -->
            <div class="col-md-3 text-center">

            </div>
        </div>

        <!-- BOTONES BUSCAR LIMPIAR-->
        <div class="row text-center mt-5 p-3 border-bottom">
            <div class="col-md-12">
                <!-- BOTÓN BUSCAR -->
                <asp:Button ID="BtnBuscar" Text="Buscar" class="btn btn-moneda" runat="server" />
                <asp:Button ID="btnLimpiarFrm" Text="Borrar" class="btn btn-secondary" runat="server" OnClick="btnLimpiarFrm_Click" />
                
            </div>
        </div>

        <div class="row mt-3">

            <!-- FECHA SUSCRIPCIÓN HASTA-->
            <div class="col-md-5">
            </div>
        </div>

        <asp:HiddenField ID="txtAccionHidden" runat="server" />

        <!-- TABLA DE RESULTADOS -->
        <div class="row mt-3">
            <div class="col-md-8">
                <h5 class="mt-3 text-secondary"><i class="fas fa-file-invoice fa-sm"></i> Resultado de la búsqueda</h5>
            </div>
            <div class="col-md-4">
                <asp:Button ID="btnImprimir" Text="Imprimir" class="btn btn-moneda btnImprimir" runat="server" Enabled="false" />
            </div>
        </div>
        
        <div class="table-responsive card p-3">
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
                    <asp:BoundField DataField="FijacionNAV" HeaderText="Fijación NAV" />
                    <asp:BoundField DataField="FijacionTCObservado" HeaderText="Fijación TC Observado" />                    
                    <asp:BoundField DataField="Nemotecnico" HeaderText="Nemotécnico" />
                    <asp:BoundField DataField="MonedaPago" HeaderText="Moneda Pago" />
                    <asp:BoundField DataField="Cuotas" HeaderText="Cuotas" />
                    <asp:BoundField DataField="Monto" HeaderText="Monto" />
                </Columns>
            </asp:GridView>
        </div>
        <div class="row mt-3">
            <div class="col-md-12 text-center">
                <asp:Button ID="BtnModificar" runat="server" class="btn btn-info btnmodificar" Text="Fijación Manual" Enabled="false"></asp:Button>
                <asp:Button ID="BtnExportar" class="btn btn-success" Text="Exportar" runat="server" Enabled="false" />
            </div>
        </div>

    </div>
   
    <!-- Bootstrap Modal Dialog Suscripcione Crear/Modificar -->
    <div class="modal fade" id="myModalSuscripcion" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" data-keyboard="false" data-backdrop="static">
        <div class="modal-dialog" style="max-width: 90%;">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title">
                        <asp:Label ID="lbModalTittle" runat="server" Text="Formulario - Suscripciones" Font-Bold="true" Font-Size="X-Large">
                        </asp:Label>
                    </h4>
                    <button id="btnXCerrar" type="button" class="close" data-dismiss="modal" aria-label="Close" runat="server"><span aria-hidden="true">&times;</span></button>
                </div>
                <div class="modal-body">
                    <div class="jumbotron p-3">
                        <!-- FORMULARIO-->
                        <asp:HiddenField ID="txtEstadoCambio" runat="server"></asp:HiddenField>
                        <div class="col-md-12 mx-auto mt-10 " style="margin-top: 30px" visible="false">
                            <div class="col-md-12 mx-auto d-flex p-0 mb-10 justify-content-between" style="margin-top: 30px">
                                <!-- TARJETA 1 -->

                                <div class="card mt-0 col-md-6">
                                    <div class="card-body">
                                        <div class="row mt-3">
                                            <div class="col-md-6">
                                                <label class="form-control-label">Id Suscripción</label>
                                                <asp:TextBox ID="txtIdSuscripcion" runat="server" CssClass="form-control form-control-sm" ReadOnly="true"></asp:TextBox>
                                            </div>
                                            <div class="col-md-6">
                                                <label class="form-control-label">RUT Aportante</label>
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
                                            <div class="col-md-6">
                                                <label class="form-control-label">Nombre Aportante</label>
                                                <asp:UpdatePanel runat="server" ID="UpdatePanel2" UpdateMode="Conditional">
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="ddlModalRutAportante" EventName="" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddlModalMultifondo" EventName="" />
                                                    </Triggers>
                                                    <ContentTemplate>
                                                        <asp:DropDownList ID="ddlModalNombreAportante" CssClass="form-control js-select2-rut" runat="server" OnSelectedIndexChanged="CargarRutAportanteNemotecnicoPorNombreAportanteModal" AutoPostBack="true" />
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                             <div class="col-md-6">
                                                 <label class="form-control-label">Multifondo</label>
                                                <asp:UpdatePanel runat="server" ID="UpdatePanel7" UpdateMode="Conditional">
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="ddlModalRutAportante" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddlModalNombreAportante" EventName="SelectedIndexChanged" />
                                                    </Triggers>
                                                    <ContentTemplate>
                                                        <asp:DropDownList ID="ddlModalMultifondo" CssClass="form-control js-select2-rut" runat="server" OnSelectedIndexChanged="CargarRutYRazonSocialPorMultifondo" AutoPostBack="true" />
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>
                                       
                                        <div class="row mt-3">
                                           
                                            <div class="col-md-6">
                                                 <asp:Label runat="server" ID="Label10">Rut Fondo</asp:Label>
                                                <asp:UpdatePanel runat="server" ID="UpdatePanel9" UpdateMode="Conditional">
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="ddlNemotecnico" EventName="SelectedIndexChanged" />
                                                    </Triggers>
                                                    <ContentTemplate>
                                                        <asp:DropDownList ID="ddlFondo" CssClass="form-control js-select2-rut" runat="server" OnSelectedIndexChanged="SelectedIndexChangedFnRut" AutoPostBack="true" />
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                               <div class="col-md-6">
                                                <asp:Label runat="server" ID="Label27">Nombre Fondo</asp:Label>
                                                <asp:UpdatePanel runat="server" ID="UpdatePanel25" UpdateMode="Conditional">
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="ddlNemotecnico" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddlFondo" EventName="SelectedIndexChanged" />
                                                    </Triggers>
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtNombreFondo" CssClass="form-control" runat="server" ReadOnly="true" />
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>
                                        <div class="row mt-3">
                                            <div class="col-md-6">
                                                <asp:Label runat="server" ID="LbNemotecnico">Nemotécnico</asp:Label>
                                                <asp:UpdatePanel runat="server" ID="UpdatePanel8" UpdateMode="Conditional">
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="ddlFondo" EventName="SelectedIndexChanged" />
                                                    </Triggers>
                                                    <ContentTemplate>
                                                        <asp:DropDownList ID="ddlNemotecnico" CssClass="form-control js-select2-rut" runat="server" OnSelectedIndexChanged="LlenarPorNemotecnico" AutoPostBack="true" />
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                            <div class="col-md-6">
                                                <asp:Label runat="server" ID="Label28">Serie</asp:Label>
                                                <asp:UpdatePanel runat="server" ID="UpdatePanel26" UpdateMode="Conditional">
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="ddlNemotecnico" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddlFondo" EventName="SelectedIndexChanged" />
                                                    </Triggers>
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtNombreSerie" CssClass="form-control" runat="server" ReadOnly="true" />
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>
                                        <div class="row mt-3">
                                            <div class="col-md-6">
                                                <asp:Label runat="server" ID="Label29">Moneda Serie</asp:Label>
                                                <asp:UpdatePanel runat="server" ID="UpdatePanel27" UpdateMode="Conditional">
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="ddlNemotecnico" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddlFondo" EventName="SelectedIndexChanged" />
                                                    </Triggers>
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtMonedaSerie" CssClass="form-control" runat="server" ReadOnly="true" />
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                            <div class="col-md-6">
                                                <asp:Label runat="server" ID="lblMonedaPago">Moneda pago</asp:Label>
                                                <asp:DropDownList ID="ddlMonedaPago" CssClass="form-control js-select2-rut" runat="server" />
                                            </div>
                                        </div>
                                        
                                        <div class="row mt-3">
                                            <div class="col-md-6">
                                                <asp:Label runat="server" ID="lbNAV">NAV</asp:Label>
                                                <asp:UpdatePanel runat="server" ID="UpdatePanel19" UpdateMode="Conditional">
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="ddlNemotecnico" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddlFondo" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtMonto" EventName="TextChanged" />
                                                    </Triggers>
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtNAV" CssClass="form-control dbs-entero20-decimal6" runat="server" OnTextChanged="CuotasTextChanged" AutoPostBack="true" />
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>   
                                            <div class="col-md-6">
                                                <asp:Label runat="server" ID="Label11">NAV (CLP)</asp:Label>
                                                <asp:UpdatePanel runat="server" ID="UpdatePanel23" UpdateMode="Conditional">
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
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>
                                        
                                    </div>
                                </div>
                                <!-- TARJETA 2 -->
                                <div class="card mt- d-flex col-md-6 justify-content-between">
                                    <div class="card-body ">
                                        <div class="row mt-3">
                                            <div class="col-md-6">
                                                <asp:Label runat="server" ID="Label15">Fecha intención</asp:Label>
                                                <asp:UpdatePanel runat="server" ID="UpdatePanel3" UpdateMode="Conditional">
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
                                                </asp:UpdatePanel>
                                            </div>
                                            <div class="col-md-6">
                                                <asp:Label runat="server" ID="Label5">Fecha NAV</asp:Label>
                                                <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Conditional">
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
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>
                                        <div class="row mt-3">
                                            <div class="col-md-6">
                                                <asp:Label runat="server" ID="Label1">Fecha suscripción</asp:Label>
                                                <asp:UpdatePanel runat="server" ID="UpdatePanel4" UpdateMode="Conditional">
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
                                                </asp:UpdatePanel>
                                            </div>
                                            <div class="col-md-6">
                                                <asp:Label runat="server" ID="Label13">Fecha TC Obs</asp:Label>
                                                <asp:UpdatePanel runat="server" ID="UpdatePanel5" UpdateMode="Conditional">
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
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>
                                        <div class="row mt-3">
                                            <div class="col-md-6">
                                                <asp:Label runat="server" ID="lbTC">TC Observado</asp:Label>
                                                <asp:UpdatePanel runat="server" ID="UpdatePanel20" UpdateMode="Conditional">
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="ddlNemotecnico" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddlFondo" EventName="SelectedIndexChanged" />
                                                    </Triggers>
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtTCObservado" CssClass="form-control dbs-entero20-decimal6" runat="server" OnTextChanged="TcObservadoChanged" AutoPostBack="true" />
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                            <div class="col-md-6">
                                                <asp:Label runat="server" ID="lbCuotas">Cuotas</asp:Label>
                                                <asp:UpdatePanel runat="server" ID="UpdatePanel24" UpdateMode="Conditional">
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="ddlNemotecnico" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddlFondo" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtMonto" EventName="TextChanged" />
                                                    </Triggers>
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtCuotas" CssClass="form-control dbs-entero-decimal" runat="server" OnTextChanged="CuotasTextChanged" AutoPostBack="true" AutoComplete="off" />
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>

                                        <div class="row mt-3">
                                            <div class="col-md-6">
                                                <asp:Label runat="server" ID="Label14">Monto</asp:Label>
                                                <asp:UpdatePanel runat="server" ID="UpdatePanel21" UpdateMode="Conditional">
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="ddlNemotecnico" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddlFondo" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtNAV" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtCuotas" EventName="TextChanged" />
                                                    </Triggers>
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtMonto" CssClass="form-control dbs-entero20-decimal2" runat="server" OnTextChanged="MontoTextChanged" AutoPostBack="true" AutoComplete="off" />
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                            <div class="col-md-6">
                                                <asp:Label runat="server" ID="Label16">Monto (CLP)</asp:Label>
                                                <asp:UpdatePanel runat="server" ID="UpdatePanel16" UpdateMode="Conditional">
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
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>
                                        <div class="row mt-3">
                                            <div class="col-md-6">
                                                <asp:Label runat="server" ID="Label17">Contrato</asp:Label>
                                                <asp:DropDownList ID="ddlContrato" CssClass="form-control js-select2-rut" runat="server" />
                                            </div>
                                            <div class="col-md-6">
                                                <asp:Label runat="server" ID="lbPoderes">Poderes</asp:Label>
                                                <asp:DropDownList ID="ddlPoderes" CssClass="form-control js-select2-rut" runat="server" />
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
                                                <asp:Label runat="server" ID="Label12">Fecha DCV</asp:Label>
                                            </div>
                                            <div class="col-md-6">
                                                <asp:UpdatePanel runat="server" ID="UpdatePanel15" UpdateMode="Conditional">
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="ddlNemotecnico" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddlFondo" EventName="SelectedIndexChanged" />
                                                    </Triggers>
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtFechaDCV" CssClass="form-control form-control-sm" runat="server" ReadOnly="True" />
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>
                                        <div class="row mt-3">
                                            <div class="col-md-6">
                                                <asp:Label runat="server" ID="Label18">Estado</asp:Label>
                                            </div>
                                            <div class="col-md-6">
                                                <asp:UpdatePanel runat="server" ID="UpdatePanel22" UpdateMode="Conditional">
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="ddlNemotecnico" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddlFondo" EventName="SelectedIndexChanged" />
                                                    </Triggers>
                                                    <ContentTemplate>
                                                        <asp:DropDownList ID="ddlEstado1" CssClass="form-control js-select2-rut" runat="server" SelectedIndexChanged="llenarCLP" />
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>

                                            </div>
                                        </div>
                                        <div class="row mt-3">
                                            <div class="col-md-6">
                                                <asp:Label runat="server" ID="Label19">Observaciones</asp:Label>
                                            </div>
                                            <div class="col-md-6">
                                                <asp:TextBox ID="txtObservaciones" CssClass="form-control form-control-sm" runat="server" />
                                            </div>
                                        </div>
                                        <div class="row mt-3">
                                            <div class="col-md-6">
                                                <asp:Label runat="server" ID="Label20">Fijación NAV</asp:Label>
                                            </div>
                                            <div class="col-md-6">
                                                <asp:UpdatePanel runat="server" ID="UpdatePanel17" UpdateMode="Conditional">
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="ddlNemotecnico" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddlFondo" EventName="SelectedIndexChanged" />
                                                    </Triggers>
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtFijacionNAV" Text="" CssClass="form-control form-control-sm" runat="server" ReadOnly="True" />
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>
                                        <div class="row mt-3">
                                            <div class="col-md-6">
                                                <asp:Label runat="server" ID="Label21">Fijación TC Obs</asp:Label>
                                            </div>
                                            <div class="col-md-6">
                                                <asp:UpdatePanel runat="server" ID="UpdatePanel18" UpdateMode="Conditional">
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="ddlNemotecnico" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddlFondo" EventName="SelectedIndexChanged" />
                                                    </Triggers>
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtFijacionTCObs" Text="" CssClass="form-control form-control-sm" runat="server" ReadOnly="True" />
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
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
                                                <asp:Label runat="server" ID="Label34">Cuotas emitidas</asp:Label>

                                                <asp:UpdatePanel runat="server" ID="UpdatePanel89" UpdateMode="Conditional">
                                                    <Triggers>
                                                    </Triggers>
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtCuotasEmitidas" CssClass="form-control dbs-entero16-decimal0" runat="server" ReadOnly="true" EventName="SelectionChanged" />
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                            <div class="col-md-6">
                                                <asp:Label runat="server" ID="Label31">Acumulada</asp:Label>

                                                <asp:UpdatePanel runat="server" ID="UpdatePanel90" UpdateMode="Conditional">
                                                    <Triggers>
                                                    </Triggers>
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtAcumulada" CssClass="form-control dbs-entero16-decimal0" runat="server" ReadOnly="true" EventName="SelectionChanged" />
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>


                                        <div class="row mt-3">
                                            <div class="col-md-6">
                                                <asp:Label runat="server" ID="Label32">Actual</asp:Label>

                                                <asp:UpdatePanel runat="server" ID="UpdatePanel91" UpdateMode="Conditional">
                                                    <Triggers>
                                                    </Triggers>
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtActual" CssClass="form-control dbs-entero16-decimal0" runat="server" ReadOnly="true" EventName="SelectionChanged" />
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                            <div class="col-md-6">
                                                <asp:Label runat="server" ID="Label33">Utilizado</asp:Label>

                                                <asp:UpdatePanel runat="server" ID="UpdatePanel92" UpdateMode="Conditional">
                                                    <Triggers>
                                                    </Triggers>
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtUtilizado" CssClass="form-control dbs-entero16-decimal0" runat="server" ReadOnly="true" EventName="SelectionChanged" />
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>



                                        <div class="row mt-3">
                                            <div class="col-md-6">
                                                <asp:Label runat="server" ID="Label35">DISPONIBLES</asp:Label>

                                                <asp:UpdatePanel runat="server" ID="UpdatePanel93" UpdateMode="Conditional">
                                                    <Triggers>
                                                    </Triggers>
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtDisponiblesEmitidas" CssClass="form-control dbs-entero16-decimal0" runat="server" ReadOnly="true" EventName="SelectionChanged" />
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
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
                                                <asp:Label runat="server" ID="Label22">Cuotas DCV</asp:Label>

                                                <asp:UpdatePanel runat="server" ID="UpdatePanel12" UpdateMode="Conditional">
                                                    <Triggers>
                                                    </Triggers>
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtCuotasDCV" CssClass="form-control dbs-entero16-decimal0" runat="server" ReadOnly="True" />
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                            <div class="col-md-6">
                                                <asp:Label runat="server" ID="Label23">Rescates</asp:Label>

                                                <asp:UpdatePanel runat="server" ID="UpdatePanel10" UpdateMode="Conditional">
                                                    <Triggers>
                                                    </Triggers>
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtRescates" CssClass="form-control dbs-entero16-decimal0" runat="server" ReadOnly="True" />
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>
                                        <div class="row mt-3">
                                            <div class="col-md-6">
                                                <asp:Label runat="server" ID="Label24">Suscripciones</asp:Label>

                                                <asp:UpdatePanel runat="server" ID="UpdatePanel11" UpdateMode="Conditional">
                                                    <Triggers>
                                                    </Triggers>
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtSuscripciones" CssClass="form-control dbs-entero16-decimal0" runat="server" ReadOnly="True" />
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                            <div class="col-md-6">
                                                <asp:Label runat="server" ID="Label25">Canje</asp:Label>

                                                <asp:UpdatePanel runat="server" ID="UpdatePanel13" UpdateMode="Conditional">
                                                    <Triggers>
                                                    </Triggers>
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtCanje" CssClass="form-control dbs-entero16-decimal0" runat="server" ReadOnly="True" />
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>
                                        <div class="row mt-3">
                                            <div class="col-md-6">
                                                <asp:Label runat="server" ID="Label26">DISPONIBLES</asp:Label>

                                                <asp:UpdatePanel runat="server" ID="UpdatePanel14" UpdateMode="Conditional">
                                                    <Triggers>
                                                    </Triggers>
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtDisponibles" CssClass="form-control dbs-entero16-decimal0" runat="server" ReadOnly="True" />
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
                                <asp:Button ID="btnModalModificar" Text="Modificar" CssClass="btn btn-info" runat="server" OnClientClick="if (!confirm('¿Seguro que desea guardar los elementos cambios?')) return false;"></asp:Button>
                                <asp:Button ID="btnModalCancelar" Text="Cancelar" CssClass="btn btn-secondary" runat="server" OnClientClick="if (!confirm('¿Seguro que desea Cancelar?')) return false;"></asp:Button>
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
                        <asp:Label ID="Label9" runat="server" Text="Formulario - Canjes" Font-Bold="true" Font-Size="X-Large">
                        </asp:Label>
                    </h4>
                    <button id="Button1" type="button" class="close" data-dismiss="modal" aria-label="Close" runat="server"><span aria-hidden="true">&times;</span></button>
                </div>
                <div class="modal-body">
                    <div class="jumbotron p-3">
                        <asp:HiddenField ID="HiddenField1" runat="server"></asp:HiddenField>
                        <div class="col-md-12 mt-10"  visible="false">
                                    <div class="col-md-12 d-flex p-0 mb-10">
                                <!-- TARJETA 1 -->
                                <div class="card mt-0 col-md-12">
                                    <div class="card-body">
                                        <div class="row mt-3">
                                            <div class="col-md-1">
                                                <label class="form-control-label">Rut Aportante</label>

                                            </div>
                                            <div class="col-md-3">
                                                <asp:UpdatePanel runat="server" ID="UpdatePanel28" UpdateMode="Conditional">
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="ddlModalMultifondoCanje" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddlModalNombreAportanteCanje" EventName="SelectedIndexChanged" />
                                                    </Triggers>
                                                    <ContentTemplate>
                                                        <asp:DropDownList ID="ddlModalRutAportanteCanje" CssClass="form-control js-select2-rut" runat="server" AutoPostBack="true" />
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>

                                            </div>
                                            <div class="col-md-1">
                                                <label class="form-control-label">Nombre Aportante</label>
                                            </div>
                                            <div class="col-md-3">
                                                <asp:UpdatePanel runat="server" ID="UpdatePanel29" UpdateMode="Conditional">
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="ddlModalRutAportanteCanje" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddlModalMultifondoCanje" EventName="SelectedIndexChanged" />
                                                    </Triggers>
                                                    <ContentTemplate>
                                                        <asp:DropDownList ID="ddlModalNombreAportanteCanje" CssClass="form-control js-select2-rut" runat="server" AutoPostBack="true" />
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                            <div class="col-md-1">
                                                <label class="form-control-label">Multifondo</label>
                                            </div>
                                            <div class="col-md-3">
                                                <asp:UpdatePanel runat="server" ID="UpdatePanel30" UpdateMode="Conditional">
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="ddlModalRutAportanteCanje" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddlModalNombreAportanteCanje" EventName="SelectedIndexChanged" />
                                                    </Triggers>
                                                    <ContentTemplate>
                                                        <asp:DropDownList ID="ddlModalMultifondoCanje" CssClass="form-control js-select2-rut" runat="server" AutoPostBack="true" />
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>

                                            </div>
                                        </div>
                                        <div class="row mt-3">
                                            <div class="col-md-1">
                                                <label class="form-control-label">Fondo</label>
                                            </div>
                                            <div class="col-md-3">
                                                <asp:UpdatePanel runat="server" ID="UpdatePanel31" UpdateMode="Conditional">
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="ddlModalNemotecnicoEntranteCanje" EventName="SelectedIndexChanged" />
                                                    </Triggers>
                                                    <ContentTemplate>
                                                        <asp:DropDownList ID="ddlModalFondoCanje" CssClass="form-control js-select2-rut" runat="server" AutoPostBack="true" />
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>

                                            </div>
                                            <div class="col-md-1">
                                                <label class="form-control-label">Tipo Transacción</label>
                                            </div>
                                            <div class="col-md-3">
                                                <asp:TextBox ID="txtModalTipoTrnasaccion" CssClass="form-control" runat="server" Enabled="false">Canje</asp:TextBox>
                                            </div>
                                            <div class="col-md-1">
                                                <label class="form-control-label">Fecha Solicitud</label>
                                            </div>
                                            <div class="col-md-3">
                                                <asp:UpdatePanel runat="server" ID="UpdatePanel32" UpdateMode="Conditional">
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
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>
                                        <div class="row mt-2">
                                            <div class="col-md-1">
                                                <label class="form-control-label">Nombre Fondo</label>
                                            </div>
                                            <div class="col-md-3">

                                                <asp:UpdatePanel runat="server" ID="UpdatePanel33" UpdateMode="Conditional">
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="ddlModalFondoCanje" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddlModalNemotecnicoEntranteCanje" EventName="SelectedIndexChanged" />
                                                    </Triggers>
                                                    <ContentTemplate>
                                                        <asp:DropDownList ID="ddlModalNombreFondoCanje" CssClass="form-control js-select2-rut" runat="server" />
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                            <div class="col-md-1">
                                                <label class="form-control-label">Fijación TC</label>
                                            </div>
                                            <div class="col-md-3">
                                                <asp:UpdatePanel runat="server" ID="UpdatePanel36" UpdateMode="Conditional">
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
                                                </asp:UpdatePanel>

                                            </div>

                                            <div class="col-md-1">
                                                <label class="form-control-label">Fecha de Canje</label>
                                            </div>
                                            <div class="col-md-3">
                                                <asp:UpdatePanel runat="server" ID="UpdatePanelFC" UpdateMode="Conditional">
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
                                                </asp:UpdatePanel>
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
                                                <asp:UpdatePanel runat="server" ID="UpdatePanel35" UpdateMode="Conditional">
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="ddlModalFondoCanje" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddlModalNemotecnicoEntranteCanje" EventName="SelectedIndexChanged" />
                                                    </Triggers>
                                                    <ContentTemplate>
                                                        <asp:DropDownList ID="ddlModalNemotecnicoSalienteCanje" CssClass="form-control js-select2-rut" runat="server" AutoPostBack="true" />
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>

                                            </div>
                                        </div>
                                        <div class="row mt-2">
                                            <div class="col-md-3">
                                                <label class="form-control-label">Fecha Nav</label>
                                            </div>
                                            <div class="col-md-9">
                                                <asp:UpdatePanel runat="server" ID="UpdatePanel37" UpdateMode="Conditional">
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
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>
                                        <div class="row mt-2">
                                            <div class="col-md-2">
                                                <label class="form-control-label">Serie</label>

                                            </div>
                                            <div class="col-md-4">
                                                <asp:UpdatePanel runat="server" ID="UpdatePanel38" UpdateMode="Conditional">
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="ddlModalNemotecnicoSalienteCanje" EventName="SelectedIndexChanged" />
                                                    </Triggers>
                                                    <ContentTemplate>
                                                        <asp:DropDownList ID="ddlModalSerieSalienteCanje" CssClass="form-control js-select2-rut" runat="server" />
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>

                                            </div>
                                            <div class="col-md-2">
                                                <label class="form-control-label">Moneda</label>
                                            </div>
                                            <div class="col-md-4">
                                                <asp:UpdatePanel runat="server" ID="UpdatePanel39" UpdateMode="Conditional">
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="ddlModalNemotecnicoSalienteCanje" EventName="SelectedIndexChanged" />
                                                    </Triggers>
                                                    <ContentTemplate>
                                                        <asp:DropDownList ID="ddlModalMonedaSalienteCanje" CssClass="form-control js-select2-rut" runat="server" />
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>

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
                                                <asp:UpdatePanel runat="server" ID="UpdatePanel40" UpdateMode="Conditional">
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="ddlModalNemotecnicoSalienteCanje" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtModalNavSaliente" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtModalNavEntrante" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtModalTipoCambio" EventName="TextChanged" />
                                                    </Triggers>
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtFactorSaliente" CssClass="form-control dbs-entero17-decimal9" runat="server"></asp:TextBox>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>

                                            </div>

                                        </div>
                                        <div class="row mt-2">
                                            <div class="col-md-2">
                                                <label class="form-control-label">Nav</label>

                                            </div>
                                            <div class="col-md-4">
                                                <asp:UpdatePanel runat="server" ID="UpdatePanel41" UpdateMode="Conditional">
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="ddlModalNemotecnicoSalienteCanje" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtModalTipoCambio" EventName="TextChanged" />

                                                    </Triggers>
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtModalNavSaliente" OnTextChanged="calcularFactor" AutoPostBack="true" CssClass="form-control dbs-entero20-decimal6" runat="server"> </asp:TextBox>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>

                                            </div>
                                            <div class="col-md-2">
                                                <label class="form-control-label">Nav (CLP)</label>

                                            </div>
                                            <div class="col-md-4">
                                                <asp:UpdatePanel runat="server" ID="UpdatePanel42" UpdateMode="Conditional">
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="ddlModalNemotecnicoSalienteCanje" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtModalNavSaliente" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtModalTipoCambio" EventName="TextChanged" />
                                                    </Triggers>
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtModalNavCLPSaliente" CssClass="form-control dbs-entero20-decimal4" runat="server"></asp:TextBox>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>

                                            </div>
                                        </div>
                                        <div class="row mt-2">
                                            <div class="col-md-2">
                                                <label class="form-control-label">Monto</label>

                                            </div>
                                            <div class="col-md-4">
                                                <asp:UpdatePanel runat="server" ID="UpdatePanel43" UpdateMode="Conditional">
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="txtModalNavSaliente" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtModalCuotaSaliente" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtModalTipoCambio" EventName="TextChanged" />
                                                    </Triggers>
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtModalMontoSaliente" CssClass="form-control dbs-entero20-decimal2" runat="server"></asp:TextBox>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>

                                            </div>
                                            <div class="col-md-2">
                                                <label class="form-control-label">Monto (CLP)</label>

                                            </div>
                                            <div class="col-md-4">
                                                <asp:UpdatePanel runat="server" ID="UpdatePanel44" UpdateMode="Conditional">
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="txtModalNavSaliente" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtModalTipoCambio" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtModalCuotaSaliente" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtModalMontoSaliente" EventName="TextChanged" />
                                                    </Triggers>
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtModalMontoCLPSaliente" CssClass="form-control dbs-entero20-decimal2" runat="server"></asp:TextBox>

                                                    </ContentTemplate>
                                                </asp:UpdatePanel>

                                            </div>
                                        </div>
                                        <div class="row mt-2">
                                            <div class="col-md-2">
                                                <label class="form-control-label">Diferencia</label>

                                            </div>
                                            <div class="col-md-4">
                                                <asp:UpdatePanel runat="server" ID="UpdatePanel45" UpdateMode="Conditional">
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
                                                </asp:UpdatePanel>
                                            </div>
                                            <div class="col-md-2">
                                                <label class="form-control-label">Diferencia CLP</label>

                                            </div>
                                            <div class="col-md-4">
                                                <asp:UpdatePanel runat="server" ID="UpdatePanel46" UpdateMode="Conditional">
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
                                                </asp:UpdatePanel>
                                            </div>

                                        </div>
                                        <div class="row mt-2">
                                            <div class="col-md-3">
                                                <label class="form-control-label">Fijación Nav</label>
                                            </div>
                                            <div class="col-md-9">
                                                <asp:UpdatePanel runat="server" ID="UpdatePanel56" UpdateMode="Conditional">
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
                                                </asp:UpdatePanel>

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
                                                <asp:UpdatePanel runat="server" ID="UpdatePanel47" UpdateMode="Conditional">
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="ddlModalFondoCanje" EventName="SelectedIndexChanged" />

                                                    </Triggers>
                                                    <ContentTemplate>
                                                        <asp:DropDownList ID="ddlModalNemotecnicoEntranteCanje" CssClass="form-control js-select2-rut" AutoPostBack="true" runat="server" />
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>
                                        <div class="row mt-2">
                                            <div class="col-md-3">
                                                <label class="form-control-label">Fecha Nav</label>
                                            </div>
                                            <div class="col-md-9">
                                                <asp:UpdatePanel runat="server" ID="UpdatePanel48" UpdateMode="Conditional">
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
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>
                                        <div class="row mt-2">
                                            <div class="col-md-2">
                                                <label class="form-control-label">Serie</label>

                                            </div>
                                            <div class="col-md-4">
                                                <asp:UpdatePanel runat="server" ID="UpdatePanel49" UpdateMode="Conditional">
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="ddlModalNemotecnicoEntranteCanje" EventName="SelectedIndexChanged" />
                                                    </Triggers>
                                                    <ContentTemplate>
                                                        <asp:DropDownList ID="ddlModalSerieEntranteCanje" CssClass="form-control js-select2-rut" runat="server" />
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>

                                            </div>
                                            <div class="col-md-2">
                                                <label class="form-control-label">Moneda</label>
                                            </div>
                                            <div class="col-md-4">
                                                <asp:UpdatePanel runat="server" ID="UpdatePanel50" UpdateMode="Conditional">
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="ddlModalNemotecnicoEntranteCanje" EventName="SelectedIndexChanged" />
                                                    </Triggers>
                                                    <ContentTemplate>
                                                        <asp:DropDownList ID="ddlModalMonedaEntranteCanje" CssClass="form-control js-select2-rut" runat="server" />
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>

                                            </div>
                                        </div>
                                        <div class="row mt-2">
                                            <div class="col-md-2">
                                                <label class="form-control-label">Cuotas</label>

                                            </div>
                                            <div class="col-md-4">
                                                <asp:UpdatePanel runat="server" ID="UpdatePanel51" UpdateMode="Conditional">
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
                                                </asp:UpdatePanel>

                                            </div>
                                            <div class="col-md-2">
                                                <label class="form-control-label">Factor</label>

                                            </div>
                                            <div class="col-md-4">
                                                <asp:UpdatePanel runat="server" ID="UpdatePanel200" UpdateMode="Conditional">
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
                                                </asp:UpdatePanel>

                                            </div>
                                        </div>
                                        <div class="row mt-2">
                                            <div class="col-md-2">
                                                <label class="form-control-label">Nav</label>

                                            </div>
                                            <div class="col-md-4">
                                                <asp:UpdatePanel runat="server" ID="UpdatePanel52" UpdateMode="Conditional">
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="ddlModalNemotecnicoEntranteCanje" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtModalTipoCambio" EventName="TextChanged" />
                                                    </Triggers>
                                                    <ContentTemplate>
                                                        <asp:Textbox ID="txtModalNavEntrante" CssClass="form-control dbs-entero20-decimal6" runat="server" onpaste="return false" oncut="return false" oncopy="return false" onchange="validarValorEntrante()" OnTextChanged="calcularFactor" AutoPostBack="true"></asp:Textbox>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                            <div class="col-md-2">
                                                <label class="form-control-label">Nav (CLP)</label>

                                            </div>
                                            <div class="col-md-4">
                                                <asp:UpdatePanel runat="server" ID="UpdatePanel53" UpdateMode="Conditional">
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="txtModalNavEntrante" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtModalCuotaSaliente" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddlModalNemotecnicoEntranteCanje" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtModalTipoCambio" EventName="TextChanged" />
                                                    </Triggers>
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtModalNavCLPEntrante" CssClass="form-control dbs-entero20-decimal4" runat="server"></asp:TextBox>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>
                                        <div class="row mt-2">
                                            <div class="col-md-2">
                                                <label class="form-control-label">Monto</label>

                                            </div>
                                            <div class="col-md-4">
                                                <asp:UpdatePanel runat="server" ID="UpdatePanel54" UpdateMode="Conditional">
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
                                                </asp:UpdatePanel>

                                            </div>
                                            <div class="col-md-2">
                                                <label class="form-control-label">Monto (CLP)</label>

                                            </div>
                                            <div class="col-md-4">
                                                <asp:UpdatePanel runat="server" ID="UpdatePanel55" UpdateMode="Conditional">
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
                                                </asp:UpdatePanel>

                                            </div>
                                         </div>   
                                        <div class="row mt-2">
                                            <div class="col-md-2">
                                                <label class="form-control-label">Fecha Observado</label>
                                            </div>
                                            <div class="col-md-4">
                                                <asp:UpdatePanel runat="server" ID="UpdatePanel34" UpdateMode="Conditional">
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
                                                </asp:UpdatePanel>
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
                                                <asp:UpdatePanel runat="server" ID="UpdatePanel57" UpdateMode="Conditional">
                                                    <Triggers>

                                                        <asp:AsyncPostBackTrigger ControlID="ddlModalNemotecnicoSalienteCanje" EventName="SelectedIndexChanged" />
                                                    </Triggers>
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtModalFechaCuotaDCV" runat="server" CssClass="form-control" ReadOnly="True"></asp:TextBox>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                            <div class="col-md-2">
                                                <label class="form-control-label">Cuotas DCV</label>
                                            </div>
                                            <div class="col-md-4">
                                                <asp:UpdatePanel runat="server" ID="UpdatePanel58" UpdateMode="Conditional">
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="ddlModalNemotecnicoSalienteCanje" EventName="SelectedIndexChanged" />
                                                    </Triggers>
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtModalCuotaDCV" CssClass="form-control dbs-entero18-decimal0" runat="server" ReadOnly="True"></asp:TextBox>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>

                                            </div>
                                        </div>
                                        <div class="row mt-2">
                                            <div class="col-md-2">
                                                <label class="form-control-label">Rescates</label>

                                            </div>
                                            <div class="col-md-4">
                                                <asp:UpdatePanel runat="server" ID="UpdatePanel59" UpdateMode="Conditional">
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="ddlModalNemotecnicoSalienteCanje" EventName="SelectedIndexChanged" />
                                                    </Triggers>
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtModalRescateTransito" CssClass="form-control dbs-entero10-decimal0" runat="server"></asp:TextBox>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>

                                            </div>
                                            <div class="col-md-2">
                                                <label class="form-control-label">Canjes</label>
                                            </div>
                                            <div class="col-md-4">
                                                <asp:UpdatePanel runat="server" ID="UpdatePanel60" UpdateMode="Conditional">
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="ddlModalNemotecnicoSalienteCanje" EventName="SelectedIndexChanged" />
                                                    </Triggers>
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtModalCanjeTransito" CssClass="form-control dbs-entero10-decimal0" runat="server"></asp:TextBox>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>

                                            </div>
                                        </div>

                                        <div class="row mt-2">
                                            <div class="col-md-3">
                                                <label class="form-control-label">Suscripciones</label>

                                            </div>
                                            <div class="col-md-3">
                                                <asp:UpdatePanel runat="server" ID="UpdatePanel61" UpdateMode="Conditional">
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="ddlModalNemotecnicoSalienteCanje" EventName="SelectedIndexChanged" />
                                                    </Triggers>
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtModalSuscripcionTransito" CssClass="form-control dbs-entero10-decimal0" runat="server"></asp:TextBox>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                            <div class="col-md-2">
                                                <label class="form-control-label">Cuotas Disponibles</label>
                                            </div>
                                            <div class="col-md-4">
                                                <asp:UpdatePanel runat="server" ID="UpdatePanel62" UpdateMode="Conditional">
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="ddlModalNemotecnicoSalienteCanje" EventName="SelectedIndexChanged" />
                                                    </Triggers>
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtModalCuotasDisponibles" CssClass="form-control dbs-entero18-decimal0" runat="server"></asp:TextBox>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>

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
                                                <asp:UpdatePanel runat="server" ID="UpdatePanel63" UpdateMode="Conditional">
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="ddlModalNemotecnicoSalienteCanje" EventName="SelectedIndexChanged" />
                                                    </Triggers>
                                                    <ContentTemplate>
                                                        <asp:Textbox ID="txtModalTipoCambio" CssClass="form-control dbs-entero20-decimal6" runat="server" onpaste="return false" oncut="return false" OnTextChanged="cambioTC" AutoPostBack="true"></asp:Textbox>
                                                           </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                            <div class="col-md-2">
                                                <label class="form-control-label">Contrato</label>
                                            </div>
                                            <div class="col-md-4">
                                                <asp:DropDownList ID="ddlModalContrato" CssClass="form-control js-select2-rut" runat="server">
                                                    <asp:ListItem Value="">Seleccione una opción</asp:ListItem>
                                                    <asp:ListItem Value="OK">OK</asp:ListItem>
                                                    <asp:ListItem Value="No OK">No OK</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="row mt-2">
                                            <div class="col-md-2">
                                                <label class="form-control-label">Estado</label>
                                            </div>
                                            <div class="col-md-4">
                                                <asp:DropDownList ID="ddlModalEstado" CssClass="form-control js-select2-rut" runat="server">
                                                    <asp:ListItem Value="">Seleccione una opción</asp:ListItem>
                                                    <asp:ListItem Value="Pendiente">Pendiente</asp:ListItem>
                                                    <asp:ListItem Value="Cerrado">Cerrado</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-md-2">
                                                <label class="form-control-label">Poderes</label>
                                            </div>
                                            <div class="col-md-4">
                                                <asp:DropDownList ID="ddlModalPoderes" CssClass="form-control js-select2-rut" runat="server">
                                                    <asp:ListItem Value="">Seleccione una opción</asp:ListItem>
                                                    <asp:ListItem Value="OK">OK</asp:ListItem>
                                                    <asp:ListItem Value="No OK">No OK</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="row mt-2">
                                            <div class="col-md-3">
                                                <label class="form-control-label">Observaciones</label>

                                            </div>
                                            <div class="col-md-9">
                                                <asp:TextBox ID="txtModalObservaciones" CssClass="form-control" runat="server" TextMode="MultiLine"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="row mt-2">
                                            <div class="col-md-3">
                                                <label class="form-control-label">Id Canje</label>

                                            </div>
                                            <div class="col-md-9">
                                                <asp:TextBox ID="txtIdCanje" CssClass="form-control" runat="server" ReadOnly="true"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>


                            </div>




                            <!-- GRUPO DE BOTONES 2 -->
                            <div class="form-group mt-5 text-right">
                                <div class="col-md-offset-1">
                                    <asp:Button ID="btnModalModificarCanje" Text="Modificar" CssClass="btn btn-info w-25" runat="server" OnClientClick="if (!confirm('¿Seguro que desea guardar los elementos cambios?')) return false;"></asp:Button>
                                    <asp:Button ID="btnModalCancelarCanje" Text="Cancelar" CssClass="btn btn-secondary w-25" Width="15%" runat="server" OnClientClick="if (!confirm('¿Seguro que desea Cancelar?')) return false;"></asp:Button>
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
                        <asp:Label ID="Label30" runat="server" Text="Formulario - Rescates" Font-Bold="true" Font-Size="X-Large">
                        </asp:Label>
                    </h4>
                    <button id="Button6" type="button" class="close" data-dismiss="modal" aria-label="Close" runat="server"><span aria-hidden="true">&times;</span></button>
                </div>
                <div class="modal-body">
                    <div class="jumbotron">
                        <div class="card p-4">
                            <div class="row">
                                <!-- FORMULARIO-->
                                <asp:TextBox ID="txtIDRescate" runat="server" Visible="false"></asp:TextBox>

                                <div class="col-lg-6">
                                    <div class="card h-30 mt-0">
                                        <div class="card-body">
                                            <hr />
                                            <div class="row mt-3">
                                                <div class="col-md-5">
                                                    <label class="form-control-label">RUT Aportante</label>
                                                </div>
                                                <div class="col-md-6">
                                                    <asp:UpdatePanel runat="server" ID="UpdatePanel64" UpdateMode="Conditional">
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="ddlModalNombreAportanteRescate" EventName="SelectedIndexChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="ddlModalMultifondoRescate" EventName="SelectedIndexChanged" />
                                                        </Triggers>
                                                        <ContentTemplate>
                                                            <asp:DropDownList ID="ddlModalRutAportanteRescate" CssClass="form-control js-select2-rut" runat="server" OnSelectedIndexChanged="CargarNombreAportanteNemotecnicoPorRutAportanteModal" AutoPostBack="true" />
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                            </div>
                                            <div class="row mt-3">
                                                <div class="col-md-5">
                                                    <label class="form-control-label">Nombre Aportante</label>
                                                </div>
                                                <div class="col-md-6">
                                                    <asp:UpdatePanel runat="server" ID="UpdatePanel65" UpdateMode="Conditional">
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="ddlModalRutAportanteRescate" EventName="SelectedIndexChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="ddlModalMultifondoRescate" EventName="SelectedIndexChanged" />
                                                        </Triggers>
                                                        <ContentTemplate>
                                                            <asp:DropDownList ID="ddlModalNombreAportanteRescate" CssClass="form-control js-select2-rut" runat="server" OnSelectedIndexChanged="CargarRutAportanteNemotecnicoPorNombreAportanteModal" AutoPostBack="true" />
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                            </div>
                                            <div class="row mt-3">
                                                <div class="col-md-5">
                                                    <label class="form-control-label">Multifondo</label>
                                                </div>
                                                <div class="col-md-6">
                                                    <asp:UpdatePanel runat="server" ID="UpdatePanel66" UpdateMode="Conditional">
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="ddlModalRutAportanteRescate" EventName="SelectedIndexChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="ddlModalNombreAportanteRescate" EventName="SelectedIndexChanged" />
                                                        </Triggers>
                                                        <ContentTemplate>
                                                            <asp:DropDownList ID="ddlModalMultifondoRescate" CssClass="form-control js-select2-rut" runat="server" AutoPostBack="true" />
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                            </div>
                                            <div class="row mt-3">
                                                <div class="col-md-5">
                                                    <label class="form-control-label">Nombre Serie</label>
                                                </div>
                                                <div class="col-md-6">
                                                    <asp:UpdatePanel runat="server" ID="UpdatePanel67" UpdateMode="Conditional">
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="ddlModalRutFondoRescate" EventName="SelectedIndexChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="ddlModalNemotecnicoRescate" EventName="SelectedIndexChanged" />
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
                                                    <asp:UpdatePanel runat="server" ID="UpdatePanel68" UpdateMode="Conditional">
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="txtModalNAV" EventName="TextChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="txtModalMonto" EventName="TextChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="txtModalMontoCLP" EventName="TextChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="txtModalNAV_CLP" EventName="TextChanged" />
                                                        </Triggers>
                                                        <ContentTemplate>
                                                            <asp:TextBox ID="txtModalCuota" runat="server" CssClass="form-control dbs-entero-decimal" onkeypress="return soloNumeros(event)"></asp:TextBox>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                            </div>
                                            <div class="row mt-3">
                                                <div class="col-md-5">
                                                    <label class="form-control-label">Moneda de Pago</label>
                                                </div>
                                                <div class="col-md-6">
                                                    <asp:UpdatePanel runat="server" ID="UpdatePanel69" UpdateMode="Conditional">
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
                                                    </asp:UpdatePanel>
                                                </div>
                                            </div>
                                            <div class="row mt-3">
                                                <div class="col-md-5">
                                                    <label class="form-control-label">TC Observado</label>
                                                </div>
                                                <div class="col-md-6">
                                                    <asp:UpdatePanel runat="server" ID="UpdatePanel70" UpdateMode="Conditional">
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
                                                    <asp:UpdatePanel runat="server" ID="UpdatePanel71" UpdateMode="Conditional">
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
                                                    </asp:UpdatePanel>
                                                </div>
                                            </div>
                                            <div class="row mt-3">
                                                <div class="col-md-5">
                                                    <label class="form-control-label">Fecha de NAV</label>
                                                </div>
                                                <div class="col-md-6">
                                                    <asp:UpdatePanel runat="server" ID="UpdatePanel72" UpdateMode="Conditional">
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
                                                    </asp:UpdatePanel>
                                                </div>
                                            </div>
                                            <div class="row mt-3">
                                                <div class="col-md-5">
                                                    <label class="form-control-label">Fecha de Pago</label>
                                                </div>
                                                <div class="col-md-6">
                                                    <asp:UpdatePanel runat="server" ID="UpdatePanel73" UpdateMode="Conditional">
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
                                                    </asp:UpdatePanel>
                                                </div>
                                            </div>
                                            <div class="row mt-3">
                                                <div class="col-md-5">
                                                    <label class="form-control-label">Fecha de TC Obs</label>
                                                </div>
                                                <div class="col-md-6">
                                                    <asp:UpdatePanel runat="server" ID="UpdatePanel74" UpdateMode="Conditional">
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
                                                    </asp:UpdatePanel>
                                                </div>
                                            </div>
                                            <div class="row mt-3">
                                                <div class="col-md-5">
                                                    <label class="form-control-label">Monto</label>
                                                </div>
                                                <div class="col-md-6">
                                                    <asp:UpdatePanel runat="server" ID="UpdatePanel75" UpdateMode="Conditional">
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
                                                    </asp:UpdatePanel>
                                                </div>
                                            </div>
                                            <div class="row mt-3">
                                                <div class="col-md-5">
                                                    <label class="form-control-label">Monto (CLP)</label>
                                                </div>
                                                <div class="col-md-6">
                                                    <asp:UpdatePanel runat="server" ID="UpdatePanel76" UpdateMode="Conditional">
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
                                                    </asp:UpdatePanel>
                                                </div>
                                            </div>
                                            <div class="row mt-3">
                                                <div class="col-md-5">
                                                    <label class="form-control-label">Contrato</label>
                                                </div>
                                                <div class="col-md-6">
                                                    <asp:DropDownList ID="Dropdownlist7" CssClass="form-control js-select2-rut" runat="server" Enabled="false">
                                                        <asp:ListItem Value=""></asp:ListItem>
                                                        <asp:ListItem Value="OK">OK</asp:ListItem>
                                                        <asp:ListItem Value="NO OK">NO OK</asp:ListItem>
                                                    </asp:DropDownList>
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
                                                    <asp:TextBox ID="txtModalPatrimonio" runat="server" CssClass="form-control dbs-entero15-decimal4" ReadOnly="True"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="row mt-3">
                                                <div class="col-md-5">
                                                    <label class="form-control-label">Porcentaje %</label>
                                                </div>
                                                <div class="col-md-6">
                                                    <asp:UpdatePanel runat="server" ID="UpdatePanel77" UpdateMode="Conditional">
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="ddlModalRutFondoRescate" EventName="SelectedIndexChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="ddlModalNombreFondoRescate" EventName="SelectedIndexChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="ddlModalNemotecnicoRescate" EventName="SelectedIndexChanged" />
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
                                                    <asp:TextBox ID="txtModalRescateMax" runat="server" CssClass="form-control dbs-entero14-decimal4" ReadOnly="True"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="row mt-3">
                                                <div class="col-md-5">
                                                    <label class="form-control-label">Utilizado</label>
                                                </div>
                                                <div class="col-md-6">
                                                    <asp:TextBox ID="txtModalUtilizado" runat="server" CssClass="form-control dbs-entero14-decimal4" ReadOnly="True"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="row mt-3">
                                                <div class="col-md-5">
                                                    <label class="form-control-label">Disponibles</label>
                                                </div>
                                                <div class="col-md-6">
                                                    <asp:TextBox ID="txtModalDisponibles" runat="server" CssClass="form-control dbs-entero14-decimal4" ReadOnly="True"></asp:TextBox>
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
                                                    <asp:UpdatePanel runat="server" ID="UpdatePanel78" UpdateMode="Conditional">
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="ddlModalNombreFondoRescate" EventName="SelectedIndexChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="ddlModalNemotecnicoRescate" EventName="SelectedIndexChanged" />
                                                        </Triggers>
                                                        <ContentTemplate>
                                                            <asp:DropDownList ID="ddlModalRutFondoRescate" CssClass="form-control js-select2-rut" runat="server" AutoPostBack="true" />
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                            </div>
                                            <div class="row mt-3">
                                                <div class="col-md-5">
                                                    <label class="form-control-label">Nombre Fondo</label>
                                                </div>
                                                <div class="col-md-6">
                                                    <asp:UpdatePanel runat="server" ID="UpdatePanel79" UpdateMode="Conditional">
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="ddlModalRutFondoRescate" EventName="SelectedIndexChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="ddlModalNemotecnicoRescate" EventName="SelectedIndexChanged" />
                                                        </Triggers>
                                                        <ContentTemplate>
                                                            <asp:DropDownList ID="ddlModalNombreFondoRescate" CssClass="form-control js-select2-rut" runat="server" AutoPostBack="true" />
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
                                                            <asp:AsyncPostBackTrigger ControlID="ddlModalRutFondoRescate" EventName="SelectedIndexChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="ddlModalNombreFondoRescate" EventName="SelectedIndexChanged" />
                                                        </Triggers>
                                                        <ContentTemplate>
                                                            <asp:DropDownList ID="ddlModalNemotecnicoRescate" CssClass="form-control js-select2-rut" runat="server" AutoPostBack="true" />
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                            </div>
                                            <div class="row mt-3">
                                                <div class="col-md-5">
                                                    <label class="form-control-label">Moneda Serie</label>
                                                </div>
                                                <div class="col-md-6">
                                                    <asp:UpdatePanel runat="server" ID="UpdatePanel80" UpdateMode="Conditional">
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="ddlModalRutFondoRescate" EventName="SelectedIndexChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="ddlModalNemotecnicoRescate" EventName="SelectedIndexChanged" />
                                                        </Triggers>
                                                        <ContentTemplate>
                                                            <%--<asp:DropDownList ID="ddlModalMonedaSerie" CssClass="form-control js-select2-rut" runat="server" />--%>
                                                            <asp:TextBox ID="txtModalMonedaSerie" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                            </div>
                                            <div class="row mt-3">
                                                <div class="col-md-5">
                                                    <label class="form-control-label">NAV</label>
                                                </div>
                                                <div class="col-md-6">
                                                    <asp:UpdatePanel runat="server" ID="UpdatePanel81" UpdateMode="Conditional">
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
                                                    </asp:UpdatePanel>
                                                </div>
                                            </div>
                                            <div class="row mt-3">
                                                <div class="col-md-5">
                                                    <label class="form-control-label">NAV (CLP)</label>
                                                </div>
                                                <div class="col-md-6">
                                                    <asp:UpdatePanel runat="server" ID="UpdatePanel88" UpdateMode="Conditional">
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
                                                    </asp:UpdatePanel>
                                                </div>
                                            </div>
                                            <div class="row mt-3">
                                                <div class="col-md-5">
                                                    <label class="form-control-label">Poderes</label>
                                                </div>
                                                <div class="col-md-6">
                                                    <asp:DropDownList ID="Dropdownlist9" CssClass="form-control js-select2-rut" runat="server" Enabled="false">
                                                        <asp:ListItem Value=""></asp:ListItem>
                                                        <asp:ListItem Value="OK">OK</asp:ListItem>
                                                        <asp:ListItem Value="NO OK">NO OK</asp:ListItem>
                                                    </asp:DropDownList>
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
                                                    <asp:UpdatePanel runat="server" ID="UpdatePanel82" UpdateMode="Conditional">
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
                                                    </asp:UpdatePanel>
                                                </div>
                                            </div>
                                            <div class="row mt-3">
                                                <div class="col-md-5">
                                                    <label class="form-control-label">Estado</label>
                                                </div>
                                                <div class="col-md-6">
                                                    <asp:DropDownList ID="Dropdownlist10" CssClass="form-control js-select2-rut" runat="server" Enabled="false">
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
                                                    <asp:TextBox ID="Textbox2" runat="server" CssClass="form-control form-control-sm" Enabled="false"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="row mt-3">
                                                <div class="col-md-5">
                                                    <label class="form-control-label">Fijación NAV</label>
                                                </div>
                                                <div class="col-md-6">
                                                    <asp:UpdatePanel runat="server" ID="UpdatePanel83" UpdateMode="Conditional">
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="ddlModalRutFondoRescate" EventName="SelectedIndexChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="ddlModalNemotecnicoRescate" EventName="SelectedIndexChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="txtModalCuota" EventName="TextChanged" />
                                                        </Triggers>
                                                        <ContentTemplate>
                                                            <asp:TextBox ID="txtModalFijacionNAV" runat="server" CssClass="form-control form-control-sm" ReadOnly="True"></asp:TextBox>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                            </div>
                                            <div class="row mt-3">
                                                <div class="col-md-5">
                                                    <label class="form-control-label">Fijacion TC Obs</label>
                                                </div>
                                                <div class="col-md-6">
                                                    <asp:UpdatePanel runat="server" ID="UpdatePanel84" UpdateMode="Conditional">
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
                                                    <asp:UpdatePanel runat="server" ID="UpdatePanel85" UpdateMode="Conditional">
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="ddlModalRutFondoRescate" EventName="SelectedIndexChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="ddlModalNombreFondoRescate" EventName="SelectedIndexChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="ddlModalNemotecnicoRescate" EventName="SelectedIndexChanged" />
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
                                                    <asp:UpdatePanel runat="server" ID="UpdatePanel86" UpdateMode="Conditional">
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="ddlModalRutFondoRescate" EventName="SelectedIndexChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="ddlModalNombreFondoRescate" EventName="SelectedIndexChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="ddlModalNemotecnicoRescate" EventName="SelectedIndexChanged" />
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
                                                    <asp:TextBox ID="txtModalSuscripciones" runat="server" CssClass="form-control dbs-entero18-decimal0" ReadOnly="True"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="row mt-3">
                                                <div class="col-md-5">
                                                    <label class="form-control-label">Canje</label>
                                                </div>
                                                <div class="col-md-6">
                                                    <asp:TextBox ID="txtModalCanje" runat="server" CssClass="form-control dbs-entero18-decimal0" ReadOnly="True"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="row mt-3">
                                                <div class="col-md-5">
                                                    <label class="form-control-label">Disponibles</label>
                                                </div>
                                                <div class="col-md-6">
                                                    <asp:UpdatePanel runat="server" ID="UpdatePanel87" UpdateMode="Conditional">
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="ddlModalRutFondoRescate" EventName="SelectedIndexChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="ddlModalNombreFondoRescate" EventName="SelectedIndexChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="ddlModalNemotecnicoRescate" EventName="SelectedIndexChanged" />
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
                            <div class="form-group mt-5 text-right">
                                <div class="col-md-offset-1">
                                    <asp:Button ID="btnModalModificarRastreo" Text="Modificar" CssClass="btn btn-info w-25" runat="server"></asp:Button>
                                    <asp:Button ID="btnCancelarModalRescates" Text="Cancelar" CssClass="btn btn-secondary w-25" Width="15%" runat="server" OnClientClick="if (!confirm('¿Seguro que desea Cancelar?')) return false;"></asp:Button>
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
                        <asp:Image ID="img_modal" ImageUrl="~/Img/info1.png" runat="server" Width="130" Height="50" />
                        <asp:Label ID="lblModalTitle" runat="server" Text="" Font-Bold="true" Font-Size="X-Large">
                        </asp:Label>
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

    <asp:HiddenField ID="HiddenPerfil" runat="server" />
    <asp:HiddenField ID="HiddenConstante" runat="server" />


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
                    }
                    else {
                        if (contador == 1) {
                            $(".btnmodificar").prop('disabled', false);
                            $(".BtnFijarNav").prop('disabled', false);
                            $(".BtnTCObs").prop('disabled', false);
                             $(".btnImprimir").prop('disabled', false);
                        }
                        else {
                            $(".btnmodificar").prop('disabled', true);
                            $(".BtnFijarNav").prop('disabled', true);
                            $(".BtnTCObs").prop('disabled', true);
                             $(".btnImprimir").prop('disabled', true);

                        }
                    }
                });
                $('.checkFijacionAll').change(function () {
                    if ($('.checkFijacionAll input').prop('checked') == true) {
                        $('.checkFijacion input').prop('checked', true);
                        $(".BtnFijarNav").prop('disabled', false);
                        $(".BtnTCObs").prop('disabled', false);
                        $(".btnImprimir").prop('disabled', false);
                    }
                    else {
                        $('.checkFijacion input').prop('checked', false);
                        $(".BtnFijarNav").prop('disabled', true);
                        $(".BtnTCObs").prop('disabled', true);
                        $(".btnImprimir").prop('disabled', true);
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
                    return false; Seguro
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
                    return false; Seguro
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
                    return false; Seguro
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

            btnModificar.disabled = newValue;
            BtnFijarNav.disabled = newValue;
            BtnTCObs.disabled = newValue;
            btnImprimir.disabled = newValue; 
        }

        function checkRadioBtn(id) {
            var gv = document.getElementById('<%=GrvTabla.ClientID %>');
            if (gv != null) {
                for (var i = 1; i < gv.rows.length; i++) {
                    var radioBtn = gv.rows[i].cells[0].getElementsByTagName("input");
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
    </script>
</asp:Content>
