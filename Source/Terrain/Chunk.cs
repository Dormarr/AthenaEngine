using AthenaEngine.Source.Utility;

namespace AthenaEngine.Source.Terrain;

public class Chunk
{
    public const int ChunkSize = 32;
    public Tile[,] Tiles;
    public Vector2Int ChunkPosition;

    public Chunk(Vector2Int chunkPosition)
    {
        ChunkPosition = chunkPosition;
        Tiles = new Tile[ChunkSize, ChunkSize];

        GenerateTiles();
    }

    public void GenerateTiles()
    {
        for (int x = 0; x < ChunkSize; x++)
        {
            for (int y = 0; y < ChunkSize; y++)
            {
                Vector2 worldPos = new Vector2(
                    (ChunkPosition.X * ChunkSize) + x,
                    (ChunkPosition.Y * ChunkSize) + y
                );

                int tileType = DetermineTileType(worldPos);
                Vector2 uv = ComputeUV(tileType);

                Tiles[x, y] = new Tile(worldPos, tileType);
            }
        }
    }
    
    private int DetermineTileType(Vector2 worldPos)
    {
        // Placeholder procedural generation
        return ((int)worldPos.X + (int)worldPos.Y) % 2 == 0 ? 1 : 2;
    }

    private Vector2 ComputeUV(int tileType)
    {
        // Placeholder: UV mapping based on tile type
        return new Vector2(tileType * 0.1f, tileType * 0.1f);
    }

}