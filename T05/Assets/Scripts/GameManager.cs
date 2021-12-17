using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour //턴 계산, 승패 체크. DataBase에서 플레이어와 적 데이터를 가져와 생성해준다.
{
    private static GameManager instance; //이 게임 씬에서 단 하나만 존재하기 때문에, Singleton 패턴으로 만들어준다.
                                         //다른 곳에서 상속시켜 주지 않더라도 어디에서나 사용할 수 있는 방식이다.
    public static GameManager Instance
    {
        get { return instance; }
        set { instance = value; }
    }

    private void Awake()
    {
        instance = this;
    }

    public int stage; //스테이지 번호

    public int turn; //턴 수 체크

    public GameObject Player_origin; //플레이어 prefab

    public GameObject player; //PlayerController를 어디에서나 사용할 수 있게 해줌.
                              //플레이어의 상황 등을 생성
    public GameObject enemy; //EnemyController를 어디에서나 사용할 수 있게 해줌.
                             //적의 상황 등을 생성

    public GameObject battleBG; //BattleScene 배경을 생성하도록 한다.

    public GameObject turnEndButton; //턴 종료 버튼

    public GameObject popup_Win; //승리 팝업
    public GameObject popup_Lose; //패배 팝업


    private void Start()
    {
        stage = DataBase.Instance.stage; //DataBase의 스테이지 번호를 시작하면서 설정해준다.
        PlayerSpawner();
        EnemySpawner();
        StageSpawner();

        StartCoroutine(Init());
    }

    IEnumerator Init() //플레이어 정보가 초기화 될 때까지 대기해 준 후에 처음 카드를 받을 수 있도록 한다.
    {
        while (!DeckManager.Instance.isInit) //DeckManager에서 초기화가 될 때까지 반복한다.
        {
            yield return null;
        }
        DeckManager.Instance.GoDeck2Hand(); //시작하면서 카드를 받도록 해준다.
    }

    public void PlayerSpawner() //플레이어 생성
    {
        player = Instantiate(Player_origin);
    }

    public void EnemySpawner() //적 생성
    {
        enemy = Instantiate(DataBase.Instance.enemyList[stage]);
    }

    public void StageSpawner() //스테이지 배경 생성
    {
        battleBG.GetComponent<SpriteRenderer>().sprite = DataBase.Instance.battleStageList[stage];
    }

    //전투 관련, 적과 플레이어 모두의 데이터를 가진 GameManager에서 실행해준다.
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
        print("도트");
    }
    public void Up_Attack(int _turn,int _amount)
    {
        print("공격 버프");
    }
    public void Down_Attack(int _turn,int _amount)
    {
        print("공격 디버프");
    }
    public void Up_Shield(int _turn,int _amount)
    {
        print("쉴드 버프");
    }
    public void Down_Shield(int _turn,int _amount)
    {
        print("쉴드 디버프");
    }

    public void Attack_Enemy(int _amount)
    {
        player.GetComponent<PlayerController>().GetDamage(_amount);
    }

    public void TurnEnd()
    {
        turnEndButton.SetActive(false);
        turn++;
        StartCoroutine(BattleDelay());
    }

    IEnumerator BattleDelay() //턴 종료 버튼을 눌렀을 때 행동 사이사이에 딜레이를 넣어준다.
    {
        DeckManager.Instance.BackCards2Deck(); //캐릭터 핸드 덱으로 이동

        yield return new WaitForSeconds(0.5f);

        enemy.GetComponent<EnemyController>().ShieldBreak(); //턴 종료를 누를 때 적 쉴드 삭제

        yield return new WaitForSeconds(0.5f);

        enemy.GetComponent<EnemyController>().EnemyTurn(); //적 패턴 수행

        yield return new WaitForSeconds(0.5f);

        player.GetComponent<PlayerController>().ShieldBreak(); //적 패턴 후 플레이어 쉴드 삭제

        yield return new WaitForSeconds(0.5f);

        DeckManager.Instance.GoDeck2Hand();

        turnEndButton.SetActive(true);
    }

    //승패 판단
    public void PlayerDeath() //패배
    {
        print("플레이어 죽음");
        Destroy(player);
        StartCoroutine(Popup_Lose());
    }
    IEnumerator Popup_Lose()
    {
        yield return new WaitForSeconds(0.5f);
        popup_Lose.SetActive(true);
    }

    public void EnemyDeath() //승리
    {
        print("적 죽음");
        Destroy(enemy);
        StartCoroutine(Popup_Win());
    }
    IEnumerator Popup_Win()
    {
        yield return new WaitForSeconds(0.5f);
        popup_Win.SetActive(true);
    }
}
