#if CLIENT
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using SuperDuper.Entities;
using System.Drawing;

namespace SuperDuper
{
    static class Render
    {
        static quadVAO quad = new quadVAO(Common.quad, Common.quad_triangles);
        static quadVAO hollowRect = new quadVAO(Common.quad, Common.hollowRectangle);
        static chunkVAO chunkVAO = new chunkVAO(null);
        static Shader shader = new Shader("Assets/shader.vert", "Assets/shader.frag");
        static Shader solidShader = new Shader("Assets/solid.vert", "Assets/solid.frag");
        static Shader chunkShader = new Shader("Assets/chunk.vert", "Assets/chunk.frag");
        static Shader pointShader = new Shader("Assets/point.vert", "Assets/point.frag");
        static Texture no_texture = new Texture("Assets/missing.png");
        static Texture stoneTexture = new Texture("Assets/stone.png");
        public static bool chunkWireframe = false;
        //static public void DrawEntity(EntityBase entity,float rotation) 
        //{
        //    shader.Use();
        //    Matrix4 model = Matrix4.Identity* Matrix4.CreateScale(entity.width, entity.height, 1) * Matrix4.CreateRotationZ(MathHelper.DegreesToRadians(rotation)) * Matrix4.CreateTranslation(entity.position.X,entity.position.Y,0);
        //    shader.SetMatrix4("model", model);
        //    shader.SetMatrix4("view",Camera.GetViewMatrix());
        //    shader.SetMatrix4("projection",Camera.GetProjectionMatrix());
        //    if (entity.texture != null)
        //    {
        //        GL.ActiveTexture(TextureUnit.Texture0);
        //        GL.BindTexture(TextureTarget.Texture2D,entity.texture);
        //    }
        //    else 
        //    {
        //        no_texture.Use(TextureUnit.Texture0);
        //    }
        //
        //    quad.Use();
        //
        //    GL.DrawElements(PrimitiveType.Triangles,6,DrawElementsType.UnsignedInt,0);
        //}
        static public void DrawSprite(SpriteComponent sprite)
        {
            Transform transform = sprite.entity.GetWorldTransform();
            shader.Use();
            Matrix4 model = Matrix4.Identity * Matrix4.CreateScale(transform.scale.X, transform.scale.Y, 1) * Matrix4.CreateRotationZ(MathHelper.DegreesToRadians(transform.rotation)) * Matrix4.CreateTranslation(transform.position.X, transform.position.Y, 0);
            shader.SetMatrix4("model", model);
            shader.SetMatrix4("view", Camera.GetViewMatrix());
            shader.SetMatrix4("projection", Camera.GetProjectionMatrix());
            if (sprite.texture != null)
            {
                GL.ActiveTexture(TextureUnit.Texture0);
                sprite.texture.Use();
            }
            else
            {
                no_texture.Use(TextureUnit.Texture0);
            }

            quad.Use();

            GL.DrawElements(PrimitiveType.Triangles, 6, DrawElementsType.UnsignedInt, 0);
        }


        static public void DrawCollider(BoxColliderComponent box)
        {
            var color = Color.FromArgb(box.GetHashCode());
            Vector3 rgb = new Vector3(color.R, color.G, color.B);
            Transform transform = box.entity.transform;
            solidShader.Use();
            Matrix4 model = Matrix4.Identity * Matrix4.CreateScale(box.box.size.X, box.box.size.Y, 1) * Matrix4.CreateTranslation(transform.position.X, transform.position.Y, 0);
            solidShader.SetMatrix4("model", model);
            solidShader.SetMatrix4("view", Camera.GetViewMatrix());
            solidShader.SetMatrix4("projection", Camera.GetProjectionMatrix());
            solidShader.SetVector3("boxColor", rgb);

            hollowRect.Use();

            GL.LineWidth(5);
            GL.DrawElements(PrimitiveType.Lines, 8, DrawElementsType.UnsignedInt, 0);
            GL.LineWidth(1);
        }

        static public void DrawRectangle(Vector2 pos, Vector2 size)
        {
            Vector3 rgb = new Vector3(1f, 0f, 0f);
            solidShader.Use();
            Matrix4 model = Matrix4.Identity * Matrix4.CreateScale(size.X, size.Y, 1) * Matrix4.CreateTranslation(pos.X + size.X / 2, pos.Y + size.Y / 2, 0);
            solidShader.SetMatrix4("model", model);
            solidShader.SetMatrix4("view", Camera.GetViewMatrix());
            solidShader.SetMatrix4("projection", Camera.GetProjectionMatrix());
            solidShader.SetVector3("boxColor", rgb);

            hollowRect.Use();

            GL.LineWidth(5);
            GL.DrawElements(PrimitiveType.Lines, 8, DrawElementsType.UnsignedInt, 0);
            GL.LineWidth(1);
        }


        static public void DrawChunk(Chunk chunk)
        {

            chunkVAO.BindData(chunk.vertices);
            chunkVAO.Use();

            chunkShader.Use();
            Matrix4 model = Matrix4.CreateScale(Chunk.tileSize, Chunk.tileSize, 1) * Matrix4.CreateTranslation(Chunk.chunkSize * Chunk.tileSize * chunk.position.X, Chunk.chunkSize * Chunk.tileSize * chunk.position.Y, 0);
            chunkShader.SetMatrix4("model", model);
            chunkShader.SetMatrix4("view", Camera.GetViewMatrix());
            chunkShader.SetMatrix4("projection", Camera.GetProjectionMatrix());
            stoneTexture.Use(TextureUnit.Texture0);
            GL.Enable(EnableCap.ProgramPointSize);
            GL.PointSize(10);
            if (chunkWireframe)
                GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Line);
            GL.DrawArrays(PrimitiveType.Triangles, 0, chunkVAO.GetSize());
            GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Fill);
        }
        public static void DrawPoint(Vector2 point, Vector3 color, int size)
        {
            float[] data = new float[2]
            {
                point.X,
                point.Y,
            };
            int vao;
            int vbo;
            vao = GL.GenVertexArray();
            vbo = GL.GenBuffer();

            GL.BindVertexArray(vao);
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
            GL.BufferData(BufferTarget.ArrayBuffer, 2 * sizeof(float), data, BufferUsageHint.StaticDraw);

            GL.VertexAttribPointer(0, 2, VertexAttribPointerType.Float, false, 2 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);

            GL.Enable(EnableCap.ProgramPointSize);
            GL.PointSize(size);

            pointShader.Use();
            pointShader.SetMatrix4("view", Camera.GetViewMatrix());
            pointShader.SetMatrix4("projection", Camera.GetProjectionMatrix());
            pointShader.SetVector3("pointColor", color);

            GL.DrawArrays(PrimitiveType.Points, 0, 1);

            GL.DeleteVertexArray(vao);
            GL.DeleteBuffer(vbo);
        }
        static public void SetupFrame()
        {
            GL.ClearColor(0f, 0f, 0.4f, 1f);
            GL.Clear(ClearBufferMask.ColorBufferBit);
        }
    }
}
#endif