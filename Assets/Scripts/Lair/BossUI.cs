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
        public int maxHealth;

        public void UpdateHealth(float h)
        {
            Debug.Log("fill=" + h/maxHealth);
            fill.fillAmount = h / maxHealth;
        }
        
    }
}
