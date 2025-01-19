using UnityEngine;

public class TileGenerator : MonoBehaviour
{
    [SerializeField] private GameObject tileStepPrefab;
    [SerializeField] private Vector3 tileStepOffset;

    [SerializeField] private GameObject lanePrefab;
    [SerializeField] private Vector3 laneOffset;

    public void GenerateTiles(Vector3 startPosition, byte numberOfTiles)
    {
        // Instantiate a tile for every step in the lane.
        for (int i = 0; i < numberOfTiles; i++)
        {
            Vector3 position = startPosition + (Vector3.forward * i) - tileStepOffset;
            Instantiate(tileStepPrefab, position, Quaternion.identity);
        }
        
        // Create the lane object.
        GameObject lane = Instantiate(lanePrefab, startPosition - laneOffset, Quaternion.identity);
        
        // Scale it to the length of the number of tiles. 
        lane.transform.position += Vector3.forward * (numberOfTiles - 1) / 2;
        lane.transform.localScale += Vector3.forward * (numberOfTiles - 1);
    }
}