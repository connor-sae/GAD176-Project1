using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    
    private static UIManager I;
    [SerializeField] private TMP_Text magazineAmmoUI;
    [SerializeField] private TMP_Text reserveAmmoUI;
    [SerializeField] private TMP_Text gunNameUI;
    [SerializeField] private Slider healthslider;

    void Awake()
    {
        if(I == null)
        {
            I = this;
        }else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// updates the ammo UI, remains unchanged if value is negative
    /// </summary>
    /// <param name="magazineAmmo"></param>
    /// <param name="reserveAmmo"></param>
    public static void UpdateAmmo(int magazineAmmo, int reserveAmmo = -1)
    {
        if(I == null)
            return;

        if(magazineAmmo >= 0)
            I.magazineAmmoUI.text = magazineAmmo.ToString();
        
        if(reserveAmmo >= 0)
            I.reserveAmmoUI.text = reserveAmmo.ToString();
    }

    public static void UpdateGunName(string gunName)
    {
        if (I == null)
            return;

        I.gunNameUI.text = gunName;
    }

    public static void UpdateHealth(int currentHealth, int maxHealth)
    {
        if (I == null)
            return;

        I.healthslider.value = (float)currentHealth / maxHealth;
    }
}
