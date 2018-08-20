using CloudSave.Connector.Auth;

using Google.Apis.Auth.OAuth2;

namespace CloudSave.GoogleDrive.Extensions
{
    public static class DriveEx
    {
        public static ClientSecrets ToClientSecrets(this ICloudServiceAuth auth)
        {
            return new ClientSecrets
            { 
                ClientId = auth.ClientId,
                ClientSecret = auth.ClientSecret
            };
        }
    }
}
