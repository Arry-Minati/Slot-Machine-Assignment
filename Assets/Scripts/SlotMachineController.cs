using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;
using TMPro;


public class SlotMachineController : MonoBehaviour
{
    // STORING THE REEL SYMBOLS
   [Header("Reel Symbols")]
   public SpriteRenderer[] reelSymbols;     //left[0], middle[1], right[2] of a Symbol
   public Sprite[] availableSymbols;        // Symbol[0], Symbol[1]...

    // CONTROLS SPEED OF SYMBOLS
   [Header("Spin Settings")]
   public float spinDuration = 2f;
   public float symbolChangeInterval = 0.08f;

    // PAYOUT SETTINGS
    [Header("Payout Settings")]
    public int startingCredits = 100;
    public int spinCost = 10;
    public int winPayout = 50;

    // UI
    [Header("UI")]
    public TextMeshProUGUI resultText;

    private int credits;
    private bool isSpinning;


    private void Start()
    {
        // Initialize the player starting credits
        credits = startingCredits;
        
        UpdateResultText("Press SPIN to play!\nCredits: " + credits);
    }


   private void Update()
    {   
        // Temp keyboard control for testing
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

        // check that the reels and symbols are assigned
        if (reelSymbols == null || reelSymbols.Length < 3 || 
            availableSymbols == null || availableSymbols.Length == 0)
        {
            UpdateResultText("Game setup error!");
            return;
        }

        // check if player has enough credits to spin
        if (credits < spinCost)
        {
            UpdateResultText("Not enough credits!");
            return;
        }
        
        // Deduct the cost before spinning
        credits -= spinCost;
        StartCoroutine(SpinReels());
    }

    private IEnumerator SpinReels()
    {
        isSpinning = true;
        UpdateResultText("Spinning...");

        // start each reel spinning independently
        for (int reel = 0; reel < reelSymbols.Length; reel++)
        {
            StartCoroutine(SpinSingleReel(reel));
        }

        // wait for the reels to finish changing symbols
        yield return new WaitForSeconds(spinDuration + symbolChangeInterval);

        isSpinning = false;
        // check the final symbols after reels stop
        CheckWin();
    }

    private IEnumerator SpinSingleReel(int reelIndex)
    {
        float elapsed = 0f;

        // randomly change the symbol at regular intervals
        while (elapsed < spinDuration)
        {
            int randomIndex = Random.Range(0, availableSymbols.Length);
            reelSymbols[reelIndex].sprite = availableSymbols[randomIndex];

            yield return new WaitForSeconds(symbolChangeInterval);
            elapsed += symbolChangeInterval;
        }

        // choose the final symbol for this reel
        int finalIndex = Random.Range(0, availableSymbols.Length);
        reelSymbols[reelIndex].sprite = availableSymbols[finalIndex];
    }

    private void CheckWin()
    {
        // player wins when all symbols match
        if (reelSymbols[0].sprite == reelSymbols[1].sprite && 
            reelSymbols[1].sprite == reelSymbols[2].sprite)
        {
            // Awward winning payout
            credits += winPayout;

            UpdateResultText($"YOU WIN {winPayout} credits\nBalance: {credits}");
            Debug.Log("Win! all 3 symbols match!");
        }
        else
        {
            UpdateResultText($"No win. Try again!\n {credits}");
            Debug.Log("No win. Try again!");
        }
    }

    private void UpdateResultText(string message)
    {
        // Update the on-screen result if the UI is assigned
        if (resultText != null)
        {
            resultText.text = message;
        }
    }
}
