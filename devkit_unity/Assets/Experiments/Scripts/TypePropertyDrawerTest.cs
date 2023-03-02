using System;
using UnityEngine;

public class TypePropertyDrawerTest : MonoBehaviour
{
    [SerializeField]
    private TypeSelector Test = new TypeSelector();

    private void OnEnable()
    {
        Debug.LogWarningFormat("Test: {0}", Test);
    }
}
