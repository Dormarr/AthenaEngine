using OpenTK.Graphics.OpenGL4;

namespace AthenaEngine.Source;

public class Renderer : IDisposable
{
    private int _vao;
    private int _vbo;
    private int _ebo;

    private Shader _shader;

    // Vertex format:
    // Position (3) | Color (3) | UV (2)
    private readonly float[] _vertices =
    {
        // x,    y,    z,     r, g, b,     u, v
        -1.0f, -1.0f, 0.0f,  1f, 0f, 0f,  0f, 0f,
         1.0f, -1.0f, 0.0f,  0f, 1f, 0f,  1f, 0f,
         1.0f,  1.0f, 0.0f,  0f, 0f, 1f,  1f, 1f,
        -1.0f,  1.0f, 0.0f,  1f, 1f, 0f,  0f, 1f
    };

    private readonly uint[] _indices =
    {
        0, 1, 2,
        0, 2, 3
    };

    public void Initialize()
    {
        _shader = new Shader(
            "Shaders/vertexShader.vert",
            "Shaders/fragmentShader.frag"
        );

        // --- GL State ---
        GL.ClearColor(0.7f, 0.7f, 0.7f, 1.0f);
        GL.Enable(EnableCap.DepthTest);
        GL.DepthFunc(DepthFunction.Less);

        // --- VAO ---
        _vao = GL.GenVertexArray();
        GL.BindVertexArray(_vao);

        // --- VBO ---
        _vbo = GL.GenBuffer();
        GL.BindBuffer(BufferTarget.ArrayBuffer, _vbo);
        GL.BufferData(
            BufferTarget.ArrayBuffer,
            _vertices.Length * sizeof(float),
            _vertices,
            BufferUsageHint.StaticDraw
        );

        // --- EBO ---
        _ebo = GL.GenBuffer();
        GL.BindBuffer(BufferTarget.ElementArrayBuffer, _ebo);
        GL.BufferData(
            BufferTarget.ElementArrayBuffer,
            _indices.Length * sizeof(uint),
            _indices,
            BufferUsageHint.StaticDraw
        );

        int stride = 8 * sizeof(float);

        // Position
        GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, stride, 0);
        GL.EnableVertexAttribArray(0);

        // Color
        GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false, stride, 3 * sizeof(float));
        GL.EnableVertexAttribArray(1);

        // UV
        GL.VertexAttribPointer(2, 2, VertexAttribPointerType.Float, false, stride, 6 * sizeof(float));
        GL.EnableVertexAttribArray(2);

        GL.BindVertexArray(0);
    }

    public void Clear()
    {
        GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
    }

    public void BindTextures(Texture[] textures)
    {
        _shader.Use();

        for (int i = 0; i < textures.Length; i++)
        {
            GL.ActiveTexture(TextureUnit.Texture0 + i);
            textures[i].Bind();
            _shader.SetInt($"texture{i}", i);
        }
    }

    public void Render(Camera camera)
    {
        _shader.Use();

        _shader.SetMatrix4("view", camera.GetViewMatrix());
        _shader.SetMatrix4("projection", camera.GetProjectionMatrix());

        GL.BindVertexArray(_vao);

        GL.DrawElements(
            PrimitiveType.Triangles,
            _indices.Length,
            DrawElementsType.UnsignedInt,
            0
        );

        GL.BindVertexArray(0);
    }

    public void Dispose()
    {
        GL.DeleteBuffer(_ebo);
        GL.DeleteBuffer(_vbo);
        GL.DeleteVertexArray(_vao);
        _shader.Delete();
    }
    
    public void SetCamera(Camera camera)
    {
        _shader.Use();
        _shader.SetMatrix4("view", camera.GetViewMatrix());
        _shader.SetMatrix4("projection", camera.GetProjectionMatrix());
    }

}
