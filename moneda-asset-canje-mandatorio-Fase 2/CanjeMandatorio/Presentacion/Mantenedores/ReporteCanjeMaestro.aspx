<%@ Page Title="" ValidateRequest="false" Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false" CodeFile="ReporteCanjeMaestro.aspx.vb" Inherits="Presentacion_Mantenedores_ReporteCanjeMaestro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <h2 class="TdRedondeado titleMant">Reporte <strong>Canje Mandatorio</strong></h2>

    <asp:scriptmanager id="ScriptManager1" runat="server"></asp:scriptmanager>

    <div class="card p-4 jumbotron">
        <!-- HEader de la pantalla -->
        <div class="row">
            <!-- RUT Usuario-->
            <div class="col-md-5">
                <asp:label runat="server" id="Label2">Fondo</asp:label>
                <asp:listbox id="ddlFondo" selectionmode="Multiple" cssclass="form-control" runat="server" />
            </div>

            <div class="col-md-1">
                <asp:label runat="server" id="Label1">Cambio</asp:label>
                <asp:textbox id="txtCambio" runat="server" cssclass="form-control datepicker" onkeypress="return soloNumerosyComa(event)" onpaste="return false" oncut="return false" oncopy="return false"></asp:textbox>
            </div>
        
            <!-- FECHA PROCESO -->
            <div class="col-md-3">
                <asp:label runat="server" id="Label3">Fecha Proceso</asp:label>
                <div class="input-group">
                    <asp:textbox id="txtFechaProceso" runat="server" cssclass="form-control datepicker" readonly="True"></asp:textbox>
                    <asp:linkbutton id="lnkMostrarCalendario" class="btn btn-moneda" runat="server" onclientclick="return clickCalendar('txtFechaProceso')"><i class="far fa-calendar-alt"></i></asp:linkbutton>
                    <asp:linkbutton id="BtnLimpiarFechaDesde" text="" class="btn btn-secondary" runat="server" onclientclick="return limpiarCalendar('txtFechaProceso')"><i class="far fa-trash-alt"></i></asp:linkbutton>

                </div>
            </div>

            <!-- FECHA CANJE -->
            <div class="col-md-3">
                <asp:label runat="server" id="Label4">Fecha Canje</asp:label>
                <div class="input-group">
                    <asp:textbox id="txtFechaCanje" runat="server" cssclass="form-control datepicker" readonly="True"></asp:textbox>
                    <asp:linkbutton id="LinkButton2" class="btn btn-moneda" runat="server" onclientclick="return clickCalendar('txtFechaCanje')"><i class="far fa-calendar-alt"></i></asp:linkbutton>
                    <asp:linkbutton id="LinkButton3" text="" class="btn btn-secondary" runat="server" onclientclick="return limpiarCalendar('txtFechaCanje')"><i class="far fa-trash-alt"></i></asp:linkbutton>

                </div>
            </div>
        </div>

        <!-- BOTONES BUSCAR LIMPIAR Y CREAR -->
        <div class="row text-center mt-5 p-3 border-bottom">
            <div class="col-md-12">
                <!-- BOTÓN BUSCAR -->
                <asp:button id="BtnBuscar" text="Buscar" class="btn btn-moneda" runat="server" />
                <asp:button id="btnLimpiarFrm" text="Borrar" class="btn btn-secondary" runat="server" onclick="btnLimpiarFrm_Click" />

                <!-- BOTÓN CREAR -->
                <asp:button id="BtnGuardar" text="Guardar" class="btn btn-info" runat="server" Visible="false" />
            </div>
        </div>


        <!-- TABLA DE RESULTADOS -->
        <h5 class="mt-3 text-secondary"><i class="fas fa-file-invoice fa-sm"></i>Resultado de la búsqueda</h5>
        <nav>
            <div class="nav nav-tabs" id="nav-tab" role="tablist">
                <a class="nav-item nav-link active" id="nav-home-tab" data-toggle="tab" href="#nav-home" role="tab" aria-controls="nav-home" aria-selected="true">Fecha Corte</a>
                <a class="nav-item nav-link" id="nav-profile-tab" data-toggle="tab" href="#nav-profile" role="tab" aria-controls="nav-profile" aria-selected="false">Fecha Canje</a>
                <a class="nav-item nav-link" id="nav-contact-tab" data-toggle="tab" href="#nav-contact" role="tab" aria-controls="nav-contact" aria-selected="false">Canje</a>
            </div>
        </nav>

        <!-- TABLA FECHA CORTE-->
        <div class="tab-content" id="nav-tabContent">
            <div class="tab-pane show active" id="nav-home" role="tabpanel" aria-labelledby="nav-home-tab">
                <div class="card border-top-0 p-3">
                    <asp:gridview
                        id="GrvTabla"
                        runat="server"
                        cssclass="table table-bordered table-hover table-sm gvv wrap-responsive table-striped"
                        headerstyle-backcolor=""
                        headerstyle-font-size=""
                        rowstyle-font-size="Small"
                        autogeneratecolumns="False" 
                        enablemodelvalidation="True" 
                        backcolor="" 
                        bordercolor="" 
                        borderwidth="1px" 
                        cellpadding="2" 
                        forecolor="Black" 
                        gridlines="None"
                         HeaderStyle-Wrap="true">

                        <AlternatingRowStyle BackColor="" />
                        <Columns>
                            <asp:BoundField DataField="FN_RUT" HeaderText="Rut Fondo" ItemStyle-Wrap="false"   ItemStyle-HorizontalAlign="Right"/>
                            <asp:BoundField DataField="FN_Razon_Social" HeaderText="FONDO" ItemStyle-Wrap="false" />
                            <asp:BoundField DataField="PR_Directo_Indirecto" HeaderText="D/I" ItemStyle-Wrap="false" />
                            <asp:BoundField DataField="FS_Grupo" HeaderText="Grupo"  ItemStyle-Wrap="false" />
                            <asp:BoundField DataField="GPA_Descripcion" HeaderText="G.Aportantes"  ItemStyle-Wrap="false" />
                            <asp:BoundField DataField="AP_RUT" HeaderText="RUT Aportante"  ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Right"/>
                            <asp:BoundField DataField="AP_Razon_Social" HeaderText="Nombre o Razón Social"  ItemStyle-Wrap="false" />
                            <asp:BoundField DataField="FS_Nemotecnico" HeaderText="Nemotecnico" ItemStyle-Wrap="false"/>
                            <asp:BoundField DataField="FS_Moneda" HeaderText="Moneda"  ItemStyle-Wrap="false" />
                            <asp:BoundField DataField="VCS_Valor" HeaderText="Valor Cuota"  ItemStyle-Wrap="false" DataFormatString="{0:N4}"  ItemStyle-HorizontalAlign="Right" />
                            <asp:BoundField DataField="ADCV_Cantidad" HeaderText="Cantidad" ItemStyle-Wrap="false" DataFormatString="{0:N0}" ItemStyle-HorizontalAlign="Right" />
                            <asp:BoundField DataField="RES_Cuotas" HeaderText="Rescates" ItemStyle-Wrap="false" DataFormatString="{0:N0}" ItemStyle-HorizontalAlign="Right" />
                            <asp:BoundField DataField="Susc_Cuotas" HeaderText="Suscripciones" ItemStyle-Wrap="false" DataFormatString="{0:N4}" ItemStyle-HorizontalAlign="Right" />
                            <asp:BoundField DataField="CANJE" HeaderText="Canjes" DataFormatString="{0:N0}" ItemStyle-HorizontalAlign="Right"/>
                            <asp:BoundField DataField="PR_Saldo_Cuotas" HeaderText="Total Cuotas" DataFormatString="{0:N0}" ItemStyle-HorizontalAlign="Right" />
                            <asp:BoundField DataField="PR_Monto" HeaderText="Monto Total" DataFormatString="{0:N4}" ItemStyle-HorizontalAlign="Right" />
                            <asp:BoundField DataField="PR_DescEstado" HeaderText="Estado" />
                            <asp:BoundField DataField="SerieOptima" HeaderText="Serie Optima" />
                            <asp:BoundField DataField="C_AP_Nac_Ext" HeaderText="Extranjero" />
                            <asp:BoundField DataField="C_AP_Calificado" HeaderText="Calificado" />
                            <asp:BoundField DataField="C_AP_Rel_MAM" HeaderText="MAM" />
                            <asp:BoundField DataField="ContratoDistribucion" HeaderText="Contrato Distribución" />
                            <asp:BoundField DataField="C_AP_Limite" HeaderText="Monto" />
                            <asp:BoundField DataField="C_Certificado" HeaderText="Certificado" />
                            <asp:BoundField DataField="C_AP_Final_I" HeaderText="Estado Certificado" />
                            <asp:BoundField DataField="C_Cuotas_C" HeaderText="Cuotas Certificadas" DataFormatString="{0:N0}" ItemStyle-HorizontalAlign="Right" />
                            <asp:BoundField DataField="C_Cuotas_Certificar" HeaderText="Cuotas x Certificar" DataFormatString="{0:N0}" ItemStyle-HorizontalAlign="Right" />
                        </Columns>

                        <FooterStyle BackColor="Tan" />

                        <HeaderStyle BackColor="" Font-Size="" Font-Bold="True"></HeaderStyle>

                        <PagerStyle BackColor="#eeeeee" ForeColor="DarkSlateBlue" HorizontalAlign="Center" />

                        <RowStyle Font-Size="Small"></RowStyle>
                        <SelectedRowStyle BackColor="DarkSlateBlue" ForeColor="GhostWhite" />
                    </asp:gridview>
                </div>
            </div>

            <!-- TABLA FECHA CANJE-->
            <div class="tab-pane" id="nav-profile" role="tabpanel" aria-labelledby="nav-profile-tab">
                <div class="card border-top-0 p-3">
                    <asp:gridview
                        id="GrvFechaCanje"
                        runat="server"
                        cssclass="table table-bordered table-hover table-sm gvv wrap-responsive table-striped"
                        headerstyle-backcolor=""
                        headerstyle-font-size=""
                        rowstyle-font-size="Small"
                        autogeneratecolumns="False" enablemodelvalidation="True" cellpadding="4" forecolor="" gridlines="None"
                        HeaderStyle-Wrap="true">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:BoundField DataField="pca_FN_RUT" HeaderText="Rut Fondo" ItemStyle-Wrap="false"  ItemStyle-HorizontalAlign="Right" />
                            <asp:BoundField DataField="pca_FN_Razon_Social" HeaderText="FONDO" ItemStyle-Wrap="false" />
                            <asp:BoundField DataField="pca_PR_Directo_Indirecto" HeaderText="D/I" ItemStyle-Wrap="false" />
                            <asp:BoundField DataField="pca_FS_Grupo" HeaderText="Grupo" ItemStyle-Wrap="false" />
                            <asp:BoundField DataField="pca_GPA_Descripcion" HeaderText="G.Aportantes" ItemStyle-Wrap="false"  />
                            <asp:BoundField DataField="pca_AP_RUT" HeaderText="RUT Aportante" ItemStyle-Wrap="false"  ItemStyle-HorizontalAlign="Right" />
                            <asp:BoundField DataField="pca_AP_Razon_Social" HeaderText="Nombre o Razón Social" ItemStyle-Wrap="false"  />
                            <asp:BoundField DataField="pca_FS_Nemotecnico" HeaderText="Nemotecnico" ItemStyle-Wrap="false"  />
                            <asp:BoundField DataField="pca_FS_Moneda" HeaderText="Moneda" ItemStyle-Wrap="false" />
                            <asp:BoundField DataField="pca_VCS_Valor" HeaderText="Valor Cuota" ItemStyle-Wrap="false" DataFormatString="{0:N4}" ItemStyle-HorizontalAlign="Right" />
                            <asp:BoundField DataField="pca_ADCV_Cantidad" HeaderText="Cantidad" ItemStyle-Wrap="false" DataFormatString="{0:N0}" ItemStyle-HorizontalAlign="Right" />
                            <asp:BoundField DataField="pca_RES_Cuotas" HeaderText="Rescates" ItemStyle-Wrap="false" DataFormatString="{0:N0}" ItemStyle-HorizontalAlign="Right" />
                            <asp:BoundField DataField="pca_Susc_Cuotas" HeaderText="Suscripciones" ItemStyle-Wrap="false" DataFormatString="{0:N4}" ItemStyle-HorizontalAlign="Right" />
                            <asp:BoundField DataField="pca_CANJE" HeaderText="Canjes" ItemStyle-Wrap="false" DataFormatString="{0:N0}" ItemStyle-HorizontalAlign="Right" />
                            <asp:BoundField DataField="pca_PR_Saldo_Cuotas" HeaderText="Total Cuotas" ItemStyle-Wrap="false" DataFormatString="{0:N0}" ItemStyle-HorizontalAlign="Right" />
                            <asp:BoundField DataField="pca_PR_Monto" HeaderText="Monto Total" ItemStyle-Wrap="false" DataFormatString="{0:N4}" ItemStyle-HorizontalAlign="Right" />
                            <asp:BoundField DataField="pca_PR_DescEstado" HeaderText="Estado" ItemStyle-Wrap="false" />
                            <asp:BoundField DataField="pca_SerieOptima" HeaderText="Serie Optima" ItemStyle-Wrap="false" />
                            <asp:BoundField DataField="pca_C_AP_Nac_Ext" HeaderText="Extranjero" ItemStyle-Wrap="false"  />
                            <asp:BoundField DataField="pca_C_AP_Calificado" HeaderText="Calificado" ItemStyle-Wrap="false" />
                            <asp:BoundField DataField="pca_C_AP_Rel_MAM" HeaderText="MAM" ItemStyle-Wrap="false"  />
                            <asp:BoundField DataField="pca_C_AP_Limite" HeaderText="Monto" ItemStyle-Wrap="false"  />
                            <asp:BoundField DataField="pca_C_Certificado" HeaderText="Certificado" ItemStyle-Wrap="false" />
                            <asp:BoundField DataField="pca_C_AP_Final_I" HeaderText="Estado Certificado" ItemStyle-Wrap="false"  />
                            <asp:BoundField DataField="pca_C_Cuotas_C" HeaderText="Cuotas Certificadas" ItemStyle-Wrap="true" DataFormatString="{0:N0}" ItemStyle-HorizontalAlign="Right" />
                            <asp:BoundField DataField="pca_C_Cuotas_Certificar" HeaderText="Cuotas x Certificar" ItemStyle-Wrap="true" DataFormatString="{0:N0}" ItemStyle-HorizontalAlign="Right" />

                        </Columns>

                        <EditRowStyle BackColor="#7C6F57" />
                        <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />

                        <HeaderStyle BackColor="" Font-Size="" Font-Bold="True" ForeColor="White"></HeaderStyle>

                        <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />

                        <RowStyle Font-Size="Small" BackColor=""></RowStyle>
                        <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                    </asp:gridview>
                </div>
            </div>

            <!--CANJE-->
            <div class="tab-pane" id="nav-contact" role="tabpanel" aria-labelledby="nav-contact-tab">
                <div class="card border-top-0 p-3">
                    <asp:gridview
                        id="GrvCanje" 
                        clientidmode="Static"
                        runat="server"
                        cssclass="table table-bordered table-hover table-sm wrap-responsive table-striped gvv"
                        headerstyle-backcolor=""
                        headerstyle-font-size=""
                        rowstyle-font-size="Small"
                        autogeneratecolumns="False"
                        enablemodelvalidation="True" 
                        backcolor="White" 
                        bordercolor="#999999" 
                        borderstyle="None" 
                        borderwidth="1px" 
                        cellpadding="3" 
                        gridlines="Vertical" HeaderStyle-Wrap="true"
                        >

                        <AlternatingRowStyle BackColor="" />
                        <Columns>
                            <asp:TemplateField HeaderText="" ItemStyle-Wrap="false"><%-- 0 --%>
                                <ItemTemplate>
                                    <asp:Button runat="server" ID="btnDistribuir" Text="Distribuir" AutoPostBack="true" OnClick="btnDistribuir_Click" />
                                    <asp:Button runat="server" ID="btnMantener" Text="Mantener" AutoPostBack="true" OnClick="btnMantener_Click" visible="true"/>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:BoundField DataField="FN_RUT" HeaderText="Rut Fondo" ItemStyle-Wrap="false"  ItemStyle-HorizontalAlign="Right" /> <%-- 1 --%>
                            <asp:TemplateField HeaderText="Fondo" ItemStyle-Wrap="false" >                       <%-- 2 --%>
                                <ItemTemplate>
                                    <asp:Label ID="FN_Razon_Social" runat="server" Text='<%# Eval("FN_Razon_Social")%>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="FN_Razon_Social" HeaderText="Fondo" ItemStyle-Wrap="false"  /> <%-- 3 --%>
                            <asp:BoundField DataField="PR_Directo_Indirecto" HeaderText="D/I" ItemStyle-Wrap="false" />   <%-- 4 --%>
                            <asp:BoundField DataField="FS_Grupo" HeaderText="Grupo" ItemStyle-Wrap="false"  />      <%-- 5 --%>
                            <asp:BoundField DataField="GPA_Descripcion" HeaderText="G.Aportantes" ItemStyle-Wrap="false" />  <%-- 6 --%>
                            <asp:BoundField DataField="AP_RUT" HeaderText="RUT Aportante" ItemStyle-Wrap="false"  ItemStyle-HorizontalAlign="Right"  /> <%-- 7 --%>
                            <asp:BoundField DataField="AP_Razon_Social" HeaderText="Nombre o Razón Social" ItemStyle-Wrap="false"  /> <%-- 8 --%>
                            
                            <asp:BoundField DataField="FS_Nemotecnico" HeaderText="Nemotecnico" ItemStyle-Wrap="false"  /> <%-- 9 --%>
                            <asp:BoundField DataField="FS_Moneda" HeaderText="Moneda" ItemStyle-Wrap="false" /> <%-- 10 --%>

                            <asp:BoundField DataField="VCS_Valor" HeaderText="Valor Cuota<br/>Corte" HTMLEncode="false" ItemStyle-Wrap="false" ItemStyle-Width="100" DataFormatString="{0:N4}" ItemStyle-HorizontalAlign="Right" /><%-- 11 --%>
                            <asp:BoundField DataField="PR_Saldo_Cuotas" HeaderText="Total Cuotas<br/>Corte" HTMLEncode="false" ItemStyle-Wrap="false" ItemStyle-Width="100" DataFormatString="{0:N0}" ItemStyle-HorizontalAlign="Right"/><%-- 12 --%>
                            <asp:BoundField DataField="PR_Monto" HeaderText="Monto Total<br/>Corte" HTMLEncode="false" ItemStyle-Wrap="false" ItemStyle-Width="100" DataFormatString="{0:N2} " ItemStyle-HorizontalAlign="Right" /><%-- 13 --%>
                            <asp:BoundField DataField="PR_DescEstado" HeaderText="Estado Corte<br/>" HTMLEncode="false" ItemStyle-Wrap="false"  ItemStyle-Width="100"/><%-- 14 --%>
                            <asp:BoundField DataField="pca_VCS_Valor" HeaderText="Valor Cuota<br/>Canje" HTMLEncode="false" ItemStyle-Wrap="false"  ItemStyle-Width="100" DataFormatString="{0:N4}" ItemStyle-HorizontalAlign="Right"/><%-- 15 --%>

                            <asp:BoundField DataField="pca_TC_Valor" HeaderText="Valor Tipo<br/>Cambio"  HTMLEncode="false"  ItemStyle-Wrap="false"  ItemStyle-Width="100" DataFormatString="{0:N4}" ItemStyle-HorizontalAlign="Right"/><%-- 16 --%>
                            
                            <asp:BoundField DataField="pca_PR_Saldo_Cuotas" HeaderText="Total Cuotas<br/>Canje"  HTMLEncode="false" ItemStyle-Wrap="false"  ItemStyle-Width="100"  DataFormatString="{0:N0}" ItemStyle-HorizontalAlign="Right"/><%-- 17--%>
                            <asp:BoundField DataField="pca_PR_Monto" HeaderText="Monto Total<br/>Canje" HTMLEncode="false" ItemStyle-Wrap="false"  ItemStyle-Width="100" DataFormatString="{0:N4}" ItemStyle-HorizontalAlign="Right"/><%-- 18 --%>
                            <asp:BoundField DataField="pca_PR_DescEstado" HeaderText="Estado Canje<br/>" HTMLEncode="false" ItemStyle-Wrap="false" ItemStyle-Width="100" /><%-- 19 --%>
                            <%--<asp:TemplateField HeaderText="Acción" ItemStyle-Wrap="false" >
                                <ItemTemplate>
                                    <asp:DropDownList ID="RowSelectorCambio" runat="server" AutoPostBack="true"
                                        OnSelectedIndexChanged="RowSelectorCambio_SelectedIndexChanged">

                                        <asp:ListItem Value="0">Seleccione una opción</asp:ListItem>
                                        <asp:ListItem Value="1">Mantener</asp:ListItem>
                                        <asp:ListItem Value="2">Canjear Hacia Arriba</asp:ListItem>
                                        <asp:ListItem Value="3">Canjear hacia abajo</asp:ListItem>
                                    </asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                            <asp:TemplateField HeaderText="Acción" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Right" ><%-- 20 --%>
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# Eval("prd_accion")%>' />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <%--<asp:TemplateField HeaderText="N°Cuotas Saliente" ItemStyle-Wrap="false"  ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:TextBox DataField="pca_PR_Saldo_Cuotas" ID="pca_PR_Saldo_Cuotas" runat="server" 
                                        Text='<%# Eval("pca_PR_Saldo_Cuotas")%>' />
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                            <asp:TemplateField HeaderText="N°Cuotas Saliente" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Right" ><%-- 21 --%>
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# Eval("prd_cuotassalientes")%>'  />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <%--<asp:BoundField DataField="pca_PR_Monto" HeaderText="Monto" /><%-- 22 --%>
                            <asp:TemplateField HeaderText="Monto" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Right" ><%-- 22 --%>
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# Eval("x_MontoSaliente")%>' />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <%--<asp:TemplateField HeaderText="Serie entrante">
                                <ItemTemplate>
                                    <asp:DropDownList ID="Can_SerieOptima" runat="server" GroupName="SerieOptima" AutoPostBack="true" OnSelectedIndexChanged="Can_SerieOptima_SelectedIndexChanged">
                                        <asp:ListItem Value="">Seleccione una opción</asp:ListItem>
                                    </asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                            <asp:TemplateField HeaderText="Serie entrante" ItemStyle-Wrap="false"  ItemStyle-HorizontalAlign="center"><%-- 23 --%>
                                <ItemTemplate>
                                    <asp:Label id="lblSerieOptima" runat="server" Text='<%# Eval("x_fs_nemotecnico")%>' Width="100%"/>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <%--<asp:BoundField DataField="NavCuotaEntrante" HeaderText="NAV Cuota Entrante" />--%>
                            <asp:TemplateField HeaderText="NAV Cuota<br/>Entrante" ItemStyle-Wrap="false" ItemStyle-Width="100" ItemStyle-HorizontalAlign="Right"><%-- 24 --%>
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# Eval("prd_naventrante")%>' Width="100%"/>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <%--<asp:BoundField DataField="Factor" HeaderText="Factor" />--%>
                            <asp:TemplateField HeaderText="Factor" ItemStyle-Wrap="false"  ItemStyle-HorizontalAlign="Right"><%-- 25 --%>
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# Eval("x_Factor")%>' Width="100%"/>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <%--<asp:BoundField DataField="CuotasEntrante" HeaderText="N°Cuotas Entrante" />--%>
                            <asp:TemplateField HeaderText="N°Cuotas Entrante" ItemStyle-Wrap="true"  ItemStyle-HorizontalAlign="Right"><%-- 26 --%>
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# Eval("prd_cuotasentrante")%>' Width="100%"/>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <%--<asp:BoundField DataField="MontoEntrante" HeaderText="Monto entrante" />--%>
                            <asp:TemplateField HeaderText="Monto entrante" ItemStyle-Wrap="true"  ItemStyle-HorizontalAlign="Right"><%-- 27 --%>
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# Eval("x_MontoEntrante")%>' Width="100%"/>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <%--<asp:BoundField DataField="MontoEntranteCLP" HeaderText="Monto entrante (CLP)" />--%>
                            <asp:TemplateField HeaderText="Monto entrante (CLP)" ItemStyle-Wrap="true"  ItemStyle-HorizontalAlign="Right" ><%-- 28 --%>
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# Eval("x_MontoEntranteCLP")%>' Width="100%"/>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <%--<asp:BoundField DataField="Diferencia" HeaderText="Diferencia" /--%>
                            <asp:TemplateField HeaderText="Diferencia" ItemStyle-Wrap="true"  ItemStyle-HorizontalAlign="Right"><%-- 29 --%>
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# Eval("prd_diferencia")%>' Width="100%"/>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <%--<asp:BoundField DataField="DiferenciaCLP" HeaderText="Diferencia CLP" />--%>
                            <asp:TemplateField HeaderText="Diferencia CLP" ItemStyle-Wrap="true"  ItemStyle-HorizontalAlign="Right"><%-- 30 --%>
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# Eval("prd_diferenciaclp")%>' Width="100%"/>
                                </ItemTemplate>
                            </asp:TemplateField>
                            
                            <%--<asp:TemplateField HeaderText="Moneda Pago">                                <ItemTemplate>
                                    <asp:DropDownList ID="pca_FS_Moneda" runat="server" AutoPostBack="false" SelectedValue='<%# Eval("FS_Moneda")%>'>
                                        <asp:ListItem Value="">Seleccione una opción</asp:ListItem>
                                        <asp:ListItem Value="USD">USD</asp:ListItem>
                                        <asp:ListItem Value="CLP">CLP</asp:ListItem>
                                        <asp:ListItem Value="EUR">EUR</asp:ListItem>
                                    </asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                            <asp:TemplateField HeaderText="Moneda Pago" ItemStyle-Wrap="true"  ItemStyle-HorizontalAlign="center"><%-- 31 --%>
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# Eval("fs_monedaentrante")%>' Width="100%"/>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <%--<asp:TemplateField HeaderText="Observacion">
                                <ItemTemplate>
                                    <asp:TextBox ID="Observacion" runat="server"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                            <asp:TemplateField HeaderText="Observacion" ItemStyle-Wrap="true" ><%-- 32 --%>
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# Eval("prd_observaciones")%>' Width="100%"/>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <%--<asp:BoundField DataField="pca_PR_DescEstado" HeaderText="Cumple Requisito" />--%>
                            <asp:TemplateField HeaderText="Cumple Requisito" ItemStyle-Wrap="true"  ItemStyle-HorizontalAlign="center"><%-- 33 --%>
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# Eval("x_pr_descestado")%>' Width="100%"/>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <%--<asp:BoundField DataField="pca_C_AP_Nac_Ext" HeaderText="Extranjero" />--%>
                            <asp:TemplateField HeaderText="Extranjero" ItemStyle-Wrap="true"  ItemStyle-HorizontalAlign="center"><%-- 34 --%>
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# Eval("x_c_ap_nac_ext")%>' Width="100%"/>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <%--<asp:BoundField DataField="pca_C_AP_Calificado" HeaderText="Calificado" />--%>
                            <asp:TemplateField HeaderText="Calificado" ItemStyle-Wrap="true"  ItemStyle-HorizontalAlign="center"><%-- 35 --%>
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# Eval("x_c_ap_calificado")%>' Width="100%"/>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <%--<asp:BoundField DataField="pca_C_AP_Rel_MAM" HeaderText="MAM" />--%>
                            <asp:TemplateField HeaderText="MAM" ItemStyle-Wrap="true"  ItemStyle-HorizontalAlign="center"><%-- 36 --%>
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# Eval("x_c_ap_calificado")%>' Width="100%"/>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <%--<asp:BoundField DataField="pca_C_AP_Limite" HeaderText="Monto" ItemStyle-Wrap="true" ItemStyle-HorizontalAlign="center"/>--%>
                            <asp:TemplateField HeaderText="Monto" ItemStyle-Wrap="true"  ItemStyle-HorizontalAlign="center"><%-- 37 --%>
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# Eval("x_c_ap_limite")%>' Width="100%"/>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <%--<asp:BoundField DataField="pca_C_Certificado" HeaderText="Certificado" ItemStyle-Wrap="true"  ItemStyle-HorizontalAlign="center"/>--%>
                            <asp:TemplateField HeaderText="Certificado" ItemStyle-Wrap="true"  ItemStyle-HorizontalAlign="center"><%-- 38 --%>
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# Eval("x_c_certificado")%>' Width="100%"/>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <%--<asp:BoundField DataField="pca_C_AP_Final_I" HeaderText="Estado Certificado" ItemStyle-Wrap="false"  ItemStyle-HorizontalAlign="center"/>--%>
                            <asp:TemplateField HeaderText="Estado Certificado" ItemStyle-Wrap="true"  ItemStyle-HorizontalAlign="center"><%-- 39 --%>
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# Eval("x_c_ap_final_i")%>' Width="100%"/>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:BoundField DataField="pca_C_Cuotas_C" HeaderText="Cuotas Certificadas" ItemStyle-Wrap="false"  ItemStyle-HorizontalAlign="center"/><%-- 40 --%>
                            <%--<asp:BoundField DataField="pca_C_Cuotas_Certificar" HeaderText="Cuotas x Certificar" />--%>
                            <asp:TemplateField HeaderText="Cuotas x Certificar" ItemStyle-Wrap="false"  ItemStyle-HorizontalAlign="center"><%-- 41 --%>
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# Eval("x_c_cuotas_certificar")%>' Width="100%" Style="text-align: right !important;"/>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:BoundField DataField="pca_PR_ID" HeaderText="ID"  /><%-- 42 --%>
                            <asp:BoundField DataField="pca_SerieOptima" HeaderText="SO"  /><%-- 43 --%>
                            <asp:BoundField DataField="PR_ID" HeaderText="PR_ID"  /><%-- 44 --%>

                            
                        </Columns>
                       <EditRowStyle BackColor="#7C6F57" />
                        <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />

                        <HeaderStyle BackColor="" Font-Size="" Font-Bold="True" ForeColor="White"></HeaderStyle>

                        <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />

                        <RowStyle Font-Size="Small" BackColor=""></RowStyle>
                        <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                    </asp:gridview>
                </div>
            </div>
        </div>

        <div class="row mt-3">
            <div class="col-md-2">
                <asp:button id="btnVolver" runat="server" width="125px" class="btn btn-secondary" text="Volver" onclientclick="return volver();" visible="False"></asp:button>
            </div>
        </div>
    </div>

    <!-- Bootstrap Modal Dialog Crear/Modificar -->
    <div class="modal fade" id="myModal" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" data-keyboard="false" data-backdrop="static">
        <div class="modal-dialog" style="max-width: 70%;">
            <div class="modal-content text-center">
                <div class="modal-header">
                    <h4 class="modal-title">
                        <asp:label id="lblModalTitulo" runat="server" text="Formulario - Distribución" font-bold="true" font-size="X-Large">
                        </asp:label>
                    </h4>
                    <button id="btnXCerrar" type="button" class="close" data-dismiss="modal" aria-label="Close" onclick="cerrarAlert(); return true;">
                        <span aria-hidden="true" onclick="cerrarAlert(); return true;">&times;</span>
                    </button>
                </div>
                <div class="modal-body">               
  
                        <div class="card-body pt-2 pb-2 text-left">
                            <div class="row">
                                <!-- RUT-->
                                <div class="col-md-2">
                                    <label class="form-control-label">Rut Aportante</label>
                                </div>
                                <div class="col-md-3">
                                    <asp:textbox runat="server" id="lblRutAportante" enabled="false" style="width:100%" />
                                </div>

                                <!-- FECHA DE INGRESO -->
                                <div class="col-md-2">
                                    <label class="form-control-label">Fondo</label>
                                </div>
                                <div class="col-md-3">
                                    <asp:textbox id="lblFondo" runat="server" enabled="false" style="width:100%" />
                                </div>
                            </div>
                            <div class="row"></div>
                            <div class="row mt-9">
                                <!-- NOMBRE APORTANTE -->
                                <div class="col-md-2">
                                    <label class="form-control-label">Nemotecnico Saliente</label>
                                </div>
                                <div class="col-md-3">
                                    <asp:textbox id="lblNemotecnicoSaliente" runat="server" enabled="false"  />
                                </div>

                                <div class="col-md-2">
                                    <label class="form-control-label">Cuotas Salientes</label>
                                </div>
                                <div class="col-md-3">
                                    <asp:textbox id="lblCuotasSalientes" runat="server" enabled="false" Style="text-align: right !important;" />
                                </div>

                            </div>
                            <div class="row"></div>
                            <div class="row mt-9">
                                <!-- NOMBRE APORTANTE -->
                                <div class="col-md-2">
                                    <label class="form-control-label">Nav Saliente</label>
                                </div>
                                <div class="col-md-3">
                                    <asp:textbox id="lblNavSaliente" runat="server" enabled="false" Style="text-align: right !important;"/>
                                </div>
                                <div class="col-md-2">
                                    <label class="form-control-label">Nav Saliente CLP</label>
                                </div>
                                <div class="col-md-3">
                                    <asp:textbox id="lblNavSalienteCLP" runat="server" enabled="false" Style="text-align: right !important;" />
                                </div>
                            </div>
                            <div class="row"></div>
                            <div class="row mt-9">
                                <!-- NOMBRE APORTANTE -->
                                <div class="col-md-2">
                                    <label class="form-control-label">Monto Saliente</label>
                                </div>
                                <div class="col-md-3">
                                    <asp:textbox id="lblMontoSaliente" runat="server" enabled="false" Style="text-align: right !important;" />
                                </div>
                                <div class="col-md-2">
                                    <label class="form-control-label">Monto Saliente CLP</label>
                                </div>
                                <div class="col-md-3">
                                    <asp:textbox id="lblMontoSalienteCLP" runat="server" enabled="false" Style="text-align: right !important;"/>
                                </div>
                            </div>
                            <div class="row"></div>
                            <div class="row mt-9">
                                <!-- NOMBRE APORTANTE -->
                                <div class="col-md-2">
                                    <label class="form-control-label">Fecha Canje</label>
                                </div>
                                <div class="col-md-3">
                                    <asp:textbox id="lblFechaCanje" runat="server" enabled="false" />
                                </div>
                                <div class="col-md-2">
                                    <label class="form-control-label">Tipo de cambio</label>
                                </div>
                                <div class="col-md-3">
                                    <asp:textbox id="txtTipoCambio" runat="server" enabled="false" Style="text-align: right !important;" />
                                </div>
                            </div>
                            <asp:HiddenField ID="hidSerieOptima" runat="server" />
                        </div>
                            <!-- TABLA -->
                            <div class="card col-md-12" style="overflow-x:auto;">

                                <h4>Asignación</h4>
                                <br />

                                <asp:UpdatePanel runat="server" ID="UpdatePanelGrilla1" UpdateMode="Always">
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="btnAddAsignacion" EventName="Click" />
                                        <asp:AsyncPostBackTrigger ControlID="btnEliminarAsignacion" EventName="Click"/>
                                    </Triggers>

                                    <ContentTemplate>
                                        <asp:GridView ID="grvAsignacion"
                                            runat="server"
                                            CssClass="table table-bordered table-hover table-sm gvv3"
                                            HeaderStyle-BackColor=""
                                            HeaderStyle-Font-Size=""
                                            RowStyle-Font-Size="Small"
                                            AutoGenerateColumns="False"
                                            AllowSorting="false">

                                            <Columns>
                                                <asp:TemplateField HeaderText="Sel.">
                                                    <ItemTemplate>
                                                        <asp:RadioButton ID="RowSelector" runat="server" onclick="checkRadioBtn(this);" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="PRD_ID" HeaderText="HijoID" />                 <%-- 1 --%>
                                                <asp:BoundField DataField="PR_ID" HeaderText="padreID" />                 <%-- 2 --%>
                                                <asp:BoundField DataField="FS_Nemotecnico" HeaderText="Serie Saliente" ItemStyle-Wrap="true"/> <%-- 3 --%>
                                                <asp:TemplateField HeaderText="Acción">                                   <%-- 4 --%>
                                                    <ItemTemplate>
                                                        <asp:DropDownList ID="cmbOpcion" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cmbOpcion_SelectedIndexChanged" SelectedValue='<%# Eval("PRD_Accion")%>'>
                                                            <asp:ListItem Value="">Seleccione una opción</asp:ListItem>
                                                            <asp:ListItem Value="Mantener">Mantener</asp:ListItem>
                                                            <asp:ListItem Value="Canjear Hacia Arriba">Canjear Hacia Arriba</asp:ListItem>
                                                            <asp:ListItem Value="Canjear hacia abajo">Canjear hacia abajo</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Cuotas Saliente (*)" >                            <%-- 5 --%>
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtPRDCuotasSaliente" runat="server" CssClass="dbs-entero13-decimal0" AutoPostBack="true" Text='<%# Eval("PRD_CuotasSalientes")%>' OnTextChanged="txtPRDCuotasSaliente_textChanged"/>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:BoundField DataField="PRD_MontoSaliente" HeaderText="Monto Saliente (*)" DataFormatString="{0:N2}" ItemStyle-HorizontalAlign="Right"/> <%-- 6 --%>
                                                <asp:BoundField DataField="PRD_MontoSalienteCLP" HeaderText="Monto Saliente CLP (*)" DataFormatString="{0:N0}" ItemStyle-HorizontalAlign="Right"/> <%-- 7 --%>
                                                
                                                <asp:TemplateField HeaderText="Serie entrante (*)">                             <%-- 8 --%>
                                                    <ItemTemplate>
                                                        <asp:DropDownList ID="cmbSerieHija" runat="server" GroupName="SerieHija" 
                                                            AutoPostBack="true" OnSelectedIndexChanged="cmbSerieHija_SelectedIndexChanged">
                                                            <asp:ListItem Value="">Seleccione una opción</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:BoundField DataField="PRD_NAVEntrante" HeaderText="NAV Cuota Entrante (*)" DataFormatString="{0:N6}" ItemStyle-Wrap="true" ItemStyle-HorizontalAlign="Right"/>   <%-- 9 --%>
                                                <asp:BoundField DataField="PRD_Factor" HeaderText="Factor" DataFormatString="{0:N8}" ItemStyle-HorizontalAlign="Right"/>                      <%-- 10 --%>
                                                <asp:BoundField DataField="PRD_CuotasEntrante" HeaderText="N°Cuotas Entrante" DataFormatString="{0:N0}" ItemStyle-HorizontalAlign="Right"/>        <%-- 11 --%>
                                                <asp:BoundField DataField="PRD_MontoEntrante" HeaderText="Monto Entrante" DataFormatString="{0:N2}" ItemStyle-HorizontalAlign="Right"/>             <%-- 12 --%>
                                                <asp:BoundField DataField="PRD_NAVEntranteCLP" HeaderText="NAV Entrante CLP" DataFormatString="{0:N4}" ItemStyle-HorizontalAlign="Right"/>             <%-- 13 --%>
                                                <asp:BoundField DataField="PRD_MontoEntranteCLP" HeaderText="Monto Entrante CLP " DataFormatString="{0:N0}" ItemStyle-Wrap="true" ItemStyle-HorizontalAlign="Right"/>   <%-- 14 --%>
                                                <asp:BoundField DataField="PRD_Diferencia" HeaderText="Diferencia" DataFormatString="{0:N2}" ItemStyle-HorizontalAlign="Right"/>                    <%-- 15 --%>
                                                <asp:BoundField DataField="PRD_DiferenciaCLP" HeaderText="Diferencia CLP" DataFormatString="{0:N0}" ItemStyle-HorizontalAlign="Right"/>             <%-- 16 --%>

                                                <asp:TemplateField HeaderText="Moneda Pago">                                             <%-- 17 --%>
                                                    <ItemTemplate>
                                                        <asp:DropDownList ID="cmbMonedaDetalle" runat="server" AutoPostBack="true" SelectedValue='<%# Eval("FS_MonedaEntrante")%>' OnSelectedIndexChanged="cmbMonedaDetalle_SelectedIndexChanged">
                                                            <asp:ListItem Value="">Seleccione una opción</asp:ListItem>
                                                            <asp:ListItem Value="USD">USD</asp:ListItem>
                                                            <asp:ListItem Value="CLP">CLP</asp:ListItem>
                                                            <asp:ListItem Value="EUR">EUR</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Observacion">                                            <%-- 18 --%>
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtObservacion" runat="server" AutoPostBack="true" Text='<%# Eval("PRD_Observaciones")%>' OnTextChanged="txtObservacion_SelectedIndexChanged" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:BoundField DataField="PR_DescEstado" HeaderText="Estado" ItemStyle-Wrap="true"/>                        <%-- 19 --%>
                                                <asp:BoundField DataField="C_AP_Nac_Ext" HeaderText="Nacional/Extranjero" ItemStyle-Wrap="true"/>            <%-- 20 --%>
                                                <asp:BoundField DataField="C_AP_Calificado" HeaderText="Calificado" ItemStyle-Wrap="true"/>                  <%-- 21 --%>
                                                <asp:BoundField DataField="C_AP_Rel_MAM" HeaderText="MAM" ItemStyle-Wrap="true"/>                       <%-- 22 --%>
                                                <asp:BoundField DataField="C_AP_Limite" HeaderText="Limite" ItemStyle-Wrap="true"/>                     <%-- 23 --%>
                                                <asp:BoundField DataField="C_Certificado" HeaderText="Certificado" ItemStyle-Wrap="true"/>                 <%-- 24 --%>
                                                <asp:BoundField DataField="C_AP_Final_I" HeaderText="Estado Certificado" ItemStyle-Wrap="true"/>                   <%-- 25 --%>
                                                <asp:BoundField DataField="C_Cuotas_C" HeaderText="Cuotas Certificadas" ItemStyle-Wrap="true" DataFormatString="{0:N2}" ItemStyle-HorizontalAlign="Right" />                      <%-- 26 --%>
                                                <asp:BoundField DataField="C_Cuotas_Certificar" HeaderText="Cuotas x Certificar" ItemStyle-Wrap="true" DataFormatString="{0:N2}" ItemStyle-HorizontalAlign="Right"/>     <%-- 27 --%>
                                                <asp:BoundField DataField="filaRowPadre" HeaderText="filaRowPadre" />     <%-- 28 --%>
                                                <asp:BoundField DataField="Clave" HeaderText="Key" />     <%-- 29 --%>
                                                <asp:BoundField DataField="NemoSeleccionado" HeaderText="Key" />     <%-- 30 --%>

                                            </Columns>
                                        </asp:GridView>

                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <!-- GRUPO DE BOTONES -->
                            <div class="form-group mt-5">
                                <div class="col-md-offset-1">
                                    <asp:button id="btnAddAsignacion" text="Agregar" cssclass="btn btn-success" runat="server"/>
                                    <asp:button id="btnEliminarAsignacion" clientidmode="Static" text="Eliminar" runat="server" class="btn btn-danger" onclientclick="if (!confirm('¿Seguro que desea Eliminar el Grupo seleccionado?')) return false;" enabled="true"/>
                                </div>
                            </div>
                            <!-- FIN GRUPO DE BOTONES -->
                            <hr />

                            <div class="form-group">
                                <div class="col-12 text-center">
                                    <asp:button id="btnModalGuardar" text="Guardar" cssclass="btn btn-info" runat="server" onclientclick="return validateBtn();" />
                                    <asp:button id="btnModalCancelar" runat="server" text="Cancelar" cssclass="btn btn-secondary" onclientclick="return volver('¿Seguro que desea Cancelar?');"/>
                                </div>
                            </div>
                        </div>
                    <!-- <div class="modal-footer">-->
              </div>
        </div>
    </div>
    <!-- End Bootstrap Modal Dialog Crear/Modificar -->

    <asp:hiddenfield id="hidPag" runat="server" value="0" />
    <asp:hiddenfield id="TabName" runat="server" value="#nav-home" />
    <asp:hiddenfield id="txtAccionHidden" runat="server" value="" />
    <asp:hiddenfield id="Hiddenfield1" runat="server" />
    <asp:hiddenfield id="txtHiddenGrupo" runat="server" />

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="FooterScript" Runat="Server">
    <script src="<%=ResolveUrl("~/Scripts/jquery.dataTables.min.js")%>"></script>
    <script src="<%=ResolveUrl("~/Scripts/dataTables.bootstrap4.min.js")%>"></script>
    <script src="<%=ResolveUrl("~/Scripts/scripts.js")%>"></script>
    <script src="<%=ResolveUrl("~/Scripts/select2.min.js")%>"></script>

    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/jquery.sumoselect.js")%>"></script>
    <link rel="stylesheet" href="<%=ResolveUrl("~/style/sumoselect.css")%>" type="text/css" />

    <style type="text/css">
        .hiddencol {
            display: none;
        }
    </style>

    <script>

        $(function () {
            var tabName = $("[id*=TabName]").val() != "" ? $("[id*=TabName]").val() : "personal";
            $('#nav-tab a[href="#' + tabName + '"]').tab('show');
            $("#nav-tab a").click(function () {
                $("[id*=TabName]").val($(this).attr("href").replace("#", ""));
            });
        });

        $(document).ready(function () {
            var txtAccionHidden = $("#<%=txtAccionHidden.ClientID %>").val();
            if (txtAccionHidden == "DISTRIBUIR") {
                $('#myModal').modal('show');
            }
            confNumeros();
            bindDataTable(); 

            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(bindDataTable);
        });

        function confNumeros() {            
            $('.dbs-entero13-decimal0').mask2(getMask(13, 0));
        }  

        function bindDataTable() {
            setComboSumo();
            setDatePicker();
            setDataTableGrvCanje();
            setDataTableGrvAsignacion()
            setDataTableGrvCorte();
            confNumeros();
        }

        function setComboSumo() {
            $(<%=ddlFondo.ClientID%>).SumoSelect({
                selectAll: true,
                okCancelInMulti: true,
                csvDispCount: 3,
                captionFormatAllSelected: '{0} Seleccionados',
                placeholder: 'Seleccione aquí',
                captionFormat: '{0} Seleccionado',
                searchText: 'Buscar...',
                noMatch: 'No hay coincidencias para "{0}"',
                prefix: '',
                locale: ['OK', 'Cancelar', 'Todos'],
                showTitle: 'False'
            });

        //$(".js-select2-rut").select2({
        //    templateResult: formatState,
        //    placeholder: 'Selecciona una opción'
        //});
        }

        function setDatePicker() {
            $("[id*=txtFechaCanje]").datepicker();
            $("[id*=txtFechaProceso]").datepicker();

            $("[id*=txtFechaProceso]").change(function () {
                changeFechas($("[id*=txtFechaProceso]"), $("[id*=txtFechaCanje]"), 1)
            });

            $("[id*=txtFechaCanje]").change(function () {
                changeFechas($("[id*=txtFechaProceso]"), $("[id*=txtFechaCanje]"), 2)
            });
        }

        function showModal(prID) {
            $('#myModal').modal('show');
            return false;
        }

        function llamarFuncion() {
        }

        function setDataTableGrvAsignacion() {
            var sTable = '#<%=grvAsignacion.ClientID%>';
             
            if ( ! $.fn.DataTable.isDataTable( sTable ) ) {
                $(".gvv3").each(function () {
                    $(this).prepend($("<thead></thead>").append($(this).find("tr:first"))).DataTable({
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
                        for (var i = 1; i < this.rows.length; i++) {
                            var radioBtn = this.rows[i].cells[0].getElementsByTagName("input");
                            if (radioBtn.length > 0) {
                                radioBtn[0].checked = false;
                                enableDisableButtons(true);
                            }
                        }
                    });
                });
               
            }

            var table = $(sTable).DataTable();

            //if (table.column(1).visible() == false) {
            table.columns([1, 2, 26, 27, 28, 29, 30]).visible(false, false);   // Oculta las columnas Indeseadas
            table.columns.adjust();
            table.draw(false);    // redibuja la grilla

            //}
        }

        function setDataTableGrvCanje() {
            var table = $('#<%=GrvCanje.ClientID%>').DataTable();
            table.columns([1, 3, 4, 5, 6, 16, 40, 42, 43,44]).visible(false, false);   // Oculta las columnas Indeseadas
            table.page(<%=hidPag.Value%>);  // Se posiciona en la pagina apuntada por hidPag ( texto Hidden )

            table.on('page.dt', function () {
                var info = table.page.info();
                $('#<%=hidPag.ClientID%>').val(info.page);
            })

            table.columns.adjust();

            table.draw(false);    // redibuja la grilla
        }

        function setDataTableGrvCorte() {
            var table = $('#<%=GrvFechaCanje.ClientID%>').DataTable();
            table.columns([2, 3]).visible(false, false);   // Oculta las columnas Indeseadas

            table.columns.adjust();

            table.draw(false);    // redibuja la grilla
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


        function checkRadioBtn(id) {
            var gv = document.getElementById('<%=grvAsignacion.ClientID %>');
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

        function enableDisableButtons(newValue) {
            //btnEliminarAsignacion.disabled = newValue;
        }

        function soloNumerosyComa(e) {
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

        function volver(msg) {
            if (!confirm(msg)) {
                return false;
            } else {
                cerrarAlert();
                return true;
            }
        }


         function cerrarAlert() {
            $('#<%=txtAccionHidden.ClientID %>').val("");
        };
    </script>

</asp:Content>

