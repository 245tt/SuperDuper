using OpenTK.Mathematics;
using SuperDuper.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperDuper.Items
{
    class RPGItem :ItemGunBase
    {
        public RPGItem()
        {
            Label = "rpg";
            Id = 1;
            Description = "Boom and foon";
        }

        public override void Shoot(World world, Vector2 startPos, Vector2 aimPos)
        {
            RPGProjectileEntity rocket = new RPGProjectileEntity("rocket",world,aimPos-startPos);
            rocket.transform.position = startPos;
            world.entityList.Add(rocket);

        }
        public override Texture? GetTexture()
        {
            return Texture.GetTexture("rpg");
        }
    }
}
