using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoftBody2D : MonoBehaviour
{
    private Mesh mesh;
    private Vector3[] vertices;
    private int centerPoint = 32;
    private int verticesCount;

    private List<GameObject> points;
    [SerializeField] private GameObject toBeInstantiated = default;

    private void Awake()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        points = new List<GameObject>();
        vertices = mesh.vertices;
        verticesCount = vertices.Length;

        for(int i=0; i<vertices.Length; i++)
        {
            GameObject childObject = Instantiate(toBeInstantiated, transform.position + vertices[i], Quaternion.identity);
            childObject.transform.SetParent(transform);
            points.Add(childObject);
        }

        transform.Rotate(0f, 180f, 0f);

        for(int i=0; i<points.Count; i++)
        {
            if(i != centerPoint)
            {
                if(i == points.Count - 1)
                {
                    points[i].GetComponent<HingeJoint2D>().connectedBody = points[0].GetComponent<Rigidbody2D>();
                }

                else
                {
                    points[i].GetComponent<HingeJoint2D>().connectedBody = points[i + 1].GetComponent<Rigidbody2D>();
                }
            }
        }
    }

    private void Update()
    {
        if (GameManager.SI.currentGameState != GameState.InGame)
        {
            return;
        }

        for (int i=0; i<vertices.Length; i++)
        {
            vertices[i] = points[i].transform.localPosition;
        }
        mesh.vertices = vertices;
    }
}
