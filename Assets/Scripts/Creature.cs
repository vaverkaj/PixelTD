using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature : MonoBehaviour {
    public List<Vector3> path;
    public int index = 0;
    public float speed = 1;
    public int health = 100;

    void Start() {
        speed = 2;
    }
    void Update() {
        gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, path[index], speed * Time.deltaTime);

        if (gameObject.transform.position == path[index]) {
            index++;
        }

        if (index == path.Count - 1) {
            Destroy(gameObject);
        }
    }
}
