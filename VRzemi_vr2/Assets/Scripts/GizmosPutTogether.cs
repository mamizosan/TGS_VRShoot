using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class GizmosPutTogether : MonoBehaviour {
	[ SerializeField ] private Player _player = null;
	[ SerializeField ] private GameObject[ ] _spawn_areas = new GameObject[ 1 ];
	[ SerializeField ] private GameObject _enemy_destory_area = null;

	private void OnDrawGizmos( ) {
		CheckReference( );

		PlayerGizmos( );
		SpawnAreaGizmos( );
		EnemyDestoryAreaGizmos( );
	}

	private void PlayerGizmos( ) { 
		Gizmos.color = Color.black;
		Gizmos.DrawWireCube( _player.transform.position, new Vector3( _player.getMoveLimit( ) * 2, 1, 2 ) );

		Gizmos.color = new Color( 0, 1, 0, 0.5f );	//半透明の緑
		Gizmos.DrawCube( _player.transform.position, new Vector3( _player.getMoveLimit( ) * 2, 1, 2 ) );
	}

	private void SpawnAreaGizmos( ) {
		Color wire_cube_color = Color.black;
		Color cube_color = new Color( 1, 0, 0, 0.5f );	//半透明の赤

		for( int i = 0; i < _spawn_areas.Length; i++ ) {
			Gizmos.color = wire_cube_color;
			Gizmos.DrawWireCube( _spawn_areas[ i ].transform.position, _spawn_areas[ i ].transform.localScale );

			Gizmos.color = cube_color;
			Gizmos.DrawCube( _spawn_areas[ i ].transform.position, _spawn_areas[ i ].transform.localScale );
		}
	}

	private void EnemyDestoryAreaGizmos( ) { 
		Gizmos.color = Color.black;
		Gizmos.DrawWireCube( _enemy_destory_area.transform.position, _enemy_destory_area.transform.localScale );

		Gizmos.color = new Color( 0, 0, 1, 0.5f );	//半透明の青
		Gizmos.DrawCube( _enemy_destory_area.transform.position, _enemy_destory_area.transform.localScale );
	}

	private void CheckReference( ) { 
		Assert.IsNotNull( _player, "[GizmosPutTogether]Playerの参照がありません" );
		for ( int i = 0; i < _spawn_areas.Length; i ++ ) {
			Assert.IsNotNull( _spawn_areas[ i ], "[GizmosPutTogether]SpawnArea" + i + "の参照がありません" );
		}
		Assert.IsNotNull( _enemy_destory_area, "[GizmosPutTogether]EnemyDestoryAreaの参照がありません" );
	}
}
