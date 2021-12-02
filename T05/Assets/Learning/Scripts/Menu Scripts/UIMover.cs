using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMover : MonoBehaviour
{
    private RectTransform rectTransform; //RectTransform 변수 선언
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>(); //RectTransform을 가져온다.
    }
    void Update()
    {
        /*if (Input.GetMouseButton(0))
        {
            rectTransform.anchoredPosition = Input.mousePosition; //Anchor로 위치를 변경하도록 한다.
            //마우스 클릭 위치로 가려면 Anchor를 왼쪽 아래로 잡아줘야 한다. (좌표 개념)
        }*/

        //크기 조절은 일반적으로 transform.localScale로 사용한다.
        if (Input.GetKey(KeyCode.KeypadPlus)) //키패드의 Plus와 Minus를 입력했을 때 크기가 변하도록 한다.
        {
            rectTransform.sizeDelta = rectTransform.sizeDelta * 1.1f; //UI의 크기 조절을 위해서는 sizeDelta를 이용한다.
        }
        if (Input.GetKey(KeyCode.KeypadMinus))
        {
            rectTransform.sizeDelta = rectTransform.sizeDelta * 0.9f;
        }
    }
}
