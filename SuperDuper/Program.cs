using OpenTK.Windowing.Desktop;
using OpenTK.Mathematics;
namespace SuperDuper
{
    internal class Program
    {
        static void Main(string[] args)
        {

            GameWindowSettings gameWindow = new GameWindowSettings()
            {
                UpdateFrequency = 144,
            };
            NativeWindowSettings nativeWindow = new NativeWindowSettings()
            {
                ClientSize = new Vector2i(1600,900),
                
            };
            
            Window window = new Window(gameWindow,nativeWindow);
            window.Run();
        }
    }
}