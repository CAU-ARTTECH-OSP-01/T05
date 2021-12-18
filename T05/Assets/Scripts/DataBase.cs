using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataBase : MonoBehaviour
{
    private static DataBase instance; //�� ���� ������ �� �ϳ��� �����ϱ� ������, Singleton �������� ������ش�.
                                      //�ٸ� ������ ��ӽ��� ���� �ʴ��� ��𿡼��� ����� �� �ִ� ����̴�.
    public static DataBase Instance
    {
        get { return instance; }
        set { instance = value; }
    }

    private void Awake()
    {
        instance = this;

        DontDestroyOnLoad(gameObject); //DataBase�� ���� ������Ʈ�� Scene�� ����Ǵ��� ������� �ʰ� �Ѵ�.
    }

    public List<int> cardInventory; //�÷��̾ ������ �ִ� ��
    public List<GameObject> enemyList; //Prefab���� ����� �� ���� ����Ʈ ���� �־� �� BattleScene�� �´� �� �����͸� �ҷ��� �� �ֵ��� �����Ѵ�.
    public List<Sprite> battleStageList; //��Ʋ �������� sprite ����Ʈ�� �� BattleScene���� �ҷ��� �� �ֵ��� GameManager���� ����Ѵ�.
    public int stage = 1; //�������� ���� ȭ�鿡�� ��ȣ�� �޾� GameManager�� �Ѱ��ش�.

    public int cardTypes; //ī�� ������ �� ����

    public Status playerStatus; //�÷��̾��� HP�� ����ؼ� ������ �� �ֵ��� �����Ѵ�.
}
