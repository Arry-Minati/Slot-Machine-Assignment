using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;
using TMPro;




public class SlotMachineController : MonoBehaviour
{
    // STORING THE REEL SYMBOLS
   [Header("Reel Symbols")]
   public SpriteRenderer[] reelSymbols;     //left[0], middle[1], right[2] of a Symbol Center
   public SpriteRenderer[] topSymbols;      //left[0], middle[1], right[2] of a Symbol Top
   public SpriteRenderer[] bottomSymbols;      //left[0], middle[1], right[2] of a Symbol Bottom
   public Sprite[] availableSymbols;        // Symbol[0], Symbol[1]...

    // CONTROLS SPEED OF SYMBOLS
   [Header("Spin Settings")]
   public float spinDuration = 1.5f;
   public float symbolChangeInterval = 0.08f;
   //public float symbolSpacing = 1.5f;

    // PAYOUT SETTINGS
    [Header("Payout Settings")]
    public int startingCredits = 100;
    public int spinCost = 10;
    public int winPayout = 50;
    private int selectedBet = 10;
    private bool hasSelectedBet = false;
    public bool HasSelectedBet => hasSelectedBet;

    // UI
    [Header("UI")]
    public TextMeshProUGUI resultText;

    private int credits;
    private bool isSpinning;

    private void Awake()
    {
        Application.targetFrameRate = 60;
        QualitySettings.vSyncCount = 0;
    }

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
            topSymbols == null ||  topSymbols.Length < 3 ||
            bottomSymbols == null ||  bottomSymbols.Length < 3 ||
            availableSymbols == null || availableSymbols.Length == 0)
        {
            UpdateResultText("Game setup error!");
            return;
        }

        if (!hasSelectedBet)
        {
            UpdateResultText("Select a bet first!");
            return;
        }

        // check if player has enough credits to spin
        if (credits < spinCost)
        {
            UpdateResultText("Not enough credits!");
            return;
        }

        // require a new section for the next spin
        hasSelectedBet = false;
        
        // Deduct the cost before spinning
        credits -= spinCost;
        StartCoroutine(SpinReels());
    }

    private IEnumerator SpinReels()
    {
        isSpinning = true;
        UpdateResultText("Spinning...");

        // all reels starts together, stops at diffrent times
        Coroutine leftReel = StartCoroutine(SpinSingleReel(0, spinDuration));
        Coroutine middleReel = StartCoroutine(SpinSingleReel(1, spinDuration + 0.5f));
        Coroutine rightReel = StartCoroutine(SpinSingleReel(2, spinDuration + 1.0f));

        // wait until every reel has stopped
        yield return leftReel;
        yield return middleReel;
        yield return rightReel;

        isSpinning = false;
        // check the final symbols after reels stop
        CheckWin();
    }

    private IEnumerator SpinSingleReel(int reelIndex, float duration)
    {
        if(availableSymbols.Length < 4)
        {
            Debug.LogError("Assign four symbols in available symbols!");
            yield break;
        }

        // choose random starting point in the 4 symbols cycle
        int currentIndex = Random.Range(0, availableSymbols.Length);

            // change top, mid, bottom symbols in sequence
            topSymbols[reelIndex].sprite = availableSymbols[currentIndex];
            reelSymbols[reelIndex].sprite = availableSymbols[(currentIndex + 1) % availableSymbols.Length];
            bottomSymbols[reelIndex].sprite = availableSymbols[(currentIndex + 2) % availableSymbols.Length];

        float elapsed = 0f;
        float stepInterval = Mathf.Max(0.03f, symbolChangeInterval);

        while (elapsed < duration)
        {
            yield return new WaitForSeconds(stepInterval);
            elapsed += stepInterval;

            // advance the sequence by one symbol
            currentIndex = (currentIndex + 1) % availableSymbols.Length;

            topSymbols[reelIndex].sprite = availableSymbols[currentIndex];
            reelSymbols[reelIndex].sprite = availableSymbols[(currentIndex + 1) % availableSymbols.Length];
            bottomSymbols[reelIndex].sprite = availableSymbols[(currentIndex + 2) % availableSymbols.Length];
        }

        Debug.Log($"Reel {reelIndex + 1} stopped.");
    }


     private Sprite RandomSymbol()
    {
        int index = Random.Range(0, availableSymbols.Length);
        return availableSymbols[index];
    }

    public void SetBet(int amount)
    {
        if (isSpinning)
            return;

        if (amount != 10 && amount != 50 && amount != 100)
            return;

        selectedBet = amount;
        hasSelectedBet = true;
        spinCost = selectedBet;
        winPayout = selectedBet * 5;

        UpdateResultText($"Bet: {selectedBet}G\nCredits: {credits}");
        Debug.Log("Bet selected: " + selectedBet);
    }

    private void CheckWin()
    {
        Debug.Log("Left: " + reelSymbols[0].sprite.name);
        Debug.Log("Middle: " + reelSymbols[1].sprite.name);
        Debug.Log("Right: " + reelSymbols[2].sprite.name);

        // player wins when all symbols match
        if (reelSymbols[0].sprite.name == reelSymbols[1].sprite.name && 
            reelSymbols[1].sprite.name == reelSymbols[2].sprite.name)
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
