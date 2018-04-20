using System;
using System.Drawing;

namespace IP_PGA.PGA
{
    /// <summary>
    /// Get Chromosome by Mutation Operand
    /// </summary>
    public static class Mutation
    {
        // Function Uniform of class Mutation
        // change a bit of offspring chromosome for mutation
        public static void mutation (this Chromosome child, Random rand)
        {
            // Random Number for choose 2 bit between 0 ~ (offspring.Length - 1)
            // if(offspring.Length == 8)
            //                           (0)×-------------×(offspring.Length-1)
            //                             |_|_|_|_|_|_|_|_|
            //                              0 1 2 3 4 5 6 7
            //
            // change 2 bit locate (Greedy Mutation)
            // before Greedy Mutate:
            // chromosome Child =      |_|_|_|_|_|_|_|_| ...
            //                          0 1 2 3 4 5 6 7 
            // Greedy Mutating:
            // Select 2 bit (1 & 4)       *     *
            // chromosome Child =      |_|_|_|_|_|_|_|_| ...            (Step 1)
            //                          0 1 2 3 4 5 6 7 
            // After Greedy Mutation:    
            // Changed 2 bit (1 & 4)      *     *
            // chromosome Child =      |_|_|_|_|_|_|_|_| ...            (Step 2)
            //                          0 4 2 3 1 5 6 7 
            //
            //
            // Step 1: -------------- Define Number of Mutation Points Randomize --------------
            int numberMutation = rand.Next(0, Chromosome.imageHeight * Chromosome.imageWidth / 1000); // Probability Of Mutation is 0.1%
            //
            for (int m = 0; m < numberMutation; m++)
            {
                // Step 2: -------------- Select 2 bit by Random Number -----------------------
                int X0 = rand.Next(0, Chromosome.imageWidth - 1);
                int Y0 = rand.Next(0, Chromosome.imageHeight - 1);

                int X1 = rand.Next(0, Chromosome.imageWidth - 1);
                int Y1 = rand.Next(0, Chromosome.imageHeight - 1);
                // -------------------------------------------------------------------------------
                // Step 3: +++++++++++++++++++ Change selected bit's +++++++++++++++++++++++++++++
                //
                //         buffer <---- bit0
                byte buffer = child.imageMatrix[X0, Y0];
                //
                //         bit0   <---- bit1
                child.imageMatrix[X0, Y0] = child.imageMatrix[X1, Y1];
                //
                //         bit1   <---- buffer
                child.imageMatrix[X1, Y1] = buffer;
                // +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
            }
        }
    }
}
