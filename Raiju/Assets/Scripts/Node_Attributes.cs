using UnityEngine;
using System.Collections;

public class Node_Attributes : MonoBehaviour {
    public bool current = false;
    private GameObject[] adjacent;

    public float x_coord;
    public float y_coord;
    public float height; //how should this happen?

    public enum nodeType
    {
        _tile,
        _wire,
        _switch,
        _source,
        _goal
    }

    public nodeType _type;
}
