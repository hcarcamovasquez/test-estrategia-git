<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Default.aspx.vb" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="<%=ResolveUrl("~/Img/favicon.ico")%>" type="image/vnd.microsoft.icon" rel="icon" />
    <title>Moneda - Canje Mandatorio</title>

    <link href="<%=ResolveUrl("~/Style/bootstrap.min.css")%>" rel="stylesheet" />
    <link href="<%=ResolveUrl("~/Style/LoginStyle.css")%>" rel="stylesheet" />
    <link href="<%=ResolveUrl("~/Style/estilos.css")%>" rel="stylesheet" />
    <!-- En el area del HEAD -->
    <%--<link href='http://fonts.googleapis.com/css?family=Snowburst+One' rel='stylesheet' type='text/css'>--%>

</head>
<body>
    <div class="container">
        <div class="row">
            <div class="col-md-12"> <div class="text-center mt-5">   
                <img src="~/../Img/logo_Moneda_blanco.png" class="logoLogin" /></div>
                <div class="cuadro p-4 mx-auto mt-5">
                    <div class="text-center">

                        <div class="text-center mt-2">
                            <h2 class="mb-4">Canje Mandatorio</h2>
                            <h4 class="TexFormula text-uppercase font-weight-bold">Iniciar Sesión</h4>
                            <p class="TextGrisB">Ingresa con tu usuario y contraseña:</p>
                        </div>

                    </div>
                    <div class="p-2">
                        <form id="form1" runat="server">
                            <div>
                                <div class="form-group">
                                    <label class="sr-only">Usuario</label>
                                    <asp:TextBox ID="txtUser" runat="server" CssClass="form-control TextCaja" placeholder="Usuario..."></asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <label class="sr-only">Contraseña</label>
                                    <asp:TextBox ID="txtPassword" TextMode="Password" runat="server" CssClass="form-control TextCaja" placeholder="Contraseña..."  autocomplete="new-password"></asp:TextBox>
                                </div>
                                <asp:Button ID="btnInciarSesion" runat="server" CssClass="btn btn-primary btn-block btnNormal" Text="Iniciar Sesión" OnClick="btnInciarSesion_Click"/>
                                <div class="text-center">
                                    <asp:Label ID="lblMensaje" CssClass="" runat="server" ForeColor="Red"></asp:Label>
                                </div>
                                <asp:HiddenField ID="counter" runat="server" />
                            </div>
                        </form>
                    </div>
                </div>
            </div>
        </div>
    </div>



    <!-- Optional JavaScript -->
    <!-- jQuery first, then Popper.js, then Bootstrap JS -->
        <script src="<%=ResolveUrl("~/Scripts/jquery-3.4.1.js")%>"></script>	
        <script src="<%=ResolveUrl("~/Scripts/popper.min.js")%>"></script>
        <script src="<%=ResolveUrl("~/Scripts/bootstrap.min.js")%>"></script>

</body>
</html>
