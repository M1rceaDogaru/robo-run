using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float Speed = 30;
    public LayerMask CollisionMask;
    public int Direction;
    public float Damage = 30;

    public AudioSource ImpactSound;

    public Player FiredByPlayer;

    private Rigidbody2D _rigidbody;
    private SpriteRenderer _renderer;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _renderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        var movement = new Vector2(Speed * Time.fixedDeltaTime * Direction, _rigidbody.velocity.y);
        _rigidbody.velocity = movement;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (CollisionMask == (CollisionMask | (1 << collision.gameObject.layer)))
        {
            var player = collision.gameObject.GetComponent<Player>();
            if (player != null)
            {
                player.TakeDamage(Damage);
            }

            var ai = collision.gameObject.GetComponent<AiControl>();
            if (ai != null)
            {
                ai.DestroyUnit();
                if (FiredByPlayer != null)
                {
                    FiredByPlayer.IncreaseSadness();
                }
            }

            ImpactSound.Play();
            _renderer.enabled = false;
            Destroy(gameObject, 0.5f);
        }
    }
}
