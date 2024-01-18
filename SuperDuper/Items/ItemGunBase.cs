using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperDuper.Items
{
    
    abstract class ItemGunBase : ItemBase
    {
        public int currentAmmo;
        public int maxAmmo;
        public ItemAmmoBase ammoType;
        public virtual void Shoot(World world, Vector2 startPos, Vector2 aimPos) { }
        public virtual void Reload() { }
    }
}
