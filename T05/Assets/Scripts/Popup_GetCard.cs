using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Popup_GetCard : MonoBehaviour
{
    public GameObject SelectCard; //생성할 카드 Prefab
    public GameObject content; //카드를 생성 시킬 위치
    public List<__CardStats> selectCards; //적이 드롭할 카드 리스트
    public List<GameObject> cards;
    public GameObject popup_Select; //카드 선택 팝업을 열고 닫기 위한 용도
    public GameObject selectButton; //카드 선택 버튼을 활성화/비활성화 하기 위한 용도

    public List<int> dropCards_Origin; //적이 드롭하는 카드 리스트
    public List<int> dropCards; //적이 드롭할 카드 리스트

    void Init()
    {
        selectCards.Clear();
        for (int i = 0; i < cards.Count; i++)
        {
            Destroy(cards[i]);
        }
        cards.Clear();
    }

    private void Awake()
    {
        RandomDrop(); //드롭하는 카드들을 미리 정해준다.
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

    public void RandomDrop()
    {
        for (int i = 0; i < GameManager.Instance.enemy.GetComponent<EnemyController>().dropCards.Count; i++) //원본이 훼손되지 않도록 한다.
        {
            int _cardNum = GameManager.Instance.enemy.GetComponent<EnemyController>().dropCards[i];
            dropCards_Origin.Add(_cardNum);
        }

        for (int j = 0; j < 3; j++)
        {
            int _rnd = Random.Range(0, dropCards_Origin.Count); //적 드롭 카드 리스트 중 랜덤한 값을 뽑는다.
            dropCards.Add(dropCards_Origin[_rnd]); //int 리스트에 적들이 드롭할 카드 리스트를 가져온다.
            dropCards_Origin.RemoveAt(_rnd); //랜덤 값이 겹치지 않게 뽑았던 값은 삭제해준다.
        }
    }

    public void OpenSelectCard()
    {
        Init();

        selectCards.Clear(); //실행될 때마다 selectCards의 요소가 추가될 것을 대비해 초기화를 시켜준다.

        List<Dictionary<string, string>> data = new List<Dictionary<string, string>>();
        data = DataReader_CSV.ReadDataByCSV("CardList");

        for (int i = 0; i < dropCards.Count; i++) //리스트의 수만큼 반복한다.
        {
            selectCards.Add(DeckManager.Instance.__SetCardInfo(data[dropCards[i]])); //selectCards 리스트에 int 리스트를 변환하여 넣어준다.
            GameObject _obj = Instantiate(SelectCard, content.transform); //카드 오브젝트를 content 내에 생성한다.
            _obj.GetComponent<Popup_CardInfo>().SetCardInfo(selectCards[i]); //생성한 카드 오브젝트에 selectCards의 정보를 넣어준다.

            cards.Add(_obj); //Hierarchy 창의 어디에 위치하는지 알아보기 위해 리스트에도 넣어준다.
        }
    }
}
