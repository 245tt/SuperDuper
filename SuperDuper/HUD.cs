using OpenTK.Mathematics;
using SuperDuper.Items;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperDuper
{
    class HUD
    {
        public ItemContainer items;
        public float health;
        public int currentSlot = 0;
        List<HUDItemSlot> itemSlots = new List<HUDItemSlot>();

        float marginLeft = 0.1f;
        float marginBottom = 0.05f;
        float marginRight = 0.05f;
        float slotSize = 0.3f;
        int numberOfToolbarSlots = 9;
        public HUD() 
        {
            
            float distance = (1-marginLeft-(-1+marginRight))/(float)numberOfToolbarSlots;
            for (int i = 0; i < numberOfToolbarSlots; i++) 
            {
                HUDItemSlot slot = new HUDItemSlot();
                slot.position = new Vector2(-1 + slotSize/2 + distance*i,-1+slotSize/2+ marginBottom);
                slot.size = slotSize;
                itemSlots.Add(slot);
            }
        }
        public void Update() 
        {
            for (int i = 0; i < numberOfToolbarSlots; i++)
            {
                itemSlots[i].item = items.items[i].item;
            }
        }
        public void Draw() 
        {
            for (int i = 0; i < itemSlots.Count; i++) 
            {
                itemSlots[i].Draw();
            }

            float distance = (1 - marginLeft - (-1 + marginRight)) / (float)numberOfToolbarSlots;
            float selectorSize = slotSize + 0.02f;
            Render.DrawHUDTexture(new Vector2(-1 + slotSize / 2 + distance * currentSlot, -1 + slotSize / 2 + marginBottom), new Vector2(selectorSize / (Window.size.X / (float)Window.size.Y), selectorSize), Texture.GetTexture("slotSelector"));
        }
    }
    class HUDItemSlot 
    {
        public Vector2 position = Vector2.Zero;
        public float size = 0.3f;
        public ItemBase item;
        public HUDItemSlot() { }
        public void Draw() 
        {
            Render.DrawHUDTexture(position, new Vector2(size / (Window.size.X / (float)Window.size.Y),size),Texture.GetTexture("ItemSlot"));
            if (item != null) 
            {
                Render.DrawHUDTexture(position, new Vector2(size / (Window.size.X / (float)Window.size.Y), size), Texture.GetTexture(item.Label));
            }
        }
    }
}
