using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TODO : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var todos = new List<string>();
        todos.Add("�������� ���������� �� ������� ����: �������� ������, ������, ���������");
        todos.Add("������� ������ ������� �������� ���������������������� �������� ��� ���������, ��������� � ������");
        todos.Add("����� � suburb � �������� ������ ���������� �������");
        todos.Add("�������� � ���������� ������� �����������, ������ ������");

        foreach(var todo in todos)
            Debug.Log(todo);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
