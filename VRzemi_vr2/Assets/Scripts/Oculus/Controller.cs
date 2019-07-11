using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller {
	public enum INPUT_TYPE { 
		BACK_BUTTON,
		HOME_BUTTON_LONG_PRESS,
		TRIGGER,
		TRIGGER_DOWN,
		TRIGGER_UP,
		TOUCH_PAD_CLICK,
		TOUCH_PAD_SCREEL_UP,
		TOUCH_PAD_SCREEL_DOWN,
		TOUCH_PAD_SCREEL_LEFT,
		TOUCH_PAD_SCREEL_RIGHT,
	};


	public bool getCotrollerInput( INPUT_TYPE input_type ) {
		switch ( input_type ) {
			//バックボタン入力取得
			case INPUT_TYPE.BACK_BUTTON:
				return OVRInput.Get( OVRInput.RawButton.Back );

			//ホームボタン長押し入力取得
			case INPUT_TYPE.HOME_BUTTON_LONG_PRESS:
				return OVRInput.GetControllerWasRecentered( );

			//トリガー長押し取得
			case INPUT_TYPE.TRIGGER:
				return OVRInput.Get( OVRInput.Button.PrimaryIndexTrigger );

			//トリガー押した時取得
			case INPUT_TYPE.TRIGGER_DOWN:
				return OVRInput.GetDown( OVRInput.Button.PrimaryIndexTrigger );

			//トリガー話した時取得
			case INPUT_TYPE.TRIGGER_UP:
				return OVRInput.GetUp( OVRInput.Button.PrimaryIndexTrigger );

			//タッチパッドクリック取得(長押し)
			case INPUT_TYPE.TOUCH_PAD_CLICK:
				return OVRInput.Get( OVRInput.Button.PrimaryTouchpad );

			//タッチパッドスクロール上方向取得
			case INPUT_TYPE.TOUCH_PAD_SCREEL_UP:
				return OVRInput.Get( OVRInput.Button.Up );

			//タッチパッドスクロール下方向取得
			case INPUT_TYPE.TOUCH_PAD_SCREEL_DOWN:
				return OVRInput.Get( OVRInput.Button.Down );

			//タッチパッドスクロール左方向取得
			case INPUT_TYPE.TOUCH_PAD_SCREEL_LEFT:
				return OVRInput.Get( OVRInput.Button.Left );

			//タッチパッドスクロール右方向取得
			case INPUT_TYPE.TOUCH_PAD_SCREEL_RIGHT:
				return OVRInput.Get( OVRInput.Button.Right );

			default:
				return false;
		}
	}


	//コントローラーのタッチパッドのタッチ位置取得
	public Vector2 getControllerPos( ) { 
		return OVRInput.Get( OVRInput.Axis2D.PrimaryTouchpad );	
	}

	//コントローラーの傾き取得
	public Vector3 getControllerDir( ) { 
		return OVRInput.GetLocalControllerRotation( OVRInput.GetActiveController( ) ).eulerAngles;
	}
	
}
