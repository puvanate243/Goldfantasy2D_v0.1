using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowPoint : MonoBehaviour
{
    public Transform arrow;
   
    void Update()
    {
        transform.position = arrow.position;
    }
}
