using System.IO;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace Demo.Services
{

    public sealed class FileAccessService : IFileAccessService
    {

        #region IFileAccessService Members

        public async Task<byte[]> GetFileContent()
        {
            var result = await MediaPicker.PickPhotoAsync();
            if (result == null)
                return null;

            return File.ReadAllBytes(result.FullPath);
        }

        #endregion

    }

}
