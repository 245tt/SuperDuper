using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperDuper
{
    class WorldGenerator
    {
        public int seed;
        public WorldGenerator(int seed)
        {
            this.seed = seed;
        }

        public Chunk GenerateChunk(Vector2i pos) 
        {
            Chunk chunk = new Chunk(pos);

            Random rng = new Random(seed);
            for (int x = 0; x < Chunk.chunkSize; x++)
            {
                for (int y = 0; y < Chunk.chunkSize; y++)
                {
                    Vector2i tileWorldCoordinate = new Vector2i(pos.X * Chunk.chunkSize+ x, pos.Y * Chunk.chunkSize + y);
                    int level = 32;// Chunk.chunkSize+ (int)(Math.Sin(tileWorldCoordinate.X/5f)*5f);
                    if (tileWorldCoordinate.Y > level) 
                    {
                        chunk.voxels[Chunk.GetIndex(x, y)] = 0;
                    }else 
                    {
                        chunk.voxels[Chunk.GetIndex(x, y)] = 1;
                    }
                }
            }
            return chunk;
        }

    }
}
