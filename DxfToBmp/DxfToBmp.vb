Imports System.IO
Imports System.Text
Imports netDxf
Imports netDxf.Blocks
Imports netDxf.Collections
Imports netDxf.Entities
Imports netDxf.Header
Imports netDxf.Objects
Imports netDxf.Tables
Imports netDxf.Units
Imports Attribute = netDxf.Entities.Attribute
Imports Image = netDxf.Entities.Image
Imports Point = netDxf.Entities.Point
Imports Trace = netDxf.Entities.Trace
Imports System.Math
Imports System.Drawing.Drawing2D
Public Class DxfToBmpFrm
    Private DxfDoc As DxfDocument
    Private WidthMm_ As Double
    Private HeightMm_ As Double
    Private WidthPcs_ As Integer
    Private HeightPcs_ As Integer
    Private Boundary_ As Bounding

    Function LoadDxf(ByVal file As String) As DxfDocument
        Dim fileInfo As New FileInfo(file)
        If Not fileInfo.Exists Then
            Return Nothing
        End If
        Dim isBinary As Boolean
        Dim dxfVersion As DxfVersion = DxfDocument.CheckDxfFileVersion(file, isBinary)
        If dxfVersion = DxfVersion.Unknown Then
            Return Nothing
        End If
        If dxfVersion < DxfVersion.AutoCad2000 Then
            Return Nothing
        End If
        Dim dxf As DxfDocument = DxfDocument.Load(file)
        If dxf Is Nothing Then
            Return Nothing
        End If

        Return dxf
    End Function

    Private Sub DxfToBmp_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.Text = Me.Text & " Ver-" & My.Application.Info.Version.Major & "." & My.Application.Info.Version.Minor & "." & My.Application.Info.Version.Build & "." & My.Application.Info.Version.MinorRevision
        ImageWorkArea1.ScalerMaximum = tbScaler.Maximum
        ImageWorkArea1.ScalerMinimum = tbScaler.Minimum
        ImageWorkArea1.SizeToFit = Me.cbSizeToFit.Checked

        Dim fileInfo As New FileInfo(My.Application.Info.DirectoryPath & "\Sample.dxf")

        If Not FileInfo.Exists Then
            Exit Sub
        End If

        Try
            DxfDoc = LoadDxf(fileInfo.FullName)
            Boundary_ = GetBoundary()
        Catch ex As Exception
            Exit Sub
        End Try

        If Boundary_ Is Nothing Then
            Exit Sub
        End If
        CalculateSize()
        DrawBitmap()
    End Sub

    Private Sub CalculateSize()
        If PcsMmTxt.Text = "" Then
            PcsMmTxt.Text = "5"
        End If
        Dim PcsMm As Integer = CInt(PcsMmTxt.Text)
        Dim MmPcs As Double = 1 / PcsMm
        Dim WHTemp As Integer
        WidthMm_ = (MmPcs * (LeftPcs.Value + RightPcs.Value)) + Boundary_.Max.X - Boundary_.Min.X
        WHTemp = Math.Floor(WidthMm_ / MmPcs)
        WidthMm_ = MmPcs * WHTemp
        HeightMm_ = (MmPcs * (TopPcs.Value + DownPcs.Value)) + Boundary_.Max.Y - Boundary_.Min.Y
        WHTemp = Math.Floor(HeightMm_ / MmPcs)
        HeightMm_ = MmPcs * WHTemp
        WidthMmTxt.Text = WidthMm_
        HeightMmTxt.Text = HeightMm_

        WidthPcs_ = WidthMm_ * PcsMm
        HeightPcs_ = HeightMm_ * PcsMm

        WidthPcsTxt.Text = WidthPcs_
        HeightPcsTxt.Text = HeightPcs_
    End Sub

    Private Function GetBoundary() As Bounding
        Dim Boundary As Bounding
        If LwPolylineBtn.Checked = True Then
            For Each LwPolyline As LwPolyline In DxfDoc.LwPolylines
                For i As Integer = 0 To LwPolyline.Vertexes.Count - 2
                    Dim Spoint As LwPolylineVertex = LwPolyline.Vertexes(i)
                    Dim Epoint As LwPolylineVertex = LwPolyline.Vertexes(i + 1)
                    If Spoint.Bulge = 0 Then
                        Dim Tb As Bounding = New Bounding(Spoint.Position, Epoint.Position)
                        Boundary = Bounding.Union(Boundary, Tb)
                    ElseIf Spoint.Bulge <> 0 Then
                        Dim Tb As Bounding = New Bounding(Spoint.Position, Epoint.Position, Spoint.Bulge)
                        Boundary = Bounding.Union(Boundary, Tb)
                    End If
                Next
            Next
        End If

        If ArcBtn.Checked = True Then
            For Each Arc As Arc In DxfDoc.Arcs
                Dim Radius As Double = Arc.Radius
                Dim StartAngle As Double = Arc.StartAngle
                Dim EndAngle As Double = Arc.EndAngle
                Dim Angle As Double
                Dim StartPoint As Vector2
                Dim EndPoint As Vector2
                If EndAngle > StartAngle Then
                    Angle = EndAngle - StartAngle
                    StartPoint = New Vector2(Arc.Center.X + Radius * Cos(StartAngle / 180 * PI), Arc.Center.Y + Radius * Sin(StartAngle / 180 * PI))
                    EndPoint = New Vector2(Arc.Center.X + Radius * Cos(EndAngle / 180 * PI), Arc.Center.Y + Radius * Sin(EndAngle / 180 * PI))
                Else
                    Angle = -1 * (360 + (EndAngle - StartAngle))
                    EndPoint = New Vector2(Arc.Center.X + Radius * Cos(StartAngle / 180 * PI), Arc.Center.Y + Radius * Sin(StartAngle / 180 * PI))
                    StartPoint = New Vector2(Arc.Center.X + Radius * Cos(EndAngle / 180 * PI), Arc.Center.Y + Radius * Sin(EndAngle / 180 * PI))
                End If
                Dim Bulge As Double = Tan((Angle / 180 * PI) / 4)
                Dim Tb As Bounding = New Bounding(StartPoint, EndPoint, Bulge)
                Boundary = Bounding.Union(Boundary, Tb)
            Next
        End If

        If CircleBtn.Checked = True Then
            For Each Circle As Circle In DxfDoc.Circles
                Dim Tb As Bounding = New Bounding(New Vector2(Circle.Center.X - Circle.Radius, Circle.Center.Y - Circle.Radius), New Vector2(Circle.Center.X + Circle.Radius, Circle.Center.Y + Circle.Radius), False)
                Boundary = Bounding.Union(Boundary, Tb)
            Next
        End If

        If EllipseBtn.Checked = True Then
            For Each Ellipse As Ellipse In DxfDoc.Ellipses
                Dim Tb As Bounding = New Bounding(New Vector2(Ellipse.Center.X - Ellipse.MajorAxis / 2, Ellipse.Center.Y - Ellipse.MinorAxis / 2), New Vector2(Ellipse.Center.X + Ellipse.MajorAxis / 2, Ellipse.Center.Y + Ellipse.MinorAxis / 2), False)
                Boundary = Bounding.Union(Boundary, Tb)
            Next
        End If

        If HatchBtn.Checked = True Then
            For Each Hatch As Hatch In DxfDoc.Hatches
                For Each HatchBoundaryPath As HatchBoundaryPath In Hatch.BoundaryPaths
                    If HatchBoundaryPath.PathType = (HatchBoundaryPathTypeFlags.External Or HatchBoundaryPathTypeFlags.Polyline Or HatchBoundaryPathTypeFlags.Derived) Or
                   HatchBoundaryPath.PathType = (HatchBoundaryPathTypeFlags.External Or HatchBoundaryPathTypeFlags.Polyline Or HatchBoundaryPathTypeFlags.Derived Or HatchBoundaryPathTypeFlags.Outermost) Then
                        For Each LwPolyline As HatchBoundaryPath.Polyline In HatchBoundaryPath.Edges
                            For i As Integer = 0 To LwPolyline.Vertexes.Count - 2
                                If LwPolyline.Vertexes(i).Z = 0 Then
                                    Dim Tb As Bounding = New Bounding(New Vector2(LwPolyline.Vertexes(i).X, LwPolyline.Vertexes(i).Y), New Vector2(LwPolyline.Vertexes(i + 1).X, LwPolyline.Vertexes(i + 1).Y))
                                    Boundary = Bounding.Union(Boundary, Tb)
                                Else
                                    Dim Tb As Bounding = New Bounding(New Vector2(LwPolyline.Vertexes(i).X, LwPolyline.Vertexes(i).Y), New Vector2(LwPolyline.Vertexes(i + 1).X, LwPolyline.Vertexes(i + 1).Y), LwPolyline.Vertexes(i).Z)
                                    Boundary = Bounding.Union(Boundary, Tb)
                                End If
                            Next
                        Next
                    End If
                Next
            Next
        End If

        If LineBtn.Checked = True Then
            For Each Line As Line In DxfDoc.Lines
                Dim Tb As Bounding = New Bounding(New Vector2(Line.StartPoint.X, Line.StartPoint.Y), New Vector2(Line.EndPoint.X, Line.EndPoint.Y))
                Boundary = Bounding.Union(Boundary, Tb)
            Next
        End If

        If SplineBtn.Checked = True Then
            For Each Spline As Spline In DxfDoc.Splines
                Dim LwPolyline As Polyline = Spline.ToPolyline(100 * Spline.ControlPoints.Count)
                For i As Integer = 0 To LwPolyline.Vertexes.Count - 2
                    Dim Spoint As PolylineVertex = LwPolyline.Vertexes(i)
                    Dim Epoint As PolylineVertex = LwPolyline.Vertexes(i + 1)
                    Dim Tb As Bounding = New Bounding(Spoint.Position, Epoint.Position)
                    Boundary = Bounding.Union(Boundary, Tb)
                Next
            Next
        End If

        Return Boundary
    End Function

    Private Sub DrawBitmap()
        Dim PcsMm As Integer = CInt(PcsMmTxt.Text)
        Dim MmPcs As Double = 1 / PcsMm

        Dim pen As New Pen(Color.Black)
        Dim BlackBrush As New SolidBrush(Color.Black)
        Dim WhiteBrush As New SolidBrush(Color.White)
        Dim MinX As Double = Boundary_.Min.X - LeftPcs.Value * MmPcs
        Dim MinY As Double = Boundary_.Min.Y - DownPcs.Value * MmPcs
        Dim Draw As Bitmap
        Try
            Draw = New Bitmap(WidthPcs_ + 1, HeightPcs_ + 1)
        Catch ex As Exception
            MessageBox.Show("������� ������� ������ �������� " & Chr(10) & Chr(13) & "��������� �������� � .dxf", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        Dim gr_Draw As Graphics = Graphics.FromImage(Draw)
        gr_Draw.Clear(Color.White)
        gr_Draw.SmoothingMode = Drawing2D.SmoothingMode.None

#Region "Text"
        If TextBtn.Checked = True Then
            For Each Text As Text In DxfDoc.Texts
                Dim gp As New GraphicsPath()
                'Microsoft sans serif
                Dim drawFont As New Font("GOST 2.304 type A", CSng(Text.Height * PcsMm), Drawing.FontStyle.Regular)
                Dim sz As SizeF = gr_Draw.MeasureString(Text.Value, drawFont)
                Dim Len As Integer
                If sz.Width > sz.Height Then
                    Len = CInt(Ceiling(sz.Width))
                Else
                    Len = CInt(Ceiling(sz.Height))
                End If
                Dim Dx As Double = Len / 2 - (Len / 2 - sz.Width / 2) * Cos((-Text.Rotation) / 180 * PI)
                Dim Dy As Double = Len / 2 - (Len / 2 - sz.Height / 2) * Sin((-Text.Rotation) / 180 * PI)
                Dim TDraw As Bitmap = New Bitmap(Len, Len)
                Dim Tgr_Draw As Graphics = Graphics.FromImage(TDraw)
                Tgr_Draw.InterpolationMode = InterpolationMode.HighQualityBilinear
                Tgr_Draw.SmoothingMode = SmoothingMode.None
                Tgr_Draw.TextRenderingHint = Drawing.Text.TextRenderingHint.SingleBitPerPixel
                Dim X As Double = (Text.Position.X - MinX) * PcsMm - Dx
                Dim Y As Double = HeightPcs_ - CInt((Text.Position.Y - MinY) * PcsMm) - Dy
                Dim DrawPoint As New Drawing.PointF(X, Y)
                Dim M As New Drawing2D.Matrix
                M.RotateAt(-Text.Rotation, New Drawing.Point(TDraw.Width / 2, TDraw.Height / 2), MatrixOrder.Append)
                Tgr_Draw.Transform = M
                Tgr_Draw.DrawString(Text.Value, drawFont, BlackBrush, (TDraw.Width / 2) - (sz.Width / 2), (TDraw.Height / 2) - (sz.Height / 2))
                gr_Draw.DrawImage(TDraw, DrawPoint)
            Next
        End If
#End Region

#Region "Hatch"
        If HatchBtn.Checked = True Then
            For Each Hatch As Hatch In DxfDoc.Hatches
                Dim gp As New GraphicsPath()
                For Each HatchBoundaryPath As HatchBoundaryPath In Hatch.BoundaryPaths
                    If HatchBoundaryPath.PathType = (HatchBoundaryPathTypeFlags.External Or HatchBoundaryPathTypeFlags.Polyline Or HatchBoundaryPathTypeFlags.Derived) Or
                       HatchBoundaryPath.PathType = (HatchBoundaryPathTypeFlags.External Or HatchBoundaryPathTypeFlags.Polyline Or HatchBoundaryPathTypeFlags.Derived Or HatchBoundaryPathTypeFlags.Outermost) Then
                        For Each LwPolyline As HatchBoundaryPath.Polyline In HatchBoundaryPath.Edges
                            For i As Integer = 0 To LwPolyline.Vertexes.Count - 2
                                Dim Spoint As LwPolylineVertex = New LwPolylineVertex(New Vector2(LwPolyline.Vertexes(i).X, LwPolyline.Vertexes(i).Y), LwPolyline.Vertexes(i).Z)
                                Dim Epoint As LwPolylineVertex = New LwPolylineVertex(New Vector2(LwPolyline.Vertexes(i + 1).X, LwPolyline.Vertexes(i + 1).Y), LwPolyline.Vertexes(i + 1).Z)
                                If Spoint.Bulge = 0 Then
                                    Dim X1 As Integer = CInt((Spoint.Position.X - MinX) * PcsMm)
                                    Dim Y1 As Integer = HeightPcs_ - CInt((Spoint.Position.Y - MinY) * PcsMm)
                                    Dim X2 As Integer = CInt((Epoint.Position.X - MinX) * PcsMm)
                                    Dim Y2 As Integer = HeightPcs_ - CInt((Epoint.Position.Y - MinY) * PcsMm)
                                    gp.AddLine(X1, Y1, X2, Y2)
                                ElseIf Spoint.Bulge <> 0 Then
                                    Dim Len As Double
                                    Dim InclAng As Double
                                    Dim Rad As Double
                                    Dim Ang As Double
                                    Dim Center As Vector2
                                    Dim Base As Double
                                    Dim X1 As Single
                                    Dim Y1 As Single
                                    Dim D As Single

                                    InclAng = Atan(Abs(Spoint.Bulge)) * 4
                                    Len = LineLenght(Spoint.Position, Epoint.Position)
                                    Ang = (InclAng / 2) - ((Atan(1) * 4) / 2)
                                    Rad = (Len / 2) / (Cos(Ang))
                                    D = CSng(2 * Rad * PcsMm)
                                    Base = angle2Point(Spoint.Position, Epoint.Position)
                                    If Spoint.Bulge > 0 Then
                                        Center = PolarPoint(Spoint.Position, Base - Ang, Rad)
                                        Base = 360 - 180 * angle2Point(Center, Epoint.Position) / PI
                                        Ang = InclAng / PI * 180
                                        Base = Base + Ang
                                        Ang = -Ang
                                    Else
                                        Center = PolarPoint(Spoint.Position, Base + Ang, Rad)
                                        Base = 360 - 180 * angle2Point(Center, Spoint.Position) / PI
                                        Ang = InclAng / PI * 180
                                    End If
                                    X1 = CSng((Center(0) - MinX - Rad) * PcsMm)
                                    Y1 = HeightPcs_ - CSng((Center(1) - MinY + Rad) * PcsMm)
                                    gp.AddArc(X1, Y1, D, D, CSng(Base), CSng(Ang))
                                End If

                                If i = LwPolyline.Vertexes.Count - 2 And LwPolyline.IsClosed = True Then
                                    Spoint = New LwPolylineVertex(New Vector2(LwPolyline.Vertexes(i + 1).X, LwPolyline.Vertexes(i + 1).Y), LwPolyline.Vertexes(i + 1).Z)
                                    Epoint = New LwPolylineVertex(New Vector2(LwPolyline.Vertexes(0).X, LwPolyline.Vertexes(0).Y), LwPolyline.Vertexes(0).Z)
                                    If Spoint.Bulge = 0 Then
                                        Dim X1 As Integer = CInt((Spoint.Position.X - MinX) * PcsMm)
                                        Dim Y1 As Integer = HeightPcs_ - CInt((Spoint.Position.Y - MinY) * PcsMm)
                                        Dim X2 As Integer = CInt((Epoint.Position.X - MinX) * PcsMm)
                                        Dim Y2 As Integer = HeightPcs_ - CInt((Epoint.Position.Y - MinY) * PcsMm)
                                        gp.AddLine(X1, Y1, X2, Y2)
                                    ElseIf Spoint.Bulge <> 0 Then
                                        Dim Len As Double
                                        Dim InclAng As Double
                                        Dim Rad As Double
                                        Dim Ang As Double
                                        Dim Center As Vector2
                                        Dim Base As Double
                                        Dim X1 As Single
                                        Dim Y1 As Single
                                        Dim D As Single
                                        InclAng = Atan(Abs(Spoint.Bulge)) * 4
                                        Len = LineLenght(Spoint.Position, Epoint.Position)
                                        Ang = (InclAng / 2) - ((Atan(1) * 4) / 2)
                                        Rad = (Len / 2) / (Cos(Ang))
                                        D = CSng(2 * Rad * PcsMm)
                                        Base = angle2Point(Spoint.Position, Epoint.Position)
                                        If Spoint.Bulge > 0 Then
                                            Center = PolarPoint(Spoint.Position, Base - Ang, Rad)
                                            Base = 360 - 180 * angle2Point(Center, Epoint.Position) / PI
                                            Ang = InclAng / PI * 180
                                            Base = Base + Ang
                                            Ang = -Ang
                                        Else
                                            Center = PolarPoint(Spoint.Position, Base + Ang, Rad)
                                            Base = 360 - 180 * angle2Point(Center, Spoint.Position) / PI
                                            Ang = InclAng / PI * 180
                                        End If
                                        X1 = CSng((Center(0) - MinX - Rad) * PcsMm)
                                        Y1 = HeightPcs_ - CSng((Center(1) - MinY + Rad) * PcsMm)
                                        gp.AddArc(X1, Y1, D, D, CSng(Base), CSng(Ang))
                                    End If
                                End If
                            Next
                            gp.CloseFigure()
                        Next
                    ElseIf HatchBoundaryPath.PathType = (HatchBoundaryPathTypeFlags.External Or HatchBoundaryPathTypeFlags.Derived) Or
                            HatchBoundaryPath.PathType = (HatchBoundaryPathTypeFlags.External Or HatchBoundaryPathTypeFlags.Derived Or HatchBoundaryPathTypeFlags.Outermost) Then
                        For Each Entity As HatchBoundaryPath.Edge In HatchBoundaryPath.Edges
                            If Entity.Type = HatchBoundaryPath.EdgeType.Spline Then
                                Dim Spline As Spline = Entity.ConvertTo()
                                Dim LwPolyline As Polyline = Spline.ToPolyline(100 * Spline.ControlPoints.Count)
                                For i As Integer = 0 To LwPolyline.Vertexes.Count - 2
                                    Dim Spoint As PolylineVertex = LwPolyline.Vertexes(i)
                                    Dim Epoint As PolylineVertex = LwPolyline.Vertexes(i + 1)
                                    Dim X1 As Integer = CInt((Spoint.Position.X - MinX) * PcsMm)
                                    Dim Y1 As Integer = HeightPcs_ - CInt((Spoint.Position.Y - MinY) * PcsMm)
                                    Dim X2 As Integer = CInt((Epoint.Position.X - MinX) * PcsMm)
                                    Dim Y2 As Integer = HeightPcs_ - CInt((Epoint.Position.Y - MinY) * PcsMm)
                                    gp.AddLine(X1, Y1, X2, Y2)
                                Next
                            End If
                            gr_Draw.DrawPath(pen, gp)
                        Next
                        gp.CloseFigure()
                    End If
                Next
                gr_Draw.FillPath(BlackBrush, gp)
            Next
        End If
#End Region

#Region "Circle"
        If CircleBtn.Checked = True Then
            For Each Circle As Circle In DxfDoc.Circles
                Dim Diam As Integer = CInt(2 * Circle.Radius * PcsMm)
                Dim X1 As Integer = CInt((Circle.Center.X - Circle.Radius - MinX) * PcsMm)
                Dim Y1 As Integer = HeightPcs_ - CInt((Circle.Center.Y + Circle.Radius - MinY) * PcsMm)
                gr_Draw.DrawEllipse(pen, X1, Y1, Diam, Diam)
            Next
        End If
#End Region

#Region "Ellipse"
        If EllipseBtn.Checked = True Then
            For Each Ellipse As Ellipse In DxfDoc.Ellipses
                Dim MajorDiam As Integer = CInt(Ellipse.MajorAxis * PcsMm) 'x
                Dim MinorDiam As Integer = CInt(Ellipse.MinorAxis * PcsMm) 'y
                Dim X1 As Integer = CInt((Ellipse.Center.X - Ellipse.MajorAxis / 2 - MinX) * PcsMm)
                Dim Y1 As Integer = HeightPcs_ - CInt((Ellipse.Center.Y + Ellipse.MinorAxis / 2 - MinY) * PcsMm)
                Dim Path As New GraphicsPath
                If Ellipse.IsFullEllipse = True Then
                    Path.AddEllipse(X1, Y1, MajorDiam, MinorDiam)
                Else
                    Dim SweepAngle As Double = 360 - Ellipse.StartAngle + Ellipse.EndAngle
                    Path.AddArc(X1, Y1, MajorDiam, MinorDiam, CSng(180 + Ellipse.StartAngle), CSng(SweepAngle))
                End If

                Dim RotateMatrix As New Matrix
                If Ellipse.Normal.Z = -1 Then
                    RotateMatrix.RotateAt(CSng(180 - Ellipse.Rotation), New PointF(CInt((Ellipse.Center.X - MinX) * PcsMm), HeightPcs_ - CInt((Ellipse.Center.Y - MinY) * PcsMm)))
                ElseIf Ellipse.Normal.Z = 1 Then
                    RotateMatrix.RotateAt(CSng(180 - Ellipse.Rotation), New PointF(CInt((Ellipse.Center.X - MinX) * PcsMm), HeightPcs_ - CInt((Ellipse.Center.Y - MinY) * PcsMm)))

                End If

                Path.Transform(RotateMatrix)
                gr_Draw.DrawPath(pen, Path)
            Next
        End If
#End Region

#Region "LwPolyline"
        If LwPolylineBtn.Checked = True Then
            For Each LwPolyline As LwPolyline In DxfDoc.LwPolylines
                Dim gp As New GraphicsPath()
                For i As Integer = 0 To LwPolyline.Vertexes.Count - 2
                    Dim Spoint As LwPolylineVertex = LwPolyline.Vertexes(i)
                    Dim Epoint As LwPolylineVertex = LwPolyline.Vertexes(i + 1)
                    If Spoint.Bulge = 0 Then
                        Dim X1 As Integer = CInt((Spoint.Position.X - MinX) * PcsMm)
                        Dim Y1 As Integer = HeightPcs_ - CInt((Spoint.Position.Y - MinY) * PcsMm)
                        Dim X2 As Integer = CInt((Epoint.Position.X - MinX) * PcsMm)
                        Dim Y2 As Integer = HeightPcs_ - CInt((Epoint.Position.Y - MinY) * PcsMm)
                        gp.AddLine(X1, Y1, X2, Y2)
                    ElseIf Spoint.Bulge <> 0 Then
                        Dim Len As Double
                        Dim InclAng As Double
                        Dim Rad As Double
                        Dim Ang As Double
                        Dim Center As Vector2
                        Dim Base As Double
                        Dim X1 As Single
                        Dim Y1 As Single
                        Dim D As Single

                        InclAng = Atan(Abs(Spoint.Bulge)) * 4
                        Len = LineLenght(Spoint.Position, Epoint.Position)
                        Ang = (InclAng / 2) - ((Atan(1) * 4) / 2)
                        Rad = (Len / 2) / (Cos(Ang))
                        D = CSng(2 * Rad * PcsMm)
                        If D < 1 Then
                            Continue For
                        End If
                        Base = angle2Point(Spoint.Position, Epoint.Position)
                        If Spoint.Bulge > 0 Then
                            Center = PolarPoint(Spoint.Position, Base - Ang, Rad)
                            Base = 360 - 180 * angle2Point(Center, Epoint.Position) / PI
                            Ang = InclAng / PI * 180
                            Base = Base + Ang
                            Ang = -Ang
                        Else
                            Center = PolarPoint(Spoint.Position, Base + Ang, Rad)
                            Base = 360 - 180 * angle2Point(Center, Spoint.Position) / PI
                            Ang = InclAng / PI * 180
                        End If
                        X1 = CSng((Center(0) - MinX - Rad) * PcsMm)
                        Y1 = HeightPcs_ - CSng((Center(1) - MinY + Rad) * PcsMm)
                        gp.AddArc(X1, Y1, D, D, CSng(Base), CSng(Ang))
                    End If

                    If i = LwPolyline.Vertexes.Count - 2 And LwPolyline.IsClosed = True Then
                        Spoint = LwPolyline.Vertexes(i + 1)
                        Epoint = LwPolyline.Vertexes(0)
                        If Spoint.Bulge = 0 Then
                            Dim X1 As Integer = CInt((Spoint.Position.X - MinX) * PcsMm)
                            Dim Y1 As Integer = HeightPcs_ - CInt((Spoint.Position.Y - MinY) * PcsMm)
                            Dim X2 As Integer = CInt((Epoint.Position.X - MinX) * PcsMm)
                            Dim Y2 As Integer = HeightPcs_ - CInt((Epoint.Position.Y - MinY) * PcsMm)
                            gp.AddLine(X1, Y1, X2, Y2)

                        ElseIf Spoint.Bulge <> 0 Then
                            Dim Len As Double
                            Dim InclAng As Double
                            Dim Rad As Double
                            Dim Ang As Double
                            Dim Center As Vector2
                            Dim Base As Double
                            Dim X1 As Single
                            Dim Y1 As Single
                            Dim D As Single

                            InclAng = Atan(Abs(Spoint.Bulge)) * 4
                            Len = LineLenght(Spoint.Position, Epoint.Position)
                            Ang = (InclAng / 2) - ((Atan(1) * 4) / 2)
                            Rad = (Len / 2) / (Cos(Ang))
                            D = CSng(2 * Rad * PcsMm)
                            If D < 1 Then
                                Continue For
                            End If
                            Base = angle2Point(Spoint.Position, Epoint.Position)
                            If Spoint.Bulge > 0 Then
                                Center = PolarPoint(Spoint.Position, Base - Ang, Rad)
                                Base = 360 - 180 * angle2Point(Center, Epoint.Position) / PI
                                Ang = InclAng / PI * 180
                                Base = Base + Ang
                                Ang = -Ang
                            Else
                                Center = PolarPoint(Spoint.Position, Base + Ang, Rad)
                                Base = 360 - 180 * angle2Point(Center, Spoint.Position) / PI
                                Ang = InclAng / PI * 180
                            End If
                            X1 = CSng((Center(0) - MinX - Rad) * PcsMm)
                            Y1 = HeightPcs_ - CSng((Center(1) - MinY + Rad) * PcsMm)
                            gp.AddArc(X1, Y1, D, D, CSng(Base), CSng(Ang))
                        End If
                    End If
                Next
                gr_Draw.DrawPath(pen, gp)
            Next
        End If
#End Region

#Region "Arc"
        If ArcBtn.Checked = True Then
            Dim gp As New GraphicsPath()
            For Each Arc As Arc In DxfDoc.Arcs
                Dim Radius As Double = Arc.Radius
                Dim D As Single = CSng(2 * Radius * PcsMm)
                If D < 1 Then
                    Continue For
                End If

                Dim StartAngle As Double = Arc.StartAngle
                Dim EndAngle As Double = Arc.EndAngle
                Dim Angle As Double
                If EndAngle > StartAngle Then
                    Angle = EndAngle - StartAngle
                Else
                    Angle = -1 * (360 + (EndAngle - StartAngle))
                End If

                Dim Bulge As Double = Tan((Angle / 180 * PI) / 4)
                Dim StartPoint As Vector2 = New Vector2(Arc.Center.X + Radius * Cos(StartAngle / 180 * PI), Arc.Center.Y + Radius * Sin(StartAngle / 180 * PI))
                Dim EndPoint As Vector2 = New Vector2(Arc.Center.X + Radius * Cos(EndAngle / 180 * PI), Arc.Center.Y + Radius * Sin(EndAngle / 180 * PI))

                Dim X1 As Single = CSng((Arc.Center.X - MinX - Radius) * PcsMm)
                Dim Y1 As Single = HeightPcs_ - CSng((Arc.Center.Y - MinY + Radius) * PcsMm)
                Dim Base As Double
                If Bulge > 0 Then
                    Base = 360 - 180 * angle2Point(New Vector2(Arc.Center.X, Arc.Center.Y), EndPoint) / PI
                Else
                    Base = 360 - 180 * angle2Point(New Vector2(Arc.Center.X, Arc.Center.Y), StartPoint) / PI
                End If
                gr_Draw.DrawArc(pen, X1, Y1, D, D, CSng(Base), CSng(Angle))
            Next
        End If
#End Region

#Region "Line"
        If LineBtn.Checked = True Then
            For Each Line As Line In DxfDoc.Lines
                Dim X1 As Integer = CInt((Line.StartPoint.X - MinX) * PcsMm)
                Dim Y1 As Integer = HeightPcs_ - CInt((Line.StartPoint.Y - MinY) * PcsMm)
                Dim X2 As Integer = CInt((Line.EndPoint.X - MinX) * PcsMm)
                Dim Y2 As Integer = HeightPcs_ - CInt((Line.EndPoint.Y - MinY) * PcsMm)
                gr_Draw.DrawLine(pen, X1, Y1, X2, Y2)
            Next
        End If
#End Region

#Region "Spline"
        If SplineBtn.Checked = True Then
            For Each Spline As Spline In DxfDoc.Splines
                Dim LwPolyline As Polyline = Spline.ToPolyline(100 * Spline.ControlPoints.Count)
                Dim gp As New GraphicsPath()
                For i As Integer = 0 To LwPolyline.Vertexes.Count - 2
                    Dim Spoint As PolylineVertex = LwPolyline.Vertexes(i)
                    Dim Epoint As PolylineVertex = LwPolyline.Vertexes(i + 1)

                    Dim X1 As Integer = CInt((Spoint.Position.X - MinX) * PcsMm)
                    Dim Y1 As Integer = HeightPcs_ - CInt((Spoint.Position.Y - MinY) * PcsMm)
                    Dim X2 As Integer = CInt((Epoint.Position.X - MinX) * PcsMm)
                    Dim Y2 As Integer = HeightPcs_ - CInt((Epoint.Position.Y - MinY) * PcsMm)
                    gp.AddLine(X1, Y1, X2, Y2)
                Next
                gr_Draw.DrawPath(pen, gp)
            Next
        End If
#End Region

        ImageWorkArea1.Image = Draw
    End Sub

    Public Function PolyRight(ByVal Vertexes As Vector3()) As Boolean
        '������� ��������� ����������� ������ �����
        PolyRight = False
        Dim i As Integer
        Dim rez As Double = 0
        rez = rez + Vertexes(0).X * (Vertexes(1).Y - Vertexes(Vertexes.Count - 1).Y)
        For i = 1 To Vertexes.Count - 2 Step 1
            rez = rez + Vertexes(i).X * (Vertexes(i + 1).Y - Vertexes(i - 1).Y)
        Next i
        rez = rez + Vertexes(i).X * (Vertexes(0).Y - Vertexes(i - 1).Y)
        If rez < 0 Then
            PolyRight = True
        End If
    End Function

    Public Function PolyRight(ByVal Vertexes As List(Of LwPolylineVertex)) As Boolean
        '������� ��������� ����������� ������ �����
        PolyRight = False
        Dim i As Integer
        Dim rez As Double = 0

        rez = rez + Vertexes(0).Position.X * (Vertexes(1).Position.Y - Vertexes(Vertexes.Count - 1).Position.Y)
        For i = 1 To Vertexes.Count - 2 Step 1
            rez = rez + Vertexes(i).Position.X * (Vertexes(i + 1).Position.Y - Vertexes(i - 1).Position.Y)
        Next i
        rez = rez + Vertexes(i).Position.X * (Vertexes(0).Position.Y - Vertexes(i - 1).Position.Y)
        If rez < 0 Then
            PolyRight = True
        End If
    End Function

    <System.Diagnostics.DebuggerStepThrough()> _
    Public Shared Function LineLenght(ByVal point1 As Vector2, ByVal point2 As Vector2) As Double
        '������� ���������� ������ ����� �������
        LineLenght = Sqrt((point2.X - point1.X) ^ 2 + (point2.Y - point1.Y) ^ 2)
    End Function
    <System.Diagnostics.DebuggerStepThrough()> _
    Public Shared Function angle2Point(ByVal StartPoint As Vector2, ByVal Endpoint As Vector2) As Double
        '�������  ���������� ���� ������� �����
        Dim dY As Double = Endpoint.Y - StartPoint.Y
        Dim dX As Double = Endpoint.X - StartPoint.X
        angle2Point = Atan2(dY, dX)
    End Function

    <System.Diagnostics.DebuggerStepThrough()> _
    Public Shared Function PolarPoint(ByVal pPt As Vector2, ByVal dAng As Double, ByVal dDist As Double)
        Return New Vector2(pPt.X + dDist * Math.Cos(dAng), pPt.Y + dDist * Math.Sin(dAng))
    End Function

    ' <System.Diagnostics.DebuggerStepThrough()> _
    Public Shared Function PointBelongCircle(ByVal StartPoint As Vector2, ByVal EndPoint As Vector2, ByVal bulg As Double, ByVal CurentPoint As Vector2) As Boolean
        ' ������� ��������� ����������� �� ������ ����
        Dim j As Single
        PointBelongCircle = False
        j = Round((CurentPoint.Y - StartPoint.Y) * (EndPoint.X - StartPoint.X) - (CurentPoint.X - StartPoint.X) * (EndPoint.Y - StartPoint.Y), 6)
        If bulg > 0 And j <= 0 Then
            PointBelongCircle = True
        ElseIf bulg < 0 And j >= 0 Then
            PointBelongCircle = True
        End If
    End Function
    Private Sub RedrawBtn_Click(sender As Object, e As EventArgs) Handles RedrawBtn.Click
        ImageWorkArea1.Image = Nothing
        Boundary_ = GetBoundary()
        If Boundary_ Is Nothing Then
            Exit Sub
        End If
        CalculateSize()
        DrawBitmap()
    End Sub

    Private Sub OpenDxfBtn_Click(sender As Object, e As EventArgs) Handles OpenDxfBtn.Click
        Dim dialog As New OpenFileDialog
        dialog.Filter = "dxf|*.dxf"
        'dialog.InitialDirectory = MyProject.Computer.FileSystem.SpecialDirectories.MyPictures
        If (dialog.ShowDialog = DialogResult.OK) Then
            DxfDoc = LoadDxf(dialog.FileName)
            RedrawBtn_Click(Nothing, Nothing)
        End If
    End Sub

    Private Sub SaveImageBtn_Click(sender As Object, e As EventArgs) Handles SaveImageBtn.Click
        Dim dialog As New SaveFileDialog
        dialog.Filter = "bmp|*.bmp"
        If (dialog.ShowDialog = DialogResult.OK) Then
            ImageWorkArea1.Image.Save(dialog.FileName, System.Drawing.Imaging.ImageFormat.Bmp)
        End If
    End Sub

    Private Sub cbSizeToFit_CheckedChanged(sender As Object, e As EventArgs) Handles cbSizeToFit.CheckedChanged
        Me.tbScaler.Enabled = Not Me.cbSizeToFit.Checked
        ImageWorkArea1.SizeToFit = Me.cbSizeToFit.Checked
        If Not Me.cbSizeToFit.Checked Then
            ImageWorkArea1.ScaleFactor = Me.tbScaler.Value / 10
        End If
        ImageWorkArea1.UpdateMe()
    End Sub

    Private Sub tbScaler_Scroll(sender As Object, e As EventArgs) Handles tbScaler.Scroll
        ImageWorkArea1.ScaleFactor = Me.tbScaler.Value / 10
        ImageWorkArea1.UpdateMe()
    End Sub

    Private Sub ImageWorkArea1_ScaleFactorChange(Value As Single) Handles ImageWorkArea1.ScaleFactorChange
        Me.tbScaler.Value = Value
    End Sub

    Private Sub ImageWorkArea1_DragEnter(sender As Object, e As DragEventArgs) Handles ImageWorkArea1.DragEnter
        Dim Ext As String = LCase(Path.GetExtension(e.Data.GetData(DataFormats.FileDrop)(0)))
        If Ext = ".dxf" Then
            e.Effect = DragDropEffects.Move
        End If
    End Sub

    Private Sub ImageWorkArea1_DragDrop(sender As Object, e As DragEventArgs) Handles ImageWorkArea1.DragDrop
        DxfDoc = LoadDxf(e.Data.GetData(DataFormats.FileDrop)(0))
        RedrawBtn_Click(Nothing, Nothing)
    End Sub
End Class
