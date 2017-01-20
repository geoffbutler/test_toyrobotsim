using ToyRobot.Properties;

namespace ToyRobot
{
    public class Messages
    {
        static Messages()
        {
            WelcomeMessage = string.Format(
                Resources.WelcomeMessageFormat,
                (Constants.MaxX + 1),
                (Constants.MaxY + 1),
                Constants.MinX,
                Constants.MaxX,
                Constants.MinY,
                Constants.MaxY
                );

            ErrorNotPlacedMessage = string.Format(
                Resources.ErrorNotPlacedMessageFormat,
                Constants.MinX,
                Constants.MaxX,
                Constants.MinY,
                Constants.MaxY
                );

            ErrorInvalidPlaceCommandSyntax = string.Format(
                Resources.ErrorInvalidPlaceCommandSyntaxFormat,
                Constants.MinX,
                Constants.MaxX,
                Constants.MinY,
                Constants.MaxY
                );

            ErrorInvalidPlaceCommandXUnparsable = string.Format(
                Resources.ErrorInvalidPlaceCommandXUnparsableFormat,
                Constants.MinX,
                Constants.MaxX
                );

            ErrorInvalidPlaceCommandXOutOfRange = string.Format(
                Resources.ErrorInvalidPlaceCommandXOutOfRangeFormat,
                Constants.MinX,
                Constants.MaxX
                );

            ErrorInvalidPlaceCommandYUnparsable = string.Format(
                Resources.ErrorInvalidPlaceCommandYUnparsableFormat,
                Constants.MinY,
                Constants.MaxY
                );

            ErrorInvalidPlaceCommandYOutOfRange = string.Format(
                Resources.ErrorInvalidPlaceCommandYOutOfRangeFormat,
                Constants.MinY,
                Constants.MaxY
                );
        }
        
        public static string WelcomeMessage
        {
            get; private set;
        }

        public static string ErrorNotPlacedMessage
        {
            get; private set;
        }

        public static string ErrorInvalidCommandMessage
        {
            get
            {
                return Resources.ErrorInvalidCommandMessage;
            }
        }

        public static string ErrorInvalidPlaceCommandSyntax
        {
            get; private set;
        }

        public static string ErrorInvalidPlaceCommandXUnparsable
        {
            get; private set;
        }

        public static string ErrorInvalidPlaceCommandXOutOfRange
        {
            get; private set;
        }

        public static string ErrorInvalidPlaceCommandYUnparsable
        {
            get; private set;
        }

        public static string ErrorInvalidPlaceCommandYOutOfRange
        {
            get; private set;
        }

        public static string ErrorInvalidPlaceCommandFUnparsable
        {
            get
            {
                return Resources.ErrorInvalidPlaceCommandFUnparsable;
            }
        }
    }
}
