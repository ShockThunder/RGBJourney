using System;
using System.Collections.Generic;
using System.Text;

namespace RgbJourney
{
    public class FieldGenerator
    {
        public int[,] GenerateArray(int arraySize)
        {
            var random = new Random();
            var array = new int[arraySize, arraySize];

            for (int i = 0; i < arraySize; i++)
            {
                for (int j = 0; j < arraySize; j++)
                {
                    array[i, j] = random.Next(3);
                }
            }

            return array;
        }
    }
}
