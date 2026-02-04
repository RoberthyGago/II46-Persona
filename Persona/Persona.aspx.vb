Public Class Persona
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub BtnGuardar_Click(sender As Object, e As EventArgs)
        Dim persona As New Models.Persona()
        'validar campos obligatorios
        If txtFechaNac.Text = "" Or txtNombre.Text = "" Then
            lblResultado.Text = "Por favor complete los campos obligatorios."
            Return
        End If

        persona.Nombre = TxtNombre.Text.Trim()
        persona.Apellidos = TxtApellidos.Text.Trim()
        persona.FechaNacimiento = txtFechaNac.Text.Trim()
        persona.Correo = txtCorreo.Text.Trim()
        persona.TipoDocumento = DdlTipoDocumento.SelectedItem.Text.Trim()
        persona.NumeroDocumento = txtDocumento.Text.Trim()

        lblResultado.Text = persona.Resumen()
    End Sub
End Class