namespace CloudSave.Connector.Auth
{
    public interface ICloudServiceAuth
    {
        string ClientId { get; set; }
        string ClientSecret { get; set; }
    }

    public static class CloudServiceAuthEx
    {
        public static bool HaveValues(this ICloudServiceAuth auth)
        {
            return !string.IsNullOrEmpty(auth?.ClientId) && 
                   !string.IsNullOrEmpty(auth.ClientSecret);
        }
    }
}