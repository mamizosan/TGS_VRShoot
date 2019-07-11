using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class GazeController : MonoBehaviour {
	[ SerializeField ] private Canvas _gaze_canvas = null;	//視線UIのあるキャンバス
	[ SerializeField ] private float _set_lock_on_timer = 0;

	private RectTransform _rec = null;
	private Ray _gaze;								//視線
	private GameObject _hit_object = null;			//視線に当たったオブジェクト
	private bool _is_hit = false;					//視線に当たったかどうか
	private bool _is_lock_on = false;				//ロックオンするかどうか
	private float _lock_on_timer = 0;

	private void Awake( ) {
		_rec = GetComponent< RectTransform >( );
	}

	private void Start( ) {
		CheckReference( );
	}

	private void FixedUpdate( ) {
		LockOnCount( );
	}

	private void Update( ) {
		GazeFly( );
	}

	//視線を飛ばす
	private void GazeFly( ) {
		Vector2 origin = RectTransformUtility.WorldToScreenPoint( _gaze_canvas.worldCamera,_rec.position );	//UIの座標をスクリーン座標に変換
		_gaze = Camera.main.ScreenPointToRay( origin );

		//デバッグ用---------------------------------------------------------
		Debug.DrawRay( _gaze.origin, _gaze.direction, Color.red );
		Debug.Log( Camera.main.ScreenToWorldPoint( origin ) );
		Debug.Log( _gaze.origin.ToString( ) + _gaze.direction.ToString( ) );
		//-------------------------------------------------------------------

		RaycastHit hit;
		_is_hit = Physics.Raycast( _gaze, out hit );
		LockOn( hit );
	}

	private void LockOn(  RaycastHit hit ) {
		if ( hit.collider == null || hit.collider.tag != StringConstantRegistry.getTag( StringConstantRegistry.TAG.ENEMY ) ) {
			_hit_object = null;
			_is_lock_on = false;
			return;
		}

		//前のフレームで取得したオブジェクトと一緒だったらロックオンを始める(同じオブジェクトじゃなくてもカウントがそのまま進むバグ防止)
		if ( _hit_object != hit.collider.gameObject ) { 
			_hit_object = hit.collider.gameObject;
			_is_lock_on = false;
		} else { 
			_is_lock_on = true;
		}

	}

	private void LockOnCount( ) {
		if ( _is_lock_on ) {
			_lock_on_timer -= Time.deltaTime;	
		} else { 
			_lock_on_timer = _set_lock_on_timer;
		}

		if ( _lock_on_timer < 0 ) { 
			_lock_on_timer = 0;	
		}
	}

	private void CheckReference( ) { 
		Assert.IsNotNull( _gaze_canvas, "[GazeController]GazeCanvasの参照がありません" );
	}

	public Vector3 getDirection( ) {
		return 	_gaze.direction;	//恐らく、2Dでの計算の角度を取得する
	}

	public bool getIsHit( ) {
		return _is_hit;
	}

	public GameObject getLockOnObject( ) {
		//視線がオブジェクトにヒットしていて、一定以上見ていたら(ロックオンが完了していたら)そのオブジェクト返す
		if ( _hit_object == null || _lock_on_timer > 0 ) { 
			return null;
		} else {
			return _hit_object;
		}
	}

	public GameObject getLockOnDoingObject( ) {
		//視線がオブジェクトにヒットしていて、一定以上見ていなかったら(ロックオン中だったら)そのオブジェクトを返す
		if ( _hit_object != null && _lock_on_timer > 0 ) { 
			return _hit_object;
		} else {
			return null;
		}
		
	}
}