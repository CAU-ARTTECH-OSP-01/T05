using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageChanger : MonoBehaviour
{
    private Image image; //Image ������Ʈ�� ��� ���� image ���� ����

    [SerializeField] //Sprite �迭�� Inspector �信�� ä���ֱ� ���� SerializedField ��Ʈ����Ʈ�� ����
    // [SerializeField]�� �����ϰ� �Ǹ� Inspector������ ��������, Publicó�� �ٸ� Ŭ���������� ���� �� �� ����.
    private Sprite[] sprites; //���ʴ�� ����� Sprite���� ��� ���� Sprite �迭 ����

    private int index; //���� � �̹����� �����ְ� �ִ����� ���� index ���� ����

    void Start()
    {
        image = GetComponent<Image>(); // ���� ������Ʈ�� �����Ǿ� �ִ� Image ������Ʈ�� ������
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)) //image.sprite�� �����̽� Ű�� ���� ������ sprites �迭�� index ��° �̹����� �ٲٵ��� �ڵ� �ۼ�
        {
            image.sprite = sprites[index]; //Image ������Ʈ�� Source Image�� ��ũ��Ʈ���� image.sprite�� �����ϸ� ���� �� �� �ִ�.
            index++; //1�� �������� ���� �����̽� Ű���� ���� �̹����� �������� ��
            if(sprites.Length == index)
            {
                index = 0; // index ���� sprites�� ���� �̹��� ���� �Ѿ�� �Ǹ� ������ �߻��ϹǷ� ������ ���� �迭 ���̿� index ���� �������� 0���� �ٲ��ش�.
            }
        }
        
    }
}
