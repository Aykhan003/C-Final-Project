namespace ConsoleApp51.Extensions;

internal static class StringExtensions
{
    public static bool IsValidEmail(this string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            return false;
        try
        {
            var addr = new System.Net.Mail.MailAddress(email);
            return addr.Address == email;
        }
        catch
        {
            return false;
        }
    }
    public static void ToTitleCase(this string str)
    {
        if (string.IsNullOrWhiteSpace(str))
            return;
        System.Globalization.CultureInfo cultureInfo = System.Threading.Thread.CurrentThread.CurrentCulture;
        System.Globalization.TextInfo textInfo = cultureInfo.TextInfo;
        string titleCase = textInfo.ToTitleCase(str.ToLower());
        Console.WriteLine(titleCase);
    }
    public static bool IsNullOrEmpty(this string str)
    {
        return string.IsNullOrEmpty(str);
    }
}
