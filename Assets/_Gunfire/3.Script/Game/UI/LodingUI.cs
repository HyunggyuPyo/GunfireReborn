using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LodingUI : MonoBehaviour
{
    public GameObject circle;

    private void Update()
    {
        circle.transform.Rotate(0, 0, 25f * Time.deltaTime);
    }
}
