using System.Text;
using static System.Console;
using System.Runtime.InteropServices;
List<Vstrecha> meet = new List<Vstrecha>();
meet.Add(
    new Vstrecha()
    {
        index = meet.Count,
        Time_Start = new DateTime(2022, 12, 01),     //Для проверки функционала программы
        Time_End = new DateTime(2023, 01, 01)
    });  
Start_Frame();
void Start_Frame() //Функция начального экрана
{
    WriteLine("\t\t\t\t\t\t  ----------------");
    WriteLine("\t\t\t\t\t\t<-Добро пожаловать!->");
    WriteLine("\t\t\t\t\t\t  ----------------");
    Thread.Sleep(2000);
    Clear();
    Meetings();
}
void Selection_Frame() //Функция экрана с выбором действий
{
    WriteLine("\t\t\t\t\t\t ---------");
    WriteLine("\t\t\t\t\t\t|-Встречи-|");
    WriteLine("\t\t\t\t\t\t ---------");
    WriteLine("     1) Добавить встречу               2) Отменить встречу            3) Изменить встречу\n" +
              "     4) Посмотреть встречи на день     5) Посмотреть все встречи      6) Экспорт встреч в текстовый файл\n" +
              "                                             0) Выйти");
}
int Select() //Функция выбора вариантов
{
    Clear();
    Selection_Frame();
    switch (CheckKeyForSelect())
    {
        case 1:
             Add();
             break;
        case 2:
             Delete();
             break;
         case 3:
            ChangeMeet();
            break;
        case 4:
            ShowMeetInThisDay();
            break;
        case 5:
            ShowAllMeet();
            break;
        case 6:
            ExportInTxtFile();
            WriteLine("Экспорт произошёл успешно! Проверьте папку Debug.");
            break;
        case 0:
            WriteLine("До скорых встреч :)");
            break;
        default:
            {
                WriteLine("Ошибка");
                break;
            }
    }
    return CheckKeyForSelect();
}
void Meetings() //Функция встреч
{
    Select();
    int choice;
    ReadKey(true);
    do
    {
        choice = Select();
    }while (choice != 0);
    Clear();
}
int CheckKeyForSelect() //Функция для проверки нажатой кнопки
{
    ConsoleKeyInfo key;
    int number = 1;
    while (true)
    {
        key = ReadKey(true);
        if (Char.IsNumber(key.KeyChar))
        {
            if (Int32.TryParse(key.KeyChar.ToString(), out number))
            {
                if (number >= 0 && number <= 6)
                    return number;
            }
            else
                WriteLine("Преобразование прошло неудачно, попробуйте еще раз!");
        }
    }
}
//--------------------------------------------------------
int MeetIndexInList(int c) //Вспомогательная функция для MeetSelection(), используется для назначения индекса встрече
{
    for (int i = 0; i < meet.Count; i++)
    {
        if (meet[i].index == c)
            return i;
    }
    return -1;
}

int MeetSelection() //Функция выбора встречи по индексу (когда удаляем и тд.)
{
    ShowAllMeet();
    string str;
    int choice;
    do
    {
        WriteLine("Введите индекс встречи: ");
        str = ReadLine();
        if (Int32.TryParse(str, out choice) && choice < meet.Count)
        {
            if (MeetIndexInList(choice) != -1)
                return MeetIndexInList(choice);
        }
        WriteLine("Введено неверное значение!");
    }while (true);
}
//----------------------------------------------------------
void Add() //Функция добавления встречи
{
    Clear();
    DateTime start, end;
    WriteLine("Введите дату начала встречи (планировать их можно только наперёд):");
    do
    {
        start = Date_Input();
        if (start > DateTime.Now)
        {
            WriteLine("Вы успешно запланировали дату начала встречи.");
            WriteLine("<-------------------------------------------->");
            break;
        }
        Clear();
        WriteLine("Введена некорректная дата, попробуйте еще раз! (Возможно, вы ввели дату раньше чем сейчас)");
    } while (true);
    WriteLine("Введите дату конца встречи (может окончится только после ее начала):");
    do
    {
        end = Date_Input();
        if (start < end)
        {
            WriteLine("Вы успешно запланировали время конца встречи.");
            WriteLine("<-------------------------------------------->");
            break;
        }
        Clear();
        WriteLine("Введена некорректная дата, попробуйте еще раз! (Возможно, вы ввели конец встречи который случиться раньше чем начало этой встречи)");
    } while (true);
    meet.Add(
        new Vstrecha()
        {
            index = meet.Last().index + 1,
            Time_Start = start,
            Time_End = end
        });
}
void Delete() //Функция добавления встречи
{
    Clear();
    WriteLine("Введите индекс встречи, которую хотите удалить:");
    meet.RemoveAt(MeetSelection());
    WriteLine("Вы успешно удалили встречу.");
}
void ChangeMeet() //Функция изменения встречи
{
    Clear();
    WriteLine("Выберите какую встречу вы хотите изменить:");
    int ind = MeetSelection();
    DateTime start, end;
    WriteLine("Введите новую дату начала встречи (планировать их можно только наперёд):");
    do
    {
        start = Date_Input();
        if (start > DateTime.Now)
        {
            WriteLine("Вы успешно запланировали дату начала встречи.");
            break;
        }
        Clear();
        WriteLine("Введена некорректная дата, попробуйте еще раз! (Возможно, вы ввели дату раньше чем сейчас)");
    } while (true);
    WriteLine("Введите новую дату конца встречи (может окончится только после ее начала):");
    do
    {
        end = Date_Input();
        if (start < end)
        {
            WriteLine("Вы успешно запланировали время конца встречи.");
            break;
        }
        Clear();
        WriteLine("Введена некорректная дата, попробуйте еще раз! (Возможно, вы ввели конец встречи который случиться раньше чем начало этой встречи)");
    } while (true);
    meet[ind].Time_End = end;
    WriteLine("\nВы успешно изменили время начала и конца встречи.");
}
void ShowAllMeet() //Функция просмотра всех встреч
{
    Clear();
    WriteLine("Список встреч:\n");
    if (meet.Count == 0)
    {
        WriteLine("У вас нет запланированных встреч.");
    }
    else
    {
        foreach (Vstrecha i in meet)
        {
            i.Show();
        }
    }
}
DateTime Date_Input() //Функция ввода даты/времени
{
    int year, month, day, hour, min;
    string str;
    do
    {
        WriteLine("Введите год:");
        str = ReadLine();
        if (Int32.TryParse(str, out year) && year >= DateTime.Now.Year)
            break;
        else
            WriteLine("Вы ввели неверное значение!");
    } while (true);
    do
    {
        WriteLine("Введите месяц:");
        str = ReadLine();
        if (Int32.TryParse(str, out month) && month >= 0 && month <= 12)
            break;
        else
            WriteLine("Вы ввели неверное значение!");
    } while (true);
    do
    {
        WriteLine("Введите день:");
        str = ReadLine();
        if (Int32.TryParse(str, out day) && day >= 0 && day <= System.DateTime.DaysInMonth(year, month))
            break;
        else
            WriteLine("Вы ввели неверное значение!");
    } while (true);
    do
    {
        WriteLine("Введите час:");
        str = ReadLine();
        if (Int32.TryParse(str, out hour) && hour >= 0 && hour <= 24)
            break;
        else
            WriteLine("Вы ввели неверное значение!");
    } while (true);
    do
    {
        WriteLine("Введите минуты:");
        str = ReadLine();
        if (Int32.TryParse(str, out min) && min >= 0 && min <= 60)
            break;
        else
            WriteLine("Вы ввели неверное значение!");
    } while (true);
    return new DateTime(year, month, day, hour, min, 0);
}
void ShowMeetInThisDay() //Функция просмотра встреч на определенный день
{
    Clear();
    int year, month, day;
    bool flag = false;
    string str;
    do
    {
        WriteLine("Введите год:");
        str = ReadLine();
        if (Int32.TryParse(str, out year) && year >= DateTime.Now.Year)
            break;
        else
            WriteLine("Вы ввели неверное значение!");
    } while (true);
    do
    {
        WriteLine("Введите месяц:");
        str = ReadLine();
        if (Int32.TryParse(str, out month) && month >= 0 && month <= 12)
            break;
        else
            WriteLine("Вы ввели неверное значение!");
    } while (true);
    do
    {
        WriteLine("Введите день:");
        str = ReadLine();
        if (Int32.TryParse(str, out day) && day >= 0 && day <= System.DateTime.DaysInMonth(year, month))
            break;
        else
            WriteLine("Вы ввели неверное значение!");
    } while (true);
    foreach (Vstrecha i in meet)
    {
        if (i.Time_Start.Year == year && i.Time_Start.Month == month && i.Time_Start.Day == day)
        {
            WriteLine();
            i.Show();
            flag = true;
        }
    }
    if (flag)
        WriteLine($"Это все встречи, запланированные на {day} {month} {year}.");
    else
        WriteLine($"У вас нет запланированных встреч на {day} {month} {year}.");
}
void ExportInTxtFile() //Функция экспорта в txt
{
    Clear();
    string file_name = "Встречи.txt";
    using (FileStream fs = new FileStream(file_name, FileMode.Create))
    {
        using (StreamWriter sw = new StreamWriter(fs,Encoding.Unicode))
        {
            foreach (Vstrecha i in meet)
            {
                sw.WriteLine(" -----------------------");
                sw.WriteLine($"| -Начало встречи назначено на: {i.Time_Start.ToString("f")}");
                sw.WriteLine($"| -Конец встречи назначен на: {i.Time_End.ToString("f")}");
                sw.WriteLine(" -----------------------\n");
            }
        }
    }
}
class Vstrecha //Класс встреч
{
    public int index;
    public DateTime Time_Start { get; set; }
    public DateTime Time_End { get; set; }
    public void Show()
    {
        WriteLine(" -------------------------");
        WriteLine($"| -Номер встречи: {index}\n" +
                  $"| -Начало встречи: {Time_Start}\n" +
                  $"| -Конец встречи: {Time_End}");
        WriteLine(" -------------------------");
    }
}