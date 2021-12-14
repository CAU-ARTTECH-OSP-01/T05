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
        //방어

        turn++; //턴 수를 늘려주고 공격과 방어를 0으로 바꿔준다.
        currentPattern.Attack = 0;
        currentPattern.Defence = 0;

        SetPatternUI(); //턴 수에 맞는 값을 UI상에 보여준다.
    }

    public void SelectPattern()
    {
        currentPattern = patterns[turn]; //턴 수에 맞는 patterns 리스트 안에서의 정보를 currentPattern에 넣어준다.

        SetPatternUI(); //현재 패턴에 맞는 UI를 보여준다.
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
        if (enemyStatus_Current.Shield <= _dmg) //쉴드량이 더 작다면 쉴드부터 제거된 후 HP 계산
        {
            enemyStatus_Current.HP -= (_dmg - enemyStatus_Current.Shield);
            enemyStatus_Current.Shield = 0;

            if (enemyStatus_Current.HP <= 0) //HP가 0보다 작다면 죽음 처리
            {
                enemyStatus_Current.HP = 0;
                print("적 죽음");
            }
        }
        else
            enemyStatus_Current.Shield -= _dmg; //쉴드량이 충분하다면 쉴드에 데미지 계산
    }
}
