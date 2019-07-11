using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;

public class MainManager : ScenesManager {
	[ SerializeField ] private TextMesh _time_limit_text = null;
	[ SerializeField ] private float _time_limit = 0;

	private bool _is_finish_time_limit = false;

	private void Start( ) {
		CheckReference( );

		int show_time_limit = ( int )_time_limit;
		_time_limit_text.text = show_time_limit.ToString( );
	}

	private void FixedUpdate( ) {
		TimeLimitCount( );
	}

	private void Update( ) {
		ShowTimeLimit( );

		if ( _is_finish_time_limit ) {
			NextScene( );
		}
	}

	private void TimeLimitCount( ) { 
		if ( _is_finish_time_limit ) return;

		if ( _time_limit <= 0 ) { 
			_time_limit = 0;
			_is_finish_time_limit = true;
			return;
		}
		_time_limit -= Time.deltaTime;
	}

	private void ShowTimeLimit( ) { 
		int show_time_limit = ( int )_time_limit;
		_time_limit_text.text = show_time_limit.ToString( );
	}

	private void CheckReference( ) { 
		Assert.IsNotNull( _time_limit_text, "[MainManager]GameObjectのTimeLimitTextの参照がありません" );
	}


	protected override void NextScene( ) { 
		_scene_changer.SceneChange( StringConstantRegistry.SCENE_NAME.RESULT );
	} 

}
