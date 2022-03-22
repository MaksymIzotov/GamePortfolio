using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class WeaponAnimations : MonoBehaviour
{
    private Animator anim;

    private void Start()
    {
        AssignVariables();
    }

    public void Shoot()
    {
        anim.Play("shoot");
    }

    public void Reload()
    {
        anim.Play("reload");
    }

    private void AssignVariables(){
        anim = GetComponent<Animator>();

        if (anim == null)
            ErrorHandler.Instance.ComponentIsMissing("Animator", gameObject);
    }
}
