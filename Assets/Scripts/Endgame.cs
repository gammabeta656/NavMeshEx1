using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Endgame : MonoBehaviour
{

    void OnTriggerEnter(Collider other)
    {
        Application.Quit();
    }
}
