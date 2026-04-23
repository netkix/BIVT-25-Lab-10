// using Lab10;
// using System.Text.Json;
//
// namespace Lab10Test.Green
// {
//     [TestClass]
//     public sealed class GeneralTest
//     {
//         private class TestManager : Lab10.Green.GreenFileManager
//         {
//             public TestManager(string name) : base(name) { }
//
//             public override void Serialize<T>(T obj)
//             {
//             }
//
//             public override T Deserialize<T>()
//             {
//                 return null;
//             }
//         }
//
//         [TestMethod]
//         public void Test_00_OOP_IFileManager()
//         {
//             var fileManagerInterface = typeof(IFileManager);
//
//             Assert.IsTrue(fileManagerInterface.IsInterface);
//
//             Assert.IsNotNull(fileManagerInterface.GetProperty("FolderPath"));
//             Assert.IsNotNull(fileManagerInterface.GetProperty("FileName"));
//             Assert.IsNotNull(fileManagerInterface.GetProperty("FileExtension"));
//             Assert.IsNotNull(fileManagerInterface.GetProperty("FullPath"));
//
//             Assert.IsNotNull(fileManagerInterface.GetMethod("SelectFolder", new[] { typeof(string) }));
//             Assert.IsNotNull(fileManagerInterface.GetMethod("SelectFile", new[] { typeof(string) }));
//             Assert.IsNotNull(fileManagerInterface.GetMethod("ChangeFormat", new[] { typeof(string) }));
//         }
//
//         [TestMethod]
//         public void Test_01_OOP_IFileLifeController()
//         {
//             var lifeInterface = typeof(IFileLifeController);
//
//             Assert.IsTrue(lifeInterface.IsInterface);
//
//             Assert.IsNotNull(lifeInterface.GetMethod("CreateFile"));
//             Assert.IsNotNull(lifeInterface.GetMethod("EditFile", new[] { typeof(string) }));
//             Assert.IsNotNull(lifeInterface.GetMethod("ChangeFileExtension", new[] { typeof(string) }));
//             Assert.IsNotNull(lifeInterface.GetMethod("DeleteFile"));
//         }
//
//         [TestMethod]
//         public void Test_02_OOP_ISerializer()
//         {
//             var serializerInterface = typeof(ISerializer);
//
//             Assert.IsTrue(serializerInterface.IsInterface);
//
//             var serialize = serializerInterface.GetMethod("Serialize");
//             Assert.IsNotNull(serialize);
//             Assert.IsTrue(serialize.IsGenericMethod);
//
//             var deserialize = serializerInterface.GetMethod("Deserialize");
//             Assert.IsNotNull(deserialize);
//             Assert.IsTrue(deserialize.IsGenericMethod);
//         }
//
//         [TestMethod]
//         public void Test_03_OOP_MyFileManager()
//         {
//             var managerType = typeof(MyFileManager);
//
//             Assert.IsTrue(managerType.IsAbstract);
//
//             Assert.IsTrue(typeof(IFileManager).IsAssignableFrom(managerType));
//             Assert.IsTrue(typeof(IFileLifeController).IsAssignableFrom(managerType));
//         }
//
//         [TestMethod]
//         public void Test_04_OOP_GreenFileManager()
//         {
//             var greenType = typeof(Lab10.Green.GreenFileManager);
//
//             Assert.IsTrue(greenType.IsAbstract);
//             Assert.IsTrue(greenType.IsSubclassOf(typeof(MyFileManager)));
//
//             Assert.IsTrue(typeof(ISerializer).IsAssignableFrom(greenType));
//
//             var serialize = greenType.GetMethod("Serialize");
//             Assert.IsTrue(serialize.IsAbstract);
//             Assert.IsTrue(serialize.IsGenericMethod);
//
//             var deserialize = greenType.GetMethod("Deserialize");
//             Assert.IsTrue(deserialize.IsAbstract);
//             Assert.IsTrue(deserialize.IsGenericMethod);
//
//             var edit = greenType.GetMethod("EditFile");
//             Assert.IsTrue(edit.IsVirtual);
//             Assert.IsFalse(edit.IsAbstract);
//
//             var changeExt = greenType.GetMethod("ChangeFileExtension");
//             Assert.IsTrue(changeExt.IsVirtual);
//             Assert.IsFalse(changeExt.IsAbstract);
//         }
//
//         [TestMethod]
//         public void Test_05_FileManager_Setup()
//         {
//             var manager = (IFileManager)new TestManager("test");
//
//             var folder = Directory.GetCurrentDirectory();
//
//             manager.SelectFolder(folder);
//             manager.ChangeFileName("task");
//
//             Assert.AreEqual(folder, manager.FolderPath);
//             Assert.AreEqual("task", manager.FileName);
//             Assert.IsTrue(manager.FullPath.Contains("task"));
//         }
//
//         [TestMethod]
//         public void Test_06_FileCreation()
//         {
//             var manager = (IFileManager)new TestManager("test");
//
//             var folder = Directory.GetCurrentDirectory();
//             Directory.CreateDirectory(folder);
//
//             manager.SelectFolder(folder);
//             manager.ChangeFileName("task");
//
//             ((IFileLifeController)manager).CreateFile();
//
//             Assert.IsTrue(File.Exists(manager.FullPath));
//         }
//
//         [TestMethod]
//         public void Test_07_ChangeFormat()
//         {
//             var manager = (IFileManager)new TestManager("test");
//
//             var folder = Path.Combine(Directory.GetCurrentDirectory(), "GreenFormatTest");
//             Directory.CreateDirectory(folder);
//
//             manager.SelectFolder(folder);
//             manager.ChangeFileName("task");
//
//             manager.ChangeFileFormat("json");
//
//             Assert.AreEqual("json", manager.FileExtension);
//             Assert.IsTrue(File.Exists(manager.FullPath));
//
//             Directory.Delete(folder, true);
//         }
//
//         [TestMethod]
//         public void Test_08_EditFile()
//         {
//             var manager = new TestManager("test");
//             var fileManager = (IFileManager)manager;
//
//             var folder = Path.Combine(Directory.GetCurrentDirectory(), "GreenEditTest");
//             Directory.CreateDirectory(folder);
//
//             fileManager.SelectFolder(folder);
//             fileManager.ChangeFileName("task");
//
//             manager.CreateFile();
//             manager.EditFile("HELLO");
//
//             var content = File.ReadAllText(fileManager.FullPath);
//
//             Assert.AreEqual("HELLO", content);
//
//             Directory.Delete(folder, true);
//         }
//
//         [TestMethod]
//         public void Test_09_ChangeFileExtension()
//         {
//             var manager = new TestManager("test");
//             var fileManager = (IFileManager)manager;
//
//             var folder = Path.Combine(Directory.GetCurrentDirectory(), "GreenExtTest");
//             Directory.CreateDirectory(folder);
//
//             fileManager.SelectFolder(folder);
//             fileManager.ChangeFileName("task");
//
//             manager.CreateFile();
//             manager.EditFile("DATA");
//
//             manager.ChangeFileExtension("json");
//
//             Assert.AreEqual("json", fileManager.FileExtension);
//             Assert.IsTrue(File.Exists(fileManager.FullPath));
//
//             var content = File.ReadAllText(fileManager.FullPath);
//
//             Assert.AreEqual("DATA", content);
//
//             Directory.Delete(folder, true);
//         }
//
//         [TestMethod]
//         public void Test_10_DeleteFile()
//         {
//             var manager = new TestManager("test");
//             var fileManager = (IFileManager)manager;
//
//             var folder = Path.Combine(Directory.GetCurrentDirectory(), "GreenDeleteTest");
//             Directory.CreateDirectory(folder);
//
//             fileManager.SelectFolder(folder);
//             fileManager.ChangeFileName("task");
//
//             manager.CreateFile();
//
//             Assert.IsTrue(File.Exists(fileManager.FullPath));
//
//             manager.DeleteFile();
//
//             var path = fileManager.FullPath;
//             manager.DeleteFile();
//
//             Assert.IsFalse(File.Exists(path),
//                 "File not deleted");
//
//             Directory.Delete(folder, true);
//         }
//         [TestMethod]
//         public void Test_11_Inheritance()
//         {
//             var fileManagerInterface = typeof(Lab10.IFileManager);
//             var lifeInterface = typeof(Lab10.IFileLifeController);
//             var serializerInterface = typeof(Lab10.IWhiteSerializer);
//             var managerType = typeof(Lab10.MyFileManager);
//
//             Assert.IsTrue(fileManagerInterface.IsAssignableFrom(managerType),
//                 "MyFileManager must implement IFileManager");
//
//             Assert.IsTrue(lifeInterface.IsAssignableFrom(managerType),
//                 "MyFileManager must implement IFileLifeController");
//         }
//     }
// }