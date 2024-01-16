using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperDuper
{
    internal class chunkVAO : IDisposable
    {
        int vao;
        int vbo;
        int size = 0;
        //int ebo;
        public chunkVAO(float[] vertices) 
        {

            GL.CreateVertexArrays(1,out vao);
            vbo = GL.GenBuffer();
            //ebo = GL.GenBuffer();

            GL.BindVertexArray(vao);
            GL.BindBuffer(BufferTarget.ArrayBuffer,vbo);
            //GL.BindBuffer(BufferTarget.ElementArrayBuffer,ebo);

            if (vertices != null)
            {
                size = vertices.Length;
                GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);
            }
            //GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Length * sizeof(uint), indices, BufferUsageHint.StaticDraw);

            GL.VertexAttribPointer(0,2,VertexAttribPointerType.Float,false,2* sizeof(float),0 * sizeof(float));
            GL.EnableVertexAttribArray(0);
            //GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, 4 * sizeof(float), 2 * sizeof(float));
            //GL.EnableVertexAttribArray(1);

            GL.BindVertexArray(0);
            GL.BindBuffer(BufferTarget.ArrayBuffer,0);
        }
        public void BindData(float[] vertices) 
        {
            size = vertices.Length;

            GL.BindVertexArray(vao);
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);

            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);

            GL.BindVertexArray(0);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
        }

        public void Use() 
        {
            GL.BindVertexArray(vao);
        }
        public int GetSize() 
        {
            return size;
        }

        public void Dispose()
        {
           GL.DeleteBuffer(vbo);
           GL.DeleteVertexArray(vao);
        }
    }
}
