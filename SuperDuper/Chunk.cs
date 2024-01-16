using OpenTK.Mathematics;

namespace SuperDuper
{
    public struct Block
    {
        public Vector2i size;
        public byte id;
        public Block(Vector2i size, byte id)
        {
            this.size = size;
            this.id = id;
        }
    }
    public class Chunk
    {
        public Vector2i position;
        public static float tileSize = 1f / 1f;
        public static int chunkSize = 32;
        public byte[] voxels;
#if CLIENT
        public float[] vertices;
        public AABB[] colliders;

        public void Mesh()
        {
            List<float> data = new List<float>();
            Dictionary<Vector2i, Block> blocks = new Dictionary<Vector2i, Block>();

            //fill array with 1x1 blocks
            for (int y = 0; y < chunkSize; y++)
            {
                for (int x = 0; x < chunkSize; x++)
                {
                    blocks.Add(new Vector2i(x, y), new Block(Vector2i.One, voxels[GetIndex(x, y)]));
                }
            }
            //merge block on x axis
            for (int y = 0; y < chunkSize; y++)
            {
                for (int x = 0; x < chunkSize; x++)
                {
                    //skip the first one
                    if (x > 0)
                    {
                        //check if previous has the same id
                        if (voxels[GetIndex(x - 1, y)] == voxels[GetIndex(x, y)])
                        {
                            //expand current
                            Block previousBlock = blocks[new Vector2i(x - 1, y)];
                            Block currentblock = blocks[new Vector2i(x, y)];
                            currentblock.size.X += previousBlock.size.X;
                            blocks[new Vector2i(x, y)] = currentblock;
                            //remove previous
                            blocks.Remove(new Vector2i(x - 1, y));


                        }
                    }
                }
            }
            //merge on y axis
            for (int x = 0; x < chunkSize; x++)
            {
                for (int y = 0; y < chunkSize; y++)
                {
                    //skip the first one
                    if (y > 0)
                    {
                        //check if there is block
                        if (blocks.ContainsKey(new Vector2i(x, y)) && blocks.ContainsKey(new Vector2i(x, y - 1)))
                        {
                            //check ids
                            if (voxels[GetIndex(x, y - 1)] == voxels[GetIndex(x, y)])
                            {
                                Block previousBlock = blocks[new Vector2i(x, y - 1)];
                                Block currentblock = blocks[new Vector2i(x, y)];
                                //check if they have the same width
                                if (currentblock.size.X == previousBlock.size.X)
                                {
                                    currentblock.size.Y += previousBlock.size.Y;
                                    blocks[new Vector2i(x, y)] = currentblock;
                                    blocks.Remove(new Vector2i(x, y - 1));
                                }
                            }
                        }
                    }
                }
            }
            for (int i = 0; i < blocks.Count; i++)
            {
                Vector2i pos = blocks.ElementAt(i).Key;
                Vector2i size = blocks.ElementAt(i).Value.size;
                byte id = blocks.ElementAt(i).Value.id;

                if (id != 0)
                {
                    //first triangle
                    data.Add(pos.X);
                    data.Add(pos.Y);

                    data.Add(pos.X - size.X);
                    data.Add(pos.Y);

                    data.Add(pos.X);
                    data.Add(pos.Y - size.Y);

                    //second triangle
                    data.Add(pos.X - size.X);
                    data.Add(pos.Y - size.Y);

                    data.Add(pos.X - size.X);
                    data.Add(pos.Y);

                    data.Add(pos.X);
                    data.Add(pos.Y - size.Y);
                }
            }
            colliders = new AABB[blocks.Count];
            for (int i = 0; i < blocks.Count; i++)
            {
                if (blocks.ElementAt(i).Value.id != 0)
                {
                    AABB col = new AABB(new Vector2(this.position.X*32f, this.position.Y * 32f) + blocks.ElementAt(i).Key - (Vector2)blocks.ElementAt(i).Value.size / 2f, blocks.ElementAt(i).Value.size);
                    colliders[i] = col;
                }


            }
            vertices = data.ToArray();
        }
#endif
        public Chunk(Vector2i pos)
        {
            this.position = pos;
            voxels = new byte[chunkSize * chunkSize];
            for (int i = 0; i < chunkSize * chunkSize; i++)
            {
                voxels[i] = 1;
            }
        }
        public static int GetIndex(int x, int y)
        {
            return y * chunkSize + x;
        }
        public static int GetIndex(Vector2i xy)
        {
            return xy.Y * chunkSize + xy.X;
        }
        public byte[] Serialize()
        {

            return null;
        }
        public void Deserialize(byte[] data)
        {

        }
    }
}

