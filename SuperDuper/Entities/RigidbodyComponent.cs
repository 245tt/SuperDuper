using OpenTK.Mathematics;

namespace SuperDuper.Entities
{

    class RigidbodyComponent : EntityComponent
    {
        public bool AffectedByGravity = true;
        public bool IsGrounded = true;
        public bool isMoving = false;
        public float friction = 0.9f;
        public Vector2 velocity = Vector2.Zero;

        public RigidbodyComponent()
        {
            RigidbodyComponentCache.Register(this);
        }

        public override void DisposeComponent()
        {
            RigidbodyComponentCache.Unregister(this);
        }

        public override void Update(World world)
        {
            if (AffectedByGravity)
            {
                velocity += world.gravity * world.deltaTime;
            }
            if (IsGrounded)
            {
                if (!isMoving)
                {
                    velocity.X *= (1.0f - friction * -world.gravity.Y * world.deltaTime);
                    if (Math.Abs(velocity.X) <= 0.1f)
                    {
                        velocity.X = 0;
                    }
                }
            }
        }
    }
}
