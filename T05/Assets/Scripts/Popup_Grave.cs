using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Popup_Grave : MonoBehaviour
{
    public GameObject usedCard; //카드 Prefab
    public GameObject graveContent; //팝업_묘지의 Content 오브젝트
    public List<CardStats> cardList; //묘지로 이동한 카드 리스트
    public List<GameObject> cards; //Hierarchy상에서 보여지는 카드

    void Init()
    {
        cardList.Clear();
        for(int i = 0; i < cards.Count; i++)
        {
            Destroy(cards[i]);
        }
        cards.Clear();
    }
    
    public void OpenGrave()
    {
        Init();

        List<Dictionary<string, string>> data = new List<Dictionary<string, string>>();
        data = DataReader_TSV.ReadDataByTSV("Data/CardList");

        List<CardStats> _list = DeckManager.Instance.graveCardStats;

        for (int i = 0; i < _list.Count; i++)
        {
            GameObject _obj = Instantiate(usedCard, graveContent.transform);
            _obj.GetComponent<CardInfo>().SetCardInfo(_list[i]);

            cardList.Add(_obj.GetComponent<CardInfo>().stats);
            cards.Add(_obj);
        }
    }
}
