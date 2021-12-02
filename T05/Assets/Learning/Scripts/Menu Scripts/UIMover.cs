using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMover : MonoBehaviour
{
    private RectTransform rectTransform; //RectTransform ���� ����
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>(); //RectTransform�� �����´�.
    }
    void Update()
    {
        /*if (Input.GetMouseButton(0))
        {
            rectTransform.anchoredPosition = Input.mousePosition; //Anchor�� ��ġ�� �����ϵ��� �Ѵ�.
            //���콺 Ŭ�� ��ġ�� ������ Anchor�� ���� �Ʒ��� ������ �Ѵ�. (��ǥ ����)
        }*/

        //ũ�� ������ �Ϲ������� transform.localScale�� ����Ѵ�.
        if (Input.GetKey(KeyCode.KeypadPlus)) //Ű�е��� Plus�� Minus�� �Է����� �� ũ�Ⱑ ���ϵ��� �Ѵ�.
        {
            rectTransform.sizeDelta = rectTransform.sizeDelta * 1.1f; //UI�� ũ�� ������ ���ؼ��� sizeDelta�� �̿��Ѵ�.
        }
        if (Input.GetKey(KeyCode.KeypadMinus))
        {
            rectTransform.sizeDelta = rectTransform.sizeDelta * 0.9f;
        }
    }
}
