using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using DG.Tweening;

public class TextEffect : MonoBehaviour
{
    Text text;
    private void Start() //���� ȭ�鿡���� ȯ�� ���� �۱�
    {
        text = GetComponent<Text>();
        StartCoroutine(Typing());
    }

    IEnumerator Typing()
    {
        yield return new WaitForSeconds(0.5f);
        //text.DOText("�ڿ��� '����'�� ���ؾ� �ϴ� ����� �ƴϴ�.\n��Ƴ��� ���� �ݵ�� ����� �� �츮�� �����ڸ��̴�.\n\n�� ��° ������ ����_Ÿ�Ϸ� ��", 15.0f);
    }

}
