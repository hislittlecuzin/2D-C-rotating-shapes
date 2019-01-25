using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics;

namespace TKMapTool
{
    class Program
    {
        static void Main(string[] args)
        {
            using (Game game = new Game(800, 600, "Map Tool"))
            {
                game.Run();
            }
            //Console.ReadLine();
        }
    }
}
