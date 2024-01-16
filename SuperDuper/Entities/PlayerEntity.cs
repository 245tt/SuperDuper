using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperDuper.Entities
{
    internal class PlayerEntity : Entity
    {
        public PlayerEntity(string name,World world) : base(name,world)
        {
            this.transform.scale = new Vector2(3,6);
            this.transform.position = new Vector2(0,35);

            this.AddComponent(new SpriteComponent(Texture.GetTexture("man")));
            this.AddComponent(new RigidbodyComponent());
            this.AddComponent(new PlayerInput());
            this.AddComponent(new BoxColliderComponent(2.9f, 5.9f));

            //weapon entity
            ItemHolderEntity holder = new ItemHolderEntity("item_holder", world);
            this.children.Add(holder);
            holder.parent = this;

        }
    }
}
