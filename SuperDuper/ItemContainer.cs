using SuperDuper.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperDuper
{
    class ItemContainer
    {
        public List<ItemStack> items = new List<ItemStack>();
        public ItemContainer(int x,int y)
        {
            for (int i = 0; i < x; i++) 
            {
                for (int j = 0; j < y; j++) 
                {
                    items.Add(new ItemStack(null,0));
                }
            }
        }
    }
    
}
