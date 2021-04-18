using OpenTK.Windowing.Desktop;
using System;
using OpenTK.Mathematics;

namespace GrafKom_C_Sharp
{
    class Program
    {
        static void Main(string[] args)
        {
            // our window setting
            var nativeWindow = new NativeWindowSettings()
            {
                // width , heigth
                Size = new Vector2i(900, 900),
                Title = "GrafKom UTS Project"
            };

            // using keyword to get another class 
            using (var window = new Project_UTS(GameWindowSettings.Default, nativeWindow))
            {
                window.Run();
            }
        }
    }
}
