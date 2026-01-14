using UnityEngine;

public class Enemy : MonoBehaviour
{
    Transform player;
    [SerializeField] float speed = 10f;

    // Variabila de viata (Nu apare explicit in video la inceput, dar e necesara pentru Hit)
    [SerializeField] int health = 100;

    Animator anim;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponent<Animator>();
    }

    void Update()
    {   
        if (player != null)
        {
            // Miscare simpla spre player
            Vector3 direction = (player.position - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;
        }
    }

    // Metoda HIT (Minutul 08:36 in video o scrie partial, apoi o completeaza)
    public void Hit(int damage)
    {
        health -= damage;

        // Declansam animatia (Minutul 09:48)
        if (anim != null)
        {
            anim.SetTrigger("Hit");
        }

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    // Metoda pentru cand loveste playerul (o aveai din video-ul trecut)
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Aici va veni logica de damage player mai tarziu
        }
    }
}