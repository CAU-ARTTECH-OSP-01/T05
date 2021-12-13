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


    public List<CardInfo> cardInfos; //�ڵ忡 �ִ� ī�� �����͸� ������ �־�� �Ѵ�.
                                     //������ �ڵ�� ī�带 ������ �� �����͵� �Բ� ������.
    public List<GameObject> cardObjects; //Hierarchy â������ � ī������ Ȯ�� ����

    public float gap = 64f; //ī�尡 �ڵ忡 ������ �� ī�� ���� �Ÿ�
    public void SetCardPositions() //ī�尡 �ڵ忡 ������ �� ��ġ ����
    {
        for (int i = 0; i < transform.childCount; i++)
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
}
