using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowScore : MonoBehaviour {
	[ SerializeField ] TextMesh _score_text = null;

	ScoreManager _scoreManager = null;

	private void Awake( ) {
		_scoreManager = ScoreManager.getInstance( );
	}

	private void Start( ) {
		_score_text.text = _scoreManager.getScore( ).ToString( );
	}
}
