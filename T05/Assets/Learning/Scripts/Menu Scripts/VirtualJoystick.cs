using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems; //Ű����, ���콺, ��ġ ���� ����� �Է��� �̺�Ʈ�� ������Ʈ�� ���� �� �ִ� ����� ����

public class VirtualJoystick : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField]
    private RectTransform lever;
    private RectTransform rectTransform;

    [SerializeField, Range(10, 150)] //�����Ϳ��� ������ �� �ִ� SerializedField, Ư�� ���� �������� ���� ������ �� �ֵ��� Range ��Ʈ����Ʈ�� �߰����ش�.
    private float leverRange; //������ ������ �� �ִ� �Ÿ�

    private Vector2 inputDirection;
    private bool isInput;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }
    private void ControlJoystickLever(PointerEventData eventData)
    {
        var inputPos = eventData.position - rectTransform.anchoredPosition; //��ġ�� ��ġ Ȯ��
        var inputVector = inputPos.magnitude < leverRange ? inputPos : inputPos.normalized * leverRange;
        lever.anchoredPosition = inputVector;
        inputDirection = inputVector / leverRange;
    }
    private void InputControlVector()
    {
        //ĳ���Ϳ��� �Է� ���͸� ����

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
        lever.anchoredPosition = Vector2.zero; //������ ��ġ�� ������� �Ѵ�.
        isInput = false;
    }
}
