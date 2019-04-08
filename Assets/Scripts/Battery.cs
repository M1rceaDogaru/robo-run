using UnityEngine;

public class Battery : MonoBehaviour
{
    public float EnergyAmount = 50;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.gameObject.GetComponent<Player>();

        if (player != null)
        {
            player.GainEnergy(EnergyAmount);
            Destroy(gameObject);
        }
    }
}
