using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class Status
{
    public int HP;
    public int Shield;
}

[Serializable]
public class Pattern
{
    public int Attack;
    public int Defence;
}

public class EnemyController : MonoBehaviour
{
    public Status enemyStatus; //���� Status ����
    public Status enemyStatus_Current; //���� ���� Status ����

    public List<Pattern> patterns; //���� ���� ����Ʈ ����
    public int turn; //���� �� ��
    public Pattern currentPattern; //���� ����
    public GameObject ui_Attack; //���� ���Ͽ��� ���� Image
    public Text ui_AttackCount; //���� ���Ͽ��� ���ݷ� Text
    public GameObject ui_Defence; //���� ���Ͽ��� ��� Image
    public Text ui_DefenceCount; //���� ���Ͽ��� �� Text

    public GameObject ui_Shield; //���� UI�� on/off�� ���� ����
    public Text ui_ShieldCount; //���� Text

    public Image ui_HPGauge; // HP Image�� Filled ���� �����ϱ� ���� ����
    public Text ui_HPCount; //HP Text

    public List<int> dropCards; //���� ���� �� ����� ī�� ����Ʈ

    public Animator animator;


    private void Start()
    {
        Status _status = new Status() //enemyStatus_Current = enemyStatus�� �����ϰ� �Ǹ� �ּ� ���� �޾ƿ��� ������ ������ �ص� ���� ���� ������ �߻��Ѵ�.
                                      //�ذ��� ���� �ٸ� �ּ� ���� �־��ְ� �������� ������� �����Ѵ�.
        {
            HP = enemyStatus.HP,
            Shield = enemyStatus.Shield
        };
        enemyStatus_Current = _status; //�����ϸ鼭 ���� ���� Status�� �⺻ ���� �����ϰ� �����ش�.

        SetEnemyUI();
        SelectPattern();
    }

    private void Update()
    {
        SetEnemyUI();
    }

    void SetEnemyUI()
    {
        if (enemyStatus_Current.Shield > 0) //���� �̹����� ������ �� �������ش�.
            ui_Shield.SetActive(true);
        else
            ui_Shield.SetActive(false);

        ui_ShieldCount.text = enemyStatus_Current.Shield.ToString(); //���� �� Status�� ���� ���� �����ش�.

        ui_HPGauge.fillAmount = (float)enemyStatus_Current.HP / (float)enemyStatus.HP; //int ������δ� 0 �Ǵ� 1�� ������ ������ float���� �ٲ��ش�.
                                                                                       //Hp�� Fill Amount ���� ���� HP�� ���� �������ش�.

        ui_HPCount.text = enemyStatus_Current.HP.ToString(); //���� �� HP ���� �����ش�.
    }

    public void EnemyTurn() //�� ���� �� ���� �����̳� ��� ���� �� �� �ֵ��� �Ѵ�.
    {
        //����
        GameManager.Instance.Attack_Enemy(currentPattern.Attack);
        //���
        GetShield(currentPattern.Defence);

        currentPattern.Attack = 0;
        currentPattern.Defence = 0;

        SelectPattern(); //�� ���� �´� ������ ����ش�.
    }

    public void SelectPattern()
    {
        int _turn = GameManager.Instance.turn;

        //patterns ����Ʈ ���� ������ ��� ����Ǿ��� �� ó������ ���� �� 3 ~ 5�� �� �Ϻθ� ��� �ݺ��� �� ����
        if (_turn + 1 > patterns.Count)
            _turn = _turn % patterns.Count;

        Pattern _pattern = new Pattern()
        {
            Attack = patterns[_turn].Attack,
            Defence = patterns[_turn].Defence
        };
        currentPattern = _pattern; //�� ���� �´� patterns ����Ʈ �ȿ����� ������ currentPattern�� �־��ش�.
        
        SetPatternUI(); //���� ������ ȭ�鿡 �����ش�.
    }

    void SetPatternUI() //���� UI�� �������� �����Ѵ�.
    {
        if (currentPattern.Attack > 0)
            ui_Attack.SetActive(true);
        else
            ui_Attack.SetActive(false);
        ui_AttackCount.text = currentPattern.Attack.ToString();

        if (currentPattern.Defence > 0)
            ui_Defence.SetActive(true);
        else
            ui_Defence.SetActive(false);
        ui_DefenceCount.text = currentPattern.Defence.ToString();
    }

    public void GetDamage(int _dmg) //������ ������ ���� ������ ����� �޶����� ��Ȳ ���
    {
        StartCoroutine(GetDamage());

        if (enemyStatus_Current.Shield <= _dmg) //���差�� �� �۴ٸ� ������� ���ŵ� �� HP ���
        {
            enemyStatus_Current.HP -= (_dmg - enemyStatus_Current.Shield);
            enemyStatus_Current.Shield = 0;

            if (enemyStatus_Current.HP <= 0) //HP�� 0���� �۴ٸ� ���� ó��
            {
                enemyStatus_Current.HP = 0;
                GameManager.Instance.EnemyDeath();
            }
        }
        else
            enemyStatus_Current.Shield -= _dmg; //���差�� ����ϴٸ� ���忡 ������ ���
    }

    IEnumerator GetDamage()
    {
        //SoundManager.PlaySFX("Attack");
        animator.SetTrigger("Hurt");
        yield return new WaitForSeconds(0.5f);
    }

    public void ShieldBreak() //���� ���� �� ���带 �����ش�.
    {
        enemyStatus_Current.Shield = 0;
    }

    public void GetShield(int _shield) //���带 �߰����ش�.
    {
        enemyStatus_Current.Shield += _shield;
        //SoundManager.PlaySFX("Defence");
    }
}
