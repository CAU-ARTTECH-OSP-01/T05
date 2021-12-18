using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Popup_GetCard : MonoBehaviour
{
    public GameObject SelectCard; //생성할 카드 Prefab
    public GameObject content; //카드를 생성 시킬 위치
    public List<CardStats> selectCards; //적이 드롭할 카드 리스트
    public List<GameObject> cards;
    public GameObject popup_Select; //카드 선택 팝업을 열고 닫기 위한 용도
    public GameObject selectButton; //카드 선택 버튼을 활성화/비활성화 하기 위한 용도

    void Init()
    {
        selectCards.Clear();
        for (int i = 0; i < cards.Count; i++)
        {
            Destroy(cards[i]);
        }
        cards.Clear();
    }

    private void Update()
    {
        for (int i = 0; i < cards.Count; i++)
        {
            if (cards[i].GetComponent<Popup_Card>().isClicked == true)
            {
                popup_Select.SetActive(false);
                selectButton.SetActive(false);
            }
        }
    }

    public void OpenSelectCard()
    {
        Init();

        selectCards.Clear(); //실행될 때마다 selectCards의 요소가 추가될 것을 대비해 초기화를 시켜준다.

        List<Dictionary<string, string>> data = new List<Dictionary<string, string>>();
        data = DataReader_TSV.ReadDataByTSV("Data/CardList");

        List<int> _list = GameManager.Instance.enemy.GetComponent<EnemyController>().dropCards; //int 리스트에 적들이 드롭할 카드 리스트를 가져온다.

        for (int i = 0; i < _list.Count; i++) //리스트의 수만큼 반복한다.
        {
            selectCards.Add(DeckManager.Instance.SetCardInfo(data[_list[i]])); //selectCards 리스트에 int 리스트를 변환하여 넣어준다.
            GameObject _obj = Instantiate(SelectCard, content.transform); //카드 오브젝트를 content 내에 생성한다.
            _obj.GetComponent<CardInfo>().SetCardInfo(selectCards[i]); //생성한 카드 오브젝트에 selectCards의 정보를 넣어준다.

            cards.Add(_obj); //Hierarchy 창의 어디에 위치하는지 알아보기 위해 리스트에도 넣어준다.
        }
    }
}
