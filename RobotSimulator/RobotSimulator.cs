using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotSimulator
{
    public class RobotSimulator
    {
        private Robot _robot;
        private Table _table;

        public RobotSimulator()
        {
            _robot = new Robot();
            _table = new Table();
        }

        public void ProcessCommands(string filePath)
        {
            string[] lines = File.ReadAllLines(filePath);

            foreach (string line in lines)
            {
                Command command = ParseCommand(line);

                if (command == null)
                    continue; // Ignore invalid commands

                ExecuteCommand(command);
            }
        }

        private Command ParseCommand(string line)
        {
            string[] parts = line.Split(' ');

            if (parts.Length == 0)
                return null;

            Command command = new Command { Type = parts[0] };

            if (command.Type == "PLACE" && parts.Length == 2)
            {
                string[] position = parts[1].Split(',');

                if (position.Length == 3 &&
                    int.TryParse(position[0], out int x) &&
                    int.TryParse(position[1], out int y))
                {
                    command.X = x;
                    command.Y = y;
                    command.Facing = position[2];
                }
                else
                {
                    return null; // Invalid PLACE command syntax
                }
            }

            return command;
        }

        private void ExecuteCommand(Command command)
        {
            switch (command.Type)
            {
                case "PLACE":
                    PlaceRobot(command.X, command.Y, command.Facing);
                    break;
                case "MOVE":
                    MoveRobot();
                    break;
                case "LEFT":
                    RotateRobotLeft();
                    break;
                case "RIGHT":
                    RotateRobotRight();
                    break;
                case "REPORT":
                    ReportRobot();
                    break;
            }
        }

        private void PlaceRobot(int x, int y, string facing)
        {
            if (_table.IsValidPosition(x, y))
            {
                _robot.X = x;
                _robot.Y = y;
                _robot.Facing = facing;
            }
        }

        private void MoveRobot()
        {
            if (_table.IsValidPosition(_robot.X, _robot.Y))
            {
                _robot.Move();
            }
        }

        private void RotateRobotLeft()
        {
            _robot.RotateLeft();
        }

        private void RotateRobotRight()
        {
            _robot.RotateRight();
        }

        private void ReportRobot()
        {
            _robot.Report();
        }
    }
}
