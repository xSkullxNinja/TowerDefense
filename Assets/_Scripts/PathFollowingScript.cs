using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFollowingScript : MonoBehaviour {

    public GameObject Path;
    public int currentPosition = 0;

	// Use this for initialization
	void Start () {
        this.gameObject.transform.position = Path.transform.GetChild(currentPosition).transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        if(currentPosition == Path.transform.childCount - 1)
        {
            Destroy(gameObject);
        }
        else
        {
            Vector3 direction = Path.transform.GetChild(currentPosition + 1).transform.position - this.gameObject.transform.position;
            Vector3 pathDestination = Path.transform.GetChild(currentPosition + 1).transform.position - Path.transform.GetChild(currentPosition).transform.position;
            Vector3 previousDestination = Path.transform.GetChild(currentPosition).transform.position - this.gameObject.transform.position;
            float currentProgress = previousDestination.magnitude - pathDestination.magnitude;

            this.gameObject.transform.position += direction.normalized * GetComponent<UnitStats>().Speed * Time.deltaTime;

            if(currentProgress > 0)
            {
                currentPosition += 1;
            }
        }
    }   
}
