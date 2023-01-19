using UnityEngine;
using System.Collections;

public class BossBattle : MonoBehaviour
{
    public GameObject boss;
    public GameObject player;

    private float bossHealth;
    private float playerDamage;
    private float bossDamage;
    private float attackInterval;

    void Start()
    {
        bossHealth = 100f;
        playerDamage = 10f;
        bossDamage = 20f;
        attackInterval = 2f; //interval between each attack
    }

    void Update()
    {
        if (boss != null)
        {
            if (Input.GetMouseButtonDown(0))
            {
                bossHealth -= playerDamage;
                Debug.Log("Player dealt " + playerDamage + " damage to the boss. Boss health: " + bossHealth);
            }

            if (bossHealth > 0)
            {
                attackInterval -= Time.deltaTime;
                if (attackInterval <= 0)
                {
                    Attack();
                    attackInterval = 2f;
                }
            }

            if (bossHealth <= 0)
            {
                Debug.Log("Boss defeated!");
                Destroy(boss);
            }
        }
    }

    void Attack()
    {
        player.GetComponent<PlayerHealth>().TakeDamage(bossDamage);
        Debug.Log("Boss dealt " + bossDamage + " damage to the player.");
    }
}
