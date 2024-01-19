using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;
using SuperDuper.Items;

namespace SuperDuper.Entities
{
    class PlayerInput : EntityComponent
    {
        InventoryContainerComponent inventory;
        public int currentSlot = 0;

        RigidbodyComponent rb;
        public float moveSpeed = 8;
        public float acceleration = 20;
        public float jumpHeight = 5;
        public float weaponAngle = 0;
        ItemHolderEntity itemHolder;

        public bool hasFired = false;
        Vector2 aimPos;
        public PlayerInput()
        {
            
        }
        public override void Update(World world)
        {
            itemHolder = (ItemHolderEntity)entity.children[0];
            itemHolder.transform.rotation = weaponAngle;
            if (hasFired) 
            {
                if (inventory.container.items[currentSlot].item is ItemGunBase) 
                {
                    ((ItemGunBase)inventory.container.items[currentSlot].item).Shoot(world,entity.transform.position,aimPos);
                    Console.WriteLine("fired");
                }
                hasFired = false;
            }

            if (rb == null)
            {
                rb = this.entity.GetComponent<RigidbodyComponent>();
            }
            if (inventory == null) 
            {
                inventory = this.entity.GetComponent<InventoryContainerComponent>();
               inventory.container.items[0] = new Items.ItemStack(new RPGItem(),1);
            }
            itemHolder.SetItem( inventory.container.items[currentSlot].item);

        }
        public void UpdateMouse(Vector2 mouseWorldPos,Vector2 normalizedMouse,MouseState mouse) 
        {
            weaponAngle = MathHelper.RadiansToDegrees(MathF.Atan2(normalizedMouse.Y, normalizedMouse.X));
            aimPos = mouseWorldPos;
            if (mouse.ScrollDelta.Y >0) 
            {
                currentSlot++;
            }
            if (mouse.ScrollDelta.Y < 0)
            {
                currentSlot--;
            }
            if (currentSlot < 0) currentSlot = 8;
            if (currentSlot > 8) currentSlot = 0;

            if (mouse.IsButtonPressed(MouseButton.Button1)) 
            {
                hasFired = true;
            }
        }
        public void Move(KeyboardState keyboard, float delta)
        {
            if (rb.IsGrounded)
            {
                if (keyboard.IsKeyDown(Keys.Space))
                {
                    rb.velocity.Y = MathF.Sqrt(jumpHeight * -this.entity.world.gravity.Y * 2f);
                }
            }
            rb.isMoving = false;
            if (keyboard.IsKeyDown(Keys.A))
            {
                if (rb.velocity.X > 0)
                {
                    rb.velocity.X = 0;
                }
                float diff = moveSpeed + rb.velocity.X;
                float accel = Math.Min(delta * acceleration, diff);
                rb.velocity.X -= accel;
                rb.isMoving = true;
            }
            if (keyboard.IsKeyDown(Keys.D))
            {
                if (rb.velocity.X < 0)
                {
                    rb.velocity.X = 0;
                }
                float diff = moveSpeed - rb.velocity.X;
                float accel = Math.Min(delta * acceleration, diff);
                rb.velocity.X += accel;
                rb.isMoving = true;
            }
        }
    }
}
