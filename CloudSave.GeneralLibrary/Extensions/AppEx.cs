namespace CloudSave.GeneralLibrary.Extensions
{
    public static class AppEx
    {
        public static string ToApplicationName(this string text)
        {
            return $"{nameof(CloudSave)} - {text}";
        }
    }
}
