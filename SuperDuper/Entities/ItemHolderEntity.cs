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
        SpriteComponent sprite;
        public ItemHolderEntity(string name, World world) : base(name, world)
        {
            this.transform.scale = new Vector2(4,4);
            sprite = new SpriteComponent(null);
            AddComponent(sprite);
        }

        public void SetItem(ItemBase item) 
        {
            if (item != null)
            {
                this.displayItem = item;
                sprite.texture = item.GetTexture();
            }
        }
    }
}
