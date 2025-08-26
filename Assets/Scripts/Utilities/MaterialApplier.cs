using UnityEngine;
using System.Collections.Generic;

public class MaterialApplier : MonoBehaviour
{
    [Tooltip("Référence à la bibliothèque de matériaux")]
    public MaterialList materialList;

    [Tooltip("Clé du matériau à appliquer")]
    public string materialKey;

    [Tooltip("Objets à modifier")]
    public List<GameObject> objectsToApply;

    public void Applies()
    {
        if (materialList == null)
            return;

        materialKey = TempleData.Instance.materialKey;
        Material selectedMaterial = materialList.GetMaterialByKey(materialKey);

        Debug.Log($"material {materialKey} from {selectedMaterial}");

        if (selectedMaterial == null)
            return;

        foreach (var obj in objectsToApply)
        {
            if (obj == null)
                continue;

            MeshRenderer meshRenderer = obj.GetComponent<MeshRenderer>();
            if (meshRenderer != null)
            {
                int slotCount = meshRenderer.materials.Length;
                Material[] newMaterials = new Material[slotCount];

                for (int i = 0; i < slotCount; i++)
                {
                    newMaterials[i] = selectedMaterial;
                }

                meshRenderer.materials = newMaterials;
            }

            ParticleSystemRenderer psRenderer = obj.GetComponent<ParticleSystemRenderer>();
            if (psRenderer != null)
            {
                psRenderer.material = selectedMaterial;
            }
        }
    }

}
