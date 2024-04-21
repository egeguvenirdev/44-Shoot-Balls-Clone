using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class PathManager : MonoBehaviour
{
    [SerializeField] private GameObject[] pathList;
    private int i;

    // Start is called before the first frame update
    void Start()
    {
        i = 0;
    }

    public PathCreator ReturnCurrenntRoad()
    {
        return pathList[i].GetComponent<PathCreator>();
        i++;
    }
}
