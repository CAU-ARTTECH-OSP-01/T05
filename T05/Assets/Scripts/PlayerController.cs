using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public Status playerStatus; //EnemyController���� ����ߴ� Status�� �״�� ����Ͽ� playerStatus�� �����.
    public Status playerStatus_Current; //���� �÷��̾��� ���¸� �����´�.

    public List<int> cardInventory; //�÷��̾ ������ �ִ� ��

    public GameObject ui_Shield; //���� UI�� on/off�� ���� ����
    public Text ui_ShieldCount; //���� Text

    public Image ui_HPGauge; // HP Image�� Filled ���� �����ϱ� ���� ����
    public Text ui_HPCount; //HP Text

    private void Start()
    {
        Status _status = new Status() //enemyStatus_Current = enemyStatus�� �����ϰ� �Ǹ� �ּ� ���� �޾ƿ��� ������ ������ �ص� ���� ���� ������ �߻��Ѵ�.
                                      //�ذ��� ���� �ٸ� �ּ� ���� �־��ְ� �������� ������� �����Ѵ�.
        {
            HP = playerStatus.HP,
            Shield = playerStatus.Shield
        };
        playerStatus_Current = _status; //�����ϸ鼭 ���� ���� Status�� �⺻ ���� �����ϰ� �����ش�.

        SetPlayerUI();
        SetPlayerDeck();
    }

    private void Update()
    {
        SetPlayerUI();
    }

    void SetPlayerUI()
    {
        if (playerStatus_Current.Shield > 0) //���� �̹����� ������ �� �������ش�.
            ui_Shield.SetActive(true);
        else
            ui_Shield.SetActive(false);

        ui_ShieldCount.text = playerStatus_Current.Shield.ToString(); //���� �÷��̾��� ���� ���� �����ش�.

        ui_HPGauge.fillAmount = (float)playerStatus_Current.HP / (float)playerStatus.HP; //int ������δ� 0 �Ǵ� 1�� ������ ������ float���� �ٲ��ش�.
                                                                                         //Hp�� Fill Amount ���� ���� HP�� ���� �������ش�.

        ui_HPCount.text = playerStatus_Current.HP.ToString(); //���� �÷��̾� HP ���� �����ش�.
    }

    void SetPlayerDeck()
    {
        List<int> _list = new List<int>(); //_list ����Ʈ�� ���ο� �ּ� �� �Ҵ�

        for (int i = 0; i < DataBase.Instance.cardInventory.Count; i++)
        {
            int _temp = DataBase.Instance.cardInventory[i]; //_temp ���� DataBase�� �����ϴ� ���� ���ڸ� �־��ش�.
            _list.Add(_temp); //_temp ���� ������ ���� _list�� �־��ش�.
        }

        cardInventory = _list; //������ ���� _list�� cardInventory�� �־��ش�.

        DeckManager.Instance.deckList = _list; //DeckManager�� ������ _list�� �־� ������ �� �ֵ��� �Ѵ�.
        DeckManager.Instance.Init(); //_list�� ���� �������� ���� stats ���� ������ش�.
    }

    public void GetDamage(int _dmg) //������ ������ ���� ������ ����� �޶����� ��Ȳ ���
    {
        if (playerStatus_Current.Shield <= _dmg) //���差�� �� �۴ٸ� ������� ���ŵ� �� HP ���
        {
            playerStatus_Current.HP -= (_dmg - playerStatus_Current.Shield);
            playerStatus_Current.Shield = 0;

            if (playerStatus_Current.HP <= 0) //HP�� 0���� �۴ٸ� ���� ó��
            {
                playerStatus_Current.HP = 0;
                print("�÷��̾� ����");
            }
        }
        else
            playerStatus_Current.Shield -= _dmg; //���差�� ����ϴٸ� ���忡 ������ ���
    }

    public void GetShield(int _shield) //���带 �߰����ش�.
    {
        playerStatus_Current.Shield += _shield;
    }

    public void GetHeal(int _Heal) //ȸ���� �ϰ� �Ǵ� ��Ȳ
    {
        playerStatus_Current.HP += _Heal;

        if (playerStatus_Current.HP > playerStatus.HP) //ȸ������ ó�� ��ġ�� �Ѿ�� �� ���, ���� status�� ü�� ���·� �����ش�.
        {
            int firstHP = playerStatus.HP;
            playerStatus_Current.HP = firstHP;

            print("ȸ�� �Ϸ�");
        }
    }
}
