using System;
using TMPro;
using UnityEngine;

public class MessageBox : MonoBehaviour
{
    public static MessageBox Instance = null;
    public TextMeshProUGUI MessageText;
    public GameObject Contents;

    void Awake()
    {
        Instance = this;
        HideMessage();
    }

    public void ShowMessage(string message)
    {
        if (Contents.activeSelf)
        {
            CancelInvoke("HideMessage");
        }
        MessageText.text = message;
        Contents.SetActive(true);

        Invoke("HideMessage", 3f);
    }

    public void HideMessage()
    {
        MessageText.text = string.Empty;
        Contents.SetActive(false);
    }
}
