using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultManager : ScenesManager {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space)) {

            NextScene();
        }
		
	}

    protected override void NextScene()
    {
        _scene_changer.SceneChange(StringConstantRegistry.SCENE_NAME.START);
    }


}
