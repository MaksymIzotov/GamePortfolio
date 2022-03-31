using UnityEngine;
using TMPro;

public class UIUpdater : MonoBehaviour
{
    public static UIUpdater Instance;
    private void Awake() => Instance = this;

    //Variables Declaration
    public TMP_Text ammo;
    public TMP_Text health;
    public GameObject pickup;

    public void UpdateAmmoText(int amount) => ammo.text = "Ammo: " + amount;

    public void UpdateHealthText(int amount) => health.text = "Health " + amount;

    public void UpdatePickupGUI(bool state) => pickup.SetActive(state);
}
