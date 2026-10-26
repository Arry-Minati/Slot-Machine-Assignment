
using UnityEngine;

public class BettingMenuController : MonoBehaviour
{
    [Header("Popup")]
    public GameObject betPopup;

    [Header("Slot Machine")]
    public SlotMachineController slotMachine;

    private void Start()
    {
        if (betPopup != null)
            betPopup.SetActive(false);
    }

    public void ToggleBetPopup()
    {
        if (betPopup != null)
            betPopup.SetActive(!betPopup.activeSelf);
    }

    public void CloseBetPopup()
    {
        if (betPopup != null)
            betPopup.SetActive(false);
    }

    public void SetBet10()
    {
        slotMachine.SetBet(10);
        CloseBetPopup();
    }

    public void SetBet50()
    {
        slotMachine.SetBet(50);
        CloseBetPopup();
    }

    public void SetBet100()
    {
        slotMachine.SetBet(100);
        CloseBetPopup();
    }
}
