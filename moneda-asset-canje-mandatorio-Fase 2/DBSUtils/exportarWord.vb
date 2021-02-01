Imports System.IO
Imports System.Text
Imports System.Text.RegularExpressions
Imports Xceed.Words.NET
Imports Xceed.Document.NET
Imports DTO
Imports Ionic.Zip

Public Class exportarWord
    Private Shared Property dirOutputDoc As String
    Private Shared Property dirPlantillasWord As String

    Public Shared Function GenerarCartas(fijaciones As List(Of FijacionDTO)) As String
        Dim listaDeArchivos As List(Of String) = New List(Of String)

        Dim configurationAppSettings As New System.Configuration.AppSettingsReader()
        Dim DocumentName As String = ""
        Dim fijacionesCanje = fijaciones.Where(Function(f) f.TipoTransaccion.ToLower().Trim().Equals("canje")).ToList()
        Dim FijacionesAportes = fijaciones.Where(Function(f) f.TipoTransaccion.ToLower().Trim().Equals("rescate") Or f.TipoTransaccion.ToLower().Trim().Equals("suscripcion")).ToList()

        Dim dirOutputExcel As String
        Dim nombreZip As String


        dirPlantillasWord = DirectCast(configurationAppSettings.GetValue("PlantillasWord", GetType(System.String)), String)
        dirOutputDoc = DirectCast(configurationAppSettings.GetValue("RutaGeneracionExcel", GetType(System.String)), String)

        dirOutputExcel = DirectCast(configurationAppSettings.GetValue("CarpetaGeneracionExcel", GetType(System.String)), String)

        listaDeArchivos.Add(CartaComprobanteDePagoAportes(FijacionesAportes))
        listaDeArchivos.Add(CartaComprobanteDePagoCanje(fijacionesCanje))

        listaDeArchivos.Add(InstrucionAlDCVdeAportes(FijacionesAportes))
        listaDeArchivos.Add(InstrucionAlDCVdeCanjes(fijacionesCanje))

        nombreZip = HacerZip(listaDeArchivos)

        Return dirOutputExcel + nombreZip

    End Function

    Private Shared Function HacerZip(listadearchivos As List(Of String)) As String
        Const CONST_EXTENSION As String = ".zip"
        Const CONST_NOMBRE_ARCHIVO As String = "Cartas_"

        Dim nombreZip As String
        Dim carpetaNombre As String

        Dim fechahoraGeneracion As String

        fechahoraGeneracion = Date.Now().ToString("ddMMyyyy")

        nombreZip = String.Format("{0}{1}{2}", CONST_NOMBRE_ARCHIVO, fechahoraGeneracion, CONST_EXTENSION)
        carpetaNombre = String.Format("{0}{1}", dirOutputDoc, nombreZip)

        Using zip As New ZipFile()
        For Each miFile As String In listadearchivos
                If File.Exists(miFile) Then
                    zip.AddFile(miFile, "cartas")
                End If
            Next
            If File.Exists(carpetaNombre) Then
                File.Delete(carpetaNombre)
            End If

            zip.Save(carpetaNombre)

        End Using

        Return nombreZip

    End Function

    Private Shared Function InstrucionAlDCVdeCanjes(fijacionesCanje As List(Of FijacionDTO)) As String
        Return HacerInstrucionAlDCVdeAportes("canje", fijacionesCanje)
    End Function

    Private Shared Function InstrucionAlDCVdeAportes(fijacionesAportes As List(Of FijacionDTO)) As String
        Return HacerInstrucionAlDCVdeAportes("aporte", fijacionesAportes)
    End Function

    Private Shared Function CartaComprobanteDePagoAportes(fijaciones As List(Of FijacionDTO)) As String
        Return HacerCartaComprobanteDePago("aporte", fijaciones)
    End Function

    Private Shared Function CartaComprobanteDePagoCanje(fijaciones As List(Of FijacionDTO)) As String
        Return HacerCartaComprobanteDePago("canje", fijaciones)
    End Function

    Private Shared Function HacerInstrucionAlDCVdeAportes(ByVal tipoDeComprobante As String, listaDeTransacciones As List(Of FijacionDTO)) As String
        Dim fijacionesOrdenadas = listaDeTransacciones.OrderBy(Function(f As FijacionDTO) f.TipoTransaccion).ThenBy(Function(f As FijacionDTO) f.Nemotecnico).ThenBy(Function(f As FijacionDTO) f.Rut).ToList
        Dim nombreOutput As String
        Dim TemplatePath As String

        Dim PlantillaWord As DocX
        Dim document As DocX

        Dim tipoTransaccion As String = ""
        Dim nemotecnico As String = ""
        Dim rutFondo As String = ""
        Dim indice As Integer = -1

        Dim listaFijacion As List(Of FijacionDTO) = New List(Of FijacionDTO)
        Dim listaDOcDVC As Dictionary(Of Integer, List(Of FijacionDTO)) = New Dictionary(Of Integer, List(Of FijacionDTO))
        Dim bPrimero As Boolean = True
        Dim numeroTabla As Integer = 0


        tipoDeComprobante = tipoDeComprobante.Trim().ToLower()

        If tipoDeComprobante.Equals("aporte") Then
            nombreOutput = dirOutputDoc & "CARTA PARA APORTE DE FONDOS"
            TemplatePath = dirPlantillasWord & "Formato carta aporte fondos.docx"
            PlantillaWord = Xceed.Words.NET.DocX.Load(TemplatePath)
        Else
            nombreOutput = dirOutputDoc & "CARTA PARA APORTE DE FONDOS CANJE"
            TemplatePath = dirPlantillasWord & "Formato Carta Canje.docx"
            PlantillaWord = Xceed.Words.NET.DocX.Load(TemplatePath)
        End If

        document = DocX.Create(nombreOutput)

        '*******************************************************************************
        'agrupacion de transacciones
        '*******************************************************************************
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

        '*******************************************************************************
        ' Proceso de Generacion de Cartas diferenciadas por "Aporte" o "Canjes" 
        '*******************************************************************************
        Dim cantidadDeTablasASaltar As Integer
        If tipoDeComprobante.Equals("aporte") Then
            cantidadDeTablasASaltar = 2
        Else
            cantidadDeTablasASaltar = 1
        End If


        For Each pair As KeyValuePair(Of Integer, List(Of FijacionDTO)) In listaDOcDVC
            If pair.Value.Count() > 0 Then
                document.InsertDocument(PlantillaWord, True)
                Dim t As Table = document.Tables(numeroTabla)

                bPrimero = True
                Dim PatronDeFila = t.Rows(t.Rows.Count() - 1)
                Dim NewRow As Row

                For Each transaccion As FijacionDTO In pair.Value

                    PatronDeFila = t.Rows(t.Rows.Count() - 1)
                    NewRow = t.InsertRow(PatronDeFila, t.RowCount - 1)

                    If tipoDeComprobante.Equals("aporte") Then
                        bPrimero = setColumnasDCVAporte(bPrimero, document, NewRow, transaccion)
                    Else
                        bPrimero = setColumnasDCVCanje(bPrimero, document, NewRow, transaccion)
                    End If

                Next

                PatronDeFila.Remove()
                numeroTabla += cantidadDeTablasASaltar

            End If
        Next

        If File.Exists(nombreOutput) Then
            File.Delete(nombreOutput)
        End If

        document.SaveAs(nombreOutput)

        Return nombreOutput + ".docx"

    End Function

    Private Shared Function setColumnasDCVCanje(bPrimero As Boolean, document As DocX, newRow As Row, transaccion As FijacionDTO) As Boolean

        If bPrimero Then
            bPrimero = False
            document.ReplaceText("[COLUMNA C]", transaccion.TipoTransaccion)
            document.ReplaceText("[COLUMNA E]", transaccion.FnNombreCorto)       ' Nombre del Fondo.
            document.ReplaceText("[COLUMNA B]", Date.Now().ToString("dd \de mmmm \de yyyy")) ' Fecha generacion del documento 
            document.ReplaceText("[COLUMNA D]", transaccion.Nemotecnico)        ' Nombre de la serie
            document.ReplaceText("[COLUMNA G]", transaccion.Rut)        ' Rut del fondo
            document.ReplaceText("[COLUMNA N]", Date.Now().ToString("dd \de mmmm \de yyyy")) ' Fecha generacion del documento 
            document.ReplaceText("[COLUMNA M]", transaccion.NAV_FIJADO)    ' Valor Cuota Nav 
        End If

        newRow.ReplaceText("[COLUMNA I]", transaccion.RazonSocial)   ' nombreAportante 
        newRow.ReplaceText("[COLUMNA J]", transaccion.ApRut)         ' rut del aportante
        newRow.ReplaceText("[COLUMNA K]", transaccion.Cuotas)            ' cantidad de cuotas

        'document.ReplaceText("[COLUMNA I]", fijacion.fechaPago.ToString("dd-mm-yyyy"))  ' Fecha de Solicitud
        'document.ReplaceText("[COLUMNA Y]", fijacion.fechaPago.ToString("HH:mm"))   ' Hora Solicitud
        'document.ReplaceText("[COLUMNA N]", fijacion.MonedaPago)    'Moneda pago
        'document.ReplaceText("[COLUMNA O]", fijacion.TipoTransaccion)   ' 
        'document.ReplaceText("[COLUMNA P]", fijacion.Monto)             ' monto en la moneda de la transaccion 

        Return bPrimero

    End Function

    Private Shared Function setColumnasDCVAporte(bPrimero As Boolean, document As DocX, newRow As Row, fijacion As FijacionDTO) As Boolean
        If bPrimero Then
            bPrimero = False
            document.ReplaceText("[COLUMNA C]", fijacion.TipoTransaccion)
            document.ReplaceText("[COLUMNA E]", fijacion.FnNombreCorto)       ' Nombre del Fondo.
            document.ReplaceText("[COLUMNA B]", Date.Now().ToString("dd \de mmmm \de yyyy")) ' Fecha generacion del documento 
            document.ReplaceText("[COLUMNA D]", fijacion.Nemotecnico)        ' Nombre de la serie
            document.ReplaceText("[COLUMNA G]", fijacion.Rut)        ' Rut del fondo
            document.ReplaceText("[COLUMNA N]", Date.Now().ToString("dd \de mmmm \de yyyy")) ' Fecha generacion del documento 
            document.ReplaceText("[COLUMNA M]", Utiles.formatearMonto(fijacion.NAV_FIJADO, fijacion.MonedaPago))    ' Valor Cuota Nav 
        End If

        newRow.ReplaceText("[COLUMNA I]", fijacion.RazonSocial)   ' nombreAportante 
        newRow.ReplaceText("[COLUMNA J]", fijacion.ApRut)         ' rut del aportante
        newRow.ReplaceText("[COLUMNA K]", Utiles.formateConSeparadorDeMiles(fijacion.Cuotas, 0))            ' cantidad de cuotas

        'document.ReplaceText("[COLUMNA I]", fijacion.fechaPago.ToString("dd-mm-yyyy"))  ' Fecha de Solicitud
        'document.ReplaceText("[COLUMNA Y]", fijacion.fechaPago.ToString("HH:mm"))   ' Hora Solicitud
        'document.ReplaceText("[COLUMNA N]", fijacion.MonedaPago)    'Moneda pago
        'document.ReplaceText("[COLUMNA O]", fijacion.TipoTransaccion)   ' 
        'document.ReplaceText("[COLUMNA P]", fijacion.Monto)             ' monto en la moneda de la transaccion 

        Return bPrimero
    End Function

    Private Shared Function HacerCartaComprobanteDePago(ByVal tipoDeCarta As String, fijaciones As List(Of FijacionDTO)) As String
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

        tipoDeCarta = tipoDeCarta.ToLower().Trim()
        Dim TemplatePath As String

        If (tipoDeCarta.Equals("aporte")) Then
            TemplatePath = dirPlantillasWord & "2Comprobante de pago aporte.dotx"
            nombreOutput = dirOutputDoc & "COMPROBANTE DE PAGO"
        Else  ' "canje"
            TemplatePath = dirPlantillasWord & "2Comprobante de pago aporte.docx"
            nombreOutput = dirOutputDoc & "COMPROBANTE DE PAGO CANJE"
        End If

        Dim PlantillaWord = DocX.Load(TemplatePath)
        Dim document = DocX.Create(nombreOutput)

        For Each fijacion As FijacionDTO In fijaciones

            document.InsertDocument(PlantillaWord, True)

            If (tipoDeCarta.Equals("aporte")) Then
                setColumnasAporte(document, fijacion)
            Else
                setColumnasCanje(document, fijacion)
            End If
        Next

        If File.Exists(nombreOutput) Then
            File.Delete(nombreOutput)
        End If

        document.SaveAs(nombreOutput)

        Return nombreOutput + ".docx"

    End Function

    Private Shared Sub setColumnasCanje(document As DocX, fijacion As FijacionDTO)

        document.ReplaceText("[COLUMNA D]", fijacion.TipoTransaccion)
        document.ReplaceText("[COLUMNA C]", Date.Now().ToString("dd \de mmmm \de yyyy")) ' Fecha generacion del documento 
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

    End Sub

    Private Shared Sub setColumnasAporte(document As DocX, fijacion As FijacionDTO)
        document.ReplaceText("[COLUMNA D]", fijacion.TipoTransaccion)
        document.ReplaceText("[COLUMNA C]", Date.Now().ToString("dd \de mmmm \de yyyy")) ' Fecha generacion del documento 
        document.ReplaceText("[COLUMNA X]", fijacion.fechaPago.ToString("dd-mm-yyyy"))  ' Fecha de Solicitud
        document.ReplaceText("[COLUMNA Y]", fijacion.fechaPago.ToString("HH:mm"))   ' Hora Solicitud
        document.ReplaceText("[COLUMNA F]", fijacion.FnNombreCorto)       ' Nombre del Fondo.
        document.ReplaceText("[COLUMNA G]", fijacion.Nemotecnico)        ' Nombre de la serie
        document.ReplaceText("[COLUMNA J]", fijacion.RazonSocial)   ' nombreAportante 
        document.ReplaceText("[COLUMNA K]", fijacion.ApRut)         ' rut del aportante
        document.ReplaceText("[COLUMNA N]", fijacion.MonedaPago)    'Moneda pago
        document.ReplaceText("[COLUMNA M]", Utiles.formatearMonto(fijacion.NAV_FIJADO, fijacion.MonedaPago))    ' Valor Cuota Nav  
        document.ReplaceText("[COLUMNA O]", fijacion.TipoTransaccion)   ' 
        document.ReplaceText("[COLUMNA L]", Utiles.formateConSeparadorDeMiles(fijacion.Cuotas, 0))             ' cantidad de cuotas
        document.ReplaceText("[COLUMNA P]", Utiles.formatearMonto(fijacion.Monto, fijacion.MonedaPago))             ' monto en la moneda de la transaccion 
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