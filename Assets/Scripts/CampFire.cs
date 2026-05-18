using UnityEngine;

public class CampFire : MonoBehaviour
{
    [SerializeField] private GameObject Smoke;
    [SerializeField] private GameObject fire;

    private Stats activePlayerStats;

    public bool FireOn;
    public bool canInteract;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canInteract = true;

            if (other.TryGetComponent<Stats>(out Stats foundStats))
            {
                activePlayerStats = foundStats;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canInteract = false;

            if (activePlayerStats != null)
            {
                activePlayerStats.isSave = false;
            }

            FireOn = false;
            fire.SetActive(false);
            Smoke.SetActive(true); 

            activePlayerStats = null;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && canInteract)
        {
            if (!FireOn)
            {
                Smoke.SetActive(false);
                fire.SetActive(true);
                FireOn = true;
            }
        }

        if (activePlayerStats != null)
        {
            activePlayerStats.isSave = FireOn;
        }
    }
}