Imports System.Data.SqlClient
Imports CBase.Trace.Core.Base
Public Class SQL_Utilities


    Public Shared Function Find_Matching_DataRows(Of T) _
                                                                                         (ByVal connString As String, _
                                                                                          ByVal tablename As String, _
                                                                                          ByVal targetValue As T, _
                                                                                          ByVal keyColName As String) _
                                                                                          As List(Of DataRow)
        Dim rows As New List(Of DataRow)

        Try

            Dim found As Boolean
            Dim dt As DataTable
            dt = Load_Data_Table(connString, _
                                                           tablename, _
                                                            found)

            If Not found Then
                Return rows
                Exit Function
            End If

            rows = Find_Matching_DataRows(Of T)(dt, targetValue, keyColName)

            If dt IsNot Nothing Then

                For Each dr As DataRow In dt.Rows

                    Dim checkValue As T
                    Safe_Get_SQL_Data_Item(Of T)(dr, keyColName, checkValue)

                    If checkValue.Equals(targetValue) Then rows.Add(dr)

                Next

            End If

        Catch ex As SqlException

            Dim keyVariableNames() As String = {"tablename", "keycolName"}
            Dim keyVariableValues() As String = {tablename, keyColName}


            Debug.Print(My.Application.Info.AssemblyName, _
                                                    "SQL_Utilities", _
                                                    System.Reflection.MethodBase.GetCurrentMethod.Name, _
                                                    keyVariableNames, _
                                                    keyVariableValues, _
                                                    ex.Message)

        End Try

        Return rows

    End Function

    Public Shared Function Find_Matching_DataRows(Of T) _
                                                                                         (ByRef dt As DataTable, _
                                                                                          ByVal targetValue As T, _
                                                                                          ByVal keyColName As String) _
                                                                                          As List(Of DataRow)
        Dim rows As New List(Of DataRow)

        Try

            If dt IsNot Nothing Then

                For Each dr As DataRow In dt.Rows

                    Dim checkValue As T
                    Safe_Get_SQL_Data_Item(Of T)(dr, keyColName, checkValue)

                    If checkValue.Equals(targetValue) Then rows.Add(dr)

                Next

            End If

        Catch ex As SqlException

            Dim keyVariableNames() As String = {"keycolName"}
            Dim keyVariableValues() As String = {keyColName}


            Debug.Print(My.Application.Info.AssemblyName, _
                                                    "SQL_Utilities", _
                                                    System.Reflection.MethodBase.GetCurrentMethod.Name, _
                                                    keyVariableNames, _
                                                    keyVariableValues, _
                                                    ex.Message)

        End Try

        Return rows

    End Function

    Public Shared Sub Clear_Table(ByRef cn As SqlConnection, ByVal tableName As String)

        Try

            Dim squery As String
            squery = "DELETE FROM " & tableName

            Dim cmd As New SqlCommand(squery, cn)
            cmd.CommandTimeout = 0
            cmd.ExecuteNonQuery()

        Catch ex As SqlException

            Debug.Print("SQL table: {0} could not be reset - Reason: {1}", tableName, ex.Message)

            Dim keyVariableNames() As String = {"tablename"}
            Dim keyVariableValues() As String = {tableName}


            Debug.Print(My.Application.Info.AssemblyName, _
                                                    "SQL_Utilities", _
                                                    System.Reflection.MethodBase.GetCurrentMethod.Name, _
                                                    keyVariableNames, _
                                                    keyVariableValues, _
                                                    ex.Message)

        End Try

    End Sub


    Public Shared Function Get_Connection_String(ByVal machineName As String, _
                                                                                 ByVal dbName As String) _
                                                                                 As String

        Dim csBuild As New SqlConnectionStringBuilder

        Try

            csBuild.DataSource = machineName
            csBuild.InitialCatalog = dbName
            csBuild.IntegratedSecurity = True
            csBuild.Pooling = True

        Catch ex As Exception

            Dim keyVariableNames() As String = {"machineName", "dbName"}
            Dim keyVariableValues() As String = {machineName, dbName}

            Debug.Print(My.Application.Info.AssemblyName, _
                                                    "SQL_Utilities", _
                                                    System.Reflection.MethodBase.GetCurrentMethod.Name, _
                                                    keyVariableNames, _
                                                    keyVariableValues, _
                                                    ex.Message)

        End Try

        Return csBuild.ToString

    End Function

    Public Shared Function Convert_String_With_Embedded_Single_Qoute(ByVal rawString As String) As String

        Dim safeString As String

        Try

            Dim unsafeQoute As String = "'"
            Dim safeQoute As String = "' + CHAR(39) + '"

            safeString = rawString.Replace(unsafeQoute, safeQoute)

        Catch ex As Exception

            Dim keyVariableNames() As String = {"String to Convert"}
            Dim keyVariableValues() As String = {rawString}

            Debug.Print(My.Application.Info.AssemblyName, _
                                                                           "SQL_Utilities", _
                                                                           System.Reflection.MethodBase.GetCurrentMethod.Name, _
                                                                           keyVariableNames, _
                                                                           keyVariableValues, _
                                                                           ex.Message)

        End Try

        Return safeString

    End Function

    Public Shared Sub Safe_Get_SQL_Data_Item(Of T)(ByVal dr As DataRow, _
                                            ByVal sql_Item_Name As String, _
                                            ByRef varToSet As T)

        Try

            If Not Convert.IsDBNull(dr.Item(sql_Item_Name)) Then

                varToSet = dr.Item(sql_Item_Name)

            End If

        Catch ex As Exception

            Dim keyVariableNames() As String = {""}
            Dim keyVariableValues() As String = {""}

            Debug.Print(My.Application.Info.AssemblyName, _
                                                                           "SQL_Utilities", _
                                                                           System.Reflection.MethodBase.GetCurrentMethod.Name, _
                                                                           keyVariableNames, _
                                                                           keyVariableValues, _
                                                                           ex.Message)

        End Try

    End Sub

    Public Shared Sub Safe_Get_SQL_Data_Item_Secure_String(ByVal dr As DataRow, _
                                                                                                                        ByVal sql_Item_Name As String, _
                                                                                                                        ByRef varToSet As System.Security.SecureString)

        Try

            If Not Convert.IsDBNull(dr.Item(sql_Item_Name)) Then

                varToSet = New System.Security.SecureString
                For Each chract In dr.Item(sql_Item_Name).ToString.ToCharArray

                    varToSet.AppendChar(chract)

                Next

            End If

        Catch ex As Exception

            Dim keyVariableNames() As String = {""}
            Dim keyVariableValues() As String = {""}

            Debug.Print(My.Application.Info.AssemblyName, _
                                                                           "SQL_Utilities", _
                                                                           System.Reflection.MethodBase.GetCurrentMethod.Name, _
                                                                           keyVariableNames, _
                                                                           keyVariableValues, _
                                                                           ex.Message)

        End Try

    End Sub


    Public Shared Sub Safe_Get_SQL_Data_Item(Of T)(ByVal dr As SqlDataReader, _
                                        ByVal sql_Item_Name As String, _
                                        ByRef varToSet As T)

        Try

            If Not Convert.IsDBNull(dr(sql_Item_Name)) Then

                varToSet = dr(sql_Item_Name)

            End If

        Catch ex As Exception

            Dim keyVariableNames() As String = {"sql_Item_Name"}
            Dim keyVariableValues() As String = {sql_Item_Name}

            Debug.Print(ex.Message)

            'Dim tracer As New Spdmn.Trace.Base

            'tracer.Debug.Print(My.Application.Info.AssemblyName, _
            '                                        Me.GetType().Name, _
            '                                        System.Reflection.MethodBase.GetCurrentMethod.Name, _
            '                                        keyVariableNames, _
            '                                        keyVariableValues, _
            '                                        ex.Message)

        End Try

    End Sub

    Public Shared Sub Safe_Get_SQL_Data_Item_Secure_String(ByVal dr As SqlDataReader, _
                                      ByVal sql_Item_Name As String, _
                                      ByRef varToSet As System.Security.SecureString)

        Try

            If Not Convert.IsDBNull(dr(sql_Item_Name)) Then

                varToSet = New System.Security.SecureString
                For Each chract In dr(sql_Item_Name).ToString.ToCharArray

                    varToSet.AppendChar(chract)

                Next

            End If

        Catch ex As Exception

            Dim keyVariableNames() As String = {"sql_Item_Name"}
            Dim keyVariableValues() As String = {sql_Item_Name}

            Debug.Print(ex.Message)

            'Dim tracer As New Spdmn.Trace.Base

            'tracer.Debug.Print(My.Application.Info.AssemblyName, _
            '                                        Me.GetType().Name, _
            '                                        System.Reflection.MethodBase.GetCurrentMethod.Name, _
            '                                        keyVariableNames, _
            '                                        keyVariableValues, _
            '                                        ex.Message)

        End Try

    End Sub

    Public Shared Sub Add_Input_Parameter_To_SQL_Query_Command(Of T)(ByRef cmd As SqlCommand, _
                                                                                                                                     ByVal paramName As String, _
                                                                                                                                     ByRef value As T, _
                                                                                                                                     ByVal sqlType As SqlDbType, _
                                                                                                                                     Optional ByVal size As Integer = -1)

        Try

            Dim param As New SqlParameter(paramName, sqlType)
            param.Value = value
            param.Direction = ParameterDirection.Input

            If size > -1 Then param.Size = size

            cmd.Parameters.Add(param)

        Catch ex As Exception

            Dim keyVariableNames() As String = {"Parameter Name"}
            Dim keyVariableValues() As String = {paramName}

            Debug.Print(My.Application.Info.AssemblyName, _
                                                                           "SQL_Utilities", _
                                                                           System.Reflection.MethodBase.GetCurrentMethod.Name, _
                                                                           keyVariableNames, _
                                                                           keyVariableValues, _
                                                                           ex.Message)

        End Try

    End Sub

    Public Shared Function Clone_SQL_DataTable_Structure(ByRef dt As DataTable) As DataTable

        Dim cloneTable As New DataTable

        Try

            For Each origColumn As DataColumn In dt.Columns

                Dim cloneColumn As New DataColumn
                With cloneColumn

                    .ColumnName = origColumn.ColumnName
                    .DataType = origColumn.DataType
                    .MaxLength = origColumn.MaxLength

                End With

                cloneTable.Columns.Add(cloneColumn)

            Next

        Catch ex As Exception

            Dim keyVariableNames() As String = {""}
            Dim keyVariableValues() As String = {""}

            Debug.Print(My.Application.Info.AssemblyName, _
                                                                           "SQL_Utilities", _
                                                                           System.Reflection.MethodBase.GetCurrentMethod.Name, _
                                                                           keyVariableNames, _
                                                                           keyVariableValues, _
                                                                           ex.Message)

        End Try

        Return cloneTable

    End Function

    Public Shared Function Clone_SQL_DataRow(ByRef dr As DataRow, ByRef newTable As DataTable) As DataRow

        Dim cloneRow As DataRow

        Try

            cloneRow = newTable.NewRow

            Dim colNumber As Integer
            For colNumber = 0 To newTable.Columns.Count - 1

                cloneRow.Item(colNumber) = dr.Item(colNumber)

            Next

        Catch ex As Exception

            Dim keyVariableNames() As String = {""}
            Dim keyVariableValues() As String = {""}

            Debug.Print(My.Application.Info.AssemblyName, _
                                                                           "SQL_Utilities", _
                                                                           System.Reflection.MethodBase.GetCurrentMethod.Name, _
                                                                           keyVariableNames, _
                                                                           keyVariableValues, _
                                                                           ex.Message)

        End Try

        Return cloneRow

    End Function

    Public Shared Function Load_Data_Table(ByVal connString As String, _
                                                                                ByVal tableName As String, _
                                                                                ByRef found As Boolean, _
                                                                                Optional ByVal query As String = "", _
                                                                                Optional ByVal filter As String = "", _
                                                                                Optional ByRef cn As SqlConnection = Nothing) _
                                                                                As DataTable

        Dim ds As New DataSet
        Dim dt As New DataTable

        found = False
        If tableName = String.Empty Then

            Return dt
            Exit Function

        End If

        Dim conn_supplied As Boolean
        conn_supplied = True

        If cn Is Nothing Then

            cn = New SqlConnection(connString)
            conn_supplied = False

        End If

        Try

            If Not conn_supplied Then cn.Open()

            Dim squery As String
            squery = String.Format("Select * From {0} {1}", tableName, filter)

            If query <> String.Empty Then squery = query

            Dim cmd As New SqlCommand(squery, cn)
            Dim da As New SqlDataAdapter(cmd)

            da.Fill(ds)
            found = True

        Catch ex As Exception

            Dim keyVariableNames() As String = {"Table Name"}
            Dim keyVariableValues() As String = {tableName}

            Debug.Print(My.Application.Info.AssemblyName, _
                                                                           "SQL_Utilities", _
                                                                           System.Reflection.MethodBase.GetCurrentMethod.Name, _
                                                                           keyVariableNames, _
                                                                           keyVariableValues, _
                                                                           ex.Message)

        Finally

            If Not conn_supplied AndAlso cn.State = ConnectionState.Open Then cn.Close()

        End Try

        If ds IsNot Nothing AndAlso ds.Tables.Count > 0 Then dt = ds.Tables(0)

        Return dt

    End Function

    Public Shared Sub Convert_Dataset_to_CSV(ByVal ds As DataSet, _
                                                                            ByVal incl_headings As Boolean, _
                                                                            ByRef csv As String, _
                                                                            ByRef success As Boolean, _
                                                                            Optional ByVal names_ExcludeColumns As List(Of String) = Nothing, _
                                                                            Optional ByVal fullFilePath As String = "")

        success = False
        csv = String.Empty

        If ds Is Nothing OrElse ds.Tables.Count = 0 Then Exit Sub

        Try

            Dim dt As DataTable
            dt = ds.Tables(0)

            Dim sb As New System.Text.StringBuilder
            Dim exclude_colNumber As New List(Of Integer)
            If incl_headings Then

                Dim sbHdr As New System.Text.StringBuilder
                Dim colNum As Integer = 0
                For Each col As DataColumn In dt.Columns

                    Dim colName As String = col.Caption

                    If names_ExcludeColumns IsNot Nothing AndAlso _
                       names_ExcludeColumns.Contains(colName) Then
                        exclude_colNumber.Add(colNum)
                    Else
                        sbHdr.Append(col.Caption & ",")
                    End If

                    colNum += 1

                Next

                sb.AppendLine(sbHdr.ToString.Trim(","))

            End If

            For Each dr As DataRow In dt.Rows

                Dim sbBody As New System.Text.StringBuilder
                For colNum As Integer = 0 To dt.Columns.Count - 1

                    If Not exclude_colNumber.Contains(colNum) Then

                        Dim value As String = String.Empty

                        If Not IsDBNull(dr.Item(colNum)) Then

                            value = dr.Item(colNum).ToString
                            sbBody.Append(value & ",")

                        End If

                    End If

                Next

                sb.AppendLine(sbBody.ToString.Trim(","))

            Next

            csv = sb.ToString

            If fullFilePath <> String.Empty Then
                My.Computer.FileSystem.WriteAllText(fullFilePath, csv, False)
            End If

            If csv <> String.Empty Then success = True

        Catch ex As Exception

            Debug.Print("Failed to convert dataset into csv. Reason: {0}", ex.Message)

        End Try

    End Sub

    Public Shared Sub Add_Input_Parameter_To_Command(Of U)(ByRef cmd As SqlCommand, _
                                                                                                             ByVal sqlDataType As SqlDbType, _
                                                                                                            ByRef paramName As String, _
                                                                                                            ByRef value As U, _
                                                                                                            Optional ByVal size As Integer = 0)
        Dim param As SqlParameter

        Try

            param = New SqlParameter(paramName, sqlDataType)
            param.Direction = ParameterDirection.Input
            param.Value = value

            If size <> 0 Then param.Size = size
            cmd.Parameters.Add(param)

        Catch ex As Exception

            Dim keyVariableNames() As String = {"param name"}
            Dim keyVariableValues() As String = {paramName}

            Debug.Print(My.Application.Info.AssemblyName, _
                                                        "SQL_Utilities", _
                                                        System.Reflection.MethodBase.GetCurrentMethod.Name, _
                                                        keyVariableNames, _
                                                        keyVariableValues, _
                                                        ex.Message)


        End Try

    End Sub


    Public Class ThreadSafe

        Public Sub Add_Input_Parameter_To_Command(Of U)(ByRef cmd As SqlCommand, _
                                                                                                              ByVal sqlDataType As SqlDbType, _
                                                                                                             ByRef paramName As String, _
                                                                                                             ByRef value As U, _
                                                                                                             Optional ByVal size As Integer = 0)
            Dim param As SqlParameter

            Try

                param = New SqlParameter(paramName, sqlDataType)
                param.Direction = ParameterDirection.Input
                param.Value = value

                If size <> 0 Then param.Size = size
                cmd.Parameters.Add(param)

            Catch ex As Exception

                Dim keyVariableNames() As String = {"param name"}
                Dim keyVariableValues() As String = {paramName}


                Debug.Print(My.Application.Info.AssemblyName, _
                                                        Me.GetType().Name, _
                                                        System.Reflection.MethodBase.GetCurrentMethod.Name, _
                                                        keyVariableNames, _
                                                        keyVariableValues, _
                                                        ex.Message)

            End Try

        End Sub

        Public Sub Clear_Table(ByRef connString As String, _
                                                 ByVal tableName As String, _
                                                 Optional ByVal filter As String = "")

            Try

                Using conn As New SqlConnection(connString)

                    conn.Open()

                    Dim squery As String
                    squery = String.Format("DELETE FROM {0} {1}", tableName, filter)

                    Dim cmd As New SqlCommand(squery, conn)
                    cmd.CommandTimeout = 600
                    cmd.ExecuteNonQuery()

                    conn.Close()

                End Using

            Catch ex As SqlException

                Debug.Print(String.Format("SQL table {0} could not be cleared {1} - Reason: {2}", tableName, filter, ex.Message))

                Dim keyVariableNames() As String = {"tablename", "filter"}
                Dim keyVariableValues() As String = {tableName, filter}


                Debug.Print(My.Application.Info.AssemblyName, _
                                                        Me.GetType().Name, _
                                                        System.Reflection.MethodBase.GetCurrentMethod.Name, _
                                                        keyVariableNames, _
                                                        keyVariableValues, _
                                                        ex.Message)

            End Try

        End Sub

        Public Sub Clear_Large_Table(ByRef connString As String, _
                                                             ByRef aColumnName As String, _
                                                              ByVal tableName As String)

            Try

                Using conn As New SqlConnection(connString)

                    conn.Open()

                    Dim query As String
                    query = String.Format("DELETE FROM {0} where {1} in (SELECT TOP 90 percent {2} FROM {3})", _
                                                             tableName, aColumnName, aColumnName, tableName)

                    Dim cmd As New SqlCommand(query, conn)
                    cmd.CommandTimeout = 600
                    cmd.ExecuteNonQuery()

                    Dim squery As String
                    squery = String.Format("DELETE FROM {0}", tableName)

                    Dim scmd As New SqlCommand(squery, conn)
                    scmd.CommandTimeout = 600
                    scmd.ExecuteNonQuery()

                    conn.Close()

                End Using

            Catch ex As SqlException

                Debug.Print(String.Format("Large SQL table {0} could not be cleared - Reason: {1}", tableName, ex.Message))

                Dim keyVariableNames() As String = {"tablename"}
                Dim keyVariableValues() As String = {tableName}


                Debug.Print(My.Application.Info.AssemblyName, _
                                                        Me.GetType().Name, _
                                                        System.Reflection.MethodBase.GetCurrentMethod.Name, _
                                                        keyVariableNames, _
                                                        keyVariableValues, _
                                                        ex.Message)

            End Try

        End Sub

        Public Function Load_Data_Table(ByVal connString As String, _
                                                                                ByVal tableName As String, _
                                                                                ByRef found As Boolean, _
                                                                                Optional ByVal query As String = "") _
                                                                                As DataTable

            Dim ds As New DataSet
            Dim dt As New DataTable

            found = False
            If tableName = String.Empty Then

                Return dt
                Exit Function

            End If

            Using cn As SqlConnection = New SqlConnection(connString)

                Try
                    cn.Open()

                    Dim squery As String
                    squery = "SELECT * FROM " & tableName

                    Dim cmd As New SqlCommand(squery, cn)
                    Dim da As New SqlDataAdapter(cmd)

                    da.Fill(ds)
                    found = True

                Catch ex As SqlException

                    Debug.Print("SQL table: {0} could not be loaded - Reason: {1}", tableName, ex.Message)

                    Dim keyVariableNames() As String = {"tablename"}
                    Dim keyVariableValues() As String = {tableName}


                    Debug.Print(My.Application.Info.AssemblyName, _
                                                            Me.GetType().Name, _
                                                            System.Reflection.MethodBase.GetCurrentMethod.Name, _
                                                            keyVariableNames, _
                                                            keyVariableValues, _
                                                            ex.Message)

                End Try

                cn.Close()

            End Using

            If ds.Tables.Count > 0 Then dt = ds.Tables(0)

            Return dt

        End Function

        Public Sub Safe_Get_SQL_Data_Item(Of T)(ByVal dr As DataRow, _
                                            ByVal sql_Item_Name As String, _
                                            ByRef varToSet As T)

            Try

                If Not Convert.IsDBNull(dr.Item(sql_Item_Name)) Then

                    varToSet = dr.Item(sql_Item_Name)

                End If

            Catch ex As Exception

                Dim keyVariableNames() As String = {"sql_Item_Name"}
                Dim keyVariableValues() As String = {sql_Item_Name}


                Debug.Print(My.Application.Info.AssemblyName, _
                                                        Me.GetType().Name, _
                                                        System.Reflection.MethodBase.GetCurrentMethod.Name, _
                                                        keyVariableNames, _
                                                        keyVariableValues, _
                                                        ex.Message)

            End Try

        End Sub

        Public Sub Safe_Get_SQL_Data_Item(Of T)(ByVal dr As SqlDataReader, _
                                                ByVal sql_Item_Name As String, _
                                                ByRef varToSet As T)

            Try

                If Not Convert.IsDBNull(dr(sql_Item_Name)) Then

                    varToSet = dr(sql_Item_Name)

                End If

            Catch ex As Exception

                Dim keyVariableNames() As String = {"sql_Item_Name"}
                Dim keyVariableValues() As String = {sql_Item_Name}


                Debug.Print(My.Application.Info.AssemblyName, _
                                                        Me.GetType().Name, _
                                                        System.Reflection.MethodBase.GetCurrentMethod.Name, _
                                                        keyVariableNames, _
                                                        keyVariableValues, _
                                                        ex.Message)

            End Try

        End Sub

        Public Sub Find_Tranche_Boundary_IDs_From_SQL_Table(ByVal connString As String, _
                                                                                                             ByVal tableName As String, _
                                                                                                             ByVal colName_ID As String, _
                                                                                                             ByVal trancheSize As String, _
                                                                                                             ByVal trancheNum_zeroBased As Integer, _
                                                                                                             ByRef startID As Integer, _
                                                                                                             ByRef endID As Integer, _
                                                                                                             ByRef success As Boolean)

            startID = 0
            endID = 0

            success = False
            Dim ids As New SortedDictionary(Of Integer, Integer) 'entry #, id #

            Try

                Using conn As New SqlConnection(connString)

                    conn.Open()

                    Dim query As String
                    query = String.Format("Select {0} FROM {1} order by {2}", _
                                                            colName_ID, tableName, colName_ID)

                    Dim cmd As New SqlCommand(query, conn)

                    Dim dr As SqlDataReader
                    dr = cmd.ExecuteReader

                    If dr.HasRows Then

                        Dim rowNum As Integer
                        While (dr.Read())

                            rowNum += 1
                            Dim id As Integer

                            Safe_Get_SQL_Data_Item(Of Integer)(dr, colName_ID, id)
                            If Not ids.ContainsKey(id) Then ids.Add(rowNum, id)

                        End While

                    End If

                    dr.Close()
                    conn.Close()

                    Dim fullTrancheNum As Integer = ids.Count \ trancheSize
                    Dim partialTranche As Boolean
                    If ((ids.Count \ trancheSize) <> (ids.Count / trancheSize)) Then _
                        partialTranche = True

                    Dim numTranches As Integer = fullTrancheNum
                    If partialTranche Then numTranches += 1

                    If trancheNum_zeroBased + 1 > numTranches Then Exit Sub

                    Dim startIDNum As Integer = trancheNum_zeroBased * trancheSize
                    Dim endIDNum As Integer = startIDNum + trancheSize - 1

                    Dim minID As Integer = Integer.MaxValue
                    Dim maxID As Integer = Integer.MinValue

                    For IDNum As Integer = startIDNum To endIDNum

                        If ids.ContainsKey(IDNum) Then

                            Dim currentID As Integer
                            currentID = ids(IDNum)

                            If currentID > maxID Then maxID = currentID
                            If currentID < minID Then minID = currentID

                        End If

                    Next

                    startID = minID
                    endID = maxID

                End Using

            Catch ex As Exception

                Debug.Print(String.Format("Encountered id boundaries for tranching from Table {0}.{1}Reason: {2}", _
                                                              tableName, vbCrLf, ex.Message))

                Dim keyVariableNames() As String = {"tablename"}
                Dim keyVariableValues() As String = {tableName}


                Debug.Print(My.Application.Info.AssemblyName, _
                                                        Me.GetType().Name, _
                                                        System.Reflection.MethodBase.GetCurrentMethod.Name, _
                                                        keyVariableNames, _
                                                        keyVariableValues, _
                                                        ex.Message)

            End Try



        End Sub

        Public Sub Get_Table_RowCount(ByRef connString As String, _
                                                               ByVal colName_ID As String, _
                                                               ByVal tableName As String, _
                                                               ByRef count As Integer, _
                                                               ByRef success As Boolean, _
                                                               Optional ByVal filter As String = "")

            success = False

            Try

                Dim dblQte As String = ChrW(34)

                Using conn As New SqlConnection(connString)
                    conn.Open()

                    Dim query As String
                    query = String.Format("SELECT count (*) as {0}row count{1} from (SELECT distinct([{2}]) FROM {3}{4}) as t", _
                                                            dblQte, dblQte, colName_ID, tableName, filter)

                    Dim cmd As New SqlCommand(query, conn)
                    count = cmd.ExecuteScalar()

                    success = True

                    conn.Close()

                End Using

            Catch ex As Exception

                Debug.Print("Failed to retreive row count from table {0} using id column named {1}.{2}Reason: {3}", _
                                         tableName, colName_ID, vbCrLf, ex.Message)

                Dim keyVariableNames() As String = {"tablename", "column id"}
                Dim keyVariableValues() As String = {tableName, colName_ID}


                Debug.Print(My.Application.Info.AssemblyName, _
                                                        Me.GetType().Name, _
                                                        System.Reflection.MethodBase.GetCurrentMethod.Name, _
                                                        keyVariableNames, _
                                                        keyVariableValues, _
                                                        ex.Message)

            End Try

        End Sub


    End Class

End Class
