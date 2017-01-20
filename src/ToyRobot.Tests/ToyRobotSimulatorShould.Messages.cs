using Xunit;

namespace ToyRobot.Tests
{
    public partial class ToyRobotSimulatorShould
    {
        [Fact]
        public void ShowWelcomeMessage()
        {
            _target.ShowWelcomeMessage();
            Assert.Equal(Messages.WelcomeMessage, _target.LastMessage);
        }

        [Fact]
        public void ShowErrorMessageWhenNotPlaced()
        {
            _target.Do("MOVE");
            Assert.Equal(Messages.ErrorNotPlacedMessage, _target.LastMessage);
        }

        [Fact]
        public void ShowInvalidCommandMessageOnBadPlaceInput()
        {
            _target.Do("PLACE foobar");
            Assert.Equal(Messages.ErrorInvalidPlaceCommandSyntax, _target.LastMessage);
        }

        [Fact]
        public void ShowInvalidCommandMessageOnBadPlaceInputX()
        {
            _target.Do("PLACE a,0,NORTH");
            Assert.Equal(Messages.ErrorInvalidPlaceCommandXUnparsable, _target.LastMessage);
        }

        [Fact]
        public void ShowInvalidCommandMessageOnOutOfRangeMinPlaceInputX()
        {
            _target.Do("PLACE -1,0,NORTH");
            Assert.Equal(Messages.ErrorInvalidPlaceCommandXOutOfRange, _target.LastMessage);
        }

        [Fact]
        public void ShowInvalidCommandMessageOnOutOfRangeMaxPlaceInputX()
        {
            _target.Do("PLACE 5,0,NORTH");
            Assert.Equal(Messages.ErrorInvalidPlaceCommandXOutOfRange, _target.LastMessage);
        }

        [Fact]
        public void ShowInvalidCommandMessageOnBadPlaceInputY()
        {
            _target.Do("PLACE 0,a,NORTH");
            Assert.Equal(Messages.ErrorInvalidPlaceCommandYUnparsable, _target.LastMessage);
        }

        [Fact]
        public void ShowInvalidCommandMessageOnOutOfRangeMinPlaceInputY()
        {
            _target.Do("PLACE 0,-1,NORTH");
            Assert.Equal(Messages.ErrorInvalidPlaceCommandYOutOfRange, _target.LastMessage);
        }

        [Fact]
        public void ShowInvalidCommandMessageOnOutOfRangeMaxPlaceInputY()
        {
            _target.Do("PLACE 0,5,NORTH");
            Assert.Equal(Messages.ErrorInvalidPlaceCommandYOutOfRange, _target.LastMessage);
        }

        [Fact]
        public void ShowInvalidCommandMessageOnBadPlaceInputF()
        {
            _target.Do("PLACE 0,0,foo");
            Assert.Equal(Messages.ErrorInvalidPlaceCommandFUnparsable, _target.LastMessage);
        }

        [Fact]
        public void ShowInvalidCommandMessageOnBadPlaceAfterSuccessfulPlace()
        {
            _target.Do("PLACE 0,0,NORTH");
            Assert.NotEqual(Messages.ErrorInvalidPlaceCommandSyntax, _target.LastMessage);

            _target.Do("PLACE foobar");
            Assert.Equal(Messages.ErrorInvalidPlaceCommandSyntax, _target.LastMessage);
        }

        [Fact]
        public void ShowInvalidCommandMessageOnBadPlaceAfterSuccessfulPlaceInputX()
        {
            _target.Do("PLACE 0,0,NORTH");
            Assert.NotEqual(Messages.ErrorInvalidPlaceCommandXUnparsable, _target.LastMessage);

            _target.Do("PLACE a,0,NORTH");
            Assert.Equal(Messages.ErrorInvalidPlaceCommandXUnparsable, _target.LastMessage);
        }

        [Fact]
        public void ShowInvalidCommandMessageOnBadPlaceAfterSuccessfulPlaceInputY()
        {
            _target.Do("PLACE 0,0,NORTH");
            Assert.NotEqual(Messages.ErrorInvalidPlaceCommandYUnparsable, _target.LastMessage);

            _target.Do("PLACE 0,a,NORTH");
            Assert.Equal(Messages.ErrorInvalidPlaceCommandYUnparsable, _target.LastMessage);
        }

        [Fact]
        public void ShowInvalidCommandMessageOnBadPlaceAfterSuccessfulPlaceInputF()
        {
            _target.Do("PLACE 0,0,NORTH");
            Assert.NotEqual(Messages.ErrorInvalidPlaceCommandFUnparsable, _target.LastMessage);

            _target.Do("PLACE 0,0,foo");
            Assert.Equal(Messages.ErrorInvalidPlaceCommandFUnparsable, _target.LastMessage);
        }

        [Fact]
        public void ShowInvalidCommandMessageOnSingleInvalidCommandInput()
        {
            _target.Do("FOO");
            Assert.Equal(Messages.ErrorInvalidCommandMessage, _target.LastMessage);
        }

        [Fact]
        public void ShowInvalidCommandMessageOnMultipleInvalidCommandInput()
        {
            _target.Do("FOO bar");
            Assert.Equal(Messages.ErrorInvalidCommandMessage, _target.LastMessage);
        }
    }
}
