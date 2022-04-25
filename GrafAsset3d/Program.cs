using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LearnOpenTK.Common;
using OpenTK.Windowing.Desktop;
namespace GrafAsset3d
{
    internal class Program
    {
        static void Main(string[] args)
        {

            var nativeWindowSettings = new NativeWindowSettings()
            {
                Size = new OpenTK.Mathematics.Vector2i(1920, 1080),
                Title = "ConsoleApp1"
            };
            using(var window = new Window3d(GameWindowSettings.Default,nativeWindowSettings))
            {
                window.Run();
            }
        }
    }
}
