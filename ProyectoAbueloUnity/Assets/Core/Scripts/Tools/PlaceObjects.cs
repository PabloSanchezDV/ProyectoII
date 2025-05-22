using System.Collections;
using System.Collections.Generic;
using PathCreation;
using System.ComponentModel;
using UnityEngine;

public class PlaceObjects : MonoBehaviour
{
    [SerializeField] private GameObject prefabToPlace;
    [SerializeField] private Transform parent;
    [SerializeField] private int amount;
    [SerializeField] private float positiveX;
    [SerializeField] private float negativeX;
    [SerializeField] private float positiveZ;
    [SerializeField] private float negativeZ;
    [SerializeField] private float height;
    [SerializeField] private LayerMask terrainLayerMask;

    public void Place()
    {
        Transform[] tArray = new Transform[amount];

        for (int i = 0; i < amount; i++)
        {
            Vector3 randomPosition = new Vector3(Random.Range(negativeX, positiveX), 200f, Random.Range(negativeZ, positiveZ));
            Quaternion randomRotation = Quaternion.Euler(Vector3.forward * Random.Range(0, 360) + Vector3.right * -90);
            tArray[i] = Instantiate(prefabToPlace, randomPosition, randomRotation, parent).transform;
        }

        for(int i = 0; i < tArray.Length; i++)
        {
            if (Physics.Raycast(tArray[i].position, Vector3.down, out RaycastHit hitInfo, Mathf.Infinity, terrainLayerMask))
            {
                Vector3 snappedWorldPos = hitInfo.point + Vector3.up * height;
                tArray[i].position = snappedWorldPos;
            }
            else
            {
                throw new WarningException($"Object {tArray[i].name} has not found terrain under it.");
            }
        }
    }
}
