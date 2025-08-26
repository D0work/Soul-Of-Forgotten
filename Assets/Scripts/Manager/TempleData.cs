using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempleData : MonoBehaviour
{
    public static TempleData Instance;

    public string materialKey;
    void Awake()
    {
        if (Instance != null)
        {
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

}
