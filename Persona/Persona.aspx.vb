Imports System.Security.Cryptography
Imports Persona.Utils

Public Class Persona
    Inherits System.Web.UI.Page
    Private db As New PersonaDb()
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub BtnGuardar_Click(sender As Object, e As EventArgs)
        Dim persona As New Models.Persona()
        'validar campos obligatorios
        If txtFechaNac.Text = "" Or txtNombre.Text = "" Then
            lblResultado.Text = "Por favor complete los campos obligatorios."
            Return
        End If

        persona.Nombre = txtNombre.Text.Trim()
        persona.Apellidos = txtApellidos.Text.Trim()
        persona.FechaNacimiento = txtFechaNac.Text.Trim()
        persona.Correo = txtCorreo.Text.Trim()
        persona.TipoDocumento = ddlTipoDocumento.SelectedItem.Value
        persona.NumeroDocumento = txtDocumento.Text.Trim()

        lblResultado.Text = persona.Resumen()
        Dim errorMessage As String = ""
        Dim resultado = db.CrearPersona(persona, errorMessage)


        If resultado Then
            SwalUtils.ShowSwal(Me, "Persona creada exitosamente")
            gvPersonas.DataBind()
        Else
            SwalUtils.ShowSwalError(Me, errorMessage)
        End If
    End Sub

    Protected Sub gvPersonas_RowDeleting(sender As Object, e As GridViewDeleteEventArgs)
        e.Cancel = True 'cancelamos el evento de eliminación predeterminado del GridView
        Dim id As Integer = Convert.ToInt32(gvPersonas.DataKeys(e.RowIndex).Value) 'obtenemos el ID de la persona a eliminar  dispar el evento de eliminación personalizado  
        Dim errorMessage As String = ""
        Dim resultado = db.EliminarPersona(id, errorMessage) 'eliminamos la persona de la base de datos

        If resultado Then
            SwalUtils.ShowSwal(Me, "Persona eliminada exitosamente")
            gvPersonas.DataBind()
        Else
            SwalUtils.ShowSwalError(Me, errorMessage)
        End If
    End Sub
End Class