using System.Diagnostics;
using OpenTK.Windowing.Desktop;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using AthenaEngine.Source.Terrain;
//using Vector3 = AthenaEngine.Source.Utility.Vector3;

namespace AthenaEngine.Source;

public class Window
{
    private GameWindow _window;
    private Renderer _renderer;
    private Tilemap _tilemap;

    public Camera camera;
    
    public Window(int width, int height, string title)
    {
        _window = new GameWindow(GameWindowSettings.Default, NativeWindowSettings.Default);
        _window.Size = new Vector2i(width, height);
        _window.Title = title;

        _renderer = new Renderer();
        _tilemap = new Tilemap(16, 16); // Add dynamically later.

        _window.Load += OnLoad;
        _window.RenderFrame += OnRenderFrame;
        _window.Unload += OnUnload;
    }

    public void Run()
    {
        _window.Run();
    }
    
    private void OnLoad()
    {
        Debug.Print("Loading window...");
        camera = new Camera(new Vector3(0f,0f,0f), (float)_window.Size.X / (float)_window.Size.Y, true);
        _renderer.Initialize(camera); // Set up OpenGL states
    }

    private void OnRenderFrame(FrameEventArgs args)
    {
        _renderer.ClearScreen();
        _tilemap.Render(_renderer);
        _window.SwapBuffers();
    }

    private void OnUnload()
    {
        Debug.Print("Unloading window...");
        _renderer.Dispose();
    }
}