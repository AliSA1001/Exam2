using TMPro;
using UnityEngine;

public class Stats : MonoBehaviour
{
    [SerializeField] private float hp = 80; 
    [SerializeField] private float maxHp = 100; 
    public bool isSave = false;
    [SerializeField] private TMP_Text hp_text;

    public void DecreaseHp()
    {
        hp -= 1 * Time.deltaTime;
    }

    public void IncreasHP()
    {
        hp += 5 * Time.deltaTime;
    }

    private void Update()
    {
        if (isSave)
        {
            IncreasHP();
        }
        else
        {
            DecreaseHp();
        }

        if (hp > maxHp)
        {
            hp = maxHp;
        }
        if (hp < 0)
        {
            hp = 0; 
        }

        
        hp_text.text = Mathf.CeilToInt(hp).ToString();
    }
}