using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testPathFind : MonoBehaviour {

    Seeker seeker;
    [SerializeField]
    public Transform dest;
	// Use this for initialization
	void Start () {
        seeker = GetComponent<Seeker>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        seeker.StartPath(this.transform.position, dest.position);
	}
}
