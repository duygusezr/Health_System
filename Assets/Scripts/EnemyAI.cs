using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [Header("AI Ayarlar�")]
    [SerializeField] private float takipMenzili = 10f;
    [SerializeField] private float saldiriMenzili = 2f;
    [SerializeField] private float saldiriHizi = 1f;
    [SerializeField] private float hasarMiktari = 10f; // �arp��ma hasar�

    [Header("Can Ayarlar�")]
    [SerializeField] private float maksimumCan = 100f;
    private float mevcutCan;
    [SerializeField] private GameObject olumEfektiPrefab;

    private Transform oyuncu;
    private NavMeshAgent agent;
    private float sonSaldiriZamani;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        oyuncu = GameObject.FindGameObjectWithTag("Player")?.transform;

        if (oyuncu == null)
        {
            Debug.LogError("HATA: Oyuncu bulunamad�! 'Player' tag'ini kontrol et.");
            enabled = false;
            return;
        }

        mevcutCan = maksimumCan;
    }

    private void Update()
    {
        if (oyuncu == null) return;

        float mesafe = Vector3.Distance(transform.position, oyuncu.position);
        if (mesafe <= saldiriMenzili)
        {
            Saldir();
        }
        else
        {
            OyuncuyuTakipEt();
        }
    }

    private void OyuncuyuTakipEt()
    {
        if (agent != null && agent.isOnNavMesh)
        {
            agent.SetDestination(oyuncu.position);
        }
    }

    private void Saldir()
    {
        if (Time.time >= sonSaldiriZamani + saldiriHizi)
        {
            Debug.Log("D��man sald�r�yor!");
            sonSaldiriZamani = Time.time;
        }
    }

    public void HasarAl(float hasarMiktari)
    {
        mevcutCan -= hasarMiktari;
        Debug.Log($"D��man hasar ald�: {hasarMiktari}. Kalan can: {mevcutCan}");

        if (mevcutCan <= 0)
        {
            Ol();
        }
    }

    private void Ol()
    {
        if (olumEfektiPrefab != null)
        {
            Instantiate(olumEfektiPrefab, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }

    // **Oyuncuya �arpt���nda hasar ver**
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.HasarAl(hasarMiktari);
            }
        }
    }
}
