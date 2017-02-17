using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System.Linq;
using System;

public class path_finder : MonoBehaviour {

    private bool pressed;
    public TileManagerScript tm;

	void Start () {
		
	}

	void Update () {
		// this was suppose to return the clicked gameobject but I couldnt get it to work
		// here is nothing useful
        
		if (Input.GetMouseButtonDown (0)) {
			// right now A_star takes in ints, but I can change it to GameObjects if you want
			// yet again, nothing useful here. This was just for testing
			List<GameObject> temp = A_star (0, 0, 3, 3);
			foreach(GameObject temp2 in temp){
				Debug.Log(temp2);
			}

		}

    }

	//why is this up here?
	List<GameObject> get_adj (GameObject node){
		//im lazy
		//fix it if you want
		//Debug.Log ("in get_adj");
		List<GameObject> neighbors = new List<GameObject>();
		int x = node.GetComponent<NodeAttributes>().x_coord;
		int y = node.GetComponent<NodeAttributes>().y_coord;
		//only 4 directional movement 
		//blame Thomas
		GameObject node_a = tm.GetTile(x,y+1);
		neighbors.Add(node_a);
		GameObject node_b = tm.GetTile(x+1,y);
		neighbors.Add(node_b);
		GameObject node_c = tm.GetTile(x-1,y);
		neighbors.Add(node_c);
		GameObject node_d = tm.GetTile(x,y-1);
		neighbors.Add(node_d);
		if (node_a == null) {
			Destroy (node_a);
		}
		if (node_b == null) {
			Destroy (node_b);
		}
		if (node_c == null) {
			Destroy (node_c);
		}
		if (node_d == null) {
			Destroy (node_d);
		}

		return neighbors;
	}

	//put in the x,y coord of the 2 places you want and *poof*, you got yourself a list of directions (as gameobjects)
	//use a foreach loop to go through it, it should come out in order
	//maybe i should have it take in gameobjects
	//nah, I got midterms to study for
    List<GameObject> A_star(int start_x, int start_y, int end_x, int end_y) {
		//Debug.Log ("in a_star");
		Dictionary<GameObject,int> queue = new Dictionary<GameObject,int>();
		Dictionary<GameObject,int> distances = new Dictionary<GameObject,int>();
        Dictionary<GameObject,GameObject> backpointer = new Dictionary<GameObject,GameObject>();
        List<GameObject> final_path = new List<GameObject>();
		GameObject initial = tm.GetTile(start_x, start_y);
		GameObject goal = tm.GetTile(end_x, end_y);

        int pathcost = 0;
        queue.Add(initial,0);
        distances.Add(initial,0);
		backpointer.Add (initial, null);

		// voila, the most inefficient way to do A*
        while (queue.Count != 0) {
       
			List<int> c_distance = new List<int>(queue.Values);
			int current_distance = c_distance.Min();
			//make walls.path_cost == 1000
			if (current_distance > 1000){
				break;
			}
			GameObject current_node = queue.FirstOrDefault(x => x.Value == current_distance).Key;
			queue.Remove (current_node);
			if (current_node.GetInstanceID() == goal.GetInstanceID()){
                //returns the path
                final_path.Add(current_node);
                GameObject current_back_node = backpointer[current_node];

				while (current_back_node != null) {
                    final_path.Add(current_back_node);
                    current_back_node = backpointer[current_back_node];
                }

				final_path.Reverse ();
				return final_path;
            }
				
            foreach (GameObject adj in get_adj(current_node)){
				if (adj == null){
					Destroy (adj);
					continue;
				}
					
				pathcost = adj.GetComponent<NodeAttributes> ().path_cost; 
				pathcost += current_distance;

				if (!(find_in(distances,adj))){ 
                    distances.Add(adj,pathcost);
                    backpointer.Add(adj,current_node);
                    queue.Add(adj,pathcost);
                }

				else {
					//redundant but it didnt work for me without this here
					if (find_in(distances,adj)){
						if (pathcost < distances [adj]) {
							distances [adj] = pathcost;
							backpointer.Add (adj, current_node);
							int tile_priority = pathcost + heuristic (goal, adj );
							queue.Add (adj,tile_priority); 

						}
					}
                }

            }
        }
		//note to self: test this part
		return null;

    }

	//im pretty sure there is a better way to do this
	bool find_in(Dictionary<GameObject,int> k, GameObject v){
		List<GameObject> keys = new List<GameObject>(k.Keys);
		foreach(GameObject item in keys){
			if (item.GetInstanceID() == v.GetInstanceID()){
				return true;
			}
		}
		return false;

	}

	//might need to change all of path_cost to doubles for it to be more accurate
	int heuristic(GameObject a, GameObject b){
		double x1 = Convert.ToDouble(a.GetComponent<NodeAttributes> ().x_coord);
		double x2 = Convert.ToDouble(b.GetComponent<NodeAttributes> ().x_coord);
		double y1 = Convert.ToDouble(a.GetComponent<NodeAttributes> ().y_coord);
		double y2 = Convert.ToDouble(b.GetComponent<NodeAttributes> ().y_coord);
		int est_cost = Convert.ToInt32 (Math.Sqrt(((x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2))));
		return est_cost;
	}





}
