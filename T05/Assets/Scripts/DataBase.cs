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
}