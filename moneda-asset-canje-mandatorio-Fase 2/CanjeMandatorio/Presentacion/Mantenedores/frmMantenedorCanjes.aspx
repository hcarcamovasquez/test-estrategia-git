<%@ Page Title="Maestro de Canjes" Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false" CodeFile="frmMantenedorCanjes.aspx.vb" Inherits="Presentacion_Mantenedores_frmMantenedorCanjes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" />

    <h2 class="TdRedondeado titleMant">Maestro de <strong>Canjes</strong></h2>

    <div class="card p-4 jumbotron">

        <div class="row">
            <!-- LISTA RUT APORTANTE-->
            <div class="col-md-3">
                <asp:Label runat="server" ID="rutaportante">Aportante</asp:Label>
                <asp:DropDownList ID="ddlListaRutAportante" CssClass="form-control js-select2-rut" runat="server" AutoPostBack="true" />
            </div>
            <!-- LISTA RUT FONDO-->
            <div class="col-md-3">
                <asp:Label runat="server" ID="rutfondo">Fondo</asp:Label>
                <asp:DropDownList ID="ddlListaRutFondo" CssClass="form-control js-select2-rut" runat="server" AutoPostBack="true" />
            </div>
            <!-- NEMOTÉCNICO -->
            <div class="col-md-3">
                <asp:Label runat="server" ID="nemotecnico">Nemotécnico</asp:Label>
                <asp:DropDownList ID="ddlListaNemotecnico" CssClass="form-control js-select2-rut" runat="server" AutoPostBack="true" />
            </div>
            <!-- ESTADO -->
            <div class="col-md-3">
                <asp:Label runat="server" ID="Label6">Estado</asp:Label>
                <asp:DropDownList ID="ddlEstado" CssClass="form-control js-select2-rut" runat="server" />
            </div>
        </div>

        <div class="row mt-4">
            <!-- FECHA DESDE -->
            <div class="col-md-3">
                <asp:Label runat="server" ID="Label2">Fecha Solicitud Desde</asp:Label>
                <div class="input-group">
                    <asp:TextBox ID="txtFechaSolicitudDesde" runat="server" CssClass="form-control datepicker" ReadOnly="True"></asp:TextBox>
                    <asp:LinkButton ID="lnkbtnFechaSolicitudDesde" class="btn btn-moneda" runat="server"
                        OnClientClick="return clickCalendar('txtFechaSolicitudDesde')"><i class="far fa-calendar-alt"></i></asp:LinkButton>

                    <asp:LinkButton ID="BtnLimpiarFechaHasta" Text="" class="btn btn-secondary ml-1" runat="server"
                        OnClientClick="return limpiarCalendar('txtFechaSolicitudDesde')"><i class="far fa-trash-alt"></i></asp:LinkButton>
                </div>
            </div>
            <!-- FECHA HASTA -->
            <div class="col-md-3">
                <asp:Label runat="server" ID="Label3">Fecha Solicitud Hasta</asp:Label>
                <div class="input-group">
                    <asp:TextBox ID="txtFechaSolicitudHasta" runat="server" CssClass="form-control datepicker" ReadOnly="True"></asp:TextBox>
                    <asp:LinkButton ID="lkbtnFechaSolicitudHasta" class="btn btn-moneda" runat="server" OnClientClick="return clickCalendar('txtFechaSolicitudHasta')"><i class="far fa-calendar-alt"></i></asp:LinkButton>
                    <asp:LinkButton ID="LinkButton1" Text="" class="btn btn-secondary ml-1" runat="server" OnClientClick="return limpiarCalendar('txtFechaSolicitudHasta')"><i class="far fa-trash-alt"></i></asp:LinkButton>
                </div>
            </div>

            <!-- FECHA NAV DESDE -->
            <div class="col-md-3">
                <asp:Label runat="server" ID="Label4">Fecha NAV desde</asp:Label>
                <div class="input-group">
                    <asp:TextBox ID="txtFechaNavDesde" runat="server" CssClass="form-control datepicker" ReadOnly="True"></asp:TextBox>
                    <asp:LinkButton ID="lkbtnFechaNavDesde" class="btn btn-moneda" runat="server" OnClientClick="return clickCalendar('txtFechaNavDesde')"><i class="far fa-calendar-alt"></i></asp:LinkButton>
                    <asp:LinkButton ID="LinkButton2" Text="" class="btn btn-secondary ml-1" runat="server"
                        OnClientClick="return limpiarCalendar('txtFechaNavDesde')"><i class="far fa-trash-alt"></i></asp:LinkButton>
                </div>
            </div>
            <!-- FECHA NAV HASTA -->
            <div class="col-md-3">
                <asp:Label runat="server" ID="Label7">Fecha NAV hasta</asp:Label>
                <div class="input-group">
                    <asp:TextBox ID="txtFechaNavHasta" runat="server" CssClass="form-control datepicker" ReadOnly="True"></asp:TextBox>
                    <asp:LinkButton ID="lkbtnFechaNavHasta" class="btn btn-moneda" runat="server" OnClientClick="return clickCalendar('txtFechaNavHasta')"><i class="far fa-calendar-alt"></i></asp:LinkButton>
                    <asp:LinkButton ID="LinkButton3" Text="" class="btn btn-secondary ml-1" runat="server" OnClientClick="return limpiarCalendar('txtFechaNavHasta')"><i class="far fa-trash-alt"></i></asp:LinkButton>
                </div>
            </div>
        </div>
        <div class="row mt-4">
            <!-- FECHA DESDE -->
            <div class="col-md-3">
                <asp:Label runat="server" ID="Label1">Fecha Canje Desde</asp:Label>
                <div class="input-group">
                    <asp:TextBox ID="txtFechaCanjeDesde" runat="server" CssClass="form-control datepicker" ReadOnly="True"></asp:TextBox>
                    <asp:LinkButton ID="lnkFechaCanjeDesde" class="btn btn-moneda" runat="server" OnClientClick="return clickCalendar('txtFechaCanjeDesde')"><i class="far fa-calendar-alt"></i></asp:LinkButton>
                    <asp:LinkButton ID="Linkbutton5" Text="" class="btn btn-secondary ml-1" runat="server" OnClientClick="return limpiarCalendar('txtFechaCanjeDesde')"><i class="far fa-trash-alt"></i></asp:LinkButton>

                </div>
            </div>
            <!-- FECHA HASTA -->
            <div class="col-md-3">
                <asp:Label runat="server" ID="Label5">Fecha Canje Hasta</asp:Label>
                <div class="input-group">
                    <asp:TextBox ID="txtFechaCanjeHasta" runat="server" CssClass="form-control datepicker" ReadOnly="True"></asp:TextBox>
                    <asp:LinkButton ID="lnkFechaCanjeHasta" class="btn btn-moneda" runat="server" OnClientClick="return clickCalendar('txtFechaCanjeHasta')"><i class="far fa-calendar-alt"></i></asp:LinkButton>
                    <asp:LinkButton ID="LinkButton7" Text="" class="btn btn-secondary ml-1" runat="server" OnClientClick="return limpiarCalendar('txtFechaCanjeHasta')"><i class="far fa-trash-alt"></i></asp:LinkButton>
                </div>
            </div>
        </div>

        <!-- BOTONES BUSCAR LIMPIAR Y CREAR -->
        <div class="row text-center mt-5 p-3 border-bottom">
            <div class="col-md-12">
                <!-- BOTÓN BUSCAR -->
                <asp:Button ID="BtnBuscar" Text="Buscar" class="btn btn-moneda" runat="server" />
                <asp:Button ID="btnLimpiarFrm" Text="Borrar" class="btn btn-secondary" runat="server" OnClick="btnLimpiarFrm_Click" />

                <!-- BOTÓN CREAR -->
                <asp:Button ID="btnCrear" Text="Crear" class="btn btn-info" runat="server" />
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
                        <ItemTemplate>
                            <asp:RadioButton ID="RowSelector" runat="server" onclick="checkRadioBtn(this);" GroupName="a" AutoPostBack="false" OnCheckedChanged="RowSelector_CheckedChanged" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="IdCanje" HeaderText="ID" />
                    <asp:BoundField DataField="TipoTransaccion" HeaderText="Tipo de Transacción" />
                    <asp:BoundField DataField="RutAportante" HeaderText="Rut Aportante" />
                    <asp:BoundField DataField="NombreAportante" HeaderText="Nombre Aportante" />
                    <asp:BoundField DataField="Multifondo" HeaderText="Multifondo" />
                    <asp:BoundField DataField="RutFondo" HeaderText="Rut Fondo" />
                    <asp:BoundField DataField="NombreFondo" HeaderText="Nombre Fondo" />
                    <asp:BoundField DataField="FechaSolicitud" HeaderText="Fecha Solicitud" DataFormatString="{0:dd/MM/yyyy}" />
                    <asp:BoundField DataField="FechaObservado" HeaderText="Fecha Observado" DataFormatString="{0:dd/MM/yyyy}" />
                    <asp:BoundField DataField="FechaCanje" HeaderText="Fecha Canje" DataFormatString="{0:dd/MM/yyyy}" />
                    <asp:BoundField DataField="FechaNavSaliente" HeaderText="Fecha Nav Saliente" DataFormatString="{0:dd/MM/yyyy}" />
                    <asp:BoundField DataField="NemotecnicoSaliente" HeaderText="Nemotécnico Saliente" />
                    <asp:BoundField DataField="NombreSerieSaliente" HeaderText="Serie Saliente" />
                    <asp:BoundField DataField="MonedaSaliente" HeaderText="Moneda Serie Saliente" />
                    <asp:BoundField DataField="cuotaSalientePaso" HeaderText="Cuotas Salientes" DataFormatString="{0:N0}" ItemStyle-HorizontalAlign="Right" />
                    <asp:BoundField DataField="NavSalienteFormat" HeaderText="Nav Saliente" DataFormatString="{0:N6}" ItemStyle-HorizontalAlign="Right" />
                    <asp:BoundField DataField="MontoSalientePaso" HeaderText="Monto Saliente" DataFormatString="{0:N2}" ItemStyle-HorizontalAlign="Right" />
                    <asp:BoundField DataField="NavCLPSalienteFormat" HeaderText="Nav CLP Saliente" DataFormatString="{0:N4}" ItemStyle-HorizontalAlign="Right" />
                    <asp:BoundField DataField="MontoCLPSaliente" HeaderText="Monto CLP Saliente" DataFormatString="{0:N0}" ItemStyle-HorizontalAlign="Right" />
                    <asp:BoundField DataField="Factor" HeaderText="Factor" DataFormatString="{0:N14}" ItemStyle-HorizontalAlign="Right" />
                    <asp:BoundField DataField="Diferencia" HeaderText="Diferencia" DataFormatString="{0:N2}" ItemStyle-HorizontalAlign="Right" />
                    <asp:BoundField DataField="DiferenciaCLP" HeaderText="Diferencia CLP" DataFormatString="{0:N0}" ItemStyle-HorizontalAlign="Right" />
                    <asp:BoundField DataField="FechaNavEntrante" HeaderText="Fecha Nav Entrante" DataFormatString="{0:dd/MM/yyyy}" />
                    <asp:BoundField DataField="NemotecnicoEntrante" HeaderText="Nemotécnico Entrante" />
                    <asp:BoundField DataField="NombreSerieEntrante" HeaderText="Serie Entrante" />
                    <asp:BoundField DataField="MonedaEntrante" HeaderText="Moneda Serie Entrante" />
                    <asp:BoundField DataField="cuotaEntrantePaso" HeaderText="Cuotas Entrantes" DataFormatString="{0:N0}" ItemStyle-HorizontalAlign="Right" />
                    <asp:BoundField DataField="NavEntranteFormat" HeaderText="Nav Entrante" DataFormatString="{0:N6}" ItemStyle-HorizontalAlign="Right" />
                    <asp:BoundField DataField="MontoEntrantePaso" HeaderText="Monto Entrante" DataFormatString="{0:N2}" ItemStyle-HorizontalAlign="Right" />
                    <asp:BoundField DataField="NavCLPEntranteFormat" HeaderText="Nav CLP Entrante" DataFormatString="{0:N4}" ItemStyle-HorizontalAlign="Right" />
                    <asp:BoundField DataField="MontoCLPEntrante" HeaderText="Monto CLP Entrante" DataFormatString="{0:N0}" ItemStyle-HorizontalAlign="Right" />
                    <asp:BoundField DataField="ContratoGeneral" HeaderText="Contrato" />
                    <asp:BoundField DataField="RevisionPoderes" HeaderText="Poderes" />
                    <asp:BoundField DataField="EstadoCanje" HeaderText="Estado Canje" />
                    <asp:BoundField DataField="Observaciones" HeaderText="Observaciones" />
                    <asp:BoundField DataField="FechaActual" HeaderText="Fecha DVC Tránsito" DataFormatString="{0:dd/MM/yyyy}" />
                    <asp:BoundField DataField="Cuotas" HeaderText="Cuota DVC Tránsito" DataFormatString="{0:N0}" ItemStyle-HorizontalAlign="Right" />
                    <asp:BoundField DataField="RescateTransito" HeaderText="Rescate Tránsito" DataFormatString="{0:N0}" ItemStyle-HorizontalAlign="Right" />
                    <asp:BoundField DataField="SuscripcionTransito" HeaderText="Suscripción Tránsito" DataFormatString="{0:N0}" ItemStyle-HorizontalAlign="Right" />
                    <asp:BoundField DataField="CanjeTransito" HeaderText="Canje Tránsito" DataFormatString="{0:N0}" ItemStyle-HorizontalAlign="Right" />
                    <asp:BoundField DataField="cuotasDisponiblesPaso" HeaderText="Cuotas Disponibles" DataFormatString="{0:N0}" ItemStyle-HorizontalAlign="Right" />
                    <asp:BoundField DataField="FijacionNav" HeaderText="Fijación Nav" />
                    <asp:BoundField DataField="FijacionTC" HeaderText="Fijación TC" />
                    <asp:BoundField DataField="TipoCambio" HeaderText="Tipo Cambio" ItemStyle-HorizontalAlign="Right" />
                    <asp:BoundField DataField="FechaIngreso" HeaderText="Fecha Ingreso" DataFormatString="{0:dd/MM/yyyy}" />
                    <asp:BoundField DataField="UsuarioIngreso" HeaderText="Usuario Ingreso" />
                    <asp:BoundField DataField="FechaModificacion" HeaderText="Fecha Modificación" DataFormatString="{0:dd/MM/yyyy}" />
                    <asp:BoundField DataField="UsuarioModificacion" HeaderText="Usuario Modificador" />
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
        <div class="modal-dialog" style="max-width: 98%;">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title">
                        <asp:Label ID="lbModalTittle" runat="server" Text="Formulario - Canjes" Font-Bold="true" Font-Size="X-Large">
                        </asp:Label>
                    </h4>
                    <button id="btnXCerrar" type="button" class="close" data-dismiss="modal" aria-label="Close" runat="server"><span aria-hidden="true">&times;</span></button>
                </div>
                <div class="modal-body">
                    <div class="jumbotron p-3">
                        <asp:HiddenField ID="txtEstadoCambio" runat="server"></asp:HiddenField>
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
                                                <asp:UpdatePanel runat="server" ID="UpdatePanel8" UpdateMode="Conditional">
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="ddlModalMultifondo" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddlModalNombreAportante" EventName="SelectedIndexChanged" />
                                                    </Triggers>
                                                    <ContentTemplate>
                                                        <asp:DropDownList ID="ddlModalRutAportante" CssClass="form-control js-select2-rut" runat="server" OnSelectedIndexChanged="LlenarMultifondoAportante" AutoPostBack="true" />
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                            <div class="col-md-1">
                                                <label class="form-control-label">Nombre Aportante</label>
                                            </div>
                                            <div class="col-md-3">
                                                <asp:UpdatePanel runat="server" ID="UpdatePanel6" UpdateMode="Conditional">
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="ddlModalRutAportante" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddlModalMultifondo" EventName="SelectedIndexChanged" />
                                                    </Triggers>
                                                    <ContentTemplate>
                                                        <asp:DropDownList ID="ddlModalNombreAportante" CssClass="form-control js-select2-rut" runat="server" OnSelectedIndexChanged="LlenaRutMultifondoAportante" AutoPostBack="true" />
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                            <div class="col-md-1">
                                                <label class="form-control-label">Multifondo</label>
                                            </div>
                                            <div class="col-md-3">
                                                <asp:UpdatePanel runat="server" ID="UpdatePanel7" UpdateMode="Conditional">
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="ddlModalRutAportante" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddlModalNombreAportante" EventName="SelectedIndexChanged" />
                                                    </Triggers>
                                                    <ContentTemplate>
                                                        <asp:DropDownList ID="ddlModalMultifondo" CssClass="form-control js-select2-rut" runat="server" OnSelectedIndexChanged="LlenaRutNombreAportante" AutoPostBack="true" />
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>
                                        <div class="row mt-3">
                                            <div class="col-md-1">
                                                <label class="form-control-label">Fondo</label>
                                            </div>
                                            <div class="col-md-3">
                                                <asp:UpdatePanel runat="server" ID="UpdatePanel15" UpdateMode="Conditional">
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="ddlModalNemotecnicoEntrante" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddlModalNombreFondo" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddlModalNemotecnicoSaliente" EventName="SelectedIndexChanged" />
                                                    </Triggers>
                                                    <ContentTemplate>
                                                        <asp:DropDownList ID="ddlModalFondo" CssClass="form-control js-select2-rut" runat="server"
                                                            OnSelectedIndexChanged="LlenarRutNombreSerie" AutoPostBack="true" />
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>

                                            </div>
                                            <div class="col-md-1">
                                                <label class="form-control-label">Tipo Transacción</label>
                                            </div>
                                            <div class="col-md-3">
                                                <asp:TextBox ID="txtModalTipoTrnasaccion" CssClass="form-control" runat="server" ReadOnly="True">Canje</asp:TextBox>
                                            </div>
                                            <div class="col-md-1">
                                                <label class="form-control-label">Fecha Solicitud</label>
                                            </div>
                                            <div class="col-md-3">
                                                <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Conditional">
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="lnkBtnModalFechaSolicitud" EventName="click" />
                                                    </Triggers>
                                                    <ContentTemplate>
                                                        <div class="input-group">
                                                            <asp:TextBox ID="txtModalFSolicitud" runat="server" CssClass="form-control datepicker" AutoPostBack="true" Enabled="false"></asp:TextBox>

                                                            <asp:LinkButton ID="lnkBtnModalFechaSolicitud" Class="btn btn-moneda" runat="server"
                                                                OnClientClick="return clickCalendar('txtModalFSolicitud')"><i class="far fa-calendar-alt"></i></asp:LinkButton>
                                                            <span id="reqtxtModalFechaSolicitud" class="reqError"></span>
                                                        </div>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>
                                        <div class="row mt-2">
                                            <div class="col-md-1">
                                                <label class="form-control-label">Nombre Fondo</label>
                                            </div>
                                            <div class="col-md-3">

                                                <asp:UpdatePanel runat="server" ID="UpdatePanel11" UpdateMode="Conditional">
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="ddlModalFondo" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddlModalNemotecnicoEntrante" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddlModalNemotecnicoSaliente" EventName="SelectedIndexChanged" />
                                                    </Triggers>
                                                    <ContentTemplate>
                                                        <asp:DropDownList ID="ddlModalNombreFondo" CssClass="form-control js-select2-rut" runat="server" OnSelectedIndexChanged="LlenarRutNemotecnicos" AutoPostBack="true" />
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                            <div class="col-md-1">
                                                <label class="form-control-label">Fijación TC</label>
                                            </div>
                                            <div class="col-md-3">
                                                <asp:UpdatePanel runat="server" ID="UpdatePanel36" UpdateMode="Conditional">
                                                    <Triggers>
                                                        <%--<asp:AsyncPostBackTrigger ControlID="CalendarModalFechaObservado" EventName="SelectionChanged" />--%>
                                                        <asp:AsyncPostBackTrigger ControlID="txtModalFechaObservado" EventName="TextChanged" />
                                                        <%--<asp:AsyncPostBackTrigger ControlID="CalendarModalFechaNavSaliente" EventName="SelectionChanged" />--%>
                                                        <asp:AsyncPostBackTrigger ControlID="txtModalFechaNavSaliente" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddlModalNemotecnicoSaliente" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtModalFSolicitud" EventName="TextChanged" />
                                                    </Triggers>
                                                    <ContentTemplate>
                                                        <asp:DropDownList ID="ddlModalFijacionTC" CssClass="form-control js-select2-rut" runat="server">

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
                                                        <asp:AsyncPostBackTrigger ControlID="lnkbtnModalFechaObservado" EventName="click" />
                                                        <%--<asp:AsyncPostBackTrigger ControlID="CalendarModalFechaNavSaliente" EventName="SelectionChanged" />--%>
                                                        <asp:AsyncPostBackTrigger ControlID="txtModalFechaNavSaliente" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddlModalNemotecnicoSaliente" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddlModalNemotecnicoEntrante" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddlModalNemotecnicoEntrante" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtModalFSolicitud" EventName="TextChanged" />
                                                    </Triggers>
                                                    <ContentTemplate>
                                                        <div class="input-group">
                                                            <asp:TextBox ID="txtModalFechaCanje" runat="server" CssClass="form-control datepicker" AutoPostBack="true" Enabled="false"></asp:TextBox>
                                                            <asp:LinkButton ID="lnkbtnModalFechaCanje" Class="btn btn-moneda" runat="server"
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

                            <div class="col-md-12 d-flex p-0 mb-10">

                                <div class="card mt-0 col-md-6">
                                    <div class="card-body">
                                        <h5 class="card-title">Saliente</h5>
                                        <hr />
                                        <div class="row mt-2">
                                            <div class="col-md-3">
                                                <label class="form-control-label">Nemotécnico</label>
                                            </div>
                                            <div class="col-md-9">
                                                <asp:UpdatePanel runat="server" ID="UpdatePanel9" UpdateMode="Conditional">
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="ddlModalFondo" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddlModalNemotecnicoEntrante" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddlModalNombreFondo" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddlModalNombreFondo" EventName="SelectedIndexChanged" />
                                                    </Triggers>
                                                    <ContentTemplate>
                                                        <asp:DropDownList ID="ddlModalNemotecnicoSaliente" CssClass="form-control js-select2-rut" runat="server"
                                                            OnSelectedIndexChanged="LlenarSerieMonedaSaliente" OnTextChanged="ConsultarFechaNavSaliente" AutoPostBack="true" />
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>
                                        <div class="row mt-2">
                                            <div class="col-md-3">
                                                <label class="form-control-label">Fecha Nav</label>
                                            </div>
                                            <div class="col-md-9">
                                                <asp:UpdatePanel runat="server" ID="UpdatePanel3" UpdateMode="Conditional">
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="lnkbtnModalFechaNavSaliente" EventName="click" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddlModalNemotecnicoSaliente" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtModalFSolicitud" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtModalFechaNavEntrante" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtModalFechaCanje" EventName="TextChanged" />
                                                    </Triggers>
                                                    <ContentTemplate>
                                                        <div class="input-group">
                                                            <asp:TextBox ID="txtModalFechaNavSaliente" runat="server" CssClass="form-control datepicker" AutoPostBack="true" Enabled="false"></asp:TextBox>
                                                            <asp:LinkButton ID="lnkbtnModalFechaNavSaliente" Class="btn btn-moneda" runat="server" Height="5%"
                                                                OnClientClick="return clickCalendar('txtModalFechaNavSaliente')"><i class="far fa-calendar-alt"></i></asp:LinkButton>

                                                            <span id="reqtxtModalFechaNavSaliente" class="reqError"></span>
                                                        </div>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>
                                        <div class="row mt-2">
                                            <div class="col-md-2">
                                                <label class="form-control-label">Serie</label>
                                            </div>
                                            <div class="col-md-4">
                                                <asp:UpdatePanel runat="server" ID="UpdatePanel12" UpdateMode="Conditional">
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="ddlModalNemotecnicoSaliente" EventName="SelectedIndexChanged" />
                                                    </Triggers>
                                                    <ContentTemplate>
                                                        <asp:DropDownList ID="ddlModalSerieSaliente" CssClass="form-control js-select2-rut" runat="server" ReadOnly="True" />
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                            <div class="col-md-2">
                                                <label class="form-control-label">Moneda</label>
                                            </div>
                                            <div class="col-md-4">
                                                <asp:UpdatePanel runat="server" ID="UpdatePanel13" UpdateMode="Conditional">
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="ddlModalNemotecnicoSaliente" EventName="SelectedIndexChanged" />
                                                    </Triggers>
                                                    <ContentTemplate>
                                                        <asp:DropDownList ID="ddlModalMonedaSaliente" CssClass="form-control js-select2-rut" runat="server" ReadOnly="True" />
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>

                                        <div class="row mt-2">
                                            <div class="col-md-2">
                                                <label class="form-control-label">Cuotas</label>

                                            </div>
                                            <div class="col-md-4">
                                                <asp:UpdatePanel runat="server" ID="UpdatePanel37" UpdateMode="Conditional">
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="txtModalMontoSaliente" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddlModalRutAportante" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddlModalNombreAportante" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtModalNavSaliente" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddlModalNemotecnicoSaliente" EventName="SelectedIndexChanged" />

                                                    </Triggers>
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtModalCuotaSaliente" CssClass="form-control dbs-entero13-decimal0"
                                                            onpaste="return false"
                                                            oncut="return false"
                                                            oncopy="return false"
                                                            runat="server"
                                                            OnTextChanged="CalcularCuotaEntrante"
                                                            AutoPostBack="true"></asp:TextBox>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>

                                            </div>
                                            <div class="col-md-2">
                                                <label class="form-control-label">Factor</label>

                                            </div>
                                            <div class="col-md-4">
                                                <asp:UpdatePanel runat="server" ID="UpdatePanel24" UpdateMode="Conditional">
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="ddlModalNemotecnicoSaliente" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtModalNavSaliente" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtModalNavEntrante" EventName="TextChanged" />
                                                        <%--<asp:AsyncPostBackTrigger ControlID="CalendarModalFechaNavSaliente" EventName="SelectionChanged" />--%>
                                                        <asp:AsyncPostBackTrigger ControlID="txtModalFechaNavSaliente" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddlModalNemotecnicoEntrante" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtModalFSolicitud" EventName="TextChanged" />
                                                        <%--<asp:AsyncPostBackTrigger ControlID="CalendarModalFechaNavEntrante" EventName="SelectionChanged" />--%>
                                                        <asp:AsyncPostBackTrigger ControlID="txtModalFechaNavEntrante" EventName="TextChanged" />


                                                    </Triggers>
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtFactorSaliente" CssClass="form-control dbs-entero20-decimal14" runat="server"></asp:TextBox>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>

                                            </div>

                                        </div>
                                        <div class="row mt-2">
                                            <div class="col-md-2">
                                                <label class="form-control-label">Nav</label>

                                            </div>
                                            <div class="col-md-4">
                                                <asp:UpdatePanel runat="server" ID="UpdatePanel16" UpdateMode="Conditional">
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="ddlModalNemotecnicoSaliente" EventName="SelectedIndexChanged" />
                                                        <%--<asp:AsyncPostBackTrigger ControlID="CalendarModalFechaNavSaliente" EventName="SelectionChanged" />--%>
                                                        <asp:AsyncPostBackTrigger ControlID="txtModalFechaNavSaliente" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtModalFSolicitud" EventName="TextChanged" />
                                                        <%--<asp:AsyncPostBackTrigger ControlID="CalendarModalFechaNavEntrante" EventName="SelectionChanged" />--%>
                                                        <asp:AsyncPostBackTrigger ControlID="txtModalFechaNavEntrante" EventName="TextChanged" />

                                                    </Triggers>
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtModalNavSaliente" CssClass="form-control dbs-entero20-decimal6" runat="server" onpaste="return false" oncut="return false" oncopy="return false" onchange="validarValorSaliente()" OnTextChanged="navsalientechange" AutoPostBack="true"> </asp:TextBox>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                            <div class="col-md-2">
                                                <label class="form-control-label">Nav (CLP)</label>

                                            </div>

                                            <div class="col-md-4">
                                                <asp:UpdatePanel runat="server" ID="UpdatePanel29" UpdateMode="Conditional">
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="txtModalNavSaliente" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtModalTipoCambio" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddlModalNemotecnicoSaliente" EventName="SelectedIndexChanged" />
                                                        <%--<asp:AsyncPostBackTrigger ControlID="CalendarModalFechaNavSaliente" EventName="SelectionChanged" />--%>
                                                        <asp:AsyncPostBackTrigger ControlID="txtModalFechaNavSaliente" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtModalFSolicitud" EventName="TextChanged" />
                                                        <%--<asp:AsyncPostBackTrigger ControlID="CalendarModalFechaObservado" EventName="SelectionChanged" />--%>
                                                        <asp:AsyncPostBackTrigger ControlID="txtModalFechaObservado" EventName="TextChanged" />
                                                        <%--<asp:AsyncPostBackTrigger ControlID="CalendarModalFechaNavEntrante" EventName="SelectionChanged" />--%>
                                                        <asp:AsyncPostBackTrigger ControlID="txtModalFechaNavEntrante" EventName="TextChanged" />
                                                    </Triggers>
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtModalNavCLPSaliente" CssClass="form-control dbs-entero20-decimal4" OnTextChanged="ConversionMonedaEntrante" AutoPostBack="true" runat="server"></asp:TextBox>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>
                                        <div class="row mt-2">
                                            <div class="col-md-2">
                                                <label class="form-control-label">Monto</label>

                                            </div>
                                            <div class="col-md-4">
                                                <asp:UpdatePanel runat="server" ID="UpdatePanel30" UpdateMode="Conditional">
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="txtModalCuotaSaliente" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtModalNavSaliente" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddlModalRutAportante" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddlModalNombreAportante" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddlModalNemotecnicoSaliente" EventName="SelectedIndexChanged" />
                                                        <%--<asp:AsyncPostBackTrigger ControlID="CalendarModalFechaNavSaliente" EventName="SelectionChanged" />--%>
                                                        <asp:AsyncPostBackTrigger ControlID="txtModalFechaNavSaliente" EventName="TextChanged" />
                                                        <%--<asp:AsyncPostBackTrigger ControlID="CalendarModalFechaObservado" EventName="SelectionChanged" />--%>
                                                        <asp:AsyncPostBackTrigger ControlID="txtModalFechaObservado" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtModalFSolicitud" EventName="TextChanged" />
                                                        <%--<asp:AsyncPostBackTrigger ControlID="CalendarModalFechaNavEntrante" EventName="SelectionChanged" />--%>
                                                        <asp:AsyncPostBackTrigger ControlID="txtModalFechaNavEntrante" EventName="TextChanged" />
                                                    </Triggers>
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtModalMontoSaliente" CssClass="form-control dbs-entero20-decimal2" runat="server" onpaste="return false" oncut="return false" oncopy="return false" OnTextChanged="CalcularCuotaPorMontoSaliente" AutoPostBack="true"></asp:TextBox>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                            <div class="col-md-2">
                                                <label class="form-control-label">Monto (CLP)</label>

                                            </div>
                                            <div class="col-md-4">
                                                <asp:UpdatePanel runat="server" ID="UpdatePanel31" UpdateMode="Conditional">
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="txtModalCuotaSaliente" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtModalMontoSaliente" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtModalNavSaliente" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtModalTipoCambio" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddlModalRutAportante" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddlModalNombreAportante" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddlModalNemotecnicoSaliente" EventName="SelectedIndexChanged" />
                                                        <%--<asp:AsyncPostBackTrigger ControlID="CalendarModalFechaNavSaliente" EventName="SelectionChanged" />--%>
                                                        <asp:AsyncPostBackTrigger ControlID="txtModalFechaNavSaliente" EventName="TextChanged" />
                                                        <%--<asp:AsyncPostBackTrigger ControlID="CalendarModalFechaObservado" EventName="SelectionChanged" />--%>
                                                        <asp:AsyncPostBackTrigger ControlID="txtModalFechaObservado" EventName="TextChanged" />
                                                        <%--<asp:AsyncPostBackTrigger ControlID="CalendarModalFechaNavEntrante" EventName="SelectionChanged" />--%>
                                                        <asp:AsyncPostBackTrigger ControlID="txtModalFechaNavEntrante" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtModalFSolicitud" EventName="TextChanged" />
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
                                                <asp:UpdatePanel runat="server" ID="UpdatePanel32" UpdateMode="Conditional">
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="txtModalCuotaSaliente" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtModalNavSaliente" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtModalNavEntrante" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtModalMontoSaliente" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddlModalNemotecnicoEntrante" EventName="SelectedIndexChanged" />
                                                        <%--<asp:AsyncPostBackTrigger ControlID="CalendarModalFechaNavSaliente" EventName="SelectionChanged" />--%>
                                                        <asp:AsyncPostBackTrigger ControlID="txtModalFechaNavSaliente" EventName="TextChanged" />
                                                        <%--<asp:AsyncPostBackTrigger ControlID="CalendarModalFechaNavEntrante" EventName="SelectionChanged" />--%>
                                                        <asp:AsyncPostBackTrigger ControlID="txtModalFechaNavEntrante" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddlModalRutAportante" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddlModalNombreAportante" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddlModalNemotecnicoSaliente" EventName="SelectedIndexChanged" />
                                                        <%--<asp:AsyncPostBackTrigger ControlID="CalendarModalFechaObservado" EventName="SelectionChanged" />--%>
                                                        <asp:AsyncPostBackTrigger ControlID="txtModalFechaObservado" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtModalTipoCambio" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtModalFSolicitud" EventName="TextChanged" />
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
                                                <asp:UpdatePanel runat="server" ID="UpdatePanel33" UpdateMode="Conditional">
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="txtModalCuotaSaliente" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtModalNavSaliente" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtModalNavEntrante" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtModalMontoSaliente" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddlModalNemotecnicoEntrante" EventName="SelectedIndexChanged" />
                                                        <%--<asp:AsyncPostBackTrigger ControlID="CalendarModalFechaNavSaliente" EventName="SelectionChanged" />--%>
                                                        <asp:AsyncPostBackTrigger ControlID="txtModalFechaNavSaliente" EventName="TextChanged" />
                                                        <%--<asp:AsyncPostBackTrigger ControlID="CalendarModalFechaNavEntrante" EventName="SelectionChanged" />--%>
                                                        <asp:AsyncPostBackTrigger ControlID="txtModalFechaNavEntrante" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddlModalRutAportante" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddlModalNombreAportante" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddlModalNemotecnicoSaliente" EventName="SelectedIndexChanged" />
                                                        <%--<asp:AsyncPostBackTrigger ControlID="CalendarModalFechaObservado" EventName="SelectionChanged" />--%>
                                                        <asp:AsyncPostBackTrigger ControlID="txtModalFechaObservado" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtModalTipoCambio" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtModalFSolicitud" EventName="TextChanged" />
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
                                                <asp:UpdatePanel runat="server" ID="UpdatePanel22" UpdateMode="Conditional">
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="ddlModalNemotecnicoSaliente" EventName="SelectedIndexChanged" />
                                                        <%--<asp:AsyncPostBackTrigger ControlID="CalendarModalFechaNavSaliente" EventName="SelectionChanged" />--%>
                                                        <asp:AsyncPostBackTrigger ControlID="txtModalFechaNavSaliente" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtModalFSolicitud" EventName="TextChanged" />
                                                    </Triggers>
                                                    <ContentTemplate>
                                                        <asp:DropDownList ID="ddlModalFijacionNav" CssClass="form-control js-select2-rut" runat="server">

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
                                                <asp:UpdatePanel runat="server" ID="UpdatePanel10" UpdateMode="Conditional">
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="ddlModalFondo" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddlModalNombreFondo" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddlModalNemotecnicoSaliente" EventName="SelectedIndexChanged" />
                                                    </Triggers>
                                                    <ContentTemplate>
                                                        <asp:DropDownList ID="ddlModalNemotecnicoEntrante" CssClass="form-control js-select2-rut"
                                                            OnSelectedIndexChanged="LlenarSerieMonedaEntrante" AutoPostBack="true" runat="server" />
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>
                                        <div class="row mt-2">
                                            <div class="col-md-3">
                                                <label class="form-control-label">Fecha Nav</label>
                                            </div>
                                            <div class="col-md-9">
                                                <asp:UpdatePanel runat="server" ID="UpdatePanel5" UpdateMode="Conditional">
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="lnkbtnModalFechaNavEntrante" EventName="click" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddlModalNemotecnicoEntrante" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtModalFSolicitud" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtModalFechaNavSaliente" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddlModalNemotecnicoSaliente" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtModalFechaCanje" EventName="TextChanged" />

                                                    </Triggers>
                                                    <ContentTemplate>
                                                        <div class="input-group">
                                                            <asp:TextBox ID="txtModalFechaNavEntrante" runat="server" CssClass="form-control datepicker" AutoPostBack="true" Enabled="false"></asp:TextBox>
                                                            <asp:LinkButton ID="lnkbtnModalFechaNavEntrante" Class="btn btn-moneda" runat="server" Height="5%"
                                                                OnClientClick="return clickCalendar('txtModalFechaNavEntrante')"><i class="far fa-calendar-alt"></i></asp:LinkButton>

                                                            <span id="reqtxtModalFechaNavEntrante" class="reqError"></span>
                                                        </div>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>

                                        <div class="row mt-2">
                                            <div class="col-md-2">
                                                <label class="form-control-label">Serie</label>
                                            </div>

                                            <div class="col-md-4">
                                                <asp:UpdatePanel runat="server" ID="UpdatePanel4" UpdateMode="Conditional">
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="ddlModalNemotecnicoEntrante" EventName="SelectedIndexChanged" />
                                                    </Triggers>
                                                    <ContentTemplate>
                                                        <asp:DropDownList ID="ddlModalSerieEntrante" CssClass="form-control js-select2-rut" runat="server" ReadOnly="True" />
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>

                                            </div>

                                            <div class="col-md-2">
                                                <label class="form-control-label">Moneda</label>
                                            </div>

                                            <div class="col-md-4">
                                                <asp:UpdatePanel runat="server" ID="UpdatePanel14" UpdateMode="Conditional">
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="ddlModalNemotecnicoEntrante" EventName="SelectedIndexChanged" />
                                                    </Triggers>
                                                    <ContentTemplate>
                                                        <asp:DropDownList ID="ddlModalMonedaEntrante" CssClass="form-control js-select2-rut" runat="server" ReadOnly="True" />
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>

                                        <div class="row mt-2">
                                            <div class="col-md-2">
                                                <label class="form-control-label">Cuotas</label>
                                            </div>

                                            <div class="col-md-4">
                                                <asp:UpdatePanel runat="server" ID="UpdatePanel25" UpdateMode="Conditional">
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="txtModalCuotaSaliente" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtModalNavSaliente" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtModalMontoSaliente" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtModalNavEntrante" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddlModalNemotecnicoEntrante" EventName="SelectedIndexChanged" />
                                                        <%--<asp:AsyncPostBackTrigger ControlID="CalendarModalFechaNavSaliente" EventName="SelectionChanged" />--%>
                                                        <asp:AsyncPostBackTrigger ControlID="txtModalFechaNavSaliente" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtModalFechaNavEntrante" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddlModalRutAportante" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddlModalNombreAportante" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtModalFSolicitud" EventName="TextChanged" />
                                                    </Triggers>
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtModalCuotaEntrante" CssClass="form-control dbs-entero13-decimal0" runat="server" OnTextChanged="CalcularMontoEntrante" AutoPostBack="true"></asp:TextBox>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>

                                            </div>

                                            <div class="col-md-2">
                                                <label class="form-control-label">Factor</label>
                                            </div>

                                            <div class="col-md-4">
                                                <asp:UpdatePanel runat="server" ID="UpdatePanel200" UpdateMode="Conditional">
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="ddlModalNemotecnicoSaliente" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtModalNavSaliente" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtModalNavEntrante" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtModalNavSaliente" EventName="TextChanged" />
                                                        <%--<asp:AsyncPostBackTrigger ControlID="CalendarModalFechaNavSaliente" EventName="SelectionChanged" />--%>
                                                        <asp:AsyncPostBackTrigger ControlID="txtModalFechaNavSaliente" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddlModalNemotecnicoEntrante" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtModalFSolicitud" EventName="TextChanged" />
                                                        <%--<asp:AsyncPostBackTrigger ControlID="CalendarModalFechaNavEntrante" EventName="SelectionChanged" />--%>
                                                        <asp:AsyncPostBackTrigger ControlID="txtModalFechaNavEntrante" EventName="TextChanged" />
                                                    </Triggers>
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtModalFactor" CssClass="form-control dbs-entero20-decimal14" runat="server" ReadOnly="true"></asp:TextBox>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>
                                        <div class="row mt-2">
                                            <div class="col-md-2">
                                                <label class="form-control-label">Nav</label>

                                            </div>
                                            <div class="col-md-4">
                                                <asp:UpdatePanel runat="server" ID="UpdatePanel27" UpdateMode="Conditional">
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="ddlModalNemotecnicoEntrante" EventName="SelectedIndexChanged" />
                                                        <%--<asp:AsyncPostBackTrigger ControlID="CalendarModalFechaNavEntrante" EventName="SelectionChanged" />--%>
                                                        <asp:AsyncPostBackTrigger ControlID="txtModalFechaNavEntrante" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtModalFSolicitud" EventName="TextChanged" />
                                                        <%--<asp:AsyncPostBackTrigger ControlID="CalendarModalFechaNavSaliente" EventName="SelectionChanged" />--%>
                                                        <asp:AsyncPostBackTrigger ControlID="txtModalFechaNavSaliente" EventName="TextChanged" />
                                                    </Triggers>
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtModalNavEntrante" CssClass="form-control dbs-entero20-decimal6" runat="server" onpaste="return false" oncut="return false" oncopy="return false" onchange="validarValorEntrante()" OnTextChanged="calcularFactor" AutoPostBack="true"></asp:TextBox>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                            <div class="col-md-2">
                                                <label class="form-control-label">Nav (CLP)</label>

                                            </div>
                                            <div class="col-md-4">
                                                <asp:UpdatePanel runat="server" ID="UpdatePanel17" UpdateMode="Conditional">
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="txtModalNavEntrante" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtModalCuotaSaliente" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtModalTipoCambio" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddlModalNemotecnicoEntrante" EventName="SelectedIndexChanged" />
                                                        <%--<asp:AsyncPostBackTrigger ControlID="CalendarModalFechaNavEntrante" EventName="SelectionChanged" />--%>
                                                        <asp:AsyncPostBackTrigger ControlID="txtModalFechaNavEntrante" EventName="TextChanged" />
                                                        <%--<asp:AsyncPostBackTrigger ControlID="CalendarModalFechaObservado" EventName="SelectionChanged" />--%>
                                                        <asp:AsyncPostBackTrigger ControlID="txtModalFechaObservado" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtModalFSolicitud" EventName="TextChanged" />
                                                        <%--<asp:AsyncPostBackTrigger ControlID="CalendarModalFechaNavSaliente" EventName="SelectionChanged" />--%>
                                                        <asp:AsyncPostBackTrigger ControlID="txtModalFechaNavSaliente" EventName="TextChanged" />
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
                                                <asp:UpdatePanel runat="server" ID="UpdatePanel26" UpdateMode="Conditional">
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="txtModalCuotaSaliente" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtModalMontoSaliente" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtModalNavEntrante" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtModalNavSaliente" EventName="TextChanged" />
                                                        <%--<asp:AsyncPostBackTrigger ControlID="CalendarModalFechaNavEntrante" EventName="SelectionChanged" />--%>
                                                        <asp:AsyncPostBackTrigger ControlID="txtModalFechaNavEntrante" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddlModalNemotecnicoEntrante" EventName="SelectedIndexChanged" />
                                                        <%--<asp:AsyncPostBackTrigger ControlID="CalendarModalFechaNavSaliente" EventName="SelectionChanged" />--%>
                                                        <asp:AsyncPostBackTrigger ControlID="txtModalFechaNavSaliente" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddlModalRutAportante" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddlModalNombreAportante" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddlModalNemotecnicoSaliente" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtModalFSolicitud" EventName="TextChanged" />
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
                                                <asp:UpdatePanel runat="server" ID="UpdatePanel28" UpdateMode="Conditional">
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="txtModalMontoSaliente" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtModalCuotaSaliente" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtModalNavEntrante" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtModalNavSaliente" EventName="TextChanged" />
                                                        <%--<asp:AsyncPostBackTrigger ControlID="CalendarModalFechaNavEntrante" EventName="SelectionChanged" />--%>
                                                        <asp:AsyncPostBackTrigger ControlID="txtModalFechaNavEntrante" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtModalTipoCambio" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddlModalNemotecnicoEntrante" EventName="SelectedIndexChanged" />
                                                        <%--<asp:AsyncPostBackTrigger ControlID="CalendarModalFechaNavSaliente" EventName="SelectionChanged" />--%>
                                                        <asp:AsyncPostBackTrigger ControlID="txtModalFechaNavSaliente" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddlModalRutAportante" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddlModalNombreAportante" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddlModalNemotecnicoSaliente" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtModalMontoSaliente" EventName="TextChanged" />
                                                        <%--<asp:AsyncPostBackTrigger ControlID="CalendarModalFechaObservado" EventName="SelectionChanged" />--%>
                                                        <asp:AsyncPostBackTrigger ControlID="txtModalFechaObservado" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtModalFSolicitud" EventName="TextChanged" />

                                                    </Triggers>
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtModalMontoCLPEntrante" CssClass="form-control dbs-entero20-decimal2" runat="server"></asp:TextBox>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>
                                        <div class="row mt-2">
                                            <div class="col-md-2">
                                                <label class="form-control-label">Fecha TC Observado</label>
                                            </div>
                                            <div class="col-md-3">
                                                <asp:UpdatePanel runat="server" ID="UpdatePanel2" UpdateMode="Conditional">
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="lnkbtnModalFechaObservado" EventName="click" />
                                                        <%--<asp:AsyncPostBackTrigger ControlID="CalendarModalFechaNavSaliente" EventName="SelectionChanged" />--%>
                                                        <asp:AsyncPostBackTrigger ControlID="txtModalFechaNavSaliente" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddlModalNemotecnicoSaliente" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtModalFSolicitud" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddlModalNemotecnicoEntrante" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddlModalNemotecnicoEntrante" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtModalFechaCanje" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtModalFechaNavEntrante" EventName="TextChanged" />
                                                    </Triggers>
                                                    <ContentTemplate>
                                                        <div class="input-group">
                                                            <asp:TextBox ID="txtModalFechaObservado" runat="server" CssClass="form-control datepicker" AutoPostBack="true" Enabled="false"></asp:TextBox>
                                                            <asp:LinkButton ID="lnkbtnModalFechaObservado" Class="btn btn-moneda" runat="server"
                                                                OnClientClick="return clickCalendar('txtModalFechaObservado')"><i class="far fa-calendar-alt"></i></asp:LinkButton>


                                                            <span id="reqtxtModalFechaObservado" class="reqError"></span>
                                                        </div>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="col-md-12 d-flex p-0 mb-10">

                                <div class="card mt-0 col-md-6">
                                    <div class="card-body">
                                        <h5 class="card-title">CUSTODIA DISPONIBLE</h5>
                                        <hr />
                                        <div class="row mt-2">
                                            <div class="col-md-2">
                                                <label class="form-control-label">Fecha Actual</label>
                                            </div>
                                            <div class="col-md-4">
                                                <asp:UpdatePanel runat="server" ID="UpdatePanel19" UpdateMode="Conditional">
                                                    <Triggers>

                                                        <asp:AsyncPostBackTrigger ControlID="ddlModalNemotecnicoSaliente" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddlModalRutAportante" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddlModalNombreAportante" EventName="SelectedIndexChanged" />
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
                                                <asp:UpdatePanel runat="server" ID="UpdatePanel18" UpdateMode="Conditional">
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="ddlModalNemotecnicoSaliente" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddlModalRutAportante" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddlModalNombreAportante" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtModalFSolicitud" EventName="TextChanged" />
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
                                                <asp:UpdatePanel runat="server" ID="UpdatePanel35" UpdateMode="Conditional">
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="ddlModalNemotecnicoSaliente" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddlModalRutAportante" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddlModalNombreAportante" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtModalFSolicitud" EventName="TextChanged" />
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
                                                <asp:UpdatePanel runat="server" ID="UpdatePanel21" UpdateMode="Conditional">
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="ddlModalNemotecnicoSaliente" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddlModalRutAportante" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtModalFSolicitud" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddlModalNombreAportante" EventName="SelectedIndexChanged" />
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
                                                <asp:UpdatePanel runat="server" ID="UpdatePanel34" UpdateMode="Conditional">
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="ddlModalNemotecnicoSaliente" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddlModalRutAportante" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddlModalNombreAportante" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtModalFSolicitud" EventName="TextChanged" />
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
                                                <asp:UpdatePanel runat="server" ID="UpdatePanel20" UpdateMode="Conditional">
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="ddlModalNemotecnicoSaliente" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddlModalRutAportante" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddlModalNombreAportante" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtModalFSolicitud" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtModalCuotaSaliente" EventName="TextChanged" />

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
                                                <asp:UpdatePanel runat="server" ID="UpdatePanel23" UpdateMode="Conditional">
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="ddlModalNemotecnicoSaliente" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtModalFechaNavSaliente" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtModalFechaObservado" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtModalFSolicitud" EventName="TextChanged" />
                                                    </Triggers>
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtModalTipoCambio" CssClass="form-control dbs-entero20-decimal6" runat="server" onpaste="return false" oncut="return false" oncopy="return false" OnTextChanged="tipoCambioChanged" AutoPostBack="true"></asp:TextBox>
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
                                                    <asp:ListItem Value="NOOK">NO OK</asp:ListItem>
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
                                                    <asp:ListItem Value="NOOK">NO OK</asp:ListItem>
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
                                        <div class="row mt-2" style="display: none">
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
                            <div class="form-group mt-5 text-center">
                                <div class="col-md-offset-1">
                                    <asp:Button ID="btnPrueba" Text="Mostrar PopUp" CssClass="btn btn-info" runat="server" Visible="false"></asp:Button>
                                    <asp:Button ID="btnModalGuardar" Text="Guardar" CssClass="btn btn-info" runat="server" OnClientClick="return validateBtn();"></asp:Button>
                                    <asp:Button ID="btnModalModificar" Text="Modificar" CssClass="btn btn-info" runat="server" OnClientClick="return validateBtn();"></asp:Button>
                                    <asp:Button ID="btnModalCancelar" Text="Cancelar" CssClass="btn btn-secondary" runat="server" OnClientClick="if (!confirm('¿Seguro que desea Cancelar?')) return false;"></asp:Button>
                                    <asp:Button ID="btnModalEliminar" Text="Eliminar" runat="server" class="btn btn-danger" OnClientClick="if (!confirm('¿Seguro que desea Eliminar los elementos seleccionados?')) return false;"></asp:Button>
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

    <!--PopUp Canjes-->
    <div class="modal fade" id="PopUpCanjes" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" data-keyboard="false" data-backdrop="static">
        <div class="modal-dialog" style="max-width: 60%;">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title">
                        <asp:Label ID="Label8" runat="server" Text="CONFIRMACION SOLICITUD DE CANJE"
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
                                    <label class="form-control-label">Fecha Solicitud</label>
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
                                <!-- Moneda del Fondo-->
                                <div class="col-md-4">
                                    <label class="form-control-label">Moneda del Fondo</label>
                                </div>
                                <div class="col-md-8">
                                    <asp:Label ID="lblPopUpMonedaDelFondo" runat="server" Text="" Font-Bold="true" Font-Size="X-Large"> </asp:Label>
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
                                    <label class="form-control-label">Rut Administradora</label>
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
                                <!-- Fecha de Canje -->
                                <div class="col-md-4">
                                    <label class="form-control-label">Fecha de Canje</label>
                                </div>
                                <div class="col-md-8">
                                    <asp:Label ID="lblPopUpFechaDeCanje" runat="server" Text="" Font-Bold="true" Font-Size="X-Large"> </asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <!-- Fecha de NAV-->
                                <div class="col-md-4">
                                    <label class="form-control-label">Fecha de NAV</label>
                                </div>
                                <div class="col-md-8">
                                    <asp:Label ID="lblPopUpFechaDeNav" runat="server" Text="" Font-Bold="true" Font-Size="X-Large"> </asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <!-- Serie Saliente-->
                                <div class="col-md-4">
                                    <label class="form-control-label">Serie Saliente</label>
                                </div>
                                <div class="col-md-8">
                                    <asp:Label ID="lblPopUpSerieSaliente" runat="server" Text="" Font-Bold="true" Font-Size="X-Large"> </asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <!-- Cuotas Salientes-->
                                <div class="col-md-4">
                                    <label class="form-control-label">Cuotas Salientes</label>
                                </div>
                                <div class="col-md-8">
                                    <asp:Label ID="lblPopUpCuotasSalientes" runat="server" Text="" Font-Bold="true" Font-Size="X-Large"> </asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <!-- NAV Saliente-->
                                <div class="col-md-4">
                                    <label class="form-control-label">NAV Saliente</label>
                                </div>
                                <div class="col-md-8">
                                    <asp:Label ID="lblPopUpNavSaliente" runat="server" Text="" Font-Bold="true" Font-Size="X-Large"> </asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <!-- Monto Saliente -->
                                <div class="col-md-4">
                                    <label class="form-control-label">Monto Saliente</label>
                                </div>
                                <div class="col-md-8">
                                    <asp:Label ID="lblPopUpMontoSaliente" runat="server" Text="" Font-Bold="true" Font-Size="X-Large"> </asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <!-- Serie Entrate-->
                                <div class="col-md-4">
                                    <label class="form-control-label">Serie Entrate</label>
                                </div>
                                <div class="col-md-8">
                                    <asp:Label ID="lblPopUpSerieEntrante" runat="server" Text="" Font-Bold="true" Font-Size="X-Large"> </asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <!-- Cuotas Entrantes-->
                                <div class="col-md-4">
                                    <label class="form-control-label">Cuotas Entrantes</label>
                                </div>
                                <div class="col-md-8">
                                    <asp:Label ID="lblPopUpCuotasEntrantes" runat="server" Text="" Font-Bold="true" Font-Size="X-Large"> </asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <!-- NAV Entrante -->
                                <div class="col-md-4">
                                    <label class="form-control-label">NAV Entrante</label>
                                </div>
                                <div class="col-md-8">
                                    <asp:Label ID="lblPopUpNavEntrante" runat="server" Text="" Font-Bold="true" Font-Size="X-Large"> </asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <!-- Monto Entrante-->
                                <div class="col-md-4">
                                    <label class="form-control-label">Monto Entrante</label>
                                </div>
                                <div class="col-md-8">
                                    <asp:Label ID="lblPopUpMontoEntrante" runat="server" Text="" Font-Bold="true" Font-Size="X-Large"> </asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <!-- Factor-->
                                <div class="col-md-4">
                                    <label class="form-control-label">Factor</label>
                                </div>
                                <div class="col-md-8">
                                    <asp:Label ID="lblPopUpFactor" runat="server" Text="" Font-Bold="true" Font-Size="X-Large"> </asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <!-- Remanente a devolver-->
                                <div class="col-md-4">
                                    <label class="form-control-label">Remanente a devolver</label>
                                </div>
                                <div class="col-md-8">
                                    <asp:Label ID="lblPopUpRemanenteADevolver" runat="server" Text="" Font-Bold="true" Font-Size="X-Large"> </asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <!-- Contrato Gral de Fondos-->
                                <div class="col-md-4">
                                    <label class="form-control-label">Contrato Gral de Fondos</label>
                                </div>
                                <div class="col-md-8">
                                    <asp:Label ID="lblPopUpContratoGralDeFondos" runat="server" Text="" Font-Bold="true" Font-Size="X-Large"> </asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <!-- Poderes-->
                                <div class="col-md-4">
                                    <label class="form-control-label">Poderes</label>
                                </div>
                                <div class="col-md-8">
                                    <asp:Label ID="lblPopUpPoderes" runat="server" Text="" Font-Bold="true" Font-Size="X-Large"> </asp:Label>
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

        function soloNumeros(e) {
            key = e.keyCode || e.which;
            tecla = String.fromCharCode(key).toLowerCase();
            letras = "0123456789";
            especiales = "8-37-39-46";
            tecla_especial = false
            for (var i in especiales) {
                if (key == especiales[i]) {
                    tecla_especial = true;
                    reak;
                }
            }
            if (letras.indexOf(tecla) == -1 && !tecla_especial) {
                return false;
            }
            else {
                return true;
            }
        }

        function soloNumerosNP(e) {
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

        function validarValorSaliente() {
            var valor = document.getElementById('<%=txtModalNavSaliente.ClientID%>').value;
            if (valor.match(/^\d{1,20}(\,\d{1,6})?$/)) {
                return true;
            } else {
                document.getElementById("<%=txtModalNavSaliente.ClientID%>").value = ""
                alert('La cadena no tiene formato correcto (20 enteros , 6 decimales)');
                return false;
            }
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
        $(document).ready(function () {
            var txtAccionHidden = $('#<%=txtAccionHidden.ClientID %>').val();

            confNumeros();

            if ((txtAccionHidden == "MODIFICAR") || (txtAccionHidden == "CREAR") || (txtAccionHidden == "ELIMINAR") || (txtAccionHidden == "MANTENER_MODAL")) {
                $('#myModal').modal('show');
            } else if (txtAccionHidden == "MOSTRAR_DIALOGO") {
                $('#myModalmg').modal();
            } else if (txtAccionHidden == "POPUPCANJES") {
                $('#PopUpCanjes').modal('show');
            } else {
                checkRadioBtn("");
            }

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

            $('.dbs-entero20-decimal2').mask2(getMask(20, 2));
            $('.dbs-entero20-decimal4').mask2(getMask(20, 4));
            $('.dbs-entero20-decimal6').mask2(getMask(20, 6));
            $('.dbs-entero17-decimal9').mask2(getMask(17, 9));
            $('.dbs-entero18-decimal0').mask2(getMask(18, 0));
            $('.dbs-entero13-decimal0').mask2(getMask(13, 0));
            $('.dbs-entero10-decimal0').mask2(getMask(10, 0));
            $('.dbs-entero20-decimal14').mask2(getMask(20, 14));

            $("[id*=txtFechaSolicitudDesde]").datepicker();
            $("[id*=txtFechaSolicitudHasta]").datepicker();

            $("[id*=txtFechaNavHasta]").datepicker();
            $("[id*=txtFechaNavDesde]").datepicker();

            $("[id*=txtFechaCanjeDesde]").datepicker();
            $("[id*=txtFechaCanjeHasta]").datepicker();

            $("[id*=txtModalFSolicitud]").datepicker();
            $("[id*=txtModalFSolicitud]").datepicker({
                container: '#myModal modal-body'
                , showOn: "none"
            });

            $('#<%=txtModalFechaNavSaliente.ClientID%>').datepicker();
            $('#<%=txtModalFechaCanje.ClientID%>').datepicker();
            $('#<%=txtModalFechaNavEntrante.ClientID%>').datepicker();
            $('#<%=txtModalFechaObservado.ClientID%>').datepicker();


            $("[id*=txtFechaSolicitudDesde]").change(function () {
                changeFechas($("[id*=txtFechaSolicitudDesde]"), $("[id*=txtFechaSolicitudHasta]"), 1)
            });
            $("[id*=txtFechaSolicitudHasta]").change(function () {
                changeFechas($("[id*=txtFechaSolicitudDesde]"), $("[id*=txtFechaSolicitudHasta]"), 2)
            });

            $("[id*=txtFechaNavHasta]").change(function () {
                changeFechas($("[id*=txtFechaNavHasta]"), $("[id*=txtFechaNavDesde]"), 1)
            });
            $("[id*=txtFechaNavDesde]").change(function () {
                changeFechas($("[id*=txtFechaNavHasta]"), $("[id*=txtFechaNavDesde]"), 2)
            });

            $("[id*=txtFechaCanjeDesde]").change(function () {
                changeFechas($("[id*=txtFechaCanjeDesde]"), $("[id*=txtFechaCanjeHasta]"), 1)
            });
            $("[id*=txtFechaCanjeHasta]").change(function () {
                changeFechas($("[id*=txtFechaCanjeDesde]"), $("[id*=txtFechaCanjeHasta]"), 2)
            });
        }

        function cerrarAlert() {
            $('#<%=txtAccionHidden.ClientID %>').val("");
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
        function msgAlert(mensaje) {
            $('#pMensajeAlert').text(mensaje);
            $('#h5dialogTitle').html("Error");
            $('#modalAlert').modal();
        }
        function validateBtn() {
            if (!confirm('¿Seguro que desea Guardar?')) {
                return false;
            } else {
                var nemo = document.getElementById('<%=ddlListaRutAportante.ClientID%>');
                if (validateRut(nemo)) {
                    return CheckTxtEmpty()
                } else {
                    return false;
                }
            }
        }
        function CheckTxtEmpty() {
            if ($('#<%=ddlModalRutAportante.ClientID %>').val() == "") {
                alert('Debe seleccionar un Rut del Aportante')
                return false;
            }
            if ($('#<%=ddlModalNombreAportante.ClientID %>').val() == "") {
                alert('Debe seleccionar un Nombre Aportante')
                return false;
            }
            if ($('#<%=ddlModalFondo.ClientID%>').val() == "") {
                alert('Debe seleccionar un Rut de Fondo')
                return false;
            }
            if ($('#<%=ddlModalNombreFondo.ClientID%>').val() == "") {
                alert('Debe seleccionar un Nombre de Fondo')
                return false;
            }
            if ($('#<%=ddlModalNemotecnicoEntrante.ClientID%>').val() == "Seleccione una opción") {
                alert('Debe seleccionar un Nemotécnico Entrante')
                return false;
            }
            if ($('#<%=txtModalFechaNavEntrante.ClientID%>').val() == "") {
                alert('Debe seleccionar una Fecha Nav Entrante válida')
                return false;
            }
            if ($('#<%=txtModalCuotaSaliente.ClientID%>').val() == "") {
                alert('Debe escribir las cuotas salientes')
                return false;
            }
            if ($('#<%=txtModalNavEntrante.ClientID%>').val() == "") {
                alert('Debe escribir un Nav Entrante')
                return false;
            }
            if ($('#<%=txtModalMontoEntrante.ClientID%>').val() == "") {
                alert('Debe escribir un Monto Entrante')
                return false;
            }
            if ($('#<%=ddlModalNemotecnicoSaliente.ClientID%>').val() == "Seleccione una opción") {
                alert('Debe seleccionar un Nemotécnico Saliente')
                return false;
            }
            if ($('#<%=txtModalFechaNavSaliente.ClientID%>').val() == "") {
                alert('Debe seleccionar una Fecha Nav válida')
                return false;
            }
            if ($('#<%=txtModalCuotaEntrante.ClientID%>').val() == "") {
                alert('Debe escribir las cuotas entrantes')
                return false;
            }
            if ($('#<%=txtModalNavSaliente.ClientID%>').val() == "") {
                alert('Debe escribir un Nav Saliente')
                return false;
            }
            if ($('#<%=txtModalFSolicitud.ClientID%>').val() == "") {
                alert('Debe ingresar una fecha de solicitud')
                return false;
            }
            if ($('#<%=txtModalFechaObservado.ClientID%>').val() == "") {
                alert('Debe seleccionar una Fecha Observado válida')
                return false;
            }
            if ($('#<%=txtModalTipoCambio.ClientID%>').val() == "") {
                alert('Debe escribir un valor tc')
                return false;
            }
            return true;
        }
        function alertaMenor() {
            var cuotas = document.getElementById('<%=txtModalCuotaSaliente.ClientID%>').value;
            var disponibles = document.getElementById('<%=txtModalCuotasDisponibles.ClientID%>').value;
            if (parseInt(cuotas) > parseInt(disponibles)) {
                alert('Las cuotas salientes no pueden ser mayor a las cuotas disponibles');
                document.getElementById('<%=txtModalCuotaSaliente.ClientID%>').value = "";
                return false;
            }
        }

        function enableDisableButtons(newValue) {
            var btnModificar = document.getElementById('<%=BtnModificar.ClientID%>');
            btnModificar.disabled = newValue;
            var btnEliminar = document.getElementById('<%=BtnEliminar.ClientID%>');
            btnEliminar.disabled = newValue;
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
