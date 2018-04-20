using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace IP_PGA.PGA
{
    public class Chromosome
    {
        public static byte[,] OriginalImageMatrix;

        // Chromosome is a Image Matrix
        public byte[,] imageMatrix;
        public static int imageWidth, imageHeight;

        /// <summary>
        /// long variable for save pixel colors distance 
        /// </summary>
        private long fitness;
        /// <summary>
        /// Read-Only Fitness of Chromosome
        /// </summary>
        public long Fitness 
        {
            get { return fitness; }
        } 
        
        /// <summary>
        /// Chromosome for save all pixel colors
        /// </summary>
        /// <param name="OriginalWidth">Width of original Picture</param>
        /// <param name="OriginalHeight">Height of original Picture</param>
        public Chromosome() // for define array length
        {
            imageMatrix = new byte[imageWidth, imageHeight];
        }

        /// <summary>
        /// clear offspring chromosome picture by 255
        /// </summary>
        public void clear(byte colorBit)
        {
            for (int x = 0; x < imageWidth; ++x)
                for (int y = 0; y < imageHeight; ++y)
                {
                    imageMatrix[x, y] = colorBit;
                }
        }

        /// <summary>
        /// Calculate Tour Distance
        /// </summary>
        /// <returns></returns>
        public void Calculate_Fitness()
        {
            this.fitness = 0;

            for (int x = 0; x < imageWidth; ++x)
                for (int y = 0; y < imageHeight; ++y)
                {
                    // Image is Gray so RGB parameters is equal by self: R=G=B
                    this.fitness += Math.Abs(OriginalImageMatrix[x, y] - imageMatrix[x, y]);
                }
            //
            // return  sum of pixel colors distances
            // return  this.fitness
        }

        /// <summary>
        /// Set Image Pixels by Random Gray Colors
        /// </summary>
        public void CreateBitmapRandomize()
        {
            Random rand = new Random();
            int bpc = 0;
            for (int x = 0; x < imageWidth; ++x)
                for (int y = 0; y < imageHeight; ++y)
                {
                    bpc = rand.Next(0, 255);
                    imageMatrix[x, y] = (byte)bpc;
                }
        }
    }
}
