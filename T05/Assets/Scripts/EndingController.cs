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
        animator.SetTrigger("Ending"); //���� ���ȭ�� �ִϸ��̼� ����
    }
    public void OnClick()
    {
        DataBase.Instance.cardInventory = DataBase.Instance.FirstCardInventory; //ī�� ����Ʈ �ʱ�ȭ
        DataBase.Instance.stage = 0; //�������� �ʱ�ȭ
        DataBase.Instance.playerStatus.HP = 50; //�÷��̾� ü�� �ʱ�ȭ
    }
}
