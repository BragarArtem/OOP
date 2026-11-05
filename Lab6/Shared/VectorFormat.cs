namespace Shared;
using System.Globalization;
public static class VectorFormat
{
    public static string ToText(double[] items)
    {
        string[] texts = items.Select(x => x.ToString(CultureInfo.InvariantCulture)).ToArray();
        return string.Join(';', texts);
    }
    public static bool TryParse(string text, out double[] items)
    {
        items = Array.Empty<double>();
        if(string.IsNullOrEmpty(text)){return false;}
        string[] parts = text.Split(';');
        double[] values = new double[parts.Length];
        for(int i =0; i < values.Length; i++)
        {
            if(!double.TryParse(parts[i], NumberStyles.Float, CultureInfo.InvariantCulture, out values[i]))
            {
                return false;
            }
        }
        items = values;
        return true;
    }
}