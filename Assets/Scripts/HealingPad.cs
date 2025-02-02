using UnityEngine;

public class HealingPad : MonoBehaviour
{
    [SerializeField] private float iyilesmeMiktari = 20f; // Küp baþýna iyileþme miktarý

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.Iyiles(iyilesmeMiktari);
                Debug.Log("Oyuncu iyileþti: " + iyilesmeMiktari);

                // Küpü sahneden kaldýr
                Destroy(gameObject);
            }
        }
    }
}
