<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ImageWorkArea
    Inherits System.Windows.Forms.UserControl

    'Пользовательский элемент управления (UserControl) переопределяет метод Dispose для очистки списка компонентов.
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

    'Является обязательной для конструктора форм Windows Forms
    Private components As System.ComponentModel.IContainer

    'Примечание: следующая процедура является обязательной для конструктора форм Windows Forms
    'Для ее изменения используйте конструктор форм Windows Form.  
    'Не изменяйте ее в редакторе исходного кода.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Me.PictureBox1 = New System.Windows.Forms.PictureBox
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.PnlScaler = New System.Windows.Forms.Panel
        Me.tbScaler = New System.Windows.Forms.TrackBar
        Me.cbSizeToFit = New System.Windows.Forms.CheckBox
        Me.TmrScalerPanel = New System.Windows.Forms.Timer(Me.components)
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.PnlScaler.SuspendLayout()
        CType(Me.tbScaler, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'PictureBox1
        '
        Me.PictureBox1.Location = New System.Drawing.Point(86, 62)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(251, 106)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox1.TabIndex = 0
        Me.PictureBox1.TabStop = False
        '
        'Panel1
        '
        Me.Panel1.AutoScroll = True
        Me.Panel1.Controls.Add(Me.PnlScaler)
        Me.Panel1.Controls.Add(Me.PictureBox1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(450, 250)
        Me.Panel1.TabIndex = 0
        '
        'PnlScaler
        '
        Me.PnlScaler.BackColor = System.Drawing.Color.White
        Me.PnlScaler.Controls.Add(Me.tbScaler)
        Me.PnlScaler.Controls.Add(Me.cbSizeToFit)
        Me.PnlScaler.Location = New System.Drawing.Point(0, 0)
        Me.PnlScaler.Name = "PnlScaler"
        Me.PnlScaler.Size = New System.Drawing.Size(180, 40)
        Me.PnlScaler.TabIndex = 1
        Me.PnlScaler.Visible = False
        '
        'tbScaler
        '
        Me.tbScaler.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbScaler.AutoSize = False
        Me.tbScaler.BackColor = System.Drawing.Color.White
        Me.tbScaler.Enabled = False
        Me.tbScaler.Location = New System.Drawing.Point(40, 5)
        Me.tbScaler.Maximum = 40
        Me.tbScaler.Minimum = 1
        Me.tbScaler.Name = "tbScaler"
        Me.tbScaler.Size = New System.Drawing.Size(140, 30)
        Me.tbScaler.TabIndex = 11
        Me.tbScaler.TickStyle = System.Windows.Forms.TickStyle.None
        Me.tbScaler.Value = 40
        '
        'cbSizeToFit
        '
        Me.cbSizeToFit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cbSizeToFit.Appearance = System.Windows.Forms.Appearance.Button
        Me.cbSizeToFit.AutoSize = True
        Me.cbSizeToFit.Checked = True
        Me.cbSizeToFit.CheckState = System.Windows.Forms.CheckState.Checked
        Me.cbSizeToFit.Image = Global.DxfToBmp.My.Resources.Resources.cbSizeToFit
        Me.cbSizeToFit.Location = New System.Drawing.Point(5, 5)
        Me.cbSizeToFit.Name = "cbSizeToFit"
        Me.cbSizeToFit.Size = New System.Drawing.Size(30, 30)
        Me.cbSizeToFit.TabIndex = 10
        Me.cbSizeToFit.UseVisualStyleBackColor = True
        '
        'TmrScalerPanel
        '
        Me.TmrScalerPanel.Interval = 5000
        '
        'ImageWorkArea
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit
        Me.Controls.Add(Me.Panel1)
        Me.Margin = New System.Windows.Forms.Padding(0)
        Me.Name = "ImageWorkArea"
        Me.Size = New System.Drawing.Size(450, 250)
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.PnlScaler.ResumeLayout(False)
        Me.PnlScaler.PerformLayout()
        CType(Me.tbScaler, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents PnlScaler As System.Windows.Forms.Panel
    Friend WithEvents tbScaler As System.Windows.Forms.TrackBar
    Friend WithEvents cbSizeToFit As System.Windows.Forms.CheckBox
    Friend WithEvents TmrScalerPanel As System.Windows.Forms.Timer

End Class
