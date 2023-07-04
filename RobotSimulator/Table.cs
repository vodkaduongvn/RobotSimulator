using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotSimulator
{
    public class Table
    {
        public bool IsValidPosition(int x, int y)
        {
            // Check if the position is within the tabletop boundaries
            return x >= 0 && x <= 4 && y >= 0 && y <= 4;
        }
    }
}
