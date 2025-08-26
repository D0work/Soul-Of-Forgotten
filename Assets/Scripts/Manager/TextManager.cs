using UnityEngine;
using System.Collections.Generic;

public class TextManager : MonoBehaviour
{
    [Header("Liste des objets à afficher")]
    [SerializeField]
    private List<GameObject> textObjects = new List<GameObject>();

    // Index de l'objet actuellement affiché
    private int currentIndex = 0;

    private void Awake()
    {
        for (int i = 0; i < textObjects.Count; i++)
        {
            textObjects[i].SetActive(i == currentIndex);
        }
    }

    public void ShowNext()
    {
        if (textObjects.Count == 0)
            return;

        textObjects[currentIndex].SetActive(false);
        currentIndex = (currentIndex + 1) % textObjects.Count;
        textObjects[currentIndex].SetActive(true);
    }


    public void ShowPrevious()
    {
        if (textObjects.Count == 0)
            return;

        textObjects[currentIndex].SetActive(false);
        currentIndex = (currentIndex - 1 + textObjects.Count) % textObjects.Count;
        textObjects[currentIndex].SetActive(true);
    }
}
