using UnityEngine;

public class HoverTipManager : MonoBehaviour
{
    static HoverTipManager Instance;

    private RectTransform rect;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        rect = GetComponent<RectTransform>();
        gameObject.SetActive(false);
    }

    void Update()
    {
        rect.anchoredPosition = Input.mousePosition + new Vector3(20, -20);
    }

    public static void ShowTip(string tip)
    {
        Instance.gameObject.SetActive(true);
        Instance.GetComponentInChildren<TMPro.TMP_Text>().text = tip;
    }

    public static void HideTip()
    {
        Instance.gameObject.SetActive(false);
    }
}
