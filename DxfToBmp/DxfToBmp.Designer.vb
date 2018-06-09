Imports DxfToBmp
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class DxfToBmpFrm
    Inherits System.Windows.Forms.Form

    'Форма переопределяет dispose для очистки списка компонентов.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Является обязательной для конструктора форм Windows Forms
    Private components As System.ComponentModel.IContainer

    'Примечание: следующая процедура является обязательной для конструктора форм Windows Forms
    'Для ее изменения используйте конструктор форм Windows Form.  
    'Не изменяйте ее в редакторе исходного кода.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(DxfToBmpFrm))
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.GroupBox5 = New System.Windows.Forms.GroupBox()
        Me.HatchBtn = New System.Windows.Forms.CheckBox()
        Me.CircleBtn = New System.Windows.Forms.CheckBox()
        Me.SplineBtn = New System.Windows.Forms.CheckBox()
        Me.LwPolylineBtn = New System.Windows.Forms.CheckBox()
        Me.LineBtn = New System.Windows.Forms.CheckBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.TableLayoutPanel3 = New System.Windows.Forms.TableLayoutPanel()
        Me.HeightPcsTxt = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.HeightMmTxt = New System.Windows.Forms.TextBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.WidthPcsTxt = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.WidthMmTxt = New System.Windows.Forms.TextBox()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.TableLayoutPanel4 = New System.Windows.Forms.TableLayoutPanel()
        Me.RightPcs = New System.Windows.Forms.NumericUpDown()
        Me.LeftPcs = New System.Windows.Forms.NumericUpDown()
        Me.DownPcs = New System.Windows.Forms.NumericUpDown()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.TopPcs = New System.Windows.Forms.NumericUpDown()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.PcsMmTxt = New System.Windows.Forms.TextBox()
        Me.SaveImageBtn = New System.Windows.Forms.Button()
        Me.RedrawBtn = New System.Windows.Forms.Button()
        Me.OpenDxfBtn = New System.Windows.Forms.Button()
        Me.ImageWorkArea1 = New DxfToBmp.ImageWorkArea()
        Me.EllipseBtn = New System.Windows.Forms.CheckBox()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.TableLayoutPanel3.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.TableLayoutPanel2.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.TableLayoutPanel4.SuspendLayout()
        CType(Me.RightPcs, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LeftPcs, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DownPcs, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TopPcs, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox3.SuspendLayout()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.Panel1, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.ImageWorkArea1, 1, 0)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(484, 412)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'Panel1
        '
        Me.Panel1.AutoScroll = True
        Me.Panel1.Controls.Add(Me.GroupBox5)
        Me.Panel1.Controls.Add(Me.GroupBox2)
        Me.Panel1.Controls.Add(Me.GroupBox1)
        Me.Panel1.Controls.Add(Me.GroupBox4)
        Me.Panel1.Controls.Add(Me.GroupBox3)
        Me.Panel1.Controls.Add(Me.SaveImageBtn)
        Me.Panel1.Controls.Add(Me.RedrawBtn)
        Me.Panel1.Controls.Add(Me.OpenDxfBtn)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(3, 3)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(144, 406)
        Me.Panel1.TabIndex = 0
        '
        'GroupBox5
        '
        Me.GroupBox5.AutoSize = True
        Me.GroupBox5.Controls.Add(Me.HatchBtn)
        Me.GroupBox5.Controls.Add(Me.EllipseBtn)
        Me.GroupBox5.Controls.Add(Me.CircleBtn)
        Me.GroupBox5.Controls.Add(Me.SplineBtn)
        Me.GroupBox5.Controls.Add(Me.LwPolylineBtn)
        Me.GroupBox5.Controls.Add(Me.LineBtn)
        Me.GroupBox5.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox5.Location = New System.Drawing.Point(0, 328)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(127, 121)
        Me.GroupBox5.TabIndex = 7
        Me.GroupBox5.TabStop = False
        Me.GroupBox5.Text = "Типы"
        '
        'HatchBtn
        '
        Me.HatchBtn.AutoSize = True
        Me.HatchBtn.Checked = True
        Me.HatchBtn.CheckState = System.Windows.Forms.CheckState.Checked
        Me.HatchBtn.Dock = System.Windows.Forms.DockStyle.Top
        Me.HatchBtn.Location = New System.Drawing.Point(3, 84)
        Me.HatchBtn.Name = "HatchBtn"
        Me.HatchBtn.Size = New System.Drawing.Size(121, 17)
        Me.HatchBtn.TabIndex = 2
        Me.HatchBtn.Text = "Hatch"
        Me.HatchBtn.UseVisualStyleBackColor = True
        '
        'CircleBtn
        '
        Me.CircleBtn.AutoSize = True
        Me.CircleBtn.Checked = True
        Me.CircleBtn.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CircleBtn.Dock = System.Windows.Forms.DockStyle.Top
        Me.CircleBtn.Location = New System.Drawing.Point(3, 67)
        Me.CircleBtn.Name = "CircleBtn"
        Me.CircleBtn.Size = New System.Drawing.Size(121, 17)
        Me.CircleBtn.TabIndex = 1
        Me.CircleBtn.Text = "Circle"
        Me.CircleBtn.UseVisualStyleBackColor = True
        '
        'SplineBtn
        '
        Me.SplineBtn.AutoSize = True
        Me.SplineBtn.Checked = True
        Me.SplineBtn.CheckState = System.Windows.Forms.CheckState.Checked
        Me.SplineBtn.Dock = System.Windows.Forms.DockStyle.Top
        Me.SplineBtn.Location = New System.Drawing.Point(3, 50)
        Me.SplineBtn.Name = "SplineBtn"
        Me.SplineBtn.Size = New System.Drawing.Size(121, 17)
        Me.SplineBtn.TabIndex = 4
        Me.SplineBtn.Text = "Spline"
        Me.SplineBtn.UseVisualStyleBackColor = True
        '
        'LwPolylineBtn
        '
        Me.LwPolylineBtn.AutoSize = True
        Me.LwPolylineBtn.Checked = True
        Me.LwPolylineBtn.CheckState = System.Windows.Forms.CheckState.Checked
        Me.LwPolylineBtn.Dock = System.Windows.Forms.DockStyle.Top
        Me.LwPolylineBtn.Location = New System.Drawing.Point(3, 33)
        Me.LwPolylineBtn.Name = "LwPolylineBtn"
        Me.LwPolylineBtn.Size = New System.Drawing.Size(121, 17)
        Me.LwPolylineBtn.TabIndex = 0
        Me.LwPolylineBtn.Text = "LwPolyline"
        Me.LwPolylineBtn.UseVisualStyleBackColor = True
        '
        'LineBtn
        '
        Me.LineBtn.AutoSize = True
        Me.LineBtn.Checked = True
        Me.LineBtn.CheckState = System.Windows.Forms.CheckState.Checked
        Me.LineBtn.Dock = System.Windows.Forms.DockStyle.Top
        Me.LineBtn.Location = New System.Drawing.Point(3, 16)
        Me.LineBtn.Name = "LineBtn"
        Me.LineBtn.Size = New System.Drawing.Size(121, 17)
        Me.LineBtn.TabIndex = 3
        Me.LineBtn.Text = "Line"
        Me.LineBtn.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.TableLayoutPanel3)
        Me.GroupBox2.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox2.Location = New System.Drawing.Point(0, 272)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(127, 56)
        Me.GroupBox2.TabIndex = 2
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Высота"
        '
        'TableLayoutPanel3
        '
        Me.TableLayoutPanel3.ColumnCount = 2
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel3.Controls.Add(Me.HeightPcsTxt, 0, 1)
        Me.TableLayoutPanel3.Controls.Add(Me.Label3, 0, 0)
        Me.TableLayoutPanel3.Controls.Add(Me.Label4, 1, 0)
        Me.TableLayoutPanel3.Controls.Add(Me.HeightMmTxt, 1, 1)
        Me.TableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel3.Location = New System.Drawing.Point(3, 16)
        Me.TableLayoutPanel3.Name = "TableLayoutPanel3"
        Me.TableLayoutPanel3.RowCount = 2
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel3.Size = New System.Drawing.Size(121, 37)
        Me.TableLayoutPanel3.TabIndex = 1
        '
        'HeightPcsTxt
        '
        Me.HeightPcsTxt.BackColor = System.Drawing.SystemColors.Window
        Me.HeightPcsTxt.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HeightPcsTxt.Location = New System.Drawing.Point(3, 16)
        Me.HeightPcsTxt.Name = "HeightPcsTxt"
        Me.HeightPcsTxt.ReadOnly = True
        Me.HeightPcsTxt.Size = New System.Drawing.Size(54, 20)
        Me.HeightPcsTxt.TabIndex = 0
        Me.HeightPcsTxt.TabStop = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label3.Location = New System.Drawing.Point(3, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(54, 13)
        Me.Label3.TabIndex = 1
        Me.Label3.Text = "pcs"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label4.Location = New System.Drawing.Point(63, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(55, 13)
        Me.Label4.TabIndex = 2
        Me.Label4.Text = "mm"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'HeightMmTxt
        '
        Me.HeightMmTxt.BackColor = System.Drawing.SystemColors.Window
        Me.HeightMmTxt.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HeightMmTxt.Location = New System.Drawing.Point(63, 16)
        Me.HeightMmTxt.Name = "HeightMmTxt"
        Me.HeightMmTxt.ReadOnly = True
        Me.HeightMmTxt.Size = New System.Drawing.Size(55, 20)
        Me.HeightMmTxt.TabIndex = 3
        Me.HeightMmTxt.TabStop = False
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.TableLayoutPanel2)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox1.Location = New System.Drawing.Point(0, 216)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(127, 56)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Ширина"
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.ColumnCount = 2
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel2.Controls.Add(Me.WidthPcsTxt, 0, 1)
        Me.TableLayoutPanel2.Controls.Add(Me.Label1, 0, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.Label2, 1, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.WidthMmTxt, 1, 1)
        Me.TableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(3, 16)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 2
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(121, 37)
        Me.TableLayoutPanel2.TabIndex = 0
        '
        'WidthPcsTxt
        '
        Me.WidthPcsTxt.BackColor = System.Drawing.SystemColors.Window
        Me.WidthPcsTxt.Dock = System.Windows.Forms.DockStyle.Fill
        Me.WidthPcsTxt.Location = New System.Drawing.Point(3, 16)
        Me.WidthPcsTxt.Name = "WidthPcsTxt"
        Me.WidthPcsTxt.ReadOnly = True
        Me.WidthPcsTxt.Size = New System.Drawing.Size(54, 20)
        Me.WidthPcsTxt.TabIndex = 0
        Me.WidthPcsTxt.TabStop = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label1.Location = New System.Drawing.Point(3, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(54, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "pcs"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label2.Location = New System.Drawing.Point(63, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(55, 13)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "mm"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'WidthMmTxt
        '
        Me.WidthMmTxt.BackColor = System.Drawing.SystemColors.Window
        Me.WidthMmTxt.Dock = System.Windows.Forms.DockStyle.Fill
        Me.WidthMmTxt.Location = New System.Drawing.Point(63, 16)
        Me.WidthMmTxt.Name = "WidthMmTxt"
        Me.WidthMmTxt.ReadOnly = True
        Me.WidthMmTxt.Size = New System.Drawing.Size(55, 20)
        Me.WidthMmTxt.TabIndex = 3
        Me.WidthMmTxt.TabStop = False
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.TableLayoutPanel4)
        Me.GroupBox4.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox4.Location = New System.Drawing.Point(0, 119)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(127, 97)
        Me.GroupBox4.TabIndex = 5
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Отступ, pcs"
        '
        'TableLayoutPanel4
        '
        Me.TableLayoutPanel4.ColumnCount = 2
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel4.Controls.Add(Me.RightPcs, 1, 3)
        Me.TableLayoutPanel4.Controls.Add(Me.LeftPcs, 0, 3)
        Me.TableLayoutPanel4.Controls.Add(Me.DownPcs, 1, 1)
        Me.TableLayoutPanel4.Controls.Add(Me.Label5, 0, 0)
        Me.TableLayoutPanel4.Controls.Add(Me.Label6, 1, 0)
        Me.TableLayoutPanel4.Controls.Add(Me.Label7, 0, 2)
        Me.TableLayoutPanel4.Controls.Add(Me.Label8, 1, 2)
        Me.TableLayoutPanel4.Controls.Add(Me.TopPcs, 0, 1)
        Me.TableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel4.Location = New System.Drawing.Point(3, 16)
        Me.TableLayoutPanel4.Name = "TableLayoutPanel4"
        Me.TableLayoutPanel4.RowCount = 4
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel4.Size = New System.Drawing.Size(121, 78)
        Me.TableLayoutPanel4.TabIndex = 1
        '
        'RightPcs
        '
        Me.RightPcs.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RightPcs.Location = New System.Drawing.Point(63, 55)
        Me.RightPcs.Name = "RightPcs"
        Me.RightPcs.Size = New System.Drawing.Size(55, 20)
        Me.RightPcs.TabIndex = 9
        '
        'LeftPcs
        '
        Me.LeftPcs.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LeftPcs.Location = New System.Drawing.Point(3, 55)
        Me.LeftPcs.Name = "LeftPcs"
        Me.LeftPcs.Size = New System.Drawing.Size(54, 20)
        Me.LeftPcs.TabIndex = 8
        '
        'DownPcs
        '
        Me.DownPcs.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DownPcs.Location = New System.Drawing.Point(63, 16)
        Me.DownPcs.Name = "DownPcs"
        Me.DownPcs.Size = New System.Drawing.Size(55, 20)
        Me.DownPcs.TabIndex = 7
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label5.Location = New System.Drawing.Point(3, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(54, 13)
        Me.Label5.TabIndex = 1
        Me.Label5.Text = "сверху"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label6.Location = New System.Drawing.Point(63, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(55, 13)
        Me.Label6.TabIndex = 2
        Me.Label6.Text = "снизу"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label7.Location = New System.Drawing.Point(3, 39)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(54, 13)
        Me.Label7.TabIndex = 4
        Me.Label7.Text = "слева"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label8.Location = New System.Drawing.Point(63, 39)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(55, 13)
        Me.Label8.TabIndex = 5
        Me.Label8.Text = "справа"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TopPcs
        '
        Me.TopPcs.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TopPcs.Location = New System.Drawing.Point(3, 16)
        Me.TopPcs.Name = "TopPcs"
        Me.TopPcs.Size = New System.Drawing.Size(54, 20)
        Me.TopPcs.TabIndex = 6
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.PcsMmTxt)
        Me.GroupBox3.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox3.Location = New System.Drawing.Point(0, 81)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(127, 38)
        Me.GroupBox3.TabIndex = 3
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "pcs/mm"
        '
        'PcsMmTxt
        '
        Me.PcsMmTxt.Dock = System.Windows.Forms.DockStyle.Top
        Me.PcsMmTxt.Location = New System.Drawing.Point(3, 16)
        Me.PcsMmTxt.Name = "PcsMmTxt"
        Me.PcsMmTxt.Size = New System.Drawing.Size(121, 20)
        Me.PcsMmTxt.TabIndex = 0
        Me.PcsMmTxt.Text = "5"
        '
        'SaveImageBtn
        '
        Me.SaveImageBtn.Dock = System.Windows.Forms.DockStyle.Top
        Me.SaveImageBtn.Location = New System.Drawing.Point(0, 54)
        Me.SaveImageBtn.Name = "SaveImageBtn"
        Me.SaveImageBtn.Size = New System.Drawing.Size(127, 27)
        Me.SaveImageBtn.TabIndex = 8
        Me.SaveImageBtn.Text = "Сохранить Bmp"
        Me.SaveImageBtn.UseVisualStyleBackColor = True
        '
        'RedrawBtn
        '
        Me.RedrawBtn.Dock = System.Windows.Forms.DockStyle.Top
        Me.RedrawBtn.Location = New System.Drawing.Point(0, 27)
        Me.RedrawBtn.Name = "RedrawBtn"
        Me.RedrawBtn.Size = New System.Drawing.Size(127, 27)
        Me.RedrawBtn.TabIndex = 6
        Me.RedrawBtn.Text = "Перерисовать"
        Me.RedrawBtn.UseVisualStyleBackColor = True
        '
        'OpenDxfBtn
        '
        Me.OpenDxfBtn.Dock = System.Windows.Forms.DockStyle.Top
        Me.OpenDxfBtn.Location = New System.Drawing.Point(0, 0)
        Me.OpenDxfBtn.Name = "OpenDxfBtn"
        Me.OpenDxfBtn.Size = New System.Drawing.Size(127, 27)
        Me.OpenDxfBtn.TabIndex = 4
        Me.OpenDxfBtn.Text = "Открыть Dxf"
        Me.OpenDxfBtn.UseVisualStyleBackColor = True
        '
        'ImageWorkArea1
        '
        Me.ImageWorkArea1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ImageWorkArea1.Image = Nothing
        Me.ImageWorkArea1.Location = New System.Drawing.Point(150, 0)
        Me.ImageWorkArea1.Margin = New System.Windows.Forms.Padding(0)
        Me.ImageWorkArea1.Name = "ImageWorkArea1"
        Me.ImageWorkArea1.RedrawImage = Nothing
        Me.ImageWorkArea1.Size = New System.Drawing.Size(334, 412)
        Me.ImageWorkArea1.TabIndex = 1
        '
        'EllipseBtn
        '
        Me.EllipseBtn.AutoSize = True
        Me.EllipseBtn.Checked = True
        Me.EllipseBtn.CheckState = System.Windows.Forms.CheckState.Checked
        Me.EllipseBtn.Dock = System.Windows.Forms.DockStyle.Top
        Me.EllipseBtn.Location = New System.Drawing.Point(3, 101)
        Me.EllipseBtn.Name = "EllipseBtn"
        Me.EllipseBtn.Size = New System.Drawing.Size(121, 17)
        Me.EllipseBtn.TabIndex = 5
        Me.EllipseBtn.Text = "Ellipse"
        Me.EllipseBtn.UseVisualStyleBackColor = True
        '
        'DxfToBmpFrm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(484, 412)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MinimumSize = New System.Drawing.Size(500, 450)
        Me.Name = "DxfToBmpFrm"
        Me.Text = "Dxf<>Bmp"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox5.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.TableLayoutPanel3.ResumeLayout(False)
        Me.TableLayoutPanel3.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.TableLayoutPanel2.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        Me.TableLayoutPanel4.ResumeLayout(False)
        Me.TableLayoutPanel4.PerformLayout()
        CType(Me.RightPcs, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LeftPcs, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DownPcs, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TopPcs, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents Panel1 As Panel
    Friend WithEvents OpenDxfBtn As Button
    Friend WithEvents GroupBox3 As GroupBox
    Friend WithEvents PcsMmTxt As TextBox
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents TableLayoutPanel3 As TableLayoutPanel
    Friend WithEvents HeightPcsTxt As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents HeightMmTxt As TextBox
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents TableLayoutPanel2 As TableLayoutPanel
    Friend WithEvents WidthPcsTxt As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents WidthMmTxt As TextBox
    Friend WithEvents ImageWorkArea1 As ImageWorkArea
    Friend WithEvents GroupBox4 As GroupBox
    Friend WithEvents TableLayoutPanel4 As TableLayoutPanel
    Friend WithEvents RightPcs As NumericUpDown
    Friend WithEvents LeftPcs As NumericUpDown
    Friend WithEvents DownPcs As NumericUpDown
    Friend WithEvents Label5 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents TopPcs As NumericUpDown
    Friend WithEvents RedrawBtn As Button
    Friend WithEvents GroupBox5 As GroupBox
    Friend WithEvents LwPolylineBtn As CheckBox
    Friend WithEvents CircleBtn As CheckBox
    Friend WithEvents HatchBtn As CheckBox
    Friend WithEvents SaveImageBtn As Button
    Friend WithEvents LineBtn As CheckBox
    Friend WithEvents SplineBtn As CheckBox
    Friend WithEvents EllipseBtn As CheckBox
End Class
