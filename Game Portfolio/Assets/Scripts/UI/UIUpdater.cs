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

    private void Start()
    {
        SyncSettings();
    }

    private void SyncSettings() => pickup.GetComponentInChildren<TMP_Text>().text = InputManager.Instance.Pickup.ToString();

    public void UpdateAmmoText(int amount) => ammo.text = "Ammo: " + amount;

    public void UpdateHealthText(int amount) => health.text = amount > 0 ? "Health " + amount: "Health 0";

    public void UpdatePickupGUI(bool state) => pickup.SetActive(state);

    public void ShowKills()
    {
        //TODO kill feed
    }
}
