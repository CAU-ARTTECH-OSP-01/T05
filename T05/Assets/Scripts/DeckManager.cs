using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckManager : MonoBehaviour //�� ��ư�� ������Ʈ�� �־��ش�.
{
    private static DeckManager instance; //�� ���� ������ �� �ϳ��� �����ϱ� ������, Singleton �������� ������ش�.
                                         //�ٸ� ������ ��ӽ��� ���� �ʴ��� ����� �� �ִ� ����̴�.
    public static DeckManager Instance
    {
        get { return instance; } //���� ����� �� instance�� ��ȯ
        set { instance = value; } //value ���� ����� �� instance�� �־��ش�.
    }

    private void Awake() //start���� ���� ����Ǵ� �Լ�
    {
        instance = this;
    }

    public List<int> deckList; //������ �ִ� ī���� ������ ������ ����Ʈ�� �־��ش�.
    
}
