
using UnityEngine;

public class PlayerMeleeAttack : MonoBehaviour
{
    public Animator animator;
    private Vector2 dirInput;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>(); 
    }

    // Update is called once per frame
    void Update()
    {
        dirInput.x = Input.GetAxis("Horizontal");
        dirInput.y = Input.GetAxis("Vertical");
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Attack();
        }
    }

    void Attack()
    {
        animator.SetFloat("InputX", dirInput.x);
        animator.SetFloat("InputY", dirInput.y);

        animator.SetTrigger("Attack");
    }
}
