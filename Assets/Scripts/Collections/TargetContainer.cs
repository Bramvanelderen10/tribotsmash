using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tribot;
using UnityEngine;

public class TargetContainer : Singleton<TargetContainer>
{
    public List<GameObject> Players = new List<GameObject>();
    public List<GameObject> Pickups = new List<GameObject>();
    public List<GameObject> ControlPoints = new List<GameObject>();
    public List<GameObject> Projectiles = new List<GameObject>();
    public List<GameObject> Meteors = new List<GameObject>();
}

