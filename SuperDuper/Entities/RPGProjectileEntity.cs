using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperDuper.Entities
{
    class RPGProjectileEntity : ProjectileEntity
    {
        public RPGProjectileEntity(string name, World world, Vector2 direction) : base(name, world, direction)
        {
            this.affectedByGravity = false;
            this.projectileSpeed = 50;
            this.projectileSize = 0.2f;
            Init();
            this.GetComponent<SpriteComponent>().texture = Texture.GetTexture("rpg_rocket");
        }
        public override void OnCollide(World world)
        {
            world.Explode(this.transform.position, 3);
            this.Destroy();
        }

    }
}
