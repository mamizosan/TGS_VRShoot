using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Enemy : Character {

	public enum ENEMY_TYPE { 
		TYPE_A,
		TYPE_B,
		TYPE_C,
	};

	private ScoreManager _scoreManager = null;
	private AudioSource _audio_source = null;
	private EnemyMove _enemy_move = null;
	private ENEMY_TYPE _enemy_type = ENEMY_TYPE.TYPE_A;
	private float _move_speed = 0;
	private int _hit_point = 0;

	private void Awake( ) {
		_scoreManager = ScoreManager.getInstance( );
		_audio_source = GetComponent< AudioSource >( );
	}

	private void FixedUpdate( ) {
		if ( _enemy_move == null ) return;	//初期化されるまで更新しない

		Move( );
	}

	private void Update( ) {
		Death( );
	}

	private void OnTriggerEnter( Collider other ) {
		//弾に当たったら体力を減らす
		if ( other.gameObject.tag == StringConstantRegistry.getTag( StringConstantRegistry.TAG.BULLET ) ) {
			Destroy( other.gameObject );
			_hit_point--;
			if ( _hit_point < 0 ) {
				_hit_point = 0;
			}
		}

		if ( other.gameObject.tag == StringConstantRegistry.getTag( StringConstantRegistry.TAG.ENEMY_DESTORY_AREA ) ) {		//敵削除エリアに入ったら
			Destroy( this.gameObject );	
		}
	}

	//移動処理
	protected override void Move( ) {
		transform.position += _enemy_move.Move( _move_speed ) * Time.deltaTime;
	}

	//死亡処理
	protected override void Death( ) {
		if ( _hit_point <= 0 ) {
			AddScore( );

			_audio_source.PlayOneShot( SoundRegistry.getSE( SoundRegistry.SE.EXPLOSION ) );

			Destroy( this.gameObject );
		}
	}

	//スコア加算
	private void AddScore( ) { 
		switch ( _enemy_type ) { 
			case ENEMY_TYPE.TYPE_A:
				_scoreManager.AddScore( ScoreManager.SCORE.ENEMY_A );
				break;

			case ENEMY_TYPE.TYPE_B:
				_scoreManager.AddScore( ScoreManager.SCORE.ENEMY_B );
				break;

			case ENEMY_TYPE.TYPE_C:
				_scoreManager.AddScore( ScoreManager.SCORE.ENEMY_B );
				break;

			default:
				Assert.IsNotNull( _enemy_move, "[Enemy]動きの種類が入ってません" );
				break;
		}
	}

	private void CheckReference( ) { 
		Assert.IsNotNull( _audio_source, "[Enemy]AudioSourceがアタッチされていません" );
	}

	//初期化
	public void Initialize( ENEMY_TYPE enemy_type, float move_speed, int hit_point ) { 
		_enemy_type = enemy_type;
		_move_speed = move_speed;
		_hit_point = hit_point;

		switch ( _enemy_type ) { 
			case ENEMY_TYPE.TYPE_A:
				_enemy_move = new StraightMove( );
				break;

			case ENEMY_TYPE.TYPE_B:
				_enemy_move = new ArcMove( );
				break;

			case ENEMY_TYPE.TYPE_C:
				_enemy_move = new WaveMove( );
				break;

			default:
				Assert.IsNotNull( _enemy_move, "[Enemy]敵の種類が入ってません" );
				break;
		}
	}
}
