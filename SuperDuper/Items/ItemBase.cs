using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace SuperDuper.Items
{
    abstract class ItemBase
    {
        public int Id;
        public string Label;
        public string Description;
        public bool stackable;
        public short stackSize;
        public virtual Texture? GetTexture() { return null; }
        public virtual void Update(World world) { }
    }
    class ItemStack
    {
        public ItemBase? item;
        public int stackSize;
        public ItemStack(ItemBase? item, int stackSize)
        {
            this.item = item;
            this.stackSize = stackSize;
        }

        public string GetLabel()
        {
            return item.Label;
        }
        public int GetMaxStackSize()
        {
            return item.stackSize;
        }
        public Type GetItemType() 
        {
            return item.GetType();
        }
    }
}
