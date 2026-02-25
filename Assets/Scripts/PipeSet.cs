using UnityEngine;

public class PipeSet : MonoBehaviour
{
    public void ResetPos()
    {
        foreach (Transform child in transform)
        {
            Pipes pipe = child.GetComponent<Pipes>();
            if (pipe != null)
            {
                pipe.InitialPosition();
            }
        }
    }

    public Transform GetNextPipe()
    {
        float leftMost = float.MaxValue;
        Transform leftChild = null;
        
        foreach (Transform child in transform)
        {
            float childX = child.localPosition.x;
            if (childX < leftMost && childX > -0.3f)
            {
                leftChild = child;
                leftMost = childX;
            }
        }
        
        return leftChild;
    }
}