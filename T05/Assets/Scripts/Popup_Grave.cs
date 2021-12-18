using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Popup_Grave : MonoBehaviour
{
    public GameObject usedCard; //ī�� Prefab
    public GameObject graveContent; //�˾�_������ Content ������Ʈ
    public List<CardStats> cardList; //������ �̵��� ī�� ����Ʈ
    public List<GameObject> cards; //Hierarchy�󿡼� �������� ī��

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
