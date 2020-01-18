using System;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;

public class Path : MonoBehaviour {

    private Vector3[] path = {
        new Vector3(0, 0, 0), 
        new Vector3(0, 0, 10), 
        new Vector3(1.5f, 0, 2.5f), 
        new Vector3(2.5f, 0, 1.5f), 
        new Vector3(12, 0, 0),
        new Vector3(2.5f, 0, 1.5f),
        new Vector3(1.5f, 0, 2.5f),
        new Vector3(0, 0, 12),
        new Vector3(-1.5f, 0, 2.5f),
        new Vector3(-2.5f, 0, 1.5f),
        new Vector3(-12, 0, 0),
        new Vector3(-2.5f, 0, -1.5f),
        new Vector3(-1.5f, 0, -2.5f),
        new Vector3(0, 0, -3), 
        new Vector3(-1.5f, 0, -2.5f),
        new Vector3(-2.5f, 0, -1.5f),
        new Vector3(-22, 0, 0),
        new Vector3(-2.5f, 0, -1.5f),
        new Vector3(-1.5f, 0, -2.5f),
        new Vector3(0, 0, -2),
        new Vector3(-1.5f, 0, -2.5f),
        new Vector3(-2.5f, 0, -1.5f),
        new Vector3(-12, 0, 0),
        new Vector3(-2.5f, 0, 1.5f),
        new Vector3(-1.5f, 0, 2.5f),
        new Vector3(0, 0, 13),
        new Vector3(1.5f, 0, 2.5f), 
        new Vector3(2.5f, 0, 1.5f), 
        new Vector3(12, 0, 0),
        new Vector3(2.5f, 0, 1.5f), 
        new Vector3(1.5f, 0, 2.5f), 
        new Vector3(0, 0, 10)
    };

    private int index = 0;

    private Boolean finish = false;
    
    // Start is called before the first frame update
    void Start() {
        Debug.Log(path.Length);
    }

    public Vector3 advance(Vector3 position, float speed, float offset) {

        if (finish) {
            return position;
        }
        
        var point1 = GetPoint(index);
        var point2 = GetPoint(index + 1);
        
        
        var right = Quaternion.AngleAxis(90, Vector3.up) * (point2-point1).normalized;

        var target = point2 + right * offset * 0.4f;
        
        if ((position - target).magnitude < 0.05f) {
            index++;
            if (index + 1 >= path.Length) {
                finish = true;
            }
            Debug.Log("Path: " + point2);
        }

        return Vector3.MoveTowards(position, target, speed);

    }
    
    public Quaternion getDirection() {
        var point1 = GetPoint(index);
        var point2 = GetPoint(index + 1);

        return Quaternion.LookRotation(point2 - point1);

    }

    public Vector3 getStart(float offset) {

        return path[0] + Vector3.right * offset * 0.4f;
    }
    
    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        for (int i = 1; i < path.Length; i++) {
            Gizmos.DrawLine(GetPoint(i-1), GetPoint(i));
        }
    }

    private Vector3 GetPoint(int index) {
        Vector3 point = Vector3.zero;
        for (int i = 1; i < path.Length && i <= index; i++) {
            point += path[i];
        }

        return point;
    }

    public void reset() {
        index = 0;
    }

    public Boolean isFinish() {
        return finish;
    }
}
