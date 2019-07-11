using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class ScoreManager {
	public enum SCORE { 
		ENEMY_A = 10,
		ENEMY_B = 20,
		ENEMY_C = 30,
	}

	private static ScoreManager _scoreManager = new ScoreManager( );
	public static int _score = 0;	//関数からのみこの変数をいじること。直接いじらない

	private ScoreManager( ) { }

	public void AddScore( SCORE score ) { 
		switch ( score ) { 
			case SCORE.ENEMY_A:
				_score += ( int )SCORE.ENEMY_A;
				break;

			case SCORE.ENEMY_B:
				_score += ( int )SCORE.ENEMY_B;
				break;

			case SCORE.ENEMY_C:
				_score += ( int )SCORE.ENEMY_C;
				break;

			default:
				Assert.IsNotNull( null, "[ScoreManager]追加したいスコアタイプが存在しません" );
				break;
		}
	}

	public void ResetScore( ) { 
		_score = 0;	
	}

	public int getScore( ) { 
		return _score;	
	}

	public static ScoreManager getInstance( ) { 
		return _scoreManager;
	}
	
}

//初めてインスタンス化された時か初めてstaticメソッドが呼び出されたときなどに行われるのでgetInstanceはstaticにしている。

