using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public enum CardComponent
{
    ATK, SHD, HEL, SEL
}

[Serializable]
public class ComponentStats
{
    public CardComponent component;
    public int value;
}

[Serializable]
public class CardStats
{
    public int index;
    public string name;
    public int count;
    public int rare;
    public Sprite image;
    public List<ComponentStats> components;
}

public class CardInfo : MonoBehaviour
{
    public CardStats stats;

    public Text txtName;
    public Text txtCount;
    public GameObject componentContainer;
    public List<GameObject> components;

    void Init()
    {
        for(int i = 0; i < componentContainer.transform.childCount; i++)
        {
            components.Add(componentContainer.transform.GetChild(i).gameObject);
        }
    }

    public void SetCardInfo(Dictionary<string, string> _data)
    {
        Init();
        stats.components.Clear();
        
        stats.index = int.Parse(_data["Index"]);
        stats.name = _data["Name"];
        stats.count = int.Parse(_data["Count"]);
        stats.rare = int.Parse(_data["Rare"]);

        for (int i = 0; i < 6; i++)
        {
            CardComponent _component = CardComponent.ATK;

            if (_data["Component_" + i] == "ATK")
                _component = CardComponent.ATK;
            else if (_data["Component_" + i] == "SHD")
                _component = CardComponent.SHD;
            else if (_data["Component_" + i] == "HEL")
                _component = CardComponent.HEL;
            else if (_data["Component_" + i] == "SEL")
                _component = CardComponent.SEL;

            ComponentStats _stats = new ComponentStats()
            {
                component = _component,
                value = int.Parse(_data["Value_" + i])
            };

            stats.components.Add(_stats);
        }

        SetCardComponents();
    }

    void SetCardComponents()
    {
        txtName.text = stats.name;
        txtCount.text = stats.count.ToString();

        for(int i = 0; i < stats.components.Count; i++)
        {
            // 아이콘 변경 : components[i].transform.GetChild(0)
            components[i].transform.GetChild(1).GetComponent<Text>().text = stats.components[i].value.ToString();
        }
    }
}
