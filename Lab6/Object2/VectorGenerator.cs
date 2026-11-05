namespace Object2;
internal static class VectorGenerator
{
    public static double[] Generate(int amount, double min, double max)
    {
        double[] items = new double[amount];
        for(int i = 0; i < items.Length; i++)
        {
            double r = Random.Shared.NextDouble();
            double value = min + r * (max - min);
            items[i] = value;
        }
        return items;
    }
}