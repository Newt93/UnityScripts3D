using UnityEngine;
using System.Collections;

public class PlayerCharacterController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float dodgeSpeed = 10f;
    public float rollDuration = 0.5f;
    public float attackDamage = 10f;
    public float jumpForce = 5f;
    private bool isJumping = false;
    private float currentHealth;
    public float maxHealth = 100f;

    private bool isRolling = false;
    private float rollTimer = 0f;

    private Rigidbody rb;
    private Animator anim;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        currentHealth = maxHealth;
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontal, 0f, vertical) * moveSpeed * Time.deltaTime;

        if (!isRolling)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                StartCoroutine(Dodge(movement));
            }
            else if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                StartCoroutine(Roll());
            }
            else if (Input.GetMouseButtonDown(0))
            {
                anim.SetTrigger("Attack");
            }
            else
            {
                rb.MovePosition(transform.position + movement);
                anim.SetFloat("Speed", movement.magnitude);
            }
        }

        if (isRolling)
        {
            rollTimer += Time.deltaTime;

            if (rollTimer >= rollDuration)
            {
                isRolling = false;
                rollTimer = 0f;
            }
        }
    }
    
    void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        isJumping = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
        }

    IEnumerator Dodge(Vector3 movement)
    {
        anim.SetTrigger("Dodge");
        rb.AddForce(movement * dodgeSpeed, ForceMode.Impulse);
        yield return new WaitForSeconds(0.3f);
    }

    IEnumerator Roll()
    {
        isRolling = true;
        anim.SetTrigger("Roll");
        rb.AddForce(transform.forward * dodgeSpeed, ForceMode.Impulse);
        yield return new WaitForSeconds(rollDuration);
        isRolling = false;
    }
    
    

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void OnAttack()
    {
        // Call attack function on any enemies hit by the attack
    }

    void Die()
    {
        // Death animation and respawn or game over
    }
}
