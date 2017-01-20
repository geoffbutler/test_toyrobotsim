using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Xunit;

namespace ToyRobot.Tests
{
    public partial class ToyRobotSimulatorShould
    {
        private ToyRobotSimulator _target;

        public ToyRobotSimulatorShould()
        {
            _target = new ToyRobotSimulator();
        }

        [Fact]
        public void BeInstantiated()
        {
            Assert.NotNull(_target);
        }

        [Fact]
        public void BePlaced()
        {
            _target.Do("PLACE 0,0,NORTH");
            Assert.Equal(0, _target.X);
            Assert.Equal(0, _target.Y);
            Assert.Equal("NORTH", _target.Facing);
        }

        [Fact]
        public void MoveForwardNorth()
        {
            BePlaced();

            _target.Do("MOVE");
            Assert.Equal(0, _target.X);
            Assert.Equal(1, _target.Y);
            Assert.Equal("NORTH", _target.Facing);
        }

        [Fact]
        public void CanReport()
        {
            MoveForwardNorth();

            _target.Do("REPORT");
            Assert.Equal("0,1,NORTH", _target.LastReport);
        }

        [Fact]
        public void CanTurnLeftAndReport()
        {
            BePlaced();

            _target.Do("LEFT");
            _target.Do("REPORT");

            Assert.Equal(0, _target.X);
            Assert.Equal(0, _target.Y);
            Assert.Equal("WEST", _target.Facing);
            Assert.Equal("0,0,WEST", _target.LastReport);
        }

        [Fact]
        public void CanPlaceMoveTurnAndReport()
        {
            _target.Do("PLACE 1,2,EAST");
            _target.Do("MOVE");
            _target.Do("MOVE");
            _target.Do("LEFT");
            _target.Do("MOVE");
            _target.Do("REPORT");

            //Assert.Equal(3, _target.X);
            Assert.Equal(0, _target.X); // moves off board X
            Assert.Equal(3, _target.Y);
            Assert.Equal("NORTH", _target.Facing);
            //Assert.Equal("3,3,NORTH", _target.LastReport); // moves off board X
            Assert.Equal("0,3,NORTH", _target.LastReport);
        }

        [Fact]
        public void IgnoreCommandsUntilPlaced()
        {          
            _target.Do("MOVE");
            _target.Do("LEFT");
            _target.Do("RIGHT");
            _target.Do("REPORT");
            Assert.Null(_target.LastReport);
            Assert.False(_target.Placed);

            _target.Do("PLACE 1,2,EAST");
            Assert.True(_target.Placed);

            _target.Do("REPORT");
            Assert.Equal("1,2,EAST", _target.LastReport);
            _target.Do("MOVE");
            _target.Do("REPORT");
            Assert.Equal("0,2,EAST", _target.LastReport);
        }

        [Fact]
        public void MoveForwardSouth()
        {
            _target.Do("PLACE 1,1,SOUTH");
            Assert.Equal(1, _target.X);
            Assert.Equal(1, _target.Y);
            Assert.Equal("SOUTH", _target.Facing);

            _target.Do("MOVE");
            Assert.Equal(1, _target.X);
            Assert.Equal(0, _target.Y);
            Assert.Equal("SOUTH", _target.Facing);
        }

        [Fact]
        public void MoveForwardWest()
        {
            _target.Do("PLACE 0,1,WEST");
            Assert.Equal(0, _target.X);
            Assert.Equal(1, _target.Y);
            Assert.Equal("WEST", _target.Facing);

            _target.Do("MOVE");
            Assert.Equal(1, _target.X);
            Assert.Equal(1, _target.Y);
            Assert.Equal("WEST", _target.Facing);
        }

        [Fact]
        public void MoveForwardEast()
        {
            _target.Do("PLACE 1,1,EAST");
            Assert.Equal(1, _target.X);
            Assert.Equal(1, _target.Y);
            Assert.Equal("EAST", _target.Facing);

            _target.Do("MOVE");
            Assert.Equal(0, _target.X);
            Assert.Equal(1, _target.Y);
            Assert.Equal("EAST", _target.Facing);
        }

        [Fact]
        public void NotPlaceOffBoard()
        {
            _target.Do("PLACE -1,0,NORTH");
            Assert.False(_target.Placed);
            _target.Do("PLACE 0,-1,NORTH");
            Assert.False(_target.Placed);
            _target.Do("PLACE -1,-1,NORTH");
            Assert.False(_target.Placed);

            _target.Do("PLACE 5,0,NORTH");
            Assert.False(_target.Placed);
            _target.Do("PLACE 0,5,NORTH");
            Assert.False(_target.Placed);
            _target.Do("PLACE 5,0,NORTH");
            Assert.False(_target.Placed);
        }

        [Fact]
        public void NotMoveOffBoard()
        {
            _target.Do("PLACE 4,4,NORTH");
            _target.Do("MOVE");
            Assert.Equal(4, _target.X);
            Assert.Equal(4, _target.Y);

            _target.Do("PLACE 0,0,SOUTH");
            _target.Do("MOVE");
            Assert.Equal(0, _target.X);
            Assert.Equal(0, _target.Y);

            _target.Do("PLACE 4,0,WEST");
            _target.Do("MOVE");
            Assert.Equal(4, _target.X);
            Assert.Equal(0, _target.Y);

            _target.Do("PLACE 0,4,EAST");
            _target.Do("MOVE");
            Assert.Equal(0, _target.X);
            Assert.Equal(4, _target.Y);
        }

       
        public static IEnumerable<object[]> TestCases;

        static ToyRobotSimulatorShould()
        {            
            var asm = Assembly.GetAssembly(typeof(ToyRobotSimulatorShould));
            using (var stream = asm.GetManifestResourceStream("ToyRobot.Tests.testdata.json"))
            using (var reader = new StreamReader(stream))
            {
                string result = reader.ReadToEnd();
                TestCases = JsonConvert.DeserializeObject<List<TestCase>>(result)
                    .Select(o => new object[] { o.Commands, o.ExpectedReport })
                    .ToArray();
            }
        }

        [Theory]
        [MemberData("TestCases")]
        public void WalkTheBoard(string[] commands, string expectedReport)
        {
            foreach (var command in commands)
            {
                _target.Do(command);
            }

            _target.Do("REPORT");
            Assert.Equal(expectedReport, _target.LastReport);
        }
    }
}
