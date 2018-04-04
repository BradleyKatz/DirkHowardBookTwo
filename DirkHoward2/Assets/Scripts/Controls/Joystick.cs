using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Joystick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    private Image joystickBase, joystickTouchpad;

    public Vector3 inputDirection;

	void Start ()
    {
        joystickBase = GetComponent<Image>();
        joystickTouchpad = transform.GetChild(0).GetComponent<Image>();
        inputDirection = Vector3.zero;
	}
	
    public void OnDrag(PointerEventData ped)
    {
        if (!GameVariables.GamePaused && !Player.instance.caught)
        {
            Vector2 position = Vector2.zero;

            RectTransformUtility.ScreenPointToLocalPointInRectangle(joystickBase.rectTransform, ped.position, ped.pressEventCamera, out position);

            position.x = position.x / joystickBase.rectTransform.sizeDelta.x;
            position.y = position.y / joystickBase.rectTransform.sizeDelta.y;

            //float x = (joystickBase.rectTransform.pivot.x == 1f) ? position.x * 2 + 1 : position.x * 2 - 1;
            //float y = (joystickBase.rectTransform.pivot.y == 1f) ? position.y * 2 + 1 : position.y * 2 - 1;
            //Debug.Log("x= " + position.x.ToString());
            // Debug.Log("y= " + position.y.ToString());
            inputDirection = new Vector3(position.x, position.y, 0);
            inputDirection = (inputDirection.magnitude > 1) ? inputDirection.normalized : inputDirection;

            joystickTouchpad.rectTransform.anchoredPosition = new Vector3(inputDirection.x * (joystickBase.rectTransform.sizeDelta.x / 3), inputDirection.y * (joystickBase.rectTransform.sizeDelta.y) / 3);
        }
    }

    public void OnPointerDown(PointerEventData ped) { }

    public void OnPointerUp(PointerEventData ped)
    {
        if (!GameVariables.GamePaused)
        {
            inputDirection = Vector3.zero;
            joystickTouchpad.rectTransform.anchoredPosition = Vector3.zero;
        }
    }
}
