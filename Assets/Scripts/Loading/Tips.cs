using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Tips : MonoBehaviour
{

    public Text tipText;
    private string[] tips = new string[] { "The longer you run on one side, the harder becomes your way.", "You can find a lot of interesting things in the Upgrade Shop.", "Every ancestor from the Family Tree gives you a unique passive ability.", "Electrical obstacles will not kill you, but you can not go to the other side for some time.", "Every new level you get will unlock ancestor in the Family Tree.", "You can extend the duration of all PowerUP's in the Upgrade Shop.", "You can hire new characters for your journey in the Upgrade Shop.", "No matter which side you choose, remember that you are awesome!", "No matter on which side you are running as long as you are alive.", "You can not run over obstacles with spikes, try to jump over them." };

    void Start()
    {
        int index = Random.Range(0, tips.Length);
        tipText.text = tips[index];
    }
}
