using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public Status playerStatus; //EnemyController에서 사용했던 Status를 그대로 사용하여 playerStatus를 만든다.
    public Status playerStatus_Current; //현재 플레이어의 상태를 가져온다.

    public List<int> cardInventory; //플레이어가 가지고 있는 덱

    public GameObject ui_Shield; //쉴드 UI의 on/off를 위해 생성
    public Text ui_ShieldCount; //쉴드 Text

    public Image ui_HPGauge; // HP Image의 Filled 값을 조절하기 위해 생성
    public Text ui_HPCount; //HP Text

    private void Start()
    {
        Status _status = new Status() //enemyStatus_Current = enemyStatus로 구성하게 되면 주소 값을 받아오기 때문에 변경을 해도 값이 같은 문제가 발생한다.
                                      //해결을 위해 다른 주소 값에 넣어주고 가져오는 방식으로 구성한다.
        {
            HP = playerStatus.HP,
            Shield = playerStatus.Shield
        };
        playerStatus_Current = _status; //시작하면서 현재 적의 Status와 기본 값을 동일하게 맞춰준다.

        SetPlayerUI();
        SetPlayerDeck();
    }

    private void Update()
    {
        SetPlayerUI();
    }

    void SetPlayerUI()
    {
        if (playerStatus_Current.Shield > 0) //쉴드 이미지를 보여줄 지 결정해준다.
            ui_Shield.SetActive(true);
        else
            ui_Shield.SetActive(false);

        ui_ShieldCount.text = playerStatus_Current.Shield.ToString(); //현재 플레이어의 쉴드 값을 보여준다.

        ui_HPGauge.fillAmount = (float)playerStatus_Current.HP / (float)playerStatus.HP; //int 계산으로는 0 또는 1만 나오기 때문에 float으로 바꿔준다.
                                                                                         //Hp의 Fill Amount 값을 현재 HP에 따라 조절해준다.

        ui_HPCount.text = playerStatus_Current.HP.ToString(); //현재 플레이어 HP 값을 보여준다.
    }

    void SetPlayerDeck()
    {
        List<int> _list = new List<int>(); //_list 리스트에 새로운 주소 값 할당

        for (int i = 0; i < DataBase.Instance.cardInventory.Count; i++)
        {
            int _temp = DataBase.Instance.cardInventory[i]; //_temp 값에 DataBase에 존재하는 덱의 숫자를 넣어준다.
            _list.Add(_temp); //_temp 값을 위에서 만든 _list에 넣어준다.
        }

        cardInventory = _list; //위에서 만든 _list를 cardInventory에 넣어준다.

        DeckManager.Instance.deckList = _list; //DeckManager의 덱에도 _list를 넣어 관리할 수 있도록 한다.
        DeckManager.Instance.Init(); //_list의 값을 바탕으로 덱의 stats 등을 만들어준다.
    }

    public void GetDamage(int _dmg) //쉴드의 유무에 따라 데미지 계산이 달라지는 상황 계산
    {
        if (playerStatus_Current.Shield <= _dmg) //쉴드량이 더 작다면 쉴드부터 제거된 후 HP 계산
        {
            playerStatus_Current.HP -= (_dmg - playerStatus_Current.Shield);
            playerStatus_Current.Shield = 0;

            if (playerStatus_Current.HP <= 0) //HP가 0보다 작다면 죽음 처리
            {
                playerStatus_Current.HP = 0;
                print("플레이어 죽음");
            }
        }
        else
            playerStatus_Current.Shield -= _dmg; //쉴드량이 충분하다면 쉴드에 데미지 계산
    }

    public void GetShield(int _shield) //쉴드를 추가해준다.
    {
        playerStatus_Current.Shield += _shield;
    }

    public void GetHeal(int _Heal) //회복을 하게 되는 상황
    {
        playerStatus_Current.HP += _Heal;

        if (playerStatus_Current.HP > playerStatus.HP) //회복량이 처음 수치를 넘어가게 될 경우, 원래 status의 체력 상태로 보여준다.
        {
            int firstHP = playerStatus.HP;
            playerStatus_Current.HP = firstHP;

            print("회복 완료");
        }
    }
}
