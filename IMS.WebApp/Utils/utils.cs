namespace IMS.WebApp.Utils
{
    public static class Utils
    {
        public static string SplitCamelCase(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return input;

            var result = new System.Text.StringBuilder();
            foreach (var c in input)
            {
                if (char.IsUpper(c) && result.Length > 0)
                    result.Append(' ');
                result.Append(c);
            }
            return result.ToString();
        }
    }
}