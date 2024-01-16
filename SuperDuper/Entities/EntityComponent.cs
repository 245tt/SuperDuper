using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperDuper.Entities
{
    class EntityComponent
    {
        public Entity entity;
        public virtual void Update(World world) { }

    }
}
