using System;
using System.Drawing;
using System.Numerics;
using Silk.NET.Input;
using Silk.NET.Maths;
using Silk.NET.OpenGL;
using Silk.NET.Windowing;

public class Core
{
    private static IWindow _window;
    private static GL _gl;
    private static Random _random = new Random();

    private static int _index = 0;
    
    public static void Main()
    {
        
        //Window Creation
        WindowOptions options = WindowOptions.Default with
        {
            Size = new Vector2D<int>(1600, 1000),
            Title = "Simple Graphics Renderer"
        };
        _window = Window.Create(options);
        
        //Window Subscribed runtime functions
        _window.Load += OnLoad;
        _window.Update += OnUpdate;
        _window.Render += OnRender;

        //Window execution
        _window.Run();
    }

    private static void OnLoad()
    {
        //Input Binding
        IInputContext input = _window.CreateInput();
        for (int i = 0; i < input.Keyboards.Count; i++) input.Keyboards[i].KeyDown += KeyDown;
        
        //Window OpenGL stuff
        _gl = _window.CreateOpenGL();
        _gl.ClearColor(Color.CornflowerBlue);
    }

    private static void OnUpdate(double deltaTime)
    {
        if (_index == 20)
        {
            _gl.ClearColor(Color.FromArgb(1, _random.Next() % 255, _random.Next() % 255, _random.Next() % 255));
            _index = 0;
        }
        else _index++;
    }

    private static void OnRender(double deltaTime)
    {
        _gl.Clear(ClearBufferMask.ColorBufferBit);
    }

    private static void KeyDown(IKeyboard keyboard, Key key, int keyCode)
    {
        if(key == Key.Escape) _window.Close();
    }
}