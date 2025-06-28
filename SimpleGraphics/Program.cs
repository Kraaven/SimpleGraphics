using System;
using System.Numerics;
using Silk.NET.Input;
using Silk.NET.Maths;
using Silk.NET.OpenGL;
using Silk.NET.Windowing;

public class Core
{
    private static IWindow _window;
    
    public static void Main()
    {
        
        //Window Creation
        WindowOptions options = WindowOptions.Default with
        {
            Size = new Vector2D<int>(1600, 1000),
            Title = "Simple Graphics Renderer"
        };
        
        _window = Window.Create(options);
        _window.Run();
    }
    
}