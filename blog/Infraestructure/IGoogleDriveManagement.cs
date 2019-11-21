using System.IO;
using System.Threading.Tasks;

namespace blog.Infraestructure
{
    public interface IGoogleDriveManagement
    {
         Task<string> UploadAsync(FileStream file);

         Task<bool> DeleteAsync(string file);

         Task<bool> IsExist(string file);         

    }
}