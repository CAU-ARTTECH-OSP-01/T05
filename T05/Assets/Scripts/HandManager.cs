using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandManager : MonoBehaviour //핸드 오브젝트에 컴포넌트로 넣어서 사용, Singleton 패턴
{
    private static HandManager instance; //이 게임 씬에서 단 하나만 존재하기 때문에, Singleton 패턴으로 만들어준다.
                                         //다른 곳에서 상속시켜 주지 않더라도 어디에서나 사용할 수 있는 방식이다.
    public static HandManager Instance
    {
        get { return instance; }
        set { instance = value; }
    }

    private void Awake()
    {
        instance = this;
    }


    public List<CardInfo> cardInfos; //핸드에 있는 카드 데이터를 가지고 있어야 한다.
                                     //덱에서 핸드로 카드를 가져올 때 데이터도 함께 가져옴.
    public List<GameObject> cardObjects; //Hierarchy 창에서의 어떤 카드인지 확인 가능

    public float gap = 64f; //카드가 핸드에 생성될 때 카드 사이 거리
    public void SetCardPositions() //카드가 핸드에 생성될 때 위치 조정
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetComponent<CardController>().defaultPos_x = -32 * (transform.childCount - 1) + gap * i; //카드의 위치 조정
            
            /*switch(i) //고정된 값을 알고 있다면 switch문을 사용해도 된다.
            {
                case 0:
                    transform.GetChild(i).GetComponent<CardController>().defaultPos_x = -375f;
                    break;
                case 1:
                    transform.GetChild(i).GetComponent<CardController>().defaultPos_x = -125f;
                    break;
                case 2:
                    transform.GetChild(i).GetComponent<CardController>().defaultPos_x = 125f;
                    break;
                case 3:
                    transform.GetChild(i).GetComponent<CardController>().defaultPos_x = 375f;
                    break;
            }*/

            transform.GetChild(i).GetComponent<CardController>().SetCardDefaultPos();
        }
    }
}
