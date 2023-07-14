﻿using System;
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
            var commandBlocks = ConvertCommandFileToCommandBlocksModel(filePath);

            foreach (var cmdBlock in commandBlocks)
            {
                var isValidCommand = false;

                foreach (var command in cmdBlock.Commands) 
                {
                    if (command.Type != "PLACE" && !isValidCommand)
                    {
                        if (!string.IsNullOrEmpty(command.Type))
                        {
                            Console.WriteLine($"Invalid command {command.Type}. PLACE command must be executed first.");
                        }
                        else
                        {
                            Console.WriteLine($"PLACE command was empty.");
                        }
                        break;
                    }
                    else
                    {
                        isValidCommand = true;
                        ExecuteCommand(command);
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

        private List<CommandBlock> ConvertCommandFileToCommandBlocksModel(string fileName)
        {
            string line = "";
            var str = "";

            using (StreamWriter writer = new StreamWriter(fileName, true))
            {
                writer.WriteLine(); // Append a line break at the bottom of the file
                writer.WriteLine();
            }

            StreamReader file = new StreamReader(fileName);
            var cmdList = new List<CommandBlock>();

            while ((line = file.ReadLine()) != null)
            {
                line = line.Trim();
                //if line is empty
                //show total values until now
                if (line == "")
                {
                    var cmdBlock = new CommandBlock();
                    if (!string.IsNullOrEmpty(str))
                    {
                        var cmds = str.Split(';');
                        foreach (var cmd in cmds)
                        {
                            cmdBlock.Commands.Add(ParseCommand(cmd));

                        }
                        cmdList.Add(cmdBlock);
                    }
                    str = "";
                    continue;
                }
                else
                {
                    //if first element, add here
                    if (str != "")
                    {
                        str = str + ";" + line;
                    }
                    else
                    {
                        str = line;
                    }

                }
            }

            file.Close();

            return cmdList;
        }
    }
}
