using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardController : MonoBehaviour //ī�带 �����ϰ�, �巡���ϴ� �� ī�� �ൿ ���� ��ũ��Ʈ
{
    public CardInfo info; //ī�� ��� Ƚ���� �������� ���� ����, ������Ʈ�� ī�� ������Ʈ�� �־� ���

    public float defaultPos_x = 0f; //�⺻ x��
    public float expandRatio = 1.3f; //Ȯ��Ǿ��� �� ����
    public float expandOffset_y = 70f; //Ȯ��Ǿ��� �� y �̵� ��
    public float defaultOffset_y = -100f; //�⺻ y�� (ī���� ��ġ�� ��¦ �Ʒ��� ������ ���� ��)
    public float usePos_y = 400f; //ī�尡 ���ǰ� �ν��� ����

    public bool isClicked;
    public bool isDeck = true; //�ڵ�� ���� �Ǵ� ��Ȳ���� false�� �ٲ��ش�.

    public Camera cam;

    public void SetCardDefaultPos() //HandManager�� SetCardPositions�� �־� ó�� ī���� ��ġ�� �������ֵ��� �Ѵ�.
    {
        transform.GetComponent<RectTransform>().anchoredPosition
            = new Vector3(defaultPos_x, defaultOffset_y, 0);
    }

    public void PointerEnter() //ī�� ������Ʈ�� Event Trigger ������Ʈ ���� ��, Pointer Enter�� �߰��Ͽ� ����Ѵ�.
    {
        if (!isDeck)
        {
            transform.localScale = Vector3.one * expandRatio; //ī�� Ȯ��, new Vector3(expandRatio, expandRatio, expandRatio)�� ����.

            transform.GetComponent<RectTransform>().anchoredPosition //RectTransform�� anchoredPosition�� �̿��ϸ� Inspectorâ���� ���̴� ��ǥ�� �״�� ����� �� �ִ�.
                = new Vector3(defaultPos_x, expandOffset_y, 0); //ī�� ��ġ ��ȭ

            transform.SetSiblingIndex(transform.parent.childCount - 1); //SetSiblingIndex = �θ� ���� ����
                                                                        //ī���� ������ �� �Ʒ��� �����Ͽ� UI�� ���� ���� ���̵��� �Ѵ�.
        }
    }
    public void PointerExit() //���� ���·� �ǵ�����.
    {
        if (!isDeck)
        {
            transform.localScale = Vector3.one;

            transform.GetComponent<RectTransform>().anchoredPosition
                = new Vector3(defaultPos_x, defaultOffset_y, 0);
        }
    }

    public void CardClick()
    {
        if (!isClicked && !isDeck)
        {
            isClicked = true;
            cam = GameObject.Find("Main Camera").GetComponent<Camera>(); //Main Camera ������Ʈ�� �� ���� Camera ������Ʈ�� �����´�.
        }
    }

    public void CardDrag()
    {
        if (isClicked && !isDeck)
        {
            Vector2 _mousePosition = Input.mousePosition; //���콺 ��ġ�� _mousePosition���� �޾ƿ´�.
            Vector2 _targetPosition = cam.ScreenToWorldPoint(_mousePosition); // ���콺 ��ġ�� ���� ��ǥ�� �����Ѵ�.

            transform.position = _targetPosition; //ī�� ������Ʈ�� ���� ��ǥ�� ���콺 ��ġ�� �����Ѵ�.
        }
    }
    public void CardDrop()
    {
        if (isClicked && !isDeck)
        {
            if (transform.GetComponent<RectTransform>().anchoredPosition.y >= usePos_y) //��� ���̺��� ���ų� ���� �� ī�尡 ���ǵ��� ����
            {
                print("ī�� ���");
                info.CardUse(); //ī�� ��뿡 ���� ��Ȳ�� �˷��ֱ� ���� CardInfo�� CardUse �Լ� ���
            }
            else //ī�� ����� ���� �ʾ��� �� ���� ��ġ�� ���ư����� ����
            {
                transform.GetComponent<RectTransform>().anchoredPosition
                    = new Vector3(defaultPos_x, defaultOffset_y, 0); //�⺻ ��ġ�� ī�� ���ư����� ����
            }

            isClicked = false; //isClicked �ʱ�ȭ
        }
    }
}
