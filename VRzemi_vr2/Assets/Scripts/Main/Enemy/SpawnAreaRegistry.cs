using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class SpawnAreaRegistry : MonoBehaviour {
	[ SerializeField ] private GameObject[ ] _spawn_area = new GameObject[ 1 ];
	
	
	public enum AREA { 
		AREA_1,
		AREA_2,
	};


	public GameObject getSpawnArea( AREA area ) {
		if ( ( int )area >= _spawn_area.Length ) { 
			Assert.IsNotNull( null, "[SpawnAreaRegistry]取得しようとしているAreaが配列の最大値を超えています。" );
			return null; 
		}
		return _spawn_area[ ( int )area ];
	}

}
