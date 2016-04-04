using UnityEngine;
using System.Collections.Generic;

public class WallBuilder : MonoBehaviour {
	public class Cell 
	{
		/*public int bullets;
        public int grenades;
        public int rockets;*/
		public float dot_x;      // position
		public float dot_y;
		public int dot_number;
		public bool used;
		/*public bool lookingLeft;
        public bool lookingUp;
        public bool lookingDown;
        public bool lookingRight;
        public bool wall;*/
		public bool leftWallTrue;
		public bool rightWallTrue;
		public bool topWallTrue;
		public bool bottomWallTrue;
		public bool odd;
		public int grid_x;
		public int grid_y;
		public Cell()
		{
			odd = false;
			dot_x = 100;
			dot_y = 100;
			used = false;
			dot_number = 0;
			leftWallTrue = true;
			rightWallTrue = true;
			topWallTrue = true;
			bottomWallTrue = true;
			grid_x = 0;
			grid_y = 0;
		}
		public void build (){
			if (topWallTrue == true) {
				GameObject wallUp = GameObject.CreatePrimitive (PrimitiveType.Cube);
				wallUp.transform.position = new Vector3 (dot_x, 2, dot_y + 2);
				wallUp.transform.localScale = new Vector3 (4.5f, 4, 1);
			}
			if (bottomWallTrue == true) {
				GameObject wallDown = GameObject.CreatePrimitive (PrimitiveType.Cube);
				wallDown.transform.position = new Vector3 (dot_x, 2, dot_y - 2);
				wallDown.transform.localScale = new Vector3 (4.5f, 4, 1);
			}
			if (rightWallTrue == true) {
				GameObject wallRight = GameObject.CreatePrimitive (PrimitiveType.Cube);
				wallRight.transform.position = new Vector3 (dot_x + 2f, 2, dot_y);
				wallRight.transform.localScale = new Vector3 (1, 4, 4.5f);
			}
			if (leftWallTrue == true) {
				GameObject wallLeft = GameObject.CreatePrimitive (PrimitiveType.Cube);
				wallLeft.transform.position = new Vector3 (dot_x - 2f, 2, dot_y);
				wallLeft.transform.localScale = new Vector3 (1, 4, 4.5f);
			}
			//x = width, y = height x = length*/

			//Debug.Log("just made a cube");
		}
	}
	public int hardwire;
	public Cell[,] grid = new Cell[24,24];
	public int i = 0;
	public int j = 0;

	public List<Cell> usable_positions = new List<Cell>();
	public int counter = 0;
	// Use this for initialization
	void Start () {
		for(int i = 0;i < hardwire;i++){
			for(int j = 0;j < hardwire;j++){
				grid[i,j] = new Cell();
				grid[i,j].dot_x = 4*i;
				grid[i,j].dot_y = 4*j;
				grid[i,j].dot_number = counter;
				//grid [i, j].build ();
				grid[i,j].grid_x = i;
				//Debug.Log("hey "+(grid[i,j].grid_x).ToString ());
				grid[i,j].grid_y = j;
				counter++;
			}
		}
		grid [hardwire - 1, hardwire - 1].rightWallTrue = false;
		Recursion (0, 0, 2, 0);
		for(int i = 0;i < hardwire;i++){
			for(int j = 0;j < hardwire;j++){
				grid [i, j].build ();
			}
		}
		Debug.Log ("hey its all good");
		//grid [0, 0].rightWallTrue = false;
		//grid [0, 0].build ();
		//grid [0, 1].build ();
		//grid [1, 0].build ();
		//Debug.Log (grid [3, 4].dot_x);
		//Toggle (0,0,2);
		//goBuild();
	}
	void Recursion(int start_x, int start_y, int from, int num){
		//Debug.Log ("start x is " + start_x);
		//Debug.Log ("start y is " + start_y);
		//Debug.Log ("from is " + from);
		//Debug.Log ("we have been here " + num);
		/*if (num == 10) {
            return;
        }*/
		int last_usable_square = 0;
		bool left = false;
		bool right = false;
		bool up = false;
		bool down = false;
		int random = 0;
		int last_x = 0;
		int last_y = 0;
		if (from == 0) {
			grid [start_x, start_y].rightWallTrue = false;
			right = true;
			//we got here by stepping left
		}
		else if (from == 1) {
			grid [start_x, start_y].bottomWallTrue = false;
			down = true;
			// we got here by stepping up
		}
		else if (from == 2) {
			grid [start_x, start_y].leftWallTrue = false;
			left = true;
			// we got here by stepping right
		}
		else if (from == 3) {
			grid [start_x, start_y].topWallTrue = false;
			up = true;
			// we got here by stepping down
		}
		if (start_x == 0) {//if we are at the left edge
			left = true;
		} 
		else if (start_x == (hardwire - 1)) {//if we are at the right edge
			right = true;
		}
		if (start_y == 0) {//if we are at the bottom edge
			down = true;
		} 
		else if (start_y == (hardwire - 1)) {//if we are at the top edge
			up = true;
		}
		grid [start_x, start_y].used = true;
		while ((right == false) || (left == false) || (up == false) || (down == false)) {
			random = Random.Range (0, 4);
			//Debug.Log ("Random is " + random);
			if ((random == 0) && (left == false)) {
				if (grid [start_x - 1, start_y].used == false) {
					grid [start_x, start_y].leftWallTrue = false;
					usable_positions.Add (grid [start_x, start_y]);
					left = true;
					Recursion (start_x - 1, start_y, 0, num + 1);
					break;
				} else {
					left = true;
				}

			} else if ((random == 1) && (up == false)) {
				if (grid [start_x, start_y + 1].used == false) {
					grid [start_x, start_y].topWallTrue = false;
					usable_positions.Add (grid [start_x, start_y]);
					up = true;
					Recursion (start_x, start_y + 1, 1, num + 1);
					break;
				} else {
					up = true;
				}

			} else if ((random == 2) && (right == false)) {
				if (grid [start_x + 1, start_y].used == false) {
					grid [start_x, start_y].rightWallTrue = false;
					usable_positions.Add (grid [start_x, start_y]);
					right = true;
					Recursion (start_x + 1, start_y, 2, num + 1);
					break;
				} else {
					right = true;
				}

			} else if ((random == 3) && (down == false)) {
				if (grid [start_x, start_y - 1].used == false) {
					grid [start_x, start_y].bottomWallTrue = false;
					usable_positions.Add (grid [start_x, start_y]);
					down = true;
					Recursion (start_x, start_y - 1, 3, num + 1);
					break;
				} else {
					down = true;
				}

			}
		}
		if (usable_positions.Count > 1) {
			usable_positions.RemoveAt (usable_positions.Count - 1);
			last_usable_square = usable_positions.Count - 1;
			last_x = usable_positions [last_usable_square].grid_x;
			last_y = usable_positions [last_usable_square].grid_y;
			if (last_x > start_x) {
				Recursion (last_x, last_y, 2, num + 1);
			} else if (last_x < start_x) {
				Recursion (last_x, last_y, 0, num + 1);
			} else if (last_y > start_y) {
				Recursion (last_x, last_y, 1, num + 1);
			} else if (last_y < start_y) {
				Recursion (last_x, last_y, 3, num + 1);
			}
		} 
		else {
			return;
		}
	}

	/*
    void goBuild(){
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.position = new Vector3(0, 0.5F, 0);
    }*/
	/*void OnDrawGizmos() {
	Vector3 pos = new Vector3 (0, 0, 0);
	for (int x = 0; x < hardwire; x ++) {
		for (int y = 0; y < hardwire; y ++) {
			Gizmos.color = Color.yellow;//set color to yellow
			//Debug.Log(grid[x,y].dot_x);
			Debug.Log("aaay" + counter);
			pos = new Vector3(0,1/2,0); // set position of wall
			Gizmos.DrawCube(pos,Vector3.one);
		}
	}
}*/
// Update is called once per frame
void Update () {

}
}  