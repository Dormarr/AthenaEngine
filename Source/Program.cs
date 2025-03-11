using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Input;
using OpenTK.Mathematics;
using AthenaEngine.Source.Utility;
using System;
using Vector3 = AthenaEngine.Source.Utility.Vector3;

namespace AthenaEngine.Source
{
    public static class Program
    {
        private static Window _window;

        static void Main()
        {
            _window = new Window(800, 600, "Athena Engine");
            _window.Run();
        }
    }
}
