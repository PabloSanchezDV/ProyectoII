using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCT : NotebookCursorTarget
{
    [SerializeField] private GameObject _pinPrefab;
    [NonSerialized] public PostItCT postIt;
    private List<GameObject> _pinsList = new List<GameObject>();

    private void Start()
    {
        StartCoroutine(WaitUntilGameManagerHasLoadedToLoadPins());
    }

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
        newPin.GetComponent<PinCT>().localPosition = newPin.transform.localPosition;
        _pinsList.Add(newPin);
        AudioManager.Instance.PlayPushpinSound(newPin);
        SavePinsListInGameManager(newPin, false);
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
        SavePinsListInGameManager(pin, true);
    }

    private void SavePinsListInGameManager(GameObject pin, bool isRemoving)
    {
        if(GameManager.Instance.PinsList == null)
        {
            List<PinCT> pins = new List<PinCT>();
            foreach(GameObject pinGO in _pinsList)
                pins.Add(pinGO.GetComponent<PinCT>());

            GameManager.Instance.PinsList = pins;
        }
        else
        {
            if(isRemoving)
                GameManager.Instance.PinsList.Remove(pin.GetComponent<PinCT>());
            else
                GameManager.Instance.PinsList.Add(pin.GetComponent<PinCT>());
        }
    }

    public void LoadPins()
    {
        foreach (PinCT pin in GameManager.Instance.PinsList)
            PlacePin(pin.localPosition, pin.text);
    }

    public void PlacePin(Vector3 localPosition, string text)
    {
        GameObject newPin = Instantiate(_pinPrefab, transform);

        newPin.transform.localRotation = Quaternion.Euler(-90f, 0f, 0f);
        newPin.transform.localScale = Vector3.one / 2;
        newPin.transform.localPosition = localPosition;

        newPin.GetComponent<PinCT>().postIt = postIt;
        newPin.GetComponent<PinCT>().mapCT = this;
        newPin.GetComponent<PinCT>().text = text;
        newPin.GetComponent<PinCT>().localPosition = newPin.transform.localPosition;

        _pinsList.Add(newPin);

        newPin.gameObject.SetActive(false);
    }

    IEnumerator WaitUntilGameManagerHasLoadedToLoadPins()
    {
        yield return new WaitUntil(() => GameManager.Instance.HasLoadedData);

        foreach(GameObject pin in _pinsList)
        {
            Destroy(gameObject);
        }
        _pinsList.Clear();

        LoadPins();
    }
}
