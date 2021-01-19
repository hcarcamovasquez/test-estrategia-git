Imports System.IO
Imports System.Text

Public Class exportarWord

    Public Property ArchivoTemplateWORD As String
    Public Property sFileDestinoWord As String

    Public Property sFileMarcadores As String

    Public Property SepDerecha As String
    Public Property SepIzquierda As String

    Public Property nTotalMarcadores As Integer
    Public Property nTotalDetalleTabla As Integer

    Public Property oLineas As List(Of TMarcadores)
    Public Property oLineasTabla() As List(Of TMarcadores)

    Public Property oTablas As Collection

    Public Property m_StrMensajeNoHayRegistros As String
    Public Property m_bEsConAnexo As Boolean
    Public Property m_bEsConMarcadores As Boolean
    Public Property m_StrArchivoAnexo As String

    Public Property ArchivoAnexo() As String
    Public Property EsConAnexo() As Boolean
    Public Property MensajeNoHayRegistros() As String
    Public Property EsConMarcadoresWord() As Boolean

    Public Property ExtensionArchivoMarcadores As String

    Public Property TieneError As Boolean
    Public Property MensajeError As String



    Public Sub New(ByVal templateFile As String)
        Dim posicionPunto As Integer

        templateFile = templateFile.Trim()

        If templateFile.Equals("") Then
            TieneError = True
            MensajeError = "No hay archivo template de Word"
            Exit Sub
        End If

        ArchivoTemplateWORD = templateFile

        posicionPunto = ArchivoTemplateWORD.IndexOf(".")

        If posicionPunto > 0 Then
            sFileMarcadores = ArchivoTemplateWORD + ExtensionArchivoMarcadores
        Else
            sFileMarcadores = ArchivoTemplateWORD.Substring(0, posicionPunto) + ExtensionArchivoMarcadores
        End If

        oLineas = New List(Of TMarcadores)
        oLineasTabla = New List(Of TMarcadores)


        sFileDestinoWord = ""

        MensajeNoHayRegistros = ""

        SepIzquierda = Chr(Asc("«"))
        SepDerecha = Chr(Asc("»"))

        nTotalMarcadores = 0
        nTotalDetalleTabla = 0

        CleanDetalle()

        EsConAnexo = False
        ArchivoAnexo = ""
        EsConMarcadoresWord = False
    End Sub

    '---------------------------------------------------------------------------------------
    ' Procedimiento : GeneraCarta
    ' Fecha         : 22/09/2006 16:01
    ' Autor         : Jorge Vidal Bastias
    ' Proposito     : Generar la carta
    ' Logica        : 1. Abre la Plantilla
    '                 2. Reemplaza los TAG de la plantilla encerrados en {}
    '                 3. Grabar el documento con otro nombre
    '                 4. Salir sin Guardar (para conservar la plantilla ;))
    '---------------------------------------------------------------------------------------
    Public Function GeneraWord() As Boolean
        '    Dim DocWord As Word.Application
        '    Dim oTabla As Word.Table

        '    Dim nX As Integer
        '    Dim nFila As Integer
        '    Dim Existe As Long
        '    Dim sNombreMarcador As String
        '    Dim sNombreMarcadorWord As String

        '    Dim sValor As String
        '    Dim m2 As Object

        '    DocWord = CreateObject("word.application")

        '    With DocWord
        '        .Application.Visible = False
        '        .Documents.Open(ArchivoTemplateWORD)

        '        If EsConAnexo Then AddArchivoWordAnexo(DocWord)

        '        For nX = 1 To nTotalMarcadores
        '            sNombreMarcadorWord = Trim(oLineas(nX).sNombreMarcador)
        '            sNombreMarcador = SepIzquierda & sNombreMarcadorWord & SepDerecha
        '            sValor = oLineas(nX).sValor

        '            If EsConMarcadoresWord Then
        '                m2 = .ActiveDocument.Range
        '                m2.ActiveDocument.Goto , , , sNombreMarcadorWord
        '                m2.SetRange.ActiveDocument.Bookmarks(sNombreMarcadorWord).Range.start, .ActiveDocument.Bookmarks(sNombreMarcadorWord).Range.start
        '                m2.InsertBefore sValor

        '        Else

        '                If sNombreMarcador = "«HEADER»" Then
        '                    Call Me.AddHeaderInfo(DocWord, sNombreMarcador, sValor)
        '                ElseIf sNombreMarcador = SepIzquierda & sValor & SepDerecha Then
        '                    Call AddTablaInfo(DocWord, sValor)
        '                Else
        '                    .ActiveDocument.Content.Find.Execute findtext:=sNombreMarcador, replacewith:=sValor, Replace:=2
        '            End If

        '            End If

        '        Next

        '        If Not EsConMarcadoresWord Then
        '            For Each oTabla In .ActiveDocument.Tables
        '                If oTabla.Rows.Count = 2 And MensajeNoHayRegistros <> "" Then
        '                    oTabla.Rows(2).Cells.Merge
        '                    oTabla.Rows(2).Cells.VerticalAlignment = wdCellAlignVerticalCenter
        '                    oTabla.Cell(2, 1).Range.Text = MensajeNoHayRegistros
        '                End If
        '            Next
        '        End If

        '        .ActiveDocument.SaveAs sFileDestinoWord
        '    .Quit wdDoNotSaveChanges

        'End With

        'Set DocWord = Nothing
        'Set oTabla = Nothing
        'Set m2 = Nothing
        Return True
    End Function

    Public Sub AddMarcador(sNombreMarcador As String, Optional sValor As String = "--")
        Dim marcador As TMarcadores = New TMarcadores()

        marcador.sNombreMarcador = sNombreMarcador.Trim().ToUpper()
        marcador.sValor = sValor

        oLineas.Add(marcador)

        nTotalMarcadores = oLineas.Count()

    End Sub

    Private Sub Class_Initialize()
        ArchivoTemplateWORD = ""
        sFileDestinoWord = ""
        sFileMarcadores = ""

        MensajeNoHayRegistros = ""

        SepIzquierda = Chr(Asc("«"))
        SepDerecha = Chr(Asc("»"))

        nTotalMarcadores = 0
        nTotalDetalleTabla = 0

        CleanDetalle()
        EsConAnexo = False
        ArchivoAnexo = ""
        EsConMarcadoresWord = False
    End Sub

    Public Sub CleanDetalle()
        nTotalMarcadores = 0
        nTotalDetalleTabla = 0
        oLineas = New List(Of TMarcadores)
        oLineas = New List(Of TMarcadores)
    End Sub

    Private Sub Class_Terminate()
        CleanDetalle()
    End Sub


    Public Function LeeTemplateTemplate() As StringBuilder
        Dim objReader As System.IO.StreamReader
        Dim texto As StringBuilder = New StringBuilder
        If ArchivoTemplateWORD <> "" Then
            If File.Exists(ArchivoTemplateWORD) Then
                objReader = File.OpenText(ArchivoTemplateWORD)
                texto.Append(objReader.ReadToEnd)
            End If
        End If
        objReader = Nothing
        Return texto

    End Function


    Function initMarcadores() As Boolean
        'Dim s As String
        'Dim nFile As Integer

        'nFile = FreeFile()

        'CleanDetalle()

        'Open sFileMarcadores For Input As #nFile

        'While Not EOF(nFile)
        '    Line Input #nFile, s
        '    AddMarcador(s, SepIzquierda & s & SepDerecha)
        'Wend

        'Close #nFile


        'Return True
    End Function

    Function AddValorMarcador(sNombreMarcador As String, sValor As String, Optional sFormato As String = "") As Boolean
        Dim nX As Integer

        For nX = 1 To nTotalMarcadores
            'Debug.Print oLineas(nX).sNombreMarcador
            If UCase(oLineas(nX).sNombreMarcador) = UCase(sNombreMarcador) Then
                If sFormato = "" Then
                    oLineas(nX).sValor = sValor
                Else
                    oLineas(nX).sValor = Format(sValor, sFormato)
                End If

                Exit For
            End If
        Next

        If nX > nTotalMarcadores Then
            AddValorMarcador = False
        Else
            AddValorMarcador = True
        End If
    End Function
    '---------------------------------------------------------------------------------------------------
    Public Sub AddHeaderInfo(ByRef p_msw As String, p_sNombreMarcador As String, p_svalor As String)
        '        On Error GoTo ControlError

        '        'Move focus to the Header pane...
        '        p_msw.ActiveWindow.ActivePane.View.Type = wdPageView
        '        p_msw.ActiveWindow.ActivePane.View.SeekView = wdSeekCurrentPageHeader

        '        With p_msw.Selection
        '            .Find.Execute findtext:=p_sNombreMarcador, replacewith:=p_svalor, Replace:=1
        '    End With

        '        'Finally, move focus back to the main document...
        '        p_msw.ActiveWindow.ActivePane.View.SeekView = wdSeekMainDocument

        'ControlError:
        '        Dim Sub_MsgError
        '        If Err() Then
        '            Sub_MsgError = MsgBox("Error " + CStr(Err.Description))
        '            If Sub_MsgError = vbRetry Then
        '                Resume
        '            End If
        '        End If

    End Sub

    '------------------------------------------------------------------------------
    Public Sub AddTablaInfo(ByRef oWord As String, ByVal sNombreMarcadorTabla As String)
        'Dim Tabla As Word.Table
        'Dim iFila As Integer
        'Dim iCol As Integer
        'Dim nIndex As Integer
        'Dim nFilas As Integer

        'Dim iTabla As Integer

        'iTabla = buscarTabla(oWord, sNombreMarcadorTabla)

        'If iTabla > 0 Then
        'Set Tabla = oWord.ActiveDocument.Tables(iTabla)

        '' tabla.Rows.Add
        'iFila = 1

        '    For nIndex = 0 To nTotalDetalleTabla
        '        If oLineasTabla(nIndex).sNombreMarcador = sNombreMarcadorTabla Then
        '            nFilas = oLineasTabla(nIndex).sValor2(1).Count

        '            For iFila = 1 To nFilas
        '                Tabla.Rows.Add
        '                For iCol = 2 To oLineasTabla(nIndex).sValor2(1).Item(iFila).Count
        '                    Tabla.Cell(Tabla.Rows.Count - 1, iCol - 1).Range.Text = oLineasTabla(nIndex).sValor2(1).Item(iFila).Item(iCol)
        '                Next
        '            Next
        '        End If
        '    Next
        'End If
    End Sub
    Private Function buscarTabla(ByRef oWord As String, ByVal sNombreTabla As String) As Integer
        'Dim iTabla As Integer
        'Dim oTabla As Word.Table
        'Dim strTabla As String
        'Dim strTablaText As String

        'strTabla = SepIzquierda & sNombreTabla & SepDerecha

        'iTabla = 1
        'For Each oTabla In oWord.ActiveDocument.Tables
        '    oTabla.Select
        '    strTablaText = oTabla.Cell(2, 1).Range.Text

        '    If Left(strTablaText, Len(strTabla)) = strTabla Then Exit For
        '    iTabla = iTabla + 1
        'Next

        'If iTabla > oWord.ActiveDocument.Tables.Count Then iTabla = -1

        'buscarTabla = iTabla
    End Function

    Public Sub AddMarcadorTabla(sNombreMarcadorTabla As String, ByVal sNombreMarcadorColumna As String, ByVal sDato As Collection)
        '    If sDato.Count > 0 Then
        '        nTotalDetalleTabla = nTotalDetalleTabla + 1

        '        ReDim Preserve oLineasTabla(nTotalDetalleTabla)

        '        oLineasTabla(nTotalDetalleTabla).sNombreMarcador = UCase(sNombreMarcadorTabla)
        '        oLineasTabla(nTotalDetalleTabla).sValor2.Add sDato
        '    oLineasTabla(nTotalDetalleTabla).sValor = oLineasTabla(nTotalDetalleTabla).sValor2.Count

        '        AddValorMarcador sNombreMarcadorTabla, sNombreMarcadorTabla
        'End If
    End Sub
    '----------------------------------------------------------------------------

    Public Function AddArchivoAnexo(ByVal strFileName As String) As Boolean
        '    Dim strExisteArchivo As String

        '    strExisteArchivo = Dir(strFileName)

        '    If strExisteArchivo = "" Then
        '        AddArchivoAnexo = False
        '        MsgBox "No se encontró plantilla " & strFileName, vbInformation
        'Else
        '        EsConAnexo = True
        '        ArchivoAnexo = strFileName
        '        AddArchivoAnexo = True
        '    End If
        Return True
    End Function

    Private Sub AddArchivoWordAnexo(ByRef oWord As String)
        '    With oWord
        '        .ActiveDocument.Select
        '        .Selection.Collapse Direction:=wdCollapseEnd
        '    .Selection.InsertBreak Type:=wdPageBreak
        '    .Selection.InsertFile filename:=ArchivoAnexo,
        '                           Range:="",
        '                           ConfirmConversions:=False,
        '                           link:=False,
        '                           attachment:=False
        'End With
    End Sub
End Class
