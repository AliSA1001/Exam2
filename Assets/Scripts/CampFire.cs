using Unity.VisualScripting;
using UnityEngine;

public class CampFire : MonoBehaviour
{
    [SerializeField] private GameObject Smoke;
    [SerializeField] private GameObject fire;
    [SerializeField] private CharacterController player;
    [SerializeField] private Stats stats;

    public bool FireOn;
    public bool canInteract;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canInteract = true;
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canInteract = false;
            FireOn = false;
        }
    }

    private void Update()
    {
         if(Input.GetKeyDown(KeyCode.E))
        {
            Smoke.SetActive(false);
            fire.SetActive(true);
            FireOn = true;
        }
        if (FireOn)
        {
            stats.isSave = true;
        }
        else if (!FireOn)
        {
            stats.isSave = false;
        }
         

        
    }

}
