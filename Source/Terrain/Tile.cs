using AthenaEngine.Source.Utility;

namespace AthenaEngine.Source.Terrain;
public class Tile
{
    public Vector2 Position;
    public int TextureIndex;
    public Vector2 UV;

    public Tile(Vector2 position, int textureIndex = 0)
    {
        Position = position;
        TextureIndex = textureIndex;
        UV = new Vector2(0, 0); //temporary
    }
    
    public float[] GetVertices()
    {
        float halfSize = 0.5f; // Half tile size to center on position

        return new float[]
        {
            // First Triangle
            Position.X - halfSize, Position.Y - halfSize, 0.0f,  0.0f, 0.0f, 
            Position.X + halfSize, Position.Y - halfSize, 0.0f,  1.0f, 0.0f, 
            Position.X + halfSize, Position.Y + halfSize, 0.0f,  1.0f, 1.0f, 

            // Second Triangle
            Position.X - halfSize, Position.Y - halfSize, 0.0f,  0.0f, 0.0f, 
            Position.X + halfSize, Position.Y + halfSize, 0.0f,  1.0f, 1.0f, 
            Position.X - halfSize, Position.Y + halfSize, 0.0f,  0.0f, 1.0f 
        };
    }

}