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
        text = GetComponent<Text>(); //���� ȭ���� �� �̸�
        StartCoroutine(Typing());
    }

    IEnumerator Typing()
    {
        yield return new WaitForSeconds(17.0f);
        //text.DOText("Made by OSP_Team 5", 5f);
        yield return new WaitForSeconds(10f); //10�� ���� ����ȭ������ �̵��ϴ� ��ư�� ������ ������ �ڵ����� �̵�
        DataBase.Instance.cardInventory = DataBase.Instance.FirstCardInventory; //ī�� ����Ʈ �ʱ�ȭ
        DataBase.Instance.stage = 0; //�������� �ʱ�ȭ
        DataBase.Instance.playerStatus.HP = 50; //�÷��̾� ü�� �ʱ�ȭ
        yield return new WaitForSeconds(0.1f);
        SceneManager.LoadScene("StartScene");
    }
}
