using System.Text.Json;
using Newtonsoft.Json.Linq;
using System.Reflection;

namespace Lab10.Purple;

public class PurpleJsonFileManager<T> : PurpleFileManager<T> where T : Lab9.Purple.Purple
{
    public PurpleJsonFileManager(string name) : base(name)
    {
    }

    public PurpleJsonFileManager(string name, string folderPath, string fileName, string fileExtension = "") : base(name, folderPath, fileName, fileExtension)
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
        ChangeFileFormat("json");
    }
    
    public override void Serialize(T obj)
    {
        if (obj == null || string.IsNullOrEmpty(FullPath))
            return;

        JObject json = JObject.FromObject(obj); 

        json["Type"] = obj.GetType().AssemblyQualifiedName;

        if (!string.IsNullOrEmpty(FolderPath))
            Directory.CreateDirectory(FolderPath); 

        File.WriteAllText(FullPath, json.ToString()); 
    }
    
    public override T Deserialize()
    {
        if (string.IsNullOrEmpty(FullPath) || !File.Exists(FullPath))
            return null!;

        try
        {
            string jsonText = File.ReadAllText(FullPath);
            JObject json = JObject.Parse(jsonText);

            string? typeName = json["Type"]?.ToString();

            if (string.IsNullOrWhiteSpace(typeName))
                return null!;

            Type? type = Type.GetType(typeName); 

            if (type == null || !typeof(T).IsAssignableFrom(type))
                return null!;

            string input = json["Input"]?.ToString() ?? string.Empty;

            object? obj;

            if (type == typeof(Lab9.Purple.Task4))
                obj = Activator.CreateInstance(type, input, null); 
            else
                obj = Activator.CreateInstance(type, input); 

            if (obj == null)
                return null!;

            JToken? outputToken = json["Output"];

            if (outputToken != null)
            {
                FieldInfo? outputField = type.GetField("_output", BindingFlags.NonPublic | BindingFlags.Instance);

                if (outputField != null)
                {
                    if (outputField.FieldType == typeof(string))
                        outputField.SetValue(obj, outputToken.ToObject<string>());

                    else if (outputField.FieldType == typeof(string[]))
                        outputField.SetValue(obj, outputToken.ToObject<string[]>());
                }
            }

            return (T)obj;
        }
        catch
        {
            return null!;
        }
    }
}