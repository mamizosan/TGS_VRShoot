using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour {

	protected abstract void Move( );
	protected abstract void Death( );

}