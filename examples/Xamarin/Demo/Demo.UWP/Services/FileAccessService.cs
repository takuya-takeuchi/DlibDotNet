using System;
using System.Threading.Tasks;
using Windows.Storage.Streams;
using Demo.Services;

namespace Demo.UWP.Services
{

    public sealed class FileAccessService : IFileAccessService
    {

        #region IFileAccessService Members

        public async Task<byte[]> GetFileContent()
        {
            var picker = new Windows.Storage.Pickers.FileOpenPicker
            {
                ViewMode = Windows.Storage.Pickers.PickerViewMode.Thumbnail, 
                SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.PicturesLibrary
            };
            
            picker.FileTypeFilter.Add(".png");

            var file = await picker.PickSingleFileAsync();
            if (file == null)
                return null;

            using (var stream = await file.OpenAsync(Windows.Storage.FileAccessMode.Read))
            using (var read = new DataReader(stream.GetInputStreamAt(0)))
            {
                await read.LoadAsync((uint)stream.Size);
                var temp = new byte[stream.Size];
                read.ReadBytes(temp);
                return temp;
            }
        }

        #endregion

    }

}
