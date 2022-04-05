using UnityEngine;
using TMPro;

public class SettingsButtonUpdater : MonoBehaviour
{
    public void UpdateButton(KeyCode key)
    {
        GetComponentInChildren<TMP_Text>().text = key.ToString();
    }
}
