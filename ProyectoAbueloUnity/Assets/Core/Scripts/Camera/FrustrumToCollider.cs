using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class FrustrumToCollider
{
    public static void ApplyFrustumCollider(Camera camera, MeshCollider meshCollider)
    {
        if (meshCollider == null)
            throw new System.Exception("Camera is missing MeshCollider component. It won't detect any object in the scene.");

        Vector3[] corners = GetFrustumCorners(camera);
        Mesh frustumMesh = BuildFrustumMesh(corners);

        meshCollider.sharedMesh = null;
        meshCollider.sharedMesh = frustumMesh;

        meshCollider.transform.position = camera.transform.position;
        meshCollider.transform.rotation = camera.transform.rotation;
        meshCollider.transform.localScale = Vector3.one;

        meshCollider.convex = true;
        meshCollider.isTrigger = true;
    }
    
    private static Vector3[] GetFrustumCorners(Camera camera)
    {
        Vector3[] corners = new Vector3[8];

        // Near plane
        Vector3[] nearCorners = new Vector3[4];
        camera.CalculateFrustumCorners(new Rect(0, 0, 1, 1), camera.nearClipPlane, Camera.MonoOrStereoscopicEye.Mono, nearCorners);
        for (int i = 0; i < 4; i++)
            corners[i] = nearCorners[i];

        // Far plane
        Vector3[] farCorners = new Vector3[4];
        camera.CalculateFrustumCorners(new Rect(0, 0, 1, 1), camera.farClipPlane, Camera.MonoOrStereoscopicEye.Mono, farCorners);
        for (int i = 0; i < 4; i++)
            corners[i + 4] = farCorners[i];

        return corners;
    }

    private static Mesh BuildFrustumMesh(Vector3[] corners)
    {
        Mesh mesh = new Mesh();
        mesh.vertices = corners;

        mesh.triangles = new int[]
        {
            // Near plane
            0, 1, 2, 2, 3, 0,

            // Far plane
            4, 6, 5, 6, 4, 7,

            // Sides
            0, 3, 7, 7, 4, 0,
            1, 5, 6, 6, 2, 1,
            0, 4, 5, 5, 1, 0,
            2, 6, 7, 7, 3, 2
        };

        mesh.RecalculateNormals();
        mesh.RecalculateBounds();

        return mesh;
    }
}
