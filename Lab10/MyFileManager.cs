using Lab10.Interfaces;

namespace Lab10;

public abstract class MyFileManager : IFileManager, IFileLifeController
{
    private string _name;
    private string _folderPath;
    private string _fileName;
    private string _fileExtension;

    public string Name => _name;
    public string FolderPath => _folderPath;
    public string FileName => _fileName;
    public string FileExtension => _fileExtension;

    public string FullPath
    {
        get
        {
            if (string.IsNullOrEmpty(_folderPath) || string.IsNullOrEmpty(_fileName)) {return string.Empty;}
            if (string.IsNullOrEmpty(_fileExtension)) {return Path.Combine(_folderPath, _fileName);} 
            return Path.Combine(_folderPath, $"{_fileName}.{_fileExtension}"); 
        }
    }

    public MyFileManager(string name)
    {
        _name = name;
    }
    
    public MyFileManager(string name, string folderPath, string fileName, string fileExtension = "")
    {
        _name = name;
        _folderPath = folderPath;
        _fileName = fileName;
        _fileExtension = fileExtension;
    }
    
    public void SelectFolder(string folderPath)
    {
        _folderPath = folderPath;
    }
    
    public void ChangeFileName(string fileName)
    {
        _fileName = fileName;
    }

    public void ChangeFileFormat(string fileExtension)
    {
        _fileExtension = fileExtension;

        if (!string.IsNullOrEmpty(FullPath)) 
        {
            CreateFile();
        }
    }

    public virtual void CreateFile()
    {
        if (string.IsNullOrEmpty(FullPath))
            return;
        if (!string.IsNullOrEmpty(_folderPath))
            Directory.CreateDirectory(_folderPath);
        if (!File.Exists(FullPath))
            File.Create(FullPath).Close();
    }
    
    public virtual void DeleteFile()
    {
        if (!string.IsNullOrEmpty(FullPath) && File.Exists(FullPath))
            File.Delete(FullPath);
    }
    
    public virtual void EditFile(string text)
    {
        if (!string.IsNullOrEmpty(FullPath) && File.Exists(FullPath))
            File.WriteAllText(FullPath, text ?? string.Empty); 
    }
    
    public virtual void ChangeFileExtension(string newExtension)
    {
        if (string.IsNullOrEmpty(FullPath))
            return;
        string oldPath = FullPath;
        ChangeFileFormat(newExtension);
        string newPath = FullPath;
        if (File.Exists(oldPath) && oldPath != newPath)
        {
            File.Move(oldPath, newPath);
        }
    }
}