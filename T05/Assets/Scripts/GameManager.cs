using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    public int stage; //�������� ��ȣ

    public int turn; //�� �� üũ

    public GameObject Player_origin; //�÷��̾� prefab

    public GameObject player; //PlayerController�� ��𿡼��� ����� �� �ְ� ����.
                              //�÷��̾��� ��Ȳ ���� ����
    public GameObject enemy; //EnemyController�� ��𿡼��� ����� �� �ְ� ����.
                             //���� ��Ȳ ���� ����

    public GameObject battleBG; //BattleScene ����� �����ϵ��� �Ѵ�.

    public GameObject turnEndButton; //�� ���� ��ư

    public GameObject popup_Win; //�¸� �˾�
    public GameObject popup_Lose; //�й� �˾�


    private void Start()
    {
        stage = DataBase.Instance.stage; //DataBase�� �������� ��ȣ�� �����ϸ鼭 �������ش�.
        PlayerSpawner();
        EnemySpawner();
        StageSpawner();

        StartCoroutine(Init());
    }

    IEnumerator Init() //�÷��̾� ������ �ʱ�ȭ �� ������ ����� �� �Ŀ� ó�� ī�带 ���� �� �ֵ��� �Ѵ�.
    {
        while (!DeckManager.Instance.isInit) //DeckManager���� �ʱ�ȭ�� �� ������ �ݺ��Ѵ�.
        {
            yield return null;
        }
        DeckManager.Instance.GoDeck2Hand(); //�����ϸ鼭 ī�带 �޵��� ���ش�.
    }

    public void PlayerSpawner() //�÷��̾� ����
    {
        player = Instantiate(Player_origin);
    }

    public void EnemySpawner() //�� ����
    {
        enemy = Instantiate(DataBase.Instance.enemyList[stage]);
    }

    public void StageSpawner() //�������� ��� ����
    {
        battleBG.GetComponent<SpriteRenderer>().sprite = DataBase.Instance.battleStageList[stage];
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

    public void Attack_Enemy(int _amount)
    {
        player.GetComponent<PlayerController>().GetDamage(_amount);
    }

    public void TurnEnd()
    {
        turnEndButton.SetActive(false); //�� ���� ��ư�� ����ؼ� ������ ���� �����ϱ� ���� ���д�.
        turn++;
        StartCoroutine(BattleDelay());
    }

    IEnumerator BattleDelay() //�� ���� ��ư�� ������ �� �ൿ ���̻��̿� �����̸� �־��ش�.
    {
        DeckManager.Instance.BackCards2Deck(); //ĳ���� �ڵ� ������ �̵�

        DeckManager.Instance.deckCardStats.Sort(delegate (CardStats A, CardStats B)
        {
            return A.index.CompareTo(B.index);
        });

        yield return new WaitForSeconds(0.5f);

        enemy.GetComponent<EnemyController>().ShieldBreak(); //�� ���Ḧ ���� �� �� ���� ����

        yield return new WaitForSeconds(0.5f);

        enemy.GetComponent<EnemyController>().EnemyTurn(); //�� ���� ����

        yield return new WaitForSeconds(0.5f);

        player.GetComponent<PlayerController>().ShieldBreak(); //�� ���� �� �÷��̾� ���� ����

        yield return new WaitForSeconds(0.5f);

        DeckManager.Instance.GoDeck2Hand(); //������ �ڵ�� ī�带 �ش�.

        turnEndButton.SetActive(true); //�����Ҵ� �� ���� ��ư�� ���ش�.
    }

    //���� �Ǵ�
    public void PlayerDeath() //�й�
    {
        print("�÷��̾� ����");
        //Destroy(player);
        StartCoroutine(Popup_Lose());
    }
    IEnumerator Popup_Lose()
    {
        yield return new WaitForSeconds(0.5f);
        popup_Lose.SetActive(true);
    }

    public void EnemyDeath() //�¸�
    {
        print("�� ����");
        //Destroy(enemy);
        StartCoroutine(Popup_Win());

        DataBase.Instance.playerStatus = player.GetComponent<PlayerController>().playerStatus_Current; //�¸� �� �÷��̾��� HP�� ���� �������������� ����� �� �ֵ��� �Ѵ�.
        DataBase.Instance.stage++;
        if (DataBase.Instance.stage > 4)
            SceneManager.LoadScene("EndingScene");
    }
    IEnumerator Popup_Win()
    {
        yield return new WaitForSeconds(0.5f);
        player.GetComponent<PlayerController>().ShieldBreak(); //���带 ���� ä�� ������ ����Ǹ� ���� ���������� �Ѿ�� �Ǵ� ���� �ذ�
        popup_Win.SetActive(true);
    }
}
