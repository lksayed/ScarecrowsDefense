using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAppearance : MonoBehaviour
{
    // Array of sprite frames
    [SerializeField] Sprite[] enemySprites;
    // The current sprite
    private int currentSprite = 0;
    // Count of how many sprites are in the array
    private int spriteCount;
    // Time it takes for the enemey to change appearance
    private float swapTime = 0.5f;
    // Time since the enemy last changed appearance
    private float timeSinceLastSwap;

    // Start is called before the first frame update
    void Start()
    {
        // On start, assign the first array item as the starting sprite
        gameObject.GetComponent<SpriteRenderer>().sprite = enemySprites[currentSprite];
        // Assign the value fro spriteCount
        spriteCount = enemySprites.Length;
    }

    // Update is called once per frame
    void Update()
    {
        // Increase timer 
        timeSinceLastSwap += Time.deltaTime;

        // Check if it is time to swap the sprite frame
        if (timeSinceLastSwap > swapTime)
        {   
            // If it isn't the last frame, load the next frame in the array
            if (currentSprite < spriteCount - 1)
            {
                currentSprite++;
                gameObject.GetComponent<SpriteRenderer>().sprite = enemySprites[currentSprite];
                timeSinceLastSwap = 0f;
            }
            // If it is the last frame, load the first frame in the array
            else 
            {
                currentSprite = 0;
                gameObject.GetComponent<SpriteRenderer>().sprite = enemySprites[currentSprite];
                timeSinceLastSwap = 0f;
            }
        }
    }
}
