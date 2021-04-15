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

        nombreArchivoDestino = InstrucionAlDCVdeAportes(FijacionesAportes)
        If FijacionesAportes.Count > 0 Then
            listaDeArchivos.Add(nombreArchivoDestino)
        End If

        nombreArchivoDestino = CartaComprobanteDePagoCanje(fijacionesCanje)
        If fijacionesCanje.Count() > 0 Then
            listaDeArchivos.Add(nombreArchivoDestino)
        End If

        nombreArchivoDestino = InstrucionAlDCVdeCanjes(fijacionesCanje)
        If fijacionesCanje.Count() > 0 Then
            listaDeArchivos.Add(nombreArchivoDestino)
        End If

        nombreZip = HacerZip(listaDeArchivos)

        Return dirOutputExcel + nombreZip

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
        Dim fechaDocumento As String
        fechaDocumento = Date.Now().ToString("yyyy-MM-dd HH:mm")

        With fijacion
            document.ReplaceText("[Columna D]", fechaDocumento)                                 ' Fecha generacion del documento ' TODO (cesar): fecha y hora pide
            document.ReplaceText("[Columna K]", .ObjCanje.NombreFondo)                  ' Nombre del fondo
            document.ReplaceText("[Columna L]", .ObjCanje.NombreSerieSaliente)          ' serie saliente 
            document.ReplaceText("[Columna R]", .ObjCanje.NombreSerieEntrante)          ' Serie entrante
            document.ReplaceText("[Columna G]", .RazonSocial)                           ' nombreAportante 
            document.ReplaceText("[Columna H]", .ApRut)                                 ' rut del aportante

            document.ReplaceText("[Columna AA]", .ObjCanje.MonedaSaliente)              ' Moneda del fondo Saliente
            document.ReplaceText("[Columna AB]", .ObjCanje.MonedaEntrante)              ' Moneda del fondo Entrante

            If fijacion.MonedaPago = "CLP" Then
                document.ReplaceText("[Columna MNO]", Utiles.formatearNAVCLP(.ObjCanje.NavCLPSaliente))
                document.ReplaceText("[Columna ST]", Utiles.formatearNAVCLP(.ObjCanje.NavCLPEntrante))
                document.ReplaceText("[Columna Q]", Utiles.formatearMontoCLP(.ObjCanje.MontoCLPSaliente))
            Else
                document.ReplaceText("[Columna MNO]", Utiles.formatearNAV(.ObjCanje.NavSaliente))
                document.ReplaceText("[Columna ST]", Utiles.formatearNAV(.ObjCanje.NavEntrante))
                document.ReplaceText("[Columna Q]", Utiles.formatearMonto(.ObjCanje.MontoSaliente, fijacion.MonedaPago))

            End If


            document.ReplaceText("[Columna U]", .ObjCanje.Factor)
            document.ReplaceText("[Columna M]", Utiles.formateConSeparadorDeMiles(.ObjCanje.CuotaSaliente, 0))   ' Cuotas Salientes
            document.ReplaceText("[Columna V]", Utiles.formateConSeparadorDeMiles(.ObjCanje.CuotaEntrante, 0))   ' Cuotas Entrantes


        End With

    End Sub

    Private Shared Sub setColumnasAporte(document As DocX, fijacion As FijacionDTO)
        Dim fechaPago As String
        Dim horaPago As String

        If fijacion.TipoTransaccion = "Rescate" Then
            fechaPago = "" + fijacion.ObjRescate.RES_Fecha_Pago
            horaPago = "" + fijacion.ObjRescate.RES_Fecha_Pago.ToString("HH:mm")
            document.ReplaceText("[Columna D]", "RESCATE")

            If fijacion.MonedaPago = "CLP" Then
                document.ReplaceText("[Columna MNO]", Utiles.formatearNAVCLP(fijacion.ObjRescate.RES_Nav_CLP))    ' Valor Cuota Nav para CLP     
                document.ReplaceText("[Columna QRS]", Utiles.formatearMontoCLP(fijacion.ObjRescate.RES_Monto_CLP))  ' Monto en la moneda de la transaccion

                'ElseIf fijacion.MonedaPago = "USD" Then
                '   document.ReplaceText("[Columna MNO]", Utiles.formatearMonto(fijacion.ObjRescate.RES_Nav, fijacion.MonedaPago))        ' Valor Cuota Nav para USD                
                '  document.ReplaceText("[Columna QRS]", Utiles.formatearMonto(fijacion.Monto, fijacion.MonedaPago))                     ' Monto en la moneda de la transaccion
            Else
                document.ReplaceText("[Columna MNO]", Utiles.formatearNAV(fijacion.ObjRescate.RES_Nav))        ' Valor Cuota Nav para EUR                
                document.ReplaceText("[Columna QRS]", Utiles.formatearMonto(fijacion.Monto, fijacion.MonedaPago))                     ' Monto en la moneda de la transaccion
            End If

            document.ReplaceText("[Columna L]", Utiles.formateConSeparadorDeMiles(fijacion.ObjRescate.RES_Cuotas, 0))                 ' cantidad de cuotas
        Else
            fechaPago = "" + fijacion.ObjSuscripcion.FechaSuscripcion
            horaPago = "" + fijacion.ObjSuscripcion.FechaSuscripcion.ToString("HH:mm")
            document.ReplaceText("[Columna D]", "APORTE")

            If fijacion.MonedaPago = "CLP" Then
                document.ReplaceText("[Columna MNO]", Utiles.formatearNAVCLP(fijacion.ObjSuscripcion.NAVCLP))     ' Valor Cuota Nav para CLP     
                document.ReplaceText("[Columna QRS]", Utiles.formatearMontoCLP(fijacion.ObjSuscripcion.MontoCLP))   ' Monto en la moneda de la transaccion para CLP
                'ElseIf fijacion.MonedaPago = "USD" Then
                '   document.ReplaceText("[Columna MNO]", Utiles.formatearMonto(fijacion.ObjSuscripcion.NAV, fijacion.MonedaPago))        ' Valor Cuota Nav para USD
                '  document.ReplaceText("[Columna QRS]", Utiles.formatearMonto(fijacion.ObjSuscripcion.Monto, fijacion.MonedaPago))      ' Monto en la moneda de la transaccion para USD
            Else
                document.ReplaceText("[Columna MNO]", Utiles.formatearNAV(fijacion.ObjSuscripcion.NAV))        ' Valor Cuota Nav para EUR
                document.ReplaceText("[Columna QRS]", Utiles.formatearMonto(fijacion.ObjSuscripcion.Monto, fijacion.MonedaPago))      ' Monto en la moneda de la transaccion para EUR
            End If

            document.ReplaceText("[Columna L]", Utiles.formateConSeparadorDeMiles(fijacion.ObjSuscripcion.CuotasEmitidas, 0))                 ' cantidad de cuotas
        End If

        document.ReplaceText("[Columna C]", Utiles.FormatodeFecha(Date.Now())) ' Fecha generacion del documento 
        document.ReplaceText("[Columna X]", fechaPago)                         ' Fecha de Solicitud
        document.ReplaceText("[Columna Y]", horaPago)                          ' Hora Solicitud

        document.ReplaceText("[Columna F]", fijacion.FnNombreCorto)            ' Nombre del Fondo.
        document.ReplaceText("[Columna G]", fijacion.Nemotecnico)              ' Nombre de la serie
        document.ReplaceText("[Columna J]", fijacion.RazonSocial)              ' nombreAportante 
        document.ReplaceText("[Columna K]", fijacion.ApRut)                    ' rut del aportante

        document.ReplaceText("[Columna N]", fijacion.MonedaPago)               ' Moneda pago

        'If fijacion.MonedaPago = "CLP" Then
        '    document.ReplaceText("[Columna MNO]", Utiles.formatearMonto(fijacion.NavCLP, fijacion.MonedaPago))        ' Valor Cuota Nav para CLP     
        '    document.ReplaceText("[Columna QRS]", Utiles.formatearMonto(fijacion.MontoCLP, fijacion.MonedaPago))      ' Monto en la moneda de la transaccion para CLP
        'ElseIf fijacion.MonedaPago = "USD" Then
        '    document.ReplaceText("[Columna MNO]", Utiles.formatearMonto(fijacion.NAV_FIJADO, fijacion.MonedaPago))    ' Valor Cuota Nav para USD
        '    document.ReplaceText("[Columna QRS]", Utiles.formatearMonto(fijacion.Monto, fijacion.MonedaPago))         ' Monto en la moneda de la transaccion para USD
        'Else
        '    document.ReplaceText("[Columna MNO]", Utiles.formatearMonto(fijacion.NAV_FIJADO, fijacion.MonedaPago))    ' Valor Cuota Nav para EUR
        '    document.ReplaceText("[Columna QRS]", Utiles.formatearMonto(fijacion.Monto, fijacion.MonedaPago))         ' Monto en la moneda de la transaccion para EUR
        'End If        

    End Sub
    Private Shared Function setColumnasDCVCanje(bPrimero As Boolean, document As DocX, newRow As Row, transaccion As FijacionDTO) As Boolean

        If bPrimero Then
            bPrimero = False
            document.ReplaceText("[COLUMNA D]", Utiles.FormatodeFecha(Date.Now()))         ' Fecha generacion del documento 
            document.ReplaceText("[COLUMNA G]", transaccion.FnNombreCorto)                 ' Nombre aportante
            document.ReplaceText("[COLUMNA L]", transaccion.ObjCanje.NombreSerieSaliente)  ' Nombre de la serie Saliente
            document.ReplaceText("[COLUMNA R]", transaccion.ObjCanje.NombreSerieEntrante)  ' Nombre de la serie Entrante
            document.ReplaceText("[COLUMNA K]", transaccion.ObjCanje.NombreFondo)          ' Rut del fondo
            document.ReplaceText("[COLUMNA J]", transaccion.Nemotecnico)                   ' Nemotecnico
        End If

        newRow.ReplaceText("[COLUMNA H]", transaccion.ApRut)                               ' Rut del aportante
        newRow.ReplaceText("[COLUMNA M]", Utiles.formateConSeparadorDeMiles(transaccion.ObjCanje.CuotaSaliente, 0))  ' Cuotas Salientes
        newRow.ReplaceText("[COLUMNA O]", Utiles.formatearNAVCLP(transaccion.ObjCanje.NavCLPSaliente)) ' Nav Saliente en CLP  --TODO (Cesar): validar si lleva 4 decimales (asi aparece en el mantenedor de Canje)
        newRow.ReplaceText("[COLUMNA V]", Utiles.formateConSeparadorDeMiles(transaccion.ObjCanje.CuotaEntrante, 0))  ' Cuotas Entrante
        newRow.ReplaceText("[COLUMNA T]", Utiles.formatearNAVCLP(transaccion.ObjCanje.NavCLPEntrante)) ' Nav Entrante en CLP  --TODO (Cesar): validar si lleva 4 decimales (asi aparece en el mantenedor de Canje)
        newRow.ReplaceText("[COLUMNA U]", transaccion.ObjCanje.Factor)                     ' Factor  --TODO (Cesar): validar cuantos numeros despues de la coma debe llevar (aparece entero)

        Return bPrimero
    End Function

    Private Shared Function setColumnasDCVAporte(bPrimero As Boolean, document As DocX, newRow As Row, fijacion As FijacionDTO) As Boolean
        If bPrimero Then
            bPrimero = False
            If fijacion.TipoTransaccion = "Suscripcion" Then
                document.ReplaceText("[COLUMNA D]", "APORTE")
                document.ReplaceText("[COLUMNA N]", Utiles.formatearNAVCLP(fijacion.ObjSuscripcion.NAVCLP))     ' Valor Cuota Nav CLP siempre
            Else
                document.ReplaceText("[COLUMNA D]", "RESCATE")
                document.ReplaceText("[COLUMNA N]", Utiles.formatearNAVCLP(fijacion.ObjRescate.RES_Nav_CLP))    ' Valor Cuota Nav CLP siempre
            End If

            document.ReplaceText("[COLUMNA C]", Utiles.FormatodeFecha(Date.Now))         ' Fecha generacion del documento 
            document.ReplaceText("[COLUMNA F]", fijacion.FnNombreCorto)                  ' Nombre de Fondo
            document.ReplaceText("[COLUMNA E]", fijacion.Nemotecnico)                    ' Nombre de la serie
            document.ReplaceText("[COLUMNA H]", fijacion.Rut)                            ' Rut del fondo                        
        End If

        newRow.ReplaceText("[COLUMNA J]", fijacion.RazonSocial)                          ' nombreAportante 
        newRow.ReplaceText("[COLUMNA K]", fijacion.ApRut)                                ' rut del aportante
        newRow.ReplaceText("[COLUMNA L]", Utiles.formateConSeparadorDeMiles(fijacion.Cuotas, 0))                             ' cantidad de cuotas

        Dim fechaAImprimir As String

        'If fijacion.TipoTransaccion.ToLower().Equals("rescate") Then
        '    fechaAImprimir = fijacion.ObjRescate.RES_Fecha_Solicitud
        'Else
        '    fechaAImprimir = fijacion.ObjSuscripcion.FechaSuscripcion
        'End If
        fechaAImprimir = Utiles.FormatodeFecha(Date.Now)
        newRow.ReplaceText("[COLUMNA O]", Utiles.FormatodeFecha(fechaAImprimir))               ' monto en la moneda de la transaccion --TODO (Cesar): que fecha es O ?
        'document.ReplaceText("[COLUMNA P]", Utiles.formateConSeparadorDeMiles(fijacion.Cuotas, 0))             ' monto en la moneda de la transaccion 

        Return bPrimero
    End Function
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