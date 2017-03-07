using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        float xPos = this.gameObject.transform.position.x;
        float yPos = this.gameObject.transform.position.y;
        float zPos = this.gameObject.transform.position.z;

        if (Input.GetKeyDown(KeyCode.W))
        {
            zPos += 1;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            zPos -= 1;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            xPos -= 1;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            xPos += 1;
        }
        this.gameObject.transform.position = new Vector3(xPos, yPos, zPos);
    }
}
