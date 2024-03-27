    using UnityEngine;

    namespace NeoFortaleza.Runtime.Systems.Behaviors
    {
        public class WeaponController : MonoBehaviour
        {
            [SerializeField] public Transform cellHand;

            public Weapon startingWeapon;
            Weapon equippedWeapon;

            void Start()
            {
                if (startingWeapon != null)
                {
                    EquipWeapon(startingWeapon);
                }
            }

            public void EquipWeapon(Weapon weaponToEquip)
            {
                if (equippedWeapon != null)
                {
                    Destroy(equippedWeapon.gameObject);
                }
                equippedWeapon = Instantiate(weaponToEquip, cellHand.position, cellHand.rotation);
                equippedWeapon.transform.parent = cellHand;
            }
        }
    }