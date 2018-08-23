using System;
using System.Collections.Generic;

namespace ECSDTechTest.Solver
{
    public static class ChallengeSolver
    {
        // Solves the challenge in TestApp
        // Given a List of x length, returns the index where all elements before and after that index add up to the same amount
        // If no such index exists, returns null
        public static int? SolveChallenge(List<int> arr)
        {
            if (arr.Count % 2 == 0)
            {
                throw new Exception("Even number of items passed, not a valid challenge");
            }

            if (arr.Count < 3)
            {
                throw new Exception("Less than 3 items passed, not a valid challenge");
            }

            var maxIndex = arr.Count - 1;
            var fromLeft = new int[arr.Count];
            var leftTotal = 0;
            var fromRight = new int[arr.Count];
            var rightTotal = 0;
            // Create two arrays where:
            // Index i of the "left" array is the total of all values working from the left (eg index 3 is the total of indexes 0+1+2+3)
            // Index i of the "right" is the total of all values working from the right (eg index 7 of a 9-element array is the total of indexes 8+7)
            // No need to do last item, as it could not possibly be a match
            for (var i = 0; i < arr.Count - 2; i++)
            {
                var leftIndex = i;
                var rightIndex = maxIndex - i;
                leftTotal += arr[leftIndex];
                fromLeft[leftIndex] = leftTotal;
                rightTotal += arr[rightIndex];
                fromRight[rightIndex] = rightTotal;
            }

            // Iterate through, and compare leftIndex[i-1] to rightIndex[i+1]
            // We can skip the first and last items, as they could not be a match
            for (var i = 1; i < arr.Count - 1; i++)
            {
                if (fromLeft[i - 1] == fromRight[i + 1]) return i;
            }

            return null;
        }

    }
}
