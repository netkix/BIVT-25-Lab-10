namespace Lab10.Interfaces;

public interface IFileLifeController
{
    void CreateFile();
    void DeleteFile();
    void EditFile(string text);
    void ChangeFileExtension(string fileExtension);
    
}