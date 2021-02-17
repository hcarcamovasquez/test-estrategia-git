Imports DTO
Imports Datos

Public Class ConfigPentahoNegocio
    Public Function GetPentahoPorId(pentaho As ConfigPentahoDTO) As ConfigPentahoDTO
        Dim datos As ConfigPentahoDatos = New ConfigPentahoDatos
        Return datos.GetPentahoPorId(pentaho)

    End Function
End Class
