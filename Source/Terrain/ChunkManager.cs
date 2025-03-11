using System;
using System.Collections.Generic;
using AthenaEngine.Source.Utility;

namespace AthenaEngine.Source.Terrain;
class ChunkManager
{
    public Dictionary<Vector2Int, Chunk> LoadedChunks = new();
    private int renderDistance = 3; // Number of chunks to load around the player

    public void Update(Vector2 playerPosition)
    {
        playerPosition = new Vector2(0, 0); // Temporary player position
        
        Vector2Int playerChunk = new Vector2Int(
            (int)(playerPosition.X / Chunk.ChunkSize),
            (int)(playerPosition.Y / Chunk.ChunkSize)
        );

        // Load new chunks if needed
        for (int x = -renderDistance; x <= renderDistance; x++)
        {
            for (int y = -renderDistance; y <= renderDistance; y++)
            {
                Vector2Int chunkPos = new Vector2Int(playerChunk.X + x, playerChunk.Y + y);

                if (!LoadedChunks.ContainsKey(chunkPos))
                {
                    LoadedChunks[chunkPos] = new Chunk(chunkPos);
                }
            }
        }

        // Unload chunks that are too far away
        List<Vector2Int> toRemove = new();
        foreach (var chunk in LoadedChunks.Keys)
        {
            if (Math.Abs(chunk.X - playerChunk.X) > renderDistance ||
                Math.Abs(chunk.Y - playerChunk.Y) > renderDistance)
            {
                toRemove.Add(chunk);
            }
        }

        foreach (var chunk in toRemove)
        {
            LoadedChunks.Remove(chunk);
        }
    }
}
