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

    public int turn; //턴 수 체크

    public GameObject player; //PlayerController를 어디에서나 사용할 수 있게 해줌.
                              //플레이어의 상황 등을 생성
    public GameObject enemy; //EnemyController를 어디에서나 사용할 수 있게 해줌.
                             //적의 상황 등을 생성


    private void Start()
    {
        PlayerSpawner();
        EnemySpawner();
    }

    public void PlayerSpawner() //플레이어 생성
    {

    }

    public void EnemySpawner() //적 생성
    {

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
        turn++;
        DeckManager.Instance.BackCards2Deck(); //캐릭터 핸드 덱으로 이동
        enemy.GetComponent<EnemyController>().EnemyTurn(); //적 패턴 수행
    }
}
