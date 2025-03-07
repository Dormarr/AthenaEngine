using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Graphics.OpenGL4;
using System;

class Game : GameWindow
{
    public Game(GameWindowSettings gws, NativeWindowSettings nws) : base(gws, nws) { }

    protected override void OnLoad()
    {
        base.OnLoad();
        GL.ClearColor(0.5f, 0.5f, 0.5f, 1.0f); // Set background color
    }

    protected override void OnRenderFrame(FrameEventArgs args)
    {
        base.OnRenderFrame(args);
        GL.Clear(ClearBufferMask.ColorBufferBit);
        SwapBuffers(); // Render the frame
    }

    public static void Main()
    {
        var game = new Game(GameWindowSettings.Default, new NativeWindowSettings { Size = new OpenTK.Mathematics.Vector2i(800, 600), Title = "My Game Engine" });
        game.Run();
    }
}