using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardController : MonoBehaviour //카드를 선택하고, 드래그하는 등 카드 행동 관련 스크립트
{
    public CardInfo info; //카드 사용 횟수를 가져오기 위해 선언, 컴포넌트에 카드 오브젝트를 넣어 사용

    public float defaultPos_x = 0f; //기본 x값
    public float expandRatio = 1.3f; //확대되었을 때 비율
    public float expandOffset_y = 70f; //확대되었을 때 y 이동 값
    public float defaultOffset_y = -100f; //기본 y값 (카드의 위치를 살짝 아래로 내리고 싶을 때)
    public float usePos_y = 400f; //카드가 사용되게 인식할 높이

    public bool isClicked;
    public bool isDeck = true; //핸드로 가게 되는 상황에서 false로 바꿔준다.

    public Camera cam;

    public void SetCardDefaultPos() //HandManager의 SetCardPositions에 넣어 처음 카드의 위치를 지정해주도록 한다.
    {
        transform.GetComponent<RectTransform>().anchoredPosition
            = new Vector3(defaultPos_x, defaultOffset_y, 0);
    }

    public void PointerEnter() //카드 오브젝트에 Event Trigger 컴포넌트 부착 후, Pointer Enter를 추가하여 사용한다.
    {
        if (!isDeck)
        {
            transform.localScale = Vector3.one * expandRatio; //카드 확대, new Vector3(expandRatio, expandRatio, expandRatio)와 같다.

            transform.GetComponent<RectTransform>().anchoredPosition //RectTransform의 anchoredPosition을 이용하면 Inspector창에서 보이는 좌표를 그대로 사용할 수 있다.
                = new Vector3(defaultPos_x, expandOffset_y, 0); //카드 위치 변화

            transform.SetSiblingIndex(transform.parent.childCount - 1); //SetSiblingIndex = 부모 안의 순서
                                                                        //카드의 순서를 맨 아래로 변경하여 UI상 가장 위에 보이도록 한다.
        }
    }
    public void PointerExit() //원래 상태로 되돌린다.
    {
        if (!isDeck)
        {
            transform.localScale = Vector3.one;

            transform.GetComponent<RectTransform>().anchoredPosition
                = new Vector3(defaultPos_x, defaultOffset_y, 0);
        }
    }

    public void CardClick()
    {
        if (!isClicked && !isDeck)
        {
            isClicked = true;
            cam = GameObject.Find("Main Camera").GetComponent<Camera>(); //Main Camera 오브젝트와 그 안의 Camera 컴포넌트를 가져온다.
        }
    }

    public void CardDrag()
    {
        if (isClicked && !isDeck)
        {
            Vector2 _mousePosition = Input.mousePosition; //마우스 위치를 _mousePosition으로 받아온다.
            Vector2 _targetPosition = cam.ScreenToWorldPoint(_mousePosition); // 마우스 위치를 월드 좌표로 지정한다.

            transform.position = _targetPosition; //카드 오브젝트의 월드 좌표를 마우스 위치로 지정한다.
        }
    }
    public void CardDrop()
    {
        if (isClicked && !isDeck)
        {
            if (transform.GetComponent<RectTransform>().anchoredPosition.y >= usePos_y) //사용 높이보다 높거나 같을 때 카드가 사용되도록 설정
            {
                print("카드 사용");
                info.CardUse(); //카드 사용에 대한 상황을 알려주기 위해 CardInfo의 CardUse 함수 사용
            }
            else //카드 사용이 되지 않았을 때 원래 위치로 돌아가도록 설정
            {
                transform.GetComponent<RectTransform>().anchoredPosition
                    = new Vector3(defaultPos_x, defaultOffset_y, 0); //기본 위치로 카드 돌아가도록 설정
            }

            isClicked = false; //isClicked 초기화
        }
    }
}
