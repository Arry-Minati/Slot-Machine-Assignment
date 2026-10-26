
using UnityEngine;
using System.Collections;

public class SlotLever : MonoBehaviour
{
    public GameObject normalLever;
    public GameObject pulledLever;
    public SlotMachineController slotMachine;

    public float pushedDuration = .25f;

    private bool isAnimating;

    public void OnLeverClicked()
    {
        Debug.Log("Lever clicked!");
        if (isAnimating || slotMachine == null)
        {
            return;
        }

        if (!slotMachine.HasSelectedBet)
        {
            Debug.Log("select a bet first");
            return;
        }

        StartCoroutine(PullLever());
    }

    private IEnumerator PullLever()
    {
        Debug.Log("1. Lever animation started");
        isAnimating = true;

        normalLever.SetActive(false);
        pulledLever.SetActive(true);

        // Spin the reels
        Debug.Log("2. Pulled sprite activated");
        slotMachine.Spin();

        Debug.Log("3. Spin called; waiting 1 second");
        yield return new WaitForSeconds(1f);

        Debug.Log("4. One second passed");
        // Return to the normal lever
        pulledLever.SetActive(false);
        normalLever.SetActive(true);

        isAnimating = false;
        Debug.Log("lever returned to normal position");
    }
}
