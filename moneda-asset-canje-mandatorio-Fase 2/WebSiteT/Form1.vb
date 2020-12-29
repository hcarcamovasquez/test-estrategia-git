Imports ActiveDirectoryWrapper.PC

Public Class Form1
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim AD As ADWrapper = New ADWrapper

#Disable Warning BC42025 ' Acceso de miembro compartido, miembro de constante, miembro de enumeración o tipo anidado a través de una instancia; la expresión de calificación no se evaluará.
        MessageBox.Show("Usuario correcto Jvidal" + IIf(AD.IsUserValid("jvidal", "jvidal.123"), "SI", "NO"))
#Enable Warning BC42025 ' Acceso de miembro compartido, miembro de constante, miembro de enumeración o tipo anidado a través de una instancia; la expresión de calificación no se evaluará.
#Disable Warning BC42025 ' Acceso de miembro compartido, miembro de constante, miembro de enumeración o tipo anidado a través de una instancia; la expresión de calificación no se evaluará.
        MessageBox.Show("Usuario correcto Administrador" + IIf(AD.IsUserValid("Administrator", "mote.123"), "SI", "NO"))
#Enable Warning BC42025 ' Acceso de miembro compartido, miembro de constante, miembro de enumeración o tipo anidado a través de una instancia; la expresión de calificación no se evaluará.



    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        MessageBox.Show(IIf(ADWrapper.Login("administrator", "mote.123") = ADWrapper.LoginResult.LOGIN_OK, "administrator is OK", "administrator NOK"))
        MessageBox.Show("jvidal " + ADWrapper.Login("jvidal", "mote.123").ToString())
        ADWrapper.GetAllUsers("")
    End Sub
End Class
