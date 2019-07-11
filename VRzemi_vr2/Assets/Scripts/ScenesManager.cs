using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ScenesManager : MonoBehaviour {
	protected SceneChanger _scene_changer = new SceneChanger( );
	protected abstract void NextScene( );
}
