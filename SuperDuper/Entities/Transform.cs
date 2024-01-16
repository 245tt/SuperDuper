using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperDuper.Entities
{
    struct Transform
    {
        public Vector2 position = Vector2.Zero;
        public Vector2 velocity = Vector2.Zero;
        public Vector2 scale = Vector2.One;
        public float rotation =0;
        public int depth =0;

        public Transform()
        {
        }
        public static Transform operator +(Transform left, Transform right)
        {
            Transform ts = new Transform();
            ts.position = left.position + right.position;
            ts.rotation = left.rotation + right.rotation;
            ts.scale = left.scale;
            ts.velocity = left.velocity + right.velocity;
            ts.depth = left.depth;
            return ts;
        }
    }
}
