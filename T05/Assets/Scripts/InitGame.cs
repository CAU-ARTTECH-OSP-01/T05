using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitGame : MonoBehaviour
{
    public void OnClick() //패배 화면의 버튼 클릭 시 게임 초기화
    {
        DataBase.Instance.cardInventory = DataBase.Instance.FirstCardInventory; //카드 리스트 초기화
        DataBase.Instance.stage = 0; //스테이지 초기화
    }
}
