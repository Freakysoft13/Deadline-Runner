using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Tips : MonoBehaviour
{

    public Text tipText;
    private string[] tipsEng = new string[] { "The longer you run on one side, the harder becomes your way.", "You can find a lot of interesting things in the Upgrade Shop.", "Every ancestor from the Family Tree gives you a unique passive ability.", "Electrical obstacles will not kill you, but you cannot go to the other side for some time.", "Earn Levels to discover new Ancestors", "You can extend the duration of all PowerUP's in the Upgrade Shop.", "You can hire new characters for your journey in the Upgrade Shop.", "No matter which side you choose, remember that you are awesome!", "No matter on which side you are running as long as you are alive.", "You can not run over obstacles with spikes, try to jump over them.", "Flying wreckage will instantly kill you if you will not swap to the other side." };
    private string[] tipsUkr = new string[] { "Чим довше ви біжите на одній стороні, тим важчим стає ваш шлях.", "Ви можете знайти багато цікавого в Магазині покращень.", "Кожен родич з Дерева Предків дає вам унікальну пасивну здатність.", "Електричні перешкоди не вбиватимуть вас, але ви не зможете перейти на іншу сторону протягом певного часу.", "Заробляйте рівні, щоб відкрити нових Предків.", "Ви можете продовжити термін дії всіх здібностей в Магазині Покращень.", "Ви можете найняти нових персонажів для вашої подорожі в Магазині Покращень.", "Незалежно від того, яку сторону ви обрали, пам'ятайте, що ви дивовижні!", "Не має значення по якій стороні ви біжите, поки ви живі.", "Ви не можете пробігти через перешкоди з шипами, спробуйте перестрибнути через них.", "Летючі Уламки миттєво вас знищать, якщо ви не перейдете на іншу сторону." };
    private string[] tipsRu = new string[] { "Чем дольше вы бежите на одной стороне, тем сложнее становится ваш путь.", "Вы можете найти много интересного в Магазине улучшений.", "Каждый родственник с Дерева предков предоставляет вам уникальную пассивную способность.", "Электрические препятствия не будут убивать вас, но вы не сможете перейти на другую сторону в течение определенного времени.", "Зарабатывайте уровне, чтобы открыть новых Предков.", "Вы можете продлить срок действия всех способностей в Магазине Улучшений.", "Вы можете нанять новых персонажей для вашего путешествия в Магазине Улучшений.", "Независимо от того, какую сторону вы выбрали, помните, что вы удивительные!", "Не имеет значения по какой стороне вы бежите, пока вы живы.", "Вы не можете пробежать через препятствия с шипами, попробуйте перепрыгнуть через них.", "Летучие Обломки мгновенно вас уничтожат, если вы не пройдете на другую сторону." };

    void Start()
    {
        if(Application.systemLanguage.ToString() == "Russian")
        {
            int index = Random.Range(0, tipsRu.Length);
            tipText.text = tipsRu[index];
        }
        else if (Application.systemLanguage.ToString() == "Ukrainian")
        {
            int index = Random.Range(0, tipsUkr.Length);
            tipText.text = tipsUkr[index];
        }
        else
        {
            int index = Random.Range(0, tipsEng.Length);
            tipText.text = tipsEng[index];
        }

    }
}
