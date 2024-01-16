using OpenTK.Mathematics;

namespace SuperDuper
{
    static class Collisions
    {
        public static bool CheckAABBCollision(AABB aabb1, AABB aabb2)
        {
            Vector2 minAABB1 = aabb1.position - aabb1.size / 2;
            Vector2 maxAABB1 = aabb1.position + aabb1.size / 2;
            Vector2 minAABB2 = aabb2.position - aabb2.size / 2;
            Vector2 maxAABB2 = aabb2.position + aabb2.size / 2;

            if (maxAABB1.X < minAABB2.X || minAABB1.X > maxAABB2.X || maxAABB1.Y < minAABB2.Y || minAABB1.Y > maxAABB2.Y)
            {
                return false; // No collision
            }
            else
            {
                return true; // Collision detected
            }
        }
        public static float GetSweptAABBTime(Vector2 velocity, AABB box1, AABB box2,out Vector2 p1,out Vector2 p2)
        {
            float time;
            float normalx;
            float normaly;

            float xInvEntry;
            float yInvEntry;
            float xInvExit;
            float yInvExit;
            if (velocity.X > 0.0f)
            {
                xInvEntry = (box2.position.X - box2.size.X / 2f) - (box1.position.X - box1.size.X / 2f);
                xInvExit = (box2.position.X + box2.size.X / 2f) - (box1.position.X - box1.size.X / 2f);
            }
            else
            {
                xInvEntry = (box1.position.X - box1.size.X / 2) - (box2.position.X-box2.size.X/2);
                xInvExit = (box1.position.X - box1.size.X / 2) - (box2.position.X + box2.size.X / 2);
            }

            if (velocity.Y < 0.0f)
            {
                yInvEntry = (box2.position.Y - box2.size.Y / 2f) - (box1.position.Y - box1.size.Y / 2f);
                yInvExit = (box2.position.Y + box2.size.Y / 2f) - (box1.position.Y - box1.size.Y / 2f);
            }
            else
            {
                yInvEntry = (box1.position.Y - box1.size.Y / 2) - (box2.position.Y - box2.size.Y / 2);
                yInvExit = (box1.position.Y - box1.size.Y / 2) - (box2.position.Y + box2.size.Y / 2);
            }

            p1 = new Vector2(xInvEntry,yInvEntry);
            p2 = new Vector2(xInvExit,yInvExit);

            float xEntry, yEntry;
            float xExit, yExit;

            if (velocity.X == 0.0f)
            {
                xEntry = float.MinValue;
                xExit = float.MaxValue;
            }
            else
            {
                xEntry = xInvEntry / velocity.X;
                xExit = xInvExit / velocity.X;
            }

            if (velocity.Y == 0.0f)
            {
                yEntry = float.MinValue;
                yExit = float.MaxValue;
            }
            else
            {
                yEntry = yInvEntry / velocity.Y;
                yExit = yInvExit / velocity.Y;
            }


            Console.WriteLine("entry: {0}, exit: {1}",yEntry,yExit);
            float entryTime = Math.Max(xEntry, yEntry);
            float exitTime = Math.Min(xExit, yExit);

            //no collision
            if (entryTime > exitTime || xEntry < 0.0f && yEntry < 0.0f || xEntry > 1.0f || yEntry > 1.0f)
            {
                normalx = 0.0f;
                normaly = 0.0f;
                return 1.0f;
            }
            else // if there was a collision 
            {
                // calculate normal of collided surface
                if (xEntry > yEntry)
                {
                    if (xInvEntry < 0.0f)
                    {
                        normalx = 1.0f;
                        normaly = 0.0f;
                    }
                    else
                    {
                        normalx = -1.0f;
                        normaly = 0.0f;
                    }
                }
                else
                {
                    if (yInvEntry < 0.0f)
                    {
                        normalx = 0.0f;
                        normaly = 1.0f;
                    }
                    else
                    {
                        normalx = 0.0f;
                        normaly = -1.0f;
                    }
                } // return the time of collisionreturn entryTime; 
                Console.WriteLine("collsion");
                return entryTime;
            }
        }
    }
    public struct AABB
    {
        public Vector2 position;
        public Vector2 size;
        public AABB(Vector2 pos, Vector2 size)
        {
            this.position = pos;
            this.size = size;
        }
    }
    struct Triangle
    {
        public Vector2 vertex1;
        public Vector2 vertex2;
        public Vector2 vertex3;

        public Triangle(Vector2 v1, Vector2 v2, Vector2 v3)
        {
            this.vertex1 = v1;
            this.vertex2 = v2;
            this.vertex3 = v3;
        }
    }
    struct Line
    {
        public Vector2 point1;
        public Vector2 point2;
        public Line(Vector2 p1, Vector2 p2)
        {
            this.point1 = p1;
            this.point2 = p2;
        }
    }
}
