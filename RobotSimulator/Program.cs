namespace RobotSimulator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            RobotSimulator simulator = new RobotSimulator();
            string filePath = "commands.txt"; // Path to the commands file

            simulator.ProcessCommands(filePath);
        }
    }
}