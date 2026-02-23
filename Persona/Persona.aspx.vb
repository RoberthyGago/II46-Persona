'esta clase se encarga de manejar la logica de la pagina Persona.aspx, como crear una nueva persona y eliminar una persona existente, ademas de mostrar mensajes de exito o error utilizando la clase SwalUtils para mostrar ventanas emergentes con sweet alert
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
        persona.FechaNacimiento = Convert.ToDateTime(txtFechaNac.Text.Trim())
        persona.Correo = txtCorreo.Text.Trim()
        persona.TipoDocumento = ddlTipoDocumento.SelectedItem.Value
        persona.NumeroDocumento = txtDocumento.Text.Trim()

        Dim errorMessage As String = ""
        Dim resultado As Boolean

        If btnGuardar.Text = "Actualizar" AndAlso Not String.IsNullOrEmpty(hfIdPersona.Value) Then
            persona.IDPersona = Convert.ToInt32(hfIdPersona.Value)
            resultado = db.ActualizarPersona(persona, errorMessage)
            If resultado Then
                SwalUtils.ShowSwal(Me, "Persona actualizada correctamente")
                btnGuardar.Text = "Guardar" ' volver al modo crear
                hfIdPersona.Value = ""       ' limpiar el ID
            Else
                SwalUtils.ShowSwalError(Me, errorMessage)
            End If
        Else
            resultado = db.CrearPersona(persona, errorMessage)
            If resultado Then
                SwalUtils.ShowSwal(Me, "Persona creada exitosamente")
            Else
                SwalUtils.ShowSwalError(Me, errorMessage)
            End If
        End If

        gvPersonas.DataBind()
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

    'estos evento se crean cuando los hago en persona.aspx
    Protected Sub gvPersonas_RowEditing(sender As Object, e As GridViewEditEventArgs)

    End Sub


    Protected Sub gvPersonas_SelectedIndexChanged(sender As Object, e As EventArgs)
        Dim selectedRow As GridViewRow = gvPersonas.SelectedRow
        Dim id As Integer = Convert.ToInt32(selectedRow.Cells(1).Text)
        Dim errorMessage As New ModelErrorMessage() ' <-- Cambia el tipo de errorMessage a ModelErrorMessage
        Dim persona As Models.Persona = db.ConsultarPersona(id, errorMessage)
        txtDocumento.Text = selectedRow.Cells(3).Text
        txtNombre.Text = HttpUtility.HtmlDecode(selectedRow.Cells(4).Text)
        txtApellidos.Text = selectedRow.Cells(5).Text
        ddlTipoDocumento.SelectedValue = selectedRow.Cells(2).Text
        txtCorreo.Text = selectedRow.Cells(7).Text
        Dim fechaTexto As String = selectedRow.Cells(6).Text
        If Not String.IsNullOrEmpty(fechaTexto) Then
            Dim fecha As DateTime
            If DateTime.TryParse(fechaTexto, fecha) Then
                txtFechaNac.Text = fecha.ToString("yyyy-MM-dd") ' formato compatible con input type="date"
            End If
        End If
    End Sub
End Class