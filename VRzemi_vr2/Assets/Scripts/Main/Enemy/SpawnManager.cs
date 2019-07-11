using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Assertions;

public class SpawnManager : MonoBehaviour {
	private readonly int SPAWN_ORDER_IDX = 0;

	[ System.Serializable ]
	private struct Spawn {
		public Enemy.ENEMY_TYPE type;
		public SpawnAreaRegistry.AREA area;
		public float spawn_time;
		public float move_speed;
		public int hit_point;
	};

	[ SerializeField ] private List< Spawn > _spawn = new List< Spawn >( );
	[ SerializeField ] private SpawnAreaRegistry _spawn_area_registry = null;
	[ SerializeField ] private GameObject _enemy = null;
	private float _spawn_count = 0;


	//Editor拡張用------------------------------------------------------------------------------------------
	//[ SerializeField ]が無いとEditor拡張して表示している値がUnityを再生したときにリセットされてしまう
	//[ SerializeField ] private Object _enemy = null;
	//[ SerializeField ] private int _editor_max_index = 0;
	//[ SerializeField ] private List< Enemy.ENEMY_TYPE > _editor_type = new List< Enemy.ENEMY_TYPE >( );
	//[ SerializeField ] private List< float > _editor_spawn_time = new List< float >( );
	//[ SerializeField ] private List< float > _editor_move_speed = new List< float >( );
	//[ SerializeField ] private List< int > _editor_hit_point = new List< int >( ); 
	//[ SerializeField ] private List< bool > _editor_foldout = new List< bool >( );
	//------------------------------------------------------------------------------------------------------

	private void Awake( ) {
		//Editor拡張で使用した値を構造体のリストに登録していく---
		//for ( int i = 0; i < _editor_max_index; i++ ) { 
		//	Spawn spawn = new Spawn { 
		//				  type = _editor_type[ i ],
		//				  spawn_time = _editor_spawn_time[ i ],
		//				  move_speed = _editor_move_speed[ i ],
		//				  hit_point = _editor_hit_point[ i ]
		//	};
		//	_spawn.Add( spawn );
		//}
		//--------------------------------------------------------
	}

	private void Start( ) {
		CheckReference( );

		_spawn_count = _spawn[ SPAWN_ORDER_IDX ].spawn_time;
	}

	private void FixedUpdate( ) {
		SpawnCount( );
	}

	private void Update( ) {
		if ( !IsSpawnNext( ) ) return;

		SpawnEnemy( );
	}

	//常に先頭にある要素で生成する
	private void SpawnEnemy( ) {
		//時間になったら生成
		if ( IsSpawn( ) ) {
			GameObject spawn_area = _spawn_area_registry.getSpawnArea( _spawn[ SPAWN_ORDER_IDX ].area );
			GameObject enemy_obj = Instantiate( ( GameObject )_enemy, spawn_area.transform.position, Quaternion.AngleAxis( 180f, new Vector3( 0, 1, 0 ) ) );
			Enemy enemy = enemy_obj.GetComponent< Enemy >( );
			enemy.Initialize( _spawn[ SPAWN_ORDER_IDX ].type, _spawn[ SPAWN_ORDER_IDX ].move_speed, _spawn[ SPAWN_ORDER_IDX ].hit_point );
			_spawn.Remove( _spawn[ SPAWN_ORDER_IDX ] );

			//次があったら次の時間を入れる
			if ( IsSpawnNext( ) ) {
				_spawn_count = _spawn[ SPAWN_ORDER_IDX ].spawn_time;
			} else { 
				Debug.Log( "[SpawnManager]全て生成終了" );
			}
		}
	}

	private void SpawnCount( ) { 
		_spawn_count -= Time.deltaTime;	
	}

	private bool IsSpawn( ) { 
		return _spawn_count <= 0;
	}

	private bool IsSpawnNext( ) { 
		return	_spawn.Count != 0;
	}

	private void CheckReference( ) { 
		Assert.IsNotNull( _spawn_area_registry, "[SpawnManger]SpawnAreaRegistryの参照がありません" );
		Assert.IsNotNull( _enemy, "[SpawnManger]PrefubのEnemyの参照がありません" );
		if ( _spawn.Count == 0 ) { 
			Assert.IsNotNull( null, "[SpawnManger]敵の要素がありません" );
		}
	}

	//public void SpawnUpdate( ) { 
	//	if ( !IsSpawnNext( ) ) return;
	//
	//	SpawnCount( );
	//	SpawnEnemy( );
	//}


	////Editor拡張クラス-----------------------------------------------------------------------------------------------------------------------------------------------------
	//[ CustomEditor( typeof( SpawnManager ) ) ]
	//public class EditorExpansion : Editor {
	//	private SpawnManager _target;
	//	
	//
	//	private void Awake( ) {
	//		_target = target as SpawnManager;
	//		//ここで変数をいれて初期化するとインスペクターで選択時に毎回呼ばれる
	//	}
	//
	//	public override void OnInspectorGUI( ) {
	//		
	//		ButtonLayout( );
	//		SpawnStatusLayout( );
	//
	//		EditorGUILayout.Space( );
	//		_target._enemy = EditorGUILayout.ObjectField( "敵のプレハブ",_target._enemy, typeof( Object ), true );
	//	}
	//
	//	private void ButtonLayout( ) { 
	//		if ( GUILayout.Button( "追加" ) ) { 
	//			_target._editor_max_index++;
	//			AddList( );
	//		}
	//
	//		if ( GUILayout.Button( "削除" ) ) { 
	//			_target._editor_max_index--;
	//			if ( _target._editor_max_index < 0 ) { 
	//				_target._editor_max_index = 0;
	//			}
	//			RemoveList( );
	//		}
	//	}
	//
	//	private void SpawnStatusLayout( ) { 
	//		for ( int i = 0; i < _target._editor_max_index; i++ ) {
	//			EditorGUILayout.LabelField( "---------------------------------------------------------" );
	//			if ( _target._editor_foldout[ i ]   = EditorGUILayout.Foldout( _target._editor_foldout[ i ], "敵" + i.ToString( ) ) ) {
	//				_target._editor_type[ i ]       = ( Enemy.ENEMY_TYPE )EditorGUILayout.EnumPopup( "敵のタイプ" + i.ToString( ), _target._editor_type[ i ] );
	//				_target._editor_spawn_time[ i ] = EditorGUILayout.FloatField( "出現時間" + i.ToString( ), _target._editor_spawn_time[ i ] );
	//				_target._editor_move_speed[ i ] = EditorGUILayout.FloatField( "移動スピード" + i.ToString( ), _target._editor_move_speed[ i ] );
	//				_target._editor_hit_point[ i ]  = EditorGUILayout.IntField( "体力" + i.ToString( ), _target._editor_hit_point[ i ] );
	//			}
	//			EditorGUILayout.LabelField( "---------------------------------------------------------" );
	//			EditorGUILayout.Space( );
	//			EditorGUILayout.Space( );
	//		}
	//	}
	//
	//	private void AddList( ) {
	//		_target._editor_type.Add( Enemy.ENEMY_TYPE.TYPE_A );      
	//		_target._editor_spawn_time.Add( 0 );
	//		_target._editor_move_speed.Add( 0 );
	//		_target._editor_hit_point.Add( 0 );
	//		_target._editor_foldout.Add( false );
	//		_target._editor_foldout.Add( false );
	//	}
	//
	//	private void RemoveList( ) { 
	//		int remove_index = _target._editor_max_index;
	//		if ( remove_index < 0 ) return;
	//
	//		_target._editor_type.Remove      ( _target._editor_type      [ remove_index ] );     
	//		_target._editor_spawn_time.Remove( _target._editor_spawn_time[ remove_index ] );
	//		_target._editor_move_speed.Remove( _target._editor_move_speed[ remove_index ] );
	//		_target._editor_hit_point.Remove ( _target._editor_hit_point [ remove_index ] );
	//		_target._editor_foldout.Remove   ( _target._editor_foldout   [ remove_index ] );
	//	}
	//
	//}
	//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------
}

//構造体をEditorで日本語表示したい場合は、メンバを抽出してそれの配列を表示する
//もし[ SerializeField ]をで前にやっていた場合は一度消して値をリセットしないと保持されてしまい、Editor拡張時に不具合がでるかも
