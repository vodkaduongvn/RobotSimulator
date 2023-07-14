namespace RobotSimulator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            RobotSimulator simulator = new RobotSimulator(new Robot(0, 0, "NORTH"), new Tabletop());
            string filePath = "commands.txt"; // Path to the commands file

            simulator.ProcessCommands(filePath);

            Thread.Sleep(100000);
        }
    }
}