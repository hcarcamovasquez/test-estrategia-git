Imports DTO
Imports WSCanjeMandatorio

Public Class ConfigPentahoDatos
    Public Function GetPentahoPorId(pentaho As ConfigPentahoDTO) As ConfigPentahoDTO
        Dim Ws = New WSCanjeMandatorio.WSConfigPentaho()
        Return Ws.GetPentahoPorId(pentaho)
    End Function
End Class
