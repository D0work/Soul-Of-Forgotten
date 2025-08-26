using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriigerFinalScene : MonoBehaviour
{
    public GameObject finalPortal = null;
    void Start()
    {
        AttributeManager attribute = AttributeManager.attributeInstance;

        if (attribute.isFire && attribute.isFire && attribute.isWind)
        {
            finalPortal.SetActive(true);
        }
    }
}
