using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicCamera : MonoBehaviour
{

    private Camera camera;
    private const float CAMERA_SPEED = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        camera = GetComponent<Camera>();

        GameObject[] creatures = GameObject.FindGameObjectsWithTag("creature");

        Vector3 gravityCenter = Vector3.zero;
        float xMin = transform.position.x, zMin = transform.position.z;
        float xMax = xMin, zMax = zMin;
        foreach(var creature in creatures)
        {
            gravityCenter += creature.transform.position;
            xMin = Mathf.Min(xMin, creature.transform.position.x);
            xMax = Mathf.Max(xMax, creature.transform.position.x);
            zMin = Mathf.Min(zMin, creature.transform.position.z);
            zMax = Mathf.Max(zMax, creature.transform.position.z);
        }
        gravityCenter /= creatures.Length;
        transform.position = new Vector3(gravityCenter.x, transform.position.y, gravityCenter.z);
        //camera.fieldOfView = 180 * Mathf.Atan(Mathf.Max(zMax - zMin, xMax - xMin)/transform.position.y) / Mathf.PI;
    }

    // Update is called once per frame
    void Update()
    {
        GameObject[] creatures = GameObject.FindGameObjectsWithTag("creature");

        Vector3 gravityCenter = Vector3.zero;
        float xMin = transform.position.x, zMin = transform.position.z;
        float xMax = xMin, zMax = zMin;
        foreach (var creature in creatures)
        {
            gravityCenter += creature.transform.position;
            xMin = Mathf.Min(xMin, creature.transform.position.x);
            xMax = Mathf.Max(xMax, creature.transform.position.x);
            zMin = Mathf.Min(zMin, creature.transform.position.z);
            zMax = Mathf.Max(zMax, creature.transform.position.z);
        }
        gravityCenter /= creatures.Length;
        float deltaX = Mathf.Lerp(transform.position.x, gravityCenter.x, CAMERA_SPEED * Time.deltaTime);
        float deltaZ = Mathf.Lerp(transform.position.z, gravityCenter.z, CAMERA_SPEED * Time.deltaTime);
        transform.position = new Vector3(deltaX, transform.position.y, deltaZ);
        //camera.fieldOfView = 180 * Mathf.Atan(Mathf.Max(zMax - zMin, xMax - xMin) / transform.position.y) / Mathf.PI;

    }
}
