using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(WeaponInfo))]
public class WeaponStateController : MonoBehaviour
{
    WeaponInfo info;
    private void Start()
    {
        info = GetComponent<WeaponInfo>();

        info.weaponState = WeaponInfo.State.PICKUP;
    }

    public void SetState(WeaponInfo.State _state)
    {
        info.weaponState = _state;
    }
}
