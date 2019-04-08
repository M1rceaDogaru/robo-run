using UnityEngine;

public class Character : MonoBehaviour
{
    public int Direction;
    public float Speed = 5;
    public float JumpForce = 10;
    public bool IsPlayer;

    public KeyCode Jump = KeyCode.Space;
    public KeyCode Shoot = KeyCode.LeftControl;

    public bool IsRunning;

    private Rigidbody2D _rigidbody;
    private SpriteRenderer _renderer;

    private bool _isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _renderer = GetComponentInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        _renderer.flipX = Direction < 0;
    }

    private void FixedUpdate()
    {
        if (!IsRunning)
        {
            _rigidbody.velocity = Vector2.zero;
            return;
        }

        var movement = new Vector2(Speed * Time.fixedDeltaTime * Direction, _rigidbody.velocity.y);
        _rigidbody.velocity = movement;

        if (IsPlayer)
        {
            if (_rigidbody.velocity.y == 0 && Input.GetKeyDown(Jump))
            {
                _rigidbody.AddForce(new Vector2(0, JumpForce), ForceMode2D.Impulse);
            }
        }

        
    }
}
