using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Popup_DeleteCard : MonoBehaviour
{
    //카드 제거 팝업에 컴포넌트로 넣어주고, 카드 제거 버튼의 On Click 이벤트로 OpenDeck 함수를 실행하도록 설정해준다.
    public GameObject originCard; //제거 카드 Prefab
    public GameObject content; //카드 제거 팝업의 content 아래에 카드들을 생성해준다.
    public List<__CardStats> cardList; //Database의 카드 리스트를 변환하여 넣어주기 위한 리스트
    public List<GameObject> cards; //Content 오브젝트 아래에 있는 카드 자체들을 리스트로 선언

    public GameObject popup_Delete; //카드 제거 팝업을 열고 닫기 위한 용도
    public GameObject selectButton; //카드 선택 버튼을 활성화/비활성화 하기 위한 용도

    private void Update()
    {
        for (int i = 0; i < cards.Count; i++)
        {
            if (cards[i].GetComponent<Popup_Card>().isClicked == true)
            {
                //OpenDeck(); 카드를 여러 장 제거하려면 주석 해제
                popup_Delete.SetActive(false);
                selectButton.SetActive(false);
            }
        }
    }

    void Init() //OpenDeck 함수를 실행할 때마다 카드가 계속해서 늘어나는 것을 방지하기 위해 초기화해주는 함수
                //덱 현황은 계속해서 바뀔 것이기 때문에 Refresh를 해줘야 한다.
    {
        cardList.Clear(); //리스트 Clear
        for (int i = 0; i < cards.Count; i++)
        {
            Destroy(cards[i]); //cards 리스트의 갯수만큼 반복하며 오브젝트를 제거해준다.
        }
        cards.Clear(); //모두 파괴가 되었다면 cards 리스트도 Clear 해준다.
    }

    public void OpenDeck() //플레이어가 가진 덱 리스트를 보여준다.
    {
        Init();

        List<Dictionary<string, string>> data = new List<Dictionary<string, string>>();
        data = DataReader_CSV.ReadDataByCSV("CardList"); //DataReader_TSV 스크립트를 통해 나눠진 데이터를 가져온다.

        DataBase.Instance.cardInventory.Sort();

        List<int> _list = DataBase.Instance.cardInventory; //DataBase의 카드 리스트를 가져온다.

        for (int i = 0; i < _list.Count; i++) //덱 안의 카드를 생성해주기 위해 덱의 갯수만큼 반복해준다.
        {
            cardList.Add(DeckManager.Instance.__SetCardInfo(data[_list[i]]));

            GameObject _obj = Instantiate(originCard, content.transform); //카드 오브젝트를 생성해준다.
            _obj.GetComponent<Popup_CardInfo>().SetCardInfo(cardList[i]); //각 카드에 맞는 정보를 넣어준다.

            cards.Add(_obj); //Hierarchy 창의 어디에 위치하는지 알아보기 위해 cards 리스트에 _obj를 넣어준다.
        }
    }
}
