using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class LockOnUIManager : MonoBehaviour {
	[ SerializeField ] private GazeController _gaze_controller = null;
	[ SerializeField ] private GameObject _lock_on_doing_ui = null;
	[ SerializeField ] private GameObject _lock_on_done_ui = null;

	private GameObject _pre_lock_on_doing_obj = null;
	private GameObject _pre_lock_on_done_obj = null;
	private AudioSource _audio_source = null;

	private void Awake( ) {
		_audio_source = GetComponent< AudioSource >( );
	}

	private void Start( ) {
		CheckReference( );

		_lock_on_doing_ui.SetActive( false );
		_lock_on_done_ui.SetActive( false );
	}

	private void Update( ) {
		LockOnUIActiveChange( );
		LockOnUIPosUpdate( );
	}

	private void LockOnUIActiveChange( ) {
		//ロックオン中UIの表示切替---------------------------------------------------------------------------
		//ロックオン中UIが表示されていてロックオン中のオブジェクトがNULLだったら非表示にする
		if ( _lock_on_doing_ui.activeSelf && _gaze_controller.getLockOnDoingObject( ) == null ) { 
			_lock_on_doing_ui.SetActive( false );
		}

		//前のフレームの時とロックオン中のオブジェクトが違っていてオブジェクトがNULLじゃなかったら表示更新
		if ( _pre_lock_on_doing_obj != _gaze_controller.getLockOnDoingObject( ) && 
			 _gaze_controller.getLockOnDoingObject( ) != null ) {
			_lock_on_doing_ui.SetActive( true );
		}
		//--------------------------------------------------------------------------------------------------

		//ロックオンUIの表示切替----------------------------------------------------------------------------
		//ロックオンUIが表示されていてロックオンしたオブジェクトがNULLだったら非表示にする
		if ( _lock_on_done_ui.activeSelf && _gaze_controller.getLockOnObject( ) == null ) { 
			_lock_on_done_ui.SetActive( false );	
		}

		//前のフレームの時とロックオンしたオブジェクトが違っていてオブジェクトがNULLじゃなかったら表示更新
		if ( _pre_lock_on_done_obj != _gaze_controller.getLockOnObject( ) && 
			 _gaze_controller.getLockOnObject( ) != null ) {
			_lock_on_done_ui.SetActive( true );
			_audio_source.PlayOneShot( SoundRegistry.getSE( SoundRegistry.SE.LOCK_ON ) );
		}
		//---------------------------------------------------------------------------------------------------

		//このフレームのオブジェクトに更新
		_pre_lock_on_doing_obj = _gaze_controller.getLockOnDoingObject( );
		_pre_lock_on_done_obj = _gaze_controller.getLockOnObject( );

	}

	//ロックオンUIが表示されていたら場所を更新
	private void LockOnUIPosUpdate( ) {
		if ( _lock_on_doing_ui.activeSelf && _gaze_controller.getLockOnDoingObject( ) != null ) {
			_lock_on_doing_ui.transform.position = _gaze_controller.getLockOnDoingObject( ).transform.position;
		}

		if ( _lock_on_done_ui.activeSelf && _gaze_controller.getLockOnObject( ) != null ) { 
			_lock_on_done_ui.transform.position = _gaze_controller.getLockOnObject( ).transform.position;
		}
	}

	private void CheckReference( ) { 
		Assert.IsNotNull( _gaze_controller, "[LockOnUIManager]GazeControllerの参照がありません" );
		Assert.IsNotNull( _lock_on_doing_ui, "[LockOnUIManager]GameObjectのLockOnDoingUIの参照がありません" );
		Assert.IsNotNull( _lock_on_done_ui, "[LockOnUIManager]GameObjectのLockOndoneUIの参照がありません" );
		Assert.IsNotNull( _audio_source, "[LockOnUIManager]AudioSourceがアタッチされていません" );
	}

}