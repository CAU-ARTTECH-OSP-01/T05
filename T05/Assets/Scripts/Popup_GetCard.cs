using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Popup_GetCard : MonoBehaviour
{
    public GameObject SelectCard; //������ ī�� Prefab
    public GameObject content; //ī�带 ���� ��ų ��ġ
    public List<CardStats> selectCards; //���� ����� ī�� ����Ʈ
    public List<GameObject> cards;
    public GameObject popup_Select; //ī�� ���� �˾��� ���� �ݱ� ���� �뵵
    public GameObject selectButton; //ī�� ���� ��ư�� Ȱ��ȭ/��Ȱ��ȭ �ϱ� ���� �뵵

    public List<int> dropCards; //������ ����ϴ� ī�� ����Ʈ

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
        for (int j = 0; j < 3; j++)
        {
            int _rnd = Random.Range(0, GameManager.Instance.enemy.GetComponent<EnemyController>().dropCards.Count); //�� ��� ī�� ����Ʈ �� ������ ���� �̴´�.
            dropCards.Add(GameManager.Instance.enemy.GetComponent<EnemyController>().dropCards[j]); //int ����Ʈ�� ������ ����� ī�� ����Ʈ�� �����´�.
            GameManager.Instance.enemy.GetComponent<EnemyController>().dropCards.RemoveAt(_rnd); //���� ���� ��ġ�� �ʰ� �̾Ҵ� ���� �������ش�.
        }
    }

    public void OpenSelectCard()
    {
        Init();

        selectCards.Clear(); //����� ������ selectCards�� ��Ұ� �߰��� ���� ����� �ʱ�ȭ�� �����ش�.

        List<Dictionary<string, string>> data = new List<Dictionary<string, string>>();
        data = DataReader_TSV.ReadDataByTSV("Data/CardList");

        for (int i = 0; i < dropCards.Count; i++) //����Ʈ�� ����ŭ �ݺ��Ѵ�.
        {
            selectCards.Add(DeckManager.Instance.SetCardInfo(data[dropCards[i]])); //selectCards ����Ʈ�� int ����Ʈ�� ��ȯ�Ͽ� �־��ش�.
            GameObject _obj = Instantiate(SelectCard, content.transform); //ī�� ������Ʈ�� content ���� �����Ѵ�.
            _obj.GetComponent<CardInfo>().SetCardInfo(selectCards[i]); //������ ī�� ������Ʈ�� selectCards�� ������ �־��ش�.

            cards.Add(_obj); //Hierarchy â�� ��� ��ġ�ϴ��� �˾ƺ��� ���� ����Ʈ���� �־��ش�.
        }
    }
}
