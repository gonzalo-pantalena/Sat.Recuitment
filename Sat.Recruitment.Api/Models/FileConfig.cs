using System.IO;

namespace Sat.Recruitment.Api.Models
{
    public class FileConfig
    {
        public string FilePath 
        { 
            get => _filePath;
            set => _filePath = $"{Directory.GetCurrentDirectory()}/{value}";
        }

        private string _filePath;
    }
}
