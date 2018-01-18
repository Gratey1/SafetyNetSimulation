using System;

public static class RandomExtended
{
    public static byte[] GenerateRandomData(int minLength, int maxLength)
    {
        Random r = new Random();
        return r.GetRandomData(minLength, maxLength);
    }

    public static byte[] GetRandomData(this Random r, int minLength, int maxLength)
    {
        if (minLength <= 0 || maxLength < minLength) return null;

        int length = r.Next(minLength, maxLength);

        byte[] ret = new byte[length];
        r.NextBytes(ret);

        return ret;
    }
}
