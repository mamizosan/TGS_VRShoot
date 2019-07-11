using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightMove : EnemyMove {
	public Vector3 Move( float speed ) { 
		return new Vector3( 0, 0, -1 ) * speed;
	}
}
