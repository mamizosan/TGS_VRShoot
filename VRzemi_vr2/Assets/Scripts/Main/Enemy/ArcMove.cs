using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcMove : EnemyMove {

    public float radius = 1.0f;
    public float speed = 1.0f;

    private float pos_x = 1.0f;
    private float pos_z = 1.0f;

    public Vector3 Move(float speed) {

        Arc();

            return new Vector3(pos_x - speed, 0, pos_z - speed);
    }

      public void Arc() {
             
       // if (pos_z != 0 && pos_x != -1) {

            pos_x = radius * Mathf.Sin(Time.time * speed);
            pos_z = radius * Mathf.Cos(Time.time * speed);

       // }
    }

}
