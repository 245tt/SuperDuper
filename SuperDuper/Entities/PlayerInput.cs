using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace SuperDuper.Entities
{
    class PlayerInput : EntityComponent
    {
        RigidbodyComponent rb;
        public float moveSpeed = 8;
        public float acceleration = 20;
        public float jumpHeight = 5;
        public float weaponAngle = 0;
        ItemHolderEntity itemHolder;
        public PlayerInput()
        {

        }
        public override void Update(World world)
        {
            itemHolder = (ItemHolderEntity)entity.children[0];
            itemHolder.transform.rotation = weaponAngle;
            itemHolder.SetItem(null);
        }
        public void UpdateMouse(Vector2 normalizedMouse) 
        {
            weaponAngle = MathHelper.RadiansToDegrees(MathF.Atan2(normalizedMouse.Y, normalizedMouse.X));
        }
        public void Move(KeyboardState keyboard, float delta)
        {

            if (rb == null)
            {
                rb = this.entity.GetComponent<RigidbodyComponent>();
            }
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
