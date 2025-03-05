using System.Windows.Media;
class MatrixChaos
{
    public MatrixChaos(Rectangle rectangle, Matrix[] matrices)
    {
        Rectangle = rectangle;
        Matrices = matrices;
        Rng = new Random();
    }

    public Rectangle Rectangle { get; }
    public Matrix[] Matrices { get; }
    Random Rng { get; }
    public Point GetPoint(int numberOfIterations)
    {
        var point = Rectangle.GetMidpoint();;
        var matrices = Matrices;
        for (int i = 0; i < numberOfIterations; i++)
        {
            var r = Matrices[Rng.Next(Matrices.Length)];            
            point -= new System.Windows.Vector(r.OffsetX, r.OffsetY) * r;
            r.OffsetX = 0;
            r.OffsetY = 0;
            point *= r;

        }
        return point;
    }
}