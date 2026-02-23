'esta clase se encarga de manejar las operaciones relacionadas con la entidad Persona en la base de datos, como crear, eliminar, actualizar y obtener personas
Imports System.Data.SqlClient
Imports Persona.Utils
Public Class PersonaDb
    Private db As New DbHealper()
    'crear persona
    Public Function CrearPersona(ByVal pPersona As Models.Persona, ByRef errorMessage As String) As Boolean
        'logica para crear persona en la base de datos
        Using db.GetConnection()
            Dim query As String = "INSERT INTO Personas (TipoDocumento, Documento, Nombre, Apellidos, FechaNac, Correo) 
            VALUES (@TipoDocumento, @Documento, @Nombre, @Apellidos, @FechaNac, @Correo)"

            Dim parameters As New Dictionary(Of String, Object) From {
            {"@Nombre", pPersona.Nombre},
            {"@FechaNac", pPersona.FechaNacimiento},
            {"@Correo", pPersona.Correo},
            {"@Apellidos", pPersona.Apellidos},
            {"@Documento", pPersona.NumeroDocumento},
            {"@TipoDocumento", pPersona.TipoDocumento}
          }
            Return db.ExecuteNonQuery(query, parameters, errorMessage)
        End Using
        Return True
    End Function

    'metodo para eliminar una persona
    Public Function EliminarPersona(ByVal id As Integer, ByRef errorMessage As String) As Boolean
        Dim query As String = "DELETE FROM Personas WHERE IDPersona = @Id"
        Dim parameters As New Dictionary(Of String, Object) From {
            {"@Id", id}
        }
        Return db.ExecuteNonQuery(query, parameters, errorMessage)
    End Function

    Friend Function ConsultarPersona(id As String, modelErrorMessage As ModelErrorMessage) As Models.Persona
        Dim persona As New Models.Persona()

        Try
            Dim query As String = "SELECT IDPersona, Nombre, Apellidos, TipoDocumento, Documento, FechaNac, Correo 
                               FROM Personas 
                               WHERE IDPersona = @Id"

            Dim parameters As New Dictionary(Of String, Object) From {
            {"@Id", id}
        }

            Using reader As SqlDataReader = db.ExecuteReader(query, parameters, modelErrorMessage.ErrorMessage)
                If reader.Read() Then
                    persona.IDPersona = Convert.ToInt32(reader("IDPersona"))
                    persona.Nombre = reader("Nombre").ToString()
                    persona.Apellidos = reader("Apellidos").ToString()
                    persona.TipoDocumento = reader("TipoDocumento").ToString()
                    persona.NumeroDocumento = reader("Documento").ToString()
                    persona.FechaNacimiento = Convert.ToDateTime(reader("FechaNac"))
                    persona.Correo = reader("Correo").ToString()
                End If
            End Using

        Catch ex As Exception
            modelErrorMessage.ErrorMessage = ex.Message
        End Try

        Return persona
    End Function


    'metodo para actualizar una persona
    Public Function ActualizarPersona(ByVal pPersona As Models.Persona, ByRef errorMessage As String) As Boolean
        Dim query As String = "UPDATE Personas 
                           SET TipoDocumento = @TipoDocumento,
                               Documento = @Documento,
                               Nombre = @Nombre,
                               Apellidos = @Apellidos,
                               FechaNac = @FechaNac,
                               Correo = @Correo
                           WHERE IDPersona = @Id"

        Dim parameters As New Dictionary(Of String, Object) From {
            {"@TipoDocumento", pPersona.TipoDocumento},
            {"@Documento", pPersona.NumeroDocumento},
            {"@Nombre", pPersona.Nombre},
            {"@Apellidos", pPersona.Apellidos},
            {"@FechaNac", pPersona.FechaNacimiento},
            {"@Correo", pPersona.Correo},
            {"@Id", pPersona.IDPERSONA}
        }

        Return db.ExecuteNonQuery(query, parameters, errorMessage)
    End Function
End Class
