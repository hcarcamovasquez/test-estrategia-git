<%@ Page Title="Series" Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false" CodeFile="frmMantenedorFondoSerie.aspx.vb" Inherits="Presentacion_Mantenedores_frmMantenedorFondoSerie" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:scriptmanager id="ScriptManager1" runat="server" />

    <h2 class="TdRedondeado titleMant">Maestro de <strong>Series</strong></h2>
    <div class="card p-4 jumbotron">

        <div class="row">
            <!-- LISTA RUT -->
            <div class="col-md-4">
                <asp:label runat="server" id="rutfondo">Fondo</asp:label>
                <asp:dropdownlist id="ddlListaRutFondo" cssclass="form-control js-select2-rut" runat="server" autopostback="True"></asp:dropdownlist>
            </div>

            <!-- LISTA NEMOTECNICOS -->
            <div class="col-md-4">
                <asp:label runat="server" id="nemotecnico">Nemotécnico</asp:label>
                <asp:dropdownlist id="ddlListaNemotecnico" cssclass="form-control js-select2-rut" runat="server" autopostback="True"></asp:dropdownlist>
            </div>

            <!-- LISTA GRUPO COMPATIBLE -->
            <div class="col-md-4">
                <asp:label runat="server" id="lbNombreCompatible">Grupo Compatible</asp:label>
                <asp:dropdownlist id="ddlListaGrupoCompatible" cssclass="form-control js-select2-rut" runat="server" autopostback="True"></asp:dropdownlist>
            </div>
        </div>

        <!-- BOTONES BUSCAR LIMPIAR Y CREAR -->
        <div class="row text-center mt-5 p-3 border-bottom">
            <div class="col-md-12">
                <!-- BOTÓN BUSCAR -->
                <asp:button id="BtnBuscar" text="Buscar" class="btn btn-moneda" runat="server" />
                <asp:button id="btnLimpiarFrm" text="Borrar" class="btn btn-secondary" runat="server" onclick="btnLimpiarFrm_Click" />

                <!-- BOTÓN CREAR -->
                <asp:button id="btnCrear" text="Crear" class="btn btn-info" runat="server" onclick="btnCrear_Click" />
            </div>
        </div>

        <div class="row">
            <!-- LISTA NOMBRE FONDOS -->
            <div class="col-md-5">
                <asp:label runat="server" id="lbnombreFondo" visible="False">Nombre Fondo</asp:label>
                <asp:dropdownlist id="ddlListaNombreFondo" cssclass="form-control js-select2-rut" runat="server" visible="False" />
            </div>
        </div>

        <!-- TABLA DE RESULTADOS -->
        <h5 class="mt-3 text-secondary"><i class="fas fa-file-invoice fa-sm"></i>Resultado de la búsqueda</h5>
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
                           <asp:RadioButton ID="RowSelector" runat="server" onclick="checkRadioBtn(this);" GroupName="a" AutoPostBack="false" OnCheckedChanged="RowSelector_CheckedChanged" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Rut" HeaderText="Rut Fondo" />
                    <asp:BoundField DataField="Nemotecnico" HeaderText="Nemotécnico" />
                    <asp:BoundField DataField="Nombrecorto" HeaderText="Nombre Corto Serie" />
                    <asp:BoundField DataField="Remuneracion" HeaderText="Tipo Remuneración" />
                    <asp:BoundField DataField="Nacionalidad" HeaderText="Domiciliado Extranjero"/>
                    <asp:BoundField DataField="Calificado" HeaderText="Inversionista Calificado"/>
                    <asp:BoundField DataField="Moneda" HeaderText="Moneda Serie"/>     
                    <asp:BoundField DataField="LimiteMoneda" HeaderText="Moneda de Límite"/>
                    <asp:BoundField DataField="LimiteMinimo" HeaderText="Monto Mínimo" ItemStyle-HorizontalAlign="Right"/>
                    <asp:BoundField DataField="LimiteMaximo" HeaderText="Monto Máximo" ItemStyle-HorizontalAlign="Right"/>
                    <asp:BoundField DataField="exclusivoPaso" HeaderText="Exclusivo MAM"/>
                    <asp:BoundField DataField="compatiblePaso" HeaderText="Contrato Distribución"/>
                    <asp:BoundField DataField="canjePaso" HeaderText="Canje Mandatario"/>
                    <asp:BoundField DataField="Nivel" HeaderText="Grupo Compatible" ItemStyle-HorizontalAlign="Right"/>
                    <asp:BoundField DataField="HorarioRecaste" HeaderText="Horario Corte Rescates" />
                    <asp:BoundField DataField="FondoRescatable" HeaderText="Fondo Rescatable" />
                    <asp:BoundField DataField="fechaNavPaso" HeaderText="Fecha Nav Rescate" />
                    <asp:BoundField DataField="fechaRescatePaso" HeaderText="Fecha Rescate" />
                    <asp:BoundField DataField="fechaTcPaso" HeaderText="Fecha TC Observado Rescate" />
                    <asp:BoundField DataField="Patrimonio" HeaderText="Patrimonio" />
                    <asp:BoundField DataField="FijacionNav" HeaderText="Fijación Nav" />
                    <asp:BoundField DataField="fechaNavSusPaso" HeaderText="Fecha Nav Suscripción" />
                    <asp:BoundField DataField="fechaSusPaso" HeaderText="Fecha Suscripción" />
                    <asp:BoundField DataField="fechaTcSusPaso" HeaderText="Fecha TC Suscripción" />
                    <asp:BoundField DataField="FijacionSuscripcion" HeaderText="Fijación Nav Suscripción" />
                    <asp:BoundField DataField="fechaNavcPaso" HeaderText="Fecha Nav Canje" />
                    <asp:BoundField DataField="fechaTcCanjePaso" HeaderText="Fecha TC Canje" />
                    <asp:BoundField DataField="FijacionCanje" HeaderText="Fijación Nav Canje" />
                    <asp:BoundField DataField="FechaIngreso" HeaderText="Fecha Ingreso" DataFormatString="{0:dd/MM/yyyy}"/>
                    <asp:BoundField DataField="UsuarioIngreso" HeaderText="Usuario Ingreso" />
                    <asp:BoundField DataField="FechaModificacion" HeaderText="Fecha Modificación" DataFormatString="{0:dd/MM/yyyy}"/>
                    <asp:BoundField DataField="UsuarioModificacion" HeaderText="Usuario Modificador"/>
                </Columns>
            </asp:gridview>
        </div>
        <div class="row mt-4">
            <div class="col-md-12 text-center">
                <asp:button id="BtnModificar" runat="server" class="btn btn-info" text="Modificar" enabled="false" onclick="BtnModificar_Click"></asp:button>
                <asp:button id="BtnEliminar" runat="server" class="btn btn-danger" text="Eliminar" onclick="BtnEliminar_Click" enabled="false"></asp:button>
                <asp:button id="BtnExportar" class="btn btn-success" text="Exportar" runat="server" enabled="false" />
            </div>
        </div>

    </div>

    <asp:hiddenfield id="txtAccionHidden" runat="server" />

    <!-- Bootstrap Modal Dialog Crear/Modificar -->
    <div class="modal fade" id="myModal" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" data-keyboard="false" data-backdrop="static">
        <div class="modal-dialog" style="max-width: 90%;">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title">
                        <asp:label id="lblModalTitulo" runat="server" text="Formulario - Maestro de Series" font-bold="true" font-size="X-Large">
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
                                <div class="card col-md-6">
                                    <h4 class="modal-title">
                                        <asp:label id="lblTitleModalCrud" runat="server" text="Modificar o eliminar" font-bold="true" font-size="X-Large" />
                                    </h4>

                                    <hr />
                                    <br />

                                    <!-- FORMULARIO -->
                                    <div class="row mt-3">
                                        <div class="col-md-6">
                                            <asp:label runat="server" id="lbRutFondo">Fondo</asp:label>
                                        </div>
                                        <div class="col-md-6">
                                            <asp:dropdownlist id="ddlRutdeFondo" cssclass="form-control js-select2-rut" runat="server" />
                                        </div>
                                    </div>
                                    <div class="row mt-3">
                                        <div class="col-md-6">
                                            <asp:label runat="server" id="lbNombreRutModal" visible="False">Nombre de Fondo</asp:label>
                                        </div>
                                        <div class="col-md-6">
                                            <asp:dropdownlist id="ddlNombreRutModal" cssclass="form-control js-select2-rut" runat="server" visible="False" />
                                        </div>
                                    </div>
                                    <div class="row mt-3">
                                        <div class="col-md-6">
                                            <asp:label runat="server" id="lbNombreNemotecnico">Nemotécnico</asp:label>
                                        </div>
                                        <div class="col-md-6">
                                            <asp:updatepanel runat="server" id="UpdatePanelNemotecnico" updatemode="Conditional">
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="txtNemotecnico" EventName="TextChanged" />
                                                </Triggers>
                                                <ContentTemplate>
                                                    <asp:TextBox ID="txtNemotecnico" MaxLength="20" runat="server" CssClass="form-control form-control-sm" onkeyup = "mayusculas()"  onkeypress = "return soloNemotecnico(event)" OnTextChanged="verificarNemotecnico" AutoPostBack="true"></asp:TextBox>
                                                </ContentTemplate>
                                            </asp:updatepanel>

                                        </div>
                                    </div>
                                    <div class="row mt-3">
                                        <div class="col-md-6">
                                            <label class="form-control-label">Nombre Corto de Serie</label>
                                        </div>
                                        <div class="col-md-6">
                                            <asp:textbox id="txtNombreSerie" maxlength="10" runat="server" cssclass="form-control form-control-sm" onkeyup="mayusculas()" onkeypress="return soloNemotecnico(event)"></asp:textbox>
                                        </div>
                                    </div>
                                    <div class="row mt-3">
                                        <div class="col-md-6">
                                            <label class="form-control-label">Moneda Serie</label>
                                        </div>
                                        <div class="col-md-6">
                                            <asp:dropdownlist id="ddlMonedaSerie" cssclass="form-control js-select2-rut" runat="server">
                                            </asp:dropdownlist>
                                        </div>
                                    </div>
                                    <div class="row mt-3">
                                        <div class="col-md-6">
                                            <label class="form-control-label">Tipo Remuneración</label>
                                        </div>
                                        <div class="col-md-6">
                                            <asp:dropdownlist id="ddlTipoRemuneracion" cssclass="form-control js-select2-rut" runat="server"> 
                                                <asp:ListItem Value="">Seleccione una opción</asp:ListItem>
                                                <asp:ListItem Value="F">Fija</asp:ListItem>
                                                <asp:ListItem Value="FV">Fija + Variables</asp:ListItem>
                                            </asp:dropdownlist>
                                        </div>
                                    </div>

                                    <div class="row mt-3">
                                        <div class="col-md-6">
                                            <label class="form-control-label">Inversionista Calificado</label>
                                        </div>
                                        <div class="col-md-6">
                                            <asp:dropdownlist id="ddlInversionistaCalificado" cssclass="form-control js-select2-rut" runat="server">
                                               <asp:ListItem Value="">Seleccione una opción</asp:ListItem>
                                               <asp:ListItem Value="S">Si</asp:ListItem>
                                               <asp:ListItem Value="N">No</asp:ListItem>
                                           </asp:dropdownlist>

                                        </div>
                                    </div>
                                    <div class="row mt-3">
                                        <div class="col-md-6">
                                            <label class="form-control-label">Exclusivo MAM</label>
                                        </div>
                                        <div class="col-md-6">
                                            <asp:dropdownlist id="ddlExclusivoMAM" cssclass="form-control js-select2-rut" runat="server"> 
                                                <asp:ListItem Value="">Seleccione una opción</asp:ListItem>
                                                <asp:ListItem Value="-1">Si</asp:ListItem>
                                                <asp:ListItem Value="0">No</asp:ListItem>
                                            </asp:dropdownlist>
                                        </div>
                                    </div>

                                    <div class="row mt-3">
                                        <div class="col-md-6">
                                            <label class="form-control-label">Moneda de Límite</label>
                                        </div>
                                        <div class="col-md-6">
                                            <asp:dropdownlist id="ddlMonedaDeLimite" cssclass="form-control js-select2-rut" runat="server">
                                                <asp:ListItem Value="">Seleccione una opción</asp:ListItem>
                                                <asp:ListItem Value="USD">USD</asp:ListItem>
                                                <asp:ListItem Value="CLP">CLP</asp:ListItem>
                                                <asp:ListItem Value="EUR">EUR</asp:ListItem>
                                           </asp:dropdownlist>
                                        </div>
                                    </div>
                                    <div class="row mt-3">
                                        <div class="col-md-6">
                                            <label class="form-control-label">Monto Mínimo</label>
                                        </div>
                                        <div class="col-md-6">
                                            <asp:textbox id="txtMontoMinimo" runat="server" cssclass="form-control form-control-sm dbs-entero-decimal" onpaste="return false" oncut="return false" oncopy="return false" onchange="validarMontosMinimo()"></asp:textbox>
                                        </div>
                                    </div>

                                    <div class="row mt-3">
                                        <div class="col-md-6">
                                            <label class="form-control-label">Monto Máximo</label>
                                        </div>
                                        <div class="col-md-6">
                                            <asp:textbox id="txtMontoMaximo" runat="server" cssclass="form-control form-control-sm dbs-entero-decimal" onpaste="return false" oncut="return false" oncopy="return false" onchange="validarMontos()"></asp:textbox>
                                        </div>
                                    </div>
                                    <div class="row mt-3">
                                        <div class="col-md-6">
                                            <label class="form-control-label">Grupo Compatible</label>
                                        </div>
                                        <div class="col-md-6">
                                            <asp:textbox id="txtGrupoCompatible" runat="server" maxlength="18" cssclass="form-control form-control-sm" onpaste="return false" oncut="return false" oncopy="return false" onkeypress="return soloNumeros(event)"></asp:textbox>
                                        </div>
                                    </div>
                                    <div class="row mt-3">
                                        <div class="col-md-6">
                                            <label class="form-control-label">Contrato de Distribución</label>

                                        </div>
                                        <div class="col-md-6">
                                            <asp:dropdownlist id="ddlContratoDistribuicion" cssclass="form-control js-select2-rut" runat="server"> 
                                               <asp:ListItem Value="">Seleccione una opción</asp:ListItem>
                                               <asp:ListItem Value="-1">Si</asp:ListItem>
                                               <asp:ListItem Value="0">No</asp:ListItem>
                                               
                                           </asp:dropdownlist>

                                        </div>
                                    </div>
                                    <div class="row mt-3">
                                        <div class="col-md-6">
                                            <label class="form-control-label">Domiciliado Extranjero</label>
                                        </div>
                                        <div class="col-md-6">
                                            <asp:dropdownlist id="ddlDomiciliadoExtranjero" cssclass="form-control js-select2-rut" runat="server">
                                               <asp:ListItem Value="">Seleccione una opción</asp:ListItem>
                                               <asp:ListItem Value="S">Si</asp:ListItem>
                                               <asp:ListItem Value="N">No</asp:ListItem>
                                            </asp:dropdownlist>
                                        </div>
                                    </div>
                                    <div class="row mt-3">
                                        <div class="col-md-6">
                                            <label class="form-control-label">Canje Mandatorio</label>
                                        </div>
                                        <div class="col-md-6">
                                            <asp:dropdownlist id="ddlCanjeMandatorio" cssclass="form-control js-select2-rut" runat="server">
                                                <asp:ListItem Value="">Seleccione una opción</asp:ListItem>
                                                <asp:ListItem Value="-1">Si</asp:ListItem>
                                                <asp:ListItem Value="0">No</asp:ListItem>
                                           </asp:dropdownlist>
                                        </div>
                                    </div>
                                    <div class="row mt-3">
                                        <div class="col-md-6">
                                            <label class="form-control-label">Horario de Corte</label>
                                        </div>
                                        <div class="col-md-6">
                                            <asp:textbox id="txtHorarioCorte" runat="server" cssclass="form-control form-control-sm" onpaste="return false" oncut="return false" oncopy="return false" onblur="validatetime()" onkeypress="return soloNumerosHoras(event)" maxlength="5"></asp:textbox>
                                        </div>
                                    </div>
                                    <!-- FIN FORMULARIO -->

                                </div>

                                <div class="col-md-6">
                                    <div class="card">
                                        <div class="card-body">
                                            <h4 class="card-title">Rescates</h4>
                                            <hr />
                                            <div class="row mt-3">
                                                <div class="col-md-4">
                                                    <asp:label runat="server" id="Label1">Fondo Rescatable</asp:label>
                                                </div>
                                                <div class="col-md-6">
                                                    <asp:dropdownlist runat="server" id="ddlFondoRescatable" cssclass="form-control js-select2-rut">
                                                            <asp:ListItem Value="">Seleccione una opción</asp:ListItem>
                                                            <asp:ListItem Value="Si">Si</asp:ListItem>
                                                            <asp:ListItem Value="No">No</asp:ListItem>
                                                            <asp:ListItem Value="N/A">N/A</asp:ListItem>
                                                        </asp:dropdownlist>
                                                </div>
                                            </div>
                                            <div class="row mt-3">
                                                <div class="col-md-4">
                                                    <label class="form-control-label">Fecha NAV</label>
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:dropdownlist id="ddlFechaNav" cssclass="form-control js-select2-rut" runat="server">
                                                            <asp:ListItem Value="">Seleccione una opción</asp:ListItem>
                                                            <asp:ListItem Value="FechaSolicitud">Fecha Solicitud</asp:ListItem>
                                                            <asp:ListItem Value="FechaRescate">Fecha Rescate</asp:ListItem>
                                                        </asp:dropdownlist>
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:textbox id="ddDiasHabilesFechaNav" maxlength="3" runat="server" cssclass="form-control form-control-sm" onpaste="return false" oncut="return false" oncopy="return false" onkeypress="return soloNumerosNP(event)" onchange="validarRango()"></asp:textbox>
                                                </div>
                                                <div class="col-md-2">
                                                    <div class="form-check">
                                                        <asp:CheckBox ID="chkDiasHabilesRescate" runat="server" Text="" cssclass="form-check-input" />
                                                        <label class="form-check-label" for="chkDiasHabilesRescate">Días hábiles</label>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row mt-3">
                                                <div class="col-md-4">
                                                    <label class="form-control-label">Fecha Rescate</label>
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:dropdownlist id="ddlFechaRescate" cssclass="form-control js-select2-rut" runat="server">
                                                            <asp:ListItem Value="">Seleccione una opción</asp:ListItem>
                                                            <asp:ListItem Value="FechaSolicitud">Fecha Solicitud</asp:ListItem>
                                                            <asp:ListItem Value="FechaNav">Fecha Nav</asp:ListItem>
                                                        </asp:dropdownlist>
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:textbox id="ddNumeroFechaRescate" maxlength="3" cssclass="form-control form-control-sm" onpaste="return false" oncut="return false" oncopy="return false" runat="server" onkeypress="return soloNumerosNP(event)" onchange="validarRangoRescate()"></asp:textbox>
                                                </div>
                                            </div>
                                            <div class="row mt-3">
                                                <div class="col-md-4">
                                                    <label class="form-control-label">Fecha TC Observado</label>
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:dropdownlist id="ddlFechaTC" cssclass="form-control js-select2-rut" runat="server">
                                                            <asp:ListItem Value="">Seleccione una opción</asp:ListItem>
                                                            <asp:ListItem Value="FechaSolicitud">Fecha Solicitud</asp:ListItem>
                                                            <asp:ListItem Value="FechaNav">Fecha Nav</asp:ListItem>
                                                            <asp:ListItem Value="FechaRescate">Fecha Rescate</asp:ListItem>
                                                        </asp:dropdownlist>
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:textbox id="ddlNumeroFechaTC" maxlength="3" cssclass="form-control form-control-sm" onpaste="return false" oncut="return false" oncopy="return false" runat="server" onkeypress="return soloNumerosNP(event)" onchange="validarRangoTC()"></asp:textbox>
                                                </div>
                                            </div>
                                            <div class="row mt-3">
                                                <div class="col-md-4">
                                                    <label class="form-control-label">Porcentaje % Patrimonio</label>
                                                </div>
                                                <div class="col-md-6">
                                                    <asp:textbox id="txtPorcentajePatrimonio" maxlength="3" max="100" min="0" runat="server" onpaste="return false" oncut="return false" oncopy="return false" cssclass="form-control form-control-sm porcentaje" onkeypress="return soloNumeros(event)"></asp:textbox>
                                                </div>
                                            </div>
                                            <div class="row mt-3">
                                                <div class="col-md-4">
                                                    <label class="form-control-label">Fijación NAV</label>
                                                </div>
                                                <div class="col-md-6">
                                                    <asp:dropdownlist id="ddlFijacionNav" cssclass="form-control js-select2-rut" runat="server">
                                                            <asp:ListItem Value="">Seleccione una opción</asp:ListItem>
                                                            <asp:ListItem Value="Automático">Automático</asp:ListItem>
                                                            <asp:ListItem Value="Manual">Manual</asp:ListItem>
                                                        </asp:dropdownlist>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="card">
                                        <div class="card-body">
                                            <h4 class="card-title">Suscripciones</h4>
                                            <hr />
                                            <div class="row mt-1">
                                                <div class="col-md-4">
                                                    <label class="form-control-label">Fecha Suscripción</label>
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:dropdownlist id="ddlFechaSuscripcion" cssclass="form-control js-select2-rut" runat="server">
                                                            <asp:ListItem Value="">Seleccione una opción</asp:ListItem>
                                                            <asp:ListItem Value="FechaIntencion">Fecha Intencion</asp:ListItem>
                                                        </asp:dropdownlist>
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:textbox id="ddNumeroFechaSuscripcion" maxlength="3" cssclass="form-control form-control-sm" runat="server" onpaste="return false" oncut="return false" oncopy="return false" onkeypress="return soloNumerosNP(event)" onchange="validarRangoSus()"></asp:textbox>

                                                </div>
                                               
                                            </div>
                                            <div class="row mt-3">
                                                <div class="col-md-4">
                                                    <label class="form-control-label">Fecha TC Observado</label>
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:dropdownlist id="ddlFechaObservadoSuscripciones" cssclass="form-control js-select2-rut" runat="server">
                                                            <asp:ListItem Value="">Seleccione una opción</asp:ListItem>
                                                            <asp:ListItem Value="FechaSuscripcion">Fecha Suscripción</asp:ListItem>
                                                            <asp:ListItem Value="FechaNav">Fecha NAV</asp:ListItem>
                                                        </asp:dropdownlist>
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:textbox id="ddNumeroTCSuscripciones" maxlength="3" cssclass="form-control form-control-sm" runat="server" onpaste="return false" oncut="return false" oncopy="return false" onkeypress="return soloNumerosNP(event)" onchange="validarRangoTcSus()"></asp:textbox>
                                                </div>
                                            </div>
                                            <div class="row mt-3">
                                                <div class="col-md-4">
                                                    <label class="form-control-label">Fecha NAV</label>
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:dropdownlist id="ddlFechaNavSuscripciones" cssclass="form-control js-select2-rut" runat="server">
                                                            <asp:ListItem Value="">Seleccione una opción</asp:ListItem>
                                                            <asp:ListItem Value="FechaSuscripcion">Fecha Suscripción</asp:ListItem>
                                                            <asp:ListItem Value="FechaIntencion">Fecha Intención</asp:ListItem>
                                                        </asp:dropdownlist>
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:textbox id="ddNumeroFechaNavSuscripciones" maxlength="3" cssclass="form-control form-control-sm" runat="server" onpaste="return false" oncut="return false" oncopy="return false" onkeypress="return soloNumerosNP(event)" onchange="validarRangoNavSus()"></asp:textbox>

                                                </div>
                                                 <div class="col-md-2">
                                                   <div class="form-check">
                                                        <asp:CheckBox ID="chkDiasHabilesSuscipciones" runat="server" Text="" cssclass="form-check-input" />
                                                        <label class="form-check-label" for="chkDiasHabilesSuscipciones">Días hábiles</label>
                                                    </div>
                                                </div>

                                            </div>
                                            <div class="row mt-3">
                                                <div class="col-md-4">
                                                    <label class="form-control-label">Fijación NAV</label>
                                                </div>
                                                <div class="col-md-6">
                                                    <asp:dropdownlist id="ddlfijacionSuscripcion" cssclass="form-control js-select2-rut" runat="server">
                                                            <asp:ListItem Value="">Seleccione una opción</asp:ListItem>
                                                            <asp:ListItem Value="Automático">Automático</asp:ListItem>
                                                            <asp:ListItem Value="Manual">Manual</asp:ListItem>
                                                        </asp:dropdownlist>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="card">
                                        <div class="card-body">
                                            <h4 class="card-title">Canjes</h4>
                                            <hr />
                                            <div class="row mt-3">
                                                <div class="col-md-4">
                                                    <label class="form-control-label">Fecha TC Observado</label>
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:dropdownlist id="ddlfechaObservadoCanje" cssclass="form-control js-select2-rut" runat="server">
                                                            <asp:ListItem Value="">Seleccione una opción</asp:ListItem>
                                                            <asp:ListItem Value="FechaSolicitud">Fecha Solicitud</asp:ListItem>
                                                            <asp:ListItem Value="FechaNav">Fecha Nav</asp:ListItem>
                                                            <asp:ListItem Value="FechaCanje">Fecha Canje</asp:ListItem>
                                                        </asp:dropdownlist>
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:textbox id="ddlNumeroTCCanje" maxlength="3" cssclass="form-control form-control-sm" onpaste="return false" oncut="return false" oncopy="return false" runat="server" onkeypress="return soloNumerosNP(event)" onchange="validarRangoCanje()"></asp:textbox>
                                                </div>
                                            </div>

                                            <div class="row mt-3">
                                                <div class="col-md-4">
                                                    <label class="form-control-label">Fecha Nav Canje</label>
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:dropdownlist id="ddFechaNavCanje" cssclass="form-control js-select2-rut" runat="server">
                                                            <asp:ListItem Value="">Seleccione una opción</asp:ListItem>
                                                            <asp:ListItem Value="FechaSolicitud">Fecha Solicitud</asp:ListItem>
                                                            <asp:ListItem Value="FechaCanje">Fecha Canje</asp:ListItem>
                                                        </asp:dropdownlist>
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:textbox id="ddNumeroFechaNavCanje" maxlength="3" cssclass="form-control" onpaste="return false" oncut="return false" oncopy="return false" runat="server" onkeypress="return soloNumerosNP(event)" onchange="validarRangoNavCanje()"></asp:textbox>
                                                </div>
                                                 <div class="col-md-2">
                                                     <div class="form-check">
                                                        <asp:CheckBox ID="chkDiasHabilesCanje" runat="server" Text="" cssclass="form-check-input" />
                                                        <label class="form-check-label" for="chkDiasHabilesCanje">Días hábiles</label>
                                                    </div>
                                                </div>

                                            </div>

                                            <div class="row mt-3">
                                                <div class="col-md-4">
                                                    <label class="form-control-label">Fecha Canje</label>
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:dropdownlist id="ddlFechaCanje" cssclass="form-control js-select2-rut" runat="server">
                                                            <asp:ListItem Value="">Seleccione una opción</asp:ListItem>
                                                            <asp:ListItem Value="FechaSolicitud">Fecha Solicitud</asp:ListItem>
                                                            <asp:ListItem Value="FechaNav">Fecha Nav</asp:ListItem>
                                                        </asp:dropdownlist>
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:textbox id="txtNumeroFechaCanje" maxlength="3" cssclass="form-control" onpaste="return false" oncut="return false" oncopy="return false" runat="server" onkeypress="return soloNumerosNP(event)" onchange="validarRangoFechaCanje()"></asp:textbox>
                                                </div>
                                                 <div class="col-md-2">
                                                     <div class="form-check">
                                                        <asp:CheckBox ID="chkDiasHabilesFechaCanje" runat="server" Text="" cssclass="form-check-input" />
                                                        <label class="form-check-label" for="chkDiasHabilesFechaCanje">Días hábiles</label>
                                                    </div>
                                                </div>

                                            </div>



                                            <div class="row mt-3">
                                                <div class="col-md-4">
                                                    <label class="form-control-label">Fijación Nav</label>
                                                </div>
                                                <div class="col-md-6">
                                                    <asp:dropdownlist id="ddlFijacionNavCanje" cssclass="form-control js-select2-rut" runat="server">
                                                            <asp:ListItem Value="">Seleccione una opción</asp:ListItem>
                                                            <asp:ListItem Value="Automático">Automático</asp:ListItem>
                                                            <asp:ListItem Value="Manual">Manual</asp:ListItem>
                                                        </asp:dropdownlist>
                                                </div>

                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <asp:hiddenfield id="txtEstadoCambio" runat="server"></asp:hiddenfield>
                            <!-- GRUPO DE BOTONES 2 -->
                            <div class="form-group mt-5 text-right">
                                <div class="col-md-offset-1">
                                    <asp:button id="btnModalGuardar" text="Guardar" cssclass="btn btn-info" runat="server" onclientclick="return validateBtn();"></asp:button>
                                    <asp:button id="btnModalModificar" text="Modificar" cssclass="btn btn-info" runat="server" onclientclick="return validateBtn();"></asp:button>
                                    <asp:button id="btnModalCancelar" text="Cancelar" cssclass="btn btn-secondary" runat="server" onclientclick="if (!confirm('¿Seguro que desea Cancelar?')) return false;"></asp:button>
                                    <asp:button id="btnModalEliminar" text="Eliminar" runat="server" class="btn btn-danger" onclientclick="if (!confirm('¿Seguro que desea Eliminar los elementos seleccionados?')) return false;"></asp:button>
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
    <!-- End Bootstrap Modal Dialog Mensajes-->
</asp:Content>
<asp:Content ContentPlaceHolderID="FooterScript" runat="Server">
    <script src="<%=ResolveUrl("~/Scripts/jquery.dataTables.min.js")%>"></script>
    <script src="<%=ResolveUrl("~/Scripts/dataTables.bootstrap4.min.js")%>"></script>
    <script src="<%=ResolveUrl("~/Scripts/scripts.js")%>"></script>
    <script src="<%=ResolveUrl("~/Scripts/select2.min.js")%>"></script>
    <script src="<%=ResolveUrl("~/Scripts/date-dd-mmm-yyyy.js")%>"></script>

    <script>

        $(document).ready(
            function () {
            var txtAccionHidden = $('#<%=txtAccionHidden.ClientID %>').val();
            if ((txtAccionHidden == "MODIFICAR") || (txtAccionHidden == "CREAR") || (txtAccionHidden == "ELIMINAR") || (txtAccionHidden == "MANTENER_MODAL")) {
                $('#myModal').modal('show');
            } else if (txtAccionHidden == "MOSTRAR_DIALOGO") {
                $('#myModalmg').modal();
            } else {
                checkRadioBtn("");
            }

            $(".js-select2-rut").select2({
                templateResult: formatState,
                placeholder: 'Selecciona una opción'
            });

             confNumeros();
        });


        function confNumeros() {            

            $('.dbs-entero-decimal').mask2(getMask(26, 0));
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

        function cerrarAlert() {
            $('#<%=txtAccionHidden.ClientID %>').val("");
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

        $(document).on('keyup', '.porcentaje', function (event) {
            let max = parseInt(this.max);
            let min = parseInt(this.min);
            let valor_int = parseInt(this.value);
            let valor = this.value;
            var algo = valor.length;
            if (valor_int > max) {
                var ultimo_valor = valor.substring(0, valor.length - 1);
                this.value = parseInt(ultimo_valor);
            } else if (valor_int < min) {
                var ultimo_valor = valor.substring(1, valor.length - 2);
                this.value = parseInt(ultimo_valor);
            }
        });

        function validarRango() {
            var number = document.getElementById('<%=ddDiasHabilesFechaNav.ClientID%>').value;

            if (number.match(/^-?[0-9]+(\.[0-9]{1,2})?$/)) {
                if (parseInt(number) <= -21 || parseInt(number) >= +21) {
                    alert('El rango es de -20 a +20');
                    document.getElementById('<%=ddDiasHabilesFechaNav.ClientID%>').value = "";
                    return false;
                } else {
                    return true;
                }
            } else {
                if (number === '') {
                    return true;
                } else {
                    alert('La cadena no tiene el formato correcto');
                    document.getElementById('<%=ddDiasHabilesFechaNav.ClientID%>').value = "";
                }

            }
        }

        function validarRangoRescate() {
            var number = document.getElementById('<%=ddNumeroFechaRescate.ClientID%>').value;
            if (number.match(/^-?[0-9]+(\.[0-9]{1,2})?$/)) {
                if (parseInt(number) <= -21 || parseInt(number) >= +21) {
                    alert('El rango es de -20 a +20');
                    document.getElementById('<%=ddNumeroFechaRescate.ClientID%>').value = "";
                    return false;
                } else {
                    return true;
                }
            } else {
                if (number === '') {
                    return true;
                } else {
                    alert('La cadena no tiene el formato correcto');
                    document.getElementById('<%=ddNumeroFechaRescate.ClientID%>').value = "";
                }
            }
        }

        function validarRangoTC() {
            var number = document.getElementById('<%=ddlNumeroFechaTC.ClientID%>').value;
            if (number.match(/^-?[0-9]+(\.[0-9]{1,2})?$/)) {
                if (parseInt(number) <= -21 || parseInt(number) >= +21) {
                    alert('El rango es de -20 a +20');
                    document.getElementById('<%=ddlNumeroFechaTC.ClientID%>').value = "";
                    return false;
                } else {
                    return true;
                }
            } else {
                if (number === '') {
                    return true;
                } else {
                    alert('La cadena no tiene el formato correcto');
                    document.getElementById('<%=ddlNumeroFechaTC.ClientID%>').value = "";
                }
            }
        }

        function validarRangoSus() {
            var number = document.getElementById('<%=ddNumeroFechaSuscripcion.ClientID%>').value;
            if (number.match(/^-?[0-9]+(\.[0-9]{1,2})?$/)) {
                if (parseInt(number) <= -21 || parseInt(number) >= +21) {
                    alert('El rango es de -20 a +20');
                    document.getElementById('<%=ddNumeroFechaSuscripcion.ClientID%>').value = "";
                    return false;
                } else {
                    return true;
                }
            } else {
                if (number === '') {
                    return true;
                } else {
                    alert('La cadena no tiene el formato correcto');
                    document.getElementById('<%=ddNumeroFechaSuscripcion.ClientID%>').value = "";
                }
            }
        }

        function validarRangoTcSus() {
            var number = document.getElementById('<%=ddNumeroTCSuscripciones.ClientID%>').value;
            if (number.match(/^-?[0-9]+(\.[0-9]{1,2})?$/)) {
                if (parseInt(number) <= -21 || parseInt(number) >= +21) {
                    alert('El rango es de -20 a +20');
                    document.getElementById('<%=ddNumeroTCSuscripciones.ClientID%>').value = "";
                    return false;
                } else {
                    return true;
                }
            } else {
                if (number === '') {
                    return true;
                } else {
                    alert('La cadena no tiene el formato correcto');
                    document.getElementById('<%=ddNumeroTCSuscripciones.ClientID%>').value = "";
                }
            }
        }

        function validarRangoNavSus() {
            var number = document.getElementById('<%=ddNumeroFechaNavSuscripciones.ClientID%>').value;
            if (number.match(/^-?[0-9]+(\.[0-9]{1,2})?$/)) {
                if (parseInt(number) <= -21 || parseInt(number) >= +21) {
                    alert('El rango es de -20 a +20');
                    document.getElementById('<%=ddNumeroFechaNavSuscripciones.ClientID%>').value = "";
                    return false;
                } else {
                    return true;
                }
            } else {
                if (number === '') {
                    return true;
                } else {
                    alert('La cadena no tiene el formato correcto');
                    document.getElementById('<%=ddNumeroFechaNavSuscripciones.ClientID%>').value = "";
                }
            }
        }

        function validarRangoCanje() {
            var number = document.getElementById('<%=ddlNumeroTCCanje.ClientID%>').value;
            if (number.match(/^-?[0-9]+(\.[0-9]{1,2})?$/)) {
                if (parseInt(number) <= -21 || parseInt(number) >= +21) {
                    alert('El rango es de -20 a +20');
                    document.getElementById('<%=ddlNumeroTCCanje.ClientID%>').value = "";
                    return false;
                } else {
                    return true;
                }
            } else {
                if (number === '') {
                    return true;
                } else {
                    alert('La cadena no tiene el formato correcto');
                    document.getElementById('<%=ddlNumeroTCCanje.ClientID%>').value = "";
                }
            }
        }

        function validarRangoNavCanje() {
            var number = document.getElementById('<%=ddNumeroFechaNavCanje.ClientID%>').value;
            if (number.match(/^-?[0-9]+(\.[0-9]{1,2})?$/)) {
                if (parseInt(number) <= -21 || parseInt(number) >= +21) {
                    alert('El rango es de -20 a +20');
                    document.getElementById('<%=ddNumeroFechaNavCanje.ClientID%>').value = "";
                    return false;
                } else {
                    return true;
                }
            } else {
                if (number === '') {
                    return true;
                } else {
                    alert('La cadena no tiene el formato correcto');
                    document.getElementById('<%=ddNumeroFechaNavCanje.ClientID%>').value = "";
                }
            }
        }

        function validarRangoFechaCanje() {
            var number = document.getElementById('<%=txtNumeroFechaCanje.ClientID%>').value;
            if (number.match(/^-?[0-9]+(\.[0-9]{1,2})?$/)) {
                if (parseInt(number) <= -21 || parseInt(number) >= +21) {
                    alert('El rango es de -20 a +20');
                    document.getElementById('<%=txtNumeroFechaCanje.ClientID%>').value = "";
                    return false;
                } else {
                    return true;
                }
            } else {
                if (number === '') {
                    return true;
                } else {
                    alert('La cadena no tiene el formato correcto');
                    document.getElementById('<%=txtNumeroFechaCanje.ClientID%>').value = "";
                }
            }
        }

        function soloNumerosNP(e) {
            key = e.keyCode || e.which;
            tecla = String.fromCharCode(key).toLowerCase();
            letras = "-0123456789";
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

        function soloNumerosHoras(e) {
            key = e.keyCode || e.which;
            tecla = String.fromCharCode(key).toLowerCase();
            letras = "0123456789:";
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
                return CheckTxtEmpty()
            }
        }
        function CheckTxtEmpty() {
            if ($('#<%=ddlRutdeFondo.ClientID%>').val() == "") {
                alert("Fondo es un obligatorio")
                return false;
            } else {
                if ($('#<%=txtNemotecnico.ClientID %>').val() == "") {
                    alert("El nemotécnico es un campo obligatorio")
                    return false;
                } else {
                    if ($('#<%=txtNombreSerie.ClientID%>').val() == "") {
                        alert("Nombre Corto de Serie es obligatorio")
                        return false;
                    } else {
                        if ($('#<%=ddlMonedaSerie.ClientID%>').val() == "") {
                            alert("Debe seleccionar una opción válida en Moneda Serie")
                            return false;
                        } else {
                            if ($('#<%=ddlTipoRemuneracion.ClientID%>').val() == "") {
                                alert("Debe seleccionar un Tipo de Remuneración")
                                return false;
                            } else {
                                if ($('#<%=ddlInversionistaCalificado.ClientID%>').val() == "") {
                                    alert("Debe seleccionar una opción válida en Inversionista Calificado")
                                    return false;
                                } else {
                                    if ($('#<%=ddlExclusivoMAM.ClientID%>').val() == "") {
                                        alert("Debe seleccionar una opción válida en Exclusivo MAM")
                                        return false;
                                    } else {
                                        if ($('#<%=ddlMonedaDeLimite.ClientID%>').val() == "") {
                                            alert("Debe seleccionar una opción válida en Moneda Limite")
                                            return false;
                                        } else {
                                            if ($('#<%=txtMontoMinimo.ClientID%>').val() == "") {
                                                alert("Debe escribir un Monto Mínimo válido")
                                                return false;
                                            } else {
                                                if ($('#<%=txtMontoMaximo.ClientID%>').val() == "") {
                                                    alert("Debe escribir un Monto Máximo válido")
                                                    return false;
                                                } else {
                                                    if ($('#<%=txtGrupoCompatible.ClientID%>').val() == "") {
                                                        alert("Debe escribir un Grupo Compatible válido")
                                                        return false;
                                                    } else {
                                                        if ($('#<%=ddlContratoDistribuicion.ClientID%>').val() == "") {
                                                            alert("Debe seleccionar una opción válida en Contrato Distribución")
                                                            return false;
                                                        } else {
                                                            if ($('#<%=ddlDomiciliadoExtranjero.ClientID%>').val() == "") {
                                                                alert("Debe seleccionar una opción válida en Domiciliado Extranjero")
                                                                return false;
                                                            } else {
                                                                if ($('#<%=ddlCanjeMandatorio.ClientID%>').val() == "") {
                                                                    alert("Debe seleccionar una opción válida en Canje Mandatorio")
                                                                    return false;
                                                                } else {
                                                                    if ($('#<%=ddlFondoRescatable.ClientID%>').val() == "") {
                                                                        alert("Debe seleccionar una opción válida en Fondo Rescatable")
                                                                        return false;
                                                                    } else {
                                                                        if ($('#<%=ddlFijacionNav.ClientID%>').val() == "") {
                                                                            alert("Debe seleccionar una opción válida en Fijación Nav (Rescates)")
                                                                            return false;
                                                                        } else {
                                                                            if ($('#<%=ddlfijacionSuscripcion.ClientID%>').val() == "") {
                                                                                alert("Debe seleccionar una opción válida en Fijación Nav (Suscripciones)")
                                                                                return false;
                                                                            } else {
                                                                                if ($('#<%=ddlFijacionNavCanje.ClientID%>').val() == "") {
                                                                                    alert("Debe seleccionar una opción válida en Fijación Nav (Canjes)")
                                                                                    return false;
                                                                                } else {
                                                                                    return true;
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
                    }
                }
            }
        }



        function enableDisableButtons(newValue) {
            var btnModificar = document.getElementById('<%=BtnModificar.ClientID%>');
            btnModificar.disabled = newValue;
            var btnEliminar = document.getElementById('<%=BtnEliminar.ClientID%>');
            btnEliminar.disabled = newValue;
        }
        $(function () {
            $("#<%=ddlRutdeFondo.ClientID%>").change(function () {
                var checkStatus = $('#<%=txtEstadoCambio.ClientID%>').val()
                if (checkStatus == "") {
                    var index = $(this).prop("selectedIndex");
                    $('#<%=ddlNombreRutModal.ClientID%>').prop("selectedIndex", index);
                    $('#<%=txtEstadoCambio.ClientID%>').val("1")
                    $('#<%=ddlNombreRutModal.ClientID%>').trigger('change');
                    $('#<%=txtEstadoCambio.ClientID%>').val("")
                }
            });
        });

        $(function () {
            $('#<%=ddlNombreRutModal.ClientID%>').change(function () {
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


        function volver() {
            if (!confirm('¿Seguro que desea volver al menu principal?')) {
                return false;
            } else {
                return true;
            }
        }

        function mayusculas() {
            var x = document.getElementById("<%=txtNemotecnico.ClientID%>");
            var y = document.getElementById("<%=txtNombreSerie.ClientID%>");
            y.value = y.value.toUpperCase();
            x.value = x.value.toUpperCase();
        }

        function validarMontos() {
            var minimo1 = document.getElementById('<%=txtMontoMinimo.ClientID%>').value.replace(/\./g, '');
            var maximo1 = document.getElementById('<%=txtMontoMaximo.ClientID%>').value.replace(/\./g, '');    

            var minimo = BigInt(minimo1);
            var maximo = BigInt(maximo1);      

            if (maximo >= minimo || maximo == 0) {
                return true;
            } else {
                if (maximo < minimo) {
                    alert("El Monto Máximo debe ser mayor o igual al Monto Mínimo o ser igual a 0");
                    document.getElementById('<%=txtMontoMaximo.ClientID%>').value = "";
                    return false;
                } else {
                    return false;
                }
            }
        }

        function validarMontosMinimo() {
            var minimo1 = document.getElementById('<%=txtMontoMinimo.ClientID%>').value.replace(/\./g, '');
            var maximo1 = document.getElementById('<%=txtMontoMaximo.ClientID%>').value.replace(/\./g, '');

            var minimo = BigInt(minimo1);
            var maximo = BigInt(maximo1);

            if (maximo >= minimo || maximo == 0) {
                return true;
            } else {

                if (maximo < minimo) {
                    alert("El Monto Máximo debe ser mayor o igual al Monto Mínimo o ser igual a 0");
                    document.getElementById('<%=txtMontoMinimo.ClientID%>').value = "";
                    return false;
                } else {
                    return false;
                }
            }
        }



        function validatetime() {
            var strval = $('#<%=txtHorarioCorte.ClientID%>').val()
            var strval1;

            //minimum lenght is 6. example 1:2 AM
            if (strval.length < 4) {
                alert("Hora inválida, el formato de hora debe ser HH:MM");
                document.getElementById('<%=txtHorarioCorte.ClientID%>').value = "";
                return false;
            }

            //Maximum length is 8. example 10:45 AM
            if (strval.lenght > 4) {
                alert("Hora inválida, el formato de hora debe ser HH:MM");
                document.getElementById('<%=txtHorarioCorte.ClientID%>').value = "";
                return false;
            }


            strval = trimAllSpace(strval);


            strval1 = strval.substring(0, strval.length);


            strval = strval1;

            var pos1 = strval.indexOf(':');

            if (pos1 < 0) {
                alert("Falta un : entre la hora y los minutos");
                document.getElementById('<%=txtHorarioCorte.ClientID%>').value = "";
                return false;
            }
            else if (pos1 > 2 || pos1 < 1) {
                alert("Hora inválida, el formato de hora debe ser HH:MM");
                document.getElementById('<%=txtHorarioCorte.ClientID%>').value = "";
                return false;
            }
            else if (pos1 < 2 || pos1 > 3) {
                alert("Hora inválida, el formato de hora debe ser HH:MM");
                document.getElementById('<%=txtHorarioCorte.ClientID%>').value = "";
                return false;
            }
            //Checking hours
            var horval = trimString(strval.substring(0, pos1));

            if (horval == -100) {
                alert("La hora debe contener un valor entero");
                document.getElementById('<%=txtHorarioCorte.ClientID%>').value = "";
                return false;
            }

            if (horval > 23) {
                alert("La hora no puede ser mayor que 23");
                document.getElementById('<%=txtHorarioCorte.ClientID%>').value = "";
            return false;
        }
        else if (horval < 0) {
            alert("La hora no puede ser menor que 0");
            document.getElementById('<%=txtHorarioCorte.ClientID%>').value = "";
                return false;
            }
            //Completes checking hours.

            //Checking minutes.
            var minval = trimString(strval.substring(pos1 + 1, pos1 + 3));

            if (minval == -100) {
                alert("Los minutos solo reciben valores entero (0-59)");
                document.getElementById('<%=txtHorarioCorte.ClientID%>').value = "";
                return false;
            }

            if (minval > 59) {
                alert("Los minutos no pueden ser mayor a 59");
                document.getElementById('<%=txtHorarioCorte.ClientID%>').value = "";
            return false;
        }
        else if (minval < 0) {
            alert("Los minutos no pueden ser menor a 0");
            document.getElementById('<%=txtHorarioCorte.ClientID%>').value = "";
                return false;
            }

            //Checking minutes completed.		

            //Checking one space after the mintues	
            //minpos = pos1 + minval.length + 1;
            //if(strval.charAt(minpos) != ' ')
            //{
            //	alert("Invalid time. Space missing after minute. Time should have HH:MM AM/PM format.");
            //	return false;
            //}

            return true;

        }

        function trimAllSpace(str) {
            var str1 = '';
            var i = 0;

            while (i != str.length) {
                if (str.charAt(i) != ' ')
                    str1 = str1 + str.charAt(i);


                i++;
            }

            return str1;
        }

        function trimString(str) {
            var str1 = '';
            var i = 0;
            while (i != str.length) {
                if (str.charAt(i) != ' ')
                    str1 = str1 + str.charAt(i);

                i++;
            }

            var retval = IsNumeric(str1);

            if (retval == false)
                return -100;
            else
                return str1;


        }

        function IsNumeric(strString) {
            var strValidChars = "0123456789";
            var strChar;
            var blnResult = true;
            //var strSequence = document.frmQuestionDetail.txtSequence.value;

            //  test strString consists of valid characters listed above
            if (strString.length == 0) return false;
            for (i = 0; i < strString.length && blnResult == true; i++) {
                strChar = strString.charAt(i);
                if (strValidChars.indexOf(strChar) == -1) {
                    blnResult = false;
                }
            }
            return blnResult;
        }

    </script>
</asp:Content>

