using UnityEngine;

public class HealingPad : MonoBehaviour
{
    [SerializeField] private float iyilesmeMiktari = 20f; // K�p ba��na iyile�me miktar�

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.Iyiles(iyilesmeMiktari);
                Debug.Log("Oyuncu iyile�ti: " + iyilesmeMiktari);

                // K�p� sahneden kald�r
                Destroy(gameObject);
            }
        }
    }
}
