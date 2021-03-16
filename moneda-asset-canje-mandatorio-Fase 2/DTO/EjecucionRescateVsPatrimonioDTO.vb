Public Class EjecucionRescateVsPatrimonioDTO
    Public Property Id As Integer
    Public Property FnRut As String
    Public Property FechaEjecucion As DateTime
    Public Property Descripcion As String
    Public Property Estado As String
    Public Property NombreFondo As String

    Public Property FechaDesde As DateTime
    Public Property Fechahasta As DateTime



    Public Sub New(iD As Integer, fnRut As String, fechaEjecucion As Date, dEscripcion As String, eSTADO As String)
        Me.ID = iD
        Me.FnRut = fnRut
        Me.FechaEjecucion = fechaEjecucion
        Me.DEscripcion = dEscripcion
        Me.ESTADO = eSTADO
    End Sub

    Public Sub New()

    End Sub
End Class
