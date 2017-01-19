using System;
using System.Linq;

namespace ToyRobot
{
    public class ToyRobotSimulator
    {        
        public bool Placed { get; private set; }
        public int X { get; private set; }
        public int Y { get; private set; }
        public string Facing { get; private set; }
        public string LastReport { get; private set; }


        public void Do(string commandText)
        {
            string[] commands = commandText.Split(Constants.Commands.CommandsDelim, StringSplitOptions.RemoveEmptyEntries);

            if (commands.Length == 2 && commands[0] == Constants.Commands.Place)
            {
                Place(commands);
                return;
            }

            if (!Placed)
                return; // must be placed first

            if (commands.Length != 1)
                return; // can't be valid

            if (commands[0] == Constants.Commands.Report)
            {
                Report();
            }
            else if (commands[0] == Constants.Commands.Move)
            {
                Move();
            }
            else if (Constants.ValidTurns.Contains(commands[0]))
            {
                Turn(commands[0]);
            }
        }


        private void Turn(string direction)
        {
            switch (direction)
            {
                case Constants.Turns.Left:
                    TurnLeft();
                    break;
                case Constants.Turns.Right:
                    TurnRight();
                    break;
            }
        }

        private void TurnLeft()
        {
            switch (Facing)
            {
                case Constants.Facing.North:
                    Facing = Constants.Facing.West;
                    break;
                case Constants.Facing.West:
                    Facing = Constants.Facing.South;
                    break;
                case Constants.Facing.South:
                    Facing = Constants.Facing.East;
                    break;
                case Constants.Facing.East:
                    Facing = Constants.Facing.North;
                    break;
            }
        }

        private void TurnRight()
        {
            switch (Facing)
            {
                case Constants.Facing.North:
                    Facing = Constants.Facing.East;
                    break;
                case Constants.Facing.East:
                    Facing = Constants.Facing.South;
                    break;
                case Constants.Facing.South:
                    Facing = Constants.Facing.West;
                    break;
                case Constants.Facing.West:
                    Facing = Constants.Facing.North;
                    break;
            }
        }

        private void Report()
        {
            var message = string.Format(Constants.Commands.PlaceParamsFormat, X, Y, Facing);
            Console.WriteLine(message);
            LastReport = message;
        }

        private void Place(string[] commands)
        {
            var parts = commands[1].Split(Constants.Commands.PlaceParamsDelim, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length != 3)
                return; // ignore invalid input

            var inputX = parts[0];
            var inputY = parts[1];
            var inputFacing = parts[2];

            // ignore invalid input
            int x, y;
            if (!int.TryParse(inputX, out x))
                return;
            if (!int.TryParse(inputY, out y))
                return;
            if (!Constants.ValidFacing.Contains(inputFacing))
                return;

            // only place if coords on the board
            if (x < Constants.MinX || x > Constants.MaxX)
                return;
            if (y < Constants.MinY || y > Constants.MaxY)
                return;

            X = x;
            Y = y;
            Facing = inputFacing;

            Placed = true;
        }

        private void Move()
        {
            switch (Facing)
            { // only move if we don't fall off the board
                case Constants.Facing.North:
                    if ((Y + 1) <= Constants.MaxY)
                        Y++;
                    break;
                case Constants.Facing.South:
                    if ((Y - 1) >= Constants.MinY)
                        Y--;
                    break;
                case Constants.Facing.East:
                    if ((X - 1) >= Constants.MinX)
                        X--;
                    break;
                case Constants.Facing.West:
                    if ((X + 1) <= Constants.MaxX)
                        X++;
                    break;
            }
        }
    }
}
