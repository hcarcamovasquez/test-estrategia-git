Imports System.Text
Imports DTO
Imports Negocio


Public Class ObtenerFechasSolicitud

    Public Shared Function ObtenerFechaNav(Nemotecnico As String, fechaSuscripcion As String, fechaIntencion As String) As Date

        Dim serieParam As FondoSerieDTO = New FondoSerieDTO
        Dim negocioSerie As FondoSeriesNegocio = New FondoSeriesNegocio
        Dim NegocioRescate As RescateNegocio = New RescateNegocio()

        serieParam.Nemotecnico = Nemotecnico

        Dim SoloDiasHabiles As Integer

        Dim series As FondoSerieDTO
        series = negocioSerie.GetFondoSeriesNemotecnico(serieParam)

        Dim estructuraFechas = New EstructuraFechasDto
        estructuraFechas = Utiles.splitCharByComma(series.FechaNavSuscripcion)

        Dim FechaSolicitud As Date

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

    Public Shared Function ObtenerFechaTCObservado(Nemotecnico As String, fechaSuscripcion As String, fechaNav As String) As Date
        Dim serie As FondoSerieDTO = New FondoSerieDTO
        Dim NegocioRescate As RescateNegocio = New RescateNegocio()
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
            End Select

            If fechaParaCalculo <> Nothing Then

                'fechaParaCalculo = Utiles.SumaDiasAFechas(ddlMonedaPago.Text, fechaParaCalculo, estructuraFechas.DiasASumar, Constantes.CONST_SOLO_DIAS_HABILES)

                fechaParaCalculo = Utiles.SumaDiasAFechas("CLP", fechaParaCalculo, estructuraFechas.DiasASumar, Constantes.CONST_SOLO_DIAS_HABILES)
                Dim bDiaInhabil As Boolean = (Not Utiles.esFechaHabil("CLP", fechaParaCalculo) And "CLP" = "USD")

                'If bDiaInhabil Then
                '    ShowAlert(CONST_INHABIL_PARA_TC)
                'End If

                fechaParaCalculo = Utiles.getDiaHabilSiguiente(fechaParaCalculo, "CLP")
                'txtFechaTC.Text = fechaParaCalculo
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
            Dim fechaNavC As String
            Dim diasNavC As String
            Dim estructuraFechas = New EstructuraFechasDto
            estructuraFechas = Utiles.splitCharByComma(series.FechaSuscripcion)
            Dim Suscripcion As SuscripcionDTO = New SuscripcionDTO

            FechaNavSuscripcion = series.FechaSuscripcion

            fechaNavC = estructuraFechas.DesdeQueFecha
            diasNavC = estructuraFechas.DiasASumar


            Dim testString As String = FormatDateTime(FechaSolicitud, DateFormat.LongDate)

            If diasNavC = "" Then
                Suscripcion.FechaIntencion = fechaIntencion

                FechaSolicitud = Suscripcion.FechaIntencion
                'txtFechaSuscripcion.Text = FechaSolicitud
            Else
                Dim dias As Integer = Integer.Parse(diasNavC)
                Suscripcion.FechaIntencion = fechaIntencion
                FechaSolicitud = Suscripcion.FechaIntencion

                'FechaPagoFondoRescatableINT es días que hay que sumar o restar, FechaCalculo es a la fecha a la que hay que sumar o restar
                'FECHA DIAS HABILES

                ' FechaSolicitud = Utiles.SumaDiasAFechas(ddlMonedaPago.Text, FechaSolicitud, estructuraFechas.DiasASumar, Constantes.CONST_SOLO_DIAS_CORRIDOS)
                FechaSolicitud = Utiles.SumaDiasAFechas("CLP", FechaSolicitud, estructuraFechas.DiasASumar, Constantes.CONST_SOLO_DIAS_HABILES)

                'fechaSolicitud = NegocioRescate.SelectFechaPagoSIRescatable(dias, fechaSolicitud, 0)


            End If
        Next
        Return FechaSolicitud
    End Function
End Class
