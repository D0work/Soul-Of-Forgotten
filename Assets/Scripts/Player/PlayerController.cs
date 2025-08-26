using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject ghost;
    public GameObject humanGhost;
    public GameObject humanShadow;
    public GameObject human;

    public void SwitchForm(string form)
    {
        ghost.SetActive(form == "Ghost");
        humanGhost.SetActive(form == "HumanGhost");
        humanShadow.SetActive(form == "HumanShadow");
        human.SetActive(form == "Human");
    }
}
