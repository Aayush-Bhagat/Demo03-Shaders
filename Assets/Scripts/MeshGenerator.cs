using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshGenerator : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<MeshFilter>().mesh = GenerateCube(false);
    }

    // Vertices for a unit cube from (0, 0, 0) to (1, 1, 1)
    public static readonly Vector3[] Vertices =
    {
        new Vector3(0, 0, 0), // 0
        new Vector3(1, 0, 0), // 1
        new Vector3(1, 1, 0), // 2
        new Vector3(0, 1, 0), // 3
        new Vector3(0, 1, 1), // 4
        new Vector3(1, 1, 1), // 5
        new Vector3(1, 0, 1), // 6
        new Vector3(0, 0, 1), // 7
    };

    // Indices creating triangles with *clockwise* winding order (yes I messed up the singular most important fact in
    // the presentation)
    // 6 quad faces * 2 tris per face = 12 faces
    public static readonly int[] Indices =
    {
        0, 2, 1,
        0, 3, 2,
        
        2, 3, 4,
        2, 4, 5,
        
        1, 2, 5,
        1, 5, 6,
        
        0, 7, 4,
        0, 4, 3,
        
        5, 4, 7,
        5, 7, 6,
        
        0, 6, 7,
        0, 1, 6
    };

    // Function that creates a Mesh with the new data
    public static Mesh GenerateCube(bool hardEdges)
    {
        var mesh = new Mesh();
        Vector3[] vertices;
        int[] indices;
        
        if (hardEdges)
        {
            vertices = new Vector3[Indices.Length];
            indices = new int[Indices.Length];
            for (int i = 0; i < Indices.Length; i++) {
                vertices[i] = Vertices[Indices[i]];
                indices[i] = i;
            }
        }
        else
        {
            vertices = Vertices;
            indices = Indices;
        }
        
        mesh.SetVertices(vertices);
        mesh.SetTriangles(indices, 0); // or mesh.SetIndices(indices, MeshTopology.Triangles, 0);
        mesh.Optimize();
        mesh.RecalculateNormals();

        return mesh;
    }
}