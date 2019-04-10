using UnityEngine;

public class AiControl : MonoBehaviour
{
    public bool IsRunning;
    public EnemyType EnemyType;

    public float RefireRateSeconds = 2f;

    public GameObject BatteryPrefab;
    public GameObject ProjectilePrefab;
    public Transform FirePoint;
    public LayerMask ProjectileCollisionMask;

    public AudioSource FireSound;

    private Character _character;
    private float _currentFireTime;

    public void DestroyUnit()
    {
        // instantiate battery
        Instantiate(BatteryPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        _character = GetComponent<Character>();
        _character.Direction = -1;
    }

    // Update is called once per frame
    void Update()
    {
        _character.IsRunning = IsRunning;

        if (!IsRunning)
        {
            return;
        }

        if (EnemyType == EnemyType.Shooter)
        {
            _character.Speed = 0;
            _currentFireTime -= Time.deltaTime;
            if (_currentFireTime <= 0)
            {
                FireProjectile();
                _currentFireTime = RefireRateSeconds;
            }
        }
    }

    void FireProjectile()
    {
        var projectileObject = Instantiate(ProjectilePrefab, FirePoint.position, Quaternion.identity);
        var projectile = projectileObject.GetComponent<Projectile>();
        projectile.Direction = -1;
        projectile.CollisionMask = ProjectileCollisionMask;

        FireSound.Play();
    }
}

public enum EnemyType
{
    Runner,
    Shooter
}
