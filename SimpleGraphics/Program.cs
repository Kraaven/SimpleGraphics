﻿using Silk.NET.Input;
using Silk.NET.OpenGL;
using Silk.NET.Windowing;
using System;
using System.Numerics;
using Silk.NET.Maths;

    class Core
    {
        private static IWindow window;
        private static GL Gl;

        //Our new abstracted objects, here we specify what the types are.
        private static BufferObject<float> Vbo;
        private static BufferObject<uint> Ebo;
        private static VertexArrayObject<float, uint> Vao;
        private static IInputContext input;
        
        private static Shader Shader;

        private static Vector3 PlayerPosition = new Vector3(0, 0, 5);
        private static Vector2 WindowSize = new Vector2(600, 600);
        private static float Focus = 1;

        private static readonly float[] Vertices =
        {
            //X    Y      Z     S    T
             1.0f,  1.0f, 0.0f, 1.0f, 0.0f,
             1.0f, -1.0f, 0.0f, 1.0f, 1.0f,
            -1.0f, -1.0f, 0.0f, 0.0f, 1.0f,
            -1.0f,  1.0f, 0.0f, 0.0f, 0.0f
        };

        private static readonly uint[] Indices =
        {
            0, 1, 3,
            1, 2, 3
        };


        private static void Main(string[] args)
        {
            var options = WindowOptions.Default;
            options.Size = new Vector2D<int>(800, 600);
            options.Title = "LearnOpenGL with Silk.NET";
            window = Window.Create(options);

            window.Load += OnLoad;
            window.Update += Update;
            window.Render += OnRender;
            window.FramebufferResize += OnFramebufferResize;
            window.Closing += OnClose;
            
            

            window.Run();

            window.Dispose();
        }


        private static void OnLoad()
        {
            input = window.CreateInput();
            for (int i = 0; i < input.Keyboards.Count; i++)
            {
                input.Keyboards[i].KeyDown += KeyDown;
            }

            Gl = GL.GetApi(window);

            //Instantiating our new abstractions
            Ebo = new BufferObject<uint>(Gl, Indices, BufferTargetARB.ElementArrayBuffer);
            Vbo = new BufferObject<float>(Gl, Vertices, BufferTargetARB.ArrayBuffer);
            Vao = new VertexArrayObject<float, uint>(Gl, Vbo, Ebo);

            //Telling the VAO object how to lay out the attribute pointers
            Vao.VertexAttributePointer(0, 3, VertexAttribPointerType.Float, 5, 0);
            Vao.VertexAttributePointer(1, 2, VertexAttribPointerType.Float, 5, 3);

            Shader = new Shader(Gl, "Resources/shader.vert", "Resources/shader.frag");
        }

        private static unsafe void Update(double delta)
        {
            HandleConstantKeyPress();
        }

        private static unsafe void OnRender(double obj)
        {
            Gl.Clear((uint) ClearBufferMask.ColorBufferBit);

            //Binding and using our VAO and shader.
            Vao.Bind();
            Shader.Use();
            
            Shader.SetUniform("WindowSize", WindowSize);
            Shader.SetUniform("PlayerPosition", PlayerPosition);
            Shader.SetUniform("Focus", Focus);
            
            Gl.DrawElements(PrimitiveType.Triangles, (uint) Indices.Length, DrawElementsType.UnsignedInt, null);
        }

        private static void OnFramebufferResize(Vector2D<int> newSize)
        {
            Gl.Viewport(newSize);
            WindowSize = new Vector2(newSize.X, newSize.Y);
        }

        private static void OnClose()
        {
            //Remember to dispose all the instances.
            Vbo.Dispose();
            Ebo.Dispose();
            Vao.Dispose();
            Shader.Dispose();
        }

        private static void KeyDown(IKeyboard arg1, Key arg2, int arg3)
        {
            switch (arg2)
            {
                case Key.Escape:
                    window.Close();
                    break;
            }
        }

        private static void HandleConstantKeyPress()
        {
            if(input.Keyboards[0].IsKeyPressed(Key.Left)) PlayerPosition.X -= 1f;
            if(input.Keyboards[0].IsKeyPressed(Key.Right)) PlayerPosition.X += 1f;
            if(input.Keyboards[0].IsKeyPressed(Key.Up)) PlayerPosition.Y -= 1f;
            if(input.Keyboards[0].IsKeyPressed(Key.Down)) PlayerPosition.Y += 1f;

            if (input.Keyboards[0].IsKeyPressed(Key.A)) Focus += 0.1f;
            if (input.Keyboards[0].IsKeyPressed(Key.S)) Focus -= 0.1f;

            
            // if(input.Keyboards.All(x => x.IsKeyPressed(Key.Left))) PlayerPosition.X -= 10f;
        }
    }