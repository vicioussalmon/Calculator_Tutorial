﻿// Laboratory_5_1.cpp : Этот файл содержит функцию "main". Здесь начинается и заканчивается выполнение программы.
//
#include <math.h> 
#include <iostream> 
#include <Windows.h> 
#pragma warning(disable : 4996)

int main()
{
    setlocale(LC_ALL, "Russian");

    long float a, b, c, x1, x2, x, D;
    while (1)
    {

        printf("Введите значения a = ");
        scanf("%lf", &a);
        printf("Введите значения b = ");
        scanf("%lf", &b);
        printf("Введите значения c = ");
        scanf("%lf", &c);

        D = (pow(b, 2) - (4 * a * c));
        x1 = (-b - sqrt(D)) / (2 * a);
        x2 = (-b + sqrt(D)) / (2 * a);
        x = -b / 2 * a;

        if (D > 0)
            printf("x1 = %lf \n", x1) && printf("x2 = %lf \n", x2);
        else
            if (D == 0)
                printf("Один корень уравнения\n x = %lf", x);
            else
                if (D < 0)
                    printf("Нет корней уравнения\n");
    }
    return 0;
}

// Запуск программы: CTRL+F5 или меню "Отладка" > "Запуск без отладки"
// Отладка программы: F5 или меню "Отладка" > "Запустить отладку"

// Советы по началу работы 
//   1. В окне обозревателя решений можно добавлять файлы и управлять ими.
//   2. В окне Team Explorer можно подключиться к системе управления версиями.
//   3. В окне "Выходные данные" можно просматривать выходные данные сборки и другие сообщения.
//   4. В окне "Список ошибок" можно просматривать ошибки.
//   5. Последовательно выберите пункты меню "Проект" > "Добавить новый элемент", чтобы создать файлы кода, или "Проект" > "Добавить существующий элемент", чтобы добавить в проект существующие файлы кода.
//   6. Чтобы снова открыть этот проект позже, выберите пункты меню "Файл" > "Открыть" > "Проект" и выберите SLN-файл.
