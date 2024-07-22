using System.Windows;
using System.Windows.Media;

public struct Point3
{
    public Point3(int x, int y, int z)
    {
        X = x;
        Y = y;
        Z = z;
    }
    public Int32Point Projection(Point3 observer, double displayDistance)
    {
        var x = observer.X;
        var y = observer.Y;
        var z = observer.Z;
        var xd = X - x;
        var yd = Y - y;
        var zd = Z - z;
        var projectionCoefficent = displayDistance / zd;
        return new((int)(xd * projectionCoefficent), (int)(yd * projectionCoefficent));
    }
    public int X { get; }
    public int Y { get; }
    public int Z { get; }
    public static Point3 operator -(Point3 left, Point3 right) => new(left.X - right.X, left.Y - right.Y, left.Z - right.Z);
}
struct Triangle3
{
    public Triangle3(Point3 a, Point3 b, Point3 c, Color color)
    {
        A = a;
        B = b;
        C = c;
        Color = color;
    }
    public TriangleProjection GetProjection(Point3 observer, double displayDistance)
    {
        return new(A.Projection(observer, displayDistance), B.Projection(observer, displayDistance), C.Projection(observer, displayDistance));
    }
    public Point3 A { get; set; }
    public Point3 B { get; set; }
    public Point3 C { get; set; }
    public Color Color { get; }
}
struct TriangleProjection
{
    public TriangleProjection(Int32Point a, Int32Point b, Int32Point c)
    {
        A = a;
        B = b;
        C = c;
    }
    public Triangle WithColors(byte r, byte g, byte b)
    {
        return new(A, B, C, r, g, b);
    }
    public Int32Point A { get; set; }
    public Int32Point B { get; set; }
    public Int32Point C { get; set; }
}
struct Camera
{
    public Camera(Point3 position, Point3 direct)
    {
        Position = position;
        Direct = direct;
    }

    public Point3 Position { get; }
    public Point3 Direct { get; }
}
class Matrix4x3
{
    public Matrix4x3(double m00, double m01, double m02, double m10, double m11, double m12, double m20, double m21, double m22, double m30, double m31, double m32)
    {
        M00 = m00;
        M01 = m01;
        M02 = m02;
        M10 = m10;
        M11 = m11;
        M12 = m12;
        M20 = m20;
        M21 = m21;
        M22 = m22;
        M30 = m30;
        M31 = m31;
        M32 = m32;
    }

    public double M00 { get; }
    public double M01 { get; }
    public double M02 { get; }
    public double M10 { get; }
    public double M11 { get; }
    public double M12 { get; }
    public double M20 { get; }
    public double M21 { get; }
    public double M22 { get; }
    public double M30 { get; }
    public double M31 { get; }
    public double M32 { get; }
    
}