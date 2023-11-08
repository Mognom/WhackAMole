using System;
using UnityEngine;

[CreateAssetMenu]
public class EventChannel : ScriptableObject {
    public event Action channel;

    public void PostEvent() {
        channel?.Invoke();
    }
}
