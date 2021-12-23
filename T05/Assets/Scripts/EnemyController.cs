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
    public Status enemyStatus; //적의 Status 생성
    public Status enemyStatus_Current; //현재 적의 Status 관리

    public List<Pattern> patterns; //적의 패턴 리스트 생성
    public int turn; //현재 턴 수
    public Pattern currentPattern; //현재 패턴
    public GameObject ui_Attack; //현재 패턴에서 공격 Image
    public Text ui_AttackCount; //현재 패턴에서 공격량 Text
    public GameObject ui_Defence; //현재 패턴에서 방어 Image
    public Text ui_DefenceCount; //현재 패턴에서 방어량 Text

    public GameObject ui_Shield; //쉴드 UI의 on/off를 위해 생성
    public Text ui_ShieldCount; //쉴드 Text

    public Image ui_HPGauge; // HP Image의 Filled 값을 조절하기 위해 생성
    public Text ui_HPCount; //HP Text

    public List<int> dropCards; //게임 종료 후 드롭할 카드 리스트

    public Animator animator;


    private void Start()
    {
        Status _status = new Status() //enemyStatus_Current = enemyStatus로 구성하게 되면 주소 값을 받아오기 때문에 변경을 해도 값이 같은 문제가 발생한다.
                                      //해결을 위해 다른 주소 값에 넣어주고 가져오는 방식으로 구성한다.
        {
            HP = enemyStatus.HP,
            Shield = enemyStatus.Shield
        };
        enemyStatus_Current = _status; //시작하면서 현재 적의 Status와 기본 값을 동일하게 맞춰준다.

        SetEnemyUI();
        SelectPattern();
    }

    private void Update()
    {
        SetEnemyUI();
    }

    void SetEnemyUI()
    {
        if (enemyStatus_Current.Shield > 0) //쉴드 이미지를 보여줄 지 결정해준다.
            ui_Shield.SetActive(true);
        else
            ui_Shield.SetActive(false);

        ui_ShieldCount.text = enemyStatus_Current.Shield.ToString(); //현재 적 Status의 쉴드 값을 보여준다.

        ui_HPGauge.fillAmount = (float)enemyStatus_Current.HP / (float)enemyStatus.HP; //int 계산으로는 0 또는 1만 나오기 때문에 float으로 바꿔준다.
                                                                                       //Hp의 Fill Amount 값을 현재 HP에 따라 조절해준다.

        ui_HPCount.text = enemyStatus_Current.HP.ToString(); //현재 적 HP 값을 보여준다.
    }

    public void EnemyTurn() //턴 종료 후 적이 공격이나 방어 등을 할 수 있도록 한다.
    {
        //공격
        GameManager.Instance.Attack_Enemy(currentPattern.Attack);
        //방어
        GetShield(currentPattern.Defence);

        currentPattern.Attack = 0;
        currentPattern.Defence = 0;

        SelectPattern(); //턴 수에 맞는 패턴을 골라준다.
    }

    public void SelectPattern()
    {
        int _turn = GameManager.Instance.turn;

        //patterns 리스트 내의 패턴이 모두 종료되었을 때 처음부터 돌릴 지 3 ~ 5번 등 일부만 계속 반복할 지 결정
        if (_turn + 1 > patterns.Count)
            _turn = _turn % patterns.Count;

        Pattern _pattern = new Pattern()
        {
            Attack = patterns[_turn].Attack,
            Defence = patterns[_turn].Defence
        };
        currentPattern = _pattern; //턴 수에 맞는 patterns 리스트 안에서의 정보를 currentPattern에 넣어준다.
        
        SetPatternUI(); //현재 패턴을 화면에 보여준다.
    }

    void SetPatternUI() //패턴 UI를 보여줄지 결정한다.
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

    public void GetDamage(int _dmg) //쉴드의 유무에 따라 데미지 계산이 달라지는 상황 계산
    {
        StartCoroutine(GetDamage());

        if (enemyStatus_Current.Shield <= _dmg) //쉴드량이 더 작다면 쉴드부터 제거된 후 HP 계산
        {
            enemyStatus_Current.HP -= (_dmg - enemyStatus_Current.Shield);
            enemyStatus_Current.Shield = 0;

            if (enemyStatus_Current.HP <= 0) //HP가 0보다 작다면 죽음 처리
            {
                enemyStatus_Current.HP = 0;
                GameManager.Instance.EnemyDeath();
            }
        }
        else
            enemyStatus_Current.Shield -= _dmg; //쉴드량이 충분하다면 쉴드에 데미지 계산
    }

    IEnumerator GetDamage()
    {
        //SoundManager.PlaySFX("Attack");
        animator.SetTrigger("Hurt");
        yield return new WaitForSeconds(0.5f);
    }

    public void ShieldBreak() //턴이 끝날 때 쉴드를 없애준다.
    {
        enemyStatus_Current.Shield = 0;
    }

    public void GetShield(int _shield) //쉴드를 추가해준다.
    {
        enemyStatus_Current.Shield += _shield;
        //SoundManager.PlaySFX("Defence");
    }
}
