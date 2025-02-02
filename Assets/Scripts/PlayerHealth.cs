using UnityEngine;
using UnityEngine.UI;
using TMPro; // TextMeshPro kullanýmý için gerekli

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float maksimumCan = 100f;
    private float mevcutCan;

    [Header("UI Ayarlarý")]
    [SerializeField] private Slider healthBar; // UI'daki saðlýk barý
    [SerializeField] private TextMeshProUGUI healthText; // UI'daki saðlýk yüzdesi

    private void Start()
    {
        mevcutCan = maksimumCan;

        // UI Saðlýk çubuðunu baþlangýçta tam dolu yap
        if (healthBar != null)
        {
            healthBar.maxValue = maksimumCan;
            healthBar.value = mevcutCan;
        }

        // UI Saðlýk yüzdesini güncelle
        GuncelleUI();
    }

    public void HasarAl(float hasarMiktari)
    {
        mevcutCan -= hasarMiktari;
        mevcutCan = Mathf.Clamp(mevcutCan, 0, maksimumCan); // 0'ýn altýna inmesini önle
        Debug.Log($"Oyuncu hasar aldý: {hasarMiktari}. Kalan can: {mevcutCan}");

        // UI saðlýk çubuðunu güncelle
        GuncelleUI();

        if (mevcutCan <= 0)
        {
            Ol();
        }
    }

    public void Iyiles(float iyilesmeMiktari)
    {
        mevcutCan += iyilesmeMiktari;
        mevcutCan = Mathf.Clamp(mevcutCan, 0, maksimumCan); // Maksimum deðeri aþmasýný önle
        Debug.Log($"Oyuncu iyileþti: {iyilesmeMiktari}. Mevcut can: {mevcutCan}");

        // UI saðlýk çubuðunu güncelle
        GuncelleUI();
    }

    private void GuncelleUI()
    {
        if (healthBar != null)
        {
            healthBar.value = mevcutCan;
        }

        if (healthText != null)
        {
            float yuzde = (mevcutCan / maksimumCan) * 100f;
            healthText.text = $"Saðlýk: {yuzde:F0}%"; // Tam sayý olarak göster
        }
    }

    private void Ol()
    {
        Debug.Log("Oyuncu öldü!");
        Destroy(gameObject); // Oyuncuyu yok et
    }
}
