using System.Threading.Tasks;

namespace Demo.Services
{

    public interface IFileAccessService
    {

        Task<byte[]> GetFileContent();

    }

}
