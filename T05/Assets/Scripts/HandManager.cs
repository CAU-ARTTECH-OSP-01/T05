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

    public List<CardStats> cardInfos; //핸드에 있는 카드 데이터를 가지고 있어야 한다.
                                     //덱에서 핸드로 카드를 가져올 때 데이터도 함께 가져옴.
    public List<GameObject> cardObjects; //Hierarchy 창에서의 어떤 카드인지 확인 가능

    public float gap = 64f; //카드가 핸드에 생성될 때 카드 사이 거리

    public void Init() //드로우 전 핸드의 리스트들을 초기화해준다.
                //드로우할 때마다 핸드를 Refresh 해주도록 한다.
    {
        cardInfos.Clear(); //핸드로 카드를 가져올 때 생성하는 카드 데이터 리스트 Clear
        for (int i = 0; i < cardObjects.Count; i++)
        {
            Destroy(cardObjects[i]); //cardObjects 리스트의 갯수만큼 반복하며 오브젝트를 제거해준다.
        }
        cardObjects.Clear(); //오브젝트를 모두 파괴한 후 cards 리스트도 Clear
    }

    public void SetDrawCard(int _rnd) //카드를 드로우할 때 cardInfos 리스트에 넣어주도록 한다.
    {
        cardInfos.Add(DeckManager.Instance.deckCardStats[_rnd]);
    }

    public void SetCardPositions() //카드가 핸드에 생성될 때 위치 조정
    {
        for (int i = 0; i < cardInfos.Count; i++) //cardInfos의 순서대로 카드 배치
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


    public IEnumerator SetCardPositions_WaitDeleteCard() //카드가 사용된 후 삭제될 때까지 기다린 후 자리 배치를 하도록 한다.
    {
        yield return new WaitForSeconds(0.1f); //0.1초 정도 기다려 준다.

        for (int j = 0; j < cardObjects.Count; j++) //사용된 카드를 cardObjects 리스트에서 삭제해준다.
        {
            if (cardObjects[j] == null)
                cardObjects.RemoveAt(j);
        }

        for (int i = 0; i < cardObjects.Count; i++) //위의 SetCardPosition 주석 참고
        {
            cardObjects[i].GetComponent<CardController>().defaultPos_x = -32 * (transform.childCount - 1) + gap * i; //리스트의 순서에 맞게 카드의 위치 조정

            cardObjects[i].GetComponent<CardController>().SetCardDefaultPos(); //변화된 위치를 카드의 원래 위치로써 지정해준다.
        }
    }
    public void WaitDeleteCard() //CardInfo의 CardUse 스크립트에서 gameObject(CardInfo를 가진 오브젝트)가 삭제 되기 전 실행하기 위해 사용.
    {
        StartCoroutine(SetCardPositions_WaitDeleteCard());
    }
}
