Imports System.Text
Imports DTO
Imports Negocio


Public Class ObtenerFechasSolicitud

    Public Shared Function ObtenerFechaNav(Nemotecnico As String, fechaSuscripcion As String, fechaIntencion As String) As Date

        Dim serieParam As FondoSerieDTO = New FondoSerieDTO
        Dim negocioSerie As FondoSeriesNegocio = New FondoSeriesNegocio
        Dim NegocioRescate As RescateNegocio = New RescateNegocio()
        Dim SoloDiasHabiles As Integer

        Dim series As FondoSerieDTO

        Dim estructuraFechas = New EstructuraFechasDto
        Dim FechaSolicitud As Date

        serieParam.Nemotecnico = Nemotecnico

        series = negocioSerie.GetFondoSeriesNemotecnico(serieParam)

        estructuraFechas = Utiles.splitCharByComma(series.FechaNavSuscripcion)

        Select Case estructuraFechas.DesdeQueFecha
            Case "FechaSuscripcion"
                FechaSolicitud = fechaSuscripcion
            Case Else
                FechaSolicitud = fechaIntencion
        End Select

        If FechaSolicitud <> Nothing Then
            SoloDiasHabiles = IIf(series.SoloDiasHabilesFechaNavSuscripciones, Constantes.CONST_SOLO_DIAS_HABILES, Constantes.CONST_SOLO_DIAS_CORRIDOS)
            FechaSolicitud = Utiles.SumaDiasAFechas("CLP", FechaSolicitud, estructuraFechas.DiasASumar, SoloDiasHabiles)
        End If

        ' txtFechaNAV.Text = FechaSolicitud
        Return FechaSolicitud
    End Function

    Public Shared Function ObtenerFechaTCObservado(Nemotecnico As String, fechaSuscripcion As String, fechaNav As String, FechaIntencion As String) As Date
        Dim serie As FondoSerieDTO = New FondoSerieDTO
        'Dim NegocioRescate As RescateNegocio = New RescateNegocio()
        Dim negocioSerie As FondoSeriesNegocio = New FondoSeriesNegocio

        Dim listaSerie As List(Of FondoSerieDTO)
        Dim fechaParaCalculo As Date

        serie.Nemotecnico = Nemotecnico
        listaSerie = negocioSerie.GrupoSeriesPorNemotecnico(serie)

        For Each series As FondoSerieDTO In listaSerie
            Dim estructuraFechas As EstructuraFechasDto = Utiles.splitCharByComma(series.FechaTCSuscripcion)


            Select Case estructuraFechas.DesdeQueFecha
                Case "FechaSuscripcion"
                    fechaParaCalculo = fechaSuscripcion
                Case "FechaNav"
                    fechaParaCalculo = fechaNav
                Case Else
                    ' TODO: JOVB Pregunta -> Si No hay configuracion de la serie ¿ que valor le debe poner al TC ? 
                    fechaParaCalculo = FechaIntencion
            End Select

            If fechaParaCalculo <> Nothing Then
                fechaParaCalculo = Utiles.SumaDiasAFechas("CLP", fechaParaCalculo, estructuraFechas.DiasASumar, Constantes.CONST_SOLO_DIAS_HABILES)
                Dim bDiaInhabil As Boolean = (Not Utiles.esFechaHabil("CLP", fechaParaCalculo) And "CLP" = "USD")

                fechaParaCalculo = Utiles.getDiaHabilSiguiente(fechaParaCalculo, "CLP")
            Else
                'txtFechaTC.Text = txtFechaIntencion.Text
            End If
        Next

        Return fechaParaCalculo

    End Function

    Public Shared Function ObtenerFechaSuscripcion(Nemotecnico As String, fechaIntencion As String, fechaNav As String) As Date
        Dim serie As FondoSerieDTO = New FondoSerieDTO
        Dim negocioSerie As FondoSeriesNegocio = New FondoSeriesNegocio

        Dim listaSerie As List(Of FondoSerieDTO)
        Dim FechaNavSuscripcion As String

        Dim FechaSolicitud As Date

        serie.Nemotecnico = Nemotecnico
        listaSerie = negocioSerie.GrupoSeriesPorNemotecnico(serie)

        For Each series As FondoSerieDTO In listaSerie
            'Dim fechaNavC As String
            'Dim diasNavC As String
            Dim estructuraFechas = New EstructuraFechasDto
            Dim Suscripcion As SuscripcionDTO = New SuscripcionDTO

            estructuraFechas = Utiles.splitCharByComma(series.FechaSuscripcion)
            FechaNavSuscripcion = series.FechaSuscripcion

            'fechaNavC = estructuraFechas.DesdeQueFecha
            'diasNavC = estructuraFechas.DiasASumar

            'Dim testString As String = FormatDateTime(FechaSolicitud, DateFormat.LongDate)

            Select Case estructuraFechas.DesdeQueFecha
                Case "FechaIntencion"
                    FechaSolicitud = fechaIntencion
                Case Else
                    FechaSolicitud = fechaIntencion
            End Select
            'If diasNavC = "" Then
            '    Suscripcion.FechaIntencion = fechaIntencion
            '    FechaSolicitud = Suscripcion.FechaIntencion
            '    'txtFechaSuscripcion.Text = FechaSolicitud
            'Else

            Suscripcion.FechaIntencion = fechaIntencion
            FechaSolicitud = Suscripcion.FechaIntencion
            FechaSolicitud = Utiles.SumaDiasAFechas("CLP", FechaSolicitud, estructuraFechas.DiasASumar, Constantes.CONST_SOLO_DIAS_HABILES)
            'End If
        Next
        Return FechaSolicitud
    End Function
End Class
