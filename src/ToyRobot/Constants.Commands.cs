
namespace ToyRobot
{
    public static partial class Constants
    {
        public static class Commands
        {
            public static readonly char[] CommandsDelim = new char[] { ' ' };

            public const string Place = "PLACE";
            public static readonly char[] PlaceParamsDelim = new char[] { ',' };
            public static readonly string PlaceParamsFormat = "{0},{1},{2}"; // X,Y,Facing                

            public const string Report = "REPORT";
            public const string Move = "MOVE";
        }

        public static readonly string[] ValidSingleWordCommands = { Commands.Report, Commands.Move, Turns.Left, Turns.Right };
    }
}
