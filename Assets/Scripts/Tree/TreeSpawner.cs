using UnityEngine;

public class TreeSpawner : MonoBehaviour
{
    public float minSpace = 2.5f;
    public int maxAttempt = 1000;
    public PolygonCollider2D[] areaPolygon;
    public Transform forest;
    public GameObject treePrefab;
    public static TreeSpawner Instance;
    public LayerMask layers;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        foreach (PolygonCollider2D polygon in areaPolygon)
            SpawnForest(polygon);
    }

    public void SpawnForest(PolygonCollider2D areaPolygon)
    {
        Bounds bounds = areaPolygon.bounds;
        int attempt = 0;
        int spawned = 0;

        while (attempt < maxAttempt)
        {
            attempt++;

            Vector2 treeSpawn = new Vector2(Random.Range(bounds.min.x, bounds.max.x), Random.Range(bounds.min.y, bounds.max.y));

            if (!areaPolygon.OverlapPoint(treeSpawn)) continue;

            Collider2D hit = Physics2D.OverlapCircle(treeSpawn, minSpace, layers);
            if(hit != null) continue;

            bool tooClose = false;
            foreach (TreeScript tree in TreeManager.Instance.trees)
            {
                if (Vector2.Distance(treeSpawn, tree.transform.position) < minSpace)
                {
                    tooClose = true;
                    break;
                }
            }
            if (tooClose) continue;

            var treeScript = Instantiate(treePrefab, treeSpawn, Quaternion.identity, forest);
            spawned++;
        }
    }
}
