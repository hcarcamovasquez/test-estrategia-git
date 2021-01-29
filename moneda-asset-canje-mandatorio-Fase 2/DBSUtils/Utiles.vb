Imports System.Web.UI.WebControls
Imports DTO
Imports Negocio
Imports System.Web
Imports System.Reflection
Imports System.Text.RegularExpressions
Imports System.IO
Imports Xceed.Words.NET
Imports Xceed.Document.NET

'Imports ListItem = System.Web.UI.WebControls.ListItem

Public Class Utiles


    Public Shared Function Version() As String
        Const CONST_VERSION As String = "1.0.0"
        Return Assembly.GetExecutingAssembly().GetName().Version.ToString()
    End Function

    Public Shared Sub CargarMonedas(ddl As DropDownList, Optional sTexto As String = "Seleccione una opción")
        Dim lista As List(Of MonedasDTO) = New List(Of MonedasDTO)

        lista = TraerMonedas()
        ddl.Items.Clear()

        ddl.DataSource = lista
        ddl.DataMember = "MNCodigo"
        ddl.DataValueField = "MNCodigo"
        ddl.DataBind()

        ddl.Items.Insert(0, New ListItem(sTexto, String.Empty))
    End Sub

    Public Shared Function TraerMonedas() As List(Of MonedasDTO)
        Dim negocio As MonedaNegocio = New MonedaNegocio
        Dim lista As List(Of MonedasDTO) = New List(Of MonedasDTO)

        lista = negocio.SelectAll()

        Return lista

    End Function

    Public Shared Function GetFondo(rutFondo As String) As FondoDTO
        Dim NegocioFondo As FondosNegocio = New FondosNegocio
        Dim fondo As FondoDTO = New FondoDTO()

        fondo.Rut = rutFondo
        Return NegocioFondo.GetFondo(fondo)

    End Function

    Public Shared Function splitCharByComma(texto As String) As EstructuraFechasDto
        Dim desNavC As String()
        Dim retorno As EstructuraFechasDto = New EstructuraFechasDto
        If InStr(texto, ",") = 0 Then
            texto += ","
        End If
        desNavC = texto.Split(New Char() {","c})

        retorno.DesdeQueFecha = desNavC(0)
        If desNavC(1) = "" Then
            retorno.DiasASumar = 0
        Else
            retorno.DiasASumar = Integer.Parse(desNavC(1))
        End If

        Return retorno
    End Function

    Public Shared Function SetNumberPointToDouble(text As String) As Double
        Dim retorno As Double = 0

        Dim numberText = text.Replace(".", "")
        If IsNumeric(numberText) Then
            retorno = Double.Parse(numberText)

        End If

        Return retorno
    End Function

    Public Shared Function SetToCapitalizedNumber(number As Double) As String

        If (number.ToString.Contains(",")) Then
            Dim strArr = number.ToString.Split(",")
            Dim numberWithPoints As String = String.Format("{0:N0}", Double.Parse(strArr(0)))
            Dim numberDecimal As String = strArr(1)

            Return numberWithPoints + "," + numberDecimal
        Else
            Return String.Format("{0:N0}", Double.Parse(number))
        End If

    End Function

    Public Shared Function formateConSeparadorDeMiles(numero As Decimal, numeroDeDecimal As Integer) As String
        Return String.Format("{0:N" + numeroDeDecimal.ToString() + "}", Decimal.Parse(numero))
    End Function

    Public Shared Function HtmlDecode(s As String) As String
        Return HttpUtility.HtmlDecode(s)
    End Function


    Public Shared Function calcularMontoCLP(cuotas As String, nav As String, tcObservado As String) As String
        If cuotas <> "" And nav <> "" And tcObservado <> "" Then
            Return formatearMontoCLP(cuotas * nav * tcObservado)
        Else
            Return "0"
        End If
    End Function

    Public Shared Function calcularMontoCLP(cuotas As String, navCLP As String) As String
        If cuotas <> "" And navCLP <> "" Then
            Return formatearMontoCLP(cuotas * navCLP)
        Else
            Return "0"
        End If
    End Function

    Public Shared Function formatearMontoCLP(valor As Object) As String
        Return String.Format("{0:N0}", valor)
    End Function

    Public Shared Function calcularMonto(cuotas As String, nav As String, moneda As String) As String
        If (IsNumeric(cuotas) AndAlso IsNumeric(nav)) Then
            Return formatearMonto(cuotas * nav, moneda)
        Else
            Return ""
        End If
    End Function

    Public Shared Function calcularNAVCLP(tcObservado As String, nav As String) As String
        If nav <> "" And tcObservado <> "" Then
            Return formatearNAVCLP(tcObservado * nav)
        Else
            Return "0"
        End If
        ' Math.Round(tcObservado * nav, MidpointRounding.ToEven))
    End Function

    Public Shared Function formatearMonto(valor As Object, moneda As String) As String
        Dim strFormat As String
        If moneda <> "CLP" Then
            strFormat = "{0:N2}"
        Else
            strFormat = "{0:N0}"
        End If
        Return String.Format(strFormat, valor)
    End Function

    Public Shared Function formatearNAVCLP(valorNavClp As String) As String
        Return String.Format("{0:N4}", Math.Round(Double.Parse(valorNavClp), 4))
    End Function

    Public Shared Function formatearNAV(Valor As Object) As String ' String.Format("{0:N4}", ValoresCuotaActualizado.Valor)
        Return String.Format("{0:N6}", Math.Round(Double.Parse(Valor), 6))
    End Function

    Public Shared Function formatearDiferencia(valor As Object, moneda As String) As String
        Return formatearMonto(valor, moneda)
    End Function

    Public Shared Function formatearDiferenciaCLP(valor As Object) As String
        Return formatearMontoCLP(valor)
    End Function

    Public Shared Function formatearTC(valor As Object) As String
        Return formatearNAV(valor)
    End Function

    Public Shared Function fncSoloDiasHabiles(serie As FondoSerieDTO) As Integer
        Dim SoloDiasHabiles As Integer

        If serie.SoloDiasHabilesFechaNavSuscripciones Then
            SoloDiasHabiles = 1
        Else
            SoloDiasHabiles = 0
        End If

        Return SoloDiasHabiles
    End Function

    Public Shared Function getDiaHabilSiguiente(fechaSolicitud As Date, monedaPago As String) As Date
        Dim negocioFechas As FechasNegocio = New FechasNegocio
        Dim fecha As FechasDTO = New FechasDTO
        Dim negocioRelacion As RelacionMonedaPaisNegocio = New RelacionMonedaPaisNegocio
        Dim calendarioPais As String

        calendarioPais = negocioRelacion.SelectOne(monedaPago)

        fecha.Anno = Year(fechaSolicitud)
        fecha.Mes = Month(fechaSolicitud)
        fecha.Dia = Day(fechaSolicitud)

        fecha.DF_PAIS = calendarioPais

        fechaSolicitud = negocioFechas.getHabilSiguiente(fecha)

        negocioFechas = Nothing
        fecha = Nothing

        Return fechaSolicitud
    End Function

    Public Shared Function SumaDiasAFechas(monedaPago As String, fechaSolicitud As Date, CantidadDias As Integer, DiasCorridos As Integer) As Date
        Dim negocioFechas As FechasNegocio = New FechasNegocio
        Dim fecha As FechasDTO = New FechasDTO
        Dim negocioRelacion As RelacionMonedaPaisNegocio = New RelacionMonedaPaisNegocio
        Dim calendarioPais As String

        calendarioPais = negocioRelacion.SelectOne(monedaPago)

        fecha.Anno = Year(fechaSolicitud)
        fecha.Mes = Month(fechaSolicitud)
        fecha.Dia = Day(fechaSolicitud)
        fecha.DF_PAIS = calendarioPais

        fecha.DiasASumar = CantidadDias
        fecha.DiasCorridos = DiasCorridos

        fechaSolicitud = negocioFechas.SumaDiasAFecha(fecha)

        negocioFechas = Nothing
        fecha = Nothing

        Return fechaSolicitud

    End Function

    Public Shared Function esFechaHabil(monedaPago As String, fechaSolicitud As Date) As Boolean
        Dim negocioFechas As FechasNegocio = New FechasNegocio
        Dim fecha As FechasDTO = New FechasDTO
        Dim negocioRelacion As RelacionMonedaPaisNegocio = New RelacionMonedaPaisNegocio
        Dim calendarioPais As String
        Dim strFechaHabil As String = ""

        calendarioPais = negocioRelacion.SelectOne(monedaPago)

        fecha.Anno = Year(fechaSolicitud)
        fecha.Mes = Month(fechaSolicitud)
        fecha.Dia = Day(fechaSolicitud)
        fecha.DF_PAIS = calendarioPais
        fecha.DiasASumar = 0
        fecha.DiasCorridos = 0

        strFechaHabil = negocioFechas.ValidaDiaHabil(fecha)

        negocioFechas = Nothing
        fecha = Nothing

        If strFechaHabil = "Festivo" Or strFechaHabil = "No_Habil" Or strFechaHabil = "error" Then
            Return False
        Else
            Return True
        End If

    End Function

End Class
