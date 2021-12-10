using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Popup_Deck : MonoBehaviour 
    //�˾�_���� ������Ʈ�� �־��ְ�, �� ��ư�� On Click �̺�Ʈ�� �˾�_���� Popup_Deck ��ũ��Ʈ ���� OpenDeck �Լ��� �����ϵ��� �������ش�.
{
    public GameObject originCard; //ī�� ������Ʈ(Prefab)�� �����´�.
                                  //CardInfo�� ������������ �ʾ�����, ī�� ������Ʈ�� �ش� ��ũ��Ʈ�� ������ �ֱ� ������ ������ �����ϴ�.
    public GameObject content; //originCard(ī�� ������Ʈ)�� ������ �� �θ� ����
                               //�˾�_�� > �ֻ��� ����Ʈ > Viewport > Content �Ʒ��� �������ش�.
    public List<CardStats> cardList; //CardInfo���� ������� CardStats�� �����´�.
                                     //DeckManager���� ī���� ������ ������ �°� ī�带 �������ֱ� ���� ����
    public List<GameObject> cards; //Content ������Ʈ �Ʒ��� �ִ� ī�� ��ü���� ����Ʈ�� ����

    void Init() //OpenDeck �Լ��� ������ ������ ī�尡 ����ؼ� �þ�� ���� �����ϱ� ���� �ʱ�ȭ���ִ� �Լ�
                //�� ��Ȳ�� ����ؼ� �ٲ� ���̱� ������ Refresh�� ����� �Ѵ�.
    {
        cardList.Clear(); //����Ʈ Clear
        for(int i = 0; i < cards.Count; i++)
        {
            Destroy(cards[i]); //cards ����Ʈ�� ������ŭ �ݺ��ϸ� ������Ʈ�� �������ش�.
        }
        cards.Clear(); //��� �ı��� �Ǿ��ٸ� cards ����Ʈ�� Clear ���ش�.
    }

    public void OpenDeck() //�� ��Ȳ�� �÷��̾ ���� ������ �ִ� ī����� �����ֱ� ���� �Լ�
    {
        Init();

        List<Dictionary<string, string>> data = new List<Dictionary<string, string>>();
        data = DataReader_TSV.ReadDataByTSV("Data/CardList"); //DataReader_TSV ��ũ��Ʈ�� ���� ������ �����͸� �����´�.

        List<int> _list = DeckManager.Instance.deckList; //Singleton���� ������� DeckManager�� ���� ������ �� �ִ�.
                                                         //_list�� �ϳ� ����� deckList�� �־� ����Ʈ�� ���´�.

        for (int i = 0; i < _list.Count; i++) //�� ���� ī�带 �������ֱ� ���� ���� ������ŭ �ݺ����ش�.
        {
            GameObject _obj = Instantiate(originCard, content.transform); //_obj�� GameObject�� �����ϰ�, ī�带 �������ش�.
                                                                          //����Ƽ���� �����ϴ� Instantiate�� ���� ������Ʈ�� �ϳ� �������ִ� �Լ��̴�.
                                                                          //originCard�� ������ ������Ʈ�� ������ �θ��� ��ġ(content.transform)�� �־��ش�.
            _obj.GetComponent<CardInfo>().SetCardInfo(data[_list[i]]); //������Ʈ�� �����ϴ� CardInfo ������Ʈ�� ���� ��,
                                                                       //SetCardInfo(data[ī�� ��ȣ])�� ���� �־��ش�.
                                                                       //ī�� ��ȣ�� DeckManager�� ������ ������ ����Ʈ�� i��°�� �־��ش�.
            cardList.Add(_obj.GetComponent<CardInfo>().stats); //ī�� stats�� �����´�.
            cards.Add(_obj); //Hierarchy â�� ��� ��ġ�ϴ��� �˾ƺ��� ���� cards ����Ʈ�� _obj�� �־��ش�.
        }
    }
}
