Imports System.Data.SqlClient

Public Class DbHealper
    Private connectionString As String = ConfigurationManager.ConnectionStrings("II-46ConnectionString").ConnectionString


    Public Function GetConnection() As SqlConnection
        Dim conn As New SqlConnection(connectionString)
        Try
            conn.Open()
        Catch ex As Exception
            conn.Dispose() 'limpia la conexion
            Throw New Exception("Error al abrir la conexión: " & ex.Message)
        End Try
        Return conn
    End Function

    ' Método para ejecutar un comando SQL (INSERT, UPDATE, DELETE)
    Public Function ExecuteNonQuery(query As String, parameters As Dictionary(Of String, Object), ByRef errorMessage As String) As Boolean

        If String.IsNullOrWhiteSpace(query) Then
            Throw New ArgumentException("La consulta no puede estar vacía")
        End If
        Using conn As SqlConnection = GetConnection()
            Using cmd As New SqlCommand(query, conn)
                If parameters IsNot Nothing Then
                    For Each p In parameters
                        cmd.Parameters.AddWithValue(p.Key, p.Value)
                    Next
                End If

                Try
                    cmd.ExecuteNonQuery()

                    Return True
                Catch ex As Exception
                    errorMessage = "Error al ejecutar la consulta: " & ex.Message
                    Return False
                End Try
            End Using
        End Using
    End Function

    Public Function ExecuteQuery(query As String,
        parameters As Dictionary(Of String, Object),
        errorMessage As String) As DataTable

        'Validar que la consulta no esté vacía
        If String.IsNullOrWhiteSpace(query) Then
            Throw New ArgumentException("La consulta no puede estar vacía")
        End If
        Dim dt As New DataTable()

        Using conn As SqlConnection = GetConnection()
            Using cmd As New SqlCommand(query, conn)

                If parameters IsNot Nothing Then
                    For Each p In parameters
                        cmd.Parameters.AddWithValue(p.Key, p.Value)
                    Next
                End If

                Try
                    Using adapter As New SqlDataAdapter(cmd)
                        adapter.Fill(dt)
                    End Using
                    Return dt
                Catch ex As Exception
                    errorMessage = "Error al ejecutar la consulta: " & ex.Message
                    Return Nothing
                End Try

            End Using
        End Using
        Return Nothing
    End Function
End Class