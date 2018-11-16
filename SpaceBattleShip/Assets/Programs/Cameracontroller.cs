using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cameracontroller : MonoBehaviour {
    public GameObject Target1;
    public GameObject Target2;
    Vector3 CenterPos;
    public float OffcetY = 4;
    public float ratio;
        

    // Use this for initialization
    void Start()
    {

    }

	
	// Update is called once per frame
	void Update ()
    {
        CenterPos = (Target1.transform.position + Target2.transform.position) / 2;
        ratio = Vector3.Distance(Target1.transform.position, Target2.transform.position) / 10;
        transform.position = new Vector3(CenterPos.x, CenterPos.y + OffcetY, - ratio * ratio);

	}
}
