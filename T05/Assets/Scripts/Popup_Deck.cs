using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Popup_Deck : MonoBehaviour 
    //팝업_덱에 컴포넌트로 넣어주고, 덱 버튼의 On Click 이벤트로 팝업_덱의 Popup_Deck 스크립트 안의 OpenDeck 함수를 실행하도록 설정해준다.
{
    public GameObject originCard; //카드 오브젝트(Prefab)를 가져온다.
                                  //CardInfo를 선언해주지는 않았지만, 카드 오브젝트가 해당 스크립트를 가지고 있기 때문에 접근이 가능하다.
    public GameObject content; //originCard(카드 오브젝트)를 생성해 줄 부모를 선언
                               //팝업_덱 > 주사위 리스트 > Viewport > Content 아래에 생성해준다.
    public List<CardStats> cardList; //CardInfo에서 만들었던 CardStats를 가져온다.
                                     //DeckManager에서 카드의 종류와 갯수에 맞게 카드를 생성해주기 위해 선언
    public List<GameObject> cards; //Content 오브젝트 아래에 있는 카드 자체들을 리스트로 선언

    void Init() //OpenDeck 함수를 실행할 때마다 카드가 계속해서 늘어나는 것을 방지하기 위해 초기화해주는 함수
                //덱 현황은 계속해서 바뀔 것이기 때문에 Refresh를 해줘야 한다.
    {
        cardList.Clear(); //리스트 Clear
        for(int i = 0; i < cards.Count; i++)
        {
            Destroy(cards[i]); //cards 리스트의 갯수만큼 반복하며 오브젝트를 제거해준다.
        }
        cards.Clear(); //모두 파괴가 되었다면 cards 리스트도 Clear 해준다.
    }

    public void OpenDeck() //덱 현황에 플레이어가 지금 가지고 있는 카드들을 보여주기 위한 함수
    {
        Init();

        List<Dictionary<string, string>> data = new List<Dictionary<string, string>>();
        data = DataReader_TSV.ReadDataByTSV("Data/CardList"); //DataReader_TSV 스크립트를 통해 나눠진 데이터를 가져온다.

        List<int> _list = DeckManager.Instance.deckList; //Singleton으로 만들었던 DeckManager에 쉽게 접근할 수 있다.
                                                         //_list를 하나 만들고 deckList를 넣어 리스트를 얻어온다.

        for (int i = 0; i < _list.Count; i++) //덱 안의 카드를 생성해주기 위해 덱의 갯수만큼 반복해준다.
        {
            GameObject _obj = Instantiate(originCard, content.transform); //_obj로 GameObject를 선언하고, 카드를 생성해준다.
                                                                          //유니티에서 제공하는 Instantiate는 게임 오브젝트를 하나 생성해주는 함수이다.
                                                                          //originCard로 생성할 오브젝트와 생성할 부모의 위치(content.transform)를 넣어준다.
            _obj.GetComponent<CardInfo>().SetCardInfo(data[_list[i]]); //오브젝트에 존재하는 CardInfo 컴포넌트에 접근 후,
                                                                       //SetCardInfo(data[카드 번호])를 통해 넣어준다.
                                                                       //카드 번호는 DeckManager의 덱에서 가져온 리스트의 i번째를 넣어준다.
            cardList.Add(_obj.GetComponent<CardInfo>().stats); //카드 stats를 가져온다.
            cards.Add(_obj); //Hierarchy 창의 어디에 위치하는지 알아보기 위해 cards 리스트에 _obj를 넣어준다.
        }
    }
}
