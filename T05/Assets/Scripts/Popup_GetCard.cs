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

        selectCards.Clear(); //����� ������ selectCards�� ��Ұ� �߰��� ���� ����� �ʱ�ȭ�� �����ش�.

        List<Dictionary<string, string>> data = new List<Dictionary<string, string>>();
        data = DataReader_TSV.ReadDataByTSV("Data/CardList");

        List<int> _list = GameManager.Instance.enemy.GetComponent<EnemyController>().dropCards; //int ����Ʈ�� ������ ����� ī�� ����Ʈ�� �����´�.

        for (int i = 0; i < _list.Count; i++) //����Ʈ�� ����ŭ �ݺ��Ѵ�.
        {
            selectCards.Add(DeckManager.Instance.SetCardInfo(data[_list[i]])); //selectCards ����Ʈ�� int ����Ʈ�� ��ȯ�Ͽ� �־��ش�.
            GameObject _obj = Instantiate(SelectCard, content.transform); //ī�� ������Ʈ�� content ���� �����Ѵ�.
            _obj.GetComponent<CardInfo>().SetCardInfo(selectCards[i]); //������ ī�� ������Ʈ�� selectCards�� ������ �־��ش�.

            cards.Add(_obj); //Hierarchy â�� ��� ��ġ�ϴ��� �˾ƺ��� ���� ����Ʈ���� �־��ش�.
        }
    }
}
