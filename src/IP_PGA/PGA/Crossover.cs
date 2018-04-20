using System;
using System.Collections;
using System.Drawing;

namespace IP_PGA.PGA
{
    public static class Crossover
    {
        /// <summary>
        /// Do Crossover between 2 Chromosome's
        /// </summary>
        /// <param name="Dad">Father chromosome for product Children Chromosome</param>
        /// <param name="Mum">Mather chromosome for product Children Chromosome</param>
        /// <param name="rand">random reproducer</param>
        /// <returns></returns>
        public static Chromosome crossover(this Chromosome Dad, Chromosome Mum, Random rand)
        {
            //
            // define offspring chromosome length
            Chromosome offspring = new Chromosome();
            //
            // Multi Points Crossover: (X and Y are pixel colors)
            //
            //          DAD:    _______________________________
            //                 |_X_|_X_|_X_|_X_|_X_|_X_|_X_|_X_|
            //                 |_X_|_X_|_X_|_X_|_X_|_X_|_X_|_X_|
            //                 |_X_|_X_|_X_|_X_|_X_|_X_|_X_|_X_|
            //                 |_X_|_X_|_X_|_X_|_X_|_X_|_X_|_X_|
            //                 |_X_|_X_|_X_|_X_|_X_|_X_|_X_|_X_|
            //
            // 
            //          MUM:    _______________________________
            //                 |_Y_|_Y_|_Y_|_Y_|_Y_|_Y_|_Y_|_Y_|
            //                 |_Y_|_Y_|_Y_|_Y_|_Y_|_Y_|_Y_|_Y_|
            //                 |_Y_|_Y_|_Y_|_Y_|_Y_|_Y_|_Y_|_Y_|
            //                 |_Y_|_Y_|_Y_|_Y_|_Y_|_Y_|_Y_|_Y_|
            //                 |_Y_|_Y_|_Y_|_Y_|_Y_|_Y_|_Y_|_Y_|
            //
            //  ● is decomposition point (Randomize Points)
            //  Offspring 1:    _______________________________
            //                 |_X_|_X_●_Y_|_Y_|_Y_●_X_|_X_|_X_●
            //                 ●_Y_|_Y_|_Y_|_Y_●_X_|_X_●_Y_|_Y_|
            //                 |_X_|_X_●_Y_|_Y_●_X_|_X_●_Y_|_Y_|
            //                 |_X_●_Y_●_X_|_X_|_X_●_Y_|_Y_|_Y_|
            //                 |_X_|_X_|_X_●_Y_●_X_|_X_|_X_●_Y_|
            //
            //  Offspring 2:    _______________________________
            //                 |_Y_|_Y_●_X_|_X_|_X_●_Y_|_Y_|_Y_●
            //                 ●_X_|_X_|_X_|_X_●_Y_|_Y_●_X_|_X_|
            //                 |_Y_|_Y_●_X_|_X_●_Y_|_Y_●_X_|_X_|
            //                 |_Y_●_X_●_Y_|_Y_|_Y_●_X_|_X_|_X_|
            //                 |_Y_|_Y_|_Y_●_X_●_Y_|_Y_|_Y_●_X_|
            //
            //
            int maxNumberOfPoints = rand.Next(1, Chromosome.imageWidth / 2); // maximum point number in a row of image
            bool Mum_Dad_State_Changer = true; // True=mum , False=Dad for change copy reference state 
            int rowRandomPointIndex = 0; // Next Point Index
            int countPoints = 0; // Number of Created Points

            for (int y = 0; y < Chromosome.imageHeight; ++y) // Column Pixels Counter
            {
                rowRandomPointIndex = rand.Next(0, Chromosome.imageWidth - 1); // select point between 0 <---> Width
                countPoints++;

                for (int x = 0; x < Chromosome.imageWidth; ++x) // Row Pixels Counter
                {
                    if (x > rowRandomPointIndex && countPoints < maxNumberOfPoints) 
                    {
                        Mum_Dad_State_Changer = !Mum_Dad_State_Changer; // Change bitspring state
                        rowRandomPointIndex = rand.Next(rowRandomPointIndex + 1, Chromosome.imageWidth - 1); // select point between ● <---> Width
                    }
                    // Set Offspring Pixels Colors:
                    offspring.imageMatrix[x, y] = (Mum_Dad_State_Changer) ? Mum.imageMatrix[x, y] : Dad.imageMatrix[x, y];
                }
            }
            //
            return offspring;
        }
    }
}