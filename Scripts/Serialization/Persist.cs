using UnityEngine;

[DisallowMultipleComponent]
public sealed class Persist : MonoBehaviour
{
    private void Start()
    {
        Monoton<Savegame>.Instance.Items.Add(gameObject);
    }
}
