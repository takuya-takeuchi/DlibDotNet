using Demo.Models;

namespace Demo.Services.Interfaces
{

    public interface IDetectService
    {

        DetectResult Detect(string file);

    }

}