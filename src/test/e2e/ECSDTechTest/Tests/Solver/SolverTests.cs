using System.Collections.Generic;
using ECSDTechTest.Solver;
using FluentAssertions;
using NUnit.Framework;

namespace ECSDTechTest.Tests.Solver
{
    // Throw some tests at the solver to assert valid behavior outside the normally tested values
    public class SolverTests
    {
        [Test]
        public void SolverReturnsNullIfNoMatchFound()
        {
            var invalid = ChallengeSolver.SolveChallenge(new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 });
            invalid.Should().Be(null, "The numbers are sequential, so no match should be found");
        }

        [Test]
        public void SolverAbleToMatchAtLeftBoundary()
        {
            // Check that the algorithm works for the left-most possible match (Index 1)
            var boundaryLeft = ChallengeSolver.SolveChallenge(new List<int> { 7, 0, 1, 1, 1, 1, 1, 1, 1 });
            boundaryLeft.Should().Be(1, "7 = 7 = 1+1+1+1+1+1+1");
        }

        [Test]
        public void SolverAbleToMatchAtRightBoundary()
        {
            // Check that the algorithm works for the right-most possible match (Index 7)
            var boundaryRight = ChallengeSolver.SolveChallenge(new List<int> { 1, 1, 1, 1, 1, 1, 1, 0, 7 });
            boundaryRight.Should().Be(7, "7 = 7 = 1+1+1+1+1+1+1");
        }
    }
}
