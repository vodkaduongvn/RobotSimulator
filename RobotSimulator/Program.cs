namespace RobotSimulator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            RobotSimulator simulator = new RobotSimulator(new Robot(), new Tabletop());
            string filePath = "commands.txt"; // Path to the commands file

            simulator.ProcessCommands(filePath);

            Thread.Sleep(30000);
        }
    }
}