using UnityEngine;

public class WeaponSwitching : MonoBehaviour
{

    public int selectedWeapon = 0;
    int previousWeapon;
    // Start is called before the first frame update
    void Start()
    {
        SelectWeapon();
    }

    public void SetIndex(int index)
    {
        selectedWeapon = index;
    }

    // Update is called once per frame
    void Update()
    {
        if (previousWeapon != selectedWeapon)
        {
            SelectWeapon();
        }
        previousWeapon = selectedWeapon;
    }

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
