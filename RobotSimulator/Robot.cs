using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotSimulator
{
    public class Robot
    {
        public int X { get; set; }
        public int Y { get; set; }
        public string Facing { get; set; }

        public void Move()
        {
            // Update the coordinates based on the current facing direction
            switch (Facing)
            {
                case "NORTH":
                    Y = Math.Min(Y + 1, 4);
                    break;
                case "SOUTH":
                    Y = Math.Max(Y - 1, 0);
                    break;
                case "EAST":
                    X = Math.Min(X + 1, 4);
                    break;
                case "WEST":
                    X = Math.Max(X - 1, 0);
                    break;
            }
        }

        public void RotateLeft()
        {
            // Rotate the robot 90 degrees to the left
            switch (Facing)
            {
                case "NORTH":
                    Facing = "WEST";
                    break;
                case "SOUTH":
                    Facing = "EAST";
                    break;
                case "EAST":
                    Facing = "NORTH";
                    break;
                case "WEST":
                    Facing = "SOUTH";
                    break;
            }
        }

        public void RotateRight()
        {
            // Rotate the robot 90 degrees to the right
            switch (Facing)
            {
                case "NORTH":
                    Facing = "EAST";
                    break;
                case "SOUTH":
                    Facing = "WEST";
                    break;
                case "EAST":
                    Facing = "SOUTH";
                    break;
                case "WEST":
                    Facing = "NORTH";
                    break;
            }
        }

        public void Report()
        {
            Console.WriteLine($"Output: {X},{Y},{Facing}");
        }
    }
  
}
