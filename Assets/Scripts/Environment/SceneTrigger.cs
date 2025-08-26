using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTriggerDoor : MonoBehaviour
{
    [Tooltip("Scene Name")]
    public string sceneToLoad;

    public string materialKeyToSend = "wind";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            TempleData.Instance.materialKey = materialKeyToSend;
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
