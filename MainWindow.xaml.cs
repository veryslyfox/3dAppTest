namespace _3dAppTest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

public partial class MainWindow : Window
{
    private readonly DispatcherTimer _timer = new();
    private readonly WriteableBitmap _bitmap;
    private readonly Random _rng = new();
    private readonly Triangle3 _t = new(new(100, 100, 100), new(100, 200, 100), new(200, 200, 100), new());
    private int _f;
    private Graphic _graphic;
    private bool[,] _map;
    public MainWindow()
    {

        InitializeComponent();
        _timer.Interval = TimeSpan.FromSeconds(0.000001);
        _bitmap = new WriteableBitmap(1000, 1000, 96, 96, PixelFormats.Bgr32, null);
        _graphic = new(_bitmap, _bitmap.BackBuffer);
        image.Source = _bitmap;
        _timer.Tick += Tick;
        _timer.Start();
    }
    public void Tick(object? sender, EventArgs args)
    {
        _bitmap.Lock();
        _graphic.Clear();
        var v = 160000 * Sin(_f / 200.0);
        // _graphic.DrawTriangle((new Triangle3(new(100, 100, 1), new(100, 200, 1), new(200, 200, 1), Color.FromRgb(255, 255, 255)).GetProjection(new(0, 0, 1), 1)).WithColors(100, 100, 100), 100, 100, 100);
        // _graphic.DrawJuliaSet(_f / 100);
        for (int y = 0; y < _bitmap.PixelWidth; y++)
        {
            for (int x = 0; x < _bitmap.PixelHeight; x++)
            {
                if (!(Abs((((x - 400) ^ 2 + (y - 500) ^ 2) * ((x - 600) ^ 2 + (y - 500) ^ 2) - v)) < 5000))
                {
                    var r = 255;
                    var g = 255;
                    var b = 255;
                    var ptr = _bitmap.BackBuffer + x * 4 + _bitmap.BackBufferStride * y;
                    unsafe
                    {
                        *((int*)ptr) = (r << 16) | (g << 8) | b;
                    }
                }
            }
        }
        this.Title = $"{_f}";
        _bitmap.AddDirtyRect(new(0, 0, _bitmap.PixelWidth, _bitmap.PixelHeight));
        _bitmap.Unlock();
        _f++;
    }
}