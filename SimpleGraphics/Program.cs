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
        _window.Load += OnLoad;
        _window.Update += OnUpdate;
        _window.Render += OnRender;

        _window.Run();
    }

    private static void OnLoad()
    {
        IInputContext input = _window.CreateInput();
        for (int i = 0; i < input.Keyboards.Count; i++) input.Keyboards[i].KeyDown += KeyDown;
    }

    private static void OnUpdate(double deltaTime) { }

    private static void OnRender(double deltaTime) { }

    private static void KeyDown(IKeyboard keyboard, Key key, int keyCode)
    {
        if(key == Key.Escape) _window.Close();
    }
}