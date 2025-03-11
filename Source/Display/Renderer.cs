using System.Diagnostics;
using OpenTK.Graphics.OpenGL4;

namespace AthenaEngine.Source;

public class Renderer
{
    private int _vao, _vbo, _ebo;
    private Shader _shader;
    private Camera _camera;

    float[] _vertices = {
        -1.0f, -1.0f, 0.0f,  1.0f, 0.0f, 0.0f,  0.0f, 0.0f, 
        1.0f, -1.0f, 0.0f,  0.0f, 1.0f, 0.0f,  1.0f, 0.0f, 
        1.0f,  1.0f, 0.0f,  0.0f, 0.0f, 1.0f,  1.0f, 1.0f, 
        -1.0f,  1.0f, 0.0f,  1.0f, 1.0f, 0.0f,  0.0f, 1.0f  
    };

    uint[] _indices = { 0, 1, 2, 2, 3, 0 };

    public void Initialize(Camera camera)
    {
        Debug.Print("Initializing renderer...");
        
        _camera = camera;
        _shader = new Shader("Shaders/vertexShader.vert", "Shaders/fragmentShader.frag");

        ClearScreen();
        
        _vao = GL.GenVertexArray();
        GL.BindVertexArray(_vao);

        _vbo = GL.GenBuffer();
        GL.BindBuffer(BufferTarget.ArrayBuffer, _vbo);
        GL.BufferData(BufferTarget.ArrayBuffer, _vertices.Length * sizeof(float), _vertices, BufferUsageHint.StaticDraw);

        _ebo = GL.GenBuffer();
        GL.BindBuffer(BufferTarget.ElementArrayBuffer, _ebo);
        GL.BufferData(BufferTarget.ElementArrayBuffer, _indices.Length * sizeof(uint), _indices, BufferUsageHint.StaticDraw);

        GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 8 * sizeof(float), 0);
        GL.EnableVertexAttribArray(0);

        GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false, 8 * sizeof(float), 3 * sizeof(float));
        GL.EnableVertexAttribArray(1);

        GL.VertexAttribPointer(2, 2, VertexAttribPointerType.Float, false, 8 * sizeof(float), 6 * sizeof(float));
        GL.EnableVertexAttribArray(2);

        GL.BindVertexArray(0);
    }

    public void ClearScreen()
    {
        GL.ClearColor(0.7f,0.7f,0.7f,0.7f);
    }

    public void EraseScreen()
    {
        GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
    }

    public void Render()
    {
        ClearScreen();
        
        // Wireframe for debugging.
        // GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Line);
        
        _shader.Use();
        
        _shader.SetMatrix4("view", _camera.GetViewMatrix());
        _shader.SetMatrix4("projection", _camera.GetProjectionMatrix());
        
        GL.BindVertexArray(_vao);
        GL.DrawElements(PrimitiveType.Quads, _indices.Length, DrawElementsType.UnsignedInt, 0);
        
        // GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Fill);
    }

    public void Dispose()
    {
        _shader.Delete();
        GL.DeleteBuffer(_vbo);
        GL.DeleteVertexArray(_vao);
    }
}
