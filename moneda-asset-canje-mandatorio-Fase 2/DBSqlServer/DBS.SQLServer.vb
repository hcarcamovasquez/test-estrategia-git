Imports System.Collections
Imports System.Collections.Generic
Imports System.Data
Imports System.Diagnostics
Imports System.Data.SqlClient
Imports System.Configuration
Imports System.ComponentModel

Namespace SqlServer

#Region "Clase CONEXION : Clase para la abstraccion de una conexion al motor de datos"
    ''' <summary>
    ''' Clase para la abstraccion de una conexion al motor de datos.
    ''' </summary>
    ''' <remarks></remarks>
    ''' 
    Public Class Conexion
        Private mSqlConn As SqlConnection
        Public Property ConSSPI() As Boolean
            Get
                Return m_ConSSPI
            End Get
            Set(value As Boolean)
                m_ConSSPI = value
            End Set
        End Property
        Private m_ConSSPI As Boolean
        Public Property Usuario() As String
            Get
                Return m_Usuario
            End Get
            Set(value As String)
                m_Usuario = value
            End Set
        End Property
        Private m_Usuario As String
        Public Property Password() As String
            Get
                Return m_Password
            End Get
            Set(value As String)
                m_Password = value
            End Set
        End Property
        Private m_Password As String
        Public Property Servidor() As String
            Get
                Return m_Servidor
            End Get
            Set(value As String)
                m_Servidor = value
            End Set
        End Property
        Private m_Servidor As String

        Public Property BaseDatos() As String
            Get
                Return m_BaseDatos
            End Get
            Set(value As String)
                m_BaseDatos = value
            End Set
        End Property
        Private m_BaseDatos As String
        Public Property SQLConn() As SqlConnection
            Get
                Return m_SQLConn
            End Get
            Set(value As SqlConnection)
                m_SQLConn = value
            End Set
        End Property
        Private m_SQLConn As SqlConnection

        Public Sub New()
            Try
                SQLConn = New SqlConnection(Me.StrConexion())
            Catch ex As Exception
                Throw ex
            End Try
        End Sub

        'Crea el string de conexion a la base de datos.
        Private Function StrConexion() As String
            Try
                Dim strConn As String = Nothing
                strConn = GetConnectionString()


                Return strConn
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Private Function GetConnectionString() As String
            Dim sConexion As String = ""

            Me.Servidor = GetKeyValue("DBSServer", Me.Servidor)
            Me.BaseDatos = GetKeyValue("DBSDataBase", Me.BaseDatos)
            Me.Usuario = GetKeyValue("DBSUser", Me.Usuario)
            Me.Password = GetKeyValue("DBSPassword", Me.Password)


            sConexion = "Data Source=" & Me.Servidor
            sConexion += ";Initial Catalog=" & Me.BaseDatos
            sConexion += ";User ID=" & Me.Usuario
            sConexion += ";Password=" & Me.Password

            Return sConexion

        End Function

        Private Function GetKeyValue(skey As String, sValue As String) As String
            Dim configurationAppSettings As New System.Configuration.AppSettingsReader()
            Dim sValue1 As String = DirectCast(configurationAppSettings.GetValue(skey, GetType(System.String)), String)

            Return sValue1
        End Function

        '
        '         private void GetKeyValue(string sKey, ref string sValue) {
        '			System.Configuration.AppSettingsReader configurationAppSettings = new System.Configuration.AppSettingsReader();
        '			sValue = configurationAppSettings.GetValue(sKey, typeof(System.String));
        '		 }
        '          
        '        


        'Consulta simple a la base de datos, utilizando un query directo.
        Public Function ConsultaBD(pQuery As String) As DataSet
            Try
                Return CreateDataSet(pQuery)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Private Function CreateDataSet(strSQL As String) As DataSet
            Try
                'Se crea la conexion a la base de datos.
                Dim sqlConn As New SqlConnection(Me.StrConexion())

                'SqlCommand es utilizado para ejecutar los comandos SQL
                Dim sqlCmd As New SqlCommand(strSQL, sqlConn)

                'Se le define el tiempo de espera en segundos para la consulta,
                'el valor default es 30 segundos.
                sqlCmd.CommandTimeout = 3600

                'SqlAdapter utiliza el SqlCommand para llenar el Dataset
                Dim sda As New SqlDataAdapter(sqlCmd)

                'Se llena el dataset
                Dim ds As New DataSet()
                sda.Fill(ds)

                'TieneDatos = (ds.Tables(0).Rows.Count > 0)


                Return ds
            Catch ex As Exception

                Throw ex
            End Try

        End Function
    End Class

#End Region

#Region "Clase StoredProcedure : Clase Abstraccion de un procedimiento almacenado"
    ''' <summary>
    ''' Clase StoredProcedure Abstraccion de un procedimiento almacenado.
    ''' </summary>
    ''' <remarks></remarks>
    ''' 
    Public Class StoredProcedure
        Public Property Conn() As Conexion
            Get
                Return m_Conn
            End Get
            Set(value As Conexion)
                m_Conn = value
            End Set
        End Property
        Private m_Conn As Conexion
        Public Property Dataset() As DataSet
            Get
                Return m_Dataset
            End Get
            Set(value As DataSet)
                m_Dataset = value
            End Set
        End Property
        Private m_Dataset As DataSet

        Public Property Nombre() As String
            Get
                Return m_Nombre
            End Get
            Set(value As String)
                m_Nombre = value
            End Set
        End Property
        Private m_Nombre As String
        Public Property TieneDatos() As Boolean
            Get
                Return m_TieneDatos
            End Get
            Set(value As Boolean)
                m_TieneDatos = value
            End Set
        End Property
        Private m_TieneDatos As Boolean
        Public Property FilasAfectas() As Integer
            Get
                Return m_FilasAfectas
            End Get
            Set(value As Integer)
                m_FilasAfectas = value
            End Set
        End Property
        Private m_FilasAfectas As Integer
        Public Property bTieneDatos() As Boolean
            Get
                Return m_bTieneDatos
            End Get
            Set(value As Boolean)
                m_bTieneDatos = value
            End Set
        End Property
        Private m_bTieneDatos As Boolean

        Public Property Parametros() As Dictionary(Of String, StoredProcedureParameter)
            Get
                Return m_Parametros
            End Get
            Set(value As Dictionary(Of String, StoredProcedureParameter))
                m_Parametros = value
            End Set
        End Property
        Private m_Parametros As Dictionary(Of String, StoredProcedureParameter)
        Public Property ConTransaccion() As Boolean
            Get
                Return m_ConTransaccion
            End Get
            Set(value As Boolean)
                m_ConTransaccion = value
            End Set
        End Property
        Private m_ConTransaccion As Boolean

        'Solo recibe el nombre del procedimiento e inicializa la colección.
        Public Sub New(nNombre As String)
            Try
                Nombre = nNombre
                Parametros = New Dictionary(Of String, StoredProcedureParameter)()

                TieneDatos = False

                Dataset = Nothing
            Catch ex As Exception
                Throw ex
            End Try
        End Sub

        '
        Private Function CreaParametro(pVariable As String, pValor As Object, pDirection As ParameterDirection) As StoredProcedureParameter
            Dim iParametro As New StoredProcedureParameter("@" & pVariable, pValor, pDirection)
            Return iParametro
        End Function

        <Description("Agrega los parametros del procedimiento y su respectivo valor.")> _
        Public Sub AgregarParametro(pVariable As String, pValor As Object)
            Try
                Dim iParametro As StoredProcedureParameter = Nothing
                iParametro = CreaParametro(pVariable, pValor, ParameterDirection.Input)
                iParametro.Tipo = SqlDbType.VarChar

                Parametros.Add(pVariable, iParametro)
            Catch ex As Exception
                Throw ex
            End Try
        End Sub

        <Description("Agrega los parametros del procedimiento y su respectivo valor.")> _
        Public Sub AgregarParametro(pVariable As String, pValor As Object, pTipo As SqlDbType)
            Try
                Dim iParametro As StoredProcedureParameter = Nothing
                iParametro = CreaParametro(pVariable, pValor, ParameterDirection.Input)
                iParametro.Tipo = pTipo


                Parametros.Add(pVariable, iParametro)
            Catch ex As Exception
                Throw ex
            End Try
        End Sub

        <Description("Agrega los parametros del procedimiento y su respectivo valor.")> _
        Public Sub AgregarParametro(pVariable As String, pValor As Object, pTipo As SqlDbType, pSize As Integer)
            Try
                Dim iParametro As StoredProcedureParameter = Nothing
                iParametro = CreaParametro(pVariable, pValor, ParameterDirection.Input)
                iParametro.Tipo = pTipo
                iParametro.Size = pSize


                Parametros.Add(pVariable, iParametro)
            Catch ex As Exception
                Throw ex
            End Try
        End Sub

        <Description("Agrega los parametros del procedimiento y su respectivo valor.")> _
        Public Sub AgregarParametro(pVariable As String, pValor As Object, pTipo As SqlDbType, pSize As Integer, pDirection As ParameterDirection)
            Try
                Dim iParametro As StoredProcedureParameter = Nothing
                iParametro = CreaParametro(pVariable, pValor, pDirection)
                iParametro.Tipo = pTipo
                iParametro.Size = pSize


                Parametros.Add(pVariable, iParametro)
            Catch ex As Exception
                Throw ex
            End Try
        End Sub


        'Agrega los parametros del procedimiento y su respectivo valor.
        Public Function GetParamValue(pVariable As String) As StoredProcedureParameter
            Dim iparametro As StoredProcedureParameter = Nothing

            Try
                If Parametros.ContainsKey(pVariable) Then
                    Return Parametros(pVariable)
                Else
                    Return iparametro
                End If
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        'Ejecuta el procedimiento almacenado.
        Public Function ReturnDataSet(Optional pNombreTabla As String = "") As DataSet
            Try
                Dim conn As Conexion = Nothing
                Dim sqlCmd As SqlCommand = Nothing
                Dim mParametro As StoredProcedureParameter = Nothing
                Dim pParam As SqlParameter = Nothing

                conn = New Conexion()
                sqlCmd = New SqlCommand(Me.Nombre, conn.SQLConn)

                sqlCmd.CommandType = CommandType.StoredProcedure

                '--------------------------------------------------
                'Agrega las variables al procedimiento almacenado
                '--------------------------------------------------
                For Each pair As KeyValuePair(Of String, StoredProcedureParameter) In Me.Parametros
                    mParametro = pair.Value
                    pParam = New SqlParameter(mParametro.Variable, mParametro.GetTypeProperty)

                    pParam.Direction = mParametro.Direccion
                    pParam.Value = mParametro.Valor

                    pParam.Size = mParametro.Size
                    'pParam.SqlDbType = SqlDbType.VarChar;

                    sqlCmd.Parameters.Add(pParam)
                Next
                '----------------------------------------------------------------
                'SqlAdapter utiliza el SqlCommand para llenar el Dataset
                '----------------------------------------------------------------
                Dim sda As SqlDataAdapter = Nothing
                'Se llena el dataset
                Dim ds As DataSet = Nothing

                sda = New SqlDataAdapter(sqlCmd)
                ds = New DataSet()

                If String.IsNullOrEmpty(pNombreTabla) Then
                    sda.Fill(ds)
                Else
                    sda.Fill(ds, pNombreTabla)
                End If

                ActualizaValoresParametros(sqlCmd)

                Dataset = ds

                If ds.Tables.Count > 0 Then
                    Me.TieneDatos = (ds.Tables(0).Rows.Count > 0)
                    Me.FilasAfectas = ds.Tables(0).Rows.Count

                Else
                    Me.TieneDatos = False
                    Me.FilasAfectas = 0
                End If

                'conn.SQLConn.Close()



                Return ds
            Catch ex As Exception
                Throw ex
            End Try

        End Function

        Private Sub ActualizaValoresParametros(sqlcmd As SqlCommand)
            Dim mParametro As StoredProcedureParameter = Nothing

            For Each pair As KeyValuePair(Of String, StoredProcedureParameter) In Me.Parametros
                mParametro = pair.Value
                If mParametro.Direccion = ParameterDirection.Output Or mParametro.Direccion = ParameterDirection.InputOutput Then
                    mParametro.Valor = sqlcmd.Parameters(mParametro.Variable).Value
                End If
            Next
        End Sub

        Public Sub Execute()
            Try
                Dim sqlCmd As SqlCommand = Nothing
                Dim mParametro As StoredProcedureParameter = Nothing
                Dim pParam As SqlParameter = Nothing

                Conn = New Conexion()

                sqlCmd = New SqlCommand(Me.Nombre, Conn.SQLConn)

                sqlCmd.CommandType = CommandType.StoredProcedure

                '--------------------------------------------------
                'Agrega las variables al procedimiento almacenado
                '--------------------------------------------------
                For Each pair As KeyValuePair(Of String, StoredProcedureParameter) In Me.Parametros
                    mParametro = pair.Value

                    pParam = New SqlParameter(mParametro.Variable, mParametro.GetTypeProperty)

                    pParam.Direction = mParametro.Direccion
                    pParam.Value = mParametro.Valor
                    pParam.Size = mParametro.Size

                    sqlCmd.Parameters.Add(pParam)
                Next

                '----------------------------------------------------------------
                'SqlAdapter utiliza el SqlCommand para llenar el Dataset
                '----------------------------------------------------------------
                Dim sda As SqlDataAdapter = Nothing
                'Se llena el dataset
                Dim ds As DataSet = Nothing

                sda = New SqlDataAdapter(sqlCmd)
                ds = New DataSet()

                'Conn.SQLConn.Open()

                sda.Fill(ds)


                'Conn.SQLConn.Close()

                ActualizaValoresParametros(sqlCmd)
            Catch ex As Exception

                Throw ex
            End Try
        End Sub

    End Class

#End Region

#Region "Clase StoredProcedureParameter : Abstraccion de un parametro del Procedimiento Almacenado"

    ''' <summary>
    ''' Abstraccion de un parametro del Procedimiento Almacenado
    ''' </summary>
    ''' <remarks></remarks>
    ''' 
    Public Class StoredProcedureParameter
        'Nombre de la variable, debe ser igual a la declarada en el procedimiento almacenado
        Public Property Variable() As String
            Get
                Return m_Variable
            End Get
            Set(value As String)
                m_Variable = value
            End Set
        End Property
        Private m_Variable As String
        'Valor de la variable, puede ser de cualquier tipo de dato. preferible que 
        'coincida con las variables declaradas en GetTypeProperty
        Public Property Valor() As Object
            Get
                Return m_Valor
            End Get
            Set(value As Object)
                m_Valor = value
            End Set
        End Property
        Private m_Valor As Object
        Public Property Direccion() As ParameterDirection
            Get
                Return m_Direccion
            End Get
            Set(value As ParameterDirection)
                m_Direccion = value
            End Set
        End Property
        Private m_Direccion As ParameterDirection

        Public Property Tipo() As SqlDbType
            Get
                Return m_Tipo
            End Get
            Set(value As SqlDbType)
                m_Tipo = value
            End Set
        End Property
        Private m_Tipo As SqlDbType

        Public Property Size() As Integer
            Get
                Return m_Size
            End Get
            Set(value As Integer)
                m_Size = value
            End Set
        End Property
        Private m_Size As Integer

        'Se definen los posibles tipos de datos que se le van a enviar al procedimiento almacenado
        'Esta lista podria aumentar conforme se usen otro tipo de variable.
        Public ReadOnly Property GetTypeProperty() As SqlDbType
            Get
                If Me.Tipo = Nothing Then
                    Select Case Me.Valor.[GetType]().FullName
                        Case "System.String"
                            Return SqlDbType.VarChar
                        Case "System.Int16"
                            Return SqlDbType.Int
                        Case "System.Int32"
                            Return SqlDbType.Int
                        Case "System.Int64"
                            Return SqlDbType.Int
                        Case "System.Decimal"
                            Return SqlDbType.[Decimal]
                        Case "System.Double"
                            Return SqlDbType.BigInt
                        Case "System.DateTime"
                            Return SqlDbType.DateTime
                        Case "System.Byte"
                            Return SqlDbType.Image
                        Case "System.Boolean"
                            Return SqlDbType.Bit
                        Case Else
                            Return SqlDbType.VarChar
                    End Select
                Else
                    Return Me.Tipo

                End If
            End Get
        End Property

        'Procedimiento de creacion de la variable.
        Public Sub New(pVariable As String, pValor As Object, Optional pDirection As ParameterDirection = ParameterDirection.Input)
            Try
                Me.Variable = pVariable

                If pValor Is Nothing Then
                    ' DBNull.Value;
                    Me.Valor = pValor
                Else
                    Me.Valor = pValor
                End If

                Me.Direccion = pDirection
            Catch ex As Exception
                Throw New Exception("Error en la creacion del Parametro" & System.Environment.NewLine & ex.Message)

            End Try
        End Sub
    End Class
#End Region
End Namespace

