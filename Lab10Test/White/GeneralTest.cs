// using Lab10;
// using Lab10.White;
// using Microsoft.VisualStudio.TestPlatform.Utilities;
// using System.Text.Json;
// namespace Lab10Test.White
// {
//     [TestClass]
//     public sealed class GeneralTest
//     {
//         private class TestManager : WhiteFileManager
//         {
//             public TestManager(string name) : base(name)
//             {
//             }
//
//             public override void Serialize(Lab9.White.White obj)
//             {
//             }
//             public override Lab9.White.White Deserialize()
//             {
//                 return null;
//             }
//         }
//         [TestMethod]
//         public void Test_00_OOP_IFileManager()
//         {
//             var fileManagerInterface = typeof(Lab10.IFileManager);
//
//             Assert.IsTrue(fileManagerInterface.IsInterface, "IFileManager must be interface");
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
//         [TestMethod]
//         public void Test_01_OOP_IFileLifeController()
//         {
//             var lifeInterface = typeof(Lab10.IFileLifeController);
//
//             Assert.IsTrue(lifeInterface.IsInterface, "IFileLifeController must be interface");
//
//             Assert.IsNotNull(lifeInterface.GetMethod("CreateFile", Type.EmptyTypes));
//             Assert.IsNotNull(lifeInterface.GetMethod("EditFile", new[] { typeof(string) }));
//             Assert.IsNotNull(lifeInterface.GetMethod("ChangeFileExtension", new[] { typeof(string) }));
//             Assert.IsNotNull(lifeInterface.GetMethod("DeleteFile", Type.EmptyTypes));
//         }
//         [TestMethod]
//         public void Test_02_OOP_IWhiteSerializer()
//         {
//             var serializerInterface = typeof(Lab10.IWhiteSerializer);
//
//             Assert.IsTrue(serializerInterface.IsInterface, "IWhiteSerializer must be interface");
//
//             Assert.IsNotNull(serializerInterface.GetMethod("Serialize", new[] { typeof(Lab9.White.White) }));
//             Assert.IsNotNull(serializerInterface.GetMethod("Deserialize", Type.EmptyTypes));
//         }
//         [TestMethod]
//         public void Test_03_OOP_MyFileManager()
//         {
//             var managerType = typeof(Lab10.MyFileManager);
//
//             Assert.IsTrue(managerType.IsClass, "MyFileManager must be class");
//             Assert.IsTrue(managerType.IsAbstract, "MyFileManager must be abstract");
//
//             Assert.IsNotNull(
//                 managerType.GetConstructor(new[] { typeof(string) }),
//                 "Constructor MyFileManager(string) missing"
//             );
//
//             Assert.IsNotNull(
//                 managerType.GetConstructor(new[] { typeof(string), typeof(string), typeof(string), typeof(string) }),
//                 "Constructor MyFileManager(string, string, string, string) missing"
//             );
//
//             var folderProp = managerType.GetProperty("FolderPath");
//             Assert.IsNotNull(folderProp);
//             Assert.IsTrue(folderProp.CanRead, "FolderPath must have getter");
//             Assert.IsTrue(folderProp.SetMethod?.IsPrivate, "FolderPath setter must be private");
//
//             var fileProp = managerType.GetProperty("FileName");
//             Assert.IsTrue(fileProp.CanRead);
//             Assert.IsTrue(!fileProp.CanWrite || fileProp.SetMethod.IsPrivate);
//
//             var extProp = managerType.GetProperty("FileExtension");
//             Assert.IsTrue(extProp.CanRead);
//             Assert.IsTrue(!extProp.CanWrite || extProp.SetMethod.IsPrivate);
//
//             var fullPathProp = managerType.GetProperty("FullPath");
//             Assert.IsTrue(fullPathProp.CanRead);
//             Assert.IsTrue(!fullPathProp.CanWrite || fullPathProp.SetMethod.IsPrivate);
//
//             Assert.IsNotNull(managerType.GetMethod("SelectFolder", new[] { typeof(string) }));
//             Assert.IsNotNull(managerType.GetMethod("SelectFile", new[] { typeof(string) }));
//             Assert.IsNotNull(managerType.GetMethod("ChangeFormat", new[] { typeof(string) }));
//             Assert.IsNotNull(managerType.GetMethod("CreateFile", Type.EmptyTypes));
//             Assert.IsNotNull(managerType.GetMethod("DeleteFile", Type.EmptyTypes));
//
//             var editMethod = managerType.GetMethod("EditFile");
//             Assert.IsTrue(editMethod.IsVirtual, "EditFile must be virtual");
//
//             var changeExtMethod = managerType.GetMethod("ChangeFileExtension");
//             Assert.IsTrue(changeExtMethod.IsVirtual, "ChangeFileExtension must be virtual");
//         }
//         [TestMethod]
//         public void Test_04_OOP_WhiteFileManager()
//         {
//             var serializerInterface = typeof(Lab10.IWhiteSerializer);
//             var managerType = typeof(Lab10.MyFileManager);
//             var whiteManagerType = typeof(Lab10.White.WhiteFileManager);
//
//             Assert.IsTrue(whiteManagerType.IsClass, "WhiteFileManager must be class");
//             Assert.IsTrue(whiteManagerType.IsAbstract, "WhiteFileManager must be abstract");
//
//             Assert.IsTrue(whiteManagerType.IsSubclassOf(managerType),
//                 "WhiteFileManager must inherit MyFileManager");
//
//             Assert.IsTrue(serializerInterface.IsAssignableFrom(whiteManagerType),
//                 "WhiteFileManager must implement IWhiteSerializer");
//
//             Assert.IsNotNull(
//                 whiteManagerType.GetConstructor(new[] { typeof(string) }),
//                 "WhiteFileManager must have constructor (string)"
//             );
//
//             Assert.IsNotNull(
//                 whiteManagerType.GetConstructor(
//                     new[] { typeof(string), typeof(string), typeof(string), typeof(string) }),
//                 "WhiteFileManager must have constructor (string, string, string, string)"
//             );
//
//             var serializeMethod = whiteManagerType.GetMethod("Serialize");
//             Assert.IsNotNull(serializeMethod, "Serialize not found");
//             Assert.IsTrue(serializeMethod.IsAbstract,
//                 "Serialize must be abstract in WhiteFileManager");
//
//             var deserializeMethod = whiteManagerType.GetMethod("Deserialize");
//             Assert.IsNotNull(deserializeMethod, "Deserialize not found");
//             Assert.IsTrue(deserializeMethod.IsAbstract,
//                 "Deserialize must be abstract in WhiteFileManager");
//
//             var editOverride = whiteManagerType.GetMethod("EditFile");
//             Assert.IsTrue(editOverride.IsVirtual,
//                 "EditFile must remain virtual");
//             Assert.IsFalse(editOverride.IsAbstract,
//                 "EditFile must be overridden (not abstract)");
//
//             var changeExtOverride = whiteManagerType.GetMethod("ChangeFileExtension");
//             Assert.IsTrue(changeExtOverride.IsVirtual,
//                 "ChangeFileExtension must remain virtual");
//             Assert.IsFalse(changeExtOverride.IsAbstract,
//                 "ChangeFileExtension must be overridden (not abstract)");
//         }
//         [TestMethod]
//         public void Test_05_Inheritance()
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
//         [TestMethod]
//         public void Test_06_FileManager_Setup()
//         {
//             var manager = (IFileManager)new TestManager("test");
//
//             var folder = Directory.GetCurrentDirectory();
//             manager.SelectFolder(folder);
//             manager.ChangeFileName("task");
//
//             Assert.AreEqual(folder, manager.FolderPath,
//                 "FolderPath not set");
//
//             Assert.AreEqual("task", manager.FileName,
//                 "FileName not set");
//
//             Assert.IsTrue(manager.FullPath.Contains("task"),
//                 "FullPath incorrect");
//         }
//         [TestMethod]
//         public void Test_07_FileCreation()
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
//             Assert.IsTrue(File.Exists(manager.FullPath),
//                 "File not created");
//         }
//         [TestMethod]
//         public void Test_08_ChangeFormat()
//         {
//             var manager = (IFileManager)new TestManager("test");
//
//             var folder = Path.Combine(Directory.GetCurrentDirectory(), "FormatTest");
//             Directory.CreateDirectory(folder);
//
//             manager.SelectFolder(folder);
//             manager.ChangeFileName("task");
//
//             manager.ChangeFileFormat("json");
//
//             Assert.AreEqual("json", manager.FileExtension,
//                 "Format not changed");
//
//             Assert.IsTrue(File.Exists(manager.FullPath),
//                 "File not created after format change");
//
//             Directory.Delete(folder, true);
//         }
//         [TestMethod]
//         public void Test_09_EditFile()
//         {
//             var manager = new TestManager("test");
//             var fileManager = (IFileManager)manager;
//
//             var folder = Path.Combine(Directory.GetCurrentDirectory(), "EditTest");
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
//             Assert.AreEqual("HELLO", content,
//                 "EditFile failed");
//
//             Directory.Delete(folder, true);
//         }
//         [TestMethod]
//         public void Test_10_ChangeFileExtension()
//         {
//             var manager = new TestManager("test");
//             var fileManager = (IFileManager)manager;
//
//             var folder = Path.Combine(Directory.GetCurrentDirectory(), "ExtTest");
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
//             Assert.AreEqual("json", fileManager.FileExtension,
//                 "Extension not changed");
//
//             Assert.IsTrue(File.Exists(fileManager.FullPath),
//                 "File with new extension not created");
//
//             var content = File.ReadAllText(fileManager.FullPath);
//
//             Assert.AreEqual("DATA", content,
//                 "Content lost after extension change");
//
//             Directory.Delete(folder, true);
//         }
//         [TestMethod]
//         public void Test_11_DeleteFile()
//         {
//             var manager = new TestManager("test");
//             var fileManager = (IFileManager)manager;
//
//             var folder = Path.Combine(Directory.GetCurrentDirectory(), "DeleteTest");
//             Directory.CreateDirectory(folder);
//
//             fileManager.SelectFolder(folder);
//             fileManager.ChangeFileName("task");
//
//             manager.CreateFile();
//
//             Assert.IsTrue(File.Exists(fileManager.FullPath),
//                 "File not created");
//
//             var path = fileManager.FullPath;
//             manager.DeleteFile();
//
//             Assert.IsFalse(File.Exists(path),
//                 "File not deleted");
//
//             Directory.Delete(folder, true);
//         }
//     }
// }