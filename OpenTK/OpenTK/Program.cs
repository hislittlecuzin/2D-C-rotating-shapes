using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace OpenTK
{
    class Program
    {
        static void Main(string[] args)
        {
            GameWindow window = new GameWindow(800,600);
            Game game = new Game(window);
        }
    }
}
