using UnityEngine;
using UnityEngine.UI;

public class RandomTargetGenerator : MonoBehaviour
{
    public GameObject targetPrefab; // Assign the target prefab in the Inspector
    public Vector3 spawnAreaSize = new Vector3(10f, 1f, 10f); // Size of the spawn area (X, Y, Z)
    public int initialTargetCount = 10; // Number of targets to generate initially
    public int targetsToRegenerate = 5; // Number of targets to regenerate after destruction
    public Text targetCounterText; // Assign a UI Text element to display the counter

    private int targetsRemaining; // Tracks how many targets are currently in the scene
    private int totalDestroyed; // Tracks the total number of targets destroyed

    void Start()
    {
        targetsRemaining = initialTargetCount;
        totalDestroyed = 0;
        UpdateCounter();
        GenerateTargets(initialTargetCount);
    }

    void GenerateTargets(int count)
    {
        for (int i = 0; i < count; i++)
        {
            // Generate a random position within the spawn area
            Vector3 randomPosition = new Vector3(
                Random.Range(-spawnAreaSize.x / 2, spawnAreaSize.x / 2),
                Random.Range(0, spawnAreaSize.y), // Adjust Y range if needed
                Random.Range(-spawnAreaSize.z / 2, spawnAreaSize.z / 2)
            );

            // Spawn the target
            GameObject newTarget = Instantiate(targetPrefab, randomPosition, Quaternion.identity);

            // Ensure the target has a tag "Target"
            newTarget.tag = "Target";

            // Attach a destroy callback to update the counter
            Target targetScript = newTarget.GetComponent<Target>();
            if (targetScript != null)
            {
                targetScript.OnTargetDestroyed += HandleTargetDestroyed;
            }
        }
    }

    void HandleTargetDestroyed()
    {
        targetsRemaining--;
        totalDestroyed++;
        UpdateCounter();

        // Regenerate targets if the specified number of targets has been destroyed
        if (totalDestroyed % targetsToRegenerate == 0)
        {
            GenerateTargets(targetsToRegenerate);
            targetsRemaining += targetsToRegenerate;
        }
    }

    void UpdateCounter()
    {
        if (targetCounterText != null)
        {
            targetCounterText.text = "Targets Remaining: " + targetsRemaining;
        }
    }
}
