using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingController : MonoBehaviour
{
    public Animator animator;

    private void Start()
    {
        EndingScene();
        //SoundManager.PlaySound(4);
    }

    public void EndingScene()
    {
        animator.SetTrigger("Ending"); //엔딩 배경화면 애니메이션 적용
    }
    public void OnClick()
    {
        DataBase.Instance.cardInventory = DataBase.Instance.FirstCardInventory; //카드 리스트 초기화
        DataBase.Instance.stage = 0; //스테이지 초기화
        DataBase.Instance.playerStatus.HP = 50; //플레이어 체력 초기화
    }
}
