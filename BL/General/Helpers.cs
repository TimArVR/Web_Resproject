namespace Web_siteResume.BL.General
{
    public static class Helpers //хелпер-враппер
    {
        public static int? StringToIntDef(string str, int? def)
        {
            int value;
            if (int.TryParse(str, out value))
                return value;
            return def;
        }

    }
}
