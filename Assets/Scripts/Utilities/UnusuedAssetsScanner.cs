using UnityEditor;
using UnityEngine;
using System.IO;

public class UnusedAssetScanner
{
    [MenuItem("Tools/Scan Unused Assets")]
    static void Scan()
    {
        string outputFolder = "Assets/UnusedAssetsReport";
        string outputFile = Path.Combine(outputFolder, "unused_assets.txt");

        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }

        using (StreamWriter writer = new StreamWriter(outputFile))
        {
            string[] allAssets = AssetDatabase.GetAllAssetPaths();
            int count = 0;

            foreach (string asset in allAssets)
            {
                if (asset.StartsWith("Assets/") && !AssetDatabase.IsValidFolder(asset))
                {
                    string[] dependencies = AssetDatabase.GetDependencies(asset, true);
                    if (dependencies.Length <= 1) 
                    {
                        writer.WriteLine(asset);
                        count++;
                    }
                }
            }

            writer.WriteLine($"\nTotal assets possibly unused: {count}");
        }

        AssetDatabase.Refresh(); 
        Debug.Log($"Report : {outputFile}");
    }
}
