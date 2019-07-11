using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;

public class CountDown : MonoBehaviour {
	private const int COUNT_DOWN_LENGTH = 6;									//最大カウント数(Startを含める)

	[ SerializeField ] private float _set_count_down_time = 0;					//次のカウント進める時間(秒速)

	private Image _image = null;
	private Sprite[ ] _cound_down_sprites = new Sprite[ COUNT_DOWN_LENGTH ];
	private float _count_down_time = 0; 
	private int _now_count_index = COUNT_DOWN_LENGTH - 1;						//現在のカウント
	private bool _is_next_count = false;										//次のカウントに進むかどうか
	private bool _is_finish_count = false;										//カウントが終了したかどうか

	private void Awake( ) {
		_image = GetComponent< Image >( );

		_cound_down_sprites[ 0 ] = Resources.Load< Sprite >( "CountDown/CountDown_Start" );
		_cound_down_sprites[ 1 ] = Resources.Load< Sprite >( "CountDown/CountDown_1" );
		_cound_down_sprites[ 2 ] = Resources.Load< Sprite >( "CountDown/CountDown_2" );
		_cound_down_sprites[ 3 ] = Resources.Load< Sprite >( "CountDown/CountDown_3" );
		_cound_down_sprites[ 4 ] = Resources.Load< Sprite >( "CountDown/CountDown_4" );
		_cound_down_sprites[ 5 ] = Resources.Load< Sprite >( "CountDown/CountDown_5" );
		
		CheckReference( );
	}

	private void Start( ) {
		_count_down_time = _set_count_down_time;
		_image.sprite = _cound_down_sprites[ _now_count_index ];
	}

	private void FixedUpdate( ) {
		if ( _is_finish_count ) return;	//カウントが終了してたらreturn
		if ( _is_next_count ) return;	//次のカウントが出来る状態だったらreturn
		
		_count_down_time -= Time.deltaTime;

		if ( _count_down_time < 0 ) {	//次のカウントをする時間だったら
			_count_down_time = 0;
			_is_next_count = true;	//次のカウントをできる状態にする
		}
	}

	private void Update( ) {
		if ( _is_finish_count ) return;	//カウントが終了してたらreturn
		if ( !_is_next_count ) return;	//次のカウントが出来ない状態だったらreturn

		_now_count_index--;
		if ( _now_count_index < 0 ) {	//次の配列が無かったら
			FinishCountDown( );
		} else {
			NextCountDown( );
		}
	}

	private void FinishCountDown( ) { 
		_is_finish_count = true;				//カウントを終了状態に
		this.gameObject.SetActive( false );		
		Resources.UnloadUnusedAssets( );		//読み込んで使ってない画像をメモリ解放
	}

	private void NextCountDown( ) { 
		_image.sprite = _cound_down_sprites[ _now_count_index ];	//次のカウントを表示する
		_count_down_time = _set_count_down_time;					//次のカウントをする時間をリセット
		_is_next_count = false;										//次のカウントを出来ない状態にする
	}

	private void CheckReference( ) { 
		for ( int i = 0; i < COUNT_DOWN_LENGTH; i++ ) { 
			Assert.IsNotNull( _cound_down_sprites[ i ], "[CountDown] CountDown配列の" + i + "番目がありません" );	
		}
		Assert.IsNotNull( _image, "[CountDown] Imageがコンポーネントされていません" );
	}

	public bool getIsFinishCount( ) { 
		return _is_finish_count;	
	}

}

//画像やサウンドの参照を一括管理するクラスを作る？Flyweight パターン