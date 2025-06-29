using UnityEngine;

public class HoverTipManager : MonoBehaviour
{
    static HoverTipManager Instance;

    public float defaultScreenHeight = 600;

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
        rect.localScale = Vector3.one * (Screen.height / defaultScreenHeight);
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
