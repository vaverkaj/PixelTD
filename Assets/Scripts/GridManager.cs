using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridManager : MonoBehaviour
{
    public Tilemap tilemap;
    public Tilemap accessibilityMap;
    public TileBase roadTile;
    public Vector3Int[,] spots;
    Astar astar;
    List<Spot> roadPath = new List<Spot>();
    new Camera camera;
    BoundsInt bounds;
    public GameObject towerPrefab;

    GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        tilemap.CompressBounds();
        accessibilityMap.CompressBounds();
        bounds = tilemap.cellBounds;
        camera = Camera.main;
        gameManager = GameObject.Find("Game").GetComponent<GameManager>();

        CreateGrid();
        astar = new Astar(spots, bounds.size.x, bounds.size.y);
    }
    public void CreateGrid()
    {
        spots = new Vector3Int[bounds.size.x, bounds.size.y];
        for (int x = bounds.xMin, i = 0; i < (bounds.size.x); x++, i++)
        {
            for (int y = bounds.yMin, j = 0; j < (bounds.size.y); y++, j++)
            {
                if (accessibilityMap.HasTile(new Vector3Int(x, y, 0)))
                {
                    spots[i, j] = new Vector3Int(x, y, 0);
                }
                else
                {
                    spots[i, j] = new Vector3Int(x, y, 1);
                }
            }
        }
    }
    private void DrawRoad()
    {
        for (int i = 0; i < roadPath.Count; i++)
        {
            accessibilityMap.SetTile(new Vector3Int(roadPath[i].X, roadPath[i].Y, 0), roadTile);
        }
    }
    // Update is called once per frame
    public Vector2Int start;
    void Update()
    { 
        if (Input.GetMouseButton(0))
        {
            Vector3 world = camera.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int gridPos = accessibilityMap.WorldToCell(world);
            if (accessibilityMap.GetTile(new Vector3Int(gridPos.x, gridPos.y, 0))) {
                accessibilityMap.SetTile(new Vector3Int(gridPos.x, gridPos.y, 0), null);


                    GameObject creature = Instantiate(towerPrefab,
                    new Vector3(gridPos.x, gridPos.y, 0) + tilemap.transform.localScale / 2.0f,
                    Quaternion.identity,
                    GameObject.Find("Instances").transform);


                CreateGrid();
                gameManager.placedStones++;
                if (gameManager.placedStones == 5) {
                    gameManager.gameState = GameManager.GameState.defending;
                    gameManager.placedStones = 0;
                }
            }
            
        }
    }

    public List<Vector3> GetPath(Vector2 start, Vector2 end) {
        Vector3Int startCell = tilemap.WorldToCell(new Vector3(start.x, start.y, 0));
        Vector3Int endCell = tilemap.WorldToCell(new Vector3(end.x, end.y, 0));
        List<Spot> pathTiles = astar.CreatePath(spots, new Vector2Int(startCell.x, startCell.y), new Vector2Int(endCell.x, endCell.y), 1000);
        List<Vector3> path = new List<Vector3>();
        for (int i = 0; i < pathTiles.Count; i++) {
            path.Add(tilemap.CellToWorld(new Vector3Int(
                pathTiles[pathTiles.Count-1 -i].X, 
                pathTiles[pathTiles.Count - 1 -i].Y
                , 0)) + tilemap.transform.localScale/2.0f);
        }
        return path;
    }

}
