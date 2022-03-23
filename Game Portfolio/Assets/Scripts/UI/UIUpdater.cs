using UnityEngine;
using TMPro;

public class UIUpdater : MonoBehaviour
{
    public static UIUpdater Instance;
    private void Awake() => Instance = this;

    //Variables Declaration
    public TMP_Text ammo;
    public GameObject pickup;

    public void UpdateAmmoText(int amount) => ammo.text = "Ammo: " + amount;

    public void UpdatePickupGUI(bool state) => pickup.SetActive(state);
}
