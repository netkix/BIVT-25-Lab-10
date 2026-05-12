using Lab10.Interfaces;

namespace Lab10.Purple;

public abstract class PurpleFileManager<T> : MyFileManager, ISerializer<T> where T : Lab9.Purple.Purple
{
    public PurpleFileManager(string name) : base(name)
    {
    }

    public PurpleFileManager(string name, string folderPath, string fileName, string fileExtension = "") : base(name, folderPath, fileName, fileExtension)
    {
    }

    public override void EditFile(string text)
    {
        if (!string.IsNullOrEmpty(FullPath) && File.Exists(FullPath))
            File.WriteAllText(FullPath, text ?? string.Empty);
    }
    
    public override void ChangeFileExtension(string newExtension)
    {
        if (string.IsNullOrEmpty(FullPath))
            return;
        string oldPath = FullPath;
        ChangeFileFormat(newExtension);
        string newPath = FullPath;
        if (File.Exists(oldPath) && oldPath != newPath)
        {
            if (File.Exists(newPath))
                File.Delete(newPath);

            File.Move(oldPath, newPath);
        }
    }
    
    public abstract void Serialize(T obj);
    public abstract T Deserialize();
}