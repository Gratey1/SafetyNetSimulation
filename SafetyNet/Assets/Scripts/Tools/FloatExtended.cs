
public static class FloatExtended
{
    public static bool AlmostEquals(this float f1, float f2)
    {
        return FloatExtended.AlmostEqual(f1, f2);
    }

    public static bool AlmostZero(this float f1)
    {
        return AlmostEqual(f1, 0.0f);
    }

    private static bool AlmostEqual(float f1, float f2)
    {
        float dif = f1 - f2;
        return (dif < 0.000001f && dif > -0.000001f);
    }
}
