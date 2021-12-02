using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems; //키보드, 마우스, 터치 등의 사용자 입력을 이벤트로 오브젝트에 보낼 수 있는 기능을 지원

public class VirtualJoystick : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField]
    private RectTransform lever;
    private RectTransform rectTransform;

    [SerializeField, Range(10, 150)] //에디터에서 수정할 수 있는 SerializedField, 특정 범위 내에서만 값을 조정할 수 있도록 Range 어트리뷰트를 추가해준다.
    private float leverRange; //레버가 움직일 수 있는 거리

    private Vector2 inputDirection;
    private bool isInput;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }
    private void ControlJoystickLever(PointerEventData eventData)
    {
        var inputPos = eventData.position - rectTransform.anchoredPosition; //터치한 위치 확인
        var inputVector = inputPos.magnitude < leverRange ? inputPos : inputPos.normalized * leverRange;
        lever.anchoredPosition = inputVector;
        inputDirection = inputVector / leverRange;
    }
    private void InputControlVector()
    {
        //캐릭터에게 입력 벡터를 전달

    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        ControlJoystickLever(eventData);
        isInput = true;
    }
    public void OnDrag(PointerEventData eventData)
    {
        ControlJoystickLever(eventData);
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        lever.anchoredPosition = Vector2.zero; //레버의 위치를 원래대로 한다.
        isInput = false;
    }
}
