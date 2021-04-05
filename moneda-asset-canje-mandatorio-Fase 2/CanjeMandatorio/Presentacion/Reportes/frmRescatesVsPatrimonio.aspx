<%@ Page Title="" Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false" CodeFile="frmRescatesVsPatrimonio.aspx.vb" Inherits="Presentacion_Mantenedores_frmRescatesVsPatrimonio" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    
    <h2 class="TdRedondeado titleMant">Reporte Rescates Versus Patrimonio</h2>

       <div class="card p-4 jumbotron">
        <div class="row">
            <!-- LISTA RUT FONDO-->
            <div class="col-md-3">
                <asp:Label runat="server" ID="rutfondo">Fondo</asp:Label>
                <asp:DropDownList ID="ddlListaRutFondo" CssClass="form-control js-select2-rut" runat="server" AutoPostBack="true" />
            </div>
            <!-- FECHA DESDE -->
            <div class="col-md-3">
                <asp:Label runat="server" ID="Label2">Fecha de ejecución</asp:Label>
                <div class="input-group">
                    <asp:TextBox ID="txtFechaEjecucion" runat="server" CssClass="form-control datepicker" ReadOnly="True"></asp:TextBox>
                    <asp:LinkButton ID="lnkbtnFechaEjecucion" class="btn btn-moneda" runat="server"
                        OnClientClick="return clickCalendar('txtFechaEjecucion')"><i class="far fa-calendar-alt"></i></asp:LinkButton>

                    <asp:LinkButton ID="BtnLimpiarFechaHasta" Text="" class="btn btn-secondary ml-1" runat="server"
                        OnClientClick="return limpiarCalendar('txtFechaEjecucion')"><i class="far fa-trash-alt"></i></asp:LinkButton>
                </div>
            </div>

            <div class="col-md-3">
                <asp:Label runat="server" ID="lblFechaNav">Fecha NAV</asp:Label>
                <div class="input-group">
                    <asp:TextBox ID="txtFechaNav" runat="server" CssClass="form-control datepicker" ReadOnly="True"></asp:TextBox>
                    <asp:LinkButton ID="lnkbtnFechaNav" class="btn btn-moneda" runat="server"
                        OnClientClick="return clickCalendar('txtFechaNav')"><i class="far fa-calendar-alt"></i></asp:LinkButton>

                    <asp:LinkButton ID="LinkButton8" Text="" class="btn btn-secondary ml-1" runat="server"
                        OnClientClick="return limpiarCalendar('txtFechaNav')"><i class="far fa-trash-alt"></i></asp:LinkButton>
                </div>                
            </div>

            <div class="col-md-3">
                <asp:Label runat="server" ID="Label1" Visible="False">Fecha de Patrimonio</asp:Label>
                <div class="input-group">
                    <asp:TextBox ID="txtFechaPatrimonio" runat="server" CssClass="form-control datepicker" ReadOnly="True" Visible="False"></asp:TextBox>
                    <asp:LinkButton ID="lnkbtnPatrimonio" class="btn btn-moneda" runat="server"
                        OnClientClick="return clickCalendar('txtFechaPatrimonio')" Visible="False"><i class="far fa-calendar-alt"></i></asp:LinkButton>

                    <asp:LinkButton ID="LinkButton2" Text="" class="btn btn-secondary ml-1" runat="server"
                        OnClientClick="return limpiarCalendar('txtFechaPatrimonio')" Visible="False"><i class="far fa-trash-alt"></i></asp:LinkButton>
                </div> 
                <asp:Label runat="server" ID="Label3">&nbsp;</asp:Label>
                <div class="input-group">
                    <asp:Button ID="btnGenerarInforme" Text="Generar Informe" class="btn btn-moneda" runat="server"
                        OnClientClick="return deshabilitaBoton();" />
                </div>

            </div>

            <!-- FECHA DESDE -->
            <div class="col-md-3">
                <asp:Label runat="server" ID="Label5">Fecha Desde</asp:Label>
                <div class="input-group">
                    <asp:TextBox ID="txtFechaDesde" runat="server" CssClass="form-control datepicker" ReadOnly="True"></asp:TextBox>
                    <asp:LinkButton ID="LinkButton3" class="btn btn-moneda" runat="server"
                        OnClientClick="return clickCalendar('txtFechaDesde')"><i class="far fa-calendar-alt"></i></asp:LinkButton>

                    <asp:LinkButton ID="LinkButton4" Text="" class="btn btn-secondary ml-1" runat="server"
                        OnClientClick="return limpiarCalendar('txtFechaDesde')"><i class="far fa-trash-alt"></i></asp:LinkButton>
                </div>
            </div>

            <div class="col-md-3">
                <asp:Label runat="server" ID="Label6">Fecha de Hasta</asp:Label>
                <div class="input-group">
                    <asp:TextBox ID="txtFechaHasta" runat="server" CssClass="form-control datepicker" ReadOnly="True"></asp:TextBox>
                    <asp:LinkButton ID="LinkButton5" class="btn btn-moneda" runat="server"
                        OnClientClick="return clickCalendar('txtFechaHasta')"><i class="far fa-calendar-alt"></i></asp:LinkButton>

                    <asp:LinkButton ID="LinkButton6" Text="" class="btn btn-secondary ml-1" runat="server"
                        OnClientClick="return limpiarCalendar('txtFechaHasta')"><i class="far fa-trash-alt"></i></asp:LinkButton>
                </div>
            </div>
        </div>

        <!-- BOTONES BUSCAR LIMPIAR Y CREAR -->
        <div class="row text-center mt-5 p-3 border-bottom">
            <div class="col-md-12">
                <!-- BOTÓN BUSCAR -->
                
               <asp:Button ID="BtnBuscar" Text="Buscar" class="btn btn-moneda" runat="server" />
                <asp:Button ID="btnLimpiarFrm" Text="Limpiar" class="btn btn-secondary" runat="server" />
            </div>
        </div>

        <asp:HiddenField ID="txtAccionHidden" runat="server" />

        <!-- TABLA DE RESULTADOS -->
        <h5 class="mt-3 text-secondary"><i class="fas fa-file-invoice fa-sm"></i>Resultado de la búsqueda</h5>
        <div class="table-responsive card mt-4 p-3">
            <asp:GridView
                ID="grvEjecuciones"
                runat="server"
                CssClass="table table-bordered table-hover table-sm gvv"
                HeaderStyle-BackColor=""
                HeaderStyle-Font-Size=""
                RowStyle-Font-Size="Small"
                AutoGenerateColumns="false"
                AllowSorting="false">
                <Columns>
                    <asp:BoundField DataField="Id" HeaderText="ID" />
                    <asp:BoundField DataField="FechaEjecucion" HeaderText="Fecha Ejecucion" DataFormatString="{0:dd/MM/yyyy}"/>
                    <asp:BoundField DataField="FnRut" HeaderText="Rut Fondo" />
                    <asp:BoundField DataField="NombreFondo" HeaderText="Nombre Fondo" />
                    <asp:BoundField DataField="Descripcion" HeaderText="Descripcion"  />
                    <asp:BoundField DataField="Estado" HeaderText="Estado" />

                </Columns>
            </asp:GridView>
        </div>
     </div>

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
        $(document).ready(function () {

            $("[id*=txtFechaEjecucion]").datepicker();
            $("[id*=txtFechaPatrimonio]").datepicker();
            $("[id*=txtFechaNav]").datepicker();
            $("[id*=txtFechaDesde]").datepicker();
            $("[id*=txtFechaHasta]").datepicker();

           
        })

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

        function deshabilitaBoton() {
            var btn = $("#<%=btnGenerarInforme.ClientID %>");
            btn.hide(); 

            return true; 
        }

    </script>

</asp:Content>