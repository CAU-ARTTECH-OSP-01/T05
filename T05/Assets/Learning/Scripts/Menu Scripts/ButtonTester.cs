using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonTester : MonoBehaviour
{
    Button button; //Button ���� ����

    public void OnclickButton()
    {
        Debug.Log("Button Click!");
    }

    void Start()
    {
        button = GetComponent<Button>(); //GetComponent�� ���� ������Ʈ�� ������ Button ������Ʈ�� �����´�.
        button.onClick.AddListener(OnclickButton); // AddListener�� �Լ��� �־� ȣ��ǵ��� �Ѵ�.
    }

    void Update()
    {
        
    }
}
