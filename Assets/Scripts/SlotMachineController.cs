using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;

public class SlotMachineController : MonoBehaviour
{
    //STORING THE REEL SYMBOLS
   [Header("Reel Symbols")]
   public SpriteRenderer[] reelSymbols;     //left[0], middle[1], right[2] of a Symbol
   public Sprite[] availableSymbols;        // Symbol[0], Symbol[1]...

    // CONTROLS SPEED OF SYMBOLS
   [Header("Spin Settings")]
   public float spinDuration = 2f;
   public float symbolChangeInterval = 0.08f;

   private bool isSpinning;


   private void Update()
    {
        if (Keyboard.current != null &&
            Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            Spin();
        }
    }

    public void Spin()
    {
        if (isSpinning){
            return;
        }

        StartCoroutine(SpinReels());
    }

    private IEnumerator SpinReels()
    {
        isSpinning = true;

        for (int reel = 0; reel < reelSymbols.Length; reel++)
        {
            StartCoroutine(SpinSingleReel(reel));
        }

        yield return new WaitForSeconds(spinDuration);

        isSpinning = false;
        CheckWin();
    }

    private IEnumerator SpinSingleReel(int reelIndex)
    {
        float elapsed = 0f;

        while (elapsed < spinDuration)
        {
            int randomIndex = Random.Range(0, availableSymbols.Length);
            reelSymbols[reelIndex].sprite = availableSymbols[randomIndex];

            yield return new WaitForSeconds(symbolChangeInterval);
            elapsed += symbolChangeInterval;
        }

        int finalIndex = Random.Range(0, availableSymbols.Length);
        reelSymbols[reelIndex].sprite = availableSymbols[finalIndex];
    }

    private void CheckWin()
    {
        if (reelSymbols.Length < 3)
        {
            return;
        }
        if (reelSymbols[0].sprite == reelSymbols[1].sprite && 
            reelSymbols[1].sprite == reelSymbols[2].sprite)
        {
            Debug.Log("Win! All three symbols match!");
        }
        else
        {
            Debug.Log("No win. Try again!");
        }
    }
}
