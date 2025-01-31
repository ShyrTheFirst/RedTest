using UnityEngine;

public class ButtonLogic : MonoBehaviour
{
    public GameObject button_ref;
    void Update()
    {
        button_ref.SetActive(!GameManager.Instance.CanPlay());
    }
}
