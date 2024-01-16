using OpenTK.Mathematics;
using SuperDuper.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperDuper.Entities
{
    class ItemHolderEntity : Entity
    {
        public ItemBase displayItem;
        public ItemHolderEntity(string name, World world) : base(name, world)
        {
            this.transform.scale = new Vector2(4,4);
            AddComponent(new SpriteComponent(Texture.GetTexture("rpg")));
        }

        public void SetItem(ItemBase item) 
        {
            this.displayItem = item;
        }
    }
}
