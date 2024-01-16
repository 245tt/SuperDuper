using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperDuper
{
    static class Common
    {
        public static float[] quad = new float[]
        {
            -0.5f,-0.5f,0.0f,0.0f,
            -0.5f,0.5f,0.0f,1.0f,
            0.5f,-0.5f,1.0f,0.0f,
            0.5f,0.5f,1.0f,1.0f,
        };
        public static uint[] quad_triangles = new uint[]
        {
            0,1,2,
            1,2,3
        };

        public static uint[] hollowRectangle = new uint[] 
        {
            0,1,
            0,2,
            1,3,
            2,3
        };
    }
}
