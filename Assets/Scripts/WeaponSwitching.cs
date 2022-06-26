using UnityEngine;

/**
 * Script which handles switching of weapons, enables or disables child objects
 */
public class WeaponSwitching : MonoBehaviour
{
    // Variable to store index value of selected weapon
    public int selectedWeapon = 0;

    // Variable to store index value of previously selected weapon
    int previousWeapon;

    // Start is called before the first frame update
    void Start()
    {
        SelectWeapon();
    }

    // Setting the index in input manager
    public void SetIndex(int index)
    {
        selectedWeapon = index;
    }

    // Calls selection of weapon
    void Update()
    {
        if (previousWeapon != selectedWeapon)
        {
            SelectWeapon();
        }
        previousWeapon = selectedWeapon;
    }

    // Processes selection of weapon
    void SelectWeapon()
    {
        int i = 0;
        foreach (Transform weapon in transform)
        {
            if (i == selectedWeapon)
            {
                weapon.gameObject.SetActive(true);
            }
            else
            {
                weapon.gameObject.SetActive(false);
            }
            i++;
        }
    }
}
