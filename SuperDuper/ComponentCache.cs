using SuperDuper.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperDuper
{
    class ComponentCache<T> where T : EntityComponent
    {
        public static List<T> components = new List<T>();

        public static void Register(T component)
        {
            components.Add(component);
        }

        public static void Update(World world)
        {
            foreach (T component in components)
            {
                component.Update(world);
            }
        }
    }

    class SpriteComponentCache : ComponentCache<SpriteComponent> { }
    class BoxColliderComponentCache : ComponentCache<BoxColliderComponent> { }
    class RigidbodyComponentCache : ComponentCache<RigidbodyComponent> { }
}
