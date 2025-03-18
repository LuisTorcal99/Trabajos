namespace WPF_CHEAT_os.Utils 
{ 
    public class StringUtils
    {
        public static int? ConvertToNumber(string str)
        {
            if (!int.TryParse(str, out int val))
            {
                return null;
            }
            return val;
        }
    }
}