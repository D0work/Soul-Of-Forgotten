using UnityEngine;

public class AttributeManager : MonoBehaviour
{
    public static AttributeManager attributeInstance;

    public bool isFire = false;
    public bool isIce = false;
    public bool isWind = false;
    public bool isHero = false;
    public int level = 0;

    void Awake()
    {
        if (attributeInstance != null)
        {
            return;
        }

        attributeInstance = this;
        DontDestroyOnLoad(gameObject);
    }


    public void setIce()
    {
        this.isIce = true;
    }

    public void setFire()
    {
        this.isFire = true;
    }

    public void setWind()
    {
        this.isWind = true;
    }

    public void setHero()
    {
        this.isHero = true;
    }

    public void setLevel(int lvl = 1)
    {
        this.level = lvl;
    }
}
