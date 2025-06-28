using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class UIDraggable : MonoBehaviour
{
    [SerializeField]
    private Vector2 borderMin;
    [SerializeField]
    private Vector2 borderMax;
    [SerializeField]
    private Vector2 screenSize;
    [SerializeField]
    private Vector2 zoomBorder;

    private Vector2 targetPosition;
    private float targetScale = 1;

    //private WorldSpaceCanvasScaler worldSpaceCanvasScaler;

    [SerializeField]
    private RectTransform targetRectTransform;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //worldSpaceCanvasScaler = GetComponentInParent<WorldSpaceCanvasScaler>();
        if (targetRectTransform == null)
        {
            targetRectTransform = GetComponent<RectTransform>();
        }
        targetPosition = targetRectTransform.localPosition;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        targetRectTransform.localPosition = Vector2.Lerp(targetRectTransform.localPosition, targetPosition, 0.1f);
        targetRectTransform.localScale = Vector3.one * Mathf.Lerp(targetRectTransform.localScale.x, targetScale, 0.1f);
        print(targetRectTransform.localScale);
    }

    public void Drag(BaseEventData data)
    {
        PointerEventData pointerData = (PointerEventData)data;

        float scaleRatio = (float)Screen.height / Screen.width;
        targetPosition += pointerData.delta * scaleRatio;
        targetPosition.x = Mathf.Clamp(targetPosition.x, borderMin.x * targetScale - screenSize.x, borderMax.x * targetScale - screenSize.x);
        targetPosition.y = Mathf.Clamp(targetPosition.y, borderMin.y * targetScale - screenSize.y, borderMax.y * targetScale - screenSize.y);
    }

    public void Scroll(BaseEventData data)
    {
        PointerEventData pointerData = (PointerEventData)data;

        float scaleRatio = (float)Screen.height / Screen.width;

        Vector2 p = (pointerData.position - new Vector2(Screen.width, Screen.height) / 2) * scaleRatio;
        float t = targetScale;

        targetScale += pointerData.scrollDelta.y / 10f;
        targetScale = Mathf.Clamp(targetScale, zoomBorder.x, zoomBorder.y);

        targetPosition = p + (targetPosition - p) * targetScale / t;
    }
}
