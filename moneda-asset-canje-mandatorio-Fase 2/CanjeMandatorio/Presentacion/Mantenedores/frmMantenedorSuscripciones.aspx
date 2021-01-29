<%@ Page Title="Maestro de suscripciones" Language="VB" EnableEventValidation="false" MasterPageFile="~/Site.master" AutoEventWireup="false" CodeFile="frmMantenedorSuscripciones.aspx.vb" Inherits="Presentacion_Mantenedores_frmMantenedorSuscripciones" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" />


    <h2 class="TdRedondeado titleMant">Maestro de <strong>Suscripciones</strong></h2>

    <div class="card p-4 jumbotron">

        <div class="row">
            <!-- LISTA RUT APORTANTE-->
            <div class="col-md-3">
                <asp:Label runat="server" ID="rutaportante">Aportante</asp:Label>
                <asp:DropDownList ID="ddlListaRutAportante" CssClass="form-control js-select2-rut" runat="server" AutoPostBack="True"/>
            </div>
            <!-- LISTA RUT FONDO-->
            <div class="col-md-3">
                <asp:Label runat="server" ID="rutfondo">Fondo</asp:Label>
                <asp:DropDownList ID="ddlListaRutFondo" CssClass="form-control js-select2-rut" runat="server"  AutoPostBack="True" />
            </div>
            <!-- NEMOTÉCNICO -->
            <div class="col-md-3">
                <asp:Label runat="server" ID="nemotecnico">Nemotécnico</asp:Label>
                <asp:DropDownList ID="ddlListaNemotecnico" CssClass="form-control js-select2-rut" runat="server" AutoPostBack="True" />
            </div>
            <!-- ESTADO -->
            <div class="col-md-3">
                <asp:Label runat="server" ID="Label6">Estado</asp:Label>
                <asp:DropDownList ID="ddlEstado" CssClass="form-control js-select2-rut" runat="server">
                </asp:DropDownList>
            </div>
        </div>

        <div class="row mt-4">

            <!-- FECHA DESDE -->
            <div class="col-md-3">
                <asp:Label runat="server" ID="Label2">Fecha intención desde</asp:Label>
                <div class="input-group">
                    <asp:TextBox ID="txtIntencionDesde" runat="server" CssClass="form-control datepicker" ReadOnly="True"></asp:TextBox>
                    <asp:LinkButton ID="LinkButton1" Class="btn btn-moneda" runat="server" 
                        OnClientClick="return clickCalendar('txtIntencionDesde')"><i class="far fa-calendar-alt"></i></asp:LinkButton>
                    <asp:LinkButton ID="BtnLimpiarFechaDesde" 
                        OnClientClick="return limpiarCalendar('txtIntencionDesde')" Text="" class="btn btn-secondary ml-1" runat="server"><i class="far fa-trash-alt"></i></asp:LinkButton>

<%--                     <asp:Calendar ID="Calendar1" runat="server" Visible="False" BackColor="White" BorderColor="White" BorderWidth="1px" Font-Names="Verdana" Font-Size="9pt" ForeColor="Black" Height="190px" NextPrevFormat="FullMonth" Width="350px" class="calendarios">
                    <DayHeaderStyle Font-Bold="True" Font-Size="8pt" />
                    <NextPrevStyle Font-Bold="True" Font-Size="8pt" ForeColor="#333333" VerticalAlign="Bottom" />
                    <OtherMonthDayStyle ForeColor="#999999" />
                    <SelectedDayStyle BackColor="#333399" ForeColor="White" />
                    <TitleStyle BackColor="White" BorderColor="Black" BorderWidth="4px" Font-Bold="True" Font-Size="12pt" ForeColor="#333399" />
                    <TodayDayStyle BackColor="#CCCCCC" />
                    </asp:Calendar>--%>
                </div>
            </div>
            <!-- FECHA HASTA -->
            <div class="col-md-3">
                <asp:Label runat="server" ID="Label3">Fecha intención hasta</asp:Label>
                <div class="input-group">
                    <asp:TextBox ID="txtIntencionHasta" runat="server" CssClass="form-control datepicker" ReadOnly="True"></asp:TextBox>
                    <asp:LinkButton ID="LinkButton2" Class="btn btn-moneda" runat="server" 
                        OnClientClick="return clickCalendar('txtIntencionHasta')"><i class="far fa-calendar-alt"></i></asp:LinkButton>
                    <asp:LinkButton ID="LinkButton11" Text=""
                        OnClientClick="return limpiarCalendar('txtIntencionHasta')" class="btn btn-secondary ml-1" runat="server"><i class="far fa-trash-alt"></i></asp:LinkButton>

<%--                    <asp:Calendar ID="Calendar2" runat="server" Visible="False" BackColor="White" BorderColor="White" BorderWidth="1px" Font-Names="Verdana" Font-Size="9pt" ForeColor="Black" Height="190px" NextPrevFormat="FullMonth" Width="350px" class="calendarios">
                        <DayHeaderStyle Font-Bold="True" Font-Size="8pt" />
                        <NextPrevStyle Font-Bold="True" Font-Size="8pt" ForeColor="#333333" VerticalAlign="Bottom" />
                        <OtherMonthDayStyle ForeColor="#999999" />
                        <SelectedDayStyle BackColor="#333399" ForeColor="White" />
                        <TitleStyle BackColor="White" BorderColor="Black" BorderWidth="4px" Font-Bold="True" Font-Size="12pt" ForeColor="#333399" />
                        <TodayDayStyle BackColor="#CCCCCC" />
                    </asp:Calendar>--%>
                </div>
            </div>
            <!-- FECHA NAV DESDE -->
            <div class="col-md-3">
                <asp:Label runat="server" ID="Label4">Fecha NAV desde</asp:Label>
                <div class="input-group">
                    <asp:TextBox ID="txtNAVDesde" runat="server" CssClass="form-control datepicker" ReadOnly="True"></asp:TextBox>
                    <asp:LinkButton ID="LinkButton3" Class="btn btn-moneda" runat="server" 
                        OnClientClick="return clickCalendar('txtNAVDesde')" ><i class="far fa-calendar-alt"></i></asp:LinkButton>
                    <asp:LinkButton ID="LinkButton12"  Text="" 
                        OnClientClick="return limpiarCalendar('txtNAVDesde')" class="btn btn-secondary ml-1" runat="server"><i class="far fa-trash-alt"></i></asp:LinkButton>

<%--                    <asp:Calendar ID="Calendar3" runat="server" Visible="False" BackColor="White" BorderColor="White" BorderWidth="1px" Font-Names="Verdana" Font-Size="9pt" ForeColor="Black" Height="190px" NextPrevFormat="FullMonth" Width="350px" class="calendarios">
                        <DayHeaderStyle Font-Bold="True" Font-Size="8pt" />
                        <NextPrevStyle Font-Bold="True" Font-Size="8pt" ForeColor="#333333" VerticalAlign="Bottom" />
                        <OtherMonthDayStyle ForeColor="#999999" />
                        <SelectedDayStyle BackColor="#333399" ForeColor="White" />
                        <TitleStyle BackColor="White" BorderColor="Black" BorderWidth="4px" Font-Bold="True" Font-Size="12pt" ForeColor="#333399" />
                        <TodayDayStyle BackColor="#CCCCCC" />
                    </asp:Calendar>--%>
                </div>
            </div>
            <!-- FECHA NAV HASTA -->
            <div class="col-md-3">
                <asp:Label runat="server" ID="Label7">Fecha NAV hasta</asp:Label>
                <div class="input-group">
                    <asp:TextBox ID="txtNAVHasta" runat="server" CssClass="form-control datepicker" ReadOnly="True"></asp:TextBox>
                    <asp:LinkButton ID="LinkButton4" Class="btn btn-moneda" runat="server" 
                        OnClientClick="return clickCalendar('txtNAVHasta')"><i class="far fa-calendar-alt"></i></asp:LinkButton>
                    <asp:LinkButton ID="LinkButton13" Text="" 
                        OnClientClick="return limpiarCalendar('txtNAVHasta')" class="btn btn-secondary ml-1" runat="server"><i class="far fa-trash-alt"></i></asp:LinkButton>
                    
<%--                    <asp:Calendar ID="Calendar4" runat="server" Visible="False" BackColor="White" BorderColor="White" BorderWidth="1px" Font-Names="Verdana" Font-Size="9pt" ForeColor="Black" Height="190px" NextPrevFormat="FullMonth" Width="350px" class="calendarios">
                        <DayHeaderStyle Font-Bold="True" Font-Size="8pt" />
                        <NextPrevStyle Font-Bold="True" Font-Size="8pt" ForeColor="#333333" VerticalAlign="Bottom" />
                        <OtherMonthDayStyle ForeColor="#999999" />
                        <SelectedDayStyle BackColor="#333399" ForeColor="White" />
                        <TitleStyle BackColor="White" BorderColor="Black" BorderWidth="4px" Font-Bold="True" Font-Size="12pt" ForeColor="#333399" />
                        <TodayDayStyle BackColor="#CCCCCC" />
                    </asp:Calendar>--%>
                </div>
            </div>
        </div>

        <div class="row mt-4">
            <!-- FECHA SUSCRIPCIÓN DESDE-->
            <div class="col-md-3">
                <asp:Label runat="server" ID="Label8">Fecha suscripción desde</asp:Label>
                <div class="input-group">
                    <asp:TextBox ID="txtSuscripcionDesde" runat="server" CssClass="form-control datepicker" ReadOnly="True"></asp:TextBox>
                    <asp:LinkButton ID="LinkButton5" Class="btn btn-moneda" runat="server" 
                        OnClientClick="return clickCalendar('txtSuscripcionDesde')"><i class="far fa-calendar-alt"></i></asp:LinkButton>

                    <asp:LinkButton ID="LinkButton14" OnClientClick="return limpiarCalendar('txtSuscripcionDesde')" Text="" class="btn btn-secondary ml-1" runat="server"><i class="far fa-trash-alt"></i></asp:LinkButton>
                    
<%--                    <asp:Calendar ID="Calendar5" runat="server" Visible="False" BackColor="White" BorderColor="White" BorderWidth="1px" Font-Names="Verdana" Font-Size="9pt" ForeColor="Black" Height="190px" NextPrevFormat="FullMonth" Width="350px" class="calendarios">
                        <DayHeaderStyle Font-Bold="True" Font-Size="8pt" />
                        <NextPrevStyle Font-Bold="True" Font-Size="8pt" ForeColor="#333333" VerticalAlign="Bottom" />
                        <OtherMonthDayStyle ForeColor="#999999" />
                        <SelectedDayStyle BackColor="#333399" ForeColor="White" />
                        <TitleStyle BackColor="White" BorderColor="Black" BorderWidth="4px" Font-Bold="True" Font-Size="12pt" ForeColor="#333399" />
                        <TodayDayStyle BackColor="#CCCCCC" />
                    </asp:Calendar>--%>
                </div>
            </div>

            <!-- FECHA SUSCRIPCIÓN HASTA-->
            <div class="col-md-3">
                <asp:Label runat="server" ID="Label9">Fecha suscripción hasta</asp:Label>
                <div class="input-group">
                    <asp:TextBox ID="txtSuscripcionHasta" runat="server" CssClass="form-control datepicker" ReadOnly="True"></asp:TextBox>
                    <asp:LinkButton ID="LinkButton6" Class="btn btn-moneda" runat="server" OnClientClick="return clickCalendar('txtSuscripcionHasta')" ><i class="far fa-calendar-alt"></i></asp:LinkButton>
                    <asp:LinkButton ID="LinkButton15" Text="" OnClientClick="return limpiarCalendar('txtSuscripcionHasta')" class="btn btn-secondary ml-1" runat="server"><i class="far fa-trash-alt"></i></asp:LinkButton>
                   
<%--                    <asp:Calendar ID="Calendar6" runat="server" Visible="False" BackColor="White" BorderColor="White" BorderWidth="1px" Font-Names="Verdana" Font-Size="9pt" ForeColor="Black" Height="190px" NextPrevFormat="FullMonth" Width="350px" class="calendarios">
                        <DayHeaderStyle Font-Bold="True" Font-Size="8pt" />
                        <NextPrevStyle Font-Bold="True" Font-Size="8pt" ForeColor="#333333" VerticalAlign="Bottom" />
                        <OtherMonthDayStyle ForeColor="#999999" />
                        <SelectedDayStyle BackColor="#333399" ForeColor="White" />
                        <TitleStyle BackColor="White" BorderColor="Black" BorderWidth="4px" Font-Bold="True" Font-Size="12pt" ForeColor="#333399" />
                        <TodayDayStyle BackColor="#CCCCCC" />
                    </asp:Calendar>--%>
                </div>
            </div>
        </div>

        <!-- BOTÓN BUSCAR -->
        <div class="row text-center mt-5 p-3 border-bottom">
            <div class="col-md-12">
                <asp:Button ID="BtnBuscar" Text="Buscar" class="btn btn-moneda" runat="server" />
                <asp:Button ID="btnLimpiarFrm" Text="Borrar" OnClick="btnLimpiarFrm_Click" class="btn btn-secondary" runat="server" />
                <!-- BOTÓN CREAR -->
                <asp:Button ID="btnCrear" Text="Crear" class="btn btn-info" runat="server" OnClick="btnCrear_Click" />
            </div>
        </div>


        <asp:HiddenField ID="txtAccionHidden" runat="server" />

        <!-- TABLA DE RESULTADOS -->
        <h5 class="mt-3 text-secondary"></h5><i class="fas fa-file-invoice fa-sm"></i> Resultado de la búsqueda
      <div>
          <br />
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
                            <ItemTemplate>
                                <asp:RadioButton ID="RowSelector" runat="server" onclick="checkRadioBtn(this);" GroupName="a" AutoPostBack="false" OnCheckedChanged="RowSelector_CheckedChanged" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="IdSuscripcion" HeaderText="ID" />
                        <asp:BoundField DataField="TipoTransaccion" HeaderText="Tipo de Transacción" />
                        <asp:BoundField DataField="FechaIntencion" HeaderText="Fecha Intención" DataFormatString="{0:dd-MM-yyyy}" />
                        <asp:BoundField DataField="RutAportante" HeaderText="Rut Aportante" />
                        <asp:BoundField DataField="RazonSocial" HeaderText="Razón Social" />
                        <asp:BoundField DataField="Multifondo" HeaderText="Multifondo" />
                        <asp:BoundField DataField="RutFondo" HeaderText="Rut Fondo" />
                        <asp:BoundField DataField="FondoNombreCorto" HeaderText="Fondo" />
                        <asp:BoundField DataField="Nemotecnico" HeaderText="Nemotécnico" />
                        <asp:BoundField DataField="SerieNombreCorto" HeaderText="Serie" />
                        <asp:BoundField DataField="MonedaSerie" HeaderText="Moneda Serie" />
                        <asp:BoundField DataField="CuotasASuscribir" HeaderText="Cuotas a suscribrir" DataFormatString="{0:N0}" ItemStyle-HorizontalAlign="Right"/>
                        <asp:BoundField DataField="Moneda_Pago" HeaderText="Moneda de pago" />
                        <asp:BoundField DataField="FechaNAV" HeaderText="Fecha NAV" DataFormatString="{0:dd-MM-yyyy}" />
                        <asp:BoundField DataField="FechaSuscripcion" HeaderText="Fecha suscripción" DataFormatString="{0:dd-MM-yyyy}" />
                        <asp:BoundField DataField="FechaTC" HeaderText="Fecha TC observado" DataFormatString="{0:dd-MM-yyyy}" />
                        <%--<asp:BoundField DataField="NAV" HeaderText="NAV" />--%>
                        <asp:BoundField DataField="NavFormat" HeaderText="NAV" ItemStyle-HorizontalAlign="Right"/>
                        <asp:BoundField DataField="Monto" HeaderText="Monto" DataFormatString="{0:N2}" ItemStyle-HorizontalAlign="Right"/>
                        <%--<asp:BoundField DataField="NAVCLP" HeaderText="NAV (CLP)" />--%>
                        <asp:BoundField DataField="NAVCLPFormat" HeaderText="NAV (CLP)" DataFormatString="{0:N4}" ItemStyle-HorizontalAlign="Right"/>
                        <asp:BoundField DataField="MontoCLP" HeaderText="Monto (CLP)" DataFormatString="{0:N0}" ItemStyle-HorizontalAlign="Right"/>
                        <asp:BoundField DataField="ContratoFondo" HeaderText="Contrato general de fondo" />
                        <asp:BoundField DataField="RevisionPoderes" HeaderText="Revisión de poderes" />
                        <asp:BoundField DataField="Observaciones" HeaderText="Observaciones" />
                        <asp:BoundField DataField="FechaDCV" HeaderText="Fecha DCV" DataFormatString="{0:dd-MM-yyyy}" />
                        <asp:BoundField DataField="CuotasDCV" HeaderText="Cuotas DCV" DataFormatString="{0:N0}" ItemStyle-HorizontalAlign="Right"/>
                        <asp:BoundField DataField="RescatesTransitos" HeaderText="Rescates en tránsito" DataFormatString="{0:N0}" ItemStyle-HorizontalAlign="Right"/>
                        <asp:BoundField DataField="SuscripcionesTransito" HeaderText="Suscripciones en tránsito" DataFormatString="{0:N0}" ItemStyle-HorizontalAlign="Right"/>
                        <asp:BoundField DataField="CanjesTransito" HeaderText="Canjes en tránsito" DataFormatString="{0:N0}" ItemStyle-HorizontalAlign="Right"/>
                        <asp:BoundField DataField="CuotasDisponibles" HeaderText="Cuotas disponibles" DataFormatString="{0:N0}" ItemStyle-HorizontalAlign="Right"/>
                        <asp:BoundField DataField="FijacionNAV" HeaderText="Fijación Nav" />
                        <asp:BoundField DataField="TcObservado" HeaderText="TC observado" />
                        <asp:BoundField DataField="FijacionTC" HeaderText="Fijación TC observado" />
                        <asp:BoundField DataField="EstadoSuscripcion" HeaderText="Estado" />
                        <asp:BoundField DataField="CuotasEmitidas" HeaderText="Cuotas emitidas" DataFormatString="{0:N6}" ItemStyle-HorizontalAlign="Right"/>
                        <asp:BoundField DataField="FnAcumulada" HeaderText="Acumulada" DataFormatString="{0:N0}" ItemStyle-HorizontalAlign="Right"/>
                        <asp:BoundField DataField="ScActual" HeaderText="Actual" DataFormatString="{0:N0}" ItemStyle-HorizontalAlign="Right"/>
                        <asp:BoundField DataField="ScUtilizado" HeaderText="Utilizado" DataFormatString="{0:N0}" ItemStyle-HorizontalAlign="Right"/>
                        <asp:BoundField DataField="ScDisponibles" HeaderText="Disponibles" DataFormatString="{0:N0}" ItemStyle-HorizontalAlign="Right"/>
                        <asp:BoundField DataField="ScFechaIngreso" HeaderText="Fecha Ingreso" DataFormatString="{0:dd-MM-yyyy}" />
                        <asp:BoundField DataField="ScUsuarioIngreso" HeaderText="Usuario Ingreso" />
                        <asp:BoundField DataField="ScFechaModificacion" HeaderText="Fecha Modificación" DataFormatString="{0:dd-MM-yyyy}" />
                        <asp:BoundField DataField="ScUsuarioModificacion" HeaderText="Usuario Modificador" />
                    </Columns>
                </asp:GridView>
            </div>
            <div class="row mt-3">
                <div class="col-md-12 text-center">
                    <asp:Button ID="BtnModificar" runat="server" class="btn btn-info" Text="Modificar" Enabled="false"></asp:Button>
                    <asp:Button ID="BtnEliminar" runat="server" class="btn btn-danger" Text="Eliminar" Enabled="false"></asp:Button>
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
                        <asp:Label ID="lbModalTittle" runat="server" Text="Formulario - Suscripciones" Font-Bold="true" Font-Size="X-Large">
                        </asp:Label>
                    </h4>
                    <button id="btnXCerrar" type="button" class="close" data-dismiss="modal" aria-label="Close" runat="server"><span aria-hidden="true">&times;</span></button>
                </div>
                <div class="modal-body">
                    <div class="jumbotron p-2">
                        <!-- FORMULARIO-->
                        <asp:HiddenField ID="txtEstadoCambio" runat="server"></asp:HiddenField>
                        <div class="col-md-12 mx-auto mt-10 " visible="false">

                            <div class="col-md-12 mx-auto d-flex p-0 mb-10 justify-content-between">
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
                                                        <asp:AsyncPostBackTrigger ControlID="txtTcObservado" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtMonto" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtCuotas" EventName="TextChanged" />
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
                                                        <asp:AsyncPostBackTrigger ControlID="ddlModalRutAportante" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddlModalMultifondo" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtTcObservado" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtMonto" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtCuotas" EventName="TextChanged" />
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
                                                        <asp:AsyncPostBackTrigger ControlID="txtTcObservado" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtMonto" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtCuotas" EventName="TextChanged" />
                                                    </Triggers>
                                                    <ContentTemplate>
                                                        <asp:DropDownList ID="ddlModalMultifondo" CssClass="form-control js-select2-rut" runat="server" OnSelectedIndexChanged="CargaPorMultifondo" AutoPostBack="true" />
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
                                                        <asp:AsyncPostBackTrigger ControlID="ddlNombreFondo" EventName="SelectedIndexChanged" />
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
                                                        <asp:AsyncPostBackTrigger ControlID="ddlNombreFondo" EventName="SelectedIndexChanged" />
                                                    </Triggers>
                                                    <ContentTemplate>
                                                        <asp:DropDownList ID="ddlNombreFondo" CssClass="form-control js-select2-rut" runat="server" OnSelectedIndexChanged="SelectedChangedNombreFondo" AutoPostBack="true" />
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
                                                        <asp:AsyncPostBackTrigger ControlID="ddlNombreFondo" EventName="SelectedIndexChanged" />
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
                                                        <asp:AsyncPostBackTrigger ControlID="ddlNombreFondo" EventName="SelectedIndexChanged" />
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
                                                        <asp:AsyncPostBackTrigger ControlID="ddlNombreFondo" EventName="SelectedIndexChanged" />
                                                    </Triggers>
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtMonedaSerie" CssClass="form-control" runat="server" ReadOnly="true" />
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                            <div class="col-md-6">
                                                <asp:Label runat="server" ID="lblMonedaPago">Moneda pago</asp:Label>

                                                <asp:UpdatePanel runat="server" ID="UpdatePanel28" UpdateMode="Conditional">
                                                    <Triggers>
                                                    </Triggers>
                                                    <ContentTemplate>
                                                        <asp:DropDownList ID="ddlMonedaPago" CssClass="form-control js-select2-rut" runat="server" OnSelectedIndexChanged="MonedaSelectedChanged" AutoPostBack="true" />
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>

                                        <div class="row mt-3">
                                            <div class="col-md-6">
                                                <asp:Label runat="server" ID="lbNAV">NAV</asp:Label>

                                                <asp:UpdatePanel runat="server" ID="UpdatePanel19" UpdateMode="Conditional">
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="ddlNemotecnico" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddlNombreFondo" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddlFondo" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddlNombreFondo" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtMonto" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtFechaSuscripcion" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtFechaNAV" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtFechaTC" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtFechaIntencion" EventName="TextChanged" />
                                                    </Triggers>
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtNAV" onpaste="return false" CssClass="form-control dbs-entero20-decimal6" runat="server" OnTextChanged="navchanged" AutoPostBack="true" />
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
                                                        <asp:AsyncPostBackTrigger ControlID="txtNAV" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddlNombreFondo" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtTcObservado" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtMonto" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtCuotas" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddlMonedaPago" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtFechaSuscripcion" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtFechaNAV" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtFechaTC" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtFechaIntencion" EventName="TextChanged" />
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
                                                        <div class="input-group">
                                                            <asp:TextBox ID="txtFechaIntencion" runat="server" CssClass="form-control datepicker" AutoPostBack="true" Enabled="false" ></asp:TextBox>
                                                            <asp:LinkButton ID="LinkButton9" Class="btn btn-moneda"  runat="server" OnClientClick="return clickCalendar('txtFechaIntencion')"><i class="far fa-calendar-alt"></i></asp:LinkButton>

                                                            <span id="reqtxtFecha3" class="reqError"></span>
                                                        </div>
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
                                                        <asp:AsyncPostBackTrigger ControlID="ddlNombreFondo" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtFechaSuscripcion" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtFechaNAV" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtFechaTC" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtFechaIntencion" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddlMonedaPago" EventName="SelectedIndexChanged" />
                                                    </Triggers>
                                                    <ContentTemplate>
                                                        <div class="input-group">
                                                            <asp:TextBox ID="txtFechaNAV" runat="server" CssClass="form-control datepicker" AutoPostBack="true" Enabled="false"></asp:TextBox>
                                                            <asp:LinkButton ID="LinkButton7" Class="btn btn-moneda" OnClientClick="return clickCalendar('txtFechaNAV')" runat="server"><i class="far fa-calendar-alt"></i></asp:LinkButton>
                                                            <span id="reqtxtFecha1" class="reqError"></span>
                                                        </div>
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
                                                        <asp:AsyncPostBackTrigger ControlID="ddlNombreFondo" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtFechaNAV" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtFechaTC" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtFechaIntencion" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddlMonedaPago" EventName="SelectedIndexChanged" />
                                                    </Triggers>
                                                    <ContentTemplate>
                                                        <div class="input-group">
                                                            <asp:TextBox ID="txtFechaSuscripcion" runat="server" CssClass="form-control datepicker" OnTextChanged="SuscripcionChanged" AutoPostBack="true" Enabled="false"></asp:TextBox>
                                                            <asp:LinkButton ID="LinkButton10" Class="btn btn-moneda" OnClientClick="return clickCalendar('txtFechaSuscripcion')" runat="server"><i class="far fa-calendar-alt"></i></asp:LinkButton>
                   
                                                            <span id="reqtxtFecha8" class="reqError"></span>
                                                        </div>
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
                                                        <asp:AsyncPostBackTrigger ControlID="ddlNombreFondo" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtFechaSuscripcion" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtFechaNAV" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtFechaTC" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtFechaIntencion" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddlMonedaPago" EventName="SelectedIndexChanged" />
                                                    </Triggers>
                                                    <ContentTemplate>
                                                        <div class="input-group">
                                                            <asp:TextBox ID="txtFechaTC" runat="server" CssClass="form-control datepicker" AutoPostBack="true" Enabled="false"></asp:TextBox>
                                                            <asp:LinkButton ID="LinkButton8" Class="btn btn-moneda" OnClientClick="return clickCalendar('txtFechaTC')" runat="server"><i class="far fa-calendar-alt"></i></asp:LinkButton>
                                      
                                                            <span id="reqtxtFecha" class="reqError"></span>
                                                        </div>
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
                                                        <asp:AsyncPostBackTrigger ControlID="ddlMonedaPago" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddlNombreFondo" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtFechaSuscripcion" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtFechaNAV" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtFechaTC" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtFechaIntencion" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddlMonedaPago" EventName="SelectedIndexChanged" />
                                                    </Triggers>
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtTCObservado"  onpaste="return false" CssClass="form-control dbs-entero20-decimal6" runat="server" OnTextChanged="TcObservadoChanged" AutoPostBack="true" />
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                            <div class="col-md-6">
                                                <asp:Label runat="server" ID="lbCuotas">Cuotas</asp:Label>

                                                <asp:UpdatePanel runat="server" ID="UpdatePanel24" UpdateMode="Conditional">
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="ddlNemotecnico" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddlFondo" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtNAV" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtMonto" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddlNombreFondo" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtFechaSuscripcion" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtFechaNAV" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtFechaTC" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtFechaIntencion" EventName="TextChanged" />
                                                    </Triggers>
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtCuotas" onpaste="return false" CssClass="form-control dbs-entero-decimal" runat="server" OnTextChanged="CuotasTextChanged" AutoPostBack="true" AutoComplete="off" />
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
                                                        <asp:AsyncPostBackTrigger ControlID="ddlEstado1" EventName="SelectedIndexChanged" />
                                                        <%--<asp:AsyncPostBackTrigger ControlID="txtMonto" EventName="TextChanged" />--%>
                                                        <asp:AsyncPostBackTrigger ControlID="ddlNombreFondo" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtCuotas" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtTcObservado" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtNAV" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddlMonedaPago" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtFechaSuscripcion" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtFechaNAV" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtFechaTC" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtFechaIntencion" EventName="TextChanged" />
                                                    </Triggers>
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtMonto" onpaste="return false" CssClass="form-control dbs-entero20-decimal2" runat="server" OnTextChanged="MontoTextChanged" AutoPostBack="true" AutoComplete="off" />
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
                                                        <asp:AsyncPostBackTrigger ControlID="ddlNombreFondo" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtCuotas" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtTcObservado" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtNAV" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddlMonedaPago" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtFechaSuscripcion" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtFechaNAV" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtFechaTC" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtFechaIntencion" EventName="TextChanged" />
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

                                                <asp:UpdatePanel runat="server" ID="UpdatePanel15" UpdateMode="Conditional">
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="ddlNemotecnico" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddlFondo" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddlNombreFondo" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtFechaSuscripcion" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtFechaNAV" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtFechaTC" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtFechaIntencion" EventName="TextChanged" />
                                                    </Triggers>
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtFechaDCV" CssClass="form-control form-control-sm" runat="server" ReadOnly="True" />
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                            <div class="col-md-6">
                                                <asp:Label runat="server" ID="Label18">Estado</asp:Label>

                                                <asp:UpdatePanel runat="server" ID="UpdatePanel22" UpdateMode="Conditional">
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="ddlNemotecnico" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddlFondo" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddlNombreFondo" EventName="SelectedIndexChanged" />
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

                                                <asp:TextBox ID="txtObservaciones" CssClass="form-control form-control-sm" runat="server" />
                                            </div>
                                            <div class="col-md-6">
                                                <asp:Label runat="server" ID="Label20">Fijación NAV</asp:Label>

                                                <asp:UpdatePanel runat="server" ID="UpdatePanel17" UpdateMode="Conditional">
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="ddlNemotecnico" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddlFondo" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddlNombreFondo" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtFechaSuscripcion" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtFechaNAV" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtFechaTC" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtFechaIntencion" EventName="TextChanged" />
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

                                                <asp:UpdatePanel runat="server" ID="UpdatePanel18" UpdateMode="Conditional">
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="ddlNemotecnico" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddlFondo" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddlNombreFondo" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddlMonedaPago" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtFechaSuscripcion" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtFechaNAV" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtFechaTC" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtFechaIntencion" EventName="TextChanged" />

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

                                                <asp:UpdatePanel runat="server" ID="UpdatePanel36" UpdateMode="Conditional">
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="ddlNemotecnico" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddlFondo" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddlNombreFondo" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtFechaSuscripcion" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtFechaNAV" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtFechaTC" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtFechaIntencion" EventName="TextChanged" />
                                                    </Triggers>
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtCuotasEmitidas" CssClass="form-control dbs-entero16-decimal0" runat="server" ReadOnly="true" EventName="SelectionChanged" />
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                            <div class="col-md-6">
                                                <asp:Label runat="server" ID="Label30">Acumulada</asp:Label>

                                                <asp:UpdatePanel runat="server" ID="UpdatePanel29" UpdateMode="Conditional">
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="ddlNemotecnico" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddlFondo" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddlNombreFondo" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtFechaSuscripcion" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtFechaNAV" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtFechaTC" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtFechaIntencion" EventName="TextChanged" />
                                                    </Triggers>
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtAcumulada" CssClass="form-control dbs-entero16-decimal0" runat="server" ReadOnly="true" EventName="SelectionChanged" />
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>


                                        <div class="row mt-3">
                                            <div class="col-md-6">
                                                <asp:Label runat="server" ID="Label31">Actual</asp:Label>

                                                <asp:UpdatePanel runat="server" ID="UpdatePanel30" UpdateMode="Conditional">
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="ddlNemotecnico" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddlFondo" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddlNombreFondo" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtFechaSuscripcion" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtFechaNAV" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtFechaTC" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtFechaIntencion" EventName="TextChanged" />
                                                    </Triggers>
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtActual" CssClass="form-control dbs-entero16-decimal0" runat="server" ReadOnly="true" EventName="SelectionChanged" />
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                            <div class="col-md-6">
                                                <asp:Label runat="server" ID="Label32">Utilizado</asp:Label>

                                                <asp:UpdatePanel runat="server" ID="UpdatePanel31" UpdateMode="Conditional">
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="ddlNemotecnico" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddlFondo" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddlNombreFondo" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtFechaSuscripcion" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtFechaNAV" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtFechaTC" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtFechaIntencion" EventName="TextChanged" />
                                                    </Triggers>
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtUtilizado" CssClass="form-control dbs-entero16-decimal0" runat="server" ReadOnly="true" EventName="SelectionChanged" />
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>



                                        <div class="row mt-3">
                                            <div class="col-md-6">
                                                <asp:Label runat="server" ID="Label33">DISPONIBLES</asp:Label>

                                                <asp:UpdatePanel runat="server" ID="UpdatePanel32" UpdateMode="Conditional">
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="ddlNemotecnico" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddlFondo" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddlNombreFondo" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtFechaSuscripcion" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtFechaNAV" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtFechaTC" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtFechaIntencion" EventName="TextChanged" />
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
                                                        <asp:AsyncPostBackTrigger ControlID="ddlNemotecnico" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtMonto" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddlFondo" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddlNombreFondo" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddlModalRutAportante" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddlModalNombreAportante" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddlModalMultifondo" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtFechaSuscripcion" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtFechaNAV" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtFechaTC" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtFechaIntencion" EventName="TextChanged" />
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
                                                        <asp:AsyncPostBackTrigger ControlID="ddlNemotecnico" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddlFondo" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddlNombreFondo" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddlModalRutAportante" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddlModalNombreAportante" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddlModalMultifondo" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtFechaSuscripcion" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtFechaNAV" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtFechaTC" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtFechaIntencion" EventName="TextChanged" />
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
                                                        <asp:AsyncPostBackTrigger ControlID="ddlNemotecnico" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddlFondo" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddlNombreFondo" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddlModalRutAportante" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddlModalNombreAportante" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddlModalMultifondo" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtFechaSuscripcion" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtFechaNAV" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtFechaTC" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtFechaIntencion" EventName="TextChanged" />
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
                                                        <asp:AsyncPostBackTrigger ControlID="ddlNemotecnico" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddlFondo" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddlNombreFondo" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddlModalRutAportante" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddlModalNombreAportante" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddlModalMultifondo" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtFechaSuscripcion" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtFechaNAV" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtFechaTC" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtFechaIntencion" EventName="TextChanged" />
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
                                                        <asp:AsyncPostBackTrigger ControlID="ddlNemotecnico" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddlFondo" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddlNombreFondo" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddlModalRutAportante" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddlModalNombreAportante" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddlModalMultifondo" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtFechaSuscripcion" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtFechaNAV" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtFechaTC" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtFechaIntencion" EventName="TextChanged" />
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
                                <asp:button id="btnPrueba" text="Mostrar PopUp" cssclass="btn btn-info" runat="server" Visible="false"></asp:button>
                                <asp:Button ID="btnModalGuardar" Text="Guardar" CssClass="btn btn-info" runat="server" OnClientClick="return validateBtn();"></asp:Button>
                                <asp:Button ID="btnModalModificar" Text="Modificar" CssClass="btn btn-info" runat="server" OnClientClick="return validateBtn();"></asp:Button>
                                <asp:Button ID="btnModalCancelar" Text="Cancelar" CssClass="btn btn-secondary" runat="server" OnClientClick="if (!confirm('¿Seguro que desea Cancelar?')) return false;"></asp:Button>
                                <asp:Button ID="btnModalEliminar" Text="Eliminar" runat="server" class="btn btn-danger" Width="15%" OnClientClick="if (!confirm('¿Seguro que desea Eliminar los elementos seleccionados?')) return false;"></asp:Button>
                            </div>
                        </div>
                        <!-- FIN GRUPO DE BOTONES 2 -->
                    </div>
                </div>
            </div>
        </div>
        <!-- End Bootstrap Modal Dialog Crear/Modificar -->

        <!-- Bootstrap Modal Dialog Mensajes-->
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
    </div>

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

    <!--PopUp Suscripciones-->
    <div class="modal fade" id="PopUpSuscripciones" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" data-keyboard="false" data-backdrop="static">
        <div class="modal-dialog" style="max-width: 90%;">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title">
                        <asp:label id="Label35" runat="server" text="CONFIRMACION SOLICITUD DE APORTE"
                            font-bold="true" font-size="X-Large"> </asp:label>
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
                                    <asp:label id="lblPopUpFechaSolicitud" runat="server" text="" font-bold="true" font-size="X-Large"> </asp:label>
                                </div>
                            </div>
                            <div class="row">
                                <!-- Hora Solicitud-->
                                <div class="col-md-4">
                                    <label class="form-control-label">Hora Solicitud</label>
                                </div>
                                <div class="col-md-8">
                                    <asp:label id="lblPopUpHoraSolicitud" runat="server" text="" font-bold="true" font-size="X-Large"> </asp:label>
                                </div>
                            </div>
                            <div class="row">
                                <!-- Tipo-->
                                <div class="col-md-4">
                                    <label class="form-control-label">Tipo</label>
                                </div>
                                <div class="col-md-8">
                                    <asp:label id="lblPopUpTipo" runat="server" text="" font-bold="true" font-size="X-Large"> </asp:label>
                                </div>
                            </div>
                            <div class="row">
                                <!-- Fondo-->
                                <div class="col-md-4">
                                    <label class="form-control-label">Fondo</label>
                                </div>
                                <div class="col-md-8">
                                    <asp:label id="lblPopUpFondo" runat="server" text="" font-bold="true" font-size="X-Large"> </asp:label>
                                </div>
                            </div>
                            <div class="row">
                                <!-- Nombre Fondo-->
                                <div class="col-md-4">
                                    <label class="form-control-label">Nombre Fondo</label>
                                </div>
                                <div class="col-md-8">
                                    <asp:label id="lblPopUpNombreFondo" runat="server" text="" font-bold="true" font-size="X-Large"> </asp:label>
                                </div>
                            </div>
                            <div class="row">
                                <!-- Serie-->
                                <div class="col-md-4">
                                    <label class="form-control-label">Serie</label>
                                </div>
                                <div class="col-md-8">
                                    <asp:label id="lblPopUpSerie" runat="server" text="" font-bold="true" font-size="X-Large"> </asp:label>
                                </div>
                            </div>
                            <div class="row">
                                <!-- Administradora-->
                                <div class="col-md-4">
                                    <label class="form-control-label">Administradora</label>
                                </div>
                                <div class="col-md-8">
                                    <asp:label id="lblPopUpAdministradora" runat="server" text="" font-bold="true" font-size="X-Large"> </asp:label>
                                </div>
                            </div>
                            <div class="row">
                                <!-- RUT Administradora-->
                                <div class="col-md-4">
                                    <label class="form-control-label">RUT Administradora</label>
                                </div>
                                <div class="col-md-8">
                                    <asp:label id="lblPopUpRutAdministradora" runat="server" text="" font-bold="true" font-size="X-Large"> </asp:label>
                                </div>
                            </div>
                            <div class="row">
                                <!-- Nombre Aportante-->
                                <div class="col-md-4">
                                    <label class="form-control-label">Nombre Aportante</label>
                                </div>
                                <div class="col-md-8">
                                    <asp:label id="lblPopUpNombreAportante" runat="server" text="" font-bold="true" font-size="X-Large"> </asp:label>
                                </div>
                            </div>
                            <div class="row">
                                <!-- Rut Aportante-->
                                <div class="col-md-4">
                                    <label class="form-control-label">Rut Aportante</label>
                                </div>
                                <div class="col-md-8">
                                    <asp:label id="lblPopUpRutAportante" runat="server" text="" font-bold="true" font-size="X-Large"> </asp:label>
                                </div>
                            </div>
                            <div class="row">
                                <!-- Moneda de Pago -->
                                <div class="col-md-4">
                                    <label class="form-control-label">Moneda de Pago</label>
                                </div>
                                <div class="col-md-8">
                                    <asp:label id="lblPopUpMonedaDePago" runat="server" text="" font-bold="true" font-size="X-Large"> </asp:label>
                                </div>
                            </div>
                            <div class="row">
                                <!-- Cuotas / Monto  -->
                                <div class="col-md-4">
                                    <label class="form-control-label">Cuotas / Monto</label>
                                </div>
                                <div class="col-md-8">
                                    <asp:label id="lblPopUpCuotasMonto" runat="server" text="" font-bold="true" font-size="X-Large"> </asp:label>
                                </div>
                            </div>
                            <div class="row">
                                <!-- NAV -->
                                <div class="col-md-4">
                                    <label class="form-control-label">NAV </label>
                                </div>
                                <div class="col-md-8">
                                    <asp:label id="lblPopUpNav" runat="server" text="" font-bold="true" font-size="X-Large"> </asp:label>
                                </div>
                            </div>
                            <div class="row">
                                <!-- Fecha NAV -->
                                <div class="col-md-4">
                                    <label class="form-control-label">Fecha NAV</label>
                                </div>
                                <div class="col-md-8">
                                    <asp:label id="lblPopUpFechaNav" runat="server" text="" font-bold="true" font-size="X-Large"> </asp:label>
                                </div>
                            </div>
                            <div class="row">
                                <!-- Monto del Aporte-->
                                <div class="col-md-4">
                                    <label class="form-control-label">Monto del Aporte</label>
                                </div>
                                <div class="col-md-8">
                                    <asp:label id="lblPopUpMontoDelAporte" runat="server" text="" font-bold="true" font-size="X-Large"> </asp:label>
                                </div>
                            </div>
                            <div class="row">
                                <!-- Cuotas de Aporte -->
                                <div class="col-md-4">
                                    <label class="form-control-label">Cuotas de Aporte</label>
                                </div>
                                <div class="col-md-8">
                                    <asp:label id="lblPopUpCuotasDeAporte" runat="server" text="" font-bold="true" font-size="X-Large"> </asp:label>
                                </div>
                            </div>
                            <div class="row">
                                <!-- Contrato Gral Fondos-->
                                <div class="col-md-4">
                                    <label class="form-control-label">Contrato Gral Fondos</label>
                                </div>
                                <div class="col-md-8">
                                    <asp:label id="lblPopUpContratoGralFondos" runat="server" text="" font-bold="true" font-size="X-Large"> </asp:label>
                                </div>
                            </div>
                            <div class="row">
                                <!-- Poderes-->
                                <div class="col-md-4">
                                    <label class="form-control-label">Poderes</label>
                                </div>
                                <div class="col-md-8">
                                    <asp:label id="lblPopUpPoderes" runat="server" text="" font-bold="true" font-size="X-Large"> </asp:label>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="FooterScript" runat="Server">
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
            $("[id*=txtIntencionDesde]").datepicker();
            $("[id*=txtIntencionHasta]").datepicker();
            $("[id*=txtNAVDesde]").datepicker();
            $("[id*=txtNAVHasta]").datepicker();
            $("[id*=txtSuscripcionDesde]").datepicker();
            $("[id*=txtSuscripcionHasta]").datepicker();

            $("[id*=txtFechaIntencion]").datepicker();
            $("[id*=txtFechaIntencion]").datepicker({
                container: '#myModal modal-body'
                , showOn: "none"
            });

            $("[id*=txtFechaNAV]").datepicker();
            $("[id*=txtFechaNAV]").datepicker({
                container: '#myModal modal-body'
                , showOn: "none"
            });

             $("[id*=txtFechaSuscripcion]").datepicker();
            $("[id*=txtFechaSuscripcion]").datepicker({
                container: '#myModal modal-body'
                , showOn: "none"
            });

             $("[id*=txtFechaTC]").datepicker();
            $("[id*=txtFechaTC]").datepicker({
                container: '#myModal modal-body'
                , showOn: "none"
            });
            
             $("[id*=txtIntencionDesde]").change(function(){
                  changeFechas( $("[id*=txtIntencionDesde]"), $("[id*=txtIntencionHasta]"), 1)
            });
            $("[id*=txtIntencionHasta]").change(function(){
                  changeFechas( $("[id*=txtIntencionDesde]"), $("[id*=txtIntencionHasta]"), 2)
            });

             $("[id*=txtNAVDesde]").change(function(){
                  changeFechas( $("[id*=txtNAVDesde]"), $("[id*=txtNAVHasta]"), 1)
            });
            $("[id*=txtNAVHasta]").change(function(){
                  changeFechas( $("[id*=txtNAVDesde]"), $("[id*=txtNAVHasta]"), 2)
            });

            $("[id*=txtSuscripcionDesde]").change(function(){
                  changeFechas( $("[id*=txtSuscripcionDesde]"), $("[id*=txtSuscripcionHasta]"), 1)
            });
            $("[id*=txtSuscripcionHasta]").change(function(){
                  changeFechas( $("[id*=txtSuscripcionDesde]"), $("[id*=txtSuscripcionHasta]"), 2)
            });
        }



        $(document).ready(function () {
            var txtHiddenAccion = $('#<%=txtAccionHidden.ClientID %>').val();

            //formatosNumeros()

            confNumeros();

            if ((txtHiddenAccion == "MODIFICAR") || (txtHiddenAccion == "CREAR") || (txtHiddenAccion == "ELIMINAR")) {
                $('#myModal').modal('show');
            } else if (txtHiddenAccion == "POPUPSUSCRIPCIONES") {
                $('#PopUpSuscripciones').modal('show');
            }else if (txtHiddenAccion == "MOSTRAR_DIALOGO") {
                $('#myModalmg').modal();
            } else {
                checkRadioBtn("");
            }
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(bindDataTable);
            calendarInitial();

            
        });

        function confNumeros() {            

            $('.dbs-entero20-decimal6').mask2(getMask(20, 6));
            $('.dbs-entero-decimal').mask2(getMask(16, 0));
            $('.dbs-entero20-decimal2').mask2(getMask(20, 2));
            $('.dbs-entero20-decimal4').mask2(getMask(20, 4));
            $('.dbs-entero20-decimal0').mask2(getMask(20, 0));
        }  


        function formatosNumeros() {

            $(".ConDecimales").on({
                "focus": function (event) {
                    $(event.target).select()

                    console.log()
                    return $(this).val.replace(".","")
                },

                "focusout": function (event) {
                    $(event.target).val(function (index, value) {

                        var arrayDeCadenas = value.split(",")
                        valEnetero = arrayDeCadenas[0].replace(/\D/g, "")
                            .replace(/([0-9])([0-9]{3})$/g, '$1.$2')
                            .replace(/\B(?=(\d{3})+(?!\d)\.?)/g, ".");

                        cadenaClases = $(this).attr("Class").split(" ")
                        cant = cadenaClases.indexOf("ConDecimales") + 1

                        valDecimal = arrayDeCadenas[1]

                        if (valDecimal == null || valDecimal == "") {
                            return valEnetero;
                        }

                        var max_chars = cadenaClases[cant];
                        if (valDecimal.length >= max_chars) {
                            valDecimal = valDecimal.substr(0, max_chars)
                        }

                        return valEnetero + "," + valDecimal;
                    });
                }
            });
            		
        }

        function noPuntoComa(event) {
            var e = event || window.event;
            var key = e.keyCode || e.which;
            if (key === 110 || key === 190 || key === 188) {
                e.preventDefault();
            }
        }

        function setToCapitalizedNumber(e) {
            var e = event || window.event;
            var key = e.keyCode || e.which;
            if (key === 110 || key === 190 || key === 188) {
                e.preventDefault();
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
                var rutTxt = document.getElementById('<%=ddlListaRutAportante.ClientID%>');
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

        function bindDataTable() {

            $(".js-select2-rut").select2({
                templateResult: formatState,
                placeholder: 'Selecciona una opción'
            });
            calendarInitial();
            confNumeros();
        };


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
                        enableDisableButtons(false)
                    }
                }
            }
        }

        function CheckTxtEmpty() {
            if ($('#<%=ddlModalNombreAportante.ClientID%>').val() == "") {
                alert("Debe seleccionar un nombre de aportante")
                return false;
            }
            if ($('#<%=ddlFondo.ClientID %>').val() == "") {
                alert("Debe seleccionar el rut de un fondo")
                return false;
            }
            if ($('#<%=ddlModalRutAportante.ClientID%>').val() == "") {
                alert("Debe seleccionar un rut de aportante")
                return false;
            }
            if ($('#<%=ddlNemotecnico.ClientID%>').val() == "") {
                alert("Debe seleccionar un Nemotécnico")
                return false;
            }
            if ($('#<%=txtCuotas.ClientID%>').val() == "") {
                alert("Debe digitar las cuotas a suscribir")
                return false;
            }
            if ($('#<%=txtMonto.ClientID%>').val() == "") {
                alert("Debe ingresar un monto")
                return false;
            }
            if ($('#<%=ddlMonedaPago.ClientID%>').val() == "") {
                alert("Debe seleccionar la moneda de pago")
                return false;
            }
            if ($('#<%=txtMontoCLP.ClientID%>').val() == "") {
                alert("Debe ingresar el monto CLP")
                return false;
            }
            if ($('<%=txtTCObservado.ClientID%>').val() == "") {
                alert("Debe ingresar el TC observado")
                return false
            }
            <%--if ($('#<%=ddlPoderes.ClientID%>').val() == "") {
                    alert("Debe seleccionar una opción en el campo 'Poderes'")
                    return false;
            } --%>
            if ($('#<%=txtFijacionNAV.ClientID%>').val() == "") {
                alert("El campo fijación NAV está vacío")
                return false
            }
            if ($('#<%=txtNAV.ClientID%>').val() == "") {
                alert("Debe ingresar un valor NAV")
                return false
            }
            if ($('#<%=txtNAVCLP.ClientID%>').val() == "") {
                alert("Debe ingresar un valor NAV CLP")
                return false
            }
            if ($('#<%=ddlEstado1.ClientID%>').val() == "") {
                alert("Debe seleccionar el estado de la suscripción");
                return false;
            }
            if ($('#<%=txtFechaIntencion.ClientID%>').val() == "") {
                alert("Debe seleccionar una fecha intención");
                return false;
            }
            if ($('#<%=txtFechaNAV.ClientID%>').val() == "") {
                alert("Debe seleccionar una fecha NAV");
                return false;
            }
            if ($('#<%=txtFechaSuscripcion.ClientID%>').val() == "") {
                alert("Debe seleccionar una fecha suscripción");
                return false;
            }
            if ($('#<%=txtFechaTC.ClientID%>').val() == "") {
                alert("Debe seleccionar una fecha TC");
                return false;
            }
            <%--if ($('#<%=txtCuotasDCV.ClientID%>').val() == "" || $('#<%=txtCuotasDCV.ClientID%>').val() == "0") {
                alert("No existen cuotas DCV cargadas.");
                return false;
            }--%>
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
