using System.Reflection;
using System.Text;

namespace Lab10.Purple;

public class PurpleTxtFileManager<T> : PurpleFileManager<T> where T : Lab9.Purple.Purple
{
    public PurpleTxtFileManager(string name) : base(name)
    {
    }

    public PurpleTxtFileManager(string name, string folderPath, string fileName, string fileExtension = "txt") : base(name, folderPath, fileName, fileExtension)
    {
    }
    
    public override void EditFile(string text)
    {
        T obj = Deserialize();

        if (obj == null)
            return;

        obj.ChangeText(text);

        Serialize(obj);
    }
    
    public override void ChangeFileExtension(string newExtension)
    {
        ChangeFileFormat("txt");
    }
    
    public override void Serialize(T obj)
{
    if (obj == null || string.IsNullOrEmpty(FullPath))
        return;

    if (!string.IsNullOrEmpty(FolderPath))
        Directory.CreateDirectory(FolderPath);

    string output = Convert.ToBase64String(
        Encoding.UTF8.GetBytes(obj.ToString())
    );

    File.WriteAllText(
        FullPath,
        $"Type: {obj.GetType().AssemblyQualifiedName}\n" +
        $"Input: {obj.Input}\n" +
        $"Output: {output}"
    );
}

public override T Deserialize()
{
    if (string.IsNullOrEmpty(FullPath) || !File.Exists(FullPath))
        return null!;

    string[] lines = File.ReadAllLines(FullPath);

    string typeName = "";
    string input = "";
    string outputCode = "";

    for (int i = 0; i < lines.Length; i++)
    {
        if (lines[i].StartsWith("Type: "))
            typeName = lines[i].Substring("Type: ".Length);

        else if (lines[i].StartsWith("Input: "))
            input = lines[i].Substring("Input: ".Length);

        else if (lines[i].StartsWith("Output: "))
            outputCode = lines[i].Substring("Output: ".Length);
    }

    if (string.IsNullOrEmpty(typeName))
        return null!;

    Type? type = Type.GetType(typeName);

    if (type == null || !typeof(T).IsAssignableFrom(type))
        return null!;

    object? obj;

    if (type == typeof(Lab9.Purple.Task4))
        obj = Activator.CreateInstance(type, input, null);
    else
        obj = Activator.CreateInstance(type, input);

    if (obj == null)
        return null!;

    if (!string.IsNullOrEmpty(outputCode))
    {
        string output = Encoding.UTF8.GetString(
            Convert.FromBase64String(outputCode)
        );

        FieldInfo? field = type.GetField(
            "_output",
            BindingFlags.NonPublic | BindingFlags.Instance
        );

        if (field != null)
        {
            if (field.FieldType == typeof(string))
                field.SetValue(obj, output);

            else if (field.FieldType == typeof(string[]))
                field.SetValue(obj, output.Split('\n'));
        }
    }

    return (T)obj;
}
}