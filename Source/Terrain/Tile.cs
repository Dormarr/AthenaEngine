using AthenaEngine.Source.Utility;

namespace AthenaEngine.Source.Terrain;

// Temporary Tile for proof-of-concept
public class Tile
{
    public Vector2 Position;
    public Vector3 Color; // Optional: simple per-tile color

    private const float HalfSize = 0.5f;

    public Tile(Vector2 position, Vector3 color)
    {
        Position = position;
        Color = color;
    }

    public float[] GetVertices()
    {
        float x = Position.X;
        float y = Position.Y;
        float z = 0f;

        // Colors
        float r = Color.X;
        float g = Color.Y;
        float b = Color.Z;

        // Return 6 vertices (two triangles) with 8 floats each
        return new float[]
        {
            // First Triangle
            x - HalfSize, y - HalfSize, z,   r, g, b,   0f, 0f, // bottom-left
            x + HalfSize, y - HalfSize, z,   r, g, b,   1f, 0f, // bottom-right
            x + HalfSize, y + HalfSize, z,   r, g, b,   1f, 1f, // top-right

            // Second Triangle
            x - HalfSize, y - HalfSize, z,   r, g, b,   0f, 0f, // bottom-left
            x + HalfSize, y + HalfSize, z,   r, g, b,   1f, 1f, // top-right
            x - HalfSize, y + HalfSize, z,   r, g, b,   0f, 1f  // top-left
        };
    }
}