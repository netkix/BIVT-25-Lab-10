namespace Lab10.Interfaces;

public interface IFileManager
{
    string FolderPath { get; }
    string FileName { get; }
    string FileExtension { get; }
    string FullPath { get; }

    void SelectFolder(string folderPath);
    void ChangeFileName(string fileName);
    void ChangeFileFormat(string fileExtension);
}