Imports Schema.Internal_DB
Imports Primitives
Imports System.Data.SqlClient
Imports System.Collections.Generic
Imports Utilities

Public Class Persist_SQL

    Public Class Content

        'Create
        Public Function Create_One(ByRef content_item As Content_Item) As Boolean

            Dim success As Boolean = False
            Try


                Using conn As New SqlClient.SqlConnection(OPS.Conn)

                    conn.Open()

                    Dim query As String = String.Format("Insert INTO {0}({1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17},{18},{19},{20},{21},{22},{23},{24},{25},{26},{27}) VALUES  ('{28}','{29}','{30}','{31}','{32}','{33}','{34}','{35}','{36}','{37}','{38}','{39}','{40}','{41}','{42}','{43}','{44}','{45}','{46}','{47}','{48}','{49}','{50}','{51}','{52}','{53}',@binary);SELECT @@IDENTITY",
                                                        OPS.Content_Item.Table_Name,
                                                        OPS.Content_Item.ColName.ID_Unique,
                                                        OPS.Content_Item.ColName.ID_Post,
                                                        OPS.Content_Item.ColName.ID_Media,
                                                        OPS.Content_Item.ColName.ID_Source,
                                                        OPS.Content_Item.ColName.Name_Source,
                                                        OPS.Content_Item.ColName.Author,
                                                        OPS.Content_Item.ColName.Category,
                                                        OPS.Content_Item.ColName.Website,
                                                        OPS.Content_Item.ColName.Title,
                                                        OPS.Content_Item.ColName.Excerpt,
                                                        OPS.Content_Item.ColName.Summary,
                                                        OPS.Content_Item.ColName.Content,
                                                        OPS.Content_Item.ColName.Date_Published,
                                                        OPS.Content_Item.ColName.Date_Harvested,
                                                        OPS.Content_Item.ColName.URL_Content,
                                                        OPS.Content_Item.ColName.URL_Media,
                                                        OPS.Content_Item.ColName.URL_Post,
                                                        OPS.Content_Item.ColName.Media_Name,
                                                        OPS.Content_Item.ColName.Media_FileName,
                                                        OPS.Content_Item.ColName.Media_Filepath,
                                                        OPS.Content_Item.ColName.Media_Format,
                                                        OPS.Content_Item.ColName.Media_isImage,
                                                        OPS.Content_Item.ColName.Media_isVideo,
                                                        OPS.Content_Item.ColName.Image_Height,
                                                        OPS.Content_Item.ColName.Image_Width,
                                                        OPS.Content_Item.ColName.Status,
                                                        OPS.Content_Item.ColName.Bin,
                                                        content_item.ID_Unique,
                                                        content_item.ID_Post,
                                                        content_item.ID_Media,
                                                        content_item.ID_Source.Replace("'", "`"),
                                                        content_item.Name_Source.Replace("'", "`"),
                                                        content_item.Author.Replace("'", "`"),
                                                        content_item.Category.Replace("'", "`"),
                                                        content_item.Website.ToString,
                                                        content_item.Title.Replace("'", "`"),
                                                        content_item.Excerpt.Replace("'", "`"),
                                                        content_item.Summary.Replace("'", "`"),
                                                        content_item.Content.Replace("'", "`"),
                                                        content_item.Date_Published,
                                                        content_item.Date_Harvested,
                                                        content_item.URL_Content.Replace("'", "`"),
                                                        content_item.URL_Media.Replace("'", "`"),
                                                        content_item.URL_Post.Replace("'", "`"),
                                                        content_item.Media_Name.Replace("'", "`"),
                                                        content_item.Media_FileName.Replace("'", "`"),
                                                        content_item.Media_Filepath.Replace("'", "`"),
                                                        content_item.Media_Format.Replace("'", "`"),
                                                        content_item.Media_isImage,
                                                        content_item.Media_isVideo,
                                                        content_item.Image_Height,
                                                        content_item.Image_Width,
                                                        content_item.Status.ToString
                                                        )


                    Dim index As Integer
                    Dim cmd As New SqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@binary", content_item.Bin)



                    'cmd.ExecuteNonQuery()

                    index = cmd.ExecuteScalar()
                    content_item.ID = index

                    conn.Close()
                    success = True



                End Using

            Catch ex As Exception
                Console.WriteLine("Failed to Persist Content reason : ", ex.Message)
            End Try

            Return success

        End Function

        Public Function Create_Multiple(ByRef items As Dictionary(Of String, Content_Item), Optional ByRef failed As List(Of String) = Nothing) As Boolean

            If items Is Nothing Then Return False

            Dim success As Boolean = False
            failed = New List(Of String)

            Dim updated_collection As New Dictionary(Of String, Content_Item)

            For Each entry In items

                Dim key As String = entry.Key
                Dim item As Content_Item = entry.Value

                If Create_One(item) Then
                    updated_collection.Add(key, item)
                Else
                    failed.Add(key)
                End If


            Next

            success = True
            items = updated_collection

            Return success

        End Function

        'Retreive

        Public Function Retreive_One(ByVal id_content As String) As DataSet


            Dim ds As New DataSet()

            Try

                Using conn As New SqlClient.SqlConnection(OPS.Conn)

                    conn.Open()

                    Dim query As String = String.Format("Select * from {0} WHERE {1} = '{2}'",
                                                        OPS.Content_Item.Table_Name,
                                                        OPS.Content_Item.ColName.ID_Unique,
                                                        id_content)



                    Dim da = New SqlDataAdapter(query, conn)
                    da.Fill(ds)

                    conn.Close()

                End Using


            Catch ex As Exception

                Console.WriteLine(ex.Message)
            End Try


            Return ds


        End Function


        Public Function Retreive_by_Status(ByVal status As String) As DataSet

            Dim ds As New DataSet()

            Try

                Using conn As New SqlClient.SqlConnection(OPS.Conn)

                    conn.Open()

                    Dim query As String = String.Format("Select * from {0} WHERE {1} = '{2}'",
                                                        OPS.Content_Item.Table_Name,
                                                        OPS.Content_Item.ColName.Status,
                                                        status)



                    Dim da = New SqlDataAdapter(query, conn)
                    da.Fill(ds)

                    conn.Close()

                End Using


            Catch ex As Exception

                Console.WriteLine(ex.Message)
            End Try


            Return ds


        End Function

        'Update
        Public Function Update_Status(ByVal id_content As String, ByVal status As String) As Boolean

            Dim success As Boolean = False
            Try

                Using conn As New SqlClient.SqlConnection(OPS.Conn)

                    conn.Open()

                    Dim query As String = String.Format("Update {0} set {1} = '{3}' where {2} = '{4}'",
                                                        OPS.Content_Item.Table_Name,
                                                        OPS.Content_Item.ColName.Status,
                                                        OPS.Content_Item.ColName.ID_Unique,
                                                        status,
                                                        id_content)



                    Dim cmd As New SqlCommand(query, conn)

                    cmd.ExecuteNonQuery()

                    conn.Close()
                    success = True

                End Using


            Catch ex As Exception

                Console.WriteLine(ex.Message)
            End Try


            Return success


        End Function

        'Delete
        Public Function Delete_One(ByVal id_unique As String) As Boolean

            Dim success As Boolean = False
            Try


                Using conn As New SqlClient.SqlConnection(OPS.Conn)

                    conn.Open()

                    Dim query As String = String.Format("delete from {0} where {1} = '{2}'", OPS.Content_Item.Table_Name,
                                                                                            OPS.Content_Item.ColName.ID_Unique,
                                                                                            id_unique)



                    Dim cmd As New SqlCommand(query, conn)
                    cmd.ExecuteNonQuery()

                    conn.Close()
                    success = True



                End Using

            Catch ex As Exception
                Console.WriteLine("Failed to Persist Content reason : ", ex.Message)
            End Try

            Return success

        End Function

        Public Function Delete_All() As Boolean

            Dim success As Boolean = False
            Try


                Using conn As New SqlClient.SqlConnection(OPS.Conn)

                    conn.Open()

                    Dim query As String = String.Format("delete  from {0}",
                                                            OPS.Content_Item.Table_Name)



                    Dim cmd As New SqlCommand(query, conn)
                    cmd.ExecuteNonQuery()

                    conn.Close()
                    success = True

                End Using

            Catch ex As Exception
                Console.WriteLine("Failed to Persist Content reason : ", ex.Message)
            End Try

            Return success

        End Function

        Public Function Update_One_Content(content_item As Content_Item) As Boolean

            Dim success As Boolean = False
            Try


                Using conn As New SqlClient.SqlConnection(OPS.Conn)

                    conn.Open()

                    Dim query As String = String.Format("UPDATE {0}  SET {2} ='{29}', {3} = '{30}'  ,{4} = '{31}'  ,{5} =  '{32}' ,{6} =  '{33}' ,{7} =  '{34}'  ,{8} =  '{35}' ,{9} = '{36}'  ,{10} = '{37}' ,{11} = '{38}' ,{12} = '{39}'  ,{13} = '{40}' ,{14} = '{41}' ,{15} = '{42}' ,{16} = '{43}'  ,{17} = '{44}',{18} = '{45}',{19} ='{46}'  ,{20} = '{47}',{21}='{48}' ,{22} ='{49}' ,{23} = '{50}' ,{24} = '{51}' ,{25} = '{52}'  ,{26} = '{53}' ,{27} = @binary   WHERE {1}= '{28}' ", OPS.Content_Item.Table_Name,
                                                        OPS.Content_Item.ColName.ID_Unique,
                                                        OPS.Content_Item.ColName.ID_Post,
                                                        OPS.Content_Item.ColName.ID_Media,
                                                        OPS.Content_Item.ColName.ID_Source,
                                                        OPS.Content_Item.ColName.Name_Source,
                                                        OPS.Content_Item.ColName.Author,
                                                        OPS.Content_Item.ColName.Category,
                                                        OPS.Content_Item.ColName.Website,
                                                        OPS.Content_Item.ColName.Title,
                                                        OPS.Content_Item.ColName.Excerpt,
                                                        OPS.Content_Item.ColName.Summary,
                                                        OPS.Content_Item.ColName.Content,
                                                        OPS.Content_Item.ColName.Date_Published,
                                                        OPS.Content_Item.ColName.Date_Harvested,
                                                        OPS.Content_Item.ColName.URL_Content,
                                                        OPS.Content_Item.ColName.URL_Media,
                                                        OPS.Content_Item.ColName.URL_Post,
                                                        OPS.Content_Item.ColName.Media_Name,
                                                        OPS.Content_Item.ColName.Media_FileName,
                                                        OPS.Content_Item.ColName.Media_Filepath,
                                                        OPS.Content_Item.ColName.Media_Format,
                                                        OPS.Content_Item.ColName.Media_isImage,
                                                        OPS.Content_Item.ColName.Media_isVideo,
                                                        OPS.Content_Item.ColName.Image_Height,
                                                        OPS.Content_Item.ColName.Image_Width,
                                                        OPS.Content_Item.ColName.Status,
                                                        OPS.Content_Item.ColName.Bin,
                                                        content_item.ID_Unique,
                                                        content_item.ID_Post,
                                                        content_item.ID_Media,
                                                        content_item.ID_Source.Replace("'", "`"),
                                                        content_item.Name_Source.Replace("'", "`"),
                                                        content_item.Author.Replace("'", "`"),
                                                        content_item.Category.Replace("'", "`"),
                                                        content_item.Website,
                                                        content_item.Title.Replace("'", "`"),
                                                        content_item.Excerpt.Replace("'", "`"),
                                                        content_item.Summary.Replace("'", "`"),
                                                        content_item.Content.Replace("'", "`"),
                                                        content_item.Date_Published,
                                                        content_item.Date_Harvested,
                                                        content_item.URL_Content.Replace("'", "`"),
                                                        content_item.URL_Media.Replace("'", "`"),
                                                        content_item.URL_Post.Replace("'", "`"),
                                                        content_item.Media_Name.Replace("'", "`"),
                                                        content_item.Media_FileName.Replace("'", "`"),
                                                        content_item.Media_Filepath.Replace("'", "`"),
                                                        content_item.Media_Format.Replace("'", "`"),
                                                        content_item.Media_isImage,
                                                        content_item.Media_isVideo,
                                                        content_item.Image_Height,
                                                        content_item.Image_Width,
                                                        content_item.Status
                                                        )


                    Dim cmd As New SqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@binary", content_item.Bin)

                    cmd.ExecuteNonQuery()
                    conn.Close()

                    success = True



                End Using

            Catch ex As Exception
                Console.WriteLine("Failed to Persist Content reason : ", ex.Message)
            End Try

            Return success


        End Function



    End Class

    Public Class Solicitation_Job

        Public Function Create_One_Job_Solicitation(ByVal job_solicitation As Job_Solicitation) As Boolean

            Dim job_binary As Byte() = {}
            Serialization_Utilities.Serialize_Object(Of Job_Solicitation)(job_binary, job_solicitation)
            Dim date_created As DateTime = DateTime.Now
            Dim success As Boolean = False
            Try


                Using conn As New SqlClient.SqlConnection(OPS.Conn)

                    conn.Open()

                    Dim query As String = String.Format("Insert INTO {0}({1},{2},{3},{4},{5},{6}) values('{7}','{8}','{9}','{10}','{11}',@binary)",
                                                        OPS.Job_Solicitation.Table_Name,
                                                        OPS.Job_Solicitation.ColName.Content_Item_ID,
                                                        OPS.Job_Solicitation.ColName.Job_Status,
                                                        OPS.Job_Solicitation.ColName.Website,
                                                        OPS.Job_Solicitation.ColName.Category,
                                                        OPS.Job_Solicitation.ColName.Date_Created,
                                                        OPS.Job_Solicitation.ColName.Job_Solicitation,
                                                        job_solicitation.Item_Content.ID,
                                                        job_solicitation.Item_Content.Status,
                                                        job_solicitation.Item_Content.Website.ToString(),
                                                        job_solicitation.Item_Content.Category,
                                                        date_created)

                    Dim cmd As New SqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@binary", job_binary)



                    cmd.ExecuteNonQuery()


                    conn.Close()
                    success = True



                End Using

            Catch ex As Exception
                Console.WriteLine("Failed to Persist Job Solicitation reason : ", ex.Message)
            End Try

            Return success

        End Function


        Public Function Create_Mulitple_Job_Solicitation(ByRef solicitations As Dictionary(Of String, Tuple(Of Job_Solicitation, Boolean))) As Boolean

            If solicitations Is Nothing Then Return False

            Dim success As Boolean = False
            Dim updated_Collection As New Dictionary(Of String, Tuple(Of Job_Solicitation, Boolean))

            For Each entry In solicitations.Values

                Dim job_solicit As Job_Solicitation = entry.Item1
                Dim ispushed As Boolean = entry.Item2

                If Not ispushed Then

                    Dim id As String = job_solicit.Item_Content.ID_Unique
                    If Not Create_One_Job_Solicitation(job_solicit) Then


                        updated_Collection.Add(id, New Tuple(Of Job_Solicitation, Boolean)(job_solicit, False))

                        Console.WriteLine("Failed To Create Job with ID : {0}", job_solicit.Item_Content.ID)

                    Else
                        updated_Collection.Add(id, New Tuple(Of Job_Solicitation, Boolean)(job_solicit, True))

                    End If

                End If


            Next


            success = True
            solicitations = updated_Collection

            Return success
        End Function

        Public Function Update_One_Solicitation(job_solicitation As Job_Solicitation) As Boolean
            Dim solicitation_binary As Byte() = {}
            Serialization_Utilities.Serialize_Object(Of Job_Solicitation)(solicitation_binary, job_solicitation)
            Dim success As Boolean = False
            Try


                Using conn As New SqlClient.SqlConnection(OPS.Conn)

                    conn.Open()

                    Dim query As String = String.Format("UPDATE {0}  SET {1}  = @binary WHERE {2}= '{3}' ",
                                                        OPS.Job_Solicitation.Table_Name,
                                                        OPS.Job_Solicitation.ColName.Job_Solicitation,
                                                        OPS.Job_Solicitation.ColName.Content_Item_ID,
                                                        job_solicitation.Item_Content.ID)


                    Dim cmd As New SqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@binary", solicitation_binary)

                    cmd.ExecuteNonQuery()
                    conn.Close()

                    success = True



                End Using

            Catch ex As Exception
                Console.WriteLine("Failed to Update Solicitation reason : ", ex.Message)
            End Try

            Return success


        End Function

        Public Function Update_Solicitation_Status(job_solicitation As Job_Solicitation, status As Content_Status) As Boolean
            Dim solicitation_binary As Byte() = {}
            Serialization_Utilities.Serialize_Object(Of Job_Solicitation)(solicitation_binary, job_solicitation)
            Dim success As Boolean = False
            Try


                Using conn As New SqlClient.SqlConnection(OPS.Conn)

                    conn.Open()

                    Dim query As String = String.Format("UPDATE {0}  SET {1}  = @binary WHERE {2}= '{3}' ",
                                                        OPS.Job_Solicitation.Table_Name,
                                                        OPS.Job_Solicitation.ColName.Job_Solicitation,
                                                        OPS.Job_Solicitation.ColName.Content_Item_ID,
                                                        job_solicitation.Item_Content.ID)


                    Dim cmd As New SqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@binary", solicitation_binary)

                    cmd.ExecuteNonQuery()
                    conn.Close()

                    success = True



                End Using

            Catch ex As Exception
                Console.WriteLine("Failed to Update Solicitation reason : ", ex.Message)
            End Try

            Return success


        End Function

    End Class

End Class
