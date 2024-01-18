using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperDuper.Entities
{
    class EntityComponent : IDisposable
    {
        public Entity entity;

        public void Dispose()
        {
            DisposeComponent();   
        }
        public virtual void DisposeComponent() { }
        public virtual void Update(World world) { }

    }
}
