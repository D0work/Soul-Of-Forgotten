using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pages : MonoBehaviour
{
    [Header("Pages")]
    [SerializeField] private GameObject[] pages;

    public void ActivatePage(int index)
    {
        if (index < 0 || index >= pages.Length)
        {
            Debug.LogWarning($"ActivatePage: index {index} out of bounds.");
            return;
        }
        pages[index].SetActive(true);
    }

    public void DeactivatePage(int index)
    {
        if (index < 0 || index >= pages.Length)
        {
            Debug.LogWarning($"DeactivatePage: index {index} out of bounds");
            return;
        }
        pages[index].SetActive(false);
    }

    public void ShowPage(int index)
    {
        if (index < 0 || index >= pages.Length)
        {
            Debug.LogWarning($"ShowPage: index {index} out of bounds");
            return;
        }

        for (int i = 0; i < pages.Length; i++)
        {
            pages[i].SetActive(i == index);
        }
    }
}
