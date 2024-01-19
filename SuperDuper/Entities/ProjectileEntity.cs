using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperDuper.Entities
{
    class ProjectileEntity : Entity
    {
        protected float projectileSize;
        protected float projectileSpeed;
        Vector2 direction;
        public bool affectedByGravity = false;

        public ProjectileEntity(string name, World world, Vector2 direction) : base(name, world)
        {
            this.direction = direction;
        }
        protected virtual void Init() 
        {
            RigidbodyComponent rb = new RigidbodyComponent();
            rb.AffectedByGravity = affectedByGravity;
            rb.velocity = direction.Normalized() * projectileSpeed;
            this.transform.rotation = MathHelper.RadiansToDegrees(MathF.Atan2(direction.Y, direction.X));
            AddComponent(rb);
            AddComponent(new BoxColliderComponent(projectileSize, projectileSize));
            AddComponent(new SpriteComponent(null));
        }
    }
}
