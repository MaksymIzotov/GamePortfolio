using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ErrorHandler : MonoBehaviour
{
    public void ComponentIsMissing(string _component, GameObject _gameObject) => Debug.LogErrorFormat("Missing {0} component on {1} gameobject", _component.ToString(), _gameObject.name);
}
