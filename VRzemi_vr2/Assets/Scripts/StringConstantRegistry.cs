using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StringConstantRegistry {
	public enum TAG { 
		BULLET,
		ENEMY,
		ENEMY_DESTORY_AREA,
	}

	public enum SCENE_NAME { 
		START,
		MAIN,
		RESULT
	}

	public static string getTag( TAG tag ) { 
		switch ( tag ) { 
			case TAG.BULLET:
				return "Bullet";

			case TAG.ENEMY:
				return "Enemy";

			case TAG.ENEMY_DESTORY_AREA:
				return "EnemyDestroyArea";

			default:
				return null;
		}
	}

	public static string getSceneName( SCENE_NAME scene_name ) { 
		switch ( scene_name ) { 
			case SCENE_NAME.START:
				return "Start";

			case SCENE_NAME.MAIN:
				return "Main";

			case SCENE_NAME.RESULT:
				return "Result";

			default:
				return null;
		}
	}
	
}
