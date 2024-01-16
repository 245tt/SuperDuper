using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperDuper.Entities
{
    class Entity
    {
        public Guid guid;
        public string name;
        public List<EntityComponent> components= new List<EntityComponent>();
        public World world;
        public List<Entity> children = new List<Entity>();
        public Entity parent = null;

        public float width = 1;
        public float height = 1;

        public Transform transform = new Transform();

        public Entity(string name,World world)
        {
            this.name = name;
            this.world = world;
            guid = Guid.NewGuid();
        }
        public void AddComponent(EntityComponent component)
        {
            components.Add(component);
            component.entity = this;
        }
        public T GetComponent<T>() where T : EntityComponent
        {
            foreach (EntityComponent component in components)
            {
                if (component.GetType().Equals(typeof(T)))
                {
                    return (T)component;
                }
            }
            return null;
        }
        public Transform GetWorldTransform() 
        {
            if (parent == null) //check if not child
            {
                return transform;
            }
            else 
            {
                return transform + parent.GetWorldTransform();
            }
        }
    }
}
