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
        public int textureID;
        public int GetTexture()
        {
            return textureID;
        }

    }
}
