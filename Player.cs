using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Zomb
{
    internal static class Player
    {
        internal static int Health = 100;
        // 1: Up, 0: Down, -1: Left, 2: Right
        internal static Direction Direction = Direction.Up;
        internal static int Speed = 10;
        internal static int Ammunition = 10;
        internal static bool Move = false;
        internal static ushort Score = 0;
        internal static PictureBox box;

        internal static bool IsAlive { get { return Health > 0; } }
    }
}
