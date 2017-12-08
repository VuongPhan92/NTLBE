namespace Domain.Utility
{
    public static class ConvertVie
    {
        private static readonly string[] VietNamChar = new string[]
            {
        "aAeEoOuUiIdDyY",
        "áàạảãâấầậẩẫăắằặẳẵ",
        "ÁÀẠẢÃÂẤẦẬẨẪĂẮẰẶẲẴ",
        "éèẹẻẽêếềệểễ",
        "ÉÈẸẺẼÊẾỀỆỂỄ",
        "óòọỏõôốồộổỗơớờợởỡ",
        "ÓÒỌỎÕÔỐỒỘỔỖƠỚỜỢỞỠ",
        "úùụủũưứừựửữ",
        "ÚÙỤỦŨƯỨỪỰỬỮ",
        "íìịỉĩ",
        "ÍÌỊỈĨ",
        "đ",
        "Đ",
        "ýỳỵỷỹ",
        "ÝỲỴỶỸ"
            };
        
        public static string Convert(string input)
        {
            if(!string.IsNullOrEmpty(input))
            {
               for (int i=1;i<VietNamChar.Length;i++)
                {
                    for( int j=0;j<VietNamChar[i].Length;j++)
                    {
                        input = input.Replace(VietNamChar[i][j], VietNamChar[0][i-1]);
                    }
                }
            }
            return input;
        }
    }
}
