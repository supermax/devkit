using System.IO;
using UnityEditor;
using UnityEngine;

public class DevKitUpdate
{
    [MenuItem("DevKit/Update Target")]
    public static void UpdateTarget()
    {
        const string sourceFolderPath = "/Users/maxim/Git/Maxim/devkit/devkit_unity/Assets/_DevKit";
        const string destinationFolderPath = "/Users/maxim/Git/Solan/spacecraze/Assets/_DevKit";

        Directory.Delete(destinationFolderPath);
        Debug.LogFormat("Deleted folder: {0}", destinationFolderPath);

        CopyFolder(sourceFolderPath, destinationFolderPath);
    }

    private static void CopyFolder(string sourceFolder, string destinationFolder)
    {
        if (!Directory.Exists(destinationFolder))
        {
            Directory.CreateDirectory(destinationFolder);
            Debug.LogFormat("Created folder: {0}", destinationFolder);
        }

        foreach (var file in Directory.GetFiles(sourceFolder))
        {
            var destinationFile = Path.Combine(destinationFolder, Path.GetFileName(file));
            File.Copy(file, destinationFile, true);
            Debug.LogFormat("Copied file: {0}", destinationFile);
        }

        foreach (var subFolder in Directory.GetDirectories(sourceFolder))
        {
            var destinationSubFolder = Path.Combine(destinationFolder, Path.GetFileName(subFolder));
            CopyFolder(subFolder, destinationSubFolder);
        }
    }

}
