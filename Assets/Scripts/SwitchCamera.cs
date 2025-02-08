using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchCamera : MonoBehaviour
{
    public CinemachineVirtualCamera VC1;

    public CinemachineVirtualCamera VC2;
    // Start is called before the first frame update
    void Start()
    {
        VC1.Priority = 1;
        VC2.Priority = 0;
        StartCoroutine(SwitchPerTime());


    }
    IEnumerator SwitchPerTime()
    {
        yield return new WaitForSeconds(3);
        VC1.Priority = 0;
        VC2.Priority = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
