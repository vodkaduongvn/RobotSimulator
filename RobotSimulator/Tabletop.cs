﻿namespace RobotSimulator
{
    public interface ITabletop
    {
        bool IsValidPosition(int x, int y);
    }

    public class Tabletop:ITabletop
    {
        public bool IsValidPosition(int x, int y)
        {
            // Check if the position is within the tabletop boundaries
            return x >= 0 && x <= 4 && y >= 0 && y <= 4;
        }
    }
}
