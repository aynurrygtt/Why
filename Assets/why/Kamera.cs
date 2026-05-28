using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KameraHareket : MonoBehaviour
{

    [SerializeField] private Transform karakter;
    void Update()
    {
        transform.position = new Vector3(karakter.position.x, karakter.position.y, -10);
    }
}
