using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public static class SaveSystem 
{
    [System.Serializable]
    public class SaveData
    {
        public bool isFire;
        public bool isIce;
        public bool isWind;
        public bool isHero;
        public int level;
        public string sceneName;
    }
    private static readonly string saveFolder =
    Path.Combine(Application.persistentDataPath, "save");

    private static readonly string filePath =
        Path.Combine(saveFolder, "save.json");
    // Save
    public static void Saving()
    {
        if (AttributeManager.attributeInstance == null)
        {
            Debug.LogError("SaveSystem: no AttributeManager!");
            return;
        }

        var adata = AttributeManager.attributeInstance;
        var data = new SaveData
        {
            isFire = adata.isFire,
            isIce = adata.isIce,
            isWind = adata.isWind,
            isHero = adata.isHero,
            level = adata.level,
            sceneName = SceneManager.GetActiveScene().name
        };

        EnsureSaveDirectory();
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(filePath, json);

        Debug.Log($"SaveSystem: saving in {filePath}");
    }

    // Check
    public static bool HasSave()
    {
        return File.Exists(filePath);
    }
    private static void EnsureSaveDirectory()
    {
        var dir = Path.GetDirectoryName(filePath);
        if (!Directory.Exists(dir))
            Directory.CreateDirectory(dir);
    }


    // Load
    public static SaveData Loading()
    {
        if (!HasSave())
        {
            Debug.LogWarning("SaveSystem: no save.");
            return null;
        }

        string json = File.ReadAllText(filePath);
        var data = JsonUtility.FromJson<SaveData>(json);

        if (AttributeManager.attributeInstance == null)
        {
            Debug.LogError("SaveSystem: no AttributeManager found!");
            return data;
        }

        var adat = AttributeManager.attributeInstance;
        adat.isFire = data.isFire;
        adat.isIce = data.isIce;
        adat.isWind = data.isWind;
        adat.isHero = data.isHero;
        adat.level = data.level;

        Debug.Log($"SaveSystem: sauvegarde loading from {filePath}");
        return data;
    }

}
