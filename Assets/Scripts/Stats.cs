using TMPro;
using UnityEngine;

public class Stats : MonoBehaviour
{
    [SerializeField] private float hp = 100;
     public bool isSave = false;
    [SerializeField] private TMP_Text hp_text;
    

    public void DecreaseHp()
    {
        hp = hp - (1* Time.deltaTime);
    }

    public void IncreasHP()
    {
        hp = hp + (5* Time.deltaTime);
    }

    private void Update()
    {
        if (isSave)
        {
            IncreasHP();
        }
        else if(!isSave)
        {
            DecreaseHp();

        }

        hp_text.text = hp.ToString();

        if(hp > 100)
        {
            hp = 100;
        }
    }
}
