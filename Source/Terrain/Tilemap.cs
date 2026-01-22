using System;
using System.Collections.Generic;
using OpenTK.Graphics.OpenGL4;
using AthenaEngine.Source.Utility;

namespace AthenaEngine.Source.Terrain;

public class Tilemap : IDisposable
{
    private readonly int _width;
    private readonly int _height;

    private readonly List<Tile> _tiles = new();

    private float[] _vertices;
    private int _vao;
    private int _vbo;

    private Texture[] _textures;

    private int _vertexCount;

    public Tilemap(int width, int height)
    {
        _width = width / 2;
        _height = height / 2;

        GenerateTiles();
        GenerateTileVertices();
        SetupBuffers();

        _textures = new Texture[]
        {
            new Texture(1, 1, 0),
            new Texture(1, 1, 1)
        };
    }

    private void GenerateTiles()
    {
        for (int y = -_height; y < _height; y++)
        {
            for (int x = -_width; x < _width; x++)
            {
                int textureIndex = Math.Abs((x + y) % 2);
                // _tiles.Add(new Tile(new Vector2(x, y), new Vector3(50, 100, 150)));
                _tiles.Add(new Tile(new Vector2(x, y), (x + y) % 2 == 0 ? new Vector3(1f,1f,1f) : new Vector3(0f,0f,0f)));

            }
        }
    }

    private void GenerateTileVertices()
    {
        List<float> vertexList = new();

        foreach (Tile tile in _tiles)
        {
            vertexList.AddRange(tile.GetVertices());
        }

        _vertices = vertexList.ToArray();
        _vertexCount = _vertices.Length / 8; // 8 floats per vertex
    }

    private void SetupBuffers()
    {
        _vao = GL.GenVertexArray();
        _vbo = GL.GenBuffer();

        GL.BindVertexArray(_vao);

        GL.BindBuffer(BufferTarget.ArrayBuffer, _vbo);
        GL.BufferData(
            BufferTarget.ArrayBuffer,
            _vertices.Length * sizeof(float),
            _vertices,
            BufferUsageHint.StaticDraw
        );

        int stride = 8 * sizeof(float);

        // Position (vec3)
        GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, stride, 0);
        GL.EnableVertexAttribArray(0);

        // Color (vec3)
        GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false, stride, 3 * sizeof(float));
        GL.EnableVertexAttribArray(1);

        // UV (vec2)
        GL.VertexAttribPointer(2, 2, VertexAttribPointerType.Float, false, stride, 6 * sizeof(float));
        GL.EnableVertexAttribArray(2);

        GL.BindVertexArray(0);
    }

    public void Render(Renderer renderer)
    {
        // renderer.BindTextures(_textures);

        GL.BindVertexArray(_vao);

        GL.DrawArrays(
            PrimitiveType.Triangles,
            0,
            _vertexCount
        );

        GL.BindVertexArray(0);
    }

    public void Dispose()
    {
        GL.DeleteBuffer(_vbo);
        GL.DeleteVertexArray(_vao);
    }
}
