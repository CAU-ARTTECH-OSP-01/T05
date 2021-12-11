using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckManager : MonoBehaviour //덱 버튼에 컴포넌트로 넣어준다.
{
    private static DeckManager instance; //이 게임 씬에서 단 하나만 존재하기 때문에, Singleton 패턴으로 만들어준다.
                                         //다른 곳에서 상속시켜 주지 않더라도 어디에서나 사용할 수 있는 방식이다.
    public static DeckManager Instance
    {
        get { return instance; } //값을 얻었을 때 instance로 반환
        set { instance = value; } //value 값을 얻었을 때 instance로 넣어준다.
    }

    private void Awake() //start보다 전에 실행되는 함수
    {
        instance = this;
    }

    public List<int> deckList; //가지고 있는 카드의 종류와 갯수를 리스트에 넣어준다, Start할 때만 사용하고 이후부터는 데이터가 포함된 deckCardStats로 관리한다.

    public GameObject originCard; //카드 오브젝트 생성을 위해 선언
    public int drawCount; //드로우할 카드 수 선언

    private void Start()
    {
        SetDeckCardInfo(); //게임이 시작될 때 덱의 데이터로 가져오도록 한다.
    }

    public void GetHandCards() //덱의 카드를 핸드로 가져오는 함수, 턴 종료 버튼의 On Click 이벤트에 DeckManager를 가진 덱 버튼을 넣어주고, 설정한다.
    {
        for (int i = 0; i < drawCount; i++)
        {
            int _rnd = Random.Range(0, deckCardStats.Count); //0부터 덱 리스트에 있는 갯수 중 랜덤한 수를 뽑는다.

            GameObject _obj = Instantiate(originCard, HandManager.Instance.transform); //HandManager를 가지고 있는 핸드 위치에 카드 오브젝트 생성



            deckCardStats.RemoveAt(_rnd); //카드를 핸드로 생성함과 동시에 덱에서는 삭제해준다.
        }
    }

    public List<CardStats> deckCardStats; //위에서 int 리스트로 선언했던 deckList를 아래의 SetCardInfo와 SetDeckCardInfo를 통해 실제 데이터로 변환해준다.
                                          //Start에서 deckList를 이용하여 deckCardStats 리스트를 만들어 준 후부터는 이 리스트로 덱을 관리하도록 한다.

    public CardStats SetCardInfo(Dictionary<string, string> _data) //CardInfo의 SetCardInfo 주석 참고
                                                                   //CardStats 값을 반환해준다.
    {
        CardStats stats = new CardStats()
        {
            components = new List<ComponentStats>()
        };
        stats.components.Clear();

        stats.index = int.Parse(_data["Index"]);
        stats.name = _data["Name"];
        stats.count = int.Parse(_data["Count"]);
        stats.rare = int.Parse(_data["Rare"]);

        for (int i = 0; i < 6; i++)
        {
            CardComponent _component = CardComponent.ATK;

            if (_data["Component_" + i] == "ATK") 
                _component = CardComponent.ATK;
            else if (_data["Component_" + i] == "SHD")
                _component = CardComponent.SHD;
            else if (_data["Component_" + i] == "HEL")
                _component = CardComponent.HEL;
            else if (_data["Component_" + i] == "SEL")
                _component = CardComponent.SEL;

            ComponentStats _stats = new ComponentStats()
            {
                component = _component,
                value = int.Parse(_data["Value_" + i])
            };

            stats.components.Add(_stats);
        }
        return stats; //stats 값을 반환해준다.
    }

    void SetDeckCardInfo() //Popup_Deck의 OpenDeck 주석 참고
    {
        deckCardStats.Clear(); //실행될 때마다 deckCardStats의 요소가 추가될 것을 대비해 초기화를 시켜준다.

        List<Dictionary<string, string>> data = new List<Dictionary<string, string>>();
        data = DataReader_TSV.ReadDataByTSV("Data/CardList");

        List<int> _list = DeckManager.Instance.deckList;

        for (int i = 0; i < _list.Count; i++)
        {
            deckCardStats.Add(SetCardInfo(data[_list[i]])); //위의 SetCardInfo를 통해 정보를 받아온 후, deckCardStats 리스트에 넣어준다.
        }
    }
}
