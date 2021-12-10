using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Popup_Deck : MonoBehaviour
{
    public GameObject originCard;
    public GameObject content;
    public List<CardStats> cardList;
    public List<GameObject> cards;

    void Init()
    {
        cardList.Clear();
        for(int i = 0; i < cards.Count; i++)
        {
            Destroy(cards[i]);
        }
        cards.Clear();
    }

    public void OpenDeck()
    {
        Init();

        List<Dictionary<string, string>> data = new List<Dictionary<string, string>>();
        data = DataReader_TSV.ReadDataByTSV("Data/CardList");

        List<int> _list = DeckManager.Instance.deckList;

        for (int i = 0; i < _list.Count; i++)
        {
            GameObject _obj = Instantiate(originCard, content.transform);
            _obj.GetComponent<CardInfo>().SetCardInfo(data[_list[i]]);

            cardList.Add(_obj.GetComponent<CardInfo>().stats);
            cards.Add(_obj);
        }
    }
}
