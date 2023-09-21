using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TODO : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var todos = new List<string>();
        todos.Add("Добавить управление от первого лица: повороты камеры, прыжки, ускорение");
        todos.Add("Главная задача сделать красивый высокопроизводительный бенчмарк для планшетов, телефонов и компов");
        todos.Add("Зайти в suburb и вырезать оттуда управление жестами");
        todos.Add("Добавить к управлению жестами контроллеры, захват кнопок");

        foreach(var todo in todos)
            Debug.Log(todo);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
