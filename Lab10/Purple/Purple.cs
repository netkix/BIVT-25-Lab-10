namespace Lab10.Purple;

public class Purple<T> where T : Lab9.Purple.Purple
{
    private T[] _tasks;
    private PurpleFileManager<T> _manager; 

    public PurpleFileManager<T> Manager => _manager;
    public T[] Tasks => _tasks;

    public Purple()
    {
        _tasks = Array.Empty<T>();
        _manager = null;
    }
    
    public Purple(T[] tasks)
    {
        _tasks = tasks == null ? Array.Empty<T>() : (T[])tasks.Clone(); 
        _manager = null;
    }

    public Purple(PurpleFileManager<T> manager, T[] tasks = null)
    {
        _manager = manager;
        _tasks = tasks == null ? Array.Empty<T>() : (T[])tasks.Clone();
    }

    public Purple(T[] tasks, PurpleFileManager<T> manager)
    {
        _tasks = tasks == null ? Array.Empty<T>() : (T[])tasks.Clone();
        _manager = manager;
    }
    
    public void Add(T task)
    {
        if (task == null)
            return;

        Array.Resize(ref _tasks, _tasks.Length + 1);
        _tasks[_tasks.Length - 1] = task;
    }

    public void Add(T[] tasks)
    {
        if (tasks == null)
            return;

        for (int i = 0; i < tasks.Length; i++)
        {
            Add(tasks[i]);
        }
    }
    
    public void Remove(T task)
    {
        if (task == null || _tasks.Length == 0)
            return;

        int index = -1;

        for (int i = 0; i < _tasks.Length; i++)
        {
            if (_tasks[i] == task)
            {
                index = i;
                break;
            }
        }

        if (index == -1)
            return;

        T[] newTasks = new T[_tasks.Length - 1];

        int j = 0;
        for (int i = 0; i < _tasks.Length; i++)
        {
            if (i == index)
                continue;

            newTasks[j++] = _tasks[i];
        }

        _tasks = newTasks;
    }
    
    public void Clear()
    {
        _tasks = Array.Empty<T>();

        if (_manager != null && !string.IsNullOrEmpty(_manager.FolderPath) && Directory.Exists(_manager.FolderPath))
        {
            Directory.Delete(_manager.FolderPath, true);
        }
    }
    
    public void SaveTasks()
    {
        if (_manager == null)
            return;

        for (int i = 0; i < _tasks.Length; i++)
        {
            _manager.ChangeFileName($"task{i}");
            _manager.Serialize(_tasks[i]);
        }
    }
    
    public void LoadTasks()
    {
        if (_manager == null)
            return;

        for (int i = 0; i < _tasks.Length; i++)
        {
            _manager.ChangeFileName($"task{i}");

            T task = _manager.Deserialize();

            _tasks[i] = task;
        }
    }
    
    public void ChangeManager(PurpleFileManager<T> manager)
    {
        if (manager == null)
            return;

        string folderPath;

        if (_manager != null && !string.IsNullOrEmpty(_manager.FolderPath))
        {
            folderPath = _manager.FolderPath;
        }
        else
        {
            folderPath = Path.Combine(
                Directory.GetCurrentDirectory(),
                manager.Name
            );

            Directory.CreateDirectory(folderPath);
        }

        manager.SelectFolder(folderPath);

        _manager = manager;
    }
}