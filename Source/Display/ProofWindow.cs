using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.Common;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using AthenaEngine.Source.Terrain;

namespace AthenaEngine.Source;

public class ProofWindow : GameWindow
{
    private Renderer _renderer;
    private Tilemap _tilemap;
    private Camera _camera;

    public ProofWindow(int width, int height, string title)
        : base(
            GameWindowSettings.Default,
            new NativeWindowSettings
            {
                Size = new Vector2i(width, height),
                Title = title
            })
    {
    }

    protected override void OnLoad()
    {
        base.OnLoad();

        // --- Renderer ---
        _renderer = new Renderer();
        _renderer.Initialize();

        // --- Camera ---
        _camera = new Camera(
            new Vector3(0f, 0f, 5f), // pull back so we can see tiles
            Size.X / (float)Size.Y,
            true
        );

        // --- Tilemap ---
        _tilemap = new Tilemap(16, 16); // small 8x8 grid
    }

    protected override void OnRenderFrame(FrameEventArgs args)
    {
        base.OnRenderFrame(args);

        // Clear frame
        _renderer.Clear();

        // Set camera uniforms for shader
        _renderer.SetCamera(_camera);

        // Draw tilemap
        _tilemap.Render(_renderer);

        SwapBuffers();
    }

    protected override void OnResize(ResizeEventArgs e)
    {
        base.OnResize(e);

        GL.Viewport(0, 0, Size.X, Size.Y);
        // _camera.SetAspectRatio(Size.X / (float)Size.Y);
    }

    protected override void OnUnload()
    {
        base.OnUnload();

        _tilemap.Dispose();
        _renderer.Dispose();
    }
}