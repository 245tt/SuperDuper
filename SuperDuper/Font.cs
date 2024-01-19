using OpenTK.Mathematics;
using System.Data;

namespace SuperDuper
{
    class LetterData
    {
        public int x;
        public int y;
        public int width;
        public int height;
        public int xoffset;
        public int yoffset;
        public int xadvance;

        public LetterData(int x, int y, int width, int height, int xoffset, int yoffset, int xadvance)
        {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
            this.xoffset = xoffset;
            this.yoffset = yoffset;
            this.xadvance = xadvance;
        }
    }
    class Font
    {
        public Texture textureAtlas;
        Dictionary<char, LetterData> letterData = new Dictionary<char, LetterData>();
        int baseSize;
        int lineSize;
        int padding;
        int spacing;
        int width;
        int height;
        public Font(string textureAtlasPath, string fontDataPath)
        {
            textureAtlas = new Texture(textureAtlasPath);
            height = textureAtlas.Height;
            width= textureAtlas.Width;
            string[] lines = File.ReadAllLines(fontDataPath);
            for (int i = 0; i < lines.Length; i++)
            {
                var parsedData = ReadData(lines[i]);
                if (parsedData.ElementAt(0).Key == "info")
                {
                    lineSize = int.Parse(parsedData.ElementAt(2).Value);
                    padding = int.Parse(parsedData.ElementAt(10).Value.ToCharArray(0,1));
                    spacing = int.Parse(parsedData.ElementAt(11).Value.ToCharArray(0, 1));
                }else
                if (parsedData.ElementAt(0).Key == "common") 
                {
                    baseSize = int.Parse(parsedData.ElementAt(2).Value);
                    width = int.Parse(parsedData.ElementAt(3).Value);
                    height = int.Parse(parsedData.ElementAt(4).Value);
                }else
                if (parsedData.ElementAt(0).Key == "char") 
                {
                    char character = (char)int.Parse(parsedData.ElementAt(1).Value);
                    int xpos = int.Parse(parsedData.ElementAt(2).Value);
                    int ypos = int.Parse(parsedData.ElementAt(3).Value);
                    int charWidth = int.Parse(parsedData.ElementAt(4).Value);
                    int charHeight = int.Parse(parsedData.ElementAt(5).Value);
                    int xoff = int.Parse(parsedData.ElementAt(6).Value);
                    int yoff = int.Parse(parsedData.ElementAt(7).Value);
                    int advance = int.Parse(parsedData.ElementAt(8).Value);
                    letterData.Add(character, new LetterData(xpos,ypos,charWidth,charHeight,xoff,yoff,advance));
                }
            }

        }
        public float[] GenerateText(string text,Vector2 pos,int sizePixels) 
        {
            List<float> textData = new List<float>();
            int advancePixels = 0;
            int advanceYPixels = 0;
            float scale =  sizePixels/(float)baseSize;
            foreach (char letter in text)
            {
                if (letter == '\n') 
                {
                    advancePixels = 0;
                    advanceYPixels += lineSize;
                    continue;
                }
                LetterData data = letterData[letter];
                Vector2 charSize = pixelPosToTexPos(new Vector2i(data.width,data.height)) * scale;
                Vector2 charPos = pixelPosToTexPos(new Vector2i(data.xoffset+(int)(advancePixels*scale),data.yoffset+ (int)(advanceYPixels * scale)))+pos;
                Vector2 texStart = pixelPosToTexPos(new Vector2i(data.x,height - data.y));
                Vector2 texEnd = pixelPosToTexPos(new Vector2i(data.x+data.width,height- (data.y+data.height)));

                //first triangle
                //top left
                textData.Add(charPos.X);
                textData.Add(charPos.Y+charSize.Y);
                textData.Add(texStart.X);
                textData.Add(texStart.Y);

                //top right
                textData.Add(charPos.X+charSize.X);
                textData.Add(charPos.Y + charSize.Y);
                textData.Add(texEnd.X);
                textData.Add(texStart.Y);

                //bottom left
                textData.Add(charPos.X);
                textData.Add(charPos.Y);
                textData.Add(texStart.X);
                textData.Add(texEnd.Y);

                //second triangle
                //top right
                textData.Add(charPos.X + charSize.X);
                textData.Add(charPos.Y + charSize.Y);
                textData.Add(texEnd.X);
                textData.Add(texStart.Y);

                //bottom left
                textData.Add(charPos.X);
                textData.Add(charPos.Y);
                textData.Add(texStart.X);
                textData.Add(texEnd.Y);

                //bottom right
                textData.Add(charPos.X + charSize.X);
                textData.Add(charPos.Y);
                textData.Add(texEnd.X);
                textData.Add(texEnd.Y);


                advancePixels += data.xadvance;
            }
            return textData.ToArray();
        }

        private static Dictionary<string, string> ReadData(string line)
        {
            Dictionary<string, string> keyData = new Dictionary<string, string>();
            string[] data = line.Split(' ');
            for (int i = 0; i < data.Length; i++)
            {
                string[] data2 = data[i].Split('=');
                if (data2.Length == 2)
                    keyData.Add(data2[0], data2[1]);
                else keyData.Add(data2[0], string.Empty);
            }

            return keyData;
        }
        private Vector2 pixelPosToTexPos(Vector2i pixel) 
        {
            return new Vector2(pixel.X /(float)width,pixel.Y / (float)height);
        }
    }
}
