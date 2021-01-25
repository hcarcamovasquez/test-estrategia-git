Imports System.Web.UI.WebControls
Imports DTO
Imports Negocio
Imports System.Web
Imports System.Reflection
Imports System.Text.RegularExpressions
Imports System.IO

'Imports ListItem = System.Web.UI.WebControls.ListItem

Public Class Utiles

    Private Shared Property dirOutputDoc As String
    Private Shared Property dirPlantillasWord As String


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

    Public Shared Sub GenerarCartas(fijaciones As List(Of FijacionDTO))
        Dim configurationAppSettings As New System.Configuration.AppSettingsReader()
        'Dim dirOutputDoc As String
        Dim DocumentName As String = ""

        dirPlantillasWord = DirectCast(configurationAppSettings.GetValue("PlantillasWord", GetType(System.String)), String)
        dirOutputDoc = DirectCast(configurationAppSettings.GetValue("OutputDoc", GetType(System.String)), String)

        ' fijaciones.GroupBy(Function(fijacion) fijacion.RazonSocial)

        'si la selección contiene transacciones del mismo tipo (rescate, suscripciones o canje), 
        'misma serie y fondo, entonces el sistema la dejará en una sola instrucción

        Dim fijacionesCanje = fijaciones.Where(Function(f) f.TipoTransaccion.ToLower().Trim().Equals("canje")).ToList()
        'Dim fijacionesSuscripciones = fijaciones.Where(Function(f) f.TipoTransaccion.ToLower().Trim().Equals("suscripcion")).ToList()
        Dim FijacionesAportes = fijaciones.Where(Function(f) f.TipoTransaccion.ToLower().Trim().Equals("rescate") Or f.TipoTransaccion.ToLower().Trim().Equals("suscripcion")).ToList()



        '
        'HacerCartaComprobanteDePagoAportes(FijacionesAportes)     ' Terminada 
        '
        HacerInstrucionAlDCVdeAportes(FijacionesAportes)

        'HacerCartaSuscripciones(fijacionesSuscripciones)



        'For Each fijacion As FijacionDTO In fijacionesOrdenadas
        '    Using document = Xceed.Words.NET.DocX.Load(dirPlantillasWord & "2Comprobante de pago aporte.docx")


        '        document.ReplaceText("[COLUMNA D]", fijacion.RazonSocial)
        '        document.ReplaceText("[COLUMNA C]", fijacion.RazonSocial)
        '        document.ReplaceText("[COLUMNA X]", fijacion.RazonSocial)
        '        document.ReplaceText("[COLUMNA Y]", fijacion.RazonSocial)
        '        document.ReplaceText("[COLUMNA F]", fijacion.RazonSocial)
        '        document.ReplaceText("[COLUMNA G]", fijacion.RazonSocial)
        '        document.ReplaceText("[COLUMNA J]", fijacion.RazonSocial)
        '        document.ReplaceText("[COLUMNA K]", fijacion.RazonSocial)
        '        document.ReplaceText("[COLUMNA N]", fijacion.RazonSocial)
        '        document.ReplaceText("[COLUMNA M]", fijacion.RazonSocial)
        '        document.ReplaceText("[COLUMNA O]", fijacion.RazonSocial)
        '        document.ReplaceText("[COLUMNA L]", fijacion.RazonSocial)
        '        document.ReplaceText("[COLUMNA P]", fijacion.RazonSocial)

        '        nombreOutput = dirOutputDoc & getNombreArchivoOutput(fijacion)

        '        If File.Exists(nombreOutput) Then
        '            File.Delete(nombreOutput)
        '        End If

        '        document.SaveAs(nombreOutput)

        '    End Using
        'Next

    End Sub

    Private Shared Sub HacerInstrucionAlDCVdeAportes(fijacionesAportes As List(Of FijacionDTO))
        Dim fijacionesOrdenadas = fijacionesAportes.OrderBy(Function(f As FijacionDTO) f.TipoTransaccion).ThenBy(Function(f As FijacionDTO) f.Nemotecnico).ThenBy(Function(f As FijacionDTO) f.Rut).ToList
        Dim nombreOutput As String

        nombreOutput = dirOutputDoc & "CARTA PARA APORTE DE FONDOS"

        Dim TemplatePath As String = dirPlantillasWord & "Formato carta aporte fondos.docx"
        Dim PlantillaWord = Xceed.Words.NET.DocX.Load(TemplatePath)
        Dim document = Xceed.Words.NET.DocX.Create(nombreOutput)

        Dim tipoTransaccion As String = ""
        Dim nemotecnico As String = ""
        Dim rutFondo As String = ""
        Dim indice As Integer = -1

        Dim listaFijacion As List(Of FijacionDTO) = New List(Of FijacionDTO)
        Dim listaDOcDVC As Dictionary(Of Integer, List(Of FijacionDTO)) = New Dictionary(Of Integer, List(Of FijacionDTO))

        For Each fijacion As FijacionDTO In fijacionesOrdenadas
            If fijacion.TipoTransaccion <> tipoTransaccion OrElse fijacion.Nemotecnico <> nemotecnico OrElse fijacion.Rut <> rutFondo Then
                tipoTransaccion = fijacion.TipoTransaccion
                nemotecnico = fijacion.Nemotecnico
                rutFondo = fijacion.Rut

                indice += 1

                listaDOcDVC.Add(indice, listaFijacion)
                listaFijacion = New List(Of FijacionDTO)

            End If

            listaFijacion.Add(fijacion)


        Next

        indice += 1
        listaDOcDVC.Add(indice, listaFijacion)

        Dim bPrimero As Boolean = True
        Dim numeroTabla As Integer = 0

        For Each pair As KeyValuePair(Of Integer, List(Of FijacionDTO)) In listaDOcDVC
            document.InsertDocument(PlantillaWord, True)
            Dim t = document.Tables(numeroTabla)

            bPrimero = True
            Dim rowPattern = t.Rows(t.Rows.Count() - 1)
            Dim newItem = t.InsertRow(rowPattern, t.RowCount - 1)

            For Each fijacion As FijacionDTO In pair.Value


                rowPattern = t.Rows(t.Rows.Count() - 1)
                newItem = t.InsertRow(rowPattern, t.RowCount - 1)

                If bPrimero Then
                    bPrimero = False
                    document.ReplaceText("[COLUMNA C]", fijacion.TipoTransaccion)
                    document.ReplaceText("[COLUMNA E]", fijacion.FnNombreCorto)       ' Nombre del Fondo.
                    document.ReplaceText("[COLUMNA B]", Date.Now().ToString("dd \de MMM \de yyyy")) ' Fecha generacion del documento 
                    document.ReplaceText("[COLUMNA D]", fijacion.Nemotecnico)        ' Nombre de la serie
                    document.ReplaceText("[COLUMNA G]", fijacion.Rut)        ' Rut del fondo
                    document.ReplaceText("[COLUMNA N]", Date.Now().ToString("dd \de MMM \de yyyy")) ' Fecha generacion del documento 
                    document.ReplaceText("[COLUMNA M]", fijacion.NAV_FIJADO)    ' Valor Cuota Nav 
                End If

                document.ReplaceText("[COLUMNA I]", String.Format("{0}", pair.Key)) ' fijacion.RazonSocial)   ' nombreAportante 
                document.ReplaceText("[COLUMNA J]", String.Format("{0}", pair.Value.Count())) ' fijacion.ApRut)         ' rut del aportante
                document.ReplaceText("[COLUMNA K]", fijacion.Cuotas)            ' cantidad de cuotas

                'document.ReplaceText("[COLUMNA I]", fijacion.fechaPago.ToString("dd-mm-yyyy"))  ' Fecha de Solicitud
                'document.ReplaceText("[COLUMNA Y]", fijacion.fechaPago.ToString("HH:mm"))   ' Hora Solicitud


                'document.ReplaceText("[COLUMNA N]", fijacion.MonedaPago)    'Moneda pago

                'document.ReplaceText("[COLUMNA O]", fijacion.TipoTransaccion)   ' 

                'document.ReplaceText("[COLUMNA P]", fijacion.Monto)             ' monto en la moneda de la transaccion 

                Debug.WriteLine(String.Format("Segundo: {0}, {1}, {2}", fijacion.TipoTransaccion, fijacion.Nemotecnico, fijacion.Rut))

            Next

            rowPattern.Remove()
            numeroTabla += 2

        Next

        If File.Exists(nombreOutput) Then
            File.Delete(nombreOutput)
        End If

        document.SaveAs(nombreOutput)

    End Sub

    Private Shared Sub HacerCartaComprobanteDePagoAportes(fijaciones As List(Of FijacionDTO))
        Dim nombreOutput As String
        ''
        'Algoritmo 
        '0. Para cada registro en Fijaciones 
        '1. Crear documento nuevo con nombre "COMPROBANTE DE PAGO.DOCX"
        '2. abrir template "2Comprobante de pago aporte.dotx"
        '3. Insertar template en documento word nuevo 
        '4. Reesplazar colummnas 
        '5. Guardar documento word nuevo con el nombre "COMPROBANTE DE PAGO.DOCX"
        '6. siguiente registro
        '
        nombreOutput = dirOutputDoc & "COMPROBANTE DE PAGO"

        Dim TemplatePath As String = dirPlantillasWord & "2Comprobante de pago aporte.dotx"
        Dim PlantillaWord = Xceed.Words.NET.DocX.Load(TemplatePath)
        Dim document = Xceed.Words.NET.DocX.Create(nombreOutput)

        For Each fijacion As FijacionDTO In fijaciones

            document.InsertDocument(PlantillaWord, True)

            document.ReplaceText("[COLUMNA D]", fijacion.TipoTransaccion)
            document.ReplaceText("[COLUMNA C]", Date.Now().ToString("dd \de MMM \de yyyy")) ' Fecha generacion del documento 
            document.ReplaceText("[COLUMNA X]", fijacion.fechaPago.ToString("dd-mm-yyyy"))  ' Fecha de Solicitud
            document.ReplaceText("[COLUMNA Y]", fijacion.fechaPago.ToString("HH:mm"))   ' Hora Solicitud
            document.ReplaceText("[COLUMNA F]", fijacion.FnNombreCorto)       ' Nombre del Fondo.
            document.ReplaceText("[COLUMNA G]", fijacion.Nemotecnico)        ' Nombre de la serie
            document.ReplaceText("[COLUMNA J]", fijacion.RazonSocial)   ' nombreAportante 
            document.ReplaceText("[COLUMNA K]", fijacion.ApRut)         ' rut del aportante
            document.ReplaceText("[COLUMNA N]", fijacion.MonedaPago)    'Moneda pago
            document.ReplaceText("[COLUMNA M]", fijacion.NAV_FIJADO)    ' Valor Cuota Nav  
            document.ReplaceText("[COLUMNA O]", fijacion.TipoTransaccion)   ' 
            document.ReplaceText("[COLUMNA L]", fijacion.Cuotas)            ' cantidad de cuotas
            document.ReplaceText("[COLUMNA P]", fijacion.Monto)             ' monto en la moneda de la transaccion 

        Next

        If File.Exists(nombreOutput) Then
            File.Delete(nombreOutput)
        End If

        document.SaveAs(nombreOutput)

    End Sub

    Private Shared Sub HacerCartaSuscripciones(fijaciones As List(Of FijacionDTO))
        Dim nombreOutput As String
        ''
        'Algoritmo 
        '0. Para cada registro en Fijaciones 
        '1. Crear documento nuevo con nombre "COMPROBANTE DE PAGO.DOCX"
        '2. abrir template "2Comprobante de pago aporte.dotx"
        '3. Insertar template en documento word nuevo 
        '4. Reesplazar colummnas 
        '5. Guardar documento word nuevo con el nombre "COMPROBANTE DE PAGO.DOCX"
        '6. siguiente registro
        '
        nombreOutput = dirOutputDoc & "COMPROBANTE DE PAGO"

        Dim TemplatePath As String = dirPlantillasWord & "2Comprobante de pago aporte.dotx"

        Dim PlantillaWord = Xceed.Words.NET.DocX.Load(TemplatePath)

        Dim document = Xceed.Words.NET.DocX.Create(nombreOutput)

        For Each fijacion As FijacionDTO In fijaciones

            document.InsertDocument(PlantillaWord, True)

            document.ReplaceText("[COLUMNA D]", fijacion.TipoTransaccion)
            document.ReplaceText("[COLUMNA C]", Date.Now().ToString("dd \de MMM \de yyyy")) ' Fecha generacion del documento 
            document.ReplaceText("[COLUMNA X]", fijacion.fechaPago.ToString("dd-mm-yyyy"))  ' Fecha de Solicitud
            document.ReplaceText("[COLUMNA Y]", fijacion.fechaPago.ToString("HH:mm"))       ' Hora Solicitud
            document.ReplaceText("[COLUMNA F]", fijacion.FnNombreCorto)                     ' Nombre del Fondo.
            document.ReplaceText("[COLUMNA G]", fijacion.Nemotecnico)                       ' Nombre de la serie
            document.ReplaceText("[COLUMNA J]", fijacion.RazonSocial)                       ' nombreAportante 
            document.ReplaceText("[COLUMNA K]", fijacion.ApRut)                             ' rut del aportante
            document.ReplaceText("[COLUMNA N]", fijacion.MonedaPago)                        'Moneda pago
            document.ReplaceText("[COLUMNA M]", fijacion.NAV_FIJADO)                        ' Valor Cuota Nav  
            document.ReplaceText("[COLUMNA O]", fijacion.TipoTransaccion)                   ' 
            document.ReplaceText("[COLUMNA L]", fijacion.Cuotas)                            ' cantidad de cuotas
            document.ReplaceText("[COLUMNA P]", fijacion.Monto)                             ' monto en la moneda de la transaccion 

        Next

        If File.Exists(nombreOutput) Then
            File.Delete(nombreOutput)
        End If

        document.SaveAs(nombreOutput)


    End Sub

    Private Shared Sub HacerCartaCanje(fijacion As FijacionDTO)
        'Dim nombreOutput As String

        'nombreOutput = dirOutputDoc & getNombreArchivoOutput(fijacion)

        'Using document = Xceed.Words.NET.DocX.Load(dirPlantillasWord & "FORMATO Comprobante Canje_.docx")
        '    document.ReplaceText("[COLUMNA D]", fijacion.RazonSocial)
        '    document.ReplaceText("[COLUMNA C]", fijacion.RazonSocial)
        '    document.ReplaceText("[COLUMNA X]", fijacion.RazonSocial)
        '    document.ReplaceText("[COLUMNA Y]", fijacion.RazonSocial)
        '    document.ReplaceText("[COLUMNA F]", fijacion.RazonSocial)
        '    document.ReplaceText("[COLUMNA G]", fijacion.RazonSocial)
        '    document.ReplaceText("[COLUMNA J]", fijacion.RazonSocial)
        '    document.ReplaceText("[COLUMNA K]", fijacion.RazonSocial)
        '    document.ReplaceText("[COLUMNA N]", fijacion.RazonSocial)
        '    document.ReplaceText("[COLUMNA M]", fijacion.RazonSocial)
        '    document.ReplaceText("[COLUMNA O]", fijacion.RazonSocial)
        '    document.ReplaceText("[COLUMNA L]", fijacion.RazonSocial)
        '    document.ReplaceText("[COLUMNA P]", fijacion.RazonSocial)

        '    If File.Exists(nombreOutput) Then
        '        File.Delete(nombreOutput)
        '    End If

        '    document.SaveAs(nombreOutput)

        'End Using
    End Sub

    Private Shared Function getNombreArchivoOutput(fijacion As FijacionDTO) As String
        Dim nombreNuevo As String

        nombreNuevo = fijacion.RazonSocial & Date.Now().ToString("ddMMyyyy") & ".docx"

        Return nombreNuevo
    End Function
End Class

Public Class EstructuraDCV
    Public Property index As Integer
    Public Property listaFijaciones As List(Of FijacionDTO)

    Public Sub New()
        listaFijaciones = New List(Of FijacionDTO)
    End Sub


End Class
