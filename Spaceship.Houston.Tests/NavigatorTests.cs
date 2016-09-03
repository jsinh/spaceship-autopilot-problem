namespace Autopilot.Tests
{
    // Install Xunit package to perform unit testing - it gives quick and easy integration and capabilities to run and debug unit test from VS.
    using Xunit;
    using Xunit.Abstractions;

    public class NavigatorTests
    {
        // Xunit logger to write to test output.
        private readonly ITestOutputHelper output;

        public NavigatorTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact(DisplayName = "Autopilot_FailCase01")]
        public void Case01()
        {
            // Arrange
            INavigator navigator = new Navigator();
            var a = new Coordinate(-1, -2, "a");
            var b = new Coordinate(1, 1, "b");
            var c = new Coordinate(2, -1, "c");
            var d = new Coordinate(3, -2, "d");
            var e = new Coordinate(6, 1, "e");
            var f = new Coordinate(3, 10, "f");
            var g = new Coordinate(-1, 8, "g");
            var h = new Coordinate(-2, 9, "h");

            // Act
            var shortestRoute = navigator.Route(
                Coordinate.Center,
                new[]
                {
                    d, h, b, g, a, c, f, e
                });

            this.output.WriteLine(
                         shortestRoute[0].Identifier
                + ", " + shortestRoute[1].Identifier
                + ", " + shortestRoute[2].Identifier
                + ", " + shortestRoute[3].Identifier
                + ", " + shortestRoute[4].Identifier
                + ", " + shortestRoute[5].Identifier
                + ", " + shortestRoute[6].Identifier
                + ", " + shortestRoute[7].Identifier);

            // Assert
            Assert.Equal(new Coordinate[8] { a, b, c, d, e, f, g, h }, shortestRoute);
        }

        [Fact(DisplayName = "Autopilot_PassCase02")]
        public void Case02()
        {
            // Arrange
            INavigator navigator = new Navigator();
            var a = new Coordinate(1, 1, "a");
            var b = new Coordinate(2, 2, "b");
            var c = new Coordinate(3, 3, "c");
            var d = new Coordinate(4, 4, "d");
            var e = new Coordinate(5, 5, "e");
            var f = new Coordinate(6, 6, "f");
            var g = new Coordinate(7, 7, "g");
            var h = new Coordinate(8, 8, "h");

            // Act
            var shortestRoute = navigator.Route(
                Coordinate.Center,
                new[]
                {
                    g, h, a, d, b, e, f, c
                });

            this.output.WriteLine(
                         shortestRoute[0].Identifier
                + ", " + shortestRoute[1].Identifier
                + ", " + shortestRoute[2].Identifier
                + ", " + shortestRoute[3].Identifier
                + ", " + shortestRoute[4].Identifier
                + ", " + shortestRoute[5].Identifier
                + ", " + shortestRoute[6].Identifier
                + ", " + shortestRoute[7].Identifier);

            // Assert
            Assert.Equal(new Coordinate[8] { a, b, c, d, e, f, g, h }, shortestRoute);
        }
    }
}