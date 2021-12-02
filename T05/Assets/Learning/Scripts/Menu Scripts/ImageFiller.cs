using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //UnityEngine.UI Using ����

public class ImageFiller : MonoBehaviour
{
    private Image image; //image ���� ����

    void Start()
    {
        image = GetComponent<Image>(); //Image ������Ʈ�� ������
    }

    float timer = 0f; //timer ���� ����
    void Update() //�ð��� ���� Sin �׷��� ���·� 1~0 ���̸� �Դٰ��� �� �� �ֵ��� ��
    {
        timer += Time.deltaTime;
        image.fillAmount = Mathf.Sin(timer) * 0.5f + 0.5f; //Image Ÿ������ ������� image �������� fillAmount�� �����ϸ� Image ������Ʈ�� Fill Amount ���� ������ �� �ְ� �ȴ�.
    }

}
