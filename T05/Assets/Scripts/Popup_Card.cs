using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Popup_Card : MonoBehaviour
{
    //획득의 경우 덱으로, 제거의 경우 덱에서 제거 (DataBase의 리스트 이용)

    public float expandRatio = 1.3f; //확대되었을 때 비율
    public bool isClicked = false;

    public void PointerEnter() //마우스를 올렸을 때 살짝 확대하도록 한다.
    {
        //SoundManager.PlaySFX("TouchCard");
        transform.localScale = Vector3.one * expandRatio; //카드 확대, new Vector3(expandRatio, expandRatio, expandRatio)와 같다.
    }

    public void PointerExit() //마우스를 내렸을 때 다시 원래 크기로 되돌린다.
    {
        transform.localScale = Vector3.one; //카드를 원래 크기로 돌린다.
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
