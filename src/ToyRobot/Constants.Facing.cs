
namespace ToyRobot
{
    public static partial class Constants
    {
        public static class Facing
        {
            public const string North = "NORTH";
            public const string South = "SOUTH";
            public const string East = "EAST";
            public const string West = "WEST";
        }

        public static readonly string[] ValidFacing = { Facing.North, Facing.South, Facing.East, Facing.West };
    }
}
