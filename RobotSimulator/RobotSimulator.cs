using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotSimulator
{
    public class RobotSimulator
    {
        private IRobot _robot;
        private ITabletop _tabletop;

        public RobotSimulator(IRobot robot, ITabletop tabletop)
        {
            _robot = robot;
            _tabletop = tabletop;
        }

        public void ProcessCommands(string filePath)
        {
            string[] lines = File.ReadAllLines(filePath);
            var cmds = new List<CommandModel>();

            for (int i = 0; i < lines.Length; i++)
            {
                var place = lines[i].Split(' ');
                if (place[0] != "PLACE" && lines[i].Split(',').Count() > 1)
                {
                    var j = i;
                    while (!string.IsNullOrEmpty(lines[j]))
                    {
                        var cmd = ParseCommand(lines[j]);
                        cmd.ValidCommand = false;
                        if (!string.IsNullOrEmpty(lines[j]))
                        {
                            cmds.Add(cmd);
                        }
                        j++;
                        i = j;
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(lines[i]))
                    {
                        var c = ParseCommand(lines[i]);
                        c.ValidCommand = true;
                        cmds.Add(c);
                    }
                    
                }
            }

            foreach (var cmd in cmds)
            {

                if (cmd.ValidCommand)
                {
                    ExecuteCommand(cmd);
                }
                else
                {
                    if (!cmd.ValidCommand && cmd.Type != "LEFT"  && cmd.Type != "RIGHT" && cmd.Type != "MOVE" && cmd.Type != "REPORT")
                    {
                        Console.WriteLine($"Invalid command {cmd.Type} {cmd.X} {cmd.Y} {cmd.Facing} sequence. PLACE command must be executed first.");
                    }
                }
            }
        }

        private CommandModel ParseCommand(string line)
        {
            string[] parts = line.Split(' ');

            if (parts.Length == 0)
                return null;

            CommandModel command = new CommandModel { Type = parts[0] };

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

        private void ExecuteCommand(CommandModel command)
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
            if (_tabletop.IsValidPosition(x, y))
            {
                _robot.SetX(x);
                _robot.SetY(y);
                _robot.SetFacing( facing); 
            }
        }

        private void MoveRobot()
        {
            if (_tabletop.IsValidPosition(_robot.GetX(), _robot.GetY()))
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
