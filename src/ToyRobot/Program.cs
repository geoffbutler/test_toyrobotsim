using System;

namespace ToyRobot
{
    class Program
    {
        static void Main(string[] args)
        {
            var toyRobot = new ToyRobotSimulator();

            string command;
            do
            {
                Console.Write(">");
                command = Console.ReadLine();
                if (command != string.Empty)
                    toyRobot.Do(command);
            }
            while (command != string.Empty);            
        }
    }  
}
