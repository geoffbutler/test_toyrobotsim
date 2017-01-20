using System;
using System.Collections.Generic;
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
        public string LastMessage { get; private set; }

        public void ShowWelcomeMessage()
        {
            ShowMessage(Messages.WelcomeMessage);
        }
        
        public void Do(string commandText)
        {
            string[] commands = commandText.Split(Constants.Commands.CommandsDelim, StringSplitOptions.RemoveEmptyEntries);

            var command = commands[0].ToUpper(); // ignore case

            if (commands.Length == 2 && command == Constants.Commands.Place)
            {
                Place(commands);
                return;
            }

            if (commands.Length != 1 || !Constants.ValidSingleWordCommands.Contains(command))
            {
                ShowMessage(Messages.ErrorInvalidCommandMessage); // must be invalid
                return;
            }

            if (!Placed)
            {
                ShowMessage(Messages.ErrorNotPlacedMessage);
                return; // must be placed first
            }

            if (command == Constants.Commands.Report)
            {
                Report();
            }
            else if (command == Constants.Commands.Move)
            {
                Move();
            }
            else if (Constants.ValidTurns.Contains(command))
            {
                Turn(command);
            }
        }        

        private void ShowMessage(string message)
        {
            Console.WriteLine(message);
            Console.WriteLine();
            LastMessage = message;
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
            var report = string.Format(Constants.Commands.PlaceParamsFormat, X, Y, Facing);
            Console.WriteLine(report);
            LastReport = report;
        }

        private bool Place(string[] commands)
        {
            var parts = commands[1].Split(Constants.Commands.PlaceParamsDelim, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length != 3)
            {
                ShowMessage(Messages.ErrorInvalidPlaceCommandSyntax); // must be invalid
                return false;
            }

            var inputX = parts[0];
            var inputY = parts[1];
            var inputFacing = parts[2].ToUpper(); // ignore case

            // ignore invalid input
            int x, y;
            if (!int.TryParse(inputX, out x))
            {
                ShowMessage(Messages.ErrorInvalidPlaceCommandXUnparsable);
                return false;
            }
            if (!int.TryParse(inputY, out y))
            {
                ShowMessage(Messages.ErrorInvalidPlaceCommandYUnparsable);
                return false;
            }
            if (!Constants.ValidFacing.Contains(inputFacing))
            {
                ShowMessage(Messages.ErrorInvalidPlaceCommandFUnparsable);
                return false;
            }

            // only place if coords on the board
            if (x < Constants.MinX || x > Constants.MaxX)
            {
                ShowMessage(Messages.ErrorInvalidPlaceCommandXOutOfRange);
                return false;
            }
            if (y < Constants.MinY || y > Constants.MaxY)
            {
                ShowMessage(Messages.ErrorInvalidPlaceCommandYOutOfRange);
                return false;
            }

            X = x;
            Y = y;
            Facing = inputFacing;

            Placed = true;
            return true; // input was good
        }

        private void Move()
        {
            switch (Facing)
            { // only move if we don't fall off the board
                case Constants.Facing.North:
                    Y = ((Y + 1) <= Constants.MaxY ? ++Y : Y);                    
                    break;
                case Constants.Facing.South:
                    Y = ((Y - 1) >= Constants.MinY ? --Y : Y);                    
                    break;
                case Constants.Facing.East:
                    X = ((X - 1) >= Constants.MinX ? --X : X);                    
                    break;
                case Constants.Facing.West:
                    X = ((X + 1) <= Constants.MaxX ? ++X : X);                    
                    break;
            }
        }
    }
}
