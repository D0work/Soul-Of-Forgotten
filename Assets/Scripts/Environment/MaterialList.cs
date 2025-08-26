using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MaterialEntry
{
    public string key;
    public Material material;
}

[CreateAssetMenu(fileName = "MaterialList", menuName = "Temple/Material List")]
public class MaterialList : ScriptableObject
{
    [System.Serializable]
    public class MaterialEntry
    {
        public string key;
        public Material material;
    }

    public List<MaterialEntry> materials;

    public Material GetMaterialByKey(string key)
    {
        foreach (var entry in materials)
        {
            if (entry.key == key)
                return entry.material;
        }

        return null;
    }
}
