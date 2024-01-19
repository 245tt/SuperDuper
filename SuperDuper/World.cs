using OpenTK.Mathematics;
using SuperDuper.Entities;

namespace SuperDuper
{
    class World
    {
        public bool DebugDraw = false;
        int drawDistance = 2;
        public float deltaTime;
        public int seed = 123;
        WorldGenerator worldGen;
        public List<Entity> entityList = new List<Entity>();
        public Dictionary<Vector2i, Chunk> loadedChunks = new Dictionary<Vector2i, Chunk>();
        public Vector2 gravity = new Vector2(0, -9.81f);
        public PlayerEntity player;
        Queue<Vector2i> chunksToLoad = new Queue<Vector2i>();
        Vector2i chunkPos;
        Vector2i worldPos;

        public World()
        {
            worldGen = new WorldGenerator(seed);
            PlayerEntity entity = new PlayerEntity("developer",this);
            entityList.Add(entity);
            player = entity;

            for (int x = -drawDistance; x < drawDistance; x++)
            {
                for (int y = -drawDistance; y < drawDistance; y++)
                {
                    Chunk chunk = worldGen.GenerateChunk(new Vector2i(x, y));
                    chunk.Mesh();
                    loadedChunks.Add(chunk.position, chunk);
                }
            }
        }
        public void Edit(Vector2 pos, byte id)
        {
            worldPos = (Vector2i)new Vector2(MathF.Floor(pos.X), MathF.Floor(pos.Y));

            chunkPos.X = (int)Math.Floor((pos.X + 1) / Chunk.chunkSize);
            chunkPos.Y = (int)Math.Floor((pos.Y + 1) / Chunk.chunkSize);
            Vector2i localPos;
            localPos.X = (worldPos.X % Chunk.chunkSize) + 1;
            localPos.Y = (worldPos.Y % Chunk.chunkSize) + 1;
            if (localPos.X < 0) localPos.X += Chunk.chunkSize;
            if (localPos.Y < 0) localPos.Y += Chunk.chunkSize;
            if (localPos.X >= Chunk.chunkSize) localPos.X -= Chunk.chunkSize;
            if (localPos.Y >= Chunk.chunkSize) localPos.Y -= Chunk.chunkSize;
            if (loadedChunks.ContainsKey(chunkPos))
            {
                loadedChunks[chunkPos].voxels[Chunk.GetIndex(localPos)] = id;
                loadedChunks[chunkPos].Mesh();
            }

        }
        public void Update(float delta)
        {
            deltaTime = delta;
            foreach (var rb in RigidbodyComponentCache.components)
            {
                rb.Update(this);
            }

            //update collisions
            MoveAndCollideAll(delta);

            for (int i = 0; i < entityList.Count; i++)
            {
                if (entityList[i].markToRemove) entityList[i].Dispose();
            }
        }
        public void MoveAndCollideAll(float delta)
        {
            foreach (RigidbodyComponent rb in RigidbodyComponentCache.components)
            {
                BoxColliderComponent colliderComp = rb.entity.GetComponent<BoxColliderComponent>();
                if (colliderComp == null) continue;
                var collider = colliderComp.box;
                collider.position += colliderComp.entity.transform.position;

                bool xmoveCollision = false;
                bool ymoveCollision = false;
                //move on X axis
                foreach (var chunk in loadedChunks.Values)
                {
                    foreach (var chunkBB in chunk.colliders)
                    {
                        AABB movedXRB = new AABB(collider.position + new Vector2(rb.velocity.X * delta, 0), collider.size);
                        AABB bb = chunkBB;// new AABB(new Vector2(-0.5f), Vector2.One);
                        if (Collisions.CheckAABBCollision(movedXRB, bb))
                        {
                            xmoveCollision = true;
                        }
                        //y move 
                        AABB movedYRB = new AABB(collider.position + new Vector2(0, rb.velocity.Y * delta), collider.size);
                        if (Collisions.CheckAABBCollision(movedYRB, bb))
                        {
                            ymoveCollision = true;
                        }

                    }
                }
                Vector2 move = Vector2.Zero;
                if (ymoveCollision && rb.velocity.Y < 0) 
                    rb.IsGrounded = true;
                else rb.IsGrounded = false;

                if (!xmoveCollision) { move.X += rb.velocity.X * delta; } else { rb.velocity.X = 0; }
                if (!ymoveCollision) { move.Y += rb.velocity.Y * delta; } else { rb.velocity.Y = 0; }
                if (xmoveCollision || ymoveCollision) rb.entity.OnCollide(this);
                rb.entity.transform.position += move;
            }
        }
        public void Draw()
        {

            for (int i = 0; i < loadedChunks.Count; i++)
            {
                if (loadedChunks.ElementAt(i).Value.vertices == null)
                {
                    loadedChunks.ElementAt(i).Value.Mesh();
                }
                Render.DrawChunk(loadedChunks.ElementAt(i).Value);
            }
            for (int i = 0; i < SpriteComponentCache.components.Count; i++)
            {
                if (SpriteComponentCache.components[i].active)
                    Render.DrawSprite(SpriteComponentCache.components[i]);
            }
        }

        public void DrawDebug()
        {
            for (int i = 0; i < loadedChunks.Count; i++)
            {
                for (int j = 0; j < loadedChunks.ElementAt(i).Value.colliders.Length; j++)
                {
                    var pos = loadedChunks.ElementAt(i).Value.colliders[j].position;
                    var size = loadedChunks.ElementAt(i).Value.colliders[j].size;
                    Render.DrawRectangle(pos - size / 2f, size);

                }
            }


            Render.DrawRectangle(chunkPos * Chunk.chunkSize - Vector2i.One, new Vector2(Chunk.chunkSize));
            Render.DrawRectangle(worldPos, new Vector2(1));
            for (int i = 0; i < BoxColliderComponentCache.components.Count; i++)
            {
                Render.DrawCollider(BoxColliderComponentCache.components[i]);
            }
        }
        public void Explode(Vector2 position, float force)
        {
            for (int y = (int)(position.Y - force - 1); y < (int)(position.Y + force + 1); y++)
            {
                for (int x = (int)(position.X - force - 1); x < (int)(position.X + force + 1); x++)
                {
                    float dist = (new Vector2(x, y) - position).Length;
                    if (dist < force)
                    {
                        this.Edit(new Vector2(x, y), 0);
                    }
                }
            }
        }
    }
}
