using UnityEngine;
using System.Collections;

public class Node_Attributes : MonoBehaviour {
    private GameObject[] adjacent;

    public float x_coord;
    public float y_coord;
    public float height; //how should this happen?

    public nodeType _type;
}

public enum nodeType
{
    _tile,
    _wire,
    _switch,
    _source,
    _goal
}
