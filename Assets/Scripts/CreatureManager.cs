using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureManager : MonoBehaviour
{
    GridManager gridManager;
    public Sprite creatureSprites;
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
        for (int i = 0; i < 6; i++) { 
            yield return new WaitForSeconds(5);
            Vector2Int start = new Vector2Int((int)pathing.transform.GetChild(0).position.x, (int)pathing.transform.GetChild(0).position.y);
            Vector2Int end = new Vector2Int((int)pathing.transform.GetChild(6).position.x, (int)pathing.transform.GetChild(6).position.y);
            List<Spot> path = gridManager.GetPath(start, end);
            Debug.Log(path[i].Y);
            GameObject creature = Instantiate(creaturePrefab,
                new Vector3(path[i].X, path[i].Y, 0), 
                Quaternion.identity,
                GameObject.Find("Instances").transform);

            creature.GetComponent<SpriteRenderer>().sprite = creatureSprites;
            
        }
    }
}
