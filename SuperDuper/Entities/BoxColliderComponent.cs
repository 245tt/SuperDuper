using System;
using OpenTK.Mathematics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperDuper.Entities
{
    class BoxColliderComponent : EntityComponent
    {
        public AABB box;

        public BoxColliderComponent(float width,float height)
        {
            box = new AABB(Vector2.Zero,new Vector2(width,height));
            BoxColliderComponentCache.Register(this);
        }
    }
}
