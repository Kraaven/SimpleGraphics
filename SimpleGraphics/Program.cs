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
    private static uint _vao;
    private static uint _vbo;
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

    private static unsafe void OnLoad()
    {
        //Input Binding
        IInputContext input = _window.CreateInput();
        for (int i = 0; i < input.Keyboards.Count; i++) input.Keyboards[i].KeyDown += KeyDown;
        
        //Window OpenGL stuff
        _gl = _window.CreateOpenGL();
        _gl.ClearColor(Color.CornflowerBlue);
        
        _vao = _gl.GenVertexArray();
        _gl.BindVertexArray(_vao);
        
        float[] vertices =
        {
            0.5f,  0.5f, 0.0f,
            0.5f, -0.5f, 0.0f,
            -0.5f, -0.5f, 0.0f,
            -0.5f,  0.5f, 0.0f
        };
        
        _vbo = _gl.GenBuffer();
        _gl.BindBuffer(BufferTargetARB.ArrayBuffer, _vbo);

        fixed (float* buf = vertices)
            _gl.BufferData(BufferTargetARB.ArrayBuffer, (nuint) (vertices.Length * sizeof(float)), buf, BufferUsageARB.StaticDraw);
    }

    private static unsafe void OnUpdate(double deltaTime)
    {
        if (_index == 20)
        {
            _gl.ClearColor(Color.FromArgb(1, _random.Next() % 255, _random.Next() % 255, _random.Next() % 255));
            _index = 0;
        }
        else _index++;
    }

    private static unsafe void OnRender(double deltaTime)
    {
        _gl.Clear(ClearBufferMask.ColorBufferBit);
    }

    private static unsafe void KeyDown(IKeyboard keyboard, Key key, int keyCode)
    {
        if(key == Key.Escape) _window.Close();
    }
}