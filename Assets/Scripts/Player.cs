using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float MaxHealth = 100;
    public float MaxEnergy = 100;
    public float MaxSadness = 100;

    public float EnergyUsePerSecond = 5;
    public float SadnessDropPerSecond = 5;
    public float SadnessIncreasePerUnit = 30;

    public float RefireRateSeconds = 1f;

    public float DamageWhenEnemyCollided = 40f;

    public GameObject ProjectilePrefab;
    public Transform FirePoint;
    public LayerMask ProjectileCollisionMask;

    public SceneManager Manager;

    public Text HealthUi;
    public Text EnergyUi;
    public Text SadnessUi;

    public bool IsRunning;

    private Character _character;

    private float _currentHealth;
    private float _currentEnergy;
    private float _currentSadness;

    private float _currentFiringTime = 0f;

    public void TakeDamage(float damage)
    {
        _currentHealth -= damage;
    }

    public void IncreaseSadness()
    {
        _currentSadness += SadnessIncreasePerUnit;
    }

    public void GainEnergy(float amount)
    {
        _currentEnergy += amount;
        if (_currentEnergy > MaxEnergy)
        {
            _currentEnergy = MaxEnergy;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        _character = GetComponent<Character>();
        InitializeValues();
    }

    void InitializeValues()
    {
        _currentHealth = MaxHealth;
        _currentEnergy = MaxEnergy;
        _currentSadness = 0;
        _character.Direction = 1;
    }

    // Update is called once per frame
    void Update()
    {
        _character.IsRunning = IsRunning;

        if (!IsRunning)
        {
            return;
        }

        if (_currentSadness > MaxSadness || _currentHealth <= 0 || _currentEnergy <= 0)
        {
            EndGame();
        }

        if (_currentSadness > 0)
        {
            _currentSadness -= SadnessDropPerSecond * Time.deltaTime;
        }

        if (_currentEnergy > 0)
        {
            _currentEnergy -= EnergyUsePerSecond * Time.deltaTime;
        }

        if (_currentFiringTime > 0f)
        {
            _currentFiringTime -= Time.deltaTime;
        }

        if (Input.GetKeyDown(_character.Shoot) && _currentFiringTime <= 0f)
        {
            FireProjectile();
            _currentFiringTime = RefireRateSeconds;
        }

        UpdateUi();
    }

    void UpdateUi()
    {
        HealthUi.text = _currentHealth.ToString("N0");
        SadnessUi.text = _currentSadness.ToString("N0");
        EnergyUi.text = _currentEnergy.ToString("N0");
    }

    void FireProjectile()
    {
        var projectileObject = Instantiate(ProjectilePrefab, FirePoint.position, Quaternion.identity);
        var projectile = projectileObject.GetComponent<Projectile>();
        projectile.Direction = 1;
        projectile.CollisionMask = ProjectileCollisionMask;
        projectile.FiredByPlayer = this;
    }

    void EndGame()
    {
        Manager.Failed();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            var enemy = collision.gameObject.GetComponent<AiControl>();
            enemy.DestroyUnit();
            TakeDamage(DamageWhenEnemyCollided);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "WinTrigger")
        {
            Manager.CompleteMission();
        }
    }
}
