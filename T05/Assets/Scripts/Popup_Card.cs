using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Popup_Card : MonoBehaviour
{
    //ȹ���� ��� ������, ������ ��� ������ ���� (DataBase�� ����Ʈ �̿�)

    public float expandRatio = 1.3f; //Ȯ��Ǿ��� �� ����
    public bool isClicked = false;

    public void PointerEnter() //���콺�� �÷��� �� ��¦ Ȯ���ϵ��� �Ѵ�.
    {
        //SoundManager.PlaySFX("TouchCard");
        transform.localScale = Vector3.one * expandRatio; //ī�� Ȯ��, new Vector3(expandRatio, expandRatio, expandRatio)�� ����.
    }

    public void PointerExit() //���콺�� ������ �� �ٽ� ���� ũ��� �ǵ�����.
    {
        transform.localScale = Vector3.one; //ī�带 ���� ũ��� ������.
    }

    public void CardClick()
    {
        if(!isClicked)
        {
            //SoundManager.PlaySFX("GetCard");
            DataBase.Instance.cardInventory.Add(GetComponent<Popup_CardInfo>().__stats.__index);
            GameManager.Instance.player_Victory.SetActive(true);
            isClicked = true;
        }
    }

    public void CardClick_Delete()
    {
        if(!isClicked)
        {
            //SoundManager.PlaySFX("GetCard");
            int _cardIndex = GetComponent<Popup_CardInfo>().__stats.__index;
            int _cardNum = DataBase.Instance.cardInventory.BinarySearch(_cardIndex);
            DataBase.Instance.cardInventory.RemoveAt(_cardNum);
            GameManager.Instance.player_Victory.SetActive(true);
            isClicked = true;
        }
    }
}
