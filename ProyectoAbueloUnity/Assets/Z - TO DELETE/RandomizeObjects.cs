using System.Collections;
using System.Collections.Generic;
using PathCreation;
using UnityEngine;

public class RandomizeObjects : MonoBehaviour
{
    [SerializeField] private Transform[] transforms;
    [SerializeField] private float minHeight;
    [SerializeField] private float maxHeight;

    public void EditTransforms()
    {
        foreach (Transform t in transforms)
        {
            t.localScale = new Vector3(t.localScale.x, t.localScale.y, Random.Range(minHeight, maxHeight));
            t.localRotation = Quaternion.Euler(Vector3.forward * Random.Range(0, 360) + Vector3.right * -90f);
            #if UNITY_EDITOR
                    UnityEditor.EditorUtility.SetDirty(t);
            #endif
        }

#if UNITY_EDITOR
        UnityEditor.SceneManagement.EditorSceneManager.MarkSceneDirty(this.gameObject.scene);
#endif
    }
}
