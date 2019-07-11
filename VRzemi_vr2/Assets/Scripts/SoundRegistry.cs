using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public static class SoundRegistry {
	public enum SE { 
		BULLET,
		ENGINE,
		EXPLOSION,
		LOCK_ON,
	}

	private static Dictionary< SE, AudioClip > _se = new Dictionary< SE, AudioClip >( );


	public static AudioClip getSE( SE se ) { 
		if ( _se[ se ] == null ) { 
			LoadSE( se );
		}

		return _se[ se ];
	}

	private static void LoadSE( SE se ) {
		AudioClip se_date = null;
		string path = "";

		switch ( se ) { 
			case SE.BULLET:
				path = "SE/Bullet";
				break;

			case SE.ENGINE:
				path = "SE/Engine";
				break;

			case SE.EXPLOSION:
				path = "SE/Explosion";
				break;

			case SE.LOCK_ON:
				path = "SE/LockOn";
				break;

			default:
				Assert.IsNotNull( null, "[SoundRegistry]読み込みたいSEが存在しません" );
				break;
		}

		se_date = Resources.Load< AudioClip >( path );
		_se.Add( se, se_date );
		//Resources.UnloadUnusedAssets( );
	}
	
}

//pathもStringConstantRegistryに入れるべき？でも、pathはここでしか使わない
