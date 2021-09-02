<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmFileSystem
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.rboLoad_SQL = New System.Windows.Forms.GroupBox()
        Me.btnFullPath = New System.Windows.Forms.Button()
        Me.txtFullFileRef = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.txtFileName = New System.Windows.Forms.TextBox()
        Me.btnDir = New System.Windows.Forms.Button()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.txtDir = New System.Windows.Forms.TextBox()
        Me.btnSelect = New System.Windows.Forms.Button()
        Me.rboLoad_SQL.SuspendLayout()
        Me.SuspendLayout()
        '
        'rboLoad_SQL
        '
        Me.rboLoad_SQL.Controls.Add(Me.btnFullPath)
        Me.rboLoad_SQL.Controls.Add(Me.txtFullFileRef)
        Me.rboLoad_SQL.Controls.Add(Me.Label9)
        Me.rboLoad_SQL.Controls.Add(Me.Label10)
        Me.rboLoad_SQL.Controls.Add(Me.txtFileName)
        Me.rboLoad_SQL.Controls.Add(Me.btnDir)
        Me.rboLoad_SQL.Controls.Add(Me.Label11)
        Me.rboLoad_SQL.Controls.Add(Me.txtDir)
        Me.rboLoad_SQL.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rboLoad_SQL.ForeColor = System.Drawing.Color.Blue
        Me.rboLoad_SQL.Location = New System.Drawing.Point(12, 12)
        Me.rboLoad_SQL.Name = "rboLoad_SQL"
        Me.rboLoad_SQL.Size = New System.Drawing.Size(528, 208)
        Me.rboLoad_SQL.TabIndex = 72
        Me.rboLoad_SQL.TabStop = False
        Me.rboLoad_SQL.Text = "FILE SYSTEM REFERENCE"
        '
        'btnFullPath
        '
        Me.btnFullPath.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnFullPath.Location = New System.Drawing.Point(456, 149)
        Me.btnFullPath.Name = "btnFullPath"
        Me.btnFullPath.Size = New System.Drawing.Size(44, 37)
        Me.btnFullPath.TabIndex = 36
        Me.btnFullPath.Text = "..."
        Me.btnFullPath.UseVisualStyleBackColor = True
        '
        'txtFullFileRef
        '
        Me.txtFullFileRef.Location = New System.Drawing.Point(19, 160)
        Me.txtFullFileRef.Name = "txtFullFileRef"
        Me.txtFullFileRef.Size = New System.Drawing.Size(412, 22)
        Me.txtFullFileRef.TabIndex = 35
        Me.txtFullFileRef.Tag = "start"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(17, 138)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(109, 16)
        Me.Label9.TabIndex = 34
        Me.Label9.Text = "Full Reference"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(17, 80)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(75, 16)
        Me.Label10.TabIndex = 33
        Me.Label10.Text = "FileName"
        '
        'txtFileName
        '
        Me.txtFileName.Location = New System.Drawing.Point(19, 104)
        Me.txtFileName.Name = "txtFileName"
        Me.txtFileName.Size = New System.Drawing.Size(481, 22)
        Me.txtFileName.TabIndex = 32
        Me.txtFileName.Tag = "start"
        '
        'btnDir
        '
        Me.btnDir.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDir.Location = New System.Drawing.Point(456, 40)
        Me.btnDir.Name = "btnDir"
        Me.btnDir.Size = New System.Drawing.Size(44, 37)
        Me.btnDir.TabIndex = 31
        Me.btnDir.Text = "..."
        Me.btnDir.UseVisualStyleBackColor = True
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(14, 25)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(71, 16)
        Me.Label11.TabIndex = 8
        Me.Label11.Text = "Directory"
        '
        'txtDir
        '
        Me.txtDir.Location = New System.Drawing.Point(19, 48)
        Me.txtDir.Name = "txtDir"
        Me.txtDir.Size = New System.Drawing.Size(412, 22)
        Me.txtDir.TabIndex = 7
        Me.txtDir.Tag = "start"
        Me.txtDir.Text = "C:\"
        '
        'btnSelect
        '
        Me.btnSelect.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSelect.Location = New System.Drawing.Point(441, 233)
        Me.btnSelect.Name = "btnSelect"
        Me.btnSelect.Size = New System.Drawing.Size(99, 37)
        Me.btnSelect.TabIndex = 37
        Me.btnSelect.Text = "Select"
        Me.btnSelect.UseVisualStyleBackColor = True
        '
        'frmFileSystem
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(561, 282)
        Me.Controls.Add(Me.btnSelect)
        Me.Controls.Add(Me.rboLoad_SQL)
        Me.Name = "frmFileSystem"
        Me.Text = "Select File"
        Me.rboLoad_SQL.ResumeLayout(False)
        Me.rboLoad_SQL.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents rboLoad_SQL As System.Windows.Forms.GroupBox
    Friend WithEvents btnFullPath As System.Windows.Forms.Button
    Friend WithEvents txtFullFileRef As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txtFileName As System.Windows.Forms.TextBox
    Friend WithEvents btnDir As System.Windows.Forms.Button
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents txtDir As System.Windows.Forms.TextBox
    Friend WithEvents btnSelect As System.Windows.Forms.Button
End Class
