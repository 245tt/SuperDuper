#if CLIENT
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using SuperDuper.Entities;


namespace SuperDuper
{
    class Window : GameWindow
    {
        public static Vector2i size;
        public static Vector2 mousePosWorldSpace;
        public static Vector2 normalizedMousePos;
        //Chunk chunk;
        World world;// = new World();
        Entity player;// = new Entity("dev");
        HUD hud;
        RigidbodyComponent rb;
        PlayerInput input;
        public Window(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings) : base(gameWindowSettings, nativeWindowSettings)
        {
        }
        protected override void OnLoad()
        {
            Texture man = new Texture("Assets/Textures/man.png");
            Texture rpg = new Texture("Assets/Textures/rpg.png");
            new Texture("Assets/Textures/ItemSlot.png");
            new Texture("Assets/Textures/slotSelector.png");
            new Texture("Assets/Textures/rpg_rocket.png");
            world = new World();
            player = world.entityList[0];
            rb = player.GetComponent<RigidbodyComponent>();
            input = player.GetComponent<PlayerInput>();
            hud = new HUD();
            hud.items = player.GetComponent<InventoryContainerComponent>().container;

            base.OnLoad();
        }
        protected override void OnRenderFrame(FrameEventArgs args)
        {
            Render.SetupFrame();
            //float weaponAngle = MathHelper.RadiansToDegrees( MathF.Atan2(normalizedMousePos.Y,normalizedMousePos.X));

            world.Draw();
            if (world.DebugDraw) 
            {
                world.DrawDebug();
            }

            Render.DrawPoint(mousePosWorldSpace, new Vector3(0,1,1),4);
            hud.Draw();
            Render.DrawText(null,"abcdefg\nABCDEFG",40,Vector2.Zero,Vector3.One);
            SwapBuffers();
            base.OnRenderFrame(args);
        }
        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            input.UpdateMouse(mousePosWorldSpace,normalizedMousePos,MouseState);
            input.Update(world);
            input.Move(KeyboardState,(float)args.Time);
            hud.currentSlot = input.currentSlot;
            hud.Update();
            world.Update((float)args.Time);

            if (KeyboardState.IsKeyPressed(Keys.F3)) 
            {
                world.DebugDraw = !world.DebugDraw;
            }

            if (KeyboardState.IsKeyPressed(Keys.B))
            {
                world.Explode(mousePosWorldSpace,5);
            }

            if (KeyboardState.IsKeyPressed(Keys.F))
                Render.chunkWireframe = !Render.chunkWireframe;

            Camera.Position = player.transform.position;
            float aspectRatio = Window.size.X / (float)Window.size.Y;
            normalizedMousePos = new Vector2();
            normalizedMousePos.X = (2.0f * MouseState.Position.X) / size.X - 1.0f;
            normalizedMousePos.Y = 1.0f - (2.0f * MouseState.Position.Y) / size.Y;
            mousePosWorldSpace.X = normalizedMousePos.X * (Camera.zoom * aspectRatio) / 2 + Camera.Position.X;
            mousePosWorldSpace.Y = normalizedMousePos.Y * Camera.zoom / 2 + Camera.Position.Y;

            base.OnUpdateFrame(args);
        }


        protected override void OnResize(ResizeEventArgs e)
        {
            base.OnResize(e);
            GL.Viewport(0,0,e.Width,e.Height);
            size = new Vector2i(e.Width,e.Height);
        }
    }
}
#endif