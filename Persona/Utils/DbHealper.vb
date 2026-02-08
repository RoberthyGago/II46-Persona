Public Class DbHealper
    Private connctionString As String = ConfigurationManager.ConnectionStrings("CAMBIAR-NOMBRE").ConnectionString

    Public Funtion GetConnection() As SqlConnection
        Dim conn As New SqlConnection(connctionString)
        conn.Open()
        Return conn
    End Function

End Class
