using System;
using System.Diagnostics;
using System.Collections.Generic;
using OpenTK.Graphics.OpenGL;
using AthenaEngine.Source.Utility;

namespace AthenaEngine.Source.Terrain;

public class Tilemap
{
    private int _width, _height;
    private List<Tile> _tiles = new();
    private float[] _vertices;
    private int _vao, _vbo;
    private Texture[] _textures = new Texture[2];

    public Tilemap(int width, int height)
    {
        _width = width / 2;
        _height = height / 2;
        GenerateTiles();
        _textures[0] = new Texture(1, 1, 0);
        _textures[1] = new Texture(1, 1, 1);
        GenerateTileVertices();
        SetupBuffers();
    }
    
    private void GenerateTiles()
    {
        Debug.Print("Generating tiles...");
        for (int y = -(_height); y < _height; y++)
        {
            for (int x = -(_width); x < _width; x++)
            {
                int binVal = Math.Abs((x + y) % 2);
                _tiles.Add(new Tile(new Vector2(x, y), binVal));
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
    }

    private void SetupBuffers()
    {
        _vao = GL.GenVertexArray();
        _vbo = GL.GenBuffer();

        GL.BindVertexArray(_vao);
        GL.BindBuffer(BufferTarget.ArrayBuffer, _vbo);
        GL.BufferData(BufferTarget.ArrayBuffer, _vertices.Length * sizeof(float), _vertices, BufferUsageHint.StaticDraw);

        // Position Attribute (Location = 0)
        GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 5 * sizeof(float), 0);
        GL.EnableVertexAttribArray(0);

        // Texture Coordinate Attribute (Location = 1)
        GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, 5 * sizeof(float), 3 * sizeof(float));
        GL.EnableVertexAttribArray(1);
    }

    public void Render(Renderer renderer)
    {
        GL.BindVertexArray(_vao);

        foreach (Tile tile in _tiles)
        {
            Debug.Print($"Rendering tile {tile.TextureIndex}");
            _textures[tile.TextureIndex].Bind();  // Bind the correct texture

            // Draw only this tile's 6 vertices (not the entire map)
            GL.DrawArrays(PrimitiveType.Triangles, tile.TextureIndex * 6, 6);
        }
    
        renderer.Render();
    }

}
