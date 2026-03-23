using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Attributes : MonoBehaviour
{
    public bool fire = false;
    public bool ice = false;
    public bool wind = false;
    public bool hero = false;
    public int level = 0;

    private void Awake()
    {
        fire = AttributeManager.attributeInstance.isFire;
        ice = AttributeManager.attributeInstance.isIce;
        wind = AttributeManager.attributeInstance.isWind;
        hero = AttributeManager.attributeInstance.isHero;
        level = AttributeManager.attributeInstance.level;
    }

    public List<string> GetActiveAttributes()
    {
        List<string> activeAttributes = new List<string>();

        if (fire) activeAttributes.Add("fire");
        if (ice) activeAttributes.Add("ice");
        if (wind) activeAttributes.Add("wind");
        if (hero) activeAttributes.Add("hero");

        return activeAttributes;
    }

    public bool IsAttributeActive(string attribute)
    {
        List<string> activeAttributes = GetActiveAttributes();
        return activeAttributes.Contains(attribute.ToLower());
    }

    public void setAttribute()
    {
        string materialKey = TempleData.Instance.materialKey;
        Debug.Log(materialKey);
        switch (materialKey)
        {
            case "Fire":
                setFire();
                break;
            case "Ice":
                setIce();
                break;
            case "Wind":
                setWind();
                break;
            case "Hero":
                setHero();
                break;
            default:
                Debug.LogWarning($"Attributes: key « {materialKey} » with TempleData {(TempleData.Instance == null ? "exist" : "not exist")}");
                break;
        }
    }
    public void setIce()
    {
        this.ice = true;
        AttributeManager.attributeInstance.isIce = true;
    }

    public void setFire()
    {
        this.fire = true;
        AttributeManager.attributeInstance.isFire = true;
    }

    public void setWind()
    {
        this.wind = true;
        AttributeManager.attributeInstance.isWind = true;
    }

    public void setHero()
    {
        this.hero = true;
        AttributeManager.attributeInstance.isHero = true;
    }

    public void addLevel()
    {
        this.level++;
    }
}
