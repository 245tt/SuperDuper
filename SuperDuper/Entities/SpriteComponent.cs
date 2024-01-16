using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace SuperDuper.Entities
{
    class SpriteComponent : EntityComponent
    {
        public Texture texture;
        //public Vector2 spriteSize;
        public SpriteComponent(Texture texture) 
        {
            this.texture = texture;
            SpriteComponentCache.Register(this);
        }
    }
}
