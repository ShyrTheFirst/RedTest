using UnityEngine;
using UnityEngine.UI;

public class StartGame : MonoBehaviour
{
    public void OnClick()
    {
        GameManager.Instance.ChangeState(true);
    }
}
