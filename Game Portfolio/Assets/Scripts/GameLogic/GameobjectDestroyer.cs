using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameobjectDestroyer : MonoBehaviour
{
    public static GameobjectDestroyer Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void DestroyGO(GameObject go)
    {
        Destroy(go);
    }

    public void DestroyGO(GameObject go, float delay)
    {
        Destroy(go, delay);
    }
}
