using UnityEngine;

public class SpawnTargets : MonoBehaviour
{
    public GameObject itemOnePrefab;
    public GameObject itemTwoPrefab;
    public GameObject itemThreePrefab;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ITargetBuilder targetBuilder = new CreateTargetBuilder();
        TargetDirector targetDirector = new TargetDirector();

        Target targetOne = targetDirector.ConstructTargetOne(targetBuilder, itemOnePrefab);

        ITargetBuilder target2Builder = new CreateTargetBuilder();
        Target targetTwo = targetDirector.ConstructTargetTwo(targetBuilder, itemTwoPrefab);

        ITargetBuilder target3Builder = new CreateTargetBuilder();
        Target targetThree = targetDirector.ConstructTargetThree(targetBuilder, itemThreePrefab);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
