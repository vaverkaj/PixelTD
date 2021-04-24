using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class CreatureManager : MonoBehaviour
{
    GridManager gridManager;
    public SpriteAtlas creatureSprites;
    public GameObject pathing;
    public GameObject creaturePrefab;
    // Start is called before the first frame update
    void Start()
    {
        gridManager = GameObject.Find("Grid").GetComponent<GridManager>();
        StartCoroutine(spawnCreatures());
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    IEnumerator spawnCreatures() 
    {
        yield return new WaitForSeconds(1);
        List<Vector3> path = GetFullPath();
        for (int i = 0; i < path.Count; i++) { 
            yield return new WaitForSeconds(0.1f);
            GameObject creature = Instantiate(creaturePrefab,
                new Vector3(path[i].x, path[i].y, 0), 
                Quaternion.identity,
                GameObject.Find("Instances").transform);

            creature.GetComponent<SpriteRenderer>().sprite = creatureSprites.GetSprite("urizen_653");
            
        }
    }

    private List<Vector3> GetFullPath() {
        List<Vector3> path = new List<Vector3>();
        for (int i = 0; i < pathing.transform.childCount - 1; i++) {
            Vector2 start = new Vector2(pathing.transform.GetChild(i).position.x, pathing.transform.GetChild(i).position.y);
            Vector2 end = new Vector2(pathing.transform.GetChild(i+1).position.x, pathing.transform.GetChild(i+1).position.y);
            path.AddRange(gridManager.GetPath(start, end));
            if (i + 1 < pathing.transform.childCount - 1) {
                path.RemoveAt(path.Count - 1);
            }
        }


        return path;
    }
}
