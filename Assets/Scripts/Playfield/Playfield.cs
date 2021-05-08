using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Playfield : MonoBehaviour
{
    [Serializable]
    public struct SpawnLocation
    {
        public int PlayerIndex;
        public Transform Spawn;
    }
    struct PlayfieldConnectedPoints
    {
        public Vector3 Point1;
        public Vector3 Point2;
        public float Distance;
        public Collider Collider;
    }
    struct DataContainer
    {
        public Vector3 Center;
        public Vector3 Extends;
    }

    [SerializeField]
    public List<SpawnLocation> SpawnLocations;
    public LayerMask GroundLayer;
    public LayerMask BorderLayer;
    public Transform PointsParent;
    public Transform SpawnLocationParent;
    public Transform ObjectiveSpawnLocationParent;
    public float BorderWidth = 1f;

    private List<Vector3> _spawnLocations;
    private List<Vector3> _objectiveSpawnLocations;
    private DataContainer Data;
    private List<PlayfieldConnectedPoints> _lines;
    private System.Random rnd = new System.Random();

    void Start()
    {
        DefinePlayfieldRectangle();
        ConnectPlayfieldPoints();
        CreatePlayfieldColliders();
        CreateSpawnLocations();
        CreateObjectiveSpawnLocation();
    }

    private void CreateSpawnLocations()
    {
        _spawnLocations = new List<Vector3>();
        for (var i = 0; i < SpawnLocationParent.childCount; i++)
        {
            _spawnLocations.Add(SpawnLocationParent.GetChild(i).position);

        }
    }

    private void CreateObjectiveSpawnLocation()
    {
        _objectiveSpawnLocations = new List<Vector3>();
        for (var i = 0; i < ObjectiveSpawnLocationParent.childCount; i++)
        {
            _objectiveSpawnLocations.Add(ObjectiveSpawnLocationParent.GetChild(i).position);
        }
    }

    public void DefinePlayfieldRectangle()
    {
        var minX = 0f;
        var maxX = 0f;
        var minZ = 0f;
        var maxZ = 0f;

        for (var i = 0; i < PointsParent.childCount; i++)
        {
            var pos = PointsParent.GetChild(i).position;

            if (i == 0)
            {
                minX = pos.x;
                maxX = pos.x;
                minZ = pos.z;
                maxZ = pos.z;
            }
            else
            {
                if (pos.x > maxX)
                {
                    maxX = pos.x;
                }
                if (pos.x < minX)
                {
                    minX = pos.x;
                }

                if (pos.z > maxZ)
                {
                    maxZ = pos.z;
                }
                if (pos.z < minZ)
                {
                    minZ = pos.z;
                }
            }
        }

        var downLeft = new Vector3(minX, 0, minZ);
        var downRight = new Vector3(maxX, 0, minZ);
        var upLeft = new Vector3(minX, 0, maxZ);
        var upRight = new Vector3(maxX, 0, maxZ);

        Data.Center = new Vector3((minX + maxX) / 2, 0, (minZ + maxZ) / 2);
        Data.Extends = new Vector3(Vector3.Distance(downLeft, downRight) / 2, 0, Vector3.Distance(downLeft, upLeft) / 2);
    }

    public void ConnectPlayfieldPoints()
    {
        _lines = new List<PlayfieldConnectedPoints>();
        for (var i = 0; i < PointsParent.childCount; i++)
        {
            var cp = new PlayfieldConnectedPoints();
            cp.Point1 = PointsParent.GetChild(i).transform.position;

            if (i == PointsParent.childCount - 1)
            {
                cp.Point2 = PointsParent.GetChild(0).transform.position;
                cp.Distance = Vector3.Distance(cp.Point1, cp.Point2);
            }

            _lines.Add(cp);

            if (i != 0)
            {
                var prevConnection = _lines[i - 1];
                prevConnection.Point2 = PointsParent.GetChild(i).transform.position;
                prevConnection.Distance = Vector3.Distance(prevConnection.Point1, prevConnection.Point2);
                _lines[i - 1] = prevConnection;
            }
        }
    }

    public void CreatePlayfieldColliders()
    {
        var parent = new GameObject();
        parent.transform.parent = transform;
        parent.name = "Borders";
        for (var i = 0; i < _lines.Count; i++)
        {
            var line = _lines[i];
            var obj = new GameObject();
            obj.transform.parent = parent.transform;
            obj.name = "Border" + i;
            obj.layer = Mathf.RoundToInt(Mathf.Log(BorderLayer.value, 2));
            obj.transform.position = (line.Point2 - line.Point1)*0.5f + line.Point1;
            obj.transform.rotation = Quaternion.FromToRotation(Vector3.right, line.Point2 - line.Point1);
            var box = obj.AddComponent<BoxCollider>();
            box.center = Vector3.zero;
            var boxSize = new Vector3(0, 0, 0);
            boxSize.x = Vector3.Distance(line.Point1, line.Point2);
            boxSize.y = 20f;
            boxSize.z = BorderWidth;
            box.size = boxSize;

            line.Collider = box;
            _lines[i] = line;
        }
    }

    public Vector3 GetSpawnLocation(int playerIndex)
    {
        if (playerIndex > _spawnLocations.Count)
            return Data.Center;

        return _spawnLocations[playerIndex];
    }

    public Vector3 GetObjectiveSpawnLocation()
    {
        if (_objectiveSpawnLocations.Count == 0)
            return GetRandomPosition();

        return _objectiveSpawnLocations[rnd.Next(0, _objectiveSpawnLocations.Count)];
    }

    //todo MAKE THIS BASE ON TRIANGULATION FOR ACCURATE RESULTS AND BETTER PERFORMANCE
    public Vector3 GetRandomPosition()
    {
        var x = UnityEngine.Random.Range(-Data.Extends.x, Data.Extends.x);
        var z = UnityEngine.Random.Range(-Data.Extends.z, Data.Extends.z);
        var ray = new Ray(new Vector3(x, 30f, z), Vector3.down);

        RaycastHit hit;
        if (!Physics.Raycast(ray, out hit, 60f, GroundLayer))
        {
            return GetRandomPosition();
        }
        
        var rayBorder = new Ray(hit.point, Vector3.right);
        var hits = Physics.RaycastAll(rayBorder, 100f, BorderLayer);
        if (hits.Length%2 == 0)
        {
            return GetRandomPosition();
        }

        return hit.point;
    }
}
