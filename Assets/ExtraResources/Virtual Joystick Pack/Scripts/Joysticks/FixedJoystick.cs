using UnityEngine;
using UnityEngine.EventSystems;

public class FixedJoystick : Joystick
{
    [Header("Fixed Joystick")]
    

    Vector2 joystickPosition = Vector2.zero;
    private Camera cam = new Camera();

    void Start()
    {
        joystickPosition = RectTransformUtility.WorldToScreenPoint(cam, background.position);
    }

    public override void OnDrag(PointerEventData eventData)
    {
        /*Vector2 direction = eventData.position - joystickPosition;
        inputVector = (direction.magnitude > background.sizeDelta.x / 2f) ? direction.normalized : direction / (background.sizeDelta.x / 2f);
        handle.anchoredPosition = (inputVector * background.sizeDelta.x / 2f) * handleLimit;*/
        Vector2 direction = eventData.position - joystickPosition;
        Debug.Log("Direction: "+direction);
        /*
        inputVector = (direction.magnitude > background.sizeDelta.x / 2f) ? direction.normalized : direction / (background.sizeDelta.x / 2f);
        handle.anchoredPosition = (inputVector * background.sizeDelta.x / 3f);*/
        
        Vector2 pos = Vector2.zero;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle
            (   background,
                eventData.position,
                eventData.pressEventCamera,
                out pos))
        {
            pos.x = (pos.x / background.sizeDelta.x);
            pos.y = (pos.y / background.sizeDelta.y);
            
            float x = (background.pivot.x == 1) ? pos.x * 2 + 1 : pos.x * 2 - 1;
            float y = (background.pivot.y == 1) ? pos.y * 2 + 1 : pos.y * 2 - 1;
            
            /*inputVector = new Vector3(pos.x * 2 + 1, 0, pos.y * 2 + 1);
            inputVector = (inputVector.magnitude > 1) ? inputVector.normalized : inputVector;*/

            Vector2 posAnchor = new Vector2(x,y);
            posAnchor = (posAnchor.magnitude > 1) ? posAnchor.normalized : posAnchor;
            inputVector = posAnchor;
            Debug.Log("Vector: "+posAnchor);
            Debug.Log("Click: "+eventData.position);
            Debug.Log("Delta: "+background.sizeDelta);
            //handle.anchoredPosition = new Vector3(inputVector.x * (background.sizeDelta.x / 3), inputVector.z * (background.sizeDelta.y / 3));
            handle.anchoredPosition = new Vector3(posAnchor.x*background.sizeDelta.x/3 , posAnchor.y*background.sizeDelta.y/3);
            Debug.Log("PosHand: "+handle.anchoredPosition);
        }
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        inputVector = Vector2.zero;
        handle.anchoredPosition = Vector2.zero;
    }
}