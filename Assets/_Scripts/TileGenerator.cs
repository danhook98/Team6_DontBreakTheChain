using UnityEngine;

public class TileGenerator : MonoBehaviour
{
    [SerializeField] private GameObject tileStepPrefab;
    [SerializeField] private Vector3 tileStepOffset;

    [SerializeField] private GameObject lanePrefab;
    [SerializeField] private Vector3 laneOffset;

    [SerializeField] private GameObject laneBasePrefab;
    [SerializeField] private Vector3 laneBaseOffset;

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

    public void GenerateBase(Vector3 position, byte numberOfTiles)
    {
        GameObject laneBase = Instantiate(laneBasePrefab, position - laneBaseOffset, Quaternion.identity);
        
        laneBase.transform.position += Vector3.forward * (numberOfTiles + 1) / 2;
        laneBase.transform.localScale += Vector3.forward * (numberOfTiles + 1);
    }
}