using OpenTK.Mathematics;
using SuperDuper.Items;

namespace SuperDuper.Entities
{
    class ItemHolderEntity : Entity
    {
        public ItemBase displayItem;
        SpriteComponent sprite;
        public ItemHolderEntity(string name, World world) : base(name, world)
        {
            this.transform.scale = new Vector2(4, 4);
            sprite = new SpriteComponent(null);
            AddComponent(sprite);
        }

        public void SetItem(ItemBase item)
        {
            this.displayItem = item;
            if (item != null)
            {
                sprite.active = true;
                sprite.texture = item.GetTexture();
            }
            else
            {
                sprite.active = false;
                sprite.texture = null;
            }

        }
    }
}
