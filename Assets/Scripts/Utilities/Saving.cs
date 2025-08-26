using UnityEngine;

public class Saving : MonoBehaviour
{
    public void SaveGame()
    {
        SaveSystem.Saving();
    }

    public void LoadGame()
    {
        var data = SaveSystem.Loading();
    }
}