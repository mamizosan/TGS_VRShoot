using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
	[ SerializeField ] private float _speed = 1;

	private GameObject _target_obj = null;	//ホーミングするターゲット

	private void Start( ) {
		Destroy( this.gameObject, 1f );
	}

	private void FixedUpdate( ) {
		//ロックオンした敵がいないまたは敵に弾が当たってなければホーミングする
		if ( _target_obj == null ) {
			transform.position += transform.forward * _speed * Time.deltaTime;
		} else {
			Vector3 target_dir = _target_obj.transform.position - transform.position;
			transform.position += target_dir.normalized * _speed * Time.deltaTime;
		}

		//transform.position += transform.forward * _speed;
	}

	public void setTarget( GameObject target_obj ) {
		_target_obj = target_obj;
	}

}