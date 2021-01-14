using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Lair
{
    public class BossUI : MonoBehaviour
    {
        public TextMeshProUGUI health;
        public Image fill;
        public float maxHealth;

        public void UpdateHealth(float h)
        {
            fill.fillAmount = h / maxHealth;
        }
        
    }
}
