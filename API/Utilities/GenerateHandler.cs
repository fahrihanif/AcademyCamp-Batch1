namespace API.Utilities;

public class GenerateHandler
{
    public static string Nik(string? nik)
    {
        if (nik == null) return "111111";
        var convertToInt = Convert.ToInt32(nik);
        return Convert.ToString(convertToInt + 1);
    }
}
