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
        Dim query As String = "DELETE FROM Personas WHERE IDPersona1 = @Id"
        Dim parameters As New Dictionary(Of String, Object) From {
            {"@Id", id}
        }
        Return db.ExecuteNonQuery(query, parameters, errorMessage)
    End Function
End Class
