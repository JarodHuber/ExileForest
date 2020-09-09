using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Utils
{
    /// <summary>
    /// Returns an int based on a skewed chance
    /// </summary>
    /// <param name="values">The number of values that could be returned</param>
    /// <param name="probabilities">Array of chances for each value</param>
    /// <returns>Returns value between 0 and values-1</returns>
    public static int SkewedNum(int values, float[] probabilities)
    {
        float totalP = 0, miscHold = 0;

        float[] oneToHundred = new float[probabilities.Length + 1];

        int[] valStorage = new int[values];
        for (int x = 0; x < values; x++)
            valStorage[x] = x;

        float[] augmentedP = probabilities;

        for (int counter = 0; counter < augmentedP.Length; counter++)
            if (augmentedP[counter] != -1)
                totalP += augmentedP[counter];

        for (int counter = 0; counter < augmentedP.Length; counter++)
            if (augmentedP[counter] != -1)
            {
                miscHold += augmentedP[counter];
                oneToHundred[counter + 1] = (miscHold / totalP) - 0.01f;
            }

        float hold = (float)Random.Range(0, 1);
        for (int counter = 1; counter < oneToHundred.Length; counter++)
            if (oneToHundred[counter - 1] <= hold && hold < oneToHundred[counter])
                return valStorage[counter - 1];

        return valStorage[0];
    }
}
