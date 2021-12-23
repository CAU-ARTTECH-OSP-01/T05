using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Popup_DeleteCard : MonoBehaviour
{
    //ī�� ���� �˾��� ������Ʈ�� �־��ְ�, ī�� ���� ��ư�� On Click �̺�Ʈ�� OpenDeck �Լ��� �����ϵ��� �������ش�.
    public GameObject originCard; //���� ī�� Prefab
    public GameObject content; //ī�� ���� �˾��� content �Ʒ��� ī����� �������ش�.
    public List<__CardStats> cardList; //Database�� ī�� ����Ʈ�� ��ȯ�Ͽ� �־��ֱ� ���� ����Ʈ
    public List<GameObject> cards; //Content ������Ʈ �Ʒ��� �ִ� ī�� ��ü���� ����Ʈ�� ����

    public GameObject popup_Delete; //ī�� ���� �˾��� ���� �ݱ� ���� �뵵
    public GameObject selectButton; //ī�� ���� ��ư�� Ȱ��ȭ/��Ȱ��ȭ �ϱ� ���� �뵵

    private void Update()
    {
        for (int i = 0; i < cards.Count; i++)
        {
            if (cards[i].GetComponent<Popup_Card>().isClicked == true)
            {
                //OpenDeck(); ī�带 ���� �� �����Ϸ��� �ּ� ����
                popup_Delete.SetActive(false);
                selectButton.SetActive(false);
            }
        }
    }

    void Init() //OpenDeck �Լ��� ������ ������ ī�尡 ����ؼ� �þ�� ���� �����ϱ� ���� �ʱ�ȭ���ִ� �Լ�
                //�� ��Ȳ�� ����ؼ� �ٲ� ���̱� ������ Refresh�� ����� �Ѵ�.
    {
        cardList.Clear(); //����Ʈ Clear
        for (int i = 0; i < cards.Count; i++)
        {
            Destroy(cards[i]); //cards ����Ʈ�� ������ŭ �ݺ��ϸ� ������Ʈ�� �������ش�.
        }
        cards.Clear(); //��� �ı��� �Ǿ��ٸ� cards ����Ʈ�� Clear ���ش�.
    }

    public void OpenDeck() //�÷��̾ ���� �� ����Ʈ�� �����ش�.
    {
        Init();

        List<Dictionary<string, string>> data = new List<Dictionary<string, string>>();
        data = DataReader_CSV.ReadDataByCSV("CardList"); //DataReader_TSV ��ũ��Ʈ�� ���� ������ �����͸� �����´�.

        DataBase.Instance.cardInventory.Sort();

        List<int> _list = DataBase.Instance.cardInventory; //DataBase�� ī�� ����Ʈ�� �����´�.

        for (int i = 0; i < _list.Count; i++) //�� ���� ī�带 �������ֱ� ���� ���� ������ŭ �ݺ����ش�.
        {
            cardList.Add(DeckManager.Instance.__SetCardInfo(data[_list[i]]));

            GameObject _obj = Instantiate(originCard, content.transform); //ī�� ������Ʈ�� �������ش�.
            _obj.GetComponent<Popup_CardInfo>().SetCardInfo(cardList[i]); //�� ī�忡 �´� ������ �־��ش�.

            cards.Add(_obj); //Hierarchy â�� ��� ��ġ�ϴ��� �˾ƺ��� ���� cards ����Ʈ�� _obj�� �־��ش�.
        }
    }
}
