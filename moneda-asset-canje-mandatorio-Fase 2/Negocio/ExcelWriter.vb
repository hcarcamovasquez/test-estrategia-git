Imports DTO
Imports Datos
#Disable Warning BC40056 ' El espacio de nombres o el tipo especificado en el 'OfficeOpenXml' Imports no contienen ningún miembro público o no se encuentran. Asegúrese de que el espacio de nombres o el tipo se hayan definido y de que contengan al menos un miembro público. Asegúrese de que el nombre del elemento importado no use ningún alias.
Imports OfficeOpenXml
#Enable Warning BC40056 ' El espacio de nombres o el tipo especificado en el 'OfficeOpenXml' Imports no contienen ningún miembro público o no se encuentran. Asegúrese de que el espacio de nombres o el tipo se hayan definido y de que contengan al menos un miembro público. Asegúrese de que el nombre del elemento importado no use ningún alias.
Imports System.IO
Imports System.Web

Public Class ExcelWriter

    Public Property rutaArchivosExcel As String

    Public Function CrearExcelReporteGeneva(lista As List(Of ReporteDcvVsGenevaDTO)) As Boolean
        Dim pkg As ExcelPackage = New ExcelPackage()
        Dim sheet As ExcelWorksheet = pkg.Workbook.Worksheets.Add("DCV vs Geneva")
        Dim fila As Integer = 3
        Dim configurationAppSettings As New System.Configuration.AppSettingsReader()

        Try
            sheet.Cells(fila, 2).Value = "DCV"
            sheet.Cells(fila, 5).Value = "GENEVA"
            sheet.Cells(fila, 8).Value = "Transito"
            sheet.Cells(fila, 8).Value = "Fecha"
            fila = fila + 1

            sheet.Cells(fila, 2).Value = "NEMO"
            sheet.Cells(fila, 3).Value = "Cuotas Colocadas"
            sheet.Cells(fila, 5).Value = "NEMO"
            sheet.Cells(fila, 6).Value = "CLASE"
            sheet.Cells(fila, 7).Value = "Cuotas Colocadas"

            sheet.Cells(fila, 8).Value = "Rescates"
            sheet.Cells(fila, 9).Value = "Suscripciones"
            sheet.Cells(fila, 10).Value = "Canje"

            sheet.Cells(fila, 11).Value = "Recompra"
            sheet.Cells(fila, 12).Value = "Total"
            sheet.Cells(fila, 13).Value = "Diferencia"
            sheet.Cells(fila, 14).Value = "Observaciones"

            fila = fila + 1

            For Each cuotas As ReporteDcvVsGenevaDTO In lista

                sheet.Cells(fila, 2).Value = cuotas.DCV_Nemo.ToString()
                sheet.Cells(fila, 3).Value = cuotas.DCV_Cuotas.ToString()
                sheet.Cells(fila, 5).Value = cuotas.GNV_Nemo.ToString()
                sheet.Cells(fila, 6).Value = cuotas.GNV_Clase.ToString()
                sheet.Cells(fila, 7).Value = cuotas.GNV_Cuotas.ToString()

                sheet.Cells(fila, 8).Value = cuotas.TRS_Rescates.ToString()
                sheet.Cells(fila, 9).Value = cuotas.TRS_Suscripciones.ToString()
                sheet.Cells(fila, 10).Value = cuotas.TRS_Canje.ToString()

                sheet.Cells(fila, 11).Value = cuotas.Recompra.ToString()
                sheet.Cells(fila, 12).Value = cuotas.Total.ToString()
                sheet.Cells(fila, 13).Value = cuotas.Diferencia.ToString()
                sheet.Cells(fila, 14).Value = cuotas.Observaciones.ToString()

                fila = fila + 1
            Next

            Dim fileNameFormat As String = "{0}_{1}{2}"
            Dim ruta As String = DirectCast(configurationAppSettings.GetValue("RutaGeneracionExcel", GetType(System.String)), String)
            Dim carpetaGeneracionExcel As String = DirectCast(configurationAppSettings.GetValue("CarpetaGeneracionExcel", GetType(System.String)), String)

            Dim filename As String
            filename = String.Format(fileNameFormat, "DCV_vs_Geneva", Date.Now().ToString("ddMMyyyy"), ".xlsx")

            Dim fi As FileInfo
            fi = New FileInfo(ruta + filename)

            If fi.Exists Then
                fi.Delete()
            End If

            pkg.SaveAs(fi)
            fi = New FileInfo(ruta + filename)
            If fi.Exists Then
                rutaArchivosExcel = carpetaGeneracionExcel + filename
                Return True
            Else
                rutaArchivosExcel = Nothing
                Return False
            End If
        Catch ex As Exception
            Throw
        End Try
        Return False
    End Function

    Public Function CrearExcelAportantes(listaAportante As List(Of AportanteDTO)) As Boolean
        Dim pkg As ExcelPackage = New ExcelPackage()
        Dim sheet As ExcelWorksheet = pkg.Workbook.Worksheets.Add("Aportantes")
        Dim fila As Integer = 1
        Dim configurationAppSettings As New System.Configuration.AppSettingsReader()
        Try

            sheet.Cells(fila, 1).Value = "RUT Aportante"
            sheet.Cells(fila, 2).Value = "Nombre / Razón Social"
            sheet.Cells(fila, 3).Value = "Multifondo"
            sheet.Cells(fila, 4).Value = "Domiciliado en el Extranjero "
            sheet.Cells(fila, 5).Value = "Inversionista Calificado"
            sheet.Cells(fila, 6).Value = "Tipo de Aportante"
            sheet.Cells(fila, 7).Value = "Relacionado a MAM"
            sheet.Cells(fila, 8).Value = "Tiene Contrato de Distribución"
            sheet.Cells(fila, 9).Value = "Usuario Ingreso"
            sheet.Cells(fila, 10).Value = "Fecha de Ingreso"
            sheet.Cells(fila, 11).Value = "Usuario de Modificación"
            sheet.Cells(fila, 12).Value = "Fecha de Modificación"


            fila = fila + 1

            For Each aportante As AportanteDTO In listaAportante
                sheet.Cells(fila, 1).Value = aportante.Rut.ToString()
                sheet.Cells(fila, 2).Value = aportante.RazonSocial.ToString()
                sheet.Cells(fila, 3).Value = aportante.Multifondo.ToString()
                sheet.Cells(fila, 4).Value = aportante.NacExt.ToString()
                sheet.Cells(fila, 5).Value = aportante.Calificado.ToString()
                sheet.Cells(fila, 6).Value = aportante.Intermediario.ToString()
                sheet.Cells(fila, 7).Value = aportante.RelacionMan.ToString()
                sheet.Cells(fila, 8).Value = aportante.Distribucion.ToString()
                sheet.Cells(fila, 9).Value = aportante.UsuarioIngreso.ToString()
                sheet.Cells(fila, 10).Value = aportante.FechaIngreso.Value.ToString(Constantes.CONST_FORMAT_FECHA)
                sheet.Cells(fila, 11).Value = aportante.UsuarioModificacion.ToString()
                sheet.Cells(fila, 12).Value = aportante.FechaModificacion.Value.ToString(Constantes.CONST_FORMAT_FECHA)

                fila = fila + 1
            Next

            Dim fileName As String = "Aportantes_" + Date.Now().ToString("ddMMyyyy") + ".xlsx"
            Dim ruta As String = DirectCast(configurationAppSettings.GetValue("RutaGeneracionExcel", GetType(System.String)), String)
            Dim carpetaGeneracionExcel As String = DirectCast(configurationAppSettings.GetValue("CarpetaGeneracionExcel", GetType(System.String)), String)
            Dim fi As FileInfo
            fi = New FileInfo(ruta + fileName)
            If fi.Exists Then
                fi.Delete()
            End If

            pkg.SaveAs(fi)
            fi = New FileInfo(ruta + fileName)
            If fi.Exists Then
                rutaArchivosExcel = carpetaGeneracionExcel + fileName
                Return True
            Else
                rutaArchivosExcel = Nothing
                Return False
            End If
        Catch ex As Exception
            Throw
        End Try
        Return False
    End Function

    Friend Function CrearExcelReporteCuotasEmitidas(lista As List(Of ReporteControlCuotasEmitidasDTO)) As Boolean
        Dim pkg As ExcelPackage = New ExcelPackage()
        Dim sheet As ExcelWorksheet = pkg.Workbook.Worksheets.Add("Fondos")
        Dim fila As Integer = 1
        Dim configurationAppSettings As New System.Configuration.AppSettingsReader()
        Try

            sheet.Cells(fila, 1).Value = "NEMO"
            sheet.Cells(fila, 2).Value = "CCY"
            sheet.Cells(fila, 3).Value = "Fecha Emisión"
            sheet.Cells(fila, 4).Value = "Fecha Vencimiento"
            sheet.Cells(fila, 5).Value = "Cuotas Emitidas"
            sheet.Cells(fila, 6).Value = "Acumulado"
            sheet.Cells(fila, 7).Value = "Año en Curso"
            sheet.Cells(fila, 9).Value = "% Sobre última emisión"
            sheet.Cells(fila, 10).Value = "Total Suscritas de última emisión"
            sheet.Cells(fila, 11).Value = "Total Cuotas Suscritas y pagadas"
            sheet.Cells(fila, 12).Value = "% Sobre total de Cuotas Suscritas y pagadas"

            fila = fila + 1

            For Each cuotas As ReporteControlCuotasEmitidasDTO In lista

                sheet.Cells(fila, 1).Value = cuotas.FsNemotecnico.ToString()
                sheet.Cells(fila, 2).Value = cuotas.FsMoneda.ToString()

                sheet.Cells(fila, 3).Value = cuotas.FnFechaEmision.ToString()
                sheet.Cells(fila, 4).Value = cuotas.FnFechaVencimiento.ToString()

                sheet.Cells(fila, 5).Value = cuotas.FnCuotasEmitidas.ToString()

                sheet.Cells(fila, 6).Value = cuotas.Acumulado.ToString()
                sheet.Cells(fila, 7).Value = cuotas.Anno_En_Curso.ToString()

                sheet.Cells(fila, 9).Value = cuotas.PorcentajeUltimaEmision.ToString()
                sheet.Cells(fila, 10).Value = cuotas.TotalSuscritasUltimaEmision.ToString()
                sheet.Cells(fila, 11).Value = cuotas.TotalCuotasSuscritaspagadas.ToString()
                sheet.Cells(fila, 12).Value = cuotas.PorcentajeTotalCuotasSuscritasPagadas.ToString()

                fila = fila + 1
            Next

            Dim fileNameFormat As String = "{0}_{1}{2}"
            Dim ruta As String = DirectCast(configurationAppSettings.GetValue("RutaGeneracionExcel", GetType(System.String)), String)
            Dim carpetaGeneracionExcel As String = DirectCast(configurationAppSettings.GetValue("CarpetaGeneracionExcel", GetType(System.String)), String)

            Dim filename As String
            filename = String.Format(fileNameFormat, "ReporteControlCuotasEmitidas", Date.Now().ToString("ddMMyyyy"), ".xlsx")

            Dim fi As FileInfo
            fi = New FileInfo(ruta + filename)

            If fi.Exists Then
                fi.Delete()
            End If

            pkg.SaveAs(fi)
            fi = New FileInfo(ruta + fileName)
            If fi.Exists Then
                rutaArchivosExcel = carpetaGeneracionExcel + fileName
                Return True
            Else
                rutaArchivosExcel = Nothing
                Return False
            End If
        Catch ex As Exception
            Throw
        End Try
        Return False
    End Function

    Public Function CrearExcelGrupoAportantes(listaGrupoAportante As List(Of AportantesXGrupoDTO)) As Boolean
        Dim pkg As ExcelPackage = New ExcelPackage()
        Dim sheet As ExcelWorksheet = pkg.Workbook.Worksheets.Add("Grupo Aportantes")
        Dim fila As Integer = 1
        Dim configurationAppSettings As New System.Configuration.AppSettingsReader()

        Try

            sheet.Cells(fila, 1).Value = "ID Grupo"
            sheet.Cells(fila, 2).Value = "Nombre del Grupo"
            sheet.Cells(fila, 3).Value = "RUT Aportante"
            sheet.Cells(fila, 4).Value = "Usuario Ingreso"
            sheet.Cells(fila, 5).Value = "Fecha de Ingreso"
            sheet.Cells(fila, 6).Value = "Usuario de Modificación"
            sheet.Cells(fila, 7).Value = "Fecha de Modificación"
            'sheet.Cells(fila, 4).Value = "Nombre / Razón Social"

            fila = fila + 1

            For Each grupoAportante As AportantesXGrupoDTO In listaGrupoAportante

                sheet.Cells(fila, 1).Value = grupoAportante.IdGrupo.ToString()
                sheet.Cells(fila, 2).Value = grupoAportante.NombreGrupo.ToString()
                sheet.Cells(fila, 3).Value = grupoAportante.RutAportante.ToString()
                sheet.Cells(fila, 4).Value = grupoAportante.UsuarioIngreso.ToString()
                sheet.Cells(fila, 5).Value = grupoAportante.FechaIngreso.Value.ToString(Constantes.CONST_FORMAT_FECHA)
                sheet.Cells(fila, 6).Value = grupoAportante.UsuarioModificacion.ToString()
                sheet.Cells(fila, 7).Value = grupoAportante.FechaModificacion.Value.ToString(Constantes.CONST_FORMAT_FECHA)
                '   sheet.Cells(fila, 4).Value = grupoAportante.NombreAportante.ToString()

                fila = fila + 1
            Next

            Dim fileName As String = "GrupoAportantes_" + Date.Now().ToString("ddMMyyyy") + ".xlsx"
            Dim ruta As String = DirectCast(configurationAppSettings.GetValue("RutaGeneracionExcel", GetType(System.String)), String)
            Dim carpetaGeneracionExcel As String = DirectCast(configurationAppSettings.GetValue("CarpetaGeneracionExcel", GetType(System.String)), String)
            Dim fi As FileInfo
            fi = New FileInfo(ruta + fileName)
            If fi.Exists Then
                fi.Delete()
            End If

            pkg.SaveAs(fi)
            fi = New FileInfo(ruta + fileName)
            If fi.Exists Then
                rutaArchivosExcel = carpetaGeneracionExcel + fileName
                Return True
            Else
                rutaArchivosExcel = Nothing
                Return False
            End If
        Catch ex As Exception
            Throw
        End Try
        Return False
    End Function

    Public Function CrearExcelFondos(listaFondo As List(Of FondoDTO)) As Boolean

        Dim pkg As ExcelPackage = New ExcelPackage()
        Dim sheet As ExcelWorksheet = pkg.Workbook.Worksheets.Add("Fondos")
        Dim fila As Integer = 1
        Dim configurationAppSettings As New System.Configuration.AppSettingsReader()
        Try

            sheet.Cells(fila, 1).Value = "RUT del Fondo"
            sheet.Cells(fila, 2).Value = "Nombre del Fondo"
            sheet.Cells(fila, 3).Value = "Nombre Corto del Fondo"
            sheet.Cells(fila, 4).Value = "Cuotas Emitidas"
            sheet.Cells(fila, 5).Value = "Fecha Emisión"
            sheet.Cells(fila, 6).Value = "Fecha Vencimiento"
            sheet.Cells(fila, 7).Value = "Acumulado"
            sheet.Cells(fila, 8).Value = "Usuario Ingreso"
            sheet.Cells(fila, 9).Value = "Fecha de Ingreso"
            sheet.Cells(fila, 10).Value = "Usuario de Modificación"
            sheet.Cells(fila, 11).Value = "Fecha de Modificación"

            fila = fila + 1

            For Each fondo As FondoDTO In listaFondo

                sheet.Cells(fila, 1).Value = fondo.Rut.ToString()
                sheet.Cells(fila, 2).Value = fondo.RazonSocial.ToString()
                sheet.Cells(fila, 3).Value = fondo.NombreCorto.ToString()

                sheet.Cells(fila, 4).Value = fondo.CuotasEmitidas.ToString()
                sheet.Cells(fila, 5).Value = IIf(fondo.FechaEmision.Value.ToString(Constantes.CONST_FORMAT_FECHA) = Nothing, "", fondo.FechaEmision.Value.ToString(Constantes.CONST_FORMAT_FECHA))

                If fondo.FechaVencimiento Is Nothing Then
                    sheet.Cells(fila, 6).Value = Nothing
                Else
                    sheet.Cells(fila, 6).Value = fondo.FechaVencimiento.Value.ToString(Constantes.CONST_FORMAT_FECHA)
                End If

                sheet.Cells(fila, 7).Value = fondo.Acumulado.ToString()

                sheet.Cells(fila, 8).Value = fondo.UsuarioIngreso.ToString()
                sheet.Cells(fila, 9).Value = IIf(fondo.FechaIngreso.Value.ToString(Constantes.CONST_FORMAT_FECHA) = Nothing, "", fondo.FechaIngreso.Value.ToString(Constantes.CONST_FORMAT_FECHA))
                sheet.Cells(fila, 10).Value = fondo.UsuarioModificacion.ToString()
                sheet.Cells(fila, 11).Value = IIf(fondo.FechaModificacion.Value.ToString(Constantes.CONST_FORMAT_FECHA) = "", "", fondo.FechaModificacion.Value.ToString(Constantes.CONST_FORMAT_FECHA))

                fila = fila + 1
            Next

            Dim fileName As String = "Fondos_" + Date.Now().ToString("ddMMyyyy") + ".xlsx"
            Dim ruta As String = DirectCast(configurationAppSettings.GetValue("RutaGeneracionExcel", GetType(System.String)), String)
            Dim carpetaGeneracionExcel As String = DirectCast(configurationAppSettings.GetValue("CarpetaGeneracionExcel", GetType(System.String)), String)
            Dim fi As FileInfo
            fi = New FileInfo(ruta + fileName)
            If fi.Exists Then
                fi.Delete()
            End If

            pkg.SaveAs(fi)
            fi = New FileInfo(ruta + fileName)
            If fi.Exists Then
                rutaArchivosExcel = carpetaGeneracionExcel + fileName
                Return True
            Else
                rutaArchivosExcel = Nothing
                Return False
            End If
        Catch ex As Exception
            Throw
        End Try
        Return False
    End Function

    Public Function CrearExcelUsuarios(listaUsuarios As List(Of UsuarioDTO)) As Boolean
        Dim pkg As ExcelPackage = New ExcelPackage()
        Dim sheet As ExcelWorksheet = pkg.Workbook.Worksheets.Add("Aportantes")
        Dim fila As Integer = 1
        Dim configurationAppSettings As New System.Configuration.AppSettingsReader()

        Try
            sheet.Cells(fila, 1).Value = "Id"
            sheet.Cells(fila, 2).Value = "Nombre"
            sheet.Cells(fila, 3).Value = "Perfil"
            sheet.Cells(fila, 4).Value = "Estado"
            sheet.Cells(fila, 5).Value = "Fecha Ingreso"
            sheet.Cells(fila, 6).Value = "Usuario Ingreso"
            sheet.Cells(fila, 7).Value = "Fecha Modificacion"
            sheet.Cells(fila, 8).Value = "Usuario Modificacion"

            fila = fila + 1

            For Each usuario As UsuarioDTO In listaUsuarios

                sheet.Cells(fila, 1).Value = usuario.US_Id.ToString()
                sheet.Cells(fila, 2).Value = usuario.US_Nombre.ToString()
                sheet.Cells(fila, 3).Value = usuario.US_Perfil.ToString()
                sheet.Cells(fila, 4).Value = usuario.US_Estado.ToString()
                sheet.Cells(fila, 5).Value = usuario.US_FechaIngreso.Value.ToString(Constantes.CONST_FORMAT_FECHA)
                sheet.Cells(fila, 6).Value = usuario.US_UsuarioIngreso.ToString()
                sheet.Cells(fila, 7).Value = usuario.US_FechaModificacion.Value.ToString(Constantes.CONST_FORMAT_FECHA)
                sheet.Cells(fila, 8).Value = usuario.US_UsuarioModificacion.ToString()

                fila = fila + 1
            Next
            Dim fileName As String = "Usuarios_" + Date.Now().ToString("ddMMyyyy") + ".xlsx"
            Dim ruta As String = DirectCast(configurationAppSettings.GetValue("RutaGeneracionExcel", GetType(System.String)), String)
            Dim carpetaGeneracionExcel As String = DirectCast(configurationAppSettings.GetValue("CarpetaGeneracionExcel", GetType(System.String)), String)
            Dim fi As FileInfo
            fi = New FileInfo(ruta + fileName)
            If fi.Exists Then
                fi.Delete()
            End If

            pkg.SaveAs(fi)
            fi = New FileInfo(ruta + fileName)
            If fi.Exists Then
                rutaArchivosExcel = carpetaGeneracionExcel + fileName
                Return True
            Else
                rutaArchivosExcel = Nothing
                Return False
            End If
        Catch ex As Exception
            Throw
        End Try

    End Function

    Public Function CrearExcelTipoCambio(ListaTipoCambio As List(Of TipoCambioDTO)) As Boolean

#Disable Warning BC30002 ' No está definido el tipo 'ExcelPackage'.
#Disable Warning BC30002 ' No está definido el tipo 'ExcelPackage'.
        Dim pkg As ExcelPackage = New ExcelPackage()
#Enable Warning BC30002 ' No está definido el tipo 'ExcelPackage'.
#Enable Warning BC30002 ' No está definido el tipo 'ExcelPackage'.
#Disable Warning BC30002 ' No está definido el tipo 'ExcelWorksheet'.
        Dim sheet As ExcelWorksheet = pkg.Workbook.Worksheets.Add("Tipo de cambio")
#Enable Warning BC30002 ' No está definido el tipo 'ExcelWorksheet'.
        Dim fila As Integer = 1
        Dim configurationAppSettings As New System.Configuration.AppSettingsReader()

        Try

            sheet.Cells(fila, 1).Value = "Fecha"
            sheet.Cells(fila, 2).Value = "Moneda"
            sheet.Cells(fila, 3).Value = "Valor"
            sheet.Cells(fila, 4).Value = "FechaIngreso"
            sheet.Cells(fila, 5).Value = "UsuarioIngreso"
            sheet.Cells(fila, 6).Value = "FechaModificación"
            sheet.Cells(fila, 7).Value = "UsuarioModificación"

            fila = fila + 1

            For Each TipoCambio As TipoCambioDTO In ListaTipoCambio

                sheet.Cells(fila, 1).Value = TipoCambio.Fecha.ToShortDateString()
                sheet.Cells(fila, 2).Value = TipoCambio.Codigo.ToString()
                sheet.Cells(fila, 3).Value = TipoCambio.Valor.ToString()
                sheet.Cells(fila, 4).Value = TipoCambio.FechaIngreso.ToShortDateString()
                sheet.Cells(fila, 5).Value = TipoCambio.UsuarioIngreso.ToString()
                sheet.Cells(fila, 6).Value = TipoCambio.FechaModificacion.ToShortDateString()
                sheet.Cells(fila, 7).Value = TipoCambio.UsuarioModificacion.ToString()
                fila = fila + 1
            Next

            Dim fileName As String = "Tipo_Cambio_" + Date.Now().ToString("ddMMyyyy") + ".xlsx"
            Dim ruta As String = DirectCast(configurationAppSettings.GetValue("RutaGeneracionExcel", GetType(System.String)), String)
            Dim carpetaGeneracionExcel As String = DirectCast(configurationAppSettings.GetValue("CarpetaGeneracionExcel", GetType(System.String)), String)
            Dim fi As FileInfo
            fi = New FileInfo(ruta + fileName)
            If fi.Exists Then
                fi.Delete()
            End If

            pkg.SaveAs(fi)
            fi = New FileInfo(ruta + fileName)
            If fi.Exists Then
                rutaArchivosExcel = carpetaGeneracionExcel + fileName
                Return True
            Else
                rutaArchivosExcel = Nothing
                Return False
            End If
        Catch ex As Exception
            Throw
        End Try
        Return False
    End Function

    Public Function CrearExcelValoresCuota(listaValoresCuota As List(Of VcSerieDTO)) As Boolean
#Disable Warning BC30002 ' No está definido el tipo 'ExcelPackage'.
#Disable Warning BC30002 ' No está definido el tipo 'ExcelPackage'.
        Dim pkg As ExcelPackage = New ExcelPackage()
#Enable Warning BC30002 ' No está definido el tipo 'ExcelPackage'.
#Enable Warning BC30002 ' No está definido el tipo 'ExcelPackage'.
#Disable Warning BC30002 ' No está definido el tipo 'ExcelWorksheet'.
        Dim sheet As ExcelWorksheet = pkg.Workbook.Worksheets.Add("ValoresCuota")
#Enable Warning BC30002 ' No está definido el tipo 'ExcelWorksheet'.
        Dim fila As Integer = 1
        Dim configurationAppSettings As New System.Configuration.AppSettingsReader()
        Try

            sheet.Cells(fila, 1).Value = "RUT"
            sheet.Cells(fila, 2).Value = "Nemotecnico"
            sheet.Cells(fila, 3).Value = "Fecha"
            sheet.Cells(fila, 4).Value = "Valor"
            sheet.Cells(fila, 5).Value = "Fecha de Ingreso"
            sheet.Cells(fila, 6).Value = "Usuario Ingreso"
            sheet.Cells(fila, 7).Value = "Fecha de Modificacion"
            sheet.Cells(fila, 8).Value = "Usuario Modificacion"

            fila = fila + 1

            For Each ValoresCuota As VcSerieDTO In listaValoresCuota

                sheet.Cells(fila, 1).Value = ValoresCuota.FnRut.ToString()
                sheet.Cells(fila, 2).Value = ValoresCuota.FsNemotecnico.ToString()
                sheet.Cells(fila, 3).Value = ValoresCuota.Fecha.ToShortDateString()
                sheet.Cells(fila, 4).Value = ValoresCuota.Valor.ToString()
                sheet.Cells(fila, 5).Value = ValoresCuota.FechaIngreso.ToShortDateString()
                sheet.Cells(fila, 6).Value = ValoresCuota.UsuarioIngreso.ToString()
                sheet.Cells(fila, 7).Value = ValoresCuota.FechaModificacion.ToShortDateString()
                sheet.Cells(fila, 8).Value = ValoresCuota.UsuarioModificacion.ToString()

                fila = fila + 1
            Next

            Dim fileName As String = "ValoresCuota_" + Date.Now().ToString("ddMMyyyy") + ".xlsx"
            Dim ruta As String = DirectCast(configurationAppSettings.GetValue("RutaGeneracionExcel", GetType(System.String)), String)
            Dim carpetaGeneracionExcel As String = DirectCast(configurationAppSettings.GetValue("CarpetaGeneracionExcel", GetType(System.String)), String)
            Dim fi As FileInfo
            fi = New FileInfo(ruta + fileName)
            If fi.Exists Then
                fi.Delete()
            End If

            pkg.SaveAs(fi)
            fi = New FileInfo(ruta + fileName)
            If fi.Exists Then
                rutaArchivosExcel = carpetaGeneracionExcel + fileName
                Return True
            Else
                rutaArchivosExcel = Nothing
                Return False
            End If
        Catch ex As Exception
            Throw
        End Try
        Return False
    End Function

    Public Function CrearExcelFondoSerie(listaFondoSerie As List(Of FondoSerieDTO)) As Boolean
#Disable Warning BC30002 ' No está definido el tipo 'ExcelPackage'.
#Disable Warning BC30002 ' No está definido el tipo 'ExcelPackage'.
        Dim pkg As ExcelPackage = New ExcelPackage()
#Enable Warning BC30002 ' No está definido el tipo 'ExcelPackage'.
#Enable Warning BC30002 ' No está definido el tipo 'ExcelPackage'.
#Disable Warning BC30002 ' No está definido el tipo 'ExcelWorksheet'.
        Dim sheet As ExcelWorksheet = pkg.Workbook.Worksheets.Add("FondoSerie")
#Enable Warning BC30002 ' No está definido el tipo 'ExcelWorksheet'.
        Dim fila As Integer = 1
        Dim configurationAppSettings As New System.Configuration.AppSettingsReader()

        Try
            sheet.Cells(fila, 1).Value = "Rut"
            sheet.Cells(fila, 2).Value = "Nemotécnico"
            sheet.Cells(fila, 3).Value = "Nombrecorto"
            sheet.Cells(fila, 4).Value = "Remuneración"
            sheet.Cells(fila, 5).Value = "Nacionalidad"
            sheet.Cells(fila, 6).Value = "Calificado"
            sheet.Cells(fila, 7).Value = "Moneda"
            sheet.Cells(fila, 8).Value = "LimiteMoneda"
            sheet.Cells(fila, 9).Value = "LimiteMínimo"
            sheet.Cells(fila, 10).Value = "LimiteMáximo"
            sheet.Cells(fila, 11).Value = "ExclusivoMAM"
            sheet.Cells(fila, 12).Value = "Contrato Distribución"
            sheet.Cells(fila, 13).Value = "Canje Mandatorio"
            sheet.Cells(fila, 14).Value = "Grupo Compatible"
            sheet.Cells(fila, 15).Value = "HorarioRecaste"
            sheet.Cells(fila, 16).Value = "FondoRescatable"
            sheet.Cells(fila, 17).Value = "FechaNav"
            sheet.Cells(fila, 18).Value = "FechaRescate"
            sheet.Cells(fila, 19).Value = "FechaTCObservado"
            sheet.Cells(fila, 20).Value = "Patrimonio"
            sheet.Cells(fila, 21).Value = "FijaciónNav"
            sheet.Cells(fila, 22).Value = "FechaNavSuscripción"
            sheet.Cells(fila, 23).Value = "FechaSuscripción"
            sheet.Cells(fila, 24).Value = "FechaTCSuscripción"
            sheet.Cells(fila, 25).Value = "FijaciónSuscripción"
            sheet.Cells(fila, 26).Value = "FechaNavCanje"
            sheet.Cells(fila, 27).Value = "FechaTCCanje"
            sheet.Cells(fila, 28).Value = "FijaciónCanje"
            sheet.Cells(fila, 29).Value = "FechaIngreso"
            sheet.Cells(fila, 30).Value = "UsuarioIngreso"
            sheet.Cells(fila, 31).Value = "FechaModificación"
            sheet.Cells(fila, 32).Value = "UsuarioModificación"

            fila = fila + 1

            For Each fondoSerie As FondoSerieDTO In listaFondoSerie
                sheet.Cells(fila, 1).Value = fondoSerie.Rut.ToString()
                sheet.Cells(fila, 2).Value = fondoSerie.Nemotecnico.ToString()
                sheet.Cells(fila, 3).Value = fondoSerie.Nombrecorto.ToString()
                sheet.Cells(fila, 4).Value = fondoSerie.Remuneracion.ToString()
                sheet.Cells(fila, 5).Value = fondoSerie.Nacionalidad.ToString()
                sheet.Cells(fila, 6).Value = fondoSerie.Calificado.ToString()
                sheet.Cells(fila, 7).Value = fondoSerie.Moneda.ToString()
                sheet.Cells(fila, 8).Value = fondoSerie.LimiteMoneda.ToString()
                sheet.Cells(fila, 9).Value = fondoSerie.LimiteMinimo.ToString()
                sheet.Cells(fila, 10).Value = fondoSerie.LimiteMaximo.ToString()
                sheet.Cells(fila, 11).Value = fondoSerie.exclusivoPaso.ToString()
                sheet.Cells(fila, 12).Value = fondoSerie.compatiblePaso.ToString()
                sheet.Cells(fila, 13).Value = fondoSerie.canjePaso.ToString()
                sheet.Cells(fila, 14).Value = fondoSerie.Nivel.ToString()
                sheet.Cells(fila, 15).Value = fondoSerie.HorarioRecaste.ToString()
                sheet.Cells(fila, 16).Value = fondoSerie.FondoRescatable.ToString()
                sheet.Cells(fila, 17).Value = fondoSerie.fechaNavPaso.ToString()
                sheet.Cells(fila, 18).Value = fondoSerie.fechaRescatePaso.ToString()
                sheet.Cells(fila, 19).Value = fondoSerie.fechaTcPaso.ToString()
                sheet.Cells(fila, 20).Value = fondoSerie.Patrimonio.ToString()
                sheet.Cells(fila, 21).Value = fondoSerie.FijacionNav.ToString()
                sheet.Cells(fila, 22).Value = fondoSerie.fechaNavSusPaso.ToString()
                sheet.Cells(fila, 23).Value = fondoSerie.fechaSusPaso.ToString()
                sheet.Cells(fila, 24).Value = fondoSerie.fechaTcSusPaso.ToString()
                sheet.Cells(fila, 25).Value = fondoSerie.FijacionSuscripcion.ToString()
                sheet.Cells(fila, 26).Value = fondoSerie.fechaNavcPaso.ToString()
                sheet.Cells(fila, 27).Value = fondoSerie.fechaTcCanjePaso.ToString()
                sheet.Cells(fila, 28).Value = fondoSerie.FijacionCanje.ToString()
                sheet.Cells(fila, 29).Value = fondoSerie.FechaIngreso.ToShortDateString()
                sheet.Cells(fila, 30).Value = fondoSerie.UsuarioIngreso.ToString()
                sheet.Cells(fila, 31).Value = fondoSerie.FechaModificacion.ToShortDateString()
                sheet.Cells(fila, 32).Value = fondoSerie.UsuarioModificacion.ToString()


                fila = fila + 1
            Next
            Dim fileName As String = "FondoSerie_" + Date.Now().ToString("ddMMyyyy") + ".xlsx"
            Dim ruta As String = DirectCast(configurationAppSettings.GetValue("RutaGeneracionExcel", GetType(System.String)), String)
            Dim carpetaGeneracionExcel As String = DirectCast(configurationAppSettings.GetValue("CarpetaGeneracionExcel", GetType(System.String)), String)
            Dim fi As FileInfo
            fi = New FileInfo(ruta + fileName)
            If fi.Exists Then
                fi.Delete()
            End If

            pkg.SaveAs(fi)
            fi = New FileInfo(ruta + fileName)
            If fi.Exists Then
                rutaArchivosExcel = carpetaGeneracionExcel + fileName
                Return True
            Else
                rutaArchivosExcel = Nothing
                Return False
            End If
        Catch ex As Exception
            Throw
        End Try

    End Function

    Public Function CrearExcelCertificados(listaCertificados As List(Of CertificadoDTO)) As Boolean
#Disable Warning BC30002 ' No está definido el tipo 'ExcelPackage'.
#Disable Warning BC30002 ' No está definido el tipo 'ExcelPackage'.
        Dim pkg As ExcelPackage = New ExcelPackage()
#Enable Warning BC30002 ' No está definido el tipo 'ExcelPackage'.
#Enable Warning BC30002 ' No está definido el tipo 'ExcelPackage'.
#Disable Warning BC30002 ' No está definido el tipo 'ExcelWorksheet'.
        Dim sheet As ExcelWorksheet = pkg.Workbook.Worksheets.Add("Certificados")
#Enable Warning BC30002 ' No está definido el tipo 'ExcelWorksheet'.
        Dim fila As Integer = 1
        Dim configurationAppSettings As New System.Configuration.AppSettingsReader()
        Try

            sheet.Cells(fila, 1).Value = "Numero Documento"
            sheet.Cells(fila, 2).Value = "Correlativo"
            sheet.Cells(fila, 3).Value = "Hito ID"
            sheet.Cells(fila, 4).Value = "Fecha Corte"
            sheet.Cells(fila, 5).Value = "Fecha de Canje"
            sheet.Cells(fila, 6).Value = "Fecha"
            sheet.Cells(fila, 7).Value = "RUT Aportante"
            sheet.Cells(fila, 8).Value = "Nombre Aportante"
            sheet.Cells(fila, 9).Value = "RUT Fondo"
            sheet.Cells(fila, 10).Value = "Nombre Fondo"
            sheet.Cells(fila, 11).Value = "Multifondo Aportante"
            sheet.Cells(fila, 12).Value = "Nemotecnico"
            sheet.Cells(fila, 13).Value = "Cantidad"
            sheet.Cells(fila, 14).Value = "Fecha Ingreso"
            sheet.Cells(fila, 15).Value = "Usuario Ingreso"
            sheet.Cells(fila, 16).Value = "Fecha Modificación"
            sheet.Cells(fila, 17).Value = "Usuario Modificación"
            fila = fila + 1

            For Each Certificados As CertificadoDTO In listaCertificados

                sheet.Cells(fila, 1).Value = Certificados.CT_Numero.ToString()
                sheet.Cells(fila, 2).Value = Certificados.CT_Correlativo.ToString()
                sheet.Cells(fila, 3).Value = Certificados.HT_ID.ToString()
                sheet.Cells(fila, 4).Value = Certificados.HT_Corte.ToShortDateString()
                sheet.Cells(fila, 5).Value = Certificados.HT_Canje.ToShortDateString()
                sheet.Cells(fila, 6).Value = Certificados.CT_Fecha.ToShortDateString()
                sheet.Cells(fila, 7).Value = Certificados.AP_Rut.ToString()
                sheet.Cells(fila, 8).Value = Certificados.AP_Razon_Social.ToString()
                sheet.Cells(fila, 9).Value = Certificados.FN_Rut.ToString()
                sheet.Cells(fila, 10).Value = Certificados.FN_Nombre_Corto.ToString()
                sheet.Cells(fila, 11).Value = Certificados.AP_Multifondo.ToString()
                sheet.Cells(fila, 12).Value = Certificados.FS_Nemotecnico.ToString()
                sheet.Cells(fila, 13).Value = Certificados.CT_Cuotas.ToString()
                sheet.Cells(fila, 14).Value = Certificados.CT_Fecha_Ingreso.ToShortDateString()
                sheet.Cells(fila, 15).Value = Certificados.CT_Usuario_Ingreso.ToString()
                sheet.Cells(fila, 16).Value = Certificados.CT_Fecha_Modificacion.ToShortDateString()
                sheet.Cells(fila, 17).Value = Certificados.CT_Usuario_Modificacion.ToString()
                fila = fila + 1
            Next

            Dim fileName As String = "Certificados_" + Date.Now().ToString("ddMMyyyy") + ".xlsx"
            Dim ruta As String = DirectCast(configurationAppSettings.GetValue("RutaGeneracionExcel", GetType(System.String)), String)
            Dim carpetaGeneracionExcel As String = DirectCast(configurationAppSettings.GetValue("CarpetaGeneracionExcel", GetType(System.String)), String)
            Dim fi As FileInfo
            fi = New FileInfo(ruta + fileName)
            If fi.Exists Then
                fi.Delete()
            End If

            pkg.SaveAs(fi)
            fi = New FileInfo(ruta + fileName)
            If fi.Exists Then
                rutaArchivosExcel = carpetaGeneracionExcel + fileName
                Return True
            Else
                rutaArchivosExcel = Nothing
                Return False
            End If
        Catch ex As Exception
            Throw
        End Try
        Return False
    End Function

    Public Function CrearExcelHitos(listaHito As List(Of HitoDTO)) As Boolean
#Disable Warning BC30002 ' No está definido el tipo 'ExcelPackage'.
#Disable Warning BC30002 ' No está definido el tipo 'ExcelPackage'.
        Dim pkg As ExcelPackage = New ExcelPackage()
#Enable Warning BC30002 ' No está definido el tipo 'ExcelPackage'.
#Enable Warning BC30002 ' No está definido el tipo 'ExcelPackage'.
#Disable Warning BC30002 ' No está definido el tipo 'ExcelWorksheet'.
        Dim sheet As ExcelWorksheet = pkg.Workbook.Worksheets.Add("Hitos")
#Enable Warning BC30002 ' No está definido el tipo 'ExcelWorksheet'.
        Dim fila As Integer = 1
        Dim configurationAppSettings As New System.Configuration.AppSettingsReader()
        Try

            sheet.Cells(fila, 1).Value = "Id Hito"
            sheet.Cells(fila, 2).Value = "Rut Fondo"
            sheet.Cells(fila, 3).Value = "Nombre Fondo"
            sheet.Cells(fila, 4).Value = "Fecha Corte"
            sheet.Cells(fila, 5).Value = "Fecha Canje"
            sheet.Cells(fila, 6).Value = "Fecha Ingreso"
            sheet.Cells(fila, 7).Value = "Usuario Ingreso"
            sheet.Cells(fila, 8).Value = "Fecha Modificación"
            sheet.Cells(fila, 9).Value = "Usuario Modificación"

            fila = fila + 1

            For Each Hito As HitoDTO In listaHito

                sheet.Cells(fila, 1).Value = Hito.IdHito.ToString()
                sheet.Cells(fila, 2).Value = Hito.Rut.ToString()
                sheet.Cells(fila, 3).Value = Hito.NombreFondo.ToString()
                sheet.Cells(fila, 4).Value = Hito.FechaCorte.ToShortDateString()
                sheet.Cells(fila, 5).Value = Hito.FechaCanje.ToShortDateString()
                sheet.Cells(fila, 6).Value = Hito.FechaIngreso.ToShortDateString()
                sheet.Cells(fila, 7).Value = Hito.UsuarioIngreso.ToString()
                sheet.Cells(fila, 8).Value = Hito.FechaModificacion.ToShortDateString()
                sheet.Cells(fila, 9).Value = Hito.UsuarioModificacion.ToString()
                fila = fila + 1
            Next

            Dim fileName As String = "Hitos_" + Date.Now().ToString("ddMMyyyy") + ".xlsx"
            Dim ruta As String = DirectCast(configurationAppSettings.GetValue("RutaGeneracionExcel", GetType(System.String)), String)
            Dim carpetaGeneracionExcel As String = DirectCast(configurationAppSettings.GetValue("CarpetaGeneracionExcel", GetType(System.String)), String)
            Dim fi As FileInfo
            fi = New FileInfo(ruta + fileName)
            If fi.Exists Then
                fi.Delete()
            End If

            pkg.SaveAs(fi)
            fi = New FileInfo(ruta + fileName)
            If fi.Exists Then
                rutaArchivosExcel = carpetaGeneracionExcel + fileName
                Return True
            Else
                rutaArchivosExcel = Nothing
                Return False
            End If
        Catch ex As Exception
            Throw
        End Try
        Return False
    End Function


    Public Function CrearExcelSuscripcion(ListaSuscripcion As List(Of SuscripcionDTO)) As Boolean
        Dim pkg As ExcelPackage = New ExcelPackage()
        Dim sheet As ExcelWorksheet = pkg.Workbook.Worksheets.Add("Suscripción")
        Dim fila As Integer = 1
        Dim configurationAppSettings As New System.Configuration.AppSettingsReader()

        Try
            sheet.Cells(fila, 1).Value = “Id Suscripción”
            sheet.Cells(fila, 2).Value = “Tipo transacción”
            sheet.Cells(fila, 3).Value = “Fecha intención”
            sheet.Cells(fila, 4).Value = “Rut aportante”
            sheet.Cells(fila, 5).Value = “Nombre aportante”
            sheet.Cells(fila, 6).Value = “Multifondo”
            sheet.Cells(fila, 7).Value = “Rut fondo”
            sheet.Cells(fila, 8).Value = “Nombre fondo”
            sheet.Cells(fila, 9).Value = “Nemotécnico”
            sheet.Cells(fila, 10).Value = “Serie”
            sheet.Cells(fila, 11).Value = “Moneda serie”
            sheet.Cells(fila, 12).Value = “Cuotas a suscribir”
            sheet.Cells(fila, 13).Value = “Moneda pago”
            sheet.Cells(fila, 14).Value = “Fecha NAV”
            sheet.Cells(fila, 15).Value = “Fecha Suscripción”
            sheet.Cells(fila, 16).Value = “Fecha TC”
            sheet.Cells(fila, 17).Value = “NAV”
            sheet.Cells(fila, 18).Value = “Monto”
            sheet.Cells(fila, 19).Value = “NAV (CLP)”
            sheet.Cells(fila, 20).Value = “Monto (CLP)”
            sheet.Cells(fila, 21).Value = “Contrato fondo”
            sheet.Cells(fila, 22).Value = “Revisión de Poderes”
            sheet.Cells(fila, 23).Value = “Observaciones”
            sheet.Cells(fila, 24).Value = “Fecha DCV”
            sheet.Cells(fila, 25).Value = “Cuotas DCV”
            sheet.Cells(fila, 26).Value = “Rescates en tránsito”
            sheet.Cells(fila, 27).Value = “Suscripciones en tránsito”
            sheet.Cells(fila, 28).Value = “Canjes en tránsito”
            sheet.Cells(fila, 29).Value = “Cuotas disponibles”
            sheet.Cells(fila, 30).Value = “Fijación Nav”
            sheet.Cells(fila, 31).Value = “Tc Observado”
            sheet.Cells(fila, 32).Value = “Fijación TC”
            sheet.Cells(fila, 33).Value = “Estado”
            sheet.Cells(fila, 34).Value = “Cuotas Emitidas”
            sheet.Cells(fila, 35).Value = “Acumulada”
            sheet.Cells(fila, 36).Value = “Actual”
            sheet.Cells(fila, 37).Value = “Utilizado”
            sheet.Cells(fila, 38).Value = “Disponible”
            sheet.Cells(fila, 39).Value = “Fecha Ingreso”
            sheet.Cells(fila, 40).Value = “Usuario Ingreso”
            sheet.Cells(fila, 41).Value = “Fecha Modificación”
            sheet.Cells(fila, 42).Value = “Usuario Modificador”

            fila = fila + 1

            For Each Suscripcion As SuscripcionDTO In ListaSuscripcion

                sheet.Cells(fila, 1).Value = Suscripcion.IdSuscripcion.ToString()
                sheet.Cells(fila, 2).Value = Suscripcion.TipoTransaccion.ToString()
                sheet.Cells(fila, 3).Value = Suscripcion.FechaIntencion.ToString("dd/MM/yyyy")
                sheet.Cells(fila, 4).Value = Suscripcion.RutAportante.ToString()
                sheet.Cells(fila, 5).Value = Suscripcion.RazonSocial.ToString()
                sheet.Cells(fila, 6).Value = Suscripcion.Multifondo.ToString()
                sheet.Cells(fila, 7).Value = Suscripcion.RutFondo.ToString()
                sheet.Cells(fila, 8).Value = Suscripcion.FondoNombreCorto.ToString()
                sheet.Cells(fila, 9).Value = Suscripcion.Nemotecnico.ToString()
                sheet.Cells(fila, 10).Value = Suscripcion.SerieNombreCorto.ToString()
                sheet.Cells(fila, 11).Value = Suscripcion.MonedaSerie.ToString()
                sheet.Cells(fila, 12).Value = Suscripcion.CuotasASuscribir.ToString()
                sheet.Cells(fila, 13).Value = Suscripcion.Moneda_Pago.ToString()
                sheet.Cells(fila, 14).Value = Suscripcion.FechaNAV.ToString("dd/MM/yyyy")
                sheet.Cells(fila, 15).Value = Suscripcion.FechaSuscripcion.ToString("dd/MM/yyyy")
                sheet.Cells(fila, 16).Value = Suscripcion.FechaTC.ToString("dd/MM/yyyy")
                sheet.Cells(fila, 17).Value = Suscripcion.NavFormat.ToString()
                sheet.Cells(fila, 18).Value = Suscripcion.Monto.ToString()
                sheet.Cells(fila, 19).Value = Suscripcion.NAVCLP.ToString()
                sheet.Cells(fila, 20).Value = Suscripcion.MontoCLP.ToString()
                sheet.Cells(fila, 21).Value = Suscripcion.ContratoFondo.ToString()
                sheet.Cells(fila, 22).Value = Suscripcion.RevisionPoderes.ToString()
                sheet.Cells(fila, 23).Value = Suscripcion.Observaciones.ToString()
                sheet.Cells(fila, 24).Value = Suscripcion.FechaDCV.ToString("dd/MM/yyyy")
                sheet.Cells(fila, 25).Value = Suscripcion.CuotasDCV.ToString()
                sheet.Cells(fila, 26).Value = Suscripcion.RescatesTransitos.ToString()
                sheet.Cells(fila, 27).Value = Suscripcion.SuscripcionesTransito.ToString()
                sheet.Cells(fila, 28).Value = Suscripcion.CanjesTransito.ToString()
                sheet.Cells(fila, 29).Value = Suscripcion.CuotasDisponibles.ToString()
                sheet.Cells(fila, 30).Value = Suscripcion.FijacionNAV.ToString()
                sheet.Cells(fila, 31).Value = Suscripcion.TcObservado.ToString()
                sheet.Cells(fila, 32).Value = Suscripcion.FijacionTC.ToString()
                sheet.Cells(fila, 33).Value = Suscripcion.EstadoSuscripcion.ToString()
                sheet.Cells(fila, 34).Value = Suscripcion.CuotasEmitidas.ToString()
                sheet.Cells(fila, 35).Value = Suscripcion.FnAcumulada.ToString()
                sheet.Cells(fila, 36).Value = Suscripcion.ScActual.ToString()
                sheet.Cells(fila, 37).Value = Suscripcion.ScUtilizado.ToString()
                sheet.Cells(fila, 38).Value = Suscripcion.ScDisponibles.ToString()
                sheet.Cells(fila, 39).Value = Suscripcion.ScFechaIngreso.ToString()
                sheet.Cells(fila, 40).Value = Suscripcion.ScUsuarioIngreso.ToString()
                sheet.Cells(fila, 41).Value = Suscripcion.ScFechaModificacion.ToString()
                sheet.Cells(fila, 42).Value = Suscripcion.ScUsuarioModificacion.ToString()
                fila = fila + 1
            Next

            Dim fileName As String = "Suscripcion_" + Date.Now().ToString("ddMMyyyy") + ".xlsx"
            Dim ruta As String = DirectCast(configurationAppSettings.GetValue("RutaGeneracionExcel", GetType(System.String)), String)
            Dim carpetaGeneracionExcel As String = DirectCast(configurationAppSettings.GetValue("CarpetaGeneracionExcel", GetType(System.String)), String)
            Dim fi As FileInfo
            fi = New FileInfo(ruta + fileName)
            If fi.Exists Then
                fi.Delete()
            End If

            pkg.SaveAs(fi)
            fi = New FileInfo(ruta + fileName)
            If fi.Exists Then
                rutaArchivosExcel = carpetaGeneracionExcel + fileName
                Return True
            Else
                rutaArchivosExcel = Nothing
                Return False
            End If
        Catch ex As Exception
            Throw
        End Try
        Return False
    End Function
    Public Function CrearExcelCanje(listaCanje As List(Of CanjeDTO)) As Boolean
        Dim pkg As ExcelPackage = New ExcelPackage()
        Dim sheet As ExcelWorksheet = pkg.Workbook.Worksheets.Add("Canjes")

        Dim fila As Integer = 1
        Dim configurationAppSettings As New System.Configuration.AppSettingsReader()
        Try

            sheet.Cells(fila, 1).Value = "Id Canje"
            sheet.Cells(fila, 2).Value = "Tipo Transacción"
            sheet.Cells(fila, 3).Value = "Rut Aportante"
            sheet.Cells(fila, 4).Value = "Multifondo"
            sheet.Cells(fila, 5).Value = "Nombre Aportante"
            sheet.Cells(fila, 6).Value = "Rut Fondo"
            sheet.Cells(fila, 7).Value = "Nombre Fondo"
            sheet.Cells(fila, 8).Value = "Fecha Nav Saliente"
            sheet.Cells(fila, 9).Value = "Fecha Solicitud"
            sheet.Cells(fila, 10).Value = "Fecha TC Observado"
            sheet.Cells(fila, 11).Value = "Fecha Canje"
            sheet.Cells(fila, 12).Value = "Nemotécnico Saliente"
            sheet.Cells(fila, 13).Value = "Nombre Serie Saliente"
            sheet.Cells(fila, 14).Value = "Moneda Saliente"
            sheet.Cells(fila, 15).Value = "Cuotas Salientes"
            sheet.Cells(fila, 16).Value = "Fecha Valor Cuota"
            sheet.Cells(fila, 17).Value = "Nav Saliente"
            sheet.Cells(fila, 18).Value = "Monto Saliente"
            sheet.Cells(fila, 19).Value = "Nav CLP Saliente"
            sheet.Cells(fila, 20).Value = "Monto CLP Saliente"
            sheet.Cells(fila, 21).Value = "Factor"
            sheet.Cells(fila, 22).Value = "Diferencia"
            sheet.Cells(fila, 23).Value = "Diferencia CLP"
            sheet.Cells(fila, 24).Value = "Fecha Nav Entrante"
            sheet.Cells(fila, 25).Value = "Nemotécnico Entrante"
            sheet.Cells(fila, 26).Value = "Nombre Serie Entrante"
            sheet.Cells(fila, 27).Value = "Moneda Entrante"
            sheet.Cells(fila, 28).Value = "Cuotas Entrantes"
            sheet.Cells(fila, 29).Value = "Nav Entrante"
            sheet.Cells(fila, 30).Value = "Monto Entrante"
            sheet.Cells(fila, 31).Value = "Nav CLP Entrante"
            sheet.Cells(fila, 32).Value = "Monto CLP Entrante"
            sheet.Cells(fila, 33).Value = "Contrato General"
            sheet.Cells(fila, 34).Value = "Revisión Poderes"
            sheet.Cells(fila, 35).Value = "Estado Canje"
            sheet.Cells(fila, 36).Value = "Observaciones"
            sheet.Cells(fila, 37).Value = "Fecha DCV Tránsito"
            sheet.Cells(fila, 38).Value = "Cuotas DCV Tránsito"
            sheet.Cells(fila, 39).Value = "Rescate Tránsito"
            sheet.Cells(fila, 40).Value = "Suscripción Tránsito"
            sheet.Cells(fila, 41).Value = "Canje Tránsito"
            sheet.Cells(fila, 42).Value = "Cuotas Disponibles"
            sheet.Cells(fila, 43).Value = "Fijación Nav"
            sheet.Cells(fila, 44).Value = "Fijación TC"
            sheet.Cells(fila, 45).Value = "Tipo Cambio"
            sheet.Cells(fila, 46).Value = "Fecha Ingreso"
            sheet.Cells(fila, 47).Value = "Usuario Ingreso"
            sheet.Cells(fila, 48).Value = "Fecha Modificación"
            sheet.Cells(fila, 49).Value = "Usuario Modificación"
            fila = fila + 1

            For Each Canje As CanjeDTO In listaCanje

                sheet.Cells(fila, 1).Value = Canje.IdCanje.ToString()
                sheet.Cells(fila, 2).Value = Canje.TipoTransaccion.ToString()
                sheet.Cells(fila, 3).Value = Canje.RutAportante.ToString()
                sheet.Cells(fila, 4).Value = Canje.Multifondo.ToString()
                sheet.Cells(fila, 5).Value = Canje.NombreAportante.ToString()
                sheet.Cells(fila, 6).Value = Canje.RutFondo.ToString()
                sheet.Cells(fila, 7).Value = Canje.NombreFondo.ToString()
                sheet.Cells(fila, 8).Value = Canje.FechaNavSaliente.ToShortDateString()
                sheet.Cells(fila, 9).Value = Canje.FechaSolicitud.ToShortDateString()
                sheet.Cells(fila, 10).Value = Canje.FechaObservado.ToShortDateString()
                sheet.Cells(fila, 11).Value = Canje.FechaCanje
                sheet.Cells(fila, 12).Value = Canje.NemotecnicoSaliente.ToString()
                sheet.Cells(fila, 13).Value = Canje.NombreSerieSaliente.ToString()
                sheet.Cells(fila, 14).Value = Canje.MonedaSaliente.ToString()
                sheet.Cells(fila, 15).Value = Canje.cuotaSalientePaso.ToString()
                sheet.Cells(fila, 16).Value = Canje.NavSalientePaso
                sheet.Cells(fila, 17).Value = Canje.MontoSalientePaso
                sheet.Cells(fila, 18).Value = Canje.NavCLPSalientePaso
                sheet.Cells(fila, 19).Value = Canje.MontoSalienteCLPPaso
                sheet.Cells(fila, 20).Value = Canje.Factor.ToString()
                sheet.Cells(fila, 21).Value = Canje.Diferencia.ToString()
                sheet.Cells(fila, 22).Value = Canje.DiferenciaCLP.ToString()
                sheet.Cells(fila, 23).Value = Canje.FechaNavEntrante.ToShortDateString()
                sheet.Cells(fila, 24).Value = Canje.NemotecnicoEntrante.ToString()
                sheet.Cells(fila, 25).Value = Canje.NombreSerieEntrante.ToString()
                sheet.Cells(fila, 26).Value = Canje.MonedaEntrante.ToString()
                sheet.Cells(fila, 27).Value = Canje.cuotaEntrantePaso
                sheet.Cells(fila, 28).Value = Canje.NavEntrantePaso
                sheet.Cells(fila, 29).Value = Canje.MontoEntrantePaso
                sheet.Cells(fila, 30).Value = Canje.NavCLPEntrantePaso
                sheet.Cells(fila, 31).Value = Canje.MontoCLPEntrantePaso
                sheet.Cells(fila, 32).Value = Canje.ContratoGeneral.ToString()
                sheet.Cells(fila, 33).Value = Canje.RevisionPoderes.ToString()
                sheet.Cells(fila, 34).Value = Canje.EstadoCanje.ToString()
                sheet.Cells(fila, 35).Value = Canje.Observaciones.ToString()
                sheet.Cells(fila, 36).Value = Canje.FechaActual.ToShortDateString()
                sheet.Cells(fila, 37).Value = Canje.Cuotas.ToString()
                sheet.Cells(fila, 38).Value = Canje.RescateTransito.ToString()
                sheet.Cells(fila, 39).Value = Canje.SuscripcionTransito.ToString()
                sheet.Cells(fila, 40).Value = Canje.CanjeTransito.ToString()
                sheet.Cells(fila, 41).Value = Canje.cuotasDisponiblesPaso
                sheet.Cells(fila, 42).Value = Canje.FijacionNav.ToString()
                sheet.Cells(fila, 43).Value = Canje.FijacionTC.ToString()
                sheet.Cells(fila, 44).Value = Canje.TipoCambio.ToString()
                sheet.Cells(fila, 45).Value = Canje.FechaIngreso.ToShortDateString()
                sheet.Cells(fila, 46).Value = Canje.UsuarioIngreso.ToString()
                sheet.Cells(fila, 47).Value = Canje.FechaModificacion.ToShortDateString()
                sheet.Cells(fila, 48).Value = Canje.UsuarioModificacion.ToString()

                fila = fila + 1
            Next

            Dim fileName As String = "Canjes_" + Date.Now().ToString("ddMMyyyy") + ".xlsx"
            Dim ruta As String = DirectCast(configurationAppSettings.GetValue("RutaGeneracionExcel", GetType(System.String)), String)
            Dim carpetaGeneracionExcel As String = DirectCast(configurationAppSettings.GetValue("CarpetaGeneracionExcel", GetType(System.String)), String)
            Dim fi As FileInfo
            fi = New FileInfo(ruta + fileName)
            If fi.Exists Then
                fi.Delete()
            End If

            pkg.SaveAs(fi)
            fi = New FileInfo(ruta + fileName)
            If fi.Exists Then
                rutaArchivosExcel = carpetaGeneracionExcel + fileName
                Return True
            Else
                rutaArchivosExcel = Nothing
                Return False
            End If
        Catch ex As Exception
            Throw
        End Try
        Return False
    End Function

    Public Function CrearExcelVentanasRescate(listaVentanasRescate As List(Of VentanasRescateDTO)) As Boolean
#Disable Warning BC30002 ' No está definido el tipo 'ExcelPackage'.
#Disable Warning BC30002 ' No está definido el tipo 'ExcelPackage'.
        Dim pkg As ExcelPackage = New ExcelPackage()
#Enable Warning BC30002 ' No está definido el tipo 'ExcelPackage'.
#Enable Warning BC30002 ' No está definido el tipo 'ExcelPackage'.

#Disable Warning BC30002 ' No está definido el tipo 'ExcelWorksheet'.
        Dim sheet As ExcelWorksheet = pkg.Workbook.Worksheets.Add("VentanasRescate")
#Enable Warning BC30002 ' No está definido el tipo 'ExcelWorksheet'.
        Dim fila As Integer = 1
        Dim configurationAppSettings As New System.Configuration.AppSettingsReader()
        Try

            sheet.Cells(fila, 1).Value = "Fondo"
            sheet.Cells(fila, 2).Value = "Nemotécnico"
            sheet.Cells(fila, 3).Value = "Fecha Solicitud"
            sheet.Cells(fila, 4).Value = "Fecha NAV"
            sheet.Cells(fila, 5).Value = "Fecha Pago"
            sheet.Cells(fila, 6).Value = "Usuario Ingreso"
            sheet.Cells(fila, 7).Value = "Fecha Ingreso"
            sheet.Cells(fila, 8).Value = "Usuario Modificación"
            sheet.Cells(fila, 9).Value = "Fecha Modificación"

            fila = fila + 1

            For Each VentanasRescate As VentanasRescateDTO In listaVentanasRescate

                sheet.Cells(fila, 1).Value = VentanasRescate.FN_Nombre_Corto.ToString()
                sheet.Cells(fila, 2).Value = VentanasRescate.FS_Nemotecnico.ToString()
                sheet.Cells(fila, 3).Value = VentanasRescate.RES_Fecha_Solicitud.ToShortDateString()
                sheet.Cells(fila, 4).Value = VentanasRescate.VTRES_Fecha_NAV.ToShortDateString()
                sheet.Cells(fila, 5).Value = VentanasRescate.VTRES_Fecha_Pago.ToShortDateString()
                sheet.Cells(fila, 6).Value = VentanasRescate.VTRES_Usuario_Ingreso.ToString()
                sheet.Cells(fila, 7).Value = VentanasRescate.VTRES_Fecha_Ingreso.ToShortDateString()
                sheet.Cells(fila, 8).Value = VentanasRescate.VTRES_Usuario_Modificacion.ToString()
                sheet.Cells(fila, 9).Value = VentanasRescate.VTRES_Fecha_Modificacion.ToShortDateString()

                fila = fila + 1
            Next

            Dim fileName As String = "VentanasRescate_" + Date.Now().ToString("ddMMyyyy") + ".xlsx"
            Dim ruta As String = DirectCast(configurationAppSettings.GetValue("RutaGeneracionExcel", GetType(System.String)), String)
            Dim carpetaGeneracionExcel As String = DirectCast(configurationAppSettings.GetValue("CarpetaGeneracionExcel", GetType(System.String)), String)
            Dim fi As FileInfo
            fi = New FileInfo(ruta + fileName)
            If fi.Exists Then
                fi.Delete()
            End If

            pkg.SaveAs(fi)
            fi = New FileInfo(ruta + fileName)
            If fi.Exists Then
                rutaArchivosExcel = carpetaGeneracionExcel + fileName
                Return True
            Else
                rutaArchivosExcel = Nothing
                Return False
            End If
        Catch ex As Exception
            Throw
        End Try
        Return False
    End Function


    Public Function CrearExcelRescates(listaRescates As List(Of RescatesDTO)) As Boolean
#Disable Warning BC30002 ' No está definido el tipo 'ExcelPackage'.
#Disable Warning BC30002 ' No está definido el tipo 'ExcelPackage'.
        Dim pkg As ExcelPackage = New ExcelPackage()
#Enable Warning BC30002 ' No está definido el tipo 'ExcelPackage'.
#Enable Warning BC30002 ' No está definido el tipo 'ExcelPackage'.
#Disable Warning BC30002 ' No está definido el tipo 'ExcelWorksheet'.
        Dim sheet As ExcelWorksheet = pkg.Workbook.Worksheets.Add("Rescates")
#Enable Warning BC30002 ' No está definido el tipo 'ExcelWorksheet'.
        Dim fila As Integer = 1
        Dim configurationAppSettings As New System.Configuration.AppSettingsReader()
        Try

            sheet.Cells(fila, 1).Value = "ID"
            sheet.Cells(fila, 2).Value = "Tipo Transacción"
            sheet.Cells(fila, 3).Value = "Fecha Solicitud"
            sheet.Cells(fila, 4).Value = "RUT Aportante"
            sheet.Cells(fila, 5).Value = "Razón Social"
            sheet.Cells(fila, 6).Value = "Multifondo"
            sheet.Cells(fila, 7).Value = "RUT Fondo"
            sheet.Cells(fila, 8).Value = "Nombre Fondo"
            sheet.Cells(fila, 9).Value = "Nemotécnico"
            sheet.Cells(fila, 10).Value = "Serie"
            sheet.Cells(fila, 11).Value = "Moneda Serie"
            sheet.Cells(fila, 12).Value = "Cuotas a Rescatar"
            sheet.Cells(fila, 13).Value = "Moneda Pago"
            sheet.Cells(fila, 14).Value = "Cuotas DCV"
            sheet.Cells(fila, 15).Value = "Fecha Nav"
            sheet.Cells(fila, 16).Value = "Fecha Rescate"
            sheet.Cells(fila, 17).Value = "Fecha TC"
            sheet.Cells(fila, 18).Value = "NAV"
            sheet.Cells(fila, 19).Value = "Monto"
            sheet.Cells(fila, 20).Value = "NAV CLP"
            sheet.Cells(fila, 21).Value = "Monto CLP"
            sheet.Cells(fila, 22).Value = "Tipo Cambio"
            sheet.Cells(fila, 23).Value = "Contrato"
            sheet.Cells(fila, 24).Value = "Poderes"
            sheet.Cells(fila, 25).Value = "Estado"
            sheet.Cells(fila, 26).Value = "Observaciones"
            sheet.Cells(fila, 27).Value = "Patrimonio"
            sheet.Cells(fila, 28).Value = "% Patrimonio"
            sheet.Cells(fila, 29).Value = "Rescates Máximo"
            sheet.Cells(fila, 30).Value = "Patrimonio Utilizado"
            sheet.Cells(fila, 31).Value = "Disponible Patrimonio"
            sheet.Cells(fila, 32).Value = "Fecha DCV"
            sheet.Cells(fila, 33).Value = "Rescates en Tránsito"
            sheet.Cells(fila, 34).Value = "Suscripciones en Tránsito"
            sheet.Cells(fila, 35).Value = "Canjes en Tránsito"
            sheet.Cells(fila, 36).Value = "Cuotas Disponibles"
            sheet.Cells(fila, 37).Value = "Fijación NAV"
            sheet.Cells(fila, 38).Value = "Fijación TC Observado"
            sheet.Cells(fila, 39).Value = "Fecha Ingreso"
            sheet.Cells(fila, 40).Value = "Usuario Ingreso"
            sheet.Cells(fila, 41).Value = "Fecha Modificación"
            sheet.Cells(fila, 42).Value = "Usuario Modificación"
            fila = fila + 1

            For Each Rescates As RescatesDTO In listaRescates

                sheet.Cells(fila, 1).Value = Rescates.RES_ID.ToString()
                sheet.Cells(fila, 2).Value = Rescates.RES_Tipo_Transaccion.ToString()
                sheet.Cells(fila, 3).Value = Rescates.RES_Fecha_Solicitud.ToShortDateString()
                sheet.Cells(fila, 4).Value = Rescates.AP_RUT.ToString()
                sheet.Cells(fila, 5).Value = Rescates.AP_Razon_Social.ToString()
                sheet.Cells(fila, 6).Value = Rescates.AP_Multifondo.ToString()
                sheet.Cells(fila, 7).Value = Rescates.FN_RUT.ToString()
                sheet.Cells(fila, 8).Value = Rescates.FN_Nombre_Corto.ToString()
                sheet.Cells(fila, 9).Value = Rescates.FS_Nemotecnico.ToString()
                sheet.Cells(fila, 10).Value = Rescates.FS_Nombre_Corto.ToString()
                sheet.Cells(fila, 11).Value = Rescates.FS_Moneda.ToString()
                sheet.Cells(fila, 12).Value = Rescates.RES_Cuotas.ToString()
                sheet.Cells(fila, 13).Value = Rescates.RES_Moneda_Pago.ToString()
                sheet.Cells(fila, 14).Value = Rescates.ADCV_Cantidad.ToString()
                sheet.Cells(fila, 15).Value = Rescates.RES_Fecha_Nav.ToShortDateString()
                sheet.Cells(fila, 16).Value = Rescates.RES_Fecha_Pago.ToShortDateString()
                sheet.Cells(fila, 17).Value = Rescates.RES_FechaTCObs.ToShortDateString()
                sheet.Cells(fila, 18).Value = Rescates.RES_Nav.ToString()
                sheet.Cells(fila, 19).Value = Rescates.RES_Monto.ToString()
                sheet.Cells(fila, 20).Value = Rescates.RES_Nav_CLP.ToString()
                sheet.Cells(fila, 21).Value = Rescates.RES_Monto_CLP.ToString()
                sheet.Cells(fila, 22).Value = Rescates.TC_Valor.ToString()
                sheet.Cells(fila, 23).Value = Rescates.RES_Contrato.ToString()
                sheet.Cells(fila, 24).Value = Rescates.RES_Poderes.ToString()
                sheet.Cells(fila, 25).Value = Rescates.RES_Estado.ToString()
                sheet.Cells(fila, 26).Value = Rescates.RES_Observaciones.ToString()
                sheet.Cells(fila, 27).Value = Rescates.RES_Patrimonio.ToString()
                sheet.Cells(fila, 28).Value = Rescates.FS_Patrimonio.ToString()
                sheet.Cells(fila, 29).Value = Rescates.RES_Maximo.ToString()
                sheet.Cells(fila, 30).Value = Rescates.RES_Utilizado.ToString()
                sheet.Cells(fila, 31).Value = Rescates.RES_Disponible_Patrimonio.ToString()
                sheet.Cells(fila, 32).Value = Rescates.ADCV_Fecha.ToShortDateString()
                sheet.Cells(fila, 33).Value = Rescates.RES_Transito.ToString()
                sheet.Cells(fila, 34).Value = Rescates.SC_Cuotas_a_Suscribir.ToString()
                sheet.Cells(fila, 35).Value = Rescates.CN_Cuotas_Disponibles.ToString()
                sheet.Cells(fila, 36).Value = Rescates.RES_Cuotas_Disponibles.ToString()
                sheet.Cells(fila, 37).Value = Rescates.RES_Fijacion_NAV.ToString()
                sheet.Cells(fila, 38).Value = Rescates.RES_Fijacion_TCObservado.ToString()
                sheet.Cells(fila, 39).Value = Rescates.RES_Fecha_Ingreso.ToShortDateString()
                sheet.Cells(fila, 40).Value = Rescates.RES_Usuario_Ingreso.ToString()
                sheet.Cells(fila, 41).Value = Rescates.RES_Fecha_Modificacion.ToShortDateString()
                sheet.Cells(fila, 42).Value = Rescates.RES_Usuario_Modificacion.ToString()
                fila = fila + 1
            Next

            Dim fileName As String = "Rescates_" + Date.Now().ToString("ddMMyyyy") + ".xlsx"
            Dim ruta As String = DirectCast(configurationAppSettings.GetValue("RutaGeneracionExcel", GetType(System.String)), String)
            Dim carpetaGeneracionExcel As String = DirectCast(configurationAppSettings.GetValue("CarpetaGeneracionExcel", GetType(System.String)), String)
            Dim fi As FileInfo
            fi = New FileInfo(ruta + fileName)
            If fi.Exists Then
                fi.Delete()
            End If

            pkg.SaveAs(fi)
            fi = New FileInfo(ruta + fileName)
            If fi.Exists Then
                rutaArchivosExcel = carpetaGeneracionExcel + fileName
                Return True
            Else
                rutaArchivosExcel = Nothing
                Return False
            End If
        Catch ex As Exception
            Throw
        End Try
        Return False
    End Function

    Public Function CrearExcelFijacion(ListaFijacion As List(Of FijacionDTO)) As Boolean
        Dim pkg As ExcelPackage = New ExcelPackage()
        Dim sheet As ExcelWorksheet = pkg.Workbook.Worksheets.Add("Fijacion")
        Dim fila As Integer = 1
        Dim configurationAppSettings As New System.Configuration.AppSettingsReader()
        Try
            sheet.Cells(fila, 1).Value = "ID"
            sheet.Cells(fila, 2).Value = "Tipo de Transacción"
            sheet.Cells(fila, 3).Value = "RUT Aportante"
            sheet.Cells(fila, 4).Value = "Nombre/Razón Social"
            sheet.Cells(fila, 5).Value = "RUT del Fondo"
            sheet.Cells(fila, 6).Value = "Nombre del Fondo"
            sheet.Cells(fila, 7).Value = "Fecha NAV"
            sheet.Cells(fila, 8).Value = "Fecha TC Observado"
            sheet.Cells(fila, 9).Value = "Fecha de Pago"
            sheet.Cells(fila, 10).Value = "Nav Fijado"
            sheet.Cells(fila, 11).Value = "Tc Observado"
            sheet.Cells(fila, 12).Value = "Fijación NAV"
            sheet.Cells(fila, 13).Value = "Fijación TC Observado"
            sheet.Cells(fila, 14).Value = "Nemotécnico"
            fila = fila + 1

            For Each Fijacion As FijacionDTO In ListaFijacion

                sheet.Cells(fila, 1).Value = Fijacion.ID.ToString()
                sheet.Cells(fila, 2).Value = Fijacion.TipoTransaccion.ToString()
                sheet.Cells(fila, 3).Value = Fijacion.ApRut.ToString()
                sheet.Cells(fila, 4).Value = Fijacion.RazonSocial.ToString()
                sheet.Cells(fila, 5).Value = Fijacion.Rut.ToString()
                sheet.Cells(fila, 6).Value = Fijacion.FnNombreCorto.ToString()
                sheet.Cells(fila, 7).Value = Fijacion.FechaNav.ToShortDateString()
                sheet.Cells(fila, 8).Value = Fijacion.FechaTCObs.ToShortDateString()
                sheet.Cells(fila, 9).Value = Fijacion.fechaPago.ToShortDateString()
                sheet.Cells(fila, 10).Value = Fijacion.NAV_FIJADO.ToString()
                sheet.Cells(fila, 11).Value = Fijacion.TC_OBSERVADO.ToString()
                sheet.Cells(fila, 12).Value = Fijacion.FijacionNAV.ToString()
                sheet.Cells(fila, 13).Value = Fijacion.FijacionTCObservado.ToString()
                sheet.Cells(fila, 14).Value = Fijacion.Nemotecnico.ToString()

                fila = fila + 1
            Next

            Dim fileName As String = "Fijacion_" + Date.Now().ToString("ddMMyyyy") + ".xlsx"
            Dim ruta As String = DirectCast(configurationAppSettings.GetValue("RutaGeneracionExcel", GetType(System.String)), String)
            Dim carpetaGeneracionExcel As String = DirectCast(configurationAppSettings.GetValue("CarpetaGeneracionExcel", GetType(System.String)), String)
            Dim fi As FileInfo

            fi = New FileInfo(ruta + fileName)
            If fi.Exists Then
                fi.Delete()
            End If

            pkg.SaveAs(fi)
            fi = New FileInfo(ruta + fileName)
            If fi.Exists Then
                rutaArchivosExcel = carpetaGeneracionExcel + fileName
                Return True
            Else
                rutaArchivosExcel = Nothing
                Return False
            End If
        Catch ex As Exception
            Throw
        End Try
        Return False
    End Function

End Class
