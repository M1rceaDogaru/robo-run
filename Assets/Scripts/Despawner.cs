using UnityEngine;

public class Despawner : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Ground")
        {
            return;
        }

        Destroy(collision.gameObject);
    }
}
