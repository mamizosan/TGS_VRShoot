using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger {
	public void SceneChange( StringConstantRegistry.SCENE_NAME scene_name ) { 
		SceneManager.LoadScene( StringConstantRegistry.getSceneName( scene_name ) );	
	}
}
