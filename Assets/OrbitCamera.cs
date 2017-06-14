using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitCamera : MonoBehaviour {
	// Update is called once per frame
	void Update () {
        transform.position = new Vector3(Mathf.Sin(Time.timeSinceLevelLoad), 0.25f, Mathf.Cos(Time.timeSinceLevelLoad)) * 30;
        transform.rotation = Quaternion.LookRotation(-transform.position, Vector3.up);
	}
}
