using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recipe.Application.Interfaces
{ 
    public interface IFileStorageService 
    {
        Task<string> SaveFileAsync(byte[] content, string containerName, string fileName);
        Task DeleteFileAsync(string filePath);
    }
}
