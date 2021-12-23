using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Popup_GetCard : MonoBehaviour
{
    public GameObject SelectCard; //������ ī�� Prefab
    public GameObject content; //ī�带 ���� ��ų ��ġ
    public List<__CardStats> selectCards; //���� ����� ī�� ����Ʈ
    public List<GameObject> cards;
    public GameObject popup_Select; //ī�� ���� �˾��� ���� �ݱ� ���� �뵵
    public GameObject selectButton; //ī�� ���� ��ư�� Ȱ��ȭ/��Ȱ��ȭ �ϱ� ���� �뵵

    public List<int> dropCards_Origin; //���� ����ϴ� ī�� ����Ʈ
    public List<int> dropCards; //���� ����� ī�� ����Ʈ

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
        RandomDrop(); //����ϴ� ī����� �̸� �����ش�.
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
        for (int i = 0; i < GameManager.Instance.enemy.GetComponent<EnemyController>().dropCards.Count; i++) //������ �Ѽյ��� �ʵ��� �Ѵ�.
        {
            int _cardNum = GameManager.Instance.enemy.GetComponent<EnemyController>().dropCards[i];
            dropCards_Origin.Add(_cardNum);
        }

        for (int j = 0; j < 3; j++)
        {
            int _rnd = Random.Range(0, dropCards_Origin.Count); //�� ��� ī�� ����Ʈ �� ������ ���� �̴´�.
            dropCards.Add(dropCards_Origin[_rnd]); //int ����Ʈ�� ������ ����� ī�� ����Ʈ�� �����´�.
            dropCards_Origin.RemoveAt(_rnd); //���� ���� ��ġ�� �ʰ� �̾Ҵ� ���� �������ش�.
        }
    }

    public void OpenSelectCard()
    {
        Init();

        selectCards.Clear(); //����� ������ selectCards�� ��Ұ� �߰��� ���� ����� �ʱ�ȭ�� �����ش�.

        List<Dictionary<string, string>> data = new List<Dictionary<string, string>>();
        data = DataReader_CSV.ReadDataByCSV("CardList");

        for (int i = 0; i < dropCards.Count; i++) //����Ʈ�� ����ŭ �ݺ��Ѵ�.
        {
            selectCards.Add(DeckManager.Instance.__SetCardInfo(data[dropCards[i]])); //selectCards ����Ʈ�� int ����Ʈ�� ��ȯ�Ͽ� �־��ش�.
            GameObject _obj = Instantiate(SelectCard, content.transform); //ī�� ������Ʈ�� content ���� �����Ѵ�.
            _obj.GetComponent<Popup_CardInfo>().SetCardInfo(selectCards[i]); //������ ī�� ������Ʈ�� selectCards�� ������ �־��ش�.

            cards.Add(_obj); //Hierarchy â�� ��� ��ġ�ϴ��� �˾ƺ��� ���� ����Ʈ���� �־��ش�.
        }
    }
}
