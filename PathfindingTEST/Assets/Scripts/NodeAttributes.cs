using UnityEngine;
using System.Collections;

public class NodeAttributes : MonoBehaviour {
    private GameObject[] adjacent;

    public int x_coord;
    public int y_coord;
    public int path_cost = 1;
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
