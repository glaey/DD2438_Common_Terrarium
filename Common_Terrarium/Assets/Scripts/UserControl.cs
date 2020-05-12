using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class UserControl : MonoBehaviour
{
    private Creature creature;

    // Start is called before the first frame update
    void Start()
    {
        creature = GetComponent<Creature>();
    }

    // Update is called once per frame
    void Update()
    {
        // pass the input to creature!
        float h = CrossPlatformInputManager.GetAxis("Horizontal");
        float v = CrossPlatformInputManager.GetAxis("Vertical");

        if (v != 0 || h != 0)
        {
            Vector3 dir = new Vector3(h, 0f, v);
            creature.Move(dir.normalized, 1f);
        }
            
    }
}
