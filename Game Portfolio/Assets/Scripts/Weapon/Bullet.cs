using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void Start()
    {
        Destroy(gameObject, 5);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);

        CreateHitPoint();

        Destroy(gameObject);
    }

    void CreateHitPoint()
    {
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.position = transform.position;
        cube.transform.localScale = transform.localScale;
        cube.GetComponent<BoxCollider>().enabled = false;
        Destroy(cube, 5);
    }

    private void Update()
    {
        //Debug purpose only

        //GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        //sphere.transform.position = transform.position;
        //sphere.transform.localScale = transform.localScale;
        //sphere.GetComponent<SphereCollider>().enabled = false;
    }
}
