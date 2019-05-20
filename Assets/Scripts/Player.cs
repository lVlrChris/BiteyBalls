using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private List<PowerUp> powerUps;
    public PlayerInfo playerInfo;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddPowerUp(PowerUp powerUp)
    {
        powerUps.Add(powerUp);
    }

    public void RemovePowerUp(PowerUp powerUp)
    {
        powerUps.Remove(powerUp);

        //Recalculate stats
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PowerUp"))
        {
            other.GetComponent<PowerUp>().PickUp(gameObject);
        }
    }
}
