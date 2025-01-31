using UnityEngine;

public class HUDLogic : MonoBehaviour
{
    RectTransform rectTransform;
    int actual_special_points = 0;
    void Start()
    {
       rectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        actual_special_points = GameManager.Instance.ReturnSpecialData();

        rectTransform.sizeDelta = new Vector2((actual_special_points),5);
    }
}
