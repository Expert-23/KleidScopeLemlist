Imports CBase.Primatives
Imports System.Drawing
Imports System.Windows.Forms
Public Class Form_Utilities

    Public Class cntrl_Checked_Listbox

        Public Shared Sub Set_Checked_Listbox_CheckState_Multiple(Of T)(ByRef cklb As CheckedListBox, ByVal selections As List(Of T), ByVal merge As Boolean)

            Dim indices As Dictionary(Of String, Integer)
            indices = Get_Checked_Listbox_Item_Indices(cklb)

            If Not merge Then Set_Checked_Listbox_CheckState_All(cklb, False)

            For Each item In selections

                If indices.ContainsKey(item.ToString) Then

                    cklb.SetItemChecked(indices(item.ToString), True)

                End If

            Next

        End Sub

        Public Shared Sub Set_Checked_Listbox_CheckState_All(ByRef cklb As CheckedListBox, ByVal status As Boolean)

            If cklb.Items.Count = 0 Then Exit Sub

            Dim count As Integer = Math.Max(0, cklb.Items.Count - 1)

            For i = 0 To count
                cklb.SetItemChecked(i, status)
            Next

        End Sub

        Public Shared Function Get_Checked_Listbox_All_Checked(Of T)(ByRef cklb As CheckedListBox) As List(Of T)

            Dim checkedItems As New List(Of T)
            For Each item In cklb.CheckedItems
                checkedItems.Add(item)
            Next

            Return checkedItems

        End Function

        Public Shared Function Get_Checked_Listbox_All(Of T)(ByRef cklb As CheckedListBox) As List(Of T)

            Dim items As New List(Of T)
            For Each item In cklb.Items
                items.Add(item)
            Next

            Return items

        End Function

        Public Shared Function Get_Checked_Listbox_Item_Indices(ByRef cklb As CheckedListBox) As Dictionary(Of String, Integer)

            Dim indices As New Dictionary(Of String, Integer)

            Dim count As Integer = Math.Max(0, cklb.Items.Count - 1)

            For i = 0 To count
                Dim item As String = cklb.Items(i).ToString
                indices.Add(item, i)
            Next

            Return indices

        End Function

        Public Shared Sub Load_Checked_List_Box(ByRef cklb As CheckedListBox, ByVal items As IEnumerable, Optional ByVal checkedAll As Boolean = False)

            cklb.Items.Clear()

            cklb.BeginUpdate()

            For Each item In items

                If Not String.IsNullOrWhiteSpace(item.ToString) Then
                    Dim index As Integer = cklb.Items.Add(item)
                    cklb.SetItemChecked(index, checkedAll)
                End If

            Next

            cklb.EndUpdate()

        End Sub


    End Class

    Public Class cntrl_Combobox

        Public Shared Sub Load_CBOBox_From_List(Of T)(ByVal cbo As ComboBox, ByVal lst As IEnumerable(Of T))

            Try

                cbo.Items.Clear()

                For Each item As T In lst
                    cbo.Items.Add(item)
                Next

            Catch ex As Exception

                Dim keyVariableNames() As String = {""}
                Dim keyVariableValues() As String = {""}

                'Debug.Print(My.Application.Info.AssemblyName, _
                '                                                               "Winform_Utility", _
                '                                                               System.Reflection.MethodBase.GetCurrentMethod.Name, _
                '                                                               keyVariableNames, _
                '                                                               keyVariableValues, _
                '                                                               ex.Message)

            End Try

        End Sub

        Public Shared Sub Append_List_To_Forms_ComboBox(Of T)(ByRef cbo As Windows.Forms.ComboBox,
                                                                                                           ByRef list As List(Of T))

            Try

                For Each item As T In list

                    cbo.Items.Add(item)

                Next

            Catch ex As Exception

                Dim keyVariableNames() As String = {""}
                Dim keyVariableValues() As String = {""}

                'Debug.Print(My.Application.Info.AssemblyName, _
                '                                                               "Bind_Utility", _
                '                                                               System.Reflection.MethodBase.GetCurrentMethod.Name, _
                '                                                               keyVariableNames, _
                '                                                               keyVariableValues, _
                '                                                               ex.Message)

            End Try


        End Sub

        Public Shared Sub Append_IDict_To_Forms_ComboBox(Of T, U)(ByRef cbo As Windows.Forms.ComboBox,
                                                                                                                     ByRef dict As IDictionary(Of T, U),
                                                                                                                     ByVal keys As Boolean)
            Try

                For Each specEntry As KeyValuePair(Of T, U) In dict

                    If keys Then
                        cbo.Items.Add(specEntry.Key)
                    Else
                        cbo.Items.Add(specEntry.Value)
                    End If

                Next

            Catch ex As Exception

                Dim keyVariableNames() As String = {""}
                Dim keyVariableValues() As String = {""}

                'Debug.Print(My.Application.Info.AssemblyName, _
                '                                                               "Bind_Utility", _
                '                                                               System.Reflection.MethodBase.GetCurrentMethod.Name, _
                '                                                               keyVariableNames, _
                '                                                               keyVariableValues, _
                '                                                               ex.Message)

            End Try

        End Sub

        Public Shared Sub Bind_List_To_Forms_ComboBox(Of T)(ByRef cbo As Windows.Forms.ComboBox,
                                                                                                         ByRef list As List(Of T))

            Try

                cbo.DataSource = Nothing
                cbo.Items.Clear()
                cbo.SelectedText = ""

                Append_List_To_Forms_ComboBox(Of T)(cbo, list)

            Catch ex As Exception

                Dim keyVariableNames() As String = {""}
                Dim keyVariableValues() As String = {""}

                'Debug.Print(My.Application.Info.AssemblyName, _
                '                                                               "Bind_Utility", _
                '                                                               System.Reflection.MethodBase.GetCurrentMethod.Name, _
                '                                                               keyVariableNames, _
                '                                                               keyVariableValues, _
                '                                                               ex.Message)

            End Try

        End Sub

        Public Shared Sub Bind_IDict_To_Forms_ComboBox(Of T, U)(ByRef cbo As Windows.Forms.ComboBox,
                                                                                                               ByRef dict As IDictionary(Of T, U),
                                                                                                               ByVal keys As Boolean)

            Try

                cbo.DataSource = Nothing
                cbo.Items.Clear()

                Append_IDict_To_Forms_ComboBox(Of T, U)(cbo, dict, keys)

            Catch ex As Exception

                Dim keyVariableNames() As String = {""}
                Dim keyVariableValues() As String = {""}

                'Debug.Print(My.Application.Info.AssemblyName, _
                '                                                               "Bind_Utility", _
                '                                                               System.Reflection.MethodBase.GetCurrentMethod.Name, _
                '                                                               keyVariableNames, _
                '                                                               keyVariableValues, _
                '                                                               ex.Message)

            End Try

        End Sub

    End Class

    Public Class cntrl_Datagridview

        Public Shared Sub Clone_DataGridView(ByVal dgvSource As DataGridView, ByRef dgvTarget As DataGridView)

            dgvTarget.Rows.Clear()
            For Each dr As DataGridViewRow In dgvSource.Rows

                Dim drNew As New DataGridViewRow

                For Each item As DataGridViewCell In dr.Cells

                    Dim cellNew As DataGridViewCell

                    cellNew = item.Clone
                    cellNew.Value = item.Value

                    drNew.Cells.Add(cellNew)

                Next

                dgvTarget.Rows.Add(drNew)

            Next

        End Sub

        Public Shared Sub Get_Values_DGV_ComboBoxCell(ByVal cellNumber As Integer,
                                                                                               ByVal dr As DataGridViewRow,
                                                                                               ByRef itemNames As IList(Of String),
                                                                                               Optional ByVal defaultValue As String = "")

            itemNames = New List(Of String)

            Dim cBox As DataGridViewComboBoxCell
            Dim cCol As DataGridViewComboBoxColumn

            cBox = CType(dr.Cells(cellNumber), DataGridViewComboBoxCell)
            cCol = CType(dr.DataGridView.Columns(cellNumber), DataGridViewComboBoxColumn)

            For Each itemname As String In cBox.Items
                itemNames.Add(itemname)
            Next

        End Sub

        Public Shared Sub Get_Value_From_DGV_ComboBoxCell(ByVal cell As DataGridViewCell,
                                                                                                ByRef varToSet As String,
                                                                                                Optional ByRef defaultValue As String = "")

            Dim cBox As DataGridViewComboBoxCell
            cBox = CType(cell, DataGridViewComboBoxCell)

            If cBox.Value Is Nothing Then
                varToSet = defaultValue
            Else
                varToSet = cBox.Value.ToString
            End If

        End Sub

        Public Shared Sub Get_Value_From_DGV_CheckBoxCell(ByVal cell As DataGridViewCell,
                                                                                                       ByRef varToSet As String,
                                                                                                       Optional ByRef defaultValue As String = "")

            Dim cBox As DataGridViewCheckBoxCell
            cBox = CType(cell, DataGridViewCheckBoxCell)

            If cBox.Value Is Nothing Then
                varToSet = defaultValue
            Else
                varToSet = cBox.Value.ToString
            End If

        End Sub

        Public Shared Sub Safe_Get_DGV_Data_Item(Of T)(ByVal dr As DataGridViewRow,
                                    ByVal sql_Item_Name As String,
                                    ByRef varToSet As T)

            Try

                If Not Convert.IsDBNull(dr.Cells(sql_Item_Name)) Then

                    varToSet = dr.Cells(sql_Item_Name).Value

                End If

            Catch ex As Exception

                Debug.Print(ex.Message)

            End Try

        End Sub

        Public Shared Sub Safe_Get_DGV_Data_Item(Of T)(ByVal dr As DataGridViewRow,
                                    ByVal cellNumber As Integer,
                                    ByRef varToSet As T)

            Try

                If Not Convert.IsDBNull(dr.Cells(cellNumber)) Then

                    varToSet = dr.Cells(cellNumber).Value

                End If

            Catch ex As Exception

                Debug.Print(ex.Message)

            End Try

        End Sub

        Public Shared Sub Set_Values_DGV_ComboBoxCell(ByVal cellNumber As Integer,
                                                                                      ByVal itemNames As IList(Of String),
                                                                                      ByRef dr As DataGridViewRow,
                                                                                      Optional ByVal defaultValue As String = "",
                                                                                      Optional ByVal defaultItemNo As Integer = -1)

            Dim cBox As DataGridViewComboBoxCell
            Dim cCol As DataGridViewComboBoxColumn

            cBox = CType(dr.Cells(cellNumber), DataGridViewComboBoxCell)
            cCol = CType(dr.DataGridView.Columns(cellNumber), DataGridViewComboBoxColumn)

            For Each itemname As String In itemNames
                cBox.Items.Add(itemname)
            Next

            Try
                If defaultValue <> String.Empty Then cBox.Value = defaultValue
                If defaultItemNo > -1 AndAlso cBox.Items.Count > defaultItemNo Then cBox.Value = cBox.Items(defaultItemNo)
            Catch ex As DataException

            End Try

        End Sub

        Public Shared Sub Set_Values_DGV_ComboBoxColumn(ByVal cellNumber As Integer,
                                                                                                     ByVal itemNames As Object,
                                                                                                     ByRef dgv As DataGridView)

            Dim cCol As DataGridViewComboBoxColumn
            cCol = CType(dgv.Columns(cellNumber), DataGridViewComboBoxColumn)

            For Each itemname As String In itemNames.keys
                cCol.Items.Add(itemname)
            Next

        End Sub

        'Thread Safe

        Public Function Get_Value_From_DGV_CheckBoxCell(ByRef dr As DataGridViewRow, ByVal colNum As Integer, Optional ByRef defaultValue As Boolean = False) As Boolean

            Dim cBox As DataGridViewCheckBoxCell
            cBox = CType(dr.Cells(colNum), DataGridViewCheckBoxCell)

            Dim value As Boolean = CBool(cBox.EditedFormattedValue)

            Return value

        End Function

        Public Sub Set_Value_In_DGV_CheckBoxCell(ByRef dr As DataGridViewRow, ByVal colNum As Integer, ByVal checked As Boolean)

            Dim cBox As DataGridViewCheckBoxCell
            cBox = CType(dr.Cells(colNum), DataGridViewCheckBoxCell)

            cBox.Value = checked

        End Sub

    End Class

    Public Class cntrl_Datatable

        Public Shared Sub Pare_Down_Datatable_Exclude_Columns(ByRef dt As DataTable, ByVal excludes As List(Of String))

            If excludes Is Nothing Then Exit Sub

            Dim remColumns As New List(Of DataColumn)
            For Each col As DataColumn In dt.Columns
                If excludes.Contains(col.ColumnName) Then remColumns.Add(col)
            Next

            For Each remCol As DataColumn In remColumns
                dt.Columns.Remove(remCol)
            Next

        End Sub

        Public Shared Sub Pare_Down_Datatable_Include_Columns(ByRef dt As DataTable, ByVal includes As List(Of String))

            If includes Is Nothing Then Exit Sub
            If dt Is Nothing Then Exit Sub

            Dim remColumns As New List(Of DataColumn)
            For Each col As DataColumn In dt.Columns
                If Not includes.Contains(col.ColumnName) Then remColumns.Add(col)
            Next

            For Each remCol As DataColumn In remColumns
                dt.Columns.Remove(remCol)
            Next

        End Sub

    End Class

    Public Class cntrl_ImageList

        Public Shared Sub Load_ImageList_From_Dictionary(ByVal iL As ImageList, ByVal dict As Dictionary(Of String, Image))

            Try

                iL.Images.Clear()

                For Each specEntry As KeyValuePair(Of String, Image) In dict

                    iL.Images.Add(specEntry.Key, specEntry.Value)

                Next

            Catch ex As Exception

                Dim keyVariableNames() As String = {""}
                Dim keyVariableValues() As String = {""}

                'Debug.Print(My.Application.Info.AssemblyName, _
                '                                                               "Winform_Utility", _
                '                                                               System.Reflection.MethodBase.GetCurrentMethod.Name, _
                '                                                               keyVariableNames, _
                '                                                               keyVariableValues, _
                '                                                               ex.Message)

            End Try

        End Sub

    End Class

    Public Class cntrl_Listbox

        Public Shared Sub Load_ListBox_From_Dictionary(Of T)(ByVal lb As ListBox, ByVal dict As Dictionary(Of String, T))

            Try

                lb.Items.Clear()

                For Each specEntry As KeyValuePair(Of String, T) In dict

                    lb.Items.Add(specEntry.Key)

                Next

            Catch ex As Exception

                Dim keyVariableNames() As String = {""}
                Dim keyVariableValues() As String = {""}

                'Debug.Print(My.Application.Info.AssemblyName, _
                '                                                               "Winform_Utility", _
                '                                                               System.Reflection.MethodBase.GetCurrentMethod.Name, _
                '                                                               keyVariableNames, _
                '                                                               keyVariableValues, _
                '                                                               ex.Message)

            End Try

        End Sub

        Public Shared Sub Load_ListBox_From_List(Of T)(ByVal lb As ListBox, ByVal lst As List(Of T))

            Try

                lb.Items.Clear()

                For Each item As T In lst
                    lb.Items.Add(item)
                Next

            Catch ex As Exception

                Dim keyVariableNames() As String = {""}
                Dim keyVariableValues() As String = {""}

                'Debug.Print(My.Application.Info.AssemblyName, _
                '                                                               "Winform_Utility", _
                '                                                               System.Reflection.MethodBase.GetCurrentMethod.Name, _
                '                                                               keyVariableNames, _
                '                                                               keyVariableValues, _
                '                                                               ex.Message)

            End Try

        End Sub


        Public Shared Sub Append_List_To_Forms_ListBox(Of T)(ByRef lstbox As Windows.Forms.ListBox,
                                                                                                         ByRef list As List(Of T))
            Try

                For Each item As T In list

                    lstbox.Items.Add(item)

                Next

            Catch ex As Exception

                Dim keyVariableNames() As String = {""}
                Dim keyVariableValues() As String = {""}

                'Debug.Print(My.Application.Info.AssemblyName, _
                '                                                               "Bind_Utility", _
                '                                                               System.Reflection.MethodBase.GetCurrentMethod.Name, _
                '                                                               keyVariableNames, _
                '                                                               keyVariableValues, _
                '                                                               ex.Message)

            End Try

        End Sub

        Public Shared Sub Append_IDict_To_Forms_ListBox(Of T, U)(ByRef lstbox As Windows.Forms.ListBox,
                                                                                                              ByRef dict As IDictionary(Of T, U),
                                                                                                              ByVal keys As Boolean)

            Try

                For Each specEntry As KeyValuePair(Of T, U) In dict

                    If keys Then
                        lstbox.Items.Add(specEntry.Key)
                    Else
                        lstbox.Items.Add(specEntry.Value)
                    End If

                Next

            Catch ex As Exception

                Dim keyVariableNames() As String = {""}
                Dim keyVariableValues() As String = {""}

                'Debug.Print(My.Application.Info.AssemblyName, _
                '                                                               "Bind_Utility", _
                '                                                               System.Reflection.MethodBase.GetCurrentMethod.Name, _
                '                                                               keyVariableNames, _
                '                                                               keyVariableValues, _
                '                                                               ex.Message)

            End Try

        End Sub

        Public Shared Sub Bind_List_To_Forms_ListBox(Of T)(ByRef lstbox As Windows.Forms.ListBox,
                                                                                                 ByRef list As List(Of T))

            Try
                lstbox.DataSource = Nothing
                lstbox.Items.Clear()

                Append_List_To_Forms_ListBox(Of T)(lstbox, list)

            Catch ex As Exception

                Dim keyVariableNames() As String = {""}
                Dim keyVariableValues() As String = {""}

                'Debug.Print(My.Application.Info.AssemblyName, _
                '                                                               "Bind_Utility", _
                '                                                               System.Reflection.MethodBase.GetCurrentMethod.Name, _
                '                                                               keyVariableNames, _
                '                                                               keyVariableValues, _
                '                                                               ex.Message)

            End Try


        End Sub

        Public Shared Sub Bind_IDict_To_Forms_ListBox(Of T, U)(ByRef lstbox As Windows.Forms.ListBox,
                                                                                                           ByRef dict As IDictionary(Of T, U),
                                                                                                           ByVal keys As Boolean)

            Try

                lstbox.DataSource = Nothing
                lstbox.Items.Clear()

                Append_IDict_To_Forms_ListBox(Of T, U)(lstbox, dict, keys)

            Catch ex As Exception

                Dim keyVariableNames() As String = {""}
                Dim keyVariableValues() As String = {""}

                'Debug.Print(My.Application.Info.AssemblyName, _
                '                                                               "Bind_Utility", _
                '                                                               System.Reflection.MethodBase.GetCurrentMethod.Name, _
                '                                                               keyVariableNames, _
                '                                                               keyVariableValues, _
                '                                                               ex.Message)

            End Try

        End Sub

    End Class

    Public Class cntrl_Listview

        'Public Shared Sub Load_ListView_From_Dictionary(ByRef Lv As ListView, ByVal dict As Dictionary(Of String, Image))

        '    Dim iL As New ImageList

        '    Load_ImageList_From_Dictionary(iL, dict)
        '    Load_ListView_From_ImageList(Lv, iL)

        'End Sub

        Public Shared Sub Load_ListView_From_IDict(Of T As IDictionary)(ByVal collect As T, ByRef lv As ListView)

            lv.Items.Clear()
            If collect Is Nothing Then Exit Sub

            For Each entry In collect

                Dim lvi As New ListViewItem
                lv.Items.Add(lvi)

                With lvi

                    Dim x As New ListViewItem.ListViewSubItem
                    x.Font = New Font("Arial", 12)
                    x.Text = entry.key

                    Dim y As New ListViewItem.ListViewSubItem
                    y.Font = New Font("Arial", 12)
                    y.Text = entry.value.ToString

                    .SubItems(0) = x
                    .SubItems.Add(y)

                End With

            Next

        End Sub

        Public Shared Sub Load_ListView_From_Tuple(Of T, U)(ByVal items As List(Of Tuple(Of T, U)), ByRef lv As ListView)

            lv.Items.Clear()
            If items Is Nothing Then Exit Sub

            For Each entry In items

                Dim lvi As New ListViewItem
                lv.Items.Add(lvi)

                With lvi

                    Dim x As New ListViewItem.ListViewSubItem
                    x.Text = entry.Item1.ToString

                    Dim y As New ListViewItem.ListViewSubItem
                    y.Text = entry.Item2.ToString

                    .SubItems(0) = x
                    .SubItems.Add(y)

                End With

            Next

        End Sub

        Public Shared Sub Load_ListView_From_Tuple(Of T, U, V)(ByVal items As List(Of Tuple(Of T, U, V)), ByRef lv As ListView)

            lv.Items.Clear()
            If items Is Nothing Then Exit Sub

            Dim fnt = New Font(New FontFamily("Arial"), 16, FontStyle.Bold)

            For Each entry In items

                Dim lvi As New ListViewItem
                lv.Columns(0).Width = 50
                lv.Columns(1).Width = 150
                ' lv.Columns(2).Width = 50
                lv.Items.Add(lvi)

                With lvi

                    Dim x As New ListViewItem.ListViewSubItem
                    x.Text = String.Empty
                    If entry.Item1 IsNot Nothing Then x.Text = entry.Item1.ToString
                    x.Font = fnt

                    Dim y As New ListViewItem.ListViewSubItem
                    y.Text = String.Empty
                    If entry.Item2 IsNot Nothing Then y.Text = entry.Item2.ToString
                    y.Font = fnt

                    'Dim z As New ListViewItem.ListViewSubItem
                    'z.Text = String.Empty
                    'If entry.Item3 IsNot Nothing Then z.Text = entry.Item3.ToString
                    'z.Font = fnt

                    .SubItems(0) = x
                    .SubItems.Add(y)
                    '.SubItems.Add(z)

                End With

            Next

        End Sub

        Public Shared Sub Load_ListView_From_ImageList(ByRef Lv As ListView, ByVal iL As ImageList)

            Dim imName As String
            Dim numImage As Integer
            Dim i As Integer

            Try

                numImage = iL.Images.Count

                For i = 0 To numImage - 1
                    Dim specImage As Image

                    imName = iL.Images.Keys(i)
                    specImage = iL.Images.Item(imName)

                    Dim newitem As New ListViewItem(imName, 0)
                    newitem.ImageKey = imName

                    'Add the items to the ListView.
                    Lv.Items.AddRange(New ListViewItem() {newitem})

                Next i

                '  Dim imageListSmall As New ImageList()

                'Assign the ImageList objects to the ListView.
                Lv.LargeImageList = iL

            Catch ex As Exception

                Dim keyVariableNames() As String = {""}
                Dim keyVariableValues() As String = {""}

                'Debug.Print(My.Application.Info.AssemblyName, _
                '                                                               "Winform_Utility", _
                '                                                               System.Reflection.MethodBase.GetCurrentMethod.Name, _
                '                                                               keyVariableNames, _
                '                                                               keyVariableValues, _
                '                                                               ex.Message)

            End Try

        End Sub

    End Class

    Public Class cntrl_RichTextBox

        Public Shared Sub Convert_RichTextBox_Contents_To_Bytes(ByRef rtf As RichTextBox,
                                                                                                                ByRef bytes As Byte(),
                                                                                                                ByRef success As Boolean)
            success = False
            If rtf Is Nothing Then Exit Sub

            Try

                Dim strm As New System.IO.MemoryStream
                rtf.SaveFile(strm, RichTextBoxStreamType.RichText)

                strm.Position = 0
                bytes = strm.ToArray

                success = True

            Catch ex As Exception

                Debug.Print("Failed to convert richtextbox to byte array. Reason: {0}", ex.Message)

            End Try

        End Sub

        'Public Shared Sub Convert_Bytes_To_RichTextBox(ByVal bytes As Byte(),
        '                                                                                       ByRef rtf As RichTextBox,
        '                                                                                       ByRef success As Boolean)
        '    success = False
        '    If bytes Is Nothing Then Exit Sub

        '    Try

        '        Dim strm As System.IO.MemoryStream
        '        Dim mu As New Memory_Utilities
        '        strm = mu.Write_Bytes_To_Memory_Stream(bytes)

        '        strm.Position = 0
        '        rtf.LoadFile(strm, RichTextBoxStreamType.RichText)

        '    Catch ex As Exception

        '        Debug.Print("Failed to convert byte array to richtextbox. Reason: {0}", ex.Message)

        '    End Try

        'End Sub

        '    Public Shared Sub Load_RichTextBox_From_String(ByVal strng As String, ByRef rtf As RichTextBox, ByRef success As Boolean)

        '        Try

        '            success = False
        '            rtf = New RichTextBox

        '            Dim bytes As Byte()
        '            bytes = System.Text.Encoding.UTF8.GetBytes(strng)
        '            If bytes.Count = 0 Then Exit Sub

        '            Convert_Bytes_To_RichTextBox(bytes, rtf, success)

        '        Catch ex As Exception

        '            Debug.Print("Failed to load richtextbox from string. Reason: {0}", ex.Message)

        '        End Try

        '    End Sub

    End Class

    Public Class cntrl_Query_Textbox

        Public Function Parse_Comma_Delimited_List_With_Ranges_Into_List_Integers(ByVal maxRows As Integer, ByVal specified As String) As List(Of Integer)

            Dim checkedRoWNums As New List(Of Integer)

            If Not String.IsNullOrWhiteSpace(specified) Then

                Dim parts As String() = specified.Split(",")
                Dim ranges As New List(Of String)
                For Each part In parts

                    part = part.Trim(" ")
                    Dim rowNum As Integer

                    If part.Contains("-") Then
                        ranges.Add(part)
                    ElseIf Integer.TryParse(part, rowNum) Then
                        If rowNum > 0 AndAlso rowNum < maxRows + 1 Then _
                    checkedRoWNums.Add(rowNum)
                    End If

                    Dim blocks As New List(Of Tuple(Of Integer, Integer))
                    For Each range In ranges

                        Dim ends As String() = range.Split("-")
                        If ends.Count = 2 Then

                            Dim startRow As Integer
                            Dim endRow As Integer

                            Dim valid As Boolean = True
                            If Not Integer.TryParse(ends(0).Trim(" "), startRow) Then valid = False
                            If Not Integer.TryParse(ends(1).Trim(" "), endRow) Then valid = False

                            If valid Then

                                For i = startRow To endRow
                                    checkedRoWNums.Add(i)
                                Next

                            End If

                        End If

                    Next

                Next

            End If

            Return checkedRoWNums

        End Function


    End Class

End Class
