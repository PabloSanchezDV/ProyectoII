using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCT : NotebookCursorTarget
{
    [SerializeField] private GameObject _pinPrefab;
    [NonSerialized] public PostItCT postIt;
    private List<GameObject> _pinsList = new List<GameObject>();

    public override void PrimaryPressed(Vector3 pressedWorldPosition)
    {
        PlacePin(pressedWorldPosition);
    }

    public void PlacePin(Vector3 position)
    {
        GameObject newPin = Instantiate(_pinPrefab, position, Quaternion.identity);
        newPin.transform.parent = transform;
        newPin.transform.localRotation = Quaternion.Euler(-90f, 0f, 0f);
        newPin.transform.localScale = Vector3.one / 2;
        newPin.GetComponent<PinCT>().postIt = postIt;
        newPin.GetComponent<PinCT>().mapCT = this;
        _pinsList.Add(newPin);
    }

    public void ShowPins()
    {
        foreach (GameObject pin in _pinsList)
            pin.SetActive(true);
    }

    public void HideMapElements()
    {
        if(postIt != null)
        {
            if(postIt.gameObject != null)
            {
                postIt.SaveText();
                postIt.gameObject.SetActive(false);
            }
        }
        foreach (GameObject pin in _pinsList)
            pin.SetActive(false);
    }

    public void RemovePinFromList(GameObject pin)
    {
        _pinsList.Remove(pin);
    }
}
