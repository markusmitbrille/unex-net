using UnityEngine;

[DisallowMultipleComponent]
public sealed class Persist : MonoBehaviour
{
    private void Start()
    {
        Savegame.Register(gameObject);
    }
}
