using OpenTK.Graphics.OpenGL4;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;
using PixelFormat = OpenTK.Graphics.OpenGL4.PixelFormat;
using System.Xml.Linq;

namespace SuperDuper
{
    public class Texture
    {
        public readonly int Handle;
        public readonly int Width;
        public readonly int Height;
        public readonly string name;

        private static List<Texture> texturesRegistry = new List<Texture>();
        public static Texture GetTexture(string name) 
        {
            foreach (Texture tex in texturesRegistry)
            {
                if (tex.name == name) return tex;
            }
            return null;
        }


        // Create texture from path.
        public Texture(string path)
        {
            // Generate handle
            Handle = GL.GenTexture();

            // Bind the handle
            Use();

            // For this example, we're going to use .NET's built-in System.Drawing library to load textures.

            // Load the image
            using (var image = new Bitmap(path))
            {

                image.RotateFlip(RotateFlipType.RotateNoneFlipY);
                var data = image.LockBits(
                    new Rectangle(0, 0, image.Width, image.Height),
                    ImageLockMode.ReadOnly,
                    System.Drawing.Imaging.PixelFormat.Format32bppArgb);

                this.Width = data.Width;
                this.Height = data.Height;
                this.name = Path.GetFileNameWithoutExtension(path);
                GL.TexImage2D(TextureTarget.Texture2D,
                    0,
                    PixelInternalFormat.Rgba,
                    image.Width,
                    image.Height,
                    0,
                    PixelFormat.Bgra,
                    PixelType.UnsignedByte,
                    data.Scan0);
            }
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Nearest);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);
            GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);

            //anisotropic filtering
            float[] aniso = new float[] { 0.0f };
            GL.GetFloat(GetPName.MaxTextureMaxAnisotropy,aniso);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMaxAnisotropy, aniso[0]);

            texturesRegistry.Add(this);
        }
        
        public void Use(TextureUnit unit = TextureUnit.Texture0)
        {
            GL.ActiveTexture(unit);
            GL.BindTexture(TextureTarget.Texture2D, Handle);
        }
    }
}
