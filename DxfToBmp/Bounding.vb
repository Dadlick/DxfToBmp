Imports netDxf
Imports System.Math
Public Class Bounding
    Private Max_ As Vector2
    Private Min_ As Vector2

    Public Shared Function Union(Bt1 As Bounding, V As Vector2) As Bounding
        If (Bt1 Is Nothing) Then
            Return Nothing
        End If
        Dim Tmin As Vector2
        Dim Tmax As Vector2

        If Bt1.Min.X <= V.X Then
            Tmin.X = Bt1.Min.X
        Else
            Tmin.X = V.X
        End If

        If Bt1.Min.Y <= V.Y Then
            Tmin.Y = Bt1.Min.Y
        Else
            Tmin.Y = V.Y
        End If

        If Bt1.Max.X >= V.X Then
            Tmax.X = Bt1.Max.X
        Else
            Tmax.X = V.X
        End If

        If Bt1.Max.Y >= V.Y Then
            Tmax.Y = Bt1.Max.Y
        Else
            Tmax.Y = V.Y
        End If
        Return New Bounding(Tmin, Tmax, False)

    End Function
    Public Shared Function Union(Bt1 As Bounding, Bt2 As Bounding) As Bounding
        If ((Bt1 Is Nothing) AndAlso (Bt2 Is Nothing)) Then
            Return Nothing
        End If
        If (Bt1 Is Nothing) Then
            Return Bt2
        End If
        If (Bt2 Is Nothing) Then
            Return Bt1
        End If

        Dim Tmin As Vector2
        Dim Tmax As Vector2

        If Bt1.Min.X <= Bt2.Min.X Then
            Tmin.X = Bt1.Min.X
        Else
            Tmin.X = Bt2.Min.X
        End If

        If Bt1.Min.Y <= Bt2.Min.Y Then
            Tmin.Y = Bt1.Min.Y
        Else
            Tmin.Y = Bt2.Min.Y
        End If

        If Bt1.Max.X >= Bt2.Max.X Then
            Tmax.X = Bt1.Max.X
        Else
            Tmax.X = Bt2.Max.X
        End If

        If Bt1.Max.Y >= Bt2.Max.Y Then
            Tmax.Y = Bt1.Max.Y
        Else
            Tmax.Y = Bt2.Max.Y
        End If
        Return New Bounding(Tmin, Tmax, False)
    End Function

    Sub New(Spoint As Vector2, Epoint As Vector2, Bulge As Double)
        Dim Len As Double
        Dim InclAng As Double
        Dim Rad As Double
        Dim Ang As Double
        Dim Center As Vector2
        Dim Base As Double

        InclAng = Atan(Abs(Bulge)) * 4
        Len = DxfToBmpFrm.LineLenght(Spoint, Epoint)
        Ang = (InclAng / 2) - ((Atan(1) * 4) / 2)
        Rad = (Len / 2) / (Cos(Ang))

        Base = DxfToBmpFrm.angle2Point(Spoint, Epoint)
        If Bulge > 0 Then
            Center = DxfToBmpFrm.PolarPoint(Spoint, Base - Ang, Rad)
        Else
            Center = DxfToBmpFrm.PolarPoint(Spoint, Base + Ang, Rad)
        End If

        Dim LeftPoint As Vector2 = New Vector2(Center.X - Rad, Center.Y)
        Dim RightPoint As Vector2 = New Vector2(Center.X + Rad, Center.Y)
        Dim UpPoint As Vector2 = New Vector2(Center.X, Center.Y + Rad)
        Dim DownPoint As Vector2 = New Vector2(Center.X, Center.Y - Rad)

        Dim Tb As Bounding = New Bounding(Spoint, Epoint)

        If DxfToBmpFrm.PointBelongCircle(Spoint, Epoint, Bulge, LeftPoint) Then
            Tb = Union(Tb, LeftPoint)
        End If

        If DxfToBmpFrm.PointBelongCircle(Spoint, Epoint, Bulge, RightPoint) Then
            Tb = Union(Tb, RightPoint)
        End If

        If DxfToBmpFrm.PointBelongCircle(Spoint, Epoint, Bulge, UpPoint) Then
            Tb = Union(Tb, UpPoint)
        End If

        If DxfToBmpFrm.PointBelongCircle(Spoint, Epoint, Bulge, DownPoint) Then
            Tb = Union(Tb, DownPoint)
        End If
        Me.Min = Tb.Min
        Me.Max = Tb.Max
    End Sub
    Sub New(V1 As Vector2, V2 As Vector2, Optional Sort As Boolean = True)
        If Sort Then
            If V1.X <= V2.X Then
                Me.Min_.X = V1.X
                Me.Max_.X = V2.X
            Else
                Me.Min_.X = V2.X
                Me.Max_.X = V1.X
            End If
            If V1.Y <= V2.Y Then
                Me.Min_.Y = V1.Y
                Me.Max_.Y = V2.Y
            Else
                Me.Min_.Y = V2.Y
                Me.Max_.Y = V1.Y
            End If
        Else
            Me.Min_ = V1
            Me.Max_ = V2
        End If
    End Sub

    Sub New(V1 As Vector3, V2 As Vector3, Optional Sort As Boolean = True)
        If Sort Then
            If V1.X <= V2.X Then
                Me.Min_.X = V1.X
                Me.Max_.X = V2.X
            Else
                Me.Min_.X = V2.X
                Me.Max_.X = V1.X
            End If
            If V1.Y <= V2.Y Then
                Me.Min_.Y = V1.Y
                Me.Max_.Y = V2.Y
            Else
                Me.Min_.Y = V2.Y
                Me.Max_.Y = V1.Y
            End If
        Else
            Me.Min_ = New Vector2(V1.X, V1.Y)
            Me.Max_ = New Vector2(V2.X, V2.Y)
        End If
    End Sub

    Public Property Max As Vector2
        Get
            Return Me.Max_
        End Get
        Set(ByVal value As Vector2)
            Me.Max_ = value
        End Set
    End Property

    Public Property Min As Vector2
        Get
            Return Me.Min_
        End Get
        Set(ByVal value As Vector2)
            Me.Min_ = value
        End Set
    End Property

End Class
