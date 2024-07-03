using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ES_PursueInsidePath : ES_Pursue
{
    [SerializeField] private Transform[] waypoints;

    //El punto más a la izquierda y el más a la derecha
    private float maxXpoint, minXpoint;

    // Start is called before the first frame update
    void Start()
    {
        //Calcular los puntos tope
        if (waypoints.Length == 0) return;

        maxXpoint = waypoints[0].position.x;
        minXpoint = waypoints[0].position.x;
        foreach(Transform point in waypoints)
        {
            if (point.position.x > maxXpoint) maxXpoint = point.position.x;
            if (point.position.x < minXpoint) minXpoint = point.position.x;
        }

    }

    // Update is called once per frame
    public override void OnUpdateState()
    {
        base.OnUpdateState();
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, minXpoint, maxXpoint), 
            transform.position.y, transform.position.z);
    }
}
