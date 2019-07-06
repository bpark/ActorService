using System;
using System.Collections.Generic;

namespace ActorService.Model
{
    // https://msdn.microsoft.com/en-us/magazine/mt829710.aspx
    class QLearningProgram
    {
        static Random rnd = new Random(1);
        
        static void Main(string[] args)
        {
            Console.WriteLine("Begin Q-learning maze demo");
            Console.WriteLine("Setting up maze and rewards");
            int ns = 12;
            int[][] FT = CreateMaze(ns);
            double[][] R = CreateReward(ns);
            double[][] Q = CreateQuality(ns);
            Console.WriteLine("Analyzing maze using Q-learning");
            int goal = 11;
            
            
            double gamma = 0.5;
            double learnRate = 0.5;
            
            // Q-learning is iterative, so the demo sets up a maxEpochs variable to control how long the algorithm
            // can use to find the Q matrix.
            int maxEpochs = 1000;
            
            
            Train(FT, R, Q, goal, gamma, learnRate, maxEpochs);
            Console.WriteLine("Done. Q matrix: ");
            Print(Q);
            Console.WriteLine("Using Q to walk from cell 8 to 11");
            
            // Define start and goal, result is the optimal path
            Walk(10, 11, Q);
            
            Console.WriteLine("End demo");
            Console.ReadLine();
        }
        
        static void Print(double[][] Q) {
            int ns = Q.Length;
            Console.WriteLine("[0] [1] . . [11]");
            for (int i = 0; i < ns; ++i) {
                for (int j = 0; j < ns; ++j) {
                    Console.Write(Q[i][j].ToString("F2") + " ");
                }
                Console.WriteLine();
            }
        }
        
        /// <summary>
        /// The method returns a matrix that defines allowable moves. For example, you can move from cell 4 to cell 8,
        /// but you can’t move from cell 4 to cell 5 because there’s a wall in the way. Recall that C# initializes int
        /// arrays to 0, so CreateMaze needs to specify only allowable moves. Notice that you can’t move from a cell to
        /// itself, except for the goal-cell 11.
        /// </summary>
        /// <param name="ns">the size, ex 12 to create 3 rows, 4 columns</param>
        /// <returns>The playground</returns>
        private static int[][] CreateMaze(int ns) {
            var ft = new int[ns][];
            for (var i = 0; i < ns; ++i) ft[i] = new int[ns];
            ft[0][1] = ft[0][4] = ft[1][0] = ft[1][5] = ft[2][3] = 1;
            ft[2][6] = ft[3][2] = ft[3][7] = ft[4][0] = ft[4][8] = 1;
            ft[5][1] = ft[5][6] = ft[5][9] = ft[6][2] = ft[6][5] = 1;
            ft[6][7] = ft[7][3] = ft[7][6] = ft[7][11] = ft[8][4] = 1;
            ft[8][9] = ft[9][5] = ft[9][8] = ft[9][10] = ft[10][9] = 1;
            ft[11][11] = 1;  // Goal
            return ft;
        }
        
        /// <summary>
        /// In this example, moving to goal-cell 11 gives a reward of 10.0, but any other move gives a negative reward
        /// of -0.1. These values are somewhat arbitrary. In general, when working with RL, the reward structure is
        /// entirely problem-dependent. Here, the small negative reward punishes every move a bit, which has the effect
        /// of preferring shorter paths over longer paths to the goal. Notice you don’t have to set a reward for moves
        /// that aren’t allowed because they will never happen.
        /// </summary>
        /// <param name="ns">the size, ex 12 to create 3 rows, 4 columns</param>
        /// <returns>The reward matrix</returns>
        private static double[][] CreateReward(int ns) {
            var r = new double[ns][];
            for (var i = 0; i < ns; ++i) r[i] = new double[ns];
            r[0][1] = r[0][4] = r[1][0] = r[1][5] = r[2][3] = -0.1;
            r[2][6] = r[3][2] = r[3][7] = r[4][0] = r[4][8] = -0.1;
            r[5][1] = r[5][6] = r[5][9] = r[6][2] = r[6][5] = -0.1;
            r[6][7] = r[7][3] = r[7][6] = r[7][11] = r[8][4] = -0.1;
            r[8][9] = r[9][5] = r[9][8] = r[9][10] = r[10][9] = -0.1;
            r[7][11] = 10.0;  // Goal
            return r;
        }
        
        /// <summary>
        /// The goal of Q-learning is to find the value of the Q matrix. Initially, all Q values are set to 0.0 and
        /// the Q matrix is created like so:
        /// </summary>
        /// <param name="ns"></param>
        /// <returns></returns>
        private static double[][] CreateQuality(int ns) {
            var q = new double[ns][];
            for (var i = 0; i < ns; ++i)
                q[i] = new double[ns];
            return q;
        }
        
        /// <summary>
        /// As you’ll see shortly, the Q-learning algorithm needs to know what states the system can transition to,
        /// given a current state. In this example, a state of the system is the same as the location in the maze so
        /// there are only 12 states. Method GetPossNextStates is defined like so.
        ///
        /// For example, if the current states is 5, then GetPossNextStates returns a List of int collection
        /// holding (1, 6, 9).
        /// </summary>
        /// <param name="s"></param>
        /// <param name="ft"></param>
        /// <returns></returns>
        private static List<int> GetPossNextStates(int s, int[][] ft) {
            var result = new List<int>();
            for (var j = 0; j < ft.Length; ++j)
                if (ft[s][j] == 1) result.Add(j);
            return result;
        }
        
        /// <summary>
        /// The Q-learning algorithm sometimes goes from the current state to a random next state. That functionality
        /// is defined by method GetRandNextState.
        ///
        /// So, if the current state s is 5, then GetRandNextState returns either 1 or 6 or 9 with equal
        /// probability (0.33 each).
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <param name="ft"></param>
        /// <returns></returns>
        private static int GetRandNextState(int s, int[][] ft) {
            var possNextStates = GetPossNextStates(s, ft);
            var ct = possNextStates.Count;
            var idx = rnd.Next(0, ct);
            return possNextStates[idx];
        }
        
        /// <summary>
        /// The key update equation for Q-learning is based on the mathematical Bellman equation and is shown at the
        /// bottom of Figure 1. The algorithm is implemented in method Train. In high-level pseudo-code, the Q-learning
        /// algorithm is:
        ///
        /// loop maxEpochs times
        ///   set currState = a random state
        ///   while currState != goalState
        ///     pick a random next-state but don't move yet
        ///     find largest Q for all next-next-states
        ///     update Q[currState][nextState] using Bellman
        ///     move to nextState
        ///   end-while
        /// end-loop
        ///
        /// The number of training epochs must be determined by trial and error. An alternative design is to iterate
        /// until the values in the Q matrix don’t change, or until they stabilize to very small changes per iteration.
        /// The inner loop iterates until the current state becomes the goal state, cell 11 in the case of the demo
        /// maze.
        ///
        /// Imagine you’re in a maze. You see that you can go to three different rooms, A, B, C. You pick B, but don’t
        /// move yet. You ask a friend to go into room B and the friend tells you that from room B you can go to rooms
        /// X, Y, Z and that of those rooms Y has the best Q value. In other words, Y is the best next-next state.
        ///
        /// The update equation has two parts. The first part, ((1 - lrnRate) * Q[currState][nextState]), is called the
        /// exploit component and adds a fraction of the old value.
        /// The second part, (lrnRate * (R[currState][nextState] + (gamma * maxQ))), is called the explore component.
        /// Larger values of the lrnRate increase the influence of both current rewards and future rewards (explore)
        /// at the expense of past rewards (exploit). The value of gamma, the discount factor, influences the
        /// importance of future rewards.
        /// 
        /// </summary>
        /// <param name="ft"></param>
        /// <param name="R"></param>
        /// <param name="Q"></param>
        /// <param name="goal"></param>
        /// <param name="gamma"></param>
        /// <param name="lrnRate"></param>
        /// <param name="maxEpochs"></param>
        private static void Train(int[][] ft, double[][] R, double[][] Q,
            int goal, double gamma, double lrnRate, int maxEpochs)
        {
            for (var epoch = 0; epoch < maxEpochs; ++epoch) {
                var currState = rnd.Next(0, R.Length);
                
                while (true) {
                    var nextState = GetRandNextState(currState, ft);
                    var possNextNextStates = GetPossNextStates(nextState, ft);
                    var maxQ = double.MinValue;
                    foreach (var nns in possNextNextStates)
                    {
                        var q = Q[nextState][nns];
                        if (q > maxQ) maxQ = q;
                    }
                    Q[currState][nextState] =
                        ((1 - lrnRate) * Q[currState][nextState]) +
                        (lrnRate * (R[currState][nextState] + (gamma * maxQ)));
                    currState = nextState;
                    if (currState == goal) break;
                } 
            } 
        } 
                
        /// <summary>
        /// After the quality matrix has been computed, it can be used to find an optimal path from any starting
        /// state to the goal state. Method Walk implements this functionality:
        /// </summary>
        /// <param name="start"></param>
        /// <param name="goal"></param>
        /// <param name="Q"></param>
        private static void Walk(int start, int goal, double[][] Q) {
            var curr = start;
            Console.Write(curr + "->");
            while (curr != goal) {
                var next = ArgMax(Q[curr]);
                Console.Write(next + "->");
                curr = next;
            }
            Console.WriteLine("done");
        }
        
        /// <summary>
        /// Notice the method assumes that the goal state is reachable from the starting state. The method uses helper
        /// ArgMax to find the best next state:
        ///
        /// For example, if a vector has values (0.5, 0.3, 0.7, 0.2) then ArgMax returns 2.
        /// </summary>
        /// <param name="vector"></param>
        /// <returns></returns>
        private static int ArgMax(double[] vector) {
            var maxVal = vector[0];  int idx = 0;
            for (var i = 0; i < vector.Length; ++i) {
                if (vector[i] > maxVal) {
                    maxVal = vector[i];  idx = i;
                }
            }
            return idx;
        }
    } // Program
}