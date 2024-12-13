using Blog.CoreLayer.Utilities; // General utility functions
using Microsoft.AspNetCore.Http; // For handling file uploads
using System; // Provides the Exception and Guid classes
using System.IO; // For working with file and directory operations

namespace Blog.CoreLayer.Services.FileManager
{
    public class FileManager : IFileManager
    {
        // Method to delete a file given its name and path
        public void DeleteFile(string fileName, string path)
        {
            // Combine the current directory, provided path, and file name to get the full file path
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), path, fileName);

            // Check if the file exists at the specified path
            if (File.Exists(filePath))
                File.Delete(filePath); // Delete the file if it exists
        }

        // Method to save an uploaded file to a specified path
        public string SaveFile(IFormFile file, string savePath)
        {
            // Check if the file is null
            if (file == null)
                throw new Exception("File is null"); // Throw an exception if no file is provided

            // Generate a unique file name by appending a GUID to the original file name
            var fileName = $"{Guid.NewGuid()}{file.FileName}";

            // Combine the current directory with the save path, replacing forward slashes with backslashes
            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), savePath.Replace("/", "\\"));

            // Check if the directory exists, and create it if it doesn't
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            // Combine the folder path and file name to get the full path to save the file
            var fullPath = Path.Combine(folderPath, fileName);

            // Open a file stream and write the file to the specified path
            using var Stream = new FileStream(fullPath, FileMode.Create);
            file.CopyTo(Stream); // Copy the uploaded file to the stream

            return fileName; // Return the generated file name
        }
    }
}
