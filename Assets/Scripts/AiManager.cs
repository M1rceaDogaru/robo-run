using UnityEngine;

public class AiManager : MonoBehaviour
{
    public float MinRespawnSeconds = 3f;
    public float MaxRespawnSeconds = 5f;

    public GameObject EnemyPrefab;
    public Transform RespawnPoint;

    public bool IsRunning;

    private float _currentRespawnTime;

    // Update is called once per frame
    void Update()
    {
        if (!IsRunning)
        {
            return;
        }

        _currentRespawnTime -= Time.deltaTime;
        if (_currentRespawnTime <= 0f)
        {
            var unitTypeToRespawn = Random.Range(1, 3);

            var unit = Instantiate(EnemyPrefab, RespawnPoint.position, Quaternion.identity);
            var ai = unit.GetComponent<AiControl>();
            ai.EnemyType = unitTypeToRespawn == 1 ? EnemyType.Runner : EnemyType.Shooter;

            var timeToNextRespawn = Random.Range(MinRespawnSeconds, MaxRespawnSeconds);
            _currentRespawnTime = timeToNextRespawn;
        }
    }
}
