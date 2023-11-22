namespace LouieLib
{
    internal static class LouieUtils
    {
        public static string ToGameObjectName(this string source)
        {
            var result = string.Empty;
            for (var index = 0; index < source.Length; index++)
            {
                var symbol = source[index];
                if (char.IsUpper(symbol) && index != 0)
                {
                    result += "_" + symbol;
                }
                else
                {
                    result += char.ToUpper(symbol);
                }
            }

            return $"[{result}]";
        }
    }
}