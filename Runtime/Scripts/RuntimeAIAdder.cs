using UnityEngine;
using Apex.AI.Components;

[RequireComponent(typeof(UtilityAIComponent))]
public class RuntimeAIAdder : MonoBehaviour
{
    public string[] aiIds = new string[0];

    private void Start()
    {
        var comp = GetComponent<UtilityAIComponent>();

        for (int i = 0; i < aiIds.Length; i++) {
            if (!string.IsNullOrEmpty(aiIds[i])) {
                comp.AddClient(aiIds[i]);
            }
        }
    }
}

