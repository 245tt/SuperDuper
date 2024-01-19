using OpenTK.Mathematics;

namespace SuperDuper
{
    static class Camera
    {
        static public Vector2 Position = Vector2.Zero;
        public static float zoom = 30;
        
        static public Matrix4 GetProjectionMatrix()
        {
            zoom = MathF.Max(0.5f,zoom);
            float aspectRatio = Window.size.X / (float)Window.size.Y;
            return Matrix4.CreateOrthographic(zoom * aspectRatio, zoom, 0.1f, 10f);
            //return Matrix4.CreateOrthographicOffCenter(-zoom * aspectRatio, zoom * aspectRatio, -zoom,zoom,0.1f,10f);
        }
        static public Matrix4 GetViewMatrix() 
        {
            //return Matrix4.CreateTranslation(Position.X,Position.Y,-1);
            return Matrix4.LookAt(new Vector3(Position.X, Position.Y, 1f),new Vector3(Position.X, Position.Y, -1f), Vector3.UnitY);
        }
    }
}
