using System;

public static class ArrayGenerator
{
    private static int width = 16;
    private static int height = 8;
    private static Random rng = new Random();
    private static int[,] array = new int[width, height];
    public static int[,] GetArray()
    {
        for(int i=0; i<width; ++i)
        {
            for(int j=0; j<height; ++j)
            {
                array[i, j] = rng.Next(0, 3);
            }
        }
        return array;
    }
        
}