Imports Persona.Utils

Partial Public Class Persona
    Inherits System.Web.UI.Page

    ' Declaración del control Label para mostrar resultados/mensajes
    'Protected WithEvents lblResultado As Global.System.Web.UI.WebControls.Label

    Private db As New PersonaDb()
    ' Private hfIdPersona As Object
    'Private lblResultado As Object


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub btnGuardar_Click(sender As Object, e As EventArgs)
        Dim persona As New Models.Persona()

        'Validar campos obligatorios 
        If txtFechaNac.Text = "" Then
            'lblResultado.Text = "Por favor, complete todos los campos obligatorios."
            Return
        End If

        persona.Nombre = txtNombre.Text.Trim()
        persona.Apellidos = txtApellidos.Text.Trim()
        persona.FechaNacimiento = txtFechaNac.Text.Trim()
        persona.Correo = txtCorreo.Text.Trim()
        persona.TipoDocumento = ddlTipoDocumento.SelectedItem.Value
        persona.NumeroDocumento = txtDocumento.Text.Trim()

        'lblResultado.Text = persona.Resumen()
        Dim errorMessage As String = ""
        Dim resultado = db.CrearPersona(persona, errorMessage)

        If resultado Then
            SwalUtils.ShowSwal(Me, "Persona creada exitosamente.")
            gvPersonas.DataBind()
        Else
            SwalUtils.ShowSwalError(Me, errorMessage)
        End If
    End Sub

    Protected Sub gvPersonas_RowDeleting(sender As Object, e As GridViewDeleteEventArgs)
        e.Cancel = True ' Cancelar la eliminación predeterminada del GridView
        Dim id As Integer = Convert.ToInt32(gvPersonas.DataKeys(e.RowIndex).Value)
        Dim errorMessage As String = ""
        Dim resultado = db.EliminarPersona(id, errorMessage)
        If resultado Then
            SwalUtils.ShowSwal(Me, "Persona eliminada exitosamente.")
            gvPersonas.DataBind() ' Refrescar el GridView después de eliminar
        Else
            SwalUtils.ShowSwalError(Me, errorMessage)
        End If
    End Sub

    Protected Sub gvPersonas_RowEditing(sender As Object, e As GridViewEditEventArgs)

    End Sub

    Protected Sub gvPersonas_SelectedIndexChanged(sender As Object, e As EventArgs)
        hfIdPersona.Value = gvPersonas.DataKeys(gvPersonas.SelectedIndex).Value
        Dim id As Integer = Convert.ToInt32(hfIdPersona.Value)

        Dim errorMessage As String = ""
        Dim persona As Models.Persona = db.ConsultarPersona(id, errorMessage)

        If persona Is Nothing Then
            SwalUtils.ShowSwalError(Me, If(errorMessage = "", "No se pudo cargar la persona.", errorMessage))
            Return
        End If

        txtDocumento.Text = persona.NumeroDocumento
        txtNombre.Text = persona.Nombre
        txtApellidos.Text = persona.Apellidos
        ddlTipoDocumento.SelectedValue = persona.TipoDocumento
        txtFechaNac.Text = persona.FechaNacimiento.ToString("yyyy-MM-dd")
        txtCorreo.Text = persona.Correo

        btnGuardar.Visible = False
        btnActualizar.Visible = True
    End Sub

    Protected Sub btnActualizar_Click(sender As Object, e As EventArgs)
        Dim persona As New Models.Persona()
        persona.IDPersona = Convert.ToInt32(hfIdPersona.Value)

        'Validar campos obligatorios 
        If txtFechaNac.Text = "" Then
            'lblResultado.Text = "Por favor, complete todos los campos obligatorios."
            Return
        End If

        persona.Nombre = txtNombre.Text.Trim()
        persona.Apellidos = txtApellidos.Text.Trim()
        persona.FechaNacimiento = txtFechaNac.Text.Trim()
        persona.Correo = txtCorreo.Text.Trim()
        persona.TipoDocumento = ddlTipoDocumento.SelectedItem.Value
        persona.NumeroDocumento = txtDocumento.Text.Trim()

        'lblResultado.Text = persona.Resumen()
        Dim errorMessage As String = ""
        Dim resultado = db.ActualizarPersona(persona, errorMessage)

        If resultado Then
            SwalUtils.ShowSwal(Me, "Persona actualizada exitosamente.")
            gvPersonas.DataBind()
            btnGuardar.Visible = True
            btnActualizar.Visible = False
            hfIdPersona.Value = ""
            LimpiarCampos()
        Else
            SwalUtils.ShowSwalError(Me, errorMessage)
        End If
    End Sub

    Private Sub LimpiarCampos()
        txtDocumento.Text = ""
        txtNombre.Text = ""
        txtApellidos.Text = ""
        ddlTipoDocumento.SelectedIndex = 0
        txtFechaNac.Text = ""
        txtCorreo.Text = ""
    End Sub
End Class