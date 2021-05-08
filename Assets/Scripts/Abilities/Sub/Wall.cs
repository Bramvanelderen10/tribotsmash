using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    public GameObject WallPrefab;
    public int MaxWallPieces = 3;
    public float WallDelay = 1f;
    public float PushVelocity = 10f;
    private int _currentWallPieces = 0;

    private List<GameObject> _hits = new List<GameObject>();

    public void StartSpawning()
    {
        StartCoroutine(SpawnWall());
    }

    IEnumerator SpawnWall()
    {
        var wall = Instantiate(WallPrefab);
        var wallComp = wall.GetComponent<WallPiece>();
        var z = wallComp.Extends.z;
        wall.transform.position = transform.position + transform.forward * (z + (_currentWallPieces * z));
        wallComp.Activate();
        _currentWallPieces++;

        //Push objects above the pillar away
        var extends = wallComp.Extends;
        var boxPos = transform.position + transform.forward * (z + (_currentWallPieces * (z)));
        boxPos.y += 1f;
        var results =
            Physics.OverlapBox(
                boxPos,
                new Vector3(extends.x / 2, extends.y / 2, extends.z / 2));

        foreach (var item in results)
        {
            if (_hits.Contains(item.gameObject)) continue;

            var rb = item.gameObject.GetComponent<Rigidbody>();
            if (!rb) continue;

            var itemPos = transform.InverseTransformPoint(item.transform.position);
            var otherPos = transform.InverseTransformPoint(boxPos);
            var dir = Vector3.zero;
            var lookDir = Vector3.zero;
            dir.y = PushVelocity;
            if (otherPos.x >= itemPos.x)
            {
                dir.x = -1;
                lookDir.x = 1;
            }
            else
            {
                lookDir.x = -1;
                dir.x = 1;
            }
            dir.x *= PushVelocity/2;
            rb.velocity += transform.TransformVector(dir);
            item.transform.forward = transform.TransformVector(lookDir);

            var player = item.gameObject.GetComponent<Player>();
            if (player)
                player.KnockDown();

            _hits.Add(item.gameObject);
        }

        yield return new WaitForSeconds(WallDelay);
        if (_currentWallPieces != MaxWallPieces)
        {
            StartCoroutine(SpawnWall());
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
