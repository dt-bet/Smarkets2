

namespace Smarkets.Model
{

    public static class StringExtension
    {
        public static string GetLast(this string source, int tail_length)
        {
            if (tail_length >= source.Length)
                return source;
            return source.Substring(source.Length - tail_length);
        }


        public static string FirstLetterToUpper(string str)
        {
            if (str == null)
                return null;

            if (str.Length > 1)
                return char.ToUpper(str[0]) + str.Substring(1);

            return str.ToUpper();
        }

        public static bool HasAnyDigits(this string str)
        {
            foreach (char c in str)
            {
                if (c >= '0' & c <= '9')
                    return true;
            }

            return false;
        }

    }
}
