using System.IO;
using UnityEditor;
using UnityEngine;

public static class DevKitUpdate
{
    [MenuItem("DevKit/Update Target")]
    public static void UpdateTarget()
    {
        const string sourceFolderPath = "/Users/maxim/Git/Maxim/devkit/devkit_unity/Assets/_DevKit";
        const string destinationFolderPath = "/Users/maxim/Git/Solan/spacecraze/Assets/_DevKit";

        var count = 0;
        count = DeleteFolder(destinationFolderPath, count);
        Debug.LogFormat("Deleted {0} folders and files", count);

        count = 0;
        count = CopyFolder(sourceFolderPath, destinationFolderPath, count);
        Debug.LogFormat("Copied {0} folders and files", count);
    }

    private static int DeleteFolder(string folderPath, int count)
    {
        if (!Directory.Exists(folderPath))
        {
            Debug.LogFormat("Folder does not exist: {0}", folderPath);
            return count;
        }

        // Delete all files in the folder
        foreach (var file in Directory.GetFiles(folderPath))
        {
            File.Delete(file);
            //Debug.LogFormat("Deleted file: {0}", file);
            count++;
        }

        // Recursively delete all subfolders
        foreach (var subFolder in Directory.GetDirectories(folderPath))
        {
            DeleteFolder(subFolder, count);
        }

        // Delete the folder itself
        Directory.Delete(folderPath, true);
        count++;
        //Debug.LogFormat("Deleted folder: {0}", folderPath);
        return count;
    }


    private static int CopyFolder(string sourceFolder, string destinationFolder, int count)
    {
        if (!Directory.Exists(destinationFolder))
        {
            Directory.CreateDirectory(destinationFolder);
            //Debug.LogFormat("Created folder: {0}", destinationFolder);
            count++;
        }

        foreach (var file in Directory.GetFiles(sourceFolder))
        {
            var destinationFile = Path.Combine(destinationFolder, Path.GetFileName(file));
            File.Copy(file, destinationFile, true);
            //Debug.LogFormat("Copied file: {0}", destinationFile);
            count++;
        }

        foreach (var subFolder in Directory.GetDirectories(sourceFolder))
        {
            var destinationSubFolder = Path.Combine(destinationFolder, Path.GetFileName(subFolder));
            CopyFolder(subFolder, destinationSubFolder, count);
        }

        return count;
    }

}
