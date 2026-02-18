'esta clase se encarga de manejar la logica de la pagina Persona.aspx, como crear una nueva persona y eliminar una persona existente, ademas de mostrar mensajes de exito o error utilizando la clase SwalUtils para mostrar ventanas emergentes con sweet alert
Namespace Models
    Public Class Persona
        Private _nombre As String
        Private _apellidos As String
        Private _tipoDocumento As String
        Private _numeroDocumento As String
        Private _fechaNacimiento As Date
        Private _correo As String

        Public Sub New()
            Me.Nombre = "SIN NOMBRE"
        End Sub

        Public Sub New(nombre As String, apellidos As String)
            Me.Nombre = nombre
            Me.Apellidos = apellidos
        End Sub

        Public Sub New(nombre As String, apellidos As String, tipoDocumento As String, numeroDocumento As String, fechaNacimiento As Date, correo As String)
            Me.Nombre = nombre
            Me.Apellidos = apellidos
            Me.TipoDocumento = tipoDocumento
            Me.NumeroDocumento = numeroDocumento
            Me.FechaNacimiento = fechaNacimiento
            Me.Correo = correo
        End Sub

        Public Property Nombre As String
            Get
                Return _nombre
            End Get
            Set(value As String)
                _nombre = value
            End Set
        End Property

        Public Property Apellidos As String
            Get
                Return _apellidos
            End Get
            Set(value As String)
                _apellidos = value
            End Set
        End Property

        Public Property TipoDocumento As String
            Get
                Return _tipoDocumento
            End Get
            Set(value As String)
                _tipoDocumento = value
            End Set
        End Property

        Public Property NumeroDocumento As String
            Get
                Return _numeroDocumento
            End Get
            Set(value As String)
                _numeroDocumento = value
            End Set
        End Property

        Public Property FechaNacimiento As Date
            Get
                Return _fechaNacimiento
            End Get
            Set(value As Date)
                _fechaNacimiento = value
            End Set
        End Property

        Public Property Correo As String
            Get
                Return _correo
            End Get
            Set(value As String)
                _correo = value
            End Set
        End Property

        Public Function Resumen() As String
            Return $"Nombre: {Nombre} {Apellidos} - {Correo}"
        End Function
    End Class

End Namespace

