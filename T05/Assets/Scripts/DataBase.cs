using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataBase : MonoBehaviour
{
    private static DataBase instance; //이 게임 씬에서 단 하나만 존재하기 때문에, Singleton 패턴으로 만들어준다.
                                      //다른 곳에서 상속시켜 주지 않더라도 어디에서나 사용할 수 있는 방식이다.
    public static DataBase Instance
    {
        get { return instance; }
        set { instance = value; }
    }

    private void Awake()
    {
        instance = this;

        DontDestroyOnLoad(gameObject); //DataBase를 가진 오브젝트를 Scene이 변경되더라도 사라지지 않게 한다.
    }

    public List<int> cardInventory; //플레이어가 가지고 있는 덱
    public List<GameObject> enemyList; //Prefab으로 만들어 준 적을 리스트 내에 넣어 각 BattleScene에 맞는 적 데이터를 불러올 수 있도록 관리한다.
    public List<Sprite> battleStageList; //배틀 스테이지 sprite 리스트를 각 BattleScene에서 불러올 수 있도록 GameManager에서 사용한다.
    public int stage = 1; //스테이지 선택 화면에서 번호를 받아 GameManager로 넘겨준다.

    public int cardTypes; //카드 종류의 총 개수

    public Status playerStatus; //플레이어의 HP를 계속해서 가져갈 수 있도록 설정한다.
}
