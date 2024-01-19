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
