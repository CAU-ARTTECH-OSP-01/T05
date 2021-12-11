using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckManager : MonoBehaviour //�� ��ư�� ������Ʈ�� �־��ش�.
{
    private static DeckManager instance; //�� ���� ������ �� �ϳ��� �����ϱ� ������, Singleton �������� ������ش�.
                                         //�ٸ� ������ ��ӽ��� ���� �ʴ��� ��𿡼��� ����� �� �ִ� ����̴�.
    public static DeckManager Instance
    {
        get { return instance; } //���� ����� �� instance�� ��ȯ
        set { instance = value; } //value ���� ����� �� instance�� �־��ش�.
    }

    private void Awake() //start���� ���� ����Ǵ� �Լ�
    {
        instance = this;
    }

    public List<int> deckList; //������ �ִ� ī���� ������ ������ ����Ʈ�� �־��ش�, Start�� ���� ����ϰ� ���ĺ��ʹ� �����Ͱ� ���Ե� deckCardStats�� �����Ѵ�.

    public GameObject originCard; //ī�� ������Ʈ ������ ���� ����
    public int drawCount; //��ο��� ī�� �� ����

    private void Start()
    {
        SetDeckCardInfo(); //������ ���۵� �� ���� �����ͷ� ���������� �Ѵ�.
    }

    public void GetHandCards() //���� ī�带 �ڵ�� �������� �Լ�, �� ���� ��ư�� On Click �̺�Ʈ�� DeckManager�� ���� �� ��ư�� �־��ְ�, �����Ѵ�.
    {
        for (int i = 0; i < drawCount; i++)
        {
            int _rnd = Random.Range(0, deckCardStats.Count); //0���� �� ����Ʈ�� �ִ� ���� �� ������ ���� �̴´�.

            GameObject _obj = Instantiate(originCard, HandManager.Instance.transform); //HandManager�� ������ �ִ� �ڵ� ��ġ�� ī�� ������Ʈ ����



            deckCardStats.RemoveAt(_rnd); //ī�带 �ڵ�� �����԰� ���ÿ� �������� �������ش�.
        }
    }

    public List<CardStats> deckCardStats; //������ int ����Ʈ�� �����ߴ� deckList�� �Ʒ��� SetCardInfo�� SetDeckCardInfo�� ���� ���� �����ͷ� ��ȯ���ش�.
                                          //Start���� deckList�� �̿��Ͽ� deckCardStats ����Ʈ�� ����� �� �ĺ��ʹ� �� ����Ʈ�� ���� �����ϵ��� �Ѵ�.

    public CardStats SetCardInfo(Dictionary<string, string> _data) //CardInfo�� SetCardInfo �ּ� ����
                                                                   //CardStats ���� ��ȯ���ش�.
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
        return stats; //stats ���� ��ȯ���ش�.
    }

    void SetDeckCardInfo() //Popup_Deck�� OpenDeck �ּ� ����
    {
        deckCardStats.Clear(); //����� ������ deckCardStats�� ��Ұ� �߰��� ���� ����� �ʱ�ȭ�� �����ش�.

        List<Dictionary<string, string>> data = new List<Dictionary<string, string>>();
        data = DataReader_TSV.ReadDataByTSV("Data/CardList");

        List<int> _list = DeckManager.Instance.deckList;

        for (int i = 0; i < _list.Count; i++)
        {
            deckCardStats.Add(SetCardInfo(data[_list[i]])); //���� SetCardInfo�� ���� ������ �޾ƿ� ��, deckCardStats ����Ʈ�� �־��ش�.
        }
    }
}
