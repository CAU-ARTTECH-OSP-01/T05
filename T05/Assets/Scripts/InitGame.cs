using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitGame : MonoBehaviour
{
    public void OnClick() //�й� ȭ���� ��ư Ŭ�� �� ���� �ʱ�ȭ
    {
        DataBase.Instance.cardInventory = DataBase.Instance.FirstCardInventory; //ī�� ����Ʈ �ʱ�ȭ
        DataBase.Instance.stage = 0; //�������� �ʱ�ȭ
    }
}
