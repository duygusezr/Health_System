using UnityEngine;
using UnityEngine.UI;
using TMPro; // TextMeshPro kullan�m� i�in gerekli

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float maksimumCan = 100f;
    private float mevcutCan;

    [Header("UI Ayarlar�")]
    [SerializeField] private Slider healthBar; // UI'daki sa�l�k bar�
    [SerializeField] private TextMeshProUGUI healthText; // UI'daki sa�l�k y�zdesi

    private void Start()
    {
        mevcutCan = maksimumCan;

        // UI Sa�l�k �ubu�unu ba�lang��ta tam dolu yap
        if (healthBar != null)
        {
            healthBar.maxValue = maksimumCan;
            healthBar.value = mevcutCan;
        }

        // UI Sa�l�k y�zdesini g�ncelle
        GuncelleUI();
    }

    public void HasarAl(float hasarMiktari)
    {
        mevcutCan -= hasarMiktari;
        mevcutCan = Mathf.Clamp(mevcutCan, 0, maksimumCan); // 0'�n alt�na inmesini �nle
        Debug.Log($"Oyuncu hasar ald�: {hasarMiktari}. Kalan can: {mevcutCan}");

        // UI sa�l�k �ubu�unu g�ncelle
        GuncelleUI();

        if (mevcutCan <= 0)
        {
            Ol();
        }
    }

    public void Iyiles(float iyilesmeMiktari)
    {
        mevcutCan += iyilesmeMiktari;
        mevcutCan = Mathf.Clamp(mevcutCan, 0, maksimumCan); // Maksimum de�eri a�mas�n� �nle
        Debug.Log($"Oyuncu iyile�ti: {iyilesmeMiktari}. Mevcut can: {mevcutCan}");

        // UI sa�l�k �ubu�unu g�ncelle
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
            healthText.text = $"Sa�l�k: {yuzde:F0}%"; // Tam say� olarak g�ster
        }
    }

    private void Ol()
    {
        Debug.Log("Oyuncu �ld�!");
        Destroy(gameObject); // Oyuncuyu yok et
    }
}
