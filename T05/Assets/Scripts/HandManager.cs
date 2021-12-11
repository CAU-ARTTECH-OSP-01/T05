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


}
