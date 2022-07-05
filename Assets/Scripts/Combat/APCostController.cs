using UnityEngine;
using TMPro;

public class APCostController : MonoBehaviour
{
    public TextMeshProUGUI label;
    public RectTransform rt;
    public Camera cam;

    public void UpdateAP(Vector3 pos, float cost)
    {
        if (cam == null)
            FindCamera();

        rt.transform.position = cam.WorldToScreenPoint(pos);
        label.text = $"AP: {Mathf.CeilToInt(cost)}";
        rt.gameObject.SetActive(true);
    }

    private void FindCamera()
    {
        cam = Camera.main;
    }

}
