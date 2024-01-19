using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperDuper
{
    class textVAO
    {
        int vao;
        int vbo;
        public textVAO() 
        {
            GL.CreateVertexArrays(1,out vao);
            vbo = GL.GenBuffer();

            GL.BindVertexArray(vao);
            GL.BindBuffer(BufferTarget.ArrayBuffer,vbo);

            GL.VertexAttribPointer(0,2,VertexAttribPointerType.Float,false,4* sizeof(float),0 * sizeof(float));
            GL.EnableVertexAttribArray(0);
            GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, 4 * sizeof(float), 2 * sizeof(float));
            GL.EnableVertexAttribArray(1);

            GL.BindVertexArray(0);
            GL.BindBuffer(BufferTarget.ArrayBuffer,0);
        }
        public void BindData(float[] vertices) 
        {
            GL.BindVertexArray(vao);
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);

            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);
        }
        public void Use() 
        {
            GL.BindVertexArray(vao);
        }
    }
}
