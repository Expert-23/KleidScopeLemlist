Public Class File_Utilities

    Public Shared Function Extract_File_Extension(ByVal fileName As String, Optional ByVal defaultExt As String = "") As String

        If String.IsNullOrEmpty(fileName) Then Return defaultExt

        Dim dotLoc As Integer = fileName.LastIndexOf(".")
        Dim ext As String = fileName.Substring(dotLoc + 1)

        Return ext

    End Function


    Public Shared Sub Load_Delimited_File(ByVal fullFileRef As String,
                                                             ByRef dict As SortedDictionary(Of Integer, SortedDictionary(Of Integer, String)),
                                                             Optional ByVal delim As String = ",")

        dict = New SortedDictionary(Of Integer, SortedDictionary(Of Integer, String))

        Dim contents As String = My.Computer.FileSystem.ReadAllText(fullFileRef)
        Parse_Delimited_Contents(contents, dict, delim)

    End Sub

    Public Shared Sub Parse_Delimited_Contents(ByVal contents As String,
                                                                                    ByRef dict As SortedDictionary(Of Integer, SortedDictionary(Of Integer, String)),
                                                                                    Optional ByVal delim As String = ",")

        dict = New SortedDictionary(Of Integer, SortedDictionary(Of Integer, String))

        Dim lines As String() = contents.Split(Environment.NewLine)
        Dim lineCount As Integer = 0
        For Each line As String In lines

            line = line.Replace(ChrW(10), "")

            If line <> String.Empty Then

                Dim words As String() = line.Split(delim)
                Dim wordCount As Integer = 0
                Dim wordDict As New SortedDictionary(Of Integer, String)
                For Each word In words

                    'If word <> String.Empty Then

                    wordDict.Add(wordCount, word)
                    wordCount += 1

                    'End If

                Next

                dict.Add(lineCount, wordDict)
                lineCount += 1

            End If

        Next


    End Sub


    Public Shared Function Standardize_Directory_Reference(ByVal path As String) As String

        If Not (path.EndsWith("\")) Then path &= "\"
        Return path

    End Function

    Public Shared Sub Split_Full_File_Ref_Into_Path_FileName(ByVal fullFileRef As String,
                                                                                                            ByRef filePath As String,
                                                                                                            ByRef fileName As String,
                                                                                                          Optional ByVal pathDelimeter As String = "\")

        Try

            Dim locLastSlash As Integer = fullFileRef.LastIndexOf(pathDelimeter)
            fileName = fullFileRef.Substring(locLastSlash + 1)
            filePath = fullFileRef.Substring(0, locLastSlash)

        Catch ex As Exception

            Dim keyVariableNames() As String = {"Full File Reference", "File Path", "File Name"}
            Dim keyVariableValues() As String = {fullFileRef, filePath, fileName}

            'Debug.Print(My.Application.Info.AssemblyName, _
            '                                                               "File_Utility", _
            '                                                               System.Reflection.MethodBase.GetCurrentMethod.Name, _
            '                                                               keyVariableNames, _
            '                                                               keyVariableValues, _
            '                                                               ex.Message)

        End Try

    End Sub

    Public Shared Sub Split_Full_File_Ref_Into_Path_FileName_Ext(ByVal fullFileRef As String,
                                                                                                                    ByRef filePath As String,
                                                                                                                    ByRef fileName As String,
                                                                                                                    ByRef ext As String,
                                                                                                                    Optional ByVal pathDelimeter As String = "\")

        Try

            Dim locLastSlash As Integer = fullFileRef.LastIndexOf(pathDelimeter)

            If locLastSlash < 0 Then
                filePath = String.Empty
                fileName = fullFileRef
            Else
                filePath = fullFileRef.Substring(0, locLastSlash)
                fileName = fullFileRef.Substring(locLastSlash + 1)
            End If

            ext = Extract_File_Extension(fileName, "")

        Catch ex As Exception

            Dim keyVariableNames() As String = {"Full File Reference", "File Path", "File Name"}
            Dim keyVariableValues() As String = {fullFileRef, filePath, fileName}

            'Debug.Print(My.Application.Info.AssemblyName, _
            '                                                               "File_Utility", _
            '                                                               System.Reflection.MethodBase.GetCurrentMethod.Name, _
            '                                                               keyVariableNames, _
            '                                                               keyVariableValues, _
            '                                                               ex.Message)

        End Try

    End Sub

    Public Shared Function Copy_File_From_One_Directory_To_Another(ByVal fullFileRef As String, ByVal dir_destination As String) As Boolean

        If Not My.Computer.FileSystem.FileExists(fullFileRef) Then
            Debug.Print("Source file {0} not found. File copy aborted", fullFileRef)
            Return False
        End If

        If Not My.Computer.FileSystem.DirectoryExists(dir_destination) Then
            Debug.Print("Destination directory {0} not found. File copy aborted", dir_destination)
            Return False
        End If

        Dim fileName As String
        File_Utilities.Split_Full_File_Ref_Into_Path_FileName(fullFileRef, String.Empty, fileName)

        Dim fullFileRef_copy As String = String.Format("{0}\{1}", dir_destination, fileName)

        Try
            My.Computer.FileSystem.CopyFile(fullFileRef, fullFileRef_copy, True)
        Catch ex As Exception
            Debug.Print("Failed to copy db from {0} to {1}.{2}Reason: {3}", fullFileRef, fullFileRef_copy, vbCrLf, ex.Message)
        End Try

        Return True

    End Function





    Public Shared Function Convert_CSV_To_Tab_Delimited(ByVal line As String) As String

        Dim placeHolder As String = vbCrLf

        While line.Contains(ChrW(34))

            Dim loc_first_dblQte As Integer = line.IndexOf(ChrW(34))
            If loc_first_dblQte = -1 Then Exit While

            Dim loc_sec_dblQte As Integer = line.IndexOf(ChrW(34), loc_first_dblQte + 1)
            If loc_sec_dblQte = -1 Then Exit While

            Dim length As Integer = loc_sec_dblQte - loc_first_dblQte + 1
            Dim head As String = line.Substring(0, loc_first_dblQte)
            Dim value As String = line.Substring(loc_first_dblQte, length)
            Dim tail As String = line.Substring(loc_sec_dblQte + 1)

            value = value.Replace(",", placeHolder)
            value = value.Trim(ChrW(34))

            line = head & value & tail

        End While

        line = line.Replace(",", vbTab)
        line = line.Replace(placeHolder, ",")

        Return line

    End Function
End Class
