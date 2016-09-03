namespace Autopilot
{
    #region Namespace

    using System;
    using System.Collections.Generic;
    using System.Linq;

    #endregion

    public class Navigator : INavigator
    {
        /// <summary>
        /// Solving the Spaceship problem by traveling salesman problem (TSP) Algorithm using a similar algorithm to Hamiltonian Cycles.
        /// </summary>
        public Coordinate[] Route(Coordinate spaceshipPosition, Coordinate[] destinations)
        {
            var destinationPointCount = destinations.Length;
            var masterShortestPathMapListCounter = 0;
            var masterShortestPathMapList = new Dictionary<int, KeyValuePair<List<KeyValuePair<KeyValuePair<int, int>, int>>, int>>();

            // Pickup a looped point from the destination list to get started.
            for (var counter = 0; counter < destinationPointCount; counter++)
            {
                // To start from origin we set the first buffer point A to -1. Consider -1 as origin point counter value.
                var bufferPointA = -1;

                // This tracks which points are already processed / considered in calculation below.
                var pointProcessedTrackerBuffer = new bool[destinationPointCount];

                // This is a route details from origin to first point and then point A to point B till the end of all destination point processed.
                // Example:
                // <<int,            int>,      int>
                // Origin (-1)  to  Point 4     Distance (3)
                // Point 4      to  Point 2     Distance (5)
                // Point 2      to  Point 1     Distance (12)
                // Point 1      to  Point 3     Distance (6)
                var level1MapRouteList = new List<KeyValuePair<KeyValuePair<int, int>, int>>();

                // Calculate first distance from origin to first looped point.
                var distanceFromOrigin = CalculateDistance(spaceshipPosition, destinations[counter]);

                // Set first looped point to processed status in tracking buffer.
                pointProcessedTrackerBuffer[counter] = true;

                // Save this route fragment into map route list for this looped point.
                level1MapRouteList.Add(new KeyValuePair<KeyValuePair<int, int>, int>(new KeyValuePair<int, int>(bufferPointA, counter), distanceFromOrigin));
                // Set current looped point position as buffer point A for next processing.
                bufferPointA = counter;

                // Keep looping through destination point list until all points are tracked as processed.
                while (pointProcessedTrackerBuffer.Any(item => !item))
                {
                    // Get next shortest distance point (point B) from point A.
                    var tempResult = SecondLevelIteration(destinations, pointProcessedTrackerBuffer, bufferPointA);

                    // Set point B as processed.
                    pointProcessedTrackerBuffer[tempResult.Key] = true;

                    // Save this route fragment into map route list for this point A to point B route.
                    level1MapRouteList.Add(new KeyValuePair<KeyValuePair<int, int>, int>(new KeyValuePair<int, int>(bufferPointA, tempResult.Key), tempResult.Value));

                    // Hop from point A to point B in buffer for next processing.
                    bufferPointA = tempResult.Key;
                }

                // Now that we have a complete route from looped point in this iteration, calculate the total sum of distance from origin to last point.
                var totalPathDistance = level1MapRouteList.Sum(item => item.Value);

                // Save this iteration's map route list and sum distance of this iteration route in master map route list.
                masterShortestPathMapList.Add(masterShortestPathMapListCounter, new KeyValuePair<List<KeyValuePair<KeyValuePair<int, int>, int>>, int>(level1MapRouteList, totalPathDistance));
                // Simple increment in master map route list.
                masterShortestPathMapListCounter++;
            }

            // We are done from origin to all available point to start and get the shorted path from there to end,
            // Now, ordering the master map route list and get the shorted 'Total summed distance' from above map route list.
            var shortestPathMapOrdered = masterShortestPathMapList.OrderBy(item => item.Value.Value);
            var shortestPathMap = shortestPathMapOrdered.First();

            // Simple loop to set final array of shortest distance route and return result.
            var tempFinalCounter = 0;
            var resultArray = new Coordinate[destinationPointCount];
            foreach (var mapPathItem in shortestPathMap.Value.Key)
            {
                resultArray[tempFinalCounter] = destinations[mapPathItem.Key.Value];
                tempFinalCounter++;
            }

            // Tada!!
            return resultArray;
        }

        /// <summary>
        /// Point A to point B shortest part finder process.
        /// </summary>
        /// <param name="destinations">Master point list.</param>
        /// <param name="positionTracker">Process point tracker list.</param>
        /// <param name="tempPointA">Position index of point A in master point list which is also marked 'true' in position tracker.</param>
        /// <returns>Returns shortest distance next point from master point list as - master point list position and distance from input point A.</returns>
        private KeyValuePair<int, int> SecondLevelIteration(Coordinate[] destinations, bool[] positionTracker, int tempPointA)
        {
            // Temporary buffers to save short point B position in destinations and shortest distance value from point A to point B.
            var bufferPosition = 0;
            var bufferDistance = 0;

            // Loop through destination list to find next shortest distance point (point B).
            for (var counter = 0; counter < destinations.Length; counter++)
            {
                // Ignore point if it is already tracked before (so we do not repeat the same point again).
                if (positionTracker[counter] != true)
                {
                    // Calculate distance from input point A to looped point B.
                    var pointAToPointBDistance = CalculateDistance(destinations[tempPointA], destinations[counter]);

                    // Initial buffer distance will be zero so set if that is the case.
                    if (bufferDistance == 0)
                    {
                        bufferPosition = counter;
                        bufferDistance = pointAToPointBDistance;
                    }

                    // Update buffer distance and position if calculated distance is shorter than the last known shortest distance from input point A to looped point B.
                    if (pointAToPointBDistance < bufferDistance)
                    {
                        bufferPosition = counter;
                        bufferDistance = pointAToPointBDistance;
                    }
                }
            }

            // Return shortest distance point B details - Position in destinations list and shortest determined distance from input point A.
            return new KeyValuePair<int, int>(bufferPosition, bufferDistance);
        }

        /// <summary>
        /// Calculate distance process
        /// </summary>
        /// <param name="pointA">Coordinate point A.</param>
        /// <param name="pointB">Coordinate point B.</param>
        /// <returns>Returns distance between point A and B.</returns>
        private int CalculateDistance(Coordinate pointA, Coordinate pointB)
        {
            return Math.Abs(pointA.X - pointB.X) + Math.Abs(pointA.Y - pointB.Y);
        }
    }
}