using System;
using UnityEngine;

public class SaveEntityLocation : MonoBehaviour, ISaveable
{
    [SerializeField] float eX;
    [SerializeField] float eY;
    [SerializeField] float eZ;

    [SerializeField] float rX;
    [SerializeField] float rY;
    [SerializeField] float rZ;

    private void SetTransform()
    {
        transform.SetPositionAndRotation(new Vector3(eX, eY, eZ), Quaternion.Euler(rX, rY, rZ));
    }

    public object CaptureState()
    {
        eX = transform.position.x;
        eY = transform.position.y;
        eZ = transform.position.z;

        rX = transform.rotation.x;
        rY = transform.rotation.y;
        rZ = transform.rotation.z;

        return new SaveData
        {
            savedX = eX,
            savedY = eY,
            savedZ = eZ
        };
    }

    public void RestoreState(object state)
    {
        var saveData = (SaveData)state;

        eX = saveData.savedX;
        eY = saveData.savedY;
        eZ = saveData.savedZ;

        rX = saveData.savedrotX;
        rY = saveData.savedrotY;
        rZ = saveData.savedrotZ;

        SetTransform();
    }

    [Serializable]
    private struct SaveData
    {
        public float savedX;
        public float savedY;
        public float savedZ;
        public float savedrotX;
        public float savedrotY;
        public float savedrotZ;
    }

}
