using System;
using System.Threading.Tasks;

namespace MultipleSensors.Helpers
{
    public interface IShare
    {
        Task Show(string title, string message, string filePath);
    }
}
