using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using DG.Tweening;
using UnityEngine.SceneManagement;

public class TextEffect_Team : MonoBehaviour
{
    Text text;
    private void Start()
    {
        text = GetComponent<Text>(); //엔딩 화면의 팀 이름
        StartCoroutine(Typing());
    }

    IEnumerator Typing()
    {
        yield return new WaitForSeconds(17.0f);
        //text.DOText("Made by OSP_Team 5", 5f);
        yield return new WaitForSeconds(10f); //10초 동안 메인화면으로 이동하는 버튼을 누르지 않으면 자동으로 이동
        DataBase.Instance.cardInventory = DataBase.Instance.FirstCardInventory; //카드 리스트 초기화
        DataBase.Instance.stage = 0; //스테이지 초기화
        DataBase.Instance.playerStatus.HP = 50; //플레이어 체력 초기화
        yield return new WaitForSeconds(0.1f);
        SceneManager.LoadScene("StartScene");
    }
}
