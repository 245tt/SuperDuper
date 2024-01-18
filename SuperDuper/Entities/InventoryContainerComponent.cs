using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperDuper.Entities
{
    class InventoryContainerComponent : EntityComponent
    {
        public ItemContainer container = new ItemContainer(9,1); 
        public InventoryContainerComponent()
        {
        }
        public override void Update(World world)
        {
            foreach (var itemStack in container.items)
            {
                itemStack.item.Update(world);
            } 
        }
    }
}
