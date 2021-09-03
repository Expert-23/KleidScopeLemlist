Imports System.IO
Imports System.Runtime.Serialization.Formatters.Binary
Imports CBase.Trace.Core.Base
Public Class Serialization_Utilities

    Public Shared Sub Serialize_Object_And_Save_FileSystem(ByVal item As Object, ByVal fileName As String, Optional ByRef success As Boolean = False)

        success = False
        If item Is Nothing Then Exit Sub

        Try

            Dim bytes As Byte()
            Serialize_Object(bytes, item)

            If bytes IsNot Nothing AndAlso bytes.Count > 0 Then
                My.Computer.FileSystem.WriteAllBytes(fileName, bytes, False)
                success = True
            End If

        Catch ex As Exception

            Debug.Print("Failed to serialize and save object of filename {0}.{1}Reason: {2}", fileName, vbCrLf, ex.Message)

        End Try

    End Sub

    Public Shared Sub Load_Object_FileSystem_And_Deserialize(Of T)(ByVal fileName As String, _
                                                                                                                         ByRef item As T, _
                                                                                                                         Optional ByRef success As Boolean = False)

        success = False
        If Not My.Computer.FileSystem.FileExists(fileName) Then Exit Sub

        Try

            Dim bytes As Byte()
            bytes = My.Computer.FileSystem.ReadAllBytes(fileName)

            item = DeSerialize_Object(Of T)(bytes, success)

            If item IsNot Nothing Then
                success = True
            End If

        Catch ex As Exception

            Debug.Print("Failed to load and deserialize object from filename {0}.{1}Reason: {2}", fileName, vbCrLf, ex.Message)

        End Try

    End Sub

    Public Shared Sub Serialize_Object(Of T)(ByRef bytes As Byte(), ByRef item As T)

        Try

            Dim ser As New BinaryFormatter

            Using strm As New MemoryStream()
                ser.Serialize(strm, item)
                bytes = strm.ToArray
                strm.Close()
            End Using

        Catch ex As Exception

            Dim keyVariableNames() As String = {""}
            Dim keyVariableValues() As String = {""}

            Debug.Print(My.Application.Info.AssemblyName, _
                                                                           "Serialization Utilities", _
                                                                           System.Reflection.MethodBase.GetCurrentMethod.Name, _
                                                                           keyVariableNames, _
                                                                           keyVariableValues, _
                                                                           ex.Message)

        End Try

    End Sub


    Public Shared Function DeSerialize_Object(Of T)(ByRef bin As Byte(), Optional ByRef success As Boolean = False) As T

        Dim bf As New BinaryFormatter
        Dim item As T

        success = False

        Try

            Dim ms As New MemoryStream(bin)
            Dim obj As Object = bf.Deserialize(ms)

            item = CType(obj, T)

            success = True

        Catch ex As Exception

            Dim keyVariableNames() As String = {""}
            Dim keyVariableValues() As String = {""}

            Debug.Print(My.Application.Info.AssemblyName, _
                                                                           "Serialization_Utilities", _
                                                                           System.Reflection.MethodBase.GetCurrentMethod.Name, _
                                                                           keyVariableNames, _
                                                                           keyVariableValues, _
                                                                           ex.Message)

        End Try

        Return item

    End Function


    Public Shared Function Copy_Object_Deep(Of T)(ByVal itemOrig As T, ByRef success As Boolean) As T

        Try

            Dim bytes As Byte()
            Serialize_Object(Of T)(bytes, itemOrig)

            Dim itemCopy As T
            itemCopy = DeSerialize_Object(Of T)(bytes, success)

        Catch ex As Exception

            Dim keyVariableNames() As String = {""}
            Dim keyVariableValues() As String = {""}

            Debug.Print(My.Application.Info.AssemblyName, _
                                                                           "Serialization Utilities", _
                                                                           System.Reflection.MethodBase.GetCurrentMethod.Name, _
                                                                           keyVariableNames, _
                                                                           keyVariableValues, _
                                                                           ex.Message)

        End Try

    End Function


    Public Class ThreadSafe

        Public Sub Serialize_Object_And_Save_FileSystem(ByVal item As Object, ByVal fileName As String, Optional ByRef success As Boolean = False)

            success = False
            If item Is Nothing Then Exit Sub

            Try

                Dim bytes As Byte()
                Serialize_Object(bytes, item, success)

                If bytes IsNot Nothing AndAlso bytes.Count > 0 Then
                    My.Computer.FileSystem.WriteAllBytes(fileName, bytes, False)
                    success = True
                End If

            Catch ex As Exception

                Debug.Print("Failed to serialize and save object of filename {0}.{1}Reason: {2}", fileName, vbCrLf, ex.Message)

            End Try

        End Sub

        Public Sub Load_Object_FileSystem_And_Deserialize(Of T)(ByVal fileName As String, _
                                                                                                                             ByRef item As T, _
                                                                                                                             Optional ByRef success As Boolean = False)

            success = False
            If Not My.Computer.FileSystem.FileExists(fileName) Then Exit Sub

            Try

                Dim bytes As Byte()
                bytes = My.Computer.FileSystem.ReadAllBytes(fileName)

                item = DeSerialize_Object(Of T)(bytes, success)

                If item IsNot Nothing Then
                    success = True
                End If

            Catch ex As Exception

                Debug.Print("Failed to load and deserialize object from filename {0}.{1}Reason: {2}", fileName, vbCrLf, ex.Message)

            End Try

        End Sub


        Public Sub Serialize_Object(Of T)(ByRef bytes As Byte(), _
                                                                    ByRef item As T, _
                                                                    ByRef success As Boolean)

            success = False
            Try

                Dim ser As New BinaryFormatter

                Using strm As New MemoryStream()
                    ser.Serialize(strm, item)
                    bytes = strm.ToArray
                    strm.Close()
                End Using

                success = True

            Catch ex As Exception

                Dim keyVariableNames() As String = {""}
                Dim keyVariableValues() As String = {""}

                Debug.Print(My.Application.Info.AssemblyName, _
                                                                               "Serialization_Utilities", _
                                                                               System.Reflection.MethodBase.GetCurrentMethod.Name, _
                                                                               keyVariableNames, _
                                                                               keyVariableValues, _
                                                                               ex.Message)

            End Try

        End Sub

        Public Function DeSerialize_Object(Of T)(ByRef bin As Byte(), Optional ByRef success As Boolean = False) As T

            Dim bf As New BinaryFormatter
            Dim item As T

            success = False

            Try

                Dim ms As New MemoryStream(bin)
                Dim obj As Object = bf.Deserialize(ms)

                item = CType(obj, T)

                success = True

            Catch ex As Exception

                Dim keyVariableNames() As String = {""}
                Dim keyVariableValues() As String = {""}

                Debug.Print(My.Application.Info.AssemblyName, _
                                                                               "Serialization_Utilities", _
                                                                               System.Reflection.MethodBase.GetCurrentMethod.Name, _
                                                                               keyVariableNames, _
                                                                               keyVariableValues, _
                                                                               ex.Message)

            End Try

            Return item

        End Function


    End Class

End Class
