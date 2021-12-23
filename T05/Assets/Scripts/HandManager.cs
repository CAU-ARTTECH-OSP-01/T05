using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandManager : MonoBehaviour //�ڵ� ������Ʈ�� ������Ʈ�� �־ ���, Singleton ����
{
    private static HandManager instance; //�� ���� ������ �� �ϳ��� �����ϱ� ������, Singleton �������� ������ش�.
                                         //�ٸ� ������ ��ӽ��� ���� �ʴ��� ��𿡼��� ����� �� �ִ� ����̴�.
    public static HandManager Instance
    {
        get { return instance; }
        set { instance = value; }
    }

    private void Awake()
    {
        instance = this;
    }

    public List<CardStats> cardInfos; //�ڵ忡 �ִ� ī�� �����͸� ������ �־�� �Ѵ�.
                                     //������ �ڵ�� ī�带 ������ �� �����͵� �Բ� ������.
    public List<GameObject> cardObjects; //Hierarchy â������ � ī������ Ȯ�� ����

    public float gap = 64f; //ī�尡 �ڵ忡 ������ �� ī�� ���� �Ÿ�

    public void Init() //��ο� �� �ڵ��� ����Ʈ���� �ʱ�ȭ���ش�.
                //��ο��� ������ �ڵ带 Refresh ���ֵ��� �Ѵ�.
    {
        cardInfos.Clear(); //�ڵ�� ī�带 ������ �� �����ϴ� ī�� ������ ����Ʈ Clear
        for (int i = 0; i < cardObjects.Count; i++)
        {
            Destroy(cardObjects[i]); //cardObjects ����Ʈ�� ������ŭ �ݺ��ϸ� ������Ʈ�� �������ش�.
        }
        cardObjects.Clear(); //������Ʈ�� ��� �ı��� �� cards ����Ʈ�� Clear
    }

    public void SetDrawCard(int _rnd) //ī�带 ��ο��� �� cardInfos ����Ʈ�� �־��ֵ��� �Ѵ�.
    {
        cardInfos.Add(DeckManager.Instance.deckCardStats[_rnd]);
    }

    public void SetCardPositions() //ī�尡 �ڵ忡 ������ �� ��ġ ����
    {
        for (int i = 0; i < cardInfos.Count; i++) //cardInfos�� ������� ī�� ��ġ
        {
            transform.GetChild(i).GetComponent<CardController>().defaultPos_x = -32 * (transform.childCount - 1) + gap * i; //ī���� ��ġ ����
            
            /*switch(i) //������ ���� �˰� �ִٸ� switch���� ����ص� �ȴ�.
            {
                case 0:
                    transform.GetChild(i).GetComponent<CardController>().defaultPos_x = -375f;
                    break;
                case 1:
                    transform.GetChild(i).GetComponent<CardController>().defaultPos_x = -125f;
                    break;
                case 2:
                    transform.GetChild(i).GetComponent<CardController>().defaultPos_x = 125f;
                    break;
                case 3:
                    transform.GetChild(i).GetComponent<CardController>().defaultPos_x = 375f;
                    break;
            }*/

            transform.GetChild(i).GetComponent<CardController>().SetCardDefaultPos();
        }
    }


    public IEnumerator SetCardPositions_WaitDeleteCard() //ī�尡 ���� �� ������ ������ ��ٸ� �� �ڸ� ��ġ�� �ϵ��� �Ѵ�.
    {
        yield return new WaitForSeconds(0.1f); //0.1�� ���� ��ٷ� �ش�.

        for (int j = 0; j < cardObjects.Count; j++) //���� ī�带 cardObjects ����Ʈ���� �������ش�.
        {
            if (cardObjects[j] == null)
                cardObjects.RemoveAt(j);
        }

        for (int i = 0; i < cardObjects.Count; i++) //���� SetCardPosition �ּ� ����
        {
            cardObjects[i].GetComponent<CardController>().defaultPos_x = -32 * (transform.childCount - 1) + gap * i; //����Ʈ�� ������ �°� ī���� ��ġ ����

            cardObjects[i].GetComponent<CardController>().SetCardDefaultPos(); //��ȭ�� ��ġ�� ī���� ���� ��ġ�ν� �������ش�.
        }
    }
    public void WaitDeleteCard() //CardInfo�� CardUse ��ũ��Ʈ���� gameObject(CardInfo�� ���� ������Ʈ)�� ���� �Ǳ� �� �����ϱ� ���� ���.
    {
        StartCoroutine(SetCardPositions_WaitDeleteCard());
    }
}
