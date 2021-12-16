using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour //�� ���, ���� üũ. DataBase���� �÷��̾�� �� �����͸� ������ �������ش�.
{
    private static GameManager instance; //�� ���� ������ �� �ϳ��� �����ϱ� ������, Singleton �������� ������ش�.
                                         //�ٸ� ������ ��ӽ��� ���� �ʴ��� ��𿡼��� ����� �� �ִ� ����̴�.
    public static GameManager Instance
    {
        get { return instance; }
        set { instance = value; }
    }

    private void Awake()
    {
        instance = this;
    }

    public int turn; //�� �� üũ

    public GameObject player; //PlayerController�� ��𿡼��� ����� �� �ְ� ����.
                              //�÷��̾��� ��Ȳ ���� ����
    public GameObject enemy; //EnemyController�� ��𿡼��� ����� �� �ְ� ����.
                             //���� ��Ȳ ���� ����


    private void Start()
    {
        PlayerSpawner();
        EnemySpawner();
    }

    public void PlayerSpawner() //�÷��̾� ����
    {

    }

    public void EnemySpawner() //�� ����
    {

    }

    //���� ����, ���� �÷��̾� ����� �����͸� ���� GameManager���� �������ش�.
    public void Attack(int _amount)
    {
        enemy.GetComponent<EnemyController>().GetDamage(_amount);
    }
    public void Shield(int _amount)
    {
        player.GetComponent<PlayerController>().GetShield(_amount);
    }
    public void Heal(int _amount)
    {
        player.GetComponent<PlayerController>().GetHeal(_amount);
    }
    public void Self(int _amount)
    {
        player.GetComponent<PlayerController>().GetDamage(_amount);
    }
    public void Dot(int _turn,int _amount)
    {
        print("��Ʈ");
    }
    public void Up_Attack(int _turn,int _amount)
    {
        print("���� ����");
    }
    public void Down_Attack(int _turn,int _amount)
    {
        print("���� �����");
    }
    public void Up_Shield(int _turn,int _amount)
    {
        print("���� ����");
    }
    public void Down_Shield(int _turn,int _amount)
    {
        print("���� �����");
    }

    public void Attack_Enemy(int _amount)
    {
        player.GetComponent<PlayerController>().GetDamage(_amount);
    }

    public void TurnEnd()
    {
        turn++;
        DeckManager.Instance.BackCards2Deck(); //ĳ���� �ڵ� ������ �̵�
        enemy.GetComponent<EnemyController>().EnemyTurn(); //�� ���� ����
    }
}
