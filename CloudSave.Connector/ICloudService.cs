using System.IO;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using System.Threading.Tasks;

namespace CloudSave.Connector
{
    public interface ICloudService
    {
        bool IsConnected { get; }
        string Name { get; }
        ICloudServiceSetting Settings { get; set; }
        ConnectionState State { get; set; }
        Icon Icon { get; }

        Control CreateControl();
        Task<Stream> GetFile(string file, CancellationToken cancellationToken = default(CancellationToken));
        Task UpdateFile(string file, CancellationToken cancellationToken = default(CancellationToken));
        Task UploadFile(string file, CancellationToken cancellationToken = default(CancellationToken));
    }
}