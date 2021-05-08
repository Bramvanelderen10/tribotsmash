using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CameraFollow : MonoBehaviour
{
    public float MoveSpeed = 4f;
    public float ResizeSpeed = 4f;
    public float Treshold = 0.1f;
    public float MinSize = 5f;
    public float ScaleOffset = 1f;

    public List<string> FollowTags = new List<string>
    {
        "Player",
        "PickUp"
    };

    private Camera _camera;
    private List<GameObject> _targetObjects = new List<GameObject>();

    private float _minX = 0f;
    private float _maxX = 0f;
    private float _minZ = 0f;
    private float _maxZ = 0f;
    private Corners _playerCorners = new Corners();
    // Use this for initialization
    void Start ()
	{
	    _camera = Camera.main;
	}
	
	// Update is called once per frame
	void LateUpdate ()
	{
	    CleanUpObjects();
        //Add all objects with specified tags to the target list
	    foreach (var item in FollowTags)
	    {
            foreach (var obj in GameObject.FindGameObjectsWithTag(item))
            {
                AddObject(obj);
            }
        }

	    if (_targetObjects.Count < 1)
	        return;

        for (var i = 0; i < _targetObjects.Count; i++)
        {
            var pos = _targetObjects[i].transform.position;

            if (i == 0)
            {
                _minX = pos.x;
                _maxX = pos.x;
                _minZ = pos.z;
                _maxZ = pos.z;
            }
            else
            {
                if (pos.x > _maxX)
                {
                    _maxX = pos.x;
                }
                if (pos.x < _minX)
                {
                    _minX = pos.x;
                }

                if (pos.z > _maxZ)
                {
                    _maxZ = pos.z;
                }
                if (pos.z < _minZ)
                {
                    _minZ = pos.z;
                }
            }
        }
	    
        _playerCorners.DownLeft = new Vector3(_minX, 0, _minZ);
        _playerCorners.DownRight = new Vector3(_maxX, 0, _minZ);
        _playerCorners.UpLeft = new Vector3(_minX, 0, _maxZ);
        _playerCorners.UpRight = new Vector3(_maxX, 0, _maxZ);

        //Calculate the center position between the targets and lerp the camera position towards the center
        var center = new Vector3((_minX + _maxX) / 2, transform.position.y, (_minZ + _maxZ) / 2);
	    transform.position = Vector3.Lerp(transform.position, center, MoveSpeed * Time.deltaTime);

        //Calculate optimal camera size based on targets
        //Determine world position of all camera corners on a invisible plane
	    var cameraCorners = new Corners();
	    var plane = new Plane(Vector3.up, Vector3.zero);
	    float distance;
	    var ray = _camera.ViewportPointToRay(Vector3.zero);
	    if (plane.Raycast(ray, out distance))
	    {
	        cameraCorners.DownLeft = ray.GetPoint(distance);
	    }

	    ray = _camera.ViewportPointToRay(Vector3.up);
	    if (plane.Raycast(ray, out distance))
	    {
	        cameraCorners.UpLeft = ray.GetPoint(distance);
	    }

        ray = _camera.ViewportPointToRay(Vector3.right);
        if (plane.Raycast(ray, out distance))
        {
            cameraCorners.DownRight = ray.GetPoint(distance);
        }

        ray = _camera.ViewportPointToRay(Vector3.one);
        if (plane.Raycast(ray, out distance))
        {
            cameraCorners.UpRight = ray.GetPoint(distance);
        }

        //Determine with which value the camera size has to be multiplied to fit all targets in the screen
	    var scaleValues = new List<float>();
	    foreach (var target in _targetObjects)
	    {
	        var targetX = target.transform.position.x;
            var targetZ = target.transform.position.z;

            //The calculate below can be explained as follows
            //Compare the minimum and maximum values on a certain axis to the value of the target
            //determine which value the camera size has to be multiplied with to get this point with the min and max axis value of the camera
            //This is done 4 times for the min and max values of the camera for both the axis x and z
            var tempScale = ((((ScaleOffset + targetX) - cameraCorners.MaxX) * 2) + cameraCorners.GetWidth()) / cameraCorners.GetWidth();
	        scaleValues.Add(tempScale);

            tempScale = (((cameraCorners.MinX - (targetX - ScaleOffset)) * 2) + cameraCorners.GetWidth()) / cameraCorners.GetWidth();
            scaleValues.Add(tempScale);

            tempScale = (((cameraCorners.MinZ - (targetZ - ScaleOffset)) * 2) + cameraCorners.GetHeight()) / cameraCorners.GetHeight();
            scaleValues.Add(tempScale);

            tempScale = ((((targetZ + ScaleOffset) - cameraCorners.MaxZ) * 2) + cameraCorners.GetHeight()) / cameraCorners.GetHeight();
            scaleValues.Add(tempScale);
        }
        //Get the optimal determined value
	    var max = scaleValues.Max(x => x);

        //Lerp camera towards optimal screen size only within the certain threshold
	    if (max > 1f || max < (1f - Treshold))
	    {
	        var target = _camera.orthographicSize * max;
            _camera.orthographicSize = Mathf.Lerp(_camera.orthographicSize, target, ResizeSpeed*Time.deltaTime);
	    }

        //Camera can't scale below minimum size
	    if (_camera.orthographicSize < MinSize)
	    {
	        _camera.orthographicSize = MinSize;
	    }
    }

    /// <summary>
    /// Remove all empty targets
    /// </summary>
    void CleanUpObjects()
    {
        _targetObjects.RemoveAll(p => p == null);
    }

    /// <summary>
    /// Adds new target to the target list
    /// </summary>
    /// <param name="obj"></param>
    void AddObject(GameObject obj)
    {
        if (_targetObjects.Contains(obj))
            return;

        _targetObjects.Add(obj);
    }

    /// <summary>
    /// Holds information about camera corners and differences
    /// </summary>
    private class Corners
    {
        public Vector3 DownLeft;
        public Vector3 DownRight;
        public Vector3 UpLeft;
        public Vector3 UpRight;

        public float MinX
        {
            get { return Mathf.Min(DownLeft.x, UpLeft.x); }
        }

        public float MaxX
        {
            get { return Mathf.Max(DownRight.x, UpRight.x); }
        }

        public float MinZ
        {
            get { return Mathf.Min(DownLeft.z, DownRight.z); }
        }

        public float MaxZ
        {
            get { return Mathf.Max(UpLeft.z, UpRight.z); }
        }

        public float GetWidth()
        {

            return MaxX - MinX;
        }

        public float GetHeight()
        {

            return UpLeft.z - DownLeft.z;
        }

    }
}
