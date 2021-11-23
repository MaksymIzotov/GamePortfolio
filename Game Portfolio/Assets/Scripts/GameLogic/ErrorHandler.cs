using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ErrorHandler : MonoBehaviour
{
    public static ErrorHandler Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void ComponentIsMissing(string _component, GameObject _gameObject) => Debug.LogErrorFormat("Missing {0} component on {1} gameobject", _component.ToString(), _gameObject.name);

    public void GameObjectIsMissing(string _gameObject) => Debug.LogErrorFormat("Missing {0} gameobject", _gameObject);
}
