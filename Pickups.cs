using UnityEngine;

public class Pickup : MonoBehaviour
{
    public int pickupAmount = 1;
    public GameObject pickupEffect;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Add pickup amount to player's inventory or health
            // You can use a script on the player object to handle this
            other.GetComponent<PlayerInventory>().AddPickup(pickupAmount);

            // Instantiate pickup effect
            Instantiate(pickupEffect, transform.position, transform.rotation);

            // Destroy pickup object
            Destroy(gameObject);
        }
    }
}
