Imports System.IO
Imports System.Text
Imports System.Text.RegularExpressions
Imports Xceed.Words.NET
Imports Xceed.Document.NET
Imports DTO
Imports Ionic.Zip

Public Class exportarWord


    Private Shared Property Documentos As DocumentosEstructura

    Public Shared Function GenerarCartas(fijaciones As List(Of FijacionDTO)) As String
        Dim listaDeArchivos As List(Of String) = New List(Of String)

        Dim configurationAppSettings As New System.Configuration.AppSettingsReader()
        Dim DocumentName As String = ""
        Dim fijacionesCanje = fijaciones.Where(Function(f) f.TipoTransaccion.ToLower().Trim().Equals("canje")).ToList()
        Dim FijacionesAportes = fijaciones.Where(Function(f) f.TipoTransaccion.ToLower().Trim().Equals("rescate") Or f.TipoTransaccion.ToLower().Trim().Equals("suscripcion")).ToList()

        Dim dirOutputExcel As String
        Dim nombreZip As String

        Dim nombreArchivoDestino As String

        Documentos = New DocumentosEstructura
        Documentos.documentoComprobanteCanje = DirectCast(configurationAppSettings.GetValue("WordComprobanteCanje", GetType(System.String)), String)
        Documentos.documentoComprobanteAporte = DirectCast(configurationAppSettings.GetValue("WordComprobanteAporte", GetType(System.String)), String)

        Documentos.documentoCartaAporte = DirectCast(configurationAppSettings.GetValue("WordCartaAporte", GetType(System.String)), String)
        Documentos.documentoCartaCanje = DirectCast(configurationAppSettings.GetValue("WordCartaCanje", GetType(System.String)), String)


        Documentos.dirPlantillasWord = DirectCast(configurationAppSettings.GetValue("PlantillasWord", GetType(System.String)), String)
        Documentos.dirOutputDoc = DirectCast(configurationAppSettings.GetValue("RutaGeneracionExcel", GetType(System.String)), String)

        dirOutputExcel = DirectCast(configurationAppSettings.GetValue("CarpetaGeneracionExcel", GetType(System.String)), String)

        nombreArchivoDestino = CartaComprobanteDePagoAportes(FijacionesAportes)
        If FijacionesAportes.Count > 0 Then
            listaDeArchivos.Add(nombreArchivoDestino)
        End If

        nombreArchivoDestino = CartaComprobanteDePagoCanje(fijacionesCanje)
        If fijacionesCanje.Count() > 0 Then
            listaDeArchivos.Add(nombreArchivoDestino)
        End If
        nombreArchivoDestino = InstrucionAlDCVdeAportes(FijacionesAportes)
        If FijacionesAportes.Count > 0 Then
            listaDeArchivos.Add(nombreArchivoDestino)
        End If

        nombreArchivoDestino = InstrucionAlDCVdeCanjes(fijacionesCanje)
        If fijacionesCanje.Count() > 0 Then
            listaDeArchivos.Add(nombreArchivoDestino)
        End If

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
        carpetaNombre = String.Format("{0}{1}", Documentos.dirOutputDoc, nombreZip)

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
            nombreOutput = Documentos.dirOutputDoc & "CARTA PARA APORTE DE FONDOS"
            TemplatePath = Documentos.dirPlantillasWord & Documentos.documentoCartaAporte
        Else
            nombreOutput = Documentos.dirOutputDoc & "CARTA PARA APORTE DE FONDOS CANJE"
            TemplatePath = Documentos.dirPlantillasWord & Documentos.documentoCartaCanje
        End If

        If File.Exists(nombreOutput) Then
            File.Delete(nombreOutput)
        End If

        If fijacionesOrdenadas.Count > 0 Then
            PlantillaWord = DocX.Load(TemplatePath)
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

            Dim firstTime As Boolean = True

            For Each pair As KeyValuePair(Of Integer, List(Of FijacionDTO)) In listaDOcDVC
                If pair.Value.Count() > 0 Then
                    PlantillaWord = Nothing
                    PlantillaWord = DocX.Load(TemplatePath)

                    firstTime = insertaPagina(document, PlantillaWord, firstTime)

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
        End If
        Return nombreOutput + ".docx"

    End Function

    Private Shared Function insertaPagina(document As DocX, plantillaWord As DocX, firstTime As Boolean) As Boolean
        If firstTime Then
            document.InsertDocument(plantillaWord, False)
            firstTime = False
        Else
            document.InsertDocument(plantillaWord, True)
        End If
        Return firstTime
    End Function

    Private Shared Function setColumnasDCVCanje(bPrimero As Boolean, document As DocX, newRow As Row, transaccion As FijacionDTO) As Boolean

        If bPrimero Then
            bPrimero = False
            document.ReplaceText("[COLUMNA B]", Utiles.FormatodeFecha(Date.Now())) ' Fecha generacion del documento 
            document.ReplaceText("[COLUMNA I]", transaccion.FnNombreCorto)
            document.ReplaceText("[COLUMNA J]", transaccion.ObjCanje.NombreSerieEntrante)       ' Nombre del Fondo.
            document.ReplaceText("[COLUMNA P]", transaccion.ObjCanje.NombreSerieSaliente)        ' Nombre de la serie
            document.ReplaceText("[COLUMNA H]", transaccion.ObjCanje.NombreFondo)        ' Rut del fondo
        End If

        newRow.ReplaceText("[COLUMNA J]", transaccion.ObjCanje.NombreSerieEntrante)       ' Nombre del Fondo.
        newRow.ReplaceText("[COLUMNA P]", transaccion.ObjCanje.NombreSerieSaliente)        ' Nombre de la ser
        newRow.ReplaceText("[COLUMNA E]", transaccion.RazonSocial)   ' nombreAportante 
        newRow.ReplaceText("[COLUMNA F]", transaccion.ApRut)         ' rut del aportante
        newRow.ReplaceText("[COLUMNA K]", transaccion.ObjCanje.CuotaEntrante)            ' cantidad de cuotas
        newRow.ReplaceText("[COLUMNA M]", transaccion.ObjCanje.NavCLPEntrante)            ' cantidad de cuotas
        newRow.ReplaceText("[COLUMNA T]", transaccion.ObjCanje.CuotaEntrante)            ' cantidad de cuotas
        newRow.ReplaceText("[COLUMNA R]", transaccion.ObjCanje.NavEntrante)            ' cantidad de cuotas
        newRow.ReplaceText("[COLUMNA S]", transaccion.ObjCanje.Factor)            ' cantidad de cuotas

        Return bPrimero
    End Function

    Private Shared Function setColumnasDCVAporte(bPrimero As Boolean, document As DocX, newRow As Row, fijacion As FijacionDTO) As Boolean
        If bPrimero Then
            bPrimero = False
            document.ReplaceText("[COLUMNA C]", "APORTE")
            document.ReplaceText("[COLUMNA B]", Utiles.FormatodeFecha(Date.Now)) ' Fecha generacion del documento 
            document.ReplaceText("[COLUMNA E]", fijacion.FnNombreCorto)
            document.ReplaceText("[COLUMNA D]", fijacion.Nemotecnico)        ' Nombre de la serie
            document.ReplaceText("[COLUMNA G]", fijacion.Rut)        ' Rut del fondo
            document.ReplaceText("[COLUMNA N]", Utiles.FormatodeFecha(Date.Now)) ' Fecha generacion del documento 
            document.ReplaceText("[COLUMNA M]", Utiles.formatearMonto(fijacion.NAV_FIJADO, fijacion.MonedaPago))    ' Valor Cuota Nav 
        End If

        newRow.ReplaceText("[COLUMNA I]", fijacion.RazonSocial)   ' nombreAportante 
        newRow.ReplaceText("[COLUMNA J]", fijacion.ApRut)         ' rut del aportante
        document.ReplaceText("[COLUMNA K]", fijacion.Monto)            ' cantidad de cuotas
        newRow.ReplaceText("[COLUMNA N]", Utiles.FormatodeFecha(Date.Now))             ' monto en la moneda de la transaccion 
        document.ReplaceText("[COLUMNA P]", Utiles.formateConSeparadorDeMiles(fijacion.Cuotas, 0))             ' monto en la moneda de la transaccion 

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
            TemplatePath = Documentos.dirPlantillasWord & Documentos.documentoComprobanteAporte
            nombreOutput = Documentos.dirOutputDoc & "COMPROBANTE DE PAGO"
        Else  ' "canje"
            TemplatePath = Documentos.dirPlantillasWord & Documentos.documentoComprobanteCanje
            nombreOutput = Documentos.dirOutputDoc & "COMPROBANTE DE PAGO CANJE"
        End If



        If File.Exists(nombreOutput) Then
            File.Delete(nombreOutput)
        End If


        If fijaciones.Count > 0 Then

            Dim document = DocX.Create(nombreOutput)
            Dim firstTime As Boolean = True

            For Each fijacion As FijacionDTO In fijaciones
                Dim PlantillaWord As DocX = DocX.Load(TemplatePath)

                firstTime = insertaPagina(document, PlantillaWord, firstTime)

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

        End If

        Return nombreOutput + ".docx"

    End Function

    Private Shared Sub setColumnasCanje(document As DocX, fijacion As FijacionDTO)

        document.ReplaceText("[Columna D]", Utiles.FormatodeFecha(Date.Now())) ' Fecha generacion del documento 
        document.ReplaceText("[Columna K]", fijacion.ObjCanje.NombreFondo)   ' Nombre del fondo
        document.ReplaceText("[Columna L]", fijacion.ObjCanje.NombreSerieEntrante) ' Serie entrante
        document.ReplaceText("[Columna R]", fijacion.ObjCanje.NombreSerieSaliente)             ' serie saliente 
        document.ReplaceText("[Columna G]", fijacion.RazonSocial)   ' nombreAportante 
        document.ReplaceText("[Columna H]", fijacion.ApRut)         ' rut del aportante

        document.ReplaceText("[Columna AB]", fijacion.ObjCanje.Cuotas)         ' Cuotas Entrante
        document.ReplaceText("[Columna N]", fijacion.ObjCanje.MontoCLPEntrante) ' Monto Clp Entrante 
        document.ReplaceText("[Columna AA]", fijacion.ObjCanje.CuotaEntrante)         ' Cuotas Entrante
        document.ReplaceText("[Columna S]", fijacion.ObjCanje.MontoCLPSaliente)         ' Monto CLP SALiente
        document.ReplaceText("[Columna U]", fijacion.ObjCanje.Factor)         ' Factor

        document.ReplaceText("[Columna M]", fijacion.ObjCanje.Cuotas)     ' 
        document.ReplaceText("[Columna V]", fijacion.ObjCanje.CuotaSaliente)     ' 

        document.ReplaceText("[Columna O]", fijacion.ObjCanje.MontoEntrante)     ' 
        document.ReplaceText("[Columna Q]", fijacion.ObjCanje.MontoSaliente)     ' 

    End Sub

    Private Shared Sub setColumnasAporte(document As DocX, fijacion As FijacionDTO)
        Dim fechaPago As String
        Dim horaPago As String

        If fijacion.TipoTransaccion = "Rescate" Then
            fechaPago = "" + fijacion.ObjRescate.RES_Fecha_Pago
            horaPago = "" + fijacion.ObjRescate.RES_Fecha_Pago.ToString("HH:mm")
        Else
            fechaPago = "" + fijacion.ObjSuscripcion.FechaSuscripcion
            horaPago = "" + fijacion.ObjSuscripcion.FechaSuscripcion.ToString("HH:mm")
        End If

        document.ReplaceText("[Columna D]", "APORTE")
        document.ReplaceText("[Columna C]", Utiles.FormatodeFecha(Date.Now())) ' Fecha generacion del documento 

        document.ReplaceText("[Columna X]", fechaPago)  ' Fecha de Solicitud
        document.ReplaceText("[Columna Y]", horaPago)   ' Hora Solicitud

        document.ReplaceText("[Columna F]", fijacion.FnNombreCorto)       ' Nombre del Fondo.
        document.ReplaceText("[Columna G]", fijacion.Nemotecnico)        ' Nombre de la serie
        document.ReplaceText("[Columna J]", fijacion.RazonSocial)   ' nombreAportante 
        document.ReplaceText("[Columna K]", fijacion.ApRut)         ' rut del aportante
        document.ReplaceText("[Columna N]", fijacion.MonedaPago)    'Moneda pago
        document.ReplaceText("[Columna M]", Utiles.formatearMonto(fijacion.NAV_FIJADO, fijacion.MonedaPago))    ' Valor Cuota Nav  
        document.ReplaceText("[Columna O]", fijacion.TipoTransaccion)   ' 
        document.ReplaceText("[Columna L]", Utiles.formateConSeparadorDeMiles(fijacion.Cuotas, 0))             ' cantidad de cuotas
        document.ReplaceText("[Columna P]", Utiles.formatearMonto(fijacion.Monto, fijacion.MonedaPago))             ' monto en la moneda de la transaccion 
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

Public Class DocumentosEstructura

    Public Property dirOutputDoc As String
    Public Property dirPlantillasWord As String

    Public Property documentoComprobanteCanje As String
    Public Property documentoComprobanteAporte As String

    Public Property documentoCartaCanje As String
    Public Property documentoCartaAporte As String

    Public Sub New()
    End Sub
End Class