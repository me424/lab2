using System;
using System.Collections.Generic;

// Интерфейс наблюдателя
public interface IObserver
{
    void Update(string message);
}

// Конкретный наблюдатель
public class TextBox : IObserver
{
    private string name;

    public TextBox(string name)
    {
        this.name = name;
    }

    public void Update(string message)
    {
        Console.WriteLine($"{name} получил сообщение: {message}");
    }
}

// Интерфейс наблюдаемого объекта
public interface ISubject
{
    void Attach(IObserver observer);
    void Detach(IObserver observer);
    void Notify(string message);
}

// Конкретный наблюдаемый объект
public class Button : ISubject
{
    private List<IObserver> observers = new List<IObserver>();

    public void Attach(IObserver observer)
    {
        observers.Add(observer);
    }

    public void Detach(IObserver observer)
    {
        observers.Remove(observer);
    }

    public void Notify(string message)
    {
        foreach (var observer in observers)
        {
            observer.Update(message);
        }
    }

    // Метод, который будет использоваться для взаимодействия с UI-компонентами
    public void Click()
    {
        // При событии происходит оповещение наблюдателей
        Notify("Изменилось состояние");
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Создаем объекты наблюдателей
        var observer1 = new TextBox("Текстовое поле 1 ");
        var observer2 = new TextBox("Текстовое поле 2");

        // Создаем наблюдаемый объект
        var subject = new Button();

        // Подписываем наблюдателей на субъект
        subject.Attach(observer1);
        subject.Attach(observer2);

        // Имитация действия пользователя (клик по кнопке)
        subject.Click();

        // Отписываем одного из наблюдателей
        subject.Detach(observer2);

        // Вызываем метод субъекта снова
        subject.Click();
    }
}
