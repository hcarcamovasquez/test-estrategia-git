Public Class ProcesoDTO
    Public Property IdProceso As Integer
    Public Property FechaProceso As Date
    Public Property Hito As Integer
    Public Property FnRut As String
    Public Property FnRazonSocial As String
    Public Property DirectoIndirecto As String
    Public Property FsGrupo As String
    Public Property GpaId As Integer
    Public Property GpaDescripcion As String
    Public Property ApMultifondo As String
    Public Property ApRut As String
    Public Property ApRazonSocial As String
    Public Property FsNemotecnico As String
    Public Property FsMoneda As String
    Public Property VcsValor As Integer
    Public Property TcValor As Decimal
    Public Property AdvcCantidad As Integer
    Public Property ResCuotas As Decimal
    Public Property SaldoCuotas As Integer 
    Public Property Monto As Integer 
    Public Property CApFinalD As String
    Public Property CApFinalI As String
    Public Property DescEstado As String
    Public Property CApNacExt As String
    Public Property CApCalificado As String
    Public Property CApRelMam As String
    Public Property CApLimite As String
    Public Property CuotasC As Integer
    Public Property CuotasCertificar As Integer
    Public Property Estado As String
    Public Property FechaIngreso As Date
    Public Property UsuarioIngreso As String
    Public Property FechaModificacion AS Date
    Public Property UsuarioModificacion As String

    Public Sub New(idProceso As Integer, fechaProceso As Date, hito As Integer, fnRut As String, fnRazonSocial As String, directoIndirecto As String, fsGrupo As String, gpaId As Integer, gpaDescripcion As String, apMultifondo As String, apRut As String, apRazonSocial As String, fsNemotecnico As String, fsMoneda As String, vcsValor As Integer, tcValor As Integer, advcCantidad As Integer, resCuotas As Decimal, saldoCuotas As Integer, monto As Integer, cApFinalD As String, cApFinalI As String, descEstado As String, cApNacExt As String, cApCalificado As String, cApRelMam As String, cApLimite As String, cuotasC As Integer, cuotasCertificar As Integer, estado As String, fechaIngreso As Date, usuarioIngreso As String, fechaModificacion As Date, usuarioModificacion As String)
        Me.IdProceso = idProceso
        Me.FechaProceso = fechaProceso
        Me.Hito = hito
        Me.FnRut = fnRut
        Me.FnRazonSocial = fnRazonSocial
        Me.DirectoIndirecto = directoIndirecto
        Me.FsGrupo = fsGrupo
        Me.GpaId = gpaId
        Me.GpaDescripcion = gpaDescripcion
        Me.ApMultifondo = apMultifondo
        Me.ApRut = apRut
        Me.ApRazonSocial = apRazonSocial
        Me.FsNemotecnico = fsNemotecnico
        Me.FsMoneda = fsMoneda
        Me.VcsValor = vcsValor
        Me.TcValor = tcValor
        Me.AdvcCantidad = advcCantidad
        Me.ResCuotas = resCuotas
        Me.SaldoCuotas = saldoCuotas
        Me.Monto = monto
        Me.CApFinalD = cApFinalD
        Me.CApFinalI = cApFinalI
        Me.DescEstado = descEstado
        Me.CApNacExt = cApNacExt
        Me.CApCalificado = cApCalificado
        Me.CApRelMam = cApRelMam
        Me.CApLimite = cApLimite
        Me.CuotasC = cuotasC
        Me.CuotasCertificar = cuotasCertificar
        Me.Estado = estado
        Me.FechaIngreso = fechaIngreso
        Me.UsuarioIngreso = usuarioIngreso
        Me.FechaModificacion = fechaModificacion
        Me.UsuarioModificacion = usuarioModificacion
    End Sub

    Public Sub New()
    End Sub 
End Class