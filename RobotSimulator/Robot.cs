using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotSimulator
{
    public interface IRobot
    {
        void Move();
        void RotateLeft();
        void RotateRight();
        void Report();
        int GetY();
        int GetX();
        void SetX(int x);
        void SetY(int y);
        void SetFacing(string facing);
    }

    public class Robot: IRobot
    {
        private int _x, _y;
        private string _facing;

        public Robot(int x, int y, string facing)
        {
            _x = x;
            _y = y;
            _facing = facing;
        }

        public void Move()
        {
            // Update the coordinates based on the current facing direction
            switch (_facing)
            {
                case "NORTH":
                    _y = Math.Min(_y + 1, 4);
                    break;
                case "SOUTH":
                    _y = Math.Max(_y - 1, 0);
                    break;
                case "EAST":
                    _x = Math.Min(_x + 1, 4);
                    break;
                case "WEST":
                    _x = Math.Max(_x - 1, 0);
                    break;
            }
        }

        public void RotateLeft()
        {
            // Rotate the robot 90 degrees to the left
            switch (_facing)
            {
                case "NORTH":
                    _facing = "WEST";
                    break;
                case "SOUTH":
                    _facing = "EAST";
                    break;
                case "EAST":
                    _facing = "NORTH";
                    break;
                case "WEST":
                    _facing = "SOUTH";
                    break;
            }
        }

        public void RotateRight()
        {
            // Rotate the robot 90 degrees to the right
            switch (_facing)
            {
                case "NORTH":
                    _facing = "EAST";
                    break;
                case "SOUTH":
                    _facing = "WEST";
                    break;
                case "EAST":
                    _facing = "SOUTH";
                    break;
                case "WEST":
                    _facing = "NORTH";
                    break;
            }
        }

        public void Report()
        {
            Console.WriteLine($"Output: {_x},{_y},{_facing}");
        }

        public int GetX()
        {
            return _x;
        }

        public int GetY()
        {
            return _y;
        }

        public void SetX(int x)
        {
            _x = x;
        }

        public void SetY(int y)
        {
            _y = y;
        }
        public void SetFacing(string facing)
        {
            _facing=facing;
        }
    }
  
}
