using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Player : Character {
	[ SerializeField ] private GazeController _gaze_controller = null;
	[ SerializeField ] private Gun _gun = null;
	[ SerializeField ] private float _pos_z = 0;
	[ SerializeField ] private float _spped = 0;
	[ SerializeField ] private float _move_limit = 0;
	[ SerializeField ] private float _direction_size = 0;

	private Controller _controller = new Controller( );
	private Vector3 _limit_pos = new Vector3( 0, 0, 0 );
	private float _pre_pos_x = 0;
	private bool _is_death = false;

	private void Start( ) {
		CheckReference( );

		_limit_pos = new Vector3( transform.position.x + _move_limit, 0, _pos_z );
		transform.position = new Vector3( 0, 0, _pos_z );
	}

	private void FixedUpdate( ) {
		Move( );
	}

	private void Update( ) {
		Direction( );
		Shoot( );
		Death( );	
	}

	private void OnTriggerEnter( Collider other ) {
		if ( other.gameObject.tag == StringConstantRegistry.getTag( StringConstantRegistry.TAG.ENEMY ) ) { 
			_is_death = true;
		}
	}

	//移動処理
	protected override void Move( ) {
		//Vector2 pos = _controller.getControllerPos( );
		//
		//if ( ( _pre_pos_x != pos.x ) && ( pos.x != 0 ) ) {
		//	if ( _pre_pos_x < pos.x ) { 
		//		transform.position += new Vector3( 1, 0, 0 ) * _spped * Time.deltaTime;	
		//	}
		//
		//	if ( _pre_pos_x > pos.x ) {
		//		transform.position += new Vector3( -1, 0, 0 ) * _spped * Time.deltaTime;
		//	}
		//}
		//
		//_pre_pos_x = pos.x;

		Vector3 controller_dir = _controller.getControllerDir( );
		if ( controller_dir.x > 90f ) { 
			transform.position += new Vector3( 1, 0, 0 ) * _spped * Time.deltaTime;
		}

		if ( controller_dir.x < -90f ) { 
			transform.position += new Vector3( -1, 0, 0 ) * _spped * Time.deltaTime;	
		}

		if ( controller_dir.y < -90f ) { 

		}


		MoveLimit( );
	}

	//死亡処理
	protected override void Death( ) {
		if ( !_is_death ) return;	//死亡フラグが立っていなかったらreturn

		Destroy( this.gameObject );
	}

	//射撃処理
	private void Shoot( ) {
		if ( !_controller.getCotrollerInput( Controller.INPUT_TYPE.TRIGGER ) ) return;	//ボタンを入力してなかったらreturn

		//敵をロックオンしていたら撃つ
		if ( _gaze_controller.getLockOnObject( ) != null ) {
			_gun.Shoot( _gaze_controller.getLockOnObject( ) );
		}
	}

	//Playerが動ける範囲
	private void MoveLimit( ) {
		if ( transform.position.x > _limit_pos.x ) {
			transform.position = _limit_pos;
		}

		if ( transform.position.x < -_limit_pos.x ) {
			Vector3 reverse_limit_pos = new Vector3( -_limit_pos.x, 0, _pos_z );
			transform.position = reverse_limit_pos;	
		}
	}

	//角度を視線と常に合わせる
	private void Direction( ) {
		Vector3 player_dir = new Vector3( -_gaze_controller.getDirection( ).y, _gaze_controller.getDirection( ).x, 0 );	//恐らく、2Dと3Dで向きの計算が違うため入れ替えてる(2Dは単純にｘは横、ｙは縦。3Dはそれぞれの線の軸を中心に回転する)
		transform.eulerAngles = player_dir * _direction_size;
	}

	private void CheckReference( ) {
		Assert.IsNotNull( _gaze_controller, "[Player]GazeControllerの参照がありません" );
		Assert.IsNotNull( _gun, "[Player]Gunの参照がありません" );
	}


	public float getMoveLimit( ) { 
		return _move_limit;	
	} 

}

//視線とオブジェクトの角度の合わせ方がよくわからない。PlayerのZには謎の値が入る　//360をかける
//Debug・Releaseで使う処理と使わない処理を分けるようにする。